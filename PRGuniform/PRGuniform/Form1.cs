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

namespace PRGuniform
{
    public partial class Form1 : Form
    {
        private UInt32 seed;
        private UInt32 generated;
        public Form1()
        {
            InitializeComponent();
        }
        private UInt32 UniformDouble()
        {
            if (generated == 0)
            {
                generated = (UInt32)(11033515245 * seed + 12345);
            }
            else 
            { 
                generated = (UInt32) (11033515245 * generated + 12345);
            }
                
            return (UInt32)(generated % Math.Pow(2, 30));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LinkedList<double> list = new LinkedList<double>();
            generated = 0;
            seed=UInt32.Parse(textBox1.Text);
            for (int i = 0; i < 100000; i++)
            {
                
                double value= UniformDouble() / (Math.Pow(2, 30) - 1);
                list.AddFirst(value);
            }

            Rectangle r = new Rectangle(50, 10, (pictureBox1.Width - 1) - 80, (pictureBox1.Height - 1) - 30);
            Bitmap b2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g2 = Graphics.FromImage(b2);
            pictureBox1.Image = b2;
            int[] array = new int[48];
            for (int i = 0; i < array.Length; i++) { array[i] = 0; }
            foreach (double result in list)
            {

                array[(int)Math.Round(result * 47)]++;
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
                    g2.DrawLine(Pens.Black, r.X + (r.Width) / 96 + (i) * (r.Width) / 48, r.Y + r.Height, r.X + (r.Width) / 96 + (i) * (r.Width) / 48, r.Y + r.Height + 4);
                    g2.DrawString((i).ToString(), new Font("calibri", 10), Brushes.Black, -5 + r.X + (r.Width) / 48 + (i) * (r.Width) / 48, r.Y + r.Height + 5);

                }

            }
            g2.DrawString(max.ToString(), new Font("calibri", 10), Brushes.Black, r.X - 35, r.Y - 5);
            g2.DrawRectangle(Pens.Black, r);
        }
    }
    
}
