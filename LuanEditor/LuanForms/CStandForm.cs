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
using LuanCore;


namespace LuanEditor.LuanForms
{
    public partial class CStandForm : Form
    {
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;
        /// <summary>
        /// 记录立绘的人物名称
        /// </summary>
        public string name;
        /// <summary>
        /// 记录立绘的人物表情
        /// </summary>
        public string face;
        /// <summary>
        /// 记录扩展名
        /// </summary>
        public string ext;
        /// <summary>
        /// 记录是否需要缩放比
        /// </summary>
        private bool check1 = false;
        public CStandForm(int index)
        {
            InitializeComponent();
            this.checkBox1.Checked = false;
            this.comboBox1.SelectedIndex = 0;
            this.index = index;
        }


        /// <summary>
        /// 按钮：确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == String.Empty)
            {
                MessageBox.Show("请选择图像");
                return;
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
            string s = "人物:" + this.name + "|表情:" + this.face + "|扩展名:" + this.ext + "|" ;
            if(this.checkBox1.Checked)
            {
                s = s + "X缩放比:" + this.numericUpDown1.Value.ToString() + "|Y缩放比:" + this.numericUpDown2.Value.ToString() + "|";
            }
            s = s + "相对位置:" + this.comboBox1.SelectedItem.ToString();
            Inst.Stand stand = new Inst.Stand();
            stand.Name = this.name;
            stand.Face = this.face;
            stand.Ext = this.ext;
            switch(this.comboBox1.SelectedIndex)
            {
                case 0:
                    stand.Pos = Inst.StandPos.Left;
                    break;
                case 1:
                    stand.Pos = Inst.StandPos.MidLeft;
                    break;
                case 2:
                    stand.Pos = Inst.StandPos.Mid;
                    break;
                case 3:
                    stand.Pos = Inst.StandPos.MidRight;
                    break;
                case 4:
                    stand.Pos = Inst.StandPos.Right;
                    break;
            }
            if(this.checkBox1.Checked)
            {
                stand.ScaleX = (double)this.numericUpDown1.Value / 100.0;
                stand.ScaleY = (double)this.numericUpDown2.Value / 100.0;
            }
            foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
            {
                if (scene.Name == scenename)
                {
                    scene.Instructions.Insert(index - sceneindex - 1, stand);
                }
            }
            (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇显示立绘:" + s);
            //(this.Owner as MainForm).Data[sectionname][scenename].Insert(index - sceneindex - 1, "        ◇显示立绘:" + s);
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }

        /// <summary>
        /// 按钮：选择图像
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            StandForm formstand = new StandForm();
            formstand.Owner = this;
            formstand.ShowDialog(this);
            this.textBox1.Text = this.GotFileName;
        }

        /// <summary>
        /// 获取或设置背景资源名
        /// </summary>
        public string GotFileName { get; set; }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
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
