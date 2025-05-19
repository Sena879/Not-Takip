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
    public partial class DersEkleYonet : Form
    {
        public DersEkleYonet()
        {
            InitializeComponent();
        }
        private void DersleriYukle()
        {
            string query = "SELECT * FROM Dersler";
            DerslerDataGrid.DataSource = DatabaseHelper.ExecuteQuery(query);
        }
        private void Temizle()
        {
            txtDersAdi.Text = "";
            txtDersKodu.Text = "";
            txtOgretimElemani.Text = "";
            txtDersAdi.Focus(); // Kullanıcı deneyimi için
        }

        private void DersEkleYonet_Load(object sender, EventArgs e)
        {
            DersleriYukle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Dersler (DersAdi, DersKodu, OgretimElemani) VALUES (@adi, @kodu, @ogretimElemani)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@adi", txtDersAdi.Text),
        new SqlParameter("@kodu", txtDersKodu.Text),
        new SqlParameter("@ogretimElemani", txtOgretimElemani.Text)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
            Temizle();
            DersleriYukle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DerslerDataGrid.SelectedRows.Count > 0)
            {
                int dersID = Convert.ToInt32(DerslerDataGrid.SelectedRows[0].Cells["DersID"].Value);
                string query = "DELETE FROM Dersler WHERE DersID = @id";
                SqlParameter param = new SqlParameter("@id", dersID);

                DatabaseHelper.ExecuteNonQuery(query, param);
                Temizle();
                DersleriYukle();
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.");
            }
        }

        private void DerslerDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DerslerDataGrid.Rows[e.RowIndex];

                txtDersAdi.Text = row.Cells["DersAdi"].Value.ToString();
                txtDersKodu.Text = row.Cells["DersKodu"].Value.ToString();
                txtOgretimElemani.Text = row.Cells["OgretimElemani"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (DerslerDataGrid.SelectedRows.Count > 0)
            {
                int dersID = Convert.ToInt32(DerslerDataGrid.SelectedRows[0].Cells["DersID"].Value);

                string query = "UPDATE Dersler SET DersAdi = @adi, DersKodu = @kodu, OgretimElemani = @ogretimElemani WHERE DersID = @id";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@adi", txtDersAdi.Text),
            new SqlParameter("@kodu", txtDersKodu.Text),
            new SqlParameter("@ogretimElemani", txtOgretimElemani.Text),
            new SqlParameter("@id", dersID)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
                Temizle();
                DersleriYukle();
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir satır seçin.");
            }
        }
    }
}
