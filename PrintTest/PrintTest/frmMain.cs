using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPaintOnControl frm = new frmPaintOnControl();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPreview frm = new frmPreview();
            frm.Show();
        }
    }
}