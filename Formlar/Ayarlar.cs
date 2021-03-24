using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Shool_Photo.Formlar
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        SQLiteConnection Baglan = new SQLiteConnection("Data Source=Bilgiler.db");
        Color dtgrdvwrRengi;
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            try
            {
                dtgrdvwrRengi = dtGrdView.DefaultCellStyle.SelectionBackColor;
                AcilisVerileri();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                resimYolu = dtGrdView["Vesikalik", 0].Value.ToString();
                Image Resim = Image.FromFile(resimYolu);
                pctboxResim.Image = Resim;

                Id = dtGrdView["Id", 0].Value.ToString();
                sira = dtGrdView["Sira", 0].Value.ToString();
                Numara = dtGrdView["No", 0].Value.ToString();
                sinif = dtGrdView["Sinif", 0].Value.ToString();

                txtNo.Text = Numara;
                txtAd.Text = dtGrdView["Ad", 0].Value.ToString();
                txtSoyad.Text = dtGrdView["Soyad", 0].Value.ToString();
                lblSinif.Text = sinif;
                cmbxSiralama.Text = sira;
            }
        }
        private void Ayarlar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Baglan.Dispose();
        }

        DataTable VeriTablosu = new DataTable();
        List<string> Sinif = new List<string>();
        List<string> Sira = new List<string>();
        string Id, sira, Numara, sinif;
        private void Tablorenglendir()
        {
            for (int i = 0; i < dtGrdView.Rows.Count; i++)
            {
                if (dtGrdView["TabloRengi", i].Value.ToString()!= "Yok")
                {
                    dtGrdView.Rows[i].DefaultCellStyle.BackColor = Color.FromName(dtGrdView["TabloRengi", i].Value.ToString());
                }
                
            }
        }
        private void AcilisVerileri()
        {
            using (var Veriler = new SQLiteDataAdapter("Select * from deneme Order By Sinif, Sira", Baglan))
            {
                try
                {
                    VeriTablosu.Clear();
                    Veriler.Fill(VeriTablosu);
                    dtGrdView.DataSource = VeriTablosu;
                    dtGrdView.Columns[0].Visible = false;
                    for (int i = 6; i <= 12; i++)
                    {
                        dtGrdView.Columns[i].Visible = false;
                    }
                    dtGrdView.Columns[10].Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Baglan.Open();
            // Sinif isimleri
            using (var Veriler = new SQLiteCommand("Select Sinif from deneme Group by Sinif", Baglan))
            {
                cmbxSinif.Items.Clear();
                cmbxAraSinif.Items.Clear();
                Sinif.Clear();
                using (var Oku = Veriler.ExecuteReader())
                {
                    while (Oku.Read())
                    {
                        cmbxSinif.Items.Add(Oku["Sinif"].ToString());
                        cmbxAraSinif.Items.Add(Oku["Sinif"].ToString());
                        Sinif.Add(Oku["Sinif"].ToString());
                    }
                }

            }
            // Sinifin Öğrenci Sayısı
            using (var Veriler = new SQLiteCommand("Select Count(Sinif) from deneme Group by Sinif", Baglan))
            {
                cmbxSiralama.Items.Clear();
                Sira.Clear();
                using (var Oku = Veriler.ExecuteReader())
                {
                    while (Oku.Read())
                    {
                        Sira.Add(Oku["Count(Sinif)"].ToString());
                    }
                    for (int i = 1; i <= int.Parse(Sira[0]); i++)
                    {
                        cmbxSiralama.Items.Add(i);
                    }
                }
            }
            Baglan.Close();
            Thread tabloRenglendir = new Thread(new ThreadStart(Tablorenglendir));
            tabloRenglendir.Start();
        }
        private void BilgileriDoldur()
        {
            resimYolu = dtGrdView.CurrentRow.Cells[6].Value.ToString();
            Image Resim = Image.FromFile(resimYolu);
            pctboxResim.Image = Resim;

            Id = dtGrdView.CurrentRow.Cells[0].Value.ToString();
            sira = dtGrdView.CurrentRow.Cells[1].Value.ToString();
            sinif = dtGrdView.CurrentRow.Cells[5].Value.ToString();
            Numara = dtGrdView.CurrentRow.Cells[2].Value.ToString();


            if (sinif != lblSinif.Text) //farklı bir sınıfa geçiirse sıralama combobox yeniden doldurulacak
            {
                cmbxSinif.Items.Clear();
                for (int i = 0; i < Sinif.Count; i++)
                {
                    if (Sinif[i]!=sinif)
                    {
                        cmbxSinif.Items.Add(Sinif[i]);
                    }
                }
                cmbxSiralama.Items.Clear();
                for (int i = 0; i < Sinif.Count; i++)
                {
                    if (dtGrdView.CurrentRow.Cells[5].Value.ToString() == Sinif[i])
                    {
                        for (int k = 1; k <= int.Parse(Sira[i]); k++)
                        {
                            cmbxSiralama.Items.Add(k);
                        }
                        break;
                    }
                }
            }
            cmbxSinif.Text = "";
            txtNo.Text = Numara;
            txtAd.Text = dtGrdView.CurrentRow.Cells[3].Value.ToString();
            txtSoyad.Text = dtGrdView.CurrentRow.Cells[4].Value.ToString();
            lblSinif.Text = sinif;
            cmbxSiralama.Text = sira;

        }

        private void HangiBilgileriDoldur(int gelen)
        {
            resimYolu = dtGrdView["Vesikalik", gelen].Value.ToString();
            Image Resim = Image.FromFile(resimYolu);
            pctboxResim.Image = Resim;

            Id = dtGrdView["Id", gelen].Value.ToString();
            sira = dtGrdView["Sira", gelen].Value.ToString();
            Numara = dtGrdView["No", gelen].Value.ToString();
            sinif = dtGrdView["Sinif", gelen].Value.ToString();

            txtNo.Text = Numara;
            txtAd.Text = dtGrdView["Ad", gelen].Value.ToString();
            txtSoyad.Text = dtGrdView["Soyad", gelen].Value.ToString();
            lblSinif.Text = sinif;
            cmbxSiralama.Text = sira;

            dtGrdView.CurrentRow.Selected = false; //datagridviewin verilerini yeniden doldurduk ve otomatik ilk satır seçili geldi onu kapatıık
            dtGrdView.FirstDisplayedScrollingRowIndex = HangiOgrenci; //Datagridview deki sırasını buluyor
            dtGrdView.Rows[HangiOgrenci].Selected = true; //En son satırı seçtikki bilgileri doldur seçili satırdan verileri alıyor
        }
        int HangiOgrenci;
        string resimYolu;
        private void dtGrdView_MouseClick(object sender, MouseEventArgs e)
        {
            if (dtGrdView.RowCount != 0)
            {
                HangiOgrenci = dtGrdView.CurrentRow.Index;
                ResimAcisi = 0;
                BilgileriDoldur();
            }
        }
        string yeniSira;
        private void cmbxSinif_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sinif != cmbxSinif.SelectedItem.ToString() & btnOgrenci.Text == "Kaydet")
            {
                yeniSira = (int.Parse(Sira[cmbxSinif.SelectedIndex]) + 1).ToString();
                pctbcDegistir.Enabled = true;
            }
            else
                pctbcDegistir.Enabled = false;
            if (btnOgrenci.Text != "Kaydet")
            {
                EkleSinif = cmbxSinif.SelectedItem.ToString();
            }
        }
        string degisenSira;
        private void cmbxSiralama_SelectedIndexChanged(object sender, EventArgs e)
        {
            degisenSira = cmbxSiralama.SelectedItem.ToString();
            if (degisenSira != sira)
            {
                try
                {
                    if (int.Parse(sira) < int.Parse(degisenSira)) // yeni Siralaması eskisinden yüksekse 
                    {
                        Baglan.Open();
                        using (var Komut = Baglan.CreateCommand())
                        {
                            Komut.CommandText = "Update deneme Set Sira=Sira-1 Where Sinif=@Sinif and Sira>@Sira and Sira<=@degisenSira";
                            Komut.Parameters.AddWithValue("@Sinif", sinif);
                            Komut.Parameters.AddWithValue("@Sira", sira);
                            Komut.Parameters.AddWithValue("@degisenSira", degisenSira);
                            Komut.ExecuteNonQuery();
                        }
                        using (var Komut = Baglan.CreateCommand())
                        {
                            Komut.CommandText = "Update deneme Set Sira=@Sira Where Id=@Id";
                            Komut.Parameters.AddWithValue("@Sira", degisenSira);
                            Komut.Parameters.AddWithValue("@Id", Id);
                            Komut.ExecuteNonQuery();
                        }
                        Baglan.Close();
                    }
                    else
                    {
                        Baglan.Open();
                        using (var Komut = Baglan.CreateCommand())
                        {
                            Komut.CommandText = "Update deneme Set Sira=Sira+1 Where Sinif=@Sinif and Sira>=@degisenSira and Sira<@Sira";
                            Komut.Parameters.AddWithValue("@Sinif", sinif);
                            Komut.Parameters.AddWithValue("@Sira", sira);
                            Komut.Parameters.AddWithValue("@degisenSira", degisenSira);
                            Komut.ExecuteNonQuery();
                        }
                        using (var Komut = Baglan.CreateCommand())
                        {
                            Komut.CommandText = "Update deneme Set Sira=@Sira Where Id=@Id";
                            Komut.Parameters.AddWithValue("@Sira", degisenSira);
                            Komut.Parameters.AddWithValue("@Id", Id);
                            Komut.ExecuteNonQuery();
                        }
                        Baglan.Close();
                    }
                    HangiOgrenci = -1;
                    for (int i = 0; i < Sinif.Count; i++)
                    {
                        if (sinif == Sinif[i]) //öğrencinin sınıfı hangisi ise onun o sınıfta ki değerini alıyoruz
                        {
                            HangiOgrenci += int.Parse(degisenSira);
                            break;
                        }
                        else
                        {
                            HangiOgrenci += int.Parse(Sira[i]);
                        }
                    }
                    AcilisVerileri();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HangiBilgileriDoldur(HangiOgrenci);
                }

            }

        }

        string EkleNo = null;
        string EkleSira = null;
        string EkleAd = null;
        string EkleSoyad = null;
        string EkleSinif = null;
        string EkleVesikalik = null;
        string EkleResim = null;
        private void btnOgrenci_Click(object sender, EventArgs e)
        {
            bool gir = true;
            try
            {
                if (btnOgrenci.Text == "Kaydet")
                {
                    using (var Komut = Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Update deneme Set Ad=@Ad, Soyad=@Soyad Where Id=@Id";
                        Komut.Parameters.AddWithValue("@Ad", txtAd.Text);
                        Komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                        Komut.Parameters.AddWithValue("@Id", Id);
                        Baglan.Open();
                        Komut.ExecuteNonQuery();
                        Baglan.Close();
                    }
                }
                else //Ekleme
                {
                    if (txtNo.Text == "" | txtAd.Text == "" | txtSoyad.Text == "" | txtNo.Text == String.Empty | txtAd.Text == String.Empty | txtSoyad.Text == String.Empty | EkleResim == null | EkleSinif == null)
                    {
                        MessageBox.Show("Resim ve ya girilmesi gereken değerlerin birini(birden fazlasını) girmediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gir = false; //hata alınca veriler silinmesin diye hatanın sebebi eksikleri olduğundan
                    }
                    else
                    {
                        EkleNo = txtNo.Text;
                        EkleAd = txtAd.Text;
                        EkleSoyad = txtSoyad.Text;

                        bool noYok = false;
                        using (var Komut = Baglan.CreateCommand()) // Numara Var mı Yok mu Kontrol
                        {
                            Komut.CommandText = "Select No from deneme Where No=@No";
                            Komut.Parameters.AddWithValue("@No", EkleNo);
                            Baglan.Open();
                            using (var veri = Komut.ExecuteReader())
                            {
                                noYok = !veri.Read();
                            }
                            Baglan.Close();
                        }

                        if (noYok)// okulda böyle bir numara yoksa
                        {
                            HangiOgrenci = 0;
                            for (int i = 0; i < Sinif.Count; i++)
                            {
                                if (EkleSinif == Sinif[i])
                                {
                                    EkleSira = (int.Parse(Sira[i]) + 1).ToString(); //Hangi Öğrenci Yapcılcak
                                    HangiOgrenci += int.Parse(EkleSira);
                                    HangiOgrenci--;
                                    break;
                                }
                                else
                                    HangiOgrenci += int.Parse(Sira[i]);
                            }
                            string uzanti = Path.GetExtension(EkleResim);
                            EkleVesikalik = Application.StartupPath + "/Vesikaliklar/" + EkleSinif + "/" + EkleNo + uzanti;
                            using (var Komut = Baglan.CreateCommand())
                            {
                                Komut.CommandText = "Insert into deneme (No,Sira, Ad, Soyad, Sinif,Vesikalik) Values(@No,@Sira, @Ad,@Soyad,@Sinif,@Vesikalik)";
                                Komut.Parameters.AddWithValue("@No", EkleNo);
                                Komut.Parameters.AddWithValue("@Sira", EkleSira);
                                Komut.Parameters.AddWithValue("@Ad", EkleAd);
                                Komut.Parameters.AddWithValue("@Soyad", EkleSoyad);
                                Komut.Parameters.AddWithValue("@Sinif", EkleSinif);
                                Komut.Parameters.AddWithValue("@Vesikalik", EkleVesikalik);
                                Baglan.Open();
                                Komut.ExecuteNonQuery();
                                Baglan.Close();
                            }
                            File.Copy(EkleResim, EkleVesikalik, true);
                            EkleNo = EkleSira = EkleAd = EkleSoyad = EkleSinif = EkleVesikalik = EkleResim = null;
                            chcbxEkle.Checked = false;
                        }
                        else
                        {
                            MessageBox.Show("Okulda " + EkleNo + " numaralı öğrenci bulunmaktadır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gir = false; //hata alınca veriler silinmesin diye hatanın sebebi eksikleri olduğundan
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (gir)
                {
                    AcilisVerileri();
                    HangiBilgileriDoldur(HangiOgrenci);
                }
            }
        }
        // FiltreNo = "No Like'%'" bu değeri atama sebenimiz and işlemi var ve ilk and işleminden önce bir değer sorgu istiyor 
        string FiltreNo = "No Like'%'", FiltreAd = "", FiltreSoyad = "", FiltreSinif = "",FiltreIkiİsim="";
        private int Ara(string NeredeAranıyor, string ArananKelime)
        {
            string butunFiltre = "",kelime;
            kelime = ArananKelime.TrimEnd().TrimStart(); //başından ve sonundaki boşlukları sildik
            DataView veriyeBak = VeriTablosu.DefaultView;
            switch (NeredeAranıyor)
            {
                case "No":
                    {
                        FiltreNo = "No Like'" + kelime + "%'";
                        break; 
                    }
                case "Ad":
                    {
                        FiltreAd = "and Ad Like'" + kelime + "%'";
                        break;
                    }
                case "Soyad":
                    {
                        FiltreSoyad = "and Soyad Like'" + kelime + "%'";
                        break;
                    }
                case "Sinif":
                    {
                        FiltreSinif = "and Sinif Like'%" + kelime + "%'";
                        break;
                    }
                case "IkiIsim":
                    {
                        FiltreIkiİsim = "and IkiIsim Like'%" + kelime + "%'";
                        break;
                    }
            }
            butunFiltre = FiltreNo + FiltreAd + FiltreSoyad + FiltreSinif+ FiltreIkiİsim;
            veriyeBak.RowFilter = butunFiltre;
            dtGrdView.DataSource = veriyeBak;
            return veriyeBak.Count;
        }

        private void txtbxAraNumara_TextChanged(object sender, EventArgs e)
        {
            
            if (Ara("No",txtbxAraNumara.Text) != 0)
            {
                dtGrdView.Rows[0].Selected = true;
                BilgileriDoldur();
            }
            else
            {
                pctboxResim.Image = null;
                lblSinif.Text = txtNo.Text = "...";
                cmbxSinif.Items.Clear();
                cmbxSiralama.Items.Clear();
                cmbxSinif.Text = cmbxSiralama.Text = "";
                txtAd.Clear();
                txtSoyad.Clear();
            }
        }

        private void txtAraAd_TextChanged(object sender, EventArgs e)
        {
            if (Ara("Ad", txtAraAd.Text) != 0)
            {
                dtGrdView.Rows[0].Selected = true;
                BilgileriDoldur();
            }
            else
            {
                pctboxResim.Image = null;
                lblSinif.Text = txtNo.Text = "...";
                cmbxSinif.Items.Clear();
                cmbxSiralama.Items.Clear();
                cmbxSinif.Text = cmbxSiralama.Text = "";
                txtAd.Clear();
                txtSoyad.Clear();
            }
        }

        private void txtAraSoyad_TextChanged(object sender, EventArgs e)
        {
            if (Ara("Soyad", txtAraSoyad.Text) != 0)
            {
                dtGrdView.Rows[0].Selected = true;
                BilgileriDoldur();
            }
            else
            {
                pctboxResim.Image = null;
                lblSinif.Text = txtNo.Text = "...";
                cmbxSinif.Items.Clear();
                cmbxSinif.Text = cmbxSiralama.Text = "";
                txtAd.Clear();
                txtSoyad.Clear();
            }
        }
        private void cmbxAraSinif_TextChanged(object sender, EventArgs e)
        {
            if (Ara("Sinif", cmbxAraSinif.Text.ToString()) != 0)
            {
                dtGrdView.Rows[0].Selected = true;
                BilgileriDoldur();
            }
            else
            {
                pctboxResim.Image = null;
                lblSinif.Text = txtNo.Text = "...";
                cmbxSinif.Items.Clear();
                cmbxSiralama.Items.Clear();
                cmbxSinif.Text = cmbxSiralama.Text = "";
                txtAd.Clear();
                txtSoyad.Clear();
            }
        }

        private void dtGrdView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) //Ok tuşları
        {
            int i = dtGrdView.CurrentRow.Index;
            if (e.KeyCode.ToString() == "Down")
            {
                if (dtGrdView.RowCount > 1 & (i + 1) < dtGrdView.RowCount) // tablonun sınırlarında klması için
                {
                    HangiOgrenci++;
                    ResimAcisi = 0;
                    HangiBilgileriDoldur(HangiOgrenci);
                }
            }
            else if (e.KeyCode.ToString() == "Up")
            {
                if (dtGrdView.RowCount > 1 & (i - 1) >= 0)
                {
                    HangiOgrenci--;
                    ResimAcisi = 0;
                    HangiBilgileriDoldur(HangiOgrenci);
                }
            }
        }

        private void chcbxEkle_CheckedChanged(object sender, EventArgs e)
        {
            if (btnOgrenci.Text == "Kaydet")
            {
                btnOgrenci.Text = "Ekle";
                pctboxResim.Image = Image.FromFile(Application.StartupPath + "/Dosyalar/OrnekVSK.jpg");
                txtNo.Enabled = true;
                lblSinif.Text = "...";
                txtNo.Clear();
                txtAd.Clear();
                txtSoyad.Clear();
                cmbxSiralama.Text = cmbxSinif.Text = "";
                txtAraAd.Enabled = txtAraSoyad.Enabled = txtbxAraNumara.Enabled = cmbxAraSinif.Enabled = cmbxSiralama.Enabled = pctbcDegistir.Enabled = pctbxSil.Enabled = false;
            }
            else
            {
                btnOgrenci.Text = "Kaydet";
                txtNo.Enabled = false;
                lblSinif.Text = "...";
                txtAraAd.Enabled = txtAraSoyad.Enabled = txtbxAraNumara.Enabled = cmbxAraSinif.Enabled = cmbxSiralama.Enabled = pctbcDegistir.Enabled = pctbxSil.Enabled = true;
                EkleResim = EkleSinif = null;
                BilgileriDoldur();
            }
        }

        private void btnTumu_Click(object sender, EventArgs e)
        {
            dtGrdView.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor=Color.FromName("Red");
            dtGrdView.Rows[0].DefaultCellStyle.BackColor= Color.FromName("Red");
        }
        int ResimAcisi = 0;
        private void pctbxSol_Click(object sender, EventArgs e)
        {
            Image resim = pctboxResim.Image;
            resim.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (ResimAcisi==360) //başa döndüğünde 0 liyoruz ki 90 180 270 bulunsun
                ResimAcisi = 0;
            ResimAcisi += 90;
            pctboxResim.Image = resim;
        }
        private void pctbxSag_Click(object sender, EventArgs e)
        {
            Image resim = pctboxResim.Image;
            resim.RotateFlip(RotateFlipType.Rotate270FlipNone);
            if (ResimAcisi == 0) //başa döndüğünde 360 yapıyoruz ki ki 270 180 90 bulunsun
                ResimAcisi = 360;
            ResimAcisi -= 90;
            pctboxResim.Image = resim;
        }

        private void btnDondurKaydet_Click(object sender, EventArgs e)
        {
            pctboxResim.Image = null;
            //ilerleme.ShowDialog(this);
            bckgrwrkResimleriDondur.RunWorkerAsync();
        }

        private void btnDondurTumuKaydet_Click(object sender, EventArgs e)
        {
            dtGrdView.SelectAll();
        }

            Formlar.Ilerleme ilerleme = new Formlar.Ilerleme();
        private void bckgrwrkResimleriDondur_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch Sure = new Stopwatch();
            //Sure.Start();
            //ilerleme.GostergeMax(50);//dtGrdView.SelectedRows.Count;
            Image resim;
            switch (ResimAcisi)
            {
                case 90:
                    {
                        for (int i = 0; i < dtGrdView.SelectedRows.Count; i++)
                        {

                            resim = Image.FromFile(dtGrdView.SelectedRows[i].Cells[6].Value.ToString());
                            resim.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            resim.Save(Application.StartupPath + "\\Vesikaliklar\\" + dtGrdView.SelectedRows[i].Cells[5].Value.ToString() + "\\" + dtGrdView.SelectedRows[i].Cells[2].Value.ToString() + ".JPG");
                            resim.Dispose();
                            //ilerleme.gosterge.Value = i;
                        }
                        break;
                    }
                case 180:
                    {
                        for (int i = 0; i < dtGrdView.SelectedRows.Count; i++)
                        {
                            resim = Image.FromFile(dtGrdView.SelectedRows[i].Cells[6].Value.ToString());
                            resim.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            resim.Save(Application.StartupPath + "\\Vesikaliklar\\" + dtGrdView.SelectedRows[i].Cells[5].Value.ToString() + "\\" + dtGrdView.SelectedRows[i].Cells[2].Value.ToString() + ".JPG");
                            resim.Dispose();
                            //ilerleme.gosterge.Value = i;

                        }
                        break;
                    }
                case 270:
                    {
                        for (int i = 0; i < dtGrdView.SelectedRows.Count; i++)
                        {
                            resim = Image.FromFile(dtGrdView.SelectedRows[i].Cells[6].Value.ToString());
                            resim.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            resim.Save(Application.StartupPath + "\\Vesikaliklar\\" + dtGrdView.SelectedRows[i].Cells[5].Value.ToString() + "\\" + dtGrdView.SelectedRows[i].Cells[2].Value.ToString() + ".JPG");
                            resim.Dispose();
                            //ilerleme.gosterge.Value = i;

                        }
                        break;
                    }
            }
            //ilerleme.Close();
            //Sure.Stop();
            //MessageBox.Show(Sure.Elapsed.Milliseconds.ToString());
            ResimAcisi = 0;
        }

        /// <summary>
        /// for (int i = 0; i < dtGrdView.SelectedRows.Count; i++) //dataGridViewde kaç tane seçili alan varsa o kadar dönüyor
        ///{
        /// dtGrdView.SelectedRows[i].Cells["3"]
        ///}
        /// datagirdviewde seçilen alanlar sırayla dtGrdView.SelectedRows[0] dtGrdView.SelectedRows[1]  .. gidiyor ve ordan ulaşıyoruz ilk seçili olan ikinci seçi oaln gibi
        /// </summary>
        private void btnKısaltmaGuncelleme_Click(object sender, EventArgs e)
        {
            if (dtGrdView.SelectedRows.Count>0)
            {
                try
                {
                    using (var Komut = Baglan.CreateCommand())
                    {
                        string[] Fazla;
                        string kısAd;
                        Baglan.Open();
                        Komut.CommandText = "Update deneme set Ad=@Ad Where Id=@Id";
                        for (int i = 0; i < dtGrdView.SelectedRows.Count; i++) //dataGridViewde kaç tane seçili alan varsa o kadar dönüyor 
                        { //döngüde hata alıyom ama bulamadım
                            kısAd = dtGrdView.SelectedRows[i].Cells[3].Value.ToString();
                            
                            if (chckIki.Checked)
                            {
                                if (kısAd.IndexOf(' ') > 0)
                                {
                                    Fazla = kısAd.Split(' ');// Birden fazla isimleri böldük
                                    kısAd = "";
                                    for (int k = 0; k < Fazla.Length - 1; k++)
                                    {
                                        kısAd += Fazla[k].Substring(0, 1) + ". ";
                                    }
                                    kısAd += Fazla[Fazla.Length - 1];
                                }
                            }
                            else
                            {
                                if (kısAd.IndexOf(' ') > 0)
                                {
                                    Fazla = kısAd.Split(' ');// Birden fazla isimleri böldük
                                    kısAd = Fazla[0] + " ";
                                    for (int k = 1; k < Fazla.Length; k++)
                                    {
                                        if (Fazla[k].Trim().Length > 0)
                                            kısAd += Fazla[k].Substring(0, 1) + ".";
                                    }
                                }
                            }
                            Komut.Parameters.AddWithValue("@Ad", kısAd);
                            Komut.Parameters.AddWithValue("@Id", dtGrdView.SelectedRows[i].Cells[0].Value.ToString());
                            Komut.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Baglan.Close();
                    HangiOgrenci = dtGrdView.SelectedRows[dtGrdView.SelectedRows.Count - 1].Index;
                    AcilisVerileri();
                    HangiBilgileriDoldur(HangiOgrenci);
                }
            }
        }

        

        bool KisaltGir =true;
        private void chckIlk_CheckedChanged(object sender, EventArgs e)
        {
            if (chckIlk.Checked )
            {
                chckIki.Checked = false;
                if (KisaltGir)
                {
                    dtGrdView.DefaultCellStyle.SelectionBackColor = Color.SaddleBrown;
                    pctboxResim.Image = Image.FromFile(Application.StartupPath + "/Dosyalar/OrnekVSK.jpg");
                    lblSinif.Text = "...";
                    txtNo.Clear();
                    txtAd.Clear();
                    txtSoyad.Clear();
                    cmbxSiralama.Text = cmbxSinif.Text = "";
                    pnlOgrenciAyarı.Enabled = pctboxResim.Enabled = false;
                    Ara("IkiIsim", "E");
                    btnTumu.Enabled = btnKısaltmaGuncelleme.Enabled = true;
                    KisaltGir = false;
                }
            }

            if (!chckIki.Checked & !chckIlk.Checked)
            {
                dtGrdView.DefaultCellStyle.SelectionBackColor = dtgrdvwrRengi;
                pnlOgrenciArama.Enabled = pnlOgrenciAyarı.Enabled = pctboxResim.Enabled = true;
                btnTumu.Enabled = btnKısaltmaGuncelleme.Enabled = false;
                DataView veriyeBak = VeriTablosu.DefaultView;
                Ara("IkiIsim", "");
                dtGrdView.DataSource = veriyeBak;
                BilgileriDoldur();
                KisaltGir = true;
            }
        }

        private void chckIki_CheckedChanged(object sender, EventArgs e)
        {
            if (chckIki.Checked)
            {
                chckIlk.Checked = false;
                if (KisaltGir)
                {
                    dtGrdView.DefaultCellStyle.SelectionBackColor = Color.SaddleBrown;
                    pctboxResim.Image = Image.FromFile(Application.StartupPath + "/Dosyalar/OrnekVSK.jpg");
                    lblSinif.Text = "...";
                    txtNo.Clear();
                    txtAd.Clear();
                    txtSoyad.Clear();
                    cmbxSiralama.Text = cmbxSinif.Text = "";
                    pnlOgrenciAyarı.Enabled = pctboxResim.Enabled = false;
                    Ara("IkiIsim", "E");
                    btnTumu.Enabled = btnKısaltmaGuncelleme.Enabled = true;
                    KisaltGir = false;
                }
            }

            if (!chckIki.Checked & !chckIlk.Checked)
            {
                dtGrdView.DefaultCellStyle.SelectionBackColor = dtgrdvwrRengi;
                pnlOgrenciArama.Enabled = pnlOgrenciAyarı.Enabled = pctboxResim.Enabled = true;
                btnTumu.Enabled = btnKısaltmaGuncelleme.Enabled = false;
                DataView veriyeBak = VeriTablosu.DefaultView;
                Ara("IkiIsim", "");
                dtGrdView.DataSource = veriyeBak;
                BilgileriDoldur();
                KisaltGir = true;
            }
        }

        private void pctboxResim_Click(object sender, EventArgs e)
        {
            string dosyaYolu,dosyaUzantisi;
            if (btnOgrenci.Text == "Kaydet")
            {
                if (DosyaSec.ShowDialog() == DialogResult.OK)
                {
                    pctboxResim.Image = null;
                    dosyaYolu = DosyaSec.FileName;
                    dosyaUzantisi = Path.GetExtension(dosyaYolu);
                    string kayitYolu = Application.StartupPath + "/Vesikaliklar" + "/" + sinif + "/" + Numara + dosyaUzantisi;
                    //File.Delete(resimYolu);
                    File.Copy(dosyaYolu, kayitYolu, true);
                    using (var Komut = Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Update deneme Set Vesikalik=@Vesikalik Where Id=@Id";
                        Komut.Parameters.AddWithValue("@Vesikalik", kayitYolu);
                        Komut.Parameters.AddWithValue("@Id", Id);
                        Baglan.Open();
                        Komut.ExecuteNonQuery();
                        Baglan.Close();
                    }
                    AcilisVerileri();
                    HangiBilgileriDoldur(HangiOgrenci);
                }
            }
            else
            {
                if (DosyaSec.ShowDialog() == DialogResult.OK)
                {
                    EkleResim = DosyaSec.FileName;
                    pctboxResim.Image = Image.FromFile(EkleResim);
                }
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txtNo.Text + " Numaralı öğrenci " + txtAd.Text + " " + txtSoyad.Text + " "+lblSinif.Text+" sınıfından "+ cmbxSinif.SelectedItem.ToString()+ " sınıfına geçecektir.", "Sınıf Değişikliği Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string[] resimİsmi;
                string DegisecekREsim = resimYolu;
                resimİsmi = DegisecekREsim.Split('/');
                string gidilenSinif = cmbxSinif.SelectedItem.ToString();
                string kayitYolu = Application.StartupPath + "\\Vesikaliklar" + "\\" + gidilenSinif + "\\" + resimİsmi[resimİsmi.Length - 1];
                File.Copy(DegisecekREsim, kayitYolu, true);
                try
                {
                    Baglan.Open();
                    using (var Komut = Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Update deneme Set Sira=@Sira, Sinif=@Sinif, Vesikalik=@Vesiklaik Where Id=@Id";
                        Komut.Parameters.AddWithValue("@Sira", yeniSira);
                        Komut.Parameters.AddWithValue("@Sinif", gidilenSinif);
                        Komut.Parameters.AddWithValue("@Vesiklaik", kayitYolu);
                        Komut.Parameters.AddWithValue("@Id", Id);
                        Komut.ExecuteNonQuery();
                    }
                    using (var Komut = Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Update deneme Set Sira=Sira-1 Where Sinif=@Sinif and Sira>@Sira";
                        Komut.Parameters.AddWithValue("@Sinif", sinif);
                        Komut.Parameters.AddWithValue("@Sira", sira);
                        Komut.ExecuteNonQuery();
                    }
                    Baglan.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    pctboxResim.Image = null;
                    AcilisVerileri();
                    HangiOgrenci = 0;
                    for (int i = 0; i < Sinif.Count; i++)
                    {
                        if (gidilenSinif == Sinif[i]) //öğrencinin sınıfı hangisi ise onun o sınıfta ki değerini alıyoruz
                        {
                            HangiOgrenci += int.Parse(Sira[i]);
                            break;
                        }
                        else
                        {
                            HangiOgrenci += int.Parse(Sira[i]);
                        }
                    }
                    HangiOgrenci--;
                    HangiBilgileriDoldur(HangiOgrenci);
                    pctbcDegistir.Enabled = false;
                    File.Delete(DegisecekREsim);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(txtNo.Text+" Numaralı öğrenci "+txtAd.Text+" "+txtSoyad.Text+" silinecektir. Onaylıyor musunuz?","Silme Onayı",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                string silinecekResim = resimYolu;
                pctboxResim.Image = null;
                try
                {
                    Baglan.Open();
                    using (var Komut = new SQLiteCommand("Delete from deneme Where Id=@Id", Baglan))
                    {
                        Komut.Parameters.AddWithValue("@Id", Id);
                        Komut.ExecuteNonQuery();
                    }
                    using (var Komut = Baglan.CreateCommand())
                    {
                        Komut.CommandText = "Update deneme set Sira=Sira-1 Where Sinif=@Sinif and Sira>@Sira";
                        Komut.Parameters.AddWithValue("@Sinif", sinif);
                        Komut.Parameters.AddWithValue("@Sira", sira);
                        Komut.ExecuteNonQuery();
                    }
                    Baglan.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    AcilisVerileri();
                    BilgileriDoldur();
                    File.Delete(silinecekResim);
                }
            }
        }
    }
}
