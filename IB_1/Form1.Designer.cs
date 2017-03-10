namespace IB_1
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
            this.btn_open_file = new System.Windows.Forms.Button();
            this.txtbx_path = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtbx_bit_form = new System.Windows.Forms.TextBox();
            this.txtbx_byte_form = new System.Windows.Forms.TextBox();
            this.txtbx_text_form = new System.Windows.Forms.TextBox();
            this.txtbx_hash = new System.Windows.Forms.TextBox();
            this.btn_hash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_open_file
            // 
            this.btn_open_file.Location = new System.Drawing.Point(662, 38);
            this.btn_open_file.Name = "btn_open_file";
            this.btn_open_file.Size = new System.Drawing.Size(87, 23);
            this.btn_open_file.TabIndex = 0;
            this.btn_open_file.Text = "btn_open_file";
            this.btn_open_file.UseVisualStyleBackColor = true;
            this.btn_open_file.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // txtbx_path
            // 
            this.txtbx_path.Location = new System.Drawing.Point(553, 12);
            this.txtbx_path.Name = "txtbx_path";
            this.txtbx_path.Size = new System.Drawing.Size(196, 20);
            this.txtbx_path.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtbx_bit_form
            // 
            this.txtbx_bit_form.Location = new System.Drawing.Point(393, 122);
            this.txtbx_bit_form.Multiline = true;
            this.txtbx_bit_form.Name = "txtbx_bit_form";
            this.txtbx_bit_form.ReadOnly = true;
            this.txtbx_bit_form.Size = new System.Drawing.Size(220, 91);
            this.txtbx_bit_form.TabIndex = 1;
            // 
            // txtbx_byte_form
            // 
            this.txtbx_byte_form.Location = new System.Drawing.Point(12, 122);
            this.txtbx_byte_form.Multiline = true;
            this.txtbx_byte_form.Name = "txtbx_byte_form";
            this.txtbx_byte_form.ReadOnly = true;
            this.txtbx_byte_form.Size = new System.Drawing.Size(227, 91);
            this.txtbx_byte_form.TabIndex = 1;
            // 
            // txtbx_text_form
            // 
            this.txtbx_text_form.Location = new System.Drawing.Point(12, 12);
            this.txtbx_text_form.Multiline = true;
            this.txtbx_text_form.Name = "txtbx_text_form";
            this.txtbx_text_form.ReadOnly = true;
            this.txtbx_text_form.Size = new System.Drawing.Size(227, 80);
            this.txtbx_text_form.TabIndex = 1;
            // 
            // txtbx_hash
            // 
            this.txtbx_hash.Location = new System.Drawing.Point(19, 269);
            this.txtbx_hash.Multiline = true;
            this.txtbx_hash.Name = "txtbx_hash";
            this.txtbx_hash.ReadOnly = true;
            this.txtbx_hash.Size = new System.Drawing.Size(594, 64);
            this.txtbx_hash.TabIndex = 1;
            // 
            // btn_hash
            // 
            this.btn_hash.Location = new System.Drawing.Point(275, 238);
            this.btn_hash.Name = "btn_hash";
            this.btn_hash.Size = new System.Drawing.Size(87, 23);
            this.btn_hash.TabIndex = 0;
            this.btn_hash.Text = "btn_hash";
            this.btn_hash.UseVisualStyleBackColor = true;
            this.btn_hash.Click += new System.EventHandler(this.btn_hash_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 385);
            this.Controls.Add(this.txtbx_text_form);
            this.Controls.Add(this.txtbx_byte_form);
            this.Controls.Add(this.txtbx_hash);
            this.Controls.Add(this.txtbx_bit_form);
            this.Controls.Add(this.txtbx_path);
            this.Controls.Add(this.btn_hash);
            this.Controls.Add(this.btn_open_file);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open_file;
        private System.Windows.Forms.TextBox txtbx_path;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtbx_bit_form;
        private System.Windows.Forms.TextBox txtbx_byte_form;
        private System.Windows.Forms.TextBox txtbx_text_form;
        private System.Windows.Forms.TextBox txtbx_hash;
        private System.Windows.Forms.Button btn_hash;
    }
}

