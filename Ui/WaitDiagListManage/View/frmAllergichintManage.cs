using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmAllergichintManage 的摘要说明。
	/// </summary>
	public class frmAllergichintManage : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region sys define
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public com.digitalwave.controls.ctlRichTextBox m_txtMed;
		private com.digitalwave.controls.ctlRichTextBox m_txtCase;
		private System.Windows.Forms.PictureBox m_cmdAdd;
		private System.ComponentModel.IContainer components;
        private System.Windows.Forms.PictureBox m_cmdDEL;
		/// <summary>
		/// 用于显示警告信息位置
		/// </summary>
		public ListView m_listView_Patient;
        #endregion 
      
        #region user define
        private clsT_opr_allergic m_objRecord;
        private int m_intRecordCount;
         frmAllergichint m_frmAllergichintShow = null;
        /// <summary>
        /// 取得过敏药物信息
        /// </summary>
         public string  ALLERGICMED_VCHR
         {
             get
             {
                 if (m_objRecord != null)
                 {
                     return m_objRecord.m_strALLERGICMED_VCHR;
                 }
                 else
                     return "";
             }
         }
	
        #endregion 

        #region 护士工作站原用的构造函数
        /// <summary>
		/// 护士工作站原用的构造函数
		/// </summary>
		/// <param name="p_objRecord">VO</param>
		/// <param name="p_strALLERGICDESC">药物过敏</param>
		/// <param name="p_intRecordCount">药物表中对应的记录数</param>
        public frmAllergichintManage(clsT_opr_allergic p_objRecord, int p_intRecordCount)
        {

            InitializeComponent();
            m_objRecord = p_objRecord;
            m_intRecordCount = p_intRecordCount;
            this.m_txtMed.Text = p_objRecord.m_strALLERGICMED_VCHR.Trim();
            this.m_txtCase.Text = p_objRecord.m_strALLERGICDESC_VCHR.Trim();
            if (m_objRecord.m_intSTATUS_INT == 1)
            {
                this.m_txtMed.Enabled = false;
                this.m_txtCase.Enabled = false;
            }
        }
        #endregion 

        #region 根据病人ID与处方号,取出过敏表信息,当处方号为空的时候,根据病人ID取最后一条信息
        /// <summary>
        /// 根据病人ID与处方号,取出过敏表信息,当处方号为空的时候,根据病人ID取最后一条信息
        /// </summary>
        /// <param name="p_strPATIENTID_CHR">用户ID</param>
        /// <param name="p_strOUTPATRECIPEID_CHR">处方号</param>
        public frmAllergichintManage(string p_strPATIENTID_CHR, string p_strOUTPATRECIPEID_CHR)
        {
            InitializeComponent();

            clsT_opr_allergic objRecord = null;
            int intCount = 0;
            clsDcl_injectInfo objSvc = new clsDcl_injectInfo();
            long lngres = objSvc.m_lngGetAllergicByPidOutPid(p_strPATIENTID_CHR, p_strOUTPATRECIPEID_CHR, out objRecord, out intCount);
            if (lngres <= 0)
            {
                return;
            }
            else
            {
                if (intCount == 0)//过敏表无记录,从基本表拿过敏信息
                {
                    string p_strIFALLERGIC = "";
                    string p_strALLERGICDESC = "";
                    lngres = objSvc.m_lngGetAllergicByPidFromTBSEPatient(p_strPATIENTID_CHR, out p_strIFALLERGIC, out p_strALLERGICDESC);
                    if (lngres > 0)
                    {
                        if (objRecord == null)
                            objRecord = new clsT_opr_allergic();

                        objRecord.m_intSTATUS_INT = 0;
                        objRecord.m_strPATIENTID_CHR = p_strPATIENTID_CHR;
                        objRecord.m_strOUTPATRECIPEID_CHR = p_strOUTPATRECIPEID_CHR;
                        objRecord.m_strALLERGICMED_VCHR = p_strALLERGICDESC;
                        objRecord.m_strALLERGICDESC_VCHR = "";
                    }
                }
                m_objRecord = objRecord;
                m_intRecordCount = intCount;
                this.m_txtMed.Text = objRecord.m_strALLERGICMED_VCHR.Trim();
                this.m_txtCase.Text = objRecord.m_strALLERGICDESC_VCHR.Trim();
                if (m_objRecord.m_intSTATUS_INT == 1)
                {
                    this.m_txtMed.Enabled = false;
                    this.m_txtCase.Enabled = false;
                }
            }
        }
        #endregion 

        #region dispose
        /// <summary>
		/// 清理所有正在使用的资源。
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
        #endregion 

        #region Windows 窗体设计器生成的代码
        /// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAllergichintManage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtCase = new com.digitalwave.controls.ctlRichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtMed = new com.digitalwave.controls.ctlRichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmdDEL = new System.Windows.Forms.PictureBox();
            this.m_cmdAdd = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmdDEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmdAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_txtCase);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_txtMed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_cmdDEL);
            this.panel1.Controls.Add(this.m_cmdAdd);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 455);
            this.panel1.TabIndex = 0;
            // 
            // m_txtCase
            // 
            this.m_txtCase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtCase.Location = new System.Drawing.Point(24, 264);
            this.m_txtCase.m_BlnIgnoreUserInfo = true;
            this.m_txtCase.m_BlnPartControl = false;
            this.m_txtCase.m_BlnReadOnly = false;
            this.m_txtCase.m_BlnUnderLineDST = false;
            this.m_txtCase.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtCase.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtCase.m_IntCanModifyTime = 500;
            this.m_txtCase.m_IntPartControlLength = 0;
            this.m_txtCase.m_IntPartControlStartIndex = 0;
            this.m_txtCase.m_StrUserID = "";
            this.m_txtCase.m_StrUserName = "";
            this.m_txtCase.Name = "m_txtCase";
            this.m_txtCase.Size = new System.Drawing.Size(544, 176);
            this.m_txtCase.TabIndex = 7;
            this.m_txtCase.Text = "";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(24, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "过敏情况:";
            // 
            // m_txtMed
            // 
            this.m_txtMed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtMed.Location = new System.Drawing.Point(24, 64);
            this.m_txtMed.m_BlnIgnoreUserInfo = true;
            this.m_txtMed.m_BlnPartControl = false;
            this.m_txtMed.m_BlnReadOnly = false;
            this.m_txtMed.m_BlnUnderLineDST = false;
            this.m_txtMed.m_ClrDST = System.Drawing.Color.Red;
            this.m_txtMed.m_ClrOldPartInsertText = System.Drawing.Color.Black;
            this.m_txtMed.m_IntCanModifyTime = 500;
            this.m_txtMed.m_IntPartControlLength = 0;
            this.m_txtMed.m_IntPartControlStartIndex = 0;
            this.m_txtMed.m_StrUserID = "";
            this.m_txtMed.m_StrUserName = "";
            this.m_txtMed.Name = "m_txtMed";
            this.m_txtMed.Size = new System.Drawing.Size(544, 168);
            this.m_txtMed.TabIndex = 5;
            this.m_txtMed.Text = "";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "过敏信息:";
            // 
            // m_cmdDEL
            // 
            this.m_cmdDEL.BackColor = System.Drawing.Color.White;
            this.m_cmdDEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_cmdDEL.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdDEL.Image")));
            this.m_cmdDEL.Location = new System.Drawing.Point(566, 8);
            this.m_cmdDEL.Name = "m_cmdDEL";
            this.m_cmdDEL.Size = new System.Drawing.Size(24, 24);
            this.m_cmdDEL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_cmdDEL.TabIndex = 3;
            this.m_cmdDEL.TabStop = false;
            this.toolTip1.SetToolTip(this.m_cmdDEL, "关闭(ESC)");
            this.m_cmdDEL.Click += new System.EventHandler(this.m_cmdDEL_Click);
            // 
            // m_cmdAdd
            // 
            this.m_cmdAdd.BackColor = System.Drawing.Color.White;
            this.m_cmdAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdAdd.Image")));
            this.m_cmdAdd.Location = new System.Drawing.Point(24, 8);
            this.m_cmdAdd.Name = "m_cmdAdd";
            this.m_cmdAdd.Size = new System.Drawing.Size(24, 24);
            this.m_cmdAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_cmdAdd.TabIndex = 1;
            this.m_cmdAdd.TabStop = false;
            this.toolTip1.SetToolTip(this.m_cmdAdd, "写入数据库(Ctrl+S)");
            this.m_cmdAdd.Click += new System.EventHandler(this.m_cmdAdd_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(56, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "删除此记录(Ctrl+D)");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmAllergichintManage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(600, 464);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmAllergichintManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAllergichintManage";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAllergichintManage_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_cmdDEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmdAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        #region delete
        private void pictureBox1_Click(object sender, System.EventArgs e)
		{
            DeleteRecordNewMethod();
        }

        private void DeleteRecordNewMethod()
        {
            if (this.m_intRecordCount > 0)
            {
                if (this.LoginInfo.m_strEmpID == this.m_objRecord.m_strCREATEEMPID_CHR.Trim())
                {
                    if (this.m_objRecord.m_intSTATUS_INT == 0)
                    {
                        long lngRes = -1;
                        clsDcl_injectInfo clsDcl = new clsDcl_injectInfo();
                        lngRes = clsDcl.m_lngDeleteAllergic(this.m_objRecord.m_strPATIENTID_CHR,m_objRecord.m_strCREATE_DAT);
                        if (lngRes > 0)
                        {
                            this.m_mthShowTip(this.pictureBox1, "操作成功");
                            this.Close();
                        }
                    }
                    else
                    {
                        this.m_mthShowTip(this.pictureBox1, "无权操作医生已确认的记录");
                    }
                }
                else
                {
                    this.m_mthShowTip(this.pictureBox1, "无权操作其它用户创建的记录");
                }
            }
            else
            {
                this.m_mthShowTip(this.m_cmdAdd, "记录无保存,无法删除.");

            }
        }
        #endregion 

        #region add or update
        private void m_cmdAdd_Click(object sender, System.EventArgs e)
		{
            AddOrUpdateNewMethod();
        }

        private void AddOrUpdateNewMethod()
        {
            if (this.m_objRecord.m_intSTATUS_INT == 0)
            {
                long lngRes = -1;
                clsDcl_injectInfo clsDcl = new clsDcl_injectInfo();
                this.m_objRecord.m_strCREATEEMPID_CHR = this.LoginInfo.m_strEmpID;
                this.m_objRecord.m_strALLERGICDESC_VCHR = this.m_txtCase.Text.Trim();
                this.m_objRecord.m_strALLERGICMED_VCHR = this.m_txtMed.Text.Trim();

                if (this.m_intRecordCount == 0)//新增
                {
                    lngRes = clsDcl.m_lngAddNewAllergic(this.m_objRecord);
                }
                else //更改
                {
                    lngRes = clsDcl.m_lngAlterAllergic(this.m_objRecord);
                }
                if (lngRes > 0)
                {
                    this.m_mthShowTip(this.m_cmdAdd, "操作成功");
                    this.Close();
                }
            }
            else
            {
                this.m_mthShowTip(this.m_cmdAdd, "无权操作医生已确认的记录");
            }
        }
        #endregion

        #region 辅助方法
        private void m_mthShowTip(Control p_control,string p_strWarnTips)
		{
            //com.digitalwave.iCare.gui.HIS.frmShowWarning ShowWarning = new com.digitalwave.iCare.gui.HIS.frmShowWarning();
            //ShowWarning.m_GetWaring=p_strWarnTips;
            //Point p = p_control.Parent.PointToScreen(p_control.Location);
            //ShowWarning.Location = p;
            //ShowWarning.Show();
		}
        private void frmAllergichintManage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    AddOrUpdateNewMethod();
                }
                else if (e.KeyCode == Keys.D)
                {
                    DeleteRecordNewMethod();
                }
            }
        }
        #endregion 

        private void m_cmdDEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 显示过敏
        /// <summary>
        /// 显示过敏
        /// </summary>
        public void m_mthShowfrmAllergichint()
        {
            m_mthShowfrmAllergichint(m_objRecord.m_strALLERGICMED_VCHR.Trim());
        }
        /// <summary>
        /// 显示过敏
        /// </summary>
        /// <param name="p_strALLERGICDESC"></param>
        public void m_mthShowfrmAllergichint(string p_strALLERGICDESC)
        {
            if (p_strALLERGICDESC == null)
                p_strALLERGICDESC = "";
            if (p_strALLERGICDESC != "")
            {
                if (this.m_frmAllergichintShow == null)
                {
                    this.m_frmAllergichintShow = new frmAllergichint();
                }
                this.m_frmAllergichintShow.DesktopLocation = new Point(570, 600);
                this.m_frmAllergichintShow.CONTENTTEXT = p_strALLERGICDESC;
                this.m_frmAllergichintShow.Visible = true;

            }
            else
            {
                if (this.m_frmAllergichintShow != null)
                {
                    this.m_frmAllergichintShow.Visible = false;
                }
            }
        }
        #endregion 
    }
}
