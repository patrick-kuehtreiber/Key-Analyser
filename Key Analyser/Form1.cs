using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Key_Analyser
{
    public partial class Form1 : Form
    {
        public void Set_A_R_values(double R, double A)
        {
            R = (double)Decimal.Round((decimal)R, 4);
            A = (double)Decimal.Round((decimal)A, 4);
            lbl_A.Text = "A: " + A;
            lbl_R.Text = "R: " + R;

            lbl_feedback.BackColor = System.Drawing.Color.Green;
            if (R > Program.thresh_R || A > Program.thresh_A)
            {
                lbl_feedback.BackColor = System.Drawing.Color.Red;
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void radio_enrollment_CheckedChanged(object sender, EventArgs e)
        {
            Program.STATE = Program.ENROLLMENT;
            lbl_debug.Text = "ENROLLMENT";
        }

        private void radio_validation_CheckedChanged(object sender, EventArgs e)
        {
            Program.STATE = Program.VALIDATION;
            lbl_debug.Text = "VALIDATION";
        }

        private void radio_passive_CheckedChanged(object sender, EventArgs e)
        {
            Program.STATE = Program.PASSIVE;
            lbl_debug.Text = "PASSIVE";
        }
        private void btn_reset_clicked(object sender, EventArgs e)
        {
            lbl_debug.Text = "Reset";
            Program.createFile();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double t = (double)trackBar1.Value / 100 + 1.2d;

            txt_t.Text = "" + t;

        }

        private void txt_t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double txtval = Convert.ToDouble(txt_t.Text);
                txtval = Math.Max(txtval, 1.2);
                txtval = Math.Min(txtval, 1.4);
                txt_t.Text = "" + txtval;
                double tbval = (txtval - 1.2) * 100;

                int tb_int = Int32.Parse("" + tbval);
                trackBar1.Value = tb_int;
                Program.t = tbval;
            }
        }

        private void txt_thresh_A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double txtval = Convert.ToDouble(txt_t.Text);
                txtval = Math.Max(txtval, 1.2);
                txtval = Math.Min(txtval, 1.4);
                txt_t.Text = "" + txtval;
                double tbval = (txtval - 1.2) * 100;

                int tb_int = Int32.Parse("" + tbval);
                tb_R.Value = tb_int;
                Program.thresh_A = tbval;
            }
        }

        private void txt_thresh_R_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double txtval = Convert.ToDouble(txt_t.Text);
                txtval = Math.Max(txtval, 1.2);
                txtval = Math.Min(txtval, 1.4);
                txt_t.Text = "" + txtval;
                double tbval = (txtval - 1.2) * 100;

                int tb_int = Int32.Parse("" + tbval);
                tb_A.Value = tb_int;
                Program.thresh_R = tbval;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tb_R_Scroll(object sender, EventArgs e)
        {
            double r_t = (double)tb_R.Value / 10;
            Program.thresh_R = r_t;
            txt_thresh_R.Text = "" + r_t;
        }

        private void tb_A_Scroll(object sender, EventArgs e)
        {
            double r_t = (double)tb_A.Value / 10;
            Program.thresh_A = r_t;
            txt_thresh_A.Text = "" + r_t;
        }

    }
}

