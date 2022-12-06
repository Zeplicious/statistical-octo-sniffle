using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Graphics g2;
        public Bitmap b;
        public Bitmap b2;
        Random rnd;
        double ran;
        Point xy1;
        Point xy2;
        double ratio;
        double max_ratio;
        LinkedList<double> results;
        double p;
        int tot_tosses;
        int trials;

        public Form1()
        {
            InitializeComponent();
            rnd = new Random();
            xy1 = new Point();
            xy2 = new Point();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(tot_tosses);
            SetUp();
            int h = pictureBox1.Height-10;
            max_ratio = 1;
            if (sender == radioButton3)
            {
                max_ratio = Math.Sqrt(tot_tosses);
            }

            for (int j = 0; j < trials; j++)
            {
                
                double current_avg = 0;
                int current = 0;
                for (int i = 0; i < tot_tosses; i++)
                {

                    xy1.X = RealX(i);
                    xy1.Y = RealY(current_avg, h, max_ratio);
                    if (sender == radioButton3) {
                        ran = rnd.NextDouble();
                        if (ran >p) current ++;
                        current_avg = current / Math.Sqrt(i + 1);
                    }
                    else
                    {
                        current = 0;
                        ran = rnd.NextDouble();
                        if (ran <= p)
                        {
                            current = 1;
                            
                        }
                        if (sender == radioButton1) current_avg += UpdateRatio(current, tot_tosses, current_avg, 0);
                        else if (sender == radioButton2) current_avg += UpdateRatio(current, i, current_avg, 1);
                        else if (sender == radioButton3) current_avg += UpdateRatio(current, tot_tosses, current_avg, 0) * Math.Sqrt(i + 1);

                    }

                    xy2.X = xy1.X;
                    xy2.Y = RealY(current_avg, h, max_ratio);

                    g.DrawLine(Pens.Black, xy1, xy2);

                    xy1.Y = xy2.Y;
                    xy2.X = RealX(i + 1);
                    g.DrawLine(Pens.Black, xy1, xy2);

                }
                results.AddFirst(current_avg);

            }
            DrawGraph();
        }

 

        private void SetUp() 
        {

            p = Double.Parse(textBox1.Text);
            tot_tosses = Int32.Parse(textBox2.Text);
            trials = Int32.Parse(textBox3.Text);

            ratio = (pictureBox1.Width-1) / tot_tosses;
            //System.Diagnostics.Debug.WriteLine(tot_tosses);
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            pictureBox1.Image = b;
            g.DrawRectangle(Pens.Black, (float)0, 0, pictureBox1.Width-1 , (float)pictureBox1.Height-1);
            results = new LinkedList<double>();
        }
        private void DrawGraph()
        {
            b2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(b2);
            pictureBox2.Image = b2;
            g.DrawRectangle(Pens.Black, (float)0, 0, pictureBox1.Width-1 , (float)pictureBox1.Height-1);
            int[] array = new int[50];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in results)
            {
                array[(int)Math.Round((1 - result/max_ratio) * 49)]++;
            }
            int max = 0;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }
            for (int i = 0; i < array.Length; i++)
            {


                g2.DrawRectangle(Pens.Blue, (float)0, (float)(i) * (pictureBox2.Height-10) / 50, (float)array[i] * (pictureBox2.Width-5) / max, (float)(pictureBox2.Height-10) / 50);
                g2.FillRectangle(Brushes.Blue, (float)0, (float)(i) * (pictureBox2.Height-10) / 50, (float)array[i] * (pictureBox2.Width-5) / max, (float)(pictureBox2.Height-10) / 50);
            }
            g2.DrawRectangle(Pens.Black, (float)0, 0, pictureBox2.Width-1, (float)pictureBox2.Height - 1);

        }
        private double UpdateRatio(int current, int toss,double ratio, int type) 
        {
            if (type == 0) return (double) (current)/toss;
            else if(type==1) return (double) (current - ratio) / (toss + 1);
            else return (double)((current - ratio)/Math.Sqrt(toss + 1));
        }
        private int RealX(int toss)
        {
            return (int)Math.Round((double)toss * ratio)+5;
        }
        private int RealY(double ratio,double h,double max_ratio)
        {
            return (int)Math.Round((1-ratio/max_ratio) * h)+5;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
