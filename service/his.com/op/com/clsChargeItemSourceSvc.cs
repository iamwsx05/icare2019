using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsChargeItemSourceSvc ��ժҪ˵����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsChargeItemSourceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsChargeItemSourceSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region �շ���Ŀ��������(�������е����)
        /// <summary>
        /// �շ���Ŀ��������(�������е����)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItemCatList(out clsCharegeItemCat_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsCharegeItemCat_VO[0];
            string strSQL = "Select itemcatid_chr sID,itemcatname_vchr sName  From t_bse_chargeitemcat ";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    objResult = new clsCharegeItemCat_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < objResult.Length; i1++)
                    {
                        objResult[i1] = new clsCharegeItemCat_VO();
                        objResult[i1].m_strItemCatID = dtResult.Rows[i1][0].ToString().Trim();
                        objResult[i1].m_strItemCatName = dtResult.Rows[i1][1].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
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
        /// <param name="p_objPrincipal"></param>
        /// <param name="strCatID">����ID</param>
        /// <param name="strType"></param>
        /// <param name="strContent"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindChargeItem(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            try
            {

                string strSQL = @"select a.ITEMID_CHR,a.ITEMCODE_VCHR,a.ITEMNAME_VCHR,a.ITEMSRCID_VCHR,a.ITEMSRCNAME_VCHR,b.assistcode_chr from t_bse_chargeitem A,t_bse_medicine B 
where a.ITEMSRCID_VCHR =b.medicineid_chr(+) and a.ITEMCATID_CHR = '" + strCatID + "' order by a.ITEMCODE_VCHR";
                if (strContent.Trim() != "")
                {
                    strSQL = @"select a.ITEMID_CHR,a.ITEMCODE_VCHR,a.ITEMNAME_VCHR,a.ITEMSRCID_VCHR,a.ITEMSRCNAME_VCHR,b.assistcode_chr from t_bse_chargeitem A,t_bse_medicine B 
where a.ITEMSRCID_VCHR =b.medicineid_chr(+) and a.ITEMCATID_CHR = '" + strCatID + "' AND " + strType + " LIKE '" + strContent + "%' order by a.ITEMCODE_VCHR";
                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����Դ����ȡ��Դ���б�
        [AutoComplete]
        public long m_lngFindAllSour(string SourType, out DataTable dtResult, string strWhere)
        {
            string strSQL = "select ''ID,''Name From dual";
            dtResult = new DataTable();
            long lngRes = 0;
            if (SourType.IndexOf("��") > -1)
            {
                string strWhere1 = "";
                if (strWhere.IndexOf("ASSISTCODE_CHR") >= 0)
                {
                    strWhere = strWhere.Replace("ASSISTCODE_CHR", "");
                    strWhere = strWhere.Replace("where", "");
                    strWhere = strWhere.Replace("like", "");
                    strWhere1 = " where APPLY_UNIT_ID_CHR like " + strWhere;
                }
                if (strWhere.IndexOf("MEDICINENAME_VCHR") >= 0)
                {
                    strWhere = strWhere.Replace("MEDICINENAME_VCHR", "");
                    strWhere = strWhere.Replace("like", "");
                    strWhere = strWhere.Replace("where", "");
                    strWhere1 = " where APPLY_UNIT_NAME_VCHR like " + strWhere + "";
                }
                strSQL = "Select APPLY_UNIT_ID_CHR HelpCode,APPLY_UNIT_ID_CHR ID,APPLY_UNIT_NAME_VCHR Name From T_AID_LIS_APPLY_UNIT" + strWhere1 + "  order by APPLY_UNIT_ID_CHR";

            }
            else
            {
                strSQL = "Select ASSISTCODE_CHR HelpCode,medicineid_chr ID,medicinename_vchr Name From t_bse_medicine " + strWhere + " order by ASSISTCODE_CHR";
            }
            //			switch (SourType.IndexOf("��")>-1)
            //			{
            //				case "2": //��ҩ
            //					strSQL="Select medicineid_chr ID,medicinename_vchr Name From t_bse_medicine ";
            //					break;
            //				case "1":
            ////					strSQL="Select MATERIALID_CHR ID,MATERIALNAME_VCHR Name From t_bse_material ";
            //					break;
            //				case "5"://����
            //					strSQL="Select APPLY_UNIT_ID_CHR ID,APPLY_UNIT_NAME_VCHR Name From T_AID_LIS_APPLY_UNIT ";
            //					break;
            //				default:
            //					//					strSQL="Select ITEMID_CHR ID,ITEMNAME_VCHR Name From t_bse_chargeitem ";
            //					strSQL="Select medicineid_chr ID,medicinename_vchr Name From t_bse_medicine ";
            //					break;
            //			}
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �޸��÷�
        [AutoComplete]
        public long m_mthSaveData(string strItemID, string strSourceID, string strSourceName, string strSourceCatID, string strCatName)
        {
            long lngRes = 0;
            string strSQL = "UPDate T_BSE_CHARGEITEM Set " +
                "ITEMSRCID_VCHR='" + strSourceID.Trim() + "'," +
                "ITEMSRCTYPE_INT='" + strSourceCatID.Trim() + "'," +
                    "ITEMSRCNAME_VCHR='" + strSourceName.Trim() + "'," +
                    "ITEMSRCTYPENAME_VCHR='" + strCatName.Trim() + "' " +
                " Where ITEMID_CHR='" + strItemID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
    /// <summary>
    /// �շ���Ŀ����ӳ���м�� 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsItemCatMappingSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsItemCatMappingSvc()
        {

        }
        #region �����������
        [AutoComplete]
        public long m_mthGetSubjectionCat(out DataTable dtResult, string strCatID, int flag)
        {
            long lngRes = 0;
            dtResult = new DataTable();
            string strSQL = "select CATID_CHR from T_BSE_CHARGECATMAP where GROUPID_CHR ='" + strCatID + "' and INTERNALFLAG_INT =" + flag.ToString();
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��������
        [AutoComplete]
        public long m_mthSaveData(clsItemCatMapping_VO[] ICM_VO, string strCatID, int flag)
        {
            long lngRes = 0;
            IDataParameter[] paramArr = null;
            string strSQL = "delete T_BSE_CHARGECATMAP where GROUPID_CHR='" + strCatID + "' and INTERNALFLAG_INT =" + flag.ToString();
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                for (int i = 0; i < ICM_VO.Length; i++)
                {
                    if (ICM_VO[i].ToString().Trim() != "")
                    {
                        //strSQL="insert into  T_BSE_CHARGECATMAP (GROUPID_CHR,CATID_CHR,INTERNALFLAG_INT) values ('"+strCatID+"','"+ICM_VO[i].m_strCatMappingID+"','"+flag.ToString()+"')";
                        //lngRes = objHRPSvc.DoExcute(strSQL);

                        strSQL = "insert into  T_BSE_CHARGECATMAP (GROUPID_CHR,CATID_CHR,INTERNALFLAG_INT) values (?,?,?)";

                        objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                        paramArr[0].Value = strCatID;
                        paramArr[1].Value = ICM_VO[i].m_strCatMappingID;
                        paramArr[2].Value = flag;
                        long lngRecordsAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��ѯҩ����Ϣ
        [AutoComplete]
        public long m_mthMedstoreInfo(out DataTable dt, string strExpen)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"select medstoreid_chr,
       medstorename_vchr,
       medstoretype_int,
       medicnetype_int,
       urgence_int,
       deptid_chr,
       shortname_chr from t_bse_Medstore where MEDSTORETYPE_INT=1 and MEDICNETYPE_INT=" + strExpen;
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion
        #region ����ҩ��ID�������
        [AutoComplete]
        public long m_mthWindowInfoByID(out DataTable dt, string strExpen)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"select windowid_chr,
       windowname_vchr,
       medstoreid_chr,
       windowtype_int,
       workstatus_int,
       winproperty_int from T_BSE_MEDSTOREWIN where MEDSTOREID_CHR ='" + strExpen + "'";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion
    }
}
