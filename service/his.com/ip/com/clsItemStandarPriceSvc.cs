using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]

    public class clsItemStandarPriceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 取所有收费项目
        /// <summary>
        /// 取所有收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAllItem(out DataTable dtMain, out DataTable dtDet)
        {
            long lngRes = 0;
            string strSQL = "";

            dtMain = new DataTable();
            dtDet = new DataTable();

            //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //            lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "com.digitalwave.iCare.middletier.HIS.clsItemStandarPriceSvc", "GetAllItem");
            //            if (lngRes < 0)
            //            {
            //                return -1;
            //            }
            //            string strSQL = @" select a.ORDERDICID_CHR seq,
            //                                      a.USERCODE_CHR ITEM_CODE,
            //                                      trim(a.NAME_CHR) ITEM_NAME,
            //                                      b.dosageunit_chr ITEM_UNIT,
            //                                      b.ITEMPRICE_MNY ITEM_PRICE,
            //                                      a.PYCODE_CHR PY_CODE,
            //                                      a.WBCODE_CHR WB_CODE,
            //                                      b.ITEMSPEC_VCHR REMARK,
            //                                      b.ITEMID_CHR,
            //                                      b.ITEMNAME_VCHR,
            //                                      c.TYPEID,
            //                                      c.typetext
            //                               from T_BSE_BIH_ORDERDIC a,
            //                                    AR_APPLY_TYPELIST c,
            //                                    T_BSE_CHARGEITEM  b
            //                               where a.itemid_chr = b.itemid_chr(+) and
            //                                     b.APPLY_TYPE_INT = c.typeid(+) and
            //                                     a.ORDERCATEID_CHR = '17' and a.status_int<>0
            //                             order by item_code";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                strSQL = @"select a.orderdicid_chr as seq,
                                  a.usercode_chr as item_code,
                                  a.name_chr as item_name,
                                  a.commname_vchr as commname,
                                  a.des_vchr as des,
                                  a.pycode_chr as py_code,
                                  a.wbcode_chr as wb_code,
                                  status_int  
                             from t_bse_bih_orderdic a 
                            where a.NEWCHARGETYPE_INT = 1 

                         order by a.usercode_chr ";

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtMain);

                strSQL = @"select c.itemid_chr as itemid,
                                  c.itemcode_vchr as itemcode,
                                  c.itemname_vchr as itemname,
                                  c.itemspec_vchr as itemspec,
                                  c.dosageunit_chr as itemunit,
                                  c.itemprice_mny as itemprice,
                                  c.apply_type_int as typeid,
                                  c.ifstop_int as ifstop,
                                  b.usescope_int as usescope,
                                  a.orderdicid_chr as seq
                             from t_bse_bih_orderdic a,
                                  t_aid_bih_orderdic_charge b,
                                  t_bse_chargeitem c 
                            where a.orderdicid_chr = b.orderdicid_chr and
                                  b.itemid_chr = c.itemid_chr and
                                  a.NEWCHARGETYPE_INT = 1 and 
                                  a.status_int <> 0";

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtDet);

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

        #region 取检查部位列表
        /// <summary>
        /// 取检查部位列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtableCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAllPartlist(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @" SELECT a.partid,
                                      a.partname,
                                      a.assistcode_chr,
                                      a.TYPEID,
                                      a.PYCODE_VCHR,
                                      a.WBCODE_VCHR
                                FROM ar_apply_partlist a
                                where 
                                a.deleted=0
                                order by a.partid";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
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
    }
}
