using System;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using System.Data;
using com.digitalwave.Utility;
using System.EnterpriseServices;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 收费项目的各项查询（用于收费项目输入控件）
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsChargeInputSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase, IDisposable
    {
        public clsChargeInputSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 根据项目类型取回项目列表
        [AutoComplete]
        public long m_GetItemByItemCat(string CatID, ref DataTable dtResult)
        {
            long lngRes = 0;
            string strSQL = "Select a.*,b.unitname_chr UnitName,d.unitname_chr OPUnitName, " +
                "e.unitname_chr IPUnitName,c.usagename_vchr UsageName " +
                " From t_bse_chargeitem a,t_Aid_unit b,t_bse_usageType c,t_Aid_unit d,t_Aid_unit e " +
                " Where a.itemcatid_chr=? And a.isgroupitem_int=0  " +
                " And a.itemunit_chr=b.unitid_chr(+) " +
                " And a.itemopunit_chr=d.unitid_chr(+) " +
                " And a.itemipunit_chr=e.unitid_chr(+) " +
                " And a.usageid_chr=c.usageid_chr(+) " +
                " Order By a.itemcode_vchr ";
            try
            {                
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { CatID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
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

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
