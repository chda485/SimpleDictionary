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

namespace DictionaryTibetion
{
    public partial class Dictionary : Form
    {

        public Dictionary()
        {
            InitializeComponent();
        }
        private string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30";
        
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string Search = String.Format("SELECT Translate FROM Words WHERE Word = '{0}'", textBox1.Text);
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("The word is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(Search, connect);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        textBox2.Clear();
                        reader.Read();
                        string text = Convert.ToString(reader.GetValue(0));
                        textBox2.Text = text;
                    }
                    else
                        MessageBox.Show("This word doesn't exist in a dictionary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connect.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Delete = String.Format("DELETE FROM Words WHERE Word = '{0}'", textBox3.Text);
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("The word is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(Delete, connect);
                    int number = command.ExecuteNonQuery();
                    if (number != 0)
                    {
                        MessageBox.Show("The word deleted!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("This word hasn't already existed in a dictionary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connect.Close();
                }
            }
        }

        private void Insert_button_Click(object sender, EventArgs e)
        {
            string Insert = String.Format("INSERT INTO Words (Word, Translate) VALUES ('{0}','{1}')", textBox4.Text, textBox5.Text);
            if (textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
            {
                MessageBox.Show("The word is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(connection))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand(Insert, connect);
                        int number = command.ExecuteNonQuery();
                        if (number != 0)
                        {
                            MessageBox.Show("The word inserted!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("This word hasn't already existed in a dictionary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connect.Close();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                       MessageBox.Show("This word is already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                       MessageBox.Show("The error if inserting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to clean up the dictionary",
                "Clean up?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string Delete = ("DELETE FROM Words");
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(Delete, connect);
                    int number = command.ExecuteNonQuery();
                    if (number != 0)
                    {
                        MessageBox.Show("It is done!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("The is some error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connect.Close();
                }
            }
        }
    }
}
