using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsWindowsCortrol : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ȡ��ǰ����ҩ����(Ҳ���Ǽ��ҩ����ҩ��������ҩ������С�Ĵ��ڣ�
        /// <summary>
        /// ��ȡ��ǰ����ҩ����(Ҳ���Ǽ��ҩ����ҩ��������ҩ������С�Ĵ��ڣ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID">����ҩ��</param>
        /// <param name="windowsID">������ҩ����</param>
        /// <param name="WaiteNO">���ش��ڶ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetWindowIDByStorage( string storageID, out string windowsID, out int WaiteNO, bool CheckScope)
        {
//            windowsID = "";
//            WaiteNO = 1;
//            long lngRegs = 0;
//            //Ȩ����
//            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
//            //����Ƿ���ʹ��Щ������Ȩ��
//            lngRegs = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "com.digitalwave.iCare.middletier.HIS.clsReckoningReport", "lngGetWindowIDByStorage");
//            if (lngRegs < 0) //û��ʹ�õ�Ȩ��
//            {
//                return -1;
//            }
//            string strSQL = "";
//            if (CheckScope)
//            {
//                strSQL = @"select   a.windowid_chr, a.medstoreid_chr,
//         decode (k.intcount, null, 0, k.intcount) as intcount1,
//         decode (k.intcount3, null, 0, k.intcount3) as intcount4
//    from t_bse_medstorewin a,
//         (select intcount, intcount3, b.windowid_chr, b.medstoreid_chr
//            from (select   max (order_int) as intcount, windowid_chr,
//                           medstoreid_chr
//                      from t_opr_medstorewinque
//                     where medstoreid_chr = ?
//                       and windowtype_int = 1
//                       and outpatrecipeid_chr like ?
//                  group by medstoreid_chr, windowid_chr) b,
//                 (select   count (order_int) as intcount3, windowid_chr,
//                           medstoreid_chr
//                      from t_opr_medstorewinque
//                     where medstoreid_chr = ?
//                       and windowtype_int = 1
//                       and outpatrecipeid_chr like ?
//                  group by medstoreid_chr, windowid_chr) c
//           where b.medstoreid_chr = c.medstoreid_chr
//             and b.windowid_chr = c.windowid_chr) k
//   where a.medstoreid_chr = ?
//     and a.windowtype_int = 1
//     and a.workstatus_int = 1
//     and a.medstoreid_chr = k.medstoreid_chr(+)
//     and a.windowid_chr = k.windowid_chr(+)
//order by intcount4
//";
//            }
//            else
//            {
//                strSQL = @"select   a.windowid_chr, a.medstoreid_chr,
//         decode (k.intcount, null, 0, k.intcount) as intcount1,
//          decode (k.intcount3, null, 0, k.intcount3) as intcount4
//    from t_bse_medstorewin a,
//         (select intcount, intcount3, b.windowid_chr, b.medstoreid_chr
//            from (select   max (order_int) as intcount, windowid_chr,
//                           medstoreid_chr
//                      from t_opr_medstorewinque
//                     where medstoreid_chr = ?
//                       and windowtype_int = 1
//                       and outpatrecipeid_chr like ?
//                  group by medstoreid_chr, windowid_chr) b,
//                 (select   count (order_int) as intcount3, windowid_chr,
//                           medstoreid_chr
//                      from t_opr_medstorewinque
//                     where medstoreid_chr = ?
//                       and windowtype_int = 1
//                       and outpatrecipeid_chr like ?
//                  group by medstoreid_chr, windowid_chr) c
//           where b.medstoreid_chr = c.medstoreid_chr
//             and b.windowid_chr = c.windowid_chr) k
//   where a.medstoreid_chr = ?
//     and a.winproperty_int = 0
//     and a.windowtype_int = 1
//     and a.workstatus_int = 1
//     and a.medstoreid_chr = k.medstoreid_chr(+)
//     and a.windowid_chr = k.windowid_chr(+)
//order by intcount4";
//            }
//            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
//            string strDateTime = objDate.m_GetServerDate().ToString("yyyyMMdd");
//            try
//            {
//                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
//                System.Data.DataTable p_dtWindow = new System.Data.DataTable();

//                System.Data.IDataParameter[] paramArr = null;
//                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
//                paramArr[0].Value = storageID.Trim();
//                paramArr[1].Value = strDateTime.Trim() + "%";
//                paramArr[2].Value = storageID.Trim();
//                paramArr[3].Value = strDateTime.Trim() + "%";
//                paramArr[4].Value = storageID.Trim();
//                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);

//                if (p_dtWindow.Rows.Count > 0)
//                {
//                    windowsID = p_dtWindow.Rows[0]["WINDOWID_CHR"].ToString();
//                    WaiteNO = int.Parse(p_dtWindow.Rows[0]["intcount1"].ToString()) + 1;
//                }
//            }
//            catch (Exception objEx)
//            {
//                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
//                bool blnRes = objLogger.LogError(objEx);
//            }
//            return lngRegs;
            windowsID = "";
            WaiteNO = 1;
            long lngRegs = 0; 
            string strSQL = "";
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyyMMdd");
            if (CheckScope)
            {
                strSQL = @"select   a.windowid_chr, a.medstoreid_chr,
                     decode (k.intcount, null, 0, k.intcount) as intcount1,
                     decode (k.intcount3, null, 0, k.intcount3) as intcount4
                from t_bse_medstorewin a,
                     (select intcount, intcount3, b.windowid_chr, b.medstoreid_chr
                        from (select   max (order_int) as intcount, windowid_chr,
                                       medstoreid_chr
                                  from t_opr_medstorewinque
                                 where medstoreid_chr = '" + storageID.Trim() + @"'
                                   and windowtype_int = 1
                                   and outpatrecipeid_chr like '" + strDateTime + @"%'
                              group by medstoreid_chr, windowid_chr) b,
                             (select   count (order_int) as intcount3, windowid_chr,
                                       medstoreid_chr
                                  from t_opr_medstorewinque
                                 where medstoreid_chr = '" + storageID.Trim() + @"'
                                   and windowtype_int = 1
                                   and outpatrecipeid_chr like '" + strDateTime + @"%'
                              group by medstoreid_chr, windowid_chr) c
                       where b.medstoreid_chr = c.medstoreid_chr
                         and b.windowid_chr = c.windowid_chr) k
               where a.medstoreid_chr = '" + storageID.Trim() + @"'
                 and a.windowtype_int = 1
                 and a.workstatus_int = 1
                 and a.medstoreid_chr = k.medstoreid_chr(+)
                 and a.windowid_chr = k.windowid_chr(+)
            order by intcount4
            ";
            }
            else
            {
                strSQL = @"select   a.windowid_chr, a.medstoreid_chr,
                     decode (k.intcount, null, 0, k.intcount) as intcount1,
                      decode (k.intcount3, null, 0, k.intcount3) as intcount4
                from t_bse_medstorewin a,
                     (select intcount, intcount3, b.windowid_chr, b.medstoreid_chr
                        from (select   max (order_int) as intcount, windowid_chr,
                                       medstoreid_chr
                                  from t_opr_medstorewinque
                                 where medstoreid_chr = '" + storageID.Trim() + @"'
                                   and windowtype_int = 1
                                   and outpatrecipeid_chr like '" + strDateTime + @"%'
                              group by medstoreid_chr, windowid_chr) b,
                             (select   count (order_int) as intcount3, windowid_chr,
                                       medstoreid_chr
                                  from t_opr_medstorewinque
                                 where medstoreid_chr = '" + storageID.Trim() + @"'
                                   and windowtype_int = 1
                                   and outpatrecipeid_chr like '" + strDateTime + @"%'
                              group by medstoreid_chr, windowid_chr) c
                       where b.medstoreid_chr = c.medstoreid_chr
                         and b.windowid_chr = c.windowid_chr) k
               where a.medstoreid_chr = '" + storageID.Trim() + @"'
                 and a.winproperty_int = 0
                 and a.windowtype_int = 1
                 and a.workstatus_int = 1
                 and a.medstoreid_chr = k.medstoreid_chr(+)
                 and a.windowid_chr = k.windowid_chr(+)
            order by intcount4";
            }


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
                lngRegs = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtWindow);

                if (p_dtWindow.Rows.Count > 0)
                {
                    windowsID = p_dtWindow.Rows[0]["WINDOWID_CHR"].ToString();
                    WaiteNO = int.Parse(p_dtWindow.Rows[0]["intcount1"].ToString()) + 1;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }
        #endregion

        #region �жϴ���Ĵ����Ƿ��ڹ���״̬
        /// <summary>
        ///  �жϴ���Ĵ����Ƿ��ڹ���״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strWindowsID">����ID</param>
        /// <param name="isWork">�Ƿ��ڹ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngWindowsWork( string strWindowsID,out bool isWork)
        {
            isWork = false;
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strSQL = @"SELECT   a.WORKSTATUS_INT
                              FROM t_bse_medstorewin a
                              WHERE a.WINDOWID_CHR = '" + strWindowsID + "'";
            DataTable dt= new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (int.Parse(dt.Rows[0]["WORKSTATUS_INT"].ToString()) == 1)
                    {
                        isWork = true;
                    }
                    else
                    {
                        isWork = false;
                    }
                }
                catch
                {
                    isWork = false;
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��ǰ�����е�ҩ��
       /// <summary>
        ///��ȡ��ǰ�����е�ҩ�� 
       /// </summary>
       /// <param name="p_objPrincipal"></param>
       /// <param name="strOldStorageID"></param>
       /// <param name="strNewStorageID"></param>
       /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWorkStorage(  string strOldStorageID,out string strNewStorageID)
        {
            long lngRes = 0;
            strNewStorageID = ""; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
            int weekDay_int = 0;//���ڼ� (1-��һ\7-����)
            clsGetServerDate getServerDate = new clsGetServerDate();
            switch (getServerDate.m_GetServerDate().DayOfWeek.ToString())
            {
                case "Monday":
                    weekDay_int = 1;
                    break;
                case "Tuesday":
                    weekDay_int = 2;
                    break;
                case "Wednesday":
                    weekDay_int = 3;
                    break;
                case "Thursday":
                    weekDay_int = 4;
                    break;
                case "Friday":
                    weekDay_int = 5;
                    break;
                case "Saturday":
                    weekDay_int = 6;
                    break;
                case "Sunday":
                    weekDay_int = 7;
                    break;
            }

            string strSQL = @"SELECT   a.*
                              FROM t_bse_deptduty a
                              WHERE a.deptid_vchr =? and a.WEEKDAY_INT=?";
            DataTable dtDuty = new DataTable();
            try
            {
            
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = strOldStorageID;
                paramArr[1].Value = weekDay_int;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtDuty, paramArr);
            }
            catch
            {
            }
            DateTime _serverDate = getServerDate.m_GetServerDate();
            if (dtDuty.Rows.Count > 0)
            {
                if (dtDuty.Rows[0]["WORKTIME_VCHR"] != System.DBNull.Value && dtDuty.Rows[0]["WORKTIME_VCHR"].ToString() != "")
                {
                    string _split = "|";
                    string[] objstr = dtDuty.Rows[0]["WORKTIME_VCHR"].ToString().Split(_split.ToCharArray());
                    for (int f2 = 0; f2 < objstr.Length; f2++)
                    {
                        _split = "-";
                        string[] objstr1 = objstr[f2].Split(_split.ToCharArray());
                        if (objstr1.Length == 2)
                        {
                            string date1 = _serverDate.Date.ToString("yyyy-MM-dd") + " " + objstr1[0];
                            string date2 = _serverDate.Date.ToString("yyyy-MM-dd") + " " + objstr1[1];
                            if (_serverDate >= DateTime.Parse(date1) && _serverDate <= DateTime.Parse(date2))
                            {
                                strNewStorageID = strOldStorageID;
                                return 1;
                            }
                        }
                    }
                }
            }
            else
            {
                strNewStorageID = strOldStorageID;
                return 1;
            }
            strNewStorageID = dtDuty.Rows[0]["OBJECTDEPTID_VCHR"].ToString();
            return lngRes;
        }
        #endregion

        #region д�������Ϣ
        /// <summary>
        /// д�������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewWinque( clsmedstorewinque p_objRecord)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"insert into t_opr_medstorewinque (seq_int, windowid_chr, windowtype_int, medstoreid_chr, outpatrecipeid_chr, recipetype_chr, order_int, sid_int) 
                                                                 values (seq_medstore.NEXTVAL, ?, ?, ?, ?, ?, ?, ?)";
            try
            {
               

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out paramArr);
                paramArr[0].Value = p_objRecord.m_strWINDOWID_CHR;
                paramArr[1].Value = p_objRecord.m_intWINDOWTYPE_INT;
                paramArr[2].Value = p_objRecord.m_strMEDSTOREID_CHR;
                paramArr[3].Value = p_objRecord.m_strOUTPATRECIPEID_CHR;
                paramArr[4].Value = p_objRecord.m_strRECIPETYPE_CHR;
                paramArr[5].Value = p_objRecord.m_intWaitNO;
                paramArr[6].Value = p_objRecord.m_intWaitNO;
             
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleWinque( clsmedstorewinque p_objRecord)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete t_opr_medstorewinque where MEDSTOREID_CHR=? and WINDOWID_CHR=? and OUTPATRECIPEID_CHR=? and WINDOWTYPE_INT=?";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = p_objRecord.m_strMEDSTOREID_CHR ;
                paramArr[1].Value = p_objRecord.m_strWINDOWID_CHR;
                paramArr[2].Value = p_objRecord.m_strOUTPATRECIPEID_CHR;
                paramArr[3].Value = p_objRecord.m_intWINDOWTYPE_INT;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��ҩ����
        /// <summary>
        /// ��ȡ��ҩ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="TreatwinID">��ҩ����</param>
        /// <param name="windowsID">���ط�ҩ����</param>
        /// <param name="WaitNO">���ض�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGiveWindID( string TreatwinID, out string windowsID, out int WaitNO)
        {
            long lngRegs = 0;
            windowsID = "";
            WaitNO = 1; 
            if (lngRegs < 0)
                return lngRegs;
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyyMMdd");
            string strSQL = @"SELECT a.givewinid_chr,
       CASE
          WHEN b.intcount IS NULL
             THEN 0
          ELSE b.intcount
       END AS intcount
  FROM (SELECT t1.*
  FROM (SELECT   MIN (order_int) AS df, a.givewinid_chr
            FROM t_opr_medstorewinrlt a, t_bse_medstorewin b
           WHERE a.treatwinid_chr =?
             AND a.givewinid_chr = b.windowid_chr
             AND b.workstatus_int = 1
        GROUP BY givewinid_chr) t1,
       (SELECT   MIN (order_int) AS df
            FROM t_opr_medstorewinrlt a, t_bse_medstorewin b
           WHERE a.treatwinid_chr = ?
             AND a.givewinid_chr = b.windowid_chr
             AND b.workstatus_int = 1
        GROUP BY treatwinid_chr) t2
        where t1.df =t2.df) a,
 (SELECT   max(ORDER_INT) AS intcount, windowid_chr
            FROM t_opr_medstorewinque where OUTPATRECIPEID_CHR like ?
        GROUP BY windowid_chr) b
 WHERE a.givewinid_chr = b.windowid_chr(+)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
          
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = TreatwinID;
                paramArr[1].Value = TreatwinID;
                paramArr[2].Value = strDateTime+"%";
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);
                if (p_dtWindow.Rows.Count > 0)
                {
                    windowsID = p_dtWindow.Rows[0]["GIVEWINID_CHR"].ToString();
                    WaitNO = int.Parse(p_dtWindow.Rows[0]["intcount"].ToString()) + 1;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }
        #endregion

        #region ��ȡҩ�������б�
        /// <summary>
        /// ��ȡҩ�������б�
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetMedWindows(string p_strMedstoreID, out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = -1;
            string SQL = @"SELECT a.windowid_chr FROM t_bse_medstorewin a where a.medstoreid_chr= ?";
            try
            {
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] param;
                objSvc.CreateDatabaseParameter(1, out param);
                lngRes = objSvc.lngGetDataTableWithParameters(SQL, ref dtbResult, param);
                objSvc.Dispose();
                objSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

    }

}
