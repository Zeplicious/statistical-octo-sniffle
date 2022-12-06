using System.Windows.Forms;

namespace Homework_1
{
	public partial class Form1 : Form
	{
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
		}

		private void button1_Click(object sender, EventArgs e)
		{
            this.richTextBox1.Text = "";
			Random r = new Random();
            this.richTextBox1.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));


            this.richTextBox2.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

            this.richTextBox3.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

			this.richTextBox4.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

            this.richTextBox5.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

            this.richTextBox6.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

            this.richTextBox7.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            this.richTextBox8.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            this.richTextBox9.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
        }

		private void button1_MouseHover(object sender, EventArgs e)
		{
			this.richTextBox1.Text = "Hai passato il mouse sopra il bottone";
		}

		private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}