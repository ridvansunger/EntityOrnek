using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void btnDersListesi_Click(object sender, EventArgs e)
        {
            //adonet kodu
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-TSR38CT\MSSQLSERVER1;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select*from tbldersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
            //entity kodu

            dataGridView1.DataSource = db.TBLOGRENCİ.ToList();

            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;



        }

        private void btnNotListesi_Click(object sender, EventArgs e)
        {
            //linq sorgu
            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NotId,
                            //diğer tablodan değer aldık join gibi
                            //item.TBLOGRENCİ.Ad,
                            //item.TBLOGRENCİ.Soyad,
                            //item.TBLDERSLER.DersAd,
                            item.OGR,
                            item.Ders,
                            item.SINAV,
                            item.SINAV1,
                            item.SINAV2,
                            item.ORTALAMA,
                            item.DURUM
                        };
            dataGridView1.DataSource = query.ToList();
        }


        //ogrenci ekleme
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCİ t = new TBLOGRENCİ();
            t.Ad = txtAd.Text;
            t.Soyad = txtSoyad.Text;
            db.TBLOGRENCİ.Add(t);//ogrenci nesnesi verdik
            db.SaveChanges();//değişiklikleri kaydet
            MessageBox.Show("Öğrenci listeye eklenmiştir.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TBLDERSLER ders = new TBLDERSLER();
            ders.DersAd = txtDersAd.Text;
            db.TBLDERSLER.Add(ders);
            db.SaveChanges();
            MessageBox.Show("Ders Listeye eklenmiştir.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //texte yazdıpımız ıd yi sildik
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);

            db.TBLOGRENCİ.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silinmiştir.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);

            x.Ad = txtAd.Text;
            x.Soyad = txtSoyad.Text;
            x.Fotograf = txtFoto.Text;
            db.SaveChanges();

            MessageBox.Show("Öğrenci Bilgisi Güncellenmiştir.");
        }

        private void btnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Sp_Not_Listesi();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            //where komutu ile sorgulama yaptık
            dataGridView1.DataSource = db.TBLOGRENCİ.Where(x => x.Ad == txtAd.Text && x.Soyad == txtSoyad.Text).ToList();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            //texte yazana göre arama
            string aranan = txtAd.Text;
            var sorgu = from t0 in db.TBLOGRENCİ
                        where t0.Ad.Contains(aranan)
                        select t0;
            dataGridView1.DataSource = sorgu.ToList();

        }

        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            //Asc
            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCİ> liste1 = db.TBLOGRENCİ.OrderBy(p => p.Ad).ToList();
                dataGridView1.DataSource = liste1;
            }

            //Desc
            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCİ> liste2 = db.TBLOGRENCİ.OrderByDescending(p => p.Ad).ToList();
                dataGridView1.DataSource = liste2;
            }

            //Belli sayıdakileri sıralama
            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCİ> liste3 = db.TBLOGRENCİ.OrderBy(p => p.Ad).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }

            if (radioButton4.Checked == true)
            {
                //ıd 5 olanı getir dedik
                List<TBLOGRENCİ> liste4 = db.TBLOGRENCİ.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }

            if (radioButton5.Checked == true)
            {
                //adı a ile başlaualar
                List<TBLOGRENCİ> liste5 = db.TBLOGRENCİ.Where(p => p.Ad.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }

            if (radioButton6.Checked == true)
            {
                //adı a ile bbitenler
                List<TBLOGRENCİ> liste6 = db.TBLOGRENCİ.Where(p => p.Ad.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }


            if (radioButton7.Checked == true)
            {
                //değer var mı
                bool deger = db.TBLOGRENCİ.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCİ.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (radioButton9.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV);
                MessageBox.Show("Toplam Sınav1 puanı " + toplam.ToString());
            }

            if (radioButton10.Checked == true)
            {
                //sınav1 ortalama
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV);

                MessageBox.Show("1. Sınav ortalama " + ortalama.ToString());

            }

            if (radioButton11.Checked == true)
            {
                //sınav1 ortalamasından yüksek olanlar gelsin
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV);

                List<TBLNOTLAR> liste7 = db.TBLNOTLAR.Where(p => p.SINAV > ortalama).ToList();
                dataGridView1.DataSource = liste7;

            }

            if (radioButton12.Checked == true)
            {
                //sınavdan en yüksek puan
                var maxPuan = db.TBLNOTLAR.Max(p => p.SINAV);
                MessageBox.Show("En yüksek puan =>" + maxPuan.ToString());
            }

            if (radioButton13.Checked == true)
            {
                //en düşük puan
                var minPuan = db.TBLNOTLAR.Min(p => p.SINAV);
                MessageBox.Show("En düşük sınav => " + minPuan.ToString());
            }

            if (radioButton14.Checked == true)
            {
                var not = db.TBLNOTLAR.Where(x => x.Ders == 1).Max(p => p.SINAV);//en yüksek puanı alanı bulduk
                var ogrenci = from item in db.TBLNOTLAR
                              where item.SINAV == not
                              select new
                              {
                                  item.TBLOGRENCİ.Ad,
                                  item.TBLOGRENCİ.Soyad
                              };

                dataGridView1.DataSource = ogrenci.ToList();


            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from t0 in db.TBLNOTLAR
                        join t1 in db.TBLOGRENCİ
                        on t0.OGR equals t1.ID
                        join t2 in db.TBLDERSLER
                        on t0.Ders equals t2.DersID
                        select new
                        {
                            Öğrenci=t1.Ad+" "+t1.Soyad,
                            Ders=t2.DersAd,
                            //Soyad=t1.Soyad,
                            sınav1=t0.SINAV,
                            sınav2=t0.SINAV1,
                            sınav3=t0.SINAV2,
                            ortalama=t0.ORTALAMA

                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
