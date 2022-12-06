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

namespace Homework3
{
    public partial class Form1 : Form
    {
        List<string[]> matrix;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;

            choofdlog.Multiselect = false;
            choofdlog.ShowDialog();

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                matrix = new List<string[]>();
                string sFileName = choofdlog.FileName;
                string[] readText = File.ReadAllLines(sFileName);
                string[] attributes = readText[0].Split(',');

                foreach (string s in attributes) this.dataGridView1.Columns.Add(s, s);

                for (int i = 2; i < readText.Length; i++)
                {
                    string[] unit = readText[i].Split(',');
                    //DataGridViewRow row = new DataGridViewRow();
                    this.dataGridView1.Rows.Add(unit);
                    matrix.Add(unit);
                    System.Diagnostics.Debug.WriteLine(matrix[0][1]);
                    /*int index=this.dataGridView1.Rows.Add(row);
                    for (int j = 0; j < unit.Length; j++)
                    {
                        dataGridView1.Rows.Cells[j].Value = unit[j];
                    }*/
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count=this.dataGridView1.Rows.Count;
            string[] attributes = { " ", "<50", "<100", "<150", "<200", "<250", "<300", "<350", "<400", "<450", "<500", "<550", "<600" };
            string[] attributes2 = { "<50", "<100", "<150", "<200", "<250", "<300", "<350", "<400"};
            int[] intervals = { 0,50, 100, 150, 200, 250, 300, 350, 400, 450,500,550,600 };
            int[] intervals2 = { 0,50, 100, 150, 200, 250, 300, 350, 400};
            foreach (string s in attributes) this.dataGridView2.Columns.Add(s, s);
            for (int k = 1; k < 9; k++)
            {
                int tot = 0;
                string[] unit = new string[13];
                System.Diagnostics.Debug.WriteLine(attributes2[k - 1]);
                unit[0] = attributes2[k-1];
                for (int j = 1; j < 13; j++)
                {
                    tot = 0;

                    foreach (string[] s in matrix)
                    {

                        if (((int.Parse(s[1]) >= intervals[j - 1] && int.Parse(s[1]) < intervals[j])||(j==0 && int.Parse(s[1]) < intervals[j])) && ((int.Parse(s[2]) >= intervals2[k - 1] && int.Parse(s[2]) < intervals2[k])||(k == 0 && int.Parse(s[2]) < intervals2[k])))
                        {
                            tot++;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine(tot);
                    unit[j] = ((float)tot/count).ToString();
                }
                this.dataGridView2.Rows.Add(unit);

            }
        }
    }
}
