using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintTest
{
    public partial class frmPaintOnControl : Form
    {
        Control[] RepControl = new Control[20001];
        //Bitmap[] Photo = new Bitmap[20001];
        Font font = new Font("Tahoma", 10, FontStyle.Bold);
        int iPage = 1;
        int iAllPages = 0;
        string stPage = "Current Page";

        public frmPaintOnControl()
        {
            InitializeComponent();

            for (int iLoop = 0; iLoop <= 20000; iLoop++)
            {
                iAllPages += 1;

                RepControl[iLoop] = new Control();
                RepControl[iLoop].Size = new System.Drawing.Size(793, 1126);
                RepControl[iLoop].BackColor = Color.White;
                RepControl[iLoop].Paint += new System.Windows.Forms.PaintEventHandler(this.Contorl_Paint);
                RepControl[iLoop].Visible = false;
            }
            label1.Text = iAllPages.ToString();
        }

        private void Contorl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightGray, 0, 0, 793, 1126);

            int x = 10;
            string s1 = "Test String";
            SizeF sf = e.Graphics.MeasureString(s1, font);
            float fWidth = sf.Width;
            float fontWidth = sf.Width;

            e.Graphics.DrawLine(Pens.Black, 0, 10, 793, 10);
            e.Graphics.DrawString("Test String", this.font, Brushes.Black, 50, 50);
            e.Graphics.DrawString(iPage.ToString(), font, Brushes.Black, 10, 50);
            //Draw Rows
            for (int iLoop = 0; iLoop <= 50; iLoop++)
            {
                //Point point1 = new Point(100, 100 + x);
                //Point point2 = new Point(500, 100 + x);
                //// Draw line to screen.
                //e.Graphics.DrawLine(Pens.Black, point1, point2);
                e.Graphics.DrawLine(Pens.LightSeaGreen, 0, 100 + x, 793, 100 + x);
                x += 20;
            }
            //Draw Columns
            for (int iLoop = 0; iLoop <= 8; iLoop++)
            {
                e.Graphics.DrawLine(Pens.RoyalBlue, (fWidth + 3), 110, 0 + (fWidth + 3), 1110);
                fWidth += fontWidth;

            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            grB.Controls.Add(RepControl[0]);
            RepControl[0].Top = 0;
            RepControl[0].Width = grB.Width;
            RepControl[0].Height = grB.Height;
            RepControl[0].Visible = true;
            //Draw To Bitmap
            //Photo[0] = new Bitmap(RepControl[0].Width, RepControl[iPage].Height);
            //RepControl[0].DrawToBitmap(Photo[0], new Rectangle(0, 0, 793, 1126));
            //pictureBox1.Image = Photo[0];
            this.AutoScrollPosition = new Point(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stPage = "Previous Page";
            grB.Controls.Add(RepControl[iPage]);
            RepControl[iPage].Top = 0;
            RepControl[iPage].Width = grB.Width;
            RepControl[iPage].Height = grB.Height;
            RepControl[iPage].Visible = true;

            //Photo[iPage] = new Bitmap(RepControl[iPage].Width, RepControl[iPage].Height);
            //RepControl[iPage].DrawToBitmap(Photo[iPage], new Rectangle(0, 0, RepControl[iPage].Width, RepControl[iPage].Height));
            //pictureBox1.Image = Photo[iPage];
            //pictureBox1.Image = RepControl[iPage].Picture;

            grB.Controls.Remove(RepControl[iPage - 1]);
            iPage += 1;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("PictureBox.Image", font, Brushes.Black, 0, 10);

            e.Graphics.DrawString(stPage, font, Brushes.Black, 396, 10);

            e.Graphics.DrawLine(Pens.Black, 0, 25, pictureBox1.Width, 25);
        }
    }
}