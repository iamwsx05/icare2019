using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using System.Collections;

namespace com.digitalwave.iCare.middletier.BIHOrderServer
{
    /// <summary>
    /// clsBIHChargeItemService ��ժҪ˵����
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBIHOrderBedManager : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ѯĳ������ĳ״̬������Ϣ
        /// <summary>
        /// ��ѯĳ������ĳ״̬������Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="p_strStatus">����״̬(Ϊ������Ϊ��ѯ������������ö��ŷָ�����: ��1,2,3��) {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <param name="p_objResultArr"></param>
        /// <param name="seach"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByAreaID(string p_strAreaid_chr, string p_strStatus, out clsT_Bse_Bed_VO[] p_objResultArr, bool seach)
        {

            p_objResultArr = new clsT_Bse_Bed_VO[0];
            long lngRes = 0;
            string strSQL = "";

            strSQL = @"
                    SELECT    a.bedid_chr,a.code_chr,a.areaid_chr,b.lastname_vchr,b.sex_chr     
                    FROM t_bse_bed a,t_opr_bih_registerdetail b,t_opr_bih_register c
                    where a.bihregisterid_chr = c.registerid_chr
                    and b.registerid_chr = c.registerid_chr
                    and a.status_int = 2
                    and c.pstatus_int= 1 and c.areaid_chr='[areaid_chr]' AND a.status_int = [p_strStatus]
                    order by a.bedid_chr";
            strSQL = strSQL.Replace("[areaid_chr]", p_strAreaid_chr.Trim());
            strSQL = strSQL.Replace("[p_strStatus]", p_strStatus.Trim());

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bse_Bed_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bse_Bed_VO();
                        p_objResultArr[i1].m_strBEDID_CHR = dtbResult.Rows[i1]["BEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCODE_CHR = dtbResult.Rows[i1]["CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPatientSex = dtbResult.Rows[i1]["sex_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim();
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
    }
}
