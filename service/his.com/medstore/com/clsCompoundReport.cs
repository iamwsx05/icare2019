using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsCompoundReport 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCompoundReport : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsCompoundReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region
        [AutoComplete]
        public long m_lngGetData(out DataTable dt, string p_strType)
        {
            dt = null;
            long lngRes = 0;
            string strSQL = "";
            switch (p_strType)
            {
                case "1":
                    strSQL = @"SELECT T_AID_MEDICINETYPE.MEDICINETYPENAME_VCHR, 
             COUNT(T_AID_MEDICINETYPE.MEDICINETYPENAME_VCHR) AS ROWCOUNT
            FROM T_AID_MEDICINETYPE, T_BSE_MEDICINE
            WHERE T_AID_MEDICINETYPE.MEDICINETYPEID_CHR = T_BSE_MEDICINE.MEDICINETYPEID_CHR
            GROUP BY T_AID_MEDICINETYPE.MEDICINETYPENAME_VCHR";
                    break;
                case "2":
                    strSQL = @"select '毒麻药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISANAESTHESIA_CHR='T' union select '非毒麻药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISANAESTHESIA_CHR='F'";
                    break;
                case "3":
                    strSQL = @"select '精神药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCHLORPROMAZINE_CHR='T' union select '非精神药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCHLORPROMAZINE_CHR='F'";
                    break;
                case "4":
                    strSQL = @"select '贵重药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCOSTLY_CHR='T' union select '非贵重药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCOSTLY_CHR='F'";
                    break;
                case "5":
                    strSQL = @"select '院内制剂' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISSELF_CHR='T' union select '非院内制剂' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISSELF_CHR='F'";
                    break;
                case "6":
                    //strSQL=@"select '进口药' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISIMPORT_CHR='T' union select '非进口药' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISIMPORT_CHR='F'";
                    strSQL = @"select '进口药' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'T'
union
select '国产药' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'F'
 union
select '合资药' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'H'";
                    break;
                case "7":
                    strSQL = @"select '中标药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where STANDARD_INT=1 union select '非中标药品' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where STANDARD_INT=0";
                    break;
                case "8":
                    DataTable dt1 = new DataTable();
                    strSQL = @"select PAYTYPEID_CHR from t_bse_patientPaytype where INTERNALFLAG_INT=2";
                    try
                    {
                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt1);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    strSQL = @"select '甲类' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.PRECENT_DEC=0 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "'  union select '乙类' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.ITEMID_CHR=a.ITEMID_CHR and b.PRECENT_DEC>0 and b.PRECENT_DEC<100  and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "' union select '自费' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and  b.PRECENT_DEC=100 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "'";
                    break;
                case "9":
                    DataTable dt2 = new DataTable();
                    strSQL = @"select PAYTYPEID_CHR from t_bse_patientPaytype where INTERNALFLAG_INT=1";
                    try
                    {
                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt2);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    strSQL = @"select '公费' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.PRECENT_DEC<100 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt2.Rows[0]["PAYTYPEID_CHR"].ToString() + "'  union select '自费' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.ITEMID_CHR=a.ITEMID_CHR and b.PRECENT_DEC=100  and b.COPAYID_CHR='" + dt2.Rows[0]["PAYTYPEID_CHR"].ToString() + "'";
                    break;
            }

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
    }
}
