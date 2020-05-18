using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ���ﴦ�����ͼ�¼Svc
    /// Create by kong 2004-07-16
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOPMedStoreSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// �޸Ŀ����ϸ����
        /// </summary>
        /// <param name="p_objDetail">�����ϸVO</param>
        /// <param name="intType">�޸����͡�1���ӿ��,2�������</param>
        /// <param name="p_lngSeriesID">����˳��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageDetailGross(clsDS_StorageDetail_VO p_objDetail, Int16 intType, long p_lngSeriesID, out string p_strExcp)
        {
            //�޸Ŀ����ϸ��
            p_strExcp = string.Empty;
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
a.iprealgross_int = a.iprealgross_int + ?,
a.opavailablegross_num = a.opavailablegross_num + ?,
a.ipavailablegross_num = a.ipavailablegross_num + ?
where (a.ipavailablegross_num + ?) >= 0
and a.seriesid_int=?";
                objHRPServ.CreateDatabaseParameter(6, out objValues);
                objValues[0].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[5].Value = p_lngSeriesID;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes > 0 && lngAffected < 1)
                {
                    //��ǰ�����Ų�����
                    p_strExcp = "��ǰҩƷ: " + p_objDetail.m_strMEDICINENAME_VCHR + " ����Ϊ: " + p_objDetail.m_strLOTNO_VCHR + " ����Ѿ�������\r\n���ܳ������뵽ҽ������վ�ؿ�������";
                    ContextUtil.SetAbort();
                    return -100;
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
        /// ����ҩ��id��ҩƷid�жϿ����ϸ���Ƿ��Ѵ��ڸ�ҩ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objStorageDetail">�����ϸ</param>
        /// <param name="m_objOutStorageDetail">��ȡ������ϸ</param>
        /// <param name="p_blnHasDetail">�Ƿ����</param>
        /// <param name="p_lngSeriesID">����ڣ��������к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMedExistInStorageDetail(clsDS_StorageDetail_VO m_objStorageDetail, ref clsDS_Outstorage_Detail m_objOutStorageDetail, out bool p_blnHasDetail, out long p_lngSeriesID)
        {
            p_lngSeriesID = 0;
            p_blnHasDetail = false;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.seriesid_int,a.medicineid_chr,a.medicinename_vchr,a.medspec_vchr,a.lotno_vchr,a.ipunit_chr,
a.opunit_chr,a.packqty_dec,a.ipretailprice_int,a.opretailprice_int,a.ipwholesaleprice_int,
a.opwholesaleprice_int,a.validperiod_dat,a.instoreid_vchr,a.drugstoreid_chr
from t_ds_storage_detail a where a.seriesid_int=? ";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = m_objStorageDetail.m_lngSERIESID_INT;
                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_blnHasDetail = true;
                    p_lngSeriesID = Convert.ToInt64(dtbValue.Rows[0]["seriesid_int"]);
                    m_objOutStorageDetail.m_datVALIDPERIOD_DAT = Convert.ToDateTime(dtbValue.Rows[0]["validperiod_dat"]);
                    m_objOutStorageDetail.m_dblIPAMOUNT_INT = m_objStorageDetail.m_dblIPREALGROSS_INT;
                    m_objOutStorageDetail.m_dblIPRETAILPRICE_INT = Convert.ToDouble(dtbValue.Rows[0]["ipretailprice_int"]);
                    m_objOutStorageDetail.m_dblIPWHOLESALEPRICE_INT = dtbValue.Rows[0]["ipwholesaleprice_int"] == System.DBNull.Value ? 0 : Convert.ToDouble(dtbValue.Rows[0]["ipwholesaleprice_int"]);
                    m_objOutStorageDetail.m_dblOPAMOUNT_INT = m_objStorageDetail.m_dblOPREALGROSS_INT;
                    m_objOutStorageDetail.m_dblOPRETAILPRICE_INT = Convert.ToDouble(dtbValue.Rows[0]["opretailprice_int"]);
                    m_objOutStorageDetail.m_dblOPWHOLESALEPRICE_INT = dtbValue.Rows[0]["opwholesaleprice_int"] == System.DBNull.Value ? 0 : Convert.ToDouble(dtbValue.Rows[0]["opwholesaleprice_int"]);
                    m_objOutStorageDetail.m_dblPACKQTY_DEC = Convert.ToDouble(dtbValue.Rows[0]["packqty_dec"]);
                    m_objOutStorageDetail.m_intSTATUS = 1;
                    m_objOutStorageDetail.m_strIPUNIT_CHR = dtbValue.Rows[0]["ipunit_chr"].ToString();
                    m_objOutStorageDetail.m_strLOTNO_VCHR = dtbValue.Rows[0]["lotno_vchr"].ToString();
                    m_objOutStorageDetail.m_strMEDICINEID_CHR = m_objStorageDetail.m_strMEDICINEID_CHR;
                    m_objOutStorageDetail.m_strMEDICINENAME_VCHR = dtbValue.Rows[0]["medicinename_vchr"].ToString();
                    m_objOutStorageDetail.m_strMEDSPEC_VCHR = dtbValue.Rows[0]["medspec_vchr"].ToString();
                    m_objOutStorageDetail.m_strOPUNIT_CHR = dtbValue.Rows[0]["opunit_chr"].ToString();
                    m_objOutStorageDetail.m_strInStorageid = dtbValue.Rows[0]["instoreid_vchr"].ToString();

                    //���س�����Ϣ��
                    m_objStorageDetail.m_strLOTNO_VCHR = m_objOutStorageDetail.m_strLOTNO_VCHR;
                    m_objStorageDetail.m_strMEDICINENAME_VCHR = m_objOutStorageDetail.m_strMEDICINENAME_VCHR;
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
        /// ���ҩ��������ˮ�ʱ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objStorageDetailVoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDSRecipeAccountInfo(clsDS_StorageDetail_VO[] m_objStorageDetailVoArr)
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
   operatorid_chr,
   ipavaigross_int,
   opavaigross_int)
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
         b.iprealgross_int,
         b.oprealgross_int,
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
         ?,
         b.ipavailablegross_num,
         b.opavailablegross_num
    from t_ds_storage_detail b, t_bse_medicine c
   where b.seriesid_int = ?
     and b.medicineid_chr = c.medicineid_chr(+)";
            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.String, DbType.String, DbType.Int64 };
            object[][] objValuesArr = new object[7][];
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
                objValuesArr[2][i] = m_objTempVo.m_intSubStorageType;
                objValuesArr[3][i] = m_objTempVo.m_strOperatorid;
                objValuesArr[4][i] = m_objTempVo.m_strOutPatientRecipeid;
                objValuesArr[5][i] = m_objTempVo.m_strOperatorid;
                objValuesArr[6][i] = m_objTempVo.m_lngSERIESID_INT;
            }
            try
            {
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValuesArr, dbTypes);
                if (lngRes <= 0)
                    throw new Exception();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }

        /// <summary>
        /// ҩ����ҩ,�޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intSubStorageType">�޸����͡�1���ӿ��,2�������</param>
        /// <param name="p_objDetail"></param>
        /// <param name="m_objOutStorageDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSubtractStorage(Int16 m_intSubStorageType, clsDS_StorageDetail_VO[] p_objDetail, ref clsDS_Outstorage_Detail[] m_objOutStorageDetail, out string p_strExcp)
        {
            p_strExcp = string.Empty;
            if (p_objDetail == null || p_objDetail.Length == 0)
            {
                return -1;
            }
            long lngRes = -1;
            bool p_blnHasDetail = false;
            long p_lngSeriesID;
            m_objOutStorageDetail = new clsDS_Outstorage_Detail[p_objDetail.Length];
            for (int intRow = 0; intRow < p_objDetail.Length; intRow++)
            {
                m_objOutStorageDetail[intRow] = new clsDS_Outstorage_Detail();
                //�жϵ�ǰҩƷ�Ƿ��Ѵ��ڿ��������
                m_lngCheckMedExistInStorageDetail(p_objDetail[intRow], ref m_objOutStorageDetail[intRow], out p_blnHasDetail, out p_lngSeriesID);
                if (p_blnHasDetail)
                {
                    //���¿����ϸ���¼
                    lngRes = m_lngModifyStorageDetailGross(p_objDetail[intRow], m_intSubStorageType, p_lngSeriesID, out p_strExcp);
                    //�޸Ŀ����������
                    //if (lngRes != -1)
                    //{
                    //    lngRes = m_lngModifyStorageGross(p_objDetail[intRow], m_intSubStorageType);
                    //}
                    if (lngRes > 0)
                    {
                        lngRes = m_lngModifyStorageGross(p_objDetail[intRow], m_intSubStorageType);
                    }
                    else
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }
                }
            }
            return lngRes;

        }

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
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ����ҩ����ҩ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objDetailList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRollBackReturnMedInfo(string m_strOperatorid, List<clsReutrnMedEntry> m_objDetailList, out string p_strExcp)
        {
            p_strExcp = string.Empty;
            long lngRes = -1;
            string strSQL;
            if (m_objDetailList == null || m_objDetailList.Count == 0)
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"delete from  t_opr_returnmed a
                                          where a.outpatrecipeid_chr=?";
            objValues = null;
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_objDetailList[0].m_strOUTPATRECIPEID_CHR;

            long lngAffected = -1;
            lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            if (lngAffected < 1 || lngRes < 1)
                throw new Exception();

            strSQL = @" delete from  t_opr_returnmed_entry b
                                     where b.outpatrecipeid_chr=?
                                   ";
            objValues = null;
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_objDetailList[0].m_strOUTPATRECIPEID_CHR;

            lngAffected = -1;
            lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            if (lngAffected < 1 || lngRes < 1)
                throw new Exception();


            clsDS_Outstorage_Detail[] m_objOutStorageDetailVoArr = null;
            clsDS_StorageDetail_VO[] m_objStorageDetailVoArr = new clsDS_StorageDetail_VO[m_objDetailList.Count];
            for (int m_intRow = 0; m_intRow < m_objDetailList.Count; m_intRow++)
            {
                m_objStorageDetailVoArr[m_intRow] = new clsDS_StorageDetail_VO();
                m_objStorageDetailVoArr[m_intRow].m_strMEDICINEID_CHR = m_objDetailList[m_intRow].m_strMEDICINEID_CHR;
                m_objStorageDetailVoArr[m_intRow].m_lngSERIESID_INT = Convert.ToInt64(m_objDetailList[m_intRow].m_strSerialno);
                m_objStorageDetailVoArr[m_intRow].m_strDRUGSTOREID_CHR = m_objDetailList[m_intRow].m_strDrugStoreid_chr;
                m_objStorageDetailVoArr[m_intRow].m_intSubStorageType = 2;
                m_objStorageDetailVoArr[m_intRow].m_strOutPatientRecipeid = m_objDetailList[m_intRow].m_strOUTPATRECIPEID_CHR;
                m_objStorageDetailVoArr[m_intRow].m_strOperatorid = m_strOperatorid;

                m_objStorageDetailVoArr[m_intRow].m_dblOPREALGROSS_INT = m_objDetailList[m_intRow].m_dblOPRETAMOUT_DEC;
                m_objStorageDetailVoArr[m_intRow].m_dblIPREALGROSS_INT = m_objDetailList[m_intRow].m_dblIPRETAMOUT_DEC;
            }

            //�޸�ҩ�����
            lngRes = this.m_lngSubtractStorage(2, m_objStorageDetailVoArr, ref m_objOutStorageDetailVoArr, out p_strExcp);
            if (lngRes <= 0)
            {
                ContextUtil.SetAbort();
                return -100;
            }
            else  //д�봦����ˮ�ʱ�
            {
                lngRes = this.m_lngAddDSRecipeAccountInfo(m_objStorageDetailVoArr);
            }
            if (lngRes <= 0)
            {
                throw new Exception();
            }

            return lngRes;

        }
        /// <summary>
        /// ���ݴ����Ż�ȡ����״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strOutPatientRecipeid"></param>
        /// <param name="m_intStatus"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeStatus(string m_strOutPatientRecipeid, out int m_intStatus)
        {
            long lngRes = 0;
            m_intStatus = 0;
            string strSQL = @"select a.pstauts_int from t_opr_outpatientrecipe a where a.outpatrecipeid_chr=?";

            try
            {

                HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtResult = new DataTable();
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strOutPatientRecipeid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    m_intStatus = Convert.ToInt16(dtResult.Rows[0][0]);
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
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public clsOPMedStoreSvc()
        {

        }
        #endregion

        #region ͨ������ȡ��ǰ���˶���
        /// <summary>
        /// ͨ������ȡ��ǰ���˶���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="windStatus">����״̬��Ϣ</param>
        /// <param name="strDate"></param></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="dtDuty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientListByWinID(clsStatusWindows_VO windStatus, string strDate, out DataTable p_dtbResult, DataTable dtDuty)
        {
            long lngRes = 0;
            p_dtbResult = null;
            strDate = DateTime.Parse(strDate).ToString("yyyy-MM-dd");
            string strStatus = string.Empty;
            string strTemp = string.Empty;
            if (windStatus.statusTone == 1)
            {

                strStatus = " and (a.PSTATUS_INT=1 or a.PSTATUS_INT=2 or a.PSTATUS_INT=-1)";
                strTemp = "a.WINDOWID_CHR";
            }
            else
            {
                strStatus = " and (a.PSTATUS_INT=2 or a.PSTATUS_INT=3) and  a.SENDWINDOWID_CHR = '" + windStatus.strWindowID + "'";
                strTemp = "a.SENDWINDOWID_CHR";
            }
            DateTime _serverDate = System.DateTime.Now;
            string strUnionSun = string.Empty;
            #region whether the patient list display in other window
            if (dtDuty.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtDuty.Rows.Count; i1++)
                {
                    if (dtDuty.Rows[i1]["WORKTIME_VCHR"] != System.DBNull.Value && dtDuty.Rows[i1]["WORKTIME_VCHR"].ToString() != "")
                    {
                        bool isBteen = false;//��־��ǰ��ʱ���Ƿ���ͨҩ�����ϰ�ʱ��
                        string _split = "|";
                        string[] objstr = dtDuty.Rows[i1]["WORKTIME_VCHR"].ToString().Split(_split.ToCharArray());
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
                                    isBteen = true;
                                    break;
                                }
                            }
                            if (isBteen == true)
                            {
                                break;
                            }
                        }
                        if (isBteen == false)
                        {
                            strUnionSun += @" union all 
         select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, a.autoprintyd_int, b.isproxyboilmed    
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient c,
       t_opr_patientregister d,
       t_bse_patientcard e,
       t_bse_patientpaytype f,
       (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
               c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
          from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                    from t_opr_outpatientrecipeinv
                   where recorddate_dat
                            between to_date ('" + strDate + @" 00:00:00',
                                             'yyyy-mm-dd hh24:mi:ss'
                                            )
                                and to_date ('" + strDate + @" 23:59:59',
                                             'yyyy-mm-dd hh24:mi:ss'
                                            )
                group by outpatrecipeid_chr) b,
               t_opr_outpatientrecipeinv c
         where b.seqid_chr = c.seqid_chr) j,
       t_opr_reciperelation h,
       t_bse_employee g,
       t_bse_employee k,
       t_bse_employee m,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and b.registerid_chr = d.registerid_chr(+)
   and b.patientid_chr = c.patientid_chr
   and b.deptmed_int <> 1
   and i.outpatrecipeid_chr = h.outpatrecipeid_chr
   and h.seqid = j.outpatrecipeid_chr
   and a.treatemp_chr = g.empid_chr(+)
   and a.sendemp_chr = m.empid_chr(+)
   and j.opremp_chr = k.empid_chr
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and " + strTemp + @" in (
          select windowid_chr
            from t_bse_medstorewin
           where medstoreid_chr = '" + dtDuty.Rows[i1]["DEPTID_VCHR"].ToString() + "') and a.createdate_chr = '" + strDate + "'and b.patientid_chr = e.patientid_chr(+) and b.paytypeid_chr = f.paytypeid_chr" + strStatus;
                        }
                    }
                }
            }
            #endregion
            if (windStatus.statusTone == 1)
            {
                string strSQL = @"select   a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel , a.autoprintyd_int, b.isproxyboilmed 
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and b.paytypeid_chr = f.paytypeid_chr
     and a.windowid_chr = ?
     and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1)
order by a.serno_chr desc";
                #region ����ҩ��ת����¥ҩ��
                // strSQL = strSQL.Append(strStatus + strUnionSun + @"ORDER BY a.serno_chr DESC");
                if (strUnionSun != string.Empty)
                {
                    strSQL = @"select  a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel, a.autoprintyd_int, b.isproxyboilmed   
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and b.paytypeid_chr = f.paytypeid_chr
     and a.windowid_chr = ?
     and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1) " + strUnionSun + @"
     order by serno_chr desc";
                }
                #endregion
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = strDate + " 00:00:00";
                    objLisAddItemRefArr[1].Value = strDate + " 23:59:59";
                    objLisAddItemRefArr[2].Value = strDate;
                    objLisAddItemRefArr[3].Value = windStatus.strWindowID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);

                    if (p_dtbResult != null && p_dtbResult.Rows.Count > 0)
                    {
                        string Sql = @"select a.parmvalue_vchr from t_bse_sysparm a where a.status_int = 1 and a.parmcode_chr = '9011'";
                        DataTable dt9011 = null;
                        objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dt9011);
                        if (dt9011 != null && dt9011.Rows.Count > 0 && Convert.ToInt32(dt9011.Rows[0]["parmvalue_vchr"].ToString()) == 1)
                        {
                            for (int i = p_dtbResult.Rows.Count - 1; i >= 0; i--)
                            {
                                if (p_dtbResult.Rows[i]["isproxyboilmed"] != DBNull.Value && Convert.ToInt32(p_dtbResult.Rows[i]["isproxyboilmed"].ToString()) > 0)
                                {
                                    p_dtbResult.Rows.RemoveAt(i);
                                }
                            }
                            p_dtbResult.AcceptChanges();
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
            }
            else if (windStatus.statusTone == 2)
            {
                string strSQL = @"select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel, a.autoprintyd_int, b.isproxyboilmed    
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and b.paytypeid_chr = f.paytypeid_chr
     and a.sendwindowid_chr = ?
     and (a.pstatus_int=2 or a.pstatus_int=3)
order by a.serno_chr desc";
                #region ����ҩ��ת����¥ҩ��
                // strSQL = strSQL.Append(strStatus + strUnionSun + @"ORDER BY a.serno_chr DESC");
                if (strUnionSun != string.Empty)
                {
                    strSQL = @"select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel, a.autoprintyd_int, b.isproxyboilmed    
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and b.paytypeid_chr = f.paytypeid_chr
     and a.sendwindowid_chr = ?
     and (a.pstatus_int=2 or a.pstatus_int=3) " + strUnionSun + @"
     order by serno_chr desc";
                }
                #endregion
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = strDate + " 00:00:00";
                    objLisAddItemRefArr[1].Value = strDate + " 23:59:59";
                    objLisAddItemRefArr[2].Value = strDate;
                    objLisAddItemRefArr[3].Value = windStatus.strWindowID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);

                    if (p_dtbResult != null && p_dtbResult.Rows.Count > 0)
                    {
                        string Sql = @"select a.parmvalue_vchr from t_bse_sysparm a where a.status_int = 1 and a.parmcode_chr = '9011'";
                        DataTable dt9011 = null;
                        objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dt9011);
                        if (dt9011 != null && dt9011.Rows.Count > 0 && Convert.ToInt32(dt9011.Rows[0]["parmvalue_vchr"].ToString()) == 1)
                        {
                            for (int i = p_dtbResult.Rows.Count - 1; i >= 0; i--)
                            {
                                if (p_dtbResult.Rows[i]["isproxyboilmed"] != DBNull.Value && Convert.ToInt32(p_dtbResult.Rows[i]["isproxyboilmed"].ToString()) > 0)
                                {
                                    p_dtbResult.Rows.RemoveAt(i);
                                }
                            }
                            p_dtbResult.AcceptChanges();
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
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ�����ƺ���㲡�˵���ҩ�б�
        /// <summary>
        /// ��ȡ�����ƺ���㲡�˵���ҩ�б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="windStatus"></param>
        /// <param name="strDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="dtDuty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTreatMetnFirstByWinID(clsStatusWindows_VO windStatus, string strDate, out DataTable p_dtbResult, DataTable dtDuty)
        {
            long lngRes = -1;
            p_dtbResult = new DataTable();
            string strSQL = @"select a.sid_int,
                                       a.serno_chr,
                                       a.sendwindowid_chr as sendwindowid,
                                       a.medstoreid_chr,
                                       a.autoprint_int,
                                       a.senddate_dat as givedate_dat,
                                       a.sendemp_chr as giveemp_chr,
                                       a.windowid_chr,
                                       a.returndate_dat,
                                       a.returnemp_chr,
                                       a.injectprint_int,
                                       a.pstatus_int,
                                       a.senddate_dat,
                                       a.sendemp_chr,
                                       a.treatdate_dat,
                                       a.treatemp_chr,
                                       a.called_int,
                                       a.currentcall_int,
                                       a.quit_int,
                                       i.outpatrecipeid_chr,
                                       b.type_int as recipetype_int,
                                       b.pstauts_int as breakpstatus,
                                       c.name_vchr,
                                       c.sex_chr,
                                       c.idcard_chr,
                                       c.birth_dat,
                                       c.patientid_chr,
                                       d.registerno_chr,
                                       d.registerdate_dat,
                                       e.patientcardid_chr,
                                       f.paytypename_vchr,
                                       f.paytypeid_chr,
                                       decode(f.internalflag_int, 0, '�Է�', 1, '����', 2, 'ҽ��') as internalname,       
                                       g.lastname_vchr,
                                       '' as opremp_chr,
                                       '' as checkname,
                                       m.lastname_vchr as sendname,
                                       p.homephone_vchr,
                                       r.typename_vchr,
                                       a.recalled_int, a.autoprintyd_int 
                                  from t_opr_recipesend       a,
                                       t_opr_recipesendentry  i,
                                       t_opr_outpatientrecipe b,
                                       t_bse_patient       c,
                                       t_opr_patientregister  d,
                                       t_bse_patientcard      e,
                                       t_bse_patientpaytype   f,
                                       t_opr_reciperelation   h,
                                       t_bse_employee         g,
                                       --   t_bse_employee k,
                                       t_bse_employee   m,
                                       t_bse_patient    p,
                                       t_aid_recipetype r
                                 where a.sid_int = i.sid_int
                                   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
                                   and b.registerid_chr = d.registerid_chr(+)
                                   and b.patientid_chr = c.patientid_chr
                                   and b.deptmed_int <> 1
                                   and b.patientid_chr = e.patientid_chr(+)
                                   and b.paytypeid_chr = f.paytypeid_chr
                                   and i.outpatrecipeid_chr = h.outpatrecipeid_chr
                                   and a.treatemp_chr = g.empid_chr(+)
                                   and a.sendemp_chr = m.empid_chr(+)
                                   and b.patientid_chr = p.patientid_chr
                                   and a.createdate_chr = ?
                                   and a.windowid_chr = ?
                                   and a.pstatus_int = 1
                                   and b.type_int = r.type_int(+)
                                   and b.type_int = r.type_int(+)
                                   and b.isgreen_int = 1";
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] objIDParr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out objIDParr);
                objIDParr[0].Value = strDate;
                objIDParr[1].Value = windStatus.strWindowID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objIDParr);
            }
            catch (Exception objEx)
            {
                clsLogText objLog = new clsLogText();
                objLog.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            return lngRes;
        }
        #endregion

        #region ͨ������ȡ��ǰ�����������ﴦ�����͵Ĳ��˶���
        /// <summary>
        /// ͨ������ȡ��ǰ��������������͵Ĳ��˶���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="windStatus">����״̬��Ϣ</param>
        /// <param name="strDate"></param></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="dtDuty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientListByWinIDForData(clsStatusWindows_VO windStatus, string strDate, out DataTable p_dtbResult, DataTable dtDuty)
        {
            long lngRes = 0;
            p_dtbResult = null;
            strDate = DateTime.Parse(strDate).ToString("yyyy-MM-dd");
            string strSQL = @"select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1)
   and a.windowid_chr = ?
   and b.type_int = 2
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1)
   and a.windowid_chr = ?
   and b.type_int = 3
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1)
   and a.windowid_chr = ?
   and b.type_int = 4
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = -1)
   and a.windowid_chr = ?
   and b.type_int = 5";
            try
            {
                if (windStatus.statusTone == 2)
                {
                    strSQL = @" select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 2 or a.pstatus_int = 3)
   and a.sendwindowid_chr = ?
   and b.type_int = 2
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 2 or a.pstatus_int = 3)
   and a.sendwindowid_chr = ?
   and b.type_int = 3
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 2 or a.pstatus_int = 3)
   and a.sendwindowid_chr = ?
   and b.type_int = 4
union all
select p.name_vchr as patientname, r.typename_vchr
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and b.deptmed_int <> 1
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and a.createdate_chr = ?
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and (a.pstatus_int = 2 or a.pstatus_int = 3)
   and a.sendwindowid_chr = ?
   and b.type_int = 5";

                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(8, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = strDate;
                objLisAddItemRefArr[1].Value = windStatus.strWindowID;
                objLisAddItemRefArr[2].Value = strDate;
                objLisAddItemRefArr[3].Value = windStatus.strWindowID;
                objLisAddItemRefArr[4].Value = strDate;
                objLisAddItemRefArr[5].Value = windStatus.strWindowID;
                objLisAddItemRefArr[6].Value = strDate;
                objLisAddItemRefArr[7].Value = windStatus.strWindowID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objLisAddItemRefArr);
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

        #region ������ҩ����ȡ��ǰ���˶���
        /// <summary>
        /// ������ҩ����ȡ��ǰ���˶���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="windStatus">����״̬��Ϣ</param>
        /// <param name="strDate"></param></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="dtDuty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientListNotByWinID(clsStatusWindows_VO windStatus, string strDate, out DataTable p_dtbResult, DataTable dtDuty)
        {
            long lngRes = 0;
            p_dtbResult = null;
            strDate = DateTime.Parse(strDate).ToString("yyyy-MM-dd");
            clsGetServerDate getServerDate = new clsGetServerDate();
            DateTime _serverDate = getServerDate.m_GetServerDate();
            string strUnionSun = string.Empty;
            #region whether display the patient list
            if (dtDuty.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtDuty.Rows.Count; i1++)
                {
                    if (dtDuty.Rows[i1]["WORKTIME_VCHR"] != System.DBNull.Value && dtDuty.Rows[i1]["WORKTIME_VCHR"].ToString() != "")
                    {
                        bool isBteen = false;//��־��ǰ��ʱ���Ƿ���ͨҩ�����ϰ�ʱ��
                        string _split = "|";
                        string[] objstr = dtDuty.Rows[i1]["WORKTIME_VCHR"].ToString().Split(_split.ToCharArray());
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
                                    isBteen = true;
                                    break;
                                }
                            }
                            if (isBteen == true)
                            {
                                break;
                            }
                        }
                        if (isBteen == false)
                        {
                            strUnionSun += @"union all 
         select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr, a.autoprintyd_int, b.isproxyboilmed   
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient c,
       t_opr_patientregister d,
       t_bse_patientcard e,
       t_bse_patientpaytype f,
       (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
               c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
          from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                    from t_opr_outpatientrecipeinv
                   where recorddate_dat
                            between to_date ('" + strDate + @" 00:00:00',
                                             'yyyy-mm-dd hh24:mi:ss'
                                            )
                                and to_date ('" + strDate + @" 23:59:59',
                                             'yyyy-mm-dd hh24:mi:ss'
                                            )
                group by outpatrecipeid_chr) b,
               t_opr_outpatientrecipeinv c
         where b.seqid_chr = c.seqid_chr) j,
       t_opr_reciperelation h,
       t_bse_employee g,
       t_bse_employee k,
       t_bse_employee m,
       t_bse_patient p,
       t_aid_recipetype r
 where a.sid_int = i.sid_int
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and b.registerid_chr = d.registerid_chr(+)
   and b.patientid_chr = c.patientid_chr
   and b.deptmed_int <> 1
   and i.outpatrecipeid_chr = h.outpatrecipeid_chr
   and h.seqid = j.outpatrecipeid_chr
   and a.treatemp_chr = g.empid_chr(+)
   and a.sendemp_chr = m.empid_chr(+)
   and j.opremp_chr = k.empid_chr
   and b.type_int = r.type_int(+)
   and (a.pstatus_int = 2 or a.pstatus_int = 3)
   and b.patientid_chr = p.patientid_chr
   and a.medstoreid_chr = '" + windStatus.strStorageID + @"'
   and a.sendwindowid_chr in (
          select windowid_chr
            from t_bse_medstorewin
           where medstoreid_chr =
                           '" + dtDuty.Rows[i1]["DEPTID_VCHR"].ToString() + @"')
   and a.createdate_chr = '" + strDate + @"'
   and b.patientid_chr = e.patientid_chr(+)
   and b.paytypeid_chr = f.paytypeid_chr ";
                        }

                    }

                }
            }
            #endregion

            string strSQL = @"select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int,
                                       a.quit_int, i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel , a.autoprintyd_int, b.isproxyboilmed   
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and a.medstoreid_chr = ?
     and b.paytypeid_chr = f.paytypeid_chr
     and (a.pstatus_int = 2 or a.pstatus_int = 3)
