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
    /// ��������
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-09-02
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBedManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        public clsBedManageSvc()
        {
        }

        //��λ�༭
        #region �޸Ĵ�λ��Ϣ
        #region ������ˮ�š����޸Ĳ���״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// <summary>
        /// ������ˮ�š����޸Ĳ���״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedid_chr">��ˮ��</param>
        /// <param name="p_intStatus_int">����״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedByBedID(string p_strBedid_chr, int p_intStatus_int)
        {
            long lngRes = 0;
            string strSQLUpdate = "";
            if (p_intStatus_int == 1)
            {
                strSQLUpdate = @"      
UPDATE t_bse_bed
   SET status_int = 1,BIHREGISTERID_CHR = null
 WHERE bedid_chr = '" + p_strBedid_chr.Trim() + "'";
            }
            else
            {
                strSQLUpdate = "UPDATE  T_BSE_BED SET ";
                strSQLUpdate += " STATUS_INT =" + p_intStatus_int.ToString();
                strSQLUpdate += " WHERE bedid_chr = '" + p_strBedid_chr.Trim() + "'";
            }
            return m_lngModifyBedInfo( strSQLUpdate);
        }
        #endregion
        #region ���ݲ����š����š����޸Ĳ���״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// <summary>
        /// ���ݲ����š����š����޸Ĳ���״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID_chr">������</param>
        /// <param name="p_strCode_chr">����</param>
        /// <param name="p_intStatus_int">����״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedByAreaIDCode(string p_strAreaID_chr, string p_strCode_chr, int p_intStatus_int)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE  T_BSE_BED SET ";
            strSQLUpdate += " STATUS_INT =" + p_intStatus_int.ToString();
            strSQLUpdate += " WHERE areaid_chr = '" + p_strAreaID_chr.Trim() + "' AND code_chr = '" + p_strCode_chr.Trim() + "'";

            return m_lngModifyBedInfo(strSQLUpdate);
        }
        #endregion
        #region �޸Ĳ�����Ϣ
        /// <summary>
        /// �޸Ĳ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBEDID_CHR">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedInfoByVo(string p_strBEDID_CHR, clsT_Bse_Bed_VO p_objRecord)
        {
            long lngRes = 0;
            string strSQLUpdate = "UPDATE T_BSE_BED SET ";
            strSQLUpdate += " AREAID_CHR = '" + p_objRecord.m_strAREAID_CHR + "'";
            strSQLUpdate += " , CODE_CHR = '" + p_objRecord.m_strCODE_CHR + "'";
            strSQLUpdate += " , STATUS_INT = " + p_objRecord.m_intSTATUS_INT;
            strSQLUpdate += " , RATE_MNY = " + p_objRecord.m_dblRATE_MNY;
            strSQLUpdate += " , SEX_INT = " + p_objRecord.m_intSEX_INT;
            strSQLUpdate += " , CATEGORY_INT = " + p_objRecord.m_intCATEGORY_INT;
            strSQLUpdate += " , AIRRATE_MNY = " + p_objRecord.m_dblAIRRATE_MNY;
            strSQLUpdate += " , CHARGEITEMID_CHR = '" + p_objRecord.m_strCHARGEITEMID_CHR + "'";
            strSQLUpdate += " , AIRCHARGEITEMID_CHR = '" + p_objRecord.m_str_AIRCHARGEITEMID_CHR + "'";
            strSQLUpdate += " WHERE BEDID_CHR = '" + p_strBEDID_CHR.Trim() + "'";

            return m_lngModifyBedInfo(strSQLUpdate);
        }
        #endregion
        #endregion
        #region ɾ����λ
        #region ɾ�����Ŵ�λ {1.ԭ����ֻ��ɾ���մ�,���Ǵ˷�����û���ж��Ƿ�մ�. 2.�˴�����ɾ��,���Ǳ�־��ɾ��[��־].}
        /// <summary>
        /// ���ݴ���ɾ����λ
        /// {1.ԭ����ֻ��ɾ���մ�,���Ǵ˷�����û���ж��Ƿ�մ�. 2.�˴�����ɾ��,���Ǳ�־��ɾ��[��־].}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCode_chr">����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteBedInfoByByCode_chr(string p_strCode_chr)
        {
            string strSQLDelete = "DELETE FROM  T_BSE_BED ";
            strSQLDelete += "WHERE Code_chr ='" + p_strCode_chr.Trim() + "'";

            return m_lngDeleteBedInfo(strSQLDelete);
        }
        #endregion
        #region ɾ��һ�������Ĵ�λ
        /// <summary>
        /// ɾ��һ�������Ĵ�λ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����id</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteBedInfoByByAreaid_chr(string p_strAreaid_chr)
        {
            string strSQLDelete = "DELETE FROM  T_BSE_BED ";
            strSQLDelete += " Areaid_chr ='" + p_strAreaid_chr.Trim() + "'";

            return m_lngDeleteBedInfo(strSQLDelete);
        }
        #endregion
        #endregion

        //��λ��ѯ��ͳ��
        #region ��ѯ���еĲ�����Ϣ
        /// <summary>
        /// ��ѯ���еĲ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllBedInfo(out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            return m_lngGetBedInfo("", out p_objResultArr);
        }
        #endregion
        #region ��ѯĳ�����ŵ����в�����Ϣ
        /// <summary>
        /// ��ѯĳ�����ŵ����в�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByAreaID(string p_strAreaid_chr, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            string strQueryCondition = " Trim(areaid_chr) = '" + p_strAreaid_chr.Trim() + "' and status_int IN (2, 3, 4)  Order By code_chr";
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strQueryCondition = " LTrim(RTRIM(areaid_chr)) = '" + p_strAreaid_chr.Trim() + "' Order By code_chr";
            }
            /* <<======================================= */
            return m_lngGetBedInfo(strQueryCondition, out p_objResultArr);
        }
        /// <summary>
        /// ��ѯĳ������ĳ״̬������Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="p_strStatus">����״̬(Ϊ������Ϊ��ѯ������������ö��ŷָ�����: ��1,2,3��) {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByAreaID(string p_strAreaid_chr, string p_strStatus, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            string strQueryCondition = " Trim(areaid_chr) = '" + p_strAreaid_chr.Trim() + "' ";
            if (p_strStatus.Trim() != "") strQueryCondition += " AND status_int in (" + p_strStatus.Trim() + ") ";
            strQueryCondition += " Order By code_chr"; ;
            return m_lngGetBedInfo(strQueryCondition, out p_objResultArr);
        }
        #endregion
        #region ��ѯĳ������ˮ�ŵĲ�����Ϣ
        /// <summary>
        /// ��ѯĳ������ˮ�ŵĲ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBedid_chr">������ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetBedInfoByBedID(string p_strBedid_chr, out clsT_Bse_Bed_VO p_objResult)
        //{
        //    p_objResult = new clsT_Bse_Bed_VO();

        //    clsT_Bse_Bed_VO[] p_objResultArr;
        //    string strQueryCondition =" a.bedid_chr = '" + p_strBedid_chr.Trim() + "'";
        //    long lngReturn = m_lngGetBedInfo(strQueryCondition,out p_objResultArr);
        //    if(lngReturn>0 && p_objResultArr.Length>0)
        //        p_objResult =p_objResultArr[0];
        //    return lngReturn;			
        //}
        #endregion

        #region ��ѯĳ������ĳ״̬������Ϣ--�Ż�(06-4-3)
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

            strSQL = @"SELECT    a.bedid_chr,a.code_chr,a.areaid_chr,b.lastname_vchr,b.sex_chr     
                    FROM t_bse_bed a,t_bse_patient b,t_opr_bih_register c
                    where a.bedid_chr=c.bedid_chr and b.patientid_chr=c.patientid_chr
                    and c.pstatus_int=1 and c.areaid_chr='[areaid_chr]' AND a.status_int = [p_strStatus]
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

        #region ��ѯĳ�����š����ŵĲ�����Ϣ
        /// <summary>
        /// ��ѯĳ�����š����ŵĲ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID_chr">������</param>
        /// <param name="p_strCode_chr">����</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByAreaIDAndCode(string p_strAreaID_chr, string p_strCode_chr, out clsT_Bse_Bed_VO p_objResult)
        {
            p_objResult = new clsT_Bse_Bed_VO();

            clsT_Bse_Bed_VO[] p_objResultArr;
            string strQueryCondition = " areaid_chr = '" + p_strAreaID_chr.Trim() + "' AND LOWER(trim(code_chr)) = '" + p_strCode_chr.ToLower().Trim() + "'";
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strQueryCondition = " areaid_chr = '" + p_strAreaID_chr.Trim() + "' AND LOWER(Ltrim(RTRIM(code_chr))) = '" + p_strCode_chr.ToLower().Trim() + "'";
            }
            /* <<======================================= */
            long lngReturn = m_lngGetBedInfo( strQueryCondition, out p_objResultArr);
            if (lngReturn > 0 && p_objResultArr.Length > 0)
                p_objResult = p_objResultArr[0];
            return lngReturn;
        }
        #endregion
        #region ���ݲ���״̬��ѯ������Ϣ
        #region  �� �մ�
        /// <summary>
        /// �� �մ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByStatus_1(out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            return m_lngGetBedInfoByStatus_int( 1, out p_objResultArr);
        }
        #endregion

        #region  �� ռ��
        /// <summary>
        /// �� ռ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByStatus_2(out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            return m_lngGetBedInfoByStatus_int( 2, out p_objResultArr);
        }
        #endregion

        #region  �� ԤԼռ��
        /// <summary>
        /// �� ԤԼռ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByStatus_3(out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            return m_lngGetBedInfoByStatus_int( 3, out p_objResultArr);
        }
        #endregion

        #region  �� ����ռ��
        /// <summary>
        /// �� ����ռ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByStatus_4(out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            return m_lngGetBedInfoByStatus_int( 4, out p_objResultArr);
        }
        #endregion

        #region ���ղ���״̬��
        /// <summary>
        /// ���ݲ���״̬��ѯ������Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus_int">����״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByStatus_int(int p_intStatus_int, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            string strQueryCondition = " status_int = " + p_intStatus_int.ToString();
            return m_lngGetBedInfo( strQueryCondition, out p_objResultArr);
        }
        #endregion
        #endregion
        #region ���ݴ�λ�Ѳ�ѯ������Ϣ
        /// <summary>
        /// ���ݴ�λ�Ѳ�ѯ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dblRate_mny">��λ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoByRate_mny(Double p_dblRate_mny, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            string strQueryCondition = " rate_mny = " + p_dblRate_mny.ToString();
            return m_lngGetBedInfo( strQueryCondition, out p_objResultArr);
        }
        #endregion
        #region �����Ա��ѯ������Ϣ
        /// <summary>
        /// �����Ա��ѯ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Sex_int">��Ů�� {1=��;2=Ů;3=����}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfoBySex_int(int p_Sex_int, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            string strQueryCondition = " sex_int = " + p_Sex_int.ToString();
            return m_lngGetBedInfo( strQueryCondition, out p_objResultArr);
        }
        #endregion
        #region ͳ��ĳ�����Ĵ�λ����
        /// <summary>
        /// ͳ��ĳ�����Ĵ�λ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="intNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatBedNumberByAreaID(string p_strAreaid_chr, out int intNumber)
        {
            intNumber = 0;
            long lngRes = 0;

            string strSQL = @"SELECT count(*) FROM t_bse_bed Where areaid_chr = '" + p_strAreaid_chr.Trim() + "'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    intNumber = Int32.Parse(dtbResult.Rows[0][0].ToString());
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
        #region ͳ��ĳ�����Ŀմ�λ����
        /// <summary>
        /// ͳ��ĳ�����Ŀմ�λ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="intNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatEmptyBedNumberByAreaID(string p_strAreaid_chr, out int intNumber)
        {
            intNumber = 0;
            long lngRes = 0;

            //status_int {1���մ���2��ռ����3��ԤԼռ����4����ռ����}
            string strSQL = @"SELECT count(*) FROM t_bse_bed Where areaid_chr = '" + p_strAreaid_chr.Trim() + "' and status_int = 1";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    intNumber = Int32.Parse(dtbResult.Rows[0][0].ToString());
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
        #region ͳ��ĳ�����Ŀմ�λ���� [�����Ա�����]
        /// <summary>
        /// ͳ��ĳ�����Ŀմ�λ���� [�����Ա�����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="intSEX_INT">�Ա�����	[1���У�2��Ů��3�����ޣ�]</param>
        /// <param name="intNumber">[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStatEmptyBedNumberByAreaIDSex(string p_strAreaid_chr, int intSEX_INT, out int intNumber)
        {
            intNumber = 0;
            long lngRes = 0;

            //status_int {1���մ���2��ռ����3��ԤԼռ����4����ռ����}
            string strSQL = @"SELECT count(*) FROM t_bse_bed Where areaid_chr = '" + p_strAreaid_chr.Trim() + "' and status_int = 1 and SEX_INT=" + intSEX_INT.ToString();

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    intNumber = Int32.Parse(dtbResult.Rows[0][0].ToString());
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


        //������ѯ��ͳ��
        #region ͳ��סԺ����[��Ч��]����
        /// <summary>
        /// ͳ��סԺ����[��Ч��]����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">DataTable [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptInfo(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"SELECT DEPTID_CHR, code_vchr, DEPTNAME_VCHR, SHORTNO_CHR,PYCODE_CHR ";
            strSQL += " FROM t_bse_deptdesc WHERE status_int=1 Order By DEPTNAME_VCHR ";

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
        #region ��ò���
        [AutoComplete]
        public long m_lngGetsickArea(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            //            string strSQL = @"SELECT   deptid_chr, MAX (deptname_vchr) AS deptname_vchr,
            //                              MAX (shortno_chr) AS shortno_chr, MAX (pycode_chr) AS pycode_chr,
            //                              COUNT (t_bse_bed.bedid_chr) as BedCount
            //                              FROM t_bse_deptdesc, t_bse_bed
            //                              WHERE t_bse_deptdesc.status_int = 1
            //                              AND attributeid = '0000003'
            //                              AND t_bse_bed.areaid_chr = t_bse_deptdesc.deptid_chr
            //                              AND t_bse_bed.category_int = 1
            //                              GROUP BY t_bse_deptdesc.deptid_chr
            //                              ORDER BY deptname_vchr";

            string strSQL = @"SELECT a.deptid_chr,
                                   MAX(a.deptname_vchr) AS deptname_vchr,
                                   MAX(a.shortno_chr) AS shortno_chr,
                                   MAX(a.pycode_chr) AS pycode_chr,
                                   COUNT(b.bedid_chr) as BedCount
                              FROM t_bse_deptdesc a, t_bse_bed b
                             WHERE a.status_int = 1
                               AND a.attributeid = '0000003'
                               AND b.areaid_chr = a.deptid_chr
                               AND b.category_int in (1,2)
                               AND b.status_int <> 5
                             GROUP BY a.deptid_chr";
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
        #region ͳ��ĳ���ҵ�����[��Ч��]����
        /// <summary>
        /// ͳ��ĳ���ҵ�����[��Ч��]����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">����ID</param>
        /// <param name="p_dtbResult">DataTable [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaInfo(string p_strDeptID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"SELECT DEPTID_CHR,DEPTNAME_VCHR,SHORTNO_CHR,PYCODE_CHR ";
            strSQL += " FROM t_bse_deptdesc WHERE status_int=1 AND attributeid='0000003' AND PARENTID='" + p_strDeptID.Trim() + "' Order By DEPTNAME_VCHR";
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
        #region ��ѯĳ�����ŵ�����ĳ״̬�Ĳ�����Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// <summary>
        /// ��ѯĳ�����ŵ�����ĳ״̬�Ĳ�����Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="p_intStatus_int">����״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <param name="p_dtbResult">[DataTable out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaBedInfoByStatus_int(string p_strAreaid_chr, int p_intStatus_int, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"SELECT BEDID_CHR,CODE_CHR,DECODE(SEX_INT, 1, '��', 2, 'Ů','����') SexType,RATE_MNY,AIRRATE_MNY ";

            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT BEDID_CHR,CODE_CHR,(CASE SEX_INT WHEN 1 THEN  '��' WHEN  2 THEN  'Ů' ELSE '����' END) SexType,RATE_MNY,AIRRATE_MNY ";
            }
            /* <<======================================= */
            strSQL += " FROM t_bse_bed Where areaid_chr = '" + p_strAreaid_chr.Trim() + "' AND status_int = " + p_intStatus_int.ToString() + " Order By code_chr";
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
        /// <summary>
        /// ��ѯĳ�����ŵ�����ĳ״̬�Ĳ�����Ϣ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaid_chr">����ID</param>
        /// <param name="p_intStatus_int">����״̬ {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}</param>
        /// <param name="p_dtbResult">[DataTable out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaBedInfoByStatus_int2(string p_strAreaid_chr, int p_intStatus_int, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"SELECT BEDID_CHR,CODE_CHR,DECODE(SEX_INT, 1, '��', 2, 'Ů','����') SexType,RATE_MNY,AIRRATE_MNY ";
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT BEDID_CHR,CODE_CHR,(CASE SEX_INT WHEN 1 THEN  '��' WHEN  2 THEN  'Ů' ELSE '����' END) SexType,RATE_MNY,AIRRATE_MNY ";
            }
            /* <<======================================= */
            strSQL += " FROM t_bse_bed Where areaid_chr = '" + p_strAreaid_chr.Trim() + "' AND status_int = " + p_intStatus_int.ToString() + " Order By code_chr";
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
        #region ��ȡ��������
        /// <summary>
        /// ��ѯĳ����ID��Ӧ�Ĳ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">����ID</param>
        /// <param name="p_strName">[�������� out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeptIDToName(string p_strID, out string p_strName)
        {
            long lngRes = 0;
            p_strName = "";

            string strSQL = @"SELECT DEPTNAME_VCHR FROM t_bse_deptdesc WHERE  DEPTID_CHR ='" + p_strID.Trim() + "'";
            try
            {
                DataTable p_dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtbResult.Rows.Count > 0)
                {
                    p_strName = p_dtbResult.Rows[0]["DEPTNAME_VCHR"].ToString();
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
        #region ��ȡ����
        /// <summary>
        /// ��ȡ����	���ݴ�λ��ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��λ��ˮ��</param>
        /// <param name="p_strName">[���� out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBedIDToBedNo(string p_strID, out string p_strName)
        {
            long lngRes = 0;
            p_strName = "";

            string strSQL = @"SELECT code_chr  FROM t_bse_bed WHERE trim(bedid_chr)='" + p_strID.Trim() + "'";
            try
            {
                DataTable p_dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtbResult.Rows.Count > 0)
                {
                    p_strName = p_dtbResult.Rows[0]["code_chr"].ToString();
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
        #region ��ȡ����[��Ч��]����
        /// <summary>
        /// ��ȡ����[��Ч��]����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">DataTable [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllAreaInfo(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            string strSQL = @"SELECT DEPTID_CHR,DEPTNAME_VCHR,SHORTNO_CHR,PYCODE_CHR ";
            strSQL += " FROM t_bse_deptdesc WHERE status_int=1 AND attributeid='0000003' Order By DEPTNAME_VCHR";
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

        #region ��ѯĳ����ĳ����ĳ��λ���һ��סԺ�Ļ�����Ϣ
        /// <summary>
        /// ��ѯĳ����ĳ����ĳ��λ���һ��סԺ�Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">���ſ��Һ�</param>
        /// <param name="p_strAreaID">����-��ˮ��</param>
        /// <param name="p_strBedID">��λ��ˮ��</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByDeptIDAreaIDBedID(string p_strDeptID, string p_strAreaID, string p_strBedID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            //��ѯ������  ȷ����ѯSQL����ִ�С�				
            string strSQL = @"SELECT *
  FROM (SELECT   a.registerid_chr, a.modify_dat, a.patientid_chr,
                 a.inpatientid_chr, a.inpatient_dat, a.deptid_chr,
                 a.areaid_chr, a.bedid_chr, a.type_int, a.diagnose_vchr,
                 a.inpatientcount_int, a.state_int, a.status_int,
                 a.pstatus_int, b.lastname_vchr, b.idcard_chr, b.married_chr,
                 b.birthplace_vchr, b.homeaddress_vchr, b.sex_chr,
                 b.nationality_vchr, b.firstname_vchr, b.birth_dat,
                 b.race_vchr, b.nativeplace_vchr, b.occupation_vchr,
                 b.name_vchr, b.homephone_vchr, b.officephone_vchr,
                 b.insuranceid_vchr, b.mobile_chr, b.officeaddress_vchr,
                 b.employer_vchr, b.officepc_vchr, b.homepc_chr,
                 b.paytypeid_chr,
                 (SELECT paytypename_vchr
                    FROM t_bse_patientpaytype
                   WHERE t_bse_patientpaytype.paytypeid_chr =
                                          b.paytypeid_chr)
                                                          AS paytypename_vchr,
                 b.optimes_int, b.deactivate_dat, b.isemployee_int,
                 b.firstdate_dat, b.patientrelation_vchr,
                 b.contactpersonpc_chr, b.contactpersonphone_vchr,
                 b.contactpersonaddress_vchr, b.contactpersonlastname_vchr,
                 b.contactpersonfirstname_vchr, b.email_vchr,
                 a.icd10diagtext_vchr, e.LASTNAME_VCHR as mainDoc,a.CASEDOCTOR_CHR,a.ICD10DIAGID_VCHR,a.DIAGNOSEID_CHR, b.consigneeaddr 
            FROM t_opr_bih_register a, t_bse_patient b, t_bse_employee e
           WHERE a.patientid_chr = b.patientid_chr
             AND a.status_int = 1
             AND areaid_chr ='" + p_strAreaID.Trim() + @"'
             AND bedid_chr ='" + p_strBedID.Trim() + @"'
             AND a.casedoctor_chr = e.empid_chr(+)
        ORDER BY a.modify_dat DESC)
 WHERE ROWNUM = 1";
            /* @update by wjqin (05-11-28)
                                     * ���SQL SERVER��strSQl�汾����
                                     */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @" SELECT  TOP 1  a.registerid_chr, a.modify_dat, a.patientid_chr,
                 a.inpatientid_chr, a.inpatient_dat, a.deptid_chr,
                 a.areaid_chr, a.bedid_chr, a.type_int, a.diagnose_vchr,
                 a.inpatientcount_int, a.state_int, a.status_int,
                 a.pstatus_int, b.lastname_vchr, b.idcard_chr, b.married_chr,
                 b.birthplace_vchr, b.homeaddress_vchr, b.sex_chr,
                 b.nationality_vchr, b.firstname_vchr, b.birth_dat,
                 b.race_vchr, b.nativeplace_vchr, b.occupation_vchr,
                 b.name_vchr, b.homephone_vchr, b.officephone_vchr,
                 b.insuranceid_vchr, b.mobile_chr, b.officeaddress_vchr,
                 b.employer_vchr, b.officepc_vchr, b.homepc_chr,
                 b.paytypeid_chr, b.consigneeaddr,              
                 b.optimes_int, b.deactivate_dat, b.isemployee_int,
                 b.firstdate_dat, b.patientrelation_vchr,
                 b.contactpersonpc_chr, b.contactpersonphone_vchr,
                 b.contactpersonaddress_vchr, b.contactpersonlastname_vchr,
                 b.contactpersonfirstname_vchr, b.email_vchr,
                 a.icd10diagtext_vchr, e.LASTNAME_VCHR as mainDoc,a.CASEDOCTOR_CHR,a.ICD10DIAGID_VCHR,a.DIAGNOSEID_CHR
            FROM t_opr_bih_register a FULL JOIN  t_bse_patient b ON a.patientid_chr = b.patientid_chr LEFT JOIN t_bse_employee e ON a.casedoctor_chr = e.empid_chr
            WHERE  a.status_int = 1          
            AND areaid_chr ='" + p_strAreaID.Trim() + @"'
            AND bedid_chr ='" + p_strBedID.Trim() + @"' 
        ORDER BY a.modify_dat DESC";
            }
            /* <<======================================= */
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
        #region ��ѯĳ����ĳ����ĳ��λ���һ��סԺ�Ļ�����Ϣ
        /// <summary>
        /// ��ѯĳ����ĳ����ĳ��λ���һ��סԺ�Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">���ſ��Һ�</param>
        /// <param name="p_strAreaID">����-��ˮ��</param>
        /// <param name="p_strBedID">��λ��ˮ��</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByRegisterid(string Registerid, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            //��ѯ������  ȷ����ѯSQL����ִ�С�				
            string strSQL = "SELECT * FROM ";
            strSQL += " (SELECT";
            strSQL += " A.REGISTERID_CHR ,A.MODIFY_DAT,A.PATIENTID_CHR , A.INPATIENTID_CHR , A.INPATIENT_DAT , A.DEPTID_CHR";
            strSQL += " , A.AREAID_CHR , A.BEDID_CHR , A.TYPE_INT , A.DIAGNOSE_VCHR , A.INPATIENTCOUNT_INT";
            strSQL += " , A.STATE_INT , A.STATUS_INT ,A.PSTATUS_INT,B.LASTNAME_VCHR , B.IDCARD_CHR , B.MARRIED_CHR";
            strSQL += " , B.BIRTHPLACE_VCHR , B.HOMEADDRESS_VCHR , B.SEX_CHR , B.NATIONALITY_VCHR , B.FIRSTNAME_VCHR";
            strSQL += " , B.BIRTH_DAT , B.RACE_VCHR , B.NATIVEPLACE_VCHR , B.OCCUPATION_VCHR , B.NAME_VCHR";
            strSQL += " , B.HOMEPHONE_VCHR , B.OFFICEPHONE_VCHR , B.INSURANCEID_VCHR , B.MOBILE_CHR, b.consigneeaddr, B.OFFICEADDRESS_VCHR";
            strSQL += " , B.EMPLOYER_VCHR , B.OFFICEPC_VCHR , B.HOMEPC_CHR , B.PAYTYPEID_CHR ,(select PAYTYPENAME_VCHR from T_BSE_PATIENTPAYTYPE where T_BSE_PATIENTPAYTYPE.paytypeid_chr = b.paytypeid_chr) as PAYTYPENAME_VCHR,B.OPTIMES_INT";
            strSQL += " , B.DEACTIVATE_DAT , B.ISEMPLOYEE_INT , B.FIRSTDATE_DAT , B.PATIENTRELATION_VCHR , B.CONTACTPERSONPC_CHR";
            strSQL += " , B.CONTACTPERSONPHONE_VCHR , B.CONTACTPERSONADDRESS_VCHR , B.CONTACTPERSONLASTNAME_VCHR , B.CONTACTPERSONFIRSTNAME_VCHR , B.EMAIL_VCHR";
            strSQL += " FROM";
            strSQL += " T_OPR_BIH_REGISTER A, T_BSE_PATIENT B";
            strSQL += " WHERE (A.PATIENTID_CHR = B.PATIENTID_CHR) AND (A.STATUS_INT=1)";
            strSQL += " AND (A.REGISTERID_CHR ='" + Registerid.Trim() + "')";
            strSQL += " ORDER BY";
            strSQL += " A.MODIFY_DAT DESC)";
            strSQL += " WHERE ROWNUM =1";

            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @" SELECT TOP 1
			 A.REGISTERID_CHR ,A.MODIFY_DAT,A.PATIENTID_CHR , A.INPATIENTID_CHR , A.INPATIENT_DAT , A.DEPTID_CHR
		 , A.AREAID_CHR , A.BEDID_CHR , A.TYPE_INT , A.DIAGNOSE_VCHR , A.INPATIENTCOUNT_INT
		 , A.STATE_INT , A.STATUS_INT ,A.PSTATUS_INT,B.LASTNAME_VCHR , B.IDCARD_CHR , B.MARRIED_CHR
		 , B.BIRTHPLACE_VCHR , B.HOMEADDRESS_VCHR , B.SEX_CHR , B.NATIONALITY_VCHR , B.FIRSTNAME_VCHR
		 , B.BIRTH_DAT , B.RACE_VCHR , B.NATIVEPLACE_VCHR , B.OCCUPATION_VCHR , B.NAME_VCHR
		 , B.HOMEPHONE_VCHR , B.OFFICEPHONE_VCHR , B.INSURANCEID_VCHR , B.MOBILE_CHR , B.OFFICEADDRESS_VCHR, b.consigneeaddr 
		 , B.EMPLOYER_VCHR , B.OFFICEPC_VCHR , B.HOMEPC_CHR , B.PAYTYPEID_CHR ,(select PAYTYPENAME_VCHR from T_BSE_PATIENTPAYTYPE where T_BSE_PATIENTPAYTYPE.paytypeid_chr = b.paytypeid_chr) as PAYTYPENAME_VCHR,B.OPTIMES_INT
		 , B.DEACTIVATE_DAT , B.ISEMPLOYEE_INT , B.FIRSTDATE_DAT , B.PATIENTRELATION_VCHR , B.CONTACTPERSONPC_CHR
		 , B.CONTACTPERSONPHONE_VCHR , B.CONTACTPERSONADDRESS_VCHR , B.CONTACTPERSONLASTNAME_VCHR , B.CONTACTPERSONFIRSTNAME_VCHR , B.EMAIL_VCHR
		 FROM
		 T_OPR_BIH_REGISTER A, T_BSE_PATIENT B
		 WHERE (A.PATIENTID_CHR = B.PATIENTID_CHR) AND (A.STATUS_INT=1)
		 AND (A.REGISTERID_CHR ='" + Registerid.Trim() + @"')
		 ORDER BY
		 A.MODIFY_DAT DESC";
            }
            /* <<======================================= */

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
        #region ��ѯĳ����ĳ�������һ��סԺ��ˮ��
        /// <summary>
        /// ��ѯĳ����ĳ�������һ��סԺ��ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID">������</param>
        /// <param name="p_strBedID">��λ��</param>
        /// <param name="p_strRegisterID">סԺ��ˮ�� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterIDByAreaIDBedID(string p_strAreaID, string p_strBedID, out string p_strRegisterID)
        {
            p_strRegisterID = "";
            long lngRes = 0;
            DataTable p_dtbResult = new DataTable();
            //��ѯ������  ȷ����ѯSQL����ִ�С�				
            string strSQL = "SELECT * FROM  ";
            strSQL += " (SELECT registerid_chr FROM t_opr_bih_register WHERE areaid_chr='" + p_strAreaID.Trim() + "' AND bedid_chr='" + p_strBedID.Trim() + "' ORDER BY MODIFY_DAT DESC)";
            strSQL += " WHERE ROWNUM =1";
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @" SELECT TOP 1  registerid_chr FROM t_opr_bih_register WHERE areaid_chr='" + p_strAreaID.Trim() + "' AND bedid_chr='" + p_strBedID.Trim() + "' ORDER BY MODIFY_DAT DESC";

            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtbResult.Rows.Count > 0)
                {
                    p_strRegisterID = p_dtbResult.Rows[0]["registerid_chr"].ToString();
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
        #region ��ѯĳסԺ�����һ��סԺ��ˮ��
        /// <summary>
        /// ��ѯĳסԺ�����һ��סԺ��ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientID">סԺ��</param>
        /// <param name="p_strRegisterID">סԺ��ˮ�� [out ����]</param>		
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterIDByInpatientID(string p_strInpatientID, out string p_strRegisterID)
        {
            p_strRegisterID = "";
            long lngRes = 0;
            DataTable p_dtbResult = new DataTable();
            //��ѯ������  ȷ����ѯSQL����ִ�С�				
            string strSQL = "SELECT * FROM  ";
            strSQL += " (SELECT registerid_chr FROM t_opr_bih_register WHERE inpatientid_chr='" + p_strInpatientID.Trim() + "' ORDER BY MODIFY_DAT DESC)";
            strSQL += " WHERE ROWNUM =1";
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT TOP 1 registerid_chr FROM t_opr_bih_register WHERE inpatientid_chr='" + p_strInpatientID.Trim() + "' ORDER BY MODIFY_DAT DESC ";
            }
            /* <<======================================= */
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtbResult.Rows.Count > 0)
                {
                    p_strRegisterID = p_dtbResult.Rows[0]["registerid_chr"].ToString();
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

        //��֤�Ƿ���ڼ�¼
        #region ��֤��Ӧ�����š������Ƿ���ڴ�λ
        /// <summary>
        ///  ��֤��Ӧ�����š������Ƿ���ڴ�λ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID_chr">������</param>
        /// <param name="p_strCode_chr">����</param>
        /// <param name="IsExistBed">�Ƿ���� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsExistBedByAreaIDAndCode(string p_strAreaID_chr, string p_strCode_chr, out bool IsExistBed)
        {
            IsExistBed = false;
            clsT_Bse_Bed_VO p_objResult = new clsT_Bse_Bed_VO();
            long lngReturn = m_lngGetBedInfoByAreaIDAndCode( p_strAreaID_chr, p_strCode_chr, out p_objResult);
            if (lngReturn > 0 && p_objResult != null && p_objResult.m_strBEDID_CHR != null && p_objResult.m_strBEDID_CHR != "")
            {
                IsExistBed = true;
            }
            return lngReturn;
        }
        #endregion
        #region ��֤��Ӧ�����š������Ƿ�մ�
        /// <summary>
        /// ��֤��Ӧ�����š������Ƿ�մ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaID_chr">������</param>
        /// <param name="p_strCode_chr">����</param>
        /// <param name="IsEmptyBed">�Ƿ�մ� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsEmptyBedByAreaIDAndCode(string p_strAreaID_chr, string p_strCode_chr, out bool IsEmptyBed)
        {
            IsEmptyBed = false;
            clsT_Bse_Bed_VO p_objResult = new clsT_Bse_Bed_VO();
            long lngReturn = m_lngGetBedInfoByAreaIDAndCode( p_strAreaID_chr, p_strCode_chr, out p_objResult);
            if (lngReturn > 0 && p_objResult != null && p_objResult.m_strBEDID_CHR != null && p_objResult.m_strBEDID_CHR != "")
            {
                if (p_objResult.m_intSTATUS_INT == 1)
                    IsEmptyBed = true;
                else
                    IsEmptyBed = false;
            }
            return lngReturn;
        }
        #endregion
        #region ��֤�����Ƿ��ǲ���
        /// <summary>
        /// ��֤�����Ƿ��ǲ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDEPTID_CHR">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngCheckIsIllAreaID(string p_strDEPTID_CHR, out bool IsAreaID)
        {
            long lngRes = 0;
            IsAreaID = false;
            string strSQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                                   inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                                   attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                                   wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                                   putmed_int
                              from t_bse_deptdesc
                             WHERE status_int=1 AND attributeid='0000003' AND deptid_chr='" + p_strDEPTID_CHR.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    IsAreaID = true;
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

        //���÷���
        #region ������������λ��ѯ
        /// <summary>
        /// ������������λ��ѯ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQueryCondition">��ѯ���� {û������������մ�}</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedInfo(string p_strQueryCondition, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bse_Bed_VO[0];
            long lngRes = 0;
            /* @update by wjqin (05-12-15)
                                      * ����ڴ���������
                                      /* @remark--------------------------------------
            string strSQL ="";
            strSQL +=" SELECT a.*";
            strSQL +="		,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.AREAID_CHR) AreaName ";
            strSQL +="		,decode(STATUS_INT,1,'�մ�',2,'ռ��',3,'ԤԼռ��',4,'����','')StatusName";
            strSQL +="		,decode(CATEGORY_INT,1,'����',2,'�Ӵ�',3,'�鴲','')CategoryName";
            strSQL +="		,decode(SEX_INT,1,'��',2,'Ů',3,'����','')SexName";
            strSQL +=" FROM t_bse_bed a";
            ---------------------------------------------- */
            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "		,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.AREAID_CHR) AreaName ";
            strSQL += "		,decode(STATUS_INT,1,'�մ�',2,'ռ��',3,'ԤԼռ��',4,'����','')StatusName";
            strSQL += "		,decode(CATEGORY_INT,1,'����',2,'�Ӵ�',3,'�鴲','')CategoryName";
            strSQL += "		,decode(SEX_INT,1,'��',2,'Ů',3,'����','')SexName";
            strSQL += "      ,(SELECT LASTNAME_VCHR FROM T_BSE_PATIENT WHERE TRIM(PATIENTID_CHR)=(SELECT TRIM(PATIENTID_CHR) FROM T_OPR_BIH_REGISTER b WHERE TRIM(b.bedid_chr)=TRIM(a.BEDID_CHR) AND b.PSTATUS_INT=1 AND ROWNUM=1)) PatientName";
            //����ʾ����סԺ��add by chenxiang2006-03-27
            strSQL += "      ,(SELECT INPATIENTID_CHR FROM T_BSE_PATIENT WHERE TRIM(PATIENTID_CHR)=(SELECT TRIM(PATIENTID_CHR) FROM T_OPR_BIH_REGISTER b WHERE TRIM(b.bedid_chr)=TRIM(a.BEDID_CHR) AND b.PSTATUS_INT=1 AND ROWNUM=1)) INPATIENTID_CHR";
            strSQL += " FROM t_bse_bed a";
            /* <<======================================= */
            /* @update by wjqin (05-11-28)
                         * ���SQL SERVER��strSQl�汾����
                         */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT a.*
			,(select deptname_vchr from t_bse_deptdesc where deptid_chr=a.AREAID_CHR) AreaName 
			--,decode(STATUS_INT,1,'�մ�',2,'ռ��',3,'ԤԼռ��',4,'����','')StatusName
,(CASE STATUS_INT WHEN 1 THEN '�մ�' WHEN 2 THEN 'ռ��' WHEN 3 THEN 'ԤԼռ��' WHEN 4 THEN '����' ELSE '' END)StatusName
			--,decode(CATEGORY_INT,1,'����',2,'�Ӵ�',3,'�鴲','')CategoryName
,(CASE CATEGORY_INT WHEN 1 THEN '����' WHEN 2 THEN '�Ӵ�' WHEN 3 THEN '�鴲' ELSE '' END )CategoryName
			--,decode(SEX_INT,1,'��',2,'Ů',3,'����','')SexName
,(CASE SEX_INT WHEN  1 THEN '��' WHEN 2 THEN 'Ů' WHEN 3 THEN '����' ELSE '' END )SexName
		 FROM t_bse_bed a";
            }
            /* <<======================================= */

            if (p_strQueryCondition != string.Empty)
            {
                strSQL += " WHERE " + p_strQueryCondition;
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
                    p_objResultArr = new clsT_Bse_Bed_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bse_Bed_VO();
                        p_objResultArr[i1].m_strBEDID_CHR = dtbResult.Rows[i1]["BEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCODE_CHR = dtbResult.Rows[i1]["CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_dblRATE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["RATE_MNY"].ToString());
                        p_objResultArr[i1].m_intSEX_INT = Convert.ToInt32(dtbResult.Rows[i1]["SEX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intCATEGORY_INT = Convert.ToInt32(dtbResult.Rows[i1]["CATEGORY_INT"].ToString().Trim());
                        p_objResultArr[i1].m_dblAIRRATE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["AIRRATE_MNY"].ToString());
                        // ��������	[���ֶ�]
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        // ռ��״̬	[���ֶ�]	{1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
                        p_objResultArr[i1].m_strStatusName = dtbResult.Rows[i1]["StatusName"].ToString().Trim();
                        // ״̬	[���ֶ�]	{1=����;2=�Ӵ�;3=�鴲}
                        p_objResultArr[i1].m_strCategoryName = dtbResult.Rows[i1]["CategoryName"].ToString().Trim();
                        // �Ա�	[���ֶ�]	{��Ů��(1-��,2-Ů,3-����)}
                        p_objResultArr[i1].m_strSexName = dtbResult.Rows[i1]["SexName"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["PatientName"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENTID_CHR = dtbResult.Rows[i1]["INPATIENTID_CHR"].ToString().Trim();
                        //�ӿյ��շ���Ŀid
                        p_objResultArr[i1].m_str_AIRCHARGEITEMID_CHR = dtbResult.Rows[i1]["AIRCHARGEITEMID_CHR"].ToString().Trim();

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
        #region ��SQL�����޸Ĵ�λ��Ϣ
        /// <summary>
        /// ��SQL�����޸Ĵ�λ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQLUpdate">Update��SQL���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBedInfo(string p_strSQLUpdate)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLUpdate);
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
        #region ��SQL����ɾ����λ��Ϣ
        /// <summary>
        /// ��SQL����ɾ����λ��Ϣ	
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQLDelete">Delete��SQL���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteBedInfo(string p_strSQLDelete)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(p_strSQLDelete);
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
        #region �����շ���Ϣ
        [AutoComplete]
        public long m_lngGetBedChargeItem(out clsT_bse_chargeitem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO[0];
            long lngRes = 0;
            DataTable dtbResult1 = null;
            lngRes = 0;
            lngRes = m_lngGetSpChargeItemIDType(out dtbResult1);
            string TypeID = "";
            if (lngRes > 0 && dtbResult1.Rows.Count > 0)
            {
                TypeID = dtbResult1.Rows[0]["BEDCHARGECATE"].ToString();
            }
            string strSQL = @"SELECT * FROM T_BSE_CHARGEITEM WHERE ITEMIPINVTYPE_CHR = '" + TypeID + "'";
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
                        if (dtbResult.Rows[i1]["ITEMSRCTYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ITEMPRICE_MNY"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblITEMPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["DOSAGE_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = Convert.ToDouble(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ISGROUPITEM_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["SELFDEFINE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["PACKQTY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblPACKQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["TRADEPRICE_MNY"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblTRADEPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["POFLAG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["ISRICH_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["OPCHARGEFLG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intOPCHARGEFLG_INT = Convert.ToInt32(dtbResult.Rows[i1]["OPCHARGEFLG_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMSRCNAME_VCHR = dtbResult.Rows[i1]["ITEMSRCNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMSRCTYPENAME_VCHR = dtbResult.Rows[i1]["ITEMSRCTYPENAME_VCHR"].ToString().Trim();
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
        #region �յ��շ���Ϣ
        [AutoComplete]
        public long m_lngGetAIRRATEChargeItem(out clsT_bse_chargeitem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_bse_chargeitem_VO[0];
            long lngRes = 0;
            DataTable dtbResult1 = null;
            lngRes = 0;
            lngRes = m_lngGetSpChargeItemIDType(out dtbResult1);
            string TypeID = "";
            if (lngRes > 0 && dtbResult1.Rows.Count > 0)
            {
                TypeID = dtbResult1.Rows[0]["BEDCHARGECATE"].ToString();
            }
            string strSQL = @"SELECT * FROM T_BSE_CHARGEITEM WHERE trim(ITEMNAME_VCHR) like '�յ���%'";
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
                        if (dtbResult.Rows[i1]["ITEMSRCTYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ITEMSRCTYPE_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMSPEC_VCHR = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ITEMPRICE_MNY"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblITEMPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMUNIT_CHR = dtbResult.Rows[i1]["ITEMUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPUNIT_CHR = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPUNIT_CHR = dtbResult.Rows[i1]["ITEMIPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMOPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPCALCTYPE_CHR = dtbResult.Rows[i1]["ITEMIPCALCTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMOPINVTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMIPINVTYPE_CHR = dtbResult.Rows[i1]["ITEMIPINVTYPE_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["DOSAGE_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = Convert.ToDouble(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ISGROUPITEM_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISGROUPITEM_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISGROUPITEM_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMCATID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUSAGEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMOPCODE_CHR = dtbResult.Rows[i1]["ITEMOPCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINSURANCEID_CHR = dtbResult.Rows[i1]["INSURANCEID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["SELFDEFINE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSELFDEFINE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SELFDEFINE_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["PACKQTY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblPACKQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["TRADEPRICE_MNY"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblTRADEPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["POFLAG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["POFLAG_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["ISRICH_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["OPCHARGEFLG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intOPCHARGEFLG_INT = Convert.ToInt32(dtbResult.Rows[i1]["OPCHARGEFLG_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strITEMSRCNAME_VCHR = dtbResult.Rows[i1]["ITEMSRCNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMSRCTYPENAME_VCHR = dtbResult.Rows[i1]["ITEMSRCTYPENAME_VCHR"].ToString().Trim();
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
        #region ��ѯ����������
        [AutoComplete]
        public long m_lngGetSpChargeItemIDType(out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "select * from T_BSE_BIH_SPECORDERCATE";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes < 0)
                {
                    return -1;
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
        #region ͬ�����Ӳ�������Ϣ
        #region �������סԺ�Ǽ���Ϣ
        [AutoComplete]
        public long m_lngGetPatientRegisterInfoByID(string p_strRegisterID, out clsT_Opr_Bih_Register_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Register_VO();
            long lngRes = 0;
            string strSQL = @"select a.* from t_opr_bih_register a,(
                              select min (t_opr_bih_register.modify_dat) as modify_dat,
                              t_opr_bih_register.registerid_chr
                              from t_opr_bih_register
                              group by t_opr_bih_register.registerid_chr
                              ) b
                              where a.modify_dat = b.modify_dat and a.registerid_chr = b.registerid_chr
                              and a.registerid_chr = '" + p_strRegisterID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_Register_VO();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["MODIFY_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[0]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["ISBOOKING_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intISBOOKING_INT = Convert.ToInt32(dtbResult.Rows[0]["ISBOOKING_INT"].ToString().Trim());
                    }
                    p_objResult.m_strINPATIENTID_CHR = dtbResult.Rows[0]["INPATIENTID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["INPATIENT_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strINPATIENT_DAT = Convert.ToDateTime(dtbResult.Rows[0]["INPATIENT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strDEPTID_CHR = dtbResult.Rows[0]["DEPTID_CHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strBEDID_CHR = dtbResult.Rows[0]["BEDID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["TYPE_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["TYPE_INT"].ToString().Trim());
                    }
                    p_objResult.m_strDIAGNOSE_VCHR = dtbResult.Rows[0]["DIAGNOSE_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["LIMITRATE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblLIMITRATE_MNY = Convert.ToDouble(dtbResult.Rows[0]["LIMITRATE_MNY"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["INPATIENTCOUNT_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intINPATIENTCOUNT_INT = Convert.ToInt32(dtbResult.Rows[0]["INPATIENTCOUNT_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["STATE_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSTATE_INT = Convert.ToInt32(dtbResult.Rows[0]["STATE_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["STATUS_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                    p_objResult.m_strOPERATORID_CHR = dtbResult.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["PSTATUS_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_INT"].ToString().Trim());
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
        #region ������Ϣ
        [AutoComplete]
        public long m_lngGetBedInfoByID(string p_strBedID, out clsT_Bse_Bed_VO p_objResult)
        {
            p_objResult = new clsT_Bse_Bed_VO();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_BSE_BED WHERE BEDID_CHR = '" + p_strBedID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Bse_Bed_VO();
                    p_objResult.m_strBEDID_CHR = dtbResult.Rows[0]["BEDID_CHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strCODE_CHR = dtbResult.Rows[0]["CODE_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["STATUS_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["RATE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblRATE_MNY = Convert.ToDouble(dtbResult.Rows[0]["RATE_MNY"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["SEX_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSEX_INT = Convert.ToInt32(dtbResult.Rows[0]["SEX_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["CATEGORY_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intCATEGORY_INT = Convert.ToInt32(dtbResult.Rows[0]["CATEGORY_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["AIRRATE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblAIRRATE_MNY = Convert.ToDouble(dtbResult.Rows[0]["AIRRATE_MNY"].ToString().Trim());
                    }
                    p_objResult.m_strCHARGEITEMID_CHR = dtbResult.Rows[0]["CHARGEITEMID_CHR"].ToString().Trim();
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
        #region ������Ϣ
        [AutoComplete]
        public long m_lngGetAreaInfoByID(string p_strAreaID, out clsT_BSE_DEPTDESC_VO p_objResult)
        {
            p_objResult = new clsT_BSE_DEPTDESC_VO();
            long lngRes = 0;
            string strSQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                                   inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                                   attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                                   wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                                   putmed_int
                              from t_bse_deptdesc
                             where status_int = '1' and deptid_chr = '" + p_strAreaID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_BSE_DEPTDESC_VO();
                    p_objResult.m_strDEPTID_CHR = dtbResult.Rows[0]["DEPTID_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["MODIFY_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[0]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strDEPTNAME_VCHR = dtbResult.Rows[0]["DEPTNAME_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["CATEGORY_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intCATEGORY_INT = Convert.ToInt32(dtbResult.Rows[0]["CATEGORY_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["INPATIENTOROUTPATIENT_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intINPATIENTOROUTPATIENT_INT = Convert.ToInt32(dtbResult.Rows[0]["INPATIENTOROUTPATIENT_INT"].ToString().Trim());
                    }
                    p_objResult.m_strOPERATORID_CHR = dtbResult.Rows[0]["OPERATORID_CHR"].ToString().Trim();
                    p_objResult.m_strADDRESS_VCHR = dtbResult.Rows[0]["ADDRESS_VCHR"].ToString().Trim();
                    p_objResult.m_strSHORTNO_CHR = dtbResult.Rows[0]["SHORTNO_CHR"].ToString().Trim();
                    p_objResult.m_strPYCODE_CHR = dtbResult.Rows[0]["PYCODE_CHR"].ToString().Trim();
                    p_objResult.m_strATTRIBUTEID = dtbResult.Rows[0]["ATTRIBUTEID"].ToString().Trim();
                    p_objResult.m_strPARENTID = dtbResult.Rows[0]["PARENTID"].ToString().Trim();
                    if (dtbResult.Rows[0]["CREATEDATE_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    if (dtbResult.Rows[0]["STATUS_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["DEACTIVATE_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strWBCODE_CHR = dtbResult.Rows[0]["WBCODE_CHR"].ToString().Trim();
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
        #endregion
        //��ԃ���˻���״̬
        #region
        [AutoComplete]
        public long m_lngGetPatientCareInfo(string p_strResgisterID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string id1 = "";
            string id2 = "";
            DataTable dtbResult1 = null;
            lngRes = 0;
            lngRes = m_lngGetSpChargeItemIDType(out dtbResult1);
            if (lngRes > 0 && dtbResult1.Rows.Count > 0)
            {
                id1 = dtbResult1.Rows[0]["EATDICCATE"].ToString();
                id2 = dtbResult1.Rows[0]["NURSECATE"].ToString();
            }
            string strSQL = @"SELECT distinct ordercateid_chr,NAME_CHR
  FROM (SELECT *
          FROM t_opr_bih_order
         WHERE  t_opr_bih_order.registerid_chr = '" + p_strResgisterID + @"'
           AND t_opr_bih_order.status_int = 2) a,
       t_bse_bih_orderdic b
 WHERE a.orderdicid_chr = b.orderdicid_chr AND (b.ordercateid_chr = '" + id1 + @"'
    OR b.ordercateid_chr = '" + id2 + "')";
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
        #endregion
        #region ��ȡ��λ�շ���Ϣ
        [AutoComplete]
        public long m_lngGetChargeNameByID(string p_strChargeItemID, out clsT_bse_chargeitem_VO p_objResult)
        {
            p_objResult = new clsT_bse_chargeitem_VO();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_BSE_CHARGEITEM WHERE ITEMID_CHR = '" + p_strChargeItemID + "'";
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
                        p_objResult.m_dblITEMPRICE_MNY = Convert.ToDouble(dtbResult.Rows[0]["ITEMPRICE_MNY"].ToString().Trim());
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
                        p_objResult.m_dblDOSAGE_DEC = Convert.ToDouble(dtbResult.Rows[0]["DOSAGE_DEC"].ToString().Trim());
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
                        p_objResult.m_dblPACKQTY_DEC = Convert.ToDouble(dtbResult.Rows[0]["PACKQTY_DEC"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["TRADEPRICE_MNY"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblTRADEPRICE_MNY = Convert.ToDouble(dtbResult.Rows[0]["TRADEPRICE_MNY"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["POFLAG_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intPOFLAG_INT = Convert.ToInt32(dtbResult.Rows[0]["POFLAG_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["ISRICH_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[0]["ISRICH_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["OPCHARGEFLG_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intOPCHARGEFLG_INT = Convert.ToInt32(dtbResult.Rows[0]["OPCHARGEFLG_INT"].ToString().Trim());
                    }
                    p_objResult.m_strITEMSRCNAME_VCHR = dtbResult.Rows[0]["ITEMSRCNAME_VCHR"].ToString().Trim();
                    p_objResult.m_strITEMSRCTYPENAME_VCHR = dtbResult.Rows[0]["ITEMSRCTYPENAME_VCHR"].ToString().Trim();
                    p_objResult.m_strITEMENGNAME_VCHR = dtbResult.Rows[0]["ITEMENGNAME_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["IFSTOP_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intIFSTOP_INT = Convert.ToInt32(dtbResult.Rows[0]["IFSTOP_INT"].ToString().Trim());
                    }
                    p_objResult.m_strPDCAREA_VCHR = dtbResult.Rows[0]["PDCAREA_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["IPCHARGEFLG_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intIPCHARGEFLG_INT = Convert.ToInt32(dtbResult.Rows[0]["IPCHARGEFLG_INT"].ToString().Trim());
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
        #region ��ѯ�������µ�ת��Ϣ
        [AutoComplete]
        public long m_lngGetPatientLastestTransferInfo(string strResgisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.*
  FROM t_opr_bih_transfer a,
       (SELECT   MAX (modify_dat) AS modify_dat, registerid_chr
            FROM t_opr_bih_transfer
        GROUP BY t_opr_bih_transfer.registerid_chr) b
 WHERE a.modify_dat = b.modify_dat AND a.registerid_chr = b.registerid_chr
 and a.registerid_chr = '" + strResgisterID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes < 0)
                {
                    return -1;
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
        #region ɾ����ת��Ϣ
        [AutoComplete]
        public long m_lngDeleteTransferInfoByID(string p_strTransferID)
        {
            long lngRes = 0;
            string strSQL = "delete from t_opr_bih_transfer where TRANSFERID_CHR = '" + p_strTransferID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
                if (lngRes < 0)
                {
                    return -1;
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
        #region ��������ת��δ������
        [AutoComplete]
        public long m_lngUndoTransferOut(string AreaID, string p_strTransferID, string RegisterID, string strBedID)
        {
            long lngRes = m_lngDeleteTransferInfoByID(p_strTransferID);

            clsT_Opr_Bih_Register_VO objRegister = null;
            if (lngRes > 0)
            {
                com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc RegSvc = new com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc();
                lngRes = 0;
                lngRes = RegSvc.m_lngGetBinRegisterByRegisterID(RegisterID, out objRegister);
                RegSvc.Dispose();
            }
            if (lngRes < 0 || objRegister.m_strREGISTERID_CHR == "")
                lngRes = -1;
            if (lngRes > 0)
            {
                DataTable dtbResult;
                lngRes = 0;
                lngRes = this.m_lngGetPatientLastestTransferInfo(objRegister.m_strREGISTERID_CHR, out dtbResult);
                objRegister.m_strBEDID_CHR = strBedID;
                objRegister.m_intPSTATUS_INT = 1;
                objRegister.m_strMODIFY_DAT = dtbResult.Rows[0]["MODIFY_DAT"].ToString();
                objRegister.m_strAREAID_CHR = AreaID;
                com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc RegSvc = new com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc();
                lngRes = 0;
                lngRes = RegSvc.m_lngModifyBihRegisterInfoByVo(objRegister);
                RegSvc.Dispose();
            }
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = this.m_lngModifyBedByBedID(strBedID, 2);
            }
            if (lngRes <= 0)
            {
                ContextUtil.SetAbort();
            }
            else
            {
                ContextUtil.SetComplete();
            }
            return lngRes;
        }
        #endregion

        #region סԺ����ת��������(��ʱ��κͿ��Ҳ�ѯ)

        /// <summary>
        /// סԺ����ת��������(��ʱ��κͿ��Ҳ�ѯ)
        /// </summary>
        /// <param name="p_intType"></param>
        /// <param name="p_BeginTime"></param>
        /// <param name="p_EndTime"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInhospitalReportData(int p_intType, string p_strAreaid, System.DateTime p_BeginTime, DateTime p_EndTime, out DataTable p_dtbResult)
        {
            /*�������ͳ���*/
            const int TR_BED = 2;  //ת��
            const int TR_AREA = 3;  //ת��
            const int IN_HOS = 5;  //��Ժ
            const int OUT_HOS = 6;  //��Ժ

            p_dtbResult = new DataTable();
            //string strToday = p_StaticTime.ToShortDateString() + " 00:00:00";
            string strBeginDate = p_BeginTime.ToString("yyyy-MM-dd") + " 00:00:00";
            string strEndDate = p_EndTime.ToString("yyyy-MM-dd") + " 23:59:59";
            long lngRes = 0;
            #region SQL
            string strSQL = @"select distinct reg.registerid_chr,
                                     pat.lastname_vchr    as lastname,
                                     pat.sex_chr          as sex,
                                     pat.birth_dat,
                                     '' as age,
                                     reg.inpatientid_chr  as inpatientid, 
                                     reg.inpatient_dat as inpatientdate,
                                     pty.paytypename_vchr as paytype,
                                     (   
                                         select deptname_vchr 
                                           from t_bse_deptdesc 
                                          where deptid_chr = trf.sourceareaid_chr
                                     )                    as sourceareaname,             
                                    (
                                         select code_chr
                                           from t_bse_bed
                                          where bedid_chr = trf.sourcebedid_chr
                                     )                    as sourcebedno,
                                     dep.deptname_vchr    as targetareaname,
                                     bed.code_chr         as targetbedno,
                                     emp.lastname_vchr    as operatorname,
                                     acc.charge_dec,
                                     acc.clearchg_dec,
                                     pre.money_dec        as money_dec,
                                     trf.modify_dat       as modify_dat
                                          
                                from t_bse_deptdesc dep,
                                     t_bse_bed  bed,
                                     t_bse_employee emp,
                                     t_bse_patientpaytype pty,
                                     t_opr_bih_transfer trf,
                                     (select   sum (money_dec) as money_dec, registerid_chr from t_opr_bih_prepay
                                            group by registerid_chr) pre,
                                     (select   sum (charge_dec) as charge_dec,sum (clearchg_dec) as clearchg_dec, registerid_chr
                                            from t_opr_bih_dayaccount group by registerid_chr) acc,
                                     t_opr_bih_register reg,
                                     t_opr_bih_registerdetail pat
                               where reg.registerid_chr = pre.registerid_chr(+) 
                                 and reg.registerid_chr = acc.registerid_chr(+) 
                                 and reg.registerid_chr = pat.registerid_chr 
                                 and reg.registerid_chr = trf.registerid_chr 
                                 and trf.targetareaid_chr   = dep.deptid_chr(+) 
                                 and trf.targetbedid_chr= bed.bedid_chr(+) 
                                 and trf.operatorid_chr = emp.empid_chr 
                                 and pty.paytypeid_chr  = reg.paytypeid_chr ";
            #endregion

            #region ����
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] param = null;
            switch (p_intType)
            {
                case TR_AREA:

                    if (p_strAreaid != null && p_strAreaid != "")
                    {
                        strSQL += "and trf.targetareaid_chr in (" + p_strAreaid + ")";
                    }

                    strSQL += @" and trf.type_int = ?
                                 and trf.modify_dat between ? and ? ";
                    objHRPSvc.CreateDatabaseParameter(3, out param);
                    param[0].Value = TR_AREA;
                    param[1].Value = DateTime.Parse(strBeginDate);
                    param[2].Value = DateTime.Parse(strEndDate);
                    break;

                case TR_BED:

                    if (p_strAreaid != null && p_strAreaid != "")
                    {
                        strSQL += "and trf.targetareaid_chr in (" + p_strAreaid + ")";
                    }

                    strSQL += @" and trf.type_int = ?
                                 and trf.modify_dat between ? and ? ";
                    objHRPSvc.CreateDatabaseParameter(3, out param);
                    param[0].Value = TR_BED;
                    param[1].Value = DateTime.Parse(strBeginDate);
                    param[2].Value = DateTime.Parse(strEndDate);
                    break;

                case IN_HOS:

                    if (p_strAreaid != null && p_strAreaid != "")
                    {
                        strSQL += "and trf.targetareaid_chr in (" + p_strAreaid + ")";
                    }

                    strSQL += @" and trf.type_int = ?  
                                and reg.inpatient_dat between ? and ?";
                    objHRPSvc.CreateDatabaseParameter(3, out param);
                    param[0].Value = IN_HOS;
                    param[1].Value = DateTime.Parse(strBeginDate);
                    param[2].Value = DateTime.Parse(strEndDate);
                    break;

                case OUT_HOS:

                    strSQL += @" and trf.type_int = ?
                                and trf.modify_dat between ? and ?";
                    objHRPSvc.CreateDatabaseParameter(3, out param);
                    param[0].Value = OUT_HOS;
                    param[1].Value = DateTime.Parse(strBeginDate);
                    param[2].Value = DateTime.Parse(strEndDate);
                    break;
                default:
                    strSQL = "";
                    break;
            }
            if (strSQL == "")
            {
                return lngRes;
            }
            #endregion

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, param);
                objHRPSvc.Dispose();
                p_dtbResult.AcceptChanges();
                clsBrithdayToAge temp = new clsBrithdayToAge();
                for (int intI = 0; intI < p_dtbResult.Rows.Count; intI++)
                {
                    String strAge = temp.m_strGetAge(p_dtbResult.Rows[intI]["birth_dat"]);
                    p_dtbResult.Rows[intI]["age"] = strAge;
                }
                p_dtbResult.DefaultView.Sort = "sourceareaname, sourcebedno,targetareaname";
                if (lngRes < 0)
                {
                    return -1;
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

        #region ��Ժ��־��������(��ʱ��κͿ��Ҳ�ѯ)
        /// <summary>
        /// ��Ժ��־��������(��ʱ��κͿ��Ҳ�ѯ)
        /// </summary>
        /// <param name="p_BeginTime"></param>
        /// <param name="p_EndTime"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOuthospitalReportData(string p_strAreaid, System.DateTime p_BeginTime, DateTime p_EndTime, out DataTable p_dtbResult)
        {

            p_dtbResult = new DataTable();
            //string strToday = p_StaticTime.ToShortDateString() + " 00:00:00";
            string strBeginDate = p_BeginTime.ToString("yyyy-MM-dd") + " 00:00:00";
            string strEndDate = p_EndTime.ToString("yyyy-MM-dd") + " 23:59:59";
            long lngRes = 0;
            string strSQL = @"select distinct reg.registerid_chr,
                                      reg.inpatientid_chr as inpatientid, 
                                      pat.lastname_vchr   as lastname,
                                     reg.inareadate_dat inpatientdate, 
                                     pat.sex_chr          as sex,
                                     pat.birth_dat,
                                     '' as age,
                                     pty.paytypename_vchr as paytype,
                                    (select deptname_vchr 
                                     from t_bse_deptdesc 
                                     where deptid_chr = trf.outareaid_chr) as sourceareaname,             
                                    (select code_chr
                                     from t_bse_bed
                                      where bedid_chr = trf.outareaid_chr)  as sourcebedno,
                                     dep.deptname_vchr    as targetareaname,
                                     bed.code_chr         as targetbedno,
                                     emp.lastname_vchr    as operatorname,
                                     acc.charge_dec,
                                     acc.clearchg_dec,
                                     pre.money_dec        as money_dec,
                                     trf.outhospital_dat  as modify_dat
                                     
                                from t_bse_deptdesc dep,
                                     t_bse_bed  bed,
                                     t_bse_employee emp,
                                     t_bse_patientpaytype pty,
                                     t_opr_bih_leave trf,
                                     (select   sum (money_dec) as money_dec, registerid_chr from t_opr_bih_prepay
                                            group by registerid_chr) pre,
                                     (select   sum (charge_dec) as charge_dec,sum (clearchg_dec) as clearchg_dec, registerid_chr
                                            from t_opr_bih_dayaccount group by registerid_chr) acc,
                                     t_opr_bih_register reg,
                                     t_opr_bih_registerdetail pat
                               where reg.registerid_chr = pre.registerid_chr(+) and
                                     reg.registerid_chr = acc.registerid_chr(+) and
                                     reg.registerid_chr = pat.registerid_chr and
                                     reg.registerid_chr = trf.registerid_chr and
                                     reg.areaid_chr = dep.deptid_chr and
                                     reg.bedid_chr = bed.bedid_chr and
                                     trf.operatorid_chr = emp.empid_chr and
                                     pty.paytypeid_chr = reg.paytypeid_chr ";// �޸��� �� �˹ⱱ 2008.8.5

            if (p_strAreaid != null && p_strAreaid != "")
            {
                strSQL += "and reg.areaid_chr in (" + p_strAreaid + ")";
            }

            strSQL += @" and trf.status_int = 1 and
                            ((trf.outhospital_dat between ? and ?)
                            or trf.outhospital_dat = ?)";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = DateTime.Parse(strBeginDate);
                param[1].Value = DateTime.Parse(strEndDate);
                param[2].Value = DateTime.Parse(strBeginDate);

                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, param);
                objHRPSvc.Dispose();
                p_dtbResult.AcceptChanges();
                clsBrithdayToAge temp = new clsBrithdayToAge();
                for (int intI = 0; intI < p_dtbResult.Rows.Count; intI++)
                {
                    String strAge = temp.m_strGetAge(p_dtbResult.Rows[intI]["birth_dat"]);
                    p_dtbResult.Rows[intI]["age"] = strAge;
                }
                p_dtbResult.DefaultView.Sort = "sourceareaname, sourcebedno";
                if (lngRes < 0)
                {
                    return -1;
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

        #region ͨ�������鲡�� ���Ӳ���
        [AutoComplete]
        public string m_mlngGetEMRroomIDBYAREAID(string AreaID)
        {
            long lngRes = 0;
            string roomID = "";
            string strSQL = "select count(*) c ,max(room_id) as id from INPATIENT_ROOM_AREA where AREA_ID = '" + AreaID + "'";
            DataTable p_dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0)
                {
                    if (Convert.ToInt32(p_dtbResult.Rows[0]["c"].ToString()) > 0)
                    {
                        roomID = p_dtbResult.Rows[0]["id"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return roomID;
        }
        #endregion
        #region ͨ�������鲡�� ���Ӳ���
        [AutoComplete]
        public string m_mlngGetEMRbedIDBYbedcode(string roomID, string bedcode)
        {
            long lngRes = 0;
            string bedid = "";
            string strSQL = @"SELECT   count(*) as c,max(b.bed_id) as id
    FROM (SELECT *
            FROM inpatient_bed_room
           WHERE end_date_bed_room = TO_DATE ('1900-1-1', 'YYYY-MM-DD')) a,
         inpatient_bed_desc b
   WHERE a.bed_id = b.bed_id
     AND a.room_id = '" + roomID + @"'
     AND b.end_date_bed_naming = TO_DATE ('1900-1-1', 'YYYY-MM-DD')
     AND B.BED_NAME = '" + bedcode + @"'
ORDER BY a.bed_id";
            /* @update by weijie.qin (05-11-18)
             * ���SQL SERVER��strSQl�汾����
             */
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                strSQL = @"SELECT   count(*) as c,max(b.bed_id) as id
				FROM (SELECT *
						FROM inpatient_bed_room
			         
					WHERE end_date_bed_room = CONVERT (DATETIME,'1900-1-1')) a,
					inpatient_bed_desc b
			WHERE a.bed_id = b.bed_id
				AND a.room_id = '" + roomID + @"'
			   
				AND b.end_date_bed_naming = CONVERT (DATETIME,'1900-1-1')
				AND B.BED_NAME = '" + bedcode + @"'";
            }
            /* <<======================================= */
            DataTable p_dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0)
                {
                    if (Convert.ToInt32(p_dtbResult.Rows[0]["c"].ToString()) > 0)
                    {
                        bedid = p_dtbResult.Rows[0]["id"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return bedid;
        }
        #endregion
        #region ͨ�������鲡�� ���Ӳ���
        [AutoComplete]
        public bool m_blnEMRGetInPatientByID(string InpatientID)
        {
            long lngRes = 0;
            string strSQL = @"select count(*) as c from PATIENTBASEINFO a where a.inpatientid = '" + InpatientID + "'";
            DataTable p_dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtbResult.Rows[0]["c"].ToString() == "1")
                {
                    return true;
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
        #region ���Ӳ�������
        [AutoComplete]
        public string GetBedIDByRoomID(string roomid, string BedName)
        {
            string BedID = "";
            string strSQL = @"SELECT Count(*) as c,max(a.bed_id) as id
  FROM inpatient_bed_room a, inpatient_bed_desc b
 WHERE a.bed_id = b.bed_id
   AND a.end_date_bed_room = TO_DATE ('1900-1-1', 'YYYY-MM-DD')
   AND a.room_id = '" + roomid + @"'
   AND b.bed_name = '" + BedName + "'";
            try
            {
                long lngRes = 0;
                DataTable p_dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && Convert.ToInt32(p_dtbResult.Rows[0]["c"].ToString()) > 0)
                {
                    BedID = p_dtbResult.Rows[0]["id"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return BedID;
        }
        #endregion
        #region ��Ӳ��˰���
        [AutoComplete]
        public long m_lngAddPatientOccupyBed(string p_strRegisterid, string p_strBedid)
        {
            long lngRes = 0;
            lngRes = this.m_lngModifyBedByBedID(p_strBedid, 4);
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = this.m_lngAddOccupyBed(p_strRegisterid, p_strBedid);
            }
            return lngRes;
        }
        #endregion
        #region ɾ�����˰���
        [AutoComplete]
        public long m_lngDelPatientOccupyBedByBedID(string p_strBedid)
        {
            long lngRes = 0;
            lngRes = this.m_lngModifyBedByBedID(  p_strBedid, 1);
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = this.m_lngDelOccupyBedByBedID(p_strBedid);
            }
            return lngRes;
        }
        #endregion
        #region ɾ�����˰���
        [AutoComplete]
        public long m_lngDelPatientOccupyBedByRegisterID(string p_strRegisterID)
        {
            long lngRes = 0;
            DataTable dtbResult = null;
            lngRes = this.m_lngQueryOccupyBed(p_strRegisterID, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                {
                    lngRes = 0;
                    lngRes = this.m_lngModifyBedByBedID(dtbResult.Rows[i]["BEDID_CHR"].ToString(), 1);
                    if (lngRes <= 0)
                    {
                        break;
                    }
                }
            }
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = this.m_lngDelOccupyBedByRegisterID(p_strRegisterID);
            }
            return lngRes;
        }
        #endregion
        #region ɾ������
        [AutoComplete]
        public long m_lngDelOccupyBedByBedID(string p_strBedid)
        {
            long lngRes = 0;
            string strSQL = @"delete from T_OPR_BIH_WRAPBED where BEDID_CHR = '" + p_strBedid + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ɾ������
        [AutoComplete]
        public long m_lngDelOccupyBedByRegisterID(string p_strRegisterID)
        {
            long lngRes = 0;
            string strSQL = @"delete from T_OPR_BIH_WRAPBED where REGISTERID_CHR = '" + p_strRegisterID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��Ӱ���
        [AutoComplete]
        public long m_lngAddOccupyBed(string p_strRegisterid, string p_strBedid)
        {
            long lngRes = 0;
            string strSQL = @"insert into T_OPR_BIH_WRAPBED (SEQ_INT,REGISTERID_CHR,BEDID_CHR) values (seq_t_opr_bih_warpbedhistory.NEXTVAL, '" + p_strRegisterid + "','" + p_strBedid + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��ѯ����
        [AutoComplete]
        public long m_lngQueryOccupyBed(string p_strRegisterid, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select * from T_OPR_BIH_WRAPBED where REGISTERID_CHR = '" + p_strRegisterid + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��ѯ����������Ϣ
        [AutoComplete]
        public long m_lngQueryPatientInfoByOccupiedBedid(string Bedid, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            string strSQL = @"select * from T_OPR_BIH_WRAPBED where BEDID_CHR = '" + Bedid + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ��籣�Ǽ�
        /// <summary>
        /// �ж��Ƿ��籣�Ǽ�
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsYbReg(string regId)
        {
            try
            {
                DataTable dt = null;
                string Sql = @"select 1 from t_ins_cszyreg where registerid_vchr = ?";
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = regId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return false;
        }
        #endregion
    }
}
