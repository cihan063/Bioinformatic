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

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            read_file("C:\\Users\\cihan\\source\\repos\\WinFormsApp2\\nukleotit1.txt");
            read_file("C:\\Users\\cihan\\source\\repos\\WinFormsApp2\\nukleotit2.txt");


            int n1 = Convert.ToInt32(listBox1.Items[0].ToString());
            int n2 = Convert.ToInt32(listBox1.Items[2].ToString());
            String n1str = listBox1.Items[1].ToString();
            String n2str = listBox1.Items[3].ToString();
            /*label1.Text = n1.ToString();
            label2.Text = n1str;
            label3.Text = n2.ToString();
            label4.Text = n2str;*/

            //string someString = "something";
            //String[] stringArray = new String[] { n1str };


            string[] words = n1str.Split(" ");
            string[] words1 = new string[words.Length + 2];
            string[] wordstwo = n2str.Split(" ");
            string[] words2 = new string[wordstwo.Length + 1];
            string[] words1_compare = new string[words.Length + 1];

            for (int i = 0; i < words1.Length; i++)
            {
                if (i <= 1)
                {
                    words1[i] = "-";
                }
                else
                {
                    words1[i] = words[i - 2];
                }
            }

            for (int i = 0; i < words1_compare.Length; i++)
            {
                if (i == 0)
                {
                    words1_compare[i] = "-";
                }
                else
                {
                    words1_compare[i] = words1[i +1];
                }
            }
            for (int i = 0; i < words2.Length; i++)
            {
                if (i == 0)
                {
                    words2[i] = "-";
                }
                else
                {
                    words2[i] = wordstwo[i - 1];
                }
            }

            DataTable table = new DataTable();

            for (int i = 0; i < words1.Length; i++)
            {
                table.Columns.Add(i.ToString(), typeof(string));

            }

            string[,] ikiboyut = new string[n1 + 1, n2 + 2];
            string[,] ikiboyut_yonler = new string[n1 + 1, n2 + 2]; ;
            string[] tmp_array = new string[words1.Length];

            
            int gap = -2;
            int tmp1 = 0;
            int tmp2 = 0;
            int tmp3 = 0;
            int tmp_sayi = 0; 

            table.Rows.Add(words1);

            for (int i = 0; i < n2 + 1; i++)
            {
                for (int j = 0; j < n1 + 2; j++)
                {
                    if (j == 0)
                    {
                        ikiboyut[i, j] = words2[i];
                        ikiboyut_yonler[i,j] = words2[i];

                    }
                    else
                    {
                        if (i==0 && j ==1)
                        {
                            ikiboyut[i, j] = "0";
                            ikiboyut_yonler[i, j] = "0";

                        }
                        if (i==0 && j>1)
                        {
                            tmp_sayi = Convert.ToInt32(ikiboyut[i, j - 1]);
                            tmp_sayi = tmp_sayi + gap;
                            ikiboyut[i, j] = tmp_sayi.ToString();
                            ikiboyut_yonler[i, j] ="left";

                        }
                        if (i>0 && j==1)
                        {
                            tmp_sayi = Convert.ToInt32(ikiboyut[i-1, j]);
                            tmp_sayi = tmp_sayi + gap;
                            ikiboyut[i, j] = tmp_sayi.ToString();
                            ikiboyut_yonler[i, j] = "up";
                            //ikiboyut_yonler[i, j] = tmp_sayi.ToString();

                        }
                        
                    }
                    
                }
            }

            
            

            for (int i = 1; i < words2.Length; i++)
            {
                for (int j = 1; j < words1_compare.Length ; j++)
                {
                    if (words2[i]==words1_compare[j])
                    {
                        tmp1 = (Convert.ToInt32(ikiboyut[i -1, j ]) + 1);
                        tmp2 = (Convert.ToInt32(ikiboyut[i - 1, j+1]) + gap);
                        tmp3 = (Convert.ToInt32(ikiboyut[i, j ]) + gap);
                        tmp_sayi = Math.Max(Math.Max(tmp2, tmp3), tmp1);
                        ikiboyut[i, j+1] = tmp_sayi.ToString();
                        if (tmp_sayi == tmp1)
                        {
                            ikiboyut_yonler[i,j+1] = "diagonal";
                        }
                        if (tmp_sayi == tmp2)
                        {
                            ikiboyut_yonler[i, j + 1] = "up";
                        }
                        if (tmp_sayi == tmp3)
                        {
                            ikiboyut_yonler[i, j + 1] = "left";
                        }

                    }
                    else
                    {
                        tmp1 = (Convert.ToInt32(ikiboyut[i - 1, j]) - 1);
                        tmp2 = (Convert.ToInt32(ikiboyut[i - 1, j + 1]) + gap);
                        tmp3 = (Convert.ToInt32(ikiboyut[i, j]) + gap);
                        tmp_sayi = Math.Max(Math.Max(tmp2, tmp3), tmp1);
                        ikiboyut[i, j + 1] = tmp_sayi.ToString();
                        if (tmp_sayi == tmp1)
                        {
                            ikiboyut_yonler[i, j + 1] = "diagonal";
                        }
                        if (tmp_sayi == tmp2)
                        {
                            ikiboyut_yonler[i, j + 1] = "up";
                        }
                        if (tmp_sayi == tmp3)
                        {
                            ikiboyut_yonler[i, j + 1] = "left";
                        }

                    }
                }
            }
            
            for (int i = 0; i < n1+1; i++)
            {
                for (int j = 0; j < n2+2; j++)
                {
                    if (j<n2+1)
                    {
                        tmp_array[j] = ikiboyut[i, j].ToString();
                    }
                    else
                    {
                        tmp_array[j] = ikiboyut[i, j].ToString();
                        table.Rows.Add(tmp_array);
                    }
                }
            }
            

            dataGridView1.DataSource = table;

            DataTable table2 = new DataTable();
            
            for (int i = 0; i < words1.Length; i++)
            {
                table2.Columns.Add(i.ToString(), typeof(string));

            }
            table2.Rows.Add(words1);
            for (int i = 0; i < n1 + 1; i++)
            {
                for (int j = 0; j < n2 + 2; j++)
                {
                    if (j < n2 + 1)
                    {
                        tmp_array[j] = ikiboyut_yonler[i, j].ToString();
                    }
                    else
                    {
                        tmp_array[j] = ikiboyut_yonler[i, j].ToString();
                        table2.Rows.Add(tmp_array);
                    }
                }
            }
            dataGridView2.DataSource = table2;

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
 