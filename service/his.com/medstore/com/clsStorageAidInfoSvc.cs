using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药库中单据类型及帐务期的服务层
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageAidInfoSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsStorageAidInfoSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region  单据类型

        #region 增加单据类型记录　欧阳孔伟　2004-05-14
        /// <summary>
        /// 增加单据类型记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStorageOrdType"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewStorageOrdType(clsStorageOrdType_VO p_objStorageOrdType)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO T_AID_STORAGEORDTYPE (STORAGEORDTYPEID_CHR,STORAGEORDTYPENAME_VCHR,SIGN_INT,DEPTTYPE_INT,BEGINSTR_CHR,MEDSTORAGE_INT) 
							VALUES('" + p_objStorageOrdType.m_strStorageOrdTypeID + "','" + p_objStorageOrdType.m_strStorageOrdTypeName +
                            "'," + p_objStorageOrdType.m_intSign.ToString() + "," + p_objStorageOrdType.m_intDeptType.ToString() + ",'" + p_objStorageOrdType.m_strBEGINSTR_CHR + "'," + p_objStorageOrdType.m_intMEDSTORAGE.ToString() + ") ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 修改单据类型　欧阳孔伟　2004-05-14
        /// <summary>
        /// 修改单据类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStorageOrdType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdStorageOrdTypeByID(clsStorageOrdType_VO p_objStorageOrdType)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_AID_STORAGEORDTYPE
							SET STORAGEORDTYPENAME_VCHR='" + p_objStorageOrdType.m_strStorageOrdTypeName +
                            "',SIGN_INT=" + p_objStorageOrdType.m_intSign.ToString() +
                            ",DEPTTYPE_INT=" + p_objStorageOrdType.m_intDeptType.ToString() +
                            " ,BEGINSTR_CHR='" + p_objStorageOrdType.m_strBEGINSTR_CHR + "',MEDSTORAGE_INT=" + p_objStorageOrdType.m_intMEDSTORAGE.ToString() +
                            "  WHERE STORAGEORDTYPEID_CHR='" + p_objStorageOrdType.m_strStorageOrdTypeID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region　删除单据类型记录　欧阳孔伟　2004-05-14
        /// <summary>
        /// 删除单据类型记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageOrdTypeID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDeleteStorageOrdTypeByID(string p_strStorageOrdTypeID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE T_AID_STORAGEORDTYPE
							WHERE STORAGEORDTYPEID_CHR='" + p_strStorageOrdTypeID + "' ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 以ID号来检索对应的单据类型记录  欧阳孔伟　2004-05-14
        /// <summary>
        /// 以ID号来查找对应的单据类型记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageOrdTypeID"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdTypeByID(string p_strStorageOrdTypeID, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            //			string strSQL = @"SELECT storageordtypeid_chr,storageordtypename_vchr,sign_int,depttype_int
            //							  FROM t_aid_storageordtype 
            //							  WHERE storageordtypeid_chr = ?";
            //			try
            //			{
            //				System.Data.IDataParameter[] objParamArr = clsIDataParameterCreator.s_objConstructIDataParameterArr(p_StorageOrdTypeID);
            //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbResult,objParamArr);
            //				
            //			}
            //			catch(Exception objEx)
            //			{
            //				string strTmp=objEx.Message;
            //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //				bool blnRes = objLogger.LogError(objEx); 
            //			}

            string strSQL = "WHERE storageordtypeid_chr='" + p_strStorageOrdTypeID + "'";

            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 查询所有单据类型　欧阳孔伟  2004-05-14
        /// <summary>
        /// 查询所有单据类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllStorageOrdType(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            //			string strSQL = @"SELECT storageordtypeid_chr,storageordtypename_vchr,sign_int,depttype_int
            //							  FROM t_aid_storageordtype";
            //			try
            //			{
            //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbResult);
            //				
            //			}
            //			catch(Exception objEx)
            //			{
            //				string strTmp=objEx.Message;
            //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //				bool blnRes = objLogger.LogError(objEx); 
            //			}

            string strSQL = "order by SIGN_INT";

            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 将DataTable数据传递到VO　欧阳孔伟　2004-06-17
        /// <summary>
        /// 将DataTable数据传递到VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据</param>
        /// <param name="objItem">单据类型VO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStorageOrdType_VO[] objItem)
        {
            objItem = new clsStorageOrdType_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItem = new clsStorageOrdType_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItem[i] = new clsStorageOrdType_VO();
                            objItem[i].m_strStorageOrdTypeID = dtbSource.Rows[i]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                            objItem[i].m_strStorageOrdTypeName = dtbSource.Rows[i]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();

                            if (dtbSource.Rows[i]["SIGN_INT"].ToString().Trim() != null)
                            {
                                objItem[i].m_intSign = int.Parse(dtbSource.Rows[i]["SIGN_INT"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i]["DEPTTYPE_INT"].ToString().Trim() != null)
                            {
                                objItem[i].m_intDeptType = int.Parse(dtbSource.Rows[i]["DEPTTYPE_INT"].ToString().Trim());
                            }

                            if (dtbSource.Rows[i]["BEGINSTR_CHR"].ToString().Trim() != null)
                            {
                                objItem[i].m_strBEGINSTR_CHR = dtbSource.Rows[i]["BEGINSTR_CHR"].ToString().Trim();
                            }
                            if (dtbSource.Rows[i]["MEDSTORAGE_INT"].ToString().Trim() != null)
                            {
                                objItem[i].m_intMEDSTORAGE = int.Parse(dtbSource.Rows[i]["MEDSTORAGE_INT"].ToString().Trim());
                            }
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

        }
        #endregion

        #region 模糊查询单据  欧阳孔伟  2004-06-05
        /// <summary>
        /// 模糊查询单据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdTypeByAny(string p_strSQL, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = @"SELECT *
							  FROM t_aid_storageordtype 
							   " + p_strSQL;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

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

        #region 模糊查询单据  欧阳孔伟  2004-06-05
        /// <summary>
        /// 模糊查询单据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeByAny(string p_strSQL, out clsStorageOrdType_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdType_VO[0];

            System.Data.DataTable dtbResult = new DataTable();

            lngRes = m_lngFindStorageOrdTypeByAny(p_strSQL, out dtbResult);
            if (lngRes > 0 && dtbResult != null)
            {
                CopyDataTableToVO(dtbResult, out p_objResult);
            }


            return lngRes;

        }
        #endregion

        #region 根据单据标识符查询单据类型  欧阳孔伟　2004-05-20
        /// <summary>
        /// 根据单据标识取出单据类型
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_intSign">单据标识，1为入库，2为出库，3为盘点，4为调价</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdTypeBySign(int p_intSign, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = " WHERE sign_int =" + p_intSign.ToString() + " ";
            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 根据ID前辍及单据标识符查询单据类型  欧阳孔伟　2004-05-20
        /// <summary>
        /// 根据ID前辍及单据标识取出单据类型
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strForwardID">代码的前辍</param>
        /// <param name="p_intSign">单据标识，1为入库，2为出库，3为盘点，4为调价</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdTypeBySign(string p_strForwardID, int p_intSign, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = " WHERE sign_int =" + p_intSign.ToString() + " ";
            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 根据院内处标识查询单据类型  欧阳孔伟　2004-06-06
        /// <summary>
        /// 根据院内外标识取出单据类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intDeptType"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdTypeByDeptType(int p_intDeptType, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            //			string strSQL = @"SELECT storageordtypeid_chr,storageordtypename_vchr,sign_int,depttype_int
            //							  FROM t_aid_storageordtype 
            //							  WHERE sign_int = ?";
            //			try
            //			{
            //				System.Data.IDataParameter[] objParamArr = clsIDataParameterCreator.s_objConstructIDataParameterArr(p_intSign);
            //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbResult,objParamArr);
            //				
            //			}
            //			catch(Exception objEx)
            //			{
            //				string strTmp=objEx.Message;
            //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //				bool blnRes = objLogger.LogError(objEx); 
            //			}

            string strSQL = " WHERE DEPTTYPE_INT =" + p_intDeptType.ToString() + " ";
            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 获得最大单据类型ID  欧阳孔伟  2004-06-05
        /// <summary>
        /// 获得最大单据类型ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxStorageOrdTypeID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("T_AID_STORAGEORDTYPE", "STORAGEORDTYPEID_CHR", 4);

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

        #endregion

        #region  帐务期

        #region 增加帐务期间　欧阳孔伟  2004-05-14
        /// <summary>
        ///  添加帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPeriod"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewPeriod(clsPeriod_VO p_objPeriod)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_bse_period (PERIODID_CHR,STARTDATE_DAT,ENDDATE_DAT) 
							VALUES('" + p_objPeriod.m_strPeriodID + "',to_date('" + p_objPeriod.m_strStartDate +
                            "','yyyy-mm-dd'),to_date('" + p_objPeriod.m_strEndDate + "','yyyy-mm-dd'))";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 修改帐务期间 欧阳孔伟　2004-05-14
        /// <summary>
        ///  修改帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPeriod"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoUpdPeriodByID(clsPeriod_VO p_objPeriod)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_bse_period 
							SET STARTDATE_DAT= to_date('" + p_objPeriod.m_strStartDate + "','yyyy-mm-dd'),ENDDATE_DAT =to_date('" + p_objPeriod.m_strEndDate + "','yyyy-mm-dd') " +
                            " WHERE PERIODID_CHR='" + p_objPeriod.m_strPeriodID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region　删除帐务期间　欧阳孔伟　2004-05-14
        /// <summary>
        ///  删除帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPeriodID"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDeletePeriodByID(string p_strPeriodID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_bse_period 
							WHERE PERIODID_CHR='" + p_strPeriodID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region　删除所有帐务期间　欧阳孔伟　2004-06-06
        /// <summary>
        ///  删除所有帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDeleteAllPeriod()
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_bse_period ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region  以ID来查询对应的帐务期间　欧阳孔伟　2004-05-14
        /// <summary>
        /// 根据ID查询帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPeriodID"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodByID(string p_strPeriodID, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = " WHERE PERIODID_CHR='" + p_strPeriodID + "'";

            lngRes = m_lngFindStorageOrdTypeByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 查询所有的帐务期间  欧阳孔伟　2004-05-14
        /// <summary>
        ///  查询所有帐务期间记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllPeriod(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = "";
            lngRes = m_lngFindPeriodByAny(strSQL, out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region 模糊查询帐务期  欧阳孔伟  2004-06-05
        /// <summary>
        /// 模糊查询帐务期
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodByAny(string p_strSQL, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = @"SELECT periodid_chr,to_char(startdate_dat,'yyyy-mm-dd') as startdate,to_char(enddate_dat,'yyyy-mm-dd') as enddate,to_char(startdate_dat,'yyyy-mm-dd') || ' 至 ' || to_char(enddate_dat,'yyyy-mm-dd') as period
							  FROM t_bse_period
							 " + p_strSQL + " order by periodid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

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

        #region　查询当前帐务期　欧阳孔伟　2004-05-20
        /// <summary>
        /// 查询当前帐务期
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCurrentPeriod(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strSQL = @"SELECT periodid_chr,to_char(startdate_dat,'yyyy-mm-dd') as startdate,to_char(enddate_dat,'yyyy-mm-dd') as enddate,to_char(startdate_dat,'yyyy-mm-dd') || ' 至 ' || to_char(enddate_dat,'yyyy-mm-dd') as period
							  FROM t_bse_period
							  WHERE periodid_chr = (
									SELECT min(periodid_chr)
									FROM t_bse_period
									WHERE periodid_chr not in (SELECT periodid_chr FROM t_opr_period)
								)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

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

        #region 获得最大帐务期ID  欧阳孔伟  2004-06-05
        /// <summary>
        /// 获得最大帐务期ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxPeriodID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("t_bse_period", "periodid_chr", 4);

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

        #region 查询帐务期表的行数
        /// <summary>
        /// 查询帐务结转表的行数
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_intRow">行数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPeriodRow(out int p_intRow)
        {
            long lngRes = 0;
            p_intRow = 0;

            System.Data.DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT COUNT(*)
								FROM t_bse_period";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    string strRow = dtbResult.Rows[0][0].ToString().Trim();
                    if (strRow != "")
                    {
                        p_intRow = int.Parse(strRow);
                    }
                }

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

        #region 获取已有的最大日期 created by weiling.huang at 2005-9-29
        /// <summary>
        /// 获取已有的最大日期 created by weiling.huang at 2005-9-29
        /// </summary>
        /// <param name="p_strPeriodID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMaxValuePeriod(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = "";

            string strSQL = @"SELECT Max(to_char(enddate_dat,'yyyy-mm-dd')) as Maxdate
							  FROM t_bse_period	 ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0][0] != System.DBNull.Value)
                        p_strResult = dtbResult.Rows[0][0].ToString().Trim();
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

        #endregion

        #region 帐务结转
        #region 增加帐务结转
        /// <summary>
        ///  新增帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objItem">帐务结转数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewPeriodOperator(clsPeriodOperator_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO t_opr_period
							    		  (storageid_chr, periodid_chr, empid_chr,
										   operdate_dat
									       )
									VALUES ('" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "','" +
                                                 p_objItem.m_objStorage.m_strStroageID.Trim() + "'," +
                                                 p_objItem.m_objOper.strEmpID.Trim() + "',TO_DATE('" +
                                                 p_objItem.m_strOperDate.Trim() + "','yyyy-mm-dd hh24:mi:ss')" +
                                           ")";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 修改帐务结转
        /// <summary>
        /// 修改帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objItem">帐务结转数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdatePeriodOperator(clsPeriodOperator_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_period
								 SET empid_chr = '" + p_objItem.m_objOper.strEmpID.Trim() + "'," +
                                    "operdate_dat = TO_DATE ('" + p_objItem.m_strOperDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')" +
                              "WHERE storageid_chr = '" + p_objItem.m_objStorage.m_strStroageID.Trim() + "' AND periodid_chr = '" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 删除帐务结转
        /// <summary>
        /// 删除帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStorageID">库房代码</param>
        /// <param name="p_strPeriodID">帐务期代码</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDeletePeriodOperator(string p_strStorageID, string p_strPeriodID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE      t_opr_period
							        WHERE storageid_chr = '" + p_strStorageID.Trim() + "' AND periodid_chr = '" + p_strPeriodID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


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

        #region 模糊查询帐务结转
        /// <summary>
        /// 模糊查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByAny(string p_strSQL, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            System.Data.DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT a.*, b.storagename_vchr, c.startdate_dat, c.enddate_dat,
									 d.lastname_vchr
								FROM t_opr_period a, t_bse_storage b, t_bse_period c, t_bse_employee d
							 " + p_strSQL;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResult = new clsPeriodOperator_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResult[i] = new clsPeriodOperator_VO();
                            p_objResult[i].m_objStorage = new clsStorage_VO();
                            p_objResult[i].m_objPeriod = new clsPeriod_VO();
                            p_objResult[i].m_objOper = new clsEmployeeVO();

                            p_objResult[i].m_objStorage.m_strStroageID = dtbResult.Rows[i]["storageid_chr"].ToString().Trim();
                            p_objResult[i].m_objStorage.m_strStroageName = dtbResult.Rows[i]["storagename_vchr"].ToString().Trim();
                            p_objResult[i].m_objPeriod.m_strPeriodID = dtbResult.Rows[i]["periodid_chr"].ToString().Trim();
                            p_objResult[i].m_objPeriod.m_strStartDate = dtbResult.Rows[i]["startdate_dat"].ToString().Trim();
                            p_objResult[i].m_objPeriod.m_strEndDate = dtbResult.Rows[i]["enddate_dat"].ToString().Trim();
                            p_objResult[i].m_objOper.strEmpID = dtbResult.Rows[i]["empid_chr"].ToString().Trim();
                            p_objResult[i].m_objOper.strLastName = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                            p_objResult[i].m_strOperDate = dtbResult.Rows[i]["operdate_dat"].ToString().Trim();
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

            return lngRes;

        }
        #endregion

        #region 按库房查询帐务结转
        /// <summary>
        /// 按库房查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">库房代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByStorage(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" WHERE a.storageid_chr='" + p_strID.Trim() + "'";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按帐务期查询帐务结转
        /// <summary>
        /// 按帐务期查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByPeriod(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" WHERE a.periodid_chr='" + p_strID.Trim() + "'";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按库房和帐务期查询帐务结转
        /// <summary>
        /// 按库房和帐务期查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStorageID">库房代码</param>
        /// <param name="p_strPeriodID">帐务期代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByStorageAndPeriod(string p_strStorageID, string p_strPeriodID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" WHERE a.storageid_chr='" + p_strStorageID.Trim() + "' AND a.periodid_chr'" + p_strPeriodID.Trim() + "'";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按结转操作员查询帐务结转
        /// <summary>
        /// 按结转操作员查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">操作员代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByOper(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" WHERE a.empid_chr='" + p_strID.Trim() + "'";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按结转时间段查询帐务结转
        /// <summary>
        /// 按结转时间段查询帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriodOperatorByDate(string p_strStartDate, string p_strEndDate, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" WHERE a.operdate_dat>=TO_DATE('" + p_strStartDate.Trim() + "','yyyy-mm-dd hh24:mi:ss') " +
                                    " AND a.operdate_dat<= TO('" + p_strEndDate.Trim() + "','yyyy-mm-dd hh24:mi:ss') ";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 查询所有的帐务结转
        /// <summary>
        /// 查询所有的帐务结转
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllPeriodOperator(out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            string strSQL = @" ";

            lngRes = m_lngFindPeriodOperatorByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 查询结转人及时间
        /// <summary>
        /// 查询结转人及时间
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStorageID">库房代码</param>
        /// <param name="p_strPeriodID">帐务期代码</param>
        /// <param name="p_strEmp">操作员</param>
        /// <param name="p_strOperDate">操作时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPeriodOperatorReal(string p_strStorageID, string p_strPeriodID, out string p_strEmp, out string p_strOperDate)
        {
            long lngRes = 0;
            p_strEmp = "";
            p_strOperDate = "";

            System.Data.DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT empid_chr,operdate_dat
								FROM t_opr_period
							   WHERE storageid_chr='" + p_strStorageID.Trim() +
                            "' AND periodid_chr='" + p_strPeriodID.Trim() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    p_strEmp = dtbResult.Rows[0]["empid_chr"].ToString().Trim();
                    p_strOperDate = dtbResult.Rows[0]["operdate_dat"].ToString();
                }
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


        #endregion
    }
}