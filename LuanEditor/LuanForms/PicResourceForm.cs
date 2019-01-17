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
    public partial class PicResourceForm : Form
    {
        public PicResourceForm(string title, int index)
        {
            InitializeComponent();
            // 加载文件夹
            this.dirInfoAsset = new DirectoryInfo(this.PicDir + @"\asset");
            this.dirInfoCharacter = new DirectoryInfo(this.PicDir + @"\stand");
            this.dirInfoBackground = new DirectoryInfo(this.PicDir + @"\background");
            // 选择默认项
            this.comboBox1.SelectedIndex = index;
            // 标题和模式
            this.Text = title;
            if (title != "图像资源管理器")
            {
                this.comboBox1.Enabled = false;
            }
            else
            {
                this.button1.Visible = false;
            }
        }
        /// <summary>
        /// 重新加载列表
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = this.dirInfoAsset;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    dirInfo = this.dirInfoAsset;
                    break;
                case 1:
                    dirInfo = this.dirInfoCharacter;
                    break;
                case 2:
                    dirInfo = this.dirInfoBackground;
                    break;
            }
            // 加载文件
            this.pathVect.Clear();
            this.listBox1.Items.Clear();
            if (this.comboBox1.SelectedIndex == 1)
            {
                foreach(var dir in dirInfo.GetDirectories())
                {
                    foreach(var f in dir.GetFiles())
                    {
                        this.listBox1.Items.Add(f.Name);
                        this.pathVect.Add(f.FullName);
                    }
                }
            }
            else
            {
                foreach (var f in dirInfo.GetFiles())
                {
                    this.listBox1.Items.Add(f.Name);
                    this.pathVect.Add(f.FullName);
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Image = new Bitmap(this.pathVect[this.listBox1.SelectedIndex]);
            this.isZoom = false;
            this.panel1.AutoScrollPosition = new Point(0, 0);
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.button2.Text = "合适大小";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请选择图像");
                return;
            }
            switch (this.Text)
            {
                /*
                case "选择按钮图像":
                    ((ButtonForm)this.Owner).GotFileName = this.listBox1.SelectedItem.ToString();
                    break;*/
                case "选择背景":
                    ((BgForm)this.Owner).GotFileName = this.listBox1.SelectedItem.ToString();
                    break;
                case "选择素材":
                    ((ButtonForm)this.Owner).GotFileName = this.listBox1.SelectedItem.ToString();
                    break;
                case "选择立绘":
                    ((CStandForm)this.Owner).GotFileName = this.listBox1.SelectedItem.ToString();
                    break;
            }
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.isZoom = !this.isZoom;
            this.panel1.AutoScrollPosition = new Point(0, 0);
            this.pictureBox1.Location = new Point(0, 0);
            if (this.isZoom)
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.pictureBox1.Size = this.panel1.Size;
                this.button2.Text = "实际大小";
            }
            else
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                this.button2.Text = "合适大小";
            }
        }

        /// <summary>
        /// 指示浏览器是否为缩放模式
        /// </summary>
        private bool isZoom = false;

        /// <summary>
        /// 图片文件夹绝对路径
        /// </summary>
        private string PicDir = Editor.projectFolder + @"\Source";

        /// <summary>
        /// 图片文件夹
        /// </summary>
        private DirectoryInfo dirInfoAsset;

        /// <summary>
        /// 立绘文件夹
        /// </summary>
        private DirectoryInfo dirInfoCharacter;

        /// <summary>
        /// 背景文件夹
        /// </summary>
        private DirectoryInfo dirInfoBackground;

        /// <summary>
        /// 路径信息向量
        /// </summary>
        private List<string> pathVect = new List<string>();
    }
}
