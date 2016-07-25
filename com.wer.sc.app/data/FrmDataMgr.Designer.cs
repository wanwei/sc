namespace com.wer.sc.app.data
{
    partial class FrmDataMgr
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
            this.menuItemDataCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUpdateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCheckData = new System.Windows.Forms.ToolStripMenuItem();
            this.浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbDataPath = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.listBoxCodes = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemUpdateByCodes = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btUpdateCode = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lbTickEnd = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTickStart = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbDayEnd = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbDayStart = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbHourEnd = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbHourStart = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lb15MinuteEnd = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lb15MinuteStart = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbMinuteEnd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbMinuteStart = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDataCenter,
            this.更新ToolStripMenuItem,
            this.MenuItemCheckData,
            this.浏览ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(633, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemDataCenter
            // 
            this.menuItemDataCenter.Name = "menuItemDataCenter";
            this.menuItemDataCenter.Size = new System.Drawing.Size(68, 21);
            this.menuItemDataCenter.Text = "数据中心";
            // 
            // 更新ToolStripMenuItem
            // 
            this.更新ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUpdateAll});
            this.更新ToolStripMenuItem.Name = "更新ToolStripMenuItem";
            this.更新ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.更新ToolStripMenuItem.Text = "更新";
            // 
            // menuItemUpdateAll
            // 
            this.menuItemUpdateAll.Name = "menuItemUpdateAll";
            this.menuItemUpdateAll.Size = new System.Drawing.Size(124, 22);
            this.menuItemUpdateAll.Text = "全部更新";
            // 
            // MenuItemCheckData
            // 
            this.MenuItemCheckData.Name = "MenuItemCheckData";
            this.MenuItemCheckData.Size = new System.Drawing.Size(44, 21);
            this.MenuItemCheckData.Text = "查看";
            this.MenuItemCheckData.Click += new System.EventHandler(this.MenuItemCheckData_Click);
            // 
            // 浏览ToolStripMenuItem
            // 
            this.浏览ToolStripMenuItem.Name = "浏览ToolStripMenuItem";
            this.浏览ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.浏览ToolStripMenuItem.Text = "浏览";
            // 
            // lbDataPath
            // 
            this.lbDataPath.AutoSize = true;
            this.lbDataPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbDataPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDataPath.Location = new System.Drawing.Point(0, 25);
            this.lbDataPath.Name = "lbDataPath";
            this.lbDataPath.Size = new System.Drawing.Size(88, 16);
            this.lbDataPath.TabIndex = 3;
            this.lbDataPath.Text = "lbDataPath";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(633, 345);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.listBoxCodes);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(174, 343);
            this.splitContainer3.SplitterDistance = 252;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // listBoxCodes
            // 
            this.listBoxCodes.ContextMenuStrip = this.contextMenuStrip1;
            this.listBoxCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCodes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxCodes.FormattingEnabled = true;
            this.listBoxCodes.ItemHeight = 16;
            this.listBoxCodes.Location = new System.Drawing.Point(0, 0);
            this.listBoxCodes.Name = "listBoxCodes";
            this.listBoxCodes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCodes.Size = new System.Drawing.Size(174, 252);
            this.listBoxCodes.TabIndex = 4;
            this.listBoxCodes.SelectedIndexChanged += new System.EventHandler(this.listBoxCodes_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUpdateByCodes});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // menuItemUpdateByCodes
            // 
            this.menuItemUpdateByCodes.Name = "menuItemUpdateByCodes";
            this.menuItemUpdateByCodes.Size = new System.Drawing.Size(100, 22);
            this.menuItemUpdateByCodes.Text = "更新";
            this.menuItemUpdateByCodes.Click += new System.EventHandler(this.menuItemUpdateByCodes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "现有品种";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btUpdateCode);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listBox2);
            this.splitContainer2.Size = new System.Drawing.Size(174, 90);
            this.splitContainer2.SplitterDistance = 30;
            this.splitContainer2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "待更新品种";
            // 
            // btUpdateCode
            // 
            this.btUpdateCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btUpdateCode.Location = new System.Drawing.Point(108, 0);
            this.btUpdateCode.Name = "btUpdateCode";
            this.btUpdateCode.Size = new System.Drawing.Size(66, 30);
            this.btUpdateCode.TabIndex = 6;
            this.btUpdateCode.Text = "更新";
            this.btUpdateCode.UseVisualStyleBackColor = true;
            this.btUpdateCode.Click += new System.EventHandler(this.btUpdateCode_Click);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(174, 56);
            this.listBox2.TabIndex = 8;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lbTickEnd);
            this.splitContainer4.Panel1.Controls.Add(this.label7);
            this.splitContainer4.Panel1.Controls.Add(this.lbTickStart);
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            this.splitContainer4.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label26);
            this.splitContainer4.Panel2.Controls.Add(this.label25);
            this.splitContainer4.Panel2.Controls.Add(this.label24);
            this.splitContainer4.Panel2.Controls.Add(this.label23);
            this.splitContainer4.Panel2.Controls.Add(this.lbDayEnd);
            this.splitContainer4.Panel2.Controls.Add(this.label20);
            this.splitContainer4.Panel2.Controls.Add(this.lbDayStart);
            this.splitContainer4.Panel2.Controls.Add(this.label22);
            this.splitContainer4.Panel2.Controls.Add(this.lbHourEnd);
            this.splitContainer4.Panel2.Controls.Add(this.label16);
            this.splitContainer4.Panel2.Controls.Add(this.lbHourStart);
            this.splitContainer4.Panel2.Controls.Add(this.label18);
            this.splitContainer4.Panel2.Controls.Add(this.lb15MinuteEnd);
            this.splitContainer4.Panel2.Controls.Add(this.label12);
            this.splitContainer4.Panel2.Controls.Add(this.lb15MinuteStart);
            this.splitContainer4.Panel2.Controls.Add(this.label14);
            this.splitContainer4.Panel2.Controls.Add(this.lbMinuteEnd);
            this.splitContainer4.Panel2.Controls.Add(this.label8);
            this.splitContainer4.Panel2.Controls.Add(this.lbMinuteStart);
            this.splitContainer4.Panel2.Controls.Add(this.label10);
            this.splitContainer4.Panel2.Controls.Add(this.label5);
            this.splitContainer4.Size = new System.Drawing.Size(454, 343);
            this.splitContainer4.SplitterDistance = 86;
            this.splitContainer4.TabIndex = 5;
            // 
            // lbTickEnd
            // 
            this.lbTickEnd.AutoSize = true;
            this.lbTickEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTickEnd.Location = new System.Drawing.Point(355, 40);
            this.lbTickEnd.Name = "lbTickEnd";
            this.lbTickEnd.Size = new System.Drawing.Size(80, 16);
            this.lbTickEnd.TabIndex = 9;
            this.lbTickEnd.Text = "lbTickEnd";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(281, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "结束时间";
            // 
            // lbTickStart
            // 
            this.lbTickStart.AutoSize = true;
            this.lbTickStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTickStart.Location = new System.Drawing.Point(179, 40);
            this.lbTickStart.Name = "lbTickStart";
            this.lbTickStart.Size = new System.Drawing.Size(96, 16);
            this.lbTickStart.TabIndex = 7;
            this.lbTickStart.Text = "lbTickStart";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(109, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "起始时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "TICK数据";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(28, 168);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(25, 16);
            this.label26.TabIndex = 29;
            this.label26.Text = "日";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(28, 130);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(51, 16);
            this.label25.TabIndex = 28;
            this.label25.Text = "1小时";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(28, 82);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(60, 16);
            this.label24.TabIndex = 27;
            this.label24.Text = "15分钟";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(28, 39);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 16);
            this.label23.TabIndex = 26;
            this.label23.Text = "1分钟";
            // 
            // lbDayEnd
            // 
            this.lbDayEnd.AutoSize = true;
            this.lbDayEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDayEnd.Location = new System.Drawing.Point(355, 168);
            this.lbDayEnd.Name = "lbDayEnd";
            this.lbDayEnd.Size = new System.Drawing.Size(64, 16);
            this.lbDayEnd.TabIndex = 25;
            this.lbDayEnd.Text = "label19";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(281, 168);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 16);
            this.label20.TabIndex = 24;
            this.label20.Text = "结束时间";
            // 
            // lbDayStart
            // 
            this.lbDayStart.AutoSize = true;
            this.lbDayStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDayStart.Location = new System.Drawing.Point(179, 168);
            this.lbDayStart.Name = "lbDayStart";
            this.lbDayStart.Size = new System.Drawing.Size(64, 16);
            this.lbDayStart.TabIndex = 23;
            this.lbDayStart.Text = "label21";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(109, 168);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(76, 16);
            this.label22.TabIndex = 22;
            this.label22.Text = "起始时间";
            // 
            // lbHourEnd
            // 
            this.lbHourEnd.AutoSize = true;
            this.lbHourEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHourEnd.Location = new System.Drawing.Point(355, 125);
            this.lbHourEnd.Name = "lbHourEnd";
            this.lbHourEnd.Size = new System.Drawing.Size(64, 16);
            this.lbHourEnd.TabIndex = 21;
            this.lbHourEnd.Text = "label15";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(281, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 16);
            this.label16.TabIndex = 20;
            this.label16.Text = "结束时间";
            // 
            // lbHourStart
            // 
            this.lbHourStart.AutoSize = true;
            this.lbHourStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHourStart.Location = new System.Drawing.Point(179, 125);
            this.lbHourStart.Name = "lbHourStart";
            this.lbHourStart.Size = new System.Drawing.Size(64, 16);
            this.lbHourStart.TabIndex = 19;
            this.lbHourStart.Text = "label17";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(109, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 16);
            this.label18.TabIndex = 18;
            this.label18.Text = "起始时间";
            // 
            // lb15MinuteEnd
            // 
            this.lb15MinuteEnd.AutoSize = true;
            this.lb15MinuteEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb15MinuteEnd.Location = new System.Drawing.Point(355, 82);
            this.lb15MinuteEnd.Name = "lb15MinuteEnd";
            this.lb15MinuteEnd.Size = new System.Drawing.Size(64, 16);
            this.lb15MinuteEnd.TabIndex = 17;
            this.lb15MinuteEnd.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(281, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 16);
            this.label12.TabIndex = 16;
            this.label12.Text = "结束时间";
            // 
            // lb15MinuteStart
            // 
            this.lb15MinuteStart.AutoSize = true;
            this.lb15MinuteStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb15MinuteStart.Location = new System.Drawing.Point(179, 82);
            this.lb15MinuteStart.Name = "lb15MinuteStart";
            this.lb15MinuteStart.Size = new System.Drawing.Size(64, 16);
            this.lb15MinuteStart.TabIndex = 15;
            this.lb15MinuteStart.Text = "label13";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(109, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 16);
            this.label14.TabIndex = 14;
            this.label14.Text = "起始时间";
            // 
            // lbMinuteEnd
            // 
            this.lbMinuteEnd.AutoSize = true;
            this.lbMinuteEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMinuteEnd.Location = new System.Drawing.Point(355, 39);
            this.lbMinuteEnd.Name = "lbMinuteEnd";
            this.lbMinuteEnd.Size = new System.Drawing.Size(56, 16);
            this.lbMinuteEnd.TabIndex = 13;
            this.lbMinuteEnd.Text = "label6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(281, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "结束时间";
            // 
            // lbMinuteStart
            // 
            this.lbMinuteStart.AutoSize = true;
            this.lbMinuteStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMinuteStart.Location = new System.Drawing.Point(179, 39);
            this.lbMinuteStart.Name = "lbMinuteStart";
            this.lbMinuteStart.Size = new System.Drawing.Size(56, 16);
            this.lbMinuteStart.TabIndex = 11;
            this.lbMinuteStart.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(109, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "起始时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "K线数据";
            // 
            // FrmDataMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 386);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbDataPath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmDataMgr";
            this.Text = "数据管理";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataCenter;
        private System.Windows.Forms.ToolStripMenuItem 更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpdateAll;
        private System.Windows.Forms.Label lbDataPath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox listBoxCodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btUpdateCode;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lbTickEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbTickStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpdateByCodes;
        private System.Windows.Forms.Label lbDayEnd;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbDayStart;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbHourEnd;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbHourStart;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lb15MinuteEnd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lb15MinuteStart;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbMinuteEnd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbMinuteStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCheckData;
        private System.Windows.Forms.ToolStripMenuItem 浏览ToolStripMenuItem;
    }
}