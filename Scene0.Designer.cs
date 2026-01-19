namespace NOVEL_
{
    partial class Scene0
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene0));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.zag_sc1 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-8, -4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(815, 460);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // zag_sc1
            // 
            this.zag_sc1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.zag_sc1.BackColor = System.Drawing.Color.Transparent;
            this.zag_sc1.ForeColor = System.Drawing.SystemColors.Info;
            this.zag_sc1.Location = new System.Drawing.Point(284, 108);
            this.zag_sc1.Name = "zag_sc1";
            this.zag_sc1.Size = new System.Drawing.Size(180, 16);
            this.zag_sc1.TabIndex = 1;
            this.zag_sc1.Text = "«Тайна особняка Воронцовых»";
            this.zag_sc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zag_sc1.Click += new System.EventHandler(this.zag_sc1_Click);
            // 
            // btn_start
            // 
            this.btn_start.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_start.Location = new System.Drawing.Point(249, 251);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(251, 95);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "НАЧАТЬ РАССЛЕДОВАНИЕ";
            this.btn_start.UseVisualStyleBackColor = true;
            // 
            // Scene0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.zag_sc1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Scene0";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label zag_sc1;
        private System.Windows.Forms.Button btn_start;
    }
}

