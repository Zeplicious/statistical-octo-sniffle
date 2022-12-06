using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace histograms
{
    public partial class Form1 : Form
    {
        LinkedList<double> list;
        Rectangle r;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            r= new Rectangle(0, 0,(int) pictureBox1.Width-1, pictureBox1.Height-1);
            g2.DrawRectangle(Pens.Black, r);
            int i = 0;
            list = new LinkedList<double>();
            Random rand=new Random();
            while(i <1000)
            { 
                list.AddLast(rand.NextDouble());
                i++;
            }

       
            }
        private void DrawGraph(Rectangle r,LinkedList<double> results,double max_ratio, bool vertical)
        {
            Bitmap b2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            int[] array = new int[50];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in results)
            {
                array[(int)Math.Round((1 - result / max_ratio) * 49)]++;
            }
            int max = 0;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }
            if (vertical)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Rectangle r_aux = new Rectangle(r.X + 1, (int)(i) * (r.Y + r.Height) / 50, (int)array[i] * (r.Width - 5) / max, (int)(r.Height - 10) / 50);
                    g2.DrawRectangle(Pens.Blue, r_aux);
                    g2.FillRectangle(Brushes.Blue, r_aux);
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    int height = (int)array[i] * (r.Height - 5) / max;
                    Rectangle r_aux = new Rectangle((int)(i) * (r.Y + r.Width) / 50, (r.Height-height)+(r.Y+1), (int)(r.Width) / 50,height);
                    g2.DrawRectangle(Pens.Blue, r_aux);
                    g2.FillRectangle(Brushes.Blue, r_aux);
                }
            }
            
            g2.DrawRectangle(Pens.Black, r);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)DrawGraph(r, list, 1, true);
            else DrawGraph(r, list, 1, false);
        }
    }
}
