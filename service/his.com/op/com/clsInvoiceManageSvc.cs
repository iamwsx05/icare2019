using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInvoiceManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsInvoiceManageSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //���﷢Ʊ����
        #region ��ӷ�Ʊ����
        /// <summary>
        /// ��ӷ�Ʊ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">[������ص��ֶβ������]</param>
        /// <param name="p_strRecordID">��Ʊ������ˮ��</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngDoAddNewT_opr_opinvoiceman(clsT_opr_opinvoiceman_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "Appid_chr", "t_opr_opinvoiceman", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_opinvoiceman (APPID_CHR,INVOICENOFROM_VCHR,INVOICENOTO_VCHR,APPLY_DAT,APPUSERID_CHR,OPERATORID_CHR,STATUS_INT, INVOICETYPEID_INT) VALUES (?,?,?,?,?,?,?, ?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(8, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strAPPID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strINVOICENOFROM_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strINVOICENOTO_VCHR;
                objLisAddItemRefArr[3].Value = DateTime.Parse(p_objRecord.m_strAPPLY_DAT);
                objLisAddItemRefArr[4].Value = p_objRecord.m_strAPPUSERID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[6].Value = 0;//0-���� 1������;
                objLisAddItemRefArr[7].Value = p_objRecord.intInvoiceTypeFlag;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes <= 0) lngRes = -100;
            return lngRes;
        }
        #endregion

        #region ��������ķ�Ʊ				2004-8-23
        /// <summary>
        /// ��������ķ�Ʊ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">[ֻ��Ҫm_strAPPID_CHR��m_strCANCELUSERID_CHR]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngModifyT_opr_opinvoiceman(clsT_opr_opinvoiceman_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "UPDATE  T_OPR_OPINVOICEMAN SET " +
                "  CANCELUSERID_CHR = '" + p_objRecord.m_strCANCELUSERID_CHR + "' " +
                " , CANCEL_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss') " +
                " , STATUS_INT = 1 " + //[״̬����-��������-����]
                " WHERE Appid_chr = '" + p_objRecord.m_strAPPID_CHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region ��ѯ���췢��Ʊ				2004-8-23
        /// <summary>
        /// ��ѯ���췢��Ʊ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStartapply_dat">��ѯ��������ʼ����ʱ��</param>
        /// <param name="p_strEndapply_dat">��ѯ��������������ʱ��</param>
        /// <param name="p_strAppid_chr">��ѯ����������</param>
        /// <param name="p_typeid">Ʊ������ 0-��ͨ 1-����Ʊ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngGetApplyInvoice(string p_strStartapply_dat, string p_strEndapply_dat, string p_strAppuserid_chr, int p_typeid, out clsT_opr_opinvoiceman_VO[] p_objResultArr)
        {
            //���ɲ�ѯ����
            string strQueryCondition = "";
            if (p_strStartapply_dat.Trim() != "")
                strQueryCondition += " AND trunc(Apply_dat) >=TO_DATE('" + p_strStartapply_dat.Trim() + "','yyyy-mm-dd') ";// hh24:mi:ss 
            if (p_strEndapply_dat.Trim() != "")
                strQueryCondition += " AND trunc(Apply_dat) <=TO_DATE('" + p_strEndapply_dat.Trim() + "','yyyy-mm-dd') ";// hh24:mi:ss 
            if (p_strAppuserid_chr.Trim() != "")
                strQueryCondition += " AND Appuserid_chr = '" + p_strAppuserid_chr.Trim() + "' "; //ֱ�Ӳ�ѯ
            //strQueryCondition += " AND Appuserid_chr like '%" + p_strAppuserid_chr.Trim() + "%' ";//ģ����ѯ
            if (p_typeid == 0)
            {
                strQueryCondition += " and invoicetypeid_int = " + p_typeid;
            }
            else
            {
                strQueryCondition += " and (invoicetypeid_int = 1 or invoicetypeid_int = 2)";
            }

            return m_lngGetApplyInvoice(strQueryCondition, out p_objResultArr);
            /*
            SELECT a.*, b.lastname_vchr AS appusername_chr,
       c.lastname_vchr AS operatorname_chr,
       d.lastname_vchr AS cancelusername_chr
  FROM t_opr_opinvoiceman a,
       t_bse_employee b,
       t_bse_employee c,
       t_bse_employee d
 WHERE a.appuserid_chr = b.empid_chr(+)
   AND a.operatorid_chr = c.empid_chr(+)
   AND a.canceluserid_chr = d.empid_chr(+)
   AND appuserid_chr = '0000001' 
            */
        }
        /// <summary>
        /// ���ݷ�Ʊ������ˮ�Ų��Ҷ�Ӧ��¼��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppid_chr">��Ʊ������ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngGetApplyInvoice(string p_strAppid_chr, out clsT_opr_opinvoiceman_VO p_objResult)
        {
            p_objResult = null;
            //���ɲ�ѯ����
            string strQueryCondition = " And appid_chr='" + p_strAppid_chr.Trim() + "' ";
            clsT_opr_opinvoiceman_VO[] p_objResultArr;
            long iReturn = m_lngGetApplyInvoice(strQueryCondition, out p_objResultArr);
            if (iReturn > 0 && p_objResultArr.Length > 0)
                p_objResult = p_objResultArr[0];
            return iReturn;
        }
        /// <summary>
        /// ��ѯ����ķ�Ʊ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strQueryCondition">��ѯ����</param>
        /// <param name="p_objResultArr"></param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngGetApplyInvoice(string p_strQueryCondition, out clsT_opr_opinvoiceman_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_opinvoiceman_VO[0];
            long lngRes = 0;

            string strSQL = "SELECT appid_chr, invoicenofrom_vchr, invoicenoto_vchr," +
                //" (invoicenoto_vchr-invoicenofrom_vchr)invoicenumber_int," + //��Ʊ����
                " apply_dat, appuserid_chr," +
                " (select LastName_vchr FROM T_BSE_EMPLOYEE WHERE EMPID_CHR=t_opr_opinvoiceman.appuserid_chr) appusername_chr," + //������
                " operatorid_chr, " +
                " (select LastName_vchr FROM T_BSE_EMPLOYEE WHERE EMPID_CHR=t_opr_opinvoiceman.operatorid_chr) operatorname_chr," + //������
                " canceluserid_chr, " +
                " (select LastName_vchr FROM T_BSE_EMPLOYEE WHERE EMPID_CHR=t_opr_opinvoiceman.canceluserid_chr) cancelusername_chr," + //������
                " cancel_dat, status_int, invoicetypeid_int" +
                " FROM t_opr_opinvoiceman WHERE 1=1 " + p_strQueryCondition;

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_opinvoiceman_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_opinvoiceman_VO();
                        p_objResultArr[i1].m_strAPPID_CHR = dtbResult.Rows[i1]["APPID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVOICENOFROM_VCHR = dtbResult.Rows[i1]["INVOICENOFROM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVOICENOTO_VCHR = dtbResult.Rows[i1]["INVOICENOTO_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["APPLY_DAT"] != null && dtbResult.Rows[i1]["APPLY_DAT"].ToString().Trim() != "")
                            p_objResultArr[i1].m_strAPPLY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["APPLY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        else
                            p_objResultArr[i1].m_strAPPLY_DAT = "";
                        p_objResultArr[i1].m_strAPPUSERID_CHR = dtbResult.Rows[i1]["APPUSERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPUSERNAME_CHR = dtbResult.Rows[i1]["appusername_chr"].ToString().Trim();//������
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOPERATORNAME_CHR = dtbResult.Rows[i1]["operatorname_chr"].ToString().Trim();//������
                        p_objResultArr[i1].m_strCANCELUSERID_CHR = dtbResult.Rows[i1]["CANCELUSERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCANCELUSERNAME_CHR = dtbResult.Rows[i1]["cancelusername_chr"].ToString().Trim();//������
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        if (dtbResult.Rows[i1]["CANCEL_DAT"] != null && dtbResult.Rows[i1]["CANCEL_DAT"].ToString().Trim() != "")
                            p_objResultArr[i1].m_strCANCEL_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CANCEL_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        else
                            p_objResultArr[i1].m_strCANCEL_DAT = "";
                        p_objResultArr[i1].intInvoiceTypeFlag = Convert.ToInt32(dtbResult.Rows[i1]["invoicetypeid_int"].ToString().Trim());
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

        #region ��ȡԱ����ˮ��	-����ְ����
        /// <summary>
        /// ��ȡԱ����ˮ��	-����ְ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeNO">ְ����</param>
        /// <param name="p_strEmployeeID">ְ����ˮ�� [out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeIDByNO(string p_strEmployeeNO, out string p_strEmployeeID)
        {
            p_strEmployeeID = "";
            DataTable dtResult = new DataTable();
            long lngResult = m_lngGetApplyName(p_strEmployeeNO, out dtResult);
            if (lngResult > 0 && dtResult.Rows.Count > 0 && dtResult.Rows[0]["EMPID_CHR"] != null)
                p_strEmployeeID = dtResult.Rows[0]["EMPID_CHR"].ToString();
            return lngResult;
        }
        #endregion
        #region ��ȡԱ������	-���ݹ���				2004-8-23
        /// <summary>
        /// ���ݹ������Ա������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strNO">����</param>
        /// <param name="p_strName">���ơ�[out������]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        /// <remarks>ע�⣺p_strApplyNameΪ�ַ�����</remarks>
        [AutoComplete]
        public long m_lngGetApplyName(string p_strNO, out string p_strName)
        {
            p_strName = "";
            DataTable dtResult = new DataTable();
            long lngResult = m_lngGetApplyName(p_strNO, out dtResult);
            if (lngResult > 0 && dtResult.Rows.Count > 0)
                p_strName = dtResult.Rows[0]["AppuserName_chr"].ToString();
            return lngResult;
        }
        /// <summary>
        /// ���ݹ������Ա������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeNO">����</param>
        /// <param name="p_dtResult">���š����Ʊ�[out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        /// <remarks>ע�⣺p_dtResultΪDataTable��</remarks>
        [AutoComplete]
        public long m_lngGetApplyName(string p_strEmployeeNO, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;
            string strSQL = "select EMPID_CHR,Empno_chr As Appuserid_chr,LastName_vchr AS AppuserName_chr FROM T_BSE_EMPLOYEE WHERE Trim(Empno_chr) = '" + p_strEmployeeNO.Trim() + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��ȡְ������ -������ˮ��
        /// <summary>
        /// ������ˮ�����Ա������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strEmployeeName">ְ�����ơ�[out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByID(string p_strID, out string p_strEmployeeName)
        {
            long lngRes = 0;
            p_strEmployeeName = "";
            string strSQL = "select LastName_vchr AS EmployeeName FROM T_BSE_EMPLOYEE WHERE Trim(EMPID_CHR) = '" + p_strID.Trim() + "'";

            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0 && dtResult.Rows[0]["EmployeeName"] != null)
                {
                    p_strEmployeeName = dtResult.Rows[0]["EmployeeName"].ToString();
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region	��鷢Ʊ�����Ƿ��Ѿ�������				2004-8-23	[ע�⣺�Ѿ����ϵķ�Ʊ������������]
        /// <summary>
        /// ��鷢Ʊ�����Ƿ��Ѿ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMinInvoiceNo">��ʼ��Ʊ��</param>
        /// <param name="p_strMaxInvoiceNo">������Ʊ��</param>
        /// <param name="p_typeid">Ʊ������ 0-��ͨ 1-����Ʊ��</param>
        /// <param name="IsUsed">�Ƿ��õı�־ [out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        /// <remarks>
        /// ע�⣺
        ///		�������������Ĭ�����Ѿ�ռ�ã�
        ///		�� IsUsed = true
        /// </remarks>
        [AutoComplete]
        public long m_lngCheckInvoiceNOIsUsed(string p_strMinInvoiceNo, string p_strMaxInvoiceNo, int p_typeid, out bool p_blnIsUsed)
        {
            p_blnIsUsed = true;
            long lngRes = 0;

            /* Get��ѯ�����ַ��� 
             * ���ȣ�������û�б�������ϵķ�Ʊ��	[״̬����-��������-����]
             * A1����ʼ���ֶΡ�A2�������ֶΡ�Min��ʼ�Ų�����Max�����Ų���
             * Min <= A2 <= Max
             * Min >= A1 AND Max <= A2
             * Min <= A1 <= Max
            */

            string strSQL = @"select appid_chr
  from t_opr_opinvoiceman
 where invoicenofrom_vchr >= ?
   and invoicenofrom_vchr <= ?
   and invoicetypeid_int = ?";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strMinInvoiceNo;
                objDPArr[1].Value = p_strMaxInvoiceNo;
                objDPArr[2].Value = p_typeid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                objHRPSvc.Dispose();
                if (dtResult.Rows.Count > 0)
                    p_blnIsUsed = true;
                else
                    p_blnIsUsed = false;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����Ӧ��Ʊ������ˮ���Ƿ�����
        /// <summary>
        /// ����Ӧ��Ʊ������ˮ���Ƿ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppid_chr">��Ʊ������ˮ��</param>
        /// <param name="p_blnIsUsed">�Ƿ����� [out ����]</param>
        /// <returns>���ز����Ƿ�ɹ�����[С�ڵ��ڣ������ɹ������ڣ����ɹ�]</returns>
        [AutoComplete]
        public long m_lngCheckInvoiceNOIsCancel(string p_strAppid_chr, out bool p_blnIsUsed)
        {
            p_blnIsUsed = true;
            long lngRes = 0;

            /* Get��ѯ�����ַ���*/
            string strQueryCondition = " status_int = 1  AND rownum<= 1 ";
            strQueryCondition += " AND appid_chr='" + p_strAppid_chr.Trim() + "' ";

            string strSQL = "SELECT appid_chr " +
                " FROM t_opr_opinvoiceman WHERE " + strQueryCondition;
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (dtResult.Rows.Count > 0)
                    p_blnIsUsed = true;
                else
                    p_blnIsUsed = false;
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //���﷢Ʊ�˻�
        #region ��÷�Ʊ��Ϣ				2004-8-27
        /// <summary>
        /// ���ݷ�Ʊ�Ż�����ﴦ����Ʊ��Ϣ [������Ч�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoByNoForReturn(string p_strINVOICENO_VCHR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            p_objResult = new clsT_opr_outpatientrecipeinv_VO();
            long lngRes = 0;
            string strSQL = @"select a.invoiceno_vchr,
                                      a.outpatrecipeid_chr,
                                      a.invdate_dat,
                                      a.acctsum_mny,
                                      a.sbsum_mny,
                                      a.opremp_chr,
                                      a.recordemp_chr,
                                      a.recorddate_dat,
                                      a.status_int,
                                      a.seqid_chr,
                                      a.balanceemp_chr,
                                      a.balance_dat,
                                      a.balanceflag_int,
                                      a.totalsum_mny,
                                      a.paytype_int,
                                      a.patientid_chr,
                                      a.patientname_chr,
                                      a.deptid_chr,
                                      a.deptname_chr,
                                      a.doctorid_chr,
                                      a.doctorname_chr,
                                      a.confirmemp_chr,
                                      a.paytypeid_chr,
                                      a.internalflag_int,
                                      a.baseseqid_chr,
                                      a.groupid_chr,
                                      a.confirmdeptid_chr,
                                      a.split_int,
                                      a.regno_chr,
                                      a.chargedeptid_chr
                                 from t_opr_outpatientrecipeinv a
                                where a.invoiceno_vchr not in
                                      (select b.invoiceno_vchr
                                         from t_opr_outpatientrecipeinv b
                                        where b.invoiceno_vchr in
                                              (select c.invoiceno_vchr
                                                 from t_opr_outpatientrecipeinv c
                                                where c.seqid_chr = ?)
                                        group by b.invoiceno_vchr
                                       having count(b.invoiceno_vchr) = 2)
                                  and a.status_int = 1
                                  and a.seqid_chr = ?";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                paraArr[0].Value = p_strINVOICENO_VCHR.Trim();
                paraArr[1].Value = p_strINVOICENO_VCHR.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, paraArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_outpatientrecipeinv_VO();
                    p_objResult.m_strINVOICENO_VCHR = dtbResult.Rows[0]["INVOICENO_VCHR"].ToString().Trim();
                    p_objResult.m_strOUTPATRECIPEID_CHR = dtbResult.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    p_objResult.m_strINVDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["INVDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_dblACCTSUM_MNY = Convert.ToDouble(dtbResult.Rows[0]["ACCTSUM_MNY"].ToString().Trim());
                    p_objResult.m_dblSBSUM_MNY = Convert.ToDouble(dtbResult.Rows[0]["SBSUM_MNY"].ToString().Trim());
                    p_objResult.m_strOPREMP_CHR = dtbResult.Rows[0]["OPREMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDEMP_CHR = dtbResult.Rows[0]["RECORDEMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["RECORDDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strSEQID_CHR = dtbResult.Rows[0]["SEQID_CHR"].ToString().Trim();
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
        /// ��������Ż�÷�Ʊ��Ϣ [������Ч�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_NO_STR">����� [�����λ]</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoBySeqidForReturn(string p_NO_STR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            p_objResult = new clsT_opr_outpatientrecipeinv_VO();
            long lngRes = 0;
            //			//ȷ�������Ϊ3λ   [ϵͳ��ˮ�� ������ʽ��20040812000]
            //			while (p_NO_STR.Trim().Length < 3)
            //			{
            //				p_NO_STR = "0" + p_NO_STR.Trim();
            //			}
            //			p_NO_STR = System.DateTime.Now.ToString("yyyyMMdd") + p_NO_STR;

            string strSQL = @"select invoiceno_vchr, outpatrecipeid_chr, invdate_dat, acctsum_mny, sbsum_mny, 
opremp_chr, recordemp_chr, recorddate_dat, status_int, seqid_chr, balanceemp_chr, balance_dat, balanceflag_int, 
totalsum_mny, paytype_int, patientid_chr, patientname_chr, deptid_chr, deptname_chr, doctorid_chr, doctorname_chr, 
confirmemp_chr, paytypeid_chr, internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, 
chargedeptid_chr from t_opr_outpatientrecipeinv where seqid_chr= '" + p_NO_STR + "' And status_int = 1";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_outpatientrecipeinv_VO();
                    p_objResult.m_strINVOICENO_VCHR = dtbResult.Rows[0]["INVOICENO_VCHR"].ToString().Trim();
                    p_objResult.m_strOUTPATRECIPEID_CHR = dtbResult.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    p_objResult.m_strINVDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["INVDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_dblACCTSUM_MNY = Convert.ToInt32(dtbResult.Rows[0]["ACCTSUM_MNY"].ToString().Trim());
                    p_objResult.m_dblSBSUM_MNY = Convert.ToInt32(dtbResult.Rows[0]["SBSUM_MNY"].ToString().Trim());
                    p_objResult.m_strOPREMP_CHR = dtbResult.Rows[0]["OPREMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDEMP_CHR = dtbResult.Rows[0]["RECORDEMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["RECORDDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strSEQID_CHR = dtbResult.Rows[0]["SEQID_CHR"].ToString().Trim();
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
        #region ��Ʊ�˻�				2004-8-27
        /// <summary>
        /// ��ƱID
        /// </summary>
        private string strSEQID = "";
        /// <summary>
        /// ��Ʊ�˻�[��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strOPREMP_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngReturnTicket(string p_strINVOICENO_VCHR, string p_strOPREMP_CHR, ref string Seqid, int intFlag)
        {
            clsOPChargeSvc obj = new clsOPChargeSvc();
            Seqid = DateTime.Now.ToString("yyMMddHHmmssffffff");
            strSEQID = Seqid;
            long lngRes = 0;
            string strSQL = "";
            if (intFlag == 1)
            {
                strSQL = @"insert into t_opr_outpatientrecipeinv (invoiceno_vchr,outpatrecipeid_chr,
invdate_dat,acctsum_mny,sbsum_mny,opremp_chr,recordemp_chr,recorddate_dat,
status_int,seqid_chr,totalsum_mny
,paytype_int,patientid_chr,patientname_chr,deptid_chr,deptname_chr,doctorid_chr,
doctorname_chr,confirmemp_chr,paytypeid_chr,  internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, chargedeptid_chr,isvouchers_int,totaldiffcost_mny) 
select invoiceno_vchr,outpatrecipeid_chr,
to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + @"','yyyy-mm-dd'),-acctsum_mny,-sbsum_mny,opremp_chr,'" + p_strOPREMP_CHR + @"',to_date('" + DateTime.Now.ToString() + @"','yyyy-mm-dd hh24:mi:ss'),
'2','" + Seqid + @"',-totalsum_mny
,paytype_int,patientid_chr,patientname_chr,deptid_chr,deptname_chr,doctorid_chr,
doctorname_chr,confirmemp_chr,paytypeid_chr,  internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, '', isvouchers_int,-totaldiffcost_mny
from t_opr_outpatientrecipeinv where seqid_chr ='" + p_strINVOICENO_VCHR + "'";//��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ
            }
            else
            {
                strSQL = @"insert into t_opr_outpatientrecipeinv (invoiceno_vchr,outpatrecipeid_chr,
invdate_dat,acctsum_mny,sbsum_mny,opremp_chr,recordemp_chr,recorddate_dat,
status_int,seqid_chr,totalsum_mny
,paytype_int,patientid_chr,patientname_chr,deptid_chr,deptname_chr,doctorid_chr,
doctorname_chr,confirmemp_chr,paytypeid_chr,  internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, chargedeptid_chr,isvouchers_int,totaldiffcost_mny) 
select invoiceno_vchr,outpatrecipeid_chr,
to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + @"','yyyy-mm-dd'),-acctsum_mny,-sbsum_mny,opremp_chr,'" + p_strOPREMP_CHR + @"',to_date('" + DateTime.Now.ToString() + @"','yyyy-mm-dd hh24:mi:ss'),
'2','" + Seqid + @"',-totalsum_mny
,paytype_int,patientid_chr,patientname_chr,deptid_chr,deptname_chr,doctorid_chr,
doctorname_chr,confirmemp_chr,paytypeid_chr,  internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, chargedeptid_chr, isvouchers_int, -totaldiffcost_mny
from t_opr_outpatientrecipeinv where seqid_chr ='" + p_strINVOICENO_VCHR + "'";//��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = @"insert into t_opr_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny)
                           select '" + Seqid + @"',
                                  paytype_int,
                                  null,
                                  null,
                                  -sbsum_mny,
                                  0
                            from  t_opr_outpatientrecipeinv
                            where seqid_chr = '" + p_strINVOICENO_VCHR + "'";
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = @"UPDATE t_opr_outpatientrecipe
   SET pstauts_int = -2
 WHERE outpatrecipeid_chr IN (
          SELECT a.outpatrecipeid_chr
            FROM t_opr_reciperelation a, t_opr_outpatientrecipeinv b
           WHERE a.seqid = b.outpatrecipeid_chr
             AND b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";

                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    this.m_mthInsertData("T_OPR_OUTPATIENTRECIPEINVDE", p_strINVOICENO_VCHR, "-", objHRPSvc);
                    this.m_mthInsertData("T_OPR_OUTPATIENTRECIPESUMDE", p_strINVOICENO_VCHR, "-", objHRPSvc);

                    //���ݴ����Ÿ��¼��顢������Ŀ�շѱ�־(�˿�)
                    strSQL = @"update t_opr_attachrelation 
									set status_int = 2 
								where sourceitemid_vchr in (
															select a.outpatrecipeid_chr
															  from t_opr_reciperelation a, t_opr_outpatientrecipeinv b
															 where a.seqid = b.outpatrecipeid_chr
															   and b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    //�˿�ʱ��ͨ�����뵥��дΪ���˿�
                    strSQL = @"update ar_common_apply 
								set chargestatus_int = 3 
								where applyid in (													
													select distinct c.attachid_vchr
													  from t_opr_reciperelation a, 
														   t_opr_outpatientrecipeinv b,
														   t_tmp_outpatienttestrecipede c
													 where a.seqid = b.outpatrecipeid_chr
													   and a.outpatrecipeid_chr = c.outpatrecipeid_chr
													   and b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    //�˿�ʱ������ѽɷ�״̬��Ϊ�˿�
                    strSQL = @"update t_pe_register 
									set chargeflag_int = 0 
								where regno_chr in (
													select b.regno_chr
													  from t_opr_outpatientrecipeinv b
													 where b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    // ���µ������뵥�˷�
                    strSQL = @"update eafInterface
                                   set chargeStatus = 2
                                 where requisitionID in (select a.appId
                                                           from eafApplication a
                                                          inner join t_opr_outpatientrecipeinv b
                                                             on a.recipeId = b.outpatrecipeid_chr
                                                          where b.seqid_chr = '{0}')";
                    lngRes = objHRPSvc.DoExcute(string.Format(strSQL, p_strINVOICENO_VCHR));
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

            //�����˻ط�Ʊ�����ۼ�¼  [�����ۼ� 1-����·�Ʊ 2-���Ϸ�Ʊ 3-�˻ط�Ʊ 4-�ָ���Ʊ]
            //			return m_lngAddNewT_opr_outpatientrecipeinvop(p_strINVOICENO_VCHR,p_strOPREMP_CHR,3);
        }
        #endregion

        #region ��Ʊ�˻ؼ���Ƿ��Ѿ���ҩ
        /// <summary>
        /// ��Ʊ�˻ؼ���Ƿ��Ѿ���ҩ 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strStatus">״̬</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngReturnTicketCheckOutSendMed(string p_strINVOICENO_VCHR, out string p_strStatus)
        {
            p_strStatus = string.Empty;
            clsOPChargeSvc obj = new clsOPChargeSvc();
            long lngRes = 0;
            string strSQl1 = @"select a.setstatus_int from t_sys_setting a where a.setid_chr='0401'";

            string strSQL = @"select a.outpatrecipeid_chr, b.pstatus_int
  from  t_opr_recipesend b,
       (select t.outpatrecipeid_chr
          from t_opr_outpatientrecipeinv t
         where t.status_int <> 0 and t.seqid_chr = ?) a,      
       t_opr_recipesendentry c
 where a.outpatrecipeid_chr = c.outpatrecipeid_chr
   and c.sid_int = b.sid_int
 and b.pstatus_int<>1";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc = new clsHRPTableService();
                DataTable dtbTmp = null;
                string strReductFlag = string.Empty;
                lngRes = objSvc.lngGetDataTableWithoutParameters(strSQl1, ref dtbTmp);
                if (dtbTmp != null && dtbTmp.Rows.Count > 0)
                {
                    strReductFlag = dtbTmp.Rows[0][0].ToString().Trim();
                }
                else
                {
                    strReductFlag = "0";
                }
                System.Data.IDataParameter[] objArr = null;
                objSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = p_strINVOICENO_VCHR;
                DataTable objDt = null;
                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref objDt, objArr);
                if (lngRes > 0)
                {
                    if (objDt == null || objDt.Rows.Count == 0)
                    {
                        p_strStatus = "1";//δ��ҩ
                    }
                    else
                    {
                        int intRowCount = objDt.Rows.Count;
                        for (int intI = 0; intI < intRowCount; intI++)
                        {
                            if (!string.IsNullOrEmpty(strReductFlag))
                            {
                                if (strReductFlag == "1")
                                {
                                    if (objDt.Rows[intI]["pstatus_int"].ToString().Trim() == "2")
                                    {
                                        p_strStatus = "1";
                                        continue;
                                    }
                                }
                            }
                            strSQL = @"select n.flag_int from t_opr_outpatientrecipeinv m,t_opr_returnmed n
where m.outpatrecipeid_chr=n.outpatrecipeid_chr
and m.seqid_chr=?";
                            objArr = null;
                            objSvc.CreateDatabaseParameter(1, out objArr);
                            objArr[0].DbType = DbType.String;
                            objArr[0].Value = p_strINVOICENO_VCHR;
                            DataTable objDt1 = null;
                            lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref objDt1, objArr);
                            if (lngRes > 0 && objDt1 != null && objDt1.Rows.Count > 0)
                            {
                                string strTmp = objDt1.Rows[0][0].ToString();
                                if (strTmp.Trim() == "2")
                                {
                                    p_strStatus = "2";//�Ѿ���ҩ
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(p_strStatus))
                        {
                            p_strStatus = "3";//�Ѿ���ҩ
                        }
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

        //���﷢Ʊ�ָ�
        #region ��÷�Ʊ��Ϣ				2004-8-27
        /// <summary>
        /// ���ݷ�Ʊ�Ż�����ﴦ����Ʊ��Ϣ [�Ѿ���Ʊ�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoByNoForResume(string p_strINVOICENO_VCHR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            p_objResult = new clsT_opr_outpatientrecipeinv_VO();
            long lngRes = 0;
            string strSQL = @"select invoiceno_vchr, outpatrecipeid_chr, invdate_dat, acctsum_mny, sbsum_mny, 
opremp_chr, recordemp_chr, recorddate_dat, status_int, seqid_chr, balanceemp_chr, balance_dat, balanceflag_int, 
totalsum_mny, paytype_int, patientid_chr, patientname_chr, deptid_chr, deptname_chr, doctorid_chr, doctorname_chr, 
confirmemp_chr, paytypeid_chr, internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, 
chargedeptid_chr from t_opr_outpatientrecipeinv where SEQID_CHR= '" + p_strINVOICENO_VCHR + "' And status_int = 2";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_outpatientrecipeinv_VO();
                    p_objResult.m_strINVOICENO_VCHR = dtbResult.Rows[0]["INVOICENO_VCHR"].ToString().Trim();
                    p_objResult.m_strOUTPATRECIPEID_CHR = dtbResult.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    p_objResult.m_strINVDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["INVDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_dblACCTSUM_MNY = Convert.ToDouble(dtbResult.Rows[0]["ACCTSUM_MNY"].ToString().Trim());
                    p_objResult.m_dblSBSUM_MNY = Convert.ToDouble(dtbResult.Rows[0]["SBSUM_MNY"].ToString().Trim());
                    p_objResult.m_strOPREMP_CHR = dtbResult.Rows[0]["OPREMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDEMP_CHR = dtbResult.Rows[0]["RECORDEMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["RECORDDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strSEQID_CHR = dtbResult.Rows[0]["SEQID_CHR"].ToString().Trim();
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
        /// ��������Ż�÷�Ʊ��Ϣ [�Ѿ���Ʊ�ķ�Ʊ ��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_NO_STR">����� [�����λ]</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoBySeqidForResume(string p_NO_STR, out clsT_opr_outpatientrecipeinv_VO p_objResult)
        {
            p_objResult = new clsT_opr_outpatientrecipeinv_VO();
            long lngRes = 0;
            //ȷ�������Ϊ3λ   [ϵͳ��ˮ�� ������ʽ��20040812000]
            //			while (p_NO_STR.Trim().Length < 3)
            //			{
            //				p_NO_STR = "0" + p_NO_STR.Trim();
            //			}
            //			p_NO_STR = System.DateTime.Now.ToString("yyyyMMdd") + p_NO_STR;

            string strSQL = @"select invoiceno_vchr, outpatrecipeid_chr, invdate_dat, acctsum_mny, sbsum_mny, 
opremp_chr, recordemp_chr, recorddate_dat, status_int, seqid_chr, balanceemp_chr, balance_dat, balanceflag_int, 
totalsum_mny, paytype_int, patientid_chr, patientname_chr, deptid_chr, deptname_chr, doctorid_chr, doctorname_chr, 
confirmemp_chr, paytypeid_chr, internalflag_int, baseseqid_chr, groupid_chr, confirmdeptid_chr, split_int, regno_chr, 
chargedeptid_chr from t_opr_outpatientrecipeinv where seqid_chr= '" + p_NO_STR + "' And status_int = 2";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_outpatientrecipeinv_VO();
                    p_objResult.m_strINVOICENO_VCHR = dtbResult.Rows[0]["INVOICENO_VCHR"].ToString().Trim();
                    p_objResult.m_strOUTPATRECIPEID_CHR = dtbResult.Rows[0]["OUTPATRECIPEID_CHR"].ToString().Trim();
                    p_objResult.m_strINVDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["INVDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_dblACCTSUM_MNY = Convert.ToInt32(dtbResult.Rows[0]["ACCTSUM_MNY"].ToString().Trim());
                    p_objResult.m_dblSBSUM_MNY = Convert.ToInt32(dtbResult.Rows[0]["SBSUM_MNY"].ToString().Trim());
                    p_objResult.m_strOPREMP_CHR = dtbResult.Rows[0]["OPREMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDEMP_CHR = dtbResult.Rows[0]["RECORDEMP_CHR"].ToString().Trim();
                    p_objResult.m_strRECORDDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["RECORDDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strSEQID_CHR = dtbResult.Rows[0]["SEQID_CHR"].ToString().Trim();
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
        #region ��Ʊ�ָ�				2004-8-27
        /// <summary>
        /// ��Ʊ�ָ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strOPREMP_CHR">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngResumeTicket(string p_strINVOICENO_VCHR, string p_strOPREMP_CHR, ref string Seqid)
        {

            clsOPChargeSvc obj = new clsOPChargeSvc();
            Seqid = DateTime.Now.ToString("yyMMddHHmmssffffff");
            strSEQID = Seqid;
            long lngRes = 0;

            string strSQL = @"insert into t_opr_outpatientrecipeinv (INVOICENO_VCHR,OUTPATRECIPEID_CHR,
                            INVDATE_DAT,ACCTSUM_MNY,SBSUM_MNY,OPREMP_CHR,RECORDEMP_CHR,RECORDDATE_DAT,
                            STATUS_INT,SEQID_CHR,TOTALSUM_MNY
                            ,PAYTYPE_INT,PATIENTID_CHR,PATIENTNAME_CHR,DEPTID_CHR,DEPTNAME_CHR,DOCTORID_CHR,
                            DOCTORNAME_CHR,CONFIRMEMP_CHR,PAYTYPEID_CHR, INTERNALFLAG_INT, BASESEQID_CHR, GROUPID_CHR, confirmdeptid_chr, split_int, regno_chr,chargedeptid_chr) 
                            select INVOICENO_VCHR,OUTPATRECIPEID_CHR,
                            to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + @"','yyyy-mm-dd'),-ACCTSUM_MNY,-SBSUM_MNY,OPREMP_CHR,'" + p_strOPREMP_CHR + @"',to_date('" + DateTime.Now.ToString() + @"','yyyy-mm-dd hh24:mi:ss'),
                            '3','" + Seqid + @"',-TOTALSUM_MNY
                            ,PAYTYPE_INT,PATIENTID_CHR,PATIENTNAME_CHR,DEPTID_CHR,DEPTNAME_CHR,DOCTORID_CHR,
                            DOCTORNAME_CHR,CONFIRMEMP_CHR,PAYTYPEID_CHR,  INTERNALFLAG_INT, BASESEQID_CHR, GROUPID_CHR, confirmdeptid_chr, split_int, regno_chr,chargedeptid_chr
                            from t_opr_outpatientrecipeinv where SEQID_CHR ='" + p_strINVOICENO_VCHR + "'";//��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = @"insert into t_opr_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny)
                           select '" + strSEQID + @"',
                                  paytype_int,
                                  null,
                                  null,
                                  -sbsum_mny,
                                  0
                            from  t_opr_outpatientrecipeinv
                            where seqid_chr = '" + p_strINVOICENO_VCHR + "'";
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = @"UPDATE t_opr_outpatientrecipe
                           SET pstauts_int = 3
                         WHERE outpatrecipeid_chr IN (
                                  SELECT a.outpatrecipeid_chr
                                    FROM t_opr_reciperelation a, t_opr_outpatientrecipeinv b
                                   WHERE a.seqid = b.outpatrecipeid_chr
                                     AND b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    this.m_mthInsertData("T_OPR_OUTPATIENTRECIPEINVDE", p_strINVOICENO_VCHR, "-", objHRPSvc);
                    this.m_mthInsertData("T_OPR_OUTPATIENTRECIPESUMDE", p_strINVOICENO_VCHR, "-", objHRPSvc);
                    //					
                    //���ݴ����Ÿ��¼��顢������Ŀ�շѱ�־(�ָ���>�շ�)
                    strSQL = @"update t_opr_attachrelation 
									set status_int = 1 
								where sourceitemid_vchr in (
															select a.outpatrecipeid_chr
															  from t_opr_reciperelation a, t_opr_outpatientrecipeinv b
															 where a.seqid = b.outpatrecipeid_chr
															   and b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    //�ָ�ʱ��ͨ�����뵥��дΪ���շ�
                    strSQL = @"update ar_common_apply 
								set chargestatus_int = 2
								where applyid in (													
													select distinct c.attachid_vchr
													  from t_opr_reciperelation a, 
														   t_opr_outpatientrecipeinv b,
														   t_tmp_outpatienttestrecipede c
													 where a.seqid = b.outpatrecipeid_chr
													   and a.outpatrecipeid_chr = c.outpatrecipeid_chr
													   and b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    //�ָ�ʱ������ѽɷ�״̬��Ϊ�տ�
                    strSQL = @"update t_pe_register 
									set chargeflag_int = 1
								where regno_chr in (
													select b.regno_chr
													  from t_opr_outpatientrecipeinv b
													 where b.seqid_chr = '" + p_strINVOICENO_VCHR + "')";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    // ���µ������뵥�շ�
                    strSQL = @"update eafInterface
                                   set chargeStatus = 1
                                 where requisitionID in (select a.appId
                                                           from eafApplication a
                                                          inner join t_opr_outpatientrecipeinv b
                                                             on a.recipeId = b.outpatrecipeid_chr
                                                          where b.seqid_chr = '{0}')";
                    lngRes = objHRPSvc.DoExcute(string.Format(strSQL, p_strINVOICENO_VCHR));
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes; ;
        }
        #endregion
        #region
        [AutoComplete]
        private void m_mthInsertData(string strTable, string Invoice, string flag, com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc)
        {
            string strSQL = "select * from " + strTable + " where SEQID_CHR ='" + Invoice + "'";
            DataTable dt = new DataTable();
            long l = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            if (l > 0 && dt.Rows.Count > 0)
            {
                //				string ID;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //					l =objHRPSvc.lngGenerateID(15,"SEQID_CHR",strTable,out ID);
                    if (l > 0)
                    {
                        decimal temp = 0;
                        decimal temp2 = 0;
                        decimal temp3 = 0;
                        try
                        {
                            //temp=Math.Abs(decimal.Parse(dt.Rows[i]["TOLFEE_MNY"].ToString()));
                            //temp2=Math.Abs(decimal.Parse(dt.Rows[i]["SBSUM_MNY"].ToString()));
                            temp = decimal.Parse(dt.Rows[i]["TOLFEE_MNY"].ToString());
                            temp2 = decimal.Parse(dt.Rows[i]["SBSUM_MNY"].ToString());
                            if (strTable.ToLower().Trim() == "t_opr_outpatientrecipeinvde")
                            {
                                temp3 = decimal.Parse(dt.Rows[i]["factsum"].ToString());
                            }
                        }
                        catch
                        {

                        }
                        if (flag.Trim() != "")
                        {
                            temp = temp * -1;
                            temp2 = temp2 * -1;
                            temp3 = temp3 * -1;
                        }

                        if (strTable.ToLower().Trim() == "t_opr_outpatientrecipeinvde")
                        {
                            strSQL = "insert into " + strTable + "(ITEMCATID_CHR,TOLFEE_MNY,INVOICENO_VCHR,SEQID_CHR,SBSUM_MNY, factsum) values ('" + dt.Rows[i]["ITEMCATID_CHR"].ToString().Trim() + "','" + temp.ToString("0.00") + "',(select INVOICENO_VCHR from  t_opr_outpatientrecipeinv where SEQID_CHR ='" + Invoice + "'),'" + strSEQID + "', " + temp2.ToString() + ", " + temp3 + ")";
                        }
                        else
                        {
                            strSQL = "insert into " + strTable + "(ITEMCATID_CHR,TOLFEE_MNY,INVOICENO_VCHR,SEQID_CHR,SBSUM_MNY) values ('" + dt.Rows[i]["ITEMCATID_CHR"].ToString().Trim() + "','" + temp.ToString("0.00") + "',(select INVOICENO_VCHR from  t_opr_outpatientrecipeinv where SEQID_CHR ='" + Invoice + "'),'" + strSEQID + "', " + temp2.ToString() + ")";
                        }
                        l = objHRPSvc.DoExcute(strSQL);
                    }
                }
            }
        }
        #endregion
        #region ���ӷ�Ʊ������				2004-8-27
        /// <summary>
        /// ���ӷ�Ʊ������
        /// </summary>
        /// <param name="p_strINVOICENO_VCHR">��Ʊ��</param>
        /// <param name="p_strOPREMP_CHR">������ID</param>
        /// <param name="p_intState">[�����ۼ� 1-����·�Ʊ 2-���Ϸ�Ʊ 3-�˻ط�Ʊ 4-�ָ���Ʊ]</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewT_opr_outpatientrecipeinvop(string p_strINVOICENO_VCHR, string p_strOPREMP_CHR, int p_intState)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //���ӷ�Ʊ�����ۼ�¼  [�����ۼ� 1-����·�Ʊ 2-���Ϸ�Ʊ 3-�˻ط�Ʊ 4-�ָ���Ʊ]
            string strSQL = "INSERT INTO T_opr_outpatientrecipeinvop (INVOICENO_VCHR,SYS_DAT,OPREMP_CHR,OPRFLAG_INT) VALUES (?,?,?,?)";
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strINVOICENO_VCHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_strOPREMP_CHR;
                objLisAddItemRefArr[3].Value = p_intState;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region ���ݿ��Ų����Ʊ��
        /// <summary>
        /// ���ݿ��Ų����Ʊ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strCardID">����</param>
        /// <param name="dt"></param>
        /// <param name="flag">��־(Ϊ��չ��)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindInvoiceByCardID(string strCardID, out DataTable dt, int flag, int p_FindFlag)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = "";

            if (flag == 1)
            {
                strSQL = @"select a.invoiceno_vchr,
                                   c.repprninvono_vchr,
                                   a.outpatrecipeid_chr,
                                   a.invdate_dat,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.opremp_chr,
                                   a.recordemp_chr,
                                   a.recorddate_dat,
                                   a.status_int,
                                   a.seqid_chr,
                                   a.balanceemp_chr,
                                   a.balance_dat,
                                   a.balanceflag_int,
                                   a.totalsum_mny,
                                   a.paytype_int,
                                   a.patientid_chr,
                                   a.patientname_chr,
                                   a.deptid_chr,
                                   a.deptname_chr,
                                   a.doctorid_chr,
                                   a.doctorname_chr,
                                   a.confirmemp_chr,
                                   a.paytypeid_chr,
                                   a.internalflag_int,
                                   a.baseseqid_chr,
                                   a.groupid_chr,
                                   a.confirmdeptid_chr,
                                   a.split_int,
                                   a.regno_chr,
                                   a.chargedeptid_chr
                              from t_opr_outpatientrecipeinv a
                             inner join t_bse_patientcard b
                                on a.patientid_chr = b.patientid_chr
                              left join t_opr_invoicerepeatprint c
                                on a.seqid_chr = c.seqid_chr
                               and c.type_chr = '1'
                             where a.status_int = {0}
                               and b.patientcardid_chr = '{1}'";
                strSQL = string.Format(strSQL, flag, strCardID);
            }
            else
            {
                strSQL = @"select a.invoiceno_vchr,
                                   c.repprninvono_vchr,
                                   a.outpatrecipeid_chr,
                                   a.invdate_dat,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.opremp_chr,
                                   a.recordemp_chr,
                                   a.recorddate_dat,
                                   a.status_int,
                                   a.seqid_chr,
                                   a.balanceemp_chr,
                                   a.balance_dat,
                                   a.balanceflag_int,
                                   a.totalsum_mny,
                                   a.paytype_int,
                                   a.patientid_chr,
                                   a.patientname_chr,
                                   a.deptid_chr,
                                   a.deptname_chr,
                                   a.doctorid_chr,
                                   a.doctorname_chr,
                                   a.confirmemp_chr,
                                   a.paytypeid_chr,
                                   a.internalflag_int,
                                   a.baseseqid_chr,
                                   a.groupid_chr,
                                   a.confirmdeptid_chr,
                                   a.split_int,
                                   a.regno_chr,
                                   a.chargedeptid_chr
                              from t_opr_outpatientrecipeinv a
                             inner join t_bse_patientcard b
                                on a.patientid_chr = b.patientid_chr
                              left join t_opr_invoicerepeatprint c
                                on a.seqid_chr = c.seqid_chr
                               and c.type_chr = '1'
                             where a.status_int = {0}
                               and b.patientcardid_chr = '{1}'
                               and a.invoiceno_vchr IN
                                   ((SELECT DISTINCT invoiceno_vchr
                                      FROM t_opr_outpatientrecipeinv
                                     GROUP BY invoiceno_vchr
                                    HAVING COUNT(invoiceno_vchr) = 2))";
                strSQL = string.Format(strSQL, flag, strCardID);
            }

            if (p_FindFlag == 1)
            {
                if (flag == 1)
                {
                    strSQL = @"select a.invoiceno_vchr,
                                      c.repprninvono_vchr, 
                                      a.outpatrecipeid_chr,
                                      a.invdate_dat,
                                      a.acctsum_mny,
                                      a.sbsum_mny,
                                      a.opremp_chr,
                                      a.recordemp_chr,
                                      a.recorddate_dat,
                                      a.status_int,
                                      a.seqid_chr,
                                      a.balanceemp_chr,
                                      a.balance_dat,
                                      a.balanceflag_int,
                                      a.totalsum_mny,
                                      a.paytype_int,
                                      a.patientid_chr,
                                      a.patientname_chr,
                                      a.deptid_chr,
                                      a.deptname_chr,
                                      a.doctorid_chr,
                                      a.doctorname_chr,
                                      a.confirmemp_chr,
                                      a.paytypeid_chr,
                                      a.internalflag_int,
                                      a.baseseqid_chr,
                                      a.groupid_chr,
                                      a.confirmdeptid_chr,
                                      a.split_int,
                                      a.regno_chr,
                                      a.chargedeptid_chr
                                    from t_opr_outpatientrecipeinv a
                               left join t_opr_invoicerepeatprint c
                                      on a.seqid_chr = c.seqid_chr
                                     and c.type_chr = '1'
                                where a.invoiceno_vchr not in
                                           (select b.invoiceno_vchr
                                              from t_opr_outpatientrecipeinv b
                                             where b.invoiceno_vchr like '" + strCardID + @"%'
                                             group by b.invoiceno_vchr
                                            having count(b.invoiceno_vchr) = 2)
                                    and a.invoiceno_vchr like '" + strCardID + @"%'
                                    and a.status_int =" + flag.ToString();
                }
                else
                {
                    strSQL = @"select a.invoiceno_vchr,
       c.repprninvono_vchr, 
       a.outpatrecipeid_chr,
       a.invdate_dat,
       a.acctsum_mny,
       a.sbsum_mny,
       a.opremp_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.status_int,
       a.seqid_chr,
       a.balanceemp_chr,
       a.balance_dat,
       a.balanceflag_int,
       a.totalsum_mny,
       a.paytype_int,
       a.patientid_chr,
       a.patientname_chr,
       a.deptid_chr,
       a.deptname_chr,
       a.doctorid_chr,
       a.doctorname_chr,
       a.confirmemp_chr,
       a.paytypeid_chr,
       a.internalflag_int,
       a.baseseqid_chr,
       a.groupid_chr,
       a.confirmdeptid_chr,
       a.split_int,
       a.regno_chr,
       a.chargedeptid_chr from t_opr_outpatientrecipeinv a  
                            left join t_opr_invoicerepeatprint c
                                      on a.seqid_chr = c.seqid_chr
                                     and c.type_chr = '1'    
                                        where a.invoiceno_vchr IN ((SELECT DISTINCT invoiceno_vchr
                                                                           FROM t_opr_outpatientrecipeinv
                                                                       GROUP BY invoiceno_vchr
                                                                         HAVING COUNT (invoiceno_vchr) = 2))
                                            and a.INVOICENO_VCHR like '" + strCardID + @"%'
                                            and a.STATUS_INT =" + flag.ToString();
                }
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        #region ��ȡ�����Ϣ
        [AutoComplete]
        public long m_mthGetInvoiceAuditingInfo(string strID, out DataTable dt, int flag)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select  a.baseseqid_chr, a.seqid_chr, a.status_int, a.cfempid_chr, a.cf_dat, b.lastname_vchr
  from t_opr_opri_confirm a, t_bse_employee b
 where a.cfempid_chr = b.empid_chr(+)
 and A.SEQID_CHR ='" + strID + "' and A.STATUS_INT = " + flag.ToString();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        #region ������Ʊ�Ƿ���ҩƷ(�С���ҩ)
        /// <summary>
        /// ������Ʊ�Ƿ���ҩƷ(�С���ҩ)
        /// </summary>
        /// <param name="p_strInvNo">��Ʊ��</param>
        /// <param name="p_blContians">����ֵ������ҩƷΪtrue������ҩƷΪfalse</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_CheckIsContainMed(string p_strInvNo, ref bool p_blContians)
        {
            long lngRes = -1;
            string SQL = @"select a.outpatrecipedeid_chr
                          from t_opr_outpatientcmrecipede a, t_opr_outpatientrecipeinv i
                         where a.outpatrecipeid_chr = i.outpatrecipeid_chr
                           and i.seqid_chr = ?
                        union all
                        select a.outpatrecipedeid_chr
                          from t_opr_outpatientpwmrecipede a, t_opr_outpatientrecipeinv i
                         where a.outpatrecipeid_chr = i.outpatrecipeid_chr
                           and i.seqid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param;
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].Value = p_strInvNo;
                param[1].Value = p_strInvNo;
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
                if (dt.Rows.Count > 0)
                {
                    p_blContians = true;
                }
                else
                {
                    p_blContians = false;
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
        #region ���������Ϣ
        [AutoComplete]
        public long m_mthAddInvoiceAuditingInfo(clsInvAuditing_VO objResult)
        {
            long lngRes = 0;
            string p_strID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //����һ���ļƻ���
            lngRes = objHRPSvc.lngGenerateID(20, "BASESEQID_CHR", "T_OPR_OPRI_CONFIRM", out p_strID);
            if (lngRes < 0)
                return -1;
            string strSQL = "Insert Into T_OPR_OPRI_CONFIRM " +
                "(BASESEQID_CHR,SEQID_CHR,STATUS_INT,CFEMPID_CHR,CF_DAT) Values " + "(?,?,?,?,to_date(?,'yyyy-mm-dd hh24:mi:ss'))";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strID;
                objLisAddItemRefArr[1].Value = objResult.strSEQID_CHR;
                objLisAddItemRefArr[2].Value = objResult.strSTATUS_INT;
                objLisAddItemRefArr[3].Value = objResult.strCFEMPID_CHR;
                objLisAddItemRefArr[4].Value = objResult.strCF_DAT;

                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��֤����

        [AutoComplete]
        public long m_mthGetEmployeeInfo(string strID, out DataTable dt, string strEx)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select empid_chr,
       begindate_dat,
       firstname_vchr,
       lastname_vchr,
       empidcard_chr,
       pycode_chr,
       sex_chr,
       educationallevel_chr,
       maritalstatus_chr,
       technicalrank_chr,
       languageability_vchr,
       birthdate_dat,
       officephone_vchr,
       homephone_vchr,
       mobile_vchr,
       officeaddress_vchr,
       officezip_chr,
       homeaddress_vchr,
       homezip_chr,
       email_vchr,
       contactname_vchr,
       contactphone_vchr,
       remark_vchr,
       status_int,
       deactivate_dat,
       shortname_chr,
       operatorid_chr,
       hasprescriptionright_chr,
       haspsychosisprescriptionright_,
       hasopiateprescriptionright_chr,
       isexpert_chr,
       expertfee_mny,
       isexternalexpert_chr,
       ancestoraddr_vchar,
       experience_vchr,
       psw_chr,
       deptcode_chr,
       technicallevel_chr,
       digitalsign_dta,
       extendid_vchr,
       isemployee_int,
       empno_chr,
       employeeidentity_int,
       empduty_int from t_bse_employee WHERE status_int = '1' AND empno_chr = '" + strID.Replace("'", "��") + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region �����ڲ����к��ж��Ƿ�Ϊ��Ʊ
        /// <summary>
        /// �����ڲ����к��ж��Ƿ�Ϊ��Ʊ
        /// </summary>
        /// <param name="seqid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnChecksplit(string seqid)
        {
            long lngRes = 0;
            bool blnRet = false;
            string SQL = "select split_int from t_opr_outpatientrecipeinv where seqid_chr = '" + seqid + "'";
            DataTable dtRecord = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtRecord.Rows.Count == 1)
            {
                if (dtRecord.Rows[0][0].ToString() == "1")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }
        #endregion

        #region �����ڲ����кŻ�ȡͬ��ַ�Ʊ����
        /// <summary>
        /// �����ڲ����кŻ�ȡͬ��ַ�Ʊ����
        /// </summary>
        /// <param name="invono"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetsplitinvoinfo(string seqid, out DataTable dtRecord)
        {
            dtRecord = new DataTable();
            long lngRes = 0;

            string SQL = @" select distinct a.invoiceno_vchr
                              from t_opr_outpatientrecipeinv a
                             where a.baseseqid_chr in (
                                                         select baseseqid_chr
                                                           from t_opr_outpatientrecipeinv
                                                          where seqid_chr = '" + seqid + "')";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
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

        #region ���ݷ�Ʊ�Ų����Ʊ��
        /// <summary>
        /// ���ݷ�Ʊ�Ų����Ʊ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strCardID"></param>
        /// <param name="p_strCreateEmpID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindInvoiceByInvoNo(string strCardID, out string p_strCreateEmpID, out string invoNo)
        {
            long lngRes = -1;
            invoNo = string.Empty;
            p_strCreateEmpID = string.Empty;
            if (string.IsNullOrEmpty(strCardID))
            {
                return lngRes;
            }
            string strSQL = "";
            strSQL = @"select  a.opremp_chr, a.invoiceno_vchr 
                          from t_opr_outpatientrecipeinv a
                         where a.invoiceno_vchr not in
                               (select b.invoiceno_vchr
                                  from t_opr_outpatientrecipeinv b
                                 where b.invoiceno_vchr in
                                       (select c.invoiceno_vchr
                                          from t_opr_outpatientrecipeinv c
                                         where c.seqid_chr = ?)
                                 group by b.invoiceno_vchr
                                having count(b.invoiceno_vchr) = 2)
                           and a.seqid_chr = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtResult = new DataTable();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                paraArr[0].Value = strCardID.Trim();
                paraArr[1].Value = strCardID.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paraArr);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strCreateEmpID = dtResult.Rows[0]["opremp_chr"].ToString();
                    invoNo = dtResult.Rows[0]["invoiceno_vchr"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

    }
}
