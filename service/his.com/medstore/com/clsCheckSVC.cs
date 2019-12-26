using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsCheckSVC 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsCheckSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsCheckSVC()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 入库价格查询
        /// <summary>
        /// 入库价格查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOrdInPriceCheck(out DataTable dt, string strmedTypeID)
        {
            long lngRes = 0;
            dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL;
            if (strmedTypeID == null)
            {
                strSQL = @"select distinct b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINETYPEID_CHR,b.PYCODE_CHR,b.WBCODE_CHR,b.MEDICINENAME_VCHR,b.STANDARD_INT from T_OPR_STORAGEORDDE a,T_BSE_MEDICINE b,T_OPR_STORAGEORD c where a.MEDICINEID_CHR=b.MEDICINEID_CHR and  a.STORAGEORDID_CHR=c.STORAGEORDID_CHR and c.SIGN_INT=1 order by b.ASSISTCODE_CHR";
            }
            else
            {
                strSQL = @"select distinct b.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINETYPEID_CHR,b.PYCODE_CHR,b.WBCODE_CHR,b.MEDICINENAME_VCHR,b.STANDARD_INT from T_OPR_STORAGEORDDE a,T_BSE_MEDICINE b,T_OPR_STORAGEORD c where a.MEDICINEID_CHR=b.MEDICINEID_CHR and  a.STORAGEORDID_CHR=c.STORAGEORDID_CHR and c.SIGN_INT=1 and MEDICINETYPEID_CHR='" + strmedTypeID + "' order by b.ASSISTCODE_CHR";
            }
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
            return lngRes;
        }
        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtMedType"></param>
        [AutoComplete]
        public long m_lngGetMedType(out DataTable dtMedType)
        {
            long lngRes = 0;
            dtMedType = new DataTable();
            string strSQL = @"SELECT * FROM T_Aid_MedicineType ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtMedType);
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
        /// 根据药品ID获取入库明细数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strmedID"></param>
        /// <param name="dtDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedDe(string strmedID, out DataTable dtDe)
        {
            long lngRes = 0;
            dtDe = new DataTable();
            string strSQL = @"select a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.ORD_DAT,a.LOTNO_VCHR,a.USEFULLIFE_DAT,a.PRODCUTORID_CHR,a.QTY_DEC,a.UNITID_CHR,a.BUYUNITPRICE_MNY,a.BUYTOLPRICE_MNY,b.DOCID_VCHR ,d.VENDORNAME_VCHR,c.MEDICINENAME_VCHR  from T_OPR_STORAGEORDDE a,T_OPR_STORAGEORD b,T_BSE_MEDICINE c,t_bse_vendor d where a.MEDICINEID_CHR='" + strmedID + "' and a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and a.MEDICINEID_CHR=c.MEDICINEID_CHR and b.SIGN_INT=1 and b.VENDORID_CHR=d.VENDORID_CHR(+)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe);
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
    }
}
