using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药库打印中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedReportSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsMedReportSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 药品入库明细报表(月)
        /// <summary>
        /// 药品入库明细报表(月)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="datestar"></param>
        /// <param name="dateEnd"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtde"></param>
        /// <param name="strSTANDARD">是否中标药 0 - 否 1 - 是,2-全部</param>
        /// <param name="strMEDICINETYPE">1-西药，2-中草药，3-中成药</param>
        /// <param name="strSIGN">出入标志 1－入库 2－出库</param>
        /// <param name="strIN">对内对外标志,1－院内，0－院外</param> 
        /// <returns></returns>
        [AutoComplete]
        public long m_mthOrdDeByMonth(System.Collections.Generic.List<string> arrList, out DataTable dtVendor, out DataTable dtde, string strSTANDARD, string strMEDICINETYPE, string strSIGN, string strIN)
        {
            long lngRes = 0;
            dtVendor = new DataTable();
            dtde = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strWhere = @" and g.SIGN_INT='" + strSIGN + "' and g.DEPTTYPE_INT=" + strIN;
            if (arrList.Count > 0)
            {
                for (int i1 = 0; i1 < arrList.Count; i1++)
                {
                    if (i1 == 0)
                    {
                        strWhere += " and (PERIODID_CHR ='" + (string)arrList[i1] + "'";
                    }
                    else
                    {
                        strWhere += " or PERIODID_CHR ='" + (string)arrList[i1] + "'";
                    }
                }
                strWhere += ")";
            }

            string strSQL = @"select distinct a.VENDORID_CHR,b.VENDORNAME_VCHR from T_OPR_STORAGEORD a,T_BSE_VENDOR b,t_bse_storage c,t_aid_storageordtype g where  a.PSTATUS_INT=2 and a.STORAGEID_CHR=c.STORAGEID_CHR and a.VENDORID_CHR=b.VENDORID_CHR and a.STORAGEORDTYPEID_CHR=g.STORAGEORDTYPEID_CHR" + strWhere;
            strSQL += @" union all select distinct c.MEDSTOREID_CHR as VENDORID_CHR,c.MEDSTORENAME_VCHR as VENDORNAME_VCHR from T_OPR_STORAGEORD a,t_bse_medstore c,t_aid_storageordtype g where  a.PSTATUS_INT=2 and a.DEPTID_CHR=c.MEDSTOREID_CHR and a.STORAGEORDTYPEID_CHR=g.STORAGEORDTYPEID_CHR" + strWhere;
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtVendor);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (strSTANDARD != "2")
            {
                if (strSTANDARD == "0")
                {
                    strWhere += @"  and (b.AIMUNIT_INT='" + strSTANDARD + "' or b.AIMUNIT_INT is null)";
                }
                else
                {
                    strWhere += @"  and b.AIMUNIT_INT='" + strSTANDARD + "'";
                }
            }
            strSQL = @"select a.VENDORID_CHR,f.VENDORNAME_VCHR,b.*,d.MEDICINENAME_VCHR,d.ASSISTCODE_CHR,d.MEDSPEC_VCHR,d.PRODUCTORID_CHR,d.PERMITNO_VCHR,d.STANDARD_INT,e.MEDICINEPREPTYPENAME_VCHR ,d.UNITPRICE_MNY from T_OPR_STORAGEORD a ,T_OPR_STORAGEORDDE b,t_bse_storage c,T_BSE_MEDICINE d,T_AID_MEDICINEPREPTYPE  e,T_BSE_VENDOR f,t_aid_storageordtype g  where  a.PSTATUS_INT=2 and a.VENDORID_CHR is not null and a.STORAGEID_CHR=c.STORAGEID_CHR  and  a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and b.MEDICINEID_CHR=d.MEDICINEID_CHR and d.MEDICINEPREPTYPE_CHR=e.MEDICINEPREPTYPE_CHR(+) and a.VENDORID_CHR=f.VENDORID_CHR and a.STORAGEORDTYPEID_CHR=g.STORAGEORDTYPEID_CHR and d.MEDICINETYPEID_CHR='" + strMEDICINETYPE + "'" + strWhere;
            strSQL += @" union all select f.MEDSTOREID_CHR as VENDORID_CHR,f.MEDSTORENAME_VCHR as VENDORNAME_VCHR,b.*,d.MEDICINENAME_VCHR,d.ASSISTCODE_CHR,d.MEDSPEC_VCHR,d.PRODUCTORID_CHR,d.PERMITNO_VCHR,d.STANDARD_INT,e.MEDICINEPREPTYPENAME_VCHR ,d.UNITPRICE_MNY from T_OPR_STORAGEORD a ,T_OPR_STORAGEORDDE b,t_bse_storage c,T_BSE_MEDICINE d,T_AID_MEDICINEPREPTYPE  e,t_bse_medstore f,t_aid_storageordtype g  where  a.PSTATUS_INT=2 and a.DEPTID_CHR is not null and a.STORAGEID_CHR=c.STORAGEID_CHR and a.STORAGEORDTYPEID_CHR=g.STORAGEORDTYPEID_CHR and  a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and b.MEDICINEID_CHR=d.MEDICINEID_CHR and d.MEDICINEPREPTYPE_CHR=e.MEDICINEPREPTYPE_CHR(+) and a.DEPTID_CHR=f.MEDSTOREID_CHR and d.MEDICINETYPEID_CHR='" + strMEDICINETYPE + "'" + strWhere;
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtde);
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

        #region 获取调价单数据
        [AutoComplete]
        public long m_lngGetChangPriceDataOfMonth(System.Collections.Generic.List<string> arrList, string strStorageType, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL;
            string strWherePeriod = "";
            if (arrList.Count > 0)
            {
                for (int i1 = 0; i1 < arrList.Count; i1++)
                {

                    if (i1 == 0)
                    {
                        strWherePeriod += @" and (a.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                    else
                    {
                        strWherePeriod += @" or a.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                }
                strWherePeriod += @" )";
            }
            if (strStorageType == "1")
                strSQL = @"select a.MEDICINEPRICECHGAPPLID_CHR,a.MEDICINEPRICECHGAPPLNO_CHR,a.ADUITDATE_DAT from T_OPR_MEDICINEPRICECHGAPPL a  where  PSTATUS_INT=2" + strWherePeriod;
            else
                strSQL = @"select a.MEDICINEPRICECHGAPPLID_CHR,a.MEDICINEPRICECHGAPPLNO_CHR,a.ADUITDATE_DAT from  T_OPR_MEDICINEPRICECHGAPPL a where  PSTATUS_INT=2" + strWherePeriod;
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
        #endregion


        #region 获取调价明细数据
        /// <summary>
        /// 获取调价明细数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <param name="date1">开始日期</param>
        /// <param name="date2">结束日期</param>
        /// <param name="intStatuse">0-全部,1-西药,2-中草药,3-中成药</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChangPriceDe(out DataTable dt, System.Collections.Generic.List<string> arrList, int intStatuse, bool bl)
        {
            long lngRes = 0;
            dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strWhere = "";
            switch (intStatuse)
            {
                case 0:
                    strWhere = " and (c.MEDICINETYPEID_CHR=1 or c.MEDICINETYPEID_CHR=2 or c.MEDICINETYPEID_CHR=3)";
                    break;
                case 1:
                    strWhere = " and c.MEDICINETYPEID_CHR=1";
                    break;
                case 2:
                    strWhere = " and c.MEDICINETYPEID_CHR=2";
                    break;
                case 3:
                    strWhere = " and c.MEDICINETYPEID_CHR=3";
                    break;
            }
            string strWherePeriod = "";
            if (arrList.Count > 0)
            {
                for (int i1 = 0; i1 < arrList.Count; i1++)
                {

                    if (i1 == 0)
                    {
                        strWherePeriod += @" and (e.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                    else
                    {
                        strWherePeriod += @" or e.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                }
                strWherePeriod += @" )";
            }
            if (!bl)
            {
                strWhere += " and b.CHANGEPRICE_MNY!=b.CURPRICE_MNY and b.curqty_dec is not null and b.curqty_dec!=0";
            }
            string strSQL;
            strSQL = @"select c.medicinename_vchr, c.assistcode_chr, c.medspec_vchr,
                b.unitid_chr AS unit_chr,b.CURPRICE_MNY as preprice_mny,b.CHANGEPRICE_MNY as curprice_mny,
                b.curqty_dec,(b.CHANGEPRICE_MNY-b.CURPRICE_MNY)*b.curqty_dec as balanceprice_mny, d.typename_chr
           FROM t_opr_medicinepricechgappl e, 
                t_opr_medicinepricechgapplde b,
                t_bse_medicine c,
                t_bse_medchagetype d
          WHERE e.medicinepricechgapplid_chr = b.medicinepricechgapplid_chr
            AND b.medicineid_chr = c.medicineid_chr
            AND b.typeid_chr = d.typeid_chr(+) and e.PSTATUS_INT=2" + strWhere + strWherePeriod;
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
        #endregion

        #region 获取调价明细数据
        [AutoComplete]
        public long m_lngGetChangPriceDeOfMonth(string ChangPriceID, string strStorageType, out DataTable dt, bool bl)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strWhere = "";
            if (!bl)
            {
                strWhere = @" and b.CHANGEPRICE_MNY!=b.CURPRICE_MNY and b.curqty_dec is not null and b.curqty_dec!=0";
            }

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL;
            if (strStorageType == "1")
                strSQL = @"select  c.MEDICINENAME_VCHR,c.ASSISTCODE_CHR,c.MEDSPEC_VCHR,b.UNITID_CHR as UNIT_CHR,b.CURPRICE_MNY as PREPRICE_MNY,b.CHANGEPRICE_MNY as CURPRICE_MNY,b.CURQTY_DEC,(b.CHANGEPRICE_MNY-b.CURPRICE_MNY)*b.curqty_dec as BALANCEPRICE_MNY,d.TYPENAME_CHR from  T_OPR_MEDICINEPRICECHGAPPLDE b,T_BSE_MEDICINE c,T_BSE_MEDCHAGETYPE d  where b.MEDICINEPRICECHGAPPLID_CHR='" + ChangPriceID + "' and a.MEDICINEID_CHR=c.MEDICINEID_CHR and b.TYPEID_CHR=d.TYPEID_CHR(+)" + strWhere;
            else
                strSQL = @"select  c.MEDICINENAME_VCHR,c.ASSISTCODE_CHR,c.MEDSPEC_VCHR,b.UNITID_CHR as UNIT_CHR,b.CURPRICE_MNY as PREPRICE_MNY,b.CHANGEPRICE_MNY as CURPRICE_MNY,b.CURQTY_DEC,(b.CHANGEPRICE_MNY-b.CURPRICE_MNY)*b.curqty_dec as BALANCEPRICE_MNY,d.TYPENAME_CHR from  T_OPR_MEDICINEPRICECHGAPPLDE b,T_BSE_MEDICINE c,T_BSE_MEDCHAGETYPE d  where b.MEDICINEPRICECHGAPPLID_CHR='" + ChangPriceID + "'  and b.MEDICINEID_CHR=c.MEDICINEID_CHR and b.TYPEID_CHR=d.TYPEID_CHR(+)" + strWhere;

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
        #endregion
        #region 药品库存积压报表
        [AutoComplete]
        public long m_lngGetOverStock(string storageID, int TimeSpace, int intStau, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = "";
            switch (intStau)
            {
                case 1:
                    DateTime date1 = DateTime.Now.Date;
                    date1.AddMonths(-TimeSpace);
                    strSQL = @"select a.STORAGEID_CHR,a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.CURQTY_DEC,a.UNITID_CHR,d.UNITPRICE_MNY as SALEUNITPRICE_MNY,d.MEDICINENAME_VCHR,d.MEDSPEC_VCHR,d.ASSISTCODE_CHR,(a.CURQTY_DEC*d.UNITPRICE_MNY) as totailMoney from t_opr_storagemeddetail a,T_OPR_STORAGEORDDE b,T_OPR_STORAGEORD c,T_BSE_MEDICINE d where a.STORAGEID_CHR='" + storageID + "' and a.FLAG_INT=0 and a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.SYSLOTNO_CHR=b.SYSLOTNO_CHR and b.STORAGEORDID_CHR=c.STORAGEORDID_CHR and c.ADUITDATE_DAT<=to_date('" + date1.ToString() + "','yyyy-mm-dd hh24:mi:ss') and b.SIGN_INT=1 and a.MEDICINEID_CHR=d.MEDICINEID_CHR and d.IFSTOP_INT=0 and a.CURQTY_DEC>0";
                    break;
                case 2:
                    strSQL = @"select a.MEDICINEID_CHR,a.AMOUNT_DEC as CURQTY_DEC,a.UNITID_CHR,c.MEDICINENAME_VCHR,c.MEDSPEC_VCHR,c.ASSISTCODE_CHR,c.UNITPRICE_MNY as SALEUNITPRICE_MNY,(a.AMOUNT_DEC*c.UNITPRICE_MNY) as totailMoney from t_bse_storagemedicine a,t_bse_storagemedlimit b,t_bse_medicine c where a.STORAGEID_CHR='" + storageID + "' and  a.STORAGEID_CHR=b.STORAGEID_CHR and a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.AMOUNT_DEC>b.HIGHLIMIT_DEC and a.MEDICINEID_CHR=c.MEDICINEID_CHR";
                    break;
                case 3:
                    strSQL = @"select a.MEDICINEID_CHR,a.AMOUNT_DEC as CURQTY_DEC,a.UNITID_CHR,c.MEDICINENAME_VCHR,c.MEDSPEC_VCHR,c.ASSISTCODE_CHR,c.UNITPRICE_MNY as SALEUNITPRICE_MNY,(a.AMOUNT_DEC*c.UNITPRICE_MNY) as totailMoney from t_bse_storagemedicine a,t_bse_storagemedlimit b,t_bse_medicine c where a.AMOUNT_DEC>0 and a.STORAGEID_CHR='" + storageID + "' and a.STORAGEID_CHR=b.STORAGEID_CHR and a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.AMOUNT_DEC<b.LOWLIMIT_DEC and a.MEDICINEID_CHR=c.MEDICINEID_CHR";
                    break;

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
        #endregion

    }
}
