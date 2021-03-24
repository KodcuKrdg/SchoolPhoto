namespace Shool_Photo
{
    partial class AnaSayfa
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaSayfa));
            this.ptbEokul = new System.Windows.Forms.PictureBox();
            this.ptbKartBaski = new System.Windows.Forms.PictureBox();
            this.ptbAlbum = new System.Windows.Forms.PictureBox();
            this.ptbVesikalik = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ptbSablon = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ptbResizer = new System.Windows.Forms.PictureBox();
            this.ptbBmpToJpeg = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ptbEokul)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbKartBaski)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAlbum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbVesikalik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSablon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbResizer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbBmpToJpeg)).BeginInit();
            this.SuspendLayout();
            // 
            // ptbEokul
            // 
            this.ptbEokul.BackColor = System.Drawing.Color.Transparent;
            this.ptbEokul.Location = new System.Drawing.Point(63, 104);
            this.ptbEokul.Name = "ptbEokul";
            this.ptbEokul.Size = new System.Drawing.Size(279, 212);
            this.ptbEokul.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbEokul.TabIndex = 0;
            this.ptbEokul.TabStop = false;
            this.ptbEokul.Click += new System.EventHandler(this.PtbEokul_Click);
            // 
            // ptbKartBaski
            // 
            this.ptbKartBaski.BackColor = System.Drawing.Color.Transparent;
            this.ptbKartBaski.Location = new System.Drawing.Point(356, 98);
            this.ptbKartBaski.Name = "ptbKartBaski";
            this.ptbKartBaski.Size = new System.Drawing.Size(276, 218);
            this.ptbKartBaski.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbKartBaski.TabIndex = 1;
            this.ptbKartBaski.TabStop = false;
            this.ptbKartBaski.Click += new System.EventHandler(this.PtbKartBaski_Click);
            // 
            // ptbAlbum
            // 
            this.ptbAlbum.BackColor = System.Drawing.Color.Transparent;
            this.ptbAlbum.Location = new System.Drawing.Point(646, 98);
            this.ptbAlbum.Name = "ptbAlbum";
            this.ptbAlbum.Size = new System.Drawing.Size(280, 218);
            this.ptbAlbum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAlbum.TabIndex = 2;
            this.ptbAlbum.TabStop = false;
            this.ptbAlbum.Click += new System.EventHandler(this.PtbAlbum_Click);
            // 
            // ptbVesikalik
            // 
            this.ptbVesikalik.BackColor = System.Drawing.Color.Transparent;
            this.ptbVesikalik.Location = new System.Drawing.Point(73, 392);
            this.ptbVesikalik.Name = "ptbVesikalik";
            this.ptbVesikalik.Size = new System.Drawing.Size(163, 50);
            this.ptbVesikalik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbVesikalik.TabIndex = 3;
            this.ptbVesikalik.TabStop = false;
            this.ptbVesikalik.Click += new System.EventHandler(this.PtbVesikalik_Click);
            // 
            // ptbSablon
            // 
            this.ptbSablon.BackColor = System.Drawing.Color.Transparent;
            this.ptbSablon.Location = new System.Drawing.Point(251, 392);
            this.ptbSablon.Name = "ptbSablon";
            this.ptbSablon.Size = new System.Drawing.Size(187, 49);
            this.ptbSablon.TabIndex = 4;
            this.ptbSablon.TabStop = false;
            this.ptbSablon.Click += new System.EventHandler(this.Btn_Sablon_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ptbResizer
            // 
            this.ptbResizer.BackColor = System.Drawing.Color.Transparent;
            this.ptbResizer.Location = new System.Drawing.Point(458, 392);
            this.ptbResizer.Name = "ptbResizer";
            this.ptbResizer.Size = new System.Drawing.Size(211, 50);
            this.ptbResizer.TabIndex = 5;
            this.ptbResizer.TabStop = false;
            this.ptbResizer.Click += new System.EventHandler(this.ptbResizer_Click);
            // 
            // ptbBmpToJpeg
            // 
            this.ptbBmpToJpeg.BackColor = System.Drawing.Color.Transparent;
            this.ptbBmpToJpeg.Location = new System.Drawing.Point(681, 392);
            this.ptbBmpToJpeg.Name = "ptbBmpToJpeg";
            this.ptbBmpToJpeg.Size = new System.Drawing.Size(234, 48);
            this.ptbBmpToJpeg.TabIndex = 6;
            this.ptbBmpToJpeg.TabStop = false;
            this.ptbBmpToJpeg.Click += new System.EventHandler(this.PtbBmpToJpeg_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 80);
            this.button1.TabIndex = 7;
            this.button1.Text = "Ayarlar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AnaSayfa
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(984, 524);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ptbBmpToJpeg);
            this.Controls.Add(this.ptbResizer);
            this.Controls.Add(this.ptbSablon);
            this.Controls.Add(this.ptbVesikalik);
            this.Controls.Add(this.ptbAlbum);
            this.Controls.Add(this.ptbKartBaski);
            this.Controls.Add(this.ptbEokul);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "AnaSayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Sayfa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnaSayfa_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ptbEokul)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbKartBaski)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAlbum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbVesikalik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSablon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbResizer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbBmpToJpeg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox ptbKartBaski;
        private System.Windows.Forms.PictureBox ptbAlbum;
        private System.Windows.Forms.PictureBox ptbVesikalik;
        private System.Windows.Forms.PictureBox ptbEokul;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox ptbSablon;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox ptbResizer;
        private System.Windows.Forms.PictureBox ptbBmpToJpeg;
        private System.Windows.Forms.Button button1;
    }
}

