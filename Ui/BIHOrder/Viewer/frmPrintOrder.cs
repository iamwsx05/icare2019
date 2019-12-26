using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.gui.HIS;
using System.Collections.Generic;

namespace com.digitalwave.iCare.BIHOrder
{
	/// <summary>
	/// ��ӡҽ��[�������ڡ���ʱ]���ÿ�
	/// �����ˣ�	����
	/// ����ʱ�䣺	2005-01-30
	/// </summary>
	public class frmPrintOrder: System.Windows.Forms.Form
	{
		#region �����ؼ�����
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDown4;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// ��Ժ�Ǽ�ID
		/// </summary>
		internal string m_strRegisterID ="";
        internal Hashtable m_htOrderCate=new Hashtable();
        internal clsSPECORDERCATE m_objSpecateVo = new clsSPECORDERCATE();
        private CheckBox m_chkPrintGridOnly;
        private CheckBox m_chkPrintContentOnly;
        internal clsBIHOrder[] arrOrder = null;
        private DataTable m_dtPatient = null;
        /// <summary>
        /// 0-��ʾԤ��,1-ֱ�Ӵ�ӡ
        /// </summary>
        private int m_intType = 0;
		#endregion

		#region ���캯��
		public frmPrintOrder()
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
		/// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
		public frmPrintOrder(string p_strRegisterID)
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			m_strRegisterID =p_strRegisterID;
		}


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        public frmPrintOrder(string p_strRegisterID, Hashtable m_htCate, clsBIHOrder[] m_arrOrder, clsSPECORDERCATE SpecateVo)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_strRegisterID = p_strRegisterID;
            m_htOrderCate = m_htCate;
            arrOrder = m_arrOrder;
            m_objSpecateVo = SpecateVo;

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        public frmPrintOrder(string p_strRegisterID,DataTable dtPatient, Hashtable m_htCate, clsBIHOrder[] m_arrOrder, clsSPECORDERCATE SpecateVo)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_strRegisterID = p_strRegisterID;
            m_htOrderCate = m_htCate;
            arrOrder = m_arrOrder;
            m_objSpecateVo = SpecateVo;
            m_dtPatient = dtPatient;

        }

        /// <summary>
        ///  ���캯��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <param name="dtPatient"></param>
        /// <param name="m_htCate"></param>
        /// <param name="m_arrOrder"></param>
        /// <param name="SpecateVo"></param>
        /// <param name="m_Class">1-������2-����</param>
        /// <param name="m_Type">0-��ʾԤ��,1-ֱ�Ӵ�ӡ</param>
        public frmPrintOrder(string p_strRegisterID, DataTable dtPatient, Hashtable m_htCate, clsBIHOrder[] m_arrOrder, clsSPECORDERCATE SpecateVo, string m_Class,int m_Type)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_strRegisterID = p_strRegisterID;
            m_htOrderCate = m_htCate;
            arrOrder = m_arrOrder;
            m_objSpecateVo = SpecateVo;
            m_dtPatient = dtPatient;
            m_strClass= m_Class;
            m_intType = m_Type;
            SetTheFrom();

        }
		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintOrder));
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.m_chkPrintGridOnly = new System.Windows.Forms.CheckBox();
            this.m_chkPrintContentOnly = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "��ӡ";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(20, 202);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 24);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "��ӡǩ��";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "ҽ�����ƴ�ӡ���";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(172, 51);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            140,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "��������λ��ӡ���";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "�÷���ӡ���";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(172, 86);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown3.Location = new System.Drawing.Point(172, 121);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown3.TabIndex = 4;
            this.numericUpDown3.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ƶ�ʴ�ӡ���";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown4.Location = new System.Drawing.Point(172, 156);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown4.TabIndex = 4;
            this.numericUpDown4.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(20, 8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(104, 24);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "����ҽ��";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(148, 8);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(104, 24);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "��ʱҽ��";
            // 
            // m_chkPrintGridOnly
            // 
            this.m_chkPrintGridOnly.Location = new System.Drawing.Point(110, 202);
            this.m_chkPrintGridOnly.Name = "m_chkPrintGridOnly";
            this.m_chkPrintGridOnly.Size = new System.Drawing.Size(96, 24);
            this.m_chkPrintGridOnly.TabIndex = 6;
            this.m_chkPrintGridOnly.Text = "ֻ��ӡ���";
            this.m_chkPrintGridOnly.CheckedChanged += new System.EventHandler(this.m_chkPrintGridOnly_CheckedChanged);
            // 
            // m_chkPrintContentOnly
            // 
            this.m_chkPrintContentOnly.Location = new System.Drawing.Point(19, 226);
            this.m_chkPrintContentOnly.Name = "m_chkPrintContentOnly";
            this.m_chkPrintContentOnly.Size = new System.Drawing.Size(96, 24);
            this.m_chkPrintContentOnly.TabIndex = 7;
            this.m_chkPrintContentOnly.Text = "ֻ��ӡ����";
            this.m_chkPrintContentOnly.CheckedChanged += new System.EventHandler(this.m_chkPrintContentOnly_CheckedChanged);
            // 
            // frmPrintOrder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(320, 268);
            this.Controls.Add(this.m_chkPrintContentOnly);
            this.Controls.Add(this.m_chkPrintGridOnly);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.radioButton2);
            this.Font = new System.Drawing.Font("����", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "��ӡҽ������ҳ��";
            this.Load += new System.EventHandler(this.frmPrintOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        /// <summary>
        /// �ⲿ�ӿڣ�����ӡ���á���Ԥ��ֱ�Ӵ�ӡ
        /// </summary>
        public void m_PrintListNoView()
        {
            button1_Click(null, null);
        }

		#region �¼�
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(m_strRegisterID.Trim()=="")
			{
				MessageBox.Show("û����Ժ�ǼǺţ�","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
            if (radioButton1.Checked)
            {
                com.digitalwave.Print.PhysicianOrder_Long objPrint = new com.digitalwave.Print.PhysicianOrder_Long();
                objPrint.m_BlnPrintDocName = checkBox1.Checked;
                objPrint.m_IntNameWidth = Convert.ToInt32(numericUpDown1.Value);
                objPrint.m_IntExecfreqWidth = Convert.ToInt32(numericUpDown3.Value);
                objPrint.m_IntDosageWidth = Convert.ToInt32(numericUpDown2.Value);
                objPrint.m_IntDoseTypeWidth = Convert.ToInt32(numericUpDown4.Value);
                objPrint.m_IntTopMargin = m_intTopMargin;
                objPrint.PrintGridOnly = m_chkPrintGridOnly.Checked ? true : false;
                objPrint.PrintContentOnly = m_chkPrintContentOnly.Checked;
                int m_intLong = 1;
                ArrayList m_arrStatus = new ArrayList();
                m_arrStatus.Add("1");
                m_arrStatus.Add("2");
                m_arrStatus.Add("3");
                m_arrStatus.Add("5");
                m_arrStatus.Add("6");
                DataTable m_dtOrder = null;
                GetTheDataTableByOrderList(m_intLong, m_arrStatus, out m_dtOrder);

                //objPrint.m_mthPrint(m_strRegisterID, m_dtOrder);
                if (m_intType == 0)
                {
                    objPrint.m_mthPrintPriew(m_dtPatient, m_dtOrder);
                }
                else
                {
                    objPrint.m_mthPrint(m_dtPatient, m_dtOrder);
                }
            }
            else
            {
                com.digitalwave.Print.PhysicianOrder_Temp objPrint = new com.digitalwave.Print.PhysicianOrder_Temp();
                objPrint.m_BlnPrintDocName = checkBox1.Checked;
                objPrint.m_IntNameWidth = Convert.ToInt32(numericUpDown1.Value);
                objPrint.m_IntExecfreqWidth = Convert.ToInt32(numericUpDown3.Value);
                objPrint.m_IntDosageWidth = Convert.ToInt32(numericUpDown2.Value);
                objPrint.m_IntDoseTypeWidth = Convert.ToInt32(numericUpDown4.Value);
                objPrint.m_IntTopMargin = m_intTopMargin;
                objPrint.PrintGridOnly = m_chkPrintGridOnly.Checked;
                objPrint.PrintContentOnly = m_chkPrintContentOnly.Checked;
                //objPrint.m_mthPrint(m_strRegisterID);
                int m_intLong = 2;
                ArrayList m_arrStatus = new ArrayList();
                m_arrStatus.Add("1");
                m_arrStatus.Add("2");
                m_arrStatus.Add("3");
                m_arrStatus.Add("5");
                DataTable m_dtOrder = null;
                GetTheDataTableByOrderList(m_intLong, m_arrStatus, out m_dtOrder);

                //objPrint.m_mthPrint(m_strRegisterID, m_dtOrder);
                if (m_intType == 0)
                {
                    objPrint.m_mthPrintPriew(m_dtPatient, m_dtOrder);
                }
                else
                {
                    objPrint.m_mthPrint(m_dtPatient, m_dtOrder);
                }
            }

            //this.Close();
		}

        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        /// <param name="m_intLong">1 ���� 2 �����ͳ�Ժ��ҩ</param>
        /// <param name="m_arrStatus"></param>
        /// <param name="m_dtOrder"></param>
        private void GetTheDataTableByOrderList(int m_intLong,ArrayList m_arrStatus,out DataTable m_dtOrder)
        {

            InitTable(out m_dtOrder);

            if (arrOrder != null && arrOrder.Length > 0)
            {
                int RecipenNo = 0;
                bool m_blSame = false;
                int m_intLong2 = m_intLong;
                if (m_intLong == 2)
                {
                    m_intLong2 = 3;
                }
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    m_blSame = false;

                    //if (arrOrder[i].m_intExecuteType == m_intLong || arrOrder[i].m_intExecuteType == m_intLong2)
                    //{
                    //    if (m_arrStatus.Contains(arrOrder[i].m_intStatus.ToString()))
                    //    {
                    //        if (arrOrder[i].m_intRecipenNo == RecipenNo)
                    //        {
                    //            m_blSame = true;
                    //        }
                    //        else
                    //        {
                    //            RecipenNo = arrOrder[i].m_intRecipenNo;
                    //        }
                    //        DataRow row = m_dtOrder.NewRow();
                    //        BindTheData(ref row, arrOrder[i], m_blSame);
                    //        m_dtOrder.Rows.Add(row);
                    //    }
                    //}
                     //����ǳ������򲻴�ӡ��Ժ��ҽ��
                    if (this.radioButton1.Checked == true)
                    {
                        if ((arrOrder[i].m_intExecuteType == 1 && !(arrOrder[i].m_intTYPE_INT == 3 || arrOrder[i].m_intTYPE_INT == 4)))
                        {
                            if (m_arrStatus.Contains(arrOrder[i].m_intStatus.ToString()))
                            {
                                if (arrOrder[i].m_intRecipenNo == RecipenNo)
                                {
                                    m_blSame = true;
                                }
                                else
                                {
                                    RecipenNo = arrOrder[i].m_intRecipenNo;
                                }
                                DataRow row = m_dtOrder.NewRow();
                                BindTheData(ref row, arrOrder[i], m_blSame);
                                m_dtOrder.Rows.Add(row);
                            }
                        }
                    }
                    else if (this.radioButton2.Checked == true)
                    {
                        if ( arrOrder[i].m_intExecuteType == 2 || arrOrder[i].m_intExecuteType == 3)
                        {
                            if (m_arrStatus.Contains(arrOrder[i].m_intStatus.ToString()))
                            {
                                if (arrOrder[i].m_intRecipenNo == RecipenNo)
                                {
                                    m_blSame = true;
                                }
                                else
                                {
                                    RecipenNo = arrOrder[i].m_intRecipenNo;
                                }
                                DataRow row = m_dtOrder.NewRow();
                                BindTheData(ref row, arrOrder[i], m_blSame);
                                m_dtOrder.Rows.Add(row);
                            }
                        }
                        else if ((arrOrder[i].m_intTYPE_INT == 3 || arrOrder[i].m_intTYPE_INT == 4))
                        {
                            if (arrOrder[i].m_intRecipenNo == RecipenNo)
                            {
                                m_blSame = true;
                            }
                            else
                            {
                                RecipenNo = arrOrder[i].m_intRecipenNo;
                            }
                            DataRow row = m_dtOrder.NewRow();
                            BindTheData(ref row, arrOrder[i], m_blSame);
                            m_dtOrder.Rows.Add(row);
                        }

                    }
                }
            }
           
        }

      

        private void InitTable(out DataTable m_dtOrder)
        {
            m_dtOrder = new DataTable();
            m_dtOrder.Columns.Add("startdoctor");
            m_dtOrder.Columns.Add("startnurse");
            m_dtOrder.Columns.Add("assessorpost");
            m_dtOrder.Columns.Add("assessorposttime");
            m_dtOrder.Columns.Add("assessorstop");
            m_dtOrder.Columns.Add("assessorstoptime");
            m_dtOrder.Columns.Add("recipeno");
            m_dtOrder.Columns.Add("ordername");
            m_dtOrder.Columns.Add("mednormalname_vchr");
            m_dtOrder.Columns.Add("dosage");
            m_dtOrder.Columns.Add("dosageunit");
            m_dtOrder.Columns.Add("dosetypename");
            m_dtOrder.Columns.Add("execfreqname");
            m_dtOrder.Columns.Add("entrust");
            m_dtOrder.Columns.Add("starttime");
            m_dtOrder.Columns.Add("endtime");
            m_dtOrder.Columns.Add("enddoctor");
            m_dtOrder.Columns.Add("endnurse");
            m_dtOrder.Columns.Add("recipeno_int");
            m_dtOrder.Columns.Add("CONFIRMER_VCHR");
            m_dtOrder.Columns.Add("USAGEVIEWTYPE");
            m_dtOrder.Columns.Add("DOSAGEVIEWTYPE");
            m_dtOrder.Columns.Add("FEQVIEWTYPE");
            m_dtOrder.Columns.Add("QTYVIEWTYPE_INT");
            m_dtOrder.Columns.Add("EXECUTOR_CHR");
            m_dtOrder.Columns.Add("EXECUTEDATE_DAT");

            m_dtOrder.Columns.Add("creatorsign", typeof(object));
            m_dtOrder.Columns.Add("confirmersign", typeof(object));
            m_dtOrder.Columns.Add("stopersign", typeof(object));
            m_dtOrder.Columns.Add("assessorsign", typeof(object));
        }

        public void BindTheData(ref DataRow row, clsBIHOrder objOrder, bool m_blSame)
        {
           
            row["startdoctor"] = objOrder.m_strCreator;//����ҽ��
            row["startnurse"] = objOrder.m_strASSESSORFOREXEC_CHR; // ��ҽ����
            row["assessorpost"] = objOrder.m_strASSESSORFOREXEC_CHR;//��ҽ����
            row["assessorposttime"] = objOrder.m_strASSESSORFOREXEC_DAT;//��ҽ��ʱ��
            row["assessorstop"] = objOrder.m_strASSESSORFORSTOP_CHR;//���ֹͣ��
            row["assessorstoptime"] = objOrder.m_strASSESSORFORSTOP_DAT;//���ͣҽ��ʱ��
            row["recipeno"] = objOrder.m_intRecipenNo2;//��ʾ�ķ���
           // row["ordername"] = objOrder.m_strName;//ҽ������
          //  row["dosage"] = objOrder.m_dmlDosage;//����
          //  row["dosageunit"] = objOrder.m_strDosageUnit;//������λ
          //  row["dosetypename"] = objOrder.m_strDosetypeName;//�÷�����

            row["execfreqname"] = "";//Ƶ������
            //row["execfreqname"] = objOrder.m_strExecFreqName;
            row["dosage"] = "";//����
            row["dosageunit"] = "";//������λ
            row["dosetypename"] = "";//�÷�����
            //row["execfreqname"] = "";//Ƶ������

            row["entrust"] = objOrder.m_strREMARK_VCHR;//˵��
            row["starttime"] = objOrder.m_dtPostdate;//����ʱ��
            row["endtime"] = objOrder.m_dtFinishDate;//����ʱ��
            row["enddoctor"] = objOrder.m_strStoper;//ͣ��ҽ��
            row["endnurse"] = objOrder.m_strASSESSORFORSTOP_CHR;//ͣ����ʿ
            row["recipeno_int"] = objOrder.m_intRecipenNo;//����
            row["CONFIRMER_VCHR"] = objOrder.m_strASSESSORFOREXEC_CHR;//������ύ��
            row["USAGEVIEWTYPE"] = "";//�÷�����
            row["DOSAGEVIEWTYPE"] = "";//��������
            row["FEQVIEWTYPE"] = "";//Ƶ������
            // л�ƽ��޸� ��Ҫ�����ر���ʾ��Ժ��ҽ��
            //row["QTYVIEWTYPE_INT"] = "";//
            row["QTYVIEWTYPE_INT"] = objOrder.m_intTYPE_INT.ToString();
            row["EXECUTOR_CHR"] = "";
            row["EXECUTEDATE_DAT"] = objOrder.m_dtExecutedate.ToString();
            row["EXECUTOR_CHR"] = objOrder.m_strExecutor;

            row["creatorsign"] = objOrder.creatorsign;
            row["confirmersign"] = objOrder.confirmersign;
            row["stopersign"] = objOrder.stopersign;
            row["assessorsign"] = objOrder.assessorsign;

            //����ҽ��
          
            //Ƥ��
            string m_strFeel = "";
            if (objOrder.m_intISNEEDFEEL == 1)
            {

                switch (objOrder.m_intFEEL_INT)
                {
                    case 0:
                        m_strFeel = " AST( ) ";
                        break;
                    case 1:
                        m_strFeel = " AST(-) ";
                        break;
                    case 2:
                        m_strFeel = " AST(+) ";
                        break;
                }

            }
            /*<==================================*/
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_htOrderCate[objOrder.m_strOrderDicCateID];
            string dtv_Dosage = "", dtv_UseType = "", dtv_Freq = "", ATTACHTIMES_INT = "", dtv_Get="";
            decimal m_dmlOneUse = 0;//��һ�ε�����
            #region ҽ�����Ϳ����б����
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;
                if (!objOrder.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ����
                {
                    if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
                    {
                        //����
                        if (objOrder.m_dmlDosage > 0)
                        {
                            dtv_Dosage = objOrder.m_dmlDosage.ToString() + "" + objOrder.m_strDosageUnit;
                        }
                        else
                        {
                            dtv_Dosage = "";

                        }
                    }
                    else
                    {
                        dtv_Dosage = "";
                    }
                }
                else
                {
                    dtv_Dosage = "";
                }
                if (p_objItem.m_intUSAGEVIEWTYPE == 1)
                {
                    //�÷�
                    dtv_UseType = objOrder.m_strDosetypeName;
                }
                else
                {
                    //�÷�
                    dtv_UseType = "";
                }
                if (objOrder.m_intExecuteType == 1 || objOrder.m_intExecuteType == 2)
                {
                    if (p_objItem.m_intExecuFrenquenceType == 1)
                    {
                        //Ƶ��
                        dtv_Freq = objOrder.m_strExecFreqName;
                    }
                    else
                    {
                        //������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                        if (objOrder.m_intCHARGE_INT == 1)
                        {
                            dtv_Freq = objOrder.m_strExecFreqName;//Ƶ��
                        }
                        else
                        {
                            dtv_Freq = "";//Ƶ��
                        }
                    }
                }
                else
                {
                    dtv_Freq = "";//Ƶ��
                }

                if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
                {
                    //����
                    ATTACHTIMES_INT = objOrder.m_intATTACHTIMES_INT.ToString();
                    m_dmlOneUse = objOrder.m_dmlOneUse * objOrder.m_intATTACHTIMES_INT;
                }
                else
                {
                    //����
                    ATTACHTIMES_INT = "";
                    m_dmlOneUse = 0;
                }
                //����
                if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
                {
                    if (objOrder.m_dmlGet > 0)
                    {
                        dtv_Get = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;

                    }
                    else
                    {
                        dtv_Get = "";

                    }
                }
                else
                {
                    //����
                    dtv_Get = "";
                }
            }
            else
            {
                //����
                dtv_Dosage = "";
                //Ƶ��
                dtv_Freq = "";
                //�÷�
                dtv_UseType = "";
                //����
                ATTACHTIMES_INT = "";
                //����
                dtv_Get = "";

            }
            #endregion

            //
            //�����ֶεĿ���
            string m_strSum = "";
            if (objOrder.m_strOrderDicCateID.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))//��ҩ�����߼�
            {
                if (m_intShowTotalNums == 1)
                {
                    m_strSum = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "����" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                }
                else
                {
                    m_strSum = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                }                
            }
            else
            {

                if (objOrder.m_intExecuteType == 3)
                {
                    if (m_intShowTotalNums == 1)
                    {
                        m_strSum = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    }
                    else
                    {
                        m_strSum = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                    }
                }
                else
                {
                    if (m_intShowTotalNums == 1)
                    {
                        m_strSum = "��" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    }
                }
            }           

            if (objOrder.m_intTYPE_INT==0)
            {
                if (m_blSame == false)
                {                    
                    if (objOrder.m_intExecuteType == 1)
                    {
                        row["ordername"] = "��" + objOrder.m_strName + " " + dtv_Dosage.ToString();
                        if (objOrder.m_mednormalname_vchr != "")
                        {
                            row["mednormalname_vchr"] = "��" + objOrder.m_mednormalname_vchr + " " + dtv_Dosage.ToString();
                        }
                        else
                        {
                            row["mednormalname_vchr"] = "";
                        }
                    }
                    else
                    {
                        row["ordername"] = " "+objOrder.m_strName + " " + dtv_Dosage.ToString();
                        if (objOrder.m_mednormalname_vchr != "")
                        {
                            row["mednormalname_vchr"] = " " + objOrder.m_mednormalname_vchr + " " + dtv_Dosage.ToString();
                        }
                        else
                        {
                            row["mednormalname_vchr"] = "";
                        }
                    }
                 
                    row["dosage"] = dtv_UseType.ToString() + " " + dtv_Freq.ToString()  + m_strFeel + " " + m_strSum+" "+objOrder.m_strREMARK_VCHR.Trim();
                }
                else
                {
                    row["ordername"] = objOrder.m_strName + " " + dtv_Dosage.ToString();//objOrder.m_strREMARK_VCHR.Trim();
                    if (objOrder.m_mednormalname_vchr != "")
                    {
                        row["mednormalname_vchr"] = objOrder.m_mednormalname_vchr + " " + dtv_Dosage.ToString();
                    }
                    else
                    {
                        row["mednormalname_vchr"] = "";
                    }
                   
                }
            }
            else 
            {
                row["ordername"] = objOrder.m_strName;
                if (objOrder.m_mednormalname_vchr != "")
                {
                    row["mednormalname_vchr"] = objOrder.m_mednormalname_vchr;
                }
                else
                {
                    row["mednormalname_vchr"] = "";
                }
                row["recipeno"] = string.Empty;
            }

        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radioButton1.Checked)
			{
				numericUpDown1.Value = 140;
				numericUpDown2.Value = 40;
				numericUpDown3.Value = 40;
				numericUpDown4.Value = 60;                
			}
			else
			{				
				numericUpDown1.Value = 170;
				numericUpDown2.Value = 70;
				numericUpDown3.Value = 70;
				numericUpDown4.Value = 80;                
			}		
		}
		#endregion

        public string m_strClass = "0";
        private void frmPrintOrder_Load(object sender, EventArgs e)
        {
            SetTheFrom();
            
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private int m_intShowTotalNums = 1;
        /// <summary>
        /// �ϱ߾�
        /// </summary>
        private int m_intTopMargin = 50;

        private void SetTheFrom()
        {
            if (m_strClass.Equals("1"))
            {
                this.radioButton1.Checked = true;
                this.radioButton2.Checked = false;
                this.radioButton2.Enabled = false;
            }
            else if (m_strClass.Equals("2"))
            {
                this.radioButton1.Checked = false;
                this.radioButton1.Enabled = false;
                this.radioButton2.Checked = true;
            }

            m_intShowTotalNums = clsPublic.m_intGetSysParm("1061");
            m_intTopMargin = clsPublic.m_intGetSysParm("1062");
        }

        private void m_chkPrintGridOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkPrintGridOnly.Checked)
            {
                if (m_chkPrintContentOnly.Checked)
                {
                    m_chkPrintContentOnly.Checked = false;
                }
            }
        }

        private void m_chkPrintContentOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkPrintContentOnly.Checked)
            {
                if (m_chkPrintGridOnly.Checked)
                {
                    m_chkPrintGridOnly.Checked = false;
                }
            }
        }
	}
}
