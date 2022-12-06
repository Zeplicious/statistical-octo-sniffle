using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace generateGaussian
{
    public partial class Form1 : Form
    {
        Random rnd;

        public Form1()
        {
            InitializeComponent();

            rnd = new Random();

        }
        private double generateGaussian() 
        {
   
            double radius = rnd.NextDouble();
            double angle = rnd.NextDouble() * Math.PI * 2;
            return Math.Cos(angle) * Math.Sqrt(-2* Math.Log(radius));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double max_ratio = 10;
            LinkedList<double> list = new LinkedList<double>();

            for(int i=0; i<trackBar1.Value;i++)
            {
                double d = generateGaussian();
                if (Math.Pow(d, 2) <= max_ratio * max_ratio) list.AddFirst(max_ratio / 2 + d);
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox1.Width - 1) - 80, (pictureBox1.Height - 1) - 30);
            Bitmap b2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            int[] array = new int[96];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result / max_ratio) * 95)]++;
            }
            int max = 0;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }


            for (int i = 0; i < array.Length; i++)
            {
                int height = (int)array[i] * (r.Height) / max;
                Rectangle r_aux = new Rectangle((int)r.X + (i) * (r.Width) / 96, (r.Height - height) + (r.Y), (int)(r.Width) / 96, height);
                g2.DrawRectangle(Pens.Blue, r_aux);
                g2.FillRectangle(Brushes.Blue, r_aux);
                if (i % 8 == 0)
                {
                    g2.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 96, r.Y + r.Height, r.X + (i) * (r.Width) / 96, r.Y + r.Height + 4);
                    g2.DrawString(Math.Round(max_ratio * (i - 48) / 96, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 96, r.Y + r.Height + 5);
                }
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g2.DrawString((max_ratio / 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g2.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g2.DrawRectangle(Pens.Black, r);
        }
    }
}
