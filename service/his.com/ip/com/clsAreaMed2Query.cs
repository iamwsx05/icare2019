using System;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAreaMed2Query : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region 中心药房发药汇总


        /// <summary>
        /// 中心药房发药汇总


        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strGoupId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMed2icineByDate(string p_strBegin, string p_strEnd, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"select d.groupid_chr,
                                     d.groupname_vchr,
                                     sum(a.get_dec * b.tradeprice_mny) as callsum,
                                     sum(a.get_dec * a.unitprice_mny) as retailsum
                                from t_bih_opr_putmeddetail  a,
                                     t_bse_medicine          b,
                                     t_opr_bih_patientcharge c,
                                     t_bse_groupdesc         d
                                where a.medid_chr = b.medicineid_chr
                                     and a.pchargeid_chr = c.pchargeid_chr
                                     and c.doctorgroupid_chr = d.groupid_chr(+)
                                     and a.isput_int = 1
                                     and a.pubdate_dat >=?
                                     and a.pubdate_dat <=?
                                group by d.groupid_chr, d.groupname_vchr
                                    order by d.groupid_chr";

            //            string strSQL = @"select d.groupid_chr,
            //                                e.groupname_vchr as 统计组名称,
            //                                sum(a.GET_DEC*b.tradeprice_mny) as 购进金额合计,
            //                                sum(a.GET_DEC*b.UNITPRICE_MNY) as 零售金额合计,
            //                                sum(b.UNITPRICE_MNY-b.tradeprice_mny) as 购零差额合计
            //                                from T_BIH_OPR_PUTMEDDETAIL    a,
            //                                T_BSE_MEDICINE           b,
            //                                T_BSE_DEPTDESC           c,
            //                                t_opr_bih_patientcharge  d,
            //                                t_bse_groupdesc          e 
            //                                where a.AREAID_CHR = c.DEPTID_CHR
            //                                and a.MEDID_CHR = b.MEDICINEID_CHR
            //                                and a.pchargeid_chr = d.pchargeid_chr
            //                                and d.doctorgroupid_chr = e.groupid_chr(+)
            //                                and a.ISPUT_INT = 1
            //                                and a.pubdate_dat >=?
            //                                and a.pubdate_dat <=?
            //                                GROUP BY  e.groupid_chr,e.groupname_vchr
            //                                order by e.groupid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                objHRPSvc.CreateDatabaseParameter(2, out arrParams);

                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 查询中心药房退药明细

        /// <summary>
        /// 查询中心药房退药明细

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetChargeMedDetailByDate(string p_strBegin, string p_strEnd, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            StringBuilder strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objDPArr = null;

                strSQL = new StringBuilder(@"select 
                                                c.groupid_chr,
                                                c.groupname_vchr ,
                                                sum(a.amount_dec * b.tradeprice_mny) callsum,
                                                sum(a.amount_dec * a.UNITPRICE_DEC) retailsum
                                            from t_opr_bih_patientcharge a,
                                                 t_bse_medicine b,
                                                 t_bse_groupdesc c
                                           where a.chargeitemid_chr = b.medicineid_chr
                                             and a.doctorgroupid_chr = c.groupid_chr
                                             and a.amount_dec < 0
                                             and a.status_int = 1
                                             and b.putmedtype_int = 1
                                             and a.chargeactive_dat >= ?
                                             and a.chargeactive_dat <= ? 
                                             and ( b.medicnetype_int in (" + p_strMedType + @") )
                                            group by c.groupid_chr, c.groupname_vchr
                                            order by c.groupid_chr");

                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = DateTime.Parse(p_strBegin);
                objDPArr[1].Value = DateTime.Parse(p_strEnd);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, objDPArr);
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
