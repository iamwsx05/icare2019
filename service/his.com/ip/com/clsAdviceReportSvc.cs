using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{

    /// <summary>
    /// advice报表数据访问类

    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAdviceReportSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region 获取配血申请单


        /// <summary>
        /// 获取配血申请单

        /// </summary>
        [AutoComplete]
        public long m_lngRptGetBloodApply(int searchType, string areadId, string beginDate, out DataTable dt)
        {
            long lngRes = 0;
            dt = null;

            #region SQL
        string sql = @"         select e.orderexecid_chr,
                                       a.createdate_dat,
                                       d.lastname_vchr,
                                       d.sex_chr,
                                       d.birth_dat,
                                       (decode(sign(months_between(sysdate, d.birth_dat) / 12 - 7),1,to_char(round((months_between(sysdate, d.birth_dat) / 12),0)) || '岁',
                                               0,'7岁' - 1,
                                       decode(sign(months_between(sysdate, d.birth_dat) - 12),1,to_char(round(months_between(sysdate, d.birth_dat) / 12,0)) || '岁' ||
                                       to_char(round(months_between(sysdate, d.birth_dat), 0) -round(months_between(sysdate, d.birth_dat) / 12,0) * 12) || '月',0,'1岁0月' - 1,to_char(round(sysdate - d.birth_dat)) || '天'))) 
                                       as  age,
                                       a.createareaname_vchr,
                                       h.code_chr,
                                       g.inpatientid_chr,
                                       a.name_vchr,
                                       a.remark_vchr,
                                       f.lastname_vchr,
                                       a.orderid_chr,
                                       g.areaid_chr, 
                                       a.dosage_dec,
                                       a.dosageunit_chr
                                  from t_opr_bih_order    a,
                                       t_opr_setusage     b,
                                       t_bse_patient      d,
                                       t_bse_employee     f,
                                       t_opr_bih_orderexecute e,
                                       t_opr_bih_register g,
                                       t_bse_bed          h
                                 where a.dosetypeid_chr = b.usageid_chr
                                   and a.patientid_chr  = d.patientid_chr
                                   and a.orderid_chr    = e.orderid_chr
                                   and a.creatorid_chr  = f.empid_chr
                                   and a.registerid_chr = g.registerid_chr
                                   and g.bedid_chr      = h.bedid_chr
                                   and b.orderid_vchr   = '23'  ";

            string condition = string.Format(" and g.areaid_chr = '{0}' ", areadId);

            if (searchType == 0)
            {
                beginDate = DateTime.Parse(beginDate).ToString("yyyy-MM-dd");
                condition += string.Format("and (to_char(a.createdate_dat, 'yyyy-mm-dd hh24:mi:ss') between '{0}' and '{1}')", beginDate, beginDate + " 23:59:59");
            }
            else if (searchType == 1)
            {
                condition += @"and not exists (   select t2.seq_int
                                                  from t_opr_bih_oeprint t2
                                                 where t2.orderid_vchr = a.orderid_chr
                                                   and t2.orderexecid_chr = e.orderexecid_chr
                                                   and t2.areaid_chr = a.createareaid_chr)  ";

                beginDate = DateTime.Today.ToString("yyyy-MM-dd"); ;
                condition += string.Format(" and (to_char(a.createdate_dat, 'yyyy-mm-dd hh24:mi:ss') between '{0}' and '{1}')", beginDate, beginDate + " 23:59:59");

            }
            else if (searchType == 2)
            {
                condition += string.Format(" and  (to_char(a.createdate_dat, 'yyyy-mm-dd hh24:mi:ss') > '{0}') ", beginDate);
            }

            #endregion

            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                lngRes = hrpService.lngGetDataTableWithoutParameters(sql + condition, ref dt);
                
            }
            catch(Exception ex)
            {
                new clsLogText().LogError(ex);
            }

            return lngRes;
        }
        #endregion

        #region 获取输血三联单


        /// <summary>
        /// 获取输血三联单

        /// </summary>
        [AutoComplete]
        public long m_lngRptGetThreeBloodApply(int searchType, string areadId, string beginDate, out DataTable dt)
        {
            long lngRes = 0;
            dt = null;

            #region == SQL ==

            string strSQL = @"          select e.orderexecid_chr,
                                               d.lastname_vchr,
                                               i.deptname_vchr, 
                                               g.inpatientid_chr,
                                               a.name_vchr,
                                               a.remark_vchr,
                                               f.lastname_vchr,
                                               a.createdate_dat,
                                               a.orderid_chr,
                                               g.areaid_chr,
                                               h.code_chr,
                                               a.dosage_dec,
                                               a.dosageunit_chr
                                          from t_opr_bih_order        a,
                                               t_opr_setusage         b,
                                               t_bse_nurseorder       c,
                                               t_bse_patient          d,
                                               t_opr_bih_orderexecute e,
                                               t_bse_employee         f,
                                               t_opr_bih_register     g,
                                               t_bse_bed              h,
                                               T_BSE_DeptDesc         i
                                         where a.dosetypeid_chr = b.usageid_chr
                                           and a.patientid_chr  = d.patientid_chr
                                           and (a.orderid_chr   = e.orderid_chr)
                                           and a.creatorid_chr  = f.empid_chr
                                           and a.registerid_chr = g.registerid_chr
                                           and b.orderid_vchr   = c.orderid_int(+)
                                           and g.bedid_chr      = h.bedid_chr
                                           and c.orderid_int    = '24'
                                           and i.deptid_chr=a.curareaid_chr ";
                       
            string condition = " ";          
            if (searchType == 0 || searchType == 2)
            {
                condition += "and (a.createdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss') )";
            }
            else if (searchType == 1)
            {
                condition += @"and not exists ( select t2.seq_int
                                                  from t_opr_bih_oeprint t2
                                                 where t2.orderid_vchr = a.orderid_chr
                                                   and t2.orderexecid_chr = e.orderexecid_chr
                                                   and t2.areaid_chr = a.createareaid_chr) 
                                                   and (a.createdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss') )";               
            }

            condition += "and ((g.areaid_chr = ? and a.sourcetype_int = 0) or (a.createareaid_chr = ? and a.sourcetype_int = 1))";
            strSQL += condition;            

            #endregion

            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();               
             
                System.Data.IDataParameter[] arrParams = null;
                hrpService.CreateDatabaseParameter(4, out arrParams);
                arrParams[0].Value = beginDate + " 00:00:00";
                if (searchType == 2)
                {
                    arrParams[1].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                }
                else
                {
                    arrParams[1].Value = beginDate + " 23:59:59";
                }
                arrParams[2].Value = areadId;
                arrParams[3].Value = areadId;

                lngRes = hrpService.lngGetDataTableWithParameters(strSQL, ref dt, arrParams);
                hrpService.Dispose();
              
            }
            catch (Exception ex)
            {             
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);              
            }

            return lngRes;
        }
        #endregion

        #region 更新医嘱执行单打印时间


        /// <summary>
        /// 更新医嘱执行单打印时间

        /// </summary>
        /// <param name="p_dvRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long UpdateBihOrderExecPrintDate(string areaId,string orderId,string orderExeId)
        {
            long lngRes = 0;
            DateTime CreateDate = DateTime.MinValue;
            DataTable dt=null;

            
            string strSQL = @"INSERT INTO t_opr_bih_oeprint
                                          (SEQ_INT, AREAID_CHR, ORDERID_VCHR, ORDEREXECID_CHR, PRINT_DATE)
                                        VALUES
                                          (seq_bih_oeprint.nextval, ?, ?, ?, sysdate)";

            try
            {
                IDataParameter[] objParams = null;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out objParams);
                objParams[0].Value = areaId;
                objParams[1].Value = orderId;
                objParams[2].Value = orderExeId;

                lngRes=objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dt,objParams);
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }

            return lngRes;
        }

        #endregion
    }
}
