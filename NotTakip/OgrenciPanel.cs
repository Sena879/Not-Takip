using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotTakip
{
    
    public partial class OgrenciPanel : Form
    {
        private string numara;
        private string adSoyad;

        public OgrenciPanel(string numara, string adSoyad)
        {
            InitializeComponent();
            this.numara = numara;
            this.adSoyad = adSoyad;
        }

        private void OgrenciPanel_Load(object sender, EventArgs e)
        {
            lblkarsiama.Text = $"Hoş geldin, {adSoyad}";

            string query = @"
        SELECT d.DersAdi, n.Vize, n.Final, n.Ortalama
        FROM Notlar n
        JOIN Ogrenciler o ON n.OgrenciID = o.OgrenciID
        JOIN Dersler d ON n.DersID = d.DersID
        WHERE o.Numara = @numara";

            SqlParameter param = new SqlParameter("@numara", numara);
            DataTable dt = DatabaseHelper.ExecuteQuery(query, param);

            lstnotlar.Items.Clear();

            // Başlık satırı (tablo gibi)
            lstnotlar.Items.Add(string.Format("{0,-25} {1,-6} {2,-6} {3,-8}", "Ders Adı", "Vize", "Final", "Ortalama"));
            lstnotlar.Items.Add(new string('-', 50)); // çizgi ayırıcı

            foreach (DataRow row in dt.Rows)
            {
                string dersAdi = row["DersAdi"].ToString();
                string vize = row["Vize"].ToString();
                string final = row["Final"].ToString();
                string ortalama = row["Ortalama"].ToString();

                string satir = string.Format("{0,-25} {1,-6} {2,-6} {3,-8}", dersAdi, vize, final, ortalama);
                lstnotlar.Items.Add(satir);
            }
        }
    }
}
