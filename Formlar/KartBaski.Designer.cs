namespace Shool_Photo.Formlar
{
    partial class KartBaski
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KartBaski));
            this.cmbYapilacaklar = new System.Windows.Forms.ComboBox();
            this.trc_Yatay = new System.Windows.Forms.TrackBar();
            this.trc_Yatay_Has = new System.Windows.Forms.TrackBar();
            this.trc_Dikey_Has = new System.Windows.Forms.TrackBar();
            this.trc_Dikey = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pct_Ayar = new System.Windows.Forms.PictureBox();
            this.pnl_Ust = new System.Windows.Forms.Panel();
            this.rd_Sinif = new System.Windows.Forms.RadioButton();
            this.rd_Soyad = new System.Windows.Forms.RadioButton();
            this.rd_Ad = new System.Windows.Forms.RadioButton();
            this.rd_No = new System.Windows.Forms.RadioButton();
            this.rd_Vesikalik = new System.Windows.Forms.RadioButton();
            this.chc_Yatay = new System.Windows.Forms.CheckBox();
            this.pnl_Yan = new System.Windows.Forms.Panel();
            this.chc_Dikey = new System.Windows.Forms.CheckBox();
            this.Btn_Font = new System.Windows.Forms.Button();
            this.Btn_Renk = new System.Windows.Forms.Button();
            this.Btn_Hazir = new System.Windows.Forms.Button();
            this.Btn_Yadir = new System.Windows.Forms.Button();
            this.Btn_Kaydet = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Onizle = new System.Windows.Forms.PrintPreviewDialog();
            this.KartCikti = new System.Drawing.Printing.PrintDocument();
            this.Yazdir = new System.Windows.Forms.PrintDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYapilanlar = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTamamlama = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Yatay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Yatay_Has)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Dikey_Has)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Dikey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pct_Ayar)).BeginInit();
            this.pnl_Ust.SuspendLayout();
            this.pnl_Yan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbYapilacaklar
            // 
            this.cmbYapilacaklar.BackColor = System.Drawing.Color.DimGray;
            this.cmbYapilacaklar.Enabled = false;
            this.cmbYapilacaklar.ForeColor = System.Drawing.Color.White;
            this.cmbYapilacaklar.FormattingEnabled = true;
            this.cmbYapilacaklar.Items.AddRange(new object[] {
            "1 A",
            "1 B"});
            this.cmbYapilacaklar.Location = new System.Drawing.Point(670, 28);
            this.cmbYapilacaklar.Name = "cmbYapilacaklar";
            this.cmbYapilacaklar.Size = new System.Drawing.Size(61, 21);
            this.cmbYapilacaklar.TabIndex = 0;
            this.cmbYapilacaklar.SelectedIndexChanged += new System.EventHandler(this.cmbYapilacaklar_SelectedIndexChanged);
            // 
            // trc_Yatay
            // 
            this.trc_Yatay.BackColor = System.Drawing.Color.DimGray;
            this.trc_Yatay.Location = new System.Drawing.Point(18, 31);
            this.trc_Yatay.Name = "trc_Yatay";
            this.trc_Yatay.Size = new System.Drawing.Size(627, 45);
            this.trc_Yatay.TabIndex = 1;
            this.trc_Yatay.Value = 5;
            this.trc_Yatay.Scroll += new System.EventHandler(this.trc_Yatay_Scroll);
            // 
            // trc_Yatay_Has
            // 
            this.trc_Yatay_Has.BackColor = System.Drawing.Color.DimGray;
            this.trc_Yatay_Has.Enabled = false;
            this.trc_Yatay_Has.Location = new System.Drawing.Point(18, 82);
            this.trc_Yatay_Has.Maximum = 100;
            this.trc_Yatay_Has.Name = "trc_Yatay_Has";
            this.trc_Yatay_Has.Size = new System.Drawing.Size(627, 45);
            this.trc_Yatay_Has.TabIndex = 2;
            this.trc_Yatay_Has.Value = 50;
            this.trc_Yatay_Has.Scroll += new System.EventHandler(this.trc_Yatay_Has_Scroll);
            // 
            // trc_Dikey_Has
            // 
            this.trc_Dikey_Has.BackColor = System.Drawing.Color.DimGray;
            this.trc_Dikey_Has.Enabled = false;
            this.trc_Dikey_Has.Location = new System.Drawing.Point(20, 26);
            this.trc_Dikey_Has.Maximum = 100;
            this.trc_Dikey_Has.Name = "trc_Dikey_Has";
            this.trc_Dikey_Has.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trc_Dikey_Has.Size = new System.Drawing.Size(45, 392);
            this.trc_Dikey_Has.TabIndex = 4;
            this.trc_Dikey_Has.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trc_Dikey_Has.Value = 50;
            this.trc_Dikey_Has.Scroll += new System.EventHandler(this.trc_Dikey_Has_Scroll);
            // 
            // trc_Dikey
            // 
            this.trc_Dikey.BackColor = System.Drawing.Color.DimGray;
            this.trc_Dikey.Location = new System.Drawing.Point(98, 26);
            this.trc_Dikey.Name = "trc_Dikey";
            this.trc_Dikey.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trc_Dikey.Size = new System.Drawing.Size(45, 392);
            this.trc_Dikey.TabIndex = 3;
            this.trc_Dikey.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trc_Dikey.Value = 5;
            this.trc_Dikey.Scroll += new System.EventHandler(this.trc_Dikey_Scroll);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pct_Ayar
            // 
            this.pct_Ayar.Location = new System.Drawing.Point(30, 239);
            this.pct_Ayar.Name = "pct_Ayar";
            this.pct_Ayar.Size = new System.Drawing.Size(634, 398);
            this.pct_Ayar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pct_Ayar.TabIndex = 5;
            this.pct_Ayar.TabStop = false;
            // 
            // pnl_Ust
            // 
            this.pnl_Ust.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Ust.Controls.Add(this.rd_Sinif);
            this.pnl_Ust.Controls.Add(this.rd_Soyad);
            this.pnl_Ust.Controls.Add(this.rd_Ad);
            this.pnl_Ust.Controls.Add(this.rd_No);
            this.pnl_Ust.Controls.Add(this.rd_Vesikalik);
            this.pnl_Ust.Controls.Add(this.chc_Yatay);
            this.pnl_Ust.Controls.Add(this.trc_Yatay);
            this.pnl_Ust.Controls.Add(this.trc_Yatay_Has);
            this.pnl_Ust.Location = new System.Drawing.Point(12, 75);
            this.pnl_Ust.Name = "pnl_Ust";
            this.pnl_Ust.Size = new System.Drawing.Size(648, 133);
            this.pnl_Ust.TabIndex = 6;
            // 
            // rd_Sinif
            // 
            this.rd_Sinif.AutoSize = true;
            this.rd_Sinif.Checked = true;
            this.rd_Sinif.ForeColor = System.Drawing.Color.White;
            this.rd_Sinif.Location = new System.Drawing.Point(193, 6);
            this.rd_Sinif.Name = "rd_Sinif";
            this.rd_Sinif.Size = new System.Drawing.Size(45, 17);
            this.rd_Sinif.TabIndex = 9;
            this.rd_Sinif.TabStop = true;
            this.rd_Sinif.Text = "Sinif";
            this.rd_Sinif.UseVisualStyleBackColor = true;
            this.rd_Sinif.CheckedChanged += new System.EventHandler(this.rd_Sinif_CheckedChanged);
            // 
            // rd_Soyad
            // 
            this.rd_Soyad.AutoSize = true;
            this.rd_Soyad.ForeColor = System.Drawing.Color.White;
            this.rd_Soyad.Location = new System.Drawing.Point(351, 5);
            this.rd_Soyad.Name = "rd_Soyad";
            this.rd_Soyad.Size = new System.Drawing.Size(55, 17);
            this.rd_Soyad.TabIndex = 8;
            this.rd_Soyad.TabStop = true;
            this.rd_Soyad.Text = "Soyad";
            this.rd_Soyad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rd_Soyad.UseVisualStyleBackColor = true;
            this.rd_Soyad.CheckedChanged += new System.EventHandler(this.rd_Soyad_CheckedChanged);
            // 
            // rd_Ad
            // 
            this.rd_Ad.AutoSize = true;
            this.rd_Ad.ForeColor = System.Drawing.Color.White;
            this.rd_Ad.Location = new System.Drawing.Point(307, 5);
            this.rd_Ad.Name = "rd_Ad";
            this.rd_Ad.Size = new System.Drawing.Size(38, 17);
            this.rd_Ad.TabIndex = 7;
            this.rd_Ad.TabStop = true;
            this.rd_Ad.Text = "Ad";
            this.rd_Ad.UseVisualStyleBackColor = true;
            this.rd_Ad.CheckedChanged += new System.EventHandler(this.rd_Ad_CheckedChanged);
            // 
            // rd_No
            // 
            this.rd_No.AutoSize = true;
            this.rd_No.ForeColor = System.Drawing.Color.White;
            this.rd_No.Location = new System.Drawing.Point(239, 5);
            this.rd_No.Name = "rd_No";
            this.rd_No.Size = new System.Drawing.Size(62, 17);
            this.rd_No.TabIndex = 6;
            this.rd_No.TabStop = true;
            this.rd_No.Text = "Numara";
            this.rd_No.UseVisualStyleBackColor = true;
            this.rd_No.CheckedChanged += new System.EventHandler(this.rd_No_CheckedChanged);
            // 
            // rd_Vesikalik
            // 
            this.rd_Vesikalik.AutoSize = true;
            this.rd_Vesikalik.ForeColor = System.Drawing.Color.White;
            this.rd_Vesikalik.Location = new System.Drawing.Point(412, 6);
            this.rd_Vesikalik.Name = "rd_Vesikalik";
            this.rd_Vesikalik.Size = new System.Drawing.Size(67, 17);
            this.rd_Vesikalik.TabIndex = 5;
            this.rd_Vesikalik.TabStop = true;
            this.rd_Vesikalik.Text = "Vesikalık";
            this.rd_Vesikalik.UseVisualStyleBackColor = true;
            this.rd_Vesikalik.CheckedChanged += new System.EventHandler(this.rd_Vesikalik_CheckedChanged);
            // 
            // chc_Yatay
            // 
            this.chc_Yatay.AutoSize = true;
            this.chc_Yatay.ForeColor = System.Drawing.Color.White;
            this.chc_Yatay.Location = new System.Drawing.Point(126, 6);
            this.chc_Yatay.Name = "chc_Yatay";
            this.chc_Yatay.Size = new System.Drawing.Size(61, 17);
            this.chc_Yatay.TabIndex = 3;
            this.chc_Yatay.Text = "Hassas";
            this.chc_Yatay.UseVisualStyleBackColor = true;
            this.chc_Yatay.CheckedChanged += new System.EventHandler(this.chc_Yatay_CheckedChanged);
            // 
            // pnl_Yan
            // 
            this.pnl_Yan.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Yan.Controls.Add(this.chc_Dikey);
            this.pnl_Yan.Controls.Add(this.trc_Dikey_Has);
            this.pnl_Yan.Controls.Add(this.trc_Dikey);
            this.pnl_Yan.Location = new System.Drawing.Point(685, 214);
            this.pnl_Yan.Name = "pnl_Yan";
            this.pnl_Yan.Size = new System.Drawing.Size(178, 435);
            this.pnl_Yan.TabIndex = 7;
            // 
            // chc_Dikey
            // 
            this.chc_Dikey.AutoSize = true;
            this.chc_Dikey.ForeColor = System.Drawing.Color.White;
            this.chc_Dikey.Location = new System.Drawing.Point(53, 3);
            this.chc_Dikey.Name = "chc_Dikey";
            this.chc_Dikey.Size = new System.Drawing.Size(61, 17);
            this.chc_Dikey.TabIndex = 4;
            this.chc_Dikey.Text = "Hassas";
            this.chc_Dikey.UseVisualStyleBackColor = true;
            this.chc_Dikey.CheckedChanged += new System.EventHandler(this.chc_Dikey_CheckedChanged);
            // 
            // Btn_Font
            // 
            this.Btn_Font.BackColor = System.Drawing.Color.DimGray;
            this.Btn_Font.Enabled = false;
            this.Btn_Font.ForeColor = System.Drawing.Color.White;
            this.Btn_Font.Location = new System.Drawing.Point(685, 75);
            this.Btn_Font.Name = "Btn_Font";
            this.Btn_Font.Size = new System.Drawing.Size(82, 24);
            this.Btn_Font.TabIndex = 9;
            this.Btn_Font.Text = "Font Seç";
            this.Btn_Font.UseVisualStyleBackColor = false;
            this.Btn_Font.Click += new System.EventHandler(this.Btn_Font_Click);
            // 
            // Btn_Renk
            // 
            this.Btn_Renk.BackColor = System.Drawing.Color.DimGray;
            this.Btn_Renk.Enabled = false;
            this.Btn_Renk.ForeColor = System.Drawing.Color.White;
            this.Btn_Renk.Location = new System.Drawing.Point(773, 75);
            this.Btn_Renk.Name = "Btn_Renk";
            this.Btn_Renk.Size = new System.Drawing.Size(77, 24);
            this.Btn_Renk.TabIndex = 10;
            this.Btn_Renk.Text = "Renk Seç";
            this.Btn_Renk.UseVisualStyleBackColor = false;
            this.Btn_Renk.Click += new System.EventHandler(this.Btn_Renk_Click);
            // 
            // Btn_Hazir
            // 
            this.Btn_Hazir.BackColor = System.Drawing.Color.DimGray;
            this.Btn_Hazir.Enabled = false;
            this.Btn_Hazir.ForeColor = System.Drawing.Color.White;
            this.Btn_Hazir.Location = new System.Drawing.Point(685, 106);
            this.Btn_Hazir.Name = "Btn_Hazir";
            this.Btn_Hazir.Size = new System.Drawing.Size(82, 43);
            this.Btn_Hazir.TabIndex = 11;
            this.Btn_Hazir.Text = "Ayarları Kaydet";
            this.Btn_Hazir.UseVisualStyleBackColor = false;
            this.Btn_Hazir.Click += new System.EventHandler(this.Btn_Hazir_Click);
            // 
            // Btn_Yadir
            // 
            this.Btn_Yadir.BackColor = System.Drawing.Color.DimGray;
            this.Btn_Yadir.Enabled = false;
            this.Btn_Yadir.ForeColor = System.Drawing.Color.White;
            this.Btn_Yadir.Location = new System.Drawing.Point(725, 157);
            this.Btn_Yadir.Name = "Btn_Yadir";
            this.Btn_Yadir.Size = new System.Drawing.Size(84, 26);
            this.Btn_Yadir.TabIndex = 13;
            this.Btn_Yadir.Text = "Yazdır";
            this.Btn_Yadir.UseVisualStyleBackColor = false;
            this.Btn_Yadir.Click += new System.EventHandler(this.Btn_Yadir_Click);
            // 
            // Btn_Kaydet
            // 
            this.Btn_Kaydet.BackColor = System.Drawing.Color.DimGray;
            this.Btn_Kaydet.Enabled = false;
            this.Btn_Kaydet.ForeColor = System.Drawing.Color.White;
            this.Btn_Kaydet.Location = new System.Drawing.Point(773, 106);
            this.Btn_Kaydet.Name = "Btn_Kaydet";
            this.Btn_Kaydet.Size = new System.Drawing.Size(77, 43);
            this.Btn_Kaydet.TabIndex = 15;
            this.Btn_Kaydet.Text = "Resimleri Kaydet";
            this.Btn_Kaydet.UseVisualStyleBackColor = false;
            this.Btn_Kaydet.Click += new System.EventHandler(this.Btn_Kaydet_Click);
            // 
            // Onizle
            // 
            this.Onizle.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.Onizle.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.Onizle.ClientSize = new System.Drawing.Size(400, 300);
            this.Onizle.Document = this.KartCikti;
            this.Onizle.Enabled = true;
            this.Onizle.Icon = ((System.Drawing.Icon)(resources.GetObject("Onizle.Icon")));
            this.Onizle.Name = "Onizle";
            this.Onizle.Text = "Baskı önizleme";
            this.Onizle.Visible = false;
            // 
            // KartCikti
            // 
            this.KartCikti.DocumentName = "Kart Baskisi";
            this.KartCikti.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.KartCiktiDc_EndPrint);
            this.KartCikti.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.KartCiktiDc_PrintPage);
            // 
            // Yazdir
            // 
            this.Yazdir.UseEXDialog = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(599, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Yapılacaklar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(737, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Yapılanalar";
            // 
            // cmbYapilanlar
            // 
            this.cmbYapilanlar.BackColor = System.Drawing.Color.DimGray;
            this.cmbYapilanlar.Enabled = false;
            this.cmbYapilanlar.ForeColor = System.Drawing.Color.White;
            this.cmbYapilanlar.FormattingEnabled = true;
            this.cmbYapilanlar.Location = new System.Drawing.Point(802, 28);
            this.cmbYapilanlar.Name = "cmbYapilanlar";
            this.cmbYapilanlar.Size = new System.Drawing.Size(61, 21);
            this.cmbYapilanlar.TabIndex = 18;
            this.cmbYapilanlar.SelectedIndexChanged += new System.EventHandler(this.cmbYapilanlar_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(16, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tamamlanan";
            // 
            // lblTamamlama
            // 
            this.lblTamamlama.AutoSize = true;
            this.lblTamamlama.BackColor = System.Drawing.Color.Transparent;
            this.lblTamamlama.ForeColor = System.Drawing.Color.White;
            this.lblTamamlama.Location = new System.Drawing.Point(90, 53);
            this.lblTamamlama.Name = "lblTamamlama";
            this.lblTamamlama.Size = new System.Drawing.Size(22, 13);
            this.lblTamamlama.TabIndex = 19;
            this.lblTamamlama.Text = ".....";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // KartBaski
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTamamlama);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbYapilanlar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Kaydet);
            this.Controls.Add(this.Btn_Yadir);
            this.Controls.Add(this.Btn_Hazir);
            this.Controls.Add(this.Btn_Renk);
            this.Controls.Add(this.Btn_Font);
            this.Controls.Add(this.pnl_Yan);
            this.Controls.Add(this.pnl_Ust);
            this.Controls.Add(this.pct_Ayar);
            this.Controls.Add(this.cmbYapilacaklar);
            this.MaximizeBox = false;
            this.Name = "KartBaski";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kart Baskı ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KartBaski_FormClosed);
            this.Load += new System.EventHandler(this.KartBaski_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trc_Yatay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Yatay_Has)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Dikey_Has)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trc_Dikey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pct_Ayar)).EndInit();
            this.pnl_Ust.ResumeLayout(false);
            this.pnl_Ust.PerformLayout();
            this.pnl_Yan.ResumeLayout(false);
            this.pnl_Yan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbYapilacaklar;
        private System.Windows.Forms.TrackBar trc_Yatay;
        private System.Windows.Forms.TrackBar trc_Yatay_Has;
        private System.Windows.Forms.TrackBar trc_Dikey_Has;
        private System.Windows.Forms.TrackBar trc_Dikey;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pct_Ayar;
        private System.Windows.Forms.Panel pnl_Ust;
        private System.Windows.Forms.Panel pnl_Yan;
        private System.Windows.Forms.Button Btn_Font;
        private System.Windows.Forms.Button Btn_Renk;
        private System.Windows.Forms.Button Btn_Hazir;
        private System.Windows.Forms.Button Btn_Yadir;
        private System.Windows.Forms.Button Btn_Kaydet;
        private System.Windows.Forms.RadioButton rd_Soyad;
        private System.Windows.Forms.RadioButton rd_Ad;
        private System.Windows.Forms.RadioButton rd_No;
        private System.Windows.Forms.RadioButton rd_Vesikalik;
        private System.Windows.Forms.CheckBox chc_Yatay;
        private System.Windows.Forms.CheckBox chc_Dikey;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PrintPreviewDialog Onizle;
        private System.Windows.Forms.PrintDialog Yazdir;
        private System.Drawing.Printing.PrintDocument KartCikti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYapilanlar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTamamlama;
        private System.Windows.Forms.RadioButton rd_Sinif;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}