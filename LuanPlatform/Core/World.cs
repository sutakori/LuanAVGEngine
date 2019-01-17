using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LuanCore;
using LuanUtils;
using LuanPlatform.Core.VM;
using LuanCore.Instructions;
using LuanPlatform.Core.Graphic;

namespace LuanPlatform.Core
{
    class World
    {

        private void InitConfig()
        {
            
        }

        private void InitRuntime()
        {
            World.RunMana.LoadSection(GlobalConfig.Section_Title);
        }
        //
        public static World GetInstance()
        {
            return World.instance ?? (World.instance = new World());
        }

        private World()
        {
            this.InitConfig();

            World.RunMana = RuntimeManager.Recover() ?? RuntimeManager.GetInstance();
            this.InitRuntime();
        }

        #region 前端更新后台相关函数
        /// <summary>
        /// 提供由前端更新后台键盘按键信息的方法
        /// </summary>
        /// <param name="e">键盘事件</param>
        /// <param name="isDown">是否按下</param>
        public void UpdateKeyboard(KeyEventArgs e, bool isDown)
        {
            LogUtils.Log(String.Format("Keyboard event: {0} <- {1}", e.Key, e.KeyStates),
                "Director", LogLevel.Info);
        }

        /// <summary>
        /// 提供由前端更新后台鼠标按键信息的方法
        /// </summary>
        /// <param name="e">鼠标事件</param>
        public void UpdateMouse(MouseButtonEventArgs e)
        {
            if (State == WorldState.Wait)
                World.RunMana.RunStep();
            else if (State == WorldState.Ani)
                World.RunMana.Stabilize();
            else if (State==WorldState.End)
            {
                MessageBox.Show("游戏结束，将返回标题界面");
                Interpreter.GetInstance().Submit(new Shift()
                {
                    Typ = ShiftTyp.section,
                    Target = GlobalConfig.Section_Title
                });
                RunMana.RunStep();
                State = WorldState.AniForce;
            }
        }

        /// <summary>
        /// 提供由前端更新后台鼠标滚轮信息的方法
        /// </summary>
        /// <param name="delta">鼠标滚轮差分，上滚为正，下滚为负</param>
        public void UpdateMouseWheel(int delta)
        {
        }
        #endregion

        /// <summary>
        /// 暂停消息循环
        /// </summary>
        public static void PauseUpdateContext()
        {
            World.IsContextUpdatePaused = true;
            LogUtils.Log("Context Update Dispatcher is stopped", "Director", LogLevel.Info);
        }

        /// <summary>
        /// 恢复消息循环
        /// </summary>
        public static void ResumeUpdateContext()
        {
            World.IsContextUpdatePaused = false;
            LogUtils.Log("Context Update Dispatcher is resumed", "Director", LogLevel.Info);
        }

        #region signal响应

        public void Start(Dictionary<string, string> Args)
        {
            //RunMana.RunStep();
            State = WorldState.Wait;
        }
        #endregion
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;

        public static RuntimeManager RunMana;

        public WorldState State { get; set; } = WorldState.AniForce;
        
        // 唯一实例
        private static World instance = null;

        public static bool IsContextUpdatePaused { get; private set; } = true;
    }

    public enum WorldState
    {
        /// <summary>
        /// 处于稳定状态，等待操作
        /// </summary>
        Wait,
        /// <summary>
        /// 自动播放状态，可以跳过
        /// </summary>
        Ani,
        /// <summary>
        /// 强制播放状态，不响应操作
        /// </summary>
        AniForce,
        /// <summary>
        /// 游戏结束
        /// </summary>
        End
    }
}
