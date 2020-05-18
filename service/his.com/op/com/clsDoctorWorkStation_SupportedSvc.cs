using System;
using System.Collections.Generic;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
using com.digitalwave.iCare.middletier.HIS;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ��ѯ����м��
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsDoctorWorkStation_SupportedSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDoctorWorkStation_SupportedSvc()
        {
        }
        #endregion

        #region ����ҩ��ID��ҩƷ��ȡ��ǰ���ۿ�棨��ɽ��ʱ�ò������ۿ�棬��Ϊ���ʵ�ʿ�棩
        /// <summary>
        /// ����ҩ��ID��ҩƷ��ȡ��ǰ���ۿ�棨��ɽ��ʱ�ò������ۿ�棬��Ϊ���ʵ�ʿ�棩
        /// </summary>
        /// <param name="strStoreID"></param>
        /// <param name="strItemID"></param>
        /// <param name="strDeductType"></param>
        /// <param name="dtInventory"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTheoryAmountByMedID(string strStoreID, string strItemID, string strDeductType, out DataTable dtInventory)
        {
            long lngRes = 0;
            string strSub = "";
            string SQL = string.Empty;
            dtInventory = new DataTable();

            SQL = @"select a.drugstoreid_chr,
                           a.seriesid_int,
                           a.medicineid_chr,
                           a.instoragedate_dat,
                           b.itemid_chr,
                           b.itemcode_vchr,
                           a.validperiod_dat,
                           a.lotno_vchr,
                           a.packqty_dec,
                           a.storagerackid_chr,
                           a.oprealgross_int   as jbsl,
                           a.iprealgross_int   as zxsl,
                           m.assistcode_chr    as medcode
                      from t_ds_storage_detail a,
                           t_bse_chargeitem    b,
                           t_ds_storage        c,
                           t_bse_medicine      m
                     where b.itemsrcid_vchr = a.medicineid_chr
                       and a.medicineid_chr = m.medicineid_chr
                       and a.status = 1
                       and a.canprovide_int = 1
                       and a.medicineid_chr = c.medicineid_chr
                       and c.noqtyflag_int = 0
                       and c.ifstop_int = 0
                       and a.drugstoreid_chr = c.drugstoreid_chr
                       and a.drugstoreid_chr in ({0})
                       and b.itemid_chr in ({1}) 
                    ";
            SQL = string.Format(SQL, strStoreID, strItemID);

            if (strDeductType == "1")
            {
                strSub = " order by a.dsinstoragedate_dat, a.instoreid_vchr";
            }
            else if (strDeductType == "2")
            {
                strSub = " order by a.validperiod_dat, a.instoreid_vchr";
            }
            else if (strDeductType == "3")
            {
                strSub = " order by a.dsinstoragedate_dat, a.validperiod_dat, a.instoreid_vchr";
            }
            else if (strDeductType == "4")
            {
                strSub = " order by a.validperiod_dat, a.dsinstoragedate_dat, a.instoreid_vchr";
            }
            SQL = SQL + strSub;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtInventory);
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

        #region �����շ���ĿID��ѯҩƷID
        /// <summary>
        /// �����շ���ĿID��ѯҩƷID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMedicineIDByChargeItemID(string ID)
        {
            long lngRes = 0;
            string strMedID = "";

            string SQL = @"select  a.itemsrcid_vchr
                              from t_bse_chargeitem a, t_bse_medicine b
                             where a.itemsrcid_vchr = b.medicineid_chr 
                               and a.itemid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;

                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    strMedID = dt.Rows[0][0].ToString().Trim();
                }

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return strMedID;
        }
        #endregion

        #region ��ȡҩ��������Ϣ
        /// <summary>
        /// ��ȡҩ��������Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStore(out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select  a.medstoreid_chr, a.medstorename_vchr, a.medstoretype_int,
                                   a.medicnetype_int, a.urgence_int, a.deptid_chr
                              from t_bse_medstore a";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
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

        #region ΢�ż���Ƿ��
        /// <summary>
        /// ΢�ż���Ƿ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsWechatBanding(string cardNo)
        {
            string Sql = @"select t.cardno from opRegWeChatBinding t where t.cardno = ? and t.status = 1";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = cardNo;

                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;    // ���ڰ�
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ҩƷ����ж�
        /// <summary>
        /// ҩƷ����ж�
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsHaveDrugStock(string itemId, string storeId)
        {
            string Sql = @"select 1
                              from t_bse_chargeitem a
                             inner join t_bse_medicine b
                                on a.itemsrcid_vchr = b.medicineid_chr
                             where a.ifstop_int = 0
                               and b.ifstop_int = 0
                               and a.itemid_chr = ?
                               and exists (select 1
                                      from t_ds_storage t1, t_ds_storage_detail t2
                                     where t1.medicineid_chr = t2.medicineid_chr
                                       and t1.medicineid_chr = b.medicineid_chr
                                       and t1.drugstoreid_chr = t2.drugstoreid_chr
                                       and t1.drugstoreid_chr = ?
                                       and t1.noqtyflag_int = 0         --ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                                       and t1.ifstop_int = 0            --ͣ�ñ�־ 0-���� 1-ͣ��
                                       and t2.canprovide_Int = 1
                                       and t2.iprealgross_Int > 0)";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = itemId;
                parm[1].Value = storeId;
                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;    // ����ҩƷ�����
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ���ұ���
        /// <summary>
        /// ���ұ���
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetDeptCode(string deptId)
        {
            string deptCode = string.Empty;
            string Sql = @"select t.code_vchr from t_bse_deptdesc t where t.deptid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = deptId;
                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                if (dt != null && dt.Rows.Count > 0)
                {
                    deptCode = dt.Rows[0]["code_vchr"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return deptCode;
        }
        #endregion

        #region ��ȡ�ֳ���ʹ�ô�����Ϣ
        /// <summary>
        /// ��ȡ�ֳ���ʹ�ô�����Ϣ
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetTodayRegInfo(string recipeId)
        {
            string Sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                Sql = @"select a.serno,
                               a.deptcode,
                               a.doctcode,
                               a.subscribeno,
                               c.lastname_vchr     as patName,
                               c.idcard_chr        as idNo,
                               c.sex_chr           as sex,
                               c.homephone_vchr    as contactTel,
                               d.patientcardid_chr as cardNo
                          from opRegPlatformLog a
                         inner join t_opr_outpatientrecipe b
                            on a.recipeid = b.outpatrecipeid_chr
                         inner join t_bse_patient c
                            on b.patientid_chr = c.patientid_chr
                         inner join t_bse_patientcard d
                            on b.patientid_chr = d.patientid_chr
                         where a.recipeid = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = recipeId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
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

        #region �жϴ������Ƿ�ֻ������Ƭ,�����������
        /// <summary>
        /// �жϴ������Ƿ�ֻ������Ƭ,�����������
        /// </summary>
        /// <param name="lstChargeItemId"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool CheckRecipeSlices(List<string> lstChargeItemId)
        {
            string Sql = string.Empty;
            bool ret = false;
            DataTable dt = new DataTable();
            try
            {
                Sql = @"select distinct b.medicinepreptype_chr
                          from t_bse_chargeitem a
                         inner join t_bse_medicine b
                            on a.itemsrcid_vchr = b.medicineid_chr
                         where a.itemid_chr in ({0})";

                string itemIdArr = string.Empty;
                foreach (string id in lstChargeItemId)
                {
                    itemIdArr += "'" + id + "',";
                }
                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(string.Format(Sql, itemIdArr.TrimEnd(',')), ref dt);
                if (dt != null && dt.Rows.Count == 1 && dt.Rows[0]["medicinepreptype_chr"].ToString() == "12")   // ��Ƭ
                {
                    Sql = @"select a.itemid_chr, c.fmednamegy
                              from t_bse_chargeitem a
                             inner join t_bse_medicine b
                                on a.itemsrcid_vchr = b.medicineid_chr
                              left join dggycmedlist c
                                on (b.medicinename_vchr = c.fmednamegy or
                                   b.medicinename_vchr = c.fmednameyy)
                             where a.itemid_chr in ({0})";
                    svc.lngGetDataTableWithoutParameters(string.Format(Sql, itemIdArr.TrimEnd(',')), ref dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["fmednamegy"] == DBNull.Value || string.IsNullOrEmpty(dr["fmednamegy"].ToString()))
                            {
                                return false;
                            }
                        }
                    }
                    ret = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return ret;
        }
        #endregion

        #region ���ߴ���ҩ�ռ���Ϣ
        /// <summary>
        /// ���ߴ���ҩ�ռ���Ϣ
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPatientContactInfo(string patientId)
        {
            string Sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                Sql = @"select t.contactpersonfirstname_vchr,
                               t.contactpersonlastname_vchr,
                               t.mobile_chr,
                               t.consigneeaddr,
                               t.birth_dat 
                          from t_bse_patient t
                         where t.patientid_chr = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = patientId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
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

        #region ��Ӧ֢
        /// <summary>
        /// �ж���Ӧ֢
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemName"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool CheckIndiction(string itemId, out string itemName, out string remark)
        {
            itemName = string.Empty;
            remark = string.Empty;
            string Sql = string.Empty;
            try
            {
                Sql = @"select distinct a.hisitemcode_vchr,
                                        a.ybitemcode_vchr,
                                        a.itemname_vchr,
                                        a.xzsysm
                          from t_bse_chargeitemybrla a
                         inner join t_bse_chargeitem b
                            on a.hisitemcode_vchr = b.itemcode_vchr
                         where exists (select 1
                                  from t_bse_shiying t
                                 where t.menucode_vchr = a.ybitemcode_vchr)
                           and b.itemid_chr = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = itemId;
                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    itemName = dt.Rows[0]["itemname_vchr"].ToString();
                    remark = dt.Rows[0]["xzsysm"].ToString();
                    return true;
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
            return false;

        }
        #endregion
    }
}
