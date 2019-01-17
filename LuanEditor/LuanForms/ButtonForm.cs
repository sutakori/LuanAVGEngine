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
    public partial class ButtonForm : Form
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
        /// 获取或设置背景资源名
        /// </summary>
        public string GotFileName { get; set; }
        public ButtonForm(bool isEdit,int index,string filename="",string buttonname = "", string signal = "")
        {
            InitializeComponent();
            this.isEditing = isEdit;
            this.index = index;
            this.comboBox1.SelectedIndex = 0;
            if (this.isEditing)
            {
                this.textBox1.Text = filename;
                this.textBox2.Text = buttonname;
                switch (signal)
                {
                    case "无":
                        this.comboBox1.SelectedIndex = 0;
                        break;
                    case "保存":
                        this.comboBox1.SelectedIndex = 1;
                        break;
                    case "读取":
                        this.comboBox1.SelectedIndex = 2;
                        break;
                    case "设置":
                        this.comboBox1.SelectedIndex = 3;
                        break;
                    case "回看":
                        this.comboBox1.SelectedIndex = 4;
                        break;
                    case "回到标题":
                        this.comboBox1.SelectedIndex = 5;
                        break;
                }          
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PicResourceForm prf = new PicResourceForm("选择素材", 0);
            prf.ShowDialog(this);
            this.textBox1.Text = this.GotFileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == String.Empty)
            {
                MessageBox.Show("请选择图像");
                return;
            }
            if (this.textBox2.Text == String.Empty)
            {
                MessageBox.Show("文字不能为空");
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
            Inst.Button button = new Inst.Button();
            button.Signal = new Inst.Signal();
            string signal = "";
            switch(this.comboBox1.SelectedIndex)
            {
                case 0:
                    signal = "无";
                    button.Signal = null;
                    break;
                case 1:
                    signal = "保存";
                    button.Signal.Name = "save";
                    break;
                case 2:
                    signal = "读取";
                    button.Signal.Name = "load";
                    break;
                case 3:
                    signal = "设置";
                    button.Signal.Name = "config";
                    break;
                case 4:
                    signal = "回看";
                    button.Signal.Name = "log";
                    break;
                case 5:
                    signal = "回到标题";
                    button.Signal.Name = "title";
                    break;
            }
            button.Filename = this.textBox1.Text;
            button.Label = this.textBox2.Text;
            button.X = (double)this.numericUpDown1.Value;
            button.Y = (double)this.numericUpDown2.Value;
            button.ScaleX = (double)this.numericUpDown3.Value / 100.0;
            button.ScaleY = (double)this.numericUpDown4.Value / 100.0;
            button.Opacity = (double)this.numericUpDown5.Value / 100.0;
            string s = this.textBox1.Text + "|按钮文字:" + this.textBox2.Text + "|X:" + this.numericUpDown1.Value.ToString() + 
                "|Y:" + this.numericUpDown2.Value.ToString() + "|X缩放比:" + this.numericUpDown3.Value.ToString() + "|Y缩放比:" + 
                this.numericUpDown4.Value.ToString() + "|不透明度:" + this.numericUpDown5.Value.ToString() + "|信号:" + signal;
            if(this.isEditing)
            {
                foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                {
                    if (scene.Name == scenename)
                    {
                        scene.Instructions.RemoveAt(index - sceneindex - 1);
                        scene.Instructions.Insert(index - sceneindex - 1, button);
                    }
                }
                (this.Owner as MainForm).codeListBox.Items.RemoveAt(index);
                (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇按钮:" + s);
            } else
            {
                foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                {
                    if (scene.Name == scenename)
                    {
                        scene.Instructions.Insert(index - sceneindex - 1, button);
                    }
                }
                (this.Owner as MainForm).codeListBox.Items.Insert(index, "        ◇按钮:" + s);
            }
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }
    }
}
