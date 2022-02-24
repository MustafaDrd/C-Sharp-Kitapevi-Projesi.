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

namespace KitapKayıt
{
    public partial class FrmKıtapEvı : Form
    {
        public FrmKıtapEvı()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-7FHN7H38;Initial Catalog=KıtapEvıVeriTabani;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-7FHN7H38;Initial Catalog=KıtapEvıVeriTabani;Integrated Security=True");

        }
        void Lıstele()
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Kitaplar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Turler()
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Turler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CmbTur.ValueMember = "Turİd";
            CmbTur.DisplayMember = "TurAd";
            CmbTur.DataSource = dt;
        }
        private void BtnLıstele_Click(object sender, EventArgs e)
        {
            Lıstele();
            Turler();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
            SqlCommand komutekle = new SqlCommand("insert into Tbl_Kitaplar(KitapAd,Yazar,Sayfa,Fiyat,YayınEvi,Tur) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komutekle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutekle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutekle.Parameters.AddWithValue("@p4", TxtFıyat.Text);
            komutekle.Parameters.AddWithValue("@p5", TxtYayınevi.Text);
            komutekle.Parameters.AddWithValue("@p6", CmbTur.SelectedValue);
            komutekle.ExecuteNonQuery();
           
            baglanti.Close();
            MessageBox.Show("Bilgiler Veri Tabanına Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Lıstele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtİd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtFıyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtYayınevi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void BtnSıl_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Kitaplar where Kitapİd=@p1", baglanti);
            komutsil.Parameters.AddWithValue("@p1", Txtİd.Text);
            komutsil.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanından Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Lıstele();
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Kitaplar set KitapAd=@p1,Yazar=@p2,Sayfa=@p3,Fiyat=@p4,YayınEvi=@p5,Tur=@p6 where Kitapİd=@p7",baglanti);
            komutguncelle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komutguncelle.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komutguncelle.Parameters.AddWithValue("@p4", TxtFıyat.Text);
            komutguncelle.Parameters.AddWithValue("@p5", TxtYayınevi.Text);
            komutguncelle.Parameters.AddWithValue("@p6", CmbTur.SelectedValue);
            komutguncelle.Parameters.AddWithValue("@p7", Txtİd.Text);
            komutguncelle.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitap Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Lıstele();
        }
    }
}