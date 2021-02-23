using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ControlLibrary;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ���ʲ�ѯ�߼�������
    /// </summary>
    public class clsCtl_QueryCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_QueryCharge()
        {
            objSvc = new clsDcl_Charge();
            objReport = new clsCtl_Report();
        }
        #endregion

        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        private clsDcl_Charge objSvc;
        /// <summary>
        /// GUI����
        /// </summary>
        com.digitalwave.iCare.gui.HIS.frmQueryCharge m_objViewer;
        /// <summary>
        /// Դ����(������ϸ)
        /// </summary>
        internal DataTable dtSource;
        /// <summary>
        /// ����Դ(������Ŀ)
        /// </summary>
        internal DataTable dtDiagSource;
        /// <summary>
        /// ����Դ��ͼ
        /// </summary>
        internal DataView dvSource;
        /// <summary>
        /// ����Դ����
        /// </summary>
        internal DataView dvMonitor;
        /// <summary>
        /// Ա��DataTable
        /// </summary>
        private DataTable dtEmployee;
        /// <summary>
        /// ����(����)DataTable
        /// </summary>
        private DataTable dtDept;
        /// <summary>
        /// �շ���Ŀ��Ʊ����
        /// </summary>
        private DataTable dtCat;
        /// <summary>
        /// ��ǰ����ID
        /// </summary>
        private string CurrAreaID = "";
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string CurrAreaName = "";
        /// <summary>
        /// ��ǰ��ʼʱ��
        /// </summary>
        private string CurrBeginDate = "";
        /// <summary>
        /// ��ǰ����ʱ��
        /// </summary>
        public string CurrEndDate = "";
        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private ArrayList objChk;
        /// <summary>
        /// ��ʼ����˳��
        /// </summary>
        internal string OrgSortStr = "chargeactive_dat asc";
        /// <summary>
        /// ���������
        /// </summary>
        private clsCtl_Report objReport;
        /// <summary>
        /// ��ǰ��Ʊ�����
        /// </summary>
        private string ChargeNo = "";
        /// <summary>
        /// �Ƿ�����帺��(ҩƷ��)
        /// </summary>
        private bool IsAllowPatchNegativeMed = false;
        /// <summary>
        /// �Ƿ�����帺��(��ҩƷ��)
        /// </summary>
        private bool IsAllowPatchNegativeNoMed = false;
        /// <summary>
        /// ��Ŀ����ʹ������ 0 �����շѱ��� 1 ��Ŀ����
        /// </summary>
        private int ItemCodeType = 1;
        /// <summary>
        /// �����Ѵ���
        /// </summary>
        private string RoundingCode = "";
        /// <summary>
        /// ҽԺ����
        /// </summary>
        private string HospitalName = "";
        /// <summary>
        /// ��Ӧ֢��� - ��ɽҽ����Ӧ֢
        /// </summary>
        internal DataTable dtSFLB = null;
        /// <summary>
        /// ��Ӧ֢��� - ��ɽҽ��
        /// </summary>
        internal DataTable dtBseSFLB = null;
        /// <summary>
        /// �Ƿ�ʹ���µķ�����ϸ�嵥��ӡ��0 �� 1 ��
        /// </summary>
        private int intIsNewOrder = 0;

        /// <summary>
        /// �������ÿ���
        /// </summary>
        internal int intDiffCostOn = 0;

        /// <summary>
        /// �Ƿ����ȡ�����ú˶�
        /// </summary>
        bool IsCanCancelCheckFee { get; set; }

        /// <summary>
        /// �Ƿ���Գ������ҩ������ڷ�ҩ
        /// </summary>
        bool IsCanSubKFMed { get; set; }

        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmQueryCharge)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            //1.
            this.m_objViewer.ucPatientInfo.m_mthSetRedraw();
            this.m_objViewer.ucPatientInfo.Status = 0;

            this.m_objViewer.dtItem.AutoGenerateColumns = false;

            // �����б�
            clsColumns_VO[] columArr = new clsColumns_VO[]{ new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                                                            new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                                                            new clsColumns_VO("��������","deptname_vchr",HorizontalAlignment.Left,130)
                                                          };

            this.m_objViewer.txtAREAID.m_strSQL = @"select '00' as deptid_chr,
                                                           'ȫԺ����' as deptname_vchr,  
                                                           'qy' as pycode_chr,
                                                           '00' as code_vchr   
                                                      from dual
                                                    union all              
                                                    select a.deptid_chr, a.deptname_vchr, a.pycode_chr, a.code_vchr
                                                      from t_bse_deptdesc a
                                                     where a.status_int = 1 
                                                       and a.attributeid = '0000003'";

            this.m_objViewer.txtAREAID.m_mthInitListView(columArr);
            this.m_objViewer.txtAREAID.m_listView.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            this.m_objViewer.llbl1.LinkVisited = true;

            this.objSvc.m_lngGetEmployee(out dtEmployee);
            this.objSvc.m_lngGetDeptArea(out dtDept, 2);
            this.objSvc.m_lngGetChargeItemCat(4, out dtCat);

            objChk = new ArrayList();
            objChk.Add(this.m_objViewer.chkSortXmdm);
            objChk.Add(this.m_objViewer.chkSortFsrq);
            objChk.Add(this.m_objViewer.chkSortYzlx);
            objChk.Add(this.m_objViewer.chkSortYzh);
            objChk.Add(this.m_objViewer.chkSortSl);
            objChk.Add(this.m_objViewer.chkSortJe);
            objChk.Add(this.m_objViewer.chkSortBq);
            objChk.Add(this.m_objViewer.chkSortFyfl);
            objChk.Add(this.m_objViewer.chkSortQzrq);

            if (clsPublic.m_strGetSysparm("1000") == "002")
            {
                this.m_objViewer.btnYBDbf.Enabled = true;
            }

            //2.


            //3.
            this.m_objViewer.dw1.LibraryList = clsPublic.PBLPath;

            //4.
            this.m_objViewer.dw2.LibraryList = clsPublic.PBLPath;

            //��ȡ����ԱĬ�Ͽ����б�
            clsDepartmentVO[] objEmpDeptArr;
            this.m_objComInfo.m_mthGetDepartmentByUserID(this.m_objViewer.LoginInfo.m_strEmpID, out objEmpDeptArr);
            if (objEmpDeptArr != null)
            {
                this.m_objViewer.ucPatientInfo.DeptArr = objEmpDeptArr;

                this.m_objViewer.ucPatientInfo.Parm_AreaID = this.m_objViewer.LoginInfo.m_strInpatientAreaID;
                this.m_objViewer.ucPatientInfo.Parm_AreaName = this.m_objViewer.LoginInfo.m_strInpatientAreaName;
            }

            //��ȡ�帺��Ȩ��
            this.m_mthGetNegativePurview();

            //��ȡ��Ŀ����ʹ������
            ItemCodeType = clsPublic.m_intGetSysParm("9008");
            if (ItemCodeType == 0)
            {
                this.m_objViewer.chkSortXmdm.Tag = "itemopcode_chr";
            }

            //��ȡ�����Ѵ���
            RoundingCode = clsPublic.m_strGetSysparm("0016");

            this.HospitalName = this.m_objComInfo.m_strGetHospitalTitle();

            //���ÿ����Ϣ
            clsPublic.SetShortCutInfo(this.m_objViewer.MdiParent, 4, " F8�� �� ֱ�Ӳ�ѯ����");
            //��ȡ�Ƿ�ʹ���µ���ϸ�嵥��ӡ
            intIsNewOrder = clsPublic.m_intGetSysParm("1146");
            // �Ƿ���������
            this.intDiffCostOn = clsPublic.m_intGetSysParm("9002");

            // 9008 
            string parm9008 = clsPublic.m_strGetSysparm("9008");
            if (!string.IsNullOrEmpty(parm9008))
            {
                string[] ps9008 = parm9008.Split(';');
                foreach (string empno in ps9008)
                {
                    if (empno == this.m_objViewer.LoginInfo.m_strEmpNo)
                    {
                        IsCanCancelCheckFee = true;
                        break;
                    }
                }
            }

            // 9010
            string parm9010 = clsPublic.m_strGetSysparm("9010");
            if (!string.IsNullOrEmpty(parm9010))
            {
                string[] ps9010 = parm9010.Split(';');
                foreach (string empno in ps9010)
                {
                    if (empno == this.m_objViewer.LoginInfo.m_strEmpNo)
                    {
                        IsCanSubKFMed = true;
                        break;
                    }
                }
            }

        }
        #endregion

        #region ��ȡ�帺��Ȩ��
        /// <summary>
        /// ��ȡ�帺��Ȩ��
        /// </summary>
        public void m_mthGetNegativePurview()
        {
            string EmpID = this.m_objViewer.LoginInfo.m_strEmpID;

            //��ҩƷ��ɫ
            System.Collections.Generic.List<string> RoleMedArr = clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0017"), ";");
            //���ҩƷ��ɫ
            System.Collections.Generic.List<string> RoleNoMedArr = clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0018"), ";");

            DataTable dt;
            long l = this.objSvc.m_lngGetEmpRole(EmpID, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string RoleID = dt.Rows[i]["roleid_chr"].ToString();

                    if (RoleMedArr.IndexOf(RoleID) >= 0)
                    {
                        IsAllowPatchNegativeMed = true;
                    }

                    if (RoleNoMedArr.IndexOf(RoleID) >= 0)
                    {
                        IsAllowPatchNegativeNoMed = true;
                    }

                }
            }
        }
        #endregion

        #region ��ո���ֵ
        /// <summary>
        /// ���
        /// </summary>
        public void m_mthClear()
        {
            //1.           

            this.m_objViewer.lvCat1.Items.Clear();
            this.m_objViewer.lvCat2.Items.Clear();

            this.m_objViewer.lblFeeInfo.Text = "";
            this.m_objViewer.lblFeeInfoSelected.Text = "";

            CurrAreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
            CurrAreaName = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaName;
            CurrBeginDate = this.m_objViewer.ucPatientInfo.BihPatient_VO.InHospitalDate;
            //if (this.m_objViewer.ucPatientInfo.BihPatient_VO.OutHospitalDate == "")
            //{
            //CurrEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            //}
            //else
            //{
            //    CurrEndDate = this.m_objViewer.ucPatientInfo.BihPatient_VO.OutHospitalDate;
            //}
            CurrEndDate = this.m_objViewer.ucPatientInfo.MaxFeeDate;

            this.m_objViewer.txtAREAID.Value = "00";
            this.m_objViewer.txtAREAID.Text = "ȫԺ����";
            this.m_objViewer.dteRq1.Value = Convert.ToDateTime(CurrBeginDate);
            this.m_objViewer.dteRq2.Value = Convert.ToDateTime(CurrEndDate);

            //2.
            this.m_objViewer.dtvDetail.Rows.Clear();

            //3.
            if (this.m_objViewer.isInint == false)
            {
                this.m_mthSelectBill(0);
            }

            //4.
            ChargeNo = "";

            if (this.m_objViewer.isInitInvo == false)
            {
                this.m_objViewer.dw2.Reset();
                clsPBNetPrint.m_mthClearInvoiceBill(this.m_objViewer.dw2, 2);
                this.m_objViewer.dw2.InsertRow(0);
                this.m_objViewer.dw2.PrintProperties.Preview = false;
            }

        }
        #endregion

        #region ��λ
        #region ����Ա��IDת��������
        /// <summary>
        /// ����Ա��IDת��������
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public string GetEmpName(string EmpID)
        {
            string Ret = "";

            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                if (EmpID == dtEmployee.Rows[i]["empid_chr"].ToString().Trim())
                {
                    Ret = dtEmployee.Rows[i]["lastname_vchr"].ToString();
                    break;
                }
            }

            return Ret;
        }
        #endregion

        #region ���ݿ���IDת���ɿ�������
        /// <summary>
        /// ���ݿ���IDת���ɿ�������
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public string GetDeptName(string DeptID)
        {
            string Ret = "";

            for (int i = 0; i < dtDept.Rows.Count; i++)
            {
                if (DeptID == dtDept.Rows[i]["deptid_chr"].ToString().Trim())
                {
                    Ret = dtDept.Rows[i]["deptname_vchr"].ToString();
                    break;
                }
            }

            return Ret;
        }
        #endregion

        #region ���ݷ�Ʊ����IDת�����������
        /// <summary>
        /// ���ݷ�Ʊ����IDת�����������
        /// </summary>
        /// <param name="TypeNo"></param>
        /// <returns></returns>
        public string GetCatName(string TypeNo)
        {
            string Ret = "";

            for (int i = 0; i < dtCat.Rows.Count; i++)
            {
                if (TypeNo == dtCat.Rows[i]["typeid_chr"].ToString().Trim())
                {
                    Ret = dtCat.Rows[i]["typename_vchr"].ToString();
                    break;
                }
            }

            return Ret;
        }
        #endregion
        #endregion

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="key"></param>
        public void m_mthShortCut(KeyEventArgs key)
        {
            if (key.Control)
            {
                if (key.KeyCode == Keys.F)
                {
                    this.m_mthFindItem();
                }
            }
            else
            {
                switch (key.KeyCode)
                {
                    case Keys.NumPad1:
                        if (this.m_objViewer.ucPatientInfo.IsFocus || this.m_objViewer.RqIsFocused)
                        {
                            return;
                        }
                        this.m_mthSelectTabpage(1);
                        break;
                    case Keys.NumPad2:
                        if (this.m_objViewer.ucPatientInfo.IsFocus || this.m_objViewer.RqIsFocused)
                        {
                            return;
                        }
                        this.m_mthSelectTabpage(2);
                        break;
                    case Keys.NumPad3:
                        if (this.m_objViewer.ucPatientInfo.IsFocus || this.m_objViewer.RqIsFocused)
                        {
                            return;
                        }
                        this.m_mthSelectTabpage(3);
                        break;
                    case Keys.NumPad4:
                        if (this.m_objViewer.ucPatientInfo.IsFocus || this.m_objViewer.RqIsFocused)
                        {
                            return;
                        }
                        this.m_mthSelectTabpage(4);
                        break;
                    case Keys.F3:
                        this.m_mthFind();
                        break;
                    case Keys.F5:
                        this.m_mthQueryCharge();
                        break;
                    case Keys.F8:
                        this.m_objViewer.ucPatientInfo.m_mthLoadArea();
                        break;
                    case Keys.Escape:
                        this.m_objViewer.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// ѡ��TabPage
        /// </summary>
        /// <param name="index"></param>
        public void m_mthSelectTabpage(int index)
        {
            switch (index)
            {
                case 1:
                    this.m_objViewer.llbl1.LinkVisited = true;
                    this.m_objViewer.llbl2.LinkVisited = false;
                    this.m_objViewer.llbl3.LinkVisited = false;
                    this.m_objViewer.llbl4.LinkVisited = false;

                    this.m_objViewer.tabControl1.SelectedIndex = 0;
                    break;
                case 2:
                    this.m_objViewer.llbl1.LinkVisited = false;
                    this.m_objViewer.llbl2.LinkVisited = true;
                    this.m_objViewer.llbl3.LinkVisited = false;
                    this.m_objViewer.llbl4.LinkVisited = false;

                    this.m_objViewer.tabControl1.SelectedIndex = 1;
                    break;
                case 3:
                    this.m_objViewer.llbl1.LinkVisited = false;
                    this.m_objViewer.llbl2.LinkVisited = false;
                    this.m_objViewer.llbl3.LinkVisited = true;
                    this.m_objViewer.llbl4.LinkVisited = false;

                    this.m_objViewer.tabControl1.SelectedIndex = 2;
                    break;
                case 4:
                    this.m_objViewer.llbl1.LinkVisited = false;
                    this.m_objViewer.llbl2.LinkVisited = false;
                    this.m_objViewer.llbl3.LinkVisited = false;
                    this.m_objViewer.llbl4.LinkVisited = true;

                    this.m_objViewer.tabControl1.SelectedIndex = 3;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthFind()
        {
            frmCommonFind f = new frmCommonFind("���Ҳ�������", this.m_objViewer.ucPatientInfo.Status);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.ucPatientInfo.m_mthFind(f.RegisterID, 3);
                if (this.m_objViewer.ucPatientInfo.IsChanged)
                {
                    this.m_mthClear();
                    this.m_mthQueryCharge();
                    this.m_mthShowDayAccounts();
                    this.m_mthShowInvoice();
                }
            }
        }
        #endregion

        #region �ܷ���
        #region ��������ʱ��β�ѯ����
        /// <summary>
        /// ��������ʱ��β�ѯ����
        /// </summary>
        public void m_mthQueryCharge()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID.Trim();
            if (RegID == "")
            {
                return;
            }

            if (this.m_objViewer.txtAREAID.Value == null || this.m_objViewer.txtAREAID.Value.ToString().Trim() == "")
            {
                MessageBox.Show("��ѡ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Convert.ToDateTime(this.m_objViewer.dteRq1.Value.ToString("yyyy-MM-dd") + " 00:00:01") > Convert.ToDateTime(this.m_objViewer.dteRq2.Value.ToString("yyyy-MM-dd") + " 00:00:01"))
            {
                MessageBox.Show("���ÿ�ʼ���ڲ��ܴ��ڽ������ڡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                clsPublic.PlayAvi("findFILE.avi", "���ڼ������ݣ����Ժ�...");

                this.m_objViewer.lvCat1.Items.Clear();
                this.m_objViewer.lvCat2.Items.Clear();
                this.m_objViewer.lblFeeInfo.Text = "";
                this.m_objViewer.lblFeeInfoSelected.Text = "";

                CurrAreaID = this.m_objViewer.txtAREAID.Value.ToString().Trim();
                CurrBeginDate = this.m_objViewer.dteRq1.Value.ToString("yyyy-MM-dd");
                CurrEndDate = this.m_objViewer.dteRq2.Value.ToString("yyyy-MM-dd");

                long l = this.objSvc.m_lngGetFeeItemByActiveType(RegID, 999, null, CurrAreaID, CurrBeginDate, CurrEndDate, out dtSource);
                long m = this.objSvc.m_lngGetFeeDiagItem(RegID, 999, null, CurrAreaID, CurrBeginDate, CurrEndDate, out dtDiagSource);
                if (l > 0 && m > 0)
                {
                    //��ʾ��Ʊ����
                    if (dtCat.Rows.Count > 0)
                    {
                        ArrayList arrcat = new ArrayList();
                        DataView dvcat = new DataView(dtSource);
                        string strDiffCostName = string.Empty;//ҩƷ������Ʊ�������� 
                        decimal decDiffCostSum = 0;
                        for (int i = 0; i < dtCat.Rows.Count; i++)
                        {
                            string invocatid = dtCat.Rows[i]["typeid_chr"].ToString().Trim();
                            decimal invosum = 0;

                            dvcat.RowFilter = "invcateid_chr = '" + invocatid + "'";
                            foreach (DataRowView drv in dvcat)
                            {
                                invosum += clsPublic.Round(clsPublic.ConvertObjToDecimal(drv["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(drv["amount_dec"]), 2);
                                decDiffCostSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(drv["totaldiffcostmoney_dec"]), 2);
                            }
                            if (string.IsNullOrEmpty(strDiffCostName) && string.Compare("3026", invocatid) == 0)
                            {
                                strDiffCostName = dtCat.Rows[i]["typename_vchr"].ToString().Trim();//��ȡҩƷ�������ֵ��е�����
                            }
                            if (invosum == 0)
                            {
                                continue;
                            }
                            clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();
                            invocat_vo.CatID = invocatid;
                            invocat_vo.CatName = dtCat.Rows[i]["typename_vchr"].ToString().Trim();
                            invocat_vo.CatSum = clsPublic.Round(invosum, 2);

                            arrcat.Add(invocat_vo);
                        }
                        //��ʾҩƷ�������༰ֵ
                        if (this.intDiffCostOn == 1)
                        {
                            clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();
                            invocat_vo.CatID = "3026";
                            invocat_vo.CatName = strDiffCostName;
                            foreach (DataRowView drv in dvcat)
                            {
                                decDiffCostSum += clsPublic.ConvertObjToDecimal(drv["totaldiffcostmoney_dec"]);
                            }
                            invocat_vo.CatSum = clsPublic.Round(decDiffCostSum, 2); ;

                            arrcat.Add(invocat_vo);
                        }

                        int no = 1;
                        decimal TotalSum = 0;
                        for (int j = 0; j < arrcat.Count; j++)
                        {
                            clsInvoiceCat_VO invocat_vo = (clsInvoiceCat_VO)arrcat[j];

                            ListViewItem lvitem = new ListViewItem(no.ToString());
                            lvitem.SubItems.Add(invocat_vo.CatName);
                            lvitem.SubItems.Add(invocat_vo.CatSum.ToString("0.00"));
                            lvitem.Tag = invocat_vo;
                            if (no <= 5)
                            {
                                this.m_objViewer.lvCat1.Items.Add(lvitem);
                            }
                            else
                            {
                                this.m_objViewer.lvCat2.Items.Add(lvitem);
                            }
                            TotalSum += invocat_vo.CatSum;
                            no++;
                        }

                        if (TotalSum > 0)
                        {
                            this.m_objViewer.lblFeeInfo.Text = "��" + TotalSum.ToString("###,##0.00");
                        }
                    }

                    //�������
                    dvSource = new DataView(dtSource);
                    this.m_mthFillData(dvSource, OrgSortStr);

                    #region ���������Ϣ

                    this.m_objViewer.picFeeCheck.Visible = false;
                    this.m_objViewer.llblFeeCheck.Visible = false;

                    // Ԥ��Ժ
                    DataTable dt = this.objSvc.GetPatientCheckFee(this.m_objViewer.ucPatientInfo.BihPatient_VO.RegisterID);
                    if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 2)
                    {
                        if (this.IsCanCancelCheckFee)
                        {
                            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "1")
                            {
                                this.m_objViewer.picFeeCheck.Visible = true;
                                this.m_objViewer.llblFeeCheck.Visible = true;
                                this.m_objViewer.llblFeeCheck.Text = "�������º˶�";
                            }
                            else
                            {
                                if ((dt == null || dt.Rows.Count == 0) || (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "0"))
                                {
                                    this.m_objViewer.picFeeCheck.Visible = true;
                                    this.m_objViewer.llblFeeCheck.Visible = true;
                                    this.m_objViewer.llblFeeCheck.Text = "���ú˶����";
                                }
                            }
                        }
                        else
                        {
                            if ((dt == null || dt.Rows.Count == 0) || (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "0"))
                            {
                                this.m_objViewer.picFeeCheck.Visible = true;
                                this.m_objViewer.llblFeeCheck.Visible = true;
                                this.m_objViewer.llblFeeCheck.Text = "���ú˶����";
                            }
                        }
                    }
                    #endregion
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="sortstr">����</param>
        public void m_mthFillData(DataView dv, string sortstr)
        {
            DataTable dtview = new DataTable();
            #region create columns
            dtview.Columns.Add("serno");
            dtview.Columns.Add("colrq");
            dtview.Columns.Add("invoname");
            dtview.Columns.Add("column1");
            dtview.Columns.Add("colxmdm");
            dtview.Columns.Add("colxmmc");
            dtview.Columns.Add("colsl");
            dtview.Columns.Add("colje");
            dtview.Columns.Add("coldj");
            dtview.Columns.Add("scale");
            dtview.Columns.Add("facttotal");
            dtview.Columns.Add("colgg");
            dtview.Columns.Add("coldw");
            dtview.Columns.Add("colszbq");
            dtview.Columns.Add("colzzys");
            dtview.Columns.Add("colfpfl");
            dtview.Columns.Add("column7");
            dtview.Columns.Add("collr");
            dtview.Columns.Add("colly");
            dtview.Columns.Add("column11");
            dtview.Columns.Add("colkdbq");
            dtview.Columns.Add("column13");
            dtview.Columns.Add("status");
            dtview.Columns.Add("colpym");

            dtview.Columns.Add("pchargeid_chr");
            dtview.Columns.Add("orderexecid_chr");
            dtview.Columns.Add("amount_dec");
            dtview.Columns.Add("unitprice_dec");
            dtview.Columns.Add("precent_dec");
            dtview.Columns.Add("orderid_chr");
            dtview.Columns.Add("pstatus_int");
            dtview.Columns.Add("chargeitemname_chr");
            dtview.Columns.Add("chargeitemid_chr");
            dtview.Columns.Add("create_dat");
            dtview.Columns.Add("totaldiffcostmoney_dec");   // ���������
            dtview.Columns.Add("requiredpay");              // ʵ�����
            dtview.Columns.Add("rptStatus");                // �����鱨��״̬�� 0 δ��; 1 �ѳ�
            dtview.Columns.Add("chargeDesc");               // ���ñ�ע����
            dtview.Columns.Add("buyprice");
            dtview.Columns.Add("itemunit2");
            #endregion

            dtview.BeginLoadData();

            dv.Sort = sortstr;
            decimal dec_PayMny = 0;//ʵ��
            decimal dec_DiffSum = 0;//����
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow dr = dv[i].Row;
                string[] sarr = new string[40];

                sarr[0] = Convert.ToString(i + 1);

                if (dr["chargeactive_dat"].ToString().Trim() != "")
                {
                    sarr[1] = Convert.ToDateTime(dr["chargeactive_dat"].ToString()).ToString("yyyyMMdd");
                }
                else
                {
                    sarr[1] = "";
                }

                string ordertype = dr["orderexectype_int"].ToString();

                if (ordertype == "0")
                {
                    ordertype = "����";
                }
                else if (ordertype == "1")
                {
                    ordertype = "����";
                }
                else if (ordertype == "2")
                {
                    ordertype = "����";
                }
                else if (ordertype == "3")
                {
                    ordertype = "����";
                }
                else if (ordertype == "4")
                {
                    ordertype = "��Ժ��ҩ";
                }
                else if (ordertype == "8")
                {
                    ordertype = "ֱ��";
                }
                else if (ordertype == "9")
                {
                    ordertype = "������";
                }
                else if (ordertype == "7")
                {
                    ordertype = "������";
                }

                sarr[2] = ordertype;
                sarr[3] = dr["recno"].ToString();
                if (ItemCodeType == 0 && dr["itemopcode_chr"].ToString().Trim() != "")
                {
                    sarr[4] = dr["itemopcode_chr"].ToString();
                }
                else
                {
                    sarr[4] = dr["itemcode_vchr"].ToString();
                }
                sarr[5] = dr["chargeitemname_chr"].ToString().Trim();
                sarr[6] = dr["amount_dec"].ToString();

                decimal d = clsPublic.Round(clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dr["amount_dec"]), 2);
                decimal decTotalDiffCost = clsPublic.Round(clsPublic.ConvertObjToDecimal(dr["totaldiffcostmoney_dec"]), 2);
                decimal dec_requiredPay = 0; // ���������
                sarr[7] = d.ToString("0.00");
                if (this.intDiffCostOn == 1)
                {
                    dec_requiredPay = d + decTotalDiffCost;
                    sarr[34] = decTotalDiffCost.ToString("0.00");
                    sarr[35] = dec_requiredPay.ToString("0.00");

                    dec_PayMny += dec_requiredPay;
                    dec_DiffSum += decTotalDiffCost;
                }
                sarr[8] = dr["unitprice_dec"].ToString();
                sarr[9] = dr["precent_dec"].ToString();
                sarr[10] = d.ToString("0.00");
                sarr[11] = dr["spec_vchr"].ToString().Trim();
                sarr[12] = dr["unit_vchr"].ToString();
                sarr[13] = GetDeptName(dr["curareaid_chr"].ToString().Trim());
                sarr[14] = dr["doctor_vchr"].ToString();
                sarr[15] = GetCatName(dr["invcateid_chr"].ToString().Trim());
                sarr[16] = dr["chargedoctor_vchr"].ToString().Trim();
                sarr[17] = GetEmpName(dr["creator_chr"].ToString().Trim());

                string activetype = dr["activatetype_int"].ToString();
                if (activetype == "1")
                {
                    activetype = "����";
                }
                else if (activetype == "2")
                {
                    activetype = "������";
                }
                else if (activetype == "3")
                {
                    activetype = "ȷ�ϼ���";
                }
                else if (activetype == "4")
                {
                    activetype = "ȷ���շ�";
                }
                else if (activetype == "5")
                {
                    activetype = "ֱ���շ�";
                }
                sarr[18] = activetype;
                if (dr["chearaccount_dat"].ToString().Trim() != "")
                {
                    sarr[19] = Convert.ToDateTime(dr["chearaccount_dat"].ToString()).ToString("yyyyMMdd");
                }
                else
                {
                    sarr[19] = "";
                }
                sarr[20] = GetDeptName(dr["createarea_chr"].ToString().Trim());
                sarr[21] = dr["execarea"].ToString();

                string Status = dr["pstatus_int"].ToString();
                if (Status == "0")
                {
                    Status = "��ȷ��";
                }
                else if (Status == "1")
                {
                    Status = "����";
                }
                else if (Status == "2")
                {
                    Status = "����";
                }
                else if (Status == "3")
                {
                    Status = "����";
                }
                else if (Status == "4")
                {
                    Status = "ֱ��";
                }
                sarr[22] = Status;
                sarr[23] = dr["itempycode_chr"].ToString();
                sarr[24] = dr["pchargeid_chr"].ToString();
                sarr[25] = dr["orderexecid_chr"].ToString();
                sarr[26] = dr["amount_dec"].ToString();
                sarr[27] = dr["unitprice_dec"].ToString();
                sarr[28] = dr["precent_dec"].ToString();
                sarr[29] = dr["orderid_chr"].ToString();
                sarr[30] = dr["pstatus_int"].ToString();
                sarr[31] = dr["chargeitemname_chr"].ToString();
                sarr[32] = dr["chargeitemid_chr"].ToString();
                sarr[33] = dr["chargeactive_dat"].ToString();

                if (dr["rptStatus"] == DBNull.Value || sarr[15].Contains("����"))
                {
                    sarr[36] = "0";
                }
                else
                {
                    sarr[36] = dr["rptStatus"].ToString();
                }
                sarr[37] = dr["des_vchr"] == DBNull.Value ? "" : dr["des_vchr"].ToString().Trim();
                sarr[38] = Function.Dec(dr["buyprice_dec"]) == 0 ? dr["unitprice_dec"].ToString() : dr["buyprice_dec"].ToString();
                sarr[39] = Function.Int(dr["itemunit2"]).ToString();
                dtview.LoadDataRow(sarr, true);
            }
            dtview.EndLoadData();
            dvMonitor = new DataView(dtview);
            this.m_objViewer.dtItem.DataSource = dvMonitor;
            this.m_mthSetRowColor();
        }
        #endregion

        #region ��������ɫ
        /// <summary>
        /// ��������ɫ
        /// </summary>
        public void m_mthSetRowColor()
        {
            for (int i = 0; i < this.m_objViewer.dtItem.Rows.Count; i++)
            {
                if (clsPublic.ConvertObjToDecimal(this.m_objViewer.dtItem.Rows[i].Cells["colje"].Value) < 0)
                {
                    this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }

                if (Math.IEEERemainder(Convert.ToDouble(i), 2) == 0)
                {
                    this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                }

                if (clsPublic.ConvertObjToDecimal(this.m_objViewer.dtItem.Rows[i].Cells["rptStatus"].Value) == 1)
                {
                    this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthFilter(int Index)
        {
            clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();

            if (Index == 1)
            {
                invocat_vo = this.m_objViewer.lvCat1.SelectedItems[0].Tag as clsInvoiceCat_VO;
            }
            else if (Index == 2)
            {
                invocat_vo = this.m_objViewer.lvCat2.SelectedItems[0].Tag as clsInvoiceCat_VO;
            }

            //clsPublic.PlayAvi("findFILE.avi", "���ڹ������ݣ����Ժ�...");
            dvSource.RowFilter = "invcateid_chr = '" + invocat_vo.CatID + "'";
            this.m_mthFillData(dvSource, OrgSortStr);
            //clsPublic.CloseAvi();
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthSort()
        {
            if (this.m_objViewer.dtItem.Rows.Count > 0)
            {
                string SortStr = "";
                string SortType = "";

                if (this.m_objViewer.rdoSortAsc.Checked)
                {
                    SortType = " Asc";
                }
                else if (this.m_objViewer.rdoSortDesc.Checked)
                {
                    SortType = " Desc";
                }

                for (int i = 0; i < objChk.Count; i++)
                {
                    CheckBox chk = objChk[i] as CheckBox;

                    if (chk.Checked)
                    {
                        SortStr += chk.Tag.ToString() + SortType + ", ";
                    }
                }

                SortStr = SortStr.Trim();

                if (SortStr.Length > 0)
                {
                    SortStr = SortStr.Substring(0, SortStr.Length - 1);

                    this.m_mthFillData(this.dvSource, SortStr);
                }
            }
        }
        #endregion

        #region ��ʾѡ�з��ý��
        /// <summary>
        /// ��ʾѡ�з��ý��
        /// </summary>
        public void m_mthShowSelectedMoney()
        {
            decimal totalsum = 0;
            decimal sbsum = 0;

            for (int i = 0; i < this.m_objViewer.dtItem.SelectedRows.Count; i++)
            {
                DataRow dr = ((DataRowView)this.m_objViewer.dtItem.SelectedRows[i].DataBoundItem).Row;

                decimal d = clsPublic.ConvertObjToDecimal(dr["amount_dec"]) * clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]);
                totalsum += clsPublic.Round(d, 2);

                d = d * clsPublic.ConvertObjToDecimal(dr["precent_dec"]) / 100;
                sbsum += clsPublic.Round(d, 2);
            }

            this.m_objViewer.lblFeeInfoSelected.Text = "�Ը�: " + sbsum.ToString("0.00") + "/" + totalsum.ToString("0.00");
        }
        #endregion

        #region ��Ŀ����
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public void m_mthItemSum()
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }

            frmQueryCharge_ItemSum f = new frmQueryCharge_ItemSum(this.m_objViewer.dtItem, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, this.m_objViewer.dteRq1.Value.ToString("yyyy-MM-dd") + " ~ " + this.m_objViewer.dteRq2.Value.ToString("yyyy-MM-dd"), this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaName);
            f.ShowDialog();
        }
        #endregion

        #region ��ӡ��ϸ�嵥
        /// <summary>
        /// ��ӡ��ϸ�嵥
        /// </summary>
        public void m_mthPrintBillDet()
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }
            string strDate = null;
            if (intIsNewOrder == 1)
            {
                TimeSpan tmSpan = this.m_objViewer.dteRq2.Value - this.m_objViewer.dteRq1.Value;
                strDate = Convert.ToString(tmSpan.Days + 1);
            }
            else
            {
                strDate = this.m_objViewer.dteRq1.Value.ToString("yyyy��MM��dd��") + " �� " + this.m_objViewer.dteRq2.Value.ToString("yyyy��MM��dd��");
            }
            string strChargeType = this.m_objViewer.ucPatientInfo.BihPatient_VO.Fee;
            this.objReport.m_mthRptChargeDet2(this.m_objViewer.dtItem, this.m_objViewer.txtAREAID.Text, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, 1, strDate, strChargeType, intIsNewOrder);
        }
        #endregion

        #region ��ӡ�����嵥
        /// <summary>
        /// ��ӡ�����嵥
        /// </summary>
        public void m_mthPrintBillCat()
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }

            ArrayList CatArr = new ArrayList();

            for (int i = 0; i < this.m_objViewer.lvCat1.Items.Count; i++)
            {
                clsInvoiceCat_VO invocat_vo = this.m_objViewer.lvCat1.Items[i].Tag as clsInvoiceCat_VO;
                CatArr.Add(invocat_vo);
            }

            for (int i = 0; i < this.m_objViewer.lvCat2.Items.Count; i++)
            {
                clsInvoiceCat_VO invocat_vo = this.m_objViewer.lvCat2.Items[i].Tag as clsInvoiceCat_VO;
                CatArr.Add(invocat_vo);
            }

            this.objReport.m_mthRptChargeCat(CatArr, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, this.m_objViewer.txtAREAID.Text.Trim(), this.m_objViewer.dteRq1.Value.ToString("yyyy.MM.dd") + " ~ " + this.m_objViewer.dteRq2.Value.ToString("yyyy.MM.dd"), this.m_objViewer.LoginInfo.m_strEmpName);
        }
        #endregion

        #region ������ϸ�嵥
        /// <summary>
        /// ������ϸ�嵥
        /// </summary>
        public void m_mthExportBillDet()
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }

            string strDate = this.m_objViewer.dteRq1.Value.ToString("yyyy��MM��dd��") + " �� " + this.m_objViewer.dteRq2.Value.ToString("yyyy��MM��dd��");
            this.objReport.m_mthRptChargeDet(this.m_objViewer.dtItem, this.m_objViewer.txtAREAID.Text, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, 2, strDate);
        }
        #endregion

        #region ��ӡ�˿��嵥
        /// <summary>
        /// ��ӡ�˿��嵥
        /// </summary>
        /// <param name="Type">1 �˷ѵ� 2 ��ͨ��(�嵥�б�)</param>    
        public void m_mthPrintAreaRefundmentBill(int Type)
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                MessageBox.Show("û�з�����Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.m_objViewer.dtItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("��ѡ�������Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.objReport.m_mthRptAreaRefundmentBill(this.m_objViewer.dtItem, this.m_objViewer.dtItem.SelectedRows[0].Cells["colKdbq"].Value.ToString(), this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, Type);
        }
        #endregion

        #region ����ҽ��DBF
        /// <summary>
        /// ����ҽ��DBF
        /// </summary>
        public void m_mthCreateDBF()
        {
            DataView dv = new DataView(dtSource);
            dv.RowFilter = "pstatus_int = 1 or pstatus_int = 2";

            clsCtl_Reckoning objReck = new clsCtl_Reckoning();
            objReck.m_blnCreateDBF(this.m_objViewer.ucPatientInfo.BihPatient_VO.IdNo, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, dv, true);
        }
        #endregion

        #region (��ɽ)������ϸ�Է��嵥
        /// <summary>
        /// (��ɽ)������ϸ�Է��嵥
        /// </summary>
        /// <param name="Type">0 ȫ�� 1 �Է� 2 ����</param>
        public void m_mthPrintSbBill_CS(int Type)
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }

            this.objReport.m_mthRptSbBill_CS(this.m_objViewer.dtItem, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.Name, this.m_objViewer.dteRq1.Value.ToString("yyyy-MM-dd") + " ~ " + this.m_objViewer.dteRq2.Value.ToString("yyyy-MM-dd"), this.m_objViewer.LoginInfo.m_strEmpNo, Type);
        }
        #endregion

        #region �˿�
        /// <summary>
        /// �˿�
        /// </summary>
        public void m_mthRefundment()
        {
            if (this.m_objViewer.dtItem.SelectedRows.Count == 0)
            {
                return;
            }

            if (IsAllowPatchNegativeMed == false && IsAllowPatchNegativeNoMed == false)
            {
                MessageBox.Show("�Բ�����û��Ȩ�޶��շ���Ŀ�˷�(��������)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.IsCanContinueRefFee() == false)
            {
                MessageBox.Show("�Բ��𣬷�������� �������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.m_objViewer.Cursor = Cursors.WaitCursor;

            List<clsParmDiagItem_VO> RefDiagArr = new List<clsParmDiagItem_VO>();

            for (int i = 0; i < this.m_objViewer.dtItem.SelectedRows.Count; i++)
            {
                DataRow dr = ((DataRowView)this.m_objViewer.dtItem.SelectedRows[i].DataBoundItem).Row;

                #region ���

                if (dr["rptStatus"].ToString() == "1")
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    //MessageBox.Show("�Բ���" + dr["chargeitemname_chr"].ToString().Trim() + "����Ӧ�ļ�鱨������ˣ������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;

                    MessageBox.Show(dr["chargeitemname_chr"].ToString().Trim() + "����Ӧ�ļ�鱨������ˣ�������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!string.IsNullOrEmpty(dr["chargeDesc"].ToString()) && dr["chargeDesc"].ToString() == "Ԥ��ҩ����")
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "ΪԤ��ҩ���ã������˷ѣ�" + Environment.NewLine + "��ѡȡ��һ�����ͬҩƷ���ý����˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string Status = dr["pstatus_int"].ToString();
                if (Status == "0")
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "Ϊ��ȷ����Ŀ�������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (Status == "3")
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "Ϊ������Ŀ�������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (Status == "4")
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "Ϊֱ����Ŀ�������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (clsPublic.ConvertObjToDecimal(dr["amount_dec"]) <= 0)
                {
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "�Ѿ��˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string InvoCat = this.m_objViewer.dtItem.SelectedRows[i].Cells["colfpfl"].Value.ToString().Trim();
                if (InvoCat.IndexOf("ҩ") >= 0)
                {
                    if (!IsAllowPatchNegativeMed)
                    {
                        this.m_objViewer.Cursor = Cursors.Default;
                        MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "ΪҩƷ����û��Ȩ�޶�ҩƷ�˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (this.objSvc.IsCanRefundment(dr["pchargeid_chr"].ToString()) == false && IsCanSubKFMed == false)
                    {
                        this.m_objViewer.Cursor = Cursors.Default;
                        MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + " (�ڷ�ҩ)����ҩ������ҩ���������˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (!IsAllowPatchNegativeNoMed)
                    {
                        this.m_objViewer.Cursor = Cursors.Default;
                        MessageBox.Show("�Բ��𣬸��� " + dr["chargeitemname_chr"].ToString().Trim() + "Ϊ��ҩƷ����û��Ȩ�޶Է�ҩƷ�˷ѡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                #endregion

                clsParmDiagItem_VO DiagItem_VO = new clsParmDiagItem_VO();
                DiagItem_VO.PchargeID = dr["pchargeid_chr"].ToString();
                DiagItem_VO.DiagName = this.m_mthShowDiagItemTip(dr);

                RefDiagArr.Add(DiagItem_VO);
            }

            this.m_objViewer.Cursor = Cursors.Default;

            this.m_mthEditRefAmount(RefDiagArr);
        }
        #endregion

        #region �����շ���Ŀ
        /// <summary>
        /// �����շ���Ŀ
        /// </summary>
        public void m_mthFindItem()
        {
            if (this.m_objViewer.dtItem.Rows.Count == 0)
            {
                return;
            }

            frmAidFindItem fItem = new frmAidFindItem(this.m_objViewer.dtItem, 1);
            if (fItem.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewRow dr in this.m_objViewer.dtItem.SelectedRows)
                {
                    dr.Selected = false;
                }

                this.m_objViewer.dtItem.CurrentCell = this.m_objViewer.dtItem.Rows[fItem.Row].Cells[0];

                this.m_objViewer.dtItem.Refresh();
            }
        }
        #endregion

        #region ������ϸ��Ӧ������Ŀ
        /// <summary>
        /// ������ϸ��Ӧ������Ŀ
        /// </summary>
        public void m_mthDiagItem()
        {
            if (dtDiagSource != null && dtDiagSource.Rows.Count > 0)
            {
                frmQueryCharge_DiagItem fDiagItem = new frmQueryCharge_DiagItem(dtDiagSource, dtSource, this);
                fDiagItem.IsAllowPatchNegativeMed = this.IsAllowPatchNegativeMed;
                fDiagItem.IsAllowPatchNegativeNoMed = this.IsAllowPatchNegativeNoMed;
                if (fDiagItem.ShowDialog() == DialogResult.OK)
                {
                    this.m_mthEditRefAmount(fDiagItem.RefDiagArr);
                }
            }
        }
        #endregion

        #region �༭�˿�����
        /// <summary>
        /// �༭�˿�����
        /// </summary>
        /// <param name="p_RefDiagArr"></param>
        public void m_mthEditRefAmount(List<clsParmDiagItem_VO> p_RefDiagArr)
        {
            try
            {
                frmAidEditAmount fedit = new frmAidEditAmount(this.dtSource, p_RefDiagArr);
                if (fedit.ShowDialog() == DialogResult.OK)
                {
                    List<clsBihRefCharge_VO> ChargeIDArr = fedit.ChargeIDArr;

                    long l = this.objSvc.m_lngPatchRefundment(ChargeIDArr, this.m_objViewer.LoginInfo.m_strEmpID);
                    if (l > 0)
                    {
                        this.m_objViewer.Cursor = Cursors.Default;
                        this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                    }
                    else
                    {
                        MessageBox.Show("�˷�ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region ��ʾ������Ŀ��Ϣ
        /// <summary>
        /// ��ʾ������Ŀ��Ϣ
        /// </summary>
        /// <param name="p_dr"></param>
        public string m_mthShowDiagItemTip(DataRow p_dr)
        {
            if (dtDiagSource != null && dtDiagSource.Rows.Count > 0)
            {
                DataView dvDiagItem = new DataView(dtDiagSource);

                for (int i = 0; i < 3; i++)
                {
                    string strfilter = "";

                    //ע��˳��#1 - #3 - #2
                    if (i == 0)
                    {
                        strfilter = "orderflag = '#1'";
                    }
                    else if (i == 1)
                    {
                        strfilter = "orderflag = '#3'";
                    }
                    else if (i == 2)
                    {
                        strfilter = "orderflag = '#2'";
                    }

                    bool blnStatus = false;
                    dvDiagItem.RowFilter = strfilter;

                    for (int j = 0; j < dvDiagItem.Count; j++)
                    {
                        DataRow dr = dvDiagItem[j].Row;

                        if (i == 0)
                        {
                            if (dr["orderexecid"].ToString().Trim() == p_dr["orderexecid_chr"].ToString().Trim())
                            {
                                blnStatus = true;
                            }
                        }
                        else if (i == 1)
                        {
                            if (dr["orderexecid"].ToString().Trim() == p_dr["pchargeid_chr"].ToString().Trim())
                            {
                                blnStatus = true;
                            }
                        }
                        else if (i == 2)
                        {
                            if (dr["orderid"].ToString().Trim() == p_dr["orderid_chr"].ToString().Trim() && dr["orderexecid"].ToString().Trim() == p_dr["orderexecid_chr"].ToString().Trim())
                            {
                                blnStatus = true;
                            }
                        }

                        if (blnStatus)
                        {
                            //����������Ŀ��Ϣ
                            clsPublic.SetShortCutInfo(this.m_objViewer.MdiParent, 3, " ������Ŀ �� " + dr["ordername"].ToString().Trim());
                            return dr["ordername"].ToString().Trim();
                        }
                    }

                    clsPublic.SetShortCutInfo(this.m_objViewer.MdiParent, 3, "");
                }
            }
            return "";
        }
        #endregion
        #endregion

        #region ����
        #region ����(��ʾ����)
        /// <summary>
        /// ����(��ʾ����)
        /// </summary>
        public void m_mthShowDayAccounts()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;
            if (RegID == "")
            {
                return;
            }

            this.m_objViewer.tv1.Nodes.Clear();

            //���ڵ�id
            string rootId = "root";
            //���ڵ�Text
            string rootName = "����Ŀ¼";

            //�����ڵ�
            TreeNode tnRoot = new TreeNode(rootName);
            tnRoot.Tag = rootId;
            tnRoot.ImageIndex = 2;
            tnRoot.SelectedImageIndex = 2;
            this.m_objViewer.tv1.Nodes.Add(tnRoot);

            //��ʾ����
            string tnText = "";

            DataTable dt;
            long l = this.objSvc.m_lngGetPatientDayaccountsByRegID(RegID, out dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tnText = dt.Rows[i]["orderno_int"].ToString() + "�� " + Convert.ToDateTime(dt.Rows[i]["square_dat"].ToString()).ToString("yyyyMMddHHmm");

                    TreeNode tn = new TreeNode(tnText);
                    tn.Tag = dt.Rows[i]["dayaccountid_chr"].ToString();
                    tn.ImageIndex = 3;
                    tn.SelectedImageIndex = 3;

                    if (clsPublic.ConvertObjToDecimal(dt.Rows[i]["charge_dec"]) == clsPublic.ConvertObjToDecimal(dt.Rows[i]["clearchg_dec"]))
                    {
                        tn.ForeColor = Color.RoyalBlue;
                    }

                    tnRoot.Nodes.Add(tn);
                }
            }

            this.m_objViewer.tv1.ExpandAll();
        }
        #endregion

        #region ��ʾ������ϸ
        /// <summary>
        /// ��ʾ������ϸ
        /// </summary>
        /// <param name="DayAccountID"></param>
        public void m_mthShowDayAcctDet(string DayAccountID)
        {
            this.m_objViewer.dtvDetail.Rows.Clear();

            DataTable dt;
            long l = this.objSvc.m_lngGetChargeInfoByID(DayAccountID, 2, out dt);
            if (l > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = "chargeactive_dat asc, chargeitemid_chr asc";

                int rowno = 1;
                ArrayList RowArr = new ArrayList();

                for (int i = 0; i < dv.Count; i++)
                {
                    //if (RowArr.IndexOf(i) >= 0)
                    //{
                    //    continue;
                    //}

                    DataRow dr = dv[i].Row;

                    string[] sarr = new string[11];

                    string itemid = dr["chargeitemid_chr"].ToString().Trim();
                    string price = dr["unitprice_dec"].ToString().Trim();
                    string statusid = dr["pstatus_int"].ToString();
                    decimal amount = clsPublic.ConvertObjToDecimal(dr["amount_dec"]);

                    decimal d = clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dr["amount_dec"]);
                    decimal totalmoney = d;
                    decimal acctmoney = d * clsPublic.ConvertObjToDecimal(dr["precent_dec"]) / 100;

                    #region ��ͬ��ϲ�
                    //for (int j = i + 1; j < dv.Count; j++)
                    //{
                    //    if (dv[j].Row["chargeitemid_chr"].ToString().Trim() == itemid &&
                    //        dv[j].Row["unitprice_dec"].ToString().Trim() == price &&
                    //        dv[j].Row["pstatus_int"].ToString() == statusid)
                    //    {
                    //        amount += clsPublic.ConvertObjToDecimal(dv[j].Row["amount_dec"]);

                    //        //����
                    //        if (statusid == "3" || statusid == "4")
                    //        {
                    //            totalmoney += clsPublic.ConvertObjToDecimal(dv[j].Row["totalmoney_dec"]);
                    //            acctmoney += clsPublic.ConvertObjToDecimal(dv[j].Row["acctmoney_dec"]);                               
                    //        }
                    //        else
                    //        {
                    //            d = clsPublic.ConvertObjToDecimal(dv[j].Row["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dv[j].Row["amount_dec"]);
                    //            totalmoney += d;
                    //            acctmoney += d * clsPublic.ConvertObjToDecimal(dv[j].Row["precent_dec"]) / 100;
                    //        }

                    //        RowArr.Add(j);
                    //    }
                    //}   
                    #endregion

                    sarr[0] = rowno.ToString();
                    sarr[1] = "";
                    sarr[2] = "";
                    sarr[3] = dr["ipinvoname"].ToString().Trim();
                    sarr[4] = dr["chargeitemname_chr"].ToString().Trim();
                    sarr[5] = amount.ToString();
                    sarr[6] = price;
                    sarr[7] = totalmoney.ToString("0.00");
                    sarr[8] = dr["precent_dec"].ToString();
                    if (acctmoney > 0)
                    {
                        sarr[9] = acctmoney.ToString("0.00");
                    }
                    else
                    {
                        sarr[9] = "";
                    }

                    //��ɫ
                    Color FCR = Color.Black;

                    //״̬

                    string StatusName = "";
                    if (statusid == "0")
                    {
                        StatusName = "��ȷ��";
                        FCR = Color.FromArgb(200, 0, 0);
                    }
                    else if (statusid == "1")
                    {
                        StatusName = "����";
                        FCR = Color.FromArgb(200, 0, 0);
                    }
                    else if (statusid == "2")
                    {
                        StatusName = "����";
                    }
                    else if (statusid == "3")
                    {
                        StatusName = "����";
                        FCR = Color.RoyalBlue;
                    }
                    else if (statusid == "4")
                    {
                        StatusName = "ֱ��";
                        FCR = Color.RoyalBlue;
                    }
                    sarr[10] = StatusName;
                    rowno++;

                    int row = this.m_objViewer.dtvDetail.Rows.Add(sarr);
                    this.m_objViewer.dtvDetail.Rows[row].Tag = dr;
                    this.m_objViewer.dtvDetail.Rows[row].DefaultCellStyle.ForeColor = FCR;

                    if (Math.IEEERemainder(Convert.ToDouble(row + 1), 2) == 0)
                    {
                        this.m_objViewer.dtvDetail.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }
                }
            }
        }
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        public void m_mthPatchDayAccount()
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID == "")
            {
                MessageBox.Show("��ѡ���ˡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmPatchDayAccount fP = new frmPatchDayAccount(this.m_objViewer.ucPatientInfo.BihPatient_VO);
                fP.ShowDialog();
                if (fP.ModifyFlag)
                {
                    this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                }
            }
        }
        #endregion
        #endregion

        #region һ���嵥
        #region ��ʾ�嵥
        /// <summary>
        /// ѡ���嵥����
        /// </summary>
        /// <param name="type"></param>
        public void m_mthSelectBill(int type)
        {
            this.m_objViewer.dw1.Reset();

            if (type == 0)
            {
                //this.m_objViewer.dw1.DataWindowObject = "d_bih_everydaybill_entry";
                if (this.intDiffCostOn == 1)
                    this.m_objViewer.dw1.DataWindowObject = "d_bih_everydaybill_entry2_diff";
                else
                    this.m_objViewer.dw1.DataWindowObject = "d_bih_everydaybill_entry2";
            }
            else if (type == 1)
            {
                if (this.intDiffCostOn == 1)
                    this.m_objViewer.dw1.DataWindowObject = "d_everydaybill_diff";
                else
                    this.m_objViewer.dw1.DataWindowObject = "d_everydaybill";
            }

            this.m_objViewer.dw1.InsertRow(0);
            this.m_objViewer.dw1.Modify("t_title.text = '" + this.objReport.HospitalName + this.m_objViewer.dw1.Describe("t_title.text") + "'");
            this.m_objViewer.dw1.PrintProperties.Preview = false;
            this.m_objViewer.dw1.Refresh();
        }
        #endregion

        #region ��ʾ�嵥
        /// <summary>
        /// ��ʾ�嵥
        /// </summary>
        public void m_mthShowBill()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;
            if (RegID == "")
            {
                return;
            }

            string BillDate = this.m_objViewer.dteRq.Value.ToString("yyyy-MM-dd");

            //һ���嵥�Զ��屨����
            string RepID = "0003";

            clsPublic.PlayAvi("findFILE.avi", "���������嵥��Ϣ�����Ժ�...");
            try
            {
                if (this.m_objViewer.cboType.SelectedIndex == 0)
                {
                    //this.objReport.m_mthRptEveryDayBillEntry(RegID, BillDate, 2, ItemCodeType, this.m_objViewer.dw1);
                    this.objReport.m_mthRptEveryDayBillEntry2(RegID, BillDate, 2, ItemCodeType, this.m_objViewer.dw1);
                }
                else if (this.m_objViewer.cboType.SelectedIndex == 1)
                {
                    this.objReport.m_mthRptEveryDayBill(RepID, RegID, BillDate, 2, this.m_objViewer.dw1);
                }
                else if (this.m_objViewer.cboType.SelectedIndex == 2)
                {
                    this.objReport.m_mthRptEveryDayBillCate(RegID, BillDate, 2, ItemCodeType, this.m_objViewer.dw1);
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion
        #endregion

        #region ��Ʊ
        #region ����(��ʾ��ƱĿ¼)
        /// <summary>
        /// ����(��ʾ��ƱĿ¼)
        /// </summary>
        public void m_mthShowInvoice()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;
            if (RegID == "")
            {
                return;
            }

            this.m_objViewer.tv2.Nodes.Clear();

            //���ڵ�id
            string rootId = "root";
            //���ڵ�Text
            string rootName = "��ƱĿ¼";

            //�����ڵ�
            TreeNode tnRoot = new TreeNode(rootName);
            tnRoot.Tag = rootId;
            tnRoot.ImageIndex = 2;
            tnRoot.SelectedImageIndex = 2;
            this.m_objViewer.tv2.Nodes.Add(tnRoot);

            //��ʾ����
            string tnText = "";

            DataTable dt;
            long l = this.objSvc.m_lngGetInvoiceInfoByRegID(RegID, 2, out dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                Hashtable has = new Hashtable();

                DataView dv = new DataView(dt);
                dv.Sort = "invono asc";

                foreach (DataRowView drv in dv)
                {
                    string invono = drv["invono"].ToString();

                    if (!has.ContainsKey(invono))
                    {
                        has.Add(invono, drv);
                    }
                    else
                    {
                        if (drv["status_int"].ToString() == "2")
                        {
                            has[invono] = drv;
                        }
                    }
                }

                ArrayList invoarr = new ArrayList();
                invoarr.AddRange(has.Values);

                for (int i = 0; i < invoarr.Count; i++)
                {
                    DataRowView drv = invoarr[i] as DataRowView;

                    string status = drv["status"].ToString().Trim();
                    if (status == "0")
                    {
                        status = "����";
                    }
                    else if (status == "1")
                    {
                        status = "����";
                    }
                    else if (status == "2")
                    {
                        status = "��Ʊ";
                    }
                    else if (status == "3")
                    {
                        status = "�ָ�";
                    }
                    else if (status == "999")
                    {
                        status = "�ش�";
                    }

                    tnText = drv["invono"].ToString() + " " + status;

                    TreeNode tn = new TreeNode(tnText);
                    tn.Tag = drv;
                    tn.ImageIndex = 4;
                    tn.SelectedImageIndex = 4;

                    tnRoot.Nodes.Add(tn);
                }

                this.m_objViewer.tv2.ExpandAll();
            }
        }
        #endregion

        #region ��ʾ��Ʊ��ϸ
        /// <summary>
        /// ��ʾ��Ʊ��ϸ
        /// </summary>
        /// <param name="drv"></param>
        public void m_mthShowInvoDet(DataRowView drv)
        {
            string RepeatPrtInvono = "";

            ChargeNo = drv["chargeno"].ToString();

            if (drv["status"].ToString() == "999")
            {
                RepeatPrtInvono = drv["invono"].ToString();
            }

            this.m_objViewer.btnInvoDet.Tag = drv["invono"].ToString();
            if (this.intDiffCostOn == 1)
            {
                this.m_objViewer.dw2.LibraryList = Application.StartupPath + @"\pb_Invioce.pbl";
                this.m_objViewer.dw2.DataWindowObject = "d_invoice_gd_diff";
            }
            clsPBNetPrint.m_mthPreviewInvoiceBill(ChargeNo, RepeatPrtInvono, this.m_objViewer.dw2, 2, this.HospitalName);
        }
        #endregion

        #region ԭ�Ŵ�ӡ��Ʊ
        /// <summary>
        /// ԭ�Ŵ�ӡ��Ʊ
        /// </summary>
        public void m_mthPrintInvoice()
        {
            if (ChargeNo == "")
            {
                return;
            }

            clsPBNetPrint.m_mthPrintInvoiceBill(ChargeNo, "", 2, this.HospitalName);
        }
        #endregion

        #region ��ʾ��Ʊ��ϸ
        /// <summary>
        /// ��ʾ��Ʊ��ϸ
        /// </summary>
        /// <param name="InvoiceNO"></param>
        public void m_mthShowInvoiceEntry(string InvoiceNO)
        {
            this.objReport.m_mthRptInvoiceEntry(InvoiceNO.ToUpper());
        }
        #endregion
        #endregion

        #region �Ƿ������Ӧ֢
        /// <summary>
        /// �Ƿ������Ӧ֢
        /// </summary>
        internal void m_mthChangeSFLB()
        {
            // -- ����Ҫ���Ȩ��


            DataView dtv1 = this.m_objViewer.dtItem.DataSource as DataView;
            DataTable dt = dtv1.ToTable();

            Dictionary<string, int> m_gdicItemIDs = new Dictionary<string, int>();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["pstatus_int"].ToString() != "1" && dr["pstatus_int"].ToString() != "2")
                {
                    continue;
                }

                if (!m_gdicItemIDs.ContainsKey(dr["chargeitemid_chr"].ToString()))
                {
                    m_gdicItemIDs.Add(dr["chargeitemid_chr"].ToString(), 1);
                }
            }

            Dictionary<string, string> m_gdicSFlB = null;
            long lngRes = this.objSvc.m_lngCheckChangeSFLB(m_gdicItemIDs, out m_gdicSFlB);
            string PayNo = string.Empty;
            lngRes = this.objSvc.m_lngGetPatientPayTypeSFLBBH(this.m_objViewer.ucPatientInfo.BihPatient_VO.PayTypeID, out PayNo);

            if (m_gdicSFlB != null && m_gdicSFlB.Count > 0)
            {
                #region init dtSFLB
                if (dtSFLB != null)
                {
                    dtSFLB.Rows.Clear();
                }
                else
                {
                    DataColumn[] m_DataColumn = new DataColumn[]{
                    new DataColumn("chargeitemID",typeof(string)), new DataColumn("chargeitemname",typeof(string)),
                    new DataColumn("pchargeid",typeof(string)), new DataColumn("creatdat",typeof(DateTime)),
                    new DataColumn("chargeitemcode",typeof(string)), new DataColumn("sflb",typeof(string)),
                    new DataColumn("amount",typeof(decimal)),new DataColumn("patchamount",typeof(decimal)),
                    new DataColumn("unit",typeof(string))};

                    dtSFLB = new DataTable();
                    dtSFLB.BeginInit();
                    dtSFLB.Columns.AddRange(m_DataColumn);
                    dtSFLB.EndInit();
                    dtSFLB.Columns["patchamount"].DefaultValue = 0;

                }
                #endregion

                dtSFLB.BeginLoadData();
                DataRow m_objOneRow = null;
                List<string> m_gslstPChargeID = new List<string>(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    if ((dr["pstatus_int"].ToString() != "1" && dr["pstatus_int"].ToString() != "2") || Convert.ToDecimal(dr["AMOUNT_DEC"].ToString()) < 0)
                    {
                        continue;
                    }

                    if (!m_gdicSFlB.ContainsKey(dr["CHARGEITEMID_CHR"].ToString()))
                    {
                        continue;
                    }

                    m_objOneRow = dtSFLB.NewRow();

                    m_objOneRow["chargeitemID"] = dr["CHARGEITEMID_CHR"];
                    m_objOneRow["chargeitemname"] = dr["CHARGEITEMNAME_CHR"];
                    m_objOneRow["pchargeid"] = dr["PCHARGEID_CHR"];

                    m_objOneRow["creatdat"] = dr["CREATE_DAT"];
                    m_objOneRow["chargeitemcode"] = dr["colxmdm"];
                    m_objOneRow["sflb"] = PayNo;

                    m_objOneRow["amount"] = dr["amount_dec"];
                    m_objOneRow["unit"] = dr["coldw"];
                    m_gslstPChargeID.Add(dr["pchargeid_chr"].ToString());

                    dtSFLB.Rows.Add(m_objOneRow);
                }
                dtSFLB.EndLoadData();
                dtSFLB.AcceptChanges();
                m_gslstPChargeID.TrimExcess();

                dtSFLB.DefaultView.Sort = "chargeitemID, pchargeid";
                dtSFLB = dtSFLB.DefaultView.ToTable();

                Dictionary<string, string> m_gdicChargeSFLB = null;
                Dictionary<string, decimal> p_gdicPatchAmount = null;
                Dictionary<string, List<string>> p_gdicPatchList = null;
                lngRes = this.objSvc.m_lngGetPatientChargeSFLB(m_gslstPChargeID, out m_gdicChargeSFLB, out p_gdicPatchAmount, out p_gdicPatchList);

                if (p_gdicPatchAmount != null && p_gdicPatchAmount.Count > 0)
                {
                    for (int i = 0; i < dtSFLB.Rows.Count; i++)
                    {
                        m_objOneRow = dtSFLB.Rows[i];

                        if (p_gdicPatchAmount.ContainsKey(m_objOneRow["pchargeid"].ToString()))
                        {
                            m_objOneRow["patchamount"] = p_gdicPatchAmount[m_objOneRow["pchargeid"].ToString()];
                        }
                    }
                }
                dtSFLB.AcceptChanges();

                if (!(this.dtBseSFLB != null && this.dtBseSFLB.Rows.Count < 1))
                {
                    lngRes = this.objSvc.m_lngGetSFLB_ForZjwsy(out this.dtBseSFLB);
                }

                frmCSYBShiyingzheng objFrm = new frmCSYBShiyingzheng(dtSFLB, m_gdicChargeSFLB, this.dtBseSFLB);

                if (objFrm.ShowDialog() == DialogResult.OK)
                {
                    if (objFrm.m_glstObjSFLB.Count > 0)
                    {
                        List<clsSFLB_log> m_glstOjbSFLB = objFrm.m_glstObjSFLB;
                        List<clsSFLB_log> m_glstALLSFLB = new List<clsSFLB_log>();
                        clsSFLB_log m_objPatchCharge = null;
                        for (int i = 0; i < m_glstOjbSFLB.Count; i++)
                        {
                            if (p_gdicPatchList != null)
                            {
                                if (p_gdicPatchList.ContainsKey(m_glstOjbSFLB[i].m_strPChargeID))
                                {
                                    for (int j = 0; j < p_gdicPatchList[m_glstOjbSFLB[i].m_strPChargeID].Count; j++)
                                    {
                                        m_objPatchCharge = new clsSFLB_log();
                                        m_glstOjbSFLB[i].m_mthCopyTo(m_objPatchCharge);
                                        m_objPatchCharge.m_strPChargeID = p_gdicPatchList[m_glstOjbSFLB[i].m_strPChargeID][j].Trim();
                                        m_glstALLSFLB.Add(m_objPatchCharge);
                                    }
                                }
                            }
                        }
                        m_glstALLSFLB.AddRange(m_glstOjbSFLB);
                        lngRes = this.objSvc.m_lngSetChargeSFLB_Zjwsy(m_glstALLSFLB, this.m_objViewer.LoginInfo.m_strEmpID,
                            this.m_objViewer.LoginInfo.m_strEmpName);
                        if (lngRes > 0)
                        {
                            MessageBox.Show("������Ӧ֢���ɹ���");
                        }
                        else
                        {
                            MessageBox.Show("������Ӧ֢���ʧ�ܡ�");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("δ��鵽��Ӧ�����ݣ���ȷ�������²�����");
            }
        }
        #endregion

        #region Ƕ��ʽ�籣���㵥��ӡ
        /// <summary>
        /// �籣���㵥��ӡ
        /// </summary>
        public void m_mthYBPrintBillDet()
        {
            clsCtl_YBChargeZY ctlYBChargeZY = new clsCtl_YBChargeZY();
            ctlYBChargeZY.m_mthYBChang(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpNo);
        }
        #endregion

        #region ���÷��������Ϣ
        /// <summary>
        /// ���÷��������Ϣ
        /// </summary>
        internal void SetPatFeeCheck()
        {
            int ret = 0;
            string info = this.m_objViewer.llblFeeCheck.Text;
            if (info == "�������º˶�")
            {
                if (MessageBox.Show("�������/Yes���󣬽��������˷ѣ���ȷ�ϣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ret = this.objSvc.CancelPatientCheckFee(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID);
                }
                else
                {
                    return;
                }
            }
            else if (info == "���ú˶����")
            {
                string itemKey = string.Empty;
                decimal itemNum = 0;
                Dictionary<string, decimal> dicItemUnit2HourDec = new Dictionary<string, decimal>();
                Dictionary<string, decimal> dicItemUnit2DayDec = new Dictionary<string, decimal>();
                for (int i = 0; i < this.m_objViewer.dtItem.Rows.Count; i++)
                {
                    if (Math.Abs(clsPublic.ConvertObjToDecimal(this.m_objViewer.dtItem.Rows[i].Cells["colje"].Value)) < Math.Abs(clsPublic.ConvertObjToDecimal(this.m_objViewer.dtItem.Rows[i].Cells["colTotalDiffCost"].Value)))
                    {
                        MessageBox.Show("��" + Convert.ToString(i + 1) + "�� ��" + this.m_objViewer.dtItem.Rows[i].Cells["colxmmc"].Value.ToString() + "�� ��������������Ŀ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                        this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        return;
                    }

                    // �շѵ�λ��1 Сʱ; 2 ��
                    if (Function.Int(this.m_objViewer.dtItem.Rows[i].Cells["itemunit2"].Value) == 1)
                    {
                        itemKey = this.m_objViewer.dtItem.Rows[i].Cells["colrq"].Value.ToString() + " " + this.m_objViewer.dtItem.Rows[i].Cells["colxmdm"].Value.ToString() + " " + this.m_objViewer.dtItem.Rows[i].Cells["colxmmc"].Value.ToString();
                        itemNum = Function.Dec(this.m_objViewer.dtItem.Rows[i].Cells["colsl"].Value.ToString());
                        if (dicItemUnit2HourDec.ContainsKey(itemKey))
                        {
                            dicItemUnit2HourDec[itemKey] += itemNum;
                        }
                        else
                        {
                            dicItemUnit2HourDec.Add(itemKey, itemNum);
                        }
                    }
                    else if (Function.Int(this.m_objViewer.dtItem.Rows[i].Cells["itemunit2"].Value) == 2)
                    {
                        itemKey = this.m_objViewer.dtItem.Rows[i].Cells["colxmdm"].Value.ToString() + " " + this.m_objViewer.dtItem.Rows[i].Cells["colxmmc"].Value.ToString();
                        itemNum = Function.Dec(this.m_objViewer.dtItem.Rows[i].Cells["colsl"].Value.ToString());
                        if (dicItemUnit2DayDec.ContainsKey(itemKey))
                        {
                            dicItemUnit2DayDec[itemKey] += itemNum;
                        }
                        else
                        {
                            dicItemUnit2DayDec.Add(itemKey, itemNum);
                        }
                    }
                }

                string errorInfo = string.Empty;
                if (dicItemUnit2HourDec.Count > 0)
                {
                    errorInfo = string.Empty;
                    foreach (KeyValuePair<string, decimal> item in dicItemUnit2HourDec)
                    {
                        if ((int)item.Value > 24)
                        {
                            errorInfo += item.Key + "  ����:" + item.Value + Environment.NewLine;
                        }
                    }
                    if (errorInfo != string.Empty)
                    {
                        MessageBox.Show(errorInfo, "�����շѵ�λΪСʱ����Ŀһ���շѳ���24Сʱ");
                        return;
                    }
                }

                if (dicItemUnit2DayDec.Count > 0)
                {
                    errorInfo = string.Empty;
                    foreach (KeyValuePair<string, decimal> item in dicItemUnit2DayDec)
                    {
                        if ((int)item.Value > this.m_objViewer.ucPatientInfo.BihPatient_VO.Days)
                        {
                            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Days == 0 && (int)item.Value == 1)
                            {
                                continue;
                            }
                            else
                            {
                                errorInfo += item.Key + "  ����:" + item.Value + Environment.NewLine;
                            }
                        }
                    }
                    if (errorInfo != string.Empty)
                    {
                        MessageBox.Show(errorInfo, "�����շѵ�λΪ�����Ŀ��������סԺ����");
                        return;
                    }
                }

                if (MessageBox.Show("�������/Yes���󣬽��������ٽ����˷ѣ���ȷ�ϣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ret = this.objSvc.SavePatientCheckFee(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID);
                }
                else
                {
                    return;
                }
            }
            if (ret > 0)
            {
                MessageBox.Show(info + " �����ɹ�!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.m_objViewer.picFeeCheck.Visible = false;
                this.m_objViewer.llblFeeCheck.Visible = false;
            }
            else
            {
                MessageBox.Show(info + " ����ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region �Ƿ���������˷�
        /// <summary>
        /// �Ƿ���������˷�
        /// </summary>
        /// <returns></returns>
        bool IsCanContinueRefFee()
        {
            // Ԥ��Ժ
            DataTable dt = this.objSvc.GetPatientCheckFee(this.m_objViewer.ucPatientInfo.BihPatient_VO.RegisterID);
            if ((dt == null || dt.Rows.Count == 0) || (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region �޸���Ӧ֢
        /// <summary>
        /// �޸���Ӧ֢
        /// </summary>
        internal void ModifySYZ()
        {
            DataView dv = this.m_objViewer.dtItem.DataSource as DataView;
            DataTable dt = dv.ToTable();
            List<string> lstPChargeId = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lstPChargeId.Add(dr["pchargeid_chr"].ToString());
            }
            if (lstPChargeId.Count == 0)
            {
                MessageBox.Show("���޼�¼��");
                return;
            }

            frmModifySYZ frm = new frmModifySYZ(lstPChargeId);
            frm.ShowDialog();
        }
        #endregion

    }
}
