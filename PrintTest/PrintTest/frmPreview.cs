using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace PrintTest
{
    public partial class frmPreview : Form
    {

        int i = 1;

        string s1 = "Test String";
        int iRow = 0;
        private bool PrintAllpages = false;
        Font font = new Font("Tahoma", 10, FontStyle.Bold);

        public frmPreview()
        {
            InitializeComponent();
        }

        private void Doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float iY = -5;

            SizeF sf = e.Graphics.MeasureString(s1, font);
            float VLineTop = e.MarginBounds.Top - 5;
            float y = e.MarginBounds.Top;
            //int nOffsetX = 20;

            float x = e.MarginBounds.Left + 6;

            float xx = 6;

            float it = -14;

            e.Graphics.DrawString(i.ToString(), font, Brushes.Black, 10, 10);

            float lineHeight = font.GetHeight(e.Graphics) + 10;
            Pen pen = new Pen(Color.Black, 1);


            //Draw Columns
            for (int iloop = 0; iloop <= 7; iloop++)
            {
                //    e.Graphics.DrawLine(Pens.LightGray, (sf.Width + 3) + xx, y + iY, (sf.Width + 3) + xx, VLineTop);
                it += x;
            }

            while ((y + lineHeight) < e.MarginBounds.Bottom && iRow <= 10000000)//doc.Text.GetUpperBound(0))
            {

                //e.Graphics.DrawLine(pen, 0, y, e.PageBounds.Width, y);
                //e.Graphics.DrawString("Test String", font, Brushes.Black, 100, y);


                e.Graphics.DrawString("Test String", font, Brushes.Black, 5, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, x, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, x * 2, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, x * 3, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, x * 4, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, x * 5, y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, (x * 6), y);
                e.Graphics.DrawString("Test String", font, Brushes.Black, (x * 7), y);

                e.Graphics.DrawLine(Pens.LightGray, 0, y + iY, it, y + iY);
                //(sf.Width)
                iRow += 1;
                y += lineHeight;
            }
            //Draw Last Line            
            e.Graphics.DrawLine(Pens.LightGray, 0, y + iY, it, y + iY);


            //Draw Columns
            for (int iloop = 0; iloop <= 7; iloop++)
            {
                e.Graphics.DrawLine(Pens.LightGray, (sf.Width + 3) + xx, y + iY, (sf.Width + 3) + xx, VLineTop);
                xx += x;
            }

            i += 1;

            if (PrintAllpages == false)
            {
                e.HasMorePages = iRow <= Doc.PrinterSettings.ToPage;
            }
            else
            {
                if (iRow < 10000000)
                {
                    e.HasMorePages = true;
                }
            }
            if (iRow >= 10000000)
            {
                iRow = 0;
                i = 1;
            }

        }

        private void frmPreview_Load(object sender, EventArgs e)
        {


            daReport.DaPrintDocument daPrintDocument = new daReport.DaPrintDocument();

            daPrintDocument = new daReport.DaPrintDocument();

            SqlConnection g_Con = new SqlConnection(@"Data Source=master;Initial Catalog=SalesDB;Persist Security Info=True;User ID=sa;Password=123");



            g_Con.Open();

            SqlCommand cmd = new SqlCommand("Select  ID As number,CS_Name AS square,CS_ADD AS cube From CSData", g_Con);
            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable("printTable");
            dt.Load(dr);
            dr.Close();
            dr.Dispose();
            Application.DoEvents();
            g_Con.Close();
            g_Con.Dispose();
            cmd.Dispose();

            daPrintDocument.AddData(dt);

            // set .xml file for printing
            daPrintDocument.setXML("reportDynamic.xml");

            //PrintAllpages = false;

            float scale = Convert.ToSingle(100) / 100f;
            long quality = Convert.ToInt64(100);
            PrintControllerImage controller = new PrintControllerImage(scale, quality);
            
            daPrintDocument.PrintController = controller;
            daPrintDocument.Print();
            //Doc.PrintController = controller;
            //Doc.Print();
            pictureBox1.Image = controller.bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintAllpages = false;

            float scale = Convert.ToSingle(100) / 100f;
            long quality = Convert.ToInt64(100);
            PrintControllerImage controller = new PrintControllerImage(scale, quality);
            Doc.PrintController = controller;
            Doc.Print();
            pictureBox1.Image = controller.bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintAllpages = true;
            
            iRow = 0;
            i = 1;
            StandardPrintController printControl = new StandardPrintController();
            Doc.PrintController = printControl;

            Doc.Print();

           
        }




    }
}