using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.ICU_Service
{
    /// <summary>
    /// ICU 120
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsICU_120Service : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取指定病区/科室下有120记录的病人

        /// <summary>
        /// 获取指定病区/科室下有120记录的病人

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">病区/科室ID</param>
        /// <param name="p_dtmInDateStart">查询开始时间</param>
        /// <param name="p_dtmInDateEnd">查询结束时间</param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGet120PatientInArea( 
            string p_strID,DateTime p_dtmInDateStart, DateTime p_dtmInDateEnd , out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLBy = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQLBy = " right('0000'+rtrim(code_chr),4) sequenceno";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQLBy = " lpad(rtrim(t.code_chr),4,'0') sequenceno ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQLBy = " right('0000'||rtrim(t.code_chr),4) sequenceno ";

                }
                string strSQL = @"select icu.ICU_120_ID,info.REGISTERDATE,
       t.code_chr,
       d.bedid_chr,
       d.patientid_chr,
       a.*,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       d.state_int,
       d.PSTATUS_INT,
       s.PATIENTPIC_GRP,
       " + strSQLBy + @"
  from t_bse_bed t
 inner join T_OPR_BIH_REGISTER d on t.bedid_chr = d.bedid_chr and d.STATUS_INT = 1
 inner join t_opr_bih_registerdetail a on d.REGISTERID_CHR =
                                          a.REGISTERID_CHR
 inner join ICU_120_BIH_RELATION icu on icu.patientid_chr =
                                        d.patientid_chr
 inner join icu_120registerinfo info on info.ICU_120_ID = 
                                        icu.ICU_120_ID
  left outer join t_bse_patientsign s on d.patientid_chr = s.patientid_chr
 where t.areaid_chr = ? and d.inpatient_dat between ? and ?
 order by d.inpatient_dat desc, sequenceno";
                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_dtmInDateStart.ToString("yyyy-MM-dd"));
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = Convert.ToDateTime(p_dtmInDateEnd.ToString("yyyy-MM-dd"));
                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //释放
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 根据指定查询号得到有120记录的病人

        /// <summary>
        /// 根据指定查询号得到有120记录的病人

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strNO">查询号</param>
        /// <param name="p_intType">查询号类型0,住院号;1,诊疗卡号</param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGet120PatientByN0(
            string p_strNO,int p_intType, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            if (string.IsNullOrEmpty(p_strNO))
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLBy = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQLBy = " right('0000'+rtrim(code_chr),4) sequenceno";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQLBy = " lpad(rtrim(t.code_chr),4,'0') sequenceno ";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQLBy = " right('0000'||rtrim(t.code_chr),4) sequenceno ";

                }

                string strSQLNO = string.Empty;
                if (p_intType == 0)
                {
                    strSQLNO = " d.inpatientid_chr = ?";
                }
                else if (p_intType == 1)
                {
                    strSQLNO = " c.PATIENTCARDID_CHR = ?";
                }
                else
                {
                    return -1;
                }

                string strSQL = @"select icu.ICU_120_ID,info.REGISTERDATE,
       t.code_chr,
       d.bedid_chr,
       d.patientid_chr,
       a.*,
       d.registerid_chr,
       d.inpatientid_chr,
       d.inpatient_dat,
       d.state_int,
       d.PSTATUS_INT,
       s.PATIENTPIC_GRP,
       " + strSQLBy + @"
  from t_bse_bed t
 inner join T_OPR_BIH_REGISTER d on t.bedid_chr = d.bedid_chr and d.STATUS_INT = 1
 inner join t_opr_bih_registerdetail a on d.REGISTERID_CHR =
                                          a.REGISTERID_CHR
 inner join ICU_120_BIH_RELATION icu on icu.patientid_chr =
                                        d.patientid_chr
 inner join icu_120registerinfo info on info.ICU_120_ID = 
                                        icu.ICU_120_ID
 inner join t_bse_patientcard c on c.patientid_chr = d.patientid_chr
  left outer join t_bse_patientsign s on d.patientid_chr = s.patientid_chr
 where " + strSQLNO +@"
 order by d.inpatient_dat desc, sequenceno";
                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strNO.Trim();
                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0)
                    p_dtbValue = dtResult;
                //释放
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取截图
        /// <summary>
        /// 获取截图
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_str120ID">120ID</param>
        /// <param name="p_arrPic"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCutPic(string p_str120ID, out DataTable p_dtbPic)
        {
            p_dtbPic = null;
            if (string.IsNullOrEmpty(p_str120ID))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                string strSQL = "select t.* from icu_120cutpic t where t.icu_120_id = ? order by t.id";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_str120ID.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbPic, objDPArr);                
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取转运资料
        /// <summary>
        /// 获取转运资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_str120ID">120ID</param>
        /// <param name="p_dtbRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTransportRecord( string p_str120ID, out DataTable p_dtbRecord)
        {
            p_dtbRecord = null;
            if (string.IsNullOrEmpty(p_str120ID))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                string strSQL = @"select t.*
  from ICU_120TRANSPORTRECORD t
 where t.icu_120_id = ?
   and t.FORM_NAME = 'frmDangerBabyRecord'";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_str120ID.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取波形
        /// <summary>
        /// 获取波形
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_str120ID">120ID</param>
        /// <param name="p_dtbRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCurveData( string p_str120ID, out DataTable p_dtbRecord)
        {
            p_dtbRecord = null;
            if (string.IsNullOrEmpty(p_str120ID))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                string strSQL = @"select a.icu_120_id,a.registerdate, b.*
  from ICU_120REGISTERINFO a
 inner join GECURVEDATA b on a.monitorip = b.monitor_ip
 where a.icu_120_id = ?
   and b.datacollectedtime between a.registerdate and
       (select nvl(min(re.registerdate),sysdate)
          from ICU_120REGISTERINFO re
         where re.icu_120_id > ?
           and re.monitorip = a.monitorip)";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值

                objDPArr[0].Value = p_str120ID.Trim();
                objDPArr[1].Value = p_str120ID.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbRecord, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取最近一次的影像检查号
        /// <summary>
        /// 获取最近一次的影像检查号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="p_strCheckID">检查号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLatestCheckIDByPatient( string p_strPatientID, out string p_strCheckID)
        {
            p_strCheckID = null;
            if (string.IsNullOrEmpty(p_strPatientID))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                string strSQL = @"select t.ORDER_NO_CHR
  from imagereport t
 where t.patientid = ?
   and t.order_no_chr is not null
 order by t.ADUITDATE desc";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                DataTable dtbRecord = new DataTable();
                //赋值

                objDPArr[0].Value = p_strPatientID.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbRecord, objDPArr);

                if (lngRes > 0 && dtbRecord != null && dtbRecord.Rows.Count > 0)
                {
                    p_strCheckID = dtbRecord.Rows[0]["ORDER_NO_CHR"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 根据检查号获取序列号列表

        /// <summary>
        /// 根据检查号获取序列号列表

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">检查号</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPACSPicSeriesuidByCheckID(
            string p_strPatientID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strPatientID))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            objHRPServ.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytPACS;

            try
            {
                string strSQL = @"SELECT a.patientid,
       b.studyuid,
       b.studydate,
       b.studytime,
       c.seriesuid,
       c.modality,
       c.SERIESDESCRIPTION,
       c.SERIESNUMBER
  FROM icare.patients a, icare.series c, icare.studies b
 WHERE (a.patientid = b.patientid)
   AND (b.studyuid = c.studyuid)
   AND a.patientid = ?
   and b.studydate = (select max(studydate)
                        from icare.studies
                       where studyuid = b.studyuid
                         and patientid = b.patientid)
 order by TO_NUMBER(c.seriesnumber), c.modality";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strPatientID.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 根据检查号、影像类别、序列号获取图片
        /// <summary>
        /// 根据检查号、影像类别、序列号获取图片
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckID">检查号</param>
        /// <param name="p_strModality">影像类别</param>
        /// <param name="p_strSeriesuid">序列号</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPACSPicBySeriesuid(
            string p_strCheckID,string p_strModality,string p_strSeriesuid, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strCheckID) || string.IsNullOrEmpty(p_strModality) || string.IsNullOrEmpty(p_strSeriesuid))
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            objHRPServ.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytPACS;

            try
            {
                string strSQL = @"SELECT a.patientid,
       b.studyuid,
       b.studydate,
       b.studytime,
       c.seriesuid,
       c.modality,
       d.instanceuid,
       d.smallimage,
       d.image
  FROM icare.patients a, icare.series c, icare.studies b, icare.images d
 WHERE (a.patientid = b.patientid)
   AND (b.studyuid = c.studyuid)
   AND a.patientid = ?
   AND c.modality = ?
   and c.seriesuid = d.seriesuid
   and d.seriesuid = ?
 order by to_Number(d.imagenumber)";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strCheckID.Trim();
                objDPArr[1].Value = p_strModality.Trim();
                objDPArr[2].Value = p_strSeriesuid.Trim();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion
    }
}
