using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Data.SQLite;
using System.Threading;
using System.Data;



namespace Shool_Photo.Formlar
{
    public partial class KartBaski : Form
    {
        public KartBaski()
        {
            InitializeComponent();
        }

        SQLiteConnection Baglan = new SQLiteConnection("Data Source=Bilgiler.db");
        private void KartBaski_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            IlkVeriler();
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Baglan.Dispose();
            AnaSayfa anaSayfa = new AnaSayfa();
            this.Hide();
            anaSayfa.Show();
        }
        private void KartBaski_FormClosed(object sender, FormClosedEventArgs e)
        {
            Baglan.Dispose();
            Application.Exit();
        }
        private void IlkVeriler()
        {
            cmbYapilacaklar.Items.Clear();
            cmbYapilanlar.Items.Clear();
            string sorgu1 = "Select Sinif from Tbl_Ogrenciler Where IsResim='E' and IsKart='H' Group By Sinif Order By Id ASC";
            string sorgu2 = "Select Sinif from Tbl_Ogrenciler Where IsResim='E' and IsKart='E' Group By Sinif Order By Id ASC";
            int say=0;
            int say1=0;
            try
            {
                Baglan.Open();
                using (var Komut= new SQLiteCommand(sorgu1, Baglan))
                {
                    using (var VeriOku1 = Komut.ExecuteReader())
                    {
                        while (VeriOku1.Read())
                        {
                            cmbYapilacaklar.Items.Add(VeriOku1["Sinif"]);
                            say++;
                        }
                    }
                }
                using (var Komut = new SQLiteCommand(sorgu2, Baglan))
                {
                    using (var VeriOku2 = Komut.ExecuteReader())
                    {
                        while (VeriOku2.Read())
                        {
                            cmbYapilanlar.Items.Add(VeriOku2["Sinif"]);
                            say1++;
                        }
                    }
                }
                Baglan.Close();
                lblTamamlama.Text = say1.ToString() + "/" + (say+say1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally
            {
                Ayar();
            }
        }

        List<string> Numaralar = new List<string>();
        List<string> Adlar = new List<string>();
        List<string> Soyadlar = new List<string>();
        List<string> Vesikalik_Yolu = new List<string>();
        string Sinif, IlkAd = "Ad", IlkSoyad = "Soyad", IlkNo = "No", IlkSinif = "Sınıf";
        private void VeriCek(string gelen)
        {
            Numaralar.Clear();
            Adlar.Clear();
            Soyadlar.Clear();
            Vesikalik_Yolu.Clear();
            IlkNo = "";
            IlkAd = "";
            IlkSoyad = "";
            Sinif = gelen;
            try
            {
                Baglan.Open();
                using (var Komut=Baglan.CreateCommand())
                {
                    Komut.CommandText = "Select No,Ad,Soyad,Sinif,Vesikalik from Tbl_Ogrenciler Where Sinif=@Sinif";
                    Komut.Parameters.AddWithValue("@Sinif", gelen);
                    using (var VeriOku = Komut.ExecuteReader())
                    {
                        int i = 0;
                        while (VeriOku.Read())
                        {
                            Numaralar.Add(VeriOku["No"].ToString());
                            Adlar.Add(VeriOku["Ad"].ToString());
                            Soyadlar.Add(VeriOku["Soyad"].ToString());
                            Vesikalik_Yolu.Add(VeriOku["Vesikalik"].ToString());
                            if (Numaralar[i].Length > IlkNo.Length)
                                IlkNo = Numaralar[i];
                            if (Adlar[i].Length > IlkAd.Length)
                            {
                                IlkAd = Adlar[i];
                                Vesikalik = Image.FromFile(Vesikalik_Yolu[i]);
                                Vesikalik.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            }
                            if (Soyadlar[i].Length > IlkSoyad.Length)
                                IlkSoyad = Soyadlar[i];
                            i++;
                        }
                        IlkSinif = Sinif;
                    }
                }
                Baglan.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally //Verileri Aldıktan sonra Diğer islemleri yaptıroyoruzki veritabanı kapandıktan sonra sorgular çalıştırılmalı
            {
                Hizalama();
                pnl_Ust.Enabled = pnl_Yan.Enabled = Btn_Font.Enabled = Btn_Renk.Enabled = Btn_Hazir.Enabled = true;
            }
        }
        
        private void cmbYapilacaklar_SelectedIndexChanged(object sender, EventArgs e)
        {
            VeriCek(cmbYapilacaklar.SelectedItem.ToString());
        }
        private void cmbYapilanlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            VeriCek(cmbYapilanlar.SelectedItem.ToString());
        }
        /// <summary>
        /// Üzerine isimlerin ve vesikalıkların konulacak şablonu seçer
        /// Şablonun boyutu '1016x638' değilse uyarı verir
        /// Şablon seçildikten sonra hizlama yapa bilmek için gerekli Komponentlerin aktif eder
        /// </summary>
        

        int Ad_Ver, Soyad_Ver, No_Ver, Resim_Ver, Sinif_Ver;
        int Ad_Hor, Soyad_Hor, No_Hor, Resim_Hor, Sinif_Hor;
        int Horizontal, Vertical, Ayar_Ver;
        string DosyaYolu, Arka;
        Image Sablon;
        private void Ayar()
        {
            string sorgu = "Select * from Tbl_Ayar";
            try
            {
                DosyaYolu = Application.StartupPath + "\\Dosyalar\\On.jpg";
                Sablon = Image.FromFile(DosyaYolu);
                Arka = Application.StartupPath + "\\Dosyalar\\Arka.jpg";

                trc_Yatay.Maximum = Sablon.Width; //Resmin Genişliğini verdi
                trc_Dikey.Maximum = Sablon.Height;

                Vertical = Sablon.Height / 2;
                Horizontal = Sablon.Width / 2;
                pct_Ayar.Image = Sablon;

                Baglan.Open();
                using (var Komut= new SQLiteCommand(sorgu, Baglan))
                {
                    using (var VeriOku = Komut.ExecuteReader())
                    {
                        if (VeriOku.Read())
                        {
                            Ad_Ver = Convert.ToInt32(VeriOku["Ad_Ver"]);
                            Soyad_Ver = Convert.ToInt32(VeriOku["Soyad_Ver"]);
                            No_Ver = Convert.ToInt32(VeriOku["No_Ver"]);
                            Sinif_Ver = Convert.ToInt32(VeriOku["Sinif_Ver"]);
                            Resim_Ver = Convert.ToInt32(VeriOku["Vesikalik_Ver"]);

                            Ad_Hor = Convert.ToInt32(VeriOku["Ad_Hor"]);
                            Soyad_Hor = Convert.ToInt32(VeriOku["Soyad_Hor"]);
                            No_Hor = Convert.ToInt32(VeriOku["No_Hor"]);
                            Sinif_Hor = Convert.ToInt32(VeriOku["Sinif_Hor"]);
                            Resim_Hor = Convert.ToInt32(VeriOku["Vesikalik_Hor"]);

                            RenkA = Convert.ToInt32(VeriOku["RenkA"]);
                            RenkR = Convert.ToInt32(VeriOku["RenkR"]);
                            RenkG = Convert.ToInt32(VeriOku["RenkG"]);
                            RenkB = Convert.ToInt32(VeriOku["RenkB"]);

                            renk = Color.FromArgb(RenkA, RenkR, RenkG, RenkB);

                            FontName = VeriOku["FontName"].ToString();
                            FontSize = float.Parse(VeriOku["FontSize"].ToString());
                            FontItalic = Convert.ToChar(VeriOku["FontItalic"]);
                            FontBold = Convert.ToChar(VeriOku["FontBold"]);

                            if (FontItalic == 'E' && FontBold == 'E')
                                font = new Font(FontName, FontSize, FontStyle.Italic | FontStyle.Bold);
                            else if (FontItalic == 'H' && FontBold == 'H')
                                font = new Font(FontName, FontSize, FontStyle.Regular);
                            else if (FontItalic == 'E')
                                font = new Font(FontName, FontSize, FontStyle.Italic);
                            else if (FontBold == 'E')
                                font = new Font(FontName, FontSize, FontStyle.Bold);
                        }
                        else
                        {
                            Ad_Ver = Soyad_Ver = No_Ver = Sinif_Ver = Resim_Ver = Vertical;
                            Ad_Hor = Soyad_Hor = No_Hor = Sinif_Hor = Resim_Hor = Horizontal;
                            font = new Font("Verdana", 11);
                            renk = Color.Black;
                        }
                    }
                }
                Baglan.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally
            {

                Horizontal = trc_Yatay.Value = Sinif_Hor;
                trc_Yatay_Has.Value = 50;

                Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Sinif_Ver);
                Vertical = Sinif_Ver;
                trc_Dikey_Has.Value = 50;

                cmbYapilacaklar.Enabled = true;
                cmbYapilanlar.Enabled = true;
                Hizalama();

            }
        }
        
        /// <summary>
        /// Font Seçimi yapma ve değişkene kaydetme
        /// </summary>
        Font font;
        string FontName;
        float FontSize;
        char FontBold;
        char FontItalic;
        private void Btn_Font_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font;
                FontName = fontDialog1.Font.Name;
                FontSize = fontDialog1.Font.Size;
                if (fontDialog1.Font.Bold)
                    FontBold = 'E';
                else
                    FontBold = 'H';
                if (fontDialog1.Font.Italic)
                    FontItalic = 'E';
                else
                    FontItalic = 'H';
                Hizalama();
            }
        }
        /// <summary>
        /// Renk Seçimi
        /// </summary>
        Color renk;
        int RenkA;
        int RenkR;
        int RenkG;
        int RenkB;
        private void Btn_Renk_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                renk = colorDialog1.Color;
                RenkA = colorDialog1.Color.A;
                RenkR = colorDialog1.Color.R;
                RenkG = colorDialog1.Color.G;
                RenkB = colorDialog1.Color.B;
                Hizalama();
            }
        }
        //*******>Hizalama
        /// <summary>
        /// Kartın üzerinde isimlerin ve Vesikalığın nerede olcağını ayarlar
        /// </summary>
        /// 
        Image Vesikalik = Image.FromFile(Application.StartupPath+"\\Dosyalar\\OrnekVSK.jpg");
        Image Taslak;
        void Hizalama()
        {
            Taslak = Image.FromFile(DosyaYolu);
            Graphics g = Graphics.FromImage(Taslak);
            
            g.DrawImage(Vesikalik, Resim_Hor, Resim_Ver, 288, 432); //295,354 
            g.DrawString(IlkAd, font, new SolidBrush(renk), Ad_Hor, Ad_Ver);
            g.DrawString(IlkSoyad, font, new SolidBrush(renk), Soyad_Hor, Soyad_Ver);
            g.DrawString(IlkNo, font, new SolidBrush(renk), No_Hor, No_Ver);
            g.DrawString(IlkSinif, font, new SolidBrush(renk), Sinif_Hor, Sinif_Ver);

            pct_Ayar.Image = Taslak;
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Resim üzerinde dikey hareketi yapan trakbarların arasında seçimi ayarlar
        /// </summary>
        private void chc_Dikey_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_Dikey.Checked == true)
            {
                trc_Dikey.Enabled = false;
                trc_Dikey_Has.Enabled = true;
                Has_Ver = trc_Dikey_Has.Value;
            }
            else
            {
                trc_Dikey.Enabled = true;
                trc_Dikey_Has.Enabled = false;
                trc_Dikey_Has.Value = 50;
            }
        }
        /// <summary>
        /// Resim üzerinde Yatay hareketi yapan trakbarların arasında seçimi ayarlar
        /// </summary>
        private void chc_Yatay_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_Yatay.Checked == true)
            {
                trc_Yatay.Enabled = false;
                trc_Yatay_Has.Enabled = true;
                Has_Hor = trc_Yatay_Has.Value;
            }
            else
            {
                trc_Yatay.Enabled = true;
                trc_Yatay_Has.Enabled = false;
                trc_Yatay_Has.Value = 50;
            }
        }

        //**********>Vertical/Horizontal Değerini ayarlama
        /// <summary>
        /// Dikey haraketi yakar trackBarların değeri değiştiğinde 
        /// Kaç birin, Hangi tarafa gidildiğini hesaplayıp 
        /// Seçili olan (Vesikalık,No,İsim,Soyad) ın Verticaline ekler
        /// </summary>
        private void trc_Dikey_Scroll(object sender, EventArgs e)
        {
            if (Ayar_Ver < trc_Dikey.Value)
            {
                if (trc_Dikey.Value != trc_Dikey.Maximum)
                {
                    Vertical -= (trc_Dikey.Value - Ayar_Ver);
                    Ayar_Ver += (trc_Dikey.Value - Ayar_Ver);
                }
                else
                    MessageBox.Show("Sınıra Ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Ayar_Ver > trc_Dikey.Value)
            {
                if (trc_Dikey.Value != trc_Dikey.Minimum)
                {
                    Vertical += (Ayar_Ver - trc_Dikey.Value);
                    Ayar_Ver -= (Ayar_Ver - trc_Dikey.Value);
                }
                else
                    MessageBox.Show("Sınıra Ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (rd_Ad.Checked == true)
                Ad_Ver = Vertical;
            else if (rd_Soyad.Checked == true)
                Soyad_Ver = Vertical;
            else if (rd_No.Checked == true)
                No_Ver = Vertical;
            else if (rd_Vesikalik.Checked == true)
                Resim_Ver = Vertical;
            else if (rd_Sinif.Checked == true)
                Sinif_Ver = Vertical;
            Hizalama();
        }
        /// <summary>
        /// Dikey konumu ayarlarken ince dokunuş için kulanılan trackBarın 
        /// Hangi yöne gittiğini ve kaç birim gittiğini hesaplayı
        /// Seçili olan (Vesikalık,No,İsim,Soyad) ın Verticaline ekler
        /// </summary>
        int Has_Ver = 50;
        private void trc_Dikey_Has_Scroll(object sender, EventArgs e)
        {
            if (Has_Ver < trc_Dikey_Has.Value)
            {
                if (trc_Dikey.Value != trc_Dikey.Maximum)
                {
                    Vertical -= (trc_Dikey_Has.Value - Has_Ver);
                    trc_Dikey.Value = trc_Dikey.Maximum - Convert.ToInt32(Vertical);
                    Has_Ver += (trc_Dikey_Has.Value - Has_Ver);
                }
                else
                    MessageBox.Show("Sınıra Ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Has_Ver > trc_Dikey_Has.Value)
            {
                if (trc_Dikey.Value != trc_Dikey.Minimum)
                {
                    Vertical += (Has_Ver - trc_Dikey_Has.Value);
                    trc_Dikey.Value = trc_Dikey.Maximum - Convert.ToInt32(Vertical);
                    Has_Ver -= (Has_Ver - trc_Dikey_Has.Value);
                }
                else
                    MessageBox.Show("Sınıra Ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (rd_Ad.Checked == true)
                Ad_Ver = Vertical;
            else if (rd_Soyad.Checked == true)
                Soyad_Ver = Vertical;
            else if (rd_No.Checked == true)
                No_Ver = Vertical;
            else if (rd_Vesikalik.Checked == true)
                Resim_Ver = Vertical;
            else if (rd_Sinif.Checked == true)
                Sinif_Ver = Vertical;
            Hizalama();
        }
        /// <summary>
        /// Yatay haraketi yakar trackBarların değeri değiştiğinde 
        /// Kaç birin, Hangi tarafa gidildiğini hesaplayıp 
        /// Seçili olan (Vesikalık,No,İsim,Soyad) ın Horizontal ekler
        /// </summary>

        private void trc_Yatay_Scroll(object sender, EventArgs e)
        {
            if (Horizontal < trc_Yatay.Value)
                Horizontal += (trc_Yatay.Value - Horizontal);

            else if (Horizontal > trc_Yatay.Value)
                Horizontal -= (Horizontal - trc_Yatay.Value);

            if (rd_Ad.Checked == true)
                Ad_Hor = Horizontal;
            else if (rd_Soyad.Checked == true)
                Soyad_Hor = Horizontal;
            else if (rd_No.Checked == true)
                No_Hor = Horizontal;
            else if (rd_Vesikalik.Checked == true)
                Resim_Hor = Horizontal;
            else if (rd_Sinif.Checked == true)
                Sinif_Hor = Horizontal;
            Hizalama();
        }

        /// <summary>
        /// Yatay konumu ayarlarken ince dokunuş için kulanılan trackBarın 
        /// Hangi yöne gittiğini ve kaç birim gittiğini hesaplayı
        /// Seçili olan (Vesikalık,No,İsim,Soyad) ın Horizontal ekler
        /// </summary>
        int Has_Hor = 50;
        private void trc_Yatay_Has_Scroll(object sender, EventArgs e)
        {
            if (Has_Hor < trc_Yatay_Has.Value)
            {
                if (trc_Yatay.Value != trc_Yatay.Maximum)
                {
                    trc_Yatay.Value = (Horizontal += (trc_Yatay_Has.Value - Has_Hor));
                    Has_Hor += (trc_Yatay_Has.Value - Has_Hor);
                }
                else
                    MessageBox.Show("Sınıra Ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Has_Hor > trc_Yatay_Has.Value)
            {
                if (trc_Yatay.Value != trc_Yatay.Minimum)
                {
                    trc_Yatay.Value = (Horizontal -= (Has_Hor - trc_Yatay_Has.Value));
                    Has_Hor -= (Has_Hor - trc_Yatay_Has.Value);
                }
                else
                    MessageBox.Show("Maksimum değere ulaştınız!", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (rd_Ad.Checked == true)
                Ad_Hor = Horizontal;
            else if (rd_Soyad.Checked == true)
                Soyad_Hor = Horizontal;
            else if (rd_No.Checked == true)
                No_Hor = Horizontal;
            else if (rd_Vesikalik.Checked == true)
                Resim_Hor = Horizontal;
            else if (rd_Sinif.Checked == true)
                Sinif_Hor = Horizontal;
            Hizalama();
        }


        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        private void rd_No_CheckedChanged(object sender, EventArgs e)
        {
            Horizontal = trc_Yatay.Value = Convert.ToInt32(No_Hor);
            trc_Yatay_Has.Value = 50;

            Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Convert.ToInt32(No_Ver));
            Vertical = Convert.ToInt32(No_Ver);
            trc_Dikey_Has.Value = 50;
        }

        private void rd_Ad_CheckedChanged(object sender, EventArgs e)
        {
            Horizontal = trc_Yatay.Value = Convert.ToInt32(Ad_Hor);
            trc_Yatay_Has.Value = 50;

            Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Convert.ToInt32(Ad_Ver));
            Vertical = Convert.ToInt32(Ad_Ver);
            trc_Dikey_Has.Value = 50;
        }

        

        private void rd_Soyad_CheckedChanged(object sender, EventArgs e)
        {
            Horizontal = trc_Yatay.Value = Convert.ToInt32(Soyad_Hor);
            trc_Yatay_Has.Value = 50;

            Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Convert.ToInt32(Soyad_Ver));
            Vertical = Convert.ToInt32(Soyad_Ver);
            trc_Dikey_Has.Value = 50;
        }

        private void rd_Vesikalik_CheckedChanged(object sender, EventArgs e)
        {
            Horizontal = trc_Yatay.Value = Convert.ToInt32(Resim_Hor);
            trc_Yatay_Has.Value = 50;

            Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Convert.ToInt32(Resim_Ver));
            Vertical = Convert.ToInt32(Resim_Ver);
            trc_Dikey_Has.Value = 50;
        }

        private void rd_Sinif_CheckedChanged(object sender, EventArgs e)
        {
            Horizontal = trc_Yatay.Value = Convert.ToInt32(Sinif_Hor);
            trc_Yatay_Has.Value = 50;

            Ayar_Ver = trc_Dikey.Value = (trc_Dikey.Maximum - Convert.ToInt32(Sinif_Ver));
            Vertical = Convert.ToInt32(Sinif_Ver);
            trc_Dikey_Has.Value = 50;
        }

        private void Btn_Hazir_Click(object sender, EventArgs e)
        {
            if (Btn_Hazir.Text == "Ayarları Kaydet")
            {
                Btn_Font.Enabled = Btn_Renk.Enabled = pnl_Ust.Enabled = pnl_Yan.Enabled = false;
                Btn_Hazir.Text = "Tekrar Ayarla";

                string Sql0, Sql1;
                Sql0 = "Delete from Tbl_Ayar";
                Sql1 = "Insert into Tbl_Ayar (Ad_Ver,Soyad_Ver,No_Ver,Sinif_Ver,Vesikalik_Ver,Ad_Hor,Soyad_Hor,No_Hor,Sinif_Hor,Vesikalik_Hor,RenkA,RenkR,RenkG,RenkB,FontName,FontSize,FontItalic,FontBold) " +
                   "Values('" + Ad_Ver + "','" + Soyad_Ver + "','" + No_Ver + "','" + Sinif_Ver + "','" + Resim_Ver + "','" + Ad_Hor + "','" + Soyad_Hor + "','" + No_Hor + "','" + Sinif_Hor + "','" + Resim_Hor + "','" + RenkA + "','" + RenkR + "','" + RenkG + "','" + RenkB + "','" + FontName + "','" + FontSize + "','" + FontItalic + "','" + FontBold + "')";
                try
                {
                    Baglan.Open();
                    using (var Komut= new SQLiteCommand(Sql0, Baglan))
                    {
                        Komut.ExecuteNonQuery();
                    }
                    using (var Komut =Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Insert into Tbl_Ayar (Ad_Ver,Soyad_Ver,No_Ver,Sinif_Ver,Vesikalik_Ver,Ad_Hor,Soyad_Hor,No_Hor,Sinif_Hor,Vesikalik_Hor,RenkA,RenkR,RenkG,RenkB,FontName,FontSize,FontItalic,FontBold)" +
                            "Values(@Ad_Ver,@Soyad_Ver,@No_Ver,@Sinif_Ver,@Vesikalik_Ver,@Ad_Hor,@Soyad_Hor,@No_Hor,@Sinif_Hor,@Vesikalik_Hor,@RenkA,@RenkR,@RenkG,@RenkB,@FontName,@FontSize,@FontItalic,@FontBold)";
                        
                        Komut.Parameters.AddWithValue("@Ad_Ver", Ad_Ver);
                        Komut.Parameters.AddWithValue("@Soyad_Ver", Soyad_Ver);
                        Komut.Parameters.AddWithValue("@No_Ver", No_Ver);
                        Komut.Parameters.AddWithValue("@Sinif_Ver", Sinif_Ver);
                        Komut.Parameters.AddWithValue("@Vesikalik_Ver", Resim_Ver);
                        Komut.Parameters.AddWithValue("@Ad_Hor", Ad_Hor);
                        Komut.Parameters.AddWithValue("@Soyad_Hor", Soyad_Hor);
                        Komut.Parameters.AddWithValue("@No_Hor", No_Hor);
                        Komut.Parameters.AddWithValue("@Sinif_Hor", Sinif_Hor);
                        Komut.Parameters.AddWithValue("@Vesikalik_Hor", Resim_Hor);
                        Komut.Parameters.AddWithValue("@RenkA", RenkA);
                        Komut.Parameters.AddWithValue("@RenkR", RenkR);
                        Komut.Parameters.AddWithValue("@RenkG", RenkG);
                        Komut.Parameters.AddWithValue("@RenkB", RenkB);
                        Komut.Parameters.AddWithValue("@FontName", FontName);
                        Komut.Parameters.AddWithValue("@FontSize", FontSize);
                        Komut.Parameters.AddWithValue("@FontItalic", "");//Char olduğundan
                        Komut.Parameters.AddWithValue("@FontBold", "");
                        Komut.Parameters["@FontItalic"].Value= FontItalic;
                        Komut.Parameters["@FontBold"].Value= FontBold;

                        Komut.ExecuteNonQuery();
                    }
                    Baglan.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata:" + ex.Message);
                }
                finally
                {
                    Btn_Kaydet.Enabled = true;
                }
            }
            else
            {
                Btn_Font.Enabled = Btn_Renk.Enabled = pnl_Ust.Enabled = pnl_Yan.Enabled = true;
                Btn_Hazir.Text = "Ayarları Kaydet";
                Btn_Kaydet.Enabled = false;
            }

        }

        string Kayit_Yolu;
        List<string> Kartlar = new List<string>();
        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                cmbYapilanlar.Enabled = cmbYapilacaklar.Enabled=pnl_Ust.Enabled = pnl_Yan.Enabled =  Btn_Renk.Enabled = Btn_Font.Enabled = Btn_Hazir.Enabled = Btn_Kaydet.Enabled = false;
                Kayit_Yolu = folderBrowserDialog1.SelectedPath; //Seçilen Klasör Yolu
                Directory.CreateDirectory(Kayit_Yolu + "\\"+Sinif); //Seçilen klasöre Yeni klasör açma
                Kayit_Yolu += "\\"+Sinif + "\\"; //Açılan Klasöre Girme
                for (int i = 0; i < Adlar.Count; i++)
                {
                    Taslak = Image.FromFile(DosyaYolu);
                    Graphics g = Graphics.FromImage(Taslak);
                    g.DrawString(Adlar[i], font, new SolidBrush(renk), Ad_Hor, Ad_Ver);
                    g.DrawString(Soyadlar[i], font, new SolidBrush(renk), Soyad_Hor, Soyad_Ver);
                    g.DrawString(Numaralar[i], font, new SolidBrush(renk), No_Hor, No_Ver);
                    g.DrawImage(Image.FromFile(Vesikalik_Yolu[i]), Resim_Hor, Resim_Ver, 295, 354);
                    g.DrawString(Sinif, font, new SolidBrush(renk), Sinif_Hor, Sinif_Ver);
                    Taslak.Save(Kayit_Yolu + Numaralar[i] + "-" + Adlar[i] + " " + Soyadlar[i] + ".jpg");//kaydecek yerin dosya yolu sonra yeni isimi sonra uzantısı
                    Kartlar.Add(Kayit_Yolu + Numaralar[i] + "-" + Adlar[i] + " " + Soyadlar[i] + ".jpg");
                }
                MessageBox.Show("Kayıt işlemi başlarılı bir şekilde gerçekleştirildi","İşlem Tamamlandı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Btn_Yadir.Enabled = true;
            }
        }
        private void IsKart(string Sinif)
        {
            Baglan.Open();
            using (var Komut=Baglan.CreateCommand())
            {
                Komut.CommandText = "Update Tbl_Ogrenciler Set IsKart='E' Where Sinif=@Sinif";
                Komut.Parameters.AddWithValue("@Sinif", Sinif);
                Komut.ExecuteNonQuery();
            }
            Baglan.Close();
        }
        //Yazdırma Önizlem Ve yazdırma ekranı
        private void Btn_Yadir_Click(object sender, EventArgs e)
        {
            if (Onizle.ShowDialog() == DialogResult.Cancel)
            {
                if (Yazdir.ShowDialog() == DialogResult.OK)
                {
                    IsKart(Sinif);
                    IlkVeriler();
                    cmbYapilacaklar.Text = "";
                    IlkAd = "Ad";
                    IlkSoyad = "Soyad";
                    IlkNo = "No";
                    IlkSinif = "Sınıf";
                    Btn_Hazir.Text = "Ayarları Kaydet";
                    cmbYapilanlar.Enabled = cmbYapilacaklar.Enabled = pnl_Ust.Enabled = pnl_Yan.Enabled = Btn_Renk.Enabled = Btn_Font.Enabled = Btn_Hazir.Enabled = Btn_Kaydet.Enabled = true;
                    Hizalama();
                    KartCikti.Print();
                    cmbYapilacaklar.Enabled = cmbYapilanlar.Enabled = true;
                }
            }
        }

        //Önizledikten sonra yeniden görebilmek için kritik ayarlar bunlar olmazsa fora giremiyor giremediği içinde çıktı ayarlayamıyor
        private void KartCiktiDc_EndPrint(object sender, PrintEventArgs e)
        {
            KartAdet = 0;
            KartSayi = 0;
            OnArka = false;
        }

        /// <summary>
        /// Kartların Çıktı sayfasında nasıl hizalancağı ve arka sayfalarda kartın arka yüzünün ayarlanması
        /// X_Konum1 ve X_Konum2 koordinat sisteminde x kısmı yani sağ sol ayarı yapıldığı yer 
        /// Y_Konum ise yukarı ayarı koordinatı
        /// For un Kartların 2 katı olmasının sebebi ne kadar kartın ön yüzü varsa o kadarda arka yüzü var
        /// kartlar eğer 13 Adet ise 14 adet arka yüz basıyor sebebi ise arka tarfın sağ ve solu farklı olduğundan 
        /// Y_Konum değişkeni Ayar değişkeni ile ile sürekli toplanarak her sorgu sonrası sayfanın yüksekliğini geçiyorsa yeni sayfa açılır ve arka tarafı basan sorguyu çalıştırır
        /// </summary>
        int KartAdet = 0;
        int KartSayi = 0;
        bool OnArka = false;
        private void KartCiktiDc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                e.HasMorePages = false;
                float SayfaGenislik = e.PageBounds.Width;
                float X_Konum, X_Konum1, X_Konum2;
                float Y_Konum;
                bool Gir = true;
                X_Konum = X_Konum1 = ((SayfaGenislik / 2) / 10);
                X_Konum2 = (SayfaGenislik / 2) + 13;
                Y_Konum = 20;

                for (; KartAdet < Kartlar.Count * 2; KartAdet++)
                {
                    if ((Kartlar.Count - KartSayi) == 0 && Kartlar.Count % 10 != 0) //kartlar bitince ve eğer basılan kart sayısı 10 un katı değilse son sayfada kaç tane varsa o kadar yada bir fazlasını basma
                    {
                        int x = 0;
                        if ((Kartlar.Count % 10) % 2 == 0)
                        {
                            x = 5 - ((Kartlar.Count % 10) / 2);
                        }
                        else
                        {
                            x = 5 - (((Kartlar.Count % 10) + 1) / 2);
                            KartAdet--;
                        }
                        Y_Konum = 210 * x;
                        X_Konum = X_Konum2;
                        e.HasMorePages = true;
                        OnArka = true;
                        KartSayi++;
                        break;
                    }
                    if (!OnArka)
                    {
                        e.Graphics.DrawImage(Image.FromFile(Kartlar[KartSayi]), X_Konum, Y_Konum);
                        KartSayi++;
                    }
                    else
                        e.Graphics.DrawImage(Image.FromFile(Arka), X_Konum, Y_Konum);
                    if (Gir) //Eğer sol tarafa yazdırdıysa sağ tarafa yazdırır
                    {
                        X_Konum = X_Konum2;
                        Gir = false;
                    }
                    else
                    {
                        Y_Konum += 210;
                        X_Konum = X_Konum1;
                        Gir = true;
                    }
                    if (Y_Konum >= e.PageBounds.Height - 150) //Sayfa yüksekliğini geçtiğse yeni sayfaya geçer
                    {
                        X_Konum = X_Konum2;
                        e.HasMorePages = true;
                        KartAdet++;
                        OnArka = !OnArka;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
