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

namespace NotTakip
{
    public partial class OgrenciYonetimPaneli : Form
    {
        public OgrenciYonetimPaneli()
        {
            InitializeComponent();
        }

        string connectionString = "Server=MONSTER\\SQLEXPRESS;Database=NotTakip;Trusted_Connection=True;";

        private void OgrenciYonetimPaneli_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            OgrenciVerileriniGetir();
        }

        private void OgrenciVerileriniGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT OgrenciID, AdSoyad, Numara, Sifre FROM Ogrenciler";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["OgrenciID"].Value.ToString();
                txtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells["AdSoyad"].Value.ToString();
                txtNumara.Text = dataGridView1.Rows[e.RowIndex].Cells["Numara"].Value.ToString();
                txtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells["Sifre"].Value.ToString();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAdSoyad.Text == "" || txtNumara.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Aynı numara var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE Numara = @numara", baglanti);
                kontrolKomut.Parameters.AddWithValue("@numara", txtNumara.Text);
                int sayi = (int)kontrolKomut.ExecuteScalar();

                if (sayi > 0)
                {
                    MessageBox.Show("Bu numaraya sahip bir öğrenci zaten var.");
                    return;
                }

                SqlCommand komut = new SqlCommand("INSERT INTO Ogrenciler (AdSoyad, Numara, Sifre) VALUES (@ad, @numara, @sifre)", baglanti);
                komut.Parameters.AddWithValue("@ad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@numara", txtNumara.Text);
                komut.Parameters.AddWithValue("@sifre", txtSifre.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            OgrenciVerileriniGetir();
            Temizle();
            MessageBox.Show("Öğrenci eklendi.");
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtAdSoyad.Text == "" || txtNumara.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Başka öğrenci bu numarayı kullanıyor mu kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE Numara = @numara AND OgrenciID != @id", baglanti);
                kontrolKomut.Parameters.AddWithValue("@numara", txtNumara.Text);
                kontrolKomut.Parameters.AddWithValue("@id", txtID.Text);
                int sayi = (int)kontrolKomut.ExecuteScalar();

                if (sayi > 0)
                {
                    MessageBox.Show("Bu numara başka bir öğrenciye ait. Güncelleme yapılamaz.");
                    return;
                }

                SqlCommand komut = new SqlCommand("UPDATE Ogrenciler SET AdSoyad=@ad, Numara=@numara, Sifre=@sifre WHERE OgrenciID=@id", baglanti);
                komut.Parameters.AddWithValue("@ad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@numara", txtNumara.Text);
                komut.Parameters.AddWithValue("@sifre", txtSifre.Text);
                komut.Parameters.AddWithValue("@id", txtID.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            OgrenciVerileriniGetir();
            Temizle();
            MessageBox.Show("Öğrenci güncellendi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Lütfen silinecek öğrenciyi seçin.");
                return;
            }

            DialogResult onay = MessageBox.Show("Silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay == DialogResult.No) return;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM Ogrenciler WHERE OgrenciID=@id", baglanti);
                komut.Parameters.AddWithValue("@id", txtID.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            OgrenciVerileriniGetir();
            Temizle();
            MessageBox.Show("Öğrenci silindi.");
        }

        // TextBox'ları temizleyen yardımcı metod
        private void Temizle()
        {
            txtID.Clear();
            txtAdSoyad.Clear();
            txtNumara.Clear();
            txtSifre.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtID.Text = dataGridView1.CurrentRow.Cells["OgrenciID"].Value?.ToString();
                txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["AdSoyad"].Value?.ToString();
                txtNumara.Text = dataGridView1.CurrentRow.Cells["Numara"].Value?.ToString();
                txtSifre.Text = dataGridView1.CurrentRow.Cells["Sifre"].Value?.ToString();
            }
        }
    }
}
