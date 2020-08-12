using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmMedFinanceReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmMedFinanceReport()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        Font objFontTitle = new Font("����_GB2312", 14, System.Drawing.FontStyle.Bold);
        /// <summary>
        /// ��������
        /// </summary>
        Font objFontNormal = new Font("SimSun", 10);
        /// <summary>
        /// �Ӵ�����
        /// </summary>
        Font objFontBold = new Font("SimSun", 11, System.Drawing.FontStyle.Bold);
        /// <summary>
        /// ��ϸ����
        /// </summary>
        Font objFont = new Font("SimSun", 9);
        /// <summary>
        /// ��߾�
        /// </summary>
        const float fltLeftIndentProp = 20f;
        /// <summary>
        /// �ұ߾�
        /// </summary>
        const float fltRightIndentProp = 20f;
        /// <summary>
        /// ���±߾�
        /// </summary>
        const float fltStarHight = 20f;
        /// <summary>
        /// �м��
        /// </summary>
        const float fltRowHeight = 25F;
        /// <summary>
        /// ������
        /// </summary>
        private float Y = fltStarHight;
        /// <summary>
        /// ������
        /// </summary>
        private double X = fltLeftIndentProp;
        /// <summary>
        /// ��ӡ�Ŀ��
        /// </summary>
        private int PageWith;
        /// <summary>
        /// ��ӡ�ĳ���
        /// </summary>
        private int PageHigh;
        /// <summary>
        /// ����
        /// </summary>
        Pen PenLine = new Pen(Brushes.Black, 1);
        int currRow = 0;
        int currPage = 1;
        DataTable dt = new DataTable();
        float tempH = 3;
        #endregion

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            PageWith = e.PageBounds.Width;
            PageHigh = e.PageBounds.Height;
            m_mthPrintTitl(e);
            m_mthPrintBody(e);
 
        }

        #region ��ӡ��ͷ
        private void m_mthPrintTitl(System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            string titleName = "������������������������ҩƷ��������ϸ��";
            SizeF fontWith = e.Graphics.MeasureString(titleName, objFontTitle);
            X = (PageWith - fltLeftIndentProp - fltRightIndentProp - fontWith.Width) / 2;
            e.Graphics.DrawString(titleName, objFontTitle, Brushes.Black, (float)X, Y);
            X = fltLeftIndentProp;
            Y += fltRowHeight;
            e.Graphics.DrawLine(PenLine, (float)X, Y, PageWith - fltRightIndentProp, Y);
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
            X = PageWith * 0.1;
            e.Graphics.DrawString("ʱ  ��", objFontNormal, Brushes.Black, (float)X, Y + tempH);
            X = PageWith * 0.2;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
            X = PageWith * 0.25;
            e.Graphics.DrawString("�� " + datestart.Value.ToString("yyyy-MM-dd") + " �� " + dateEnd.Value.ToString("yyyy-MM-dd"), objFontNormal, Brushes.Black, (float)X, Y + tempH);
            e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
            Y += fltRowHeight;
            X = fltLeftIndentProp;
            e.Graphics.DrawLine(PenLine, (float)X, Y, PageWith - fltRightIndentProp, Y);
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight*2);
            X = PageWith * 0.03;
            e.Graphics.DrawString("ҩ Ʒ �� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight / 2 + tempH);
            X = PageWith * 0.15;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.155;
            e.Graphics.DrawString("ҩ Ʒ �� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight / 2 + tempH);
            X = PageWith * 0.25;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.255;
            e.Graphics.DrawString("��λ", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight / 2 + tempH);
            X = PageWith * 0.3;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.31;
            e.Graphics.DrawString("ϵͳ����", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight / 2 + tempH);
            X = PageWith * 0.38;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2 + tempH);

            X = PageWith * 0.381;
            e.Graphics.DrawString("�Ƿ�", objFontNormal, Brushes.Black, (float)X, Y + tempH * 3);
            e.Graphics.DrawString("�б�", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH * 2 + tempH);
            X = PageWith * 0.43;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);


            X = PageWith * 0.431;
            e.Graphics.DrawString("     �ա�����", objFontNormal, Brushes.Black, (float)X, Y + tempH);
            X = PageWith * 0.63;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);

            X = PageWith * 0.631;
            e.Graphics.DrawString("     ��������", objFontNormal, Brushes.Black, (float)X, Y + tempH);
            X = PageWith * 0.84;
            e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight * 2);


            X = PageWith * 0.841;
            e.Graphics.DrawString("   �ᡡ����", objFontNormal, Brushes.Black, (float)X, Y + tempH);

            X = PageWith * 0.43;

            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
            #region ����
            X = PageWith * 0.431;
            e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            X = PageWith * 0.490;
            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, (float)X, Y + fltRowHeight * 2);

            X = PageWith * 0.491;
            e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            X = PageWith * 0.55;
            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.551;
            e.Graphics.DrawString("��  ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            #endregion


            #region ����
            X = PageWith * 0.631;
            e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            X = PageWith * 0.690;
            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, (float)X, Y + fltRowHeight * 2);

            X = PageWith * 0.691;
            e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            X = PageWith * 0.75;
            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.751;
            e.Graphics.DrawString("��  ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            #endregion


            #region ���
            X = PageWith * 0.841;
            e.Graphics.DrawString("�� ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            X = PageWith * 0.90;
            e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, (float)X, Y + fltRowHeight * 2);
            X = PageWith * 0.901;
            e.Graphics.DrawString("��  ��", objFontNormal, Brushes.Black, (float)X, Y + fltRowHeight + tempH);
            #endregion
            e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight * 2, PageWith - fltRightIndentProp, Y + fltRowHeight * 2);
            e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight * 2);
            Y += fltRowHeight;
            e.Graphics.DrawString("�� " + currPage.ToString() + " ҳ", objFontNormal, Brushes.Black, PageWith - fltRightIndentProp - 50, PageHigh - fltRowHeight*2);
        }
        #endregion
        #region ��ӡ����
        private void m_mthPrintBody(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i1 = currRow; i1 < dt.Rows.Count; i1++)
                {
                    Y += fltRowHeight;
                    X = fltLeftIndentProp;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    e.Graphics.DrawLine(PenLine, (float)X, Y, PageWith - fltRightIndentProp, Y);
                    X = PageWith * 0.03;
                    if (e.Graphics.MeasureString(dt.Rows[i1]["medicinename_vchr"].ToString(), objFontNormal).Width > PageWith * 0.12)
                    {
                        e.Graphics.DrawString(dt.Rows[i1]["medicinename_vchr"].ToString().Substring(0, 7), objFont, Brushes.Black, (float)X, Y);
                        e.Graphics.DrawString(dt.Rows[i1]["medicinename_vchr"].ToString().Substring(7), objFont, Brushes.Black, (float)X, Y + tempH+10);
                    }
                    else
                    {
                        e.Graphics.DrawString(dt.Rows[i1]["medicinename_vchr"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    }
                    X = PageWith * 0.15;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    X = PageWith * 0.155;
                    if (e.Graphics.MeasureString(dt.Rows[i1]["medspec_vchr"].ToString(), objFontNormal).Width > PageWith * 0.1)
                    {
                        e.Graphics.DrawString(dt.Rows[i1]["medspec_vchr"].ToString().Substring(0, 7), objFont, Brushes.Black, (float)X, Y);
                        e.Graphics.DrawString(dt.Rows[i1]["medspec_vchr"].ToString().Substring(7), objFont, Brushes.Black, (float)X, Y + tempH + 10);
                    }
                    else
                    {
                        e.Graphics.DrawString(dt.Rows[i1]["medspec_vchr"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    }
                    X = PageWith * 0.25;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight );
                    X = PageWith * 0.255;
                    e.Graphics.DrawString(dt.Rows[i1]["OPUNIT_CHR"].ToString(), objFontNormal, Brushes.Black, (float)X, Y +  tempH);
                    X = PageWith * 0.3;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    X = PageWith * 0.31;
                    e.Graphics.DrawString(dt.Rows[i1]["syslotno_chr"].ToString(), objFontNormal, Brushes.Black, (float)X, Y +tempH);
                    X = PageWith * 0.38;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    X = PageWith * 0.381;
                    e.Graphics.DrawString(dt.Rows[i1]["strAIMUNIT"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH );
                    X = PageWith * 0.43;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    #region ����
                    X = PageWith * 0.431;
                    e.Graphics.DrawString(dt.Rows[i1]["buyqty"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.490;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);

                    X = PageWith * 0.491;
                    e.Graphics.DrawString(dt.Rows[i1]["buyunitprice_mny"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.55;
                    e.Graphics.DrawLine(PenLine, (float)X, Y , (float)X, Y + fltRowHeight);
                    X = PageWith * 0.551;
                    e.Graphics.DrawString(dt.Rows[i1]["buymoney"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.63;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    #endregion


                    #region ����
                    X = PageWith * 0.631;
                    e.Graphics.DrawString(dt.Rows[i1]["saleqty"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.690;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);

                    X = PageWith * 0.691;
                    e.Graphics.DrawString(dt.Rows[i1]["saleunitprice_mny"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.75;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    X = PageWith * 0.751;
                    e.Graphics.DrawString(dt.Rows[i1]["salemoney"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.84;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    #endregion


                    #region ���
                    X = PageWith * 0.841;
                    e.Graphics.DrawString(dt.Rows[i1]["banlqty"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    X = PageWith * 0.90;
                    e.Graphics.DrawLine(PenLine, (float)X, Y, (float)X, Y + fltRowHeight);
                    X = PageWith * 0.901;
                    e.Graphics.DrawString(dt.Rows[i1]["banlmoney"].ToString(), objFontNormal, Brushes.Black, (float)X, Y + tempH);
                    #endregion
                    e.Graphics.DrawLine(PenLine, PageWith - fltRightIndentProp, Y, PageWith - fltRightIndentProp, Y + fltRowHeight);
                    e.Graphics.DrawLine(PenLine, fltLeftIndentProp, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
                    if (Y > PageHigh - 100)
                    {
                        currRow = i1;
                        X = fltLeftIndentProp;
                        e.Graphics.DrawLine(PenLine, (float)X, Y + fltRowHeight, PageWith - fltRightIndentProp, Y + fltRowHeight);
                        Y = fltStarHight;
                        currPage++;
                        e.HasMorePages = true;
                        return;
                    }
                }
            }
        }

        #endregion
        private void frmMedFinanceReport_Load(object sender, EventArgs e)
        {
            string moth = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
            datestart.Value = DateTime.Parse(moth);
            ctlprintShow1.setDocument = printDocument1;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                dt.Columns.Add("medicinename_vchr");
                dt.Columns.Add("syslotno_chr");
                dt.Columns.Add("medspec_vchr");
                dt.Columns.Add("straimunit");
                dt.Columns.Add("OPUNIT_CHR");
                dt.Columns.Add("buyunitprice_mny");
                dt.Columns.Add("buyqty");
                dt.Columns.Add("buymoney");
                dt.Columns.Add("saleunitprice_mny");
                dt.Columns.Add("saleqty");
                dt.Columns.Add("salemoney");
                dt.Columns.Add("banlqty");
                dt.Columns.Add("banlmoney");
            }
            catch
            {
            }
            clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
            DataTable dtDeIn,dtDeOut;
            domain.m_lngGetReportData(datestart.Value.ToShortDateString(), dateEnd.Value.ToShortDateString(), out dtDeIn, out dtDeOut);
            if (dtDeIn != null && dtDeIn.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtDeIn.Rows.Count; i1++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["medicinename_vchr"] = dtDeIn.Rows[i1]["medicinename_vchr"];
                    newRow["syslotno_chr"] = dtDeIn.Rows[i1]["syslotno_chr"];
                    newRow["medspec_vchr"] = dtDeIn.Rows[i1]["medspec_vchr"];
                    newRow["buyunitprice_mny"] = dtDeIn.Rows[i1]["buyunitprice_mny"];
                    newRow["buymoney"] = dtDeIn.Rows[i1]["buymoney"];
                    newRow["buyqty"] = dtDeIn.Rows[i1]["buyqty"];
                    newRow["OPUNIT_CHR"] = dtDeIn.Rows[i1]["OPUNIT_CHR"];
                    if (dtDeIn.Rows[i1]["aimunit_int"] == null || dtDeIn.Rows[i1]["aimunit_int"].ToString() == "0")
                    {
                        newRow["straimunit"] = "��";
                    }
                    else
                    {
                        newRow["straimunit"] = "��";
                    }
                    newRow["buymoney"] = dtDeIn.Rows[i1]["buymoney"];
                    if (dtDeOut != null && dtDeOut.Rows.Count > 0)
                    {
                        #region 

                            bool isChang = false;
                            #region 
                            for (int k2 = 0; k2 < dtDeOut.Rows.Count; k2++)
                            {
                                if (dtDeIn.Rows[i1]["medicinename_vchr"].ToString().Trim() == dtDeOut.Rows[k2]["medicinename_vchr"].ToString().Trim() && dtDeIn.Rows[i1]["syslotno_chr"].ToString().Trim() == dtDeOut.Rows[k2]["syslotno_chr"].ToString().Trim())
                                {
                                    if (isChang == false)
                                    {
                                        newRow["saleunitprice_mny"] = dtDeOut.Rows[k2]["saleunitprice_mny"];
                                        newRow["saleqty"] = dtDeOut.Rows[k2]["saleqty"];
                                        newRow["salemoney"] = dtDeOut.Rows[k2]["salemoney"];
                                        if (newRow["buyqty"] == null || newRow["buyqty"].ToString().Trim() == "")
                                            newRow["buyqty"] = 0;
                                        if (newRow["saleqty"] == null || newRow["saleqty"].ToString().Trim() == "")
                                            newRow["saleqty"] = 0;
                                        newRow["banlqty"] = double.Parse(newRow["buyqty"].ToString()) - double.Parse(newRow["saleqty"].ToString());
                                        if (newRow["buymoney"] == null || newRow["buymoney"].ToString().Trim() == "")
                                            newRow["buymoney"] = 0;
                                        if (newRow["salemoney"] == null || newRow["salemoney"].ToString().Trim() == "")
                                            newRow["salemoney"] = 0;
                                        newRow["banlmoney"] = double.Parse(newRow["buymoney"].ToString()) - double.Parse(newRow["salemoney"].ToString());
                                        dt.Rows.Add(newRow);
                                        isChang = true;
                                    }
                                    else
                                    {
                                        DataRow newRow1 = dt.NewRow();
                                        newRow1["medicinename_vchr"] = dtDeOut.Rows[k2]["medicinename_vchr"];
                                        newRow1["syslotno_chr"] = dtDeOut.Rows[k2]["syslotno_chr"];
                                        newRow1["medspec_vchr"] = dtDeOut.Rows[k2]["medspec_vchr"];
                                        newRow1["saleqty"] = dtDeOut.Rows[k2]["saleqty"];
                                        newRow1["salemoney"] = dtDeOut.Rows[k2]["salemoney"];
                                        newRow1["saleunitprice_mny"] = dtDeOut.Rows[k2]["saleunitprice_mny"];
                                        newRow1["OPUNIT_CHR"] = dtDeOut.Rows[k2]["OPUNIT_CHR"];
                                        if (dtDeOut.Rows[k2]["aimunit_int"] == null || dtDeOut.Rows[k2]["aimunit_int"].ToString() == "0")
                                        {
                                            newRow1["straimunit"] = "��";
                                        }
                                        else
                                        {
                                            newRow1["straimunit"] = "��";
                                        }
                                        newRow1["medspec_vchr"] = dtDeOut.Rows[k2]["medspec_vchr"];
                                        newRow1["banlqty"] = "-" + dtDeOut.Rows[k2]["saleqty"].ToString();
                                        newRow1["banlmoney"] = "-" + dtDeOut.Rows[k2]["salemoney"].ToString();
                                        dt.Rows.Add(newRow1);
                                    }
                                    dtDeOut.Rows[k2].Delete();
                                    dtDeOut.AcceptChanges();
                                    k2--;
                                }
                            }
                            #endregion
                            if (isChang == false)
                            {
                                newRow["banlqty"] = newRow["buyqty"];
                                newRow["banlmoney"] = newRow["buymoney"];
                                dt.Rows.Add(newRow);
                            }

                        #endregion
                    }
                    else
                    {
                        newRow["banlqty"] = newRow["buyqty"];
                        newRow["banlmoney"] = newRow["buymoney"];
                        dt.Rows.Add(newRow);
                    }
                    
                }
                if (dtDeOut.Rows.Count > 0)
                {
                    for (int f3 = 0; f3 < dtDeOut.Rows.Count; f3++)
                    {
                        DataRow newRow1 = dt.NewRow();
                        newRow1["medicinename_vchr"] = dtDeOut.Rows[f3]["medicinename_vchr"];
                        newRow1["syslotno_chr"] = dtDeOut.Rows[f3]["syslotno_chr"];
                        newRow1["medspec_vchr"] = dtDeOut.Rows[f3]["medspec_vchr"];
                        newRow1["saleqty"] = dtDeOut.Rows[f3]["saleqty"];
                        newRow1["salemoney"] = dtDeOut.Rows[f3]["salemoney"];
                        newRow1["saleunitprice_mny"] = dtDeOut.Rows[f3]["saleunitprice_mny"];
                        newRow1["banlqty"] = "-" + dtDeOut.Rows[f3]["saleqty"].ToString();
                        newRow1["banlmoney"] = "-" + dtDeOut.Rows[f3]["salemoney"].ToString();
                        dt.Rows.Add(newRow1);
                    }
                }

            }

        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Y = fltStarHight;
            ctlprintShow1.setDocument = printDocument1;
        }

        private void buttonXP3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            com.digitalwave.iCare.common.frmSelectPrinter selectPrinter = new com.digitalwave.iCare.common.frmSelectPrinter();
            if (selectPrinter.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings.PrinterName = selectPrinter.PrinterName;
            }
            else
            {
                return;
            }
            printDocument1.Print();
        }
    }
}