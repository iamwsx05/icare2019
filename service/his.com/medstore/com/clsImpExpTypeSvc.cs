using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data; 

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsImpExpTypeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsImpExpTypeSvc()
        {

        }

        #region 获得最大编号
        /// <summary>
        /// 获得最大编号
        /// </summary>
        /// <param name="MaxCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxCode(ref string MaxCode)
        {
            MaxCode = "";
            long lngRes = -1;
            string strSQL = @"select max(to_number(a.typecode_vchr)) as maxcode from  t_aid_impexptype a ";
            DataTable dt = new DataTable();
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count == 1)
                {
                    MaxCode = dt.Rows[0][0].ToString();
                }
                objHRPSvc.Dispose();
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

        #region 获取全部数据
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="dtTypes"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllType(out clsImpExpType_VO[] objTypes)
        {
            long lngRes = -1;
            string strSQL = @"select a.typecode_vchr, a.typename_vchr, a.flag_int, a.storgeflag_int,
                                     a.status_int
                                from t_aid_impexptype a
                            order by to_number (a.typecode_vchr) ";
            objTypes = null;
            DataTable dtTypes = new DataTable();
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtTypes);
                if (lngRes > 0)
                {
                    int intCount = dtTypes.Rows.Count;
                    objTypes = new clsImpExpType_VO[intCount];
                    DataRow dr = null;
                    for (int i1 = 0; i1 < intCount; i1++)
                    {
                        dr = dtTypes.Rows[i1];
                        objTypes[i1] = new clsImpExpType_VO();

                        objTypes[i1].m_strTypecode = dr["typecode_vchr"].ToString();
                        objTypes[i1].m_strTypeName = dr["typename_vchr"].ToString();
                        objTypes[i1].m_intFlag = int.Parse(dr["flag_int"].ToString());
                        objTypes[i1].m_intStatus = int.Parse(dr["status_int"].ToString());
                        objTypes[i1].m_intStorgeflag = int.Parse(dr["storgeflag_int"].ToString());
                    }
                }
                objHRPSvc.Dispose();
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

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertData(clsImpExpType_VO objVO)
        {
            long lngRes = -1;
            long lngAffter = -1;
            string strSQL = @"insert into t_aid_impexptype
                                    (typecode_vchr, typename_vchr, flag_int, storgeflag_int,
                                     status_int )
                             values (?, ?, ?, ?,
                                     ? ) ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(5, out param);

                param[0].Value = objVO.m_strTypecode;
                param[1].Value = objVO.m_strTypeName;
                param[2].Value = objVO.m_intFlag;
                param[3].Value = objVO.m_intStorgeflag;
                param[4].Value = objVO.m_intStatus;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
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

        [AutoComplete]
        public long m_lngUpdate(clsImpExpType_VO objVO, string oldCode)
        {
            long lngRes = -1;
            long lngAffter = -1;
            string strSQL = @"update t_aid_impexptype
                               set typecode_vchr = ?,
                                   typename_vchr = ?,
                                   flag_int = ?,
                                   storgeflag_int = ?,
                                   status_int = ?
                             where typecode_vchr = ? ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(6, out param);

                param[0].Value = objVO.m_strTypecode;
                param[1].Value = objVO.m_strTypeName;
                param[2].Value = objVO.m_intFlag;
                param[3].Value = objVO.m_intStorgeflag;
                param[4].Value = objVO.m_intStatus;
                param[5].Value = oldCode;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngUpdateStatus(int Status, string Typecode)
        {
            long lngRes = -1;
            long lngAffter = -1;
            string strSQL = @"update t_aid_impexptype
                               set status_int = ?
                             where typecode_vchr = ? ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(2, out param);

                param[0].Value = Status;
                param[1].Value = Typecode;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
