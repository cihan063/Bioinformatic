using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            read_file("C:\\Users\\cihan\\source\\repos\\WinFormsApp4\\seq1.txt");
            read_file("C:\\Users\\cihan\\source\\repos\\WinFormsApp4\\seq2.txt");

            int match = Convert.ToInt32(textBox1.Text);
            int mismatch = Convert.ToInt32(textBox2.Text);
            int gap = Convert.ToInt32(textBox3.Text);

            String n1str = listBox1.Items[1].ToString();
            String n2str = listBox1.Items[3].ToString();

            string[] words1 = n1str.Split(" ");
            string[] words1_1 = new string[words1.Length + 2];
            string[] words2 = n2str.Split(" ");
            int word_dif = words1.Length - words2.Length;
            string[] words2_1 = new string[words2.Length +1 ];

            DataTable table = new DataTable();

            for (int i = 0; i < words1_1.Length; i++)
            {
                table.Columns.Add(i.ToString(), typeof(string));

            }
            table.Rows.Add(words1_1);

            for (int i = 0; i < words1_1.Length; i++)
            {
                if (i <= 1)
                {
                    words1_1[i] = "-";
                }
                else
                {
                    words1_1[i] = words1[i - 2];
                }
            }

            for (int i = 0; i < words2_1.Length; i++)
            {
                if (i < word_dif)
                {
                    words2_1[i] = "-";
                }
                else
                {
                    words2_1[i] = words2[i - word_dif];
                }
            }

            string[,] twodim = new string[words2_1.Length +1 ,  words1_1.Length];
            /*for (int i = 0; i < words2_1.Length; i++)
            {
                twodim[i + 1, 0] = words2_1[i];
            }*/
            int tmp_sayi = 0;
            for (int i = 0; i < words2_1.Length + 1; i++)
            {
                for (int j = 0; j < words1_1.Length ; j++)
                {
                    if (i==0)
                    {
                        twodim[i, j] = words1_1[j];
                    }
                    else if (i>0 && j==0)
                    {
                        twodim[i, j] = words2_1[i - 1];
                    }
                    else
                    {
                        if (i==1 && j==1)
                        {
                            twodim[i, j] = "0";
                        }
                        if (i==1 && j!=1)
                        {
                            tmp_sayi = Convert.ToInt32(twodim[i, j - 1]);
                            tmp_sayi = tmp_sayi + gap;
                            if (tmp_sayi < 0)
                            {
                                tmp_sayi = 0;
                            }
                            twodim[i, j] = tmp_sayi.ToString();
                        }
                        if (i>1 && j==1)
                        {
                            tmp_sayi = Convert.ToInt32(twodim[i-1, j ]);
                            tmp_sayi = tmp_sayi + gap;
                            if (tmp_sayi<0)
                            {
                                tmp_sayi = 0;
                            }
                            twodim[i, j] = tmp_sayi.ToString();
                        }
                        
                        
                    }
                }
            }
            int tmp1;
            int tmp2;
            int tmp3;
            for (int i = 0; i < words2.Length; i++)
            {
                for (int j = 0; j < words1.Length; j++)
                {
                    if (words2[i]==words1[j])
                    {
                        tmp1 = Convert.ToInt32(twodim[i + 1, j + 1]) + match;
                        tmp2 = Convert.ToInt32(twodim[i + 1, j + 2]) + gap;
                        tmp3 = Convert.ToInt32(twodim[i + 2, j + 1]) + gap;
                        tmp_sayi = Math.Max(Math.Max(tmp2, tmp3), tmp1);
                        if (tmp_sayi <0)
                        {
                            tmp_sayi = 0;
                        }
                        twodim[i + 2, j + 2] = tmp_sayi.ToString();
                    }
                    if (words2[i] != words1[j])
                    {
                        tmp1 = Convert.ToInt32(twodim[i + 1, j + 1]) + mismatch;
                        tmp2 = Convert.ToInt32(twodim[i + 1, j + 2]) + gap;
                        tmp3 = Convert.ToInt32(twodim[i + 2, j + 1]) + gap;
                        tmp_sayi = Math.Max(Math.Max(tmp2, tmp3), tmp1);
                        if (tmp_sayi < 0)
                        {
                            tmp_sayi = 0;
                        }
                        twodim[i + 2, j + 2] = tmp_sayi.ToString();
                    }
                }
            }

            //[words2_1.Length +1 ,  words1_1.Length];
            string[] tmp_array = new string[words1_1.Length];
            for (int i = 0; i < words2_1.Length+1; i++)
            {
                for (int j = 0; j < words1_1.Length; j++)
                {
                    if (j<words1_1.Length-1)
                    {
                        tmp_array[j] = twodim[i, j];
                    }
                    else
                    {
                        tmp_array[j] = twodim[i, j];
                        table.Rows.Add(tmp_array);
                    }
                }
            }
            

            dataGridView1.DataSource = table;


            int lenX = twodim.GetLength(0)-1;
            int lenY = twodim.GetLength(1)-1;
            int[,] twodim1 = new int[lenX, lenY];
            int[] tmp_int_array = new int[lenY];
            for (int i = 1; i < lenX+1; i++)
            {
                for (int j = 1; j < lenY+1; j++)
                {
                    twodim1[i - 1, j - 1] = Convert.ToInt32(twodim[i, j]);
                }
            }

            int tmp_max_value;
            int tmp_max_index;
            int k = 1;
            int tmp_total;
            int tmp_ind_val;
            int tmp_global_total = 0;
            for (int i = lenX-1; i > 0; i--)
            {
                k = 0;
                for (int j = 0; j < lenY; j++)
                {   
                    
                    if (j==lenY-1)
                    {
                        tmp_int_array[j] = twodim1[i, j];
                        tmp_max_value = tmp_int_array.Max();
                        tmp_max_index = tmp_int_array.ToList().IndexOf(tmp_max_value);
                        //label4.Text = tmp_max_index.ToString();
                        //label5.Text = tmp_max_value.ToString();
                        // Bir while loop patlattık mı iş biter Cihan :D 
                        tmp_total = tmp_max_value;
                        if (tmp_total != 0)
                        {
                            while (twodim1[i-k, j-k] >0)
                            {
                                tmp_ind_val = twodim1[i - k, j - k];
                                tmp_total += tmp_ind_val;
                                k = k + 1;
                                if (tmp_global_total == 0 || tmp_global_total < tmp_total)
                                {
                                    tmp_global_total = tmp_total - tmp_max_value;
                                }
                               
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        tmp_int_array[j] = twodim1[i, j];
                    }
                    
                }
                
            }
            label5.Text = tmp_global_total.ToString();
            watch.Stop();
            label6.Text = watch.Elapsed.ToString();
        }
        public void read_file(string path)
        {
            StreamReader sr = File.OpenText(path);
            string text;
            while ((text = sr.ReadLine()) != null)
            {
                listBox1.Items.Add(text);
            }
            sr.Close();
        }
    }
}
