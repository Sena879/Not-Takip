using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotTakip
{
    public partial class YonetimPanel : Form
    {
        public YonetimPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OgrenciYonetimPaneli ogrenciYonetimPaneli = new OgrenciYonetimPaneli();
            ogrenciYonetimPaneli.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DersEkleYonet dersEkleYonet = new DersEkleYonet();
            dersEkleYonet.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NotEkleYonet notEkleYonet = new NotEkleYonet();
            notEkleYonet.ShowDialog();
        }
    }
}
