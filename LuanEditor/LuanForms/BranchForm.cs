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
    public partial class BranchForm : Form
    {
        /// <summary>
        /// 记录编辑的条目在CodeListBox中的位置
        /// </summary>
        private int index;
        /// <summary>
        /// 记录是否赋值
        /// </summary>
        private bool check1 = false;
        public BranchForm(int index)
        {
            InitializeComponent();
            this.index = index;
        }
        /// <summary>
        /// 按钮：确定
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
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
            Inst.Branch branch = new Inst.Branch();
            branch.Options = new List<Inst.Option>();
            int nrows = this.switchDataGridView.Rows.Count - 1;
            string s = "        ◇选择分支:";
            if(this.textBox1.Text.Trim() != string.Empty)
            {
                s = s + string.Format("总赋值:{0}", (this.textBox1.Text.Trim()).Replace("\n", ""));
            }
            for (int i = 0; i < nrows; i++)
            {
                // 文字不能为空
                if (this.switchDataGridView.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("文本不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string branchName = (this.switchDataGridView.Rows[i].Cells[0].Value.ToString()).Replace("\n", "");                // 标签不能为空
                string target = "";
                if(this.switchDataGridView.Rows[i].Cells[1].Value != null)
                {
                    target = (this.switchDataGridView.Rows[i].Cells[1].Value.ToString()).Replace("\n", "");
                }
                Inst.Option option = new Inst.Option();
                option.Label = branchName;
                if (target.Trim() != string.Empty)
                {
                    bool isFind = false; //判断是否找到跳转目标 
                    Dictionary<string, Section>.ValueCollection valueCol = (this.Owner as MainForm).Data.Values;
                    foreach (var sec in valueCol)
                    {
                        if (target == sec.Name)
                        {
                            option.Shift = new Inst.Shift();
                            option.Shift.Typ = Inst.ShiftTyp.section;
                            option.Shift.Target = target;
                            isFind = true;
                        }
                    }
                    if (!isFind) //在章节名中没找到对应的跳转目标，继续在当前章节下寻找跳转目标是否为场景名
                    {
                        foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
                        {
                            if (target == scene.Name)
                            {
                                option.Shift = new Inst.Shift();
                                option.Shift.Typ = Inst.ShiftTyp.scene;
                                option.Shift.Target = target;
                                isFind = true;
                            }
                        }
                    }
                    if (!isFind)//没找到对应的目标
                    {
                        MessageBox.Show(String.Format("第{0}行选项未找到该跳转目标!", (i + 1).ToString()));
                        return;
                    }
                } else
                {
                    option.Shift = null;
                }
                string blockStr = "";
                if(this.switchDataGridView.Rows[i].Cells[2].Value != null)
                {
                    blockStr = (this.switchDataGridView.Rows[i].Cells[2].Value.ToString()).Replace("\n", "");
                }
                option.SetBlock(blockStr.Trim());
                s = s + string.Format("|文本:{0}", branchName);
                if (target.Trim() != string.Empty)
                {
                    s = s + string.Format(",跳转目标:{0}", target.Trim());
                }
                if(blockStr.Trim() != string.Empty)
                {
                    s = s + string.Format(",赋值:{0}", blockStr.Trim());
                }
                branch.Options.Add(option);
            }
            (this.Owner as MainForm).codeListBox.Items.Insert(index, s);
            branch.SetBlock((this.textBox1.Text.Trim()).Replace("\n", ""));
            foreach (var scene in (this.Owner as MainForm).Data[sectionname].Scenes)
            {
                if (scene.Name == scenename)
                {
                    scene.Instructions.Insert(index - sceneindex - 1, branch);
                }
            }
            (this.Owner as MainForm).isSave = false;
            this.Close();
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (check1)
            {
                check1 = false;
                this.checkBox1.Checked = false;
                this.textBox1.Enabled = false;
            }
            else
            {
                check1 = true;
                this.checkBox1.Checked = true;
                this.textBox1.Enabled = true;
            }
        }
    }
}
