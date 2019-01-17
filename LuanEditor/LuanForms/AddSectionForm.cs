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
    public partial class AddSectionForm : Form
    {
        public AddSectionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("要添加的章节名不能为空");
            } else
            {
                if ((this.Owner as MainForm).projTreeView.Nodes.Find(this.textBox1.Text.Trim(), false).Count() > 0)
                {
                    MessageBox.Show("不能添加名字相同的章节！");
                } else
                {
                    (this.Owner as MainForm).projTreeView.Nodes.Add(this.textBox1.Text.Trim(), this.textBox1.Text.Trim());
                    File.Create(Editor.projectFolder + @"\Script\" + this.textBox1.Text.Trim() + ".lls").Close();
                   // (this.Owner as MainForm).Data.Add(this.textBox1.Text.Trim(), new Section(this.textBox1.Text.Trim()));
                    Section section = new Section();
                    section.Name = this.textBox1.Text.Trim();
                    section.Id = (this.Owner as MainForm).idNum;
                    (this.Owner as MainForm).Data.Add(this.textBox1.Text.Trim(), section);
                    (this.Owner as MainForm).idNum++;
                    (this.Owner as MainForm).isSave = false;
                    StreamWriter file = new System.IO.StreamWriter(Editor.projectFolder + @"\Script\index", true);
                    string line =  this.textBox1.Text.Trim();
                    file.WriteLine(line);
                    file.Close();
                    this.Close();
                }                 
            }
        }
    }
}
