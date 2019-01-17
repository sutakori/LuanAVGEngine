namespace LuanEditor.LuanForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.资源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.背景ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.立绘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.projTreeView = new System.Windows.Forms.TreeView();
            this.codeListBox = new System.Windows.Forms.ListBox();
            this.button_AddNewScene = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_DeleteSection = new System.Windows.Forms.Button();
            this.button_AddNewSection = new System.Windows.Forms.Button();
            this.button_DeleteScene = new System.Windows.Forms.Button();
            this.codeGroupBox = new System.Windows.Forms.GroupBox();
            this.actionGroupBox = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.CodeListContextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.codeGroupBox.SuspendLayout();
            this.actionGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.资源ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1030, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem1,
            this.保存ToolStripMenuItem1,
            this.退出ToolStripMenuItem1});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件ToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem1
            // 
            this.打开ToolStripMenuItem1.Name = "打开ToolStripMenuItem1";
            this.打开ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem1.Text = "打开";
            this.打开ToolStripMenuItem1.Click += new System.EventHandler(this.打开ToolStripMenuItem1_Click);
            // 
            // 保存ToolStripMenuItem1
            // 
            this.保存ToolStripMenuItem1.Name = "保存ToolStripMenuItem1";
            this.保存ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.保存ToolStripMenuItem1.Text = "保存";
            this.保存ToolStripMenuItem1.Click += new System.EventHandler(this.保存ToolStripMenuItem1_Click);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem1_Click);
            // 
            // 资源ToolStripMenuItem
            // 
            this.资源ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.背景ToolStripMenuItem,
            this.立绘ToolStripMenuItem,
            this.图片ToolStripMenuItem});
            this.资源ToolStripMenuItem.Name = "资源ToolStripMenuItem";
            this.资源ToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.资源ToolStripMenuItem.Text = "资源(&S)";
            // 
            // 背景ToolStripMenuItem
            // 
            this.背景ToolStripMenuItem.Name = "背景ToolStripMenuItem";
            this.背景ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.背景ToolStripMenuItem.Text = "背景";
            this.背景ToolStripMenuItem.Click += new System.EventHandler(this.背景ToolStripMenuItem_Click);
            // 
            // 立绘ToolStripMenuItem
            // 
            this.立绘ToolStripMenuItem.Name = "立绘ToolStripMenuItem";
            this.立绘ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.立绘ToolStripMenuItem.Text = "立绘";
            this.立绘ToolStripMenuItem.Click += new System.EventHandler(this.立绘ToolStripMenuItem_Click);
            // 
            // 图片ToolStripMenuItem
            // 
            this.图片ToolStripMenuItem.Name = "图片ToolStripMenuItem";
            this.图片ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.图片ToolStripMenuItem.Text = "素材";
            this.图片ToolStripMenuItem.Click += new System.EventHandler(this.素材ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // CodeListContextMenuStrip
            // 
            this.CodeListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem5,
            this.ToolStripMenuItem4});
            this.CodeListContextMenuStrip.Name = "CodeListContextMenuStrip";
            this.CodeListContextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // ToolStripMenuItem5
            // 
            this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
            this.ToolStripMenuItem5.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem5.Text = "编辑";
            this.ToolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
            // 
            // ToolStripMenuItem4
            // 
            this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
            this.ToolStripMenuItem4.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem4.Text = "删除";
            this.ToolStripMenuItem4.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // projTreeView
            // 
            this.projTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projTreeView.Location = new System.Drawing.Point(6, 20);
            this.projTreeView.Name = "projTreeView";
            this.projTreeView.Size = new System.Drawing.Size(185, 600);
            this.projTreeView.TabIndex = 2;
            this.projTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projTreeView_NodeMouseDoubleClick);
            // 
            // codeListBox
            // 
            this.codeListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.codeListBox.Font = new System.Drawing.Font("SimHei", 10.5F);
            this.codeListBox.HorizontalScrollbar = true;
            this.codeListBox.ItemHeight = 14;
            this.codeListBox.Location = new System.Drawing.Point(17, 29);
            this.codeListBox.Name = "codeListBox";
            this.codeListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.codeListBox.Size = new System.Drawing.Size(610, 662);
            this.codeListBox.TabIndex = 0;
            this.codeListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.codeListBox_DrawItem);
            this.codeListBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.codeListBox_MeasureItem);
            this.codeListBox.SelectedIndexChanged += new System.EventHandler(this.codeListBox_SelectedIndexChanged);
            this.codeListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.codeListBox_MouseUp);
            // 
            // button_AddNewScene
            // 
            this.button_AddNewScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AddNewScene.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_AddNewScene.Location = new System.Drawing.Point(105, 626);
            this.button_AddNewScene.Name = "button_AddNewScene";
            this.button_AddNewScene.Size = new System.Drawing.Size(89, 28);
            this.button_AddNewScene.TabIndex = 34;
            this.button_AddNewScene.Text = "新建场景";
            this.button_AddNewScene.UseVisualStyleBackColor = false;
            this.button_AddNewScene.Click += new System.EventHandler(this.button_AddNewScene_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.button_DeleteSection);
            this.groupBox1.Controls.Add(this.button_AddNewSection);
            this.groupBox1.Controls.Add(this.button_DeleteScene);
            this.groupBox1.Controls.Add(this.projTreeView);
            this.groupBox1.Controls.Add(this.button_AddNewScene);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 700);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工程树";
            // 
            // button_DeleteSection
            // 
            this.button_DeleteSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DeleteSection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_DeleteSection.Location = new System.Drawing.Point(10, 660);
            this.button_DeleteSection.Name = "button_DeleteSection";
            this.button_DeleteSection.Size = new System.Drawing.Size(89, 28);
            this.button_DeleteSection.TabIndex = 37;
            this.button_DeleteSection.Text = "删除章节";
            this.button_DeleteSection.UseVisualStyleBackColor = false;
            this.button_DeleteSection.Click += new System.EventHandler(this.button_DeleteSection_Click);
            // 
            // button_AddNewSection
            // 
            this.button_AddNewSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AddNewSection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_AddNewSection.Location = new System.Drawing.Point(10, 626);
            this.button_AddNewSection.Name = "button_AddNewSection";
            this.button_AddNewSection.Size = new System.Drawing.Size(89, 28);
            this.button_AddNewSection.TabIndex = 36;
            this.button_AddNewSection.Text = "新建章节";
            this.button_AddNewSection.UseVisualStyleBackColor = false;
            this.button_AddNewSection.Click += new System.EventHandler(this.button_AddNewSection_Click);
            // 
            // button_DeleteScene
            // 
            this.button_DeleteScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DeleteScene.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_DeleteScene.Location = new System.Drawing.Point(105, 660);
            this.button_DeleteScene.Name = "button_DeleteScene";
            this.button_DeleteScene.Size = new System.Drawing.Size(89, 28);
            this.button_DeleteScene.TabIndex = 35;
            this.button_DeleteScene.Text = "删除场景";
            this.button_DeleteScene.UseVisualStyleBackColor = false;
            this.button_DeleteScene.Click += new System.EventHandler(this.button_DeleteScene_Click);
            // 
            // codeGroupBox
            // 
            this.codeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeGroupBox.Controls.Add(this.codeListBox);
            this.codeGroupBox.Location = new System.Drawing.Point(209, 3);
            this.codeGroupBox.Name = "codeGroupBox";
            this.codeGroupBox.Size = new System.Drawing.Size(633, 700);
            this.codeGroupBox.TabIndex = 6;
            this.codeGroupBox.TabStop = false;
            this.codeGroupBox.Text = "章节";
            // 
            // actionGroupBox
            // 
            this.actionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionGroupBox.Controls.Add(this.button5);
            this.actionGroupBox.Controls.Add(this.button16);
            this.actionGroupBox.Controls.Add(this.button11);
            this.actionGroupBox.Controls.Add(this.button10);
            this.actionGroupBox.Controls.Add(this.button9);
            this.actionGroupBox.Controls.Add(this.button8);
            this.actionGroupBox.Controls.Add(this.button6);
            this.actionGroupBox.Controls.Add(this.button4);
            this.actionGroupBox.Controls.Add(this.button3);
            this.actionGroupBox.Controls.Add(this.button2);
            this.actionGroupBox.Controls.Add(this.button1);
            this.actionGroupBox.Enabled = false;
            this.actionGroupBox.Location = new System.Drawing.Point(848, 3);
            this.actionGroupBox.Name = "actionGroupBox";
            this.actionGroupBox.Size = new System.Drawing.Size(179, 700);
            this.actionGroupBox.TabIndex = 0;
            this.actionGroupBox.TabStop = false;
            this.actionGroupBox.Text = "动作";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button5.Location = new System.Drawing.Point(48, 392);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 28);
            this.button5.TabIndex = 52;
            this.button5.Text = "信号";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button16.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button16.Location = new System.Drawing.Point(48, 98);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(89, 28);
            this.button16.TabIndex = 51;
            this.button16.Text = "显示立绘";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button11.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button11.Location = new System.Drawing.Point(48, 626);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(89, 28);
            this.button11.TabIndex = 46;
            this.button11.Text = "同步";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button10.Location = new System.Drawing.Point(48, 451);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(89, 28);
            this.button10.TabIndex = 45;
            this.button10.Text = "选择分支";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button9.Location = new System.Drawing.Point(48, 508);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(89, 28);
            this.button9.TabIndex = 44;
            this.button9.Text = "条件";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button8.Location = new System.Drawing.Point(48, 278);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(89, 28);
            this.button8.TabIndex = 43;
            this.button8.Text = "播放BGS";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Location = new System.Drawing.Point(48, 565);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(89, 28);
            this.button6.TabIndex = 41;
            this.button6.Text = "条件结束";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button4.Location = new System.Drawing.Point(48, 223);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 28);
            this.button4.TabIndex = 39;
            this.button4.Text = "播放BGM";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.Location = new System.Drawing.Point(48, 160);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 28);
            this.button3.TabIndex = 38;
            this.button3.Text = "显示背景";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(48, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 28);
            this.button2.TabIndex = 37;
            this.button2.Text = "按钮";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(48, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 28);
            this.button1.TabIndex = 36;
            this.button1.Text = "显示对话";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.codeGroupBox);
            this.panel1.Controls.Add(this.actionGroupBox);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1030, 705);
            this.panel1.TabIndex = 36;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1030, 733);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "LuanAVGEngine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.CodeListContextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.codeGroupBox.ResumeLayout(false);
            this.actionGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        internal System.Windows.Forms.ContextMenuStrip CodeListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
        internal System.Windows.Forms.TreeView projTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox actionGroupBox;
        private System.Windows.Forms.GroupBox codeGroupBox;
        internal System.Windows.Forms.ListBox codeListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_AddNewScene;
        private System.Windows.Forms.Button button_DeleteScene;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button_AddNewSection;
        private System.Windows.Forms.Button button_DeleteSection;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.ToolStripMenuItem 资源ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 背景ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 立绘ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.Button button5;
    }
}