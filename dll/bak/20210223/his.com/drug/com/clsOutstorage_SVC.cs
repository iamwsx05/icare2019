using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ������ҵ����
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOutstorage_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ҩ����ҩ,�޸Ŀ��
        /// <summary>
        ///  ҩ����ҩ,�޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetail">�����ϸ</param>
        /// <param name="m_objOutStorageDetail">������ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSubtractStorage( clsDS_StorageDetail_VO[] p_objDetail, ref clsDS_Outstorage_Detail[] m_objOutStorageDetail)
        {
            if (p_objDetail == null || p_objDetail.Length == 0)
            {
                return -1;
            }
            long lngRes = -1;
            bool p_blnHasDetail = false;
            long p_lngSeriesID;
            m_objOutStorageDetail = new clsDS_Outstorage_Detail[p_objDetail.Length];
            clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
            for (int intRow = 0; intRow < p_objDetail.Length; intRow++)
            {
                m_objOutStorageDetail[intRow] = new clsDS_Outstorage_Detail();
                //�жϵ�ǰҩƷ�Ƿ��Ѵ��ڿ��������
                objSelect.m_lngCheckMedExistInStorageDetail(  p_objDetail[intRow], ref  m_objOutStorageDetail[intRow], out p_blnHasDetail, out p_lngSeriesID);
                if (p_blnHasDetail)
                {
                    //���¿����ϸ���¼
                    lngRes = m_lngModifyStorageDetailGross(ref p_objDetail[intRow], 2, p_lngSeriesID);
                    //�޸Ŀ����������
                    if (lngRes != -1)
                    {
                        lngRes = m_lngModifyStorageGross(p_objDetail[intRow], 2);
                    }
                }
            }
            if (lngRes < 1)
                throw new Exception();
            else
            {
                this.m_lngAddDSRecipeAccountInfo( p_objDetail);
            }
            if (lngRes < 1)
                throw new Exception();
            return lngRes;

        }
        #endregion

        #region ���ҩ��������ˮ�ʱ�
        /// <summary>
        /// ���ҩ��������ˮ�ʱ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objStorageDetailVoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDSRecipeAccountInfo( clsDS_StorageDetail_VO[] m_objStorageDetailVoArr)
        {
            long lngRes = -1;
            string strSQL;
            if (m_objStorageDetailVoArr == null || m_objStorageDetailVoArr.Length < 1)
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            strSQL = @" insert into t_ds_recipeaccount_detail a
  (seriesid_int,
   medicineid_chr,
   medicinename_vchr,
   medicinetypeid_chr,
   medspec_vchr,
   drugstoreid_int,
   lotno_vchr,
   validperiod_dat,
   ipretailprice_int,
   opretailprice_int,
   ipunit_chr,
   ipamount_int,
   opamount_int,
   opunit_chr,
   ipoldgross_int,
   opoldgross_int,
   type_int,
   state_int,
   isend_int,
   endipamount_int,
   endopamount_int,
   endipretailprice_int,
   endopretailprice_int,
   inaccountid_chr,
   inaccountdate_dat,
   accountid_chr,
   productorid_chr,
   operatedate_dat,
   outpatrecipeid_chr,
   medseriesid_int,
   operatorid_chr)
  select seq_ds_recipeaccount_detail.nextval,
         b.medicineid_chr,
         b.medicinename_vchr,
         c.medicinetypeid_chr,
         b.medspec_vchr,
         b.drugstoreid_chr,
         b.lotno_vchr,
         b.validperiod_dat,
         b.ipretailprice_int,
         b.opretailprice_int,
         b.ipunit_chr,
         ?,
         ?,
         b.opunit_chr,
         ?,
         ?,
         ?,
         1,
         0,
         null,
         null,
         null,
         null,
         ?,
         sysdate,
         null,
         b.productorid_chr,
         sysdate,
         ?,
         b.seriesid_int,
         ?
    from t_ds_storage_detail b, t_bse_medicine c
   where b.seriesid_int = ?
     and b.medicineid_chr = c.medicineid_chr(+)";
            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.String, DbType.String, DbType.Int64 };
            object[][] objValuesArr = new object[9][];
            int m_intCount = m_objStorageDetailVoArr.Length;
            clsDS_StorageDetail_VO m_objTempVo;
            for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
            {
                objValuesArr[j] = new object[m_intCount];
            }
            for (int i = 0; i < m_intCount; i++)
            {
                m_objTempVo = m_objStorageDetailVoArr[i];
                objValuesArr[0][i] = m_objTempVo.m_dblIPREALGROSS_INT;
                objValuesArr[1][i] = m_objTempVo.m_dblOPREALGROSS_INT;
                objValuesArr[2][i] = m_objTempVo.m_dblOldIPREALGROSS_INT;
                objValuesArr[3][i] = m_objTempVo.m_dblOldOPREALGROSS_INT;
                objValuesArr[4][i] = m_objTempVo.m_intSubStorageType;
                objValuesArr[5][i] = m_objTempVo.m_strOperatorid;
                objValuesArr[6][i] = m_objTempVo.m_strOutPatientRecipeid;
                objValuesArr[7][i] = m_objTempVo.m_strOperatorid;
                objValuesArr[8][i] = m_objTempVo.m_lngSERIESID_INT;
            }
            try
            {
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValuesArr, dbTypes);
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
        #endregion

        #region �޸Ŀ����������
        /// <summary>
        /// �޸Ŀ����������
        /// </summary>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1���ӿ��,2�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageGross(clsDS_StorageDetail_VO p_objDetail, Int16 intType)
        {
            //�޸Ŀ������
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_ds_storage a
set a.opcurrentgross_num = a.opcurrentgross_num + ?,
a.ipcurrentgross_num = a.ipcurrentgross_num + ?
where a.medicineid_chr = ? and a.drugstoreid_chr=?";
            objHRPServ.CreateDatabaseParameter(4, out objValues);
            //�жϵ�ǰΪ��ӿ�������Ǽ�С
            if (intType == 1)
            {
                objValues[0].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_objDetail.m_strMEDICINEID_CHR;
                objValues[3].Value = p_objDetail.m_strDRUGSTOREID_CHR;
            }
            else
            {
                objValues[0].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_objDetail.m_strMEDICINEID_CHR;
                objValues[3].Value = p_objDetail.m_strDRUGSTOREID_CHR;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region �޸Ŀ����ϸ����
        /// <summary>
        /// �޸Ŀ����ϸ����
        /// </summary>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1���ӿ��,2�������</param>
        /// <param name="p_lngSeriesID">����˳��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageDetailGross(ref clsDS_StorageDetail_VO p_objDetail, Int16 intType, long p_lngSeriesID)
        {
            //�޸Ŀ����ϸ��
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;


            //�жϵ�ǰΪ��ӿ�������Ǽ�С
            if (intType == 1)
            {
                strSQL = @"update t_ds_storage_detail a
set a.oprealgross_int = a.oprealgross_int + ?,
a.iprealgross_int = a.iprealgross_int + ?,
a.opavailablegross_num = a.opavailablegross_num + ?,
a.ipavailablegross_num = a.ipavailablegross_num + ?
where a.seriesid_int=?";
                objHRPServ.CreateDatabaseParameter(5, out objValues);
                objValues[0].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = p_lngSeriesID;
            }
            else
            {
                strSQL = @"update t_ds_storage_detail a
set a.oprealgross_int = a.oprealgross_int + ?,
a.iprealgross_int = a.iprealgross_int + ?
where a.seriesid_int=?";
                objHRPServ.CreateDatabaseParameter(3, out objValues);
                objValues[0].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_lngSeriesID;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    throw new Exception();
                }
                DataTable dtReuslt = new DataTable();
                strSQL = @"select a.iprealgross_int,
       round(a.iprealgross_int / a.packqty_dec, 2) oprealgross_int
  from t_ds_storage_detail a
                           where a.seriesid_int=?";
                objValues = null;
                objHRPServ.CreateDatabaseParameter(1, out objValues);
                objValues[0].Value = p_lngSeriesID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtReuslt, objValues);
                if (lngRes > 0 && dtReuslt != null && dtReuslt.Rows.Count > 0)
                {
                    p_objDetail.m_dblOldIPREALGROSS_INT = Convert.ToDouble(dtReuslt.Rows[0]["iprealgross_int"]);
                    p_objDetail.m_dblOldOPREALGROSS_INT = Convert.ToDouble(dtReuslt.Rows[0]["oprealgross_int"]);
                }
                else
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
        #endregion

        #region ����ҩ�������������ϸ������
        /// <summary>
        /// ����ҩ�������������ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <param name="m_strOutStorageFlag">�����־��02���⣻09��ҩ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOutstorage( clsDS_OutStorage_VO m_objMainVo, clsDS_Outstorage_Detail[] m_objDetailArr, string m_strOutStorageFlag)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL =
             @"insert into t_ds_outstorage
(seriesid_int,outdrugstoreid_vchr,formtype_int,status_int,instoredept_chr,
 patientid_chr,makeorder_dat,examdate_dat,inaccount_dat,makerid_chr,
 examid_chr,inaccounterid_chr,comment_vchr,drugstoreid_chr)
values
(?,?,?,?,?,
 ?,?,?,?,?,
 ?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                string m_strTempId = string.Empty;
                string m_strMedStoreShortCode = string.Empty;
                string m_strParaValue = string.Empty;
                objPubSvc.m_lngGetSysParaByID( "8007", out m_strParaValue);
                objPubSvc.m_lngGetMedStoreShortCodeByDeptid( m_objMainVo.m_strDRUGSTOREID_CHR, out m_strMedStoreShortCode);
                objPubSvc.m_lngGetSequence( "SEQ_DS_OUTSTORAGE", out m_objMainVo.m_lngSERIESID_INT);
                objPubSvc.m_lngGetNewIdByName( "t_ds_outstorage", "outdrugstoreid_vchr", m_strMedStoreShortCode, m_objMainVo.m_datMAKEORDER_DAT, ref m_strTempId);
                m_objMainVo.m_strOUTDRUGSTOREID_VCHR = m_strMedStoreShortCode + m_objMainVo.m_datMAKEORDER_DAT.ToString("yyMMdd") + m_strParaValue.Split(';')[1] + m_strTempId;
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(14, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_lngSERIESID_INT;
                objDataParm[1].Value = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                objDataParm[2].Value = m_objMainVo.m_intFORMTYPE_INT;
                objDataParm[3].Value = m_objMainVo.m_intSTATUS;
                objDataParm[4].Value = m_objMainVo.m_strINSTOREDEPT_CHR;

                objDataParm[5].Value = m_objMainVo.m_strPatientid;
                objDataParm[6].Value = m_objMainVo.m_datMAKEORDER_DAT; ;
                objDataParm[7].Value = m_objMainVo.m_datEXAM_DATE;
                objDataParm[8].Value = m_objMainVo.m_datINACCOUNT_DAT;
                objDataParm[9].Value = m_objMainVo.m_strMAKERID_CHR;

                objDataParm[10].Value = m_objMainVo.m_strEXAMID_CHR;
                objDataParm[11].Value = m_objMainVo.m_strINACCOUNTERID_CHR;
                objDataParm[12].Value = m_objMainVo.m_strCOMMENT_VCHR;
                objDataParm[13].Value = m_objMainVo.m_strDRUGSTOREID_CHR;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                if (m_objDetailArr == null || m_objDetailArr.Length == 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    return -1;
                }

                strSQL = @"insert into t_ds_outstorage_detail
(seriesid_int,seriesid2_int,medicineid_chr,medicinename_vchr,
 medspec_vchr,lotno_vchr,validperiod_dat,opamount_int,
 opunit_chr,ipamount_int,ipunit_chr,opwholesaleprice_int,
 ipwholesaleprice_int,opretailprice_int,ipretailprice_int,
 rejectreason,status,packqty_dec,productorid_chr)
values
(?,?,?,?,
 ?,?,?,?,
 ?,?,?,?,
 ?,?,?,
 ?,?,?,?
)";
                //long[] lngSEQArr = null;
                //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_DS_OUTSTORAGE_DETAIL", m_objDetailArr.Length, out lngSEQArr);
                DbType[] dbTypes = new DbType[] 
                  { DbType.Int64, DbType.Int64, DbType.String, DbType.String, 
                    DbType.String, DbType.String, DbType.DateTime, DbType.Double,
                    DbType.String, DbType.Double, DbType.String, DbType.Double, 
                    DbType.Double, DbType.Double, DbType.Double,
                    DbType.String,DbType.Int16,DbType.Double,DbType.String 
                  };
                object[][] objValues = new object[19][];
                int intItemCount = m_objDetailArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }
                for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                {
                    m_objDetailArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_DS_OUTSTORAGE_DETAIL");   // lngSEQArr[iRow];
                    objValues[0][iRow] = m_objDetailArr[iRow].m_lngSERIESID_INT;
                    m_objDetailArr[iRow].m_lngSERIESID2_INT = m_objMainVo.m_lngSERIESID_INT;
                    objValues[1][iRow] = m_objDetailArr[iRow].m_lngSERIESID2_INT;
                    objValues[2][iRow] = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[3][iRow] = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[4][iRow] = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = m_objDetailArr[iRow].m_strLOTNO_VCHR;
                    objValues[6][iRow] = m_objDetailArr[iRow].m_datVALIDPERIOD_DAT;
                    objValues[7][iRow] = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;
                    objValues[8][iRow] = m_objDetailArr[iRow].m_strOPUNIT_CHR;
                    objValues[9][iRow] = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;
                    objValues[10][iRow] = m_objDetailArr[iRow].m_strIPUNIT_CHR;
                    objValues[11][iRow] = m_objDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                    objValues[12][iRow] = m_objDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                    objValues[13][iRow] = m_objDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                    objValues[14][iRow] = m_objDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                    objValues[15][iRow] = m_objDetailArr[iRow].m_strRejectReason;
                    objValues[16][iRow] = m_objDetailArr[iRow].m_intSTATUS;
                    objValues[17][iRow] = m_objDetailArr[iRow].m_dblPACKQTY_DEC;
                    objValues[18][iRow] = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;
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

        #region ����ҩ�������������ϸ������
        /// <summary>
        /// ����ҩ�������������ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <param name="p_intCommitFolow">�Ƿ񱣴漴���</param>
        /// <param name="p_strExamerID">�������������</param>
        /// <param name="p_strMedicineName">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOutstorageInfo( ref clsDS_OutStorage_VO m_objMainVo, ref clsDS_Outstorage_Detail[] m_objDetailArr, int p_intCommitFolow, string p_strExamerID, out string p_strMedicineName)
        {
            p_strMedicineName = string.Empty;
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL =
             @"insert into t_ds_outstorage
            (seriesid_int,  outdrugstoreid_vchr,formtype_int,
             status_int, drugstoreid_chr, instoredept_chr,
             makeorder_dat, examdate_dat,inaccount_dat, 
             comment_vchr, makerid_chr, examid_chr,
             inaccounterid_chr,patientid_chr,typecode_vchr
            )
            values (?, ?, ?, ?,?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                string m_strTempId = string.Empty;
                string m_strMedStoreShortCode = string.Empty;
                string m_strParaValue = string.Empty;
                objPubSvc.m_lngGetSysParaByID( "8007", out m_strParaValue);
                objPubSvc.m_lngGetMedStoreShortCodeByDeptid( m_objMainVo.m_strDRUGSTOREID_CHR, out m_strMedStoreShortCode);
                objPubSvc.m_lngGetSequence( "SEQ_DS_OUTSTORAGE", out m_objMainVo.m_lngSERIESID_INT);
                objPubSvc.m_lngGetNewIdByName( "t_ds_outstorage", "outdrugstoreid_vchr", m_strMedStoreShortCode, m_objMainVo.m_datMAKEORDER_DAT, ref m_strTempId);
                m_objMainVo.m_strOUTDRUGSTOREID_VCHR = m_strMedStoreShortCode + m_objMainVo.m_datMAKEORDER_DAT.ToString("yyMMdd") + m_strParaValue.Split(';')[1] + m_strTempId;
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(15, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_lngSERIESID_INT;
                objDataParm[1].Value = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                objDataParm[2].Value = m_objMainVo.m_intFORMTYPE_INT;
                objDataParm[3].Value = m_objMainVo.m_intSTATUS;

                objDataParm[4].Value = m_objMainVo.m_strDRUGSTOREID_CHR;
                objDataParm[5].Value = m_objMainVo.m_strINSTOREDEPT_CHR;
                objDataParm[6].Value = m_objMainVo.m_datMAKEORDER_DAT; ;
                objDataParm[7].Value = m_objMainVo.m_datEXAM_DATE;

                objDataParm[8].Value = m_objMainVo.m_datINACCOUNT_DAT;
                objDataParm[9].Value = m_objMainVo.m_strCOMMENT_VCHR;
                objDataParm[10].Value = m_objMainVo.m_strMAKERID_CHR;
                objDataParm[11].Value = m_objMainVo.m_strEXAMID_CHR;

                objDataParm[12].Value = m_objMainVo.m_strINACCOUNTERID_CHR;
                objDataParm[13].Value = m_objMainVo.m_strPatientid;
                objDataParm[14].Value = m_objMainVo.m_strTYPECODE_VCHR;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                if (m_objDetailArr == null || m_objDetailArr.Length == 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    //return -1;
                    throw new Exception("������ϸ����Ϊ��");
                }

                strSQL = @"insert into t_ds_outstorage_detail
            (seriesid_int, seriesid2_int, medicineid_chr, medicinename_vchr,
             medspec_vchr, opamount_int, opunit_chr, ipamount_int,
             ipunit_chr, packqty_dec, opwholesaleprice_int,
             ipwholesaleprice_int, opretailprice_int, ipretailprice_int,
             lotno_vchr, validperiod_dat, status,rejectreason,storageseriesid_chr,productorid_chr
            )
     values (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?,
             ?, ?, ?,
             ?, ?, ?,?,?,?
            )";
                //long[] lngSEQArr = null;
                clsDS_UpdateStorageBySeriesID_VO[] objUpdateArr = null;
                //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_DS_OUTSTORAGE_DETAIL", m_objDetailArr.Length, out lngSEQArr);
                DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64, DbType.String, DbType.String, 
                    DbType.String, DbType.Double, DbType.String, DbType.Double, 
                    DbType.String, DbType.Double, DbType.Double, 
                    DbType.Double, DbType.Double, DbType.Double, 
                    DbType.String,DbType.DateTime, DbType.Int16,
                    DbType.String,DbType.String,DbType.String};
                object[][] objValues = new object[20][];
                int intItemCount = m_objDetailArr.Length;
                objUpdateArr = new clsDS_UpdateStorageBySeriesID_VO[intItemCount];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }

                for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                {
                    objUpdateArr[iRow] = new clsDS_UpdateStorageBySeriesID_VO();
                    m_objDetailArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_DS_OUTSTORAGE_DETAIL"); //lngSEQArr[iRow];
                    objValues[0][iRow] = m_objDetailArr[iRow].m_lngSERIESID_INT;
                    m_objDetailArr[iRow].m_lngSERIESID2_INT = m_objMainVo.m_lngSERIESID_INT;
                    objValues[1][iRow] = m_objDetailArr[iRow].m_lngSERIESID2_INT;
                    objValues[2][iRow] = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[3][iRow] = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[4][iRow] = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;
                    objValues[6][iRow] = m_objDetailArr[iRow].m_strOPUNIT_CHR;
                    objValues[7][iRow] = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;
                    objValues[8][iRow] = m_objDetailArr[iRow].m_strIPUNIT_CHR;
                    objValues[9][iRow] = m_objDetailArr[iRow].m_dblPACKQTY_DEC;
                    objValues[10][iRow] = m_objDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                    objValues[11][iRow] = m_objDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                    objValues[12][iRow] = m_objDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                    objValues[13][iRow] = m_objDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                    objValues[14][iRow] = m_objDetailArr[iRow].m_strLOTNO_VCHR;
                    objValues[15][iRow] = m_objDetailArr[iRow].m_datVALIDPERIOD_DAT;
                    objValues[16][iRow] = m_objDetailArr[iRow].m_intSTATUS;
                    objValues[17][iRow] = m_objDetailArr[iRow].m_strRejectReason;
                    objValues[18][iRow] = m_objDetailArr[iRow].m_intSTORAGESERIESID_CHR;
                    objValues[19][iRow] = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;

                    objUpdateArr[iRow].m_dblOPAvalid = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;
                    objUpdateArr[iRow].m_dblIPAvalid = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;
                    objUpdateArr[iRow].m_intSeriesID = Convert.ToInt64(m_objDetailArr[iRow].m_intSTORAGESERIESID_CHR);
                    objUpdateArr[iRow].m_strMEDICINENAME_VCHR = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                //���¿��ÿ��                 
                lngRes = m_lngUpdateStorageAvalid( objUpdateArr, out p_strMedicineName);
                if (lngRes <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    throw new Exception(p_strMedicineName + "���ÿ�治�㣬���޸�");
                }

                //���漴���
                if (p_intCommitFolow == 1 && m_objDetailArr.Length > 0)
                {
                    clsDS_UpdateStorageBySeriesID_VO[] objStorageDetailArr = new clsDS_UpdateStorageBySeriesID_VO[m_objDetailArr.Length];
                    for (int i1 = 0; i1 < m_objDetailArr.Length; i1++)
                    {
                        objStorageDetailArr[i1] = new clsDS_UpdateStorageBySeriesID_VO();
                        objStorageDetailArr[i1].m_strMedicineCode = m_objDetailArr[i1].m_strMEDICINEID_CHR;
                        objStorageDetailArr[i1].m_strMEDICINENAME_VCHR = m_objDetailArr[i1].m_strMEDICINENAME_VCHR;
                        objStorageDetailArr[i1].m_strMEDSPEC_VCHR = m_objDetailArr[i1].m_strMEDSPEC_VCHR;
                        objStorageDetailArr[i1].m_strLOTNO_VCHR = m_objDetailArr[i1].m_strLOTNO_VCHR;
                        objStorageDetailArr[i1].m_strOPUNIT_CHR = m_objDetailArr[i1].m_strOPUNIT_CHR;
                        objStorageDetailArr[i1].m_dblOPReal = m_objDetailArr[i1].m_dblOPAMOUNT_INT;
                        objStorageDetailArr[i1].m_dblOPAvalid = m_objDetailArr[i1].m_dblOPAMOUNT_INT;
                        objStorageDetailArr[i1].m_dblOPRETAILPRICE_INT = m_objDetailArr[i1].m_dblOPRETAILPRICE_INT;
                        objStorageDetailArr[i1].m_dblOPWHOLESALEPRICE_INT = m_objDetailArr[i1].m_dblOPWHOLESALEPRICE_INT;
                        objStorageDetailArr[i1].m_strIPUNIT_CHR = m_objDetailArr[i1].m_strIPUNIT_CHR;
                        objStorageDetailArr[i1].m_dblIPReal = m_objDetailArr[i1].m_dblIPAMOUNT_INT;
                        objStorageDetailArr[i1].m_dblIPAvalid = m_objDetailArr[i1].m_dblIPAMOUNT_INT;
                        objStorageDetailArr[i1].m_dblIPRETAILPRICE_INT = m_objDetailArr[i1].m_dblIPRETAILPRICE_INT;
                        objStorageDetailArr[i1].m_dblIPWHOLESALEPRICE_INT = m_objDetailArr[i1].m_dblIPWHOLESALEPRICE_INT;
                        objStorageDetailArr[i1].m_dblPACKQTY_DEC = m_objDetailArr[i1].m_dblPACKQTY_DEC;
                        objStorageDetailArr[i1].m_dtmVALIDPERIOD_DAT = m_objDetailArr[i1].m_datVALIDPERIOD_DAT;
                        objStorageDetailArr[i1].m_strINSTOREID_VCHR = m_objDetailArr[i1].m_strDSInStorageid;//����ȡҩ����ⵥ�ĵ���
                        objStorageDetailArr[i1].m_strDrugID = m_objMainVo.m_strDRUGSTOREID_CHR;
                        objStorageDetailArr[i1].m_strDSINSTOREID_VCHR = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;//���ﵱ��chittyid_vchr
                        objStorageDetailArr[i1].m_intType = Convert.ToInt16(m_objMainVo.m_intFORMTYPE_INT);
                        objStorageDetailArr[i1].m_strPRODUCTORID_CHR = m_objDetailArr[i1].m_strPRODUCTORID_CHR;
                        objStorageDetailArr[i1].m_intSeriesID = Convert.ToInt64(m_objDetailArr[i1].m_intSTORAGESERIESID_CHR);
                        objStorageDetailArr[i1].m_strMEDICINETYPEID_CHR = m_objDetailArr[i1].m_strMedicineTypeid;
                        objStorageDetailArr[i1].m_lngRELATEDSERIESID_INT = m_objDetailArr[i1].m_lngSERIESID_INT;
                    }
                    string strInfo = string.Empty;
                    lngRes = m_lngSubtractStorage( objStorageDetailArr, 2, out strInfo);
                    if (lngRes < 0)
                    {
                        objHRPServ.Dispose();
                        objHRPServ = null;
                        throw new Exception(strInfo);
                    }
                    else
                    {
                        lngRes = m_lngOutstorageExam(p_strExamerID, m_objMainVo.m_lngSERIESID_INT);
                        if (lngRes <= 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception("��������߳���");
                        }
                        lngRes = m_lngAddNewAccountDetail( objStorageDetailArr);
                    }

                    //������벿����ҩ������ͬʱ����һ�Ŵ�ҩ������ⵥ��
                    bool m_blnIsDrugStore = false;
                    bool m_blnIsHospital = false;
                    m_lngCheckDrugStore(m_objMainVo.m_strDRUGSTOREID_CHR, m_objMainVo.m_strINSTOREDEPT_CHR, out m_blnIsDrugStore, out m_blnIsHospital);
                    if (m_blnIsDrugStore)
                    {
                        clsInstorage_SVC m_objInstorageSvc = new clsInstorage_SVC();
                        clsDS_Instorage_VO m_objMainInstorageVo = new clsDS_Instorage_VO();
                        clsDS_Instorage_Detail[] m_oInstoragebjDetailArr = new clsDS_Instorage_Detail[m_objDetailArr.Length];
                        m_objMainInstorageVo.m_datMAKEORDER_DAT = m_objMainVo.m_datEXAM_DATE;//m_datMAKEORDER_DAT
                        m_objMainInstorageVo.m_datSTORAGEEXAM_DATE = m_objMainVo.m_datEXAM_DATE;
                        if (m_objMainVo.m_strDRUGSTOREID_CHR == m_objMainVo.m_strINSTOREDEPT_CHR)
                        {
                            m_objMainInstorageVo.m_intFORMTYPE_INT = 2;
                        }
                        else
                        {
                            m_objMainInstorageVo.m_intFORMTYPE_INT = 3;
                        }
                        m_objMainInstorageVo.m_intSTATUS = 1;
                        m_objMainInstorageVo.m_strBORROWDEPT_CHR = m_objMainVo.m_strDRUGSTOREID_CHR;
                        m_objMainInstorageVo.m_strDRUGSTOREID_INT = m_objMainVo.m_strINSTOREDEPT_CHR;
                        m_objMainInstorageVo.m_strMAKERID_CHR = m_objMainVo.m_strMAKERID_CHR;
                        m_objMainInstorageVo.m_strOUTSTORAGEID_VCHR = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                        m_objMainInstorageVo.m_strSTORAGEEXAMID_CHR = m_objMainVo.m_strEXAMID_CHR;
                        int intTypeCode = 0;
                        objPubSvc.m_lngGetTypeCodeByName( 0, "�������", out intTypeCode);
                        if (intTypeCode == 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception("������������ͣ��������");
                        }
                        m_objMainInstorageVo.m_strTYPECODE_VCHR = intTypeCode.ToString();
                        for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                        {
                            m_oInstoragebjDetailArr[iRow] = new clsDS_Instorage_Detail();
                            m_oInstoragebjDetailArr[iRow].m_dblPACKQTY_DEC = m_objDetailArr[iRow].m_dblPACKQTY_DEC;
                            m_oInstoragebjDetailArr[iRow].m_strMEDICINEID_CHR = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                            m_oInstoragebjDetailArr[iRow].m_strMEDICINENAME_VCHR = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                            m_oInstoragebjDetailArr[iRow].m_strMEDSPEC_VCHR = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                            m_oInstoragebjDetailArr[iRow].m_dblOPAMOUNT_INT = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;
                            m_oInstoragebjDetailArr[iRow].m_strOPUNIT_CHR = m_objDetailArr[iRow].m_strOPUNIT_CHR;
                            m_oInstoragebjDetailArr[iRow].m_dblIPAMOUNT_INT = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;
                            m_oInstoragebjDetailArr[iRow].m_strIPUNIT_CHR = m_objDetailArr[iRow].m_strIPUNIT_CHR;
                            m_oInstoragebjDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT = m_objDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                            m_oInstoragebjDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT = m_objDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                            m_oInstoragebjDetailArr[iRow].m_dblOPRETAILPRICE_INT = m_objDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                            m_oInstoragebjDetailArr[iRow].m_dblIPRETAILPRICE_INT = m_objDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                            m_oInstoragebjDetailArr[iRow].m_strLOTNO_VCHR = m_objDetailArr[iRow].m_strLOTNO_VCHR;
                            m_oInstoragebjDetailArr[iRow].m_datVALIDPERIOD_DAT = m_objDetailArr[iRow].m_datVALIDPERIOD_DAT;
                            m_oInstoragebjDetailArr[iRow].m_intSTATUS = 1;
                            m_oInstoragebjDetailArr[iRow].m_strINSTOREID_VCHR = m_objDetailArr[iRow].m_strInStorageid;//ҩ����ⵥ��
                            m_oInstoragebjDetailArr[iRow].m_dtmINSTORAGEDATE_DAT = m_objDetailArr[iRow].m_dtmINSTORAGEDATE_DAT.Date;//ҩ���������
                            m_oInstoragebjDetailArr[iRow].m_strPRODUCTORID_CHR = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                        }
                        lngRes = m_objInstorageSvc.m_lngAddNewInstorage( ref m_objMainInstorageVo, ref m_oInstoragebjDetailArr, 0, "");
                        if (lngRes <= 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception("������ⵥʱ����");
                        }
                    }
                    else 
                    {
                        //20090622:����Ƿ���ҩ�⣬������һ��ҩ�����˵�
                        strSQL = @"select a.setstatus_int from t_sys_setting a where a.setid_chr = '0420'";
                        DataTable dtbTemp = new DataTable();
                        objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbTemp);
                        if (dtbTemp.Rows.Count > 0)
                        {
                            if (dtbTemp.Rows[0][0].ToString() == "0")
                                return lngRes;
                        }

                        string m_strStorageID = string.Empty;//ҩ��ID
                        m_lngCheckMedStore(m_objMainVo.m_strINSTOREDEPT_CHR, out m_strStorageID);
                        if (m_strStorageID.Length > 0)
                        {
                            clsMS_InStorage_VO m_objMSInMainVO = new clsMS_InStorage_VO();
                            clsMS_InStorageDetail_VO[] m_objMSInDetailVO = new clsMS_InStorageDetail_VO[m_objDetailArr.Length];

                            m_objMSInMainVO.m_strSTORAGEID_CHR = m_strStorageID;
                            m_objMSInMainVO.m_strRETURNDEPT_CHR = m_objMainVo.m_strDRUGSTOREID_CHR;
                            m_objMSInMainVO.m_dtmNEWORDER_DAT = m_objMainVo.m_datEXAM_DATE;
                            m_objMSInMainVO.m_dtmINSTORAGEDATE_DAT = m_objMainVo.m_datEXAM_DATE;
                            m_objMSInMainVO.m_intSTATE_INT = 1;
                            //m_objMSInMainVO.m_strOUTDRUGSTOREID_VCHR = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                            m_objMSInMainVO.m_strMAKERID_CHR = m_objMainVo.m_strMAKERID_CHR;
                            m_objMSInMainVO.m_strCOMMNET_VCHR = "��Ӧ��ҩ�����ⵥ�ţ�" + m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                            m_objMSInMainVO.m_intFORMTYPE_INT = 2;

                            long lngMainSeq = 0;//��ⵥ��������
                            string m_strMSInID = string.Empty;//��ⵥ����
                            lngRes = m_lngAddNewInStorage( m_objMSInMainVO, out lngMainSeq, out m_strMSInID, 5);

                            string strInBillNo = string.Empty;
                            string strOutBillNo = string.Empty;
                            int intReturnCount = 0;
                            for (int i1 = 0; i1 < m_objDetailArr.Length; i1++)
                            {
                                m_objMSInDetailVO[i1] = new clsMS_InStorageDetail_VO();
                                m_objMSInDetailVO[i1].m_lngSERIESID_INT2 = lngMainSeq;
                                m_objMSInDetailVO[i1].m_intStatus = 1;
                                m_objMSInDetailVO[i1].m_strMEDICINEID_CHR = m_objDetailArr[i1].m_strMEDICINEID_CHR;
                                m_objMSInDetailVO[i1].m_strMEDICINENAME_VCH = m_objDetailArr[i1].m_strMEDICINENAME_VCHR;
                                m_objMSInDetailVO[i1].m_strMEDSPEC_VCHR = m_objDetailArr[i1].m_strMEDSPEC_VCHR;
                                m_objMSInDetailVO[i1].m_strLOTNO_VCHR = m_objDetailArr[i1].m_strLOTNO_VCHR;
                                m_objMSInDetailVO[i1].m_dblAMOUNT = m_objDetailArr[i1].m_dblOPAMOUNT_INT;
                                m_objMSInDetailVO[i1].m_dcmCALLPRICE_INT = (decimal)m_objDetailArr[i1].m_dblOPWHOLESALEPRICE_INT;
                                m_objMSInDetailVO[i1].m_dcmWHOLESALEPRICE_INT = (decimal)m_objDetailArr[i1].m_dblOPWHOLESALEPRICE_INT;
                                m_objMSInDetailVO[i1].m_dcmRETAILPRICE_INT = (decimal)m_objDetailArr[i1].m_dblOPRETAILPRICE_INT;
                                m_objMSInDetailVO[i1].m_strPRODUCTORID_CHR = m_objDetailArr[i1].m_strPRODUCTORID_CHR;
                                m_objMSInDetailVO[i1].m_strUNIT_VCHR = m_objDetailArr[i1].m_strOPUNIT_CHR;

                                //��ȡ��ҩ�����ⵥ��Ӧ��ҩ����ⵥ�š�ҩ����ⵥ��
                                m_lngGetInStorageID(Convert.ToInt64(m_objDetailArr[i1].m_intSTORAGESERIESID_CHR), out strInBillNo, out strOutBillNo);
                                if(strInBillNo.Length == 0)
                                {
                                    m_objMSInDetailVO[i1].m_strInStorageID = m_strMSInID;
                                    intReturnCount = 0;
                                }
                                else
                                {
                                    m_objMSInDetailVO[i1].m_strInStorageID = strInBillNo;
                                    //��ȡ�˿����
                                    m_lngGetReturnCount(strInBillNo, m_objDetailArr[i1].m_strMEDICINEID_CHR, out intReturnCount);
                                }
                                
                                m_objMSInDetailVO[i1].m_strOUTSTORAGEID_VCHR = strOutBillNo;
                                intReturnCount += 1;
                                m_objMSInDetailVO[i1].m_intRUTURNNUM_INT = intReturnCount;

                                m_objMSInDetailVO[i1].m_dtmVALIDPERIOD_DAT = m_objDetailArr[i1].m_datVALIDPERIOD_DAT;
                            }

                            m_lngAddInStorageDetail( ref m_objMSInDetailVO);
                        }
                    }
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
        /// ���ò����Ƿ�ҩ��
        /// </summary>
        /// <param name="p_strDrugId">����ҩ��</param>
        /// <param name="p_strDeptId">����ID</param>
        /// <param name="p_blnIsDrugStore">�Ƿ�ҩ��</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        [AutoComplete]
        private long m_lngCheckDrugStore(string p_strDrugId, string p_strDeptId, out bool p_blnIsDrugStore, out bool p_blnIsHospital)
        {
            p_blnIsDrugStore = false;
            p_blnIsHospital = false;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeptId))
            {
                return lngRes;
            }
            try
            {
                string strSQL = @"select a.deptid_chr,a.medstoretype_int
	from t_bse_medstore a
	left join t_aid_outindeptrelation b on b.instoragedept_chr = a.deptid_chr
 where a.deptid_chr = ?
	 and b.outstoragedept_chr = ?";
                //select a.deptid_chr from t_bse_medstore a where a.deptid_chr = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeptId;
                objDPArr[1].Value = p_strDrugId;
                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    if (Convert.ToString(dtbValue.Rows[0][0]) == p_strDrugId)//������ⲿ�ź���ⲿ����ͬ���򲻿���ⵥ
                        p_blnIsDrugStore = false;
                    else
                        p_blnIsDrugStore = true;

                    if (Convert.ToInt16(dtbValue.Rows[0][1]) == 2)//סԺҩ��
                        p_blnIsHospital = true;
                    else
                        p_blnIsHospital = false;
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

        #region ����ҩ�������ϸ����ÿ��
        /// <summary>
        /// ����ҩ�������ϸ����ÿ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objUpdateArr"></param>
        /// <param name="p_strMedicineName">ҩƷ���ƣ��������ؿ�治��ۼ�����Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStorageAvalid( clsDS_UpdateStorageBySeriesID_VO[] m_objUpdateArr, out string p_strMedicineName)
        {
            p_strMedicineName = string.Empty;
            long lngRes = 0;
            try
            {
                //20081112:�ֳ�������������ǣ����������Ǹ����ģ�������º�Ŀ��ÿ����Ϊ����������������
                clsDS_UpdateStorageBySeriesID_VO[] objPlusArr = null;//����
                clsDS_UpdateStorageBySeriesID_VO[] objNegaArr = null;//����
                int intPlusAmount = 0;//����������
                int intNegaAmount = 0;//����������
                for (int i1 = 0; i1 < m_objUpdateArr.Length; i1++)
                {
                    if (m_objUpdateArr[i1].m_dblIPAvalid > 0)
                    {
                        intPlusAmount++;
                    }
                    else
                    {
                        intNegaAmount++;
                    }
                }
                int intIndex = 0;
                if (intPlusAmount > 0)
                {
                    objPlusArr = new clsDS_UpdateStorageBySeriesID_VO[intPlusAmount];

                    for (int i1 = 0; i1 < m_objUpdateArr.Length; i1++)
                    {
                        if (m_objUpdateArr[i1].m_dblIPAvalid > 0)
                        {
                            objPlusArr[intIndex] = new clsDS_UpdateStorageBySeriesID_VO();
                            m_objUpdateArr[i1].m_mthCopyTo(objPlusArr[intIndex]);
                            intIndex++;
                        }
                    }
                }

                if (intNegaAmount > 0)
                {
                    intIndex = 0;
                    objNegaArr = new clsDS_UpdateStorageBySeriesID_VO[intNegaAmount];
                    for (int i1 = 0; i1 < m_objUpdateArr.Length; i1++)
                    {
                        if (m_objUpdateArr[i1].m_dblIPAvalid <= 0)
                        {
                            objNegaArr[intIndex] = new clsDS_UpdateStorageBySeriesID_VO();
                            m_objUpdateArr[i1].m_mthCopyTo(objNegaArr[intIndex]);
                            intIndex++;
                        }
                    }
                }

                string strSQL = "";
                clsHRPTableService objHRPServ = new clsHRPTableService();

                if (intNegaAmount > 0)
                {
                    strSQL = @"update t_ds_storage_detail set 
                opavailablegross_num = opavailablegross_num - (?),
                ipavailablegross_num = ipavailablegross_num - (?)
                where seriesid_int = ?";


                    DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Int64 };
                    object[][] objValues = new object[3][];
                    int intItemCount = objNegaArr.Length;
                    long lngReff = -1;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }
                    for (int iRow = 0; iRow < objNegaArr.Length; iRow++)
                    {
                        objValues[0][iRow] = objNegaArr[iRow].m_dblOPAvalid;
                        objValues[1][iRow] = objNegaArr[iRow].m_dblIPAvalid;
                        objValues[2][iRow] = objNegaArr[iRow].m_intSeriesID;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngReff, dbTypes);
                    if (lngReff < intItemCount)
                    {
                        throw new Exception("���¿��ÿ�����");
                    }
                }

                if (intPlusAmount > 0)
                {
                    strSQL = @"update t_ds_storage_detail set 
                opavailablegross_num = opavailablegross_num - (?),
                ipavailablegross_num = ipavailablegross_num - (?)
                where seriesid_int = ? and ipavailablegross_num - (?) >= 0";


                    DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Int64, DbType.Double };
                    object[][] objValues = new object[4][];
                    int intItemCount = objPlusArr.Length;
                    long lngReff = -1;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }
                    for (int iRow = 0; iRow < objPlusArr.Length; iRow++)
                    {
                        objValues[0][iRow] = objPlusArr[iRow].m_dblOPAvalid;
                        objValues[1][iRow] = objPlusArr[iRow].m_dblIPAvalid;
                        objValues[2][iRow] = objPlusArr[iRow].m_intSeriesID;
                        objValues[3][iRow] = objPlusArr[iRow].m_dblIPAvalid;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngReff, dbTypes);
                    if (lngReff < intItemCount)
                    {
                        //����ĸ�ҩƷ�����ۼ����
                        strSQL = @"select a.medicinename_vchr
  from t_ds_storage_detail a
 where a.seriesid_int = ?
   and a.ipavailablegross_num - (?) >= 0";

                        IDataParameter[] objDataParamArr = null;
                        DataTable dtbTemp = new DataTable();
                        for (int i1 = 0; i1 < objPlusArr.Length; i1++)
                        {
                            objDataParamArr = null;
                            objHRPServ.CreateDatabaseParameter(2, out objDataParamArr);
                            objDataParamArr[0].Value = objPlusArr[i1].m_intSeriesID;
                            objDataParamArr[1].Value = objPlusArr[i1].m_dblIPAvalid;
                            lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDataParamArr);
                            if (dtbTemp == null || dtbTemp.Rows.Count == 0)
                            {
                                p_strMedicineName = objPlusArr[i1].m_strMEDICINENAME_VCHR;
                                lngRes = -1;
                                return lngRes;
                            }
                        }
                    }
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

        #region ����ҩ�������������ϸ������
        /// <summary>
        /// ����ҩ�������������ϸ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objUpdateArr"></param>
        /// <param name="m_objDetailArr"></param>
        /// <param name="p_intCommitFolow">�Ƿ񱣴漴���</param>
        /// <param name="p_strExamerID">�������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOutStorageInfo( clsDS_OutStorage_VO m_objMainVo, clsDS_UpdateStorageBySeriesID_VO[] m_objUpdateArr, ref clsDS_Outstorage_Detail[] m_objDetailArr, int p_intCommitFolow, string p_strExamerID)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL = @"update   t_ds_outstorage
                                       set formtype_int = ?, drugstoreid_chr = ?, instoredept_chr = ?,
                                           comment_vchr = ?
                                     where seriesid_int = ?
                                       and status_int = 1";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(5, out objDataParm);
                objDataParm[0].Value = m_objMainVo.m_intFORMTYPE_INT;
                objDataParm[1].Value = m_objMainVo.m_strDRUGSTOREID_CHR;
                objDataParm[2].Value = m_objMainVo.m_strINSTOREDEPT_CHR;
                objDataParm[3].Value = m_objMainVo.m_strCOMMENT_VCHR;
                objDataParm[4].Value = m_objMainVo.m_lngSERIESID_INT;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                if(lngAffected != 1)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    ContextUtil.SetAbort();
                    return -99;
                }
                if (m_objDetailArr == null || m_objDetailArr.Length == 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    return -1;
                }
                System.Collections.Generic.List<clsDS_Outstorage_Detail> m_objInsertDetialArr = new List<clsDS_Outstorage_Detail>(m_objDetailArr.Length);
                System.Collections.Generic.List<clsDS_Outstorage_Detail> m_objUpdateDetialArr = new List<clsDS_Outstorage_Detail>(m_objDetailArr.Length);
                for (int i = 0; i < m_objDetailArr.Length; i++)
                {
                    if(m_objDetailArr[i].m_lngSERIESID_INT > 0)
                    {
                        m_objUpdateDetialArr.Add(m_objDetailArr[i]);
                    }
                    else
                    {
                        m_objInsertDetialArr.Add(m_objDetailArr[i]);
                    }

                   // m_objInsertDetialArr.Add(m_objDetailArr[i]);
                   ////if (m_objDetailArr[i].m_lngSERIESID_INT >= 0)
                   ////{
                   ////  m_objUpdateDetialArr.Add(m_objDetailArr[i]);
                   ////}
                   // if (m_objUpdateArr != null)
                   // {
                   //     for (int j = 0; j < m_objUpdateArr.Length; j++)
                   //     {
                   //         if (m_objDetailArr[i].m_intSTORAGESERIESID_CHR.Trim() == m_objUpdateArr[j].m_intSeriesID.ToString().Trim())
                   //         {
                   //             m_objUpdateDetialArr.Add(m_objDetailArr[i]);
                   //             m_objInsertDetialArr.Remove(m_objDetailArr[i]);
                   //         }
                   //     }
                   // }
                }
                m_objInsertDetialArr.TrimExcess();
                m_objUpdateDetialArr.TrimExcess();

                DbType[] dbTypes = null;
                object[][] objValues = null;
                int intItemCount = 0;
                string strMedicineName = string.Empty;

                if (m_objUpdateDetialArr.Count > 0)
                {
                    strSQL = @"update t_ds_outstorage_detail set 
             seriesid2_int=?, medicineid_chr=?, medicinename_vchr=?,
             medspec_vchr=?, opamount_int=?, opunit_chr=?, ipamount_int=?,
             ipunit_chr=?, packqty_dec=?, opwholesaleprice_int=?,
             ipwholesaleprice_int=?, opretailprice_int=?, ipretailprice_int=?,
             lotno_vchr=?, validperiod_dat=?, status=?,rejectreason = ?,storageseriesid_chr = ?,productorid_chr = ?
             where seriesid_int=?";
                    //,rejectreason = ?,storageseriesid_chr = ?�޸�ʱ�������?
                    dbTypes = new DbType[] { DbType.Int64, DbType.String, DbType.String, 
                    DbType.String, DbType.Double, DbType.String, DbType.Double, 
                    DbType.String, DbType.Double, DbType.Double, 
                    DbType.Double, DbType.Double, DbType.Double, 
                    DbType.String,DbType.DateTime, DbType.Int16,
                    DbType.String,DbType.Int64,DbType.String,DbType.Int64};
                    objValues = new object[20][];
                    intItemCount = m_objUpdateDetialArr.Count;
                    //clsDS_UpdateStorageBySeriesID_VO[] objModifyUpdateArr = new clsDS_UpdateStorageBySeriesID_VO[intItemCount];
                    clsDS_UpdateStorageBySeriesID_VO objModifyUpdate = null;
                    List<clsDS_UpdateStorageBySeriesID_VO> m_glstModifyUpdate = new List<clsDS_UpdateStorageBySeriesID_VO>(intItemCount);
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }
                    double m_dblOPAvalid = 0;
                    double m_dblIPAvalid = 0;
                    for (int iRow = 0; iRow < m_objUpdateDetialArr.Count; iRow++)
                    {
                        //objModifyUpdateArr[iRow] = new clsDS_UpdateStorageBySeriesID_VO();
                        m_dblOPAvalid = 0;
                        m_dblIPAvalid = 0;
                        objValues[0][iRow] = m_objUpdateDetialArr[iRow].m_lngSERIESID2_INT;
                        objValues[1][iRow] = m_objUpdateDetialArr[iRow].m_strMEDICINEID_CHR;
                        objValues[2][iRow] = m_objUpdateDetialArr[iRow].m_strMEDICINENAME_VCHR;
                        objValues[3][iRow] = m_objUpdateDetialArr[iRow].m_strMEDSPEC_VCHR;
                        objValues[4][iRow] = m_objUpdateDetialArr[iRow].m_dblOPAMOUNT_INT;
                        objValues[5][iRow] = m_objUpdateDetialArr[iRow].m_strOPUNIT_CHR;
                        objValues[6][iRow] = m_objUpdateDetialArr[iRow].m_dblIPAMOUNT_INT;
                        objValues[7][iRow] = m_objUpdateDetialArr[iRow].m_strIPUNIT_CHR;
                        objValues[8][iRow] = m_objUpdateDetialArr[iRow].m_dblPACKQTY_DEC;
                        objValues[9][iRow] = m_objUpdateDetialArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objValues[10][iRow] = m_objUpdateDetialArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objValues[11][iRow] = m_objUpdateDetialArr[iRow].m_dblOPRETAILPRICE_INT;
                        objValues[12][iRow] = m_objUpdateDetialArr[iRow].m_dblIPRETAILPRICE_INT;
                        objValues[13][iRow] = m_objUpdateDetialArr[iRow].m_strLOTNO_VCHR;
                        objValues[14][iRow] = m_objUpdateDetialArr[iRow].m_datVALIDPERIOD_DAT;
                        objValues[15][iRow] = m_objUpdateDetialArr[iRow].m_intSTATUS;
                        objValues[16][iRow] = m_objUpdateDetialArr[iRow].m_strRejectReason;
                        objValues[17][iRow] = m_objUpdateDetialArr[iRow].m_intSTORAGESERIESID_CHR;
                        objValues[18][iRow] = m_objUpdateDetialArr[iRow].m_strPRODUCTORID_CHR;
                        objValues[19][iRow] = m_objUpdateDetialArr[iRow].m_lngSERIESID_INT;
                        for (int i1 = 0; i1 < m_objUpdateArr.Length; i1++)
                        {
                            if(m_objUpdateArr[i1].m_lngOutSeriesID == m_objUpdateDetialArr[iRow].m_lngSERIESID_INT)
                            {
                                m_dblOPAvalid = m_objUpdateArr[i1].m_dblOPAvalid;
                                m_dblIPAvalid = m_objUpdateArr[i1].m_dblIPAvalid;
                                break;
                            }
                        }

                        if(m_objUpdateDetialArr[iRow].m_dblOPAMOUNT_INT + m_dblOPAvalid != 0)
                        {
                            objModifyUpdate = new clsDS_UpdateStorageBySeriesID_VO();
                            objModifyUpdate.m_dblOPAvalid = m_objUpdateDetialArr[iRow].m_dblOPAMOUNT_INT + m_dblOPAvalid;
                            objModifyUpdate.m_dblIPAvalid = m_objUpdateDetialArr[iRow].m_dblIPAMOUNT_INT + m_dblIPAvalid;
                            objModifyUpdate.m_intSeriesID = Convert.ToInt64(m_objUpdateDetialArr[iRow].m_intSTORAGESERIESID_CHR);
                            m_glstModifyUpdate.Add(objModifyUpdate);
                        } 
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    strMedicineName = string.Empty;
                    m_glstModifyUpdate.TrimExcess();
                    if(m_glstModifyUpdate.Count > 0)
                    {
                        lngRes = m_lngUpdateStorageAvalid( m_glstModifyUpdate.ToArray(), out strMedicineName);
                        if(lngRes <= 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception(strMedicineName + "���¿��ÿ�����");
                        }
                    }

                    //���漴���
                    if (p_intCommitFolow == 1 && m_objUpdateDetialArr.Count > 0)
                    {
                        clsDS_UpdateStorageBySeriesID_VO[] objStorageDetailArr = new clsDS_UpdateStorageBySeriesID_VO[m_objUpdateDetialArr.Count];
                        for (int i1 = 0; i1 < m_objUpdateDetialArr.Count; i1++)
                        {
                            objStorageDetailArr[i1] = new clsDS_UpdateStorageBySeriesID_VO();
                            objStorageDetailArr[i1].m_strMedicineCode = m_objUpdateDetialArr[i1].m_strMEDICINEID_CHR;
                            objStorageDetailArr[i1].m_strMEDICINENAME_VCHR = m_objUpdateDetialArr[i1].m_strMEDICINENAME_VCHR;
                            objStorageDetailArr[i1].m_strMEDSPEC_VCHR = m_objUpdateDetialArr[i1].m_strMEDSPEC_VCHR;
                            objStorageDetailArr[i1].m_strLOTNO_VCHR = m_objUpdateDetialArr[i1].m_strLOTNO_VCHR;
                            objStorageDetailArr[i1].m_strOPUNIT_CHR = m_objUpdateDetialArr[i1].m_strOPUNIT_CHR;
                            objStorageDetailArr[i1].m_dblOPReal = m_objUpdateDetialArr[i1].m_dblOPAMOUNT_INT;
                            objStorageDetailArr[i1].m_dblOPAvalid = m_objUpdateDetialArr[i1].m_dblOPAMOUNT_INT;
                            objStorageDetailArr[i1].m_dblOPRETAILPRICE_INT = m_objUpdateDetialArr[i1].m_dblOPRETAILPRICE_INT;
                            objStorageDetailArr[i1].m_dblOPWHOLESALEPRICE_INT = m_objUpdateDetialArr[i1].m_dblOPWHOLESALEPRICE_INT;
                            objStorageDetailArr[i1].m_strIPUNIT_CHR = m_objUpdateDetialArr[i1].m_strIPUNIT_CHR;
                            objStorageDetailArr[i1].m_dblIPReal = m_objUpdateDetialArr[i1].m_dblIPAMOUNT_INT;
                            objStorageDetailArr[i1].m_dblIPAvalid = m_objUpdateDetialArr[i1].m_dblIPAMOUNT_INT;
                            objStorageDetailArr[i1].m_dblIPRETAILPRICE_INT = m_objUpdateDetialArr[i1].m_dblIPRETAILPRICE_INT;
                            objStorageDetailArr[i1].m_dblIPWHOLESALEPRICE_INT = m_objUpdateDetialArr[i1].m_dblIPWHOLESALEPRICE_INT;
                            objStorageDetailArr[i1].m_dblPACKQTY_DEC = m_objUpdateDetialArr[i1].m_dblPACKQTY_DEC;
                            objStorageDetailArr[i1].m_dtmVALIDPERIOD_DAT = m_objUpdateDetialArr[i1].m_datVALIDPERIOD_DAT;
                            objStorageDetailArr[i1].m_strINSTOREID_VCHR = m_objUpdateDetialArr[i1].m_strInStorageid;
                            objStorageDetailArr[i1].m_strDrugID = m_objMainVo.m_strDRUGSTOREID_CHR;
                            objStorageDetailArr[i1].m_strDSINSTOREID_VCHR = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                            objStorageDetailArr[i1].m_strPRODUCTORID_CHR = m_objUpdateDetialArr[i1].m_strPRODUCTORID_CHR;
                            objStorageDetailArr[i1].m_intSeriesID = Convert.ToInt64(m_objUpdateDetialArr[i1].m_intSTORAGESERIESID_CHR);
                            objStorageDetailArr[i1].m_lngRELATEDSERIESID_INT = m_objUpdateDetialArr[i1].m_lngSERIESID_INT;
                        }
                        string strInfo = string.Empty;
                        lngRes = m_lngSubtractStorage( objStorageDetailArr, 2, out strInfo);
                        if (lngRes < 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception(strInfo);
                        }
                        lngRes = m_lngAddNewAccountDetail( objStorageDetailArr);
                        //lngRes = m_lngSubtractStorage( objStorageDetailArr, 2);
                    }
                }

                if (m_objInsertDetialArr.Count > 0)
                {
//                    strSQL = @"insert into t_ds_instorage_detail
//            (seriesid_int, seriesid2_int, medicineid_chr, medicinename_vchr,
//             medspec_vchr, opamount_int, opunit_chr, ipamount_int,
//             ipunit_chr, packqty_dec, opwholesaleprice_int,
//             ipwholesaleprice_int, opretailprice_int, ipretailprice_int,
//             lotno_vchr, validperiod_dat, status,rejectreason,storageseriesid_chr,productorid_chr
//            )
//     values (?, ?, ?, ?,
//             ?, ?, ?, ?,
//             ?, ?, ?,
//             ?, ?, ?,
//             ?, ?, ?,?,?,?
//            )";
                    strSQL = @"insert into t_ds_outstorage_detail t
  (seriesid_int,seriesid2_int,
   medicineid_chr,
   medicinename_vchr,
   medspec_vchr,
   opamount_int,
   opunit_chr,
   ipamount_int,
   ipunit_chr,
   packqty_dec,
   opwholesaleprice_int,
   ipwholesaleprice_int,
   opretailprice_int,
   ipretailprice_int,
   lotno_vchr,
   validperiod_dat,
   status,
   rejectreason,
   storageseriesid_chr,
   productorid_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    //long[] lngSEQArr = null;
                    //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_DS_OUTSTORAGE_DETAIL", m_objInsertDetialArr.Count, out lngSEQArr);
                    dbTypes = new DbType[] { DbType.Int64, DbType.Int64, DbType.String, DbType.String, 
                    DbType.String, DbType.Double, DbType.String, DbType.Double, 
                    DbType.String, DbType.Double, DbType.Double, 
                    DbType.Double, DbType.Double, DbType.Double, 
                    DbType.String,DbType.DateTime, DbType.Int16,DbType.String,DbType.String,DbType.String };
                    objValues = new object[20][];
                    intItemCount = m_objInsertDetialArr.Count;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }
                    clsDS_UpdateStorageBySeriesID_VO[] objModifyUpdateArr2 = new clsDS_UpdateStorageBySeriesID_VO[intItemCount];
                    for (int iRow = 0; iRow < m_objInsertDetialArr.Count; iRow++)
                    {
                        objModifyUpdateArr2[iRow] = new clsDS_UpdateStorageBySeriesID_VO();
                        m_objInsertDetialArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_DS_OUTSTORAGE_DETAIL"); //lngSEQArr[iRow];
                        objValues[0][iRow] = m_objInsertDetialArr[iRow].m_lngSERIESID_INT;
                        m_objInsertDetialArr[iRow].m_lngSERIESID2_INT = m_objMainVo.m_lngSERIESID_INT;
                        objValues[1][iRow] = m_objInsertDetialArr[iRow].m_lngSERIESID2_INT;
                        objValues[2][iRow] = m_objInsertDetialArr[iRow].m_strMEDICINEID_CHR;
                        objValues[3][iRow] = m_objInsertDetialArr[iRow].m_strMEDICINENAME_VCHR;
                        objValues[4][iRow] = m_objInsertDetialArr[iRow].m_strMEDSPEC_VCHR;
                        objValues[5][iRow] = m_objInsertDetialArr[iRow].m_dblOPAMOUNT_INT;
                        objValues[6][iRow] = m_objInsertDetialArr[iRow].m_strOPUNIT_CHR;
                        objValues[7][iRow] = m_objInsertDetialArr[iRow].m_dblIPAMOUNT_INT;
                        objValues[8][iRow] = m_objInsertDetialArr[iRow].m_strIPUNIT_CHR;
                        objValues[9][iRow] = m_objInsertDetialArr[iRow].m_dblPACKQTY_DEC;
                        objValues[10][iRow] = m_objInsertDetialArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                        objValues[11][iRow] = m_objInsertDetialArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                        objValues[12][iRow] = m_objInsertDetialArr[iRow].m_dblOPRETAILPRICE_INT;
                        objValues[13][iRow] = m_objInsertDetialArr[iRow].m_dblIPRETAILPRICE_INT;
                        objValues[14][iRow] = m_objInsertDetialArr[iRow].m_strLOTNO_VCHR;
                        objValues[15][iRow] = m_objInsertDetialArr[iRow].m_datVALIDPERIOD_DAT;
                        objValues[16][iRow] = m_objInsertDetialArr[iRow].m_intSTATUS;
                        objValues[17][iRow] = m_objInsertDetialArr[iRow].m_strRejectReason;
                        objValues[18][iRow] = m_objInsertDetialArr[iRow].m_intSTORAGESERIESID_CHR;
                        objValues[19][iRow] = m_objInsertDetialArr[iRow].m_strPRODUCTORID_CHR;

                        objModifyUpdateArr2[iRow].m_dblOPAvalid = m_objInsertDetialArr[iRow].m_dblOPAMOUNT_INT;
                        objModifyUpdateArr2[iRow].m_dblIPAvalid = m_objInsertDetialArr[iRow].m_dblIPAMOUNT_INT;
                        objModifyUpdateArr2[iRow].m_intSeriesID = Convert.ToInt64(m_objInsertDetialArr[iRow].m_intSTORAGESERIESID_CHR);
                        objModifyUpdateArr2[iRow].m_strMEDICINENAME_VCHR = m_objInsertDetialArr[iRow].m_strMEDICINENAME_VCHR;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    lngRes = m_lngUpdateStorageAvalid( objModifyUpdateArr2, out strMedicineName);
                    if (lngRes <= 0)
                    {
                        objHRPServ.Dispose();
                        objHRPServ = null;
                        throw new Exception(strMedicineName + "���¿��ÿ�����");
                    }


                    //���漴���
                    if (p_intCommitFolow == 1 && m_objInsertDetialArr.Count > 0)
                    {
                        clsDS_UpdateStorageBySeriesID_VO[] objStorageDetailInsertArr = new clsDS_UpdateStorageBySeriesID_VO[m_objInsertDetialArr.Count];
                        for (int i1 = 0; i1 < m_objInsertDetialArr.Count; i1++)
                        {
                            objStorageDetailInsertArr[i1] = new clsDS_UpdateStorageBySeriesID_VO();
                            objStorageDetailInsertArr[i1].m_strMedicineCode = m_objInsertDetialArr[i1].m_strMEDICINEID_CHR;
                            objStorageDetailInsertArr[i1].m_strMEDICINENAME_VCHR = m_objInsertDetialArr[i1].m_strMEDICINENAME_VCHR;
                            objStorageDetailInsertArr[i1].m_strMEDSPEC_VCHR = m_objInsertDetialArr[i1].m_strMEDSPEC_VCHR;
                            objStorageDetailInsertArr[i1].m_strLOTNO_VCHR = m_objInsertDetialArr[i1].m_strLOTNO_VCHR;
                            objStorageDetailInsertArr[i1].m_strOPUNIT_CHR = m_objInsertDetialArr[i1].m_strOPUNIT_CHR;
                            objStorageDetailInsertArr[i1].m_dblOPReal = m_objInsertDetialArr[i1].m_dblOPAMOUNT_INT;
                            objStorageDetailInsertArr[i1].m_dblOPAvalid = m_objInsertDetialArr[i1].m_dblOPAMOUNT_INT;
                            objStorageDetailInsertArr[i1].m_dblOPRETAILPRICE_INT = m_objInsertDetialArr[i1].m_dblOPRETAILPRICE_INT;
                            objStorageDetailInsertArr[i1].m_dblOPWHOLESALEPRICE_INT = m_objInsertDetialArr[i1].m_dblOPWHOLESALEPRICE_INT;
                            objStorageDetailInsertArr[i1].m_strIPUNIT_CHR = m_objInsertDetialArr[i1].m_strIPUNIT_CHR;
                            objStorageDetailInsertArr[i1].m_dblIPReal = m_objInsertDetialArr[i1].m_dblIPAMOUNT_INT;
                            objStorageDetailInsertArr[i1].m_dblIPAvalid = m_objInsertDetialArr[i1].m_dblIPAMOUNT_INT;
                            objStorageDetailInsertArr[i1].m_dblIPRETAILPRICE_INT = m_objInsertDetialArr[i1].m_dblIPRETAILPRICE_INT;
                            objStorageDetailInsertArr[i1].m_dblIPWHOLESALEPRICE_INT = m_objInsertDetialArr[i1].m_dblIPWHOLESALEPRICE_INT;
                            objStorageDetailInsertArr[i1].m_dblPACKQTY_DEC = m_objInsertDetialArr[i1].m_dblPACKQTY_DEC;
                            objStorageDetailInsertArr[i1].m_dtmVALIDPERIOD_DAT = m_objInsertDetialArr[i1].m_datVALIDPERIOD_DAT;
                            objStorageDetailInsertArr[i1].m_strINSTOREID_VCHR = m_objInsertDetialArr[i1].m_strInStorageid;
                            objStorageDetailInsertArr[i1].m_strDrugID = m_objMainVo.m_strDRUGSTOREID_CHR;
                            objStorageDetailInsertArr[i1].m_strDSINSTOREID_VCHR = m_objMainVo.m_strOUTDRUGSTOREID_VCHR;
                            objStorageDetailInsertArr[i1].m_strMEDICINETYPEID_CHR = m_objInsertDetialArr[i1].m_strMedicineTypeid;
                            objStorageDetailInsertArr[i1].m_strPRODUCTORID_CHR = m_objInsertDetialArr[i1].m_strPRODUCTORID_CHR;
                            objStorageDetailInsertArr[i1].m_intSeriesID = Convert.ToInt64(m_objInsertDetialArr[i1].m_intSTORAGESERIESID_CHR);
                            objStorageDetailInsertArr[i1].m_lngRELATEDSERIESID_INT = m_objInsertDetialArr[i1].m_lngSERIESID_INT;
                        }
                        string strInfo = string.Empty;
                        lngRes = m_lngSubtractStorage( objStorageDetailInsertArr, 2, out strInfo);
                        if (lngRes < 0)
                        {
                            objHRPServ.Dispose();
                            objHRPServ = null;
                            throw new Exception(strInfo);
                        }
                        lngRes = m_lngAddNewAccountDetail( objStorageDetailInsertArr);
                        //lngRes = m_lngSubtractStorage( objStorageDetailInsertArr, 2);
                    }

                }
                if (p_intCommitFolow == 1 && lngRes > 0)
                {
                    lngRes = m_lngOutstorageExam(p_strExamerID, m_objMainVo.m_lngSERIESID_INT);
                    if (lngRes <= 0)
                    {
                        objHRPServ.Dispose();
                        objHRPServ = null;
                        throw new Exception("��������߳���");
                    }
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

        #region ������ˮ��ɾ��ҩ��������ϸ
        /// <summary>
        /// ������ˮ��ɾ��ҩ��������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSeqid"></param>
        /// <param name="p_intMode">ɾ��ģʽ��0Ϊɾȫ����ϸ��1Ϊɾ������ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOutstorageDetail( long m_lngSeqid,int p_intMode)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                //��ȡ�����¿��
                clsDS_UpdateStorageBySeriesID_VO[] objForUpdateArr;
                clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
                objSelect.m_lngGetDetailForUpdate( m_lngSeqid, p_intMode,out objForUpdateArr);
                //����ΰ�޸��ж�:��ԭ�ȵĳ����ж��޸ĳɷǿ��ж�
                if (objForUpdateArr != null)
                {
                    string p_strMedicineName = string.Empty;
                    lngRes = m_lngUpdateStorageAvalid( objForUpdateArr, out p_strMedicineName);
                    if (lngRes <= 0)
                    {
                        throw new Exception(p_strMedicineName + "���¿��ÿ�����");
                    }
                }
                else
                {
                    return -11;
                }

                string strSQL = @"update t_ds_outstorage_detail a set a.status=0 where a.seriesid_int=?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objDataParm);
                objDataParm[0].Value = m_lngSeqid;
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

        #region ������ˮ��ɾ��ҩ����������
        /// <summary>
        /// ������ˮ��ɾ��ҩ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSeqid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOutstorage( long m_lngSeqid,int p_intMode)
        {
            long lngRes = 0;
            long lngAffected = -1;
            try
            {
                string strSQL = @"update t_ds_outstorage a set a.status_int = 0 where a.seriesid_int = ? and a.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objDataParm);
                objDataParm[0].Value = m_lngSeqid;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objDataParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                if(lngAffected != 1)
                {
                    ContextUtil.SetAbort();
                    return -99;
                }
                //ɾ���ӱ�
                lngRes = m_lngDelOutstorageDetail( m_lngSeqid, p_intMode);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        ///��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeriesID"></param>
        /// <param name="m_strEmpid"></param>
        /// <param name="m_strChittyid_vchr"></param>
        /// <param name="m_strDrugStoreid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOutstorageInAccount( long p_lngSeriesID, string m_strEmpid, string m_strChittyid_vchr, string m_strDrugStoreid)
        {
            long lngRes = -1;
            string strSQL;
            strSQL = @"update t_ds_outstorage a
                        set a.inaccounterid_chr=?,a.inaccount_dat=sysdate, a.status_int = 3
                        where  a.seriesid_int = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            objHRPServ.CreateDatabaseParameter(2, out objValues);

            objValues[0].Value = m_strEmpid;
            objValues[1].Value = p_lngSeriesID;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes > 0)
                {
                    strSQL = @"update t_ds_account_detail t
   set t.state_int = 1, t.inaccountid_chr = ?, t.inaccountdate_dat = sysdate
 where t.chittyid_vchr = ?
   and t.drugstoreid_int = ?
   and t.state_int = 2";
                    objValues = null;
                    objHRPServ.CreateDatabaseParameter(3, out objValues);
                    objValues[0].Value = m_strEmpid;
                    objValues[1].Value = m_strChittyid_vchr;
                    objValues[2].Value = m_strDrugStoreid;
                    lngAffected = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="m_strdrugstoreexamid"></param>
        /// <param name="m_datdrugstoreexam"></param>
        /// <param name="p_lngSeriesID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOutstorageExam(string m_strdrugstoreexamid, long p_lngSeriesID)
        {
            long lngRes = -1;
            string strSQL;
            strSQL = @"update  t_ds_outstorage a
                           set a.examid_chr = ?, a.examdate_dat = sysdate, a.status_int = 2
                         where a.seriesid_int = ?
                           and a.status_int = 1 ";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            objHRPServ.CreateDatabaseParameter(2, out objValues);

            objValues[0].Value = m_strdrugstoreexamid;
            objValues[1].Value = p_lngSeriesID;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                objHRPServ.Dispose();
                objHRPServ = null;
                if(lngAffected != 1)
                {
                    ContextUtil.SetAbort();
                    return -99;
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

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strdrugstoreexamid"></param>
        /// <param name="m_datdrugstoreexam"></param>
        /// <param name="p_lngSeriesID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOutstorageUnExam(long p_lngSeriesID)
        {
            long lngRes = -1;
            string strSQL;
            strSQL = @"update t_ds_outstorage a
                        set a.status_int = 1
                        where  a.seriesid_int = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            objHRPServ.CreateDatabaseParameter(1, out objValues);

            objValues[0].Value = p_lngSeriesID;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region �����˱���ϸ
        /// <summary>
        /// �����˱���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDurgStoreid"></param>
        /// <param name="m_strChittyid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateAccountDetail( string m_strDurgStoreid, string m_strChittyid)
        {

            long lngRes = -1;
            string strSQL;
            strSQL = @"update t_ds_account_detail t
   set t.state_int = 0
   where t.chittyid_vchr = ?
   and t.drugstoreid_int = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            objHRPServ.CreateDatabaseParameter(2, out objValues);
            objValues[0].Value = m_strChittyid;
            objValues[1].Value = m_strDurgStoreid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region ����ҩ�����
        /// <summary>
        /// ����ҩ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1:�ӿ��,2:�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddStorage( clsDS_UpdateStorageBySeriesID_VO[] p_objDetail, Int16 intType)
        {
            if (p_objDetail == null || p_objDetail.Length == 0)
            {
                return -1;
            }
            long lngRes = -1;
            bool p_blnHasDetail;
            long p_lngSeriesID;
            clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
            for (int intRow = 0; intRow < p_objDetail.Length; intRow++)
            {
                //�жϵ�ǰҩƷ�Ƿ��Ѵ��ڿ��������
                objSelect.m_lngCheckMedExistOutStorage(  p_objDetail[intRow].m_strMedicineCode, p_objDetail[intRow].m_strDrugID, out p_blnHasDetail, out p_lngSeriesID);
                if (p_blnHasDetail)
                {
                    //�޸Ŀ����������
                    lngRes = m_lngModifyStorageGross(p_objDetail[intRow].m_strDrugID, p_objDetail[intRow], intType, p_lngSeriesID);
                }
                if (lngRes != -1)
                {
                    //�޸Ŀ����ϸ���¼
                    lngRes = m_lngUpdateStorageDetail(p_objDetail[intRow], intType);
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
        /// <param name="intType">�޸����͡�1���ӿ��,2�������</param>
        /// <param name="p_lngSeriesID">����˳��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageGross(string p_strStorageID, clsDS_UpdateStorageBySeriesID_VO p_objDetail, Int16 intType, long p_lngSeriesID)
        {
            //�޸Ŀ������
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_ds_storage
       set opcurrentgross_num = opcurrentgross_num + ?,
       ipcurrentgross_num = ipcurrentgross_num + ?
       where seriesid_int = ?";
            objHRPServ.CreateDatabaseParameter(3, out objValues);
            //�жϵ�ǰΪ��ӿ�������Ǽ�С
            if (intType == 1)
            {
                objValues[0].Value = p_objDetail.m_dblOPAvalid;
                objValues[1].Value = p_objDetail.m_dblIPAvalid;
                objValues[2].Value = p_lngSeriesID;
            }
            else
            {
                objValues[0].Value = -p_objDetail.m_dblOPAvalid;
                objValues[1].Value = -p_objDetail.m_dblIPAvalid;
                objValues[2].Value = p_lngSeriesID;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region ����ҩ�����
        /// <summary>
        /// ����ҩ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1:�ӿ��,2:�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSubtractStorage( clsDS_UpdateStorageBySeriesID_VO[] p_objDetail, Int16 intType, out string p_strErrorInfo)
        {
            //���ؾ���ĳ��ҩƷ�������Ϣ
            p_strErrorInfo = string.Empty;
            if (p_objDetail == null || p_objDetail.Length == 0)
            {
                return -1;
            }
            long lngRes = -1;
            bool p_blnHasDetail;
            long p_lngSeriesID;
            string p_strMedicineName = string.Empty;
            clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
            for (int intRow = 0; intRow < p_objDetail.Length; intRow++)
            {
                p_strMedicineName = p_objDetail[intRow].m_strMEDICINENAME_VCHR;
                //�жϵ�ǰҩƷ�Ƿ��Ѵ��ڿ��������
                objSelect.m_lngCheckMedExistOutStorage(  p_objDetail[intRow].m_strMedicineCode, p_objDetail[intRow].m_strDrugID, out p_blnHasDetail, out p_lngSeriesID);
                if (p_blnHasDetail)
                {
                    //�޸Ŀ����������
                    lngRes = m_lngModifyStorageGross(p_objDetail[intRow].m_strDrugID, p_objDetail[intRow], intType, p_lngSeriesID);
                    if (lngRes != -1)
                    {
                        //���¿����ϸ���¼
                        lngRes = m_lngUpdateStorageDetail(p_objDetail[intRow], intType);
                        if (lngRes == -1)
                        {
                            p_strErrorInfo = p_strMedicineName + "���¿����ϸʱ������������ο�������Ƿ��㹻�ۼ�";
                            return lngRes;
                        }
                    }
                    else
                    {
                        p_strErrorInfo = p_strMedicineName + "�޸Ŀ���������";
                        return lngRes;
                    }
                }
                else
                {
                    p_strErrorInfo = p_strMedicineName + "�����������";
                    return lngRes;
                }
            }
            return lngRes;
        }
        #endregion

        #region ���¿����ϸ����¼(������ϸ)
        /// <summary>
        /// ���¿����ϸ����¼(������ϸ)
        /// </summary>
        /// <param name="m_objForUpdateArr">��¼</param>
        /// <param name="intType">2Ϊ���٣�1Ϊ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStorageDetail(clsDS_UpdateStorageBySeriesID_VO m_objForUpdateArr, Int16 intType)
        {

            long lngRes = -1;
            string strSQL;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;

            if (intType == 1)//���
            {
                strSQL = @" update t_ds_storage_detail a
                                  set a.oprealgross_int      = a.oprealgross_int + ?,
                                      a.iprealgross_int      = a.iprealgross_int + ?
                                  where a.seriesid_int = ? and a.iprealgross_int + ? >= 0";//20080509 ��治��С��0  
                objHRPServ.CreateDatabaseParameter(4, out objValues);
                objValues[0].Value = m_objForUpdateArr.m_dblOPReal;
                objValues[1].Value = m_objForUpdateArr.m_dblIPReal;
                objValues[2].Value = m_objForUpdateArr.m_intSeriesID;
                objValues[3].Value = m_objForUpdateArr.m_dblIPReal;
            }
            else//����
            {
                if (m_objForUpdateArr.m_dblIPReal < 0)
                {
                    strSQL = @" update t_ds_storage_detail a
                                  set a.oprealgross_int      = a.oprealgross_int + ?,
                                      a.iprealgross_int      = a.iprealgross_int + ?
                                  where a.seriesid_int = ?";//20080924 ��������Ϊ����ʱ�����ÿ����
                    objHRPServ.CreateDatabaseParameter(3, out objValues);
                    objValues[0].Value = -m_objForUpdateArr.m_dblOPReal;
                    objValues[1].Value = -m_objForUpdateArr.m_dblIPReal;
                    objValues[2].Value = m_objForUpdateArr.m_intSeriesID;
                }
                else
                {
                    strSQL = @" update t_ds_storage_detail a
                                  set a.oprealgross_int      = a.oprealgross_int + ?,
                                      a.iprealgross_int      = a.iprealgross_int + ?
                                  where a.seriesid_int = ? and a.iprealgross_int + ? >= 0";//20080509 ��治��С��0  

                    objHRPServ.CreateDatabaseParameter(4, out objValues);
                    objValues[0].Value = -m_objForUpdateArr.m_dblOPReal;
                    objValues[1].Value = -m_objForUpdateArr.m_dblIPReal;
                    objValues[2].Value = m_objForUpdateArr.m_intSeriesID;
                    objValues[3].Value = -m_objForUpdateArr.m_dblIPReal;
                }
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngAffected <= 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    lngRes = -1;
                    return lngRes;
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

        #region �����˱���ϸ
        /// <summary>
        /// �����˱���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objAccountDetailArr">�˱���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewAccountDetail( clsDS_UpdateStorageBySeriesID_VO[] m_objForUpdateArr)
        {
            if (m_objForUpdateArr == null || m_objForUpdateArr.Length == 0)
            {
                return -1;
            }
            clsDS_StorageDetail_VO[] p_objAccountDetailArr = new clsDS_StorageDetail_VO[m_objForUpdateArr.Length];
            for (int i = 0; i < m_objForUpdateArr.Length; i++)
            {
                p_objAccountDetailArr[i] = new clsDS_StorageDetail_VO();
                p_objAccountDetailArr[i].m_strMEDICINEID_CHR = m_objForUpdateArr[i].m_strMedicineCode;
                p_objAccountDetailArr[i].m_strMEDICINENAME_VCHR = m_objForUpdateArr[i].m_strMEDICINENAME_VCHR;
                p_objAccountDetailArr[i].m_strMEDICINETYPEID_CHR = m_objForUpdateArr[i].m_strMEDICINETYPEID_CHR;
                p_objAccountDetailArr[i].m_strMEDSPEC_VCHR = m_objForUpdateArr[i].m_strMEDSPEC_VCHR;
                p_objAccountDetailArr[i].m_strDRUGSTOREID_CHR = m_objForUpdateArr[i].m_strDrugID;
                p_objAccountDetailArr[i].m_strLOTNO_VCHR = m_objForUpdateArr[i].m_strLOTNO_VCHR;
                p_objAccountDetailArr[i].m_dtmVALIDPERIOD_DAT = m_objForUpdateArr[i].m_dtmVALIDPERIOD_DAT;
                p_objAccountDetailArr[i].m_strINSTOREID_VCHR = m_objForUpdateArr[i].m_strINSTOREID_VCHR;
                p_objAccountDetailArr[i].m_strDSINSTOREID_VCHR = m_objForUpdateArr[i].m_strDSINSTOREID_VCHR;
                p_objAccountDetailArr[i].m_dblIPWHOLESALEPRICE_INT = m_objForUpdateArr[i].m_dblIPWHOLESALEPRICE_INT;
                p_objAccountDetailArr[i].m_dblOPWHOLESALEPRICE_INT = m_objForUpdateArr[i].m_dblOPWHOLESALEPRICE_INT;
                p_objAccountDetailArr[i].m_dblIPRETAILPRICE_INT = m_objForUpdateArr[i].m_dblIPRETAILPRICE_INT;
                p_objAccountDetailArr[i].m_dblOPRETAILPRICE_INT = m_objForUpdateArr[i].m_dblOPRETAILPRICE_INT;
                p_objAccountDetailArr[i].m_dtmINSTORAGEDATE_DAT = m_objForUpdateArr[i].m_dtmINSTORAGEDATE_DAT;
                p_objAccountDetailArr[i].m_strIPUNIT_CHR = m_objForUpdateArr[i].m_strIPUNIT_CHR;
                p_objAccountDetailArr[i].m_dblIPREALGROSS_INT = m_objForUpdateArr[i].m_dblIPAvalid;
                p_objAccountDetailArr[i].m_dblOPREALGROSS_INT = m_objForUpdateArr[i].m_dblOPAvalid;
                p_objAccountDetailArr[i].m_strOPUNIT_CHR = m_objForUpdateArr[i].m_strOPUNIT_CHR;
                //p_objAccountDetailArr[i].m_dblOldIPREALGROSS_INT = m_objForUpdateArr[i].m_dblOldIPREALGROSS_INT;
                //p_objAccountDetailArr[i].m_dblOldOPREALGROSS_INT = m_objForUpdateArr[i].m_dblOldOPREALGROSS_INT;
                p_objAccountDetailArr[i].m_strDRUGSTOREID_CHR = m_objForUpdateArr[i].m_strDrugID;
                p_objAccountDetailArr[i].m_intType = m_objForUpdateArr[i].m_intType;
                p_objAccountDetailArr[i].m_lngSERIESID_INT = m_objForUpdateArr[i].m_intSeriesID;
                p_objAccountDetailArr[i].m_strPRODUCTORID_CHR = m_objForUpdateArr[i].m_strPRODUCTORID_CHR;
                p_objAccountDetailArr[i].m_dblPACKQTY_DEC = m_objForUpdateArr[i].m_dblPACKQTY_DEC;

                p_objAccountDetailArr[i].m_lngRELATEDSERIESID_INT = m_objForUpdateArr[i].m_lngRELATEDSERIESID_INT;
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
   a.productorid_chr,
   a.operatedate_dat,a.packqty_dec)
  values( ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         ?,
         sysdate,?
          )";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();
                clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
                //long[] lngSEQArr = null;

                //lngRes = objPublic.m_lngGetSequenceArr( "SEQ_DS_ACCOUNT_DETAIL", p_objAccountDetailArr.Length, out lngSEQArr);


                DbType[] dbTypes = new DbType[] { DbType.Int64,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.DateTime,
                        DbType.String,DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.DateTime,DbType.String,DbType.Double,DbType.Double,DbType.String,
                        DbType.Double,DbType.Double,DbType.Int16,DbType.String,DbType.String,DbType.Int16,DbType.Int16,DbType.Int16,DbType.Double,DbType.Double,
                        DbType.Double,DbType.Double,DbType.Double,DbType.Double,DbType.String,DbType.DateTime,DbType.String,DbType.String,DbType.Double};

                object[][] objValues = new object[37][];

                int intItemCount = p_objAccountDetailArr.Length;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��

                }
                double m_dblOPAmount = 0d;
                double m_dblIPAmount = 0d;
                //if (lngSEQArr == null || lngSEQArr.Length == 0)
                //{
                //    return -1;
                //}

                for (int iRow = 0; iRow < intItemCount; iRow++)
                {
                    objValues[0][iRow] = objPublic.GetSeqNextVal("SEQ_DS_ACCOUNT_DETAIL"); // lngSEQArr[iRow];
                    objValues[1][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINEID_CHR;
                    objValues[2][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINENAME_VCHR;
                    objValues[3][iRow] = p_objAccountDetailArr[iRow].m_strMEDICINETYPEID_CHR;
                    objValues[4][iRow] = p_objAccountDetailArr[iRow].m_strMEDSPEC_VCHR;
                    objValues[5][iRow] = p_objAccountDetailArr[iRow].m_strDRUGSTOREID_CHR;
                    objValues[6][iRow] = p_objAccountDetailArr[iRow].m_strLOTNO_VCHR;
                    objValues[7][iRow] = p_objAccountDetailArr[iRow].m_dtmVALIDPERIOD_DAT;
                    objValues[8][iRow] = p_objAccountDetailArr[iRow].m_strINSTOREID_VCHR;
                    objValues[9][iRow] = p_objAccountDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                    objValues[10][iRow] = p_objAccountDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                    objValues[11][iRow] = p_objAccountDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                    objValues[12][iRow] = p_objAccountDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                    objValues[13][iRow] = p_objAccountDetailArr[iRow].m_dtmINSTORAGEDATE_DAT;
                    objValues[14][iRow] = p_objAccountDetailArr[iRow].m_strIPUNIT_CHR;
                    objValues[15][iRow] = p_objAccountDetailArr[iRow].m_dblIPREALGROSS_INT;
                    objValues[16][iRow] = p_objAccountDetailArr[iRow].m_dblOPREALGROSS_INT;
                    objValues[17][iRow] = p_objAccountDetailArr[iRow].m_strOPUNIT_CHR;
                    objSelect.m_lngGetCurrentGrossByConditions(p_objAccountDetailArr[iRow].m_lngSERIESID_INT, ref m_dblOPAmount, ref m_dblIPAmount);
                    p_objAccountDetailArr[iRow].m_dblOldIPREALGROSS_INT = m_dblIPAmount;
                    p_objAccountDetailArr[iRow].m_dblOldOPREALGROSS_INT = m_dblOPAmount;
                    p_objAccountDetailArr[iRow].m_lngRELATEDSERIESID_INT = p_objAccountDetailArr[iRow].m_lngRELATEDSERIESID_INT;
                    objValues[18][iRow] = p_objAccountDetailArr[iRow].m_dblOldIPREALGROSS_INT;
                    objValues[19][iRow] = p_objAccountDetailArr[iRow].m_dblOldOPREALGROSS_INT;
                    objValues[20][iRow] = 2;
                    objValues[21][iRow] = p_objAccountDetailArr[iRow].m_strDRUGSTOREID_CHR;
                    objValues[22][iRow] = p_objAccountDetailArr[iRow].m_strDSINSTOREID_VCHR;
                    objValues[23][iRow] = p_objAccountDetailArr[iRow].m_intType;
                    objValues[24][iRow] = 2;
                    objValues[25][iRow] = 0;
                    objValues[26][iRow] = 0;
                    objValues[27][iRow] = 0;
                    objValues[28][iRow] = 0;
                    objValues[29][iRow] = 0;
                    objValues[30][iRow] = 0;
                    objValues[31][iRow] = 0;
                    objValues[32][iRow] = string.Empty;
                    objValues[33][iRow] = DateTime.MinValue;
                    objValues[34][iRow] = string.Empty;
                    objValues[35][iRow] = p_objAccountDetailArr[iRow].m_strPRODUCTORID_CHR;
                    objValues[36][iRow] = p_objAccountDetailArr[iRow].m_dblPACKQTY_DEC;

                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                //���������ϸ��Ľ����
                if (lngRes > 0)
                {
                    strSQL = @"update t_ds_outstorage_detail a
   set a.opoldgross_int = ?, a.ipoldgross_int = ?
 where a.seriesid_int = ?";

                    DbType[] dbTypes2 = new DbType[] { DbType.Double, DbType.Double, DbType.Int64 };
                    object[][] objValues2 = new object[3][];
                    intItemCount = p_objAccountDetailArr.Length;
                    for (int j = 0; j < objValues2.Length; j++)
                    {
                        objValues2[j] = new object[intItemCount];//��ʼ��

                    }
                    for (int k = 0; k < intItemCount; k++)
                    {
                        objValues2[0][k] = p_objAccountDetailArr[k].m_dblOldOPREALGROSS_INT;
                        objValues2[1][k] = p_objAccountDetailArr[k].m_dblOldIPREALGROSS_INT;
                        objValues2[2][k] = p_objAccountDetailArr[k].m_lngRELATEDSERIESID_INT;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues2, dbTypes2);
                    if (lngRes <= 0)
                    {
                        objHRPServ.Dispose();
                        objHRPServ = null;
                        throw new Exception("������ϸ����������");
                    }
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

        #region ������벿����ҩ������ͬʱ����һ�Ŵ�ҩ������ⵥ��
        /// <summary>
        /// ������벿����ҩ������ͬʱ����һ�Ŵ�ҩ������ⵥ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeqid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddInstorage( long p_lngSeqid)
        {
            long lngRes = -1;
            if (p_lngSeqid == 0)
                return lngRes;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_OutStorage_VO objOutMainVO = null;
                clsOutstorage_Supported_SVC objSelect = new clsOutstorage_Supported_SVC();
                objSelect.m_mthGetOutstorageInfoBySeriesID( p_lngSeqid, out objOutMainVO);
                bool m_blnIsDrugStore = false;
                bool m_blnIsHospital = false;
                m_lngCheckDrugStore(objOutMainVO.m_strDRUGSTOREID_CHR, objOutMainVO.m_strINSTOREDEPT_CHR, out m_blnIsDrugStore, out m_blnIsHospital);
                if (m_blnIsDrugStore == false)
                {
                    //20090622:����Ƿ���ҩ�⣬������һ��ҩ�����˵�
                    string strSQL = @"select a.setstatus_int from t_sys_setting a where a.setid_chr = '0420'";
                    DataTable dtbTemp = new DataTable();
                    objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbTemp);
                    if (dtbTemp.Rows.Count > 0)
                    {
                        if (dtbTemp.Rows[0][0].ToString() == "0")
                            return lngRes;
                    }
                    
                    string m_strStorageID = string.Empty;//ҩ��ID
                    m_lngCheckMedStore(objOutMainVO.m_strINSTOREDEPT_CHR, out m_strStorageID);
                    if (m_strStorageID.Length > 0)
                    {
                        clsDS_Outstorage_Detail[] objOutDetailVO = null;
                        DataTable dtbDetail = new DataTable();
                        objSelect.m_lngGetOutstorageDetailByID( m_blnIsHospital, p_lngSeqid, out dtbDetail);
                        if (dtbDetail != null && dtbDetail.Rows.Count > 0)
                        {
                            objOutDetailVO = new clsDS_Outstorage_Detail[dtbDetail.Rows.Count];
                            DataRow dr = null;
                            for (int i1 = 0; i1 < dtbDetail.Rows.Count; i1++)
                            {
                                dr = dtbDetail.Rows[i1];
                                objOutDetailVO[i1] = new clsDS_Outstorage_Detail();
                                objOutDetailVO[i1].m_dblPACKQTY_DEC = Convert.ToDouble(dr["packqty_dec"]);
                                objOutDetailVO[i1].m_strMEDICINEID_CHR = dr["medicineid_chr"].ToString();
                                objOutDetailVO[i1].m_strMEDICINENAME_VCHR = dr["medicinename_vchr"].ToString();
                                objOutDetailVO[i1].m_strMEDSPEC_VCHR = dr["medspec_vchr"].ToString();
                                objOutDetailVO[i1].m_dblOPAMOUNT_INT = Convert.ToDouble(dr["opamount_int"]);
                                objOutDetailVO[i1].m_strOPUNIT_CHR = dr["opunit_chr"].ToString();
                                objOutDetailVO[i1].m_dblIPAMOUNT_INT = Convert.ToDouble(dr["ipamount_int"]);
                                objOutDetailVO[i1].m_strIPUNIT_CHR = dr["ipunit_chr"].ToString();
                                objOutDetailVO[i1].m_dblOPWHOLESALEPRICE_INT = Convert.ToDouble(dr["opwholesaleprice_int"]);
                                objOutDetailVO[i1].m_dblIPWHOLESALEPRICE_INT = Convert.ToDouble(dr["ipwholesaleprice_int"]);
                                objOutDetailVO[i1].m_dblOPRETAILPRICE_INT = Convert.ToDouble(dr["opretailprice_int"]);
                                objOutDetailVO[i1].m_dblIPRETAILPRICE_INT = Convert.ToDouble(dr["ipretailprice_int"]);
                                objOutDetailVO[i1].m_strLOTNO_VCHR = dr["lotno_vchr"].ToString();
                                objOutDetailVO[i1].m_datVALIDPERIOD_DAT = Convert.ToDateTime(dr["validperiod_dat"]);
                                objOutDetailVO[i1].m_strInStorageid = dr["instoreid_vchr"].ToString();
                                objOutDetailVO[i1].m_dtmINSTORAGEDATE_DAT = Convert.ToDateTime(dr["instoragedate_dat"]);
                                objOutDetailVO[i1].m_strPRODUCTORID_CHR = dr["productorid_chr"].ToString();
                                objOutDetailVO[i1].m_intSTORAGESERIESID_CHR = dr["storageseriesid_chr"].ToString();
                            }
                        }

                        clsMS_InStorage_VO m_objMSInMainVO = new clsMS_InStorage_VO();
                        clsMS_InStorageDetail_VO[] m_objMSInDetailVO = new clsMS_InStorageDetail_VO[objOutDetailVO.Length];

                        m_objMSInMainVO.m_strSTORAGEID_CHR = m_strStorageID;
                        m_objMSInMainVO.m_strRETURNDEPT_CHR = objOutMainVO.m_strDRUGSTOREID_CHR;
                        m_objMSInMainVO.m_dtmNEWORDER_DAT = objOutMainVO.m_datEXAM_DATE;
                        m_objMSInMainVO.m_dtmINSTORAGEDATE_DAT = objOutMainVO.m_datEXAM_DATE;
                        m_objMSInMainVO.m_intSTATE_INT = 1;
                        m_objMSInMainVO.m_strOUTDRUGSTOREID_VCHR = objOutMainVO.m_strOUTDRUGSTOREID_VCHR;
                        m_objMSInMainVO.m_strMAKERID_CHR = objOutMainVO.m_strMAKERID_CHR;
                        m_objMSInMainVO.m_strCOMMNET_VCHR = "��Ӧ��ҩ�����ⵥ�ţ�" + objOutMainVO.m_strOUTDRUGSTOREID_VCHR;
                        m_objMSInMainVO.m_intFORMTYPE_INT = 2;

                        long lngMainSeq = 0;//��ⵥ��������
                        string m_strMSInID = string.Empty;//��ⵥ����
                        lngRes = m_lngAddNewInStorage( m_objMSInMainVO, out lngMainSeq, out m_strMSInID, 5);
                        lngRes = m_lngUpdateInStorageIDToOutDrugStorage(p_lngSeqid, m_strMSInID);

                        string strInBillNo = string.Empty;
                        string strOutBillNo = string.Empty;
                        int intReturnCount = 0;
                        for (int i1 = 0; i1 < objOutDetailVO.Length; i1++)
                        {
                            m_objMSInDetailVO[i1] = new clsMS_InStorageDetail_VO();
                            m_objMSInDetailVO[i1].m_lngSERIESID_INT2 = lngMainSeq;
                            m_objMSInDetailVO[i1].m_intStatus = 1;
                            m_objMSInDetailVO[i1].m_strMEDICINEID_CHR = objOutDetailVO[i1].m_strMEDICINEID_CHR;
                            m_objMSInDetailVO[i1].m_strMEDICINENAME_VCH = objOutDetailVO[i1].m_strMEDICINENAME_VCHR;
                            m_objMSInDetailVO[i1].m_strMEDSPEC_VCHR = objOutDetailVO[i1].m_strMEDSPEC_VCHR;
                            m_objMSInDetailVO[i1].m_strLOTNO_VCHR = objOutDetailVO[i1].m_strLOTNO_VCHR;
                            m_objMSInDetailVO[i1].m_dblAMOUNT = objOutDetailVO[i1].m_dblOPAMOUNT_INT;
                            m_objMSInDetailVO[i1].m_dcmCALLPRICE_INT = (decimal)objOutDetailVO[i1].m_dblOPWHOLESALEPRICE_INT;
                            m_objMSInDetailVO[i1].m_dcmWHOLESALEPRICE_INT = (decimal)objOutDetailVO[i1].m_dblOPWHOLESALEPRICE_INT;
                            m_objMSInDetailVO[i1].m_dcmRETAILPRICE_INT = (decimal)objOutDetailVO[i1].m_dblOPRETAILPRICE_INT;
                            m_objMSInDetailVO[i1].m_strPRODUCTORID_CHR = objOutDetailVO[i1].m_strPRODUCTORID_CHR;
                            m_objMSInDetailVO[i1].m_strUNIT_VCHR = objOutDetailVO[i1].m_strOPUNIT_CHR;

                            //��ȡ��ҩ�����ⵥ��Ӧ��ҩ����ⵥ�š�ҩ����ⵥ��
                            m_lngGetInStorageID(Convert.ToInt64(objOutDetailVO[i1].m_intSTORAGESERIESID_CHR), out strInBillNo, out strOutBillNo);
                            if(strInBillNo.Length == 0)
                            {
                                m_objMSInDetailVO[i1].m_strInStorageID = m_strMSInID;
                                intReturnCount = 0;
                            }
                            else
                            {
                                m_objMSInDetailVO[i1].m_strInStorageID = strInBillNo;
                                //��ȡ�˿����
                                m_lngGetReturnCount(strInBillNo, objOutDetailVO[i1].m_strMEDICINEID_CHR, out intReturnCount);
                            }

                            m_objMSInDetailVO[i1].m_strOUTSTORAGEID_VCHR = strOutBillNo;
                            intReturnCount += 1;
                            m_objMSInDetailVO[i1].m_intRUTURNNUM_INT = intReturnCount;

                            m_objMSInDetailVO[i1].m_dtmVALIDPERIOD_DAT = objOutDetailVO[i1].m_datVALIDPERIOD_DAT;
                        }

                        m_lngAddInStorageDetail( ref m_objMSInDetailVO);
                    }
                }
                else
                {
                    clsDS_Outstorage_Detail[] objOutDetailVO = null;
                    DataTable dtbDetail = new DataTable();
                    objSelect.m_lngGetOutstorageDetailByID( m_blnIsHospital, p_lngSeqid, out dtbDetail);
                    if (dtbDetail != null && dtbDetail.Rows.Count > 0)
                    {
                        objOutDetailVO = new clsDS_Outstorage_Detail[dtbDetail.Rows.Count];
                        DataRow dr = null;
                        for (int i1 = 0; i1 < dtbDetail.Rows.Count; i1++)
                        {
                            dr = dtbDetail.Rows[i1];
                            objOutDetailVO[i1] = new clsDS_Outstorage_Detail();
                            objOutDetailVO[i1].m_dblPACKQTY_DEC = Convert.ToDouble(dr["packqty_dec"]);
                            objOutDetailVO[i1].m_strMEDICINEID_CHR = dr["medicineid_chr"].ToString();
                            objOutDetailVO[i1].m_strMEDICINENAME_VCHR = dr["medicinename_vchr"].ToString();
                            objOutDetailVO[i1].m_strMEDSPEC_VCHR = dr["medspec_vchr"].ToString();
                            objOutDetailVO[i1].m_dblOPAMOUNT_INT = Convert.ToDouble(dr["opamount_int"]);
                            objOutDetailVO[i1].m_strOPUNIT_CHR = dr["opunit_chr"].ToString();
                            objOutDetailVO[i1].m_dblIPAMOUNT_INT = Convert.ToDouble(dr["ipamount_int"]);
                            objOutDetailVO[i1].m_strIPUNIT_CHR = dr["ipunit_chr"].ToString();
                            objOutDetailVO[i1].m_dblOPWHOLESALEPRICE_INT = Convert.ToDouble(dr["opwholesaleprice_int"]);
                            objOutDetailVO[i1].m_dblIPWHOLESALEPRICE_INT = Convert.ToDouble(dr["ipwholesaleprice_int"]);
                            objOutDetailVO[i1].m_dblOPRETAILPRICE_INT = Convert.ToDouble(dr["opretailprice_int"]);
                            objOutDetailVO[i1].m_dblIPRETAILPRICE_INT = Convert.ToDouble(dr["ipretailprice_int"]);
                            objOutDetailVO[i1].m_strLOTNO_VCHR = dr["lotno_vchr"].ToString();
                            objOutDetailVO[i1].m_datVALIDPERIOD_DAT = Convert.ToDateTime(dr["validperiod_dat"]);
                            objOutDetailVO[i1].m_strInStorageid = dr["instoreid_vchr"].ToString();
                            objOutDetailVO[i1].m_dtmINSTORAGEDATE_DAT = Convert.ToDateTime(dr["instoragedate_dat"]);
                            objOutDetailVO[i1].m_strPRODUCTORID_CHR = dr["productorid_chr"].ToString();
                        }
                    }


                    clsInstorage_SVC m_objInstorageSvc = new clsInstorage_SVC();
                    clsDS_Instorage_VO m_objMainInstorageVo = new clsDS_Instorage_VO();
                    clsDS_Instorage_Detail[] m_oInstoragebjDetailArr = new clsDS_Instorage_Detail[objOutDetailVO.Length];
                    m_objMainInstorageVo.m_datMAKEORDER_DAT = objOutMainVO.m_datEXAM_DATE;//m_datMAKEORDER_DAT
                    m_objMainInstorageVo.m_datSTORAGEEXAM_DATE = objOutMainVO.m_datEXAM_DATE;
                    //2��ҩ���������(��Դ�����Ǳ�ҩ��)  3��ҩ���������Դ����������ҩ����
                    if (objOutMainVO.m_strDRUGSTOREID_CHR == objOutMainVO.m_strINSTOREDEPT_CHR)
                    {
                        m_objMainInstorageVo.m_intFORMTYPE_INT = 2;
                    }
                    else
                    {
                        m_objMainInstorageVo.m_intFORMTYPE_INT = 3;
                    }
                    m_objMainInstorageVo.m_intSTATUS = 1;
                    m_objMainInstorageVo.m_strBORROWDEPT_CHR = objOutMainVO.m_strDRUGSTOREID_CHR;
                    m_objMainInstorageVo.m_strDRUGSTOREID_INT = objOutMainVO.m_strINSTOREDEPT_CHR;
                    m_objMainInstorageVo.m_strMAKERID_CHR = objOutMainVO.m_strMAKERID_CHR;
                    m_objMainInstorageVo.m_strOUTSTORAGEID_VCHR = objOutMainVO.m_strOUTDRUGSTOREID_VCHR;
                    m_objMainInstorageVo.m_strSTORAGEEXAMID_CHR = objOutMainVO.m_strEXAMID_CHR;

                    int intTypeCode = 0;
                    clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                    objPubSvc.m_lngGetTypeCodeByName( 0, "�������", out intTypeCode);
                    if (intTypeCode == 0)
                    {
                        objHRPServ.Dispose();
                        objHRPServ = null;
                        throw new Exception("������������ͣ��������");
                    }
                    m_objMainInstorageVo.m_strTYPECODE_VCHR = intTypeCode.ToString();

                    for (int iRow = 0; iRow < objOutDetailVO.Length; iRow++)
                    {
                        m_oInstoragebjDetailArr[iRow] = new clsDS_Instorage_Detail();
                        m_oInstoragebjDetailArr[iRow].m_dblPACKQTY_DEC = objOutDetailVO[iRow].m_dblPACKQTY_DEC;
                        m_oInstoragebjDetailArr[iRow].m_strMEDICINEID_CHR = objOutDetailVO[iRow].m_strMEDICINEID_CHR;
                        m_oInstoragebjDetailArr[iRow].m_strMEDICINENAME_VCHR = objOutDetailVO[iRow].m_strMEDICINENAME_VCHR;
                        m_oInstoragebjDetailArr[iRow].m_strMEDSPEC_VCHR = objOutDetailVO[iRow].m_strMEDSPEC_VCHR;
                        m_oInstoragebjDetailArr[iRow].m_dblOPAMOUNT_INT = objOutDetailVO[iRow].m_dblOPAMOUNT_INT;
                        m_oInstoragebjDetailArr[iRow].m_strOPUNIT_CHR = objOutDetailVO[iRow].m_strOPUNIT_CHR;
                        m_oInstoragebjDetailArr[iRow].m_dblIPAMOUNT_INT = objOutDetailVO[iRow].m_dblIPAMOUNT_INT;
                        m_oInstoragebjDetailArr[iRow].m_strIPUNIT_CHR = objOutDetailVO[iRow].m_strIPUNIT_CHR;
                        m_oInstoragebjDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT = objOutDetailVO[iRow].m_dblOPWHOLESALEPRICE_INT;
                        m_oInstoragebjDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT = objOutDetailVO[iRow].m_dblIPWHOLESALEPRICE_INT;
                        m_oInstoragebjDetailArr[iRow].m_dblOPRETAILPRICE_INT = objOutDetailVO[iRow].m_dblOPRETAILPRICE_INT;
                        m_oInstoragebjDetailArr[iRow].m_dblIPRETAILPRICE_INT = objOutDetailVO[iRow].m_dblIPRETAILPRICE_INT;
                        m_oInstoragebjDetailArr[iRow].m_strLOTNO_VCHR = objOutDetailVO[iRow].m_strLOTNO_VCHR;
                        m_oInstoragebjDetailArr[iRow].m_datVALIDPERIOD_DAT = objOutDetailVO[iRow].m_datVALIDPERIOD_DAT;
                        m_oInstoragebjDetailArr[iRow].m_intSTATUS = 1;
                        m_oInstoragebjDetailArr[iRow].m_strINSTOREID_VCHR = objOutDetailVO[iRow].m_strInStorageid;//ҩ����ⵥ��
                        m_oInstoragebjDetailArr[iRow].m_dtmINSTORAGEDATE_DAT = objOutDetailVO[iRow].m_dtmINSTORAGEDATE_DAT.Date;//ҩ���������
                        m_oInstoragebjDetailArr[iRow].m_strPRODUCTORID_CHR = objOutDetailVO[iRow].m_strPRODUCTORID_CHR;
                    }
                    lngRes = m_objInstorageSvc.m_lngAddNewInstorage( ref m_objMainInstorageVo, ref m_oInstoragebjDetailArr, 0, "");
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

        //��ȡ�˿����
        private long m_lngGetReturnCount(string p_strInBillNo, string p_strMedicineID, out int p_intReturnCount)
        {
            p_intReturnCount = 0;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = @"select count(a.seriesid_int)
	from t_ms_instorage_detal a
	left join t_ms_instorage b on b.seriesid_int = a.seriesid2_int
 where a.instorageid_vchr = ?
	 and a.medicineid_chr = ?
	 and a.instorageid_vchr <> b.instorageid_vchr
	 and a.status = 1
	 and b.state_int > 0";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(2, out m_objParaArr);
                m_objParaArr[0].Value = p_strInBillNo;
                m_objParaArr[1].Value = p_strMedicineID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, m_objParaArr);
                {
                    if (dtbResult != null && dtbResult.Rows.Count > 0)
                    {
                        p_intReturnCount = Convert.ToInt32(dtbResult.Rows[0][0]);
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

        //��ȡҩ��������Ϣ��Ӧ��ҩ����ⵥ��
        private long m_lngGetInStorageID(long p_lngSeq, out string p_strInBillNo, out string p_strOutBillNo)
        {
            p_strInBillNo = string.Empty;
            p_strOutBillNo = string.Empty;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct b.instoreid_vchr, b.dsinstoreid_vchr, c.outstorageid_vchr
	from t_ds_outstorage_detail a
	left join t_ds_storage_detail b on b.seriesid_int = a.storageseriesid_chr
	left join t_ds_instorage c on c.indrugstoreid_vchr = b.dsinstoreid_vchr
 where a.storageseriesid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out m_objParaArr);
                m_objParaArr[0].Value = p_lngSeq;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, m_objParaArr);
                {
                    if (dtbResult != null && dtbResult.Rows.Count > 0)
                    {
                        p_strInBillNo = Convert.ToString(dtbResult.Rows[0][0]);
                        p_strOutBillNo = Convert.ToString(dtbResult.Rows[0][2]);
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

        #region ��������ϸ
        /// <summary>
        /// ��������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetailArr">�����ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddInStorageDetail( ref clsMS_InStorageDetail_VO[] p_objDetailArr)
        {
            if (p_objDetailArr == null || p_objDetailArr.Length == 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_ms_instorage_detal
                                        (seriesid_int, seriesid2_int, medicineid_chr, medicinename_vch,
                                         medspec_vchr, packamount, packunit_vchr, packcallprice_int,
                                         packconvert_int, lotno_vchr, amount, callprice_int,
                                         wholesaleprice_int, retailprice_int, validperiod_dat,
                                         acceptance_int, approvecode_vchr, apparentquality_int,
                                         packquality_int, examrusult_int, examiner, productorid_chr,
                                         accountperiod_int, acceptancecompany_chr, unit_vchr, status,
                                         instorageid_vchr, ruturnnum_int, outstorageid_vchr,
                                         grossprofitrate_int, limitunitprice_mny, invoicecode_vchr,
                                         invoicedater_dat, gmpflag_int, trademark_vchr )
                                 values (?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ? ) ";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();
                long lngEff = -1;
                //long[] lngSEQArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    IDataParameter[] objLisAddItemRefArr = null;
                    //lngRes = objPublic.m_lngGetSequenceArr( "SEQ_MS_INSTORAGEDETAIL", p_objDetailArr.Length, out lngSEQArr);
                    //if (lngSEQArr == null || lngSEQArr.Length == 0)
                    //{
                    //    return -1;
                    //}

                    for (int iRow = 0; iRow < p_objDetailArr.Length; iRow++)
                    {
                        objHRPServ.CreateDatabaseParameter(34, out objLisAddItemRefArr);
                        p_objDetailArr[iRow].m_lngSERIESID_INT = objPublic.GetSeqNextVal("SEQ_MS_INSTORAGEDETAIL"); // lngSEQArr[iRow];
                        objLisAddItemRefArr[0].Value = p_objDetailArr[iRow].m_lngSERIESID_INT; // lngSEQArr[iRow];
                        objLisAddItemRefArr[1].Value = p_objDetailArr[iRow].m_lngSERIESID_INT2;
                        objLisAddItemRefArr[2].Value = p_objDetailArr[iRow].m_strMEDICINEID_CHR;
                        objLisAddItemRefArr[3].Value = p_objDetailArr[iRow].m_strMEDICINENAME_VCH;
                        objLisAddItemRefArr[4].Value = p_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                        objLisAddItemRefArr[5].Value = p_objDetailArr[iRow].m_dblPACKAMOUNT.ToString("F2");
                        objLisAddItemRefArr[6].Value = p_objDetailArr[iRow].m_strPACKUNIT_VCHR;
                        objLisAddItemRefArr[7].Value = p_objDetailArr[iRow].m_dcmPACKCALLPRICE_INT.ToString("F4");
                        objLisAddItemRefArr[8].Value = p_objDetailArr[iRow].m_dblPACKCONVERT_INT.ToString("F2");
                        objLisAddItemRefArr[9].Value = p_objDetailArr[iRow].m_strLOTNO_VCHR;
                        objLisAddItemRefArr[10].Value = p_objDetailArr[iRow].m_dblAMOUNT;
                        objLisAddItemRefArr[11].Value = p_objDetailArr[iRow].m_dcmCALLPRICE_INT;
                        objLisAddItemRefArr[12].Value = p_objDetailArr[iRow].m_dcmWHOLESALEPRICE_INT;
                        objLisAddItemRefArr[13].Value = p_objDetailArr[iRow].m_dcmRETAILPRICE_INT;
                        objLisAddItemRefArr[14].DbType = DbType.DateTime;
                        objLisAddItemRefArr[14].Value = p_objDetailArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objLisAddItemRefArr[15].Value = p_objDetailArr[iRow].m_intACCEPTANCE_INT;
                        objLisAddItemRefArr[16].Value = p_objDetailArr[iRow].m_strAPPROVECODE_VCHR;
                        objLisAddItemRefArr[17].Value = p_objDetailArr[iRow].m_intAPPARENTQUALITY_INT;
                        objLisAddItemRefArr[18].Value = p_objDetailArr[iRow].m_intPACKQUALITY_INT;
                        objLisAddItemRefArr[19].Value = p_objDetailArr[iRow].m_intEXAMRUSULT_INT;
                        objLisAddItemRefArr[20].Value = p_objDetailArr[iRow].m_strEXAMINER;
                        objLisAddItemRefArr[21].Value = p_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                        objLisAddItemRefArr[22].Value = p_objDetailArr[iRow].m_lngACCOUNTPERIOD_INT;
                        objLisAddItemRefArr[23].Value = p_objDetailArr[iRow].m_strACCEPTANCECOMPANY_CHR;
                        objLisAddItemRefArr[24].Value = p_objDetailArr[iRow].m_strUNIT_VCHR;
                        objLisAddItemRefArr[25].Value = p_objDetailArr[iRow].m_intStatus;
                        objLisAddItemRefArr[26].Value = p_objDetailArr[iRow].m_strInStorageID;
                        objLisAddItemRefArr[27].Value = p_objDetailArr[iRow].m_intRUTURNNUM_INT;
                        objLisAddItemRefArr[28].Value = p_objDetailArr[iRow].m_strOUTSTORAGEID_VCHR;
                        objLisAddItemRefArr[29].Value = p_objDetailArr[iRow].m_dblGrossProfitRate;
                        objLisAddItemRefArr[30].Value = p_objDetailArr[iRow].m_strInvoicecode_vchr;
                        objLisAddItemRefArr[31].DbType = DbType.DateTime;
                        objLisAddItemRefArr[31].Value = p_objDetailArr[iRow].m_dtmInvoicedater_dat;
                        objLisAddItemRefArr[32].Value = p_objDetailArr[iRow].m_intGMPFlag;
                        objLisAddItemRefArr[33].Value = p_objDetailArr[iRow].m_strTrade;
                        //�������Ӽ�¼

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objLisAddItemRefArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64,DbType.String,DbType.String,DbType.String,DbType.Double,DbType.String,
                        DbType.Decimal,DbType.Double,DbType.String,DbType.Double,DbType.Decimal,DbType.Decimal,DbType.Decimal,DbType.DateTime,
                        DbType.Int32,DbType.String,DbType.Int32,DbType.Int32,DbType.Int32,DbType.String,DbType.String,DbType.Int64,DbType.String,DbType.String,
                        DbType.Int32,DbType.String,DbType.Int32,DbType.String,DbType.Double,DbType.Double,DbType.String,DbType.DateTime,
                        DbType.Int32,DbType.String};

                    object[][] objValues = new object[35][];

                    int intItemCount = p_objDetailArr.Length;
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[intItemCount];//��ʼ��

                    }

                    //lngRes = objPublic.m_lngGetSequenceArr( "SEQ_MS_INSTORAGEDETAIL", intItemCount, out lngSEQArr);
                    //if (lngSEQArr == null || lngSEQArr.Length == 0)
                    //{
                    //    return -1;
                    //}

                    for (int iRow = 0; iRow < intItemCount; iRow++)
                    {
                        p_objDetailArr[iRow].m_lngSERIESID_INT = objPublic.GetSeqNextVal("SEQ_MS_INSTORAGEDETAIL"); // lngSEQArr[iRow];
                        objValues[0][iRow] = p_objDetailArr[iRow].m_lngSERIESID_INT; // lngSEQArr[iRow];
                        objValues[1][iRow] = p_objDetailArr[iRow].m_lngSERIESID_INT2;
                        objValues[2][iRow] = p_objDetailArr[iRow].m_strMEDICINEID_CHR;
                        objValues[3][iRow] = p_objDetailArr[iRow].m_strMEDICINENAME_VCH;
                        objValues[4][iRow] = p_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                        objValues[5][iRow] = p_objDetailArr[iRow].m_dblPACKAMOUNT.ToString("F2"); ;
                        objValues[6][iRow] = p_objDetailArr[iRow].m_strPACKUNIT_VCHR;
                        objValues[7][iRow] = p_objDetailArr[iRow].m_dcmPACKCALLPRICE_INT.ToString("F4");
                        objValues[8][iRow] = p_objDetailArr[iRow].m_dblPACKCONVERT_INT.ToString("F2");
                        objValues[9][iRow] = p_objDetailArr[iRow].m_strLOTNO_VCHR;
                        objValues[10][iRow] = p_objDetailArr[iRow].m_dblAMOUNT;
                        objValues[11][iRow] = p_objDetailArr[iRow].m_dcmCALLPRICE_INT;
                        objValues[12][iRow] = p_objDetailArr[iRow].m_dcmWHOLESALEPRICE_INT;
                        objValues[13][iRow] = p_objDetailArr[iRow].m_dcmRETAILPRICE_INT;
                        objValues[14][iRow] = p_objDetailArr[iRow].m_dtmVALIDPERIOD_DAT;
                        objValues[15][iRow] = p_objDetailArr[iRow].m_intACCEPTANCE_INT;
                        objValues[16][iRow] = p_objDetailArr[iRow].m_strAPPROVECODE_VCHR;
                        objValues[17][iRow] = p_objDetailArr[iRow].m_intAPPARENTQUALITY_INT;
                        objValues[18][iRow] = p_objDetailArr[iRow].m_intPACKQUALITY_INT;
                        objValues[19][iRow] = p_objDetailArr[iRow].m_intEXAMRUSULT_INT;
                        objValues[20][iRow] = p_objDetailArr[iRow].m_strEXAMINER;
                        objValues[21][iRow] = p_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                        objValues[22][iRow] = p_objDetailArr[iRow].m_lngACCOUNTPERIOD_INT;
                        objValues[23][iRow] = p_objDetailArr[iRow].m_strACCEPTANCECOMPANY_CHR;
                        objValues[24][iRow] = p_objDetailArr[iRow].m_strUNIT_VCHR;
                        objValues[25][iRow] = p_objDetailArr[iRow].m_intStatus;
                        objValues[26][iRow] = p_objDetailArr[iRow].m_strInStorageID;
                        objValues[27][iRow] = p_objDetailArr[iRow].m_intRUTURNNUM_INT;
                        objValues[28][iRow] = p_objDetailArr[iRow].m_strOUTSTORAGEID_VCHR;
                        objValues[29][iRow] = p_objDetailArr[iRow].m_dblGrossProfitRate;
                        objValues[30][iRow] = p_objDetailArr[iRow].m_dblLimitunitPrice;
                        objValues[31][iRow] = p_objDetailArr[iRow].m_strInvoicecode_vchr;
                        objValues[32][iRow] = p_objDetailArr[iRow].m_dtmInvoicedater_dat;
                        objValues[33][iRow] = p_objDetailArr[iRow].m_intGMPFlag;
                        objValues[34][iRow] = p_objDetailArr[iRow].m_strTrade;
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

        /// <summary>
        /// ���ò����Ƿ�ҩ��
        /// </summary>        
        /// <param name="p_strDeptId">����ID</param>        
        /// <param name="p_strStorageID">��Ӧ��ҩ��ID</param>
        [AutoComplete]
        public long m_lngCheckMedStore(string p_strDeptId, out string p_strStorageID)
        {
            p_strStorageID = string.Empty;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeptId))
            {
                return lngRes;
            }
            try
            {
                string strSQL = @"select distinct a.medicineroomid
	from t_ms_medicinestoreroomset a
 where a.deptid_chr = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptId;
                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_strStorageID = dtbValue.Rows[0][0].ToString();
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

        #region ������˵�����
        /// <summary>
        /// ������˵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objISVO">�������</param>
        /// <param name="p_lngSEQ">���к�</param>
        /// <param name="p_strInStorageID">��ⵥ�ݺ�</param>
        /// <param name="p_intFlagIndex">��ʶλ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewInStorage( clsMS_InStorage_VO p_objISVO, out long p_lngSEQ, out string p_strInStorageID, int p_intFlagIndex)
        {
            p_lngSEQ = 1;
            p_strInStorageID = string.Empty;
            if (p_objISVO == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_ms_instorage
  (seriesid_int,
   instorageid_vchr,
   formtype_int,
   instoragetype_int,
   state_int,
   storageid_chr,
   vendorid_chr,
   buyerid_char,
   storagerid_char,
   accounterid_char,
   instoragedate_dat,
   neworder_dat,
   exam_dat,
   account_dat,
   supplycode_vchr,
   paystate_int,
   paydate_dat,
   commnet_vchr,
   makerid_chr,
   examerid_chr,
   inaccounterid_chr,
   returndept_chr,
   exportdept_chr,
   typecode_vchr,
   outtypecode_vchr,
   outdrugstoreid_vchr)
values
  (?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?,
   ?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                clsDS_Public_Supported_SVC objPublic = new clsDS_Public_Supported_SVC();
                IDataParameter[] objLisAddItemRefArr = null;
                long lngSEQ = 0;
                lngRes = objPublic.m_lngGetSequence( "SEQ_MS_INSTORAGE", out lngSEQ);
                if (lngSEQ <= 0)
                {
                    return -1;
                }

                string strType = "1";
                if (p_objISVO.m_intFORMTYPE_INT == 1 || p_objISVO.m_intFORMTYPE_INT == 3)
                {
                    strType = "1";
                }
                else if (p_objISVO.m_intFORMTYPE_INT == 2)
                {
                    strType = "4";
                }
                //else if (p_objISVO.m_intFORMTYPE_INT == 3)
                //{
                //    strType = "6";
                //}

                //string m_strTempId = string.Empty;
                //string m_strMedStoreShortCode = string.Empty;
                //string m_strParaValue = string.Empty;
                //lngRes = objPublic.m_lngGetSysParaByID( "5002", out m_strParaValue);
                //lngRes = objPublic.m_lngGetStorageShortCode( p_objISVO.m_strSTORAGEID_CHR, out m_strMedStoreShortCode);
                //lngRes = objPublic.m_lngGetNewIdByName( "t_ms_instorage", "instorageid_vchr", m_strMedStoreShortCode,m_strParaValue.Split(';')[p_intFlagIndex], p_objISVO.m_dtmNEWORDER_DAT, ref m_strTempId);
                //p_strInStorageID = m_strMedStoreShortCode + p_objISVO.m_dtmNEWORDER_DAT.ToString("yyMMdd") + m_strParaValue.Split(';')[p_intFlagIndex] + m_strTempId;
                lngRes = m_lngGetLatestInStorageID(strType, out p_strInStorageID);
                if (lngRes < 0 || string.IsNullOrEmpty(p_strInStorageID))
                {
                    return -1;
                }

                p_lngSEQ = lngSEQ;

                objHRPServ.CreateDatabaseParameter(26, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = lngSEQ;
                objLisAddItemRefArr[1].Value = p_strInStorageID;
                objLisAddItemRefArr[2].Value = p_objISVO.m_intFORMTYPE_INT;
                objLisAddItemRefArr[3].Value = p_objISVO.m_intINSTORAGETYPE_INT;
                objLisAddItemRefArr[4].Value = p_objISVO.m_intSTATE_INT;
                objLisAddItemRefArr[5].Value = p_objISVO.m_strSTORAGEID_CHR;
                objLisAddItemRefArr[6].Value = p_objISVO.m_strVENDORID_CHR;
                objLisAddItemRefArr[7].Value = p_objISVO.m_strBUYERID_CHAR;
                objLisAddItemRefArr[8].Value = p_objISVO.m_strSTORAGERID_CHAR;
                objLisAddItemRefArr[9].Value = p_objISVO.m_strACCOUNTERID_CHAR;
                objLisAddItemRefArr[10].DbType = DbType.DateTime;
                objLisAddItemRefArr[10].Value = p_objISVO.m_dtmINSTORAGEDATE_DAT;
                objLisAddItemRefArr[11].DbType = DbType.DateTime;
                objLisAddItemRefArr[11].Value = p_objISVO.m_dtmNEWORDER_DAT;
                objLisAddItemRefArr[12].DbType = DbType.DateTime;
                objLisAddItemRefArr[12].Value = p_objISVO.m_dtmEXAM_DAT;
                objLisAddItemRefArr[13].DbType = DbType.DateTime;
                objLisAddItemRefArr[13].Value = p_objISVO.m_dtmACCOUNT_DAT;
                objLisAddItemRefArr[14].Value = p_objISVO.m_strSUPPLYCODE_VCHR;
                objLisAddItemRefArr[15].Value = p_objISVO.m_intPAYSTATE_INT;
                objLisAddItemRefArr[16].DbType = DbType.DateTime;
                objLisAddItemRefArr[16].Value = p_objISVO.m_dtmPAYDATE_DAT;
                objLisAddItemRefArr[17].Value = p_objISVO.m_strCOMMNET_VCHR;
                objLisAddItemRefArr[18].Value = p_objISVO.m_strMAKERID_CHR;
                objLisAddItemRefArr[19].Value = p_objISVO.m_strEXAMERID_CHR;
                objLisAddItemRefArr[20].Value = p_objISVO.m_strINACCOUNTERID_CHR;
                objLisAddItemRefArr[21].Value = p_objISVO.m_strRETURNDEPT_CHR;
                objLisAddItemRefArr[22].Value = p_objISVO.m_strExportDept_CHR;
                objLisAddItemRefArr[23].Value = p_objISVO.m_strTYPECODE_VCHR;
                objLisAddItemRefArr[24].Value = p_objISVO.m_strOutTypeCode_vchr;
                objLisAddItemRefArr[25].Value = p_objISVO.m_strOUTDRUGSTOREID_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���µ���ⵥ�ݺ�
        /// <summary>
        /// ���µ���ⵥ�ݺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strType">������</param>
        /// <param name="p_strID">���ص��ݺ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLatestInStorageID( string p_strType, out string p_strID)
        {
            p_strID = string.Empty;

            long lngRes = -1;
            try
            {
                string strSQL = @"select max(t.instorageid_vchr)
  from t_ms_instorage t
 where t.instorageid_vchr like ?";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = DateTime.Now.ToString("yyyyMMdd") + p_strType + "%";

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (dtbValue == null || dtbValue.Rows.Count == 0)
                {
                    p_strID = DateTime.Now.ToString("yyyyMMdd") + p_strType + "0001";
                }
                else
                {
                    string strTemp = dtbValue.Rows[0][0].ToString();
                    if (string.IsNullOrEmpty(strTemp))
                    {
                        p_strID = DateTime.Now.ToString("yyyyMMdd") + p_strType + "0001";
                    }
                    else
                    {
                        strTemp = strTemp.Substring(9, 4);
                        p_strID = DateTime.Now.ToString("yyyyMMdd") + p_strType + (Convert.ToInt32(strTemp) + 1).ToString("0000");
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

        #region �޸ĳ��ⵥ��FormType���������͡��������š���ע��
        /// <summary>
        /// �޸ĳ��ⵥ��FormType���������͡��������š���ע��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBillNo">���ⵥ��</param>
        /// <param name="p_intFormType_int">FormType</param>
        /// <param name="p_strTypeCode">��������</param>
        /// <param name="p_strDeptCode">��������</param>
        /// <param name="p_strComment">��ע</param>
        /// <param name="p_blnHasGenerateInBill">�Ƿ���������ⵥ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInfo( string p_strBillNo, int p_intFormType_int, string p_strTypeCode, string p_strDeptCode,string p_strComment, bool p_blnHasGenerateInBill)
        {
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_ds_outstorage a
	 set a.formtype_int = ?, a.typecode_vchr = ?, a.instoredept_chr = ?,comment_vchr = ?
 where a.outdrugstoreid_vchr = ?";
            objHRPServ.CreateDatabaseParameter(5, out objValues);

            objValues[0].Value = p_intFormType_int;
            objValues[1].Value = p_strTypeCode;
            objValues[2].Value = p_strDeptCode;
            objValues[3].Value = p_strComment;
            objValues[4].Value = p_strBillNo;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngAffected > 0)
                {
                    strSQL = @"update t_ds_account_detail a
	 set a.deptid_chr = ?, a.formtype_int = ?
 where a.chittyid_vchr = ?";
                    objValues = null;
                    objHRPServ.CreateDatabaseParameter(3, out objValues);
                    objValues[0].Value = p_strDeptCode;
                    objValues[1].Value = p_intFormType_int;
                    objValues[2].Value = p_strBillNo;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                }
                if (lngRes > 0 && p_intFormType_int == 2 && p_blnHasGenerateInBill == false)
                {
                    long lngSeq = 0;
                    m_lngGetOutStorageBillSeq(p_strBillNo, out lngSeq);
                    m_lngAddInstorage( lngSeq);
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

        private long m_lngGetOutStorageBillSeq(string p_strBillNo, out long p_lngSeq)
        {
            p_lngSeq = 0;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL =
       @"select a.seriesid_int	from t_ds_outstorage a where a.outdrugstoreid_vchr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out m_objParaArr);
                m_objParaArr[0].Value = p_strBillNo;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, m_objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_lngSeq = Convert.ToInt64(dtbResult.Rows[0][0]);
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

        [AutoComplete]
        public long m_lngUpdateInStorageIDToOutDrugStorage(long p_lngSEQID,string p_strInStorageID)
        {  
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_outstorage a set a.instorageid_vchr = ? where a.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(2, out m_objParaArr);
                m_objParaArr[0].Value = p_strInStorageID;
                m_objParaArr[1].DbType = DbType.Int64;
                m_objParaArr[1].Value = p_lngSEQID;
                long lngAffect=-1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, m_objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                //lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, m_objParaArr); 
            }
            catch(Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }


        #region �޸���ⵥ��FormType��������͡���Դ���š���ע
        /// <summary>
        /// �޸���ⵥ��FormType��������͡���Դ���š���ע
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">״̬��1Ϊ��ˡ�2Ϊ����</param>
        /// <param name="p_strBillNo">��ⵥ��</param>
        /// <param name="p_intFormType_int">FormType</param>
        /// <param name="p_strTypeCode">�������</param>
        /// <param name="p_strDeptCode">��Դ����</param>
        /// <param name="p_strComment">��ע</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateTypeAndDept( int p_intStatus, string p_strBillNo, int p_intFormType_int, string p_strTypeCode, string p_strDeptCode, string p_strComment)
        {
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update  t_ds_outstorage  a
                           set a.formtype_int = ?, a.typecode_vchr = ?, a.instoredept_chr = ?,
                               a.comment_vchr = ?
                         where a.drugstoreid_chr = ?";
            objHRPServ.CreateDatabaseParameter(5, out objValues);

            objValues[0].Value = p_intFormType_int;
            objValues[1].Value = p_strTypeCode;
            objValues[2].Value = p_strDeptCode;
            objValues[3].Value = p_strComment;
            objValues[4].Value = p_strBillNo;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);

                if (lngAffected != 1)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    ContextUtil.SetAbort();
                    return -99;
                }

                if (lngAffected > 0 && p_intStatus == 1)
                {
                    strSQL = @"update t_ds_account_detail a
	 set a.deptid_chr = ?, a.formtype_int = ?
 where a.chittyid_vchr = ?";
                    objValues = null;
                    objHRPServ.CreateDatabaseParameter(3, out objValues);
                    objValues[0].Value = p_strDeptCode;
                    objValues[1].Value = p_intFormType_int;
                    objValues[2].Value = p_strBillNo;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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

        #region 2009-10-24 ����ΰ���:ҩ�������޸�����ҵ��
        /// <summary>
        ///  ҩ�������޸�����ҵ��
        ///  //1.���ݵ��ݺŲ�ѯ��������
        ///  //2.���ݳ��ⵥ�еĿ��ID�ӻؿ��
        ///  //3.ɾ��������ϸ���е�����
        ///  //4.���������µĳ�����ϸ
        ///  //5.���ݳ��ⵥ�еĿ��ID�ۼ������
        /// </summary>
        /// <param name="p_strReceipt">���ݺ�</param>
        /// <param name="m_objDetailArr">������ϸVO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOutStoreAndStore(string p_strReceipt, ref clsDS_Outstorage_Detail[] m_objDetailArr)
        {
            long lngRes = 0;
            long lngRff = 0;
            DataTable dtResult = new DataTable();
            try {
                //1.��ѯҩƷ��������(������λ����С��λ)
                string strSQL1 = @"select b.seriesid2_int,b.storageseriesid_chr,b.ipamount_int,b.opamount_int
                          from t_ds_outstorage_detail b
                         where b.seriesid2_int =
                               (select a.seriesid_int
                                  from t_ds_outstorage a
                                 where a.outdrugstoreid_vchr = ?)";
                clsHRPTableService clsHrpSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                clsHrpSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strReceipt;
                lngRes = clsHrpSvc.lngGetDataTableWithParameters(strSQL1, ref dtResult, objDPArr);
                objDPArr = null;
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    lngRes = 0;
                    //2.���ݳ��ⵥ�Ķ�Ӧ���ID�ӻؿ��
                    string strSQL2 = @"update t_ds_storage_detail a
                                       set a.ipavailablegross_num = a.ipavailablegross_num + (?),
                                           a.opavailablegross_num = a.opavailablegross_num + (?)
                                     where a.seriesid_int = ?";
                    int intfor = 0;
                    foreach(DataRow dtr in dtResult.Rows)
                    {
                        clsHrpSvc.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = Convert.ToDouble(dtr["ipamount_int"]);
                        objDPArr[1].Value = Convert.ToDouble(dtr["opamount_int"]);
                        objDPArr[2].Value = Convert.ToInt32(dtr["storageseriesid_chr"]);
                        lngRes =  clsHrpSvc.lngExecuteParameterSQL(strSQL2, ref lngRff, objDPArr);
                        if (lngRff > 0)
                        {
                            intfor++;//�Դ����ж��Ƿ�ѭ�����ӻص�����Ƿ���ȷ
                        }
                    }
                    objDPArr = null;
                    if (lngRes > 0 && intfor == dtResult.Rows.Count)
                    {
                        lngRes = 0;
                        intfor = 0;
                        //3.ɾ���ɵĳ�����ϸ��Ϣ
                        string strSQL3 = @"delete from t_ds_outstorage_detail a
                                         where a.seriesid2_int =
                                               (select b.seriesid_int
                                                  from t_ds_outstorage b
                                                 where b.outdrugstoreid_vchr = ?)";
                        clsHrpSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = p_strReceipt;
                        lngRes = clsHrpSvc.lngExecuteParameterSQL(strSQL3, ref lngRff, objDPArr);
                        if (lngRes > 0 && lngRff > 0)
                        {
                            lngRes = 0;
                            //4.�����µĳ����¼
                            string strSQL4 = @"insert into t_ds_outstorage_detail
                                      (seriesid_int,
                                       seriesid2_int,
                                       medicineid_chr,
                                       medicinename_vchr,
                                       medspec_vchr,
                                       opamount_int,
                                       opunit_chr,
                                       ipamount_int,
                                       ipunit_chr,
                                       packqty_dec,
                                       opwholesaleprice_int,
                                       ipwholesaleprice_int,
                                       opretailprice_int,
                                       ipretailprice_int,
                                       lotno_vchr,
                                       validperiod_dat,
                                       status,
                                       rejectreason,
                                       storageseriesid_chr,
                                       productorid_chr)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ";
                            //long[] lngSEQArr = null; 
                            clsDS_UpdateStorageBySeriesID_VO[] objUpdateArr = null;
                            clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                            //lngRes = objPubSvc.m_lngGetSequenceArr( "SEQ_DS_OUTSTORAGE_DETAIL", m_objDetailArr.Length, out lngSEQArr);
                            DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.Int64, DbType.String, DbType.String, 
                            DbType.String, DbType.Double, DbType.String, DbType.Double, 
                            DbType.String, DbType.Double, DbType.Double, 
                            DbType.Double, DbType.Double, DbType.Double, 
                            DbType.String,DbType.DateTime, DbType.Int16,
                            DbType.String,DbType.String,DbType.String};
                            object[][] objValues = new object[20][];
                            int intItemCount = m_objDetailArr.Length;
                            objUpdateArr = new clsDS_UpdateStorageBySeriesID_VO[intItemCount];
                            for (int j = 0; j < objValues.Length; j++)
                            {
                                objValues[j] = new object[intItemCount];//��ʼ��

                            }

                            for (int iRow = 0; iRow < m_objDetailArr.Length; iRow++)
                            {
                                objUpdateArr[iRow] = new clsDS_UpdateStorageBySeriesID_VO();
                                m_objDetailArr[iRow].m_lngSERIESID_INT = objPubSvc.GetSeqNextVal("SEQ_DS_OUTSTORAGE_DETAIL");   // lngSEQArr[iRow];
                                objValues[0][iRow] = m_objDetailArr[iRow].m_lngSERIESID_INT;
                                m_objDetailArr[iRow].m_lngSERIESID2_INT = Convert.ToInt64(dtResult.Rows[0]["seriesid2_int"]);
                                objValues[1][iRow] = m_objDetailArr[iRow].m_lngSERIESID2_INT;
                                objValues[2][iRow] = m_objDetailArr[iRow].m_strMEDICINEID_CHR;
                                objValues[3][iRow] = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                                objValues[4][iRow] = m_objDetailArr[iRow].m_strMEDSPEC_VCHR;
                                objValues[5][iRow] = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;
                                objValues[6][iRow] = m_objDetailArr[iRow].m_strOPUNIT_CHR;
                                objValues[7][iRow] = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;
                                objValues[8][iRow] = m_objDetailArr[iRow].m_strIPUNIT_CHR;
                                objValues[9][iRow] = m_objDetailArr[iRow].m_dblPACKQTY_DEC;
                                objValues[10][iRow] = m_objDetailArr[iRow].m_dblOPWHOLESALEPRICE_INT;
                                objValues[11][iRow] = m_objDetailArr[iRow].m_dblIPWHOLESALEPRICE_INT;
                                objValues[12][iRow] = m_objDetailArr[iRow].m_dblOPRETAILPRICE_INT;
                                objValues[13][iRow] = m_objDetailArr[iRow].m_dblIPRETAILPRICE_INT;
                                objValues[14][iRow] = m_objDetailArr[iRow].m_strLOTNO_VCHR;
                                objValues[15][iRow] = m_objDetailArr[iRow].m_datVALIDPERIOD_DAT;
                                objValues[16][iRow] = m_objDetailArr[iRow].m_intSTATUS;
                                objValues[17][iRow] = m_objDetailArr[iRow].m_strRejectReason;
                                objValues[18][iRow] = m_objDetailArr[iRow].m_intSTORAGESERIESID_CHR;
                                objValues[19][iRow] = m_objDetailArr[iRow].m_strPRODUCTORID_CHR;
                                //��ֵ������VO
                                objUpdateArr[iRow].m_dblOPAvalid = m_objDetailArr[iRow].m_dblOPAMOUNT_INT;//���ÿ�棨������
                                objUpdateArr[iRow].m_dblIPAvalid = m_objDetailArr[iRow].m_dblIPAMOUNT_INT;//���ÿ�棨��С��
                                objUpdateArr[iRow].m_intSeriesID = Convert.ToInt64(m_objDetailArr[iRow].m_intSTORAGESERIESID_CHR);//���ID
                                //objUpdateArr[iRow].m_strMEDICINENAME_VCHR = m_objDetailArr[iRow].m_strMEDICINENAME_VCHR;
                            }
                            lngRes = clsHrpSvc.m_lngSaveArrayWithParameters(strSQL4, objValues, dbTypes);
                            lngRff = 0;
                            objDPArr = null;
                            intfor = 0;
                            //5.���ݳ��ⵥ�еĿ��ID�ۼ������
                            if (lngRes > 0)
                            {
                                lngRes = 0;
                                string strSQL5 = @"update t_ds_storage_detail a
                                       set a.ipavailablegross_num = a.ipavailablegross_num - (?),
                                           a.opavailablegross_num = a.opavailablegross_num - (?)
                                     where a.seriesid_int = ?";
                                
                                for (int i2 = 0; i2 < m_objDetailArr.Length; i2++)
                                {
                                    clsHrpSvc.CreateDatabaseParameter(3, out objDPArr);
                                    objDPArr[0].Value = objUpdateArr[i2].m_dblIPAvalid;//���ÿ�棨������
                                    objDPArr[1].Value = objUpdateArr[i2].m_dblOPAvalid;//���ÿ�棨��С��
                                    objDPArr[2].Value = objUpdateArr[i2].m_intSeriesID;//���ID
                                    lngRes = clsHrpSvc.lngExecuteParameterSQL(strSQL5, ref lngRff, objDPArr);
                                    if (lngRff > 0)
                                    {
                                        intfor++;
                                    }
                                }
                                if (intfor != m_objDetailArr.Length)
                                {
                                    clsHrpSvc.Dispose();
                                    clsHrpSvc = null;
                                    ContextUtil.SetAbort();//�ع�����
                                    throw new Exception("�ۼ�������");
                                }
                            }
                            else
                            {
                                clsHrpSvc.Dispose();
                                clsHrpSvc = null;
                                ContextUtil.SetAbort();//�ع�����
                            }
                        }
                        else {
                            clsHrpSvc.Dispose();
                            clsHrpSvc = null;
                            ContextUtil.SetAbort();//�ع�����
                        }
                    }
                }
                else {
                    return -1;
                }
            }
            catch (Exception objex)
            {
                com.digitalwave.Utility.clsLogText clsError = new com.digitalwave.Utility.clsLogText();
                bool blnRes = clsError.LogError(objex.ToString());
            }   
            return lngRes;
        }
        #endregion
    }
}
