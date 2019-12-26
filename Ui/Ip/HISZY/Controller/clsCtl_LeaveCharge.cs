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
    /// ��Ժ�����߼�������
    /// </summary>
    public class clsCtl_LeaveCharge : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_LeaveCharge()
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
        com.digitalwave.iCare.gui.HIS.frmLeaveCharge m_objViewer;

        /// <summary>
        /// ��ǰ���˷���������ϸ
        /// </summary>
        private DataTable ChargeDt;

        /// <summary>
        /// ѡ�����ķ�����ϸ
        /// </summary>
        private DataTable ChargeDtSelect;

        /// <summary>
        /// ��������δ�ᴦ������ʱ���Ƴ�Ժ������ҽ��¼��(ҽ��¼��1��2״̬��Ϊ��ʾѡ��)0-�ر�;1-��ʾѡ��2-��ס
        /// </summary>
        internal int m_intParm1068 = 0;

        /// <summary>
        /// �������ÿ���
        /// </summary>
        internal int intDiffCostOn = 0;

        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmLeaveCharge)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        internal void m_mthInt()
        {
            m_intParm1068 = clsPublic.m_intGetSysParm("1068");
            intDiffCostOn = clsPublic.m_intGetSysParm("9002");
        }
        #endregion

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="ky"></param>
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
                    if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 2)
                    {
                        this.m_mthShowAllFeeDetail(this.m_objViewer.ucPatientInfo.RegisterID);
                    }
                    else if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 3)
                    {
                        this.m_mthShowAllFeeDetail(this.m_objViewer.ucPatientInfo.RegisterID);
                    }
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthReset()
        {
            this.m_objViewer.lblTotalSum.Text = "";
            this.m_objViewer.lblSbSum.Text = "";
            this.m_objViewer.lblAcctSum.Text = "";
            this.m_objViewer.lblCompleteSum.Text = "";
            this.m_objViewer.lblPay.Text = "";
            this.m_objViewer.lvInvoiceCat.Items.Clear();
            if (ChargeDtSelect != null)
            {
                ChargeDtSelect.Rows.Clear();
            }
        }
        #endregion

        #region ��ʾ��ǰ�������з�����Ϣ
        /// <summary>
        /// ��ʾ��ǰ�������з�����Ϣ
        /// </summary>
        /// <param name="RegID"></param>
        public void m_mthShowAllFeeDetail(string RegID)
        {
            this.m_objViewer.Cursor = Cursors.WaitCursor;
            this.m_mthReset();
            //this.m_objViewer.dtgDetail.Rows.Clear();

            DataTable dt = this.objSvc.GetPatientCheckFee(RegID);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ischeckfee"].ToString() == "1")
            { }
            else
            {
                this.m_objViewer.Cursor = Cursors.Default;
                MessageBox.Show("������δ�Բ��˷��ý������պ˶ԣ����ܽ��н��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus != 4)
            {
                #region ��ʱ����
                //if (clsPublic.m_blnChargeContinueItem(this.m_objViewer.ucPatientInfo.RegisterID, this.m_objViewer.LoginInfo.m_strEmpID) == false)
                //{
                //    this.m_objViewer.Cursor = Cursors.Default;
                //    MessageBox.Show("���ݽ����쳣��������ֹ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                #endregion
            }

            ChargeDt = null;
            long l = this.objSvc.m_lngGetChargeInfoByID(RegID, 1, out ChargeDt);

            int intParm = clsPublic.m_intGetSysParm("1119");//��ȡĸӤ�ϲ����㿪��
            #region ����Ӥ������
            if (intParm == 1)
            {
                DataTable dtbBabyCharge = new DataTable();
                bool blnHaveBagy = this.m_blnHaveBaby(this.m_objViewer.ucPatientInfo.BihPatient_VO.RegisterID, out dtbBabyCharge);
                if (blnHaveBagy)
                {
                    ChargeDt.Merge(dtbBabyCharge);
                }
            }
            #endregion
            if (l > 0)
            {
                DataTable dtview = new DataTable();
                #region create columns
                dtview.Columns.Add("selectflag");
                dtview.Columns.Add("serno");
                dtview.Columns.Add("dayaccountno");
                dtview.Columns.Add("begindate");
                dtview.Columns.Add("invoname");
                dtview.Columns.Add("itemname");
                dtview.Columns.Add("nums");
                dtview.Columns.Add("price");
                dtview.Columns.Add("total");
                dtview.Columns.Add("scale");
                dtview.Columns.Add("facttotal");
                dtview.Columns.Add("status");
                dtview.Columns.Add("totaldiffcostmoney_dec");// ���������
                dtview.Columns.Add("requiredpay");// ʵ�����
                #endregion

                DataView dv = new DataView(ChargeDt);

                int rowno = 1;

                dtview.BeginLoadData();
                decimal dec_PayMny = 0;//ʵ��
                decimal dec_DiffSum = 0;//����
                decimal dec_SumPrice = 0;//�ܽ��
                for (int i = 0; i < dv.Count; i++)
                {
                    DataRow dr = dv[i].Row;

                    string[] sarr = new string[14];

                    string orderno = dr["orderno_int"].ToString().Trim();
                    string createdate = Convert.ToDateTime(dr["chargeactive_dat"].ToString()).ToString("yyyyMMdd");
                    string itemid = dr["chargeitemid_chr"].ToString().Trim();
                    string price = dr["unitprice_dec"].ToString().Trim();
                    string statusid = dr["pstatus_int"].ToString();
                    decimal amount = clsPublic.ConvertObjToDecimal(dr["amount_dec"]);
                    decimal d = 0;
                    decimal totalmoney = 0;
                    decimal acctmoney = 0, dec_requiredPay = 0;
                    decimal decTotalDiffCost = 0;// ���������
                    if (statusid == "3" || statusid == "4")
                    {
                        d = clsPublic.ConvertObjToDecimal(dr["totalmoney_dec"]);
                        totalmoney = d;
                        acctmoney = d - clsPublic.ConvertObjToDecimal(dr["acctmoney_dec"]);
                    }
                    else
                    {
                        d = clsPublic.ConvertObjToDecimal(dr["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(dr["amount_dec"]);
                        if (d != clsPublic.ConvertObjToDecimal(dr["totalmoney_dec"]))
                        { 
                            com.digitalwave.Utility.Log.Output("����*������" + d.ToString() + "    �ܽ�" + clsPublic.ConvertObjToDecimal(dr["totalmoney_dec"]).ToString());
                        }
                        totalmoney = d;
                        acctmoney = d * clsPublic.ConvertObjToDecimal(dr["precent_dec"]) / 100;
                    }
                    decTotalDiffCost = clsPublic.ConvertObjToDecimal(dr["totaldiffcostmoney_dec"]);
                    if (this.intDiffCostOn == 1)
                    {
                        dec_requiredPay = totalmoney + decTotalDiffCost;
                        sarr[12] = decTotalDiffCost.ToString("0.00");
                        sarr[13] = dec_requiredPay.ToString("0.00"); 
                        com.digitalwave.Utility.Log.Output("�ܽ�" + totalmoney.ToString() + "    ҩƷ������" + decTotalDiffCost.ToString() + "      ʵ����" + dec_requiredPay.ToString());
                        dec_PayMny += dec_requiredPay;
                        dec_DiffSum += decTotalDiffCost;
                    }
                    #region �ϲ���ͬ��
                    //for (int j = i + 1; j < dv.Count; j++)
                    //{
                    //    if (dv[j].Row["orderno_int"].ToString().Trim() == orderno &&
                    //        Convert.ToDateTime(dv[j].Row["chargeactive_dat"].ToString()).ToString("yyyyMMdd") == createdate &&
                    //        dv[j].Row["chargeitemid_chr"].ToString().Trim() == itemid &&
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

                    //        //RowArr.Add(j);
                    //        hasRowNo.Add(j.ToString(), null);
                    //    }
                    //}    
                    #endregion

                    sarr[0] = "F";
                    sarr[1] = rowno.ToString();
                    sarr[2] = orderno;
                    sarr[3] = createdate;
                    sarr[4] = dr["ipinvoname"].ToString().Trim();
                    sarr[5] = dr["chargeitemname_chr"].ToString().Trim();
                    sarr[6] = amount.ToString();
                    sarr[7] = price;
                    sarr[8] = totalmoney.ToString("0.00");
                    dec_SumPrice += totalmoney;
                    sarr[9] = dr["precent_dec"].ToString();
                    if (acctmoney > 0)
                    {
                        sarr[10] = acctmoney.ToString("0.00");
                    }
                    else
                    {
                        sarr[10] = "";
                    }

                    //״̬                    
                    string statusname = "";
                    if (statusid == "0")
                    {
                        statusname = "��ȷ��";
                    }
                    else if (statusid == "1")
                    {
                        statusname = "����";
                    }
                    else if (statusid == "2")
                    {
                        statusname = "����";
                    }
                    else if (statusid == "3")
                    {
                        statusname = "����";
                    }
                    else if (statusid == "4")
                    {
                        statusname = "ֱ��";
                    }
                    sarr[11] = statusname;

                    rowno++;
                    dtview.LoadDataRow(sarr, true);
                }
                dtview.EndLoadData();
                this.m_objViewer.dtgDetail.DataSource = dtview;
                this.m_mthSetRowColor();
                this.m_mthGetCheckType();
            }

            this.m_objViewer.Cursor = Cursors.Default;
            //��ȡĸӤ�ϲ����㿪��
            if (intParm == 0)
            {
                this.m_mthCheckBaby(this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh);
            }
        }
        #endregion

        #region ��������ɫ
        /// <summary>
        /// ��������ɫ
        /// </summary>
        public void m_mthSetRowColor()
        {
            for (int i = 0; i < this.m_objViewer.dtgDetail.Rows.Count; i++)
            {
                //��ɫ
                Color FCR = Color.Black;
                Color BCR = Color.White;

                string statusname = this.m_objViewer.dtgDetail.Rows[i].Cells["status"].Value.ToString().Trim();
                if (statusname == "��ȷ��")
                {
                    FCR = Color.FromArgb(200, 0, 0);
                }
                else if (statusname == "����")
                {
                    FCR = Color.FromArgb(200, 0, 0);
                }
                else if (statusname == "����")
                {
                    FCR = Color.RoyalBlue;
                }
                else if (statusname == "ֱ��")
                {
                    FCR = Color.RoyalBlue;
                }

                this.m_objViewer.dtgDetail.Rows[i].DefaultCellStyle.ForeColor = FCR;

                if (Math.IEEERemainder(Convert.ToDouble(i), 2) == 0)
                {
                    this.m_objViewer.dtgDetail.Rows[i].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                }
            }
        }
        #endregion

        #region ��ȡ���÷�Ʊ����
        /// <summary>
        /// ��ȡ���÷�Ʊ����
        /// </summary>        
        public void m_mthGetCheckType()
        {
            DataView dv = new DataView(ChargeDt);
            ChargeDtSelect = ChargeDt.Clone();

            foreach (DataRowView drv in dv)
            {
                ChargeDtSelect.Rows.Add(drv.Row.ItemArray);
            }
            ChargeDtSelect.AcceptChanges();

            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 2 && ChargeDtSelect.Rows.Count == 0)
            {
                MessageBox.Show("û�з��á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //�����ܽ��Ը����ͼ��ʽ��
            decimal decTotalSum = 0;
            decimal decSbSum = 0;
            decimal decAcctSum = 0;

            //�����������������ȷ�Ͻ��
            decimal decCompleteSum = 0;
            decimal decWaitClearSum = 0;
            decimal decWaitChrgSum = 0;
            decimal decWaitConfSum = 0;
            // ���������
            decimal decDiffCostSum = 0;
            for (int i = 0; i < ChargeDt.Rows.Count; i++)
            {
                decimal d = clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["amount_dec"]);

                if (ChargeDt.Rows[i]["pstatus_int"].ToString() == "3" || ChargeDt.Rows[i]["pstatus_int"].ToString() == "4")
                {
                    decTotalSum += clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totalmoney_dec"]);
                    d = clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totalmoney_dec"]);
                    decSbSum += clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totalmoney_dec"]) - clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["acctmoney_dec"]);
                }
                else
                {
                    decTotalSum += clsPublic.Round(d, 2);
                    decSbSum += clsPublic.Round(d * clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["precent_dec"]) / 100, 2);
                    decTotalSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                    decSbSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                }
                //�Ƿ������������� 
                if (this.intDiffCostOn == 1)
                {
                    decDiffCostSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                    //decTotalSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                    //decSbSum += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                    d += clsPublic.Round(clsPublic.ConvertObjToDecimal(ChargeDt.Rows[i]["totaldiffcostmoney_dec"]), 2);
                }

                //����״̬ 0 ��ȷ�� 1 ���� 2 ���� 3 ���� 4 ֱ�� 
                int status = int.Parse(ChargeDt.Rows[i]["pstatus_int"].ToString());
                if (status == 0)
                {
                    decWaitConfSum += clsPublic.Round(d, 2);
                }
                else if (status == 1)
                {
                    decWaitChrgSum += clsPublic.Round(d, 2);
                }
                else if (status == 2)
                {
                    decWaitClearSum += clsPublic.Round(d, 2);
                }
                else if (status == 3)
                {
                    decCompleteSum += clsPublic.Round(d, 2);
                }
            }
            decAcctSum = decTotalSum - decSbSum;

            if (decTotalSum > 0)
            {
                this.m_objViewer.lblTotalSum.Text = decTotalSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblTotalSum.Text = "";
            }
            if (decSbSum > 0)
            {
                this.m_objViewer.lblSbSum.Text = decSbSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblSbSum.Text = "";
            }
            if (decAcctSum > 0)
            {
                this.m_objViewer.lblAcctSum.Text = decAcctSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblAcctSum.Text = "";
            }
            if (decCompleteSum > 0)
            {
                this.m_objViewer.lblCompleteSum.Text = decCompleteSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblCompleteSum.Text = "";
            }
            if (decWaitClearSum > 0)
            {
                this.m_objViewer.lblPay.Text = decWaitClearSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblPay.Text = "";
            }
            if (decWaitChrgSum > 0)
            {
                this.m_objViewer.lblWaitCharge.Text = decWaitChrgSum.ToString("###,##0.00");
            }
            else
            {
                this.m_objViewer.lblWaitCharge.Text = "";
            }

            //���㷢Ʊ����
            DataTable dtcat = new DataTable();
            long l = this.objSvc.m_lngGetChargeItemCat(4, out dtcat);
            string strDiffCostName = string.Empty;//ҩƷ������Ʊ��������

            if (l > 0 && dtcat.Rows.Count > 0)
            {
                ArrayList arrcat = new ArrayList();
                DataView dvcat = new DataView(ChargeDt);

                for (int i = 0; i < dtcat.Rows.Count; i++)
                {
                    string invocatid = dtcat.Rows[i]["typeid_chr"].ToString().Trim();
                    decimal invosum = 0;

                    dvcat.RowFilter = "invcateid_chr = '" + invocatid + "'";
                    foreach (DataRowView drv in dvcat)
                    {
                        invosum += clsPublic.Round(clsPublic.ConvertObjToDecimal(drv["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(drv["amount_dec"]), 2);
                    }
                    if (string.IsNullOrEmpty(strDiffCostName) && string.Compare("3026", invocatid) == 0)
                    {
                        strDiffCostName = dtcat.Rows[i]["typename_vchr"].ToString().Trim();//��ȡҩƷ�������ֵ��е�����
                    }
                    if (invosum == 0)
                    {
                        continue;
                    }

                    clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();
                    invocat_vo.CatID = invocatid;
                    invocat_vo.CatName = dtcat.Rows[i]["typename_vchr"].ToString().Trim();
                    invocat_vo.CatSum = invosum;

                    arrcat.Add(invocat_vo);
                }
                //��ʾҩƷ�������༰ֵ
                if (this.intDiffCostOn == 1)
                {
                    clsInvoiceCat_VO invocat_vo = new clsInvoiceCat_VO();
                    invocat_vo.CatID = "3026";
                    invocat_vo.CatName = strDiffCostName;
                    invocat_vo.CatSum = clsPublic.Round(decDiffCostSum, 2); ;

                    arrcat.Add(invocat_vo);
                }

                this.m_objViewer.lvInvoiceCat.Items.Clear();
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
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthCharge()
        {
            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.Status == 3 && this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus != 4)
            {
                MessageBox.Show("�ò����Ѱ����Ժ���㣬��ǰΪ��ѯ״̬��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_intParm1068 != 0)
            {
                ////////����ǰ�жϲ����������Ƿ���δ�����õĴ�����������ʾ
                string strMessage = "";
                clsPublic.m_lngSelectPatientNoPayRecipe(this.m_objViewer.ucPatientInfo.BihPatient_VO.RegisterID, out strMessage);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    if (m_intParm1068 == 1)
                    {
                        if (MessageBox.Show("�Ƿ��������" + strMessage, "�����������δ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else if (m_intParm1068 == 2)
                    {
                        MessageBox.Show("���������" + strMessage, "�����������δ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //////////////////
            }
            if (ChargeDtSelect == null || ChargeDtSelect.Rows.Count == 0)
            {
                if (MessageBox.Show("�ò�����סԺ�ڼ�û�з����κη��ã��Ƿ�ֱ�Ӱ����Ժ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    frmReckoning f = new frmReckoning(this.m_objViewer.InvoNo);
                    if (this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus == 4)
                    {
                        f.ChargeType = 6;
                    }
                    else
                    {
                        f.ChargeType = 2;
                    }
                    f.ChargeDetail = null;
                    f.objPatient = this.m_objViewer.ucPatientInfo;
                    f.DayChrgType = 0;
                    f.DayAccountsArr = null;
                    f.DirectChargeOut = true;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
                    }
                }

                return;
            }

            #region ��ȡ������Ϣ
            //�������� 1 ���� 2 ��ϸ
            int DayChrgType = 1;

            Hashtable has = new Hashtable();
            for (int i = 0; i < ChargeDtSelect.Rows.Count; i++)
            {
                string dayid = ChargeDtSelect.Rows[i]["dayaccountid_chr"].ToString();

                if (!has.ContainsKey(dayid))
                {
                    has.Add(dayid, null);
                }
            }

            ArrayList DayaccountsNoArr = new ArrayList();
            DayaccountsNoArr.AddRange(has.Keys);

            DataView dvDayAll = new DataView(ChargeDt);
            DataView dvDaySub = new DataView(ChargeDtSelect);
            List<clsBihDayAccounts_VO> DayAccountsArr = new List<clsBihDayAccounts_VO>();
            for (int i = 0; i < DayaccountsNoArr.Count; i++)
            {
                string dayid = DayaccountsNoArr[i].ToString();

                clsBihDayAccounts_VO DayAccounts_VO = new clsBihDayAccounts_VO();
                DayAccounts_VO.AccountsID = dayid;
                DayAccounts_VO.ChargeEmp = this.m_objViewer.LoginInfo.m_strEmpID;

                dvDayAll.RowFilter = "dayaccountid_chr = '" + dayid + "'";
                dvDaySub.RowFilter = "dayaccountid_chr = '" + dayid + "'";

                decimal decTotalSum = 0;
                decimal decSbSum = 0;
                decimal decAcctSum = 0;

                foreach (DataRowView drv in dvDayAll)
                {
                    decimal d = clsPublic.ConvertObjToDecimal(drv["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(drv["amount_dec"]);
                    decTotalSum += clsPublic.Round(d, 2);
                    decSbSum += clsPublic.Round(d * clsPublic.ConvertObjToDecimal(drv["precent_dec"]) / 100, 2);
                }
                decAcctSum = decTotalSum - decSbSum;

                DayAccounts_VO.TotalSum = decTotalSum;
                DayAccounts_VO.SbSum = decSbSum;
                DayAccounts_VO.AcctSum = decAcctSum;

                decTotalSum = 0;
                decSbSum = 0;
                decAcctSum = 0;

                foreach (DataRowView drv in dvDaySub)
                {
                    decimal d = clsPublic.ConvertObjToDecimal(drv["unitprice_dec"]) * clsPublic.ConvertObjToDecimal(drv["amount_dec"]);
                    decTotalSum += clsPublic.Round(d, 2);
                    decSbSum += clsPublic.Round(d * clsPublic.ConvertObjToDecimal(drv["precent_dec"]) / 100, 2);
                }
                decAcctSum = decTotalSum - decSbSum;

                DayAccounts_VO.ClearSbSum = decSbSum;
                DayAccounts_VO.ClearAcctSum = decAcctSum;

                DayAccountsArr.Add(DayAccounts_VO);
            }

            #endregion

            //ֻ�ܽ�����ᡢ�������
            DataView dv = new DataView(ChargeDt);
            dv.RowFilter = "pstatus_int = 1 or pstatus_int = 2";

            DataTable dt = ChargeDt.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.Rows.Add(drv.Row.ItemArray);
            }
            dt.AcceptChanges();

            frmReckoning frec = new frmReckoning(this.m_objViewer.InvoNo);
            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus == 4)
            {
                frec.ChargeType = 6;
            }
            else
            {
                frec.ChargeType = 2;
            }
            frec.ChargeDetail = dt;
            frec.objPatient = this.m_objViewer.ucPatientInfo;
            frec.DayChrgType = DayChrgType;
            frec.DayAccountsArr = DayAccountsArr;
            if (frec.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
            }
        }
        #endregion

        #region �ش�Ʊ
        /// <summary>
        /// �ش�Ʊ
        /// </summary>        
        public void m_mthRepeatPrt()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;

            if (RegID == "")
            {
                return;
            }

            frmInvoiceRepeatPrt finvoprt = new frmInvoiceRepeatPrt(RegID);
            finvoprt.ShowDialog();
        }
        #endregion

        #region �˿�
        /// <summary>
        /// �˿�
        /// </summary>
        public void m_mthRefundment()
        {
            string RegID = this.m_objViewer.ucPatientInfo.RegisterID;

            if (RegID == "")
            {
                return;
            }

            int ChargeType = 2;
            if (this.m_objViewer.ucPatientInfo.BihPatient_VO.FeeStatus == 6)
            {
                ChargeType = 6;
            }
            frmInvoiceRefundment finvoref = new frmInvoiceRefundment(RegID, ChargeType);
            finvoref.ShowDialog();
            if (finvoref.IsRefundment)
            {
                this.m_objViewer.ucPatientInfo.m_mthShortCurFind();
            }
        }
        #endregion

        #region ����ҽ������
        /// <summary>
        /// ����ҽ������
        /// </summary>
        public void m_mthDownLoadYBData()
        {
            if (this.m_objViewer.ucPatientInfo.RegisterID == "")
            {
                return;
            }

            #region ��ȡ����ҽ��ǰ�����ݿ����
            string tmpfs = clsPublic.XMLFile;
            clsPublic.XMLFile = Application.StartupPath + @"\HISYB.xml";

            //��ȡ����ҽ��ǰ�����ݿ����                
            string DSN = clsPublic.m_strReadXML("FOSHAN.NO2", "DBDSN", "AnyOne");
            string UserID = clsPublic.m_strReadXML("FOSHAN.NO2", "DBUserID", "AnyOne");
            string PassWord = clsPublic.m_strReadXML("FOSHAN.NO2", "DBPassWord", "AnyOne");
            string Hospcode = clsPublic.m_strReadXML("FOSHAN.NO2", "HospitalNO", "AnyOne");
            string DB2Parm = "DSN=" + DSN + ";UID=" + UserID + ";PWD=" + PassWord;

            clsPublic.XMLFile = tmpfs;
            #endregion

            frmYB_F2DownLoad fd = new frmYB_F2DownLoad();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                int type = fd.DoType;
                if (type == 1)
                {
                    if (MessageBox.Show("סԺ��:" + this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh + "  ����:" + this.m_objViewer.ucPatientInfo.BihPatient_VO.Name + "\r\n\r\nȷ���Ƿ��ҽ��ǰ�û����ط�����ϸ?", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.m_objViewer.Cursor = Cursors.WaitCursor;
                        DataTable dt;
                        long l = this.objSvc.m_lngDownloadYBData(DB2Parm, Hospcode, this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.InsuredZycs.ToString(), out dt);
                        if (l > 0)
                        {
                            l = this.objSvc.m_lngDownloadYBData(dt);
                            if (l <= 0)
                            {
                                this.m_objViewer.Cursor = Cursors.Default;
                                MessageBox.Show("��������ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            this.m_objViewer.Cursor = Cursors.Default;
                            MessageBox.Show("��������ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
                else if (type == 2)
                {
                    if (MessageBox.Show("סԺ��:" + this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh + "  ����:" + this.m_objViewer.ucPatientInfo.BihPatient_VO.Name + "\r\n\r\nȷ���Ƿ�ɾ��?", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.m_objViewer.Cursor = Cursors.WaitCursor;

                        long l = this.objSvc.m_lngDelDownloadYBData(this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.InsuredZycs);
                        if (l <= 0)
                        {
                            this.m_objViewer.Cursor = Cursors.Default;
                            MessageBox.Show("ɾ������ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
                else if (type == 3)
                {
                    this.m_objViewer.Cursor = Cursors.WaitCursor;
                    DataTable dt;
                    long l = this.objSvc.m_lngGetDownloadYBData(this.m_objViewer.ucPatientInfo.BihPatient_VO.Zyh, this.m_objViewer.ucPatientInfo.BihPatient_VO.InsuredZycs, out dt);
                    if (l > 0)
                    {
                        l = this.objSvc.m_lngSendybdata(DB2Parm, dt);
                        if (l <= 0)
                        {
                            this.m_objViewer.Cursor = Cursors.Default;
                            MessageBox.Show("�ϴ���������ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        this.m_objViewer.Cursor = Cursors.Default;
                        MessageBox.Show("��ȡ��������ʧ��.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                this.m_objViewer.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region ���Ӥ����
        /// <summary>
        /// ���Ӥ����
        /// </summary>
        /// <param name="Zyh"></param>
        public void m_mthCheckBaby(string Zyh)
        {
            DataTable dt;
            long l = this.objSvc.m_lngCheckBaby(Zyh, out dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                string Msg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Msg += dr["inpatientid_chr"].ToString() + "  " + dr["lastname_vchr"].ToString() + "  " + dr["sex_chr"].ToString() + "  \r\n\r\n";
                }

                MessageBox.Show("��ܰ��ʾ��\r\n\r\n" + Msg + "����δ�ᡣ", "��Ժ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


        #region �жϸò����Ƿ���Ӥ������-ĸ�׺ϲ������� by yibin.zheng 09-07-03
        /// <summary>
        /// �жϸò����Ƿ���Ӥ������
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        public bool m_blnHaveBaby(string p_strRegisterId, out DataTable p_dtbBabyCharge)
        {
            DataTable dtbBaby = null;
            p_dtbBabyCharge = new DataTable();
            long lngRes = this.objSvc.m_lngGetBabyRegisterId(p_strRegisterId, out dtbBaby);
            if (dtbBaby.Rows.Count > 0 && lngRes > 0)
            {
                int intRowsCount = dtbBaby.Rows.Count;
                string strBabyRegisterId = "";
                for (int i = 0; i < intRowsCount; i++)
                {
                    strBabyRegisterId += "'" + dtbBaby.Rows[i]["registerid_chr"] + "',";
                }
                strBabyRegisterId = strBabyRegisterId.Remove(strBabyRegisterId.Length - 1);
                lngRes = this.objSvc.m_lngCheckBabyNoPayCharge(strBabyRegisterId, out p_dtbBabyCharge);
                if (lngRes > 0 && p_dtbBabyCharge.Rows.Count > 0)
                {
                    return true;
                }
            }

            return false;
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
    }
}
