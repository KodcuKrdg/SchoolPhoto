using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.SQLite;

namespace Shool_Photo.Formlar
{
    public partial class EOkul : Form
    {
        public EOkul()
        {
            CheckForIllegalCrossThreadCalls = false; //Enables cross procesesing
            InitializeComponent();
        }
        SQLiteConnection Baglan = new SQLiteConnection("Data Source=Bilgiler.db");
        
        private void EOkul_Load(object sender, EventArgs e)
        {
            bcWrVeriCek.WorkerReportsProgress = true;
            Basla();
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Baglan.Dispose();
            AnaSayfa anaSayfa = new AnaSayfa();
            this.Hide();
            anaSayfa.Show();
        }
        private void EOkul_FormClosed(object sender, FormClosedEventArgs e)
        {
            Baglan.Dispose();
            Application.Exit();
        }
        // Veri tabanında veri varsa comboBox ı doldurcak ve listview de dolcak 
        private void Basla()
        {
            cmbSinif.Items.Clear();
            cmbSinif.Text = "";
            string sorgu = "Select Sinif,OkulAdi from Tbl_Ogrenciler Group By Sinif Order By Id ASC";
            bool gir = false;
            
            try
            {
                using (var Komut = new SQLiteCommand(sorgu, Baglan))
                {
                    Baglan.Open();
                    using (var Veriler = Komut.ExecuteReader())
                    {
                        while (Veriler.Read())
                        {
                            cmbSinif.Items.Add(Veriler["Sinif"]);
                            txtOkulAdi.Text = Veriler["OkulAdi"].ToString();
                            gir = true;
                        }
                        if (gir)
                        {
                            cmbSinif.Enabled = true;
                            btnDuzenle.Enabled = true;
                            button1.Enabled = true;
                            txtOkulAdi.Enabled = true;
                            txbSinif.Enabled = true;
                            btn_Ac.Text = "Bilgileri Yenile";
                        }
                    }

                    Baglan.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally
            {
                if (gir)
                    cmbSinif.SelectedIndex = 0; //ComboBox da ilk indexi seçtik ve ComboBox in changed event ini tetikledik
            }
        }
        private void OkulAdiEkle(object sender,EventArgs e)
        {
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = false;
            string Sql = "Update Tbl_Ogrenciler Set OkulAdi='" + txtOkulAdi.Text.ToString() + "'";
            using (var Komut=new SQLiteCommand(Sql,Baglan))
            {
                Baglan.Open();
                Komut.ExecuteNonQuery();
                Baglan.Close();
            }
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = true;
            MessageBox.Show("İşleminiz başarıyla gerçekleşmiştir.","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        //---------------------------------------------------------------->Excelden Veri Çekme/İşleme/Sqlite<-----------------------------------------------------------------------


        /// <summary>
        /// Excel Dosyasını seçme ve veri çekmeyi başlatma işleri
        /// </summary>
        string ExcelDosyasi;
        private void Excel_Ac(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Excel Dosyası Seçiniz.";
            openFileDialog1.RestoreDirectory = true;//Seçilen son dosyayı gösterir
            openFileDialog1.Filter = "Excel Dosyası |*.xls| Excel Dosyası|*.xlsx";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = false;
                VerileriSilme();
                //Directory.Delete(Application.StartupPath + "\\Vesikaliklar\\", true); // Klasördeki vesikalıklar varsa silmek için
                ExcelDosyasi = openFileDialog1.FileName;
                bcWrVeriCek.RunWorkerAsync();
            }
        }
        private void VerileriSilme()
        {
            
            try
            {
                if (btn_Ac.Text!="Excel Seç")
                {
                    cmbSinif.Items.Clear();
                    cmbSinif.Text = "";
                    txbSinif.Clear();
                    string sor = "Delete from Tbl_Ogrenciler";
                    using (var Komut = new SQLiteCommand(sor,Baglan))
                    {
                        Baglan.Open();
                        Komut.ExecuteNonQuery();
                        Baglan.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Hata: " + Ex.Message);
                throw;
            }
        }

        //**********>Veri Çekimi
        /// <summary>
        /// Excelden  Verileri çektik işledik ve değişkelere atayıp listViewde gösterdik.
        /// Sınıfın bittiğinide E sütununda sınıfın kız mevcudun yazıyor o sıraya gelip gelmediğini algılatıp geldiyse Değişkenin içine "Sınıf" yazdırdık
        /// Excelden Verileri çektikten sonra Not Defterine Kaydettik
        /// </summary>
        /// <summary>
        /// Exceldeki verileri almak için gerek duyduğumuz değişkenler
        /// </summary>
        
        List<string> Adlar = new List<string>();
        List<string> Soyadlar = new List<string>();
        List<string> Numara = new List<string>();
        List<string> Siniflar = new List<string>();
        List<string> klasor = new List<string>();
        private void bcWrVeriCek_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();   //Yeni Bir Excel Nesnesi Oluşturuldu
            ExcelApp.Workbooks.Open(ExcelDosyasi);//Excel Dosyasını aktif ediyor
            ExcelApp.Visible = false;
            Adlar.Clear();
            Soyadlar.Clear();
            Numara.Clear();
            txtOkulAdi.Text = txbSinif.Text = "";
            lstvTablo.Items.Clear();// Veriler Parça Parşça geldiğinden her veri eklendiğinde ListView İ temizledik
            string Gelen; // Satırda sayı varsa demekki sınıf bitmiştir o yüzden kulandık
            int i = 0,x=1;
            try
            {
                while (true)
                {
                    if (Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("E") + Convert.ToString(i + 2)).Value) != null)
                    {
                        i++;
                    }
                    else
                    {
                        progressBar1.Maximum = i;
                        i = 0;
                        break;
                    }
                }
                SQLiteCommand Komut = Baglan.CreateCommand();
                Baglan.Open();
                SQLiteTransaction Islem = Baglan.BeginTransaction();
                Komut.CommandText= "INSERT OR IGNORE INTO Tbl_Ogrenciler (No,Sira, Ad, Soyad, Sinif,IkiIsim) Values(@Numara,@Sira,@Adlar,@Soyadlar,@Sinif,@IkiIsim)";
                Komut.Parameters.AddWithValue("@Numara", "");
                Komut.Parameters.AddWithValue("@Sira", "");
                Komut.Parameters.AddWithValue("@Adlar", "");
                Komut.Parameters.AddWithValue("@Soyadlar", "");
                Komut.Parameters.AddWithValue("@Sinif", "");
                Komut.Parameters.AddWithValue("@IkiIsim", "");
                int sira=1;
                char IkiIsim = 'H';
                while (true)
                {
                    // "E" Sütunundan bir değer olup olmadığını kotrol ediyoruz eğer E sütununda değer bittiyse artık döngü duruyor 
                    if (Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("E") + Convert.ToString(i + 2)).Value) != null)
                    {
                        Gelen = Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("E") + Convert.ToString(i + 2)).Value).Substring(0, 1); //Excelden gelen verinin ilk harfini aldık
                                                                                                                                                     // E Sunutunda sınıf bitince  sınıftaki kız sayısı yazıyor onu kulanarak diğer sınıfa geçtiğini anlıyoruz
                        if (Gelen == "0" | Gelen == "1" | Gelen == "2" | Gelen == "3" | Gelen == "4" | Gelen == "5" | Gelen == "6" | Gelen == "7" | Gelen == "8" | Gelen == "9")
                        {
                            Adlar.Add("Sınıf");
                            Soyadlar.Add("Sınıf");
                            Numara.Add("Sınıf");
                            //ComboBoxlar
                            cmbSinif.Items.Add(x.ToString());
                            i++; // Bir alt satıra geçirdik
                            x++;
                            sira = 1;
                        }

                        //E sütununda değer varsa bilgiyi çekip işlicek
                        if (Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("E") + Convert.ToString(i + 2)).Value) != null)
                        {
                            Numara.Add(Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("B") + Convert.ToString(i + 2)).Value)); //Verileri Aldık
                            Adlar.Add(Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("E") + Convert.ToString(i + 2)).Value)); //Verileri Aldık
                            Soyadlar.Add(Convert.ToString(ExcelApp.ActiveSheet.Range(Convert.ToString("J") + Convert.ToString(i + 2)).Value)); //Verileri Aldık

                            //Eğer iki yada daha fazla isimliyse Veri Tabanına 'E' ekli,yoruzki Ayarlar formunda hangi öğrenci iki isimli onu anlamak için
                            if ( Adlar[i].IndexOf(' ') >= 0)
                                IkiIsim = 'E';
                            else
                                IkiIsim = 'H';
                            //Tablo
                            string[] Bilgiler = { Numara[i], Adlar[i], Soyadlar[i], x.ToString() };
                            ListViewItem Veriler = new ListViewItem(Bilgiler);//^ Verileri ListView E ekledik
                            lstvTablo.Items.Add(Veriler);

                            //Sqlite
                            Komut.Parameters["@Numara"].Value = Numara[i];
                            Komut.Parameters["@Sira"].Value = sira;
                            Komut.Parameters["@Adlar"].Value = Adlar[i];
                            Komut.Parameters["@Soyadlar"].Value = Soyadlar[i];
                            Komut.Parameters["@Sinif"].Value = x.ToString();
                            Komut.Parameters["@IkiIsim"].Value = IkiIsim;
                            Komut.ExecuteNonQuery();
                            i++;
                            sira++;
                        }
                        bcWrVeriCek.ReportProgress(i);
                    }
                    else
                        break;
                }
                Islem.Commit();
                Komut.Dispose();
                Baglan.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally
            {
                try
                {
                    btn_Ac.Text = "Bilgileri Yenile";
                    ExcelApp.Workbooks.Close(); //Açık olna Excel kitaplarını kapatır
                    ExcelApp.Quit();
                    System.GC.Collect();//Verileri bellketen siler
                    ExcelApp = null;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex);
                    throw;
                }
            }
        }
        private void BcWrVeriCek_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void BcWrVeriCek_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            Directory.CreateDirectory(Application.StartupPath + "\\Vesikaliklar"); //Sildikten sonra Klasörü oluşturduk 
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = true;
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Excelden veri çekme işi bittikten sonra Exceli kapatma ve  Açtığmız Belleği Silmek için Kullandık
        /// Verileri Sqlite ekleme
        /// </summary>
        /// <summary>
        /// Veritqabanındaki Sınıf değerlerini gruplayıp cmbSiniflara ekliyor
        /// </summary>
        private void SqlSiniflar()
        {
            cmbSinif.Items.Clear();
            cmbSinif.Text = "";
            klasor.Clear();
            string sorgu = "Select Sinif from Tbl_Ogrenciler Group By Sinif Order By Id ASC";
            try
            {
                using (var Komut = new SQLiteCommand(sorgu, Baglan))
                {
                    Baglan.Open();
                    using (var VeriOku=Komut.ExecuteReader())
                    {
                        while (VeriOku.Read())
                        {
                            cmbSinif.Items.Add(VeriOku["Sinif"]);
                            klasor.Add(VeriOku["Sinif"].ToString());
                        }
                    }
                    Baglan.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
                throw;
            }
            finally
            {
                grpAdlandir.Enabled = grpOkulAdi.Enabled = true;
            }
        }
        private void TabloDuzen(string Veri)
        {
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = false;
            lstvTablo.Items.Clear();
            string sorgu = "Select No,Ad,Soyad,Sinif from Tbl_Ogrenciler Where Sinif='" + Veri + "'";
            try
            {
                using (var Kmt = new SQLiteCommand(sorgu, Baglan))
                {
                    Baglan.Open();
                    using (var VeriOku = Kmt.ExecuteReader())
                    {
                        while (VeriOku.Read())
                        {
                            Numara.Add(VeriOku["No"].ToString());
                            Adlar.Add(VeriOku["Ad"].ToString());
                            Soyadlar.Add(VeriOku["Soyad"].ToString());
                            string[] Bilgiler = { VeriOku["No"].ToString(), VeriOku["Ad"].ToString(), VeriOku["Soyad"].ToString(), VeriOku["Sinif"].ToString() };
                            ListViewItem Veriler = new ListViewItem(Bilgiler);//^ Verileri ListView E ekledik
                            lstvTablo.Items.Add(Veriler);
                        }
                    }
                    Baglan.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.Message);
            }
            finally
            {
                btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = true;
            }
        }
        private void CmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabloDuzen(cmbSinif.SelectedItem.ToString());
        }

        private void Duzenle(object sener, EventArgs e)
        {
            bool gir = true;
            for (int i = 0; i < cmbSinif.Items.Count; i++)
            {
                if (txbSinif.Text == cmbSinif.Items[i].ToString())
                {
                    gir = false;
                    break;
                }
            }
            if (gir)
            {
                btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = false;
                string veri0 = "", veri1 = "";
                veri0 = txbSinif.Text.ToString();
                veri1 = cmbSinif.SelectedItem.ToString();
                string sorgu;
                sorgu = "Update Tbl_Ogrenciler set Sinif='" + veri0 + "',IsSinifAd='E' Where Sinif='" + veri1 + "'";
                try
                {
                    Baglan.Open();
                    using (var Kmt = new SQLiteCommand(sorgu, Baglan))
                    {
                        Kmt.ExecuteNonQuery();
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
                    SqlSiniflar();
                    cmbSinif.Text = veri0;
                    TabloDuzen(veri0);
                    btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = true;
                }
            }
            else
                MessageBox.Show(txbSinif.Text + " Sınıfı bulunmaktadır. Lütfen sınıf listenizi kontrol edin.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        List<string> Vesikaliklar = new List<string>();
        private void btnSiniflandir_Click(object sender, EventArgs e)
        {
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = false;
            string sql = "Select IsSinifAd from Tbl_Ogrenciler Where IsSinifAd='H'";
            Baglan.Open();
            using (var Kmt= new SQLiteCommand(sql, Baglan))
            {
                if (!Kmt.ExecuteReader().Read())
                {
                    Baglan.Close();
                    Siniflandir();
                }
                else
                {
                    Baglan.Close();
                    MessageBox.Show("İsimleri düzeltilmemiş sınıflar mevcuttur!", "Sınıf Düzeltmesi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            btn_Ac.Enabled = button1.Enabled = btnSiniflandir.Enabled = btnDuzenle.Enabled = txtOkulAdi.Enabled = txbSinif.Enabled = cmbSinif.Enabled = true;
        }

        private void Siniflandir()
        {
            string uzanti = "", KayitYolu = "";
            int x = 0;
            Numara.Clear();
            Adlar.Clear();
            Soyadlar.Clear();
            Siniflar.Clear();
            string sorgu = "select Count(Ad) from Tbl_Ogrenciler"; // Kaç Tane öğrenci var
            string sorgu1 = "select No,Ad,Soyad,Sinif from Tbl_Ogrenciler";
            try
            {
                Baglan.Open();
                using (var Komut = new SQLiteCommand(sorgu, Baglan))
                {
                    using (var VeriOku = Komut.ExecuteReader())
                    {
                        if (VeriOku.Read())
                        {
                            x = Convert.ToInt32(VeriOku["Count(Ad)"]);
                        }
                    }
                }
                using (var Kmt1 = new SQLiteCommand(sorgu1, Baglan))
                {
                    using (var VeriOku1 = Kmt1.ExecuteReader())
                    {
                        while (VeriOku1.Read())
                        {
                            Numara.Add(VeriOku1["No"].ToString());
                            Adlar.Add(VeriOku1["Ad"].ToString());
                            Soyadlar.Add(VeriOku1["Soyad"].ToString());
                            Siniflar.Add(VeriOku1["Sinif"].ToString());
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
            SqlSiniflar();
            MessageBox.Show("Okulda " + x.ToString() + " öğrenci bulunmaktadır. Seçeceğiniz klasörde " + x.ToString() + " tane vesikalık olmalıdır.", "Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            folderBrowserDialog1.Description = "Veseikalıklar Klasörünü Seçin";
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Vesikaliklar.Clear();
                Vesikaliklar.AddRange(Directory.GetFiles(folderBrowserDialog1.SelectedPath));
                uzanti = Path.GetExtension(Vesikaliklar[0]);
            }
            if (x != Vesikaliklar.Count)
            {
                if (x > Vesikaliklar.Count)
                    MessageBox.Show("Seçtiğiniz Excell'de " + x.ToString() + " öğrenci var, klasörde ise " + Vesikaliklar.Count.ToString() + " adet vesikalık var." + (x - Vesikaliklar.Count) + " Adet vesikalık eksik.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Seçtiğiniz Excell'de " + x.ToString() + " öğrenci var, klasörde ise " + Vesikaliklar.Count.ToString() + " adet vesikalık var." + (Vesikaliklar.Count - x) + " Adet vesikalık fazla.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("İsim değiştirme ve sınıflara ayırma işlemi başlatılacaktır onaylıyor musunuz?", "İsim Değiştirme Ve Sınıflandırma", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    folderBrowserDialog1.Description = "Kayıt Klasörünü Seçin";
                    folderBrowserDialog1.ShowNewFolderButton = true;
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        int y = 0;
                        KayitYolu = folderBrowserDialog1.SelectedPath;
                        progressBar1.Maximum = Vesikaliklar.Count - 1;
                        Directory.CreateDirectory(KayitYolu + "\\" + klasor[y]); //Seçilen klasöre Yeni klasör açma
                        Directory.CreateDirectory(Application.StartupPath + "\\Vesikaliklar\\" + klasor[y]); //Kök  klasöre Yeni klasör açma
                        //SQLite
                        SQLiteCommand Komut = Baglan.CreateCommand();
                        Baglan.Open();
                        SQLiteTransaction Islem = Baglan.BeginTransaction();
                        Komut.CommandText = "Update Tbl_Ogrenciler set Vesikalik=@Yol Where No=@No";
                        Komut.Parameters.AddWithValue("@Yol", "");
                        Komut.Parameters.AddWithValue("@No", "");
                        string yol="";
                        for (int i = 0; i < Vesikaliklar.Count; i++)
                        {
                            if (Siniflar[i] == klasor[y])
                            {
                                yol = Application.StartupPath + "\\Vesikaliklar" + "\\" + klasor[y] + "\\" + Numara[i] + uzanti;
                                File.Copy(Vesikaliklar[i], yol, true);
                                File.Copy(Vesikaliklar[i], KayitYolu + "\\" + klasor[y] + "\\" + Numara[i] + " " + Adlar[i] + " " + Soyadlar[i] + uzanti, true);
                                
                                //Sqlite
                                Komut.Parameters["@Yol"].Value = yol;
                                Komut.Parameters["@No"].Value = Numara[i];
                                Komut.ExecuteNonQuery();
                                progressBar1.Value = i;
                            }
                            else
                            {
                                y++;
                                if (y < klasor.Count)
                                {
                                    Directory.CreateDirectory(KayitYolu + "\\" + klasor[y]);
                                    Directory.CreateDirectory(Application.StartupPath + "\\Vesikaliklar\\" + klasor[y]); //Kök  klasöre Yeni klasör açma

                                }
                                i--;
                            }
                        }
                        Islem.Commit();
                        Komut.Dispose();
                        Baglan.Close();
                        MessageBox.Show("İşlem Başarı ile gerçekleşmiştir.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("İşlem iptal edilmiştir.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                }
            }
        }
    }
}
