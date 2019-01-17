using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuanCore;
using LuanPlatform.Core.VM;

namespace LuanPlatform.Core
{
    [Serializable]
    class RuntimeManager
    {
        internal void LoadSection(string name)
        {
            this.section = resourceManager.GetSection(name);
            if (this.section == null)
            {
                World.GetInstance().State = WorldState.End;
                return;
            }
            this.scenes = new Queue<Scene>(section.Scenes);
            if (this.scenes.Count > 0)
            {
                interpreter.LoadInstructions(scenes.Dequeue().Instructions);
            }
        }

        internal void LoadSectionById(int id)
        {
            this.section = resourceManager.GetSectionById(id);
            if (this.section == null)
            {
                World.GetInstance().State = WorldState.End;
                return;
            }
            this.scenes = new Queue<Scene>(section.Scenes);
            if (this.scenes.Count > 0)
            {
                interpreter.LoadInstructions(scenes.Dequeue().Instructions);
            }
        }

        /// <summary>
        /// 前进到稳定的游戏状态，结束文本动画、语音与（尚未实现）场景动画。
        /// </summary>
        internal void Stabilize()
        {
            frame.Stabilize();
        }

        /// <summary>
        /// 解释脚本到稳定的命令，如文本，分支，同步命令
        /// </summary>
        internal void RunStep()
        {
            //if (World.GetInstance().State==WorldState.End) return;
            var sceneStep = interpreter.SteadyStep(ref frame);
            RuntimeShift shift = sceneStep.shift;
            // 每个新scene刷新页面
            if (shift != RuntimeShift.Wait) frame.Clear();
            switch (shift)
            {
                case RuntimeShift.Scene:
                    ShiftScene(sceneStep.target);
                    interpreter.SteadyStep(ref frame);
                    break;
                case RuntimeShift.Section:
                    LoadSection(sceneStep.target);
                    interpreter.SteadyStep(ref frame);
                    break;
                case RuntimeShift.Next:
                    if (this.scenes.Count > 0)
                        interpreter.LoadInstructions(scenes.Dequeue().Instructions);
                    else
                        LoadSectionById(this.section.Id + 1);
                    interpreter.SteadyStep(ref frame);
                    break;
                case RuntimeShift.Wait:
                    break;
            }
        }

        internal void Save(string name)
        {
            LuanUtils.IOUtils.Serialize(this, GlobalConfig.SAVE_PATH + name + ".savedata");
        }

        /// <summary>
        /// 跳转到指定scene
        /// </summary>
        /// <param name="target"></param>
        private void ShiftScene(string target)
        {
            while (scenes.Count() > 0)
            {
                var s = scenes.Peek();
                if (s.Match(target))
                {
                    return;
                }
                scenes.Dequeue();
            }
        }

        internal static RuntimeManager RecoverFromSave(string name)
        {
            instance = (RuntimeManager)LuanUtils.IOUtils.Deserialize(GlobalConfig.SAVE_PATH + name + ".savedata");
            return instance;
        }

        internal static RuntimeManager Recover()
        {
            return null;
        }
        internal RuntimeManager()
        {
            interpreter = Interpreter.GetInstance();
            resourceManager = ResourceManager.GetInstance();
            frame = new Frame();
        }

        internal static RuntimeManager GetInstance()
        {
            return RuntimeManager.instance ?? (RuntimeManager.instance = new RuntimeManager());
        }
        private static RuntimeManager instance = null;

        [NonSerialized]
        private ResourceManager resourceManager = null;
        private bool isEnd = false;
        internal Section section { get; private set; }
        private Frame frame;
        private Interpreter interpreter;
        private Queue<Scene> scenes;
    }

    enum RuntimeShift
    {
        Scene,
        Section,
        Next,
        Wait
    }
}
