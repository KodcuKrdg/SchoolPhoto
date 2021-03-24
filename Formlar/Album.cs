using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SQLite;
using System.Data;


namespace Shool_Photo.Formlar
{
    public partial class Album : Form
    {
        public Album()
        {
            InitializeComponent();
        }
        SQLiteConnection Baglan = new SQLiteConnection("Data Source = Bilgiler.db");
        private void ComboBoxlarıDoldur()
        {
            cmbSinif.Text = "";
            cmbYapilanlar.Text = "";
            cmbSinif.Items.Clear();
            cmbYapilanlar.Items.Clear();
            Baglan.Open();
            using (var Komut = new SQLiteCommand("Select Sinif from Tbl_Ogrenciler Where IsNotCizergesi='H' Group by Sinif Order by Id", Baglan))
            {
                using (var Veri = Komut.ExecuteReader())
                {
                    while (Veri.Read())
                    {
                        cmbSinif.Items.Add(Veri["Sinif"].ToString());
                    }
                }
            }
            using (var Komut = new SQLiteCommand("Select Sinif from Tbl_Ogrenciler Where IsNotCizergesi='E' Group by Sinif Order by Id", Baglan))
            {
                using (var Veri = Komut.ExecuteReader())
                {
                    while (Veri.Read())
                    {
                        cmbYapilanlar.Items.Add(Veri["Sinif"].ToString());
                    }
                }
            }
            Baglan.Close();
        }
        private void Album_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            ComboBoxlarıDoldur();            
        }
        
