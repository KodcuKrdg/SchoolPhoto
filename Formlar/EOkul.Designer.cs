namespace Shool_Photo.Formlar
{
    partial class EOkul
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EOkul));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bcWrVeriCek = new System.ComponentModel.BackgroundWorker();
            this.lstvTablo = new System.Windows.Forms.ListView();
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Soyad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Sinif = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Excel = new System.Windows.Forms.GroupBox();
            this.btn_Ac = new System.Windows.Forms.Button();
            this.cmbSinif = new System.Windows.Forms.ComboBox();
            this.grpAdlandir = new System.Windows.Forms.GroupBox();
            this.btnDuzenle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbSinif = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtOkulAdi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSiniflandir = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpOkulAdi = new System.Windows.Forms.GroupBox();
            this.bcwrSiniflandir = new System.ComponentModel.BackgroundWorker();
            this.Excel.SuspendLayout();
            this.grpAdlandir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSiniflandir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpOkulAdi.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bcWrVeriCek
            // 
            this.bcWrVeriCek.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bcWrVeriCek_DoWork);
            this.bcWrVeriCek.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BcWrVeriCek_ProgressChanged);
            this.bcWrVeriCek.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BcWrVeriCek_RunWorkerCompleted);
            // 
            // lstvTablo
            // 
            this.lstvTablo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvTablo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstvTablo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.Ad,
            this.Soyad,
            this.Sinif});
            this.lstvTablo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(160)));
            this.lstvTablo.ForeColor = System.Drawing.Color.White;
            this.lstvTablo.GridLines = true;
            this.lstvTablo.HideSelection = false;
            this.lstvTablo.Location = new System.Drawing.Point(6, 80);
            this.lstvTablo.Name = "lstvTablo";
            this.lstvTablo.RightToLeftLayout = true;
            this.lstvTablo.ShowItemToolTips = true;
            this.lstvTablo.Size = new System.Drawing.Size(445, 285);
            this.lstvTablo.TabIndex = 5;
            this.lstvTablo.TileSize = new System.Drawing.Size(50, 10);
            this.lstvTablo.UseCompatibleStateImageBehavior = false;
            this.lstvTablo.View = System.Windows.Forms.View.Details;
            // 
            // No
            // 
            this.No.Text = "NO";
            // 
            // Ad
            // 
            this.Ad.Text = "AD";
            this.Ad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Ad.Width = 129;
            // 
            // Soyad
            // 
            this.Soyad.Text = "SOYAD";
            this.Soyad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Soyad.Width = 172;
            // 
            // Sinif
            // 
            this.Sinif.Text = "SINIF";
            this.Sinif.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Sinif.Width = 80;
            // 
            // Excel
            // 
            this.Excel.BackColor = System.Drawing.Color.Transparent;
            this.Excel.Controls.Add(this.btn_Ac);
            this.Excel.Controls.Add(this.lstvTablo);
            this.Excel.ForeColor = System.Drawing.Color.White;
            this.Excel.Location = new System.Drawing.Point(56, 23);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(457, 371);
            this.Excel.TabIndex = 6;
            this.Excel.TabStop = false;
            this.Excel.Text = "İsimleri Sisteme Ekle";
            // 
            // btn_Ac
            // 
            this.btn_Ac.BackColor = System.Drawing.Color.DimGray;
            this.btn_Ac.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Ac.ForeColor = System.Drawing.Color.White;
            this.btn_Ac.Location = new System.Drawing.Point(172, 29);
            this.btn_Ac.Name = "btn_Ac";
            this.btn_Ac.Size = new System.Drawing.Size(104, 31);
            this.btn_Ac.TabIndex = 6;
            this.btn_Ac.Text = "Excel Seç";
            this.btn_Ac.UseVisualStyleBackColor = false;
            this.btn_Ac.Click += new System.EventHandler(this.Excel_Ac);
            // 
            // cmbSinif
            // 
            this.cmbSinif.Enabled = false;
            this.cmbSinif.FormattingEnabled = true;
            this.cmbSinif.Location = new System.Drawing.Point(99, 39);
            this.cmbSinif.Name = "cmbSinif";
            this.cmbSinif.Size = new System.Drawing.Size(100, 21);
            this.cmbSinif.TabIndex = 0;
            this.cmbSinif.Tag = "";
            this.cmbSinif.SelectedIndexChanged += new System.EventHandler(this.CmbSinif_SelectedIndexChanged);
            // 
            // grpAdlandir
            // 
            this.grpAdlandir.BackColor = System.Drawing.Color.Transparent;
            this.grpAdlandir.Controls.Add(this.btnDuzenle);
            this.grpAdlandir.Controls.Add(this.label2);
            this.grpAdlandir.Controls.Add(this.label1);
            this.grpAdlandir.Controls.Add(this.txbSinif);
            this.grpAdlandir.Controls.Add(this.cmbSinif);
            this.grpAdlandir.ForeColor = System.Drawing.Color.White;
            this.grpAdlandir.Location = new System.Drawing.Point(532, 134);
            this.grpAdlandir.Name = "grpAdlandir";
            this.grpAdlandir.Size = new System.Drawing.Size(228, 162);
            this.grpAdlandir.TabIndex = 8;
            this.grpAdlandir.TabStop = false;
            this.grpAdlandir.Text = "Sınıfları Yeniden Adlandır";
            // 
            // btnDuzenle
            // 
            this.btnDuzenle.BackColor = System.Drawing.Color.DimGray;
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.Location = new System.Drawing.Point(86, 119);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(68, 27);
            this.btnDuzenle.TabIndex = 12;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.UseVisualStyleBackColor = false;
            this.btnDuzenle.Click += new System.EventHandler(this.Duzenle);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Yeniden Adlandır";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sınıflar";
            // 
            // txbSinif
            // 
            this.txbSinif.Enabled = false;
            this.txbSinif.Location = new System.Drawing.Point(99, 84);
            this.txbSinif.Name = "txbSinif";
            this.txbSinif.Size = new System.Drawing.Size(100, 20);
            this.txbSinif.TabIndex = 1;
            this.txbSinif.Tag = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(56, 406);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(457, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // txtOkulAdi
            // 
            this.txtOkulAdi.Enabled = false;
            this.txtOkulAdi.Location = new System.Drawing.Point(66, 28);
            this.txtOkulAdi.Name = "txtOkulAdi";
            this.txtOkulAdi.Size = new System.Drawing.Size(158, 20);
            this.txtOkulAdi.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Okul Adı";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.Enabled = false;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(86, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Ekle/Düzenle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OkulAdiEkle);
            // 
            // btnSiniflandir
            // 
            this.btnSiniflandir.BackColor = System.Drawing.Color.Transparent;
            this.btnSiniflandir.Location = new System.Drawing.Point(602, 323);
            this.btnSiniflandir.Name = "btnSiniflandir";
            this.btnSiniflandir.Size = new System.Drawing.Size(109, 106);
            this.btnSiniflandir.TabIndex = 18;
            this.btnSiniflandir.TabStop = false;
            this.btnSiniflandir.Click += new System.EventHandler(this.btnSiniflandir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(586, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sınıflandırma ve İsimlendirme";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // grpOkulAdi
            // 
            this.grpOkulAdi.BackColor = System.Drawing.Color.Transparent;
            this.grpOkulAdi.Controls.Add(this.button1);
            this.grpOkulAdi.Controls.Add(this.label3);
            this.grpOkulAdi.Controls.Add(this.txtOkulAdi);
            this.grpOkulAdi.ForeColor = System.Drawing.Color.White;
            this.grpOkulAdi.Location = new System.Drawing.Point(532, 34);
            this.grpOkulAdi.Name = "grpOkulAdi";
            this.grpOkulAdi.Size = new System.Drawing.Size(234, 94);
            this.grpOkulAdi.TabIndex = 20;
            this.grpOkulAdi.TabStop = false;
            this.grpOkulAdi.Text = "Okul Adı Ekleme/Düzeltme";
            // 
            // EOkul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.grpOkulAdi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSiniflandir);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.grpAdlandir);
            this.Controls.Add(this.Excel);
            this.MaximizeBox = false;
            this.Name = "EOkul";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "E Okul";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EOkul_FormClosed);
            this.Load += new System.EventHandler(this.EOkul_Load);
            this.Excel.ResumeLayout(false);
            this.grpAdlandir.ResumeLayout(false);
            this.grpAdlandir.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSiniflandir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpOkulAdi.ResumeLayout(false);
            this.grpOkulAdi.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker bcWrVeriCek;
        private System.Windows.Forms.ListView lstvTablo;
        private System.Windows.Forms.ColumnHeader Ad;
        private System.Windows.Forms.ColumnHeader Soyad;
        private System.Windows.Forms.ColumnHeader Sinif;
        private System.Windows.Forms.GroupBox Excel;
        private System.Windows.Forms.ColumnHeader No;
        private System.Windows.Forms.ComboBox cmbSinif;
        private System.Windows.Forms.GroupBox grpAdlandir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbSinif;
        private System.Windows.Forms.Button btn_Ac;
        private System.Windows.Forms.Button btnDuzenle;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtOkulAdi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox btnSiniflandir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpOkulAdi;
        private System.ComponentModel.BackgroundWorker bcwrSiniflandir;
    }
}