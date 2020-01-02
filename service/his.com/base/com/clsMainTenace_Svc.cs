using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMainTenace_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 基本配置
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExistSetting(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select bedchargecate,
                                   eatdiccate,
                                   nursecate,
                                   autochargeitemtype,
                                   freqid_chr,
                                   ordercateid_leave_chr,
                                   ordercateid_transfer_chr,
                                   leaverange_int,
                                   ordercateid_medicine_chr,
                                   confreqid_chr,
                                   ischeckmed,
                                   isorderexclude,
                                   iscontrlmoneyactived,
                                   ishosnumatuo,
                                   seq_int,
                                   ordercateid_lis_chr,
                                   usageid_chr,
                                   mid_medicine_chr
                              from t_bse_bih_specordercate";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// //获取住院发票类别
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetBEDCHARGECATE(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select typeid_chr,
                                     typename_vchr,
                                     flag_int,
                                     usercode_chr,
                                     sortcode_int,
                                     govtopcharge_mny,
                                     emrcat_vchr
                                from t_bse_chargeitemextype
                               where flag_int = '4'
                               order by sortcode_int";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// 获取收费类别
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetEATDICCATE(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select ordercateid_chr,
                                       name_chr,
                                       des_vchr,
                                       sourcetable_vchr,
                                       tablepk_vchr,
                                       dllname_vchr,
                                       classname_vchr,
                                       opradd_vchr,
                                       oprupd_vchr,
                                       isattach_int,
                                       viewname_vchr,
                                       usageviewtype,
                                       dosageviewtype,
                                       createchargetype,
                                       iscontrolmoney,
                                       feqviewtype,
                                       appendviewtype_int,
                                       qtyviewtype_int,
                                       orderseq_int,
                                       usercode_vchr,
                                       autoshow_int,
                                       orderselect_int,
                                       sameorder_int,
                                       changetype_int
                                  from t_aid_bih_ordercate";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// 获取用法列表
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetUsageType(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select usageid_chr,usagename_vchr
				from t_bse_usagetype order by usercode_chr";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// 临时医嘱特定频率
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetFREQID_CHR(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select freqid_chr,
                                     freqname_chr,
                                     usercode_chr,
                                     times_int,
                                     days_int,
                                     lexectime_vchr,
                                     texectime_vchr,
                                     execweekday_chr,
                                     printdesc_vchr,
                                     opfredesc_vchr
                              from  t_aid_recipefreq";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// 获取医嘱类别
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long longGetOrderCateID(out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @" select	a.ordercateid_chr, a.name_chr, a.des_vchr, a.sourcetable_vchr,
						a.tablepk_vchr, a.dllname_vchr, a.classname_vchr, a.opradd_vchr,
						a.oprupd_vchr, a.isattach_int
				from t_aid_bih_ordercate a";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                objHRPSvc = null;
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
        /// 获取医嘱类别
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetISHOSNUMATUO(out int intResult)
        {
            DataTable dtResult = new DataTable();
            long lngRes = 0;
            intResult = 0;
            string strSQL = @" select a.ishosnumatuo from t_bse_bih_specordercate a";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                intResult = int.Parse(dtResult.Rows[0][0].ToString());
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngCheckExist(out int p_intExist)
        {
            DataTable dtResult = new DataTable();
            long lngRes = 0;
            p_intExist = 0;
            string strSQL = @"select Count(*) as c from t_bse_bih_specordercate";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                p_intExist = int.Parse(dtResult.Rows[0][0].ToString());
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngDoExcute(string strSQL)
        {
            DataTable dtResult = new DataTable();
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
