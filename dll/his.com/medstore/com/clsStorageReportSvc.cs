using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsStorageReport 的摘要说明。
    /// </summary>
    /// 
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]

    public class clsStorageReportSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase
    {
        public clsStorageReportSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获得所有药品资料
        /// <summary>
        /// 获得所有药品资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbMedicine"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStanMed(out DataTable dtbMedicine)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = "SELECT t1.medicineid_chr AS strcomm1, assistcode_chr AS strcomm2," +
                                    "medicinename_vchr AS strcomm3, medspec_vchr AS strcomm4, " +
                                    "opunit_chr AS strcomm5, ipunit_chr AS strcomm6," +
                                    "t1.packqty_dec AS deccomm4," +
                                    "t2.curqty_dec AS deccomm1, t2.saleunitprice_mny AS deccomm2," +
                                    "t2.curqty_dec * t2.saleunitprice_mny AS deccomm3 " +
                            "FROM t_bse_medicine t1, t_opr_storagemeddetail t2 " +
                            "WHERE t1.standard_int = 1 AND t1.medicineid_chr = t2.medicineid_chr(+) " +
                            "ORDER BY t1.assistcode_chr";

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

    }
}
