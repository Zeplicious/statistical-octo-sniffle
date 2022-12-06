using System.Runtime.CompilerServices;

namespace MovingRectangle
{
    public partial class Form1 : Form
    {
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
        readonly double ScaleFactor = 0.1d;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drag = false;
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            pictureBox1.Image = b;

            r = new Rectangle(20, 20, 400, 300);
            g.DrawRectangle(Pens.Black, r);

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
                int delta=(int)((e.Delta * ScaleFactor));
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
        private void redraw()
        {
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Black, r);
            pictureBox1.Image = b;
        }


    }
}