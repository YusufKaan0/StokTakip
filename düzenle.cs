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

namespace babam_is
{
    public partial class düzenle : Form
    {
        public düzenle()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=KAAN;Initial Catalog=DBStokTakip;Integrated Security=True;Encrypt=False");
        public void VeriGörme()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Hareket", con);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
        }

        private void düzenle_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            VeriGörme();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Hareket set kalantutar = kalantutar - @p1 where partikodu= @p2 and aciklama = @p3",con);
            cmd.Parameters.AddWithValue("@p1",textBox2.Text);
            cmd.Parameters.AddWithValue("@p2",textBox3.Text);
            cmd.Parameters.AddWithValue("@p3",textBox4.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Düzenleme işlemi yapıldı.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox1.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }
    }
}
