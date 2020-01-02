using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ���е������ͼ������ڵķ����
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageAidInfoSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsStorageAidInfoSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region  ��������

        #region ���ӵ������ͼ�¼��ŷ����ΰ��2004-05-14
        /// <summary>
        /// ���ӵ������ͼ�¼
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

        #region �޸ĵ������͡�ŷ����ΰ��2004-05-14
        /// <summary>
        /// �޸ĵ�������
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

        #region��ɾ���������ͼ�¼��ŷ����ΰ��2004-05-14
        /// <summary>
        /// ɾ���������ͼ�¼
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

        #region ��ID����������Ӧ�ĵ������ͼ�¼  ŷ����ΰ��2004-05-14
        /// <summary>
        /// ��ID�������Ҷ�Ӧ�ĵ������ͼ�¼
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

        #region ��ѯ���е������͡�ŷ����ΰ  2004-05-14
        /// <summary>
        /// ��ѯ���е�������
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

        #region ��DataTable���ݴ��ݵ�VO��ŷ����ΰ��2004-06-17
        /// <summary>
        /// ��DataTable���ݴ��ݵ�VO
        /// </summary>
        /// <param name="dtbSource">DataTable����</param>
        /// <param name="objItem">��������VO</param>
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

        #region ģ����ѯ����  ŷ����ΰ  2004-06-05
        /// <summary>
        /// ģ����ѯ����
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

        #region ģ����ѯ����  ŷ����ΰ  2004-06-05
        /// <summary>
        /// ģ����ѯ����
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
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

        #region ���ݵ��ݱ�ʶ����ѯ��������  ŷ����ΰ��2004-05-20
        /// <summary>
        /// ���ݵ��ݱ�ʶȡ����������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_intSign">���ݱ�ʶ��1Ϊ��⣬2Ϊ���⣬3Ϊ�̵㣬4Ϊ����</param>
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

        #region ����IDǰ꡼����ݱ�ʶ����ѯ��������  ŷ����ΰ��2004-05-20
        /// <summary>
        /// ����IDǰ꡼����ݱ�ʶȡ����������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strForwardID">�����ǰ�</param>
        /// <param name="p_intSign">���ݱ�ʶ��1Ϊ��⣬2Ϊ���⣬3Ϊ�̵㣬4Ϊ����</param>
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

        #region ����Ժ�ڴ���ʶ��ѯ��������  ŷ����ΰ��2004-06-06
        /// <summary>
        /// ����Ժ�����ʶȡ����������
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

        #region �����󵥾�����ID  ŷ����ΰ  2004-06-05
        /// <summary>
        /// �����󵥾�����ID
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

        #region  ������

        #region ���������ڼ䡡ŷ����ΰ  2004-05-14
        /// <summary>
        ///  ��������ڼ��¼
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

        #region �޸������ڼ� ŷ����ΰ��2004-05-14
        /// <summary>
        ///  �޸������ڼ��¼
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

        #region��ɾ�������ڼ䡡ŷ����ΰ��2004-05-14
        /// <summary>
        ///  ɾ�������ڼ��¼
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

        #region��ɾ�����������ڼ䡡ŷ����ΰ��2004-06-06
        /// <summary>
        ///  ɾ�����������ڼ��¼
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

        #region  ��ID����ѯ��Ӧ�������ڼ䡡ŷ����ΰ��2004-05-14
        /// <summary>
        /// ����ID��ѯ�����ڼ��¼
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

        #region ��ѯ���е������ڼ�  ŷ����ΰ��2004-05-14
        /// <summary>
        ///  ��ѯ���������ڼ��¼
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

        #region ģ����ѯ������  ŷ����ΰ  2004-06-05
        /// <summary>
        /// ģ����ѯ������
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

            string strSQL = @"SELECT periodid_chr,to_char(startdate_dat,'yyyy-mm-dd') as startdate,to_char(enddate_dat,'yyyy-mm-dd') as enddate,to_char(startdate_dat,'yyyy-mm-dd') || ' �� ' || to_char(enddate_dat,'yyyy-mm-dd') as period
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

        #region����ѯ��ǰ�����ڡ�ŷ����ΰ��2004-05-20
        /// <summary>
        /// ��ѯ��ǰ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCurrentPeriod(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strSQL = @"SELECT periodid_chr,to_char(startdate_dat,'yyyy-mm-dd') as startdate,to_char(enddate_dat,'yyyy-mm-dd') as enddate,to_char(startdate_dat,'yyyy-mm-dd') || ' �� ' || to_char(enddate_dat,'yyyy-mm-dd') as period
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

        #region ������������ID  ŷ����ΰ  2004-06-05
        /// <summary>
        /// ������������ID
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

        #region ��ѯ�����ڱ������
        /// <summary>
        /// ��ѯ�����ת�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_intRow">����</param>
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

        #region ��ȡ���е�������� created by weiling.huang at 2005-9-29
        /// <summary>
        /// ��ȡ���е�������� created by weiling.huang at 2005-9-29
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

        #region �����ת
        #region ���������ת
        /// <summary>
        ///  ���������ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�����ת����</param>
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

        #region �޸������ת
        /// <summary>
        /// �޸������ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�����ת����</param>
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

        #region ɾ�������ת
        /// <summary>
        /// ɾ�������ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
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

        #region ģ����ѯ�����ת
        /// <summary>
        /// ģ����ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
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

        #region ���ⷿ��ѯ�����ת
        /// <summary>
        /// ���ⷿ��ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ⷿ����</param>
        /// <param name="p_objResult">�������</param>
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

        #region �������ڲ�ѯ�����ת
        /// <summary>
        /// �������ڲ�ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResult">�������</param>
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

        #region ���ⷿ�������ڲ�ѯ�����ת
        /// <summary>
        /// ���ⷿ�������ڲ�ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <param name="p_objResult">�������</param>
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

        #region ����ת����Ա��ѯ�����ת
        /// <summary>
        /// ����ת����Ա��ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">����Ա����</param>
        /// <param name="p_objResult">�������</param>
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

        #region ����תʱ��β�ѯ�����ת
        /// <summary>
        /// ����תʱ��β�ѯ�����ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResult">�������</param>
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

        #region ��ѯ���е������ת
        /// <summary>
        /// ��ѯ���е������ת
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objResult">�������</param>
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

        #region ��ѯ��ת�˼�ʱ��
        /// <summary>
        /// ��ѯ��ת�˼�ʱ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <param name="p_strEmp">����Ա</param>
        /// <param name="p_strOperDate">����ʱ��</param>
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