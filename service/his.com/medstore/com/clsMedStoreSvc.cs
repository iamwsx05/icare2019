using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ��ҵ�����
    /// Create by kong 2004-07-07
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedStoreSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsMedStoreSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ҩ������ҩ

        #region ��ϵͳ

        #region ���ҩƷ������Ϣ
        /// <summary>
        /// ���ҩƷ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbMedicine"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = @"select b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.IPUNIT_CHR,b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,b.PACKQTY_DEC from t_bse_medicine b  order by b.ASSISTCODE_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

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

        #region ����ҩ������п���ҩƷ
        /// <summary>
        /// ����ҩ������п���ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbMedicine"></param>
        /// <param name="strStorageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByID(out DataTable dtbMedicine, string strStorageID)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = @"select a.*,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.IPUNIT_CHR,b.UNITPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,b.PACKQTY_DEC from t_tol_medstoremedicine a, t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.AMOUNT_DEC>0 and MEDSTOREID_CHR='" + strStorageID + "' order by b.ASSISTCODE_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

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

        #region  ���ҩ��������͡�ҩ����Ϣ
        /// <summary>
        /// ���ҩ��������͡�ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strTypeName">�������������</param>
        /// <param name="intSIGN_INT">�����־��2-����,1-���,3-����</param>
        /// <param name="StorageName">ҩ������</param>
        ///  <param name="strTypeID">�������ID</param>
        ///  <param name="storageID">ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTypeAndStorage(string strTypeID, string storageID, out string strTypeName, out int intSIGN_INT, out string StorageName)
        {
            long lngRes = 0;
            strTypeName = "";
            StorageName = "";
            intSIGN_INT = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"select MEDSTOREORDTYPE_VCHR,SIGN_INT from t_aid_medstoreordtype where MEDSTOREORDTYPEID_CHR='" + strTypeID + "'";
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
            if (dt.Rows.Count > 0)
            {
                strTypeName = dt.Rows[0]["MEDSTOREORDTYPE_VCHR"].ToString().Trim();
                if (dt.Rows[0]["SIGN_INT"].ToString() != "")
                    intSIGN_INT = Convert.ToInt16(dt.Rows[0]["SIGN_INT"]);
            }
            strSQL = @"select MEDSTORENAME_VCHR from t_bse_medstore where MEDSTORETYPE_INT=1 and MEDSTOREID_CHR='" + storageID + "'";
            DataTable dtbStorage = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbStorage);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbStorage.Rows.Count > 0)
            {
                StorageName = dtbStorage.Rows[0]["MEDSTORENAME_VCHR"].ToString();
            }
            return lngRes;

        }
        #endregion

        #region �������е���ⵥ
        /// <summary>
        /// �������е���ⵥ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strTypeID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="nowPriod"></param>
        /// <param name="strStorageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrd(string strTypeID, out DataTable dtbResult, string nowPriod, string strStorageID, string strOUTFLAN)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL;
            if (nowPriod != "")
                strSQL = @"SELECT a.MEDSTOREORDID_CHR,a.MEDSTOREID_CHR,a.STORERDOCNO_CHR,a.ORDDATE_DAT,a.TOLMNY_MNY,a.MEMO_VCHR,a.CREATOR_CHR," +
                    "a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT,case when SRCTYPE_INT=3 or SRCTYPE_INT=4 then a.SRCID_CHR when SRCTYPE_INT<3 then ''  end as SRCID_CHR,a.MEDSTOREORDTYPEID_CHR,a.PSTATUS_INT,a.PERIODID_CHR, b.MEDSTORENAME_VCHR ,"
                    + "c.LASTNAME_VCHR as CREATORNAME,d.LASTNAME_VCHR as ADUITEMPNAME,e.MEDSTOREORDTYPE_VCHR,case when SRCTYPE_INT=3 then (select STORAGENAME_VCHR from T_BSE_STORAGE where a.SRCID_CHR=STORAGEID_CHR )when SRCTYPE_INT=4 then (select  MEDSTORENAME_VCHR from t_bse_medstore where a.SRCID_CHR=MEDSTOREID_CHR) end as STORAGENAME_VCHR  FROM t_opr_medstoreord a,"
                    + "t_bse_medstore b,t_bse_employee c,t_bse_employee d,t_aid_medstoreordtype e  where a.MEDSTOREID_CHR=b.MEDSTOREID_CHR(+) and a.CREATOR_CHR=c.EMPID_CHR(+) and a.ADUITEMP_CHR=d.EMPID_CHR(+) AND a.MEDSTOREORDTYPEID_CHR=e.MEDSTOREORDTYPEID_CHR(+) and a.MEDSTOREORDTYPEID_CHR='" + strTypeID + "' and a.MEDSTOREID_CHR='" + strStorageID + "' and a.OUTFLAN_INT=" + strOUTFLAN + " and  a.PERIODID_CHR='" + nowPriod + "'";
            else
                strSQL = @"SELECT a.MEDSTOREORDID_CHR,a.MEDSTOREID_CHR,a.STORERDOCNO_CHR,a.ORDDATE_DAT,a.TOLMNY_MNY,a.MEMO_VCHR,a.CREATOR_CHR," +
                    "a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT,case when SRCTYPE_INT=3 or SRCTYPE_INT=4 then a.SRCID_CHR when SRCTYPE_INT<3 then ''  end as SRCID_CHR,a.MEDSTOREORDTYPEID_CHR,a.PSTATUS_INT,a.PERIODID_CHR, b.MEDSTORENAME_VCHR ,"
                    + "c.LASTNAME_VCHR as CREATORNAME,d.LASTNAME_VCHR as ADUITEMPNAME,e.MEDSTOREORDTYPE_VCHR,case when SRCTYPE_INT=3 then (select STORAGENAME_VCHR from T_BSE_STORAGE where a.SRCID_CHR=STORAGEID_CHR) when SRCTYPE_INT=4 then (select  MEDSTORENAME_VCHR from t_bse_medstore where a.SRCID_CHR=MEDSTOREID_CHR) end as STORAGENAME_VCHR  FROM t_opr_medstoreord a,"
                    + "t_bse_medstore b,t_bse_employee c,t_bse_employee d,t_aid_medstoreordtype e  where a.MEDSTOREID_CHR=b.MEDSTOREID_CHR(+) and a.CREATOR_CHR=c.EMPID_CHR(+) and a.ADUITEMP_CHR=d.EMPID_CHR(+) AND a.MEDSTOREORDTYPEID_CHR=e.MEDSTOREORDTYPEID_CHR(+) and a.MEDSTOREORDTYPEID_CHR='" + strTypeID + "' and a.MEDSTOREID_CHR='" + strStorageID + "' and a.OUTFLAN_INT=" + strOUTFLAN;
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

        #region ���ҩ����Ϣ
        /// <summary>
        /// ���ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StoreID"></param>
        /// <param name="StoreName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreName(string StoreID, out string StoreName)
        {
            long lngRes = 0;
            StoreName = null;
            string strSQL = @"SELECT MEDSTORENAME_VCHR
							    FROM t_bse_medstore where MEDSTOREID_CHR='" + StoreID + "'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
            {
                StoreName = dt.Rows[0]["MEDSTORENAME_VCHR"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region ���ҩ����Ϣ
        /// <summary>
        /// ���ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStore(out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"SELECT MEDSTOREID_CHR, MEDSTORENAME_VCHR
							    FROM t_bse_medstore";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ���ҩ����Ϣ
        /// <summary>
        /// ���ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStore"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStore(out DataTable dtStore)
        {
            long lngRes = 0;
            dtStore = null;
            string strSQL = @"SELECT STORAGEID_CHR,STORAGENAME_VCHR
							    FROM T_BSE_STORAGE";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStore);
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

        #region  ���ҩ������Դ���ݺ�
        /// <summary>
        ///  ���ҩ������Դ���ݺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ScrNO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetScrNO(out string ScrNO)
        {
            long lngRes = 0;
            ScrNO = null;
            string strSQL = @"SELECT max(STORERDOCNO_CHR)
							    FROM T_OPR_MEDSTOREORD";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
            {
                ScrNO = dt.Rows[0][0].ToString();
            }
            return lngRes;
        }


        #endregion

        #region ��ҩ������ҩ��¼��ID��ѯҩ������ҩ��ϸ��
        /// <summary>
        /// ��ҩ������ҩ��¼��ID��ѯҩ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <param name="p_dtbResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStoreOrdDeByOrdID(string p_strID, out DataTable p_dtbResultArr, bool blCenter, string storageID)
        {
            long lngRes = 0;
            p_dtbResultArr = null;
            string strSQL = "";
            if (blCenter == true)
            {
                strSQL = @" select a.MEDSTOREORDDEID_CHR,a.MEDICINEID_CHR,a.ROWNO_CHR,a.QTY_DEC,a.SALEUNITPRICE_DEC,a.SALETOLPRICE_DEC," +
                    "a.UNITID_CHR,a.USEFULLIFE_DAT,a.SYSLOTNO_CHR,a.MEDNO_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.PRODUCTORID_CHR,c.AMOUNT_DEC  from t_opr_medstoreordde a left join T_BSE_STORAGEMEDICINE c on c.MEDICINEID_CHR=a.MEDICINEID_CHR and c.STORAGEID_CHR='" + storageID + "' and c.FLAG_INT=1,t_bse_medicine b  where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.MEDSTOREORDID_CHR='" + p_strID + "' order by ROWNO_CHR";
            }
            else
            {
                strSQL = @" select a.MEDSTOREORDDEID_CHR,a.MEDICINEID_CHR,a.ROWNO_CHR,a.QTY_DEC,a.SALEUNITPRICE_DEC,a.SALETOLPRICE_DEC," +
                    "a.UNITID_CHR,a.USEFULLIFE_DAT,a.SYSLOTNO_CHR,a.MEDNO_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.PRODUCTORID_CHR,c.CURQTY_DEC as AMOUNT_DEC  from t_opr_medstoreordde a left join T_OPR_STORAGEMEDDETAIL c on  c.MEDICINEID_CHR=a.MEDICINEID_CHR and c.SYSLOTNO_CHR=a.SYSLOTNO_CHR and c.STORAGEID_CHR='" + storageID + "' and c.FLAG_INT=1,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.MEDSTOREORDID_CHR='" + p_strID + "' order by ROWNO_CHR";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbResultArr);


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

        #region �������ⵥ
        /// <summary>
        /// �������ⵥ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="DtrStorage"></param>
        /// <param name="dtbStorageDe"></param>
        /// <param name="newid"></param>
        ///  <param name="intSIGN_INT">�����־��2-����,1-���,3-������⣬4���γ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSave(DataRow DtrStorage, DataTable dtbStorageDe, out string newid, int intSIGN_INT)
        {
            long lngRes = 0;
            newid = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            objHRPSvc.m_lngGenerateNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", out newid);
            string strSQL = "";
            string strSign = "";
            string strSrc = "";
            switch (intSIGN_INT)
            {
                case 1:
                    strSign = "1";
                    strSrc = "3";
                    break;
                case 2:
                    strSign = "2";
                    strSrc = "3";
                    break;
                case 3:
                    strSign = "1";
                    strSrc = "4";
                    break;
                case 4:
                    strSign = "2";
                    strSrc = "4";
                    break;
            }

            strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,MEMO_VCHR,CREATOR_CHR," +
                "CREATEDATE_DAT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,PERIODID_CHR,SRCID_CHR,SRCTYPE_INT,STORERDOCNO_CHR) values('" + newid + "','"
                + DtrStorage["MEDSTOREID_CHR"].ToString().Trim() + "',To_Date('" + DtrStorage["ORDDATE_DAT"].ToString().Trim() + "','yyyy-mm-dd hh24:mi:ss')," + DtrStorage["TOLMNY_MNY"].ToString().Trim() + ",'" + DtrStorage["MEMO_VCHR"].ToString().Trim() + "','"
                + DtrStorage["CREATOR_CHR"].ToString().Trim() + "',sysdate,'"
                + DtrStorage["MEDSTOREORDTYPEID_CHR"].ToString().Trim() + "',1," + strSign + ",'" + DtrStorage["PERIODID_CHR"].ToString().Trim() + "','" + DtrStorage["SRCID_CHR"].ToString() + "'," + strSrc + ",'" + DtrStorage["STORERDOCNO_CHR"].ToString().Trim() + "')";
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
            if (lngRes > 0 && dtbStorageDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbStorageDe.Rows.Count; i1++)
                {
                    string newDeid = "";
                    objHRPSvc.m_lngGenerateNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", out newDeid);
                    strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR,USEFULLIFE_DAT,MEDNO_CHR,SYSLOTNO_CHR)"
                            + " values('" + newDeid + "','" + newid + "','" + dtbStorageDe.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() + "','"
                            + dtbStorageDe.Rows[i1]["ROWNO_CHR"].ToString().Trim() + "'," + dtbStorageDe.Rows[i1]["QTY_DEC"].ToString().Trim() + "," + dtbStorageDe.Rows[i1]["SALEUNITPRICE_DEC"].ToString().Trim() + ","
                            + dtbStorageDe.Rows[i1]["SALETOLPRICE_DEC"].ToString().Trim() + ",'" + dtbStorageDe.Rows[i1]["UNITID_CHR"].ToString().Trim() + "',To_Date('" + dtbStorageDe.Rows[i1]["USEFULLIFE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + dtbStorageDe.Rows[i1]["MEDNO_CHR"].ToString().Trim() + "','" + dtbStorageDe.Rows[i1]["SYSLOTNO_CHR"].ToString().Trim() + "')";
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
                }
            }
            return lngRes;
        }
        #endregion

        #region ������ⵥ
        /// <summary>
        /// ������ⵥ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="DtrStorage"></param>
        /// <param name="dtbStorageDe"></param>
        /// <param name="newid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveOut(DataRow DtrStorage, DataTable dtbStorageDe, out string newid)
        {
            long lngRes = 0;
            newid = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            newid = objHRPSvc.m_strGetNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", 18);
            string strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,MEMO_VCHR,CREATOR_CHR," +
                "CREATEDATE_DAT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,PERIODID_CHR) values('" + newid + "','"
                + DtrStorage["MEDSTOREID_CHR"].ToString() + "',To_Date('" + DtrStorage["ORDDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')," + DtrStorage["TOLMNY_MNY"].ToString() + ",'" + DtrStorage["MEMO_VCHR"].ToString() + "','"
                + DtrStorage["CREATOR_CHR"].ToString() + "',To_Date('" + DtrStorage["CREATEDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'"
                + DtrStorage["MEDSTOREORDTYPEID_CHR"].ToString() + "',1,2,'" + DtrStorage["PERIODID_CHR"].ToString() + "')";
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
            if (lngRes > 0 && dtbStorageDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbStorageDe.Rows.Count; i1++)
                {
                    string newDeid = objHRPSvc.m_strGetNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", 18);
                    strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR)"
                        + " values('" + newDeid + "','" + newid + "','" + dtbStorageDe.Rows[i1]["MEDICINEID_CHR"].ToString() + "','" + dtbStorageDe.Rows[i1]["SYSLOTNO_CHR"].ToString() + "','"
                        + dtbStorageDe.Rows[i1]["ROWNO_CHR"].ToString() + "'," + dtbStorageDe.Rows[i1]["QTY_DEC"].ToString() + "," + dtbStorageDe.Rows[i1]["SALEUNITPRICE_DEC"].ToString() + ","
                        + dtbStorageDe.Rows[i1]["SALETOLPRICE_DEC"].ToString() + ",'" + dtbStorageDe.Rows[i1]["UNITID_CHR"].ToString() + "')";
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
                }
            }
            return lngRes;
        }
        #endregion

        #region ������ϸ
        /// <summary>
        /// ������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strOrdID">���ݺ�ID</param>
        /// <param name="tolMoney">���ݵ��ܽ��</param>
        /// <param name="ModifiyMoney">�������Ӻ�ĵ����ܽ��</param>
        /// <param name="dtbStorageDe">��ϸ����</param>
        /// <param name="strOrdDeID">������������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDe(string strOrdID, double tolMoney, DataRow dtbStorageDe, out string strOrdDeID, out double ModifiyMoney)
        {
            ModifiyMoney = 0;
            long lngRes = 0;
            strOrdDeID = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            objHRPSvc.m_lngGenerateNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", out strOrdDeID);
            string strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR,USEFULLIFE_DAT,MEDNO_CHR)"
                + " values('" + strOrdDeID + "','" + strOrdID + "','" + dtbStorageDe["MEDICINEID_CHR"].ToString() + "','"
                + dtbStorageDe["ROWNO_CHR"].ToString() + "'," + dtbStorageDe["QTY_DEC"].ToString() + "," + dtbStorageDe["SALEUNITPRICE_DEC"].ToString() + ","
                + dtbStorageDe["SALETOLPRICE_DEC"].ToString() + ",'" + dtbStorageDe["UNITID_CHR"].ToString() + "',To_Date('" + dtbStorageDe["USEFULLIFE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + dtbStorageDe["MEDNO_CHR"].ToString() + "')";
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
            if (lngRes > 0)
            {
                ModifiyMoney = tolMoney + Convert.ToDouble(dtbStorageDe["SALETOLPRICE_DEC"]);
                strSQL = @"update t_opr_medstoreord set TOLMNY_MNY=" + ModifiyMoney + "where MEDSTOREORDID_CHR='" + strOrdID + "'";
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
            }
            return lngRes;
        }

        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">��ⵥID</param>
        /// <param name="strDeID">��ⵥ��ϸID,��Ϊnullֻɾ����ϸ����</param>
        /// <param name="TolMoney">�����ܽ��</param>
        /// <param name="DelDeMoney">Ҫɾ����ϸ���ݵĽ��</param>
        [AutoComplete]
        public long m_lngDelete(string strID, string strDeID, double TolMoney, double DelDeMoney)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (strDeID != null)
            {
                strSQL = @"Delete t_opr_medstoreordde where MEDSTOREORDDEID_CHR='" + strDeID + "'";
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
                if (lngRes > 0)
                {
                    TolMoney = TolMoney - DelDeMoney;
                    strSQL = @"update t_opr_medstoreord set TOLMNY_MNY=" + TolMoney + " where MEDSTOREORDID_CHR='" + strID + "'";
                }
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

            }
            else
            {
                strSQL = @"Delete t_opr_medstoreordde where MEDSTOREORDID_CHR='" + strID + "'";
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
                if (lngRes > 0)
                {
                    strSQL = @"Delete t_opr_medstoreord where MEDSTOREORDID_CHR='" + strID + "'";
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
                }
            }
            return lngRes;
        }
        #endregion

        #region �޸�����
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="UpOrdDe">��ϸ�����У���Ϊnull�����޸�</param>
        /// <param name="UpOrd">��ⵥ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifiy(DataRow UpOrdDe, DataRow UpOrd)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (UpOrdDe == null)
            {
                strSQL = @"update t_opr_medstoreord set MEDSTOREID_CHR='" + UpOrd["MEDSTOREID_CHR"].ToString() + "',ORDDATE_DAT=To_Date('" + UpOrd["ORDDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),MEMO_VCHR='" + UpOrd["MEMO_VCHR"].ToString() + "',MEDSTOREORDTYPEID_CHR='" + UpOrd["MEDSTOREORDTYPEID_CHR"].ToString() + "',TOLMNY_MNY=" + UpOrd["TOLMNY_MNY"].ToString() + " ,SRCID_CHR='" + UpOrd["SRCID_CHR"].ToString() + "',STORERDOCNO_CHR='" + UpOrd["STORERDOCNO_CHR"].ToString() + "' where MEDSTOREORDID_CHR='" + UpOrd["MEDSTOREORDID_CHR"].ToString() + "'";
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
            }
            else
            {
                strSQL = @"update t_opr_medstoreord set MEDSTOREID_CHR='" + UpOrd["MEDSTOREID_CHR"].ToString() + "',ORDDATE_DAT=To_Date('" + UpOrd["ORDDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),MEMO_VCHR='" + UpOrd["MEMO_VCHR"].ToString() + "',MEDSTOREORDTYPEID_CHR='" + UpOrd["MEDSTOREORDTYPEID_CHR"].ToString() + "',TOLMNY_MNY=" + UpOrd["TOLMNY_MNY"].ToString() + " ,SRCID_CHR='" + UpOrd["SRCID_CHR"].ToString() + "',STORERDOCNO_CHR='" + UpOrd["STORERDOCNO_CHR"].ToString() + "'  where MEDSTOREORDID_CHR='" + UpOrd["MEDSTOREORDID_CHR"].ToString() + "'";
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
                if (lngRes > 0)
                {
                    strSQL = @"update t_opr_medstoreordde set MEDICINEID_CHR='" + UpOrdDe["MEDICINEID_CHR"].ToString() + "',MEDNO_CHR='" + UpOrdDe["MEDNO_CHR"].ToString() + "',QTY_DEC=" + UpOrdDe["QTY_DEC"].ToString() + ",SALEUNITPRICE_DEC=" + UpOrdDe["SALEUNITPRICE_DEC"].ToString() + ",SALETOLPRICE_DEC=" + UpOrdDe["SALETOLPRICE_DEC"].ToString() + ",UNITID_CHR='" + UpOrdDe["UNITID_CHR"].ToString() + "',USEFULLIFE_DAT=to_date('" + UpOrdDe["USEFULLIFE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss') where MEDSTOREORDDEID_CHR='" + UpOrdDe["MEDSTOREORDDEID_CHR"].ToString() + "'";
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
                }

            }
            return lngRes;

        }
        #endregion

        #region �����Ӧ�ĵ��ݺ��Ƿ����
        /// <summary>
        /// �����Ӧ�ĵ��ݺ��Ƿ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ordTypeName"></param>
        /// <param name="ordTypeID"></param>
        /// <param name="intFlan">0-ҩ����1-ҩ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrdTypeID(out string ordTypeID, int intFlan)
        {
            long lngRes = 0;
            ordTypeID = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = "";
            if (intFlan == 0)
                strSQL = @"SELECT MEDSTOREORDTYPEID_CHR
								FROM t_aid_medstoreordtype where trim(MEDSTORAGE_INT)=0";
            else
                strSQL = @"SELECT STORAGEORDTYPEID_CHR as MEDSTOREORDTYPEID_CHR
								FROM T_AID_STORAGEORDTYPE  where trim(MEDSTORAGE_INT)=1";
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
            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                ordTypeID = dt.Rows[0]["MEDSTOREORDTYPEID_CHR"].ToString();

            }
            return lngRes;
        }
        #endregion

        #region ��˹���
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="stroageID">ҩ��ID</param>
        /// <param name="GrearName">�����ID</param>
        /// <param name="strID">����ID</param>
        /// <param name="OrdDeTable">��ⵥ��ϸ</param>
        /// <param name="intFlan">�����־��2-����,1-���,3-������⣬4���γ���</param>
        /// <param name="blisAutoInsert">�Ƿ�Ҫ�Զ�������Ӧ����ⵥ</param>
        /// <param name="OrdTableRow">���ⵥ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAduiTemp(string strID, string stroageID, string GrearName, DataTable OrdDeTable, int intFlan, bool blisAutoInsert, DataRow OrdTableRow)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            com.digitalwave.iCare.middletier.HIS.clsMedStorageManage mange = new clsMedStorageManage();
            if (blisAutoInsert == true)
            {
                switch (intFlan)
                {
                    case 1:
                        break;
                    case 2:
                        string newID = "";
                        lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageord", "STORAGEORDID_CHR", out newID);
                        strSQL = @"insert into t_opr_storageord(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT,DOCID_VCHR,OFFERID_CHR,DEPTID_CHR,VENDORID_CHR,CREATORID_CHR,CREATEDATE_DAT,MEMO_VCHR,sign_int) 
                            VALUES('" + newID + "','" + OrdTableRow["MEDSTOREORDTYPEID_CHR"].ToString().Trim() + "','" + OrdTableRow["SRCID_CHR"].ToString().Trim() + "','" + OrdTableRow["PERIODID_CHR"].ToString().Trim() + "',sysdate," + OrdTableRow["TOLMNY_MNY"].ToString().Trim()
                            + ",1,'" + OrdTableRow["STORERDOCNO_CHR"].ToString().Trim() + "',' ','" + OrdTableRow["MEDSTOREID_CHR"].ToString().Trim() + "',' ','" + GrearName + "',sysdate"
                            + ",'ҩ���˻�ҩƷ',1)";
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
                        if (OrdDeTable.Rows.Count > 0)
                        {
                            string intStarNO = "";

                            for (int i1 = 0; i1 < OrdDeTable.Rows.Count; i1++)
                            {
                                #region ��ȡҩƷ�Ļ�����λ
                                string strUnit = "";
                                int PACKQTY = 0;
                                Double totNuber = 0;
                                DataTable dt1 = new DataTable();
                                strSQL = @"select OPUNIT_CHR,PACKQTY_DEC,OPCHARGEFLG_INT from t_bse_medicine where MEDICINEID_CHR='" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "'";
                                try
                                {
                                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
                                }
                                catch (Exception objEx)
                                {
                                    string strTmp = objEx.Message;
                                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                    bool blnRes = objLogger.LogError(objEx);
                                }
                                if (lngRes > 0 && dt1.Rows.Count > 0)
                                {
                                    if (dt1.Rows[0]["OPCHARGEFLG_INT"].ToString() == "1")
                                    {
                                        if (dt1.Rows[0]["OPUNIT_CHR"].ToString() == "")
                                        {
                                            throw new Exception("ҩƷ" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "�ġ�ҩƷ������λ��������");
                                        }
                                        else
                                        {
                                            strUnit = dt1.Rows[0]["OPUNIT_CHR"].ToString();
                                        }
                                        if (dt1.Rows[0]["PACKQTY_DEC"].ToString() == "" || dt1.Rows[0]["PACKQTY_DEC"].ToString() == "0")
                                        {
                                            throw new Exception("ҩƷ" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "�ġ���װ����������");
                                        }
                                        else
                                        {
                                            PACKQTY = int.Parse(dt1.Rows[0]["PACKQTY_DEC"].ToString());
                                            try
                                            {
                                                totNuber = Double.Parse(OrdDeTable.Rows[i1]["QTY_DEC"].ToString()) / PACKQTY;
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        totNuber = Double.Parse(OrdDeTable.Rows[i1]["QTY_DEC"].ToString());
                                        strUnit = dt1.Rows[0]["OPUNIT_CHR"].ToString();
                                    }

                                }
                                else
                                {
                                    throw new Exception("ҩƷ" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "�ġ�ҩƷ������λ���򡮰�װ����������");
                                }
                                #endregion
                                objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out intStarNO);

                                strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,BUYUNITPRICE_MNY,WHOLESALEUNITPRICE_MNY,
                           DISCOUNT_DEC,BUYTOLPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,SALEUNITPRICE_MNY) 
                          VALUES('" + intStarNO + "','" + newID + "','" + OrdTableRow["MEDSTOREORDTYPEID_CHR"].ToString().Trim() + "','" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() + "',sysdate,'" + OrdDeTable.Rows[i1]["ROWNO_CHR"].ToString().Trim() + "','" + strUnit + "','" + OrdDeTable.Rows[i1]["MEDNO_CHR"].ToString().Trim() + "',To_Date('" + OrdDeTable.Rows[i1]["USEFULLIFE_DAT"].ToString().Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + ",'" + OrdDeTable.Rows[i1]["PRODUCTORID_CHR"].ToString().Trim() + "'," + totNuber.ToString().Trim() + "," + OrdDeTable.Rows[i1]["SALEUNITPRICE_DEC"].ToString().Trim() + ",0,1," + OrdDeTable.Rows[i1]["SALETOLPRICE_DEC"].ToString().Trim() + ",' '," + PACKQTY
                                    + ",0,0)";
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

                            }
                        }
                        break;
                    case 3:
                        string newID1 = "";
                        m_lngSave(OrdTableRow, OrdDeTable, out newID1, 3);
                        break;
                    case 4:
                        m_lngSave(OrdTableRow, OrdDeTable, out newID1, 4);
                        break;
                }
            }

            for (int i1 = 0; i1 < OrdDeTable.Rows.Count; i1++)
            {
                if (intFlan == 1 || intFlan == 3)
                {
                    mange.m_lnghMedInit(OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString().Trim(), OrdDeTable.Rows[i1]["MEDNO_CHR"].ToString().Trim(),
                        stroageID, Double.Parse(OrdDeTable.Rows[i1]["QTY_DEC"].ToString().Trim()), OrdDeTable.Rows[i1]["UNITID_CHR"].ToString().Trim(), 0,
                        0, 0, OrdDeTable.Rows[i1]["USEFULLIFE_DAT"].ToString().Trim(),
                        OrdDeTable.Rows[i1]["PRODUCTORID_CHR"].ToString().Trim(), "1", OrdDeTable.Rows[i1]["SYSLOTNO_CHR"].ToString());
                }
                else
                {
                    mange.m_lngReduceMedQty(OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString().Trim(), OrdDeTable.Rows[i1]["MEDNO_CHR"].ToString().Trim(), OrdDeTable.Rows[i1]["SYSLOTNO_CHR"].ToString().Trim(), stroageID.Trim(), "1", Double.Parse(OrdDeTable.Rows[i1]["QTY_DEC"].ToString().Trim()), OrdDeTable.Rows[i1]["UNITID_CHR"].ToString().Trim(), 1);
                }
                #region ���õĴ���
                //				DataTable dtbstroage=new DataTable();
                //				strSQL=@"select AMOUNT_DEC from t_tol_medstoremedicine where MEDSTOREID_CHR='"+stroageID+"' and MEDICINEID_CHR='"+OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString()+"'";
                //				try
                //				{
                //					lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbstroage);
                //					
                //				}
                //				catch(Exception objEx)
                //				{
                //					string strTmp=objEx.Message;
                //					com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //					bool blnRes = objLogger.LogError(objEx);
                //				}
                //				if(dtbstroage.Rows.Count>0)
                //				{
                //					int Tolstrage=Convert.ToInt32(dtbstroage.Rows[0]["AMOUNT_DEC"])+Convert.ToInt32(OrdDeTable.Rows[i1]["QTY_DEC"]);
                //					strSQL=@"update t_tol_medstoremedicine set AMOUNT_DEC="+Tolstrage+" where MEDSTOREID_CHR='"+stroageID+"' and MEDICINEID_CHR='"+OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString()+"'";
                //					try
                //					{
                //						lngRes = objHRPSvc.DoExcute(strSQL);
                //						
                //					}
                //					catch(Exception objEx)
                //					{
                //						string strTmp=objEx.Message;
                //						com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //						bool blnRes = objLogger.LogError(objEx);
                //					}
                //				}
                //				else
                //				{
                //					strSQL=@"insert into  t_tol_medstoremedicine(MEDSTOREID_CHR,MEDICINEID_CHR,AMOUNT_DEC) values('"+stroageID+"','"+OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString()+"',"+OrdDeTable.Rows[i1]["QTY_DEC"].ToString()+")";
                //					try
                //					{
                //						lngRes = objHRPSvc.DoExcute(strSQL);
                //						
                //					}
                //					catch(Exception objEx)
                //					{
                //						string strTmp=objEx.Message;
                //						com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //						bool blnRes = objLogger.LogError(objEx);
                //					}
                //				}
                #endregion
            }
            if (lngRes > 0)
            {
                strSQL = @"update t_opr_medstoreord set ADUITEMP_CHR='" + GrearName + "',ADUITDATE_DAT=To_Date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss'),PSTATUS_INT=2 where MEDSTOREORDID_CHR='" + strID + "'";
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
            }
            return lngRes;
        }
        #endregion

        #region  ���ҩ����ҩ���͡�ҩ����Ϣ
        /// <summary>
        /// ���ҩ����ҩ���͡�ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strTypeName">��ҩ��������</param>
        /// <param name="dtbStorage">ҩ����Ϣ</param>
        /// <param name="strTypeID">��ҩ����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTypeAndStorageOut(string strTypeID, out string strTypeName, out DataTable dtbStorage)
        {
            long lngRes = 0;
            strTypeName = "";
            dtbStorage = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"select MEDSTOREORDTYPE_VCHR from t_aid_medstoreordtype where SIGN_INT=2 and MEDSTOREORDTYPEID_CHR='" + strTypeID + "'";
            DataTable dtbType = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbType);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbType.Rows.Count > 0)
            {
                strTypeName = dtbType.Rows[0]["MEDSTOREORDTYPE_VCHR"].ToString().Trim();
            }
            strSQL = @"select STORAGEID_CHR,MEDSTORENAME_VCHR from t_bse_medstore where MEDSTORETYPE_INT=1";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbStorage);


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

        #region ������еĳ�ҩ����
        /// <summary>
        /// ������еĳ�ҩ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <param name="nowPriod">������ID</param>
        /// <param name="strTypeID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdOut(string strTypeID, out DataTable dtbResult, string nowPriod)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL;
            if (nowPriod != "")
                strSQL = @"SELECT a.MEDSTOREORDID_CHR,a.MEDSTOREID_CHR,a.ORDDATE_DAT,a.TOLMNY_MNY,a.MEMO_VCHR,a.CREATOR_CHR," +
                    "a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT,a.MEDSTOREORDTYPEID_CHR,a.PSTATUS_INT,a.PERIODID_CHR,b.MEDSTORENAME_VCHR,"
                    + "c.LASTNAME_VCHR as CREATORNAME,d.LASTNAME_VCHR as ADUITEMPNAME FROM t_opr_medstoreord a,"
                    + "t_bse_medstore b,t_bse_employee c,t_bse_employee d where a.MEDSTOREID_CHR=b.MEDSTOREID_CHR(+) and a.CREATOR_CHR=c.EMPID_CHR(+) and a.ADUITEMP_CHR=d.EMPID_CHR(+) AND a.MEDSTOREORDTYPEID_CHR='" + strTypeID + "'"
                    + " and a.OUTFLAN_INT=2 and PERIODID_CHR='" + nowPriod + "'";
            else
                strSQL = @"SELECT a.MEDSTOREORDID_CHR,a.MEDSTOREID_CHR,a.ORDDATE_DAT,a.TOLMNY_MNY,a.MEMO_VCHR,a.CREATOR_CHR," +
                    "a.CREATEDATE_DAT,a.ADUITEMP_CHR,a.ADUITDATE_DAT,a.MEDSTOREORDTYPEID_CHR,a.PSTATUS_INT,a.PERIODID_CHR,b.MEDSTORENAME_VCHR,"
                    + "c.LASTNAME_VCHR as CREATORNAME,d.LASTNAME_VCHR as ADUITEMPNAME  FROM t_opr_medstoreord a,"
                    + "t_bse_medstore b,t_bse_employee c,t_bse_employee d where a.MEDSTOREID_CHR=b.MEDSTOREID_CHR(+) and a.CREATOR_CHR=c.EMPID_CHR(+) and a.ADUITEMP_CHR=d.EMPID_CHR(+) AND a.MEDSTOREORDTYPEID_CHR='" + strTypeID + "'"
                    + " and a.OUTFLAN_INT=2";

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

        #region ��˹���(��ҩ)
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="stroageID">ҩ��ID</param>
        /// <param name="strID">����ID</param>
        /// <param name="GrearName">�����ID</param>
        /// <param name="OrdDeTable">��ⵥ��ϸ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAduiTempOut(string strID, string stroageID, string GrearName, DataTable OrdDeTable)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            for (int i1 = 0; i1 < OrdDeTable.Rows.Count; i1++)
            {
                DataTable dtbstroage = new DataTable();
                strSQL = @"select AMOUNT_DEC from t_tol_medstoremedicine where MEDSTOREID_CHR='" + stroageID + "' and MEDICINEID_CHR='" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "'";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbstroage);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dtbstroage.Rows.Count > 0)
                {
                    int Tolstrage = Convert.ToInt32(dtbstroage.Rows[0]["AMOUNT_DEC"]) - Convert.ToInt32(OrdDeTable.Rows[i1]["QTY_DEC"]);
                    if (Tolstrage >= 0)
                    {
                        strSQL = @"update t_tol_medstoremedicine set AMOUNT_DEC=" + Tolstrage + " where MEDSTOREID_CHR='" + stroageID + "' and MEDICINEID_CHR='" + OrdDeTable.Rows[i1]["MEDICINEID_CHR"].ToString() + "'";
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
                    }
                    else
                    {
                        lngRes = -2;
                        return lngRes;
                    }
                }
                else
                {
                    lngRes = -2;
                    return lngRes;
                }
            }
            if (lngRes > 0)
            {
                strSQL = @"update t_opr_medstoreord set ADUITEMP_CHR='" + GrearName + "',ADUITDATE_DAT=To_Date('" + DateTime.Now + "','yyyy-mm-dd hh24:mi:ss'),PSTATUS_INT=2 where MEDSTOREORDID_CHR='" + strID + "'";
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
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�Ŀ��
        /// <summary>
        /// ����ҩƷ�Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strstogeId"></param>
        /// <param name="Medid"></param>
        /// <param name="StoreNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllStorage(string strstogeId, string Medid, out int StoreNumber)
        {
            long lngRes = 0;
            StoreNumber = -1;
            string strSQL = @"select AMOUNT_DEC FROM T_TOL_MEDSTOREMEDICINE WHERE MEDSTOREID_CHR='" + strstogeId + "' AND MEDICINEID_CHR='" + Medid + "'";
            DataTable dtResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtResult.Rows.Count > 0)
            {
                StoreNumber = Convert.ToInt32(dtResult.Rows[0]["AMOUNT_DEC"]);
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region ����ҩ������ҩ��¼��
        /// <summary>
        /// ����ҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ��������¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStoreOrd(clsMedStoreOrd_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstoreord
										  (medstoreordid_chr, medstoreid_chr, orddate_dat,
										   tolqty_dec, tolmny_mny, periodid_chr, memo_vchr, creator_chr,
										   createdate_dat, aduitemp_chr,
										   aduitdate_dat, acctemp_chr,
										   acctdate_dat, srcid_chr, srctype_int,
										   medstoreordtypeid_chr, pstatus_int
										  )
								  VALUES ('" + p_objItem.m_strMedStoreOrdID.Trim() + "', '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "', TO_DATE ('" + p_objItem.m_strOrdDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"				  " + p_objItem.m_decTolQty.ToString() + ", " + p_objItem.m_decTolMny.ToString() + ", '" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "', '" + p_objItem.m_strMemo.Trim() + "', '" + p_objItem.m_objCreator.strEmpID.Trim() + "',";
            strSQL += @"				  TO_DATE ('" + p_objItem.m_strCreateDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "',";
            strSQL += @"				  TO_DATE ('" + p_objItem.m_strAduitDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "',";
            strSQL += @"				  TO_DATE ('" + p_objItem.m_strAcctDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_strSrcID.Trim() + "', " + p_objItem.m_intSrcType.ToString() + ",";
            strSQL += @"				  '" + p_objItem.m_objMedStoreOrdType.m_strMedStoreOrdTypeID.Trim() + "', " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"				 )";

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

        #region �޸�ҩ������ҩ��¼��
        /// <summary>
        /// �޸�ҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ��������¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoreOrd(clsMedStoreOrd_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreord
								 SET orddate_dat = TO_DATE ('" + p_objItem.m_strOrdDate.Trim() + "', 'yyyy-mm-dd hh24:mi;ss'),";
            strSQL += @"			 tolqty_dec = " + p_objItem.m_decTolQty.ToString() + ", ";
            strSQL += @"			 tolmny_mny = " + p_objItem.m_decTolMny.ToString() + ", ";
            strSQL += @"			 periodid_chr = '" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "', ";
            strSQL += @"			 memo_vchr = '" + p_objItem.m_strMemo.Trim() + "', ";
            strSQL += @"			 creator_chr = '" + p_objItem.m_objCreator.strEmpID.Trim() + "', ";
            strSQL += @"			 createdate_dat = TO_DATE ('" + p_objItem.m_strCreateDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 aduitemp_chr = '" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 aduitdate_dat = TO_DATE ('" + p_objItem.m_strAduitDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 acctemp_chr = '" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 acctdate_dat = TO_DATE ('" + p_objItem.m_strAcctDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 srcid_chr = '" + p_objItem.m_strSrcID.Trim() + "',";
            strSQL += @"			 srctype_int = " + p_objItem.m_intSrcType.ToString() + ", ";
            strSQL += @"			 medstoreordtypeid_chr = '" + p_objItem.m_objMedStoreOrdType.m_strMedStoreOrdTypeID.Trim() + "', ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medstoreordid_chr) = '" + p_objItem.m_strMedStoreOrdID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "'";

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

        #region �޸�ҩ������ҩ��¼��״̬��־
        /// <summary>
        /// �޸�ҩ������ҩ��¼��״̬��־
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="strID">ҩ��������¼��ID</param>
        /// <param name="intStatus">״̬��־</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoreOrdStatus(string strID, int intStatus)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreord
								 SET pstatus_int = " + intStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medstoreordid_chr) = '" + strID.Trim() + "' ";

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

        #region ɾ��ҩ������ҩ��¼��
        /// <summary>
        /// ɾ��ҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��������¼��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedStoreOrd(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_opr_medstoreord
							   WHERE TRIM(medstoreordid_chr) = '" + p_strID.Trim() + "' ";

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

        #region ���ҩ������ҩ��¼��
        /// <summary>
        /// ҩ������ҩ��¼�����
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ��������¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAduitMedStoreOrd(clsMedStoreOrd_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreord
								 SET aduitemp_chr = '" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 aduitdate_dat = TO_DATE ('" + p_objItem.m_strAduitDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medstoreordid_chr) = '" + p_objItem.m_strMedStoreOrdID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "'";

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

        #region ���ҩ������ҩ��¼������Ŀ��
        /// <summary>
        /// ҩ������ҩ��¼����˺���Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��������¼��ID</param>
        /// <param name="p_intFlag">��ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeStorageAfterAduitMedStoreOrd(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_MEDSTOREORDADUIT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "ordid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());

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

        #region ҩ������ҩ��¼������
        /// <summary>
        /// ҩ������ҩ��¼������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ��������¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAcctMedStoreOrd(clsMedStoreOrd_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreord
								 SET acctemp_chr = '" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 acctdate_dat = TO_DATE ('" + p_objItem.m_strAcctDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medstoreordid_chr) = '" + p_objItem.m_strMedStoreOrdID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "'";

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

        #region ҩ������ҩ��¼�����ʺ��������
        /// <summary>
        /// ҩ������ҩ��¼�����ʺ��������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��������¼��ID</param>
        /// <param name="p_intFlag">��ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeFinAfterAcctMedStoreOrd(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_MEDSTOREORDACCT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "ordid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());

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

        #region ����ҩ������ҩ��ϸ����¼
        /// <summary>
        /// ����ҩ������ҩ��ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ������ҩ��ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStoreOrdDe(clsMedStoreOrdDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstoreordde
										  (medstoreorddeid_chr, medstoreordid_chr, medicineid_chr,
										   syslotno_chr, rowno_chr, orddate_dat,
										   qty_dec, saleunitprice_dec, saletolprice_dec, unitid_chr
										  )
								   VALUES ('" + p_objItem.m_strMedStoreOrdDeID.Trim() + "', '" + p_objItem.m_strMedStoreOrdID.Trim() + "', '" + p_objItem.m_objMedicine.m_strMedicineID.Trim() + "', ";
            strSQL += @"				   '" + p_objItem.m_strSysLotNo.Trim() + "', '" + p_objItem.m_strRowNo.Trim() + "', TO_DATE ('" + p_objItem.m_strOrdDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"				   " + p_objItem.m_decQty.ToString() + ", " + p_objItem.m_decSaleUnitPrice.ToString() + ", " + p_objItem.m_decSaleTolPrice.ToString() + ", '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "' ";
            strSQL += @"				  )";

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

        #region �޸�ҩ������ҩ��ϸ����¼
        /// <summary>
        /// �޸�ҩ������ҩ��ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ������ҩ��ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoreOrdDe(clsMedStoreOrdDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreordde
								 SET rowno_chr = '" + p_objItem.m_strRowNo.Trim() + "', ";
            strSQL += @"			 unitid_chr = '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "', ";
            strSQL += @"			 qty_dec = " + p_objItem.m_decQty.ToString() + ", ";
            strSQL += @"			 saleunitprice_dec = " + p_objItem.m_decSaleUnitPrice.ToString() + ", ";
            strSQL += @"			 saletolprice_dec = " + p_objItem.m_decSaleTolPrice.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medstoreorddeid_chr) = '" + p_objItem.m_strMedStoreOrdDeID.Trim() + "'";

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

        #region ɾ��ҩ������ҩ��ϸ����¼
        /// <summary>
        /// ɾ��ҩ������ҩ��ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ������ҩ��ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedStoreOrdDe(string p_strID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE t_opr_medstoreordde
							   WHERE TRIM(medstoreorddeid_chr) = '" + p_strID.Trim() + "'";

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

        #region ģ����ѯҩ������ҩ��¼��
        /// <summary>
        /// ģ����ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByAny(string p_strSQL, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstoreord
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreOrd_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreOrd_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_objMedStoreOrdType = new clsMedStoreOrdType_VO();
                            p_objResultArr[i].m_objPeriod = new clsPeriod_VO();
                            p_objResultArr[i].m_objCreator = new clsEmployeeVO();
                            p_objResultArr[i].m_objAduitEmp = new clsEmployeeVO();
                            p_objResultArr[i].m_objAcctEmp = new clsEmployeeVO();
                            p_objResultArr[i].m_strMedStoreOrdID = dtbResult.Rows[i]["medstoreordid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            p_objResultArr[i].m_strOrdDate = dtbResult.Rows[i]["orddate_dat"].ToString().Trim();
                            p_objResultArr[i].m_decTolQty = Convert.ToDecimal(dtbResult.Rows[i]["tolqty_dec"].ToString().Trim());
                            p_objResultArr[i].m_decTolMny = Convert.ToDecimal(dtbResult.Rows[i]["tolmny_mny"].ToString().Trim());
                            p_objResultArr[i].m_objPeriod.m_strPeriodID = dtbResult.Rows[i]["periodid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strStartDate = dtbResult.Rows[i]["startdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strEndDate = dtbResult.Rows[i]["enddate_dat"].ToString().Trim();
                            p_objResultArr[i].m_strMemo = dtbResult.Rows[i]["memo_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objCreator.strEmpID = dtbResult.Rows[i]["creator_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCreateDate = dtbResult.Rows[i]["createdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objAduitEmp.strEmpID = dtbResult.Rows[i]["aduitemp_chr"].ToString().Trim();
                            p_objResultArr[i].m_strAduitDate = dtbResult.Rows[i]["aduitdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objAcctEmp.strEmpID = dtbResult.Rows[i]["acctemp_chr"].ToString().Trim();
                            p_objResultArr[i].m_strAcctDate = dtbResult.Rows[i]["acctdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_strSrcID = dtbResult.Rows[i]["srcid_chr"].ToString().Trim();
                            p_objResultArr[i].m_intSrcType = Convert.ToInt32(dtbResult.Rows[i]["srctype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStoreOrdType.m_strMedStoreOrdTypeID = dtbResult.Rows[i]["medstoreordtypeid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStoreOrdType.m_strMedStoreOrdTypeName = dtbResult.Rows[i]["medstoreordtype_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStoreOrdType.m_intSign = Convert.ToInt32(dtbResult.Rows[i]["sign_int"].ToString().Trim());
                            p_objResultArr[i].m_intPStatus = Convert.ToInt32(dtbResult.Rows[i]["pstatus_int"].ToString().Trim());
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

        #region ����¼��ID��ѯҩ������ҩ��¼��
        /// <summary>
        /// ����¼��ID��ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByID(string p_strID, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];

            string strSQL = @" WHERE TRIM(medstoreordid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯҩ������ҩ��¼��
        /// <summary>
        /// ���������Ͳ�ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByOrdType(string p_strID, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];
            string strSQL = @" WHERE TRIM(medstoreordtypeid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩ����ѯҩ������ҩ��¼��
        /// <summary>
        /// ��ҩ����ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByMedStore(string p_strID, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];
            string strSQL = @" WHERE TRIM(medstoreid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ������ʱ���ѯҩ������ҩ��¼��
        /// <summary>
        /// ������ʱ���ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByDate(string p_strStartDate, string p_strEndDate, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];

            string strSQL = @" WHERE orddate_dat >= TO_DATE ('" + p_strStartDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
            strSQL += @"	     AND orddate_dat <= TO_DATE ('" + p_strEndDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯҩ������ҩ��¼��
        /// <summary>
        /// �������ڲ�ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByPeriod(string p_strID, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];
            string strSQL = @" WHERE TRIM(periodid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��״̬��־��ѯҩ������ҩ��¼��
        /// <summary>
        /// ��״̬��־��ѯҩ������ҩ��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_intStatus">״̬��ʶ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdByStatus(int p_intStatus, out clsMedStoreOrd_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrd_VO[0];
            string strSQL = @" WHERE pstatus_int = " + p_intStatus.ToString() + " ";

            lngRes = m_lngGetMedStoreOrdByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ������ҩ��ϸ��
        /// <summary>
        /// ģ����ѯҩ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdDeByAny(string p_strSQL, out clsMedStoreOrdDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdDe_VO[0];
            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstoreordde
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreOrdDe_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreOrdDe_VO();
                            p_objResultArr[i].m_objMedicine = new clsMedicine_VO();
                            p_objResultArr[i].m_objUnit = new clsUnit_VO();

                            p_objResultArr[i].m_strMedStoreOrdDeID = dtbResult.Rows[i]["medstoreorddeid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strMedStoreOrdID = dtbResult.Rows[i]["medstoreordid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineEngName = dtbResult.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strPYCode = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strWBCode = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strSysLotNo = dtbResult.Rows[i]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i].m_strRowNo = dtbResult.Rows[i]["rowno_chr"].ToString().Trim();
                            p_objResultArr[i].m_strOrdDate = dtbResult.Rows[i]["orddate_dat"].ToString().Trim();
                            p_objResultArr[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty_dec"].ToString().Trim());
                            p_objResultArr[i].m_decSaleUnitPrice = Convert.ToDecimal(dtbResult.Rows[i]["saleunitprice_dec"].ToString().Trim());
                            p_objResultArr[i].m_decSaleTolPrice = Convert.ToDecimal(dtbResult.Rows[i]["saletolprice_dec"].ToString().Trim());
                            p_objResultArr[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objUnit.m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
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

        #region ��ҩ������ҩ��¼��ID��ѯҩ������ҩ��ϸ��
        /// <summary>
        /// ��ҩ������ҩ��¼��ID��ѯҩ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdDeByOrdID(string p_strID, out clsMedStoreOrdDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdDe_VO[0];
            string strSQL = @" WHERE TRIM(medstoreordid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯҩ������ҩ��ϸ��
        /// <summary>
        /// ��ҩƷ��ѯҩ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdDeByMedicine(string p_strID, out clsMedStoreOrdDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdDe_VO[0];
            string strSQL = @" WHERE TRIM(medicineid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ȡ��ǰ����ҩ������ҩ��¼��ID
        /// <summary>
        /// ��ȡ��ǰ����ҩ������ҩ��¼��ID
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strID = objHRPSvc.m_strGetNewID("t_opr_medstoreord", "medstoreordid_chr", 18);


                if (p_strID != null)
                {
                    if (Convert.ToInt32(p_strID.Trim()) <= 0)
                    {
                        int ID = Convert.ToInt32(p_strID.Trim()) + 1;
                        p_strID = ID.ToString("000000000000000000");
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

        #region ��ȡ��ǰ����ҩ����ʾ��ҩ��ϸ��ID
        /// <summary>
        /// ��ȡ��ǰ����ҩ����ʾ��ҩ��ϸ��ID
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��ϸ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdDeID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strID = objHRPSvc.m_strGetNewID("t_opr_medstoreordde", "medstoreorddeid_chr", 18);


                if (p_strID != null)
                {
                    if (Convert.ToInt32(p_strID.Trim()) <= 0)
                    {
                        int ID = Convert.ToInt32(p_strID.Trim()) + 1;
                        p_strID = ID.ToString("000000000000000000");
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

        #endregion

        #region ҩ����ҩ����

        #region ��ϵͳ�ķ���

        #region ��ȡ��ǰ����ҩ����ҩ���뵥ID
        /// <summary>
        /// ��ȡ��ǰ����ҩ����ҩ���뵥ID
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���뵥ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strID = objHRPSvc.m_strGetNewID("t_opr_medstoremedappl", "MEDAPPLID_CHR", 14);


                if (p_strID != null)
                {
                    if (Convert.ToInt32(p_strID.Trim()) <= 0)
                    {
                        int ID = Convert.ToInt32(p_strID.Trim()) + 1;
                        p_strID = ID.ToString("00000000000000");
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

        #region  ���ҩ�⡢ҩ����Ϣ
        /// <summary>
        /// ���ҩ�⡢ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbStorage">ҩ����Ϣ��</param>
        /// <param name="dtbStore">ҩ����Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStoreAndStorage(out DataTable dtbStorage, out DataTable dtbStore)
        {
            long lngRes = 0;
            dtbStore = null;
            dtbStorage = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"select STORAGEID_CHR,STORAGENAME_VCHR from t_bse_storage";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbStorage);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select MEDSTOREID_CHR,MEDSTORENAME_VCHR from t_bse_medstore where MEDSTORETYPE_INT=1";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbStore);


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

        #region ���ҩ����ҩ�����¼��
        /// <summary>
        /// ���ҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <param name="storageID">����ҩ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplAll(out DataTable p_objResultArr, string storageID)
        {
            long lngRes = 0;
            p_objResultArr = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"SELECT a.*, b.medstorename_vchr,c.storagename_vchr, d.lastname_vchr" +
                            " FROM t_opr_medstoremedappl a,t_bse_medstore b,t_bse_storage c,t_bse_employee d" +
                            " WHERE a.applmedstoreid_chr = b.medstoreid_chr(+) AND a.medstorageid_chr = c.storageid_chr(+) AND a.creator_chr = d.empid_chr(+) and a.PSTATUS_INT!=2 and APPLMEDSTOREID_CHR='" + storageID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_objResultArr);


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

        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="DtrAppl">���뵥��</param>
        /// <param name="dtbApplDe">��ϸ������</param>
        /// <param name="newid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngApplSave(DataRow DtrAppl, DataTable dtbApplDe, out string newid)
        {
            long lngRes = 0;
            newid = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            objHRPSvc.m_lngGenerateNewID("t_opr_medstoremedappl", "MEDAPPLID_CHR", out newid);
            string strSQL = @"insert into t_opr_medstoremedappl(MEDAPPLID_CHR,APPLMEDSTOREID_CHR,APPLDATE_DAT,MEMO_VCHR,CREATOR_CHR,MEDSTORAGEID_CHR," +
                "PSTATUS_INT) values('" + newid + "','" + DtrAppl["APPLMEDSTOREID_CHR"].ToString() + "',To_Date('" + DtrAppl["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'"
                + DtrAppl["MEMO_VCHR"].ToString() + "','" + DtrAppl["CREATOR_CHR"].ToString() + "','" + DtrAppl["MEDSTORAGEID_CHR"].ToString() + "',1)";
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
            if (lngRes > 0 && dtbApplDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbApplDe.Rows.Count; i1++)
                {
                    string newDeid = "";
                    objHRPSvc.m_lngGenerateNewID("t_opr_medstoremedapplde", "MEDAPPLDEID_CHR", out newDeid);
                    strSQL = @"insert into t_opr_medstoremedapplde(MEDAPPLID_CHR,MEDAPPLDEID_CHR,MEDICINEID_CHR,ROWNO_CHR,UNITID_CHR,QTY_DEC)"
                        + " values('" + newid + "','" + newDeid + "','" + dtbApplDe.Rows[i1]["MEDICINEID_CHR"].ToString() + "','"
                        + dtbApplDe.Rows[i1]["ROWNO_CHR"].ToString() + "','" + dtbApplDe.Rows[i1]["UNITID_CHR"].ToString() + "',"
                        + dtbApplDe.Rows[i1]["QTY_DEC"].ToString() + ")";
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
                }
            }
            return lngRes;
        }
        #endregion


        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="DtrAppl">���뵥��</param>
        /// <param name="dtbApplDe">��ϸ������</param>
        /// <param name="newid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngApplSave(DataTable dtbApp, DataTable dtbApplDe, out string newid)
        {
            long lngRes = 0;
            newid = null;
            DataRow DtrAppl = dtbApp.Rows[0];
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            objHRPSvc.m_lngGenerateNewID("t_opr_medstoremedappl", "MEDAPPLID_CHR", out newid);
            string strSQL = @"insert into t_opr_medstoremedappl(MEDAPPLID_CHR,APPLMEDSTOREID_CHR,APPLDATE_DAT,MEMO_VCHR,CREATOR_CHR,MEDSTORAGEID_CHR," +
                "PSTATUS_INT) values('" + newid + "','" + DtrAppl["APPLMEDSTOREID_CHR"].ToString() + "',To_Date('" + DtrAppl["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'"
                + DtrAppl["MEMO_VCHR"].ToString() + "','" + DtrAppl["CREATOR_CHR"].ToString() + "','" + DtrAppl["MEDSTORAGEID_CHR"].ToString() + "',1)";
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
            if (lngRes > 0 && dtbApplDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbApplDe.Rows.Count; i1++)
                {
                    string newDeid = "";
                    objHRPSvc.m_lngGenerateNewID("t_opr_medstoremedapplde", "MEDAPPLDEID_CHR", out newDeid);
                    strSQL = @"insert into t_opr_medstoremedapplde(MEDAPPLID_CHR,MEDAPPLDEID_CHR,MEDICINEID_CHR,ROWNO_CHR,UNITID_CHR,QTY_DEC)"
                        + " values('" + newid + "','" + newDeid + "','" + dtbApplDe.Rows[i1]["MEDICINEID_CHR"].ToString() + "','"
                        + dtbApplDe.Rows[i1]["ROWNO_CHR"].ToString() + "','" + dtbApplDe.Rows[i1]["UNITID_CHR"].ToString() + "',"
                        + dtbApplDe.Rows[i1]["QTY_DEC"].ToString() + ")";
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
                }
            }
            return lngRes;
        }
        #endregion

        #region �������뵥��ID��ȡ������ϸ����
        /// <summary>
        /// �������뵥��ID��ȡ������ϸ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbApplDe">����������ϸ</param>
        /// <param name="strID">���뵥ID</param>
        /// <param name="strSTORAGEID">ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplDeById(string strID, out DataTable dtbApplDe, string strSTORAGEID)
        {
            long lngRes = 0;
            dtbApplDe = null;
            string strSQL = @"SELECT a.*,b.ASSISTCODE_CHR, b.MEDICINENAME_VCHR, b.MEDSPEC_VCHR ,c.AMOUNT_DEC as TOLFINANCE_DEC FROM t_opr_medstoremedapplde a left join  t_bse_storagemedicine c 
    on c.storageid_chr='" + strSTORAGEID + "' and a.medicineid_chr = c.medicineid_chr  and c.flag_int = 1, t_bse_medicine b WHERE a.medicineid_chr = b.medicineid_chr and a.MEDAPPLID_CHR='" + strID + "' order by ROWNO_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbApplDe);

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

        #region �޸�����
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="RowApplDe"></param>
        /// <param name="RowAppl"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifiyAppl(DataRow RowApplDe, DataRow RowAppl)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (RowApplDe != null)
            {
                strSQL = @"update t_opr_medstoremedapplde  set MEDICINEID_CHR='" + RowApplDe["MEDICINEID_CHR"].ToString() + "',UNITID_CHR='" + RowApplDe["UNITID_CHR"].ToString() + "',QTY_DEC=" + RowApplDe["QTY_DEC"].ToString() + "  where MEDAPPLDEID_CHR='" + RowApplDe["MEDAPPLDEID_CHR"].ToString() + "'";
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
            }
            strSQL = @"update t_opr_medstoremedappl set APPLMEDSTOREID_CHR='" + RowAppl["APPLMEDSTOREID_CHR"].ToString() + "',APPLDATE_DAT=To_Date('" + RowAppl["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),MEMO_VCHR='" + RowAppl["MEMO_VCHR"].ToString() + "',MEDSTORAGEID_CHR='" + RowAppl["MEDSTORAGEID_CHR"].ToString() + "' where MEDAPPLID_CHR='" + RowAppl["MEDAPPLID_CHR"].ToString() + "'";
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
            return lngRes;

        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDeId">��ϸID</param>
        /// <param name="strId">���뵥ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleAppl(string strDeId, string strId)
        {
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (strDeId != null)
            {
                strSQL = @"delete  t_opr_medstoremedapplde where MEDAPPLDEID_CHR='" + strDeId + "'";
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
            }
            else
            {
                strSQL = @"delete  t_opr_medstoremedapplde where MEDAPPLID_CHR='" + strId + "'";
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
                strSQL = @"delete  t_opr_medstoremedappl where MEDAPPLID_CHR='" + strId + "'";
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
            }
            return lngRes;

        }
        #endregion

        #region �ύ���뵥
        /// <summary>
        /// �ύ���뵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPutinAppll(string strDeId)
        {
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (strDeId != null)
            {
                strSQL = @"update  T_OPR_MEDSTOREMEDAPPL set PSTATUS_INT=3 where MEDAPPLID_CHR='" + strDeId + "'";
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
            }
            return lngRes;

        }
        #endregion

        #region �����뵥����һ����ϸ
        /// <summary>
        /// �����뵥����һ����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strId">���뵥ID</param>
        /// <param name="RowDe">��ϸ����</param>
        /// <param name="newDeid">������ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddApplDe(string strId, DataRow RowDe, out string newDeid)
        {
            long lngRes = 0;
            newDeid = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            objHRPSvc.m_lngGenerateNewID("t_opr_medstoremedapplde", "MEDAPPLDEID_CHR", out newDeid);
            string strSQL = @"insert into t_opr_medstoremedapplde(MEDAPPLID_CHR,MEDAPPLDEID_CHR,MEDICINEID_CHR,ROWNO_CHR,UNITID_CHR,QTY_DEC)"
                + " values('" + strId + "','" + newDeid + "','" + RowDe["MEDICINEID_CHR"].ToString() + "','"
                + RowDe["ROWNO_CHR"].ToString() + "','" + RowDe["UNITID_CHR"].ToString() + "',"
                + RowDe["QTY_DEC"].ToString() + ")";
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
            return lngRes;

        }
        #endregion

        #region �Զ�������ҩ���뵥
        /// <summary>
        /// �Զ�������ҩ���뵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult">�������ݱ�</param>
        /// <param name="stroageID">ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAutoGetMedAppl(out DataTable dtbResult, string stroageID)
        {
            long lngRes = 0;
            dtbResult = null;
            dtbResult = new DataTable();
            string strSQL = @"select f.MEDSTOREID_CHR,f.MEDICINEID_CHR,f.ASSISTCODE_CHR,f.PYCODE_CHR,f.WBCODE_CHR,f.LOWLIMIT_DEC,f.PLANQTY_DEC,f.UNITID_CHR,
                                     f.medstorename_vchr, 
									 f.medicinename_vchr, f.medspec_vchr,
									 case when f.amount_dec is null then 0 when f.amount_dec is not null then f.amount_dec end as  amount_dec  from (SELECT a.MEDSTOREID_CHR,a.MEDICINEID_CHR,a.LOWLIMIT_DEC,a.PLANQTY_DEC,a.UNITID_CHR,
                                     b.medstorename_vchr, 
									 c.medicinename_vchr, c.medspec_vchr,
									 e.amount_dec,c.ASSISTCODE_CHR,c.PYCODE_CHR,c.WBCODE_CHR
								FROM t_bse_medstorelimit a left join T_BSE_STORAGEMEDICINE e on e.FLAG_INT=1 and e.STORAGEID_CHR='" + stroageID + "' and e.MEDICINEID_CHR=a.MEDICINEID_CHR," + "t_bse_medstore b,t_bse_medicine c WHERE a.medstoreid_chr = b.medstoreid_chr AND a.medicineid_chr = c.medicineid_chr and a.MEDSTOREID_CHR='" + stroageID + "') f where f.amount_dec<=f.LOWLIMIT_DEC or f.amount_dec is null";
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

        #endregion

        #region ����ҩ����ҩ�����¼��
        /// <summary>
        /// ����ҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ����ҩ�����¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedAppl(clsMedStoreMedAppl_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstoremedappl
										  (medapplid_chr, applmedstoreid_chr, appldate_dat,
										   memo_vchr, creator_chr, medstorageid_chr, pstatus_int
										  )
								   VALUES ('" + p_objItem.m_strMedApplID.Trim() + "', '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "', TO_DATE ('" + p_objItem.m_strApplDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"				   '" + p_objItem.m_strMemo.Trim() + "', '" + p_objItem.m_objCreator.strEmpID.Trim() + "', '" + p_objItem.m_objStorage.m_strStroageID.Trim() + "', " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"				  )";

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

        #region �޸�ҩ����ҩ�����¼��
        /// <summary>
        /// �޸�ҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ����ҩ�����¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedAppl(clsMedStoreMedAppl_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoremedappl
								 SET appldate_dat = TO_DATE ('" + p_objItem.m_strApplDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 memo_vchr = '" + p_objItem.m_strMemo.Trim() + "', ";
            strSQL += @"			 creator_chr = '" + p_objItem.m_objCreator.strEmpID.Trim() + "', ";
            strSQL += @"		     medstorageid_chr = '" + p_objItem.m_objStorage.m_strStroageID.Trim() + "', ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"       WHERE TRIM(medapplid_chr) = '" + p_objItem.m_strMedApplID.Trim() + "'";

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

        #region ɾ��ҩ����ҩ�����¼��
        /// <summary>
        /// ɾ��ҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedAppl(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_opr_medstoremedappl
							   WHERE TRIM(medapplid_chr) = '" + p_strID.Trim() + "'";

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

        #region ����ҩ����ҩ������ϸ����¼
        /// <summary>
        /// ����ҩ����ҩ������ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ����ҩ������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedApplDe(clsMedStoreMedApplDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstoremedapplde
										  (medapplid_chr, medappldeid_chr, syslotno_chr, medicineid_chr,
										   rowno_chr, appldate_dat, unitid_chr, qty_dec
										  )
								   VALUES ('" + p_objItem.m_strMedApplID.Trim() + "', '" + p_objItem.m_strMedApplDeID.Trim() + "', '" + p_objItem.m_strSysLotNo.Trim() + "', '" + p_objItem.m_objMedicine.m_strMedicineID.Trim() + "', ";
            strSQL += @"				   '" + p_objItem.m_strRowNo.Trim() + "', TO_DATE ('" + p_objItem.m_strApplDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "', " + p_objItem.m_decQty.ToString() + " ";
            strSQL += @"				  )";

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

        #region �޸�ҩ����ҩ������ϸ����¼
        /// <summary>
        /// �޸�ҩ����ҩ������ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ����ҩ������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedApplDe(clsMedStoreMedApplDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoremedapplde
								 SET rowno_chr = '" + p_objItem.m_strRowNo.Trim() + "', ";
            strSQL += @"			 unitid_chr = '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "', ";
            strSQL += @"			 qty_dec = " + p_objItem.m_decQty.ToString() + " ";
            strSQL += @"	   WHERE TRIM(medappldeid_chr) = '" + p_objItem.m_strMedApplDeID.Trim() + "'";

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

        #region ɾ��ҩ����ҩ������ϸ����¼
        /// <summary>
        /// ɾ��ҩ����ҩ������ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��ϸ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedApplDe(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_opr_medstoremedapplde
							   WHERE TRIM(medappldeid_chr) = '" + p_strID.Trim() + "'";

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

        #region ģ����ѯҩ����ҩ�����¼��
        /// <summary>
        /// ģ����ѯҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByAny(string p_strSQL, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstoremedappl
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreMedAppl_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreMedAppl_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_objStorage = new clsStorage_VO();
                            p_objResultArr[i].m_objCreator = new clsEmployeeVO();

                            p_objResultArr[i].m_strMedApplID = dtbResult.Rows[i]["medapplid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["applmedstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            p_objResultArr[i].m_strApplDate = dtbResult.Rows[i]["appldate_dat"].ToString().Trim();
                            p_objResultArr[i].m_strMemo = dtbResult.Rows[i]["memo_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objCreator.strEmpID = dtbResult.Rows[i]["creator_chr"].ToString().Trim();
                            p_objResultArr[i].m_objCreator.strLastName = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objStorage.m_strStroageID = dtbResult.Rows[i]["medstorageid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objStorage.m_strStroageName = dtbResult.Rows[i]["storagename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_intPStatus = Convert.ToInt32(dtbResult.Rows[i]["pstatus_int"].ToString().Trim());
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

        #region ����¼��ID��ѯҩ����ҩ�����¼��
        /// <summary>
        /// ����¼��ID��ѯҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByID(string p_strID, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            string strSQL = " WHERE TRIM(medapplid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩ����ѯҩ����ҩ�����¼��
        /// <summary>
        /// ��ҩ����ѯҩ����ҩ�����¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByMedStore(string p_strID, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            string strSQL = " WHERE TRIM(applmedstoreid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ������ҩ���ѯҩ����ҩ���뵥
        /// <summary>
        /// ������ҩ���ѯҩ����ҩ���뵥
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ⷿID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByStorage(string p_strID, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            string strSQL = " WHERE TRIM(medstorageid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ������ʱ���ѯҩ����ҩ���뵥
        /// <summary>
        /// ������ʱ���ѯҩ����ҩ���뵥
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByDate(string p_strStartDate, string p_strEndDate, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            string strSQL = " WHERE appldate_dat >= TO_DATE('" + p_strStartDate.Trim() + "','yyyy-mm-dd hh24:mi:ss'),appldate_dat <= TO_DATE('" + p_strEndDate.Trim() + "','yyyy-mm-dd hh24:mi:ss')";

            lngRes = m_lngGetMedApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��״̬��־��ѯҩ����ҩ���뵥
        /// <summary>
        /// ��״̬��־��ѯҩ����ҩ���뵥
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_intStatus">״̬��ʶ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplByStatus(int p_intStatus, out clsMedStoreMedAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedAppl_VO[0];

            string strSQL = " WHERE pstatus_int = " + p_intStatus.ToString() + " ";

            lngRes = m_lngGetMedApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ����ҩ������ϸ��
        /// <summary>
        /// ģ����ѯҩ����ҩ������ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplDeByAny(string p_strSQL, out clsMedStoreMedApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedApplDe_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstoremedapplde
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreMedApplDe_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreMedApplDe_VO();
                            p_objResultArr[i].m_objMedicine = new clsMedicine_VO();
                            p_objResultArr[i].m_objUnit = new clsUnit_VO();

                            p_objResultArr[i].m_strMedApplID = dtbResult.Rows[i]["medapplid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strMedApplDeID = dtbResult.Rows[i]["medappldeid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strSysLotNo = dtbResult.Rows[i]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineEngName = dtbResult.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strPYCode = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strWBCode = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strRowNo = dtbResult.Rows[i]["rowno_chr"].ToString().Trim();
                            p_objResultArr[i].m_strApplDate = dtbResult.Rows[i]["appldate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objUnit.m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
                            p_objResultArr[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty_dec"].ToString().Trim());
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

        #region ����ҩ�����¼��ID��ѯҩ����ҩ������ϸ��
        /// <summary>
        /// ����ҩ�����¼��ID��ѯҩ����ҩ������ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��¼��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplDeByApplID(string p_strID, out clsMedStoreMedApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedApplDe_VO[0];

            string strSQL = " WHERE TRIM(medapplid_chr) = '" + p_strID.Trim() + "' ";
            lngRes = m_lngGetMedApplDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯҩ����ҩ������ϸ��
        /// <summary>
        /// ��ҩƷ��ѯҩ����ҩ������ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplDeByMedicine(string p_strID, out clsMedStoreMedApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreMedApplDe_VO[0];

            string strSQL = " WHERE TRIM(medicineid_chr) = '" + p_strID.Trim() + " ";
            lngRes = m_lngGetMedApplDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �Զ������ҩ���뵥
        /// <summary>
        /// �Զ����ɲɹ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAutoCalcMedAppl(string p_strID, out clsMedStoreMedApplDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsMedStoreMedApplDe_VO[0];
            DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT a.*, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int,
									 c.medicinename_vchr, c.medspec_vchr, d.unitname_chr,
									 a.lowlimit_dec - e.amount_dec AS qty
								FROM t_bse_medstorelimit a,
									 t_bse_medstore b,
									 t_bse_medicine c,
									 t_aid_unit d,
									 t_tol_medstoremedicine e
							   WHERE a.medstoreid_chr = b.medstoreid_chr
								 AND a.medicineid_chr = c.medicineid_chr
								 AND a.unitid_chr = d.unitid_chr(+)
							     AND a.medstoreid_chr = e.medstoreid_chr
								 AND a.medicineid_chr = e.medicineid_chr
								 AND e.amount_dec < a.lowlimit_dec ";
            strSQL += @" AND a.medstoreid_chr = '" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);



                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    p_objResult = new clsMedStoreMedApplDe_VO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        p_objResult[i] = new clsMedStoreMedApplDe_VO();
                        p_objResult[i].m_objMedicine = new clsMedicine_VO();
                        p_objResult[i].m_objUnit = new clsUnit_VO();
                        int RowNo = i + 1;
                        p_objResult[i].m_strRowNo = RowNo.ToString("0000");
                        p_objResult[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                        p_objResult[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                        p_objResult[i].m_objMedicine.m_strMedSpec = dtbResult.Rows[i]["medspec_vchr"].ToString().Trim();
                        p_objResult[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                        p_objResult[i].m_objUnit.m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
                        p_objResult[i].m_strSysLotNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                        p_objResult[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty"].ToString().Trim());
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

        #endregion

        #region ҩ���̵�

        #region ��ϵͳ
        #region ������е��̵�����
        /// <summary>
        /// ������е��̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStorData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckStore(out DataTable dtStorData)
        {
            long lngRes = 0;
            dtStorData = null;
            string strSQL = @"select a.*,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.OPUNIT_CHR,b.UNITPRICE_MNY,c.MEDSTORENAME_VCHR from t_tol_medstoremedicine a,t_bse_medicine b,t_bse_medstore c where a.MEDICINEID_CHR=b.MEDICINEID_CHR  and a.MEDSTOREID_CHR=c.MEDSTOREID_CHR";
            try
            {
                dtStorData = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorData);

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

        #region �Զ����ɳ���ⵥ
        /// <summary>
        /// �Զ����ɳ���ⵥ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="dtStorCheckData">�Զ����ɵ��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAutoGreat(DataTable dtStorCheckData)
        {
            long lngRes = 0;
            string newid;
            string newDeid;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            //com.digitalwave.iCare.middletier.HIS.clsHisBase  HisBase=new clsHisBase();
            DateTime nowData = System.DateTime.Now.Date;//HisBase.s_GetServerDate().Date;
            string strSQL = @"select * from t_bse_period";
            DataTable periodTable = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref periodTable);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes == 0 || periodTable.Rows.Count == 0)
                return -3;
            string nowperodID = "";
            for (int i1 = 0; i1 < periodTable.Rows.Count; i1++)
            {
                if (nowData >= Convert.ToDateTime(periodTable.Rows[i1]["STARTDATE_DAT"]) && nowData <= Convert.ToDateTime(periodTable.Rows[i1]["ENDDATE_DAT"]))
                    nowperodID = (string)periodTable.Rows[i1]["PERIODID_CHR"];
            }
            if (nowperodID == "")
                return -3;

            for (int i1 = 0; i1 < dtStorCheckData.Rows.Count; i1++)
            {
                newid = objHRPSvc.m_strGetNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", 18);
                strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,MEMO_VCHR,CREATOR_CHR," +
                    "CREATEDATE_DAT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,PERIODID_CHR) values('" + newid + "','"
                    + dtStorCheckData.Rows[i1]["MEDSTOREID_CHR"].ToString() + "',To_Date('" + dtStorCheckData.Rows[i1]["ORDDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')," + dtStorCheckData.Rows[i1]["TOLMNY_MNY"].ToString() + ",'" + dtStorCheckData.Rows[i1]["MEMO_VCHR"].ToString() + "','"
                    + dtStorCheckData.Rows[i1]["CREATOR_CHR"].ToString() + "',To_Date('" + dtStorCheckData.Rows[i1]["CREATEDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'"
                    + dtStorCheckData.Rows[i1]["MEDSTOREORDTYPEID_CHR"].ToString() + "',1," + dtStorCheckData.Rows[i1]["OUTFLAN_INT"].ToString() + ",'" + nowperodID + "')";
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
                if (lngRes > 0)
                {
                    newDeid = objHRPSvc.m_strGetNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", 18);
                    strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR)"
                        + " values('" + newDeid + "','" + newid + "','" + dtStorCheckData.Rows[i1]["MEDICINEID_CHR"].ToString() + "','0001',"
                        + dtStorCheckData.Rows[i1]["QTY_DEC"].ToString() + "," + dtStorCheckData.Rows[i1]["SALEUNITPRICE_DEC"].ToString() + ","
                        + dtStorCheckData.Rows[i1]["SALETOLPRICE_DEC"].ToString() + ",'" + dtStorCheckData.Rows[i1]["UNITID_CHR"].ToString() + "')";
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
                }
            }

            return lngRes;
        }
        #endregion

        #region �ж��Ƿ��������̵��������ĵ�������
        /// <summary>
        /// �ж��Ƿ��������̵��������ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="typeName">�������������</param>
        /// <param name="typeID">���ص�������ID</param>
        /// <returns>2�У�3���Ǹ������ڸ��¿�����в�����</returns>
        [AutoComplete]
        public long m_lngisCheckType(string typeName, out string typeID)
        {
            long lngRes = 0;
            typeID = "";
            DataTable dtResult = new DataTable();
            string strSQL = @"select MEDSTOREORDTYPEID_CHR from t_aid_medstoreordtype where MEDSTOREORDTYPE_VCHR='" + typeName + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtResult.Rows.Count == 0)
                lngRes = 3;
            if (lngRes > 0 && dtResult.Rows.Count > 0)
            {
                typeID = dtResult.Rows[0]["MEDSTOREORDTYPEID_CHR"].ToString();
                lngRes = 2;
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region ����ҩ���̵��¼��
        /// <summary>
        /// ����ҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStoreCheck(clsMedStoreCheck_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstorecheck
										  (checkid_chr, checkdate_dat, remark_vchr,
										   medstoreid_chr, periodid_chr, creator_chr,
										   createdate_dat, aduitemp_chr,
										   aduitdate_dat, acctemp_chr,
										   acctdate_dat, pstatus_int
										  )
								   VALUES ('" + p_objItem.m_strCheckID.Trim() + "', TO_DATE ('" + p_objItem.m_strCheckDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_strRemark.Trim() + "', ";
            strSQL += @"				   '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "', '" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "', '" + p_objItem.m_objCreator.strEmpID.Trim() + "', ";
            strSQL += @"				   TO_DATE ('" + p_objItem.m_strCreateDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "', ";
            strSQL += @"				   TO_DATE ('" + p_objItem.m_strAduitDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), '" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "', ";
            strSQL += @"				   TO_DATE ('" + p_objItem.m_strAcctDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"				  )";

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

        #region �޸�ҩ���̵��¼��
        /// <summary>
        /// �޸�ҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoreCheck(clsMedStoreCheck_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstorecheck
								 SET checkdate_dat = TO_DATE ('" + p_objItem.m_strCheckDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 remark_vchr = '" + p_objItem.m_strRemark.Trim() + "', ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(checkid_chr) = '" + p_objItem.m_strCheckID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "' AND TRIM(period_chr) = '" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "'";

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

        #region ɾ��ҩ���̵��¼��
        /// <summary>
        /// ɾ��ҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵��¼��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedStoreCheck(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE t_opr_medstorecheck
							   WHERE TRIM(checkid_chr) = '" + p_strID.Trim() + "'";

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

        #region ���ҩ���̵��¼��
        /// <summary>
        /// ���ҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵��¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAduitMedStoreCheck(clsMedStoreCheck_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstoreord
								 SET aduitemp_chr = '" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 aduitdate_dat = TO_DATE ('" + p_objItem.m_strAduitDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(checkid_chr) = '" + p_objItem.m_strCheckID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "'";

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

        #region ���ҩ���̵��¼������Ŀ��
        /// <summary>
        /// ���ҩ���̵��¼������Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵��¼��ID</param>
        /// <param name="p_intFlag">��ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeStorageAfterAduitMedStoreCheck(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_MEDSTORECHECKADUIT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "checkid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());

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

        #region ҩ���̵��¼������
        /// <summary>
        /// ҩ���̵��¼������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵��¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAcctMedStoreCheck(clsMedStoreCheck_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstorecheck
								 SET acctemp_chr = '" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "', ";
            strSQL += @"			 acctdate_dat = TO_DATE ('" + p_objItem.m_strAcctDate.Trim() + "', 'yyyy-mm-dd hh24:mi:ss'), ";
            strSQL += @"			 pstatus_int = " + p_objItem.m_intPStatus.ToString() + " ";
            strSQL += @"	   WHERE TRIM(checkid_chr) = '" + p_objItem.m_strCheckID.Trim() + "' AND TRIM(medstoreid_chr) = '" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "'";

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

        #region ҩ���̵��¼�����ʺ��������
        /// <summary>
        /// ҩ���̵��¼�����ʺ��������
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵��¼��ID</param>
        /// <param name="p_intFlag">��ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeFinAfterAcctMedStoreCheck(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_MEDSTORECHECKACCT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "checkid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());

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

        #region ����ҩ���̵���ϸ����¼
        /// <summary>
        /// ����ҩ���̵���ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStoreCheckDe(clsMedStoreCheckDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_medstorecheckde
										  (checkid_chr, checkdetailid_chr, rowno_chr, medicineid_chr,
										   unitid_chr, calcqty_dec, realqty_dec, syslotno_chr,
										   curprice_mny, buyprice_mny
										  )
								   VALUES ('" + p_objItem.m_strCheckID.Trim() + "', '" + p_objItem.m_strCheckDetailID.Trim() + "', '" + p_objItem.m_strRowNo.Trim() + "', '" + p_objItem.m_objMedicine.m_strMedicineID.Trim() + "', ";
            strSQL += @"				   '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "', " + p_objItem.m_decCalcQty.ToString() + ", " + p_objItem.m_decRealQty.ToString() + ", '" + p_objItem.m_strSysLotNo.Trim() + "', ";
            strSQL += @"				   " + p_objItem.m_decCurPrice.ToString() + ", " + p_objItem.m_decBuyPrice.ToString() + " ";
            strSQL += @"				  )";

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

        #region �޸�ҩ���̵���ϸ����¼
        /// <summary>
        /// �޸�ҩ���̵���ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ���̵���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoreCheckDe(clsMedStoreCheckDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_medstorecheckde
								 SET rowno_chr = '" + p_objItem.m_strRowNo.Trim() + "', ";
            strSQL += @"			 unitid_chr = '" + p_objItem.m_objUnit.m_strUnitID.Trim() + "', ";
            strSQL += @"			 realqty_dec = '" + p_objItem.m_decRealQty.ToString() + "' ";
            strSQL += @"	   WHERE TRIM(checkdetailid_chr) = '" + p_objItem.m_strCheckDetailID.Trim() + "'";

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

        #region ɾ��ҩ���̵���ϸ����¼
        /// <summary>
        /// ɾ��ҩ���̵���ϸ����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵���ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedStoreCheckDe(string p_strID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE t_opr_medstorecheckde
							   WHERE TRIM(checkdetailid_chr) = '" + p_strID.Trim() + "'";

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

        #region ģ����ѯҩ���̵��¼��
        /// <summary>
        /// ģ����ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByAny(string p_strSQL, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstorecheck
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreCheck_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreCheck_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_objPeriod = new clsPeriod_VO();
                            p_objResultArr[i].m_objCreator = new clsEmployeeVO();
                            p_objResultArr[i].m_objAduitEmp = new clsEmployeeVO();
                            p_objResultArr[i].m_objAcctEmp = new clsEmployeeVO();

                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["checkid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckDate = dtbResult.Rows[i]["checkdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_strRemark = dtbResult.Rows[i]["remark_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            p_objResultArr[i].m_objPeriod.m_strPeriodID = dtbResult.Rows[i]["periodid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strStartDate = dtbResult.Rows[i]["startdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strEndDate = dtbResult.Rows[i]["enddate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objCreator.strEmpID = dtbResult.Rows[i]["creator_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCreateDate = dtbResult.Rows[i]["createdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objAduitEmp.strEmpID = dtbResult.Rows[i]["aduitemp_chr"].ToString().Trim();
                            p_objResultArr[i].m_strAduitDate = dtbResult.Rows[i]["aduitdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objAcctEmp.strEmpID = dtbResult.Rows[i]["acctemp_chr"].ToString().Trim();
                            p_objResultArr[i].m_strAcctDate = dtbResult.Rows[i]["acctdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_intPStatus = Convert.ToInt32(dtbResult.Rows[i]["pstatus_int"].ToString().Trim());
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

        #region ���̵㵥ID��ѯҩ���̵��¼��
        /// <summary>
        /// ���̵㵥ID��ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵㵥ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByID(string p_strID, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            string strSQL = @" WHERE TRIM(checkid_chr) = '" + p_strID.Trim() + " ";

            lngRes = m_lngGetMedStoreCheckByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩ����ѯҩ���̵��¼��
        /// <summary>
        /// ��ҩ����ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByMedStore(string p_strID, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            string strSQL = @" WHERE TRIM(medstoreid_chr) = '" + p_strID.Trim() + " ";

            lngRes = m_lngGetMedStoreCheckByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���̵�ʱ���ѯҩ���̵��¼��
        /// <summary>
        /// ���̵�ʱ���ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByDate(string p_strStartDate, string p_strEndDate, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            string strSQL = @" WHERE checkdate_dat >= TO_DATE('" + p_strStartDate.Trim() + "','yyyy-mm-dd hh24:mi:ss'), checkdate_dat <= TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss') ";

            lngRes = m_lngGetMedStoreCheckByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯҩ���̵��¼��
        /// <summary>
        /// �������ڲ�ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByPeriod(string p_strID, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            string strSQL = @" WHERE TRIM(periodid_chr) = '" + p_strID.Trim() + "' ";

            lngRes = m_lngGetMedStoreCheckByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �����ݱ�־��ѯҩ���̵��¼��
        /// <summary>
        /// �����ݱ�־��ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_intStatus">״̬��־</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckByStatus(int p_intStatus, out clsMedStoreCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheck_VO[0];

            string strSQL = @" WHERE pstatus_int = " + p_intStatus.ToString() + " ";

            lngRes = m_lngGetMedStoreCheckByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ���̵���ϸ��
        /// <summary>
        /// ģ����ѯҩ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckDeByAny(string p_strSQL, out clsMedStoreCheckDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheckDe_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstorecheckde
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreCheckDe_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreCheckDe_VO();
                            p_objResultArr[i].m_objMedicine = new clsMedicine_VO();
                            p_objResultArr[i].m_objUnit = new clsUnit_VO();

                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["checkid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["checkdetailid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["rowno_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["calcqty_dec"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["realqty_dec"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["curprice_mny"].ToString().Trim();
                            p_objResultArr[i].m_strCheckID = dtbResult.Rows[i]["buyprice_mny"].ToString().Trim();
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

        #region ����¼��ID��ѯҩ���̵���ϸ��
        /// <summary>
        /// ����¼��ID��ѯҩ���̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵��¼��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckDeByCheckID(string p_strID, out clsMedStoreCheckDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheckDe_VO[0];
            string strSQL = @" WHERE TRIM(checkid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreCheckDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯҩ���̵���ϸ��
        /// <summary>
        /// ��ҩƷ��ѯҩ���̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckDeByMedicine(string p_strID, out clsMedStoreCheckDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreCheckDe_VO[0];

            string strSQL = @" WHERE TRIM(medicineid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreCheckDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ȡ��ǰ����ҩ���̵��¼��ID
        /// <summary>
        /// ��ȡ��ǰ����ҩ���̵��¼��ID
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵��¼��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strID = objHRPSvc.m_strGetNewID("t_opr_medstorecheck", "checkid_chr", 14);


                if (p_strID != null)
                {
                    if (Convert.ToInt32(p_strID.Trim()) <= 0)
                    {
                        int ID = Convert.ToInt32(p_strID.Trim()) + 1;
                        p_strID = ID.ToString("00000000000000");
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

        #region ��ȡ��ǰ�����̵���ϸ��ID
        /// <summary>
        /// ��ȡ��ǰ�����̵���ϸ��ID
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ���̵���ϸID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCheckDeID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strID = objHRPSvc.m_strGetNewID("t_opr_medstorecheckde", "checkdetailid_chr", 18);


                if (p_strID != null)
                {
                    if (Convert.ToInt32(p_strID.Trim()) <= 0)
                    {
                        int ID = Convert.ToInt32(p_strID.Trim()) + 1;
                        p_strID = ID.ToString("000000000000000000");
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

        #endregion

        #region ҩ����ϸ��

        #region ģ����ѯҩ����ϸ��
        /// <summary>
        /// ģ����ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDeByAny(string p_strSQL, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT *
							    FROM v_opr_medstoreordfinde
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreOrdFinDe_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreOrdFinDe_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_objPeriod = new clsPeriod_VO();
                            p_objResultArr[i].m_objOperator = new clsEmployeeVO();

                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            p_objResultArr[i].m_strMedStoreOrdID = dtbResult.Rows[i]["medstoreordid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strMedStoreOrdType = dtbResult.Rows[i]["medstoreordtypeid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineEngName = dtbResult.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strPYCode = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strWBCode = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strSyslotNo = dtbResult.Rows[i]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strPeriodID = dtbResult.Rows[i]["periodid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strStartDate = dtbResult.Rows[i]["startdate_dat"].ToString().Trim();
                            p_objResultArr[i].m_objPeriod.m_strEndDate = dtbResult.Rows[i]["enddate_dat"].ToString().Trim();
                            p_objResultArr[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty_dec"].ToString().Trim());
                            p_objResultArr[i].m_decBorrow = Convert.ToDecimal(dtbResult.Rows[i]["borrow_mny"].ToString().Trim());
                            p_objResultArr[i].m_decLoan = Convert.ToDecimal(dtbResult.Rows[i]["loan_mny"].ToString().Trim());
                            p_objResultArr[i].m_objOperator.strEmpID = dtbResult.Rows[i]["operator_chr"].ToString().Trim();
                            p_objResultArr[i].m_objOperator.strLastName = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_strCreateDate = dtbResult.Rows[i]["createdate_dat"].ToString().Trim();
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

        #region ��ҩ����ѯҩ����ϸ��
        /// <summary>
        /// ��ҩ����ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDeByMedStore(string p_strID, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            string strSQL = @" WHERE TRIM(medstoreid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdFinDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯҩ����ϸ��
        /// <summary>
        /// ���������Ͳ�ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDeByOrdType(string p_strID, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            string strSQL = @" WHERE TRIM(medstoreordtypeid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdFinDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������ͺ͵��ݺŲ�ѯҩ����ϸ��
        /// <summary>
        /// ���������ͺ͵��ݺŲ�ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���ݺ�</param>
        /// <param name="p_strOrdTypeID">��������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDeByOrdIDAndOrdType(string p_strID, string p_strOrdTypeID, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            string strSQL = @" WHERE TRIM(medstoreordid_chr) = '" + p_strID.Trim() + "' AND TRIM(medstoreordtypeid_chr) = '" + p_strOrdTypeID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdFinDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯҩ����ϸ��
        /// <summary>
        /// �������ڲ�ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDePeriod(string p_strID, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            string strSQL = @" WHERE TRIM(periodid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdFinDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯҩ����ϸ��
        /// <summary>
        /// ��ҩƷ��ѯҩ����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreOrdFinDeMedicine(string p_strID, out clsMedStoreOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdFinDe_VO[0];

            string strSQL = @" WHERE TRIM(medicineid_chr) = '" + p_strID.Trim() + "'";

            lngRes = m_lngGetMedStoreOrdFinDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region ҩ�����

        #region ģ����ѯҩ����ϸ���
        /// <summary>
        /// ģ����ѯҩ����ϸ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreDetailByAny(string p_strSQL, out clsMedStoreDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreDetail_VO[0];

            DataTable dtbResult = null;
            string strSQL = @"SELECT a.*, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int,
									 c.medicinename_vchr, c.medicineengname_vchr, c.pycode_chr,
									 c.wbcode_chr
							    FROM t_opr_medstoredetail a, t_bse_medstore b, t_bse_medicine c
							   WHERE a.medstoreid_chr = b.medstoreid_chr AND a.medicineid_chr = c.medicineid_chr(+)
							 " + p_strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStoreDetail_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStoreDetail_VO();
                            p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
                            p_objResultArr[i].m_objMedicine = new clsMedicine_VO();

                            p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            p_objResultArr[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strMedicineEngName = dtbResult.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strPYCode = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
                            p_objResultArr[i].m_objMedicine.m_strWBCode = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();
                            p_objResultArr[i].m_strSysLotNo = dtbResult.Rows[i]["syslotno_chr"].ToString().Trim();
                            p_objResultArr[i].m_decQty = Convert.ToDecimal(dtbResult.Rows[i]["qty_dec"].ToString().Trim());

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

        #region ��ҩ����ѯҩ����ϸ���
        /// <summary>
        /// ��ҩ����ѯҩ����ϸ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreDetailByMedStore(string p_strID, out clsMedStoreDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreDetail_VO[0];

            string strSQL = " AND TRIM(a.medstoreid_chr) = '" + p_strID.Trim() + "'";
            lngRes = m_lngGetMedStoreDetailByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯҩ����ϸ���
        /// <summary>
        /// ��ҩƷ��ѯҩ����ϸ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreDetailByMedicine(string p_strID, out clsMedStoreDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreDetail_VO[0];

            string strSQL = " AND TRIM(a.medicineid_chr) = '" + p_strID.Trim() + "'";
            lngRes = m_lngGetMedStoreDetailByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ���ܿ��
        #endregion

        #region ��ҩ����ѯҩ���ܿ��
        #endregion

        #region ��ҩƷ��ѯҩ���ܿ��
        #endregion

        #region �����շ���ĿID����ҩƷ���
        /// <summary>
        /// �����շ���ĿID����ҩƷ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�շ���ĿID</param>
        /// <param name="p_decQty">ҩƷ����</param>
        /// <param name="p_blnResult">����Ƿ��㣺trueΪ����falseΪ��治�����������</param>
        /// <param name="p_decResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMedStoreMedicineStorageByID(string p_strID, decimal p_decQty, out bool p_blnResult, out decimal p_decResult)
        {
            long lngRes = 0;
            p_blnResult = false;
            p_decResult = 0;

            DataTable dtbResult = null;
            string strSQL = @"SELECT SUM (amount_dec) AS tolqty
								FROM t_tol_medstoremedicine
							   WHERE TRIM(medicineid_chr) = (SELECT TRIM(itemsrcid_vchr)
															   FROM t_bse_chargeitem
															  WHERE TRIM (itemid_chr) = '" + p_strID.Trim() + "')";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    p_decResult = Convert.ToDecimal(dtbResult.Rows[0]["tolqty"].ToString().Trim());
                    if ((p_decResult - p_decQty) >= 0)
                        p_blnResult = true;
                    else
                        p_blnResult = false;

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

        #region ����ģ��ķ���
        [AutoComplete]
        public long m_lngGetMedicine(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = @"SELECT MEDICINEID_CHR,MEDICINENAME_VCHR,MEDSPEC_VCHR,OPUNIT_CHR FROM T_BSE_MEDICINE";

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

        #region ����ҩ��ID����ҩ����Ϣ��������Ϣ
        /// <summary>
        /// ����ҩ��ID����ҩ����Ϣ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="dtstroageMessage"></param>
        /// <param name="dtwindowsMessage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageMessage(string strID, out DataTable dtstroageMessage, out DataTable dtwindowsMessage, int intStatus)
        {
            long lngRes = 0;
            dtstroageMessage = null;
            dtwindowsMessage = null;
            string strSQL = @"SELECT MEDSTORENAME_VCHR,deptid_chr,case when MEDICNETYPE_INT=1 then '��ҩ' when MEDICNETYPE_INT=2 then '��ҩ' else '����' end as MEDICNETYPE,URGENCE_INT  FROM T_BSE_MEDSTORE WHERE MEDSTOREID_CHR='" + strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtstroageMessage);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes == 1 && dtstroageMessage.Rows.Count > 0)
            {
                strSQL = @"select * from t_bse_medstorewin where MEDSTOREID_CHR='" + strID + "' and WINDOWTYPE_INT=" + intStatus;
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtwindowsMessage);

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

        #region �ѷ�ҩ���������Ч����
        /// <summary>
        /// �ѷ�ҩ���������Ч����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetNullityData(string strID)
        {
            long lngRes = 0;
            string strSQL = @"update t_opr_medrecipesend set PSTATUS_INT=3  WHERE OUTPATRECIPEID_CHR='" + strID + "'";
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

        #region ȡʱ��
        /// <summary>
        /// ȡʱ��
        /// </summary>
        /// <param name="p_datatime"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetServerDate(out DateTime p_datatime)
        {
            long lngRes = 0;
            p_datatime = DateTime.Now;
            string strSQL = @"SELECT sysdate from dual";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0)
                {
                    p_datatime = Convert.ToDateTime(dtbResult.Rows[0][0].ToString());
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

        #region ���ݴ����Ų��ҷ��ʹ�������ID
        /// <summary>
        /// �������кŻ�ȡ�������ͱ�״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSid"></param>
        /// <param name="m_strStatus"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeSendStatusBySid(long m_lngSid, out string m_strStatus)
        {
            long lngRes = 0;
            m_strStatus = "";

            lngRes = 0;
            string strSQL = @"select  a.pstatus_int from t_opr_recipesend a where a.sid_int=?";
            try
            {
                DataTable dtbResult = new DataTable();
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = m_lngSid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strStatus = dtbResult.Rows[0]["pstatus_int"].ToString();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ȡ������Ϣ
        /// <summary>
        /// ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWindowInfo(out DataTable dtbResult, string p_strWINDOWID_CHR, string p_strMEDSTOREID_CHR)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = @"select * from t_bse_medstorewin where WINDOWID_CHR='" + p_strWINDOWID_CHR + "' and MEDSTOREID_CHR='" + p_strMEDSTOREID_CHR + "'";

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

        #region GetSendMedInfo
        /// <summary>
        /// GetSendMedInfo
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetSendMedInfo(string recipeId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select a.sid_int,
                               b.outpatrecipeid_chr as recipeid,
                               d.lastname_vchr      as patname,
                               a.sendwindowid_chr   as sendwindowid,
                               a.medstoreid_chr     as medstoreid,
                               d.birth_dat          as birthday
                          from t_opr_recipesend a
                         inner join t_opr_recipesendentry b
                            on a.sid_int = b.sid_int
                         inner join t_opr_outpatientrecipe c
                            on b.outpatrecipeid_chr = c.outpatrecipeid_chr
                         inner join t_bse_patient d
                            on c.patientid_chr = d.patientid_chr
                         where c.outpatrecipeid_chr = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = recipeId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region �ж��Ƿ��ѷ�ҩ
        /// <summary>
        /// �ж��Ƿ��ѷ�ҩ
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsSendMed(string sid)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select 1 from t_opr_recipesend t where t.sid_int = ? and t.pstatus_int = 3";
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = sid;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return false;
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
            //lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_nurseexecute","SEQ_INT",out p_strRecordID);
            //if(lngRes < 0)
            //    return lngRes;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"insert into t_opr_nurseexecute
            (seq_int, business_int, tablename_vchr, outpatrecipeid_chr,
             rowno_chr, itemid_chr, exectimes_int, operatortype_int,
             operatorid_chr, exectime_dat, systime_dat, remark1_vchr,
             remark2_vchr, status_int
            )
           values (seq_nurseexecuteid.nextval, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?
            )";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(13, out objLisAddItemRefArr);
                //objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[0].Value = p_objRecord.m_intBUSINESS_INT;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strTABLENAME_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strOUTPATRECIPEID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strROWNO_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intEXECTIMES_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intOPERATORTYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[8].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[9].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[10].Value = p_objRecord.m_strREMARK1_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strREMARK2_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intSTATUS_INT;
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

        #region ����SQL
        /// <summary>
        /// ҩ���½ᱨ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrPrID">Ҫͳ�ƵĲ������б�</param>
        /// <param name="strUpPr">Ҫͳ�Ƶĵ�һ��������</param>
        /// <param name="dt">������Ӧ�Ĳ�����ͳ������</param>
        /// <param name="strStorageID">Ҫͳ�ƵĲֿ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportmoth(System.Collections.Generic.List<string> arrPrID, string strUpPr, out DataTable dt, string strStorageID)
        {
            long lngRes = 0;
            dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            string strWhere = "";
            string strWhere2 = "";
            string strWhere1 = " and a.PERIODID_CHR='" + strUpPr + "' and a.STORAGEID_CHR='" + strStorageID + "'";
            if (arrPrID.Count > 0)
            {
                for (int i1 = 0; i1 < arrPrID.Count; i1++)
                {
                    strSQL =  arrPrID[i1];
                    if (i1 == 0)
                    {
                        strWhere = "  and (a.PERIODID_CHR='" + strSQL + "'";
                    }
                    else
                    {
                        strWhere += " or a.PERIODID_CHR='" + strSQL + "'";
                    }
                    if (i1 == arrPrID.Count - 1)
                    {
                        strWhere2 = @" and a.PERIODID_CHR='" + strSQL + "'";
                        strWhere += ")";
                    }
                }
            }
            strWhere += " and MEDSTOREID_CHR='" + strStorageID + "'";
            strWhere2 += " and a.STORAGEID_CHR='" + strStorageID + "'";
            strSQL = @"select k.*,f.*,h.* from(SELECT  '���ڽ��' as RowName, sum(b.realqty_dec * b.BUYPRICE_MNY) AS WestMedinmoney,
       sum(b.realqty_dec * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
    and  PSTATUS_INT=1  and FLAG_INT=1
   AND c.medicinetypeid_chr = 1" + strWhere1 + @")k,
(SELECT sum(b.realqty_dec *  b.BUYPRICE_MNY) AS WCinmoney,
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=1
   AND c.medicinetypeid_chr = 3" + strWhere1 + @")f,
(SELECT 
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=1
   AND c.medicinetypeid_chr = 2" + strWhere1 + @")h";
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
            strSQL = @"select k.*,f.*,h.* from(SELECT '���ڹ���' as RowName,
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WestMedsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=1
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WCsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=1
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =3" + strWhere + @")f,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS CHsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=1
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '�����˿�' as RowName,
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WestMedsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=2
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WCsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=2
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =3" + strWhere + @") f,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS CHsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=2
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt2);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '���ڷ�ҩ' as RowName,
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WestMedsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=5
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS WCsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=5
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =3" + strWhere + @")f,
(SELECT 
       sum(b.QTY_DEC * b.SALEUNITPRICE_DEC) AS CHsalemoney
  FROM t_opr_medstoreord a, t_opr_medstoreordde b, t_bse_medicine c,t_aid_medstoreordtype d
 WHERE a.MEDSTOREORDID_CHR = b.MEDSTOREORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.MEDSTOREORDTYPEID_CHR=d.MEDSTOREORDTYPEID_CHR
   and d.SIGN_INT=5
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr =2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt3);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT  '���ڽ��' as RowName,
       sum(b.realqty_dec * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
    and  PSTATUS_INT=1 and FLAG_INT=1
   AND c.medicinetypeid_chr = 1" + strWhere2 + @")k,
(SELECT 
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=1
   AND c.medicinetypeid_chr = 3" + strWhere2 + @")f,
(SELECT 
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=1
   AND c.medicinetypeid_chr = 2" + strWhere2 + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt4);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            dt.ImportRow(dt1.Rows[0]);
            dt.ImportRow(dt2.Rows[0]);
            dt.ImportRow(dt3.Rows[0]);
            dt.ImportRow(dt4.Rows[0]);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("Totailsalemoney");
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    double Totailinmoney = 0;
                    double Totailsalemoney = 0;
                    for (int f3 = 1; f3 < dt.Columns.Count - 2; f3++)
                    {
                        if (dt.Rows[i1][f3].ToString() != "")
                            Totailsalemoney += double.Parse(dt.Rows[i1][f3].ToString());
                    }
                    dt.Rows[i1]["Totailsalemoney"] = Totailsalemoney;
                }
            }
            return lngRes;
        }
        #endregion

        #region ͨ��ID����ҩƷ�������ͼ�¼��״̬
        /// <summary>
        ///   ͨ��ID����ҩƷ�������ͼ�¼��״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="winID"></param>
        /// <param name="p_objItem"></param>
        /// <param name="dtbStorageDe"></param>
        /// <param name="stroageID"></param>
        /// <param name="strTOLMNY"></param>
        /// <param name="nurseexecuteArr"></param>
        /// <param name="p_objDetail"></param>
        /// <param name="m_objOutStorageDetail"></param>
        /// <param name="m_strSubtractMode"></param>
        /// <returns>-99 ����ҩ������������ϸ����� -100 ���봦����ˮ���쳣�� </returns>
        [AutoComplete]
        public long m_lngUpdateMedRecipeListByID(string winID,
            clsMedRecipeSend_VO p_objItem, DataTable dtbStorageDe, string stroageID, string strTOLMNY,
            clst_opr_nurseexecute[] nurseexecuteArr, clsDS_StorageDetail_VO[] p_objDetail,
            ref clsDS_Outstorage_Detail[] m_objOutStorageDetail, string m_strSubtractMode, string m_strSecodeLevelMode)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                string strSQL = @"update t_opr_recipesend
       set autoprint_int = ?,
       pstatus_int = ?,
       finaltreateemp_chr = ?,
       sendemp_chr = ?,
       senddate_dat = sysdate
       where sid_int = ? and sendwindowid_chr = ?";
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(6, out paramArr);
                    paramArr[0].Value = p_objItem.m_AUTOPRINT_INT;
                    paramArr[1].Value = p_objItem.m_intPStatus;
                    paramArr[2].Value = string.Empty;
                    paramArr[3].Value = p_objItem.m_objSendEmp.strEmpID;
                    paramArr[4].Value = p_objItem.m_intSID;
                    paramArr[5].Value = winID;

                    long lngRecordsAffected = -1;
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

                    if (!string.IsNullOrEmpty(stroageID))
                    {
                        strSQL = @"update  t_opr_recipesend t
                                   set t.orgmedstoreid_chr = t.medstoreid_chr, t.medstoreid_chr = ?
                                 where sid_int = ?
                                   and t.medstoreid_chr <> ?";
                        HRPSvc.CreateDatabaseParameter(3, out paramArr);
                        paramArr[0].Value = stroageID;
                        paramArr[1].Value = p_objItem.m_intSID;
                        paramArr[1].DbType = DbType.Int32;
                        paramArr[2].Value = stroageID;

                        long lngAffected = -1;
                        lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, paramArr);
                    }

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
                }
                if (m_strSecodeLevelMode == "1" && m_strSubtractMode == "1")
                {
                    lngRes = this.m_lngSubtractStorage(p_objDetail, ref m_objOutStorageDetail);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    //throw new Exception("ҩ����ҩ�ۼ�������");
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return lngRes;
        }
        #endregion

        #region ��ҩ����
        /// <summary>
        /// ��ҩ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <param name="m_strWindowid"></param>
        /// <param name="m_strSendWindowid"></param>
        /// <param name="m_intSID"></param>
        /// <param name="p_objDetail"></param>
        /// <param name="m_objOutStorageDetail"></param>
        /// <param name="m_strSubtractMode">��������ҩ�����ĸ�ҵ��֮��ۼ�ҩ�����,0-��ҩ��,1-��ҩ��;</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDosageRecipe(clst_opr_nurseexecute[] p_objRecord, string m_strWindowid, string m_strSendWindowid, int m_intSID,
               clsDS_StorageDetail_VO[] p_objDetail, ref clsDS_Outstorage_Detail[] m_objOutStorageDetail,
               string m_strSubtractMode, string m_strSecondLevelMode, string p_strMedStoreID)
        {
            long lngRes = 0;
            try
            {
                if (p_objRecord.Length > 0)
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    string strWindowID = m_strSendWindowid;
                    int waiterNO = 0;
                    clsWindowsCortrol windCortrol = new clsWindowsCortrol();
                    string strTemp = string.Empty;
                    windCortrol.m_lngGetGiveWindID(p_objRecord[0].m_strWindow, out strTemp, out waiterNO);
                    //windCortrol.m_lngGetGiveWindID( p_objRecord[0].m_strWindow, out strWindowID, out waiterNO);
                    //if (strWindowID == "")
                    //    throw (new System.Exception("��û��������Ӧ�ķ�ҩ���ڣ���ǰ���еķ�ҩ���ڶ����ڹ����У�"));

                    string strSQL = @"update t_opr_recipesend
       set pstatus_int = 2, 
       treatdate_dat = sysdate,
       treatemp_chr = ?
       where sid_int = ?";
                    try
                    {
                        System.Data.IDataParameter[] param = null;
                        objHRPSvc.CreateDatabaseParameter(2, out param);
                        param[0].Value = p_objRecord[0].m_strOPERATORID_CHR;
                        param[1].Value = m_intSID;
                        long lngAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, param);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                    if (!string.IsNullOrEmpty(p_strMedStoreID))
                    {
                        strSQL = @"update  t_opr_recipesend t
                                   set t.orgmedstoreid_chr = t.medstoreid_chr, t.medstoreid_chr = ?
                                 where sid_int = ?
                                   and t.medstoreid_chr <> ?";
                        IDataParameter[] param = null;
                        objHRPSvc.CreateDatabaseParameter(3, out param);
                        param[0].Value = p_strMedStoreID;
                        param[1].Value = m_intSID;
                        param[1].DbType = DbType.Int32;
                        param[2].Value = p_strMedStoreID;

                        long lngAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, param);
                    }


                    clsmedstorewinque p_objWind = new clsmedstorewinque();
                    p_objWind.m_strMEDSTOREID_CHR = p_objRecord[0].m_strFrom;
                    p_objWind.m_strOUTPATRECIPEID_CHR = p_objRecord[0].m_strOUTPATRECIPEID_CHR;
                    p_objWind.m_strRECIPETYPE_CHR = p_objRecord[0].m_strOUTPATRECIPETYPE;
                    p_objWind.m_intSid = m_intSID;

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
                if (m_strSecondLevelMode == "1" && m_strSubtractMode == "0")
                {
                    lngRes = this.m_lngSubtractStorage(p_objDetail, ref m_objOutStorageDetail);
                    if (lngRes <= 0)
                        throw new Exception("ҩ����ҩ�ۼ�������");
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return lngRes;
        }
        #endregion

        #region ҩ����ҩ,�޸Ŀ��
        /// <summary>
        ///  ҩ����ҩ,�޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetail">�����ϸ</param>
        /// <param name="m_objOutStorageDetail">������ϸ</param>
        /// <returns>-99 ����ҩ������������ϸ����� -100 ���봦����ˮ���쳣�� </returns>
        [AutoComplete]
        public long m_lngSubtractStorage(clsDS_StorageDetail_VO[] p_objDetail, ref clsDS_Outstorage_Detail[] m_objOutStorageDetail)
        {
            long lngRes = -1;
            try
            {
                if (p_objDetail == null || p_objDetail.Length == 0)
                {
                    return -1;
                }

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
                        lngRes = m_lngModifyStorageDetailGross(ref p_objDetail[intRow], 2, p_lngSeriesID);
                        //�޸Ŀ����������
                        if (lngRes > 0)
                        {
                            lngRes = m_lngModifyStorageGross(p_objDetail[intRow], 2);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (lngRes < 1)
                    //throw new Exception("����ҩ������������ϸ�����");
                    return -99;
                else
                {
                    lngRes = this.m_lngAddDSRecipeAccountInfo(p_objDetail);
                }
                if (lngRes < 1)
                    return -100;
                //throw new Exception("���봦����ˮ���쳣��");
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
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
         ?,
         b.ipavailablegross_num,
         b.opavailablegross_num
    from t_ds_storage_detail b, t_bse_medicine c
   where b.seriesid_int = ?
     and b.medicineid_chr = c.medicineid_chr(+)";
            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.String, DbType.String, DbType.Int64 };
            object[][] objValuesArr = new object[9][];

            //20091021 ������Ϊ0ʱ����������ˮ��
            int intLength = 0;
            for (int i1 = 0; i1 < m_objStorageDetailVoArr.Length; i1++)
            {
                if (m_objStorageDetailVoArr[i1].m_dblIPREALGROSS_INT != 0)
                {
                    intLength++;
                }
            }

            if (intLength == 0)
            {
                return 1;
            }


            int m_intCount = m_objStorageDetailVoArr.Length;
            clsDS_StorageDetail_VO m_objTempVo;
            for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
            {
                objValuesArr[j] = new object[intLength];
            }

            int intIdx = 0;
            for (int i = 0; i < m_intCount; i++)
            {
                if (m_objStorageDetailVoArr[i].m_dblIPREALGROSS_INT == 0)
                    continue;
                m_objTempVo = m_objStorageDetailVoArr[i];
                objValuesArr[0][intIdx] = m_objTempVo.m_dblIPREALGROSS_INT;
                objValuesArr[1][intIdx] = m_objTempVo.m_dblOPREALGROSS_INT;
                objValuesArr[2][intIdx] = m_objTempVo.m_dblOldIPREALGROSS_INT;
                objValuesArr[3][intIdx] = m_objTempVo.m_dblOldOPREALGROSS_INT;
                objValuesArr[4][intIdx] = m_objTempVo.m_intSubStorageType;
                objValuesArr[5][intIdx] = m_objTempVo.m_strOperatorid;
                objValuesArr[6][intIdx] = m_objTempVo.m_strOutPatientRecipeid;
                objValuesArr[7][intIdx] = m_objTempVo.m_strOperatorid;
                objValuesArr[8][intIdx] = m_objTempVo.m_lngSERIESID_INT;
                intIdx++;
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
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩ��id��ҩƷid�жϿ����ϸ���Ƿ��Ѵ��ڸ�ҩ
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
a.opwholesaleprice_int,a.validperiod_dat,a.instoreid_vchr,a.drugstoreid_chr,a.productorid_chr
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
                    m_objOutStorageDetail.m_strPRODUCTORID_CHR = dtbValue.Rows[0]["productorid_chr"].ToString();
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
where a.seriesid_int=?
   and a.iprealgross_int + ? >= 0";
                objHRPServ.CreateDatabaseParameter(6, out objValues);
                objValues[0].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = p_lngSeriesID;
                objValues[5].Value = p_objDetail.m_dblIPREALGROSS_INT;
            }
            else
            {
                strSQL = @"update t_ds_storage_detail a
set a.oprealgross_int = a.oprealgross_int + ?,
a.iprealgross_int = a.iprealgross_int + ?,
a.opavailablegross_num = a.opavailablegross_num + ?,
a.ipavailablegross_num = a.ipavailablegross_num + ?
where a.seriesid_int=?
   and a.iprealgross_int + ? >= 0";
                objHRPServ.CreateDatabaseParameter(6, out objValues);
                objValues[0].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = p_lngSeriesID;
                objValues[5].Value = -p_objDetail.m_dblIPREALGROSS_INT;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes <= 0 || lngAffected != 1)
                    return -90;
                //throw new Exception("�ۿ�����");

                //20091010:�ڷ�ҩ��ʱ��д��ҩƷ�ۼ��� t_opr_recipededuct
                if (intType == 2 && p_objDetail.m_dblIPREALGROSS_INT != 0)
                {
                    //��ȡ�к�
                    int p_intRowNo = 0;
                    strSQL = @"select a.rowno_chr
  from t_opr_outpatientpwmrecipede a
  left join t_bse_chargeitem b on b.itemid_chr = a.itemid_chr
 where a.outpatrecipeid_chr = ?
   and b.itemsrcid_vchr = ?";
                    objHRPServ.CreateDatabaseParameter(2, out objValues);
                    objValues[0].Value = p_objDetail.m_strOutPatientRecipeid;
                    objValues[1].Value = p_objDetail.m_strMEDICINEID_CHR;
                    DataTable dtbTemp = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objValues);
                    if (dtbTemp != null && dtbTemp.Rows.Count > 0)
                    {
                        Int32.TryParse(dtbTemp.Rows[0][0].ToString(), out p_intRowNo);
                    }


                    strSQL = @"insert into t_opr_recipededuct
  (outpatrecipeid_chr, medseriesid_int, drugstoreid_chr, medicineid_chr,
   rowno_vchr, lotno_vchr, chargetype_int, opamount_dec, ipamount_dec, status_int)
values
  (?, ?, ?, ?, ?,'X', ?, ?, ?, 0)";

                    objHRPServ.CreateDatabaseParameter(8, out objValues);
                    objValues[0].Value = p_objDetail.m_strOutPatientRecipeid;
                    objValues[1].Value = p_objDetail.m_lngSERIESID_INT;
                    objValues[2].Value = p_objDetail.m_strDRUGSTOREID_CHR;
                    objValues[3].Value = p_objDetail.m_strMEDICINEID_CHR;
                    objValues[4].Value = p_intRowNo;
                    objValues[5].Value = p_objDetail.m_dblOPCHARGEFLG_INT;//��������С
                    objValues[6].Value = p_objDetail.m_dblOPREALGROSS_INT;
                    objValues[7].Value = p_objDetail.m_dblIPREALGROSS_INT;

                    lngAffected = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                    if (lngAffected < 1 || lngRes < 1)
                        throw new Exception("д��ҩƷ�ۼ���ʧ�ܣ�");
                }

                DataTable dtReuslt = new DataTable();
                strSQL = @"select a.iprealgross_int ,a.oprealgross_int from  t_ds_storage_detail a where a.seriesid_int=?";
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

        #region ���ҩ����ҩ��Ϣ
        /// <summary>
        /// ���ҩ����ҩ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddReturnMedInfo(clsReutrnMed m_objMainVo, List<clsReutrnMedEntry> m_objDetailList)
        {
            long lngRes = -1;
            string strSQL;
            if (m_objDetailList == null || m_objDetailList.Count == 0)
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"insert into t_opr_returnmed a
                                           (a.outpatrecipeid_chr,
                                            a.operemp_chr,
                                            a.opertime_dat,
                                            a.confirmemp_chr,
                                            a.confirmtime_dat,
                                            a.status_int,
                                            a.flag_int,a.drugstoreid_chr)
                                         values
                                           (?, ?, ?, ?, ?, ?, ?,?)";
            objValues = null;
            objHRPServ.CreateDatabaseParameter(8, out objValues);
            objValues[0].Value = m_objMainVo.m_strOUTPATRECIPEID_CHR;
            objValues[1].Value = m_objMainVo.m_strOPEREMP_CHR;
            objValues[2].Value = m_objMainVo.m_datOPERTIME_DAT;
            objValues[3].Value = m_objMainVo.m_strCONFIRMEMP_CHR;
            objValues[4].Value = m_objMainVo.m_datCONFIRMTIME_DAT;
            objValues[5].Value = m_objMainVo.m_intSTATUS_INT;
            objValues[6].Value = m_objMainVo.m_intFLAG_INT;
            objValues[7].Value = m_objMainVo.m_strDrugStoreid;
            long lngAffected = -1;
            lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
            if (lngAffected < 1 || lngRes < 1)
                throw new Exception();
            if (lngRes > 0)
            {
                strSQL = @" insert into t_opr_returnmed_entry b
  (b.outpatrecipeid_chr,
   b.itemid_chr,
   b.itemname_vchr,
   b.orgamout_dec,
   b.retamout_dec,
   b.price_dec,
   b.billrowno_int,
   b.unitid_chr,
   b.medseriesid_int,
   b.opamount_dec,
   b.ipamount_dec)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ";
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.Double, DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.Int64, DbType.Double, DbType.Double };
                object[][] objValuesArr = new object[11][];
                int m_intListCount = m_objDetailList.Count;
                clsReutrnMedEntry m_objTempVo;
                for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
                {
                    objValuesArr[j] = new object[m_intListCount];
                }
                for (int i = 0; i < m_intListCount; i++)
                {
                    m_objTempVo = m_objDetailList[i];
                    objValuesArr[0][i] = m_objTempVo.m_strOUTPATRECIPEID_CHR;
                    objValuesArr[1][i] = m_objTempVo.m_strITEMID_CHR;
                    objValuesArr[2][i] = m_objTempVo.m_strITEMNAME_VCHR;
                    objValuesArr[3][i] = m_objTempVo.ORGAMOUT_DEC;
                    objValuesArr[4][i] = m_objTempVo.RETAMOUT_DEC;
                    objValuesArr[5][i] = m_objTempVo.PRICE_DEC;
                    objValuesArr[6][i] = m_objTempVo.BILLROWNO_INT;
                    objValuesArr[7][i] = m_objTempVo.m_strUnit_chr;
                    objValuesArr[8][i] = Convert.ToInt64(m_objTempVo.m_strSerialno);

                    objValuesArr[9][i] = Convert.ToDouble(m_objDetailList[i].m_dblLotsReturnAmount / m_objDetailList[i].m_dblPackage).ToString("0.0000");
                    objValuesArr[10][i] = m_objDetailList[i].m_dblLotsReturnAmount;



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
            }
            else
            {
                throw new Exception();
            }
            if (lngRes > 0)
            {
                clsDS_Outstorage_Detail[] m_objOutStorageDetailVoArr = null;
                clsDS_StorageDetail_VO[] m_objStorageDetailVoArr = new clsDS_StorageDetail_VO[m_objDetailList.Count];
                for (int m_intRow = 0; m_intRow < m_objDetailList.Count; m_intRow++)
                {
                    m_objStorageDetailVoArr[m_intRow] = new clsDS_StorageDetail_VO();
                    m_objStorageDetailVoArr[m_intRow].m_strMEDICINEID_CHR = m_objDetailList[m_intRow].m_strMEDICINEID_CHR;
                    m_objStorageDetailVoArr[m_intRow].m_lngSERIESID_INT = Convert.ToInt64(m_objDetailList[m_intRow].m_strSerialno);
                    m_objStorageDetailVoArr[m_intRow].m_strDRUGSTOREID_CHR = m_objDetailList[m_intRow].m_strDrugStoreid_chr;
                    m_objStorageDetailVoArr[m_intRow].m_intSubStorageType = 1;
                    m_objStorageDetailVoArr[m_intRow].m_strOutPatientRecipeid = m_objMainVo.m_strOUTPATRECIPEID_CHR;
                    m_objStorageDetailVoArr[m_intRow].m_strOperatorid = m_objMainVo.m_strOPEREMP_CHR;
                    //m_objStorageDetailVoArr[m_intRow].m_strMEDICINEID_CHR = dtSendMedRecipeDetail.Rows[m_intRow]["medicineid_chr"].ToString();

                    m_objStorageDetailVoArr[m_intRow].m_dblOPREALGROSS_INT = Convert.ToDouble(Convert.ToDouble(m_objDetailList[m_intRow].m_dblLotsReturnAmount / m_objDetailList[m_intRow].m_dblPackage).ToString("0.0000"));
                    m_objStorageDetailVoArr[m_intRow].m_dblIPREALGROSS_INT = m_objDetailList[m_intRow].m_dblLotsReturnAmount;


                }
                //�޸�ҩ�����
                lngRes = this.m_lngSubtractStorage(1, m_objStorageDetailVoArr, ref m_objOutStorageDetailVoArr);
                if (lngRes <= 0)
                {
                    throw new Exception();
                }
                else  //д�봦����ˮ�ʱ�
                {
                    lngRes = this.m_lngAddDSRecipeAccountInfo(m_objStorageDetailVoArr);
                }
                if (lngRes <= 0)
                {
                    throw new Exception();
                }
            }
            return lngRes;

        }
        #endregion

        #region ҩ����ҩ,�޸Ŀ��
        /// <summary>
        /// ҩ����ҩ,�޸Ŀ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intSubStorageType">�޸����͡�1���ӿ��,2�������</param>
        /// <param name="p_objDetail"></param>
        /// <param name="m_objOutStorageDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSubtractStorage(Int16 m_intSubStorageType, clsDS_StorageDetail_VO[] p_objDetail, ref clsDS_Outstorage_Detail[] m_objOutStorageDetail)
        {
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
                    lngRes = m_lngModifyStorageDetailGross(p_objDetail[intRow], m_intSubStorageType, p_lngSeriesID);
                    //�޸Ŀ����������
                    if (lngRes != -1)
                    {
                        lngRes = m_lngModifyStorageGross(p_objDetail[intRow], m_intSubStorageType);
                    }
                }
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
        public long m_lngModifyStorageDetailGross(clsDS_StorageDetail_VO p_objDetail, Int16 intType, long p_lngSeriesID)
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
where a.seriesid_int=?
   and a.ipavailablegross_num + ? >= 0 ";
                objHRPServ.CreateDatabaseParameter(6, out objValues);
                objValues[0].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = p_lngSeriesID;
                objValues[5].Value = p_objDetail.m_dblIPREALGROSS_INT;
            }
            else
            {
                strSQL = @"update t_ds_storage_detail a
set a.oprealgross_int = a.oprealgross_int + ?,
a.iprealgross_int = a.iprealgross_int + ?,
a.opavailablegross_num = a.opavailablegross_num + ?,
a.ipavailablegross_num = a.ipavailablegross_num + ?
where a.seriesid_int=?
   and a.ipavailablegross_num + ? >= 0";
                objHRPServ.CreateDatabaseParameter(6, out objValues);
                objValues[0].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[1].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[2].Value = -p_objDetail.m_dblOPREALGROSS_INT;
                objValues[3].Value = -p_objDetail.m_dblIPREALGROSS_INT;
                objValues[4].Value = p_lngSeriesID;
                objValues[5].Value = -p_objDetail.m_dblIPREALGROSS_INT;
            }
            try
            {
                long lngAffected = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffected, objValues);
                if (lngRes <= 0 || lngAffected != 1)
                    throw new Exception("�ۿ�����");
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ȡ��ҩ�����ͱ�кű�־
        /// <summary>
        /// ȡ��ҩ�����ͱ�кű�־
        /// </summary>
        /// <param name="m_intSid">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelRecipeSendCalledFlag(long m_intSid)
        {

            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"update t_opr_recipesend a set a.called_int=0,a.currentcall_int=0,a.recalled_int=0,a.quit_int=0 where a.sid_int=?";
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

        #region ͨ����Ʊ�Ų�ѯ������Ϣ
        /// <summary>
        /// ͨ����Ʊ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="invoNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPatInfoByInvo(string invoNo)
        {
            DataTable dt = null;
            string Sql = @"select a.patientid_chr, a.patientname_chr, a.seqid_chr, b.patientcardid_chr
                              from t_opr_outpatientrecipeinv a
                             inner join t_bse_patientcard b
                                on a.patientid_chr = b.patientid_chr
                             where a.invoiceno_vchr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = invoNo;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region ΢�ż���Ƿ��
        /// <summary>
        /// ΢�ż���Ƿ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsWechatBanding(string cardNo)
        {
            string Sql = @"select t.cardno from opRegWeChatBinding t where t.cardno = ? and t.status = 1";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = cardNo;

                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;    // ���ڰ�
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ͨ������ID��ȡ���ƿ���
        /// <summary>
        /// ͨ������ID��ȡ���ƿ���
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetCardNoByRecipeId(string recipeId)
        {
            string cardNo = string.Empty;
            string Sql = @"select b.patientcardid_chr
                              from t_opr_outpatientrecipe a
                             inner join t_bse_patientcard b
                                on a.patientid_chr = b.patientid_chr
                             where a.outpatrecipeid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = recipeId;

                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    cardNo = dt.Rows[0]["patientcardid_chr"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return cardNo;
        }
        #endregion

        #region ���ʹ�����ҩ����

        #region ��ѯ��ҩ����
        /// <summary>
        /// ��ѯ��ҩ����
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="opIp">1 ����; 2 סԺ</param>
        /// <param name="no">���￨��; סԺ��</param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable QueryChinaMedRecipe(string startDate, string endDate, int opIp, string no)
        {
            string Sql = string.Empty;
            DataTable dtMed = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                if (opIp == 1)              // ����
                {
                    Sql = @"select a.outpatrecipeid_chr as recipeid,
                                   a.registerid_chr as registerid,
                                   '' as ipno,
                                   b.lastname_vchr as patname,
                                   b.sex_chr as sex,
                                   b.birth_dat as birthday,
                                   b.homephone_vchr as tel,
                                   b.email_vchr as email,
                                   b.consigneeaddr,
                                   b.mobile_chr,
                                   c.deptname_vchr as deptname,
                                   '' as areaname,
                                   d.lastname_vchr as doctname,
                                   to_char(a.recorddate_dat, 'yyyy-mm-dd hh24:mi') as recipedate,
                                   0 as recipeno,
                                   e.invoiceno_vchr as invono,
                                   (e.totalsum_mny - abs(e.totaldiffcost_mny)) as invmoney,
                                   f.paytypename_vchr as paytypename,
                                   g.patientcardid_chr as cardno,
                                   '' as bedno,
                                   s.sid_int as putmedid,
                                   nvl(a.isproxyboilmed,0) as sendstatus,
                                   ch.diag_vchr as diagdesc
                              from t_opr_outpatientrecipe a
                             inner join t_bse_patient b
                                on a.patientid_chr = b.patientid_chr
                             inner join t_bse_deptdesc c
                                on a.diagdept_chr = c.deptid_chr
                             inner join t_bse_employee d
                                on a.diagdr_chr = d.empid_chr
                             inner join t_opr_outpatientrecipeinv e
                                on a.outpatrecipeid_chr = e.outpatrecipeid_chr
                             inner join t_bse_patientpaytype f
                                on a.paytypeid_chr = f.paytypeid_chr
                             inner join t_bse_patientcard g
                                on a.patientid_chr = g.patientid_chr
                             inner join t_opr_recipesendentry s
                                on a.outpatrecipeid_chr = s.outpatrecipeid_chr 
                              left join t_opr_outpatientcasehis ch
                                on a.casehisid_chr = ch.casehisid_chr
                             where a.pstauts_int = 2
                           --  and (a.isproxyboilmed not in (1, 2)) 
                               and (a.recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                               and g.patientcardid_chr = ? ";
                }
                else if (opIp == 2)         // סԺ
                {
                    // -- a.orderid_chr as recipeid,
                    Sql = @"select a.registerid_chr as recipeid,
                                   b.registerid_chr as registerid,
                                   b.inpatientid_chr as ipno,
                                   c.lastname_vchr as patname,
                                   c.sex_chr as sex,
                                   c.birth_dat as birthday,
                                   c.homephone_vchr as tel,
                                   c.email_vchr as email,
                                   c.consigneeaddr,
                                   c.mobile_chr,
                                   '' as deptname,
                                   d.deptname_vchr as areaname,
                                   a.creator_chr as doctname,
                                   to_char(p.create_dat, 'yyyy-mm-dd hh24:mi') as recipedate,
                                   nvl(a.recipeno_int, 0) as recipeno,
                                   '' as invono,
                                   0 as invmoney,
                                   f.paytypename_vchr as paytypename,
                                   g.patientcardid_chr as cardno,
                                   x.code_chr as bedno,
                                   p.putmeddetailid_chr as putmedid,
                                   nvl(a.isproxyboilmed,0) as sendstatus, b.icd10diagtext_vchr as diagdesc 
                              from t_opr_bih_order a
                             inner join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                             inner join t_bse_patient c
                                on b.patientid_chr = c.patientid_chr
                             inner join t_bse_deptdesc d
                                on a.curareaid_chr = d.deptid_chr
                             inner join t_bse_patientpaytype f
                                on b.paytypeid_chr = f.paytypeid_chr
                             inner join t_bse_patientcard g
                                on b.patientid_chr = g.patientid_chr
                             inner join t_bih_opr_putmeddetail p
                                on a.orderid_chr = p.orderid_chr
                               and p.status_int = 1
                              left join t_bse_bed x
                                on b.registerid_chr = x.bihregisterid_chr 
                             where a.status_int in ( 2, 3, 4, 5, 6 )  
                          --   and (a.isproxyboilmed not in (1, 2))
                               and (p.create_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                               and b.inpatientid_chr = ? ";

                }

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = startDate + " 00:00:00";
                parm[1].Value = endDate + " 23:59:59";
                parm[2].Value = no;
                svc.lngGetDataTableWithParameters(Sql, ref dtMed, parm);
                if (opIp == 1 && dtMed != null && dtMed.Rows.Count > 0)
                {
                    DataTable dtSumUsage = null;
                    Sql = @"select distinct a.outpatrecipeid_chr as recipeid,
                                            b.sumusage_vchr      as sumusage
                              from t_opr_outpatientrecipe a
                             inner join t_opr_outpatientcmrecipede b
                                on a.outpatrecipeid_chr = b.outpatrecipeid_chr
                             inner join t_bse_patientcard c
                                on a.patientid_chr = c.patientid_chr
                             where a.pstauts_int = 2
                        --     and (a.isproxyboilmed not in (1, 2))
                               and (a.recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                               and c.patientcardid_chr = ?";

                    svc.CreateDatabaseParameter(3, out parm);
                    parm[0].Value = startDate + " 00:00:00";
                    parm[1].Value = endDate + " 23:59:59";
                    parm[2].Value = no;
                    svc.lngGetDataTableWithParameters(Sql, ref dtSumUsage, parm);
                    if (dtSumUsage != null)
                    {
                        string recipeId = string.Empty;
                        DataRow[] drr = null;
                        for (int i = 0; i < dtMed.Rows.Count; i++)
                        {
                            recipeId = dtMed.Rows[i]["recipeid"].ToString();
                            drr = dtSumUsage.Select("recipeid = '" + recipeId + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                dtMed.Rows[i]["diagdesc"] += " / " + drr[0]["sumusage"];
                            }
                        }
                    }
                }
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dtMed;
        }
        #endregion

        #region ��ѯ���ʹ�����ҩ����Դ�б�
        /// <summary>
        /// ��ѯ���ʹ�����ҩ����Դ�б�
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="opIp">1 ����; 2 סԺ</param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable QueryProxyBoilMed(string startDate, string endDate, int opIp)
        {
            string Sql = string.Empty;
            DataTable dtMed = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                if (opIp == 1)              // ����
                {
                    Sql = @"select a.outpatrecipeid_chr as recipeid,
                                   a.registerid_chr as registerid,
                                   '' as ipno,
                                   b.lastname_vchr as patname,
                                   b.sex_chr as sex,
                                   b.birth_dat as birthday,
                                   b.homephone_vchr as tel,
                                   b.email_vchr as email,
                                   b.consigneeaddr,
                                   b.mobile_chr,
                                   c.deptname_vchr as deptname,
                                   '' as areaname,
                                   d.lastname_vchr as doctname,
                                   to_char(a.recorddate_dat, 'yyyy-mm-dd hh24:mi') as recipedate,
                                   0 as recipeno,
                                   e.invoiceno_vchr as invono,
                                   (e.totalsum_mny - abs(e.totaldiffcost_mny)) as invmoney,
                                   f.paytypename_vchr as paytypename,
                                   g.patientcardid_chr as cardno,
                                   '' as bedno,
                                   s.sid_int as putmedid,
                                   p.status as sendstatus,
                                   ch.diag_vchr as diagdesc
                              from t_opr_outpatientrecipe a
                             inner join t_bse_patient b
                                on a.patientid_chr = b.patientid_chr
                             inner join t_bse_deptdesc c
                                on a.diagdept_chr = c.deptid_chr
                             inner join t_bse_employee d
                                on a.diagdr_chr = d.empid_chr
                             inner join t_opr_outpatientrecipeinv e
                                on a.outpatrecipeid_chr = e.outpatrecipeid_chr
                             inner join t_bse_patientpaytype f
                                on a.paytypeid_chr = f.paytypeid_chr
                             inner join t_bse_patientcard g
                                on a.patientid_chr = g.patientid_chr
                             inner join t_opr_recipesendentry s
                                on a.outpatrecipeid_chr = s.outpatrecipeid_chr
                              left join t_log_medtrans_cfyp p
                                on a.outpatrecipeid_chr = p.recipeid
                               and p.flagid = 1
                              left join t_opr_outpatientcasehis ch
                                on a.casehisid_chr = ch.casehisid_chr
                             where a.pstauts_int = 2
                               and (a.isproxyboilmed = 1 or a.isproxyboilmed = 2) 
                               and (a.recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";
                }
                else if (opIp == 2)         // סԺ
                {
                    // -- a.orderid_chr as recipeid,
                    Sql = @"select a.registerid_chr as recipeid,
                                   b.registerid_chr as registerid,
                                   b.inpatientid_chr as ipno,
                                   c.lastname_vchr as patname,
                                   c.sex_chr as sex,
                                   c.birth_dat as birthday,
                                   c.homephone_vchr as tel,
                                   c.email_vchr as email,
                                   c.consigneeaddr,
                                   c.mobile_chr,
                                   '' as deptname,
                                   d.deptname_vchr as areaname,
                                   a.creator_chr as doctname,
                                   to_char(p.create_dat, 'yyyy-mm-dd hh24:mi') as recipedate,
                                   nvl(a.recipeno_int, 0) as recipeno,
                                   '' as invono,
                                   0 as invmoney,
                                   f.paytypename_vchr as paytypename,
                                   g.patientcardid_chr as cardno,
                                   x.code_chr as bedno,
                                   p.putmeddetailid_chr as putmedid,
                                   y.status as sendstatus, b.icd10diagtext_vchr as diagdesc 
                              from t_opr_bih_order a
                             inner join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                             inner join t_bse_patient c
                                on b.patientid_chr = c.patientid_chr
                             inner join t_bse_deptdesc d
                                on a.curareaid_chr = d.deptid_chr
                             inner join t_bse_patientpaytype f
                                on b.paytypeid_chr = f.paytypeid_chr
                             inner join t_bse_patientcard g
                                on b.patientid_chr = g.patientid_chr
                             inner join t_bih_opr_putmeddetail p
                                on a.orderid_chr = p.orderid_chr
                               and p.status_int = 1
                              left join t_bse_bed x
                                on b.registerid_chr = x.bihregisterid_chr
                              left join t_log_medtrans_cfyp y
                                on p.putmeddetailid_chr = y.putmedid
                               and y.flagid = 2
                             where (a.isproxyboilmed = 1 or a.isproxyboilmed = 2)
                               and a.status_int in ( 2, 3, 4, 5, 6 ) 
                               and (p.create_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                }

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = startDate + " 00:00:00";
                parm[1].Value = endDate + " 23:59:59";
                svc.lngGetDataTableWithParameters(Sql, ref dtMed, parm);
                if (opIp == 1 && dtMed != null && dtMed.Rows.Count > 0)
                {
                    DataTable dtSumUsage = null;
                    Sql = @"select distinct a.outpatrecipeid_chr as recipeid,
                                            b.sumusage_vchr      as sumusage
                              from t_opr_outpatientrecipe a
                             inner join t_opr_outpatientcmrecipede b
                                on a.outpatrecipeid_chr = b.outpatrecipeid_chr
                             where a.pstauts_int = 2
                               and (a.isproxyboilmed = 1 or a.isproxyboilmed = 2)
                               and (a.recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = startDate + " 00:00:00";
                    parm[1].Value = endDate + " 23:59:59";
                    svc.lngGetDataTableWithParameters(Sql, ref dtSumUsage, parm);
                    if (dtSumUsage != null)
                    {
                        string recipeId = string.Empty;
                        DataRow[] drr = null;
                        for (int i = 0; i < dtMed.Rows.Count; i++)
                        {
                            recipeId = dtMed.Rows[i]["recipeid"].ToString();
                            drr = dtSumUsage.Select("recipeid = '" + recipeId + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                dtMed.Rows[i]["diagdesc"] += " / " + drr[0]["sumusage"];
                            }
                        }
                    }
                }
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dtMed;
        }
        #endregion

        #region ��ѯ���ʹ�����ҩ��ϸ
        /// <summary>
        /// ��ѯ���ʹ�����ҩ��ϸ
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="recipeNo"></param>
        /// <param name="opIp"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable QueryProxyBoilMedDet(string recipeId, string recipeNo, string recipeDate, int opIp)
        {
            string Sql = string.Empty;
            DataTable dtMed = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                if (opIp == 1)              // ����
                {
                    Sql = @"select a.outpatrecipeid_chr as recipeid,
                                   a.times_int          as times,
                                   a.itemid_chr         as itemid,
                                   a.itemname_vchr      as itemname,
                                   a.unitid_chr         as unit,
                                   a.qty_dec            as qty,
                                   a.unitprice_mny      as price,
                                   a.sumusage_vchr      as recipeusage,
                                   c.medicineid_chr     as medid,
                                   c.assistcode_chr     as medcode,
                                   c.medicinename_vchr  as medname,
                                   c.medspec_vchr       as spec,
                                   d.usagename_vchr     as usageName,
                                   round(a.qty_dec * a.unitprice_mny * a.times_int, 2) as total                                   
                              from t_opr_outpatientcmrecipede a
                             inner join t_bse_chargeitem b
                                on a.itemid_chr = b.itemid_chr
                             inner join t_bse_medicine c
                                on b.itemsrcid_vchr = c.medicineid_chr
                              left join t_bse_usagetype d
                                on a.usageid_chr = d.usageid_chr
                             where a.outpatrecipeid_chr = ?";

                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = recipeId;
                }
                else if (opIp == 2)         // סԺ
                {
                    Sql = @"select a.registerid_chr as recipeid,
                                   (case nvl(a.outgetmeddays_int, 1)
                                         when 0 then
                                          1
                                         else
                                          nvl(a.outgetmeddays_int, 1)
                                       end) as times,
                                   '' as itemid,
                                   '' as itemname,
                                   p.unit_vchr as unit,
                                   (p.get_dec / (case nvl(a.outgetmeddays_int, 1)
                                         when 0 then
                                          1
                                         else
                                          nvl(a.outgetmeddays_int, 1)
                                       end)) as qty,
                                   p.unitprice_mny as price,
                                   '' as recipeusage,
                                   c.medicineid_chr as medid,
                                   c.assistcode_chr as medcode,
                                   c.medicinename_vchr as medname,
                                   c.medspec_vchr as spec,
                                   (a.dosetypename_chr || nvl(a.remark_vchr,'')) as usageName, 
                                   round(p.get_dec * p.unitprice_mny, 2) as total,
                                   to_char(p.create_dat, 'yyyy-mm-dd hh24:mi') as recipedate 
                              from t_opr_bih_order a
                             inner join t_bih_opr_putmeddetail p
                                on a.orderid_chr = p.orderid_chr
                               and p.status_int = 1
                             inner join t_bse_medicine c
                                on p.medid_chr = c.medicineid_chr
                             where a.registerid_chr = ?
                               and a.recipeno_int = ? ";
                    // (a.isproxyboilmed = 1 or a.isproxyboilmed = 2) and 

                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = recipeId;
                    parm[1].Value = recipeNo;
                }
                svc.lngGetDataTableWithParameters(Sql, ref dtMed, parm);
                svc.Dispose();
                if (opIp == 2 && dtMed != null && dtMed.Rows.Count > 0)
                {
                    for (int i = dtMed.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtMed.Rows[i]["recipedate"].ToString() != recipeDate)
                        {
                            dtMed.Rows.RemoveAt(i);
                        }
                    }
                    dtMed.AcceptChanges();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dtMed;
        }
        #endregion

        #region ����Ƿ��ѷ���
        /// <summary>
        /// ����Ƿ��ѷ���
        /// </summary>
        /// <param name="putMedIds"></param>
        /// <param name="isEqual">true �жϷ���; false �ж�ȡ������</param>
        /// <returns></returns>
        [AutoComplete]
        public bool CheckIsSend(string putMedIds, bool isEqual)
        {
            string Sql = @"select t.status from t_log_medtrans_cfyp t where t.putmedid in ({0})";

            string[] sarr = putMedIds.Split(',');
            putMedIds = string.Empty;
            foreach (string str in sarr)
            {
                putMedIds += "'" + str.Replace("'", "") + "',";
            }
            putMedIds = putMedIds.TrimEnd(',');

            Sql = string.Format(Sql, putMedIds);
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                DataTable dt = null;
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                svc.Dispose();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (isEqual)
                        {
                            if (Convert.ToInt32(dr["status"].ToString()) == 1)
                            {
                                return true;        // �ѷ���
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(dr["status"].ToString()) != 1)
                            {
                                return true;        // δ����
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
            return false;
        }
        #endregion

        #region ���ʹ���ҩת������ҩ��
        /// <summary>
        /// ���ʹ���ҩת������ҩ��
        /// </summary>
        /// <param name="recipeIds"></param>
        /// <param name="putMedIds"></param>
        /// <param name="operId"></param>
        /// <param name="opIp"></param>
        /// <returns></returns>
        [AutoComplete]
        public int ConvertMedStore(string recipeIds, string putMedIds, string operId, int opIp)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                if (opIp == 1)              // ����
                {
                    Sql = @"update t_opr_outpatientrecipe t set t.isproxyboilmed = 0 where t.outpatrecipeid_chr in ({0})";
                    Sql = string.Format(Sql, recipeIds);
                }
                else if (opIp == 2)         // סԺ
                {
                    Sql = @"update t_opr_bih_order t
                               set t.isproxyboilmed = 0
                             where t.orderid_chr in
                                   (select b.orderid_chr
                                      from t_bih_opr_putmeddetail b
                                     where b.putmeddetailid_chr in ({0}))";
                    Sql = string.Format(Sql, putMedIds);
                }
                affectRows += svc.DoExcute(Sql);

                Sql = @"update t_log_medtrans_cfyp t set t.convertoperid = '{0}', t.convertdate = sysdate where t.putmedid in ({1})";
                affectRows += svc.DoExcute(string.Format(Sql, operId, putMedIds));

                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)affectRows;
        }
        #endregion

        #region ͳ��
        /// <summary>
        /// ͳ��
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="medCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable StatPBM(string startDate, string endDate, string medCode, int opIp, out int allTimes, out int djdsTimes)
        {
            DataTable dt = null;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            allTimes = 0;
            djdsTimes = 0;
            try
            {
                Sql = @"select b.assistcode_chr as medCode,
                               a.medname,
                               trim(a.unit) as unit,
                               a.price,
                               round(nvl(a.price, 0) / 1.25, 3) as tradprice,
                               sum(a.qty * decode(nvl(a.times, 1), 0, 1, nvl(a.times, 1))) as qty,
                               sum(round(a.price * a.qty *
                                         decode(nvl(a.times, 1), 0, 1, nvl(a.times, 1)),
                                         2)) as total,
                               sum(round(round(nvl(a.price, 0) / 1.25, 3) * a.qty *
                                         decode(nvl(a.times, 1), 0, 1, nvl(a.times, 1)),
                                         2)) as tradtotal
                          from t_log_medtrans_cfyp a
                         inner join t_bse_medicine b
                            on a.medid = b.medicineid_chr
                         where a.status = 1
                           and (a.recorddate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 
                           {0}     
                         group by b.assistcode_chr, a.medname, a.price, trim(a.unit)
                         order by b.assistcode_chr";

                string sub = string.Empty;
                if (opIp > 0)
                {
                    sub += string.Format("and a.flagid = {0} ", opIp);
                }
                if (!string.IsNullOrEmpty(medCode))
                {
                    sub += string.Format("and b.assistcode_chr = '{0}' ", medCode);
                }
                Sql = string.Format(Sql, sub);

                svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = startDate + " 00:00:00";
                parm[1].Value = endDate + " 23:59:59";
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);

                Sql = @"select sum(times) as allTimes
                          from (select distinct a.recipeno,
                                                decode(nvl(a.times, 1), 0, 1, nvl(a.times, 1)) as times
                                  from t_log_medtrans_cfyp a
                                 where a.status = 1
                                   and (a.recorddate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                   {0} )";
                Sql = string.Format(Sql, sub);

                DataTable dtTimes = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = startDate + " 00:00:00";
                parm[1].Value = endDate + " 23:59:59";
                svc.lngGetDataTableWithParameters(Sql, ref dtTimes, parm);
                if (dtTimes != null && dtTimes.Rows.Count > 0)
                {
                    if (dtTimes.Rows[0][0] != DBNull.Value)
                        allTimes = Convert.ToInt32(dtTimes.Rows[0][0].ToString());
                }

                Sql = @"select sum(times) as allTimes
                          from (select distinct a.recipeno,
                                                decode(nvl(a.times, 1), 0, 1, nvl(a.times, 1)) as times
                                  from t_log_medtrans_cfyp a
                                 where a.status = 1 and a.boiltype = 1 
                                   and (a.recorddate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                   {0} )";
                Sql = string.Format(Sql, sub);

                dtTimes = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = startDate + " 00:00:00";
                parm[1].Value = endDate + " 23:59:59";
                svc.lngGetDataTableWithParameters(Sql, ref dtTimes, parm);
                if (dtTimes != null && dtTimes.Rows.Count > 0)
                {
                    if (dtTimes.Rows[0][0] != DBNull.Value)
                        djdsTimes = Convert.ToInt32(dtTimes.Rows[0][0].ToString());
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region �޸���ҩ��������
        /// <summary>
        /// �޸���ҩ��������
        /// </summary>
        /// <param name="opIp">1 ����; 2 סԺ</param>
        /// <param name="proxyTypeId">0 ����ҩ��; 1 �������; 2 ��ҩ����</param>
        /// <param name="ids">����ID; ��ҩ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public int ModifyProxyBoilMedType(int opIp, int proxyTypeId, string ids)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                if (opIp == 1)
                {
                    Sql = @"update t_opr_outpatientrecipe 
                               set isproxyboilmed = {0}
                             where outpatrecipeid_chr in ({1})";
                }
                else if (opIp == 2)
                {
                    Sql = @"update t_opr_bih_order
                               set isproxyboilmed = {0}
                             where orderid_chr in (select a.orderid_chr
                                                     from t_opr_bih_order a
                                                    inner join t_bih_opr_putmeddetail p
                                                       on a.orderid_chr = p.orderid_chr
                                                    where p.putmeddetailid_chr in ({1}))";
                }
                else
                {
                    return -1;
                }
                Sql = string.Format(Sql, proxyTypeId, ids);
                affectRows = svc.DoExcute(Sql);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return (int)affectRows;
        }
        #endregion

        #endregion

    }
}
