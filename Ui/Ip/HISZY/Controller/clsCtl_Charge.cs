using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����ҵ�����߼�������
    /// </summary>
    public class clsCtl_Charge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_Charge()
        {
            objSvc = new clsDcl_Charge();
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
        com.digitalwave.iCare.gui.HIS.frmCharge m_objViewer;
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
        /// ������Ŀ����Դ
        /// </summary>
        internal DataTable dtSource;
        #endregion

        #region ��鷢Ʊ�Ź���
        /// <summary>
        /// ��鷢Ʊ�Ź���
        /// </summary>
        /// <returns>��ȷ true ���� false</returns>
        public bool m_blnCheckInvoiceNoExpression(string CurrInvoNo)
        {
            string InvoExp = clsPublic.m_strReadXML("BeInHospital", "InvoiceNoExp", "AnyOne");
            Regex r = new Regex(InvoExp);
            Match m = r.Match(CurrInvoNo);
            if (m.Success)
            {
                return true;
            }
            else
            {
                MessageBox.Show("��ǰ��Ʊ�ŵı�Ź�����ȷ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmCharge)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            this.m_objViewer.ucPatientInfo.m_mthSetRedraw();
            if (this.m_objViewer.ChargeType == "1")
            {
                this.m_objViewer.ucPatientInfo.Status = 2;
            }
            else
            {
                this.m_objViewer.ucPatientInfo.Status = 9;
            }
            this.m_objViewer.ucPatientInfo.ShowFeeCheckStatusFlag = true;

            this.objSvc.m_lngGetEmployee(out dtEmployee);
            this.objSvc.m_lngGetDeptArea(out dtDept, 2);
            this.objSvc.m_lngGetChargeItemCat(4, out dtCat);

            this.m_objViewer.lblInfo.Visible = false;
            this.m_objViewer.lblDays.Visible = false;
        }
        #endregion

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="key"></param>
        public void m_mthShortCut(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
                case Keys.F3:
                    this.m_mthFind();
                    break;
                case Keys.F8:
                    this.m_mthCharge();
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
            frmCommonFind f = new frmCommonFind("������Ժ��������", this.m_objViewer.ucPatientInfo.Status);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.ucPatientInfo.m_mthFind(f.RegisterID, 3);
                if (this.m_objViewer.ucPatientInfo.IsChanged)
                {
                    this.m_mthGetData();
                }
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

        #region �Զ������������
        /// <summary>
        /// �Զ������������
        /// </summary>
        public void m_mthGetData()
        {
            if (this.m_objViewer.ChargeType == "1")
            {
                //this.m_objViewer.Cursor = Cursors.WaitCursor;
                //if (clsPublic.m_blnChargeContinueItem(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID) == false)
                //{
                //    this.m_objViewer.Cursor = Cursors.Default;
                //    MessageBox.Show("���ݽ����쳣��������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //this.m_objViewer.Cursor = Cursors.Default;
            }

            clsPublic.PlayAvi("findFILE.avi", "���ڼ������ݣ����Ժ�...");
            try
            {

                this.m_objViewer.lvInvoiceCat.Items.Clear();
                this.m_objViewer.dtItem.Rows.Clear();
                this.m_objViewer.lblInfo.Visible = false;
                this.m_objViewer.lblDays.Visible = false;

                dtSource = null;
                long l = this.objSvc.m_lngGetFeeItemByActiveType(this.m_objViewer.ucPatientInfo.RegisterID, 888, "1", null, null, null, out dtSource);
                if (l > 0)
                {
                    #region ��ʾ��Ʊ����
                    //��ʾ��Ʊ����
                    if (dtCat.Rows.Count > 0)
                    {
                        ArrayList arrcat = new ArrayList();
                        DataView dvcat = new DataView(dtSource);

                        for (int i = 0; i < dtCat.Rows.Count; i++)
                        {
                            string invocatid = dtCat.Rows[i]["typeid_chr"].ToString().Trim();
                            decimal invosum = 0;

                            dvcat.RowFilter = "invcateid_chr = '" + invocatid + "'";
                            foreach (DataRowView drv in dvcat)
                            {
                                invosum += clsPublic.Round(clsPublic.ConvertObjToDecimal(drv["totalmony"]), 2);
                            }

                            if (invosum == 0)
                            {
                                continue;
                            }

                            clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();
                            invocat_vo.CatID = invocatid;
                            invocat_vo.CatName = dtCat.Rows[i]["typename_vchr"].ToString().Trim();
                            invocat_vo.CatSum = invosum;

                            arrcat.Add(invocat_vo);
                        }

                        for (int j = 0; j < arrcat.Count; j++)
                        {
                            clsInvoiceCat_VO invocat_vo = (clsInvoiceCat_VO)arrcat[j];

                            ListViewItem lvitem = new ListViewItem();
                            lvitem.Text = invocat_vo.CatName + "\r\n" + invocat_vo.CatSum.ToString("0.00");
                            lvitem.ImageIndex = 11;
                            lvitem.Tag = invocat_vo;
                            this.m_objViewer.lvInvoiceCat.Items.Add(lvitem);
                        }
                    }
                    #endregion

                    //�������
                    this.m_mthFillData(dtSource);
                }

                this.m_blnBatchCharge(true);

                if (this.m_objViewer.ucPatientInfo.FeeCheckStatus == 3)
                {
                    this.m_objViewer.btnCheckStatus.Enabled = false;
                }
                else
                {
                    this.m_objViewer.btnCheckStatus.Enabled = true;
                }
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="dt"></param>
        public void m_mthFillData(DataTable dt)
        {
            #region �������
            this.m_objViewer.dtItem.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string[] sarr = new string[20];

                sarr[0] = Convert.ToString(i + 1);

                if (dr["chargeactive_dat"].ToString().Trim() != "")
                {
                    sarr[1] = Convert.ToDateTime(dr["chargeactive_dat"].ToString()).ToString("yyyyMMdd");
                }
                else
                {
                    sarr[1] = "";
                }
                //�������� 0 ����(����) 1 ���� 2 ���� 3 �����¿��� 4 ��Ժ��ҩ 7 ������ 8 ֱ�� 9 ������
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
                    ordertype = "�¿���";
                }
                else if (ordertype == "4")
                {
                    ordertype = "��Ժ��ҩ";
                }
                else if (ordertype == "7")
                {
                    ordertype = "������";
                }
                else if (ordertype == "8")
                {
                    ordertype = "ֱ��";
                }
                else if (ordertype == "9")
                {
                    ordertype = "������";
                }

                sarr[2] = ordertype;
                sarr[3] = dr["recno"].ToString();
                sarr[4] = dr["itemcode_vchr"].ToString();
                sarr[5] = dr["chargeitemname_chr"].ToString();
                sarr[6] = dr["amount_dec"].ToString();

                decimal d = clsPublic.ConvertObjToDecimal(dr["amount_dec"]) * clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]);
                sarr[7] = d.ToString("0.00");
                sarr[8] = dr["unitprice_dec"].ToString();
                sarr[9] = dr["precent_dec"].ToString();

                d = d * clsPublic.ConvertObjToDecimal(dr["precent_dec"]) / 100;
                sarr[10] = d.ToString("0.00");
                sarr[11] = dr["spec_vchr"].ToString();
                sarr[12] = dr["unit_vchr"].ToString();
                sarr[13] = GetDeptName(dr["curareaid_chr"].ToString().Trim());
                sarr[14] = GetCatName(dr["invcateid_chr"].ToString().Trim());
                sarr[15] = GetEmpName(dr["doctorid_chr"].ToString().Trim());
                sarr[16] = GetEmpName(dr["activator_chr"].ToString().Trim());

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
                sarr[17] = activetype;
                sarr[18] = GetDeptName(dr["createarea_chr"].ToString().Trim());
                sarr[19] = dr["execarea"].ToString();

                this.m_objViewer.dtItem.Rows.Add(sarr);

                if (Math.IEEERemainder(Convert.ToDouble(i + 1), 2) == 0)
                {
                    this.m_objViewer.dtItem.Rows[i].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                }
            }

            if (dt.Rows.Count == 0)
            {
                this.m_objViewer.lblInfo.Visible = true;
            }
            #endregion
        }
        #endregion
        #endregion

        #region �Զ�������
        /// <summary>
        /// �Զ�������
        /// </summary>
        /// <param name="OnlyShow"></param>
        /// <returns></returns>
        public bool m_blnBatchCharge(bool OnlyShow)
        {
            //�����(��λ��)�������ʱ��->���㲹�ǹ��Ѵ���

            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;
            if (RegID == "")
            {
                return false;
            }

            //����ֵ
            long l = 0;

            //��Ժʱ��
            string inhospdate = this.m_objViewer.ucPatientInfo.BihPatient_VO.InHospitalDate;

            //����ʱ��                
            DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:01");
            DateTime dt2 = Convert.ToDateTime(inhospdate.Substring(0, 10) + " 00:00:01");

            //��ȡ����������ʱ��
            string FinallyDate = this.objSvc.GetDayAccountsMaxDate(RegID);
            if (FinallyDate != "")
            {
                dt2 = Convert.ToDateTime(FinallyDate.Substring(0, 10) + " 00:00:01");
            }
            else
            {
                dt2 = dt2.AddDays(-1);
            }

            TimeSpan ts = dt1.Subtract(dt2);

            if (ts.Days > 1)
            {
                if (OnlyShow)
                {
                    this.m_objViewer.lblDays.Visible = true;
                    this.m_objViewer.lblDays.Text = "δ����������" + Convert.ToString(ts.Days - 1) + "��";
                    return true;
                }

                /***������***/
                //ÿ�չ���ʱ��(HH:mm:ss)
                string autoFeeTime = " 23:59:59"; /*��ȡ��������*/
                string CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    clsPublic.PlayAvi("findFILE.avi", "���ڲ������ã����Ժ�...");

                    for (int i = 1; i < ts.Days; i++)
                    {
                        string autoFeeDate = dt2.AddDays(i).ToString("yyyy-MM-dd") + autoFeeTime;
                        l = this.objSvc.AutoCharge(CreateTime, autoFeeDate, this.m_objViewer.LoginInfo.m_strEmpID, RegID, 2);
                        if (l < 0)
                        {
                            clsPublic.CloseAvi();
                            MessageBox.Show("��������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                    }

                    clsPublic.CloseAvi();
                    this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                }
                catch (Exception ex)
                {
                    clsPublic.CloseAvi();
                    MessageBox.Show("��������ʧ�ܡ�" + ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                finally
                {
                    clsPublic.CloseAvi();
                }
            }

            return true;
        }
        #endregion

        #region ���ʴ���:������תΪ����,ͬʱ����һ������.(��������)
        /// <summary>
        /// ���ʴ���:������תΪ����,ͬʱ����һ������.(��������)
        /// </summary>
        public void m_mthCharge()
        {
            if (!this.m_blnBatchCharge(false))
            {
                return;
            }

            if (this.dtSource.Rows.Count == 0)
            {
                MessageBox.Show("�ò���û�д�����á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MessageBox.Show("�����շ���Ŀǰ���ٴ�ȷ�ϣ� [��]-ȷ�� [��]-ȡ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }

            frmDayReckoningRemark fremark = new frmDayReckoningRemark();
            fremark.Text = "���ʱ�ע";
            DialogResult dg = fremark.ShowDialog();
            if (dg == DialogResult.Yes || dg == DialogResult.OK)
            {
                clsBihDayAccounts_VO DayAccounts_VO = new clsBihDayAccounts_VO();
                DayAccounts_VO.RegisterID = this.m_objViewer.ucPatientInfo.RegisterID;
                DayAccounts_VO.PatientID = this.m_objViewer.ucPatientInfo.BihPatient_VO.PatientID;
                DayAccounts_VO.AreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
                DayAccounts_VO.Note = fremark.RemarkInfo;
                DayAccounts_VO.CurrAreaID = this.m_objViewer.ucPatientInfo.BihPatient_VO.AreaID;
                DayAccounts_VO.OperID = this.m_objViewer.LoginInfo.m_strEmpID;
                DayAccounts_VO.Type = "1";

                //�����ܽ��Ը����ͼ��ʽ��
                decimal decTotalSum = 0;
                decimal decSbSum = 0;
                decimal decAcctSum = 0;

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    decimal d = clsPublic.ConvertObjToDecimal(dtSource.Rows[i]["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dtSource.Rows[i]["amount_dec"]);
                    decTotalSum += clsPublic.Round(d, 2);
                    decSbSum += clsPublic.Round(d * clsPublic.ConvertObjToDecimal(dtSource.Rows[i]["precent_dec"]) / 100, 2);
                }

                DayAccounts_VO.TotalSum = decTotalSum;
                DayAccounts_VO.SbSum = decSbSum;
                DayAccounts_VO.AcctSum = decAcctSum;

                long l = this.objSvc.m_lngBuildDayAccounts(DayAccounts_VO, this.m_objViewer.LoginInfo.m_strEmpID, int.Parse(this.m_objViewer.ChargeType));
                if (l > 0)
                {
                    //��Ժ���������ڳ���λ
                    if (this.m_objViewer.ChargeType == "1")
                    {
                        this.objSvc.m_lngClearBed(this.m_objViewer.ucPatientInfo.RegisterID);
                        this.objSvc.m_lngUpdatePatientChargeCheckStatus(this.m_objViewer.ucPatientInfo.RegisterID, "3");
                    }

                    MessageBox.Show("���ʳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
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
            frmPatchDayAccount fP = new frmPatchDayAccount(this.m_objViewer.ucPatientInfo.BihPatient_VO);
            fP.ShowDialog();
            if (fP.ModifyFlag)
            {
                this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
            }
        }
        #endregion

        #region ���·��ú˶�״̬
        /// <summary>
        /// ���·��ú˶�״̬
        /// </summary>
        /// <param name="CheckStatus"></param>
        public void m_mthUpdateCheckStatus(string CheckStatus)
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID.Trim() == "")
            {
                return;
            }

            this.m_objViewer.ucPatientInfo.FeeCheckStatus = int.Parse(CheckStatus);
            this.m_objViewer.ucPatientInfo.m_mthShowFeeCheckStatus();
            this.objSvc.m_lngUpdatePatientChargeCheckStatus(this.m_objViewer.ucPatientInfo.RegisterID, CheckStatus);
        }
        #endregion

    }
}
