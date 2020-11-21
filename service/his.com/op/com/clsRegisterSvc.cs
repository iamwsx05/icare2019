using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsRegisterSvc ��ժҪ˵����
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsRegisterSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsRegisterSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����ܷ���һЩϵͳ���õĲ���
        [AutoComplete]
        public bool m_mthIsCanDo(string p_flag)
        {
            bool ret = false;
            string strSQL = @"select setid_chr, setname_vchr, setdesc_vchr, setstatus_int, moduleid_chr from t_sys_setting where setid_chr ='" + p_flag + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                long lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["SETSTATUS_INT"].ToString().Trim() == "1")
                    {
                        ret = true;
                    }
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

        #region ��ѯ������Ϣ
        [AutoComplete]
        public long m_lngFindPatient(string strName, string Sex, string brith, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strwhere = "";
            if (Sex != "")
            {
                strwhere = " and a.SEX_CHR='" + Sex + "'";
            }

            string strSQL = @"select b.PATIENTCARDID_CHR,a.LASTNAME_VCHR,a.SEX_CHR,a.BIRTH_DAT,a.HOMEADDRESS_VCHR,a.HOMEPHONE_VCHR,a.IDCARD_CHR  from t_bse_patient a,t_bse_patientcard b where  a.LASTNAME_VCHR like '" + strName + "%' and a.PATIENTID_CHR=b.PATIENTID_CHR  and b.STATUS_INT!=0" + strwhere;
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
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

        #region ������Ϣ�Ǽ�����
        [AutoComplete]
        public long m_lngPatientAffair(clsclsPatientIdxVO objPatienIdxVO, clsPatient_VO objPatienVO, clsPatientCardVO objPatienCardVO, out string cardId, out string patientID)
        {


            //����������
            long lngRes = 0;
            lngRes = m_lngAddNewPatientIdx(out objPatienIdxVO.m_strPATIENTID_CHR, objPatienIdxVO);
            objPatienVO.m_strPATIENTID_CHR = objPatienIdxVO.m_strPATIENTID_CHR;

            //������Ϣ��

            lngRes = m_lngAddNewPatient(out objPatienVO.m_strPATIENTID_CHR, objPatienVO);
            patientID = objPatienVO.m_strPATIENTID_CHR;
            objPatienCardVO.m_strPATIENTID_CHR = objPatienVO.m_strPATIENTID_CHR;

            //���˿��ű�

            lngRes = m_lngAddNewPatientCard(out cardId, objPatienCardVO);
            if (lngRes == -2)
            {
                throw new Exception("�����Ѿ���ռ�ã�");
            }

            if (lngRes > 0)
            {
                #region ��Ϣ����
                //com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update objMsgUpdate = new com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update();
                //try
                //{
                //    lngRes = objMsgUpdate.AddMsg("10001", 1, objPatienVO.m_strPATIENTID_CHR);
                //}
                //catch (Exception objEx)
                //{
                //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //    bool blnRes = objLogger.LogError(objEx);
                //    lngRes = -1;
                //}
                //finally
                //{
                //    objMsgUpdate.Dispose();
                //    objMsgUpdate = null;
                //}
                #endregion
            }

            return lngRes;
        }
        #endregion
        #region ���Ӳ��˻������������� zlc 2004 - 7-21
        /// <summary>
        /// ���Ӳ��˻�������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatientIdx(out string p_strRecordID, clsclsPatientIdxVO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.m_lngGenerateNewID("T_BSE_PATIENTIDX", "PATIENTID_CHR", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_BSE_PATIENTIDX (PATIENTID_CHR,INPATIENTID_CHR,IDCARD_CHR,HOMEADDRESS_VCHR,SEX_CHR,BIRTH_DAT,NAME_VCHR,HOMEPHONE_VCHR,INSURANCEID_VCHR,DIFFICULTY_VCHR,GOVCARD_CHR) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strINPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strIDCARD_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strHOMEADDRESS_VCHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[5].Value = DateTime.Parse(p_objRecord.m_strBIRTH_DAT);
                objLisAddItemRefArr[6].Value = p_objRecord.m_strNAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strHOMEPHONE_VCHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strINSURANCEID_VCHR;

                objLisAddItemRefArr[9].Value = p_objRecord.m_strDIFFICULTY_VCHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strGOVCARD_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region ���Ӳ��˻������� zlc 2004-7-21
        /// <summary>
        /// ���Ӳ��˻�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewPatient(out string p_strRecordID, clsPatient_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = p_objRecord.m_strPATIENTID_CHR;
            long lngRecEff = -1;
            #region iCare���Ӳ���
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //			lngRes = objHRPSvc.lngGenerateID(10,"PATIENTID_CHR","T_BSE_PATIENT",out p_strRecordID);
            //			if(lngRes < 0)
            //				return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_BSE_PATIENT (PATIENTID_CHR,INPATIENTID_CHR,LASTNAME_VCHR,IDCARD_CHR,MARRIED_CHR,BIRTHPLACE_VCHR,HOMEADDRESS_VCHR,SEX_CHR,NATIONALITY_VCHR,FIRSTNAME_VCHR,BIRTH_DAT,RACE_VCHR,NATIVEPLACE_VCHR,OCCUPATION_VCHR,NAME_VCHR,HOMEPHONE_VCHR,OFFICEPHONE_VCHR,INSURANCEID_VCHR,MOBILE_CHR,OFFICEADDRESS_VCHR,EMPLOYER_VCHR,OFFICEPC_VCHR,HOMEPC_CHR,EMAIL_VCHR,CONTACTPERSONFIRSTNAME_VCHR,CONTACTPERSONLASTNAME_VCHR,CONTACTPERSONADDRESS_VCHR,CONTACTPERSONPHONE_VCHR,CONTACTPERSONPC_CHR,PATIENTRELATION_VCHR,FIRSTDATE_DAT,ISEMPLOYEE_INT,STATUS_INT,DEACTIVATE_DAT,OPERATORID_CHR,MODIFY_DAT,PAYTYPEID_CHR,GOVCARD_CHR,BLOODTYPE_CHR,IFALLERGIC_INT,ALLERGICDESC_VCHR,DIFFICULTY_VCHR) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(42, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strINPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strLASTNAME_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strIDCARD_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strMARRIED_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strBIRTHPLACE_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strHOMEADDRESS_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strNATIONALITY_VCHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strNAME_VCHR;
                objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strBIRTH_DAT);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strRACE_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strNATIVEPLACE_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strOCCUPATION_VCHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strNAME_VCHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strHOMEPHONE_VCHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strOFFICEPHONE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strINSURANCEID_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strMOBILE_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strOFFICEADDRESS_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strEMPLOYER_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strOFFICEPC_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strHOMEPC_CHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strEMAIL_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strCONTACTPERSONFIRSTNAME_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCONTACTPERSONLASTNAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONTACTPERSONADDRESS_VCHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCONTACTPERSONPHONE_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strCONTACTPERSONPC_CHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strPATIENTRELATION_VCHR;
                objLisAddItemRefArr[30].Value = DateTime.Parse(p_objRecord.m_strFIRSTDATE_DAT);
                objLisAddItemRefArr[31].Value = p_objRecord.m_intISEMPLOYEE_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_intSTATUS_INT;
                try
                {
                    objLisAddItemRefArr[33].Value = DateTime.Parse(p_objRecord.m_strDEACTIVATE_DAT);
                }
                catch
                {
                    objLisAddItemRefArr[33].Value = null;
                }
                objLisAddItemRefArr[34].Value = p_objRecord.m_strOPERATORID_CHR;
                objLisAddItemRefArr[35].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                objLisAddItemRefArr[36].Value = p_objRecord.m_strPAYTYPEID_CHR;

                objLisAddItemRefArr[37].Value = p_objRecord.m_strGOVCARD_CHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strBLOODTYPE_CHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_intIFALLERGIC_INT;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strALLERGICDESC_VCHR;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strDIFFICULTY_VCHR;

                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region  ���Ӳ��˿���¼ zlc -7-21
        /// <summary>
        /// ���Ӳ��˿���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">���ز��˿���</param>
        /// <param name="p_objRecord">����Ϣ</param>
        /// <returns>����-2����Ĳ��˿����Ѿ���ռ��</returns>
        [AutoComplete]
        public long m_lngAddNewPatientCard(out string CardId, clsPatientCardVO p_objRecord)
        {
            long lngRes = 0;
            if (p_objRecord.m_intSTATUS_INT == 3)
                CardId = null;
            else
                CardId = p_objRecord.m_strPATIENTCARDID_CHR;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            if (p_objRecord.m_intSTATUS_INT == 3)
            {
                lngRes = objHRPSvc.lngGenerateID(10, "PATIENTCARDID_CHR", "T_BSE_PATIENTCARD", out CardId);
                if (lngRes < 0)
                    return lngRes;
                if (Convert.ToInt64(CardId) < 8000000000)
                    CardId = "8000000000";
            }
            else
            {
                strSQL = @"select PATIENTCARDID_CHR from T_BSE_PATIENTCARD where PATIENTCARDID_CHR='" + CardId + "' and STATUS_INT!=0";
                DataTable dt = new DataTable();
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);


                }
                if (dt.Rows.Count > 0)
                {
                    return -2;
                }


            }
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSQL = "insert into T_BSE_PATIENTCARD (PATIENTCARDID_CHR,PATIENTID_CHR,ISSUE_DATE,STATUS_INT) VALUES (?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = CardId;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTID_CHR;
                objLisAddItemRefArr[2].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[3].Value = p_objRecord.m_intSTATUS_INT;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region ����һ���Һ�
        /// <summary>
        /// ����һ���Һ�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="objResult"></param>
        /// <param name="strID"></param>
        /// <param name="strNo"></param>
        /// <param name="strOrderNo"></param>
        /// <param name="strRegCount"></param>
        /// <returns>����-2����ʧ������Ĳ��˿����Ѿ���ռ��</returns>
        [AutoComplete]
        public long m_lngAddNewPatientRegister(clsPatientRegister_VO objResult,
            out string strID, out string strNo, out string strOrderNo, out string strRegCount, clsPatient_VO clsPatientvo, int isNewPatient, string strCardID, out string outCardID, clsPatientDetail_VO[] PatientDetail_VO, string paytypeid, string patientidentityno)
        {
            long lngRes = 0;
            strID = "";
            strNo = "";
            strOrderNo = "";
            strRegCount = "0";
            outCardID = "";
            string CardID = "";
            string PatientID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc = new clsHRPTableService();
            string strSQL = "";
            #region ������²�����Ǽǲ�����Ϣ
            if (isNewPatient == 1)
            {
                clsclsPatientIdxVO p_objRecord = new clsclsPatientIdxVO();
                p_objRecord.m_strBIRTH_DAT = clsPatientvo.strBirthDate;
                p_objRecord.m_strDIFFICULTY_VCHR = clsPatientvo.m_strDIFFICULTY_VCHR;
                p_objRecord.m_strGOVCARD_CHR = clsPatientvo.m_strGOVCARD_CHR;
                p_objRecord.m_strHOMEADDRESS_VCHR = clsPatientvo.m_strHOMEADDRESS_VCHR;
                p_objRecord.m_strHOMEPHONE_VCHR = clsPatientvo.m_strHOMEPHONE_VCHR;
                p_objRecord.m_strINSURANCEID_VCHR = clsPatientvo.m_strINSURANCEID_VCHR;
                p_objRecord.m_strNAME_VCHR = clsPatientvo.m_strLASTNAME_VCHR;
                p_objRecord.m_strSEX_CHR = clsPatientvo.m_strSEX_CHR;
                clsPatientCardVO CardVO = new clsPatientCardVO();
                CardVO.m_strISSUE_DATE = clsPatientvo.m_strFIRSTDATE_DAT;

                if (strCardID == "")
                {
                    CardVO.m_intSTATUS_INT = 3;
                }
                else
                {
                    CardVO.m_strPATIENTCARDID_CHR = strCardID;
                    CardVO.m_intSTATUS_INT = 1;
                }
                try
                {
                    lngRes = m_lngPatientAffair(p_objRecord, clsPatientvo, CardVO, out CardID, out PatientID);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    return lngRes;
                }
                objResult.m_objPatientCard.m_strCardID = CardID;
                objResult.m_strPatient = PatientID;
            }
            else //�޸Ĳ��˹��ѡ�ҽ������������Ϣ
            {
                if (clsPatientvo.m_intERNALFLAG_INT != 0)
                {
                    long lngRes_Msg = 0;
                    switch (clsPatientvo.m_intERNALFLAG_INT)
                    {
                        case 1:
                            strSQL = @"update T_BSE_PATIENT set GOVCARD_CHR='" + clsPatientvo.m_strGOVCARD_CHR + "' where PATIENTID_CHR='" + objResult.m_strPatient + "'";
                            break;
                        case 2:
                            strSQL = @"update T_BSE_PATIENT set INSURANCEID_VCHR='" + clsPatientvo.m_strINSURANCEID_VCHR + "' where PATIENTID_CHR='" + objResult.m_strPatient + "'";
                            break;
                        case 3:
                            strSQL = @"update T_BSE_PATIENT set DIFFICULTY_VCHR='" + clsPatientvo.m_strDIFFICULTY_VCHR + "' where PATIENTID_CHR='" + objResult.m_strPatient + "'";
                            break;
                    }
                    try
                    {
                        lngRes_Msg = objSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                    #region ��Ϣ����
                    if (lngRes_Msg > 0)
                    {
                        //com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update objMsgUpdate = new com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update();
                        //try
                        //{
                        //    lngRes = objMsgUpdate.AddMsg("10001", 2, objResult.m_strPatient);
                        //}
                        //catch (Exception objEx)
                        //{
                        //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        //    bool blnRes = objLogger.LogError(objEx);
                        //    lngRes = -1;
                        //}
                        //finally
                        //{
                        //    objMsgUpdate.Dispose();
                        //    objMsgUpdate = null;
                        //}
                    }
                    #endregion
                }
            }

            //��������ݶ�Ӧ�ű�    
            if (paytypeid != "")
            {
                if (patientidentityno.Trim() == "")
                {
                    patientidentityno = " ";
                }

                strSQL = "delete from t_bse_patientidentityno where patientid_chr = ? and paytypeid_chr = ? ";
                System.Data.IDataParameter[] paramArr = null;
                objSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = objResult.m_strPatient;
                paramArr[1].Value = paytypeid;
                long lngRecordsAffected = -1;
                lngRes = objSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

                strSQL = @"insert into t_bse_patientidentityno(patientid_chr, paytypeid_chr, idno_vchr)
                                                values (?,?,?)";
                paramArr = null;
                objSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = objResult.m_strPatient;
                paramArr[1].Value = paytypeid;
                paramArr[2].Value = patientidentityno;

                lngRecordsAffected = -1;
                lngRes = objSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            #endregion

            outCardID = objResult.m_objPatientCard.m_strCardID;
            DataTable dt = new DataTable();
            #region

            //�Һ���ˮ��
            string p_strREGISTERNO = "";
            strSQL = @"SELECT MAX (registerno_chr) as  registerno
                     FROM t_opr_patientregister 
                     WHERE registerdate_dat = to_date('" + objResult.m_strRegisterDate + "','yyyy-MM-dd')";
            try
            {
                objSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["registerno"] != System.DBNull.Value)
                {
                    long dblregisterno = Convert.ToInt64(dt.Rows[0]["registerno"].ToString().Trim()) + 1;
                    p_strREGISTERNO = dblregisterno.ToString();
                }
                else
                {
                    p_strREGISTERNO = DateTime.Parse(objResult.m_strRegisterDate).Year.ToString() + DateTime.Parse(objResult.m_strRegisterDate).Month.ToString("00") + DateTime.Parse(objResult.m_strRegisterDate).Date.ToString("00") + "00001";
                }
            }
            else
            {
                p_strREGISTERNO = DateTime.Parse(objResult.m_strRegisterDate).Year.ToString() + DateTime.Parse(objResult.m_strRegisterDate).Month.ToString("00") + DateTime.Parse(objResult.m_strRegisterDate).Date.ToString("00") + "00001";
            }

            #region д��Һű�����
            string p_strREGISTERID = "";
            if (objResult.m_objPatientCard.m_strCardID == "")
            {
                System.IO.File.Copy(@"D:\code\log.txt", @"C:\log.txt", true);
                throw new Exception(@"�Һ�ϵͳ�������صĴ������'D:\code\log.txt'�ļ����沢����ʵʩ��Ա������");
            }
            lngRes = objSvc.m_lngGenerateNewID("t_opr_patientregister", "registerid_chr", out p_strREGISTERID);
            strSQL = @"INSERT INTO t_opr_patientregister
                           (registerid_chr, patientcardid_chr, registerdate_dat,
                            diagdept_chr, diagdoctor_chr, registeremp_chr, pstatus_int,
                            registertypeid_chr, paytypeid_chr, registerno_chr, flag_int,
                            planperiod_chr, patientid_chr, paytype_int,INVNO_CHR,bespeakDate_DAT,DATESPACE
                           )
					VALUES (?,?,to_date(?,'yyyy-mm-dd hh24:mi:ss'),?,?,?,?,?,?,?,?,?,?,?,?,to_date(?,'yyyy-mm-dd hh24:mi:ss'),?)";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objSvc.CreateDatabaseParameter(17, out paramArr);
                paramArr[0].Value = p_strREGISTERID;
                paramArr[1].Value = objResult.m_objPatientCard.m_strCardID;
                paramArr[2].Value = objResult.m_strRegisterDate;
                paramArr[3].Value = objResult.m_objDiagDept.strDeptID;
                paramArr[4].Value = objResult.m_objDiagDoctor.strEmpID;
                paramArr[5].Value = objResult.m_objRegisterEmp.strEmpID;
                paramArr[6].Value = objResult.m_intPStatus;
                paramArr[7].Value = objResult.m_strRegisterType.m_strRegisterTypeID;
                paramArr[8].Value = objResult.m_strPayType.m_strPayTypeID;


                paramArr[9].Value = p_strREGISTERNO;
                paramArr[10].Value = objResult.m_intFlag;
                paramArr[11].Value = objResult.m_strPiod;
                paramArr[12].Value = objResult.m_strPatient;
                paramArr[13].Value = objResult.m_decRegisterPay;
                paramArr[14].Value = objResult.strINVNO_CHR;
                paramArr[15].Value = objResult.strbespeakDate;
                paramArr[16].Value = objResult.strbespeak;
                long lngRecordsAffected = -1;
                lngRes = objSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion

            #region �޸���ر������
            string date = "";
            if (objResult.m_intFlag == "2")
            {
                date = objResult.strbespeakDate;

            }
            else
            {
                date = objResult.m_strRegisterDate;
            }
            strSQL = @" UPDATE t_opr_opdoctorplan
					SET optimes_int = NVL (optimes_int, 0) + 1
					WHERE opdcotor_chr = ? AND plandate_dat = to_date(?,'yyyy-MM-dd') AND planperiod_chr =?";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = objResult.m_objDiagDoctor.strEmpID;
                paramArr[1].Value = date;
                paramArr[2].Value = objResult.m_strPiod;
                long lngRecordsAffected = -1;
                lngRes = objSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @" UPDATE t_bse_patient
					SET optimes_int = NVL (optimes_int, 0) + 1
					WHERE patientid_chr = '" + objResult.m_strPatient + "'";
            try
            {
                objSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion

            strSQL = @"SELECT COUNT (*) as totMunber
                        FROM t_opr_opdoctorplan
                        WHERE opdcotor_chr ='" + objResult.m_objDiagDoctor.strEmpID + @"'
                              AND plandate_dat = to_date('" + date + "','yyyy-MM-dd') AND planperiod_chr ='" + objResult.m_strPiod + "'";
            try
            {
                //System.Data.IDataParameter[] paramArr = null;
                //objSvc.CreateDatabaseParameter(3, out paramArr);
                //paramArr[0].Value = objResult.m_objDiagDoctor.strEmpID;
                //paramArr[1].Value = date;
                //paramArr[2].Value = objResult.m_strPiod;
                //lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);
                lngRes = objSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            int intregistercount = 0;
            if (double.Parse(dt.Rows[0]["totMunber"].ToString()) > 0)
            {
                strSQL = @"SELECT TO_CHAR (NVL (optimes_int, 0)) as  registercount
                         FROM t_opr_opdoctorplan
                         WHERE opdcotor_chr =?
                              AND plandate_dat = to_date(?,'yyyy-MM-dd')
                              AND planperiod_chr =?";
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    objSvc.CreateDatabaseParameter(3, out paramArr);
                    paramArr[0].Value = objResult.m_objDiagDoctor.strEmpID;
                    paramArr[1].Value = date;
                    paramArr[2].Value = objResult.m_strPiod;
                    lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    intregistercount = int.Parse(dt.Rows[0]["registercount"].ToString());
                }
            }
            //��ȡ����ID��
            string strWaitID = "";
            objSvc.m_lngGenerateNewID("t_opr_waitdiaglist", "waitdiaglistid_chr", out strWaitID);
            //��ȡ�����
            int strWaitNO = 1;
            //            if (objResult.m_objDiagDoctor.strEmpID != null && objResult.m_objDiagDoctor.strEmpID != "")
            //            {
            //                strSQL = @"SELECT max(order_int) as waitNO
            //                         FROM t_opr_waitdiaglist
            //                         WHERE waitdiagdr_chr = '" + objResult.m_objDiagDoctor.strEmpID + @"'
            //                               AND PSTATUS_INT=1
            //                               AND registerdate_dat = to_date('" + objResult.m_strRegisterDate + "','yyyy-mm-dd hh24:mi:ss')";
            //            }
            //            else
            //            {
            strSQL = @"SELECT max(order_int) as waitNO
				FROM t_opr_waitdiaglist
				WHERE waitdiagdept_chr =?
					AND PSTATUS_INT=1 AND registerdate_dat = to_date(?,'yyyy-mm-dd hh24:mi:ss')";
            //}
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = objResult.m_objDiagDept.strDeptID;
                paramArr[1].Value = date;

                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["waitNO"] != System.DBNull.Value && dt.Rows[0]["waitNO"].ToString() != "0" && dt.Rows[0]["waitNO"].ToString() != "")
                {
                    strWaitNO = int.Parse(dt.Rows[0]["waitNO"].ToString()) + 1;

                }
                else
                {
                    strWaitNO = 1;
                }
            }
            else
            {
                strWaitNO = 1;
            }
            strSQL = @"INSERT INTO t_opr_waitdiaglist
               (waitdiaglistid_chr, registerid_chr, waitdiagdept_chr,
                waitdiagdr_chr, order_int, registerdate_dat,
                pstatus_int, registerop_vchr
               )
               VALUES (?,?,?,?,?,to_date(?,'yyyy-mm-dd hh24:mi:ss'),?,?)";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objSvc.CreateDatabaseParameter(8, out paramArr);
                paramArr[0].Value = strWaitID;
                paramArr[1].Value = p_strREGISTERID;
                paramArr[2].Value = objResult.m_objDiagDept.strDeptID;
                paramArr[3].Value = objResult.m_objDiagDoctor.strEmpID;
                paramArr[4].Value = strWaitNO;
                paramArr[5].Value = date;
                paramArr[6].Value = 1;
                paramArr[7].Value = objResult.m_strPiod;
                long lngRecordsAffected = -1;
                lngRes = objSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strID = p_strREGISTERID;
            strNo = p_strREGISTERNO;
            strOrderNo = strWaitNO.ToString();
            strRegCount = intregistercount.ToString();
            #endregion
            m_lngAddNewRegDetail(PatientDetail_VO, strID);
            //if (objResult.m_strPatient != "" && objResult.m_strPayType.m_strPayTypeID != "" )
            //{
            //    lngRes = m_lngAddPatientIdTypeIdNo(objPri, objResult.m_strPatient, objResult.m_strPayType.m_strPayTypeID, objResult.m_strPayType.m_strPayTypeNo);
            //}
            return lngRes;
        }
        #region ���ӹҺ���ϸ
        [AutoComplete]
        public long m_lngAddNewRegDetail(clsPatientDetail_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO t_opr_patientregdetail (REGISTERID_CHR,CHARGEID_CHR,PAYMENT_MNY,DISCOUNT_DEC) VALUES (?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strCHARGEID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_dblPAYMENT_MNY;
                objLisAddItemRefArr[3].Value = p_objRecord.m_fltDISCOUNT_DEC;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region ���ӹҺ���ϸ
        [AutoComplete]
        public long m_lngAddNewRegDetail(clsPatientDetail_VO[] p_objRecord, string strID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL;
            for (int i1 = 0; i1 < p_objRecord.Length; i1++)
            {
                strSQL = "INSERT INTO t_opr_patientregdetail (REGISTERID_CHR,CHARGEID_CHR,PAYMENT_MNY,DISCOUNT_DEC) VALUES (?,?,?,?)";
                try
                {
                    System.Data.IDataParameter[] objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                    //Please change the datetime and reocrdid 
                    objLisAddItemRefArr[0].Value = strID;
                    objLisAddItemRefArr[1].Value = p_objRecord[i1].m_strCHARGEID_CHR;
                    objLisAddItemRefArr[2].Value = p_objRecord[i1].m_dblPAYMENT_MNY;
                    objLisAddItemRefArr[3].Value = p_objRecord[i1].m_fltDISCOUNT_DEC;
                    long lngRecEff = -1;
                    //�������Ӽ�¼
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion
        #endregion

        #region �޸ĹҺ���ϸ
        [AutoComplete]
        public long m_lngModifyRegDetail(clsPatientDetail_VO p_objRecord)
        {
            DataTable dtRegister = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT
                              A.REGISTERID_CHR , A.CHARGEID_CHR , A.CHARGENAME_CHR , A.PAYMENT_MNY , A.DISCOUNT_DEC
                              FROM
                              V_OPR_PATIENTREGDETAIL A where REGISTERID_CHR=? and A.CHARGEID_CHR=?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();


            System.Data.IDataParameter[] paramArr = null;
            HRPSvc.CreateDatabaseParameter(2, out paramArr);
            paramArr[0].Value = p_objRecord.m_strREGISTERID_CHR;
            paramArr[1].Value = p_objRecord.m_strCHARGEID_CHR;

            lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRegister, paramArr);
            if (dtRegister.Rows.Count > 0)
            {
                try
                {
                    strSQL = @"update T_OPR_PATIENTREGDETAIL A 
				    set PAYMENT_MNY =?,DISCOUNT_DEC=? where REGISTERID_CHR=? and A.CHARGEID_CHR=?";

                    paramArr = null;
                    HRPSvc.CreateDatabaseParameter(4, out paramArr);
                    paramArr[0].Value = p_objRecord.m_dblPAYMENT_MNY.ToString();
                    paramArr[1].Value = p_objRecord.m_fltDISCOUNT_DEC;
                    paramArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                    paramArr[3].Value = p_objRecord.m_strCHARGEID_CHR;

                    lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtRegister, paramArr);
                }
                catch (Exception e)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    objLogger.LogError(e);
                }
            }
            else
            {
                lngRes = m_lngAddNewRegDetail(p_objRecord);
            }

            return lngRes;
        }
        #endregion

        #region �޸ĹҺż�¼
        /// <summary>
        /// �޸ĹҺż�¼��һ��ֻ���޸Ŀ��ҡ�ҽ�����Һ���𡢷��ã�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdPatientRegisterByID(clsPatientRegister_VO objResult)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"update t_opr_patientregister a
							set A.PAYTYPEID_CHR=?,
								A.PATIENTCARDID_CHR =?,
								A.PATIENTID_CHR =?							
				          Where registerid_chr=?";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = objResult.m_strPayType.m_strPayTypeID;
                paramArr[1].Value = objResult.m_objPatientCard.m_strCardID;
                paramArr[2].Value = objResult.m_strPatient;
                paramArr[3].Value = objResult.m_strRegisterID;
                long lngRecordsAffected = -1;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = "P_OPR_MARKREG";
            clsSQLParamDefinitionVO[] objParameter = new clsSQLParamDefinitionVO[5];
            for (int i = 0; i < objParameter.Length; i++)
                objParameter[i] = new clsSQLParamDefinitionVO();
            objParameter[0].objParameter_Value = objResult.m_strRegisterID;
            objParameter[1].objParameter_Value = objResult.m_objRegisterEmp.strEmpID;
            objParameter[2].objParameter_Value = 2;
            objParameter[3].objParameter_Value = objResult.m_strRegisterDate;

            objParameter[0].strParameter_Type = "Varchar2";
            objParameter[1].strParameter_Type = "Varchar2";
            objParameter[2].strParameter_Type = "Int32";
            objParameter[3].strParameter_Type = "Varchar2";
            objParameter[4].strParameter_Type = "Int32";
            objParameter[4].strParameter_Direction = "Output";

            try
            {
                lngRes = HRPSvc.lngExecuteParameterProc(strSQL, objParameter);
                string strReturn = objParameter[4].objParameter_Value.ToString().Trim();
                if (strReturn == "-1")
                {
                    return 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���¹���״̬
        /// <summary>
        /// ���ĵ�ǰ��¼������״̬ 1-���� 2-���� 3-ȡ��
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strStatus"></param>
        /// <param name="strRegID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdatePStatus(string strStatus, string strRegID)
        {
            long lngRes = 0;

            string strSQL = "Update t_opr_patientregister  Set pstatus_int=? Where registerid_chr=? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = strStatus;
                paramArr[1].Value = strRegID;
                long lngRecordsAffected = -1;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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

        #region ���¹Һż�¼״̬���˺ţ�
        /// <summary>
        /// ���¹Һż�¼״̬���˺ţ�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strStatus"></param>
        /// <param name="strRegID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDropRegister(string strReturnEmp, string strRegID)
        {
            long lngRes = 0;
            //�˺�ʱ�ñ�־Ϊ3
            string strSQL = "Update t_opr_patientregister  Set flag_int=3,returnemp_chr='" + strReturnEmp + "'," +
                " returndate_dat=sysdate Where registerid_chr='" + strRegID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = strReturnEmp;
                paramArr[1].Value = strRegID;
                long lngRecordsAffected = -1;
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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

        #region ��ѯ���˼�¼
        /// <summary>
        /// ��ѯ���˼�¼
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="CardID"></param>
        /// <param name="clsResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatByCardID(string CardID, out clsPatient_VO clsResult, string registerDate, out string DepName, out string doctorName, out string reCorddate)
        {
            DepName = null;
            doctorName = null;
            reCorddate = null;
            clsResult = new clsPatient_VO();
            long lngRes = 0;

            string strSQL = @"SELECT a.patientid_chr,
       a.inpatientid_chr,
       a.lastname_vchr,
       a.idcard_chr,
       a.married_chr,
       a.birthplace_vchr,
       a.homeaddress_vchr,
       a.sex_chr,
       a.nationality_vchr,
       a.firstname_vchr,
       a.birth_dat,
       a.race_vchr,
       a.nativeplace_vchr,
       a.occupation_vchr,
       a.name_vchr,
       a.homephone_vchr,
       a.officephone_vchr,
       a.insuranceid_vchr,
       a.mobile_chr,
       a.officeaddress_vchr,
       a.employer_vchr,
       a.officepc_vchr,
       a.homepc_chr,
       a.email_vchr,
       a.contactpersonfirstname_vchr,
       a.contactpersonlastname_vchr,
       a.contactpersonaddress_vchr,
       a.contactpersonphone_vchr,
       a.contactpersonpc_chr,
       a.patientrelation_vchr,
       a.firstdate_dat,
       a.isemployee_int,
       a.status_int,
       a.deactivate_dat,
       a.operatorid_chr,
       a.modify_dat,
       a.paytypeid_chr,
       a.optimes_int,
       a.govcard_chr,
       a.bloodtype_chr,
       a.ifallergic_int,
       a.allergicdesc_vchr,
       a.difficulty_vchr,
       a.extendid_vchr,
       a.inpatienttempid_vchr,
       a.modifytime_dat,
       a.modifyman_vchr,
       a.registertime_dat,
       a.registerman_vchr,
       a.patientsources_vchr, c.paytypename_vchr,c.INTERNALFLAG_INT " +//, c.discount_dec " +
                " FROM t_bse_patient a, t_bse_patientcard b, t_bse_patientpaytype c " +
                " WHERE a.patientid_chr=b.patientid_chr And " +
                " b.patientcardid_chr = '" + CardID + "' AND a.paytypeid_chr = c.paytypeid_chr(+) and b.STATUS_INT<>0 ";
            DataTable dtResult = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            try
            {
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    clsResult.m_strGOVCARD_CHR = dtResult.Rows[0]["GOVCARD_CHR"].ToString().Trim();
                    clsResult.m_strDIFFICULTY_VCHR = dtResult.Rows[0]["DIFFICULTY_VCHR"].ToString().Trim();
                    clsResult.strInsuranceID = dtResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    clsResult.strBirthDate = dtResult.Rows[0]["BIRTH_DAT"].ToString().Trim();
                    clsResult.strName = dtResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    clsResult.strPatientCardID = CardID;
                    clsResult.strPatientID = dtResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                    clsResult.strSex = dtResult.Rows[0]["SEX_CHR"].ToString().Trim();
                    clsResult.objPatType = new clsPatientType_VO();
                    clsResult.objPatType.m_strPayTypeID = dtResult.Rows[0]["PAYTYPEID_CHR"].ToString().Trim();
                    clsResult.objPatType.m_strPayTypeName = dtResult.Rows[0]["paytypename_vchr"].ToString().Trim();
                    clsResult.strID_Card = dtResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
                    clsResult.m_intERNALFLAG_INT = Convert.ToInt16(dtResult.Rows[0]["INTERNALFLAG_INT"].ToString().Trim());
                    clsResult.strInsuranceID = dtResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    if (dtResult.Rows[0]["OPTIMES_INT"] == Convert.DBNull)
                    {
                        clsResult.m_strOPTIMES_INT = "0";
                    }
                    else
                    {
                        clsResult.m_strOPTIMES_INT = dtResult.Rows[0]["OPTIMES_INT"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select a.RECORDDATE_DAT,a.REGISTERID_CHR,b.DEPTNAME_VCHR,c.LASTNAME_VCHR   from t_opr_patientregister a,T_BSE_DEPTDESC b,t_bse_employee  c where a.REGISTERDATE_DAT=to_date('" + registerDate + "','yyyy-MM-dd')  and a.PATIENTCARDID_CHR='" + CardID + "' and a.DIAGDEPT_CHR=b.DEPTID_CHR(+) and a.DIAGDOCTOR_CHR=c.EMPID_CHR(+)";
            try
            {
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtResult.Rows.Count > 0)
            {
                if (dtResult.Rows[0]["REGISTERID_CHR"] != System.DBNull.Value)
                {
                    if (dtResult.Rows[0]["DEPTNAME_VCHR"] != System.DBNull.Value)
                        DepName = dtResult.Rows[0]["DEPTNAME_VCHR"].ToString();
                    else
                        DepName = "û�п���";
                    if (dtResult.Rows[0]["LASTNAME_VCHR"] != System.DBNull.Value)
                        doctorName = dtResult.Rows[0]["LASTNAME_VCHR"].ToString();
                    else
                        doctorName = "û��ҽ��";
                    reCorddate = dtResult.Rows[0]["RECORDDATE_DAT"].ToString();
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�ҹҺ�����״̬
        /// <summary>
        /// ��ѯ�ҹҺ�����״̬
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strTypeid"></param>
        /// <param name="command">���عҺŷ��õ�״̬��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindType(string strTypeid, out string command)
        {
            command = "";
            long lngRes = 0;
            string strSQL = "SELECT ISDOCTOR_NUM FROM  T_BSE_REGISTERTYPE  where REGISTERTYPENAME_VCHR='" + strTypeid + "'";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    command = dtResult.Rows[0]["ISDOCTOR_NUM"].ToString();
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

        #region ��ѯ��������
        [AutoComplete]
        public long m_lngGetPatType(out clsPatientType_VO[] clsResult)
        {
            clsResult = new clsPatientType_VO[0];
            long lngRes = 0;

            string strSQL = "";

            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

                bool b = false;
                strSQL = "select setid_chr, setname_vchr, setdesc_vchr, setstatus_int, moduleid_chr from t_sys_setting where setid_chr = '0063'";
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    b = dtResult.Rows[0]["setstatus_int"].ToString().Trim() == "1";
                }

                if (b)
                {
                    Hashtable has = new Hashtable();
                    has.Add("monday", "1");
                    has.Add("tuesday", "2");
                    has.Add("wednesday", "3");
                    has.Add("thursday", "4");
                    has.Add("friday", "5");
                    has.Add("saturday", "6");
                    has.Add("sunday", "7");

                    b = false;

                    string NowWeekNo = has[DateTime.Now.DayOfWeek.ToString().ToLower()].ToString();

                    strSQL = "select weekno_int, timespan_vchr from t_opr_ybtimespan where weekno_int = " + NowWeekNo;
                    lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                    if (dtResult.Rows.Count == 1)
                    {
                        string TimeSpan = dtResult.Rows[0]["timespan_vchr"].ToString().Trim();

                        DateTime dte1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + TimeSpan.Substring(0, 5) + ":01");
                        DateTime dte2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + TimeSpan.Substring(6) + ":59");

                        if (DateTime.Now >= dte1 && DateTime.Now <= dte2)
                        {
                            b = true;
                        }
                    }
                }

                if (b)
                {
                    strSQL = "select paytypeid_chr, paytypename_vchr, memo_vchr, paylimit_mny, payflag_dec, paypercent_dec, paytypeno_vchr, isusing_num, copayid_chr, chargepercent_dec, internalflag_int, coalitionrecipeflag_int, bihlimitrate_dec from t_bse_patientpaytype where isusing_num=1 and PAYFLAG_DEC<>2 and paytypename_vchr <> '�ض�ҽ��' order by PAYTYPENO_VCHR";
                }
                else
                {
                    strSQL = "select paytypeid_chr, paytypename_vchr, memo_vchr, paylimit_mny, payflag_dec, paypercent_dec, paytypeno_vchr, isusing_num, copayid_chr, chargepercent_dec, internalflag_int, coalitionrecipeflag_int, bihlimitrate_dec from t_bse_patientpaytype where isusing_num=1 and payflag_dec<>2  order by paytypeno_vchr";
                }

                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                HRPSvc.Dispose();
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    clsResult = new clsPatientType_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        clsResult[i1] = new clsPatientType_VO();
                        clsResult[i1].m_strPayTypeID = dtResult.Rows[i1]["PAYTYPEID_CHR"].ToString().Trim();
                        clsResult[i1].m_strPayTypeName = dtResult.Rows[i1]["paytypename_vchr"].ToString().Trim();
                        clsResult[i1].m_strPayTypeNo = dtResult.Rows[i1]["paytypeno_vchr"].ToString().Trim();
                        if (dtResult.Rows[i1]["PAYFLAG_DEC"] != System.DBNull.Value)
                            clsResult[i1].m_decDiscount = decimal.Parse(dtResult.Rows[i1]["PAYFLAG_DEC"].ToString().Trim());
                        else
                            clsResult[i1].m_decDiscount = 0;
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

        #region ���»�ȡ��ǰ�Һż�¼ zlc
        [AutoComplete]
        public long m_lngGetCurRegisterByID(string strRegisterID, out DataTable dtbSource)
        {
            long lngRes = 0;
            dtbSource = null;
            string strsql = @"select distinct a.pstatus_int,
       a.registertypeid_chr,
       a.paytypeid_chr,
       a.registerno_chr,
       a.flag_int,
       a.returnemp_chr,
       a.returndate_dat,
       a.recorddate_dat,
       a.planperiod_chr,
       a.patientid_chr,
       a.paytype_int,
       a.balance_dat,
       a.invno_chr,
       a.balanceemp_chr,
       a.bespeakdate_dat,
       a.datespace,
       a.confirmemp_chr,
       a.confirmdate_dat,
       a.takedoctor_chr,
       a.diagdoctor_chr,
       a.diagdept_chr,
       a.registerid_chr,
       a.patientcardid_chr,
       a.registerdate_dat,
       a.registeremp_chr,b.order_int,c.opaddress_vchr,c.planperiod_chr,d.registertypename_vchr,f.name_vchr,
				a.diagpay_mny+a.registerpay_mny as payinall_mny ,g.deptname_vchr 
				from t_opr_patientregister a inner join t_opr_waitdiaglist b on a.registerid_chr=b.registerid_chr
				inner join t_opr_opdoctorplan c on a.diagdoctor_chr = c.opdcotor_chr 
				inner join t_bse_registertype d on a.registertypeid_chr = d.registertypeid_chr 
				inner join t_bse_patientcard e on a.patientcardid_chr = e.patientcardid_chr 
				inner join t_bse_patientidx f on e.patientid_chr = f.patientid_chr 
				inner join t_bse_deptdesc g on a.diagdept_chr =g.deptid_chr where a.registerid_chr = '" + strRegisterID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.DoGetDataTable(strsql, ref dtbSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //dtbSource = dt;

            return lngRes;
        }
        #endregion

        #region ���»�ȡ��ǰ�Һż�¼ zlc
        [AutoComplete]
        public long m_lngGetCurRegisterByNo(string strRegisterID, string strDate, out clsPatientRegister_VO objreg)
        {
            long lngRes = 0;
            string strsql = @"SELECT
   A.REGID , A.CARDID , A.REGDATE , A.DEPTID , A.DOCID
 , A.REGEMP , A.PSTATUS , A.REGTYPEID , A.PATTYPEID , A.REGNO
 , A.FLAG , A.DROPEMP , A.DROPDATE , A.RECORDDATE , A.PATTYPENAME
 , A.REGTYPENAME , A.NAME , A.SEX , A.BIRTH , A.PATID
 , A.DEPTNAME , A.DOCNAME , A.AGE ,  A.PLANPERIOD
 , A.STARTTIME , A.ENDTIME , A.OPADDRESS , A.EMPNO , A.ORDERNO
FROM
   ICARE.VW_OPREGISTER A  where a.REGNO =? and a.REGDATE >= to_date(?,'yyyy-mm-dd') " + " and a.FLAG<>3";
            DataTable dtbSource = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                HRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = strRegisterID;
                paramArr[1].Value = strDate;
                lngRes = HRPSvc.lngGetDataTableWithParameters(strsql, ref dtbSource, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            objreg = new clsPatientRegister_VO();
            objreg.m_objPatientCard = new clsPatientCard_VO();
            objreg.m_clsPatientVO = new clsPatientVO();
            objreg.m_strPayType = new clsPatientType_VO();
            objreg.m_clsOPDoctorPlanVO = new clsOPDoctorPlan_VO();
            objreg.m_strRegisterType = new clsRegisterType_VO();
            objreg.m_objDiagDept = new clsDepartmentVO();
            objreg.m_objDiagDoctor = new clsEmployeeVO();
            objreg.m_objRegisterEmp = new clsEmployeeVO();
            if (dtbSource.Rows.Count == 1)
            {
                objreg.m_objPatientCard.m_strCardID = dtbSource.Rows[0]["CARDID"].ToString();
                objreg.m_strRegisterID = dtbSource.Rows[0]["REGID"].ToString();
                objreg.m_clsPatientVO.strName = dtbSource.Rows[0]["NAME"].ToString();
                objreg.m_strPatient = dtbSource.Rows[0]["PATID"].ToString();
                objreg.m_clsPatientVO.strSex = dtbSource.Rows[0]["SEX"].ToString();
                objreg.m_clsPatientVO.strBirthDate = dtbSource.Rows[0]["BIRTH"].ToString();

                objreg.m_strPayType.m_strPayTypeName = dtbSource.Rows[0]["PATTYPENAME"].ToString();
                objreg.m_strPayType.m_strPayTypeID = dtbSource.Rows[0]["PATTYPEID"].ToString();

                objreg.m_objDiagDept.strDeptName = dtbSource.Rows[0]["DEPTNAME"].ToString();
                objreg.m_objDiagDept.strDeptID = dtbSource.Rows[0]["DEPTID"].ToString();
                objreg.m_strRegisterType.m_strRegisterTypeName = dtbSource.Rows[0]["REGTYPENAME"].ToString();
                objreg.m_strRegisterType.m_strRegisterTypeID = dtbSource.Rows[0]["REGTYPEID"].ToString();
                objreg.m_objDiagDoctor.strFirstName = dtbSource.Rows[0]["DOCNAME"].ToString();
                objreg.m_objDiagDoctor.strEmpID = dtbSource.Rows[0]["DOCID"].ToString();
                objreg.m_objRegisterEmp.strEmpNO = dtbSource.Rows[0]["EMPNO"].ToString();

                objreg.m_clsOPDoctorPlanVO.m_strStartTime = dtbSource.Rows[0]["STARTTIME"].ToString();
                objreg.m_clsOPDoctorPlanVO.m_strEndTime = dtbSource.Rows[0]["ENDTIME"].ToString();
                objreg.m_clsOPDoctorPlanVO.m_strOPAddress = dtbSource.Rows[0]["OPADDRESS"].ToString();
                objreg.m_strPiod = dtbSource.Rows[0]["PLANPERIOD"].ToString();
            }
            else
            {
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion

        #region ���ص�ǰҽ���ѽ��ﲡ����
        [AutoComplete]
        public long m_lngGetDocTakeCount(string strDocID, string strRegDate, out int p_Count)
        {
            p_Count = 0;
            long lngRes = 0;

            string strSQL = "select count(order_int) from t_opr_waitdiaglist " +
                " where waitdiagdr_chr=? " +
                " And registerdate_dat=?  ";
            System.DateTime RegDate = Convert.ToDateTime(strRegDate).Date;

            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPar = HRPSvc.CreateDatabaseParameter(new object[] { strDocID, RegDate });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPar);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0][0].ToString().Trim() != "")
                        p_Count = int.Parse(dtResult.Rows[0][0].ToString().Trim());
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ڲ�ѯ�Һ�
        /// <summary>
        /// �����ڲ�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dtRegister">���һ����</param>
        /// <param name="firstdate">��ʼ����</param>
        /// <param name="lastdate">��ֹ����</param>
        /// <returns>������</returns>
        [AutoComplete]
        public long m_lngQulRegByDate(out DataTable dtRegister, string firstdate, string lastdate)
        {
            dtRegister = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT distinct
   A.REGID , A.CARDID , A.REGNO , A.ORDERNO,A.REGTYPENAME , A.NAME , A.SEX , A.BIRTH ,case when a.paytype=0 then '�ֽ�' when a.paytype=1 then '����'  when a.paytype=2 then '֧Ʊ' end as paytype, A.REGDATE , A.DEPTID , A.DOCID ,A.DEPTNAME , A.DOCNAME
 , A.REGEMP , case when A.PSTATUS=1 then '����' when A.PSTATUS=2 then '����'  when A.PSTATUS=3 then 'ȡ��' when A.PSTATUS=4 then '����' end as PSTATUS , A.REGTYPEID , A.PATTYPEID
 , case when A.FLAG=1 then '����' when A.FLAG=2 then 'ԤԼ'  when A.FLAG=3 then '�˺�' when A.FLAG=4 then '��ԭ' end as FLAG , A.REEMPNO , A.DROPDATE , A.RECORDDATE ,
 A.PATTYPENAME
 ,  A.PATID
 ,  A.AGE ,  A.PLANPERIOD
 , A.STARTTIME , A.ENDTIME , A.ADDRESS , A.EMPNO ,a.ghmoney,a.kbmoney,a.gbmoney
FROM
   ICARE.VW_OPREGISTER A where a.REGDATE>=to_date('" + firstdate + "','yyyy-MM-dd') and a.REGDATE<=to_date('" + lastdate + "','yyyy-MM-dd') order by REGNO";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtRegister);
            HRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ʷ��ѯ
        [AutoComplete]
        public long m_lngGetHistorRegister(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            dt = null;
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            strSQL = @"select distinct BALANCE_DAT  from T_OPR_PATIENTREGISTER where BALANCE_DAT between to_date('" + startDate + "','yyyy-MM-dd') and to_date('" + endDate + " 23:59:59','yyyy-mm-dd hh24:mi:ss') and BALANCEEMP_CHR='" + checkMan + "' order by BALANCE_DAT";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
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

        #region ���ڲ�ѯ�Һ�(�£�
        /// <summary>
        /// ���ڲ�ѯ�Һ�(�£�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dtRegister">���һ����</param>
        /// <param name="firstdate">��ʼ����</param>
        /// <param name="lastdate">��ֹ����</param>
        /// <param name="EmpID">�Һ�ԱID</param>
        /// <param name="Scope">0 �շѴ� 1 �Һ�Ա</param>
        /// <returns>������</returns>
        [AutoComplete]
        public long m_lngQulRegByDateNew(out DataTable dtRegister, string firstdate, string lastdate, string EmpID, string Scope)
        {
            dtRegister = new DataTable();
            long lngRes = 0;

            string strSQL = @"SELECT a.registerid_chr, a.patientcardid_chr, a.invno_chr, a.registerno_chr, 0 as order_int,c.registertypename_vchr, d.name_vchr,d.sex_chr,CASE
                                      WHEN a.paytype_int = 0
                                         THEN '�ֽ�'
                                      WHEN a.paytype_int = 1
                                         THEN '����'
                                      WHEN a.paytype_int = 2
                                         THEN '֧Ʊ'
                                   END AS paytype,a.registerdate_dat,
                                   CASE
                                      WHEN a.flag_int = 2
                                         THEN    TO_CHAR (a.bespeakdate_dat,
                                                          'yyyy-MM-dd'
                                                         )
                                              || a.datespace
                                      ELSE ''
                                   END AS bespeakdate_dat, f.deptname_vchr,g.lastname_vchr, CASE
                                      WHEN a.balance_dat IS NULL
                                         THEN 'δ����'
                                      WHEN a.balance_dat IS NOT NULL
                                         THEN '����'
                                   END AS pstatus,
                                   CASE
                                      WHEN a.flag_int = 1
                                         THEN '�˹��Һ�'
                                      WHEN a.flag_int = 2
                                         THEN 'ԤԼ'
                                      WHEN a.flag_int = 3
                                         THEN '�˺�'
                                      WHEN a.flag_int = 4
                                         THEN '��ԭ'
                                      WHEN a.flag_int = 5
                                         THEN '�����Һ�'
                                      WHEN a.flag_int = 6
                                         THEN '����ԤԼ�Һ�'
                                   END AS flag, m.empno_chr AS reempno,a.returndate_dat,a.recorddate_dat, b.paytypename_vchr, k.address_vchr,i.empno_chr,

                                   (SELECT payment_mny * discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '001') AS ghmoney,
                                   (SELECT payment_mny * discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '002') AS kbmoney,
                                   (SELECT discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '001') AS ghdiscount,
                                   (SELECT discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '002') AS kbdiscount,
                                                   nvl((SELECT p.payment_mny * discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '003'),0) AS gbmoney,

                                   nvl((SELECT discount_dec
                                      FROM t_opr_patientregdetail p
                                     WHERE p.registerid_chr = a.registerid_chr
                                       AND p.chargeid_chr = '003'),0) AS gbdiscount
                              FROM t_opr_patientregister a,
                                   t_bse_patientpaytype b,
                                   t_bse_registertype c,
                                   t_bse_patientcard e,
                                   t_bse_patientidx d,
                                   t_bse_deptdesc f,
                                   t_bse_employee g,
                                   t_bse_employee i,
                                   t_bse_employee m,
                                   t_bse_deptdesc k
                             WHERE a.paytypeid_chr = b.paytypeid_chr
                               AND a.registertypeid_chr = c.registertypeid_chr
                               AND a.patientcardid_chr = e.patientcardid_chr
                               AND a.patientid_chr = d.patientid_chr
                               AND a.diagdept_chr = f.deptid_chr(+)
                               AND a.diagdoctor_chr = g.empid_chr(+)
                               AND a.registeremp_chr = i.empid_chr(+)
                               AND a.returnemp_chr = m.empid_chr(+)
                               AND a.diagdept_chr = k.deptid_chr(+)                          
                               AND a.registerdate_dat >= TO_DATE ('" + firstdate + @"', 'yyyy-MM-dd')
                               AND a.registerdate_dat <= TO_DATE ('" + lastdate + @"', 'yyyy-MM-dd')";

            if (Scope == "1")
            {
                strSQL += " and (a.registeremp_chr = '" + EmpID + "' or a.returnemp_chr = '" + EmpID + "')";
            }

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtRegister);
                HRPSvc.Dispose();
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

        #region ���ֶβ�ѯ�Һ�(�£�
        /// <summary>
        /// ���ֶβ�ѯ�Һ�(�£�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strArr"></param>
        /// <param name="dt"></param>
        /// <returns>������</returns>
        [AutoComplete]
        public long m_lngQulRegByFieldNew(string[] m_strArr, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.registerid_chr, a.patientcardid_chr, a.invno_chr, a.registerno_chr, j.order_int,c.registertypename_vchr, d.name_vchr,d.sex_chr,CASE
          WHEN a.paytype_int = 0
             THEN '�ֽ�'
          WHEN a.paytype_int = 1
             THEN '����'
          WHEN a.paytype_int = 2
             THEN '֧Ʊ'
       END AS paytype,a.registerdate_dat,
       CASE
          WHEN a.flag_int = 2
             THEN    TO_CHAR (a.bespeakdate_dat,
                              'yyyy-MM-dd'
                             )
                  || a.datespace
          ELSE ''
       END AS bespeakdate_dat, f.deptname_vchr,g.lastname_vchr, CASE
          WHEN a.balance_dat IS NULL
             THEN 'δ����'
          WHEN a.balance_dat IS NOT NULL
             THEN '����'
       END AS pstatus,
       CASE
          WHEN a.flag_int = 1
             THEN '����'
          WHEN a.flag_int = 2
             THEN 'ԤԼ'
          WHEN a.flag_int = 3
             THEN '�˺�'
          WHEN a.flag_int = 4
             THEN '��ԭ'
       END AS flag, m.empno_chr AS reempno,a.returndate_dat,a.recorddate_dat, b.paytypename_vchr, k.address_vchr,i.empno_chr,

       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '001') AS ghmoney,
       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '002') AS kbmoney,
       (SELECT discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '001') AS ghdiscount,
       (SELECT discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '002') AS kbdiscount,
                       nvl((SELECT p.payment_mny * discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '003'),0) AS gbmoney,

       nvl((SELECT discount_dec
          FROM t_opr_patientregdetail p
         WHERE p.registerid_chr = a.registerid_chr
           AND p.chargeid_chr = '003'),0) AS gbdiscount
  FROM t_opr_patientregister a,
       t_bse_patientpaytype b,
       t_bse_registertype c,
       t_bse_patientcard e,
       t_bse_patientidx d,
       t_bse_deptdesc f,
       t_bse_employee g,
       t_bse_employee i,
       t_bse_employee m,
       t_bse_deptdesc k,
       t_opr_waitdiaglist j
 WHERE a.paytypeid_chr = b.paytypeid_chr
   AND a.registertypeid_chr = c.registertypeid_chr
   AND a.patientcardid_chr = e.patientcardid_chr
   AND a.patientid_chr = d.patientid_chr
   AND a.diagdept_chr = f.deptid_chr(+)
   AND a.diagdoctor_chr = g.empid_chr(+)
   AND a.registeremp_chr = i.empid_chr(+)
   AND a.returnemp_chr = m.empid_chr(+)
   AND a.diagdept_chr = k.deptid_chr(+)
   AND a.registerid_chr = j.registerid_chr(+)";
            try
            {

                int m_intStatus = -1;

                switch (m_strArr[0].Trim())
                {
                    case "���ƿ���": strSQL += "and a.patientcardid_chr='" + m_strArr[1].Trim() + "'"; break;
                    case "��Ʊ��": strSQL += "and a.invno_chr='" + m_strArr[1].Trim() + "'"; break;
                    case "��ˮ��": strSQL += "and a.registerno_chr='" + m_strArr[1].Trim() + "'"; break;
                    case "�Һ�����": strSQL += "and c.registertypename_vchr='" + m_strArr[1].Trim() + "'"; break;
                    case "��������": strSQL += "and d.name_vchr='" + m_strArr[1].Trim() + "'"; break;
                    case "�Һ�����": strSQL += "and a.registerdate_dat=to_date('" + m_strArr[1].Trim() + "','yyyy-mm-dd')"; break;
                    case "ԤԼ����": strSQL += "and a.bespeakdate_dat=to_date('" + m_strArr[1].Trim() + "','yyyy-mm-dd') and a.flag_int = 2"; break;

                    case "��������": strSQL += "and f.deptname_vchr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "ҽ������": strSQL += "and g.lastname_vchr='" + m_strArr[1].Trim() + "'"; break;
                    case "����״̬":
                        if (m_strArr[1].Trim() == "δ����")
                        {
                            strSQL += "and a.balance_dat is null";
                        }
                        else if (m_strArr[1].Trim() == "����")
                        {
                            strSQL += "and a.balance_dat is not null";
                        }
                        break;
                    case "�Һ�״̬":
                        if (m_strArr[1].Trim() == "����")
                        {
                            m_intStatus = 1;
                        }
                        else if (m_strArr[1].Trim() == "ԤԼ")
                        {
                            m_intStatus = 2;
                        }
                        else if (m_strArr[1].Trim() == "�˺�")
                        {
                            m_intStatus = 3;
                        }
                        else if (m_strArr[1].Trim() == "��ԭ")
                        {
                            m_intStatus = 4;
                        }
                        strSQL += "and a.flag_int=" + m_intStatus + ""; break;
                    case "�˺��˹���": strSQL += "and m.empno_chr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "�˺�����": strSQL += "and a.returndate_dat=to_date('" + m_strArr[1] + "','yyyy-mm-dd hh24:mi:ss')"; break;
                    case "��¼����": strSQL += "and a.recorddate_dat=to_date('" + m_strArr[1] + "','yyyy-mm-dd hh24:mi:ss')"; break;
                    case "�������": strSQL += "and b.paytypename_vchr='" + m_strArr[1].Trim() + "'"; break;
                    case "�Һ��˹���": strSQL += "and i.empno_chr='" + m_strArr[1].Trim() + "'"; break;
                    default: break;

                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        #region ���Һ�Ա�ڵ����Ƿ�����
        [AutoComplete]
        public long m_lngCheckEnd(string strID, string strDate)
        {
            long lngRes = 0;

            string strSQL = "select REGISTERID_CHR from t_opr_patientregister where BALANCEEMP_CHR='" + strID + "' and BALANCE_DAT>=To_Date('" + strDate + "','yyyy-mm-dd')";
            DataTable bt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref bt);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (bt.Rows.Count > 0)
            {
                return 3;
            }
            return lngRes;

        }



        #endregion

        #region �����ֶβ�ѯ�Һ�
        /// <summary>
        /// �����ֶβ�ѯ�Һŵ�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dtRegister">���ر�</param>
        /// <param name="strFeild">����</param>
        /// <param name="strValue">��ֵ</param>
        /// <param name="Option">�Ƿ�ȷ0-ģ�� 1-��ȷ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQulRegByCol(out DataTable dtRegister, string strFeild, string strValue, string Option)
        {
            dtRegister = new DataTable();
            long lngRes = 0;
            string strSQL;
            if (Option == "1")
            {
                strSQL = @"SELECT distinct
   A.REGID , A.CARDID , A.REGDATE , A.DEPTID , A.DOCID ,A.DEPTNAME , A.DOCNAME
 , A.REGEMP , case when A.PSTATUS=1 then '����' when A.PSTATUS=2 then '����'  when A.PSTATUS=3 then 'ȡ��' when A.PSTATUS=4 then '����' end as PSTATUS , A.REGTYPEID , A.PATTYPEID , A.REGNO
 , case when A.FLAG=1 then '����' when A.FLAG=2 then 'ԤԼ'  when A.FLAG=3 then '�˺�' end as FLAG , A.DROPDATE , A.RECORDDATE ,
case when a.paytype=0 then '�Է�' when a.paytype=1 then '����'  when a.paytype=2 then '֧Ʊ' end as paytype, A.PATTYPENAME
 , A.REGTYPENAME , A.NAME , A.SEX , A.BIRTH , A.PATID
 ,  A.AGE ,  A.PLANPERIOD
 , A.STARTTIME , A.ENDTIME , A.ADDRESS , A.EMPNO , A.ORDERNO
FROM
   ICARE.VW_OPREGISTER A where a." + strFeild + "='" + strValue + "' and a.regdate=to_char(sysdate)";
            }
            else
            {
                strSQL = @"SELECT distinct
   A.REGID , A.CARDID , A.REGDATE , A.DEPTID , A.DOCID ,A.DEPTNAME , A.DOCNAME
 , A.REGEMP , case when A.PSTATUS=1 then '����' when A.PSTATUS=2 then '����'  when A.PSTATUS=3 then 'ȡ��' when A.PSTATUS=4 then '����' end as PSTATUS , A.REGTYPEID , A.PATTYPEID , A.REGNO
 , case when A.FLAG=1 then '����' when A.FLAG=2 then 'ԤԼ'  when A.FLAG=3 then '�˺�' end as FLAG , A.DROPDATE , A.RECORDDATE ,
case when a.paytype=0 then '�Է�' when a.paytype=1 then '����'  when a.paytype=2 then '֧Ʊ' end as paytype, A.PATTYPENAME
 , A.REGTYPENAME , A.NAME , A.SEX , A.BIRTH , A.PATID
 ,  A.AGE ,  A.PLANPERIOD
 , A.STARTTIME , A.ENDTIME , A.ADDRESS , A.EMPNO , A.ORDERNO
FROM
   ICARE.VW_OPREGISTER A where a." + strFeild + "like '%" + strValue + "'%";
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtRegister);

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

        #region ��ѯ�Һŷ���(�Һ�ģ��ʹ��)
        /// <summary>
        /// ��ѯ�Һŷ���
        /// <param name="objPri"></param>
        /// <param name="strRegisterID">�Һ�ID</param>
        /// <param name="dtRegister">���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQulRegDetailByID(string strRegisterID, out DataTable dtRegister)
        {
            dtRegister = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT
   A.REGISTERID_CHR , A.CHARGEID_CHR , A.CHARGENAME_CHR , A.PAYMENT_MNY , A.DISCOUNT_DEC,a.MEMO_VCHR
FROM
   V_OPR_PATIENTREGDETAIL A where REGISTERID_CHR='" + strRegisterID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtRegister);

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


        #region ��ѯ�Һŷ���(��ѯģ��ʹ�ã�
        /// <summary>
        /// ��ѯ�Һŷ���(��ѯģ��ʹ�ã�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegisterID">�Һ�ID</param>
        /// <param name="dtRegister">���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQulRegDetailByIDFind(string strRegisterID, out DataTable dtRegister)
        {
            dtRegister = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT
   A.REGISTERID_CHR , A.CHARGEID_CHR , A.CHARGENAME_CHR , A.PAYMENT_MNY , A.DISCOUNT_DEC,a.MEMO_VCHR
FROM
   V_OPR_PATIENTREGDETAIL A where REGISTERID_CHR='" + strRegisterID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtRegister);
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

        #region �˺�
        /// <summary>
        /// �˺�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegisterID"></param>
        /// <param name="strRturnRegEmpno"></param>
        /// <param name="strReturndate"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelReg(string strRegisterID, string strRturnRegEmpno, string strReturndate, string ConfirmID, out string newID)
        {
            newID = "";
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"select pstatus_int,
       registertypeid_chr,
       paytypeid_chr,
       registerno_chr,
       flag_int,
       returnemp_chr,
       returndate_dat,
       recorddate_dat,
       planperiod_chr,
       patientid_chr,
       paytype_int,
       balance_dat,
       invno_chr,
       balanceemp_chr,
       bespeakdate_dat,
       datespace,
       confirmemp_chr,
       confirmdate_dat,
       takedoctor_chr,
       diagdoctor_chr,
       diagdept_chr,
       registerid_chr,
       patientcardid_chr,
       registerdate_dat,
       registeremp_chr from t_opr_patientregister where REGISTERID_CHR='" + strRegisterID + "'";
            DataTable m_dtgRegister = new DataTable();
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref m_dtgRegister);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"update t_opr_waitdiaglist set PSTATUS_INT=2 where REGISTERID_CHR='" + strRegisterID + "'";
            try
            {
                lngRes = HRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            newID = null;
            HRPSvc.m_lngGenerateNewID("t_opr_patientregister", "REGISTERID_CHR", out newID);
            string DateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (m_dtgRegister.Rows[0]["PSTATUS_INT"].ToString().Trim() == "4")
                m_dtgRegister.Rows[0]["PSTATUS_INT"] = "3";
            if (lngRes > 0 && m_dtgRegister.Rows.Count > 0)
            {
                strSQL = @"insert into t_opr_patientregister(REGISTERID_CHR,PATIENTCARDID_CHR,REGISTERDATE_DAT,DIAGDEPT_CHR,DIAGDOCTOR_CHR,REGISTEREMP_CHR,PSTATUS_INT,REGISTERTYPEID_CHR,PAYTYPEID_CHR,REGISTERNO_CHR,FLAG_INT,RETURNEMP_CHR,RETURNDATE_DAT,RECORDDATE_DAT,PLANPERIOD_CHR,PATIENTID_CHR,PAYTYPE_INT,INVNO_CHR, confirmemp_chr, confirmdate_dat)
                values(?,?,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),?,?,?,?,?,?,?,3,?,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),To_Date(?,'yyyy-mm-dd hh24:mi:ss'),?,?,?,?,?, sysdate)";
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(18, out paramArr);
                    paramArr[0].Value = newID;
                    paramArr[1].Value = m_dtgRegister.Rows[0]["PATIENTCARDID_CHR"].ToString();
                    paramArr[2].Value = m_dtgRegister.Rows[0]["REGISTERDATE_DAT"].ToString();
                    paramArr[3].Value = m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString();
                    paramArr[4].Value = m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString();
                    paramArr[5].Value = m_dtgRegister.Rows[0]["REGISTEREMP_CHR"].ToString();
                    paramArr[6].Value = m_dtgRegister.Rows[0]["PSTATUS_INT"].ToString();
                    paramArr[7].Value = m_dtgRegister.Rows[0]["REGISTERTYPEID_CHR"].ToString();
                    paramArr[8].Value = m_dtgRegister.Rows[0]["PAYTYPEID_CHR"].ToString();
                    paramArr[9].Value = m_dtgRegister.Rows[0]["REGISTERNO_CHR"].ToString();
                    paramArr[10].Value = strRturnRegEmpno;
                    paramArr[11].Value = strReturndate;
                    paramArr[12].Value = DateNow;
                    paramArr[13].Value = m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString();
                    paramArr[14].Value = m_dtgRegister.Rows[0]["PATIENTID_CHR"].ToString();
                    paramArr[15].Value = m_dtgRegister.Rows[0]["PAYTYPE_INT"].ToString();
                    paramArr[16].Value = m_dtgRegister.Rows[0]["INVNO_CHR"].ToString();
                    paramArr[17].Value = ConfirmID;
                    long lngRecordsAffected = -1;
                    lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                DataTable m_dtgRegisterTail = new DataTable();
                strSQL = @"select  registerid_chr, chargeid_chr, payment_mny, discount_dec from t_opr_patientregdetail where REGISTERID_CHR='" + strRegisterID + "'";
                try
                {
                    lngRes = HRPSvc.DoGetDataTable(strSQL, ref m_dtgRegisterTail);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                strSQL = @"insert into t_opr_patientregdetail(REGISTERID_CHR,CHARGEID_CHR,PAYMENT_MNY,DISCOUNT_DEC)values(?,?,?,?)";
                for (int i1 = 0; i1 < m_dtgRegisterTail.Rows.Count; i1++)
                {

                    System.Data.IDataParameter[] paramArr = null;
                    HRPSvc.CreateDatabaseParameter(4, out paramArr);
                    paramArr[0].Value = newID;
                    paramArr[1].Value = m_dtgRegisterTail.Rows[i1]["CHARGEID_CHR"].ToString();
                    paramArr[2].Value = m_dtgRegisterTail.Rows[i1]["PAYMENT_MNY"].ToString();
                    paramArr[3].Value = m_dtgRegisterTail.Rows[i1]["DISCOUNT_DEC"].ToString();


                    try
                    {
                        long lngRecordsAffected = -1;
                        lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                }
                strSQL = @"update t_aid_table_sequence_id set MAX_SEQUENCE_ID_CHR='" + newID + "' where TABLE_NAME_VCHR='t_opr_patientregister'";
                try
                {
                    lngRes = HRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
                return 0;
            strSQL = "P_OPR_MARKREG";
            clsSQLParamDefinitionVO[] objParameter = new clsSQLParamDefinitionVO[5];
            for (int i = 0; i < objParameter.Length; i++)
                objParameter[i] = new clsSQLParamDefinitionVO();
            objParameter[0].objParameter_Value = newID;
            objParameter[1].objParameter_Value = strRturnRegEmpno;
            objParameter[2].objParameter_Value = 0;
            objParameter[3].objParameter_Value = strReturndate;
            objParameter[0].strParameter_Type = "Varchar2";
            objParameter[1].strParameter_Type = "Varchar2";
            objParameter[2].strParameter_Type = "Int32";
            objParameter[3].strParameter_Type = "Varchar2";
            objParameter[4].strParameter_Type = "Int32";
            objParameter[4].strParameter_Direction = "Output";

            try
            {
                lngRes = HRPSvc.lngExecuteParameterProc(strSQL, objParameter);
                string strReturn = objParameter[4].objParameter_Value.ToString().Trim();
                if (strReturn == "-1")
                {
                    return 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�˺ŵ�״̬
        /// <summary>
        /// ��ȡ�˺ŵ�״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strSetStatus"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSetStatus(out int strSetStatus)
        {
            strSetStatus = -1;
            long lngRes = 0;
            string strSQL = @"select SETSTATUS_INT from t_sys_setting where SETID_CHR='0001'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
                strSetStatus = int.Parse(dt.Rows[0][0].ToString());
            else
                strSetStatus = -1;
            return lngRes;

        }
        #endregion

        #region ��ԭ�˺�
        /// <summary>
        /// ��ԭ�˺�
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strRegisterID"></param>
        /// <param name="strResetRegEmpno"></param>
        /// <param name="strResetRegdate"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngResetReg(string strRegisterID, string strResetRegEmpno, string strResetRegdate, out string newID, out int waitNO)
        {
            newID = "";
            waitNO = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"select pstatus_int,
       registertypeid_chr,
       paytypeid_chr,
       registerno_chr,
       flag_int,
       returnemp_chr,
       returndate_dat,
       recorddate_dat,
       planperiod_chr,
       patientid_chr,
       paytype_int,
       balance_dat,
       invno_chr,
       balanceemp_chr,
       bespeakdate_dat,
       datespace,
       confirmemp_chr,
       confirmdate_dat,
       takedoctor_chr,
       diagdoctor_chr,
       diagdept_chr,
       registerid_chr,
       patientcardid_chr,
       registerdate_dat,
       registeremp_chr from t_opr_patientregister where REGISTERID_CHR='" + strRegisterID + "'";
            DataTable m_dtgRegister = new DataTable();
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref m_dtgRegister);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            newID = null;
            HRPSvc.m_lngGenerateNewID("t_opr_patientregister", "REGISTERID_CHR", out newID);
            string DateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (lngRes > 0 && m_dtgRegister.Rows.Count > 0)
            {
                strSQL = @"insert into t_opr_patientregister(REGISTERID_CHR,PATIENTCARDID_CHR,REGISTERDATE_DAT,DIAGDEPT_CHR,DIAGDOCTOR_CHR,REGISTEREMP_CHR,PSTATUS_INT,REGISTERTYPEID_CHR,PAYTYPEID_CHR,REGISTERNO_CHR,FLAG_INT,RECORDDATE_DAT,PLANPERIOD_CHR,PATIENTID_CHR,PAYTYPE_INT,INVNO_CHR,RETURNEMP_CHR,RETURNDATE_DAT)values('" + newID + "','" + m_dtgRegister.Rows[0]["PATIENTCARDID_CHR"].ToString() + "',To_Date('" + m_dtgRegister.Rows[0]["REGISTERDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString() + "','" + m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString() + "','" + m_dtgRegister.Rows[0]["REGISTEREMP_CHR"].ToString() + "',1,'" + m_dtgRegister.Rows[0]["REGISTERTYPEID_CHR"].ToString() + "','" + m_dtgRegister.Rows[0]["PAYTYPEID_CHR"].ToString() + "','" + m_dtgRegister.Rows[0]["REGISTERNO_CHR"].ToString() + "',4," + "To_Date('" + DateNow + "','yyyy-mm-dd hh24:mi:ss'),'" + m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString() + "','" + m_dtgRegister.Rows[0]["PATIENTID_CHR"].ToString() + "'," + m_dtgRegister.Rows[0]["PAYTYPE_INT"].ToString() + ",'" + m_dtgRegister.Rows[0]["INVNO_CHR"].ToString() + "','" + strResetRegEmpno + "',To_Date('" + strResetRegdate + "','yyyy-mm-dd hh24:mi:ss'))";
                try
                {
                    lngRes = HRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                string newWaitID = null;
                newWaitID = HRPSvc.m_strGetNewID("t_opr_waitdiaglist", "WAITDIAGLISTID_CHR", 18);
                if (m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString() == null || m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString() == "")
                {
                    strSQL = @"select COUNT(*) from t_opr_waitdiaglist where PSTATUS_INT=1 and  WAITDIAGDEPT_CHR='" + m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString() + "' and REGISTERDATE_DAT=To_Date('" + strResetRegdate + "','yyyy-MM-dd')";
                    DataTable maxwait = new DataTable();
                    try
                    {
                        lngRes = HRPSvc.DoGetDataTable(strSQL, ref maxwait);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (maxwait.Rows[0][0].ToString() != "")
                    {
                        waitNO = Convert.ToInt32(maxwait.Rows[0][0].ToString()) + 1;
                        strSQL = @"insert into t_opr_waitdiaglist(WAITDIAGLISTID_CHR,REGISTERID_CHR,WAITDIAGDEPT_CHR,ORDER_INT,REGISTERDATE_DAT,PSTATUS_INT,REGISTEROP_VCHR) 
                                   values(?,?,?,?,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),1,?)";

                        System.Data.IDataParameter[] paramArr = null;
                        HRPSvc.CreateDatabaseParameter(6, out paramArr);
                        paramArr[0].Value = newWaitID;
                        paramArr[1].Value = newID;
                        paramArr[2].Value = m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString();
                        paramArr[3].Value = waitNO.ToString();
                        paramArr[4].Value = strResetRegdate;
                        paramArr[5].Value = m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString().Trim();
                        long lngRecordsAffected = -1;

                        try
                        {
                            lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }

                    }
                    else
                    {
                        strSQL = @"insert into t_opr_waitdiaglist(WAITDIAGLISTID_CHR,REGISTERID_CHR,WAITDIAGDEPT_CHR,ORDER_INT,REGISTERDATE_DAT,PSTATUS_INT,REGISTEROP_VCHR) 
                                   values(?,?,?,1,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),1,?)";

                        System.Data.IDataParameter[] paramArr = null;
                        HRPSvc.CreateDatabaseParameter(5, out paramArr);
                        paramArr[0].Value = newWaitID;
                        paramArr[1].Value = newID;
                        paramArr[2].Value = m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString();
                        paramArr[3].Value = strResetRegdate;
                        paramArr[4].Value = m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString().Trim();

                        long lngRecordsAffected = -1;

                        try
                        {
                            lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }

                }
                else
                {
                    strSQL = @"select COUNT(*) from t_opr_waitdiaglist where PSTATUS_INT=1 and  WAITDIAGDEPT_CHR='" + m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString() + "' and WAITDIAGDR_CHR='" + m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString().Trim() + "'  and REGISTERDATE_DAT=to_date('" + strResetRegdate + "','yyyy-MM-dd')";
                    DataTable maxwait = new DataTable();
                    try
                    {
                        lngRes = HRPSvc.DoGetDataTable(strSQL, ref maxwait);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (maxwait.Rows[0][0].ToString() != "")
                    {
                        waitNO = Convert.ToInt32(maxwait.Rows[0][0].ToString()) + 1;
                        strSQL = @"insert into t_opr_waitdiaglist(WAITDIAGLISTID_CHR,REGISTERID_CHR,WAITDIAGDEPT_CHR,ORDER_INT,REGISTERDATE_DAT,PSTATUS_INT,REGISTEROP_VCHR,WAITDIAGDR_CHR) 
                       values(?,?,?,?,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),1,?,?)";

                        System.Data.IDataParameter[] paramArr = null;
                        HRPSvc.CreateDatabaseParameter(7, out paramArr);
                        paramArr[0].Value = newWaitID;
                        paramArr[1].Value = newID;
                        paramArr[2].Value = m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString();
                        paramArr[3].Value = waitNO.ToString();
                        paramArr[4].Value = strResetRegdate;
                        paramArr[5].Value = m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString().Trim();
                        paramArr[6].Value = m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString().Trim();


                        long lngRecordsAffected = -1;

                        try
                        {
                            lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }

                    }
                    else
                    {
                        strSQL = @"insert into t_opr_waitdiaglist(WAITDIAGLISTID_CHR,REGISTERID_CHR,WAITDIAGDEPT_CHR,ORDER_INT,REGISTERDATE_DAT,PSTATUS_INT,REGISTEROP_VCHR,WAITDIAGDR_CHR) 
                        values(?,?,?,1,To_Date(?,'yyyy-mm-dd hh24:mi:ss'),1,?,?)";

                        System.Data.IDataParameter[] paramArr = null;
                        HRPSvc.CreateDatabaseParameter(6, out paramArr);
                        paramArr[0].Value = newWaitID;
                        paramArr[1].Value = newID;
                        paramArr[2].Value = m_dtgRegister.Rows[0]["DIAGDEPT_CHR"].ToString();

                        paramArr[3].Value = strResetRegdate;
                        paramArr[4].Value = m_dtgRegister.Rows[0]["PLANPERIOD_CHR"].ToString().Trim();
                        paramArr[5].Value = m_dtgRegister.Rows[0]["DIAGDOCTOR_CHR"].ToString().Trim();


                        long lngRecordsAffected = -1;

                        try
                        {
                            lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }

                    }


                }
                DataTable m_dtgRegisterTail = new DataTable();
                strSQL = @"select registerid_chr, chargeid_chr, payment_mny, discount_dec from t_opr_patientregdetail where REGISTERID_CHR='" + strRegisterID + "'";
                try
                {
                    lngRes = HRPSvc.DoGetDataTable(strSQL, ref m_dtgRegisterTail);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                for (int i1 = 0; i1 < m_dtgRegisterTail.Rows.Count; i1++)
                {
                    strSQL = @"insert into t_opr_patientregdetail(REGISTERID_CHR,CHARGEID_CHR,PAYMENT_MNY,DISCOUNT_DEC)values('" + newID + "','" + m_dtgRegisterTail.Rows[i1]["CHARGEID_CHR"].ToString() + "','" + m_dtgRegisterTail.Rows[i1]["PAYMENT_MNY"].ToString() + "','" + m_dtgRegisterTail.Rows[i1]["DISCOUNT_DEC"].ToString() + "')";
                    try
                    {
                        lngRes = HRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                }
                strSQL = @"update t_aid_table_sequence_id set MAX_SEQUENCE_ID_CHR='" + newID + "' where TABLE_NAME_VCHR='t_opr_patientregister'";
                try
                {
                    lngRes = HRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            strSQL = "P_OPR_MARKREG";
            clsSQLParamDefinitionVO[] objParameter = new clsSQLParamDefinitionVO[5];
            for (int i = 0; i < objParameter.Length; i++)
                objParameter[i] = new clsSQLParamDefinitionVO();
            objParameter[0].objParameter_Value = strRegisterID;
            objParameter[1].objParameter_Value = strResetRegEmpno;
            objParameter[2].objParameter_Value = 1;
            objParameter[3].objParameter_Value = strResetRegdate;

            objParameter[0].strParameter_Type = "Varchar2";
            objParameter[1].strParameter_Type = "Varchar2";
            objParameter[2].strParameter_Type = "Int32";
            objParameter[3].strParameter_Type = "Varchar2";
            objParameter[4].strParameter_Type = "Int32";
            objParameter[4].strParameter_Direction = "Output";

            try
            {
                lngRes = HRPSvc.lngExecuteParameterProc(strSQL, objParameter);
                string strReturn = objParameter[4].objParameter_Value.ToString().Trim();
                if (strReturn == "-1")
                {
                    return 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ùҺ��Ƿ񿪹����������
        /// <summary>
        /// ���ùҺ��Ƿ񿪹����������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="registerID"></param>
        /// <param name="isReMoney"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckRegister(string registerID, out bool isReMoney, out string outint)
        {
            outint = "-1";
            isReMoney = false;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"select setstatus_int from t_sys_setting where setid_chr='0003'";
            DataTable bt2 = new DataTable();
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref bt2);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            if (bt2.Rows.Count > 0)
            {
                outint = bt2.Rows[0]["setstatus_int"].ToString();
            }
            DataTable bt = new DataTable();
            strSQL = @"select OUTPATRECIPEID_CHR from t_opr_outpatientrecipe where REGISTERID_CHR='" + registerID + "'";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref bt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            if (bt2.Rows.Count == 0 || bt2.Rows[0]["setstatus_int"].ToString() == "0")
            {
                if (bt.Rows.Count > 0)
                {
                    isReMoney = true;
                    return lngRes;
                }
                else
                {
                    isReMoney = false;
                    return lngRes;
                }
            }
            else
            {
                DataTable bt1 = new DataTable();
                for (int i1 = 0; i1 < bt.Rows.Count; i1++)
                {
                    strSQL = @"select INVOICENO_VCHR from t_opr_outpatientrecipeinv where OUTPATRECIPEID_CHR='" + bt.Rows[i1]["OUTPATRECIPEID_CHR"].ToString().Trim() + "'";
                    try
                    {
                        lngRes = HRPSvc.DoGetDataTable(strSQL, ref bt1);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp1 = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                        bool blnRes1 = objLogger1.LogError(objEx);
                    }
                    if (lngRes == 0)
                    {
                        return lngRes;
                    }
                    if (bt1.Rows.Count > 0)
                    {
                        isReMoney = true;
                        return lngRes;
                    }
                }
            }
            isReMoney = false;
            return lngRes;
        }
        #endregion

        #region �˿�ϵͳ
        #region �������ڷ��ؼ�������Ϣ
        /// <summary>
        /// �������ڷ��ؼ�������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCarData(string startDate, string endDate, out DataTable dt, string strCardID, string strName)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.PATIENTCARDID_CHR,a.PATIENTID_CHR,Case when a.STATUS_INT=0 then '���˿�' when a.STATUS_INT<>0 then '����' end as status,b.LASTNAME_VCHR  from t_bse_patientcard a,t_bse_patient b where a.PATIENTID_CHR=b.PATIENTID_CHR";
            if (startDate != null)
            {
                strSQL += @" and ISSUE_DATE between  To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and  To_Date('" + endDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')";
            }
            if (strCardID != null)
            {
                strSQL += @" and a.PATIENTCARDID_CHR='" + strCardID + "'";
            }
            if (strName != null)
            {
                strSQL += @" and b.LASTNAME_VCHR like '" + strName + "'";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �˿�
        [AutoComplete]
        public long m_lngReturnCar(string CarID, string patientNO)
        {
            long lngRes = 0;
            string strSQL = @"update t_bse_patientcard set STATUS_INT=0 where PATIENTCARDID_CHR='" + CarID + "' and PATIENTID_CHR='" + patientNO + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }


            ////��Ϣ����
            //com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update objMsgUpdate = new com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update();
            //try
            //{
            //    lngRes = objMsgUpdate.AddMsg("10003", 2, CarID);
            //}
            //catch (Exception objEx)
            //{
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //    lngRes = -1;
            //}
            //finally
            //{
            //    objMsgUpdate.Dispose();
            //    objMsgUpdate = null;
            //}

            return lngRes;
        }
        #endregion
        #region �޸Ŀ���
        [AutoComplete]
        public long m_lngUpdateCar(string CarID, string patientNO, string strEmpID, string oldCardID)
        {
            long lngRes = 0;

            string strSQL = @"update t_bse_patientcard set PATIENTCARDID_CHR='" + CarID + "'  where PATIENTID_CHR='" + patientNO + "'";
            #region д��ۼ���¼
            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();
            Markvo.m_strOPERATORID_CHR = strEmpID;
            Markvo.m_strTABLESEQID_CHR = "1";
            Markvo.m_strRECORDDETAIL_VCHR = strSQL;
            //recordMark.m_mthAddNewRecord(Markvo);
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }

            ////��Ϣ����
            //com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update objMsgUpdate = new com.digitalwave.iCare.middletier.SynchroTransmitter.clsSvc_MessageOperator_Update();
            //try
            //{
            //    lngRes = objMsgUpdate.AddMsg("10002", 2, patientNO);
            //}
            //catch (Exception objEx)
            //{
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //    lngRes = -1;
            //}
            //finally
            //{
            //    objMsgUpdate.Dispose();
            //    objMsgUpdate = null;
            //}

            strSQL = @"update ar_content set CONTROLID='" + CarID + "'  where CTL_CONTENT='m_txtCardID' and ctl_content='" + oldCardID + "'";
            //			#region д��ۼ���¼
            //			Markvo.m_strOPERATORID_CHR=strEmpID;
            //			Markvo.m_strTABLESEQID_CHR="1";
            //			Markvo.m_strRECORDDETAIL_VCHR=strSQL;
            //			recordMark.m_mthAddNewRecord(Markvo);
            //			#endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"update ar_common_apply  set CARDNO='" + CarID + "'  where CARDNO='" + oldCardID + "'";
            //			#region д��ۼ���¼
            //			Markvo.m_strOPERATORID_CHR=strEmpID;
            //			Markvo.m_strTABLESEQID_CHR="1";
            //			Markvo.m_strRECORDDETAIL_VCHR=strSQL;
            //			recordMark.m_mthAddNewRecord(Markvo);
            //			#endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"update t_opr_pacs_booking_order  set PATIENT_NO_CHR='" + CarID + "'  where PATIENT_NO_CHR='" + oldCardID + "'";
            //			#region д��ۼ���¼
            //			Markvo.m_strOPERATORID_CHR=strEmpID;
            //			Markvo.m_strTABLESEQID_CHR="1";
            //			Markvo.m_strRECORDDETAIL_VCHR=strSQL;
            //			recordMark.m_mthAddNewRecord(Markvo);
            //			#endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @" update t_opr_patientregister set PATIENTCARDID_CHR='" + CarID + "'  where PATIENTID_CHR='" + patientNO + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �ж��µĿ����Ƿ����
        /// <summary>
        /// �ж��µĿ����Ƿ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="CarID"></param>
        /// <returns>����3����</returns>
        [AutoComplete]
        public long m_lngCheckCarID(string CarID)
        {
            long lngRes = 0;
            string strSQL = @"select patientcardid_chr, patientid_chr, issue_date, status_int from  t_bse_patientcard where PATIENTCARDID_CHR='" + CarID + "' and STATUS_INT!=0";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                if (dtResult.Rows.Count > 0 && dtResult.Rows[0]["PATIENTCARDID_CHR"].ToString() == CarID)
                {
                    lngRes = 3;
                    return lngRes;
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion

        #region  �ҺŽ��ʱ���
        /// <summary>
        ///  �ҺŽ��ʱ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPlan"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckOutRep(out DataTable dtSource, string date, out DataTable dtSourceDetail, out string regNo)
        {
            long lngRes = 0;
            dtSource = new DataTable();
            dtSourceDetail = new DataTable();
            regNo = "";

            //			string strSQL = @"select * from V_CHECKOUTREGREPORD order by paytypeid_chr" ;
            string strSQL = @"select * from (select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as regcount,
'��' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat = to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.flag_int = 3 and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a

union all

select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as regcount,
'��' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.flag_int = 3 and pstatus_int<>4 
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat<= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a

union all

select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as regcount,
'֧' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.flag_int = 3 and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat<= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a) f order by f.paytypename_vchr";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            try
            {
                strSQL = "select min(registerno_chr) as minRegNo,max(registerno_chr) as maxRegNo from t_opr_patientregister where registerdate_dat = to_date('" + date + @"','yyyy_MM_dd')";
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtSourceDetail);
                if (dtSourceDetail.Rows.Count > 0)
                {
                    regNo = dtSourceDetail.Rows[0][0].ToString() + "-" + dtSourceDetail.Rows[0][1].ToString();
                    dtSourceDetail.Clear();
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }

            try
            {
                strSQL = @"select a.registerno_chr,d.NAME_VCHR,b.paytypename_vchr,
      case  when a.paytype_int=0 then '�ֽ�'
      when a.paytype_int = 1 then '����'
      when a.paytype_int = 2 then '֧Ʊ'
      else ''
      end as paytype_int,
      (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge,
        c.LASTNAME_VCHR,a.registerdate_dat
from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,T_BSE_PATIENTIDX d

where a.paytypeid_chr = b.paytypeid_chr
      and a.RETURNEMP_CHR = c.empid_chr(+)
      and a.patientid_chr = d.patientid_chr
      and a.registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
	  and a.flag_int = 3";
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtSourceDetail);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �ҺŽ��ʱ����£�
        /// <summary>
        /// �ҺŽ��ʱ����£�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtTolSource">������Ч����</param>
        /// <param name="date">��������</param>
        /// <param name="strempno">������ID</param>
        /// <param name="dtRestoreDetail">�����˺�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngEndReport(out DataTable dtTolSource, string date, string strempno, out DataTable dtRestoreDetail)
        {
            long lngRes = 0;
            dtRestoreDetail = new DataTable();
            dtTolSource = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = "select a.REGISTERID_CHR,a.REGISTERDATE_DAT,a.FLAG_INT,a.REGISTERNO_CHR,a.INVNO_CHR,g.LASTNAME_VCHR as PatientName,f.PAYTYPENAME_VCHR,c.LASTNAME_VCHR,e.DEPTNAME_VCHR," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge, " +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge ,  " +
                "a.RECORDDATE_DAT" +
                " from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,t_bse_patient g,t_bse_patientpaytype f,T_BSE_PATIENTIDX d,T_BSE_DEPTDESC e 	" +
                "where a.paytypeid_chr = b.paytypeid_chr " +
                "and a.DIAGDOCTOR_CHR = c.empid_chr(+) " +
                "and a.patientid_chr = d.patientid_chr " +
                " and a.DIAGDEPT_CHR = e.DEPTID_CHR " +
                " and a.PATIENTID_CHR = g.PATIENTID_CHR " +
                " and a.PAYTYPEID_CHR = f.PAYTYPEID_CHR " +
                " AND (a.REGISTEREMP_CHR='" + strempno + "' and a.REGISTERDATE_DAT<=To_date('" + date + "','yyyy-mm-dd hh24:mi:ss') and flag_int<=2 or a.RETURNEMP_CHR='" + strempno + "' and a.RETURNDATE_DAT<=To_date('" + date + "','yyyy-mm-dd hh24:mi:ss') and flag_int=4)" +
                " and BALANCE_DAT is null ";

            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dtTolSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = "select a.REGISTERID_CHR,a.REGISTERDATE_DAT,a.REGISTERNO_CHR,a.INVNO_CHR,g.LASTNAME_VCHR as PatientName,f.PAYTYPENAME_VCHR,c.LASTNAME_VCHR,e.DEPTNAME_VCHR," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge, " +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge ,  " +
                "a.RECORDDATE_DAT" +
                " from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,t_bse_patient g,t_bse_patientpaytype f,T_BSE_PATIENTIDX d,T_BSE_DEPTDESC e 	" +
                "where a.paytypeid_chr = b.paytypeid_chr " +
                "and a.REGISTEREMP_CHR = c.empid_chr(+) " +
                "and a.patientid_chr = d.patientid_chr " +
                " and a.DIAGDEPT_CHR = e.DEPTID_CHR " +
                " and a.PATIENTID_CHR = g.PATIENTID_CHR " +
                " and a.PAYTYPEID_CHR = f.PAYTYPEID_CHR " +
                " AND a.RETURNEMP_CHR='" + strempno + "'" +
                " and BALANCE_DAT is null " +
                "and  a.FLAG_INT=3 " +
                "and a.returndate_dat<=To_date('" + date + "','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dtRestoreDetail);
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

        #region �Һ���ʷ���ʱ������ݣ��£�
        /// <summary>
        /// �Һ���ʷ���ʱ������ݣ��£�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtTolSource">������Ч����</param>
        /// <param name="date">��������</param>
        /// <param name="strempno">������ID</param>
        /// <param name="dtRestoreDetail">�����˺�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngHistoryReport(out DataTable dtTolSource, string date, string strempno, out DataTable dtRestoreDetail)
        {
            long lngRes = 0;
            dtRestoreDetail = new DataTable();
            dtTolSource = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = "select a.REGISTERID_CHR,a.REGISTERDATE_DAT,a.FLAG_INT,a.REGISTERNO_CHR,a.INVNO_CHR,g.LASTNAME_VCHR as PatientName,f.PAYTYPENAME_VCHR,c.LASTNAME_VCHR,e.DEPTNAME_VCHR," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge, " +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge ,  " +
                "a.RECORDDATE_DAT, to_char(a.RECORDDATE_DAT, 'yyyy-mm-dd hh24:mi:ss') as invodate " +
                " from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,t_bse_patient g,t_bse_patientpaytype f,T_BSE_PATIENTIDX d,T_BSE_DEPTDESC e 	" +
                "where a.paytypeid_chr = b.paytypeid_chr " +
                "and a.DIAGDOCTOR_CHR = c.empid_chr(+) " +
                "and a.patientid_chr = d.patientid_chr " +
                " and a.DIAGDEPT_CHR = e.DEPTID_CHR " +
                " and a.PATIENTID_CHR = g.PATIENTID_CHR " +
                " and a.PAYTYPEID_CHR = f.PAYTYPEID_CHR " +
                "and  FLAG_INT<>3 " +
                " AND a.BALANCEEMP_CHR='" + strempno + "'" +
                "and a.BALANCE_DAT=To_date('" + date + "','yyyy-mm-dd hh24:mi:ss')  order by a.INVNO_CHR";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dtTolSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = "select a.REGISTERID_CHR,a.REGISTERDATE_DAT,a.REGISTERNO_CHR,a.INVNO_CHR,g.LASTNAME_VCHR as PatientName,f.PAYTYPENAME_VCHR,c.LASTNAME_VCHR,e.DEPTNAME_VCHR," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge, " +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge," +
                "(select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d " +
                "where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge ,  " +
                "a.RECORDDATE_DAT" +
                " from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,t_bse_patient g,t_bse_patientpaytype f,T_BSE_PATIENTIDX d,T_BSE_DEPTDESC e 	" +
                "where a.paytypeid_chr = b.paytypeid_chr " +
                "and a.REGISTEREMP_CHR = c.empid_chr(+) " +
                "and a.patientid_chr = d.patientid_chr " +
                " and a.DIAGDEPT_CHR = e.DEPTID_CHR " +
                " and a.PATIENTID_CHR = g.PATIENTID_CHR " +
                " and a.PAYTYPEID_CHR = f.PAYTYPEID_CHR " +
                " AND a.BALANCEEMP_CHR='" + strempno + "'" +
                "and  a.FLAG_INT=3 " +
                "and a.BALANCE_DAT=To_date('" + date + "','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dtRestoreDetail);
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
        /// ��ȡ���е��շ�Ա����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtCheckMan"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckMan(out DataTable dtCheckMan)
        {
            dtCheckMan = new DataTable();
            long lngRes = 0;
            string strSQL = @"select distinct a.BALANCEEMP_CHR,b.lastname_vchr from T_OPR_PATIENTREGISTER a,t_bse_employee b where a.BALANCEEMP_CHR=b.empid_chr and a.BALANCEEMP_CHR is not null";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckMan);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ��ȡ�Һ����͵�״̬
        /// <summary>
        /// ��ȡ�Һ����͵�״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strTypeID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatTypeFLAG(string strTypeID, out int intType)
        {
            intType = -1;
            long lngRes = 0;
            string strSQL = @"select INTERNALFLAG_INT from t_bse_patientpaytype where PAYTYPEID_CHR='" + strTypeID + "'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            if (dt.Rows.Count > 0)
                intType = int.Parse(dt.Rows[0][0].ToString());
            else
                intType = -1;
            return lngRes;

        }
        #endregion

        #region ��鷢Ʊ��
        /// <summary>
        /// ��鷢Ʊ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strNO"></param>
        /// <param name="dt">����ռ�ø÷�Ʊ�ŵ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckNO(string strNO, out DataTable dt)
        {
            long lngRes = 0;
            dt = null;

            string strSQL = @"select a.REGISTERID_CHR,
                                     a.REGISTERDATE_DAT,
                                     b.EMPNO_CHR 
                                from t_opr_patientregister a,
                                     t_bse_employee b 
                               where a.INVNO_CHR = '" + strNO.Trim() + @"' 
                                 and a.REGISTEREMP_CHR = b.EMPID_CHR 
                            union all
                              select '' as registerid_chr,
                                     c.printdate_dat as registerdate_dat,
                                     d.empno_chr 
                                from t_opr_invoicerepeatprint c, 
                                     t_bse_employee d
                               where c.type_chr = '2' 
                                 and c.repprninvono_vchr = '" + strNO.Trim() + @"' 
                                 and c.printemp_chr = d.empid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);

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

        #region ��ȡָ���Һ�Ա�ķ�Ʊ��Ϣ(δ�ᡢ�ѽ�)
        /// <summary>
        /// ��ȡָ���Һ�Ա�ķ�Ʊ��Ϣ(δ�ᡢ�ѽ�)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="BalDate"></param>
        /// <param name="Flag">0 δ�� 1 �ѽ�</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterInvoInfo(string EmpID, string BalDate, int Flag, out DataTable dt)
        {
            long lngRes = 0;

            dt = new DataTable();

            string SQL = "";

            if (Flag == 0)
            {
                SQL = @"select a.invno_chr as invono, a.flag_int as flag 
                          from t_opr_patientregister a
                         where (a.flag_int = 1 or a.flag_int = 2) 
                           and a.balance_dat is null 
                           and a.registeremp_chr = '" + EmpID + @"' 
                           and a.registerdate_dat <= to_date('" + BalDate + @"', 'yyyy-mm-dd hh24:mi:ss') 
 
                        union all 
                        
                        select a.invno_chr as invono, a.flag_int as flag 
                          from t_opr_patientregister a
                         where (a.flag_int = 3 or a.flag_int = 4) 
                           and a.balance_dat is null 
                           and a.returnemp_chr = '" + EmpID + @"' 
                           and a.returndate_dat <= to_date('" + BalDate + @"', 'yyyy-mm-dd hh24:mi:ss') 

                        union all 

                        select a.repprninvono_vchr, 9 as flag  
                          from t_opr_invoicerepeatprint a,
                               t_opr_patientregister b 
                         where a.seqid_chr = b.registerid_chr 
                           and a.type_chr = '2'  
                           and b.balance_dat is null 
                           and a.printemp_chr = '" + EmpID + @"' 
                           and b.registerdate_dat <= to_date('" + BalDate + "', 'yyyy-mm-dd hh24:mi:ss')";
            }
            else if (Flag == 1)
            {
                SQL = @"select a.invno_chr as invono, a.flag_int as flag 
                          from t_opr_patientregister a
                         where a.balanceemp_chr = '" + EmpID + @"' 
                           and a.balance_dat = to_date('" + BalDate + @"', 'yyyy-mm-dd hh24:mi:ss')                        

                        union all 

                        select a.repprninvono_vchr, 9 as flag  
                          from t_opr_invoicerepeatprint a,
                               t_opr_patientregister b 
                         where a.seqid_chr = b.registerid_chr 
                           and a.type_chr = '2' 
                           and b.balance_dat is not null 
                           and a.printemp_chr = '" + EmpID + @"' 
                           and b.balance_dat = to_date('" + BalDate + "', 'yyyy-mm-dd hh24:mi:ss')";
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
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

        #region  �Һ�Ա�ҺŽ��ʱ���
        /// <summary>
        ///  �Һ�Ա�ҺŽ��ʱ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPlan"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckOutRegP(out DataTable dtSource, string date, string strempno, out DataTable dtSourceDetail, out string regNo)
        {
            long lngRes = 0;
            dtSource = new DataTable();
            dtSourceDetail = new DataTable();
            regNo = "";

            //			string strSQL = @"select * from V_CHECKOUTREGREPORD order by paytypeid_chr" ;select * from (
            string strSQL = @"select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + strempno + @"'  and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as regcount,
'��' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and  
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + strempno + @"' and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + strempno + @"' and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + strempno + @"' and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + strempno + @"' and pstatus_int<>4 and FLAG_INT<>3
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.flag_int = 3 and b.RETURNEMP_CHR='" + strempno + @"' and pstatus_int<>4
 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + strempno + @"' and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + strempno + @"' and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + strempno + @"' and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + strempno + @"' and pstatus_int<>4
and b.flag_int = 3 and registerdate_dat <= to_date('" + date + @"','yyyy_MM_dd')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a";

            //union all
            //
            //select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
            //b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')) as regcount,
            //'֧' as paytype,nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,
            //
            //(select count(*) from t_opr_patientregister b where
            // b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.flag_int = 3 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')) as rregcount,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney
            //
            //from t_bse_patientpaytype a
            //
            //union all
            //
            //select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
            //b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')) as regcount,
            //'��' as paytype,nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='"+strempno+@"' and pstatus_int<>4 and FLAG_INT<>3
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,
            //
            //(select count(*) from t_opr_patientregister b where
            // b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.flag_int = 3 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            // and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')) as rregcount,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,
            //
            //nvl((
            //select sum(c.payment_mny*c.discount_dec) as money from
            //t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
            //c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='"+strempno+@"' and pstatus_int<>4
            //and b.flag_int = 3 and registerdate_dat <= to_date('"+date+@"','yyyy_MM_dd')
            //group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney
            //
            //from t_bse_patientpaytype a) f order by f.paytypename_vchr";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            regNo = "";
            try
            {
                strSQL = "select min(registerno_chr) as minRegNo,max(registerno_chr) as maxRegNo from t_opr_patientregister where registerdate_dat <= to_date(?,'yyyy_MM_dd')  and REGISTEREMP_CHR=? and pstatus_int<>4";


                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = date;
                paramArr[1].Value = strempno;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtSourceDetail, paramArr);

                if (dtSourceDetail.Rows.Count > 0)
                {
                    regNo = dtSourceDetail.Rows[0][0].ToString() + "-" + dtSourceDetail.Rows[0][1].ToString();
                    dtSourceDetail.Clear();
                }
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }

            try
            {
                strSQL = @"select a.registerno_chr,d.NAME_VCHR,b.paytypename_vchr,
      case  when a.paytype_int=0 then '�ֽ�'
      when a.paytype_int = 1 then '����'
      when a.paytype_int = 2 then '֧Ʊ'
      else ''
      end as paytype_int,
      (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge,
        c.LASTNAME_VCHR,a.registerdate_dat
from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,T_BSE_PATIENTIDX d

where a.paytypeid_chr = b.paytypeid_chr
      and a.RETURNEMP_CHR = c.empid_chr(+)
      and a.patientid_chr = d.patientid_chr(+)
      and a.registerdate_dat <= to_date(?,'yyyy_MM_dd') and a.pstatus_int<>4
	  and a.flag_int = 3 and a.RETURNEMP_CHR=?";


                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = date;
                paramArr[1].Value = strempno;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtSourceDetail, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �ҺŽ���(��)
        /// <summary>
        /// �ҺŽ���(��)
        /// </summary>
        /// <param name="OperID">�տ�ԱID</param>
        /// <param name="CheckDate">����ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckOut(string OperID, out string CheckDate)
        {
            long lngRes = 0;
            string strSQl = "";
            CheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;

                strSQl = @"update t_opr_patientregister 
                              set balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), 
                                  balanceemp_chr = ?
                            where balance_dat is null 
                              and (registeremp_chr = ? or returnemp_chr = ?)";

                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = CheckDate;
                paramArr[1].Value = OperID;
                paramArr[2].Value = OperID;
                paramArr[3].Value = OperID;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQl, ref lngRecordsAffected, paramArr);
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

        #region �ҺŽ���(��,ͣ��)
        /// <summary>
        /// �ҺŽ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="checkoutdate">��������</param>
        /// <param name="checkoutempid">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckOut(string checkoutdate, string checkoutempid, DataTable dtTolSource, DataTable dtRestoreDetail1)
        {
            long lngRes = 0;
            string strSQl = "";
            if (dtTolSource.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtTolSource.Rows.Count; i1++)
                {
                    strSQl = @"update T_OPR_PATIENTREGISTER set BALANCE_DAT=To_Date(?,'yyyy-mm-dd hh24:mi:ss'),BALANCEEMP_CHR=? where REGISTERID_CHR=?";
                    try
                    {
                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                        paramArr[0].Value = checkoutdate;
                        paramArr[1].Value = checkoutempid;
                        paramArr[2].Value = dtTolSource.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        long lngRecordsAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQl, ref lngRecordsAffected, paramArr);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                }
            }
            //			string strSQL="update T_OPR_PATIENTREGISTER  set PSTATUS_INT = 4,BALANCE_DAT=To_Date('"+checkoutdate+"','yyyy-mm-dd hh24:mi:ss')"+ "  where REGISTEREMP_CHR='"+checkoutempid+"'  and PSTATUS_INT<>4 and FLAG_INT<>3";

            //			strSQL="update T_OPR_PATIENTREGISTER  set PSTATUS_INT = 4,BALANCE_DAT=To_Date('"+checkoutdate+"','yyyy-mm-dd hh24:mi:ss')"+ "  where RETURNEMP_CHR='"+checkoutempid+"'  and PSTATUS_INT<>4 and FLAG_INT=3";

            if (dtRestoreDetail1.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtRestoreDetail1.Rows.Count; i1++)
                {
                    strSQl = @"update T_OPR_PATIENTREGISTER set BALANCE_DAT=To_Date(?,'yyyy-mm-dd hh24:mi:ss'),BALANCEEMP_CHR=? where REGISTERID_CHR=? ";
                    try
                    {
                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                        paramArr[0].Value = checkoutdate;
                        paramArr[1].Value = checkoutempid;
                        paramArr[2].Value = dtRestoreDetail1.Rows[i1]["REGISTERID_CHR"].ToString().Trim();
                        long lngRecordsAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQl, ref lngRecordsAffected, paramArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                }
            }

            return lngRes;
        }
        #endregion

        #region ��ȡĬ�ϵĴ�ӡ״̬
        /// <summary>
        /// ��ȡĬ�ϵĴ�ӡ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="STATUSINT">0,Ĭ�ϴ�ӡ��1,����ӡ�� -2��û������Ĭ��ֵ </param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPrint(string strID, out int STATUSINT)
        {
            STATUSINT = -2;
            long lngRes = 0;
            string strSQL = @"select SETSTATUS_INT from t_sys_setting where SETID_CHR='" + strID + "'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            if (lngRes == 1 && dt.Rows.Count > 0)
                STATUSINT = int.Parse(dt.Rows[0][0].ToString());
            return lngRes;
        }
        #endregion

        #region  �Һ�Ա�ҺŽ�����ʷ����
        /// <summary>
        ///  �Һ�Ա�ҺŽ�����ʷ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objPlan"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckOutH(out DataTable dtSource, string CHECKOUTDATE, string CHECKOUTREGID, out DataTable dtSourceDetail)
        {
            long lngRes = 0;
            dtSource = new DataTable();
            dtSourceDetail = new DataTable();

            //			string strSQL = @"select * from V_CHECKOUTREGREPORD order by paytypeid_chr" ;
            string strSQL = @"select * from (select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"'  and pstatus_int=4 and FLAG_INT<>3
 and REGISTERID_CHR in(select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"') ) as regcount,
'��' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.flag_int = 3 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR  in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 0 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a

union all

select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')) as regcount,
'��' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.flag_int = 3 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 1 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a

union all

select a.paytypename_vchr,(select count(*) from t_opr_patientregister b where
b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and flag_int <>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')) as regcount,
'֧' as paytype,nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as regmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as curemoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as materialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.REGISTEREMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 and FLAG_INT<>3
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and REGISTEREMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as cardmoney,

(select count(*) from t_opr_patientregister b where
 b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.flag_int = 3 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')) as rregcount,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '001' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4 
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rregmoney,
nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '002' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcuremoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '003' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rmaterialmoney,

nvl((
select sum(c.payment_mny*c.discount_dec) as money from
t_opr_patientregister b,t_opr_patientregdetail c where b.registerid_chr=c.registerid_chr and
c.chargeid_chr = '004' and b.paytypeid_chr = a.paytypeid_chr and b.paytype_int = 2 and b.RETURNEMP_CHR='" + CHECKOUTREGID + @"' and pstatus_int=4
and b.flag_int = 3 and b.REGISTERID_CHR in (select REGISTERID_CHR from t_opr_patientregister where BALANCE_DAT=To_Date('" + CHECKOUTDATE + "','yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR='" + CHECKOUTREGID + @"')
group by b.paytypeid_chr,b.paytypeid_chr),0) as rcardmoney

from t_bse_patientpaytype a) f order by f.paytypename_vchr";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtSource);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            try
            {
                strSQL = @"select a.registerno_chr,d.NAME_VCHR,b.paytypename_vchr,
      case  when a.paytype_int=0 then '�ֽ�'
      when a.paytype_int = 1 then '����'
      when a.paytype_int = 2 then '֧Ʊ'
      else ''
      end as paytype_int,
      (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='001') as rcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='002') as dcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='003') as gcharge,
        (select PAYMENT_MNY*DISCOUNT_DEC from t_opr_patientregdetail d
        where d.registerid_chr = a.registerid_chr and d.chargeid_chr='004') as ccharge,
        c.LASTNAME_VCHR,a.registerdate_dat
from t_opr_patientregister a,t_bse_patientpaytype b,T_BSE_EMPLOYEE c,T_BSE_PATIENTIDX d

where a.paytypeid_chr = b.paytypeid_chr
      and a.RETURNEMP_CHR = c.empid_chr(+)
      and a.patientid_chr = d.patientid_chr(+)
      and registerno_chr in (select REGISTERNO_CHR from t_opr_patientregister where BALANCE_DAT=To_Date(?,'yyyy-mm-dd hh24:mi:ss') and RETURNEMP_CHR=? ) 
	  and a.flag_int = 3 and a.pstatus_int=4 and a.RETURNEMP_CHR=? ";

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = CHECKOUTDATE;
                paramArr[1].Value = CHECKOUTREGID;
                paramArr[2].Value = CHECKOUTREGID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtSourceDetail, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ��ȡ�ѽ��ʼ�¼����
        [AutoComplete]
        public long m_lngGetCheckOutHistoryData(string m_strFirstDate, string m_strEndDate, string BALANCEEMP, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            dtCheckOut = null;
            dtPayType = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr from t_bse_chargeitemextype  where flag_int='1' order by sortcode_int";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.recorddate_dat, a.opremp_chr, a.seqid_chr,
         a.status_int, a.balanceemp_chr, a.paytype_int, a.paytypeid_chr,
         b.tolfee_mny, b.itemcatid_chr, c.internalflag_int, a.acctsum_mny,
         a.sbsum_mny, a.totalsum_mny
     FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
     WHERE a.invoiceno_vchr = b.invoiceno_vchr
     AND a.balanceflag_int = 1
     AND a.seqid_chr = b.seqid_chr
     AND a.paytypeid_chr = c.paytypeid_chr(+)
     AND a.balance_dat between TO_DATE (?, 'yyyy-mm-dd HH24:mi:ss') and TO_DATE (?, 'yyyy-mm-dd HH24:mi:ss') 
     AND balanceemp_chr = ?
     ORDER BY a.invoiceno_vchr, a.seqid_chr";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strFirstDate + " 00:00:00";
                objLisAddItemRefArr[1].Value = m_strEndDate + " 23:59:59";
                objLisAddItemRefArr[2].Value = BALANCEEMP;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, objLisAddItemRefArr);
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
        #region �շѽ����ձ���
        /// <summary>
        /// �շѽ����ձ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDate"></param>
        /// <param name="dtPayType"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPayTypeAndCheckOutData(string OPREMPID, string strDate, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            dtPayType = null;
            dtCheckOut = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.recorddate_dat, a.opremp_chr, a.status_int,
         a.seqid_chr, a.balanceemp_chr, a.paytypeid_chr,
         b.tolfee_mny, b.itemcatid_chr, c.internalflag_int, a.acctsum_mny,
         a.sbsum_mny, a.totalsum_mny,a.paytype_int
    FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
   WHERE a.invoiceno_vchr = b.invoiceno_vchr
     AND a.seqid_chr = b.seqid_chr
     AND a.balanceflag_int = 0
     AND a.paytypeid_chr = c.paytypeid_chr
     AND a.recorddate_dat <
                       TO_DATE (?, 'yyyy-mm-dd HH24:mi:ss')
     AND a.recordemp_chr =?    
ORDER BY a.invoiceno_vchr, a.seqid_chr";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = strDate;
                paramArr[1].Value = OPREMPID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region ����(��,ͣ��)
        [AutoComplete]
        public long m_lngCheckData(DataTable dt, string CheckName, string CheckDate)
        {
            long lngRes = 0;
            string strSQL = "";
            if (dt.Rows.Count > 0)
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    strSQL = @"update t_opr_outpatientrecipeinv set BALANCEEMP_CHR=?,BALANCE_DAT=to_date(?,'yyyy-mm-dd HH24:mi:ss'),BALANCEFLAG_INT=1 where INVOICENO_VCHR=? and SEQID_CHR=? and RECORDEMP_CHR=? ";
                    try
                    {

                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                        paramArr[0].Value = CheckName;
                        paramArr[1].Value = CheckDate;
                        paramArr[2].Value = dt.Rows[i1]["INVOICENO_VCHR"].ToString();
                        paramArr[3].Value = dt.Rows[i1]["SEQID_CHR"].ToString();
                        paramArr[4].Value = CheckName;
                        long lngRecordsAffected = -1;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }

            }
            return lngRes;
        }
        #endregion

        #region ����(��)
        /// <summary>
        /// ����(��)
        /// </summary>
        /// <param name="OperID">�տ�ԱID</param>
        /// <param name="CheckDate">�ս�ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckData(string OperID, out string CheckDate)
        {
            long lngRes = 0;
            string strSQL = "";

            CheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            strSQL = @"update t_opr_outpatientrecipeinv 
                          set balanceemp_chr = ?, 
                              balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), 
                              balanceflag_int = 1 
                        where balanceflag_int = 0
                          and recordemp_chr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = OperID;
                paramArr[1].Value = CheckDate;
                paramArr[2].Value = OperID;

                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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

        #region ��ʷ��ѯ
        [AutoComplete]
        public long m_lngGetHistor(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            dt = null;
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            strSQL = @"select distinct BALANCE_DAT from t_opr_outpatientrecipeinv where BALANCE_DAT between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date(?,'yyyy-mm-dd hh24:mi:ss') and BALANCEEMP_CHR=? and BALANCEFLAG_INT=1 order by BALANCE_DAT";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = startDate + " 00:00:00";
                paramArr[1].Value = endDate + " 23:59:59";
                paramArr[2].Value = checkMan;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }

        #region ������¼����
        [AutoComplete]
        public long m_lngGetPayTypeAndCheckOutDatahistory(string strDate, string BALANCEEMP, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            dtPayType = null;
            dtCheckOut = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT,a.OPREMP_CHR, a.SEQID_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,b.TOLFEE_MNY,b.ITEMCATID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.BALANCEFLAG_INT=1 and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR(+) and a.BALANCE_DAT=To_Date('" + strDate + "','yyyy-mm-dd HH24:mi:ss') and BALANCEEMP_CHR='" + BALANCEEMP + "'  order by a.INVOICENO_VCHR,a.SEQID_CHR";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
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

        #region ��ȡ�շ�Ա�ĵ�ǰδ���ʵ��շѼ�¼
        [AutoComplete]
        public long m_lngGetOneDayData(string OPREMPID, string strDate, out DataTable dtCheckOut)
        {
            dtCheckOut = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.INVOICENO_VCHR, a.SEQID_CHR,a.RECORDDATE_DAT,a.OPREMP_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,t_bse_patientpaytype c
                    where   a.BALANCEFLAG_INT=0 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and a.RECORDDATE_DAT<= To_Date('" + strDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and OPREMP_CHR='" + OPREMPID + "'  order by a.INVOICENO_VCHR";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
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

        #region ������н���Ա����
        [AutoComplete]
        public long m_lngGetCheckMan(out DataTable dtEmpAll, string strINTERNALFLAG)
        {
            dtEmpAll = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (strINTERNALFLAG == "-1")
            {
                strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1 and a.BALANCEEMP_CHR=b.EMPID_CHR ";
            }
            else
            {
                strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1  and a.BALANCEEMP_CHR=b.EMPID_CHR and a.INTERNALFLAG_INT=" + strINTERNALFLAG;
            }

            string Sub = string.Format(" and a.recorddate_dat >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')", DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd HH:mm:ss"));
            strSQL += Sub;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtEmpAll);
                //lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmpAll);
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

        #region �շ��ղ�ѯ����

        [AutoComplete]
        public long m_lngGetPayTypeAndCheckOutBetWeenDay(string strDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtEmp, string strINTERNALFLAG, string strCheckManName)
        {
            dtEmp = null;
            dtPayType = null;
            dtCheckOut = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            string SqlWhere = "";
            if (strCheckManName != null)
            {
                SqlWhere = @" and a.BALANCEEMP_CHR='" + strCheckManName + "'";
            }
            if (strINTERNALFLAG == "-1")
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT,a.OPREMP_CHR, a.SEQID_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,d.PAYTYPE_INT,a.PAYTYPEID_CHR,b.TOLFEE_MNY,b.ITEMCATID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c,t_opr_payment d
                    where a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR and  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR(+)  AND d.chargeno_vchr = a.seqid_chr and a.BALANCE_DAT between To_Date('" + strDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + strDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + SqlWhere + " order by a.INVOICENO_VCHR,a.SEQID_CHR";
            }
            else
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT,a.OPREMP_CHR, a.SEQID_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,d.PAYTYPE_INT,a.PAYTYPEID_CHR,b.TOLFEE_MNY,b.ITEMCATID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c,t_opr_payment d
                    where a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR and  a.BALANCEFLAG_INT=1 AND d.chargeno_vchr = a.seqid_chr and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR(+) and a.BALANCE_DAT between To_Date('" + strDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + strDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + SqlWhere + " and a.INTERNALFLAG_INT=" + strINTERNALFLAG + " order by a.INVOICENO_VCHR,a.SEQID_CHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (strCheckManName == null)
            {
                if (strINTERNALFLAG == "-1")
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1 and a.BALANCEEMP_CHR=b.EMPID_CHR and a.BALANCE_DAT between To_Date('" + strDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + strDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')";
                }
                else
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1  and a.BALANCEEMP_CHR=b.EMPID_CHR and a.BALANCE_DAT between To_Date('" + strDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + strDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and a.INTERNALFLAG_INT=" + strINTERNALFLAG;
                }
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmp);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }


            return lngRes;
        }

        #endregion

        #region �շ��²�ѯ����

        [AutoComplete]
        public long m_lngGetPayTypeAndCheckOutBetWeenDate(string startDate, string EndDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtEmp, string strINTERNALFLAG, string strCheckManName)
        {
            dtEmp = null;
            dtPayType = null;
            dtCheckOut = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            string SqlWhere = "";
            if (strCheckManName != null)
            {
                SqlWhere = @" and a.BALANCEEMP_CHR='" + strCheckManName + "'";
            }
            if (strINTERNALFLAG == "-1")
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT, a.SEQID_CHR,a.OPREMP_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,b.TOLFEE_MNY,b.ITEMCATID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR and  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR(+) and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') order by a.INVOICENO_VCHR,a.SEQID_CHR";
            }
            else
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT, a.SEQID_CHR,a.OPREMP_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,b.TOLFEE_MNY,b.ITEMCATID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR and  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR(+) and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and a.INTERNALFLAG_INT=" + strINTERNALFLAG + "  order by a.INVOICENO_VCHR,a.SEQID_CHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (strCheckManName == null)
            {
                if (strINTERNALFLAG == "-1")
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1 and BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and a.BALANCEEMP_CHR=b.EMPID_CHR";
                }
                else
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1 and BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and a.BALANCEEMP_CHR=b.EMPID_CHR and a.INTERNALFLAG_INT=" + strINTERNALFLAG;
                }
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmp);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }

        #endregion

        #region ����������ͳ�Ʊ���

        #region ����������ͳ�Ʊ���
        /// <summary>
        /// ����������ͳ�Ʊ���
        /// </summary>
        /// <param name="p_objPrincipal">��ʼʱ��</param>
        /// <param name="startDate">��ʼʱ��</param>
        /// <param name="EndDate">����ʱ��</param>
        /// <param name="dtPayType">�����շ�����</param>
        /// <param name="dtCheckOut">�����շ�����</param>
        /// <param name="dtCheckOutDe">�����շ���ϸ���ݱ�</param>
        /// <param name="dtEmp">�����շ�Ա�б�</param>
        /// <param name="isOne">�����շ�ԱID��ALLȫ��ͳ��</param>
        /// <param name="isfull">ָ�Ƿ���������������ݷֿ�ͳ��-1-ͳ���������������(ҽ������0-����ͳ����������(ҽ������1-����ͳ�ƺ������(ҽ����.2-ͳ��������������ݣ����ѣ���3-����ͳ���������ݣ����ѣ���4-����ͳ�ƺ�����ݣ����ѣ���5-ͳ��������������ݣ��Էѣ���6-����ͳ���������ݣ��Էѣ���7-����ͳ�ƺ�����ݣ��Էѣ���8-ͳ��������������ݣ���������9-����ͳ���������ݣ���������10-����ͳ�ƺ�����ݣ�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetIatrical(string startDate, string EndDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtCheckOutDe, out DataTable dtEmp, string isOne, string isfull)
        {
            dtPayType = null;
            dtCheckOut = null;
            dtCheckOutDe = null;
            dtEmp = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            string strcondition = " ";
            string strFLAG = "";
            if (isOne == "All")
            {
                switch (isfull)
                {
                    case "-1":
                        strcondition = @" ";
                        strFLAG = " and c.INTERNALFLAG_INT=2";
                        break;
                    case "0":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        strFLAG = " and c.INTERNALFLAG_INT=2";
                        break;
                    case "1":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        strFLAG = " and c.INTERNALFLAG_INT=2";
                        break;

                    case "2":
                        strcondition = @" ";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "3":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "4":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;

                    case "11":
                        strcondition = @" ";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "12":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "13":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;

                    case "5":
                        strcondition = @" ";
                        break;
                    case "6":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        break;
                    case "7":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        break;

                    case "8":
                        strcondition = @" ";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;
                    case "9":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;
                    case "10":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;


                    case "14":
                        strcondition = @" ";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                    case "15":
                        strcondition = @" and a.INTERNALFLAG_INT=1";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                    case "16":
                        strcondition = @" and a.INTERNALFLAG_INT=0";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                }
            }
            else
            {
                switch (isfull)
                {
                    case "-1":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=2";
                        break;
                    case "0":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = "2";
                        break;
                    case "1":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=2";
                        break;

                    case "2":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "3":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "4":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;

                    case "11":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "12":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;
                    case "13":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT=1";
                        break;

                    case "5":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        break;
                    case "6":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        break;
                    case "7":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        break;

                    case "8":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;
                    case "9":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;
                    case "10":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and c.INTERNALFLAG_INT>2";
                        break;


                    case "14":
                        strcondition = @" and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                    case "15":
                        strcondition = @" and a.INTERNALFLAG_INT=1 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                    case "16":
                        strcondition = @" and a.INTERNALFLAG_INT=0 and a.BALANCEEMP_CHR='" + isOne + "'";
                        strFLAG = " and (a.PAYTYPE_INT=1 or c.INTERNALFLAG_INT=2)";
                        break;
                }
            }
            if (isfull == "5" || isfull == "6" || isfull == "7")
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT, a.SEQID_CHR,a.OPREMP_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,c.INTERNALFLAG_INT,a.SBSUM_MNY,case when c.INTERNALFLAG_INT!=0 then a.SBSUM_MNY when c.INTERNALFLAG_INT=0 then a.TOTALSUM_MNY end as TOTALSUM_MNY,c.INTERNALFLAG_INT
                    from t_opr_outpatientrecipeinv a,t_bse_patientpaytype c where  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + strcondition + " and c.INTERNALFLAG_INT!=1   order by a.INVOICENO_VCHR";
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR as ITEMOPCALCTYPE_CHR,b.SBSUM_MNY as TOLPRICE_MNY ,b.TOLFEE_MNY,a.STATUS_INT as PSTAUTS_INT,c.INTERNALFLAG_INT
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + strcondition + " and c.INTERNALFLAG_INT!=1 order by a.INVOICENO_VCHR";
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOutDe);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                strSQL = @"select a.INVOICENO_VCHR,a.RECORDDATE_DAT, a.SEQID_CHR,a.OPREMP_CHR,a.STATUS_INT,a.BALANCEEMP_CHR,a.PAYTYPE_INT,a.PAYTYPEID_CHR,c.INTERNALFLAG_INT,a.ACCTSUM_MNY,a.SBSUM_MNY,a.TOTALSUM_MNY,c.INTERNALFLAG_INT
                    from t_opr_outpatientrecipeinv a,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + strFLAG + strcondition + "  order by a.INVOICENO_VCHR";
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOut);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR as ITEMOPCALCTYPE_CHR,b.SBSUM_MNY as TOLPRICE_MNY,b.TOLFEE_MNY,a.STATUS_INT as PSTAUTS_INT,c.INTERNALFLAG_INT
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and a.BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss')" + strFLAG + strcondition + "   order by a.INVOICENO_VCHR";
                try
                {

                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckOutDe);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

            }

            strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b,t_bse_patientpaytype c where BALANCEFLAG_INT=1 and BALANCE_DAT between To_Date('" + startDate + " 00:00:00','yyyy-mm-dd HH24:mi:ss') and To_Date('" + EndDate + " 23:59:59','yyyy-mm-dd HH24:mi:ss') and a.BALANCEEMP_CHR=b.EMPID_CHR and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR" + strFLAG + strcondition;
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmp);
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

        #region ���������н��ʵ��շ�Ա����
        [AutoComplete]
        public long m_lngReturnAllBALANCEEMP(out DataTable dt)
        {
            dt = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b,t_bse_patientpaytype c where BALANCEFLAG_INT=1 and a.BALANCEEMP_CHR=b.EMPID_CHR and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=2 ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
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

        #region ����ͳ�Ʊ�
        [AutoComplete]
        public long m_lngGetPublicMoney(string startDate, string endDate, out DataTable dt)
        {
            dt = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"select  a.INVDATE_DAT,a.PAYTYPEID_CHR,a.PATIENTID_CHR,b.GOVCARD_CHR,d.INVOICENO_VCHR,c.PAYTYPENAME_VCHR,c.CHARGEPERCENT_DEC,d.ITEMCATID_CHR,d.TOLFEE_MNY  from T_OPR_OUTPATIENTRECIPEINV a,T_BSE_PATIENTPAYTYPE c,T_BSE_PATIENT b,T_OPR_OUTPATIENTRECIPEINVDE d  where  a.INVDATE_DAT between to_date('" + startDate + " 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date('" + endDate + " 23:59:59','yyyy-mm-dd hh24:mi:ss') and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and c.INTERNALFLAG_INT=1 and a.PATIENTID_CHR=b.PATIENTID_CHR and a.INVOICENO_VCHR=d.INVOICENO_VCHR and a.SEQID_CHR=d.SEQID_CHR and b.GOVCARD_CHR is not null order by GOVCARD_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }

        #region ��ȡ�����������Ϣ

        [AutoComplete]
        public long m_lngGetGopRla(out DataTable dt)
        {
            dt = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"select rptid_chr, groupid_chr, typeid_chr, flag_int from t_aid_rpt_gop_rla where  flag_int ='2'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region �շ�Ա�սᱨ��ֿ���ӡ
        /// <summary>
        /// �շ�Ա�սᱨ��ֿ���ӡ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="CheckDate">��������</param>
        /// <param name="checkMan">������</param>
        /// <param name="dt1">�ֽ��սᱨ��</param>
        /// <param name="dtDe1">�ֽ��սᱨ��(��ϸ)</param>
        /// <param name="dt2">ҽ����ˢ���սᱨ��</param>
        ///  <param name="dtDe2">ҽ����ˢ���սᱨ��(��ϸ)</param>
        /// <param name="dt3">�����սᱨ��</param>
        /// <param name="dtDe3">�����սᱨ��(��ϸ)</param>
        /// <param name="dt4">�����սᱨ��</param>
        /// <param name="dtDe4">�����սᱨ��(��ϸ)</param>
        /// <param name="dtType">�շ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataAllOfStat(string CheckDate, string checkMan, out DataTable dt1, out DataTable dtDe1, out DataTable dt2, out DataTable dtDe2, out DataTable dt3, out DataTable dtDe3, out DataTable dt4, out DataTable dtDe4, out DataTable dtType)
        {
            dt1 = null;
            dtDe1 = null;
            dt2 = null;
            dtDe2 = null;
            dt3 = null;
            dtDe3 = null;
            dt4 = null;
            dtDe4 = null;
            dtType = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string OtherWhere = @"  AND a.BALANCE_DAT=to_date('" + CheckDate + "','yyyy-mm-dd hh24:mi:ss') and a.BALANCEEMP_CHR='" + checkMan + "'";
            string strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
    FROM t_opr_outpatientrecipeinv a
   WHERE a.balanceflag_int = 1
     AND a.PAYTYPE_INT=0" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.paytype_int,
         b.itemcatid_chr,b.sbsum_mny,
         b.tolfee_mny
    FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
   WHERE a.balanceflag_int = 1
	 and a.PAYTYPE_INT=0
     AND a.invoiceno_vchr = b.invoiceno_vchr
     AND a.seqid_chr = b.seqid_chr
     AND a.paytypeid_chr = c.paytypeid_chr" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe1);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select MIN (recorddate_dat) AS minrecorddate_dat,
       MAX (recorddate_dat) AS maxrecorddate_dat, sum(sbsum_mny1) as totalmoney from (SELECT a.recorddate_dat, '' AS acctsum_mny1, sbsum_mny AS sbsum_mny1,a.paytype_int
  FROM t_opr_outpatientrecipeinv a
 WHERE a.balanceflag_int = 1 AND a.paytype_int = 1" + OtherWhere + "UNION ALL SELECT a.recorddate_dat, '' AS acctsum_mny1, a.acctsum_mny AS sbsum_mny1,a.paytype_int FROM t_opr_outpatientrecipeinv a, t_bse_patientpaytype c WHERE a.paytypeid_chr = c.paytypeid_chr AND c.internalflag_int = 2" + OtherWhere + ") ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt2);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT invoiceno_vchr, paytype_int, itemcatid_chr,
       sbsum_mny1 AS sbsum_mny, tolfee_mny
  FROM (SELECT a.invoiceno_vchr, a.paytype_int, b.itemcatid_chr,
               b.sbsum_mny AS sbsum_mny1, b.tolfee_mny
          FROM t_opr_outpatientrecipeinv a, t_opr_outpatientrecipesumde b
         WHERE a.balanceflag_int = 1
           AND a.paytype_int = 1
           AND a.invoiceno_vchr = b.invoiceno_vchr
           AND a.seqid_chr = b.seqid_chr" + OtherWhere + " UNION ALL  SELECT a.invoiceno_vchr, a.paytype_int, b.itemcatid_chr,(b.tolfee_mny - b.sbsum_mny) AS sbsum_mny1, b.tolfee_mny FROM t_opr_outpatientrecipeinv a,t_opr_outpatientrecipesumde b,t_bse_patientpaytype c WHERE a.balanceflag_int = 1  AND a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and  a.invoiceno_vchr = b.invoiceno_vchr AND a.seqid_chr = b.seqid_chr AND c.internalflag_int = 2" + OtherWhere + ")";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe2);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select MIN (recorddate_dat) AS minrecorddate_dat,
       MAX (recorddate_dat) AS maxrecorddate_dat, sum(a.ACCTSUM_MNY) as totalmoney
                    from t_opr_outpatientrecipeinv a,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR and c.INTERNALFLAG_INT=1" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt3);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }

            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,tolfee_mny
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 AND a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe3);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select MIN (recorddate_dat) AS minrecorddate_dat,
       MAX (recorddate_dat) AS maxrecorddate_dat, sum(a.ACCTSUM_MNY) as totalmoney
                    from t_opr_outpatientrecipeinv a,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT>2" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt4);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT>2 " + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe4);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtType);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }

        #endregion
        #region �շ�Ա�սᱨ������շ�
        /// <summary>
        ///  �շ�Ա�սᱨ������շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="CheckDate"></param>
        /// <param name="checkMan"></param>
        /// <param name="dt1">�ֽ�֧��</param>
        /// <param name="dtDe1">�ֽ�֧����ϸ</param>
        /// <param name="dt2">IC��</param>
        /// <param name="dtDe2">IC����ϸ</param>
        /// <param name="dt3">���п�</param>
        /// <param name="dtDe3">���п���ϸ</param>
        /// <param name="dt4">֧Ʊ</param>
        /// <param name="dtDe4">֧Ʊ��ϸ</param>
        /// <param name="dt5">��������</param>
        /// <param name="dtDe5">����������ϸ</param>
        /// <param name="dt6">���Ѽ���</param>
        /// <param name="dtDe6">���Ѽ�����ϸ</param>
        /// <param name="dt7">����</param>
        /// <param name="dtDe7">������ϸ</param>
        /// <param name="dt8">����</param>
        /// <param name="dtDe8">������ϸ</param>
        /// <param name="dt9">��Ժ</param>
        /// <param name="dtDe9">��Ժ��ϸ</param>
        /// <param name="dt10">�ض�ҽ������</param>
        /// <param name="dtDe10">�ض�ҽ��������ϸ</param>
        /// <param name="dtType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckOutOfClassification(string CheckDate, string checkMan, out DataTable dt1, out DataTable dtDe1, out DataTable dt2, out DataTable dtDe2, out DataTable dt3, out DataTable dtDe3, out DataTable dt4, out DataTable dtDe4, out DataTable dt5, out DataTable dtDe5, out DataTable dt6, out DataTable dtDe6, out DataTable dt7, out DataTable dtDe7, out DataTable dt8, out DataTable dtDe8, out DataTable dt9, out DataTable dtDe9, out DataTable dt10, out DataTable dtDe10, out DataTable dtType)
        {
            dt1 = null;
            dtDe1 = null;
            dt2 = null;
            dtDe2 = null;
            dt3 = null;
            dtDe3 = null;
            dt4 = null;
            dtDe4 = null;
            dt5 = null;
            dtDe5 = null;
            dt6 = null;
            dtDe6 = null;
            dt7 = null;
            dtDe7 = null;
            dt8 = null;
            dtDe8 = null;
            dt9 = null;
            dtDe9 = null;
            dt10 = null;
            dtDe10 = null;
            dtType = null;
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string OtherWhere = @"  AND a.BALANCE_DAT=to_date('" + CheckDate + "','yyyy-mm-dd hh24:mi:ss') and a.BALANCEEMP_CHR='" + checkMan + "'";
            string strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
    FROM t_opr_outpatientrecipeinv a
   WHERE a.balanceflag_int = 1
     AND a.PAYTYPE_INT=0" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.paytype_int,
         b.itemcatid_chr,b.sbsum_mny,
         b.tolfee_mny
    FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
   WHERE a.balanceflag_int = 1
	 and a.PAYTYPE_INT=0
     AND a.invoiceno_vchr = b.invoiceno_vchr
     AND a.seqid_chr = b.seqid_chr
     AND a.paytypeid_chr = c.paytypeid_chr" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe1);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT=3" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt2);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=3" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe2);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
    FROM t_opr_outpatientrecipeinv a
   WHERE a.balanceflag_int = 1
     AND a.PAYTYPE_INT=1" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt3);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.paytype_int,
         b.itemcatid_chr,b.sbsum_mny,
         b.tolfee_mny
    FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
   WHERE a.balanceflag_int = 1
	 and a.PAYTYPE_INT=1
     AND a.invoiceno_vchr = b.invoiceno_vchr
     AND a.seqid_chr = b.seqid_chr
     AND a.paytypeid_chr = c.paytypeid_chr" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe3);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
    FROM t_opr_outpatientrecipeinv a
   WHERE a.balanceflag_int = 1
     AND a.PAYTYPE_INT=2" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt4);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   a.invoiceno_vchr, a.paytype_int,
         b.itemcatid_chr,b.sbsum_mny,
         b.tolfee_mny
    FROM t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype c
   WHERE a.balanceflag_int = 1
	 and a.PAYTYPE_INT=2
     AND a.invoiceno_vchr = b.invoiceno_vchr
     AND a.seqid_chr = b.seqid_chr
     AND a.paytypeid_chr = c.paytypeid_chr" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe4);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT>5" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt5);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT>5" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe5);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT=1" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt6);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=1" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe6);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT=3" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt7);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=3" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe7);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT=4" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt8);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=4" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe8);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and a.INTERNALFLAG_INT=5" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt9);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=5" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe9);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"SELECT   min(a.recorddate_dat) as minrecorddate_dat,max(a.recorddate_dat) as maxrecorddate_dat,Sum(SBSUM_MNY) as totalmoney
                       FROM t_opr_outpatientrecipeinv a
                       WHERE a.balanceflag_int = 1
                       and  a.INTERNALFLAG_INT=2" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt10);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            strSQL = @"select a.INVOICENO_VCHR,a.PAYTYPE_INT,b.ITEMCATID_CHR ,(b.TOLFEE_MNY-b.SBSUM_MNY) as SBSUM_MNY,b.TOLFEE_MNY
                    from t_opr_outpatientrecipeinv a,T_OPR_OUTPATIENTRECIPESUMDE b,t_bse_patientpaytype c
                    where  a.BALANCEFLAG_INT=1 and a.INVOICENO_VCHR=b.INVOICENO_VCHR and a.SEQID_CHR=b.SEQID_CHR  and a.PAYTYPEID_CHR=c.PAYTYPEID_CHR  and c.INTERNALFLAG_INT=2" + OtherWhere;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDe10);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }


            strSQL = @"Select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int, govtopcharge_mny, emrcat_vchr From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtType);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }

        #endregion
        [AutoComplete]
        public long m_lngGetIsUsing(string feild, string valueid)
        {
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"select count(*) from t_opr_patientregdetail where " + feild + "='" + valueid + "'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            lngRes = long.Parse(dt.Rows[0][0].ToString());
            return lngRes;
        }
        [AutoComplete]
        public long m_lngGetIsUsingChargeType(string feild, string valueid)
        {
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"select count(*) from t_opr_patientregister where " + feild + "='" + valueid + "'";
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            lngRes = long.Parse(dt.Rows[0][0].ToString());
            return lngRes;
        }
        [AutoComplete]
        public long m_blDeleteDetail(string feild, string valueid)
        {
            //����Ƿ���ʹ��Щ������Ȩ��
            long lngRes = 0;
            string strSQL = @"delete from t_bse_registerdetail where " + feild + "='" + valueid + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_lngGetRegisterdetail()
        {
            long lngRes = 0;
            string strSQL = @"insert into t_bse_registerdetail
select distinct e.* from 
(select a.registertypeid_chr,b.chargeid_chr,c.paytypeid_chr,0 as pay,1 as dis
from t_bse_registertype a cross join t_bse_registerchargetype b cross join t_bse_patientpaytype c
) e , t_bse_registerdetail d
where e.registertypeid_chr=d.registertypeid_chr(+) and e.chargeid_chr=d.chargeid_chr(+)
and e.paytypeid_chr = d.paytypeid_chr(+) and d.registertypeid_chr is null
and d.chargeid_chr is null and d.paytypeid_chr is null";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;
        }

        #region �շѴ��Һŷ�Ʊ��ͳ�Ʊ���(��)
        [AutoComplete]
        public long m_lngGetRegisterStatData(string p_strOperatorId,
            string p_strStartDate,
            string p_strEndDate,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            StringBuilder strSQL = new StringBuilder(@"select a.registerid_chr,
                                                        a.invno_chr,
                                                        a.flag_int,
                                                        c.chargeid_chr,
                                                        d.chargename_chr,
                                                        c.payment_mny,
                                                        c.discount_dec,
                                                        b.empid_chr,
                                                        b.empno_chr,
                                                        b.lastname_vchr
                                                      from t_opr_patientregister    a,
                                                           t_bse_employee           b,
                                                           t_opr_patientregdetail   c,
                                                           t_bse_registerchargetype d
                                                      where a.balanceemp_chr = b.empid_chr
                                                        and a.registerid_chr = c.registerid_chr
                                                        and c.chargeid_chr = d.chargeid_chr
                                                        and a.pstatus_int = 4
                                                        and a.flag_int in (1, 3, 4)
                                                        and a.registerdate_dat >= ? 
                                                        and a.registerdate_dat <= ?");

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;
            IDataParameter[] tmp_objParamerArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out tmp_objParamerArr);
            int m_intParamCount = 2;
            tmp_objParamerArr[0].DbType = DbType.Date;
            tmp_objParamerArr[0].Value = Convert.ToDateTime(p_strStartDate);
            tmp_objParamerArr[1].DbType = DbType.Date;
            tmp_objParamerArr[1].Value = Convert.ToDateTime(p_strEndDate);

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL.Append(@" and a.balanceemp_chr = ?");
                    ++m_intParamCount;
                    tmp_objParamerArr[m_intParamCount - 1].Value = p_strOperatorId;
                    strSQL.Append(@" order by a.invno_chr asc");
                }
                else
                {
                    strSQL.Append(@" order by a.invno_chr asc");
                }

                objHRPSvc.CreateDatabaseParameter(m_intParamCount, out objParamerArr);
                for (int i1 = 0; i1 < m_intParamCount; i1++)
                {
                    objParamerArr[i1].Value = tmp_objParamerArr[i1].Value;
                    objParamerArr[i1].DbType = tmp_objParamerArr[i1].DbType;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbResult, objParamerArr);
            }
            catch
            {
            }
            objHRPSvc.Dispose();
            return lngRes;
        }


        #region ���ݲ���ԱId�����ڲ��������ش�Һŷ�Ʊ��Ϣ
        /// <summary>
        /// ���ݲ���ԱId�����ڲ��������ش�Һŷ�Ʊ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRegisterBillReprintByDate(string p_strOperatorId,
                                                 string p_strStartDate,
                                                 string p_strEndDate,
                                                 out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            StringBuilder strSQL = new StringBuilder(@"select a.sourceinvono_vchr,
                                     a.repprninvono_vchr,
                                     a.printemp_chr,
                                     a.printstatus_int,
                                     b.empid_chr,
                                     b.empno_chr,
                                     b.lastname_vchr
                             from t_opr_invoicerepeatprint a,
                                  t_bse_employee b
                             where a.printemp_chr = b.empid_chr
                               and a.type_chr = 2

                               and a.printdate_dat >= ?
                               and a.printdate_dat <= ?");

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;
            IDataParameter[] tmp_objParamerArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out tmp_objParamerArr);
            int m_intParamCount = 2;
            tmp_objParamerArr[0].DbType = DbType.Date;
            tmp_objParamerArr[0].Value = Convert.ToDateTime(p_strStartDate);
            tmp_objParamerArr[1].DbType = DbType.Date;
            tmp_objParamerArr[1].Value = Convert.ToDateTime(p_strEndDate);

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL.Append(@" and a.printemp_chr = ?");
                    ++m_intParamCount;
                    tmp_objParamerArr[m_intParamCount - 1].Value = p_strOperatorId;
                }

                objHRPSvc.CreateDatabaseParameter(m_intParamCount, out objParamerArr);
                for (int i1 = 0; i1 < m_intParamCount; i1++)
                {
                    objParamerArr[i1].Value = tmp_objParamerArr[i1].Value;
                    objParamerArr[i1].DbType = tmp_objParamerArr[i1].DbType;
                }


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, objParamerArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion //���ݲ���ԱId�����ڲ��������ش�Ʊ��Ϣ


        #endregion //�շѴ��Һŷ�Ʊ��ͳ�Ʊ���(��)


        #region ��ѯ��ʷ���� �Ź���   2004-9-9
        /// <summary>
        /// ��ѯ��ʷ���� �Ź���   2004-9-9
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="checkoutEmpNo"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="strempno"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQulHistory(string checkoutEmpNo, string fromDate, string toDate, out clscheckoutreg_VO[] objResult)
        {

            objResult = new clscheckoutreg_VO[0];
            long lngRes = 0;
            string strSQL = "SELECT  distinct balance_dat FROM t_opr_patientregister WHERE  balance_dat BETWEEN TO_DATE ('" + fromDate + "', 'yyyy-MM-dd') " +
"AND TO_DATE ('" + toDate + " 23:59:59','yyyy-mm-dd hh24:mi:ss') and BALANCEEMP_CHR='" + checkoutEmpNo + "' GROUP BY balance_dat";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    objResult = new clscheckoutreg_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < objResult.Length; i1++)
                    {
                        objResult[i1] = new clscheckoutreg_VO();
                        objResult[i1].m_strCHECKOUTDATE_DAT = dtResult.Rows[i1]["balance_dat"].ToString().Trim();
                        objResult[i1].m_strMINREGNO_CHR = dtResult.Rows[i1]["rowsCount"].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;


        }
        #endregion

        #region ��ʷ������ϸ �Ź���   2004-9-9
        /// <summary>
        /// ��ѯ��ʷ���� �Ź���   2004-9-9
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="checkoutEmpNo"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="strempno"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQulHistoryDetial(string checkoutEmpNo, string findDate, out System.Data.DataTable p_datCheckoutDetial)
        {

            p_datCheckoutDetial = new DataTable();
            long lngRes = 0;
            string strSQL = "SELECT   registerid_chr " +
                "FROM t_opr_patientregister " +
                "WHERE registeremp_chr = '" + checkoutEmpNo + "' " +
                "AND pstatus_int = 4 " +
                "AND balance_dat = TO_DATE('" + findDate + "', 'yyyy-mm-dd hh24:mi:ss') ";


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_datCheckoutDetial);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;


        }
        #endregion

        #region ����Һű��� �Ź���   2004-9-9
        /// <summary>
        /// ����Һű��� �Ź���   2004-9-9
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="firstdate">��ʼ����</param>
        /// <param name="lastdate">��������</param>
        /// <param name="p_tabReport">���ݱ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindByDateReport(string firstdate, string lastdate, out System.Data.DataTable p_tabReport)
        {

            p_tabReport = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT a.registerid_chr, a.registerdate_dat, a.diagdoctor_chr,
       c.lastname_vchr, a.diagdept_chr, e.deptname_vchr,
       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail d
         WHERE d.registerid_chr = a.registerid_chr
           AND d.chargeid_chr = '001') AS rcharge,
       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail d
         WHERE d.registerid_chr = a.registerid_chr
           AND d.chargeid_chr = '002') AS dcharge,
       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail d
         WHERE d.registerid_chr = a.registerid_chr
           AND d.chargeid_chr = '003') AS gcharge,
       (SELECT payment_mny * discount_dec
          FROM t_opr_patientregdetail d
         WHERE d.registerid_chr = a.registerid_chr
           AND d.chargeid_chr = '004') AS ccharge,
       a.recorddate_dat, a.flag_int
  FROM t_opr_patientregister a, t_bse_employee c, t_bse_deptdesc e
 WHERE a.diagdoctor_chr = c.empid_chr(+)
   AND a.diagdept_chr = e.deptid_chr
   AND a.balanceemp_chr IS NOT NULL 
   AND a.registerdate_dat BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss')
                         AND TO_DATE (?,
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )   order by a.diagdept_chr";

            try
            {



                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = firstdate + " 00:00:00";
                objLisAddItemRefArr[1].Value = lastdate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_tabReport, objLisAddItemRefArr);
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

        #region �Һ�Ա�ձ��� �Ź���   2004-9-9
        /// <summary>
        /// �Һ�Ա�ձ��� �Ź���   2004-9-9
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="firstdate">��ѯ��ʼ����</param>
        /// <param name="lastdate">��ѯ��������</param>
        /// <param name="p_tabReport">���ݱ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindByDateReport2(string firstdate, string lastdate, out System.Data.DataTable p_tabReport)
        {

            p_tabReport = new DataTable();

            long lngRes = 0;
            string strSQL = @"SELECT   a.registerid_chr, a.registeremp_chr AS empid, c.lastname_vchr,
         a.registerdate_dat, e.deptname_vchr,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '001') AS rcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '002') AS dcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '003') AS gcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '004') AS ccharge,
         a.recorddate_dat, a.flag_int
    FROM t_opr_patientregister a, t_bse_employee c, t_bse_deptdesc e
   WHERE a.registeremp_chr = c.empid_chr(+)
     AND a.diagdept_chr = e.deptid_chr
     AND a.balanceemp_chr IS NOT NULL
     AND a.returnemp_chr IS NULL
     AND a.registerdate_dat BETWEEN TO_DATE (?,
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
                           AND TO_DATE (?,
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
UNION ALL
SELECT   a.registerid_chr, a.returnemp_chr AS empid, c.lastname_vchr,
         a.registerdate_dat, e.deptname_vchr,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '001') AS rcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '002') AS dcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '003') AS gcharge,
         (SELECT payment_mny * discount_dec
            FROM t_opr_patientregdetail d
           WHERE d.registerid_chr = a.registerid_chr
             AND d.chargeid_chr = '004') AS ccharge,
         a.recorddate_dat, a.flag_int
    FROM t_opr_patientregister a, t_bse_employee c, t_bse_deptdesc e
   WHERE a.returnemp_chr = c.empid_chr(+)
     AND a.diagdept_chr = e.deptid_chr
     AND a.balanceemp_chr IS NOT NULL
     AND a.returnemp_chr IS NOT NULL
     AND a.registerdate_dat BETWEEN TO_DATE (?,
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
                           AND TO_DATE (?,
                                        'yyyy-mm-dd hh24:mi:ss'
                                       )
ORDER BY empid";


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = firstdate + " 00:00:00";
                objLisAddItemRefArr[1].Value = lastdate + " 23:59:59";
                objLisAddItemRefArr[2].Value = firstdate + " 00:00:00";
                objLisAddItemRefArr[3].Value = lastdate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_tabReport, objLisAddItemRefArr);
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

        #region �����˴��ձ��� �Ź���   2005-03-2
        [AutoComplete]
        public long m_lngDepIncomerpt(string firstdate, string lastdate, out System.Data.DataTable p_tabReport, out System.Data.DataTable depDt)
        {

            p_tabReport = new DataTable();
            depDt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT   t2.deptid_chr, t2.deptname_vchr, t2.parentid, t1.flag_int,
         t1.planperiod_chr, t1.registertypeid_chr,
         t3.deptname_vchr AS parentname_vchr, t3.code_vchr
    FROM t_opr_patientregister t1, t_bse_deptdesc t2, t_bse_deptdesc t3
   WHERE  t1.balanceemp_chr IS NOT NULL and t1.registerdate_dat  BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss')
                            AND TO_DATE (?,
                                         'yyyy-mm-dd hh24:mi:ss'
                                        )
     AND t1.diagdept_chr = t2.deptid_chr(+)
     AND t2.parentid = t3.deptid_chr(+)";

            try
            {



                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = firstdate + " 00:00:00";
                objLisAddItemRefArr[1].Value = lastdate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_tabReport, objLisAddItemRefArr);
                objHRPSvc.Dispose();


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT   distinct(t2.deptid_chr),t2.deptname_vchr, 
         t4.ordercode_vchr,t3.deptname_vchr AS parentname_vchr, t3.code_vchr
    FROM t_opr_patientregister t1,
         t_bse_deptdesc t2,
         t_bse_deptdesc t3,
         t_opr_rptorder t4 where  t1.balanceemp_chr IS NOT NULL and t1.registerdate_dat  BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss')AND TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss') AND t1.diagdept_chr = t2.deptid_chr(+)
     AND t2.deptid_chr = t4.deptid_chr(+)
     AND t2.parentid = t3.deptid_chr(+)
ORDER BY t4.ordercode_vchr";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = firstdate + " 00:00:00";
                objLisAddItemRefArr[1].Value = lastdate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref depDt, objLisAddItemRefArr);
                objHRPSvc.Dispose();


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;


        }
        [AutoComplete]
        public long m_lngSelectAllRptorder(out System.Data.DataTable p_tabReport)
        {
            p_tabReport = new DataTable();
            long lngRes = 0;
            //Ȩ����
            string strSQL = @"SELECT a.deptid_chr, a.deptname_vchr, a.parentid,
       b.ordercode_vchr AS shortno_chr
  FROM t_bse_deptdesc a, t_opr_rptorder b
 WHERE a.deptid_chr = b.deptid_chr(+) order by b.ordercode_vchr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_tabReport);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;


        }
        [AutoComplete]
        public long m_lngInsertAllRptorder(string[] p_IdArr)
        {
            long lngRes = 0;
            //Ȩ����
            string strSQL = @"delete from t_opr_rptorder ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                string Sql = "";
                for (int i = 0; i < p_IdArr.Length; i++)
                {
                    Sql = "insert into t_opr_rptorder(DEPTID_CHR,ORDERCODE_VCHR) values('" + p_IdArr[i] + "','" + i.ToString().PadLeft(10, '0') + "')";
                    lngRes = objHRPSvc.DoExcute(Sql);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;


        }
        #endregion

        #region ��ȡ���ݿ���Ϣ(������ҹҺ���������)
        // ע�⣺Ҫ��ʾ�������ֶ������������ݼ�dataset���ֶ�����ͬ
        /// <summary>
        ///  ��ȡ���ݿ���Ϣ(������ҹҺ���������)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtpStartDate">yyyy-mm-dd</param>
        /// <param name="p_dtpEndDate">yyyy-mm-dd</param>
        /// <param name="p_outDatatable"></param>
        /// <returns></returns>

        [AutoComplete]
        public long m_lngGetRegReportPicture(string p_dtpStartDate, string p_dtpEndDate, out DataTable p_outDatatable)
        {
            long lngRes = 0;
            p_outDatatable = null;
            string strSQL = @"select t1.deptname_vchr as deptname, sum(t1.payment) as payment,chargename 
         from(
         SELECT  e.deptname_vchr , SUM (f.payment_mny*f.discount_dec) AS payment,g.chargename_chr AS chargename
         FROM t_opr_patientregister a,
              t_bse_patientpaytype b,
			  t_bse_employee c,
			  t_bse_patientidx d,
			  t_bse_deptdesc e,
			  t_opr_patientregdetail f,
			  t_bse_registerchargetype g
		WHERE a.paytypeid_chr = b.paytypeid_chr(+)
				AND a.registeremp_chr = c.empid_chr(+) 
			AND a.patientid_chr = d.patientid_chr(+)
			AND a.diagdept_chr = e.deptid_chr
			AND a.registerid_chr = f.registerid_chr(+)
			AND f.chargeid_chr = g.chargeid_chr
            AND a.balanceemp_chr IS NOT NULL 
			and a.flag_int<>3 AND a.registerdate_dat BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss' )AND TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss') GROUP BY e.deptname_vchr, g.chargename_chr
			UNION
			SELECT  e.deptname_vchr , SUM (-f.payment_mny*f.discount_dec) AS payment,g.chargename_chr AS chargename
         FROM t_opr_patientregister a,
              t_bse_patientpaytype b,
			  t_bse_employee c,
			  t_bse_patientidx d,
			  t_bse_deptdesc e,
			  t_opr_patientregdetail f,
			  t_bse_registerchargetype g
		WHERE a.paytypeid_chr = b.paytypeid_chr(+)
			AND a.registeremp_chr = c.empid_chr(+) 
			AND a.patientid_chr = d.patientid_chr(+)
			AND a.diagdept_chr = e.deptid_chr
			AND a.registerid_chr = f.registerid_chr(+)
			AND f.chargeid_chr = g.chargeid_chr
            AND a.balanceemp_chr IS NOT NULL 
			and a.flag_int=3 AND a.registerdate_dat BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss' ) AND TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss') GROUP BY e.deptname_vchr, g.chargename_chr) t1
			GROUP BY t1.deptname_vchr, t1.chargename  order by  payment";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_dtpStartDate + " 00:00:00";
                objLisAddItemRefArr[1].Value = p_dtpEndDate + " 23:59:59";
                objLisAddItemRefArr[2].Value = p_dtpStartDate + " 00:00:00";
                objLisAddItemRefArr[3].Value = p_dtpEndDate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_outDatatable, objLisAddItemRefArr);
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

        #region ��ȡ���ݿ���Ϣ(����ҽ���Һ���������)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtpStartDate">yyyy-mm-dd</param>
        /// <param name="p_dtpEndDate">yyyy-mm-dd</param>
        /// <param name="p_outDatatable"></param>
        /// <returns></returns>
        // ע�⣺Ҫ��ʾ�������ֶ������������ݼ�dataset���ֶ�����ͬ
        [AutoComplete]
        public long m_lngGetRegReportDoctPicture(string p_dtpStartDate, string p_dtpEndDate, out DataTable p_outDatatable)
        {
            long lngRes = 0;
            p_outDatatable = null;
            string strSQL = @"select t1.lastname_vchr, sum(t1.payment) as payment,chargename 
         from(
         SELECT  c.lastname_vchr , SUM(f.payment_mny*f.discount_dec) AS payment,g.chargename_chr AS chargename
         FROM t_opr_patientregister a,
              t_bse_patientpaytype b,
			  t_bse_employee c,
			  t_bse_patientidx d,
			  t_bse_deptdesc e,
			  t_opr_patientregdetail f,
			  t_bse_registerchargetype g
		WHERE a.paytypeid_chr = b.paytypeid_chr(+)
			AND a.DIAGDOCTOR_CHR = c.empid_chr
			AND a.patientid_chr = d.patientid_chr(+)
			AND a.diagdept_chr = e.deptid_chr(+)
			AND a.registerid_chr = f.registerid_chr(+)
			AND f.chargeid_chr = g.chargeid_chr
������������AND a.balanceemp_chr IS NOT NULL
			and a.flag_int<>3 AND a.registerdate_dat BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss' ) AND TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss')
            GROUP BY c.lastname_vchr, g.chargename_chr
			UNION
			SELECT c.lastname_vchr, SUM (-f.payment_mny*f.discount_dec) AS payment,g.chargename_chr AS chargename
         FROM t_opr_patientregister a,
              t_bse_patientpaytype b,
			  t_bse_employee c,
			  t_bse_patientidx d,
			  t_bse_deptdesc e,
			  t_opr_patientregdetail f,
			  t_bse_registerchargetype g
		WHERE a.paytypeid_chr = b.paytypeid_chr(+)
			AND a.DIAGDOCTOR_CHR = c.empid_chr
			AND a.patientid_chr = d.patientid_chr(+)
			AND a.diagdept_chr = e.deptid_chr(+)
			AND a.registerid_chr = f.registerid_chr(+)
			AND f.chargeid_chr = g.chargeid_chr
������������AND a.balanceemp_chr IS NOT NULL
			and a.flag_int=3 AND a.registerdate_dat BETWEEN TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss' )AND TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss')
            GROUP BY c.lastname_vchr, g.chargename_chr) t1
		    GROUP BY t1.lastname_vchr, t1.chargename  order by  payment";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_dtpStartDate + " 00:00:00";
                objLisAddItemRefArr[1].Value = p_dtpEndDate + " 23:59:59";
                objLisAddItemRefArr[2].Value = p_dtpStartDate + " 00:00:00";
                objLisAddItemRefArr[3].Value = p_dtpEndDate + " 23:59:59";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_outDatatable, objLisAddItemRefArr);
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

        #region ��Ʊ����

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dtRow"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew(DataRow dtRow, out string newID)
        {
            newID = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            newID = HRPSvc.m_strGetNewID("t_opr_reginvman", "APPID_CHR", 10);
            string strSQL = @"insert into t_opr_reginvman(APPID_CHR,INVOICENOFROM_VCHR,INVOICENOTO_VCHR,APPLY_DAT,APPUSERID_CHR,OPERATORID_CHR,STATUS_INT)values('" + newID + "','" + dtRow["INVOICENOFROM_VCHR"].ToString().Trim() + "','" + dtRow["INVOICENOTO_VCHR"].ToString().Trim() + "',To_Date('" + dtRow["APPLY_DAT"].ToString().Trim() + "','yyyy-mm-dd hh24:mi:ss'),'" + dtRow["APPUSERID_CHR"].ToString().Trim() + "','" + dtRow["OPERATORID_CHR"].ToString().Trim() + "',0)";
            try
            {
                lngRes = HRPSvc.DoExcute(strSQL);
                HRPSvc.Dispose();
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

        #region ���Ϸ�Ʊ
        /// <summary>
        /// ���Ϸ�Ʊ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancel(string strID, string acctID, DateTime AccDate)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string newID = HRPSvc.m_strGetNewID("t_opr_reginvman", "APPID_CHR", 10);
            string strSQL = @"update t_opr_reginvman set STATUS_INT=1,CANCELUSERID_CHR='" + acctID + "',CANCEL_DAT=To_Date('" + AccDate + "','yyyy-mm-dd hh24:mi:ss') where APPID_CHR='" + strID + "'";
            try
            {
                lngRes = HRPSvc.DoExcute(strSQL);
                HRPSvc.Dispose();
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

        #region ��ȡ���еķ�Ʊ����
        /// <summary>
        /// ��ȡ���еķ�Ʊ����
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dt">�������е�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllData(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string newID = HRPSvc.m_strGetNewID("t_opr_reginvman", "APPID_CHR", 10);
            string strSQL = @"select a.appid_chr,
       a.invoicenofrom_vchr,
       a.invoicenoto_vchr,
       a.apply_dat,
       a.appuserid_chr,
       a.operatorid_chr,
       a.canceluserid_chr,
       a.status_int,
       a.cancel_dat,case when a.status_int=0 then '����'  when a.status_int=1 then '����' end as statustype, b.lastname_vchr as appusername,b.empno_chr,c.lastname_vchr as operatorname,d.lastname_vchr as cancelname from t_opr_reginvman a,t_bse_employee b,t_bse_employee c,t_bse_employee d where a.appuserid_chr=b.empid_chr and a.operatorid_chr=c.empid_chr(+) and a.canceluserid_chr=d.empid_chr(+)";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dt);
                HRPSvc.Dispose();
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

        #region ���ݹ��Ų���Ա��
        /// <summary>
        /// ���ݹ��Ų���Ա��
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strNO"></param>
        /// <param name="dtEmp"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngfindEmp(string strNO, out DataTable dtEmp)
        {
            dtEmp = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            string strSQL = @"select EMPID_CHR,lastname_vchr from t_bse_employee where EMPNO_CHR='" + strNO + "'";
            try
            {
                lngRes = HRPSvc.DoGetDataTable(strSQL, ref dtEmp);
                HRPSvc.Dispose();
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

        #region ���������֤������Ϣ��ȡ�����(������ݶ�Ӧ�ű�)
        /// <summary>
        /// ���ݲ���ID���뻼����������ҳ�֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPATIENTID_CHR">���߱��</param>
        /// <param name="p_strPAYTYPEID_CHR">�����������</param>
        /// <param name="p_strNo">������Ͷ�Ӧ����</param>
        /// <param name="p_strResultPAYTYPEID_CHR">�������</param>
        /// <param name="p_strPAYTYPENAME_VCHR"></param>
        /// <param name="p_strINTERNALFLAG_INT">0-��ͨ 1-���� 2-ҽ�� 3-���� ���ڲ�ʹ�ã��������֣�</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindNoByPatientIdAndTypeId(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR,
            out string p_strNo,
            out string p_strResultPAYTYPEID_CHR,
            out string p_strPAYTYPENAME_VCHR,
            out string p_strINTERNALFLAG_INT
            )
        {
            long lngRes = 0;
            p_strNo = "";
            p_strResultPAYTYPEID_CHR = "";
            p_strPAYTYPENAME_VCHR = "";
            p_strINTERNALFLAG_INT = "";
            string strSQL = "SELECT   A.PATIENTID_CHR,A.PAYTYPEID_CHR,A.IDNO_VCHR,B.PAYTYPENAME_VCHR ,B.INTERNALFLAG_INT  from t_bse_patientidentityno A ,t_bse_patientpaytype B where A.PAYTYPEID_CHR = B.PAYTYPEID_CHR   and A.PATIENTID_CHR='" + p_strPATIENTID_CHR + "' ";//
            if (p_strPAYTYPEID_CHR != "")
            {
                strSQL += " and  a.PAYTYPEID_CHR='" + p_strPAYTYPEID_CHR + "' ";
            }
            strSQL += " order by  a.PAYTYPEID_CHR asc";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable objdatCheckoutDetial = null;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref objdatCheckoutDetial);
                if (lngRes > 0)
                {
                    if (objdatCheckoutDetial.Rows.Count > 0)
                    {
                        p_strNo = objdatCheckoutDetial.Rows[0]["IDNO_VCHR"].ToString();
                        p_strResultPAYTYPEID_CHR = objdatCheckoutDetial.Rows[0]["PAYTYPEID_CHR"].ToString();
                        p_strPAYTYPENAME_VCHR = objdatCheckoutDetial.Rows[0]["PAYTYPENAME_VCHR"].ToString();
                        p_strINTERNALFLAG_INT = objdatCheckoutDetial.Rows[0]["INTERNALFLAG_INT"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ���Ӳ��������֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPATIENTID_CHR"></param>
        /// <param name="p_strPAYTYPEID_CHR"></param>
        /// <param name="p_strNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddPatientIdTypeIdNo(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR, string p_strNo)
        {
            long lngRes = 0;
            string strSQL = @"insert into t_bse_patientidentityno( PATIENTID_CHR,PAYTYPEID_CHR,IDNO_VCHR) values ('" + p_strPATIENTID_CHR.Trim().PadLeft(10, '0') + "','" + p_strPAYTYPEID_CHR + "','" + p_strNo.Trim() + "') ";
            string strNo;
            string strPayTypeid;
            string strName;
            string strFlag;
            bool blnHas = false;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //string strSQL0 = "update t_bse_patientidentityno set iscurrentuse_chr = '0' where PATIENTID_CHR = '" + p_strPATIENTID_CHR + "' ";
                //lngRes = objHRPSvc.DoExcute(strSQL0);

                string strSQLFind = @"SELECT   A.PATIENTID_CHR,A.PAYTYPEID_CHR,A.IDNO_VCHR,B.PAYTYPENAME_VCHR ,B.INTERNALFLAG_INT  from t_bse_patientidentityno A ,t_bse_patientpaytype B where A.PAYTYPEID_CHR = B.PAYTYPEID_CHR   and A.PATIENTID_CHR='" + p_strPATIENTID_CHR.Trim().PadLeft(10, '0') + "'  and  a.PAYTYPEID_CHR='" + p_strPAYTYPEID_CHR + "' ";
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.DoGetDataTable(strSQLFind, ref dt);
                if (lngRes < 0)
                {
                    return -1;
                }
                if (dt.Rows.Count > 0)
                {
                    blnHas = true;
                }
                if (blnHas)
                {
                    string strSQLUpdate = @"update t_bse_patientidentityno set IDNO_VCHR='" + p_strNo + "'  where PATIENTID_CHR = '" + p_strPATIENTID_CHR.Trim().PadLeft(10, '0') + "' and PAYTYPEID_CHR='" + p_strPAYTYPEID_CHR + "'";
                    lngRes = objHRPSvc.DoExcute(strSQLUpdate);
                }
                else
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ���²��������֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPATIENTID_CHR"></param>
        /// <param name="p_strPAYTYPEID_CHR"></param>
        /// <param name="p_strNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdatePatientIdTypeIdNo(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR, string p_strNo)
        {
            long lngRes = 0;
            string strSQL = "update t_bse_patientidentityno set IDNO_VCHR='" + p_strNo + "'    where PATIENTID_CHR = '" + p_strPATIENTID_CHR + "' and PAYTYPEID_CHR='" + p_strPAYTYPEID_CHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion



    }
    public class clsGetPatientType : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsGetPatientType()
        {
        }
        /// <summary>
        /// ��ò���������Ϣ
        /// </summary>
        /// <param name="strPatientType">��������</param>
        [AutoComplete]
        public long m_mthGetPatientInfo(string strPatientType, out int intPatientType)
        {
            long lngRes = 0;
            intPatientType = 0;
            System.Data.DataTable dtbResult = null;

            string strSQL = @"SELECT PAYFLAG_DEC FROM t_bse_patientpaytype  WHERE PAYTYPEID_CHR = '" + strPatientType.Trim() + "'";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {

                    try
                    {
                        intPatientType = Convert.ToInt16(dtbResult.Rows[0]["PAYFLAG_DEC"].ToString().Trim());
                    }
                    catch
                    {
                        intPatientType = 0;
                    }
                }
                else
                {
                    intPatientType = 0;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsConcertreCipeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsConcertreCipeSvc() { }

        #region ����Э������
        [AutoComplete]
        public long m_lngAddNewConcertreCipe(out string p_strRecordID, clsConcertrectpe_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "RECIPEID_CHR", "T_AID_CONCERTRECIPE", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_AID_CONCERTRECIPE (RECIPEID_CHR,RECIPENAME_CHR,PRIVILEGE_INT,USERCODE_CHR,WBCODE_CHR,PYCODE_CHR,STATUS_INT,CREATERID_CHR) VALUES (?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(8, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strRECIPEID_CHR = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strRECIPENAME_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_intPRIVILEGE_INT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strUSERCODE_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strWBCODE_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strPYCODE_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strCREATERID_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region ����Э��������ϸ
        [AutoComplete]
        public long m_lngAddNewConcertreCipeDetail(out string p_strRecordID, clsConcertrecipeDetail_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "DETAILID_CHR", "T_AID_CONCERTRECIPEDETAIL", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_AID_CONCERTRECIPEDETAIL (RECIPEID_CHR,DETAILID_CHR,ITEMID_CHR,QTY_DEC,DOSETYPE_CHR,FREQID_CHR) VALUES (?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strRECIPEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strDETAILID_CHR = p_strRecordID;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strITEMID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strQTY_DEC;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strUsageID;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strFrequencyID;
                long lngRecEff = -1;
                //�������Ӽ�¼
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
        #region ����������Ƿ�ʹ��
        /// <summary>
        /// ����������Ƿ�ʹ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">����ID ��Ϊ�վ����޸�ʱʹ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthCheckCodeIsUsed(string strCode, string strID, string strFLAG)
        {
            long lngRes = 0;



            string strSQL = @"SELECT USERCODE_CHR FROM T_AID_CONCERTRECIPE where USERCODE_CHR ='" + strCode + "' and STATUS_INT =1 and FLAG_INT=" + strFLAG;
            if (strID.Trim() != "")
            {
                strSQL += " and  RECIPEID_CHR <>'" + strID + "'";
            }

            try
            {
                DataTable dt = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (lngRes > 0)
                {
                    if (dt.Rows.Count > 0 && dt.Rows[0]["USERCODE_CHR"].ToString().Trim() == strCode.Trim())
                    {
                        lngRes = 3;
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �������ò���
        [AutoComplete]
        public long m_lngAddNewConcertreCipeDept(clsConcertrecipeDept_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_AID_CONCERTRECIPEDEPT (RECIPEID_CHR,DEPTID_CHR) VALUES (?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strRECIPEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strDEPTID_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
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

        #region �޸�Э������
        [AutoComplete]
        public long m_lngConcertreCipeModify(clsConcertrectpe_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"update  T_AID_CONCERTRECIPE
				set 
RECIPENAME_CHR='" + p_objRecord.m_strRECIPENAME_CHR + @"',
PRIVILEGE_INT='" + p_objRecord.m_intPRIVILEGE_INT.ToString() + @"',
USERCODE_CHR='" + p_objRecord.m_strUSERCODE_CHR + @"',
WBCODE_CHR='" + p_objRecord.m_strWBCODE_CHR + @"',
PYCODE_CHR='" + p_objRecord.m_strPYCODE_CHR + @"',
STATUS_INT='" + p_objRecord.m_intSTATUS_INT.ToString() + @"',
CREATERID_CHR='" + p_objRecord.m_strCREATERID_CHR + "' where RECIPEID_CHR='" + p_objRecord.m_strRECIPEID_CHR + "'"
 ;
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

        #region �޸Ĵ�����ϸ
        [AutoComplete]
        public long m_lngConcertreCipeDetailModify(clsConcertrecipeDetail_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"update T_AID_CONCERTRECIPEDETAIL
				set
ITEMID_CHR='" + p_objRecord.m_strITEMID_CHR + @"',
QTY_DEC='" + p_objRecord.m_strQTY_DEC + @"', 
DOSETYPE_CHR='" + p_objRecord.m_strUsageID + @"', 
FREQID_CHR='" + p_objRecord.m_strFrequencyID + @"' 
 where RECIPEID_CHR ='" + p_objRecord.m_strRECIPEID_CHR + "' and DETAILID_CHR='" + p_objRecord.m_strDETAILID_CHR + "'";
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

        #region ɾ������
        [AutoComplete]
        public long m_lngDeleteConcertrecipe(clsConcertrectpe_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete from T_AID_CONCERTRECIPE where RECIPEID_CHR ='" + p_objRecord.m_strRECIPEID_CHR + "'";

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

        #region ɾ��������ϸ
        [AutoComplete]
        public long m_lngDeleteConcertrecipeDetail(clsConcertrecipeDetail_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete from T_AID_CONCERTRECIPEDETAIL where RECIPEID_CHR ='" + p_objRecord.m_strRECIPEID_CHR + "' and DETAILID_CHR='" + p_objRecord.m_strDETAILID_CHR + "'";

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

        #region ɾ����������
        [AutoComplete]
        public long m_lngDeleteConcertrecipeDept(clsConcertrecipeDept_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete from T_AID_CONCERTRECIPEDEPT where RECIPEID_CHR='" + p_objRecord.m_strRECIPEID_CHR + "' and DEPTID_CHR ='" + p_objRecord.m_strDEPTID_CHR + "'";

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

        #region Э����ϵͳ(��)
        #region ����Ա��IDȡ������
        [AutoComplete]
        public long m_lngGetConcertreCipeByEmpIDOutTB(string CREATERID, string strEmptID, out DataTable dtbResult, int intFLAG, bool IsPublic)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select A.RECIPEID_CHR,A.RECIPENAME_CHR,a.DISEASENAME_VCHR,case when A.PRIVILEGE_INT=0 then '����' when A.PRIVILEGE_INT=1 then '˽��' when A.PRIVILEGE_INT=2 then '����' end as strPRIVILEGE,A.USERCODE_CHR,A.WBCODE_CHR,A.PYCODE_CHR,A.CREATERID_CHR,b.LASTNAME_VCHR, a.PECLUSCODE, a.PECLUSNAME from T_AID_CONCERTRECIPE A,T_BSE_EMPLOYEE b where a.CREATERID_CHR=b.EMPID_CHR  and A.PRIVILEGE_INT =1 and
 A.CREATERID_CHR ='" + strEmptID + @"' and A.STATUS_INT =1 and a.FLAG_INT=" + intFLAG.ToString();
            if (IsPublic == true)
            {
                strSQL += @" union select A.RECIPEID_CHR,A.RECIPENAME_CHR,a.DISEASENAME_VCHR,case when A.PRIVILEGE_INT=0 then '����' when A.PRIVILEGE_INT=1 then '˽��' when A.PRIVILEGE_INT=2 then '����' end as strPRIVILEGE,A.USERCODE_CHR,A.WBCODE_CHR,A.PYCODE_CHR,A.CREATERID_CHR,b.LASTNAME_VCHR, a.PECLUSCODE, a.PECLUSNAME from T_AID_CONCERTRECIPE A ,T_BSE_EMPLOYEE b WHERE a.CREATERID_CHR=b.EMPID_CHR and A.PRIVILEGE_INT =0 and A.STATUS_INT =1 and a.FLAG_INT=" + intFLAG.ToString() + @"  
 union
 select AA.RECIPEID_CHR,AA.RECIPENAME_CHR,AA.DISEASENAME_VCHR,case when AA.PRIVILEGE_INT=0 then '����' when AA.PRIVILEGE_INT=1 then '˽��' when AA.PRIVILEGE_INT=2 then '����' end as strPRIVILEGE,AA.USERCODE_CHR,AA.WBCODE_CHR,AA.PYCODE_CHR,AA.CREATERID_CHR,CC.LASTNAME_VCHR, aa.PECLUSCODE, aa.PECLUSNAME from T_AID_CONCERTRECIPE AA,
 ( select A.recipeid_chr from T_AID_CONCERTRECIPEDEPT A where  A.deptid_chr
 in (select DEPTID_CHR from T_BSE_DEPTEMP where EMPID_CHR ='" + strEmptID + @"')) BB,T_BSE_EMPLOYEE CC
  where AA.RECIPEID_CHR =BB.RECIPEID_CHR and aa.CREATERID_CHR=cc.EMPID_CHR and AA.PRIVILEGE_INT =2 
  and AA.STATUS_INT =1 and AA.FLAG_INT=" + intFLAG.ToString() + @"
  union 
select AA.RECIPEID_CHR,AA.RECIPENAME_CHR,AA.DISEASENAME_VCHR,case when AA.PRIVILEGE_INT=0 
 then '����' when AA.PRIVILEGE_INT=1 then '˽��' when AA.PRIVILEGE_INT=2 
 then '����' end as strPRIVILEGE,AA.USERCODE_CHR,AA.WBCODE_CHR,AA.PYCODE_CHR,
 AA.CREATERID_CHR,CC.LASTNAME_VCHR, aa.PECLUSCODE, aa.PECLUSNAME from T_AID_CONCERTRECIPE AA,T_BSE_EMPLOYEE CC
 where AA.CREATERID_CHR='" + CREATERID + "'and  aa.CREATERID_CHR=cc.EMPID_CHR and AA.PRIVILEGE_INT =2 and AA.STATUS_INT =1 and AA.FLAG_INT=" + intFLAG.ToString() + @" order by USERCODE_CHR";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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


        #region ��ȡƵ�ʵĴ���������
        /// <summary>
        /// ��ȡƵ�ʵĴ���������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strResult"></param>
        /// <param name="strFREQID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayAndTime(out string strResult, out string strResult1, string strFREQID)
        {
            strResult1 = null;
            strResult = null;
            long lngRes = 0;
            string strSQL = @"Select TIMES_INT,DAYS_INT   From t_aid_recipefreq where FREQID_CHR='" + strFREQID + "'";
            DataTable dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                strResult = dtbResult.Rows[0]["TIMES_INT"].ToString();
                strResult1 = dtbResult.Rows[0]["DAYS_INT"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��鲿λ����������
        /// <summary>
        /// ��ȡ��鲿λ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="P_dtPark"></param>
        /// <param name="strItemId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPart(out DataTable P_dtPark, out string ParkName, out string ParkID, string strItemId, string strType)
        {
            P_dtPark = new DataTable();
            ParkName = "";
            ParkID = "";
            long lngRes = 0;
            DataTable dt = new DataTable();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (strType == "0")//��������
            {
                strSQL = "select g.sample_type_id_chr,h.sample_type_desc_vchr from 	t_aid_lis_apply_unit g,t_aid_lis_sampletype h where g.APPLY_UNIT_ID_CHR='" + strItemId + "' and g.sample_type_id_chr = h.sample_type_id_chr";

                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    ParkName = dt.Rows[0]["sample_type_desc_vchr"].ToString();
                }
                strSQL = @"select SAMPLE_TYPE_DESC_VCHR,PYCODE_CHR,WBCODE_CHR,SAMPLE_TYPE_ID_CHR  from t_aid_lis_sampletype";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref P_dtPark);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    ParkName = dt.Rows[0]["sample_type_desc_vchr"].ToString();
                    ParkID = dt.Rows[0]["sample_type_id_chr"].ToString();
                }
            }
            else//��鲿λ
            {
                strSQL = @"select ASSISTCODE_CHR,PARTNAME,PARTID from ar_apply_partlist";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref P_dtPark);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    ParkName = dt.Rows[0]["sample_type_desc_vchr"].ToString();
                }
            }

            return lngRes;
        }

        #endregion

        #region ���ݴ���IDȡ��������ϸ
        [AutoComplete]
        public long m_lngGetConcertreCipeDetailByIDOutTb(string strID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.*,case when a.FLAG_INT is null or a.FLAG_INT=2 then '' when a.FLAG_INT=0 then (select f.SAMPLE_TYPE_DESC_VCHR from t_aid_lis_sampletype f where a.PARTORTYPE_VCHR=f.SAMPLE_TYPE_ID_CHR) else (select k.PARTNAME from ar_apply_partlist k where a.PARTORTYPE_VCHR=k.PARTID) end 
  as PARTORTYPENAME_VCHR,b.ITEMNAME_VCHR,(select k.TYPENAME_VCHR from t_bse_chargeitemextype k where k.TYPEID_CHR=b.ITEMOPINVTYPE_CHR and FLAG_INT=2) as ItemType,b.ITEMSPEC_VCHR,case when b.OPCHARGEFLG_INT=1 then b.ITEMIPUNIT_CHR  when b.OPCHARGEFLG_INT=0 then b.ITEMOPUNIT_CHR end as ITEMOPUNIT_CHR,case when b.OPCHARGEFLG_INT=1 then  ROUND (b.ITEMPRICE_MNY / b.PACKQTY_DEC, 4) when b.OPCHARGEFLG_INT=0 then  b.ITEMPRICE_MNY end as ITEMPRICE_MNY ,b.DOSAGEUNIT_CHR,d.usagename_vchr,e.freqname_chr,e.DAYS_INT as daytag ,f.NOQTYFLAG_INT,f.medicineid_chr,b.DOSAGE_DEC  FROM T_AID_CONCERTRECIPEDETAIL a,T_BSE_CHARGEITEM b,T_AID_CONCERTRECIPE c ,T_BSE_USAGETYPE D,T_AID_RECIPEFREQ E,T_BSE_MEDICINE f WHERE a.dosetype_chr=d.usageid_chr(+)and a.freqid_chr=e.freqid_chr(+) and  a.ITEMID_CHR = b.ITEMID_CHR and a.RECIPEID_CHR = c.RECIPEID_CHR and trim(b.ITEMSRCID_VCHR)=trim(f.MEDICINEID_CHR(+)) and c.RECIPEID_CHR='" + strID + "' order by a.sort_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region ��ȡ���е���Ŀ����
        [AutoComplete]
        public long m_mthFindMedicine(out DataTable dt, string strType)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL;
            if (strType != null)
                strSQL = @"SELECT   itemid_chr, h.partname as partortypename_vchr, a.ITEMCHECKTYPE_CHR,a.itemsrcid_vchr, a.dosage_dec,
         a.dosageunit_chr,
         (SELECT k.typename_vchr
            FROM t_bse_chargeitemextype k
           WHERE k.typeid_chr = a.itemopinvtype_chr
             AND flag_int = 2) AS itemtype,
         a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
         a.itemwbcode_chr, a.itempycode_chr,
         CASE
            WHEN a.opchargeflg_int = 1
               THEN ROUND (a.itemprice_mny / a.packqty_dec, 4)
            WHEN a.opchargeflg_int = 0
               THEN a.itemprice_mny
         END AS submoney,
         CASE
            WHEN a.opchargeflg_int = 1
               THEN a.itemipunit_chr
            WHEN a.opchargeflg_int = 0
               THEN a.itemopunit_chr
         END AS itemopunit_chr,
         a.itemopunit_chr AS itemopunit, a.itemprice_mny, a.isrich_int,
         (SELECT precent_dec
            FROM t_aid_inschargeitem g
           WHERE g.itemid_chr = a.itemid_chr
             AND g.copayid_chr = '" + strType + @"') AS precent,
         a.itemopinvtype_chr, a.itemcatid_chr, a.selfdefine_int,
         a.itemcode_vchr, a.itemopcalctype_chr, b.noqtyflag_int,
         b.medicineid_chr, a.itemipunit_chr, a.opchargeflg_int, a.usageid_chr,
         c.usagename_vchr, d.groupid_chr
    FROM t_bse_chargeitem a,
         (SELECT groupid_chr, catid_chr
            FROM t_bse_chargecatmap
           WHERE internalflag_int = 0) d,
         t_bse_medicine b,
         t_bse_usagetype c,
         ar_apply_partlist h
   WHERE a.ifstop_int = 0
     AND a.itemopinvtype_chr = d.catid_chr(+)
     AND a.itemsrcid_vchr = b.medicineid_chr(+)
     AND a.usageid_chr = c.usageid_chr(+)
     AND a.itemchecktype_chr = h.partid(+)
ORDER BY itemcode_vchr";
            else
                strSQL = @"SELECT   itemid_chr, h.partname, a.itemsrcid_vchr, a.dosage_dec,
         a.dosageunit_chr,
         (SELECT k.typename_vchr
            FROM t_bse_chargeitemextype k
           WHERE k.typeid_chr = a.itemopinvtype_chr
             AND flag_int = 2) AS itemtype,
         a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
         a.itemwbcode_chr, a.itempycode_chr,
         CASE
            WHEN a.opchargeflg_int = 1
               THEN ROUND (a.itemprice_mny / a.packqty_dec, 4)
            WHEN a.opchargeflg_int = 0
               THEN a.itemprice_mny
         END AS submoney,
         CASE
            WHEN a.opchargeflg_int = 1
               THEN a.itemipunit_chr
            WHEN a.opchargeflg_int = 0
               THEN a.itemopunit_chr
         END AS itemopunit_chr,
         a.itemopunit_chr AS itemopunit, a.itemprice_mny, a.isrich_int,
         a.itemopinvtype_chr, a.itemcatid_chr, a.selfdefine_int,
         a.itemcode_vchr, a.itemopcalctype_chr, b.noqtyflag_int,
         b.medicineid_chr, a.itemipunit_chr, a.opchargeflg_int, a.usageid_chr,
         c.usagename_vchr, d.groupid_chr
    FROM t_bse_chargeitem a,
         (SELECT groupid_chr, catid_chr
            FROM t_bse_chargecatmap
           WHERE internalflag_int = 0) d,
         t_bse_medicine b,
         t_bse_usagetype c,
         ar_apply_partlist h
   WHERE a.ifstop_int = 0
     AND a.itemopinvtype_chr = d.catid_chr(+)
     AND a.itemsrcid_vchr = b.medicineid_chr(+)
     AND a.usageid_chr = c.usageid_chr(+)
     AND a.itemchecktype_chr = h.partid(+)
ORDER BY itemcode_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region ȡ�����������Ĳ���
        [AutoComplete]
        public long m_lngGetDeptByConcertreCipeID(string strReciptID, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.recipeid_chr,a.deptid_chr,c.deptname_vchr from t_aid_concertrecipedept a,t_aid_concertrecipe b,t_bse_deptdesc c where a.deptid_chr = c.deptid_chr and a.recipeid_chr = b.recipeid_chr and b.recipeid_chr='" + strReciptID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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

        #region �����÷�
        [AutoComplete]
        public long m_mthFindUsage(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select usageid_chr, usagename_vchr, usercode_chr, pycode_vchr, wbcode_vchr, scope_int, putmed_int, test_int, opusagedesc from t_bse_usagetype order by usercode_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region ����Ƶ��
        [AutoComplete]
        public long m_mthFindFrequency(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select FREQID_CHR,FREQNAME_CHR,USERCODE_CHR,DAYS_INT  from T_AID_RECIPEFREQ order by USERCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region �����Ը�����
        [AutoComplete]
        public long m_longPrecent(DataTable dt, out DataTable dt1, string payType)
        {
            dt1 = null;
            long lngRes = 0;
            string strSQL;
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                strSQL = @"select PRECENT_DEC  from T_AID_INSCHARGEITEM 
where itemid_chr='" + dt.Rows[i1]["ITEMID_CHR"].ToString() + "' and COPAYID_CHR='" + payType + "'";
                DataTable dt3 = new DataTable();
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt3);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt3.Rows.Count > 0)
                {
                    dt.Rows[i1]["precent"] = dt3.Rows[0][0].ToString() + "%";
                }
                else
                {
                    dt.Rows[i1]["precent"] = "100%";
                }
            }
            dt1 = dt;
            return lngRes;
        }
        #endregion

        #endregion

        #region  ������в�����Ϣ
        /// <summary>
        ///  ������в�����Ϣ
        /// </summary>
        [AutoComplete]
        public long m_lngGetDept(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string strSQL = @"select SHORTNO_CHR,DEPTNAME_VCHR,PYCODE_CHR,WBCODE_CHR,DEPTID_CHR from T_BSE_DEPTDESC where CATEGORY_INT=0 and (ATTRIBUTEID = '0000002' or ATTRIBUTEID ='0000001') and DEPTNAME_VCHR <> '����' and INPATIENTOROUTPATIENT_INT = 0  order by SHORTNO_CHR";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes < 0)
                    return lngRes;
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

        #region  ��ȡ���� �б�

        [AutoComplete]
        public long m_lngGetDeptList(out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;

            string strSQL = @"select SHORTNO_CHR,DEPTNAME_VCHR,PYCODE_CHR,WBCODE_CHR,DEPTID_CHR from T_BSE_DEPTDESC where (ATTRIBUTEID = '0000002' or ATTRIBUTEID ='0000001') and DEPTNAME_VCHR <> '����' order by SHORTNO_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��¼��ID</param>
        /// <param name="bt">������Ϣ</param>
        /// <param name="btDe">������ϸ��</param>
        /// <param name="btDetp">������Ϣ��</param>
        /// <param name="isDetp">�Ƿ���������1-�ǣ�0-����</param>
        /// <returns></returns>
        [AutoComplete]//(datarow ��Ϊ object[])
        public long m_lngAddNewConcertre(out string p_strRecordID, string[] bt, DataTable btDe, DataTable btDetp, string isDetp, int intFLAG)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            p_strRecordID = objHRPSvc.m_strGetNewID("T_AID_CONCERTRECIPE", "RECIPEID_CHR", 10);
            //('" + p_strRecordID + "', '" + bt.GetValue(1).ToString() + "',
            // " + bt.GetValue(3).ToString() + ",
            //'" + bt.GetValue(4).ToString() + "',
            //'" + bt.GetValue(6).ToString() + "',
            //'" + bt.GetValue(5).ToString() + "', 1,
            // '" + bt.GetValue(7).ToString() + "', " + intFLAG.ToString() + ",
            //'" + bt.GetValue(2).ToString() + "'
            //)
            string strSQL = @"INSERT INTO T_AID_CONCERTRECIPE (RECIPEID_CHR,RECIPENAME_CHR,PRIVILEGE_INT,USERCODE_CHR,PYCODE_CHR,WBCODE_CHR,STATUS_INT,CREATERID_CHR,FLAG_INT,DISEASENAME_VCHR, PECLUSCODE, PECLUSNAME) 
VALUES ('" + p_strRecordID + "','" + bt.GetValue(1).ToString() + "'," + bt.GetValue(3).ToString() + ",'" + bt.GetValue(4).ToString() + "','" + bt.GetValue(6).ToString() + "','" + bt.GetValue(5).ToString() + "',1,'" + bt.GetValue(7).ToString() + "'," + intFLAG.ToString() + ",'" + bt.GetValue(2).ToString() + "','" + bt.GetValue(8).ToString() + "','" + bt.GetValue(9).ToString() + "')";
            //�������Ӽ�¼
            try
            {

                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes == 1)
            {
                if (isDetp == "1" && btDetp.Rows.Count > 0)
                {
                    for (int f2 = 0; f2 < btDetp.Rows.Count; f2++)
                    {
                        if (btDetp.Rows[f2]["DEPTID_CHR"].ToString().Trim() != "")
                        {
                            strSQL = @"insert into T_AID_CONCERTRECIPEDEPT(RECIPEID_CHR,DEPTID_CHR) VALUES('" + p_strRecordID + "','" + btDetp.Rows[f2]["DEPTID_CHR"].ToString() + "')";
                            try
                            {

                                lngRes = objHRPSvc.DoExcute(strSQL);
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

                for (int i1 = 0; i1 < btDe.Rows.Count; i1++)
                {
                    string newID = "";
                    objHRPSvc.lngGenerateID(10, "DETAILID_CHR", "T_AID_CONCERTRECIPEDETAIL", out newID);
                    strSQL = "INSERT INTO T_AID_CONCERTRECIPEDETAIL (RECIPEID_CHR,DETAILID_CHR,ITEMID_CHR,QTY_DEC,DOSETYPE_CHR,FREQID_CHR,DOSAGEQTY_DEC,DAYS_INT,ROWNO_CHR,PARTORTYPE_VCHR,FLAG_INT,PARTORTYPENAME_VCHR,sort_int) VALUES ('" + p_strRecordID + "','" + newID + "','" + btDe.Rows[i1]["ITEMID_CHR"].ToString() + "'," + btDe.Rows[i1]["QTY_DEC"].ToString() + ",'" + btDe.Rows[i1]["DOSETYPE_CHR"].ToString() + "','" + btDe.Rows[i1]["FREQID_CHR"].ToString() + "'," + btDe.Rows[i1]["DOSAGEQTY_DEC"].ToString() + "," + btDe.Rows[i1]["DAYS_INT"].ToString() + ",'" + btDe.Rows[i1]["ROWNO_CHR"].ToString() + "','" + btDe.Rows[i1]["PARTORTYPE_VCHR"].ToString() + "'," + btDe.Rows[i1]["FLAG_INT"].ToString() + ",'" + btDe.Rows[i1]["PARTORTYPENAME_VCHR"].ToString() + "'," + btDe.Rows[i1]["sort_int"] + ")";
                    //�������Ӽ�¼
                    try
                    {

                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                }

            }
            return lngRes;
        }
        #endregion

        #region ɾ������
        [AutoComplete]
        public long m_lngDeleteConcertrecipeAndDe(string[] DeleRow, string[] DeleRowDe, string strItem, string strFlag)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (DeleRow != null)
            {
                strSQL = "delete from T_AID_CONCERTRECIPE where RECIPEID_CHR ='" + DeleRow.GetValue(0).ToString() + "'";

                try
                {

                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (lngRes == 1)
                {
                    strSQL = "delete from T_AID_CONCERTRECIPEDETAIL where RECIPEID_CHR ='" + DeleRow.GetValue(0).ToString() + "'";

                    try
                    {

                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
                else
                {

                }
                if (DeleRow.GetValue(0).ToString() == "2")
                {
                    strSQL = "delete from T_AID_CONCERTRECIPEDEPT where RECIPEID_CHR ='" + DeleRow.GetValue(0).ToString() + "'";
                    try
                    {

                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
            }
            if (DeleRowDe != null)
            {
                if (strItem == null)
                {
                    strSQL = "delete from T_AID_CONCERTRECIPEDETAIL where RECIPEID_CHR ='" + DeleRowDe.GetValue(0).ToString() + "' and DETAILID_CHR='" + DeleRowDe.GetValue(1).ToString() + "'";
                }
                else
                {
                    strSQL = "delete from T_AID_CONCERTRECIPEDETAIL where ITEMID_CHR ='" + strItem + "' and recipeid_chr IN (SELECT recipeid_chr FROM t_aid_concertrecipe WHERE flag_int =" + strFlag + ")";
                }
                try
                {

                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }

            return lngRes;
        }
        #endregion

        #region �޸Ĵ�����ϸ
        /// <summary>
        /// �޸Ĵ�����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">����ID</param>
        /// <param name="dtRow">�µ���ϸ����</param>
        /// <param name="oldITEMID">�ɵ���ϸ��ĿID����= null�޸ĵ�����¼����=NULL�����޸Ĵ�����ϸ����ͬ��Ŀ������ </param>
        /// <param name="strFLAG">��־ 0��Э������ 1���շ����</param>
        /// <param name="blIsPublic">�Ƿ��й���Ȩ��</param>
        /// <param name="CREATERID">������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConcertreCipeDetailModifyDe(string strID, string[] dtRow, string oldITEMID, string strFLAG, bool blIsPublic, string CREATERID, int m_intSort)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strwhere = " RECIPEID_CHR ='" + strID + "' and DETAILID_CHR='" + dtRow.GetValue(1).ToString() + "'";
            string strSQL = "";
            string strSQL1 = "";
            if (oldITEMID == null)
            {
                strSQL = @"update T_AID_CONCERTRECIPEDETAIL
				set
ITEMID_CHR='" + dtRow.GetValue(2).ToString() + @"',
QTY_DEC='" + dtRow.GetValue(3).ToString() + @"', 
DOSAGEQTY_DEC=" + dtRow.GetValue(6).ToString() + @", 
DOSETYPE_CHR='" + dtRow.GetValue(5).ToString() + @"', 
FREQID_CHR='" + dtRow.GetValue(4).ToString() + @"', 
DAYS_INT=" + dtRow.GetValue(7).ToString() + @",
ROWNO_CHR='" + dtRow.GetValue(8).ToString() + @"',FLAG_INT=" + dtRow.GetValue(10).ToString() + ",PARTORTYPE_VCHR='" + dtRow.GetValue(9).ToString() + "',PARTORTYPENAME_VCHR='" + dtRow.GetValue(11).ToString() + "',sort_int=" + m_intSort + "  where " + strwhere;
            }
            else
            {

                strSQL1 = @"update T_AID_CONCERTRECIPEDETAIL
				set
ITEMID_CHR='" + dtRow.GetValue(2).ToString() + @"',
QTY_DEC='" + dtRow.GetValue(3).ToString() + @"', 
DOSAGEQTY_DEC=" + dtRow.GetValue(6).ToString() + @", 
DOSETYPE_CHR='" + dtRow.GetValue(5).ToString() + @"', 
FREQID_CHR='" + dtRow.GetValue(4).ToString() + @"', 
DAYS_INT=" + dtRow.GetValue(7).ToString() + @",
ROWNO_CHR='" + dtRow.GetValue(8).ToString() + @"' ,FLAG_INT=" + dtRow.GetValue(10).ToString() + ",PARTORTYPE_VCHR='" + dtRow.GetValue(9).ToString() + "',PARTORTYPENAME_VCHR='" + dtRow.GetValue(11).ToString() + "',sort_int=" + m_intSort + "   where " + strwhere;
                if (blIsPublic)
                {
                    strwhere = "  ITEMID_CHR ='" + oldITEMID + "' and RECIPEID_CHR in (select  RECIPEID_CHR from t_aid_concertrecipe where FLAG_INT=" + strFLAG + ")";

                }
                else
                {
                    strwhere = "  ITEMID_CHR ='" + oldITEMID + "' and RECIPEID_CHR in (select  RECIPEID_CHR from t_aid_concertrecipe where FLAG_INT=" + strFLAG + " and CREATERID_CHR='" + CREATERID + "')";
                }
                strSQL = @"update T_AID_CONCERTRECIPEDETAIL
				set ITEMID_CHR='" + dtRow.GetValue(2).ToString() + @"' ,sort_int=" + m_intSort + "  where " + strwhere;
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL1);
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

        #region �޸�Э������
        [AutoComplete]//(datarow ��Ϊ object[])
        public long m_lngConcertreModify(string[] ModifiyRow, DataTable Deptbt)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"update  T_AID_CONCERTRECIPE
				set 
RECIPENAME_CHR='" + ModifiyRow.GetValue(1).ToString() + @"',
PRIVILEGE_INT='" + ModifiyRow.GetValue(3).ToString() + @"',
USERCODE_CHR='" + ModifiyRow.GetValue(4).ToString() + @"',
WBCODE_CHR='" + ModifiyRow.GetValue(5).ToString() + @"',
PYCODE_CHR='" + ModifiyRow.GetValue(6).ToString() + @"',
CREATERID_CHR='" + ModifiyRow.GetValue(7).ToString() + "',DISEASENAME_VCHR='" + ModifiyRow.GetValue(2).ToString() + @"',
PECLUSCODE = '" + ModifiyRow.GetValue(9).ToString() + "', PECLUSNAME = '" + ModifiyRow.GetValue(10).ToString() +
                 "'  where RECIPEID_CHR='" + ModifiyRow.GetValue(0).ToString() + "'"
                ;
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"delete T_AID_CONCERTRECIPEDEPT where RECIPEID_CHR='" + ModifiyRow.GetValue(0).ToString() + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (Deptbt != null && Deptbt.Rows.Count > 0)
            {
                for (int f2 = 0; f2 < Deptbt.Rows.Count; f2++)
                {
                    if (Deptbt.Rows[f2]["DEPTID_CHR"].ToString().Trim() != "")
                    {
                        strSQL = @"insert into T_AID_CONCERTRECIPEDEPT(RECIPEID_CHR,DEPTID_CHR) VALUES('" + ModifiyRow.GetValue(0).ToString() + "','" + Deptbt.Rows[f2]["DEPTID_CHR"].ToString() + "')";
                        try
                        {

                            lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region ������ϸ
        [AutoComplete]
        public long m_lngConcertreCipeDetailAddNEWDe(string strID, string[] btDe, int m_intSort)
        {
            long lngRes = 0;
            string newID;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.lngGenerateID(10, "DETAILID_CHR", "T_AID_CONCERTRECIPEDETAIL", out newID); ;
            string strSQL = @"INSERT INTO T_AID_CONCERTRECIPEDETAIL (RECIPEID_CHR,DETAILID_CHR,ITEMID_CHR,QTY_DEC,DOSETYPE_CHR,FREQID_CHR,DOSAGEQTY_DEC,ROWNO_CHR,DAYS_INT,PARTORTYPE_VCHR,FLAG_INT,PARTORTYPENAME_VCHR,sort_int) 
            VALUES ('" + strID + "','" + newID + "','" + btDe.GetValue(2).ToString() + "'," + btDe.GetValue(3).ToString() + ",'" + btDe.GetValue(5).ToString() + "','" + btDe.GetValue(4).ToString() + "'," + btDe.GetValue(6).ToString() + ",'" + btDe.GetValue(8).ToString() + "'," + btDe.GetValue(7).ToString() + ",'" + btDe.GetValue(9).ToString() + "'," + btDe.GetValue(10).ToString() + ",'" + btDe.GetValue(11).ToString() + "'," + m_intSort + ")";
            try
            {
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


        //		#endregion

        #region ���������շ���Ŀά��

        #region ������еĲ�������
        /// <summary>
        /// ������еĲ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="btPatientPayType"></param>
        [AutoComplete]
        public long m_lngGetAllPatientPayType(out DataTable btPatientPayType)
        {
            long lngRes = 0;
            btPatientPayType = new DataTable();
            string strSQL = @"select PAYTYPENO_VCHR,PAYTYPENAME_VCHR,case when ISUSING_NUM=0 then 'ͣ��' when ISUSING_NUM=1 then '����' end as strISUSING,MEMO_VCHR,PAYTYPEID_CHR from t_bse_patientpaytype order by PAYTYPENO_VCHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref btPatientPayType);
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

        #region ������Ŀ��ϸ
        /// <summary>
        /// ������Ŀ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPayTypeID"></param>
        /// <param name="strItemId"></param>
        /// <param name="intQty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewItem(string strPayTypeID, string strItemId, int intQty, int REGISTER, int RECIPEFLAG, int EXPERT)
        {
            long lngRes = 0;
            if (lngRes < 0)
                return lngRes;
            string strSQL = "INSERT INTO t_aid_chargepaytype(PAYTYPEID_CHR,ITEMID_CHR,QTY_DEC,REGISTER_INT,RECIPEFLAG_INT,EXPERT_INT) VALUES ('" + strPayTypeID + "','" + strItemId + "'," + intQty + "," + REGISTER + "," + RECIPEFLAG + "," + EXPERT + ")";
            //�������Ӽ�¼
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

        #region ɾ����Ŀ
        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPayTypeID"></param>
        /// <param name="strItemId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleItem(string strPayTypeID, string strItemId)
        {
            long lngRes = 0;
            if (lngRes < 0)
                return lngRes;
            string strSQL = "Delete t_aid_chargepaytype where PAYTYPEID_CHR='" + strPayTypeID + "' and ITEMID_CHR='" + strItemId + "'";
            //�������Ӽ�¼
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

        #region �޸���Ŀ����
        /// <summary>
        ///�޸���Ŀ���� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPayTypeID"></param>
        /// <param name="strOldItemId"></param>
        /// <param name="strItemId"></param>
        /// <param name="intQty"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyItem(string strPayTypeID, string strOldItemId, string strItemId, int intQty, int REGISTER, int RECIPEFLAG, int EXPERT)
        {
            long lngRes = 0;
            if (lngRes < 0)
                return lngRes;
            string strSQL = "update  t_aid_chargepaytype set ITEMID_CHR='" + strItemId + "',QTY_DEC=" + intQty + ",REGISTER_INT=" + REGISTER + ",RECIPEFLAG_INT=" + RECIPEFLAG + ",EXPERT_INT=" + EXPERT + " where PAYTYPEID_CHR='" + strPayTypeID + "' and ITEMID_CHR='" + strOldItemId + "'";
            //�޸���Ŀ����
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

        #region ���ݲ�������ID��ȡ��Ŀ����
        /// <summary>
        /// ���ݲ�������ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPayTypeID"></param>
        /// <param name="bt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemByPayID(string strPayTypeID, out DataTable bt)
        {
            long lngRes = 0;
            bt = new DataTable();
            if (lngRes < 0)
                return lngRes;
            string strSQL = "select a.paytypeid_chr, a.itemid_chr, a.qty_dec, a.register_int, a.recipeflag_int, a.expert_int,b.itemname_vchr,b.itemcode_vchr,b.itemopunit_chr,b.itemspec_vchr,b.itemprice_mny,case when b.isrich_int=1 then '��' when b.isrich_int=0 then '��' end as strisrich,case when b.itemcatid_chr='0002' then '��ҩ' when b.itemcatid_chr='0003' then '����' when b.itemcatid_chr='0004' then '����' when b.itemcatid_chr='0005' then '����' when b.itemcatid_chr='0006' then '����' when b.itemcatid_chr='0001' then '��ҩ' end as itemtype,case when register_int=0 then 'ȫ��' when  register_int=1 then '�ѹҺ�' else 'δ�Һ�' end as register,case when recipeflag_int=0 then 'ȫ��' when recipeflag_int=1 then '����' else '����' end as recipeflag,case when expert_int=0 then 'ȫ��' when expert_int=1 then 'ר��' else '��ͨ' end as  expert from t_aid_chargepaytype a,t_bse_chargeitem b where a.paytypeid_chr='" + strPayTypeID + "' and a.itemid_chr=b.itemid_chr";
            //�������Ӽ�¼
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref bt);
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

        #region  ��鵱ǰ��½���û��Ƿ��б༭���ô�����Ȩ��
        /// <summary>
        /// ��鵱ǰ��½���û��Ƿ��б༭���ô�����Ȩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">�û�ID</param>
        /// <param name="isPublic">false-û��Ȩ��,true-��Ȩ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPublic(string strID, out bool isPublic)
        {
            isPublic = false;
            long lngRes = 0;
            if (lngRes < 0)
                return lngRes;
            string strSQL = "select b.roleid_chr,b.name_vchr,b.desc_vchr,b.deptid_chr from t_sys_emprolemap a,t_sys_role b where  a.roleid_chr=b.roleid_chr and a.empid_chr='" + strID + @"' and b.NAME_VCHR='�༭����Э������'";
            DataTable bt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref bt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (bt.Rows.Count > 0 && bt.Rows[0]["NAME_VCHR"].ToString() == "�༭����Э������")
            {
                isPublic = true;
            }
            return lngRes;
        }
        #endregion

        #region ����Ա��IDȡ������
        [AutoComplete]
        public long m_lngGetConcertreCipeByEmpID(string strEmptID, out clsConcertrectpe_VO[] p_objResultArr)
        {
            p_objResultArr = new clsConcertrectpe_VO[0];
            long lngRes = 0;
            //			string strSQL = @"SELECT a.*,b.LASTNAME_VCHR FROM T_AID_CONCERTRECIPE a,T_BSE_EMPLOYEE b WHERE a.CREATERID_CHR=b.EMPID_CHR and a.CREATERID_CHR='"+strEmptID+@"'
            //								union all
            //								select a.*,d.LASTNAME_VCHR from T_AID_CONCERTRECIPE a,T_AID_CONCERTRECIPEDEPT b,T_BSE_DEPTEMP c,T_BSE_EMPLOYEE d
            //								where a.RECIPEID_CHR=b.RECIPEID_CHR(+) and b.DEPTID_CHR=c.DEPTID_CHR(+) and c.EMPID_CHR='"+strEmptID+"' and c.EMPID_CHR=d.EMPID_CHR(+)";
            string strSQL = @"select a.recipeid_chr, a.recipename_chr, a.privilege_int, a.usercode_chr, a.wbcode_chr, a.pycode_chr, a.status_int, a.createrid_chr, a.flag_int, a.diseasename_vchr,b.lastname_vchr from t_aid_concertrecipe a ,t_bse_employee b where a.createrid_chr=b.empid_chr and a.privilege_int =0 and a.status_int =1
union
select a.recipeid_chr, a.recipename_chr, a.privilege_int, a.usercode_chr, a.wbcode_chr, a.pycode_chr, a.status_int, a.createrid_chr, a.flag_int, a.diseasename_vchr,b.lastname_vchr from t_aid_concertrecipe a,t_bse_employee b where a.createrid_chr=b.empid_chr  and a.privilege_int =1 and
 a.createrid_chr ='" + strEmptID + @"' and a.status_int =1
 union
 select aa.recipeid_chr, aa.recipename_chr, aa.privilege_int, aa.usercode_chr, aa.wbcode_chr, aa.pycode_chr, aa.status_int, aa.createrid_chr, aa.flag_int, aa.diseasename_vchr,cc.lastname_vchr from t_aid_concertrecipe aa,
 ( select a.recipeid_chr from t_aid_concertrecipedept a where a.deptid_chr
 in (select deptid_chr from t_bse_deptemp where empid_chr ='" + strEmptID + @"')) bb,t_bse_employee cc
  where aa.recipeid_chr(+) =bb.recipeid_chr and aa.createrid_chr=cc.empid_chr and aa.privilege_int =2 
  and aa.status_int =1";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsConcertrectpe_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsConcertrectpe_VO();
                        p_objResultArr[i1].m_strRECIPEID_CHR = dtbResult.Rows[i1]["RECIPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRECIPENAME_CHR = dtbResult.Rows[i1]["RECIPENAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPRIVILEGE_INT = Convert.ToInt32(dtbResult.Rows[i1]["PRIVILEGE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strCREATERID_CHR = dtbResult.Rows[i1]["CREATERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].clsEmployee_VO.m_strLASTNAME_VCHR = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
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

        #region ���ݴ���IDȡ��������ϸ
        [AutoComplete]
        public long m_lngGetConcertreCipeDetailByID(string strID, out clsConcertrecipeDetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsConcertrecipeDetail_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.recipeid_chr, a.detailid_chr, a.itemid_chr, a.qty_dec, a.freqid_chr, a.dosetype_chr, a.dosageqty_dec, a.days_int, a.rowno_chr, a.partortype_vchr, a.flag_int, a.partortypename_vchr, a.sort_int,b.itemname_vchr,b.itemcatid_chr,b.itemspec_vchr,b.itemopunit_chr,b.itemprice_mny ,d.usagename_vchr,e.freqname_chr from t_aid_concertrecipedetail a,t_bse_chargeitem b,t_aid_concertrecipe c ,t_bse_usagetype d,t_aid_recipefreq e where a.dosetype_chr=d.usageid_chr(+)and a.freqid_chr=e.freqid_chr(+) and  a.itemid_chr = b.itemid_chr and a.recipeid_chr = c.recipeid_chr and c.createrid_chr='" + strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsConcertrecipeDetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsConcertrecipeDetail_VO();
                        p_objResultArr[i1].m_strRECIPEID_CHR = dtbResult.Rows[i1]["RECIPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDETAILID_CHR = dtbResult.Rows[i1]["DETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strITEMID_CHR = dtbResult.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQTY_DEC = dtbResult.Rows[i1]["QTY_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageID = dtbResult.Rows[i1]["DOSETYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUsageName = dtbResult.Rows[i1]["USAGENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFrequencyID = dtbResult.Rows[i1]["FREQID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFrequencyName = dtbResult.Rows[i1]["FREQNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsChargeItem_VO.m_strItemName = dtbResult.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsChargeItem_VO.m_strINSURANCEID_CHR = dtbResult.Rows[i1]["ITEMCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsChargeItem_VO.m_strItemSpec = dtbResult.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsChargeItem_VO.m_strItemCode = dtbResult.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsChargeItem_VO.m_fltItemPrice = float.Parse(dtbResult.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
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

        #region ȡ�����Ŵ���
        [AutoComplete]
        public long m_lngGetConcertreCipeDeptByID(string strReciptID, out clsConcertrecipeDept_VO[] p_objResultArr)
        {
            p_objResultArr = new clsConcertrecipeDept_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.*,c.DEPTNAME_VCHR FROM T_AID_CONCERTRECIPEDEPT a,T_AID_CONCERTRECIPE b,T_BSE_DEPTDESC c WHERE a.DEPTID_CHR = c.DEPTID_CHR and a.RECIPEID_CHR = b.RECIPEID_CHR and b.CREATERID_CHR='" + strReciptID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsConcertrecipeDept_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsConcertrecipeDept_VO();
                        p_objResultArr[i1].m_strRECIPEID_CHR = dtbResult.Rows[i1]["RECIPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_clsDepart_VO.m_strDEPTNAME_VCHR = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
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
    /// <summary>
    /// ��Ʊ��ѯϵͳ
    /// </summary>
    public class clsChargeCheckSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsChargeCheckSvc()
        {
        }

        #region ��ѯ��Ʊ����
        /// <summary>
        /// ����ʱ��λ�ȡ��Ʊ����
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strDateStart"></param>
        /// <param name="strDateEnd"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByDate(string strDateStart, string strDateEnd, out DataTable dt, bool p_blnOnlySelectVip, bool isWechatRePrt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select d.patientcardid_chr,
                                   a.invoiceno_vchr,
                                   r.repprninvono_vchr,
                                   a.seqid_chr,
                                   c.paytypename_vchr as internaltype,
                                   a.patientname_chr,
                                   b.sex_chr,
                                   case 
                                     when a.paytype_int=0 then
                                      '�ֽ�'
                                     when a.paytype_int=1 then
                                      '���п�'
                                     when a.paytype_int=2 then
                                      '֧Ʊ'
                                     when a.paytype_int=3 then
                                      'IC��'
                                     when a.paytype_int=5 then
                                      '΢��2'
                                     when a.paytype_int=6 then
                                      '֧����'
                                     when a.paytype_int=8 then
                                      '΢��'
                                     when a.paytype_int=9 then
                                      '֧����'
                                   end as paytypename,
                                   a.invdate_dat,
                                   case
                                     when a.status_int = 1 then
                                      '����'
                                     when a.status_int = 2 then
                                      '��Ʊ'
                                     when a.status_int = 3 then
                                      '�ָ�'
                                   end as statusname,
                                   a.deptname_chr,
                                   h.lastname_vchr as doctorname_chr,
                                   case
                                     when a.balanceflag_int = 0 then
                                      'δ���� '
                                     when a.balanceflag_int = 1 then
                                      '�ѽ���'
                                   end as balancename,
                                   a.recorddate_dat,
                                   e.empno_chr as opremp_chr,
                                   f.empno_chr as balanceemp_chr,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.totalsum_mny,
                                   a.outpatrecipeid_chr,
                                   a.seqid_chr,
                                   b.employer_vchr,                                   
                                   round(a.totaldiffcost_mny, 2),
                                   (a.totalsum_mny - a.totaldiffcost_mny) as totalpay
                              from t_opr_outpatientrecipeinv a,
                                   t_bse_patient             b,
                                   t_bse_patientpaytype      c,
                                   t_bse_patientcard         d,
                                   t_bse_employee            e,
                                   t_bse_employee            f,
                                   t_bse_employee            h,
                                   t_opr_invoicerepeatprint  r 
                             where a.recorddate_dat between to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and a.patientid_chr = b.patientid_chr
                               and a.paytypeid_chr = c.paytypeid_chr
                               and a.patientid_chr = d.patientid_chr
                               and a.opremp_chr = e.empid_chr
                               and a.balanceemp_chr = f.empid_chr(+)                            
                               and a.doctorid_chr = h.empid_chr
                               and a.seqid_chr = r.seqid_chr(+) 
                            ";
            if (p_blnOnlySelectVip == true)
            {
                strSQL = strSQL + " and a.isvouchers_int = 2";
            }
            if (isWechatRePrt)
            {
                string sub = @" and a.seqid_chr in 
                                (select a.seqid_chr
                                  from t_opr_outpatientrecipeinv a, t_opr_invoicerepeatprint b
                                 where a.seqid_chr = b.seqid_chr
                                   and b.type_chr = '1'
                                   and a.opremp_chr = '0000000'
                                   and (a.recorddate_dat between to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')))";
                sub = string.Format(sub, strDateStart + " 00:00:00", strDateEnd + " 23:59:59");
                strSQL += sub;
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                strSQL = string.Format(strSQL, strDateStart + " 00:00:00", strDateEnd + " 23:59:59");
                lngRes = svc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        /// ���ݲ����ֶθ���ѯ���ݻ�ȡ��Ʊ����  add by liuyingrui
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByCondition(string[] m_strArr, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select d.patientcardid_chr,
                                       a.invoiceno_vchr,
                                       a.seqid_chr,
                                       c.paytypename_vchr as internaltype,
                                       a.patientname_chr,
                                       b.sex_chr,
                                       case 
                                         when a.paytype_int=0 then
                                          '�ֽ�'
                                         when a.paytype_int=1 then
                                          '���п�'
                                         when a.paytype_int=2 then
                                          '֧Ʊ'
                                         when a.paytype_int=3 then
                                          'IC��'
                                         when a.paytype_int=5 then
                                          '΢��2'
                                         when a.paytype_int=6 then
                                          '֧����'
                                         when a.paytype_int=8 then
                                          '΢��'
                                         when a.paytype_int=9 then
                                          '֧����'
                                       end as paytypename,
                                       a.invdate_dat,
                                       case
                                         when a.status_int = 1 then
                                          '����'
                                         when a.status_int = 2 then
                                          '��Ʊ'
                                         when a.status_int = 3 then
                                          '�ָ�'
                                       end as statusname,
                                       a.deptname_chr,
                                       h.lastname_vchr as doctorname_chr,
                                       case
                                         when a.balanceflag_int = 0 then
                                          'δ���� '
                                         when a.balanceflag_int = 1 then
                                          '�ѽ���'
                                       end as balancename,
                                       a.recorddate_dat,
                                       e.empno_chr as opremp_chr,
                                       f.empno_chr as balanceemp_chr,
                                       a.acctsum_mny,
                                       a.sbsum_mny,
                                       a.totalsum_mny,
                                       a.outpatrecipeid_chr,
                                       a.seqid_chr,
                                       b.employer_vchr
                                  from t_opr_outpatientrecipeinv a,
                                       t_bse_patient             b,
                                       t_bse_patientpaytype      c,
                                       t_bse_patientcard         d,
                                       t_bse_employee            e,
                                       t_bse_employee            f,
                                       t_bse_employee            h
                                 where a.patientid_chr = b.patientid_chr
                                   and a.paytypeid_chr = c.paytypeid_chr
                                   and a.patientid_chr = d.patientid_chr
                                   and a.opremp_chr = e.empid_chr
                                   and a.balanceemp_chr = f.empid_chr(+)
                                   and a.doctorid_chr = h.empid_chr
                                ";
            try
            {

                int m_intStatus = -1;
                switch (m_strArr[0].Trim())
                {
                    case "���ƿ���": strSQL += "and d.PATIENTCARDID_CHR='" + m_strArr[1].Trim() + "'"; break;
                    case "��Ʊ���": strSQL += "and a.INVOICENO_VCHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "�������": strSQL += "and c.PAYTYPENAME_VCHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "��������": strSQL += "and a.PATIENTNAME_CHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "֧������":
                        if (m_strArr[1].Trim() == "�ֽ�")
                        {
                            m_intStatus = 0;
                        }
                        else if (m_strArr[1].Trim() == "���п�")
                        {
                            m_intStatus = 1;
                        }
                        else if (m_strArr[1].Trim() == "֧Ʊ")
                        {
                            m_intStatus = 2;
                        }
                        else if (m_strArr[1].Trim() == "IC��")
                        {
                            m_intStatus = 3;
                        }
                        strSQL += "and a.PAYTYPE_INT=" + m_intStatus + ""; break;
                    case "��Ʊ����": strSQL += "and a.INVDATE_DAT=to_date('" + m_strArr[1].Trim() + "','yyyy-mm-dd hh24:mi:ss')"; break;
                    case "��Ʊ״̬":
                        if (m_strArr[1].Trim() == "����")
                        {
                            m_intStatus = 1;
                        }
                        else if (m_strArr[1].Trim() == "��Ʊ")
                        {
                            m_intStatus = 2;
                        }
                        else if (m_strArr[1].Trim() == "�ָ�")
                        {
                            m_intStatus = 3;
                        }
                        strSQL += "and a.STATUS_INT=" + m_intStatus + ""; break;
                    case "��������": strSQL += "and a.DEPTNAME_CHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "ҽ������": strSQL += "and h.LASTNAME_VCHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "�ɷ�״̬":
                        if (m_strArr[1].Trim() == "δ����")
                        {
                            m_intStatus = 0;
                        }
                        else if (m_strArr[1].Trim() == "�ѽ���")
                        {
                            m_intStatus = 1;
                        }
                        strSQL += "and a.BALANCEFLAG_INT=" + m_intStatus + ""; break;
                    case "�շ�Ա": strSQL += "and e.EMPNO_CHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "����Ա": strSQL += "and f.EMPNO_CHR like '%" + m_strArr[1].Trim() + "%'"; break;
                    case "��¼ʱ��": strSQL += "and a.RECORDDATE_DAT between to_date('" + m_strArr[1] + " 00:00:00'" + ",'yyyy-mm-dd hh24:mi:ss') and to_date('" + m_strArr[1] + " 23:59:59'" + ",'yyyy-mm-dd hh24:mi:ss') "; break;
                    default: break;

                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        /// ���ݲ����ֶθ���ѯ�����Ѿ�����Ա(�շ�ԱID)��ȡ��Ʊ����  add by liuyingrui
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="m_strArr"></param>
        /// <param name="m_strEmpID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByEmpID(string[] m_strArr, string m_strEmpID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select d.patientcardid_chr,a.invoiceno_vchr ,a.seqid_chr,
                                     c.paytypename_vchr as internaltype,a.patientname_chr,
                                     b.sex_chr,case when a.paytype_int=0 then '�ֽ�' when a.paytype_int=1 then '���п�' when a.paytype_int=2 then '֧Ʊ' when a.paytype_int=3 then 'IC��' when a.paytype_int=5 then '΢��2' when a.paytype_int=6 then '֧����' when a.paytype_int=8 then '΢��' when a.paytype_int=9 then '֧����' end as paytypename,
                                     a.invdate_dat,case when a.status_int=1 then '����' when a.status_int=2 then '��Ʊ' when a.status_int=3 then '�ָ�' end as statusname, 
                                     a.deptname_chr,h.lastname_vchr as doctorname_chr,case when a.balanceflag_int=0 then 'δ���� ' when a.balanceflag_int=1 then '�ѽ���' end as balancename,
                                     a.recorddate_dat,e.empno_chr as opremp_chr,f.empno_chr as balanceemp_chr,a.acctsum_mny,a.sbsum_mny,a.totalsum_mny ,a.outpatrecipeid_chr,a.seqid_chr ,b.employer_vchr,
                                     a.totaldiffcost_mny,(a.totalsum_mny-a.totaldiffcost_mny) as totalpay
                                from t_opr_outpatientrecipeinv a,
                                     t_bse_patient b,
                                     t_bse_patientpaytype c,
                                     t_bse_patientcard d,
                                     t_bse_employee e,
                                     t_bse_employee f,
                                     t_bse_employee h 
                               where a.patientid_chr = b.patientid_chr
                                 and a.paytypeid_chr = c.paytypeid_chr
                                 and a.patientid_chr = d.patientid_chr
                                 and a.opremp_chr = e.empid_chr
                                 and a.balanceemp_chr = f.empid_chr(+)
                                 and a.doctorid_chr = h.empid_chr 
                                 and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                 and (a.opremp_chr = '" + m_strEmpID + "' or a.recordemp_chr = '" + m_strEmpID + @"') ";

            try
            {

                int m_intStatus = -1;
                switch (m_strArr[0].Trim())
                {
                    case "���ƿ���": strSQL += "and d.patientcardid_chr='" + m_strArr[1].Trim() + "'"; break;
                    case "��Ʊ���": strSQL += "and a.invoiceno_vchr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "�������": strSQL += "and c.paytypename_vchr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "��������": strSQL += "and a.patientname_chr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "֧������":
                        if (m_strArr[1].Trim() == "�ֽ�")
                        {
                            m_intStatus = 0;
                        }
                        else if (m_strArr[1].Trim() == "���п�")
                        {
                            m_intStatus = 1;
                        }
                        else if (m_strArr[1].Trim() == "֧Ʊ")
                        {
                            m_intStatus = 2;
                        }
                        else if (m_strArr[1].Trim() == "IC��")
                        {
                            m_intStatus = 3;
                        }
                        else if (m_strArr[1].Trim() == "΢��2")
                        {
                            m_intStatus = 5;
                        }
                        else if (m_strArr[1].Trim() == "֧����")
                        {
                            m_intStatus = 6;
                        }
                        else if (m_strArr[1].Trim() == "΢��")
                        {
                            m_intStatus = 8;
                        }
                        else if (m_strArr[1].Trim() == "֧����")
                        {
                            m_intStatus = 9;
                        }
                        strSQL += "and a.paytype_int=" + m_intStatus + ""; break;
                    case "��Ʊ����": strSQL += "and a.invdate_dat=to_date('" + m_strArr[1].Trim() + "','yyyy-mm-dd hh24:mi:ss')"; break;
                    case "��Ʊ״̬":
                        if (m_strArr[1].Trim() == "����")
                        {
                            m_intStatus = 1;
                        }
                        else if (m_strArr[1].Trim() == "��Ʊ")
                        {
                            m_intStatus = 2;
                        }
                        else if (m_strArr[1].Trim() == "�ָ�")
                        {
                            m_intStatus = 3;
                        }
                        strSQL += "and a.status_int=" + m_intStatus + ""; break;
                    case "��������": strSQL += "and a.deptname_chr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "ҽ������": strSQL += "and h.lastname_vchr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "�ɷ�״̬":
                        if (m_strArr[1].Trim() == "δ����")
                        {
                            m_intStatus = 0;
                        }
                        else if (m_strArr[1].Trim() == "�ѽ���")
                        {
                            m_intStatus = 1;
                        }
                        strSQL += "and a.balanceflag_int=" + m_intStatus + ""; break;
                    case "�շ�Ա": strSQL += "and e.empno_chr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "����Ա": strSQL += "and f.empno_chr like'%" + m_strArr[1].Trim() + "%'"; break;
                    case "��¼ʱ��": strSQL += "and a.recorddate_dat between to_date('" + m_strArr[1] + " 00:00:00'" + ",'yyyy-mm-dd hh24:mi:ss') and to_date('" + m_strArr[1] + " 23:59:59'" + ",'yyyy-mm-dd hh24:mi:ss') "; break;
                    default: break;

                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();

                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        /// ���ݲ���Ա(�շ�Ա)ID��ʱ��β�ѯ��Ʊ��Ϣ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="empid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByempid(string strDateStart, string strDateEnd, string empid, out DataTable dt, bool p_blnOnlySelectVip, bool isWechatRePrt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select d.patientcardid_chr,a.invoiceno_vchr, r.repprninvono_vchr, a.seqid_chr,
                                     c.paytypename_vchr as internaltype,a.patientname_chr,
                                     b.sex_chr,case when a.paytype_int=0 then '�ֽ�' when a.paytype_int=1 then '���п�' when a.paytype_int=2 then '֧Ʊ' when a.paytype_int=3 then 'IC��' when a.paytype_int=5 then '΢��2' when a.paytype_int=6 then '֧����' when a.paytype_int=8 then '΢��' when a.paytype_int=9 then '֧����' end as paytypename,
                                     a.invdate_dat,case when a.status_int=1 then '����' when a.status_int=2 then '��Ʊ' when a.status_int=3 then '�ָ�' end as statusname, 
                                     a.deptname_chr,h.lastname_vchr as doctorname_chr,case when a.balanceflag_int=0 then 'δ���� ' when a.balanceflag_int=1 then '�ѽ���' end as balancename,
                                     a.recorddate_dat,e.empno_chr as opremp_chr,f.empno_chr as balanceemp_chr,a.acctsum_mny,a.sbsum_mny,a.totalsum_mny ,a.outpatrecipeid_chr,a.seqid_chr ,b.employer_vchr, 
                                     round(a.totaldiffcost_mny,2),(a.totalsum_mny-a.totaldiffcost_mny) as totalpay
                                from t_opr_outpatientrecipeinv a,
                                     t_bse_patient b,
                                     t_bse_patientpaytype c,
                                     t_bse_patientcard d,
                                     t_bse_employee e,
                                     t_bse_employee f,
                                     t_bse_employee h,
                                     t_opr_invoicerepeatprint r 
                               where a.patientid_chr = b.patientid_chr
                                 and a.paytypeid_chr = c.paytypeid_chr
                                 and a.patientid_chr = d.patientid_chr
                                 and a.opremp_chr = e.empid_chr
                                 and a.balanceemp_chr = f.empid_chr(+)
                                 and a.doctorid_chr = h.empid_chr 
                                 and (a.isvouchers_int <> 2 or a.isvouchers_int is null) 
                                 and a.seqid_chr = r.seqid_chr(+)
                                 and (a.opremp_chr = '" + empid + "' or a.recordemp_chr = '" + empid + @"')
                                 and a.recorddate_dat between to_date('" + strDateStart + " 00:00:00'" + ",'yyyy-mm-dd hh24:mi:ss') and to_date('" + strDateEnd + " 23:59:59'" + ",'yyyy-mm-dd hh24:mi:ss')";
            if (p_blnOnlySelectVip == true)
            {
                strSQL = strSQL + " and a.isvouchers_int = 2";
            }
            if (isWechatRePrt)
            {
                string sub = @" and a.seqid_chr in 
                                (select a.seqid_chr
                                  from t_opr_outpatientrecipeinv a, t_opr_invoicerepeatprint b
                                 where a.seqid_chr = b.seqid_chr
                                   and b.type_chr = '1'
                                   and a.opremp_chr = '0000000'
                                   and (a.recorddate_dat between to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')))";
                sub = string.Format(sub, strDateStart + " 00:00:00", strDateEnd + " 23:59:59");
                strSQL += sub;
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region �޸ķ�Ʊ���շ�����
        #region ������ϸ
        [AutoComplete]
        public long m_lngModifiyType(string strType, string strINVOICENO, string strSEQID, string modifiyMan)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "update t_opr_outpatientrecipeinv set PAYTYPE_INT=" + strType + " where INVOICENO_VCHR='" + strINVOICENO + "' and SEQID_CHR='" + strSEQID + "'";
            try
            {
                #region д��ۼ���¼
                clsRecordMark_VO Markvo = new clsRecordMark_VO();
                //clsRecordMark recordMark = new clsRecordMark();
                Markvo.m_strOPERATORID_CHR = modifiyMan;
                Markvo.m_strTABLESEQID_CHR = "1";
                Markvo.m_strRECORDDETAIL_VCHR = strSQL;
                //recordMark.m_mthAddNewRecord(Markvo);
                #endregion
                lngRes = objHRPSvc.DoExcute(strSQL);

                strSQL = "update t_opr_payment set paytype_int = " + strType + " where chargeno_vchr = '" + strSEQID + "'";
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
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="blIS">������ 0--�� 1--��ʱ��false ��true �ǣ������� 1--�� 0--��ʱ��false �ǣ�true ��</param>
        /// <param name="strsetid">����ID��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCollocate(out bool blIS, string strsetid)
        {
            long lngRes = 0;
            blIS = false;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select setstatus_int from t_sys_setting where  setid_chr='" + strsetid + "'";
            DataTable dt = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["setstatus_int"].ToString()) == 1)
            {
                blIS = true;
            }
            return lngRes;
        }

        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// by huafeng.xiao
        /// 2009��9��15��9:31:13
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStatus">����״̬���ʺ϶�״̬����ʹ��</param>
        /// <param name="strsetid">����ID��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCollocateStatus(out string p_strStatus, string strsetid)
        {
            long lngRes = 0;
            p_strStatus = string.Empty;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select setstatus_int from t_sys_setting where  setid_chr='" + strsetid + "'";
            DataTable dt = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                p_strStatus = dt.Rows[0]["setstatus_int"].ToString();
            }
            return lngRes;
        }

        #endregion

        /// <summary>
        /// ���ݴ����Ż�ȡ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="recipeNO">������</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeDate(string recipeNO, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            #region old bak
            //            string strSQL = @"select D.NAME,D.DEC,D.COUNT,D.PRICE,d.pdcarea_vchr,d.UINT,C.DOCTORNAME_CHR,C.sbsum_mny,C.ACCTSUM_MNY,e.LASTNAME_VCHR  From t_opr_outpatientrecipeinv C,
            //				(select A.OUTPATRECIPEID_CHR ID,B.itemname_vchr NAME, a.UNITID_CHR   UINT,
            //				B.ITEMSPEC_VCHR DEC,A.TOLQTY_DEC COUNT,b.PDCAREA_VCHR ,A.UNITPRICE_MNY PRICE
            //				from t_opr_outpatientpwmrecipede A,t_bse_chargeitem B
            //				where A.ITEMID_CHR=B.itemid_chr(+)
            //				union all
            //				select A.OUTPATRECIPEID_CHR ID,B.itemname_vchr NAME, a.UNITID_CHR   UINT,
            //				B.ITEMSPEC_VCHR DEC,A.QTY_DEC COUNT,b.PDCAREA_VCHR ,A.UNITPRICE_MNY PRICE
            //				from t_opr_outpatientcmrecipede A,t_bse_chargeitem B
            //				where A.ITEMID_CHR=B.itemid_chr(+)
            //				union all
            //				select A.OUTPATRECIPEID_CHR ID,B.itemname_vchr NAME,b.ITEMUNIT_CHR   UINT,
            //				B.ITEMSPEC_VCHR DEC,A.QTY_DEC COUNT,b.PDCAREA_VCHR ,A.PRICE_MNY PRICE
            //				from t_opr_outpatientchkrecipede A,t_bse_chargeitem B
            //				where A.ITEMID_CHR=B.itemid_chr(+)
            //				union all
            //				select A.OUTPATRECIPEID_CHR ID,B.itemname_vchr NAME,b.ITEMUNIT_CHR   UINT,
            //				B.ITEMSPEC_VCHR DEC,A.QTY_DEC COUNT,b.PDCAREA_VCHR ,A.PRICE_MNY PRICE
            //				from t_opr_outpatienttestrecipede A,t_bse_chargeitem B
            //				where A.ITEMID_CHR=B.itemid_chr(+)
            //				UNION ALL
            //				SELECT a.outpatrecipeid_chr ID, b.itemname_vchr NAME,b.ITEMUNIT_CHR   UINT,
            //					b.itemspec_vchr DEC, a.qty_dec COUNT,b.PDCAREA_VCHR, a.price_mny price
            //				FROM t_opr_outpatientopsrecipede a, t_bse_chargeitem b
            //				WHERE a.itemid_chr = b.itemid_chr(+)
            //				union  all
            //				select A.OUTPATRECIPEID_CHR ID,B.itemname_vchr NAME, a.UNITID_CHR  UINT,
            //				B.ITEMSPEC_VCHR DEC,A.QTY_DEC COUNT,b.PDCAREA_VCHR ,A.UNITPRICE_MNY PRICE
            //				from t_opr_outpatientothrecipede A,t_bse_chargeitem B
            //				where A.ITEMID_CHR=B.itemid_chr(+)) D,t_bse_employee e
            //				where  c.OPREMP_CHR = e.EMPID_CHR(+) and C.OUTPATRECIPEID_CHR=D.ID(+)
            //				AND C.SEQID_CHR= '" + recipeNO + "'";
            #endregion

            #region new
            string strSQLNew = @"
                            select d.name, d.dec, d.count, d.price, d.pdcarea_vchr, d.uint,
                                   c.doctorname_chr, c.sbsum_mny, c.acctsum_mny, e.lastname_vchr,d.toldiffprice_mny
                              from t_opr_outpatientrecipeinv c,
                                   (select a.outpatrecipeid_chr id, a.itemname_vchr name,
                                           a.unitid_chr uint, a.itemspec_vchr dec, a.tolqty_dec count,
                                           b.pdcarea_vchr, a.unitprice_mny price,a.toldiffprice_mny
                                      from t_opr_outpatientpwmrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)
                                    union all
                                    select a.outpatrecipeid_chr id, b.itemname_vchr name,
                                           a.unitid_chr uint, a.itemspec_vchr dec, a.qty_dec*a.times_int count,
                                           b.pdcarea_vchr, a.unitprice_mny price,a.toldiffprice_mny
                                      from t_opr_outpatientcmrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)
                                    union all
                                    select a.outpatrecipeid_chr id, a.itemname_vchr name,
                                           b.itemunit_chr uint, a.itemspec_vchr dec, a.qty_dec count,
                                           b.pdcarea_vchr, a.price_mny price,0 as toldiffprice_mny
                                      from t_opr_outpatientchkrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)
                                    union all
                                    select a.outpatrecipeid_chr id, a.itemname_vchr name,
                                           b.itemunit_chr uint, a.itemspec_vchr dec, a.qty_dec count,
                                           b.pdcarea_vchr, a.price_mny price,0 as toldiffprice_mny
                                      from t_opr_outpatienttestrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)
                                    union all
                                    select a.outpatrecipeid_chr id, a.itemname_vchr name,
                                           b.itemunit_chr uint, a.itemspec_vchr dec, a.qty_dec count,
                                           b.pdcarea_vchr, a.price_mny price,0 as toldiffprice_mny
                                      from t_opr_outpatientopsrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)
                                    union all
                                    select a.outpatrecipeid_chr id, a.itemname_vchr name,
                                           a.unitid_chr uint, a.itemspec_vchr dec, a.qty_dec count,
                                           b.pdcarea_vchr, a.unitprice_mny price,a.toldiffprice_mny
                                      from t_opr_outpatientothrecipede a, t_bse_chargeitem b
                                     where a.itemid_chr = b.itemid_chr(+)) d,
                                   t_bse_employee e
                             where c.opremp_chr = e.empid_chr(+)
                               and c.outpatrecipeid_chr = d.id(+)
                               and c.seqid_chr = '" + recipeNO + "'";
            #endregion

            string Sql = string.Empty;

            Sql = @"select d.name,
       d.dec,
       d.count,
       d.price,
       d.pdcarea_vchr,
       d.uint,
       c.doctorname_chr,
       c.sbsum_mny,
       c.acctsum_mny,
       e.lastname_vchr,
       d.toldiffprice_mny,
       d.buyprice
  from t_opr_outpatientrecipeinv c
  left join (select distinct a.outpatrecipeid_chr id,
                             a.itemname_vchr      name,
                             a.unitid_chr         uint,
                             a.itemspec_vchr      dec,
                             a.tolqty_dec         count,
                             b.pdcarea_vchr,
                             a.unitprice_mny      price,
                             a.toldiffprice_mny,
                             p.buyprice_dec       as buyprice,
                             a.rowno_chr
               from t_opr_outpatientpwmrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
               left join t_opr_oprecipeitemde p
                 on a.outpatrecipeid_chr = p.outpatrecipeid_chr
                and a.itemid_chr = p.itemid_chr
             union all
             select a.outpatrecipeid_chr id,
                    b.itemname_vchr name,
                    a.unitid_chr uint,
                    a.itemspec_vchr dec,
                    a.qty_dec * a.times_int count,
                    b.pdcarea_vchr,
                    a.unitprice_mny price,
                    a.toldiffprice_mny,
                    0 as buyprice,
                    a.rowno_chr
               from t_opr_outpatientcmrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
               left join t_opr_oprecipeitemde c
                 on a.outpatrecipeid_chr = c.outpatrecipeid_chr
                and a.itemid_chr = c.itemid_chr
             union all
             select a.outpatrecipeid_chr id,
                    a.itemname_vchr      name,
                    b.itemunit_chr       uint,
                    a.itemspec_vchr      dec,
                    a.qty_dec            count,
                    b.pdcarea_vchr,
                    a.price_mny          price,
                    0                    as toldiffprice_mny,
                    0                    as buyprice,
                    a.rowno_chr
               from t_opr_outpatientchkrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
             union all
             select a.outpatrecipeid_chr id,
                    a.itemname_vchr      name,
                    b.itemunit_chr       uint,
                    a.itemspec_vchr      dec,
                    a.qty_dec            count,
                    b.pdcarea_vchr,
                    a.price_mny          price,
                    0                    as toldiffprice_mny,
                    0                    as buyprice,
                    a.rowno_chr
               from t_opr_outpatienttestrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
             union all
             select a.outpatrecipeid_chr id,
                    a.itemname_vchr      name,
                    b.itemunit_chr       uint,
                    a.itemspec_vchr      dec,
                    a.qty_dec            count,
                    b.pdcarea_vchr,
                    a.price_mny          price,
                    0                    as toldiffprice_mny,
                    0                    as buyprice,
                    a.rowno_chr
               from t_opr_outpatientopsrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
             union all
             select distinct a.outpatrecipeid_chr id,
                             a.itemname_vchr      name,
                             a.unitid_chr         uint,
                             a.itemspec_vchr      dec,
                             a.qty_dec            count,
                             b.pdcarea_vchr,
                             a.unitprice_mny      price,
                             a.toldiffprice_mny,
                             p.buyprice_dec       as buyprice,
                             a.rowno_chr
               from t_opr_outpatientothrecipede a
               left join t_bse_chargeitem b
                 on a.itemid_chr = b.itemid_chr
               left join (select distinct p2.itemid_chr, p2.buyprice_dec
                           from t_opr_outpatientrecipeinv p1
                          inner join t_opr_oprecipeitemde p2
                             on p1.outpatrecipeid_chr =
                                p2.outpatrecipeid_chr
                          where p1.seqid_chr = ?) p
                 on a.itemid_chr = p.itemid_chr) d
    on c.outpatrecipeid_chr = d.id
  left join t_bse_employee e
    on c.opremp_chr = e.empid_chr
 where c.seqid_chr = ?

                    ";

            try
            {
                IDataParameter[] parm = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = recipeNO;
                parm[1].Value = recipeNO;               
                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                //lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQLNew, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);

            }
            return lngRes;
        }

        #region ��ȡ����֤����Ϣ
        /// <summary>
        /// ��ȡ����֤����Ϣ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="recipeNO">������</param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetPatientCertificateInfo(string strID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select   decode (b.internalflag_int,
               5, c.insuranceid_vchr,
               2, c.insuranceid_vchr,
               3, c.difficulty_vchr,
               ''
              ) as certificateno
  from t_opr_outpatientrecipeinv a, t_bse_patientpaytype b, t_bse_patient c
 where a.paytypeid_chr = b.paytypeid_chr
   and a.patientid_chr = c.patientid_chr(+)
   and b.internalflag_int > 0
   and a.seqid_chr ='" + strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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
        /// ����ʱ��λ�ȡ��Ʊ����
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strDateStart"></param>
        /// <param name="strDateEnd"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByDate1(string strDateStart, string strDateEnd, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select d.patientcardid_chr,a.invoiceno_vchr ,a.outpatrecipeid_chr,c.paytypename_vchr as internaltype,a.patientname_chr,b.sex_chr,case when a.paytype_int=0 then '�ֽ�' when a.paytype_int=1 then 'ˢ��' when a.paytype_int=3 then '֧Ʊ' when a.paytype_int=5 then '΢��2' when a.paytype_int=6 then '֧����' when a.paytype_int=8 then '΢��' when a.paytype_int=9 then '֧����' end as paytypename,a.invdate_dat,a.deptname_chr,h.lastname_vchr as doctorname_chr,e.empno_chr as opremp_chr,a.acctsum_mny,a.sbsum_mny,a.totalsum_mny,case when c.internalflag_int=0 then '�Է�' when c.internalflag_int=1 then '����' when c.internalflag_int=2 then 'ҽ��' when c.internalflag_int>2 then '����' end as patientype  from t_opr_outpatientrecipeinv a ,t_bse_patient b,t_bse_patientpaytype c,t_bse_patientcard d,t_bse_employee e,t_bse_employee h  where a.recorddate_dat  between to_date('" + strDateStart + " 00:00:00'" + ",'yyyy-mm-dd hh24:mi:ss') and to_date('" + strDateEnd + " 23:59:59'" + ",'yyyy-mm-dd hh24:mi:ss') and a.patientid_chr=b.patientid_chr and a.paytypeid_chr=c.paytypeid_chr and a.patientid_chr=d.patientid_chr and a.opremp_chr=e.empid_chr and a.doctorid_chr=h.empid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);

            }
            return lngRes;
        }

        #region �����ڲ���Ż�ȡ��Ʊ����¼��Ϣ
        /// <summary>
        /// �����ڲ���Ż�ȡ��Ʊ����¼��Ϣ
        /// </summary>
        /// <param name="seqid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByseqid(string seqid, out DataTable dtRecord)
        {
            long lngRes = 0;
            string SQL = @"select invoiceno_vchr,
       outpatrecipeid_chr,
       invdate_dat,
       acctsum_mny,
       sbsum_mny,
       opremp_chr,
       recordemp_chr,
       recorddate_dat,
       status_int,
       seqid_chr,
       balanceemp_chr,
       balance_dat,
       balanceflag_int,
       totalsum_mny,
       paytype_int,
       patientid_chr,
       patientname_chr,
       deptid_chr,
       deptname_chr,
       doctorid_chr,
       doctorname_chr,
       confirmemp_chr,
       paytypeid_chr,
       internalflag_int,
       baseseqid_chr,
       groupid_chr,
       confirmdeptid_chr,
       split_int,
       regno_chr,
       chargedeptid_chr from t_opr_outpatientrecipeinv where seqid_chr = '" + seqid + "'";

            dtRecord = new DataTable();

            try
            {
                clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
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
        /// ��ȡ��Ʊ������ϸ
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="strINVOICENO"></param>
        /// <param name="strSEQID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeDe(string strINVOICENO, string strSEQID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"select b.typeid_chr,b.typename_vchr,a.tolfee_mny from t_opr_outpatientrecipeinvde a,t_bse_chargeitemextype b where a.invoiceno_vchr='" + strINVOICENO + "' and a.seqid_chr='" + strSEQID + "' and a.itemcatid_chr=b.typeid_chr and b.flag_int=2";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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



    /// <summary>
    /// �����շ��Ż� 2004-8-6 �ƹ�ƽ
    /// </summary>
    public class clsRegisterDetailSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsRegisterDetailSvc()
        { }
        /// <summary>
        /// �������ݵĺ���
        /// </summary>
        /// <param name="objPri"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngLoadData(out DataTable dt)
        {
            dt = new DataTable();
            string strSQL = @"select a.registertypeid_chr,a.chargeid_chr,a.paytypeid_chr,a.payment_mny,a.discount_dec, 
                            b.registertypename_vchr,c.chargename_chr,d.paytypename_vchr from 
							t_bse_registerdetail a, t_bse_registertype b,t_bse_registerchargetype c,
							t_bse_patientpaytype d 
							where a.registertypeid_chr=b.registertypeid_chr(+) and
							a.chargeid_chr=c.chargeid_chr(+) and
							a.paytypeid_chr=d.paytypeid_chr and b.isusing_num=1 and c.isusing_num = 1 and d.isusing_num=1 order by a.registertypeid_chr,a.chargeid_chr,a.paytypeid_chr";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            return HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

        }
        [AutoComplete]
        public long m_lngSave(string ID1, string ID2, string ID3, string PAYMENT_MNY, string DISCOUNT_DEC)
        {
            string strSQL = @"update t_bse_registerdetail set payment_mny ='" + PAYMENT_MNY + "',discount_dec='" + DISCOUNT_DEC + @"' where
registertypeid_chr='" + ID1 + "' and chargeid_chr ='" + ID2 + "'and paytypeid_chr ='" + ID3 + "'";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
            return HRPSvc.DoExcute(strSQL);
        }
    }
}
