using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 诊疗项目|收费项目-收费项目
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOrderdicChargeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 构造函数
        public clsOrderdicChargeSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        //T_bse_bih_orderdic(诊疗项目)
        #region 查询
        /// <summary>
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strName, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "     ,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=a.execdept_chr) Execdept";
            strSQL += "     ,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=a.ordercateid_chr) OrderCate";
            strSQL += "     ,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=a.itemid_chr) Item  ";
            strSQL += "     ,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(a.nullitemdosetypeid_chr)) NullItemDosetypeName";
            strSQL += " FROM t_bse_bih_orderdic a";
            strSQL += " WHERE ";
            strSQL += "		(LOWER(trim(a.name_chr)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(USERCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(WBCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(PYCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%')";
            strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            strSQL += " Order By USERCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strName, out clsT_bse_bih_orderdic_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_bih_orderdic_VO[0];
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "     ,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=a.execdept_chr) Execdept";
            strSQL += "     ,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=a.ordercateid_chr) OrderCate";
            strSQL += "     ,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=a.itemid_chr) Item  ";
            strSQL += "     ,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(a.nullitemdosetypeid_chr)) NullItemDosetypeName";
            strSQL += " FROM t_bse_bih_orderdic a";
            strSQL += " WHERE ";
            strSQL += "		(LOWER(trim(a.name_chr)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(USERCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(WBCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(PYCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%')";
            strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_bse_bih_orderdic_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_bse_bih_orderdic_VO();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_STATUS_INT = int.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strEXECDEPT_CHR = dtbResult.Rows[i1]["EXECDEPT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[i1]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();
                        //执行科室		[非字段]
                        p_objResultArr[i1].m_strExecdept = dtbResult.Rows[i1]["Execdept"].ToString().Trim();
                        //医嘱类型		[非字段]
                        p_objResultArr[i1].m_strOrderCate = dtbResult.Rows[i1]["OrderCate"].ToString().Trim();
                        //主收费项目	[非字段]
                        p_objResultArr[i1].m_strItem = dtbResult.Rows[i1]["Item"].ToString().Trim();
                        //用法名称	[非字段]
                        p_objResultArr[i1].m_strNullItemDosetypeName = dtbResult.Rows[i1]["NullItemDosetypeName"].ToString().Trim();
                    }
                }
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
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateID">诊疗项目类型 {null 或 "" 则不作查询条件.}</param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_blnDisplayOnly50">是否最多只显示50条记录</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strOrderCateID, string p_strName, bool p_blnDisplayOnly50, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "     ,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=a.execdept_chr) Execdept";
            strSQL += "     ,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=a.ordercateid_chr) OrderCate";
            strSQL += "     ,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=a.itemid_chr) Item  ";
            strSQL += "     ,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(a.nullitemdosetypeid_chr)) NullItemDosetypeName";
            strSQL += " FROM t_bse_bih_orderdic a";
            strSQL += " WHERE ";
            strSQL += "		(LOWER(trim(a.name_chr)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(USERCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(WBCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(PYCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%')";
            if (p_strOrderCateID != null && p_strOrderCateID.Trim() != "")
            {
                strSQL += "  and Trim(ORDERCATEID_CHR) ='" + p_strOrderCateID.Trim() + "'";
            }
            if (p_blnDisplayOnly50)
            {
                strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            }
            strSQL += " Order By USERCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateID">诊疗项目类型 {null 或 "" 则不作查询条件.}</param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_blnDisplayOnly50">是否最多只显示50条记录</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strOrderCateID, string p_strName, bool p_blnDisplayOnly50, out clsT_bse_bih_orderdic_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_bih_orderdic_VO[0];
            long lngRes = 0;
            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "     ,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=a.execdept_chr) Execdept";
            strSQL += "     ,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=a.ordercateid_chr) OrderCate";
            strSQL += "     ,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=a.itemid_chr) Item  ";
            strSQL += "     ,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(a.nullitemdosetypeid_chr)) NullItemDosetypeName";
            strSQL += " FROM t_bse_bih_orderdic a";
            strSQL += " WHERE ";
            strSQL += "		(LOWER(trim(a.name_chr)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(USERCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(WBCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(PYCODE_CHR)) LIKE '" + p_strName.Trim().ToLower() + "%')";
            if (p_strOrderCateID != null && p_strOrderCateID.Trim() != "")
            {
                strSQL += "  and Trim(ORDERCATEID_CHR) ='" + p_strOrderCateID.Trim() + "'";
            }
            if (p_blnDisplayOnly50)
            {
                strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_bse_bih_orderdic_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_bse_bih_orderdic_VO();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECDEPT_CHR = dtbResult.Rows[i1]["EXECDEPT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[i1]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();
                        //执行科室		[非字段]
                        p_objResultArr[i1].m_strExecdept = dtbResult.Rows[i1]["Execdept"].ToString().Trim();
                        //医嘱类型		[非字段]
                        p_objResultArr[i1].m_strOrderCate = dtbResult.Rows[i1]["OrderCate"].ToString().Trim();
                        //主收费项目	[非字段]
                        p_objResultArr[i1].m_strItem = dtbResult.Rows[i1]["Item"].ToString().Trim();
                        //用法名称	[非字段]
                        p_objResultArr[i1].m_strNullItemDosetypeName = dtbResult.Rows[i1]["NullItemDosetypeName"].ToString().Trim();
                    }
                }
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
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateID">诊疗项目类型 {null 或 "" 则不作查询条件.}</param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_intConut">{1=全部；2=一对一；3=一对零；4=一对多}</param>
        /// <param name="p_blnDisplayOnly50">是否最多只显示50条记录</param>
        /// <param name="p_blnDisplayStopItems">是否显示诊疗项目</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strOrderCateID, string p_strName, int p_intConut, bool p_blnDisplayOnly50, bool p_blnDisplayStopItems, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            #region SQL

            string strSQL = @"select a.orderdicid_chr, a.name_chr, a.des_vchr, a.usercode_chr,
                                     a.wbcode_chr, a.pycode_chr, a.execdept_chr, a.ordercateid_chr,
                                     a.itemid_chr, a.nullitemdosageunit_chr, a.nullitemuseunit_chr,
                                     a.nullitemdosetypeid_chr, a.status_int, a.sampleid_vchr,
                                     a.partid_vchr, a.engname_vchr, a.commname_vchr,
                                     a.nullitemfreqid_vchr, a.nullitemuse_dec, a.lisapplyunitid_chr,
                                     a.applytypeid_chr, a.newchargetype_int, b2.name_chr ordercate,
                                     b3.itemname_vchr item, a1.sample_type_desc_vchr sample_name,
                                     a2.partname part_name, a3.deptname_vchr execdept,
                                     a4.usagename_vchr nullitemdosetypename, a5.freqname_chr,
                                     a6.apply_unit_name_vchr, a7.typetext,
                                     a.opexecdept_chr as opexecdeptid, a32.deptname_vchr opexecdeptname, a.samplingtimes, a.isoutside, a.outsideunit, a.itemScope  
            								FROM t_bse_bih_orderdic a,
            								t_aid_bih_ordercate b2,
            								t_bse_chargeitem b3,
                                             (select k1.orderdicid_chr from t_bse_bih_orderdic k1 ,
                                             (select orderdicid_chr, count(orderdicid_chr) qty FROM t_aid_bih_orderdic_charge  group by orderdicid_chr) k2
                                            where
                                                k1.orderdicid_chr=k2.orderdicid_chr(+)
                                                and 
            								   (   LOWER(k1.name_chr) LIKE ?
            									OR LOWER(k1.usercode_chr) LIKE ?
            									OR LOWER(k1.wbcode_chr) LIKE ?
            									OR LOWER(k1.pycode_chr) LIKE ?
            									)
            								[ORDERCATEID]
            								[QTY]
            								[ROWNUM]
                                            [DISPLAYSTOPITEMS]
                                            ) b
                                            ,
                                            t_aid_lis_sampletype a1,
                                            ar_apply_partlist a2,
                                            t_bse_deptdesc a3,
                                            t_bse_deptdesc a32,
                                            t_bse_usagetype a4,
                                            T_AID_RECIPEFREQ a5,
                                            t_aid_lis_apply_unit a6,
                                            AR_APPLY_TYPELIST a7	
            							WHERE a.orderdicid_chr = b.orderdicid_chr
            							     and
            							      a.ordercateid_chr=b2.ordercateid_chr(+)
            							     and
            							      a.itemid_chr=b3.itemid_chr(+)
            							   
                                            and a.sampleid_vchr=a1.sample_type_id_chr(+)
                                            and a.partid_vchr=a2.partid(+)
                                            and a.execdept_chr=a3.deptid_chr(+)
                                            and a.opexecdept_chr = a32.deptid_chr(+)
                                            and a.nullitemdosetypeid_chr=a4.usageid_chr(+)
                                            and a.NULLITEMFREQID_VCHR = a5.FREQID_CHR (+)
                                            and a.LISAPPLYUNITID_CHR=a6.APPLY_UNIT_ID_CHR(+)
                                            and a.APPLYTYPEID_CHR=a7.TYPEID(+)
            							ORDER BY a.usercode_chr";

            if (p_strOrderCateID != null && p_strOrderCateID.Trim() != "")
            {
                strSQL = strSQL.Replace("[ORDERCATEID]", "  and Trim(k1.ORDERCATEID_CHR) in (select ORDERCATEID_CHR from t_aid_bih_orderCate where trim(VIEWNAME_VCHR)='" + p_strOrderCateID.Trim() + "')");
            }
            else
            {
                strSQL = strSQL.Replace("[ORDERCATEID]", "");
            }
            switch (p_intConut)
            {
                case 2:
                    strSQL = strSQL.Replace("[QTY]", " AND qty=1 ");
                    break;
                case 3:
                    strSQL = strSQL.Replace("[QTY]", " AND (qty<1 or qty is null ) ");//{qty=0[错误]}
                    break;
                case 4:
                    strSQL = strSQL.Replace("[QTY]", " AND qty>1 ");
                    break;
                default:
                    strSQL = strSQL.Replace("[QTY]", "");
                    break;
            }
            if (p_blnDisplayOnly50)
            {
                strSQL = strSQL.Replace("[ROWNUM]", " and ROWNUM<=" + m_intGetTop().ToString());
            }
            else
            {
                strSQL = strSQL.Replace("[ROWNUM]", "");
            }
            if (p_blnDisplayStopItems)
            {
                strSQL = strSQL.Replace("[DISPLAYSTOPITEMS]", "");
            }
            else
            {
                strSQL = strSQL.Replace("[DISPLAYSTOPITEMS]", " and k1.STATUS_INT='1'");
            }
            #endregion
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(4, out arrParams);
                arrParams[0].Value = "%" + p_strName.Trim() + "%";
                arrParams[1].Value = p_strName.Trim() + "%";
                arrParams[2].Value = p_strName.Trim() + "%";
                arrParams[3].Value = p_strName.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
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
        /// <summary>
        /// 查询诊疗项目-按字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderCateID">诊疗项目类型 {null 或 "" 则不作查询条件.}</param>
        /// <param name="p_strName">查询字符串</param>
        /// <param name="p_intConut">{1=全部；2=一对一；3=一对零；4=一对多}</param>
        /// <param name="p_blnDisplayOnly50">是否最多只显示50条记录</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strOrderCateID, string p_strName, int p_intConut, bool p_blnDisplayOnly50, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            #region SQL
            string strSQL = @"
							SELECT   a.ORDERDICID_CHR,   
                                     a.NAME_CHR,   
                                     a.DES_VCHR,   
                                     a.USERCODE_CHR,   
                                     a.WBCODE_CHR,   
                                     a.PYCODE_CHR,   
                                     a.EXECDEPT_CHR,   
                                     a.ORDERCATEID_CHR,   
                                     a.ITEMID_CHR,   
                                     a.NULLITEMDOSAGEUNIT_CHR,   
                                     a.NULLITEMUSEUNIT_CHR,   
                                     a.NULLITEMDOSETYPEID_CHR,   
                                     a.STATUS_INT,   
                                     a.SAMPLEID_VCHR,   
                                     a.PARTID_VCHR,   
                                     a.ENGNAME_VCHR,   
                                     a.COMMNAME_VCHR, 
                                    (SELECT b1.deptname_vchr
											FROM t_bse_deptdesc b1
											WHERE b1.deptid_chr = a.execdept_chr) execdept,
									(SELECT b2.name_chr
										FROM t_aid_bih_ordercate b2
									WHERE b2.ordercateid_chr = a.ordercateid_chr) ordercate,
									(SELECT b3.itemname_vchr
										FROM t_bse_chargeitem b3
									WHERE b3.itemid_chr = a.itemid_chr) item,
									(SELECT b4.usagename_vchr
										FROM t_bse_usagetype b4
									WHERE TRIM (b4.usageid_chr) =
													TRIM (a.nullitemdosetypeid_chr))
																					nullitemdosetypename,
									DECODE (b.qty, NULL, 0, b.qty) qty
								FROM t_bse_bih_orderdic a,
									(SELECT   orderdicid_chr, COUNT (orderdicid_chr) qty
										FROM t_aid_bih_orderdic_charge
									GROUP BY orderdicid_chr) b
							WHERE a.orderdicid_chr = b.orderdicid_chr(+)
								AND (   LOWER (TRIM (a.name_chr)) LIKE '%[STRING]%'
									OR LOWER (TRIM (usercode_chr)) LIKE '[STRING]%'
									OR LOWER (TRIM (wbcode_chr)) LIKE '[STRING]%'
									OR LOWER (TRIM (pycode_chr)) LIKE '[STRING]%'
									)
								[ORDERCATEID]
								[QTY]
								[ROWNUM]
                     
                         
							ORDER BY usercode_chr";
            strSQL = strSQL.Replace("[STRING]", p_strName.Trim());

            if (p_strOrderCateID != null && p_strOrderCateID.Trim() != "")
            {
                /** update by xzf (05-09-29) 
                 * 改为按"显示名称"查询
                 */
                //@ strSQL =strSQL.Replace("[ORDERCATEID]","  and Trim(ORDERCATEID_CHR) ='" + p_strOrderCateID.Trim() + "'");
                strSQL = strSQL.Replace("[ORDERCATEID]", "  and Trim(ORDERCATEID_CHR) in (select trim(ORDERCATEID_CHR) from t_aid_bih_orderCate where trim(VIEWNAME_VCHR)='" + p_strOrderCateID.Trim() + "')");
                /* <<========================================================= */
            }
            else
            {
                strSQL = strSQL.Replace("[ORDERCATEID]", "");
            }

            //{1=全部[默认]；2=一对一；3=一对零；4=一对多}
            switch (p_intConut)
            {
                case 2:
                    strSQL = strSQL.Replace("[QTY]", " AND qty=1 ");
                    break;
                case 3:
                    strSQL = strSQL.Replace("[QTY]", " AND  (qty is null)");//{qty=0[错误]}
                    break;
                case 4:
                    strSQL = strSQL.Replace("[QTY]", " AND qty>1 ");
                    break;
                default:
                    strSQL = strSQL.Replace("[QTY]", "");
                    break;
            }

            if (p_blnDisplayOnly50)
            {
                strSQL = strSQL.Replace("[ROWNUM]", " and ROWNUM<=" + m_intGetTop().ToString());
            }
            else
            {
                strSQL = strSQL.Replace("[ROWNUM]", "");
            }
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询诊疗项目-按诊疗项目流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdericid">诊疗项目流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByOrdericid(string p_strOrdericid, out clsT_bse_bih_orderdic_VO p_objResult)
        {
            p_objResult = new clsT_bse_bih_orderdic_VO();
            long lngRes = 0;
            string strSQL = @"
                    SELECT a.ORDERDICID_CHR,   
                         a.NAME_CHR,   
                         a.DES_VCHR,   
                         a.USERCODE_CHR,   
                         a.WBCODE_CHR,   
                         a.PYCODE_CHR,   
                         a.EXECDEPT_CHR,   
                         a.ORDERCATEID_CHR,   
                         a.ITEMID_CHR,   
                         a.NULLITEMDOSAGEUNIT_CHR,   
                         a.NULLITEMUSEUNIT_CHR,   
                         a.NULLITEMDOSETYPEID_CHR,   
                         a.STATUS_INT,   
                         a.SAMPLEID_VCHR,   
                         a.PARTID_VCHR,   
                         a.ENGNAME_VCHR,   
                         a.COMMNAME_VCHR, 
                         a.NULLITEMFREQID_VCHR,
                         a.NULLITEMUSE_DEC,
                    b.sample_type_desc_vchr sample_name,
                    c.partname part_name,
                    d.deptname_vchr execdept,
                    e.name_chr  ordercate,
                    f.itemname_vchr item,
                    g.usagename_vchr nullitemdosetypename,
                    h.FREQNAME_CHR

                    FROM 
                    t_bse_bih_orderdic a,
                    t_aid_lis_sampletype b,
                    ar_apply_partlist c,
                    t_bse_deptdesc d,
                    t_aid_bih_ordercate e,
                    t_bse_chargeitem f,
                    t_bse_usagetype g,
                    (select trim(FREQID_CHR) FREQID_CHR, FREQNAME_CHR from T_AID_RECIPEFREQ ) h

                    WHERE 
                    a.sampleid_vchr=b.sample_type_id_chr(+)
                    and
                    a.partid_vchr=c.partid(+)
                    and
                    a.execdept_chr=d.deptid_chr(+)
                    and
                    a.ordercateid_chr=e.ordercateid_chr(+)
                    and
                    a.itemid_chr=f.itemid_chr(+)
                    and
                    a.nullitemdosetypeid_chr=g.usageid_chr(+)
                    and
                    a.NULLITEMFREQID_VCHR = h.FREQID_CHR(+) 
                    and
                    a.orderdicid_chr ='" + p_strOrdericid.Trim() + "'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult.m_strORDERDICID_CHR = dtbResult.Rows[0]["ORDERDICID_CHR"].ToString().Trim();
                    p_objResult.m_strNAME_CHR = dtbResult.Rows[0]["NAME_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strUSERCODE_CHR = dtbResult.Rows[0]["USERCODE_CHR"].ToString().Trim();
                    p_objResult.m_strWBCODE_CHR = dtbResult.Rows[0]["WBCODE_CHR"].ToString().Trim();
                    p_objResult.m_strPYCODE_CHR = dtbResult.Rows[0]["PYCODE_CHR"].ToString().Trim();
                    p_objResult.m_strEXECDEPT_CHR = dtbResult.Rows[0]["EXECDEPT_CHR"].ToString().Trim();
                    p_objResult.m_strORDERCATEID_CHR = dtbResult.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                    p_objResult.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    p_objResult.m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[0]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[0]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[0]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();


                    //执行科室		[非字段]
                    p_objResult.m_strExecdept = dtbResult.Rows[0]["Execdept"].ToString().Trim();
                    //医嘱类型		[非字段]
                    p_objResult.m_strOrderCate = dtbResult.Rows[0]["OrderCate"].ToString().Trim();
                    //主收费项目	[非字段]
                    p_objResult.m_strItem = dtbResult.Rows[0]["Item"].ToString().Trim();
                    //用法名称		[非字段]
                    p_objResult.m_strNullItemDosetypeName = dtbResult.Rows[0]["NullItemDosetypeName"].ToString().Trim();
                    //样本ID		
                    p_objResult.m_strSAMPLEID_VCHR = dtbResult.Rows[0]["SAMPLEID_VCHR"].ToString().Trim();
                    //样本名称		
                    p_objResult.m_strSAMPLE_NAME = dtbResult.Rows[0]["SAMPLE_NAME"].ToString().Trim();
                    //部位ID		
                    p_objResult.m_strPARTID = dtbResult.Rows[0]["PARTID_VCHR"].ToString().Trim();
                    //部位名称		
                    p_objResult.m_strPARTNAME = dtbResult.Rows[0]["PART_name"].ToString().Trim();
                    /*<=========================================*/


                    p_objResult.m_strCOMMNAME_VCHR = dtbResult.Rows[0]["COMMNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strENGNAME_VCHR = dtbResult.Rows[0]["ENGNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strNULLITEMFREQID_VCHR = dtbResult.Rows[0]["NULLITEMFREQID_VCHR"].ToString().Trim();
                    p_objResult.m_strNULLITEMUSE_DEC = dtbResult.Rows[0]["NULLITEMUSE_DEC"].ToString().Trim();
                    p_objResult.m_strFREQNAME_CHR = dtbResult.Rows[0]["FREQNAME_CHR"].ToString().Trim();
                }
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
        #region 新增
        /// <summary>
        ///	新增诊疗项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderdic(out string p_strRecordID, clsT_bse_bih_orderdic_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            string strSQL = "   select lpad(seq_ORDERDICID.NEXTVAL,10,'0') p_strRecordID   from dual ";
            DataTable dtbResult2 = new DataTable();
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
            if (lngRes > 0 && dtbResult2.Rows.Count > 0)
            {
                p_strRecordID = Convert.ToString(dtbResult2.Rows[0]["p_strRecordID"].ToString());
            }
            else
            {
                return -1;
            }
            if (lngRes < 0) return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            strSQL = @"INSERT INTO t_bse_bih_orderdic 
                                     (ORDERDICID_CHR, NAME_CHR, DES_VCHR, USERCODE_CHR,
                                      WBCODE_CHR, PYCODE_CHR, EXECDEPT_CHR, ORDERCATEID_CHR,
                                      ITEMID_CHR, NULLITEMDOSAGEUNIT_CHR, NULLITEMUSEUNIT_CHR, NULLITEMDOSETYPEID_CHR,
                                      SAMPLEID_VCHR, PARTID_VCHR, ENGNAME_VCHR, COMMNAME_VCHR,
                                      NULLITEMFREQID_VCHR, NULLITEMUSE_DEC,LISAPPLYUNITID_CHR,APPLYTYPEID_CHR,
                                      NEWCHARGETYPE_INT, opexecdept_chr, peGroupCode, peGroupName, samplingtimes, isoutside, outsideunit, itemScope) 
                              VALUES (?, ?, ?, ?, 
                                      ?, ?, ?, ?,
                                      ?, ?, ?, ?,
                                      ?, ?, ?, ?,
                                      ?, ?, ?, ?,
                                      ?, ?, ?, ?, ?, ?, ?, ?)";

            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(28, out objLisAddItemRefArr);
                int n = -1;
                objLisAddItemRefArr[++n].Value = p_strRecordID;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNAME_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strUSERCODE_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strWBCODE_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strPYCODE_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strEXECDEPT_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strORDERCATEID_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMDOSAGEUNIT_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMUSEUNIT_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMDOSETYPEID_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strSAMPLEID_VCHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strPARTID;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strENGNAME_VCHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strCOMMNAME_VCHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMFREQID_VCHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMUSE_DEC;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strLISAPPLYUNITID_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strAPPLYTYPEID_CHR;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_intNEWCHARGETYPE_INT;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strOPExecDeptID_chr;
                objLisAddItemRefArr[++n].Value = p_objRecord.peCombCode;
                objLisAddItemRefArr[++n].Value = p_objRecord.peCombName;
                objLisAddItemRefArr[++n].Value = p_objRecord.SamplingTimes;
                objLisAddItemRefArr[++n].Value = p_objRecord.IsOutside;
                objLisAddItemRefArr[++n].Value = p_objRecord.OutsideUnit;
                objLisAddItemRefArr[++n].Value = p_objRecord.ItemScope;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改诊疗项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicid">诊疗项目流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderdicByOrderdicid(string p_strOrderdicid, clsT_bse_bih_orderdic_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
             UPDATE T_BSE_BIH_ORDERDIC
             set NAME_CHR=?, DES_VCHR=?, USERCODE_CHR=?, WBCODE_CHR=?,
                 PYCODE_CHR=?, ORDERCATEID_CHR=?, ITEMID_CHR=?, ENGNAME_VCHR=?, 
                 COMMNAME_VCHR=?,NULLITEMDOSETYPEID_CHR = ?, NULLITEMFREQID_VCHR = ?, NULLITEMUSE_DEC = ?,
                 NULLITEMUSEUNIT_CHR = ?,SAMPLEID_VCHR=?,PARTID_VCHR=?,LISAPPLYUNITID_CHR=?,
                 APPLYTYPEID_CHR=?,NEWCHARGETYPE_INT=?, opexecdept_chr = ?, peGroupCode = ?, peGroupName = ?, samplingtimes = ?, isoutside = ?, outsideunit = ?, itemScope = ?    
             WHERE ORDERDICID_CHR =?
            ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(26, out objLisAddItemRefArr);
                int n = -1;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNAME_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strDES_VCHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strUSERCODE_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strWBCODE_CHR.Trim();

                objLisAddItemRefArr[++n].Value = p_objRecord.m_strPYCODE_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strORDERCATEID_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strITEMID_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strENGNAME_VCHR.Trim();

                objLisAddItemRefArr[++n].Value = p_objRecord.m_strCOMMNAME_VCHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMDOSETYPEID_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMFREQID_VCHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMUSE_DEC.Trim();

                objLisAddItemRefArr[++n].Value = p_objRecord.m_strNULLITEMUSEUNIT_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strSAMPLEID_VCHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strPARTID.Trim();

                objLisAddItemRefArr[++n].Value = p_objRecord.m_strLISAPPLYUNITID_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strAPPLYTYPEID_CHR.Trim();
                objLisAddItemRefArr[++n].Value = p_objRecord.m_intNEWCHARGETYPE_INT;
                objLisAddItemRefArr[++n].Value = p_objRecord.m_strOPExecDeptID_chr;
                objLisAddItemRefArr[++n].Value = p_objRecord.peCombCode;
                objLisAddItemRefArr[++n].Value = p_objRecord.peCombName;
                objLisAddItemRefArr[++n].Value = p_objRecord.SamplingTimes;
                objLisAddItemRefArr[++n].Value = p_objRecord.IsOutside;
                objLisAddItemRefArr[++n].Value = p_objRecord.OutsideUnit;
                objLisAddItemRefArr[++n].Value = p_objRecord.ItemScope;
                objLisAddItemRefArr[++n].Value = p_strOrderdicid.Trim();

                long lngRecEff = -1;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        /// <summary>
        ///停用/启用 诊疗项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicid">诊疗项目ID</param>
        /// <param name="m_blUsed">(true-启用，false-停用)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStopItemsByOrderdicid(string p_strOrderdicid, bool m_blUsed)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL = @"
            UPDATE T_BSE_BIH_ORDERDIC
            SET STATUS_INT =[STATUS_INT] WHERE   ORDERDICID_CHR ='[ORDERDICID_CHR]'";
            strSQL = strSQL.Replace("[STATUS_INT]", Convert.ToString((m_blUsed) ? 1 : 0));
            strSQL = strSQL.Replace("[ORDERDICID_CHR]", p_strOrderdicid.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        /// 更改主收费项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strChiefItemID">主收费项目</param>
        /// <param name="p_strOrderdicid">诊疗项目流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyChiefItemIDByOrderdicid(string p_strChiefItemID, string p_strOrderdicid)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_BSE_BIH_ORDERDIC";
            strSQL += " SET";
            strSQL += "  ITEMID_CHR = '" + p_strChiefItemID.Trim() + "'";
            strSQL += " WHERE";
            strSQL += "      ORDERDICID_CHR ='" + p_strOrderdicid.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 删除诊疗项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicid">诊疗项目流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderdicByOrderdicid(string p_strOrderdicid)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM  T_BSE_BIH_ORDERDIC WHERE ORDERDICID_CHR ='" + p_strOrderdicid.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = @"delete from t_bse_bih_orderdic_applyunit where orderdicid_chr ='" + p_strOrderdicid.Trim() + "'";
                objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //检测是否有关联收费项目
            return lngRes;
        }
        /// <summary>
        /// 强制删除诊疗项目-按流水号	[事务]
        /// 提示: 如果有关联,则关联也删除
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicid">诊疗项目流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderdic(string p_strOrderdicid)
        {
            long lngRes = 0;
            //删除关联
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngDeleteOrderdicChargeByOrderdicID(p_strOrderdicid);
            }
            //删除诊疗项目-按流水号
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngDeleteOrderdicByOrderdicid(p_strOrderdicid);
            }
            if (lngRes <= 0)
            {
                throw (new Exception("删除诊疗项目错误！"));
            }
            return lngRes;
        }
        #endregion
        #region 其他
        /// <summary>
        /// 获取医嘱类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderCate(out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.ordercateid_chr, a.name_chr, a.des_vchr  FROM t_aid_bih_ordercate a";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 获取可以设为主收费项目的收费项
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strOrderdicID">诊疗项目流水号</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChiefChargeItem(string strOrderdicID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT itemid_chr,itemname_vchr,ITEMSPEC_VCHR,ITEMPRICE_MNY,ITEMPYCODE_CHR,ITEMWBCODE_CHR FROM t_bse_chargeitem";
            strSQL += " WHERE";
            strSQL += " (itemid_chr in(SELECT itemid_chr FROM t_aid_bih_orderdic_charge where orderdicid_chr='" + strOrderdicID.Trim() + "'))";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询[有效的]科室	根据查询字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFindString">查询字符串</param>
        /// <param name="p_dtbResult">DataTable [out 参数]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptByFindString(string p_strFindString, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            //病区: AND attributeid='0000003'
            string strSQL = @"SELECT DEPTID_CHR,DEPTNAME_VCHR,SHORTNO_CHR,PYCODE_CHR,CODE_VCHR  FROM t_bse_deptdesc a ";
            strSQL += " WHERE status_int=1 ";
            strSQL += "		and (LOWER(trim(DEPTNAME_VCHR)) LIKE '%" + p_strFindString.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(SHORTNO_CHR)) LIKE '" + p_strFindString.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(WBCODE_CHR)) LIKE '" + p_strFindString.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(PYCODE_CHR)) LIKE '" + p_strFindString.Trim().ToLower() + "%'";
            strSQL += " and a.INPATIENTOROUTPATIENT_INT=1 and a.CATEGORY_INT=0 order by a.code_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
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

        //T_bse_chargeitem(收费项目)
        #region 查询

        /// <summary>
        /// T_bse_chargeitem(收费项目)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strText"></param>
        /// <param name="m_intCondition"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByName(string p_strText, int m_intCondition, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            switch (m_intCondition)
            {
                case 0: strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE LOWER(trim(a.ITEMNAME_VCHR)) LIKE '%" + p_strText.Trim().ToLower() + "%' and ROWNUM<=50"; break;
                case 1: strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE LOWER(trim(a.ITEMCODE_VCHR)) LIKE '" + p_strText.Trim().ToLower() + "%'and ROWNUM<=50"; break;
                case 2: strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE LOWER(trim(a.ITEMPYCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'and ROWNUM<=50"; break;
                case 3: strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE LOWER(trim(a.ITEMWBCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'and ROWNUM<=50"; break;
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询收费项目-查询字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strText">查询字符串</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByName(string p_strText, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE ";
            strSQL += "		(LOWER(trim(a.ITEMNAME_VCHR)) LIKE '%" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMCODE_VCHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMWBCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMPYCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%')";
            strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询收费项目-查询字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strText">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByName(string p_strText, out clsT_bse_chargeitem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE ";
            strSQL += "		(LOWER(trim(a.ITEMNAME_VCHR)) LIKE '%" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMCODE_VCHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMWBCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMPYCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%')";
            //strSQL +="		and ROWNUM<=" + m_intGetTop().ToString();
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_bse_chargeitem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_bse_chargeitem_VO();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMPYCODE_CHR = dtbResult.Rows[i1]["ITEMPYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMWBCODE_CHR = dtbResult.Rows[i1]["ITEMWBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMSRCID_VCHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        try
                        {
                            p_objResultArr[i1].m_dblMinPrice = double.Parse(dtbResult.Rows[i1]["MinPrice"].ToString().Trim());
                        }
                        catch { }
                    }
                }
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
        /// 查询收费项目-查询字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strText">查询字符串</param>
        /// <param name="p_intRowCount">只取前几行</param>
        /// <param name="p_objResultArr">对象</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByName(string p_strText, int p_intRowCount, out clsT_bse_chargeitem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE ";
            strSQL += "		(LOWER(trim(a.ITEMNAME_VCHR)) LIKE '%" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMCODE_VCHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMWBCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.ITEMPYCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%')";
            strSQL += "		and ROWNUM<=" + p_intRowCount.ToString();
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_bse_chargeitem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_bse_chargeitem_VO();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMPYCODE_CHR = dtbResult.Rows[i1]["ITEMPYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMWBCODE_CHR = dtbResult.Rows[i1]["ITEMWBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMSRCID_VCHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        try
                        {
                            p_objResultArr[i1].m_dblMinPrice = double.Parse(dtbResult.Rows[i1]["MinPrice"].ToString().Trim());
                        }
                        catch { }
                    }
                }
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
        /// 查询收费项目-按收费项目流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemid">收费项目流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByItemid(string p_strItemid, out clsT_bse_chargeitem_VO p_objResult)
        {
            p_objResult = new clsT_bse_chargeitem_VO();
            long lngRes = 0;
            string strSQL = @"SELECT a.*,decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice FROM t_bse_chargeitem a WHERE a.itemid_chr='" + p_strItemid.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_bse_chargeitem_VO();
                    p_objResult.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    p_objResult.m_strITEMNAME_VCHR = dtbResult.Rows[0]["ITEMNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strITEMCODE_VCHR = dtbResult.Rows[0]["ITEMCODE_VCHR"].ToString().Trim();
                    p_objResult.m_strITEMPYCODE_CHR = dtbResult.Rows[0]["ITEMPYCODE_CHR"].ToString().Trim();
                    p_objResult.m_strITEMWBCODE_CHR = dtbResult.Rows[0]["ITEMWBCODE_CHR"].ToString().Trim();
                    p_objResult.m_strITEMSRCID_VCHR = dtbResult.Rows[0]["ITEMSRCID_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ITEMSRCTYPE_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["ITEMSRCTYPE_INT"].ToString().Trim());
                    }
                    p_objResult.m_strITEMSPEC_VCHR = dtbResult.Rows[0]["ITEMSPEC_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ITEMPRICE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[0]["ITEMPRICE_MNY"].ToString().Trim());
                    }
                    p_objResult.m_strITEMUNIT_CHR = dtbResult.Rows[0]["ITEMUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strITEMOPUNIT_CHR = dtbResult.Rows[0]["ITEMOPUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strITEMIPUNIT_CHR = dtbResult.Rows[0]["ITEMIPUNIT_CHR"].ToString().Trim();
                    p_objResult.m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[0]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                    p_objResult.m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[0]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                    p_objResult.m_strITEMOPINVTYPE_CHR = dtbResult.Rows[0]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                    p_objResult.m_strITEMIPINVTYPE_CHR = dtbResult.Rows[0]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["DOSAGE_DEC"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[0]["DOSAGE_DEC"].ToString().Trim());
                    }
                    p_objResult.m_strDOSAGEUNIT_CHR = dtbResult.Rows[0]["DOSAGEUNIT_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ISGROUPITEM_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[0]["ISGROUPITEM_INT"].ToString().Trim());
                    }
                    p_objResult.m_strITEMCATID_CHR = dtbResult.Rows[0]["ITEMCATID_CHR"].ToString().Trim();
                    p_objResult.m_strUSAGEID_CHR = dtbResult.Rows[0]["USAGEID_CHR"].ToString().Trim();
                    p_objResult.m_strITEMOPCODE_CHR = dtbResult.Rows[0]["ITEMOPCODE_CHR"].ToString().Trim();
                    p_objResult.m_strINSURANCEID_CHR = dtbResult.Rows[0]["INSURANCEID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["SELFDEFINE_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[0]["SELFDEFINE_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["PACKQTY_DEC"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[0]["PACKQTY_DEC"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["TRADEPRICE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[0]["TRADEPRICE_MNY"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["POFLAG_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[0]["POFLAG_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["MinPrice"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblMinPrice = double.Parse(dtbResult.Rows[0]["MinPrice"].ToString().Trim());
                    }
                }
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
        #region 新增
        /// <summary>
        ///	新增收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewChargeItem(out string p_strRecordID, clsT_bse_chargeitem_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(10, "ITEMID_CHR", "t_bse_chargeitem", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_bse_chargeitem (ITEMID_CHR,ITEMNAME_VCHR,ITEMCODE_VCHR,ITEMPYCODE_CHR,ITEMWBCODE_CHR,ITEMSRCID_VCHR,ITEMSRCTYPE_INT,ITEMSPEC_VCHR,ITEMPRICE_MNY,ITEMUNIT_CHR,ITEMOPUNIT_CHR,ITEMIPUNIT_CHR,ITEMOPCALCTYPE_CHR,ITEMIPCALCTYPE_CHR,ITEMOPINVTYPE_CHR,ITEMIPINVTYPE_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,ISGROUPITEM_INT,ITEMCATID_CHR,USAGEID_CHR,ITEMOPCODE_CHR,INSURANCEID_CHR,SELFDEFINE_INT,PACKQTY_DEC,TRADEPRICE_MNY,POFLAG_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(27, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strITEMNAME_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strITEMCODE_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strITEMPYCODE_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strITEMWBCODE_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strITEMSRCID_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intITEMSRCTYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strITEMSPEC_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_dblITEMPRICE_MNY;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strITEMUNIT_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strITEMOPUNIT_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strITEMIPUNIT_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strITEMOPCALCTYPE_CHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strITEMIPCALCTYPE_CHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strITEMOPINVTYPE_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strITEMIPINVTYPE_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_dblDOSAGE_DEC;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strDOSAGEUNIT_CHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_intISGROUPITEM_INT;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strITEMCATID_CHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strUSAGEID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strITEMOPCODE_CHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strINSURANCEID_CHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_intSELFDEFINE_INT;
                objLisAddItemRefArr[24].Value = p_objRecord.m_dblPACKQTY_DEC;
                objLisAddItemRefArr[25].Value = p_objRecord.m_dblTRADEPRICE_MNY;
                objLisAddItemRefArr[26].Value = p_objRecord.m_intPOFLAG_INT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改收费项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemid">收费项目流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyChargeItemByItemid(string p_strItemid, clsT_bse_chargeitem_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";// "INSERT INTO XXXXXX (ITEMID_CHR,ITEMNAME_VCHR,ITEMCODE_VCHR,ITEMPYCODE_CHR,ITEMWBCODE_CHR,ITEMSRCID_VCHR,ITEMSRCTYPE_INT,ITEMSPEC_VCHR,ITEMPRICE_MNY,ITEMUNIT_CHR,ITEMOPUNIT_CHR,ITEMIPUNIT_CHR,ITEMOPCALCTYPE_CHR,ITEMIPCALCTYPE_CHR,ITEMOPINVTYPE_CHR,ITEMIPINVTYPE_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,ISGROUPITEM_INT,ITEMCATID_CHR,USAGEID_CHR,ITEMOPCODE_CHR,INSURANCEID_CHR,SELFDEFINE_INT,PACKQTY_DEC,TRADEPRICE_MNY,POFLAG_INT) VALUES ('"+p_objRecord.m_strITEMID_CHR+"','"+p_objRecord.m_strITEMNAME_VCHR+"','"+p_objRecord.m_strITEMCODE_VCHR+"','"+p_objRecord.m_strITEMPYCODE_CHR+"','"+p_objRecord.m_strITEMWBCODE_CHR+"','"+p_objRecord.m_strITEMSRCID_VCHR+"','"+p_objRecord.m_intITEMSRCTYPE_INT.ToString()+"','"+p_objRecord.m_strITEMSPEC_VCHR+"','"+p_objRecord.m_dblITEMPRICE_MNY.ToString()+"','"+p_objRecord.m_strITEMUNIT_CHR+"','"+p_objRecord.m_strITEMOPUNIT_CHR+"','"+p_objRecord.m_strITEMIPUNIT_CHR+"','"+p_objRecord.m_strITEMOPCALCTYPE_CHR+"','"+p_objRecord.m_strITEMIPCALCTYPE_CHR+"','"+p_objRecord.m_strITEMOPINVTYPE_CHR+"','"+p_objRecord.m_strITEMIPINVTYPE_CHR+"','"+p_objRecord.m_dblDOSAGE_DEC.ToString()+"','"+p_objRecord.m_strDOSAGEUNIT_CHR+"','"+p_objRecord.m_intISGROUPITEM_INT.ToString()+"','"+p_objRecord.m_strITEMCATID_CHR+"','"+p_objRecord.m_strUSAGEID_CHR+"','"+p_objRecord.m_strITEMOPCODE_CHR+"','"+p_objRecord.m_strINSURANCEID_CHR+"','"+p_objRecord.m_intSELFDEFINE_INT.ToString()+"','"+p_objRecord.m_dblPACKQTY_DEC.ToString()+"','"+p_objRecord.m_dblTRADEPRICE_MNY.ToString()+"','"+p_objRecord.m_intPOFLAG_INT.ToString()+"')";
            strSQL += " ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 删除收费项目-按流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemid">收费项目流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteChargeItemByItemid(string p_strItemid)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM  t_bse_chargeitem WHERE itemid_chr ='" + p_strItemid.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //T_aid_bih_orderdic_charge(诊疗项目|收费项目)
        #region 查询
        /// <summary>
        /// 查询诊疗项目|收费项目-按诊疗项目名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">按诊疗项目名称</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByOrderdicName(string p_strName, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"
						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, C.ITEMNAME_VCHR ItemName,B.itemid_chr ChiefItemID 
							,(select b0.itemname_vchr from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
						FROM
							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            strSQL += " WHERE A.orderdicid_chr =B.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            strSQL += "     AND (LOWER(trim(B.NAME_CHR)) LIKE '%" + p_strName.Trim().ToLower() + "%')";
            strSQL += "		and ROWNUM<=" + m_intGetTop().ToString();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        /// <summary>
        /// 查询诊疗项目|收费项目-按诊疗项目名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">按诊疗项目名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByOrderdicName(string p_strName, out clsT_aid_bih_orderdic_charge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[0];
            long lngRes = 0;
            string strSQL = @"
						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, C.ITEMNAME_VCHR ItemName,B.itemid_chr ChiefItemID 
							,(select b0.itemname_vchr from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
						FROM
							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            strSQL += " WHERE A.orderdicid_chr =B.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            strSQL += "     AND LOWER(trim(B.NAME_CHR)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[dtbResult.Rows.Count];
                    clsT_bse_chargeitem_VO objChargeItem = new clsT_bse_chargeitem_VO();
                    clsT_bse_bih_orderdic_VO objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_orderdic_charge_VO();
                        #region 收费项目对象
                        objChargeItem = new clsT_bse_chargeitem_VO();
                        objChargeItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMPYCODE_CHR = dtbResult.Rows[i1]["ITEMPYCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMWBCODE_CHR = dtbResult.Rows[i1]["ITEMWBCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMSRCID_VCHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        objChargeItem.m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        //非字段
                        try
                        {
                            objChargeItem.m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        try
                        {
                            objChargeItem.m_dblMinPrice = double.Parse(dtbResult.Rows[i1]["MinPrice"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_objChargeItem = objChargeItem;
                        #endregion
                        #region 诊疗项目对象
                        objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                        objOrderDicItem.m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        objOrderDicItem.m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        objOrderDicItem.m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strEXECDEPT_CHR = dtbResult.Rows[i1]["EXECDEPT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[i1]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();
                        //执行科室		[非字段]
                        objOrderDicItem.m_strExecdept = dtbResult.Rows[i1]["Execdept"].ToString().Trim();
                        //医嘱类型		[非字段]
                        objOrderDicItem.m_strOrderCate = dtbResult.Rows[i1]["OrderCate"].ToString().Trim();
                        //主收费项目	[非字段]
                        objOrderDicItem.m_strItem = dtbResult.Rows[i1]["Item"].ToString().Trim();
                        //用法名称	[非字段]
                        objOrderDicItem.m_strNullItemDosetypeName = dtbResult.Rows[i1]["NullItemDosetypeName"].ToString().Trim();
                        p_objResultArr[i1].m_objOrderDic = objOrderDicItem;
                        #endregion
                        #region 影射对象
                        p_objResultArr[i1].m_strOCMAPID_CHR = dtbResult.Rows[i1]["OCMAPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intQTY_INT = decimal.Parse(dtbResult.Rows[i1]["QTY_INT"].ToString());
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //非字段
                        p_objResultArr[i1].m_strItemName = dtbResult.Rows[i1]["ItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrdericName = dtbResult.Rows[i1]["OrdericName"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemID = dtbResult.Rows[i1]["ChiefItemID"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemName = dtbResult.Rows[i1]["ChiefItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsChiefItem = dtbResult.Rows[i1]["IsChiefItem"].ToString().Trim();
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        #endregion
                    }
                }
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
        /// 查询诊疗项目|收费项目-按收费项目名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemName">按收费项目名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByItemName(string p_strItemName, out clsT_aid_bih_orderdic_charge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[0];
            long lngRes = 0;
            string strSQL = @"
						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, C.ITEMNAME_VCHR ItemName,B.itemid_chr ChiefItemID 
							,(select b0.itemname_vchr from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
						FROM
							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            strSQL += " WHERE A.orderdicid_chr =B.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            strSQL += "     AND LOWER(trim(C.ITEMNAME_VCHR)) LIKE '%" + p_strItemName.Trim().ToLower() + "%'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[dtbResult.Rows.Count];
                    clsT_bse_chargeitem_VO objChargeItem = new clsT_bse_chargeitem_VO();
                    clsT_bse_bih_orderdic_VO objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_orderdic_charge_VO();
                        #region 收费项目对象
                        objChargeItem = new clsT_bse_chargeitem_VO();
                        objChargeItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMPYCODE_CHR = dtbResult.Rows[i1]["ITEMPYCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMWBCODE_CHR = dtbResult.Rows[i1]["ITEMWBCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMSRCID_VCHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        objChargeItem.m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        //非字段
                        try
                        {
                            objChargeItem.m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        try
                        {
                            objChargeItem.m_dblMinPrice = double.Parse(dtbResult.Rows[i1]["MinPrice"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_objChargeItem = objChargeItem;
                        #endregion
                        #region 诊疗项目对象
                        objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                        objOrderDicItem.m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        objOrderDicItem.m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        objOrderDicItem.m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strEXECDEPT_CHR = dtbResult.Rows[i1]["EXECDEPT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[i1]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();
                        //执行科室		[非字段]
                        objOrderDicItem.m_strExecdept = dtbResult.Rows[i1]["Execdept"].ToString().Trim();
                        //医嘱类型		[非字段]
                        objOrderDicItem.m_strOrderCate = dtbResult.Rows[i1]["OrderCate"].ToString().Trim();
                        //主收费项目	[非字段]
                        objOrderDicItem.m_strItem = dtbResult.Rows[i1]["Item"].ToString().Trim();
                        //用法名称	[非字段]
                        objOrderDicItem.m_strNullItemDosetypeName = dtbResult.Rows[i1]["NullItemDosetypeName"].ToString().Trim();
                        p_objResultArr[i1].m_objOrderDic = objOrderDicItem;
                        #endregion
                        #region 影射对象
                        p_objResultArr[i1].m_strOCMAPID_CHR = dtbResult.Rows[i1]["OCMAPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intQTY_INT = decimal.Parse(dtbResult.Rows[i1]["QTY_INT"].ToString());
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //非字段
                        p_objResultArr[i1].m_strItemName = dtbResult.Rows[i1]["ItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrdericName = dtbResult.Rows[i1]["OrdericName"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemID = dtbResult.Rows[i1]["ChiefItemID"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemName = dtbResult.Rows[i1]["ChiefItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsChiefItem = dtbResult.Rows[i1]["IsChiefItem"].ToString().Trim();
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        #endregion
                    }
                }
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
        /// 查询诊疗项目|收费项目-按诊疗项目名称和按收费项目名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">按诊疗项目名称</param>
        /// <param name="p_strItemName">按收费项目名称</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByName(string p_strName, string p_strItemName, out clsT_aid_bih_orderdic_charge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[0];
            long lngRes = 0;
            string strSQL = @"
						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, C.ITEMNAME_VCHR ItemName,B.itemid_chr ChiefItemID 
							,(select b0.itemname_vchr from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
						FROM
							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            strSQL += " WHERE A.orderdicid_chr =b.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            strSQL += "     AND LOWER(trim(B.NAME_CHR)) LIKE '%" + p_strName.Trim().ToLower() + "%'";
            strSQL += "     AND LOWER(trim(C.ITEMNAME_VCHR)) LIKE '%" + p_strItemName.Trim().ToLower() + "%'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_orderdic_charge_VO[dtbResult.Rows.Count];
                    clsT_bse_chargeitem_VO objChargeItem = new clsT_bse_chargeitem_VO();
                    clsT_bse_bih_orderdic_VO objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_orderdic_charge_VO();
                        #region 收费项目对象
                        objChargeItem = new clsT_bse_chargeitem_VO();
                        objChargeItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMNAME_VCHR = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMCODE_VCHR = dtbResult.Rows[i1]["ITEMCODE_VCHR"].ToString().Trim();
                        objChargeItem.m_strITEMPYCODE_CHR = dtbResult.Rows[i1]["ITEMPYCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMWBCODE_CHR = dtbResult.Rows[i1]["ITEMWBCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMSRCID_VCHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        try
                        {
                            objChargeItem.m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        catch { }
                        objChargeItem.m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        objChargeItem.m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        objChargeItem.m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        objChargeItem.m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        //非字段
                        try
                        {
                            objChargeItem.m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            objChargeItem.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        catch { }
                        //非字段
                        try
                        {
                            objChargeItem.m_dblMinPrice = double.Parse(dtbResult.Rows[i1]["MinPrice"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_objChargeItem = objChargeItem;
                        #endregion
                        #region 诊疗项目对象
                        objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                        objOrderDicItem.m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        objOrderDicItem.m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        objOrderDicItem.m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        objOrderDicItem.m_strEXECDEPT_CHR = dtbResult.Rows[i1]["EXECDEPT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[i1]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                        objOrderDicItem.m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[i1]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();
                        //执行科室		[非字段]
                        objOrderDicItem.m_strExecdept = dtbResult.Rows[i1]["Execdept"].ToString().Trim();
                        //医嘱类型		[非字段]
                        objOrderDicItem.m_strOrderCate = dtbResult.Rows[i1]["OrderCate"].ToString().Trim();
                        //主收费项目	[非字段]
                        objOrderDicItem.m_strItem = dtbResult.Rows[i1]["Item"].ToString().Trim();
                        //用法名称	[非字段]
                        objOrderDicItem.m_strNullItemDosetypeName = dtbResult.Rows[i1]["NullItemDosetypeName"].ToString().Trim();
                        p_objResultArr[i1].m_objOrderDic = objOrderDicItem;
                        #endregion
                        #region 影射对象
                        p_objResultArr[i1].m_strOCMAPID_CHR = dtbResult.Rows[i1]["OCMAPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERDICID_CHR = dtbResult.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intQTY_INT = decimal.Parse(dtbResult.Rows[i1]["QTY_INT"].ToString());
                        p_objResultArr[i1].m_intTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["TYPE_INT"].ToString());
                        //非字段
                        p_objResultArr[i1].m_strItemName = dtbResult.Rows[i1]["ItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strOrdericName = dtbResult.Rows[i1]["OrdericName"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemID = dtbResult.Rows[i1]["ChiefItemID"].ToString().Trim();
                        p_objResultArr[i1].m_strChiefItemName = dtbResult.Rows[i1]["ChiefItemName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsChiefItem = dtbResult.Rows[i1]["IsChiefItem"].ToString().Trim();
                        p_objResultArr[i1].m_strTypeName = dtbResult.Rows[i1]["TypeName"].ToString().Trim();
                        #endregion
                    }
                }
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
        /// 查询诊疗项目|收费项目-按诊疗项目流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicID">按诊疗项目流水号</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByOrderdicID(string p_strOrderdicID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            //            string strSQL = @"
            //						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, trim(C.ITEMNAME_VCHR) as ItemName,B.itemid_chr ChiefItemID 
            //							,(select trim(b0.itemname_vchr) from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
            //							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
            //							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
            //							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
            //							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
            //							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
            //							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
            //							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
            //						FROM
            //							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            //            strSQL += " WHERE A.orderdicid_chr =B.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            //            strSQL += "     AND A.orderdicid_chr = '" + p_strOrderdicID.Trim() + "'";
            //            strSQL += " ORDER BY A.ocmapid_chr";
            string strSQL = @"
            SELECT   a.*, b.*, c.*, b.name_chr ordericname,
         TRIM (c.itemname_vchr) AS itemname, b.itemid_chr chiefitemid,
         b0.itemname_vchr chiefitemname,
         DECODE (a.itemid_chr, b.itemid_chr, '√', '×') ischiefitem,
         DECODE (a.type_int, 1, '领量单位', 2, '剂量单位', '') typename,
          b1.deptname_vchr  execdept,
         b2.name_chr ordercate,
         b3.itemname_vchr item,
          b4.usagename_vchr  nullitemdosetypename,
         DECODE (c.ipchargeflg_int,
                 1, ROUND (c.itemprice_mny / c.packqty_dec, 4),
                 0, c.itemprice_mny,
                 ROUND (c.itemprice_mny / c.packqty_dec, 4)
                ) minprice
         FROM t_aid_bih_orderdic_charge a, 
         t_bse_bih_orderdic b,
         t_bse_chargeitem c,
         t_bse_chargeitem b0,
         t_bse_deptdesc b1,
         t_aid_bih_ordercate b2,
         t_bse_chargeitem b3,
         t_bse_usagetype b4
       WHERE a.orderdicid_chr = b.orderdicid_chr(+)
         AND a.itemid_chr = c.itemid_chr(+)
         and b.itemid_chr=b0.itemid_chr(+)
         and b.execdept_chr=b1.deptid_chr(+)
         and b.ordercateid_chr=b2.ordercateid_chr(+)
         and b.itemid_chr=b3.itemid_chr(+)
         and b.nullitemdosetypeid_chr=b4.usageid_chr(+)
         AND a.orderdicid_chr = ?
         ORDER BY a.ocmapid_chr
            ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(1, out parameters);
                parameters[0].Value = p_strOrderdicID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parameters);
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
        /// <summary>
        /// 查询诊疗项目|收费项目-流水号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOcmapid">流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicChargeByOcmapid(string p_strOcmapid, out clsT_aid_bih_orderdic_charge_VO p_objResult)
        {
            p_objResult = new clsT_aid_bih_orderdic_charge_VO();
            long lngRes = 0;
            string strSQL = @"
						SELECT A.*,b.*,c.*,B.NAME_CHR OrdericName, C.ITEMNAME_VCHR ItemName,B.itemid_chr ChiefItemID 
							,(select b0.itemname_vchr from T_BSE_CHARGEITEM b0 where b.itemid_chr=b0.itemid_chr)ChiefItemName
							,decode(A.itemid_chr,B.itemid_chr,'√','×' )IsChiefItem	
							,decode(A.type_int,1,'领量单位',2,'剂量单位','' )TypeName
							,(select b1.deptname_vchr from t_bse_deptdesc b1 where b1.deptid_chr=b.execdept_chr) Execdept
							,(select b2.name_chr from t_aid_bih_ordercate b2 where b2.ordercateid_chr=b.ordercateid_chr) OrderCate
							,(select b3.itemname_vchr from t_bse_chargeitem b3 where b3.itemid_chr=b.itemid_chr) Item  
							,(select b4.usagename_vchr from t_bse_usagetype b4 where trim(b4.usageid_chr)=trim(b.nullitemdosetypeid_chr)) NullItemDosetypeName
							,decode(c.IPCHARGEFLG_INT,1,Round(c.ItemPrice_Mny/c.PackQty_Dec,4),0,c.ItemPrice_Mny,Round(c.ItemPrice_Mny/c.PackQty_Dec,4)) MinPrice
						FROM
							T_AID_BIH_ORDERDIC_CHARGE A , T_BSE_BIH_ORDERDIC B , T_BSE_CHARGEITEM C ";
            strSQL += " WHERE A.orderdicid_chr =B.orderdicid_chr(+) AND A.itemid_chr =C.itemid_chr (+)";
            strSQL += "		  and A.OCMAPID_CHR = '" + p_strOcmapid.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_bih_orderdic_charge_VO();
                    clsT_bse_chargeitem_VO objChargeItem = new clsT_bse_chargeitem_VO();
                    clsT_bse_bih_orderdic_VO objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                    #region 收费项目对象
                    objChargeItem = new clsT_bse_chargeitem_VO();
                    objChargeItem.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMNAME_VCHR = dtbResult.Rows[0]["ITEMNAME_VCHR"].ToString().Trim();
                    objChargeItem.m_strITEMCODE_VCHR = dtbResult.Rows[0]["ITEMCODE_VCHR"].ToString().Trim();
                    objChargeItem.m_strITEMPYCODE_CHR = dtbResult.Rows[0]["ITEMPYCODE_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMWBCODE_CHR = dtbResult.Rows[0]["ITEMWBCODE_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMSRCID_VCHR = dtbResult.Rows[0]["ITEMSRCID_VCHR"].ToString().Trim();
                    try
                    {
                        objChargeItem.m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["ITEMSRCTYPE_INT"].ToString().Trim());
                    }
                    catch { }
                    objChargeItem.m_strITEMSPEC_VCHR = dtbResult.Rows[0]["ITEMSPEC_VCHR"].ToString().Trim();
                    try
                    {
                        objChargeItem.m_dblITEMPRICE_MNY = double.Parse(dtbResult.Rows[0]["ITEMPRICE_MNY"].ToString().Trim());
                    }
                    catch { }
                    objChargeItem.m_strITEMUNIT_CHR = dtbResult.Rows[0]["ITEMUNIT_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMOPUNIT_CHR = dtbResult.Rows[0]["ITEMOPUNIT_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMIPUNIT_CHR = dtbResult.Rows[0]["ITEMIPUNIT_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[0]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[0]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMOPINVTYPE_CHR = dtbResult.Rows[0]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMIPINVTYPE_CHR = dtbResult.Rows[0]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                    try
                    {
                        objChargeItem.m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[0]["DOSAGE_DEC"].ToString().Trim());
                    }
                    catch { }
                    objChargeItem.m_strDOSAGEUNIT_CHR = dtbResult.Rows[0]["DOSAGEUNIT_CHR"].ToString().Trim();
                    try
                    {
                        objChargeItem.m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[0]["ISGROUPITEM_INT"].ToString().Trim());
                        objChargeItem.m_strITEMCATID_CHR = dtbResult.Rows[0]["ITEMCATID_CHR"].ToString().Trim();
                    }
                    catch { }
                    objChargeItem.m_strUSAGEID_CHR = dtbResult.Rows[0]["USAGEID_CHR"].ToString().Trim();
                    objChargeItem.m_strITEMOPCODE_CHR = dtbResult.Rows[0]["ITEMOPCODE_CHR"].ToString().Trim();
                    objChargeItem.m_strINSURANCEID_CHR = dtbResult.Rows[0]["INSURANCEID_CHR"].ToString().Trim();
                    try
                    {
                        objChargeItem.m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[0]["SELFDEFINE_INT"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        objChargeItem.m_dblPACKQTY_DEC = double.Parse(dtbResult.Rows[0]["PACKQTY_DEC"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        objChargeItem.m_dblTRADEPRICE_MNY = double.Parse(dtbResult.Rows[0]["TRADEPRICE_MNY"].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        objChargeItem.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[0]["POFLAG_INT"].ToString().Trim());
                    }
                    catch { }
                    //非字段
                    try
                    {
                        objChargeItem.m_dblMinPrice = double.Parse(dtbResult.Rows[0]["MinPrice"].ToString().Trim());
                    }
                    catch { }

                    p_objResult.m_objChargeItem = objChargeItem;
                    #endregion
                    #region 影射对象
                    objOrderDicItem = new clsT_bse_bih_orderdic_VO();
                    objOrderDicItem.m_strORDERDICID_CHR = dtbResult.Rows[0]["ORDERDICID_CHR"].ToString().Trim();
                    objOrderDicItem.m_strNAME_CHR = dtbResult.Rows[0]["NAME_CHR"].ToString().Trim();
                    objOrderDicItem.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    objOrderDicItem.m_strUSERCODE_CHR = dtbResult.Rows[0]["USERCODE_CHR"].ToString().Trim();
                    objOrderDicItem.m_strWBCODE_CHR = dtbResult.Rows[0]["WBCODE_CHR"].ToString().Trim();
                    objOrderDicItem.m_strPYCODE_CHR = dtbResult.Rows[0]["PYCODE_CHR"].ToString().Trim();
                    objOrderDicItem.m_strEXECDEPT_CHR = dtbResult.Rows[0]["EXECDEPT_CHR"].ToString().Trim();
                    objOrderDicItem.m_strORDERCATEID_CHR = dtbResult.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                    objOrderDicItem.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    objOrderDicItem.m_strNULLITEMDOSAGEUNIT_CHR = dtbResult.Rows[0]["NULLITEMDOSAGEUNIT_CHR"].ToString().Trim();
                    objOrderDicItem.m_strNULLITEMUSEUNIT_CHR = dtbResult.Rows[0]["NULLITEMUSEUNIT_CHR"].ToString().Trim();
                    objOrderDicItem.m_strNULLITEMDOSETYPEID_CHR = dtbResult.Rows[0]["NULLITEMDOSETYPEID_CHR"].ToString().Trim();

                    objOrderDicItem.m_strCOMMNAME_VCHR = dtbResult.Rows[0]["COMMNAME_VCHR"].ToString().Trim();
                    objOrderDicItem.m_strENGNAME_VCHR = dtbResult.Rows[0]["ENGNAME_VCHR"].ToString().Trim();

                    //执行科室		[非字段]
                    objOrderDicItem.m_strExecdept = dtbResult.Rows[0]["Execdept"].ToString().Trim();
                    //医嘱类型		[非字段]
                    objOrderDicItem.m_strOrderCate = dtbResult.Rows[0]["OrderCate"].ToString().Trim();
                    //主收费项目	[非字段]
                    objOrderDicItem.m_strItem = dtbResult.Rows[0]["Item"].ToString().Trim();
                    //用法名称	[非字段]
                    objOrderDicItem.m_strNullItemDosetypeName = dtbResult.Rows[0]["NullItemDosetypeName"].ToString().Trim();
                    p_objResult.m_objOrderDic = objOrderDicItem;
                    #endregion
                    #region 影射对象
                    p_objResult.m_strOCMAPID_CHR = dtbResult.Rows[0]["OCMAPID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERDICID_CHR = dtbResult.Rows[0]["ORDERDICID_CHR"].ToString().Trim();
                    p_objResult.m_strITEMID_CHR = dtbResult.Rows[0]["ITEMID_CHR"].ToString().Trim();
                    p_objResult.m_intQTY_INT = decimal.Parse(dtbResult.Rows[0]["QTY_INT"].ToString());
                    p_objResult.m_intTYPE_INT = Int32.Parse(dtbResult.Rows[0]["TYPE_INT"].ToString());
                    //非字段
                    p_objResult.m_strItemName = dtbResult.Rows[0]["ItemName"].ToString().Trim();
                    p_objResult.m_strOrdericName = dtbResult.Rows[0]["OrdericName"].ToString().Trim();
                    p_objResult.m_strChiefItemID = dtbResult.Rows[0]["ChiefItemID"].ToString().Trim();
                    p_objResult.m_strChiefItemName = dtbResult.Rows[0]["ChiefItemName"].ToString().Trim();
                    p_objResult.m_strIsChiefItem = dtbResult.Rows[0]["IsChiefItem"].ToString().Trim();
                    p_objResult.m_strTypeName = dtbResult.Rows[0]["TypeName"].ToString().Trim();
                    #endregion
                }
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
        #region 新增
        /// <summary>
        /// 新增诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderdicCharge(out string p_strRecordID, clsT_aid_bih_orderdic_charge_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(18, "ocmapid_chr", "t_aid_bih_orderdic_charge", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_aid_bih_orderdic_charge (OCMAPID_CHR,ORDERDICID_CHR,ITEMID_CHR,QTY_INT,TYPE_INT) VALUES (?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strORDERDICID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intQTY_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_intTYPE_INT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 更改
        /// <summary>
        /// 更改诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOcmapid">流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderdicChargeByOcmapid(string p_strOcmapid, clsT_aid_bih_orderdic_charge_VO p_objRecord)
        {
            long lngRes = 0;
            string strSQL = "UPDATE  T_AID_BIH_ORDERDIC_CHARGE SET ORDERDICID_CHR = '" + p_objRecord.m_strORDERDICID_CHR + "', ITEMID_CHR = '" + p_objRecord.m_strITEMID_CHR + "', QTY_INT = " + p_objRecord.m_intQTY_INT + ", TYPE_INT = " + p_objRecord.m_intTYPE_INT + " WHERE Trim(OCMAPID_CHR)='" + p_strOcmapid.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 删除诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOcmapid">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderdicChargeByOcmapid(string p_strOcmapid)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM  T_AID_BIH_ORDERDIC_CHARGE WHERE Trim(OCMAPID_CHR)='" + p_strOcmapid.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        /// 删除诊疗项目|收费项目	根据诊疗项目ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderdicID">诊疗项目ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderdicChargeByOrderdicID(string p_strOrderdicID)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM  T_AID_BIH_ORDERDIC_CHARGE WHERE Trim(ORDERDICID_CHR)='" + p_strOrderdicID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //T_aid_recipefreq(用药频率辅助表)
        #region SQL
        /// <summary>
        /// 返回查询的select部分
        /// </summary>
        /// <returns></returns>
        private string strSQLAidRecipefreq()
        {
            string strSQL = "";
            strSQL += " SELECT a.* FROM t_aid_recipefreq a";
            return strSQL;
        }
        #endregion
        #region 查询
        [AutoComplete]
        public long m_lngGetAidRecipefreqByFindString(string p_strText, out clsT_aid_recipefreq_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_recipefreq_VO[0];
            long lngRes = 0;
            string strSQL = strSQLAidRecipefreq();
            strSQL += " Where ";
            strSQL += "		LOWER(trim(a.FREQNAME_CHR)) LIKE '%" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.USERCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_recipefreq_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_recipefreq_VO();
                        p_objResultArr[i1].m_strFREQID_CHR = dtbResult.Rows[i1]["FREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFREQNAME_CHR = dtbResult.Rows[i1]["FREQNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["TIMES_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intDAYS_INT = Convert.ToInt32(dtbResult.Rows[i1]["DAYS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strLEXECTIME_VCHR = dtbResult.Rows[i1]["LEXECTIME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTEXECTIME_VCHR = dtbResult.Rows[i1]["TEXECTIME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECWEEKDAY_CHR = dtbResult.Rows[i1]["EXECWEEKDAY_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPRINTDESC_VCHR = dtbResult.Rows[i1]["PRINTDESC_VCHR"].ToString().Trim();
                    }
                }
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
        public long m_lngGetAidRecipefreqByFindString(string p_strText, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = strSQLAidRecipefreq();
            strSQL += " Where ";
            strSQL += "		LOWER(trim(a.FREQNAME_CHR)) LIKE '%" + p_strText.Trim().ToLower() + "%'";
            strSQL += "		or LOWER(trim(a.USERCODE_CHR)) LIKE '" + p_strText.Trim().ToLower() + "%'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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
        [AutoComplete]
        public long m_lngGetAidRecipefreqByID(string p_strID, out clsT_aid_recipefreq_VO p_objResult)
        {
            p_objResult = new clsT_aid_recipefreq_VO();
            long lngRes = 0;
            string strSQL = strSQLAidRecipefreq();
            strSQL += " Where ";
            strSQL += "		a.FREQID_CHR='" + p_strID.Trim().ToLower() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_recipefreq_VO();
                    p_objResult.m_strFREQID_CHR = dtbResult.Rows[0]["FREQID_CHR"].ToString().Trim();
                    p_objResult.m_strFREQNAME_CHR = dtbResult.Rows[0]["FREQNAME_CHR"].ToString().Trim();
                    p_objResult.m_strUSERCODE_CHR = dtbResult.Rows[0]["USERCODE_CHR"].ToString().Trim();
                    p_objResult.m_intTIMES_INT = Convert.ToInt32(dtbResult.Rows[0]["TIMES_INT"].ToString().Trim());
                    p_objResult.m_intDAYS_INT = Convert.ToInt32(dtbResult.Rows[0]["DAYS_INT"].ToString().Trim());
                    p_objResult.m_strLEXECTIME_VCHR = dtbResult.Rows[0]["LEXECTIME_VCHR"].ToString().Trim();
                    p_objResult.m_strTEXECTIME_VCHR = dtbResult.Rows[0]["TEXECTIME_VCHR"].ToString().Trim();
                    p_objResult.m_strEXECWEEKDAY_CHR = dtbResult.Rows[0]["EXECWEEKDAY_CHR"].ToString().Trim();
                    p_objResult.m_strPRINTDESC_VCHR = dtbResult.Rows[0]["PRINTDESC_VCHR"].ToString().Trim();
                }
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
        #region 增加
        [AutoComplete]
        public long m_lngAddNewAidRecipefreq(out string p_strRecordID, clsT_aid_recipefreq_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            //lngRes = objHRPSvc.lngGenerateID(4,"FREQID_CHR","T_aid_recipefreq",out p_strRecordID);
            DataTable m_objTable = new DataTable();
            string m_strSQL = @"select max(to_number(FREQID_CHR))+1 from t_aid_recipefreq";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_strSQL, ref m_objTable);
            if (lngRes < 0)
                return lngRes;
            else
            {
                p_strRecordID = m_objTable.Rows[0][0].ToString();
            }
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"INSERT INTO T_aid_recipefreq (FREQID_CHR,FREQNAME_CHR,USERCODE_CHR,TIMES_INT,DAYS_INT,LEXECTIME_VCHR,TEXECTIME_VCHR,EXECWEEKDAY_CHR,PRINTDESC_VCHR) VALUES (?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strFREQID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strFREQNAME_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strUSERCODE_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intTIMES_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_intDAYS_INT;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strLEXECTIME_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strTEXECTIME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strEXECWEEKDAY_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strPRINTDESC_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 修改
        [AutoComplete]
        public long m_lngModifyAidRecipefreq(string p_strID, clsT_aid_recipefreq_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "UPDATE T_AID_RECIPEFREQ A";
            strSQL += " SET";
            strSQL += "    A.FREQNAME_CHR = '" + p_objRecord.m_strFREQNAME_CHR + "'";
            strSQL += "  , A.USERCODE_CHR = '" + p_objRecord.m_strUSERCODE_CHR + "'";
            strSQL += "  , A.TIMES_INT = " + p_objRecord.m_intTIMES_INT.ToString() + "";
            strSQL += "  , A.DAYS_INT = " + p_objRecord.m_intDAYS_INT.ToString() + "";
            strSQL += "  , A.LEXECTIME_VCHR = '" + p_objRecord.m_strLEXECTIME_VCHR + "'";
            strSQL += "  , A.TEXECTIME_VCHR = '" + p_objRecord.m_strTEXECTIME_VCHR + "'";
            strSQL += "  , A.EXECWEEKDAY_CHR = '" + p_objRecord.m_strEXECWEEKDAY_CHR + "'";
            strSQL += "  , A.PRINTDESC_VCHR = '" + p_objRecord.m_strPRINTDESC_VCHR + "'";
            strSQL += " Where  A.FREQID_CHR = '" + p_strID.Trim() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        [AutoComplete]
        public long m_lngDeleteAidRecipefreq(string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE T_AID_RECIPEFREQ WHERE FREQID_CHR = '" + p_strID.Trim() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //T_bse_usagetype(收费项目用法表)
        #region 查询
        /// <summary>
        /// 获取收费项目用法	根据用户输入
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind">查询字符串</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsageTypeByFindString(string p_strFind, out clsT_Bse_UsageType_Vo[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bse_UsageType_Vo[0];
            long lngRes = 0;
            string strSQL = @"
							SELECT   a.usageid_chr, a.usagename_vchr, a.usercode_chr
								FROM t_bse_usagetype a
							WHERE LOWER (TRIM (usagename_vchr)) LIKE ?
								OR LOWER (TRIM (usercode_chr)) LIKE ?
							ORDER BY usercode_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "%" + p_strFind.Trim().ToLower() + "%";
                parameters[1].Value = "" + p_strFind.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parameters);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bse_UsageType_Vo[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bse_UsageType_Vo();
                        p_objResultArr[i1].m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGENAME_VCHR = dtbResult.Rows[i1]["USAGENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                    }
                }
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

        //t_aid_lis_sampletype( 验验样本表)
        #region 验验样本查询
        /// <summary>
        /// 验验样本查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleByFindString(string p_strFind, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"
                        SELECT a.sample_type_id_chr sample_code,a.sample_type_desc_vchr sample_name
                        FROM t_aid_lis_sampletype a
                        WHERE LOWER (TRIM (a.sample_type_id_chr)) LIKE ?
                        OR LOWER (TRIM (a.sample_type_desc_vchr)) LIKE ?
                        OR LOWER (TRIM (a.pycode_chr)) LIKE ?
                        OR LOWER (TRIM (a.wbcode_chr)) LIKE ?
                        ORDER BY a.sample_type_id_chr
							";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(4, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "" + p_strFind.Trim().ToLower() + "%";
                parameters[1].Value = "%" + p_strFind.Trim().ToLower() + "%";
                parameters[2].Value = "%" + p_strFind.Trim().ToLower() + "%";
                parameters[3].Value = "%" + p_strFind.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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

        #region 检验申请单元查询
        /// <summary>
        /// 检验申请单元查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLISAPPLYUNITIDByFindString(string p_strFind, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"
            select a.apply_unit_id_chr,a.apply_unit_name_vchr,a.SAMPLE_TYPE_ID_CHR,a.py_code_chr,a.wb_code_chr,
            a.SAMPLE_TYPE_ID_CHR,
            b.sample_type_desc_vchr 
            from t_aid_lis_apply_unit a,t_aid_lis_sampletype b
            where
            a.sample_type_id_chr=b.sample_type_id_chr(+)
           and
                       (LOWER (TRIM (a.apply_unit_id_chr)) LIKE ?
                        OR LOWER (TRIM (a.apply_unit_name_vchr)) LIKE ?
                        OR LOWER (TRIM (a.py_code_chr)) LIKE ?
                        OR LOWER (TRIM (a.wb_code_chr)) LIKE ?)
            order by a.apply_unit_id_chr
             ";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(4, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "" + p_strFind.Trim().ToLower() + "%";
                parameters[1].Value = "%" + p_strFind.Trim().ToLower() + "%";
                parameters[2].Value = "%" + p_strFind.Trim().ToLower() + "%";
                parameters[3].Value = "%" + p_strFind.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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

        //ar_apply_partlist(检查部位表)
        #region 检查部位查询
        /// <summary>
        /// 验验样本查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckByFindString(string p_strFind, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"
                SELECT a.partid,a.partname,a.assistcode_chr,a.TYPEID
                FROM ar_apply_partlist a
                where 

                (trim(lower(a.assistcode_chr)) like ?
                or
                trim(lower(a.partname)) like ?)
                and a.deleted=0
                order by a.partid
							";

            //strSQL = strSQL.Replace("[p_strFind]", p_strFind.Trim().ToLower());
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "" + p_strFind.Trim().ToLower() + "%";
                parameters[1].Value = "%" + p_strFind.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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

        #region 检查部位申请单元查询
        /// <summary>
        /// 检查部位申请单元查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAPPLYTYPEIDByFindString(string p_strFind, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"
            select a.typeid,a.typetext from AR_APPLY_TYPELIST a
            where 

            (trim(lower(a.typeid)) like ?
            or
            trim(lower(a.typetext)) like ?)
            and
            a.deleted=0
            order by a.orderseq_int
            ";
            //strSQL = strSQL.Replace("[p_strFind]", p_strFind.Trim().ToLower());
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "" + p_strFind.Trim().ToLower() + "%";
                parameters[1].Value = "%" + p_strFind.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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

        //T_Bse_UsageTypeGroup(药品用法组套)
        #region 查找
        /// <summary>
        /// 获取药品用法组套	根据ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsageTypeGroupByID(string p_strID, out clsT_Bse_UsageTypeGroup_Vo p_objResult)
        {
            p_objResult = new clsT_Bse_UsageTypeGroup_Vo();
            long lngRes = 0;
            string strSQL = @"
					SELECT a.groupid_chr, a.groupname_vchr, a.usageids_vchr
					FROM t_bse_usagetypegroup a
					WHERE TRIM (a.groupid_chr) = '[GROUPID]'";
            strSQL = strSQL.Replace("[GROUPID]", p_strID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Bse_UsageTypeGroup_Vo();
                    p_objResult.m_strGROUPID_CHR = dtbResult.Rows[0]["GROUPID_CHR"].ToString().Trim();
                    p_objResult.m_strGROUPNAME_VCHR = dtbResult.Rows[0]["GROUPNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strUSAGEIDS_VCHR = dtbResult.Rows[0]["USAGEIDS_VCHR"].ToString().Trim();
                }
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
        /// 查找药品用法组套	根据输入字符串
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQuery">输入字符串	[全匹配]</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsageTypeGroup(string p_strQuery, out clsT_Bse_UsageTypeGroup_Vo[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bse_UsageTypeGroup_Vo[0];
            long lngRes = 0;
            string strSQL = @"
						SELECT a.groupid_chr, a.groupname_vchr, a.usageids_vchr
						FROM t_bse_usagetypegroup a
						WHERE a.groupname_vchr LIKE '%[QUERYSTRING]%'";
            strSQL = strSQL.Replace("[QUERYSTRING]", p_strQuery.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bse_UsageTypeGroup_Vo[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bse_UsageTypeGroup_Vo();
                        p_objResultArr[i1].m_strGROUPID_CHR = dtbResult.Rows[i1]["GROUPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strGROUPNAME_VCHR = dtbResult.Rows[i1]["GROUPNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGEIDS_VCHR = dtbResult.Rows[i1]["USAGEIDS_VCHR"].ToString().Trim();
                    }
                }
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
        #region 增加
        /// <summary>
        /// 增加药品用法组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">[out参数]流水号</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewUsageTypeGroup(out string p_strRecordID, clsT_Bse_UsageTypeGroup_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(4, "GROUPID_CHR", "T_Bse_UsageTypeGroup", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Bse_UsageTypeGroup (GROUPID_CHR,GROUPNAME_VCHR,USAGEIDS_VCHR) VALUES (?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strGROUPID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strGROUPNAME_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strUSAGEIDS_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 修改
        /// <summary>
        /// 修改药品用法组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyUsageTypeGroup(clsT_Bse_UsageTypeGroup_Vo p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"	UPDATE t_bse_usagetypegroup a
								SET a.groupname_vchr = '[GROUPNAME]',
									a.usageids_vchr = '[USAGEIDS]'
								WHERE TRIM (a.groupid_chr) = '[GROUPID]'";
            strSQL = strSQL.Replace("[GROUPNAME]", p_objRecord.m_strGROUPNAME_VCHR.Trim());
            strSQL = strSQL.Replace("[USAGEIDS]", p_objRecord.m_strUSAGEIDS_VCHR.Trim());
            strSQL = strSQL.Replace("[GROUPID]", p_objRecord.m_strGROUPID_CHR.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region 删除
        /// <summary>
        /// 删除药品用法组套
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteUsageTypeGroup(string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"DELETE FROM t_bse_usagetypegroup WHERE TRIM(groupid_chr) = '[GROUPID]'";
            strSQL = strSQL.Replace("[GROUPID]", p_strID.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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


        //T_aid_bih_ordercate(医嘱类型)
        #region 查找
        [AutoComplete]
        public long m_lngGetAidOrderCate(string p_strText, out clsT_aid_bih_ordercate_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_ordercate_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.* ,decode(a.isattach_int,1,'√',0,'×','') IsAttach FROM t_aid_bih_ordercate a 
			     Where 
                 LOWER(a.NAME_CHR) LIKE ? or LOWER(a.usercode_vchr) like ? order by ORDERSEQ_INT";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "" + p_strText.Trim().ToLower() + "%";
                parameters[1].Value = "%" + p_strText.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parameters);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_aid_bih_ordercate_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_aid_bih_ordercate_VO();
                        p_objResultArr[i1].m_strORDERCATEID_CHR = dtbResult.Rows[i1]["ORDERCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strNAME_CHR = dtbResult.Rows[i1]["NAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSOURCETABLE_VCHR = dtbResult.Rows[i1]["SOURCETABLE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTABLEPK_VCHR = dtbResult.Rows[i1]["TABLEPK_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDLLNAME_VCHR = dtbResult.Rows[i1]["DLLNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLASSNAME_VCHR = dtbResult.Rows[i1]["CLASSNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPRADD_VCHR = dtbResult.Rows[i1]["OPRADD_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPRUPD_VCHR = dtbResult.Rows[i1]["OPRUPD_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strVIEWNAME_VCHR = dtbResult.Rows[i1]["VIEWNAME_VCHR"].ToString().Trim();
                        //if(dtbResult.Rows[i1]["ISATTACH_INT"]!=System.DBNull.Value)
                        //{
                        //    p_objResultArr[i1].m_intISATTACH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISATTACH_INT"].ToString().Trim());
                        //}
                        //if(dtbResult.Rows[i1]["USAGEVIEWTYPE"]!=System.DBNull.Value)
                        //{
                        //    p_objResultArr[i1].m_intUSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[i1]["USAGEVIEWTYPE"].ToString().Trim());
                        //}
                        //if(dtbResult.Rows[i1]["DOSAGEVIEWTYPE"]!=System.DBNull.Value)
                        //{
                        //    p_objResultArr[i1].m_intDOSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[i1]["DOSAGEVIEWTYPE"].ToString().Trim());
                        //}
                        //非字段
                        //p_objResultArr[i1].m_strIsAttach =dtbResult.Rows[i1]["IsAttach"].ToString();

                        /*=================================================>*/
                        if (dtbResult.Rows[i1]["ISATTACH_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISATTACH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISATTACH_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["FEQVIEWTYPE"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intExecuFrenquenceType = Convert.ToInt32(dtbResult.Rows[i1]["FEQVIEWTYPE"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["USAGEVIEWTYPE"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intUSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[i1]["USAGEVIEWTYPE"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["DOSAGEVIEWTYPE"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intDOSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[i1]["DOSAGEVIEWTYPE"].ToString().Trim());
                        }
                        //收费标志
                        if (dtbResult.Rows[i1]["CREATECHARGETYPE"].ToString().Trim() != "")
                        {
                            p_objResultArr[i1].m_intChargeType = int.Parse(dtbResult.Rows[i1]["CREATECHARGETYPE"].ToString());
                        }
                        if (dtbResult.Rows[i1]["APPENDVIEWTYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intAPPENDVIEWTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["APPENDVIEWTYPE_INT"].ToString().Trim());

                        }
                        /*<=============================================*/
                        //是否显示数量 {1/2}
                        if (dtbResult.Rows[i1]["QTYVIEWTYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intQTYVIEWTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["QTYVIEWTYPE_INT"].ToString().Trim());

                        }
                        if (!dtbResult.Rows[i1]["ORDERSEQ_INT"].ToString().Trim().Equals(""))
                        {
                            p_objResultArr[i1].m_intORDERSEQ_INT = int.Parse(dtbResult.Rows[i1]["ORDERSEQ_INT"].ToString());
                        }
                        if (dtbResult.Rows[i1]["USERCODE_VCHR"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strUSERCODE_VCHR = dtbResult.Rows[i1]["USERCODE_VCHR"].ToString().Trim();
                        }
                        if (dtbResult.Rows[i1]["AUTOSHOW_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intAUTOSHOW_INT = Convert.ToInt32(dtbResult.Rows[i1]["AUTOSHOW_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["ORDERSELECT_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intORDERSELECT_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDERSELECT_INT"].ToString().Trim());
                        }

                        p_objResultArr[i1].m_intSAMEORDER_INT = int.Parse(dtbResult.Rows[i1]["SAMEORDER_INT"].ToString());

                    }
                }
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
        public long m_lngGetAidOrderCate(string p_strText, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = -1;
            string strSQL = @"select a.ordercateid_chr, a.name_chr, a.des_vchr, a.sourcetable_vchr,
                                     a.tablepk_vchr, a.dllname_vchr, a.classname_vchr, a.opradd_vchr,
                                     a.oprupd_vchr, a.isattach_int, a.viewname_vchr, a.usageviewtype,
                                     a.dosageviewtype, a.createchargetype, a.iscontrolmoney,
                                     a.feqviewtype, a.appendviewtype_int, a.qtyviewtype_int,
                                     a.orderseq_int, a.usercode_vchr, a.autoshow_int, a.orderselect_int,
                                     a.sameorder_int, a.changetype_int,
                                     decode (a.isattach_int, 1, '√', 0, '×', '') isattach,
                                     decode (a.usageviewtype, 1, '√', 2, '×', '') usageviewtypename,
                                     decode (a.dosageviewtype, 1, '√', 2, '×', '') dosageviewtypename,
                                     decode (a.feqviewtype, 1, '√', 2, '×', '') feqviewtypename,
                                     decode (a.appendviewtype_int,
                                             1, '√',
                                             2, '×',
                                             ''
                                            ) appendviewtypename,
                                     decode (a.qtyviewtype_int, 1, '√', 2, '×', '') qtyviewtypename,
                                     decode (a.autoshow_int, 1, '√', 2, '×', '') canautoshow_int,
                                     decode (a.orderselect_int, 1, '√', 2, '×', '') canorderselect_int,
                                     decode (a.sameorder_int, 1, '√', 2, '×', '') cansameorder_int,
                                     decode (a.changetype_int, 1, '√', 2, '×', '') canchangetype_int
                                from t_aid_bih_ordercate a
                               where lower(a.name_chr) like ? or lower(a.usercode_vchr) like ?
                            order by a.orderseq_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = -1;

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = "%" + p_strText.Trim().ToLower() + "%";
                paramArr[1].Value = "%" + p_strText.Trim().ToLower() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, paramArr);
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
        [AutoComplete]
        public long m_lngGetAidOrderCateByID(string p_strID, out clsT_aid_bih_ordercate_VO p_objResult)
        {
            p_objResult = new clsT_aid_bih_ordercate_VO();
            long lngRes = 0;
            string strSQL = @"select a.ordercateid_chr, a.name_chr, a.des_vchr, a.sourcetable_vchr,
                                   a.tablepk_vchr, a.dllname_vchr, a.classname_vchr, a.opradd_vchr,
                                   a.oprupd_vchr, a.isattach_int, a.viewname_vchr, a.usageviewtype,
                                   a.dosageviewtype, a.createchargetype, a.iscontrolmoney, a.feqviewtype,
                                   a.appendviewtype_int, a.qtyviewtype_int, a.orderseq_int,
                                   a.usercode_vchr, a.autoshow_int, a.orderselect_int, a.sameorder_int,
                                   a.changetype_int,
                                   decode (a.isattach_int, 1, '√', 0, '×', '') isattach
                              from t_aid_bih_ordercate a
                             where TRIM (a.ordercateid_chr) = ?";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strID.Trim();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, paramArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_aid_bih_ordercate_VO();
                    p_objResult.m_strORDERCATEID_CHR = dtbResult.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                    p_objResult.m_strNAME_CHR = dtbResult.Rows[0]["NAME_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strSOURCETABLE_VCHR = dtbResult.Rows[0]["SOURCETABLE_VCHR"].ToString().Trim();
                    p_objResult.m_strTABLEPK_VCHR = dtbResult.Rows[0]["TABLEPK_VCHR"].ToString().Trim();
                    p_objResult.m_strDLLNAME_VCHR = dtbResult.Rows[0]["DLLNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCLASSNAME_VCHR = dtbResult.Rows[0]["CLASSNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strOPRADD_VCHR = dtbResult.Rows[0]["OPRADD_VCHR"].ToString().Trim();
                    p_objResult.m_strOPRUPD_VCHR = dtbResult.Rows[0]["OPRUPD_VCHR"].ToString().Trim();
                    p_objResult.m_strVIEWNAME_VCHR = dtbResult.Rows[0]["VIEWNAME_VCHR"].ToString().Trim();

                    if (dtbResult.Rows[0]["ISATTACH_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intISATTACH_INT = Convert.ToInt32(dtbResult.Rows[0]["ISATTACH_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["FEQVIEWTYPE"] != System.DBNull.Value)
                    {
                        p_objResult.m_intExecuFrenquenceType = Convert.ToInt32(dtbResult.Rows[0]["FEQVIEWTYPE"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["USAGEVIEWTYPE"] != System.DBNull.Value)
                    {
                        p_objResult.m_intUSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[0]["USAGEVIEWTYPE"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["DOSAGEVIEWTYPE"] != System.DBNull.Value)
                    {
                        p_objResult.m_intDOSAGEVIEWTYPE = Convert.ToInt32(dtbResult.Rows[0]["DOSAGEVIEWTYPE"].ToString().Trim());
                    }
                    //非字段
                    p_objResult.m_strIsAttach = dtbResult.Rows[0]["IsAttach"].ToString();
                    //收费标志
                    if (dtbResult.Rows[0]["CREATECHARGETYPE"].ToString().Trim() != "")
                    {
                        p_objResult.m_intChargeType = int.Parse(dtbResult.Rows[0]["CREATECHARGETYPE"].ToString());
                    }
                    p_objResult.m_intAPPENDVIEWTYPE_INT = int.Parse(dtbResult.Rows[0]["APPENDVIEWTYPE_INT"].ToString());
                    p_objResult.m_intSelect = int.Parse(dtbResult.Rows[0]["ORDERSELECT_INT"].ToString());
                    p_objResult.m_intAUTOSHOW_INT = int.Parse(dtbResult.Rows[0]["AUTOSHOW_INT"].ToString());
                    p_objResult.m_intQTYVIEWTYPE_INT = int.Parse(dtbResult.Rows[0]["QTYVIEWTYPE_INT"].ToString());
                    p_objResult.m_intORDERSEQ_INT = int.Parse(dtbResult.Rows[0]["ORDERSEQ_INT"].ToString());
                    p_objResult.m_strUSERCODE_VCHR = dtbResult.Rows[0]["USERCODE_VCHR"].ToString();
                    p_objResult.m_intSAMEORDER_INT = int.Parse(dtbResult.Rows[0]["SAMEORDER_INT"].ToString());
                    p_objResult.m_intCHANGETYPE_INT = int.Parse(dtbResult.Rows[0]["CHANGETYPE_INT"].ToString());

                }
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
        #region 增加
        [AutoComplete]
        public long m_lngAddNewAidOrderCate(out string p_strRecordID, clsT_aid_bih_ordercate_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(2, "ORDERCATEID_CHR", "T_aid_bih_ordercate", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_aid_bih_ordercate (ORDERCATEID_CHR,NAME_CHR,DES_VCHR,SOURCETABLE_VCHR,TABLEPK_VCHR,DLLNAME_VCHR,CLASSNAME_VCHR,OPRADD_VCHR,OPRUPD_VCHR,ISATTACH_INT,VIEWNAME_VCHR,USAGEVIEWTYPE,DOSAGEVIEWTYPE,CREATECHARGETYPE,FEQVIEWTYPE,APPENDVIEWTYPE_INT,QTYVIEWTYPE_INT,ORDERSEQ_INT,USERCODE_VCHR,AUTOSHOW_INT,ORDERSELECT_INT,SAMEORDER_INT,CHANGETYPE_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(23, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strORDERCATEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strNAME_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strDES_VCHR;
                try
                {
                    objLisAddItemRefArr[3].Value = p_objRecord.m_strSOURCETABLE_VCHR;
                }
                catch
                {
                    objLisAddItemRefArr[3].Value = System.DBNull.Value;
                }
                objLisAddItemRefArr[4].Value = p_objRecord.m_strTABLEPK_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strDLLNAME_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strCLASSNAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strOPRADD_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strOPRUPD_VCHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_intISATTACH_INT;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strVIEWNAME_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_intUSAGEVIEWTYPE;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intDOSAGEVIEWTYPE;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intChargeType;
                objLisAddItemRefArr[14].Value = p_objRecord.m_intExecuFrenquenceType;
                objLisAddItemRefArr[15].Value = p_objRecord.m_intAPPENDVIEWTYPE_INT;
                objLisAddItemRefArr[16].Value = p_objRecord.m_intQTYVIEWTYPE_INT;
                objLisAddItemRefArr[17].Value = p_objRecord.m_intORDERSEQ_INT;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strUSERCODE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_intAUTOSHOW_INT;
                objLisAddItemRefArr[20].Value = p_objRecord.m_intSelect;
                objLisAddItemRefArr[21].Value = p_objRecord.m_intSAMEORDER_INT;
                objLisAddItemRefArr[22].Value = p_objRecord.m_intCHANGETYPE_INT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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
        #region 修改
        [AutoComplete]
        public long m_lngModifyAidOrderCate(string p_strID, clsT_aid_bih_ordercate_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"UPDATE T_AID_BIH_ORDERCATE A 
                               SET A.NAME_CHR = ?, 
                                   A.DES_VCHR = ?, 
                                   A.SOURCETABLE_VCHR =? ,
                                   A.TABLEPK_VCHR = ?,
                                   A.DLLNAME_VCHR =? ,
                                   A.CLASSNAME_VCHR = ?,
                                   A.OPRADD_VCHR = ? ,
                                   A.OPRUPD_VCHR = ? ,
                                   A.ISATTACH_INT = ? ,
                                   A.VIEWNAME_VCHR =? ,
                                   A.USAGEVIEWTYPE = ?,
                                   A.DOSAGEVIEWTYPE = ? ,
                                   A.CREATECHARGETYPE = ? ,
                                   A.FEQVIEWTYPE =?,
                                   A.APPENDVIEWTYPE_INT= ? ,
                                   A.QTYVIEWTYPE_INT=?,
                                   A.ORDERSEQ_INT=?,
                                   A.USERCODE_VCHR=?,
                                   A.AUTOSHOW_INT=?,
                                   A.ORDERSELECT_INT=?,
                                   A.SAMEORDER_INT=?,
                                   A.CHANGETYPE_INT=?
			                       WHERE A.ORDERCATEID_CHR =?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(23, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strNAME_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strSOURCETABLE_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strTABLEPK_VCHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strDLLNAME_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strCLASSNAME_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strOPRADD_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strOPRUPD_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_intISATTACH_INT.ToString();
                objLisAddItemRefArr[9].Value = p_objRecord.m_strVIEWNAME_VCHR.ToString();
                objLisAddItemRefArr[10].Value = p_objRecord.m_intUSAGEVIEWTYPE.ToString();
                objLisAddItemRefArr[11].Value = p_objRecord.m_intDOSAGEVIEWTYPE;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intChargeType.ToString();
                objLisAddItemRefArr[13].Value = p_objRecord.m_intExecuFrenquenceType.ToString();
                objLisAddItemRefArr[14].Value = p_objRecord.m_intAPPENDVIEWTYPE_INT.ToString();
                objLisAddItemRefArr[15].Value = p_objRecord.m_intQTYVIEWTYPE_INT.ToString();
                objLisAddItemRefArr[16].Value = p_objRecord.m_intORDERSEQ_INT;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strUSERCODE_VCHR;

                objLisAddItemRefArr[18].Value = p_objRecord.m_intAUTOSHOW_INT;
                objLisAddItemRefArr[19].Value = p_objRecord.m_intSelect;
                objLisAddItemRefArr[20].Value = p_objRecord.m_intSAMEORDER_INT;
                objLisAddItemRefArr[21].Value = p_objRecord.m_intCHANGETYPE_INT;
                objLisAddItemRefArr[22].Value = p_strID.Trim();
                long lngRecEff = -1;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);

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
        #region 删除
        [AutoComplete]
        public long m_lngDeleteAidOrderCate(string p_strID)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM T_AID_BIH_ORDERCATE A WHERE ORDERCATEID_CHR='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //事务
        #region 整体保存诊疗项目|收费项目
        /// <summary>
        /// 整体保存诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        [AutoComplete]
        public long m_lngSaveOrderdicChargeItem(DataTable p_dtDataName, ref clsT_bse_bih_orderdic_VO p_objResult)
        {
            long lngRes = 0;
            //给主收费项目赋值
            if (p_dtDataName.Rows.Count > 0)
            {
                try { p_objResult.m_strITEMID_CHR = p_dtDataName.Rows[0]["ChiefItemID"].ToString(); }
                catch { p_objResult.m_strITEMID_CHR = ""; }
            }
            string strRecordID = "";
            if (p_objResult.m_strORDERDICID_CHR == null || p_objResult.m_strORDERDICID_CHR == string.Empty)
            {	//增加
                lngRes = 0;
                lngRes = m_lngAddNewOrderdic(out strRecordID, p_objResult);
                p_objResult.m_strORDERDICID_CHR = strRecordID;
            }
            else
            {	//保存
                lngRes = 0;
                lngRes = m_lngModifyOrderdicByOrderdicid(p_objResult.m_strORDERDICID_CHR, p_objResult);
            }

            clsT_aid_bih_orderdic_charge_VO objItem = new clsT_aid_bih_orderdic_charge_VO();
            for (int i1 = 0; i1 < p_dtDataName.Rows.Count; i1++)
            {
                try { objItem.m_strOCMAPID_CHR = p_dtDataName.Rows[i1]["OCMAPID_CHR"].ToString(); }
                catch { }
                try { objItem.m_strITEMID_CHR = p_dtDataName.Rows[i1]["ITEMID_CHR"].ToString(); }
                catch { }
                try { objItem.m_strORDERDICID_CHR = p_objResult.m_strORDERDICID_CHR; }
                catch { }
                try { objItem.m_intQTY_INT = decimal.Parse(p_dtDataName.Rows[i1]["QTY_INT"].ToString()); }
                catch { objItem.m_intQTY_INT = 1; }
                try { objItem.m_intTYPE_INT = Int32.Parse(p_dtDataName.Rows[i1]["TYPE_INT"].ToString()); }
                catch { objItem.m_intTYPE_INT = 1; }
                //增加
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Added)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngAddNewOrderdicCharge(out strRecordID, objItem);
                    }
                }
                //删除
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Deleted)
                {
                    if (lngRes > 0)
                    {
                        //lngRes =0;
                        //lngRes =m_lngDeleteOrderdicChargeByOcmapid(p_objPrincipal,objItem.m_strOCMAPID_CHR);
                    }
                }
                //修改
                if (p_dtDataName.Rows[i1].RowState == DataRowState.Modified)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyOrderdicChargeByOcmapid(objItem.m_strOCMAPID_CHR, objItem);
                    }
                }
            }

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("操作错误!"));
            }
            return lngRes;
        }
        #endregion
        #region 非主收费项目计费
        /// <summary>
        /// 计算非主收费项目费用
        /// </summary>
        /// <param name="p_objRecipeFreq">用药频率辅助表对象</param>
        /// <param name="p_objOrderdicCharge">诊疗项目|收费项目对象</param>
        /// <param name="p_dmlGet">单位频率天数的领量	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        [AutoComplete]
        public long m_lngGetChargeNotMainItem(clsT_aid_recipefreq_VO p_objRecipeFreq, clsT_aid_bih_orderdic_charge_VO p_objOrderdicCharge, out decimal p_dmlGet)
        {
            p_dmlGet = 0;
            if (p_objRecipeFreq == null || p_objRecipeFreq.m_strFREQID_CHR == null || p_objRecipeFreq.m_strFREQID_CHR.Trim() == "") return -1;
            return m_lngGetChargeNotMainItem(p_objRecipeFreq.m_intTIMES_INT, p_objOrderdicCharge, out p_dmlGet);
        }
        /// <summary>
        /// 计算非主收费项目费用
        /// </summary>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="p_objOrderdicCharge">诊疗项目|收费项目对象</param>
        /// <param name="p_dmlGet">单位频率天数的领量	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        [AutoComplete]
        public long m_lngGetChargeNotMainItem(int p_intTIMES, clsT_aid_bih_orderdic_charge_VO p_objOrderdicCharge, out decimal p_dmlGet)
        {
            p_dmlGet = 0;
            if (p_objOrderdicCharge.m_intTYPE_INT == 2)//剂量单位
            {
                decimal dmlDosage = p_objOrderdicCharge.m_intQTY_INT;
                decimal dmlUnitDosage = System.Convert.ToDecimal(p_objOrderdicCharge.m_objChargeItem.m_dblDOSAGE_DEC);
                decimal dmlUse = dmlDosage / dmlUnitDosage;
                p_dmlGet = dmlUse * p_intTIMES;	//用量*次数
            }
            else if (p_objOrderdicCharge.m_intTYPE_INT == 1)//领量单位
            {
                p_dmlGet = p_objOrderdicCharge.m_intQTY_INT * p_intTIMES;
            }
            return 1;
        }
        /// <summary>
        /// 计算非主收费项目费用
        /// </summary>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="m_intQTY">数量	{if(p_intType==1) 一次领量; if(p_intType==2) 医生下的剂量;}</param>
        /// <param name="p_intType">{1=领量单位;2=剂量单位}</param>
        /// <param name="p_dmlDosage">医生下的剂量</param>
        /// <param name="p_dmlUnitDosage">单位剂量	{只有p_intType==2，此参数才有意义}</param>
        /// <param name="p_dmlGet">单位频率天数的领量	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        [AutoComplete]
        public long m_lngGetChargeNotMainItem(int p_intTIMES, int p_intQTY, int p_intType, decimal p_dmlDosage, decimal p_dmlUnitDosage, out decimal p_dmlGet)
        {
            p_dmlGet = 0;
            if (p_intType == 2)//剂量单位
            {
                decimal dmlUse = p_dmlDosage / p_dmlUnitDosage;
                p_dmlGet = dmlUse * p_intTIMES;	//用量*次数
            }
            else if (p_intType == 1)//领量单位
            {
                p_dmlGet = p_intQTY * p_intTIMES;
            }
            return 1;
        }
        #endregion

        //公用方法
        #region 按SQL——查询
        /// <summary>
        /// 按SQL——查询
        /// </summary>
        /// <param name="p_strSQLSelect">Select的SQL语句</param>
        /// <param name="p_dtbResult">[out 参数]</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngSelect(string p_strSQLSelect, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(p_strSQLSelect, ref p_dtbResult);
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
        #region 按SQL——修改
        /// <summary>
        /// 按SQL——修改Update
        /// </summary>
        /// <param name="p_strSQLUpdate">Update的SQL语句</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngUpDate(string p_strSQLUpdate)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLUpdate);
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
        #region 按SQL——删除
        /// <summary>
        /// 按SQL——删除
        /// </summary>
        /// <param name="p_strSQLDelete">Delete的SQL语句</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDelete(string p_strSQLDelete)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLDelete);
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
        #region 获取查询的数
        /// <summary>
        /// 获取查询的最大显示数目
        /// </summary>
        /// <returns>返回查询的最大显示数目</returns>
        public int m_intGetTop()
        {
            return 50;
        }
        #endregion

        #region 整体保存诊疗项目|收费项目
        /// <summary>
        /// 整体保存诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtDataName"></param>
        /// <param name="p_objResult"></param>
        [AutoComplete]
        public long m_lngSaveOrderdicChargeItem2(clsT_aid_bih_orderdic_charge_VO[] objItem, ref clsT_bse_bih_orderdic_VO p_objResult)
        {
            long lngRes = 0;
            //给主收费项目赋值
            if (objItem.Length > 0)
            {
                for (int i = 0; i < objItem.Length; i++)
                {
                    if (objItem[i].m_strIsChiefItem.ToString().Trim().Equals("1"))
                    {
                        p_objResult.m_strITEMID_CHR = objItem[i].m_strITEMID_CHR;
                        break;
                    }
                }
            }
            else
            {
                p_objResult.m_strITEMID_CHR = "";
            }
            string strRecordID = "";
            if (p_objResult.m_strORDERDICID_CHR == null || p_objResult.m_strORDERDICID_CHR == string.Empty)
            {	//增加
                lngRes = 0;
                lngRes = m_lngAddNewOrderdic(out strRecordID, p_objResult);
                p_objResult.m_strORDERDICID_CHR = strRecordID;
            }
            else
            {	//保存
                lngRes = 0;
                lngRes = m_lngModifyOrderdicByOrderdicid(p_objResult.m_strORDERDICID_CHR, p_objResult);
            }
            //同步组套项目
            if (lngRes > 0)
            {
                lngRes = m_lngUpdateTheGroupDataByOrderDic(p_objResult.m_strORDERDICID_CHR);
            }
            /*<==========================================*/

            m_lngDelOrderdicCharge(p_objResult);

            //clsT_aid_bih_orderdic_charge_VO objItem = new clsT_aid_bih_orderdic_charge_VO();
            for (int i1 = 0; i1 < objItem.Length; i1++)
            {
                objItem[i1].m_strORDERDICID_CHR = p_objResult.m_strORDERDICID_CHR;
                lngRes = 0;
                lngRes = m_lngAddNewOrderdicCharge2(out strRecordID, objItem[i1]);

            }

            //报告结果
            if (lngRes <= 0)
            {
                throw (new Exception("操作错误!"));
            }
            return lngRes;
        }

        private long m_lngUpdateTheGroupDataByOrderDic(string m_strORDERDICID_CHR)
        {
            long lngRes = 0;
            string strSQL = @" 
            update t_aid_bih_ordergroup_detail a
            set a.name_vchr=(select b.name_chr from t_bse_bih_orderdic b where b.orderdicid_chr=?)
            where
            a.ORDERDICID_CHR=? and (select c.VIEWNAME_VCHR from t_aid_bih_ordercate c,t_bse_bih_orderdic d where c.ORDERCATEID_CHR=d.ORDERCATEID_CHR and d.orderdicid_chr=a.orderdicid_chr and rownum=1)<>'文字医嘱'
            ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                parameters[0].Value = m_strORDERDICID_CHR;
                parameters[1].Value = m_strORDERDICID_CHR;
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parameters);
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

        #region 新增
        /// <summary>
        /// 新增诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderdicCharge2(out string p_strRecordID, clsT_aid_bih_orderdic_charge_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;


            //lngRes = objHRPSvc.lngGenerateID(18, "ocmapid_chr", "t_aid_bih_orderdic_charge", out p_strRecordID);
            string strSQL = "   select lpad(seq_ocmapid.NEXTVAL,18,'0') p_strRecordID   from dual ";
            DataTable dtbResult2 = new DataTable();
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
            if (lngRes > 0 && dtbResult2.Rows.Count > 0)
            {
                p_strRecordID = Convert.ToString(dtbResult2.Rows[0]["p_strRecordID"].ToString());
            }
            else
            {
                return -1;
            }
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSQL = @"INSERT INTO t_aid_bih_orderdic_charge (OCMAPID_CHR, ORDERDICID_CHR, ITEMID_CHR, QTY_INT, TYPE_INT, USESCOPE_INT, CONTINUEUSETYPE_INT,CONTINUEFREQID_CHR) 
                                                      VALUES (?, ?, ?, ?, ?, ?, ?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(8, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strORDERDICID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intQTY_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_intTYPE_INT;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intUSESCOPE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intCONTINUEUSETYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strCONTINUEFREQID_CHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region 删除
        /// <summary>
        /// 删除诊疗项目|收费项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOrderdicCharge(clsT_bse_bih_orderdic_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            try
            {
                string strSQL = "delete from t_aid_bih_orderdic_charge where ORDERDICID_CHR='" + p_objRecord.m_strORDERDICID_CHR + "'";
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes < 0)
                    return lngRes;

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

        #region 医嘱类型查询
        /// <summary>
        /// 医嘱类型查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFind"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAidOrderCateByFindString(string p_strFind, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"
                    SELECT a.*
                    FROM t_aid_bih_ordercate a
                    WHERE 
                    LOWER (TRIM (a.USERCODE_VCHR)) LIKE ?
                    or
                    LOWER (TRIM (a.name_chr)) LIKE ?
					order by a.ordercateid_chr
                   ";


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = "%" + p_strFind + "%";
                parameters[1].Value = "%" + p_strFind + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResultArr, parameters);
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


        #region 茶山护理工作量报表字段维护
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="p_objVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNursingField(clsRptNursingForCS_VO p_objVO)
        {
            long lngRes = -1;
            long lngAffect = 0;
            string strSQL = @"insert into t_bse_rptnursing
                                    (seqid_chr, nurname_vchr, orderdicid_chr, nurunit_vchr,
                                     nurscore_int, status_int, disporder_int)
                             values (seq_rptnursing.NEXTVAL, ?, ?, ?,
                                     ?, ? ,?) ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(6, out param);
                param[0].Value = p_objVO.m_strNurseName;
                param[1].Value = p_objVO.m_strOrderdic;
                param[2].Value = p_objVO.m_strNurseUnit;
                param[3].DbType = DbType.Int16;
                param[3].Value = p_objVO.m_intSorce;
                param[4].DbType = DbType.Int16;
                param[4].Value = p_objVO.m_intStatus;
                param[5].Value = p_objVO.m_intDisporder;
                param[5].DbType = DbType.Int16;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffect, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
                param = null;
                strSQL = "";
                p_objVO = null;
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
        /// 获取数据
        /// </summary>
        /// <param name="p_objVOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNursingRpt(out clsRptNursingForCS_VO[] p_objVOArr)
        {
            long lngRes = -1;
            p_objVOArr = null;
            try
            {
                string strSQL = @"select   a.seqid_chr, a.nurname_vchr, a.orderdicid_chr, a.nurunit_vchr,a.disporder_int,
                                           a.nurscore_int, a.status_int, b.name_chr
                                      from t_bse_rptnursing a, t_bse_bih_orderdic b
                                     where a.orderdicid_chr = b.orderdicid_chr";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable dt = null;

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                objHRPSvc.Dispose();

                if (lngRes < 0 || dt == null || dt.Rows.Count < 1)
                {
                    strSQL = "";
                    return lngRes;
                }

                int intRow = dt.Rows.Count;
                DataRow[] drArr = dt.Select("", "disporder_int");
                DataRow dr = null;
                p_objVOArr = new clsRptNursingForCS_VO[intRow];
                for (int i1 = 0; i1 < intRow; i1++)
                {
                    dr = drArr[i1];
                    p_objVOArr[i1] = new clsRptNursingForCS_VO();

                    p_objVOArr[i1].m_strSEQID = dr["seqid_chr"].ToString();
                    p_objVOArr[i1].m_strOrderdic = dr["orderdicid_chr"].ToString();
                    p_objVOArr[i1].m_strNurseUnit = dr["nurunit_vchr"].ToString();
                    p_objVOArr[i1].m_strNurseName = dr["nurname_vchr"].ToString();
                    p_objVOArr[i1].m_intSorce = Convert.ToInt16(dr["nurscore_int"]);
                    p_objVOArr[i1].m_intStatus = Convert.ToInt16(dr["status_int"]);
                    p_objVOArr[i1].m_strOrderdicName = dr["name_chr"].ToString();
                    if (dr["disporder_int"] != DBNull.Value)
                    {
                        p_objVOArr[i1].m_intDisporder = Convert.ToInt16(dr["disporder_int"]);
                    }
                }
                dt.Dispose();
                dt = null;
                dr = null;
                drArr = null;
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
        /// 更新
        /// </summary>
        /// <param name="p_objVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateNursingRpt(clsRptNursingForCS_VO p_objVO)
        {
            long lngRes = -1;
            long lngAffter = -1;
            string strSQL = @"update t_bse_rptnursing
                               set nurname_vchr = ?,
                                   orderdicid_chr = ?,
                                   nurunit_vchr = ?,
                                   nurscore_int = ?,
                                   status_int = ?,
                                   disporder_int = ?
                             where seqid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(7, out param);
                param[0].Value = p_objVO.m_strNurseName;
                param[1].Value = p_objVO.m_strOrderdic;
                param[2].Value = p_objVO.m_strNurseUnit;
                param[3].DbType = DbType.Int16;
                param[3].Value = p_objVO.m_intSorce;
                param[4].DbType = DbType.Int16;
                param[4].Value = p_objVO.m_intStatus;
                param[5].Value = p_objVO.m_intDisporder;
                param[5].DbType = DbType.Int16;
                param[6].Value = p_objVO.m_strSEQID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
                p_objVO = null;
                strSQL = "";
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
        /// 删除字段
        /// </summary>
        /// <param name="p_strSEQID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteNursingRpt(string p_strSEQID)
        {
            long lngRes = -1;
            long lngAffect = -1;
            string strSQL = "delete from t_bse_rptnursing where seqid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = p_strSEQID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffect, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = "";
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
        /// 诊疗项目
        /// </summary>
        /// <param name="p_intType">0 拼音码 1 项目名称 2 用户代码</param>
        /// <param name="p_strContent"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderDic(int p_intType, string p_strContent, out DataTable dtResult)
        {
            long lngRes = -1;
            dtResult = null;
            try
            {
                string strSQL = "";
                if (p_intType == 0)
                {
                    strSQL = @"select a.orderdicid_chr, a.name_chr,a.usercode_chr,
                                       nvl (a.nullitemdosageunit_chr, b.dosageunit_chr) as unit
                                  from t_bse_bih_orderdic a, t_bse_chargeitem b
                                 where a.itemid_chr = b.itemid_chr(+)
                                 and a.status_int=1
                                 and b.ifstop_int=0
                                 and a.pycode_chr like ? ";
                }
                else if (p_intType == 1)
                {
                    strSQL = @"select a.orderdicid_chr, a.name_chr,a.usercode_chr,
                                       nvl (a.nullitemdosageunit_chr, b.dosageunit_chr) as unit
                                  from t_bse_bih_orderdic a, t_bse_chargeitem b
                                 where a.itemid_chr = b.itemid_chr(+)
                                 and a.status_int=1
                                 and b.ifstop_int=0
                                 and a.name_chr like ? ";
                }
                else
                {
                    strSQL = @"select a.orderdicid_chr, a.name_chr,a.usercode_chr,
                                       nvl (a.nullitemdosageunit_chr, b.dosageunit_chr) as unit
                                  from t_bse_bih_orderdic a, t_bse_chargeitem b
                                 where a.itemid_chr = b.itemid_chr(+)
                                 and a.status_int=1
                                 and b.ifstop_int=0
                                 and a.usercode_chr like ? ";
                }

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = p_strContent + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, param);

                param = null;
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = null;
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

        #region 维护:诊疗项目-检验申请单元(一对多,如:糖耐量)

        #region 保存:诊疗项目-检验申请单元(一对多,如:糖耐量)
        /// <summary>
        /// 保存:诊疗项目-检验申请单元(一对多,如:糖耐量)
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <param name="lstApplyUnitId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long SaveOrderDicLisApplyUnit(string orderDicId, List<string> lstApplyUnitId)
        {
            long lngRes = -1;
            long lngAffect = -1;
            string Sql = "delete from t_bse_bih_orderdic_applyunit where orderdicid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = orderDicId;
                lngRes = objHRPSvc.lngExecuteParameterSQL(Sql, ref lngAffect, param);

                if (lstApplyUnitId != null && lstApplyUnitId.Count > 0)
                {
                    int no = 0;
                    Sql = @"insert into t_bse_bih_orderdic_applyunit (orderdicid_chr, lisapplyunitid_chr, sortno_int) values (?, ?, ?)";
                    foreach (string applyUnitId in lstApplyUnitId)
                    {
                        objHRPSvc.CreateDatabaseParameter(3, out param);
                        param[0].Value = orderDicId;
                        param[1].Value = applyUnitId;
                        param[2].Value = ++no;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(Sql, ref lngAffect, param);
                    }
                }
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

        #region 查询:诊疗项目-检验申请单元(一对多,如:糖耐量)
        /// <summary>
        /// 查询:诊疗项目-检验申请单元(一对多,如:糖耐量)
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOrderDicLisApplyUnit(string orderDicId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select a.orderdicid_chr,
                               a.lisapplyunitid_chr,
                               a.sortno_int,
                               b.apply_unit_name_vchr
                          from t_bse_bih_orderdic_applyunit a
                         inner join t_aid_lis_apply_unit b
                            on a.lisapplyunitid_chr = b.apply_unit_id_chr
                         where a.orderdicid_chr = ? 
                        order by a.sortno_int ";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = orderDicId;
                objHRPSvc.lngGetDataTableWithParameters(Sql, ref dt, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #endregion

        #region 获取外送单位
        /// <summary>
        /// 获取外送单位
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOutsideUnit()
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select a.funitno, a.funitname from t_aid_outsideunit a";

                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion
    }
}
