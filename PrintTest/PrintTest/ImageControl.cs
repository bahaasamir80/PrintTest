using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PrintTest
{
    class ImageControl:Control
    {
        private Bitmap image;

        public ImageControl()
        { 
            Bitmap image = new Bitmap(793, 1126);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null)
            {
                Rectangle Rect = new Rectangle(0, 0, this.Width, this.Height);
                this.DrawToBitmap(image, Rect);
         
            }
            base.OnPaint(e);
        }
        public Image Picture
        {
            get 
            {
               
                return image;
            }
        
        }



    }
}
