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
using Inst = LuanCore.Instructions;
using LuanConvertor;

namespace LuanEditor.LuanForms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 记录每个章节的指令 <章节名,<场景名,指令集>>
        /// </summary>
        public Dictionary<string,Section> Data;
        /// <summary>
        /// 记录是否新建或打开工程状态
        /// </summary>
        public bool isInit;
        /// <summary>
        /// 记录是否保存状态
        /// </summary>
        public bool isSave;
        /// <summary>
        /// 记录有章节数
        /// </summary>
        public int idNum = 0;
        public MainForm()
        {
            InitializeComponent();  
            Data = new Dictionary<string,Section>();
            this.资源ToolStripMenuItem.Enabled = false;
            isSave = true;
            isInit = false;
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm npf = new NewProjectForm();
            npf.ShowDialog(this);
            if (npf.InitSuccess)
            {
                this.资源ToolStripMenuItem.Enabled = this.groupBox1.Enabled = true;
                this.Text = Editor.projectName;
                this.isInit = true;
            }
        }

        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult dr = fd.ShowDialog(this);
            string path = "";
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                path = fd.SelectedPath;
                Editor.projectFolder = path;
                string[] stringitem = path.Split('\\');
                Editor.projectName = stringitem[stringitem.Length - 1];
                this.Text = Editor.projectName;
                string indexname = path + "\\Script" + "\\index";
                if (File.Exists(indexname))
                {
                    this.资源ToolStripMenuItem.Enabled = this.groupBox1.Enabled = true;
                    StreamReader sr = new StreamReader(indexname);
                    string nextline;
                    int id = 0;
                    //读取所有章节，并刷新工程树
                    while ((nextline = sr.ReadLine()) != null)
                    {
                        string llsname = path + "\\" + "Script" + "\\" + nextline + ".lls";
                        if (File.Exists(llsname))
                        {
                            this.projTreeView.Nodes.Add(nextline, nextline);
                            Section section = new Section();
                            section.Name = nextline;
                            section.Id = id;
                            LuanConvertor.SectionLoader.Load(Editor.projectFolder, section);
                            Data.Add(nextline, section);
                            id++;
                        }
                    }
                    this.idNum = id;
                    sr.Close();
                    //读取所有章节中的场景名，并刷新工程树
                    Dictionary<string, Section>.ValueCollection valueCol = Data.Values;
                    foreach (var sec in valueCol)
                    {
                        foreach (var scene in sec.Scenes)
                        {
                            foreach (TreeNode node in projTreeView.Nodes)
                            {
                                if (node.Text == sec.Name)
                                {
                                    node.Nodes.Add(scene.Name, scene.Name);
                                }
                            }
                        }
                    }
                    this.isInit = true;
                    /*Dictionary<string, Dictionary<string, List<string>>>.KeyCollection keys = Data.Keys; //所有章节名的集合
                    foreach (string sectionname in keys)
                    {
                        string llsname = path + "\\" + sectionname + ".lls";
                        if (File.Exists(llsname))
                        {
                            StreamReader sr1 = new StreamReader(llsname);
                            string current_scene = ""; //记录当前读取的场景名
                            while ((nextline = sr1.ReadLine()) != null)
                            {
                                //如果是场景名
                                if (nextline.Substring(0, 1) == "@" && nextline.Substring(1, 6) == "scene:")
                                {
                                    string scenename = nextline.Substring(7).Trim();
                                    current_scene = scenename.Trim();
                                    Data[sectionname].Add(scenename, new List<string>());
                                    foreach (TreeNode node in projTreeView.Nodes)
                                    {
                                        if (node.Text == sectionname)
                                        {
                                            node.Nodes.Add(scenename, scenename);
                                        }
                                    }
                                }
                                else
                                {
                                    Data[sectionname][current_scene].Add(nextline);
                                }
                            }
                            sr1.Close();
                        }
                        this.isInit = true;
                        this.projTreeView.SelectedNode = this.projTreeView.TopNode;
                    }*/
                }  else
                {
                    MessageBox.Show("打开工程失败");
                }
            }
        }
        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StreamWriter file = new System.IO.StreamWriter(Editor.projectFolder + @"\Script\index", false);
            Dictionary<string, Section>.ValueCollection valueCol = Data.Values;
            foreach (var sec in valueCol)
            {
                sec.Save(Editor.projectFolder);
                string line = sec.Name;
                file.WriteLine(line);
            }
            isSave = true;
            file.Close();
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!isSave)
            {
                DialogResult result = MessageBox.Show("新的内容还未保存，确认退出？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 菜单：删除
        /// </summary>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.codeListBox.SelectedIndex != -1)
            {
                if (this.codeListBox.SelectedItem.ToString().StartsWith("@"))
                {
                    MessageBox.Show("该条目不支持右键删除功能");
                    return;
                }
                string sectionname = "", scenename = "";
                int selected = this.codeListBox.SelectedIndex;
                /// <summary>
                /// 找到章节名、场景名
                /// </summary>
                if (this.projTreeView.SelectedNode.Parent != null)  //在场景下
                {
                    sectionname = this.projTreeView.SelectedNode.Parent.Text;
                    scenename = this.projTreeView.SelectedNode.Text;
                    foreach(var scene in Data[sectionname].Scenes)
                    {
                        if(scene.Name == scenename)
                        {
                            scene.Instructions.RemoveAt(selected - 1);
                            break;
                        }
                    }
                    //Data[sectionname][scenename].RemoveAt(selected - 1);
                }
                else //在章节下
                {
                    int sceneindex = -1; //记录当前场景的位置
                    sectionname = this.projTreeView.SelectedNode.Text;
                    for (int i = selected; i >= 0; i--)
                    {
                        if (this.codeListBox.Items[i].ToString().StartsWith("@scene:"))
                        {
                            scenename = this.codeListBox.Items[i].ToString().Substring(7);
                            sceneindex = i;
                            break;
                        }
                    }
                    foreach (var scene in Data[sectionname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            scene.Instructions.RemoveAt(selected - sceneindex - 1);
                            break;
                        }
                    }
                    //Data[sectionname][scenename].RemoveAt(selected - sceneindex - 1);
                }
                this.isSave = false;
                this.codeListBox.Items.RemoveAt(selected);
                this.codeListBox.SelectedIndex = selected;
               
            }
        }
        /// <summary>
        /// 代码框右键菜单
        /// </summary>
        private void codeListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.codeListBox.SelectedIndices.Clear();
                int posindex = this.codeListBox.IndexFromPoint(new Point(e.X, e.Y));
                this.codeListBox.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < this.codeListBox.Items.Count)
                {
                    this.codeListBox.SelectedIndex = posindex;
                    this.CodeListContextMenuStrip.Show(this.codeListBox, new Point(e.X, e.Y));
                } else
                {
                    if(this.codeListBox.SelectedIndex == -1)
                    {
                        this.actionGroupBox.Enabled = false;
                    }
                }
            }
            this.codeListBox.Refresh();
        }
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string str = this.codeListBox.SelectedItem.ToString();
            if (str == "◇")
                return;
            if (str.Trim().Substring(1, 4) == "显示对话")
            {
                int index = this.codeListBox.SelectedIndex;
                int pos1 = str.IndexOf(":");
                int pos2 = str.IndexOf("|");
                string str2 = "";//对话内容
                if(pos2 != -1)
                {
                    str2 = str.Substring(pos1 + 1, pos2 - pos1 - 1);
                } else
                {
                    str2 = str.Substring(pos1 + 1);
                }
                string str3 = "", str4 = ""; //赋值和配音文件名称
                int pos3 = str.IndexOf("|赋值:");
                int pos4 = str.IndexOf("|配音:");
                if (pos3 != -1)
                {
                    if(pos4 != -1)
                    {
                        str3 = str.Substring(pos3 + 4, pos4 - pos3 - 4);
                    } else
                    {
                        str3 = str.Substring(pos3 + 4);
                    }
                }
                if(pos4 != -1)
                {
                    str4 = str.Substring(pos4 + 4);
                }
                DialogForm formDialog = new DialogForm("显示对话", true, index, str2, str3, str4);
                formDialog.ShowDialog(this);
            } else if(str.Trim().Substring(1, 2) == "按钮")
            {
                
            } else if (str.Trim().Substring(1, 4) == "显示背景")
            {
                int index = this.codeListBox.SelectedIndex;
                int pos1 = str.IndexOf(":");
                int pos2 = str.IndexOf("|");
                int pos3 = str.IndexOf("|X缩放比:");
                int pos4 = str.IndexOf("|Y缩放比:");
                int pos5 = str.IndexOf("|不透明度:");
                
                string str2 = "";//背景文件名字
                string str3 = "", str4 = "";//X缩放比 Y缩放比
                string str5 = ""; //不透明度
                if (pos2 != -1)
                {
                    str2 = str.Substring(pos1 + 1, pos2 - pos1 - 1);
                }
                else
                {
                    str2 = str.Substring(pos1 + 1);
                }
                if(pos3 != -1)
                {
                    str3 = str.Substring(pos3 + 6, pos4 - pos3 - 6);
                    if(pos5 != -1)
                    {
                        str4 = str.Substring(pos4 + 6, pos5 - pos4 - 6);
                    } else
                    {
                        str4 = str.Substring(pos4 + 6);
                    }
                }
                if(pos5 != -1)
                {
                    str5 = str.Substring(pos5 + 6);
                }
                BgForm formbg = new BgForm(true, index, str2, str3, str4, str5);
                formbg.Owner = this;
                formbg.ShowDialog(this);
            } else
            {
                MessageBox.Show("该命令暂时不支持编辑功能");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            DialogForm formDialog = new DialogForm("显示对话", false, index);
            formDialog.Owner = this;
            formDialog.ShowDialog(this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            BranchForm bf = new BranchForm(index);
            bf.Owner = this;
            bf.ShowDialog(this);
        }

        private void button_AddNewScene_Click(object sender, EventArgs e)
        {
            if ((this.projTreeView.SelectedNode == null) || (this.projTreeView.SelectedNode.Parent != null))
            {
                MessageBox.Show("请在章节下添加场景！");
                return;
            }
            AddSceneForm addsceneform = new AddSceneForm();
            addsceneform.Owner = this;
            addsceneform.ShowDialog();
            this.codeListBox.Refresh();
        }

        private void button_AddNewSection_Click(object sender, EventArgs e)
        {
            AddSectionForm addsectionform = new AddSectionForm();
            addsectionform.Owner = this;
            addsectionform.ShowDialog();
        }

        private void button_DeleteSection_Click(object sender, EventArgs e)
        {
            if (projTreeView.SelectedNode == null)
            {
                MessageBox.Show("请选择要删除的章节!");
            }
            else
            {
                if (projTreeView.SelectedNode.Parent == null)
                {
                    string sectionname = projTreeView.SelectedNode.Text;
                    Data.Remove(sectionname);
                    isSave = false;
                    projTreeView.SelectedNode.Remove();
                    codeListBox.Items.Clear();
                    string deletepath = Editor.projectFolder + @"\Script\" + sectionname + ".lls";
                    if (File.Exists(deletepath))
                    {
                        File.Delete(deletepath);
                    }
                }
                else
                    MessageBox.Show("请选择要删除的章节!");
            }
        }

        private void button_DeleteScene_Click(object sender, EventArgs e)
        {
            if (projTreeView.SelectedNode.Parent != null)
            {
                string sectionname = projTreeView.SelectedNode.Parent.Text;
                string scenename = projTreeView.SelectedNode.Text;
                foreach (var scene in Data[sectionname].Scenes)
                {
                    if (scene.Name == scenename)
                    {
                        Data[sectionname].Scenes.Remove(scene);
                        break;
                    }
                }
                isSave = false;
                projTreeView.SelectedNode.Remove();
                codeListBox.Items.Clear();
            }
            else
                MessageBox.Show("请选择要删除的场景!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            ButtonForm formbutton = new ButtonForm(false,index);
            formbutton.Owner = this;
            formbutton.ShowDialog(this);
        }

        private void 背景ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicResourceForm prf = new PicResourceForm("图像资源管理器", 2);
            prf.ShowDialog(this);
        }

        private void 立绘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicResourceForm prf = new PicResourceForm("图像资源管理器", 1);
            prf.ShowDialog(this);
        }

        private void 素材ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicResourceForm prf = new PicResourceForm("图像资源管理器", 0);
            prf.ShowDialog(this);
        }

        /// <summary>
        /// 代码编辑框选择项改变事件
        /// </summary>
        private void codeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 跳过空的情况
            if (this.codeListBox.SelectedItem == null || this.codeListBox.SelectedIndex == -1)
            {
                return;
            }
            // 可插入性
            this.actionGroupBox.Enabled = this.codeListBox.Enabled && this.codeListBox.SelectedIndex != -1;
            //可选择性
            string itemStr = this.codeListBox.SelectedItem.ToString();
            string trimItem = itemStr.Trim().Split(':')[0];
            if(trimItem.Substring(1) == "scene" || !isInit)
            {
                this.actionGroupBox.Enabled = false;
                return;
            } else
            {
                this.actionGroupBox.Enabled = true;
                return;
            }
        }

        private void codeListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (e.Index > -1)
            {
                string itemFull = listBox.Items[e.Index].ToString();
                string trimItem = itemFull.Trim().Split(':')[0];
                Brush FontBrush;
                if (trimItem.Length == 0)
                {
                    FontBrush = Brushes.Black;
                }
                else
                {
                    switch (trimItem.Substring(1))
                    {
                        case "播放BGM":
                        case "播放BGS":
                            FontBrush = Brushes.Red;
                            break;
                        case "显示图片":
                        case "显示背景":
                        case "显示立绘":
                            FontBrush = Brushes.Violet;
                            break;
                        case "选择分支":
                            FontBrush = Brushes.DeepSkyBlue;
                            break;
                        case "按钮":
                            FontBrush = Brushes.Orange;
                            break;
                        case "信号":
                            FontBrush = Brushes.Green;
                            break;
                        case "同步":
                            FontBrush = Brushes.Blue;
                            break;
                        case "条件":
                        case "条件结束":
                            FontBrush = Brushes.Purple;
                            break;
                        case "scene":
                            FontBrush = Brushes.Blue;
                            break;
                        default:
                            FontBrush = Brushes.Black;
                            break;
                    }
                }
                e.DrawBackground();
                e.Graphics.DrawString(itemFull, e.Font, FontBrush, e.Bounds.Location);
                e.DrawFocusRectangle();
                this.codeListBox.HorizontalExtent = Math.Max(this.codeListBox.HorizontalExtent,
                    (int)e.Graphics.MeasureString(itemFull, e.Font).Width);
            }
        }

        /// <summary>
        /// 为ListBox的项设置正确的高度。
        /// <remarks>仅在<see cref="ListBox.DrawMode"/>=<see cref="DrawMode.OwnerDrawVariable"/>时有效。</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void codeListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            e.ItemHeight = listBox.Font.Height;
        }

        private void projTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            codeListBox.Items.Clear(); //清空区域
            if (projTreeView.SelectedNode == null)
                return;
            if (projTreeView.SelectedNode.Parent == null) //说明选择的是章节
            {
                /*string sectionname = projTreeView.SelectedNode.Text;
                Dictionary<string, List<string>>.KeyCollection scenekeys = Data[sectionname].Keys; //该章节所有场景名的集合
                foreach (string scenename in scenekeys)
                {
                    codeListBox.Items.Add("@scene:" + scenename);
                    for (int j = 0; j < Data[sectionname][scenename].Count; j++) //读取场景中的每条指令
                    {
                        codeListBox.Items.Add(Data[sectionname][scenename][j]);
                    }
                    codeListBox.Items.Add("◇");
                }
                this.codeGroupBox.Text = "章节[" + sectionname + "]" ;*/
                string sectionname = projTreeView.SelectedNode.Text;
                foreach (var scene in Data[sectionname].Scenes)
                {
                    codeListBox.Items.Add("@scene:" + scene.Name);
                    foreach(var instruc in scene.Instructions)
                    {
                        //判断不同指令的类型，从而显示不同的格式
                        string s = "";
                        switch (instruc)
                        {
                            case Inst.Text t:
                                s = s + "        ◇显示对话:" + t.Content;
                                if (t.GetBlockStr() != "{}")
                                {
                                    s = s + "|赋值:" + t.GetBlockStr();
                                }
                                if (t.Vocal != null)
                                {
                                    s = s + "|配音:" + t.Vocal.Name + "\\" + t.Vocal.Filename;
                                }
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Stand stand:
                                s = s + "        ◇显示立绘:" + "人物:" + stand.Name + "|表情:" + stand.Face + "|扩展名:" + stand.Ext + "|";
                                if (stand.ScaleX != 0.0)
                                {
                                    s = s + "X缩放比:" + ((int)(stand.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(stand.ScaleY * 100)).ToString() + "|";
                                }
                                s = s + "相对位置:";
                                string pos = "";
                                switch (stand.Pos)
                                {
                                    case Inst.StandPos.Left:
                                        pos = "左";
                                        break;
                                    case Inst.StandPos.MidLeft:
                                        pos = "偏左";
                                        break;
                                    case Inst.StandPos.Mid:
                                        pos = "中";
                                        break;
                                    case Inst.StandPos.MidRight:
                                        pos = "偏右";
                                        break;
                                    case Inst.StandPos.Right:
                                        pos = "右";
                                        break;
                                }
                                s = s + pos;
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Bg bg:
                                s = s + "        ◇显示背景:" + bg.Filename;
                                if (bg.ScaleX != 0.0)
                                {
                                    s = s + "|X缩放比:" + ((int)(bg.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(bg.ScaleY * 100)).ToString();
                                }
                                if (bg.Opacity != 0.0)
                                {
                                    s = s + "|不透明度:" + ((int)(bg.Opacity * 100)).ToString();
                                }
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Bgm bgm:
                                s = s + "        ◇播放BGM:" + bgm.Filename;
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Bgs bgs:
                                s = s + "        ◇播放BGS:" + bgs.Filename;
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Button button:
                                s = s + "        ◇按钮:" + button.Filename + "|按钮文字:" + button.Label + "|X:" + ((int)button.X).ToString() + "|Y:" + ((int)button.Y).ToString()
                                    + "|X缩放比:" + ((int)(button.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(button.ScaleY * 100)).ToString() + "|不透明度:" + ((int)(button.Opacity * 100)).ToString();
                                string sig = "";
                                if (button.Signal == null)
                                {
                                    sig = "无";
                                }
                                else
                                {
                                    switch (button.Signal.Name)
                                    {
                                        case "save":
                                            sig = "保存";
                                            break;
                                        case "load":
                                            sig = "读取";
                                            break;
                                        case "config":
                                            sig = "设置";
                                            break;
                                        case "log":
                                            sig = "回看";
                                            break;
                                        case "title":
                                            sig = "回到标题";
                                            break;
                                    }
                                }
                                s = s + "|信号:" + sig;
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Signal signal:
                                s = "        ◇信号:";
                                sig = "";
                                switch (signal.Name)
                                {
                                    case "save":
                                        sig = "保存";
                                        break;
                                    case "load":
                                        sig = "读取";
                                        break;
                                    case "config":
                                        sig = "设置";
                                        break;
                                    case "log":
                                        sig = "回看";
                                        break;
                                    case "title":
                                        sig = "回到标题";
                                        break;
                                }
                                s = s + sig;
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Branch branch:
                                s = "        ◇选择分支:";
                                if (branch.GetBlockStr() != "{}")
                                {
                                    s = s + "总赋值:" + branch.GetBlockStr();
                                }
                                foreach (var option in branch.Options)
                                {
                                    string label = option.Label;
                                    s = s + "|文本:" + label;
                                    if (option.Shift != null)
                                    {
                                        string target = option.Shift.Target;
                                        s = s + ",跳转目标:" + target;
                                    }
                                    if (option.GetBlockStr() != "{}")
                                    {
                                        s = s + ",赋值:" + option.GetBlockStr();
                                    }
                                }
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Guard guard:
                                s = "        ◇条件:" + guard.Expr.Lexeme;
                                if (guard.GetBlockStr() != "{}")
                                {
                                    s = s + "|赋值:" + guard.GetBlockStr();
                                }
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Guardend guardend:
                                s = "        ◇条件结束:";
                                if (guardend.GetBlockStr() != "{}")
                                {
                                    s = s + "赋值:" + guardend.GetBlockStr();
                                }
                                codeListBox.Items.Add(s);
                                break;
                            case Inst.Sync sync:
                                s = "        ◇同步:";
                                if (sync.GetBlockStr() != "{}")
                                {
                                    s = s + "赋值:" + sync.GetBlockStr();
                                }
                                codeListBox.Items.Add(s);
                                break;
                        }
                    }
                    codeListBox.Items.Add("◇");
                }
            }
            else //说明选择的是场景
            {
                string sectionname = projTreeView.SelectedNode.Parent.Text; //父节点为该场景属于的章节名
                string scenename = projTreeView.SelectedNode.Text;
                codeListBox.Items.Add("@scene:" + scenename);
                /*for (int i = 0; i < Data[sectionname][scenename].Count; i++)
                {
                    codeListBox.Items.Add(Data[sectionname][scenename][i]);
                }*/
                foreach(var scene in Data[sectionname].Scenes)
                {
                    if(scenename == scene.Name)
                    {
                        foreach(var instruc in scene.Instructions)
                        {
                            //判断不同指令的类型，从而显示不同的格式
                            string s = "";
                            switch(instruc)
                            {
                                case Inst.Text t:
                                    s = s + "        ◇显示对话:" + t.Content;
                                    if (t.GetBlockStr() != "{}")
                                    {
                                        s = s + "|赋值:" + t.GetBlockStr();
                                    }
                                    if(t.Vocal != null)
                                    {
                                        s = s + "|配音:" + t.Vocal.Name + "\\" + t.Vocal.Filename;
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Stand stand:
                                    s = s + "        ◇显示立绘:" + "人物:" + stand.Name + "|表情:" + stand.Face + "|扩展名:" + stand.Ext + "|";
                                    if(stand.ScaleX != 0.0)
                                    {
                                        s = s + "X缩放比:" + ((int)(stand.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(stand.ScaleY * 100)).ToString() + "|";
                                    }
                                    s = s + "相对位置:";
                                    string pos = "";
                                    switch(stand.Pos)
                                    {
                                        case Inst.StandPos.Left:
                                            pos = "左";
                                            break;
                                        case Inst.StandPos.MidLeft:
                                            pos = "偏左";
                                            break;
                                        case Inst.StandPos.Mid:
                                            pos = "中";
                                            break;
                                        case Inst.StandPos.MidRight:
                                            pos = "偏右";
                                            break;
                                        case Inst.StandPos.Right:
                                            pos = "右";
                                            break;
                                    }
                                    s = s + pos;
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Bg bg:
                                    s = s + "        ◇显示背景:" + bg.Filename;
                                    if(bg.ScaleX != 0.0)
                                    {
                                        s = s + "|X缩放比:" + ((int)(bg.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(bg.ScaleY * 100)).ToString();
                                    }
                                    if(bg.Opacity != 0.0)
                                    {
                                        s = s + "|不透明度:" + ((int)(bg.Opacity * 100)).ToString();
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Bgm bgm:
                                    s = s + "        ◇播放BGM:" + bgm.Filename;
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Bgs bgs:
                                    s = s + "        ◇播放BGS:" + bgs.Filename;
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Button button:
                                    s = s + "        ◇按钮:" + button.Filename + "|按钮文字:" + button.Label + "|X:" + ((int)button.X).ToString() + "|Y:" + ((int)button.Y).ToString()
                                        + "|X缩放比:" + ((int)(button.ScaleX * 100)).ToString() + "|Y缩放比:" + ((int)(button.ScaleY * 100)).ToString() + "|不透明度:" + ((int)(button.Opacity * 100)).ToString();
                                    string sig = "";
                                    if(button.Signal == null)
                                    {
                                        sig = "无";
                                    } else
                                    {
                                        switch(button.Signal.Name)
                                        {
                                            case "save":
                                                sig = "保存";
                                                break;
                                            case "load":
                                                sig = "读取";
                                                break;
                                            case "config":
                                                sig = "设置";
                                                break;
                                            case "log":
                                                sig = "回看";
                                                break;
                                            case "title":
                                                sig = "回到标题";
                                                break;
                                        }
                                    }
                                    s = s + "|信号:" + sig;
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Signal signal:
                                    s = "        ◇信号:";
                                    sig = "";
                                    switch (signal.Name)
                                    {
                                        case "save":
                                            sig = "保存";
                                            break;
                                        case "load":
                                            sig = "读取";
                                            break;
                                        case "config":
                                            sig = "设置";
                                            break;
                                        case "log":
                                            sig = "回看";
                                            break;
                                        case "title":
                                            sig = "回到标题";
                                            break;
                                    }
                                    s = s + sig;
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Branch branch:
                                    s = "        ◇选择分支:";
                                    if (branch.GetBlockStr() != "{}")
                                    {
                                        s = s + "总赋值:" + branch.GetBlockStr();
                                    }
                                    foreach(var option in branch.Options)
                                    {
                                        string label = option.Label;
                                        s = s + "|文本:" + label;
                                        if (option.Shift != null)
                                        {
                                            string target = option.Shift.Target;
                                            s = s + ",跳转目标:" + target;
                                        }
                                        if(option.GetBlockStr() != "{}")
                                        {
                                            s = s + ",赋值:" + option.GetBlockStr();
                                        }
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Guard guard:
                                    s = "        ◇条件:" + guard.Expr.Lexeme;
                                    if(guard.GetBlockStr() != "{}")
                                    {
                                        s = s + "|赋值:" + guard.GetBlockStr();
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Guardend guardend:
                                    s = "        ◇条件结束:";
                                    if (guardend.GetBlockStr() != "{}")
                                    {
                                        s = s + "赋值:" + guardend.GetBlockStr();
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                                case Inst.Sync sync:
                                    s = "        ◇同步:";
                                    if (sync.GetBlockStr() != "{}")
                                    {
                                        s = s + "赋值:" + sync.GetBlockStr();
                                    }
                                    codeListBox.Items.Add(s);
                                    break;
                            }
                        }
                        break;
                    }
                }
                codeListBox.Items.Add("◇");
                this.codeGroupBox.Text = "章节[" + sectionname + "]";
            }
            this.codeListBox.SelectedIndex = this.codeListBox.Items.Count - 1;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            CStandForm formstand = new CStandForm(index);
            formstand.Owner = this;
            formstand.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            BgForm formbg = new BgForm(false, index);
            formbg.Owner = this;
            formbg.ShowDialog(this);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSave)
            {
                DialogResult result = MessageBox.Show("新的内容还未保存，确认退出？", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    e.Cancel = false;
                } else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            MusicForm formmusic = new MusicForm("插入音乐",index, 0);
            formmusic.Owner = this;
            formmusic.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            this.codeListBox.Items.Insert(index, "        ◇停止播放音乐:");
            //添加停止播放的指令
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            MusicForm formmusic = new MusicForm("插入音乐", index, 1);
            formmusic.Owner = this;
            formmusic.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            this.codeListBox.Items.Insert(index, "        ◇停止播放音效:");
            //添加停止播放的指令
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            SignalForm formsignal = new SignalForm(index);
            formsignal.Owner = this;
            formsignal.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            GuardForm formguard = new GuardForm(index);
            formguard.Owner = this;
            formguard.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            GuardendForm formguardend = new GuardendForm(index);
            formguardend.Owner = this;
            formguardend.ShowDialog(this);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int index = this.codeListBox.SelectedIndex;
            SynForm formsyn = new SynForm(index);
            formsyn.Owner = this;
            formsyn.ShowDialog(this);
        }

        private void 编译工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
