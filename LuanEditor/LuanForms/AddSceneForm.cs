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
using LuanCore;

namespace LuanEditor.LuanForms
{
    public partial class AddSceneForm : Form
    {
        public AddSceneForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("要添加的场景名不能为空");
                return;
            }
            (this.Owner as MainForm).projTreeView.SelectedNode.Nodes.Add(this.textBox1.Text.Trim(), this.textBox1.Text.Trim());
            string sectionName = (this.Owner as MainForm).projTreeView.SelectedNode.Text;
            (this.Owner as MainForm).Data[sectionName].Scenes.Add(new Scene(this.textBox1.Text.Trim()));
            (this.Owner as MainForm).codeListBox.Items.Add("@scene:" + this.textBox1.Text.Trim());
            (this.Owner as MainForm).codeListBox.Items.Add("◇");
            (this.Owner as MainForm).isSave = false;
            /*StreamWriter file = new System.IO.StreamWriter(Editor.projectFolder + @"\Script\" + sectionName + ".lls", true);
            string line = "@scene:" + this.textBox1.Text.Trim();
            file.WriteLine(line);
            file.Close();*/
            this.Close();
        }
    }
}
