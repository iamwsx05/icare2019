using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 常用诊疗项目维护 
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCommonUseOrderDicSVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region 构造函数
        /// <summary>
        /// 常用诊疗项目维护
        /// </summary>
        public clsCommonUseOrderDicSVC()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion
        //T_bse_bih_orderdic(诊疗项目)
        #region 查询
        /// <summary>
        /// 查询诊疗项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdic(out DataTable dtbResult, int m_intQueryType)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.*,b.itemname_vchr as Item,c.name_chr as OrderCate,d.deptname_vchr as Execdept,e.IPNOQTYFLAG_INT,b.ITEMSPEC_VCHR from t_bse_bih_orderdic a,t_bse_chargeitem b,t_aid_bih_ordercate c, t_bse_deptdesc d ,t_bse_medicine e
                            where  a.execdept_chr=d.deptid_chr(+)  and a.itemid_chr=b.itemid_chr(+) and a.ordercateid_chr=c.ordercateid_chr(+)  AND a.status_int <> 0   and b.itemsrcid_vchr=e.medicineid_chr(+) 
                            order by a.usercode_chr ";
            if (m_intQueryType == 1)
            {
                strSQL = @"select a.*,b.itemname_vchr as Item,c.name_chr as OrderCate,d.deptname_vchr as Execdept,e.IPNOQTYFLAG_INT,b.ITEMSPEC_VCHR from t_bse_bih_orderdic a,t_bse_chargeitem b,t_aid_bih_ordercate c, t_bse_deptdesc d ,t_bse_medicine e
                            where  a.execdept_chr=d.deptid_chr(+)  and a.itemid_chr=b.itemid_chr(+) and a.ordercateid_chr=c.ordercateid_chr(+)  AND a.status_int <> 0   and b.itemsrcid_vchr=e.medicineid_chr(+)  and rownum<=500
                            order by a.usercode_chr  ";
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
        /// 查询诊疗项目-按诊疗项目类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strName">诊疗项目类型</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByName(string p_strName, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.*,b.itemname_vchr as Item,b.ITEMSPEC_VCHR,c.name_chr as OrderCate,d.deptname_vchr as Execdept,e.IPNOQTYFLAG_INT from t_bse_bih_orderdic a,t_bse_chargeitem b,t_aid_bih_ordercate c, t_bse_deptdesc d ,t_bse_medicine e
                            where  a.execdept_chr=d.deptid_chr(+)  and a.itemid_chr=b.itemid_chr(+) and a.ordercateid_chr=c.ordercateid_chr(+)  and b.itemsrcid_vchr=e.medicineid_chr(+) 
                            and c.ORDERCATEID_CHR=? and a.status_int <> 0 order by a.usercode_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(1, out parameters);
                parameters[0].Value = p_strName;
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
        #endregion
        #region 
        /// <summary>
        /// 查询诊疗项目-按查询内容和查询条件
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ORDERCATEID_CHR"></param>
        /// <param name="p_strContent">查询内容</param>
        /// <param name="p_strCondition">查询条件</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderdicByCondtion(string ORDERCATEID_CHR, string p_strCondition, string p_strContent, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            if (ORDERCATEID_CHR.Trim().Equals(""))
            {
                strSQL = @"select a.*,b.itemname_vchr as Item,b.ITEMSPEC_VCHR,c.name_chr as OrderCate,d.deptname_vchr as Execdept,e.IPNOQTYFLAG_INT from t_bse_bih_orderdic a,t_bse_chargeitem b,t_aid_bih_ordercate c, t_bse_deptdesc d ,t_bse_medicine e
                            where  a.execdept_chr=d.deptid_chr(+)  and a.itemid_chr=b.itemid_chr(+) and a.ordercateid_chr=c.ordercateid_chr(+)  and b.itemsrcid_vchr=e.medicineid_chr(+) 
                            and a.STATUS_INT=1 
                            and upper(a.[p_strCondition]) like ? 
                            order by a.usercode_chr";
            }
            else
            {
                strSQL = @"select a.*,b.itemname_vchr as Item,b.ITEMSPEC_VCHR,c.name_chr as OrderCate,d.deptname_vchr as Execdept,e.IPNOQTYFLAG_INT from t_bse_bih_orderdic a,t_bse_chargeitem b,t_aid_bih_ordercate c, t_bse_deptdesc d ,t_bse_medicine e
                            where  a.execdept_chr=d.deptid_chr(+)  and a.itemid_chr=b.itemid_chr(+) and a.ordercateid_chr=c.ordercateid_chr(+)  and b.itemsrcid_vchr=e.medicineid_chr(+) 
                            and a.STATUS_INT=1 and c.ORDERCATEID_CHR=? 
                            and upper(a.[p_strCondition]) like ? 
                            order by a.usercode_chr";
            }
            if (p_strCondition != "")
            {
                strSQL = strSQL.Replace("[p_strCondition]", p_strCondition.Trim().ToUpper());
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] parameters = null;
                if (ORDERCATEID_CHR.Trim().Equals(""))
                {

                    objHRPSvc.CreateDatabaseParameter(1, out parameters);
                    parameters[0].Value = p_strContent.Trim().ToUpper() + "%";
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(2, out parameters);
                    parameters[0].Value = ORDERCATEID_CHR;
                    parameters[1].Value = p_strContent.Trim().ToUpper() + "%";
                }
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
        #endregion
        #region 查询常用诊疗项目
        /// <summary>
        /// 查询常用诊疗项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strEmpID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCommonUseOrderdic(string m_strEmpID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.*,c.usercode_chr as USERCODE_CHR ,decode (a.DES_VCHR , null,c.name_chr,a.des_vchr  ) as NAME_CHR,b.deptname_vchr as Execdept,d.name_chr as OrderCate ,e.itemname_vchr as Item, decode(a.privilege_int,0,'个人',1,'科室','') as PRIVILEGE_name , c.status_int ,f.IPNOQTYFLAG_INT,e.ITEMSPEC_VCHR from t_aid_bih_comuseorderdic a ,t_bse_deptdesc b,t_bse_bih_orderdic c,
                             t_aid_bih_ordercate d,t_bse_chargeitem e ,t_bse_medicine f
                             where a.deptid_chr=b.deptid_chr(+) and a.orderdicid_chr=c.orderdicid_chr(+) and c.ordercateid_chr=d.ordercateid_chr(+) 
                             and c.itemid_chr=e.itemid_chr(+) and e.itemsrcid_vchr=f.medicineid_chr(+) 
                             AND a.CREATERID_CHR=?
                             order by  c.usercode_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] parameters = null;
                objHRPSvc.CreateDatabaseParameter(1, out parameters);
                //Please change the datetime and reocrdid 
                parameters[0].Value = m_strEmpID;
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

        //保存增加或删除常用诊疗项目
        /// <summary>
        /// 保存增加或删除常用诊疗项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objDtSource"></param>
        /// <param name="m_objDtDelete"></param>
        /// <param name="strType">医生或者护士</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveCommonUseOrder(System.Data.DataTable m_objDtSource, System.Data.DataTable m_objDtDelete, string strType)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            if (m_objDtDelete != null)
            {

                for (int i1 = 0; i1 < m_objDtDelete.Rows.Count; i1++)
                {
                    strSQL = "DELETE t_aid_bih_comuseorderdic WHERE SEQID_CHR =? ";
                    try
                    {
                        System.Data.IDataParameter[] parameters = null;
                        objHRPSvc.CreateDatabaseParameter(1, out parameters);
                        parameters[0].Value = m_objDtDelete.Rows[i1]["SEQID_CHR"].ToString().Trim();
                        long lngAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, parameters);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
            }
            if (m_objDtSource != null)
            {
                string strSeqid = "";
                for (int i1 = 0; i1 < m_objDtSource.Rows.Count; i1++)
                {
                    if (m_objDtSource.Rows[i1].RowState == System.Data.DataRowState.Added)
                    {
                        //objHRPSvc.lngGenerateID(10, "seqid_chr", "t_aid_bih_comuseorderdic", out strSeqid);
                        strSQL = @"select seq_comuseid.NEXTVAL from dual ";
                        DataTable m_objTable = new DataTable();
                        objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                        if (m_objTable.Rows.Count > 0)
                        {
                            strSeqid = m_objTable.Rows[0][0].ToString().PadLeft(10, '0'); ;
                        }
                        strSQL = @"insert into t_aid_bih_comuseorderdic
								(seqid_chr,CREATE_DAT, DEPTID_CHR ,ORDERDICID_CHR ,CREATERID_CHR, privilege_int,TYPE_INT)
								values
								(?,?,?,?,?,?,?)";

                        try
                        {

                            System.Data.IDataParameter[] parameters = null;
                            objHRPSvc.CreateDatabaseParameter(7, out parameters);
                            parameters[0].Value = strSeqid;
                            parameters[1].Value = m_objDtSource.Rows[i1]["CREATE_DAT"];
                            parameters[2].Value = m_objDtSource.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                            parameters[3].Value = m_objDtSource.Rows[i1]["ORDERDICID_CHR"].ToString().Trim();
                            parameters[4].Value = m_objDtSource.Rows[i1]["CREATERID_CHR"].ToString().Trim();
                            parameters[5].Value = m_objDtSource.Rows[i1]["privilege_int"];
                            parameters[6].Value = int.Parse(strType);
                            long lngAffected = -1;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, parameters);

                            m_objDtSource.Rows[i1]["seqid_chr"] = strSeqid;

                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region 根据ID获取角色
        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="strEmpID"></param>
        /// <param name="arrRoles"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRolesByID(string strEmpID, out System.Collections.Generic.List<string> arrRoles)
        {
            string strSQL = @"select a.roleid_chr
                              from t_sys_emprolemap a, t_sys_role b
                             where a.roleid_chr = b.roleid_chr and a.empid_chr = ? ";
            long lngRes = -1;
            arrRoles = new System.Collections.Generic.List<string>();
            try
            {
                DataTable dt = new DataTable();
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = strEmpID.Trim();

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param);
                if (lngRes > 0)
                {
                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        arrRoles.Add(dt.Rows[i1]["roleid_chr"].ToString());
                    }
                }
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


        /// <summary>
        /// 修改时间
        /// </summary>
        /// <param name="newTime"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <param name="hasOrder"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyTime(DateTime newTime, string strEmpID, string strEmpName, System.Collections.Generic.Dictionary<string, string> hasOrder, string OrderID)
        {
            string strSQL = @"update t_opr_bih_orderexecute
                                   set createdate_dat = ?,
                                       operatorid_chr = ?,
                                       operator_chr = ?
                                 where orderexecid_chr = ? ";
            ArrayList ArrID = new ArrayList(hasOrder.Keys);
            long lngAffter = 0;
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                DbType[] dbtypes = new DbType[] { DbType.DateTime, DbType.String, DbType.String, DbType.String };
                object[][] objValues = new object[4][];
                for (int k = 0; k < objValues.Length; k++)
                {
                    objValues[k] = new object[ArrID.Count];
                }

                for (int i1 = 0; i1 < ArrID.Count; i1++)
                {
                    int n = 0;
                    objValues[n++][i1] = newTime;
                    objValues[n++][i1] = strEmpID;
                    objValues[n++][i1] = strEmpName;
                    objValues[n++][i1] = ArrID[i1].ToString();
                }
                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbtypes);

                objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                strSQL = @"update  t_opr_bih_order
                               set startdate_dat = ?
                             where orderid_chr = ? ";
                foreach (string obj in ArrID)
                {
                    if (int.Parse(hasOrder[obj].ToString()) == 1)
                    {
                        objHRPSvc.CreateDatabaseParameter(2, out param);
                        param[0].Value = newTime;
                        param[1].Value = OrderID;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                        break;
                    }
                }

                strSQL = @"update t_opr_bih_order
                               set executedate_dat = ?
                             where orderid_chr = ? ";
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].Value = newTime;
                param[1].Value = OrderID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);

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
        /// 根据医嘱ID获得执行单
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <param name="dt"></param>
        /// <param name="SecondMaxTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecute(string strOrderID, out DataTable dt, ref string SecondMaxTime)
        {
            string strSQL = @"select a.orderexecid_chr, a.createdate_dat, a.orderid_chr,
                                     case
                                        when a.createdate_dat = b.startdate_dat
                                           then 1
                                        else 0
                                     end as firstflag, b.postdate_dat, a.isrecruit_int
                                from t_opr_bih_orderexecute a, t_opr_bih_order b
                               where a.orderid_chr = b.orderid_chr
                                 and trunc (a.createdate_dat) = trunc (sysdate )
                                 and b.orderid_chr = ?
                            order by a.createdate_dat desc ";
            dt = null;
            SecondMaxTime = "";
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = strOrderID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param);
                if (dt.Rows.Count == 0)
                {
                    objHRPSvc.Dispose();
                    return lngRes;
                }

                DataTable dtTmp = new DataTable();
                strSQL = @"select max (a.createdate_dat) as createdate from t_opr_bih_orderexecute a where a.orderid_chr = ? and a.createdate_dat <> ? ";
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].Value = strOrderID;
                param[1].DbType = DbType.DateTime;
                param[1].Value = Convert.ToDateTime(dt.Rows[0]["createdate_dat"]);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, param);
                if (dtTmp.Rows.Count > 0 && dtTmp.Rows[0][0].ToString().Length > 0)
                {
                    SecondMaxTime = Convert.ToDateTime(dtTmp.Rows[0][0]).ToString("yyyy-MM-dd HH:mm:ss");
                }

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
    }
}
