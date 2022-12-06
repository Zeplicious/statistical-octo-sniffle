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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Graphics g;
        public Graphics g2;
        public Bitmap b;
        public Bitmap b2;
        public Bitmap b3;
        Random rnd;
        Point xy1;
        Point xy2;

        LinkedList<Tuple<double, double>> results;
        LinkedList<double> vX;
        LinkedList<double> vY;
        int tot_tosses;
        double maxR;
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
            double radius = 0,angle=0;
            for (int j = 0; j < tot_tosses; j++)
            {
                radius = rnd.NextDouble()*24;
                angle=rnd.NextDouble()*Math.PI*2;

                xy1.X = (pictureBox1.Width - 1) / 2;
                xy1.Y = (pictureBox1.Height - 1) / 2;

                xy2.X = (int)RealX(radius*maxR/24,angle);
                xy2.Y = (int)RealY(radius * maxR / 24, angle);

                g.DrawRectangle(new Pen(GetRandomColour()), xy2.X, xy2.Y,1,1);
                
                results.AddFirst(new Tuple<double,double>(Math.Cos(angle)*radius, Math.Sin(angle) * radius));

                vX.AddFirst(24+Math.Cos(angle) * radius);

                vY.AddFirst(24+Math.Sin(angle) * radius);
            }
            
            DrawHistogram(new Rectangle(30, 10, (pictureBox2.Width-1-20)/2 - 30, (pictureBox2.Height - 1) - 30), vX, 48, b2, pictureBox2);

            DrawHistogram(new Rectangle(pictureBox2.Width/2 + 30, 10, (pictureBox2.Width - 1-20) / 2 - 30, (pictureBox2.Height - 1) - 30), vY, 48, b2, pictureBox2);

        }
        private static readonly Random rand = new Random();

        private Color GetRandomColour()
        {
            return Color.FromArgb(rand.Next(64,128), rand.Next(64,128), rand.Next(64,128));
        }



        private void SetUp() 
        {
            tot_tosses = Int32.Parse(textBox2.Text);

            //System.Diagnostics.Debug.WriteLine(tot_tosses);
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            pictureBox1.Image = b;

            g.DrawLine(Pens.Black,0, (pictureBox1.Height - 1) / 2, pictureBox1.Width - 1, (pictureBox1.Height - 1)/2);
            g.DrawLine (Pens.Black, (pictureBox1.Width-1)/2,0, (pictureBox1.Width - 1) / 2, pictureBox1.Height-1);
            float offset_x=0,offset_y=0;
            for (int i = 0; i < 80; i++)
            {
                if (i-40 == -24) offset_x = (i) * (pictureBox1.Width - 1) / 80;
                g.DrawLine(Pens.Black, (i) * (pictureBox1.Width-1) / 80, (pictureBox1.Height-1)/2, (i) * (pictureBox1.Width-1) / 80, (pictureBox1.Height-1)/2 + 2);
                if (i % 4 == 0 && i-40 != 0) 
                {
                    if ((i-40) >= 10){
                        g.DrawString((i - 40).ToString(), new Font("calibri", 10), Brushes.Black, -10 + (i) * (pictureBox1.Width) / 80, pictureBox1.Height / 2 + 5);
                    }
                    else if ((i-40) <= -10) g.DrawString((i - 40).ToString(), new Font("calibri", 10), Brushes.Black, -10 + (i) * (pictureBox1.Width) / 80, pictureBox1.Height / 2 + 5);
                    else if ((i - 40) < 0) g.DrawString((i - 40).ToString(), new Font("calibri", 10), Brushes.Black, -10 + (i) * (pictureBox1.Width) / 80, pictureBox1.Height / 2 + 5);
                    else g.DrawString((i - 40).ToString(), new Font("calibri", 10), Brushes.Black, -5 + (i) * (pictureBox1.Width) / 80, pictureBox1.Height / 2 + 5);

                    g.DrawLine(Pens.Black, (i) * (pictureBox1.Width - 1) / 80, (pictureBox1.Height - 1) / 2, (i) * (pictureBox1.Width - 1) / 80, (pictureBox1.Height - 1) / 2 + 4);

                }
            }
            for (int i = 0; i < 50; i++)
            {
                g.DrawLine(Pens.Black, (pictureBox1.Width - 1) / 2, (i) * (pictureBox1.Height - 1) / 50, (pictureBox1.Width - 1) / 2 -2, (i) * (pictureBox1.Height - 1) / 50);
                if (25 - i == 24) offset_y=(i) * (pictureBox1.Height - 1) / 50;
                if (i % 5 == 0 && i-25!=0)
                {
                    
                    g.DrawString((-i + 25).ToString(), new Font("calibri", 10), Brushes.Black, -25 + (pictureBox1.Width) / 2,-  7+ (i) * (pictureBox1.Height) / 50);
                    g.DrawLine(Pens.Black, (pictureBox1.Width - 1) / 2, (i) * (pictureBox1.Height - 1) / 50, (pictureBox1.Width - 1) / 2 - 4, (i) * (pictureBox1.Height - 1) / 50);

                }
            }
            
            Rectangle square=new Rectangle((int)offset_x, (int)offset_y, (int)(pictureBox1.Width-1-2*offset_x), (int)(pictureBox1.Height - 1 - 2 * offset_y));
            maxR=(pictureBox1.Width - 1 - 2 * offset_x)/2;
            g.DrawEllipse(Pens.Red,square);
            g.DrawRectangle(Pens.Black, (float)0, 0, pictureBox1.Width-1 , (float)pictureBox1.Height-1);
            results = new LinkedList<Tuple<double,double>>();
            vX = new LinkedList<double>();
            vY = new LinkedList<double>();
            b2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            


        }


        private float RealX(double radius, double angle)
        {
            return (float)(Math.Cos(angle)*radius + (pictureBox1.Width-1)/2);
        }
        private float RealY(double radius, double angle)
        {
            return (float)(Math.Sin(angle) * radius*-1 + (pictureBox1.Height - 1) / 2);
        }
        private void DrawHistogram(Rectangle r, LinkedList<double> results, double max_ratio, Bitmap b,PictureBox p)
        {
            Bitmap b2 = b;
            Graphics g2 = Graphics.FromImage(b2);
            p.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in results)
            {
                if ((result / max_ratio) <= 1)array[(int)Math.Round(( result / max_ratio) * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i-24) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString((max_ratio-24).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);

            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 25, r.Y - 5);

            g2.DrawRectangle(Pens.Black, r);

        }



        private void button3_Click(object sender, EventArgs e)
        {
            double max_ratio = 48;
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();
            
            foreach (Tuple<double,double> d in results)
            {
                if (d.Item2 != 0 && Math.Pow(d.Item1 / d.Item2, 2) <= 24*24) list.AddFirst(24+d.Item1 / d.Item2);
            }
           
            Rectangle r = new Rectangle(50, 10, (pictureBox3.Width - 1) - 80, (pictureBox3.Height - 1) - 30);
            Bitmap b2 = b3;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox3.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / 48) * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i-24) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio/2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();
            double max_ratio = ( 24 * 24);
            foreach (Tuple<double, double> d in results)
            {
                list.AddFirst( Math.Pow(d.Item1, 2));
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox3.Width - 1) - 80, (pictureBox3.Height - 1) - 30);
            Bitmap b2 = b3;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox3.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / max_ratio) * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            double max_ratio = 25;
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();

            foreach (Tuple<double, double> d in results)
            {
                if (d.Item2 != 0 && (Math.Pow(d.Item1, 2) / Math.Pow(d.Item2, 2))<=25) list.AddFirst(Math.Pow(d.Item1, 2) / Math.Pow(d.Item2, 2));
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox3.Width - 1) - 80, (pictureBox3.Height - 1) - 30);
            Bitmap b2 = b3;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox3.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / (25)) * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            double max_ratio = 24;
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();

            foreach (Tuple<double, double> d in results)
            {
                if (d.Item2 != 0 && Math.Pow(d.Item1 / Math.Pow(d.Item2, 2),2) <= 24*24) list.AddFirst(24+d.Item1 / Math.Pow(d.Item2, 2));
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox3.Width - 1) - 80, (pictureBox3.Height - 1) - 30);
            Bitmap b2 = b3;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox3.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / 48) * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i-max_ratio/2) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio / 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }
    }
}
