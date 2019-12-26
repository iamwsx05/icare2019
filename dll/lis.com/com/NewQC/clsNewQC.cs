using System;
using System.Data;
using System.Collections.Generic;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// 新质控
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class bizNewQC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_lngUpdateConcentration(clsLisConcentrationVO QCBatch)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_bse_lis_concentration
                           set concentration_vchr = ?, status_int = ?
                         where concentration_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(3, out parm);
                parm[++n].Value = QCBatch.m_strConcentration;
                parm[++n].Value = (int)QCBatch.m_enmStatus;
                parm[++n].Value = QCBatch.m_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertCheckMethod(clsLisCheckMethodVO p_objCheckMethod, out int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            p_intSeq = 0;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                svc.m_lngGenerateNewID("T_BSE_LIS_CHECKMETHOD", "METHOD_SEQ_INT", out p_intSeq);

                Sql = @"insert into t_bse_lis_checkmethod
                          (method_seq_int, checkmethod_name_vchr, pycode_vchr, wbcode_vchr)
                        values
                          (?, ?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(4, out parm);
                parm[++n].Value = p_intSeq;
                parm[++n].Value = p_objCheckMethod.m_strName;
                parm[++n].Value = p_objCheckMethod.m_strPycode;
                parm[++n].Value = p_objCheckMethod.m_strWbcode;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (ret > 0)
                {
                    p_objCheckMethod.m_intSeq = p_intSeq;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngUpdateCheckMethod(clsLisCheckMethodVO p_objCheckMethod)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_bse_lis_checkmethod
                           set checkmethod_name_vchr = ?, pycode_vchr = ?, wbcode_vchr = ?
                         where method_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(4, out parm);
                parm[++n].Value = p_objCheckMethod.m_strName;
                parm[++n].Value = p_objCheckMethod.m_strPycode;
                parm[++n].Value = p_objCheckMethod.m_strWbcode;
                parm[++n].Value = p_objCheckMethod.m_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertConcentration(clsLisConcentrationVO p_objConcentration, out int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            p_intSeq = 0;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                svc.m_lngGenerateNewID("T_BSE_LIS_CONCENTRATION", "CONCENTRATION_SEQ_INT", out p_intSeq);

                Sql = @"insert into t_bse_lis_concentration
                          (concentration_seq_int, concentration_vchr, status_int)
                        values
                          (?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(3, out parm);
                parm[++n].Value = p_intSeq;
                parm[++n].Value = p_objConcentration.m_strConcentration;
                parm[++n].Value = (int)p_objConcentration.m_enmStatus;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (ret > 0)
                {
                    p_objConcentration.m_intSeq = p_intSeq;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngDeleteCheckMethod(int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"delete t_bse_lis_checkmethod where method_seq_int = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = p_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertVendor(clsLisVendorVO p_objVendor, out int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            p_intSeq = 0;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                svc.m_lngGenerateNewID("T_BSE_LIS_VENDOR", "VENDOR_SEQ_INT", out p_intSeq);

                Sql = @"insert into t_bse_lis_vendor
                          (vendor_seq_int, vendor_vchr, vendor_id_vchr, pycode_vchr, wbcode_vchr)
                        values
                          (?, ?, ?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(5, out parm);
                parm[++n].Value = p_intSeq;
                parm[++n].Value = p_objVendor.m_strVendor;
                parm[++n].Value = p_objVendor.m_strId;
                parm[++n].Value = p_objVendor.m_strPycode;
                parm[++n].Value = p_objVendor.m_strWbcode;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (ret > 0)
                {
                    p_objVendor.m_intSeq = p_intSeq;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngUpdateVendor(clsLisVendorVO p_objVendor)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_bse_lis_vendor
                           set vendor_vchr    = ?,
                               vendor_id_vchr = ?,
                               pycode_vchr    = ?,
                               wbcode_vchr    = ?
                         where vendor_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(5, out parm);
                parm[++n].Value = p_objVendor.m_strVendor;
                parm[++n].Value = p_objVendor.m_strId;
                parm[++n].Value = p_objVendor.m_strPycode;
                parm[++n].Value = p_objVendor.m_strWbcode;
                parm[++n].Value = p_objVendor.m_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngUpdateWorkGruop(clsLisWorkGroupVO p_objWorkGroup)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_bse_lis_workgroup
                           set workgroup_name_vchr = ?, summary_vchr = ?, status_int = ?
                         where workgroup_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(4, out parm);
                parm[++n].Value = p_objWorkGroup.m_strName;
                parm[++n].Value = p_objWorkGroup.m_strSummary;
                parm[++n].Value = (int)p_objWorkGroup.m_enmStatus;
                parm[++n].Value = p_objWorkGroup.m_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertWorkGroup(clsLisWorkGroupVO p_objWorkGroup, out int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            p_intSeq = 0;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                svc.m_lngGenerateNewID("T_BSE_LIS_WORKGROUP", "WORKGROUP_SEQ_INT", out p_intSeq);

                Sql = @"insert into t_bse_lis_workgroup
                          (workgroup_seq_int, workgroup_name_vchr, summary_vchr, status_int)
                        values
                          (?, ?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(4, out parm);
                parm[++n].Value = p_intSeq;
                parm[++n].Value = p_objWorkGroup.m_strName;
                parm[++n].Value = p_objWorkGroup.m_strSummary;
                parm[++n].Value = (int)p_objWorkGroup.m_enmStatus;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (ret > 0)
                {
                    p_objWorkGroup.m_intSeq = p_intSeq;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngFindCheckMethod(out clsLisCheckMethodVO[] p_objResultArr)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_objResultArr = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select * from t_bse_lis_checkmethod";

                DataTable dt = null;
                ret = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsLisCheckMethodVO vo = null;
                    List<clsLisCheckMethodVO> lstData = new List<clsLisCheckMethodVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsLisCheckMethodVO();
                        vo.m_intSeq = DBAssist.ToInt32(dr["METHOD_SEQ_INT"]);
                        vo.m_strName = dr["CHECKMETHOD_NAME_VCHR"].ToString();
                        vo.m_strPycode = dr["PYCODE_VCHR"].ToString();
                        vo.m_strWbcode = dr["WBCODE_VCHR"].ToString();
                        lstData.Add(vo);
                    }
                    p_objResultArr = lstData.ToArray();
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngFindConcentration(out clsLisConcentrationVO[] p_objResultArr)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_objResultArr = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select * from t_bse_lis_concentration ";

                DataTable dt = null;
                ret = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsLisConcentrationVO vo = null;
                    List<clsLisConcentrationVO> lstData = new List<clsLisConcentrationVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsLisConcentrationVO();
                        vo.m_intSeq = DBAssist.ToInt32(dr["CONCENTRATION_SEQ_INT"]);
                        vo.m_strConcentration = dr["CONCENTRATION_VCHR"].ToString();
                        vo.m_enmStatus = DBAssist.ToInt32(dr["STATUS_INT"]) == 1 ? enmQCStatus.Natrural : enmQCStatus.Delete;
                        lstData.Add(vo);
                    }
                    p_objResultArr = lstData.ToArray();
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngFindVendor(out clsLisVendorVO[] p_objResultArr)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_objResultArr = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select * from t_bse_lis_vendor ";

                DataTable dt = null;
                ret = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsLisVendorVO vo = null;
                    List<clsLisVendorVO> lstData = new List<clsLisVendorVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsLisVendorVO();
                        vo.m_intSeq = (dr["VENDOR_SEQ_INT"] == DBNull.Value) ? 0 : int.Parse(dr["VENDOR_SEQ_INT"].ToString().Trim());
                        vo.m_strVendor = dr["VENDOR_VCHR"].ToString().Trim();
                        vo.m_strId = dr["VENDOR_ID_VCHR"].ToString().Trim();
                        vo.m_strPycode = dr["PYCODE_VCHR"].ToString().Trim();
                        vo.m_strWbcode = dr["WBCODE_VCHR"].ToString().Trim();
                        lstData.Add(vo);
                    }
                    p_objResultArr = lstData.ToArray();
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngFindWorkGroup(out clsLisWorkGroupVO[] p_objResultArr)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_objResultArr = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select * from t_bse_lis_workgroup";

                DataTable dt = null;
                ret = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsLisWorkGroupVO vo = null;
                    List<clsLisWorkGroupVO> lstData = new List<clsLisWorkGroupVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsLisWorkGroupVO();
                        vo.m_intSeq = (dr["WORKGROUP_SEQ_INT"] == DBNull.Value) ? 0 : int.Parse(dr["WORKGROUP_SEQ_INT"].ToString().Trim());
                        vo.m_strName = dr["WORKGROUP_NAME_VCHR"].ToString().Trim();
                        vo.m_strSummary = dr["SUMMARY_VCHR"].ToString().Trim();
                        vo.m_enmStatus = DBAssist.ToInt32(dr["STATUS_INT"]) == 1 ? enmQCStatus.Natrural : enmQCStatus.Delete;
                        lstData.Add(vo);
                    }
                    p_objResultArr = lstData.ToArray();
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertQCConcentration(clsLisQCConcentrationVO p_objQCConcentration)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"insert into t_opr_lis_qcbatchconcentration
                          (qcbatch_seq_int,
                           concentration_seq_int,
                           devicesample_id_vchr,
                           status_int,
                           avg_num,
                           sd_num,
                           cv_num)
                        values
                          (?, ?, ?, ?, ?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(7, out parm);
                parm[++n].Value = p_objQCConcentration.m_intQCBatchSeq;
                parm[++n].Value = p_objQCConcentration.m_intConcentrationSeq;
                parm[++n].Value = p_objQCConcentration.m_strDeviceSampleId;
                parm[++n].Value = (int)p_objQCConcentration.m_enmStatus;
                parm[++n].Value = p_objQCConcentration.m_dblAVG;
                parm[++n].Value = p_objQCConcentration.m_dblSD;
                parm[++n].Value = p_objQCConcentration.m_dblCV;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngUpdateQCConcentration(clsLisQCConcentrationVO p_objQCConcentration)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_opr_lis_qcbatchconcentration
                           set devicesample_id_vchr = ?,
                               status_int           = ?,
                               avg_num              = ?,
                               sd_num               = ?,
                               cv_num               = ?
                         where qcbatch_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(6, out parm);
                parm[++n].Value = p_objQCConcentration.m_strDeviceSampleId;
                parm[++n].Value = (int)p_objQCConcentration.m_enmStatus;
                parm[++n].Value = p_objQCConcentration.m_dblAVG;
                parm[++n].Value = p_objQCConcentration.m_dblSD;
                parm[++n].Value = p_objQCConcentration.m_dblCV;
                parm[++n].Value = p_objQCConcentration.m_intQCBatchSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngFindDelQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_objResultArr = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select t1.*, t2.concentration_vchr
                          from t_opr_lis_qcbatchconcentration t1, t_bse_lis_concentration t2
                         where t1.concentration_seq_int = t2.concentration_seq_int
                           and t1.qcbatch_seq_int = ?
                           and t1.status_int = 0";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = p_intQCBatchSeq;

                DataTable dt = null;
                ret = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsLisQCConcentrationVO vo = null;
                    List<clsLisQCConcentrationVO> lstData = new List<clsLisQCConcentrationVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsLisQCConcentrationVO();
                        vo.m_intConcentrationSeq = DBAssist.ToInt32(dr["CONCENTRATION_SEQ_INT"]);
                        vo.m_intQCBatchSeq = DBAssist.ToInt32(dr["QCBATCH_SEQ_INT"]);
                        vo.m_strDeviceSampleId = dr["DEVICESAMPLE_ID_VCHR"].ToString();
                        try
                        {
                            vo.m_enmStatus = DBAssist.ToInt32(dr["STATUS_INT"]) == 1 ? enmQCStatus.Natrural : enmQCStatus.Delete;
                        }
                        catch { }
                        vo.m_dblAVG = DBAssist.ToDouble(dr["AVG_NUM"]);
                        vo.m_dblSD = DBAssist.ToDouble(dr["SD_NUM"]);
                        vo.m_dblCV = DBAssist.ToDouble(dr["CV_NUM"]);
                        vo.m_strConcentration = dr["concentration_vchr"].ToString();
                        lstData.Add(vo);
                    }
                    if (lstData.Count > 0)
                        p_objResultArr = lstData.ToArray();
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngQCCheckItem(string p_strDeviceId, out DataTable p_dtResult)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_dtResult = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select t.device_check_item_id_chr, t.device_check_item_name_vchr
                          from t_bse_lis_device_check_item t
                         inner join t_bse_lis_device a
                            on t.device_model_id_chr = a.device_model_id_chr
                         where t.is_qc_item_int = 1
                           and a.deviceid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = p_strDeviceId;

                ret = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngQueryQCLot(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            long ret = 0;
            string Sql = string.Empty;
            p_dtResult = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select t.qcsample_lotno_vchr
                          from t_opr_lis_qcbatch t
                         where t.deviceid_chr = ?
                           and t.begin_dat <= ?
                           and t.end_dat >= ?
                         group by t.qcsample_lotno_vchr";

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = objSch.m_strQCDevice;
                parm[1].Value = objSch.m_datQueryEnd;
                parm[2].Value = objSch.m_datQueryBegin;

                ret = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm); 
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngDeleteQCRule(int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"delete t_bse_lis_qcrules where rule_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(1, out parm);
                parm[++n].Value = p_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngInsertQCRule(clsLisQCRuleVO p_objRule, out int p_intSeq)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            p_intSeq = 0;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                svc.m_lngGenerateNewID("T_BSE_LIS_QCRULES", "RULE_SEQ_INT", out p_intSeq);

                Sql = @"insert into t_bse_lis_qcrules
                          (rule_seq_int,
                           rule_name_vchr,
                           rule_alias_vchr,
                           rule_desc_vchr,
                           rule_formula_vchr,
                           rule_summary_vchr,
                           rule_defaultflag_int,
                           rule_typeflag_int)
                        values
                          (?, ?, ?, ?, ?, ?, ?, ?)";

                int n = -1;
                svc.CreateDatabaseParameter(8, out parm);
                parm[++n].Value = p_intSeq;
                parm[++n].Value = p_objRule.m_strName;
                parm[++n].Value = p_objRule.m_strAlias;
                parm[++n].Value = p_objRule.m_strDesc;
                parm[++n].Value = p_objRule.m_strFormula;
                parm[++n].Value = p_objRule.m_strSummary;
                parm[++n].Value = (int)p_objRule.m_enmDefaultflag;
                parm[++n].Value = (int)p_objRule.m_enmWarnType;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (ret > 0)
                {
                    p_objRule.m_intSeq = p_intSeq;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        [AutoComplete]
        public long m_lngUpdateQCRule(clsLisQCRuleVO p_objRule)
        {
            long ret = 0;
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_bse_lis_qcrules
                           set rule_name_vchr       = ?,
                               rule_alias_vchr      = ?,
                               rule_desc_vchr       = ?,
                               rule_formula_vchr    = ?,
                               rule_summary_vchr    = ?,
                               rule_defaultflag_int = ?,
                               rule_typeflag_int    = ?
                         where rule_seq_int = ?";

                int n = -1;
                svc.CreateDatabaseParameter(8, out parm);
                parm[++n].Value = p_objRule.m_strName;
                parm[++n].Value = p_objRule.m_strAlias;
                parm[++n].Value = p_objRule.m_strDesc;
                parm[++n].Value = p_objRule.m_strFormula;
                parm[++n].Value = p_objRule.m_strSummary;
                parm[++n].Value = (int)p_objRule.m_enmDefaultflag;
                parm[++n].Value = (int)p_objRule.m_enmWarnType;
                parm[++n].Value = p_objRule.m_intSeq;

                ret = svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
            }
            catch (Exception ex)
            {
                ret = -1;
                new clsLogText().LogError(ex);
            }
            return ret;
        }

        #region m_lngQueryDeviceInfo
        [AutoComplete]
        public long m_lngQueryDeviceInfo(string p_strComputerName, out DataTable p_dtResult)
        {
            long rec = 0;
            string Sql = string.Empty;
            p_dtResult = null;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            Sql = @"select d.deviceid_chr,
                           d.devicename_vchr,
                           m.device_model_desc_vchr,
                           s.comno_chr,
                           s.baulrate_chr,
                           s.databit_chr,
                           s.stopbit_chr,
                           s.parity_chr,
                           s.flowcontrol_chr,
                           s.receivebuffer_chr,
                           s.sendbuffer_chr,
                           s.sendcommand_chr,
                           s.sendcommandinternal_chr,
                           s.dataanalysisdll_vchr,
                           s.dataanalysisnamespace_vchr,
                           d.devicename_vchr,
                           d.dataacquisitioncomputerip_chr,
                           d.device_code_chr
                      from t_bse_lis_serialsetup s,
                           t_bse_lis_device       d,
                           t_bse_lis_device_model m
                     where d.device_model_id_chr = s.device_model_id_chr
                       and d.device_model_id_chr = m.device_model_id_chr
                       and d.end_date_dat is null
                       and lower(d.dataacquisitioncomputerip_chr) = lower(?)";

            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = p_strComputerName;
            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);
            return rec;
        }
        #endregion

        #region m_lngQueryQCDeviceResult
        [AutoComplete]
        public long m_lngQueryQCDeviceResult(int p_strQCId, out List<double> lstDbl)
        {
            long rec = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            lstDbl = null;

            Sql = @"select t.result_num from t_opr_lis_qcdata t where t.qcbatch_seq_int = ?";

            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = p_strQCId;
            rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                lstDbl = new List<double>();
                foreach (DataRow dr in dt.Rows)
                {
                    lstDbl.Add(dr["result_num"] == DBNull.Value ? 0 : Convert.ToDouble(dr["result_num"]));
                }
            }
            return rec;
        }
        #endregion

        #region m_lngQueryQCInfo
        [AutoComplete]
        public long m_lngQueryQCInfo(int p_intQCID, out List<clsLisQCConcentrationVO> p_lstQCConTemp)
        {
            long rec = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            p_lstQCConTemp = null;

            Sql = @"select t.avg_num, t.sd_num from t_opr_lis_qcbatchconcentration t where t.qcbatch_seq_int = ?";

            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = p_intQCID;
            rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_lstQCConTemp = new List<clsLisQCConcentrationVO>();
                foreach (DataRow dr in dt.Rows)
                {
                    p_lstQCConTemp.Add(new clsLisQCConcentrationVO() { m_dblAVG = Convert.ToDouble(dr["avg_num"]), m_dblSD = Convert.ToDouble(dr["sd_num"]) });
                }
            }
            return rec;
        }
        #endregion

        #region m_lngGetAllCheckItemInfo
        [AutoComplete]
        public long m_lngGetAllCheckItemInfo(out DataTable p_dtResult)
        {
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            p_dtResult = null;

            Sql = @"select t.deviceid_chr, t.devicename_vchr, t.device_code_chr
                      from t_bse_lis_device t
                     where (end_date_dat is null or end_date_dat > sysdate)
                       and begin_date_dat <= sysdate";

            rec = svc.lngGetDataTableWithoutParameters(Sql, ref p_dtResult);
            return rec;
        }
        #endregion

        #region m_lngQueryItemQCResult
        [AutoComplete]
        public long m_lngQueryItemQCResult(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            p_dtResult = null;

            Sql = @"select t.result_num, t.qcdate_dat from t_opr_lis_qcdata t where t.qcbatch_seq_int = ? order by t.qcdate_dat asc";

            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = objSch.m_intQCBatchSeq;
            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);
            return rec;
        }
        #endregion 

        #region m_lngQueryResult
        [AutoComplete]
        public long m_lngQueryResult(clsLisQCBatchSchVO objSch, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, out DataTable p_dtQcResult)
        {
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            p_dtResult = null;
            p_dtQcResult = null;

            Sql = @"select t.check_item_id_chr,
                           t1.data_seq_int,
                           t1.qcbatch_seq_int,
                           t1.result_num,
                           t1.qcdate_dat,
                           t1.data_seq_int,
                           t2.device_check_item_name_vchr,
                           t5.avg_num,
                           t5.cv_num,
                           t5.sd_num,
                           t5.devicesample_id_vchr,
                           t5.concentration_seq_int
                      from t_opr_lis_qcbatch              t,
                           t_opr_lis_qcdata               t1,
                           t_bse_lis_device_check_item    t2,
                           t_bse_lis_device               t4,
                           t_opr_lis_qcbatchconcentration t5
                     where t.qcbatch_seq_int = t1.qcbatch_seq_int
                       and t.deviceid_chr = t4.deviceid_chr
                       and t4.device_model_id_chr = t2.device_model_id_chr
                       and t.check_item_id_chr = t2.device_check_item_id_chr
                       and t.qcbatch_seq_int = t5.qcbatch_seq_int
                       and t5.status_int = 1
                       and t.deviceid_chr = ?
                       and t.qcsample_lotno_vchr = ?
                       and t1.qcdate_dat >= ?
                       and t1.qcdate_dat < ?";

            svc.CreateDatabaseParameter(4, out parm);
            parm[0].Value = objSch.m_strQCDevice;
            parm[1].Value = objSch.m_strQCSampleLotNO;
            parm[2].Value = DateTime.Parse(p_strStartDate);
            parm[3].Value = DateTime.Parse(p_strEndDate);
            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);

            Sql = @"select t.check_item_id_chr,
                           t5.qcbatch_seq_int,
                           t2.device_check_item_name_vchr,
                           t5.avg_num,
                           t5.cv_num,
                           t5.sd_num,
                           t5.devicesample_id_vchr,
                           t5.concentration_seq_int
                      from t_opr_lis_qcbatch              t,
                           t_bse_lis_device_check_item    t2,
                           t_bse_lis_device               t4,
                           t_opr_lis_qcbatchconcentration t5
                     where t.deviceid_chr = t4.deviceid_chr
                       and t4.device_model_id_chr = t2.device_model_id_chr
                       and t.check_item_id_chr = t2.device_check_item_id_chr
                       and t.qcbatch_seq_int = t5.qcbatch_seq_int
                       and t5.status_int = 1
                       and t.deviceid_chr = ?
                       and t.qcsample_lotno_vchr = ?
                       and t.end_dat >= ?
                       and t.begin_dat <= ?";

            svc.CreateDatabaseParameter(4, out parm);
            parm[0].Value = objSch.m_strQCDevice;
            parm[1].Value = objSch.m_strQCSampleLotNO;
            parm[2].Value = objSch.m_datQueryBegin;
            parm[3].Value = objSch.m_datQueryEnd;
            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtQcResult, parm);

            return rec;
        }
        #endregion

    }
}
