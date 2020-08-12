using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.iCare.common;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmOrdDeReportOfMonth ��ժҪ˵����
	/// </summary>
	public class frmOrdDeReportOfMonth :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private com.digitalwave.controls.Control.ctlprintShow ctlprintShow1;
		private System.Windows.Forms.GroupBox groupBox1;
		private PinkieControls.ButtonXP buttonXP2;
		private PinkieControls.ButtonXP buttonXP1;
        private PinkieControls.ButtonXP buttonXP3;
		private System.Windows.Forms.ComboBox m_cboSeleVendor;
        private Label label3;
        private ComboBox comboBox1;
        private Label label4;
        private ComboBox comboBox2;
        private Label label5;
        private Label label6;
        private Label label7;
        private NumericUpDown m_nudFrom;
        private NumericUpDown m_nudEnd;
        private RadioButton m_rdbAll;
        private RadioButton m_rdbSpecify;
        private Button button1;
        internal exComboBox m_cboSelPeriodEnd;
        internal exComboBox m_cboSelPeriodBegion;
        private Label label1;
        private Label label2;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmOrdDeReportOfMonth()
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_nudFrom = new System.Windows.Forms.NumericUpDown();
            this.m_nudEnd = new System.Windows.Forms.NumericUpDown();
            this.m_rdbAll = new System.Windows.Forms.RadioButton();
            this.m_rdbSpecify = new System.Windows.Forms.RadioButton();
            this.ctlprintShow1 = new com.digitalwave.controls.Control.ctlprintShow();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_cboSeleVendor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.m_cboSelPeriodEnd = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboSelPeriodBegion = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudEnd)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.m_nudFrom);
            this.panel1.Controls.Add(this.m_nudEnd);
            this.panel1.Controls.Add(this.m_rdbAll);
            this.panel1.Controls.Add(this.m_rdbSpecify);
            this.panel1.Controls.Add(this.ctlprintShow1);
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 415);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(735, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 25;
            this.button1.Text = "����";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(609, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 14);
            this.label6.TabIndex = 24;
            this.label6.Text = "��:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 21;
            this.label7.Text = "��ӡ��Χ:";
            // 
            // m_nudFrom
            // 
            this.m_nudFrom.Enabled = false;
            this.m_nudFrom.Location = new System.Drawing.Point(553, 0);
            this.m_nudFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_nudFrom.Name = "m_nudFrom";
            this.m_nudFrom.Size = new System.Drawing.Size(48, 23);
            this.m_nudFrom.TabIndex = 23;
            this.m_nudFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_nudEnd
            // 
            this.m_nudEnd.Enabled = false;
            this.m_nudEnd.Location = new System.Drawing.Point(641, 0);
            this.m_nudEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_nudEnd.Name = "m_nudEnd";
            this.m_nudEnd.Size = new System.Drawing.Size(48, 23);
            this.m_nudEnd.TabIndex = 22;
            this.m_nudEnd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_rdbAll
            // 
            this.m_rdbAll.Checked = true;
            this.m_rdbAll.Location = new System.Drawing.Point(396, -1);
            this.m_rdbAll.Name = "m_rdbAll";
            this.m_rdbAll.Size = new System.Drawing.Size(55, 24);
            this.m_rdbAll.TabIndex = 20;
            this.m_rdbAll.TabStop = true;
            this.m_rdbAll.Text = "ȫ��";
            // 
            // m_rdbSpecify
            // 
            this.m_rdbSpecify.Location = new System.Drawing.Point(457, -1);
            this.m_rdbSpecify.Name = "m_rdbSpecify";
            this.m_rdbSpecify.Size = new System.Drawing.Size(103, 24);
            this.m_rdbSpecify.TabIndex = 19;
            this.m_rdbSpecify.Text = "ָ��ҳ  ��:";
            this.m_rdbSpecify.CheckedChanged += new System.EventHandler(this.m_rdbSpecify_CheckedChanged);
            // 
            // ctlprintShow1
            // 
            this.ctlprintShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlprintShow1.Location = new System.Drawing.Point(0, 0);
            this.ctlprintShow1.Name = "ctlprintShow1";
            this.ctlprintShow1.Size = new System.Drawing.Size(993, 413);
            this.ctlprintShow1.TabIndex = 2;
            this.ctlprintShow1.Zoom = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_cboSelPeriodEnd);
            this.groupBox1.Controls.Add(this.m_cboSelPeriodBegion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.m_cboSeleVendor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonXP3);
            this.groupBox1.Controls.Add(this.buttonXP1);
            this.groupBox1.Controls.Add(this.buttonXP2);
            this.groupBox1.Location = new System.Drawing.Point(0, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(995, 68);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Items.AddRange(new object[] {
            "��",
            "��",
            "ȫ��"});
            this.comboBox2.Location = new System.Drawing.Point(383, 28);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(50, 22);
            this.comboBox2.TabIndex = 334;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 335;
            this.label5.Text = "�Ƿ��б꣺";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "��ҩͳ��",
            "�г�ҩͳ��",
            "�в�ҩͳ��"});
            this.comboBox1.Location = new System.Drawing.Point(500, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(85, 22);
            this.comboBox1.TabIndex = 332;
            // 
            // m_cboSeleVendor
            // 
            this.m_cboSeleVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSeleVendor.Location = new System.Drawing.Point(642, 28);
            this.m_cboSeleVendor.Name = "m_cboSeleVendor";
            this.m_cboSeleVendor.Size = new System.Drawing.Size(126, 22);
            this.m_cboSeleVendor.TabIndex = 330;
            this.m_cboSeleVendor.SelectedIndexChanged += new System.EventHandler(this.m_cboSeleVendor_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 333;
            this.label4.Text = "ҩƷ���ͣ�";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 331;
            this.label3.Text = "��Ӧ�̣�";
            // 
            // buttonXP3
            // 
            this.buttonXP3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(921, 20);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(57, 35);
            this.buttonXP3.TabIndex = 326;
            this.buttonXP3.Text = "�˳�(&E)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(854, 20);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(61, 35);
            this.buttonXP1.TabIndex = 325;
            this.buttonXP1.Text = "��ӡ(&P)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(787, 20);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(63, 35);
            this.buttonXP2.TabIndex = 324;
            this.buttonXP2.Text = "ͳ��(&S)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // m_cboSelPeriodEnd
            // 
            this.m_cboSelPeriodEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodEnd.Location = new System.Drawing.Point(88, 41);
            this.m_cboSelPeriodEnd.Name = "m_cboSelPeriodEnd";
            this.m_cboSelPeriodEnd.Size = new System.Drawing.Size(225, 22);
            this.m_cboSelPeriodEnd.TabIndex = 338;
            // 
            // m_cboSelPeriodBegion
            // 
            this.m_cboSelPeriodBegion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodBegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodBegion.Location = new System.Drawing.Point(88, 15);
            this.m_cboSelPeriodBegion.Name = "m_cboSelPeriodBegion";
            this.m_cboSelPeriodBegion.Size = new System.Drawing.Size(225, 22);
            this.m_cboSelPeriodBegion.TabIndex = 337;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 339;
            this.label1.Text = "����������";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 336;
            this.label2.Text = "��ʼ������";
            // 
            // frmOrdDeReportOfMonth
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(995, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmOrdDeReportOfMonth";
            this.Text = "ҩƷ�������ϸͳ�Ʊ���";
            this.Load += new System.EventHandler(this.frmOrdDeReportOfMonth_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudEnd)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// �Ƿ��б�ҩ 0 - �� 1 - ��,2-ȫ��
		/// </summary>
		string strStandard;
		/// <summary>
        /// ҩƷ����1-��ҩ��2-�в�ҩ��3-�г�ҩ
		/// </summary>
		string strStorageTypeId;
		/// <summary>
		/// �����־ 1����� 2������
		/// </summary>
		string strSIGN;
        /// <summary>
        /// ���ڶ����־,1��Ժ�ڣ�0��Ժ��
        /// </summary>
        string strIn;
		/// <summary>
		/// 
		/// </summary>
        /// <param name="str1">1-���ͳ�ƣ���������ͳ�ƣ������˿�ͳ�ƣ������˻�ͳ��</param>
		public void m_mthShowMe(string str1)
		{
			switch(str1)
			{
				case "1":
                    strIn = "0";
					strSIGN="1";
				break;
				case "2":
                    strIn = "1";
					strSIGN="2";
				break;
				case "3":
                    strIn = "1";
					strSIGN="1";
					break;
				case "4":
                    strIn = "0";
					strSIGN="2";
					break;
			}
			this.Show();
		}
         clsPeriod_VO[] objPriodItems = null;
		private void frmOrdDeReportOfMonth_Load(object sender, System.EventArgs e)
		{
            objPriodItems = clsPublicParm.s_GetPeriodList();
            string nowdate = clsPublicParm.s_datGetServerDate().Date.ToString();
            int intSelPeriod = -1;
            if (objPriodItems.Length > 0)
            {
                for (int i1 = 0; i1 < objPriodItems.Length; i1++)
                {
                    this.m_cboSelPeriodBegion.Item.Add(objPriodItems[i1].m_strStartDate + " �� " + objPriodItems[i1].m_strEndDate, objPriodItems[i1].m_strPeriodID);
                    this.m_cboSelPeriodEnd.Item.Add(objPriodItems[i1].m_strStartDate + " �� " + objPriodItems[i1].m_strEndDate, objPriodItems[i1].m_strPeriodID);
                    if (Convert.ToDateTime(nowdate) >= Convert.ToDateTime(objPriodItems[i1].m_strStartDate) && Convert.ToDateTime(nowdate) <= Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
                    {
                        intSelPeriod = i1;
                    }
                }
            }
            if (intSelPeriod != -1)
            {
                this.m_cboSelPeriodEnd.SelectedIndex = intSelPeriod;
                this.m_cboSelPeriodBegion.SelectedIndex = intSelPeriod;
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
			ctlprintShow1.setDocument=printDocument1;
		}
		DomainControlMedReport Domain=new DomainControlMedReport();
		DataTable dtVendor;
		DataTable dtde;
		DataSet dtAll=new DataSet();
		string strSaveVendor="ȫ��";
		/// <summary>
		/// �Ƿ�Ҫ�����ݿ����»�ȡ����
		/// </summary>
		int intIsGetData=1;
		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			if(intIsGetData==0)
				return;
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            for (int i1 = this.m_cboSelPeriodBegion.SelectedIndex; i1 <= this.m_cboSelPeriodEnd.SelectedIndex; i1++)
            {
                list.Add(objPriodItems[i1].m_strPeriodID);
            }
			dtAll.Tables.Clear();
            Domain.m_mthOrdDeByMonth(list, out dtVendor, out dtde, strStandard, strStorageTypeId, strSIGN, strIn);
			if(dtVendor!=null&&dtde!=null)
			{
				DataTable dt =new DataTable();
				dt.Columns.Add("���");
				dt.Columns.Add("ҩƷ����");
				dt.Columns.Add("���");
				dt.Columns.Add("����");
				dt.Columns.Add("��������");
				dt.Columns.Add("��λ");
				dt.Columns.Add("����");
				dt.Columns.Add("����");
				dt.Columns.Add("���۽��");
				dt.Columns.Add("�б����");
				dt.Columns.Add("�б���");
				dt.Columns.Add("�����޼�");
				dt.Columns.Add("�޼۽��");
                dt.Columns.Add("���۵���");
                dt.Columns.Add("���۽��");
				dt.Columns.Add("����");
				dt.Columns.Add("��׼�ĺ�");
				dt.Columns.Add("ʧЧ��");
				dt.Columns.Add("����");
				dt.Columns.Add("����");
				for(int i1=0;i1<dtVendor.Rows.Count;i1++)
				{
					DataTable dt1=new DataTable();
					dt1=dt.Clone();
					dt1.TableName=dtVendor.Rows[i1]["VENDORNAME_VCHR"].ToString();
					for(int f2=0;f2<dtde.Rows.Count;f2++)
					{
                        if (dtde.Rows[f2]["VENDORID_CHR"].ToString().Trim() == dtVendor.Rows[i1]["VENDORID_CHR"].ToString().Trim() && dtde.Rows[f2]["VENDORNAME_VCHR"].ToString().Trim() == dtVendor.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim())
						{
							DataRow addRow=dt1.NewRow();
							addRow["���"]=dtde.Rows[f2]["ASSISTCODE_CHR"];
							addRow["ҩƷ����"]=dtde.Rows[f2]["MEDICINENAME_VCHR"];
							addRow["���"]=dtde.Rows[f2]["MEDSPEC_VCHR"];
							addRow["����"]=dtde.Rows[f2]["MEDICINEPREPTYPENAME_VCHR"];
							addRow["��������"]=dtde.Rows[f2]["PRODUCTORID_CHR"];
							addRow["��λ"]=dtde.Rows[f2]["UNITID_CHR"];
							addRow["����"]=dtde.Rows[f2]["QTY_DEC"];
                            if (strSIGN == "2" && strIn == "1")
                            {
                                addRow["����"] = "";
                                addRow["���۽��"] = "";
                            }
                            else
                            {
                                addRow["����"] = dtde.Rows[f2]["BUYUNITPRICE_MNY"];
                                addRow["���۽��"] = dtde.Rows[f2]["BUYTOLPRICE_MNY"];
                            }
                            if (dtde.Rows[f2]["AIMUNITPRICE_MNY"] != null && dtde.Rows[f2]["AIMUNITPRICE_MNY"].ToString() != "")
                            {
                                addRow["�б����"] = dtde.Rows[f2]["AIMUNITPRICE_MNY"];
                                addRow["�б���"] = Double.Parse(dtde.Rows[f2]["AIMUNITPRICE_MNY"].ToString()) * double.Parse(dtde.Rows[f2]["QTY_DEC"].ToString());
                            }
                            if (dtde.Rows[f2]["LIMITUNITPRICE_MNY"] != null && dtde.Rows[f2]["LIMITUNITPRICE_MNY"].ToString() != "")
                            {
                                addRow["�����޼�"] = dtde.Rows[f2]["LIMITUNITPRICE_MNY"];
                                addRow["�޼۽��"] = Double.Parse(dtde.Rows[f2]["LIMITUNITPRICE_MNY"].ToString()) * double.Parse(dtde.Rows[f2]["QTY_DEC"].ToString());
                            }
                            if (strSIGN == "1" && strIn == "1")
                            {
                                addRow["���۵���"] = dtde.Rows[f2]["UNITPRICE_MNY"];
                                addRow["���۽��"] = Double.Parse(dtde.Rows[f2]["UNITPRICE_MNY"].ToString()) * double.Parse(dtde.Rows[f2]["QTY_DEC"].ToString());
                            }
                            else
                            {
                                addRow["���۵���"] = dtde.Rows[f2]["SALEUNITPRICE_MNY"];
                                addRow["���۽��"] = Double.Parse(dtde.Rows[f2]["SALEUNITPRICE_MNY"].ToString()) * double.Parse(dtde.Rows[f2]["QTY_DEC"].ToString());
                            }
							addRow["����"]=dtde.Rows[f2]["LOTNO_VCHR"];
							addRow["��׼�ĺ�"]=dtde.Rows[f2]["PERMITNO_VCHR"];
							addRow["ʧЧ��"]=dtde.Rows[f2]["USEFULLIFE_DAT"];
							addRow["����"]="�ϸ�";
							addRow["����"]=dtde.Rows[f2]["ORD_DAT"];
							dt1.Rows.Add(addRow);
							dtde.Rows[f2].Delete();
                            f2--;
							dtde.AcceptChanges();
						}
					}
                    if (dt1.Rows.Count > 0)
                        dtAll.Tables.Add(dt1);
				}

			}
			#region ��乩Ӧ����Ϣ
			m_cboSeleVendor.Items.Clear();
			if(dtAll.Tables.Count>0)
			{
				m_cboSeleVendor.Items.Add("ȫ��");
				for(int i1=0;i1<dtAll.Tables.Count;i1++)
				{
					m_cboSeleVendor.Items.Add(dtAll.Tables[i1].TableName);
				}
			 }
			for(int i1=0;i1<m_cboSeleVendor.Items.Count;i1++)
			{
				if((string)m_cboSeleVendor.Items[i1]==strSaveVendor)
				{
					m_cboSeleVendor.SelectedIndex=i1;
					break;
				}
			}

			#endregion
			#region ���ô�ӡ
			this.printDocument1.DefaultPageSettings.Landscape=true;
			foreach(System.Drawing.Printing.PaperSize ps in this.printDocument1.PrinterSettings.PaperSizes)
			{
				if(ps.PaperName=="A4")
				{
					this.printDocument1.DefaultPageSettings.PaperSize=ps;
					break;
				}
			}

			#endregion
			pringPage=new clsStorageOrd();
            pringPage.pagenuber = 0;
            pringPage.currdt = 0;
            pringPage.currRow = 0;
		}

		clsStorageOrd pringPage;
		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
            if (m_rdbAll.Checked || !isPrintBtn)//����Ҫ���ƴ�ӡ��������ӡ��ͨ����Ԥ��ʱ��
            {
                m_mthPrint(e);
                pringPage.pagenuber++;
            }
            else
            {
                //��û��ָ���Ŀ�ʼҳ�����
                while (pringPage.pagenuber < (int)(m_nudFrom.Value - 1))
                {
                    m_mthPrint(e);
                    e.Graphics.Clear(Color.White);
                    pringPage.pagenuber++;
                }
                m_mthPrint(e);
                pringPage.pagenuber++;
                //�Ѿ���ָ���Ľ���ҳ������
                if (pringPage.pagenuber > (int)(m_nudEnd.Value - 1))
                {
                    e.HasMorePages = false;
                    return;
                }
            }	
		}
        private void m_mthPrint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (m_cboSeleVendor.Text == "ȫ��")
            {
                pringPage.m_mthPrint(this.m_cboSelPeriodBegion.Text, this.m_cboSelPeriodEnd.Text, dtAll, this.LoginInfo.m_strEmpName, strStandard, strStorageTypeId, strSIGN, strIn, e);
            }
            else
            {
                DataSet dtSele = new DataSet();
                for (int i1 = 0; i1 < dtAll.Tables.Count; i1++)
                {
                    if (dtAll.Tables[i1].TableName == strSaveVendor)
                    {

                        DataTable temdt = dtAll.Tables[i1].Copy();
                        dtSele.Tables.Add(temdt);
                        pringPage.m_mthPrint(this.m_cboSelPeriodBegion.Text, this.m_cboSelPeriodEnd.Text, dtSele, this.LoginInfo.m_strEmpName, strStandard, strStorageTypeId, strSIGN, strIn, e);
                        break;
                    }
                }
            }
        }
        bool isPrintBtn;
		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
            if (int.Parse(this.m_cboSelPeriodBegion.SelectItemValue) > int.Parse(this.m_cboSelPeriodEnd.SelectItemValue))
            {
                MessageBox.Show("��ʼ�����ڲ��ܴ��ڽ��������ڣ�");
                return;
            }
            if(comboBox2.Text=="��")
                strStandard = "1";
            else if (comboBox2.Text == "��")
                strStandard = "0";
            else
                strStandard = "2";
            if(comboBox1.Text=="��ҩͳ��")
                strStorageTypeId = "1";
            else
            if (comboBox1.Text == "�г�ҩͳ��")
                strStorageTypeId = "3";
            else
                strStorageTypeId = "2";
			intIsGetData=1;
            pringPage.isPrint = false;
            isPrintBtn = false;
			ctlprintShow1.setDocument=printDocument1;

		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
            if (m_nudFrom.Value > m_nudEnd.Value)
            {
                MessageBox.Show("��ʼҳ���ܴ��ڽ���ҳ��");
                return;
            }
            com.digitalwave.iCare.common.frmSelectPrinter selectPrinter = new com.digitalwave.iCare.common.frmSelectPrinter();
            if (selectPrinter.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings.PrinterName = selectPrinter.PrinterName;
            }
            else
            {
                return;
            }
            isPrintBtn = true;
            pringPage.pagenuber = 0;
            pringPage.currdt = 0;
            pringPage.currRow = 0;
            printDocument1.Print();
			
		}

		private void m_cboSeleVendor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			strSaveVendor=m_cboSeleVendor.Text;
			intIsGetData=0;
		}

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_rdbSpecify_CheckedChanged(object sender, EventArgs e)
        {
            m_nudFrom.Enabled = m_rdbSpecify.Checked;
            m_nudEnd.Enabled = m_rdbSpecify.Checked;
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            pringPage.pagenuber = 0;
            pringPage.currdt = 0;
            pringPage.currRow = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_mthOutExcel();
        }
        public void m_mthOutExcel()
        {
            ExcelExporter excel = new ExcelExporter(dtAll);
            bool b = excel.m_mthExport();
            if (b)
            {
                MessageBox.Show("�������ݳɹ�!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("��������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
	}
}
