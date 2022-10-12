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

namespace Homework2_csv_parser
{
    public partial class Form1 : Form
    {
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
                string sFileName = choofdlog.FileName;
                string[] readText = File.ReadAllLines(sFileName);
                string[] attributes= readText[0].Split(',');

                foreach (string s in attributes)this.dataGridView1.Columns.Add(s, s);

                for (int i=2;i< readText.Length;i++)
                {
                    string[] unit = readText[i].Split(',');
                    //DataGridViewRow row = new DataGridViewRow();
                    this.dataGridView1.Rows.Add(unit);
                    /*int index=this.dataGridView1.Rows.Add(row);
                    for (int j = 0; j < unit.Length; j++)
                    {
                        dataGridView1.Rows.Cells[j].Value = unit[j];
                    }*/
                }

            }
        }
    }
}
