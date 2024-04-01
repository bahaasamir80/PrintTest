using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PrintTest
{
    class PrintControllerImage : PreviewPrintController
    {
        private int _Page = 0;
        private Metafile _Metafile = null;
        private float _Scale = 1f;
        private long _Quality = 75L;
        public bool ShowNext = false;



        public PrintControllerImage()
        { }
        public PrintControllerImage(float scale, long quality)
        {
            _Scale = scale;
            _Quality = quality;
            //_pic = pic;
        }

        public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
        {
            _Page++;

            return base.OnStartPage(document, e);
        }

        public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
        {
            base.OnEndPage(document, e);

            // Get the current Metafile
            PreviewPageInfo[] ppia = GetPreviewPageInfo();
            PreviewPageInfo ppi = ppia[ppia.Length - 1];
            Image image = ppi.Image;
            _Metafile = (Metafile)image;
            SaveViaBitmap(document, e);
            //frm.ShowDialog();
            //}
        }

        protected bool PlayRecord(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {
                // Copy the unmanaged record to a managed byte buffer 
                // that can be used by PlayRecord.
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
            }

            _Metafile.PlayRecord(recordType, flags, dataSize, dataArray);

            return true;
        }

        public Bitmap bitmap;// = new Bitmap((int)(width * _Scale), (int)(height * _Scale));
        protected void SaveViaBitmap(PrintDocument document, PrintPageEventArgs e)
        {

            int width = e.PageBounds.Width;
            int height = e.PageBounds.Height;

            //using (Bitmap bitmap = new Bitmap((int)(width * _Scale), (int)(height * _Scale)))

            bitmap = new Bitmap((int)(width * _Scale), (int)(height * _Scale));
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                if (_Scale != 1) graphics.ScaleTransform(_Scale, _Scale);

                Point point = new Point(0, 0);
                Graphics.EnumerateMetafileProc callback = new Graphics.EnumerateMetafileProc(PlayRecord);

                graphics.EnumerateMetafile(_Metafile, point, callback);

                if (_Scale == 1 || true)
                {

                    Retbitmap = bitmap;
                    //Form2 frm = new Form2();
                    //                    frm.pic.Image = bitmap;
                    //                    frm.ShowDialog();
                    ////                    //frm = null;



                    //Save(bitmap);
                }


                //else
                //{
                //    using (Bitmap bitmap2 = new Bitmap(width, height))
                //    using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                //    {
                //        graphics2.DrawImage(bitmap, 0, 0, width, height);

                //        //Save(bitmap2);
                //    }
                //}
            }
        }

        private Bitmap doc;
        public Bitmap Retbitmap;
        //{
        //    get { return doc; }
        //    set { doc = value; }
        //}


    }
}
