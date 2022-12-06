using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InverseCDFexp
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        private double computeValue(double lambda)
        {
            return -1*Math.Log(r.NextDouble())/lambda;
        }
        

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            double max_ratio = 4;
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            LinkedList<double> list = new LinkedList<double>();
            LinkedList<double> results = new LinkedList<double>(); 
            for(int i=0; i< trackBar1.Value; i++)
            {
                double value=computeValue(0.5);
                if (value <= max_ratio) list.AddFirst(value);

            }
            

            Rectangle r = new Rectangle(50, 10, (pictureBox1.Width - 1) - 80, (pictureBox1.Height - 1) - 30);
            
            Graphics g = Graphics.FromImage(b);
            pictureBox1.Image = b;
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
                g.DrawRectangle(Pens.Blue, r_aux);
                g.FillRectangle(Brushes.Blue, r_aux);
                if (i % 8 == 0)
                {
                    g.DrawLine(Pens.Black, r.X + (i) * (r.Width) / 96, r.Y + r.Height, r.X + (i) * (r.Width) / 96, r.Y + r.Height + 4);
                    g.DrawString(Math.Round(max_ratio * (i) / 96, 2).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (i) * (r.Width) / 96, r.Y + r.Height + 5);
                }
            }
            g.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);

            g.DrawString((max_ratio).ToString(), new Font("calibri", 10), Brushes.Black, r.X - 5 + (r.Width), r.Y + r.Height + 5);
            g.DrawLine(Pens.Black, r.X + r.Width, r.Y + r.Height, r.X + r.Width, r.Y + r.Height + 4);
            g.DrawRectangle(Pens.Black, r);
        }
    }
    
}
