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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Data.Odbc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace babam_is
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=KAAN;Initial Catalog=DBStokTakip;Integrated Security=True;Encrypt=False");
        DataTable dt;
        DataTable dt2;


        private void button2_Click(object sender, EventArgs e)
        {
            verisilme();
            VeriAlma();
        }

        public void VeriAlma()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Malzeme", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        public void verisilme()
        {
            con.Open();
            SqlCommand komut4 = new SqlCommand("delete from Malzeme where stok = '0'", con);
            komut4.ExecuteNonQuery();
            con.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            VeriGörme();
        }

        public void VeriGörme()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Hareket", con);
            dt2 = new DataTable();
            da.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void filtrele()
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "malzemeadi LIKE'" + textBox7.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            filtrele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Malzeme (malzemetur,malzemeadi,renkkodu,partikodu,stok) values (@e1,@e2,@e3,@e4,@e5)", con);
            komut.Parameters.AddWithValue("@e1", comboBox2.SelectedItem);
            komut.Parameters.AddWithValue("@e2", textBox1.Text);
            komut.Parameters.AddWithValue("@e3", textBox2.Text);
            komut.Parameters.AddWithValue("@e4", textBox3.Text);
            komut.Parameters.AddWithValue("@e5", textBox4.Text);
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();


            MessageBox.Show("Ekleme Yapıldı");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int tutar = Convert.ToInt32(textBox9.Text);
            int alınan = Convert.ToInt32(textBox11.Text);
            int kalan = tutar - alınan;


            SqlCommand komut2 = new SqlCommand("insert into Hareket (aciklama,malzemeadi,partikodu,adet,tutar,alinantutar,kalantutar) values (@s1,@s2,@s3,@s4,@s5,@s6,@s7)", con);
            komut2.Parameters.AddWithValue("@s1", textBox10.Text);
            komut2.Parameters.AddWithValue("@s2", textBox8.Text);
            komut2.Parameters.AddWithValue("@s3", textBox6.Text);
            komut2.Parameters.AddWithValue("@s4", textBox5.Text);
            komut2.Parameters.AddWithValue("@s5", textBox9.Text);
            komut2.Parameters.AddWithValue("@s6", textBox11.Text);
            komut2.Parameters.AddWithValue("@s7", kalan);
            con.Open();
            komut2.ExecuteNonQuery();
            con.Close();


            con.Open();
            SqlCommand komut3 = new SqlCommand("update Malzeme set stok = stok - @p1 where partikodu = @p2", con);
            komut3.Parameters.AddWithValue("@p2", textBox6.Text);
            komut3.Parameters.AddWithValue("@p1", textBox5.Text);
            komut3.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Satış Yapıldı");


        }

        private void button5_Click(object sender, EventArgs e)
        {
            düzenle frm = new düzenle();
            frm.ShowDialog();
            frm.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox8.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox8.Text = "";
            textBox6.Text = "";
            textBox10.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        public void filtre()
        {
            DataView dv2 = dt2.DefaultView;
            dv2.RowFilter = "malzemeadi LIKE'" + textBox12.Text + "%'";
            dataGridView1.DataSource = dv2;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            filtre();
        }

        public void filtre3()
        {
            DataView dv3 = dt2.DefaultView;
            dv3.RowFilter = "partikodu LIKE'" + textBox15.Text.ToString() + "%'";
            dataGridView1.DataSource = dv3;
        }
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            filtre3();
        }
    }
}

