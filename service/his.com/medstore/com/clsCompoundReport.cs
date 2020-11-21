using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsCompoundReport ��ժҪ˵����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCompoundReport : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsCompoundReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
                    strSQL = @"select '����ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISANAESTHESIA_CHR='T' union select '�Ƕ���ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISANAESTHESIA_CHR='F'";
                    break;
                case "3":
                    strSQL = @"select '����ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCHLORPROMAZINE_CHR='T' union select '�Ǿ���ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCHLORPROMAZINE_CHR='F'";
                    break;
                case "4":
                    strSQL = @"select '����ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCOSTLY_CHR='T' union select '�ǹ���ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISCOSTLY_CHR='F'";
                    break;
                case "5":
                    strSQL = @"select 'Ժ���Ƽ�' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISSELF_CHR='T' union select '��Ժ���Ƽ�' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISSELF_CHR='F'";
                    break;
                case "6":
                    //strSQL=@"select '����ҩ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISIMPORT_CHR='T' union select '�ǽ���ҩ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where ISIMPORT_CHR='F'";
                    strSQL = @"select '����ҩ' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'T'
union
select '����ҩ' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'F'
 union
select '����ҩ' as MEDICINETYPENAME_VCHR,
			 count(MEDICINENAME_VCHR) as ROWCOUNT
	from T_BSE_MEDICINE
 where ISIMPORT_CHR = 'H'";
                    break;
                case "7":
                    strSQL = @"select '�б�ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where STANDARD_INT=1 union select '���б�ҩƷ' as MEDICINETYPENAME_VCHR,count(MEDICINENAME_VCHR) as ROWCOUNT from T_BSE_MEDICINE where STANDARD_INT=0";
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
                    strSQL = @"select '����' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.PRECENT_DEC=0 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "'  union select '����' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.ITEMID_CHR=a.ITEMID_CHR and b.PRECENT_DEC>0 and b.PRECENT_DEC<100  and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "' union select '�Է�' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and  b.PRECENT_DEC=100 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt1.Rows[0]["PAYTYPEID_CHR"].ToString() + "'";
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
                    strSQL = @"select '����' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.PRECENT_DEC<100 and b.ITEMID_CHR=a.ITEMID_CHR and b.COPAYID_CHR='" + dt2.Rows[0]["PAYTYPEID_CHR"].ToString() + "'  union select '�Է�' as MEDICINETYPENAME_VCHR,count(a.itemid_chr) as ROWCOUNT from t_bse_chargeitem a ,t_aid_inschargeitem b where a.ITEMSRCTYPE_INT=1 and b.ITEMID_CHR=a.ITEMID_CHR and b.PRECENT_DEC=100  and b.COPAYID_CHR='" + dt2.Rows[0]["PAYTYPEID_CHR"].ToString() + "'";
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
