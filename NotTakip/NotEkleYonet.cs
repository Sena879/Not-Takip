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
    public partial class NotEkleYonet : Form
    {
        public NotEkleYonet()
        {
            InitializeComponent();
        }

        private void NotlarEkraniHazirla()
        {
            // Öğrenciler
            DataTable ogrenciler = DatabaseHelper.ExecuteQuery("SELECT OgrenciID, AdSoyad FROM Ogrenciler");
            cmbOgrenci.DataSource = ogrenciler;
            cmbOgrenci.DisplayMember = "AdSoyad";
            cmbOgrenci.ValueMember = "OgrenciID";

            // Dersler
            DataTable dersler = DatabaseHelper.ExecuteQuery("SELECT DersID, DersAdi FROM Dersler");
            cmbDers.DataSource = dersler;
            cmbDers.DisplayMember = "DersAdi";
            cmbDers.ValueMember = "DersID";
        }

        private void NotlariYukle()
        {
            string query = @"SELECT NotID, AdSoyad, DersAdi, Vize, Final, Ortalama
                     FROM Notlar n
                     JOIN Ogrenciler o ON n.OgrenciID = o.OgrenciID
                     JOIN Dersler d ON n.DersID = d.DersID";
            dataGridView1.DataSource = DatabaseHelper.ExecuteQuery(query);
        }

        private void NotTemizle()
        {
            cmbOgrenci.SelectedIndex = 0;
            cmbDers.SelectedIndex = 0;
            txtVize.Text = "";
            txtFinal.Text = "";
            lblOrtalama.Text = "";
        }


        private void OrtalamaHesapla()
        {
            if (double.TryParse(txtVize.Text, out double v) && double.TryParse(txtFinal.Text, out double f))
            {
                double ort = Math.Round(v * 0.4 + f * 0.6, 2);
                lblOrtalama.Text = ort.ToString();
            }
        }

        private void NotEkleYonet_Load(object sender, EventArgs e)
        {
            NotlarEkraniHazirla();
            NotlariYukle();

        }

        private void btnNotEkle_Click(object sender, EventArgs e)
        {
            int ogrenciID = Convert.ToInt32(cmbOgrenci.SelectedValue);
            int dersID = Convert.ToInt32(cmbDers.SelectedValue);
            double vize = double.Parse(txtVize.Text);
            double final = double.Parse(txtFinal.Text);
            

            string query = "INSERT INTO Notlar (OgrenciID, DersID, Vize, Final) VALUES (@ogrenciID, @dersID, @vize, @final)";

            SqlParameter[] parameters = new SqlParameter[]
            {
    new SqlParameter("@ogrenciID", ogrenciID),
    new SqlParameter("@dersID", dersID),
    new SqlParameter("@vize", vize),
    new SqlParameter("@final", final)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);

            // Güncel listeyi yeniden yükle
            NotlariYukle();
            // Form elemanlarını temizle
            NotTemizle();

        }

        private void txtVize_TextChanged(object sender, EventArgs e)
        {
            OrtalamaHesapla();
        }

        private void txtFinal_TextChanged(object sender, EventArgs e)
        {
            OrtalamaHesapla();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                cmbOgrenci.Text = row.Cells["AdSoyad"].Value.ToString();
                cmbDers.Text = row.Cells["DersAdi"].Value.ToString();
                txtVize.Text = row.Cells["Vize"].Value.ToString();
                txtFinal.Text = row.Cells["Final"].Value.ToString();
                lblOrtalama.Text = row.Cells["Ortalama"].Value.ToString();
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int notID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["NotID"].Value);
                int ogrenciID = Convert.ToInt32(cmbOgrenci.SelectedValue);
                int dersID = Convert.ToInt32(cmbDers.SelectedValue);
                double vize = double.Parse(txtVize.Text);
                double final = double.Parse(txtFinal.Text);
                // Ortalama'yı hesaplıyoruz ama veritabanına göndermiyoruz çünkü computed column

                string query = "UPDATE Notlar SET OgrenciID = @ogr, DersID = @ders, Vize = @vize, Final = @final WHERE NotID = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
        new SqlParameter("@ogr", ogrenciID),
        new SqlParameter("@ders", dersID),
        new SqlParameter("@vize", vize),
        new SqlParameter("@final", final),
        new SqlParameter("@id", notID)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                NotlariYukle();
                NotTemizle();
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek notu seçin.");
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int notID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["NotID"].Value);

                string query = "DELETE FROM Notlar WHERE NotID = @id";
                SqlParameter param = new SqlParameter("@id", notID);

                DatabaseHelper.ExecuteNonQuery(query, param);
                NotlariYukle();
                NotTemizle();
            }
            else
            {
                MessageBox.Show("Lütfen silinecek notu seçin.");
            }
        }
    }
}
