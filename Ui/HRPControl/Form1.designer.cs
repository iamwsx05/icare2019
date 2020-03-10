namespace ApplicationTest
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ctlPaintContainer1 = new com.digitalwave.Utility.Controls.ctlPaintContainer();
            this.SuspendLayout();
            // 
            // ctlPaintContainer1
            // 
            this.ctlPaintContainer1.AutoScroll = true;
            this.ctlPaintContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ctlPaintContainer1.ForeColor = System.Drawing.Color.White;
            this.ctlPaintContainer1.Location = new System.Drawing.Point(119, 87);
            this.ctlPaintContainer1.m_BlnCanAddImage = true;
            this.ctlPaintContainer1.m_BlnScaleSize = true;
            this.ctlPaintContainer1.m_ClrcmdRubber = System.Drawing.Color.Silver;
            this.ctlPaintContainer1.m_ClrcmdSelected = System.Drawing.Color.White;
            this.ctlPaintContainer1.m_ClrgpbTools = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ctlPaintContainer1.m_ClrppgPicSize = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.ctlPaintContainer1.m_ClrrdbDash = System.Drawing.Color.Silver;
            this.ctlPaintContainer1.m_ClrrdbLine = System.Drawing.Color.Silver;
            this.ctlPaintContainer1.m_ClrrdbPen = System.Drawing.Color.Silver;
            this.ctlPaintContainer1.m_ClrrdbText = System.Drawing.Color.Silver;
            this.ctlPaintContainer1.m_IntDefaultHeight = 253;
            this.ctlPaintContainer1.m_IntDefaultWidth = 320;
            this.ctlPaintContainer1.Name = "ctlPaintContainer1";
            this.ctlPaintContainer1.Size = new System.Drawing.Size(444, 264);
            this.ctlPaintContainer1.TabIndex = 0;
            this.ctlPaintContainer1.选择科室图片 = com.digitalwave.Utility.Controls.ctlPaintContainer.enmImageNames.无;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(679, 678);
            this.Controls.Add(this.ctlPaintContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private com.digitalwave.Utility.Controls.ctlPaintContainer ctlPaintContainer1;





    }
}

