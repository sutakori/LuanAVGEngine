using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuanCore;
using Inst = LuanCore.Instructions;


namespace LuanEditor.LuanForms
{
    /// <summary>
    /// 窗体：显示对话
    /// </summary>
    public partial class DialogForm : Form
    {
        /// <summary>
        /// 是否编辑模式
        /// </summary>
        private bool isEditing;
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;
        /// <summary>
        /// 记录是否赋值
        /// </summary>
        private bool check1 = false;
        /// <summary>
        /// 记录是否配音
        /// </summary>
        private bool check2 = false;
        /// <summary>
        /// Vocal中人物的名字
        /// </summary>
        public string standName = "";
        /// <summary>
        /// Vocal的名字
        /// </summary>
        public string vocalName = "";
        /// <summary>
        /// 构造器
        /// </summary>
        public DialogForm(string text, bool isEdit, int index = -1,string editStr = "",string blockStr = "",string vocalStr = "")
        {
            InitializeComponent();
            this.Text = text;
            this.isEditing = isEdit;
            this.index = index;
            if (this.isEditing)
            {
                this.textBox1.Text = editStr;
                this.textBox2.Text = blockStr;
                if(blockStr == string.Empty)
                {
                    this.checkBox1.Checked = false;
                } else
                {
                    this.checkBox1.Checked = true;
                    this.check1 = true;
                    this.textBox2.Enabled = true;
                    this.textBox2.Text = blockStr;
                }
                if(vocalStr == string.Empty)
                {
                    this.CheckBox2.Checked = false;
                } else
                {
                    this.CheckBox2.Checked = true;
                    this.check2 = true;
                    this.button2.Enabled = true;
                    this.textBox3.Enabled = true;
                    this.textBox3.Text = vocalStr;
                    this.standName = vocalStr.Split('\\')[0];
                    this.vocalName = vocalStr.Split('\\')[1];
                }
            }
        }

        /// <summary>
        /// 按钮：确定
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            
            if((this.textBox1.Text == string.Empty) || (this.checkBox1.Checked && this.textBox2.Text.Trim() == string.Empty) || (this.CheckBox2.Checked && this.textBox3.Text.Trim() == string.Empty))
            {
                MessageBox.Show("文本不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                (this.Owner as MainForm).isSave = false;
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
                foreach(string str in (this.Owner as MainForm).codeListBox.Items)
                {
                    if(str.Length > 7)
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
                //判断需不需要赋值
                string blockstr;
                if(this.checkBox1.Checked)
                {
                    blockstr = (this.textBox2.Text).Replace("\n", "");
                } else
                {
                    blockstr = "";
                }
                Inst.Text text;
                string s = "        ◇显示对话:" + (this.textBox1.Text).Replace("\n", "");
                if(this.checkBox1.Checked)
                {
                    s = s + "|赋值:" + (this.textBox2.Text).Replace("\n", "");
                }
                if(this.CheckBox2.Checked)
                {
                    s = s + "|配音:" + this.standName + "\\" + this.vocalName;
                }
                if (this.isEditing)
                {
                    (this.Owner as MainForm).codeListBox.Items.RemoveAt(index);
                    (this.Owner as MainForm).codeListBox.Items.Insert(index, s);
                    //(this.Owner as MainForm).Data[sectionname][scenename][index-sceneindex-1] = "        ◇显示对话:" + this.textBox1.Text;
                    foreach(var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                    {
                        if(scene.Name == scenename)
                        {
                            scene.Instructions.RemoveAt(index - sceneindex - 1);
                            text = new Inst.Text();
                            text.Content = (this.textBox1.Text).Replace("\n", "");
                            text.SetBlock((this.textBox2.Text).Replace("\n", ""));
                            if(this.CheckBox2.Checked)
                            {
                                Inst.Vocal vocal = new Inst.Vocal();
                                vocal.Filename = this.vocalName;
                                vocal.Name = this.standName;
                                text.Vocal = vocal;
                            }
                            else
                            {
                                text.Vocal = null;
                            }
                            scene.Instructions.Insert(index - sceneindex - 1, text);
                        }
                    }
                } else
                {
                    (this.Owner as MainForm).codeListBox.Items.Insert(index, s);
                    foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            text = new Inst.Text();
                            text.Content = (this.textBox1.Text).Replace("\n", "");
                            text.SetBlock((this.textBox2.Text).Replace("\n", ""));
                            if (this.CheckBox2.Checked)
                            {
                                Inst.Vocal vocal = new Inst.Vocal();
                                vocal.Filename = this.vocalName;
                                vocal.Name = this.standName;
                                text.Vocal = vocal;
                            }
                            else
                            {
                                text.Vocal = null;
                            }
                        scene.Instructions.Insert(index - sceneindex - 1, text);
                        }
                    }
                //(this.Owner as MainForm).Data[sectionname][scenename].Insert(index-sceneindex-1,"        ◇显示对话:" + this.textBox1.Text);
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VocalForm formvocal = new VocalForm();
            formvocal.Owner = this;
            formvocal.ShowDialog();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            if (check2)
            {
                check2 = false;
                this.CheckBox2.Checked = false;
                this.textBox3.Enabled = false;
                this.button2.Enabled = false;
            }
            else
            {
                check2 = true;
                this.CheckBox2.Checked = true;
                this.textBox3.Enabled = true;
                this.button2.Enabled = true;
            }
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
