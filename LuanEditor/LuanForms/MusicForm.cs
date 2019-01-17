using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Inst = LuanCore.Instructions;

namespace LuanEditor.LuanForms
{
    public partial class MusicForm : Form
    {
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;

        public MusicForm(string title, int index, int type)
        {
            InitializeComponent();
            this.Text = title;
            this.index = index;
            this.tabControl1.SelectedIndex = type;
            this.tabControl1.TabPages.RemoveAt(1 - type);
            if (title == "音乐管理")
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.tabControl1.Enabled = true;
            }
        }

        private void MusicForm_Load(object sender, EventArgs e)
        {
            // 加载文件夹
            DirectoryInfo dirInfoBGM = new DirectoryInfo(this.SoundDir + @"\bgm");
            DirectoryInfo dirInfoBGS = new DirectoryInfo(this.SoundDir + @"\bgs");
            // 加载文件
            foreach (var f in dirInfoBGM.GetFiles())
            {
                this.listBoxBGM.Items.Add(f.Name);
            }
            foreach (var f in dirInfoBGS.GetFiles())
            {
                this.listBoxBGS.Items.Add(f.Name);
            }    
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl1.SelectedTab.Text)
            {
                case "BGM":
                    if (this.listBoxBGM.Items.Count > 0)
                    {
                        this.listBoxBGM.SelectedIndex = 0;
                    }
                    else
                    {
                        this.listBoxBGM.SelectedIndex = -1;
                    }
                    break;
                case "BGS":
                    if (this.listBoxBGS.Items.Count > 0)
                    {
                        this.listBoxBGS.SelectedIndex = 0;
                    }
                    else
                    {
                        this.listBoxBGS.SelectedIndex = -1;
                    }
                    break;
            }
        }

        /// <summary>
        /// 音频文件夹绝对路径
        /// </summary>
        private string SoundDir = Editor.projectFolder + @"\Source";

        private void button1_Click(object sender, EventArgs e)
        {
            string sectionname = "", scenename = "";
            if ((this.Owner as MainForm).projTreeView.SelectedNode.Parent != null)
            {
                sectionname = (this.Owner as MainForm).projTreeView.SelectedNode.Parent.Text;
                scenename = (this.Owner as MainForm).projTreeView.SelectedNode.Text;
            }
            else
            {
                sectionname = (this.Owner as MainForm).projTreeView.SelectedNode.Text;
                for (int i = index; i >= 0; i--)
                {
                    if ((this.Owner as MainForm).codeListBox.Items[i].ToString().StartsWith("@scene:"))
                    {
                        scenename = (this.Owner as MainForm).codeListBox.Items[i].ToString().Substring(7);
                        break;
                    }
                }
            }
            int sceneindex = 0; //记录scene在codelistbox中的位置
            foreach (string str in (this.Owner as MainForm).codeListBox.Items)
            {
                if (str.Length > 7)
                {
                    if (str.Substring(0, 7) == "@scene:")
                    {
                        if (str.Substring(7).Trim() == scenename)
                        {
                            sceneindex = (this.Owner as MainForm).codeListBox.Items.IndexOf(str);
                        }
                    }
                }
            }
            if (this.tabControl1.SelectedTab.Text == "BGM")
            {
                if (this.listBoxBGM.SelectedIndex != -1)
                {
                    (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇播放BGM:" + this.listBoxBGM.SelectedItem.ToString());
                    Inst.Bgm bgm = new Inst.Bgm();//添加播放的指令
                    bgm.Filename = this.listBoxBGM.SelectedItem.ToString();
                    foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            scene.Instructions.Insert(index - sceneindex - 1, bgm);
                            (this.Owner as MainForm).isSave = false;
                        }
                    }
                }
            }
            if (this.tabControl1.SelectedTab.Text == "BGS")
            {
                if (this.listBoxBGS.SelectedIndex != -1)
                {
                    (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇播放BGS:" + this.listBoxBGS.SelectedItem.ToString());
                    Inst.Bgs bgs = new Inst.Bgs();//添加播放的指令
                    bgs.Filename = this.listBoxBGS.SelectedItem.ToString();
                    foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            scene.Instructions.Insert(index - sceneindex - 1, bgs);
                            (this.Owner as MainForm).isSave = false;
                        }
                    }
                }
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
