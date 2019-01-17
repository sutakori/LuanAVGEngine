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
    public partial class GuardendForm : Form
    {
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;
        /// <summary>
        /// 记录是否赋值
        /// </summary>
        private bool check1 = false;
        public GuardendForm(int index)
        {
            InitializeComponent();
            this.index = index;
            this.checkBox1.Checked = false;
            this.textBox2.Enabled = false;
        }

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
            string s = "        ◇条件结束:";
            if(this.checkBox1.Checked)
            {
                s = s + "赋值:" + (this.textBox2.Text.Trim()).Replace("\n", "");
            }
            (this.Owner as MainForm).codeListBox.Items.Insert(index, s);
            Inst.Guardend guardend = new Inst.Guardend();
            guardend.SetBlock((this.textBox2.Text.Trim()).Replace("\n", ""));
            foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
            {
                if (scene.Name == scenename)
                {
                    scene.Instructions.Insert(index - sceneindex - 1, guardend);
                }
            }
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {

            if (check1)
            {
                check1 = false;
                this.checkBox1.Checked = false;
                this.textBox2.Enabled = false;
            }
            else
            {
                check1 = true;
                this.checkBox1.Checked = true;
                this.textBox2.Enabled = true;
            }
        }
    }
}
