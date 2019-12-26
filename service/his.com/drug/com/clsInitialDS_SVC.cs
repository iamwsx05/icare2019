using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ������ʼ��
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInitialDS_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region ���ҩƷ����ʼ����
        /// <summary>
        /// ���ҩƷ����ʼ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedArr">ҩƷ����</param>
        /// <param name="p_lngSEQArr">�������ɵ�����</param>
        /// <param name="p_strIDArr">�������ɵĵ��ݺ�</param> 
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddMedicineToInitialDS( clsDS_Initial_VO[] p_objMedArr, out long[] p_lngSEQArr, out string[] p_strIDArr)
        {
            p_lngSEQArr = null;
            p_strIDArr = null;
            if (p_objMedArr == null || p_objMedArr.Length == 0)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"insert into t_ds_initial
  (seriesid_int,
   drugstoreid_chr,
   medicineid_chr,
   medicinename_vchr,
   medspec_vchr,
   ipamount,
   ipunit_chr,
   opamount,
   opunit_chr,
   packqty_dec,
   ipretailprice_int,
   opretailprice_int,
   ipwholesaleprice_int,
   opwholesaleprice_int,
   validperiod_dat,
   lotno_vchr,
   createrid,
   examerid,
   inaccounterid_chr,
   initialid_chr,
   productorid_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();

                string m_strTempId = string.Empty;
                string m_strMedStoreShortCode = string.Empty;
                string m_strINITIALID_CHR = string.Empty;
                string m_strParaValue = string.Empty;
                string m_strExistsInitialID = string.Empty;//�Ƿ���ڼ�¼�����ų�ʼ��ֻ��һ�����ݺ�
                m_lngCheckExistsInitialID( p_objMedArr[0].m_strDRUGSTOREID_CHR, out m_strExistsInitialID);

                if (m_strExistsInitialID == string.Empty)
                {
                    objPublic.m_lngGetSysParaByID( "8007", out m_strParaValue);
                    objPublic.m_lngGetMedStoreShortCodeByDeptid( p_objMedArr[0].m_strDRUGSTOREID_CHR, out m_strMedStoreShortCode);
                    DateTime dtmNow = DateTime.Now;
                    clsDS_Public_Supported_SVC clsPub = new clsDS_Public_Supported_SVC();
                    clsPub.m_lngGetCurrentDateTime(out dtmNow);
                    objPublic.m_lngGetNewIdByName( "t_ds_initial", "initialid_chr", m_strMedStoreShortCode, dtmNow, ref m_strTempId);
                    m_strINITIALID_CHR = m_strMedStoreShortCode + dtmNow.ToString("yyMMdd") + m_strParaValue.Split(';')[4] + m_strTempId;
                }
                else
                {
                    m_strINITIALID_CHR = m_strExistsInitialID;
                }
                long lngEff = -1;
                //long[] lngSEQArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;
                    //lngRes = objPublic.m_lngGetSequenceArr( "seq_ds_initial", p_objMedArr.Length, out lngSEQArr);
                    //if (lngSEQArr == null || lngSEQArr.Length == 0)
                    //{
                    //    return -1;
                    //}

                    //p_lngSEQArr = lngSEQArr;
                    //p_strIDArr = new string[lngSEQArr.Length];
                    for (int iRow = 0; iRow < p_objMedArr.Length; iRow++)
                    {
                        long seqId = objPublic.GetSeqNextVal("seq_ds_initial");
                        string strSEQ = seqId.ToString().PadLeft(4, '0'); //lngSEQArr[iRow].ToString().PadLeft(4, '0');

                        p_objMedArr[iRow].m_lngSERIESID_INT = seqId; //lngSEQArr[iRow];
                        p_objMedArr[iRow].m_strINITIALID_CHR = m_strINITIALID_CHR;
                        p_strIDArr[iRow] = p_objMedArr[iRow].m_strINITIALID_CHR;

                        objHRPServ.CreateDatabaseParameter(21, out objDPArr);
                        objDPArr[0].Value = p_objMedArr[iRow].m_lngSERIESID_INT;
                        objDPArr[1].Value = p_objMedArr[iRow].m_strDRUGSTOREID_CHR;
                        objDPArr[2].Value = p_objMedArr[iRow].m_strMEDICINEID_CHR;
                        objDPArr[3].Value = p_objMedArr[iRow].m_strMEDICINENAME_VCH;
                        objDPArr[4].Value = p_objMedArr[iRow].m_strMEDSPEC_VCHR;
                        objDPArr[5].Value = p_objMedArr[iRow].m_dblIPAMOUNT;
                        objDPArr[6].Value = p_objMedArr[iRow].m_strIPUNIT_CHR;
                        objDPArr[7].Value = p_objMedArr[iRow].m_dblOPAMOUNT;
                        objDPArr[8].Value = p_objMedArr[iRow].m_strOPUNIT_CHR;
                        objDPArr[9].Value = p_objMedArr[iRow].m_dblPACKQTY_DEC;
                        objDPArr[10].Value = p_objMedArr[iRow].m_dblIPRETAILPRICE_INT;
                        objDPArr[11].Value = p_objMedArr[iRow].m_dblOPRETAILPRICE_INT;
                        objDPArr[12].Value = p_objMedArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objDPArr[13].Value = p_objMedArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objDPArr[14].DbType = DbType.DateTime;
                        objDPArr[14].Value = p_objMedArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objDPArr[15].Value = p_objMedArr[iRow].m_strLOTNO_VCHR;
                        objDPArr[16].Value = p_objMedArr[iRow].m_strCREATERID;
                        objDPArr[17].Value = p_objMedArr[iRow].m_strEXAMERID;
                        objDPArr[18].Value = p_objMedArr[iRow].m_strINACCOUNTERID_CHR;
                        objDPArr[19].Value = p_objMedArr[iRow].m_strINITIALID_CHR;
                        objDPArr[20].Value = p_objMedArr[iRow].m_strPRODUCTORID_CHR;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int64,DbType.String,DbType.String,DbType.String,DbType.String,DbType.Double,
                        DbType.String,DbType.Double,DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.Double,
                        DbType.DateTime,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String};

                    object[][] objValues = new object[21][];

                    int intItemCount = p_objMedArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��
                    }


                    //lngRes = objPublic.m_lngGetSequenceArr( "seq_ms_initial", intItemCount, out lngSEQArr);
                    //if (lngSEQArr == null || lngSEQArr.Length == 0)
                    //{
                    //    return -1;
                    //}

                    //p_lngSEQArr = lngSEQArr;
                    //p_strIDArr = new string[lngSEQArr.Length];

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        long seqId = objPublic.GetSeqNextVal("seq_ms_initial");
                        string strSEQ = seqId.ToString().PadLeft(4, '0'); ; // lngSEQArr[iRow].ToString().PadLeft(4, '0');

                        p_objMedArr[iRow].m_lngSERIESID_INT = seqId;// lngSEQArr[iRow];
                        p_objMedArr[iRow].m_strINITIALID_CHR = m_strINITIALID_CHR;
                        p_strIDArr[iRow] = p_objMedArr[iRow].m_strINITIALID_CHR;

                        objValues[0][iRow] = p_objMedArr[iRow].m_lngSERIESID_INT;
                        objValues[1][iRow] = p_objMedArr[iRow].m_strDRUGSTOREID_CHR;
                        objValues[2][iRow] = p_objMedArr[iRow].m_strMEDICINEID_CHR;
                        objValues[3][iRow] = p_objMedArr[iRow].m_strMEDICINENAME_VCH;
                        objValues[4][iRow] = p_objMedArr[iRow].m_strMEDSPEC_VCHR;
                        objValues[5][iRow] = p_objMedArr[iRow].m_dblIPAMOUNT;
                        objValues[6][iRow] = p_objMedArr[iRow].m_strIPUNIT_CHR;
                        objValues[7][iRow] = p_objMedArr[iRow].m_dblOPAMOUNT;
                        objValues[8][iRow] = p_objMedArr[iRow].m_strOPUNIT_CHR;
                        objValues[9][iRow] = p_objMedArr[iRow].m_dblPACKQTY_DEC;
                        objValues[10][iRow] = p_objMedArr[iRow].m_dblIPRETAILPRICE_INT;
                        objValues[11][iRow] = p_objMedArr[iRow].m_dblOPRETAILPRICE_INT;
                        objValues[12][iRow] = p_objMedArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objValues[13][iRow] = p_objMedArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objValues[14][iRow] = p_objMedArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objValues[15][iRow] = p_objMedArr[iRow].m_strLOTNO_VCHR;
                        objValues[16][iRow] = p_objMedArr[iRow].m_strCREATERID;
                        objValues[17][iRow] = p_objMedArr[iRow].m_strEXAMERID;
                        objValues[18][iRow] = p_objMedArr[iRow].m_strINACCOUNTERID_CHR;
                        objValues[19][iRow] = p_objMedArr[iRow].m_strINITIALID_CHR;
                        objValues[20][iRow] = p_objMedArr[iRow].m_strPRODUCTORID_CHR;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        ///  �Ƿ���ڼ�¼�����ų�ʼ��ֻ��һ�����ݺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID"></param>
        /// <param name="p_strInitialID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckExistsInitialID( string p_strDrugStoreID, out string p_strInitialID)
        {
            p_strInitialID = string.Empty;
            long lngRes = 0;
            DataTable dtTemp = null;
            string strSQL = @"select initialid_chr
	from (select a.initialid_chr
					from t_ds_initial a
				 where a.drugstoreid_chr = ?
				 order by a.seriesid_int)
 where rownum = 1";
            try
            {

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objParm);
                objParm[0].Value = p_strDrugStoreID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtTemp, objParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (lngRes > 0 && dtTemp.Rows.Count == 1)
                {
                    p_strInitialID = dtTemp.Rows[0][0].ToString();
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

        #region �޸�ҩ������ʼ��ҩƷ
        /// <summary>
        /// �޸�ҩ������ʼ��ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedArr">ҩƷ��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyInitialMedicine( clsDS_Initial_VO[] p_objMedArr)
        {
            if (p_objMedArr == null || p_objMedArr.Length == 0)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"update t_ds_initial
       set medicineid_chr = ?,
       medicinename_vchr = ?,
       medspec_vchr = ?,
       ipamount = ?,
       ipunit_chr = ?,
       opamount = ?,
       opunit_chr = ?,
       packqty_dec = ?,
       ipretailprice_int = ?,
       opretailprice_int = ?,
       ipwholesaleprice_int = ?,
       opwholesaleprice_int = ?,
       validperiod_dat = ?,
       lotno_vchr = ?,
       productorid_chr = ?
       where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                long lngEff = -1;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;

                    for (int iRow = 0; iRow < p_objMedArr.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(16, out objDPArr);
                        objDPArr[0].Value = p_objMedArr[iRow].m_strMEDICINEID_CHR;
                        objDPArr[1].Value = p_objMedArr[iRow].m_strMEDICINENAME_VCH;
                        objDPArr[2].Value = p_objMedArr[iRow].m_strMEDSPEC_VCHR;
                        objDPArr[3].Value = p_objMedArr[iRow].m_dblIPAMOUNT;
                        objDPArr[4].Value = p_objMedArr[iRow].m_strIPUNIT_CHR;
                        objDPArr[5].Value = p_objMedArr[iRow].m_dblOPAMOUNT;
                        objDPArr[6].Value = p_objMedArr[iRow].m_strOPUNIT_CHR;
                        objDPArr[7].Value = p_objMedArr[iRow].m_dblPACKQTY_DEC;
                        objDPArr[8].Value = p_objMedArr[iRow].m_dblIPRETAILPRICE_INT;
                        objDPArr[9].Value = p_objMedArr[iRow].m_dblOPRETAILPRICE_INT;
                        objDPArr[10].Value = p_objMedArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objDPArr[11].Value = p_objMedArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objDPArr[12].DbType = DbType.DateTime;
                        objDPArr[12].Value = p_objMedArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objDPArr[13].Value = p_objMedArr[iRow].m_strLOTNO_VCHR;
                        objDPArr[14].Value = p_objMedArr[iRow].m_strPRODUCTORID_CHR;
                        objDPArr[15].Value = p_objMedArr[iRow].m_lngSERIESID_INT;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String,DbType.String,DbType.String,DbType.Double,DbType.String,DbType.Double,
                        DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.DateTime,DbType.String,
                        DbType.String,DbType.Int64};

                    object[][] objValues = new object[16][];

                    int intItemCount = p_objMedArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��
                    }

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_objMedArr[iRow].m_strMEDICINEID_CHR;
                        objValues[1][iRow] = p_objMedArr[iRow].m_strMEDICINENAME_VCH;
                        objValues[2][iRow] = p_objMedArr[iRow].m_strMEDSPEC_VCHR;
                        objValues[3][iRow] = p_objMedArr[iRow].m_dblIPAMOUNT;
                        objValues[4][iRow] = p_objMedArr[iRow].m_strIPUNIT_CHR;
                        objValues[5][iRow] = p_objMedArr[iRow].m_dblOPAMOUNT;
                        objValues[6][iRow] = p_objMedArr[iRow].m_strOPUNIT_CHR;
                        objValues[7][iRow] = p_objMedArr[iRow].m_dblPACKQTY_DEC;
                        objValues[8][iRow] = p_objMedArr[iRow].m_dblIPRETAILPRICE_INT;
                        objValues[9][iRow] = p_objMedArr[iRow].m_dblOPRETAILPRICE_INT;
                        objValues[10][iRow] = p_objMedArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objValues[11][iRow] = p_objMedArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objValues[12][iRow] = p_objMedArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objValues[13][iRow] = p_objMedArr[iRow].m_strLOTNO_VCHR;
                        objValues[14][iRow] = p_objMedArr[iRow].m_strPRODUCTORID_CHR;
                        objValues[15][iRow] = p_objMedArr[iRow].m_lngSERIESID_INT;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷ
        /// <summary>
        /// ����ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objNew">�����ҩƷ</param>
        /// <param name="p_objModify">�޸ĵ�ҩƷ</param>
        /// <param name="p_lngNewSeqArr">������¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveMedicineInfo( clsDS_Initial_VO[] p_objNew, clsDS_Initial_VO[] p_objModify, out long[] p_lngNewSeqArr, out string[] p_strIDArr)
        {
            p_lngNewSeqArr = null;
            p_strIDArr = null;
            if ((p_objNew == null || p_objNew.Length == 0) && (p_objModify == null || p_objModify.Length == 0))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                if (p_objNew != null && p_objNew.Length > 0)
                {
                    lngRes = m_lngAddMedicineToInitialDS( p_objNew, out p_lngNewSeqArr, out p_strIDArr);

                    if (lngRes <= 0)
                    {
                        return -1;
                    }
                }

                if (p_objModify != null && p_objModify.Length > 0)
                {
                    lngRes = m_lngModifyInitialMedicine( p_objModify);
                    if (lngRes <= 0)
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

        #region ɾ��ָ����ʼ���
        /// <summary>
        /// ɾ��ָ����ʼ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedicineInitial( long p_lngSEQ)
        {
            if (p_lngSEQ <= 0)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                string strSQL = @"delete from t_ds_initial where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngSEQ;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���������

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQArr">���ҩƷ���к�</param>
        /// <param name="p_strEMPID">�����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCommitUser( long[] p_lngSEQArr, string p_strEMPID)
        {
            if (p_lngSEQArr == null || p_lngSEQArr.Length == 0 || p_strEMPID == null)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                string strSQL = @"update t_ds_initial set examerid = ?,exam_dat = sysdate where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                long lngEff = -1;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objDPArr = null;

                    for (int iRow = 0; iRow < p_lngSEQArr.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                        objDPArr[0].Value = p_strEMPID;
                        objDPArr[1].Value = p_lngSEQArr[iRow];

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int64 };

                    object[][] objValues = new object[2][];

                    int intItemCount = p_lngSEQArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��
                    }

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        objValues[0][iRow] = p_strEMPID;
                        objValues[1][iRow] = p_lngSEQArr[iRow];
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion     

        #region �޸Ŀ��
        /// <summary>
        /// �޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡������ӿ��,���������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorage( string p_strStorageID, clsDS_StorageDetail_VO[] p_objDetail, Int16 intType)
        {
            if (p_objDetail == null || p_objDetail.Length == 0)
            {
                return -1;
            }
            long lngRes = -1;
            bool p_blnHasDetail;
            long p_lngSeriesID;
            clsInitialDS_Supported_SVC objSelect = new clsInitialDS_Supported_SVC();
            for (int intRow = 0; intRow < p_objDetail.Length; intRow++)
            {
                //�жϵ�ǰҩƷ�Ƿ��Ѵ��ڿ��������
                objSelect.m_lngCheckHasStorage( p_objDetail[intRow].m_strMEDICINEID_CHR, p_strStorageID, out p_blnHasDetail, out p_lngSeriesID);
                if (p_blnHasDetail)
                {
                    //�޸Ŀ����������
                    lngRes = m_lngModifyStorageGross(p_strStorageID, p_objDetail, intType, p_lngSeriesID);
                }
                else
                {
                    //��ӿ�������¼
                    lngRes = m_lngAddStorageGross(p_strStorageID, p_objDetail);
                }
                if (lngRes != -1)
                {
                    //��ӿ����ϸ���¼
                    lngRes = m_lngAddStorage_detail( p_objDetail);
                }
                else
                {
                    return -1;
                }
            }
            return lngRes;

        }

        #endregion

        #region �޸Ŀ����������
        /// <summary>
        /// �޸Ŀ����������
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡������ӿ��,���������</param>
        /// <param name="p_lngSeriesID">����˳��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageGross(string p_strStorageID, clsDS_StorageDetail_VO[] p_objDetail, Int16 intType, long p_lngSeriesID)
        {
            //�޸Ŀ������
            long lngRes = -1;
            string strSQL;
            strSQL = @"update t_ds_storage
   set opcurrentgross_num = opcurrentgross_num + ?,
       ipcurrentgross_num = ipcurrentgross_num + ?
 where seriesid_int = ?";

            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Int32 };
            clsHRPTableService objHRPServ = new clsHRPTableService();
            object[][] objValues = new object[3][];
            int intItemCount = p_objDetail.Length;
            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[intItemCount];//��ʼ��
            }

            for (int iRow = 0; iRow < intItemCount; iRow++)
            {
                //�жϵ�ǰΪ��ӿ�������Ǽ�С
                if (intType == 1)
                {
                    objValues[0][iRow] = p_objDetail[iRow].m_dblOPREALGROSS_INT;
                    objValues[1][iRow] = p_objDetail[iRow].m_dblIPREALGROSS_INT;
                }
                else
                {
                    objValues[0][iRow] = 0 - p_objDetail[iRow].m_dblOPREALGROSS_INT;
                    objValues[1][iRow] = 0 - p_objDetail[iRow].m_dblIPREALGROSS_INT;
                }

                objValues[2][iRow] = p_lngSeriesID;

            }
            try
            {
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ӿ�������¼
        [AutoComplete]
        public long m_lngAddStorageGross(string p_strStorageID, clsDS_StorageDetail_VO[] p_objDetail)
        {
            long lngRes = -1;
            string strSQL;
           // long[] lngSEQArr = null;
            strSQL = @"insert into t_ds_storage
  (seriesid_int,
   medicineid_chr,
   medicinename_vchr,
   medspec_vchr,
   opcurrentgross_num,
   opunit_chr,
   ipcurrentgross_num,
   ipunit_chr,
   drugstoreid_chr
   )
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?)";

            DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, 
            DbType.Double, DbType.String,DbType.Double,DbType.String,DbType.Int32};
            clsDS_Public_Supported_SVC clsPub = new clsDS_Public_Supported_SVC();
            //lngRes = clsPub.m_lngGetSequenceArr(  "SEQ_DS_STORAGE", p_objDetail.Length, out lngSEQArr);

            clsHRPTableService objHRPServ = new clsHRPTableService();
            object[][] objValues = new object[9][];
            int intItemCount = p_objDetail.Length;
            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[intItemCount];//��ʼ��
            }
            for (int iRow = 0; iRow < intItemCount; iRow++)
            {
                objValues[0][iRow] = clsPub.GetSeqNextVal("SEQ_DS_STORAGE");    // lngSEQArr;
                objValues[1][iRow] = p_objDetail[iRow].m_strDRUGSTOREID_CHR;
                objValues[2][iRow] = p_objDetail[iRow].m_strMEDICINEID_CHR;
                objValues[3][iRow] = p_objDetail[iRow].m_strMEDICINENAME_VCHR;
                objValues[4][iRow] = p_objDetail[iRow].m_strMEDSPEC_VCHR;
                objValues[5][iRow] = p_objDetail[iRow].m_strLOTNO_VCHR;
                objValues[6][iRow] = p_objDetail[iRow].m_dblIPREALGROSS_INT;
                objValues[7][iRow] = p_objDetail[iRow].m_strIPUNIT_CHR;
                objValues[8][iRow] = p_objDetail[iRow].m_dblOPREALGROSS_INT;
            }
            try
            {
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ӿ����ϸ���¼
        /// <summary>
        /// ��ӿ����ϸ����¼
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objDetail">�����ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddStorage_detail( clsDS_StorageDetail_VO[] p_objDetail)
        {
            long lngRes = -1;
            string strSQL;
           // long[] lngSEQArr = null;
            //��ͬҩƷ����������ͬ����ϲ�����������(20080626�˷������ϣ���Ϊֱ�Ӳ�����)
            //clsDS_StorageDetail_VO[] p_objUpdateDetail = null;
            //bool blnExist = false;
            //for (int i1 = 0; i1 < p_objDetail.Length; i1++)
            //{
            //    m_lngCheckExistMedicine( p_objDetail[i1].m_strDRUGSTOREID_CHR, p_objDetail[i1].m_strMEDICINEID_CHR, p_objDetail[i1].m_strLOTNO_VCHR, p_objDetail[i1].m_strDSINSTOREID_VCHR, out blnExist);
            //    if (blnExist)
            //    {
            //        lngRes = m_lngUpdateStorage_detail( p_objDetail[i1]);
            //    }
            //    else
            //    {
            //        lngRes = m_lngAddStorage_detail( p_objDetail[i1]);
            //    }
            //}
            //return lngRes;
            strSQL = @"insert into t_ds_storage_detail
              (seriesid_int,
               drugstoreid_chr,
               medicineid_chr,
               medicinename_vchr,
               medspec_vchr,
               lotno_vchr,   
               iprealgross_int,   
               ipunit_chr,
               oprealgross_int,
               opunit_chr,   
               packqty_dec,
               ipretailprice_int,
               opretailprice_int,
               ipwholesaleprice_int,
               opwholesaleprice_int,
               validperiod_dat ,
               instoreid_vchr,
               instoragedate_dat,   
               adjustpriceman_chr,
               adjustpricedate_dat,
               opavailablegross_num,
               ipavailablegross_num,
               status,
               productorid_chr,
               dsinstoreid_vchr,
               dsinstoragedate_dat
               )
            values
              (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?,?,sysdate)";

            DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String,                                                                                                         
                            DbType.String, DbType.String,DbType.Double,DbType.String,DbType.Double, DbType.String,                                                                                                      
                            DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.DateTime,                                                                                                   
                            DbType.String,DbType.DateTime,DbType.String, DbType.DateTime,DbType.Double,DbType.Double,
                            DbType.Double,DbType.String,DbType.String};

            clsDS_Public_Supported_SVC clsPub = new clsDS_Public_Supported_SVC();
           // lngRes = clsPub.m_lngGetSequenceArr(  "SEQ_DS_STORAGE_detail", p_objDetail.Length, out lngSEQArr);
            clsHRPTableService objHRPServ = new clsHRPTableService();
            object[][] objValues = new object[25][];
            int intItemCount = p_objDetail.Length;
            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[intItemCount];//��ʼ��
            }
            for (int iRow = 0; iRow < intItemCount; iRow++)
            {
                objValues[0][iRow] = clsPub.GetSeqNextVal("SEQ_DS_STORAGE_detail"); // lngSEQArr[iRow];
                objValues[1][iRow] = p_objDetail[iRow].m_strDRUGSTOREID_CHR;
                objValues[2][iRow] = p_objDetail[iRow].m_strMEDICINEID_CHR;
                objValues[3][iRow] = p_objDetail[iRow].m_strMEDICINENAME_VCHR;
                objValues[4][iRow] = p_objDetail[iRow].m_strMEDSPEC_VCHR;
                objValues[5][iRow] = p_objDetail[iRow].m_strLOTNO_VCHR;
                objValues[6][iRow] = p_objDetail[iRow].m_dblIPREALGROSS_INT;
                objValues[7][iRow] = p_objDetail[iRow].m_strIPUNIT_CHR;
                objValues[8][iRow] = p_objDetail[iRow].m_dblOPREALGROSS_INT;
                objValues[9][iRow] = p_objDetail[iRow].m_strOPUNIT_CHR;
                objValues[10][iRow] = p_objDetail[iRow].m_dblPACKQTY_DEC;
                objValues[11][iRow] = p_objDetail[iRow].m_dblIPRETAILPRICE_INT;
                objValues[12][iRow] = p_objDetail[iRow].m_dblOPRETAILPRICE_INT;
                objValues[13][iRow] = p_objDetail[iRow].m_dblIPWHOLESALEPRICE_INT;
                objValues[14][iRow] = p_objDetail[iRow].m_dblOPWHOLESALEPRICE_INT;
                objValues[15][iRow] = p_objDetail[iRow].m_dtmVALIDPERIOD_DAT;
                objValues[16][iRow] = p_objDetail[iRow].m_strINSTOREID_VCHR;
                objValues[17][iRow] = p_objDetail[iRow].m_dtmINSTORAGEDATE_DAT;
                objValues[18][iRow] = p_objDetail[iRow].m_strADJUSTPRICEMAN_CHR;
                objValues[19][iRow] = p_objDetail[iRow].m_dtmADJUSTPRICEDATE_DAT;
                objValues[20][iRow] = p_objDetail[iRow].m_dblOPREALGROSS_INT;
                objValues[21][iRow] = p_objDetail[iRow].m_dblIPREALGROSS_INT;
                objValues[22][iRow] = 1;
                objValues[23][iRow] = p_objDetail[iRow].m_strPRODUCTORID_CHR;
                objValues[24][iRow] = p_objDetail[iRow].m_strDSINSTOREID_VCHR;
                //objValues[25][iRow] = p_objDetail[iRow].m_dtmDSINSTORAGEDATE_DAT;
            }
            try
            {
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }

        /// <summary>
        /// ����Ƿ����ͬ���ŵ�ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugID">ҩ��</param>
        /// <param name="p_strMedicineID">ҩƷ</param>
        /// <param name="p_strLotno">����</param>
        /// <param name="p_strInstorageID">��ʼ������</param>
        /// <param name="p_blnExist">�Ƿ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckExistMedicine( string p_strDrugID, string p_strMedicineID, string p_strLotno, string p_strInstorageID, out bool p_blnExist)
        {
            p_blnExist = false;
            long lngRes = 0;

            try
            {
                string strSQL = @"select count(*)
	from t_ds_storage_detail a
 where a.status = 1
	 and a.drugstoreid_chr = ?
	 and a.medicineid_chr = ?
	 and a.lotno_vchr = ?
	 and a.dsinstoreid_vchr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strDrugID;
                objDPArr[1].Value = p_strMedicineID;
                objDPArr[2].Value = p_strLotno;
                objDPArr[3].Value = p_strInstorageID;

                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtbValue.Rows[0][0]) > 0)
                    {
                        p_blnExist = true;
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

        #region ��ӿ����ϸ���¼(����)
        /// <summary>
        /// ��ӿ����ϸ����¼(����)
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objDetail">�����ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddStorage_detail( clsDS_StorageDetail_VO p_objDetail)
        {
            long lngRes = -1;
            string strSQL;
            long lngSEQArr = 0;

            strSQL = @"insert into t_ds_storage_detail
  (seriesid_int,
   drugstoreid_chr,
   medicineid_chr,
   medicinename_vchr,
   medspec_vchr,
   lotno_vchr,   
   iprealgross_int,   
   ipunit_chr,
   oprealgross_int,
   opunit_chr,   
   packqty_dec,
   ipretailprice_int,
   opretailprice_int,
   ipwholesaleprice_int,
   opwholesaleprice_int,
   validperiod_dat ,
   instoreid_vchr,
   instoragedate_dat,   
   adjustpriceman_chr,
   adjustpricedate_dat,
   opavailablegross_num,
   ipavailablegross_num,
   status,
   productorid_chr,
   dsinstoreid_vchr,
   dsinstoragedate_dat
   )
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?,?,sysdate)";

            DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String,                                                                                                         
                DbType.String, DbType.String,DbType.Double,DbType.String,DbType.Double, DbType.String,                                                                                                      
                DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.DateTime,                                                                                                   
                DbType.String,DbType.DateTime,DbType.String, DbType.DateTime,DbType.Double,DbType.Double,
                DbType.Double,DbType.String,DbType.String};

            clsDS_Public_Supported_SVC clsPub = new clsDS_Public_Supported_SVC();
            lngRes = clsPub.m_lngGetSequence( "SEQ_DS_STORAGE_detail", out lngSEQArr);
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objDataParm = null;
            long lngAffected = -1;
            objHRPServ.CreateDatabaseParameter(25, out objDataParm);
            objDataParm[0].Value = lngSEQArr;
            objDataParm[1].Value = p_objDetail.m_strDRUGSTOREID_CHR;
            objDataParm[2].Value = p_objDetail.m_strMEDICINEID_CHR;
            objDataParm[3].Value = p_objDetail.m_strMEDICINENAME_VCHR;
            objDataParm[4].Value = p_objDetail.m_strMEDSPEC_VCHR;
            objDataParm[5].Value = p_objDetail.m_strLOTNO_VCHR;
            objDataParm[6].Value = p_objDetail.m_dblIPREALGROSS_INT;
            objDataParm[7].Value = p_objDetail.m_strIPUNIT_CHR;
            objDataParm[8].Value = p_objDetail.m_dblOPREALGROSS_INT;
            objDataParm[9].Value = p_objDetail.m_strOPUNIT_CHR;
            objDataParm[10].Value = p_objDetail.m_dblPACKQTY_DEC;
            objDataParm[11].Value = p_objDetail.m_dblIPRETAILPRICE_INT;
            objDataParm[12].Value = p_objDetail.m_dblOPRETAILPRICE_INT;
            objDataParm[13].Value = p_objDetail.m_dblIPWHOLESALEPRICE_INT;
            objDataParm[14].Value = p_objDetail.m_dblOPWHOLESALEPRICE_INT;
            objDataParm[15].Value = p_objDetail.m_dtmVALIDPERIOD_DAT;
            objDataParm[16].Value = p_objDetail.m_strINSTOREID_VCHR;
            objDataParm[17].Value = p_objDetail.m_dtmINSTORAGEDATE_DAT;
            objDataParm[18].Value = p_objDetail.m_strADJUSTPRICEMAN_CHR;
            objDataParm[19].Value = p_objDetail.m_dtmADJUSTPRICEDATE_DAT;
            objDataParm[20].Value = p_objDetail.m_dblOPREALGROSS_INT;
            objDataParm[21].Value = p_objDetail.m_dblIPREALGROSS_INT;
            objDataParm[22].Value = 1;
            objDataParm[23].Value = p_objDetail.m_strPRODUCTORID_CHR;
            objDataParm[24].Value = p_objDetail.m_strDSINSTOREID_VCHR;

            try
            {
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ��ӿ����ϸ���¼(����)
        /// <summary>
        /// ��ӿ����ϸ����¼(����)
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objDetail">�����ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStorage_detail( clsDS_StorageDetail_VO p_objDetail)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage_detail a
	 set a.opavailablegross_num = a.opavailablegross_num + ?,
			 a.ipavailablegross_num = a.ipavailablegross_num + ?,
			 a.oprealgross_int      = a.oprealgross_int + ?,
			 a.iprealgross_int      = a.iprealgross_int + ?
 where a.medicineid_chr = ?
	 and a.lotno_vchr = ?
	 and dsinstoreid_vchr = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objDataParm = null;
                long lngAffected = -1;
                objHRPServ.CreateDatabaseParameter(7, out objDataParm);
                objDataParm[0].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objDataParm[1].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objDataParm[2].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objDataParm[3].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objDataParm[4].Value = p_objDetail.m_strMEDICINEID_CHR;
                objDataParm[5].Value = p_objDetail.m_strLOTNO_VCHR;
                objDataParm[6].Value = p_objDetail.m_strDSINSTOREID_VCHR;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ҩ������ʼ��
        /// <summary>
        /// ���ҩ������ʼ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1���ӿ��,2�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitInitila( string p_strStorageID, clsDS_StorageDetail_VO[] p_objDetail, Int16 intType)
        {
            long lngRes = -1;
            lngRes = m_lngModifyStorage(  p_strStorageID, p_objDetail, intType);
            return lngRes;
        }
        #endregion      

        #region ���ҩƷ
        /// <summary>
        /// ���ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetailArr">�����ϸ</param>
        /// <param name="p_objStorageArr">�����������</param>
        /// <param name="p_lngSEQArr">���������</param>
        /// <param name="p_strEmpID">�����ID</param>
        /// <param name="p_blnIsImmAccount">�Ƿ���˼�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitMedicineInfo( clsDS_StorageDetail_VO[] p_objDetailArr, clsDS_Storage_VO[] p_objStorageArr, long[] p_lngSEQArr, string p_strEmpID, bool p_blnIsImmAccount)
        {
            if (p_objDetailArr == null || p_objDetailArr.Length == 0 || p_objStorageArr == null || p_objStorageArr.Length == 0)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                lngRes = m_lngAddStorage_detail( p_objDetailArr);
                if (lngRes <= 0)
                {
                    return -1;
                }

                System.Collections.Hashtable hstMedicine = new System.Collections.Hashtable();
                bool blnHasDetail = false;//�Ƿ��Ѵ���
                clsInitialDS_Supported_SVC objSelect = new clsInitialDS_Supported_SVC();
                for (int iRow = 0; iRow < p_objStorageArr.Length; iRow++)
                {
                    if (!hstMedicine.Contains(p_objStorageArr[iRow].m_strMEDICINEID_CHR))
                    {
                        long lngCurrentSeriesID = 0;
                        //����������Ƿ��Ѵ��ڸ�ҩ
                        lngRes = objSelect.m_lngCheckHasStorage( p_objStorageArr[iRow].m_strMEDICINEID_CHR, p_objStorageArr[iRow].m_strDRUGSTOREID_CHR, out blnHasDetail, out lngCurrentSeriesID);

                        if (blnHasDetail)
                        {
                            //���������ӿ��
                            lngRes = m_lngModifyStorageFromInitial( p_objStorageArr[iRow], lngCurrentSeriesID);
                            if (lngRes <= 0)
                            {
                                throw new Exception("���������ӿ��ʧ��1");
                            }
                        }
                        else
                        {
                            //�����������ҩƷ
                            lngRes = m_lngAddNewStorage( ref p_objStorageArr[iRow]);
                            if (lngRes <= 0)
                            {
                                throw new Exception("�����������ҩƷʧ��");
                            }
                            hstMedicine.Add(p_objStorageArr[iRow].m_strMEDICINEID_CHR, p_objStorageArr[iRow].m_lngSERIESID_INT);
                        }
                    }
                    else
                    {
                        //����������ӿ��

                        lngRes = m_lngModifyStorageFromInitial( p_objStorageArr[iRow], Convert.ToInt64(hstMedicine[p_objStorageArr[iRow].m_strMEDICINEID_CHR]));
                        if (lngRes <= 0)
                        {
                            throw new Exception("���������ӿ��ʧ��2");
                        }
                    }
                }
                hstMedicine = null;
                lngRes = m_lngSetCommitUser( p_lngSEQArr, p_strEmpID);
                if (lngRes <= 0)
                {
                    throw new Exception("���������ʧ��");
                }

                #region �����ʱ���ϸ��
                this.m_lngAddNewAccountDetail( p_objDetailArr);
                #endregion
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #region �����˱���ϸ
        /// <summary>
        /// �����˱���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objAccountDetailArr">�˱���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewAccountDetail( clsDS_StorageDetail_VO[] p_objAccountDetailArr)
        {
            if (p_objAccountDetailArr == null || p_objAccountDetailArr.Length == 0)
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_ds_account_detail a
  (a.seriesid_int,
   a.medicineid_chr,
   a.medicinename_vchr,
   a.medicinetypeid_chr,
   a.medspec_vchr,
   a.drugstoreid_int,
   a.lotno_vchr,
   a.validperiod_dat,
   a.instoreid_vchr,
   a.ipwholesaleprice_int,
   a.opwholesaleprice_int,
   a.ipretailprice_int,
   a.opretailprice_int,
   a.instoragedate_dat,
   a.ipunit_chr,
   a.ipamount_int,
   a.opamount_int,
   a.opunit_chr,
   a.ipoldgross_int,
   a.opoldgross_int,
   a.type_int,
   a.deptid_chr,
   a.chittyid_vchr,
   a.formtype_int,
   a.state_int,
   a.isend_int,
   a.endipamount_int,
   a.endopamount_int,
   a.endipwholesaleprice_int,
   a.endopwholesaleprice_int,
   a.endipretailprice_int,
   a.endopretailprice_int,
   a.inaccountid_chr,
   a.inaccountdate_dat,
   a.accountid_chr,
   a.productorid_chr,operatedate_dat,packqty_dec)
values
  (?,?,?,?,?,?,?,
  ?,?,?,?,?,?,sysdate,
  ?,?,?,?,?,?,?,
  ?,?,?,?,?,?,?,
  ?,?,?,?,?,?,?,?,sysdate,?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();
                //long[] lngSEQArr = null;
                //lngRes = objPublic.m_lngGetSequenceArr( "SEQ_DS_ACCOUNT_DETAIL", p_objAccountDetailArr.Length, out lngSEQArr);


                DbType[] dbTypes = new DbType[] { DbType.Int64,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.DateTime,
                        DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.String,DbType.Double,DbType.Double,DbType.String,
                        DbType.Double,DbType.Double,DbType.Int16,DbType.String,DbType.String,DbType.Int16,DbType.Int16,DbType.Int16,DbType.Double,DbType.Double,
                        DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.String,DbType.DateTime,DbType.String,DbType.String,DbType.Double};

                object[][] objValues = new object[36][];

                int intItemCount = p_objAccountDetailArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }

                //if (lngSEQArr == null || lngSEQArr.Length == 0)
                //{
                //    return -1;
                //}

                for (int iRow = 0; iRow < intItemCount; iRow++)
                {
                    objValues[0][iRow] = objPublic.GetSeqNextVal("SEQ_DS_ACCOUNT_DETAIL"); //lngSEQArr[iRow];
                    objValues[1][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[2][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[3][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINETYPEID_CHR;
                    objValues[4][iRow] = p_objAccountDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = p_objAccountDetailArr[iRow].m_strDRUGSTOREID_CHR;
                    objValues[6][iRow] = p_objAccountDetailArr[iRow].m_strLOTNO_VCHR;
                    objValues[7][iRow] = p_objAccountDetailArr[iRow].m_dtmVALIDPERIOD_DAT;
                    objValues[8][iRow] = p_objAccountDetailArr[iRow].m_strDSINSTOREID_VCHR;
                    objValues[9][iRow] = p_objAccountDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                    objValues[10][iRow] = p_objAccountDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                    objValues[11][iRow] = p_objAccountDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                    objValues[12][iRow] = p_objAccountDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                    objValues[13][iRow] = p_objAccountDetailArr[iRow].m_strIPUNIT_CHR;
                    objValues[14][iRow] = p_objAccountDetailArr[iRow].m_dblIPREALGROSS_INT;
                    objValues[15][iRow] = p_objAccountDetailArr[iRow].m_dblOPREALGROSS_INT;
                    objValues[16][iRow] = p_objAccountDetailArr[iRow].m_strOPUNIT_CHR;
                    objValues[17][iRow] = p_objAccountDetailArr[iRow].m_dblOldIPREALGROSS_INT;
                    objValues[18][iRow] = p_objAccountDetailArr[iRow].m_dblOldOPREALGROSS_INT;
                    objValues[19][iRow] = 3;
                    objValues[20][iRow] = p_objAccountDetailArr[iRow].m_strDRUGSTOREID_CHR;
                    objValues[21][iRow] = p_objAccountDetailArr[iRow].m_strDSINSTOREID_VCHR;
                    objValues[22][iRow] = p_objAccountDetailArr[iRow].m_intType;
                    objValues[23][iRow] = 2;
                    objValues[24][iRow] = 0;
                    objValues[25][iRow] = 0;
                    objValues[26][iRow] = 0;
                    objValues[27][iRow] = 0;
                    objValues[28][iRow] = 0;
                    objValues[29][iRow] = 0;
                    objValues[30][iRow] = 0;
                    objValues[31][iRow] = string.Empty;
                    objValues[32][iRow] = DateTime.MinValue;
                    objValues[33][iRow] = string.Empty;
                    objValues[34][iRow] = p_objAccountDetailArr[iRow].m_strPRODUCTORID_CHR;
                    objValues[35][iRow] = p_objAccountDetailArr[iRow].m_dblPACKQTY_DEC;

                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �޸Ŀ��������Ϣ
        /// <summary>
        /// ���������ӿ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">�����Ϣ</param>
        /// <param name="p_lngSEQ">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageFromInitial( clsDS_Storage_VO p_objRecord, long p_lngSEQ)
        {
            if (p_objRecord == null || p_lngSEQ <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage
   set opcurrentgross_num = opcurrentgross_num + ?,
       ipcurrentgross_num = ipcurrentgross_num + ?
 where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecord.m_dblOPCURRENTGROSS_NUM;
                objDPArr[1].Value = p_objRecord.m_dblIPCURRENTGROSS_NUM;
                objDPArr[2].Value = p_lngSEQ;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ��ӿ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSDVO">���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewStorage( ref clsDS_Storage_VO p_objSDVO)
        {
            if (p_objSDVO == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_ds_storage
  (seriesid_int,
   medicineid_chr,
   medicinename_vchr,
   medspec_vchr,
   opcurrentgross_num,
   opunit_chr,
   ipcurrentgross_num,
   ipunit_chr,
   drugstoreid_chr
   )
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();
                long lngEff = -1;
                IDataParameter[] objLisAddItemRefArr = null;
                long lngSEQ = 0;
                lngRes = objPublic.m_lngGetSequence( "SEQ_DS_STORAGE", out lngSEQ);
                if (lngSEQ <= 0)
                {
                    return -1;
                }

                objHRPServ.CreateDatabaseParameter(9, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                p_objSDVO.m_lngSERIESID_INT = lngSEQ;
                objLisAddItemRefArr[0].Value = lngSEQ;
                objLisAddItemRefArr[1].Value = p_objSDVO.m_strMEDICINEID_CHR;
                objLisAddItemRefArr[2].Value = p_objSDVO.m_strMEDICINENAME_VCHR;
                objLisAddItemRefArr[3].Value = p_objSDVO.m_strMEDSPEC_VCHR;
                objLisAddItemRefArr[4].Value = p_objSDVO.m_dblOPCURRENTGROSS_NUM;
                objLisAddItemRefArr[5].Value = p_objSDVO.m_strOPUNIT_CHR;
                objLisAddItemRefArr[6].Value = p_objSDVO.m_dblIPCURRENTGROSS_NUM;
                objLisAddItemRefArr[7].Value = p_objSDVO.m_strIPUNIT_CHR;
                objLisAddItemRefArr[8].Value = p_objSDVO.m_strDRUGSTOREID_CHR;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objLisAddItemRefArr);
                objHRPServ.Dispose();
                objHRPServ = null;
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
        /// <param name="p_lngSEQ">����</param>
        /// <param name="p_strInitialID">����</param>
        /// <param name="p_strStorageID">ҩ��ID</param>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_strLotNO">����</param>
        /// <param name="p_dblInAmount">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnCommit( long p_lngSEQ, string p_strInitialID, string p_strStorageID, string p_strMedicineID, string p_strLotNO, double p_dblInAmount)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"delete from t_ds_storage_detail t
 where t.medicineid_chr = ?
   and t.lotno_vchr = ?
   and t.dsinstoreid_vchr = ?
   and t.drugstoreid_chr = ?
   and t.status = 1
   and t.oprealgross_int = ?
   and t.opavailablegross_num = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strMedicineID;
                objDPArr[1].Value = p_strLotNO;
                objDPArr[2].Value = p_strInitialID;
                objDPArr[3].Value = p_strStorageID;
                objDPArr[4].Value = p_dblInAmount;
                objDPArr[5].Value = p_dblInAmount;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngEff <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    return -1;
                }

                clsDS_Storage_VO objSt = new clsDS_Storage_VO();
                objSt.m_strMEDICINEID_CHR = p_strMedicineID;
                objSt.m_strDRUGSTOREID_CHR = p_strStorageID;
                objSt.m_dblOPCURRENTGROSS_NUM = p_dblInAmount;
                objSt.m_dblIPCURRENTGROSS_NUM = p_dblInAmount;

                m_lngSubStorageGross( objSt);
                if (lngRes <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    throw new Exception();
                }

                strSQL = @"delete from t_ds_account_detail t
 where t.medicineid_chr = ?
   and t.lotno_vchr = ?
   and t.instoreid_vchr = ?
   and t.drugstoreid_int = ?
   and t.chittyid_vchr = ?";

                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strMedicineID;
                objDPArr[1].Value = p_strLotNO;
                objDPArr[2].Value = p_strInitialID;
                objDPArr[3].Value = p_strStorageID;
                objDPArr[4].Value = p_strInitialID;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngEff <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    throw new Exception();
                }

                long[] lngSeq = new long[] { p_lngSEQ };
                lngRes = m_lngSetCommitUser( lngSeq, string.Empty);
                if (lngRes <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    throw new Exception();
                }
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ���������ٵ�ǰ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">�����Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSubStorageGross( clsDS_Storage_VO p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage
   set ipcurrentgross_num = ipcurrentgross_num - ?,
       opcurrentgross_num = opcurrentgross_num - ?
 where medicineid_chr = ?
   and drugstoreid_chr = ?
   and ipcurrentgross_num - ? >= 0
   and opcurrentgross_num - ? >= 0";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_objRecord.m_dblIPCURRENTGROSS_NUM;
                objDPArr[1].Value = p_objRecord.m_dblOPCURRENTGROSS_NUM;
                objDPArr[2].Value = p_objRecord.m_strMEDICINEID_CHR;
                objDPArr[3].Value = p_objRecord.m_strDRUGSTOREID_CHR;
                objDPArr[4].Value = p_objRecord.m_dblIPCURRENTGROSS_NUM;
                objDPArr[5].Value = p_objRecord.m_dblOPCURRENTGROSS_NUM;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
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
        /// <param name="p_lngSEQArr">���ʼ�¼����</param>
        /// <param name="p_strInitialID">����ID</param>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInAccount( long[] p_lngSEQArr, string[] p_strInitialID, string p_strEmpID, string p_strStorageID)
        {
            if (p_lngSEQArr == null || p_lngSEQArr.Length == 0 || p_strInitialID == null || p_strInitialID.Length == 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                lngRes = m_lngSetAccountUser( p_strEmpID, p_lngSEQArr);
                if (lngRes <= 0)
                {
                    return -1;
                }

                lngRes = this.m_lngUpdateAccountDetail( p_strInitialID, p_strStorageID, p_strEmpID);
                if (lngRes <= 0)
                {
                    throw new Exception();
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

        #region ����������

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_lngSeq">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetAccountUser( string p_strEmpID, long[] p_lngSeq)
        {
            if (p_lngSeq == null || p_lngSeq.Length == 0 || string.IsNullOrEmpty(p_strEmpID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_initial set inaccounterid_chr = ? where seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();


                DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int64 };

                object[][] objValues = new object[2][];

                int intItemCount = p_lngSeq.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }

                for (int iRow = 0; iRow < intItemCount; iRow++)
                {
                    objValues[0][iRow] = p_strEmpID;
                    objValues[1][iRow] = p_lngSeq[iRow];
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ȷ������
        /// <summary>
        /// ȷ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strChittyIDArr">���ݺ�</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strEmpID">����Ա��ID</param>
        /// <param name="p_dtmAccountDate">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateAccountDetail( string[] p_strChittyIDArr, string m_strDrugStoreid, string p_strEmpID)
        {
            if (p_strChittyIDArr == null || p_strChittyIDArr.Length == 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_account_detail t
   set t.state_int = 1, t.inaccountid_chr = ?, t.inaccountdate_dat = sysdate
 where t.chittyid_vchr = ?
   and t.drugstoreid_int = ?
   and t.state_int = 2";

                clsHRPTableService objHRPServ = new clsHRPTableService();


                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String };

                object[][] objValues = new object[3][];

                int intItemCount = p_strChittyIDArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }

                for (int iRow = 0; iRow < intItemCount; iRow++)
                {
                    objValues[0][iRow] = p_strEmpID;
                    objValues[1][iRow] = p_strChittyIDArr[iRow];
                    objValues[2][iRow] = m_strDrugStoreid;
                }

                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                objHRPServ.Dispose();
                objHRPServ = null;
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
