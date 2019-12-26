using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ���ӵ��ݱ༭	�����ʾ��
	/// ���ߣ� ����
	/// ����ʱ�䣺 2005-01-11
	/// </summary>
	public class frmOrderAttachEdit : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		#region �ؼ�����
		internal System.Windows.Forms.Label m_lblPATIENTNAME_CHR;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.ComboBox m_cboMAZUI_CHR;
		internal System.Windows.Forms.TextBox m_txtDESC_VCHR;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCanCel;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Label m_lblPSTATUS_CHR;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button cmdDel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.Label m_lblSEX_CHR;
		internal System.Windows.Forms.Label m_lblINPATIENTID_CHR;
		internal System.Windows.Forms.Label m_lblIDCARD_CHR;
		private System.Windows.Forms.Button cmdCommit;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// ҽ��ID
		/// </summary>
		internal string m_strOrderID ="";
		/// <summary>
		/// ��½��Ϣ
		/// </summary>
		internal weCare.Core.Entity.clsLoginInfo m_objLoginInfo=null;
		/// <summary>
		/// �༭״̬{0=����;1=�༭;2=ֻ��;}
		/// </summary>
		internal int m_intEditState=0;
		/// <summary>
		/// ����ID
		/// </summary>
		internal string m_strPatientID ="";
		/// <summary>
		/// ���ӵ���ID
		/// </summary>
		internal string m_strAttachID ="";
		#endregion 

		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public frmOrderAttachEdit()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="p_strPatientID">����ID</param>
		/// <param name="p_strOrderID">ҽ����ID</param>
		/// <param name="p_strAttachID">���ӵ���ID</param>
		/// <param name="p_intEditState">�༭״̬{0=����;1=�༭;2=ֻ��;}</param>
		public frmOrderAttachEdit(string p_strPatientID,string p_strOrderID,string p_strAttachID,int p_intEditState)
		{
			m_strPatientID =p_strPatientID;
			m_strOrderID =p_strOrderID;
			m_strAttachID =p_strAttachID;
			m_intEditState =p_intEditState;
			InitializeComponent();
		}
		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.m_lblPATIENTNAME_CHR = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_cboMAZUI_CHR = new System.Windows.Forms.ComboBox();
			this.m_txtDESC_VCHR = new System.Windows.Forms.TextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCanCel = new System.Windows.Forms.Button();
			this.m_lblPSTATUS_CHR = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.m_lblSEX_CHR = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.m_lblIDCARD_CHR = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_lblINPATIENTID_CHR = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdDel = new System.Windows.Forms.Button();
			this.cmdCommit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_lblPATIENTNAME_CHR
			// 
			this.m_lblPATIENTNAME_CHR.BackColor = System.Drawing.SystemColors.Control;
			this.m_lblPATIENTNAME_CHR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblPATIENTNAME_CHR.Font = new System.Drawing.Font("����", 10.5F);
			this.m_lblPATIENTNAME_CHR.ForeColor = System.Drawing.SystemColors.ControlText;
			this.m_lblPATIENTNAME_CHR.Location = new System.Drawing.Point(48, 21);
			this.m_lblPATIENTNAME_CHR.Name = "m_lblPATIENTNAME_CHR";
			this.m_lblPATIENTNAME_CHR.Size = new System.Drawing.Size(88, 22);
			this.m_lblPATIENTNAME_CHR.TabIndex = 1;
			this.m_lblPATIENTNAME_CHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 19);
			this.label2.TabIndex = 0;
			this.label2.Text = "����ʽ: ";
			// 
			// m_cboMAZUI_CHR
			// 
			this.m_cboMAZUI_CHR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboMAZUI_CHR.Items.AddRange(new object[] {
																"����ʽ1",
																"����ʽ2",
																"����ʽ3",
																"����ʽ4",
																"����ʽ5",
																"����ʽ6"});
			this.m_cboMAZUI_CHR.Location = new System.Drawing.Point(80, 21);
			this.m_cboMAZUI_CHR.Name = "m_cboMAZUI_CHR";
			this.m_cboMAZUI_CHR.Size = new System.Drawing.Size(120, 22);
			this.m_cboMAZUI_CHR.TabIndex = 2;
			// 
			// m_txtDESC_VCHR
			// 
			this.m_txtDESC_VCHR.Location = new System.Drawing.Point(3, 53);
			this.m_txtDESC_VCHR.Multiline = true;
			this.m_txtDESC_VCHR.Name = "m_txtDESC_VCHR";
			this.m_txtDESC_VCHR.Size = new System.Drawing.Size(530, 128);
			this.m_txtDESC_VCHR.TabIndex = 3;
			this.m_txtDESC_VCHR.Text = "ҽ�����ӵ���-����ʽ��ע";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(376, 16);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 32);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "��|��";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCanCel
			// 
			this.cmdCanCel.Location = new System.Drawing.Point(456, 56);
			this.cmdCanCel.Name = "cmdCanCel";
			this.cmdCanCel.Size = new System.Drawing.Size(72, 32);
			this.cmdCanCel.TabIndex = 5;
			this.cmdCanCel.Text = "ȡ��";
			this.cmdCanCel.Click += new System.EventHandler(this.cmdCanCel_Click);
			// 
			// m_lblPSTATUS_CHR
			// 
			this.m_lblPSTATUS_CHR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblPSTATUS_CHR.Location = new System.Drawing.Point(256, 20);
			this.m_lblPSTATUS_CHR.Name = "m_lblPSTATUS_CHR";
			this.m_lblPSTATUS_CHR.Size = new System.Drawing.Size(104, 23);
			this.m_lblPSTATUS_CHR.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(208, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 19);
			this.label4.TabIndex = 0;
			this.label4.Text = "״̬:  ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_lblPATIENTNAME_CHR);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.m_lblSEX_CHR);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.m_lblIDCARD_CHR);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.m_lblINPATIENTID_CHR);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 88);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "������Ϣ: ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 19);
			this.label1.TabIndex = 2;
			this.label1.Text = "������";
			// 
			// m_lblSEX_CHR
			// 
			this.m_lblSEX_CHR.BackColor = System.Drawing.SystemColors.Control;
			this.m_lblSEX_CHR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblSEX_CHR.Font = new System.Drawing.Font("����", 10.5F);
			this.m_lblSEX_CHR.ForeColor = System.Drawing.SystemColors.ControlText;
			this.m_lblSEX_CHR.Location = new System.Drawing.Point(48, 56);
			this.m_lblSEX_CHR.Name = "m_lblSEX_CHR";
			this.m_lblSEX_CHR.Size = new System.Drawing.Size(48, 22);
			this.m_lblSEX_CHR.TabIndex = 1;
			this.m_lblSEX_CHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 59);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 19);
			this.label5.TabIndex = 2;
			this.label5.Text = "�Ա�";
			// 
			// m_lblIDCARD_CHR
			// 
			this.m_lblIDCARD_CHR.BackColor = System.Drawing.SystemColors.Control;
			this.m_lblIDCARD_CHR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblIDCARD_CHR.Font = new System.Drawing.Font("����", 10.5F);
			this.m_lblIDCARD_CHR.ForeColor = System.Drawing.SystemColors.ControlText;
			this.m_lblIDCARD_CHR.Location = new System.Drawing.Point(208, 21);
			this.m_lblIDCARD_CHR.Name = "m_lblIDCARD_CHR";
			this.m_lblIDCARD_CHR.Size = new System.Drawing.Size(144, 22);
			this.m_lblIDCARD_CHR.TabIndex = 1;
			this.m_lblIDCARD_CHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(144, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 19);
			this.label7.TabIndex = 2;
			this.label7.Text = "���֤�ţ�";
			// 
			// m_lblINPATIENTID_CHR
			// 
			this.m_lblINPATIENTID_CHR.BackColor = System.Drawing.SystemColors.Control;
			this.m_lblINPATIENTID_CHR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblINPATIENTID_CHR.Font = new System.Drawing.Font("����", 10.5F);
			this.m_lblINPATIENTID_CHR.ForeColor = System.Drawing.SystemColors.ControlText;
			this.m_lblINPATIENTID_CHR.Location = new System.Drawing.Point(208, 56);
			this.m_lblINPATIENTID_CHR.Name = "m_lblINPATIENTID_CHR";
			this.m_lblINPATIENTID_CHR.Size = new System.Drawing.Size(144, 22);
			this.m_lblINPATIENTID_CHR.TabIndex = 1;
			this.m_lblINPATIENTID_CHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(158, 59);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63, 19);
			this.label9.TabIndex = 2;
			this.label9.Text = "סԺ�ţ�";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.m_cboMAZUI_CHR);
			this.groupBox2.Controls.Add(this.m_lblPSTATUS_CHR);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.m_txtDESC_VCHR);
			this.groupBox2.Location = new System.Drawing.Point(0, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(536, 184);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "���ӵ�����Ϣ��";
			// 
			// cmdDel
			// 
			this.cmdDel.Location = new System.Drawing.Point(376, 56);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Size = new System.Drawing.Size(72, 32);
			this.cmdDel.TabIndex = 4;
			this.cmdDel.Text = "ɾ��";
			this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
			// 
			// cmdCommit
			// 
			this.cmdCommit.Location = new System.Drawing.Point(456, 16);
			this.cmdCommit.Name = "cmdCommit";
			this.cmdCommit.Size = new System.Drawing.Size(72, 32);
			this.cmdCommit.TabIndex = 8;
			this.cmdCommit.Text = "�ύ";
			this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
			// 
			// frmOrderAttachEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(536, 285);
			this.Controls.Add(this.cmdCommit);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.cmdCanCel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdDel);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("����", 10.5F);
			this.Name = "frmOrderAttachEdit";
			this.Text = "ҽ�����ӵ���";
			this.Load += new System.EventHandler(this.frmOrderAttachEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_OrderAttachEdit();
			objController.Set_GUI_Apperance(this);
		}

		#region �¼�
		private void frmOrderAttachEdit_Load(object sender, System.EventArgs e)
		{
			m_objLoginInfo = this.LoginInfo;
			((clsCtl_OrderAttachEdit)this.objController).m_strOperatorID =m_objLoginInfo.m_strEmpID;
			((clsCtl_OrderAttachEdit)this.objController).m_LoadData();
			switch(m_intEditState)
			{
				case 0://����
					this.Text ="����ҽ�����ӵ���";
					break;
				case 1://�༭
					this.Text ="�༭ҽ�����ӵ���";
					break;
				case 2://ֻ��
					this.Text ="�쿴ҽ�����ӵ���";
					((clsCtl_OrderAttachEdit)this.objController).m_SetReadOnly();
					break;
				default://ֻ��
					((clsCtl_OrderAttachEdit)this.objController).m_SetReadOnly();
					break;
			}
		}

		private void cmdCanCel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			((clsCtl_OrderAttachEdit)this.objController).m_OK();
		}

		private void cmdDel_Click(object sender, System.EventArgs e)
		{
			((clsCtl_OrderAttachEdit)this.objController).m_Del();		
		}

		private void cmdCommit_Click(object sender, System.EventArgs e)
		{
			((clsCtl_OrderAttachEdit)this.objController).m_Commit();
		}
		#endregion

		#region ����	������	[ִ��ҽ��ʱ�Ա㸽�ӵ����Զ��ύ] 
		/// <summary>
		/// �ύҽ�����ӵ���	����ҽ��ID
		/// </summary>
		/// <param name="p_strOrderID">ҽ��ID</param>
		public long m_lngCommitAttachOrder(string p_strOrderID)
		{
			long lngRes =0;
			clsDcl_ExecuteOrder objTem =new clsDcl_ExecuteOrder();
			try
			{
				lngRes =objTem.m_lngCommitAttachOrder(p_strOrderID);
			}
			catch{}
			return lngRes;
		}
		#endregion 
	}
}
