namespace PathSearching1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.RunButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.MapSizeTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Erb = new System.Windows.Forms.RadioButton();
            this.SAERb = new System.Windows.Forms.RadioButton();
            this.WallRb = new System.Windows.Forms.RadioButton();
            this.MapFreshBtn = new System.Windows.Forms.Button();
            this.SearchWayBox = new System.Windows.Forms.GroupBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.Depthtxt = new System.Windows.Forms.TextBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.AStarRadioButton = new System.Windows.Forms.RadioButton();
            this.BiBFSradioButton = new System.Windows.Forms.RadioButton();
            this.BFSradioButton = new System.Windows.Forms.RadioButton();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.LoadMapBtn = new System.Windows.Forms.Button();
            this.SaveMapBtn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.ObstaleRankBox = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton14 = new System.Windows.Forms.RadioButton();
            this.radioButton13 = new System.Windows.Forms.RadioButton();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EndPNum = new System.Windows.Forms.Label();
            this.radioButton16 = new System.Windows.Forms.RadioButton();
            this.radioButton15 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ObShowBtn = new System.Windows.Forms.Button();
            this.SAAbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Timelabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SearchWayBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.ObstaleRankBox.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(658, 41);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "新游戏";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(657, 96);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "开始搜索";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MapSizeTxt
            // 
            this.MapSizeTxt.Location = new System.Drawing.Point(726, 11);
            this.MapSizeTxt.Name = "MapSizeTxt";
            this.MapSizeTxt.Size = new System.Drawing.Size(52, 21);
            this.MapSizeTxt.TabIndex = 2;
            this.MapSizeTxt.Text = "8";
            this.MapSizeTxt.TextChanged += new System.EventHandler(this.MapSizeTxt_TextChanged);
            this.MapSizeTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MapSizeTxt_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Erb);
            this.groupBox1.Controls.Add(this.SAERb);
            this.groupBox1.Controls.Add(this.WallRb);
            this.groupBox1.Location = new System.Drawing.Point(17, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 91);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置方式";
            // 
            // Erb
            // 
            this.Erb.AutoSize = true;
            this.Erb.Location = new System.Drawing.Point(7, 67);
            this.Erb.Name = "Erb";
            this.Erb.Size = new System.Drawing.Size(71, 16);
            this.Erb.TabIndex = 2;
            this.Erb.TabStop = true;
            this.Erb.Text = "设置终点";
            this.Erb.UseVisualStyleBackColor = true;
            this.Erb.CheckedChanged += new System.EventHandler(this.Erb_CheckedChanged);
            // 
            // SAERb
            // 
            this.SAERb.AutoSize = true;
            this.SAERb.Location = new System.Drawing.Point(7, 44);
            this.SAERb.Name = "SAERb";
            this.SAERb.Size = new System.Drawing.Size(71, 16);
            this.SAERb.TabIndex = 1;
            this.SAERb.Text = "设置起点";
            this.SAERb.UseVisualStyleBackColor = true;
            this.SAERb.CheckedChanged += new System.EventHandler(this.SAERb_CheckedChanged);
            // 
            // WallRb
            // 
            this.WallRb.AutoSize = true;
            this.WallRb.Checked = true;
            this.WallRb.Location = new System.Drawing.Point(7, 21);
            this.WallRb.Name = "WallRb";
            this.WallRb.Size = new System.Drawing.Size(83, 16);
            this.WallRb.TabIndex = 0;
            this.WallRb.TabStop = true;
            this.WallRb.Text = "设置障碍物";
            this.WallRb.UseVisualStyleBackColor = true;
            this.WallRb.CheckedChanged += new System.EventHandler(this.WallRb_CheckedChanged);
            // 
            // MapFreshBtn
            // 
            this.MapFreshBtn.Location = new System.Drawing.Point(815, 96);
            this.MapFreshBtn.Name = "MapFreshBtn";
            this.MapFreshBtn.Size = new System.Drawing.Size(75, 23);
            this.MapFreshBtn.TabIndex = 4;
            this.MapFreshBtn.Text = "刷新地图";
            this.MapFreshBtn.UseVisualStyleBackColor = true;
            this.MapFreshBtn.Click += new System.EventHandler(this.MapFreshBtn_Click);
            // 
            // SearchWayBox
            // 
            this.SearchWayBox.Controls.Add(this.radioButton7);
            this.SearchWayBox.Controls.Add(this.radioButton6);
            this.SearchWayBox.Controls.Add(this.Depthtxt);
            this.SearchWayBox.Controls.Add(this.radioButton5);
            this.SearchWayBox.Controls.Add(this.radioButton4);
            this.SearchWayBox.Controls.Add(this.AStarRadioButton);
            this.SearchWayBox.Controls.Add(this.BiBFSradioButton);
            this.SearchWayBox.Controls.Add(this.BFSradioButton);
            this.SearchWayBox.Location = new System.Drawing.Point(16, 15);
            this.SearchWayBox.Name = "SearchWayBox";
            this.SearchWayBox.Size = new System.Drawing.Size(200, 189);
            this.SearchWayBox.TabIndex = 5;
            this.SearchWayBox.TabStop = false;
            this.SearchWayBox.Text = "搜索方法";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(8, 158);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(71, 16);
            this.radioButton7.TabIndex = 12;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Dijkstra";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(7, 135);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(119, 16);
            this.radioButton6.TabIndex = 11;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "贪婪最佳优先搜索";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // Depthtxt
            // 
            this.Depthtxt.Location = new System.Drawing.Point(137, 111);
            this.Depthtxt.Name = "Depthtxt";
            this.Depthtxt.Size = new System.Drawing.Size(56, 21);
            this.Depthtxt.TabIndex = 10;
            this.Depthtxt.Text = "100";
            this.Depthtxt.TextChanged += new System.EventHandler(this.Depthtxt_TextChanged);
            this.Depthtxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Depthtxt_KeyPress);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(7, 112);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(95, 16);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "迭代加深搜索";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(7, 89);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(143, 16);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "A*算法（优先级队列）";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // AStarRadioButton
            // 
            this.AStarRadioButton.AutoSize = true;
            this.AStarRadioButton.Location = new System.Drawing.Point(7, 67);
            this.AStarRadioButton.Name = "AStarRadioButton";
            this.AStarRadioButton.Size = new System.Drawing.Size(59, 16);
            this.AStarRadioButton.TabIndex = 2;
            this.AStarRadioButton.TabStop = true;
            this.AStarRadioButton.Text = "A*算法";
            this.AStarRadioButton.UseVisualStyleBackColor = true;
            this.AStarRadioButton.CheckedChanged += new System.EventHandler(this.AStarRadioButton_CheckedChanged);
            // 
            // BiBFSradioButton
            // 
            this.BiBFSradioButton.AutoSize = true;
            this.BiBFSradioButton.Location = new System.Drawing.Point(7, 44);
            this.BiBFSradioButton.Name = "BiBFSradioButton";
            this.BiBFSradioButton.Size = new System.Drawing.Size(65, 16);
            this.BiBFSradioButton.TabIndex = 1;
            this.BiBFSradioButton.Text = "双向BFS";
            this.BiBFSradioButton.UseVisualStyleBackColor = true;
            this.BiBFSradioButton.CheckedChanged += new System.EventHandler(this.BiBFSradioButton_CheckedChanged);
            // 
            // BFSradioButton
            // 
            this.BFSradioButton.AutoSize = true;
            this.BFSradioButton.Checked = true;
            this.BFSradioButton.Location = new System.Drawing.Point(7, 21);
            this.BFSradioButton.Name = "BFSradioButton";
            this.BFSradioButton.Size = new System.Drawing.Size(95, 16);
            this.BFSradioButton.TabIndex = 0;
            this.BFSradioButton.TabStop = true;
            this.BFSradioButton.Text = "宽度优先搜索";
            this.BFSradioButton.UseVisualStyleBackColor = true;
            this.BFSradioButton.CheckedChanged += new System.EventHandler(this.BFSradioButton_CheckedChanged);
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.AutoScroll = true;
            this.ButtonPanel.Location = new System.Drawing.Point(12, 0);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(580, 580);
            this.ButtonPanel.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(239, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "距离类型";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 67);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "对角距离";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 44);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "曼哈顿距离";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "欧式距离";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // LoadMapBtn
            // 
            this.LoadMapBtn.Location = new System.Drawing.Point(976, 41);
            this.LoadMapBtn.Name = "LoadMapBtn";
            this.LoadMapBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadMapBtn.TabIndex = 8;
            this.LoadMapBtn.Text = "载入地图";
            this.LoadMapBtn.UseVisualStyleBackColor = true;
            this.LoadMapBtn.Click += new System.EventHandler(this.LoadMapBtn_Click);
            // 
            // SaveMapBtn
            // 
            this.SaveMapBtn.Location = new System.Drawing.Point(976, 96);
            this.SaveMapBtn.Name = "SaveMapBtn";
            this.SaveMapBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveMapBtn.TabIndex = 9;
            this.SaveMapBtn.Text = "保存地图";
            this.SaveMapBtn.UseVisualStyleBackColor = true;
            this.SaveMapBtn.Click += new System.EventHandler(this.SaveMapBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton9);
            this.groupBox4.Controls.Add(this.radioButton8);
            this.groupBox4.Location = new System.Drawing.Point(239, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 74);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "搜索邻域";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(7, 45);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(83, 16);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.Text = "八方向搜索";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Checked = true;
            this.radioButton8.Location = new System.Drawing.Point(7, 21);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(83, 16);
            this.radioButton8.TabIndex = 0;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "四方向搜索";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // ObstaleRankBox
            // 
            this.ObstaleRankBox.Controls.Add(this.button5);
            this.ObstaleRankBox.Controls.Add(this.button4);
            this.ObstaleRankBox.Controls.Add(this.button3);
            this.ObstaleRankBox.Controls.Add(this.button2);
            this.ObstaleRankBox.Controls.Add(this.button1);
            this.ObstaleRankBox.Controls.Add(this.radioButton14);
            this.ObstaleRankBox.Controls.Add(this.radioButton13);
            this.ObstaleRankBox.Controls.Add(this.radioButton12);
            this.ObstaleRankBox.Controls.Add(this.radioButton11);
            this.ObstaleRankBox.Controls.Add(this.radioButton10);
            this.ObstaleRankBox.Location = new System.Drawing.Point(239, 10);
            this.ObstaleRankBox.Name = "ObstaleRankBox";
            this.ObstaleRankBox.Size = new System.Drawing.Size(200, 145);
            this.ObstaleRankBox.TabIndex = 11;
            this.ObstaleRankBox.TabStop = false;
            this.ObstaleRankBox.Text = "障碍物等级";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Red;
            this.button5.BackgroundImage = global::PathSearching1.Properties.Resources.dengpai;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(165, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(20, 20);
            this.button5.TabIndex = 16;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.BackgroundImage = global::PathSearching1.Properties.Resources.timg;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(165, 44);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(20, 20);
            this.button4.TabIndex = 16;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.BackgroundImage = global::PathSearching1.Properties.Resources.nuanbaobao;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(165, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 15;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.BackgroundImage = global::PathSearching1.Properties.Resources.laowang;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(165, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 14;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(165, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // radioButton14
            // 
            this.radioButton14.AutoSize = true;
            this.radioButton14.Location = new System.Drawing.Point(7, 22);
            this.radioButton14.Name = "radioButton14";
            this.radioButton14.Size = new System.Drawing.Size(101, 16);
            this.radioButton14.TabIndex = 4;
            this.radioButton14.Text = "灯牌（代价2）";
            this.radioButton14.UseVisualStyleBackColor = true;
            this.radioButton14.CheckedChanged += new System.EventHandler(this.radioButton14_CheckedChanged);
            // 
            // radioButton13
            // 
            this.radioButton13.AutoSize = true;
            this.radioButton13.Location = new System.Drawing.Point(7, 44);
            this.radioButton13.Name = "radioButton13";
            this.radioButton13.Size = new System.Drawing.Size(125, 16);
            this.radioButton13.TabIndex = 3;
            this.radioButton13.Text = "新浪微博（代价3）";
            this.radioButton13.UseVisualStyleBackColor = true;
            this.radioButton13.CheckedChanged += new System.EventHandler(this.radioButton13_CheckedChanged);
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(7, 67);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(113, 16);
            this.radioButton12.TabIndex = 2;
            this.radioButton12.Text = "暖宝宝（代价4）";
            this.radioButton12.UseVisualStyleBackColor = true;
            this.radioButton12.CheckedChanged += new System.EventHandler(this.radioButton12_CheckedChanged);
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(7, 89);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(101, 16);
            this.radioButton11.TabIndex = 1;
            this.radioButton11.Text = "老王（代价5）";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Checked = true;
            this.radioButton10.Location = new System.Drawing.Point(6, 111);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(107, 16);
            this.radioButton10.TabIndex = 0;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "墙（无法逾越）";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.EndPNum);
            this.groupBox6.Controls.Add(this.radioButton16);
            this.groupBox6.Controls.Add(this.radioButton15);
            this.groupBox6.Location = new System.Drawing.Point(17, 107);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 48);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "终点个数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "终点个数";
            this.label1.Visible = false;
            // 
            // EndPNum
            // 
            this.EndPNum.AutoSize = true;
            this.EndPNum.Location = new System.Drawing.Point(155, 32);
            this.EndPNum.Name = "EndPNum";
            this.EndPNum.Size = new System.Drawing.Size(11, 12);
            this.EndPNum.TabIndex = 2;
            this.EndPNum.Text = "0";
            this.EndPNum.Visible = false;
            // 
            // radioButton16
            // 
            this.radioButton16.AutoSize = true;
            this.radioButton16.Location = new System.Drawing.Point(7, 32);
            this.radioButton16.Name = "radioButton16";
            this.radioButton16.Size = new System.Drawing.Size(59, 16);
            this.radioButton16.TabIndex = 1;
            this.radioButton16.Text = "多终点";
            this.radioButton16.UseVisualStyleBackColor = true;
            this.radioButton16.CheckedChanged += new System.EventHandler(this.radioButton16_CheckedChanged);
            // 
            // radioButton15
            // 
            this.radioButton15.AutoSize = true;
            this.radioButton15.Checked = true;
            this.radioButton15.Location = new System.Drawing.Point(7, 13);
            this.radioButton15.Name = "radioButton15";
            this.radioButton15.Size = new System.Drawing.Size(59, 16);
            this.radioButton15.TabIndex = 0;
            this.radioButton15.TabStop = true;
            this.radioButton15.Text = "单终点";
            this.radioButton15.UseVisualStyleBackColor = true;
            this.radioButton15.CheckedChanged += new System.EventHandler(this.radioButton15_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.SearchWayBox);
            this.panel1.Location = new System.Drawing.Point(641, 373);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 207);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.ObstaleRankBox);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(641, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(447, 161);
            this.panel2.TabIndex = 14;
            // 
            // ObShowBtn
            // 
            this.ObShowBtn.Location = new System.Drawing.Point(815, 41);
            this.ObShowBtn.Name = "ObShowBtn";
            this.ObShowBtn.Size = new System.Drawing.Size(75, 23);
            this.ObShowBtn.TabIndex = 15;
            this.ObShowBtn.Text = "隐藏障碍物";
            this.ObShowBtn.UseVisualStyleBackColor = true;
            this.ObShowBtn.Click += new System.EventHandler(this.ObShowBtn_Click);
            // 
            // SAAbtn
            // 
            this.SAAbtn.Enabled = false;
            this.SAAbtn.Location = new System.Drawing.Point(657, 96);
            this.SAAbtn.Name = "SAAbtn";
            this.SAAbtn.Size = new System.Drawing.Size(75, 23);
            this.SAAbtn.TabIndex = 13;
            this.SAAbtn.Text = "模拟退火";
            this.SAAbtn.UseVisualStyleBackColor = true;
            this.SAAbtn.Visible = false;
            this.SAAbtn.Click += new System.EventHandler(this.SAAbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(655, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "地图大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(893, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "算法耗时";
            // 
            // Timelabel
            // 
            this.Timelabel.AutoSize = true;
            this.Timelabel.Location = new System.Drawing.Point(959, 14);
            this.Timelabel.Name = "Timelabel";
            this.Timelabel.Size = new System.Drawing.Size(11, 12);
            this.Timelabel.TabIndex = 19;
            this.Timelabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 610);
            this.Controls.Add(this.Timelabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SAAbtn);
            this.Controls.Add(this.ObShowBtn);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SaveMapBtn);
            this.Controls.Add(this.LoadMapBtn);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.MapFreshBtn);
            this.Controls.Add(this.MapSizeTxt);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.RunButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.SearchWayBox.ResumeLayout(false);
            this.SearchWayBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ObstaleRankBox.ResumeLayout(false);
            this.ObstaleRankBox.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox MapSizeTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SAERb;
        private System.Windows.Forms.RadioButton WallRb;
        private System.Windows.Forms.RadioButton Erb;
        private System.Windows.Forms.Button MapFreshBtn;
        private System.Windows.Forms.GroupBox SearchWayBox;
        private System.Windows.Forms.RadioButton BiBFSradioButton;
        private System.Windows.Forms.RadioButton BFSradioButton;
        private System.Windows.Forms.RadioButton AStarRadioButton;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button LoadMapBtn;
        private System.Windows.Forms.Button SaveMapBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.TextBox Depthtxt;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.GroupBox ObstaleRankBox;
        private System.Windows.Forms.RadioButton radioButton14;
        private System.Windows.Forms.RadioButton radioButton13;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton16;
        private System.Windows.Forms.RadioButton radioButton15;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ObShowBtn;
        private System.Windows.Forms.Button SAAbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EndPNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Timelabel;
    }
}

