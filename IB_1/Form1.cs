using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace IB_1
{
    public partial class Form1 : Form
    {

        byte[] Mess_Byte;
        BitArray Bits_messege;
        UInt32[] Hash;
        int[] info_for_graph = new int[80];
        public string messege;
        //Journal journal = new Journal();

        public Form1()
        {
            InitializeComponent();
        }



        private void btn_open_file_Click(object sender, EventArgs e)
        {
           if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Mess_Byte = File.ReadAllBytes(openFileDialog1.FileName);
                    txtbx_byte_form.Text = String.Concat(from M in Mess_Byte select M.ToString("X"));
                    txtbx_path.Text = openFileDialog1.InitialDirectory + openFileDialog1.FileName;

                    Bits_messege = new BitArray(Mess_Byte);
                    RIPEMD320.Reverse_Byte(ref Bits_messege);
                    txtbx_bit_form.Text = String.Concat(from M in Mess_Byte select Convert.ToString(M, 2) + "  ");

                    //label_count_bits.Text += Bits_messege.Count.ToString();
                    //label_value_bit.Text += Bits_messege[0] ? '1' : '0';
                    //numericUpDown1.Value = 0;
                    //numericUpDown1.Maximum = Bits_messege.Count - 1;

                    if (openFileDialog1.FileName.Contains(".txt"))
                        txtbx_text_form.Text = File.ReadAllText(openFileDialog1.FileName);
                    else txtbx_text_form.Text = "";


                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void btn_hash_Click(object sender, EventArgs e)
        {
            var RIPEMD = new RIPEMD320();
            RIPEMD.prepear(Bits_messege);
            Hash = RIPEMD.Hashing();

            txtbx_hash.Text = String.Concat(from H in Hash select H.ToString("X") + "   ");
            //if (checkBox1.Checked && !journal.Contains(Hash))
            //{
            //    journal.add_header(Hash);
            //    journal.add_intermediate(SHA.log_hash);
            //}
        }
    }
}
