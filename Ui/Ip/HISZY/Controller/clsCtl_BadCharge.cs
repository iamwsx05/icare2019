using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ���ʽ����߼�������
    /// </summary>
    public class clsCtl_BadCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_BadCharge()
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
        com.digitalwave.iCare.gui.HIS.frmBadCharge m_objViewer;
        /// <summary>
        /// ����Դ
        /// </summary>
        private DataTable dtSource = new DataTable();
        /// <summary>
        /// �Ƿ��Ѽ����̯
        /// </summary>
        private bool IsCompute = false;
        /// <summary>
        /// ��ֵ�����ID
        /// </summary>
        private string DiffValDeptID = "";
        /// <summary>
        /// ��ֵ��������ID
        /// </summary>
        private string DiffValCatID = "";
        /// <summary>
        /// ĸӤ�ϲ����㿪��,0Ϊ��,1Ϊ��
        /// </summary>
        private int intBabyParm = 0;
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBadCharge)frmMDI_Child_Base_in;
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
            frmCommonFind f = new frmCommonFind("���ҳ�Ժ��������", this.m_objViewer.ucPatientInfo.Status);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.ucPatientInfo.m_mthFind(f.RegisterID, 3);
                if (this.m_objViewer.ucPatientInfo.IsChanged)
                {
                    this.m_blnChargePatch();
                    if (this.m_objViewer.cboDeptClass.SelectedIndex == 0)
                    {
                        this.m_mthShowFeeCat(this.m_objViewer.ucPatientInfo.RegisterID, 1);
                    }
                    else
                    {
                        this.m_objViewer.cboDeptClass.SelectedIndex = 0;
                    }
                }
            }
        }
        #endregion

        #region ���������Է���
        /// <summary>
        /// ���������Է���
        /// </summary>
        /// <returns>true �ɹ� false ʧ��</returns>
        public bool m_blnChargePatch()
        {
            bool ret = true;

            try
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                ret = clsPublic.m_blnChargeContinueItem(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID);
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
            }

            return ret;
        }
        #endregion

        #region ��ʾ�������
        /// <summary>
        /// ��ʾ�������
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 �������� 2 ִ�п��� 3 ���ڲ���</param>
        public void m_mthShowFeeCat(string RegID, int DeptClass)
        {
            DataTable dt = this.objSvc.GetPatientCheckFee(RegID);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "1")
            { }
            else
            {
                this.m_objViewer.Cursor = Cursors.Default;
                MessageBox.Show("������δ�Բ��˷��ý������պ˶ԣ����ܽ��н��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.m_objViewer.btnCompute.Enabled = true;
            this.m_objViewer.btnCharge.Enabled = true;

            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.PrepayMoney >= (this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitChargeFee + this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitClearFee))
            {
                MessageBox.Show("�ò��˵�Ԥ��������ܷ��ý���ʹ��������Ժ���㣡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            int FeeStatus = this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus;
            if (FeeStatus == 4)
            {
                this.m_objViewer.btnCompute.Enabled = false;
                this.m_objViewer.btnCharge.Enabled = false;
            }
            else if (FeeStatus == 3)
            {
                this.m_objViewer.lblPrepayMoney.Text = "���Ǵ��ʲ��ˡ�";
                this.m_objViewer.dtgDetail.Rows.Clear();
                return;
            }

            this.m_objViewer.Cursor = Cursors.WaitCursor;
            this.m_objViewer.dtgDetail.Rows.Clear();

            int Status = (FeeStatus == 4 ? 1 : 0);
            if (Status == 1)
            {
                this.m_objViewer.lblPrepayMoney.Text = "�����ʽ��㡿";
            }
            else
            {
                this.m_objViewer.lblPrepayMoney.Text = this.m_objViewer.ucPatientInfo.BihPatient_VO.PrepayMoney.ToString("0.00");
            }

            dtSource = null;
            //ĸӤ�ϲ�������������ж� 
            intBabyParm = clsPublic.m_intGetSysParm("1119");
            long lngRes;
            if (intBabyParm == 1)
            {
                //ĸӤ����һ����
                lngRes = this.objSvc.m_lngGetFeeCatByDeptClassForMortherBaby(RegID, DeptClass, Status, out dtSource);

            }
            else
            {
                //ֻ��ĸ�׷���

                lngRes = this.objSvc.m_lngGetFeeCatByDeptClass(RegID, DeptClass, Status, out dtSource);
            }
            if (lngRes > 0)
            {
                decimal decTotalmny = 0;
                decimal decComputemny = 0;
                int intDelRow = 0;
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    DataRow dr = dtSource.Rows[i];
                    //�ж��Ƿ���Ӥ������һ���ó������м�����ÿճ�һ��������
                    if (dr["catsum"].ToString() == "")
                    {
                        //����һ���ֱ�ʾ����ΪӤ������
                        this.m_mthInsertBlankRow(decTotalmny, decComputemny);
                        intDelRow = m_intInsertBabyRow();
                        continue;
                    }
                    decimal d = Convert.ToDecimal(dr["catsum"].ToString());

                    string[] sarr = new string[7];

                    sarr[0] = Convert.ToString(i + 1);
                    if (i > 0)
                    {
                        if (dtSource.Rows[i]["deptname_vchr"].ToString().Trim() == dtSource.Rows[i - 1]["deptname_vchr"].ToString().Trim())
                        {
                            decTotalmny += d;
                            if (Status == 1)
                            {
                                decComputemny += d;
                            }

                            sarr[1] = "";
                        }
                        else
                        {
                            this.m_mthInsertBlankRow(decTotalmny, decComputemny);
                            decTotalmny = 0;
                            decComputemny = 0;

                            decTotalmny += d;
                            if (Status == 1)
                            {
                                decComputemny += d;
                            }

                            sarr[1] = dr["deptname_vchr"].ToString().Trim();
                        }
                    }
                    else
                    {
                        decTotalmny += d;
                        if (Status == 1)
                        {
                            decComputemny += d;
                        }
                        sarr[1] = dr["deptname_vchr"].ToString().Trim();
                    }
                    sarr[2] = dr["calccateid_chr"].ToString().Trim();
                    sarr[3] = dr["typename_vchr"].ToString().Trim();
                    sarr[4] = d.ToString("###,##0.00");
                    if (Status == 1)
                    {
                        sarr[5] = d.ToString("###,##0.00");
                    }
                    else
                    {
                        sarr[5] = "";
                    }
                    sarr[6] = dr["deptid"].ToString();

                    int row = this.m_objViewer.dtgDetail.Rows.Add(sarr);
                    this.m_objViewer.dtgDetail.Rows[row].Tag = dr;

                    if (sarr[1].ToString().Trim() != "")
                    {
                        this.m_objViewer.dtgDetail.Rows[row].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }
                }

                this.m_mthInsertBlankRow(decTotalmny, decComputemny);
                if (intDelRow > 0)
                {
                    this.m_objViewer.dtgDetail.Rows.RemoveAt(intDelRow + 1);
                }
            }

            this.m_objViewer.Cursor = Cursors.Default;

            IsCompute = false;
        }
        #endregion

        #region �����
        /// <summary>
        /// �����
        /// </summary>
        private void m_mthInsertBlankRow(decimal p_totalmny, decimal p_computemny)
        {
            string[] sarr = new string[7];
            sarr[3] = "����С��:";
            sarr[4] = p_totalmny.ToString("###,##0.00");
            if (p_computemny == 0)
            {
                sarr[5] = "";
            }
            else
            {
                sarr[5] = p_computemny.ToString("###,##0.00");
            }

            int row = this.m_objViewer.dtgDetail.Rows.Add(sarr);
            this.m_objViewer.dtgDetail.Rows[row].Cells["colhjje"].Style.ForeColor = Color.Blue;
            this.m_objViewer.dtgDetail.Rows[row].Cells["colftje"].Style.ForeColor = Color.Blue;
        }

        /// <summary>
        /// ���������ʾӤ������
        /// </summary>
        /// <returns></returns>
        private int m_intInsertBabyRow()
        {
            string[] sarr = new string[7];
            sarr[1] = "Ӥ������";
            sarr[2] = "--------";
            sarr[3] = "--------";



            int intRow = this.m_objViewer.dtgDetail.Rows.Add(sarr);
            //this.m_objViewer.dtgDetail.Rows[intRow].Tag = p_strDeptID;
            this.m_objViewer.dtgDetail.Rows[intRow].DefaultCellStyle.BackColor = SystemColors.Control;
            return intRow;

        }
        #endregion

        #region ��̯����
        /// <summary>
        /// ��̯����
        /// </summary>
        public bool m_mthCompute()
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID.Trim() == "" || this.m_objViewer.dtgDetail.Rows.Count == 0)
            {
                return false;
            }

            decimal decPrepayMny = this.m_objViewer.ucPatientInfo.BihPatient_VO.PrepayMoney;
            decimal decDeptMny = this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitChargeFee + this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitClearFee;

            if (decPrepayMny == 0)
            {
                MessageBox.Show("Ԥ����Ϊ0�����ܽ��з��÷�̯��", "ϵͳ��ʾ", MessageBoxButtons.OK);
                return false;
            }

            if (decDeptMny == 0)
            {
                MessageBox.Show("û��δ��Ŀ��ҷ��ã�����Ҫ���з��÷�̯��", "ϵͳ��ʾ", MessageBoxButtons.OK);
                return false;
            }

            DiffValDeptID = "";
            DiffValCatID = "";

            int rowcount = this.m_objViewer.dtgDetail.Rows.Count;
            decimal d = 0;
            decimal decDiff = 0;
            decimal decComputemny = 0;
            decimal scale = decPrepayMny / decDeptMny;

            for (int i = 0; i < rowcount; i++)
            {
                if (this.m_objViewer.dtgDetail.Rows[i].Cells["colflmc"].Value.ToString().IndexOf("����С��") >= 0)
                {
                    this.m_objViewer.dtgDetail.Rows[i].Cells["colftje"].Value = decComputemny.ToString("###,##0.00");
                    decComputemny = 0;
                    continue;
                }

                d = clsPublic.Round(clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgDetail.Rows[i].Cells["colhjje"].Value) * scale, 2);

                this.m_objViewer.dtgDetail.Rows[i].Cells["colftje"].Value = d.ToString("###,##0.00");

                decDiff += d;
                decComputemny += d;
            }

            if (scale < 1)
            {
                //����ֵ�������һ��(����)
                d = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgDetail.Rows[rowcount - 2].Cells["colftje"].Value) + (decPrepayMny - decDiff);
                this.m_objViewer.dtgDetail.Rows[rowcount - 2].Cells["colftje"].Value = d.ToString("###,##0.00");
                //(����С��)
                d = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgDetail.Rows[rowcount - 1].Cells["colftje"].Value) + (decPrepayMny - decDiff);
                this.m_objViewer.dtgDetail.Rows[rowcount - 1].Cells["colftje"].Value = d.ToString("###,##0.00");


                DiffValDeptID = this.m_objViewer.dtgDetail.Rows[rowcount - 2].Cells["colksid"].Value.ToString();
                DiffValCatID = this.m_objViewer.dtgDetail.Rows[rowcount - 2].Cells["colfldm"].Value.ToString();
            }

            IsCompute = true;

            return true;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthCharge()
        {
            #region У��
            if (this.m_objViewer.ucPatientInfo.RegisterID.Trim() == "" || this.m_objViewer.dtgDetail.Rows.Count == 0)
            {
                return;
            }

            if (this.m_objViewer.cboDeptClass.SelectedIndex != 0)
            {
                this.m_objViewer.cboDeptClass.SelectedIndex = 0;
            }

            //�Ƿ���Ԥ���� (true �� false ��)
            bool IsHavePrepayMoney = false;
            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.PrepayMoney > 0)
            {
                IsHavePrepayMoney = true;

                if (!IsCompute)
                {
                    if (!this.m_mthCompute())
                    {
                        return;
                    }
                }
            }
            #endregion

            #region ���ɽ������VO
            //�������
            List<clsBihChargeCat_VO> ChargeCatArr = new List<clsBihChargeCat_VO>();
            if (IsHavePrepayMoney)
            {
                if (intBabyParm == 0)
                {
                    #region ��ͨģʽ
                    for (int i = 0; i < this.m_objViewer.dtgDetail.Rows.Count; i++)
                    {
                        if (this.m_objViewer.dtgDetail.Rows[i].Cells["colflmc"].Value.ToString().IndexOf("����С��") >= 0)
                        {
                            continue;
                        }

                        DataRow dr = this.m_objViewer.dtgDetail.Rows[i].Tag as DataRow;

                        if (dr["typename_vchr"].ToString().Trim() == "")
                        {
                            MessageBox.Show("�շ���ϸ����סԺ�������Ϊ�գ�������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        clsBihChargeCat_VO ChargeCat_VO = new clsBihChargeCat_VO();
                        ChargeCat_VO.DeptID = dr["deptid"].ToString();
                        ChargeCat_VO.ItemCatID = dr["calccateid_chr"].ToString();
                        ChargeCat_VO.TotalSum = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgDetail.Rows[i].Cells["colftje"].Value);
                        ChargeCat_VO.AcctSum = 0;

                        ChargeCatArr.Add(ChargeCat_VO);
                    }
                    #endregion
                }
                else
                {
                    #region ĸӤ�ϲ��������ɽ�����VO
                    int intTempRows = 0;
                    DataTable dtbMater = new DataTable();
                    DataTable dtbBaby = new DataTable();
                    dtbMater.Columns.Add("deptid");
                    dtbMater.Columns.Add("calccateid_chr");
                    dtbMater.Columns.Add("TotalSum");

                    dtbBaby.Columns.Add("deptid");
                    dtbBaby.Columns.Add("calccateid_chr");
                    dtbBaby.Columns.Add("TotalSum");

                    string[] sarr = new string[3];
                    for (int i = 0; i < this.m_objViewer.dtgDetail.Rows.Count; i++)
                    {
                        if (this.m_objViewer.dtgDetail.Rows[i].Cells["colflmc"].Value.ToString().IndexOf("����С��") >= 0)
                        {
                            continue;
                        }
                        else if (this.m_objViewer.dtgDetail.Rows[i].Cells["colflmc"].Value.ToString().IndexOf("--------") >= 0)
                        {
                            intTempRows = i + 1;
                            break;
                        }

                        DataRow dr = this.m_objViewer.dtgDetail.Rows[i].Tag as DataRow;

                        if (dr["typename_vchr"].ToString().Trim() == "")
                        {
                            MessageBox.Show("�շ���ϸ����סԺ�������Ϊ�գ�������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        sarr[0] = dr["deptid"].ToString();
                        sarr[1] = dr["calccateid_chr"].ToString();
                        sarr[2] = this.m_objViewer.dtgDetail.Rows[i].Cells["colhjje"].Value.ToString();

                        dtbMater.Rows.Add(sarr);
                        //clsBihChargeCat_VO ChargeCat_VO = new clsBihChargeCat_VO();
                        //ChargeCat_VO.DeptID = dr["deptid"].ToString();
                        //ChargeCat_VO.ItemCatID = dr["calccateid_chr"].ToString();
                        //ChargeCat_VO.TotalSum = clsPublic.ConvertObjToDecimal(this.m_objViewer.dtgDetail.Rows[i].Cells["colhjje"].Value);
                        //ChargeCat_VO.AcctSum = 0;

                        //ChargeCatArr.Add(ChargeCat_VO);

                    }
                    //�����Ӥ�����ã�Ҳ�ֳ�������
                    if (intTempRows > 0)
                    {

                        for (int i2 = intTempRows; i2 < this.m_objViewer.dtgDetail.Rows.Count; i2++)
                        {
                            if (this.m_objViewer.dtgDetail.Rows[i2].Cells["colflmc"].Value.ToString().IndexOf("����С��") >= 0)
                            {
                                continue;
                            }


                            DataRow dr = this.m_objViewer.dtgDetail.Rows[i2].Tag as DataRow;

                            if (dr["typename_vchr"].ToString().Trim() == "")
                            {
                                MessageBox.Show("�շ���ϸ����סԺ�������Ϊ�գ�������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            sarr[0] = dr["deptid"].ToString();
                            sarr[1] = dr["calccateid_chr"].ToString();
                            sarr[2] = this.m_objViewer.dtgDetail.Rows[i2].Cells["colhjje"].Value.ToString();

                            dtbBaby.Rows.Add(sarr);
                        }
                        DataRow drwMater = null;
                        DataRow drwBaby = null;
                        for (int i3 = 0; i3 < dtbMater.Rows.Count; i3++)
                        {
                            drwMater = dtbMater.Rows[i3];
                            for (int i4 = 0; i4 < dtbBaby.Rows.Count; i4++)
                            {
                                drwBaby = dtbBaby.Rows[i4];
                                if (drwMater["deptid"].ToString() == drwBaby["deptid"].ToString() && drwMater["calccateid_chr"].ToString() == drwBaby["calccateid_chr"].ToString())
                                {
                                    dtbMater.Rows[i3]["TotalSum"] = Convert.ToDecimal(drwMater["TotalSum"].ToString()) + Convert.ToDecimal(drwBaby["TotalSum"].ToString());
                                    dtbBaby.Rows.RemoveAt(i4);
                                    i4--;
                                }
                            }
                        }
                        drwMater = null;
                        drwBaby = null;
                        //��Ӥ���еķ������ͣ�ĸ��û�е�Ҳ��ӽ����÷����ܱ�ȥ
                        if (dtbBaby.Rows.Count > 0)
                        {

                            object[] objarr = null; ;
                            for (int i6 = 0; i6 < dtbBaby.Rows.Count; i6++)
                            {
                                objarr = dtbBaby.Rows[i6].ItemArray;
                                dtbMater.Rows.Add(objarr);
                            }
                            objarr = null;

                        }
                    }
                    //���еķ��ñ����ɽ���������VO
                    DataRow drwMater2 = null;
                    for (int i5 = 0; i5 < dtbMater.Rows.Count; i5++)
                    {
                        drwMater2 = dtbMater.Rows[i5];
                        clsBihChargeCat_VO ChargeCat_VO = new clsBihChargeCat_VO();
                        ChargeCat_VO.DeptID = drwMater2["deptid"].ToString();
                        ChargeCat_VO.ItemCatID = drwMater2["calccateid_chr"].ToString();
                        ChargeCat_VO.TotalSum = Convert.ToDecimal(drwMater2["TotalSum"].ToString());
                        ChargeCat_VO.AcctSum = 0;

                        ChargeCatArr.Add(ChargeCat_VO);
                    }
                    #endregion
                }
            }
            #endregion

            #region ���ɷ�Ʊ����VO
            //��Ʊ����
            List<clsBihInvoiceCat_VO> ChargeInvArr = new List<clsBihInvoiceCat_VO>();
            if (IsHavePrepayMoney)
            {
                string RegID = this.m_objViewer.ucPatientInfo.RegisterID.Trim();
                DataTable dtFee;
                long l = 0;
                if (intBabyParm == 1)
                {
                    l = this.objSvc.m_lngGetBadChargeFeeInfoMotherBaby(RegID, out dtFee);
                }
                else
                {
                    l = this.objSvc.m_lngGetBadChargeFeeInfo(RegID, out dtFee);
                }
                if (l > 0)
                {
                    DataView dv = new DataView(dtFee);
                    DataTable dtCat = new DataTable();

                    l = this.objSvc.m_lngGetChargeItemCat(4, out dtCat);
                    if (l > 0 && dtCat.Rows.Count > 0)
                    {
                        decimal decPrepayMny = this.m_objViewer.ucPatientInfo.BalancePrepayMoney;     // .BihPatient_VO.PrepayMoney;  2019-11-14
                        decimal decDeptMny = this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitChargeFee + this.m_objViewer.ucPatientInfo.BihPatient_VO.WaitClearFee;
                        decimal scale = decPrepayMny / decDeptMny;
                        decimal decSum = 0;

                        for (int i = 0; i < dtCat.Rows.Count; i++)
                        {
                            string catid = dtCat.Rows[i]["typeid_chr"].ToString().Trim();
                            string catname = dtCat.Rows[i]["typename_vchr"].ToString().Trim();

                            decimal dtotal = 0;

                            dv.RowFilter = "invcateid_chr = '" + catid + "'";
                            foreach (DataRowView drv in dv)
                            {
                                catid = drv["invcateid_chr"].ToString().Trim();
                                if (catid == "")
                                {
                                    MessageBox.Show("�շ���ϸ����סԺ��Ʊ����Ϊ�գ�������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }

                                decimal dc = clsPublic.Round(clsPublic.ConvertObjToDecimal(drv["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(drv["amount_dec"]) * scale, 2);
                                dtotal += dc;
                            }

                            if (dtotal == 0)
                            {
                                continue;
                            }

                            clsBihInvoiceCat_VO InvoiceCat_VO = new clsBihInvoiceCat_VO();
                            InvoiceCat_VO.ItemCatID = catid;
                            InvoiceCat_VO.ItemCatName = catname;
                            InvoiceCat_VO.TotalSum = dtotal;
                            InvoiceCat_VO.AcctSum = 0;

                            decSum += dtotal;

                            if (i == dtCat.Rows.Count - 1)
                            {
                                if (decSum != decPrepayMny)
                                {
                                    InvoiceCat_VO.TotalSum = InvoiceCat_VO.TotalSum + decPrepayMny - decSum;
                                }
                            }

                            ChargeInvArr.Add(InvoiceCat_VO);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            #endregion

            frmReckoning frec = new frmReckoning(this.m_objViewer.InvoNo);
            frec.ChargeType = 3;
            frec.objPatient = this.m_objViewer.ucPatientInfo;
            frec.BadChargeCatArr = ChargeCatArr;
            frec.BadChargeInvArr = ChargeInvArr;
            frec.BadChargeDiffValDeptID = DiffValDeptID;
            frec.BadChargeDiffValCatID = DiffValCatID;
            frec.DirectChargeFlag = !IsHavePrepayMoney;
            if (frec.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.btnCompute.Enabled = false;
                this.m_objViewer.btnCharge.Enabled = false;
                this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
            }
        }
        #endregion

    }
}
