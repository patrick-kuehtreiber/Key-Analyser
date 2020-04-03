namespace Key_Analyser
{
    public partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.radio_enrollment = new System.Windows.Forms.RadioButton();
            this.radio_validation = new System.Windows.Forms.RadioButton();
            this.lbl_debug = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_t = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_R = new System.Windows.Forms.Label();
            this.lbl_A = new System.Windows.Forms.Label();
            this.lbl_feedback = new System.Windows.Forms.Label();
            this.txt_thresh_R = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_R = new System.Windows.Forms.TrackBar();
            this.txt_thresh_A = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_A = new System.Windows.Forms.TrackBar();
            this.radio_passive = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_A)).BeginInit();
            this.SuspendLayout();
            // 
            // radio_enrollment
            // 
            this.radio_enrollment.AutoSize = true;
            this.radio_enrollment.Checked = true;
            this.radio_enrollment.Location = new System.Drawing.Point(34, 44);
            this.radio_enrollment.Name = "radio_enrollment";
            this.radio_enrollment.Size = new System.Drawing.Size(74, 17);
            this.radio_enrollment.TabIndex = 0;
            this.radio_enrollment.TabStop = true;
            this.radio_enrollment.Text = "Enrollment";
            this.radio_enrollment.UseVisualStyleBackColor = true;
            this.radio_enrollment.CheckedChanged += new System.EventHandler(this.radio_enrollment_CheckedChanged);
            // 
            // radio_validation
            // 
            this.radio_validation.AutoSize = true;
            this.radio_validation.Location = new System.Drawing.Point(34, 67);
            this.radio_validation.Name = "radio_validation";
            this.radio_validation.Size = new System.Drawing.Size(71, 17);
            this.radio_validation.TabIndex = 1;
            this.radio_validation.Text = "Validation";
            this.radio_validation.UseVisualStyleBackColor = true;
            this.radio_validation.CheckedChanged += new System.EventHandler(this.radio_validation_CheckedChanged);
            // 
            // lbl_debug
            // 
            this.lbl_debug.AutoSize = true;
            this.lbl_debug.Location = new System.Drawing.Point(31, 135);
            this.lbl_debug.Name = "lbl_debug";
            this.lbl_debug.Size = new System.Drawing.Size(81, 13);
            this.lbl_debug.TabIndex = 2;
            this.lbl_debug.Text = "ENROLLMENT";
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(41, 399);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 3;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_clicked);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(84, 223);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(184, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "t-value";
            // 
            // txt_t
            // 
            this.txt_t.Location = new System.Drawing.Point(288, 223);
            this.txt_t.Name = "txt_t";
            this.txt_t.Size = new System.Drawing.Size(41, 20);
            this.txt_t.TabIndex = 7;
            this.txt_t.Text = "1,25";
            this.txt_t.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_t_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(232, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_R
            // 
            this.lbl_R.AutoSize = true;
            this.lbl_R.Location = new System.Drawing.Point(163, 48);
            this.lbl_R.Name = "lbl_R";
            this.lbl_R.Size = new System.Drawing.Size(21, 13);
            this.lbl_R.TabIndex = 9;
            this.lbl_R.Text = "R: ";
            // 
            // lbl_A
            // 
            this.lbl_A.AutoSize = true;
            this.lbl_A.Location = new System.Drawing.Point(163, 69);
            this.lbl_A.Name = "lbl_A";
            this.lbl_A.Size = new System.Drawing.Size(20, 13);
            this.lbl_A.TabIndex = 10;
            this.lbl_A.Text = "A: ";
            // 
            // lbl_feedback
            // 
            this.lbl_feedback.AutoSize = true;
            this.lbl_feedback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_feedback.Location = new System.Drawing.Point(163, 135);
            this.lbl_feedback.Name = "lbl_feedback";
            this.lbl_feedback.Size = new System.Drawing.Size(76, 13);
            this.lbl_feedback.TabIndex = 11;
            this.lbl_feedback.Text = "                       ";
            // 
            // txt_thresh_R
            // 
            this.txt_thresh_R.Location = new System.Drawing.Point(288, 274);
            this.txt_thresh_R.Name = "txt_thresh_R";
            this.txt_thresh_R.Size = new System.Drawing.Size(41, 20);
            this.txt_thresh_R.TabIndex = 14;
            this.txt_thresh_R.Text = "1,00";
            this.txt_thresh_R.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_thresh_R_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Threshold R";
            // 
            // tb_R
            // 
            this.tb_R.Location = new System.Drawing.Point(84, 274);
            this.tb_R.Maximum = 30;
            this.tb_R.Name = "tb_R";
            this.tb_R.Size = new System.Drawing.Size(184, 45);
            this.tb_R.TabIndex = 12;
            this.tb_R.Value = 10;
            this.tb_R.Scroll += new System.EventHandler(this.tb_R_Scroll);
            // 
            // txt_thresh_A
            // 
            this.txt_thresh_A.Location = new System.Drawing.Point(288, 325);
            this.txt_thresh_A.Name = "txt_thresh_A";
            this.txt_thresh_A.Size = new System.Drawing.Size(41, 20);
            this.txt_thresh_A.TabIndex = 17;
            this.txt_thresh_A.Text = "1,00";
            this.txt_thresh_A.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_thresh_A_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Threshold A";
            // 
            // tb_A
            // 
            this.tb_A.Location = new System.Drawing.Point(84, 325);
            this.tb_A.Maximum = 30;
            this.tb_A.Name = "tb_A";
            this.tb_A.Size = new System.Drawing.Size(184, 45);
            this.tb_A.TabIndex = 15;
            this.tb_A.Value = 10;
            this.tb_A.Scroll += new System.EventHandler(this.tb_A_Scroll);
            // 
            // radio_passive
            // 
            this.radio_passive.AutoSize = true;
            this.radio_passive.Location = new System.Drawing.Point(34, 90);
            this.radio_passive.Name = "radio_passive";
            this.radio_passive.Size = new System.Drawing.Size(62, 17);
            this.radio_passive.TabIndex = 18;
            this.radio_passive.Text = "Passive";
            this.radio_passive.UseVisualStyleBackColor = true;
            this.radio_passive.CheckedChanged += new System.EventHandler(this.radio_passive_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 456);
            this.Controls.Add(this.radio_passive);
            this.Controls.Add(this.txt_thresh_A);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_A);
            this.Controls.Add(this.txt_thresh_R);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_R);
            this.Controls.Add(this.lbl_feedback);
            this.Controls.Add(this.lbl_A);
            this.Controls.Add(this.lbl_R);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_t);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.lbl_debug);
            this.Controls.Add(this.radio_validation);
            this.Controls.Add(this.radio_enrollment);
            this.Name = "Form1";
            this.Text = "Key Analyser";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_A)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radio_enrollment;
        private System.Windows.Forms.RadioButton radio_validation;
        private System.Windows.Forms.Label lbl_debug;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_t;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_R;
        private System.Windows.Forms.Label lbl_A;
        private System.Windows.Forms.Label lbl_feedback;
        private System.Windows.Forms.TextBox txt_thresh_R;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tb_R;
        private System.Windows.Forms.TextBox txt_thresh_A;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tb_A;
        private System.Windows.Forms.RadioButton radio_passive;
    }

}

