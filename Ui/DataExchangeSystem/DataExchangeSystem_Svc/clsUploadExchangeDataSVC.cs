using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.ValueObject;
using com.digitalwave.Utility;
using System.EnterpriseServices;
using System.Data.SqlClient;

namespace com.digitalwave.iCare.middletier.DataExchangeSystem_Svc
{
    /// <summary>
    /// 茶山万能转账接口数据上传
    /// </summary>
    [ObjectPooling(Enabled = true)]
    [Transaction(TransactionOption.NotSupported)]
    public class clsUploadExchangeDataSVC
    {
        #region 入库数据上传
        /// <summary>
        /// 入库数据上传
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUploadInStorageData(clsInStorageData_VO InStorageData)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"insert into 采购入库
(业务类别 ,单据编号,日期,药库号,药库名称,单位编号,单位名称,项目编号,项目名称,买入金额,零售金额,进零差价,标识)
values
(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                SqlParameter[] objParmArr = null;
                if (InStorageData != null)
                {
                    objParmArr = new SqlParameter[13];

                    objParmArr[0] = new SqlParameter("@1", System.Data.SqlDbType.NVarChar);
                    objParmArr[1] = new SqlParameter("@2", System.Data.SqlDbType.NVarChar);
                    objParmArr[2] = new SqlParameter("@3", System.Data.SqlDbType.DateTime);
                    objParmArr[3] = new SqlParameter("@4", System.Data.SqlDbType.NVarChar);
                    objParmArr[4] = new SqlParameter("@5", System.Data.SqlDbType.NVarChar);
                    objParmArr[5] = new SqlParameter("@6", System.Data.SqlDbType.NVarChar);
                    objParmArr[6] = new SqlParameter("@7", System.Data.SqlDbType.NVarChar);
                    objParmArr[7] = new SqlParameter("@8", System.Data.SqlDbType.NVarChar);
                    objParmArr[8] = new SqlParameter("@9", System.Data.SqlDbType.NVarChar);
                    objParmArr[9] = new SqlParameter("@10", System.Data.SqlDbType.Float);
                    objParmArr[10] = new SqlParameter("@11", System.Data.SqlDbType.Float);
                    objParmArr[11] = new SqlParameter("@12", System.Data.SqlDbType.Float);
                    objParmArr[12] = new SqlParameter("@13", System.Data.SqlDbType.NVarChar);

                    if (!string.IsNullOrEmpty(InStorageData.YWLB))
                    {
                        objParmArr[0].Value = InStorageData.YWLB;
                    }
                    else
                    {
                        objParmArr[0].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.DJBH))
                    {
                        objParmArr[1].Value = InStorageData.DJBH;
                    }
                    else
                    {
                        objParmArr[1].Value = DBNull.Value;
                    }

                    objParmArr[2].Value = InStorageData.RQ;

                    if (!string.IsNullOrEmpty(InStorageData.YKH))
                    {
                        objParmArr[3].Value = InStorageData.YKH;
                    }
                    else
                    {
                        objParmArr[3].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.YKMC))
                    {
                        objParmArr[4].Value = InStorageData.YKMC;
                    }
                    else
                    {
                        objParmArr[4].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.DWBH))
                    {
                        objParmArr[5].Value = InStorageData.DWBH;
                    }
                    else
                    {
                        objParmArr[5].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.DWMC))
                    {
                        objParmArr[6].Value = InStorageData.DWMC;
                    }
                    else
                    {
                        objParmArr[6].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.XMBH))
                    {
                        objParmArr[7].Value = InStorageData.XMBH;
                    }
                    else
                    {
                        objParmArr[7].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InStorageData.XMMC))
                    {
                        objParmArr[8].Value = InStorageData.XMMC;
                    }
                    else
                    {
                        objParmArr[8].Value = DBNull.Value;
                    }

                    objParmArr[9].Value = InStorageData.MRJE;


                    objParmArr[10].Value = InStorageData.LSJE;

                    objParmArr[11].Value = InStorageData.JLCJ;

                    if (!string.IsNullOrEmpty(InStorageData.BZ))
                    {
                        objParmArr[12].Value = InStorageData.BZ;
                    }
                    else
                    {
                        objParmArr[12].Value = DBNull.Value;
                    }

                }
                lngRes = objSvc.ExecuteSQL(strSQL, objParmArr);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }

            return lngRes;
        }

        #endregion

        #region 入库数据删除
        /// <summary>
        /// 入库数据删除
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelInStorageData(DateTime dayTime)
        {
            long lngRes = -1;
            try
            {
                string p_dtmBegin = dayTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string p_dtmEnd = dayTime.ToString("yyyy-MM-dd") + " 23:59:59";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                string strSQL = @"delete  采购入库 where 日期 between '{0}' and '{1}'";
                strSQL = string.Format(strSQL, p_dtmBegin, p_dtmEnd);
                lngRes = objSvc.ExecuteScalar(strSQL);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }

        #endregion

        #region 出库数据上传
        /// <summary>
        /// 出库数据上传
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUploadOutStorageData(clsOutStorageData_VO OutStorageData)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"insert into 出库
(业务类别,单据编号,日期,药库号,药库名称,单位编号,单位名称,项目编号,项目名称,买入金额,零售金额,进零差价,标识)
values
(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                SqlParameter[] objParmArr = null;
                if (OutStorageData != null)
                {
                    objParmArr = new SqlParameter[13];

                    objParmArr[0] = new SqlParameter("@1", System.Data.SqlDbType.NVarChar);
                    objParmArr[1] = new SqlParameter("@2", System.Data.SqlDbType.NVarChar);
                    objParmArr[2] = new SqlParameter("@3", System.Data.SqlDbType.DateTime);
                    objParmArr[3] = new SqlParameter("@4", System.Data.SqlDbType.NVarChar);
                    objParmArr[4] = new SqlParameter("@5", System.Data.SqlDbType.NVarChar);
                    objParmArr[5] = new SqlParameter("@6", System.Data.SqlDbType.NVarChar);
                    objParmArr[6] = new SqlParameter("@7", System.Data.SqlDbType.NVarChar);
                    objParmArr[7] = new SqlParameter("@8", System.Data.SqlDbType.NVarChar);
                    objParmArr[8] = new SqlParameter("@9", System.Data.SqlDbType.NVarChar);
                    objParmArr[9] = new SqlParameter("@10", System.Data.SqlDbType.Float);
                    objParmArr[10] = new SqlParameter("@11", System.Data.SqlDbType.Float);
                    objParmArr[11] = new SqlParameter("@12", System.Data.SqlDbType.Float);
                    objParmArr[12] = new SqlParameter("@13", System.Data.SqlDbType.NVarChar);

                    if (!string.IsNullOrEmpty(OutStorageData.YWLB))
                    {
                        objParmArr[0].Value = OutStorageData.YWLB;
                    }
                    else
                    {
                        objParmArr[0].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.DJBH))
                    {
                        objParmArr[1].Value = OutStorageData.DJBH;
                    }
                    else
                    {
                        objParmArr[1].Value = DBNull.Value;
                    }

                    objParmArr[2].Value = OutStorageData.RQ;

                    if (!string.IsNullOrEmpty(OutStorageData.YKH))
                    {
                        objParmArr[3].Value = OutStorageData.YKH;
                    }
                    else
                    {
                        objParmArr[3].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.YKMC))
                    {
                        objParmArr[4].Value = OutStorageData.YKMC;
                    }
                    else
                    {
                        objParmArr[4].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.DWBH))
                    {
                        objParmArr[5].Value = OutStorageData.DWBH;
                    }
                    else
                    {
                        objParmArr[5].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.DWMC))
                    {
                        objParmArr[6].Value = OutStorageData.DWMC;
                    }
                    else
                    {
                        objParmArr[6].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.XMBH))
                    {
                        objParmArr[7].Value = OutStorageData.XMBH;
                    }
                    else
                    {
                        objParmArr[7].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(OutStorageData.XMMC))
                    {
                        objParmArr[8].Value = OutStorageData.XMMC;
                    }
                    else
                    {
                        objParmArr[8].Value = DBNull.Value;
                    }

                    objParmArr[9].Value = OutStorageData.MRJE;


                    objParmArr[10].Value = OutStorageData.LSJE;

                    objParmArr[11].Value = OutStorageData.JLCJ;


                    if (!string.IsNullOrEmpty(OutStorageData.BZ))
                    {
                        objParmArr[12].Value = OutStorageData.BZ;
                    }
                    else
                    {
                        objParmArr[12].Value = DBNull.Value;
                    }


                }
                lngRes = objSvc.ExecuteSQL(strSQL, objParmArr);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }
        #endregion

        #region 出库数据删除
        /// <summary>
        /// 出库数据删除
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOutStorageData(DateTime dayTime)
        {
            long lngRes = -1;
            try
            {
                string p_dtmBegin = dayTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string p_dtmEnd = dayTime.ToString("yyyy-MM-dd") + " 23:59:59";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                string strSQL = @"delete  出库 where 日期 between '{0}' and '{1}'";
                strSQL = string.Format(strSQL, p_dtmBegin, p_dtmEnd);
                lngRes = objSvc.ExecuteScalar(strSQL);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }

        #endregion

        #region 住院收入上传
        /// <summary>
        /// 住院收入上传
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUploadInHospital(clsInHospital_VO InHospital)
        {
            long lngRes = -1;
            try
            {
            
                clsDatabaseSVC objSvc = new clsDatabaseSVC();

                string strSQL = @"insert into 住院收入
(单据编号 ,标识 ,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额)
values
(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
               
                SqlParameter[] objParmArr = null;
                if (InHospital != null)
                {
                    objParmArr = new SqlParameter[10];

                    objParmArr[0] = new SqlParameter("@1", System.Data.SqlDbType.NVarChar);
                    objParmArr[1] = new SqlParameter("@2", System.Data.SqlDbType.NVarChar);
                    objParmArr[2] = new SqlParameter("@3", System.Data.SqlDbType.DateTime);
                    objParmArr[3] = new SqlParameter("@4", System.Data.SqlDbType.NVarChar);
                    objParmArr[4] = new SqlParameter("@5", System.Data.SqlDbType.NVarChar);
                    objParmArr[5] = new SqlParameter("@6", System.Data.SqlDbType.NVarChar);
                    objParmArr[6] = new SqlParameter("@7", System.Data.SqlDbType.NVarChar);
                    objParmArr[7] = new SqlParameter("@8", System.Data.SqlDbType.NVarChar);
                    objParmArr[8] = new SqlParameter("@9", System.Data.SqlDbType.NVarChar);
                    objParmArr[9] = new SqlParameter("@10", System.Data.SqlDbType.Float);

                    if (!string.IsNullOrEmpty(InHospital.DJBH))
                    {
                        objParmArr[0].Value = InHospital.DJBH;
                    }
                    else
                    {
                        objParmArr[0].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.BZ))
                    {
                        objParmArr[1].Value = InHospital.BZ;
                    }
                    else
                    {
                        objParmArr[1].Value = DBNull.Value;
                    }


                    objParmArr[2].Value = InHospital.RQ;

                    if (!string.IsNullOrEmpty(InHospital.BMBH))
                    {
                        objParmArr[3].Value = InHospital.BMBH;
                    }
                    else
                    {
                        objParmArr[3].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.BMMC))
                    {
                        objParmArr[4].Value = InHospital.BMMC;
                    }
                    else
                    {
                        objParmArr[4].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.YSBH))
                    {
                        objParmArr[5].Value = InHospital.YSBH;
                    }
                    else
                    {
                        objParmArr[5].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.YSMC))
                    {
                        objParmArr[6].Value = InHospital.YSMC;
                    }
                    else
                    {
                        objParmArr[6].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.XMBH))
                    {
                        objParmArr[7].Value = InHospital.XMBH;
                    }
                    else
                    {
                        objParmArr[7].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(InHospital.XMMC))
                    {
                        objParmArr[8].Value = InHospital.XMMC;
                    }
                    else
                    {
                        objParmArr[8].Value = DBNull.Value;
                    }

                    objParmArr[9].Value = InHospital.XMJE;

                }
                lngRes = objSvc.ExecuteSQL(strSQL, objParmArr);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }

        #endregion

        #region 住院收入删除
        /// <summary>
        /// 住院收入删除
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelInHospital(DateTime dayTime)
        {
            long lngRes = -1;
            try
            {
                string p_dtmBegin = dayTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string p_dtmEnd = dayTime.ToString("yyyy-MM-dd") + " 23:59:59";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                string strSQL = @"delete  住院收入 where 日期 between '{0}' and '{1}'";
                strSQL = string.Format(strSQL, p_dtmBegin, p_dtmEnd);
                lngRes = objSvc.ExecuteScalar(strSQL);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }

        #endregion

        #region 门诊收入上传
        /// <summary>
        /// 门诊收入上传
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUploadOutpatient(clsOutpatient_VO Outpatient)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"insert into 门诊收入
(单据编号 ,标识 ,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额,收费处标识)
values
(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11)";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                SqlParameter[] objParmArr = null;
                if (Outpatient != null)
                {
                    objParmArr = new SqlParameter[11];

                    objParmArr[0] = new SqlParameter("@1", System.Data.SqlDbType.NVarChar);
                    objParmArr[1] = new SqlParameter("@2", System.Data.SqlDbType.NVarChar);
                    objParmArr[2] = new SqlParameter("@3", System.Data.SqlDbType.DateTime);
                    objParmArr[3] = new SqlParameter("@4", System.Data.SqlDbType.NVarChar);
                    objParmArr[4] = new SqlParameter("@5", System.Data.SqlDbType.NVarChar);
                    objParmArr[5] = new SqlParameter("@6", System.Data.SqlDbType.NVarChar);
                    objParmArr[6] = new SqlParameter("@7", System.Data.SqlDbType.NVarChar);
                    objParmArr[7] = new SqlParameter("@8", System.Data.SqlDbType.NVarChar);
                    objParmArr[8] = new SqlParameter("@9", System.Data.SqlDbType.NVarChar);
                    objParmArr[9] = new SqlParameter("@10", System.Data.SqlDbType.Float);
                    objParmArr[10] = new SqlParameter("@11", System.Data.SqlDbType.NVarChar);

                    if (!string.IsNullOrEmpty(Outpatient.DJBH))
                    {
                        objParmArr[0].Value = Outpatient.DJBH;
                    }
                    else
                    {
                        objParmArr[0].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.BZ))
                    {
                        objParmArr[1].Value = Outpatient.BZ;
                    }
                    else
                    {
                        objParmArr[1].Value = DBNull.Value;
                    }

                    objParmArr[2].Value = Outpatient.RQ;

                    if (!string.IsNullOrEmpty(Outpatient.BMBH))
                    {
                        objParmArr[3].Value = Outpatient.BMBH;
                    }
                    else
                    {
                        objParmArr[3].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.BMMC))
                    {
                        objParmArr[4].Value = Outpatient.BMMC;
                    }
                    else
                    {
                        objParmArr[4].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.YSBH))
                    {
                        objParmArr[5].Value = Outpatient.YSBH;
                    }
                    else
                    {
                        objParmArr[5].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.YSMC))
                    {
                        objParmArr[6].Value = Outpatient.YSMC;
                    }
                    else
                    {
                        objParmArr[6].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.XMBH))
                    {
                        objParmArr[7].Value = Outpatient.XMBH;
                    }
                    else
                    {
                        objParmArr[7].Value = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(Outpatient.XMMC))
                    {
                        objParmArr[8].Value = Outpatient.XMMC;
                    }
                    else
                    {
                        objParmArr[8].Value = DBNull.Value;
                    }

                    objParmArr[9].Value = Outpatient.XMJE;
                    objParmArr[10].Value = Outpatient.SFCBZ;


                }
                lngRes = objSvc.ExecuteSQL(strSQL, objParmArr);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }
        #endregion

        #region 门诊收入删除
        /// <summary>
        /// 门诊收入删除
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOutpatient(DateTime dayTime)
        {
            long lngRes = -1;
            try
            {
                string p_dtmBegin = dayTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string p_dtmEnd = dayTime.ToString("yyyy-MM-dd") + " 23:59:59";
                clsDatabaseSVC objSvc = new clsDatabaseSVC();
                string strSQL = @"delete  门诊收入 where 日期 between '{0}' and '{1}'";
                strSQL = string.Format(strSQL, p_dtmBegin, p_dtmEnd);
                lngRes = objSvc.ExecuteScalar(strSQL);
                objSvc.Dispose();
            }
            catch (Exception objExt)
            {
                clsLogText logtxt = new clsLogText();
                logtxt.LogError(objExt);
            }
            return lngRes;
        }

        #endregion

    }
}
