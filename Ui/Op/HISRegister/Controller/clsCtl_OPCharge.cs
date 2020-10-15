using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using com.digitalwave.iCare.gui.LIS;
using weCare.Core.Entity;
using weCare.Core.Utils;
using com.digitalwave.iCare.middletier.HI;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsCtl_OPCharge ��ժҪ˵����
    /// </summary>
    public class clsCtl_OPCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��Ҫ������ҩƷ����ID����ҩ��1����ҩ��2������
        /// </summary>
        private ArrayList m_objNeedCheckArr = null;
        /// <summary>
        /// 
        /// </summary>
        internal bool m_blnSecondStockLimitFlag = false;
        /// <summary>
        /// �Զ��ۼ�ҩ�����ۿ��ķ�ʽ 1 �����ʱ�䣻2 ����Ч�ڣ�3 �Ȱ����ʱ���ٰ���Ч�ڣ�4 �Ȱ���Ч���ٰ����ʱ��
        /// </summary>
        internal string m_strDeductType = "1";
        private clsDcl_OPCharge objSvc = null;
        /// <summary>
        /// ȫ�ֱ���,����Ѿ��շѴ�����Ϊfalse,����Ϊtrue
        /// </summary>
        public bool IsReadOnly = true;
        /// <summary>
        /// ��ҩ��ID
        /// </summary>
        private string strWMedicineStoreID = "0001";
        /// <summary>
        /// ��ҩ��ID
        /// </summary>
        private string strCMedicineStoreID = "0003";
        /// <summary>
        /// ��ҩ����ID
        /// </summary>
        private string strWMedicineWindowID = "0001";
        /// <summary>
        /// ��ҩ����ID
        /// </summary>
        private string strCMedicineWindowID = "0001";
        /// <summary>
        /// ���Ͽ�ID
        /// </summary>
        private string strMaterialStoreID = "0001";
        /// <summary>
        /// ���ϴ���ID
        /// </summary>
        private string strMaterialWindowID = "0001";

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        private string strInvoiceExpression = "";
        /// <summary>
        /// ��ҩ����ӡ������
        /// </summary>
        private string strSendMedicinePrinterName = "";
        /// <summary>
        /// ��¼����һ��ҽԺ�շ�(ֻ������ҽԺ����)
        /// </summary>
        public string strHopitalID = "0";
        /// <summary>
        /// �ж��ܷ��޸�ҽ�����Ĵ���
        /// </summary>
        private bool IsCanModify = true;
        /// <summary>
        /// �ж��Ƿ�����޸�ҽ����������������������
        /// </summary>
        private bool IsModifyrecipetype = false;
        /// <summary>
        /// �ж��Ƿ���ʾȱҩ true ��ʾ,false����ʾ
        /// </summary>
        private bool isShowLackMedicine = true;
        /// <summary>
        /// �ж��Ƿ�����տ�Ա����
        /// </summary>
        internal bool IsChargeReceiverRec = true;
        /// <summary>
        /// �Ƿ���ת��,true ��
        /// </summary>
        internal bool IsCanTurn = true;
        /// <summary>
        /// �Ƿ����Զ������,true ����,false ������
        /// </summary>
        internal bool IsDiscount = true;
        /// <summary>
        /// �Ƿ���������,1 ����,0 ������
        /// ��������
        /// </summary>
        internal int intDiffPriceOn = 0;

        /// <summary>
        /// Hashtable���󣬱��洦����Ϣ���������¼ƷѶ���ʱ��ա�
        /// </summary>
        internal Hashtable objHashTable = null;
        /// <summary>
        /// ������ĿID��
        /// </summary>
        private const int ResubitemCol = 27;
        /// <summary>
        /// ��Ŀ����������
        /// </summary>
        private const int ResubnumsCol = 28;
        /// <summary>
        /// �÷���ĿID��
        /// </summary>
        private const int UsageitemCol = 29;
        /// <summary>
        /// �÷�����������
        /// </summary>
        private const int UsagenumsCol = 30;
        /// <summary>
        /// �����Ա�ҩ��־
        /// </summary>
        internal const int Deptmed = 31;
        /// <summary>
        /// ������ĿID
        /// </summary>
        private const int OrderID = 32;
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        private const int OrderNum = 33;
        /// <summary>
        /// Ĭ�ϴ�����
        /// </summary>
        private const int DefaultCol = 35;
        /// <summary>
        /// �����ܽ���� 
        /// </summary>
        private const int intDiffPriceTotalCol = 37;
        /// <summary>
        /// ���������� 
        /// </summary>
        private const int intDiffUnitPriceCol = 38;
        /// <summary>
        /// Ĭ�ϴ�����Ŀʱ�Ƿ�ɾ���ϴμƷ���
        /// </summary>
        private bool IsChrgFlag = true;
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private List<clsOutPatientRecipe_VO> m_objMainRecipeList;
        /// <summary>
        /// ��ҩ������Ϣ
        /// </summary>
        private List<clsMedrecipesend_VO> m_objCMSendList;
        /// <summary>
        /// ��ҩ������Ϣ
        /// </summary>
        private List<clsMedrecipesend_VO> m_objWMSendList;
        /// <summary>
        /// XML�ļ���
        /// </summary>
        private string XMLFile = System.Windows.Forms.Application.StartupPath + @"\LoginFile.xml";

        /// <summary>
        /// ���Ŵ����ϼ�״̬��־
        /// </summary>
        private bool Recsumflag = false;
        /// <summary>
        /// ҽ�����ݿ����Ӳ���(DB2)
        /// </summary>
        private string DB2Parm = "";
        /// <summary>
        /// ҽ�����ݿ����Ӳ���(SQL SERVER)
        /// </summary>
        private string SQLParm = "";
        /// <summary>
        /// DBF DBQ
        /// </summary>
        private string DBQ = "";
        /// <summary>
        /// (ҽ��)ҽԺ����
        /// </summary>
        private string Hospcode = "";
        /// <summary>
        /// (ҽ��)��������
        /// </summary>
        private ArrayList arrPaytype;
        /// <summary>
        /// ҽ����Hash��
        /// </summary>
        private Hashtable HasYB = null;
        /// <summary>
        /// ҽ�����ʵ���
        /// </summary>
        internal string BillNO = "";
        /// <summary>
        /// ҩ��ר�ô����Ƿ���Խ������п��Ҵ��� true ���� false ��ֹ
        /// </summary>
        private bool Ismedwinpublic;
        /// <summary>
        /// ��������(��������Ŵ���)����
        /// </summary>
        private bool BathCalc = false;
        /// <summary>
        /// �����÷���Ӧ��ҩ��ID����
        /// </summary>
        private ArrayList WMUsageIDArr = new ArrayList();
        /// <summary>
        /// �����÷���Ӧ��ҩ��ID����
        /// </summary>
        private ArrayList CMUsageIDArr = new ArrayList();
        /// <summary>
        /// �����÷���Ӧ���Ϸ��Ŵ�ID����
        /// </summary>
        private ArrayList MATUsageIDArr = new ArrayList();
        /// <summary>
        /// 001 ��ɽ�� 002 ��ݸ�� 003 ��ɽ˳����
        /// </summary>
        internal string YBType = "001";
        /// <summary>
        /// �շѴ�ID
        /// </summary>
        private string SDYBMzbm = "";
        /// <summary>
        /// �շ�Ա¼����ģʽ
        /// </summary>
        internal bool EmpInputMode = false;
        /// <summary>
        /// �������� 0 ������ 1 �������뵽�� 2 �������� 3 ������Ԫ
        /// </summary>
        internal string RoundingRule = "0";
        /// <summary>
        /// ��������
        /// </summary>
        internal string RoundingCode = "";
        /// <summary>
        /// �Ƿ��Ƕ�ݸ������ҽ����ݣ��ѱ�
        /// </summary>
        internal bool IsDongGuanYBPatient = false;
        /// <summary>
        /// ��ݸ������ҽ����ݣ��ѱ𣩶�ӦID (����)
        /// </summary>
        private ArrayList YBPayTypeArr = new ArrayList();
        /// <summary>
        /// ��ݸ������ҽ�����ˣ�����ҽ������վ���շѴ��Ƿ���ʾȫ�Էѵ��շ���Ŀ
        /// </summary>
        private bool YBIsShowSelfItem = false;
        /// <summary>
        /// ����շѹ���վ true �� false ��
        /// </summary>
        internal bool PEWorkStationFlag = false;
        /// <summary>
        /// ����
        /// </summary>
        internal string PERegisterNoArr = "";

        /// <summary>
        /// 0077�����շѴ������Ƿ��͵����Ϸ� 0-��1��
        /// </summary>
        private int m_intFlag = 0;
        /// <summary>
        /// ��Է�Ժ�շѼ�������ҩƷ�Ƿ�����ҩ�� 1-����������0-������
        /// </summary>
        private int m_intIsOrNotSendMed = 0;
        /// <summary>
        /// ѡ�񽻷ѵ���Ŀid
        /// </summary>
        internal Dictionary<string, List<string>> m_dicSelectChargeItemID = null;

        /// <summary>
        /// �Ƿ�ʹ�ö�ͯ�۸� 9015
        /// </summary>
        bool isUseChildPrice { get; set; }

        #endregion

        #region ��ini�ļ�
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA")]
        public static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        #endregion

        #region �жϷ�Ʊ�Ƿ����ظ�
        private bool isCanDo = false;
        /// <summary>
        /// �жϷ�Ʊ�Ƿ����ظ�
        /// </summary>
        public bool InvoiceCanRepeat
        {
            get
            {
                return isCanDo;
            }
        }
        private bool _IsPrintSendMedicineBill = false;
        /// <summary>
        /// �ж��Ƿ��ӡ��ҩ��
        /// </summary>
        public bool IsPrintSendMedicineBill
        {
            get
            {
                return _IsPrintSendMedicineBill;
            }
        }
        #endregion
        /// <summary>
        /// �Ƿ���ʾ�շ�Ա������Ϣ 0-����ʾ;1-��ʾ
        /// </summary>
        private string m_strShowOPChargeManInfo = string.Empty;
        public clsCtl_OPCharge()
        {
            objSvc = new clsDcl_OPCharge();
            objHashTable = new Hashtable();
            m_objMainRecipeList = new List<clsOutPatientRecipe_VO>();
            m_objCMSendList = new List<clsMedrecipesend_VO>();
            m_objWMSendList = new List<clsMedrecipesend_VO>();
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmOPCharge m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOPCharge)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡ��Ʊ��
        //		private  string m_strConfigFilePath="InvoiceNO.bin";
        public void m_strReadMedwinID()
        {
            try
            {
                string patXML = Application.StartupPath + "\\LoginFile.xml";
                if (File.Exists(patXML))
                {
                    string strCurrEmpNO = "AnyOne";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(patXML);
                    XmlNode xn = doc.DocumentElement.SelectNodes(@"//register")[0];
                    XmlNode xnCurr = xn.SelectSingleNode(@"//WMedicinestore[@key='" + strCurrEmpNO + @"']");
                    if (xnCurr != null)
                    {
                        this.strWMedicineStoreID = xnCurr.Attributes["value"].Value.ToString();
                        this.strWMedicineWindowID = xnCurr.Attributes["windows"].Value.ToString();
                    }
                    xnCurr = xn.SelectSingleNode(@"//CMedicinestore[@key='" + strCurrEmpNO + @"']");
                    if (xnCurr != null)
                    {
                        this.strCMedicineStoreID = xnCurr.Attributes["value"].Value.ToString();
                        this.strCMedicineWindowID = xnCurr.Attributes["windows"].Value.ToString();
                    }
                    xnCurr = xn.SelectSingleNode(@"//MaterialStore[@key='" + strCurrEmpNO + @"']");
                    if (xnCurr != null)
                    {
                        this.strMaterialStoreID = xnCurr.Attributes["value"].Value.ToString();
                        this.strMaterialWindowID = xnCurr.Attributes["windows"].Value.ToString();
                    }
                    xn = doc.DocumentElement.SelectNodes(@"//Printer")[0];
                    xnCurr = xn.SelectSingleNode(@"//SendMedicine");
                    if (xnCurr != null)
                    {
                        this.strSendMedicinePrinterName = xnCurr.Attributes["PrinterName"].Value.ToString();
                    }
                    if (this.strInvoiceExpression.Trim() == "")
                    {
                        xn = doc.DocumentElement.SelectSingleNode("InvoiceExpression");
                        this.strInvoiceExpression = xn.InnerText;
                    }

                    #region ҩƷ�Ƿ��͵�ҩ�� 2011-9-5 by zxm
                    xnCurr = xn.SelectSingleNode(@"//IsOrNotSendMed[@key='" + strCurrEmpNO + @"']");
                    if (xnCurr != null)
                    {
                        this.m_intIsOrNotSendMed = Convert.ToInt32(xnCurr.Attributes["value"].Value.ToString().Trim());
                    }
                    #endregion


                }
            }
            catch
            {
            }
        }
        public string m_strReadInvoiceNO()
        {
            string invono = "";

            try
            {
                string patXML = Application.StartupPath + "\\LoginFile.xml";
                if (File.Exists(patXML))
                {
                    string strCurrEmpNO = "AnyOne";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(patXML);
                    XmlNode xn = doc.DocumentElement.SelectNodes(@"//register")[0];
                    XmlNode xnCurr = xn.SelectSingleNode(@"//InvoiceNo[@key='" + strCurrEmpNO + @"']");
                    if (xnCurr != null)
                    {
                        int maxint = Convert.ToInt32(xnCurr.Attributes["value"].Value.Substring(2, 8)) + 1;
                        invono = xnCurr.Attributes["value"].Value.Substring(0, 2) + maxint.ToString("00000000");
                    }
                    else
                    {
                        invono = "DW00000001";

                    }
                    if (this.strInvoiceExpression.Trim() == "")
                    {
                        xn = doc.DocumentElement.SelectSingleNode("InvoiceExpression");
                        this.strInvoiceExpression = xn.InnerText;
                    }
                }
            }
            catch
            {
                invono = "DW00000001";
            }

            return invono;
        }

        public void m_mthSaveInvoiceNO(string strInvoiceNO)
        {
            m_objViewer.m_txtInvoiceNO.Text = strInvoiceNO;
            string strTemp = "0000000000";
            try
            {
                int maxint = Convert.ToInt32(strInvoiceNO.Substring(2, 8)) - 1;
                strTemp = strInvoiceNO.Substring(0, 2) + maxint.ToString("00000000");
            }
            catch { }

            if (!(this.m_blnWriteXML("register", "InvoiceNo", "AnyOne", strTemp)))
            {
                MessageBox.Show("\t���淢Ʊ��ʧ��,\n����\"" + XMLFile + "\"�Ƿ�ֻ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region ����һ���µļƷ������
        public com.digitalwave.iCare.middletier.HI.clsCalcPatientCharge objCalPatientCharge = null;
        /// <summary>
        /// ����һ���µļƷ������
        /// </summary>
        public void m_mthCreatCalObj()
        {
            //�ѵ�ǰ����ID���ݵ����Ҵ�����Ϣ�ؼ���
            this.m_objViewer.txtLoadRecipeNO.PatientID = this.m_objViewer.m_PatientBasicInfo.PatientID;
            //�Ѱ�ť��Ϊ����״̬��
            this.m_mthSetControlEnable(true);
            //��֧�����ͻ�ԭ��Ĭ��״̬
            this.m_objViewer.m_cmbPayMode.SelectedIndex = 0;
            //�Ѵ������ͻ�ԭ��Ĭ��״̬
            this.m_objViewer.m_cmbRecipeType.SelectedIndex = 0;
            //����һ���µļƷѶ���
            this.m_mthCreatNewCalobj();
            this.m_objViewer.btSave.Tag = null;
            //�ӼƷ����ȡ�շ���ϸ���շ���ϸ���������շ����е�һ������(VO)
            this.m_mthDisplayCharge();
            SumRecipeCount = 0;
            SumTotalMoney = 0;
            SumChargeUpMoney = 0;
            this.strSumChargeUpMoney = "";
            this.strSumPersonMoney = "";
            this.strSumTotalMoney = "";
            SumPersonMoney = 0;
            RecipeCountThisTime = 0;
        }
        /// <summary>
        /// ����һ���µļƷ���
        /// </summary>
        private void m_mthCreatNewCalobj()
        {
            this.objHashTable.Clear();
            objCalPatientCharge = null;
            objCalPatientCharge = new clsCalcPatientCharge(m_objViewer.m_PatientBasicInfo.PatientID, m_objViewer.m_PatientBasicInfo.PayTypeID, m_objViewer.m_PatientBasicInfo.Limit, this.m_objComInfo.m_strGetHospitalTitle(), m_objViewer.m_PatientBasicInfo.PatientType, m_objViewer.m_PatientBasicInfo.Discount);
            if (this.m_objViewer.panel2.Controls.Count > 0)
            {
                this.objCalPatientCharge.GetDisplayControl = this.m_objViewer.panel2.Controls[0];
            }
            else
            {
                this.m_objViewer.panel2.Controls.Add(this.objCalPatientCharge.GetDisplayControl);
            }
            this.m_objViewer.ctlDataGrid1.m_mthDeleteAllRow();
            m_objViewer.numericUpDown1.Value = 1;
            // ���üƷ����е���������
            if (this.intDiffPriceOn == 1)
                objCalPatientCharge.ObjMain.m_blnDiffCostOn = true;
            else
                objCalPatientCharge.ObjMain.m_blnDiffCostOn = false;
        }
        #endregion

        #region �ж��Ƿ�����
        /// <summary>
        /// �ж��Ƿ�����
        /// </summary>
        /// <param name="rowNo"></param>
        /// <returns></returns>
        bool CheckIsDiff(int rowNo)
        {
            if (m_objViewer.ctlDataGrid1[rowNo, 10] != null && m_objViewer.ctlDataGrid1[rowNo, 10].ToString().Trim() != string.Empty)
                return blMedicine9003(m_objViewer.ctlDataGrid1[rowNo, 10].ToString());
            else
                return false;
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool CheckIsDiff(string itemId)
        {
            if (!string.IsNullOrEmpty(itemId))
                return blMedicine9003(itemId);
            else
                return false;
        }
        #endregion

        #region �Ƿ��ͯ.������ö�ͯ�۸�
        /// <summary>
        /// �Ƿ��ͯ.������ö�ͯ�۸�
        /// </summary>
        bool IsChildPrice
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_objViewer.m_PatientBasicInfo.PatientBirth))
                {
                    return false;
                }
                else
                {
                    if (this.isUseChildPrice)
                        return new clsBrithdayToAge().IsChild(Convert.ToDateTime(this.m_objViewer.m_PatientBasicInfo.PatientBirth));
                    else
                        return false;
                }
            }
        }
        #endregion

        #region ����ҩƷ
        /// <summary>
        /// ����ҩƷ
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int m_mthFindMedicineByID(string strType, string ID)
        {
            int ret = 0;
            DataTable dt = null;
            string strpayID = "0001";
            if (this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim() != "")
            {
                strpayID = this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim();
            }
            long strRet = objSvc.m_mthFindMedicineByID(strType, ID, strpayID, out dt, this.m_objViewer.LoginInfo.m_strEmpID, this.IsChildPrice);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1 && blnSinglechrgitem)
                {
                    ret = dt.Rows.Count;
                    int row = m_objViewer.rowNO;
                    //					int row =m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
                    if (dt.Rows[0]["noqtyflag_int"].ToString().Trim() != "" && dt.Rows[0]["noqtyflag_int"].ToString().Trim() != "0" && m_mthIsMedicine(dt.Rows[0]["itemopinvtype_chr"].ToString().Trim()))
                    {
                        //if (isShowLackMedicine)
                        //{

                        //    if (MessageBox.Show("ȱҩ!�Ƿ����?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //    {
                        //        return 0;
                        //    }
                        //}
                        //else
                        //{
                        MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
                        return 0;
                        //}

                    }

                    //��ݸ����ҽ��
                    if (!YBIsShowSelfItem && IsDongGuanYBPatient)
                    {
                        if (dt.Rows[0]["precent_dec"].ToString().Trim() == "100")
                        {
                            return ret;
                        }
                    }

                    string strCurrItem = m_objViewer.ctlDataGrid1[row, 10].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 0] = dt.Rows[0]["tempitemcode"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 2] = dt.Rows[0]["itemname_vchr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dt.Rows[0]["itemopinvtype_chr"].ToString().Trim());
                    m_objViewer.ctlDataGrid1[row, 4] = dt.Rows[0]["itemspec_vchr"].ToString().Trim();
                    //�������ҩƷ��������Ŀ��ʹ�÷�ҩƷ��λ
                    string strTemp = m_mthRelationInfo(dt.Rows[0]["itemopinvtype_chr"].ToString().Trim());
                    if (strTemp != "0001" && strTemp != "0002" && strTemp != "0005")//��ҩʱȡ��һ����λ
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dt.Rows[0]["unit"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dt.Rows[0]["itemprice_mny"].ToString().Trim();
                    }
                    else
                    {
                        if (/*m_mthIsMedicine(dt.Rows[0]["itemopinvtype_chr"].ToString().Trim()) && */dt.Rows[0]["opchargeflg_int"].ToString().Trim() == "1")
                        {
                            m_objViewer.ctlDataGrid1[row, 5] = dt.Rows[0]["itemipunit_chr"].ToString().Trim();
                            m_objViewer.ctlDataGrid1[row, 6] = dt.Rows[0]["submoney"].ToString().Trim();
                            m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = dt.Rows[0]["subtrademoney"].ToString().Trim();     // ������λ����
                        }
                        else
                        {
                            m_objViewer.ctlDataGrid1[row, 5] = dt.Rows[0]["itemopunit_chr"].ToString().Trim();
                            m_objViewer.ctlDataGrid1[row, 6] = dt.Rows[0]["itemprice_mny"].ToString().Trim();
                            m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = dt.Rows[0]["tradeprice_mny"].ToString().Trim();    // ��������
                        }
                    }
                    m_objViewer.ctlDataGrid1[row, 36] = dt.Rows[0]["opchargeflg_int"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 8] = dt.Rows[0]["itemopinvtype_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 9] = m_mthRelationInfo(dt.Rows[0]["itemopinvtype_chr"].ToString().Trim());
                    if (m_objViewer.ctlDataGrid1[row, 9].ToString() != "0001" && m_objViewer.ctlDataGrid1[row, 9].ToString() != "0002")//��ҩʱ��ȡ��һ��λ
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dt.Rows[0]["unit"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dt.Rows[0]["itemprice_mny"].ToString().Trim();
                    }

                    m_objViewer.ctlDataGrid1[row, 10] = dt.Rows[0]["itemid_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 11] = dt.Rows[0]["selfdefine_int"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 15] = dt.Rows[0]["itemopcalctype_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 16] = 0;
                    m_objViewer.ctlDataGrid1[row, 17] = "";
                    m_objViewer.ctlDataGrid1[row, 18] = "";
                    m_objViewer.ctlDataGrid1[row, 21] = dt.Rows[0]["dosageunit_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, "colYbcode"] = dt.Rows[0]["insuranceid_chr"].ToString().Trim();

                    string strPRECENT_DEC = "100";
                    if (dt.Rows[0]["precent_dec"].ToString().Trim() != "")
                    {
                        strPRECENT_DEC = dt.Rows[0]["precent_dec"].ToString().Trim();
                    }
                    m_objViewer.ctlDataGrid1[row, 13] = strPRECENT_DEC + "%";
                    m_objViewer.ctlDataGrid1[row, 14] = this.m_mthConvertObjToDecimal(strPRECENT_DEC);

                    if (m_objViewer.ctlDataGrid1[row, 12].ToString().Trim() != "")
                    {
                        m_objViewer.IsSave = false;
                    }

                    m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, 1);
                    m_objViewer.ctlDataGrid1[row, 0] = dt.Rows[0]["tempitemcode"].ToString().Trim();
                }
                else
                {
                    this.m_objViewer.listView1.BeginUpdate();
                    m_objViewer.listView1.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //��ݸ����ҽ��
                        if (!YBIsShowSelfItem && IsDongGuanYBPatient)
                        {
                            if (dt.Rows[i]["precent_dec"].ToString().Trim() == "100")
                            {
                                continue;
                            }
                        }

                        ListViewItem lv = new ListViewItem(dt.Rows[i]["type"].ToString().Trim());////��ѯ��
                        lv.SubItems.Add(dt.Rows[i]["itemcode_vchr"].ToString().Trim());//��ѯ��
                        lv.SubItems.Add(dt.Rows[i]["itemname_vchr"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["itemengname_vchr"].ToString().Trim());// Ӣ����
                        lv.SubItems.Add(m_mthConvertToChType(dt.Rows[i]["itemopinvtype_chr"].ToString().Trim()));//����
                        lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());//���
                        //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                        //string strTemp = m_mthRelationInfo(dt.Rows[i]["itemopinvtype_chr"].ToString().Trim());
                        //if (strTemp != "0001" && strTemp != "0002" && strTemp != "0005")//��ҩʱȡ��һ����λ
                        //{
                        //    lv.SubItems.Add(dt.Rows[i]["unit"].ToString().Trim());//����
                        //    lv.SubItems.Add(dt.Rows[i]["itemprice_mny"].ToString().Trim());//����
                        //}
                        //else
                        //{
                        //    if (m_mthIsMedicine(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()) && dt.Rows[i]["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                        //    {
                        //        lv.SubItems.Add(dt.Rows[i]["itemipunit_chr"].ToString().Trim());//��λ
                        //        lv.SubItems.Add(dt.Rows[i]["submoney"].ToString().Trim());//����
                        //    }
                        //    else
                        //    {
                        //        lv.SubItems.Add(dt.Rows[i]["itemopunit_chr"].ToString().Trim());//��λ
                        //        lv.SubItems.Add(dt.Rows[i]["itemprice_mny"].ToString().Trim());//����
                        //    }
                        //}
                        //�������ҩƷ��������Ŀ��ʹ�÷�ҩƷ��λ
                        string strTemp = m_mthRelationInfo(dt.Rows[i]["itemopinvtype_chr"].ToString().Trim());
                        if (strTemp != "0001" && strTemp != "0002" && strTemp != "0005")//��ҩʱȡ��һ����λ
                        {
                            lv.SubItems.Add(dt.Rows[i]["unit"].ToString().Trim());//����
                            lv.SubItems.Add(dt.Rows[i]["itemprice_mny"].ToString().Trim());//����
                        }
                        else  //�����ҩƷ��������Ŀ��ʹ���趨�������շѵ�λ
                        {
                            if (/*m_mthIsMedicine(dt.Rows[i]["itemopinvtype_chr"].ToString().Trim()) && */dt.Rows[i]["opchargeflg_int"].ToString().Trim() == "1")
                            {
                                lv.SubItems.Add(dt.Rows[i]["itemipunit_chr"].ToString().Trim());//��λ
                                lv.SubItems.Add(dt.Rows[i]["submoney"].ToString().Trim());//����
                            }
                            else
                            {
                                lv.SubItems.Add(dt.Rows[i]["itemopunit_chr"].ToString().Trim());//��λ
                                lv.SubItems.Add(dt.Rows[i]["itemprice_mny"].ToString().Trim());//����
                            }
                        }
                        string strPRECENT_DEC = "100";
                        if (dt.Rows[i]["precent_dec"].ToString().Trim() != "")
                        {
                            strPRECENT_DEC = dt.Rows[i]["precent_dec"].ToString().Trim();
                        }
                        lv.SubItems.Add(strPRECENT_DEC + "%");//�շѱ���
                        lv.SubItems.Add("");
                        //if (dt.Rows[i]["noqtyflag_int"].ToString().Trim() != "" && dt.Rows[i]["noqtyflag_int"].ToString().Trim() != "0" && m_mthIsMedicine(dt.Rows[i]["itemopinvtype_chr"].ToString().Trim()))
                        //{
                        //    if (!isShowLackMedicine)
                        //    {
                        //        continue;
                        //    }

                        //    lv.SubItems.Add("ȱҩ");
                        //    lv.ForeColor = Color.Red;
                        //    lv.SubItems.Add("");//�Ƿ�ȱҩ
                        //}
                        //else
                        //{
                        //    lv.SubItems.Add("");//�Ƿ�ȱҩ
                        //}
                        //						lv.SubItems.Add(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim());//���﷢Ʊ�������
                        //						lv.SubItems.Add(dt.Rows[i]["ITEMCATID_CHR"].ToString().Trim());//�շ���Ŀ����ID
                        //						lv.SubItems.Add(dt.Rows[i]["SELFDEFINE_INT"].ToString().Trim());//�Ƿ��Զ���۸�
                        lv.Tag = dt.Rows[i];
                        m_objViewer.listView1.Items.Add(lv);
                    }
                    this.m_objViewer.listView1.EndUpdate();
                    if (m_objViewer.listView1.Items.Count > 0)
                    {
                        m_objViewer.listView1.Height = 175;
                        m_objViewer.listView1.Visible = true;
                        m_objViewer.listView1.Items[0].Selected = true;
                        m_objViewer.listView1.Select();
                        m_objViewer.listView1.Focus();
                        //���listView
                    }
                    else
                    {
                        MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
                    }
                }
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            return ret;
        }

        public int m_mthFindMedicineByIDAcc(string strType, string ID)
        {
            int ret = 0;
            DataTable dt = null;
            string strpayID = "0001";
            if (this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim() != "")
            {
                strpayID = this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim();
            }
            long strRet = objSvc.m_mthFindMedicineByID(strType, ID, strpayID, out dt, this.m_objViewer.LoginInfo.m_strEmpID, this.IsChildPrice);
            if (strRet > 0 && dt.Rows.Count == 1)
            {
                ret = dt.Rows.Count;
                int row = m_objViewer.rowNO;
                DataRow dr = dt.Rows[0];
                if (dr["NOQTYFLAG_INT"].ToString().Trim() != "0" && m_mthIsMedicine(dr["ITEMOPINVTYPE_CHR"].ToString().Trim()))
                {
                    //if (isShowLackMedicine)
                    //{

                    //    if (MessageBox.Show("ȱҩ!�Ƿ����?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    //    {
                    //        return 0;
                    //    }
                    //}
                    //else
                    //{
                    MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
                    return 0;
                    //}

                }
                string strCurrItem = m_objViewer.ctlDataGrid1[row, 10].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 0] = dr["TempItemCode"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 2] = dr["ITEMNAME_VCHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString().Trim());
                m_objViewer.ctlDataGrid1[row, 4] = dr["ITEMSPEC_VCHR"].ToString().Trim();
                //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                if (m_mthIsMedicine(dr["ITEMOPINVTYPE_CHR"].ToString().Trim()) && dr["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dr["ITEMIPUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dr["SUBMONEY"].ToString().Trim();
                }
                else
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dr["ITEMOPUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dr["ITEMPRICE_MNY"].ToString().Trim();
                }
                m_objViewer.ctlDataGrid1[row, 8] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 9] = m_mthRelationInfo(dr["ITEMOPINVTYPE_CHR"].ToString().Trim());
                if (m_objViewer.ctlDataGrid1[row, 9].ToString() != "0001" && m_objViewer.ctlDataGrid1[row, 9].ToString() != "0002")//��ҩʱ��ȡ��һ��λ
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dr["Unit"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dr["ITEMPRICE_MNY"].ToString().Trim();
                }

                m_objViewer.ctlDataGrid1[row, 10] = dr["ITEMID_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 11] = dr["SELFDEFINE_INT"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 15] = dr["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 16] = 0;
                m_objViewer.ctlDataGrid1[row, 17] = "";
                m_objViewer.ctlDataGrid1[row, 18] = "";
                m_objViewer.ctlDataGrid1[row, 21] = dr["DOSAGEUNIT_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, "colYbcode"] = dr["insuranceid_chr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 36] = dr["opchargeflg_int"].ToString().Trim();
                string strPRECENT_DEC = "100";
                if (dr["PRECENT_DEC"].ToString().Trim() != "")
                {
                    strPRECENT_DEC = dr["PRECENT_DEC"].ToString().Trim();
                }
                m_objViewer.ctlDataGrid1[row, 13] = strPRECENT_DEC + "%";
                m_objViewer.ctlDataGrid1[row, 14] = this.m_mthConvertObjToDecimal(strPRECENT_DEC);

                if (m_objViewer.ctlDataGrid1[row, 12].ToString().Trim() != "")
                {
                    m_objViewer.IsSave = false;
                }
                m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, 1);
                m_objViewer.ctlDataGrid1[row, 0] = dr["TempItemCode"].ToString().Trim();
            }
            return ret;
        }

        /// <summary>
        /// ����IDת�����������
        /// </summary>
        /// <param name="strTypeNo"></param>
        /// <returns></returns>
        private string m_mthConvertToChType(string strTypeNo)
        {
            string strRet = "";
            {
                for (int i = 0; i < objResult.Length; i++)
                {
                    if (strTypeNo == objResult[i].m_strTypeID.Trim())
                    {
                        strRet = objResult[i].m_strTypeName;
                        break;
                    }
                }
            }
            //			switch(strTypeNo)
            //			{
            //				case "0002":
            //					strRet="��ҩ";
            //					break;
            //				case "0003":
            //					strRet="����";
            //					break;
            //				case "0004":
            //					strRet="����";
            //					break;
            //				case "0005":
            //					strRet="����";
            //					break;
            //				case "0006":
            //					strRet="����";
            //					break;
            //				default:
            //				strRet="��ҩ";
            //					break;
            //			}
            return strRet;
        }
        #endregion

        #region ������Ŀ����
        private clsChargeItemEXType_VO[] objResult = null;
        public void m_mthLoadCat()
        {
            clsDomainControl_ChargeItem clsDomain = new clsDomainControl_ChargeItem();
            long l = clsDomain.m_GetEXType("2", out objResult);
            if (l < 0)
            {
                MessageBox.Show("������Ŀ����ʧ��!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            l = objSvc.m_mthRelationInfo(out this.dt_RelationInfo);
            if (l < 0)
            {
                MessageBox.Show("���ع�ϵ��ʧ��!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ȡ������
        public void m_mthGetWindowName()
        {
            DataTable dtbWinName;
            objSvc.m_mthGetWindowName(out dtbWinName);
            DataRow[] dr = dtbWinName.Select("windowid_chr='" + m_objViewer.strEmergencyMedStoreTWindow + "'");
            if (dr != null && dr.Length > 0)
            {
                m_objViewer.strEmergencyTWinName = dr[0]["windowname_vchr"].ToString();
            }
            dr = null;
            dr = dtbWinName.Select("windowid_chr='" + m_objViewer.strEmergencyMedStoreSWindow + "'");
            if (dr != null && dr.Length > 0)
            {
                m_objViewer.strEmergencySWinName = dr[0]["windowname_vchr"].ToString();
            }
            dr = null;
            dr = dtbWinName.Select("windowid_chr='" + m_objViewer.strSpecialTWinName + "'");
            if (dr != null && dr.Length > 0)
            {
                m_objViewer.strSpecialTWinName = dr[0]["windowname_vchr"].ToString();
            }
            dr = null;
            dr = dtbWinName.Select("windowid_chr='" + m_objViewer.strSpecialSWinName + "'");
            if (dr != null && dr.Length > 0)
            {
                m_objViewer.strSpecialSWinName = dr[0]["windowname_vchr"].ToString();
            }
        }
        #endregion

        #region listview��˫���¼�

        #region FillRowData
        /// <summary>
        /// FillRowData
        /// </summary>
        /// <param name="dr"></param>
        bool FillRowData(DataRow dr, decimal qty)
        {
            try
            {
                int row = m_objViewer.rowNO;
                string strCurrItem = m_objViewer.ctlDataGrid1[row, 10].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 0] = dr["tempitemcode"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 1] = qty;
                m_objViewer.ctlDataGrid1[row, 2] = dr["itemname_vchr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dr["itemopinvtype_chr"].ToString().Trim());
                m_objViewer.ctlDataGrid1[row, 4] = dr["itemspec_vchr"].ToString().Trim();
                //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                string strTemp = m_mthRelationInfo(dr["itemopinvtype_chr"].ToString().Trim());
                if (strTemp != "0001" && strTemp != "0002" && strTemp != "0005")//��ҩʱȡ��һ����λ
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dr["unit"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dr["itemprice_mny"].ToString().Trim();
                }
                else
                {
                    if (/*m_mthIsMedicine(dr["itemopinvtype_chr"].ToString().Trim()) && */dr["opchargeflg_int"].ToString().Trim() == "1")
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dr["itemipunit_chr"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dr["submoney"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = dr["subtrademoney"].ToString().Trim();
                    }
                    else
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dr["itemopunit_chr"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dr["itemprice_mny"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = dr["tradeprice_mny"].ToString().Trim();
                    }
                }
                m_objViewer.ctlDataGrid1[row, 8] = dr["itemopinvtype_chr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 9] = strTemp; // m_mthRelationInfo(dt["itemopinvtype_chr"].ToString().Trim());
                //if (m_objViewer.ctlDataGrid1[row, 9].ToString() != "0001" && m_objViewer.ctlDataGrid1[row, 9].ToString() != "0002")//��ҩʱ��ȡ��һ��λ
                //{
                //    m_objViewer.ctlDataGrid1[row, 5] = dt["unit"].ToString().Trim();
                //    m_objViewer.ctlDataGrid1[row, 6] = dt["itemprice_mny"].ToString().Trim();
                //}

                m_objViewer.ctlDataGrid1[row, 10] = dr["itemid_chr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 11] = dr["selfdefine_int"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 15] = dr["itemopcalctype_chr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 16] = 0;
                m_objViewer.ctlDataGrid1[row, 17] = "";
                m_objViewer.ctlDataGrid1[row, 18] = "";
                m_objViewer.ctlDataGrid1[row, 21] = dr["dosageunit_chr"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 36] = dr["opchargeflg_int"].ToString().Trim();
                string strPRECENT_DEC = "100";
                if (dr["precent_dec"].ToString().Trim() != "")
                {
                    strPRECENT_DEC = dr["precent_dec"].ToString().Trim();
                }
                m_objViewer.ctlDataGrid1[row, 13] = strPRECENT_DEC + "%";
                m_objViewer.ctlDataGrid1[row, 14] = strPRECENT_DEC;
                m_objViewer.ctlDataGrid1[row, "colYbcode"] = dr["insuranceid_chr"].ToString().Trim();

                //0040 �ж��Ƿ����������Ŀ 0-������ 1-��
                if (this.objSvc.m_mthIsCanDo("0040") == 1)
                {
                    string strItemID = dr["itemid_chr"].ToString().Trim();
                    //�Զ��������������շ���Ŀ			
                    if (strCurrItem != strItemID)
                    {
                        string strpayID = "0001";
                        if (this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim() != "")
                        {
                            strpayID = this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim();
                        }

                        string strReItemID = m_objViewer.ctlDataGrid1[row, ResubitemCol].ToString().Trim();
                        DataTable dtRecord = new DataTable();
                        bool blnStat = objSvc.m_blnCheckSubChargeItem(strpayID, strItemID, out dtRecord, this.IsChildPrice);

                        if (strReItemID.StartsWith("[PK]"))
                        {
                            m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                        }

                        m_objViewer.ctlDataGrid1[row, ResubitemCol] = "";
                        m_objViewer.ctlDataGrid1[row, ResubnumsCol] = 0;

                        if (blnStat)
                        {
                            m_mthGetChargeItemByItem(row.ToString() + "->" + strItemID, 0, dtRecord);
                            m_objViewer.ctlDataGrid1[row, ResubitemCol] = "[PK]" + row.ToString() + "->" + strItemID;
                            m_objViewer.ctlDataGrid1[row, ResubnumsCol] = m_objViewer.ctlDataGrid1[row, 1];
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        public void m_mthListViewDoubleClick()
        {
            if (m_objViewer.listView1.SelectedItems.Count > 0 || m_objViewer.listView1.SelectedItems[0].ForeColor != Color.Red)
            {
                if (m_objViewer.listView1.SelectedItems[0].Tag == null)
                {
                    return;
                }
                int row = m_objViewer.rowNO;
                DataRow dr = (DataRow)this.m_objViewer.listView1.SelectedItems[0].Tag;
                this.FillRowData(dr, 1);

                m_objViewer.listView1.Height = 0;
                if (m_objViewer.ctlDataGrid1[row, 12].ToString().Trim() != "")
                {
                    m_objViewer.IsSave = false;
                }
                m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, 1);
            }
        }
        #endregion

        #region �����ı�
        /// <summary>
        /// �����ı�
        /// </summary>
        /// <param name="strChargeID"></param>
        /// <param name="decPrice"></param>
        /// <param name="ChargeTypeID"></param>
        /// <param name="decDosage"></param>
        /// <param name="rowNo"></param>
        public void m_mthDosageChange(string strChargeID, string decPrice, string ChargeTypeID, string decDosage, int rowNo)
        {
            if (this.objCalPatientCharge == null)
            {
                MessageBox.Show("�������벡������!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BathCalc)
            {
                return;
            }

            decimal price = m_mthConvertObjToDecimal(decPrice.Trim()), decTradePrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[rowNo, intDiffUnitPriceCol].ToString().Trim());
            //if (intDiffPriceOn == 1)// ������
            //    price = decTradePrice;
            decimal dosage = m_mthConvertObjToDecimal(decDosage.Trim());
            int row = 2000;

            if (m_objViewer.ctlDataGrid1[rowNo, 12] != null && m_objViewer.ctlDataGrid1[rowNo, 12].ToString().Trim() != "")
            {
                row = int.Parse(m_objViewer.ctlDataGrid1[rowNo, 12].ToString().Trim());
            }

            decimal discount = 100;
            if (m_objViewer.ctlDataGrid1[rowNo, 14] != null && m_objViewer.ctlDataGrid1[rowNo, 14].ToString().Trim() != "")
            {
                discount = Convert.ToDecimal(m_objViewer.ctlDataGrid1[rowNo, 14].ToString().Trim());
            }

            m_objViewer.ctlDataGrid1[rowNo, 1] = dosage;
            if (m_objViewer.ctlDataGrid1[rowNo, 8].ToString().Trim() == objCalPatientCharge.InvoiceCatID)
            {
                dosage = dosage * m_objViewer.numericUpDown1.Value;
                dosage = Math.Ceiling(dosage);
            }

            decimal temp = price * dosage, decDiffPriceTotal = (decTradePrice - price) * dosage;// ���������
            m_objViewer.ctlDataGrid1[rowNo, 7] = Function.Round(temp, 2);
            if (blMedicine9003(m_objViewer.ctlDataGrid1[rowNo, 10].ToString()))// �ж��Ƿ���ҩƷ  m_mthIsMedicine((m_objViewer.ctlDataGrid1[rowNo, 8].ToString().Trim()))
                m_objViewer.ctlDataGrid1[rowNo, intDiffPriceTotalCol] = Function.Round(decDiffPriceTotal, 2);
            else
            {
                decTradePrice = price;
                m_objViewer.ctlDataGrid1[rowNo, intDiffPriceTotalCol] = 0;
            }

            string strcat = m_objViewer.ctlDataGrid1[rowNo, 15].ToString().Trim();
            if (this.intDiffPriceOn == 1)
                m_objViewer.ctlDataGrid1[rowNo, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(strChargeID, price, decTradePrice, ChargeTypeID, dosage, row, discount, strcat, true);
            else
                m_objViewer.ctlDataGrid1[rowNo, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(strChargeID, price, ChargeTypeID, dosage, row, discount, strcat, false);
            this.m_mthDisplayCharge();

            if (this.m_objViewer.ctlDataGrid1.CurrentCell.ColumnNumber == 6)
            {
                this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(rowNo, 7);
                this.m_objViewer.ctlDataGrid1[rowNo, 6] = decPrice;
            }
            else
            {
                if (this.m_objViewer.IsSendTabKey)
                {
                    SendKeys.SendWait("{Tab}");
                    if (this.m_objViewer.ctlDataGrid1[rowNo + 1, 0].ToString().Trim() != "")
                    {
                        SendKeys.SendWait("{Tab}");
                    }
                }
            }

            if (!Recsumflag)
            {
                this.m_mthIsOverFlow();
            }

            this.m_objViewer.ctlDataGrid1.Focus();
        }
        /// <summary>
        /// ��ʾ������ϸ
        /// </summary>
        private void m_mthDisplayCharge()
        {
            this.objCalPatientCharge.m_mthDisplayCharge();
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ֻ���洦����Ϣ
        /// </summary>
        public void m_mthSaveRecipeOnly()
        {
            #region �жϱ�������
            for (int i = m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                if (m_objViewer.ctlDataGrid1.RowCount == 1 && m_objViewer.ctlDataGrid1[0, 10].ToString().Trim() == "")
                {
                    break;
                }
                if (m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() == "")
                {
                    m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                }

            }
            if (this.m_objViewer.btSave.Tag != null)
            {
                MessageBox.Show("����ʷ�������豣��!");
                return;
            }
            else
            {
                if (!this.IsChargeReceiverRec)
                {
                    MessageBox.Show("����ϵͳ��ǰ����Ϊ���ܽ����տ�Ա����������Ŀ�����Բ��ܱ��浱ǰ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            clsRecipeDetail_VO[] objRD_VO = new clsRecipeDetail_VO[m_objViewer.ctlDataGrid1.RowCount];
            int intLocation = 0;
            for (int i = 0; i < m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                objRD_VO[intLocation] = new clsRecipeDetail_VO();
                if (m_objViewer.ctlDataGrid1[i, 10] == null || m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() == "")
                {
                    continue;
                }
                if (m_objViewer.ctlDataGrid1[i, 1] == null || m_objViewer.ctlDataGrid1[i, 1].ToString().Trim() == "")
                {
                    MessageBox.Show("����Ϊ�ղ��ܱ���!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.m_objViewer.ctlDataGrid1.Select();
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(i, 1);
                    return;
                }
                objRD_VO[intLocation].decDiscount = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 14]);
                objRD_VO[intLocation].strRowNO = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 16]).ToString();
                objRD_VO[intLocation].decPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 6]);
                objRD_VO[intLocation].decQuantity = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 1]);
                objRD_VO[intLocation].decSumMoney = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 7]);
                objRD_VO[intLocation].strCharegeItemID = m_objViewer.ctlDataGrid1[intLocation, 10].ToString().Trim();
                objRD_VO[intLocation].strUsageID = m_objViewer.ctlDataGrid1[intLocation, 17].ToString().Trim();
                objRD_VO[intLocation].strFrequencyID = m_objViewer.ctlDataGrid1[intLocation, 18].ToString().Trim();
                objRD_VO[intLocation].strDosage = m_objViewer.ctlDataGrid1[intLocation, 19].ToString().Trim();
                objRD_VO[intLocation].strDays = m_objViewer.ctlDataGrid1[intLocation, 20].ToString().Trim();
                objRD_VO[intLocation].strType = m_objViewer.ctlDataGrid1[intLocation, 9].ToString().Trim();
                objRD_VO[intLocation].strUint = m_objViewer.ctlDataGrid1[intLocation, 5].ToString().Trim();
                objRD_VO[intLocation].strApplyID = m_objViewer.ctlDataGrid1[intLocation, 22].ToString().Trim();
                objRD_VO[intLocation].strCMedicineUsage = m_objViewer.numericUpDown1.Tag.ToString();

                /***����***/
                objRD_VO[intLocation].strHYPETEST_INT = m_objViewer.ctlDataGrid1[intLocation, 24].ToString().Trim();
                objRD_VO[intLocation].strDESC_VCHR = m_objViewer.ctlDataGrid1[intLocation, 25].ToString().Trim();
                objRD_VO[intLocation].m_strOutpatRecipeID = m_objViewer.ctlDataGrid1[intLocation, 26].ToString().Trim();
                objRD_VO[intLocation].m_strItemspec = m_objViewer.ctlDataGrid1[intLocation, 4].ToString().Trim();
                objRD_VO[intLocation].m_strDosageunit = m_objViewer.ctlDataGrid1[intLocation, 21].ToString().Trim();
                objRD_VO[intLocation].m_strATTACHPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, ResubitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decAttachitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, ResubnumsCol]);
                objRD_VO[intLocation].m_strUSAGEPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, UsageitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decUsageitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, UsagenumsCol]);
                objRD_VO[intLocation].m_strItemname = m_objViewer.ctlDataGrid1[intLocation, 2].ToString().Trim();
                objRD_VO[intLocation].m_intDeptmed = Function.Int(m_objViewer.ctlDataGrid1[intLocation, Deptmed].ToString());
                objRD_VO[intLocation].m_strOrderID = m_objViewer.ctlDataGrid1[intLocation, OrderID].ToString().Trim();
                objRD_VO[intLocation].m_decOrderBaseNum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, OrderNum]);
                /******/

                intLocation++;
            }

            if (objRD_VO.Length == 0)
            {
                return;
            }
            if (objRD_VO[0].strCharegeItemID == null || objRD_VO[0].strCharegeItemID.Trim() == "")//��һ�е���ĿIDΪ�ձ�ʾû���շ���Ŀ
            {
                return;
            }
            #endregion

            #region �ռ���������Ϣ
            clsOutPatientRecipe_VO OPR_VO = new clsOutPatientRecipe_VO();
            OPR_VO.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strEmployeeID = "0000001";
            if (m_objViewer.LoginInfo != null)
            {
                strEmployeeID = m_objViewer.LoginInfo.m_strEmpID;
            }
            //			OPR_VO.m_strOperatorID=m_objViewer.LoginInfo.m_strEmpNo;
            OPR_VO.m_strOperatorID = strEmployeeID;//����ԱID,��δ����Ȩ�޹���֮ǰ,�ȶ���Ϊ"0001"
            OPR_VO.m_strRegisterID = m_objViewer.m_PatientBasicInfo.RegisterID;
            OPR_VO.m_strDoctorID = m_objViewer.m_PatientBasicInfo.DoctorID;
            OPR_VO.m_strDepID = m_objViewer.m_PatientBasicInfo.DeptID;
            OPR_VO.m_strCreateDate = m_objViewer.m_PatientBasicInfo.RegisterDate;
            OPR_VO.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID;
            OPR_VO.m_intPStatus = 1;//�½�
            OPR_VO.m_intType = int.Parse(m_objViewer.m_cmbRecipeType.Tag.ToString());
            OPR_VO.m_strPatientType = m_objViewer.m_PatientBasicInfo.PayTypeID;
            OPR_VO.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
            //if (OPR_VO.strIDcard == "" || (OPR_VO.strIDcard.Length != 15 && OPR_VO.strIDcard.Length != 18))
            //{
            //    MessageBox.Show("���֤�Ų���Ϊ�ղ���λ��������15λ��18λ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    this.m_objViewer.txtIDcard.Focus();
            //    return;
            //}  
            OPR_VO.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
            if (this.m_objViewer.btSave.Tag != null)
            {
                OPR_VO.m_strOutpatRecipeID = this.m_objViewer.btSave.Tag.ToString();
            }
            else
            {
                OPR_VO.m_strOutpatRecipeID = "";
            }
            #endregion

            string strRecipeID;
            long strRet = objSvc.m_mthAddRecipeMain(OPR_VO, out strRecipeID);
            if (strRet <= 0)
            {
                MessageBox.Show("�Բ���,������Ϣʧ�ܡ�", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("����ɹ���", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.m_mthSaveRecipeDetial(strRecipeID, objRD_VO);
            this.m_mthCreatCalObj();
            this.m_objViewer.txtLoadRecipeNO.m_mthClearText();
            this.m_objViewer.m_PatientBasicInfo.Clear();
            this.m_objViewer.txtIDcard.Text = "";
            this.m_objViewer.txtInsuranceID.Text = "";
            this.m_objViewer.cboProxyBoilMed.SelectedIndex = 0;
            this.m_mthCreatCalObj();
            this.m_objViewer.m_PatientBasicInfo.txtCardID.Focus();
        }
        #region ����ҩƷ���ͱ�
        /// <summary>
        /// ����ҩƷ���ͱ�
        /// </summary>
        /// <returns></returns>
        public long m_mthSaveMedicineSend()
        {
            long lngRes = 0;
            try
            {
                if (m_objMainRecipeList.Count > 0)
                    lngRes = objSvc.m_mthSaveMedicineSend(ref m_objMainRecipeList, ref m_objWMSendList, ref m_objCMSendList);
                if (lngRes > 0)
                {
                    m_objMainRecipeList.Clear();
                    m_objWMSendList.Clear();
                    m_objCMSendList.Clear();
                }
            }
            catch
            {
                m_objMainRecipeList.Clear();
                m_objWMSendList.Clear();
                m_objCMSendList.Clear();
            }
            return lngRes;

        }
        #endregion
        /// <summary>
        /// ���洦��
        /// </summary>
        public long m_mthSaveRecipe(com.digitalwave.iCare.middletier.HI.clsPatientChargeCal[] p_objPC)
        {
            bool A = false;
            bool B = false;
            string m_strSerNO = string.Empty;
            List<clsInvoiceTypeDetail_VO>[] objArr1 = new List<clsInvoiceTypeDetail_VO>[p_objPC.Length];
            clsInvoice_VO[] objInvoice_VO = new clsInvoice_VO[p_objPC.Length];
            List<clsInvoiceTypeDetail_VO>[] objArr2 = new List<clsInvoiceTypeDetail_VO>[p_objPC.Length];
            List<clsMedrecipesend_VO> objMedicineSend = new List<clsMedrecipesend_VO>();
            //��������Ϣ
            List<clsOutPatientRecipe_VO> objMainRecipeArr = new List<clsOutPatientRecipe_VO>();
            //��������
            ArrayList objRecTypeArr = new ArrayList();
            //ҩ��ID
            ArrayList objMedStoreArr = new ArrayList();
            //��ҩ����ID
            ArrayList objMedWinArr = new ArrayList();
            System.Collections.Generic.Dictionary<string, clsMedStoreWindowsVo> m_gdicSpecialMedStore = new System.Collections.Generic.Dictionary<string, clsMedStoreWindowsVo>(5);// -- �̶� 5 ����
            //�����
            ArrayList objSortnoArr = new ArrayList();
            ArrayList objMedStoreType = new ArrayList();
            string pid = this.m_objViewer.m_PatientBasicInfo.PatientID;
            string docdeptid = this.m_objViewer.m_PatientBasicInfo.DeptID;
            clsMedStoreWindowsVo currentMedStoreWindow = null;
            System.Collections.Generic.List<clsMedStoreWindowsVo> m_glstMedStoreVo = new System.Collections.Generic.List<clsMedStoreWindowsVo>();
            string strMainRecipeID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            if (this.objHashTable.Count > 0)//�Ѵ�����ŵ�һ������ID
            {
                strMainRecipeID = ((clsOutPatientRecipe_VO)objHashTable[0]).m_strOutpatRecipeID;

                // �ٴ�У�鴦���Ƿ��ѽɷ�
                clsDcl_OPCharge dclCharge = new clsDcl_OPCharge();
                if (dclCharge.CheckRecipeIsCharge(strMainRecipeID))
                {
                    MessageBox.Show("�ô����Ѿ��շѣ������ظ��շѡ�", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                dclCharge = null;
            }

            #region �жϱ�������
            for (int i = m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                if (m_objViewer.ctlDataGrid1.RowCount == 1 && m_objViewer.ctlDataGrid1[0, 10].ToString().Trim() == "")
                {
                    break;
                }
                if (m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() == "")
                {
                    m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                    continue;
                }
                if (m_objViewer.ctlDataGrid1[i, 26].ToString().Trim() == "")//�������Ϊ�������
                {
                    m_objViewer.ctlDataGrid1[i, 26] = strMainRecipeID;
                }
            }

            //ͬһ��ҩ��ҩ�������
            bool mb = this.m_blnCheckmedproperty();

            //��ϸ��Ϣ
            clsRecipeDetail_VO[] objRD_VO = new clsRecipeDetail_VO[m_objViewer.ctlDataGrid1.RowCount];
            int intLocation = 0;
            string strChrgItem = "";
            string strMedStoreID = "", strMedStoreIDCurr = "";
            string strMedStoretype = "1";
            string strMedWinID = "";
            int intSortno = 1;
            Hashtable has = new Hashtable();
            for (int i = 0; i < m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                objRD_VO[intLocation] = new clsRecipeDetail_VO();

                if (m_objViewer.ctlDataGrid1[i, 1] == null || m_objViewer.ctlDataGrid1[i, 1].ToString().Trim() == "")
                {
                    MessageBox.Show("����Ϊ�ղ��ܱ���!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.m_objViewer.ctlDataGrid1.Select();
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(i, 1);
                    return -1;
                }
                //���շѣ���ĿID
                strChrgItem = m_objViewer.ctlDataGrid1[intLocation, 10].ToString().Trim();
                objRD_VO[intLocation].strRowNO = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 16]).ToString();
                objRD_VO[intLocation].decDiscount = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 14]);
                objRD_VO[intLocation].decPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 6]);
                objRD_VO[intLocation].decQuantity = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 1]);
                objRD_VO[intLocation].decSumMoney = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 7]);
                objRD_VO[intLocation].strCharegeItemID = strChrgItem;
                objRD_VO[intLocation].strUsageID = m_objViewer.ctlDataGrid1[intLocation, 17].ToString().Trim();
                objRD_VO[intLocation].strFrequencyID = m_objViewer.ctlDataGrid1[intLocation, 18].ToString().Trim();
                objRD_VO[intLocation].strDosage = m_objViewer.ctlDataGrid1[intLocation, 19].ToString().Trim();
                objRD_VO[intLocation].strDays = m_objViewer.ctlDataGrid1[intLocation, 20].ToString().Trim();
                objRD_VO[intLocation].strType = m_objViewer.ctlDataGrid1[intLocation, 9].ToString().Trim();
                objRD_VO[intLocation].strApplyID = m_objViewer.ctlDataGrid1[intLocation, 22].ToString().Trim();
                objRD_VO[intLocation].strCMedicineUsage = m_objViewer.numericUpDown1.Tag.ToString();
                objRD_VO[intLocation].strHYPETEST_INT = m_objViewer.ctlDataGrid1[intLocation, 24].ToString().Trim();
                objRD_VO[intLocation].strDESC_VCHR = m_objViewer.ctlDataGrid1[intLocation, 25].ToString().Trim();
                objRD_VO[intLocation].m_strOutpatRecipeID = m_objViewer.ctlDataGrid1[intLocation, 26].ToString().Trim();
                objRD_VO[intLocation].m_strItemspec = m_objViewer.ctlDataGrid1[intLocation, 4].ToString().Trim();
                objRD_VO[intLocation].m_decDosage = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 19]);
                objRD_VO[intLocation].m_strDosageunit = m_objViewer.ctlDataGrid1[intLocation, 21].ToString().Trim();
                objRD_VO[intLocation].m_strATTACHPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, ResubitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decAttachitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, ResubnumsCol]);
                objRD_VO[intLocation].m_strUSAGEPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, UsageitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decUsageitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, UsagenumsCol]);
                objRD_VO[intLocation].m_strItemname = m_objViewer.ctlDataGrid1[intLocation, 2].ToString().Trim();
                objRD_VO[intLocation].strUint = m_objViewer.ctlDataGrid1[intLocation, 5].ToString().Trim();
                objRD_VO[intLocation].m_intDeptmed = Function.Int(m_objViewer.ctlDataGrid1[intLocation, Deptmed].ToString());
                objRD_VO[intLocation].m_strUnitFlag = m_objViewer.ctlDataGrid1[intLocation, 36].ToString().Trim();
                objRD_VO[intLocation].m_strOrderID = m_objViewer.ctlDataGrid1[intLocation, OrderID].ToString().Trim();
                objRD_VO[intLocation].m_decOrderBaseNum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, OrderNum]);
                // ���������
                if (this.intDiffPriceOn == 1)
                    objRD_VO[intLocation].m_decTolDiffPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, intDiffPriceTotalCol]);

                if (objRD_VO[intLocation].m_decTolDiffPrice == 0)
                    objRD_VO[intLocation].BuyPrice = objRD_VO[intLocation].decPrice;
                else
                    objRD_VO[intLocation].BuyPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, intDiffUnitPriceCol]);

                #region ���ɷ�ҩ����
                // 2019-11-15 ����ʹ�ÿƱ�ҩ
                if (objRD_VO[intLocation].m_intDeptmed == 1)
                    objRD_VO[intLocation].m_intDeptmed = 0;
                if (objRD_VO[intLocation].m_intDeptmed == 1)
                {
                    strMedStoreIDCurr = "";
                    if (!has.ContainsKey(1))
                    {
                        has.Add(1, 1);
                    }
                }
                else
                {
                    if (!has.ContainsKey(0))
                    {
                        has.Add(0, 0);
                    }
                    //��ȡҩ������
                    strMedStoretype = objSvc.m_strGetOutSendMedStoretype(strChrgItem);

                    if (strMedStoretype.Trim() == "")
                    {
                        string UsageID = objRD_VO[intLocation].strUsageID;
                        if (UsageID != "")
                        {
                            if (WMUsageIDArr.IndexOf(UsageID) >= 0)
                            {
                                strMedStoretype = "1";//��ҩ��
                            }
                            else if (CMUsageIDArr.IndexOf(UsageID) >= 0)
                            {
                                strMedStoretype = "2";//��ҩ��
                            }
                            else if (MATUsageIDArr.IndexOf(UsageID) >= 0)
                            {
                                strMedStoretype = "3";
                            }
                        }
                    }
                    else
                    {
                        if (strMedStoretype.Trim() != "" && mb)
                        {
                            //��ҩ��
                            strMedStoretype = "2";
                        }
                    }

                    switch (strMedStoretype)
                    {
                        case "1":
                            if (m_intIsOrNotSendMed == 1)
                            {
                                strMedStoreID = "";
                            }
                            else
                            {
                                strMedStoreID = this.m_strReadXML("register", "WMedicinestore", "AnyOne");//��ҩ��ID
                            }
                            break;
                        case "2":
                            if (m_intIsOrNotSendMed == 1)
                            {
                                strMedStoreID = "";
                            }
                            else
                            {
                                strMedStoreID = this.m_strReadXML("register", "CMedicinestore", "AnyOne");//��ҩ��ID
                            }
                            break;
                        case "3":
                            if (m_intIsOrNotSendMed == 1)
                            {
                                strMedStoreID = "";
                            }
                            else
                            {
                                if (m_intFlag == 1)
                                {
                                    strMedStoreID = this.m_strReadXML("register", "MaterialStore", "AnyOne");
                                }
                                else
                                {
                                    strMedStoreID = "";
                                }
                            }
                            break;
                        case "4": /* ����ҩ���� -> ��ҩ���� */
                            if (m_intIsOrNotSendMed == 1)
                            {
                                strMedStoreID = "";
                            }
                            else
                            {
                                strMedStoreID = this.m_strReadXML("register", "WMedicinestore", "AnyOne");
                            }
                            break;
                        default:
                            strMedStoreID = "";
                            break;
                    }

                    //���strMedStoreIDCurr���Է��ϴ�ѭ�����µ�ҩ��ID����ɴ���������ϸ(�����Һŷѵȵ�)����ͬһ��ҩ����
                    strMedStoreIDCurr = "";

                    #region �°����ͨҩ��ת����ҩ������������ͨҩ���ͼ���ҩ���ϲ�������תҩ��
                    if (m_objViewer.IsDetachWMedStore == 1)
                    {
                        long ret = objSvc.m_lngGetWorkStorage(strMedStoreID, out strMedStoreIDCurr);
                    }
                    #endregion

                    if (strMedStoreIDCurr.Trim() == "")
                    {
                        strMedStoreIDCurr = strMedStoreID;
                    }
                }
                if (strMedStoreIDCurr != "")
                {
                    bool blntmp = false;
                    //(�Ƿ�ϲ��š�����ҩ��������ֿ������߾�����) || (��ҩҲ�߾�����)
                    if (m_objViewer.IsDetachWMedStore > 0 || strMedStoretype == "2" || strMedStoretype == "3")
                    {
                        //��ȡ����û�����ͬҩ������ yunjie.xie
                        //strMedWinID = this.m_strGetwinid(pid, strMedStoreIDCurr, out intSortno);
                        currentMedStoreWindow = null;
                        if (m_gdicSpecialMedStore.ContainsKey(strMedStoreIDCurr))
                        {
                            currentMedStoreWindow = m_gdicSpecialMedStore[strMedStoreIDCurr];
                        }
                        else
                        {
                            long l1 = this.objSvc.m_lngGetsendmedinfoBypid(pid, strMedStoreIDCurr, out currentMedStoreWindow);
                            m_gdicSpecialMedStore.Add(strMedStoreIDCurr, currentMedStoreWindow);
                        }

                        if (currentMedStoreWindow != null)
                        {
                            strMedWinID = currentMedStoreWindow.m_strWindowID;
                            intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                        }

                        if (strMedWinID == "")
                        {
                            //ͬҩ����ҩ������ͬһ����
                            for (int k = 0; k < objMedStoreArr.Count; k++)
                            {
                                if (objMedStoreArr[k].ToString() == strMedStoreIDCurr)
                                {
                                    strMedWinID = objMedWinArr[k].ToString().Trim();
                                    blntmp = true;
                                    break;
                                }
                            }

                            if (!blntmp)
                            {
                                //ר�Ŵ�������
                                //objSvc.m_lngGetespecialwin(docdeptid, strMedStoreIDCurr, out strMedWinID, out intSortno); 
                                objSvc.m_lngGetespecialwinNew(docdeptid, strMedStoreIDCurr, out currentMedStoreWindow);

                                if (currentMedStoreWindow != null)
                                {
                                    strMedWinID = currentMedStoreWindow.m_strWindowID;
                                    intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                                }
                                if (strMedWinID == "")
                                {
                                    //�ŶӴ���
                                    //objSvc.lngGetWindowIDByStorage(strMedStoreIDCurr, out strMedWinID, out intSortno, Ismedwinpublic);
                                    objSvc.lngGetWindowIDByStorage(strMedStoreIDCurr, out currentMedStoreWindow);
                                    if (currentMedStoreWindow != null)
                                    {
                                        strMedWinID = currentMedStoreWindow.m_strWindowID;
                                        intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                                    }
                                }
                            }
                        }

                        #region �жϴ��ڷ����Ƿ�ɹ����������Ƿ�Ҫ�����ڼ���������(�ϲ��š�����ҩ������󣬴˴��Ĵ������º���)
                        //if (strMedWinID == null || strMedWinID.Trim() == "")
                        //{
                        //    //strMedWinID = " ";

                        //    MessageBox.Show("�䷢ҩ���ڷ���ʧ�ܣ������²�����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        //    //com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        //    //objLogger.LogError("���䷢ҩ����ʧ�� " + strMainRecipeID);
                        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(@"d:\code\medstorewin.log", true);
                        //    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  RecipeID = '" + strMainRecipeID
                        //                 + "'  ItemID = '" + strChrgItem + "' \r\n");
                        //    sw.Close();
                        //    sw.Dispose();

                        //    return -1;
                        //}

                        //for (int l = 0; l < objMedStoreArr.Count; l++)
                        //{
                        //    if (objMedStoreArr[l].ToString() == strMedStoreIDCurr && objMedWinArr[l].ToString() == strMedWinID)
                        //    {
                        //        blntmp = true;
                        //        break;
                        //    }
                        //}

                        //if (!blntmp)
                        //{
                        //    objRecTypeArr.Add(objRD_VO[intLocation].strType.ToString());
                        //    objMedStoreArr.Add(strMedStoreIDCurr);
                        //    objMedWinArr.Add(strMedWinID);
                        //    objSortnoArr.Add(intSortno);
                        //    objMedStoreType.Add(strMedStoretype);

                        //    m_glstMedStoreVo.Add(currentMedStoreWindow);
                        //}
                        //objRD_VO[intLocation].strMedstroeID = strMedStoreIDCurr;
                        //objRD_VO[intLocation].strWindowsID = strMedWinID;
                        //strMedWinID = "";
                        #endregion
                    }
                    else//�ϲ�ҩ��(��ҩ)
                    {
                        //ͬҩ����ҩ������ͬһ����
                        for (int k = 0; k < objMedStoreArr.Count; k++)
                        {
                            if (objMedStoreArr[k].ToString() == strMedStoreIDCurr)
                            {
                                strMedWinID = objMedWinArr[k].ToString().Trim();
                                blntmp = true;
                                break;
                            }
                        }

                        if (!blntmp)
                        {
                            //�����շѴ��ڲ�����������ҩ����ҩ
                            if (m_objViewer.intChargeWindowType == 1 && strMedStoreIDCurr == "0001")
                            {
                                currentMedStoreWindow = new clsMedStoreWindowsVo();
                                currentMedStoreWindow.m_strWindowID = m_objViewer.strEmergencyMedStoreTWindow;
                                currentMedStoreWindow.m_strWindowName = m_objViewer.strEmergencyTWinName;
                                currentMedStoreWindow.m_strSendWindowID = m_objViewer.strEmergencyMedStoreSWindow;
                                currentMedStoreWindow.m_strSendWindowName = m_objViewer.strEmergencySWinName;
                                strMedWinID = currentMedStoreWindow.m_strWindowID;
                                intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                            }
                            else//��ͨ�շѴ���
                            {
                                //����Ʋ��˻��ض����䲡�˷���ר��(������������ҩ����ҩ)
                                if ((DateTime.Now.AddYears(-m_objViewer.intSpecialPatientAge).CompareTo(DateTime.Parse(m_objViewer.m_PatientBasicInfo.PatientBirth)) >= 0 || docdeptid == m_objViewer.strEmergencyDeptID) && strMedStoreIDCurr == "0001")
                                //if (Convert.ToInt32(m_objViewer.m_PatientBasicInfo.PatientAge) > m_objViewer.intSpecialPatientAge || docdeptid == m_objViewer.strEmergencyDeptID)//����ƻ����ض����䲡��
                                {
                                    currentMedStoreWindow = new clsMedStoreWindowsVo();
                                    currentMedStoreWindow.m_strWindowID = m_objViewer.strSpecialMedStoreTWindow;
                                    currentMedStoreWindow.m_strWindowName = m_objViewer.strSpecialTWinName;
                                    currentMedStoreWindow.m_strSendWindowID = m_objViewer.strSpecialMedStoreSWindow;
                                    currentMedStoreWindow.m_strSendWindowName = m_objViewer.strSpecialSWinName;
                                    strMedWinID = currentMedStoreWindow.m_strWindowID;
                                    intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                                }
                                else
                                {
                                    //��ͨ�շѴ�����ͨ���ˣ�������˵��췢��ҩ�ģ���ȡ����û�����ͬҩ������
                                    //strMedWinID = this.m_strGetwinid(pid, strMedStoreIDCurr, out intSortno);

                                    currentMedStoreWindow = null;
                                    if (m_gdicSpecialMedStore.ContainsKey(strMedStoreIDCurr))
                                    {
                                        currentMedStoreWindow = m_gdicSpecialMedStore[strMedStoreIDCurr];
                                    }
                                    else
                                    {
                                        long l1 = this.objSvc.m_lngGetsendgeneralmedinfoBypid(pid, strMedStoreIDCurr, out currentMedStoreWindow);
                                        m_gdicSpecialMedStore.Add(strMedStoreIDCurr, currentMedStoreWindow);
                                    }


                                    //�����ѷ���ҩ�Ĵ��ڣ������Ǽ��ﴰ��ר�������ܷ���֮ǰ�Ĵ���
                                    if (currentMedStoreWindow != null && currentMedStoreWindow.m_strSendWindowID != m_objViewer.strSpecialMedStoreSWindow && currentMedStoreWindow.m_strSendWindowID != m_objViewer.strEmergencyMedStoreSWindow)
                                    {
                                        strMedWinID = currentMedStoreWindow.m_strWindowID;
                                        intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                                    }

                                    //�����ȡ�����ѷ���ҩ�Ĵ��ڲ��ɹ�����ӵ�ǰ����ͬҩ���л�ȡ���Դﵽ����һ��
                                    if (strMedWinID == "")
                                    {
                                        for (int k = 0; k < objMedStoreArr.Count; k++)
                                        {
                                            if (objMedStoreArr[k].ToString() == strMedStoreIDCurr)
                                            {
                                                strMedWinID = objMedWinArr[k].ToString().Trim();
                                                break;
                                            }
                                        }
                                    }

                                    //�������û�з���ҩ(���߲�������)���򰴴��ڰ�����������
                                    //ע���ʱ�ķ��������ϵͳ����0057Ӱ�죬���ר�����Խ������п��ҵĴ���������ͨ�շѴ���ͨ���˵Ĵ����п��ܻᷢ��ר�����ﴰ�����ﴰҲ��ר����
                                    //objSvc.lngGetWindowIDByStorage(strMedStoreIDCurr, out strMedWinID, out intSortno, Ismedwinpublic);
                                    if (strMedWinID == "")
                                    {
                                        objSvc.lngGetWindowIDByStorage(strMedStoreIDCurr, out currentMedStoreWindow);
                                        if (currentMedStoreWindow != null)
                                        {
                                            strMedWinID = currentMedStoreWindow.m_strWindowID;
                                            intSortno = currentMedStoreWindow.m_intWindowOrderNo;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #region �жϴ��ڷ����Ƿ�ɹ����������Ƿ�Ҫ�����ڼ���������(�ϲ��š�����ҩ�����������Ĵ������´˴�)
                    if (strMedWinID == null || strMedWinID.Trim() == "")
                    {
                        //strMedWinID = " ";

                        MessageBox.Show("�䷢ҩ���ڷ���ʧ�ܣ������²�����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        //com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        //objLogger.LogError("���䷢ҩ����ʧ�� " + strMainRecipeID);
                        //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"d:\code\medstorewin.log", true);
                        //sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  RecipeID = '" + strMainRecipeID
                        //             + "'  ItemID = '" + strChrgItem + "' \r\n");
                        //sw.Close();
                        //sw.Dispose();

                        return -1;
                    }

                    for (int l = 0; l < objMedStoreArr.Count; l++)
                    {
                        if (objMedStoreArr[l].ToString() == strMedStoreIDCurr && objMedWinArr[l].ToString() == strMedWinID)
                        {
                            blntmp = true;
                            break;
                        }
                    }

                    if (!blntmp)
                    {
                        objRecTypeArr.Add(objRD_VO[intLocation].strType.ToString());
                        objMedStoreArr.Add(strMedStoreIDCurr);
                        objMedWinArr.Add(strMedWinID);
                        objSortnoArr.Add(intSortno);
                        objMedStoreType.Add(strMedStoretype);

                        m_glstMedStoreVo.Add(currentMedStoreWindow);
                    }
                    objRD_VO[intLocation].strMedstroeID = strMedStoreIDCurr;
                    objRD_VO[intLocation].strWindowsID = strMedWinID;
                    strMedWinID = "";
                    #endregion
                }
                else
                {
                    objRD_VO[intLocation].strMedstroeID = " ";
                    objRD_VO[intLocation].strWindowsID = " ";
                }
                #endregion

                intLocation++;
            }

            if (objRD_VO[0].strCharegeItemID == null || objRD_VO[0].strCharegeItemID.Trim() == "")//��һ�е���ĿIDΪ�ձ�ʾû���շ���Ŀ
            {
                return -1;
            }

            Hashtable hasMed = new Hashtable();
            ArrayList objList = new ArrayList();
            clsDcl_DoctorWorkstation objSvcDoct = new clsDcl_DoctorWorkstation();
            DataTable dtTemp;
            DataView dvMedStore = null;
            string WMDrugStoreID = string.Empty;
            string CMDrugStoreID = string.Empty;
            if (objSvcDoct.m_lngGetMedStore(out dtTemp) > 0)
            {
                dvMedStore = new DataView(dtTemp);
            }
            for (int intI = 0; intI < objRD_VO.Length; intI++)
            {
                if (m_intIsOrNotSendMed == 1)
                {
                    break;
                }
                if (objRD_VO[intI].strType == "0001" || objRD_VO[intI].strType == "0002")
                {
                    MedDeduct_VO objMedVO = new MedDeduct_VO();
                    dvMedStore.RowFilter = "medstoreid_chr = '" + objRD_VO[intI].strMedstroeID + "'";
                    if (dvMedStore.Count > 0)
                    {
                        WMDrugStoreID = dvMedStore[0]["deptid_chr"].ToString().Trim();
                    }
                    else
                    {
                        WMDrugStoreID = string.Empty;
                    }
                    objMedVO.strRecipeID = objRD_VO[intI].m_strOutpatRecipeID;
                    // objMedVO.intRowNO = int.Parse(objRD_VO[intI].strBillNO);
                    objMedVO.strItemID = objRD_VO[intI].strCharegeItemID;
                    objMedVO.strItemName = objRD_VO[intI].m_strItemname;
                    objMedVO.decAmount = objRD_VO[intI].decQuantity;
                    //if (objRD_VO[intI].UnitType == "1" && clsPublic.ConvertObjToDecimal(objRD_VO[intI].UnitScale) > 0)
                    //{
                    //    decMidPackQty = clsPublic.ConvertObjToDecimal(objRD_VO[intI].UnitScale);
                    //}
                    //else
                    //{
                    //    decMidPackQty = 0;
                    //}
                    objMedVO.intUnitFlag = int.Parse(string.IsNullOrEmpty(objRD_VO[intI].m_strUnitFlag) ? "0" : objRD_VO[intI].m_strUnitFlag);
                    objMedVO.decMidPackQty = 0;
                    objMedVO.intMedType = objRD_VO[intI].strType == "0001" ? 1 : 2;
                    objMedVO.strExecDeptID = WMDrugStoreID;
                    // objMedVO.strExecDeptID = objRD_VO[intI].strType == "0001" ? WMDrugStoreID:CMDrugStoreID;
                    //if (decMidPackQty > 0)
                    //{
                    //    objMedVO.decAmount = objMedVO.decAmount * decMidPackQty;
                    //    objMedVO.decMidPackQty = decMidPackQty;
                    //    objMedVO.intUnitFlag = 2;
                    //}

                    objList.Add(objMedVO);

                    if (hasMed.ContainsKey(objMedVO.strItemID))
                    {
                        hasMed[objMedVO.strItemID] = decimal.Parse(hasMed[objMedVO.strItemID].ToString()) + objMedVO.decAmount;
                    }
                    else
                    {
                        hasMed.Add(objMedVO.strItemID, objMedVO.decAmount);
                    }
                }
            }
            MedDeduct_VO[] objMed = new MedDeduct_VO[objList.Count];
            for (int intI = 0; intI < objList.Count; intI++)
            {
                objMed[intI] = (MedDeduct_VO)objList[intI];
            }
            string strExecDeptIDArr = string.Empty;
            string strItemIDArr = string.Empty;
            for (int i = 0; i < objMed.Length; i++)
            {
                strExecDeptIDArr += "'" + objMed[i].strExecDeptID + "',";
                strItemIDArr += "'" + objMed[i].strItemID + "',";
            }
            Hashtable hasCompleteID = new Hashtable();
            DataTable dtMed = new DataTable();
            long ret1 = 0;
            if (!string.IsNullOrEmpty(strExecDeptIDArr) && !string.IsNullOrEmpty(strItemIDArr))
            {
                strExecDeptIDArr = strExecDeptIDArr.Substring(0, strExecDeptIDArr.Length - 1);
                strItemIDArr = strItemIDArr.Substring(0, strItemIDArr.Length - 1);
                ret1 = objSvcDoct.m_lngGetTheoryAmountByMedID(strExecDeptIDArr, strItemIDArr, this.m_strDeductType, out dtMed);
                if (ret1 > 0 && dtMed.Rows.Count > 0)
                {
                    decimal decTotal = 0;
                    for (int j = 0; j < objMed.Length; j++)
                    {
                        DataRow[] drr = dtMed.Select("itemid_chr = '" + objMed[j].strItemID + "' and drugstoreid_chr = '" + objMed[j].strExecDeptID + "'");

                        //�ؼ������
                        decTotal = 0;
                        for (int k = 0; k < drr.Length; k++)
                        {
                            if (objMed[j].intUnitFlag == 0)
                            {
                                //decTotal += clsPublic.ConvertObjToDecimal(drr[k]["jbsl"].ToString());
                                decTotal += clsPublic.ConvertObjToDecimal(drr[k]["zxsl"].ToString()) / clsPublic.ConvertObjToDecimal(drr[k]["packqty_dec"].ToString());
                            }
                            else
                            {
                                decTotal += clsPublic.ConvertObjToDecimal(drr[k]["zxsl"].ToString());
                            }
                        }

                        if (!hasCompleteID.ContainsKey(objMed[j].strItemID))
                        {
                            if (decimal.Parse(hasMed[objMed[j].strItemID].ToString()) > decTotal)
                            {
                                if (this.m_blnSecondStockLimitFlag)
                                {
                                    MessageBox.Show("ҩƷ��" + objMed[j].strItemName + "��治�㣬����ϵҩ����(ҩ����ǰ���ÿ����: " + decTotal.ToString() + ")", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return -1;
                                }
                            }

                            hasCompleteID.Add(objMed[j].strItemID, objMed[j].strItemID);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("��ǰҩƷ�޿��򲻿ɹ����������Ա��ϵ", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }
            }
            #endregion

            #region �ռ���������Ϣ

            int deptmed = 0;
            if (!has.ContainsKey(0) && has.ContainsKey(1))
            {
                deptmed = 1;
            }

            if (this.objHashTable.Count > 0)
            {
                foreach (clsOutPatientRecipe_VO o in objHashTable.Values)
                {
                    o.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
                    o.m_strPatientType = this.m_objViewer.m_PatientBasicInfo.PayTypeID;
                    o.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
                    o.intDeptmed = deptmed;
                    o.m_intPStatus = 2;
                    if (o.m_strDoctorID == null || o.m_strDoctorID.Trim() == "")
                    {
                        o.m_strDoctorID = m_objViewer.m_PatientBasicInfo.DoctorID.Trim();
                    }
                    o.IsProxyBoilMed = this.m_objViewer.cboProxyBoilMed.SelectedIndex;
                    objMainRecipeArr.Add(o);
                    m_objMainRecipeList.Add(o);
                }
            }

            if (objMainRecipeArr.Count == 0)
            {
                clsOutPatientRecipe_VO OPR_VO = new clsOutPatientRecipe_VO();
                OPR_VO.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                OPR_VO.m_strOperatorID = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
                OPR_VO.m_strRegisterID = m_objViewer.m_PatientBasicInfo.RegisterID.Trim();
                OPR_VO.m_strDoctorID = m_objViewer.m_PatientBasicInfo.DoctorID.Trim();
                OPR_VO.m_strDepID = m_objViewer.m_PatientBasicInfo.DeptID.Trim();
                OPR_VO.m_strCreateDate = m_objViewer.m_PatientBasicInfo.RegisterDate;
                OPR_VO.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID.Trim();
                OPR_VO.m_intPStatus = 2;
                OPR_VO.m_intType = int.Parse(m_objViewer.m_cmbRecipeType.Tag.ToString());
                OPR_VO.m_strPatientType = m_objViewer.m_PatientBasicInfo.PayTypeID;
                OPR_VO.m_strRecipeType = "0";
                OPR_VO.intCreatetype = 1;
                OPR_VO.intDeptmed = deptmed;
                OPR_VO.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
                OPR_VO.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
                if (this.m_objViewer.btSave.Tag == null)
                {
                    OPR_VO.m_strOutpatRecipeID = strMainRecipeID;
                }
                else
                {
                    OPR_VO.m_strOutpatRecipeID = this.m_objViewer.btSave.Tag.ToString();
                }
                objMainRecipeArr.Add(OPR_VO);
                m_objMainRecipeList.Add(OPR_VO);

            }
            #endregion

            for (int i = 0; i < p_objPC.Length; i++)
            {
                if (i == 0)
                {
                    objArr2[0] = objCalPatientCharge.m_mthSaveInvoiceDetail2(p_objPC[0].m_strInvoiceNO, "");
                }
                else
                {
                    clsInvoiceTypeDetail_VO objDV = new clsInvoiceTypeDetail_VO();
                    objDV.m_strINVOICENO_VCHR = p_objPC[i].m_strInvoiceNO;
                    objDV.m_decSUM_MNY = 0;
                    objDV.m_strITEMCATID_CHR = ((clsInvoiceTypeDetail_VO)objArr2[0][0]).m_strITEMCATID_CHR;
                    objArr2[i] = new List<clsInvoiceTypeDetail_VO>();
                    objArr2[i].Add(objDV);
                }
                objArr1[i] = objCalPatientCharge.m_mthSaveInvoiceDetail(p_objPC[i].m_strInvoiceNO, p_objPC[i], "");
                objInvoice_VO[i] = new clsInvoice_VO();
                #region �ռ���Ʊ��Ϣ
                //if (IsDongGuanYBPatient)
                //{
                //    objInvoice_VO[i].m_decACCTSUM_MNY = 0;
                //    objInvoice_VO[i].m_decSBSUM_MNY = p_objPC[i].m_decTotalCost;
                //}
                //else
                //{
                objInvoice_VO[i].m_decACCTSUM_MNY = p_objPC[i].m_decChargeUpCost;
                objInvoice_VO[i].m_decSBSUM_MNY = p_objPC[i].m_decPersonCost;
                //}

                objInvoice_VO[i].m_intSTATUS_INT = 1;
                if (p_objPC[i].m_strDateOfReception != null && p_objPC[i].m_strDateOfReception.ToString().Trim() != "")
                {
                    objInvoice_VO[i].m_strINVDATE_DAT = p_objPC[i].m_strDateOfReception;
                }
                else
                {
                    objInvoice_VO[i].m_strINVDATE_DAT = m_objViewer.m_PatientBasicInfo.RegisterDate;
                }

                objInvoice_VO[i].m_strINVOICENO_VCHR = p_objPC[i].m_strInvoiceNO;
                objInvoice_VO[i].m_strOPREMP_CHR = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
                objInvoice_VO[i].m_strRECORDDATE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objInvoice_VO[i].m_strRECORDEMP_CHR = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
                objInvoice_VO[i].m_strSEQID_CHR = p_objPC[i].m_strSeriesNumber;
                objInvoice_VO[i].m_strBALANCEEMP_CHR = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
                objInvoice_VO[i].m_strBALANCE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objInvoice_VO[i].m_intBALANCEFLAG_INT = 0;
                objInvoice_VO[i].m_decTOTALSUM_MNY = p_objPC[i].m_decTotalCost;
                objInvoice_VO[i].m_intPAYTYPE_INT = int.Parse(p_objPC[i].m_strPayTypeIndex);
                objInvoice_VO[i].m_strPATIENTID_CHR = this.m_objViewer.m_PatientBasicInfo.PatientID;
                objInvoice_VO[i].m_strPATIENTNAME_CHR = this.m_objViewer.m_PatientBasicInfo.PatientName;
                objInvoice_VO[i].m_strDEPTID_CHR = this.m_objViewer.m_PatientBasicInfo.DeptID;
                objInvoice_VO[i].m_strDEPTNAME_CHR = this.m_objViewer.m_PatientBasicInfo.DeptName;
                objInvoice_VO[i].m_strDOCTORID_CHR = this.m_objViewer.m_PatientBasicInfo.DoctorID;
                objInvoice_VO[i].m_strDOCTORNAME_CHR = this.m_objViewer.m_PatientBasicInfo.DoctorName;
                objInvoice_VO[i].m_strCONFIRMEMP_CHR = this.m_objViewer.m_PatientBasicInfo.DoctorID; //���Ա�ݶ�Ϊ����ҽ��
                objInvoice_VO[i].m_strCONFIRMDEPT_CHR = this.m_objViewer.m_PatientBasicInfo.DeptID;  //���Ա�ݶ�Ϊ����ҽ�����ڵ�ǰ����
                objInvoice_VO[i].m_strPAYTYPEID_CHR = this.m_objViewer.m_PatientBasicInfo.PayTypeID;
                objInvoice_VO[i].m_strHospitalID_CHR = this.strHopitalID;
                objInvoice_VO[i].Paycardtype = p_objPC[i].Paycardtype;
                objInvoice_VO[i].Paycardno = p_objPC[i].Paycardno;
                objInvoice_VO[i].m_decTolDiffPrice = p_objPC[i].m_decTotalDiffCost;// ���������

                if (this.PEWorkStationFlag)
                {
                    if (this.PERegisterNoArr.Length >= 12)
                    {
                        objInvoice_VO[i].RegNo = this.PERegisterNoArr.Substring(0, 12);
                    }
                }
                else
                {
                    objInvoice_VO[i].RegNo = "";
                }
                #endregion
            }

            #region �ռ�ҩƷ���ͱ���Ϣ
            clsMedrecipesend_VO objMRS_VO;
            for (int i = 0; i < objMedStoreArr.Count; i++)
            {
                objMRS_VO = new clsMedrecipesend_VO();
                objMRS_VO.m_strOUTPATRECIPEID_CHR = "";
                objMRS_VO.m_intPSTATUS_INT = 1;
                objMRS_VO.m_strRECIPETYPE_INT = objRecTypeArr[i].ToString().Trim();
                objMRS_VO.m_strSENDDATE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objMRS_VO.m_strSENDEMP_CHR = m_objViewer.LoginInfo.m_strEmpID;
                objMRS_VO.m_strMedstroeID_CHR = objMedStoreArr[i].ToString();
                //objMRS_VO.m_strWINDOWID_CHR = objMedWinArr[i].ToString();
                //objMRS_VO.Sortno = objSortnoArr[i].ToString();

                if (m_glstMedStoreVo[i] != null)
                {
                    objMRS_VO.m_strWINDOWID_CHR = m_glstMedStoreVo[i].m_strWindowID;
                    objMRS_VO.Sortno = m_glstMedStoreVo[i].m_intWindowOrderNo.ToString();
                    objMRS_VO.m_strSendWINDOWID_CHR = m_glstMedStoreVo[i].m_strSendWindowID;
                    objMRS_VO.m_strSendWINDOWName_VCHR = m_glstMedStoreVo[i].m_strSendWindowName;
                }


                objMRS_VO.m_strTREATDATE_DAT = "";
                objMRS_VO.m_strTREATEMP_CHR = "";
                //objMedicineSend.Add(objMRS_VO);//�����Ϣ
                //if (objMRS_VO.m_strRECIPETYPE_INT == "0001" && (objMedStoreType[i].ToString() == "1" || objMedStoreType[i].ToString() == "4"))
                //{
                //    objSvc.m_mthGetSerNO(out m_strSerNO);
                //    objMRS_VO.m_strSerNO = m_strSerNO;
                //    m_objWMSendList.Add(objMRS_VO);
                //}
                //else
                //{
                //    m_objCMSendList.Add(objMRS_VO);
                //}


                if (objMRS_VO.m_strRECIPETYPE_INT == "0001" && (objMedStoreType[i].ToString() == "1" || objMedStoreType[i].ToString() == "4"))
                {
                    objMRS_VO.m_strFlag = "0";
                }
                else
                {
                    objMRS_VO.m_strFlag = "1";
                }
                objMedicineSend.Add(objMRS_VO);//�����Ϣ
            }

            #endregion
            //����һ��У��
            if (objRD_VO[0].m_strOutpatRecipeID != ((clsOutPatientRecipe_VO)objMainRecipeArr[0]).m_strOutpatRecipeID)
            {
                for (int intI = 0; intI < objRD_VO.Length; intI++)
                {
                    objRD_VO[intI].m_strOutpatRecipeID = ((clsOutPatientRecipe_VO)objMainRecipeArr[0]).m_strOutpatRecipeID;
                }
            }
            string strIniFileName = Application.StartupPath + @"\ID.ini";
            string strOpChargeDeptId = null;
            if (File.Exists(strIniFileName))
            {
                StringBuilder isAutorun = new StringBuilder(128);
                GetPrivateProfileString("OpChargeDeptID", "DeptId", "", isAutorun, 128, strIniFileName);
                strOpChargeDeptId = isAutorun.ToString();
            }
            string strRecipeID;
            //ArrayList m_objMedicineSend;
            if (this.m_objViewer.blnFlag)
            {
                objMedicineSend = new List<clsMedrecipesend_VO>();
                //objRD_VO = new clsRecipeDetail_VO[0];
            }
            long strRet = objSvc.m_mthSaveAllData(objMainRecipeArr, out strRecipeID, objRD_VO, this.m_objViewer.numericUpDown1.Value, objInvoice_VO, objArr1, objArr2, objMedicineSend, strOpChargeDeptId, this.m_objViewer.blnFlag);
            if (strRet <= 0)
            {
                MessageBox.Show("�Բ���,������Ϣʧ�ܡ�", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                //for (int i = 0; i < m_objMedicineSend.Count; i++)
                //{
                //    clsMedrecipesend_VO m_objTemp = m_objMedicineSend[i] as clsMedrecipesend_VO;
                //    if (m_objTemp.m_strFlag == "0")
                //    {
                //        objSvc.m_mthGetSerNO(out m_strSerNO);
                //        m_objTemp.m_strSerNO = m_strSerNO;
                //        m_objWMSendList.Add(m_objTemp);
                //    }
                //    else
                //    {
                //        m_objCMSendList.Add(m_objTemp);
                //    }
                //}

                //objMedicineSend.Clear();
                //objMedicineSend.AddRange(m_objMedicineSend);
            }

            // ��ҩ�� --> ��ҩ��������
            this.m_objViewer.IsCmRecipe = false;
            for (int i = 0; i < p_objPC.Length; i++)
            {
                foreach (clsInvoiceTypeDetail_VO item in objArr1[i])
                {
                    if (item.m_strITEMCATID_CHR == "0003")
                    {
                        this.m_objViewer.IsCmRecipe = true;
                        break;
                    }
                }
            }

            this.m_objViewer.btSave.Tag = strRecipeID;
            return strRet;
        }

        private bool m_mthIsTheRightMedstore(string strWinID, ref DataTable dtbMedWindows)
        {
            bool blRes = false;
            if (dtbMedWindows == null)
            {
                return blRes;
            }
            DataView dv = dtbMedWindows.DefaultView;
            dv.RowFilter = "windowid_chr='" + strWinID + "'";
            if (dv.Count > 0)
            {
                blRes = true;
            }
            else
            {
                blRes = false;
            }
            dv.Dispose();
            dv = null;
            return blRes;
        }

        #region �ۼ���Ϣ��VO
        /// <summary>
        /// �ۼ���Ϣ��VO
        /// </summary>
        private class MedDeduct_VO
        {
            /// <summary>
            /// ������
            /// </summary>
            public string strRecipeID = string.Empty;
            /// <summary>
            /// �к�
            /// </summary>
            public int intRowNO = 0;
            /// <summary>
            /// ҩƷ�շ���ĿID
            /// </summary>
            public string strItemID = string.Empty;
            /// <summary>
            /// ҩƷ�շ���Ŀ����
            /// </summary>
            public string strItemName = string.Empty;
            /// <summary>
            /// ��ҩ����
            /// </summary>
            public decimal decAmount = 0;
            /// <summary>
            /// �м��װ��
            /// </summary>
            public decimal decMidPackQty = 0;
            /// <summary>
            /// ��λ��־ 0 ������λ 1 ��С��λ 2 �м䵥λ
            /// </summary>
            public int intUnitFlag = 1;
            /// <summary>
            /// ҩƷ���� 1 ��ҩ 2 ��ҩ
            /// </summary>
            public int intMedType = 1;
            /// <summary>
            /// ִ�п���ID
            /// </summary>
            public string strExecDeptID = string.Empty;
        }
        #endregion

        /// <summary>
        /// ���洦����ϸ
        /// </summary>
        /// <param name="strRecipeNo">��������</param>
        /// <param name="objRD_VO"></param>
        private void m_mthSaveRecipeDetial(string strRecipeNo, clsRecipeDetail_VO[] objRD_VO)
        {
            objSvc.m_mthSaveRecipeDetial(strRecipeNo, objRD_VO, m_objViewer.numericUpDown1.Value);
        }
        /// <summary>
        /// ���洦���շ���ϸ
        /// </summary>
        /// <param name="strRecipeNo"></param>
        /// <param name="objRD_VO"></param>
        private void m_mthSaveRecipeChargeItemDetial(string strRecipeNo, clsRecipeDetail_VO[] objRD_VO)
        {
            objSvc.m_mthSaveRecipeChargeItemDetial(strRecipeNo, objRD_VO);
        }
        /// <summary>
        /// ���洦�����ͼ�¼
        /// </summary>
        /// <param name="strRecipeNo">������</param>
        /// <param name="strRECIPETYPE">����ID</param>
        private void m_mthSaveRecipeSend(string strRecipeNo, string strRECIPETYPE)
        {
            clsMedrecipesend_VO objMRS_VO = new clsMedrecipesend_VO();
            objMRS_VO.m_strOUTPATRECIPEID_CHR = strRecipeNo;
            objMRS_VO.m_intPSTATUS_INT = 1;
            objMRS_VO.m_strRECIPETYPE_INT = strRECIPETYPE;
            objMRS_VO.m_strSENDDATE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strEmployeeID = "0000001";
            if (m_objViewer.LoginInfo != null)
            {
                strEmployeeID = m_objViewer.LoginInfo.m_strEmpID;
            }
            objMRS_VO.m_strSENDEMP_CHR = strEmployeeID;

            objMRS_VO.m_strWINDOWID_CHR = this.strWMedicineWindowID;//����"0003"�Ժ������¶�(��ҩ)
            if (strRECIPETYPE == "0002")
            {
                objMRS_VO.m_strWINDOWID_CHR = this.strCMedicineWindowID;//����"0004"�Ժ������¶�(��ҩ)
            }
            objMRS_VO.m_strTREATDATE_DAT = "";
            objMRS_VO.m_strTREATEMP_CHR = "";
            objSvc.m_mthSaveRecipeSend(objMRS_VO);
        }
        #endregion

        #region �������洦������
        /// <summary>
        /// �������洦������
        /// </summary>
        public void m_mthSaveRecipe()
        {
            clsRecipeInfo_VO obj = this.m_objViewer.txtLoadRecipeNO.RecipeInfo;
            if (obj != null && obj.m_intPSTATUS_INT != 1)
            {
                MessageBox.Show("����ʷ�������豣��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.m_objViewer.btSave.Tag != null && this.EmpInputMode == false)
            {
                MessageBox.Show("����ʷ�������豣��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (!this.IsChargeReceiverRec)
                {
                    MessageBox.Show("����ϵͳ��ǰ����Ϊ���ܽ����տ�Ա����������Ŀ�����Բ��ܱ��浱ǰ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            string RecipeID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            if (this.m_objViewer.btSave.Tag != null)
            {
                RecipeID = this.m_objViewer.btSave.Tag.ToString();
            }

            //��������Ϣ
            List<clsOutPatientRecipe_VO> objMainRecipeArr = new List<clsOutPatientRecipe_VO>();

            #region �жϱ������� ��ϸ��Ϣ
            for (int i = m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                if (m_objViewer.ctlDataGrid1.RowCount == 1 && m_objViewer.ctlDataGrid1[0, 10].ToString().Trim() == "")
                {
                    break;
                }
                if (m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() == "")
                {
                    m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                    continue;
                }
                if (m_objViewer.ctlDataGrid1[i, 26].ToString().Trim() == "")//�������Ϊ�������
                {
                    m_objViewer.ctlDataGrid1[i, 26] = RecipeID;
                }
            }

            //��ϸ��Ϣ
            clsRecipeDetail_VO[] objRD_VO = new clsRecipeDetail_VO[m_objViewer.ctlDataGrid1.RowCount];
            int intLocation = 0;
            Hashtable has = new Hashtable();
            for (int i = 0; i < m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                objRD_VO[intLocation] = new clsRecipeDetail_VO();

                if (m_objViewer.ctlDataGrid1[i, 1] == null || m_objViewer.ctlDataGrid1[i, 1].ToString().Trim() == "")
                {
                    MessageBox.Show("����Ϊ�ղ��ܱ���!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.m_objViewer.ctlDataGrid1.Select();
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(i, 1);
                    return;
                }
                //���շѣ���ĿID                
                objRD_VO[intLocation].strRowNO = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 16]).ToString();
                objRD_VO[intLocation].decDiscount = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 14]);
                objRD_VO[intLocation].decPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 6]);
                objRD_VO[intLocation].decQuantity = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 1]);
                objRD_VO[intLocation].decSumMoney = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 7]);
                objRD_VO[intLocation].strCharegeItemID = m_objViewer.ctlDataGrid1[intLocation, 10].ToString().Trim();
                objRD_VO[intLocation].strUsageID = m_objViewer.ctlDataGrid1[intLocation, 17].ToString().Trim();
                objRD_VO[intLocation].strFrequencyID = m_objViewer.ctlDataGrid1[intLocation, 18].ToString().Trim();
                objRD_VO[intLocation].strDosage = m_objViewer.ctlDataGrid1[intLocation, 19].ToString().Trim();
                objRD_VO[intLocation].strDays = m_objViewer.ctlDataGrid1[intLocation, 20].ToString().Trim();
                objRD_VO[intLocation].strType = m_objViewer.ctlDataGrid1[intLocation, 9].ToString().Trim();
                objRD_VO[intLocation].strApplyID = m_objViewer.ctlDataGrid1[intLocation, 22].ToString().Trim();
                objRD_VO[intLocation].strCMedicineUsage = m_objViewer.numericUpDown1.Tag.ToString();
                objRD_VO[intLocation].strHYPETEST_INT = m_objViewer.ctlDataGrid1[intLocation, 24].ToString().Trim();
                objRD_VO[intLocation].strDESC_VCHR = m_objViewer.ctlDataGrid1[intLocation, 25].ToString().Trim();
                objRD_VO[intLocation].m_strOutpatRecipeID = m_objViewer.ctlDataGrid1[intLocation, 26].ToString().Trim();
                objRD_VO[intLocation].m_strItemspec = m_objViewer.ctlDataGrid1[intLocation, 4].ToString().Trim();
                objRD_VO[intLocation].m_decDosage = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, 19]);
                objRD_VO[intLocation].m_strDosageunit = m_objViewer.ctlDataGrid1[intLocation, 21].ToString().Trim();
                objRD_VO[intLocation].m_strATTACHPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, ResubitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decAttachitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, ResubnumsCol]);
                objRD_VO[intLocation].m_strUSAGEPARENTID_VCHR = m_objViewer.ctlDataGrid1[intLocation, UsageitemCol].ToString().Trim();
                objRD_VO[intLocation].m_decUsageitembasenum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, UsagenumsCol]);
                objRD_VO[intLocation].m_strItemname = m_objViewer.ctlDataGrid1[intLocation, 2].ToString().Trim();
                objRD_VO[intLocation].strUint = m_objViewer.ctlDataGrid1[intLocation, 5].ToString().Trim();
                objRD_VO[intLocation].m_intDeptmed = Function.Int(m_objViewer.ctlDataGrid1[intLocation, Deptmed].ToString());
                objRD_VO[intLocation].m_strOrderID = m_objViewer.ctlDataGrid1[intLocation, OrderID].ToString().Trim();
                objRD_VO[intLocation].m_decOrderBaseNum = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, OrderNum]);
                // ���������
                if (this.intDiffPriceOn == 1)
                    objRD_VO[intLocation].m_decTolDiffPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, intDiffPriceTotalCol]);
                if (objRD_VO[intLocation].m_decTolDiffPrice == 0)
                    objRD_VO[intLocation].BuyPrice = objRD_VO[intLocation].decPrice;
                else
                    objRD_VO[intLocation].BuyPrice = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[intLocation, intDiffUnitPriceCol]);
                intLocation++;
            }
            if (objRD_VO[0].strCharegeItemID == null || objRD_VO[0].strCharegeItemID.Trim() == "")//��һ�е���ĿIDΪ�ձ�ʾû���շ���Ŀ
            {
                return;
            }
            #endregion

            #region �ռ���������Ϣ
            clsOutPatientRecipe_VO OPR_VO = new clsOutPatientRecipe_VO();
            OPR_VO.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            OPR_VO.m_strOperatorID = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
            OPR_VO.m_strRegisterID = m_objViewer.m_PatientBasicInfo.RegisterID.Trim();
            OPR_VO.m_strDoctorID = m_objViewer.m_PatientBasicInfo.DoctorID.Trim();
            OPR_VO.m_strDepID = m_objViewer.m_PatientBasicInfo.DeptID.Trim();
            OPR_VO.m_strCreateDate = m_objViewer.m_PatientBasicInfo.RegisterDate;
            OPR_VO.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID.Trim();
            OPR_VO.m_intPStatus = 1;
            OPR_VO.m_intType = int.Parse(m_objViewer.m_cmbRecipeType.Tag.ToString());
            OPR_VO.m_strPatientType = m_objViewer.m_PatientBasicInfo.PayTypeID;
            OPR_VO.m_strRecipeType = "0";
            OPR_VO.intCreatetype = 1;
            OPR_VO.intDeptmed = 0;
            OPR_VO.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
            OPR_VO.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
            OPR_VO.m_strOutpatRecipeID = RecipeID;
            OPR_VO.IsProxyBoilMed = this.m_objViewer.cboProxyBoilMed.SelectedIndex;

            objMainRecipeArr.Add(OPR_VO);
            m_objMainRecipeList.Add(OPR_VO);
            #endregion

            long l = this.objSvc.m_lngSaveRecipe(objMainRecipeArr, objRD_VO, this.m_objViewer.numericUpDown1.Value);
            if (l <= 0)
            {
                MessageBox.Show("�Բ���,���洦����Ϣʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("���洦����Ϣ�ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.objHashTable.Clear();

            clsOutPatientRecipe_VO objTemp = new clsOutPatientRecipe_VO();
            objTemp.m_intPStatus = 1;
            objTemp.intCreatetype = 1;
            objTemp.m_strCaseHistoryID = "";
            objTemp.m_strDepID = this.m_objViewer.m_PatientBasicInfo.CurrentDeptID;
            objTemp.m_strCreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objTemp.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objTemp.m_strDoctorID = this.m_objViewer.m_PatientBasicInfo.CurrentDoctorID;
            objTemp.m_strOutpatRecipeID = RecipeID;
            objTemp.m_strPatientType = this.m_objViewer.m_PatientBasicInfo.PayTypeID;
            objTemp.m_strRecipeType = "1";
            objTemp.m_strRegisterID = this.m_objViewer.m_PatientBasicInfo.RegisterID;
            objTemp.m_intType = int.Parse(m_objViewer.m_cmbRecipeType.Tag.ToString());
            objTemp.m_strPatientID = this.m_objViewer.m_PatientBasicInfo.PatientID.Trim();
            objTemp.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
            objTemp.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
            this.objHashTable.Add(0, objTemp);

            this.m_objViewer.btSave.Tag = RecipeID;
            this.EmpInputMode = true;
        }
        #endregion

        #region ���淢Ʊ��Ϣ
        /// <summary>
        /// ���淢Ʊ��Ϣ
        /// </summary>
        /// <param name="strRecipeNo">��������</param>
        public long m_mthSaveInvoicInfo(com.digitalwave.iCare.middletier.HI.clsPatientChargeCal p_objPC, int flag)
        {
            //		clsPatientChargeCal objPC=this.objCalPatientCharge.m_mthGetChargeTypeDetail();

            com.digitalwave.iCare.middletier.HI.clsPatientChargeCal objPC = p_objPC;
            clsInvoice_VO objIn_VO = new clsInvoice_VO();
            objIn_VO.m_decACCTSUM_MNY = objPC.m_decChargeUpCost;
            objIn_VO.m_decSBSUM_MNY = objPC.m_decPersonCost;
            objIn_VO.m_intSTATUS_INT = 1;
            if (p_objPC.m_strDateOfReception != null && p_objPC.m_strDateOfReception.ToString().Trim() != "")
            {
                objIn_VO.m_strINVDATE_DAT = p_objPC.m_strDateOfReception;
            }
            else
            {
                objIn_VO.m_strINVDATE_DAT = m_objViewer.m_PatientBasicInfo.RegisterDate;
            }

            objIn_VO.m_strINVOICENO_VCHR = objPC.m_strInvoiceNO;
            string strEmployeeID = "0000001";
            if (m_objViewer.LoginInfo != null)
            {

                strEmployeeID = m_objViewer.LoginInfo.m_strEmpID;
            }
            objIn_VO.m_strOPREMP_CHR = strEmployeeID;//m_objViewer.LoginInfo.m_strEmpName;//
            objIn_VO.m_strRECORDDATE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objIn_VO.m_strRECORDEMP_CHR = strEmployeeID;//m_objViewer.LoginInfo.m_strEmpName;
            objIn_VO.m_strSEQID_CHR = m_objViewer.m_PatientBasicInfo.RegisterNo;//�������м�����¶�������ֵ
            objIn_VO.m_strBALANCEEMP_CHR = strEmployeeID;
            objIn_VO.m_strBALANCE_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objIn_VO.m_intBALANCEFLAG_INT = 0;
            objIn_VO.m_decTOTALSUM_MNY = objPC.m_decTotalCost;
            objIn_VO.m_intPAYTYPE_INT = this.m_objViewer.m_cmbPayMode.SelectedIndex;
            objIn_VO.m_strPATIENTID_CHR = this.m_objViewer.m_PatientBasicInfo.PatientID;
            objIn_VO.m_strPATIENTNAME_CHR = this.m_objViewer.m_PatientBasicInfo.PatientName;
            objIn_VO.m_strDEPTID_CHR = this.m_objViewer.m_PatientBasicInfo.DeptID;
            objIn_VO.m_strDEPTNAME_CHR = this.m_objViewer.m_PatientBasicInfo.DeptName;
            objIn_VO.m_strDOCTORID_CHR = this.m_objViewer.m_PatientBasicInfo.DoctorID;
            objIn_VO.m_strDOCTORNAME_CHR = this.m_objViewer.m_PatientBasicInfo.DoctorName;
            objIn_VO.m_strCONFIRMEMP_CHR = "";
            objIn_VO.m_strPAYTYPEID_CHR = this.m_objViewer.m_PatientBasicInfo.PayTypeID;
            objIn_VO.m_strHospitalID_CHR = this.strHopitalID;
            if (flag > 0 && m_objViewer.m_txtInvoiceNO.Tag != null)
            {
                objIn_VO.m_strBASESEQID_CHR = m_objViewer.m_txtInvoiceNO.Tag.ToString();
            }
            else
            {
                objIn_VO.m_strBASESEQID_CHR = "";
            }
            if (this.m_objViewer.btSave.Tag != null)
            {
                objIn_VO.m_strSEQID_CHR = DateTime.Now.ToString("yyyyMMddhhmmssffff");
                m_objViewer.m_txtInvoiceNO.Tag = objIn_VO.m_strSEQID_CHR;
                objIn_VO.m_strOUTPATRECIPEID_CHR = this.m_objViewer.btSave.Tag.ToString();
            }
            else
            {
                return -1;
            }
            return objSvc.m_mthSaveInvoicInfo(objIn_VO);

        }
        public void m_mthSaveInvoiceDetail(com.digitalwave.iCare.middletier.HI.clsPatientChargeCal p_objPC)
        {

            //			this.objCalPatientCharge.m_mthSaveInvoiceDetail(p_objPC.m_strInvoiceNO,p_objPC,m_objViewer.m_txtInvoiceNO.Tag.ToString());

        }

        public void m_mthSaveInvoiceDetail2(string strInvoiceNO)
        {
            //			this.objCalPatientCharge.m_mthSaveInvoiceDetail2(strInvoiceNO,m_objViewer.m_txtInvoiceNO.Tag.ToString());
            //			

        }
        #endregion

        #region ��ӡ��Ʊ
        /// <summary>
        /// ��ӡ��Ʊ
        /// </summary>
        /// <param name="p_objPC"></param>
        /// <param name="strInvoiceNO"></param>
        /// <returns></returns>
        public long m_mthPrintInvoice(com.digitalwave.iCare.middletier.HI.clsPatientChargeCal p_objPC, out string strInvoiceNO)
        {
            strInvoiceNO = p_objPC.m_strInvoiceNO;
            com.digitalwave.iCare.middletier.HI.clsPatientChargeCal objPC = p_objPC;
            objPC.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID;
            objPC.m_strPatientCardID = m_objViewer.m_PatientBasicInfo.PatientCardID;
            objPC.m_strPatientName = m_objViewer.m_PatientBasicInfo.PatientName;
            //				objPC.m_strBalanceMode=m_objViewer.m_cmbPayMode.Text;
            string strEmployeeID = "0000001";
            if (m_objViewer.LoginInfo != null)
            {
                strEmployeeID = m_objViewer.LoginInfo.m_strEmpNo;
            }
            //				objPC.m_strAssessor=strEmployeeID;//m_objViewer.LoginInfo.m_strEmpName;
            //���Ա��Ϊ�� �����Ҵ��룩ҽ������
            objPC.m_strAssessor = "(" + objSvc.m_strGetDeptNO(m_objViewer.m_PatientBasicInfo.DeptID) + ")" + m_objViewer.m_PatientBasicInfo.DoctorNo;
            if (this.m_strShowOPChargeManInfo == "1")
            {
                objPC.m_strCollector = strEmployeeID + "(" + this.m_objViewer.LoginInfo.m_strEmpName + ")"; ;//m_objViewer.LoginInfo.m_strEmpName;
            }
            else
            {
                objPC.m_strCollector = strEmployeeID;
            }
            objPC.m_strSeriesNumber = p_objPC.m_strSeriesNumber;
            //				if(this.m_objViewer.btSave.Tag!=null)
            //				{
            //					objPC.m_strSeriesNumber=this.m_objViewer.btSave.Tag.ToString();
            //				}
            if (this.m_objWMSendList.Count > 0)
            {
                objPC.m_strServerNo = ((clsMedrecipesend_VO)m_objWMSendList[0]).m_strSerNO;
            }
            if (p_objPC.m_strDateOfReception != null && p_objPC.m_strDateOfReception.ToString().Trim() != "")
            {
                objPC.m_strDateOfReception = p_objPC.m_strDateOfReception;
            }
            else
            {
                objPC.m_strDateOfReception = m_objViewer.m_PatientBasicInfo.RegisterDate;
            }
            //				objPC.m_strDateOfReception=p_objPC.m_strDateOfReception;
            objPC.m_strInvoiceNO = p_objPC.m_strInvoiceNO;
            objPC.m_strDocNo = m_objViewer.m_PatientBasicInfo.DoctorNo;
            objPC.m_strPatientTypeName = this.m_objViewer.m_PatientBasicInfo.txtType.Text;
            objPC.m_strSex = this.m_objViewer.m_PatientBasicInfo.txtSex.Text.Trim();
            objPC.m_strInsuranceID = this.m_objViewer.txtInsuranceID.Text.ToString().Trim();
            return this.objCalPatientCharge.m_mthPrintCharge(objPC);

        }
        #endregion

        #region ��ƱԤ��
        /// <summary>
        /// ��ƱԤ��
        /// </summary>
        /// <param name="p_objPC"></param>
        /// <returns></returns>
        public long m_mthPrintInvoicePreview(com.digitalwave.iCare.middletier.HI.clsPatientChargeCal p_objPC)
        {
            com.digitalwave.iCare.middletier.HI.clsPatientChargeCal objPC = p_objPC;
            objPC.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID;
            objPC.m_strPatientName = m_objViewer.m_PatientBasicInfo.PatientName;
            objPC.m_strBalanceMode = m_objViewer.m_cmbPayMode.Text;
            string strEmployeeID = "0000001";
            objPC.m_strDocNo = m_objViewer.m_PatientBasicInfo.DoctorNo;
            if (m_objViewer.LoginInfo != null)
            {
                strEmployeeID = m_objViewer.LoginInfo.m_strEmpNo;

            }
            //			objPC.m_strAssessor=strEmployeeID;//m_objViewer.LoginInfo.m_strEmpName;
            //���Ա��Ϊ�� �����Ҵ��룩ҽ������
            objPC.m_strAssessor = "(" + objSvc.m_strGetDeptNO(m_objViewer.m_PatientBasicInfo.DeptID) + ")" + m_objViewer.m_PatientBasicInfo.DoctorNo;
            if (this.m_strShowOPChargeManInfo == "1")
            {
                objPC.m_strCollector = strEmployeeID + "(" + this.m_objViewer.LoginInfo.m_strEmpName + ")"; ;//m_objViewer.LoginInfo.m_strEmpName;
            }
            else
            {
                objPC.m_strCollector = strEmployeeID;
            }
            objPC.m_strSeriesNumber = "";
            if (this.m_objViewer.btSave.Tag != null)
            {
                objPC.m_strSeriesNumber = this.m_objViewer.btSave.Tag.ToString();
            }
            objPC.m_strDateOfReception = m_objViewer.m_PatientBasicInfo.RegisterDate;
            objPC.m_strInvoiceNO = m_objViewer.m_txtInvoiceNO.Text.Trim();
            objPC.m_strHospitalName = this.m_objComInfo.m_strGetHospitalTitle();
            objPC.m_strPatientTypeName = this.m_objViewer.m_PatientBasicInfo.txtType.Text;
            return objCalPatientCharge.m_mthPrintChargePreview(objPC);


        }
        #endregion

        #region ɾ��������Ŀ
        public void m_mthDeleteRecipe(int row)
        {
            //		m_objViewer.ctlDataGrid1.m_mthDeleteRow(row);
            this.objCalPatientCharge.m_mthDelteChargeItem(row);
            this.m_mthDisplayCharge();
        }
        #endregion

        #region ���ķ���
        /// <summary>
        /// ���ķ���
        /// </summary>
        /// <param name="CurRow">datagrid��ǰ���к�</param>
        /// <param name="ChaRow">�շ����е��к�</param>
        public void m_mthChangeTimes(int CurRow, int ChaRow)
        {

            decimal price = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[CurRow, 6]);
            decimal dosage = m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[CurRow, 1]) * m_objViewer.numericUpDown1.Value;
            dosage = Math.Ceiling(dosage);
            decimal discount = 100;
            if (m_objViewer.ctlDataGrid1[CurRow, 14] != null && m_objViewer.ctlDataGrid1[CurRow, 14].ToString().Trim() != "")
            {
                discount = Convert.ToDecimal(m_objViewer.ctlDataGrid1[CurRow, 14].ToString().Trim());
            }
            //			decimal dosage=0;
            //			try
            //			{
            //			dosage=Convert.ToDecimal(m_objViewer.ctlDataGrid1[CurRow,1].ToString());
            //			dosage=dosage*m_objViewer.numericUpDown1.Value;
            //			}
            //			catch
            //			{
            //			dosage=0;
            //			} 
            //			decimal price=0;
            //			try
            //			{
            //			price =Convert.ToDecimal(m_objViewer.ctlDataGrid1[CurRow,6].ToString());
            //			}
            //			catch
            //			{
            //			price=0;
            //			}
            decimal summoney = dosage * price;
            summoney = m_mthConvertObjToDecimal(summoney.ToString("0.00"));
            m_objViewer.ctlDataGrid1[CurRow, 7] = summoney.ToString();

            //			((ChargeItem)this.objCalPatientCharge.m_ChargeItem[ChaRow]).decDosage=dosage;

            string strChargeID = m_objViewer.ctlDataGrid1[CurRow, 10].ToString().Trim();
            string ChargeTypeID = m_objViewer.ctlDataGrid1[CurRow, 8].ToString().Trim();
            string strcat = m_objViewer.ctlDataGrid1[CurRow, 15].ToString().Trim();
            if (this.intDiffPriceOn == 1)
                m_objViewer.ctlDataGrid1[CurRow, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(strChargeID, price, m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[CurRow, intDiffUnitPriceCol]), ChargeTypeID, dosage, ChaRow, discount, strcat, CheckIsDiff(CurRow));
            else
                m_objViewer.ctlDataGrid1[CurRow, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(strChargeID, price, ChargeTypeID, dosage, ChaRow, discount, strcat, false);

            this.m_mthDisplayCharge();
            //			if(!p)
            //			{
            //				MessageBox.Show("��ע��,����ҩ���Ѿ����޶�!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //			}
        }
        #endregion

        #region ���Ҳ��˴�����¼
        //		/// <summary>
        //		/// ���ҳ����˵Ĵ�����¼,�����������
        //		/// </summary>
        //		public void m_mthFindRecipeNoByPatientID()
        //		{
        //		clsRecipeInfo_VO[] objRI_VO=null;
        //		long ret =objSvc.m_mthFindRecipeNoByPatientID(m_objViewer.m_PatientBasicInfo.PatientID, out objRI_VO);
        //			m_objViewer.m_cmbRecipeNO.Items.Clear();
        //			if(ret>0&&objRI_VO!=null)
        //			{
        //				
        //				for(int i=0;i<objRI_VO.Length;i++)
        //				{
        //				m_objViewer.m_cmbRecipeNO.Items.Add(objRI_VO[i].m_strOUTPATRECIPEID_CHR.Trim());
        //				}
        //				m_objViewer.m_cmbRecipeNO.Tag=objRI_VO;
        //			}
        //		}
        #endregion

        #region ���Ҳ������һ��û���շѵĴ�����
        //ҵ������ͬһ���˿����ж��Ŵ���������һ�ź��ӡ��Ʊ��ϵͳ�������û��Ƿ������
        //��δ�շѵĴ�����������ǡ���������������ͬһ������������һ�����ϵĴ������շѣ�Ϊ
        //���շ�Աһ������Ǯ�ķ��������Ҫ��һ��С���ڵ�����δ���ж����Ŵ�����������Ǯ�����˺�
        //�Ը����ٵ���Ϣ��Ϊ��
        //���涨����⼸��������Ϊ�˱����շ���Ϣ�ĺϼ�
        /// <summary>
        /// ���ν��˴�����
        /// </summary>
        public int RecipeCountThisTime = 0;
        /// <summary>
        /// ��¼δ���˴�����
        /// </summary>
        public int RecipeCount = 0;
        /// <summary>
        /// δ�ᴦ������
        /// </summary>
        public int SumRecipeCount = 0;
        /// <summary>
        /// �������ܶ�
        /// </summary>
        public decimal SumTotalMoney = 0;
        /// <summary>
        /// ���˽��ϼ�
        /// </summary>
        public decimal SumChargeUpMoney = 0;
        /// <summary>
        /// �Ը����ϼ�
        /// </summary>
        public decimal SumPersonMoney = 0;
        /// <summary>
        /// �������ܶ�
        /// </summary>
        public string strSumTotalMoney = "";
        /// <summary>
        /// ���˽��ϼ�
        /// </summary>
        public string strSumChargeUpMoney = "";
        /// <summary>
        /// �Ը����ϼ�
        /// </summary>
        public string strSumPersonMoney = "";
        /// <summary>
        /// ���Ҳ������һ��û���շѵĴ�����
        /// </summary>
        public void m_mthFindMaxRecipeNoByPatientID()
        {
            string strRecipe;
            string strstatus;
            string strSeqid = "";
            string strISgreen = "";
            DataTable dt;
            long ret = 0;
            ////�����ƺ���㲡�˴�����ѯ
            //if (this.m_objViewer.m_PatientBasicInfo.m_strIsVip == "1")
            //{
            //    ret = objSvc.m_mthFindTreatRecipeNoByPatientID(m_objViewer.m_PatientBasicInfo.PatientID, out strRecipe,out strSeqid, out strstatus, out RecipeCount, out dt);
            //    this.m_objViewer.blnFlag = true;
            //}
            //else
            //{
            ret = objSvc.m_mthFindMaxRecipeNoByPatientID(m_objViewer.m_PatientBasicInfo.PatientID, out strRecipe, out strSeqid, out strstatus, out RecipeCount, out dt, out strISgreen, this.IsChildPrice);
            //}
            if (SumRecipeCount == 0)//���Ϊ0�������¿�ʼ��ѯ���˴�����
            {
                SumRecipeCount = this.RecipeCount;
                //if(RecipeCount>1)
                //{
                //    MessageBox.Show("�ò��˹�"+RecipeCount.ToString()+"δ�ᴦ��");
                //}
            }
            if (!string.IsNullOrEmpty(strISgreen) && strISgreen == "1")
            {
                this.m_objViewer.blnFlag = true;
            }
            else
            {
                this.m_objViewer.blnFlag = false;
            }
            if (ret > 0 && strRecipe.Trim() != "")
            {
                this.m_objViewer.txtLoadRecipeNO.Text = strRecipe;
                this.m_objViewer.txtLoadRecipeNO.Tag = strSeqid;
                clsRecipeInfo_VO[] objRI_VO = null;
                long l = objSvc.m_mthFindRecipeDoctor(this.m_objViewer.m_PatientBasicInfo.PatientID, strRecipe, out objRI_VO);
                if (l > 0 && objRI_VO != null)
                {
                    this.m_objViewer.m_PatientBasicInfo.DoctorName = objRI_VO[0].m_strDoctorName;
                    this.m_objViewer.m_PatientBasicInfo.DoctorID = objRI_VO[0].m_strDoctorID;
                    this.m_objViewer.m_PatientBasicInfo.DoctorNo = objRI_VO[0].m_strDoctorNo;
                    this.m_objViewer.m_PatientBasicInfo.DeptName = objRI_VO[0].m_strDepName;
                    this.m_objViewer.m_PatientBasicInfo.DeptID = objRI_VO[0].m_strDepID;
                    this.m_objViewer.m_PatientBasicInfo.PayTypeName = objRI_VO[0].m_strPatientTypeName;
                    this.m_objViewer.m_PatientBasicInfo.PayTypeID = objRI_VO[0].m_strPatientTypeID;
                    this.m_objViewer.m_PatientBasicInfo.PatientType = objRI_VO[0].m_intINTERNALFLAG_INT;
                    this.m_objViewer.m_PatientBasicInfo.Limit = objRI_VO[0].decLimint;
                    this.m_objViewer.m_PatientBasicInfo.Discount = objRI_VO[0].decDiscount;
                    //����������, Ĭ��ȡ��һ������������
                    if (objRI_VO[0].m_strRECIPEFLAG_INT.Trim() == "2")
                    {
                        this.m_objViewer.m_cmbRecipeType.SelectedIndex = 1;
                    }
                    else
                    {
                        this.m_objViewer.m_cmbRecipeType.SelectedIndex = 0;
                    }
                }
                this.m_objViewer.btSave.Tag = strRecipe;
                this.m_mthCreatNewCalobj();
                //��������strstatus��¼��ͬһ���շѺʹ�����
                //ҵ���������һ�������ж���δ��Ǯ�Ĵ��������ڵ�ϵͳҪ���ͬҽ����ͬ������ݵĴ�
                //��һ������շѡ�
                int intTempCount = int.Parse(strstatus);
                if (intTempCount > 1)
                {
                    //ҵ��������һ�����ϴ���ʱ��Ҫ��ѯ���û�ʱ�����������û�ѡ���ˡ���
                    //���ȡ��һ�ŵĴ����š�Ȼ��ѭ����ϸ�б��Ѳ�ͬ�����ŵ�����ɾ����
                    if (MessageBox.Show("����" + strstatus + "��ͬҽ���������Ƿ�һ�����?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        string strTempRecipeID = "";
                        if (dt.Rows.Count > 0)
                        {
                            strTempRecipeID = dt.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                        }
                        for (int i = dt.Rows.Count - 1; i > -1; i--)
                        {
                            if (dt.Rows[i]["OUTPATRECIPEID_CHR"].ToString().Trim() != strTempRecipeID)
                            {
                                dt.Rows[i].Delete();
                            }
                        }
                        dt.AcceptChanges();

                    }
                }
                //��������ݵ�DataGrid��ʱ����Ѵ����ű��浽HashTalbe��ȥ�������ڲ�Ҳ��˵����
                this.m_objViewer.IsSave = false;

                //�ٴγ�ʼ��
                this.m_objViewer.m_blnIsSelectChargeItem = false;
                //  �ж��������ƺ����Ĳ��ˣ�Ȼ��ѡ��ɷ���Ŀ
                if (strISgreen == "1")
                {
                    frmOPSelectChargeItem objFrom = new frmOPSelectChargeItem(dt, strRecipe, this.IsChildPrice);
                    if (objFrom.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dtbTemp = objFrom.m_dtbSelectTable;
                        List<string> lstOrderDicItemID = objFrom.m_lstOrderDicItemID;
                        if (dtbTemp.Rows.Count != dt.Rows.Count)//����ȣ�˵���Ѿ�����ѡ�񽻷�
                        {
                            //�ı��Ϊ�ֹ�����,����֮ǰ����������pstauts_int��Ϊ5
                            //m_mthSelectFeeDispose(dtbTemp, lstOrderDicItemID);
                            m_mthGetSelectChargeItemID(dtbTemp, lstOrderDicItemID);
                            this.m_objViewer.m_blnIsSelectChargeItem = true;
                            dt = dtbTemp;
                            //��¼ѡ����շ���Ŀ
                        }

                    }
                }

                string strMedUsageID = "0021";
                DataTable dtKFUsageID = new DataTable();
                clsDcl_OPCharge objDcl = new clsDcl_OPCharge();
                objDcl.m_lngGetMedUsageID(strMedUsageID, out dtKFUsageID);
                objDcl = null;

                List<string> lisKFUsageID = new List<string>();
                string[] strKFUsageIDArr = null;
                if (dtKFUsageID != null && dtKFUsageID.Rows.Count > 0)
                {
                    strKFUsageIDArr = dtKFUsageID.Rows[0][0].ToString().Split(';');
                    for (int k = 0; k < strKFUsageIDArr.Length; k++)
                    {
                        lisKFUsageID.Add(strKFUsageIDArr[k]);
                    }
                }

                DataView dvTemp = dt.DefaultView;
                dvTemp.Sort = "rowno_chr asc";
                DataTable dtTemp = dvTemp.ToTable();
                int intNumOfMedBag = 0;
                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    string strRowNo = dtTemp.Rows[j]["rowno_chr"].ToString().Trim();
                    string strCatID = dtTemp.Rows[j]["catid"].ToString().Trim();
                    string strUsageID = dtTemp.Rows[j]["usageid_chr"].ToString().Trim();
                    string strNextRowNo = "";
                    if (j + 1 < dt.Rows.Count)
                    {
                        strNextRowNo = dtTemp.Rows[j + 1]["catid"].ToString().Trim();
                    }

                    if (strRowNo == "0" && strCatID == "2" && lisKFUsageID.Contains(strUsageID))
                    {
                        ++intNumOfMedBag;
                    }
                    else if (strCatID == "2" && lisKFUsageID.Contains(strUsageID))
                    {
                        if (strRowNo != strNextRowNo)
                        {
                            ++intNumOfMedBag;
                        }
                    }
                }

                this.m_mthFillDataGrid(dt);

                if (intNumOfMedBag > 0)
                {
                    m_objViewer.ctlDataGrid1.Select();
                    m_objViewer.ctlDataGrid1.Focus();
                    m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid1.RowCount, 0);
                    // SendKeys.SendWait("841257"); ����ҩ���շѱ��� --��
                    SendKeys.SendWait("594957");  // ����ҩ���շѱ��� -- �� ���뷴������������
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait("{ENTER}");
                    string strTemp = intNumOfMedBag.ToString();
                    string strNumOfBag = "";
                    for (int i1 = strTemp.Length - 1; i1 >= 0; i1--)
                    {
                        strNumOfBag += strTemp[i1].ToString();
                    }
                    SendKeys.SendWait(strNumOfBag);
                    SendKeys.SendWait("{ENTER}");
                }

                if (IsCanModify)
                {
                    //this.m_objViewer.ctlDataGrid1.Enabled=false;
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[0]).ReadOnly = true;
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[1]).ReadOnly = true;
                    this.m_objViewer.btOK.Focus();
                }
                else
                {
                    this.m_objViewer.ctlDataGrid1.Enabled = true;
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[0]).ReadOnly = false;
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[1]).ReadOnly = false;
                    this.m_mthSetFocusOnDataGrid();
                }

                if (!IsModifyrecipetype)
                {
                    this.m_objViewer.m_PatientBasicInfo.txtType.Enabled = false;
                    this.m_objViewer.m_PatientBasicInfo.txtRegisterDept.Enabled = false;
                    this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Enabled = false;
                    this.m_objViewer.m_cmbRecipeType.Enabled = false;
                }

                if (!Recsumflag)
                {
                    this.m_mthIsOverFlow();
                }
            }
        }
        #endregion

        #region ���ݴ����Ų�������������ϸ
        /// <summary>
        /// ���ݴ����Ų�������������ϸ
        /// </summary>
        /// <param name="ID"></param>
        public void m_mthFindRecipeByID(string ID)
        {

            if (ID.Trim() == "")
            {
                return;
            }

            clsRecipeInfo_VO obj = this.m_objViewer.txtLoadRecipeNO.RecipeInfo;
            bool flag = false;
            if (obj.m_intPSTATUS_INT == 4)
            {
                flag = true;
            }

            this.m_mthFindRecipeDataByID(ID, flag);
            this.m_mthSetButtonEnable();
            if (!(obj.m_intPSTATUS_INT == 5))//m_intPSTATUS_INT == 5 �������ƺ����Ĵ���
            {
                if (objSvc.m_blnCheckRecipeProperty(ID))
                {
                    MessageBox.Show("�ô���Ϊ���շ�(��Ʊ)��ҽ���������������ܸ��á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.m_objViewer.btReUse.Enabled = false;
                }
            }
            else
            {
                //��������Ĵ�������������ʱ�������ǰ�Ľɷ���ˮ��
                string strSeqid = string.Empty;
                this.objSvc.m_lngGetRecipeByRecipeNo(ID, out strSeqid);
                this.m_objViewer.txtLoadRecipeNO.Tag = strSeqid;
                this.m_mthSetControlEnable(true);
            }


            if (flag && !IsModifyrecipetype)
            {
                this.m_objViewer.m_PatientBasicInfo.txtType.Enabled = false;
                this.m_objViewer.m_PatientBasicInfo.txtRegisterDept.Enabled = false;
                this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Enabled = false;
                this.m_objViewer.m_cmbRecipeType.Enabled = false;
            }
        }
        private void m_mthFindRecipeDataByID(string ID, bool flag)
        {
            this.OverFlowFlag = false;
            DataTable dt = null;

            //�����ƺ���㣬ѡ��ɷ���Ŀ  ddy
            //�ٴγ�ʼ��
            long ret = -1;
            this.m_objViewer.m_blnIsSelectChargeItem = false;
            //  �ж��������ƺ����Ĳ��ˣ�Ȼ��ѡ��ɷ���Ŀ
            if (this.m_objViewer.txtLoadRecipeNO.RecipeInfo.m_strIsGreen == "1")
            {
                ret = objSvc.m_mthFindRecipeByID(ID, out dt, true, this.IsChildPrice);
                frmOPSelectChargeItem objFrom = new frmOPSelectChargeItem(dt, ID, this.IsChildPrice);
                if (objFrom.ShowDialog() == DialogResult.OK)
                {
                    DataTable dtbTemp = objFrom.m_dtbSelectTable;
                    List<string> lstOrderDicItemID = objFrom.m_lstOrderDicItemID;
                    if (dtbTemp.Rows.Count != dt.Rows.Count)//����ȣ�˵���Ѿ�����ѡ�񽻷�
                    {
                        //�ı��Ϊ�ֹ�����,����֮ǰ����������pstauts_int��Ϊ5
                        //m_mthSelectFeeDispose(dtbTemp, lstOrderDicItemID);
                        m_mthGetSelectChargeItemID(dtbTemp, lstOrderDicItemID);
                        this.m_objViewer.m_blnIsSelectChargeItem = true;
                        dt = dtbTemp;
                        //��¼ѡ����շ���Ŀ
                    }
                }
            }
            else
            {
                ret = objSvc.m_mthFindRecipeByID(ID, out dt, flag, this.IsChildPrice);
            }

            this.m_mthCreatNewCalobj();
            if (ret > 0 && dt.Rows.Count > 0)
            {
                this.m_mthFillDataGrid(dt);
            }
            this.m_objViewer.btSave.Tag = ID;
            if (IsCanModify && flag)
            {
                //this.m_objViewer.ctlDataGrid1.Enabled = false;
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[0]).ReadOnly = true;
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[1]).ReadOnly = true;
                this.m_objViewer.btOK.Focus();
            }
            else
            {
                this.m_objViewer.ctlDataGrid1.Enabled = true;
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[0]).ReadOnly = false;
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[1]).ReadOnly = false;
                this.m_mthSetFocusOnDataGrid();
            }

            this.OverFlowFlag = true;

            if (!Recsumflag)
            {
                this.m_mthIsOverFlow();
            }
        }
        /// <summary>
        /// ���ݲ���Ĵ�����ϸ���datagrid
        /// </summary>
        /// <param name="dt"></param>
        private void m_mthFillDataGrid(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }

            BathCalc = true;
            //��ʱ������        
            string strTempRecipeID = "";

            for (int ii = 0; ii < dt.Rows.Count; ii++)
            {
                //Ҳ������Ϣ������HashTable�С�
                if (strTempRecipeID != dt.Rows[ii]["OUTPATRECIPEID_CHR"].ToString().Trim())
                {
                    clsOutPatientRecipe_VO objTemp = new clsOutPatientRecipe_VO();
                    objTemp.m_intPStatus = 2;
                    objTemp.intCreatetype = 0;
                    objTemp.m_strCaseHistoryID = dt.Rows[ii]["casehisid_chr"].ToString().Trim();
                    objTemp.m_strDepID = dt.Rows[ii]["DIAGDEPT_CHR"].ToString().Trim();
                    objTemp.m_strCreateDate = dt.Rows[ii]["CREATEDATE_DAT"].ToString().Trim();
                    objTemp.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objTemp.m_strDoctorID = dt.Rows[ii]["DIAGDR_CHR"].ToString().Trim();
                    objTemp.m_strOutpatRecipeID = dt.Rows[ii]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    objTemp.m_strPatientType = dt.Rows[ii]["PAYTYPEID_CHR"].ToString().Trim();
                    objTemp.m_strRecipeType = dt.Rows[ii]["TYPE_INT"].ToString().Trim();
                    objTemp.m_strRegisterID = dt.Rows[ii]["REGISTERID_CHR"].ToString().Trim();
                    objTemp.m_intType = int.Parse(dt.Rows[ii]["RECIPEFLAG_INT"].ToString());
                    objTemp.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID.Trim();
                    objTemp.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
                    objTemp.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
                    strTempRecipeID = dt.Rows[ii]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    this.objHashTable.Add(ii, objTemp);
                }
                //				decimal discount =this.objCalPatientCharge.m_mthGetDiscountByID(dt.Rows[ii]["ItemID"].ToString());
                decimal discount = this.m_mthConvertObjToDecimal(dt.Rows[ii]["DISCOUNT_DEC"]);
                string strUnit = dt.Rows[ii]["UNIT"].ToString();
                decimal decPrice = m_mthConvertObjToDecimal(dt.Rows[ii]["price"]);
                decimal decDiffPrice = 0;
                if (blMedicine9003(dt.Rows[ii]["ItemID"].ToString().Trim()))
                {
                    decDiffPrice = m_mthConvertObjToDecimal(dt.Rows[ii]["tradeprice_mny"]);// �����۸�
                }
                else
                {
                    decDiffPrice = decPrice;
                }
                if (string.IsNullOrEmpty(dt.Rows[ii]["tradeprice_mny"].ToString()))
                    decDiffPrice = decPrice;
                //�ж��Ƿ���ҩ���Ƿ�����С��λ���㡣С��λ������ָ��ҩ�ܷ��ɢ���ۣ���һ��ҩ��ʮ����
                //�����һ��һ����������С��λ���㣬��֮����λ���㡣���շ���Ŀ�ж����ˡ�
                //�ж��Ƿ�ʱҩ���ǰ���Ʊ����������
                if (m_mthIsMedicine(dt.Rows[ii]["InvType"].ToString().Trim()) && dt.Rows[ii]["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                {
                    strUnit = dt.Rows[ii]["ITEMIPUNIT_CHR"].ToString().Trim();//��λ
                    decPrice = this.m_mthConvertObjToDecimal(dt.Rows[ii]["SUBMONEY"]);//����
                }
                if (blMedicine9003(dt.Rows[ii]["ItemID"].ToString().Trim()) && dt.Rows[ii]["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                {
                    decDiffPrice = this.m_mthConvertObjToDecimal(dt.Rows[ii]["subtrademoney"]);// ������λ�۸�
                }
                if (m_mthConvertObjToDecimal(dt.Rows[ii]["Times"]) > m_objViewer.numericUpDown1.Value)//��ҩ������ֵ,������ֵ�ĸ�����
                {
                    m_objViewer.numericUpDown1.Value = m_mthConvertObjToDecimal(dt.Rows[ii]["Times"]);
                }
                this.m_objViewer.cboProxyBoilMed.SelectedIndex = (int)m_mthConvertObjToDecimal(dt.Rows[ii]["isproxyboilmed"]);

                //�����ݷŵ�DataGrid �С�
                object[] objNewRow ={
                            dt.Rows[ii]["ITEMCODE_VCHR"].ToString(),//��ѯ
							dt.Rows[ii]["quantity"].ToString(),//����
							dt.Rows[ii]["itemname"],//��Ŀ����
							m_mthConvertToChType(dt.Rows[ii]["InvType"].ToString()),//����
							dt.Rows[ii]["Dec"],//���
							strUnit,//��λ
							decPrice,//����
							dt.Rows[ii]["SumMoney"],//�ܼ�
							dt.Rows[ii]["InvType"].ToString(),//���﷢Ʊ�������ID
							m_mthRelationInfo(dt.Rows[ii]["InvType"].ToString().Trim()),//�շ���Ŀ����ID
							dt.Rows[ii]["ItemID"].ToString(),//��ĿID
							dt.Rows[ii]["SELFDEFINE"].ToString(),//�Զ��۸�
							ii.ToString(),//�к�
							discount.ToString()+"%",
                            discount,
                            dt.Rows[ii]["ITEMOPCALCTYPE_CHR"].ToString(),
                            dt.Rows[ii]["ROWNO_CHR"].ToString(),
                            dt.Rows[ii]["USAGEID_CHR"].ToString(),
                            dt.Rows[ii]["FREQID_CHR"].ToString(),
                               dt.Rows[ii]["QTY_DEC"].ToString(),
                            dt.Rows[ii]["DAYS_INT"].ToString(),
                            dt.Rows[ii]["DOSAGEUNIT_CHR"].ToString(),
                            dt.Rows[ii]["ATTACHID_VCHR"].ToString(),
                            m_mthConvertObjToDecimal(dt.Rows[ii]["SumMoney"])*discount/100,
                             dt.Rows[ii]["HYPETEST_INT"].ToString(),
                            dt.Rows[ii]["DESC_VCHR"].ToString(),
                            dt.Rows[ii]["OUTPATRECIPEID_CHR"].ToString(),
                            dt.Rows[ii]["ATTACHPARENTID_VCHR"].ToString(),
                            dt.Rows[ii]["attachitembasenum_dec"].ToString(),
                            dt.Rows[ii]["USAGEPARENTID_VCHR"].ToString(),
                            dt.Rows[ii]["usageitembasenum_dec"].ToString(),
                            dt.Rows[ii]["deptmed_int"].ToString(),
                            dt.Rows[ii]["orderid"].ToString(),
                            dt.Rows[ii]["ordernum"].ToString(),
                            dt.Rows[ii]["insuranceid_chr"].ToString()};

                m_objViewer.ctlDataGrid1.m_mthAppendRow(objNewRow);
                decimal temp = m_mthConvertObjToDecimal(dt.Rows[ii]["quantity"]);
                //decimal temp = m_mthConvertObjToDecimal(dt.Rows[ii]["discount_dec"]);

                if (dt.Rows[ii]["InvType"].ToString().Trim() == objCalPatientCharge.InvoiceCatID)
                {
                    temp = temp * this.m_mthConvertObjToDecimal(dt.Rows[ii]["Times"]);
                    temp = Math.Ceiling(temp);
                }
                if (dt.Rows[ii]["SUMUSAGE_VCHR"].ToString().Trim() != "")
                {
                    this.m_objViewer.numericUpDown1.Tag = dt.Rows[ii]["SUMUSAGE_VCHR"].ToString().Trim();
                }
                if (intDiffPriceOn == 1)// �����������������۸�����
                    m_objViewer.ctlDataGrid1[ii, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(dt.Rows[ii]["ItemID"].ToString(), decPrice, decDiffPrice, dt.Rows[ii]["InvType"].ToString(), temp, 2000, discount, dt.Rows[ii]["ITEMOPCALCTYPE_CHR"].ToString(), CheckIsDiff(dt.Rows[ii]["ItemID"].ToString()));
                else
                    m_objViewer.ctlDataGrid1[ii, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(dt.Rows[ii]["ItemID"].ToString(), decPrice, dt.Rows[ii]["InvType"].ToString(), temp, 2000, discount, dt.Rows[ii]["ITEMOPCALCTYPE_CHR"].ToString(), false);

                m_objViewer.ctlDataGrid1[ii, 36] = dt.Rows[ii]["opchargeflg_int"].ToString();
                //if (m_mthConvertObjToDecimal(dt.Rows[ii]["Times"]) > m_objViewer.numericUpDown1.Value)//��ҩ������ֵ,������ֵ�ĸ�����
                //{
                //    m_objViewer.numericUpDown1.Value = m_mthConvertObjToDecimal(dt.Rows[ii]["Times"]);
                //}
                // ���������
                if (intDiffPriceOn == 1)
                {
                    if (blMedicine9003(m_objViewer.ctlDataGrid1[ii, 10].ToString()))
                    {
                        if (!string.IsNullOrEmpty(temp.ToString()))
                        {
                            m_objViewer.ctlDataGrid1[ii, intDiffPriceTotalCol] = Function.Round((decDiffPrice - Function.Dec(m_objViewer.ctlDataGrid1[ii, 6].ToString())) * Function.Dec(dt.Rows[ii]["quantity"].ToString()) * m_objViewer.numericUpDown1.Value, 2);
                        }
                        else
                        {
                            m_objViewer.ctlDataGrid1[ii, intDiffPriceTotalCol] = Function.Round(Function.Dec(dt.Rows[ii]["toldiffprice_mny"].ToString()), 2);
                        }
                    }
                    else
                    {
                        m_objViewer.ctlDataGrid1[ii, intDiffPriceTotalCol] = 0;
                    }
                    //((m_mthConvertObjToDecimal(dt.Rows[ii]["tradeprice_mny"]) - m_mthConvertObjToDecimal(dt.Rows[ii]["price"])) * temp).ToString("0.00"); //this.objCalPatientCharge.m_mthGetChargeIetmPrice(dt.Rows[ii]["ItemID"].ToString(), decPrice, decDiffPrice, dt.Rows[ii]["InvType"].ToString(), temp, 2000, discount, dt.Rows[ii]["ITEMOPCALCTYPE_CHR"].ToString()); // dt.Rows[ii]["toldiffprice_mny"].ToString();
                }
                else
                {
                    m_objViewer.ctlDataGrid1[ii, intDiffPriceTotalCol] = 0;
                }
                m_objViewer.ctlDataGrid1[ii, intDiffUnitPriceCol] = decDiffPrice;

                this.m_objViewer.IsSave = false;
                this.m_objViewer.rowNO = ii;
            }
            this.m_mthDisplayCharge();

            m_objViewer.ctlDataGrid1.Select();
            m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(m_objViewer.ctlDataGrid1.RowCount, 0);

            BathCalc = false;
        }
        private void m_mthSetButtonEnable()
        {
            clsRecipeInfo_VO obj = this.m_objViewer.txtLoadRecipeNO.RecipeInfo;
            if (obj.m_strRECIPEFLAG_INT.Trim() == "2")//����������
            {
                m_objViewer.m_cmbRecipeType.SelectedIndex = 1;
            }
            else
            {
                m_objViewer.m_cmbRecipeType.SelectedIndex = 0;
            }
            if (obj.m_intPSTATUS_INT == 1 || obj.m_intPSTATUS_INT == 4)
            {
                this.m_mthSetControlEnable(true);
            }
            else
            {
                this.m_mthSetControlEnable(false);
            }

            //m_objViewer.ctlDataGrid1.Enabled = !IsCanModify;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[0]).ReadOnly = IsCanModify;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[1]).ReadOnly = IsCanModify;
        }
        /// <summary>
        /// ���ÿؼ��Ƿ����
        /// </summary>
        /// <param name="flag"></param>
        private void m_mthSetControlEnable(bool flag)
        {
            m_objViewer.btOK.Enabled = flag;
            m_objViewer.btSave.Enabled = flag;
            this.m_objViewer.m_PatientBasicInfo.txtRegisterDept.Enabled = flag;
            this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Enabled = flag;
            IsReadOnly = flag;
            m_objViewer.btReUse.Enabled = !flag;
        }
        #endregion

        #region ���ù����DataGrid�ؼ�
        private void m_mthClearEmptyRow()
        {
            for (int i = m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                //				if(m_objViewer.ctlDataGrid1.RowCount==1&&m_objViewer.ctlDataGrid1[0,12].ToString().Trim()=="")
                //				{
                //					break;
                //				}
                if (m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() == "" || m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() == "")
                {
                    m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                }

            }
        }
        public void m_mthSetFocusOnDataGrid()
        {
            if (this.m_objViewer.ctlDataGrid1.Enabled == false)
            {
                this.m_objViewer.m_PatientBasicInfo.txtCardID.Focus();
                return;
            }
            m_mthClearEmptyRow();
            int row = m_objViewer.ctlDataGrid1.RowCount;
            m_objViewer.ctlDataGrid1.Select();
            m_objViewer.ctlDataGrid1.Focus();
            m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, 0);
            SendKeys.SendWait("{Right}");
            SendKeys.SendWait("{Left}");
        }
        #endregion

        #region �жϷ�Ʊ�Ƿ��ǵ�ǰ����������
        public bool IsOccupied(string InvoiceNO)
        {
            if (InvoiceNO.Trim() == "")
            {
                return true;
            }
            clsT_opr_opinvoiceman_VO[] p_objResultArr = new clsT_opr_opinvoiceman_VO[0];
            long lngRes = new clsDcl_InvoiceManage().m_lngGetApplyInvoice("", "", this.m_objViewer.LoginInfo.m_strEmpID, 0, out p_objResultArr);
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                if (int.Parse(InvoiceNO) > int.Parse(m_mtRemoveString(p_objResultArr[i].m_strINVOICENOFROM_VCHR)) && int.Parse(InvoiceNO) < int.Parse(m_mtRemoveString(p_objResultArr[i].m_strINVOICENOTO_VCHR)))
                {
                    return true;
                }
            }
            return false;

        }
        private string m_mtRemoveString(string str)
        {
            for (int i = str.Length - 1; i > -1; i--)
            {
                if (!char.IsNumber(str, i))
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }
        #endregion

        #region ת��������
        private decimal m_mthConvertObjToDecimal(object obj)
        {
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToDecimal(obj.ToString());

            }
            else
            {
                return 0;
            }
        }
        private decimal m_mthConvertObjToDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region ��������ת��
        /// <summary>
        /// ����������ת��
        /// ҵ������:���ı䲡�����ʱ��Ҫ�����Բ������ID����һ���µļƷ��ࡣ
        /// Ȼ��ѭ��DataGrid����Ŀ���¼�����Ŀ��Ǯ��
        /// �Ʒ�������ṩ�˷�����m_mthGetDiscountByID����
        /// </summary>
        public void m_mthPatientTypeChanged()
        {
            objCalPatientCharge = null;
            objCalPatientCharge = new clsCalcPatientCharge(m_objViewer.m_PatientBasicInfo.PatientID, m_objViewer.m_PatientBasicInfo.PayTypeID, m_objViewer.m_PatientBasicInfo.Limit, this.m_objComInfo.m_strGetHospitalTitle(), m_objViewer.m_PatientBasicInfo.PatientType, m_objViewer.m_PatientBasicInfo.Discount);
            //�����ұߵ���ʾ�ؼ���
            if (this.m_objViewer.panel2.Controls.Count > 0)
            {
                this.objCalPatientCharge.GetDisplayControl = this.m_objViewer.panel2.Controls[0];
            }
            else
            {
                this.m_objViewer.panel2.Controls.Add(this.objCalPatientCharge.GetDisplayControl);
            }

            if (this.m_objViewer.chkDefaultItem.Checked)
            {
                IsChrgFlag = false;
                this.m_mthGetDefaultItem();
            }

            decimal discount;
            decimal count;
            //ѭ��DataGrid����Ŀ���¼�����Ŀ��Ǯ(Ĭ�ϴ���������)

            ArrayList objItemArr = new ArrayList();
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 12] != null && this.m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() != "" && this.m_objViewer.ctlDataGrid1[i, DefaultCol].ToString().Trim() != "&")
                {
                    objItemArr.Add(this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim());
                }
            }

            Dictionary<string, string> hasItemScale = new Dictionary<string, string>();
            if (objItemArr.Count > 0)
            {
                //string[] strItemIDArr = new string[objItemArr.Count];
                //for (int i = 0; i < objItemArr.Count; i++)
                //{
                //    strItemIDArr[i] = objItemArr[i].ToString();
                //}
                string[] strItemIDArr = (string[])objItemArr.ToArray(typeof(string));

                clsDcl_DoctorWorkstation objSvcDoct = new clsDcl_DoctorWorkstation();
                objSvcDoct.m_lngGetItemScaleByArr(this.m_objViewer.m_PatientBasicInfo.PayTypeID, strItemIDArr, ref hasItemScale);
                objSvcDoct = null;
            }


            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 12] != null && this.m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() != "" && this.m_objViewer.ctlDataGrid1[i, DefaultCol].ToString().Trim() != "&")
                {
                    discount = Convert.ToDecimal(hasItemScale[this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim()].ToString());
                    this.m_objViewer.ctlDataGrid1[i, 14] = discount;
                    this.m_objViewer.ctlDataGrid1[i, 13] = discount.ToString() + "%";
                    count = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]);
                    //������в�ҩ�Ļ�����Ҫ���Ϸ��������㡣
                    //��Ϊÿ��ҽԺ���в�ҩ�ķ�Ʊ���඼��һ������Ϊ�ڼƷ��ඨ�����в�ҩ�ķ�Ʊ����ID
                    if (this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim() == this.objCalPatientCharge.InvoiceCatID)
                    {
                        count = count * this.m_objViewer.numericUpDown1.Value;
                    }
                    string strcat = m_objViewer.ctlDataGrid1[i, 15].ToString().Trim();
                    if (this.intDiffPriceOn == 1)
                        m_objViewer.ctlDataGrid1[i, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim(), m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]), m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 38]), this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim(), count, 2000, discount, strcat, CheckIsDiff(i));
                    else
                        m_objViewer.ctlDataGrid1[i, 12] = this.objCalPatientCharge.m_mthGetChargeIetmPrice(this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim(), m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]), this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim(), count, 2000, discount, strcat, false);
                    this.m_objViewer.ctlDataGrid1[i, 23] = discount / 100 * this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                }
                else
                {
                    continue;
                }
            }

            this.m_mthSetcardtype();
            this.m_mthDisplayCharge();
        }
        #endregion

        #region ���û�������(��/��)
        /// <summary>
        /// ���û�������(��/��)
        /// </summary>
        public void m_mthSetcardtype()
        {
            switch (this.m_objViewer.m_PatientBasicInfo.PatientType)
            {
                case 1:
                    this.m_objViewer.lblCardtype.Text = "���ѱ��:";
                    break;
                case 2:
                    this.m_objViewer.lblCardtype.Text = "ҽ�����:";
                    break;
                case 3:
                    this.m_objViewer.lblCardtype.Text = "�������:";
                    break;
                case 4:
                    this.m_objViewer.lblCardtype.Text = "���ݱ��:";
                    break;
                case 5:
                    this.m_objViewer.lblCardtype.Text = "ҽ��֤��:";
                    break;
                default:
                    this.m_objViewer.lblCardtype.Text = "��ͨ:";
                    break;
            }

            if (this.m_objViewer.m_PatientBasicInfo.PatientID.Trim() != "")
            {
                this.m_objViewer.txtInsuranceID.Text = objSvc.m_strGetpatientidentityno(this.m_objViewer.m_PatientBasicInfo.PatientID, this.m_objViewer.m_PatientBasicInfo.PayTypeID);
            }
        }
        #endregion

        #region ��鷢Ʊ���Ƿ�����
        /// <summary>
        /// ��鷢Ʊ���Ƿ�����
        /// </summary>
        /// <returns></returns>
        public bool m_mthCheckInvoice()
        {
            if (this.m_objViewer.m_txtInvoiceNO.Text.Trim() == "")
            {
                MessageBox.Show("��Ʊ�Ų���Ϊ��!");
                this.m_objViewer.m_txtInvoiceNO.Select();
                this.m_objViewer.m_txtInvoiceNO.SelectAll();
                return true;
            }

            if (objSvc.m_mthCheckInvoice(this.m_objViewer.m_txtInvoiceNO.Text.Trim()))
            {
                if (!this.isCanDo)
                {
                    MessageBox.Show("��Ʊ���Ѿ�ռ��,���޸�!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    if (MessageBox.Show("��Ʊ���Ѿ�ռ��,�Ƿ����?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.m_objViewer.m_txtInvoiceNO.Select();
                        this.m_objViewer.m_txtInvoiceNO.SelectAll();
                        return true;
                    }
                }

            }
            return false;
        }
        #endregion

        #region ��鷢Ʊ���Ƿ��ǵ�ǰ�շ�Ա����
        /// <summary>
        /// ��鷢Ʊ���Ƿ��ǵ�ǰ�շ�Ա����
        /// </summary>
        /// <returns></returns>
        internal bool m_blnCheckInvoice()
        {
            if (clsPublic.m_intGetSysParm("0100") == 0)
            {
                return true;
            }
            bool blnPass = false;
            string strInvoiceNo = m_objViewer.m_txtInvoiceNO.Text.Trim();
            blnPass = objSvc.m_blnCheckInvoice(m_objViewer.LoginInfo.m_strEmpID, strInvoiceNo);
            if (!blnPass)
            {
                MessageBox.Show("����ķ�Ʊ�Ų�����������", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_txtInvoiceNO.SelectAll();
            }
            return blnPass;
        }
        #endregion

        #region �ж��Ƿ�ҩƷ
        /// <summary>
        /// �ж��Ƿ�ҩƷ�Ͳ�����Ŀ
        /// </summary>
        /// <param name="strCatID"></param>
        /// <returns></returns>
        public bool m_mthIsMedicine(string strCatID)
        {
            bool ret = false;
            if (objCalPatientCharge.m_mthIsMedicine(strCatID) > 0)
            {
                ret = true;
            }
            return ret;
        }
        #endregion

        #region �ж���ĳ��ҩƷ��ҩƷ�����Ƿ���9003��
        /// <summary>
        /// �ж���ĳ��ҩƷ��ҩƷ�����Ƿ���9003��
        /// </summary>
        /// <param name="p_strMedicineId"></param>
        /// <returns></returns>
        public bool blMedicine9003(string p_strMedicineId)
        {
            bool blMedicine = false;
            blMedicine = objSvc.blMedicine9003(p_strMedicineId);
            return blMedicine;
        }
        #endregion

        #region ���Ҷ�Ӧ����Ϣ
        private DataTable dt_RelationInfo;
        /// <summary>
        /// ���ݷ�Ʊ���������ַ��ࡣ
        /// ҵ������ϵͳ�����˼�������������Ʊ�����Ӧ��0001-��ҩ��0002-�в�ҩ��0003-���飬0004-��飬0005-����\���ƣ�0005-����
        /// ����������Ϊ�ˣ�����������ϸ���浽��Ӧ�ı��С��и�����ά����ϵ��
        /// </summary>
        /// <param name="strCatID"></param>
        /// <returns></returns>
        private string m_mthRelationInfo(string strCatID)
        {
            string str = "0005";//Ĭ������
            for (int i = 0; i < this.dt_RelationInfo.Rows.Count; i++)
            {
                if (strCatID == this.dt_RelationInfo.Rows[i]["CATID_CHR"].ToString().Trim())
                {
                    str = this.dt_RelationInfo.Rows[i]["GROUPID_CHR"].ToString().Trim();
                    break;
                }
            }
            return str;
        }
        #endregion

        #region ��ӡ����
        public void m_mthBeginPrint()
        {
            if (this.strSendMedicinePrinterName.Trim() != "")
            {
                this.m_objViewer.printDocument1.DefaultPageSettings.PrinterSettings.PrinterName = this.strSendMedicinePrinterName;
            }
            clsOutpatientPrintRecipe_VO obj_VO = new clsOutpatientPrintRecipe_VO();
            //�г�,��
            decimal decWMedicineCost = 0;
            //�в�ҩ
            decimal decZCMedicineCost = 0;
            //����
            decimal decCureCost = 0;
            //����
            decimal decOther = 0;

            obj_VO.m_strAge = this.m_objViewer.m_PatientBasicInfo.PatientAge.Trim();
            obj_VO.m_strPatientType = this.m_objViewer.m_PatientBasicInfo.txtType.Text;
            obj_VO.m_strCardID = this.m_objViewer.m_PatientBasicInfo.PatientCardID.Trim();
            obj_VO.m_strDiagDeptID = this.m_objViewer.m_PatientBasicInfo.txtRegisterDept.Text.Trim();
            obj_VO.m_strDiagDrName = this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Text.Trim();
            obj_VO.m_strHospitalName = this.m_objComInfo.m_strGetHospitalTitle();
            obj_VO.m_strPatientName = this.m_objViewer.m_PatientBasicInfo.PatientName;
            if (this.m_objViewer.btSave.Tag != null)
            {
                obj_VO.m_strRecipeID = this.m_objViewer.btSave.Tag.ToString().Trim();
            }
            else
            {
                obj_VO.m_strRecipeID = "";
            }
            clsHISInfoDefine_VO objResult;
            long l = this.m_objComInfo.m_strGetHospitalInfo(out objResult);
            if (l > 0 && objResult != null)
            {
                obj_VO.m_strAddress = objResult.m_strADDRESS_VCHR + "+" + objResult.m_strPHONE_NUMBER_CHR + "/" + objResult.m_strPHONE_NUMBER2_CHR;
            }
            else
            {
                obj_VO.m_strAddress = "";
            }
            obj_VO.m_strRecipeType = this.m_objViewer.m_cmbRecipeType.Text;
            obj_VO.m_strdiagnose = "";
            obj_VO.m_strGOVCARD = this.m_objViewer.m_PatientBasicInfo.PatientGOVCARD;
            obj_VO.m_strINSURANCEID = this.m_objViewer.m_PatientBasicInfo.PatientINSURANCEID;
            string strEmployee = "0001";//Ա��ID
            if (this.m_objViewer.LoginInfo != null)
            {
                strEmployee = this.m_objViewer.LoginInfo.m_strEmpNo;
            }
            obj_VO.m_strRecordEmpID = strEmployee;//Ա��ID
            obj_VO.m_strRegisterID = this.m_objViewer.m_PatientBasicInfo.RegisterID;
            obj_VO.m_strPrintDate = this.m_objViewer.m_PatientBasicInfo.RegisterDate;
            obj_VO.m_strSex = this.m_objViewer.m_PatientBasicInfo.PatientSex;
            obj_VO.m_strSelfPay = "";
            obj_VO.m_strChargeUp = "";
            obj_VO.m_strRecipePrice = "";
            obj_VO.m_strSelfPay = "";
            if (this.m_objViewer.numericUpDown1.Tag != null)
            {
                obj_VO.m_strHerbalmedicineUsage = this.m_objViewer.numericUpDown1.Tag.ToString();
            }
            else
            {
                obj_VO.m_strHerbalmedicineUsage = "";
            }
            obj_VO.m_strTimes = this.m_objViewer.numericUpDown1.Value.ToString();
            int count = this.m_objViewer.ctlDataGrid1.RowCount;
            List<clsOutpatientPrintRecipeDetail_VO> objPRDArr = new List<clsOutpatientPrintRecipeDetail_VO>();
            int CMTemp = 0;//��ҩ��Ŀ��ǰλ��
            List<clsOutpatientPrintRecipeDetail_VO> objPRDArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 12].ToString().Trim() != "")
                {
                    clsOutpatientPrintRecipeDetail_VO objRDVO = new clsOutpatientPrintRecipeDetail_VO();
                    objRDVO.m_strChargeName = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim();
                    objRDVO.m_strCount = this.m_objViewer.ctlDataGrid1[i, 1].ToString().Trim();
                    objRDVO.m_strDosage = this.m_objViewer.ctlDataGrid1[i, 19].ToString().Trim() + this.m_objViewer.ctlDataGrid1[i, 21].ToString().Trim();
                    objRDVO.m_strUsage = this.m_mthFindUsageNameByID(this.m_objViewer.ctlDataGrid1[i, 17].ToString().Trim());
                    objRDVO.m_strPrice = this.m_objViewer.ctlDataGrid1[i, 6].ToString().Trim();
                    objRDVO.m_strRowNo = this.m_objViewer.ctlDataGrid1[i, 16].ToString().Trim();
                    objRDVO.m_strSpec = this.m_objViewer.ctlDataGrid1[i, 4].ToString().Trim();
                    objRDVO.m_strSumPrice = this.m_objViewer.ctlDataGrid1[i, 7].ToString().Trim();
                    objRDVO.m_strUnit = this.m_objViewer.ctlDataGrid1[i, 5].ToString().Trim();
                    objRDVO.m_strInvoiceCat = this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim();
                    //������������¼�Ը�����
                    objRDVO.m_strDays = this.m_objViewer.ctlDataGrid1[i, 13].ToString().Trim();
                    switch (this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim())
                    {
                        case "0001":
                            objPRDArr.Add(objRDVO);
                            decWMedicineCost += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                            break;
                        case "0002":
                            objRDVO.m_strDosage = this.m_objViewer.ctlDataGrid1[i, 1].ToString().Trim() + this.m_objViewer.ctlDataGrid1[i, 5].ToString().Trim();
                            objPRDArr2.Add(objRDVO);
                            decZCMedicineCost += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                            break;
                        case "0003":
                            if (obj_VO.objinjectArr2 == null)
                            {
                                obj_VO.objinjectArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                            }
                            obj_VO.objinjectArr2.Add(objRDVO);
                            decCureCost += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                            break;
                        case "0006":
                            if (obj_VO.objinjectArr2 == null)
                            {
                                obj_VO.objinjectArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                            }
                            obj_VO.objinjectArr2.Add(objRDVO);
                            decCureCost += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                            break;
                        default:
                            if (obj_VO.objinjectArr == null)
                            {
                                obj_VO.objinjectArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                            }
                            obj_VO.objinjectArr.Add(objRDVO);
                            decOther += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                            break;
                    }

                }
            }
            obj_VO.m_strWMedicineCost = decWMedicineCost.ToString();
            obj_VO.m_strSelfPay = ((decimal)(decWMedicineCost + decOther + decCureCost)).ToString();
            obj_VO.m_strCMedicineCost = decOther.ToString();
            obj_VO.m_strZCMedicineCost = decZCMedicineCost.ToString();
            obj_VO.m_strCureCost = decCureCost.ToString();
            objPRDArr.Sort(0, objPRDArr.Count, null);
            obj_VO.objPRDArr = objPRDArr;
            objPRDArr2.Sort(0, objPRDArr2.Count, null);
            obj_VO.objPRDArr2 = objPRDArr2;
            objCalPatientCharge.UseFlag = 2;
            objCalPatientCharge.PrintRecipeVOInfo = obj_VO;

            //			objCalPatientCharge.m_mthPrintRecipe(e,obj_VO);


        }
        public void m_mthPrintRecipe(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.m_objViewer.m_PatientBasicInfo.PatientID.Trim() == "" || this.m_objViewer.ctlDataGrid1.RowCount == 0)
            {
                e.Cancel = true;
                return;
            }
            #region �ռ�����
            //			clsOutpatientPrintRecipe_VO obj_VO=new clsOutpatientPrintRecipe_VO();
            //			obj_VO.m_strAge=this.m_objViewer.m_PatientBasicInfo.PatientAge.Trim();
            //			obj_VO.m_strCardID=this.m_objViewer.m_PatientBasicInfo.PatientCardID.Trim();
            //			obj_VO.m_strDiagDeptID=this.m_objViewer.m_PatientBasicInfo.txtRegisterDept.Text.Trim();
            //			obj_VO.m_strDiagDrName=this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Text.Trim();
            //			obj_VO.m_strHospitalName=this.m_objComInfo.m_strGetHospitalTitle();
            //			obj_VO.m_strPatientName=this.m_objViewer.m_PatientBasicInfo.PatientName;
            //			if(this.m_objViewer.btSave.Tag!=null)
            //			{
            //				obj_VO.m_strRecipeID=this.m_objViewer.btSave.Tag.ToString().Trim();
            //			}
            //			else
            //			{
            //			obj_VO.m_strRecipeID= "";
            //			}
            //			string strEmployee ="0001";//Ա��ID
            //			if(this.m_objViewer.LoginInfo!=null)
            //			{
            //				strEmployee =this.m_objViewer.LoginInfo.m_strEmpID;
            //			}
            //			obj_VO.m_strRecordEmpID=strEmployee;//Ա��ID
            //			obj_VO.m_strRegisterID=this.m_objViewer.m_PatientBasicInfo.RegisterID;
            //			obj_VO.m_strPrintDate=this.m_objViewer.m_PatientBasicInfo.RegisterDate;
            //			obj_VO.m_strSex=this.m_objViewer.m_PatientBasicInfo.PatientSex;
            //			clsPatientChargeCal objPC=this.objCalPatientCharge.m_mthGetChargeTypeDetail();
            //			obj_VO.m_strSelfPay=objPC.m_decPersonCost.ToString();//�Ը�
            //			obj_VO.m_strChargeUp=objPC.m_decChargeUpCost.ToString();//����
            //			obj_VO.m_strRecipePrice=objPC.m_decTotalCost.ToString();//�ܶ�
            //			int count=this.m_objViewer.ctlDataGrid1.RowCount;
            //			clsOutpatientPrintRecipeDetail_VO[] objPRDArr=new clsOutpatientPrintRecipeDetail_VO[count];
            //			
            //			for(int i=0;i<this.m_objViewer.ctlDataGrid1.RowCount;i++)
            //			{
            //				if(this.m_objViewer.ctlDataGrid1[i,12].ToString().Trim()!="")
            //				{
            //					objPRDArr[i]=new clsOutpatientPrintRecipeDetail_VO();
            //					objPRDArr[i].m_strChargeName=this.m_objViewer.ctlDataGrid1[i,2].ToString().Trim();
            //					objPRDArr[i].m_strCount=this.m_objViewer.ctlDataGrid1[i,1].ToString().Trim();
            //					objPRDArr[i].m_strPrice=this.m_objViewer.ctlDataGrid1[i,6].ToString().Trim();
            //					objPRDArr[i].m_strSumPrice=this.m_objViewer.ctlDataGrid1[i,7].ToString().Trim();
            //					objPRDArr[i].m_strUnit=this.m_objViewer.ctlDataGrid1[i,5].ToString().Trim();
            //					objPRDArr[i].m_strUsage="";
            //					
            //				}
            //			}
            //			
            //			obj_VO.objPRDArr=objPRDArr;
            //			clsPrintRecipe obj =new clsPrintRecipe(e,obj_VO);
            //			obj.m_mthBegionPrint();
            this.objCalPatientCharge.m_mthPrintRecipe(e, "");
            #endregion
        }
        #endregion

        #region ����ģ���б�(����Э������)
        public void m_mthFindAccordRecipe(string strCode)
        {
            string strEmployeeID = this.m_objViewer.LoginInfo.m_strEmpID;
            frmAccordRecipe objForm = new frmAccordRecipe();
            objForm.UseFlag = 1;
            objForm.FindText = strCode;
            objForm.FindIndex = this.m_objViewer.m_cmbFind.SelectedIndex;
            if (objForm.ShowDialog() == DialogResult.OK)
            {
                int row = this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
                if ((this.m_objViewer.ctlDataGrid1[row, 10].ToString() != "" && this.m_objViewer.ctlDataGrid1[row, 1].ToString() != "") || (this.m_objViewer.ctlDataGrid1[row, 10] == null || this.m_objViewer.ctlDataGrid1[row, 10].ToString() == ""))
                {
                    if (this.m_objViewer.ctlDataGrid1[row, 12].ToString().Trim() != "")
                    {
                        int chrgrow = int.Parse(this.m_objViewer.ctlDataGrid1[row, 12].ToString());
                        this.m_mthDeleteRecipe(chrgrow);
                    }
                    this.m_objViewer.ctlDataGrid1.m_mthDeleteRow(row);
                }

                this.OverFlowFlag = false;
                this.m_mthFillDataGrid(objForm);
                this.OverFlowFlag = true;
                if (!Recsumflag)
                {
                    this.m_mthIsOverFlow();
                }
            }
        }
        /// <summary>
        /// ��Э�����������������������DataGrid
        /// </summary>
        /// <param name="objForm"></param>
        private void m_mthFillDataGrid(frmAccordRecipe objForm)
        {
            List<DataTable> lstDt = new List<DataTable>();
            if (objForm.GetTable1 != null && objForm.GetTable1.Rows.Count > 0)
                lstDt.Add(objForm.GetTable1);
            if (objForm.GetTable2 != null && objForm.GetTable2.Rows.Count > 0)
                lstDt.Add(objForm.GetTable2);
            if (objForm.GetTable3 != null && objForm.GetTable3.Rows.Count > 0)
                lstDt.Add(objForm.GetTable3);
            if (objForm.GetTable4 != null && objForm.GetTable4.Rows.Count > 0)
                lstDt.Add(objForm.GetTable4);
            if (objForm.GetTable5 != null && objForm.GetTable5.Rows.Count > 0)
                lstDt.Add(objForm.GetTable5);
            if (objForm.GetTable6 != null && objForm.GetTable6.Rows.Count > 0)
                lstDt.Add(objForm.GetTable6);
            if (lstDt.Count > 0)
            {
                foreach (DataTable dt2 in lstDt)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        DataTable dt3 = null;
                        string strpayID = "0001";
                        if (this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim() != "")
                        {
                            strpayID = this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim();
                        }
                        objSvc.m_mthFindMedicineByID("ITEMID_CHR", dr[5].ToString().Trim(), strpayID, out dt3, this.m_objViewer.LoginInfo.m_strEmpID, this.IsChildPrice);
                        if (dt3 != null && dt3.Rows.Count > 0)
                        {
                            this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                            this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                            bool ret = this.FillRowData(dt3.Rows[0], Convert.ToDecimal(dr[0]));
                            this.m_objViewer.IsSave = false;

                            string strItemID = this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 10].ToString().Trim();
                            string price = this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 6].ToString().Trim();
                            string strCatID = this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 8].ToString().Trim();
                            string strCount = this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1].ToString().Trim();
                            this.m_mthDosageChange(strItemID, price, strCatID, strCount, this.m_objViewer.rowNO);
                            if (ret) this.m_objViewer.rowNO++;
                        }
                    }
                }
            }
            this.m_mthSetFocusOnDataGrid();

            return;
            DataTable dt = null;
            dt = objForm.GetTable1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                //����������Ϊ3000��Ϊ�����շ����ڲ��ҵ���ͬ������������һ���µ���Ŀ��
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                //����ֻ�ǵ���ԭ���Ĳ�ѯ�շ���Ŀ�ķ���������շ���Ŀ��
                int ret = m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                //��ѯ�����󣬰��������Ǳ��������DataGrid��Ȼ���״̬��־Ϊ�������¼����״̬��
                //��ͨ��DataGrid��CellChanged�¼������㣬ͬ�����Ʒ����С�
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 17] = dt.Rows[i][9].ToString().Trim();//�÷�
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 18] = dt.Rows[i][11].ToString().Trim();//Ƶ��
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 19] = dt.Rows[i][17].ToString().Trim();//����
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 20] = dt.Rows[i][14].ToString().Trim();//����
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                if (ret > 0)
                {
                    this.m_objViewer.rowNO++;
                }
            }

            dt = objForm.GetTable2;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                int ret = m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                if (ret > 0)
                {
                    this.m_objViewer.rowNO++;
                }
            }

            dt = objForm.GetTable3;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                //				this.m_objViewer.rowNO =this.m_objViewer.ctlDataGrid1.RowCount-1;
                m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                this.m_objViewer.rowNO++;
            }
            //////
            dt = objForm.GetTable4;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                //				this.m_objViewer.rowNO =this.m_objViewer.ctlDataGrid1.RowCount-1;
                m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                this.m_objViewer.rowNO++;
            }
            //////
            dt = objForm.GetTable5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                //				this.m_objViewer.rowNO =this.m_objViewer.ctlDataGrid1.RowCount-1;
                m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                this.m_objViewer.rowNO++;
            }
            //////
            dt = objForm.GetTable6;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                //				this.m_objViewer.rowNO =this.m_objViewer.ctlDataGrid1.RowCount-1;
                m_mthFindMedicineByIDAcc("ITEMID_CHR", dt.Rows[i][5].ToString().Trim());
                this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = dt.Rows[i][0].ToString().Trim();
                this.m_objViewer.IsSave = false;
                this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                this.m_objViewer.rowNO++;
            }
            this.m_mthSetFocusOnDataGrid();
        }
        #endregion

        /// <summary>
        /// ָʾ�Ƿ��չ���д����
        /// </summary>
        public bool m_blnHandingRecipePayyed = false;

        #region ����Ĭ����Ŀ
        /// <summary>
        /// һ�㹫�Ѳ��˼������ѣ������Ĭ���շ���Ŀ���Ǹ�������ݵġ�����ά��Ĭ���շ���Ŀ
        /// </summary>
        public void m_mthGetDefaultItem()
        {
            this.OverFlowFlag = false;

            if (this.m_objViewer.m_PatientBasicInfo.PatientID.Trim() == "")
            {
                return;
            }

            string RecipeID = "";
            if (this.m_objViewer.btSave.Tag != null)
            {
                RecipeID = this.m_objViewer.btSave.Tag.ToString();
            }

            string RegisterID = this.m_objViewer.m_PatientBasicInfo.RegisterID.Trim();

            //��������
            string strPatientTypeID = m_objViewer.m_PatientBasicInfo.PayTypeID;
            ////�Ƿ�Һ� 1 �ѹ� �� 2 δ��
            //string strRegister = "2";
            //if (this.m_objViewer.m_PatientBasicInfo.RegisterID.Trim() != "" && this.m_objViewer.m_PatientBasicInfo.RegisterID != null)
            //{
            //    strRegister = "1";
            //}
            //if( !objSvc.m_blnCheckNormalReg(m_objViewer.m_PatientBasicInfo.RegisterID) )
            //{
            //    strRegister = "2";
            //}			

            //1 ���� �� 2 ����
            string strRecipeflag = m_objViewer.m_cmbRecipeType.Tag.ToString();

            //ְ��
            string strTechnicalRank = "";
            if (!string.IsNullOrEmpty(this.m_objViewer.m_PatientBasicInfo.DoctTechnicalRank))
            {
                strTechnicalRank = this.m_objViewer.m_PatientBasicInfo.DoctTechnicalRank;
            }
            else
            {
                strTechnicalRank = this.objSvc.m_strGetTechnicalRank(this.m_objViewer.m_PatientBasicInfo.DoctorID);
            }

            //�Ƿ�ר�� 1 �� �� 2 ����
            string strExpert = "1";
            if (!objSvc.m_blnCheckExpert(m_objViewer.m_PatientBasicInfo.DoctorID))
            {
                strExpert = "2";
            }
            //ɾ����ǰĬ����Ŀ
            m_mthGetChargeItemByItem(IsChrgFlag);

            m_mthClearEmptyRow();
            this.m_objViewer.rowNO = this.m_objViewer.ctlDataGrid1.RowCount;

            DataTable dt;

            long ret = objSvc.m_mthGetDefaultItem(out dt, strPatientTypeID, strRecipeflag, strTechnicalRank.Trim(), RecipeID, this.m_objViewer.m_PatientBasicInfo.RegisterID, this.m_objViewer.m_PatientBasicInfo.DeptID);
            if (ret > 0 && dt.Rows.Count > 0)
            {
                string[] strNoGHArr = clsPublic.m_strGetSysparm("0023").Split(';');
                string strSex = this.m_objViewer.m_PatientBasicInfo.PatientSex;
                int intAge = 1;
                try
                {
                    intAge = Convert.ToInt32(this.m_objViewer.m_PatientBasicInfo.PatientAge);
                }
                catch
                { }
                int intMaleAge = Convert.ToInt32(strNoGHArr[0]);
                int intFemaleAge = Convert.ToInt32(strNoGHArr[1]);
                bool blnIfChargeRegister = true;

                if (strSex == "��" && intAge >= intMaleAge)
                {
                    blnIfChargeRegister = false;
                }
                if (strSex == "Ů" && intAge >= intFemaleAge)
                {
                    blnIfChargeRegister = false;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ITEMID_CHR"].ToString().Trim() == "0000006423" && blnIfChargeRegister == false)
                    {
                        continue;
                    }
                    else
                    {
                        this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                        m_mthGetChargeItemByItem(dt.Rows[i]["ITEMID_CHR"].ToString().Trim(), strPatientTypeID, 1);
                        this.m_objViewer.IsSave = false;
                        this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                        this.m_objViewer.rowNO++;
                    }
                }
                m_mthClearEmptyRow();
            }
            OverFlowFlag = true;
            if (!Recsumflag)
            {
                this.m_mthIsOverFlow();
            }
        }
        #endregion

        #region �Ƿ�ѡ��
        /// <summary>
        ///���ѡ�����Ϣ
        /// </summary>
        /// <param name="objControl">�ؼ�</param>
        /// <returns>true ��ʾ�ؼ�,false����ʾ</returns>
        public bool m_mthGetFillDataToComboBox(com.digitalwave.iCare.gui.HIS.exComboBox objControl)
        {
            bool ret = true;
            if (objCHInfoVoArr == null)
            {
                ret = false;
            }
            else
            {
                for (int i = 0; i < objCHInfoVoArr.Length; i++)
                {
                    objControl.Item.Add(objCHInfoVoArr[i].strName, objCHInfoVoArr[i].strID);
                }
                objControl.SelectedIndex = 0;
            }
            return ret;
        }
        private clsChargeHospitalInfoVO[] objCHInfoVoArr = null;
        public void m_mthGetChooseHospitalInfo()
        {
            objSvc.m_mthGetChooseHospitalInfo(out this.objCHInfoVoArr);
        }
        #endregion

        #region �����÷�ID���÷�����
        private string m_mthFindUsageNameByID(string strID)
        {
            string ret = "";
            if (strID.Trim() != "")
            {
                DataTable dt;
                long l = objSvc.m_mthFindUsage(strID, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0]["USAGENAME_VCHR"].ToString().Trim();
                }
            }
            return ret;
        }
        #endregion

        #region ����Ƶ��ID��Ƶ������
        private string m_mthFindFreqNameByID(string strID)
        {
            string ret = "";
            if (strID.Trim() != "")
            {
                DataTable dt;
                long l = objSvc.m_mthFindFreq(strID, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0]["FREQNAME_CHR"].ToString().Trim();
                }
            }
            return ret;
        }
        #endregion

        #region ����ϵͳ����
        /// <summary>
        /// ����ϵͳ����
        /// </summary>
        public void m_mthGetsysparm()
        {
            this.m_blnSecondStockLimitFlag = this.objSvc.m_mthIsCanDo("9101") == 1;
            this.m_strShowOPChargeManInfo = this.objSvc.m_mthIsCanDo("9009").ToString().Trim();
            this.isCanDo = this.objSvc.m_mthIsCanDo("0007") == 1;
            this._IsPrintSendMedicineBill = this.objSvc.m_mthIsCanDo("0009") == 1;
            this.IsCanModify = this.objSvc.m_mthIsCanDo("0010") == 1;
            this.IsModifyrecipetype = this.objSvc.m_mthIsCanDo("0053") == 1;
            this.IsChargeReceiverRec = this.objSvc.m_mthIsCanDo("0056") == 1;
            this.Ismedwinpublic = this.objSvc.m_mthIsCanDo("0057") == 1;
            this.isShowLackMedicine = objSvc.m_mthIsCanDo("0012") == 1;
            this.IsCanTurn = objSvc.m_mthIsCanDo("0013") == 1;
            this.m_objViewer.m_txtInvoiceNO.MaxLength = objSvc.m_mthIsCanDo("0014");
            this.IsDiscount = objSvc.m_mthIsCanDo("0016") == 1;
            this.m_objViewer.m_PatientBasicInfo.txtType.Enabled = objSvc.m_mthIsCanDo("0027") == 1;
            if (!this.m_objViewer.m_PatientBasicInfo.txtType.Enabled)
            {
                this.m_objViewer.m_PatientBasicInfo.txtType.BackColor = SystemColors.Control;
            }

            //����շѹ���վ��־
            if (this.m_strReadXML("register", "PEWorkStation", "AnyOne") == "1")
            {
                PEWorkStationFlag = true;
            }

            clsDcl_DoctorWorkstation objDoct = new clsDcl_DoctorWorkstation();
            WMUsageIDArr = this.m_ArrGettoken(objDoct.m_strGetSysparm("1001"), ";");
            CMUsageIDArr = this.m_ArrGettoken(objDoct.m_strGetSysparm("1002"), ";");
            MATUsageIDArr = this.m_ArrGettoken(objDoct.m_strGetSysparm("1003"), ";");
            m_objNeedCheckArr = this.m_ArrGettoken(objDoct.m_strGetSysparm("1200"), ";");
            YBType = objDoct.m_strGetSysparm("1000");
            RoundingRule = objDoct.m_strGetSysparm("0015");
            RoundingCode = objDoct.m_strGetSysparm("0016");

            arrPaytype = this.m_ArrGettoken(objDoct.m_strGetSysparm("0001"), ";");

            this.YBIsShowSelfItem = (objSvc.m_mthIsCanDo("0066") == 1);
            this.YBPayTypeArr = this.m_ArrGettoken(objDoct.m_strGetSysparm("0031"), ";");
            this.m_objViewer.BirthInsuranceCode = objDoct.m_strGetSysparm("0084");
            this.m_objViewer.Covi19Code = objDoct.m_strGetSysparm("0085");
            objDoct = null;

            #region ��ȡ����ҽ��ǰ�����ݿ����
            string xf = XMLFile;
            XMLFile = System.Windows.Forms.Application.StartupPath + @"\HISYB.xml";

            this.Hospcode = this.m_strReadXML("DONGGUAN.CHASHAN", "HospitalNO", "AnyOne");
            string DSN = this.m_strReadXML("DONGGUAN.CHASHAN", "DSN", "AnyOne");
            DBQ = this.m_strReadXML("DONGGUAN.CHASHAN", "DBQ", "AnyOne");

            SQLParm = DSN + DBQ;

            XMLFile = xf;
            #endregion

            m_intFlag = clsPublic.m_intGetSysParm("0077");//�����շѴ��Ƿ��Ƿ����������ҩ��Ϣ
            intDiffPriceOn = clsPublic.m_intGetSysParm("9002");// �������ÿ���
            if (intDiffPriceOn == 0)
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[intDiffPriceTotalCol]).ColumnWidth = 0;
            else
                ((com.digitalwave.controls.datagrid.clsColumnInfo)this.m_objViewer.ctlDataGrid1.Columns[intDiffPriceTotalCol]).ColumnWidth = 80;

            this.isUseChildPrice = (new clsDcl_YB()).IsUseChildPrice();
        }
        #endregion

        #region ���ô���
        /// <summary>
        /// ���ô���
        /// </summary>
        /// <returns></returns>
        public bool m_blnRounding()
        {
            // �籣���˲�����
            if (this.m_Isybcharge())
            {
                return true;
            }

            if (RoundingRule == "0")
            {
                return true;
            }

            string totalmny, acctmny, sbmny;
            objCalPatientCharge.m_mthGetchargeinfo(out acctmny, out sbmny, out totalmny);

            if (sbmny.Trim() == "")
            {
                return true;
            }

            decimal d = Convert.ToDecimal(sbmny);
            sbmny = d.ToString("0.00");

            int val = int.Parse(sbmny.Substring(sbmny.Length - 1, 1));
            if (val > 0)
            {
                int amount = 0;

                if (RoundingRule == "1")
                {
                    if (val < 5)
                    {
                        amount = -1 * val;
                    }
                    else
                    {
                        amount = 10 - val;
                    }
                }

                DataTable dt;
                long l = this.objSvc.m_lngGetRoundingItem(RoundingCode, out dt, this.IsChildPrice);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    for (int i = this.m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
                    {
                        if (this.m_objViewer.ctlDataGrid1[i, 10] == null || this.m_objViewer.ctlDataGrid1[i, 10].ToString() == "" || this.m_objViewer.ctlDataGrid1[i, 1].ToString() == "")
                        {
                            this.m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                        }
                    }

                    int row = this.m_objViewer.ctlDataGrid1.RowCount;
                    this.m_objViewer.ctlDataGrid1.m_mthAppendRow();

                    m_objViewer.ctlDataGrid1[row, 0] = dr["TempItemCode"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 1] = amount.ToString();
                    m_objViewer.ctlDataGrid1[row, 2] = dr["ITEMNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString().Trim());
                    m_objViewer.ctlDataGrid1[row, 4] = dr["ITEMSPEC_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 5] = dr["itemunit_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dr["ITEMPRICE_MNY"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 8] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 9] = m_mthRelationInfo(dr["ITEMOPINVTYPE_CHR"].ToString().Trim());
                    m_objViewer.ctlDataGrid1[row, 10] = dr["ITEMID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 11] = "";
                    m_objViewer.ctlDataGrid1[row, 15] = dr["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 16] = 0;
                    m_objViewer.ctlDataGrid1[row, 17] = "";
                    m_objViewer.ctlDataGrid1[row, 18] = "";
                    m_objViewer.ctlDataGrid1[row, 21] = "";
                    m_objViewer.ctlDataGrid1[row, "colYbcode"] = dr["insuranceid_chr"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 13] = "100%";
                    m_objViewer.ctlDataGrid1[row, 14] = 100;
                    m_objViewer.ctlDataGrid1[row, 12] = 3000;

                    this.m_mthDosageChange(dr["ITEMID_CHR"].ToString().Trim(), dr["ITEMPRICE_MNY"].ToString().Trim(), dr["ITEMOPINVTYPE_CHR"].ToString().Trim(), amount.ToString(), row);

                    objCalPatientCharge.m_mthModifyInvoCat();
                    this.m_mthDisplayCharge();
                }
            }

            return true;
        }
        #endregion

        #region �������õķѱ��ж��Ƿ�ʹ��ҽ������
        /// <summary>
        /// �������õķѱ��ж��Ƿ�ʹ��ҽ������
        /// </summary>
        /// <returns></returns>
        public bool m_Isybcharge()
        {
            if (this.arrPaytype == null)
            {
                return false;
            }

            return arrPaytype.Contains(this.m_objViewer.m_PatientBasicInfo.PayTypeID);
        }
        #endregion

        #region  ��ȡҩƷ�������
        /// <summary>
        /// ��ȡҩƷ��������� 
        /// </summary>
        /// <returns></returns>
        internal decimal m_decGetTotalDiffPrice()
        {
            decimal decTotalMoney = 0;//�ܼ�

            string strType = string.Empty;//����
            com.digitalwave.controls.datagrid.ctlDataGrid objGrid = m_objViewer.ctlDataGrid1;
            int intRowsCount = objGrid.RowCount;
            for (int i1 = 0; i1 < intRowsCount; i1++)
            {
                if (this.blMedicine9003(objGrid[i1, 10].ToString().Trim()))
                    decTotalMoney += m_mthConvertObjToDecimal(objGrid[i1, intDiffPriceTotalCol]);
            }
            decTotalMoney = Convert.ToDecimal(decTotalMoney.ToString("0.00"));
            return decTotalMoney;
        }
        #endregion

        #region �ռ���ҩ��Ϣ
        private clsReportSendMedStart_VO ReportSendMedStart = null;
        private clsReportSendMed_VO[] sendMedEN = null;
        private clsReportSendMed_VO[] sendMedCH = null;
        private clsContorlReportPrint objprint = null;
        public void m_mthBegionPrintMedicineSend()
        {
            if (this.strSendMedicinePrinterName.Trim() != "")
            {
                this.m_objViewer.printDocument2.DefaultPageSettings.PrinterSettings.PrinterName = this.strSendMedicinePrinterName;
            }
            objprint = new clsContorlReportPrint();
            ReportSendMedStart = new clsReportSendMedStart_VO();
            ReportSendMedStart.m_intFlag = 0;
            ReportSendMedStart.m_intIsShow = 1;
            ReportSendMedStart.m_strAge = this.m_objViewer.m_PatientBasicInfo.PatientAge + "��";
            ReportSendMedStart.m_strDoctorName = this.m_objViewer.m_PatientBasicInfo.txtRegisterDoctor.Text;
            ReportSendMedStart.m_strname = this.m_objViewer.m_PatientBasicInfo.PatientName;
            ReportSendMedStart.m_strInternalCH = this.m_objViewer.m_PatientBasicInfo.txtType.Text;
            ReportSendMedStart.m_strInternalEN = this.m_objViewer.m_PatientBasicInfo.txtType.Text;
            ReportSendMedStart.m_strPatCardID = this.m_objViewer.m_PatientBasicInfo.PatientCardID;
            ReportSendMedStart.m_strPintdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ReportSendMedStart.m_strRegisterdate = this.m_objViewer.m_PatientBasicInfo.RegisterDate;
            ReportSendMedStart.m_strsex = this.m_objViewer.m_PatientBasicInfo.PatientSex;
            ReportSendMedStart.m_strSun = this.m_objViewer.numericUpDown1.Value.ToString();
            ReportSendMedStart.m_strTotalMoney = this.objCalPatientCharge.m_mthGetChargeTypeDetail().m_decTotalCost.ToString();
            if (this.m_objViewer.numericUpDown1.Tag != null)
            {
                ReportSendMedStart.m_strUseNameAll = this.m_objViewer.numericUpDown1.Tag.ToString();
            }

            int ECount = 0;
            int CCount = 0;

            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0001" || this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0006")
                {
                    if (objCalPatientCharge.m_mthIsMedicine(m_objViewer.ctlDataGrid1[i, 8].ToString().Trim()) == 2)//(��������)������г�ҩ�͵���ҩ����ҩ
                    {
                        CCount++;
                    }
                    else
                    {
                        ECount++;

                    }
                    continue;
                }
                if (this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0002")
                {
                    CCount++;
                }
            }
            sendMedEN = new clsReportSendMed_VO[ECount];
            sendMedCH = new clsReportSendMed_VO[CCount];
            ECount = 0;
            CCount = 0;
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                clsReportSendMed_VO temp = new clsReportSendMed_VO();
                temp.m_strdosage = this.m_objViewer.ctlDataGrid1[i, 19].ToString();
                temp.m_strdosageUnit = this.m_objViewer.ctlDataGrid1[i, 21].ToString();
                temp.m_strMedSpace = this.m_objViewer.ctlDataGrid1[i, 4].ToString();
                temp.m_strMedName = this.m_objViewer.ctlDataGrid1[i, 2].ToString();
                temp.m_strMedUnit = this.m_objViewer.ctlDataGrid1[i, 5].ToString();
                temp.m_strTotal = this.m_objViewer.ctlDataGrid1[i, 1].ToString();
                temp.m_strUseName = m_mthFindUsageNameByID(this.m_objViewer.ctlDataGrid1[i, 17].ToString());
                temp.m_strFREQIDName = m_mthFindFreqNameByID(this.m_objViewer.ctlDataGrid1[i, 18].ToString());

                if (this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0001" || this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0006")
                {
                    if (objCalPatientCharge.m_mthIsMedicine(m_objViewer.ctlDataGrid1[i, 8].ToString().Trim()) == 2)//(��������)������г�ҩ�͵���ҩ����ҩ
                    {
                        temp.m_strdosage = this.m_objViewer.ctlDataGrid1[i, 1].ToString();
                        temp.m_strMedType = "2";
                        sendMedCH[CCount] = temp;
                        CCount++;

                    }
                    else
                    {
                        temp.m_strMedType = "1";
                        sendMedEN[ECount] = temp;
                        ECount++;
                    }
                    continue;
                }
                if (this.m_objViewer.ctlDataGrid1[i, 9].ToString().Trim() == "0002")
                {
                    temp.m_strMedType = "3";
                    temp.m_strdosage = this.m_objViewer.ctlDataGrid1[i, 1].ToString();
                    sendMedCH[CCount] = temp;
                    CCount++;
                }
            }


        }
        public void m_mthEndPrintMedicineSend()
        {
            ReportSendMedStart = null;
            sendMedEN = null;
            sendMedCH = null;
            objprint = null;
        }
        #endregion

        #region ��ӡ��ҩ��Ϣ
        public void m_mthPrintMedicineSend(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (sendMedEN.Length + sendMedCH.Length == 0)
            {
                e.Cancel = true;
            }
            else
            {
                objprint.m_lngPrint(e, ReportSendMedStart, sendMedEN, sendMedCH);
            }
        }
        #endregion

        #region ���������Ƿ񳬶�
        /// <summary>
        /// �Ƿ���ʾ�泬�� true��ʾ��false ����ʾ
        /// </summary>
        private bool OverFlowFlag = true;
        /// <summary>
        /// ���������Ƿ񳬶�
        /// </summary>
        /// <returns>true ����,false û�г���</returns>
        private bool m_mthIsOverFlow()
        {
            bool ret = false;
            //			if(this.m_objViewer.m_PatientBasicInfo.PatientType==3)
            //			{

            if (showOverFlow && this.m_objViewer.m_PatientBasicInfo.Limit > 0 && this.objCalPatientCharge.m_mthGetChargeTypeDetail().m_decTotalCost > this.m_objViewer.m_PatientBasicInfo.Limit && this.m_objViewer.m_PatientBasicInfo.PatientID.Trim() != "" && OverFlowFlag)
            {
                frmShowOverFlow objfrm = new frmShowOverFlow();
                objfrm.Left = 856;
                objfrm.Top = 668;
                objfrm.Show();
                ret = false;
            }
            //			}
            return ret;
        }
        #endregion

        #region �����س�
        public void m_mthChangeDiscount(string strText, int p_row)
        {
            if (strText.Trim() == "")
            {
                MessageBox.Show("���������!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.ctlDataGrid1[p_row, 13] = this.m_objViewer.ctlDataGrid1[p_row, 14].ToString().Trim() + "%";
                return;
            }
            decimal temp = this.m_mthConvertObjToDecimal(strText);
            if (temp < 0)
            {
                MessageBox.Show("����ֵ����С�� 0!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.ctlDataGrid1[p_row, 13] = this.m_objViewer.ctlDataGrid1[p_row, 14].ToString().Trim();
                return;
            }
            if (temp > 100)
            {
                MessageBox.Show("����ֵ���ܳ���100!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.ctlDataGrid1[p_row, 13] = this.m_objViewer.ctlDataGrid1[p_row, 14].ToString().Trim();
                return;
            }
            this.m_objViewer.ctlDataGrid1[p_row, 14] = temp;
            this.m_objViewer.IsSave = false;
            this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(p_row, 14);

        }
        #endregion

        #region �����÷����շ���Ŀ
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        private bool showOverFlow = true;

        //�����շ���Ŀ��־
        private bool blnSinglechrgitem = false;
        public void m_mthFindItemByUsage(string strID)
        {
            frmGetItemByUsage objfrm = new frmGetItemByUsage(strID);
            if (objfrm.ShowDialog() == DialogResult.OK)
            {
                showOverFlow = false;
                //				this.m_objViewer.rowNO=this.m_objViewer.ctlDataGrid1.RowCount-1;
                clsChargeItem_VO[] objResult = objfrm.ItemResult;
                if (objResult == null)
                {
                    return;
                }
                for (int i = 0; i < objResult.Length; i++)
                {
                    this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                    this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 12] = 3000;
                    blnSinglechrgitem = true;
                    int ret = m_mthFindMedicineByID("ITEMID_CHR", objResult[i].m_strItemID);
                    blnSinglechrgitem = false;
                    this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO, 1] = objResult[i].m_strUNITPRICE;
                    //					this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO,17] =dt.Rows[i][9].ToString().Trim();//�÷�
                    //					this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO,18] =dt.Rows[i][11].ToString().Trim();//Ƶ��
                    //					this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO,19] =dt.Rows[i][17].ToString().Trim();//����
                    //					this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO,20] =dt.Rows[i][14].ToString().Trim();//����
                    this.m_objViewer.IsSave = false;
                    //					this.m_objViewer.ctlDataGrid1[this.m_objViewer.rowNO,12]=3000;
                    this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                    if (ret > 0)
                    {
                        this.m_objViewer.rowNO++;
                    }
                }
                showOverFlow = true;
                if (!Recsumflag)
                {
                    this.m_mthIsOverFlow();
                }
                this.m_mthSetFocusOnDataGrid();
            }
        }
        #endregion

        #region �жϷ�Ʊ���Ƿ���ȷ
        public bool m_mthInvoiceExpression()
        {
            Regex r = new Regex(this.strInvoiceExpression);
            Match m = r.Match(this.m_objViewer.m_txtInvoiceNO.Text);
            if (m.Success)
            {
                return false;
            }
            else
            {
                MessageBox.Show("��Ʊ�Ÿ�ʽ������!", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.m_txtInvoiceNO.Focus();
                return true;
            }
        }
        #endregion

        #region ѭ����ȡ�շ���Ŀ��������Ŀ
        /// <summary>
        /// ѭ����ȡ�շ���Ŀ��������Ŀ
        /// </summary>
        /// <param name="strCurrItemID">��ǰ�շ���Ŀ</param>		
        /// <param name="intCurrRow">��ǰ�к�</param>
        /// <param name="intType">����: -1 ɾ��	0 ����</param>
        public void m_mthGetChargeItemByItem(string strCurrItemID, int intType, DataTable dtRecord)
        {
            //ɾ������Ŀ
            if (intType == -1)
            {
                for (int i = this.m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
                {
                    if (this.m_objViewer.ctlDataGrid1[i, ResubitemCol].ToString().Trim() == strCurrItemID)
                    {
                        //ɾ���Ʒ����Ӧ����Ŀ
                        if (this.m_objViewer.ctlDataGrid1[i, 10].ToString() != "" && this.m_objViewer.ctlDataGrid1[i, 1].ToString() != "")
                        {
                            int row = int.Parse(this.m_objViewer.ctlDataGrid1[i, 12].ToString());
                            ((clsCtl_OPCharge)this.m_objViewer.objController).m_mthDeleteRecipe(row);
                        }
                        this.m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                    }
                }
            }
            //��������Ŀ
            else if (intType == 0)
            {
                int row = 0;

                foreach (DataRow dtRow in dtRecord.Rows)
                {
                    //ͣ����ʾ
                    if (dtRow["IFSTOP_INT"].ToString() == "1")
                    {
                        MessageBox.Show("���������Ŀ" + "(" + dtRow["ITEMCODE_VCHR"].ToString() + ")" + dtRow["ITEMNAME_VCHR"].ToString() + "��ͣ�ã���֪ͨ����Ա�������ã�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        continue;
                    }

                    row = m_objViewer.ctlDataGrid1.RowCount;
                    m_objViewer.ctlDataGrid1.m_mthAppendRow();
                    m_objViewer.ctlDataGrid1[row, 0] = dtRow["ITEMCODE_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 1] = m_mthConvertObjToDecimal(dtRow["totalqty_dec"]);
                    m_objViewer.ctlDataGrid1[row, 2] = dtRow["ITEMNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim());
                    m_objViewer.ctlDataGrid1[row, 4] = dtRow["ITEMSPEC_VCHR"].ToString().Trim();
                    //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                    if (m_mthIsMedicine(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim()) && dtRow["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dtRow["ITEMIPUNIT_CHR"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dtRow["SUBMONEY"].ToString().Trim();
                    }
                    else
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dtRow["ITEMOPUNIT_CHR"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dtRow["ITEMPRICE_MNY"].ToString().Trim();
                    }
                    m_objViewer.ctlDataGrid1[row, 8] = dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 9] = m_mthRelationInfo(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim());
                    if (m_objViewer.ctlDataGrid1[row, 9].ToString() != "0001" && m_objViewer.ctlDataGrid1[row, 9].ToString() != "0002")//��ҩʱ��ȡ��һ��λ
                    {
                        m_objViewer.ctlDataGrid1[row, 5] = dtRow["Unit"].ToString().Trim();
                        m_objViewer.ctlDataGrid1[row, 6] = dtRow["ITEMPRICE_MNY"].ToString().Trim();
                    }

                    m_objViewer.ctlDataGrid1[row, 10] = dtRow["ITEMID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 11] = dtRow["SELFDEFINE_INT"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 15] = dtRow["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 16] = 0;
                    m_objViewer.ctlDataGrid1[row, 17] = "";
                    m_objViewer.ctlDataGrid1[row, 18] = "";
                    m_objViewer.ctlDataGrid1[row, 21] = dtRow["DOSAGEUNIT_CHR"].ToString().Trim();

                    string strPRECENT_DEC = "100";
                    if (dtRow["PRECENT_DEC"].ToString().Trim() != "")
                    {
                        strPRECENT_DEC = dtRow["PRECENT_DEC"].ToString().Trim();
                    }
                    m_objViewer.ctlDataGrid1[row, 13] = strPRECENT_DEC + "%";
                    m_objViewer.ctlDataGrid1[row, 14] = this.m_mthConvertObjToDecimal(strPRECENT_DEC);
                    m_objViewer.ctlDataGrid1[row, 12] = 2000;

                    this.m_objViewer.ctlDataGrid1[row, ResubitemCol] = strCurrItemID;
                    this.m_objViewer.ctlDataGrid1[row, ResubnumsCol] = m_objViewer.ctlDataGrid1[row, 1];
                }
                //�㵥�����ʾ�ܷ���
                this.m_mthCalReItemTotal(strCurrItemID, 1);
            }
        }
        #endregion

        #region ����������Ŀ������
        /// <summary>
        /// ����������Ŀ������
        /// </summary>
        /// <param name="strCurrItemID"></param>
        /// <param name="strOldNum"></param>
        /// <param name="strNewNum"></param>
        public void m_mthAdjustReItemNum(string strCurrItemID, string strOldNum, string strNewNum)
        {
            if (!strCurrItemID.StartsWith("[PK]"))
            {
                return;
            }

            if (strOldNum == strNewNum)
            {
                return;
            }

            double dnum = 0;
            double dscale = 0;

            if (strOldNum.Trim() == "" || strOldNum.Trim() == "0")
            {
                dnum = 1;
            }
            else
            {
                dnum = Convert.ToDouble(strOldNum);
            }

            dscale = Math.Ceiling((Convert.ToDouble(strNewNum) / dnum));

            this.m_mthCalReItemTotal(strCurrItemID.Replace("[PK]", ""), Convert.ToDecimal(dscale));
        }

        private void m_mthCalReItemTotal(string strCurrItemID, decimal scale)
        {
            decimal orgNum = 0;
            this.m_objViewer.IsSendTabKey = true;
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, ResubitemCol].ToString().Trim() == strCurrItemID)
                {
                    orgNum = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, ResubnumsCol]);
                    this.m_objViewer.ctlDataGrid1[i, 1] = orgNum * scale;

                    //�㵥�����ʾ�ܷ���
                    this.m_mthDosageChange(m_objViewer.ctlDataGrid1[i, 10].ToString().Trim(), m_objViewer.ctlDataGrid1[i, 6].ToString().Trim(),
                        m_objViewer.ctlDataGrid1[i, 8].ToString().Trim(), m_objViewer.ctlDataGrid1[i, 1].ToString().Trim(), i);

                    //�Ը�
                    if (this.m_objViewer.ctlDataGrid1[i, 14].ToString().Trim() != "")
                    {
                        this.m_objViewer.ctlDataGrid1[i, 23] = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 14]) / 100 * m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                    }
                }
            }
        }
        #endregion

        #region ѭ�������շ���Ŀ
        /// <summary>
        /// ѭ�������շ���Ŀ
        /// </summary>
        /// <param name="strItemID"></param>
        /// <param name="strPatType"></param>
        /// <param name="Flag">1 Ĭ�ϴ����� 2 ����շ���Ŀ</param>
        public void m_mthGetChargeItemByItem(string strItemID, string strPatType, int Flag)
        {
            long lngRet = 0;
            DataTable dtRecord = new DataTable();
            if (Flag == 1)
            {
                lngRet = objSvc.m_mthFindChrgItemByID(strItemID, strPatType, out dtRecord, this.IsChildPrice);
            }
            else if (Flag == 2)
            {
                lngRet = objSvc.m_lngGetPEChargeItemInfo(strItemID, strPatType, out dtRecord, this.IsChildPrice);
            }
            if (lngRet > 0 && dtRecord.Rows.Count == 1)
            {
                DataRow dtRow = dtRecord.Rows[0];

                int row = m_objViewer.ctlDataGrid1.RowCount;
                m_objViewer.ctlDataGrid1.m_mthAppendRow();
                m_objViewer.ctlDataGrid1[row, 0] = dtRow["ITEMCODE_VCHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 1] = m_mthConvertObjToDecimal(dtRow["itemnum"]);
                m_objViewer.ctlDataGrid1[row, 2] = dtRow["ITEMNAME_VCHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 3] = m_mthConvertToChType(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim());
                m_objViewer.ctlDataGrid1[row, 4] = dtRow["ITEMSPEC_VCHR"].ToString().Trim();
                //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                if (m_mthIsMedicine(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim()) && dtRow["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dtRow["ITEMIPUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dtRow["SUBMONEY"].ToString().Trim();

                }
                else
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dtRow["ITEMOPUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dtRow["ITEMPRICE_MNY"].ToString().Trim();

                }
                if (blMedicine9003(m_objViewer.ctlDataGrid1[row, 10].ToString()))
                {
                    m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = string.IsNullOrEmpty(dtRow["subtrademoney"].ToString().Trim()) ? dtRow["SUBMONEY"].ToString().Trim() : dtRow["subtrademoney"].ToString().Trim();
                }
                else
                {
                    m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = string.IsNullOrEmpty(dtRow["tradeprice_mny"].ToString().Trim()) ? dtRow["ITEMPRICE_MNY"].ToString().Trim() : dtRow["tradeprice_mny"].ToString().Trim();
                }
                m_objViewer.ctlDataGrid1[row, 8] = dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 9] = m_mthRelationInfo(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim());
                if (m_objViewer.ctlDataGrid1[row, 9].ToString() != "0001" && m_objViewer.ctlDataGrid1[row, 9].ToString() != "0002")//��ҩʱ��ȡ��һ��λ
                {
                    m_objViewer.ctlDataGrid1[row, 5] = dtRow["Unit"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, 6] = dtRow["ITEMPRICE_MNY"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, intDiffUnitPriceCol] = string.IsNullOrEmpty(dtRow["tradeprice_mny"].ToString().Trim()) ? dtRow["ITEMPRICE_MNY"].ToString().Trim() : dtRow["tradeprice_mny"].ToString().Trim();
                }

                m_objViewer.ctlDataGrid1[row, 10] = dtRow["ITEMID_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 11] = dtRow["SELFDEFINE_INT"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 15] = dtRow["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                m_objViewer.ctlDataGrid1[row, 16] = 0;
                m_objViewer.ctlDataGrid1[row, 17] = "";
                m_objViewer.ctlDataGrid1[row, 18] = "";
                m_objViewer.ctlDataGrid1[row, 21] = dtRow["DOSAGEUNIT_CHR"].ToString().Trim();

                string strPRECENT_DEC = "100";
                if (dtRow["PRECENT_DEC"].ToString().Trim() != "")
                {
                    strPRECENT_DEC = dtRow["PRECENT_DEC"].ToString().Trim();
                }
                m_objViewer.ctlDataGrid1[row, 13] = strPRECENT_DEC + "%";
                m_objViewer.ctlDataGrid1[row, 14] = this.m_mthConvertObjToDecimal(strPRECENT_DEC);
                m_objViewer.ctlDataGrid1[row, 12] = 2000;

                this.m_objViewer.ctlDataGrid1[row, ResubitemCol] = "";
                this.m_objViewer.ctlDataGrid1[row, ResubnumsCol] = 0;
                this.m_objViewer.ctlDataGrid1[row, "colYbcode"] = dtRow["insuranceid_chr"].ToString().Trim();
                this.m_objViewer.ctlDataGrid1[row, DefaultCol] = "&";

                //�㵥�����ʾ�ܷ���
                this.m_mthDosageChange(m_objViewer.ctlDataGrid1[row, 10].ToString().Trim(), m_objViewer.ctlDataGrid1[row, 6].ToString().Trim(),
                    m_objViewer.ctlDataGrid1[row, 8].ToString().Trim(), m_objViewer.ctlDataGrid1[row, 1].ToString().Trim(), row);

                //�Ը�
                if (this.m_objViewer.ctlDataGrid1[row, 14].ToString().Trim() != "")
                {
                    this.m_objViewer.ctlDataGrid1[row, 23] = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[row, 14]) / 100 * m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[row, 7]);
                }
            }
        }
        #endregion

        #region ѭ������Ĭ�ϴ�����Ŀ
        /// <summary>
        /// ѭ������Ĭ�ϴ�����Ŀ
        /// </summary>
        /// <param name="strItemID"></param>
        /// <param name="strPatType"></param>
        public void m_mthGetChargeItemByItem(bool blnFlag)
        {
            for (int i = this.m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGrid1[i, DefaultCol].ToString().Trim() == "&")
                {
                    if (blnFlag)
                    {
                        //ɾ���Ʒ����Ӧ����Ŀ
                        if (this.m_objViewer.ctlDataGrid1[i, 10].ToString() != "" && this.m_objViewer.ctlDataGrid1[i, 1].ToString() != "")
                        {
                            int row = int.Parse(this.m_objViewer.ctlDataGrid1[i, 12].ToString());
                            ((clsCtl_OPCharge)this.m_objViewer.objController).m_mthDeleteRecipe(row);
                        }
                    }
                    this.m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                }
            }
        }
        #endregion

        #region �������ҽ��
        /// <summary>
        /// �������ҽ��
        /// </summary>
        public void m_mthDoctorChanged()
        {
            if (objCalPatientCharge == null)
            {
                return;
            }

            if (objSvc.m_blnValidatePatientRecipeByDoctor(m_objViewer.m_PatientBasicInfo.PatientID, m_objViewer.m_PatientBasicInfo.DoctorID))
            {
                m_objViewer.m_cmbRecipeType.SelectedIndex = 1;
            }
            else
            {
                m_objViewer.m_cmbRecipeType.SelectedIndex = 0;
            }

            IsChrgFlag = true;
            if (this.m_objViewer.chkDefaultItem.Checked)
            {
                this.m_mthGetDefaultItem();
            }
            this.m_mthDisplayCharge();
        }
        #endregion

        #region �������ҽ��
        /// <summary>
        /// �������ҽ��
        /// </summary>
        public void m_mthRecipeChanged()
        {
            if (objCalPatientCharge == null)
            {
                return;
            }

            IsChrgFlag = true;
            if (this.m_objViewer.chkDefaultItem.Checked)
            {
                this.m_mthGetDefaultItem();
            }
            this.m_mthDisplayCharge();
        }
        #endregion

        #region ��ʾ�Ƿ�ר��
        /// <summary>
        /// ��ʾ�Ƿ�ר��
        /// </summary>
        /// <param name="strEmpID"></param>
        public void m_mthShowExpert(string strDocID)
        {
            if (objSvc.m_blnCheckExpert(strDocID))
            {
                this.m_objViewer.m_PatientBasicInfo.lbeDocType.Text = "ר��";
            }
            else
            {
                this.m_objViewer.m_PatientBasicInfo.lbeDocType.Text = "��ͨ";
            }
        }
        #endregion

        #region LoginFile.XML��д����
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        public string m_strReadXML(string parentnode, string childnode, string key)
        {
            string strRet = "";

            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }

        /// <summary>
        /// ��ȡָ��������ֵ
        /// </summary>
        /// <param name="parentnode">���ڵ�</param>
        /// <param name="childnode">�ӽڵ�</param>
        /// <param name="key"></param>
        /// <param name="attributes">��Ҫ��ȡ������</param>
        /// <returns></returns>
        public string m_strReadXMLAttr(string parentnode, string childnode, string key, string attributes)
        {
            string strRet = "";

            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes[attributes].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }

        /// <summary>
        /// д����
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public bool m_blnWriteXML(string parentnode, string childnode, string key, string val)
        {
            bool blnRet = false;
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null && xndC != null)
                    {
                        xndC.Attributes["value"].Value = val;
                        xdoc.Save(XMLFile);
                        blnRet = true;
                    }
                }
            }
            catch
            {
                blnRet = false;
            }
            return blnRet;
        }
        #endregion

        #region ��ȡ���˴���(��������Ŀ)������Ϣ
        /// <summary>
        /// ��ȡ���˴���(��������Ŀ)������Ϣ
        /// </summary>
        /// <param name="Pid">����ID</param>
        /// <param name="CheckRecNums">�Ƿ��⴦����</param>
        /// <param name="RecipeNums">������</param>
        /// <param name="TotalMny">�ϼƽ��</param>
        /// <param name="SbMny">�Ը����</param>
        public void m_mthGetPatientRecipeFee(string Pid, string CardID, bool CheckRecNums, out int RecipeNums, out decimal TotalMny, out decimal SbMny)
        {
            RecipeNums = 0;
            TotalMny = 0;
            SbMny = 0;

            if (Pid.Trim() == "")
            {
                return;
            }

            try
            {
                DataTable dt = new DataTable();
                long l = 0;
                // �����ƺ���㴦����ѯ
                //if (this.m_objViewer.m_PatientBasicInfo.m_strIsVip == "1")
                // {
                //l = this.objSvc.m_lngGetTreatRecinfoBypid(Pid, out RecipeNums, out dt);
                //}
                //if (l > 0 && RecipeNums > 0)
                //{

                //}
                //else
                //{
                l = this.objSvc.m_lngGetAllrecinfoBypid(Pid, out RecipeNums, out dt, this.IsChildPrice);
                //}
                if (l > 0)
                {
                    if (RecipeNums == 0)
                    {
                        return;
                    }
                    else
                    {
                        if (RecipeNums == 1 && CheckRecNums)
                        {
                            return;
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TotalMny += clsMZPublic.Round(clsMZPublic.ConvertObjToDecimal(dt.Rows[i]["SumMoney"]), 2);
                            SbMny += clsMZPublic.Round(clsMZPublic.ConvertObjToDecimal(dt.Rows[i]["SumMoney"]) * clsMZPublic.ConvertObjToDecimal(dt.Rows[i]["Discount_dec"]) / 100, 2);
                        }

                        if (!this.m_objViewer.chkDefaultItem.Checked)
                        {
                            return;
                        }

                        string RecID = "";
                        string Status = "";
                        string strSeqid = "";
                        string strISgreen = "";
                        int RecCount = 0;
                        l = this.objSvc.m_mthFindMaxRecipeNoByPatientID(Pid, out RecID, out strSeqid, out Status, out RecCount, out dt, out strISgreen, this.IsChildPrice);
                        if (l > 0 && RecID.Trim() != "")
                        {
                            if (strISgreen == "1")
                            {
                                this.m_objViewer.blnFlag = true;
                            }
                            else
                            {
                                this.m_objViewer.blnFlag = false;
                            }
                            clsRecipeInfo_VO[] objRI_VO;
                            l = this.objSvc.m_mthFindRecipeDoctor(Pid, RecID, out objRI_VO);
                            if (l > 0 && objRI_VO != null)
                            {
                                //��������
                                string PayTypeID = objRI_VO[0].m_strPatientTypeID;
                                //�������� 1 ���� �� 2 ����
                                string RecipeFlag = ((objRI_VO[0].m_strRECIPEFLAG_INT.Trim() == "2") ? "2" : "1");
                                //�Ƿ�Һ� 1 �ѹ� �� 2 δ��
                                string IsReg = "2";
                                if (CheckRecNums)
                                {
                                    if (this.m_objViewer.m_PatientBasicInfo.RegisterID.Trim() != "" && this.m_objViewer.m_PatientBasicInfo.RegisterID != null)
                                    {
                                        IsReg = "1";
                                    }
                                }
                                else
                                {
                                    DataTable dtCard;
                                    l = this.objSvc.m_lngGetPatientInfoByCard(CardID, out dtCard);
                                    if (l > 0 && dtCard.Rows.Count > 0)
                                    {
                                        IsReg = "1";
                                    }
                                }

                                //�Ƿ�ר��
                                string DoctorID = objRI_VO[0].m_strDoctorID;
                                string IsExpert = "1";
                                if (!this.objSvc.m_blnCheckExpert(Pid))
                                {
                                    IsExpert = "2";
                                }

                                l = this.objSvc.m_mthGetDefaultItem(Pid, IsReg, RecipeFlag, IsExpert, out dt);
                                if (l > 0 && dt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        string ItemID = dt.Rows[j]["ITEMID_CHR"].ToString().Trim();
                                        DataTable dtRecord;
                                        l = this.objSvc.m_mthFindChrgItemByID(ItemID, PayTypeID, out dtRecord, this.IsChildPrice);
                                        if (l > 0 && dtRecord.Rows.Count == 1)
                                        {
                                            DataRow dtRow = dtRecord.Rows[0];

                                            //����
                                            decimal Price = 0;

                                            //�������ҩ�����õ�����С��λ,����С���ۺ�סԺ��С��λ
                                            if (this.m_mthIsMedicine(dtRow["ITEMOPINVTYPE_CHR"].ToString().Trim()) && dtRow["OPCHARGEFLG_INT"].ToString().Trim() == "1")
                                            {
                                                Price = clsMZPublic.ConvertObjToDecimal(dtRow["SUBMONEY"]);
                                            }
                                            else
                                            {
                                                Price = clsMZPublic.ConvertObjToDecimal(dtRow["ITEMPRICE_MNY"]);
                                            }

                                            //����
                                            decimal Amount = clsMZPublic.ConvertObjToDecimal(dtRow["itemnum"]);

                                            //����
                                            decimal Precent = 100;
                                            if (dtRow["PRECENT_DEC"].ToString().Trim() != "")
                                            {
                                                Precent = clsMZPublic.ConvertObjToDecimal(dtRow["PRECENT_DEC"]);
                                            }

                                            decimal d = clsMZPublic.Round(Price * Amount, 2);
                                            TotalMny += d;
                                            SbMny += clsMZPublic.Round(d * Precent / 100, 2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                RecipeNums = 0;
            }
        }
        #endregion

        #region ��ȡ���쵱ǰ��������δ���㴦����Ϣ
        /// <summary>
        /// ��ȡ���쵱ǰ��������δ���㴦����Ϣ
        /// </summary>
        /// <returns></returns>
        public int m_intGetallrecinfo()
        {
            int l = 0;
            RecipeCount = 0;
            DataTable dt = new DataTable();

            long ret = objSvc.m_lngGetAllrecinfoBypid(m_objViewer.m_PatientBasicInfo.PatientID, out RecipeCount, out dt, this.IsChildPrice);
            if (RecipeCount > 1)
            {
                decimal decTotal = 0;
                decimal decSbMny = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decTotal += clsPublic.Round(this.m_mthConvertObjToDecimal(dt.Rows[i]["SumMoney"]), 2);
                    decSbMny += clsPublic.Round(this.m_mthConvertObjToDecimal(dt.Rows[i]["SumMoney"]) * this.m_mthConvertObjToDecimal(dt.Rows[i]["Discount_dec"]) / 100, 2);
                }

                string SbMny = (decSbMny == 0 ? "" : decSbMny.ToString());
                string AcctMny = ((decTotal - decSbMny) == 0 ? "" : Convert.ToString(decTotal - decSbMny));

                frmShowpatallrecinfo f = new frmShowpatallrecinfo(RecipeCount.ToString(), AcctMny, SbMny, decTotal.ToString());
                if (f.ShowDialog() == DialogResult.OK)
                {
                    l = 1;
                }
                else
                {
                    l = -1;
                }

                RecipeCount = 0;
            }

            return l;
        }
        #endregion

        #region ��ҩʱ��ȡ������ͬҩ��ID�Ĵ���ID
        /// <summary>
        /// ��ҩʱ��ȡ������ͬҩ��ID�Ĵ���ID
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="medstoreid"></param>
        /// <returns></returns>
        private string m_strGetwinid(string pid, string medstoreid, out int waitno)
        {
            string winid = "";
            waitno = 1;
            DataTable dt = new DataTable();

            long ret = objSvc.m_lngGetsendmedinfoBypid(pid, medstoreid, out dt);
            if (dt.Rows.Count > 0)
            {
                winid = dt.Rows[0]["windowid_chr"].ToString().Trim();
                waitno = Convert.ToInt32(dt.Rows[0]["order_int"]) + 1;
            }

            return winid;
        }
        #endregion

        #region ���һ��ҩͬʱ���Է��С���ҩ�����趨
        /// <summary>
        /// ���һ��ҩͬʱ���Է��С���ҩ�����趨
        /// (һ�Ŵ������ҩ(��): û��ָ������ҩ��������ͬʱ������ҩ������������ҩ�����ģ����ж�ֻ����ҩ������)
        /// </summary>
        /// <returns></returns>
        public bool m_blnCheckmedproperty()
        {
            bool b = false;

            if (this.m_objViewer.ctlDataGrid1.RowCount <= 0)
            {
                return b;
            }

            string itemid = "";
            string medtype = "";
            Hashtable has = new Hashtable();

            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                itemid = this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim();
                medtype = objSvc.m_strGetOutSendMedStoretype(itemid);

                if (medtype != null && medtype != "" && !has.ContainsKey(medtype))
                {
                    has.Add(medtype, medtype);
                }
            }

            if (has.Count > 0)
            {
                bool b1 = has.ContainsKey("1");
                bool b2 = has.ContainsKey("2");
                bool b4 = has.ContainsKey("4");

                //һ�Ŵ������ҩ(��): û��ָ������ҩ��������ͬʱ������ҩ������������ҩ�����ģ����ж�ֻ����ҩ������
                if (!b1 && b2 && b4)
                {
                    b = true;
                }
            }

            return b;
        }
        #endregion

        #region ��ȡ�ָ��ַ�����ֵ
        /// <summary>
        /// ��ȡ�ָ��ַ�����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public ArrayList m_Gettoken(string str, string sign)
        {
            ArrayList val = null;

            if (str.Trim() == "")
            {
                return val;
            }

            int pos = 0;
            val = new ArrayList();

            do
            {
                pos = str.IndexOf(sign);
                if (pos > 0)
                {
                    val.Add(str.Substring(0, pos));
                    str = str.Substring(pos + 1);
                }
                else
                {
                    val.Add(str);
                }
            } while (pos > 0);


            return val;
        }
        #endregion

        #region ����DBF����
        /// <summary>
        /// ����DBF����
        /// </summary>
        /// <param name="p_strDBFile"></param>
        /// <param name="p_total"></param>
        /// <returns></returns>
        public bool m_blnCreateDBFData(ref string p_strDBFile, decimal p_total)
        {
            string str = this.m_objViewer.txtIDcard.Text.Trim();
            if (str == "")
            {
                MessageBox.Show("�ò���û�����֤�ţ������շ�������������ϵǼǴ���¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if ((this.m_objViewer.btSave.Tag == null) || (this.m_objViewer.btSave.Tag.ToString() == string.Empty))
            {
                MessageBox.Show("ҽ�����˽���ǰ�ȱ��洦����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            string str2 = string.Empty;
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if ((this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "") && (this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0M))
                {
                    str2 = str2 + "'" + this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() + "',";
                }
            }
            Dictionary<string, string> hashtable = new Dictionary<string, string>();
            if (str2 == string.Empty)
            {
                return false;
            }
            str2 = str2.Substring(0, str2.Length - 1);
            this.objSvc.m_lngGetIPInvoCat(str2, out hashtable);
            string str3 = this.m_objViewer.btSave.Tag.ToString().Substring(8, 6);
            int num2 = 0;
            this.HasYB = new Hashtable();
            ArrayList objYBArr = new ArrayList();
            clsOPSB_VO sopsb_vo = null;
            for (int j = 0; j < this.m_objViewer.ctlDataGrid1.RowCount; j++)
            {
                if (((this.m_objViewer.ctlDataGrid1[j, 10].ToString().Trim() != "") && (this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]) > 0M)) && !this.HasYB.ContainsKey(j))
                {
                    sopsb_vo = new clsOPSB_VO();
                    str2 = this.m_objViewer.ctlDataGrid1[j, 10].ToString().Trim();
                    string str4 = this.m_objViewer.ctlDataGrid1[j, "colYbcode"].ToString().Trim();
                    decimal num4 = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]);
                    decimal num5 = 0;
                    if (this.intDiffPriceOn == 0)
                    {
                        num5 = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]);
                    }
                    else
                    {
                        num5 = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]) - Math.Abs(clsPublic.Round(this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, intDiffPriceTotalCol]), 2));
                    }
                    for (int m = j + 1; m < this.m_objViewer.ctlDataGrid1.RowCount; m++)
                    {
                        if ((this.m_objViewer.ctlDataGrid1[m, "colYbcode"].ToString().Trim() == str4) && (this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[m, 1]) > 0M))
                        {
                            num4 += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[m, 1]);
                            if (this.intDiffPriceOn == 0)
                            {
                                num5 += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[m, 7]);
                            }
                            else
                            {
                                num5 += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[m, 7]) - Math.Abs(clsPublic.Round(this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[m, intDiffPriceTotalCol]), 2));
                            }
                            this.HasYB.Add(m, j);
                        }
                    }
                    if (this.m_objViewer.ctlDataGrid1[j, 8].ToString().Trim() == "0003")
                    {
                        decimal num7 = 1M;
                        if (this.m_objViewer.numericUpDown1.Value > 1M)
                        {
                            num7 = this.m_objViewer.numericUpDown1.Value;
                        }
                        num4 *= num7;
                    }
                    num2++;
                    sopsb_vo.YYBH = this.Hospcode;
                    sopsb_vo.ZYH = this.m_objViewer.LoginInfo.m_strEmpNo + str3;
                    sopsb_vo.GMSFHM = str;
                    sopsb_vo.FYRQ = DateTime.Now.ToString("yyyyMMdd");
                    sopsb_vo.XMXH = num2;
                    sopsb_vo.XMBH = str4;
                    sopsb_vo.XMMC = this.m_objViewer.ctlDataGrid1[j, 2].ToString().Trim();
                    sopsb_vo.FLDM = hashtable[str2].ToString();
                    sopsb_vo.YPGG = this.m_objViewer.ctlDataGrid1[j, 4].ToString().Trim();
                    sopsb_vo.YPJX = "";
                    if (this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 6]) > 0 && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, intDiffUnitPriceCol]) > 0)
                        sopsb_vo.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, intDiffUnitPriceCol]); //6]); // col6: ���۵���
                    else
                        sopsb_vo.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 6]); // ���۵���
                    sopsb_vo.MCYL = num4;
                    sopsb_vo.JE = num5;
                    objYBArr.Add(sopsb_vo);
                }
            }
            if (objYBArr.Count == 0)
            {
                return false;
            }
            p_strDBFile = this.DBQ + "A" + sopsb_vo.FYRQ + sopsb_vo.ZYH + this.m_objViewer.m_PatientBasicInfo.PatientName + ".dbf";
            if (File.Exists(p_strDBFile))
            {
                File.Delete(p_strDBFile);
            }
            if (this.objSvc.m_lngCreateDbf_OutPatient(this.SQLParm, p_strDBFile, objYBArr) <= 0L)
            {
                MessageBox.Show("����ҽ��DBF�ļ�ʧ�ܡ�", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����DBF����
        /// </summary>
        /// <returns></returns>
        public bool m_blnCreateDBFData_bak(ref string p_strDBFile, decimal totalMoney)
        {
            string strIDCard = this.m_objViewer.txtIDcard.Text.Trim();
            if (strIDCard == "")
            {
                MessageBox.Show("�ò���û�����֤�ţ������շ�������������ϵǼǴ���¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (this.m_objViewer.btSave.Tag == null || this.m_objViewer.btSave.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("ҽ�����˽���ǰ�ȱ��洦����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            string strItemID = string.Empty;
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "" && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0)
                {
                    strItemID += "'" + this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() + "',";
                }
            }

            Dictionary<string, string> hasItemCat = new Dictionary<string, string>();
            if (strItemID == string.Empty)
            {
                return false;
            }
            else
            {
                strItemID = strItemID.Substring(0, strItemID.Length - 1);
                this.objSvc.m_lngGetIPInvoCat(strItemID, out hasItemCat);
            }

            string strSubRecipeID = this.m_objViewer.btSave.Tag.ToString().Substring(8, 6);

            int intXmxh = 0;
            HasYB = new Hashtable();
            ArrayList objYBArr = new ArrayList();

            clsOPSB_VO objYB = null;
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "" && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0)
                {
                    if (HasYB.ContainsKey(i))
                    {
                        continue;
                    }

                    objYB = new clsOPSB_VO();

                    strItemID = this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim();
                    string ybcode = this.m_objViewer.ctlDataGrid1[i, "colYbcode"].ToString().Trim();
                    decimal qnt = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]);
                    decimal amt = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                    string relrowno = i.ToString();

                    for (int j = i + 1; j < this.m_objViewer.ctlDataGrid1.RowCount; j++)
                    {
                        if (this.m_objViewer.ctlDataGrid1[j, "colYbcode"].ToString().Trim() == ybcode && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]) > 0)
                        {
                            qnt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]);
                            //amt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]);
                            if (intDiffPriceOn == 0)
                                amt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]);
                            else
                                amt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]) + clsPublic.Round(this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, intDiffPriceTotalCol]), 2);// �������������
                            HasYB.Add(j, i);
                        }
                    }

                    /***��ҩ��Ʊ����̶���0003����this.objCalPatientCharge.InvoiceCatID��***/
                    if (this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim() == "0003")
                    {
                        decimal fs = 1;
                        if (this.m_objViewer.numericUpDown1.Value > 1)
                        {
                            fs = this.m_objViewer.numericUpDown1.Value;
                        }
                        qnt = qnt * fs;
                    }
                    /******/
                    intXmxh++;

                    objYB.YYBH = this.Hospcode;
                    objYB.ZYH = this.m_objViewer.LoginInfo.m_strEmpNo + strSubRecipeID;
                    objYB.GMSFHM = strIDCard;
                    objYB.FYRQ = DateTime.Now.ToString("yyyyMMdd");
                    objYB.XMXH = intXmxh;
                    objYB.XMBH = ybcode;
                    objYB.XMMC = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim();
                    objYB.FLDM = hasItemCat[strItemID].ToString();
                    objYB.YPGG = this.m_objViewer.ctlDataGrid1[i, 4].ToString().Trim();
                    objYB.YPJX = "";
                    if (this.intDiffPriceOn == 1)
                    {
                        if (blMedicine9003((m_objViewer.ctlDataGrid1[i, 10].ToString().Trim())))// �ж��Ƿ���ҩƷ
                        {
                            objYB.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, intDiffUnitPriceCol]);
                            if (objYB.JG == 0)
                                objYB.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]);
                        }
                        else
                            objYB.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]);
                    }
                    else
                        objYB.JG = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]);
                    objYB.MCYL = qnt;
                    objYB.JE = amt;
                    // 20151102     objYB.JG = decimal.Round(amt / qnt, 4);//���/������
                    objYBArr.Add(objYB);
                }
            }

            if (objYBArr.Count == 0)
            {
                return false;
            }

            // 20151102
            int k = -1;
            decimal dec = 0;
            decimal tmpMoney1 = 0;
            decimal tmpMoney2 = 0;
            foreach (clsOPSB_VO item in objYBArr)
            {
                ++k;
                dec += clsPublic.Round(item.JG * item.MCYL, 2);
                tmpMoney1 += dec;
                if (k != objYBArr.Count - 1) tmpMoney2 += dec;
            }
            if (totalMoney != tmpMoney1)
            {
                ((clsOPSB_VO)objYBArr[objYBArr.Count - 1]).JG = clsPublic.Round((totalMoney - tmpMoney2) / ((clsOPSB_VO)objYBArr[objYBArr.Count - 1]).MCYL, 4);
            }

            //decimal TempTotal = 0;
            //decimal AddedTotal = 0;
            //for (int k = 0; k < objYBArr.Count; k++)
            //{
            //    clsOPSB_VO vo =(clsOPSB_VO) objYBArr[k];
            //    TempTotal += vo.MCYL * vo.JG;
            //    AddedTotal += vo.JE;
            //}
            //decimal diff = AddedTotal - TempTotal;
            //if (diff != 0)
            //{
            //    if (diff > 0)//˵����������� ��������֮�������� ��Ҫ����
            //    {
            //        clsOPSB_VO voTemp=(clsOPSB_VO)objYBArr[0];
            //        voTemp.JE += diff;
            //        voTemp.JG = decimal.Round(voTemp.JE / voTemp.MCYL, 4);//���/����
            //    }
            //    else
            //    {
            //        for (int k = 0; k < objYBArr.Count; k++)
            //        {
            //            clsOPSB_VO vo = (clsOPSB_VO)objYBArr[k];
            //            if (vo.JE + diff > 1)
            //            {
            //                vo.JE += diff;
            //                vo.JG = decimal.Round(vo.JE / vo.MCYL,4);//���/����
            //                break;
            //            }
            //        }
            //    }
            //}

            //A+���������������գ�ҽԺҵ����ˮ��+����.dbf
            p_strDBFile = this.DBQ + "A" + objYB.FYRQ + objYB.ZYH + this.m_objViewer.m_PatientBasicInfo.PatientName + ".dbf";

            if (File.Exists(p_strDBFile))
            {
                File.Delete(p_strDBFile);
            }

            long l = this.objSvc.m_lngCreateDbf_OutPatient(this.SQLParm, p_strDBFile, objYBArr);
            if (l > 0)
            {
                //MessageBox.Show("����ҽ��DBF�ļ��ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("����ҽ��DBF�ļ�ʧ�ܡ�", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion

        #region ��Ƕ��ʽ�籣�ӿ���֤
        /// <summary>
        /// ��Ƕ��ʽ�籣�ӿ���֤
        /// </summary>
        /// <returns></returns>
        public bool m_blnNewYbInterface()
        {
            string strIDCard = this.m_objViewer.txtIDcard.Text.Trim();
            if (strIDCard == "")
            {
                MessageBox.Show("�ò���û�����֤�ţ������շ�������������ϵǼǴ���¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (this.m_objViewer.btSave.Tag == null || this.m_objViewer.btSave.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("ҽ�����˽���ǰ�ȱ��洦����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        #endregion

        #region ��ȡDBF�ļ���Ϣ
        /// <summary>
        /// ��ȡDBF�ļ���Ϣ
        /// </summary>
        /// <param name="p_strDBFile"></param>
        public bool m_blnReadDBFData(string p_strDBFile, out DataTable p_dtYB)
        {
            p_dtYB = new DataTable();

            long l = this.objSvc.m_lngGetResult_OutPatient(this.SQLParm, p_strDBFile, out p_dtYB);
            if (l > 0 && p_dtYB.Rows.Count > 0)
            {

            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion

        #region ���½����
        /// <summary>
        /// ���½����
        /// </summary>
        /// <param name="p_strChargeNo"></param>
        public void m_mthUpdateChargeNo(string p_strChargeNo)
        {
            if (string.IsNullOrEmpty(this.m_objViewer.btSave.Tag.ToString()))
            {
                return;
            }

            this.objSvc.m_lngUpdateYBChargeNo(this.m_objViewer.btSave.Tag.ToString(), p_strChargeNo);
        }
        #endregion

        #region �����շ���Ϣ��ҽ��ǰ�û�
        /// <summary>
        /// �����շ���Ϣ��ҽ��ǰ�û�
        /// </summary>
        /// <returns></returns>
        public bool m_blnSendybdata(ref string BillNO)
        {
            bool ret = false;
            ArrayList objYBArr = new ArrayList();

            string YBCardNo = this.m_objViewer.txtInsuranceID.Text.Trim();
            if (YBCardNo == "")
            {
                MessageBox.Show("�ò���û��ҽ������(�����ݱ��)�������շ�������������ϵǼǴ���¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            YBCardNo += "+" + this.m_objViewer.m_PatientBasicInfo.PatientName;

            HasYB = null;
            HasYB = new Hashtable();

            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "" && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0)
                {
                    if (HasYB.ContainsKey(i))
                    {
                        continue;
                    }

                    clsYB_VO objYB = new clsYB_VO();

                    string ybcode = this.m_objViewer.ctlDataGrid1[i, "colYbcode"].ToString().Trim();
                    decimal qnt = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]);
                    decimal amt = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 7]);
                    string relrowno = i.ToString();

                    for (int j = i + 1; j < this.m_objViewer.ctlDataGrid1.RowCount; j++)
                    {
                        if (this.m_objViewer.ctlDataGrid1[j, "colYbcode"].ToString().Trim() == ybcode && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]) > 0)
                        {
                            qnt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]);
                            amt += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 7]);
                            HasYB.Add(j, i);
                        }
                    }

                    /***��ҩ��Ʊ����̶���0003����this.objCalPatientCharge.InvoiceCatID��***/
                    if (this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim() == "0003")
                    {
                        decimal fs = 1;
                        if (this.m_objViewer.numericUpDown1.Value > 1)
                        {
                            fs = this.m_objViewer.numericUpDown1.Value;
                        }
                        qnt = qnt * fs;
                    }
                    /******/

                    objYB.Hoscode = Hospcode;
                    objYB.Billno = "";
                    objYB.XMCode = ybcode;
                    objYB.Asssign = "";

                    if (this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim().Length <= 15)
                    {
                        objYB.XMDes = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim();
                    }
                    else
                    {
                        objYB.XMDes = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim().Substring(0, 15);
                    }

                    if (this.m_objViewer.ctlDataGrid1[i, 5].ToString().Trim().Length <= 4)
                    {
                        objYB.XMUnt = this.m_objViewer.ctlDataGrid1[i, 5].ToString().Trim();
                    }
                    else
                    {
                        objYB.XMUnt = this.m_objViewer.ctlDataGrid1[i, 5].ToString().Trim().Substring(0, 4);
                    }
                    objYB.XMQnt = qnt;
                    objYB.XMPrc = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]);
                    objYB.XMAmt = amt;
                    objYB.Trndate = DateTime.Now.ToString("yyyy-MM-dd");
                    objYB.Trnflag = "F";
                    objYB.Memoa = YBCardNo;
                    objYB.UVersion = "";

                    objYBArr.Add(objYB);
                }
            }

            List<string> objRecipeArr = new List<string>();

            foreach (clsOutPatientRecipe_VO o in objHashTable.Values)
            {
                objRecipeArr.Add(o.m_strOutpatRecipeID);
            }

            long l = this.objSvc.m_lngSendybdata(DB2Parm, objRecipeArr, objYBArr, ref BillNO);
            if (l == 1)
            {
                ret = true;
            }

            return ret;
        }
        #endregion

        #region ��ȡҽ�����ݲ�����䵽DATAGRID
        /// <summary>
        /// ��ȡҽ�����ݲ�����䵽DATAGRID
        /// </summary>
        public void m_mthGetybdata(com.digitalwave.controls.datagrid.ctlDataGrid dg)
        {
            DataTable dt = new DataTable();

            long l = this.objSvc.m_lngGetybjsmx(DB2Parm, Hospcode, BillNO, out dt);
            if (l == 0)
            {
                return;
            }

            if (dg.RowCount == 0)
            {
                return;
            }

            this.m_objViewer.TolVal = 0;
            this.m_objViewer.YBVal = this.m_mthConvertObjToDecimal(dt.Rows[0]["zfyp"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["ylyp"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["zcyp"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["zcyf"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["gxylf"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["qtfy"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["lxqjf"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["lxfqjf"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["lxjkcl"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["ylxma"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["ylxmb"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["ylxmc"]) +
                                     this.m_mthConvertObjToDecimal(dt.Rows[0]["ylxmd"]) + this.m_mthConvertObjToDecimal(dt.Rows[0]["ylxme"]);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string itemcode = dt.Rows[i]["xmcode"].ToString().Trim();
                decimal ybval = this.m_mthConvertObjToDecimal(dt.Rows[i]["nrzamt"]);
                decimal tolval = this.m_mthConvertObjToDecimal(dt.Rows[i]["ylfamt"]);
                decimal scale = 100;

                if (tolval != 0)
                {
                    scale = 100 - Math.Floor((ybval / tolval) * 100);
                }

                this.m_objViewer.TolVal += tolval;

                for (int j = 0; j < dg.RowCount; j++)
                {
                    if (dg[j, 0].ToString().Trim() == itemcode)
                    {
                        decimal zje = this.m_mthConvertObjToDecimal(dg[j, 7]);
                        decimal zfje = Math.Round(zje * scale / 100, 2);
                        decimal ybje = zje - zfje;

                        dg[j, 13] = scale.ToString() + "%";
                        dg[j, 14] = scale;
                        dg[j, 23] = zfje.ToString("0.00");
                        dg[j, 32] = ybje.ToString("0.00");
                    }
                }

                this.m_objViewer.YBFlag = true;
            }
        }
        #endregion

        #region ���ͻ��ȡҽ������
        /// <summary>
        /// ���ͻ��ȡҽ������
        /// </summary>
        public bool m_blnYBStart(Label lblBillNO, com.digitalwave.controls.datagrid.ctlDataGrid dg)
        {
            if (this.m_objViewer.btSave.Tag != null && this.m_objViewer.btSave.Tag.ToString().Trim() != "")
            {
                string recno = this.m_objViewer.btSave.Tag.ToString().Trim();
                bool b = false;
                BillNO = "";

                long l = this.objSvc.m_lngGetybbillno(recno, ref BillNO);
                if (l > 0 && BillNO.Trim() != "")
                {
                    if (this.objSvc.m_blnCheckBillNo(DB2Parm, this.Hospcode, BillNO))
                    {
                        b = true;
                    }
                    else
                    {
                        l = this.objSvc.m_lngDelybdata(DB2Parm, BillNO);
                        if (l > 0)
                        {
                            bool b1 = this.m_blnSendybdata(ref BillNO);
                            if (!b1)
                            {
                                MessageBox.Show("�����շ���Ϣ��ҽ��ϵͳʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�����շ���Ϣ��ҽ��ϵͳʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                    }
                }
                else
                {
                    bool b1 = this.m_blnSendybdata(ref BillNO);
                    if (!b1)
                    {
                        MessageBox.Show("�����շ���Ϣ��ҽ��ϵͳʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                if (!this.objSvc.m_blnCheckSendRes(DB2Parm, this.Hospcode, BillNO))
                {
                    MessageBox.Show("���ٴε����ҽ�����㡿��ť��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                lblBillNO.Text = BillNO;
                dg.m_mthDeleteAllRow();

                for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
                {
                    if (this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "" && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0)
                    {
                        int row = dg.RowCount;
                        dg.m_mthAppendRow();

                        dg[row, 0] = this.m_objViewer.ctlDataGrid1[i, "colYbcode"];
                        dg[row, 1] = this.m_objViewer.ctlDataGrid1[i, 1];
                        dg[row, 2] = this.m_objViewer.ctlDataGrid1[i, 2];
                        dg[row, 3] = this.m_objViewer.ctlDataGrid1[i, 3];
                        dg[row, 4] = this.m_objViewer.ctlDataGrid1[i, 4];
                        dg[row, 5] = this.m_objViewer.ctlDataGrid1[i, 5];
                        dg[row, 6] = this.m_objViewer.ctlDataGrid1[i, 6];
                        dg[row, 7] = this.m_objViewer.ctlDataGrid1[i, 7];
                        dg[row, 13] = this.m_objViewer.ctlDataGrid1[i, 13];
                        dg[row, 14] = this.m_objViewer.ctlDataGrid1[i, 14];
                        dg[row, 23] = this.m_objViewer.ctlDataGrid1[i, 23];
                    }
                }

                if (b)
                {
                    this.m_mthGetybdata(dg);
                }
            }
            else
            {
                MessageBox.Show("ҽ������Ĵ�����ҪԤ���ֹ����棬�뱣��ô������ٴν��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }
        #endregion

        #region �����Ը�����
        /// <summary>
        /// �����Ը�����
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        public void m_mthYBEnd(com.digitalwave.controls.datagrid.ctlDataGrid dg)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                this.m_objViewer.ctlDataGrid1[i, 13] = dg[i, 13];
                this.m_objViewer.ctlDataGrid1[i, 14] = dg[i, 14];
            }
        }
        #endregion

        #region �ֹ����ļ��ʵ���
        /// <summary>
        /// �ֹ����ļ��ʵ���
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public bool m_blnModifyBillNo(out string BillNo)
        {
            BillNo = this.BillNO;
            string ErrMsg = "�����µļ��ʵ���ʧ�ܡ�";
            bool ret = false;

            bool IsExist = this.objSvc.m_blnCheckBillNo(DB2Parm, this.Hospcode, BillNo);
            if (IsExist)
            {
                ErrMsg = "�ñʷ�������ҽ�����Ľ��㣬�����ֹ����ļ��ʵ��š�";
            }
            else
            {
                string NewBillNo = "";
                this.objSvc.m_mthGenBillNo(out NewBillNo);
                if (NewBillNo != "")
                {
                    long l = this.objSvc.m_lngModifyBillNo(BillNo, NewBillNo);
                    if (l > 0)
                    {
                        l = this.objSvc.m_lngModifyBillNo(DB2Parm, BillNo, NewBillNo);
                        if (l > 0)
                        {
                            this.BillNO = NewBillNo;
                            BillNo = NewBillNo;
                            ret = true;
                        }
                        else
                        {
                            l = this.objSvc.m_lngModifyBillNo(NewBillNo, BillNo);
                        }
                    }
                }
            }

            if (!ret)
            {
                MessageBox.Show(ErrMsg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return ret;
        }
        #endregion

        #region ��ȡ�ָ��ַ�����ֵ
        /// <summary>
        /// ��ȡ�ָ��ַ�����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public ArrayList m_ArrGettoken(string str, string sign)
        {
            ArrayList val = new ArrayList();

            if (str.Trim() == "")
            {
                return val;
            }

            int pos = 0;
            do
            {
                pos = str.IndexOf(sign);
                if (pos > 0)
                {
                    val.Add(str.Substring(0, pos));
                    str = str.Substring(pos + 1);
                }
                else
                {
                    val.Add(str);
                }
            } while (pos > 0);

            return val;
        }
        #endregion

        #region ˳���ض�ҽ��
        /// <summary>
        /// ˳���ض�˳��ҽ��
        /// </summary>
        /// <returns></returns>
        public bool m_blnSDTDYB()
        {
            ArrayList objYBArr = new ArrayList();

            if (this.m_objViewer.txtInsuranceID.Text.Trim() == "")
            {
                MessageBox.Show("�ò���û��ҽ������(�����ݱ��)�������շ�������������ϵǼǴ���¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string RecipeID = this.m_objViewer.btSave.Tag.ToString();
            HasYB = null;
            HasYB = new Hashtable();

            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, 10].ToString().Trim() != "" && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]) > 0)
                {
                    if (HasYB.ContainsKey(i))
                    {
                        continue;
                    }

                    clsSDTDMZYB_VO objYB = new clsSDTDMZYB_VO();

                    string ybcode = this.m_objViewer.ctlDataGrid1[i, "colYbcode"].ToString().Trim();
                    string invocate = this.m_objViewer.ctlDataGrid1[i, 8].ToString().Trim();
                    decimal amount = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 1]);
                    decimal price = this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[i, 6]);

                    for (int j = i + 1; j < this.m_objViewer.ctlDataGrid1.RowCount; j++)
                    {
                        if (this.m_objViewer.ctlDataGrid1[j, "colYbcode"].ToString().Trim() == ybcode && this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]) > 0)
                        {
                            amount += this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[j, 1]);
                            HasYB.Add(j, i);
                        }
                    }

                    /***��ҩ��Ʊ����̶���0003����this.objCalPatientCharge.InvoiceCatID��***/
                    if (invocate == "0003")
                    {
                        decimal fs = 1;
                        if (this.m_objViewer.numericUpDown1.Value > 1)
                        {
                            fs = this.m_objViewer.numericUpDown1.Value;
                        }
                        amount = amount * fs;
                    }
                    /******/

                    objYB.Yyid = RecipeID;
                    objYB.Xmbh = ybcode;
                    objYB.Fldm = invocate;

                    if (this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim().Length <= 25)
                    {
                        objYB.Zlxm = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim();
                    }
                    else
                    {
                        objYB.Zlxm = this.m_objViewer.ctlDataGrid1[i, 2].ToString().Trim().Substring(0, 25);
                    }

                    objYB.Xmjg = price;
                    objYB.Xmsl = amount;
                    objYB.Zlfy = price * amount;
                    objYB.Xm = this.m_objViewer.m_PatientBasicInfo.PatientName;

                    objYBArr.Add(objYB);
                }
            }

            decimal YBMoney = 0;
            string OutMsg = "";
            long l = this.objSvc.m_lngSDTDMZYB(SQLParm, RecipeID, SDYBMzbm, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.txtInsuranceID.Text.Trim(), objYBArr, out YBMoney, out OutMsg);
            if (l == 0 || OutMsg.Trim() != "")
            {
                MessageBox.Show("�ض�ҽ������ʧ�ܡ�\r\n\r\nʧ��ԭ��" + OutMsg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            this.m_objViewer.YBVal = YBMoney;
            this.m_objViewer.YBFlag = true;

            return true;
        }
        #endregion

        #region ���ÿ����Ϣ
        /// <summary>
        /// ���ÿ����Ϣ
        /// </summary>
        /// <param name="frmMdi"></param>
        /// <param name="Index"></param>
        /// <param name="Info"></param>
        public void SetShortCutInfo(Form frmMdi, int Index, string Info)
        {
            StatusBar objInfoBar = new StatusBar();
            if (frmMdi != null)
            {
                foreach (Control c in frmMdi.Controls)
                {
                    if (c is StatusBar)
                    {
                        objInfoBar = c as StatusBar;
                        break;
                    }
                }
            }

            if (objInfoBar != null && objInfoBar.Panels.Count > 4)
            {
                objInfoBar.Panels[Index].Text = Info;
                objInfoBar.Panels[Index].ToolTipText = "";
            }
        }
        #endregion

        #region �ж��Ƿ��Ƕ�ݸ��ҽ������
        /// <summary>
        /// �ж��Ƿ��Ƕ�ݸ��ҽ������
        /// </summary>
        public void m_mthCheckYBPayType()
        {
            IsDongGuanYBPatient = false;
            if (YBPayTypeArr.Count > 0 && this.m_objViewer.m_PatientBasicInfo.PayTypeID.ToString().Trim() != "")
            {
                if (YBPayTypeArr.IndexOf(this.m_objViewer.m_PatientBasicInfo.PayTypeID) >= 0)
                {
                    IsDongGuanYBPatient = true;
                }
            }
        }
        #endregion


        public void m_mthGetPEDoctor()
        {
            if (!this.PEWorkStationFlag)
            {
                return;
            }

            string CardNo = this.m_objViewer.m_PatientBasicInfo.PatientCardID.Trim();
            if (CardNo == "")
            {
                return;
            }

            //string strDoctorID = "";
            clsEmployeeVO objDoctor;

            long l = this.objSvc.m_lngGetPEDoctor(CardNo, out objDoctor);

            if (l > 0 && objDoctor != null)
            {
                //this.m_objViewer.m_PatientBasicInfo.DoctorID = strDoctorID;
                //this.m_objViewer.m_PatientBasicInfo.m_FindLvw(strDoctorID);
                ////this.m_objViewer.m_PatientBasicInfo.m_GetlvwItem();
                //this.m_objViewer.m_PatientBasicInfo.m_lsvAllplan_DoubleClick(null, null);
                this.m_objViewer.m_PatientBasicInfo.m_mthFillDoc(objDoctor);
                this.m_mthSetFocusOnDataGrid();
            }
            else
            {
                MessageBox.Show(this.m_objViewer, "δ�ҵ����Ǽ�����Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region (���)����������շ���Ϣ
        /// <summary>
        /// (���)����������շ���Ϣ
        /// </summary>
        public void m_mthLoadPEItem()
        {
            this.PERegisterNoArr = "";

            if (!this.PEWorkStationFlag)
            {
                return;
            }

            string CardNo = this.m_objViewer.m_PatientBasicInfo.PatientCardID.Trim();
            if (CardNo == "")
            {
                return;
            }

            DataTable dt;

            long l = this.objSvc.m_lngGetPEChargeItem(CardNo, out dt);
            if (l > 0)
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    string strPatientTypeID = "0001";
                    if (this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim() != "")
                    {
                        strPatientTypeID = this.m_objViewer.m_PatientBasicInfo.PayTypeID.Trim();
                    }

                    ArrayList objRegNo = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        m_mthGetChargeItemByItem(dt.Rows[i]["ITEMID_CHR"].ToString().Trim(), strPatientTypeID, 2);
                        this.m_objViewer.IsSave = false;
                        this.m_objViewer.ctlDataGrid1_m_evtCurrentCellChanged(null, null);
                        this.m_objViewer.rowNO++;

                        if (objRegNo.IndexOf(dt.Rows[i]["regno_chr"].ToString().Trim()) < 0)
                        {
                            objRegNo.Add(dt.Rows[i]["regno_chr"].ToString().Trim());

                        }
                    }

                    if (objRegNo.Count > 0)
                    {
                        for (int j = 0; j < objRegNo.Count; j++)
                        {
                            this.PERegisterNoArr += objRegNo[j].ToString() + ";";
                        }
                        this.PERegisterNoArr = this.PERegisterNoArr.Substring(0, this.PERegisterNoArr.Length - 1);
                    }

                    this.m_mthSetFocusOnDataGrid();
                }
            }
        }
        #endregion

        #region (���)�Զ����ͼ������뵥
        /// <summary>
        /// (���)�Զ����ͼ������뵥
        /// </summary>
        public void m_mthAutoSendLisApplyBill()
        {
            string CardNo = this.m_objViewer.m_PatientBasicInfo.PatientCardID.Trim();
            if (CardNo == "")
            {
                return;
            }

            if (this.PERegisterNoArr.Trim() == "")
            {
                return;
            }

            DataTable dt;

            long l = this.objSvc.m_lngGetPELisItem(CardNo, out dt);
            if (l > 0)
            {
                string SampleTypeID = "";
                DataRow dr = null;
                List<clsTestApplyItme_VO> objTemp = new List<clsTestApplyItme_VO>();
                System.Collections.Generic.List<clsPERegGroup_VO> objPERegGroup = new System.Collections.Generic.List<clsPERegGroup_VO>();
                clsPERegGroup_VO objRegGroup;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];

                    clsTestApplyItme_VO item_VO = new clsTestApplyItme_VO();
                    item_VO.m_decDiscount = 100;
                    item_VO.m_decPrice = m_mthConvertObjToDecimal(dr["price_dec"]);
                    item_VO.m_decQty = 1;
                    item_VO.m_decTolPrice = m_mthConvertObjToDecimal(dr["price_dec"]);
                    item_VO.m_strItemID = dr["lisappgroupid_vchr"].ToString().Trim();
                    item_VO.m_strItemName = dr["groupname_vchr"].ToString().Trim();
                    item_VO.m_strSpec = "";
                    item_VO.m_strUnit = "";
                    item_VO.m_strOutpatRecipeID = "";
                    item_VO.m_strRowNo = i.ToString();
                    item_VO.m_strOprDeptID = "";
                    item_VO.strPartID = dr["groupcode_chr"].ToString().Trim();
                    item_VO.m_strOutpatRecipeDeID = dr["groupcode_chr"].ToString().Trim();  //������ñ������CODE
                    if (dr["lissampleid_vchr"].ToString().Trim() != "")
                    {
                        SampleTypeID = dr["lissampleid_vchr"].ToString().Trim();
                        item_VO.m_strSampleId = SampleTypeID;
                    }
                    objTemp.Add(item_VO);

                    objRegGroup = new clsPERegGroup_VO();
                    objRegGroup.Itemcode = dr["groupcode_chr"].ToString().Trim();
                    objRegGroup.RegNo = dr["regno_chr"].ToString().Trim();
                    objRegGroup.intStatus = 2;
                    objPERegGroup.Add(objRegGroup);

                }

                clsLisApplMainVO objLMVO = new clsLisApplMainVO();
                objLMVO.m_intEmergency = 0;
                objLMVO.m_intForm_int = 0;
                if (this.m_objViewer.m_PatientBasicInfo.PatientAge.IndexOf("��") >= 0)
                {
                    objLMVO.m_strAge = this.m_objViewer.m_PatientBasicInfo.PatientAge;
                }
                else
                {
                    objLMVO.m_strAge = this.m_objViewer.m_PatientBasicInfo.PatientAge + " ��";
                }
                objLMVO.m_strAppl_DeptID = this.m_objViewer.m_PatientBasicInfo.DeptID;
                objLMVO.m_strAppl_EmpID = this.m_objViewer.m_PatientBasicInfo.DoctorID;
                objLMVO.m_strDiagnose = "";
                objLMVO.m_strOperator_ID = this.m_objViewer.LoginInfo.m_strEmpID;
                objLMVO.m_strPatient_Name = this.m_objViewer.m_PatientBasicInfo.PatientName;
                objLMVO.m_strPatientcardID = this.m_objViewer.m_PatientBasicInfo.PatientCardID;
                objLMVO.m_strPatientID = this.m_objViewer.m_PatientBasicInfo.PatientID;
                objLMVO.m_strPatientType = "2";
                objLMVO.m_strSex = this.m_objViewer.m_PatientBasicInfo.PatientSex;
                objLMVO.m_strPatient_inhospitalno_chr = this.PERegisterNoArr.Replace("'", "").Substring(0, 12);

                List<clsATTACHRELATION_VO> objAttach = new List<clsATTACHRELATION_VO>();
                clsTestApplyItme_VO[] itemArr_VO = objTemp.ToArray();
                if (itemArr_VO.Length > 0)
                {
                    objLMVO.m_strSampleTypeID = SampleTypeID;      //�����ȡ�������ʹ���

                    frmLisAppl obj = new frmLisAppl();
                    if (obj.m_mthNewApp(objLMVO, itemArr_VO, false) == DialogResult.OK)
                    {
                        string ApplyID = "";
                        Hashtable hasTmp = new Hashtable();

                        clsLISAppResults[] objAppResult = obj.m_objGetMutiResults();
                        for (int j = 0; j < objAppResult.Length; j++)
                        {
                            for (int k = 0; k < objAppResult[j].m_strChargeIDs.Length; k++)
                            {
                                for (int k1 = 0; k1 < itemArr_VO.Length; k1++)
                                {
                                    if (objAppResult[j].m_ObjApplyUnitIDArr[k] == itemArr_VO[k1].m_strItemID)
                                    {
                                        ApplyID = objAppResult[j].m_StrApplicationID;

                                        if (hasTmp.ContainsKey(ApplyID))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            hasTmp.Add(ApplyID, null);
                                        }

                                        clsATTACHRELATION_VO ATTACHRELATION_VO = new clsATTACHRELATION_VO();
                                        ATTACHRELATION_VO.strSYSFROM_INT = "7";
                                        ATTACHRELATION_VO.strATTACHTYPE_INT = "7";
                                        ATTACHRELATION_VO.strSOURCEITEMID_VCHR = this.PERegisterNoArr;
                                        ATTACHRELATION_VO.strATTACHID_VCHR = ApplyID;
                                        ATTACHRELATION_VO.strURGENCY_INT = "0";

                                        objAttach.Add(ATTACHRELATION_VO);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("�Զ����ɼ������뵥ʧ�ܣ����ֹ����·��͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                //�����շѱ�־
                l = this.objSvc.m_lngUpdatePEChargeFlag(clsPublic.m_ArrGettoken(this.PERegisterNoArr, ";"), objAttach, objPERegGroup);
                if (l == 0)
                {
                    MessageBox.Show("����������շѱ�־ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        #endregion

        #region ������ϸ��������ȡ��ϸ�ܽ��
        /// <summary>
        /// ������ϸ��������ȡ��ϸ�ܽ��
        /// </summary>
        /// <param name="p_decTotalMoney"></param>
        /// <returns></returns>
        internal decimal m_decGetTotalPay()
        {
            decimal decTotalMoney = 0;//�ܼ�

            string strType = string.Empty;//����
            decimal decPrice = 0;//�۸�
            decimal decQty = 0;//����
            //decimal decPencent = 100;//����
            decimal decDose = m_objViewer.numericUpDown1.Value; //����
            decimal decSubTotal = 0;//��ҩƷ�ܼ�

            com.digitalwave.controls.datagrid.ctlDataGrid objGrid = m_objViewer.ctlDataGrid1;
            int intRowsCount = objGrid.RowCount;
            for (int i1 = 0; i1 < intRowsCount; i1++)
            {
                decSubTotal = 0;
                decQty = m_mthConvertObjToDecimal(objGrid[i1, 1]);
                decPrice = m_mthConvertObjToDecimal(objGrid[i1, 6]);
                //decPencent = m_mthConvertObjToDecimal(objGrid[i1, 14]);
                //decPencent /= 100;
                // *decPencent;
                strType = objGrid[i1, 9].ToString().Trim();
                //���Ϊ��ҩ�����Է���
                if (strType == "0002")
                {
                    decimal cmQty = Math.Ceiling(decQty * decDose);
                    decSubTotal = cmQty * decPrice;
                    //decSubTotal *= decDose;
                }
                else
                {
                    decSubTotal = decQty * decPrice;
                }
                decSubTotal = Convert.ToDecimal(decSubTotal.ToString("0.00"));
                decTotalMoney += decSubTotal;
            }

            return decTotalMoney;
        }
        #endregion

        #region ����ѡ�񽻷���Ŀ
        /// <summary>
        /// ����ѡ�񽻷���Ŀ
        /// </summary>
        /// <param name="p_dtbChargeItemDetail">ѡ����շ���Ŀ��ϸ</param>
        /// <param name="p_dtpDetailTableInfo">��ϸ��Ӧ��</param>
        public long m_lngSelectFeeDispose()
        {
            /*
             * 1.����һ���µ����������¼,����ԭ���Ĵ������pstauts_int״̬�ı� 
             * 2.�������ݽ���6������,����еĻ�
             * 3.update����������뵥������еĻ�,
             * 4.ҩƷ����δ���
             * 5.�½�һ������¼�����޸���Ϣ�ļ�¼
             * 6��t_opr_outpatient_orderdic ������Ŀ���в�����顢���
             * *
             */

            long lngRes = -1;

            if (m_dicSelectChargeItemID == null || m_dicSelectChargeItemID.Count == 0)
            {
                return lngRes;
            }

            //ԭ���Ĵ�����
            string strOriginalRepcideID = this.m_objViewer.txtLoadRecipeNO.Text;
            //�ռ��µĴ�����Ϣ
            string strRecipeID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            clsOutPatientRecipe_VO OPR_VO = new clsOutPatientRecipe_VO();
            OPR_VO.m_strRecordDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            OPR_VO.m_strOperatorID = m_objViewer.LoginInfo.m_strEmpID;//����ԱID
            OPR_VO.m_strRegisterID = m_objViewer.m_PatientBasicInfo.RegisterID.Trim();
            OPR_VO.m_strDoctorID = m_objViewer.m_PatientBasicInfo.DoctorID.Trim();
            OPR_VO.m_strDepID = m_objViewer.m_PatientBasicInfo.DeptID.Trim();
            OPR_VO.m_strCreateDate = m_objViewer.m_PatientBasicInfo.RegisterDate;
            OPR_VO.m_strPatientID = m_objViewer.m_PatientBasicInfo.PatientID.Trim();
            OPR_VO.m_intPStatus = 1;
            OPR_VO.m_intType = int.Parse(m_objViewer.m_cmbRecipeType.Tag.ToString());
            OPR_VO.m_strPatientType = m_objViewer.m_PatientBasicInfo.PayTypeID;
            OPR_VO.m_strRecipeType = "0";
            OPR_VO.intCreatetype = 1;
            OPR_VO.intDeptmed = 0;
            OPR_VO.strIDcard = this.m_objViewer.txtIDcard.Text.Trim();
            OPR_VO.strInsuranceID = this.m_objViewer.txtInsuranceID.Text.Trim();
            OPR_VO.m_strOutpatRecipeID = strRecipeID;

            lngRes = this.objSvc.m_lngSelectFeeDispose(m_dicSelectChargeItemID, OPR_VO, strOriginalRepcideID);

            if (this.objHashTable.Count > 0)//�Ѵ�����ŵ�һ������ID
            {
                this.objHashTable.Clear();
            }

            this.objHashTable.Add(0, OPR_VO);

            return lngRes;
            //this.m_objViewer.txtLoadRecipeNO.Text = strRecipeID;
            //this.m_objViewer.btSave.Tag = strRecipeID;
        }
        #endregion

        #region ѡ�񽻷���Ŀ��id
        /// <summary>
        /// ѡ�񽻷���Ŀ��id
        /// </summary>
        /// <param name="p_dtbChargeItemDetail"></param>
        /// <param name="p_lstOrderDicItemID"></param>
        public void m_mthGetSelectChargeItemID(DataTable p_dtbChargeItemDetail, List<string> p_lstOrderDicItemID)
        {

            if (m_dicSelectChargeItemID != null)
            {
                m_dicSelectChargeItemID.Clear();
            }

            m_dicSelectChargeItemID = new Dictionary<string, List<string>>();

            List<string> lstItemIDWM = new List<string>();
            List<string> lstItemIDCM = new List<string>();
            //����
            List<string> lstItemIDCheck = new List<string>();
            //���
            List<string> lstItemIDTest = new List<string>();
            List<string> lstItemIDOps = new List<string>();
            List<string> lstItemIDOth = new List<string>();


            int intRowCount = p_dtbChargeItemDetail.Rows.Count;
            for (int i1 = 0; i1 < intRowCount; i1++)
            {
                DataRow dtrTmp = p_dtbChargeItemDetail.Rows[i1];

                string strTmp = dtrTmp["InvType"].ToString();
                string strType = m_mthRelationInfo(strTmp);
                string strItemID = dtrTmp["ItemID"].ToString();

                if (string.IsNullOrEmpty(strItemID))
                {
                    continue;
                }

                switch (strType)
                {
                    case "0001":
                        lstItemIDWM.Add(strItemID);
                        break;
                    case "0002":
                        lstItemIDCM.Add(strItemID);
                        break;
                    case "0003":
                        lstItemIDCheck.Add(strItemID);
                        break;
                    case "0004":
                        lstItemIDTest.Add(strItemID);
                        break;
                    case "0005":
                        lstItemIDOps.Add(strItemID);
                        break;
                    case "0006":
                        lstItemIDOth.Add(strItemID);
                        break;
                }
            }

            m_dicSelectChargeItemID.Add("cm", lstItemIDCM);
            m_dicSelectChargeItemID.Add("wm", lstItemIDWM);
            m_dicSelectChargeItemID.Add("test", lstItemIDTest);
            m_dicSelectChargeItemID.Add("check", lstItemIDCheck);
            m_dicSelectChargeItemID.Add("ops", lstItemIDOps);
            m_dicSelectChargeItemID.Add("oth", lstItemIDOth);
            //���������Ŀid
            m_dicSelectChargeItemID.Add("orderdic", p_lstOrderDicItemID);
        }

        #endregion

        #region �жϵ�ǰ�����Ƿ�����ҩ������Ϣ��������ҩ����Ƭ��
        /// <summary>
        /// �жϵ�ǰ�����Ƿ�����ҩ������Ϣ��������ҩ����Ƭ��
        /// </summary>
        internal void CheckCMachine()
        {
            DataTable dt1015 = new DataTable();
            clsDcl_OPCharge dcl = new clsDcl_OPCharge();
            dcl.m_lngGetMedUsageID("1015", out dt1015);
            dcl = null;
            if (dt1015 != null && dt1015.Rows.Count > 0)
            {
                DataRow dr = dt1015.Rows[0];
                if (dr["status_int"].ToString() == "1")
                {
                    this.m_objViewer.HttpUri = dr["parmvalue_vchr"].ToString();
                    string ipAddrs = dr["note_vchr"] == DBNull.Value ? string.Empty : dr["note_vchr"].ToString().Trim();
                    if (ipAddrs != string.Empty)
                    {
                        string localIp = LocalIP();
                        string[] ips = ipAddrs.Split(';');
                        foreach (string ip in ips)
                        {
                            if (ip == localIp)
                            {
                                this.m_objViewer.IsRecipeHttpPost = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        string LocalIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            string strHostIP = ipEntry.AddressList[0].ToString();
            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                strHostIP = ipEntry.AddressList[i].ToString();
                if (strHostIP.Length <= 15)
                {
                    if ((strHostIP.Split('.')).Length == 4) break;
                }
            }
            return strHostIP;
        }
        #endregion
    }
}
