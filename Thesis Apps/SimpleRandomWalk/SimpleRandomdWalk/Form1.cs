using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRandomdWalk
{
    public partial class Form1 : Form
    {   private readonly Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        private int RandomWalk(int c,int iterations) 
        {
            int position = 0;
            double ran=random.NextDouble();
            for(int i = 0; i < iterations; i++)
            {
                double left_pr = 0.5 + 0.5*(position / (Math.Sqrt(Math.Pow( position,2)) + c));
                if (ran <= left_pr)
                {
                    position--;
                }
                else position++;
            }
            return position;

        }


        private void button1_Click(object sender, EventArgs e)
        {

            LinkedList<int> list = new LinkedList<int>();

            for(int i= 0; i<10000;i++)
            {
                int value=0;
                if (sender == button1)
                {
                    value = RandomWalk(trackBar1.Value, 2);
                }
                else if(sender == button2)
                {
                    value = RandomWalk(trackBar1.Value, 3);
                }

                list.AddFirst(value);
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox1.Width - 1) - 80, (pictureBox1.Height - 1) - 30);
            Bitmap b2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            int[] array = new int[7];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {
                array[(int)Math.Round((result+3))]++;
            }
            int max = 0;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }


            for (int i = 0; i < array.Length; i++)
            {
                int height = (int)array[i] * (r.Height) / max;
                Rectangle r_aux = new Rectangle((int)r.X + (i) * (r.Width) / 7, (r.Height - height) + (r.Y), (int)(r.Width) / 7, height);
                g2.DrawRectangle(Pens.Blue, r_aux);
                g2.FillRectangle(Brushes.Blue, r_aux);

                g2.DrawLine(Pens.Black, r.X + (r.Width) / 14 + (i) * (r.Width) / 7, r.Y + r.Height, r.X + (r.Width) / 14 + (i) * (r.Width) / 7, r.Y + r.Height + 4);
                g2.DrawString((i - 3).ToString(), new Font("calibri", 10), Brushes.Black, - 5+ r.X + (r.Width) / 14 + (i) * (r.Width) / 7, r.Y + r.Height + 5);
                
            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);
            g2.DrawRectangle(Pens.Black, r);
        }
    }
}
