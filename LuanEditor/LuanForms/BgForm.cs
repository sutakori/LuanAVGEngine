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
    public partial class BgForm : Form
    {
        private readonly bool isEditing;
        /// <summary>
        /// 记录是否需要缩放比
        /// </summary>
        private bool check1 = false;
        /// <summary>
        /// 记录是否调整不透明度
        /// </summary>
        private bool check2 = false;
        private int index;
        public BgForm(bool isEdit, int index, string filename = "", string xscale = "", string yscale = "", string op = "")
        {
            InitializeComponent();
            this.isEditing = isEdit;
            this.index = index;
            if (isEdit)
            {
                this.textBox1.Text = filename;
                if(xscale == string.Empty) //没有缩放比
                {
                    this.checkBox1.Checked = false;
                } else
                {
                    this.checkBox1.Checked = true;
                    this.check1 = true;
                    this.numericUpDown1.Enabled = true;
                    this.numericUpDown2.Enabled = true;
                    this.numericUpDown1.Value = Convert.ToInt32(xscale);
                    this.numericUpDown2.Value = Convert.ToInt32(yscale);
                }
                if(op == string.Empty)
                {
                    this.checkBox2.Checked = false;
                } else
                {
                    this.checkBox2.Checked = true;
                    this.check2 = true;
                    this.numericUpDown3.Enabled = true;
                    this.numericUpDown3.Value = Convert.ToInt32(op);
                }
                this.GotFileName = filename;
            }
        }

        /// <summary>
        /// 按钮：选择图像
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            PicResourceForm prf = new PicResourceForm("选择背景", 2);
            prf.ShowDialog(this);
            this.textBox1.Text = this.GotFileName;
        }

        /// <summary>
        /// 按钮：确定
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == String.Empty)
            {
                MessageBox.Show(@"请选择图像");
                return;
            }
            string s = "";
            s = s + this.textBox1.Text;
            if(check1)
            {
                s = s + "|X缩放比:" + this.numericUpDown1.Value.ToString() + "|Y缩放比:" + this.numericUpDown2.Value.ToString();
            }
            if(check2)
            {
                s = s + "|不透明度:" + this.numericUpDown3.Value.ToString();
            }
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
            Inst.Bg bg;
            if (this.isEditing)
            {
                foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                {
                    if (scene.Name == scenename)
                    {
                        scene.Instructions.RemoveAt(index - sceneindex - 1);
                        bg = new Inst.Bg();
                        bg.Filename = this.textBox1.Text;
                        if (this.checkBox1.Checked)
                        {
                            double xscale = (double)this.numericUpDown1.Value;
                            double yscale = (double)this.numericUpDown2.Value;
                            bg.ScaleX = xscale / 100.0;
                            bg.ScaleY = yscale / 100.0;
                        } else
                        {
                            bg.ScaleX = 0.0;
                            bg.ScaleY = 0.0;
                        }
                        if(this.checkBox2.Checked)
                        {
                            double op = (double)this.numericUpDown3.Value;
                            bg.Opacity = op / 100.0;
                        } else
                        {
                            bg.Opacity = 0.0;
                        }
                        scene.Instructions.Insert(index - sceneindex - 1, bg);
                    }
                }
                (this.Owner as MainForm).codeListBox.Items.RemoveAt(index);
                (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇显示背景:" + s);
                //(this.Owner as MainForm).Data[sectionname][scenename][index - sceneindex - 1] = "        ◇显示背景:" + s;
            }
            else
            {
                foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                {
                    if (scene.Name == scenename)
                    {
                        bg = new Inst.Bg();
                        bg.Filename = this.textBox1.Text;
                        if (this.checkBox1.Checked)
                        {
                            double xscale = (double)this.numericUpDown1.Value;
                            double yscale = (double)this.numericUpDown2.Value;
                            bg.ScaleX = xscale / 100.0;
                            bg.ScaleY = yscale / 100.0;
                        }
                        else
                        {
                            bg.ScaleX = 0.0;
                            bg.ScaleY = 0.0;
                        }
                        if (this.checkBox2.Checked)
                        {
                            double op = (double)this.numericUpDown3.Value;
                            bg.Opacity = op / 100.0;
                        }
                        else
                        {
                            bg.Opacity = 0.0;
                        }
                        scene.Instructions.Insert(index - sceneindex - 1, bg);
                    }
                }
                (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇显示背景:" + s);
                //(this.Owner as MainForm).Data[sectionname][scenename].Insert(index - sceneindex - 1, "        ◇显示背景:" + s);
            }
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }

        /// <summary>
        /// 获取或设置背景资源名
        /// </summary>
        public string GotFileName { get; set; }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            if (check2)
            {
                check2 = false;
                this.numericUpDown3.Enabled = false;
                this.checkBox2.Checked = false;
            }
            else
            {
                check2 = true;
                this.numericUpDown3.Enabled = true;
                this.checkBox2.Checked = true;
            }
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (check1)
            {
                check1 = false;
                this.checkBox1.Checked = false;
                this.numericUpDown1.Enabled = false;
                this.numericUpDown2.Enabled = false;
            }
            else
            {
                check1 = true;
                this.checkBox1.Checked = true;
                this.numericUpDown1.Enabled = true;
                this.numericUpDown2.Enabled = true;
            }
        }
    }
}
