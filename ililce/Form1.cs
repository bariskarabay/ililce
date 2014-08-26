using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ililce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=BARISK; initial catalog=ornekuygulamalar; integrated security=true");
        private void Form1_Load(object sender, EventArgs e)
        {
            doldur();
        }
        public void doldur() 
        {
            {

                try
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("select il_adi from il_tablo", baglanti);
                    SqlDataReader drMemleket = cmd.ExecuteReader();

                    while (drMemleket.Read())
                    {
                        comboBox1.Items.Add(drMemleket["il_adi"]);
                    }
                    drMemleket.Close();
                }
                catch
                {


                }
                finally
                {
                    baglanti.Close();
                }


            }
        }

        public void ilcedoldur() 
        {

            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("ililce", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ilkodu = new SqlParameter("@ilkodu", SqlDbType.Int);
                ilkodu.Direction = ParameterDirection.Input;
                ilkodu.Value = comboBox1.SelectedIndex + 1;
                cmd.Parameters.Add(ilkodu);

                SqlDataReader railce = cmd.ExecuteReader();

                while (railce.Read())
                {
                    comboBox2.Items.Add(railce["ilce_adi"]);
                }
                railce.Close();
            }
            catch
            {

            }
            finally 
            {
                baglanti.Close();
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show(comboBox1.Text +"  ili için  " +comboBox2.Text + "  ilçesi seçildi  ");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            ilcedoldur();
           
        }
    }
}
