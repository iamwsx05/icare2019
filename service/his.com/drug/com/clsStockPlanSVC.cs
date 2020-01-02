using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.MedicineStoreService
{
    /// <summary>
    /// �ɹ��ƻ���
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsStockPlanSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region �����������ϸ������
        /// <summary>
        /// �����������ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intCommit">���漴���</param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPlanMedInfo( int m_intCommit, ref clsMS_StockPlan_VO m_objMainVo, ref clsMS_StockPlan_Detail_VO[] m_objDetailArr)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL = @"insert into t_ms_stockplan a
            (a.seriesid_int, a.stockplanid_vchr, a.state_int, a.storageid_chr,
             a.vendorid_chr, a.stockplan_dat, a.neworder_dat,a.exam_dat,
                a.comment_vchr,a.makerid_chr,a.examerid_chr)
            values (?, ?, ?, ?,?, ?, ?,?,?,?,?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsMS_PublicSVC objPubSvc = new clsMS_PublicSVC();
                string m_strTempId = string.Empty;
                objPubSvc.m_lngGetSequence( "SEQ_MS_STOCKPLAN", out m_objMainVo.m_lngSERIESID_INT);
                objPubSvc.m_lngGetNewIdByName( "t_ms_stockplan", "stockplanid_vchr", m_objMainVo.m_datNEWORDER_DAT, ref m_strTempId);
                m_objMainVo.m_strSTOCKPLANID_VCHR = m_objMainVo.m_datNEWORDER_DAT.ToString("yyMMdd") + "10" + m_strTempId;
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(11, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_lngSERIESID_INT;
                objDataParm[1].Value = m_objMainVo.m_strSTOCKPLANID_VCHR;
                if (m_intCommit == 1)
                {
                    objDataParm[2].Value = 2;
                    objDataParm[7].Value = m_objMainVo.m_datEXAM_DAT;
                }
                else
                {
                    objDataParm[2].Value = 1;
                    objDataParm[7].Value = DateTime.MinValue;
                }
                objDataParm[3].Value = m_objMainVo.m_strSTORAGEID_CHR;
                objDataParm[4].Value = m_objMainVo.m_strVENDORID_CHR;
                objDataParm[5].Value = m_objMainVo.m_datSTOCKPLAN_DAT;
                objDataParm[6].Value = m_objMainVo.m_datNEWORDER_DAT;
                objDataParm[8].Value = m_objMainVo.m_strCOMMENT_VCHR;
                objDataParm[9].Value = m_objMainVo.m_strMAKERID_CHR;
                objDataParm[10].Value = m_objMainVo.m_strEXAMERID_CHR;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                if (m_objDetailArr == null || m_objDetailArr.Length == 0)
                {
                    return -1;
                }

                strSQL = @"insert into t_ms_stockplan_detail a
                            (a.seriesid_int, a.seriesid2_int, a.medicineid_chr,
                             a.medicinename_vchr, a.medspec_vchr, a.unit_vchr,
                             a.amount, a.productorid_chr, a.status, a.remark_vchr,a.vendorid_chr,a.callprice_int,a.lastinstoragedate_dat
                            )                
                            values (?, ?, ?,?, ?, ?,?, ?, ?, ?,?,?,?)";
                //long[] lngSEQArr = null;
                //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_MS_STOCKPLANDETAIL", m_objDetailArr.Length, out lngSEQArr);
                DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Double, 
                    DbType.String, DbType.Double, DbType.String,DbType.String ,DbType.Double,DbType.DateTime };
                object[][] objValues = new object[13][];
                int intItemCount = m_objDetailArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }
                for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                {
                    m_objDetailArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_MS_STOCKPLANDETAIL"); // lngSEQArr[iRow];
                    objValues[0][iRow] = m_objDetailArr[iRow].m_lngSERIESID_INT;
                    m_objDetailArr[iRow].m_lngSERIESID2_INT = m_objMainVo.m_lngSERIESID_INT;
                    objValues[1][iRow] = m_objDetailArr[iRow].m_lngSERIESID2_INT;
                    objValues[2][iRow] = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[3][iRow] = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[4][iRow] = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = m_objDetailArr[iRow].m_strUNIT_VCHR;
                    objValues[6][iRow] = m_objDetailArr[iRow].m_dblAMOUNT;
                    objValues[7][iRow] = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                    objValues[8][iRow] = 1;
                    objValues[9][iRow] = m_objDetailArr[iRow].m_strREMARK_VCHR;
                    objValues[10][iRow] = m_objDetailArr[iRow].m_strVENDORID_CHR;
                    objValues[11][iRow] = m_objDetailArr[iRow].m_dblCALLPRICE_INT;
                    objValues[12][iRow] = m_objDetailArr[iRow].m_datLASTINSTORAGEDATE_DAT;
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �����������ϸ������
        /// <summary>
        /// �����������ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intCommit">�Ƿ����</param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdatePlanMedInfo( int p_intCommit, clsMS_StockPlan_VO m_objMainVo, ref clsMS_StockPlan_Detail_VO[] m_objDetailArr)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan a
       set a.vendorid_chr = ?,
       a.stockplan_dat = ?,
       a.comment_vchr = ?
       where a.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsMS_PublicSVC objPubSvc = new clsMS_PublicSVC();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(4, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_strVENDORID_CHR;
                objDataParm[1].Value = m_objMainVo.m_datSTOCKPLAN_DAT;
                objDataParm[2].Value = m_objMainVo.m_strCOMMENT_VCHR;
                objDataParm[3].Value = m_objMainVo.m_lngSERIESID_INT;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                if (m_objDetailArr == null || m_objDetailArr.Length == 0)
                {
                    return -1;
                }

                strSQL = @"update t_ms_stockplan_detail set status = -1 where seriesid2_int=?";
                objDataParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_lngSERIESID_INT;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);

                strSQL = @"insert into t_ms_stockplan_detail a
                            (a.seriesid_int, a.seriesid2_int, a.medicineid_chr,
                             a.medicinename_vchr, a.medspec_vchr, a.unit_vchr,
                             a.amount, a.productorid_chr, a.status, a.remark_vchr,a.vendorid_chr,a.callprice_int,a.lastinstoragedate_dat
                            )                
                            values (?, ?, ?,?, ?, ?,?, ?, ?, ?,?,?,?)";
                //long[] lngSEQArr = null;
                //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_MS_STOCKPLANDETAIL", m_objDetailArr.Length, out lngSEQArr);
                DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Double, 
                    DbType.String, DbType.Double, DbType.String,DbType.String ,DbType.Double,DbType.DateTime };
                object[][] objValues = new object[13][];
                int intItemCount = m_objDetailArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }

                for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                {
                    m_objDetailArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_MS_STOCKPLANDETAIL"); //lngSEQArr[iRow];
                    objValues[0][iRow] = m_objDetailArr[iRow].m_lngSERIESID_INT;
                    m_objDetailArr[iRow].m_lngSERIESID2_INT = m_objMainVo.m_lngSERIESID_INT;
                    objValues[1][iRow] = m_objDetailArr[iRow].m_lngSERIESID2_INT;
                    objValues[2][iRow] = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[3][iRow] = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[4][iRow] = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = m_objDetailArr[iRow].m_strUNIT_VCHR;
                    objValues[6][iRow] = m_objDetailArr[iRow].m_dblAMOUNT;
                    objValues[7][iRow] = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                    if (p_intCommit == 1)
                        objValues[8][iRow] = 2;
                    else
                        objValues[8][iRow] = 1;
                    objValues[9][iRow] = m_objDetailArr[iRow].m_strREMARK_VCHR;
                    objValues[10][iRow] = m_objDetailArr[iRow].m_strVENDORID_CHR;
                    objValues[11][iRow] = m_objDetailArr[iRow].m_dblCALLPRICE_INT;
                    objValues[12][iRow] = m_objDetailArr[iRow].m_datLASTINSTORAGEDATE_DAT;
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������ˮ��ɾ����ϸ
        /// <summary>
        /// ������ˮ��ɾ����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSeqid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelPlanMedDetail( long m_lngSeqid)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan_detail set status = -1 where seriesid_int=?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objDataParm);
                objDataParm[0].Value = m_lngSeqid;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ��ָ�����������Ϣ
        /// <summary>
        /// ɾ��ָ�����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMainStockPlan( long p_lngSEQ)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan
   set state_int = 0
 where seriesid_int = ?
   and state_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSEQ;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                if (lngRes > 0)
                {
                    lngRes = m_lngUpdateInStorageStatusByMainSEQ( 0, p_lngSEQ);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ɾ��ָ�����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMainStockPlan( long[] p_lngSEQ)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan
   set state_int = 0
 where seriesid_int = ?
   and state_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int iRow = 0; iRow < p_lngSEQ.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = p_lngSEQ[iRow];

                        long lngEff = -1;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int64 };

                    object[][] objValues = new object[1][];

                    int intItemCount = p_lngSEQ.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_lngSEQ[iRow];
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                if (lngRes > 0)
                {
                    lngRes = m_lngUpdateInStorageStatusByMainSEQ( 0, p_lngSEQ);
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

        #region ɾ��ָ����ϸ
        /// <summary>
        /// ɾ��ָ����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeq">����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteInStorage( long p_lngSeq)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_instorage_detal t set t.status = 0 where t.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSeq;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ������ϸ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">���º��״̬</param>
        /// <param name="p_lngMainSeq">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInStorageStatusByMainSEQ( int p_intStatus, long p_lngMainSeq)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan_detail t set t.status = ? where t.seriesid2_int = ? and t.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_intStatus;
                objDPArr[1].Value = p_lngMainSeq;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ������ϸ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">���º��״̬</param>
        /// <param name="p_lngMainSeqArr">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInStorageStatusByMainSEQ( int p_intStatus, long[] p_lngMainSeqArr)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan_detail t set t.status = ? where t.seriesid2_int = ? and t.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int iRow = 0; iRow < p_lngMainSeqArr.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                        objDPArr[0].Value = p_intStatus;
                        objDPArr[1].Value = p_lngMainSeqArr[iRow];

                        long lngEff = -1;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.Int64 };

                    object[][] objValues = new object[2][];

                    int intItemCount = p_lngMainSeqArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_intStatus;
                        objValues[1][iRow] = p_lngMainSeqArr[iRow];
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ������ϸ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">���º��״̬</param>
        /// <param name="p_lngSeq">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInStorageStatus( int p_intStatus, long p_lngSeq)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_instorage_detal t set t.status = ? where t.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_intStatus;
                objDPArr[1].Value = p_lngSeq;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ���������ϸ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">���º��״̬</param>
        /// <param name="p_lngSeqArr">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInStorageStatus( int p_intStatus, long[] p_lngSeqArr)
        {
            if (p_lngSeqArr == null)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_instorage_detal t set t.status = ? where t.seriesid_int = ?";

                long lngEff = -1;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    for (int iRow = 0; iRow < p_lngSeqArr.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                        objDPArr[0].Value = p_intStatus;
                        objDPArr[1].Value = p_lngSeqArr[iRow];

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        if (lngRes <= 0)
                        {
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.Int64 };

                    object[][] objValues = new object[2][];

                    int intItemCount = p_lngSeqArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��


                    }

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_intStatus;
                        objValues[1][iRow] = p_lngSeqArr[iRow];
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngEff, dbTypes);
                    if (lngEff < intItemCount)
                    {
                        throw new Exception();
                    }
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

        #region ��ȡ�ɹ���ϸ������
        /// <summary>
        /// ��ȡ�ɹ���ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStockPlanDetail( long p_lngSeries2ID, out DataTable p_dtbValue)
        {
            p_dtbValue = null;
            if (p_lngSeries2ID < 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.seriesid_int,
			 a.seriesid2_int,
			 a.medicineid_chr,
			 a.medicinename_vchr,
			 a.medspec_vchr,
			 a.amount,
			 a.productorid_chr,
			 a.unit_vchr,
			 a.status,
			 a.remark_vchr,
			 b.assistcode_chr,
			 a.callprice_int,
			 a.lastinstoragedate_dat,
			 a.vendorid_chr,
			 c.vendorname_vchr vendorname,
			 amount * callprice_int stocksum
	from t_ms_stockplan_detail a
	left join t_bse_medicine b on b.medicineid_chr = a.medicineid_chr
	left join t_bse_vendor c on c.vendorid_chr = a.vendorid_chr
 where a.seriesid2_int = ?
	 and a.status <> -1
 order by a.seriesid_int";
                //and a.status = 1

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSeries2ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbValue, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="lngSEQ">��������</param>
        /// <param name="p_strExamer">�����</param>
        /// <param name="p_datExamDate">���ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitStockPlan( long lngSEQ, string p_strExamer, DateTime p_datExamDate)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ms_stockplan t set t.state_int = 2,t.examerid_chr = ?,t.exam_dat = ? where t.seriesid_int = ?";

                long lngEff = -1;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                //for (int iRow = 0; iRow < lngSEQ.Length; iRow++)
                //{
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strExamer;
                objDPArr[1].Value = p_datExamDate;
                objDPArr[2].Value = lngSEQ;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                {
                    throw new Exception();
                }
                //}

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeq">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnCommit( long[] p_lngSeq)
        {
            if (p_lngSeq == null || p_lngSeq.Length == 0)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                string strSQL = @"update t_ms_stockplan set state_int=1 where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                long lngEff = -1;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    DateTime dtmNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    for (int iRow = 0; iRow < p_lngSeq.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = p_lngSeq[iRow];

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int64 };

                    object[][] objValues = new object[1][];

                    int intItemCount = p_lngSeq.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��
                    }

                    DateTime dtmNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_lngSeq[iRow];
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ��ȡ�����������
        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmBeginDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbValue">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStockPlan( DateTime p_dtmBeginDate, DateTime p_dtmEndDate, string p_strStorageID, out DataTable p_dtbValue)
        {
            p_dtbValue = null;
            if (p_strStorageID == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.seriesid_int,
			 a.stockplanid_vchr,
			 a.state_int,
			 a.storageid_chr,
			 a.vendorid_chr,
			 a.stockplan_dat,
			 a.neworder_dat,
			 a.exam_dat,
			 a.comment_vchr,
			 a.makerid_chr,
			 a.examerid_chr,
			 b.vendorname_vchr,
			 case state_int
				 when 1 then
					'����'
				 when 2 then
					'���'
				 when 0 then
					'ɾ��'
				 else
					''
			 end statedesc,
			 c.lastname_vchr makername,
			 d.lastname_vchr examername
	from t_ms_stockplan a
 inner join t_bse_vendor b on a.vendorid_chr = b.vendorid_chr
	left outer join t_bse_employee c on c.empid_chr = a.makerid_chr
	left outer join t_bse_employee d on d.empid_chr = a.examerid_chr
 where a.storageid_chr = ?
	 and a.stockplan_dat between ? and ?	 
 order by a.seriesid_int";
                //and a.state_int <> 0

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strStorageID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmBeginDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEndDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbValue, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmBeginDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strMedicineName">ҩƷ����</param>
        /// <param name="p_strVendorName">��Ӧ������</param>
        /// <param name="p_strStockPlanID">���ݺ�</param>
        /// <param name="p_strMedicineType">ҩƷ����</param>
        /// <param name="p_dtbValue">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStockPlan( DateTime p_dtmBeginDate, DateTime p_dtmEndDate, string p_strStorageID,
            string p_strMedicineName, string p_strVendorName, string p_strStockPlanID, string p_strMedicineType, out DataTable p_dtbValue)
        {
            p_dtbValue = null;
            if (p_strStorageID == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct a.seriesid_int,
			 a.stockplanid_vchr,
			 a.state_int,
			 a.storageid_chr,
			 a.vendorid_chr,
			 a.stockplan_dat,
			 a.neworder_dat,
			 a.exam_dat,
			 a.comment_vchr,
			 a.makerid_chr,
			 a.examerid_chr,
			 b.vendorname_vchr,
			 case state_int
				 when 1 then
					'����'
				 when 2 then
					'���'
				 when 0 then
					'ɾ��'
				 else
					''
			 end statedesc,
			 c.lastname_vchr makername,
			 d.lastname_vchr examername
  from t_ms_stockplan a
 inner join t_bse_vendor b on a.vendorid_chr = b.vendorid_chr
 inner join (select i.seriesid_int,
                    i.medicinename_vchr,
                    i.seriesid2_int,
                    j.medicinetypeid_chr,
                    j.assistcode_chr
               from t_ms_stockplan_detail i, t_bse_medicine j
              where i.medicineid_chr = j.medicineid_chr) h on a.seriesid_int =
                                                              h.seriesid2_int
 left outer join t_bse_employee c on c.empid_chr = a.makerid_chr
 left outer join t_bse_employee d on d.empid_chr = a.examerid_chr
 where a.storageid_chr = ?
   and a.stockplan_dat between ? and ?
   and (h.medicinename_vchr like ?
   or h.assistcode_chr like ?)
   and b.vendorname_vchr like ?
   and a.stockplanid_vchr like ?
   and h.medicinetypeid_chr like ?   
 order by a.seriesid_int";
                //and i.status = 1
                //and a.state_int <> 0

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_strStorageID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmBeginDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEndDate;
                objDPArr[3].Value = p_strMedicineName + "%";
                objDPArr[4].Value = p_strMedicineName + "%";
                objDPArr[5].Value = p_strVendorName + "%";
                objDPArr[6].Value = p_strStockPlanID + "%";
                if (p_strMedicineType != "0")
                {
                    objDPArr[7].Value = p_strMedicineType + "%";
                }
                else
                {
                    objDPArr[7].Value = "%";
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbValue, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���״̬
        /// <summary>
        /// ���״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBillNo">����</param>
        /// <param name="p_intCommitStatus">״̬</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetCommitStatus( string p_strBillNo, out int p_intCommitStatus)
        {
            p_intCommitStatus = -1;

            long lngRes = 0;
            try
            {
                string strSQL = @"select state_int from t_ms_stockplan where stockplanid_vchr = ?";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBillNo;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_intCommitStatus = Convert.ToInt16(dtbValue.Rows[0]["state_int"]);
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

        #region �Զ�����ɹ�����
        /// <summary>
        /// �Զ�����ɹ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID��</param>
        /// <param name="p_strMedicineID">ҩƷID��</param>
        /// <param name="p_intAmount">�ɹ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetAmount( string p_strStorageID, string p_strMedicineID, out double p_intAmount)
        {
            p_intAmount = 0;

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.medicineid_chr,
			 sum(b.realgross_int) realgross_int,
			 decode(a.tiptoplimit_int, null, 0, a.tiptoplimit_int) tiptoplimit_int,
			 decode(a.neaplimit_int, null, 0, a.neaplimit_int) neaplimit_int
	         from t_bse_medicine a
	         left join t_ms_storage_detail b on b.medicineid_chr = a.medicineid_chr
             where b.storageid_chr = ? and a.medicineid_chr = ? and b.status = 1
             group by a.medicineid_chr, a.tiptoplimit_int, a.neaplimit_int";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strStorageID;
                objDPArr[1].Value = p_strMedicineID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                double intTemp = 0;
                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_intAmount = Convert.ToDouble(dtbValue.Rows[0]["tiptoplimit_int"]);
                    if (Convert.ToString(dtbValue.Rows[0]["realgross_int"]) == "")
                        intTemp = 0;
                    else
                        intTemp = Convert.ToDouble(dtbValue.Rows[0]["realgross_int"]);
                }
                if (p_intAmount >= intTemp)
                {
                    p_intAmount -= intTemp;
                }
                else
                {
                    p_intAmount = 0;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region �Զ�����ɹ�����
        /// <summary>
        /// �Զ�����ɹ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID">ҩƷID��</param>
        /// <param name="p_intAmount">�ɹ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetAmount( string p_strStorageID, string p_strMedicineID, out double p_intRealAmount, out double p_intTopAmount, out double p_intNeapAmount)
        {
            p_intRealAmount = 0;
            p_intTopAmount = 0;
            p_intNeapAmount = 0;

            long lngRes = 0;
            try
            {
                string strSQL = @"select a.medicineid_chr,
			 sum(b.realgross_int) realgross_int,
			 decode(a.tiptoplimit_int, null, 0, a.tiptoplimit_int) tiptoplimit_int,
			 decode(a.neaplimit_int, null, 0, a.neaplimit_int) neaplimit_int
	         from t_bse_medicine a
	         left join t_ms_storage_detail b on b.medicineid_chr = a.medicineid_chr
             where b.storageid_chr = ? and a.medicineid_chr = ? and b.status = 1
             group by a.medicineid_chr, a.tiptoplimit_int, a.neaplimit_int";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strStorageID;
                objDPArr[1].Value = p_strMedicineID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    if (Convert.ToString(dtbValue.Rows[0]["realgross_int"]) == "")
                        p_intRealAmount = 0;
                    else
                        p_intRealAmount = Convert.ToDouble(dtbValue.Rows[0]["realgross_int"]);
                    p_intTopAmount = Convert.ToDouble(dtbValue.Rows[0]["tiptoplimit_int"]);
                    p_intNeapAmount = Convert.ToDouble(dtbValue.Rows[0]["neaplimit_int"]);

                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region ��ȡ��ϸ������(��ӡ)
        /// <summary>
        /// ��ȡ��ϸ������(��ӡ)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_intState">��״̬��ɾ��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStockPlanForPrint( long p_lngSeries2ID, int p_intState, out DataTable p_dtbValue)
        {
            p_dtbValue = null;
            if (p_lngSeries2ID < 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                StringBuilder strSQL = new StringBuilder(@"select d.assistcode_chr,a.medicineid_chr,a.medicinename_vchr,a.medspec_vchr,
a.productorid_chr,a.amount,a.remark_vchr,b.storageid_chr,c.storagename_vchr,
b.stockplanid_vchr,b.state_int,b.stockplan_dat,b.neworder_dat
from t_ms_stockplan_detail a,t_ms_stockplan b,t_bse_storage c,t_bse_medicine d
where a.seriesid2_int = b.seriesid_int(+)
and b.storageid_chr = c.storageid_chr(+)
and a.medicineid_chr = d.medicineid_chr(+)");
                if (p_intState == 0)//ɾ��
                {
                    strSQL.Append(" and a.status = 0 and b.state_int = 0 and a.seriesid2_int = ? ");
                }
                else
                {
                    strSQL.Append(" and a.status = 1 and (b.state_int = 1 or b.state_int = 2) and a.seriesid2_int = ? ");
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSeries2ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbValue, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��ϸ������(��ӡ)̨ɽ
        /// <summary>
        /// ��ȡ��ϸ������(��ӡ)̨ɽ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_intState">��״̬��ɾ��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStockPlanForPrintTaiShan( long p_lngSeries2ID, int p_intState, out DataTable p_dtbValue)
        {
            p_dtbValue = null;
            if (p_lngSeries2ID < 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                StringBuilder strSQL = new StringBuilder(@"select d.assistcode_chr,
			 a.medicineid_chr,
			 a.medicinename_vchr,
			 a.medspec_vchr,
			 a.unit_vchr,
			 a.productorid_chr,
			 a.amount,
			 a.remark_vchr,
			 b.storageid_chr,
			 c.storagename_vchr,
			 b.stockplanid_vchr,
			 b.state_int,
			 b.stockplan_dat,
			 b.neworder_dat,
			 e.currentgross_num,
			 a.callprice_int,
			 a.amount*a.callprice_int stocksum
	from t_ms_stockplan_detail a,
			 t_ms_stockplan        b,
			 t_bse_storage         c,
			 t_bse_medicine        d,
			 t_ms_storage          e
 where a.seriesid2_int = b.seriesid_int(+)
	 and b.storageid_chr = c.storageid_chr(+)
	 and a.medicineid_chr = d.medicineid_chr(+)
	 and a.medicineid_chr = e.medicineid_chr(+)");
                if (p_intState == 0)//ɾ��
                {
                    strSQL.Append(" and a.status = 0 and b.state_int = 0 and a.seriesid2_int = ? ");
                }
                else
                {
                    strSQL.Append(" and a.status = 1 and (b.state_int = 1 or b.state_int = 2) and a.seriesid2_int = ? ");
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSeries2ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbValue, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


        #region �Զ����ɲɹ���ϸ
        /// <summary>
        /// �Զ����ɲɹ���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbGenerate">�ɹ���ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDetailForGenerate( string p_strStorageID, ref DataTable p_dtbGenerate)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.medicineid_chr,
			 t.assistcode_chr,
			 t.medicinename_vchr,
			 t.medspec_vchr,
			 t.opunit_chr,
			 t.tiptoplimit_int,
			 t.neaplimit_int,
             r.currentgross_num,
			 case
				 when t.neaplimit_int > r.currentgross_num then
					t.tiptoplimit_int - r.currentgross_num
				 else
					0
			 end as amount,
			 q.vendorname_vchr vendorname,
			 r.vendorid_chr,
			 case
				 when r.callprice_int = 0 then
					t.tradeprice_mny
				 else
					r.callprice_int
			 end as callprice_int,
			 max(o.instoragedate_dat) instoragedate_dat,
			 t.productorid_chr
	from t_bse_medicine t
	left join t_ms_storage r on r.medicineid_chr = t.medicineid_chr
	left join t_bse_vendor q on q.vendorid_chr = r.vendorid_chr
	left join t_ms_instorage_detal p on p.medicineid_chr = t.medicineid_chr
	left join t_ms_instorage o on o.seriesid_int = p.seriesid2_int
 where exists (select r.medicineroomid
					from t_ms_medicinestoreroomset r
				 where r.medicinetypeid_chr = t.medicinetypeid_chr
					 and r.medicineroomid = ?)
	 and ((o.state_int is null) or o.state_int <> 0)
 group by t.medicineid_chr,
					t.assistcode_chr,
					t.medicinename_vchr,
					t.medspec_vchr,
					t.opunit_chr,
					t.tiptoplimit_int,
					t.neaplimit_int,
					t.pycode_chr,
					r.vendorid_chr,
					q.vendorname_vchr,
					r.callprice_int,
					t.tradeprice_mny,
					t.productorid_chr,
					r.currentgross_num
having t.neaplimit_int > r.currentgross_num and t.tiptoplimit_int - r.currentgross_num > 0
 order by t.assistcode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbGenerate, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ��������ǰ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAssistCode">��ѯ����</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbMedicine">���ؽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBaseMedicineWithGross( string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;
            long lngRes = 0;
            if (p_strStorageID == null)
            {
                return -1;
            }

            if (p_strAssistCode == null)
            {
                p_strAssistCode = string.Empty;
            }

            try
            {
                string strSQL = @"select t.assistcode_chr,
       t.medicinename_vchr,
       t.medspec_vchr,
       t.opunit_chr,
       t.ipunit_chr,
       t.packqty_dec,
       t.productorid_chr,
       t.pycode_chr,
       t.wbcode_chr,
       t.medicineid_chr,
       t.ispoison_chr,
       t.ischlorpromazine2_chr,
       t.unitprice_mny,
       t.medicinetypeid_chr,
       t.tradeprice_mny,
       t.limitunitprice_mny,
       t.opchargeflg_int,
       t.ipchargeflg_int,
       t.ifstop_int,
       p.currentgross_num,
       q.vendorname_vchr as vendorname,
       p.vendorid_chr,
       case
         when p.callprice_int = 0 then
          t.tradeprice_mny
         else
          p.callprice_int
       end as callprice_int,
       max(o.instoragedate_dat) instoragedate_dat
  from t_bse_medicine t
  left join t_ms_storage p on p.medicineid_chr = t.medicineid_chr
  left join t_bse_vendor q on q.vendorid_chr = p.vendorid_chr
  left join t_ms_instorage_detal u on u.medicineid_chr = t.medicineid_chr
  left join t_ms_instorage o on o.seriesid_int = u.seriesid2_int
 where t.assistcode_chr like '%'
   and exists (select r.medicineroomid
          from t_ms_medicinestoreroomset r
         where r.medicinetypeid_chr = t.medicinetypeid_chr
           and r.medicineroomid = 0001)
   and ((o.state_int is null) or o.state_int <> 0)
 group by t.assistcode_chr,
          t.medicinename_vchr,
          t.medspec_vchr,
          t.opunit_chr,
          t.ipunit_chr,
          t.packqty_dec,
          t.productorid_chr,
          t.pycode_chr,
          t.wbcode_chr,
          t.medicineid_chr,
          t.ispoison_chr,
          t.ischlorpromazine2_chr,
          t.unitprice_mny,
          t.medicinetypeid_chr,
          t.tradeprice_mny,
          t.limitunitprice_mny,
          t.opchargeflg_int,
          t.ipchargeflg_int,
          t.ifstop_int,
          q.vendorname_vchr,
          p.vendorid_chr,
          p.callprice_int,
          t.tradeprice_mny,
					p.currentgross_num
 order by t.assistcode_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //objDPArr[0].Value = p_strStorageID;
                objDPArr[0].Value = p_strAssistCode + "%";
                objDPArr[1].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
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
