using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �ض����ֶ�Ӧ�շ���Ŀά���м��
    /// ���ߣ�He Guiqiu
    /// ����ʱ��:2006-06-24
    /// </summary>
    /// 
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsYbdeaDefChargeitemSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsYbdeaDefChargeitemSvc()
        {

        }

        #region ȡ�շ���Ŀ
        /// <summary>
        /// ȡ�շ���Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeaCode"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetChargeItem(out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"  SELECT T_BSE_CHARGEITEM.ITEMID_CHR,   
                                     T_BSE_CHARGEITEM.ITEMNAME_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMCODE_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMPYCODE_CHR,   
                                     T_BSE_CHARGEITEM.ITEMWBCODE_CHR,   
                                     T_BSE_CHARGEITEM.ITEMSPEC_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMPRICE_MNY
                                FROM T_BSE_CHARGEITEM   
                                ORDER BY T_BSE_CHARGEITEM.ITEMCODE_VCHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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

        #region ����ҽ����ȡ��Ӧ���շ���Ŀ
        /// <summary>
        /// ����ҽ����ȡ��Ӧ���շ���Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeaCode"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetChargeItemByDeaCode(string p_strDeaCode, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"  SELECT T_BSE_CHARGEITEM.ITEMID_CHR,   
                                     T_BSE_CHARGEITEM.ITEMNAME_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMCODE_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMPYCODE_CHR,   
                                     T_BSE_CHARGEITEM.ITEMWBCODE_CHR,   
                                     T_BSE_CHARGEITEM.ITEMSPEC_VCHR,   
                                     T_BSE_CHARGEITEM.ITEMPRICE_MNY,   
                                     T_OPR_YBDEADEFCHARGEITEM.DEACODE_CHR  
                                FROM T_BSE_CHARGEITEM,   
                                     T_OPR_YBDEADEFCHARGEITEM  
                                WHERE ( T_BSE_CHARGEITEM.ITEMID_CHR = T_OPR_YBDEADEFCHARGEITEM.ITEMID_CHR )  AND 
                                            T_OPR_YBDEADEFCHARGEITEM.DEACODE_CHR = '"
                                       + p_strDeaCode + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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

        #region �����ض����ֺ��շ���Ŀ���Ӧ��¼
        /// <summary>
        /// �����ض����ֺ��շ���Ŀ���Ӧ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeaCode"></param>
        /// <param name="p_newArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long AddYbdeaDefChargeItem(string p_strDeaCode, System.Collections.Generic.List<string> p_newArr)
        {
            long lngRes = 0;

            if (p_strDeaCode == "" || p_newArr.Count == 0)
            {
                return -1;
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            for (int i1 = 0; i1 < p_newArr.Count; i1++)
            {
                string strSQL = "INSERT INTO T_OPR_YBDEADEFCHARGEITEM (DEACODE_CHR, ITEMID_CHR) VALUES (?,?)";
                try
                {
                    System.Data.IDataParameter[] dataParameterArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out dataParameterArr);

                    dataParameterArr[0].Value = p_strDeaCode;
                    dataParameterArr[1].Value = ( p_newArr[i1]).Trim();

                    long lngRecEff = -1;
                    lngRes = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, dataParameterArr);
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

        #region ɾ���ض����ֺ��շ���Ŀ���Ӧ��¼
        /// <summary>
        /// ɾ���ض����ֺ��շ���Ŀ���Ӧ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeaCode"></param>
        /// <param name="p_removeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long RemoveYbdeaDefChargeItem(string p_strDeaCode, System.Collections.Generic.List<string> p_removeArr)
        {
            long lngRes = 0;

            if (p_strDeaCode == "")
            {
                return -1;
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            for (int i1 = 0; i1 < p_removeArr.Count; i1++)
            {
                string strSQL = "DELETE FROM T_OPR_YBDEADEFCHARGEITEM WHERE (trim(DEACODE_CHR) = ? AND trim(ITEMID_CHR) = ?)";
                try
                {
                    System.Data.IDataParameter[] dataParameterArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out dataParameterArr);

                    dataParameterArr[0].Value = p_strDeaCode;
                    dataParameterArr[1].Value = ( p_removeArr[i1]).Trim();

                    long lngRecEff = -1;
                    lngRes = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, dataParameterArr);
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

        #region �����޸�
        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeaCode"></param>
        /// <param name="p_removeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long SaveDeaDefChargeItem(string p_strDeaCode, System.Collections.Generic.List<string> p_removeArr, System.Collections.Generic.List<string> p_newArr)
        {
            long lngRes = 0;

            if (p_strDeaCode == "")
            {
                return -1;
            }

            if (p_removeArr.Count > 0)
            {
                lngRes = RemoveYbdeaDefChargeItem(p_strDeaCode, p_removeArr);
                if (lngRes < 0)
                {
                    return lngRes;
                }
            }

            if (p_newArr.Count > 0)
            {
                lngRes = AddYbdeaDefChargeItem(p_strDeaCode, p_newArr);
            }


            return lngRes;
        }
        #endregion 

    }
}
