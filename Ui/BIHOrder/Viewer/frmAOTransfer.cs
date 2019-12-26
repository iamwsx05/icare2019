using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ת�����ӵ��ݱ༭	�����ʾ��
	/// ���ߣ� ����
	/// ����ʱ�䣺 2005-01-18
	/// </summary>
	public class frmAOTransfer: com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		#region �ؼ�����
		private System.Windows.Forms.GroupBox groupBox3;
		private PinkieControls.ButtonXP cmdCanCel;
		internal PinkieControls.ButtonXP cmdDel;
		internal PinkieControls.ButtonXP cmdOK;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.TextBox m_lblINPATIENTID_CHR;
		internal System.Windows.Forms.TextBox m_lblSEX_CHR;
		internal System.Windows.Forms.TextBox m_lblIDCARD_CHR;
		internal System.Windows.Forms.TextBox m_lblPATIENTNAME_CHR;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox2;
		internal System.Windows.Forms.CheckBox m_chkISACTIVE_INT;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		internal System.Windows.Forms.TextBox m_txtSOURCEAREAID_CHR;
		internal System.Windows.Forms.TextBox m_txtSOURCEBEDID_CHR;
		internal System.Windows.Forms.TextBox m_txtSTATUS_INT;
		internal System.Windows.Forms.TextBox m_txtCREATEDATE_DAT;
		internal System.Windows.Forms.TextBox m_txtACTIVEEMPID_CHR;
		internal System.Windows.Forms.TextBox m_txtACTIVEDATE_DAT;
		internal System.Windows.Forms.TextBox m_txtDES_VCHR;
		internal com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox m_txtTARGETAREAID_CHR;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// ��½��Ϣ
		/// </summary>
		internal weCare.Core.Entity.clsLoginInfo m_objLoginInfo=null;
		/// <summary>
		/// ���ӵ���ID
		/// </summary>
		internal string m_strAttachID ="";
		/// <summary>
		/// ҽ��ID
		/// </summary>
		internal string m_strOrderID ="";
		/// <summary>
		/// �༭״̬{0=����;1=�༭;2=ֻ��;}
		/// </summary>
		internal int m_intEditState=0;
		internal PinkieControls.ButtonXP cmdBecomeEffective;
		/// <summary>
		/// ����ID
		/// </summary>
		internal string m_strPatientID ="";
		#endregion 
		#region ���캯��
		public frmAOTransfer()
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
		public frmAOTransfer(string p_strPatientID,string p_strOrderID,string p_strAttachID,int p_intEditState)
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cmdCanCel = new PinkieControls.ButtonXP();
			this.cmdDel = new PinkieControls.ButtonXP();
			this.cmdBecomeEffective = new PinkieControls.ButtonXP();
			this.cmdOK = new PinkieControls.ButtonXP();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_lblINPATIENTID_CHR = new System.Windows.Forms.TextBox();
			this.m_lblSEX_CHR = new System.Windows.Forms.TextBox();
			this.m_lblIDCARD_CHR = new System.Windows.Forms.TextBox();
			this.m_lblPATIENTNAME_CHR = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.m_txtTARGETAREAID_CHR = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
			this.m_txtACTIVEEMPID_CHR = new System.Windows.Forms.TextBox();
			this.m_txtSOURCEBEDID_CHR = new System.Windows.Forms.TextBox();
			this.m_txtACTIVEDATE_DAT = new System.Windows.Forms.TextBox();
			this.m_txtSTATUS_INT = new System.Windows.Forms.TextBox();
			this.m_txtSOURCEAREAID_CHR = new System.Windows.Forms.TextBox();
			this.m_chkISACTIVE_INT = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.m_txtDES_VCHR = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_txtCREATEDATE_DAT = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cmdCanCel);
			this.groupBox3.Controls.Add(this.cmdDel);
			this.groupBox3.Controls.Add(this.cmdBecomeEffective);
			this.groupBox3.Controls.Add(this.cmdOK);
			this.groupBox3.Location = new System.Drawing.Point(368, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(168, 88);
			this.groupBox3.TabIndex = 18;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "������";
			// 
			// cmdCanCel
			// 
			this.cmdCanCel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdCanCel.DefaultScheme = true;
			this.cmdCanCel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdCanCel.Hint = "";
			this.cmdCanCel.Location = new System.Drawing.Point(84, 48);
			this.cmdCanCel.Name = "cmdCanCel";
			this.cmdCanCel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdCanCel.Size = new System.Drawing.Size(79, 30);
			this.cmdCanCel.TabIndex = 46;
			this.cmdCanCel.Text = "ȡ��(Ecs)";
			this.cmdCanCel.Click += new System.EventHandler(this.cmdCanCel_Click);
			// 
			// cmdDel
			// 
			this.cmdDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdDel.DefaultScheme = true;
			this.cmdDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdDel.Hint = "";
			this.cmdDel.Location = new System.Drawing.Point(4, 48);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdDel.Size = new System.Drawing.Size(79, 30);
			this.cmdDel.TabIndex = 45;
			this.cmdDel.Text = "ɾ��(F4)";
			this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
			// 
			// cmdBecomeEffective
			// 
			this.cmdBecomeEffective.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdBecomeEffective.DefaultScheme = true;
			this.cmdBecomeEffective.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdBecomeEffective.Enabled = false;
			this.cmdBecomeEffective.Hint = "";
			this.cmdBecomeEffective.Location = new System.Drawing.Point(84, 16);
			this.cmdBecomeEffective.Name = "cmdBecomeEffective";
			this.cmdBecomeEffective.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdBecomeEffective.Size = new System.Drawing.Size(79, 30);
			this.cmdBecomeEffective.TabIndex = 44;
			this.cmdBecomeEffective.Text = "��Ч(F3)";
			this.cmdBecomeEffective.Click += new System.EventHandler(this.cmdBecomeEffective_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.cmdOK.DefaultScheme = true;
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.cmdOK.Hint = "";
			this.cmdOK.Location = new System.Drawing.Point(4, 16);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.cmdOK.Size = new System.Drawing.Size(79, 30);
			this.cmdOK.TabIndex = 43;
			this.cmdOK.Text = "����(F2)";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_lblINPATIENTID_CHR);
			this.groupBox1.Controls.Add(this.m_lblSEX_CHR);
			this.groupBox1.Controls.Add(this.m_lblIDCARD_CHR);
			this.groupBox1.Controls.Add(this.m_lblPATIENTNAME_CHR);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 88);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "������Ϣ: ";
			// 
			// m_lblINPATIENTID_CHR
			// 
			this.m_lblINPATIENTID_CHR.Location = new System.Drawing.Point(208, 56);
			this.m_lblINPATIENTID_CHR.Name = "m_lblINPATIENTID_CHR";
			this.m_lblINPATIENTID_CHR.ReadOnly = true;
			this.m_lblINPATIENTID_CHR.Size = new System.Drawing.Size(144, 23);
			this.m_lblINPATIENTID_CHR.TabIndex = 3;
			this.m_lblINPATIENTID_CHR.Text = "";
			// 
			// m_lblSEX_CHR
			// 
			this.m_lblSEX_CHR.Location = new System.Drawing.Point(48, 56);
			this.m_lblSEX_CHR.Name = "m_lblSEX_CHR";
			this.m_lblSEX_CHR.ReadOnly = true;
			this.m_lblSEX_CHR.Size = new System.Drawing.Size(88, 23);
			this.m_lblSEX_CHR.TabIndex = 3;
			this.m_lblSEX_CHR.Text = "";
			// 
			// m_lblIDCARD_CHR
			// 
			this.m_lblIDCARD_CHR.Location = new System.Drawing.Point(208, 20);
			this.m_lblIDCARD_CHR.Name = "m_lblIDCARD_CHR";
			this.m_lblIDCARD_CHR.ReadOnly = true;
			this.m_lblIDCARD_CHR.Size = new System.Drawing.Size(144, 23);
			this.m_lblIDCARD_CHR.TabIndex = 3;
			this.m_lblIDCARD_CHR.Text = "";
			// 
			// m_lblPATIENTNAME_CHR
			// 
			this.m_lblPATIENTNAME_CHR.Location = new System.Drawing.Point(48, 20);
			this.m_lblPATIENTNAME_CHR.Name = "m_lblPATIENTNAME_CHR";
			this.m_lblPATIENTNAME_CHR.ReadOnly = true;
			this.m_lblPATIENTNAME_CHR.Size = new System.Drawing.Size(88, 23);
			this.m_lblPATIENTNAME_CHR.TabIndex = 3;
			this.m_lblPATIENTNAME_CHR.Text = "";
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
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 19);
			this.label5.TabIndex = 2;
			this.label5.Text = "�Ա�";
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
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(158, 60);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63, 19);
			this.label9.TabIndex = 2;
			this.label9.Text = "סԺ�ţ�";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.m_txtTARGETAREAID_CHR);
			this.groupBox2.Controls.Add(this.m_txtACTIVEEMPID_CHR);
			this.groupBox2.Controls.Add(this.m_txtSOURCEBEDID_CHR);
			this.groupBox2.Controls.Add(this.m_txtACTIVEDATE_DAT);
			this.groupBox2.Controls.Add(this.m_txtSTATUS_INT);
			this.groupBox2.Controls.Add(this.m_txtSOURCEAREAID_CHR);
			this.groupBox2.Controls.Add(this.m_chkISACTIVE_INT);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.m_txtDES_VCHR);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.m_txtCREATEDATE_DAT);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(0, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(536, 184);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "���ӵ�����Ϣ��";
			// 
			// m_txtTARGETAREAID_CHR
			// 
			this.m_txtTARGETAREAID_CHR.Location = new System.Drawing.Point(424, 20);
			this.m_txtTARGETAREAID_CHR.Name = "m_txtTARGETAREAID_CHR";
			this.m_txtTARGETAREAID_CHR.Size = new System.Drawing.Size(104, 23);
			this.m_txtTARGETAREAID_CHR.TabIndex = 32;
			this.m_txtTARGETAREAID_CHR.Text = "";
			this.m_txtTARGETAREAID_CHR.Leave += new System.EventHandler(this.m_txtTARGETAREAID_CHR_Leave);
			this.m_txtTARGETAREAID_CHR.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtTARGETAREAID_CHR_m_evtInitListView);
			this.m_txtTARGETAREAID_CHR.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtTARGETAREAID_CHR_m_evtSelectItem);
			this.m_txtTARGETAREAID_CHR.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.m_txtTARGETAREAID_CHR_m_evtFindItem);
			// 
			// m_txtACTIVEEMPID_CHR
			// 
			this.m_txtACTIVEEMPID_CHR.Location = new System.Drawing.Point(72, 78);
			this.m_txtACTIVEEMPID_CHR.Name = "m_txtACTIVEEMPID_CHR";
			this.m_txtACTIVEEMPID_CHR.ReadOnly = true;
			this.m_txtACTIVEEMPID_CHR.Size = new System.Drawing.Size(104, 23);
			this.m_txtACTIVEEMPID_CHR.TabIndex = 6;
			this.m_txtACTIVEEMPID_CHR.Text = "";
			// 
			// m_txtSOURCEBEDID_CHR
			// 
			this.m_txtSOURCEBEDID_CHR.Location = new System.Drawing.Point(248, 20);
			this.m_txtSOURCEBEDID_CHR.Name = "m_txtSOURCEBEDID_CHR";
			this.m_txtSOURCEBEDID_CHR.ReadOnly = true;
			this.m_txtSOURCEBEDID_CHR.Size = new System.Drawing.Size(104, 23);
			this.m_txtSOURCEBEDID_CHR.TabIndex = 6;
			this.m_txtSOURCEBEDID_CHR.Text = "";
			// 
			// m_txtACTIVEDATE_DAT
			// 
			this.m_txtACTIVEDATE_DAT.Location = new System.Drawing.Point(248, 78);
			this.m_txtACTIVEDATE_DAT.Name = "m_txtACTIVEDATE_DAT";
			this.m_txtACTIVEDATE_DAT.ReadOnly = true;
			this.m_txtACTIVEDATE_DAT.Size = new System.Drawing.Size(104, 23);
			this.m_txtACTIVEDATE_DAT.TabIndex = 6;
			this.m_txtACTIVEDATE_DAT.Text = "";
			// 
			// m_txtSTATUS_INT
			// 
			this.m_txtSTATUS_INT.Location = new System.Drawing.Point(72, 48);
			this.m_txtSTATUS_INT.Name = "m_txtSTATUS_INT";
			this.m_txtSTATUS_INT.ReadOnly = true;
			this.m_txtSTATUS_INT.Size = new System.Drawing.Size(104, 23);
			this.m_txtSTATUS_INT.TabIndex = 6;
			this.m_txtSTATUS_INT.Text = "";
			// 
			// m_txtSOURCEAREAID_CHR
			// 
			this.m_txtSOURCEAREAID_CHR.Location = new System.Drawing.Point(72, 20);
			this.m_txtSOURCEAREAID_CHR.Name = "m_txtSOURCEAREAID_CHR";
			this.m_txtSOURCEAREAID_CHR.ReadOnly = true;
			this.m_txtSOURCEAREAID_CHR.Size = new System.Drawing.Size(104, 23);
			this.m_txtSOURCEAREAID_CHR.TabIndex = 5;
			this.m_txtSOURCEAREAID_CHR.Text = "";
			// 
			// m_chkISACTIVE_INT
			// 
			this.m_chkISACTIVE_INT.Enabled = false;
			this.m_chkISACTIVE_INT.Location = new System.Drawing.Point(360, 47);
			this.m_chkISACTIVE_INT.Name = "m_chkISACTIVE_INT";
			this.m_chkISACTIVE_INT.Size = new System.Drawing.Size(88, 24);
			this.m_chkISACTIVE_INT.TabIndex = 4;
			this.m_chkISACTIVE_INT.Text = "�Ƿ���Ч";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 19);
			this.label4.TabIndex = 0;
			this.label4.Text = "Դ����:  ";
			// 
			// m_txtDES_VCHR
			// 
			this.m_txtDES_VCHR.Location = new System.Drawing.Point(24, 106);
			this.m_txtDES_VCHR.Multiline = true;
			this.m_txtDES_VCHR.Name = "m_txtDES_VCHR";
			this.m_txtDES_VCHR.Size = new System.Drawing.Size(504, 72);
			this.m_txtDES_VCHR.TabIndex = 3;
			this.m_txtDES_VCHR.Text = "��Ժ���ӵ���";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(198, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 19);
			this.label6.TabIndex = 0;
			this.label6.Text = "Դ����:  ";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 52);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 19);
			this.label11.TabIndex = 0;
			this.label11.Text = "״̬��־:  ";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(5, 118);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(16, 48);
			this.label14.TabIndex = 0;
			this.label14.Text = "��  ע ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 82);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 19);
			this.label3.TabIndex = 0;
			this.label3.Text = "��Ч��:  ";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(184, 82);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(84, 19);
			this.label13.TabIndex = 0;
			this.label13.Text = "��Чʱ��:  ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(360, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 19);
			this.label2.TabIndex = 0;
			this.label2.Text = "Ŀ�겡��:  ";
			// 
			// m_txtCREATEDATE_DAT
			// 
			this.m_txtCREATEDATE_DAT.Location = new System.Drawing.Point(248, 48);
			this.m_txtCREATEDATE_DAT.Name = "m_txtCREATEDATE_DAT";
			this.m_txtCREATEDATE_DAT.ReadOnly = true;
			this.m_txtCREATEDATE_DAT.Size = new System.Drawing.Size(104, 23);
			this.m_txtCREATEDATE_DAT.TabIndex = 6;
			this.m_txtCREATEDATE_DAT.Text = "";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(184, 52);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(84, 19);
			this.label8.TabIndex = 0;
			this.label8.Text = "��������:  ";
			// 
			// frmAOTransfer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(536, 285);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("����", 10.5F);
			this.Name = "frmAOTransfer";
			this.Text = "ת�����ӵ��ݱ༭����";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAOTransfer_KeyDown);
			this.Load += new System.EventHandler(this.frmAOTransfer_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_AOTransfer();
			objController.Set_GUI_Apperance(this);
		}

		#region �¼�
		private void frmAOTransfer_Load(object sender, System.EventArgs e)
		{
			m_objLoginInfo = this.LoginInfo;
			((clsCtl_AOTransfer)this.objController).m_strOperatorID =m_objLoginInfo.m_strEmpID;
			((clsCtl_AOTransfer)this.objController).m_LoadData();
			((clsCtl_AOTransfer)this.objController).m_Initialization();
		}

		private void frmAOTransfer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					if(MessageBox.Show("�Ƿ�ȷ���˳�","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.None)==DialogResult.Yes)
					{
						this.Close();
					}
					break;
				case Keys.F2://����
					if(cmdOK.Enabled) cmdOK_Click(sender,e);
					break;
				case Keys.F3://�ύ
					if(cmdBecomeEffective.Enabled) cmdBecomeEffective_Click(sender,e);
					break;
				case Keys.F4://ɾ��
					if(cmdDel.Enabled) cmdDel_Click(sender,e);
					break;
			}
		}

		private void cmdCanCel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			((clsCtl_AOTransfer)this.objController).m_OK();
		}

		private void cmdDel_Click(object sender, System.EventArgs e)
		{
			((clsCtl_AOTransfer)this.objController).m_Del();		
		}

		private void cmdBecomeEffective_Click(object sender, System.EventArgs e)
		{
			((clsCtl_AOTransfer)this.objController).m_BecomeEffective();
		}

		#region Ŀ�겡��
		private void m_txtTARGETAREAID_CHR_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
		{
			clsBIHArea[] arrArea;
			 //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem =new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
			long ret= (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode,out arrArea);
			if((ret>0) && (arrArea!=null))
			{
				for(int i=0;i<arrArea.Length;i++)
				{
					ListViewItem lvi=lvwList.Items.Add(arrArea[i].m_strAreaName);
					lvi.Tag=arrArea[i].m_strAreaID;
				}
			}
		}

		private void m_txtTARGETAREAID_CHR_m_evtInitListView(System.Windows.Forms.ListView lvwList)
		{
			lvwList.Columns.Add("",120,HorizontalAlignment.Left);
			lvwList.Width=140;
		}

		private void m_txtTARGETAREAID_CHR_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
		{
			if(lviSelected!=null)
			{
				m_txtTARGETAREAID_CHR.Text=lviSelected.Text;
				m_txtTARGETAREAID_CHR.Tag=lviSelected.Tag ;
			}
		}
		private void m_txtTARGETAREAID_CHR_Leave(object sender, System.EventArgs e)
		{
			if(m_txtTARGETAREAID_CHR.Text.Trim()=="") m_txtTARGETAREAID_CHR.Tag ="";
		}
		#endregion
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
				lngRes =objTem.m_lngCommitAttachOrder_Transfer(p_strOrderID,1);
			}
			catch{}
			return lngRes;
		}
		#endregion 
	}
}
