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

namespace Not_Kayit_Sistemi
{
    public partial class FrmOğretmenDetay : Form
    {
        public FrmOğretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-7KPL2B4\MSSQLSERVERRRR;Initial Catalog=DbNotKayit;Integrated Security=True");
        private void FrmOğretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }
        int toplamkisi;
        private void BtnOgrenciKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS(OGRNUMARA,OGRAD,OGRSOYAD,DURUM)values (@P1,@P2,@P3,@P4)",baglanti );
            komut.Parameters.AddWithValue("@P1", MskNumara.Text);
            komut.Parameters.AddWithValue("@P2", TxtAd.Text);
            komut.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P4","False");
            //kişi sayası
            toplamkisi=dataGridView1.Rows.Count-1;
           // MessageBox.Show(toplamkisi.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            toplamkisi = dataGridView1.Rows.Count - 1;

            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            MskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtS1.Text=dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtS2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtS3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            toplamkisi = dataGridView1.Rows.Count-1;
            double ortlama, s1, s2, s3;
            string durum;
            int gecenler=0, kalanlar=0;
            s1=Convert.ToDouble(TxtS1.Text);
            s2 = Convert.ToDouble(TxtS2.Text);
            s3 = Convert.ToDouble(TxtS3.Text);

            ortlama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text=ortlama.ToString();

            if(ortlama>=50)
            {
                durum = "True";
                
            }
            else
            {
                durum = "False";
                
            }
            
            
            

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6",baglanti);
            komut.Parameters.AddWithValue("@P1",TxtS1.Text);
            komut.Parameters.AddWithValue("@P2", TxtS2.Text);
            komut.Parameters.AddWithValue("@P3", TxtS3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");

            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
            for (int i = 0; i < toplamkisi; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[8].Value)
                {
                    gecenler++;
                }


            }
            kalanlar = toplamkisi - gecenler;
            LblGecenSayisi.Text = gecenler.ToString();
            LblKalanSayisi.Text = kalanlar.ToString();
        }
    }
}
//geçen ile kalanları yazdır ve durum geçti ve kaldı yazıcak 