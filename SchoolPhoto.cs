using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows.Forms;



namespace Shool_Photo
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Enables cross procesesing
        }
        SQLiteConnection Baglan = new SQLiteConnection("Data Source=Bilgiler.db");
        private void PtbEokul_Click(object sender, EventArgs e)
        {
            Baglan.Dispose();
            Formlar.EOkul eOkul = new Formlar.EOkul();
            eOkul.Show(this);
            this.Hide();
        }


        private void PtbKartBaski_Click(object sender, EventArgs e)
        {
            Baglan.Open();
            using (var Komut=new SQLiteCommand("Select Sinif from Tbl_Ogrenciler  Where IsResim='H' Group by Sinif", Baglan))
            {
                using (var Veri = Komut.ExecuteReader())
                {
                    string Mj ="Croplanıp programa yüklemeyen vesikalıklar var. Yüklenecek Sınıf/Sınıflar:";
                    int i = 0;
                    while (Veri.Read())
                    {
                        Mj += " " + Veri["Sinif"].ToString();
                        i++;
                    }
                    Baglan.Close();
                    if (i==0)
                    {
                        Baglan.Dispose();
                        Formlar.KartBaski kartBaski = new Formlar.KartBaski();
                        kartBaski.Show(this);
                        this.Hide();
                    }
                    else
                        MessageBox.Show(Mj, "Vesikalık Ekle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PtbAlbum_Click(object sender, EventArgs e)
        {
            Baglan.Dispose();
            Formlar.Album album = new Formlar.Album();
            album.ShowDialog(this);
            //this.Hide();
        }
        private void AnaSayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Baglan.Dispose();
            Application.Exit();
        }
        private void PtbVesikalik_Click(object sender, EventArgs e)
        {
            string[] Parca, Vesikalik;
            string KayitYolu, Kayit, klasorAdi, No, Uzantı;
            KayitYolu = Application.StartupPath + "\\Vesikaliklar";
            MessageBox.Show("Lütfen Crop ladığınız sınıfı seçiniz.", "Crop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Parca = folderBrowserDialog1.SelectedPath.Split('\\');
                klasorAdi = Parca[(Parca.Length - 1)]; //Klasör Adını Bulmak için
                Vesikalik = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                Uzantı = Path.GetExtension(Vesikalik[0]);
                KayitYolu = KayitYolu + "\\" + klasorAdi;
                Directory.CreateDirectory(KayitYolu); //Seçilen klasöre Yeni klasör açma
                for (int i = 0; i < Vesikalik.Length; i++) // Veritabanında işlem yapmak için numaraları aldım ve kopyalamak için yollarını aldık
                {
                    Parca = Vesikalik[i].Split('\\');
                    Kayit = Parca[(Parca.Length - 1)];
                    Parca = Kayit.Split(' ');
                    No = Parca[0];
                    File.Copy(Vesikalik[i], KayitYolu + "\\" + No + Uzantı, true);
                    SqlVesikalik(No, (KayitYolu + "\\" + No + Uzantı));
                }
                MessageBox.Show("İşleminiz başarıyla gerçekleşmiştir.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btn_Sablon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen kartın ön tarafaını seçiniz.", "Kartın ön tarafı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            openFileDialog1.Title = "Şablon Dosyası Seçiniz.";
            openFileDialog1.RestoreDirectory = true;//Seçilen son dosyayı gösterir
            openFileDialog1.Filter = "Sablon Dosyası |*.jpg";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(openFileDialog1.FileName, Application.StartupPath + "\\Dosyalar\\On.jpg", true);
                MessageBox.Show("Lütfen kartın arka tarafaını seçiniz.", "Kartın arka tarafı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    File.Copy(openFileDialog1.FileName, Application.StartupPath + "\\Dosyalar\\Arka.jpg", true);
            }
        }
        private void SqlVesikalik(string no, string yol)
        {
            try
            {
                using (var Komut=Baglan.CreateCommand())
                {
                    Komut.CommandText= "Update Tbl_Ogrenciler set Vesikalik=@Yol,IsResim='E' Where No=@No";
                    Komut.Parameters.AddWithValue("@Yol", yol);
                    Komut.Parameters.AddWithValue("@No", no);
                    Baglan.Open();
                    Komut.ExecuteNonQuery();
                    Baglan.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
        }

        private void ptbResizer_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                File.Copy(Application.StartupPath + "\\Dosyalar\\High Quality.rar",folderBrowserDialog1.SelectedPath+ "\\High Quality.rar",true);
            }
        }

        private void PtbBmpToJpeg_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(Application.StartupPath + "\\Dosyalar\\bmptojpg.bat", folderBrowserDialog1.SelectedPath + "\\bmptojpg.bat", true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formlar.Ayarlar ayarlar = new Formlar.Ayarlar();
            ayarlar.Show(this);
            this.Hide();
        }
    }
}