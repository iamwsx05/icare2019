using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Summary description for AutoEvalResult.
	/// </summary>
	public class frmAutoEvalResult : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdClearData;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ListView lsvEvalResult;
		private System.Windows.Forms.CheckBox chkAllMostOnTop;
		private System.Windows.Forms.ColumnHeader chCollectTime;
		private clsBorderTool m_objBorderTool;
		private PinkieControls.ButtonXP cmdCLear;
		private PinkieControls.ButtonXP cmdExit;
        private Label m_lblTitle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		private System.ComponentModel.Container components = null;

		public frmAutoEvalResult(params ColumnHeader[] chEvalResults)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			lsvEvalResult.Columns.AddRange(chEvalResults);

			m_objBorderTool = new clsBorderTool(Color.White);
			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
																			
																			 lsvEvalResult
																		 });
		}

        public string M_lblTitle
        {
            set { m_lblTitle.Text = value; }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutoEvalResult));
            this.lsvEvalResult = new System.Windows.Forms.ListView();
            this.chCollectTime = new System.Windows.Forms.ColumnHeader();
            this.cmdClearData = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.chkAllMostOnTop = new System.Windows.Forms.CheckBox();
            this.cmdCLear = new PinkieControls.ButtonXP();
            this.cmdExit = new PinkieControls.ButtonXP();
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lsvEvalResult
            // 
            this.lsvEvalResult.BackColor = System.Drawing.Color.White;
            this.lsvEvalResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvEvalResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCollectTime});
            this.lsvEvalResult.ForeColor = System.Drawing.Color.Black;
            this.lsvEvalResult.FullRowSelect = true;
            this.lsvEvalResult.GridLines = true;
            this.lsvEvalResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvEvalResult.Location = new System.Drawing.Point(4, 72);
            this.lsvEvalResult.Name = "lsvEvalResult";
            this.lsvEvalResult.Size = new System.Drawing.Size(1016, 628);
            this.lsvEvalResult.TabIndex = 12;
            this.lsvEvalResult.UseCompatibleStateImageBehavior = false;
            this.lsvEvalResult.View = System.Windows.Forms.View.Details;
            // 
            // chCollectTime
            // 
            this.chCollectTime.Text = "   采  集  时  间";
            this.chCollectTime.Width = 180;
            // 
            // cmdClearData
            // 
            this.cmdClearData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdClearData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClearData.ForeColor = System.Drawing.Color.White;
            this.cmdClearData.Image = ((System.Drawing.Image)(resources.GetObject("cmdClearData.Image")));
            this.cmdClearData.Location = new System.Drawing.Point(750, 12);
            this.cmdClearData.Name = "cmdClearData";
            this.cmdClearData.Size = new System.Drawing.Size(48, 48);
            this.cmdClearData.TabIndex = 399;
            this.cmdClearData.Visible = false;
            this.cmdClearData.Click += new System.EventHandler(this.cmdClearData_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.ForeColor = System.Drawing.Color.White;
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.Location = new System.Drawing.Point(804, 12);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(48, 48);
            this.cmdClose.TabIndex = 399;
            this.cmdClose.Visible = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // chkAllMostOnTop
            // 
            this.chkAllMostOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAllMostOnTop.ForeColor = System.Drawing.Color.Black;
            this.chkAllMostOnTop.Location = new System.Drawing.Point(616, 32);
            this.chkAllMostOnTop.Name = "chkAllMostOnTop";
            this.chkAllMostOnTop.Size = new System.Drawing.Size(116, 24);
            this.chkAllMostOnTop.TabIndex = 400;
            this.chkAllMostOnTop.Text = "总在最上方";
            this.chkAllMostOnTop.CheckedChanged += new System.EventHandler(this.chkAllMostOnTop_CheckedChanged);
            // 
            // cmdCLear
            // 
            this.cmdCLear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdCLear.DefaultScheme = true;
            this.cmdCLear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdCLear.ForeColor = System.Drawing.Color.Black;
            this.cmdCLear.Hint = "";
            this.cmdCLear.Location = new System.Drawing.Point(208, 24);
            this.cmdCLear.Name = "cmdCLear";
            this.cmdCLear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCLear.Size = new System.Drawing.Size(96, 32);
            this.cmdCLear.TabIndex = 10000019;
            this.cmdCLear.Text = "清  除(&C)";
            this.cmdCLear.Click += new System.EventHandler(this.cmdCLear_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.cmdExit.DefaultScheme = true;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdExit.ForeColor = System.Drawing.Color.Black;
            this.cmdExit.Hint = "";
            this.cmdExit.Location = new System.Drawing.Point(312, 24);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdExit.Size = new System.Drawing.Size(96, 32);
            this.cmdExit.TabIndex = 10000018;
            this.cmdExit.Text = "退  出(&E)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblTitle.Location = new System.Drawing.Point(48, 32);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(76, 16);
            this.m_lblTitle.TabIndex = 10000020;
            this.m_lblTitle.Text = "自动评分";
            // 
            // AutoEvalResult
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.m_lblTitle);
            this.Controls.Add(this.cmdCLear);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.chkAllMostOnTop);
            this.Controls.Add(this.cmdClearData);
            this.Controls.Add(this.lsvEvalResult);
            this.Controls.Add(this.cmdClose);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AutoEvalResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AutoEvalResult_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		private void cmdClearData_Click(object sender, System.EventArgs e)
		{
			lock(this)
			{
				lsvEvalResult.Items.Clear();
			}			
		}
		
		public void AddResult(ListViewItem item)
		{
			lsvEvalResult.Items.Add(item);
		}

		private void chkAllMostOnTop_CheckedChanged(object sender, System.EventArgs e)
		{
			this.TopMost = chkAllMostOnTop.Checked;
		}

		private void AutoEvalResult_Load(object sender, System.EventArgs e)
		{
			lsvEvalResult.Focus();
		}

		private void cmdCLear_Click(object sender, System.EventArgs e)
		{
			cmdClearData_Click(sender,e);
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			cmdClose_Click(sender,e);
		}
	}
}