order by a.serno_chr desc";
            if (strUnionSun != string.Empty)
            {
                strSQL = @"select a.sid_int, a.serno_chr, a.sendwindowid_chr as sendwindowid,b.isgreen_int,
         a.medstoreid_chr, a.autoprint_int, a.senddate_dat as givedate_dat,
         a.sendemp_chr as giveemp_chr, a.windowid_chr, a.returndate_dat,
         a.returnemp_chr, a.injectprint_int, a.pstatus_int, a.senddate_dat,
         a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.called_int,a.currentcall_int ,
                                       a.quit_int,i.outpatrecipeid_chr,
         b.type_int as recipetype_int, b.pstauts_int as breakpstatus,
         c.name_vchr, c.sex_chr, c.idcard_chr, c.birth_dat, c.patientid_chr,
         d.registerno_chr, d.registerdate_dat, e.patientcardid_chr,
         f.paytypename_vchr,f.paytypeid_chr,
         decode (f.internalflag_int,
                 0, '�Է�',
                 1, '����',
                 2, 'ҽ��'
                ) as internalname,
         j.status_int, j.recorddate_dat, j.split_int, j.invoiceno_vchr,
         g.lastname_vchr, k.empno_chr as opremp_chr,
         k.lastname_vchr as checkname, m.lastname_vchr as sendname,
         p.homephone_vchr, r.typename_vchr,a.recalled_int, b.seculevel , a.autoprintyd_int, b.isproxyboilmed   
    from t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patient c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            from (select   max (seqid_chr) as seqid_chr, outpatrecipeid_chr
                      from t_opr_outpatientrecipeinv
                     where recorddate_dat
                              between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                  and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                  group by outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           where b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   where a.sid_int = i.sid_int
     and i.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.registerid_chr = d.registerid_chr(+)
     and b.patientid_chr = c.patientid_chr
     and b.deptmed_int <> 1
     and i.outpatrecipeid_chr = h.outpatrecipeid_chr
     and h.seqid = j.outpatrecipeid_chr
     and a.treatemp_chr = g.empid_chr(+)
     and a.sendemp_chr = m.empid_chr(+)
     and j.opremp_chr = k.empid_chr
     and b.type_int = r.type_int(+)
     and b.patientid_chr = p.patientid_chr
     and a.createdate_chr = ?
     and b.patientid_chr = e.patientid_chr(+)
     and a.medstoreid_chr = ?
     and b.paytypeid_chr = f.paytypeid_chr
     and (a.pstatus_int = 2 or a.pstatus_int = 3) " + strUnionSun + @"
order by serno_chr desc";

            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = strDate + " 00:00:00";
                objLisAddItemRefArr[1].Value = strDate + " 23:59:59";
                objLisAddItemRefArr[2].Value = strDate;
                objLisAddItemRefArr[3].Value = windStatus.strStorageID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbResult, objLisAddItemRefArr);
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
        #region �Ƿ��Զ���ӡ��ҩ��
        [AutoComplete]
        public long m_mthIsAutoPrint(out bool isAuto)
        {
            isAuto = false;
            long lngRes = 0;
            string strSQL = @"select SETSTATUS_INT from t_sys_setting where setid_chr ='0034'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["SETSTATUS_INT"].ToString().Trim() == "1")
                    {
                        isAuto = true;
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

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">1-��ҩ 2-��ҩ</param>
        /// <param name="p_strStorageID"> �ֿ�ID</param>
        /// <param name="p_strWinID"></param>
        /// <param name="p_strCardID"></param>
        /// <param name="p_strPatient"></param>
        /// <param name="p_strRegNo"></param>
        /// <param name="p_strRegDate"></param>
        /// <param name="p_endDate"></param>
        /// <param name="isShowReturn">�Ƿ���ʾ��Ʊ����<param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientList(int p_intStatus, string p_strStorageID,
            string p_strWinID, string p_strCardID, string p_strPatient, string p_strRegNo,
            string p_strRegDate, string p_endDate, bool isShowReturn, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strWhere = "";
            if (p_strCardID.Trim() != "")
                strWhere += " AND e.patientcardid_chr = '" + p_strCardID.Trim() + "' ";
            if (p_strPatient.Trim() != "")
                strWhere += " AND c.name_vchr like '" + p_strPatient.Trim() + "%'";
            if (p_strRegNo.Trim() != "")
                strWhere += " AND d.registerno_chr like '" + p_strRegNo.Trim() + "%'";
            strWhere += " and MEDSTOREID_CHR  in (select  MEDSTOREID_CHR from t_bse_medstore where MEDICNETYPE_INT=(select MEDICNETYPE_INT from t_bse_medstore where MEDSTOREID_CHR='" + p_strStorageID + "')) ";
            if (p_intStatus == 1)
            {
                strWhere += " and a.PSTATUS_INT in(-1,1,2) ";
            }
            if (p_intStatus == 2)
            {
                strWhere += " and (a.PSTATUS_INT=2 or a.PSTATUS_INT=3)";
            }
            string strStatus;
            if (isShowReturn == false)
                strStatus = @" and b.PSTAUTS_INT!=-2";
            else
                strStatus = @"";

            #region comment
            /*
            StringBuilder strSQL = new StringBuilder(@"SELECT a.WINDOWID_CHR,i.outpatrecipeid_chr, b.type_int as recipetype_int, a.pstatus_int,b.isgreen_int,
         a.senddate_dat, a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,a.orderno_int, a.called_int, a.currentcall_int,
         c.name_vchr, d.registerno_chr, d.registerdate_dat,b.PSTAUTS_INT,b.pstauts_int AS breakpstatus,
         e.patientcardid_chr, c.sex_chr,c.idcard_chr, r.typename_vchr, c.birth_dat,case when f.internalflag_int=0 then '�Է�' when f.internalflag_int=1 then '����'  when f.internalflag_int=2 then 'ҽ��' end as internalName,
         j.invoiceno_vchr,f.paytypename_vchr,f.paytypeid_chr,j.status_int,g.lastname_vchr,a.autoprint_int,a.medstoreid_chr,a.returndate_dat,p.homephone_vchr,c.patientid_chr,j.recorddate_dat,k.empno_chr as opremp_chr,k.lastname_vchr as checkname,
         m.lastname_vchr as sendname,j.split_int,a.sendwindowid_chr as sendwindowid,a.senddate_dat as givedate_dat,a.sid_int, a.serno_chr,a.injectprint_int,a.sendemp_chr as giveemp_chr,a.returnemp_chr,a.quit_int,a.recalled_int, b.seculevel 
    FROM t_opr_recipesend  a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patientidx c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
		 (SELECT c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int,c.SPLIT_INT
            FROM (SELECT   MAX (seqid_chr) AS seqid_chr, outpatrecipeid_chr
                      FROM t_opr_outpatientrecipeinv
                     WHERE recorddate_dat
                              BETWEEN TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                                  AND TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                  GROUP BY outpatrecipeid_chr ) b,
                 t_opr_outpatientrecipeinv c
           WHERE b.seqid_chr = c.seqid_chr)j,
		 t_opr_reciperelation h,
		 t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   WHERE a.sid_int=i.sid_int
     AND i.outpatrecipeid_chr = b.outpatrecipeid_chr
     AND b.registerid_chr = d.registerid_chr(+)
     AND b.patientid_chr = c.patientid_chr
	 and i.outpatrecipeid_chr =h.outpatrecipeid_chr
     AND h.seqid = j.outpatrecipeid_chr
     and c.patientid_chr = p.patientid_chr
	 and a.treatemp_chr=g.empid_chr(+)
     and j.opremp_chr=k.empid_chr
     and a.sendemp_chr=m.empid_chr(+)
     and b.type_int = r.type_int(+)
	 and j.totalsum_mny>=0
     and to_date(a.createdate_chr,'yyyy-mm-dd') between to_date(?,'yyyy-mm-dd') and to_date(?,'yyyy-mm-dd')
     and b.patientid_chr = e.patientid_chr(+)  and b.paytypeid_chr = f.PAYTYPEID_CHR ");
             
         strSQL = strSQL.Append(strStatus + " " + strWhere + " ORDER BY a.serno_chr DESC");
            */
            #endregion

            string strSQL = string.Empty;
            strSQL = @"SELECT a.WINDOWID_CHR,
                               i.outpatrecipeid_chr,
                               b.type_int as recipetype_int,
                               a.pstatus_int,
                               b.isgreen_int,
                               a.senddate_dat,
                               a.sendemp_chr,
                               a.treatdate_dat,
                               a.treatemp_chr,
                               a.orderno_int,
                               a.called_int,
                               a.currentcall_int,
                               c.name_vchr,
                               d.registerno_chr,
                               d.registerdate_dat,
                               b.PSTAUTS_INT,
                               b.pstauts_int AS breakpstatus,
                               e.patientcardid_chr,
                               c.sex_chr,
                               c.idcard_chr,
                               r.typename_vchr,
                               c.birth_dat,
                               case
                                 when f.internalflag_int = 0 then
                                  '�Է�'
                                 when f.internalflag_int = 1 then
                                  '����'
                                 when f.internalflag_int = 2 then
                                  'ҽ��'
                               end as internalName,
                               j.invoiceno_vchr,
                               f.paytypename_vchr,
                               f.paytypeid_chr,
                               j.status_int,
                               g.lastname_vchr,
                               a.autoprint_int,
                               a.medstoreid_chr,
                               a.returndate_dat,
                               p.homephone_vchr,
                               c.patientid_chr,
                               j.recorddate_dat,
                               k.empno_chr as opremp_chr,
                               k.lastname_vchr as checkname,
                               m.lastname_vchr as sendname,
                               j.split_int,
                               a.sendwindowid_chr as sendwindowid,
                               a.senddate_dat as givedate_dat,
                               a.sid_int,
                               a.serno_chr,
                               a.injectprint_int,
                               a.sendemp_chr as giveemp_chr,
                               a.returnemp_chr,
                               a.quit_int,
                               a.recalled_int,
                               b.seculevel, a.autoprintyd_int 
                          FROM t_opr_recipesend a,
                               t_opr_recipesendentry i,
                               t_opr_outpatientrecipe b,
                               t_bse_patient c,
                               t_opr_patientregister d,
                               t_bse_patientcard e,
                               t_bse_patientpaytype f,
                               (SELECT c.recorddate_dat,
                                       c.invoiceno_vchr,
                                       c.outpatrecipeid_chr,
                                       c.totalsum_mny,
                                       c.opremp_chr,
                                       c.status_int,
                                       c.SPLIT_INT
                                  FROM (SELECT MAX(seqid_chr) AS seqid_chr, outpatrecipeid_chr
                                          FROM t_opr_outpatientrecipeinv
                                         WHERE recorddate_dat BETWEEN
                                               TO_DATE(?, 'yyyy-mm-dd hh24:mi:ss') AND
                                               TO_DATE(?, 'yyyy-mm-dd hh24:mi:ss')
                                         GROUP BY outpatrecipeid_chr) b,
                                       t_opr_outpatientrecipeinv c
                                 WHERE b.seqid_chr = c.seqid_chr) j,
                               t_opr_reciperelation h,
                               t_bse_employee g,
                               t_bse_employee k,
                               t_bse_employee m,
                               t_bse_patient p,
                               t_aid_recipetype r
                         WHERE a.sid_int = i.sid_int
                           AND i.outpatrecipeid_chr = b.outpatrecipeid_chr
                           AND b.registerid_chr = d.registerid_chr(+)
                           AND b.patientid_chr = c.patientid_chr
                           and i.outpatrecipeid_chr = h.outpatrecipeid_chr
                           AND h.seqid = j.outpatrecipeid_chr
                           and c.patientid_chr = p.patientid_chr
                           and a.treatemp_chr = g.empid_chr(+)
                           and j.opremp_chr = k.empid_chr
                           and a.sendemp_chr = m.empid_chr(+)
                           and b.type_int = r.type_int(+)
                           and j.totalsum_mny >= 0
                           and to_date(a.createdate_chr, 'yyyy-mm-dd') between
                               to_date(?, 'yyyy-mm-dd') and to_date(?, 'yyyy-mm-dd')
                           and b.patientid_chr = e.patientid_chr(+)
                           and b.paytypeid_chr = f.PAYTYPEID_CHR 
                           ";

            strSQL += strStatus + " " + strWhere + " ORDER BY a.serno_chr DESC ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strRegDate + " 00:00:00";
                objLisAddItemRefArr[1].Value = p_endDate + " 23:59:59";
                objLisAddItemRefArr[2].Value = p_strRegDate;
                objLisAddItemRefArr[3].Value = p_endDate;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbResult, objLisAddItemRefArr);
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

        #region ��ҩ����
        /// <summary>
        ///��ҩ���� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="flnallyWindowsID">��ҩ����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDosage(clst_opr_nurseexecute[] p_objRecord, string flnallyWindowsID, string oldWinID, int m_intSID)
        {
            long lngRes = 0;

            if (p_objRecord.Length > 0)
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string strWindowID = "";
                int waiterNO = 0;
                clsWindowsCortrol windCortrol = new clsWindowsCortrol();
                windCortrol.m_lngGetGiveWindID(p_objRecord[0].m_strWindow, out strWindowID, out waiterNO);
                if (strWindowID == "")
                    throw (new System.Exception("��û��������Ӧ�ķ�ҩ���ڣ���ǰ���еķ�ҩ���ڶ����ڹ����У�"));
                //string strSQL = @"update t_opr_recipesend set PSTATUS_INT=2,SENDWINDOWID_CHR='" + strWindowID + "',  TREATDATE_DAT=to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss'),TREATEMP_CHR='" + p_objRecord[0].m_strOPERATORID_CHR + "' where OUTPATRECIPEID_CHR='" + p_objRecord[0].m_strOUTPATRECIPEID_CHR + "' and MEDSTOREID_CHR='" + oldWinID + "'";
                string strSQL = @"update t_opr_recipesend set PSTATUS_INT=2,SENDWINDOWID_CHR='" + strWindowID + "',  TREATDATE_DAT=to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss'),TREATEMP_CHR='" + p_objRecord[0].m_strOPERATORID_CHR + "' where sid_int=" + m_intSID + "";
                try
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                clsmedstorewinque p_objWind = new clsmedstorewinque();

                p_objWind.m_strMEDSTOREID_CHR = p_objRecord[0].m_strFrom;
                p_objWind.m_strOUTPATRECIPEID_CHR = p_objRecord[0].m_strOUTPATRECIPEID_CHR;
                p_objWind.m_strRECIPETYPE_CHR = p_objRecord[0].m_strOUTPATRECIPETYPE;

                //ɾ����ҩ����
                p_objWind.m_strWINDOWID_CHR = p_objRecord[0].m_strWindow;
                p_objWind.m_intWINDOWTYPE_INT = 1;
                windCortrol.m_lngDeleWinque(p_objWind);
                //д�뷢ҩ����

                p_objWind.m_strWINDOWID_CHR = strWindowID;
                p_objWind.m_intWaitNO = waiterNO;
                p_objWind.m_intWINDOWTYPE_INT = 0;
                windCortrol.m_lngAddNewWinque(p_objWind);

                for (int i1 = 0; i1 < p_objRecord.Length; i1++)
                {
                    lngRes = m_lngAddNewNurseexecute(p_objRecord[i1]);
                }

            }

            return lngRes;
        }

        #endregion

        #region �˴���
        /// <summary>
        /// �˴���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBreak(clst_opr_nurseexecute[] p_objRecord, int m_intSID)
        {
            long lngRes = 0;

            if (p_objRecord.Length > 0)
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                string strSQL = @"update t_opr_recipesend set PSTATUS_INT=-1,RETURNDATE_DAT=to_date(?,'yyyy-mm-dd hh24:mi:ss'),RETURNEMP_CHR=? where sid_int=? and MEDSTOREID_CHR=? and WINDOWID_CHR=?";
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                    paramArr[0].Value = DateTime.Now.ToString();
                    paramArr[1].Value = p_objRecord[0].m_strOPERATORID_CHR;
                    paramArr[2].Value = m_intSID;
                    paramArr[3].Value = p_objRecord[0].m_strFrom;
                    paramArr[4].Value = p_objRecord[0].m_strWindow;
                    long lngRecordsAffected = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                for (int i1 = 0; i1 < p_objRecord.Length; i1++)
                {
                    lngRes = m_lngAddNewNurseexecute(p_objRecord[i1]);
                }
            }
            return lngRes;
        }

        #endregion

        #region д��ִ�м�¼
        /// <summary>
        /// д��ִ�м�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewNurseexecute(clst_opr_nurseexecute p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            int p_strRecordID = 0;
            lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_nurseexecute", "SEQ_INT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_nurseexecute (SEQ_INT,BUSINESS_INT,TABLENAME_VCHR,OUTPATRECIPEID_CHR,ROWNO_CHR,ITEMID_CHR,EXECTIMES_INT,OPERATORTYPE_INT,OPERATORID_CHR,EXECTIME_DAT,SYSTIME_DAT,REMARK1_VCHR,REMARK2_VCHR,STATUS_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(14, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_intBUSINESS_INT;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strTABLENAME_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strOUTPATRECIPEID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strROWNO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intEXECTIMES_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intOPERATORTYPE_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[9].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[10].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strREMARK1_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strREMARK2_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intSTATUS_INT;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region �������еķ�Ʊ�ţ��ַ�Ʊ��
        /// <summary>
        /// �������еķ�Ʊ�ţ��ַ�Ʊ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strOutpatrecipeid"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_lngGetAllINVOICENO(string strOutpatrecipeid)
        {
            string strAllNO = "";
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "select INVOICENO_VCHR from t_opr_outpatientrecipeinv where OUTPATRECIPEID_CHR='" + strOutpatrecipeid + "' and STATUS_INT=1";
            DataTable dt = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                if (i1 == 0)
                {
                    strAllNO += dt.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
                }
                else
                {
                    strAllNO += "," + dt.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
                }
            }
            return strAllNO;
        }
        #endregion

        #region ����δ��ҩ����
        /// <summary>
        /// ����δ��ҩ����
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strWinID">���ں�</param>
        /// <param name="p_strCardID">���ƿ���</param>
        /// <param name="p_strPatient">��������</param>
        /// <param name="p_strRegNo">��ˮ��</param>
        /// <param name="p_strRegDate">�Һ�����</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatient(string p_strWinID, string p_strCardID, string p_strPatient, string p_strRegNo, string p_strRegDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strWhere = "";
            if (p_strCardID.Trim() != "")
                strWhere += " AND d.patientcardid_chr = '" + p_strCardID.Trim() + "' ";
            if (p_strPatient.Trim() != "")
                strWhere += " AND b.name_vchr like '" + p_strPatient.Trim() + "%' ";
            if (p_strRegNo.Trim() != "")
                strWhere += " AND c.registerno_chr like '" + p_strRegNo.Trim() + "%' ";
            if (p_strRegDate.Trim() != "2004-01-01")
                strWhere += " AND registerdate_dat = TO_DATE ('" + p_strRegDate.Trim() + "', 'yyyy-mm-dd') ";

            string strSQL = @"SELECT DISTINCT a.registerid_chr,a.OUTPATRECIPEID_CHR, a.patientid_chr, b.name_vchr,
										      c.registerno_chr, c.registerdate_dat
										 FROM t_opr_outpatientrecipe a,
										      t_bse_patientidx b,
											  t_opr_patientregister c,
											  t_bse_patientcard d
									    WHERE a.patientid_chr = b.patientid_chr
											  AND a.registerid_chr = c.registerid_chr
											  AND b.patientid_chr = d.patientid_chr
											  AND a.pstauts_int = 2 
											  AND a.outpatrecipeid_chr IN (
											                     SELECT h.outpatrecipeid_chr
																   FROM t_opr_recipesend g,t_opr_recipesendentry h
																  WHERE g.sid_int=h.sid_int and  g.pstatus_int = 1
																	    AND TRIM(g.windowid_chr) = '" + p_strWinID.Trim() + "') ";
            strSQL += strWhere;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ͨ������ȡ����ҩ���˶���
        /// <summary>
        /// ͨ������ȡ��ǰ���˶���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���ں�</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutOutPatientListByWinID(string p_strID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            string strSQL = @"SELECT DISTINCT a.registerid_chr,a.OUTPATRECIPEID_CHR, a.patientid_chr, b.name_vchr,
										      c.registerno_chr, c.registerdate_dat
										 FROM t_opr_outpatientrecipe a,
										      t_bse_patientidx b,
											  t_opr_patientregister c
									    WHERE a.patientid_chr = b.patientid_chr
											  AND a.registerid_chr = c.registerid_chr(+)
											  AND a.pstauts_int = 3
											  AND a.outpatrecipeid_chr IN (
											                       SELECT h.outpatrecipeid_chr
																   FROM t_opr_recipesend g,t_opr_recipesendentry h
																  WHERE g.sid_int=h.sid_int and  g.pstatus_int = 2
																	    AND TRIM(g.windowid_chr) = '" + p_strID.Trim() + "')";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ��ò��˴�����״̬��
        /// <summary>
        /// ��ò��˴�����״̬��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strRegID">���ﴦ����¼ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMainRecipe(string p_strRegID, out clsOutpatientRecipe_VO[] p_objResultArr, DateTime date1, DateTime date2, int intptatus, string strDepID)
        {
            long lngRes = 0;
            p_objResultArr = new clsOutpatientRecipe_VO[0];

            DataTable dtResult = new DataTable();
            string strWhere = "";
            if (intptatus == 3)
            {
                strWhere = @" and a.CREATEDATE_DAT BETWEEN to_date('" + date1.ToShortDateString() + " 00:00:00" + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + date2.ToShortDateString() + " 23:59:59" + "','yyyy-mm-dd hh24:mi:ss') and (a.PSTAUTS_INT!=2)";
                if (strDepID != "")
                    strWhere += @" and a.DIAGDEPT_CHR='" + strDepID + "'";
            }
            else
            {
                strWhere = @" and a.outpatrecipeid_chr='" + p_strRegID.Trim() + "'";
            }
            string strSQL = @"SELECT a.outpatrecipeid_chr,
       a.patientid_chr,
       a.createdate_dat,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.pstauts_int,
       a.recipeflag_int,
       a.outpatrecipeno_vchr,
       a.paytypeid_chr,
       a.casehisid_chr,
       a.groupid_chr,
       a.type_int,
       a.confirm_int,
       a.confirmdesc_vchr,
       a.createtype_int,
       a.deptmed_int,
       a.archtakeflag_int,
       a.printed_int,
       a.chargedeptid_chr, b.name_vchr, c.lastname_vchr, d.DEPTNAME_VCHR, h.idcard_chr, h.sex_chr, h.birth_dat,
								   e.lastname_vchr AS recordemp,f.DIAG_VCHR,h.HOMEADDRESS_VCHR,h.HOMEPHONE_VCHR,h.GOVCARD_CHR , h.DIFFICULTY_VCHR , h.INSURANCEID_VCHR,f.RECORDDATE_DAT,k.PAYTYPENAME_VCHR,
       p.diag_vchr, j.patientcardid_chr,(SELECT sum(totalsum_mny)
          FROM t_opr_outpatientrecipeinv
         WHERE outpatrecipeid_chr = '" + p_strRegID.Trim() + @"' and totalsum_mny>0) totailmoney
							  FROM t_opr_outpatientrecipe a,
								   t_bse_patientidx b,
								   t_bse_employee c,
								   T_BSE_DEPTDESC d,
								   t_bse_employee e,
								   t_opr_outpatientcasehis f,
								   t_bse_patient h,
								   t_bse_patientPaytype  k,
                                   T_BSE_PATIENTCARD j,
                                   T_OPR_OUTPATIENTCASEHIS p
							 WHERE a.patientid_chr = b.patientid_chr(+)
							   AND a.DIAGDR_CHR = c.EMPID_CHR(+)
							   AND a.DIAGDEPT_CHR = d.DEPTID_CHR(+)
							   AND a.recordemp_chr = e.EMPID_CHR(+)
							   and a.CASEHISID_CHR=f.CASEHISID_CHR(+)
							   and a.patientid_chr=h.patientid_chr(+)
								and a.PAYTYPEID_CHR=k.PAYTYPEID_CHR(+)
                                and a.patientid_chr=j.patientid_chr(+)
                                and a.CASEHISID_CHR=p.CASEHISID_CHR(+)
							";
            strSQL += strWhere;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);


                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsOutpatientRecipe_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsOutpatientRecipe_VO();


                        p_objResultArr[i1].CONFIRM_INT = int.Parse(dtResult.Rows[i1]["CONFIRM_INT"].ToString());
                        p_objResultArr[i1].strDIAG_VCHR = dtResult.Rows[i1]["DIAG_VCHR"].ToString().Trim();
                        p_objResultArr[i1].strHOMEADDRESS_VCHR = dtResult.Rows[i1]["HOMEADDRESS_VCHR"].ToString().Trim();


                        p_objResultArr[i1].HOMEPHONE_VCHR = dtResult.Rows[i1]["HOMEPHONE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].strRECORDDATE_DAT = dtResult.Rows[i1]["RECORDDATE_DAT"].ToString().Trim();
                        p_objResultArr[i1].strDIAG_VCHR = dtResult.Rows[i1]["DIAG_VCHR"].ToString().Trim();


                        p_objResultArr[i1].m_strOutpatRecipeID = dtResult.Rows[i1]["outpatrecipeid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strOutpatRecipeNo = dtResult.Rows[i1]["outpatrecipeno_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_strRecordDate = dtResult.Rows[i1]["recorddate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_strRegisterID = dtResult.Rows[i1]["registerid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateDate = dtResult.Rows[i1]["createdate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp = new clsEmployeeVO();
                        p_objResultArr[i1].m_objRecordEmp.strEmpID = dtResult.Rows[i1]["recordemp_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp.strLastName = dtResult.Rows[i1]["recordemp"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient = new clsPatientVO();
                        p_objResultArr[i1].m_objPatient.strPatientID = dtResult.Rows[i1]["patientid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.strPatientCardID = dtResult.Rows[i1]["PATIENTCARDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.m_strDIFFICULTY_VCHR = dtResult.Rows[i1]["DIFFICULTY_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.objPatType = new clsPatientType_VO();
                        p_objResultArr[i1].m_objPatient.objPatType.m_strPayTypeName = dtResult.Rows[i1]["PAYTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.m_strGOVCARD_CHR = dtResult.Rows[i1]["GOVCARD_CHR"].ToString().Trim();

                        p_objResultArr[i1].m_objPatient.strInsuranceID = dtResult.Rows[i1]["INSURANCEID_VCHR"].ToString().Trim();

                        p_objResultArr[i1].strIDcard = dtResult.Rows[i1]["idcard_chr"].ToString().Trim();
                        p_objResultArr[i1].strSex = dtResult.Rows[i1]["sex_chr"].ToString().Trim();
                        if (dtResult.Rows[i1]["birth_dat"] != DBNull.Value)
                        {
                            p_objResultArr[i1].dtmAge = Convert.ToDateTime(dtResult.Rows[i1]["birth_dat"]);
                        }

                        p_objResultArr[i1].m_objPatient.strName = dtResult.Rows[i1]["name_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr = new clsEmployeeVO();
                        p_objResultArr[i1].m_objDiagDr.strEmpID = dtResult.Rows[i1]["diagdr_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr.strLastName = dtResult.Rows[i1]["lastname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept = new clsDepartmentVO();
                        p_objResultArr[i1].m_objDiagDept.strDeptID = dtResult.Rows[i1]["diagdept_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept.strDeptName = dtResult.Rows[i1]["deptname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_intPStatus = dtResult.Rows[i1]["pstauts_int"].ToString().Trim();
                        p_objResultArr[i1].stroutpatrecipeMoney = dtResult.Rows[i1]["totailmoney"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion
        #region ��������id��ò��˴���
        /// <summary>
        /// ��������id��ò��˴���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strSid_int"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMainRecipe(string m_strSid_int, out clsOutpatientRecipe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOutpatientRecipe_VO[0];
            DataTable dtResult = new DataTable();
            string strSQL = @"SELECT distinct a.outpatrecipeid_chr,
       a.patientid_chr,
       a.createdate_dat,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.pstauts_int,
       a.recipeflag_int,
       a.outpatrecipeno_vchr,
       a.paytypeid_chr,
       a.casehisid_chr,
       a.groupid_chr,
       a.type_int,
       a.confirm_int,
       a.confirmdesc_vchr,
       a.createtype_int,
       a.deptmed_int,
       a.archtakeflag_int,
       a.printed_int, a.isproxyboilmed,    
       a.chargedeptid_chr, b.name_vchr, c.lastname_vchr, d.deptname_vchr,
       e.lastname_vchr AS recordemp, f.diag_vchr, h.homeaddress_vchr,
       h.homephone_vchr, h.govcard_chr, h.difficulty_vchr, h.insuranceid_vchr, h.idcard_chr, h.sex_chr, h.birth_dat,
       /*f.recorddate_dat,*/ k.paytypename_vchr, p.diag_vchr, j.patientcardid_chr,
       (SELECT SUM (totalsum_mny)
          FROM t_opr_outpatientrecipeinv a,t_opr_recipesend b,t_opr_recipesendentry c
         WHERE a.outpatrecipeid_chr =c.outpatrecipeid_chr and b.sid_int=c.sid_int and b.sid_int=?
           AND totalsum_mny > 0) totailmoney
  FROM 
       t_opr_recipesend m,
       t_opr_recipesendentry n,
       t_opr_outpatientrecipe a,
       t_bse_patientidx b,
       t_bse_employee c,
       t_bse_deptdesc d,
       t_bse_employee e,
       t_opr_outpatientcasehis f,
       t_bse_patient h,
       t_bse_patientpaytype k,
       t_bse_patientcard j,
       t_opr_outpatientcasehis p
 WHERE m.sid_int=n.sid_int
   and a.outpatrecipeid_chr=n.outpatrecipeid_chr
   and a.patientid_chr = b.patientid_chr(+)
   AND a.diagdr_chr = c.empid_chr(+)
   AND a.diagdept_chr = d.deptid_chr(+)
   AND a.recordemp_chr = e.empid_chr(+)
   AND a.casehisid_chr = f.casehisid_chr(+)
   AND a.patientid_chr = h.patientid_chr(+)
   AND a.paytypeid_chr = k.paytypeid_chr(+)
   AND a.patientid_chr = j.patientid_chr(+)
   AND a.casehisid_chr = p.casehisid_chr(+)
   AND m.sid_int=?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strSid_int;
                objLisAddItemRefArr[1].Value = m_strSid_int;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsOutpatientRecipe_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsOutpatientRecipe_VO();

                        p_objResultArr[i1].CONFIRM_INT = int.Parse(dtResult.Rows[i1]["CONFIRM_INT"].ToString());
                        p_objResultArr[i1].strDIAG_VCHR = dtResult.Rows[i1]["DIAG_VCHR"].ToString().Trim();
                        p_objResultArr[i1].strHOMEADDRESS_VCHR = dtResult.Rows[i1]["HOMEADDRESS_VCHR"].ToString().Trim();

                        p_objResultArr[i1].HOMEPHONE_VCHR = dtResult.Rows[i1]["HOMEPHONE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].strRECORDDATE_DAT = dtResult.Rows[i1]["RECORDDATE_DAT"].ToString().Trim();
                        p_objResultArr[i1].strDIAG_VCHR = dtResult.Rows[i1]["DIAG_VCHR"].ToString().Trim();

                        p_objResultArr[i1].m_strOutpatRecipeID = dtResult.Rows[i1]["outpatrecipeid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strOutpatRecipeNo = dtResult.Rows[i1]["outpatrecipeno_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_strRecordDate = dtResult.Rows[i1]["recorddate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_strRegisterID = dtResult.Rows[i1]["registerid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateDate = dtResult.Rows[i1]["createdate_dat"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp = new clsEmployeeVO();
                        p_objResultArr[i1].m_objRecordEmp.strEmpID = dtResult.Rows[i1]["recordemp_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objRecordEmp.strLastName = dtResult.Rows[i1]["recordemp"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient = new clsPatientVO();
                        p_objResultArr[i1].m_objPatient.strPatientID = dtResult.Rows[i1]["patientid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.strPatientCardID = dtResult.Rows[i1]["PATIENTCARDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.m_strDIFFICULTY_VCHR = dtResult.Rows[i1]["DIFFICULTY_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.objPatType = new clsPatientType_VO();
                        p_objResultArr[i1].m_objPatient.objPatType.m_strPayTypeName = dtResult.Rows[i1]["PAYTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objPatient.m_strGOVCARD_CHR = dtResult.Rows[i1]["GOVCARD_CHR"].ToString().Trim();

                        p_objResultArr[i1].m_objPatient.strInsuranceID = dtResult.Rows[i1]["INSURANCEID_VCHR"].ToString().Trim();

                        p_objResultArr[i1].strIDcard = dtResult.Rows[i1]["idcard_chr"].ToString().Trim();
                        p_objResultArr[i1].strSex = dtResult.Rows[i1]["sex_chr"].ToString().Trim();
                        if (dtResult.Rows[i1]["birth_dat"] != DBNull.Value)
                        {
                            p_objResultArr[i1].dtmAge = Convert.ToDateTime(dtResult.Rows[i1]["birth_dat"]);
                        }

                        p_objResultArr[i1].m_objPatient.strName = dtResult.Rows[i1]["name_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr = new clsEmployeeVO();
                        p_objResultArr[i1].m_objDiagDr.strEmpID = dtResult.Rows[i1]["diagdr_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDr.strLastName = dtResult.Rows[i1]["lastname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept = new clsDepartmentVO();
                        p_objResultArr[i1].m_objDiagDept.strDeptID = dtResult.Rows[i1]["diagdept_chr"].ToString().Trim();
                        p_objResultArr[i1].m_objDiagDept.strDeptName = dtResult.Rows[i1]["deptname_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_intPStatus = dtResult.Rows[i1]["pstauts_int"].ToString().Trim();
                        p_objResultArr[i1].stroutpatrecipeMoney = dtResult.Rows[i1]["totailmoney"].ToString().Trim();
                        // �Ƿ����ʹ���ҩ
                        p_objResultArr[i1].IsProxyBoilMed = dtResult.Rows[i1]["isproxyboilmed"] == DBNull.Value ? 0 : Convert.ToInt32(dtResult.Rows[i1]["isproxyboilmed"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion
        #region ��ȡ������ϸ����
        /// <summary>
        /// ��ȡ������ϸ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="OUTPATRECIPEID">������¼ID</param>
        /// <param name="dtbResult">�������ݱ�</param>
        /// <returns></returns>

        [AutoComplete]
        public long m_lngGetItemData(string OUTPATRECIPEID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            string strSQL = @"SELECT DISTINCT a.registerid_chr,a.OUTPATRECIPEID_CHR, a.patientid_chr, b.name_vchr,
										      c.registerno_chr, c.registerdate_dat
										 FROM t_opr_outpatientrecipe a,
										      t_bse_patientidx b,
											  t_opr_patientregister c,
											  T_BSE_DEPTDESC d,
                                              T_BSE_EMPLOYEE e,
                                              T_BSE_EMPLOYEE f
									    WHERE a.patientid_chr = b.patientid_chr
											  AND a.registerid_chr = c.registerid_chr(+)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

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

        #region ģ����ѯ��ҩ�Ĵ�������
        /// <summary>
        /// ģ����ѯ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_strWhere">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        private long lngGetMedRecipeListByAny(string p_strWhere, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT *
								FROM v_opr_medrecipesend " + p_strWhere;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    int intRow = dtbResult.Rows.Count;
                    p_objResultArr = new clsMedRecipeSend_VO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        p_objResultArr[i] = new clsMedRecipeSend_VO();
                        p_objResultArr[i].m_objWindow = new clsOPMedStoreWin_VO();
                        p_objResultArr[i].m_objWindow.m_objMedStore = new clsMedStore_VO();
                        p_objResultArr[i].m_objSendEmp = new clsEmployeeVO();
                        p_objResultArr[i].m_objTreatEmp = new clsEmployeeVO();

                        p_objResultArr[i].m_strOutpatRecipeID = dtbResult.Rows[i]["outpatrecipeid_chr"].ToString().Trim();
                        p_objResultArr[i].m_intRecipeType = Convert.ToInt32(dtbResult.Rows[i]["recipetype_int"].ToString().Trim());
                        p_objResultArr[i].m_objWindow.m_strWindowID = dtbResult.Rows[i]["windowid_chr"].ToString().Trim();
                        p_objResultArr[i].m_objWindow.m_strWindowName = dtbResult.Rows[i]["windowname_vchr"].ToString().Trim();
                        p_objResultArr[i].m_objWindow.m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                        p_objResultArr[i].m_objWindow.m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                        p_objResultArr[i].m_objWindow.m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                        p_objResultArr[i].m_objWindow.m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                        p_objResultArr[i].m_intPStatus = Convert.ToInt32(dtbResult.Rows[i]["pstatus_int"].ToString().Trim());
                        p_objResultArr[i].m_strSendDate = dtbResult.Rows[i]["senddate_dat"].ToString().Trim();
                        p_objResultArr[i].m_objSendEmp.strEmpID = dtbResult.Rows[i]["sendemp_chr"].ToString().Trim();
                        p_objResultArr[i].m_strTreatDate = dtbResult.Rows[i]["treatdate_dat"].ToString().Trim();
                        p_objResultArr[i].m_objTreatEmp.strEmpID = dtbResult.Rows[i]["treatemp_chr"].ToString().Trim();
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

        #region ͨ������IDȡ��ǰ��Ҫ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ������IDȡ��ǰ��Ҫ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinID(string p_strID, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strID.Trim() + "' AND pstatus_int = 1";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ������IDȡ��ǰ��Ҫ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ������IDȡ��ǰ��Ҫ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByOPID(string p_strID, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(outpatrecipeid_chr) = '" + p_strID.Trim() + "' AND p_status_int = 1";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźʹ�������ȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźʹ�������ȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���ں�</param>
        /// <param name="p_intType">�������ͣ�1����ҩ��2����ҩ</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndType(string p_strID, int p_intType, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strID.Trim() + "' AND recipetype_int = " + p_intType.ToString() + " ";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźʹ���״̬ȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźʹ���״̬ȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���ں�</param>
        /// <param name="p_intStatus">����״̬��1���½���2���ѷ�ҩ...</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndStatus(string p_strID, int p_intStatus, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strID.Trim() + "' AND pstatus_int = " + p_intStatus.ToString() + " ";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźͷ���Աȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźͷ���Աȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strWinID">���ں�</param>
        /// <param name="p_strEmpID">����Ա����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndSendEmp(string p_strWinID, string p_strEmpID, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strWinID.Trim() + "' AND TRIM(sendemp_chr) = '" + p_strEmpID.Trim() + "'";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźͷ���ʱ��ȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźͷ���ʱ��ȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strWinID">���ں�</param>
        /// <param name="p_strDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndSendDate(string p_strWinID, string p_strDate, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strWinID.Trim() + "' AND senddate_dat = TO_DATE( '" + p_strDate.Trim() + "','yyyy-mm-dd')";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźʹ���Աȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźʹ���Աȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strWinID">���ں�</param>
        /// <param name="p_strEmpID">����Ա����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndTreatEmp(string p_strWinID, string p_strEmpID, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strWinID.Trim() + "' AND TRIM(treatemp_chr) = '" + p_strEmpID.Trim() + "";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ͨ�����ںźʹ���ʱ��ȡ��ҩ�Ĵ�������
        /// <summary>
        /// ͨ�����ںźʹ���ʱ��ȡ��ҩ�Ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strWinID">���ں�</param>
        /// <param name="p_strDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedRecipeListByWinAndTreatDate(string p_strWinID, string p_strDate, out clsMedRecipeSend_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedRecipeSend_VO[0];

            string strSQL = @" WHERE TRIM(windowid_chr) = '" + p_strWinID.Trim() + "' AND treatdate_dat = TO_DATE( '" + p_strDate.Trim() + "','yyyy-mm-dd')";

            lngRes = lngGetMedRecipeListByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �����շ���Ŀ��ԴID
        [AutoComplete]
        public long m_lngGetID(string NewID, out string oldID)
        {
            long lngRes = 0;
            oldID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"select ITEMSRCID_VCHR from t_bse_chargeitem where ITEMID_CHR='" + NewID + "'";
            DataTable bt = new DataTable();
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref bt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (bt.Rows.Count > 0)
            {
                oldID = bt.Rows[0]["ITEMSRCID_VCHR"].ToString();
            }
            return lngRes;
        }

        #endregion

        #region ͨ��ID����ҩƷ�������ͼ�¼��״̬
        /// <summary>
        /// ͨ��ID���ķ�ҩ�Ĵ�����¼��״̬
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">������������</param>
        ///  <param name="winID">����ID</param>
        ///  <param name="stroageID">ҩ��ID</param>
        ///  <param name="strTOLMNY">�ܽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedRecipeListByID(string winID, clsMedRecipeSend_VO p_objItem, DataTable dtbStorageDe, string stroageID, string strTOLMNY, clst_opr_nurseexecute[] nurseexecuteArr)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"UPDATE t_opr_recipesend
								 SET  AUTOPRINT_INT = ?, pstatus_int = ?,FINALTREATEEMP_CHR=?, SENDEMP_CHR =? ,senddate_dat = sysdate WHERE sid_int = ? and SENDWINDOWID_CHR=?";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(6, out paramArr);
                paramArr[0].Value = p_objItem.m_AUTOPRINT_INT;
                paramArr[1].Value = p_objItem.m_intPStatus;
                // paramArr[2].Value = p_objItem.m_objTreatEmp.strEmpID;
                paramArr[2].Value = string.Empty;
                paramArr[3].Value = p_objItem.m_objSendEmp.strEmpID;
                paramArr[4].Value = p_objItem.m_intSID;
                paramArr[5].Value = winID;

                long lngRecordsAffected = -1;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes == 1)
            {
                clsmedstorewinque p_objVO = new clsmedstorewinque();
                p_objVO.m_strMEDSTOREID_CHR = nurseexecuteArr[0].m_strFrom;
                p_objVO.m_strWINDOWID_CHR = nurseexecuteArr[0].m_strWindow;
                p_objVO.m_strOUTPATRECIPEID_CHR = nurseexecuteArr[0].m_strOUTPATRECIPEID_CHR;
                p_objVO.m_intWINDOWTYPE_INT = 0;
                clsWindowsCortrol windowsctl = new clsWindowsCortrol();
                windowsctl.m_lngDeleWinque(p_objVO);
                com.digitalwave.iCare.middletier.HIS.clsMedStorageManage mange = new clsMedStorageManage();
                DataTable dtDe = new DataTable();
                #region ��ҩ��������������
                string newid = "";
                #region ��ȡ������

                strSQL = @" select PERIODID_CHR from t_bse_period where STARTDATE_DAT<=to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss') and ENDDATE_DAT>=to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                DataTable dt = new DataTable();
                string priID = "";
                try
                {
                    lngRes = HRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    priID = dt.Rows[0][0].ToString();
                }
                #endregion

                #region ��ȡ��������

                strSQL = @" select MEDSTOREORDTYPEID_CHR from t_aid_medstoreordtype where MEDSTOREORDTYPE_VCHR='ҩ����ҩ����'";
                string OrdType = "";
                try
                {
                    lngRes = HRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    OrdType = dt.Rows[0][0].ToString();
                }
                #endregion
                HRPSvc.m_lngGenerateNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", out newid);
                strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,PERIODID_CHR,MEMO_VCHR,CREATOR_CHR,CREATEDATE_DAT,SRCID_CHR,SRCTYPE_INT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,ADUITEMP_CHR,ADUITDATE_DAT) 
                         values(?,?,sysdate,?,?,'ҩ����ҩ���ⵥ',?,sysdate,?,1,?,2,2,?,sysdate)";
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(8, out paramArr);
                    paramArr[0].Value = newid;
                    paramArr[1].Value = stroageID;
                    paramArr[2].Value = strTOLMNY;
                    paramArr[3].Value = priID;
                    paramArr[4].Value = p_objItem.m_objTreatEmp.strEmpID;
                    paramArr[5].Value = p_objItem.m_strOutpatRecipeID;
                    paramArr[6].Value = OrdType;
                    paramArr[7].Value = p_objItem.m_objTreatEmp.strEmpID;
                    long lngRecordsAffected = -1;
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                for (int i1 = 0; i1 < dtbStorageDe.Rows.Count; i1++)
                {
                    string newDeid = "";
                    int Row = i1 + 1;
                    HRPSvc.m_lngGenerateNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", out newDeid);
                    strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR)"
                        + " values('" + newDeid + "','" + newid + "',(select MEDICINEID_CHR from t_bse_medicine a,t_bse_chargeitem b where b.ITEMID_CHR='" + dtbStorageDe.Rows[i1]["ITEMID_CHR"].ToString().Trim() + "'  and b.ITEMSRCID_VCHR=a.MEDICINEID_CHR),'"
                        + Row.ToString("000") + "'," + dtbStorageDe.Rows[i1]["QTY_DEC"].ToString().Trim() + "," + dtbStorageDe.Rows[i1]["PRICE_MNY"].ToString().Trim() + ","
                        + dtbStorageDe.Rows[i1]["TOLPRICE_MNY"].ToString().Trim() + ",'" + dtbStorageDe.Rows[i1]["UNITID_CHR"].ToString().Trim() + "')";
                    try
                    {
                        lngRes = HRPSvc.DoExcute(strSQL);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
                #endregion
            }
            return lngRes;
        }
        #endregion

        #region ͨ��ID����ҩƷ�������ͼ�¼��״̬(�Ƿ��Զ���ӡ��)
        /// <summary>
        /// ͨ��ID���ķ�ҩ�Ĵ�����¼��״̬(�Ƿ��Զ���ӡ��)
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="arrList">������������</param>
        ///  <param name="winID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedRecipeListByID(string winID, List<string> arrList, int m_intFlag)
        {
            long lngRes = 0;
            if (arrList.Count > 0)
            {

                for (int i1 = 0; i1 < arrList.Count; i1++)
                {
                    m_lngPrintSucc(winID, int.Parse(arrList[i1].ToString()), m_intFlag);
                }
            }
            return lngRes;
        }
        #endregion

        #region д���Ѵ�ӡ��־
        /// <summary>
        /// д���Ѵ�ӡ��־
        /// </summary>
        /// <param name="winID"></param>
        /// <param name="currOutid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPrintSucc(string winID, int m_intSID, int m_intFlag)
        {
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            if (m_intFlag == 0)
            {
                strSQL = @"UPDATE t_opr_recipesend
								 SET  AUTOPRINT_INT = 1
                                 WHERE sid_int=" + m_intSID + "";
            }
            else if (m_intFlag == 3)
            {
                strSQL = @"UPDATE t_opr_recipesend
								 SET  AUTOPRINTYD_INT = 1
                                 WHERE sid_int=" + m_intSID + "";
            }
            else
            {
                strSQL = @"UPDATE t_opr_recipesend
								 SET  AUTOPRINT_INT = 1,INJECTPRINT_INT = 1
                                 WHERE sid_int=" + m_intSID + "";
            }

            try
            {
                lngRes = HRPSvc.DoExcute(strSQL);
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

        #region ͨ��ID����ҩƷ�������ͼ�¼��״̬(�Ƿ��Զ���ӡ��ע�䵥)
        /// <summary>
        /// ͨ��ID����ҩƷ�������ͼ�¼��״̬(�Ƿ��Զ���ӡ��ע�䵥)
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="arrList">������������</param>
        ///  <param name="winID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateRecipeSendTableByID(string winID, List<string> arrList)
        {
            long lngRes = 0;
            if (arrList.Count > 0)
            {

                for (int i1 = 0; i1 < arrList.Count; i1++)
                {
                    m_lngPrintSuccessful(winID, int.Parse(arrList[i1].ToString()));
                }
            }
            return lngRes;
        }
        #endregion

        #region д���Ѵ�ӡ��־
        /// <summary>
        /// д���Ѵ�ӡ��־
        /// </summary>
        /// <param name="winID"></param>
        /// <param name="currOutid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPrintSuccessful(string winID, int m_intSID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

            string strSQL = @"UPDATE t_opr_recipesend
								 SET  INJECTPRINT_INT = 1
                                 WHERE sid_int=" + m_intSID + "";

            try
            {
                lngRes = HRPSvc.DoExcute(strSQL);
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
        #region �Ѽ�¼��Ϊ��Ч
        /// <summary>
        /// �Ѽ�¼��Ϊ��Ч
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="winID"></param>
        /// <param name="outID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdataByID(string winID, string outID, int m_intSID)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_recipesend
								 SET PSTATUS_INT=3
							   WHERE sid_int=" + m_intSID + "";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);

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

        #region ͨ��ID���ﴦ����¼��״̬
        /// <summary>
        /// ͨ��ID���ķ�ҩ�Ĵ�����¼��״̬
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedCiPeByID(string winID, int m_intSID)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_recipesend
								 SET PSTATUS_INT=2  WHERE sid_int=" + m_intSID + "";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoExcute(strSQL);

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

        #region//����Ա������
        [AutoComplete]
        public long m_lngfinedata(string P_strID, out string p_strName, out string p_strID)
        {
            long lngRes = 0;
            p_strName = null;
            p_strID = null;
            string strSQL = "select LASTNAME_VCHR,EMPID_CHR  FROM T_BSE_EMPLOYEE WHERE EMPNO_CHR='" + P_strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult.Rows.Count > 0)
                {
                    p_strName = dtbResult.Rows[0][0].ToString().Trim();
                    p_strID = dtbResult.Rows[0][1].ToString().Trim();
                }
                else
                {
                    p_strName = "";
                    p_strID = "";
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

        #region//���ݴ���ID����Ա�����Ƽ�����
        [AutoComplete]
        public long m_lngFinaEmp(string p_patrecipeid, out string P_strID, out string p_strName)
        {
            long lngRes = 0;
            p_strName = null;
            P_strID = null;
            string strSQL = @"select a.TREATEMP_CHR, b.LASTNAME_VCHR FROM T_OPR_RECIPESEND a,T_BSE_EMPLOYEE b,t_opr_recipesendentry c
                              WHERE a.sid_int=c.sid_int
                              c.OUTPATRECIPEID_CHR='" + p_patrecipeid + "' AND a.TREATEMP_CHR=b.EMPNO_CHR";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult.Rows.Count > 0)
                {
                    P_strID = dtbResult.Rows[0]["TREATEMP_CHR"].ToString().Trim();
                    p_strName = dtbResult.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                }
                else
                {
                    P_strID = "";
                    p_strName = "";
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

        #region ͨ������ID������ID����������ȡ��ǰ��Ҫ��ҩ�Ĵ�����ϸ��������Ϣ
        /// <summary>
        /// ͨ������ID������ID����������ȡ��ǰ��Ҫ��ҩ�Ĵ�����ϸ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOPRecID">����ID</param>
        /// <param name="p_strWinID">����ID</param>
        /// <param name="p_dtOutPatrecIp">������Ϣ</param>
        /// <param name="p_dtItemDe">������ϸ��Ϣ</param>
        /// <param name="flag">��־λ,1-��ҩ����ҩ 2-������˴���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintItem(int m_intSID, string p_strWinID, out DataTable p_dtOutPatrecIp, out DataTable p_dtItemDe, int flag)
        {
            long lngRes = 0;
            p_dtItemDe = new DataTable();
            p_dtOutPatrecIp = new DataTable();
            string strSQL = @" select p.homephone_vchr, a.sendwindowid_chr as sendwindowid,
       i.outpatrecipeid_chr, b.type_int as recipetype_int, a.pstatus_int,
       a.senddate_dat, a.sendemp_chr, a.treatdate_dat, a.treatemp_chr,
       b.pstauts_int as breakpstatus, a.windowid_chr, c.name_vchr,
       d.registerno_chr, d.registerdate_dat, j.invoiceno_vchr,
       k.empno_chr as opremp_chr, k.lastname_vchr as checkname,
       e.patientcardid_chr, c.sex_chr, c.idcard_chr, r.typename_vchr,
       c.birth_dat,
       case
          when f.internalflag_int = 0
             then '�Է�'
          when f.internalflag_int = 1
             then '����'
          when f.internalflag_int = 2
             then 'ҽ��'
       end as internalname,
       f.paytypename_vchr, j.status_int, g.lastname_vchr, a.autoprint_int,
       j.recorddate_dat, c.patientid_chr, a.medstoreid_chr,
       a.senddate_dat as givedate_dat, a.sendemp_chr as giveemp_chr,
       a.returndate_dat, a.returnemp_chr, m.lastname_vchr as sendname,
       j.split_int, a.sid_int, a.serno_chr, a.injectprint_int, dept.deptname_vchr , a.autoprintyd_int 
  from t_opr_recipesend a,
       t_opr_recipesendentry i,
       t_opr_outpatientrecipe b,
       t_bse_patient c,
       t_opr_patientregister d,
       t_bse_patientcard e,
       t_bse_patientpaytype f,
       (select c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
               c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
          from (select m.seqid_chr, m.outpatrecipeid_chr
                  from t_opr_outpatientrecipeinv m,
                       t_opr_recipesend n,
                       t_opr_recipesendentry l
                 where m.outpatrecipeid_chr = l.outpatrecipeid_chr
                   and n.sid_int = l.sid_int
                   and n.sid_int = ?) b,
               t_opr_outpatientrecipeinv c
         where b.seqid_chr = c.seqid_chr) j,
       t_opr_reciperelation h,
       t_bse_employee g,
       t_bse_employee k,
       t_bse_employee m,
       t_bse_patient p,
       t_aid_recipetype r,
       t_bse_deptdesc dept
 where a.sid_int = i.sid_int
   and i.outpatrecipeid_chr = b.outpatrecipeid_chr
   and b.registerid_chr = d.registerid_chr(+)
   and b.patientid_chr = c.patientid_chr
   and b.deptmed_int <> 1
   and i.outpatrecipeid_chr = h.outpatrecipeid_chr
   and h.seqid = j.outpatrecipeid_chr
   and a.treatemp_chr = g.empid_chr(+)
   and a.sendemp_chr = m.empid_chr(+)
   and j.opremp_chr = k.empid_chr
   and b.type_int = r.type_int(+)
   and b.patientid_chr = p.patientid_chr
   and b.patientid_chr = e.patientid_chr(+)
   and b.paytypeid_chr = f.paytypeid_chr
   and b.diagdept_chr = dept.deptid_chr 
   and a.sid_int = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = m_intSID;
                paramArr[1].Value = m_intSID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtOutPatrecIp, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (p_dtOutPatrecIp.Rows.Count > 0)
            {
                m_lngGetOPRecipeListByWinAndOpRecAndType(m_intSID, p_strWinID, out p_dtItemDe, flag);
            }
            return lngRes;
        }
        #endregion

        #region ͨ������ID������ID����������ȡ��ǰ��Ҫ��ҩ�Ĵ�����ϸ
        /// <summary>
        /// ͨ������ID������ID����������ȡ��ǰ��Ҫ��ҩ�Ĵ�����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intsid"></param>
        /// <param name="p_strWinID"></param>
        /// <param name="p_dtItemDe"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPRecipeListByWinAndOpRecAndType(int m_intsid, string p_strWinID, out DataTable p_dtItemDe, int flag)
        {
            long lngRes = 0;
            p_dtItemDe = new DataTable();
            string strSQL = string.Empty;
            if (flag != 3)
            {
                #region old
                //                strSQL = @"select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, a.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, a.unitid_chr, a.tolqty_dec as qty_dec,
                //                 a.unitprice_mny as price_mny, a.tolprice_mny,
                //                 a.medstoreid_chr, a.usageid_chr, a.days_int, a.freqid_chr,
                //                 d.usagename_vchr, a.desc_vchr, b.itemopinvtype_chr,
                //                 a.dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
                //                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                //                 e.freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
                //                 '' sumusage_vchr, 't_opr_outpatientpwmrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr, a.discount_dec,
                //                 g.mednormalname_vchr, '' itemunit_vchr, g.medicinetypeid_chr,
                //                 e.times_int as times_int1, e.days_int as days_int1,
                //                 b.dosage_dec as basicdosage, e.opfredesc_vchr as freqdesc,
                //                 b.itemipunit_chr, d.putmed_int, d.opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatientpwmrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f,
                //                 t_bse_usagetype d,
                //                 t_aid_recipefreq e,
                //                 t_bse_medicine g
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and a.deptmed_int = 0
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and b.itemopinvtype_chr = f.typeid_chr
                //             and f.flag_int = 2
                //             and a.usageid_chr = d.usageid_chr(+)
                //             and a.freqid_chr = e.freqid_chr(+)
                //             and b.itemsrcid_vchr = g.medicineid_chr(+)
                //        order by a.rowno_chr, a.itemname_vchr)
                //union all
                //select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, a.unitid_chr,
                //                 (a.qty_dec * a.times_int) as qty_dec,
                //                 a.unitprice_mny as price_mny, a.tolprice_mny,
                //                 a.medstoreid_chr, '' usageid_chr, 0 as days_int,
                //                 '' freqid_chr, d.usagename_vchr,
                //                 usagedetail_vchr as desc_vchr, b.itemopinvtype_chr,
                //                 b.dosage_dec, a.itemspec_vchr, 0 as dosageqty,
                //                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                //                 e.freqname_chr, a.times_int, a.min_qty_dec as min_qty_dec1,
                //                 a.min_qty_dec, a.sumusage_vchr,
                //                 't_opr_outpatientcmrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr, a.discount_dec,
                //                 g.mednormalname_vchr, '' itemunit_vchr, g.medicinetypeid_chr,
                //                 e.times_int as times_int1, e.days_int as days_int1,
                //                 b.dosage_dec as basicdosage, e.opfredesc_vchr as freqdesc,
                //                 b.itemipunit_chr, d.putmed_int, d.opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatientcmrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f,
                //                 t_bse_usagetype d,
                //                 t_aid_recipefreq e,
                //                 t_bse_medicine g
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and a.deptmed_int = 0
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and a.itemid_chr = e.freqid_chr(+)
                //             and b.itemopinvtype_chr = f.typeid_chr
                //             and f.flag_int = 2
                //             and a.usageid_chr = d.usageid_chr(+)
                //             and b.itemsrcid_vchr = g.medicineid_chr(+)
                //        order by a.rowno_chr, a.itemname_vchr)
                //union all
                //select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, a.unitid_chr, a.qty_dec as qty_dec,
                //                 a.unitprice_mny as price_mny, a.tolprice_mny,
                //                 a.medstoreid_chr, '' as usageid_chr, 0 as days_int,
                //                 '' as freqid_chr, '' as usagename_vchr, '' as desc_vchr,
                //                 b.itemopinvtype_chr, 0 as dosage_dec, a.itemspec_vchr,
                //                 a.qty_dec as dosageqty, a.itemname_vchr, b.itemcode_vchr,
                //                 f.typename_vchr, '' freqname_chr, 0 times_int,
                //                 0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,
                //                 't_opr_outpatientothrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr,
                //                 b.dosage_dec as discount_dec, g.mednormalname_vchr,
                //                 0 type_int, a.itemunit_vchr, g.medicinetypeid_chr,
                //                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                //                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatientothrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f,
                //                 t_bse_medicine g
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and a.deptmed_int = 0
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and b.itemopinvtype_chr = f.typeid_chr
                //             and b.itemsrcid_vchr = g.medicineid_chr(+)
                //        order by a.rowno_chr, a.itemname_vchr)
                //union all
                //select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                //                 a.price_mny as price_mny, a.tolprice_mny, a.medstoreid_chr,
                //                 '' as usageid_chr, 0 as days_int, '' as freqid_chr,
                //                 '' as usagename_vchr, '' as desc_vchr, b.itemopinvtype_chr,
                //                 0 as dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
                //                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                //                 '' freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
                //                 '' sumusage_vchr, 't_opr_outpatientchkrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr,
                //                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                //                 a.itemunit_vchr, '' medicinetypeid_chr, 0 times_int1,
                //                 0 days_int1, b.dosage_dec as basicdosage, '' freqdesc,
                //                 b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatientchkrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and b.itemopinvtype_chr = f.typeid_chr
                //        order by a.rowno_chr, a.itemname_vchr)
                //union all
                //select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                //                 a.price_mny as price_mny, a.tolprice_mny, a.medstoreid_chr,
                //                 '' as usageid_chr, 0 as days_int, '' as freqid_chr,
                //                 '' as usagename_vchr, '' as desc_vchr, b.itemopinvtype_chr,
                //                 0 as dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
                //                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                //                 '' freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
                //                 '' sumusage_vchr,
                //                 't_opr_outpatienttestrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr,
                //                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                //                 0 type_int, a.itemunit_vchr, '' medicinetypeid_chr,
                //                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                //                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatienttestrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and b.itemopinvtype_chr = f.typeid_chr
                //        order by a.rowno_chr, a.itemname_vchr)
                //union all
                //select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
                //       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
                //       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
                //       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
                //       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
                //       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
                //       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
                //       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
                //       opusagedesc, itemsrcid_vchr
                //  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                //                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                //                 a.price_mny as price_mny, a.tolprice_mny, a.medstoreid_chr,
                //                 '' as usageid_chr, 0 as days_int, '' as freqid_chr,
                //                 '' as usagename_vchr, '' as desc_vchr, b.itemopinvtype_chr,
                //                 0 as dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
                //                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                //                 '' freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
                //                 '' sumusage_vchr, 't_opr_outpatientopsrecipede' as fromtable,
                //                 b.itemsrcid_vchr as medicineid_chr,
                //                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                //                 0 type_int, a.itemunit_vchr, '' medicinetypeid_chr,
                //                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                //                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                //                 b.itemsrcid_vchr
                //            from t_opr_recipesend m,
                //                 t_opr_recipesendentry n,
                //                 t_opr_outpatientopsrecipede a,
                //                 t_bse_chargeitem b,
                //                 t_bse_chargeitemextype f
                //           where m.sid_int = n.sid_int
                //             and n.outpatrecipeid_chr = a.outpatrecipeid_chr
                //             and a.itemid_chr = b.itemid_chr
                //             and m.sid_int = ?
                //             and a.windowid_chr = ?
                //             and b.itemopinvtype_chr = f.typeid_chr
                //        order by a.rowno_chr, a.itemname_vchr)
                //";
                #endregion
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                    DataTable p_dtTemp = new DataTable();
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    strSQL = @"select   a.outpatrecipeid_chr, a.dosageunit_chr, a.rowno_chr, a.itemid_chr,
         a.unitid_chr, a.tolqty_dec as qty_dec, a.unitprice_mny as price_mny,
         a.tolprice_mny, a.medstoreid_chr, a.usageid_chr, a.days_int,
         a.freqid_chr, a.desc_vchr, a.discount_dec, a.dosage_dec,
         a.itemspec_vchr, a.qty_dec as dosageqty, a.itemname_vchr,
         b.itemopinvtype_chr, b.itemcode_vchr, b.itemsrcid_vchr,
         b.itemsrcid_vchr as medicineid_chr, b.dosage_dec as basicdosage,
         b.itemipunit_chr, f.typename_vchr, d.putmed_int, d.opusagedesc,
         d.usagename_vchr, e.times_int as times_int1, e.days_int as days_int1,
         e.opfredesc_vchr as freqdesc, e.freqname_chr, 0 times_int,
         0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,
         't_opr_outpatientpwmrecipede' as fromtable,b.packqty_dec, g.mednormalname_vchr,b.opchargeflg_int,
         '' itemunit_vchr, g.medicinetypeid_chr, 
         (round(nvl(a.tolprice_mny, 0), 2) + round(nvl(a.toldiffprice_mny, 0), 2)) as facttotal 
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientpwmrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f,
         t_bse_usagetype d,
         t_aid_recipefreq e,
         t_bse_medicine g
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and a.deptmed_int <> 1
     and m.sid_int = ?
     and a.windowid_chr = ?
     and b.itemopinvtype_chr = f.typeid_chr
     and f.flag_int = 2
     and a.usageid_chr = d.usageid_chr(+)
     and a.freqid_chr = e.freqid_chr(+)
     and b.itemsrcid_vchr = g.medicineid_chr(+)
order by a.rowno_chr, a.itemname_vchr";

                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    object[] m_objItemArr;
                    p_dtItemDe = p_dtTemp.Clone();
                    DataRow m_objTempDr;
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
                        }
                    }
                    strSQL = @"select   a.outpatrecipeid_chr,b.dosageunit_chr,a.rowno_chr,a.itemid_chr,
         a.unitid_chr,(a.qty_dec * a.times_int) as qty_dec, a.unitprice_mny as price_mny,
          a.tolprice_mny,a.medstoreid_chr, '' usageid_chr,  0 as days_int,
          '' freqid_chr, usagedetail_vchr as desc_vchr, a.discount_dec, b.dosage_dec,
         a.itemspec_vchr,  0 as dosageqty, a.itemname_vchr,
         b.itemopinvtype_chr,b.itemcode_vchr,  b.itemsrcid_vchr,
         b.itemsrcid_vchr as medicineid_chr,  b.dosage_dec as basicdosage,
         b.itemipunit_chr,f.typename_vchr,d.putmed_int, d.opusagedesc,
         d.usagename_vchr, e.times_int as times_int1, e.days_int as days_int1,
         e.opfredesc_vchr as freqdesc,e.freqname_chr,a.times_int, 
         a.min_qty_dec as min_qty_dec1, a.min_qty_dec, a.sumusage_vchr,
          't_opr_outpatientcmrecipede' as fromtable ,b.packqty_dec,g.mednormalname_vchr,b.opchargeflg_int,
         '' itemunit_vchr,g.medicinetypeid_chr,
         (round(nvl(a.tolprice_mny, 0), 2) + round(nvl(a.toldiffprice_mny, 0), 2)) as facttotal 
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientcmrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f,
         t_bse_usagetype d,
         t_aid_recipefreq e,
         t_bse_medicine g
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and a.deptmed_int <> 1
     and m.sid_int = ?
     and a.windowid_chr = ?
     and a.itemid_chr = e.freqid_chr(+)
     and b.itemopinvtype_chr = f.typeid_chr
     and f.flag_int = 2
     and a.usageid_chr = d.usageid_chr(+)
     and b.itemsrcid_vchr = g.medicineid_chr(+)
order by a.rowno_chr, a.itemname_vchr";
                    p_dtTemp.Rows.Clear();
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
                        }
                    }
                    strSQL = @"select a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr, 
         a.unitid_chr,a.qty_dec as qty_dec, a.unitprice_mny as price_mny, 
         a.tolprice_mny,a.medstoreid_chr, '' as usageid_chr, 0 as days_int, 
         '' as freqid_chr, '' as desc_vchr, b.dosage_dec as discount_dec, 0 as dosage_dec,
         a.itemspec_vchr,a.qty_dec as dosageqty,a.itemname_vchr, 
         b.itemopinvtype_chr,b.itemcode_vchr, b.itemsrcid_vchr,
          b.itemsrcid_vchr as medicineid_chr,b.dosage_dec as basicdosage,
         b.itemipunit_chr,f.typename_vchr, 1 putmed_int,  '' opusagedesc,
         '' as usagename_vchr, 0 times_int1, 0 days_int1,
          '' freqdesc, '' freqname_chr,0 times_int,
         0 min_qty_dec1, 0 min_qty_dec,'' sumusage_vchr,
         't_opr_outpatientothrecipede' as fromtable, b.packqty_dec, g.mednormalname_vchr,b.opchargeflg_int,
         a.itemunit_vchr,g.medicinetypeid_chr,
         (round(nvl(a.tolprice_mny, 0), 2) + round(nvl(a.toldiffprice_mny, 0), 2)) as facttotal 
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientothrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f,
         t_bse_medicine g
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and a.deptmed_int <> 1
     and m.sid_int = ?
     and a.windowid_chr = ?
     and b.itemopinvtype_chr = f.typeid_chr
     and b.itemsrcid_vchr = g.medicineid_chr(+)
order by a.rowno_chr, a.itemname_vchr";
                    p_dtTemp.Rows.Clear();
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
                        }
                    }
                    strSQL = @"select   a.outpatrecipeid_chr, a.rowno_chr, a.itemid_chr, '' unitid_chr,
         a.qty_dec as qty_dec, a.price_mny as price_mny, a.itemunit_vchr,
         '' medicinetypeid_chr, 0 times_int1, 0 days_int1, a.tolprice_mny,
         a.medstoreid_chr, '' as usageid_chr, 0 as days_int, '' as freqid_chr,
         '' as usagename_vchr, '' as desc_vchr, a.itemspec_vchr,
         a.itemname_vchr, a.qty_dec as dosageqty, b.itemopinvtype_chr,
         b.dosageunit_chr, 0 as dosage_dec, b.itemcode_vchr,
         b.itemsrcid_vchr as medicineid_chr, b.dosage_dec as discount_dec,
         b.dosage_dec as basicdosage, '' freqdesc, b.itemipunit_chr,
         1 putmed_int, '' opusagedesc, b.itemsrcid_vchr,
         '' as mednormalname_vchr, f.typename_vchr, '' freqname_chr,
         0 times_int, 0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,b.packqty_dec,b.opchargeflg_int,
         't_opr_outpatientchkrecipede' as fromtable, a.tolprice_mny as facttotal
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientchkrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and m.sid_int = ?
     and a.windowid_chr = ?
     and b.itemopinvtype_chr = f.typeid_chr
order by a.rowno_chr, a.itemname_vchr";
                    p_dtTemp.Rows.Clear();
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
                        }
                    }
                    strSQL = @"select   a.outpatrecipeid_chr, a.rowno_chr, a.itemid_chr, a.itemunit_vchr,
         '' unitid_chr, a.qty_dec as qty_dec, a.price_mny as price_mny,
         a.tolprice_mny, a.medstoreid_chr, '' as usageid_chr, 0 as days_int,
         '' as freqid_chr, '' as usagename_vchr, '' as desc_vchr,
         0 as dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
         a.itemname_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
         0 min_qty_dec, '' sumusage_vchr,
         't_opr_outpatienttestrecipede' as fromtable,
         b.itemsrcid_vchr as medicineid_chr, b.dosage_dec as discount_dec,
         b.dosageunit_chr, b.itemopinvtype_chr, '' as mednormalname_vchr,
         0 type_int, '' medicinetypeid_chr, 0 times_int1, 0 days_int1,
         b.dosage_dec as basicdosage, '' freqdesc, b.itemcode_vchr,
         b.itemipunit_chr, 1 putmed_int, '' opusagedesc, b.itemsrcid_vchr,b.packqty_dec,b.opchargeflg_int,
         f.typename_vchr, a.tolprice_mny as facttotal
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatienttestrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and m.sid_int = ''
     and a.windowid_chr = ''
     and b.itemopinvtype_chr = f.typeid_chr
order by a.rowno_chr, a.itemname_vchr";
                    p_dtTemp.Rows.Clear();
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
                        }
                    }
                    strSQL = @"select   a.outpatrecipeid_chr, a.rowno_chr, a.itemid_chr, a.itemunit_vchr,
         '' unitid_chr, a.qty_dec as qty_dec, a.price_mny as price_mny,
         a.tolprice_mny, a.medstoreid_chr, '' as usageid_chr, 0 as days_int,
         a.itemspec_vchr, a.qty_dec as dosageqty, a.itemname_vchr,
         '' as freqid_chr, '' as usagename_vchr, '' as desc_vchr,
         b.itemopinvtype_chr, 0 as dosage_dec, b.itemcode_vchr,
         '' freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
         '' sumusage_vchr, 't_opr_outpatientopsrecipede' as fromtable,
         b.itemsrcid_vchr as medicineid_chr, '' as mednormalname_vchr,
         0 type_int, '' medicinetypeid_chr, 0 times_int1, 0 days_int1,
         b.dosage_dec as basicdosage, '' freqdesc, b.itemipunit_chr,
         1 putmed_int, '' opusagedesc, b.dosage_dec as discount_dec,b.packqty_dec,b.opchargeflg_int,
         b.dosageunit_chr, b.itemsrcid_vchr, f.typename_vchr, a.tolprice_mny as facttotal
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientopsrecipede a,
         t_bse_chargeitem b,
         t_bse_chargeitemextype f
   where m.sid_int = n.sid_int
     and n.outpatrecipeid_chr = a.outpatrecipeid_chr
     and a.itemid_chr = b.itemid_chr
     and m.sid_int = ?
     and a.windowid_chr = ?
     and b.itemopinvtype_chr = f.typeid_chr
order by a.rowno_chr, a.itemname_vchr";
                    p_dtTemp.Rows.Clear();
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid;
                    objLisAddItemRefArr[1].Value = p_strWinID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtTemp, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        for (int i = 0; i < p_dtTemp.Rows.Count; i++)
                        {
                            m_objItemArr = p_dtTemp.Rows[i].ItemArray;
                            m_objTempDr = p_dtItemDe.NewRow();
                            m_objTempDr.ItemArray = m_objItemArr;
                            p_dtItemDe.Rows.Add(m_objTempDr);
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
            #region 3-��˴���
            else
            {
                strSQL = @"select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, a.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, a.unitid_chr, a.tolqty_dec as qty_dec,
                 a.unitprice_mny as price_mny, a.tolprice_mny, a.usageid_chr,
                 a.days_int, a.freqid_chr, d.usagename_vchr, a.desc_vchr,
                 b.itemopinvtype_chr, a.dosage_dec, a.itemspec_vchr,
                 a.qty_dec as dosageqty, a.itemname_vchr, b.itemcode_vchr,
                 f.typename_vchr, e.freqname_chr, 0 times_int, 0 min_qty_dec1,
                 0 min_qty_dec, '' sumusage_vchr,
                 't_tmp_outpatientpwmrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr, a.discount_dec,
                 g.mednormalname_vchr, '' itemunit_vchr, g.medicinetypeid_chr,
                 e.times_int as times_int1, e.days_int as days_int1,
                 b.dosage_dec as basicdosage, e.opfredesc_vchr as freqdesc,
                 b.itemipunit_chr, d.putmed_int, d.opusagedesc,
                 b.itemsrcid_vchr
            from t_tmp_outpatientpwmrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f,
                 t_bse_usagetype d,
                 t_aid_recipefreq e,
                 t_bse_medicine g
           where a.itemid_chr = b.itemid_chr
             and a.deptmed_int <> 1
             and a.outpatrecipeid_chr = ?
             and b.itemopinvtype_chr = f.typeid_chr
             and f.flag_int = 2
             and a.usageid_chr = d.usageid_chr(+)
             and a.freqid_chr = e.freqid_chr(+)
             and b.itemsrcid_vchr = g.medicineid_chr(+)
        order by a.rowno_chr, a.itemname_vchr)
union all
select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, a.unitid_chr,
                 (a.qty_dec * a.times_int) as qty_dec,
                 a.unitprice_mny as price_mny, a.tolprice_mny, '' usageid_chr,
                 0 as days_int, '' freqid_chr, d.usagename_vchr,
                 usagedetail_vchr as desc_vchr, b.itemopinvtype_chr,
                 b.dosage_dec, a.itemspec_vchr, 0 as dosageqty,
                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                 e.freqname_chr, a.times_int, a.min_qty_dec as min_qty_dec1,
                 a.min_qty_dec, a.sumusage_vchr,
                 't_tmp_outpatientcmrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr, a.discount_dec,
                 g.mednormalname_vchr, '' itemunit_vchr, g.medicinetypeid_chr,
                 e.times_int as times_int1, e.days_int as days_int1,
                 b.dosage_dec as basicdosage, e.opfredesc_vchr as freqdesc,
                 b.itemipunit_chr, d.putmed_int, d.opusagedesc,
                 b.itemsrcid_vchr
            from t_tmp_outpatientcmrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f,
                 t_bse_usagetype d,
                 t_aid_recipefreq e,
                 t_bse_medicine g
           where a.itemid_chr = b.itemid_chr
             and a.deptmed_int <> 1
             and a.outpatrecipeid_chr = ?
             and a.itemid_chr = e.freqid_chr(+)
             and b.itemopinvtype_chr = f.typeid_chr
             and f.flag_int = 2
             and a.usageid_chr = d.usageid_chr(+)
             and b.itemsrcid_vchr = g.medicineid_chr(+)
        order by a.rowno_chr, a.itemname_vchr)
union all
select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, a.unitid_chr, a.qty_dec as qty_dec,
                 a.unitprice_mny as price_mny, a.tolprice_mny,
                 '' as usageid_chr, 0 as days_int, '' as freqid_chr,
                 '' as usagename_vchr, '' as desc_vchr, b.itemopinvtype_chr,
                 0 as dosage_dec, a.itemspec_vchr, a.qty_dec as dosageqty,
                 a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
                 '' freqname_chr, 0 times_int, 0 min_qty_dec1, 0 min_qty_dec,
                 '' sumusage_vchr, 't_tmp_outpatientothrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr,
                 b.dosage_dec as discount_dec, g.mednormalname_vchr,
                 0 type_int, a.itemunit_vchr, g.medicinetypeid_chr,
                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                 b.itemsrcid_vchr
            from t_tmp_outpatientothrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f,
                 t_bse_medicine g
           where a.itemid_chr = b.itemid_chr
             and a.deptmed_int <> 1
             and a.outpatrecipeid_chr = ?
             and b.itemopinvtype_chr = f.typeid_chr
             and b.itemsrcid_vchr = g.medicineid_chr(+)
        order by a.rowno_chr, a.itemname_vchr)
union all
select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                 a.price_mny as price_mny, a.tolprice_mny, '' as usageid_chr,
                 0 as days_int, '' as freqid_chr, '' as usagename_vchr,
                 '' as desc_vchr, b.itemopinvtype_chr, 0 as dosage_dec,
                 a.itemspec_vchr, a.qty_dec as dosageqty, a.itemname_vchr,
                 b.itemcode_vchr, f.typename_vchr, '' freqname_chr,
                 0 times_int, 0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,
                 't_tmp_outpatientchkrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr,
                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                 0 type_int, a.itemunit_vchr, '' medicinetypeid_chr,
                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                 b.itemsrcid_vchr
            from t_tmp_outpatientchkrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f
           where a.itemid_chr = b.itemid_chr
             and a.outpatrecipeid_chr = ?
             and b.itemopinvtype_chr = f.typeid_chr
        order by a.rowno_chr, a.itemname_vchr)
union all
select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                 a.price_mny as price_mny, a.tolprice_mny, '' as usageid_chr,
                 0 as days_int, '' as freqid_chr, '' as usagename_vchr,
                 '' as desc_vchr, b.itemopinvtype_chr, 0 as dosage_dec,
                 a.itemspec_vchr, a.qty_dec as dosageqty, a.itemname_vchr,
                 b.itemcode_vchr, f.typename_vchr, '' freqname_chr,
                 0 times_int, 0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,
                 't_tmp_outpatienttestrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr,
                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                 0 type_int, a.itemunit_vchr, '' medicinetypeid_chr,
                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                 b.itemsrcid_vchr
            from t_tmp_outpatienttestrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f
           where a.itemid_chr = b.itemid_chr
             and a.outpatrecipeid_chr = ?
             and b.itemopinvtype_chr = f.typeid_chr
        order by a.rowno_chr, a.itemname_vchr)
union all
select outpatrecipeid_chr, dosageunit_chr, rowno_chr, itemid_chr, unitid_chr,
       qty_dec, price_mny, tolprice_mny, medstoreid_chr, usageid_chr,
       days_int, freqid_chr, usagename_vchr, desc_vchr, itemopinvtype_chr,
       dosage_dec, itemspec_vchr, dosageqty, itemname_vchr, itemcode_vchr,
       typename_vchr, freqname_chr, times_int, min_qty_dec1, min_qty_dec,
       sumusage_vchr, fromtable, medicineid_chr, discount_dec,
       mednormalname_vchr, itemunit_vchr, medicinetypeid_chr, times_int1,
       days_int1, basicdosage, freqdesc, itemipunit_chr, putmed_int,
       opusagedesc, itemsrcid_vchr
  from (select   a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr,
                 a.itemid_chr, '' unitid_chr, a.qty_dec as qty_dec,
                 a.price_mny as price_mny, a.tolprice_mny, '' as usageid_chr,
                 0 as days_int, '' as freqid_chr, '' as usagename_vchr,
                 '' as desc_vchr, b.itemopinvtype_chr, 0 as dosage_dec,
                 a.itemspec_vchr, a.qty_dec as dosageqty, a.itemname_vchr,
                 b.itemcode_vchr, f.typename_vchr, '' freqname_chr,
                 0 times_int, 0 min_qty_dec1, 0 min_qty_dec, '' sumusage_vchr,
                 't_opr_outpatientopsrecipede' as fromtable,
                 b.itemsrcid_vchr as medicineid_chr,
                 b.dosage_dec as discount_dec, '' as mednormalname_vchr,
                 0 type_int, a.itemunit_vchr, '' medicinetypeid_chr,
                 0 times_int1, 0 days_int1, b.dosage_dec as basicdosage,
                 '' freqdesc, b.itemipunit_chr, 1 putmed_int, '' opusagedesc,
                 b.itemsrcid_vchr
            from t_opr_outpatientopsrecipede a,
                 t_bse_chargeitem b,
                 t_bse_chargeitemextype f
           where a.itemid_chr = b.itemid_chr
             and a.outpatrecipeid_chr = ?
             and b.itemopinvtype_chr = f.typeid_chr
        order by a.rowno_chr, a.itemname_vchr)";
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(6, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_intsid.ToString();
                    objLisAddItemRefArr[1].Value = m_intsid.ToString();
                    objLisAddItemRefArr[2].Value = m_intsid.ToString();
                    objLisAddItemRefArr[3].Value = m_intsid.ToString();
                    objLisAddItemRefArr[4].Value = m_intsid.ToString();
                    objLisAddItemRefArr[5].Value = m_intsid.ToString();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtItemDe, objLisAddItemRefArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ����ҩƷ���ţ����Ŀ�棩
        /// <summary>
        /// ����ҩƷ���ţ����Ŀ�棩
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strMedStoreID">ҩ��</param>
        /// <param name="p_strWinID">����</param>
        /// <param name="p_strOPRecID">������</param>
        /// <param name="p_intType">�������ͣ�1����ҩ��2����ҩ</param>
        /// <param name="p_intFlag">��־��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOPRecipeMedProvide(string p_strMedStoreID, string p_strWinID, string p_strOPRecID, int p_intType, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            try
            {
                string strProcedure = "P_OPMEDSTOREWINSEND";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[5];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strMedStoreID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "medstoreid";

                objParams[1].objParameter_Value = p_strWinID;
                objParams[1].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[1].strParameter_Name = "winid";

                objParams[2].objParameter_Value = p_strOPRecID;
                objParams[2].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[2].strParameter_Name = "recipeid";

                objParams[3].objParameter_Value = p_intType;
                objParams[3].strParameter_Type = clsOracleDbType.strInt32;
                objParams[3].strParameter_Name = "typeid";

                objParams[4].strParameter_Type = clsOracleDbType.strInt32;
                objParams[4].strParameter_Direction = clsDirection.strOutput;
                objParams[4].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[4].objParameter_Value.ToString().Trim());
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

        #region ��ô����������շ���ϸ
        /// <summary>
        /// ��ô����������շ���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOUTPATRECIPEID"></param>
        /// <param name="btpatientcnkre">�����</param>
        /// <param name="btpatientest">����</param>
        /// <param name="btpatienOpsre">������</param>
        /// <param name="btpatienothre">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAll(string p_strOUTPATRECIPEID, out DataTable btpatientcnkre, out DataTable btpatientest, out DataTable btpatienOpsre, out DataTable btpatienothre)
        {
            long lngRes = 0;
            btpatientcnkre = new DataTable();
            btpatientest = new DataTable();
            btpatienOpsre = new DataTable();
            btpatienothre = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select b.ITEMNAME_VCHR,a.PRICE_MNY,a.OPRDEPT_CHR,a.DISCOUNT_DEC,a.TOLPRICE_MNY from t_opr_outpatientchkrecipede a, t_bse_chargeitem b where a.ITEMID_CHR=b.ITEMID_CHR and a.OUTPATRECIPEID_CHR='" + p_strOUTPATRECIPEID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref btpatientcnkre);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"select b.ITEMNAME_VCHR,a.PRICE_MNY,a.OPRDEPT_CHR,a.DISCOUNT_DEC,a.TOLPRICE_MNY from t_opr_outpatienttestrecipede a, t_bse_chargeitem b where a.ITEMID_CHR=b.ITEMID_CHR and a.OUTPATRECIPEID_CHR='" + p_strOUTPATRECIPEID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref btpatientest);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select b.ITEMNAME_VCHR,a.PRICE_MNY,a.OPRDEPT_CHR,a.DISCOUNT_DEC,a.TOLPRICE_MNY from t_opr_outpatientopsrecipede a, t_bse_chargeitem b where a.ITEMID_CHR=b.ITEMID_CHR and a.OUTPATRECIPEID_CHR='" + p_strOUTPATRECIPEID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref btpatienOpsre);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select b.ITEMNAME_VCHR,a.UNITID_CHR,a.UNITPRICE_MNY,a.QTY_DEC,a.DISCOUNT_DEC,a.TOLPRICE_MNY from t_opr_outpatientothrecipede a, t_bse_chargeitem b where a.ITEMID_CHR=b.ITEMID_CHR and a.OUTPATRECIPEID_CHR='" + p_strOUTPATRECIPEID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref btpatienothre);
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

        #region ������ҩ

        #region ��ȡ���е���Ŀ����
        [AutoComplete]
        public long m_mthFindMedicine(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select A.ITEMSRCID_VCHR,A.ITEMID_CHR case when A.ITEMCATID_CHR='0002' then '��ҩ' when A.ITEMCATID_CHR='0003' then '����' when A.ITEMCATID_CHR='0004' then '����' when A.ITEMCATID_CHR='0005' then '����' when A.ITEMCATID_CHR='0006' then '����' when A.ITEMCATID_CHR='0001' then '��ҩ' end as ItemType,A.ITEMNAME_VCHR,A.ITEMSPEC_VCHR,A.ITEMENGNAME_VCHR,A.ITEMWBCODE_CHR,A.ITEMPYCODE_CHR,case when A.OPCHARGEFLG_INT=1 then  ROUND (a.ITEMPRICE_MNY / a.PACKQTY_DEC, 4) when A.OPCHARGEFLG_INT=0 then  A.ITEMPRICE_MNY end as submoney,case when A.OPCHARGEFLG_INT=1 then A.ITEMIPUNIT_CHR when A.OPCHARGEFLG_INT=0 then A.ITEMOPUNIT_CHR end as ITEMOPUNIT_CHR,A.ITEMOPUNIT_CHR as ITEMOPUNIT,a.ITEMPRICE_MNY,a.ISRICH_INT,
A.ITEMOPINVTYPE_CHR,A.ITEMCATID_CHR,A.SELFDEFINE_INT,A.ITEMCODE_VCHR,A.ITEMOPCALCTYPE_CHR,
B.NOQTYFLAG_INT,a.itemipunit_chr,a.opchargeflg_int from t_bse_chargeitem A ,T_BSE_MEDICINE B
where  trim(A.ITEMSRCID_VCHR)=trim(B.MEDICINEID_CHR(+)) and a.IFSTOP_INT =0  order by ITEMCODE_VCHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region ������ⵥ�޸Ŀ��
        /// <summary>
        /// ������ⵥ�޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="SaveRow"></param>
        /// <param name="SaveTableDe"></param>
        /// <returns>1-������2-��û������ҩ���������ͣ�3-û���ҵ���Ӧ��ҩƷ</returns>
        [AutoComplete]
        public long m_mthSaveData(DataRow SaveRow, DataTable SaveTableDe)
        {
            long lngRes = 0;
            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select MEDSTOREORDTYPEID_CHR from  t_aid_medstoreordtype where MEDSTOREORDTYPE_VCHR='��ҩ����'";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count == 0)
            {
                return 2;//��û������ҩ����������
            }
            else
            {
                SaveRow["MEDSTOREORDTYPEID_CHR"] = dt.Rows[0]["MEDSTOREORDTYPEID_CHR"];
            }
            string newid = objHRPSvc.m_strGetNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", 18);
            strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,MEMO_VCHR,CREATOR_CHR," +
               "CREATEDATE_DAT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,PERIODID_CHR,ADUITEMP_CHR) values('" + newid + "','"
               + SaveRow["MEDSTOREID_CHR"].ToString() + "',To_Date('" + SaveRow["ORDDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')," + SaveRow["TOLMNY_MNY"].ToString() + ",'" + SaveRow["MEMO_VCHR"].ToString() + "','"
               + SaveRow["CREATOR_CHR"].ToString() + "',To_Date('" + SaveRow["CREATEDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'"
               + SaveRow["MEDSTOREORDTYPEID_CHR"].ToString() + "',2,2,'" + SaveRow["PERIODID_CHR"].ToString() + "','" + SaveRow["ADUITEMP_CHR"].ToString() + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && SaveTableDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < SaveTableDe.Rows.Count; i1++)
                {
                    string newDeid = objHRPSvc.m_strGetNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", 18);
                    strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR)"
                        + " values('" + newDeid + "','" + newid + "','" + SaveTableDe.Rows[i1]["MEDICINEID_CHR"].ToString() + "',"
                        + SaveTableDe.Rows[i1]["QTY_DEC"].ToString() + ","
                        + SaveTableDe.Rows[i1]["SALEUNITPRICE_DEC"].ToString() + "," + SaveTableDe.Rows[i1]["SALETOLPRICE_DEC"].ToString() + ",'" + SaveTableDe.Rows[i1]["UNITID_CHR"].ToString() + "')";
                    try
                    {
                        lngRes = objHRPSvc.DoExcute(strSQL);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (lngRes == 0)
                        ContextUtil.SetAbort();
                    //					#region �޸Ŀ��
                    //					DataTable dtDe=new DataTable();
                    //					strSQL=@"select AMOUNT_DEC from t_tol_medstoremedicine where MEDSTOREID_CHR='"+SaveRow["MEDSTOREID_CHR"].ToString()+"' and MEDICINEID_CHR='"+SaveTableDe.Rows[i1]["MEDICINEID_CHR"].ToString()+"'";
                    //					try
                    //					{
                    //						lngRes=objHRPSvc.DoGetDataTable(strSQL,ref dtDe);
                    //					}		
                    //					catch(Exception objEx)
                    //					{
                    //						string strTmp=objEx.Message;
                    //						com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    //						bool blnRes = objLogger.LogError(objEx);
                    //					}
                    //					if(dtDe.Rows.Count>0)
                    //					{
                    //						int Couns=Convert.ToInt32(dtDe.Rows[0]["AMOUNT_DEC"].ToString())-Convert.ToInt32(SaveTableDe.Rows[i1]["QTY_DEC"].ToString());
                    //						strSQL=@"update t_tol_medstoremedicine set AMOUNT_DEC="+Couns.ToString()+" where MEDSTOREID_CHR='"+SaveRow["MEDSTOREID_CHR"].ToString()+"' and MEDICINEID_CHR='"+SaveTableDe.Rows[i1]["MEDICINEID_CHR"].ToString()+"'"; 
                    //						try
                    //						{
                    //							lngRes=objHRPSvc.DoExcute(strSQL);
                    //						}		
                    //						catch(Exception objEx)
                    //						{
                    //							string strTmp=objEx.Message;
                    //							com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    //							bool blnRes = objLogger.LogError(objEx);
                    //						}
                    //						if(lngRes==0)
                    //							ContextUtil.SetAbort();
                    //					}
                    //					else
                    //					{
                    //						strSQL=@"insert into t_tol_medstoremedicine(MEDSTOREID_CHR,MEDICINEID_CHR,AMOUNT_DEC) VALUES('002"+"','"+SaveTableDe.Rows[i1]["MEDICINEID_CHR"].ToString()+"',"+"-"+SaveTableDe.Rows[i1]["QTY_DEC"].ToString()+")";
                    //						try
                    //						{
                    //							lngRes=objHRPSvc.DoGetDataTable(strSQL,ref dtDe);
                    //						}		
                    //						catch(Exception objEx)
                    //						{
                    //							string strTmp=objEx.Message;
                    //							com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    //							bool blnRes = objLogger.LogError(objEx);
                    //						}
                    //						if(lngRes==0)
                    //							ContextUtil.SetAbort();
                    //					}
                    //
                    //					#endregion
                }
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region �жϷ�Ʊ�Ƿ���Ч
        [AutoComplete]
        public bool m_blCheckOut(string strOUTPATRECIPEID)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string strSQL = @"select STATUS_INT from t_opr_outpatientrecipeinv where INVOICENO_VCHR='" + strOUTPATRECIPEID + "' and STATUS_INT=2";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            string strSQL2 = @"select STATUS_INT from t_opr_outpatientrecipeinv where INVOICENO_VCHR='" + strOUTPATRECIPEID + "' and STATUS_INT=3";
            try
            {
                objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0 && dt1.Rows.Count == 0)
            {
                return false;
            }
            else if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                return true;
            }
            return true;

        }
        #endregion

        #region ��ȡ�����ķ���
        [AutoComplete]
        public string m_getOutpatientNO(string strOUTPATRECIPEID, string RECORDDATE, string strPATIENTID)
        {
            string strNO = "��";
            DataTable dt = new DataTable();
            string strSQL = @"select distinct(a.OUTPATRECIPEID_CHR) from (select b.OUTPATRECIPEID_CHR from t_opr_outpatientrecipeinv a,T_OPR_RECIPERELATION b where a.PATIENTID_CHR='" + strPATIENTID + "' and a.RECORDDATE_DAT=to_date('" + RECORDDATE + "','yyyy-mm-dd hh24:mi:ss') and a.OUTPATRECIPEID_CHR=b.seqid)  a,t_opr_outpatientrecipe b where a.OUTPATRECIPEID_CHR=b.OUTPATRECIPEID_CHR order by a.OUTPATRECIPEID_CHR";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                if (dt.Rows[i1]["OUTPATRECIPEID_CHR"].ToString() == strOUTPATRECIPEID)
                {
                    int intNo = i1 + 1;
                    strNO += intNo.ToString() + "��" + dt.Rows.Count.ToString() + "��";
                }
            }
            return strNO;

        }
        #endregion

        #region ��ȡ����ҩ��������ָ���ҩ��
        /// <summary>
        /// ��ȡ����ҩ��������ָ���ҩ��
        /// </summary>
        /// <param name="strStorageID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_longDutydt(string strStorageID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

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
            string strSQL = @"select DEPTID_VCHR,WEEKDAY_INT,WORKTIME_VCHR from t_bse_deptduty where OBJECTDEPTID_VCHR='" + strStorageID + "' and TYPEID_INT=1 and WEEKDAY_INT=" + weekDay_int.ToString();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region ��˴�����������
        /// <summary>
        /// ��˴�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="intStatus"></param>
        /// <param name="strCONFIRMDESC"></param>
        /// <param name="strEMPID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditing(clsOutpatientRecipe_VO[] p_objRecord, int intStatus, string strCONFIRMDESC, string strEMPID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (p_objRecord.Length > 0)
            {
                string strSQL = "";
                for (int i1 = 0; i1 < p_objRecord.Length; i1++)
                {
                    strSQL = @"update T_OPR_OUTPATIENTRECIPE set CONFIRM_INT=" + intStatus + ",CONFIRMDESC_VCHR='" + strCONFIRMDESC + "' where OUTPATRECIPEID_CHR='" + p_objRecord[i1].m_strOutpatRecipeID + "'";
                    try
                    {
                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    clsOPRCONFIRMVO p_objRecord1 = new clsOPRCONFIRMVO();
                    p_objRecord1.m_intCONFRIM_INT = intStatus;
                    p_objRecord1.m_strCONFIRMDESC_VCHR = strCONFIRMDESC;
                    p_objRecord1.m_strCONFIRMEMP_CHR = strEMPID;
                    p_objRecord1.m_strOUTPATRECIPEID_CHR = p_objRecord[i1].m_strOutpatRecipeID;
                    p_objRecord1.m_strCONFIRMDATE_DAT = DateTime.Now.ToString();
                    m_lngAddNewData(p_objRecord1);
                }
            }
            return lngRes;
        }
        #endregion

        #region ���ﴦ����˼�¼��
        /// <summary>
        /// ���ﴦ����˼�¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewData(clsOPRCONFIRMVO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_OPR_OPRCONFIRM (SEQ_INT,OUTPATRECIPEID_CHR,CONFIRMEMP_CHR,CONFIRMDATE_DAT,CONFRIM_INT,CONFIRMDESC_VCHR) VALUES (SEQ_OPRCONFIRM.NEXTVAL,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strOUTPATRECIPEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strCONFIRMEMP_CHR;
                objLisAddItemRefArr[2].Value = DateTime.Parse(p_objRecord.m_strCONFIRMDATE_DAT);
                objLisAddItemRefArr[3].Value = p_objRecord.m_intCONFRIM_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strCONFIRMDESC_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region  ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objDep"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPDeptList(out DataTable objDep)
        {
            objDep = new DataTable();
            long lngRes = 0;
            string strSQL = @"select CODE_VCHR,DEPTNAME_VCHR,PYCODE_CHR,WBCODE_CHR,DEPTID_CHR from T_BSE_DEPTDESC where CATEGORY_INT=0 and (ATTRIBUTEID = '0000002' or ATTRIBUTEID ='0000001') and DEPTNAME_VCHR <> '����' and INPATIENTOROUTPATIENT_INT = 0  order by SHORTNO_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref objDep);
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

        #region ���ݵ�������ȡ�÷��б�0 ע�䵥 1 ��ҺѲ�ӿ� 2 ��ƿ�� 3 ���Ƶ� 4 ������ 5 ��Ѫ�� 6 ��ҩ 7 ��ҩ
        /// <summary>
        /// ���ݵ�������ȡ�÷��б�0 ע�䵥 1 ��ҺѲ�ӿ� 2 ��ƿ�� 3 ���Ƶ� 4 ������ 5 ��Ѫ�� 6 ��ҩ 7 ��ҩ
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsagebyordertypeid(string typeid, out DataTable dtRecord)
        {
            long lngRes = 0;
            dtRecord = new DataTable();

            string SQL = "select distinct usageid_chr from t_opr_setusage where trim(orderid_vchr) = '" + typeid + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
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

        #region �����û����������ȡԱ������
        /// <summary>
        /// �����û����������ȡԱ������
        /// </summary>
        /// <param name="EmpNO"></param>
        /// <param name="EmpPw"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpName(string EmpNO, string EmpPw, out string EmpName, out string EmpID)
        {
            EmpName = "";
            EmpID = "";
            long lngRes = 0;
            DataTable dtRecord = new DataTable();
            string SQL = "";
            if (EmpPw == "")
            {
                SQL = "select LASTNAME_VCHR,EMPID_CHR from t_bse_employee where EMPNO_CHR='" + EmpNO + "' and PSW_CHR is null";
            }
            else
            {
                com.digitalwave.iCare.middletier.CryptographyLib.clsSymmetricAlgorithm objSym = new com.digitalwave.iCare.middletier.CryptographyLib.clsSymmetricAlgorithm();
                string strPW = objSym.m_strEncrypt(EmpPw, com.digitalwave.iCare.middletier.CryptographyLib.clsSymmetricAlgorithm.enmSymmetricAlgorithmType.DES);
                SQL = "select LASTNAME_VCHR,EMPID_CHR from t_bse_employee where EMPNO_CHR='" + EmpNO + "' and trim(PSW_CHR)='" + strPW + "'";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtRecord.Rows.Count > 0)
            {
                EmpName = dtRecord.Rows[0]["LASTNAME_VCHR"].ToString();
                EmpID = dtRecord.Rows[0]["EMPID_CHR"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region ����ҩ������
        /// <summary>
        /// ����ҩ������
        /// </summary>
        [AutoComplete]
        public long m_lngGetMedStoreData(string p_strWhere, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strSQL = "select medstoreid_chr, medstorename_vchr, medstoretype_int, medicnetype_int, urgence_int, deptid_chr, shortname_chr from t_bse_medstore " + p_strWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        [AutoComplete]
        public long m_lngGetItemData(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strSQL = @"SELECT itemcode_vchr, itemname_vchr, itempycode_chr, itemwbcode_chr,
                                       itemid_chr
                                 FROM t_bse_chargeitem
                                 WHERE ifstop_int = 0 ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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
        /// <summary>
        /// ��������ҩ��ҩƷ�����嵥��������
        /// </summary>
        [AutoComplete]
        public long m_lngGetMedSendItemData(string p_strRecordBeginDate, string p_strRecordEndDate, string[] p_strMedstoreIdArr, string[] p_strStatus, string p_strOrderBy, string p_strSingleItemName, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            #region ��֯����
            string strTemp = "";
            if (p_strSingleItemName != "")
            {

                strTemp = " And ( td.itemname_vchr = '" + p_strSingleItemName + "')";
            }
            string strAnd = strTemp + " And (";
            for (int i = 0; i < p_strMedstoreIdArr.Length; i++)
            {
                if (i != p_strMedstoreIdArr.Length - 1)
                {
                    strAnd += " tf.medstoreid_chr='" + p_strMedstoreIdArr[i].Trim() + "' OR ";
                }
                else
                {
                    strAnd += " tf.medstoreid_chr='" + p_strMedstoreIdArr[i].Trim() + "' ) ";
                }
            }
            strAnd += "And (";
            for (int i = 0; i < p_strStatus.Length; i++)
            {
                if (i != p_strStatus.Length - 1)
                {
                    strAnd += " te.pstatus_int=" + p_strStatus[i].Trim() + " OR ";
                }
                else
                {
                    strAnd += " te.pstatus_int=" + p_strStatus[i].Trim() + " ) ";
                }
            }
            strAnd += " and ta.recorddate_dat between to_date('" + p_strRecordBeginDate + " 00:00:00', 'yyyy-mm-dd hh24:mi:ss') AND to_date('" + p_strRecordEndDate + " 23:59:59', 'yyyy-mm-dd hh24:mi:ss')";

            #endregion

            #region sql
            StringBuilder strSQL = new StringBuilder();
            if (p_strOrderBy != "")
            {
                strSQL = strSQL.Append(@"SELECT * FROM (");
            }
            strSQL = strSQL.Append(@" SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.TOLQTY_DEC qty_dec,
                                   td.dosageunit_chr, td.unitprice_mny, td.tolprice_mny,
                                   th1.lastname_vchr AS treatemp_name, th2.lastname_vchr AS give_name,
                                   tf.medstoreid_chr, tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatientpwmrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd + @"
                            UNION ALL
                            SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.qty_dec,
                                   tg.dosageunit_chr, td.unitprice_mny, td.tolprice_mny,
                                   th1.lastname_vchr AS treatemp_name, th2.lastname_vchr AS give_name,
                                   tf.medstoreid_chr, tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatientcmrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd + @"
                            UNION ALL
                            SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.qty_dec,
                                   tg.dosageunit_chr, td.price_mny unitprice_mny, td.tolprice_mny,
                                   th1.lastname_vchr AS treatemp_name, th2.lastname_vchr AS give_name,
                                   tf.medstoreid_chr, tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatientchkrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd + @"
                            UNION ALL
                            SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.qty_dec,
                                   td.itemunit_vchr dosageunit_chr, td.price_mny unitprice_mny,
                                   td.tolprice_mny, th1.lastname_vchr AS treatemp_name,
                                   th2.lastname_vchr AS give_name, tf.medstoreid_chr,
                                   tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatienttestrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd + @"
                            UNION ALL
                            SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.qty_dec,
                                   td.itemunit_vchr dosageunit_chr, td.price_mny unitprice_mny,
                                   td.tolprice_mny, th1.lastname_vchr AS treatemp_name,
                                   th2.lastname_vchr AS give_name, tf.medstoreid_chr,
                                   tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatientopsrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd + @"
                            UNION ALL
                            SELECT ta.outpatrecipeid_chr, ta.patientid_chr,
                                   TO_CHAR (ta.recorddate_dat, 'yyyy-mm-dd') AS recorddate_dat,
                                   ta.diagdr_chr, tb.lastname_vchr, tc.lastname_vchr AS patientname,
                                   tg.itemcode_vchr, td.itemname_vchr, td.itemspec_vchr, td.qty_dec,
                                   td.itemunit_vchr dosageunit_chr, td.unitprice_mny unitprice_mny,
                                   td.tolprice_mny AS tolprice_mny, th1.lastname_vchr AS treatemp_name,
                                   th2.lastname_vchr AS give_name, tf.medstoreid_chr,
                                   tf.medstorename_vchr,td.itemid_chr,td.itemid_chr strTemp1
                              FROM t_opr_outpatientrecipe ta,
                                   t_bse_employee tb,
                                   t_bse_patient tc,
                                   t_opr_outpatientothrecipede td,
                                   t_opr_recipesend te,
                                   t_opr_recipesendentry i,
                                   t_bse_medstore tf,
                                   t_bse_chargeitem tg,
                                   t_bse_employee th1,
                                   t_bse_employee th2
                             WHERE te.sid_int=i.sid_int
                               AND ta.outpatrecipeid_chr = td.outpatrecipeid_chr
                               AND td.outpatrecipeid_chr = i.outpatrecipeid_chr
                               AND td.itemid_chr = tg.itemid_chr(+)
                               AND td.medstoreid_chr = te.medstoreid_chr
                               AND te.medstoreid_chr = tf.medstoreid_chr(+)
                               AND (ta.pstauts_int = 2 OR ta.pstauts_int = 3)
                               AND ta.diagdr_chr = tb.empid_chr(+)
                               AND ta.patientid_chr = tc.patientid_chr(+)
                               AND te.treatemp_chr = th1.empid_chr(+)
                               AND te.sendemp_chr = th2.empid_chr(+)
                               " + strAnd);
            if (p_strOrderBy != "")
            {
                strSQL = strSQL.Append(" ) ORDER BY " + p_strOrderBy);
            }
            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL.ToString(), ref p_dtbResult);
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
        #region ���ݲ���ID�Ϳ����Ա����ж��Ƿ���ڸ�Ա��
        /// <summary>
        /// ���ݲ���ID�Ϳ����Ա����ж��Ƿ���ڸ�Ա��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDeptID"></param>
        /// <param name="m_strDeptSelfCode"></param>
        /// <param name="m_strEMPID"></param>
        /// <param name="m_strEMPName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeEmpByIDAndCode(string m_strDeptID, string m_strDeptSelfCode, out string m_strEMPID, out string m_strEMPName)
        {
            long lngRes = 0;
            DataTable m_objTable = new DataTable();
            m_strEMPID = string.Empty;
            m_strEMPName = string.Empty;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"select A.empid_chr,A.lastname_vchr from t_bse_employee A ,t_bse_deptemp B 
                              where A.empid_chr=b.empid_chr and b.deptid_chr='" + m_strDeptID + "' and A.deptcode_chr='" + m_strDeptSelfCode + "' ";
            try
            {
                //System.Data.IDataParameter[] objParaArr = null;
                //objHRPSvc.CreateDatabaseParameter(2, out objParaArr);
                //objParaArr[0].Value = m_strDeptID;
                //objParaArr[1].Value = m_strDeptSelfCode;
                //lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, objParaArr);
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                if (lngRes > 0 && m_objTable.Rows.Count > 0)
                {
                    m_strEMPID = m_objTable.Rows[0]["empid_chr"].ToString().Trim();
                    m_strEMPName = m_objTable.Rows[0]["lastname_vchr"].ToString().Trim();
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
        #region  ���ݹ����ж��Ƿ�����Ÿ�Ա��
        /// <summary>
        /// ���ݹ����ж��Ƿ�����Ÿ�Ա��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDeptID"></param>
        /// <param name="m_strDeptSelfCode"></param>
        /// <param name="m_strEMPID"></param>
        /// <param name="m_strEMPName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeEmpByEmpNo(string m_strEmpNO, out string m_strEMPID, out string m_strEMPName)
        {
            long lngRes = 0;
            DataTable m_objTable = new DataTable();
            m_strEMPID = string.Empty;
            m_strEMPName = string.Empty;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"select A.empid_chr,A.lastname_vchr from t_bse_employee A  where A.EMPNO_CHR=?";
            try
            {
                System.Data.IDataParameter[] objParaArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objParaArr);
                objParaArr[0].Value = m_strEmpNO;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, objParaArr);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                if (lngRes > 0 && m_objTable.Rows.Count > 0)
                {
                    m_strEMPID = m_objTable.Rows[0]["empid_chr"].ToString().Trim();
                    m_strEMPName = m_objTable.Rows[0]["lastname_vchr"].ToString().Trim();
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
        #region  ��ȡ�������Ƶ���Ӧ���÷�ID
        /// <summary>
        /// ��ȡ�������Ƶ���Ӧ���÷�ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strOrderID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsageIDByOrderID(string m_strOrderID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"select USAGEID_CHR from t_opr_setusage where ORDERID_VCHR='" + m_strOrderID.Trim() + "'";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
        #region  ��ȡ����ID��ȡ������Ϣ
        /// <summary>
        ///  ��ȡ����ID��ȡ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strOrderID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseHistoryByID(string m_strCaseHistoryID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT   a.casehisid_chr,
       a.modifydate_dat,
       a.patientid_chr,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.status_int,
       a.diagmain_vchr,
       a.diagmain_xml_vchr,
       a.diagcurr_vchr,
       a.diagcurr_xml_vhcr,
       a.diaghis_vchr,
       a.diaghis_xml_vchr,
       a.aidcheck_vchr,
       a.aidcheck_xml_vchr,
       a.diag_vchr,
       a.diag_xml_vchr,
       a.treatment_vchr,
       a.treatment_xml_vchr,
       a.remark_vchr,
       a.remark_xml_vchr,
       a.anaphylaxis_vchr,
       a.bodycheck_vchr,
       a.bodychrck_xml_vchr,
       a.prihis_vchr,
       a.prihis_xml_vchr,
       a.parcasehisid_chr,
       a.anaphylaxis_xml_vchr,
       a.caldept_vchr,
       a.caldept_xml_vchr, b.deptname_vchr, c.lastname_vchr, f.patientcardid_chr,
         TO_CHAR (modifydate_dat, 'yyyy-mm-dd') creatdate, d.sign_grp,
         e.lastname_vchr patientname, e.sex_chr, e.birth_dat,
         e.homeaddress_vchr, e.homephone_vchr
    FROM t_opr_outpatientcasehis a,
         t_bse_deptdesc b,
         t_bse_employee c,
         t_bse_empsign d,
         t_bse_patient e,
         t_bse_patientcard f,
         t_opr_outpatientrecipe g
   WHERE a.diagdept_chr = b.deptid_chr(+)
     AND a.diagdr_chr = c.empid_chr(+)
     AND a.diagdr_chr = d.empid_chr(+)
     AND a.patientid_chr = e.patientid_chr(+)
     AND a.patientid_chr = f.patientid_chr(+)
     AND (a.status_int <> 0 OR a.status_int IS NULL)
     and a.casehisid_chr=g.casehisid_chr
     and g.outpatrecipeid_chr='" + m_strCaseHistoryID + @"'
   ORDER BY creatdate DESC ";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
        #region  ��ȡ����ID��ȡ��Ŀ��Ϣ
        /// <summary>
        /// ��ȡ����ID��ȡ��Ŀ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strCaseHistoryID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemsInformationByID(string m_strCaseHistoryID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT   aa.*, cc.seqid_chr
    FROM (SELECT a.outpatrecipeid_chr, a.itemid_chr itemid, a.unitid_chr unit,
                 a.tolqty_dec quantity, a.unitprice_mny price,
                 a.tolprice_mny summoney, a.rowno_chr, a.usageid_chr,
                 a.freqid_chr, a.qty_dec, a.days_int,
                 a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 '' AS sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, a.dosageunit_chr,
                 b.selfdefine_int selfdefine, 1 times, b.itemipunit_chr,
                 ROUND (b.itemprice_mny / b.packqty_dec, 4) submoney,
                 b.opchargeflg_int, b.itemopcalctype_chr, a.discount_dec,
                 b.itemcode_vchr, c.usagename_vchr, d.freqname_chr,'t_tmp_outpatientpwmrecipede' fromtable
            FROM t_tmp_outpatientpwmrecipede a,
                 t_bse_chargeitem b,
                 t_bse_usagetype c,
                 t_aid_recipefreq d
           WHERE a.itemid_chr = b.itemid_chr(+)
             AND a.usageid_chr = c.usageid_chr(+)
             AND a.freqid_chr = d.freqid_chr(+)
          UNION ALL
          SELECT a.outpatrecipeid_chr, a.itemid_chr itemid, a.unitid_chr unit,
                 a.min_qty_dec quantity, a.unitprice_mny price,
                 a.tolprice_mny summoney, a.rowno_chr AS rowno_chr,
                 a.usageid_chr, '' AS freqid_chr, min_qty_dec AS qty_dec,
                 1 AS days_int, a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 a.sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, b.dosageunit_chr,
                 b.selfdefine_int selfdefine, a.times_int times, '', 1, 0,
                 b.itemopcalctype_chr, a.discount_dec, b.itemcode_vchr,
                 c.usagename_vchr, '','t_tmp_outpatientcmrecipede' fromtable
            FROM t_tmp_outpatientcmrecipede a,
                 t_bse_chargeitem b,
                 t_bse_usagetype c
           WHERE a.itemid_chr = b.itemid_chr(+) AND a.usageid_chr = c.usageid_chr(+)
          UNION ALL
          SELECT a.outpatrecipeid_chr, a.itemid_chr itemid,
                 a.itemunit_vchr unit, a.qty_dec quantity, a.price_mny price,
                 a.tolprice_mny summoney, a.rowno_chr AS rowno_chr,
                 '' AS usageid_chr, '' AS freqid_chr, 0 AS qty_dec,
                 1 AS days_int, a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 '' AS sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, b.dosageunit_chr,
                 b.selfdefine_int selfdefine, 1 times, '', 1, 0,
                 b.itemopcalctype_chr, a.discount_dec, b.itemcode_vchr, '',
                 '','t_tmp_outpatientchkrecipede' fromtable
            FROM t_tmp_outpatientchkrecipede a, t_bse_chargeitem b
           WHERE a.itemid_chr = b.itemid_chr(+)
          UNION ALL
          SELECT a.outpatrecipeid_chr, a.itemid_chr itemid,
                 a.itemunit_vchr unit, a.qty_dec quantity, a.price_mny price,
                 a.tolprice_mny summoney, a.rowno_chr AS rowno_chr,
                 '' AS usageid_chr, '' AS freqid_chr, 0 AS qty_dec,
                 1 AS days_int, a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 '' AS sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, b.dosageunit_chr,
                 b.selfdefine_int selfdefine, 1 times, '', 1, 0,
                 b.itemopcalctype_chr, a.discount_dec, b.itemcode_vchr, '',
                 '','t_tmp_outpatienttestrecipede' fromtable
            FROM t_tmp_outpatienttestrecipede a, t_bse_chargeitem b
           WHERE a.itemid_chr = b.itemid_chr(+)
          UNION ALL
          SELECT a.outpatrecipeid_chr, a.itemid_chr itemid,
                 a.itemunit_vchr unit, a.qty_dec quantity,
                 a.unitprice_mny price, a.tolprice_mny summoney,
                 a.rowno_chr AS rowno_chr, '' AS usageid_chr,
                 '' AS freqid_chr, 0 AS qty_dec, 1 AS days_int,
                 a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 '' AS sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, b.dosageunit_chr,
                 b.selfdefine_int selfdefine, 1 times, '', 1, 0,
                 b.itemopcalctype_chr, a.discount_dec, b.itemcode_vchr, '',
                 '','t_tmp_outpatientothrecipede' fromtable
            FROM t_tmp_outpatientothrecipede a, t_bse_chargeitem b
           WHERE a.itemid_chr = b.itemid_chr(+)
          UNION ALL
          SELECT a.outpatrecipeid_chr, a.itemid_chr itemid,
                 a.itemunit_vchr unit, a.qty_dec quantity, a.price_mny price,
                 a.tolprice_mny summoney, a.rowno_chr AS rowno_chr,
                 '' AS usageid_chr, '' AS freqid_chr, 0 AS qty_dec,
                 1 AS days_int, a.itemname_vchr itemname, a.itemspec_vchr DEC,
                 '' AS sumusage_vchr, b.itemopinvtype_chr invtype,
                 b.itemcatid_chr catid, b.dosageunit_chr,
                 b.selfdefine_int selfdefine, 1 times, '', 1, 0,
                 b.itemopcalctype_chr, a.discount_dec, b.itemcode_vchr, '',
                 '','t_tmp_outpatientopsrecipede' fromtable
            FROM t_tmp_outpatientopsrecipede a, t_bse_chargeitem b
           WHERE a.itemid_chr = b.itemid_chr(+)) aa,
         (SELECT *
            FROM t_opr_outpatientrecipe
           WHERE pstauts_int <> '-1' AND casehisid_chr = '" + m_strCaseHistoryID + @"') bb,
         t_opr_outpatientcasehischr cc
   WHERE aa.outpatrecipeid_chr = bb.outpatrecipeid_chr AND aa.invtype = cc.typeid_chr(+)
ORDER BY aa.outpatrecipeid_chr";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
        #region ���ݴ����Ż�ȡ������ӡ��Ϣ
        /// <summary>
        /// ���ݴ����Ż�ȡ������ӡ��Ϣ
        /// </summary>
        /// <param name="m_objPrintcipal"></param>
        /// <param name="strRecipedeID"></param>
        /// <param name="obj_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutpatientRecipeDetail(string strRecipedeID, out clsOutpatientPrintRecipe_VO obj_VO)
        {
            long lngRes = 0;
            string strSQL = @"SELECT a.outpatrecipeid_chr, a.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, a.tolqty_dec AS qty_dec, a.unitprice_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, a.usageid_chr, a.days_int,
       a.freqid_chr, d.usagename_vchr, a.desc_vchr, b.itemopinvtype_chr,
       a.dosage_dec, a.itemspec_vchr, a.qty_dec AS dosageqty, a.itemname_vchr,
       b.itemcode_vchr, f.typename_vchr, e.freqname_chr, 0 times_int,
       0 min_qty_dec1, '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       't_opr_outpatientpwmrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, a.discount_dec,
       g.mednormalname_vchr, '' itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientpwmrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_usagetype d,
       t_aid_recipefreq e,
       t_bse_medicine g
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ?
       AND b.itemopinvtype_chr = f.typeid_chr
       AND f.flag_int = 2
       AND a.usageid_chr = d.usageid_chr(+)
       AND a.freqid_chr = e.freqid_chr(+)
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, (a.qty_dec * a.times_int) AS qty_dec,
       a.unitprice_mny AS price_mny, a.tolprice_mny, a.medstoreid_chr,
       '' usageid_chr, 0 AS days_int, '' freqid_chr, d.usagename_vchr,
       '' desc_vchr, b.itemopinvtype_chr, b.dosage_dec, a.itemspec_vchr,
       0 AS dosageqty, a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
       e.freqname_chr, a.times_int, a.min_qty_dec AS min_qty_dec1,
       '' usagename_vchr, a.min_qty_dec, a.sumusage_vchr,
       't_opr_outpatientcmrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, a.discount_dec,
       g.mednormalname_vchr, '' itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientcmrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_usagetype d,
       t_aid_recipefreq e,
       t_bse_medicine g
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ? 
       AND a.itemid_chr = e.freqid_chr(+)
       AND b.itemopinvtype_chr = f.typeid_chr
       AND f.flag_int = 2
       AND a.usageid_chr = d.usageid_chr(+)
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, a.qty_dec AS qty_dec, a.unitprice_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       't_opr_outpatientothrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       g.mednormalname_vchr,a.itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientothrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_medicine g
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTCHKRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr,a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatientchkrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTTESTRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr, a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatienttestrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTOPSRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr, a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatientopsrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr";
            obj_VO = null;
            try
            {
                DataTable dtbResult = new DataTable();
                clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = strRecipedeID;
                ParamArr[1].Value = strRecipedeID;
                ParamArr[2].Value = strRecipedeID;
                ParamArr[3].Value = strRecipedeID;
                ParamArr[4].Value = strRecipedeID;
                ParamArr[5].Value = strRecipedeID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    obj_VO = new clsOutpatientPrintRecipe_VO();

                    obj_VO.objinjectArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                    obj_VO.objPRDArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                    obj_VO.objPRDArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                    decimal m_decWM = 0;
                    decimal m_decCM = 0;
                    decimal m_decOther = 0;
                    decimal m_decCheck = 0;
                    decimal m_decTest = 0;
                    decimal m_decOperation = 0;
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();

                        objtemp.m_strChargeName = dtbResult.Rows[i]["ITEMNAME_VCHR"].ToString().Trim();
                        objtemp.m_strCount = dtbResult.Rows[i]["QTY_DEC"].ToString().Trim() + dtbResult.Rows[i]["UNITID_CHR"].ToString().Trim();
                        objtemp.m_strPrice = dtbResult.Rows[i]["PRICE_MNY"].ToString().Trim();
                        objtemp.m_strSumPrice = dtbResult.Rows[i]["TOLPRICE_MNY"].ToString().Trim();
                        objtemp.m_strUnit = dtbResult.Rows[i]["UNITID_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = dtbResult.Rows[i]["FREQNAME_CHR"].ToString().Trim();
                        objtemp.m_strDosage = dtbResult.Rows[i]["DOSAGE_DEC"].ToString().Trim() + dtbResult.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                        objtemp.m_strDays = dtbResult.Rows[i]["DAYS_INT"].ToString().Trim();
                        objtemp.m_strSpec = dtbResult.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim();
                        objtemp.m_strUsage = dtbResult.Rows[i]["USAGENAME_VCHR"].ToString().Trim();
                        objtemp.m_strRowNo = dtbResult.Rows[i]["ROWNO_CHR"].ToString().Trim();
                        objtemp.m_strUsageDetail = dtbResult.Rows[i]["DESC_VCHR"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dtbResult.Rows[i]["itemopinvtype_chr"].ToString().Trim();
                        obj_VO.m_strHerbalmedicineUsage = "";
                        if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "t_opr_outpatientpwmrecipede")
                        {
                            m_decWM += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                            obj_VO.objPRDArr.Add(objtemp);
                        }
                        else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "t_opr_outpatientcmrecipede")
                        {
                            obj_VO.m_strHerbalmedicineUsage = dtbResult.Rows[i]["SUMUSAGE_VCHR"].ToString().Trim();
                            obj_VO.m_strTimes = dtbResult.Rows[i]["TIMES_INT"].ToString().Trim();
                            m_decCM += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                            obj_VO.objPRDArr2.Add(objtemp);
                        }
                        else
                        {
                            if (dtbResult.Rows[i]["TYPENAME_VCHR"].ToString().Trim() != "����" && dtbResult.Rows[i]["TYPENAME_VCHR"].ToString().Trim() != "���")
                            {
                                objtemp.m_strCount = dtbResult.Rows[i]["QTY_DEC"].ToString().Trim() + dtbResult.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                                obj_VO.objinjectArr.Add(objtemp);
                                if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTCHKRECIPEDE")
                                {
                                    m_decCheck += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTTESTRECIPEDE")
                                {
                                    m_decTest += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTOPSRECIPEDE")
                                {
                                    m_decOperation += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else
                                {
                                    m_decOther += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                            }
                        }

                    }
                    obj_VO.m_strWMedicineCost = m_decWM.ToString("0.00");
                    obj_VO.m_strZCMedicineCost = m_decCM.ToString("0.00");
                    obj_VO.m_strCureCost = ((decimal)(m_decCheck + m_decTest + m_decOperation + m_decOther)).ToString("0.00");


                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT a.outpatrecipeid_chr,
       a.patientid_chr,
       a.createdate_dat,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.pstauts_int,
       a.recipeflag_int,
       a.outpatrecipeno_vchr,
       a.paytypeid_chr,
       a.casehisid_chr,
       a.groupid_chr,
       a.type_int,
       a.confirm_int,
       a.confirmdesc_vchr,
       a.createtype_int,
       a.deptmed_int,
       a.archtakeflag_int,
       a.printed_int,
       a.chargedeptid_chr, b.name_vchr, c.lastname_vchr, d.deptname_vchr,b.BIRTH_DAT,
       e.lastname_vchr AS recordemp, h.homeaddress_vchr,h.SEX_CHR,h.IDCARD_CHR,
       h.homephone_vchr, h.govcard_chr, h.difficulty_vchr, h.insuranceid_vchr,
       k.paytypename_vchr, p.diag_vchr, j.patientcardid_chr,
       (SELECT SUM (totalsum_mny)
          FROM t_opr_outpatientrecipeinv
         WHERE outpatrecipeid_chr = ? 
  AND totalsum_mny > 0) totailmoney
  FROM t_opr_outpatientrecipe a,
       t_bse_patient b,
       t_bse_employee c,
       t_bse_deptdesc d,
       t_bse_employee e,
       t_bse_patient h,
       t_bse_patientpaytype k,
       t_bse_patientcard j,
       t_opr_outpatientcasehis p
 WHERE a.patientid_chr = b.patientid_chr(+)
   AND a.diagdr_chr = c.empid_chr(+)
   AND a.diagdept_chr = d.deptid_chr(+)
   AND a.recordemp_chr = e.empid_chr(+)
   AND a.patientid_chr = h.patientid_chr(+)
   AND a.paytypeid_chr = k.paytypeid_chr(+)
   AND a.patientid_chr = j.patientid_chr(+)
   AND a.casehisid_chr = p.casehisid_chr(+)
   AND a.outpatrecipeid_chr = ?";

            try
            {
                DataTable dtbResult = new DataTable();
                clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strRecipedeID;
                ParamArr[1].Value = strRecipedeID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    DateTime dteBirth = Convert.ToDateTime("1900-1-1");
                    if (dtbResult.Rows[0]["BIRTH_DAT"] != System.DBNull.Value)
                        dteBirth = Convert.ToDateTime(dtbResult.Rows[0]["BIRTH_DAT"].ToString());

                    if (obj_VO == null)
                    {
                        obj_VO = new clsOutpatientPrintRecipe_VO();
                        obj_VO.objinjectArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                        obj_VO.objPRDArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                        obj_VO.objPRDArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                    }
                    obj_VO.m_strAge = com.digitalwave.iCare.middletier.HIS.clsConvertDateTime.CalcAge(dteBirth);
                    obj_VO.m_strDiagDrName = dtbResult.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                    obj_VO.m_strHospitalName = "��ݸ��ɽҽԺ";
                    obj_VO.m_strPatientName = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    obj_VO.m_strPhotoNo = dtbResult.Rows[0]["HOMEPHONE_VCHR"].ToString().Trim();
                    obj_VO.m_strCardID = dtbResult.Rows[0]["PATIENTCARDID_CHR"].ToString().Trim();
                    obj_VO.m_strdiagnose = dtbResult.Rows[0]["diag_vchr"].ToString().Trim();
                    obj_VO.m_strPatientType = dtbResult.Rows[0]["PAYTYPENAME_VCHR"].ToString().Trim();
                    obj_VO.m_strDiagDeptID = dtbResult.Rows[0]["DEPTNAME_VCHR"].ToString().Trim();
                    obj_VO.m_strRecipeID = strRecipedeID;
                    obj_VO.m_strRecordEmpID = dtbResult.Rows[0]["DIAGDR_CHR"].ToString().Trim().Substring(3);//Ա��ID
                    obj_VO.m_strIDcardno = dtbResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["RECORDDATE_DAT"] != System.DBNull.Value)
                        obj_VO.m_strPrintDate = DateTime.Parse(dtbResult.Rows[0]["RECORDDATE_DAT"].ToString()).ToString("yyyy-MM-dd");
                    else
                        obj_VO.m_strPrintDate = DateTime.Now.ToString("yyyy-MM-dd");
                    obj_VO.m_strSex = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
                    obj_VO.m_strSelfPay = "";
                    obj_VO.m_strChargeUp = "";
                    obj_VO.m_strRecipePrice = "";

                    obj_VO.m_strHerbalmedicineUsage = "";
                    obj_VO.m_strAddress = dtbResult.Rows[0]["HOMEADDRESS_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["RECIPEFLAG_INT"].ToString().Trim() == "1")
                    {
                        obj_VO.m_strRecipeType = "����";
                    }
                    else if (dtbResult.Rows[0]["RECIPEFLAG_INT"].ToString().Trim() == "2")
                    {
                        obj_VO.m_strRecipeType = "����";
                    }
                    else
                    {
                        obj_VO.m_strRecipeType = "";
                    }
                    obj_VO.m_strGOVCARD = dtbResult.Rows[0]["GOVCARD_CHR"].ToString().Trim();
                    obj_VO.m_strINSURANCEID = dtbResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    obj_VO.m_strRegisterID = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    obj_VO.m_strPayType = dtbResult.Rows[0]["PAYTYPEID_CHR"].ToString().Trim();
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
        #endregion
        #region ��ȡͳ�ƿ�����Ϣ
        [AutoComplete]
        public long m_lngGetOPDeptInfo(out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = new DataTable();

            string strSQL = @"SELECT a.deptid_chr, a.deptname_vchr
  FROM t_bse_deptdesc a
  where a.category_int=0 order by a.deptname_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref m_objTable);

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
        #region ��ȡҩƷ������Ϣ
        [AutoComplete]
        public long m_lngGetMedTypeInfo(string m_strID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = new DataTable();

            string strSQL = @"SELECT   a.medicinetypename_vchr
                               FROM t_aid_medicinetype a
                               WHERE a.medicinetypeid_chr IN (" + m_strID + @")
                               ORDER BY a.medicinetypeid_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref m_objTable);

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
        #region  ��ȡҽ������Ҫ��Ϣ
        /// <summary>
        /// ��ȡҽ������Ҫ��Ϣ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strDepID"></param>
        /// <param name="m_strMedType"></param>
        /// <param name="m_strBeginTime"></param>
        /// <param name="m_strEndTime"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorUseMedInfo(string m_strDepID, string m_strDoctorID, string m_strMedType, string m_strBeginTime, string m_strEndTime, out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;

            if (m_strDepID == string.Empty)
            {
                string strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by totalmoney desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)";
                if (m_strDoctorID != string.Empty)
                {
                    strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
              and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by totalmoney desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                  (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)";
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(8, out paramArr);
                    paramArr[0].Value = m_strBeginTime;
                    paramArr[1].Value = m_strEndTime;
                    paramArr[2].Value = m_strBeginTime;
                    paramArr[3].Value = m_strEndTime;
                    paramArr[4].Value = m_strBeginTime;
                    paramArr[5].Value = m_strEndTime;
                    paramArr[6].Value = m_strBeginTime;
                    paramArr[7].Value = m_strEndTime;
                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                string strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
                AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by totalmoney desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                    (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)";
                if (m_strDoctorID != string.Empty)
                {
                    strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
              and d.medicinetypeid_chr in (" + m_strMedType + @")
              and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by totalmoney desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by totalmoney desc,f.empno_chr,e.deptname_vchr)";
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(12, out paramArr);
                    paramArr[0].Value = m_strDepID;
                    paramArr[1].Value = m_strBeginTime;
                    paramArr[2].Value = m_strEndTime;
                    paramArr[3].Value = m_strDepID;
                    paramArr[4].Value = m_strBeginTime;
                    paramArr[5].Value = m_strEndTime;
                    paramArr[6].Value = m_strDepID;
                    paramArr[7].Value = m_strBeginTime;
                    paramArr[8].Value = m_strEndTime;
                    paramArr[9].Value = m_strDepID;
                    paramArr[10].Value = m_strBeginTime;
                    paramArr[11].Value = m_strEndTime;
                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion
        #region  ��ȡҽ������Ҫ��Ϣ-������
        /// <summary>
        /// ��ȡҽ������Ҫ��Ϣ-������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strDepID"></param>
        /// <param name="m_strMedType"></param>
        /// <param name="m_strBeginTime"></param>
        /// <param name="m_strEndTime"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorUseMedInfoByQuatity(string m_strDepID, string m_strDoctorID, string m_strMedType, string m_strBeginTime, string m_strEndTime, out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;

            if (m_strDepID == string.Empty)
            {
                string strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by acount desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)";
                if (m_strDoctorID != string.Empty)
                {
                    strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
              and d.medicinetypeid_chr in (" + m_strMedType + @")
              and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by acount desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                  (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)";
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(8, out paramArr);
                    paramArr[0].Value = m_strBeginTime;
                    paramArr[1].Value = m_strEndTime;
                    paramArr[2].Value = m_strBeginTime;
                    paramArr[3].Value = m_strEndTime;
                    paramArr[4].Value = m_strBeginTime;
                    paramArr[5].Value = m_strEndTime;
                    paramArr[6].Value = m_strBeginTime;
                    paramArr[7].Value = m_strEndTime;
                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                string strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                 (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
                AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
              and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by acount desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                    (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)";
                if (m_strDoctorID != string.Empty)
                {
                    strSQL = @"SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.tolqty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientpwmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientcmrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
              and d.medicinetypeid_chr in (" + m_strMedType + @")
              and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)
                 UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.itemunit_vchr as unitid_chr , e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientopsrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
              AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.itemunit_vchr order by acount desc,f.empno_chr,e.deptname_vchr)

UNION ALL
SELECT medicinename_vchr, medspec_vchr, assistcode_chr, unitid_chr,
       deptname_vchr, empno_chr, lastname_vchr, acount, totalmoney
  FROM (SELECT   d.medicinename_vchr, d.medspec_vchr, d.assistcode_chr,
                 b.unitid_chr, e.deptname_vchr, f.empno_chr, f.lastname_vchr,
                 SUM (b.qty_dec) acount, SUM (b.tolprice_mny) totalmoney
            FROM t_opr_outpatientrecipe a,
                 t_opr_outpatientothrecipede b,
                 t_bse_chargeitem c,
                 t_bse_medicine d,
                   (SELECT a.empid_chr, a.deptid_chr,b.deptname_vchr
  FROM t_bse_deptemp a, t_bse_deptdesc b
 WHERE a.default_dept_int = 1 AND a.deptid_chr = b.deptid_chr) e,
                 t_bse_employee f
           WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
             AND b.itemid_chr = c.itemid_chr
             AND c.itemsrcid_vchr = d.medicineid_chr
             AND a.diagdr_chr = f.empid_chr
             AND a.diagdr_chr = e.empid_chr(+)
             AND a.pstauts_int = 2
             AND e.deptid_chr =?
             and d.medicinetypeid_chr in (" + m_strMedType + @")
             and a.diagdr_chr in (" + m_strDoctorID + @")
             AND a.recorddate_dat BETWEEN TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
                                      AND TO_DATE (?,
                                                   'yyyy-mm-dd HH24:mi:ss'
                                                  )
        GROUP BY f.empno_chr,
                 f.lastname_vchr,
                 e.deptname_vchr,
                 d.assistcode_chr,
                 d.medicinename_vchr,
                 d.medspec_vchr,
                 b.unitid_chr order by acount desc,f.empno_chr,e.deptname_vchr)";
                }
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(12, out paramArr);
                    paramArr[0].Value = m_strDepID;
                    paramArr[1].Value = m_strBeginTime;
                    paramArr[2].Value = m_strEndTime;
                    paramArr[3].Value = m_strDepID;
                    paramArr[4].Value = m_strBeginTime;
                    paramArr[5].Value = m_strEndTime;
                    paramArr[6].Value = m_strDepID;
                    paramArr[7].Value = m_strBeginTime;
                    paramArr[8].Value = m_strEndTime;
                    paramArr[9].Value = m_strDepID;
                    paramArr[10].Value = m_strBeginTime;
                    paramArr[11].Value = m_strEndTime;
                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion
        #region  ��ȡ����ҩƷ���ñ���
        /// <summary>
        /// ��ȡ����ҩƷ���ñ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDeptID"></param>
        /// <param name="m_strCateID"></param>
        /// <param name="m_strBeginTime"></param>
        /// <param name="m_strEndTime"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptMedFeePercentInfo(string m_strDeptID, string m_strCateID, string m_strBeginTime, string m_strEndTime, out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT n.code_vchr, n.deptname_vchr, decode (m.medtoltalmoney,null,0,m.medtoltalmoney) medtoltalmoney, n.toltalmoney
  FROM (SELECT   e.code_vchr, e.deptname_vchr,
                 SUM (b.tolfee_mny) AS medtoltalmoney
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_outpatientrecipesumde b,
                 t_bse_deptdesc e
           WHERE a.seqid_chr = b.seqid_chr(+)
             AND a.balanceflag_int = 1
             AND a.deptid_chr = e.deptid_chr(+)
             AND b.itemcatid_chr IN (" + m_strCateID + @")
             AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginTime + @"',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE ('" + m_strEndTime + @" ',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY e.code_vchr, e.deptname_vchr
        ORDER BY e.code_vchr, e.deptname_vchr) m,
       (SELECT   e.code_vchr, e.deptname_vchr,
                 SUM (b.tolfee_mny) AS toltalmoney
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_outpatientrecipesumde b,
                 t_bse_deptdesc e
           WHERE a.seqid_chr = b.seqid_chr(+)
             AND a.balanceflag_int = 1
             AND a.deptid_chr = e.deptid_chr(+)
             AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginTime + @"',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE ('" + m_strEndTime + @" ',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY e.code_vchr, e.deptname_vchr
        ORDER BY e.code_vchr, e.deptname_vchr) n
 WHERE m.code_vchr(+) = n.code_vchr
";
            if (m_strDeptID != string.Empty)
            {
                strSQL = @"SELECT n.code_vchr, n.deptname_vchr, decode (m.medtoltalmoney,null,0,m.medtoltalmoney) medtoltalmoney, n.toltalmoney
  FROM (SELECT   e.code_vchr, e.deptname_vchr,
                 SUM (b.tolfee_mny) AS medtoltalmoney
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_outpatientrecipesumde b,
                 t_bse_deptdesc e
           WHERE a.seqid_chr = b.seqid_chr(+)
             AND a.balanceflag_int = 1
             AND a.deptid_chr = e.deptid_chr(+)
             and a.deptid_chr='" + m_strDeptID + @"'
             AND b.itemcatid_chr IN (" + m_strCateID + @")
             AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginTime + @"',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE ('" + m_strEndTime + @" ',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY e.code_vchr, e.deptname_vchr
        ORDER BY e.code_vchr, e.deptname_vchr) m,
       (SELECT   e.code_vchr, e.deptname_vchr,
                 SUM (b.tolfee_mny) AS toltalmoney
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_outpatientrecipesumde b,
                 t_bse_deptdesc e
           WHERE a.seqid_chr = b.seqid_chr(+)
             AND a.balanceflag_int = 1
             AND a.deptid_chr = e.deptid_chr(+)
             and a.deptid_chr='" + m_strDeptID + @"'
             AND a.balance_dat BETWEEN TO_DATE ('" + m_strBeginTime + @"',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE ('" + m_strEndTime + @" ',
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY e.code_vchr, e.deptname_vchr
        ORDER BY e.code_vchr, e.deptname_vchr) n
 WHERE m.code_vchr(+) = n.code_vchr
";
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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

        #region  ��ȡ����ҩƷ������Ϣ
        /// <summary>
        /// ��ȡ����ҩƷ������Ϣ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strDepID"></param>
        /// <param name="m_strItemID"></param>
        /// <param name="m_strMedType"></param>
        /// <param name="m_strBeginTime"></param>
        /// <param name="m_strEndTime"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetDoctorUseMedByItemId(string m_strDepID, string m_strItemID, string m_strMedType, string m_strBeginTime, string m_strEndTime, out DataTable m_objTable)
        {
            m_objTable = new DataTable();

            m_strBeginTime += " 00:00:00";
            m_strEndTime += " 23:59:59";

            string strSQL = @"SELECT c.itemname_vchr,
                                       c.itemspec_vchr,
                                       c.itemcode_vchr,
                                       b.unitid_chr,
                                       e.deptname_vchr,
                                       f.empno_chr,
                                       f.lastname_vchr,
                                       b.price_mny,
                                       SUM(b.qty_dec) acount,
                                       SUM(b.tolprice_mny) totalmoney
                                  FROM t_opr_outpatientrecipe a,
                                       t_opr_oprecipeitemde b,
                                       t_bse_chargeitem c,
                                      -- t_bse_medicine d,
                                       (SELECT t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                          FROM t_bse_deptemp t1, t_bse_deptdesc t2
                                         WHERE t1.default_dept_int = 1
                                           AND t1.deptid_chr = t2.deptid_chr) e,
                                       t_bse_employee f
                                 WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                   AND b.itemid_chr = c.itemid_chr
                                 --  AND c.itemsrcid_vchr = d.medicineid_chr
                                   AND a.diagdr_chr = f.empid_chr
                                   AND a.diagdr_chr = e.empid_chr(+)
                                   AND a.pstauts_int in (2,3)
                                [deptid] [medicinetype] 
                                AND c.itemcode_vchr = ?
                                   AND a.recorddate_dat BETWEEN
                                       TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss') AND
                                       TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                                 GROUP BY f.empno_chr,
                                          f.lastname_vchr,
                                          e.deptname_vchr,
                                          c.itemcode_vchr,
                                          c.itemname_vchr,
                                          c.itemspec_vchr,
                                          b.price_mny,
                                          b.unitid_chr
                                 order by totalmoney desc, f.empno_chr, e.deptname_vchr";

            long lngRes = -1;
            try
            {
                if (m_strDepID != null && m_strDepID != "")
                {
                    strSQL = strSQL.Replace("[deptid]", "AND e.deptid_chr in (" + m_strDepID + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[deptid]", "");
                }

                if (m_strMedType != null && m_strMedType != "")
                {
                    strSQL = strSQL.Replace("[medicinetype]", "AND d.medicinetypeid_chr in (" + m_strMedType + ")");
                }
                else
                {
                    strSQL = strSQL.Replace("[medicinetype]", "");
                }

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = m_strItemID;
                paramArr[1].Value = m_strBeginTime;
                paramArr[2].Value = m_strEndTime;

                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
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

        #region ��ѯ�շ���Ŀ
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr">��������</param>
        /// <param name="PatType">�������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select a.itemid_chr,
       a.itemname_vchr,
       a.itemcode_vchr,
       a.itempycode_chr,
       a.itemwbcode_chr,
       a.itemsrcid_vchr,
       a.itemsrctype_int,
       a.itemspec_vchr,
       a.itemprice_mny,
       a.itemunit_chr,
       a.itemopunit_chr,
       a.itemipunit_chr,
       a.itemopcalctype_chr,
       a.itemipcalctype_chr,
       a.itemopinvtype_chr,
       a.itemipinvtype_chr,
       a.dosage_dec,
       a.dosageunit_chr,
       a.isgroupitem_int,
       a.itemcatid_chr,
       a.usageid_chr,
       a.itemopcode_chr,
       a.insuranceid_chr,
       a.selfdefine_int,
       a.packqty_dec,
       a.tradeprice_mny,
       a.poflag_int,
       a.isrich_int,
       a.opchargeflg_int,
       a.itemsrcname_vchr,
       a.itemsrctypename_vchr,
       a.itemengname_vchr,
       a.ifstop_int,
       a.pdcarea_vchr,
       a.ipchargeflg_int,
       a.insurancetype_vchr,
       a.apply_type_int,
       a.itembihctype_chr,
       a.defaultpart_vchr,
       a.itemchecktype_chr,
       a.itemcommname_vchr,
       a.ordercateid_chr,
       a.freqid_chr,
       a.inpinsurancetype_vchr,
       a.ordercateid1_chr,
       a.isselfpay_chr,
       a.itemprice_mny_old,
       a.itemprice_mny_new,
       a.keepuse_int,
       a.price_temp,
       a.itemspec_vchr1,
       a.lastchange_dat, 
								  b.ipnoqtyflag_int, b.ifstop_int, c.precent_dec, e.typename_vchr as ybtypename,  
								  round (a.itemprice_mny / a.packqty_dec,4) as submoney, b.putmedtype_int 
							 from t_bse_chargeitem a, 
                                  t_bse_medicine b,
                                  (select itemid_chr, precent_dec from t_aid_inschargeitem where copayid_chr = ?) c, 
                                  t_aid_medicaretype e 
  						    where trim(a.itemsrcid_vchr) = trim(b.medicineid_chr(+))
							  and a.ifstop_int = 0
                              and a.itemid_chr = c.itemid_chr(+) 
                              and a.inpinsurancetype_vchr = e.typeid_chr(+) 
							  and ((lower(a.itemname_vchr) like ?)
								   or (lower(a.itemcode_vchr) like ?)
								   or (lower(a.itempycode_chr) like ?)
								   or (lower(a.itemwbcode_chr) like ?)
								  )
						 order by a.itemcatid_chr";

            SQL = SQL.Replace("[FindStr]", FindStr.Trim().ToLower());

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = PatType;
                ParamArr[1].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[2].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[3].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[4].Value = FindStr.Trim().ToLower() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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
        #region ����ҩ��id���Ҵ�����Ϣ
        /// <summary>
        /// ����ҩ��id���Ҵ�����Ϣ
        /// </summary>
        /// <param name="m_objPrincipal"></param>
        /// <param name="m_strMedStoreid"></param>
        /// <param name="m_bjTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWindowInfoByMedstoreid(string m_strMedStoreid, out DataTable m_bjTable)
        {
            long lngRes = 0;
            m_bjTable = null;
            string strSQL = @"select a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, a.windowtype_int,
       a.workstatus_int, a.winproperty_int
       from t_bse_medstorewin a
       where a.medstoreid_chr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                System.Data.IDataParameter[] m_objDataParas = null;
                objHRPSvc.CreateDatabaseParameter(1, out m_objDataParas);
                m_objDataParas[0].Value = m_strMedStoreid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_bjTable, m_objDataParas);
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
        #region ����ҩ��id�ͷ������ڻ�ȡ��ҩ��ҩ��Ϣ
        /// <summary>
        ///  ����ҩ��id�ͷ������ڻ�ȡ��ҩ��ҩ��Ϣ
        /// </summary>
        /// <param name="m_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strCreateDate"></param>
        /// <param name="m_dtSendWindows"></param>
        /// <param name="m_dtWindows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataByMedStoreID(string m_strMedStoreID, string m_strCreateDate, ref List<clsWindowsInfo> m_objWindowList, ref List<clsWindowsInfo> m_objSendWindowsList)
        {
            long lngRes = 0;
            string strSQL = @"select b.lastname_vchr
     from t_opr_recipesend a, t_bse_patient b
     where a.patientid_chr = b.patientid_chr(+)
     and a.createdate_chr = ?
     and a.pstatus_int = ?
     and a.windowid_chr=?
     and a.medstoreid_chr = ?
     order by a.createdate_chr, a.serno_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                //System.Data.IDataParameter[] m_objDataParas = null;
                for (int i = 0; i < m_objWindowList.Count; i++)
                {
                    System.Data.IDataParameter[] m_objDataParas = null;
                    objHRPSvc.CreateDatabaseParameter(4, out m_objDataParas);
                    m_objDataParas[0].Value = m_strCreateDate;
                    m_objDataParas[1].Value = 1;
                    m_objDataParas[2].Value = m_objWindowList[i].m_strWindowID.Trim();
                    m_objDataParas[3].Value = m_strMedStoreID;
                    m_objWindowList[i].m_dtTable = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objWindowList[i].m_dtTable, m_objDataParas);
                }
                strSQL = @"select b.lastname_vchr
     from t_opr_recipesend a, t_bse_patient b
     where a.patientid_chr = b.patientid_chr(+)
     and a.createdate_chr = ?
     and a.pstatus_int = ?
     and a.sendwindowid_chr=?
     and a.medstoreid_chr = ?
     order by a.createdate_chr, a.serno_chr";
                for (int i = 0; i < m_objWindowList.Count; i++)
                {
                    System.Data.IDataParameter[] m_objDataParas = null;
                    objHRPSvc.CreateDatabaseParameter(4, out m_objDataParas);
                    m_objDataParas[0].Value = m_strCreateDate;
                    m_objDataParas[1].Value = 2;
                    m_objDataParas[2].Value = m_objSendWindowsList[i].m_strWindowID.Trim();
                    m_objDataParas[3].Value = m_strMedStoreID;
                    m_objSendWindowsList[i].m_dtTable = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objSendWindowsList[i].m_dtTable, m_objDataParas);
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

        #region ��ȡҽ����������Ϣ
        [AutoComplete]
        public long m_lngGetOPDoctorWorkLoadInfo(string m_strBeginTime, string m_strEndTime, string m_strDoctorID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = new DataTable();

            string strSQL = @"SELECT a.groupid_chr, a.groupname_chr, a.empno_chr, a.doctorname_chr,
       a.tolfee_mny, b.zfs, c.ffs
  FROM (SELECT   a.groupid_chr, a.groupname_chr, c.empno_chr,c.doctorid_chr,
                 c.doctorname_chr, SUM (c.tolfee_mny) tolfee_mny
            FROM t_aid_rpt_gop_def a,
                 t_aid_rpt_gop_rla b,
                 (SELECT   b.itemcatid_chr,a.doctorid_chr, e.empno_chr, a.doctorname_chr,
                           SUM (b.tolfee_mny) tolfee_mny
                      FROM t_opr_outpatientrecipeinv a,
                           t_opr_outpatientrecipesumde b,
                           t_opr_outpatientrecipe c,
                           t_opr_reciperelation d,
                           t_bse_employee e
                     WHERE a.seqid_chr = b.seqid_chr(+)
                       AND a.balanceflag_int = 1
                       AND a.outpatrecipeid_chr = d.seqid
                       AND d.outpatrecipeid_chr = c.outpatrecipeid_chr
                       AND a.doctorid_chr = e.empid_chr(+)
                       AND a.balance_dat
                              BETWEEN TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                                  AND TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                  GROUP BY b.itemcatid_chr,a.doctorid_chr, e.empno_chr, a.doctorname_chr) c
           WHERE a.groupid_chr = b.groupid_chr(+)
             AND b.typeid_chr = c.itemcatid_chr(+)
             AND a.rptid_chr = '0005'
             AND b.rptid_chr = '0005'
        GROUP BY a.groupid_chr, a.groupname_chr, c.empno_chr,c.doctorid_chr,
                 c.doctorname_chr
        ORDER BY a.groupid_chr) a,
       (SELECT   a.doctorid_chr,
                 COUNT (CASE a.status_int
                           WHEN 1
                              THEN 1
                           WHEN 3
                              THEN 1
                           WHEN 2
                              THEN -1
                        END
                       ) AS zfs
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_reciperelation b,
                 t_opr_outpatientrecipe c
           WHERE a.outpatrecipeid_chr = b.seqid
             AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
             AND a.balanceflag_int = 1
             AND c.recipeflag_int = 1
             AND a.balance_dat BETWEEN TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY a.doctorid_chr) b,
       (SELECT   a.doctorid_chr,
                 COUNT (CASE a.status_int
                           WHEN 1
                              THEN 1
                           WHEN 3
                              THEN 1
                           WHEN 2
                              THEN -1
                        END
                       ) AS ffs
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_reciperelation b,
                 t_opr_outpatientrecipe c
           WHERE a.outpatrecipeid_chr = b.seqid
             AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
             AND a.balanceflag_int = 1
             AND c.recipeflag_int = 2
             AND a.balance_dat BETWEEN TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY a.doctorid_chr) c
 WHERE a.doctorid_chr = b.doctorid_chr AND a.doctorid_chr = c.doctorid_chr";
            if (m_strDoctorID != string.Empty)
            {
                strSQL = @"SELECT a.groupid_chr, a.groupname_chr, a.empno_chr, a.doctorname_chr,
       a.tolfee_mny, b.zfs, c.ffs
  FROM (SELECT   a.groupid_chr, a.groupname_chr, c.empno_chr,c.doctorid_chr,
                 c.doctorname_chr, SUM (c.tolfee_mny) tolfee_mny
            FROM t_aid_rpt_gop_def a,
                 t_aid_rpt_gop_rla b,
                 (SELECT   b.itemcatid_chr,a.doctorid_chr, e.empno_chr, a.doctorname_chr,
                           SUM (b.tolfee_mny) tolfee_mny
                      FROM t_opr_outpatientrecipeinv a,
                           t_opr_outpatientrecipesumde b,
                           t_opr_outpatientrecipe c,
                           t_opr_reciperelation d,
                           t_bse_employee e
                     WHERE a.seqid_chr = b.seqid_chr(+)
                       AND a.balanceflag_int = 1
                       AND a.outpatrecipeid_chr = d.seqid
                       AND d.outpatrecipeid_chr = c.outpatrecipeid_chr
                       AND a.doctorid_chr = e.empid_chr(+)
                       and a.doctorid_chr in (" + m_strDoctorID + @")
                       AND a.balance_dat
                              BETWEEN TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                                  AND TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                  GROUP BY b.itemcatid_chr,a.doctorid_chr, e.empno_chr, a.doctorname_chr) c
           WHERE a.groupid_chr = b.groupid_chr(+)
             AND b.typeid_chr = c.itemcatid_chr(+)
             AND a.rptid_chr = '0005'
             AND b.rptid_chr = '0005'
        GROUP BY a.groupid_chr, a.groupname_chr, c.empno_chr,c.doctorid_chr,
                 c.doctorname_chr
        ORDER BY a.groupid_chr) a,
       (SELECT   a.doctorid_chr,
                 COUNT (CASE a.status_int
                           WHEN 1
                              THEN 1
                           WHEN 3
                              THEN 1
                           WHEN 2
                              THEN -1
                        END
                       ) AS zfs
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_reciperelation b,
                 t_opr_outpatientrecipe c
           WHERE a.outpatrecipeid_chr = b.seqid
             AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
             AND a.balanceflag_int = 1
             AND c.recipeflag_int = 1
             and a.doctorid_chr in (" + m_strDoctorID + @")
             AND a.balance_dat BETWEEN TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY a.doctorid_chr) b,
       (SELECT   a.doctorid_chr,
                 COUNT (CASE a.status_int
                           WHEN 1
                              THEN 1
                           WHEN 3
                              THEN 1
                           WHEN 2
                              THEN -1
                        END
                       ) AS ffs
            FROM t_opr_outpatientrecipeinv a,
                 t_opr_reciperelation b,
                 t_opr_outpatientrecipe c
           WHERE a.outpatrecipeid_chr = b.seqid
             AND b.outpatrecipeid_chr = c.outpatrecipeid_chr
             AND a.balanceflag_int = 1
             AND c.recipeflag_int = 2
             and a.doctorid_chr in (" + m_strDoctorID + @")
             AND a.balance_dat BETWEEN TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
                                   AND TO_DATE (?,
                                                'yyyy-mm-dd hh24:mi:ss'
                                               )
        GROUP BY a.doctorid_chr) c
 WHERE a.doctorid_chr = b.doctorid_chr AND a.doctorid_chr = c.doctorid_chr";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out paramArr);
                paramArr[0].Value = m_strBeginTime;
                paramArr[1].Value = m_strEndTime;
                paramArr[2].Value = m_strBeginTime;
                paramArr[3].Value = m_strEndTime;
                paramArr[4].Value = m_strBeginTime;
                paramArr[5].Value = m_strEndTime;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, paramArr);
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

        #region ר���鴦����Ϣ
        /// <summary>
        /// ר���鴦����Ϣ
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGroupInfo(ref DataTable dtResult)
        {
            long lngRes = 0;
            string strSQL = @"select distinct a.groupid_chr, a.groupname_vchr
                               from t_bse_groupdesc a, t_opr_outpatientrecipe b
                               where a.groupid_chr = b.groupid_chr";
            try
            {
                HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
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

        #region ������ˮ��id��ȡ���Ƶ���Ϣ
        /// <summary>
        /// ������ˮ��id��ȡ���Ƶ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strRecipeid"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTreatInfoByRecipeid(string m_strSid_int, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = new DataTable();
            string strSQL = @"select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         b.dosageunit_chr as unitid_chr, b.usageid_chr, b.tolqty_dec,
         b.unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, b.days_int, b.qty_dec, b.discount_dec,
         b.freqid_chr, b.itemname_vchr, b.dosageunit_chr, b.itemspec_vchr,
         h.usagename_vchr, k.freqname_chr, b.dosage_dec, m.medicineid_chr,
         m.medicinetypeid_chr, 't_opr_outpatientpwmrecipede' fromtable,
         e.lastname_vchr, h.usageid_chr as usageid,
         b.desc_vchr as itemusagedetail_vchr, b.unitid_chr as itemunit
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatientpwmrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype h,
         t_aid_recipefreq k,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and b.freqid_chr = k.freqid_chr
     and b.usageid_chr = f.usageid_chr
     and b.usageid_chr = h.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
union all
select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         d.dosageunit_chr as unitid_chr, d.usageid_chr, 0 as tolqty_dec,
         b.unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, 0 as days_int, b.qty_dec, b.discount_dec,
         '' as freqid_chr, b.itemname_vchr, d.dosageunit_chr, b.itemspec_vchr,
         h.usagename_vchr, '' as freqname_chr, d.dosage_dec, m.medicineid_chr,
         m.medicinetypeid_chr, 't_opr_outpatientpwmrecipede' fromtable,
         e.lastname_vchr, h.usageid_chr as usageid,
         '' as itemusagedetail_vchr, b.unitid_chr as itemunit
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatientcmrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype h,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and b.usageid_chr = f.usageid_chr
     and b.usageid_chr = h.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
union all
select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         b.itemunit_vchr as unitid_chr, '' as usageid_chr, 0 as tolqty_dec,
         0 as unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, 0 as days_int, b.qty_dec, b.discount_dec,
         '' as freqid_chr, b.itemname_vchr, b.itemunit_vchr as dosageunit_chr,
         b.itemspec_vchr, g.usagename_vchr, '' as freqname_chr, d.dosage_dec,
         m.medicineid_chr, m.medicinetypeid_chr,
         't_opr_outpatientchkrecipede' fromtable, e.lastname_vchr,
         g.usageid_chr as usageid, b.itemusagedetail_vchr,
         b.itemunit_vchr as itmeunit
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatientchkrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype g,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and d.usageid_chr = f.usageid_chr
     and g.usageid_chr = d.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
union all
select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         b.itemunit_vchr as unitid_chr, '' as usageid_chr, 0 as tolqty_dec,
         0 as unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, 0 as days_int, b.qty_dec, b.discount_dec,
         '' as freqid_chr, b.itemname_vchr, b.itemunit_vchr as dosageunit_chr,
         b.itemspec_vchr, g.usagename_vchr, '' as freqname_chr, d.dosage_dec,
         m.medicineid_chr, m.medicinetypeid_chr,
         't_opr_outpatienttestrecipede' fromtable, e.lastname_vchr,
         b.itemunit_vchr as itemunit, g.usageid_chr as usageid,
         b.itemusagedetail_vchr
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatienttestrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype g,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and d.usageid_chr = f.usageid_chr
     and g.usageid_chr = d.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
union all
select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         b.itemunit_vchr as unitid_chr, '' as usageid_chr, 0 as tolqty_dec,
         0 as unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, 0 as days_int, b.qty_dec, b.discount_dec,
         '' as freqid_chr, b.itemname_vchr, b.itemunit_vchr as dosageunit_chr,
         b.itemspec_vchr, g.usagename_vchr, '' as freqname_chr, d.dosage_dec,
         m.medicineid_chr, m.medicinetypeid_chr,
         't_opr_outpatientopsrecipede' fromtable, e.lastname_vchr,
         g.usageid_chr as usageid, b.itemusagedetail_vchr,
         b.itemunit_vchr as itemunit
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatientopsrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype g,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and d.usageid_chr = f.usageid_chr
     and g.usageid_chr = d.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
union all
select   a.pstauts_int, a.outpatrecipeid_chr, b.itemid_chr,
         b.itemunit_vchr as unitid_chr, '' as usageid_chr, 0 as tolqty_dec,
         0 as unitprice_mny, b.tolprice_mny,
         case b.rowno_chr
            when '0'
               then ''
            else b.rowno_chr
         end as rowno_chr, 0 as days_int, b.qty_dec, b.discount_dec,
         '' as freqid_chr, b.itemname_vchr, b.itemunit_vchr as dosageunit_chr,
         b.itemspec_vchr, g.usagename_vchr, '' as freqname_chr, d.dosage_dec,
         m.medicineid_chr, m.medicinetypeid_chr,
         't_opr_outpatientothrecipede' fromtable, e.lastname_vchr,
         g.usageid_chr as usageid, '' as itemusagedetail_vchr,
         b.itemunit_vchr as itemunit
    from t_opr_recipesend m,
         t_opr_recipesendentry n,
         t_opr_outpatientrecipe a,
         t_opr_outpatientothrecipede b,
         t_bse_chargeitem d,
         (select distinct usageid_chr
                     from t_opr_setusage
                    where type_int = 2 and orderid_vchr = '3') f,
         t_bse_usagetype g,
         t_bse_medicine m,
         t_bse_employee e
   where m.sid_int = n.sid_int
     and a.outpatrecipeid_chr = n.outpatrecipeid_chr
     and a.outpatrecipeid_chr = b.outpatrecipeid_chr
     and b.itemid_chr = d.itemid_chr
     and d.usageid_chr = f.usageid_chr
     and g.usageid_chr = d.usageid_chr
     and d.itemsrcid_vchr = m.medicineid_chr(+)
     and a.diagdr_chr = e.empid_chr(+)
     and m.sid_int = ?
order by rowno_chr

";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] m_objDataParam = null;
                objHRPSvc.CreateDatabaseParameter(6, out m_objDataParam);
                m_objDataParam[0].Value = m_strSid_int;
                m_objDataParam[1].Value = m_strSid_int;
                m_objDataParam[2].Value = m_strSid_int;
                m_objDataParam[3].Value = m_strSid_int;
                m_objDataParam[4].Value = m_strSid_int;
                m_objDataParam[5].Value = m_strSid_int;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, m_objDataParam);

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

        #region �޸�ҩ�����ͱ�кű�־
        //���ڽкŹ�����ĺ��˵����by dianliang.liang 2009-12-18
        //��ҩ���ڵĽкŸ�Ϊ�ؽкţ��״νк�������ҩʱ�Զ��к�
        //�к�״̬˵����
        //��called_int=1,recalled_int=0,quit_int=0����ʾ�����кţ���ҩʱ�Զ��кţ�
        //��called_int=0,recalled_int=1,quit_int=0����ʾ�ؽкţ��ڷ�ҩ������кţ�
        //��called_int=0,recalled_int=0,quit_int=1����ʾ�ѽкţ������˲���ȡҩ���ֹ������кţ���Ѹò��˷ŵ��������Ķ�������棨�����������������������ŵ���ҩ���ڴ���ҩ���е������
        //��called_int=0,recalled_int=0,quit_int=0����������������


        /// <summary>
        /// �޸�ҩ�����ͱ�кű�־����ҩ���ڵ���кţ���Ϊ�ؽкţ�
        /// </summary>
        /// <param name="m_intSid">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateRecipeSendCalledFlag2(long m_intSid)
        {
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = string.Empty;
            strSQL = @"update t_opr_recipesend a set a.called_int = 0, a.recalled_int = 1, a.quit_int=0, a.recalled_dat=sysdate where a.sid_int=?";
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_intSid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// �޸�ҩ�����ͱ�кű�־����ҩ���ڷ������ܣ�����ϸ��ο�ǰ��ĽкŹ������˵��
        /// </summary>
        /// <param name="m_intSid">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRecipeSendQuit(long m_intSid)
        {
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = string.Empty;
            strSQL = @"update t_opr_recipesend a set a.called_int = 0, a.recalled_int = 0, a.quit_int=1, a.recalled_dat=sysdate where a.sid_int=?";
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_intSid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// �޸�ҩ�����ͱ�кű�־
        /// </summary>
        /// <param name="m_intSid">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateRecipeSendCalledFlag(long m_intSid, int m_intIsReCall)
        {

            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = string.Empty;
            if (m_intIsReCall == 1)
            {
                strSQL = @"update t_opr_recipesend a set a.called_int = 1, a.recalled_int = 1,a.recalled_dat=sysdate where a.sid_int=?";
            }
            else
            {
                strSQL = @"update t_opr_recipesend a set a.called_int=1,a.recalled_dat=sysdate where a.sid_int=?";
            }
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_intSid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// �ɷ���
        /// </summary>
        /// <param name="m_intSid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateRecipeSendCalledFlag(long m_intSid)
        {

            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_opr_recipesend a set a.called_int=1 where a.sid_int=?";
            objHRPServ.CreateDatabaseParameter(1, out objValues);
            objValues[0].Value = m_intSid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
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
        /// �޸�ҩ�����ͱ�ǰ�кű�־
        /// </summary>
        /// <param name="m_intSid"></param>
        /// <param name="m_strSendWindowid"></param>
        /// <param name="m_intReCall"></param>
        /// <param name="m_blnModfilySendWid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateRecipeSendCurrentCallFlag2(long m_intSid, string m_strSendWindowid, int m_intReCall, bool m_blnModfilySendWid)
        {

            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_opr_recipesend a set a.currentcall_int=0 where a.sid_int = ? and a.sendwindowid_chr=? and (a.pstatus_int=2 or a.recalled_int=1)";
            objHRPServ.CreateDatabaseParameter(2, out objValues);
            objValues[0].Value = m_intSid;
            objValues[1].Value = m_strSendWindowid;
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes > 0)
                {

                    strSQL = string.Empty;
                    if (m_intReCall == 1)
                    {
                        if (m_blnModfilySendWid)
                        {
                            strSQL = @"update t_opr_recipesend a set a.currentcall_int = 1,a.sendwindowid_chr= ?,a.recalled_int=1,a.quit_int=0,a.recalled_dat=sysdate where a.sid_int=?";
                        }
                        else
                        {
                            strSQL = @"update t_opr_recipesend a set a.currentcall_int = 1,a.called_int=1,a.recalled_int=1,a.quit_int=0,a.recalled_dat=sysdate where a.sid_int=?";
                        }
                    }
                    else
                    {
                        if (m_blnModfilySendWid)
                        {
                            strSQL = @"update t_opr_recipesend a set a.currentcall_int = 1,a.sendwindowid_chr= ?,a.called_int=1,a.recalled_int=1,a.quit_int=0,a.recalled_dat=sysdate  where a.sid_int=?";
                        }
                        else
                        {
                            strSQL = @"update t_opr_recipesend a set a.currentcall_int = 1,a.called_int=1,a.recalled_int=1,a.quit_int=0,a.recalled_dat=sysdate  where a.sid_int=?";
                        }
                    }
                    if (m_blnModfilySendWid)
                    {

                        objHRPServ.CreateDatabaseParameter(2, out objValues);
                        objValues[0].Value = m_strSendWindowid;
                        objValues[1].Value = m_intSid;

                    }
                    else
                    {
                        objHRPServ.CreateDatabaseParameter(1, out objValues);
                        objValues[0].Value = m_intSid;
                    }
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region �շ�ʱ�Ĵ��ڷ���

        #region ����PID��ȡ���ߵ��췢ҩ��Ϣ
        /// <summary>
        /// ����PID��ȡ���ߵ��췢ҩ��Ϣ
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="medid"></param>
        /// <param name="blnCmRecipe"></param>
        /// <param name="m_objMedStoreVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetsendmedinfoBypid(string pid, string medid, bool blnCmRecipe, out clsMedStoreWindowsVo m_objMedStoreVo)
        {
            m_objMedStoreVo = null;
            long lngRes = -1;
            string SQL = @"select  a.windowid_chr, 0 as treatwindowflag_int,
                                   c.workstatus_int as treatwindowworkstatus_int,
                                   c.windowname_vchr as treatwindowname, a.pstatus_int, a.senddate_dat,
                                   a.sendemp_chr, a.treatdate_dat, a.treatemp_chr, a.autoprint_int,
                                   a.medstoreid_chr, a.sendwindowid_chr,
                                   0 as sendwindowflag_int,
                                   e.workstatus_int as sendwindowworkstatus_int,
                                   e.windowname_vchr as sendwindowname, 0 as order_int, a.autoprintyd_int 
                              from t_opr_recipesend a, t_bse_medstorewin c, t_bse_medstorewin e,
                                   t_opr_recipesendentry f
                             where a.sid_int = f.sid_int
                               and a.pstatus_int <> -1
                               and c.windowtype_int = 1
                               and c.workstatus_int = 1
                               and e.workstatus_int = 1
                               and e.windowtype_int = 0
                               and a.medstoreid_chr = c.medstoreid_chr
                               and a.windowid_chr = c.windowid_chr
                               and a.sendwindowid_chr = e.windowid_chr
                               and a.createdate_chr = ?
                               and a.patientid_chr = ?
                               and a.medstoreid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = DateTime.Now.ToString("yyyy-MM-dd");
                ParamArr[1].Value = pid;
                ParamArr[2].Value = medid;
                DataTable dtRecord = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
                objHRPSvc.Dispose();

                if (dtRecord != null && dtRecord.Rows.Count > 0)
                {
                    DataView dv = dtRecord.DefaultView;
                    if (blnCmRecipe)
                        dv.Sort = "treatwindowflag_int desc";
                    else
                        dv.Sort = "treatwindowflag_int";
                    dtRecord = dv.ToTable();
                    DataRow dr = null;
                    for (int i = 0; i < dtRecord.Rows.Count; i++)
                    {
                        dr = dtRecord.Rows[i];
                        if (blnCmRecipe && Convert.ToByte(dr["treatwindowflag_int"]) == 1)
                        {
                            m_objMedStoreVo = new clsMedStoreWindowsVo();
                            m_objMedStoreVo.m_intWindowOrderNo = Convert.ToInt32(dr["order_int"]) + 1;
                            m_objMedStoreVo.m_strWindowID = dr["windowid_chr"].ToString();
                            m_objMedStoreVo.m_strSendWindowID = dr["sendwindowid_chr"].ToString();
                            m_objMedStoreVo.m_strSendWindowName = dr["sendwindowname"].ToString();
                            break;
                        }
                        else if (!blnCmRecipe && Convert.ToByte(dr["treatwindowflag_int"]) == 0)
                        {
                            m_objMedStoreVo = new clsMedStoreWindowsVo();
                            m_objMedStoreVo.m_intWindowOrderNo = Convert.ToInt32(dr["order_int"]) + 1;
                            m_objMedStoreVo.m_strWindowID = dr["windowid_chr"].ToString();
                            m_objMedStoreVo.m_strSendWindowID = dr["sendwindowid_chr"].ToString();
                            m_objMedStoreVo.m_strSendWindowName = dr["sendwindowname"].ToString();
                            break;
                        }
                    }
                    dtRecord.Dispose();
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

        #region ���ݽ�����ҡ�ҩ����ȡר�ô�����Ϣ
        /// <summary>
        /// ���ݽ�����ҡ�ҩ����ȡר�ô�����Ϣ
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="medid"></param>
        /// <param name="winid"></param>
        /// <param name="waitno"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetespecialwin(string deptid, string medid, out clsMedStoreWindowsVo objMedStoreVo)
        {
            string winid = "";
            int waitno = 1;
            DataTable dt = new DataTable();
            long lngRes = 0;
            objMedStoreVo = null;
            string Recdate = DateTime.Now.ToString("yyyyMMdd");

            string SQL = @"select t1.windowid_chr, nvl(t3.ordermax,0) as ordermax, nvl(t3.ordercount,0) as ordercount
                             from t_bse_medstorewin t1,                                  
                                   (select windowid_chr
                                      from t_bse_medstorewindeptdef 
                                     where deptid_chr = ? and medstoreid_chr = ?) t2,                              
                                   (select a.medstoreid_chr, a.windowid_chr, b.ordercount, a.ordermax      
                                      from (select medstoreid_chr, windowid_chr, max(order_int) as ordermax
                                              from t_opr_medstorewinque
                                             where windowtype_int = 1
                                               and medstoreid_chr = ?
                                               and outpatrecipeid_chr like ? 
                                            group by medstoreid_chr, windowid_chr) a,
                                           (select medstoreid_chr, windowid_chr, count(order_int) as ordercount
                                              from t_opr_medstorewinque
                                             where windowtype_int = 1
                                               and medstoreid_chr = ? 
                                               and outpatrecipeid_chr like ? 
                                            group by medstoreid_chr, windowid_chr) b
                                     where a.medstoreid_chr = b.medstoreid_chr
                                       and a.windowid_chr = b.windowid_chr) t3
                              where t1.winproperty_int = 1
                                and t1.windowtype_int = 1
                                and t1.workstatus_int = 1
                                and t1.windowid_chr = t2.windowid_chr
                                and t1.windowid_chr = t3.windowid_chr(+)
                                and t1.medstoreid_chr = t3.medstoreid_chr(+)
                            order by ordercount";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = deptid;
                ParamArr[1].Value = medid;
                ParamArr[2].Value = medid;
                ParamArr[3].Value = Recdate + "%";
                ParamArr[4].Value = medid;
                ParamArr[5].Value = Recdate + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    winid = dt.Rows[0]["windowid_chr"].ToString();
                    waitno = Convert.ToInt32(dt.Rows[0]["ordermax"]) + 1;
                    objMedStoreVo = new clsMedStoreWindowsVo();
                    objMedStoreVo.m_intWindowOrderNo = waitno;
                    objMedStoreVo.m_strWindowID = winid; 
                    this.lngGetSpecialSendWindowInfo(medid, false, ref objMedStoreVo, false);
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

        #region ��ȡ��ǰ����ҩ���ںͷ�ҩ����(Ҳ���Ǽ��ҩ����ҩ��������ҩ������С�Ĵ��ڣ�
        /// <summary>
        /// ��ȡ��ǰ����ҩ���ںͷ�ҩ����(Ҳ���Ǽ��ҩ����ҩ��������ҩ������С�Ĵ��ڣ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID">ҩ��id</param>
        /// <param name="m_objWindowsVo">����Ҳ������ʵ���ҩ���ںͷ�ҩ���ڣ�����null</param>
        /// <param name="CheckScope">ҩ��ר�ô����Ƿ���Խ������п��Ҵ��� true ���� false ��ֹ ������0057</param>
        /// <param name="m_blnWindowType">�Ƿ��ҩ���ڱ�־��false-��true-��</param>
        /// <param name="m_blnWindowRelation">�䡢��ҩ�����Ƿ������ϵ</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetWindowIDByStorage(string storageID, out clsMedStoreWindowsVo m_objWindowsVo, bool CheckScope, bool m_blnWindowType, bool m_blnWindowRelation)
        {
            m_objWindowsVo = null;
            long lngRegs = 0;
            string strSQL = "";
            if (CheckScope)
            {
                strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.windowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 1) c,
                                   (select max(b.treatdate_dat) as lastdate, b.windowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ? 
                                      group by b.windowid_chr) d
                             where a.windowid_chr = c.windowid_chr(+)
                               and a.windowid_chr = d.windowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 1
                               and a.workstatus_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                       d.lastdate";
            }
            else
            {
                strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.windowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 1) c,
                                   (select max(b.treatdate_dat) as lastdate, b.windowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ? 
                                      group by b.windowid_chr) d
                             where a.windowid_chr = c.windowid_chr(+)
                               and a.windowid_chr = d.windowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 1
                               and a.winproperty_int = 0
                               and a.workstatus_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                        d.lastdate";
            }
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                paramArr[0].Value = strDateTime;
                paramArr[1].Value = storageID;
                paramArr[2].Value = strDateTime;
                paramArr[3].Value = storageID;
                paramArr[4].Value = storageID;
                //��ȡ��ǰҩ��������ҩ���ڵ���ҩ����
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);


                if (lngRegs > 0 && p_dtWindow.Rows.Count > 0)
                {
                    DataView dv = p_dtWindow.DefaultView;
                    if (m_blnWindowType == true)//�Ի�ȡ�ı��������
                        dv.Sort = " windowflag_int desc,numcount, lastdate";//����ҩ���ڶ��Ҷ������ٵĴ�������ǰ��
                    else
                        dv.Sort = "windowflag_int ,numcount,lastdate";//����ҩ���ڶ��Ҷ������ٵĴ�������ǰ��
                    p_dtWindow = dv.ToTable();
                    int m_intCount = p_dtWindow.Rows.Count;
                    DataRow dtRowTemp = null;
                    for (int i = 0; i < m_intCount; i++)
                    {
                        dtRowTemp = p_dtWindow.Rows[i];
                        m_objWindowsVo = new clsMedStoreWindowsVo();
                        m_objWindowsVo.m_strWindowID = dtRowTemp["WINDOWID_CHR"].ToString();
                        m_objWindowsVo.m_strWindowName = dtRowTemp["windowname_vchr"].ToString();
                        m_objWindowsVo.m_intWindowOrderNo = int.Parse(dtRowTemp["numcount"].ToString()) + 1;
                        if (m_blnWindowRelation == true)
                        {
                            this.lngGetSendWindowInfoByWindowid(storageID, m_objWindowsVo.m_strWindowID, ref m_objWindowsVo);
                        }
                        else
                        {
                            this.lngGetSendWindowInfo(storageID, CheckScope, ref m_objWindowsVo, m_blnWindowType);
                        }
                        if (m_objWindowsVo != null)//�ɹ�ȡ����ҩ������Ϣ
                            break;
                    }
                }
                else
                {
                    m_objWindowsVo = null;//����null���շѽ��棬��Ϊȡ�����κ���ҩ������Ϣ�ı�ʶ����ҩ����Ա���ҩ���������ã�
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }


        /// <summary>
        /// ������ҩ����id��ȡ��ҩ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strWindowid">��ҩ����id</param>
        /// <param name="m_objWindowsVo">��ҩ������Ϣvo,��ȡ��ҩ������Ϣʱ����null</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetSendWindowInfoByWindowid(string m_strMedStoreid, string m_strWindowid, ref clsMedStoreWindowsVo m_objWindowsVo)
        {
            long lngRegs = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
                System.Data.IDataParameter[] paramArr = null;
                strSQL = @"select windowid_chr, windowname_vchr, medstoreid_chr, 0 as windowflag_int,
       numcount
  from (select a.windowid_chr, a.windowname_vchr,
                a.medstoreid_chr, sum(decode(c.sid_int, null, 0, 1)) as numcount
           from t_bse_medstorewin a,
                (select b.sid_int, b.sendwindowid_chr
                    from t_opr_recipesend b
                   where b.createdate_chr = ?
                     and b.medstoreid_chr = ?
                     and b.pstatus_int = 2) c
          where a.windowid_chr = c.sendwindowid_chr(+)
            and a.medstoreid_chr = ?
            and a.windowtype_int = 0
            and a.workstatus_int = 1
          group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr
          order by numcount) d, t_opr_medstorewinrlt e
 where e.treatwinid_chr = ?
   and e.givewinid_chr = d.windowid_chr
 order by numcount";

                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = strDateTime;
                paramArr[1].Value = m_strMedStoreid;
                paramArr[2].Value = m_strMedStoreid;
                paramArr[3].Value = m_strWindowid;
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);

                if (lngRegs > 0 && p_dtWindow.Rows.Count > 0)
                {
                    DataRow drTemp = p_dtWindow.Rows[0];
                    if (m_objWindowsVo == null)
                        m_objWindowsVo = new clsMedStoreWindowsVo();
                    m_objWindowsVo.m_strSendWindowID = drTemp["windowid_chr"].ToString();
                    m_objWindowsVo.m_strSendWindowName = drTemp["windowname_vchr"].ToString();
                    m_objWindowsVo.m_intSendWindowOrderNo = int.Parse(drTemp["numcount"].ToString()) + 1;
                }
                else
                {
                    m_objWindowsVo = null;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }

        /// <summary>
        /// ��ȡ��ǰ��ҩ������Ϣ--����ҩ����û�й���
        /// </summary>
        /// <param name="p_objPrincipal"></param> 
        /// <param name="m_objWindowsVo">��ҩ������Ϣvo,��ȡ��ҩ������Ϣʱ����null</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetSendWindowInfo(string m_strMedStoreid, bool CheckScope, ref clsMedStoreWindowsVo m_objWindowsVo, bool m_blnWindowType)
        {
            long lngRegs = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
                System.Data.IDataParameter[] paramArr = null;
                if (CheckScope)
                {
                    strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.called_int = 0
                                        and b.pstatus_int in (1, 2)) c,
                                   (select max(b.senddate_dat) as lastdate, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 3
                                      group by b.sendwindowid_chr) d
                             where a.windowid_chr = c.sendwindowid_chr(+)
                               and a.windowid_chr = d.sendwindowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 0
                               and a.workstatus_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                      d.lastdate";
                }
                else
                {
                    strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.called_int = 0
                                        and b.pstatus_int in (1, 2)) c,
                                   (select max(b.senddate_dat) as lastdate, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 3
                                      group by b.sendwindowid_chr) d
                             where a.windowid_chr = c.sendwindowid_chr(+)
                               and a.windowid_chr = d.sendwindowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 0
                               and a.workstatus_int = 1 and a.winproperty_int = 0
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                      d.lastdate";
                }


                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                paramArr[0].Value = strDateTime;
                paramArr[1].Value = m_strMedStoreid;
                paramArr[2].Value = strDateTime;
                paramArr[3].Value = m_strMedStoreid;
                paramArr[4].Value = m_strMedStoreid;
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);

                if (lngRegs > 0 && p_dtWindow.Rows.Count > 0)
                {
                    if (m_blnWindowType == true)
                    {
                        p_dtWindow.DefaultView.Sort = "windowflag_int desc,numcount, lastdate";
                    }
                    else
                    {
                        p_dtWindow.DefaultView.Sort = " windowflag_int,numcount, lastdate";
                    }
                    p_dtWindow = p_dtWindow.DefaultView.ToTable();
                    DataRow drTemp = p_dtWindow.Rows[0];
                    if (m_objWindowsVo == null)
                    {
                        m_objWindowsVo = new clsMedStoreWindowsVo();
                    }
                    m_objWindowsVo.m_strSendWindowID = drTemp["windowid_chr"].ToString();
                    m_objWindowsVo.m_strSendWindowName = drTemp["windowname_vchr"].ToString();
                    m_objWindowsVo.m_intSendWindowOrderNo = int.Parse(drTemp["numcount"].ToString()) + 1;
                }
                else
                {
                    m_objWindowsVo = null;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }

        [AutoComplete]
        public long lngGetSpecialSendWindowInfo(string m_strMedStoreid, bool CheckScope, ref clsMedStoreWindowsVo m_objWindowsVo, bool m_blnWindowType)
        {
            long lngRegs = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
                System.Data.IDataParameter[] paramArr = null;
                if (CheckScope)
                {
                    strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.called_int = 0
                                        and b.pstatus_int in (1, 2)) c,
                                   (select max(b.senddate_dat) as lastdate, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 3
                                      group by b.sendwindowid_chr) d
                             where a.windowid_chr = c.sendwindowid_chr(+)
                               and a.windowid_chr = d.sendwindowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 0
                               and a.workstatus_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                      d.lastdate";
                }
                else
                {
                    strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.called_int = 0
                                        and b.pstatus_int in (1, 2)) c,
                                   (select max(b.senddate_dat) as lastdate, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 3
                                      group by b.sendwindowid_chr) d
                             where a.windowid_chr = c.sendwindowid_chr(+)
                               and a.windowid_chr = d.sendwindowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 0
                               and a.workstatus_int = 1 and a.winproperty_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                      d.lastdate";
                }


                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                paramArr[0].Value = strDateTime;
                paramArr[1].Value = m_strMedStoreid;
                paramArr[2].Value = strDateTime;
                paramArr[3].Value = m_strMedStoreid;
                paramArr[4].Value = m_strMedStoreid;
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);

                if (lngRegs > 0 && p_dtWindow.Rows.Count > 0)
                {
                    if (m_blnWindowType == true)
                    {
                        p_dtWindow.DefaultView.Sort = "windowflag_int desc,numcount, lastdate";
                    }
                    else
                    {
                        p_dtWindow.DefaultView.Sort = " windowflag_int,numcount, lastdate";
                    }
                    p_dtWindow = p_dtWindow.DefaultView.ToTable();
                    DataRow drTemp = p_dtWindow.Rows[0];
                    if (m_objWindowsVo == null)
                    {
                        m_objWindowsVo = new clsMedStoreWindowsVo();
                    }
                    m_objWindowsVo.m_strSendWindowID = drTemp["windowid_chr"].ToString();
                    m_objWindowsVo.m_strSendWindowName = drTemp["windowname_vchr"].ToString();
                    m_objWindowsVo.m_intSendWindowOrderNo = int.Parse(drTemp["numcount"].ToString()) + 1;
                }
                else
                {
                    m_objWindowsVo = null;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRegs;
        }

        [AutoComplete]
        public long lngOnlyGetSendWindowInfo(string m_strMedStoreid, out clsMedStoreWindowsVo m_objWindowsVo, bool m_blnWindowType)
        {
            m_objWindowsVo = null;
            long lngRegs = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HIS.clsGetServerDate objDate = new clsGetServerDate();
            string strDateTime = objDate.m_GetServerDate().ToString("yyyy-MM-dd");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.DataTable p_dtWindow = new System.Data.DataTable();
                System.Data.IDataParameter[] paramArr = null;
                strSQL = @"select  a.windowid_chr, a.windowname_vchr, a.medstoreid_chr, 0 as windowflag_int,
                                   sum(decode(c.sid_int, null, 0, 1)) as numcount, d.lastdate
                              from t_bse_medstorewin a,
                                   (select b.sid_int, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.called_int = 0
                                        and b.pstatus_int in (1, 2)) c,
                                   (select max(b.senddate_dat) as lastdate, b.sendwindowid_chr
                                       from t_opr_recipesend b
                                      where b.createdate_chr = ?
                                        and b.medstoreid_chr = ?
                                        and b.pstatus_int = 3
                                      group by b.sendwindowid_chr) d
                             where a.windowid_chr = c.sendwindowid_chr(+)
                               and a.windowid_chr = d.sendwindowid_chr(+)
                               and a.medstoreid_chr = ?
                               and a.windowtype_int = 0
                               and a.workstatus_int = 1
                             group by a.windowid_chr, a.windowname_vchr, a.medstoreid_chr,
                                      d.lastdate";

                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                paramArr[0].Value = strDateTime;
                paramArr[1].Value = m_strMedStoreid;
                paramArr[2].Value = strDateTime;
                paramArr[3].Value = m_strMedStoreid;
                paramArr[4].Value = m_strMedStoreid;
                lngRegs = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtWindow, paramArr);

                if (lngRegs > 0 && p_dtWindow.Rows.Count > 0)
                {
                    if (m_blnWindowType == true)
                    {
                        p_dtWindow.DefaultView.Sort = "windowflag_int desc,numcount, lastdate";
                    }
                    else
                    {
                        p_dtWindow.DefaultView.Sort = " windowflag_int,numcount, lastdate";
                    }
                    p_dtWindow = p_dtWindow.DefaultView.ToTable();
                    DataRow drTemp = p_dtWindow.Rows[0];
                    if (m_objWindowsVo == null)
                    {
                        m_objWindowsVo = new clsMedStoreWindowsVo();
                    }
                    m_objWindowsVo.m_strSendWindowID = drTemp["windowid_chr"].ToString();
                    m_objWindowsVo.m_strSendWindowName = drTemp["windowname_vchr"].ToString();
                    m_objWindowsVo.m_intSendWindowOrderNo = int.Parse(drTemp["numcount"].ToString()) + 1;
                }
                else
                {
                    m_objWindowsVo = null;
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
        #endregion
    }
}
