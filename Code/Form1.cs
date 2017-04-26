using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = 100;
            int heigth = 30;
            Bitmap bit = new Bitmap(width, heigth);
            Graphics g = Graphics.FromImage(bit);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, width, heigth));
            g.DrawString(textBox1.Text, new Font("Arial", 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), 0, 0);
            pictureBox1.Image = bit;
            bit.Save( System.Environment.CurrentDirectory + "/../../Img/test.jpg");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MODI.Document doc = new MODI.Document();

            doc.Create(textBox1.Text);
            //Bitmap img = new Bitmap(500, 750);
            //Graphics g = Graphics.FromImage(img);
            //g.FillRectangle(Brushes.White, new Rectangle(0, 0, newwidth, newheigth));
            //g.DrawImage(imgBack, xwidth / 2, yheigth / 3, imgBack.Width, imgBack.Height);
            MODI.Image image;
            MODI.Layout layout;
            try
            {
                doc.OCR(MODI.MiLANGUAGES.miLANG_CHINESE_SIMPLIFIED, true, true);
            }
            catch (Exception ex)
            {
                string str = System.Environment.CurrentDirectory;
                File.WriteAllText(str+"../../../log/"+DateTime.Now.ToString("yyyyMMddHHmmss")+".txt",ex.Message+ex.StackTrace);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < doc.Images.Count; i++)
            {
                image = (MODI.Image)doc.Images[i];
                layout = image.Layout;
                sb.Append(layout.Text);
            }

            textBox2.Text = sb.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();

            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fileDialog.FileName;
            }
        }
    }

}
