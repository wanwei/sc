namespace com.wer.sc.comp.test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btCandle = new System.Windows.Forms.Button();
            this.btTest = new System.Windows.Forms.Button();
            this.btCandle2 = new System.Windows.Forms.Button();
            this.btAna = new System.Windows.Forms.Button();
            this.btReal = new System.Windows.Forms.Button();
            this.btModel = new System.Windows.Forms.Button();
            this.btAna2 = new System.Windows.Forms.Button();
            this.btAna3 = new System.Windows.Forms.Button();
            this.btCandle3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btCandle
            // 
            this.btCandle.Location = new System.Drawing.Point(57, 41);
            this.btCandle.Name = "btCandle";
            this.btCandle.Size = new System.Drawing.Size(75, 23);
            this.btCandle.TabIndex = 0;
            this.btCandle.Text = "蜡烛图";
            this.btCandle.UseVisualStyleBackColor = true;
            this.btCandle.Click += new System.EventHandler(this.btCandle_Click);
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(57, 12);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 1;
            this.btTest.Text = "测试画图";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // btCandle2
            // 
            this.btCandle2.Location = new System.Drawing.Point(138, 41);
            this.btCandle2.Name = "btCandle2";
            this.btCandle2.Size = new System.Drawing.Size(75, 23);
            this.btCandle2.TabIndex = 2;
            this.btCandle2.Text = "蜡烛图2";
            this.btCandle2.UseVisualStyleBackColor = true;
            this.btCandle2.Click += new System.EventHandler(this.btCandle2_Click);
            // 
            // btAna
            // 
            this.btAna.Location = new System.Drawing.Point(57, 86);
            this.btAna.Name = "btAna";
            this.btAna.Size = new System.Drawing.Size(75, 23);
            this.btAna.TabIndex = 3;
            this.btAna.Text = "分析";
            this.btAna.UseVisualStyleBackColor = true;
            this.btAna.Click += new System.EventHandler(this.btAna_Click);
            // 
            // btReal
            // 
            this.btReal.Location = new System.Drawing.Point(57, 131);
            this.btReal.Name = "btReal";
            this.btReal.Size = new System.Drawing.Size(75, 23);
            this.btReal.TabIndex = 4;
            this.btReal.Text = "分时图";
            this.btReal.UseVisualStyleBackColor = true;
            this.btReal.Click += new System.EventHandler(this.btReal_Click);
            // 
            // btModel
            // 
            this.btModel.Location = new System.Drawing.Point(57, 175);
            this.btModel.Name = "btModel";
            this.btModel.Size = new System.Drawing.Size(75, 23);
            this.btModel.TabIndex = 5;
            this.btModel.Text = "模型树";
            this.btModel.UseVisualStyleBackColor = true;
            this.btModel.Click += new System.EventHandler(this.btModel_Click);
            // 
            // btAna2
            // 
            this.btAna2.Location = new System.Drawing.Point(138, 86);
            this.btAna2.Name = "btAna2";
            this.btAna2.Size = new System.Drawing.Size(75, 23);
            this.btAna2.TabIndex = 6;
            this.btAna2.Text = "分析2";
            this.btAna2.UseVisualStyleBackColor = true;
            this.btAna2.Click += new System.EventHandler(this.btAna2_Click);
            // 
            // btAna3
            // 
            this.btAna3.Location = new System.Drawing.Point(219, 86);
            this.btAna3.Name = "btAna3";
            this.btAna3.Size = new System.Drawing.Size(75, 23);
            this.btAna3.TabIndex = 7;
            this.btAna3.Text = "分析3";
            this.btAna3.UseVisualStyleBackColor = true;
            this.btAna3.Click += new System.EventHandler(this.btAna3_Click);
            // 
            // btCandle3
            // 
            this.btCandle3.Location = new System.Drawing.Point(219, 41);
            this.btCandle3.Name = "btCandle3";
            this.btCandle3.Size = new System.Drawing.Size(75, 23);
            this.btCandle3.TabIndex = 8;
            this.btCandle3.Text = "蜡烛图3";
            this.btCandle3.UseVisualStyleBackColor = true;
            this.btCandle3.Click += new System.EventHandler(this.btCandle3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 327);
            this.Controls.Add(this.btCandle3);
            this.Controls.Add(this.btAna3);
            this.Controls.Add(this.btAna2);
            this.Controls.Add(this.btModel);
            this.Controls.Add(this.btReal);
            this.Controls.Add(this.btAna);
            this.Controls.Add(this.btCandle2);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.btCandle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCandle;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Button btCandle2;
        private System.Windows.Forms.Button btAna;
        private System.Windows.Forms.Button btReal;
        private System.Windows.Forms.Button btModel;
        private System.Windows.Forms.Button btAna2;
        private System.Windows.Forms.Button btAna3;
        private System.Windows.Forms.Button btCandle3;
    }
}

