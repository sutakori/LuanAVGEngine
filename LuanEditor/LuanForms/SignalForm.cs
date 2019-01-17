using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inst = LuanCore.Instructions;


namespace LuanEditor.LuanForms
{
    public partial class SignalForm : Form
    {
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;
        public SignalForm(int index)
        {
            InitializeComponent();
            this.index = index;
            this.comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
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
            string s = "        ◇信号:";
            string signalname = "";
            Inst.Signal signal = new Inst.Signal();
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    signalname = "保存";
                    signal.Name = "save";
                    break;
                case 1:
                    signalname = "读取";
                    signal.Name = "load";
                    break;
                case 2:
                    signalname = "设置";
                    signal.Name = "config";
                    break;
                case 3:
                    signalname = "回看";
                    signal.Name = "log";
                    break;
                case 4:
                    signalname = "回到标题";
                    signal.Name = "title";
                    break;
            }
            s = s + signalname;
            foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
            {
                if (scene.Name == scenename)
                {
                    scene.Instructions.Insert(index - sceneindex - 1, signal);
                }
            }
            (this.Owner as MainForm).codeListBox.Items.Insert(index,s);
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }
    }
}
