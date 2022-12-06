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
            double rx = 0,ry=0;
            for (int j = 0; j < tot_tosses; j++)
            {
                rx = rnd.NextDouble()*2-1;
                ry =rnd.NextDouble()*2-1;


                double radius = rx * rx + ry * ry;
                if(radius==0 || radius >= 1)
                {
                    j--;
                    continue;
                }
                else
                {
                    
                    results.AddFirst(new Tuple<double, double>(Math.Sqrt(-2.0 * Math.Log(radius) / radius)*rx , Math.Sqrt(-2.0 * Math.Log(radius) / radius) * ry));

                }

            }
            

            

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
           
            results = new LinkedList<Tuple<double,double>>();
          
            


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
            double max_ratio = ( 24);
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
            double max_ratio = 6;
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();

            foreach (Tuple<double, double> d in results)
            {
                if (d.Item2 != 0 && (Math.Pow(d.Item1, 2) / Math.Pow(d.Item2, 2))<=6) list.AddFirst(Math.Pow(d.Item1, 2) / Math.Pow(d.Item2, 2));
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox3.Width - 1) - 80, (pictureBox3.Height - 1) - 30);
            Bitmap b2 = b3;
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox3.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / (6)) * 47)]++;
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
                    g2.DrawString(Math.Round((i-max_ratio) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double max_ratio = 10;
            b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            LinkedList<double> list = new LinkedList<double>();

            foreach (Tuple<double, double> d in results)
            {
                if(Math.Pow(d.Item1,2)<= max_ratio * max_ratio) list.AddFirst(max_ratio/2 + d.Item1);
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
                    g2.DrawString(Math.Round(max_ratio * (i - 24) / 48, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 48, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio / 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }
    }
    
}
