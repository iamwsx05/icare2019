using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// סԺ�շ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-10-20
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBIHChargeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsBIHChargeSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion
        //Ԥ�����շ�
        #region ��ѯ
        /// <summary>
        /// ��ȡȫ��Ԥ�����շѼ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrePay(out clsT_opr_bih_prepay_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_prepay_VO[0];
            long lngRes = 0;
            string strSQL = " SELECT a.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,decode(a.cuycate_int,1,'�ֽ�',2,'���п�',3,'֧Ʊ',4,'IC��',5,'���ÿ�','') CuyCateName";
            strSQL += "        ,decode(a.liner_int,1,'�հ�',2,'���','') LinerName";
            strSQL += "        ,decode(a.paytype_int,1,'һ��',2,'���ڽ�ת',3,'�˷�','') PayTayeName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creatorid_chr) CreatorName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactid_chr) DeactName";
            strSQL += " FROM t_opr_bih_prepay a";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_prepay_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_prepay_VO();
                        p_objResultArr[i1].m_strPREPAYID_CHR = dtbResult.Rows[i1]["PREPAYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intLINER_INT = Convert.ToInt32(dtbResult.Rows[i1]["LINER_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intPAYTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PAYTYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intCUYCATE_INT = Convert.ToInt32(dtbResult.Rows[i1]["CUYCATE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_dblMONEY_DEC = double.Parse(dtbResult.Rows[i1]["MONEY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strPREPAYINV_VCHR = dtbResult.Rows[i1]["PREPAYINV_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEACTID_CHR = dtbResult.Rows[i1]["DEACTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intISCLEAR_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISCLEAR_INT"].ToString().Trim());

                        //Add by jli in 2005-02-28
                        p_objResultArr[i1].m_strPRESSNO_VCHR = dtbResult.Rows[i1]["PRESSNO_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intUPTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["UPTYPE_INT"].ToString().Trim());
                        //Add End

                        //���ֶ�
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["PatientName"].ToString().Trim();
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactName = dtbResult.Rows[i1]["DeactName"].ToString().Trim();
                        p_objResultArr[i1].m_strCuyCateName = dtbResult.Rows[i1]["CuyCateName"].ToString().Trim();
                        p_objResultArr[i1].m_strLinerName = dtbResult.Rows[i1]["LinerName"].ToString().Trim();
                        p_objResultArr[i1].m_strPayTayeName = dtbResult.Rows[i1]["PayTayeName"].ToString().Trim();
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
        /// ��ȡԤ�����շ�--������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrePayByID(string p_strID, out clsT_opr_bih_prepay_VO p_objResult)
        {
            p_objResult = new clsT_opr_bih_prepay_VO();
            long lngRes = 0;
            string strSQL = " SELECT a.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,decode(a.cuycate_int,1,'�ֽ�',2,'���п�',3,'֧Ʊ',4,'IC��',5,'���ÿ�','') CuyCateName";
            strSQL += "        ,decode(a.liner_int,1,'�հ�',2,'���','') LinerName";
            strSQL += "        ,decode(a.paytype_int,1,'һ��',2,'���ڽ�ת',3,'�˷�','') PayTayeName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creatorid_chr) CreatorName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactid_chr) DeactName";
            strSQL += " FROM t_opr_bih_prepay a";
            strSQL += " WHERE a.prepayid_chr='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_bih_prepay_VO();
                    p_objResult.m_strPREPAYID_CHR = dtbResult.Rows[0]["PREPAYID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_intLINER_INT = Convert.ToInt32(dtbResult.Rows[0]["LINER_INT"].ToString().Trim());
                    p_objResult.m_intPAYTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["PAYTYPE_INT"].ToString().Trim());
                    p_objResult.m_intCUYCATE_INT = Convert.ToInt32(dtbResult.Rows[0]["CUYCATE_INT"].ToString().Trim());
                    p_objResult.m_dblMONEY_DEC = double.Parse(dtbResult.Rows[0]["MONEY_DEC"].ToString().Trim());
                    p_objResult.m_strPREPAYINV_VCHR = dtbResult.Rows[0]["PREPAYINV_VCHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strCREATORID_CHR = dtbResult.Rows[0]["CREATORID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strDEACTID_CHR = dtbResult.Rows[0]["DEACTID_CHR"].ToString().Trim();
                    p_objResult.m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString());
                    p_objResult.m_intISCLEAR_INT = Convert.ToInt32(dtbResult.Rows[0]["ISCLEAR_INT"].ToString());

                    //Add by jli in 2005-02-28

                    p_objResult.m_strPRESSNO_VCHR = dtbResult.Rows[0]["PRESSNO_VCHR"].ToString();
                    p_objResult.m_intUPTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["UPTYPE_INT"].ToString());

                    //Add End

                    //���ֶ�
                    p_objResult.m_strPatientName = dtbResult.Rows[0]["PatientName"].ToString().Trim();
                    p_objResult.m_strAreaName = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                    p_objResult.m_strCreatorName = dtbResult.Rows[0]["CreatorName"].ToString().Trim();
                    p_objResult.m_strDeactName = dtbResult.Rows[0]["DeactName"].ToString().Trim();
                    p_objResult.m_strCuyCateName = dtbResult.Rows[0]["CuyCateName"].ToString().Trim();
                    p_objResult.m_strLinerName = dtbResult.Rows[0]["LinerName"].ToString().Trim();
                    p_objResult.m_strPayTayeName = dtbResult.Rows[0]["PayTayeName"].ToString().Trim();
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
        /// ��ȡԤ�����շ�--������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">������Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dtResult"></param>
        [AutoComplete]
        public long m_lngGetPrePayByRegisterID(string p_strRegisterID, int[] types, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = " SELECT distinct a.*,b.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,decode(a.cuycate_int,1,'�ֽ�',2,'���п�',3,'֧Ʊ',4,'IC��',5,'���ÿ�','') CuyCateName";
            strSQL += "        ,decode(a.liner_int,1,'�հ�',2,'���','') LinerName";
            strSQL += "        ,decode(a.paytype_int,1,'һ��',2,'���ڽ�ת',3,'�˷�','') PayTayeName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creatorid_chr) CreatorName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactid_chr) DeactName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee ";
            strSQL += "          WHERE empid_chr=(select aa.clearactid_chr from t_opr_bih_paymoney aa where b.paymoneyid_chr =aa.paymoneyid_chr)";
            strSQL += "         ) ClearActName";//������
            strSQL += "        ,(select aa.chearact_dat from t_opr_bih_paymoney aa where b.paymoneyid_chr =aa.paymoneyid_chr) chearact_dat";//��������
            strSQL += "        ,(select aa.clearinv_vchr from t_opr_bih_paymoney aa where b.paymoneyid_chr =aa.paymoneyid_chr) clearinv_vchr";//���ʵ���
            strSQL += " FROM t_opr_bih_prepay a,t_opr_bih_payandprepaymap b";
            strSQL += " WHERE a.prepayid_chr=b.prepayid_chr (+) and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            if (types.Length > 0)
            {
                strSQL += "and ( 1=2";
                for (int i = 0; i < types.Length; i++)
                {
                    strSQL += " or a.PAYTYPE_INT = " + types[i].ToString();
                }
                strSQL += ") order by a.PREPAYINV_VCHR,a.CREATE_DAT";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        /// ��ȡ��δ���˵�Ԥ����-������Ժ��ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ��ˮ��</param>
        /// <param name="p_intIsClear">�Ƿ�����{1/0}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrePayByRegisterID(string p_strRegisterID, int p_intIsClear, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = " SELECT distinct a.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,decode(a.cuycate_int,1,'�ֽ�',2,'���п�',3,'֧Ʊ',4,'IC��',5,'���ÿ�','') CuyCateName";
            strSQL += "        ,decode(a.liner_int,1,'�հ�',2,'���','') LinerName";
            strSQL += "        ,decode(a.paytype_int,1,'һ��',2,'���ڽ�ת',3,'�˷�','') PayTayeName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creatorid_chr) CreatorName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactid_chr) DeactName";
            strSQL += " FROM t_opr_bih_prepay a";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' and isclear_int=" + p_intIsClear.ToString() + " and PAYTYPE_INT = 1";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ��֤�û�
        [AutoComplete]
        public bool m_bolIsPassedCheck(string p_strEmpID, string p_strPSW)
        {
            string strSQL = "";
            if (p_strPSW != "")
            {
                strSQL = @"SELECT COUNT (*) AS c
  FROM t_bse_employee a
 WHERE a.empid_chr = '" + p_strEmpID + "' AND a.psw_chr = '" + p_strPSW + "'";
            }
            else
            {
                strSQL = @"SELECT COUNT (*) AS c
  FROM t_bse_employee a
 WHERE a.empid_chr = '" + p_strEmpID + "'";
            }
            try
            {
                DataTable p_dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && int.Parse(p_dtResult.Rows[0]["c"].ToString()) > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return false;
            }
        }
        #endregion
        #region ����
        /// <summary>
        /// ����Ԥ�����շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPrePay(out string p_strRecordID, clsT_opr_bih_prepay_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(12, "PREPAYID_CHR", "T_opr_bih_prepay", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //Modify by jli 2005-02-28
            string strSQL = "INSERT INTO T_opr_bih_prepay (PREPAYID_CHR,PATIENTID_CHR,REGISTERID_CHR,LINER_INT,PAYTYPE_INT,CUYCATE_INT,MONEY_DEC,PREPAYINV_VCHR,AREAID_CHR,DES_VCHR,CREATORID_CHR,CREATE_DAT,STATUS_INT,ISCLEAR_INT,PRESSNO_VCHR,UPTYPE_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //Modify End

            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;

                //Modify by jli in 2005-02-28 Add 2 parameters to the SQL string

                //				objHRPSvc.CreateDatabaseParameter(14,out objLisAddItemRefArr);
                objHRPSvc.CreateDatabaseParameter(16, out objLisAddItemRefArr);

                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strPREPAYID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intLINER_INT;
                objLisAddItemRefArr[4].Value = p_objRecord.m_intPAYTYPE_INT;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intCUYCATE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_dblMONEY_DEC;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strPREPAYINV_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strCREATORID_CHR;
                objLisAddItemRefArr[11].Value = DateTime.Parse(strDateTime);//DateTime.Parse(p_objRecord.m_strCREATE_DAT);
                objLisAddItemRefArr[12].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intISCLEAR_INT;
                //Add by jli
                objLisAddItemRefArr[14].Value = p_objRecord.m_strPRESSNO_VCHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_intUPTYPE_INT;

                //Modify End

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
        #region ����
        /// <summary>
        /// ����Ԥ�����շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        //[AutoComplete]
        [AutoComplete]
        public long m_lngModifyPrePay(string p_strID, clsT_opr_bih_prepay_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = p_objRecord.m_strCREATE_DAT;
            string strSQL = " ";

            //Modify by jli in 2005-02-28 Add 2 parameters to the SQL string

            strSQL += " UPDATE T_OPR_BIH_PREPAY";
            strSQL += " SET";
            strSQL += "    PATIENTID_CHR = '" + p_objRecord.m_strPATIENTID_CHR + "'";
            strSQL += "  , REGISTERID_CHR = '" + p_objRecord.m_strREGISTERID_CHR + "'";
            strSQL += "  , LINER_INT = " + p_objRecord.m_intLINER_INT.ToString();
            strSQL += "  , PAYTYPE_INT = " + p_objRecord.m_intPAYTYPE_INT.ToString();
            strSQL += "  , CUYCATE_INT = " + p_objRecord.m_intCUYCATE_INT.ToString();
            strSQL += "  , MONEY_DEC = " + p_objRecord.m_dblMONEY_DEC.ToString();
            strSQL += "  , PREPAYINV_VCHR = '" + p_objRecord.m_strPREPAYINV_VCHR + "'";
            strSQL += "  , AREAID_CHR = '" + p_objRecord.m_strAREAID_CHR + "'";
            strSQL += "  , DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "'";
            strSQL += "  , CREATORID_CHR = '" + p_objRecord.m_strCREATORID_CHR + "'";
            strSQL += "  , CREATE_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//p_objRecord.m_strCREATE_DAT
            strSQL += "  , STATUS_INT = " + p_objRecord.m_intSTATUS_INT.ToString();
            strSQL += "  , ISCLEAR_INT = " + p_objRecord.m_intISCLEAR_INT.ToString();
            //Add by jli
            strSQL += "  , PRESSNO_VCHR = '" + p_objRecord.m_strPRESSNO_VCHR.ToString() + "'";
            strSQL += "  , UPTYPE_INT = " + p_objRecord.m_intUPTYPE_INT.ToString();
            //Add end
            strSQL += " WHERE prepayid_chr='" + p_strID.Trim() + "'";

            //Modify End

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        [AutoComplete]
        public long m_lngRefundment(string OperatorID, int Type, clsT_opr_bih_prepay_VO p_objRecord)
        {
            long lngRes = 0;
            if (Type == 0)
            {
                p_objRecord.m_intPAYTYPE_INT = 4;
            }
            else if (Type == 1)
            {
                p_objRecord.m_intPAYTYPE_INT = 3;
            }
            lngRes = m_lngModifyPrePay(p_objRecord.m_strPREPAYID_CHR, p_objRecord);
            if (lngRes > 0)
            {
                string ID = "";
                p_objRecord.m_strCREATORID_CHR = OperatorID;
                p_objRecord.m_dblMONEY_DEC = p_objRecord.m_dblMONEY_DEC * -1;
                p_objRecord.m_strCREATE_DAT = System.DateTime.Now.ToString();
                lngRes = m_lngAddNewPrePay(out ID, p_objRecord);
            }
            return lngRes;
        }
        /// <summary>
        /// ���Ԥ�����շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strDeactID">�����</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCleanupPrePay(string p_strID, string p_strDeactID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " ";
            strSQL += " UPDATE T_OPR_BIH_PREPAY";
            strSQL += " SET";
            strSQL += "  ISCLEAR_INT = 1";//�Ƿ�����{1/0}
            strSQL += "  , DEACTID_CHR = '" + p_strDeactID.Trim() + "'";
            strSQL += "  , DEACTIVATE_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += " WHERE prepayid_chr='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ɾ��
        /// <summary>
        /// ɾ��Ԥ�����շ�	[��ɾ��]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strDeactID">ɾ����ID</param>
        /// <returns></returns>
        //[AutoComplete]
        [AutoComplete]
        public long m_lngDeletePrePay(string p_strID, string p_strDeactID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_PREPAY";
            strSQL += " SET";
            strSQL += "    DEACTID_CHR = '" + p_strDeactID.Trim() + "'";
            strSQL += "  , DEACTIVATE_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "    STATUS_INT = 0";
            strSQL += " WHERE prepayid_chr='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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

        //���˽��Ѽ�¼
        #region ��ѯ
        /// <summary>
        /// ��ȡȫ�����˽��Ѽ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPayMoney(out clsT_opr_bih_paymoney_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_paymoney_VO[0];
            long lngRes = 0;
            string strSQL = " SELECT a.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName";
            strSQL += "        ,decode(a.squaretype_int,1,'�Է�',2,'ҽ��','') SquareTypeName";
            strSQL += "        ,decode(a.targetflag_int,1,'ֱ��',2,'����','') TargetFlagName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.clearactid_chr) ClearActName";
            strSQL += " FROM t_opr_bih_paymoney a ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_paymoney_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_paymoney_VO();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        if (dtbResult.Rows[i1]["SQUARETYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSQUARETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SQUARETYPE_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["YSMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblYSMONEY_DEC = double.Parse(dtbResult.Rows[i1]["YSMONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["BJMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblBJMONEY_DEC = double.Parse(dtbResult.Rows[i1]["BJMONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["TARGETFLAG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intTARGETFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["TARGETFLAG_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["CBMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblCBMONEY_DEC = double.Parse(dtbResult.Rows[i1]["CBMONEY_DEC"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strCLEARACTID_CHR = dtbResult.Rows[i1]["CLEARACTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHEARACT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCLEARINV_VCHR = dtbResult.Rows[i1]["CLEARINV_VCHR"].ToString().Trim();

                        //Add by jli in 2005-02-28
                        p_objResultArr[i1].m_strCLEARINV_VCHR = dtbResult.Rows[i1]["CLEARINV_VCHR"].ToString().Trim();
                        //Add End

                        //���ֶ�
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["PatientName"].ToString().Trim();
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strSquareTypeName = dtbResult.Rows[i1]["SquareTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strTargetFlagName = dtbResult.Rows[i1]["TargetFlagName"].ToString().Trim();
                        p_objResultArr[i1].m_strClearActName = dtbResult.Rows[i1]["ClearActName"].ToString().Trim();
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
        /// ��ȡ���˽��Ѽ�¼	���ݷ�ƱID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInvoiceID">��Ʊ��¼��ˮ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPayMoneyByInvoiceID(string p_strInvoiceID, out clsT_opr_bih_paymoney_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_paymoney_VO[0];
            long lngRes = 0;
            string strSQL = @"
					SELECT a.*, 
						(SELECT lastname_vchr
								FROM t_bse_patient
								WHERE t_bse_patient.patientid_chr = a.patientid_chr) patientname,
						(SELECT deptname_vchr
							FROM t_bse_deptdesc
							WHERE t_bse_deptdesc.deptid_chr = a.areaid_chr) areaname,
						(SELECT lastname_vchr
							FROM t_bse_employee
							WHERE t_bse_employee.empid_chr = a.creator_chr) creatorname,
						DECODE (a.squaretype_int, 1, '�Է�', 2, 'ҽ��', '') squaretypename,
						DECODE (a.targetflag_int, 1, 'ֱ��', 2, '����', '') targetflagname,
						(SELECT lastname_vchr
							FROM t_bse_employee
							WHERE t_bse_employee.empid_chr = a.clearactid_chr) clearactname
					FROM t_opr_bih_paymoney a WHERE a.clearinv_vchr='[InvoiceID]'";
            strSQL = strSQL.Replace("[InvoiceID]", p_strInvoiceID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_paymoney_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_paymoney_VO();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        if (dtbResult.Rows[i1]["SQUARETYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSQUARETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["SQUARETYPE_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["YSMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblYSMONEY_DEC = double.Parse(dtbResult.Rows[i1]["YSMONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["BJMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblBJMONEY_DEC = double.Parse(dtbResult.Rows[i1]["BJMONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["TARGETFLAG_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intTARGETFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["TARGETFLAG_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["CBMONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblCBMONEY_DEC = double.Parse(dtbResult.Rows[i1]["CBMONEY_DEC"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strCLEARACTID_CHR = dtbResult.Rows[i1]["CLEARACTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHEARACT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCLEARINV_VCHR = dtbResult.Rows[i1]["CLEARINV_VCHR"].ToString().Trim();

                        //Add by jli in 2005-02-28
                        p_objResultArr[i1].m_strCLEARINV_VCHR = dtbResult.Rows[i1]["CLEARINV_VCHR"].ToString().Trim();
                        //Add End

                        //���ֶ�
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["PatientName"].ToString().Trim();
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strSquareTypeName = dtbResult.Rows[i1]["SquareTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strTargetFlagName = dtbResult.Rows[i1]["TargetFlagName"].ToString().Trim();
                        p_objResultArr[i1].m_strClearActName = dtbResult.Rows[i1]["ClearActName"].ToString().Trim();
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
        /// ��ȡ���˽��Ѽ�¼--������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPayMoneyByID(string p_strID, out clsT_opr_bih_paymoney_VO p_objResult)
        {
            p_objResult = new clsT_opr_bih_paymoney_VO();
            long lngRes = 0;
            string strSQL = " SELECT a.*";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_patient WHERE t_bse_patient.patientid_chr=a.patientid_chr) PatientName";
            strSQL += "        ,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr) CreatorName";
            strSQL += "        ,decode(a.squaretype_int,1,'�Է�',2,'ҽ��','') SquareTypeName";
            strSQL += "        ,decode(a.targetflag_int,1,'ֱ��',2,'����','') TargetFlagName";
            strSQL += "        ,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.clearactid_chr) ClearActName";
            strSQL += " FROM t_opr_bih_paymoney a ";
            strSQL += " WHERE a.paymoneyid_chr='" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_bih_paymoney_VO();
                    p_objResult.m_strPAYMONEYID_CHR = dtbResult.Rows[0]["PAYMONEYID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATOR_CHR = dtbResult.Rows[0]["CREATOR_CHR"].ToString().Trim();
                    p_objResult.m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSQUARETYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["SQUARETYPE_INT"].ToString().Trim());
                    p_objResult.m_dblYSMONEY_DEC = double.Parse(dtbResult.Rows[0]["YSMONEY_DEC"].ToString().Trim());
                    p_objResult.m_dblBJMONEY_DEC = double.Parse(dtbResult.Rows[0]["BJMONEY_DEC"].ToString().Trim());
                    p_objResult.m_intTARGETFLAG_INT = Convert.ToInt32(dtbResult.Rows[0]["TARGETFLAG_INT"].ToString().Trim());
                    p_objResult.m_dblCBMONEY_DEC = double.Parse(dtbResult.Rows[0]["CBMONEY_DEC"].ToString().Trim());
                    p_objResult.m_strCLEARACTID_CHR = dtbResult.Rows[0]["CLEARACTID_CHR"].ToString().Trim();
                    p_objResult.m_strCHEARACT_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CHEARACT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strCLEARINV_VCHR = dtbResult.Rows[0]["CLEARINV_VCHR"].ToString().Trim();
                    //���ֶ�
                    p_objResult.m_strPatientName = dtbResult.Rows[0]["PatientName"].ToString().Trim();
                    p_objResult.m_strAreaName = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                    p_objResult.m_strCreatorName = dtbResult.Rows[0]["CreatorName"].ToString().Trim();
                    p_objResult.m_strSquareTypeName = dtbResult.Rows[0]["SquareTypeName"].ToString().Trim();
                    p_objResult.m_strTargetFlagName = dtbResult.Rows[0]["TargetFlagName"].ToString().Trim();
                    p_objResult.m_strClearActName = dtbResult.Rows[0]["ClearActName"].ToString().Trim();
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
        #region ����
        /// <summary>
        /// �������˽��Ѽ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">���˽��Ѽ�¼��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPayMoney(out string p_strRecordID, clsT_opr_bih_paymoney_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "PAYMONEYID_CHR", "t_opr_bih_paymoney", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_bih_paymoney (PAYMONEYID_CHR,PATIENTID_CHR,REGISTERID_CHR,AREAID_CHR,CREATOR_CHR,CREATE_DAT,SQUARETYPE_INT,YSMONEY_DEC,BJMONEY_DEC,TARGETFLAG_INT,CBMONEY_DEC,CLEARACTID_CHR,CHEARACT_DAT,CLEARINV_VCHR,BALANCETOTYPE_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(15, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;// p_objRecord.m_strPAYMONEYID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strCREATOR_CHR;
                objLisAddItemRefArr[5].Value = DateTime.Parse(strDateTime);//DateTime.Parse(p_objRecord.m_strCREATE_DAT);
                objLisAddItemRefArr[6].Value = p_objRecord.m_intSQUARETYPE_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_dblYSMONEY_DEC;
                objLisAddItemRefArr[8].Value = p_objRecord.m_dblBJMONEY_DEC;
                objLisAddItemRefArr[9].Value = p_objRecord.m_intTARGETFLAG_INT;
                objLisAddItemRefArr[10].Value = p_objRecord.m_dblCBMONEY_DEC;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strCLEARACTID_CHR;
                objLisAddItemRefArr[12].Value = DateTime.Parse(strDateTime);//DateTime.Parse(p_objRecord.m_strCHEARACT_DAT);
                objLisAddItemRefArr[13].Value = p_objRecord.m_strCLEARINV_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_intBALANCETOTYPE_INT;
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

        /// <summary>
        /// �������˽��Ѽ�¼	������Ʊ	[����]
        /// ҵ��˵��: ���շ�Ʊ��ѯ,ÿ�ŷ�Ʊ��¼����һ�������ļ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInvoiceID">��Ʊ��¼��ˮ��</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <param name="p_strOperateDateTime">����ʱ��</param>
        /// <param name="p_alPayMoneyID">����ID	[ArrayList]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPayMoney(string p_strInvoiceID, string p_strOperatorID, string p_strOperateDateTime, out System.Collections.Generic.List<string> p_alPayMoneyID)
        {
            long lngRes = 0;
            p_alPayMoneyID = new System.Collections.Generic.List<string>();
            System.Collections.ArrayList p_alRecordID = new System.Collections.ArrayList();
            clsT_opr_bih_paymoney_VO[] objItemArr = new clsT_opr_bih_paymoney_VO[0];
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngGetPayMoneyByInvoiceID(p_strInvoiceID, out objItemArr);
            }

            if (lngRes > 0 && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    if (!p_alPayMoneyID.Contains(objItemArr[i1].m_strPAYMONEYID_CHR.Trim()))
                        p_alPayMoneyID.Add(objItemArr[i1].m_strPAYMONEYID_CHR.Trim());
                }
            }

            if (lngRes > 0 && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    string strRecordID = "";
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        objItemArr[i1].m_dblBJMONEY_DEC = -objItemArr[i1].m_dblYSMONEY_DEC;
                        objItemArr[i1].m_dblCBMONEY_DEC = -objItemArr[i1].m_dblCBMONEY_DEC;
                        objItemArr[i1].m_dblYSMONEY_DEC = -objItemArr[i1].m_dblYSMONEY_DEC;
                        objItemArr[i1].m_strCLEARACTID_CHR = p_strOperatorID;
                        objItemArr[i1].m_strCHEARACT_DAT = p_strOperateDateTime;
                        objItemArr[i1].m_strCREATOR_CHR = p_strOperatorID;
                        objItemArr[i1].m_strCREATE_DAT = p_strOperateDateTime;
                        lngRes = m_lngAddNewPayMoney(out strRecordID, objItemArr[i1]);
                        if (!p_alRecordID.Contains(strRecordID.Trim())) p_alRecordID.Add(strRecordID.Trim());
                    }
                }
            }
            if (lngRes <= 0)
            {
                throw (new Exception("����ʧ��!"));
            }
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ���Ĳ��˽��Ѽ�¼--������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        //[AutoComplete]
        [AutoComplete]
        public long m_lngModifyPayMoney(string p_strID, clsT_opr_bih_paymoney_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_PAYMONEY ";
            strSQL += " SET";
            strSQL += "    PATIENTID_CHR = '" + p_objRecord.m_strPATIENTID_CHR + "'";
            strSQL += "  , REGISTERID_CHR = '" + p_objRecord.m_strREGISTERID_CHR + "'";
            strSQL += "  , AREAID_CHR = '" + p_objRecord.m_strAREAID_CHR + "'";
            //strSQL +="  , CREATOR_CHR = '" + p_objRecord.m_strCREATOR_CHR + "'";
            //strSQL +="  , CREATE_DAT = TO_DATE('"+p_objRecord.m_strCREATE_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , SQUARETYPE_INT = " + p_objRecord.m_intSQUARETYPE_INT.ToString();
            strSQL += "  , YSMONEY_DEC = " + p_objRecord.m_dblYSMONEY_DEC.ToString();
            strSQL += "  , BJMONEY_DEC = " + p_objRecord.m_dblBJMONEY_DEC.ToString();
            strSQL += "  , TARGETFLAG_INT = " + p_objRecord.m_intTARGETFLAG_INT.ToString();
            strSQL += "  , CBMONEY_DEC = " + p_objRecord.m_dblCBMONEY_DEC.ToString();
            strSQL += "  , CLEARACTID_CHR = '" + p_objRecord.m_strCLEARACTID_CHR + "'";
            strSQL += "  , CHEARACT_DAT = TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//p_objRecord.m_strCHEARACT_DAT
            strSQL += "  , CLEARINV_VCHR = '" + p_objRecord.m_strCLEARINV_VCHR + "'";
            strSQL += "  , BALANCETOTYPE_INT = '" + p_objRecord.m_intBALANCETOTYPE_INT + "'";
            strSQL += " WHERE";
            strSQL += "     PAYMONEYID_CHR = '" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ɾ��
        /// <summary>
        ///  ɾ�����˽��Ѽ�¼--������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        //[AutoComplete]
        [AutoComplete]
        public long m_lngDeletePayMoney(string p_strID, clsT_opr_bih_paymoney_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete from t_opr_bih_paymoney where t_opr_bih_paymoney.paymoneyid_chr='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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

        //��������
        #region ��ѯ
        /// <summary>
        /// ��ȡ���ʼ�¼	����������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">������ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAccountByID(string p_strID, out clsT_opr_bih_dayaccount_VO p_objResult)
        {
            p_objResult = new clsT_opr_bih_dayaccount_VO();
            long lngRes = 0;
            try
            {
                DataTable dtbResult = new DataTable();
                string strCondition = "dayaccountid_chr='" + p_strID.Trim() + "'";
                lngRes = m_lngGetDayAccount(strCondition, out dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_opr_bih_dayaccount_VO();
                    p_objResult.m_strDAYACCOUNTID_CHR = dtbResult.Rows[0]["DAYACCOUNTID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_intORDERNO_INT = Convert.ToInt32(dtbResult.Rows[0]["ORDERNO_INT"].ToString().Trim());
                    try { p_objResult.m_strSQUARE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["SQUARE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim(); }
                    catch { }
                    p_objResult.m_dblCHARGE_DEC = double.Parse(dtbResult.Rows[0]["CHARGE_DEC"].ToString().Trim());
                    p_objResult.m_dblCLEARCHG_DEC = double.Parse(dtbResult.Rows[0]["CLEARCHG_DEC"].ToString().Trim());
                    p_objResult.m_dblCHGOFMEPAY_DEC = double.Parse(dtbResult.Rows[0]["CHGOFMEPAY_DEC"].ToString().Trim());
                    p_objResult.m_dblCLEARCHGOFME_DEC = double.Parse(dtbResult.Rows[0]["CLEARCHGOFME_DEC"].ToString().Trim());
                    p_objResult.m_dblCHGOFHEPAY_DEC = double.Parse(dtbResult.Rows[0]["CHGOFHEPAY_DEC"].ToString().Trim());
                    p_objResult.m_dblCLEARCHGOFHE_DEC = double.Parse(dtbResult.Rows[0]["CLEARCHGOFHE_DEC"].ToString().Trim());
                    p_objResult.m_strCLEARMPOPTID_CHR = dtbResult.Rows[0]["CLEARMPOPTID_CHR"].ToString().Trim();
                    try { p_objResult.m_strCHEARMP_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CHEARMP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim(); }
                    catch { }
                    p_objResult.m_strCLEARHPOPTID_CHR = dtbResult.Rows[0]["CLEARHPOPTID_CHR"].ToString().Trim();
                    try { p_objResult.m_strCLEARHP_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CLEARHP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim(); }
                    catch { }
                    p_objResult.m_strAREAID_CHR = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    //���ֶ�
                    p_objResult.m_strPatientName = dtbResult.Rows[0]["PatientName"].ToString().Trim();
                    p_objResult.m_strClearMpoptName = dtbResult.Rows[0]["ClearMpoptName"].ToString().Trim();
                    p_objResult.m_strClearHpoptName = dtbResult.Rows[0]["ClearHpoptName"].ToString().Trim();
                    p_objResult.m_strAreaName = dtbResult.Rows[0]["AreaName"].ToString().Trim();
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
        /// ��ѯδ���˵�������Ϣ	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAccountByRegisterID(string p_strRegisterID, out clsT_opr_bih_dayaccount_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_dayaccount_VO[0];
            long lngRes = 0;

            try
            {
                DataTable dtbResult = new DataTable();
                string strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' and CHARGE_DEC!=CLEARCHG_DEC ";
                lngRes = m_lngGetDayAccount(strCondition, out dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_dayaccount_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_dayaccount_VO();
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intORDERNO_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDERNO_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strSQUARE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["SQUARE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_dblCHARGE_DEC = double.Parse(dtbResult.Rows[i1]["CHARGE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHG_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHG_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCHGOFMEPAY_DEC = double.Parse(dtbResult.Rows[i1]["CHGOFMEPAY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHGOFME_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHGOFME_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCHGOFHEPAY_DEC = double.Parse(dtbResult.Rows[i1]["CHGOFHEPAY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHGOFHE_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHGOFHE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strCLEARMPOPTID_CHR = dtbResult.Rows[i1]["CLEARMPOPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHEARMP_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARMP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCLEARHPOPTID_CHR = dtbResult.Rows[i1]["CLEARHPOPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLEARHP_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CLEARHP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        //���ֶ�
                        p_objResultArr[i1].m_strPatientName = dtbResult.Rows[i1]["PatientName"].ToString().Trim();
                        p_objResultArr[i1].m_strClearMpoptName = dtbResult.Rows[i1]["ClearMpoptName"].ToString().Trim();
                        p_objResultArr[i1].m_strClearHpoptName = dtbResult.Rows[i1]["ClearHpoptName"].ToString().Trim();
                        p_objResultArr[i1].m_strAreaName = dtbResult.Rows[i1]["AreaName"].ToString().Trim();
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
        /// ��ѯδ���˵�������Ϣ	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAccountByRegisterID(string p_strRegisterID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;

            try
            {
                string strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' and CHARGE_DEC!=CLEARCHG_DEC ";
                lngRes = m_lngGetDayAccount(strCondition, out p_dtResult);
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
        /// ��ѯ������Ϣ	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="intType">��ѯ������ {1��ȫ����Ч��¼��2��δ���˵���Ч��¼��3���Ѿ�������Ч��¼������Ĭ��Ϊȫ����Ч��¼}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAccountByRegisterID(string p_strRegisterID, int intType, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            try
            {
                string strCondition = "";
                switch (intType)
                {
                    case 1: //1��ȫ����Ч��¼��
                        strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' ";
                        break;
                    case 2: //2��δ���˵���Ч��¼��	{���ڷ���=������� and (�Է������� is null)}
                            //strCondition =" REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' and CHARGE_DEC!=CLEARCHG_DEC and (CHEARMP_DAT is null)";
                        strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' and (CHEARMP_DAT is null)";
                        break;
                    case 3: //3���Ѿ�������Ч��¼
                        strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' and (CHEARMP_DAT is not null)";
                        break;
                    default: //ȫ����Ч��¼��
                        strCondition = " REGISTERID_CHR ='" + p_strRegisterID.Trim() + "' ";
                        break;
                }

                lngRes = m_lngGetDayAccount(strCondition, out p_dtResult);
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
        ///  ��ѯ������Ϣ	��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCondition">�������ʽ [������Where	�磺"1=1"]</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAccount(string p_strCondition, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;

            string strSQL = "";
            strSQL += " SELECT a.*";
            strSQL += "        ,(select b1.name_vchr from t_bse_patient b1 where b1.patientid_chr=a.patientid_chr)PatientName";//--��������
            strSQL += "        ,(select b2.lastname_vchr from t_bse_employee b2 where b2.empid_chr=a.clearmpoptid_chr)ClearMpoptName";//--�Է�������
            strSQL += "        ,(select b3.lastname_vchr from t_bse_employee b3 where b3.empid_chr=a.clearhpoptid_chr)ClearHpoptName";//--����������
            strSQL += "        ,(select deptname_vchr from t_bse_deptdesc where t_bse_deptdesc.deptid_chr=a.areaid_chr) AreaName";
            strSQL += " FROM t_opr_bih_dayaccount a";
            if (p_strCondition != string.Empty)
            {
                strSQL += " Where " + p_strCondition;
            }
            strSQL += " order by dayaccountid_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ����
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">������ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDayAccount(out string p_strRecordID, clsT_opr_bih_dayaccount_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "DAYACCOUNTID_CHR", "T_opr_bih_dayaccount", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_opr_bih_dayaccount (DAYACCOUNTID_CHR,PATIENTID_CHR,REGISTERID_CHR,ORDERNO_INT,SQUARE_DAT,CHARGE_DEC,CLEARCHG_DEC,CHGOFMEPAY_DEC,CLEARCHGOFME_DEC,CHGOFHEPAY_DEC,CLEARCHGOFHE_DEC,CLEARMPOPTID_CHR,CHEARMP_DAT,CLEARHPOPTID_CHR,CLEARHP_DAT,AREAID_CHR) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(16, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strDAYACCOUNTID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intORDERNO_INT;
                objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strSQUARE_DAT);
                objLisAddItemRefArr[5].Value = p_objRecord.m_dblCHARGE_DEC;
                objLisAddItemRefArr[6].Value = p_objRecord.m_dblCLEARCHG_DEC;
                objLisAddItemRefArr[7].Value = p_objRecord.m_dblCHGOFMEPAY_DEC;
                objLisAddItemRefArr[8].Value = p_objRecord.m_dblCLEARCHGOFME_DEC;
                objLisAddItemRefArr[9].Value = p_objRecord.m_dblCHGOFHEPAY_DEC;
                objLisAddItemRefArr[10].Value = p_objRecord.m_dblCLEARCHGOFHE_DEC;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strCLEARMPOPTID_CHR;
                objLisAddItemRefArr[12].Value = DateTime.Parse(p_objRecord.m_strCHEARMP_DAT);
                objLisAddItemRefArr[13].Value = p_objRecord.m_strCLEARHPOPTID_CHR;
                objLisAddItemRefArr[14].Value = DateTime.Parse(p_objRecord.m_strCLEARHP_DAT);
                objLisAddItemRefArr[15].Value = DateTime.Parse(p_objRecord.m_strAREAID_CHR);
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
        #region ����
        /// <summary>
        /// �޸�������Ϣ	���ݣӣѣ���䡣
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strSQL">Update�ģӣѣ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDayAccount(string strSQL)
        {
            long lngRes = 0;
            if (strSQL == string.Empty) return -1;
            if (strSQL.Trim().ToLower().Substring(0, 6) != "update") return -1;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        /// ��ԭ������Ϣ	��������ID	[������Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDayAccountID">����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDayAccountByDayAccountID(string p_strDayAccountID)
        {
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            //�����������
            double dblMoney = 0;
            #region �����������
            strSQL = @"
					SELECT SUM (amount_dec * unitprice_dec)
					FROM t_opr_bih_patientcharge
					WHERE status_int = 1 AND pstatus_int = 3 AND dayaccountid_chr = '[DAYACCOUNTID]'";
            strSQL = strSQL.Replace("[DAYACCOUNTID]", p_strDayAccountID.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows[0][0] != System.DBNull.Value)
                {
                    try { dblMoney = Convert.ToDouble(dtbResult.Rows[0][0].ToString()); }
                    catch { }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion
            //�����Ƿ�ȫ����
            bool blnIsClearAll = false;
            #region �����Ƿ�ȫ����
            strSQL = @"
					SELECT COUNT (*)
					FROM t_opr_bih_patientcharge
					WHERE status_int = 1 AND pstatus_int != 3 AND dayaccountid_chr = '[DAYACCOUNTID]'";
            strSQL = strSQL.Replace("[DAYACCOUNTID]", p_strDayAccountID.Trim());
            try
            {
                dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows[0][0] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(dtbResult.Rows[0][0].ToString()) > 0)
                    {
                        blnIsClearAll = false;
                    }
                    else
                    {
                        blnIsClearAll = true;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion
            //��ԭ�˷�ǰ״̬
            #region ��ԭ�˷�ǰ״̬
            if (!blnIsClearAll)
            {
                strSQL = @"
					UPDATE t_opr_bih_dayaccount
					SET clearchg_dec = [CLEARMONEY],
						clearchgofme_dec = [CLEARMONEY],
						clearchgofhe_dec = [CLEARMONEY],
						clearmpoptid_chr = null,
						chearmp_dat =  TO_DATE (null),
						clearhpoptid_chr = null,
						clearhp_dat  = TO_DATE (null)
					WHERE dayaccountid_chr = '[DAYACCOUNTID]'";
            }
            else
            {
                strSQL = @"
					UPDATE t_opr_bih_dayaccount
					SET clearchg_dec = [CLEARMONEY],
						clearchgofme_dec = [CLEARMONEY],
						clearchgofhe_dec = [CLEARMONEY]						
					WHERE dayaccountid_chr = '[DAYACCOUNTID]'";
            }
            strSQL = strSQL.Replace("[CLEARMONEY]", dblMoney.ToString("0.00"));
            strSQL = strSQL.Replace("[DAYACCOUNTID]", p_strDayAccountID.Trim());
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
            #endregion

            if (lngRes <= 0)
            {
                ContextUtil.SetAbort();
            }
            return lngRes;
        }
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ��������Ϣ	���ݣӣѣ���䡣
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strSQL">Delete�ģӣѣ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDayAccount(string strSQL)
        {
            long lngRes = 0;
            if (strSQL == string.Empty) return -1;
            if (strSQL.Trim().ToLower().Substring(0, 6) != "delete") return -1;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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

        //����������ϸ
        #region ��ѯ
        /// <summary>
        /// ��ȡ����������ϸ--������ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByID(string p_strID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName";//--��Ч��
            strSQL += "		,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName";//--��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "		,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += " FROM t_opr_bih_patientcharge a";
            strSQL += " WHERE a.pchargeid_chr='" + p_strID.Trim() + "' AND status_int=1 ";//AND a.pstatus_int=" + p_intPStatus.ToString();//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        /// ��ȡ����������ϸ--������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_intPStatus">����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByRegisterID(string p_strRegisterID, int p_intPStatus, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName";//--��Ч��
            strSQL += "		,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName";//--��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "		,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName,unitprice_dec * amount_dec AS totalmoney";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += @" FROM (SELECT a.*,b.itemspec_vchr
  FROM t_opr_bih_patientcharge a, T_BSE_CHARGEITEM b
 WHERE a.chargeitemid_chr = b.itemid_chr(+)) a";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' AND status_int=1 AND a.pstatus_int=" + p_intPStatus.ToString();//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        /// ��ȡ����������ϸ--������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_intPStatusArr">[����]	����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}</param>
        /// <param name="p_intCreateTypeArr">[����]	¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=�շѴ�����(��ҽ��);5��������(��ҽ��)}</param>
        /// <param name="p_intStatusArr">[����]	��Ч״̬	{1=��Ч;0=��Ч;-1=��ʷ}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByRegisterID(string p_strRegisterID, int[] p_intPStatusArr, int[] p_intCreateTypeArr, int[] p_intStatusArr, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            #region SQL
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName";//--��Ч��
            strSQL += "		,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName";//--��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "		,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName,unitprice_dec * amount_dec AS totalmoney";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += @" FROM (SELECT a.*,b.itemspec_vchr,b.itemsrctype_int
  FROM t_opr_bih_patientcharge a, T_BSE_CHARGEITEM b
 WHERE a.chargeitemid_chr = b.itemid_chr(+)) a";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' AND ([a.pstatus_int]) AND ([a.createtype_int]) AND ([a.status_int])";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}

            string strPSTATUS = "", strCreateType = "", strSTATUS = "";
            if (p_intPStatusArr.Length <= 0) strPSTATUS = " 1=2 ";
            for (int i1 = 0; i1 < p_intPStatusArr.Length; i1++)
            {
                if (strPSTATUS.Trim() != "") strPSTATUS += " or ";
                strPSTATUS += " a.pstatus_int=" + p_intPStatusArr[i1].ToString();
            }
            if (p_intCreateTypeArr.Length <= 0) strCreateType = " 1=2 ";
            for (int i1 = 0; i1 < p_intCreateTypeArr.Length; i1++)
            {
                if (strCreateType.Trim() != "") strCreateType += " or ";
                strCreateType += " a.createtype_int=" + p_intCreateTypeArr[i1].ToString();
            }
            if (p_intStatusArr.Length <= 0) strSTATUS = " 1=2 ";
            for (int i1 = 0; i1 < p_intStatusArr.Length; i1++)
            {
                if (strSTATUS.Trim() != "") strSTATUS += " or ";
                strSTATUS += " a.status_int=" + p_intStatusArr[i1].ToString();
            }
            strSQL = strSQL.Replace("[a.pstatus_int]", strPSTATUS.Trim());
            strSQL = strSQL.Replace("[a.createtype_int]", strCreateType.Trim());
            strSQL = strSQL.Replace("[a.status_int]", strSTATUS.Trim());
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        /// ��ȡ�����˷�������ϸ--������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByRegisterIDForSendBack(string p_strRegisterID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;

            #region SQL

            string strSQL = @"
							SELECT a.* 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName		
									,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName
									,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName
									,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName,unitprice_dec * amount_dec AS totalmoney
							FROM (
									SELECT a.pchargeid_chr,
    a.patientid_chr,
    a.registerid_chr,
    a.active_dat,
    a.orderid_chr,
    a.orderexectype_int,
    a.orderexecid_chr,
    a.clacarea_chr,
    a.createarea_chr,
    a.calccateid_chr,
    a.invcateid_chr,
    a.chargeitemid_chr,
    a.chargeitemname_chr,
    a.unit_vchr,
    a.unitprice_dec,
    a.amount_dec,
    a.discount_dec,
    a.ismepay_int,
    a.des_vchr,
    a.createtype_int,
    a.creator_chr,
    a.create_dat,
    a.operator_chr,
    a.modify_dat,
    a.deactivator_chr,
    a.deactivate_dat,
    a.status_int,
    a.pstatus_int,
    a.chearaccount_dat,
    a.dayaccountid_chr,
    a.paymoneyid_chr,
    a.activator_chr,
    a.activatetype_int,
   -- a.isrich_int,
    a.isconfirmrefundment,
    a.refundmentchecker,
    a.refundmentdate,
    a.BMSTATUS_INT,
    
    b.itemspec_vchr, b.itemsrctype_int,b.isrich_int
									FROM t_opr_bih_patientcharge a, t_bse_chargeitem b
									WHERE (a.amount_dec<0)
										AND Trim(a.registerid_chr) = '[REGISTERID]'
										AND a.status_int = 1
										AND Trim(a.chargeitemid_chr) = Trim(b.itemid_chr(+))
								 ) a
							ORDER BY itemsrctype_int";
            /* <<======================================= */
            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterID.Trim());
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        /// ��ȡ����������ϸ--����ҽ��ID[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">����������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByOrderID(string[] p_strOrderIDArr, out clsT_opr_bih_patientcharge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_patientcharge_VO[0];
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName";//--��Ч��
            strSQL += "		,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName";//--��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "		,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += " FROM t_opr_bih_patientcharge a";
            strSQL += " WHERE status_int=1 AND Trim(a.orderid_chr) in([GETORDERID])";
            string strTem = "";
            if (p_strOrderIDArr != null)
            {
                for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
                {
                    if (i1 > 1) strTem += ",";
                    strTem += "'" + p_strOrderIDArr[i1].Trim() + "'";
                }
            }
            if (strTem.Trim() == "") strTem = "''";
            strSQL = strSQL.Replace("[GETORDERID]", strTem);
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_patientcharge_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ���ö���ֵ
                        p_objResultArr[i1] = new clsT_opr_bih_patientcharge_VO();
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strACTIVE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ACTIVE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLACAREA_CHR = dtbResult.Rows[i1]["CLACAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEAREA_CHR = dtbResult.Rows[i1]["CREATEAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCALCCATEID_CHR = dtbResult.Rows[i1]["CALCCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVCATEID_CHR = dtbResult.Rows[i1]["INVCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMNAME_CHR = dtbResult.Rows[i1]["CHARGEITEMNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_DEC = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltAMOUNT_DEC = float.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISMEPAY_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISMEPAY_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intCREATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["CREATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strOPERATOR_CHR = dtbResult.Rows[i1]["OPERATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_strCHEARACCOUNT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACCOUNT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVATOR_CHR = dtbResult.Rows[i1]["ACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //���ֶ�
                        p_objResultArr[i1].m_strClacAreaName = dtbResult.Rows[i1]["ClacAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateAreaName = dtbResult.Rows[i1]["CreateAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpcalcTypeName = dtbResult.Rows[i1]["ItemIpcalcTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpinvTypeName = dtbResult.Rows[i1]["ItemIpinvTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsmePayName = dtbResult.Rows[i1]["IsmePayName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateTypeName = dtbResult.Rows[i1]["CreateTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivatorName = dtbResult.Rows[i1]["ActivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivateTypeName = dtbResult.Rows[i1]["ActivateTypeName"].ToString().Trim();
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
        /// ��ȡ����������ϸ--��ˮ��[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPChargeIdArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">����������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByPChargeIDArr(string[] p_strPChargeIdArr, out clsT_opr_bih_patientcharge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_patientcharge_VO[0];
            long lngRes = 0;
            string strSQL = @"
							SELECT a.* 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName
									,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName
									,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName
									,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName
							FROM t_opr_bih_patientcharge a
							WHERE status_int=1 AND Trim(a.pchargeid_chr) in([PCHARGEID])";
            string strTem = "";
            if (p_strPChargeIdArr != null)
            {
                for (int i1 = 0; i1 < p_strPChargeIdArr.Length; i1++)
                {
                    if (i1 > 1) strTem += ",";
                    strTem += "'" + p_strPChargeIdArr[i1].Trim() + "'";
                }
            }
            if (strTem.Trim() == "") strTem = "''";
            strSQL = strSQL.Replace("[PCHARGEID]", strTem);
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_patientcharge_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ���ö���ֵ
                        p_objResultArr[i1] = new clsT_opr_bih_patientcharge_VO();
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strACTIVE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ACTIVE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLACAREA_CHR = dtbResult.Rows[i1]["CLACAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEAREA_CHR = dtbResult.Rows[i1]["CREATEAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCALCCATEID_CHR = dtbResult.Rows[i1]["CALCCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVCATEID_CHR = dtbResult.Rows[i1]["INVCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMNAME_CHR = dtbResult.Rows[i1]["CHARGEITEMNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_DEC = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltAMOUNT_DEC = float.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISMEPAY_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISMEPAY_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intCREATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["CREATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strOPERATOR_CHR = dtbResult.Rows[i1]["OPERATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_strCHEARACCOUNT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACCOUNT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVATOR_CHR = dtbResult.Rows[i1]["ACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //���ֶ�
                        p_objResultArr[i1].m_strClacAreaName = dtbResult.Rows[i1]["ClacAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateAreaName = dtbResult.Rows[i1]["CreateAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpcalcTypeName = dtbResult.Rows[i1]["ItemIpcalcTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpinvTypeName = dtbResult.Rows[i1]["ItemIpinvTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsmePayName = dtbResult.Rows[i1]["IsmePayName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateTypeName = dtbResult.Rows[i1]["CreateTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivatorName = dtbResult.Rows[i1]["ActivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivateTypeName = dtbResult.Rows[i1]["ActivateTypeName"].ToString().Trim();
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
        /// ��ȡ����������ϸ--������ˮ��[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDayAccountIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">����������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByDayAccountIDArr(string[] p_strDayAccountIDArr, out clsT_opr_bih_patientcharge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_patientcharge_VO[0];
            long lngRes = 0;
            #region SQL
            string strSQL = @"
							SELECT a.* 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName 
									,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName
									,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName
									,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activator_chr)ActivatorName
									,decode(a.activatetype_int,1,'����',2,'������',3,'ȷ�ϼ���',4,'ȷ���շ�',5,'ֱ�ӽ���','') ActivateTypeName
									,decode(a.ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName
									,decode(a.createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName
							FROM t_opr_bih_patientcharge a
							WHERE status_int=1 AND Trim(a.dayaccountid_chr) in([DAYACCOUNTID])";
            string strTem = "";
            if (p_strDayAccountIDArr != null)
            {
                for (int i1 = 0; i1 < p_strDayAccountIDArr.Length; i1++)
                {
                    if (i1 > 1) strTem += ",";
                    strTem += "'" + p_strDayAccountIDArr[i1].Trim() + "'";
                }
            }
            if (strTem.Trim() == "") strTem = "''";
            strSQL = strSQL.Replace("[DAYACCOUNTID]", strTem);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_patientcharge_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region ���ö���ֵ
                        p_objResultArr[i1] = new clsT_opr_bih_patientcharge_VO();
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strACTIVE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ACTIVE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLACAREA_CHR = dtbResult.Rows[i1]["CLACAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEAREA_CHR = dtbResult.Rows[i1]["CREATEAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCALCCATEID_CHR = dtbResult.Rows[i1]["CALCCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVCATEID_CHR = dtbResult.Rows[i1]["INVCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMNAME_CHR = dtbResult.Rows[i1]["CHARGEITEMNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_DEC = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltAMOUNT_DEC = float.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intISMEPAY_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISMEPAY_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intCREATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["CREATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strOPERATOR_CHR = dtbResult.Rows[i1]["OPERATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        }
                        catch { }
                        try
                        {
                            p_objResultArr[i1].m_strCHEARACCOUNT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACCOUNT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        catch { }
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVATOR_CHR = dtbResult.Rows[i1]["ACTIVATOR_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //���ֶ�
                        p_objResultArr[i1].m_strClacAreaName = dtbResult.Rows[i1]["ClacAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateAreaName = dtbResult.Rows[i1]["CreateAreaName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpcalcTypeName = dtbResult.Rows[i1]["ItemIpcalcTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strItemIpinvTypeName = dtbResult.Rows[i1]["ItemIpinvTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreatorName = dtbResult.Rows[i1]["CreatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strOperatorName = dtbResult.Rows[i1]["OperatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strDeactivatorName = dtbResult.Rows[i1]["DeactivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strIsmePayName = dtbResult.Rows[i1]["IsmePayName"].ToString().Trim();
                        p_objResultArr[i1].m_strCreateTypeName = dtbResult.Rows[i1]["CreateTypeName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivatorName = dtbResult.Rows[i1]["ActivatorName"].ToString().Trim();
                        p_objResultArr[i1].m_strActivateTypeName = dtbResult.Rows[i1]["ActivateTypeName"].ToString().Trim();
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
        ///	��ȡ�Ƿ����δȷ�Ϸ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="IsTrue">�Ƿ���� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsExistUndeterminedMoneyByRegisterID(string p_strRegisterID, out bool IsTrue)
        {
            IsTrue = false;
            long lngRes = 0;
            string strSQL = " SELECT count(*) Count";
            strSQL += " FROM t_opr_bih_patientcharge a ";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' and STATUS_INT=1 and a.PSTATUS_INT=0";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0 && Int32.Parse(dtbResult.Rows[0]["Count"].ToString()) > 0)
                {
                    IsTrue = true;
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
        ///	��ȡ�Ƿ���ڴ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="IsTrue">�Ƿ���� [out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsExistWaitReckoningMoneyByRegisterID(string p_strRegisterID, out bool IsTrue)
        {
            IsTrue = false;
            long lngRes = 0;
            string strSQL = " SELECT count(*) Count";
            strSQL += " FROM t_opr_bih_patientcharge a ";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' and STATUS_INT=1 and a.PSTATUS_INT=1";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0 && Int32.Parse(dtbResult.Rows[0]["Count"].ToString()) > 0)
                {
                    IsTrue = true;
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
        #region ����
        /// <summary>
        /// ��������������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">����������ϸ��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatientCharge(out string p_strRecordID, clsT_opr_bih_patientcharge_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "PCHARGEID_CHR", "T_opr_bih_patientcharge", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_opr_bih_patientcharge (PCHARGEID_CHR,PATIENTID_CHR,REGISTERID_CHR,ACTIVE_DAT,ORDERID_CHR,ORDEREXECTYPE_INT,ORDEREXECID_CHR,CLACAREA_CHR,CREATEAREA_CHR,CALCCATEID_CHR,INVCATEID_CHR,CHARGEITEMID_CHR,CHARGEITEMNAME_CHR,UNIT_VCHR,UNITPRICE_DEC,AMOUNT_DEC,DISCOUNT_DEC,ISMEPAY_INT,DES_VCHR,CREATETYPE_INT,CREATOR_CHR,CREATE_DAT,OPERATOR_CHR,MODIFY_DAT,STATUS_INT,PSTATUS_INT,CHEARACCOUNT_DAT,DAYACCOUNTID_CHR,PAYMONEYID_CHR,ACTIVATOR_CHR,ACTIVATETYPE_INT,ISRICH_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(32, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strPCHARGEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = DateTime.Parse(p_objRecord.m_strACTIVE_DAT);
                objLisAddItemRefArr[4].Value = p_objRecord.m_strORDERID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intORDEREXECTYPE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strORDEREXECID_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strCLACAREA_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strCREATEAREA_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strCALCCATEID_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strINVCATEID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strCHARGEITEMID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strCHARGEITEMNAME_CHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strUNIT_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_dblUNITPRICE_DEC;

                objLisAddItemRefArr[15].Value = p_objRecord.m_fltAMOUNT_DEC;

                objLisAddItemRefArr[16].Value = p_objRecord.m_fltDISCOUNT_DEC;
                objLisAddItemRefArr[17].Value = p_objRecord.m_intISMEPAY_INT;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_intCREATETYPE_INT;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCREATOR_CHR;
                objLisAddItemRefArr[21].Value = DateTime.Parse(strDateTime);//p_objRecord.m_strCREATE_DAT
                objLisAddItemRefArr[22].Value = p_objRecord.m_strOPERATOR_CHR;
                objLisAddItemRefArr[23].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[24].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[25].Value = p_objRecord.m_intPSTATUS_INT;
                try
                {
                    objLisAddItemRefArr[26].Value = DateTime.Parse(p_objRecord.m_strCHEARACCOUNT_DAT);
                }
                catch
                {
                    objLisAddItemRefArr[26].Value = null;
                }
                objLisAddItemRefArr[27].Value = p_objRecord.m_strDAYACCOUNTID_CHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strPAYMONEYID_CHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strACTIVATOR_CHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_intACTIVATETYPE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intISRICH_INT;
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
        /// <summary>
        /// ������������������ϸ	[����]
        /// </summary>
        /// <param name="p_strRecordIDArr">����������ϸ��ˮ��</param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatientChargeForMore(out string[] p_strRecordIDArr, clsT_opr_bih_patientcharge_VO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_strRecordIDArr = null;
            p_strRecordIDArr = new string[p_objRecordArr.Length];
            for (int i1 = 0; i1 < p_objRecordArr.Length; i1++)
            {
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngAddNewPatientCharge(out p_strRecordIDArr[i1], p_objRecordArr[i1]);
                }
            }
            if (lngRes <= 0)
            {
                ContextUtil.SetAbort();
            }
            return lngRes;
        }
        #endregion
        #region ֱ�ӽ���
        [AutoComplete]
        public long m_lngDirectCharge(clsT_opr_bih_dayaccount_VO p_objRecord, clsT_opr_bih_patientcharge_VO[] p_objItems)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            lngRes = this.m_lngAddNewSettleAccount(p_objRecord, out p_strRecordID);
            if (lngRes > 0)
            {
                for (int i = 0; i < p_objItems.Length; i++)
                {
                    string ID;
                    p_objItems[i].m_strDAYACCOUNTID_CHR = p_strRecordID;
                    lngRes = 0;
                    lngRes = this.m_lngAddNewPatientCharge(out ID, p_objItems[i]);
                    if (lngRes <= 0)
                    {
                        break;
                    }
                }
            }

            return lngRes;
        }
        #endregion
        #region ����
        #region �޸Ĳ���������ϸ
        /// <summary>
        /// �޸Ĳ���������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">������ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientCharge(clsT_opr_bih_patientcharge_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
							UPDATE  T_OPR_BIH_PATIENTCHARGE A
							SET
								A.PCHARGEID_CHR = '[PCHARGEID_CHR]'
								, A.PATIENTID_CHR = '[PATIENTID_CHR]'
								, A.REGISTERID_CHR = '[REGISTERID_CHR]'
								, A.ACTIVE_DAT =  TO_DATE('[ACTIVE_DAT]','YYYY-MM-DD hh24:mi:ss')
								, A.ORDERID_CHR =  '[ORDERID_CHR]'
								, A.ORDEREXECTYPE_INT =  '[ORDEREXECTYPE_INT]'
								, A.ORDEREXECID_CHR =  '[ORDEREXECID_CHR]'
								, A.CLACAREA_CHR =  '[CLACAREA_CHR]'
								, A.CREATEAREA_CHR =  '[CREATEAREA_CHR]'
								, A.CALCCATEID_CHR =  '[CALCCATEID_CHR]'
								, A.INVCATEID_CHR =  '[INVCATEID_CHR]'
								, A.CHARGEITEMID_CHR =  '[CHARGEITEMID_CHR]'
								, A.CHARGEITEMNAME_CHR =  '[CHARGEITEMNAME_CHR]'
								, A.UNIT_VCHR =  '[UNIT_VCHR]'
								, A.UNITPRICE_DEC =  '[UNITPRICE_DEC]'
								, A.AMOUNT_DEC =  '[AMOUNT_DEC]'
								, A.DISCOUNT_DEC =  '[DISCOUNT_DEC]'
								, A.ISMEPAY_INT =  '[ISMEPAY_INT]'
								, A.DES_VCHR =  '[DES_VCHR]'
								, A.CREATETYPE_INT =  '[CREATETYPE_INT]'
								, A.CREATOR_CHR =  '[CREATOR_CHR]'
								, A.CREATE_DAT =  TO_DATE('[CREATE_DAT]','YYYY-MM-DD hh24:mi:ss')
								, A.OPERATOR_CHR =  '[OPERATOR_CHR]'
								, A.MODIFY_DAT =  TO_DATE('[MODIFY_DAT]','YYYY-MM-DD hh24:mi:ss')
								, A.STATUS_INT =  '[STATUS_INT]'
								, A.PSTATUS_INT =  '[PSTATUS_INT]'
								, A.ACTIVATOR_CHR =  '[ACTIVATOR_CHR]'
								, A.ACTIVATETYPE_INT =  '[ACTIVATETYPE_INT]'
							WHERE A.PCHARGEID_CHR =  '[PCHARGEID_CHR]'";
            strSQL = strSQL.Replace("[PCHARGEID_CHR]", p_objRecord.m_strPCHARGEID_CHR.Trim());
            strSQL = strSQL.Replace("[PATIENTID_CHR]", p_objRecord.m_strPATIENTID_CHR.Trim());
            strSQL = strSQL.Replace("[REGISTERID_CHR]", p_objRecord.m_strREGISTERID_CHR.Trim());
            strSQL = strSQL.Replace("[ACTIVE_DAT]", p_objRecord.m_strACTIVE_DAT.Trim());
            strSQL = strSQL.Replace("[ORDERID_CHR]", (p_objRecord.m_strORDERID_CHR == null) ? ("") : (p_objRecord.m_strORDERID_CHR.Trim()));
            strSQL = strSQL.Replace("[ORDEREXECTYPE_INT]", p_objRecord.m_intORDEREXECTYPE_INT.ToString().Trim());
            strSQL = strSQL.Replace("[ORDEREXECID_CHR]", (p_objRecord.m_strORDEREXECID_CHR == null) ? ("") : (p_objRecord.m_strORDEREXECID_CHR.Trim()));
            strSQL = strSQL.Replace("[CLACAREA_CHR]", (p_objRecord.m_strCLACAREA_CHR == null) ? ("") : (p_objRecord.m_strCLACAREA_CHR.Trim()));
            strSQL = strSQL.Replace("[CREATEAREA_CHR]", (p_objRecord.m_strCREATEAREA_CHR == null) ? ("") : (p_objRecord.m_strCREATEAREA_CHR.Trim()));
            strSQL = strSQL.Replace("[CALCCATEID_CHR]", (p_objRecord.m_strCALCCATEID_CHR == null) ? ("") : (p_objRecord.m_strCALCCATEID_CHR.Trim()));
            strSQL = strSQL.Replace("[INVCATEID_CHR]", (p_objRecord.m_strINVCATEID_CHR == null) ? ("") : (p_objRecord.m_strINVCATEID_CHR.Trim()));
            strSQL = strSQL.Replace("[CHARGEITEMID_CHR]", (p_objRecord.m_strCHARGEITEMID_CHR == null) ? ("") : (p_objRecord.m_strCHARGEITEMID_CHR.Trim()));
            strSQL = strSQL.Replace("[CHARGEITEMNAME_CHR]", (p_objRecord.m_strCHARGEITEMNAME_CHR == null) ? ("") : (p_objRecord.m_strCHARGEITEMNAME_CHR.Trim()));
            strSQL = strSQL.Replace("[UNIT_VCHR]", (p_objRecord.m_strUNIT_VCHR == null) ? ("") : (p_objRecord.m_strUNIT_VCHR.Trim()));
            strSQL = strSQL.Replace("[UNITPRICE_DEC]", p_objRecord.m_dblUNITPRICE_DEC.ToString().Trim());
            strSQL = strSQL.Replace("[AMOUNT_DEC]", p_objRecord.m_fltAMOUNT_DEC.ToString().Trim());
            strSQL = strSQL.Replace("[DISCOUNT_DEC]", p_objRecord.m_fltDISCOUNT_DEC.ToString().Trim());
            strSQL = strSQL.Replace("[ISMEPAY_INT]", p_objRecord.m_intISMEPAY_INT.ToString().Trim());
            strSQL = strSQL.Replace("[DES_VCHR]", (p_objRecord.m_strDES_VCHR == null) ? ("") : (p_objRecord.m_strDES_VCHR.Trim()));
            strSQL = strSQL.Replace("[CREATETYPE_INT]", p_objRecord.m_intCREATETYPE_INT.ToString().Trim());
            strSQL = strSQL.Replace("[CREATOR_CHR]", (p_objRecord.m_strCREATOR_CHR == null) ? ("") : (p_objRecord.m_strCREATOR_CHR.Trim()));
            strSQL = strSQL.Replace("[CREATE_DAT]", p_objRecord.m_strCREATE_DAT.Trim());
            strSQL = strSQL.Replace("[OPERATOR_CHR]", (p_objRecord.m_strOPERATOR_CHR == null) ? ("") : (p_objRecord.m_strOPERATOR_CHR.Trim()));
            strSQL = strSQL.Replace("[MODIFY_DAT]", strDateTime);//p_objRecord.m_strMODIFY_DAT.Trim()
            strSQL = strSQL.Replace("[STATUS_INT]", p_objRecord.m_intSTATUS_INT.ToString().Trim());
            strSQL = strSQL.Replace("[PSTATUS_INT]", p_objRecord.m_intPSTATUS_INT.ToString().Trim());
            strSQL = strSQL.Replace("[ACTIVATOR_CHR]", (p_objRecord.m_strACTIVATOR_CHR == null) ? ("") : (p_objRecord.m_strACTIVATOR_CHR.Trim()));
            strSQL = strSQL.Replace("[ACTIVATETYPE_INT]", p_objRecord.m_intACTIVATETYPE_INT.ToString().Trim());
            strSQL = strSQL.Replace("[PCHARGEID_CHR]", p_objRecord.m_strPCHARGEID_CHR.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ȷ�ϼ���
        /// <summary>
        /// ȷ�ϼ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_strOperatorID">ȷ�ϼ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientCharge(string p_strRecordID, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  ACTIVE_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//��Ч����	{����״̬����ʱ���� if(!����״̬==0)}
            strSQL += "  , ACTIVATOR_CHR ='" + p_strOperatorID.Trim() + "'";//��Ч��{=��Ա.id}
            strSQL += "  , ACTIVATETYPE_INT =3";//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "  , OPERATOR_CHR ='" + p_strOperatorID.Trim() + "'";
            strSQL += "  , MODIFY_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , PSTATUS_INT =1";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += " WHERE PCHARGEID_CHR='" + p_strRecordID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ȷ���շ�
        /// <summary>
        /// ȷ���շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_strOperatorID">ȷ���շ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyConfirmCharge(string p_strRecordID, string p_strPayMoneyID, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  ACTIVE_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//��Ч����	{����״̬����ʱ���� if(!����״̬==0)}
            strSQL += "  , ACTIVATOR_CHR ='" + p_strOperatorID.Trim() + "'";//��Ч��{=��Ա.id}
            strSQL += "  , ACTIVATETYPE_INT =4";//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ�ӽ���}
            strSQL += "  , OPERATOR_CHR ='" + p_strOperatorID.Trim() + "'";
            strSQL += "  , MODIFY_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , PSTATUS_INT =4";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "  , PAYMONEYID_CHR ='" + p_strPayMoneyID.Trim() + "'";//���Ѽ�¼id{=���Ѽ�¼.ID}
            strSQL += "  , CHEARACCOUNT_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//��������
            strSQL += " WHERE PCHARGEID_CHR='" + p_strRecordID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ����
        /// <summary>
        /// ������ˮ������-������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strPayMoneyID">������ˮ��</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long ClearupPatientChargeByID(string p_strID, string p_strPayMoneyID, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  PSTATUS_INT =3";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "  , OPERATOR_CHR ='" + p_strOperatorID.Trim() + "'";//�޸���	{=��Ա.id}
            strSQL += "  , MODIFY_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//�޸�ʱ��
            strSQL += "  , PAYMONEYID_CHR ='" + p_strPayMoneyID.Trim() + "'";//���Ѽ�¼id{=���Ѽ�¼.ID}
            strSQL += "  , CHEARACCOUNT_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//��������
            strSQL += " WHERE PCHARGEID_CHR='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        /// ����������ˮ������-������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDayAccountID">������ˮ��</param>
        /// <param name="p_strPayMoneyID">������ˮ��</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long ClearupPatientCharge(string p_strDayAccountID, string p_strPayMoneyID, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  PSTATUS_INT =3";//����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "  , OPERATOR_CHR ='" + p_strOperatorID.Trim() + "'";//�޸���	{=��Ա.id}
            strSQL += "  , MODIFY_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//�޸�ʱ��
            strSQL += "  , PAYMONEYID_CHR ='" + p_strPayMoneyID.Trim() + "'";//���Ѽ�¼id{=���Ѽ�¼.ID}
            strSQL += "  , CHEARACCOUNT_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//��������
            strSQL += " WHERE DAYACCOUNTID_CHR='" + p_strDayAccountID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region �޸�ȷ�ϼ���
        /// <summary>
        /// �޸�ȷ�ϼ��˼�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_strCalccateID">סԺ���ú������id</param>
        /// <param name="p_strInvcateID">סԺ���÷�Ʊ���id</param>
        /// <param name="p_intISMEPAY">�Ƿ��Է���Ŀ	{=�շ���Ŀ.�Ƿ��Է���Ŀ}</param>
        /// <param name="p_strOperatorID">ȷ�ϼ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientCharge(string p_strRecordID, string p_strCalccateID, string p_strInvcateID, int p_intISMEPAY, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  CALCCATEID_CHR ='" + p_strCalccateID.Trim() + "'";
            strSQL += "  , INVCATEID_CHR ='" + p_strInvcateID.Trim() + "'";
            strSQL += "  , ISMEPAY_INT =" + p_intISMEPAY.ToString();
            strSQL += "  , OPERATOR_CHR ='" + p_strOperatorID.Trim() + "'";
            strSQL += "  , MODIFY_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += " WHERE PCHARGEID_CHR='" + p_strRecordID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region	�޸���Ʊ
        /// <summary>
        ///  �޸Ĳ���������ϸ	���Ѽ�¼ID	[������Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPayMoneyID">���Ѽ�¼ID</param>
        /// <param name="p_alDayAccountID">����ID</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <param name="p_strOperatorDateTime">����ʱ��	[Ϊ��,����ϵͳ��ǰʱ��]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatientChargeByPayMoneyID(string p_strPayMoneyID, string p_strOperatorID, string p_strOperatorDateTime, out List<string> p_alDayAccountID)
        {
            long lngRes = 0;
            p_alDayAccountID = new List<string>();
            System.Collections.ArrayList p_alRecordID = new System.Collections.ArrayList();
            string strSQL = "";
            //���ҽɷѼ�¼��Ӧ��ȫ������
            if (lngRes > 0)
            {
                #region ���ҽɷѼ�¼��Ӧ��ȫ������
                strSQL = @"
					SELECT dayaccountid_chr
					FROM t_opr_bih_patientcharge
					WHERE paymoneyid_chr = '[PAYMONEYID]'";
                strSQL = strSQL.Replace("[PAYMONEYID]", p_strPayMoneyID.Trim());
                try
                {
                    DataTable dtResult = new DataTable();
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                    objHRPSvc.Dispose();
                    if (lngRes > 0 && dtResult.Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                        {
                            if (dtResult.Rows[i1][0] != System.DBNull.Value)
                            {
                                if (!p_alDayAccountID.Contains(dtResult.Rows[i1][0].ToString().Trim()))
                                {
                                    p_alDayAccountID.Add(dtResult.Rows[i1][0].ToString().Trim());
                                }
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
                #endregion
            }
            //��ԭ������ǰ��״̬
            if (lngRes > 0)
            {
                #region ��ԭ������ǰ��״̬
                string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strSQL = @"
						UPDATE t_opr_bih_patientcharge
						SET pstatus_int = 2,
							chearaccount_dat = TO_DATE (NULL),
							paymoneyid_chr = NULL,
							operator_chr = [OPERATOR],
							modify_dat = TO_DATE('[MODIFY]','YYYY-MM-DD hh24:mi:ss')
						WHERE paymoneyid_chr = '[PAYMONEYID]'";
                if (p_strOperatorDateTime == null || p_strOperatorDateTime.Trim() == "")
                    p_strOperatorDateTime = strDateTime;
                try { DateTime dt = Convert.ToDateTime(p_strOperatorDateTime); }
                catch { p_strOperatorDateTime = strDateTime; }
                strSQL = strSQL.Replace("[OPERATOR]", p_strOperatorID.Trim());
                strSQL = strSQL.Replace("[MODIFY]", p_strOperatorDateTime.Trim());
                strSQL = strSQL.Replace("[PAYMONEYID]", p_strPayMoneyID.Trim());
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
                #endregion
            }

            if (lngRes <= 0)
            {
                ContextUtil.SetAbort();
            }
            return lngRes;
        }
        #endregion
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_strOperatorID">ɾ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeletePatientCharge(string p_strRecordID, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE ";
            strSQL += " SET";
            strSQL += "  deactivator_chr ='" + p_strOperatorID.Trim() + "'";
            strSQL += "  , deactivate_dat =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , status_int=0";
            strSQL += " WHERE PCHARGEID_CHR='" + p_strRecordID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        /// ɾ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordIDArr">[����]	��ˮ��</param>
        /// <param name="p_strOperatorID">ɾ����ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeletePatientChargeForMore(string[] p_strRecordIDArr, string p_strOperatorID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"
					UPDATE T_OPR_BIH_PATIENTCHARGE  SET
						deactivator_chr ='[OperatorID]' 
						, deactivate_dat =TO_DATE('[DateTime]','YYYY-MM-DD hh24:mi:ss')
						, status_int=0 
					WHERE [PCHARGEID]";
            if (p_strRecordIDArr.Length <= 0) return 1;
            string strTem = " (PCHARGEID_CHR='" + p_strRecordIDArr[0].Trim() + "'";
            for (int i1 = 1; i1 < p_strRecordIDArr.Length; i1++)
            {
                strTem += " OR PCHARGEID_CHR='" + p_strRecordIDArr[i1].Trim() + "'";
            }
            strTem += ")";
            strSQL = strSQL.Replace("[OperatorID]", p_strOperatorID.Trim());
            strSQL = strSQL.Replace("[DateTime]", strDateTime.Trim());
            strSQL = strSQL.Replace("[PCHARGEID]", strTem.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ����
        /// <summary>
        /// ��ȡ�շ���Ŀ���	��������־ [1��������㣬2�����﷢Ʊ��3��סԺ���㣬4��סԺ��Ʊ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFlag">����־ [1��������㣬2�����﷢Ʊ��3��סԺ���㣬4��סԺ��Ʊ]</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemType(int p_intFlag, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += " FROM t_bse_chargeitemextype a";
            strSQL += " WHERE a.flag_int=" + p_intFlag.ToString();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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

        //���ʼ�¼(���˽ɷѳ岡��Ԥ����)
        #region ����
        /// <summary>
        /// �������ʼ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPayAndPrepayMap(out string p_strRecordID, clsT_opr_bih_payandprepaymap_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(7, "MAPID_CHR", "t_opr_bih_payandprepaymap", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_bih_payandprepaymap (MAPID_CHR,PAYMONEYID_CHR,PREPAYID_CHR) VALUES (?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strMAPID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPAYMONEYID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strPREPAYID_CHR;
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

        //��Ʊ��¼
        #region ����
        /// <summary>
        /// ���ҷ�Ʊ��¼	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult">��Ʊ��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoiceByID(string p_strID, out clsT_Opr_Bih_Invoice_Vo p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Invoice_Vo();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_Opr_Bih_Invoice WHERE INVOICEID_CHR = '" + p_strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_Invoice_Vo();
                    p_objResult.m_strINVOICEID_CHR = dtbResult.Rows[0]["INVOICEID_CHR"].ToString().Trim();
                    p_objResult.m_strINVOICENO_CHR = dtbResult.Rows[0]["INVOICENO_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["PSTATUS_INT"] != System.DBNull.Value)
                    {
                        p_objResult.m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_INT"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["MONEY_DEC"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblMONEY_DEC = Convert.ToDouble(dtbResult.Rows[0]["MONEY_DEC"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["CREATEDATE_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    if (dtbResult.Rows[0]["LASTUPDATEDATE_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strLASTUPDATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["LASTUPDATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    p_objResult.m_strCREATOR_CHR = dtbResult.Rows[0]["CREATOR_CHR"].ToString().Trim();
                    p_objResult.m_strLASTUPDATOR_CHR = dtbResult.Rows[0]["LASTUPDATOR_CHR"].ToString().Trim();
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
        /// ���ҷ�Ʊ��¼	��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoice(string p_strCondition, out clsT_Opr_Bih_Invoice_Vo[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Invoice_Vo[0];
            long lngRes = 0;
            if (p_strCondition.Trim() != "" && (!p_strCondition.Trim().ToLower().StartsWith("where"))) p_strCondition = "Where " + p_strCondition;
            string strSQL = @"SELECT * FROM T_Opr_Bih_Invoice " + p_strCondition.Trim();
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Invoice_Vo[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Invoice_Vo();
                        p_objResultArr[i1].m_strINVOICEID_CHR = dtbResult.Rows[i1]["INVOICEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVOICENO_CHR = dtbResult.Rows[i1]["INVOICENO_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["PSTATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["MONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblMONEY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["MONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        if (dtbResult.Rows[i1]["LASTUPDATEDATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strLASTUPDATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["LASTUPDATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strLASTUPDATOR_CHR = dtbResult.Rows[i1]["LASTUPDATOR_CHR"].ToString().Trim();
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
        #region ����
        /// <summary>
        /// ������Ʊ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord">��Ʊ��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewInvoice(out string p_strRecordID, clsT_Opr_Bih_Invoice_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(7, "INVOICEID_CHR", "T_Opr_Bih_Invoice", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Opr_Bih_Invoice (INVOICEID_CHR,INVOICENO_CHR,PSTATUS_INT,MONEY_DEC,CREATEDATE_DAT,CREATOR_CHR) VALUES (?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strINVOICEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strINVOICENO_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_intPSTATUS_INT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_dblMONEY_DEC;
                objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strCREATEDATE_DAT);
                //objLisAddItemRefArr[5].Value = DateTime.Parse(p_objRecord.m_strLASTUPDATEDATE_DAT);
                objLisAddItemRefArr[5].Value = p_objRecord.m_strCREATOR_CHR;
                //objLisAddItemRefArr[6].Value = p_objRecord.m_strLASTUPDATOR_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
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
        #region �޸�
        /// <summary>
        /// �޸ķ�Ʊ��¼״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_intPStatus">״̬{1=����;2=����;3=��Ʊ}</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <param name="p_strOperateDateTime">����ʱ��	��ʽ��:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyInvoice(string p_strID, int p_intPStatus, string p_strOperatorID, string p_strOperateDateTime)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            #region SQL
            string strSQL = @"
						UPDATE t_opr_bih_invoice a
						SET a.pstatus_int = [pstatus],
							a.lastupdatedate_dat = TO_DATE('[lastupdatedate]','YYYY-MM-DD hh24:mi:ss'),
							a.lastupdator_chr = '[lastupdator]'
						WHERE invoiceid_chr = '[invoiceid]'";
            if (p_strOperateDateTime == null || p_strOperateDateTime.Trim() == "") p_strOperateDateTime = strDateTime;
            try { DateTime dt = Convert.ToDateTime(p_strOperateDateTime); }
            catch { p_strOperateDateTime = strDateTime; }

            strSQL = strSQL.Replace("[pstatus]", p_intPStatus.ToString());
            strSQL = strSQL.Replace("[lastupdatedate]", p_strOperateDateTime.Trim());
            strSQL = strSQL.Replace("[lastupdator]", p_strOperatorID.Trim());
            strSQL = strSQL.Replace("[invoiceid]", p_strID.Trim());
            #endregion
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

        //��Ʊ��������
        #region ����
        /// <summary>
        /// ���ҷ�Ʊ��������	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult">��Ʊ�����������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoicecat(string p_strID, out clsT_Opr_Bih_Invoicecat_Vo p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Invoicecat_Vo();
            long lngRes = 0;
            string strSQL = @"
						SELECT a.*
							,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE flag_int=4 AND typeid_chr=a.categoryid_chr) CategoryName
						FROM t_opr_bih_invoicecat a
						WHERE a.invcatid_chr = '" + p_strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_Invoicecat_Vo();
                    p_objResult.m_strINVCATID_CHR = dtbResult.Rows[0]["INVCATID_CHR"].ToString().Trim();
                    p_objResult.m_strINVOICEID_CHR = dtbResult.Rows[0]["INVOICEID_CHR"].ToString().Trim();
                    p_objResult.m_strCATEGORYID_CHR = dtbResult.Rows[0]["CATEGORYID_CHR"].ToString().Trim();
                    p_objResult.m_strCategoryName = dtbResult.Rows[0]["CategoryName"].ToString().Trim();
                    if (dtbResult.Rows[0]["MONEY_DEC"] != System.DBNull.Value)
                    {
                        p_objResult.m_dblMONEY_DEC = Convert.ToDouble(dtbResult.Rows[0]["MONEY_DEC"].ToString().Trim());
                    }
                    if (dtbResult.Rows[0]["CREATEDATE_DAT"] != System.DBNull.Value)
                    {
                        p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        /// ���ҷ�Ʊ��������	���ݷ�ƱID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInvoiceID">��ƱID</param>
        /// <param name="p_objResultArr">��Ʊ�����������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoicecatByInvoiceID(string p_strInvoiceID, out clsT_Opr_Bih_Invoicecat_Vo[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Invoicecat_Vo[0];
            long lngRes = 0;
            string strSQL = @"
						SELECT	a.* 
								,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE flag_int=4 AND typeid_chr=a.categoryid_chr) CategoryName
						FROM T_Opr_Bih_Invoicecat a 
						WHERE a.invoiceid_chr = '" + p_strInvoiceID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Invoicecat_Vo[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Invoicecat_Vo();
                        p_objResultArr[i1].m_strINVCATID_CHR = dtbResult.Rows[i1]["INVCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVOICEID_CHR = dtbResult.Rows[i1]["INVOICEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCATEGORYID_CHR = dtbResult.Rows[i1]["CATEGORYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCategoryName = dtbResult.Rows[i1]["CategoryName"].ToString().Trim();
                        if (dtbResult.Rows[i1]["MONEY_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblMONEY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["MONEY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
        #region ����
        /// <summary>
        /// ������Ʊ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��</param>
        /// <param name="p_objRecord">��Ʊ�����������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewInvoicecat(out string p_strRecordID, clsT_Opr_Bih_Invoicecat_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(7, "INVCATID_CHR", "T_Opr_Bih_Invoicecat", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Opr_Bih_Invoicecat (INVCATID_CHR,INVOICEID_CHR,CATEGORYID_CHR,MONEY_DEC,CREATEDATE_DAT) VALUES (?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strINVCATID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strINVOICEID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strCATEGORYID_CHR;
                objLisAddItemRefArr[3].Value = -p_objRecord.m_dblMONEY_DEC;
                objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strCREATEDATE_DAT);
                long lngRecEff = -1;
                //�������Ӽ�¼
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
        /// ������Ʊ��������	������Ʊ
        /// ҵ��˵��: ���շ�Ʊ��ѯ,ÿ�ŷ�Ʊ��¼����һ�������ļ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInvoiceID">��ƱID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewInvoicecat(string p_strInvoiceID)
        {
            long lngRes = 0;
            System.Collections.ArrayList p_alRecordID = new System.Collections.ArrayList();
            clsT_Opr_Bih_Invoicecat_Vo[] objItemArr = new clsT_Opr_Bih_Invoicecat_Vo[0];
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngGetInvoicecatByInvoiceID(p_strInvoiceID, out objItemArr);
            }

            if (lngRes > 0 && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    string strRecordID = "";
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngAddNewInvoicecat(out strRecordID, objItemArr[i1]);
                        if (!p_alRecordID.Contains(strRecordID.Trim())) p_alRecordID.Add(strRecordID.Trim());
                    }
                }
            }
            if (lngRes <= 0)
            {
                throw (new Exception("����ʧ��!"));
            }
            return lngRes;
        }
        #endregion

        //�ۺ�
        #region �ۺ�
        #region ����SQL��䷵��DataTable
        /// <summary>
        /// ����SQL��䷵��DataTable
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQL">��ѯ��SQL���</param>
        /// <param name="p_dtbResult">DataTable [out����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataTableBySQL(string p_strSQL, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtResult);
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
        #region ����Ʊ�����ܴ��������ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]}
        /// <summary>
        /// ����Ʊ�����ܷ�����ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ���ˮ��</param>
        /// <param name="p_strDayAccountIDArr">������ˮ��[����]</param>
        /// <param name="p_intStatus">����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��;	ע�⣺С��0�����״̬����Ϊ��ѯ����}</param>
        /// <param name="p_dtResult"></param>
        [AutoComplete]
        public long lngCollectPatientChargeByDayAccountID(string p_strRegisterID, string[] p_strDayAccountIDArr, int p_intStatus, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = " ";
            strSQL += " SELECT  a.invcateid_chr";
            strSQL += "         ,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4) InvcateName";//��Ʊ���
            strSQL += "         ,Sum(UNITPRICE_DEC*AMOUNT_DEC) TotalMoney";
            strSQL += " FROM t_opr_bih_patientcharge a,t_opr_bih_dayaccount b";
            strSQL += " WHERE ";
            strSQL += "     a.dayaccountid_chr=b.dayaccountid_chr AND a.status_int=1";
            if (p_intStatus >= 0)
            {
                strSQL += "     AND a.pstatus_int=" + p_intStatus.ToString();//��Ч�� ����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}			
            }
            strSQL += "     and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            //���������ˮ����������
            strSQL += " and (1=2 ";
            for (int i = 0; i < p_strDayAccountIDArr.Length; i++)
            {
                if (p_strDayAccountIDArr[i] != string.Empty)
                {
                    strSQL += " or a.dayaccountid_chr='" + p_strDayAccountIDArr[i].Trim() + "' ";
                }
            }
            strSQL += " )";
            strSQL += " Group BY a.invcateid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ����Ʊ�����ܴ��������ϸ {��Ժ�ǼǺ���ˮ�š�������ϸ��ˮ��[����]}
        /// <summary>
        /// ����Ʊ�����ܴ��������ϸ {��Ժ�ǼǺ���ˮ�š�������ϸ��ˮ��[����]}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ���ˮ��</param>
        /// <param name="p_strPChargeIdArr">������ϸ��ˮ��[����]</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngCollectPatientChargeByPChargeId(string p_strRegisterID, string[] p_strPChargeIdArr, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = " ";
            strSQL += " SELECT  a.invcateid_chr";
            strSQL += "         ,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4) InvcateName";//��Ʊ���
            strSQL += "         ,Sum(UNITPRICE_DEC*AMOUNT_DEC) TotalMoney";
            strSQL += " FROM t_opr_bih_patientcharge a";
            strSQL += " WHERE ";
            strSQL += "     a.status_int=1 and a.pstatus_int=2 ";//�����.��Ч�� ����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "     and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            //���������ˮ����������
            strSQL += " and (1=2 ";
            for (int i = 0; i < p_strPChargeIdArr.Length; i++)
            {
                if (p_strPChargeIdArr[i] != string.Empty)
                {
                    strSQL += " or a.pchargeid_chr='" + p_strPChargeIdArr[i].Trim() + "' ";
                }
            }
            strSQL += " )";
            strSQL += " Group BY a.invcateid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ��ȡ������ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]����Ʊ���ID}
        /// <summary>
        /// ��ȡ���������ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]����Ʊ���ID}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ���ˮ��</param>
        /// <param name="p_strDayAccountIDArr">������ˮ��[����]</param>
        /// <param name="p_strInvcateID">��Ʊ���ID</param>
        /// <param name="p_intStatus">����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��;	ע�⣺С��0�����״̬����Ϊ��ѯ����}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetPatientCharge(string p_strRegisterID, string[] p_strDayAccountIDArr, string p_strInvcateID, int p_intStatus, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,decode(ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName,unitprice_dec * amount_dec totalmoney";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += @" FROM (SELECT a.*,b.itemspec_vchr
  FROM t_opr_bih_patientcharge a, T_BSE_CHARGEITEM b
 WHERE a.chargeitemid_chr = b.itemid_chr(+)) a,t_opr_bih_dayaccount b";
            strSQL += " WHERE ";
            strSQL += "     a.dayaccountid_chr=b.dayaccountid_chr AND a.status_int=1 ";
            strSQL += "		and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            strSQL += " and a.invcateid_chr='" + p_strInvcateID.Trim() + "'";
            if (p_intStatus >= 0)
            {
                strSQL += "     AND a.pstatus_int=" + p_intStatus.ToString();//��Ч�� ����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}			
            }
            //���������ˮ����������
            strSQL += " and (1=2 ";
            for (int i = 0; i < p_strDayAccountIDArr.Length; i++)
            {
                if (p_strDayAccountIDArr[i] != string.Empty)
                {
                    strSQL += " or a.dayaccountid_chr='" + p_strDayAccountIDArr[i].Trim() + "' ";
                }
            }
            strSQL += " )";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ��ȡ������ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]}
        /// <summary>
        /// ��ȡ������ϸ {��Ժ�ǼǺ���ˮ�š�������ˮ��[����]}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ���ˮ��</param>
        /// <param name="p_strDayAccountIDArr">������ˮ��[����]</param>
        /// <param name="p_intStatus">����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��;	ע�⣺С��0�����״̬����Ϊ��ѯ����}</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetPatientCharge(string p_strRegisterID, string[] p_strDayAccountIDArr, int p_intStatus, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT a.* ";
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.clacarea_chr)ClacAreaName"; //--���㲡��
            strSQL += "		,(SELECT deptname_vchr FROM t_bse_deptdesc WHERE t_bse_deptdesc.deptid_chr=a.createarea_chr)CreateAreaName";//--�����ص�
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.calccateid_chr and flag_int=3)ItemIpcalcTypeName";//--��ĿסԺ�������
            strSQL += "		,(SELECT typename_vchr FROM t_bse_chargeitemextype WHERE typeid_chr=a.invcateid_chr and flag_int=4)ItemIpinvTypeName";//--��ĿסԺ��Ʊ���
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.creator_chr)CreatorName";//--¼����
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.operator_chr)OperatorName";//--�޸��� 
            strSQL += "		,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.deactivator_chr)DeactivatorName";//--ɾ����
            strSQL += "		,decode(ismepay_int,1,'�Է�',0,'���Է�','') IsmePayName";//--�Ƿ��Է�
            strSQL += "		,decode(createtype_int,1,'�Զ�(ҽ��)',2,'�Զ�(�մ���)',3,'����(ҽ��)',4,'�շѴ�����(��ҽ��)',5,'��������(��ҽ��)','') CreateTypeName,unitprice_dec * amount_dec totalmoney";//¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            strSQL += @" FROM (SELECT a.*,b.itemspec_vchr
  FROM t_opr_bih_patientcharge a, T_BSE_CHARGEITEM b
 WHERE a.chargeitemid_chr = b.itemid_chr(+)) a,t_opr_bih_dayaccount b";
            strSQL += " WHERE ";
            strSQL += "     a.dayaccountid_chr=b.dayaccountid_chr AND a.status_int=1 ";
            strSQL += "		and a.registerid_chr='" + p_strRegisterID.Trim() + "'";
            if (p_intStatus >= 0)
            {
                strSQL += "     AND a.pstatus_int=" + p_intStatus.ToString();//��Ч�� ����״̬{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}			
            }
            //���������ˮ����������
            strSQL += " and (1=2 ";
            for (int i = 0; i < p_strDayAccountIDArr.Length; i++)
            {
                if (p_strDayAccountIDArr[i] != string.Empty)
                {
                    strSQL += " or a.dayaccountid_chr='" + p_strDayAccountIDArr[i].Trim() + "' ";
                }
            }
            strSQL += " )";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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
        #region ��ȡ���˵Ļ�����Ϣ�ͷ�����Ϣ
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ż�ȡסԺ��Ϣ�Ͳ��˻�����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoByRegisterID(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            //������Ϣ	{סԺ�š��������Ա����䡢��������λ����Ժ���ڡ���Ժ����}
            strSQL += " select a.*,b.* ";
            strSQL += "		,TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(b.birth_dat,'YYYY') As Age";
            strSQL += "		,(select deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.areaid_chr) AreaName";
            strSQL += "		,(select code_chr from t_bse_bed a2 where a2.areaid_chr=a.areaid_chr and a2.bedid_chr=a.bedid_chr) BedCode";
            strSQL += "		,(select modify_dat from t_opr_bih_leave a3 where a3.registerid_chr=a.registerid_chr and a3.status_int=1) LeaveHospitalTime";
            //���ƿ���
            strSQL += "		,(SELECT p0.patientcardid_chr FROM t_bse_patientcard p0 WHERE p0.patientid_chr =a.patientid_chr) patientcardid_chr";
            //�Ը�: 
            strSQL += "		,(SELECT chargepercent_dec FROM t_bse_patientpaytype b1 WHERE (payflag_dec=0 OR payflag_dec=2) AND Trim(b1.paytypeid_chr)=Trim(b.paytypeid_chr)) chargepercent_dec";
            //����: 
            //ҩ��: 
            //����: 
            //������:  
            strSQL += "		,(TO_CHAR(sysdate,'yyyyMMdd')-TO_CHAR(a.inpatient_dat,'yyyyMMdd')) TotalDays";
            //δ������: 
            //���: 
            //�߿�����: 
            //��������: 
            strSQL += "		,(SELECT square_dat FROM (SELECT e0.square_dat FROM t_opr_bih_dayaccount e0 WHERE e0.registerid_chr='" + p_strRegisterID.Trim() + "' ORDER BY e0.square_dat desc) WHERE rownum=1) square_dat";
            //��������: 
            //��������: 
            strSQL += "		,(SELECT chearact_dat FROM (SELECT e.chearact_dat FROM t_opr_bih_paymoney e WHERE e.registerid_chr='" + p_strRegisterID.Trim() + "' ORDER BY e.chearact_dat desc) WHERE rownum=1) chearact_dat";
            //�ܷ���:	����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "		,(select sum(cAll.UNITPRICE_DEC * cAll.amount_dec) MoneycAll from t_opr_bih_patientcharge cAll where cAll.status_int=1 and cAll.registerid_chr='" + p_strRegisterID.Trim() + "') MoneycAll";
            //��ȷ��:		
            strSQL += "		,(select sum(c1.UNITPRICE_DEC * c1.amount_dec) Moneyc1 from t_opr_bih_patientcharge c1 where c1.status_int=1 and c1.registerid_chr='" + p_strRegisterID.Trim() + "' and c1.pstatus_int=0) Moneyc0";
            //����:		
            strSQL += "		,(select sum(c1.UNITPRICE_DEC * c1.amount_dec) Moneyc1 from t_opr_bih_patientcharge c1 where c1.status_int=1 and c1.registerid_chr='" + p_strRegisterID.Trim() + "' and c1.pstatus_int=1) Moneyc1";
            //����: 
            strSQL += "		,(select sum(c2.UNITPRICE_DEC * c2.amount_dec) Moneyc2 from t_opr_bih_patientcharge c2 where c2.status_int=1 and c2.registerid_chr='" + p_strRegisterID.Trim() + "' and c2.pstatus_int=2) Moneyc2";
            //����: 
            strSQL += "		,(select sum(c3.UNITPRICE_DEC * c3.amount_dec) Moneyc3 from t_opr_bih_patientcharge c3 where c3.status_int=1 and c3.registerid_chr='" + p_strRegisterID.Trim() + "' and c3.pstatus_int=3) Moneyc3";
            //ֱ�ӽ���: 
            strSQL += "		,(select sum(c4.UNITPRICE_DEC * c4.amount_dec) Moneyc4 from t_opr_bih_patientcharge c4 where c4.status_int=1 and c4.registerid_chr='" + p_strRegisterID.Trim() + "' and c4.pstatus_int=4) Moneyc4";

            //����: 
            //Ԥ��: [Ԥ�����ܶ�]
            strSQL += "		,(select sum(dAll.MONEY_DEC)  MoneydAll from t_opr_bih_prepay dAll where dAll.registerid_chr='" + p_strRegisterID.Trim() + "') MoneydAll";
            //����: [Ԥ������� - �����ܶ� - �����ܶ�] 
            strSQL += "		,(select sum(d1.MONEY_DEC) Moneyd1 from t_opr_bih_prepay d1 where d1.registerid_chr='" + p_strRegisterID.Trim() + "' and isclear_int=0) Moneyd1";
            strSQL += @"  ,DECODE (a.pstatus_int,
               0, 'δ�ϴ�',
               1, '���ϴ�',
               2, 'Ԥ��Ժ',
               3, 'ʵ�ʳ�Ժ',
               4, '���',
               ''
              ) AS state";
            //��ʾ: 
            strSQL += " from t_opr_bih_register a,t_bse_patient b";
            strSQL += " where a.patientid_chr=b.patientid_chr";
            strSQL += "     and a.registerid_chr='" + p_strRegisterID.Trim() + "'";

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
        #region �鿴�ɷѼ�¼
        [AutoComplete]
        public long m_lngGetPatientPayHistory(string p_strPatientID, string[] p_strTARGETFLAGArr, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            string strSQL = @"SELECT ROWID, t.create_dat, (SELECT a.lastname_vchr
                               FROM t_bse_employee a
                              WHERE a.empid_chr = t.creator_chr) AS creator,
       decode(t.squaretype_int,1,'�Է�',2,'ҽ��','') as squaretype, t.ysmoney_dec, t.bjmoney_dec, DECODE (t.targetflag_int, 1, 'ֱ��', 2, '����', 3, '����', '') as TargeFlag,
       t.cbmoney_dec, (SELECT a.lastname_vchr
                         FROM t_bse_employee a
                        WHERE a.empid_chr = t.clearactid_chr) AS clearact,
       t.chearact_dat, t.clearinv_vchr, t.balancetotype_int
  FROM t_opr_bih_paymoney t where t.PATIENTID_CHR = '" + p_strPatientID + @"'
";
            strSQL += " and ( 1=2";
            for (int i = 0; i < p_strTARGETFLAGArr.Length; i++)
            {
                strSQL += " or t.TARGETFLAG_INT = " + p_strTARGETFLAGArr[i].ToString();
            }
            strSQL += ")";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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


        //Add by jli 2005-03-02

        #region �������ƿ��Ż�ȡ����סԺ��
        /// <summary>
        /// �������ƿ��Ż�ȡ����סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strpatientcardid">���ƿ���</param>
        /// <param name="p_strregisterid">סԺ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfoBypatientcardid(string p_strpatientcardid, out string p_strregisterid)
        {
            p_strregisterid = "";
            long lngRes = 0;
            string strSQL = @"select * from t_bse_patientcard a
							left join t_opr_bih_register b
							on a.patientid_chr=b.patientid_chr
							where a.status_int=1 and b.status_int=1 and patientcardid_chr='" + p_strpatientcardid.Trim() + "'";
            try
            {
                DataTable dtbreg = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbreg);
                objHRPSvc.Dispose();
                if (lngRes <= 0)
                {
                    return -1;
                }
                if (dtbreg.Rows.Count <= 0 || dtbreg.Rows[0].IsNull("registerid_chr") || dtbreg.Rows[0]["registerid_chr"].ToString().Trim() == "")
                {
                    return -1;
                }
                p_strregisterid = dtbreg.Rows[0]["registerid_chr"].ToString().Trim();
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

        //Add end

        #region ��ȡ�ۼƷ���	������Ժ�Ǽ���ˮ��
        /// <summary>
        /// ��ȡ�ۼƷ���	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dblSumMoney">�ۼƷ��� [out ���� double����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSumMoneyByRegisterID(string p_strRegisterID, out double p_dblSumMoney)
        {
            long lngRes = -1;
            p_dblSumMoney = 0;
            string strSQL = @"
							SELECT SUM (CALL.unitprice_dec * CALL.amount_dec) moneycall
							FROM t_opr_bih_patientcharge CALL
							WHERE status_int = 1 AND TRIM (CALL.registerid_chr) ='" + p_strRegisterID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try { p_dblSumMoney = double.Parse(dtbResult.Rows[0]["MoneycAll"].ToString()); }
                    catch { }
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
        #region ��ȡ����	������Ժ�Ǽ���ˮ��
        /// <summary>
        /// ��ȡ����	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dblBalanceMoney">���� [out ���� double����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBalanceMoneyByRegisterID(string p_strRegisterID, out double p_dblBalanceMoney)
        {
            long lngRes = -1;
            p_dblBalanceMoney = 0;

            //��δ���Ԥ�����ܶ�	dblPrePayMoney
            double dblPrePayMoney = 0;
            //������ܶ�	dblWaitBalanceMoney
            double dblWaitBalanceMoney = 0;
            //������ܶ�	dblWaitAccountMoney
            double dblWaitAccountMoney = 0;
            string strSQL = "";
            strSQL += " select ";
            //�ܷ���:	����״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
            strSQL += "		(select sum(cAll.UNITPRICE_DEC * cAll.amount_dec) MoneycAll from t_opr_bih_patientcharge cAll where cAll.status_int = 1 AND cAll.registerid_chr='" + p_strRegisterID.Trim() + "') MoneycAll";
            //����:		
            strSQL += "		,(select sum(c1.UNITPRICE_DEC * c1.amount_dec) Moneyc1 from t_opr_bih_patientcharge c1 where c1.status_int = 1 AND c1.registerid_chr='" + p_strRegisterID.Trim() + "' and c1.pstatus_int=1) Moneyc1";
            //ֱ�ӽ���: 
            strSQL += "		,(select sum(c4.UNITPRICE_DEC * c4.amount_dec) Moneyc4 from t_opr_bih_patientcharge c4 where c4.status_int = 1 AND c4.registerid_chr='" + p_strRegisterID.Trim() + "' and c4.pstatus_int=4) Moneyc4";
            //����: 
            strSQL += "		,(select sum(c3.UNITPRICE_DEC * c3.amount_dec) Moneyc3 from t_opr_bih_patientcharge c3 where c3.status_int = 1 AND c3.registerid_chr='" + p_strRegisterID.Trim() + "' and c3.pstatus_int=3) Moneyc3";
            //����: 
            strSQL += "		,(select sum(c2.UNITPRICE_DEC * c2.amount_dec) Moneyc2 from t_opr_bih_patientcharge c2 where c2.status_int = 1 AND c2.registerid_chr='" + p_strRegisterID.Trim() + "' and c2.pstatus_int=2) Moneyc2";
            //Ԥ����: [�ܶ�]
            strSQL += "		,(select sum(dAll.MONEY_DEC)  MoneydAll from t_opr_bih_prepay dAll where dAll.status_int = 1 and dAll.registerid_chr='" + p_strRegisterID.Trim() + "') MoneydAll";
            //Ԥ����: [δ���ܶ�] 
            strSQL += "		,(select sum(d1.MONEY_DEC) Moneyd1 from t_opr_bih_prepay d1 where d1.status_int = 1 and d1.registerid_chr='" + p_strRegisterID.Trim() + "' and isclear_int=0) Moneyd1";
            strSQL += " from DUAL";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try { dblPrePayMoney = double.Parse(dtbResult.Rows[0]["Moneyd1"].ToString()); }
                    catch { }
                    try { dblWaitBalanceMoney = double.Parse(dtbResult.Rows[0]["Moneyc1"].ToString()); }
                    catch { }
                    try { dblWaitAccountMoney = double.Parse(dtbResult.Rows[0]["Moneyc2"].ToString()); }
                    catch { }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����: [Ԥ������� - �����ܶ� - �����ܶ�] 
            p_dblBalanceMoney = dblPrePayMoney - dblWaitBalanceMoney - dblWaitAccountMoney;
            return lngRes;
        }
        #endregion
        #region	��ȡ��������	������Ժ�Ǽ�ID
        /// <summary>
        /// ��ȡ����	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_dblLowerLimitMoney">�������� [out ���� double����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLowerLimitMoneyByRegisterID(string p_strRegisterID, out double p_dblLowerLimitMoney)
        {
            long lngRes = -1;
            p_dblLowerLimitMoney = 0;
            string strSQL = "select LIMITRATE_MNY from T_Opr_Bih_Register where Trim(registerid_chr)='" + p_strRegisterID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try { p_dblLowerLimitMoney = double.Parse(dtbResult.Rows[0]["LIMITRATE_MNY"].ToString()); }
                    catch { }
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
        #region ��ȡ�Ƿ���Ԥ��Ժ״̬
        /// <summary>
        /// ��ȡ�Ƿ���Ԥ��Ժ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="IsTrue">�Ƿ�Ԥ��Ժ	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsPreOutHospital(string p_strRegisterID, out bool IsTrue)
        {
            IsTrue = false;
            long lngRes = 0;
            string strSQL = " SELECT count(*) Count FROM t_opr_bih_register a ";
            strSQL += " WHERE a.registerid_chr='" + p_strRegisterID.Trim() + "' and a.STATUS_INT=1 and a.pstatus_int=2";//{0=δ�ϴ�;1=���ϴ�;2=Ԥ��Ժ;3=ʵ�ʳ�Ժ}			
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0 && Int32.Parse(dtResult.Rows[0]["Count"].ToString()) >= 1)
                {
                    IsTrue = true;
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
        #endregion

        //����������
        #region ����
        /// <summary>
        /// ���˽���
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="Type">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngBIHReckoning(string p_strRegisterID, int Type)
        {
            return 0;
        }
        #region �鿴���˼�¼
        [AutoComplete]
        public long m_lngGetSettleByPatientID(string p_strPatientID, out clsT_opr_bih_dayaccount_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_dayaccount_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_OPR_BIH_DAYACCOUNT WHERE PATIENTID_CHR = '" + p_strPatientID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_dayaccount_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_dayaccount_VO();
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intORDERNO_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDERNO_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strSQUARE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["SQUARE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_dblCHARGE_DEC = double.Parse(dtbResult.Rows[i1]["CHARGE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHG_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHG_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCHGOFMEPAY_DEC = double.Parse(dtbResult.Rows[i1]["CHGOFMEPAY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHGOFME_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHGOFME_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCHGOFHEPAY_DEC = double.Parse(dtbResult.Rows[i1]["CHGOFHEPAY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_dblCLEARCHGOFHE_DEC = double.Parse(dtbResult.Rows[i1]["CLEARCHGOFHE_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strCLEARMPOPTID_CHR = dtbResult.Rows[i1]["CLEARMPOPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHEARMP_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARMP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCLEARHPOPTID_CHR = dtbResult.Rows[i1]["CLEARHPOPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLEARHP_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CLEARHP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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

        #region �������˼�¼
        [AutoComplete]
        public long m_lngAddNewSettleAccount(clsT_opr_bih_dayaccount_VO p_objRecord, out string p_strRecordID)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "DAYACCOUNTID_CHR", "T_OPR_BIH_DAYACCOUNT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strGetNewORDERNO_INT = @"SELECT (MAX (TO_NUMBER (orderno_int))+1) AS ORDERNO_INT
                                           FROM t_opr_bih_dayaccount
                                           WHERE t_opr_bih_dayaccount.registerid_chr = '" + p_objRecord.m_strREGISTERID_CHR + "'";
            int intNewORDERNO_INT = 0;
            DataTable dtbNewID = new DataTable();
            lngRes = objHRPSvc.DoGetDataTable(strGetNewORDERNO_INT, ref dtbNewID);
            if (lngRes > 0 && dtbNewID.Rows.Count > 0)
            {
                if (dtbNewID.Rows[0]["ORDERNO_INT"] != System.DBNull.Value)
                {
                    intNewORDERNO_INT = Convert.ToInt32(dtbNewID.Rows[0]["ORDERNO_INT"].ToString());
                }
                else
                {
                    intNewORDERNO_INT = 1;
                }
            }
            else
            {
                return -1;
            }
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            p_objRecord.m_strSQUARE_DAT = strDateTime;
            p_objRecord.m_strDAYACCOUNTID_CHR = p_strRecordID;
            p_objRecord.m_intORDERNO_INT = intNewORDERNO_INT;
            string strSQL = "INSERT INTO T_OPR_BIH_DAYACCOUNT (DAYACCOUNTID_CHR,PATIENTID_CHR,REGISTERID_CHR,ORDERNO_INT,SQUARE_DAT,CHARGE_DEC,CLEARCHG_DEC,CHGOFMEPAY_DEC,CLEARCHGOFME_DEC,CHGOFHEPAY_DEC,CLEARCHGOFHE_DEC,CLEARMPOPTID_CHR,CHEARMP_DAT,CLEARHPOPTID_CHR,CLEARHP_DAT,AREAID_CHR) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(16, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strDAYACCOUNTID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_intORDERNO_INT;
                if (p_objRecord.m_strSQUARE_DAT != null)
                {
                    objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strSQUARE_DAT);
                }

                objLisAddItemRefArr[5].Value = p_objRecord.m_dblCHARGE_DEC;
                objLisAddItemRefArr[6].Value = p_objRecord.m_dblCLEARCHG_DEC;

                objLisAddItemRefArr[7].Value = p_objRecord.m_dblCHGOFMEPAY_DEC;

                objLisAddItemRefArr[8].Value = p_objRecord.m_dblCLEARCHGOFME_DEC;
                objLisAddItemRefArr[9].Value = p_objRecord.m_dblCHGOFHEPAY_DEC;
                objLisAddItemRefArr[10].Value = p_objRecord.m_dblCLEARCHGOFHE_DEC;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strCLEARMPOPTID_CHR;
                if (p_objRecord.m_strCHEARMP_DAT == null)
                {
                    objLisAddItemRefArr[12].Value = null;
                }
                else
                {
                    objLisAddItemRefArr[12].Value = DateTime.Parse(p_objRecord.m_strCHEARMP_DAT);
                }
                objLisAddItemRefArr[13].Value = p_objRecord.m_strCLEARHPOPTID_CHR;
                if (p_objRecord.m_strCLEARHP_DAT == null)
                {
                    objLisAddItemRefArr[14].Value = null;
                }
                else
                {
                    objLisAddItemRefArr[14].Value = DateTime.Parse(p_objRecord.m_strCLEARHP_DAT);
                }
                objLisAddItemRefArr[15].Value = p_objRecord.m_strAREAID_CHR;
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
        #region �޸����˼�¼
        [AutoComplete]
        public long m_lngModifySettleInfo(clsT_opr_bih_dayaccount_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_OPR_BIH_DAYACCOUNT (DAYACCOUNTID_CHR,PATIENTID_CHR,REGISTERID_CHR,ORDERNO_INT,SQUARE_DAT,CHARGE_DEC,CLEARCHG_DEC,CHGOFMEPAY_DEC,CLEARCHGOFME_DEC,CHGOFHEPAY_DEC,CLEARCHGOFHE_DEC,CLEARMPOPTID_CHR,CHEARMP_DAT,CLEARHPOPTID_CHR,CLEARHP_DAT) VALUES ('" + p_objRecord.m_strDAYACCOUNTID_CHR + "','" + p_objRecord.m_strPATIENTID_CHR + "','" + p_objRecord.m_strREGISTERID_CHR + "','" + p_objRecord.m_intORDERNO_INT.ToString() + "',TO_DATE('" + p_objRecord.m_strSQUARE_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_dblCHARGE_DEC.ToString() + "','" + p_objRecord.m_dblCLEARCHG_DEC.ToString() + "','" + p_objRecord.m_dblCHGOFMEPAY_DEC.ToString() + "','" + p_objRecord.m_dblCLEARCHGOFME_DEC.ToString() + "','" + p_objRecord.m_dblCHGOFHEPAY_DEC.ToString() + "','" + p_objRecord.m_dblCLEARCHGOFHE_DEC.ToString() + "','" + p_objRecord.m_strCLEARMPOPTID_CHR + "',TO_DATE('" + p_objRecord.m_strCHEARMP_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strCLEARHPOPTID_CHR + "',TO_DATE('" + p_objRecord.m_strCLEARHP_DAT + "','YYYY-MM-DD hh24:mi:ss'))";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ɾ�����˼�¼
        [AutoComplete]
        public long m_lngDeleteSettleAccountRecord(clsT_opr_bih_dayaccount_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "DELETE FROM T_OPR_BIH_DAYACCOUNT WHERE DAYACCOUNTID_CHR = '" + p_objRecord.m_strDAYACCOUNTID_CHR + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ��������ȡ���˷�����Ϣ
        [AutoComplete]
        public long m_lngGetPatientChargeItemInfo(string p_strRegID, out clsT_opr_bih_patientcharge_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_opr_bih_patientcharge_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_OPR_BIH_PATIENTCHARGE WHERE STATUS_INT = 1 and PSTATUS_INT = 1 AND REGISTERID_CHR = '" + p_strRegID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_opr_bih_patientcharge_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_opr_bih_patientcharge_VO();
                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ACTIVE_DAT"] == System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strACTIVE_DAT = "";
                        }
                        else
                        {
                            p_objResultArr[i1].m_strACTIVE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ACTIVE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ORDEREXECTYPE_INT"] == System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = -1;
                        }
                        else
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLACAREA_CHR = dtbResult.Rows[i1]["CLACAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATEAREA_CHR = dtbResult.Rows[i1]["CREATEAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCALCCATEID_CHR = dtbResult.Rows[i1]["CALCCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINVCATEID_CHR = dtbResult.Rows[i1]["INVCATEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMNAME_CHR = dtbResult.Rows[i1]["CHARGEITEMNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["UNITPRICE_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_DEC = Convert.ToDouble(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString());
                        }
                        if (dtbResult.Rows[i1]["AMOUNT_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_fltAMOUNT_DEC = Convert.ToSingle(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString());
                        }
                        if (dtbResult.Rows[i1]["DISCOUNT_DEC"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = Convert.ToSingle(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString());
                        }
                        if (dtbResult.Rows[i1]["ISMEPAY_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISMEPAY_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISMEPAY_INT"].ToString());
                        }
                        p_objResultArr[i1].m_strDES_VCHR = dtbResult.Rows[i1]["DES_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATETYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intCREATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["CREATETYPE_INT"].ToString());
                        }
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strCREATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strOPERATOR_CHR = dtbResult.Rows[i1]["OPERATOR_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["MODIFY_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strDEACTIVATOR_CHR = dtbResult.Rows[i1]["DEACTIVATOR_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["DEACTIVATE_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strDEACTIVATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["DEACTIVATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        if (dtbResult.Rows[i1]["STATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString());
                        }
                        if (dtbResult.Rows[i1]["PSTATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString());
                        }
                        if (dtbResult.Rows[i1]["CHEARACCOUNT_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strCHEARACCOUNT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHEARACCOUNT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strDAYACCOUNTID_CHR = dtbResult.Rows[i1]["DAYACCOUNTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAYMONEYID_CHR = dtbResult.Rows[i1]["PAYMONEYID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVATOR_CHR = dtbResult.Rows[i1]["ACTIVATOR_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ACTIVATETYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString());
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
        #region �޸Ĳ��˷��ü�¼
        [AutoComplete]
        public long m_lngModifyChargeItem(clsT_opr_bih_patientcharge_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "UPDATE T_OPR_BIH_PATIENTCHARGE SET OPERATOR_CHR = '" + p_objRecord.m_strOPERATOR_CHR + "',DAYACCOUNTID_CHR = '" + p_objRecord.m_strDAYACCOUNTID_CHR + "',MODIFY_DAT = TO_DATE('" + p_objRecord.m_strMODIFY_DAT + "','YYYY-MM-DD hh24:mi:ss'),PSTATUS_INT = " + p_objRecord.m_intPSTATUS_INT + ",ACTIVATETYPE_INT = " + p_objRecord.m_intACTIVATETYPE_INT + ",ACTIVATOR_CHR = '" + p_objRecord.m_strOPERATOR_CHR + "' WHERE PCHARGEID_CHR = '" + p_objRecord.m_strPCHARGEID_CHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region �������˼�¼
        [AutoComplete]
        public long m_lngAddNewSettleAccountInfo(int p_intActivateType, string p_strOperatorID, clsT_opr_bih_dayaccount_VO p_objRecord)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            clsT_opr_bih_patientcharge_VO[] p_objResultArr;
            lngRes = m_lngGetPatientChargeItemInfo(p_objRecord.m_strREGISTERID_CHR, out p_objResultArr);
            if (lngRes < 0 || p_objResultArr.Length < 1)
            {
                return -1;
            }
            double decTotalCharge = 0;
            double decSelfCharge = 0;
            //			double decCommonCharge = 0;
            //ҽ�������ݲ������ȫ�������ԷѼ���
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                decTotalCharge += p_objResultArr[i].m_dblUNITPRICE_DEC * p_objResultArr[i].m_fltAMOUNT_DEC;
            }
            decSelfCharge = decTotalCharge;
            p_objRecord.m_dblCHARGE_DEC = decTotalCharge;
            p_objRecord.m_dblCHGOFMEPAY_DEC = decSelfCharge;
            lngRes = m_lngAddNewSettleAccount(p_objRecord, out p_strRecordID);
            if (lngRes < 0)
            {
                return -1;
            }
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i].m_intACTIVATETYPE_INT = p_intActivateType;
                p_objResultArr[i].m_intPSTATUS_INT = 2;
                p_objResultArr[i].m_intSTATUS_INT = 1;
                p_objResultArr[i].m_strACTIVATOR_CHR = p_strOperatorID;
                p_objResultArr[i].m_strACTIVE_DAT = System.DateTime.Now.ToString();
                p_objResultArr[i].m_strDAYACCOUNTID_CHR = p_strRecordID;
                p_objResultArr[i].m_strMODIFY_DAT = System.DateTime.Now.ToString();
                p_objResultArr[i].m_strOPERATOR_CHR = p_strOperatorID;
                lngRes = m_lngModifyChargeItem(p_objResultArr[i]);
                if (lngRes < 0)
                {
                    return -1;
                }
            }
            return lngRes;
        }
        #endregion
        #region �Զ��ս���
        #region ��ѯ����סԺ����δ������Ϣ
        [AutoComplete]
        public long m_lngGetPatientChargeItemInfoAUTO(out clsPatientCharge[] objPatientArr)
        {
            objPatientArr = new clsPatientCharge[0];
            long lngRes = 0;
            string strSQL = @"SELECT   MAX (t_opr_bih_patientcharge.patientid_chr) AS patientid_chr,
                              t_opr_bih_patientcharge.registerid_chr,
                              t_opr_bih_patientcharge.clacarea_chr AS clacarea_chr,
                              SUM (  t_opr_bih_patientcharge.amount_dec
                              * t_opr_bih_patientcharge.unitprice_dec
                              ) AS sumprice
                              FROM t_opr_bih_patientcharge
                              WHERE status_int =1 and pstatus_int = 1
                              GROUP BY registerid_chr,t_opr_bih_patientcharge.clacarea_chr";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objPatientArr = new clsPatientCharge[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        objPatientArr[i] = new clsPatientCharge();
                        objPatientArr[i].strPatientID = dtbResult.Rows[i]["patientid_chr"].ToString();
                        objPatientArr[i].strRegisterID = dtbResult.Rows[i]["registerid_chr"].ToString();
                        objPatientArr[i].AreaID = dtbResult.Rows[i]["clacarea_chr"].ToString();
                        if (dtbResult.Rows[i]["sumprice"] != System.DBNull.Value)
                        {
                            objPatientArr[i].dblsumPrice = Convert.ToDouble(dtbResult.Rows[i]["sumprice"].ToString());
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
        #region �޸�����סԺ�����շ���Ϣ�Ӵ��ᵽ����
        [AutoComplete]
        public long m_lngModifyChargeItemAUTO(clsT_opr_bih_patientcharge_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "UPDATE T_OPR_BIH_PATIENTCHARGE SET OPERATOR_CHR = '" + p_objRecord.m_strOPERATOR_CHR + "',DAYACCOUNTID_CHR = '" + p_objRecord.m_strDAYACCOUNTID_CHR + "',MODIFY_DAT = TO_DATE('" + p_objRecord.m_strMODIFY_DAT + "','YYYY-MM-DD hh24:mi:ss'),PSTATUS_INT = " + p_objRecord.m_intPSTATUS_INT + ",ACTIVATETYPE_INT = " + p_objRecord.m_intACTIVATETYPE_INT + ",ACTIVATOR_CHR = '" + p_objRecord.m_strOPERATOR_CHR + "' WHERE PSTATUS_INT = '1' AND REGISTERID_CHR ='" + p_objRecord.m_strREGISTERID_CHR + "' AND CLACAREA_CHR = '" + p_objRecord.m_strCLACAREA_CHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ִ���Զ��ս���
        [AutoComplete]
        public long m_lngAddNewSettleAccountInfoAUTO()
        {
            long lngRes = 0;
            clsPatientCharge[] objPatientArr = null;
            lngRes = m_lngGetPatientChargeItemInfoAUTO(out objPatientArr);
            if (lngRes < 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            for (int i = 0; i < objPatientArr.Length; i++)
            {
                clsT_opr_bih_dayaccount_VO objAccount = new clsT_opr_bih_dayaccount_VO();
                objAccount.m_strPATIENTID_CHR = objPatientArr[i].strPatientID;
                objAccount.m_strREGISTERID_CHR = objPatientArr[i].strRegisterID;
                objAccount.m_strSQUARE_DAT = System.DateTime.Now.ToString();
                objAccount.m_dblCHARGE_DEC = objPatientArr[i].dblsumPrice;
                objAccount.m_dblCHGOFMEPAY_DEC = objPatientArr[i].dblsumPrice;
                objAccount.m_strAREAID_CHR = objPatientArr[i].AreaID;
                string strRecordID;
                lngRes = m_lngAddNewSettleAccount(objAccount, out strRecordID);
                if (lngRes < 0)
                {
                    throw new Exception("����ʧ�ܣ�");
                }
                clsT_opr_bih_patientcharge_VO objCharge = new clsT_opr_bih_patientcharge_VO();
                //�Զ����ʵĲ�������δȷ��
                objCharge.m_strOPERATOR_CHR = null;
                objCharge.m_strMODIFY_DAT = System.DateTime.Now.ToString();
                objCharge.m_strDAYACCOUNTID_CHR = strRecordID;
                objCharge.m_intSTATUS_INT = 1;
                objCharge.m_strCLACAREA_CHR = objPatientArr[i].AreaID;
                objCharge.m_intPSTATUS_INT = 2;
                objCharge.m_intACTIVATETYPE_INT = 1;
                objCharge.m_strREGISTERID_CHR = objPatientArr[i].strRegisterID;
                objCharge.m_strACTIVE_DAT = System.DateTime.Now.ToString();
                lngRes = m_lngModifyChargeItemAUTO(objCharge);
                if (lngRes < 0)
                {
                    throw new Exception("����ʧ�ܣ�");
                }
            }
            return lngRes;
        }
        #endregion
        
        #endregion
        #region �Զ������ռ����շ�
        #region �Ǽ��շ���Ϣ
        [AutoComplete]
        public long m_lngCreateDailyChargeAUTO(string p_strOperatorID)
        {
            long lngRes = 0;
            clsT_Opr_Bih_Register_VO[] p_objResultArr = null;
            lngRes = m_lngGetPatientInhosPitalInfo(out p_objResultArr);
            if (lngRes < 0)
            {
                return -1;
            }
            if (p_objResultArr.Length > 0)
            {
                for (int i = 0; i < p_objResultArr.Length; i++)
                {
                    clsT_Bse_Bed_VO p_objResult = null;
                    lngRes = m_lngGetPatientBedInfo(p_objResultArr[i].m_strAREAID_CHR, p_objResultArr[i].m_strBEDID_CHR, out p_objResult);
                    if (lngRes < 0)
                    {
                        return -1;
                    }
                    clsT_opr_bih_patientcharge_VO objPatientCharge = null;
                    lngRes = m_lngMadePatientDailyCharge(p_objResultArr[i].m_strAREAID_CHR, p_objResultArr[i].m_strPATIENTID_CHR, p_objResultArr[i].m_strREGISTERID_CHR, p_objResult.m_strCHARGEITEMID_CHR, p_strOperatorID, out objPatientCharge);
                    if (lngRes < 0)
                    {
                        return -1;
                    }
                    string PatientItemID = "";
                    lngRes = m_lngAddNewPatientCharge1(out PatientItemID, objPatientCharge);
                    if (lngRes < 0)
                    {
                        return -1;
                    }
                }
            }
            return lngRes;
        }
        #endregion
        #region ��ȡ���в��˵�סԺ��Ϣ
        [AutoComplete]
        public long m_lngGetPatientInhosPitalInfo(out clsT_Opr_Bih_Register_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Opr_Bih_Register_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_OPR_BIH_REGISTER WHERE STATUS_INT = '1' AND PSTATUS_INT <> 3";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Opr_Bih_Register_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Opr_Bih_Register_VO();
                        p_objResultArr[i1].m_strREGISTERID_CHR = dtbResult.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["MODIFY_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ISBOOKING_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intISBOOKING_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISBOOKING_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strINPATIENTID_CHR = dtbResult.Rows[i1]["INPATIENTID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["INPATIENT_DAT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_strINPATIENT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["INPATIENT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["AREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEDID_CHR = dtbResult.Rows[i1]["BEDID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["TYPE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["TYPE_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = dtbResult.Rows[i1]["DIAGNOSE_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["LIMITRATE_MNY"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_dblLIMITRATE_MNY = double.Parse(dtbResult.Rows[i1]["LIMITRATE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["INPATIENTCOUNT_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intINPATIENTCOUNT_INT = Convert.ToInt32(dtbResult.Rows[i1]["INPATIENTCOUNT_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["STATE_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSTATE_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATE_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["STATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strOPERATORID_CHR = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["PSTATUS_INT"] != System.DBNull.Value)
                        {
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
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
        #region ���ݲ���סԺ��Ϣ��ò��˵�סԺ������Ϣ��
        [AutoComplete]
        public long m_lngGetPatientBedInfo(string p_strAreaID, string p_strBedID, out clsT_Bse_Bed_VO p_objResult)
        {
            p_objResult = new clsT_Bse_Bed_VO();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_BSE_BED WHERE BEDID_CHR = '" + p_strBedID + "' and AREAID_CHR = '" + p_strAreaID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
                        p_objResult.m_dblRATE_MNY = double.Parse(dtbResult.Rows[0]["RATE_MNY"].ToString().Trim());
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
                        p_objResult.m_dblAIRRATE_MNY = double.Parse(dtbResult.Rows[0]["AIRRATE_MNY"].ToString().Trim());
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
        #region ���벡��סԺ�շ���Ŀ
        [AutoComplete]
        public long m_lngAddNewPatientCharge1(out string p_strRecordID, clsT_opr_bih_patientcharge_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(18, "PCHARGEID_CHR", "T_OPR_BIH_PATIENTCHARGE", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_BSE_EMPLOYEE (PCHARGEID_CHR,PATIENTID_CHR,REGISTERID_CHR,ACTIVE_DAT,ORDERID_CHR,ORDEREXECTYPE_INT,ORDEREXECID_CHR,CLACAREA_CHR,CREATEAREA_CHR,CALCCATEID_CHR,INVCATEID_CHR,CHARGEITEMID_CHR,CHARGEITEMNAME_CHR,UNIT_VCHR,UNITPRICE_DEC,AMOUNT_DEC,DISCOUNT_DEC,ISMEPAY_INT,DES_VCHR,CREATETYPE_INT,CREATOR_CHR,CREATE_DAT,OPERATOR_CHR,MODIFY_DAT,DEACTIVATOR_CHR,DEACTIVATE_DAT,STATUS_INT,PSTATUS_INT,CHEARACCOUNT_DAT,DAYACCOUNTID_CHR,PAYMONEYID_CHR,ACTIVATOR_CHR,ACTIVATETYPE_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(33, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                if (p_objRecord.m_strACTIVE_DAT != "")
                {
                    objLisAddItemRefArr[3].Value = DateTime.Parse(p_objRecord.m_strACTIVE_DAT);
                }
                objLisAddItemRefArr[4].Value = p_objRecord.m_strORDERID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intORDEREXECTYPE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strORDEREXECID_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strCLACAREA_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strCREATEAREA_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strCALCCATEID_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strINVCATEID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strCHARGEITEMID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strCHARGEITEMNAME_CHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strUNIT_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_dblUNITPRICE_DEC;
                objLisAddItemRefArr[15].Value = p_objRecord.m_fltAMOUNT_DEC;
                objLisAddItemRefArr[16].Value = p_objRecord.m_fltDISCOUNT_DEC;
                objLisAddItemRefArr[17].Value = p_objRecord.m_intISMEPAY_INT;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_intCREATETYPE_INT;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCREATOR_CHR;
                if (p_objRecord.m_strCREATE_DAT != "")
                {
                    objLisAddItemRefArr[21].Value = DateTime.Parse(p_objRecord.m_strCREATE_DAT);
                }
                objLisAddItemRefArr[22].Value = p_objRecord.m_strOPERATOR_CHR;
                if (p_objRecord.m_strMODIFY_DAT != "")
                {
                    objLisAddItemRefArr[23].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                }
                objLisAddItemRefArr[24].Value = p_objRecord.m_strDEACTIVATOR_CHR;
                if (p_objRecord.m_strDEACTIVATE_DAT != "")
                {
                    objLisAddItemRefArr[25].Value = DateTime.Parse(p_objRecord.m_strDEACTIVATE_DAT);
                }
                objLisAddItemRefArr[26].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[27].Value = p_objRecord.m_intPSTATUS_INT;
                if (p_objRecord.m_strCHEARACCOUNT_DAT != "")
                {
                    objLisAddItemRefArr[28].Value = DateTime.Parse(p_objRecord.m_strCHEARACCOUNT_DAT);
                }
                objLisAddItemRefArr[29].Value = p_objRecord.m_strDAYACCOUNTID_CHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strPAYMONEYID_CHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strACTIVATOR_CHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_intACTIVATETYPE_INT;
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
        #region ��ȡ�շ���Ŀ��Ϣ
        [AutoComplete]
        public long m_lngGetChargeItemInfoByID(string p_strChargeItemID, out clsT_bse_chargeitem_VO p_objResult)
        {
            p_objResult = new clsT_bse_chargeitem_VO();
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_BSE_CHARGEITEM WHERE ITEMID_CHR = '" + p_strChargeItemID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
        #region ����סԺ�������շ�����
        [AutoComplete]
        public long m_lngMadePatientDailyCharge(string p_strAreaID, string p_strPatientID, string p_strRegisterID, string p_strChargeItemID, string p_strOperatorID, out clsT_opr_bih_patientcharge_VO objPatientCharge)
        {
            long lngRes = 0;
            objPatientCharge = new clsT_opr_bih_patientcharge_VO();
            clsT_bse_chargeitem_VO p_objResult;
            lngRes = m_lngGetChargeItemInfoByID(p_strChargeItemID, out p_objResult);
            if (lngRes < 0)
            {
                return -1;
            }
            else
            {
                //סԺ����
                objPatientCharge.m_dblUNITPRICE_DEC = p_objResult.m_dblITEMPRICE_MNY;
                //����
                objPatientCharge.m_fltAMOUNT_DEC = 1;
                //�ۿ۱���
                objPatientCharge.m_fltDISCOUNT_DEC = 1;
                //��Ч���͡�
                objPatientCharge.m_intACTIVATETYPE_INT = 1;
                //¼������
                objPatientCharge.m_intCREATETYPE_INT = 2;
                //�Ƿ��Է���Ŀ
                objPatientCharge.m_intISMEPAY_INT = 1;
                //ҽ��ִ������
                //			objPatientCharge.m_intORDEREXECTYPE_INT=0;
                //����״̬
                objPatientCharge.m_intPSTATUS_INT = 1;
                //��С״̬
                objPatientCharge.m_intSTATUS_INT = 1;
                //��Ч��
                objPatientCharge.m_strACTIVATOR_CHR = p_strOperatorID;
                //��Чʱ��
                objPatientCharge.m_strACTIVE_DAT = System.DateTime.Now.ToString();
                //���ú������
                objPatientCharge.m_strCALCCATEID_CHR = p_objResult.m_strITEMOPCALCTYPE_CHR;
                //�շ���ĿID
                objPatientCharge.m_strCHARGEITEMID_CHR = p_objResult.m_strITEMID_CHR;
                //�շ���Ŀ����
                objPatientCharge.m_strCHARGEITEMNAME_CHR = p_objResult.m_strITEMNAME_VCHR;
                //��������
                //            objPatientCharge.m_strCHEARACCOUNT_DAT="";
                //���㲡��
                objPatientCharge.m_strCLACAREA_CHR = p_strAreaID;
                //¼������
                objPatientCharge.m_strCREATE_DAT = System.DateTime.Now.ToString();
                //�����ص�
                objPatientCharge.m_strCREATEAREA_CHR = p_strAreaID;
                //¼����
                objPatientCharge.m_strCREATOR_CHR = p_strOperatorID;
                //����ID
                //			objPatientCharge.m_strDAYACCOUNTID_CHR ="";
                //ɾ��ʱ��
                //            objPatientCharge.m_strDEACTIVATE_DAT = "";
                //ɾ����
                //			objPatientCharge.m_strDEACTIVATOR_CHR = "";
                //��ע
                objPatientCharge.m_strDES_VCHR = "�Զ�¼��";
                //���÷�Ʊ���
                objPatientCharge.m_strINVCATEID_CHR = p_objResult.m_strITEMIPINVTYPE_CHR;
                //�޸�ʱ��
                objPatientCharge.m_strMODIFY_DAT = System.DateTime.Now.ToString();
                //�޸���
                objPatientCharge.m_strOPERATOR_CHR = p_strOperatorID;
                //ҽ��ִ�е�ID
                //            objPatientCharge.m_strORDEREXECID_CHR ="";
                //ҽ����ID
                //			objPatientCharge.m_strORDERID_CHR = "";
                //����ID
                objPatientCharge.m_strPATIENTID_CHR = p_strPatientID;
                //����סԺ�Ǽ�iD
                objPatientCharge.m_strREGISTERID_CHR = p_strRegisterID;
                //�ɷѼ�¼ID
                //			objPatientCharge.m_strPAYMONEYID_CHR = "";
                //			objPatientCharge.m_strPCHARGEID_CHR
                objPatientCharge.m_strUNIT_VCHR = p_objResult.m_strITEMIPUNIT_CHR;
            }
            return lngRes;
        }
        #endregion
        #endregion
        #endregion
        #region ȷ���շ�
        /// <summary>
        /// ȷ���շ�	
        /// ����˵����
        ///		1���������Ѽ�¼��
        ///		2��������ϸ��־Ϊ���壻
        ///		3��������Ʊ��¼
        ///		4��������Ʊ��������¼{1.���������ط�����ϸ��2.���շ�Ʊ�����ܷ��ò���䷢Ʊ�������3.Ϊÿһ����Ʊ�������һ����Ʊ��������¼��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPChargeIDArr">������ϸ��ˮ�� (����)</param>
        /// <param name="p_objPayMoney"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngClearAccountByConfirmCharge(string[] p_strPChargeIDArr, clsT_opr_bih_paymoney_VO p_objPayMoney)
        {
            long lngRes = 0;
            lngRes = 1;
            string strTem = "";
            int i = 0;

            //1���������Ѽ�¼��
            #region �������Ѽ�¼
            if (lngRes > 0)
            {
                lngRes = m_lngAddNewPayMoney(out strTem, p_objPayMoney);
                p_objPayMoney.m_strPAYMONEYID_CHR = strTem;
            }
            #endregion

            //2��������ϸ��־Ϊ���壻
            #region ������ϸ��־Ϊ����
            for (i = 0; i < p_strPChargeIDArr.Length; i++)
            {
                if (p_strPChargeIDArr[i] != string.Empty)
                {
                    if (lngRes > 0)
                    {
                        lngRes = m_lngModifyConfirmCharge(p_strPChargeIDArr[i], p_objPayMoney.m_strPAYMONEYID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                    }
                }
            }
            #endregion

            //3��������Ʊ��¼
            #region	������Ʊ��¼
            string strInvoiceID = "";
            clsT_Opr_Bih_Invoice_Vo objInvoice = new clsT_Opr_Bih_Invoice_Vo();
            objInvoice.m_strINVOICENO_CHR = p_objPayMoney.m_strCLEARINV_VCHR;
            objInvoice.m_intPSTATUS_INT = 1;
            objInvoice.m_dblMONEY_DEC = p_objPayMoney.m_dblYSMONEY_DEC;
            objInvoice.m_strCREATEDATE_DAT = p_objPayMoney.m_strCREATE_DAT;
            objInvoice.m_strCREATOR_CHR = p_objPayMoney.m_strCREATOR_CHR;
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngAddNewInvoice(out strInvoiceID, objInvoice);
                if (lngRes > 0) objInvoice.m_strINVOICEID_CHR = strInvoiceID;
            }
            #endregion

            //4��������Ʊ��������¼
            #region	������Ʊ��¼
            //1.���������ط�����ϸ��
            clsT_opr_bih_patientcharge_VO[] p_objPatientChargeArr = new clsT_opr_bih_patientcharge_VO[0];
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngGetPatientChargeByPChargeIDArr(p_strPChargeIDArr, out p_objPatientChargeArr);
            }

            //2.���շ�Ʊ�����ܷ��ò���䷢Ʊ������󣬲�Ϊÿһ����Ʊ�������һ����Ʊ��������¼��
            string strInvoicecat = "";
            if (lngRes > 0 && p_objPatientChargeArr != null && p_objPatientChargeArr.Length > 0)
            {
                System.Collections.ArrayList alItem = new System.Collections.ArrayList();
                for (int i1 = 0; i1 < p_objPatientChargeArr.Length; i1++)
                {
                    if (!alItem.Contains(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim()))
                    {
                        clsT_Opr_Bih_Invoicecat_Vo objInvoicecat = new clsT_Opr_Bih_Invoicecat_Vo();
                        objInvoicecat.m_strINVOICEID_CHR = objInvoice.m_strINVOICEID_CHR;
                        objInvoicecat.m_strCREATEDATE_DAT = objInvoice.m_strCREATEDATE_DAT;
                        objInvoicecat.m_strCATEGORYID_CHR = p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim();
                        double dblMoney = 0;
                        for (int i2 = 0; i2 < p_objPatientChargeArr.Length; i2++)
                        {
                            if (p_objPatientChargeArr[i2].m_strINVCATEID_CHR.Trim() == p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim())
                            {
                                dblMoney += p_objPatientChargeArr[i2].m_dblUNITPRICE_DEC * p_objPatientChargeArr[i2].m_fltAMOUNT_DEC;
                            }
                        }
                        objInvoicecat.m_dblMONEY_DEC = double.Parse(dblMoney.ToString("0.00"));
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngAddNewInvoicecat(out strInvoicecat, objInvoicecat);
                        }
                        alItem.Add(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim());
                    }
                }
            }
            #endregion

            if (lngRes <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ������װ	
        /// ����˵����
        ///		1���޸����˱�
        ///		2��Ԥ�����״̬��־Ϊ���壻
        ///		3���������Ѽ�¼��
        ///		4�����ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)��
        ///		5����Ӧ������ˮ�ŵķ�����ϸ��־Ϊ���壻
        ///		6��������Ʊ��¼
        ///		7��������Ʊ��������¼	{1.���������ط�����ϸ��2.���շ�Ʊ�����ܷ��ò���䷢Ʊ�������3.Ϊÿһ����Ʊ�������һ����Ʊ��������¼��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDayAccountIDArr">������ˮ�� ����</param>
        /// <param name="p_strPrepayIDArr">Ԥ������ˮ�� ����</param>
        /// <param name="p_objPayMoney">���˽��Ѽ�¼VO </param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngClearAccountByIssue(string[] p_strDayAccountIDArr, string[] p_strPrepayIDArr, clsT_opr_bih_paymoney_VO p_objPayMoney)
        {
            long lngRes = 0;
            lngRes = 1;
            string strTem = "";
            int i = 0;

            //1���޸����˱�
            #region �޸����˱�
            for (i = 0; i < p_strDayAccountIDArr.Length; i++)
            {
                if (p_strDayAccountIDArr[i] != string.Empty)
                {
                    if (lngRes > 0)
                    {
                        strTem = GetUpdateDayAccountSQLClear(p_strDayAccountIDArr[i], p_objPayMoney.m_strAREAID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                        lngRes = m_lngModifyDayAccount(strTem);
                    }
                }
            }
            #endregion

            //2��Ԥ�����״̬��־Ϊ���壻
            #region Ԥ�����״̬��־Ϊ����
            for (i = 0; i < p_strPrepayIDArr.Length; i++)
            {
                if (p_strPrepayIDArr[i] != string.Empty)
                {
                    if (lngRes > 0)
                    {
                        lngRes = m_lngCleanupPrePay(p_strPrepayIDArr[i], p_objPayMoney.m_strCREATOR_CHR);
                    }
                }
            }
            #endregion

            //3���������Ѽ�¼
            #region �������Ѽ�¼
            if (lngRes > 0)
            {
                lngRes = m_lngAddNewPayMoney(out strTem, p_objPayMoney);
                p_objPayMoney.m_strPAYMONEYID_CHR = strTem;
            }
            #endregion

            //3.5��������ת����һ��Ԥ����������һ��Ԥ�����¼	{��ʱû�п���}
            if (lngRes > 0 && p_objPayMoney.m_intBALANCETOTYPE_INT == 2)
            {
                #region ����һ��Ԥ�����¼
                //clsT_opr_bih_prepay_VO objItem =new clsT_opr_bih_prepay_VO();
                //objItem.m_strPATIENTID_CHR =p_objPayMoney.m_strPATIENTID_CHR;
                //objItem.m_strREGISTERID_CHR =p_objPayMoney.m_strREGISTERID_CHR;
                //objItem.m_intLINER_INT =1;//���	{1=�հ�;2=ҹ��}
                //objItem.m_intPAYTYPE_INT =2;//��������{1=һ��;2=���ڽ�ת;3=�˷�(�����)}
                //objItem.m_intCUYCATE_INT =1;//����{1=�ֽ�;2=֧Ʊ;3=���ÿ�;4=���}
                //objItem.m_dblMONEY_DEC =-p_objPayMoney.m_dblBJMONEY_DEC;
                //objItem.m_strPREPAYINV_VCHR ="";//Ԥ������ ��Ʊ��
                //objItem.m_strAREAID_CHR =p_objPayMoney.m_strAREAID_CHR;
                //objItem.m_strDES_VCHR ="";
                //objItem.m_strCREATORID_CHR =p_objPayMoney.m_strCREATOR_CHR;
                //objItem.m_strCREATE_DAT =p_objPayMoney.m_strCREATE_DAT;
                //objItem.m_intSTATUS_INT =1;
                //objItem.m_intISCLEAR_INT =0;
                #endregion
            }

            //4�����ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)
            #region �������ʼ�¼
            clsT_opr_bih_payandprepaymap_Vo objpayandprepaymap = new clsT_opr_bih_payandprepaymap_Vo();
            for (i = 0; i < p_strPrepayIDArr.Length; i++)
            {
                if (p_strPrepayIDArr[i] != string.Empty)
                {
                    objpayandprepaymap.m_strMAPID_CHR = "";
                    objpayandprepaymap.m_strPAYMONEYID_CHR = p_objPayMoney.m_strPAYMONEYID_CHR;
                    objpayandprepaymap.m_strPREPAYID_CHR = p_strPrepayIDArr[i].Trim();

                    if (lngRes > 0)
                    {
                        lngRes = m_lngAddNewPayAndPrepayMap(out strTem, objpayandprepaymap);
                    }
                }
            }
            #endregion

            //5����Ӧ������ˮ�ŵķ�����ϸ��־Ϊ����
            #region ��Ӧ������ˮ�ŵķ�����ϸ��־Ϊ����
            for (i = 0; i < p_strDayAccountIDArr.Length; i++)
            {
                if (p_strDayAccountIDArr[i] != string.Empty)
                {
                    if (lngRes > 0)
                    {
                        lngRes = ClearupPatientCharge(p_strDayAccountIDArr[i], p_objPayMoney.m_strPAYMONEYID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                    }
                }
            }
            #endregion

            //6��������Ʊ��¼
            #region	������Ʊ��¼
            string strInvoiceID = "";
            clsT_Opr_Bih_Invoice_Vo objInvoice = new clsT_Opr_Bih_Invoice_Vo();
            objInvoice.m_strINVOICENO_CHR = p_objPayMoney.m_strCLEARINV_VCHR;
            objInvoice.m_intPSTATUS_INT = 1;
            objInvoice.m_dblMONEY_DEC = p_objPayMoney.m_dblYSMONEY_DEC;
            objInvoice.m_strCREATEDATE_DAT = p_objPayMoney.m_strCREATE_DAT;
            objInvoice.m_strCREATOR_CHR = p_objPayMoney.m_strCREATOR_CHR;

            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngAddNewInvoice(out strInvoiceID, objInvoice);
                if (lngRes > 0) objInvoice.m_strINVOICEID_CHR = strInvoiceID;
            }
            #endregion

            //7��������Ʊ��������¼
            #region	������Ʊ��¼
            //1.���������ط�����ϸ��
            clsT_opr_bih_patientcharge_VO[] p_objPatientChargeArr = new clsT_opr_bih_patientcharge_VO[0];
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngGetPatientChargeByDayAccountIDArr(p_strDayAccountIDArr, out p_objPatientChargeArr);
            }

            //2.���շ�Ʊ�����ܷ��ò���䷢Ʊ������󣬲�Ϊÿһ����Ʊ�������һ����Ʊ��������¼��
            string strInvoicecat = "";
            if (lngRes > 0 && p_objPatientChargeArr != null && p_objPatientChargeArr.Length > 0)
            {
                System.Collections.ArrayList alItem = new System.Collections.ArrayList();
                for (int i1 = 0; i1 < p_objPatientChargeArr.Length; i1++)
                {
                    if (!alItem.Contains(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim()))
                    {
                        clsT_Opr_Bih_Invoicecat_Vo objInvoicecat = new clsT_Opr_Bih_Invoicecat_Vo();
                        objInvoicecat.m_strINVOICEID_CHR = objInvoice.m_strINVOICEID_CHR;
                        objInvoicecat.m_strCREATEDATE_DAT = objInvoice.m_strCREATEDATE_DAT;
                        objInvoicecat.m_strCATEGORYID_CHR = p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim();
                        double dblMoney = 0;
                        for (int i2 = 0; i2 < p_objPatientChargeArr.Length; i2++)
                        {
                            if (p_objPatientChargeArr[i2].m_strINVCATEID_CHR.Trim() == p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim())
                            {
                                dblMoney += p_objPatientChargeArr[i2].m_dblUNITPRICE_DEC * p_objPatientChargeArr[i2].m_fltAMOUNT_DEC;
                            }
                        }
                        objInvoicecat.m_dblMONEY_DEC = double.Parse(dblMoney.ToString("0.00"));
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngAddNewInvoicecat(out strInvoicecat, objInvoicecat);
                        }
                        alItem.Add(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim());
                    }
                }
            }
            #endregion

            if (lngRes <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngRes;
        }
        /// <summary>
        /// ����ϸ��װ	
        ///	����˵����
        ///		1��Ԥ�����״̬��־Ϊ���壻
        ///		2���������Ѽ�¼��
        ///		3���޸ķ�����ϸ��3.0�޸ķ�����ϸ��3.1������˺š�������Ϣ��3.2��ѯ��Ӧ������Ϣ��3.3IF���ɺ�ȫ�������Then{3.3.1�޸�������Ϣ[�Ѿ���־]��},else{3.3.3�޸�������Ϣ[δ��]��}}����
        ///		4�����ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)
        ///		5��������Ʊ��¼
        ///		6��������Ʊ��������¼{1.���������ط�����ϸ��2.���շ�Ʊ�����ܷ��ò���䷢Ʊ�������3.Ϊÿһ����Ʊ�������һ����Ʊ��������¼��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPChargeIdArr">������ϸ��ˮ��[����]</param>
        /// <param name="p_strPrepayIDArr">Ԥ������ˮ�� ����</param>
        /// <param name="p_objPayMoney">���˽��Ѽ�¼VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngClearAccountByList(string[] p_strPChargeIdArr, string[] p_strPrepayIDArr, clsT_opr_bih_paymoney_VO p_objPayMoney)
        {
            long lngRes = 0;
            lngRes = 1;
            string strTem = "";
            int i = 0;
            int j = 0;

            //1��Ԥ�����״̬��־Ϊ���壻
            #region Ԥ�����״̬��־Ϊ����
            for (i = 0; i < p_strPrepayIDArr.Length; i++)
            {
                if (p_strPrepayIDArr[i] != string.Empty)
                {
                    if (lngRes > 0)
                    {
                        lngRes = m_lngCleanupPrePay(p_strPrepayIDArr[i], p_objPayMoney.m_strCREATOR_CHR);
                    }
                }
            }
            #endregion

            //2���������Ѽ�¼
            #region �������Ѽ�¼
            if (lngRes > 0)
            {
                lngRes = m_lngAddNewPayMoney(out strTem, p_objPayMoney);
                p_objPayMoney.m_strPAYMONEYID_CHR = strTem;
            }
            #endregion

            //2.5��������ת����һ��Ԥ����������һ��Ԥ�����¼	{��ʱû�п���}
            if (lngRes > 0 && p_objPayMoney.m_intBALANCETOTYPE_INT == 2)
            {
                #region ����һ��Ԥ�����¼
                //clsT_opr_bih_prepay_VO objItem =new clsT_opr_bih_prepay_VO();
                //objItem.m_strPATIENTID_CHR =p_objPayMoney.m_strPATIENTID_CHR;
                //objItem.m_strREGISTERID_CHR =p_objPayMoney.m_strREGISTERID_CHR;
                //objItem.m_intLINER_INT =1;//���	{1=�հ�;2=ҹ��}
                //objItem.m_intPAYTYPE_INT =2;//��������{1=һ��;2=���ڽ�ת;3=�˷�(�����)}
                //objItem.m_intCUYCATE_INT =1;//����{1=�ֽ�;2=֧Ʊ;3=���ÿ�;4=���}
                //objItem.m_dblMONEY_DEC =-p_objPayMoney.m_dblBJMONEY_DEC;
                //objItem.m_strPREPAYINV_VCHR ="";//Ԥ������ ��Ʊ��
                //objItem.m_strAREAID_CHR =p_objPayMoney.m_strAREAID_CHR;
                //objItem.m_strDES_VCHR ="";
                //objItem.m_strCREATORID_CHR =p_objPayMoney.m_strCREATOR_CHR;
                //objItem.m_strCREATE_DAT =p_objPayMoney.m_strCREATE_DAT;
                //objItem.m_intSTATUS_INT =1;
                //objItem.m_intISCLEAR_INT =0;
                #endregion
            }

            //3��3���޸ķ�����ϸ��3.0�޸ķ�����ϸ��3.1������˺š�������Ϣ��3.2��ѯ��Ӧ������Ϣ��3.3IF���ɺ�ȫ�������Then{3.3.1�޸�������Ϣ[�Ѿ���־]��},else{3.3.3�޸�������Ϣ[δ��]��}}����
            #region �޸ķ�����ϸ
            DataTable dtPCharge = new DataTable();
            clsT_opr_bih_dayaccount_VO p_objDayAccount = new clsT_opr_bih_dayaccount_VO();
            clsT_opr_bih_payandprepaymap_Vo objpayandprepaymap = new clsT_opr_bih_payandprepaymap_Vo();
            double dblTem = 0;
            for (i = 0; i < p_strPChargeIdArr.Length; i++)
            {
                if (p_strPChargeIdArr[i] != string.Empty)
                {
                    //3.0�޸ķ�����ϸ��
                    #region 3.0�޸ķ�����ϸ��
                    if (lngRes > 0)
                    {
                        lngRes = ClearupPatientChargeByID(p_strPChargeIdArr[i], p_objPayMoney.m_strPAYMONEYID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                    }
                    #endregion
                    //3.1������˺š�������Ϣ��
                    #region 3.1������˺š�������Ϣ��
                    if (lngRes > 0)
                    {
                        lngRes = m_lngGetPatientChargeByID(p_strPChargeIdArr[i], out dtPCharge);
                    }
                    #endregion
                    if (dtPCharge == null || dtPCharge.Rows.Count <= 0) lngRes = -1;
                    //3.2��ѯ��Ӧ������Ϣ��
                    #region 3.2��ѯ��Ӧ������Ϣ��
                    if (lngRes > 0)
                    {
                        lngRes = m_lngGetDayAccountByID(dtPCharge.Rows[0]["DAYACCOUNTID_CHR"].ToString(), out p_objDayAccount);
                    }
                    #endregion
                    //3.3IF���ɺ�ȫ�������Then{3.3.1�޸�������Ϣ[�Ѿ���־]��3.3.2���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)},else{3.3.3�޸�������Ϣ[δ��]��3.3.4���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}}
                    dblTem = double.Parse(dtPCharge.Rows[0]["UNITPRICE_DEC"].ToString()) * double.Parse(dtPCharge.Rows[0]["AMOUNT_DEC"].ToString());
                    dblTem += p_objDayAccount.m_dblCLEARCHG_DEC;
                    if (dblTem >= p_objDayAccount.m_dblCHARGE_DEC)
                    {
                        //3.3.1�޸�������Ϣ[�Ѿ���־]��
                        #region 3.3.1�޸�������Ϣ[�Ѿ���־]��
                        if (lngRes > 0)
                        {
                            strTem = GetUpdateDayAccountSQLClear(dtPCharge.Rows[0]["DAYACCOUNTID_CHR"].ToString(), p_objPayMoney.m_strAREAID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                            lngRes = m_lngModifyDayAccount(strTem);
                        }
                        #endregion
                        //3.3.2���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}
                        #region 3.3.2���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}
                        //						for(j=0;j<p_strPrepayIDArr.Length;j++)
                        //						{
                        //							if(p_strPrepayIDArr[j]!=string.Empty)
                        //							{
                        //								objpayandprepaymap.m_strMAPID_CHR="";
                        //								objpayandprepaymap.m_strPAYMONEYID_CHR =p_objPayMoney.m_strPAYMONEYID_CHR;
                        //								objpayandprepaymap.m_strPREPAYID_CHR =p_strPrepayIDArr[j].Trim();
                        //
                        //								if(lngRes>0)
                        //								{
                        //									lngRes =m_lngAddNewPayAndPrepayMap(out strTem,objpayandprepaymap);
                        //								}
                        //							}
                        //						}
                        #endregion
                    }
                    else
                    {
                        //3.3.3�޸�������Ϣ[δ��]��
                        #region 3.3.3�޸�������Ϣ[δ��]��
                        if (lngRes > 0)
                        {
                            strTem = GetUpdateDayAccountSQLUnClear(dtPCharge.Rows[0]["DAYACCOUNTID_CHR"].ToString(), dblTem, p_objPayMoney.m_strAREAID_CHR, p_objPayMoney.m_strCREATOR_CHR);
                            lngRes = m_lngModifyDayAccount(strTem);
                        }
                        #endregion
                        //3.3.4���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}   ??????????????????(δ�����Ƿ�Ҫ���˼�¼)???????????????????
                        #region 3.3.4���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}
                        //						for(j=0;j<p_strPrepayIDArr.Length;j++)
                        //						{
                        //							if(p_strPrepayIDArr[j]!=string.Empty)
                        //							{
                        //								objpayandprepaymap.m_strMAPID_CHR="";
                        //								objpayandprepaymap.m_strPAYMONEYID_CHR =p_objPayMoney.m_strPAYMONEYID_CHR;
                        //								objpayandprepaymap.m_strPREPAYID_CHR =p_strPrepayIDArr[j].Trim();
                        //
                        //								if(lngRes>0)
                        //								{
                        //									lngRes =m_lngAddNewPayAndPrepayMap(out strTem,objpayandprepaymap);
                        //								}
                        //							}
                        //						}
                        #endregion
                    }
                }
            }
            #endregion

            //4�����ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}
            #region ���ӳ��ʼ�¼(���˽ɷѳ岡��Ԥ����)}
            for (j = 0; j < p_strPrepayIDArr.Length; j++)
            {
                if (p_strPrepayIDArr[j] != string.Empty)
                {
                    objpayandprepaymap.m_strMAPID_CHR = "";
                    objpayandprepaymap.m_strPAYMONEYID_CHR = p_objPayMoney.m_strPAYMONEYID_CHR;
                    objpayandprepaymap.m_strPREPAYID_CHR = p_strPrepayIDArr[j].Trim();

                    if (lngRes > 0)
                    {
                        lngRes = m_lngAddNewPayAndPrepayMap(out strTem, objpayandprepaymap);
                    }
                }
            }
            #endregion

            //5��������Ʊ��¼
            #region	������Ʊ��¼
            string strInvoiceID = "";
            clsT_Opr_Bih_Invoice_Vo objInvoice = new clsT_Opr_Bih_Invoice_Vo();
            objInvoice.m_strINVOICENO_CHR = p_objPayMoney.m_strCLEARINV_VCHR;
            objInvoice.m_intPSTATUS_INT = 1;
            objInvoice.m_dblMONEY_DEC = p_objPayMoney.m_dblYSMONEY_DEC;
            objInvoice.m_strCREATEDATE_DAT = p_objPayMoney.m_strCREATE_DAT;
            objInvoice.m_strCREATOR_CHR = p_objPayMoney.m_strCREATOR_CHR;
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngAddNewInvoice(out strInvoiceID, objInvoice);
                if (lngRes > 0) objInvoice.m_strINVOICEID_CHR = strInvoiceID;
            }
            #endregion

            //6��������Ʊ��������¼
            #region	������Ʊ��¼
            //1.���������ط�����ϸ��
            clsT_opr_bih_patientcharge_VO[] p_objPatientChargeArr = new clsT_opr_bih_patientcharge_VO[0];
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngGetPatientChargeByPChargeIDArr(p_strPChargeIdArr, out p_objPatientChargeArr);
            }

            //2.���շ�Ʊ�����ܷ��ò���䷢Ʊ������󣬲�Ϊÿһ����Ʊ�������һ����Ʊ��������¼��
            string strInvoicecat = "";
            if (lngRes > 0 && p_objPatientChargeArr != null && p_objPatientChargeArr.Length > 0)
            {
                System.Collections.ArrayList alItem = new System.Collections.ArrayList();
                for (int i1 = 0; i1 < p_objPatientChargeArr.Length; i1++)
                {
                    if (!alItem.Contains(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim()))
                    {
                        clsT_Opr_Bih_Invoicecat_Vo objInvoicecat = new clsT_Opr_Bih_Invoicecat_Vo();
                        objInvoicecat.m_strINVOICEID_CHR = objInvoice.m_strINVOICEID_CHR;
                        objInvoicecat.m_strCREATEDATE_DAT = objInvoice.m_strCREATEDATE_DAT;
                        objInvoicecat.m_strCATEGORYID_CHR = p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim();
                        double dblMoney = 0;
                        for (int i2 = 0; i2 < p_objPatientChargeArr.Length; i2++)
                        {
                            if (p_objPatientChargeArr[i2].m_strINVCATEID_CHR.Trim() == p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim())
                            {
                                dblMoney += p_objPatientChargeArr[i2].m_dblUNITPRICE_DEC * p_objPatientChargeArr[i2].m_fltAMOUNT_DEC;
                            }
                        }
                        objInvoicecat.m_dblMONEY_DEC = double.Parse(dblMoney.ToString("0.00"));
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngAddNewInvoicecat(out strInvoicecat, objInvoicecat);
                        }
                        alItem.Add(p_objPatientChargeArr[i1].m_strINVCATEID_CHR.Trim());
                    }
                }
            }
            #endregion


            if (lngRes <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngRes;
        }
        /// <summary>
        /// �޸����˵�SQL���--[����]
        /// </summary>
        /// <param name="p_strDayAccountID">������ˮ��</param>
        /// <param name="p_strAreaID">���㲡��id{=����.id}</param>
        /// <param name="p_strOperateID">������</param>
        /// <returns></returns>
        [AutoComplete]
        private string GetUpdateDayAccountSQLClear(string p_strDayAccountID, string p_strAreaID, string p_strOperateID)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " ";
            strSQL += " update t_opr_bih_dayaccount ";
            strSQL += " set ";
            strSQL += "     CLEARCHG_DEC=CHARGE_DEC ";//--�������
            strSQL += "     ,CLEARCHGOFME_DEC=CHGOFMEPAY_DEC";//--�Է� 
            strSQL += "     ,CLEARCHGOFHE_DEC=CHGOFHEPAY_DEC";//--����
            strSQL += "     ,CLEARMPOPTID_CHR='" + p_strOperateID.Trim() + "'";//--�Է�������
            strSQL += "     ,CHEARMP_DAT=TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//--�Է���������	����ȫ��ʱ������
            strSQL += "     ,CLEARHPOPTID_CHR='" + p_strOperateID.Trim() + "'";//--����������	{=��Ա.id}
            strSQL += "     ,CLEARHP_DAT=TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";//--������������	����ȫ��ʱ������
            strSQL += "     ,AREAID_CHR='" + p_strAreaID.Trim() + "'";//--���㲡��id{=����.id}
            strSQL += " where dayaccountid_chr='" + p_strDayAccountID.Trim() + "'";
            return strSQL;
        }
        /// <summary>
        /// �޸����˵�SQL���--[δ��]
        /// </summary>
        /// <param name="p_strDayAccountID">������ˮ��</param>
        /// <param name="p_dblClearChg">�������[�ܶ�]</param>
        /// <param name="p_strAreaID">���㲡��id{=����.id}</param>
        /// <param name="p_strOperateID">������</param>
        /// <returns></returns>
        [AutoComplete]
        private string GetUpdateDayAccountSQLUnClear(string p_strDayAccountID, double p_dblClearChg, string p_strAreaID, string p_strOperateID)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = " ";
            strSQL += " update t_opr_bih_dayaccount ";
            strSQL += " set ";
            strSQL += "     CLEARCHG_DEC=" + p_dblClearChg.ToString();//--�������
                                                                      //strSQL +="     ,CLEARCHGOFME_DEC=CHGOFMEPAY_DEC";//--�Է� 
                                                                      //strSQL +="     ,CLEARCHGOFHE_DEC=CHGOFHEPAY_DEC";//--����
                                                                      //strSQL +="     ,CLEARMPOPTID_CHR='" + p_strOperateID.Trim() + "'";//--�Է�������
                                                                      //strSQL +="     ,CHEARMP_DAT=TO_DATE('"+ strDateTime +"','YYYY-MM-DD hh24:mi:ss')";//--�Է���������	����ȫ��ʱ������
                                                                      //strSQL +="     ,CLEARHPOPTID_CHR='" + p_strOperateID.Trim() + "'";//--����������	{=��Ա.id}
                                                                      //strSQL +="     ,CLEARHP_DAT=TO_DATE('"+ strDateTime +"','YYYY-MM-DD hh24:mi:ss')";//--������������	����ȫ��ʱ������
            strSQL += "     ,AREAID_CHR='" + p_strAreaID.Trim() + "'";//--���㲡��id{=����.id}
            strSQL += " where dayaccountid_chr='" + p_strDayAccountID.Trim() + "'";
            return strSQL;
        }
        #endregion
        #region ֱ���շ�
        /// <summary>
        /// ֱ���շ�	{1���������Ѽ�¼��2�����ӷ�����ϸ[����״̬]��}
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordArr">������ϸVo (����)</param>
        /// <param name="p_objPayMoney">����VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngClearAccountByDirectCharge(clsT_opr_bih_patientcharge_VO[] p_objRecordArr, clsT_opr_bih_paymoney_VO p_objPayMoney)
        {
            long lngRes = 0;
            lngRes = 1;
            string strTem = "";
            int i = 0;

            //1���������Ѽ�¼��
            #region �������Ѽ�¼
            if (lngRes > 0)
            {
                lngRes = m_lngAddNewPayMoney(out strTem, p_objPayMoney);
                p_objPayMoney.m_strPAYMONEYID_CHR = strTem;
            }
            #endregion

            //2�����ӷ�����ϸ[����״̬]��
            #region ���ӷ�����ϸ[����״̬]
            for (i = 0; i < p_objRecordArr.Length; i++)
            {
                if (lngRes > 0)
                {
                    p_objRecordArr[i].m_strPAYMONEYID_CHR = p_objPayMoney.m_strPAYMONEYID_CHR;
                    lngRes = m_lngAddNewPatientCharge(out strTem, p_objRecordArr[i]);
                    p_objRecordArr[i].m_strPCHARGEID_CHR = strTem;
                }
            }
            #endregion

            if (lngRes <= 0)
            {
                throw new Exception("����ʧ�ܣ�");
            }
            return lngRes;
        }
        #endregion
        #region �˷�
        /// <summary>
        /// �˷�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objInvoice">��Ʊ��¼����</param>
        /// <param name="p_intState">��Ʊ״̬	״̬{1=����;2=����;3=��Ʊ}</param>
        /// <param name="p_strOperatorID">������ID</param>
        /// <param name="p_strOperateDateTime">����ʱ��	[Ϊ�գ���ȡϵͳ��ǰʱ��]</param>
        /// <returns></returns>
        /// <remarks>
        /// ����˵��: 
        ///		1���޸ķ�Ʊ״̬��			Ϊ2��3	{״̬{1=����;2=����;3=��Ʊ}}
        ///		2�����ӷ�Ʊ��������		��Ʊ��Ӧ�����
        ///		3�������ɷѱ�				��Ʊ��Ӧ�����
        ///		4���޸ķ�����ϸ��			״̬��Ϊδ��	ͬʱ�����������˺�
        ///		5����ԭ���ʱ�����ǰԭʼ״̬
        /// </remarks>
        [AutoComplete]
        public long m_lngReturnChargeForBIH(clsT_Opr_Bih_Invoice_Vo p_objInvoice, int p_intState, string p_strOperatorID, string p_strOperateDateTime)
        {
            long lngRes = 0;
            if (p_strOperateDateTime == null || p_strOperateDateTime.Trim() == "")
                p_strOperateDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
            //1���޸ķ�Ʊ״̬��				Ϊ2��3	{״̬{1=����;2=����;3=��Ʊ}}
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngModifyInvoice(p_objInvoice.m_strINVOICEID_CHR, p_intState, p_strOperatorID, p_strOperateDateTime);
            }
            //2�����ӷ�Ʊ��������		��Ʊ��Ӧ�����
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngAddNewInvoicecat(p_objInvoice.m_strINVOICEID_CHR);
            }
            //3�������ɷѱ�				��Ʊ��Ӧ�����
            List<string> alPayMoneyID = new List<string>();
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = m_lngAddNewPayMoney(p_objInvoice.m_strINVOICENO_CHR, p_strOperatorID, p_strOperateDateTime, out alPayMoneyID);
            }
            //4���޸ķ�����ϸ��			״̬��Ϊδ��	ͬʱ�����������˺�
            System.Collections.ArrayList alDayAccountID = new System.Collections.ArrayList();
            if (lngRes > 0 && alPayMoneyID.Count > 0)
            {
                for (int i1 = 0; i1 < alPayMoneyID.Count; i1++)
                {
                    List<string> alItem = new List<string>();
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyPatientChargeByPayMoneyID(alPayMoneyID[i1].ToString(), p_strOperatorID, p_strOperateDateTime, out alItem);
                        if (lngRes > 0 && alItem.Count > 0)
                        {
                            for (int i2 = 0; i2 < alItem.Count; i2++)
                            {
                                if (!alDayAccountID.Contains(alItem[i2].ToString().Trim()))
                                {
                                    alDayAccountID.Add(alItem[i2].ToString().Trim());
                                }
                            }
                        }
                    }
                }
            }
            //5����ԭ���ʱ�����ǰԭʼ״̬	
            if (lngRes > 0 && alDayAccountID.Count > 0)
            {
                for (int i1 = 0; i1 < alPayMoneyID.Count; i1++)
                {
                    if (lngRes > 0)
                    {
                        lngRes = 0;
                        lngRes = m_lngModifyDayAccountByDayAccountID(alPayMoneyID[i1].ToString());
                    }
                }
            }
            if (lngRes <= 0)
            {
                throw (new Exception("����ʧ�ܣ�"));
            }
            return lngRes;
        }
        #endregion

        #region Check the invoice number.write by jli in 2005-03-23
        /// <summary>
        /// Check the invoice number.
        /// </summary>
        /// <param name="p_strInvoiceNO">Invoice number</param>
        /// <returns>OK:Count,Fail:-1</returns>
        [AutoComplete]
        public long m_lngCheckInvoiceNO(string p_strInvoiceNO)
        {
            long lngRes = 0;
            DataTable dtbRes = new DataTable();
            string strSQL = @"SELECT count(*) AS invnocount FROM T_OPR_BIH_PREPAY WHERE prepayinv_vchr='" + p_strInvoiceNO.Trim() + "'";

            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbRes);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes < 0)
            {
                return -1;
            }

            try
            {
                return long.Parse(dtbRes.Rows[0]["invnocount"].ToString().Trim());
            }
            catch
            {
                return -1;
            }
        }
        #endregion Modify End

        #region Check the clear account invoice number.write by jli in 2005-03-23
        /// <summary>
        /// Check the clear account invoice number.
        /// </summary>
        /// <param name="p_strClearAccountInvoiceNumber">clear account invoice number</param>
        /// <returns>OK:count,Fail:-1</returns>
        [AutoComplete]
        public long m_lngCheckClearAccountInvoiceNumber(string p_strClearAccountInvoiceNumber)
        {
            long lngRes = 0;
            DataTable dtbRes = new DataTable();
            string strSQL = @"SELECT count(*) AS invnocount FROM T_OPR_BIH_PAYMONEY WHERE clearinv_vchr='" + p_strClearAccountInvoiceNumber.Trim() + "'";

            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbRes);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes < 0)
            {
                return -1;
            }

            try
            {
                return long.Parse(dtbRes.Rows[0]["invnocount"].ToString().Trim());
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region Get the charge item type relation.wirte by jli in 2005-04-06
        [AutoComplete]
        public long m_lngGetChargeItemRalation(out DataTable dtbRelation)
        {
            dtbRelation = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_AID_RPT_GOP_RLA A
							RIGHT JOIN T_AID_RPT_GOP_DEF B
							ON B.groupid_chr=A.groupid_chr
							LEFT JOIN T_BSE_CHARGEITEMEXTYPE C
							ON C.typeid_chr=A.typeid_chr
							WHERE B.RPTID_CHR ='0006' and FLAG_INT ='4'";
            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbRelation);
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

        #region Get the charge item type relation by ReportName and Flag.wirte by jli in 2005-05-19
        /// <summary>
        /// Get the charge item type relation by ReportName and Flag
        /// </summary>
        /// <param name="p_objPrincipal">User's Principal</param>
        /// <param name="p_strReportName">ReportName</param>
        /// <param name="p_strFlag">Flag</param>
        /// <param name="dtbRelation">The relation result</param>
        /// <returns>Success:0,Failed:-1</returns>
        [AutoComplete]
        public long m_lngGetChargeItemRalation(string p_strReportName, string p_strFlag, out DataTable dtbRelation)
        {
            dtbRelation = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_AID_RPT_GOP_RLA A
							RIGHT JOIN T_AID_RPT_GOP_DEF B
							ON B.groupid_chr=A.groupid_chr
							AND B.RPTID_CHR=A.RPTID_CHR
							LEFT JOIN T_BSE_CHARGEITEMEXTYPE C
							ON C.typeid_chr=A.typeid_chr
							LEFT JOIN T_AID_RPT_DEF D
							ON D.RPTID_CHR=A.RPTID_CHR
							WHERE D.RPTNAME_CHR='" + p_strReportName.Trim() + "' and FLAG_INT ='" + p_strFlag.Trim() + "'";
            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbRelation);
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

        #region Get the invoice category data by the ID of invoice.write by jli in 2005-05-19
        /// <summary>
        /// Get the invoice category data by the ID of invoice
        /// </summary>
        /// <param name="p_objPrincipal">User's Principal</param>
        /// <param name="p_strInvoiceID">Invoice ID</param>
        /// /// <param name="p_strReportName">ReportName</param>
        /// <param name="p_strFlag">Flag</param>
        /// <param name="dtbInvoiceCat">The category data result</param>
        /// <returns>Success:0,Failed:-1</returns>
        [AutoComplete]
        public long m_lngGetInvoiceCategoryDataByID(string p_strInvoiceID, string p_strReportName, string p_strFlag, out DataTable dtbInvoiceCat)
        {
            dtbInvoiceCat = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_AID_RPT_GOP_RLA A
							RIGHT JOIN T_AID_RPT_GOP_DEF B
							ON TRIM(B.GROUPID_CHR)=TRIM(A.GROUPID_CHR)
							AND TRIM(B.RPTID_CHR)=TRIM(A.RPTID_CHR)
							LEFT JOIN T_BSE_CHARGEITEMEXTYPE C
							ON TRIM(C.TYPEID_CHR)=TRIM(A.TYPEID_CHR)
							LEFT JOIN T_AID_RPT_DEF D
							ON TRIM(D.RPTID_CHR)=TRIM(A.RPTID_CHR)
							LEFT JOIN T_OPR_BIH_INVOICECAT E
							ON TRIM(E.CATEGORYID_CHR)=TRIM(C.TYPEID_CHR)
							LEFT JOIN T_OPR_BIH_INVOICE F
							ON TRIM(F.INVOICEID_CHR)=TRIM(E.INVOICEID_CHR)
							RIGHT JOIN T_OPR_BIH_PAYMONEY G
							ON TRIM(F.INVOICENO_CHR)=TRIM(G.CLEARINV_VCHR)
							WHERE D.RPTNAME_CHR='" + p_strReportName.Trim() + "' and FLAG_INT ='" + p_strFlag.Trim() + "' AND INVOICEID_CHR='" + p_strInvoiceID.Trim() + "'";
            try
            {
                lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref dtbInvoiceCat);
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

        #region ���ݲ����˷�������ϸ��ˮ�ţ�����ҩƷȷ��״̬,������һ���¼�¼��ҽ����ҩ��ϸ����
        [AutoComplete]
        public long m_lngSetPatientChargeByPCHARGEID_CHR(string p_strPCHARGEID_CHR, clsT_Bih_Opr_Putmeddetail_VO p_objRecord)
        {
            long lngRes = 0;
            string strSQL = "";
            strSQL = " UPDATE T_OPR_BIH_PATIENTCHARGE SET BMSTATUS_INT=2 WHERE PCHARGEID_CHR='" + p_strPCHARGEID_CHR.ToString().Trim() + "'";


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                //����һ���¼�¼����ҩ��ϸ����
                /*
                 strSQL="";
				 strSQL = @"insert into  t_bih_opr_putmeddetail
							(
							t_bih_opr_putmeddetail.PUTMEDDETAILID_CHR,
							t_bih_opr_putmeddetail.paientid_chr,
							t_bih_opr_putmeddetail.registerid_chr,
							t_bih_opr_putmeddetail.orderid_chr,
							t_bih_opr_putmeddetail.orderexectype_int,
							t_bih_opr_putmeddetail.orderexecid_chr,
							t_bih_opr_putmeddetail.chargeitemid_chr,
							t_bih_opr_putmeddetail.get_dec,
							t_bih_opr_putmeddetail.isrich_int
							) 
							values(?,?,?,?,?,?,?,?,?)";
 
               */

                string p_strRecordID;
                clsPutMedicineSvc svc = new clsPutMedicineSvc();
                //�������Ӽ�¼
                svc.m_lngAddNewPutMedDetail(out p_strRecordID, p_objRecord);
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
        /* <<============================================ */
        #endregion
    }
}
