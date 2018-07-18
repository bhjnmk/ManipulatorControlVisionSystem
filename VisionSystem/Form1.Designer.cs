namespace VisionSystem
{
    partial class AppWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppWin));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.picBoxCameraView = new System.Windows.Forms.PictureBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.menu = new System.Windows.Forms.TabControl();
            this.glowne = new System.Windows.Forms.TabPage();
            this.pomocPictureBox = new System.Windows.Forms.PictureBox();
            this.ustawieniaPictureBox = new System.Windows.Forms.PictureBox();
            this.parametryPictureBox = new System.Windows.Forms.PictureBox();
            this.rozpoznawaniePictureBox = new System.Windows.Forms.PictureBox();
            this.rozpoznawanie = new System.Windows.Forms.TabPage();
            this.parametry = new System.Windows.Forms.TabPage();
            this.xyzMoveBtn = new System.Windows.Forms.Button();
            this.jointMoveBtn = new System.Windows.Forms.Button();
            this.servoOffBtn = new System.Windows.Forms.Button();
            this.servoOnBtn = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.ustawienia = new System.Windows.Forms.TabPage();
            this.stopSelect = new System.Windows.Forms.ComboBox();
            this.dataSelect = new System.Windows.Forms.ComboBox();
            this.baudSelect = new System.Windows.Forms.ComboBox();
            this.paritySelect = new System.Windows.Forms.ComboBox();
            this.comSelect = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.connect_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pomoc = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCameraView)).BeginInit();
            this.menu.SuspendLayout();
            this.glowne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pomocPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ustawieniaPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parametryPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozpoznawaniePictureBox)).BeginInit();
            this.rozpoznawanie.SuspendLayout();
            this.parametry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.ustawienia.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(687, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.cameraToolStripMenuItem.Text = "Camera";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.startToolStripMenuItem.Text = "IP Camera";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.IPCamera_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.stopToolStripMenuItem.Text = "Embeded Camera";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.EmbededCamera_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(778, 48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(357, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // picBoxCameraView
            // 
            this.picBoxCameraView.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxCameraView.Location = new System.Drawing.Point(37, 38);
            this.picBoxCameraView.Name = "picBoxCameraView";
            this.picBoxCameraView.Size = new System.Drawing.Size(600, 600);
            this.picBoxCameraView.TabIndex = 4;
            this.picBoxCameraView.TabStop = false;
            this.picBoxCameraView.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoxCameraView_Paint);
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(60, 159);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(75, 23);
            this.start_btn.TabIndex = 7;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(643, 606);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(117, 42);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox2.Location = new System.Drawing.Point(766, 561);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(274, 87);
            this.richTextBox2.TabIndex = 15;
            this.richTextBox2.Text = "";
            // 
            // menu
            // 
            this.menu.Controls.Add(this.glowne);
            this.menu.Controls.Add(this.rozpoznawanie);
            this.menu.Controls.Add(this.parametry);
            this.menu.Controls.Add(this.ustawienia);
            this.menu.Controls.Add(this.pomoc);
            this.menu.Location = new System.Drawing.Point(668, 181);
            this.menu.Name = "menu";
            this.menu.SelectedIndex = 0;
            this.menu.Size = new System.Drawing.Size(372, 374);
            this.menu.TabIndex = 16;
            // 
            // glowne
            // 
            this.glowne.Controls.Add(this.pomocPictureBox);
            this.glowne.Controls.Add(this.ustawieniaPictureBox);
            this.glowne.Controls.Add(this.parametryPictureBox);
            this.glowne.Controls.Add(this.rozpoznawaniePictureBox);
            this.glowne.Location = new System.Drawing.Point(4, 22);
            this.glowne.Name = "glowne";
            this.glowne.Padding = new System.Windows.Forms.Padding(3);
            this.glowne.Size = new System.Drawing.Size(364, 348);
            this.glowne.TabIndex = 0;
            this.glowne.Text = "Main Menu";
            this.glowne.UseVisualStyleBackColor = true;
            // 
            // pomocPictureBox
            // 
            this.pomocPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pomocPictureBox.Image")));
            this.pomocPictureBox.Location = new System.Drawing.Point(275, 54);
            this.pomocPictureBox.Name = "pomocPictureBox";
            this.pomocPictureBox.Size = new System.Drawing.Size(75, 75);
            this.pomocPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pomocPictureBox.TabIndex = 5;
            this.pomocPictureBox.TabStop = false;
            this.pomocPictureBox.Click += new System.EventHandler(this.pomocPictureBox_Click);
            // 
            // ustawieniaPictureBox
            // 
            this.ustawieniaPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ustawieniaPictureBox.Image")));
            this.ustawieniaPictureBox.Location = new System.Drawing.Point(189, 55);
            this.ustawieniaPictureBox.Name = "ustawieniaPictureBox";
            this.ustawieniaPictureBox.Size = new System.Drawing.Size(75, 75);
            this.ustawieniaPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ustawieniaPictureBox.TabIndex = 2;
            this.ustawieniaPictureBox.TabStop = false;
            this.ustawieniaPictureBox.Click += new System.EventHandler(this.ustawieniaPictureBox_Click);
            // 
            // parametryPictureBox
            // 
            this.parametryPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("parametryPictureBox.Image")));
            this.parametryPictureBox.Location = new System.Drawing.Point(102, 54);
            this.parametryPictureBox.Name = "parametryPictureBox";
            this.parametryPictureBox.Size = new System.Drawing.Size(75, 75);
            this.parametryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.parametryPictureBox.TabIndex = 1;
            this.parametryPictureBox.TabStop = false;
            this.parametryPictureBox.Click += new System.EventHandler(this.parametryPictureBox_Click);
            // 
            // rozpoznawaniePictureBox
            // 
            this.rozpoznawaniePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rozpoznawaniePictureBox.Image")));
            this.rozpoznawaniePictureBox.Location = new System.Drawing.Point(15, 54);
            this.rozpoznawaniePictureBox.Name = "rozpoznawaniePictureBox";
            this.rozpoznawaniePictureBox.Size = new System.Drawing.Size(75, 75);
            this.rozpoznawaniePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rozpoznawaniePictureBox.TabIndex = 0;
            this.rozpoznawaniePictureBox.TabStop = false;
            this.rozpoznawaniePictureBox.Click += new System.EventHandler(this.rozpoznawaniePictureBox_Click);
            // 
            // rozpoznawanie
            // 
            this.rozpoznawanie.Controls.Add(this.start_btn);
            this.rozpoznawanie.Controls.Add(this.button1);
            this.rozpoznawanie.Location = new System.Drawing.Point(4, 22);
            this.rozpoznawanie.Name = "rozpoznawanie";
            this.rozpoznawanie.Size = new System.Drawing.Size(364, 348);
            this.rozpoznawanie.TabIndex = 3;
            this.rozpoznawanie.Text = "Rozpoznawanie";
            this.rozpoznawanie.UseVisualStyleBackColor = true;
            // 
            // parametry
            // 
            this.parametry.Controls.Add(this.xyzMoveBtn);
            this.parametry.Controls.Add(this.jointMoveBtn);
            this.parametry.Controls.Add(this.servoOffBtn);
            this.parametry.Controls.Add(this.servoOnBtn);
            this.parametry.Controls.Add(this.trackBar1);
            this.parametry.Controls.Add(this.label7);
            this.parametry.Location = new System.Drawing.Point(4, 22);
            this.parametry.Name = "parametry";
            this.parametry.Padding = new System.Windows.Forms.Padding(3);
            this.parametry.Size = new System.Drawing.Size(364, 348);
            this.parametry.TabIndex = 1;
            this.parametry.Text = "Paramtery";
            this.parametry.UseVisualStyleBackColor = true;
            // 
            // xyzMoveBtn
            // 
            this.xyzMoveBtn.Location = new System.Drawing.Point(222, 21);
            this.xyzMoveBtn.Name = "xyzMoveBtn";
            this.xyzMoveBtn.Size = new System.Drawing.Size(75, 23);
            this.xyzMoveBtn.TabIndex = 4;
            this.xyzMoveBtn.Text = "XYZ";
            this.xyzMoveBtn.UseVisualStyleBackColor = true;
            this.xyzMoveBtn.Click += new System.EventHandler(this.xyzMoveBtn_Click);
            // 
            // jointMoveBtn
            // 
            this.jointMoveBtn.Location = new System.Drawing.Point(63, 21);
            this.jointMoveBtn.Name = "jointMoveBtn";
            this.jointMoveBtn.Size = new System.Drawing.Size(75, 23);
            this.jointMoveBtn.TabIndex = 4;
            this.jointMoveBtn.Text = "JOINT";
            this.jointMoveBtn.UseVisualStyleBackColor = true;
            this.jointMoveBtn.Click += new System.EventHandler(this.jointMoveBtn_Click);
            // 
            // servoOffBtn
            // 
            this.servoOffBtn.Location = new System.Drawing.Point(222, 62);
            this.servoOffBtn.Name = "servoOffBtn";
            this.servoOffBtn.Size = new System.Drawing.Size(75, 23);
            this.servoOffBtn.TabIndex = 3;
            this.servoOffBtn.Text = "servo OFF";
            this.servoOffBtn.UseVisualStyleBackColor = true;
            this.servoOffBtn.Click += new System.EventHandler(this.servoOffBtn_Click);
            // 
            // servoOnBtn
            // 
            this.servoOnBtn.Location = new System.Drawing.Point(63, 62);
            this.servoOnBtn.Name = "servoOnBtn";
            this.servoOnBtn.Size = new System.Drawing.Size(75, 23);
            this.servoOnBtn.TabIndex = 3;
            this.servoOnBtn.Text = "servo ON";
            this.servoOnBtn.UseVisualStyleBackColor = true;
            this.servoOnBtn.Click += new System.EventHandler(this.servoOnBtn_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(107, 109);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(190, 45);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Prędkość";
            // 
            // ustawienia
            // 
            this.ustawienia.Controls.Add(this.stopSelect);
            this.ustawienia.Controls.Add(this.dataSelect);
            this.ustawienia.Controls.Add(this.baudSelect);
            this.ustawienia.Controls.Add(this.paritySelect);
            this.ustawienia.Controls.Add(this.comSelect);
            this.ustawienia.Controls.Add(this.label6);
            this.ustawienia.Controls.Add(this.label5);
            this.ustawienia.Controls.Add(this.connect_btn);
            this.ustawienia.Controls.Add(this.label4);
            this.ustawienia.Controls.Add(this.label3);
            this.ustawienia.Controls.Add(this.label1);
            this.ustawienia.Location = new System.Drawing.Point(4, 22);
            this.ustawienia.Name = "ustawienia";
            this.ustawienia.Padding = new System.Windows.Forms.Padding(3);
            this.ustawienia.Size = new System.Drawing.Size(364, 348);
            this.ustawienia.TabIndex = 2;
            this.ustawienia.Text = "Ustawienia";
            this.ustawienia.UseVisualStyleBackColor = true;
            // 
            // stopSelect
            // 
            this.stopSelect.FormattingEnabled = true;
            this.stopSelect.Items.AddRange(new object[] {
            "One",
            "Two"});
            this.stopSelect.Location = new System.Drawing.Point(129, 83);
            this.stopSelect.Name = "stopSelect";
            this.stopSelect.Size = new System.Drawing.Size(97, 21);
            this.stopSelect.TabIndex = 14;
            // 
            // dataSelect
            // 
            this.dataSelect.FormattingEnabled = true;
            this.dataSelect.Items.AddRange(new object[] {
            "2",
            "4",
            "8"});
            this.dataSelect.Location = new System.Drawing.Point(15, 83);
            this.dataSelect.Name = "dataSelect";
            this.dataSelect.Size = new System.Drawing.Size(97, 21);
            this.dataSelect.TabIndex = 13;
            // 
            // baudSelect
            // 
            this.baudSelect.FormattingEnabled = true;
            this.baudSelect.Items.AddRange(new object[] {
            "9600"});
            this.baudSelect.Location = new System.Drawing.Point(245, 33);
            this.baudSelect.Name = "baudSelect";
            this.baudSelect.Size = new System.Drawing.Size(97, 21);
            this.baudSelect.TabIndex = 12;
            // 
            // paritySelect
            // 
            this.paritySelect.FormattingEnabled = true;
            this.paritySelect.Items.AddRange(new object[] {
            "Even"});
            this.paritySelect.Location = new System.Drawing.Point(129, 33);
            this.paritySelect.Name = "paritySelect";
            this.paritySelect.Size = new System.Drawing.Size(97, 21);
            this.paritySelect.TabIndex = 11;
            // 
            // comSelect
            // 
            this.comSelect.FormattingEnabled = true;
            this.comSelect.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5"});
            this.comSelect.Location = new System.Drawing.Point(15, 33);
            this.comSelect.Name = "comSelect";
            this.comSelect.Size = new System.Drawing.Size(97, 21);
            this.comSelect.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Stop bits";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data bits";
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(94, 189);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(150, 23);
            this.connect_btn.TabIndex = 7;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Baud Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Parity";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Com";
            // 
            // pomoc
            // 
            this.pomoc.Location = new System.Drawing.Point(4, 22);
            this.pomoc.Name = "pomoc";
            this.pomoc.Size = new System.Drawing.Size(364, 348);
            this.pomoc.TabIndex = 4;
            this.pomoc.Text = "Pomoc";
            this.pomoc.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // AppWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.picBoxCameraView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "AppWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Window";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCameraView)).EndInit();
            this.menu.ResumeLayout(false);
            this.glowne.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pomocPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ustawieniaPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parametryPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rozpoznawaniePictureBox)).EndInit();
            this.rozpoznawanie.ResumeLayout(false);
            this.parametry.ResumeLayout(false);
            this.parametry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ustawienia.ResumeLayout(false);
            this.ustawienia.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox picBoxCameraView;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabControl menu;
        private System.Windows.Forms.TabPage glowne;
        private System.Windows.Forms.PictureBox pomocPictureBox;
        private System.Windows.Forms.PictureBox ustawieniaPictureBox;
        private System.Windows.Forms.PictureBox parametryPictureBox;
        private System.Windows.Forms.PictureBox rozpoznawaniePictureBox;
        private System.Windows.Forms.TabPage rozpoznawanie;
        private System.Windows.Forms.TabPage parametry;
        private System.Windows.Forms.TabPage ustawienia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage pomoc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox stopSelect;
        private System.Windows.Forms.ComboBox dataSelect;
        private System.Windows.Forms.ComboBox baudSelect;
        private System.Windows.Forms.ComboBox paritySelect;
        private System.Windows.Forms.ComboBox comSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button servoOffBtn;
        private System.Windows.Forms.Button servoOnBtn;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button xyzMoveBtn;
        private System.Windows.Forms.Button jointMoveBtn;
    }
}

