using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ͨ��PB��ӡ��
    /// </summary>
    public class clsPBNetPrint
    {
        /// <summary>
        /// �Ƿ����°淢Ʊ��ӡ
        /// </summary>
        private static int isOpen = clsPublic.m_intGetSysParm("1145");

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private static int intDiffCostOn = clsPublic.m_intGetSysParm("9002");

        #region Ʊ�ݴ�ӡ
        /// <summary>
        /// Ʊ�ݴ�ӡ
        /// </summary>
        /// <param name="BillFileName">���ݶ�������</param>
        /// <param name="TextValueArr">ֵ�б�</param>
        /// <param name="ShowPrintDialog">True ��ʾ��ӡ�Ի����� False ����ʾ��ӡ�Ի�����</param>
        /// <param name="AllowCancelPrt">��ʾ������ӡ����</param>
        public static void BillPrint(string BillFileName, ArrayList TextValueArr, bool ShowPrintDialog, bool ShowCancelDialog)
        {
            try
            {
                DataStore dsBill = new DataStore();

                if (isOpen == 0)
                {
                    dsBill.LibraryList = clsPublic.PBLPath;
                }
                else
                {
                    dsBill.LibraryList = Application.StartupPath + @"\pb_Invioce.pbl";
                }
                dsBill.DataWindowObject = BillFileName;

                dsBill.InsertRow(0);

                for (int i = 0; i < TextValueArr.Count; i++)
                {
                    dsBill.Modify(TextValueArr[i].ToString());
                }

                if (ShowPrintDialog)
                {
                    clsPublic.ChoosePrintDialog(dsBill, ShowCancelDialog);
                }
                else
                {
                    dsBill.Print(ShowCancelDialog);
                }

                dsBill.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ɵ���ʧ�ܣ�\r\n\r\n  " + ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion



        #region Ʊ�ݴ�ӡ��ѡ���ӡ����
        /// <summary>
        /// Ʊ�ݴ�ӡ��ѡ���ӡ����CS-586 (ID:15626)add by zxm2012-12-29
        /// </summary>
        /// <param name="BillPrinterName">��ӡ������</param>
        /// <param name="BillFileName">���ݶ�������</param>
        /// <param name="TextValueArr">ֵ�б�</param>
        /// <param name="ShowPrintDialog">True ��ʾ��ӡ�Ի����� False ����ʾ��ӡ�Ի�����</param>
        /// <param name="AllowCancelPrt">��ʾ������ӡ����</param>
        public static void BillPrint_new(string BillPrinterName, string BillFileName, ArrayList TextValueArr, bool ShowPrintDialog, bool ShowCancelDialog)
        {
            try
            {
                DataStore dsBill = new DataStore();

                if (isOpen == 0)
                {
                    dsBill.LibraryList = clsPublic.PBLPath;
                }
                else
                {
                    dsBill.LibraryList = Application.StartupPath + @"\pb_Invioce.pbl";
                }
                dsBill.DataWindowObject = BillFileName;

                dsBill.InsertRow(0);

                for (int i = 0; i < TextValueArr.Count; i++)
                {
                    dsBill.Modify(TextValueArr[i].ToString());
                }

                if (ShowPrintDialog)
                {
                    clsPublic.ChoosePrintDialog(dsBill, ShowCancelDialog);
                }
                else
                {
                    dsBill.PrintProperties.PrinterName = BillPrinterName;
                    dsBill.Print(ShowCancelDialog);
                }

                dsBill.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ɵ���ʧ�ܣ�\r\n\r\n  " + ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region Ʊ��Ԥ��
        /// <summary>
        /// Ʊ��Ԥ��
        /// </summary>
        /// <param name="DWC"></param>
        /// <param name="TextValueArr"></param>
        public static void BillPreview(DataWindowControl DWC, ArrayList TextValueArr)
        {
            try
            {
                for (int i = 0; i < TextValueArr.Count; i++)
                {
                    DWC.Modify(TextValueArr[i].ToString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ɵ���ʧ�ܣ�\r\n\r\n  " + ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region ��ӡѺ��
        /// <summary>
        /// ��ӡѺ��
        /// </summary>
        /// <param name="PrePayID"></param>
        /// <param name="RepNo">�ش��º�</param>
        public static void m_mthPrintPrepayBill_old(string PrePayID, string RepNo)
        {
            DataTable dtPrePay;
            clsDcl_PrePay objPrePay = new clsDcl_PrePay();
            long l = objPrePay.m_lngGetPrepayByPrePayID(PrePayID, out dtPrePay);
            if (l > 0 && dtPrePay.Rows.Count == 1)
            {
                ArrayList TextValueArr = new ArrayList();

                if (RepNo.Trim() == "")
                {
                    TextValueArr.Add("t_prepayno.text = '" + dtPrePay.Rows[0]["PREPAYINV_VCHR"].ToString() + "'");
                    TextValueArr.Add("t_repeatno.text = ''");
                }
                else
                {
                    TextValueArr.Add("t_prepayno.text = '" + RepNo + "'");
                    TextValueArr.Add("t_repeatno.text = '*REPEAT(" + dtPrePay.Rows[0]["PREPAYINV_VCHR"].ToString() + ")*'");
                }
                TextValueArr.Add("t_date.text = '" + Convert.ToDateTime(dtPrePay.Rows[0]["CREATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                TextValueArr.Add("t_zyh.text = '" + dtPrePay.Rows[0]["INPATIENTID_CHR"].ToString() + "'");
                TextValueArr.Add("t_name.text = '" + dtPrePay.Rows[0]["LASTNAME_VCHR"].ToString() + "'");

                string paytype = dtPrePay.Rows[0]["CUYCATE_INT"].ToString();
                if (paytype == "1")
                {
                    paytype = "�ֽ�";
                }
                else if (paytype == "2")
                {
                    paytype = "֧Ʊ";
                }
                else if (paytype == "3")
                {
                    paytype = "���п�";
                }
                else if (paytype == "4")
                {
                    paytype = "΢��2";
                }
                else if (paytype == "5")
                {
                    paytype = "����";
                }
                else if (paytype == "6")
                {
                    paytype = "֧����";     // ����.֧����
                }
                else if (paytype == "8")
                {
                    paytype = "΢��";
                }
                else if (paytype == "9")
                {
                    paytype = "֧����";     // ����.֧����
                }
                else
                {
                    paytype = "�ֽ�";
                }

                double money = Convert.ToDouble(dtPrePay.Rows[0]["MONEY_DEC"].ToString());
                TextValueArr.Add("t_paytype.text = '" + paytype + "'");
                //TextValueArr.Add("t_samllmoney.text = '��" + money.ToString("####,##0.00") + "'");
                TextValueArr.Add("t_samllmoney.text = '" + money.ToString("####,##0.00") + "'");
                TextValueArr.Add("t_bigmoney.text = '" + clsPublic.DoubleConvertToCurrency(money) + "'");
                TextValueArr.Add("t_operno.text = '" + dtPrePay.Rows[0]["empname"].ToString() + "'");
                TextValueArr.Add("t_areaname.text = '" + dtPrePay.Rows[0]["deptname_vchr"].ToString() + "'");
                TextValueArr.Add("t_bedno.text = '" + dtPrePay.Rows[0]["bed_no"].ToString() + "'");

                DateTime dtmCreateDay = DateTime.Parse(dtPrePay.Rows[0]["CREATE_DAT"].ToString());
                TextValueArr.Add("t_year.text = '" + dtmCreateDay.Year.ToString() + "'");
                TextValueArr.Add("t_month.text = '" + dtmCreateDay.Month.ToString() + "'");
                TextValueArr.Add("t_day.text = '" + dtmCreateDay.Day.ToString() + "'");


                float fTotalmny = Convert.ToSingle(money);
                long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(fTotalmny), new decimal(100))));

                //ʮ��
                //TextValueArr[0].Add("t_sw.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100000 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (100000 * 100);
                TextValueArr.Add("t_wan.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10000 * 100))).ToString() + "'");
                //ǧ
                lTotalmny = lTotalmny % (10000 * 100);
                TextValueArr.Add("t_qian.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1000 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (1000 * 100);
                TextValueArr.Add("t_bai.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100 * 100))).ToString() + "'");
                //ʮ
                lTotalmny = lTotalmny % (100 * 100);
                TextValueArr.Add("t_shi.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10 * 100))).ToString() + "'");
                //Ԫ
                lTotalmny = lTotalmny % (10 * 100);
                TextValueArr.Add("t_yuan.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (1 * 100);
                TextValueArr.Add("t_jie.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % 10;
                TextValueArr.Add("t_fen.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1))).ToString() + "'");

                clsPBNetPrint.BillPrint("d_prepaybill", TextValueArr, true, false);
            }
        }

        #region ��ӡסԺԤ�����վ�
        /// <summary>
        /// ��ӡסԺԤ�����վ�
        /// </summary>
        /// <param name="PrePayID">Ԥ����</param>
        /// <param name="RepNo"></param>
        public static void m_mthPrintPrepayBill(string PrePayID, string RepNo)
        {
            DataTable dtPrePay;
            clsDcl_PrePay objPrePay = new clsDcl_PrePay();
            long l = objPrePay.m_lngGetPrepayByPrePayID(PrePayID, out dtPrePay);

            if (l > 0 && dtPrePay.Rows.Count == 1)
            {
                DataRow oneRow = dtPrePay.Rows[0];

                ArrayList TextValueArr = new ArrayList();

                TextValueArr.Add("inpatient_id.text = '" + oneRow["inpatientid_chr"].ToString() + "'");
                TextValueArr.Add("dept_in.text = '" + oneRow["areaname_vchr"].ToString() + "'");
                TextValueArr.Add("name.text = '" + oneRow["patientname_chr"].ToString() + "'");



                string paytype = oneRow["cuycate_int"].ToString();
                if (paytype == "1")
                {
                    paytype = "�ֽ�";
                }
                else if (paytype == "2")
                {
                    paytype = "֧Ʊ";
                }
                else if (paytype == "3")
                {
                    paytype = "���п�";
                }
                else if (paytype == "4")
                {
                    paytype = "΢��2";
                }
                else if (paytype == "5")
                {
                    paytype = "����";
                }
                else if (paytype == "6")
                {
                    paytype = "֧����";     // ����.֧����
                }
                else if (paytype == "8")
                {
                    paytype = "΢��";
                }
                else if (paytype == "9")
                {
                    paytype = "֧����";     // ����.֧����
                }
                else
                {
                    paytype = "�ֽ�";
                }

                TextValueArr.Add("pay_way.text = '" + paytype + "'");

                TextValueArr.Add("operator_no.text = '" + oneRow["empno_chr"].ToString() + "'");
                TextValueArr.Add("amount.text = '" + oneRow["money_dec"].ToString() + "'");
                TextValueArr.Add("t_jine.text = '��" + Convert.ToDouble(oneRow["money_dec"].ToString()).ToString("####,##0.00") + "'");
                float fTotalmny = Convert.ToSingle(oneRow["money_dec"].ToString());
                long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(fTotalmny), new decimal(100))));

                string strTemp = lTotalmny.ToString();
                int intCount = strTemp.Length;

                for (int intI1 = 0; intI1 < intCount; intI1++)
                {
                    string strTemp1 = "youn_" + intI1.ToString() + ".text = '";
                    TextValueArr.Add(strTemp1 + strTemp.Substring(intCount - intI1 - 1, 1) + "'");
                }
                string strTemp2 = "youn_" + intCount.ToString() + ".text = '";
                TextValueArr.Add(strTemp2 + "��" + "'");


                //ʮ��
                TextValueArr.Add("text_shi.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100000 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (100000 * 100);
                TextValueArr.Add("text_wan.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10000 * 100))).ToString() + "'");
                //ǧ
                lTotalmny = lTotalmny % (10000 * 100);
                TextValueArr.Add("text_qian.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1000 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (1000 * 100);
                TextValueArr.Add("text_bai.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100 * 100))).ToString() + "'");
                //ʮ
                lTotalmny = lTotalmny % (100 * 100);
                TextValueArr.Add("text_shi1.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10 * 100))).ToString() + "'");
                //Ԫ
                lTotalmny = lTotalmny % (10 * 100);
                TextValueArr.Add("text_yuan.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1 * 100))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % (1 * 100);
                TextValueArr.Add("text_jiao.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10))).ToString() + "'");
                //��
                lTotalmny = lTotalmny % 10;
                TextValueArr.Add("text_fen.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1))).ToString() + "'");

                TextValueArr.Add("charge_type.text = '" + oneRow["paytypename_vchr"].ToString() + "'");

                strTemp = oneRow["paytype_int"].ToString();
                if (strTemp == "1")
                {
                    TextValueArr.Add("transact_type.text = '" + "����" + "'");
                }
                else if (strTemp == "2")
                {
                    TextValueArr.Add("transact_type.text = '" + "�˿�" + "'");
                }
                else if (strTemp == "3")
                {
                    TextValueArr.Add("transact_type.text = '" + "�ָ�" + "'");
                }
                else
                {
                    TextValueArr.Add("transact_type.text = '" + "�嵥" + "'");
                }

                if (string.IsNullOrEmpty(RepNo))
                {
                    TextValueArr.Add("t_no.text = '" + oneRow["prepayinv_vchr"].ToString() + "'");
                }
                else
                {
                    //�ش�Ʊ����
                    TextValueArr.Add("t_no.text = '" + RepNo.ToString() + "'");
                }
                TextValueArr.Add("t_hospitalname.text='" + Common.Entity.GlobalParm.HospitalName + "'");
                //com.digitalwave.iCare.common.clsCommmonInfo.m_objHospitalInfo.m_strHOSPITAL_NAME_CHR

                //clsPBNetPrint.BillPrint("d_prepaybill", TextValueArr, false, false);

                //CS-586 (ID:15626)
                clsPublic.XMLFile = Application.StartupPath + @"\LoginFile.xml";
                //��ȡָ����ӡ��
                string strPrinter = clsPublic.m_strReadXML("HISPreCharge", "HISPreInvoicePrinter", "AnyOne");

                clsPBNetPrint.BillPrint_new(strPrinter, "d_prepaybill", TextValueArr, false, false);
            }
        }
        #endregion
        #endregion

        #region ��ӡ��Ʊ
        /// <summary>
        /// Ԥ����Ʊ
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="RepNo"></param>
        /// <param name="DWC"></param>
        /// <param name="Scope">1 ���� 2 סԺ</param>
        /// <param name="HospitalName"></param>
        public static void m_mthPreviewInvoiceBill(string ChargeNo, string RepNo, DataWindowControl DWC, int Scope, string HospitalName)
        {
            ArrayList[] TextValueArr;
            clsPBNetPrint.m_mthGetInvoiceContent(ChargeNo, RepNo, Scope, HospitalName, out TextValueArr);
            clsPBNetPrint.BillPreview(DWC, TextValueArr[0]);
        }
        /// <summary>
        /// ��ӡ��Ʊ
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="RepNo"></param>
        /// <param name="Scope">1 ���� 2 סԺ</param>
        /// <param name="HospitalName"></param>
        public static void m_mthPrintInvoiceBill(string ChargeNo, string RepNo, int Scope, string HospitalName)
        {
            ArrayList[] TextValueArr;
            clsPBNetPrint.m_mthGetInvoiceContent(ChargeNo, RepNo, Scope, HospitalName, out TextValueArr);
            if (Scope == 1)
            {
                for (int i = 0; i < TextValueArr.Length; i++)
                {
                    clsPBNetPrint.BillPrint("d_op_invoice_prt_new_diff", TextValueArr[i], false, false);
                }
            }
            else if (Scope == 2)
            {
                if (isOpen == 0)
                    clsPBNetPrint.BillPrint("d_invoice_prt", TextValueArr[0], true, false);
                else
                    clsPBNetPrint.BillPrint("d_invoice_prt_gd_diff", TextValueArr[0], true, false);
            }
        }
        /// <summary>
        /// ��ȡ��Ʊ����
        /// </summary>
        /// <param name="ChargeNo">�����</param>
        /// <param name="RepNo">�ش��</param>   
        /// <param name="Scope">1 ���� 2 סԺ</param>   
        /// <param name="HospitalName"></param>
        /// <param name="TextValueArr"></param>    
        public static void m_mthGetInvoiceContent(string ChargeNo, string RepNo, int Scope, string HospitalName, out ArrayList[] TextValueArr)
        {
            long l = 0;
            TextValueArr = null;

            DataTable dtInvoiceMain;
            DataTable dtInvoiceDet;
            DataTable dtPrepay;
            DataTable dtPayMode;
            DataTable dtItemDate;

            clsDcl_Charge objCharge = new clsDcl_Charge();

            if (Scope == 1)
            {
                #region ����
                if (Microsoft.VisualBasic.Information.IsNumeric(ChargeNo))
                {
                    l = objCharge.m_lngGetOPInvoiceByChargeNo(ChargeNo, out dtInvoiceMain, out dtInvoiceDet, out dtPayMode);
                }
                else
                {
                    if (RepNo == "*")
                    {
                        l = objCharge.m_lngGetOPInvoiceByInvoNo(0, ChargeNo, out dtInvoiceMain, out dtInvoiceDet, out dtPayMode);
                        RepNo = "";
                    }
                    else
                    {
                        l = objCharge.m_lngGetOPInvoiceByInvoNo(ChargeNo, out dtInvoiceMain, out dtInvoiceDet, out dtPayMode);
                    }
                }
                DataView dvInvoiceDet = new DataView(dtInvoiceDet);
                if (l > 0 && dtInvoiceMain.Rows.Count > 0)
                {
                    TextValueArr = new ArrayList[dtInvoiceMain.Rows.Count];

                    for (int i = 0; i < dtInvoiceMain.Rows.Count; i++)
                    {
                        TextValueArr[i] = new ArrayList();

                        TextValueArr[i].Add("t_zlkh.text = '" + dtInvoiceMain.Rows[i]["patientcardid_chr"].ToString() + "'");
                        //TextValueArr[i].Add("t_brlx.text = '" + dtInvoiceMain.Rows[i]["paytypename_vchr"].ToString() + "'");

                        DateTime invdate = Convert.ToDateTime(dtInvoiceMain.Rows[i]["invdate_dat"].ToString());
                        TextValueArr[i].Add("t_year.text = '" + invdate.Year.ToString() + "'");
                        TextValueArr[i].Add("t_month.text = '" + invdate.Month.ToString() + "'");
                        TextValueArr[i].Add("t_day.text = '" + invdate.Day.ToString() + "'");

                        TextValueArr[i].Add("t_xlh.text = '" + dtInvoiceMain.Rows[i]["seqid_chr"].ToString() + "'");
                        TextValueArr[i].Add("t_fb.text = '" + dtInvoiceMain.Rows[i]["paytypename_vchr"].ToString() + "'");
                        TextValueArr[i].Add("t_name.text = '" + dtInvoiceMain.Rows[i]["patientname_chr"].ToString() + "'");
                        if (intDiffCostOn == 1)// ��ӡҩƷ������
                        {
                            TextValueArr[i].Add("t_ypyl.text = '" + dtInvoiceMain.Rows[i]["totaldiffcost_mny"].ToString() + "'");
                            TextValueArr[i].Add("t_ypyl2.text = '" + dtInvoiceMain.Rows[i]["totaldiffcost_mny"].ToString() + "'");
                        }

                        string PayModeName = "";
                        if (dtPayMode.Rows.Count > 0)
                        {
                            for (int i1 = 0; i1 < dtPayMode.Rows.Count; i1++)
                            {
                                //֧����ʽ 0 �ֽ� 1 ���п� 2 ֧Ʊ 3 IC�� 4 ���л� 5 ����
                                switch (dtPayMode.Rows[i1]["paytype_int"].ToString())
                                {
                                    case "0":
                                        PayModeName += "�ֽ�";
                                        break;
                                    case "1":
                                        PayModeName += "���п�";
                                        break;
                                    case "2":
                                        PayModeName += "֧Ʊ";
                                        break;
                                    case "3":
                                        PayModeName += "IC��";
                                        break;
                                    case "4":
                                        PayModeName += "���л�";
                                        break;
                                    case "5":
                                        PayModeName += "΢��2";
                                        break;                                    
                                    case "8":
                                        PayModeName += "΢��";
                                        break;
                                    case "9":
                                        PayModeName += "֧����";         
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        TextValueArr[i].Add("t_paytype.text = '" + PayModeName + "'");

                        //��Ʊ�ܽ��
                        decimal decTotalmny = clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["totalsum_mny"]);
                        if (intDiffCostOn == 1)//��ȥ����
                            decTotalmny += clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["totaldiffcost_mny"]);
                        float fTotalmny = Convert.ToSingle(decTotalmny);
                        long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(fTotalmny), new decimal(100))));

                        //��
                        lTotalmny = lTotalmny % (100000 * 100);
                        TextValueArr[i].Add("t_w.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10000 * 100))).ToString() + "'");
                        //ǧ
                        lTotalmny = lTotalmny % (10000 * 100);
                        TextValueArr[i].Add("t_q.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1000 * 100))).ToString() + "'");
                        //��
                        lTotalmny = lTotalmny % (1000 * 100);
                        TextValueArr[i].Add("t_b.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100 * 100))).ToString() + "'");
                        //ʮ
                        lTotalmny = lTotalmny % (100 * 100);
                        TextValueArr[i].Add("t_s.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10 * 100))).ToString() + "'");
                        //Ԫ
                        lTotalmny = lTotalmny % (10 * 100);
                        TextValueArr[i].Add("t_y.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1 * 100))).ToString() + "'");
                        //��
                        lTotalmny = lTotalmny % (1 * 100);
                        TextValueArr[i].Add("t_j.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10))).ToString() + "'");
                        //��
                        lTotalmny = lTotalmny % 10;
                        TextValueArr[i].Add("t_f.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1))).ToString() + "'");

                        //״̬ 0-���� 1-��Ч 2-��Ʊ 3-�ָ�
                        string status = dtInvoiceMain.Rows[i]["status_int"].ToString();
                        if (status == "2")
                        {
                            status = "-";
                        }
                        else
                        {
                            status = "";
                        }

                        TextValueArr[i].Add("t_total.text = '" + decTotalmny.ToString("0.00") + "'");

                        if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["acctsum_mny"]) != 0)
                        {
                            TextValueArr[i].Add("t_acctsum.text = '" + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["acctsum_mny"]).ToString("0.00") + "'");
                        }
                        else
                        {
                            TextValueArr[i].Add("t_acctsum.text = ''");
                        }

                        if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["sbsum_mny"]) != 0)
                        {
                            TextValueArr[i].Add("t_sbsum.text = '" + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[i]["sbsum_mny"]).ToString("0.00") + "'");
                        }
                        else
                        {
                            TextValueArr[i].Add("t_sbsum.text = ''");
                        }

                        TextValueArr[i].Add("t_gatheremp.text = '" + dtInvoiceMain.Rows[i]["a"].ToString() + "'");
                        TextValueArr[i].Add("t_doctor.text = '" + dtInvoiceMain.Rows[i]["b"].ToString() + "'");

                        TextValueArr[i].Add("t_confirmemp.text = '" + "(" + dtInvoiceMain.Rows[i]["confdept"].ToString() + ")" + dtInvoiceMain.Rows[i]["b"].ToString() + "'");

                        //��Ʊ��ϸ
                        Hashtable has = new Hashtable();
                        dvInvoiceDet.RowFilter = "invoiceno_vchr = '" + dtInvoiceMain.Rows[i]["invoiceno_vchr"].ToString() + "'";
                        for (int i1 = 0; i1 < dvInvoiceDet.Count; i1++)
                        {
                            has.Add(dvInvoiceDet[i1]["itemcatid_chr"].ToString(), dvInvoiceDet[i1]["totalsum_mny"].ToString());
                        }

                        string catid = "";
                        DataTable dt;
                        l = objCharge.m_lngGetDefChargeCat("2", "1", out dt);
                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            catid = dt.Rows[i1]["catid_chr"].ToString();

                            //�������ͣ� 0 ��ͨ�� 1 ������
                            string type = dt.Rows[i1]["type_int"].ToString();

                            if (type == "0")
                            {
                                if (!has.ContainsKey(catid))
                                {
                                    TextValueArr[i].Add(dt.Rows[i1]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                                    continue;
                                }
                                else
                                {
                                    TextValueArr[i].Add(dt.Rows[i1]["prtclt_vchr"].ToString().Trim() + ".text = '" + status + clsPublic.ConvertObjToDecimal(has[catid]).ToString("0.00") + "'");
                                }
                            }
                            else if (type == "1")
                            {
                                string compexp = dt.Rows[i1]["compexp_vchr"].ToString().Trim().Replace("��", "+");
                                decimal total = 0;

                                System.Collections.Generic.List<string> catidarr = clsPublic.m_ArrGettoken(compexp, "+");
                                for (int j = 0; j < catidarr.Count; j++)
                                {
                                    catid = catidarr[j].ToString().Trim();
                                    if (has.ContainsKey(catid))
                                    {
                                        total += clsPublic.ConvertObjToDecimal(has[catid]);
                                    }
                                }

                                if (total > 0)
                                {
                                    TextValueArr[i].Add(dt.Rows[i1]["prtclt_vchr"].ToString().Trim() + ".text = '" + status + total.ToString("0.00") + "'");
                                }
                                else
                                {
                                    TextValueArr[i].Add(dt.Rows[i1]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                                }
                            }
                        }

                        //��Ʊ�š��ش�Ʊ��
                        if (RepNo == "")
                        {
                            TextValueArr[i].Add("t_fph.text = '" + dtInvoiceMain.Rows[i]["invoiceno_vchr"].ToString() + "'");
                            TextValueArr[i].Add("t_repeatinvono.text = ''");
                        }
                        else
                        {
                            TextValueArr[i].Add("t_fph.text = '" + RepNo + "'");
                            TextValueArr[i].Add("t_repeatinvono.text = '*REPEAT(" + dtInvoiceMain.Rows[i]["invoiceno_vchr"].ToString() + ")*'");
                        }

                        TextValueArr[i].Add("t_hospital.text = '" + HospitalName + "'");

                        if (dtInvoiceMain.Rows[i]["medsendwininfo_vchr"].ToString().Trim() != "")
                        {
                            TextValueArr[i].Add("t_sendmedwindowname.text = '�뵽:" + dtInvoiceMain.Rows[i]["medsendwininfo_vchr"].ToString().Trim() + "ȡҩ'");
                        }
                        if (HospitalName.IndexOf("�׽�ҽԺ") >= 0)
                        {
                            TextValueArr[i].Add("r_badge.visible = '1'");
                            TextValueArr[i].Add("t_badge1.visible = '1'");
                            TextValueArr[i].Add("t_badge2.visible = '1'");
                        }
                    }
                }
                #endregion
            }
            else if (Scope == 2)
            {
                #region סԺ
                TextValueArr = new ArrayList[1];
                TextValueArr[0] = new ArrayList();

                l = objCharge.m_lngGetInvoiceByChargeNo(ChargeNo, out dtInvoiceMain, out dtInvoiceDet, out dtPrepay, out dtPayMode, out dtItemDate);
                if (l > 0)
                {
                    TextValueArr[0].Add("t_zyh.text = '" + dtInvoiceMain.Rows[0]["inpatientid_chr"].ToString() + "(" + dtInvoiceMain.Rows[0]["inpatientcount_int"].ToString() + ")'");
                    TextValueArr[0].Add("t_area.text = '" + dtInvoiceMain.Rows[0]["deptname_vchr"].ToString() + "'");

                    DateTime invdate = Convert.ToDateTime(dtInvoiceMain.Rows[0]["invdate_dat"].ToString());
                    TextValueArr[0].Add("t_year.text = '" + invdate.Year.ToString() + "'");
                    TextValueArr[0].Add("t_month.text = '" + invdate.Month.ToString() + "'");
                    TextValueArr[0].Add("t_day.text = '" + invdate.Day.ToString() + "'");

                    TextValueArr[0].Add("t_name.text = '" + dtInvoiceMain.Rows[0]["lastname_vchr"].ToString() + "'");
                    TextValueArr[0].Add("t_insur.text = '" + dtInvoiceMain.Rows[0]["insuranceid_vchr"].ToString().Trim() + "'");
                    if (dtInvoiceMain.Rows[0]["sex_chr"].ToString().Trim() == "��")
                    {
                        TextValueArr[0].Add("l_nan1.visible = '1'");
                        TextValueArr[0].Add("l_nan2.visible = '1'");
                    }
                    else if (dtInvoiceMain.Rows[0]["sex_chr"].ToString().Trim() == "Ů")
                    {
                        TextValueArr[0].Add("l_nv1.visible = '1'");
                        TextValueArr[0].Add("l_nv2.visible = '1'");
                    }
                    string InDate = dtInvoiceMain.Rows[0]["inpatient_dat"].ToString();
                    string OutDate = dtInvoiceMain.Rows[0]["outpatient_dat"].ToString();
                    if (OutDate == "")
                    {
                        string date1 = "";
                        string date2 = "";
                        if (dtItemDate.Rows.Count == 1)
                        {
                            date1 = dtItemDate.Rows[0]["mindate"].ToString();
                            date2 = dtItemDate.Rows[0]["maxdate"].ToString();
                        }

                        if (date1 == date2 && date1 != "")
                        {
                            TextValueArr[0].Add("t_indate.text = '" + date1 + "'");
                        }
                        else
                        {
                            if (date1 != date2 && date1 != "" && date2 != "")
                            {
                                if (isOpen == 0)
                                    TextValueArr[0].Add("t_indate.text = '" + date1 + "��" + date2 + "'");
                                else
                                {
                                    TextValueArr[0].Add("t_indate.text = '" + date1 + "'");
                                    TextValueArr[0].Add("t_outdate.text = '" + date2 + "'");
                                }
                            }
                            else
                            {
                                TextValueArr[0].Add("t_indate.text = '" + Convert.ToDateTime(InDate).ToString("yyyy-MM-dd") + "'");
                            }
                        }
                    }
                    else
                    {
                        if (isOpen == 0)
                            TextValueArr[0].Add("t_indate.text = '" + Convert.ToDateTime(InDate).ToString("yyyy-MM-dd") + "��" + Convert.ToDateTime(OutDate).ToString("yyyy-MM-dd") + "'");
                        else
                        {
                            TextValueArr[0].Add("t_indate.text = '" + Convert.ToDateTime(InDate).ToString("yyyy-MM-dd") + "'");
                            TextValueArr[0].Add("t_outdate.text = '" + Convert.ToDateTime(OutDate).ToString("yyyy-MM-dd") + "'");
                        }
                    }

                    if (isOpen == 0)
                        TextValueArr[0].Add("t_paytype.text = '" + dtInvoiceMain.Rows[0]["paytypename_vchr"].ToString() + "'");
                    else
                        TextValueArr[0].Add("t_chargetype.text = '" + dtInvoiceMain.Rows[0]["paytypename_vchr"].ToString() + "'");
                    //��Ʊ��ϸ
                    Hashtable has = new Hashtable();
                    decimal decDiffCost = 0;
                    for (int i = 0; i < dtInvoiceDet.Rows.Count; i++)
                    {
                        has.Add(dtInvoiceDet.Rows[i]["itemcatid_chr"].ToString(), dtInvoiceDet.Rows[i]["totalsum_mny"].ToString());
                        if (string.Compare(dtInvoiceDet.Rows[i]["itemcatid_chr"].ToString(), "3026") == 0)
                            decDiffCost += clsPublic.ConvertObjToDecimal(dtInvoiceDet.Rows[i]["totalsum_mny"]);//�����ܽ��
                    }
                    decDiffCost = clsPublic.Round(decDiffCost, 2);
                    //��Ʊ�ܽ��(=�����ܽ��)
                    decimal decTotalmny = clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["totalsum_mny"]);
                    if (intDiffCostOn == 1)// ��ӡҩƷ������
                    {
                        TextValueArr[0].Add("t_ypyl.text = '" + decDiffCost.ToString("0.00") + "'");
                        TextValueArr[0].Add("t_ypyl2.text = '" + (-1 * decDiffCost).ToString("0.00") + "'");
                        //decTotalmny += decDiffCost * (-1);//�������ӻ���
                    }


                    //float fTotalmny = Convert.ToSingle(decTotalmny);
                    //long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(fTotalmny), new decimal(100))));

                    //���ڵ��ܽ�������֮��ʹ��float����ľ��Ȼ���ֶ�ʧ���������ڸ���double���ͼ���
                    double dTotalmny = Convert.ToDouble(decTotalmny);
                    long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(dTotalmny), new decimal(100))));

                    #region ����ƽ�������ϵĽ���ӡ��ʾ����

                    //���ݳ�ƽҽԺ�����󣬷�Ʊ���ܽ����Գ���ʮ������
                    string strMoreThanMillion = string.Empty;  //�����������ϵ�

                    string[] strChineseNumber = { "��", "Ǫ", "��", "ʰ", "��", "Ǫ" };
                    string strNumber = Convert.ToString(lTotalmny / (1000000 * 100));
                    int intLen = strNumber.Length;
                    if (strNumber != "0" && intLen > 0 && intLen <= 6)
                    {
                        string s = string.Empty;
                        for (int i1 = 1; i1 <= intLen; i1++)
                        {
                            s = strNumber.Substring(intLen - i1, 1);
                            strMoreThanMillion = string.Concat(clsPublic.NumberToChineseNumber(s).ToString() + strChineseNumber[i1 - 1], strMoreThanMillion);
                        }

                        //ȡģ�õ�ʮ��λ����ֵ
                        lTotalmny = lTotalmny % (1000000 * 100);
                    }

                    #endregion

                    //�������ʱ��DataWindows�е�ʮ��ġ�ʰ�������ص�
                    // TextValueArr[0].Add("t_30.visible = '1'");
                    // 2019-03-11
                    TextValueArr[0].Add("t_30.visible = '0'");

                    //ʮ��
                    TextValueArr[0].Add("t_sw.text = '" + strMoreThanMillion + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100000 * 100))).ToString() + "'");
                    //��
                    lTotalmny = lTotalmny % (100000 * 100);
                    TextValueArr[0].Add("t_w.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10000 * 100))).ToString() + "'");
                    //ǧ
                    lTotalmny = lTotalmny % (10000 * 100);
                    TextValueArr[0].Add("t_q.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1000 * 100))).ToString() + "'");
                    //��
                    lTotalmny = lTotalmny % (1000 * 100);
                    TextValueArr[0].Add("t_b.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100 * 100))).ToString() + "'");
                    //ʮ
                    lTotalmny = lTotalmny % (100 * 100);
                    TextValueArr[0].Add("t_s.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10 * 100))).ToString() + "'");
                    //Ԫ
                    lTotalmny = lTotalmny % (10 * 100);
                    TextValueArr[0].Add("t_y.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1 * 100))).ToString() + "'");
                    //��
                    lTotalmny = lTotalmny % (1 * 100);
                    TextValueArr[0].Add("t_j.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10))).ToString() + "'");
                    //��
                    lTotalmny = lTotalmny % 10;
                    TextValueArr[0].Add("t_f.text = '" + clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1))).ToString() + "'");

                    TextValueArr[0].Add("t_total.text = '" + decTotalmny.ToString("0.00") + "'");

                    //״̬ 0-���� 1-��Ч 2-��Ʊ 3-�ָ�
                    string status = dtInvoiceMain.Rows[0]["status_int"].ToString();
                    if (status == "2")
                    {
                        status = "-";
                    }
                    else
                    {
                        status = "";
                    }

                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["acctsum_mny"]) != 0)
                    {
                        TextValueArr[0].Add("t_acctsum.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["acctsum_mny"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_acctsum.text = ''");
                    }


                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["sbsum_mny"]) != 0)
                    {
                        TextValueArr[0].Add("t_sbsum.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["sbsum_mny"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_sbsum.text = ''");
                    }

                    TextValueArr[0].Add("t_gatheremp.text = '" + dtInvoiceMain.Rows[0]["empno_chr"].ToString() + "'");


                    string catid = "";
                    DataTable dt;
                    if (isOpen == 0)
                        l = objCharge.m_lngGetDefChargeCat("4", "1", out dt);
                    else
                        l = objCharge.m_lngGetDefChargeCat("5", "1", out dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        catid = dt.Rows[i]["catid_chr"].ToString();

                        //�������ͣ� 0 ��ͨ�� 1 ������
                        string type = dt.Rows[i]["type_int"].ToString();

                        if (type == "0")
                        {
                            if (!has.ContainsKey(catid))
                            {
                                TextValueArr[0].Add(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                                continue;
                            }
                            else
                            {
                                TextValueArr[0].Add(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = '" + status + clsPublic.ConvertObjToDecimal(has[catid]).ToString("0.00") + "'");
                            }
                        }
                        else if (type == "1")
                        {
                            string compexp = dt.Rows[i]["compexp_vchr"].ToString().Trim().Replace("��", "+");
                            decimal total = 0;

                            System.Collections.Generic.List<string> catidarr = clsPublic.m_ArrGettoken(compexp, "+");
                            for (int j = 0; j < catidarr.Count; j++)
                            {
                                catid = catidarr[j].ToString().Trim();
                                if (has.ContainsKey(catid))
                                {
                                    total += clsPublic.ConvertObjToDecimal(has[catid]);
                                }
                            }

                            if (total > 0)
                            {
                                TextValueArr[0].Add(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = '" + status + total.ToString("0.00") + "'");
                            }
                            else
                            {
                                TextValueArr[0].Add(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                            }
                        }
                    }

                    //Ԥ���������Ϣ

                    //���տ�
                    decimal decPremny = 0;
                    //����
                    decimal decRecmny = 0;
                    //�˿�
                    decimal decRefmny = 0;
                    //Ƿ��
                    decimal decNotmny = 0;

                    //����֧��
                    decimal decPersonMny = clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["sbsum_mny"]);

                    if (dtPrepay.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPrepay.Rows.Count; i++)
                        {
                            decPremny += clsPublic.ConvertObjToDecimal(dtPrepay.Rows[i]["money_dec"]);
                        }

                        //ʹ��Ԥ�������ʱ��¼��֧�����
                        decimal decPaymny = clsPublic.ConvertObjToDecimal(dtPrepay.Rows[0]["paysum_mny"]);

                        if (decPaymny >= decPersonMny)
                        {
                            decRefmny = decPremny - decPersonMny;
                        }
                        else if (decPaymny < decPersonMny)
                        {
                            decRecmny = decPersonMny - decPremny;
                        }
                    }
                    else
                    {
                        decRecmny = decPersonMny;
                    }

                    if (decPremny != 0)
                    {
                        TextValueArr[0].Add("t_prepaysum.text = '" + decPremny.ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_prepaysum.text = ''");
                    }

                    if (decRecmny != 0)
                    {
                        string PayModeName = "";
                        if (dtPayMode.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPayMode.Rows.Count; i++)
                            {
                                //֧����ʽ 0 Ԥ���� 1 �ֽ� 2 ֧Ʊ 3 ���п� 4 ���� 5 ΢��2; 6 ֧����
                                switch (dtPayMode.Rows[i]["paytype_int"].ToString())
                                {
                                    case "1":
                                        PayModeName += "��";
                                        break;
                                    case "2":
                                        PayModeName += "֧";
                                        break;
                                    case "3":
                                        PayModeName += "��";
                                        break;
                                    case "4":
                                        PayModeName += "����";
                                        break;
                                    case "5":
                                        PayModeName += "΢";
                                        break;
                                    case "6":
                                        PayModeName += "��";
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        if (PayModeName != "")
                        {
                            TextValueArr[0].Add("t_rerecsum.text = '" + decRecmny.ToString("0.00") + "(" + PayModeName + ")'");
                        }
                        else
                        {
                            TextValueArr[0].Add("t_rerecsum.text = '" + decRecmny.ToString("0.00") + "'");
                        }
                    }
                    else
                    {
                        TextValueArr[0].Add("t_rerecsum.text = ''");
                    }

                    if (decRefmny != 0)
                    {
                        TextValueArr[0].Add("t_refundsum.text = '" + decRefmny.ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_refundsum.text = ''");
                    }

                    //��Ʊ�š��ش�Ʊ��
                    if (RepNo == "")
                    {
                        TextValueArr[0].Add("t_fph.text = '" + dtInvoiceMain.Rows[0]["invoiceno_vchr"].ToString() + "'");
                        TextValueArr[0].Add("t_repeatinvono.text = ''");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_fph.text = '" + RepNo + "'");
                        TextValueArr[0].Add("t_repeatinvono.text = '*REPEAT(" + dtInvoiceMain.Rows[0]["invoiceno_vchr"].ToString() + ")*'");
                    }
                    //����֧��
                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["qtzhifu"]) != 0)
                    {
                        TextValueArr[0].Add("t_qtzf.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["qtzhifu"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_qtzf.text = ''");
                    }
                    //����֧��1
                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf1"]) != 0)
                    {
                        TextValueArr[0].Add("t_bc1zf.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf1"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_bc1zf.text = ''");
                    }
                    //����֧��2
                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf2"]) != 0)
                    {
                        TextValueArr[0].Add("t_bc2zf.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf2"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_bc2zf.text = ''");
                    }
                    //����֧��3
                    if (clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf3"]) != 0)
                    {
                        TextValueArr[0].Add("t_bc3zf.text = '" + status + clsPublic.ConvertObjToDecimal(dtInvoiceMain.Rows[0]["bcyltczf3"]).ToString("0.00") + "'");
                    }
                    else
                    {
                        TextValueArr[0].Add("t_bc3zf.text = ''");
                    }

                }
                #endregion
            }
        }
        /// <summary>
        /// ��շ�Ʊ����
        /// </summary>
        /// <param name="dwc"></param>
        /// <param name="Scope">1 ���� 2 סԺ</param>
        public static void m_mthClearInvoiceBill(DataWindowControl dwc, int Scope)
        {
            if (Scope == 1)
            {
                #region ����
                dwc.Modify("t_zlkh.text = ''");
                dwc.Modify("t_year.text = ''");
                dwc.Modify("t_month.text = ''");
                dwc.Modify("t_day.text = ''");
                dwc.Modify("t_xlh.text = ''");
                dwc.Modify("t_fb.text = ''");
                dwc.Modify("t_name.text = ''");
                dwc.Modify("t_paytype.text = ''");
                dwc.Modify("t_w.text = ''");
                dwc.Modify("t_q.text = ''");
                dwc.Modify("t_b.text = ''");
                dwc.Modify("t_s.text = ''");
                dwc.Modify("t_y.text = ''");
                dwc.Modify("t_j.text = ''");
                dwc.Modify("t_f.text = ''");

                dwc.Modify("t_total.text = ''");
                dwc.Modify("t_acctsum.text = ''");
                dwc.Modify("t_sbsum.text = ''");
                dwc.Modify("t_gatheremp.text = ''");
                dwc.Modify("t_doctor.text = ''");
                dwc.Modify("t_confirmemp.text = ''");

                DataTable dt;
                clsDcl_Charge objCharge = new clsDcl_Charge();
                objCharge.m_lngGetDefChargeCat("2", "1", out dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        dwc.Modify(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                    }
                    catch { }
                }

                dwc.Modify("t_fph.text = ''");
                dwc.Modify("t_repeatinvono.text = ''");
                dwc.Modify("t_hospital.text = ''");

                #endregion
            }
            else if (Scope == 2)
            {
                #region סԺ
                dwc.Modify("t_zyh.text = ''");
                dwc.Modify("t_area.text = ''");
                dwc.Modify("t_year.text = ''");
                dwc.Modify("t_month.text = ''");
                dwc.Modify("t_day.text = ''");
                dwc.Modify("t_name.text = ''");
                dwc.Modify("t_indate.text = ''");
                dwc.Modify("t_paytype.text = ''");

                dwc.Modify("t_sw.text = ''");
                dwc.Modify("t_w.text = ''");
                dwc.Modify("t_q.text = ''");
                dwc.Modify("t_b.text = ''");
                dwc.Modify("t_s.text = ''");
                dwc.Modify("t_y.text = ''");
                dwc.Modify("t_j.text = ''");
                dwc.Modify("t_f.text = ''");
                dwc.Modify("t_total.text = ''");

                dwc.Modify("t_acctsum.text = ''");
                dwc.Modify("t_sbsum.text = ''");
                dwc.Modify("t_gatheremp.text = ''");

                DataTable dt;
                clsDcl_Charge objCharge = new clsDcl_Charge();
                objCharge.m_lngGetDefChargeCat("4", "1", out dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        dwc.Modify(dt.Rows[i]["prtclt_vchr"].ToString().Trim() + ".text = ''");
                    }
                    catch { }
                }

                dwc.Modify("t_prepaysum.text = ''");
                dwc.Modify("t_rerecsum.text = ''");
                dwc.Modify("t_refundsum.text = ''");

                dwc.Modify("t_fph.text = ''");
                dwc.Modify("t_repeatinvono.text = ''");
                #endregion
            }
        }
        #endregion
    }
}
