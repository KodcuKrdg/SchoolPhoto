namespace Shool_Photo.Formlar
{
    partial class Album
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Album));
            this.btnYazdir = new System.Windows.Forms.Button();
            this.rdAlbum = new System.Windows.Forms.RadioButton();
            this.rdNotCizergesi = new System.Windows.Forms.RadioButton();
            this.printYazdir = new System.Windows.Forms.PrintDialog();
            this.printAlbum = new System.Drawing.Printing.PrintDocument();
            this.printOnİzleme = new System.Windows.Forms.PrintPreviewDialog();
            this.printNotCizergesi = new System.Drawing.Printing.PrintDocument();
            this.cmbSinif = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbYapilanlar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnYazdir
            // 
            this.btnYazdir.Enabled = false;
            this.btnYazdir.Location = new System.Drawing.Point(65, 132);
            this.btnYazdir.Name = "btnYazdir";
            this.btnYazdir.Size = new System.Drawing.Size(85, 23);
            this.btnYazdir.TabIndex = 3;
            this.btnYazdir.Text = "Yazdır";
            this.btnYazdir.UseVisualStyleBackColor = true;
            this.btnYazdir.Click += new System.EventHandler(this.BtnYazdir_Click);
            // 
            // rdAlbum
            // 
            this.rdAlbum.AutoSize = true;
            this.rdAlbum.BackColor = System.Drawing.Color.Transparent;
            this.rdAlbum.ForeColor = System.Drawing.Color.White;
            this.rdAlbum.Location = new System.Drawing.Point(124, 41);
            this.rdAlbum.Name = "rdAlbum";
            this.rdAlbum.Size = new System.Drawing.Size(54, 17);
            this.rdAlbum.TabIndex = 1;
            this.rdAlbum.Text = "Albüm";
            this.rdAlbum.UseVisualStyleBackColor = false;
            this.rdAlbum.CheckedChanged += new System.EventHandler(this.rdAlbum_CheckedChanged);
            // 
            // rdNotCizergesi
            // 
            this.rdNotCizergesi.AutoSize = true;
            this.rdNotCizergesi.BackColor = System.Drawing.Color.Transparent;
            this.rdNotCizergesi.Checked = true;
            this.rdNotCizergesi.ForeColor = System.Drawing.Color.White;
            this.rdNotCizergesi.Location = new System.Drawing.Point(31, 41);
            this.rdNotCizergesi.Name = "rdNotCizergesi";
            this.rdNotCizergesi.Size = new System.Drawing.Size(87, 17);
            this.rdNotCizergesi.TabIndex = 2;
            this.rdNotCizergesi.TabStop = true;
            this.rdNotCizergesi.Text = "Not Çizergesi";
            this.rdNotCizergesi.UseVisualStyleBackColor = false;
            this.rdNotCizergesi.CheckedChanged += new System.EventHandler(this.rdNotCizergesi_CheckedChanged);
            // 
            // printYazdir
            // 
            this.printYazdir.UseEXDialog = true;
            // 
            // printAlbum
            // 
            this.printAlbum.DocumentName = "Album";
            this.printAlbum.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintAlbum_EndPrint);
            this.printAlbum.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintAlbum_PrintPage);
            // 
            // printOnİzleme
            // 
            this.printOnİzleme.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printOnİzleme.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printOnİzleme.ClientSize = new System.Drawing.Size(400, 300);
            this.printOnİzleme.Enabled = true;
            this.printOnİzleme.Icon = ((System.Drawing.Icon)(resources.GetObject("printOnİzleme.Icon")));
            this.printOnİzleme.Name = "printOnİzleme";
            this.printOnİzleme.Text = "Baskı önizleme";
            this.printOnİzleme.Visible = false;
            // 
            // printNotCizergesi
            // 
            this.printNotCizergesi.DocumentName = "Not Çizergesi";
            this.printNotCizergesi.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintNotCizergesi_EndPrint);
            this.printNotCizergesi.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintNotCizergesi_PrintPage);
            // 
            // cmbSinif
            // 
            this.cmbSinif.FormattingEnabled = true;
            this.cmbSinif.Location = new System.Drawing.Point(6, 30);
            this.cmbSinif.Name = "cmbSinif";
            this.cmbSinif.Size = new System.Drawing.Size(72, 21);
            this.cmbSinif.TabIndex = 5;
            this.cmbSinif.SelectedIndexChanged += new System.EventHandler(this.cmbSinif_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbYapilanlar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSinif);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(23, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 62);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sınıf";
            // 
            // cmbYapilanlar
            // 
            this.cmbYapilanlar.FormattingEnabled = true;
            this.cmbYapilanlar.Location = new System.Drawing.Point(84, 30);
            this.cmbYapilanlar.Name = "cmbYapilanlar";
            this.cmbYapilanlar.Size = new System.Drawing.Size(72, 21);
            this.cmbYapilanlar.TabIndex = 7;
            this.cmbYapilanlar.SelectedIndexChanged += new System.EventHandler(this.cmbYapilanlar_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(83, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Yapılanlar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Yapılacaklar";
            // 
            // Album
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(223, 183);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rdNotCizergesi);
            this.Controls.Add(this.rdAlbum);
            this.Controls.Add(this.btnYazdir);
            this.Name = "Album";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Albüm";
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.Album_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYazdir;
        private System.Windows.Forms.RadioButton rdAlbum;
        private System.Windows.Forms.RadioButton rdNotCizergesi;
        private System.Windows.Forms.PrintDialog printYazdir;
        private System.Drawing.Printing.PrintDocument printAlbum;
        private System.Windows.Forms.PrintPreviewDialog printOnİzleme;
        private System.Drawing.Printing.PrintDocument printNotCizergesi;
        private System.Windows.Forms.ComboBox cmbSinif;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbYapilanlar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}