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
    public partial class StandForm : Form
    {
        public StandForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.projTreeView.SelectedNode == null)
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
                (this.Owner as CStandForm).face = this.projTreeView.SelectedNode.Text.Split('.')[0];
                (this.Owner as CStandForm).name = this.projTreeView.SelectedNode.Parent.Text;
                (this.Owner as CStandForm).ext = this.projTreeView.SelectedNode.Text.Split('.')[1];
                (this.Owner as CStandForm).GotFileName = this.projTreeView.SelectedNode.Parent.Text + "\\" + this.projTreeView.SelectedNode.Text;
            }
            this.Close();
        }
        /// <summary>
        /// 立绘文件夹绝对路径
        /// </summary>
        private string standDir = Editor.projectFolder + @"\Source";

        private void StandForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfostand = new DirectoryInfo(this.standDir + @"\stand");
            foreach (var dir in dirInfostand.GetDirectories())
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
