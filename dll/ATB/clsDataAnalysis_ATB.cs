using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using com.digitalwave.iCare.LIS;
using LIS.SpecialDataInteface;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.LIS
{
   
    /// <summary>
    /// ATB细菌仪
    /// 读取对方数据库
    /// 密码：scsl
    /// </summary>
    public class clsDataAnalysis_ATB : clsLISDataAcquisitionBase_DB
    {
        /*
        string strAutoSQL = @"select a.reqno,
       a.exeno,
       a.germid,
       a.antiid,
       a.test,
       a.antiname,
       a.micexplain,
       a.antidate,
       a.resshow,
       c.samno,
       b.micname,
       b.Express
  from resultmic as a, resultexe as b, antiresultbill c
 where a.reqno = b.reqno
   and a.reqno = c.reqno
   and a.istran <>1
";
         * */
        string strAutoSQL = @"select a.reqno,
       a.exeno,
       a.germid,
       a.antiid,
       a.test,
       a.antiname,
       a.micexplain,
       a.antidate,
       a.resshow,
       c.samno,
       b.micnum,
       b.micname,
       b.Express
  from ((resultmic as a
left join resultexe as b on (a.reqno = b.reqno
                       and b.exeno='1'))
left join antiresultbill as c on(a.reqno=c.reqno))
 where a.exeno='1' and a.istran <>1
";
        string strAutoUpdateSQL = @"update micresult as a
   set a.istran = 1
 where a.reqno = ?
   and a.exeno = ?
   and a.germid = ?
   and a.antiid = ?
   and a.test = ?
   and a.istran = 0";

       

       frmDefaultHandle m_frmSampleID = null;
        public clsDataAnalysis_ATB()
        {
            //m_blnMuiltySample = true;
            this.m_mthSetMuiltySample(true);
            //m_mthSetMuiltySample(true);
            m_frmSampleID = new frmDefaultHandle();
        }

        //clsHRPTableService objHRPSvc = new clsHRPTableService();
        #region 自动读取
        /// <summary>
        /// 自动读取
        /// </summary>
        /// <returns></returns>
        public override long m_lngWorkByAuto()
        {
            DataTable dtResult = null;
            long lngRes = 0;
            Log.Output("begin....");
            //objHRPSvc.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytODBC, clsHRPTableService.enumDatabase.bytYZ_LIS);
            lngRes = m_objService.lngGetDataTableWithoutParameters(strAutoSQL, ref dtResult);
            if (lngRes <= 0)
            {
                return lngRes;
            }
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return lngRes;
            }
            SpecialDataIntefacs tp_SpecialData = null;

            tp_SpecialData = new SpecialDataIntefacs();

            string m_DnsStr = "";
            string m_DnsMoudle = "";
            m_DnsStr = m_objService.M_strONLINE_DNS_VCHR;
            m_DnsMoudle = m_objService.M_strONLINE_MODULE_CHR;
            tp_SpecialData.ConnetDNS(m_DnsStr, m_DnsMoudle);

            try
            {
                tp_SpecialData.m_InsertLisDB(0);
                tp_SpecialData.m_InsertLisDB(1);
                tp_SpecialData.m_InsertLisDB(2);
                
            }
            catch (Exception ex)
            {
                string srr = ex.Message;
            }
           
            List<clsLIS_Device_Test_ResultVO> m_lstResult = new List<clsLIS_Device_Test_ResultVO>();
            clsLIS_Device_Test_ResultVO objResultTemp = null;
            List<clsUpdateTransVO> m_lstUpdate = new List<clsUpdateTransVO>();
            List<clsLIS_Device_Test_ResultVO> m_lstBacterialResult = new List<clsLIS_Device_Test_ResultVO>();
            clsLIS_Device_Test_ResultVO objBacterial = null;
            clsUpdateTransVO objUpdate = null;
            DataRow drTemp = null;
            string strCheckDate = null;
            string strResult = null;
            long lngSampleID = 0;
            bool blnSure = false;

            Log.Output("Data comming...." + dtResult.Rows.Count.ToString());
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                blnSure = false;
                drTemp = dtResult.Rows[i];
                objUpdate = new clsUpdateTransVO();
                objResultTemp = new clsLIS_Device_Test_ResultVO();
                objResultTemp.strDevice_Check_Item_Name = drTemp["antiid"].ToString().Trim();
                strCheckDate = drTemp["antidate"].ToString().Trim();
                try
                {
                    DateTime dt = Convert.ToDateTime(strCheckDate);
                }
                catch
                {
                    strCheckDate=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                objResultTemp.strCheck_Date = strCheckDate;
                objResultTemp.strDevice_Sample_ID = drTemp["samno"].ToString().Trim();
                Log.Output("strDevice_Sample_ID-->" + objResultTemp.strDevice_Sample_ID);

                if (long.TryParse(objResultTemp.strDevice_Sample_ID, out lngSampleID))
                {
                    if (lngSampleID > 1000)
                    {
                        objResultTemp.strDevice_Sample_ID = lngSampleID.ToString();
                    }
                    else
                    {
                        objResultTemp.strDevice_Sample_ID = lngSampleID.ToString("000");
                    }
                }
                strResult = drTemp["micexplain"].ToString().Trim().ToLower();
                if (strResult.Contains("s"))
                {
                    objResultTemp.strResult = "敏感";
                }
                else if (strResult.Contains("i"))
                {
                    objResultTemp.strResult = "中介";
                }
                else if (strResult.Contains("r"))
                {
                    objResultTemp.strResult = "耐药";
                }
                else
                {
                    objResultTemp.strResult = "\\";
                }
                objResultTemp.strResult2 = drTemp["resshow"].ToString().Trim();
                objUpdate.m_strAntiid = drTemp["antiid"].ToString().Trim();
                objUpdate.m_strExeno = drTemp["exeno"].ToString().Trim();
                objUpdate.m_strGermid = drTemp["germid"].ToString().Trim();
                objUpdate.m_lngReqno = drTemp["reqno"] != DBNull.Value ? Convert.ToInt64(drTemp["reqno"].ToString().Trim()) : 0;
                objUpdate.m_strTest = drTemp["test"].ToString().Trim();
                objResultTemp.strDevice_Check_Item_Name = this.m_strConvertItem(objResultTemp.strDevice_Check_Item_Name);
                objResultTemp.strDoctorExpress = drTemp["Express"].ToString().Trim();
                objResultTemp.strRefRange = drTemp["ResShow"].ToString().Trim();
                objBacterial = new clsLIS_Device_Test_ResultVO();
                objResultTemp.m_mthCopyTo(objBacterial);
                clsLIS_Device_Test_ResultVO objTemp = new clsLIS_Device_Test_ResultVO();
                objResultTemp.m_mthCopyTo(objTemp);
                for (int j = 0; j < m_lstBacterialResult.Count; j++)
                {
                    if (m_lstBacterialResult[j].strDevice_Sample_ID == objBacterial.strDevice_Sample_ID)
                    {
                        blnSure = true;
                        break;
                    }
                }
                if (!blnSure)
                {
                    if (!string.IsNullOrEmpty(drTemp["micnum"].ToString().Trim()))
                    {
                        objTemp.strDevice_Check_Item_Name = "细菌计数";
                        objTemp.strResult = drTemp["micnum"].ToString().Trim();
                        objTemp.strResult2 = "";
                        objTemp.strRefRange = "";
                        m_lstBacterialResult.Add(objTemp);
                    }

                    objBacterial.strDevice_Check_Item_Name = "结果";
                    objBacterial.strResult = drTemp["micname"].ToString().Trim();
                    objBacterial.strResult2 = "";
                    objBacterial.strRefRange = "";
                    m_lstBacterialResult.Add(objBacterial);
                }
                m_lstResult.Add(objResultTemp);
                m_lstUpdate.Add(objUpdate);
            }
            if (m_lstBacterialResult.Count > 0)
            {
                m_lstResult.AddRange(m_lstBacterialResult.ToArray());
            }
            Log.Output("m_lstResult.Count-->" + m_lstResult.Count.ToString());
            if (m_lstResult.Count > 0)
            {
                int j = 0;
                for (int intTemp = 0; intTemp < m_lstResult.Count; intTemp += 10)
                {
                    List<clsLIS_Device_Test_ResultVO> m_lstTempResult = new List<clsLIS_Device_Test_ResultVO>();
                    for (int i = 0; i < 10; i++)
                    {
                        clsLIS_Device_Test_ResultVO tempLis_result=new clsLIS_Device_Test_ResultVO();
                        tempLis_result = m_lstResult[j];
                        m_lstTempResult.Add(tempLis_result);
                        j++;
                        if (j >= m_lstResult.Count)
                        {
                            break;
                        }
                    }
                    Log.Output("m_lngDataProcess");
                    lngRes = m_lngDataProcess(m_lstTempResult.ToArray());
                }
                Log.Output("m_lngDataProcess--lngRes-->"+ lngRes.ToString());
                if (lngRes > 0)
                {

                    for (int i = 0; i < m_lstUpdate.Count; i++)
                    {
                        /*
                         * and a.exeno = '" + m_lstUpdate[i].m_strExeno + @"'
   and a.antiid = '" + m_lstUpdate[i].m_strAntiid + @"'
                         * */

                        strAutoUpdateSQL = @"update resultmic as a
set a.istran = 1
where a.reqno = " + m_lstUpdate[i].m_lngReqno + @"

and a.istran = 0";
                        Log.Output("strAutoUpdateSQL");
                        lngRes = m_objService.lngExecuteSQL(strAutoUpdateSQL);
                    }
                }
            }
            return 1;
        }

        #endregion

        #region 置为已传输VO
        /// <summary>
        /// 置为已传输VO
        /// </summary>
        [Serializable]
        public class clsUpdateTransVO
        {
            /// <summary>
            /// 申请单编号
            /// </summary>
            public long m_lngReqno;
            /// <summary>
            /// 卡片号码
            /// </summary>
            public string m_strExeno;
            /// <summary>
            /// 该标本所存在的细菌的代号
            /// </summary>
            public string m_strGermid;
            /// <summary>
            /// 抗生素编号
            /// </summary>
            public string m_strAntiid;
            /// <summary>
            /// 实验方法
            /// </summary>
            public string m_strTest;
        }
        #endregion        
    }
}
