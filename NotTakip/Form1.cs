using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // MSSQL için gerekli

namespace NotTakip
{
    public partial class GirisEkran : Form
    {
        public GirisEkran()
        {
            InitializeComponent();
        }

        // Giriş butonu
        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciID = txtID.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            string connectionString = "Server=MONSTER\\SQLEXPRESS;Database=NotTakip;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Admin kontrolü
                    string adminSorgu = "SELECT * FROM Adminler WHERE AdminID = @id AND Sifre = @sifre";
                    using (SqlCommand cmd = new SqlCommand(adminSorgu, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", kullaniciID);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string kullaniciTipi = reader["KullaniciTipi"].ToString();

                                MessageBox.Show("Giriş başarılı. Kullanıcı tipi: " + kullaniciTipi);

                                if (kullaniciTipi == "Admin")
                                {
                                    YonetimPanel yonetimPanel = new YonetimPanel();
                                    yonetimPanel.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Yetkisiz kullanıcı türü.");
                                }
                                return; // Admin bulunduysa devam etme
                            }
                        }
                    }

                    // Öğrenci kontrolü (Numara ile)
                    string ogrenciSorgu = "SELECT * FROM Ogrenciler WHERE Numara = @numara AND Sifre = @sifre";
                    using (SqlCommand cmd = new SqlCommand(ogrenciSorgu, connection))
                    {
                        cmd.Parameters.AddWithValue("@numara", kullaniciID);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string adSoyad = reader["AdSoyad"].ToString();
                                string numara = reader["Numara"].ToString(); // Artık OgrenciID değil, Numara gönderiyoruz

                                MessageBox.Show("Hoş geldiniz, " + adSoyad);

                                OgrenciPanel ogrenciPanel = new OgrenciPanel(numara, adSoyad); // Numara gönderiliyor
                                ogrenciPanel.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("ID veya şifre hatalı.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
