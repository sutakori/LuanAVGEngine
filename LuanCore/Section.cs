using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuanCore
{
    /// <summary>
    /// 剧本单元，由scene序列组成
    /// </summary>
    [Serializable]
    public class Section
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public List<Scene> Scenes { get; set; } = new List<Scene>();

        public void Save(string path)
        {
            string filepath = path + "\\" + GlobalConfig.Path_Section + this.Name + "." + GlobalConfig.Ext_Section;
            if (File.Exists(filepath))
                File.Delete(filepath);
            FileStream fs = new FileStream(filepath, FileMode.Create);
            using(StreamWriter file = new StreamWriter(fs))
            {
                foreach(var scene in Scenes)
                {
                    file.WriteLine($"@scene name=\"{scene.Name}\"" + " {}");
                    foreach (var inst in scene.Instructions)
                    {
                        file.WriteLine(inst.ToString());
                    }
                }
            }
            fs.Close();
        }

        public void InitScene(string name)
        {
            Scenes.Add(new Scene(name));
        }

        public void AddInstruction(Instruction instruction)
        {
            Scenes[Scenes.Count - 1].Instructions.Add(instruction);
        }
    }
}