        List<string> Vesikaliklar = new List<string>();
        List<string> Numaralar = new List<string>();
        List<string> Adlar = new List<string>();
        List<string> Soyadlar = new List<string>(); 
        List<string> Siniflar = new List<string>();
        string OkulAdi;
        private void Veriler(string Gelen)
        {
            btnYazdir.Enabled = false;
            cmbSinif.Text = cmbYapilanlar.Text = "";
            Vesikaliklar.Clear();
            Numaralar.Clear();
            Adlar.Clear();
            Soyadlar.Clear();
            Siniflar.Clear();
            string sorgu = "Select No,Ad,Soyad,Sinif,Vesikalik,OkulAdi from Tbl_Ogrenciler Where Sinif='" + Gelen + "'";
            try
            {
                Baglan.Open();
                using (var Komut = new SQLiteCommand(sorgu, Baglan))
                {
                    using (var VeriOku = Komut.ExecuteReader())
                    {
                        while (VeriOku.Read())
                        {
                            Vesikaliklar.Add(VeriOku["Vesikalik"].ToString());
                            Numaralar.Add(VeriOku["No"].ToString());
                            Adlar.Add(VeriOku["Ad"].ToString());
                            Soyadlar.Add(VeriOku["Soyad"].ToString());
                            Siniflar.Add(VeriOku["Sinif"].ToString());
                            OkulAdi = VeriOku["OkulAdi"].ToString();
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
                IsimleriAyarla();
                btnYazdir.Enabled = true;
            }
        }
        private void cmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            Veriler(cmbSinif.SelectedItem.ToString());
        }
        private void cmbYapilanlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Veriler(cmbYapilanlar.SelectedItem.ToString());
        }
        private void rdNotCizergesi_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            btnYazdir.Enabled = false;
            cmbSinif.Text = cmbYapilanlar.Text = "";
        }
        private void rdAlbum_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            btnYazdir.Enabled = true;
            cmbSinif.Text = cmbYapilanlar.Text = "";
        }

        List<string> Isimler = new List<string>();
        float okulAdKonum;
        private void IsimleriAyarla()
        {
            Isimler.Clear();
            string Ara;
            int uz;
            if (rdAlbum.Checked == true)
                uz = 18;
            else
                uz = 19;
            for (int i = 0; i < Adlar.Count; i++)
            {
               Ara = Numaralar[i]+" "+Adlar[i];
                if (Ara.Length>=uz)
                {
                    Isimler.Add(Ara.Substring(0, uz-1) + ".");
                }
                else
                    Isimler.Add(Ara);
            }
            //okul Adı hizlama
            int hsp = OkulAdi.Length;
            okulAdKonum = ((100* hsp) / 17)-15;

        }
        List<int> sinifsayısı = new List<int>();
        private void sinifsayısıHesapla()
        {
            sinifsayısı.Clear();
            string sorgu = "Select COUNT(Id) from Tbl_Ogrenciler Group By Sinif Order By Id ASC";
            try
            {
                Baglan.Open();
                using (var Komut=new SQLiteCommand(sorgu,Baglan))
                {
                    using (var VeriOku=Komut.ExecuteReader())
                    {
                        while (VeriOku.Read())
                        {
                            sinifsayısı.Add(Convert.ToInt32(VeriOku["COUNT(Id)"]));
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
        }
        private void TamamlananVeriler(string gelen)
        {
            try
            {
                groupBox1.Enabled = false;
                rdAlbum.Enabled = false;
                Baglan.Open();
                using (var Komut = Baglan.CreateCommand())
                {
                    Komut.CommandText = "Update Tbl_Ogrenciler Set IsNotCizergesi='E' Where Sinif=@Sinif";
                    Komut.Parameters.AddWithValue("@Sinif", gelen);
                    Komut.ExecuteNonQuery();
                }
                Baglan.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                groupBox1.Enabled = true;
                rdAlbum.Enabled = true;
            }

        }
        private void TumVeriler()
        {
            Vesikaliklar.Clear();
            Numaralar.Clear();
            Adlar.Clear();
            Soyadlar.Clear();
            Siniflar.Clear();
            string sorgu = "Select No,Ad,Soyad,Sinif,Vesikalik,OkulAdi from Tbl_Ogrenciler Order By Id ASC";
            try
            {
                Baglan.Open();
                using (var Komut = new SQLiteCommand(sorgu, Baglan))
                {
                    using (var VeriOku = Komut.ExecuteReader())
                    {
                        while (VeriOku.Read())
                        {
                            Vesikaliklar.Add(VeriOku["Vesikalik"].ToString());
                            Numaralar.Add(VeriOku["No"].ToString());
                            Adlar.Add(VeriOku["Ad"].ToString());
                            Soyadlar.Add(VeriOku["Soyad"].ToString());
                            Siniflar.Add(VeriOku["Sinif"].ToString());
                            OkulAdi = VeriOku["OkulAdi"].ToString();
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
                IsimleriAyarla();
                sinifsayısıHesapla();
            }
        }
        int Bitis;
        private void BtnYazdir_Click(object sender, EventArgs e)
        {
            int Parca;
            if (rdAlbum.Checked==true)
            {
                TumVeriler();
                printOnİzleme.Document = printAlbum;
                if (printOnİzleme.ShowDialog()==DialogResult.Cancel)
                {
                    if (printYazdir.ShowDialog()==DialogResult.OK)
                    {
                        btnYazdir.Enabled = false;
                        printAlbum.Print();
                    }
                }
            }
            else if(rdNotCizergesi.Checked==true)
            {
                Parca =(Vesikaliklar.Count) % 4;
                Bitis=((Vesikaliklar.Count - Parca) / 4);
                if (Parca != 0)
                    Bitis++;
                printOnİzleme.Document = printNotCizergesi;
                if (printOnİzleme.ShowDialog() == DialogResult.Cancel)
                {
                    if (printYazdir.ShowDialog() == DialogResult.OK)
                    {
                        btnYazdir.Enabled = false;
                        cmbSinif.Text = cmbYapilanlar.Text = "";
                        TamamlananVeriler(Siniflar[0]);
                        ComboBoxlarıDoldur();
                        printNotCizergesi.Print();
                    }
                }
            }
        }
        private void PrintNotCizergesi_EndPrint(object sender, PrintEventArgs e)
        {
            d= 0;
            Bas = 3;
            Bit = 0;
        }
        int d = 0;
        int Bas = 3;
        int Bit = 0;
        private void PrintNotCizergesi_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            Pen RnkCizgi = new Pen(Color.Black, 5);
            float SyGenislik = e.PageBounds.Width;
            float SyYukseklik = e.PageBounds.Height;
            float Orta = SyYukseklik / 2;
            float X0, Y0;
            float Z;
            Z = 20;
            X0 = 10;
            Y0 = 20;

            Image Img;
            Font Fnt = new Font("Verdana", 7);
            Color Rnk = Color.Black;
            bool Gir = true;
            float VesiX, AltX, UstX, YaziX, DkyX, DkyY;
            VesiX = 33;
            AltX = 0;
            UstX = 154;
            YaziX = 34;
            DkyX = (UstX - X0) / 6;
            DkyY = (Orta - Z - 128) / 10;
            //Üst
            if (d != Bitis)
            {
                // Ana Dikdörtgen
                e.Graphics.DrawLine(RnkCizgi, X0, Y0, SyGenislik - X0, Y0); //Yatay
                e.Graphics.DrawLine(RnkCizgi, X0, Orta - Z, SyGenislik - X0, Orta - Z);
                e.Graphics.DrawLine(RnkCizgi, X0, Y0 - 2.5f, X0, Orta - Z + 2.5f); //Dikey
                e.Graphics.DrawLine(RnkCizgi, SyGenislik - X0, Y0 - 2.5f, SyGenislik - X0, Orta - Z + 2.5f);
                e.Graphics.DrawLine(RnkCizgi, SyGenislik - X0 - 41, Y0, SyGenislik - X0 - 41, Orta - Z);
                e.Graphics.TranslateTransform(SyGenislik - X0-15, (Y0 + 108 + DkyY * 5)); //Yazı Konum
                e.Graphics.RotateTransform(90); //Yazı açı
                e.Graphics.DrawString(OkulAdi, Fnt, new SolidBrush(Rnk), -((OkulAdi.Length*5)/2), 0);
                e.Graphics.DrawString(Siniflar[0], Fnt, new SolidBrush(Rnk), 2, 10);
                e.Graphics.ResetTransform(); //Yazı Açı restleme 

                for (int i = Bas; i >= Bit; i--)
                {
                    if (Gir)
                    {
                        //burası tamam onun üstüne ekleyerek diğer kodları yap
                        if (i<Vesikaliklar.Count)
                        {
                            Img = Image.FromFile(Vesikaliklar[i]);
                            Img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            e.Graphics.TranslateTransform(YaziX, Y0 +2); //Yazı Konum
                            e.Graphics.RotateTransform(90); //Yazı açı
                            e.Graphics.DrawString(Isimler[i], Fnt, new SolidBrush(Rnk),2, 0);
                            e.Graphics.DrawString(Soyadlar[i], Fnt, new SolidBrush(Rnk),2,10);
                            e.Graphics.ResetTransform(); //Yazı Açı restleme 
                            e.Graphics.DrawImage(Img, VesiX+2, Y0 + 6.5f, 115, 95.3f); //Vesikalık ı Döndürdüğümüz içi Genişlik ve Yükseliiği yeri değişiyor (Yükseklik x Genişlik)
                        }
                        e.Graphics.DrawLine(RnkCizgi, UstX, Y0, 154, Orta - Z); // Vesikalık Üst Çizgi
                        //e.Graphics.DrawLine(RnkCizgi, UstX + (48 / 2), 128, UstX + (48 / 2), Orta - Z); //Alt çizgi ile üst çizgi arasındaki kısım
                        //Alt ve Üst çizgi arasındaki Dikey çizgiler
                        for (int k = 1; k < 6; k++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, X0 + DkyX * k, 128, X0 + DkyX * k, Orta - Z);
                        }
                        //Alt ve Üst çizgi arasındaki Yatay çizgiler
                        for (int j = 1; j < 10; j++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, X0, 128 + DkyY * j, UstX, 128 + DkyY * j);
                        }
                        Gir = false;
                    }
                    else
                    {
                        AltX = UstX + 48;
                        YaziX = AltX + 25;
                        VesiX = AltX + 26;
                        UstX = AltX + 145;
                        if (i < Vesikaliklar.Count)
                        {
                            Img = Image.FromFile(Vesikaliklar[i]);
                            Img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            e.Graphics.TranslateTransform(YaziX, Y0 + 2); //Yazı Konum
                            e.Graphics.RotateTransform(90); //Yazı açı
                            e.Graphics.DrawString(Isimler[i], Fnt, new SolidBrush(Rnk), 2, 0);
                            e.Graphics.DrawString(Soyadlar[i], Fnt, new SolidBrush(Rnk), 2, 10);
                            e.Graphics.ResetTransform(); //Yazı Açı restleme 
                            e.Graphics.DrawImage(Img, VesiX, Y0 + 6.5f, 115, 95.3f); ////Vesikalık ı Döndürdüğümüz içi Genişlik ve Yükseliiği yeri değişiyor
                        }
                        e.Graphics.DrawLine(RnkCizgi, AltX, Y0, AltX, Orta - Z); // Vesikalık Alt Çizgi
                        e.Graphics.DrawLine(RnkCizgi, UstX, Y0, UstX, Orta - Z); // Vesikalık Üst Çizgi
                        //e.Graphics.DrawLine(RnkCizgi, UstX + (48 / 2), 128, UstX + (48 / 2), Orta - Z); //Alt çizgi ile üst çizgi arasındaki kısım
                        //Alt ve Üst çizgi arasındaki Dikey çizgil
                        for (int k = 1; k < 6; k++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, AltX + DkyX * k, 128, AltX + DkyX * k, Orta - Z);
                        }
                        //Alt ve Üst çizgi arasındaki Yatay çizgiler
                        for (int j = 1; j < 10; j++)
                        {
                            if (j == 5) // notların arasındaki uzun cizgiler 
                                e.Graphics.DrawLine(RnkCizgi, X0, 128 + DkyY * j, SyGenislik - X0 - 41, 128 + DkyY * j);
                            else
                                e.Graphics.DrawLine(RnkCizgi, AltX, 128 + DkyY * j, UstX, 128 + DkyY * j);

                        }
                    }
                }
                Bas += 4;
                Bit += 4;
                e.Graphics.DrawLine(RnkCizgi, X0, 128, SyGenislik - X0, 128); //vesikalıkların ve yazıların bitiminde çekilen çizgi
                d++;
            }
            //Alt
            Gir = true;
            if (d != Bitis)
            {
                Y0 = Orta + Z;
                VesiX = 33;
                AltX = 0;
                UstX = 154;
                YaziX = 35;
                // Ana Dikdörtgen
                e.Graphics.DrawLine(RnkCizgi, X0, Y0, SyGenislik - X0, Y0); //Yatay
                e.Graphics.DrawLine(RnkCizgi, X0, SyYukseklik - Z, SyGenislik - X0, SyYukseklik - Z);
                e.Graphics.DrawLine(RnkCizgi, X0, Y0 - 2.5f, X0, SyYukseklik - Z + 2.5f); //Dikey
                e.Graphics.DrawLine(RnkCizgi, SyGenislik - X0, Y0 - 2.5f, SyGenislik - X0, SyYukseklik - Z + 2.5f);
                e.Graphics.DrawLine(RnkCizgi, SyGenislik - X0 - 41, Y0, SyGenislik - X0 - 41, SyYukseklik - Z);

                DkyY = ((SyYukseklik - Z) - (Y0 + 108)) / 10;

                e.Graphics.TranslateTransform(SyGenislik - X0 - 15, (Y0 + 108 + DkyY *5)); //Yazı Konum
                e.Graphics.RotateTransform(90); //Yazı açı
                e.Graphics.DrawString(OkulAdi, Fnt, new SolidBrush(Rnk), -((OkulAdi.Length * 5) / 2), 0);
                e.Graphics.DrawString(Siniflar[0], Fnt, new SolidBrush(Rnk), 2, 10);
                e.Graphics.ResetTransform(); //Yazı Açı restleme 
                
                for (int i = Bas; i >= Bit; i--)
                {
                    if (Gir)
                    {
                        if (i<Vesikaliklar.Count)
                        {
                            Img = Image.FromFile(Vesikaliklar[i]);
                            Img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            e.Graphics.TranslateTransform(YaziX, Y0 + 2); //Yazı Konum
                            e.Graphics.RotateTransform(90); //Yazı açı
                            e.Graphics.DrawString(Isimler[i], Fnt, new SolidBrush(Rnk), 2, 0);
                            e.Graphics.DrawString(Soyadlar[i], Fnt, new SolidBrush(Rnk),2, 10);
                            e.Graphics.ResetTransform(); //Yazı Açı restleme 
                            e.Graphics.DrawImage(Img, VesiX+2, Y0 + 6.5f, 115, 95.3f); ////Vesikalık ı Döndürdüğümüz içi Genişlik ve Yükseliiği yeri değişiyor
                        }
                        e.Graphics.DrawLine(RnkCizgi, UstX, Y0, 154, SyYukseklik - Z); // Vesikalık Üst Çizgi
                        e.Graphics.DrawLine(RnkCizgi, UstX + (48 / 2), Y0 + 108, UstX + (48 / 2), SyYukseklik - Z); //Alt çizgi ile üst çizgi arasındaki kısım
                        //Alt ve Üst çizgi arasındaki Dikey çizgiler
                        for (int k = 1; k < 6; k++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, X0 + DkyX * k, Y0 + 108, X0 + DkyX * k, SyYukseklik - Z);
                        }
                        //Alt ve Üst çizgi arasındaki Yatay çizgiler
                        for (int j = 1; j < 10; j++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, X0, Y0 + 108 + DkyY * j, UstX, Y0 + 108 + DkyY * j);
                        }
                        Gir = false;
                    }
                    else
                    {
                        AltX = UstX + 48;
                        YaziX = AltX + 25;
                        VesiX = AltX + 26;
                        UstX = AltX + 145;
                        if (i < Vesikaliklar.Count)
                        {
                            Img = Image.FromFile(Vesikaliklar[i]);
                            Img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            e.Graphics.TranslateTransform(YaziX, Y0 + 2); //Yazı Konum
                            e.Graphics.RotateTransform(90); //Yazı açı
                            e.Graphics.DrawString(Isimler[i], Fnt, new SolidBrush(Rnk), 2, 0);
                            e.Graphics.DrawString(Soyadlar[i], Fnt, new SolidBrush(Rnk),2, 10);
                            e.Graphics.ResetTransform(); //Yazı Açı restleme 
                            e.Graphics.DrawImage(Img, VesiX, Y0 + 6.5f, 115, 95.3f); ////Vesikalık ı Döndürdüğümüz içi Genişlik ve Yükseliiği yeri değişiyor
                        }
                        e.Graphics.DrawLine(RnkCizgi, AltX, Y0, AltX, SyYukseklik - Z); // Vesikalık Alt Çizgi
                        e.Graphics.DrawLine(RnkCizgi, UstX, Y0, UstX, SyYukseklik - Z); // Vesikalık Üst Çizgi
                        //Alt ve Üst çizgi arasındaki Dikey çizgiler
                        for (int k = 1; k < 6; k++)
                        {
                            e.Graphics.DrawLine(RnkCizgi, AltX + DkyX * k, Y0 + 108, AltX + DkyX * k, SyYukseklik - Z);
                        }
                        //Alt ve Üst çizgi arasındaki Yatay çizgiler
                        for (int j = 1; j < 10; j++)
                        {
                            if (j == 5)
                                e.Graphics.DrawLine(RnkCizgi, X0, Y0 + 108 + DkyY * j, SyGenislik - X0 - 41, Y0 + 108 + DkyY * j);
                            else
                                e.Graphics.DrawLine(RnkCizgi, AltX, Y0 + 108 + DkyY * j, UstX, Y0 + 108 + DkyY * j);

                        }
                    }
                }
                Bas += 4;
                Bit += 4;
                e.Graphics.DrawLine(RnkCizgi, X0, Y0 + 108, SyGenislik - X0, Y0 + 108); //vesikalıkların ve yazıların bitiminde çekilen çizgi
                d++;
                if (d != Bitis)
                    e.HasMorePages = true;
            }
        }

        private void PrintAlbum_EndPrint(object sender, PrintEventArgs e)
        {
            say = 0;
            sinifsayaç = 0;
            sayaç = 0;
            okulY = 0;
        }

        float[] korY = new float[5];
        int say=0;
        int sinifsayaç=0;
        int sayaç=0;
        float okulY=0;
        private void PrintAlbum_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            float SyGenislik = e.PageBounds.Width;
            float SyYukseklik = e.PageBounds.Height;
            float Orta,islem,vesix;

            Image Vesi;
            
            Pen Cizgi = new Pen(Color.Black,5);
            Font Fnt = new Font("Verdana", 7);
            Font Fnt1 = new Font("Verdana", 15);
            Color Rnk = Color.Black;
            Orta = SyYukseklik / 2;
            
            islem = (Orta) / 5;
            vesix = SyGenislik - 270;
            for (int i = 0; i < 5; i++)
            {
                if (i == 2)
                    korY[i] = (islem * i) + 5;
                else if (i > 2)
                    korY[i] = ((islem * i) - (12 / (5 - i))) + 5; // 4.5. vesikalıklar
                else
                    korY[i] = ((islem * i) + (12 / (i + 1))) + 5; // 1.2 resimler
            }
            okulY = ((korY[4] + 108) - (korY[0] - 8)) / 2;
            // Üst
            // Okul Bilgi kısmıın çerçevesi
            e.Graphics.DrawLine(Cizgi, SyGenislik-137, korY[0] -5.5f, SyGenislik - 50, korY[0] -5.5f); //Resimin üstündeki Okul Ad kısmı dikey cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik-137, korY[4] +105.5f, SyGenislik - 50, korY[4] +105.5f); //Resimin üstündeki Okul Ad kısmı dikey cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik-52.5f, korY[0] - 5.5f, SyGenislik - 52.5f, korY[4] +105.5f); //Resimin üstündeki Okul Ad kısmı Üst cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik-137, korY[0] - 8, SyGenislik - 137, korY[4] + 108); //Resimin üstündeki Okul Ad kısmı alt cizgi
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sinifsayaç < sinifsayısı[sayaç])
                    {
                        Vesi = Image.FromFile(Vesikaliklar[say]);
                        Vesi.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        //Okul Bilgi kısmı 
                        e.Graphics.TranslateTransform(SyGenislik - 85, okulY); //Yazı Konum
                        e.Graphics.RotateTransform(90); //Yazı açı
                        e.Graphics.DrawString(OkulAdi, Fnt1, new SolidBrush(Rnk), -okulAdKonum, -10);
                        e.Graphics.DrawString(Siniflar[say], Fnt1, new SolidBrush(Rnk), 0, 11); 
                        e.Graphics.ResetTransform(); //Yazı Açı restleme
                        //Öğrenci İsimleri
                        e.Graphics.TranslateTransform(vesix, korY[i]); //Yazı Konum
                        e.Graphics.RotateTransform(90); //Yazı açı
                        e.Graphics.DrawString(Isimler[say], Fnt, new SolidBrush(Rnk), 2, 0);
                        e.Graphics.DrawString(Soyadlar[say], Fnt, new SolidBrush(Rnk), 2, 11); //isimx[i] ekle
                        e.Graphics.ResetTransform(); //Yazı Açı restleme
                        e.Graphics.DrawImage(Vesi, vesix, korY[i], 129, 100); //Vesikalık*/
                        say++;
                        sinifsayaç++;
                    }
                    e.Graphics.DrawLine(Cizgi, vesix-26, korY[i] - 5.5f, vesix+132, korY[i] - 5.5f); //Resimin Sol Yanındaki
                    e.Graphics.DrawLine(Cizgi, vesix - 26, korY[i] - 7.9f, vesix - 26, korY[i] + 108); //Resimin Altındaki
                    
                }
                e.Graphics.DrawLine(Cizgi, vesix - 26, korY[4] + 105.5f, vesix + 132, korY[4] + 105.5f); //Resimin SAğ Yanındaki
                vesix -= 160;
            }
            // Alt
            if (sinifsayaç == sinifsayısı[sayaç] && sayaç<sinifsayısı.Count-1)
            {
                sinifsayaç = 0;
                sayaç++;
            }
            vesix = SyGenislik - 270;
            for (int i = 0; i < korY.Length; i++)
            {
                korY[i] = korY[i] + Orta;
            }
            okulY += Orta;
            // Okul Bilgi kısmıın çerçevesi
            e.Graphics.DrawLine(Cizgi, SyGenislik - 137, korY[0] - 5.5f, SyGenislik - 50, korY[0] - 5.5f); //Resimin üstündeki Okul Ad kısmı dikey cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik - 137, korY[4] + 105.5f, SyGenislik - 50, korY[4] + 105.5f); //Resimin üstündeki Okul Ad kısmı dikey cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik - 52.5f, korY[0] - 5.5f, SyGenislik - 52.5f, korY[4] + 105.5f); //Resimin üstündeki Okul Ad kısmı Üst cizgi
            e.Graphics.DrawLine(Cizgi, SyGenislik - 137, korY[0] - 8, SyGenislik - 137, korY[4] + 108); //Resimin üstündeki Okul Ad kısmı alt cizgi
            
            //e.Graphics.DrawLine(Cizgi, SyGenislik - 137, korY[0] - 8, SyGenislik - 137, korY[4] + 108); //Resimin üstündeki cizgi Yanındaki
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (sinifsayaç < sinifsayısı[sayaç])
                    {
                        Vesi = Image.FromFile(Vesikaliklar[say]);
                        Vesi.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        //Okul Bilgi kısmı 
                        e.Graphics.TranslateTransform(SyGenislik - 85, okulY); //Yazı Konum
                        e.Graphics.RotateTransform(90); //Yazı açı
                        e.Graphics.DrawString(OkulAdi, Fnt1, new SolidBrush(Rnk), -okulAdKonum, -10);
                        e.Graphics.DrawString(Siniflar[say], Fnt1, new SolidBrush(Rnk), 0, 11); 
                        e.Graphics.ResetTransform(); //Yazı Açı restleme
                        //Öğrenci isimleri
                        e.Graphics.TranslateTransform(vesix, korY[i]); //Yazı Konum
                        e.Graphics.RotateTransform(90); //Yazı açı
                        e.Graphics.DrawString(Isimler[say], Fnt, new SolidBrush(Rnk), 2, 0);
                        e.Graphics.DrawString(Soyadlar[say], Fnt, new SolidBrush(Rnk), 2, 11); //isimx[i] ekle
                        e.Graphics.ResetTransform(); //Yazı Açı restleme
                        e.Graphics.DrawImage(Vesi, vesix, korY[i], 129, 100); //Vesikalık*/
                        say++;
                        sinifsayaç++;
                    }
                    e.Graphics.DrawLine(Cizgi, vesix - 26, korY[i] - 5.5f, vesix + 132, korY[i] - 5.5f); //Resimin Sol Yanındaki
                    e.Graphics.DrawLine(Cizgi, vesix - 26, korY[i] - 7.9f, vesix - 26, korY[i] + 108); //Resimin Altındaki

                }
                e.Graphics.DrawLine(Cizgi, vesix - 26, korY[4] + 105.5f, vesix + 132, korY[4] + 105.5f); //Resimin SAğ Yanındaki
                vesix -= 160;
            }
            if (sinifsayaç < sinifsayısı[sayaç])
            {
                e.HasMorePages = true;
            }
            else if(sinifsayaç == sinifsayısı[sayaç] && sayaç < sinifsayısı.Count - 1)
            {
                sayaç++;
                sinifsayaç = 0;
                e.HasMorePages = true;
            }
        }
    }
}
