using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    class clsCtlRptOpDoctorPerformance : com.digitalwave.GUI_Base.clsController_Base
    {
        #region 变量
        DataTable dtFeeData = new DataTable();
        public Transaction m_objTransation;
        #endregion

        #region 设置窗体对象
        com.digitalwave.iCare.gui.HIS.Reports.frmRptOpDoctorPerformance m_objViewer;
        /// <summary>
        /// 设置窗体对象
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmRptOpDoctorPerformance)frmMDI_Child_Base_in;

        }
        #endregion

        #region GetRptDoctorPerformance
        /// <summary>
        /// GetRptDoctorPerformance
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="m_strStatType"></param>
        internal void GetRptDoctorPerformance(string beginDate, string endDate, string m_strStatType)
        {
            DataTable dtResult = new DataTable();
            string p_strDoctorID = this.m_objViewer.m_strDoctorID;
            List<string> DeptIDArr = this.m_objViewer.DeptIDArr;
            int intFlag = 0;//用来标识是按医生查还是按科室查，0医生，1科室
            if (this.m_objViewer.rdoDoctor.Checked == true)
            {
                intFlag = 0;
            }
            if (this.m_objViewer.rdoDept.Checked == true)
            {
                intFlag = 1;
            }

          (new weCare.Proxy.ProxyReport()).Service.m_lngGetRptDoctorPerformance(beginDate, endDate, m_strStatType, p_strDoctorID, DeptIDArr, intFlag, out dtResult);
            this.m_objViewer.m_dwShow.Modify("t_opername.text = '" + this.m_objViewer.LoginInfo.m_strEmpName + "'");
            this.m_objViewer.m_dwShow.Modify("t_date.text = '" + this.m_objViewer.dteRq1.Value.ToShortDateString() + " - " + this.m_objViewer.dteRq2.Value.ToShortDateString() + "'");

            this.m_objViewer.m_dwShow.PrintProperties.Preview = false;
            this.m_objViewer.m_dwShow.SetRedrawOff();
            this.m_objViewer.m_dwShow.Reset();
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                int rowIndex = 0;
                foreach (DataRow dr in dtResult.Rows)
                {
                    rowIndex = this.m_objViewer.m_dwShow.InsertRow();
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "typeid_chr", dr["typeid_chr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "typename_vchr", dr["typename_vchr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "empno_chr", dr["empno_chr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "lastname_vchr", dr["lastname_vchr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "code_vchr", dr["code_vchr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "deptname_vchr", dr["deptname_vchr"].ToString());
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "zfs", clsPublic.ConvertObjToDecimal(dr["zfs"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "cfs", clsPublic.ConvertObjToDecimal(dr["cfs"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "tolfee_mny", clsPublic.ConvertObjToDecimal(dr["tolfee_mny"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "jxywl", clsPublic.ConvertObjToDecimal(dr["jxywl"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "kjybl", dr["kjybl"].ToString());
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "tolprice_mny", clsPublic.ConvertObjToDecimal(dr["tolprice_mny"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "xytolprice_mny", clsPublic.ConvertObjToDecimal(dr["xytolprice_mny"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "kjysybl", dr["kjysybl"].ToString());
                    this.m_objViewer.m_dwShow.SetItemString(rowIndex, "jbybl", dr["jbybl"].ToString());
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "kjrs", clsPublic.ConvertObjToDecimal(dr["kjrs"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "tolprice_jby", clsPublic.ConvertObjToDecimal(dr["tolprice_jby"].ToString()));
                    this.m_objViewer.m_dwShow.SetItemDecimal(rowIndex, "xytolprice_jby", clsPublic.ConvertObjToDecimal(dr["xytolprice_jby"].ToString()));
                }
            }
            //this.m_objViewer.m_dwShow.Retrieve(dtResult);              
            //this.m_objViewer.m_dwShow.SetSort("deptname_vchr asc, lastname_vchr asc");
            this.m_objViewer.m_dwShow.Sort();
            this.m_objViewer.m_dwShow.SetRedrawOn();    // 一定要把重绘放到 CalculateGroups 之前，否则分组不起作用
            // this.m_objViewer.m_dwShow.Sort();
            this.m_objViewer.m_dwShow.CalculateGroups();
            //this.m_objViewer.m_dwShow.SetRedrawOn();
            this.m_objViewer.m_dwShow.Refresh();
            decimal dclwestmedicinetotal = 0;
            decimal dclkjytotal = 0;
            decimal dclkjybl = 0;
            decimal zrs = 0;
            decimal kjyrs = 0;
            decimal xytolprice_jby = 0;
            decimal tolprice_jby = 0;
            if (dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    dclwestmedicinetotal += clsPublic.ConvertObjToDecimal(dr["xytolprice_mny"]);
                    dclkjytotal += clsPublic.ConvertObjToDecimal(dr["tolprice_mny"]);
                    zrs += clsPublic.ConvertObjToDecimal(dr["zfs"]);
                    kjyrs += clsPublic.ConvertObjToDecimal(dr["kjrs"]);
                    xytolprice_jby += clsPublic.ConvertObjToDecimal(dr["xytolprice_jby"]);
                    tolprice_jby += clsPublic.ConvertObjToDecimal(dr["tolprice_jby"]);
                }
                if (dclwestmedicinetotal > 0)
                {
                    dclkjybl = clsPublic.Round((dclkjytotal / dclwestmedicinetotal) * 100, 2);
                    this.m_objViewer.m_dwShow.Modify("t_kjybl.text = '" + dclkjybl + "%" + "'");
                }
                if (zrs > 0)
                {
                    this.m_objViewer.m_dwShow.Modify("t_kjysyl.text = '" + clsPublic.Round((kjyrs / zrs) * 100, 2) + "%" + "'");
                }
                if (xytolprice_jby > 0)
                {
                    this.m_objViewer.m_dwShow.Modify("t_jbybl.text = '" + clsPublic.Round((tolprice_jby / xytolprice_jby) * 100, 2) + "%" + "'");
                }
            }
        }
        #endregion

        #region FillDataWindow
        /// <summary>
        /// FillDataWindow
        /// </summary>
        void FillDataWindow()
        {
            if (this.m_objViewer.selectStatus == true)
            {
                DataView dvTemp = new DataView(dtFeeData);
                dvTemp.RowFilter = "doctorId_chr in (" + this.m_objViewer.doctIDArr + ")";
                if (dvTemp.Count == 0)
                    return;

                this.dtFeeData = dvTemp.ToTable();
            }
            this.m_objViewer.m_dwShow.SetRedrawOff();
            DataView dv = new DataView(this.dtFeeData);
            //dv.Sort = "code_vchr, empno_chr, outpatrecipeid_chr";
            string temp;
            string doctorCode = "";
            string doctorName = "";
            string dptCode;
            string dptName = "";
            string feeName;
            string percentage;
            decimal conmentRcpCount = 0;
            decimal recipeCount = 0;
            decimal recipeMoney = 0;
            decimal medMoney = 0;
            decimal otherMoney = 0;
            decimal medPercent = 0;
            decimal perfermance = 0;
            decimal totalMoney = 0;
            decimal tempMoney;

            doctorCode = dv[0]["empno_chr"].ToString();
            string recipeId = dv[0]["outpatrecipeid_chr"].ToString();
            string recipeIdTemp;
            ////if(dv[0]["type_int"].ToString()=="0")
            ////{
            ////    conmentRcpCount = 1;
            ////}
            string m_strRecordDate = dv[0]["recorddate_dat"].ToString().Trim();
            string m_strRecordDateTemp;
            string m_strStatus = dv[0]["status_int"].ToString();
            string m_strStatusTemp;
            int row;
            for (int i = 0; i < dv.Count; i++)
            {
                temp = dv[i]["empno_chr"].ToString();
                if (temp.Equals(doctorCode))
                {
                    doctorName = dv[i]["lastname_vchr"].ToString();
                    dptName = dv[i]["deptname_vchr"].ToString();
                    dptCode = dv[i]["code_vchr"].ToString();
                    m_strStatusTemp = dv[i]["status_int"].ToString();
                    recipeIdTemp = dv[i]["outpatrecipeid_chr"].ToString();
                    m_strRecordDateTemp = dv[i]["recorddate_dat"].ToString().Trim();
                    if (i == 0 || !recipeId.Equals(recipeIdTemp))
                    {
                        if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                        {
                            recipeCount--;
                        }
                        else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                        {
                            recipeCount++;
                        }
                        if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                        {

                            if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                            {
                                conmentRcpCount--;
                            }
                            else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                            {
                                conmentRcpCount++;
                            }
                        }
                        recipeId = recipeIdTemp;
                        m_strStatus = m_strStatusTemp;
                        m_strRecordDate = m_strRecordDateTemp;
                    }
                    else
                    {
                        if (m_strStatus != m_strStatusTemp)
                        {
                            if (m_strStatusTemp == "2" || m_strStatusTemp == "0")
                            {
                                m_strStatus = m_strStatusTemp;
                                m_strRecordDate = m_strRecordDateTemp;
                                recipeCount--;
                                if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                                {

                                    if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                                    {
                                        conmentRcpCount--;
                                    }
                                    else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                                    {
                                        conmentRcpCount++;
                                    }
                                }

                            }
                            else if (m_strStatusTemp == "1" || m_strStatusTemp == "3")
                            {
                                m_strStatus = m_strStatusTemp;
                                m_strRecordDate = m_strRecordDateTemp;
                                recipeCount++;
                                if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                                {

                                    if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                                    {
                                        conmentRcpCount--;
                                    }
                                    else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                                    {
                                        conmentRcpCount++;
                                    }
                                }
                            }
                        }
                        else
                        {

                            if (m_strStatus == "2" && m_strRecordDate != m_strRecordDateTemp)
                            {
                                m_strRecordDate = m_strRecordDateTemp;
                                recipeCount--;
                                if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                                {

                                    if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                                    {
                                        conmentRcpCount--;
                                    }
                                    else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                                    {
                                        conmentRcpCount++;
                                    }
                                }
                            }
                            else if (m_strStatus == "3" && m_strRecordDate != m_strRecordDateTemp)
                            {
                                m_strRecordDate = m_strRecordDateTemp;
                                recipeCount++;
                                if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                                {

                                    if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                                    {
                                        conmentRcpCount--;
                                    }
                                    else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                                    {
                                        conmentRcpCount++;
                                    }
                                }
                            }

                        }
                    }
                    tempMoney = Convert.ToDecimal(dv[i]["tolfee_mny"].ToString());
                    recipeMoney += tempMoney;
                    totalMoney += tempMoney;
                    percentage = dv[i]["percentage"].ToString();
                    if (percentage == null || percentage == "")
                    {
                        percentage = "0";
                    }
                    perfermance += tempMoney * Convert.ToDecimal(percentage);

                    feeName = dv[i]["typename_vchr"].ToString();
                    if (feeName.Contains("西药") || feeName.Contains("中草") || feeName.Contains("中成"))
                    {
                        medMoney += tempMoney;
                    }
                    else
                    {
                        otherMoney += tempMoney;
                    }

                    if (i == dv.Count - 1)
                    {
                        row = this.m_objViewer.m_dwShow.InsertRow(0);
                        this.m_objViewer.m_dwShow.SetItemString(row, "gh", doctorCode);
                        this.m_objViewer.m_dwShow.SetItemString(row, "xm", doctorName);
                        this.m_objViewer.m_dwShow.SetItemString(row, "ks", dptName);

                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ptrc", conmentRcpCount);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfs", recipeCount + 1);
                        if (recipeCount == 0)
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", 0);
                        }
                        else
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", decimal.Round(totalMoney / recipeCount, 2));
                        }
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypje", medMoney);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ylsr", otherMoney);
                        if (totalMoney != 0)
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypbl", decimal.Round(medMoney / totalMoney, 4));
                        }
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "jxywl", perfermance);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "hjje", totalMoney);
                    }
                }
                else
                {
                    row = this.m_objViewer.m_dwShow.InsertRow(0);
                    this.m_objViewer.m_dwShow.SetItemString(row, "gh", doctorCode);
                    this.m_objViewer.m_dwShow.SetItemString(row, "xm", doctorName);
                    this.m_objViewer.m_dwShow.SetItemString(row, "ks", dptName);

                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "ptrc", conmentRcpCount);
                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfs", recipeCount);
                    if (recipeCount == 0)
                    {
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", 0);
                    }
                    else
                    {
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", decimal.Round(totalMoney / recipeCount, 2));
                    }
                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypje", medMoney);
                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "ylsr", otherMoney);

                    if (totalMoney != 0)
                    {
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypbl", decimal.Round(medMoney / totalMoney, 4));
                    }
                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "jxywl", perfermance);
                    this.m_objViewer.m_dwShow.SetItemDecimal(row, "hjje", totalMoney);

                    doctorCode = temp;
                    recipeId = dv[i]["outpatrecipeid_chr"].ToString();
                    doctorName = dv[i]["lastname_vchr"].ToString();
                    dptName = dv[i]["deptname_vchr"].ToString();
                    dptCode = dv[i]["code_vchr"].ToString();
                    //if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                    //{
                    //    conmentRcpCount = 1;

                    //}
                    //else
                    //{
                    //    conmentRcpCount = 0;
                    //}
                    if (dv[i]["RECIPEFLAG_INT"].ToString() == "1")
                    {

                        if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                        {
                            conmentRcpCount = -1;
                        }
                        else if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                        {
                            conmentRcpCount = 1;
                        }
                    }
                    else
                    {
                        conmentRcpCount = 0;
                    }
                    if (dv[i]["status_int"].ToString() == "1" || dv[i]["status_int"].ToString() == "3")
                    {
                        recipeCount = 1;
                    }
                    else if (dv[i]["status_int"].ToString() == "2" || dv[i]["status_int"].ToString() == "0")
                    {
                        recipeCount = -1;
                    }

                    tempMoney = Convert.ToDecimal(dv[i]["tolfee_mny"].ToString());
                    recipeMoney = tempMoney;
                    totalMoney = tempMoney;
                    percentage = dv[i]["percentage"].ToString();
                    if (percentage == null || percentage == "")
                    {
                        percentage = "0";
                    }
                    perfermance = tempMoney * Convert.ToDecimal(percentage);

                    feeName = dv[i]["typename_vchr"].ToString();
                    if (feeName.Contains("西药") || feeName.Contains("中草") || feeName.Contains("中成"))
                    {
                        medMoney = tempMoney;
                        otherMoney = 0;
                    }
                    else
                    {
                        medMoney = 0;
                        otherMoney = tempMoney;
                    }
                    if (i == dv.Count - 1)
                    {
                        row = this.m_objViewer.m_dwShow.InsertRow(0);
                        this.m_objViewer.m_dwShow.SetItemString(row, "gh", doctorCode);
                        this.m_objViewer.m_dwShow.SetItemString(row, "xm", doctorName);
                        this.m_objViewer.m_dwShow.SetItemString(row, "ks", dptName);

                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ptrc", conmentRcpCount);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfs", recipeCount + 1);
                        if (recipeCount == 0)
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", 0);
                        }
                        else
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "cfje", decimal.Round(totalMoney / recipeCount, 2));
                        }
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypje", medMoney);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "ylsr", otherMoney);
                        if (totalMoney != 0)
                        {
                            this.m_objViewer.m_dwShow.SetItemDecimal(row, "ypbl", decimal.Round(medMoney / totalMoney, 4));
                        }
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "jxywl", perfermance);
                        this.m_objViewer.m_dwShow.SetItemDecimal(row, "hjje", totalMoney);
                    }
                }
            }
            this.m_objViewer.m_dwShow.Modify("t_opername.text = '" + this.m_objViewer.LoginInfo.m_strEmpName + "'");
            this.m_objViewer.m_dwShow.Modify("t_date.text = '" + this.m_objViewer.dteRq1.Value.ToShortDateString() + " - " + this.m_objViewer.dteRq2.Value.ToShortDateString() + "'");

            this.m_objViewer.m_dwShow.SetRedrawOn();
            this.m_objViewer.Refresh();
        }
        #endregion

    }
}
