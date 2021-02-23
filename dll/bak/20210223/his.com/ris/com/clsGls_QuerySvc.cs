using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.GLS_WS.GlsWSServer
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsGls_QuerySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 判断某个员工是否有修改提交了的B超报告的权限
        /// <summary>
        /// 判断某个员工是否有修改提交了的B超报告的权限
        /// </summary>
        /// <param name="m_objPrincipal"></param>
        /// <param name="m_strEmp"></param>
        /// <param name="m_strRoleName"></param>
        /// <param name="m_blnHasRole"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeModifyBultraReport(string m_strEmp, string m_strRoleName, out bool m_blnHasRole)
        {
            long lngRes = 0;
            DataTable m_objDataTable = new DataTable();
            m_blnHasRole = false;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                string strSQL = @"select a.roleid_chr
    from t_sys_role a, t_bse_employee b, t_sys_emprolemap c
    where a.roleid_chr = c.roleid_chr
    and b.empid_chr = c.empid_chr
    and b.empid_chr = ?
    and a.name_vchr = ?";
                System.Data.IDataParameter[] m_objDataPara = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataPara);
                m_objDataPara[0].Value = m_strEmp.Trim();
                m_objDataPara[1].Value = m_strRoleName;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objDataTable, m_objDataPara);
                if (lngRes > 0 && m_objDataTable.Rows.Count > 0)
                {
                    m_blnHasRole = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        [AutoComplete]
        public long m_lngGetReport(string p_strReportID, out DataTable p_dtValue)
        {
            string strSql = @"select controlid, ctl_content, ctl_content_xml, recordid
            from ar_content
            where recordid = ?";
            long lngRes = 0;
            clsHRPTableService objHRPsvc = new clsHRPTableService();
            p_dtValue = null;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPsvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                clsLogText objLogger = new clsLogText();
                objHRPsvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strReportID;
                lngRes = objHRPsvc.lngGetDataTableWithParameters(strSql, ref p_dtValue, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPsvc.Dispose();
            }
            //返回
            return lngRes;
        }


        [AutoComplete]
        public long m_lngGetUSReportList(DateTime p_dtmStart, DateTime p_dtmEnd, out List<int> p_intCount)
        {
            string strSql = @"select t1.recordid
  from ar_apply_report t1, ar_content t2
 where t1.delstatus = 0
   and t1.recordid = t2.recordid
   and t1.formclsname = 'frmBultrasoundWorkStation'
   and t2.controlid = 'm_dtpRecordDate'
   and t2.ctl_content between ? and
       ?
";

            long lngRes = 0;
            clsHRPTableService objHRPsvc = new clsHRPTableService();
            DataTable p_dtValue = null;
            p_intCount = null;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPsvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                objHRPsvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStart.ToString("yyyy-MM-dd 00:00:00");
                objDPArr[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");
                lngRes = objHRPsvc.lngGetDataTableWithParameters(strSql, ref p_dtValue, objDPArr);
                if (lngRes > 0 && p_dtValue != null && p_dtValue.Rows.Count > 0)
                {
                    p_intCount = new List<int>(p_dtValue.Rows.Count);
                    foreach (DataRow row in p_dtValue.Rows)
                    {
                        p_intCount.Add(Convert.ToInt32(row[0]));
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPsvc.Dispose();
            }
            //返回
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetUSReport(DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtValue)
        {
            string strSql = @"select t.reportdoctor_chr, count(t.reportid_chr), t.checkroom_chr
  from t_ris_us_report t
 where t.status_int = 1
   and t.checkdate_dat between
       to_date(?, 'yyyy-MM-dd hh24:mi:ss') and
       to_date(?, 'yyyy-MM-dd hh24:mi:ss')
 group by t.reportdoctor_chr, t.checkroom_chr
 order by t.reportdoctor_chr
";

            long lngRes = 0;
            clsHRPTableService objHRPsvc = new clsHRPTableService();
            p_dtValue = null;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPsvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_dtmStart.ToString("yyyy-MM-dd") + " 00:00:00";
                objDPArr[1].Value = p_dtmEnd.ToString("yyyy-MM-dd") + " 23:59:59";
                lngRes = objHRPsvc.lngGetDataTableWithParameters(strSql, ref p_dtValue, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPsvc.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取新的报告ID
        /// </summary>
        /// <param name="p_strReportID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportID(out string p_strReportID)
        {
            string strSql = @"select seq_ris_us_reportid.nextval from dual";
            long lngRes = 0;
            p_strReportID = string.Empty;
            clsHRPTableService objHRPsvc = new clsHRPTableService();
            try
            {
                DataTable dtValue = null;
                lngRes = objHRPsvc.lngGetDataTableWithoutParameters(strSql, ref dtValue);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    p_strReportID = dtValue.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPsvc.Dispose();
            }
            //返回
            return lngRes;
        }

        #region 获取超声序列号
        /// <summary>
        /// 获取超声序列号
        /// </summary>
        /// <param name="m_strBultraSoundID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBultraSoundID(out string m_strBultraSoundID)
        {
            long lngRes = 0;
            m_strBultraSoundID = string.Empty;
            DataTable m_objDataTable = null;
            DataTable m_dtTemp = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {

                // string strSql = @"select substr (seq_bultrasoundid.nextval, -3) as bultrasoundid  from dual";
                #region 查找最大序列号尾数语句 刘浩景 2008-3-24 修改

                //                string strSql = @"select max (substr (a.ctl_content, -3)) + 1 as bultrasoundid 
                //                                  from ar_content a where a.controlid = 'm_txtBultraSoundID'
                //                                  and substr (a.ctl_content, 0, 8) =(select to_char (sysdate, 'yyyymmdd') from dual)
                //                                 ";

                string strSql = @"select max(substr(a.otherid_chr, -3)) + 1 as bultrasoundid
  from t_ris_us_report a
 where a.checkdate_dat between trunc(sysdate) and (trunc(sysdate) + 1)";
                #endregion

                lngRes = objHRPServer.lngGetDataTableWithoutParameters(strSql, ref m_objDataTable);
                lngRes = objHRPServer.lngGetDataTableWithoutParameters("select to_char(sysdate, 'yyyymmdd') as currentday from dual", ref m_dtTemp);
                if (lngRes > 0 && m_objDataTable.Rows.Count > 0)
                {
                    string m_strTemp = m_objDataTable.Rows[0]["bultrasoundid"].ToString().Trim();
                    if (m_strTemp == string.Empty)
                    {
                        m_strBultraSoundID = m_dtTemp.Rows[0]["currentday"].ToString() + "001";
                    }
                    else
                    {
                        m_strBultraSoundID = m_dtTemp.Rows[0]["currentday"].ToString() + m_strTemp.PadLeft(3, '0');
                    }
                }
                else
                {
                    m_strBultraSoundID = m_dtTemp.Rows[0]["currentday"].ToString() + "001";
                }

                objHRPServer.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServer.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion


        #region 模糊查找检查部位
        /// <summary>
        /// 模糊查找检查部位
        /// </summary>
        /// <param name="m_strLike"></param>
        /// <param name="m_objDataTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckPartListByLike(string m_strLike, string p_strType, out DataTable m_objDataTable)
        {
            long lngRes = 0;
            m_objDataTable = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                string strSql = @"select   a.partid, a.partname, a.assistcode_chr, a.typeid
     from ar_apply_partlist a
     where (   lower (a.assistcode_chr) like ?
          or lower (a.partname) like ?
          or lower (a.pycode_vchr) like ?
          or lower (a.wbcode_vchr) like ?
         )
     and a.deleted = 0
     and a.typeid in ([type])
     order by a.assistcode_chr";
                strSql = strSql.Replace("[type]", p_strType);
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strLike.Trim() + "%";
                m_objDataParaArr[1].Value = m_strLike.Trim() + "%";
                m_objDataParaArr[2].Value = m_strLike.Trim() + "%";
                m_objDataParaArr[3].Value = m_strLike.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objDataTable, m_objDataParaArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 根据超声号获得报告单号
        /// <summary>
        /// 根据超声号获得报告单号
        /// </summary>
        /// <param name="p_strPathologyID"></param>
        [AutoComplete]
        public string m_strRecordIDByBultraNo(string m_strBultraID)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            //            string SQL = @"select tb.recordid
            //  from ar_apply_report ta, ar_content tb
            // where ta.recordid = tb.recordid
            //   and ta.delstatus = 0
            //   and tb.controlid = 'm_txtBultraSoundID'
            //   and tb.ctl_content = ?";
            string SQL = @"select tb.recordid
  from  ar_content tb
 where tb.controlid = 'm_txtBultraSoundID'
   and tb.ctl_content = ?";
            try
            {
                System.Data.DataTable dtRecords = null;
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = m_strBultraID;
                objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecords, ParamArr);
                if (dtRecords.Rows.Count == 1)
                    return dtRecords.Rows[0][0].ToString();
                else
                    return string.Empty;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return string.Empty;
        }
        #endregion (获得报告单号)

        #region (显示指定时间段内的人员列表结束)
        [AutoComplete]
        public string m_strGetColValue(long p_lngRecordID, string p_strColName)
        {
            string SQL;
            System.Data.DataTable dtRecords = null;
            SQL = @"select a.ctl_content
  from ar_content a, ar_apply_report b
 where a.recordid = b.recordid and a.controlid = ? and a.recordid = ?";
            IDataParameter[] ParamArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = p_strColName;
                ParamArr[1].Value = p_lngRecordID;
                objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecords, ParamArr);
                if (dtRecords.Rows.Count == 1)
                    return dtRecords.Rows[0][0].ToString();
                else
                    return string.Empty;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return string.Empty;
        }
        #endregion

        #region 获取登记完成列表主要信息
        [AutoComplete]
        public long m_lngGetRecordInfoById(long p_lngRecordID, out string m_strName, out string m_strSex, out string m_strAge, out string m_strInType, out string p_strRoom)
        {
            string SQL;
            m_strName = string.Empty;
            m_strSex = string.Empty;
            m_strInType = string.Empty;
            m_strAge = string.Empty;
            p_strRoom = string.Empty;
            DataTable dtRecords = null;
            SQL = @"select a.controlid ,a.ctl_content
            from ar_content a where  a.recordid = ?";
            IDataParameter[] ParamArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            long lngRes = 0;
            try
            {
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_lngRecordID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecords, ParamArr);
                if (lngRes > 0 && dtRecords.Rows.Count > 0)
                {
                    DataRow row = null;
                    for (int i = 0; i < dtRecords.Rows.Count; i++)
                    {
                        row = dtRecords.Rows[i];
                        switch (dtRecords.Rows[i]["controlid"].ToString().Trim())
                        {
                            case "m_txtName": m_strName = row["ctl_content"].ToString().Trim(); break;
                            case "m_cboSex": m_strSex = row["ctl_content"].ToString().Trim(); break;
                            case "m_txtAge": m_strAge = row["ctl_content"].ToString().Trim(); break;
                            case "m_cboInType": m_strInType = row["ctl_content"].ToString().Trim(); break;
                            case "m_cboCheckRoomList": p_strRoom = row["ctl_content"].ToString().Trim(); break;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;


        }
        #endregion


        #region 获得指定时间段内的B超病人列表
        /// <summary>
        ///获得指定时间段内的B超病人列表
        /// </summary>
        [AutoComplete]
        public void m_mthGetBultraPatientList(string p_strBeginData, string p_strEndDate, string p_strSendStatus, out System.Data.DataTable p_dtRecords)
        {
            p_dtRecords = new DataTable();
            string SQL = string.Empty;
            string strDay = string.Empty;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strSendStatus == "0")//B超登记人员名单显示天数1--99, 1为显示当天
                {   //已完成的只显示当天的时间
                    string SQL1 = @"select setstatus_int
  from t_sys_setting
 where setid_chr = '8012' and moduleid_chr = '0008'";
                    System.Data.DataTable dtRecords = new DataTable();
                    objHRPSvc.lngGetDataTableWithoutParameters(SQL1, ref dtRecords);
                    if (dtRecords.Rows.Count == 1)
                        strDay = dtRecords.Rows[0][0].ToString();
                }
                else if (p_strSendStatus == "1")//B超完成检查名单显示天数1--99, 1为显示当天
                {
                    string SQL1 = @"select setstatus_int
  from t_sys_setting
 where setid_chr = '8013' and moduleid_chr = '0008'";
                    System.Data.DataTable dtRecords = new DataTable();
                    objHRPSvc.lngGetDataTableWithoutParameters(SQL1, ref dtRecords);
                    if (dtRecords.Rows.Count == 1)
                        strDay = dtRecords.Rows[0][0].ToString();
                }
                IDataParameter[] objDPArr = null;
                if (99 >= Convert.ToInt32(strDay) && Convert.ToInt32(strDay) > 0)
                {
                    SQL = @"select   recordid, ctl_content bultrasoundid
    from ar_content
   where controlid = 'm_txtBultraSoundID'
     and recordid in (
            select recordid
              from ar_apply_report
             where sendstatus = ?
               and delstatus = 0
                and formclsname='frmBultrasoundWorkStation'
               and ceateddate
                      between ? and ?)
order by bultrasoundid";
                    objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = Convert.ToInt32(p_strSendStatus);
                    DateTime dtmFrom = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(strDay)).ToString("yyyy-MM-dd 00:00:00"));
                    DateTime dtmTo = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                    objDPArr[1].Value = dtmFrom;
                    objDPArr[2].Value = dtmTo;
                }
                else
                {
                    SQL = @"select   recordid, ctl_content bultrasoundid
    from ar_content
   where controlid = 'm_txtBultraSoundID'
     and recordid in (
                  select recordid
                    from ar_apply_report
                   where sendstatus = ?
                        and formclsname='frmBultrasoundWorkStation'
                         and delstatus = 0)
order by bultrasoundid
";
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = Convert.ToInt32(p_strSendStatus);
                }
                long lngRes = -1;

                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref p_dtRecords, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }
        #endregion


        #region 根据住院号取病人信息
        /// <summary>
        /// 根据住院号取病人信息
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByInPatID(string p_strInPatientID, out clsPatientInfo_Value p_objPatient)
        {
            p_objPatient = null;
            long lngRes = 0;
            try
            {
                string strInPatientID = p_strInPatientID;
                if (strInPatientID == null)
                    return -1;
                string strSQL = @"select a.patientid_chr as patientid, a.inpatientid_chr as inpatientid,
       a.firstname_vchr as patientname, c.deptname, c.areaname, c.bedname,
       a.married_chr as married, a.birthplace_vchr as birthplace,
       a.sex_chr as sex, c.inpatientcount_int as inpatientcount,
       a.idcard_chr as idcard, a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation, a.birth_dat as birth,
       a.nationality_vchr as nationality,
       b.patientcardid_chr as patientcardid, c.deptid_chr as deptid,
       c.areaid_chr as areaid, c.bedid_chr as bedid,
       a.nativeplace_vchr as nativeplace
  from t_bse_patient a left outer join t_bse_patientcard b on a.patientid_chr =
                                                                b.patientid_chr
       left outer join (select inpatientid_chr, patientid_chr,
                               inpatientcount_int, deptid_chr, areaid_chr,
                               bedid_chr, b2.deptname_vchr as deptname,
                               c2.deptname_vchr as areaname,
                               d2.code_chr as bedname
                          from t_opr_bih_register a2 left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                                                      b2.deptid_chr
                               left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                                 c2.deptid_chr
                               left join t_bse_bed d2 on a2.bedid_chr =
                                                                  d2.bedid_chr
                         where a2.status_int = '1' and a2.pstatus_int <> '3') c on a.patientid_chr =
                                                                                     c.patientid_chr
 where b.status_int > 0 and c.inpatientid_chr = ?";
                lngRes = m_lngGetPatient(strSQL, strInPatientID, out p_objPatient);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        #endregion


        #region 根据病人卡号取病人信息
        /// <summary>
        /// 根据病人卡号取病人信息
        /// </summary>
        /// <param name="p_strCardID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByCardID(string p_strCardID, out clsPatientInfo_Value p_objPatient)
        {

            p_objPatient = null;
            long lngRes = 0;
            try
            {
                string strCardID = p_strCardID;

                if (strCardID == null)
                    return -1;
                if (strCardID.Trim().Length < 10)
                    strCardID = strCardID.Trim().PadLeft(10, '0');
                string strSql = @"select a.patientid_chr as patientid, a.inpatientid_chr as inpatientid,
       a.firstname_vchr as patientname, c.deptname, c.areaname, c.bedname,
       a.married_chr as married, a.birthplace_vchr as birthplace,
       a.sex_chr as sex, c.inpatientcount_int as inpatientcount,
       a.idcard_chr as idcard, a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation, a.birth_dat as birth,
       a.nationality_vchr as nationality,
       b.patientcardid_chr as patientcardid, c.deptid_chr as deptid,
       c.areaid_chr as areaid, c.bedid_chr as bedid,
       a.nativeplace_vchr as nativeplace
  from t_bse_patient a left outer join t_bse_patientcard b on a.patientid_chr =
                                                                b.patientid_chr
       left outer join (select inpatientid_chr, patientid_chr,
                               inpatientcount_int, deptid_chr, areaid_chr,
                               bedid_chr, b2.deptname_vchr as deptname,
                               c2.deptname_vchr as areaname,
                               d2.code_chr as bedname
                          from t_opr_bih_register a2 left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                                                      b2.deptid_chr
                               left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                                 c2.deptid_chr
                               left join t_bse_bed d2 on a2.bedid_chr =
                                                                  d2.bedid_chr
                         where a2.status_int = '1' and a2.pstatus_int <> '3') c on a.patientid_chr =
                                                                                     c.patientid_chr
 where b.status_int > 0 and b.patientcardid_chr = ? ";
                lngRes = m_lngGetPatient(strSql, strCardID, out p_objPatient);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 根据病人编号取病人信息
        /// <summary>
        /// 根据病人编号取病人信息
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByID(string p_strPatientID, out clsPatientInfo_Value p_objPatient)
        {
            long lngRes = 0;
            p_objPatient = null;
            try
            {
                string strPatientID = p_strPatientID;
                if (strPatientID == null)
                    return -1;
                if (strPatientID.Trim().Length < 10)
                    strPatientID = strPatientID.Trim().PadLeft(10, '0');
                string strSql = @"select a.patientid_chr as patientid, a.inpatientid_chr as inpatientid,
       a.firstname_vchr as patientname, c.deptname, c.areaname, c.bedname,
       a.married_chr as married, a.birthplace_vchr as birthplace,
       a.sex_chr as sex, c.inpatientcount_int as inpatientcount,
       a.idcard_chr as idcard, a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation, a.birth_dat as birth,
       a.nationality_vchr as nationality,
       b.patientcardid_chr as patientcardid, c.deptid_chr as deptid,
       c.areaid_chr as areaid, c.bedid_chr as bedid,
       a.nativeplace_vchr as nativeplace
  from t_bse_patient a left outer join t_bse_patientcard b on a.patientid_chr =
                                                                b.patientid_chr
       left outer join (select inpatientid_chr, patientid_chr,
                               inpatientcount_int, deptid_chr, areaid_chr,
                               bedid_chr, b2.deptname_vchr as deptname,
                               c2.deptname_vchr as areaname,
                               d2.code_chr as bedname
                          from t_opr_bih_register a2 left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                                                      b2.deptid_chr
                               left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                                 c2.deptid_chr
                               left join t_bse_bed d2 on a2.bedid_chr =
                                                                  d2.bedid_chr
                         where a2.status_int = '1' and a2.pstatus_int <> '3') c on a.patientid_chr =
                                                                                     c.patientid_chr
 where b.status_int > 0 and a.patientid_chr = ?
 ";
                lngRes = m_lngGetPatient(strSql, strPatientID, out p_objPatient);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        #endregion
        #region 模糊查找员工，返回员工ID和员工名称数组
        /// <summary>
        /// 模糊查找员工，返回员工ID和员工名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByLike(string p_strLike, string m_strDeptLike, out DataTable dtResult2)
        {
            dtResult2 = null;
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return -1;
                string strSql = @"select distinct t.empno_chr, t.lastname_vchr
           from t_bse_employee t inner join t_bse_deptemp a on t.empid_chr =
                                                                   a.empid_chr
                inner join t_bse_deptdesc b on a.deptid_chr = b.deptid_chr
          where (   t.empno_chr like ?
                 or t.lastname_vchr like ?
                 or t.pycode_chr like ?
                 or t.shortname_chr like ?
                 or b.shortno_chr like ?
                )
            and b.deptname_vchr like ?
            and t.status_int = 1
            and t.hasprescriptionright_chr='1'
       order by t.lastname_vchr";
                DataTable dtResult = new DataTable();

                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[1].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                m_objDataParaArr[3].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[4].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[5].Value = m_strDeptLike.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, m_objDataParaArr);
                objHRPSvc.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                dtResult2 = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["EMPNO_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;

        }
        #endregion
        #region 取病人信息（全院，包括门诊、住院、电子病历）
        /// <summary>
        /// 取病人信息（全院，包括门诊、住院、电子病历）
        /// </summary>
        /// <param name="p_strSql"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetPatient(string strSQL, string m_strValue, out clsPatientInfo_Value p_objPatient)
        {
            p_objPatient = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            long lngRes = 0;
            DataTable dtRecords = new DataTable();
            try
            {
                System.Data.IDataParameter[] m_objDataPara = null;
                objHRPSvc.CreateDatabaseParameter(1, out m_objDataPara);
                m_objDataPara[0].Value = m_strValue.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRecords, m_objDataPara);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            if (lngRes <= 0 || dtRecords.Rows.Count != 1)
                return 0;
            p_objPatient = new clsPatientInfo_Value();
            p_objPatient.m_StrInPatientID = dtRecords.Rows[0]["INPATIENTID"].ToString().Trim();
            p_objPatient.m_StrPatientID = dtRecords.Rows[0]["PATIENTID"].ToString().Trim();
            p_objPatient.m_StrPatientCardID = dtRecords.Rows[0]["PATIENTCARDID"].ToString().Trim();
            p_objPatient.m_StrOutPatientID = "";
            p_objPatient.m_StrDeptID = dtRecords.Rows[0]["DEPTID"].ToString().Trim();
            p_objPatient.m_StrAreaID = dtRecords.Rows[0]["AREAID"].ToString().Trim();
            p_objPatient.m_StrBedID = dtRecords.Rows[0]["BEDID"].ToString().Trim();

            p_objPatient.m_StrDeptName = dtRecords.Rows[0]["DEPTNAME"].ToString().Trim();
            p_objPatient.m_StrAreaName = dtRecords.Rows[0]["AREANAME"].ToString().Trim();
            p_objPatient.m_StrBedName = dtRecords.Rows[0]["BEDNAME"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo = new clsPeopleInfo();
            try
            {
                p_objPatient.m_ObjPeopleInfo.m_DtmBirth = Convert.ToDateTime(dtRecords.Rows[0]["BIRTH"]);
            }
            catch
            {
                p_objPatient.m_ObjPeopleInfo.m_DtmBirth = DateTime.Parse("1900-1-1");
            }
            try
            {
                p_objPatient.m_ObjPeopleInfo.m_IntTimes = Convert.ToInt32(dtRecords.Rows[0]["INPATIENTCOUNT"]);
            }
            catch
            {
                p_objPatient.m_ObjPeopleInfo.m_IntTimes = 0;
            }
            p_objPatient.m_ObjPeopleInfo.m_StrFirstName = dtRecords.Rows[0]["PATIENTNAME"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrHomeAddress = dtRecords.Rows[0]["AREAID"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrIDCard = dtRecords.Rows[0]["IDCARD"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrLastName = p_objPatient.m_ObjPeopleInfo.m_StrFirstName;
            p_objPatient.m_ObjPeopleInfo.m_StrMarried = dtRecords.Rows[0]["MARRIED"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrNation = "";
            p_objPatient.m_ObjPeopleInfo.m_StrNationality = dtRecords.Rows[0]["NATIONALITY"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrNativePlace = dtRecords.Rows[0]["NATIVEPLACE"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrSex = dtRecords.Rows[0]["SEX"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrOccupation = dtRecords.Rows[0]["OCCUPATION"].ToString().Trim();
            p_objPatient.m_ObjPeopleInfo.m_StrAge = clsConvertDateTime.CalcAge(Convert.ToDateTime(dtRecords.Rows[0]["BIRTH"]));
            //返回
            return lngRes;
        }
        #endregion

        #region 模糊查找病人姓名，返回病人ID和病人名称数组
        /// <summary>
        /// 模糊查找病人姓名，返回病人ID和病人名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientNameByLike(string p_strLike, out DataTable dtResult2)
        {
            dtResult2 = null;
            if (p_strLike == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string strSql = @"select t.patientid_chr,t.lastname_vchr from t_bse_patient t where t.lastname_vchr like ? order by t.lastname_vchr";
                DataTable dtResult = new DataTable();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = "%" + p_strLike.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, ParamArr);
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                dtResult2 = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["PATIENTID_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 模糊查找员工，返回员工ID和员工名称数组
        /// <summary>
        /// 模糊查找员工，返回员工ID和员工名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByLike(string p_strLike, out DataTable dtResult2)
        {
            dtResult2 = null;
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return -1;
                string strSql = @"select distinct t.empno_chr, t.lastname_vchr
           from t_bse_employee t inner join t_bse_deptemp a on t.empid_chr =
                                                                   a.empid_chr
                inner join t_bse_deptdesc b on a.deptid_chr = b.deptid_chr
          where (   t.empno_chr like ?
                 or t.lastname_vchr like ?
                 or t.pycode_chr like ?
                 or t.shortname_chr like ?
                 or b.shortno_chr like ?
                )
            and t.status_int = 1
            and t.hasprescriptionright_chr='1'
       order by t.lastname_vchr";
                DataTable dtResult = new DataTable();
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[1].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                m_objDataParaArr[3].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[4].Value = p_strLike.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, m_objDataParaArr);
                objHRPSvc.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                dtResult2 = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["EMPNO_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;

        }
        #endregion

        #region 模糊查找床号，返回床号ID和床号名称数组
        /// <summary>
        /// 模糊查找床号，返回床号ID和床号名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedNameByLike(string p_strLike, out DataTable dtResult2)
        {
            long lngRes = 0;
            dtResult2 = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //p_strNameArr = null;
                if (p_strLike == null)
                    return -1;
                string strSql = @"select distinct t.bedid_chr, t.code_chr
           from t_bse_bed t
           where (t.bedid_chr like ? or t.code_chr like ?)
           and t.status_int <> '5'
           order by t.code_chr";
                DataTable dtResult = new DataTable();
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strLike.Trim() + "%";
                m_objDataParaArr[1].Value = p_strLike.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, m_objDataParaArr);
                objHRPSvc.Dispose();
                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                    return 0;
                dtResult2 = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["BEDID_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["CODE_CHR"].ToString().Trim();
                //}

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 返回科室ID和科室名称数组
        /// <summary>
        /// 返回科室ID和科室名称数组
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaByLike(string p_strLike, out DataTable dtResult, string p_strDeptID)
        {
            //p_strDeptArr = null;
            dtResult = m_dtbGetDept(p_strLike, p_strDeptID);
            if (dtResult == null || dtResult.Rows.Count == 0)
                return 0;
            //p_strDeptArr = new string[dtResult.Rows.Count, 2];
            //for (int i = 0; i < dtResult.Rows.Count; i++)
            //{
            //    p_strDeptArr[i, 0] = dtResult.Rows[i]["SHORTNO_CHR"].ToString().Trim();
            //    p_strDeptArr[i, 1] = dtResult.Rows[i]["DEPTNAME_VCHR"].ToString().Trim();
            //}
            return 1;
        }
        #endregion

        #region 取得科室
        /// <summary>
        /// 取得科室
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        private DataTable m_dtbGetDept(string p_strLike, string p_strDeptID)
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strLike == null)
                    return null;
                string strSql = string.Empty;
                IDataParameter[] objDPArr = null;
                if (p_strLike.Trim() != "")
                {
                    if (p_strDeptID.Trim() != string.Empty)
                    {
                        strSql = @"select distinct a.deptid_chr, a.modify_dat, a.deptname_vchr, a.category_int,
                a.inpatientoroutpatient_int, a.operatorid_chr, a.address_vchr,
                a.pycode_chr, a.attributeid, a.parentid, a.createdate_dat,
                a.status_int, a.deactivate_dat, a.wbcode_chr, a.code_vchr,
                a.extendid_vchr, a.shortno_chr, a.stdbed_count_int,
                a.putmed_int
           from t_bse_deptdesc a, t_bse_deptemp b, t_bse_employee c
          where a.deptid_chr = b.deptid_chr
            and b.empid_chr = c.empid_chr
            and c.hasprescriptionright_chr = '1'
            and a.status_int = 1
            and a.category_int = 0 
            and (a.deptid_chr like ?
                 or a.deptname_vchr like ?
                 or (a.pycode_chr like ?)
                 or (a.wbcode_chr like ?)
                 or a.shortno_chr like ?
                )
            and a.attributeid = '0000003' and a.parentid = ?
           order by a.code_vchr";
                        objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
                        objDPArr[0].Value = p_strLike.Trim() + "%";
                        objDPArr[1].Value = "%" + p_strLike.Trim() + "%";
                        objDPArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                        objDPArr[3].Value = p_strLike.Trim() + "%";
                        objDPArr[4].Value = p_strLike.Trim() + "%";
                        objDPArr[5].Value = p_strDeptID.Trim();
                    }
                    else
                    {
                        strSql = @"select distinct a.deptid_chr, a.modify_dat, a.deptname_vchr, a.category_int,
                a.inpatientoroutpatient_int, a.operatorid_chr, a.address_vchr,
                a.pycode_chr, a.attributeid, a.parentid, a.createdate_dat,
                a.status_int, a.deactivate_dat, a.wbcode_chr, a.code_vchr,
                a.extendid_vchr, a.shortno_chr, a.stdbed_count_int,
                a.putmed_int
           from t_bse_deptdesc a, t_bse_deptemp b, t_bse_employee c
          where a.deptid_chr = b.deptid_chr
            and b.empid_chr = c.empid_chr
            and c.hasprescriptionright_chr = '1'
            and a.status_int = 1
            and a.category_int = 0 
            and (a.deptid_chr like ?
                 or a.deptname_vchr like ?
                 or (a.pycode_chr like ?)
                 or (a.wbcode_chr like ?)
                 or a.shortno_chr like ?
                )
           order by a.code_vchr";
                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_strLike.Trim() + "%";
                        objDPArr[1].Value = "%" + p_strLike.Trim() + "%";
                        objDPArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                        objDPArr[3].Value = p_strLike.Trim() + "%";
                        objDPArr[4].Value = p_strLike.Trim() + "%";
                    }
                }
                else
                {
                    if (p_strDeptID.Trim() != string.Empty)
                    {
                        strSql = @"select distinct a.deptid_chr, a.modify_dat, a.deptname_vchr, a.category_int,
                a.inpatientoroutpatient_int, a.operatorid_chr, a.address_vchr,
                a.pycode_chr, a.attributeid, a.parentid, a.createdate_dat,
                a.status_int, a.deactivate_dat, a.wbcode_chr, a.code_vchr,
                a.extendid_vchr, a.shortno_chr, a.stdbed_count_int,
                a.putmed_int
           from t_bse_deptdesc a, t_bse_deptemp b, t_bse_employee c
          where a.deptid_chr = b.deptid_chr
            and b.empid_chr = c.empid_chr
            and c.hasprescriptionright_chr = '1'
            and a.status_int = 1
            and a.category_int = 0    
            and a.attributeid = '0000003' and a.parentid = ?
            order by a.code_vchr";
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = p_strDeptID.Trim();
                    }
                    else
                    {
                        strSql = @"select distinct a.deptid_chr, a.modify_dat, a.deptname_vchr, a.category_int,
                a.inpatientoroutpatient_int, a.operatorid_chr, a.address_vchr,
                a.pycode_chr, a.attributeid, a.parentid, a.createdate_dat,
                a.status_int, a.deactivate_dat, a.wbcode_chr, a.code_vchr,
                a.extendid_vchr, a.shortno_chr, a.stdbed_count_int,
                a.putmed_int
           from t_bse_deptdesc a, t_bse_deptemp b, t_bse_employee c
          where a.deptid_chr = b.deptid_chr
            and b.empid_chr = c.empid_chr
            and c.hasprescriptionright_chr = '1'
            and a.status_int = 1
            and a.category_int = 0
            order by a.code_vchr";
                    }
                }
                if (objDPArr == null)
                {
                    long lngRes = objHRPSvc.DoGetDataTable(strSql, ref dtResult);
                }
                else
                {
                    long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return dtResult;
        }
        #endregion

        #region 根据病人ID获取住院号
        /// <summary>
        /// 根据病人ID获取住院号
        /// </summary>
        /// <param name="p_strPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetInPatientIDByPatientID(string p_strPatient)
        {
            string strRes = "";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strPatient == null || p_strPatient == "")
                    return "";
                string strSql = @"select t.inpatientid_chr from t_opr_bih_register t where t.patientid_chr = ?";
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatient;
                DataTable dtResult = new DataTable();

                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count == 1)
                {
                    return dtResult.Rows[0]["INPATIENTID"].ToString().Trim();
                }
                else
                    return "";

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return strRes;
        }
        #endregion

        #region 根据住院号取病人信息
        /// <summary>
        /// 根据住院号取病人信息
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_objPatient"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByName(string p_strInPatientName, out clsPatientInfo_Value[] p_objPatientArr)
        {
            long lngRes = 0;
            p_objPatientArr = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            try
            {
                p_objPatientArr = null;
                if (p_strInPatientName == null || p_strInPatientName == "")
                    return -1;
                string strSql = @"select a.patientid_chr as patientid, a.inpatientid_chr as inpatientid,
       a.lastname_vchr as patientname, c.deptname, c.areaname, c.bedname,
       a.married_chr as married, a.birthplace_vchr as birthplace,
       a.sex_chr as sex, c.inpatientcount_int as inpatientcount,
       a.idcard_chr as idcard, a.homeaddress_vchr as homeaddress,
       a.occupation_vchr as occupation, a.birth_dat as birth,
       a.nationality_vchr as nationality,
       b.patientcardid_chr as patientcardid, c.deptid_chr as deptid,
       c.areaid_chr as areaid, c.bedid_chr as bedid,
       a.nativeplace_vchr as nativeplace
  from t_bse_patient a left outer join t_bse_patientcard b on a.patientid_chr =
                                                                b.patientid_chr
       left outer join (select a2.*, b2.deptname_vchr as deptname,
                               c2.deptname_vchr as areaname,
                               d2.code_chr as bedname
                          from t_opr_bih_register a2 left join t_bse_deptdesc b2 on a2.deptid_chr =
                                                                                      b2.deptid_chr
                               left join t_bse_deptdesc c2 on a2.areaid_chr =
                                                                 c2.deptid_chr
                               left join t_bse_bed d2 on a2.bedid_chr =
                                                                  d2.bedid_chr
                         where a2.status_int = '1') c on a.patientid_chr =
                                                               c.patientid_chr
 where b.status_int > 0 and a.lastname_vchr = ?";
                DataTable dtRecords = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientName.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtRecords, objDPArr);
                if (lngRes <= 0 || dtRecords.Rows.Count <= 0)
                    return 0;

                p_objPatientArr = new clsPatientInfo_Value[dtRecords.Rows.Count];
                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    p_objPatientArr[i] = new clsPatientInfo_Value();
                    p_objPatientArr[i].m_StrInPatientID = dtRecords.Rows[i]["INPATIENTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrPatientID = dtRecords.Rows[i]["PATIENTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrPatientCardID = dtRecords.Rows[i]["PATIENTCARDID"].ToString().Trim();
                    p_objPatientArr[i].m_StrOutPatientID = "";
                    p_objPatientArr[i].m_StrDeptID = dtRecords.Rows[i]["DEPTID"].ToString().Trim();
                    p_objPatientArr[i].m_StrAreaID = dtRecords.Rows[i]["AREAID"].ToString().Trim();
                    p_objPatientArr[i].m_StrBedID = dtRecords.Rows[i]["BEDID"].ToString().Trim();

                    p_objPatientArr[i].m_StrDeptName = dtRecords.Rows[i]["DEPTNAME"].ToString().Trim();
                    p_objPatientArr[i].m_StrAreaName = dtRecords.Rows[i]["AREANAME"].ToString().Trim();
                    p_objPatientArr[i].m_StrBedName = dtRecords.Rows[i]["BEDNAME"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo = new clsPeopleInfo();
                    try
                    {
                        p_objPatientArr[i].m_ObjPeopleInfo.m_DtmBirth = Convert.ToDateTime(dtRecords.Rows[i]["BIRTH"]);
                    }
                    catch { p_objPatientArr[i].m_ObjPeopleInfo.m_DtmBirth = DateTime.Parse("1900-1-1"); }
                    try
                    {
                        p_objPatientArr[i].m_ObjPeopleInfo.m_IntTimes = Convert.ToInt32(dtRecords.Rows[i]["INPATIENTCOUNT"]);
                    }
                    catch { p_objPatientArr[i].m_ObjPeopleInfo.m_IntTimes = 0; }
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrFirstName = dtRecords.Rows[i]["PATIENTNAME"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrHomeAddress = dtRecords.Rows[i]["AREAID"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrIDCard = dtRecords.Rows[i]["IDCARD"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrLastName = p_objPatientArr[i].m_ObjPeopleInfo.m_StrFirstName;
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrMarried = dtRecords.Rows[i]["MARRIED"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNation = "";
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNationality = dtRecords.Rows[i]["NATIONALITY"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrNativePlace = dtRecords.Rows[i]["NATIVEPLACE"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrSex = dtRecords.Rows[i]["SEX"].ToString().Trim();
                    p_objPatientArr[i].m_ObjPeopleInfo.m_StrOccupation = dtRecords.Rows[i]["OCCUPATION"].ToString().Trim();
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion



        #region 获取数据
        /// <summary>
        /// 获取数据 
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetData(string p_strSQL, ref DataTable p_dtRecords)
        {
            p_dtRecords = null;
            long lngRes = -1;

            if (p_strSQL.Trim().Length <= 0)
                return lngRes;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtRecords);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion
        /// <summary>
        /// 获取数据 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="m_dtRecords"></param>
        /// <param name="m_objDataParmArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataByParmArr(string strSQL, ref DataTable m_dtRecords, object[] m_objArr, bool m_blnSetOtherDSN)
        {
            long lngRes = -1;
            System.Data.IDataParameter[] m_objDataParmsArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (m_blnSetOtherDSN == true)
                {
                    objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                }
                objHRPSvc.CreateDatabaseParameter(m_objArr.Length, out m_objDataParmsArr);
                for (int i = 0; i < m_objArr.Length; i++)
                {
                    m_objDataParmsArr[i].Value = m_objArr[i];
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtRecords, m_objDataParmsArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        #region 获取某记录的图片
        /// <summary>
        /// 获取某记录的图片
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objPicArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAR_PicValue(string p_strRecordID, int p_intType, string p_strPrintPic, out clsPictureBoxValue[] p_objPicArr)
        {
            string SQL = "";
            DataTable dtRecords = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;

            p_objPicArr = null;
            IDataParameter[] objDPArr = null;
            //p_intType: 0 全部图像； 1 部分(选择)打印图像
            if (p_intType == 0)
            {
                SQL = @"select   recordid, controlid, imageid, imagecontent, height, width,
         remark_vchr
    from ar_image
   where recordid = ?
order by imageid
";
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRecordID;
            }
            else if (p_intType == 1)
            {
                SQL = @"select   recordid, controlid, imageid, imagecontent, height, width,
         remark_vchr
    from ar_image
   where recordid = ?
         and imageid in (" + p_strPrintPic + @")
order by imageid";
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRecordID;
            }
            long lngRes = 0;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecords, objDPArr);
                if (lngRes <= 0 || dtRecords == null || dtRecords.Rows.Count == 0) { return 0; }

                p_objPicArr = new clsPictureBoxValue[dtRecords.Rows.Count];
                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    p_objPicArr[i] = new clsPictureBoxValue();

                    p_objPicArr[i].m_strREMARK_VCHR = dtRecords.Rows[i]["REMARK_VCHR"].ToString().Trim();

                    p_objPicArr[i].m_strCONTROLID = dtRecords.Rows[i]["CONTROLID"].ToString().Trim();
                    try
                    {
                        System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])(dtRecords.Rows[i]["IMAGECONTENT"]));
                        p_objPicArr[i].m_bytImage = (byte[])(dtRecords.Rows[i]["IMAGECONTENT"]);
                        p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(objStream);
                    }
                    catch { p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(32, 32); }

                    try
                    {
                        p_objPicArr[i].intWidth = Convert.ToInt32(dtRecords.Rows[i]["WIDTH"]);
                    }
                    catch { p_objPicArr[i].intWidth = 100; }

                    try
                    {
                        p_objPicArr[i].intHeight = Convert.ToInt32(dtRecords.Rows[i]["HEIGHT"]);
                    }
                    catch { p_objPicArr[i].intHeight = 100; }

                    p_objPicArr[i].intImgID = Convert.ToInt32(dtRecords.Rows[i]["IMAGEID"]);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion


        #region 从t_sys_setting 得setstatus_int
        /// <summary>
        /// 从t_sys_setting 得setstatus_int
        /// </summary>
        /// <param name="p_strPathologyID"></param>
        [AutoComplete]
        public string m_strGetsetstatusFromt_sys_setting(string p_strsetid_chr)
        {
            string strSQL = @"select setstatus_int
  from t_sys_setting
 where setid_chr = ? and moduleid_chr = '0008'";
            System.Data.DataTable dtRecords = null;
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            string m_strReturn = string.Empty;
            try
            {
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strsetid_chr;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRecords, ParamArr);
                if (lngRes > 0)
                {
                    if (dtRecords.Rows.Count == 1)
                    {
                        m_strReturn = dtRecords.Rows[0][0].ToString();
                    }

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return m_strReturn;
        }
        #endregion (获得报告单号)

        #region 获得指定时间段内的人员列表
        /// <summary>
        /// 获得指定时间段内的人员列表		
        /// </summary>
        [AutoComplete]
        public void m_mthGetPatientList(string p_strBeginData, string p_strEndDate, string p_strSendStatus, out System.Data.DataTable p_dtRecords)
        {
            p_dtRecords = new DataTable();
            object[] objDPArr = null;
            string SQL = string.Empty;
            //				string SQL = "";
            string strDay = "";
            if (p_strSendStatus == "0")//病理登记人员名单显示天数1--99, 1为显示当天
            {//已完成的只显示当天的时间
                strDay = m_strGetsetstatusFromt_sys_setting("8002");
            }
            else if (p_strSendStatus == "1")//病理完成检查名单显示天数1--99, 1为显示当天
            {
                strDay = m_strGetsetstatusFromt_sys_setting("8003");
            }
            if (99 >= Convert.ToInt32(strDay) && Convert.ToInt32(strDay) > 0)
            {
                SQL = @" select   recordid, ctl_content pathologyid
    from ar_content
   where controlid = 'm_txtPathologyID'
     and recordid in (
            select recordid
              from ar_apply_report
             where sendstatus = ?
               and delstatus = 0
               and ceateddate
                      between ?
                          and ?
order by pathologyid desc";
                objDPArr = new object[3];
                DateTime dtmFrom = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(strDay)).ToString("yyyy-MM-dd 00:00:00"));
                DateTime dtmTo = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                objDPArr[0] = Convert.ToInt32(p_strSendStatus);
                objDPArr[1] = dtmFrom;
                objDPArr[2] = dtmTo;
            }
            else
            {
                SQL = @"select   recordid, ctl_content pathologyid
    from ar_content
   where controlid = 'm_txtPathologyID'
     and recordid in (
                  select recordid
                    from ar_apply_report
                   where sendstatus = ?
                         and delstatus = 0)
order by pathologyid desc";
                objDPArr = new object[1];
                objDPArr[0] = Convert.ToInt32(p_strSendStatus);
            }
            m_lngGetDataByParmArr(SQL, ref p_dtRecords, objDPArr, true);
        }
        #endregion (获得指定时间段内的人员列表结束)

        #region 获得报告单号
        /// <summary>
        /// 根据病理号获得报告单号
        /// </summary>
        /// <param name="p_strPathologyID"></param>
        [AutoComplete]
        public string m_strRecordID(string p_strPathologyID)
        {
            string strSQL = @"select tb.recordid
  from ar_apply_report ta, ar_content tb
 where ta.recordid = tb.recordid
   and ta.delstatus = 0
   and tb.controlid = 'm_txtPathologyID'
   and tb.ctl_content = ?";
            System.Data.DataTable dtRecords = null;
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            string m_strReturn = string.Empty;
            try
            {
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strPathologyID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRecords, ParamArr);
                if (lngRes > 0)
                {
                    if (dtRecords.Rows.Count == 1)
                    {
                        m_strReturn = dtRecords.Rows[0][0].ToString();
                    }

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return m_strReturn;
        }
        #endregion (获得报告单号)

        #region 取得图像最大ID号
        /// <summary>
        /// 取得图像最大ID号
        /// </summary>
        /// <param name="m_strPathologyID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMaxImageID(string m_strPathologyID)
        {
            string m_strRecordID = this.m_strRecordID(m_strPathologyID);

            if (m_strRecordID.Trim() == "")
            {
                return "";
            }

            string SQL = "select max (imageid) from ar_image where recordid = ?";
            System.Data.DataTable dtRecords = null;
            object[] objDPArr = new object[1];
            objDPArr[0] = m_strRecordID;
            m_lngGetDataByParmArr(SQL, ref dtRecords, objDPArr, true);
            if (dtRecords.Rows.Count == 1)
                return dtRecords.Rows[0][0].ToString();
            else
                return "";
        }

        #endregion
        #region 通过B超号取得图像最大ID号
        /// <summary>
        /// 通过B超号取得图像最大ID号
        /// </summary>
        /// <param name="m_strPathologyID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMaxImageIDByBultraSoundID(string m_strBultraSoundIDD)
        {
            string m_strRecordID = this.m_strRecordIDByBultraNo(m_strBultraSoundIDD);

            if (m_strRecordID.Trim() == "")
            {
                return "";
            }

            string SQL = "select max (imageid) from ar_image where recordid = ?";
            System.Data.DataTable dtRecords = null;
            object[] objDPArr = new object[1];
            objDPArr[0] = m_strRecordID;
            m_lngGetDataByParmArr(SQL, ref dtRecords, objDPArr, true);
            if (dtRecords.Rows.Count == 1)
                return dtRecords.Rows[0][0].ToString();
            else
                return "";
        }

        #endregion

        [AutoComplete]
        public long m_lngGetApplyList(out DataTable dt, DateTime dtmBegin, DateTime dtmEnd, string strFilter, int intChargeFlag, int intPatchFkag)
        {
            long lngRes = -1;
            string strSQL = @"select b.status_int, a.applyid, a.name, a.sex, a.age, a.bihno, a.cardno
                                  from ar_common_apply a, t_opr_attachrelation b
                                 where a.applyid = b.attachid_vchr
                                   and b.attachtype_int = 1
                                   and a.typeid = '9'
                                   and deleted = 0
                                   and submitted = 1
                                   and (a.applydate between ? and ?) ";
            dt = new DataTable();
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //if (intChargeFlag > 0 && intPatchFkag == 0)
                //{
                //    strSQL += " and b.status_int = 0";
                //}


                if (intPatchFkag == 0)
                {
                    if (intChargeFlag == 0)
                    {
                        strSQL += " and (b.status_int <> 0 or b.status_int <> 2)";
                    }
                    else
                    {
                        strSQL += " and b.status_int <> 2";
                    }
                }
                else
                {
                    if (intChargeFlag == 0)
                    {
                        strSQL += " and b.status_int <> 0";
                    }
                }

                if (!string.IsNullOrEmpty(strFilter))
                {
                    strSQL += strFilter;
                }

                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].DbType = DbType.DateTime;
                param[0].Value = dtmBegin;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        #region 申请单处理 2007-3-7 yunjie.xie添加
        /// <summary>
        /// 申请单处理
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strSearchTime"></param>
        /// <param name="intChargeFlag">是否显示未收费 0 不显示 >0 显示 </param>
        /// <param name="intPatchFkag">是否显示退费 0 不显示 >0 显示 </param>
        /// <param name="strFilter">条件过滤</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplyList(out DataTable dt, string p_strTypeID, string p_strSearchTime, int intChargeFlag, int intPatchFkag)
        {
            long lngRes = -1;
            string strSQL = @"select b.status_int, a.applyid, a.name, a.sex, a.age, a.bihno, a.cardno
                                  from ar_common_apply a, t_opr_attachrelation b
                                 where a.applyid = b.attachid_vchr
                                   and b.attachtype_int = 1
                                   and a.typeid = ?
                                   and deleted = 0
                                   and submitted = 1
                                   and (a.applydate between ? and ?) ";
            dt = new DataTable();
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //if (intChargeFlag > 0 && intPatchFkag == 0)
                //{
                //    strSQL += " and b.status_int = 0";
                //}

                if (intPatchFkag == 0)
                {
                    if (intChargeFlag == 0)
                    {
                        strSQL += " and (b.status_int <> 0 or b.status_int <> 2)";
                    }
                    else
                    {
                        strSQL += " and b.status_int <> 2";
                    }
                }
                else
                {
                    if (intChargeFlag == 0)
                    {
                        strSQL += " and b.status_int <> 0";
                    }
                }
                DateTime dtmBegin = Convert.ToDateTime(p_strSearchTime + " 00:00:00");
                DateTime dtmEnd = Convert.ToDateTime(p_strSearchTime + " 23:59:59");

                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = p_strTypeID;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 读取申请单数据			
        /// </summary>
        /// <param name="dtRecords"></param>
        /// <param name="p_strTypeID">申请单类型</param>
        [AutoComplete]
        public void m_mthGetApplyList(out System.Data.DataTable dtRecords, string p_strTypeID)
        {
            dtRecords = new DataTable();
            string strSQL = @"select applyid, name, sex, age
  from ar_common_apply
 where typeid = ? and deleted = 0 and submitted = 1";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] objLisAddItemRefArr = null;
            try
            {
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strTypeID;

                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRecords, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }

        }
        /// <summary>
        /// 获取B超常用值模板信息
        /// </summary>
        /// <param name="m_strEmpID"></param>
        /// <param name="m_strDeptID"></param>
        /// <param name="m_objTable"></param>
        [AutoComplete]
        public long m_mthGetCommonTemplateInfo(string m_strEmpID, string m_strDeptID, out DataTable m_objTableA, out DataTable m_objTableB)
        {
            m_objTableA = new DataTable();
            m_objTableB = new DataTable();
            string strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmBultrasoundWorkStation'
                                                        and control_id =
                                                               'm_txtPatholoySaw') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmBultrasoundWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmBultrasoundWorkStation'
                                                        and control_id =
                                                               'm_txtPatholoySaw') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmBultrasoundWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by b.order_no, a.order_no
";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableA, m_objDataParaArr);
                strSQL = @"select   a.set_id, a.start_date, a.end_date, a.set_name, a.visiblity_level,
         a.employee_id, a.keyword, a.keyword_type, a.keyword_py, a.order_no,
         a.form_id
    from (select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmBultrasoundWorkStation'
                                                        and control_id =
                                                               'm_txtPatholoyDiagnoses') b on ts.set_id =
                                                                                          b.templateset_id
           where ts.form_id = 'frmBultrasoundWorkStation'
             and (   (ts.visiblity_level = 0 and ts.employee_id = ?)
                  or (ts.visiblity_level = 1)
                 )
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%'
          union all
          select ts.start_date, ts.end_date, ts.set_name, ts.visiblity_level,
                 ts.employee_id, ts.keyword, ts.keyword_type, ts.keyword_py,
                 ts.order_no, ts.form_id, ts.set_id
            from templateset ts inner join (select distinct templateset_id,order_no
                                                       from template_item
                                                      where form_id =
                                                               'frmBultrasoundWorkStation'
                                                        and control_id =
                                                               'm_txtPatholoyDiagnoses') b on ts.set_id =
                                                                                          b.templateset_id
                 join templateset_dept tsd on ts.set_id = tsd.set_id
           where ts.form_id = 'frmBultrasoundWorkStation'
             and ts.visiblity_level = 2
             and tsd.department_id = ?
             and ts.end_date > sysdate
             and ts.keyword like '常用值--%') a
join template_path b on a.keyword || '>>' like b.fullpath || '>>%'
                                     order by  b.order_no, a.order_no";
                m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = m_strEmpID;
                m_objDataParaArr[1].Value = m_strDeptID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTableB, m_objDataParaArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #region sql
        private const string c_strGetTemplateBySetID = @"select   a.template_id, a.content, a.contentxml, a.templateset_id, a.form_id,
         a.control_id, a.order_no, b.control_desc
    from template_item a left join gui_control b on a.form_id = b.form_id
                                               and a.control_id = b.control_id
   where templateset_id = ?
   order by order_no, template_id";
        #endregion
        [AutoComplete]
        public long m_lngGetTemplateArr(string strTemplateSetID, out clsTemplate[] arrTemplate)
        {
            arrTemplate = null;
            long ret = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = c_strGetTemplateBySetID;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strTemplateSetID;

                DataTable objDT = new DataTable();
                ret = objHRPServ.lngGetDataTableWithParameters(strSql, ref objDT, objDPArr);
                if (ret > 0)
                {
                    if (objDT.Rows.Count > 0)
                    {
                        arrTemplate = new clsTemplate[objDT.Rows.Count];
                        for (int i = 0; i < arrTemplate.Length; i++)
                        {
                            arrTemplate[i] = new clsTemplate();
                            arrTemplate[i].m_strTemplateID = objDT.Rows[i]["Template_ID"].ToString().Trim();
                            arrTemplate[i].m_strContent = objDT.Rows[i]["Content"].ToString().Trim();
                            arrTemplate[i].m_strContentXml = objDT.Rows[i]["ContentXml"].ToString().Trim();
                            arrTemplate[i].m_strTemplateSetID = objDT.Rows[i]["TemplateSet_ID"].ToString().Trim();
                            arrTemplate[i].m_strFormID = objDT.Rows[i]["Form_ID"].ToString().Trim();
                            arrTemplate[i].m_strControlID = objDT.Rows[i]["Control_ID"].ToString().Trim();
                            arrTemplate[i].m_intOrderNo = Int32.Parse(objDT.Rows[i]["Order_No"].ToString());
                            arrTemplate[i].m_strControlDesc = objDT.Rows[i]["Control_Desc"].ToString().Trim();
                        }
                    }
                    else
                    {
                        arrTemplate = new clsTemplate[0];
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ.Dispose();
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngGetBultraSoundTypeID(string p_strAttach, out string p_strTypeID)
        {
            long ret = 0;
            p_strTypeID = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select typeid from t_aid_apply_rlt where attachtype_int in ([id])";
                strSql = strSql.Replace("[id]", p_strAttach);
                DataTable objDT = new DataTable();
                ret = objHRPServ.lngGetDataTableWithoutParameters(strSql, ref objDT);
                if (ret > 0 && objDT != null)
                {
                    foreach (DataRow row in objDT.Rows)
                    {
                        p_strTypeID += (row[0].ToString() + ",");
                    }
                }
                if (p_strTypeID.Length > 1)
                {
                    p_strTypeID = p_strTypeID.Substring(0, p_strTypeID.Length - 1);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// 获取影像科室列表
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoomList(ref List<clsBultraSoundRoomInfo> roomList)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSql = "select t.roomname, t.hostname from t_ar_roomlist t ";
            DataTable dtValue = null;
            roomList = new List<clsBultraSoundRoomInfo>();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dtValue);
                if (lngRes > 0 && dtValue.Rows.Count > 0)
                {
                    foreach (DataRow row in dtValue.Rows)
                    {
                        clsBultraSoundRoomInfo room = new clsBultraSoundRoomInfo();
                        room.m_strRoomName = (row[0] == null) ? "" : row[0].ToString();
                        room.m_strHostName = (row[1] == null) ? "" : row[1].ToString();
                        roomList.Add(room);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetReportData(int p_intStatus, DateTime p_dtmBegin, DateTime p_dtmEnd, out DataTable p_dtValue)
        {
            string strSql = @"select count(t.reportid_chr), t.checkroom_chr
  from t_ris_us_report t
 where t.status_int = 1
   and t.checkdate_dat between
       to_date(?, 'yyyy-MM-dd hh24:mi:ss') and
       to_date(?, 'yyyy-MM-dd hh24:mi:ss')
 group by t.checkroom_chr order by t.checkroom_chr";
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_dtValue = null;
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_dtmBegin.ToString();
            objDPArr[1].Value = p_dtmEnd.ToString();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref p_dtValue, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }

        #region 申请单处理
        /// <summary>
        /// 获取当天申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetBultraSoundApplyList(string m_strFilter, string m_strBeginDate, string m_strEndDate, out List<clsBultraSoundApplyVO> p_objValues)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_objValues = null;
            long lngRes = 0;
            try
            {
                string strTypeID = string.Empty;
                m_lngGetBultraSoundTypeID("7,8", out strTypeID);
                string strSql = string.Empty;
                DataTable m_objTempTable = new DataTable();
                IDataParameter[] objLisAddItemRefArr = null;
                strSql = @"select   /*+ all_rows*/
         a.applyid, a.name, a.sex, a.age, b.status_int, b.sysfrom_int,
         a.applydate,a.cardno,a.bihno,a.tel,a.address,a.deptid_chr,b.isgreen_int
    from ar_common_apply a, t_opr_attachrelation b
   where a.typeid in ([type])
     and a.deleted = 0
     and a.submitted = 1
     and a.status_int = 0
     and a.applyid = b.attachid_vchr
     and b.attachtype_int = 1
     and a.applydate between ?
                         and ?
";
                if (m_strFilter != string.Empty)
                {
                    strSql = @"select   /*+ all_rows*/
         a.applyid, a.name, a.sex, a.age, b.status_int, b.sysfrom_int,
         a.applydate,a.cardno,a.bihno,a.tel,a.address,a.deptid_chr,b.isgreen_int
    from ar_common_apply a, t_opr_attachrelation b
   where a.typeid in ([type])
     and a.deleted = 0
     and a.submitted = 1
     and a.status_int = 0
     and a.applyid = b.attachid_vchr
     and b.attachtype_int = 1
     and a.applydate between ?
                         and ?
     and " + m_strFilter;
                }
                strSql = strSql.Replace("[type]", strTypeID);
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = DateTime.Parse(m_strBeginDate + " 00:00:00");
                objLisAddItemRefArr[1].Value = DateTime.Parse(m_strEndDate + " 23:59:59");
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objLisAddItemRefArr);
                p_objValues = new List<clsBultraSoundApplyVO>();
                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    string strCurrentApplyID = string.Empty;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        clsBultraSoundApplyVO vo = new clsBultraSoundApplyVO();
                        vo.m_strApplyID = row[0].ToString();
                        if (vo.m_strApplyID != strCurrentApplyID)
                        {
                            strCurrentApplyID = vo.m_strApplyID;
                            vo.m_strName = row[1].ToString();
                            vo.m_strSex = row[2].ToString();
                            vo.m_strAge = row[3].ToString();
                            vo.m_intStatus = Convert.ToInt32(row[4]);
                            vo.m_intSysFrom = Convert.ToInt32(row[5]);
                            vo.m_dtmApplyDate = Convert.ToDateTime(row[6]);
                            vo.m_strCardNo = row[7].ToString();
                            vo.m_strBihNo = row[8].ToString();
                            vo.m_strTel = row[9].ToString();
                            vo.m_strAddress = row[10].ToString();
                            vo.m_strDeptID = row[11].ToString();
                            int.TryParse(row[12].ToString().Trim(), out vo.m_intIsGreen);
                            p_objValues.Add(vo);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion
        #region 申请单处理
        /// <summary>
        /// 获取当天申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetBultraSoundApplyList(out string m_strUnPay, out string m_strReturn, string m_strDate, out List<clsBultraSoundApplyVO> p_objValues)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            long lngRes = 0;
            m_strReturn = string.Empty;
            m_strUnPay = string.Empty;
            p_objValues = null;
            objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable m_objTempTable = new DataTable();

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                string strTypeID = string.Empty;
                m_lngGetBultraSoundTypeID("7,8", out strTypeID);
                string strSql = @"select setstatus_int
            from t_sys_setting
            where setid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8014";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                double m_objDoubleDay = Convert.ToDouble(m_objTempTable.Rows[0][0].ToString());
                m_objDoubleDay--;
                string m_strBeginTime = DateTime.Now.AddDays(-m_objDoubleDay).ToShortDateString();

                m_objTempTable.Clear();
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8016";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                m_strUnPay = m_objTempTable.Rows[0][0].ToString();

                m_objTempTable.Clear();
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8017";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                m_strReturn = m_objTempTable.Rows[0][0].ToString();
                strSql = @"select   /*+ all_rows*/
         a.applyid, a.name, a.sex, a.age, b.status_int, b.sysfrom_int,
         a.applydate,a.cardno,a.bihno,a.tel,a.address,a.deptid_chr,b.isgreen_int
    from ar_common_apply a, t_opr_attachrelation b
   where a.typeid in ([type])
     and a.deleted = 0
     and a.submitted = 1
     and a.status_int = 0
     and a.applyid = b.attachid_vchr
     and b.attachtype_int = 1
     and a.applydate between ?
                         and ?
";
                strSql = strSql.Replace("[type]", strTypeID);
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = DateTime.Parse(m_strBeginTime + " 00:00:00");
                objLisAddItemRefArr[1].Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objLisAddItemRefArr);
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    p_objValues = new List<clsBultraSoundApplyVO>(dtValue.Rows.Count);
                    string strCurrentApplyID = string.Empty;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        clsBultraSoundApplyVO vo = new clsBultraSoundApplyVO();
                        vo.m_strApplyID = row[0].ToString();
                        if (vo.m_strApplyID != strCurrentApplyID)
                        {
                            strCurrentApplyID = vo.m_strApplyID;
                            vo.m_strName = row[1].ToString();
                            vo.m_strSex = row[2].ToString();
                            vo.m_strAge = row[3].ToString();
                            vo.m_intStatus = Convert.ToInt32(row[4]);
                            vo.m_intSysFrom = Convert.ToInt32(row[5]);
                            vo.m_dtmApplyDate = Convert.ToDateTime(row[6]);
                            vo.m_strCardNo = row[7].ToString();
                            vo.m_strBihNo = row[8].ToString();
                            vo.m_strTel = row[9].ToString();
                            vo.m_strAddress = row[10].ToString();
                            vo.m_strDeptID = row[11].ToString();
                            int.TryParse(row[12].ToString().Trim(), out vo.m_intIsGreen);
                            p_objValues.Add(vo);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion
        /// <summary>
        /// 依据申请单号读取项目明细
        /// </summary>
        /// <param name="dtRecords"></param>
        /// <param name="p_strApplyID"></param>
        [AutoComplete]
        public void m_mthGetApplyDetail(out System.Data.DataTable dtRecords, string p_strApplyID)
        {
            dtRecords = new DataTable();
            string strSQL = @"select applyid,
       cardno,
       bihno,
       name,
       sex,
       age,
       address,
       tel,
       diagnose,
       diagnosepart,
       diagnoseaim,
       department,
       doctorname,
       area,
       bedno,
       applydate,
       summary,
       chargedetail, t.deptid_chr,
       t.doctorid_chr,
       b.isgreen_int
  from ar_common_apply t,t_opr_attachrelation b
 where t.applyid = b.attachid_vchr(+)
 and t.applyid = ?
";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strApplyID;
                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRecords, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }


        #region 获取病理号前缀
        /// <summary>
        /// 获取病理号前缀
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetPatholoyPrefix(string strSetID)
        {
            long lngRes = 0;
            string strRet = "";
            string SQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";
            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = strSetID;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            if (lngRes > 0 && dt != null)
            {
                strRet = dt.Rows[0]["setstatus_int"].ToString();
                int it = int.Parse(strRet);
                strRet = System.Text.Encoding.ASCII.GetString(new byte[] { (byte)it });
            }
            return strRet;
        }
        #endregion
        #region 获取最大B超号（无前缀）
        /// <summary>
        ///  获取最大B超号（无前缀）
        /// </summary>
        /// <param name="strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMaxBultralSoundID(string strTableName, string strColName)
        {
            long lngRes = 0;
            string strRet = "";
            string SQL = @"select max_sequence_id_chr from t_aid_table_sequence_id 
							where lower(trim(table_name_vchr)) = ? and lower(trim(col_name_vchr)) = ?";

            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = strTableName.Trim().ToLower();
                objLisAddItemRefArr[1].Value = strColName.Trim().ToLower();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, objLisAddItemRefArr);
                if (lngRes > 0 && dt != null)
                {
                    strRet = dt.Rows[0]["max_sequence_id_chr"].ToString().Trim();

                    dt = null;
                    SQL = @"select count(*) nums from ar_content where controlid= 'm_txtBultraSoundID' and trim(ctl_content) =? ";
                    try
                    {

                        objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                        objLisAddItemRefArr = null;
                        objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                        objLisAddItemRefArr[0].Value = strRet;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, objLisAddItemRefArr);
                    }
                    catch { }
                    if (lngRes > 0 && dt != null)
                    {
                        string str = dt.Rows[0]["nums"].ToString().Trim();
                        if (str != "0")
                        {
                            strRet = "编号重复";
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return strRet;
        }
        #endregion
        #region 获取最大病理号（无前缀）
        /// <summary>
        /// 获取最大病理号（无前缀）
        /// </summary>
        /// <param name="strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMaxPathologyID(string strTableName, string strColName)
        {
            long lngRes = 0;
            string strRet = "";
            string SQL = @"select max_sequence_id_chr from t_aid_table_sequence_id 
							where lower(trim(table_name_vchr)) = lower(trim(?)) 
							  and lower(trim(col_name_vchr)) = lower(trim(?))";

            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = strTableName;
            objDPArr[1].Value = strColName;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, objDPArr);
                if (lngRes > 0 && dt != null)
                {
                    strRet = dt.Rows[0]["max_sequence_id_chr"].ToString().Trim();

                    dt = null;
                    SQL = @"select count (controlid) nums
                        from ar_content
                        where controlid = 'm_txtPathologyID' and ctl_content =?";
                    try
                    {

                        objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                        System.Data.IDataParameter[] m_objDataPara = null;
                        objHRPSvc.CreateDatabaseParameter(1, out m_objDataPara);
                        m_objDataPara[0].Value = strRet.Trim();
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, m_objDataPara);
                    }
                    catch { }
                    if (lngRes > 0 && dt != null)
                    {
                        string str = dt.Rows[0]["nums"].ToString().Trim();
                        if (str != "0")
                        {
                            strRet = "编号重复";
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return strRet;
        }
        #endregion

        #region 查看是否有相同的病理号的记录
        /// <summary>
        /// 查看是否有相同的病理号的记录
        /// </summary>
        /// <param name="strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMaxPathologyID(string p_strRECORDID, string p_strctl_content, out int p_resultCount)
        {
            p_resultCount = -1;
            long lngRes = 0;
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            string SQL = @"select count (*) nums
  from ar_content
 where lower (recordid) = ?
   and lower (controlid) = 'm_txtpathologyid'
   and trim (ctl_content) = ?";
            try
            {
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strRECORDID.ToLower();
                m_objDataParaArr[1].Value = p_strctl_content;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, m_objDataParaArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dt != null)
            {
                p_resultCount = Convert.ToInt32(dt.Rows[0]["nums"].ToString());
            }
            objHRPSvc.Dispose();

            return lngRes;
        }
        #endregion
        #region 查看是否有相同的B超号的记录
        /// <summary>
        /// 查看是否有相同的B超号的记录
        /// </summary>
        /// <param name="strSetID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMaxBultraSoundID(string p_strRECORDID, string p_strctl_content, out int p_resultCount)
        {
            p_resultCount = -1;
            long lngRes = 0;
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            string SQL = @"select count (*) nums
                           from ar_content
                           where lower (recordid) = ?
                           and lower (controlid) = 'm_txtbultrasoundid'
                           and trim (ctl_content) = ?";
            try
            {
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strRECORDID.ToLower();
                m_objDataParaArr[1].Value = p_strctl_content;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, m_objDataParaArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            if (lngRes > 0 && dt != null)
            {
                p_resultCount = Convert.ToInt32(dt.Rows[0]["nums"].ToString());
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngCheckBultraSoundID(string p_strBultraSoundID, out int p_resultCount)
        {
            p_resultCount = -1;
            long lngRes = 0;
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            string SQL = @"select count (*) nums
                           from ar_content
                           where controlid = 'm_txtBultraSoundID'
                           and trim (ctl_content) = ?";
            try
            {
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out m_objDataParaArr);
                m_objDataParaArr[0].Value = p_strBultraSoundID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, m_objDataParaArr);
                if (lngRes > 0 && dt != null)
                {
                    p_resultCount = Convert.ToInt32(dt.Rows[0]["nums"]);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }

            return lngRes;
        }
        #endregion


        #region 电子胃镜申请单处理
        /// <summary>
        /// 获取当天申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetEMWApplyList(out string m_strUnPay, out string m_strReturn, string m_strDate, out List<clsBultraSoundApplyVO> p_objValues)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            long lngRes = 0;
            m_strReturn = string.Empty;
            m_strUnPay = string.Empty;
            p_objValues = null;
            objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable m_objTempTable = new DataTable();

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                string strTypeID = string.Empty;
                //
                string strSql = @"select setstatus_int
            from t_sys_setting
            where setid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8014";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                double m_objDoubleDay = Convert.ToDouble(m_objTempTable.Rows[0][0].ToString());
                m_objDoubleDay--;
                string m_strBeginTime = DateTime.Now.AddDays(-m_objDoubleDay).ToShortDateString();

                m_objTempTable.Clear();
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8016";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                m_strUnPay = m_objTempTable.Rows[0][0].ToString();

                m_objTempTable.Clear();
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = "8017";
                objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_objTempTable, objLisAddItemRefArr);
                m_strReturn = m_objTempTable.Rows[0][0].ToString();
                strSql = @"select   /*+ all_rows*/
         a.applyid, a.name, a.sex, a.age, b.status_int, b.sysfrom_int,
         a.applydate,a.cardno,a.bihno,a.tel,a.address,a.deptid_chr
    from ar_common_apply a, t_opr_attachrelation b
   where a.typeid in ([type])
     and a.deleted = 0
     and a.submitted = 1
     and a.status_int = 0
     and a.applyid = b.attachid_vchr
     and b.attachtype_int = 1
     and a.applydate between ?
                         and ?
";
                strSql = strSql.Replace("[type]", "7");
                objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = DateTime.Parse(m_strBeginTime + " 00:00:00");
                objLisAddItemRefArr[1].Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                DataTable dtValue = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtValue, objLisAddItemRefArr);
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    p_objValues = new List<clsBultraSoundApplyVO>(dtValue.Rows.Count);
                    string strCurrentApplyID = string.Empty;
                    foreach (DataRow row in dtValue.Rows)
                    {
                        clsBultraSoundApplyVO vo = new clsBultraSoundApplyVO();
                        vo.m_strApplyID = row[0].ToString();
                        if (vo.m_strApplyID != strCurrentApplyID)
                        {
                            strCurrentApplyID = vo.m_strApplyID;
                            vo.m_strName = row[1].ToString();
                            vo.m_strSex = row[2].ToString();
                            vo.m_strAge = row[3].ToString();
                            vo.m_intStatus = Convert.ToInt32(row[4]);
                            vo.m_intSysFrom = Convert.ToInt32(row[5]);
                            vo.m_dtmApplyDate = Convert.ToDateTime(row[6]);
                            vo.m_strCardNo = row[7].ToString();
                            vo.m_strBihNo = row[8].ToString();
                            vo.m_strTel = row[9].ToString();
                            vo.m_strAddress = row[10].ToString();
                            vo.m_strDeptID = row[11].ToString();
                            p_objValues.Add(vo);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 获得指定时间段内的电子胃镜病人列表
        /// <summary>
        ///获得指定时间段内的B超病人列表
        /// </summary>
        [AutoComplete]
        public void m_mthGetEleMicPatientList(string p_strBeginData, string p_strEndDate, string p_strSendStatus, out System.Data.DataTable p_dtRecords)
        {
            p_dtRecords = new DataTable();
            string SQL = string.Empty;
            string strDay = string.Empty;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strSendStatus == "0")//B超登记人员名单显示天数1--99, 1为显示当天
                {   //已完成的只显示当天的时间
                    string SQL1 = @"select setstatus_int
  from t_sys_setting
 where setid_chr = '8012' and moduleid_chr = '0008'";
                    System.Data.DataTable dtRecords = new DataTable();
                    objHRPSvc.lngGetDataTableWithoutParameters(SQL1, ref dtRecords);
                    if (dtRecords.Rows.Count == 1)
                        strDay = dtRecords.Rows[0][0].ToString();
                }
                else if (p_strSendStatus == "1")//B超完成检查名单显示天数1--99, 1为显示当天
                {
                    string SQL1 = @"select setstatus_int
  from t_sys_setting
 where setid_chr = '8013' and moduleid_chr = '0008'";
                    System.Data.DataTable dtRecords = new DataTable();
                    objHRPSvc.lngGetDataTableWithoutParameters(SQL1, ref dtRecords);
                    if (dtRecords.Rows.Count == 1)
                        strDay = dtRecords.Rows[0][0].ToString();
                }
                IDataParameter[] objDPArr = null;
                if (99 >= Convert.ToInt32(strDay) && Convert.ToInt32(strDay) > 0)
                {
                    SQL = @"select   recordid, ctl_content bultrasoundid
    from ar_content
   where controlid = 'm_txtBultraSoundID'
     and recordid in (
            select recordid
              from ar_apply_report
             where sendstatus = ?
               and delstatus = 0
                and formclsname='frmEleMicroscopeWorkStation'
               and ceateddate
                      between ? and ?)
order by bultrasoundid";
                    objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = Convert.ToInt32(p_strSendStatus);
                    DateTime dtmFrom = DateTime.Parse(DateTime.Now.AddDays(1 - Convert.ToInt32(strDay)).ToString("yyyy-MM-dd 00:00:00"));
                    DateTime dtmTo = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                    objDPArr[1].Value = dtmFrom;
                    objDPArr[2].Value = dtmTo;
                }
                else
                {
                    SQL = @"select   recordid, ctl_content bultrasoundid
    from ar_content
   where controlid = 'm_txtBultraSoundID'
     and recordid in (
                  select recordid
                    from ar_apply_report
                   where sendstatus = ?
                        and formclsname='frmEleMicroscopeWorkStation'
                         and delstatus = 0)
order by bultrasoundid
";
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = Convert.ToInt32(p_strSendStatus);
                }
                long lngRes = -1;

                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref p_dtRecords, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
        }
        #endregion

        #region 电子胃镜内镜号
        [AutoComplete]
        public long m_mthGetEleMicMum(out string p_strEleMicNum)
        {
            long lngRes = 0;
            p_strEleMicNum = string.Empty;
            DataTable m_objDataTable = null;
            DataTable m_dtTemp = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {

                // string strSql = @"select substr (seq_bultrasoundid.nextval, -3) as bultrasoundid  from dual";
                #region 查找最大序列号尾数语句 刘浩景 2008-3-24 修改

                //                string strSql = @"select max (substr (a.ctl_content, -3)) + 1 as bultrasoundid 
                //                                  from ar_content a where a.controlid = 'm_txtBultraSoundID'
                //                                  and substr (a.ctl_content, 0, 8) =(select to_char (sysdate, 'yyyymmdd') from dual)
                //                                 ";

                string strSql = @"select max(substr(a.otherid_chr, -3)) + 1 as bultrasoundid
  from t_ris_es_report a
 where a.checkdate_dat between trunc(sysdate) and (trunc(sysdate) + 1)";
                #endregion

                lngRes = objHRPServer.lngGetDataTableWithoutParameters(strSql, ref m_objDataTable);
                lngRes = objHRPServer.lngGetDataTableWithoutParameters("select to_char(sysdate, 'yyyymmdd') as currentday from dual", ref m_dtTemp);
                if (lngRes > 0 && m_objDataTable.Rows.Count > 0)
                {
                    string m_strTemp = m_objDataTable.Rows[0]["bultrasoundid"].ToString().Trim();
                    if (m_strTemp == string.Empty)
                    {
                        p_strEleMicNum = m_dtTemp.Rows[0]["currentday"].ToString() + "001";
                    }
                    else
                    {
                        p_strEleMicNum = m_dtTemp.Rows[0]["currentday"].ToString() + m_strTemp.PadLeft(3, '0');
                    }
                }
                else
                {
                    p_strEleMicNum = m_dtTemp.Rows[0]["currentday"].ToString() + "001";
                }

                objHRPServer.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServer.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion
    }
}
