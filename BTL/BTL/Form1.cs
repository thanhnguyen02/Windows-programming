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

namespace BTL
{
    public partial class Form1 : Form
    {
        string chuoiketnoi = "Data Source = LAPTOP-LCVBQ6M6; " 
            + "Initial Catalog = BTLWIN; " + "Integrated Security = True ";
        SqlConnection conn = null;
        SqlConnection connn = null;
        public Form1()
        {
            InitializeComponent();
        }

        void loaddata1()
        {

            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string sql = "Select *from SACH";
            SqlDataAdapter daS = new SqlDataAdapter(sql, conn);
            DataTable dtS = new DataTable();
            daS.Fill(dtS);
            dataGridView1.DataSource = dtS;
            conn.Close();
        }
        void loaddata2()
        {
            connn = new SqlConnection(chuoiketnoi);
            connn.Open();
            string sql = "Select *from CTHOADON";
            SqlDataAdapter daHD = new SqlDataAdapter(sql, connn);
            DataTable dtHD = new DataTable();
            daHD.Fill(dtHD);
            dataGridView2.DataSource = dtHD;
            connn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" || textBox2.Text != "" || textBox1.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                try
                {

                    int sl = Convert.ToInt32(numericUpDown2.Text);
                    try
                    {
                        
                        int gt = Convert.ToInt32(textBox5.Text);
                        conn.Open();
                        string sql_Insert = "Insert into SACH values(N'" + textBox1.Text + "',N'" + textBox2.Text + "'," +
                            "N'" + comboBox1.Text + "',N'" + textBox3.Text + "',N'" + textBox4.Text + "'," +
                            "N'" + textBox5.Text + "',N'" + numericUpDown2.Text + "')";
                        SqlCommand cmd = new SqlCommand(sql_Insert, conn);
                        cmd.ExecuteNonQuery();
                        loaddata1();
                        conn.Close();

                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Gía tiền không phù hợp");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Số lượng không phù hợp");
                }
            }
            else
            {
                MessageBox.Show("Thông tin cần được nhập đủ");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql_Insert = "delete from SACH where MaS= N'" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_Insert, conn);
            cmd.ExecuteNonQuery();
            loaddata1();
            conn.Close ();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            loaddata1();
            loaddata2();
            SqlDataAdapter daTL = new SqlDataAdapter("SELECT *from TL", conn);
            DataTable dtTL = new DataTable();
            daTL.Fill(dtTL);

            SqlDataAdapter daMS = new SqlDataAdapter("SELECT * from SACH", conn);
            DataTable dtMS = new DataTable();
            daMS.Fill(dtMS);

            comboBox1.DataSource = dtTL;
            comboBox1.DisplayMember = "TheLoai";
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = dtTL;
            comboBox2.DisplayMember = "TheLoai";
            comboBox2.SelectedIndex = -1;

            comboBox4.DataSource = dtMS;
            comboBox4.DisplayMember = "MaS";
            comboBox4.SelectedIndex = -1;

            comboBox3.DataSource = dtMS;
            comboBox3.DisplayMember = "TenS";
            comboBox3.SelectedIndex = -1;

            /*comboBox3.DataSource = dtTL;
            comboBox3.DisplayMember = "TheLoai";*/


        }
        int dong;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            dong = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[dong].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dong].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[dong].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[dong].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[dong].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[dong].Cells[5].Value.ToString();
            numericUpDown2.Text = dataGridView1.Rows[dong].Cells[6].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connn.Open();
            //string sql_Insertt = "select SoLuong from SACH where MaS = N'" + comboBox4.Text + "'";
            string sql_Insert = "Insert into CTHOADON values (N'" + dateTimePicker1.Text + "',N'" + comboBox4.Text + "',N'" + comboBox3.Text + "'," +
                "N'" + textBox11.Text + "',N'" + textBox6.Text + "',N'" + numericUpDown1.Text + "',N'" + textBox7.Text + "',N'" + textBox9.Text + "')";
            SqlCommand cmd = new SqlCommand(sql_Insert, connn);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Thành công");
            loaddata2();
            
            connn.Close();


        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
         
                SqlCommand cmd = new SqlCommand("select * from SACH where MaS = N'"+ comboBox4.Text + "'",conn);
                cmd.Parameters.AddWithValue("MaS",comboBox4.Text);
                if(conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    string TheLoai = reader["TheLoai"].ToString();
                    string TenS = reader["TenS"].ToString();
                    string TacGia = reader["TacGia"].ToString();
                    string GiaTien = reader["GiaTien"].ToString();
                  

                    comboBox3.Text = TenS;
                    textBox11.Text = TheLoai;
                    textBox6.Text = TacGia;
                    textBox7.Text = GiaTien;
                    
                }
                
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                
           
        }
        int tonghd=0;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value != 0)
                tonghd= (int)numericUpDown1.Value* Convert.ToInt32(textBox7.Text);
            textBox9.Text=tonghd.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = -1;
            textBox6.Clear();
            comboBox3.SelectedIndex=-1;
            textBox11.Clear();
            textBox7.Clear();
            textBox9.Clear();
            numericUpDown1.Value=0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update SACH set MaS = N'" + textBox1.Text + "',TenS = N'" + textBox2.Text + "'," +
                "TheLoai = N'" + comboBox1.Text + "',TacGia = N'" + textBox3.Text + "',NXB = N'" + textBox4.Text + "'," +
                "GiaTien = N'" + textBox5.Text + "'," +"SoLuong = " + numericUpDown2.Value + " where MaS = N'" + textBox1.Text + "'", conn);
            cmd.ExecuteNonQuery();
            loaddata1();
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (radioButton1.Checked)
            {
                /*conn.Open();
                SqlCommand cmd = new SqlCommand("select * from CTHOADON where NgayBan = '" + dateTimePicker2.Text + "'", conn);
                cmd.ExecuteNonQuery();
                loaddata1();
                conn.Close();*/
                connn = new SqlConnection(chuoiketnoi);
                connn.Open();
                string sql = "select * from CTHOADON where NgayBan = '" + dateTimePicker2.Text + "'";
                SqlDataAdapter daHD = new SqlDataAdapter(sql, connn);
                DataTable dtHD = new DataTable();
                daHD.Fill(dtHD);
                dataGridView2.DataSource = dtHD;
                connn.Close();
            }
            else
            {
                connn = new SqlConnection(chuoiketnoi);
                connn.Open();
                string sql = "select * from CTHOADON where TheLoai = N'" + comboBox2.Text + "'";
                SqlDataAdapter daHD = new SqlDataAdapter(sql, connn);
                DataTable dtHD = new DataTable();
                daHD.Fill(dtHD);
                dataGridView2.DataSource = dtHD;
                connn.Close();
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            loaddata2();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            
            int tongtien = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                tongtien += Convert.ToInt32(dataGridView2.Rows[i].Cells[7].Value);
            }
            textBox8.Text = tongtien.ToString();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* SqlCommand cmd = new SqlCommand("select * from SACH where TenS = N'" + comboBox3.Text + "'", conn);
            cmd.Parameters.AddWithValue("TenS", comboBox3.Text);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string TheLoai = reader["TheLoai"].ToString();
                string MaS = reader["MaS"].ToString();
                string TacGia = reader["TacGia"].ToString();
                string GiaTien = reader["GiaTien"].ToString();


                comboBox4.Text = MaS;
                textBox11.Text = TheLoai;
                textBox6.Text = TacGia;
                textBox7.Text = GiaTien;

            }

            if (conn.State == ConnectionState.Open)
                conn.Close();*/
        }
    }
}
   
        

      





        

