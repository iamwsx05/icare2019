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
    public class clsAreaMedQuery : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region 中心药房发药明细汇总


        /// <summary>
        /// 中心药房发药明细汇总


        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_strGoupId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMedicineByDate( string p_strBegin, string p_strEnd, string p_strAreaId, string p_strGoupId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            StringBuilder strSQL = new StringBuilder("");
            p_dtResult = new DataTable();
            //if (p_strGoupId == null || p_strGoupId == "")
            //{
            //    p_strGoupId = "%";
            //}                   
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out arrParams);

                arrParams[0].Value = DateTime.Parse(p_strBegin);
                arrParams[1].Value = DateTime.Parse(p_strEnd);

                if (p_strAreaId.Trim().Length > 0)
                {
                    strSQL.Append(@"select c.deptname_vchr as groupname_vchr,
                                   b.assistcode_chr, 
                                   b.medicinename_vchr, 
                                   b.medspec_vchr, 
                                   a.unitprice_mny,
                                   b.tradeprice_mny,
                                   b.packqty_dec,
                                   sum(a.get_dec) as get_dec
                               from t_bih_opr_putmeddetail   a,
                                    t_bse_medicine           b,
                                    t_bse_deptdesc           c,
                                    t_opr_bih_patientcharge  d
                                where a.areaid_chr = c.deptid_chr
                                   and a.medid_chr = b.medicineid_chr
                                   and a.pchargeid_chr = d.pchargeid_chr
                                   and a.isput_int = 1                                             
                                   and a.pubdate_dat >=?
                                   and a.pubdate_dat <=?
                                   and c.deptid_chr = ? 
                                group by  
                                       c.deptname_vchr,                                          
                                       b.assistcode_chr,
                                       b.medicinename_vchr,
                                       b.medspec_vchr,
                                       b.tradeprice_mny, 
                                       a.unitprice_mny,
                                       b.packqty_dec");

                    arrParams[2].Value = p_strAreaId;
                    
                }

                else if (p_strGoupId.Trim().Length > 0)
                {
                    strSQL.Append(@"select e.groupname_vchr,
                                   b.assistcode_chr, 
                                   b.medicinename_vchr, 
                                   b.medspec_vchr, 
                                   a.unitprice_mny,
                                   b.tradeprice_mny,
                                   b.packqty_dec,
                                   sum(a.get_dec) as get_dec
                               from t_bih_opr_putmeddetail   a,
                                    t_bse_medicine           b,
                                    t_opr_bih_patientcharge  d,
                                    t_bse_groupdesc          e
                                where  a.medid_chr = b.medicineid_chr
                                   and a.pchargeid_chr = d.pchargeid_chr
                                   and d.doctorgroupid_chr = e.groupid_chr(+)
                                   and a.isput_int = 1                                              
                                   and a.pubdate_dat >=?
                                   and a.pubdate_dat <=?
                                   and e.groupid_chr = ? 
                                group by 
                                    e.groupname_vchr,
                                    b.assistcode_chr,
                                    b.medicinename_vchr,
                                    b.medspec_vchr,
                                    b.tradeprice_mny, 
                                    a.unitprice_mny,
                                    b.packqty_dec");

                    arrParams[2].Value = p_strGoupId;
                }


                
                
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, arrParams);
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
        public long GetChargeMedDetailByDate(  string p_strBegin,
            string p_strEnd,
            string p_strAreaId,
            string p_strGoupId,
            string p_strMedType, 
            out DataTable p_dtResult)
        {
            long lngRes = 0;
            StringBuilder strSQL = new StringBuilder ("");
            p_dtResult = new DataTable(); 
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objDPArr = null;

                                

                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                //if (p_strGoupId == null || p_strGoupId == "")
                //{
                //    p_strGoupId = "%";
                //}
                               
                objDPArr[0].Value = DateTime.Parse(p_strBegin);
                objDPArr[1].Value = DateTime.Parse(p_strEnd);

                if (p_strAreaId.Trim().Length > 0)
                {
                    strSQL.Append(@"select 
                                    a.unitprice_dec,
                                    b.assistcode_chr,
                                    b.medicinename_vchr,
                                    b.medspec_vchr,
                                    b.tradeprice_mny,
                                    b.packqty_dec,       
                                    c.deptname_vchr as groupname_vchr,
                                    sum(a.amount_dec) as get_dec
                                from  t_opr_bih_patientcharge a
                                      inner join t_bse_medicine b on a.chargeitemid_chr = b.medicineid_chr
                                            and a.amount_dec < 0
                                            and a.status_int = 1
                                            and b.putmedtype_int = 1
                                            and ( b.medicnetype_int in (" + p_strMedType + @") )
                                      inner join t_bse_deptdesc c on a.curareaid_chr = c.deptid_chr
                                where (a.chargeactive_dat >=?)
                                    and (a.chargeactive_dat <=?)
                                    and c.deptid_chr = ? 
                                group by a.unitprice_dec,
                                         b.assistcode_chr,
                                         b.medicinename_vchr,
                                         b.medspec_vchr,
                                         b.tradeprice_mny,
                                         b.packqty_dec,
                                         c.deptname_vchr");

                    objDPArr[2].Value = p_strAreaId;
                    
                }

                else if (p_strGoupId.Trim().Length > 0)
                {
                    strSQL.Append(@"select 
                                    a.unitprice_dec,
                                    b.assistcode_chr,
                                    b.medicinename_vchr,
                                    b.medspec_vchr,
                                    b.tradeprice_mny,
                                    b.packqty_dec,       
                                    d.groupname_vchr,
                                    sum(a.amount_dec) as get_dec
                                from  t_opr_bih_patientcharge a
                                      inner join t_bse_medicine b on a.chargeitemid_chr = b.medicineid_chr
                                            and a.amount_dec < 0
                                            and a.status_int = 1
                                            and b.putmedtype_int = 1
                                            and ( b.medicnetype_int in (" + p_strMedType + @") )
                                      inner join t_bse_groupdesc d on a.doctorgroupid_chr = d.groupid_chr
                                where (a.chargeactive_dat >=?)
                                    and (a.chargeactive_dat <=?)
                                    and d.groupid_chr = ? 
                                group by a.unitprice_dec,
                                         b.assistcode_chr,
                                         b.medicinename_vchr,
                                         b.medspec_vchr,
                                         b.tradeprice_mny,
                                         b.packqty_dec,
                                         d.groupname_vchr");

                    objDPArr[2].Value = p_strGoupId;
                }


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
