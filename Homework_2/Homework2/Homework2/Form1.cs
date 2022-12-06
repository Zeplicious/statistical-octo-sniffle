using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace Homework2
{
    public partial class Form1 : Form
    {
        List<double> list = new List<double>();
        int i1 = 0;
        int i2 = 0;
        int i3 = 0;
        int i4 = 0;
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        int i8 = 0;
        int i9 = 0;
        int i0 = 0;
        int total = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.timer1.Enabled)
            {
                this.timer1.Enabled = false;
                this.richTextBox1.AppendText("\n");
                this.richTextBox1.AppendText(list.Average().ToString());
                this.richTextBox1.AppendText("\n");
            }
            else
            {
                this.richTextBox1.ResetText();
                list.Clear();
                i1 = 0;
                i2 = 0;
                i3 = 0;
                i4 = 0;
                i5 = 0;
                i6 = 0;
                i7 = 0;
                i8 = 0;
                i9 = 0;
                i0 = 0;
                total = 0;
                chart1.Series["Series1"].Points.Clear();
                this.timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            double ran = rnd.NextDouble();
            total++;
            if (ran < 0.1)++i0;
            else if (ran < 0.2) ++i1;
            else if (ran < 0.3) ++i2;
            else if (ran < 0.4) ++i3;
            else if (ran < 0.5) ++i4;
            else if (ran < 0.6) ++i5;
            else if (ran < 0.7) ++i6;
            else if (ran < 0.8) ++i7;
            else if (ran < 0.9) ++i8;
            else ++i9;
            setChart();



            this.richTextBox1.AppendText("new: ");
            this.richTextBox1.AppendText(ran.ToString());
            list.Add(ran);
            this.richTextBox1.AppendText("  avg: ");
            this.richTextBox1.AppendText(list.Average().ToString());
            this.richTextBox1.AppendText("\n");
        }
        private void setChart()
        {
            this.richTextBox2.Text = Math.Round((double)i0 * 100 / total, 2).ToString() + "%";
            this.richTextBox3.Text = Math.Round((double)i1 * 100 / total, 2).ToString() + "%";
            this.richTextBox4.Text = Math.Round((double)i2 * 100 / total, 2).ToString() + "%";
            this.richTextBox5.Text = Math.Round((double)i3 * 100 / total, 2).ToString() + "%";
            this.richTextBox6.Text = Math.Round((double)i4 * 100 / total, 2).ToString() + "%";
            this.richTextBox7.Text = Math.Round((double)i5 * 100 / total, 2).ToString() + "%";
            this.richTextBox8.Text = Math.Round((double)i6 * 100 / total, 2).ToString() + "%";
            this.richTextBox9.Text = Math.Round((double)i7 * 100 / total, 2).ToString() + "%";
            this.richTextBox10.Text= Math.Round((double)i8 * 100 / total, 2).ToString() + "%";
            this.richTextBox11.Text= Math.Round((double)i9 * 100 / total, 2).ToString() + "%";
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddXY("0.1", i0 * 1000/total);
            chart1.Series["Series1"].Points.AddXY("0.2", i1 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.3", i2 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.4", i3 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.5", i4 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.6", i5 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.7", i6 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.8", i7 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("0.9", i8 * 1000 / total);
            chart1.Series["Series1"].Points.AddXY("1",i9 * 1000 / total);

        }
    }
}
