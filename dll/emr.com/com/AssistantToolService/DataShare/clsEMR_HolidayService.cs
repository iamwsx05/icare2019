using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.DataShareService
{
    /// <summary>
    /// 假期、值班时间设置
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_HolidayService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取假期、值班时间设置
        /// </summary>
        /// <param name="p_dtbData">数据(直接返回DataTable，在界面根据类型作筛选)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHolidaySet(out DataTable p_dtbData)
        {
            p_dtbData = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select holidayid_int, begindate_dat, enddate_dat, type_int, status_int
  from t_emr_holiday
 where status_int = 1
 order by begindate_dat";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbData);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取日常工作时间设置
        /// </summary>
        /// <param name="p_dtbData">数据(直接返回DataTable，在界面根据类型作筛选)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWorkingHours(out DataTable p_dtbData)
        {
            p_dtbData = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.workingid,
       t.ambegintime,
       t.amendtime,
       t.pmbegintime,
       t.pmendtime,
       t.type_int,
       t.status_int
  from t_emr_setworkinghours t
 where t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbData);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 删除指定的假期日期
        /// </summary>
        /// <param name="p_lngSetID">主键ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteHolidaySet(long p_lngSetID)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_holiday set status_int = 0 where holidayid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSetID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 添加假期、值班时间设置
        /// </summary>
        /// <param name="p_objHoliday">假期、值班时间设置</param>
        /// <param name="p_lngSetID">主键ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddHolidaySet(clsEMR_HolidayValue p_objHoliday, out long p_lngSetID)
        {
            p_lngSetID = 1;

            if (p_objHoliday == null)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select max(holidayid_int) from t_emr_holiday";

                long lngSetID = 0;
                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbValue);
                if (dtbValue != null && dtbValue.Rows.Count == 1)
                {
                    long lngTemp = 0;
                    if (long.TryParse(dtbValue.Rows[0][0].ToString(), out lngTemp))
                    {
                        lngSetID = lngTemp + 1;
                    }
                    else
                    {
                        lngSetID = 1;
                    }
                }
                else
                {
                    lngSetID = 1;
                }

                p_lngSetID = lngSetID;

                strSQL = @"insert into t_emr_holiday
  (holidayid_int, begindate_dat, enddate_dat, type_int, STATUS_INT)
values
  (?, ?, ?, ?, ?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = lngSetID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objHoliday.m_dtmBEGINDATE_DAT;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objHoliday.m_dtmENDDATE_DAT;
                objDPArr[3].Value = p_objHoliday.m_intTYPE_INT;
                objDPArr[4].Value = 1;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 添加日常工作时间设置
        /// </summary>
        /// <param name="p_objWorkArr">日常工作时间设置</param>
        /// <param name="p_lngSetID">主键ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddWorkingHoursSet(clsEMR_WorkingHoursSet[] p_objWorkArr, out long[] p_lngSetID)
        {
            p_lngSetID = null;

            if (p_objWorkArr == null || p_objWorkArr.Length == 0)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select max(workingid) from t_emr_setworkinghours";

                long lngSetID = 0;
                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbValue);
                if (dtbValue != null && dtbValue.Rows.Count == 1)
                {
                    long lngTemp = 0;
                    if (long.TryParse(dtbValue.Rows[0][0].ToString(), out lngTemp))
                    {
                        lngSetID = lngTemp + 1;
                    }
                    else
                    {
                        lngSetID = 1;
                    }
                }
                else
                {
                    lngSetID = 1;
                }

                DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Int32 };
                object[][] objValues = new object[7][];

                p_lngSetID = new long[p_objWorkArr.Length];

                strSQL = @"insert into t_emr_setworkinghours
  (workingid,
   ambegintime,
   amendtime,
   pmbegintime,
   pmendtime,
   type_int,
   status_int)
values
  (?, ?, ?, ?, ?, ?, ?)";

                if (p_objWorkArr.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objWorkArr.Length];//初始化
                    }

                    for (int k1 = 0; k1 < p_objWorkArr.Length; k1++)
                    {
                        p_lngSetID[k1] = lngSetID;
                        lngSetID++;
                        objValues[0][k1] = p_lngSetID[k1];
                        objValues[1][k1] = p_objWorkArr[k1].m_strAMBEGINTIME;
                        objValues[2][k1] = p_objWorkArr[k1].m_strAMENDTIME;
                        objValues[3][k1] = p_objWorkArr[k1].m_strPMBEGINTIME;
                        objValues[4][k1] = p_objWorkArr[k1].m_strPMENDTIME;
                        objValues[5][k1] = p_objWorkArr[k1].m_intTYPE_INT;
                        objValues[6][k1] = p_objWorkArr[k1].m_intSTATUS_INT;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    objHRPServ = null;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 修改工作日值班时间
        /// </summary>
        /// <param name="p_lngSetID">主键ID</param>
        /// <param name="p_dtmBeginTime">值班开始时间</param>
        /// <param name="p_dtmEndTime">值班结束时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDutyTime(long p_lngSetID, DateTime p_dtmBeginTime, DateTime p_dtmEndTime)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_holiday
   set begindate_dat = ?, enddate_dat = ?
 where holidayid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmBeginTime;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEndTime;
                objDPArr[2].Value = p_lngSetID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 修改日常工作时间设置
        /// </summary>
        /// <param name="p_objWorkArr">日常工作时间设置</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyWorkingHoursSet(clsEMR_WorkingHoursSet[] p_objWorkArr)
        {
            if (p_objWorkArr == null || p_objWorkArr.Length == 0)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32 };
                object[][] objValues = new object[5][];

                string strSQL = @"update t_emr_setworkinghours
   set ambegintime = ?, amendtime = ?, pmbegintime = ?, pmendtime = ?
 where workingid = ?
   and status_int = 1";

                if (p_objWorkArr.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objWorkArr.Length];//初始化
                    }

                    for (int k1 = 0; k1 < p_objWorkArr.Length; k1++)
                    {
                        objValues[0][k1] = p_objWorkArr[k1].m_strAMBEGINTIME;
                        objValues[1][k1] = p_objWorkArr[k1].m_strAMENDTIME;
                        objValues[2][k1] = p_objWorkArr[k1].m_strPMBEGINTIME;
                        objValues[3][k1] = p_objWorkArr[k1].m_strPMENDTIME;
                        objValues[4][k1] = p_objWorkArr[k1].m_intWORKINGID;
                    }
                    clsHRPTableService objHRPServ = new clsHRPTableService();
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    objHRPServ = null;
                    p_objWorkArr = null;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 检查是否已经设置该日期(只针对假期设置与补假上班日期设置)
        /// </summary>
        /// <param name="p_dtmSetDate">新设置日期列表</param>
        /// <param name="p_intType">设置日期类型</param>
        /// <param name="p_blnHasSet">是否已设置</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckHasSetDate(DateTime[] p_dtmSetDate, int p_intType, out bool p_blnHasSet)
        {
            p_blnHasSet = false;
            if (p_dtmSetDate == null || p_dtmSetDate.Length == 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select t.begindate_dat,t.enddate_dat
  from t_emr_holiday t
   where t.type_int = ?
   and t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_intType;

                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ = null;

                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    List<DateTime> lstDate = new List<DateTime>();
                    DataRow drRow = null;
                    for (int iRow = 0; iRow < dtbValue.Rows.Count; iRow++)
                    {
                        drRow = dtbValue.Rows[iRow];
                        DateTime dtmBegin = Convert.ToDateTime(drRow["begindate_dat"]);
                        DateTime dtmEnd = Convert.ToDateTime(drRow["enddate_dat"]);
                        if (dtmBegin.Date == dtmEnd.Date)
                        {
                            lstDate.Add(dtmBegin.Date);
                        }
                        else
                        {
                            while (dtmBegin <= dtmEnd)
                            {
                                lstDate.Add(dtmBegin);
                                dtmBegin = dtmBegin.AddDays(1);
                            }
                        }
                    }

                    if (lstDate.Count > 0)
                    {
                        for (int iSet = 0; iSet < p_dtmSetDate.Length; iSet++)
                        {
                            if (lstDate.IndexOf(p_dtmSetDate[iSet].Date) >= 0)
                            {
                                p_blnHasSet = true;
                                break;
                            }
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
            return lngRes;
        }
    }
}
