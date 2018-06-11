namespace Hein_Remot_Control
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        	this.btn_start = new System.Windows.Forms.Button();
        	this.lbl_con = new System.Windows.Forms.Label();
        	this.lbl_IP = new System.Windows.Forms.Label();
        	this.txt_log = new System.Windows.Forms.TextBox();
        	this.btn_exit = new System.Windows.Forms.Button();
        	this.txt_IP = new System.Windows.Forms.TextBox();
        	this.btn_stop = new System.Windows.Forms.Button();
        	this.button1 = new System.Windows.Forms.Button();
        	this.button2 = new System.Windows.Forms.Button();
        	this.button3 = new System.Windows.Forms.Button();
        	this.button4 = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.SuspendLayout();
        	// 
        	// btn_start
        	// 
        	this.btn_start.Location = new System.Drawing.Point(307, 10);
        	this.btn_start.MaximumSize = new System.Drawing.Size(75, 23);
        	this.btn_start.MinimumSize = new System.Drawing.Size(75, 23);
        	this.btn_start.Name = "btn_start";
        	this.btn_start.Size = new System.Drawing.Size(75, 23);
        	this.btn_start.TabIndex = 0;
        	this.btn_start.Text = "تشغيل";
        	this.btn_start.UseVisualStyleBackColor = true;
        	this.btn_start.Click += new System.EventHandler(this.Btn_startClick);
        	// 
        	// lbl_con
        	// 
        	this.lbl_con.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lbl_con.ForeColor = System.Drawing.Color.White;
        	this.lbl_con.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
        	this.lbl_con.Location = new System.Drawing.Point(307, 40);
        	this.lbl_con.MaximumSize = new System.Drawing.Size(74, 22);
        	this.lbl_con.MinimumSize = new System.Drawing.Size(74, 22);
        	this.lbl_con.Name = "lbl_con";
        	this.lbl_con.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.lbl_con.Size = new System.Drawing.Size(74, 22);
        	this.lbl_con.TabIndex = 2;
        	this.lbl_con.Text = "متصل ب  : ";
        	this.lbl_con.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// lbl_IP
        	// 
        	this.lbl_IP.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lbl_IP.ForeColor = System.Drawing.Color.White;
        	this.lbl_IP.Location = new System.Drawing.Point(12, 39);
        	this.lbl_IP.Name = "lbl_IP";
        	this.lbl_IP.Size = new System.Drawing.Size(288, 23);
        	this.lbl_IP.TabIndex = 3;
        	this.lbl_IP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// txt_log
        	// 
        	this.txt_log.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.txt_log.Location = new System.Drawing.Point(13, 70);
        	this.txt_log.MaximumSize = new System.Drawing.Size(288, 87);
        	this.txt_log.MinimumSize = new System.Drawing.Size(288, 87);
        	this.txt_log.Multiline = true;
        	this.txt_log.Name = "txt_log";
        	this.txt_log.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.txt_log.Size = new System.Drawing.Size(288, 87);
        	this.txt_log.TabIndex = 4;
        	// 
        	// btn_exit
        	// 
        	this.btn_exit.Location = new System.Drawing.Point(306, 134);
        	this.btn_exit.MaximumSize = new System.Drawing.Size(75, 23);
        	this.btn_exit.MinimumSize = new System.Drawing.Size(75, 23);
        	this.btn_exit.Name = "btn_exit";
        	this.btn_exit.Size = new System.Drawing.Size(75, 23);
        	this.btn_exit.TabIndex = 5;
        	this.btn_exit.Text = "خروج";
        	this.btn_exit.UseVisualStyleBackColor = true;
        	this.btn_exit.Click += new System.EventHandler(this.Btn_exitClick);
        	// 
        	// txt_IP
        	// 
        	this.txt_IP.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.txt_IP.Location = new System.Drawing.Point(13, 10);
        	this.txt_IP.MaximumSize = new System.Drawing.Size(287, 21);
        	this.txt_IP.MinimumSize = new System.Drawing.Size(287, 21);
        	this.txt_IP.Name = "txt_IP";
        	this.txt_IP.Size = new System.Drawing.Size(287, 21);
        	this.txt_IP.TabIndex = 6;
        	this.txt_IP.Text = "192.168.XXX.XXX";
        	this.txt_IP.TextChanged += new System.EventHandler(this.Txt_IPTextChanged);
        	// 
        	// btn_stop
        	// 
        	this.btn_stop.Enabled = false;
        	this.btn_stop.Location = new System.Drawing.Point(306, 71);
        	this.btn_stop.Name = "btn_stop";
        	this.btn_stop.Size = new System.Drawing.Size(75, 23);
        	this.btn_stop.TabIndex = 7;
        	this.btn_stop.Text = "ايقاف";
        	this.btn_stop.UseVisualStyleBackColor = true;
        	this.btn_stop.Click += new System.EventHandler(this.Btn_stopClick);
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(13, 164);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 8;
        	this.button1.Text = "Play/Pause";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// button2
        	// 
        	this.button2.Location = new System.Drawing.Point(94, 164);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(68, 23);
        	this.button2.TabIndex = 9;
        	this.button2.Text = "Stop";
        	this.button2.UseVisualStyleBackColor = true;
        	this.button2.Click += new System.EventHandler(this.Button2Click);
        	// 
        	// button3
        	// 
        	this.button3.Location = new System.Drawing.Point(168, 164);
        	this.button3.Name = "button3";
        	this.button3.Size = new System.Drawing.Size(52, 23);
        	this.button3.TabIndex = 10;
        	this.button3.Text = "Mute";
        	this.button3.UseVisualStyleBackColor = true;
        	this.button3.Click += new System.EventHandler(this.Button3Click);
        	// 
        	// button4
        	// 
        	this.button4.Location = new System.Drawing.Point(226, 164);
        	this.button4.Name = "button4";
        	this.button4.Size = new System.Drawing.Size(75, 23);
        	this.button4.TabIndex = 11;
        	this.button4.Text = "FullScreen";
        	this.button4.UseVisualStyleBackColor = true;
        	this.button4.Click += new System.EventHandler(this.Button4Click);
        	// 
        	// label1
        	// 
        	this.label1.ForeColor = System.Drawing.Color.White;
        	this.label1.Location = new System.Drawing.Point(307, 169);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(75, 23);
        	this.label1.TabIndex = 12;
        	this.label1.Text = "By: MDMKS";
        	// 
        	// Form1
        	// 
        	this.BackColor = System.Drawing.Color.DarkViolet;
        	this.ClientSize = new System.Drawing.Size(394, 197);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.button4);
        	this.Controls.Add(this.button3);
        	this.Controls.Add(this.button2);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.btn_stop);
        	this.Controls.Add(this.txt_IP);
        	this.Controls.Add(this.btn_exit);
        	this.Controls.Add(this.txt_log);
        	this.Controls.Add(this.lbl_IP);
        	this.Controls.Add(this.lbl_con);
        	this.Controls.Add(this.btn_start);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MinimumSize = new System.Drawing.Size(410, 208);
        	this.Name = "Form1";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Hein 4.5.2 Remote (server)";
        	this.Load += new System.EventHandler(this.Form1Load);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.Label lbl_con;

        private System.Windows.Forms.Button btn_start;



        #endregion


    }
}

