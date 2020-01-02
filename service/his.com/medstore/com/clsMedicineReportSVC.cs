using System;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药品相关查询 by shaowei.zheng on 22 June 2010.
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsMedicineReportSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取药品目录
        /// <summary>
        /// 获取药品目录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineList(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"select distinct 1 as itemno,
                a.itemcode_vchr,
                a.itemname_vchr,
                b.medspec_vchr,
                b.opunit_chr,
                round(b.unitprice_mny, 4) as unitprice_mny,
                b.productorid_chr,
                c.vendorname_vchr,
                b.medicineid_chr,
                a.itemid_chr
  from t_bse_chargeitem a,
       t_bse_medicine b,
       (select distinct t2.medicineid_chr, t3.vendorname_vchr
          from t_ms_instorage t1, t_ms_instorage_detal t2, t_bse_vendor t3
         where t1.seriesid_int = t2.seriesid2_int
           and t1.vendorid_chr = t3.vendorid_chr) c
 where a.itemsrcid_vchr = b.medicineid_chr
   and b.medicineid_chr = c.medicineid_chr(+)
 order by a.itemcode_vchr";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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
        #endregion

        #region 根据条件获取药品信息
        /// <summary>
        /// 根据条件获取药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngImpMedItem(string p_strVal, out DataTable p_dtResult)
        {
            long lngRes = 0L;
            string strSQL = @"select a.itemid_chr,
       b.medicineid_chr,
       a.itemcode_vchr,
       a.itemname_vchr,
       b.medspec_vchr
  from t_bse_chargeitem a, t_bse_medicine b
 where a.itemsrcid_vchr = b.medicineid_chr
   and a.itemcode_vchr in (" + p_strVal + ")";
            p_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strVal;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
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
        #endregion

        #region 根据条件获取药品信息
        /// <summary>
        /// 根据条件获取药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedItem(string p_strVal, out DataTable p_dtResult)
        {
            long lngRes = 0L;
            string strSQL = @"select a.itemid_chr,
       b.medicineid_chr,
       a.itemcode_vchr,
       a.itemname_vchr,
       b.medspec_vchr
  from t_bse_chargeitem a, t_bse_medicine b
 where a.itemsrcid_vchr = b.medicineid_chr
   and (upper(a.itemcode_vchr) like ? or upper(a.itemname_vchr) like ? or
       upper(a.itempycode_chr) like ? or upper(a.itemwbcode_chr) like ?)
 order by a.itemname_vchr";
            p_dtResult = new DataTable();
            p_strVal = p_strVal.ToUpper() + "%";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strVal;
                objDPArr[1].Value = p_strVal;
                objDPArr[2].Value = p_strVal;
                objDPArr[3].Value = p_strVal;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
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
        #endregion

        #region 医生用药
        /// <summary>
        /// 医生用药
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dteBegin"></param>
        /// <param name="p_dteEnd"></param>
        /// <param name="p_strItemIDArr"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatDoctUseMed(DateTime p_dteBegin, DateTime p_dteEnd, string p_strItemIDArr, out DataTable p_dtResult)
        {
            long lngRes = 0L;
            string strSQL = @"select tb.deptname,
       tb.doctname,
       tb.itemcode_vchr,
       tb.itemname_vchr,
       tb.itemspec_vchr,
       round(tb.unitprice_mny, 4) as unitprice_mny,
       sum(tb.tolqty) as totalqty,
       sum(totalsum) as totalsum,
       tb.depttype
  from (select nvl(e.deptname_vchr, '未注明') as deptname,
               nvl(c.lastname_vchr, '未注明') as doctname,
               d.itemcode_vchr,
               b.itemname_vchr,
               b.itemspec_vchr,
               b.unitprice_mny,
               sum(b.tolqty_dec) as tolqty,
               sum(b.tolprice_mny) as totalsum,
               1 as depttype
          from t_opr_outpatientrecipe      a,
               t_opr_outpatientpwmrecipede b,
               t_bse_employee              c,
               t_bse_chargeitem            d,
               t_bse_deptdesc              e
         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
           and a.diagdr_chr = c.empid_chr(+)
           and b.itemid_chr = d.itemid_chr
           and a.diagdept_chr = e.deptid_chr(+)
           and a.pstauts_int = 2
           and (a.recorddate_dat between ? and ?)
           and (b.itemid_chr in (" + p_strItemIDArr + @"))
         group by e.deptname_vchr,
                  c.lastname_vchr,
                  d.itemcode_vchr,
                  b.itemname_vchr,
                  b.itemspec_vchr,
                  b.unitprice_mny
        union all
        select nvl(e.deptname_vchr, '未注明') as deptname,
               nvl(c.lastname_vchr, '未注明') as doctname,
               d.itemcode_vchr,
               b.itemname_vchr,
               b.itemspec_vchr,
               b.unitprice_mny,
               sum(b.times_int * min_qty_dec) as tolqty,
               sum(b.tolprice_mny) as totalsum,
               1 as depttype
          from t_opr_outpatientrecipe     a,
               t_opr_outpatientcmrecipede b,
               t_bse_employee             c,
               t_bse_chargeitem           d,
               t_bse_deptdesc             e
         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
           and a.diagdr_chr = c.empid_chr(+)
           and b.itemid_chr = d.itemid_chr
           and a.diagdept_chr = e.deptid_chr(+)
           and a.pstauts_int = 2
           and (a.recorddate_dat between ? and ?)
           and (b.itemid_chr in (" + p_strItemIDArr + @"))
         group by e.deptname_vchr,
                  c.lastname_vchr,
                  d.itemcode_vchr,
                  b.itemname_vchr,
                  b.itemspec_vchr,
                  b.unitprice_mny
        union all
        select nvl(e.deptname_vchr, '未注明') as deptname,
               nvl(c.lastname_vchr, '未注明') as doctname,
               d.itemcode_vchr,
               b.itemname_vchr,
               b.itemspec_vchr,
               b.unitprice_mny,
               sum(b.qty_dec) as tolqty,
               sum(b.tolprice_mny) as totalsum,
               1 as depttype
          from t_opr_outpatientrecipe      a,
               t_opr_outpatientothrecipede b,
               t_bse_employee              c,
               t_bse_chargeitem            d,
               t_bse_deptdesc              e
         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
           and a.diagdr_chr = c.empid_chr(+)
           and b.itemid_chr = d.itemid_chr
           and a.diagdept_chr = e.deptid_chr(+)
           and a.pstauts_int = 2
           and (a.recorddate_dat between ? and ?)
           and (b.itemid_chr in (" + p_strItemIDArr + @"))
         group by e.deptname_vchr,
                  c.lastname_vchr,
                  d.itemcode_vchr,
                  b.itemname_vchr,
                  b.itemspec_vchr,
                  b.unitprice_mny
        union all
        select nvl(d.deptname_vchr, '未注明') as deptname,
               nvl(c.lastname_vchr, '未注明') as doctname,
               b.itemcode_vchr,
               a.chargeitemname_chr as itemname_vchr,
               a.spec_vchr as itemspec_vchr,
               a.unitprice_dec as unitprice_mny,
               sum(a.amount_dec) as tolqty,
               sum(round(a.unitprice_dec * a.amount_dec, 2)) as totalsum,
               2 as depttype
          from t_opr_bih_patientcharge a,
               t_bse_chargeitem        b,
               t_bse_employee          c,
               t_bse_deptdesc          d
         where a.chargeitemid_chr = b.itemid_chr
           and a.chargedoctorid_chr = c.empid_chr(+)
           and a.createarea_chr = d.deptid_chr(+)
           and a.pstatus_int <> 0
           and a.status_int = 1
           and (a.chargeactive_dat between ? and ?)
           and (a.chargeitemid_chr in (" + p_strItemIDArr + @"))
         group by d.deptname_vchr,
                  c.lastname_vchr,
                  b.itemcode_vchr,
                  a.chargeitemname_chr,
                  a.spec_vchr,
                  a.unitprice_dec) tb
 group by tb.deptname,
          tb.doctname,
          tb.itemcode_vchr,
          tb.itemname_vchr,
          tb.itemspec_vchr,
          tb.unitprice_mny,
          tb.depttype";
            p_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_dteBegin;
                objDPArr[1].Value = p_dteEnd;
                objDPArr[2].Value = p_dteBegin;
                objDPArr[3].Value = p_dteEnd;
                objDPArr[4].Value = p_dteBegin;
                objDPArr[5].Value = p_dteEnd;
                objDPArr[6].Value = p_dteBegin;
                objDPArr[7].Value = p_dteEnd;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
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
        #endregion

        #region 库存
        /// <summary>
        /// 库存
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemIDArr"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatMedStock(string p_strItemIDArr, out DataTable p_dtResult)
        {
            long lngRes = 0L;
            string strSQL = @"select distinct d.itemcode_vchr,
                d.itemname_vchr as medicinename_vchr,
                b.productorid_chr,
                b.medspec_vchr,
                b.opunit_vchr,
                decode(b.lotno_vchr, '未注明', '', b.lotno_vchr) lotno_vchr,
                b.validperiod_dat,
                b.realgross_int,
                b.availagross_int,
                b.callprice_int,
                b.retailprice_int,
                b.wholesaleprice_int
  from t_ms_storage        a,
       t_ms_storage_detail b,
       t_bse_medicine      c,
       t_bse_chargeitem    d
 where a.medicineid_chr = b.medicineid_chr
   and a.storageid_chr = b.storageid_chr
   and b.medicineid_chr = c.medicineid_chr
   and c.medicineid_chr = d.itemsrcid_vchr
   and b.status = 1
   and (d.itemid_chr in (" + p_strItemIDArr + @"))
 order by d.itemcode_vchr, lotno_vchr";
            p_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #endregion

        #region 出入库
        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dteBegin"></param>
        /// <param name="p_dteEnd"></param>
        /// <param name="p_strItemIDArr"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatMedInOutStore(DateTime p_dteBegin, DateTime p_dteEnd, string p_strItemIDArr, out DataTable p_dtResult)
        {
            long lngRes = 0L;
            string strSQL = @"select e.itemcode_vchr,
       e.itemname_vchr as medicinename_vch,
       b.lotno_vchr,
       b.medspec_vchr,
       b.amount as inamount,
       0 as outamount,
       d.opunit_chr as unit,
       round(b.callprice_int, 4) as callprice_int,
       round(b.retailprice_int, 4) as retailprice_int,
       a.instoragedate_dat as meddate,
       f.vendorname_vchr as deptname
  from t_ms_instorage       a,
       t_ms_instorage_detal b,
       t_bse_medicine       d,
       t_bse_chargeitem     e,
       t_bse_vendor         f
 where a.seriesid_int = b.seriesid2_int
   and b.medicineid_chr = d.medicineid_chr
   and d.medicineid_chr = e.itemsrcid_vchr
   and a.vendorid_chr = f.vendorid_chr(+)
   and (a.state_int = 3 or a.state_int = 2)
   and b.status = 1
   and (a.instoragedate_dat between ? and ?)
   and (e.itemid_chr in (" + p_strItemIDArr + @"))
union all
select d.itemcode_vchr,
       d.itemname_vchr as medicinename_vch,
       b.lotno_vchr,
       b.medspec_vchr,
       0 as inamount,
       b.netamount_int as outamount,
       b.opunit_chr as unit,
       round(b.callprice_int, 4) as callprice_int,
       round(b.retailprice_int, 4) as retailprice_int,
       a.outstoragedate_dat as meddate,
       f.deptname_vchr as deptname
  from t_ms_outstorage        a,
       t_ms_outstorage_detail b,
       t_bse_medicine         c,
       t_bse_chargeitem       d,
       t_bse_deptdesc         f
 where a.seriesid_int = b.seriesid2_int
   and b.medicineid_chr = c.medicineid_chr
   and c.medicineid_chr = d.itemsrcid_vchr
   and a.askdept_chr = f.deptid_chr(+)
   and b.status = 1
   and (a.status = 2 or a.status = 3)
   and (a.outstoragedate_dat between ? and ?)
   and (d.itemid_chr in (" + p_strItemIDArr + "))";
            p_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_dteBegin;
                objDPArr[1].Value = p_dteEnd;
                objDPArr[2].Value = p_dteBegin;
                objDPArr[3].Value = p_dteEnd;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
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
        #endregion
    }
}
