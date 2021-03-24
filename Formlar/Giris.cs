using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Shool_Photo.Formlar
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        SQLiteConnection SQLiteBaglan = new SQLiteConnection("Data Source = Bilgiler.db");
        string versiyonBilgisi = "10";
        private void GuncellemeKontrol()
        {
            try
            {
                WebRequest istek = HttpWebRequest.Create("https://ekibyazilim.com/guncelleme/versiyon.php");
                WebResponse yanit = istek.GetResponse();
                StreamReader veri = new StreamReader(yanit.GetResponseStream());
                string sonVersiyon = veri.ReadToEnd();
                sonVersiyon = sonVersiyon.Replace("<p>", "");
                sonVersiyon = sonVersiyon.Replace("</p>", "");
                sonVersiyon.Trim();

                if (versiyonBilgisi != sonVersiyon)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(Application.StartupPath + "\\Guncelleme\\Guncelleme Kontrol.exe");
                    processStartInfo.Verb = "runas";
                    using (Process process = new Process())
                    {
                        process.StartInfo = processStartInfo;
                        process.Start();
                    }
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Giris_Load(object sender, EventArgs e)
        {
            GuncellemeKontrol();
            txtEposta.Focus();
            using (var Komut = new SQLiteCommand("Select Mail,Sifre from Tbl_Kullanici", SQLiteBaglan))
            {
                SQLiteBaglan.Open();
                using (var Veri = Komut.ExecuteReader())
                {
                    if (Veri.Read())
                    {
                        txtEposta.Text = Veri["Mail"].ToString();
                        txtSifre.Text = Veri["Sifre"].ToString();
                    }
                }
                SQLiteBaglan.Close();
            }
        }
        
        private string MD5Sifrele(string sifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in dizi)
                sb.Append(ba.ToString("x2").ToLower());

            return sb.ToString();
        }

        private void btnGir_Click(object sender, EventArgs e)
        {
            string Mail, Sifre,OrMail,OrSifre;
            OrMail = txtEposta.Text;
            OrSifre = txtSifre.Text;
            Mail = MD5Sifrele(OrMail);
            Sifre = MD5Sifrele(OrSifre);

            try
            {
                using (var Komut = SQLiteBaglan.CreateCommand())
                {
                    Komut.CommandText = "Select * from Tbl_Kullanici Where Mail=@Mail and Sifre=@Sifre";
                    Komut.Parameters.AddWithValue("@Mail", OrMail);
                    Komut.Parameters.AddWithValue("@Sifre", OrSifre);
                    SQLiteBaglan.Open();
                    using (var Veri = Komut.ExecuteReader())
                    {
                        if (Veri.Read())
                        {
                            
                            using (var SQLiteKomut = new SQLiteCommand(SQLiteBaglan))
                            {
                                SQLiteKomut.CommandText = "Update Tbl_Kullanici set Mail=@Mail, Sifre=@Sifre";
                                SQLiteKomut.Parameters.AddWithValue("@Mail", "");
                                SQLiteKomut.Parameters.AddWithValue("@Sifre", "");
                                SQLiteKomut.Parameters["@Mail"].Value = OrMail;
                                SQLiteKomut.Parameters["@Sifre"].Value = OrSifre;
                                SQLiteKomut.ExecuteNonQuery();
                                AnaSayfa anaSayfa = new AnaSayfa();
                                this.Hide();
                                anaSayfa.Show();
                            }

                        }
                        else
                            MessageBox.Show("Mailiniz yada Şifreniz hatalıdır Lütfen kontrol edin.");
                    }
                    SQLiteBaglan.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
