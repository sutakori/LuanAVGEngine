using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LuanEditor.LuanForms
{
    public partial class VocalForm : Form
    {
        public VocalForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.projTreeView.SelectedNode == null)
            {
                MessageBox.Show("请选择文件!");
                return;
            }
            if (this.projTreeView.SelectedNode.Parent == null)
            {
                MessageBox.Show("请选择文件!");
            }
            else
            {
                (this.Owner as DialogForm).vocalName = this.projTreeView.SelectedNode.Text;
                (this.Owner as DialogForm).standName = this.projTreeView.SelectedNode.Parent.Text;
                (this.Owner as DialogForm).textBox3.Text = this.projTreeView.SelectedNode.Parent.Text + "\\" + this.projTreeView.SelectedNode.Text;
            }
            this.Close();
        }
        /// <summary>
        /// 音频文件夹绝对路径
        /// </summary>
        private string SoundDir = Editor.projectFolder + @"\Source";

        private void VocalForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfoVocal = new DirectoryInfo(this.SoundDir + @"\vocal");
            foreach (var dir in dirInfoVocal.GetDirectories())
            {
                this.projTreeView.Nodes.Add(dir.Name, dir.Name);
                foreach (var f in dir.GetFiles())
                {
                    foreach (TreeNode node in projTreeView.Nodes)
                    {
                        if (node.Text == dir.Name)
                        {
                            node.Nodes.Add(f.Name, f.Name);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
