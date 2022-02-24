using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.TBLNOTLAR.Where(x => x.SINAV < 80);
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton2.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.Where(x => x.Ad == "ali");
                dataGridView1.DataSource = degerler.ToList();
            }


            if (radioButton3.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.Where(x => x.Ad == textBox1.Text || x.Soyad == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }


            if (radioButton4.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.Select(x => new { soyadı = x.Soyad });
                dataGridView1.DataSource = degerler.ToList();
            }


            if (radioButton5.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.Select(x => new
                {
                    Ad = x.Ad.ToUpper(),
                    Soyad = x.Soyad.ToLower()
                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton6.Checked == true)
            {
                //adı ali olmayanlr gelsin
                var degerler = db.TBLOGRENCİ.Select(x => new
                {
                    Ad = x.Ad.ToUpper(),
                    Soyad = x.Soyad.ToLower()
                }).Where(x => x.Ad != "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }


            if (radioButton7.Checked == true)
            {
                //geçti mi kaldı mı
                var degerler = db.TBLNOTLAR.Select(x => new
                {
                    OgrebciAdı = x.OGR,
                    Ortalamsı = x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = degerler.ToList();
            }



            if (radioButton8.Checked == true)
            {
                //iki tabloyu birleştirip istenilen değeri çektik
                //x notlar tablosunu, y öğrenci tablosunu ifade eder.
                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCİ.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.Ad,
                    x.ORTALAMA,
                    //Durum=x.DURUM== true ? "Geçti" : "Kaldı" //burada geçti kaldıyı ekledik
                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton9.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
             
            }


            if (radioButton10.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = degerler.ToList();

            }


            if (radioButton11.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.OrderBy(x => x.Ad);
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton12.Checked == true)
            {
                var degerler = db.TBLOGRENCİ.OrderBy(x => x.ID).Skip(5);
                dataGridView1.DataSource = degerler.ToList();

            }



        }
    }
}
