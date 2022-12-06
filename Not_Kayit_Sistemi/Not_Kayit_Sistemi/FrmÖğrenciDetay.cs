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
    public partial class FrmÖğrenciDetay : Form
    {
        public FrmÖğrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-7KPL2B4\MSSQLSERVERRRR;Initial Catalog=DbNotKayit;Integrated Security=True");
        

        private void FrmÖğrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From TBLDERS where OGRNUMARA=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",numara);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text=dr[2].ToString()+" "+dr[3].ToString();
                LblS1.Text = dr[4].ToString();
                LblS2.Text = dr[5].ToString();
                LblS3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                if (Convert.ToInt32(dr[7])>=50)
                {
                    LblDurum.Text = "Geçti";
                }
                else
                    LblDurum.Text = "Kaldı";

                

            }


        }
    }
}
