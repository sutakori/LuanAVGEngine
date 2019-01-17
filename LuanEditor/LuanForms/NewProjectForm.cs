﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuanEditor.LuanForms
{
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
        }

        public bool InitSuccess { get; private set; }



        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult dr = fd.ShowDialog(this);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = fd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != String.Empty && this.textBox2.Text != String.Empty && !this.textBox2.Text.Contains("\\"))
            {
                Editor.GetInstance().NewProject(this.textBox1.Text, this.textBox2.Text);
                this.Close();
                this.InitSuccess = true;
            }
            else
            {
                MessageBox.Show("请选择文件夹并正确填写工程名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
