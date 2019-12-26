using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// frmOrderTemplate 
    /// </summary>
    public class frmOrderTemplate : System.Windows.Forms.Form
    {

        #region �Զ������ 
        public ItemTag[] arlOrders = new ItemTag[0];
        public frmBIHOrderInput frmParent;
        // Ϊ��ͬҽ������ҽ���б�ؼ����Ӧ

        // ��ǰҽ�������Ƿ��Ѿ�����
        ArrayList m_arrOrderDicID = new ArrayList();
        /// <summary>
        /// ���ڸ��Ƽ�ճ��ҽ��
        /// </summary>
        System.Collections.Generic.List<clsBIHOrder> m_arrOrderTempList = new System.Collections.Generic.List<clsBIHOrder>();
        /*<===========================*/
        #endregion

        private System.Windows.Forms.ImageList m_imgIcons;
        private System.Windows.Forms.ImageList m_imgBigIcons;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private PinkieControls.ButtonXP m_cmdAdd;
        private PinkieControls.ButtonXP m_cmdRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_chkAll;
        private PinkieControls.ButtonXP m_cmdClear;
        private PinkieControls.ButtonXP m_cmdGroupAdd;
        internal DataGridView dgv_orderTemp;
        private DataGridViewCheckBoxColumn tmp_check;
        private DataGridViewTextBoxColumn tmp_Name;
        private DataGridViewTextBoxColumn tmp_ExecuteType;
        private DataGridViewTextBoxColumn tmp_Dosage;
        private DataGridViewTextBoxColumn tmp_UseType;
        private DataGridViewTextBoxColumn tmp_Freq;
        private DataGridViewTextBoxColumn tmp_Get;
        private System.ComponentModel.IContainer components;

        public frmOrderTemplate()
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderTemplate));
            this.m_imgBigIcons = new System.Windows.Forms.ImageList(this.components);
            this.m_imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.m_cmdAdd = new PinkieControls.ButtonXP();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.m_cmdRemove = new PinkieControls.ButtonXP();
            this.m_chkAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cmdClear = new PinkieControls.ButtonXP();
            this.m_cmdGroupAdd = new PinkieControls.ButtonXP();
            this.dgv_orderTemp = new System.Windows.Forms.DataGridView();
            this.tmp_check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tmp_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmp_ExecuteType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmp_Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmp_UseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmp_Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmp_Get = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_orderTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // m_imgBigIcons
            // 
            this.m_imgBigIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgBigIcons.ImageStream")));
            this.m_imgBigIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgBigIcons.Images.SetKeyName(0, "");
            this.m_imgBigIcons.Images.SetKeyName(1, "");
            // 
            // m_imgIcons
            // 
            this.m_imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgIcons.ImageStream")));
            this.m_imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgIcons.Images.SetKeyName(0, "");
            this.m_imgIcons.Images.SetKeyName(1, "");
            // 
            // m_cmdAdd
            // 
            this.m_cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAdd.DefaultScheme = true;
            this.m_cmdAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAdd.Hint = "";
            this.m_cmdAdd.Location = new System.Drawing.Point(268, 377);
            this.m_cmdAdd.Name = "m_cmdAdd";
            this.m_cmdAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAdd.Size = new System.Drawing.Size(71, 26);
            this.m_cmdAdd.TabIndex = 44;
            this.m_cmdAdd.Text = "����ҽ��";
            this.m_cmdAdd.Click += new System.EventHandler(this.m_cmdAdd_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(8, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(52, 24);
            this.radioButton1.TabIndex = 45;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ͼ��";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(76, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(40, 24);
            this.radioButton2.TabIndex = 46;
            this.radioButton2.Text = "Сͼ��";
            this.radioButton2.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(64, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(52, 24);
            this.radioButton3.TabIndex = 47;
            this.radioButton3.Text = "��ϸ";
            // 
            // m_cmdRemove
            // 
            this.m_cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdRemove.DefaultScheme = true;
            this.m_cmdRemove.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdRemove.Hint = "";
            this.m_cmdRemove.Location = new System.Drawing.Point(339, 377);
            this.m_cmdRemove.Name = "m_cmdRemove";
            this.m_cmdRemove.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdRemove.Size = new System.Drawing.Size(71, 26);
            this.m_cmdRemove.TabIndex = 48;
            this.m_cmdRemove.Text = "�� ��";
            this.m_cmdRemove.Click += new System.EventHandler(this.m_cmdRemove_Click);
            // 
            // m_chkAll
            // 
            this.m_chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkAll.Location = new System.Drawing.Point(24, 377);
            this.m_chkAll.Name = "m_chkAll";
            this.m_chkAll.Size = new System.Drawing.Size(84, 24);
            this.m_chkAll.TabIndex = 49;
            this.m_chkAll.Text = "ȫѡ(&A)";
            this.m_chkAll.CheckedChanged += new System.EventHandler(this.m_chkAll_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 409);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 40);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdClear.DefaultScheme = true;
            this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClear.Hint = "";
            this.m_cmdClear.Location = new System.Drawing.Point(411, 377);
            this.m_cmdClear.Name = "m_cmdClear";
            this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClear.Size = new System.Drawing.Size(71, 26);
            this.m_cmdClear.TabIndex = 51;
            this.m_cmdClear.Text = "�� ��";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdGroupAdd
            // 
            this.m_cmdGroupAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdGroupAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdGroupAdd.DefaultScheme = true;
            this.m_cmdGroupAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGroupAdd.Hint = "";
            this.m_cmdGroupAdd.Location = new System.Drawing.Point(438, 376);
            this.m_cmdGroupAdd.Name = "m_cmdGroupAdd";
            this.m_cmdGroupAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGroupAdd.Size = new System.Drawing.Size(72, 28);
            this.m_cmdGroupAdd.TabIndex = 52;
            this.m_cmdGroupAdd.Text = "��Ϊ����";
            this.m_cmdGroupAdd.Visible = false;
            // 
            // dgv_orderTemp
            // 
            this.dgv_orderTemp.AllowUserToAddRows = false;
            this.dgv_orderTemp.BackgroundColor = System.Drawing.Color.White;
            this.dgv_orderTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_orderTemp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tmp_check,
            this.tmp_Name,
            this.tmp_ExecuteType,
            this.tmp_Dosage,
            this.tmp_UseType,
            this.tmp_Freq,
            this.tmp_Get});
            this.dgv_orderTemp.Location = new System.Drawing.Point(12, 12);
            this.dgv_orderTemp.Name = "dgv_orderTemp";
            this.dgv_orderTemp.ReadOnly = true;
            this.dgv_orderTemp.RowHeadersVisible = false;
            this.dgv_orderTemp.RowTemplate.Height = 23;
            this.dgv_orderTemp.Size = new System.Drawing.Size(483, 361);
            this.dgv_orderTemp.TabIndex = 53;
            this.dgv_orderTemp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_orderTemp_CellClick);
            // 
            // tmp_check
            // 
            this.tmp_check.FalseValue = "0";
            this.tmp_check.HeaderText = "";
            this.tmp_check.Name = "tmp_check";
            this.tmp_check.ReadOnly = true;
            this.tmp_check.TrueValue = "1";
            this.tmp_check.Width = 30;
            // 
            // tmp_Name
            // 
            this.tmp_Name.HeaderText = "����";
            this.tmp_Name.Name = "tmp_Name";
            this.tmp_Name.ReadOnly = true;
            // 
            // tmp_ExecuteType
            // 
            this.tmp_ExecuteType.HeaderText = "��/��";
            this.tmp_ExecuteType.Name = "tmp_ExecuteType";
            this.tmp_ExecuteType.ReadOnly = true;
            this.tmp_ExecuteType.Width = 80;
            // 
            // tmp_Dosage
            // 
            this.tmp_Dosage.HeaderText = "���� ";
            this.tmp_Dosage.Name = "tmp_Dosage";
            this.tmp_Dosage.ReadOnly = true;
            this.tmp_Dosage.Width = 60;
            // 
            // tmp_UseType
            // 
            this.tmp_UseType.HeaderText = "�÷�";
            this.tmp_UseType.Name = "tmp_UseType";
            this.tmp_UseType.ReadOnly = true;
            this.tmp_UseType.Width = 60;
            // 
            // tmp_Freq
            // 
            this.tmp_Freq.HeaderText = "Ƶ��";
            this.tmp_Freq.Name = "tmp_Freq";
            this.tmp_Freq.ReadOnly = true;
            this.tmp_Freq.Width = 60;
            // 
            // tmp_Get
            // 
            this.tmp_Get.HeaderText = "����";
            this.tmp_Get.Name = "tmp_Get";
            this.tmp_Get.ReadOnly = true;
            this.tmp_Get.Width = 80;
            // 
            // frmOrderTemplate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(507, 413);
            this.Controls.Add(this.dgv_orderTemp);
            this.Controls.Add(this.m_cmdClear);
            this.Controls.Add(this.m_cmdGroupAdd);
            this.Controls.Add(this.m_cmdRemove);
            this.Controls.Add(this.m_cmdAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_chkAll);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmOrderTemplate";
            this.Text = "frmOrderTemplate";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_orderTemp)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void m_lngAddOrder(ItemTag p_objAdd)
        {
            this.dgv_orderTemp.Rows.Add();

            DataGridViewRow objRow = this.dgv_orderTemp.Rows[this.dgv_orderTemp.RowCount - 1];

            objRow.Height = 20;

            //��
            //objRow.Cells["tmp_NO"].Value = this.dgv_orderTemp.RowCount.ToString();
            objRow.Cells["tmp_check"].Value = "1";
            //����
            objRow.Cells["tmp_Name"].Value = p_objAdd.objOrder.m_strName;
            //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
            //if (!objOrder.m_strPARTID_VCHR.Trim().Equals(""))
            //{
            //    objRow.Cells["tmp_method"].Value = objOrder.m_strPARTNAME_VCHR;
            //}
            //else if (!objOrder.m_strSAMPLEID_VCHR.Trim().Equals(""))
            //{
            //    objRow.Cells["tmp_method"].Value = objOrder.m_strSAMPLEName_VCHR;
            //}


            ////����  N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
            //if (objOrder.m_intExecuteType == 3)
            //{
            //    objRow.Cells["tmp_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objOrder.m_dmlGet * objOrder.m_intOUTGETMEDDAYS_INT) + objOrder.m_strGetunit;
            //}
            //else
            //{
            //    objRow.Cells["tmp_sum"].Value = "";
            //}
            ////objRow["TotalMoney"] =(double.Parse(objOrder.m_dmlGet.ToString()) * double.Parse(objOrder.m_dmlPrice.ToString())).ToString("0.00");
            //ִ��ʱ��/����
            //objRow.Cells["tmp_ENTRUST"].Value = objOrder.m_strEntrust;
            //�Ա�ҩ (1-ȫ�Ʒ� 1-��)( 2-�÷��շ� 2-��)(3 ���Ʒ� ����)
            //switch (objOrder.m_intRateType)
            //{
            //    case 0:
            //        objRow.Cells["RATETYPE_INT"].Value = "��";
            //        break;
            //    case 1:
            //        objRow.Cells["RATETYPE_INT"].Value = "��";
            //        break;
            //    case 2:
            //        objRow.Cells["RATETYPE_INT"].Value = "";
            //        break;

            //}


            /*<===============================*/
            #region ҽ�����Ϳ����б����
            // clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_frmInput.m_htOrderCate[objOrder.m_strOrderDicCateID];
            //clsT_aid_bih_ordercate_VO p_objItem = new clsT_aid_bih_ordercate_VO();
            //if (p_objItem != null)
            //{
            //    if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
            //    {
            //        //����
            //        if (objOrder.m_dmlDosage > 0)
            //        {
            //            objRow.Cells["tmp_Dosage"].Value = objOrder.m_dmlDosage.ToString() + " " + objOrder.m_strDosageUnit;
            //        }
            //        else
            //        {
            //            objRow.Cells["tmp_Dosage"].Value = "";

            //        }
            //    }
            //    else
            //    {
            //        objRow.Cells["tmp_Dosage"].Value = "";
            //    }
            //    if (p_objItem.m_intExecuFrenquenceType == 1)
            //    {
            //        //Ƶ��
            //        objRow.Cells["tmp_Freq"].Value = objOrder.m_strExecFreqName;
            //    }
            //    else
            //    {
            //        //Ƶ��
            //        objRow.Cells["tmp_Freq"].Value = "";
            //    }
            //    if (p_objItem.m_intUSAGEVIEWTYPE == 1)
            //    {
            //        //�÷�
            //        objRow.Cells["tmp_UseType"].Value = objOrder.m_strDosetypeName;
            //    }
            //    else
            //    {
            //        //�÷�
            //        objRow.Cells["tmp_UseType"].Value = "";
            //    }
            //    if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
            //    {
            //        //����
            //        objRow.Cells["ATTACHTIMES_INT"].Value = objOrder.m_intATTACHTIMES_INT;
            //    }
            //    else
            //    {
            //        //����
            //        objRow.Cells["ATTACHTIMES_INT"].Value = "";
            //    }
            //    //����
            //    if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
            //    {
            //        if (objOrder.m_dmlGet > 0)
            //        {
            //            objRow.Cells["tmp_Get"].Value = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;

            //        }
            //        else
            //        {
            //            objRow.Cells["tmp_Get"].Value = "";

            //        }
            //    }
            //    else
            //    {
            //        //����
            //        objRow.Cells["tmp_Get"].Value = "";
            //    }
            //}
            //else
            //{
            //����
            objRow.Cells["tmp_Dosage"].Value = p_objAdd.objOrder.m_dmlDosage.ToString() + " " + p_objAdd.objOrder.m_strDosageUnit;
            //Ƶ��
            objRow.Cells["tmp_Freq"].Value = p_objAdd.objOrder.m_strExecFreqName;
            //�÷�
            objRow.Cells["tmp_UseType"].Value = p_objAdd.objOrder.m_strDosetypeName;
            //����
            //objRow.Cells["tmp_ATTACHTIMES_INT"].Value = p_objAdd.objOrder.m_intATTACHTIMES_INT;
            //����
            objRow.Cells["tmp_Get"].Value = p_objAdd.objOrder.m_dmlGet.ToString() + " " + p_objAdd.objOrder.m_strGetunit;

            //}
            #endregion

            // ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��
            //��/��
            if (p_objAdd.objOrder.m_intExecuteType == 1)
            {
                objRow.Cells["tmp_ExecuteType"].Value = "����";

            }
            else if (p_objAdd.objOrder.m_intExecuteType == 2)
            {
                objRow.Cells["tmp_ExecuteType"].Value = "��ʱ";

            }
            else if (p_objAdd.objOrder.m_intExecuteType == 3)
            {
                objRow.Cells["tmp_ExecuteType"].Value = "��Ժ��ҩ";

            }
            else
            {
                objRow.Cells["tmp_ExecuteType"].Value = "";
            }

            //��Ժ��ҩ����
            //if (objOrder.m_intExecuteType == 3)
            //{
            //    objRow.Cells["tmp_OUTGETMEDDAYS_INT"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
            //}
            //else
            //{
            //    objRow.Cells["tmp_OUTGETMEDDAYS_INT"].Value = "";
            //}
            //ҽ����������
            //objRow.Cells["viewname_vchr"].Value = objOrder.m_strOrderDicCateName.ToString().Trim();



            /*<==================================================================*/
            objRow.Tag = p_objAdd;
        }

        /// <summary>
        /// ���ģ�崰���е���󷽺�
        /// </summary>
        /// <returns></returns>
        int iRes = 0;
        private int GetListRecipeNOInList()
        {

            iRes++;
            return iRes;
        }


        /// <summary>
        /// �����ҽ�����б���
        /// </summary>
        /// <param name="m_arrOrderList"></param>
        /// <returns></returns>
        public void m_lngAddOrderGroupList(System.Collections.Generic.List<clsBIHOrder> m_arrOrderList)
        {
            //ģ���ĺ�
            int iRecipeNO = 0;
            //ҽ������
            int m_intRecipenNo = 0;
            for (int i = 0; i < m_arrOrderList.Count; i++)
            {
                if (m_intRecipenNo != (m_arrOrderList[i]).m_intRecipenNo)
                {
                    iRecipeNO = GetListRecipeNOInList();

                }
                ItemTag itOrder = new ItemTag();
                itOrder.objOrder = this.frmParent.CopyNewOrder(m_arrOrderList[i]);
                itOrder.intGroupNO = iRecipeNO;
                itOrder.objOrder.m_intRecipenNo = iRecipeNO;
                m_lngAddOrder(itOrder);
                m_intRecipenNo = (m_arrOrderList[i]).m_intRecipenNo;
            }
        }

        /// <summary>
        /// �����ҽ�����б���
        /// </summary>
        /// <param name="m_arrOrderList"></param>
        /// <returns></returns>
        public void m_lngAddOrderGroupTempList(System.Collections.Generic.List<clsBIHOrder> m_arrOrderList)
        {
            this.m_arrOrderTempList.Clear();
            for (int i = 0; i < m_arrOrderList.Count; i++)
            {

                clsBIHOrder Order = new clsBIHOrder();
                Order = this.frmParent.CopyNewOrder(m_arrOrderList[i]);

                this.m_arrOrderTempList.Add(Order);

            }
        }


        private void m_cmdAdd_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<clsBIHOrder> m_arrOrderSameNo = GetTheSelectItemWithSon();
            string[] p_strRecordIDArr = null;
            clsBIHOrder objOrder = null;
            for (int i = 0; i < m_arrOrderSameNo.Count; i++)
            {
                objOrder = m_arrOrderSameNo[i] as clsBIHOrder;

                if (!this.frmParent.m_objDomain.m_blChcekOpcurrentgross(ref objOrder))
                {
                    return;
                }
                this.frmParent.SetTheCurrentOrder((clsBIHOrder)m_arrOrderSameNo[i]);
            }
            try
            {

                this.frmParent.m_objDomain.m_lngAddNewOrderByGroup(out p_strRecordIDArr, m_arrOrderSameNo);
                this.frmParent.m_objDomain.m_mthLoadOrderList();
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�Ѹ��Ƶ���ʱ�����ҽ����Ŀ
        /// </summary>
        /// <returns></returns>
        public int getTheTempCout()
        {
            return m_arrOrderTempList.Count;
        }
        /// <summary>
        /// ���Ƶ�ǰҽ��
        /// </summary>
        public void m_cmdParseOrder()
        {
            string[] p_strRecordIDArr = null;
            System.Collections.Generic.List<clsBIHOrder> m_arrOrderSameNo = new System.Collections.Generic.List<clsBIHOrder>();
            for (int i = 0; i < m_arrOrderTempList.Count; i++)
            {
                clsBIHOrder objOrder = this.frmParent.CopyNewOrder(m_arrOrderTempList[i]);
                //ճ��ҽ�������ж�
                if (!this.frmParent.m_objDomain.m_blChcekOpcurrentgross(ref objOrder))
                {
                    return;
                }
                m_arrOrderSameNo.Add(objOrder);
            }

            lngParentOrderSet(ref m_arrOrderSameNo);
            if (this.frmParent != null)
            {
                try
                {
                    this.frmParent.m_objDomain.m_lngAddNewOrderByGroup(out p_strRecordIDArr, m_arrOrderSameNo);

                    this.frmParent.m_objDomain.m_mthLoadOrderList();
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        ///��õ�ǰѡ�е�ҽ���б�(��������ҽ��)
        /// </summary>
        /// <returns></returns>
        internal System.Collections.Generic.List<clsBIHOrder> GetTheSelectItemWithSon()
        {
            //�����Ѵ��ڵ�ҽ����
            ArrayList m_arrOrderHave = new ArrayList();
            System.Collections.Generic.List<clsBIHOrder> m_arrOrderSameNo = new System.Collections.Generic.List<clsBIHOrder>();
            int m_intRecipenNo = -1;
            for (int i = 0; i < this.dgv_orderTemp.RowCount; i++)
            {
                if (this.dgv_orderTemp.Rows[i].Cells["tmp_check"].Value.ToString().Trim().Equals("1"))
                {
                    m_arrOrderSameNo.Add(this.frmParent.CopyNewOrder(((ItemTag)this.dgv_orderTemp.Rows[i].Tag).objOrder));
                }
            }

            lngParentOrderSet(ref m_arrOrderSameNo);

            return m_arrOrderSameNo;
        }

        /// <summary>
        /// Ϊ��ǰҽ���б���и�������
        /// </summary>
        /// <param name="m_arrOrderSameNo"></param>
        private void lngParentOrderSet(ref System.Collections.Generic.List<clsBIHOrder> m_arrOrderSameNo)
        {
            //���õ�һ��ҽ��Ϊ��ҽ����������ڸ��ӵ�����£�
            if (m_arrOrderSameNo.Count > 0)
            {
                ArrayList m_arrParentSet = new ArrayList();
                for (int i = 0; i < m_arrOrderSameNo.Count; i++)
                {
                    this.frmParent.SetTheCurrentOrder(m_arrOrderSameNo[i]);
                    if (i < m_arrOrderSameNo.Count - 1)
                    {
                        if (m_arrParentSet.Contains((m_arrOrderSameNo[i]).m_intRecipenNo))
                        {
                            continue;
                        }
                        if ((m_arrOrderSameNo[i]).m_intRecipenNo == (m_arrOrderSameNo[i + 1]).m_intRecipenNo)
                        {
                            (m_arrOrderSameNo[i]).m_intIFPARENTID_INT = 1;
                            m_arrParentSet.Add((m_arrOrderSameNo[i]).m_intRecipenNo);
                        }
                    }
                }
            }
        }
        private void dgv_orderTemp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowNum = e.RowIndex;
                if (this.dgv_orderTemp.Rows.Count > 0 && e.RowIndex >= 0)
                {
                    if (this.dgv_orderTemp.Rows[rowNum].Cells["tmp_check"].Value.ToString().Trim().Equals("0"))
                    {
                        this.dgv_orderTemp.Rows[rowNum].Cells["tmp_check"].Value = "1";
                    }
                    else if (this.dgv_orderTemp.Rows[rowNum].Cells["tmp_check"].Value.ToString().Trim().Equals("1"))
                    {
                        this.dgv_orderTemp.Rows[rowNum].Cells["tmp_check"].Value = "0";
                    }

                }
                //ͬ������
                TheSamerecipeno(rowNum);
            }
        }

        private void TheSamerecipeno(int rowNum)
        {
            string m_strCheck = this.dgv_orderTemp.Rows[rowNum].Cells["tmp_check"].Value.ToString().Trim();
            int No = ((ItemTag)this.dgv_orderTemp.Rows[rowNum].Tag).intGroupNO;
            for (int i = 0; i < this.dgv_orderTemp.RowCount; i++)
            {
                if (((ItemTag)this.dgv_orderTemp.Rows[i].Tag).intGroupNO == No)
                {
                    this.dgv_orderTemp.Rows[i].Cells["tmp_check"].Value = m_strCheck;
                }
            }
        }

        private void m_cmdRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgv_orderTemp.RowCount; i++)
            {
                if (this.dgv_orderTemp.Rows[i].Cells["tmp_check"].Value.ToString().Trim().Equals("1"))
                {
                    this.dgv_orderTemp.Rows.Remove(this.dgv_orderTemp.Rows[i]);
                    i = i - 1;
                }

            }
        }

        private void m_cmdClear_Click(object sender, EventArgs e)
        {
            this.dgv_orderTemp.Rows.Clear();
        }

        private void m_chkAll_CheckedChanged(object sender, EventArgs e)
        {
            string check = "0";
            if (this.m_chkAll.Checked == true)
            {
                check = "1";
            }
            else
            {
                check = "0";
            }
            for (int i = 0; i < this.dgv_orderTemp.RowCount; i++)
            {
                this.dgv_orderTemp.Rows[i].Cells["tmp_check"].Value = check;
            }
        }
    }

    /// <summary>
    /// ItemTag �б�Tagʹ�õ��࣬����ԭ���Ų��䣬Ϊ�����б��а���ѡ��������һ������
    /// </summary>
    public class ItemTag
    {
        public ItemTag()
        {
            intGroupNO = 0;
            objOrder = new clsBIHOrder();
        }
        public int intGroupNO;
        public clsBIHOrder objOrder;
    }

}
