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

namespace Homework_6
{
    public partial class Form1 : Form
    {
        LinkedList<int> data;
        LinkedList<int[]> samples;
        LinkedList<double> means;
        LinkedList<double> variances;
        double real_mean;
        double real_variance;
        int max_mean;
        double max_variance;

        Bitmap b;
        Graphics g;
        bool drag;
        bool resizing;
        bool scroll;
        Rectangle r;
        int r_width;
        int r_height;
        int x_mouse;
        int y_mouse;
        int x_down;
        int y_down;
        bool mean_plot;
        bool variance_plot;
        readonly double ScaleFactor = 0.1d;
        public Form1()
        {
           
            InitializeComponent();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (r.Contains(e.X, e.Y))
            {
                y_mouse = e.Y;
                x_mouse = e.X;
                y_down = r.Y;
                x_down = r.X;
                r_height = r.Height;
                r_width = r.Width;
                if (e.Button == MouseButtons.Left) drag = true;
                else if (e.Button == MouseButtons.Right) resizing = true;


            }
            
            

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            resizing = false;
        }
        private void editableRectangle_Zoom(object sender, MouseEventArgs e)
        {
            if (r.Contains(e.X, e.Y))
            {
                x_down = r.X;
                y_down = r.Y;
                int delta = (int)((e.Delta * ScaleFactor));
                //double ratio = r.Height / r.Width;
                r.Width += delta;
                r.Height += delta;
                r.X -= (int)delta / 2;
                r.Y -= (int)delta / 2;
                redraw();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int delta_x = e.X - x_mouse;
            int delta_y = e.Y - y_mouse;
            if (drag)
            {

                r.X = x_down + delta_x;
                r.Y = y_down + delta_y;

                redraw();
            }
            else if (resizing)
            {

                r.Width = r_width + delta_x;
                r.Height = r_height + delta_y;
                redraw();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            data = new LinkedList<int>();

            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;

            choofdlog.Multiselect = false;
            choofdlog.ShowDialog();

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                string[] readText = File.ReadAllLines(sFileName);
                int sum=0;
                max_mean = 0;
                for (int i = 2; i < readText.Length; i++)
                {
                    string[] unit = readText[i].Split(',');
                    //DataGridViewRow row = new DataGridViewRow();
                    data.AddFirst(int.Parse(unit[1]));
                    sum += int.Parse(unit[1]);
                    if (int.Parse(unit[1]) > max_mean) max_mean = int.Parse(unit[1]);

                    /*int index=this.dataGridView1.Rows.Add(row);
                    for (int j = 0; j < unit.Length; j++)
                    {
                        dataGridView1.Rows.Cells[j].Value = unit[j];
                    }*/
                }
                real_mean = sum / (readText.Length - 2);
                double aux = 0;
                for (int i = 2; i < readText.Length; i++)
                {
                    string[] unit = readText[i].Split(',');
                    aux += Math.Pow((double)(real_mean - double.Parse(unit[1])),2);
                }
                
                real_variance =aux  / (readText.Length - 2);

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            samples = new LinkedList<int[]>();
            means = new LinkedList<double>();
            variances = new LinkedList<double>();
            for (int i = 0; i < 100; i++)
            {
                int[] s = sample(20, random);

                samples.AddFirst(s);
                double mean = s.Average();

                means.AddFirst(mean);
                double sum = 0;
                foreach (double v in s)
                {
                    sum += Math.Pow(v - mean, 2);
                }
                double variance = sum / s.Length;
                System.Console.WriteLine(variance);
                if (variance > max_variance) max_variance = variance;
                variances.AddFirst(variance);

            }
            this.richTextBox1.Text = "Real mean: " + real_mean.ToString();
            this.richTextBox1.Text += "\nSample mean: " + means.Average().ToString();
            this.richTextBox1.Text += "\nReal variance: " + real_variance.ToString();
            this.richTextBox1.Text += "\nSample variance: " + variances.Average().ToString();


            drag = false;
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            pictureBox1.Image = b;
            mean_plot = false;
            variance_plot = false;
            r = new Rectangle(20, 20, 400, 300);
            g.DrawRectangle(Pens.Black, r);



        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            mean_plot = true;
            variance_plot = false;
            g.DrawRectangle(Pens.Black, r);
            DrawGraph(r, means, max_mean, b);
            richTextBox2.Text="Mean: "+means.Average().ToString();
            double avg = means.Average();
            double sum = 0;
            foreach (double mean in means)
            {
                sum+=Math.Pow(mean - avg, 2);
            }
            richTextBox2.Text += "\nVariance: " + sum / 100;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            mean_plot = false;
            variance_plot = true;
            g.DrawRectangle(Pens.Black, r);
            DrawGraph(r, variances, max_variance, b);

            richTextBox2.Text = "Mean: " + variances.Average().ToString();
            double avg = variances.Average();
            double sum = 0;
            foreach (double variance in variances)
            {
                sum += Math.Pow(variance - avg, 2);
            }
            richTextBox2.Text += "\nVariance: " + sum / 100;

        }
        private int[] sample(int n, Random random)
        {
            int[] sample = new int[n];

            for (int i = 0; i < n; i++)
            {
                int random_index = random.Next(488);
                sample[i] = data.ElementAt(random_index);

            }
            return sample;
        }
        private void DrawGraph(Rectangle r, LinkedList<double> results, double max_ratio, Bitmap b)
        {
            Bitmap b2 = b;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in results)
            {
                array[(int)Math.Round((1 - result / max_ratio) * 47)]++;
            }
            int max = 0;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }

            for (int i = 0; i < array.Length; i++)
            {
                int height = (int)array[i] * (r.Height) / max;
                Rectangle r_aux = new Rectangle((int)r.X + (i) * (r.Width) / 48, (r.Height - height) + (r.Y), (int)(r.Width) / 48, height);
                g2.DrawRectangle(Pens.Blue, r_aux);
                g2.FillRectangle(Brushes.Blue, r_aux);
                if (i % 8 == 0)
                {
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height+4);
                    g2.DrawString(Math.Round(max_ratio*i/48,2).ToString(), new Font("calibri", 10), Brushes.Black, r.X-5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max_ratio.ToString(), new Font("calibri", 10), Brushes.Black, r.X-5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X +r.Width, r.Y + r.Height + 4);

            g2.DrawString(max.ToString(),new Font("calibri",10),Brushes.Black,r.X-20,r.Y-5);
            
            g2.DrawRectangle(Pens.Black, r);

        }
        private void redraw()
        {
            g.Clear(Color.White);
            if (mean_plot) DrawGraph(r, means, max_mean, b);
            else if (variance_plot) DrawGraph(r, variances, max_variance, b);
            else
            {
                g.DrawRectangle(Pens.Black, r);
                pictureBox1.Image = b;
            }

        }
    }
}
