using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;
using System.Collections.Generic;


namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    ///
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPutMedRequisitionSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���ɰ�ҩ��ϸ
        /// <summary>
        /// ���ɰ�ҩ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrRegisterId"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long CreatePutMedDetail(List<string> arrRegisterId, string operatorId)
        {
            long ret = 0;

            clsT_Bih_Opr_Putmeddetail_VO[] putMedDt;
            for (int i1 = 0; i1 < arrRegisterId.Count; i1++)
            {
                //����סԺ�Ǽ�Id 
                ret = GetPutMedDetailByRegisterId(arrRegisterId[i1], out putMedDt);

                if (ret > 0 && putMedDt.Length > 0)
                {
                    int n = 0;
                    Hashtable htPutmedDetail = new Hashtable();
                    for (int i2 = 0; i2 < putMedDt.Length; i2++)
                    {
                        putMedDt[i2].m_strCREATOR_CHR = operatorId;

                        if (putMedDt[i2].m_intITEMSRCTYPE_INT == 1 || putMedDt[i2].m_intITEMSRCTYPE_INT == 2)
                        {
                            //������ҩ��ϸ����¼
                            //ret = AddNewPutMedDetail(p_objPrincipal, putMedDt[i2]);
                            //if (ret < 0)
                            //    return ret;
                            htPutmedDetail.Add(n, putMedDt[i2]);
                            n++;
                        }

                        //���²��˷�����ϸ��
                        //ret = UpdatePatientCharge(p_objPrincipal, putMedDt[i2].m_strPCHARGEID_CHR, operatorId);
                        //if (ret < 0)
                        //    return ret;

                        //����ҽ��ִ�е�
                        //ret = UpdateOrderExecute(p_objPrincipal, putMedDt[i2].m_strORDEREXECID_CHR);
                        //if (ret < 0)
                        //    return ret;
                    }

                    //������ҩ��ϸ����¼
                    if (htPutmedDetail.Count > 0)
                    {
                        ret = AddNewPutMedDetail(htPutmedDetail);
                        if (ret < 0)
                            return ret;
                    }

                    //���²��˷�����ϸ��
                    ret = UpdatePatientCharge(putMedDt, operatorId);
                    if (ret < 0)
                        return ret;

                    //����ҽ��ִ�е�
                    ret = UpdateOrderExecute(putMedDt);
                    if (ret < 0)
                        return ret;

                }
            }
            return ret;
        }

        #endregion

        #region ���ɰ�ҩ��ϸ(������)
        /// <summary>
        /// ���ɰ�ҩ��ϸ(������)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrRegisterId"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long CreatePutMedDetailForRecipe(List<string> arrRegisterId, string operatorId)
        {
            long ret = 0;

            clsT_Bih_Opr_Putmeddetail_VO[] putMedDt;
            for (int i1 = 0; i1 < arrRegisterId.Count; i1++)
            {
                //����סԺ�Ǽ�Id 
                ret = GetRecipeDetailByRegisterId(arrRegisterId[i1], out putMedDt);

                if (ret > 0 && putMedDt.Length > 0)
                {
                    for (int i2 = 0; i2 < putMedDt.Length; i2++)
                    {
                        putMedDt[i2].m_strCREATOR_CHR = operatorId;

                        if (putMedDt[i2].m_intITEMSRCTYPE_INT == 1)
                        {
                            //������ҩ��ϸ����¼
                            ret = AddNewPutMedDetail(putMedDt[i2]);
                            if (ret < 0)
                                return ret;
                        }

                        //���²��˷�����ϸ��
                        ret = UpdatePatientCharge(putMedDt[i2].m_strPCHARGEID_CHR, operatorId);
                        if (ret < 0)
                            return ret;

                        //����ҽ��ִ�е�
                        //ret = UpdateOrderExecute(  putMedDt[i2].m_strORDEREXECID_CHR);
                        //if (ret < 0)
                        //    return ret;
                    }
                }
            }
            return ret;
        }

        #endregion

        #region ���ɰ�ҩ��ϸ(���ݲ�ͬ���ʵĲ�����)
        /// <summary>
        /// ���ɰ�ҩ��ϸ(���ݲ�ͬ���ʵĲ�����)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrRegisterId"></param>
        /// <param name="operatorId"></param>
        /// <param name="dptType">1 �������ҵ�����Ϊ�ǲ��� 2 ��������Ϊ���� </param>
        /// <returns></returns>
        [AutoComplete]
        public long CreatePutMedDetailByDptType(List<string> arrRegisterId, string operatorId, int dptType)
        {
            long ret = 0;

            clsT_Bih_Opr_Putmeddetail_VO[] putMedDt;
            for (int i1 = 0; i1 < arrRegisterId.Count; i1++)
            {
                //����סԺ�Ǽ�Id 
                if (dptType == 1)
                {
                    ret = GetCDRecipeDetailByRegisterId(arrRegisterId[i1], out putMedDt);
                }
                else
                {
                    ret = GetRecipeDetailByRegisterId(arrRegisterId[i1], out putMedDt);
                }

                if (ret > 0 && putMedDt.Length > 0)
                {
                    for (int i2 = 0; i2 < putMedDt.Length; i2++)
                    {
                        putMedDt[i2].m_strCREATOR_CHR = operatorId;

                        if (putMedDt[i2].m_intITEMSRCTYPE_INT == 1)
                        {
                            //������ҩ��ϸ����¼
                            ret = AddNewPutMedDetail(putMedDt[i2]);
                            if (ret < 0)
                                return ret;
                        }

                        //���²��˷�����ϸ��
                        ret = UpdatePatientCharge(putMedDt[i2].m_strPCHARGEID_CHR, operatorId);
                        if (ret < 0)
                            return ret;

                        //����ҽ��ִ�е�
                        //ret = UpdateOrderExecute(p_objPrincipal, putMedDt[i2].m_strORDEREXECID_CHR);
                        //if (ret < 0)
                        //    return ret;
                    }
                }
            }
            return ret;
        }

        #endregion

        #region ��ѯ�Ƿ������в��˶��ѷ���
        /// <summary>
        /// ��ѯ�Ƿ������в��˶��ѷ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        [AutoComplete]
        public long IsAllPatSend(string areaId, out bool ret)
        {
            long lngRes = 0;
            ret = false;
            string strSQL = @"select a.PCHARGEID_CHR,
                                     a.CREATEAREA_CHR,
                                     a.PATIENTID_CHR,
                                     a.ORDEREXECID_CHR,
                                     a.ORDEREXECTYPE_INT,
                                     a.CHARGEITEMID_CHR,
                                     a.ISRICH_INT,
                                     b.itemname_vchr,
                                     b.itemsrctype_int,
                                     c.RECIPENO_INT,
                                     c.ORDERID_CHR,
                                     c.DOSAGE_DEC,
                                     c.DOSAGEUNIT_CHR,
                                     c.DOSETYPEID_CHR,
                                     c.EXECFREQID_CHR,
                                     c.GETUNIT_CHR,
                                     d.EXECUTETIME_INT,
                                     d.EXECUTEDAYS_INT,
                                     d.EXECUTEDATE_VCHR
                                from 
                                T_Opr_Bih_PatientCharge a,
                                t_bse_chargeitem b,
                                T_Opr_Bih_Order c,
                                T_OPR_BIH_ORDEREXECUTE d,
                                T_OPR_BIH_REGISTER e
                                where a.chargeitemid_chr = b.itemid_chr and
                                      a.registerid_chr = c.registerid_chr and
                                      a.ORDEREXECID_CHR= d.orderexecid_chr and
                                      c.orderid_chr = d.orderid_chr and
                                      a.registerid_chr = e.registerid_chr and
                                     d.ISINCEPT_INT = 0 and 
                                     e.AREAID_CHR = ? ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = areaId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    ret = false;
                }
                else
                {
                    ret = true;
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

        #region ��ѯȫ��������ϱ�־
        /// <summary>
        /// ��ѯȫ��������ϱ�־
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAreaComplete(string areaId, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = new DataTable();

            string strSQL = @"select * from T_OPR_BIH_AREAPUTMEDRECORD
                              where STATUS_INT = 1 and ISPUT_INT = 0 and trunc(PUT_DAT) = trunc(sysdate) and AREAID_CHR = '" + areaId + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
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

        #region ��ȫ��������ϱ�־
        /// <summary>
        /// ��ȫ��������ϱ�־
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="operatorId"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long SetAreaComplete(string areaId, string operatorId, string operatorName)
        {
            long lngReg = 0;
            int areaSeq;

            lngReg = InsertAreaPutMedRecord(areaId, operatorId, operatorName, out areaSeq);
            if (lngReg > 0)
            {
                lngReg = UpdatePutMedDetailByAreaId(areaId, areaSeq);
            }
            return lngReg;
        }
        #endregion

        #region ȡ��ȫ��������ϱ�־
        /// <summary>
        /// ȡ��ȫ��������ϱ�־
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long CancelAreaComplete(string areaId, string operatorId, string operatorName)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					SET STATUS_INT = 0, CANCELERID_CHR = '" + operatorId + @"',CANCELER_VCHR = '" + operatorName + @"'
				    WHERE STATUS_INT = 1 and  trunc(PUT_DAT) = trunc(sysdate) and AREAID_CHR = '" + areaId.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                if (lngRes > 0)
                {
                    lngRes = UpdatePutMedDetailByAreaId(areaId, 0);
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

        #region ������ҩ��ϸ��
        /// <summary>
        /// ������ҩ��ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        private long AddNewPutMedDetail(clsT_Bih_Opr_Putmeddetail_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //lngRes = objHRPSvc.lngGenerateID(18, "PUTMEDDETAILID_CHR", "T_Bih_Opr_PutMedDetail", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            //string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"INSERT INTO T_Bih_Opr_PutMedDetail 
                (PUTMEDDETAILID_CHR, AREAID_CHR, PAIENTID_CHR, REGISTERID_CHR, ORDERID_CHR,
                 ORDEREXECID_CHR, ORDEREXECTYPE_INT, RECIPENO_INT, DOSAGE_DEC, DOSAGEUNIT_VCHR,
                 CHARGEITEMID_CHR, MEDID_CHR,MEDNAME_VCHR, ISRICH_INT,DOSETYPEID_CHR, 
                 EXECFREQID_CHR, EXECTIMES_INT, EXECDAYS_INT, UNITPRICE_MNY, UNIT_VCHR, 
                 GET_DEC, PCHARGEID_CHR, CREATOR_CHR, CREATE_DAT, ISPUT_INT,
                 PUTTYPE_INT, PUTMEDREQID_CHR, EXECTIME_VCHR, NEEDCONFIRM_INT, ACTIVATETYPE_INT, 
                 ISRECRUIT_INT, OUTGETMEDDAYS_INT, MEDICNETYPE_INT, PUTMEDTYPE_INT, BEDID_CHR) 
                VALUES (lpad(SEQ_PutMedDetail.Nextval,18,'0'), ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, SYSDATE, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ? )";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(33, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                //objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[0].Value = p_objRecord.m_strAREAID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strPAIENTID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strORDERID_CHR;

                objLisAddItemRefArr[4].Value = p_objRecord.m_strORDEREXECID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_intORDEREXECTYPE_INT;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intRECIPENO_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_dblDOSAGE_DEC;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strDOSAGEUNIT_VCHR;

                objLisAddItemRefArr[9].Value = p_objRecord.m_strCHARGEITEMID_CHR;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strMEDID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strMEDNAME_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intISRICH_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strDOSETYPEID_CHR;

                objLisAddItemRefArr[14].Value = p_objRecord.m_strEXECFREQID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_intEXECTIMES_INT;
                objLisAddItemRefArr[16].Value = p_objRecord.m_intEXECDAYS_INT;
                objLisAddItemRefArr[17].Value = p_objRecord.m_dblUNITPRICE_MNY;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strUNIT_VCHR;

                objLisAddItemRefArr[19].Value = p_objRecord.m_dblGET_DEC;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strPCHARGEID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCREATOR_CHR;
                //objLisAddItemRefArr[23].Value = DateTime.Parse(strDateTime);//p_objRecord.m_strCREATE_DAT
                objLisAddItemRefArr[22].Value = 0; //p_objRecord.m_intISPUT_INT;

                objLisAddItemRefArr[23].Value = p_objRecord.m_intPUTTYPE_INT;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strPUTMEDREQID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strEXECTIME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_intNEEDCONFIRM_INT;
                objLisAddItemRefArr[27].Value = p_objRecord.m_intACTIVATETYPE_INT;

                objLisAddItemRefArr[28].Value = p_objRecord.m_intISRECRUIT_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_intOUTGETMEDDAYS_INT;
                objLisAddItemRefArr[30].Value = p_objRecord.m_intMEDICNETYPE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intPUTMEDTYPE_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strBedID;

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

        #region ������ҩ��ϸ��(������
        /// <summary>
        /// ������ҩ��ϸ��(������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_htPutmedDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        private long AddNewPutMedDetail(Hashtable p_htPutmedDetail)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //lngRes = objHRPSvc.lngGenerateID(18, "PUTMEDDETAILID_CHR", "T_Bih_Opr_PutMedDetail", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;

            //string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"INSERT INTO T_Bih_Opr_PutMedDetail 
                (PUTMEDDETAILID_CHR, AREAID_CHR, PAIENTID_CHR, REGISTERID_CHR, ORDERID_CHR,
                 ORDEREXECID_CHR, ORDEREXECTYPE_INT, RECIPENO_INT, DOSAGE_DEC, DOSAGEUNIT_VCHR,
                 CHARGEITEMID_CHR, MEDID_CHR,MEDNAME_VCHR, ISRICH_INT,DOSETYPEID_CHR, 
                 EXECFREQID_CHR, EXECTIMES_INT, EXECDAYS_INT, UNITPRICE_MNY, UNIT_VCHR, 
                 GET_DEC, PCHARGEID_CHR, CREATOR_CHR, CREATE_DAT, ISPUT_INT,
                 PUTTYPE_INT, PUTMEDREQID_CHR, EXECTIME_VCHR, NEEDCONFIRM_INT, ACTIVATETYPE_INT, 
                 ISRECRUIT_INT, OUTGETMEDDAYS_INT, MEDICNETYPE_INT, PUTMEDTYPE_INT, BEDID_CHR) 
                VALUES (lpad(SEQ_PutMedDetail.Nextval,18,'0'), ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, SYSDATE, ?,
                        ?, ?, ?, ?, ?,
                        ?, ?, ?, ?, ? )";
            try
            {

                DbType[] dbTypes = new DbType[] {
                    DbType.String, DbType.String, DbType.String, DbType.String,
                    DbType.String, DbType.Int16, DbType.Int16, DbType.Decimal, DbType.String,
                    DbType.String, DbType.String, DbType.String, DbType.Int16, DbType.String,
                    DbType.String, DbType.Int16, DbType.Int16, DbType.Decimal, DbType.String,
                    DbType.Decimal, DbType.String, DbType.String, DbType.Int16,
                    DbType.Int16, DbType.String, DbType.String, DbType.Int16, DbType.Int16,
                    DbType.Int16, DbType.Int16, DbType.Int16, DbType.Int16,DbType.String
                    };
                object[][] objValues = new object[33][];



                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_htPutmedDetail.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < p_htPutmedDetail.Count; k1++)
                {

                    objValues[0][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strAREAID_CHR;
                    objValues[1][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strPAIENTID_CHR;
                    objValues[2][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strREGISTERID_CHR;
                    objValues[3][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strORDERID_CHR;

                    objValues[4][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strORDEREXECID_CHR;
                    objValues[5][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intORDEREXECTYPE_INT;
                    objValues[6][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intRECIPENO_INT;
                    objValues[7][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_dblDOSAGE_DEC;
                    objValues[8][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strDOSAGEUNIT_VCHR;

                    objValues[9][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strCHARGEITEMID_CHR;
                    objValues[10][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strMEDID_CHR;
                    objValues[11][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strMEDNAME_VCHR;
                    objValues[12][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intISRICH_INT;
                    objValues[13][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strDOSETYPEID_CHR;

                    objValues[14][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strEXECFREQID_CHR;
                    objValues[15][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intEXECTIMES_INT;
                    objValues[16][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intEXECDAYS_INT;
                    objValues[17][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_dblUNITPRICE_MNY;
                    objValues[18][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strUNIT_VCHR;

                    objValues[19][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_dblGET_DEC;
                    objValues[20][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strPCHARGEID_CHR;
                    objValues[21][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strCREATOR_CHR;
                    objValues[22][k1] = 0; //p_objRecord.m_intISPUT_INT;

                    objValues[23][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intPUTTYPE_INT;
                    objValues[24][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strPUTMEDREQID_CHR;
                    objValues[25][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strEXECTIME_VCHR;
                    objValues[26][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intNEEDCONFIRM_INT;
                    objValues[27][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intACTIVATETYPE_INT;

                    objValues[28][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intISRECRUIT_INT;
                    objValues[29][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intOUTGETMEDDAYS_INT;
                    objValues[30][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intMEDICNETYPE_INT;
                    objValues[31][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_intPUTMEDTYPE_INT;
                    objValues[32][k1] = ((clsT_Bih_Opr_Putmeddetail_VO)p_htPutmedDetail[k1]).m_strBedID;

                }

                if (p_htPutmedDetail.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ����סԺ�Ǽ�Id���ɰ�ҩ��ϸ
        /// <summary>
        /// ����סԺ�Ǽ�Id���ɰ�ҩ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long GetPutMedDetailByRegisterId(string p_strRegisterId, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            string strSQL = @"select a.PCHARGEID_CHR,
                                     d.EXEAREAID_CHR CREATEAREA_CHR,
                                     d.EXEBEDID_CHR CURBEDID_CHR,
                                     a.PATIENTID_CHR,
                                     a.ORDEREXECID_CHR,
                                     a.ORDEREXECTYPE_INT,
                                     a.CHARGEITEMID_CHR,
                                     a.ISRICH_INT,
                                     a.NEEDCONFIRM_INT,
                                     a.ACTIVATETYPE_INT,
                                     a.AMOUNT_DEC,
                                     a.UNITPRICE_DEC,
                                     b.ITEMSRCID_VCHR,
                                     b.itemname_vchr,
                                     b.itemsrctype_int,
                                     c.RECIPENO2_INT as RECIPENO_INT,
                                     c.ORDERID_CHR,
                                     c.DOSAGE_DEC,
                                     c.DOSAGEUNIT_CHR,
                                     c.DOSETYPEID_CHR,
                                     c.EXECFREQID_CHR,
                                     c.GET_DEC,
                                     c.GETUNIT_CHR,
                                     c.CURAREAID_CHR,
                                     c.CREATEAREAID_CHR,
                                     c.OUTGETMEDDAYS_INT,
                                     c.SOURCETYPE_INT,
                                     d.EXECUTETIME_INT,
                                     d.EXECUTEDAYS_INT,
                                     d.EXECUTEDATE_VCHR,
                                     d.ISRECRUIT_INT,
                                     e.MEDICNETYPE_INT,
                                     e.PUTMEDTYPE_INT
                                from 
                                T_Opr_Bih_PatientCharge a,
                                t_bse_chargeitem b,
                                T_Opr_Bih_Order c,
                                T_OPR_BIH_ORDEREXECUTE d,
                                T_BSE_MEDICINE e
                                where a.chargeitemid_chr = b.itemid_chr and
                                      a.registerid_chr = c.registerid_chr and
                                      a.ORDEREXECID_CHR= d.orderexecid_chr and
                                      c.orderid_chr = d.orderid_chr and
                                      b.itemsrcid_vchr = e.MEDICINEID_CHR(+) and
                                     a.AMOUNT_DEC > 0 and
                                     a.PSTATUS_INT = '1' and
                                     a.STATUS_INT = '1' and
                                     d.ISINCEPT_INT = 0 and 
                                     ((d.NEEDCONFIRM_INT = 0) or (d.NEEDCONFIRM_INT = 1 and d.CONFIRM_DAT is not null) ) and
                                     a.registerid_chr = ? ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_strRegisterId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        //p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["SOURCETYPE_INT"].ToString().Trim() == "0")
                        {
                            p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["CURAREAID_CHR"].ToString().Trim();
                        }
                        else
                        {
                            p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["CREATEAREAID_CHR"].ToString().Trim();
                        }

                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["CURBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = p_strRegisterId;
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["itemname_vchr"].ToString().Trim();

                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["DOSETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();

                        try
                        {
                            p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECUTETIME_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intEXECDAYS_INT = Int32.Parse(dtbResult.Rows[i1]["EXECUTEDAYS_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["GETUNIT_CHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECUTEDATE_VCHR"].ToString().Trim();

                        try
                        {
                            p_objResultArr[i1].m_intNEEDCONFIRM_INT = Int32.Parse(dtbResult.Rows[i1]["NEEDCONFIRM_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["itemsrctype_int"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intISRECRUIT_INT = Int32.Parse(dtbResult.Rows[i1]["ISRECRUIT_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intOUTGETMEDDAYS_INT = Int32.Parse(dtbResult.Rows[i1]["OUTGETMEDDAYS_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intPUTMEDTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["PUTMEDTYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intMEDICNETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["MEDICNETYPE_INT"].ToString().Trim());
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
        #endregion

        #region ����סԺ�Ǽ�Id���ɲ����ʰ�ҩ��ϸ
        /// <summary>
        /// ����סԺ�Ǽ�Id���ɴ���ҩ��ҩ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long GetRecipeDetailByRegisterId(string p_strRegisterId, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            string strSQL = @"select a.PCHARGEID_CHR,
                                     a.CREATEAREA_CHR,
                                     a.CURBEDID_CHR,
                                     a.PATIENTID_CHR,
                                     a.ORDERID_CHR,
                                     a.ORDEREXECID_CHR,
                                     a.ORDEREXECTYPE_INT,
                                     a.CHARGEITEMID_CHR,
                                     a.ISRICH_INT,
                                     a.NEEDCONFIRM_INT,
                                     a.ACTIVATETYPE_INT,
                                     a.UNIT_VCHR,
                                     a.UNITPRICE_DEC,
                                     a.AMOUNT_DEC,
                                     a.CURAREAID_CHR,
                                     a.PUTMEDICINEFLAG_INT,
                                     b.ITEMSRCID_VCHR,
                                     b.itemname_vchr,
                                     b.itemsrctype_int,
                                     b.USAGEID_CHR,
                                     c.MEDICNETYPE_INT,
                                     c.PUTMEDTYPE_INT
                                from 
                                T_Opr_Bih_PatientCharge a,
                                t_bse_chargeitem b,
                                T_BSE_MEDICINE c                          
                                where a.chargeitemid_chr = b.itemid_chr and
                                      b.itemsrcid_vchr = c.MEDICINEID_CHR(+) and
                                     a.AMOUNT_DEC > 0 and 
                                     a.STATUS_INT = 1 and
                                     a.PSTATUS_INT = 1 and
                                     a.ACTIVATETYPE_INT in(2,4,5) and 
                                     a.ACTIVE_DAT is null and
                                     a.registerid_chr = ? ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_strRegisterId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        //p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["CURAREAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["CURBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = p_strRegisterId;
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();


                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        //}
                        //catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        //}
                        //catch { }
                        //p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["itemname_vchr"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        //p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        //try
                        //{
                        //    p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECUTETIME_INT"].ToString().Trim());
                        //}
                        //catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_intEXECDAYS_INT = Int32.Parse(dtbResult.Rows[i1]["EXECUTEDAYS_INT"].ToString().Trim());
                        //}
                        //catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        // p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECUTEDATE_VCHR"].ToString().Trim();

                        try
                        {
                            p_objResultArr[i1].m_intNEEDCONFIRM_INT = Int32.Parse(dtbResult.Rows[i1]["NEEDCONFIRM_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        //try
                        //{
                        //    p_objResultArr[i1].m_intISRECRUIT_INT = Int32.Parse(dtbResult.Rows[i1]["ISRECRUIT_INT"].ToString().Trim());
                        //}
                        //catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTMEDTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["PUTMEDTYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intMEDICNETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["MEDICNETYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["itemsrctype_int"].ToString().Trim());
                        }
                        catch { }

                        //������˷�����ϸ��ĵİ�ҩ��־Ϊ0 �ǰ�Ҫ�Ļ��򲻻����ɰ�ҩ��ϸ
                        string putFlag = dtbResult.Rows[i1]["PUTMEDICINEFLAG_INT"].ToString().Trim();
                        if (putFlag == "0")
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = 0;
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

        #region ����סԺ�Ǽ�Id���ɿ������Ҳ����ʰ�ҩ��ϸ
        /// <summary>
        /// ����סԺ�Ǽ�Id���ɿ������Ҳ����ʰ�ҩ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long GetCDRecipeDetailByRegisterId(string p_strRegisterId, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[0];
            long lngRes = 0;
            string strSQL = @"select a.PCHARGEID_CHR,
                                     a.CREATEAREA_CHR,
                                     a.CURBEDID_CHR,
                                     a.PATIENTID_CHR,
                                     a.ORDERID_CHR,
                                     a.ORDEREXECID_CHR,
                                     a.ORDEREXECTYPE_INT,
                                     a.CHARGEITEMID_CHR,
                                     a.ISRICH_INT,
                                     a.NEEDCONFIRM_INT,
                                     a.ACTIVATETYPE_INT,
                                     a.UNIT_VCHR,
                                     a.UNITPRICE_DEC,
                                     a.AMOUNT_DEC,
                                     a.CREATEAREA_CHR,
                                     a.PUTMEDICINEFLAG_INT,
                                     b.ITEMSRCID_VCHR,
                                     b.itemname_vchr,
                                     b.itemsrctype_int,
                                     b.USAGEID_CHR,
                                     c.MEDICNETYPE_INT,
                                     c.PUTMEDTYPE_INT                                     
                                from 
                                T_Opr_Bih_PatientCharge a,
                                t_bse_chargeitem b,
                                T_BSE_MEDICINE c                             
                                where a.chargeitemid_chr = b.itemid_chr and
                                      b.itemsrcid_vchr = c.MEDICINEID_CHR(+) and
                                      a.AMOUNT_DEC > 0 and 
                                      a.STATUS_INT = 1 and 
                                      a.PSTATUS_INT = 1 and
                                      a.ACTIVATETYPE_INT in(2,4,5) and 
                                      a.ACTIVE_DAT is null and
                                      a.registerid_chr = ? ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_strRegisterId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsT_Bih_Opr_Putmeddetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        //p_objResultArr[i1].m_strPUTMEDDETAILID_CHR = dtbResult.Rows[i1]["PUTMEDDETAILID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAREAID_CHR = dtbResult.Rows[i1]["CREATEAREA_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBedID = dtbResult.Rows[i1]["CURBEDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPAIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREGISTERID_CHR = p_strRegisterId;
                        p_objResultArr[i1].m_strORDERID_CHR = dtbResult.Rows[i1]["ORDERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDEREXECID_CHR = dtbResult.Rows[i1]["ORDEREXECID_CHR"].ToString().Trim();


                        try
                        {
                            p_objResultArr[i1].m_intORDEREXECTYPE_INT = Convert.ToInt32(dtbResult.Rows[i1]["ORDEREXECTYPE_INT"].ToString().Trim());
                        }
                        catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_intRECIPENO_INT = Convert.ToInt32(dtbResult.Rows[i1]["RECIPENO_INT"].ToString().Trim());
                        //}
                        //catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_dblDOSAGE_DEC = double.Parse(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString().Trim());
                        //}
                        //catch { }
                        //p_objResultArr[i1].m_strDOSAGEUNIT_VCHR = dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strCHARGEITEMID_CHR = dtbResult.Rows[i1]["CHARGEITEMID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDID_CHR = dtbResult.Rows[i1]["ITEMSRCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDNAME_VCHR = dtbResult.Rows[i1]["itemname_vchr"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intISRICH_INT = Convert.ToInt32(dtbResult.Rows[i1]["ISRICH_INT"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strDOSETYPEID_CHR = dtbResult.Rows[i1]["USAGEID_CHR"].ToString().Trim();
                        //p_objResultArr[i1].m_strEXECFREQID_CHR = dtbResult.Rows[i1]["EXECFREQID_CHR"].ToString().Trim();
                        //try
                        //{
                        //    p_objResultArr[i1].m_intEXECTIMES_INT = Convert.ToInt32(dtbResult.Rows[i1]["EXECUTETIME_INT"].ToString().Trim());
                        //}
                        //catch { }
                        //try
                        //{
                        //    p_objResultArr[i1].m_intEXECDAYS_INT = Int32.Parse(dtbResult.Rows[i1]["EXECUTEDAYS_INT"].ToString().Trim());
                        //}
                        //catch { }
                        try
                        {
                            p_objResultArr[i1].m_dblUNITPRICE_MNY = double.Parse(dtbResult.Rows[i1]["UNITPRICE_DEC"].ToString().Trim());
                        }
                        catch { }
                        p_objResultArr[i1].m_strUNIT_VCHR = dtbResult.Rows[i1]["UNIT_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_dblGET_DEC = double.Parse(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
                        }
                        catch { }

                        p_objResultArr[i1].m_strPCHARGEID_CHR = dtbResult.Rows[i1]["PCHARGEID_CHR"].ToString().Trim();
                        // p_objResultArr[i1].m_strEXECTIME_VCHR = dtbResult.Rows[i1]["EXECUTEDATE_VCHR"].ToString().Trim();

                        try
                        {
                            p_objResultArr[i1].m_intNEEDCONFIRM_INT = Int32.Parse(dtbResult.Rows[i1]["NEEDCONFIRM_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intACTIVATETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["ACTIVATETYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["itemsrctype_int"].ToString().Trim());
                        }
                        catch { }

                        //try
                        //{
                        //    p_objResultArr[i1].m_intISRECRUIT_INT = Int32.Parse(dtbResult.Rows[i1]["ISRECRUIT_INT"].ToString().Trim());
                        //}
                        //catch { }
                        try
                        {
                            p_objResultArr[i1].m_intPUTMEDTYPE_INT = Int32.Parse(dtbResult.Rows[i1]["PUTMEDTYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        try
                        {
                            p_objResultArr[i1].m_intMEDICNETYPE_INT = Int32.Parse(dtbResult.Rows[i1]["MEDICNETYPE_INT"].ToString().Trim());
                        }
                        catch { }

                        //������˷�����ϸ��ĵİ�ҩ��־Ϊ0 �ǰ�Ҫ�Ļ��򲻻����ɰ�ҩ��ϸ
                        string putFlag = dtbResult.Rows[i1]["PUTMEDICINEFLAG_INT"].ToString().Trim();
                        if (putFlag == "0")
                        {
                            p_objResultArr[i1].m_intITEMSRCTYPE_INT = 0;
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

        #region ִ�а�ҩ
        /// <summary>
        ///  ִ�а�ҩ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="operatorId"></param>
        /// <param name="medStoreId">0005 ������ҩ��</param>
        /// <param name="strMedType"></param>
        /// <param name="putMedType"></param>
        /// <param name="dt"></param>
        /// <param name="FilterExp"></param>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="m_strMsg"></param>
        /// <returns></returns>
        [AutoComplete]
        public long PutMed(string areaId, string operatorId, string medStoreId, string strMedType, int putMedType, DataTable dt, string FilterExp, string m_strDrugStoreid, string m_strSecondLevelMode, out string m_strMsg, out DataTable p_dtbPutMedResult, out DataTable p_dtbNoPutMedResult, int medTypeId, out bool isPretestMed)
        {
            long lngReg = 0;
            m_strMsg = string.Empty;
            isPretestMed = false;
            string putMedReqId;
            System.Collections.Generic.Dictionary<string, clsPutMedicineDetailGroup> m_htReturn = new System.Collections.Generic.Dictionary<string, clsPutMedicineDetailGroup>();
            p_dtbNoPutMedResult = new DataTable();
            p_dtbPutMedResult = new DataTable();
            //List<clsPutMedicineDetailGroup> objListDetail = null;
            if (m_strSecondLevelMode == "1")
            {
                //lngReg = m_lngPutMedCheckGross(m_strDrugStoreid, dt, out dt, out p_dtbNoPutMedResult, out objListDetail);

                List<clsPutMedicineDetailGroup> lstCureMedSub = null;
                //�ж��Ƿ����㹻��汻�ۼ�
                if (!JudgeHasEnoughStorage(m_strDrugStoreid, dt, out m_strMsg, out m_htReturn, out lstCureMedSub))
                {
                    return 0;
                }
                else
                {
                    lngReg = this.SubstractStorage(m_htReturn, lstCureMedSub);
                    if (lngReg <= 0) throw new Exception("�ۼ����ʧ�ܣ�");
                }
            }
            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = FilterExp;
                lngReg = InsertPutMedReq(areaId, operatorId, putMedType, medStoreId, medTypeId, out putMedReqId);
                if (lngReg > 0 && dv != null)
                {
                    clsT_Bih_Opr_Putmeddetail_VO[] putMedDt = new clsT_Bih_Opr_Putmeddetail_VO[dv.Count];
                    for (int i1 = 0; i1 < dv.Count; i1++)
                    {
                        putMedDt[i1] = new clsT_Bih_Opr_Putmeddetail_VO();
                        putMedDt[i1].m_strPUTMEDDETAILID_CHR = dv[i1]["PUTMEDDETAILID_CHR"].ToString();
                        putMedDt[i1].m_strPUTMEDREQID_CHR = putMedReqId;
                        putMedDt[i1].m_strMEDSTOREID_CHR = medStoreId;
                        putMedDt[i1].m_intPUTTYPE_INT = putMedType;
                    }
                    List<clsPutMedicineDetailGroup> objList = new List<clsPutMedicineDetailGroup>();
                    lngReg = this.m_lngGetPutMedicineDetailGroup(m_htReturn, m_strDrugStoreid, dt, out objList);
                    lngReg = UpdatePutMedDetailByPutMedDetailId(putMedDt, (medStoreId == "0005"), out isPretestMed);
                    lngReg = this.m_lngAddDSRecipeAccountInfo(operatorId, objList);
                    if (lngReg <= 0)
                    {
                        throw new Exception(lngReg == 0 ? "û�и��µ��κΰ�ҩ��ϸ��¼��" : "���°�ҩ��ϸ���ҩ��־����");
                    }
                }
            }
            p_dtbPutMedResult = dt;
            return lngReg;
        }
        #endregion

        #region �ж��Ƿ����㹻�Ŀ����Խ��пۼ�
        /// <summary>
        /// �ж��Ƿ����㹻�Ŀ����Խ��пۼ�
        /// </summary>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="m_dtPutMedDetail"></param>
        /// <param name="m_strMsg"></param>
        /// <param name="m_htReturn"></param>
        /// <returns></returns>
        [AutoComplete]
        bool JudgeHasEnoughStorage(string drugStoreId, DataTable dtPutMedDetail, out string msg, out System.Collections.Generic.Dictionary<string, clsPutMedicineDetailGroup> hasMed, out List<clsPutMedicineDetailGroup> lstCureMedSub)
        {
            //1.�԰�ҩ��ϸ��ҩƷid���з��飬ͳ�Ƹ���ҩƷʵ�ʿۼ����������
            //2.�ж��Ƿ����㹻�Ŀ����Խ��пۼ���
            long lngRes = 0;
            msg = string.Empty;
            DataTable dtTable = null;
            hasMed = new System.Collections.Generic.Dictionary<string, clsPutMedicineDetailGroup>();

            DataTable dtPatSub = new DataTable();   // �ۼ�������
            dtPatSub.Columns.Add("registerId", typeof(string));
            dtPatSub.Columns.Add("storeId", typeof(string));
            dtPatSub.Columns.Add("orderId", typeof(string));
            dtPatSub.Columns.Add("medId", typeof(string));
            dtPatSub.Columns.Add("ipAmount", typeof(double));
            dtPatSub.Columns.Add("opAmount", typeof(double));
            DataRow drPat = dtPatSub.NewRow();
            double opAmount = 0;
            double ipAmount = 0;
            lstCureMedSub = new List<clsPutMedicineDetailGroup>();

            DataRow[] drr = null;
            clsPutMedicineDetailGroup vo;
            dtPatSub.BeginLoadData();
            foreach (DataRow dr in dtPutMedDetail.Rows)
            {
                // �ѿۿ��
                if (dr["isclinicsub"] != DBNull.Value && Convert.ToInt32(dr["isclinicsub"].ToString()) == 1)
                {
                    continue;
                }
                if (dr["ipchargeflg_int"].ToString() == "0")        // ������λ(��λ)
                {
                    opAmount = Convert.ToDouble(dr["get_dec"]);
                    ipAmount = Convert.ToDouble(dr["get_dec"]) * Convert.ToDouble(dr["packqty_dec"]);
                }
                else
                {
                    opAmount = Math.Round(Convert.ToDouble(dr["get_dec"]) / Convert.ToDouble(dr["packqty_dec"]), 4);
                    ipAmount = Convert.ToDouble(dr["get_dec"]);
                }

                if (hasMed.ContainsKey(dr["medid_chr"].ToString()))
                {
                    vo = hasMed[dr["medid_chr"].ToString()]; // as clsPutMedicineDetailGroup;
                    vo.m_dblOPAmount += opAmount;
                    vo.m_dblIPAmount += ipAmount;
                }
                else
                {
                    vo = new clsPutMedicineDetailGroup();
                    vo.m_listSubStorageDetail = new List<clsPutMedicineDetailGroup>();
                    vo.m_strMedicineid_chr = dr["medid_chr"].ToString();
                    vo.m_strMedicineName = dr["medicinename_vchr"].ToString();
                    vo.m_strDrugStoreid = drugStoreId;
                    vo.m_dblPackage = Convert.ToDouble(dr["packqty_dec"]);
                    vo.m_dblOPAmount = opAmount;
                    vo.m_dblIPAmount = ipAmount;
                    vo.m_strPutMedDetaileId = dr["PUTMEDDETAILID_CHR"].ToString();
                    hasMed.Add(dr["medid_chr"].ToString(), vo);
                }
                if (1 != 1)     // �Ƴ���ҩ����--ͣ��
                {
                    drPat["registerId"] = dr["registerid_chr"];
                    drPat["storeId"] = drugStoreId; //dr["medstoreid_chr"]; dr["medstoreid_chr"] û��ֵ? ����
                    drPat["orderId"] = dr["orderid_chr"];
                    drPat["medId"] = dr["medid_chr"];
                    drPat["ipAmount"] = ipAmount;
                    drPat["opAmount"] = opAmount;
                    string filter = "registerId = '{0}' and storeId = '{1}' and orderId = '{2}' and medId = '{3}'";
                    string[] filterData = new string[4] { drPat["registerId"].ToString(), drPat["storeId"].ToString(), drPat["orderId"].ToString(), drPat["medId"].ToString() };
                    drr = dtPatSub.Select(string.Format(filter, filterData));
                    if (drr == null || drr.Length == 0)
                    {
                        dtPatSub.LoadDataRow(drPat.ItemArray, true);
                    }
                    else
                    {
                        drr[0]["ipAmount"] = Convert.ToDouble(drr[0]["ipAmount"]) + ipAmount;
                        drr[0]["opAmount"] = Convert.ToDouble(drr[0]["opAmount"]) + opAmount;
                        dtPatSub.AcceptChanges();
                    }
                }
            }
            dtPatSub.EndLoadData();

            // �ٴ��ѿۿ��  �Ƴ���ҩ����--ͣ��
            //if (dtPatSub.Rows.Count == 0)
            //{
            //    return true;
            //}

            try
            {
                string Sql = string.Empty;
                clsHRPTableService svc = new clsHRPTableService();
                System.Data.IDataParameter[] parm = null;

                Sql = @"select t.serno,
                               t.registerid,
                               t.orderid,
                               t.storeid,
                               t.medid,
                               t.seriesid,
                               t.ipamountreal,
                               t.opamountreal,
                               t.ipamountre,
                               t.opamountre,
                               b.ipchargeflg_int as ipchargeflag
                          from t_curemedsubtract t
                         inner join t_bse_medicine b
                            on t.medid = b.medicineid_chr
                         where t.registerid = ?
                           and t.storeid = ?
                           and t.orderid = ?
                           and t.medid = ?";

                DataTable dtPatMed = null;      // Ԥ����(�Ƴ���ҩ)
                DataTable dtPatMedTmp = null;
                if (1 != 1)     // �Ƴ���ҩ����--ͣ��
                {
                    foreach (DataRow dr in dtPatSub.Rows)
                    {
                        svc.CreateDatabaseParameter(4, out parm);
                        parm[0].Value = dr["registerid"].ToString();
                        parm[1].Value = dr["storeid"].ToString();
                        parm[2].Value = dr["orderid"].ToString();
                        parm[3].Value = dr["medid"].ToString();
                        svc.lngGetDataTableWithParameters(Sql, ref dtPatMedTmp, parm);
                        if (dtPatMed == null) dtPatMed = dtPatMedTmp.Clone();
                        if (dtPatMed != null && dtPatMedTmp != null && dtPatMedTmp.Rows.Count > 0)
                            dtPatMed.Merge(dtPatMedTmp);
                    }
                }

                Sql = @"select a.seriesid_int,
                               a.iprealgross_int,
                               a.oprealgross_int,
                               b.assistcode_chr as medcode
                          from t_ds_storage_detail a
                         inner join t_bse_medicine b
                            on a.medicineid_chr = b.medicineid_chr
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?
                         order by a.validperiod_dat";

                clsPutMedicineDetailGroup voTmp = null;
                foreach (clsPutMedicineDetailGroup de in hasMed.Values)
                {
                    vo = de;    //.Value as clsPutMedicineDetailGroup;
                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = vo.m_strDrugStoreid;
                    parm[1].Value = vo.m_strMedicineid_chr;
                    lngRes = svc.lngGetDataTableWithParameters(Sql, ref dtTable, parm);
                    if (lngRes > 0 && dtTable != null && dtTable.Rows.Count > 0)
                    {
                        if (dtPatMed != null && dtPatMed.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTable.Rows)
                            {
                                drr = dtPatMed.Select("seriesid = " + dr["seriesid_int"].ToString());
                                if (drr != null && drr.Length > 0)
                                {
                                    dr["iprealgross_int"] = Convert.ToDouble(dr["iprealgross_int"]) + Convert.ToDouble(drr[0]["ipamountre"]);
                                    dr["oprealgross_int"] = Convert.ToDouble(dr["oprealgross_int"]) + Convert.ToDouble(drr[0]["opamountre"]);
                                }
                            }
                        }
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]) < vo.m_dblIPAmount)
                            {
                                if (i == dtTable.Rows.Count - 1)
                                {
                                    msg = string.Format("ҩƷ({0})û�г�����,���ܽ�����ҩ��", dtTable.Rows[i]["medcode"].ToString() + " " + vo.m_strMedicineName);
                                    return false;
                                }
                                if (Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]) <= 0)
                                {
                                    continue;
                                }
                                vo.m_dblIPAmount -= Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]);
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dtTable.Rows[i]["seriesid_int"]);
                                voTmp.m_dblIPAmount = Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]);
                                voTmp.m_dblOPAmount = Math.Round(voTmp.m_dblIPAmount / vo.m_dblPackage, 4);
                                voTmp.m_strMedicineid_chr = vo.m_strMedicineid_chr;
                                voTmp.m_strDrugStoreid = vo.m_strDrugStoreid;
                                vo.m_listSubStorageDetail.Add(voTmp);
                            }
                            else
                            {
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dtTable.Rows[i]["seriesid_int"]);
                                voTmp.m_dblIPAmount = Convert.ToDouble(vo.m_dblIPAmount);
                                voTmp.m_dblOPAmount = Math.Round(vo.m_dblIPAmount / vo.m_dblPackage, 4);
                                voTmp.m_strMedicineid_chr = vo.m_strMedicineid_chr;
                                voTmp.m_strDrugStoreid = vo.m_strDrugStoreid;
                                voTmp.m_strPutMedDetaileId = vo.m_strPutMedDetaileId;
                                vo.m_listSubStorageDetail.Add(voTmp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dtTable == null || dtTable.Rows.Count == 0)
                        {
                            msg = string.Format("ҩƷ({0})�������κο��,���ܽ�����ҩ��", vo.m_strMedicineName);
                            return false;
                        }
                        else
                        {
                            msg = "��ȡҩƷ������ݴ���";
                            return false;
                        }
                    }
                }

                if (dtPatMed != null && dtPatMed.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dtPatSub.Rows)          // �ۼ�������
                    {
                        ipAmount = Convert.ToDouble(dr1["ipAmount"]);
                        opAmount = Convert.ToDouble(dr1["opAmount"]);
                        foreach (DataRow dr2 in dtPatMed.Rows)      // Ԥ����(�Ƴ���ҩ)
                        {
                            if (dr1["registerid"].ToString() == dr2["registerid"].ToString() && dr1["storeid"].ToString() == dr2["storeid"].ToString() &&
                                dr1["orderid"].ToString() == dr2["orderid"].ToString() && dr1["medid"].ToString() == dr2["medid"].ToString())
                            {
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dr2["seriesid"]);
                                voTmp.m_strMedicineid_chr = dr2["medid"].ToString();
                                voTmp.m_strDrugStoreid = dr2["storeid"].ToString();
                                voTmp.OrderId = dr2["orderid"].ToString();
                                voTmp.CuremedSubtractSerNo = Convert.ToDecimal(dr2["serno"].ToString());       // Ԥ��ҩID
                                voTmp.IpChargeFlag = dr2["ipchargeflag"] == DBNull.Value ? 1 : Convert.ToInt32(dr2["ipchargeflag"]);  // 0 ������λ; 1 ��С��λ
                                if (Convert.ToDouble(dr2["ipamountre"]) >= ipAmount)        // Ԥ���� >= ������
                                {
                                    voTmp.m_dblIPAmount = Convert.ToDouble(ipAmount);
                                    voTmp.m_dblOPAmount = Convert.ToDouble(opAmount);
                                    lstCureMedSub.Add(voTmp);
                                    break;
                                }
                                else
                                {
                                    voTmp.m_dblIPAmount = Convert.ToDouble(dr2["ipamountre"]);
                                    voTmp.m_dblOPAmount = Convert.ToDouble(dr2["opamountre"]);
                                    lstCureMedSub.Add(voTmp);

                                    ipAmount -= Convert.ToDouble(dr2["ipamountre"]);
                                    opAmount -= Convert.ToDouble(dr2["opamountre"]);
                                }
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
            return true;
        }
        #endregion

        #region ��ҩ����ж�
        /// <summary>
        /// ��ҩ����ж�
        /// </summary>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="p_dtbPutMedInfo"></param>
        /// <param name="p_dtbResultPutMedInfo"></param>
        /// <param name="p_dtbNoPutMedInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngPutMedCheckGross(string m_strDrugStoreid, DataTable p_dtbPutMedInfo, out DataTable p_dtbResultPutMedInfo, out DataTable p_dtbNoPutMedInfo, out List<clsPutMedicineDetailGroup> objListDetail)
        {
            long lngRes = 0;
            //bool blnGross = true;
            List<string> lstMedId = new List<string>();//ҩƷID
            string strMedID = string.Empty;//ҩƷID
            string strPharmacyID = string.Empty;//ҩ��ID
            //ҩƷID��LIST��ӦҩƷ�İ�ҩ��ϸ(����֮��Աȿ��)--���жϰ�ҩҩƷ����ֻ�������㹻��ҩƷ
            Dictionary<string, List<clsPutMedicineDetailGroup>> dtnPutMedInfo = new Dictionary<string, List<clsPutMedicineDetailGroup>>();
            Dictionary<string, double> dtnKCL = new Dictionary<string, double>();//��������
            List<string> lstInpatientRecipeno = new List<string>();//��¼���˿�治��İ�ҩ��ϸ��ҽ������(����סԺ��*ҽ������)��
            // clsDsStorageVO[] objDsStorageVOArr=null;//����������ϸ�����ڿۿ��ʱ�����δ���
            Dictionary<string, List<clsDsStorageVO>> m_dtnKCLDetail = new Dictionary<string, List<clsDsStorageVO>>();//����������ϸ�����ڿۿ��ʱ�����δ���
            List<clsPutMedicineDetailGroup> lstPutMedDtVo;//��ҩ��ϸLIST
            p_dtbResultPutMedInfo = new DataTable();//���տ���㹻��ҩ��
            p_dtbNoPutMedInfo = new DataTable();//���ܰ�ҩ��
            objListDetail = new List<clsPutMedicineDetailGroup>();//��ҩ�����ˮ����ϸ��Ϣ

            int intRowsCount = p_dtbPutMedInfo.Rows.Count;
            DataRow drPutMedInfo = null;
            clsPutMedicineDetailGroup objPutmedVo = null;
            for (int i = 0; i < intRowsCount; i++)
            {
                drPutMedInfo = p_dtbPutMedInfo.Rows[i];
                strMedID = drPutMedInfo["medid_chr"].ToString();
                objPutmedVo = new clsPutMedicineDetailGroup();
                objPutmedVo.m_strPutMedDetaileId = drPutMedInfo["PUTMEDDETAILID_CHR"].ToString();
                objPutmedVo.m_strMedicineid_chr = strMedID;
                objPutmedVo.m_strInPatientIdRecipeno = drPutMedInfo["inpatientid_chr"].ToString() + "*" + drPutMedInfo["recipeno_int"].ToString();
                objPutmedVo.m_strDrugStoreid = m_strDrugStoreid;
                if (drPutMedInfo["ipchargeflg_int"].ToString() == "0")
                {
                    objPutmedVo.m_dblOPAmount = Convert.ToDouble(drPutMedInfo["get_dec"]);
                    objPutmedVo.m_dblIPAmount = Convert.ToDouble(drPutMedInfo["get_dec"]) * Convert.ToDouble(drPutMedInfo["packqty_dec"]);
                }
                else
                {
                    objPutmedVo.m_dblOPAmount = Math.Round(Convert.ToDouble(drPutMedInfo["get_dec"]) / Convert.ToDouble(drPutMedInfo["packqty_dec"]), 4);
                    objPutmedVo.m_dblIPAmount = Convert.ToDouble(drPutMedInfo["get_dec"]);
                }
                if (!dtnPutMedInfo.ContainsKey(strMedID))
                {
                    lstMedId.Add(strMedID);
                    lstPutMedDtVo = new List<clsPutMedicineDetailGroup>();
                    lstPutMedDtVo.Add(objPutmedVo);
                    dtnPutMedInfo.Add(strMedID, lstPutMedDtVo);
                }
                else
                {
                    dtnPutMedInfo[strMedID].Add(objPutmedVo);
                }
            }
            lngRes = this.m_lngGetMedicineGross(m_strDrugStoreid, lstMedId, out dtnKCL, out m_dtnKCLDetail);//��ȡ���

            List<string> lstNoGrossPutMedId = new List<string>();//��¼�п�浫�����ҩ��ϸID
            //�ж�ÿ����ҩ��ϸ�Ŀ��
            foreach (string strKey in dtnPutMedInfo.Keys)
            {
                lstPutMedDtVo = dtnPutMedInfo[strKey];//����ҩƷ���еİ�ҩ��ϸVO
                for (int i4 = 0; i4 < lstPutMedDtVo.Count; i4++)
                {
                    clsPutMedicineDetailGroup objPutMedDtVo = lstPutMedDtVo[i4];
                    double dblGet = objPutMedDtVo.m_dblIPAmount;//��ǰ��ҩ��Ϣ������
                    if (dtnKCL.ContainsKey(objPutMedDtVo.m_strMedicineid_chr))
                    {
                        if (dblGet <= dtnKCL[objPutMedDtVo.m_strMedicineid_chr])//����С�ڿ�棬��ۼ�
                        {
                            dtnKCL[objPutMedDtVo.m_strMedicineid_chr] = dtnKCL[objPutMedDtVo.m_strMedicineid_chr] - dblGet;
                        }
                        else if (dblGet > dtnKCL[objPutMedDtVo.m_strMedicineid_chr])//��治��������ҩ��ϸID��¼����
                        {
                            lstNoGrossPutMedId.Add(objPutMedDtVo.m_strPutMedDetaileId);

                            if (!lstInpatientRecipeno.Contains(objPutMedDtVo.m_strInPatientIdRecipeno))//��¼���ܰ�ҩ��ҽ�����ţ�����ɾ������ͬ��ҽ��
                            {
                                lstInpatientRecipeno.Add(objPutMedDtVo.m_strInPatientIdRecipeno);
                            }
                            dtnPutMedInfo[strKey].Remove(objPutMedDtVo);
                            i4--;
                            // blnGross = false;
                        }
                    }
                    else
                    {
                        lstNoGrossPutMedId.Add(objPutMedDtVo.m_strPutMedDetaileId);

                        if (!lstInpatientRecipeno.Contains(objPutMedDtVo.m_strInPatientIdRecipeno))//��¼���ܰ�ҩ��ҽ�����ţ�����ɾ������ͬ��ҽ��
                        {
                            lstInpatientRecipeno.Add(objPutMedDtVo.m_strInPatientIdRecipeno);
                        }
                        dtnPutMedInfo[strKey].Remove(objPutMedDtVo);
                        i4--;
                        //blnGross = false;
                    }
                }
            }
            //�Ƴ���ҩ��治���ͬ��ҽ��
            foreach (string strKey in dtnPutMedInfo.Keys)
            {
                lstPutMedDtVo = dtnPutMedInfo[strKey];//����ҩƷ���еİ�ҩ��ϸVO
                for (int i5 = 0; i5 < lstPutMedDtVo.Count; i5++)
                {
                    clsPutMedicineDetailGroup objPutMedDtVo = lstPutMedDtVo[i5];
                    if (lstInpatientRecipeno.Contains(objPutMedDtVo.m_strInPatientIdRecipeno))
                    {
                        lstNoGrossPutMedId.Add(objPutMedDtVo.m_strPutMedDetaileId);
                        dtnPutMedInfo[strKey].Remove(objPutMedDtVo);
                        i5--;
                    }
                }
            }

            //�Ƴ��İ�ҩ��Ϣ
            if (lstNoGrossPutMedId.Count > 0)
            {
                p_dtbResultPutMedInfo = p_dtbPutMedInfo.Clone();
                p_dtbNoPutMedInfo = p_dtbPutMedInfo.Clone();
                intRowsCount = p_dtbPutMedInfo.Rows.Count;
                DataRow drTemp = null;
                for (int i5 = 0; i5 < intRowsCount; i5++)
                {
                    drTemp = p_dtbPutMedInfo.Rows[i5];
                    if (!lstNoGrossPutMedId.Contains(drTemp["PUTMEDDETAILID_CHR"].ToString()))
                    {
                        p_dtbResultPutMedInfo.ImportRow(drTemp);
                    }
                    else
                    {
                        p_dtbNoPutMedInfo.ImportRow(drTemp);
                    }
                }
            }
            else
            {
                p_dtbResultPutMedInfo = p_dtbPutMedInfo.Copy();
            }

            #region ���ۼ�����
            lngRes = this.m_lngGetDownGrossDetail(ref dtnPutMedInfo, m_dtnKCLDetail, out objListDetail);
            #endregion

            return lngRes;
        }
        /// <summary>
        /// ��ѯ���
        /// </summary>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="p_lstMedId"></param>
        /// <param name="p_dtnKCL"></param>
        /// <param name="p_objDsStorageVOArr"></param>
        /// <returns></returns>
        private long m_lngGetMedicineGross(string m_strDrugStoreid, List<string> p_lstMedId, out Dictionary<string, double> p_dtnKCL, out Dictionary<string, List<clsDsStorageVO>> p_dtnKCLDetail)
        {
            long lngRes = 0;
            string strSQL = "";
            List<string> lstMedId100 = new List<string>();
            p_dtnKCL = new Dictionary<string, double>();
            p_dtnKCLDetail = new Dictionary<string, List<clsDsStorageVO>>();
            //p_objDsStorageVOArr = null;
            string strMedId = "";
            int intListCount = p_lstMedId.Count;
            int int100 = 0;
            DataTable dtbResult = new DataTable();
            clsHRPTableService objHRPSvc = null;
            try
            {
                if (intListCount > 0)
                {
                    //������ҩƷID��ֳ�100��Ϊһ��
                    for (int i = 0; i < intListCount; i++)
                    {
                        strMedId += "'" + p_lstMedId[i] + "',";
                        int100++;
                        if (int100 >= 100)
                        {
                            strMedId = strMedId.TrimEnd(',');
                            lstMedId100.Add(strMedId);
                            strMedId = "";
                            int100 = 0;
                        }
                        else if ((i + 1) == intListCount)
                        {
                            strMedId = strMedId.TrimEnd(',');
                            lstMedId100.Add(strMedId);
                            strMedId = "";
                            int100 = 0;
                        }
                    }

                }
                strSQL = @"select distinct a.seriesid_int,
                                   a.medicineid_chr,
                                   a.packqty_dec,
                                   a.validperiod_dat,
                                   a.drugstoreid_chr,
                                   a.iprealgross_int,
                                   a.ipavailablegross_num,
                                   a.oprealgross_int,
                                   a.opavailablegross_num,
                                   a.dsinstoragedate_dat,
                                   b.ipchargeflg_int
                              from t_ds_storage_detail a, t_bse_medicine b, t_ds_storage c
                             where a.medicineid_chr = b.medicineid_chr
                               and a.medicineid_chr = c.medicineid_chr
                               and a.drugstoreid_chr = c.drugstoreid_chr
                               and a.canprovide_int = 1
                               and c.noqtyflag_int = 0
                               and c.ifstop_int = 0 
                               and a.drugstoreid_chr ='" + m_strDrugStoreid + @"'
                               and a.medicineid_chr in ([medid])";
                objHRPSvc = new clsHRPTableService();
                for (int i2 = 0; i2 < lstMedId100.Count; i2++)
                {
                    strSQL = strSQL.Replace("[medid]", lstMedId100[i2]);
                    DataTable dtbTemp = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbTemp);
                    if (lngRes > 0 && dtbTemp.Rows.Count > 0)
                    {
                        if (dtbResult.Rows.Count > 0)
                        {
                            if (dtbResult != null && dtbTemp != null && dtbTemp.Rows.Count > 0)
                                dtbResult.Merge(dtbTemp);
                        }
                        else
                        {
                            dtbResult = dtbTemp;
                        }
                    }

                }

                if (dtbResult.Rows.Count > 0)
                {
                    DataRow[] drGrossArr = null;
                    drGrossArr = dtbResult.Select("iprealgross_int > 0.00 ", "validperiod_dat asc,dsinstoragedate_dat asc,seriesid_int asc,drugstoreid_chr asc,medicineid_chr asc");
                    int intLen = drGrossArr.Length;
                    //p_objDsStorageVOArr = new clsDsStorageVO[intLen];
                    List<clsDsStorageVO> lstCurrentV0;//=new List<clsDsStorageVO> ();
                    clsDsStorageVO objCurrentVO = null;
                    for (int i3 = 0; i3 < intLen; i3++)
                    {

                        objCurrentVO = new clsDsStorageVO();
                        objCurrentVO.m_intSeriesID = Convert.ToInt32(drGrossArr[i3]["seriesid_int"]);

                        objCurrentVO.m_strMedicineID = drGrossArr[i3]["medicineid_chr"].ToString();

                        objCurrentVO.m_strPharmacyID = drGrossArr[i3]["drugstoreid_chr"].ToString();
                        objCurrentVO.m_dblPackqty = Convert.ToDouble(drGrossArr[i3]["packqty_dec"]);
                        objCurrentVO.m_dtmValidperiod = Convert.ToDateTime(drGrossArr[i3]["validperiod_dat"]);

                        objCurrentVO.m_dblIpavailableGross = Convert.ToDouble(drGrossArr[i3]["ipavailablegross_num"]);
                        objCurrentVO.m_dbIprealgross = Convert.ToDouble(drGrossArr[i3]["iprealgross_int"]);
                        objCurrentVO.m_intIpChargeFlg = Convert.ToInt32(drGrossArr[i3]["ipchargeflg_int"]);
                        //p_objDsStorageVOArr[i3] = objCurrentVO;
                        lstCurrentV0 = new List<clsDsStorageVO>();
                        lstCurrentV0.Add(objCurrentVO);
                        if (!p_dtnKCL.ContainsKey(objCurrentVO.m_strMedicineID))
                        {
                            p_dtnKCL.Add(objCurrentVO.m_strMedicineID, objCurrentVO.m_dbIprealgross);
                        }
                        else
                        {
                            p_dtnKCL[objCurrentVO.m_strMedicineID] = p_dtnKCL[objCurrentVO.m_strMedicineID] + objCurrentVO.m_dbIprealgross;
                        }
                        if (!p_dtnKCLDetail.ContainsKey(objCurrentVO.m_strMedicineID))
                        {
                            //p_dtnKCLDetail[objCurrentVO.m_strMedicineID] = new List<clsDsStorageVO>();
                            p_dtnKCLDetail.Add(objCurrentVO.m_strMedicineID, lstCurrentV0);
                        }
                        else
                        {
                            p_dtnKCLDetail[objCurrentVO.m_strMedicineID].Add(objCurrentVO);
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
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
                strSQL = null;
            }
            return lngRes;
        }

        #region ���Ŀۼ����� --> 2018-03-12 �÷�������ʹ��
        /// <summary>
        /// ���Ŀۼ�����      --> 2018-03-12 �÷�������ʹ��
        /// </summary>
        /// <param name="p_dtnPutMedInfo">��ҩƷ�İ�ҩ��Ϣ</param>
        /// <param name="p_dtnKCLDetail">��ҩƷ�Ŀ����ϸ��Ϣ</param>
        /// <param name="objListDetail">��ҩ�����ˮ����ϸ��Ϣ</param>
        /// <returns></returns>
        private long m_lngGetDownGrossDetail(ref Dictionary<string, List<clsPutMedicineDetailGroup>> p_dtnPutMedInfo, Dictionary<string, List<clsDsStorageVO>> p_dtnKCLDetail, out List<clsPutMedicineDetailGroup> objListDetail)
        {
            long lngRes = 0;
            long lngRecEff = -1;
            List<clsPutMedicineDetailGroup> lstPutMedDt = null;
            clsPutMedicineDetailGroup objPutMedDtVo = null;
            clsPutMedicineDetailGroup objSubPutMedDtVo = null;
            foreach (string strKey in p_dtnPutMedInfo.Keys)
            {
                lstPutMedDt = p_dtnPutMedInfo[strKey];
                for (int i1 = 0; i1 < lstPutMedDt.Count; i1++)
                {
                    objPutMedDtVo = lstPutMedDt[i1];
                    objPutMedDtVo.m_listSubStorageDetail = new List<clsPutMedicineDetailGroup>();
                    double dblGet = objPutMedDtVo.m_dblIPAmount;
                    double dblOpGet = objPutMedDtVo.m_dblOPAmount;

                    for (int i2 = 0; i2 < p_dtnKCLDetail[strKey].Count; i2++)
                    {

                        if (dblGet > 0)
                        {
                            objSubPutMedDtVo = new clsPutMedicineDetailGroup();
                            objSubPutMedDtVo.m_lngStorageSerial = p_dtnKCLDetail[strKey][i2].m_intSeriesID;
                            objSubPutMedDtVo.m_strPutMedDetaileId = objPutMedDtVo.m_strPutMedDetaileId;
                            objSubPutMedDtVo.m_strDrugStoreid = objPutMedDtVo.m_strDrugStoreid;
                            if (dblGet <= p_dtnKCLDetail[strKey][i2].m_dbIprealgross)//�˰�ҩ����С�ڸ������
                            {
                                p_dtnKCLDetail[strKey][i2].m_dbIprealgross = p_dtnKCLDetail[strKey][i2].m_dbIprealgross - dblGet;
                                p_dtnKCLDetail[strKey][i2].m_dbOprealgross = p_dtnKCLDetail[strKey][i2].m_dbOprealgross = dblOpGet;
                                objSubPutMedDtVo.m_dblIPAmount = dblGet;
                                objSubPutMedDtVo.m_dblOPAmount = dblOpGet;
                                objPutMedDtVo.m_dblIPDownGross = p_dtnKCLDetail[strKey][i2].m_dbIprealgross;
                                objPutMedDtVo.m_dblOPDownGross = p_dtnKCLDetail[strKey][i2].m_dbOprealgross;
                                objPutMedDtVo.m_lngStorageSerial = p_dtnKCLDetail[strKey][i2].m_intSeriesID;
                                objPutMedDtVo.m_listSubStorageDetail.Add(objSubPutMedDtVo);
                            }
                            else if (dblGet > p_dtnKCLDetail[strKey][i2].m_dbIprealgross)
                            {
                                objSubPutMedDtVo.m_dblIPAmount = p_dtnKCLDetail[strKey][i2].m_dbIprealgross;
                                objSubPutMedDtVo.m_dblOPAmount = p_dtnKCLDetail[strKey][i2].m_dbOprealgross;
                                p_dtnKCLDetail[strKey][i2].m_dbIprealgross = 0;
                                p_dtnKCLDetail[strKey][i2].m_dbOprealgross = 0;
                                dblGet = dblGet - p_dtnKCLDetail[strKey][i2].m_dbIprealgross;
                                dblOpGet = dblOpGet - p_dtnKCLDetail[strKey][i2].m_dbOprealgross;
                                objPutMedDtVo.m_dblIPDownGross = 0;
                                objPutMedDtVo.m_dblOPDownGross = 0;
                                objPutMedDtVo.m_lngStorageSerial = p_dtnKCLDetail[strKey][i2].m_intSeriesID;
                                objPutMedDtVo.m_listSubStorageDetail.Add(objSubPutMedDtVo);
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            //�����ϸ1
            objListDetail = new List<clsPutMedicineDetailGroup>();
            foreach (string strKey in p_dtnPutMedInfo.Keys)
            {
                for (int i2 = 0; i2 < p_dtnPutMedInfo[strKey].Count; i2++)
                {
                    objListDetail.AddRange(p_dtnPutMedInfo[strKey][i2].m_listSubStorageDetail.ToArray());
                }
            }
            if (objListDetail.Count <= 0)
            {
                return 1;
            }
            clsHRPTableService objHRPSvc = null;

            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Int64 };
            object[][] objValues = new object[3][];
            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[objListDetail.Count];//��ʼ��
            }

            for (int k1 = 0; k1 < objListDetail.Count; k1++)
            {
                objValues[0][k1] = objListDetail[k1].m_dblIPAmount;

                objValues[1][k1] = objListDetail[k1].m_dblOPAmount;

                objValues[2][k1] = objListDetail[k1].m_lngStorageSerial;

            }
            string strSQL = @"update t_ds_storage_detail a
   set a.iprealgross_int = a.iprealgross_int - ?,
       a.oprealgross_int = a.oprealgross_int - ?
 where a.seriesid_int = ?";
            try
            {
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                {
                    throw new Exception("�ۼ�������ӦҩƷ�����ϸ��");
                }

                //�����2
                objListDetail.Clear();
                foreach (string strKey in p_dtnPutMedInfo.Keys)
                {
                    objListDetail.AddRange(p_dtnPutMedInfo[strKey].ToArray());
                }
                dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.String, DbType.String };
                objValues = new object[4][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[objListDetail.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < objListDetail.Count; k1++)
                {
                    objValues[0][k1] = objListDetail[k1].m_dblIPAmount;
                    objValues[1][k1] = objListDetail[k1].m_dblOPAmount;
                    objValues[2][k1] = objListDetail[k1].m_strDrugStoreid;
                    objValues[3][k1] = objListDetail[k1].m_strMedicineid_chr;

                }
                strSQL = @"update t_ds_storage a
   set a.ipcurrentgross_num = a.ipcurrentgross_num - ?,
       a.opcurrentgross_num = a.opcurrentgross_num - ?
 where a.drugstoreid_chr = ?
   and a.medicineid_chr = ?";
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                {
                    throw new Exception("�ۼ�������ӦҩƷ�����ϸ��");
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
                strSQL = null;

                p_dtnKCLDetail.Clear();
                p_dtnKCLDetail = null;
            }
            return lngRes;

        }
        #endregion

        #endregion

        #region �ۼ����
        /// <summary>
        /// �ۼ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="hsTable"></param>
        /// <returns></returns>
        [AutoComplete]
        long SubstractStorage(System.Collections.Generic.Dictionary<string, clsPutMedicineDetailGroup> hsTable, List<clsPutMedicineDetailGroup> lstCureMedSub)
        {
            long lngRes = 0;
            if ((hsTable == null || hsTable.Count == 0) && (lstCureMedSub == null || lstCureMedSub.Count == 0))
            {
                return 99;
            }
            List<clsPutMedicineDetailGroup> objList = new List<clsPutMedicineDetailGroup>();
            foreach (clsPutMedicineDetailGroup de in hsTable.Values)
            {
                objList.AddRange(de.m_listSubStorageDetail.ToArray());
            }
            if (lstCureMedSub != null && lstCureMedSub.Count > 0)
            {
                foreach (clsPutMedicineDetailGroup vo1 in lstCureMedSub)
                {
                    foreach (clsPutMedicineDetailGroup vo2 in objList)
                    {
                        if (vo1.m_lngStorageSerial == vo2.m_lngStorageSerial)
                        {
                            vo2.m_dblIPAmount -= vo1.m_dblIPAmount;
                            vo2.m_dblOPAmount -= vo1.m_dblOPAmount;
                        }
                    }
                }
            }

            try
            {
                string Sql = string.Empty;
                DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int64 };
                object[][] objValues = new object[5][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[objList.Count];//��ʼ��
                }

                for (int k = 0; k < objList.Count; k++)
                {
                    objValues[0][k] = objList[k].m_dblIPAmount;
                    objValues[1][k] = objList[k].m_dblIPAmount;
                    objValues[2][k] = objList[k].m_dblOPAmount;
                    objValues[3][k] = objList[k].m_dblOPAmount;
                    objValues[4][k] = objList[k].m_lngStorageSerial;

                }
                Sql = @"update t_ds_storage_detail a
                               set a.iprealgross_int      = a.iprealgross_int - ?,
                                   a.ipavailablegross_num = a.ipavailablegross_num - ?,
                                   a.oprealgross_int      = a.oprealgross_int - ?,
                                   a.opavailablegross_num = a.opavailablegross_num - ?
                             where a.seriesid_int = ?";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRecEff = -1;
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                    throw new Exception("�ۼ�������ӦҩƷ�����ϸ��");

                dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.String, DbType.String };
                objValues = new object[4][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[objList.Count];
                }
                for (int k = 0; k < objList.Count; k++)
                {
                    objValues[0][k] = objList[k].m_dblIPAmount;
                    objValues[1][k] = objList[k].m_dblOPAmount;
                    objValues[2][k] = objList[k].m_strDrugStoreid;
                    objValues[3][k] = objList[k].m_strMedicineid_chr;
                }

                Sql = @"update t_ds_storage a
                           set a.ipcurrentgross_num = a.ipcurrentgross_num - ?,
                               a.opcurrentgross_num = a.opcurrentgross_num - ?
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?";

                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                    throw new Exception("�ۼ�������ӦҩƷ�����棡");

                if (lstCureMedSub != null && lstCureMedSub.Count > 0)
                {
                    Sql = @"update t_curemedsubtract
                               set ipamountre = ipamountre - ?, opamountre = opamountre - ?
                             where serno = ?";

                    dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Decimal };
                    objValues = new object[3][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[lstCureMedSub.Count];
                    }
                    for (int k = 0; k < lstCureMedSub.Count; k++)
                    {
                        objValues[0][k] = lstCureMedSub[k].m_dblIPAmount;
                        objValues[1][k] = lstCureMedSub[k].m_dblOPAmount;
                        objValues[2][k] = lstCureMedSub[k].CuremedSubtractSerNo;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("����t_curemedsubtractʧ��");

                    Sql = @"update t_opr_bih_order 
                               set preamount2 = preamount2 - ? 
                             where orderid_chr = ?";

                    dbTypes = new DbType[] { DbType.Double, DbType.String };
                    objValues = new object[2][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[lstCureMedSub.Count];
                    }
                    for (int k = 0; k < lstCureMedSub.Count; k++)
                    {
                        objValues[0][k] = (lstCureMedSub[k].IpChargeFlag == 0 ? lstCureMedSub[k].m_dblOPAmount : lstCureMedSub[k].m_dblIPAmount);
                        objValues[1][k] = lstCureMedSub[k].OrderId;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("����t_opr_bih_orderʧ��");

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

        #region UpdatePatientCharge
        [AutoComplete]
        private long UpdatePatientCharge(string patCharegeId, string operatorId)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE t_opr_bih_patientcharge 
					SET 
						ACTIVE_DAT = sysdate,
						ACTIVATOR_CHR = ? WHERE PCHARGEID_CHR= ? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = operatorId;
                objLisAddItemRefArr[1].Value = patCharegeId.Trim();

                long lngRecEff = -1;
                //
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

        #region UpdatePatientCharge
        [AutoComplete]
        private long UpdatePatientCharge(clsT_Bih_Opr_Putmeddetail_VO[] p_objPutMedDt, string operatorId)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE t_opr_bih_patientcharge 
					SET 
						ACTIVE_DAT = sysdate,
						ACTIVATOR_CHR = ? WHERE PCHARGEID_CHR= ? ";
            try
            {
                DbType[] dbTypes = new DbType[] {
                    DbType.String, DbType.String
                    };
                object[][] objValues = new object[2][];



                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_objPutMedDt.Length];//��ʼ��
                }

                for (int k1 = 0; k1 < p_objPutMedDt.Length; k1++)
                {
                    objValues[0][k1] = operatorId;
                    objValues[1][k1] = p_objPutMedDt[k1].m_strPCHARGEID_CHR;

                }

                if (p_objPutMedDt.Length > 0)
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region UpdateOrderExecute
        [AutoComplete]
        private long UpdateOrderExecute(string orderExecId)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE T_OPR_BIH_ORDEREXECUTE 
					SET 
						ISINCEPT_INT = 1 WHERE ORDEREXECID_CHR = ? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.DoExcute(strSQL);
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = orderExecId;
                long lngRecEff = -1;
                //
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

        #region UpdateOrderExecute
        [AutoComplete]
        private long UpdateOrderExecute(clsT_Bih_Opr_Putmeddetail_VO[] p_objPutMedDt)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE T_OPR_BIH_ORDEREXECUTE 
					SET 
						ISINCEPT_INT = 1 WHERE ORDEREXECID_CHR = ? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                DbType[] dbTypes = new DbType[] {
                    DbType.String
                    };
                object[][] objValues = new object[1][];



                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_objPutMedDt.Length];//��ʼ��
                }

                for (int k1 = 0; k1 < p_objPutMedDt.Length; k1++)
                {

                    objValues[0][k1] = p_objPutMedDt[k1].m_strORDEREXECID_CHR;

                }

                if (p_objPutMedDt.Length > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region InsertAreaPutMedRecord
        [AutoComplete]
        private long InsertAreaPutMedRecord(string areaId, string operatorId, string operatorName, out int recordPK)
        {
            long lngRes = 0;
            recordPK = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string seq = GetNextSeq("SEQ_AREAPUTMEDRECORD");
                recordPK = int.Parse(seq);

                string strSQL = @"INSERT INTO T_OPR_BIH_AREAPUTMEDRECORD
                                     (SEQ_INT, AREAID_CHR, PUT_DAT, PUTERID_CHR, PUTER_VCHR) 
					              VALUES(" + recordPK.ToString() + ", ?, sysdate, ?, ?)";

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = areaId;
                objLisAddItemRefArr[1].Value = operatorId;
                objLisAddItemRefArr[2].Value = operatorName;

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

        #region UpdatePutMedDetailByAreaId
        [AutoComplete]
        private long UpdatePutMedDetailByAreaId(string areaId, int areaSeq)
        {
            long lngRes = 0;

            string strSQL = @"
					UPDATE T_BIH_OPR_PUTMEDDETAIL 
					SET AREASEQ_INT = " + areaSeq.ToString() + @"
				    WHERE ISPUT_INT = 0 and 
                          ACTIVATETYPE_INT = 1 and 
                          ORDEREXECTYPE_INT <> 4 and 
                          AREAID_CHR='" + areaId.Trim() + "'";
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

        #region InsertPutMedReq
        /// <summary>
        /// ������ҩִ�е� 2018-05-04 ��Ч
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="operatorId"></param>
        /// <param name="putMedType"></param>
        /// <param name="medStoreId"></param>
        /// <param name="recordPK"></param>
        /// <returns></returns> 
        [AutoComplete]
        private long InsertPutMedReq(string areaId, string operatorId, int putMedType, string medStoreId, int medTypeId, out string recordPK)
        {
            long lngRes = 0;
            recordPK = "";

            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                string seq = GetNextSeq("SEQ_PUTMEDREQ");
                recordPK = seq.PadLeft(12, '0');

                string strSQL = @"insert into t_bih_opr_putmedreq
                                     (putmedreqid_chr, status_int, creator_chr, create_dat, areaid_chr, putmedtype_int, medstoreid_chr, medTypeId) 
					              values(?, 1, ?, sysdate, ?, ?, ?, ?)";

                System.Data.IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(6, out parm);

                int n = -1;
                parm[++n].Value = recordPK;
                parm[++n].Value = operatorId;
                parm[++n].Value = areaId;
                parm[++n].Value = putMedType;
                parm[++n].Value = medStoreId;
                parm[++n].Value = medTypeId;

                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = svc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parm);
                svc.Dispose();
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

        #region UpdatePutMedDetailByPutMedDetailId
        /// <summary>
        /// UpdatePutMedDetailByPutMedDetailId
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objPutMedDt"></param>
        /// <param name="isWMStore">�Ƿ�������ҩ��</param>
        /// <param name="isPretestMed"></param>
        /// <returns></returns>
        [AutoComplete]
        private long UpdatePutMedDetailByPutMedDetailId(clsT_Bih_Opr_Putmeddetail_VO[] p_objPutMedDt, bool isWMStore, out bool isPretestMed)
        {
            long lngRes = 0;
            string Sql = string.Empty;
            isPretestMed = false;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"update t_bih_opr_putmeddetail
                           set pubdate_dat     = sysdate,
                               isput_int       = 1,
                               putmedreqid_chr = ?,
                               puttype_int     = ?,
                               medstoreid_chr  = ?
                         where putmeddetailid_chr = ?";


                DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int16, DbType.String, DbType.String };
                object[][] objValues = new object[4][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_objPutMedDt.Length];
                }
                for (int k1 = 0; k1 < p_objPutMedDt.Length; k1++)
                {
                    objValues[0][k1] = p_objPutMedDt[k1].m_strPUTMEDREQID_CHR;
                    objValues[1][k1] = p_objPutMedDt[k1].m_intPUTTYPE_INT;
                    objValues[2][k1] = p_objPutMedDt[k1].m_strMEDSTOREID_CHR;
                    objValues[3][k1] = p_objPutMedDt[k1].m_strPUTMEDDETAILID_CHR;
                }
                if (p_objPutMedDt.Length > 0)
                {
                    lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                }

                if (isWMStore)
                {
                    Sql = @"select b.putmeddetailid_chr
                              from t_bih_opr_putmeddetail a
                             inner join t_pretestmed b
                                on a.putmeddetailid_chr = b.putmeddetailid_chr
                             inner join (select p.areaid_chr, p.medid_chr
                                           from t_bih_opr_putmeddetail p
                                          where p.putmeddetailid_chr = ? and p.puttype_int <> 4) c
                                on a.areaid_chr = c.areaid_chr
                               and a.medid_chr = c.medid_chr
                             where (b.recorddate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                               and b.refputmedreqid_chr is null
                               ";

                    DateTime dtmYesterday = DateTime.Now.AddDays(-1);
                    DataTable dt = null;
                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    foreach (clsT_Bih_Opr_Putmeddetail_VO item in p_objPutMedDt)
                    {
                        System.Data.IDataParameter[] parm = null;
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = item.m_strPUTMEDDETAILID_CHR;
                        parm[1].Value = dtmYesterday.ToString("yyyy-MM-dd") + " 00:00:00";
                        parm[2].Value = dtmYesterday.ToString("yyyy-MM-dd") + " 23:59:59";
                        svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string key = dr["putmeddetailid_chr"].ToString();
                                if (dicKey.ContainsKey(key) == false)
                                {
                                    dicKey.Add(key, item.m_strPUTMEDDETAILID_CHR);
                                    // �������reqId������ѿ������������ǻ��ܰ�ҩ���Ĵ浥һҩƷ��id
                                    //dicKey.Add(key, item.m_strPUTMEDREQID_CHR);
                                }
                            }
                        }
                    }

                    if (dicKey.Keys.Count > 0)
                    {
                        List<string> lstKey = new List<string>();
                        lstKey.AddRange(dicKey.Keys);

                        Sql = @"update t_pretestmed
                                   set refputmedreqid_chr = ?,
                                       recqty            =
                                       (select b.get_dec2 * nvl(b.pretestdays, 0) /
                                               (nvl(b.pretestdays, 0) + 1)
                                          from t_bih_opr_putmeddetail b
                                         where b.putmeddetailid_chr = ?)
                                 where putmeddetailid_chr = ?";

                        dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String };
                        objValues = new object[3][];

                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[lstKey.Count];
                        }
                        for (int k1 = 0; k1 < lstKey.Count; k1++)
                        {
                            objValues[0][k1] = dicKey[lstKey[k1]];
                            objValues[1][k1] = lstKey[k1];
                            objValues[2][k1] = lstKey[k1];
                        }
                        lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                        if (lngRes > 0) isPretestMed = true;
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

        #region UpdateAreaPutMedRecord
        /// <summary>
        /// UpdateAreaPutMedRecord
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="strMedType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long UpdateAreaPutMedRecord(string areaId, string strMedType)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD a
                               SET a.ISPUT_INT = 1
                             WHERE trunc(a.PUT_DAT) = trunc(sysdate)
                               and a.STATUS_INT = 1
                               and a.ISPUT_INT = 0
                               and not exists (select ''
                                      from t_bih_opr_putmeddetail b
                                     where b.areaseq_int = a.seq_int
                                       and b.isput_int = 0
                                       and b.MEDICNETYPE_INT in ( " + strMedType + @" )
                                       and b.areaid_chr = a.areaid_chr)
                               and a.AREAID_CHR = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.DoExcute(strSQL);

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = areaId;

                long lngRecEff = -1;
                //
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

        #region UpdateAreaPutMedRecord
        [AutoComplete]
        private long UpdateAreaPutMedRecord(string areaId, int filterType)
        {
            long lngRes = 0;

            string strSQL;

            switch (filterType)
            {
                case 0:
                    strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					    SET ISPUT_INT = 1, ISPUTA_INT = 1, ISPUTB_INT = 1, ISPUTC_INT = 1 ";
                    break;
                case 1:
                    strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					    SET ISPUT_INT = 1 ";
                    break;
                case 2:
                    strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					    SET ISPUTA_INT = 1 ";
                    break;
                case 3:
                    strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					    SET ISPUTB_INT = 1, ISPUTC_INT = 1";
                    break;
                default:
                    strSQL = @"UPDATE T_OPR_BIH_AREAPUTMEDRECORD 
					    SET ISPUT_INT = 1, ISPUTA_INT = 1, ISPUTB_INT = 1, ISPUTC_INT = 1 ";
                    break;

            }

            strSQL += @"  WHERE trunc(PUT_DAT) = trunc(sysdate) and
                          STATUS_INT = 1 and
                          AREAID_CHR = ? ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = areaId;

                long lngRecEff = -1;
                //
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

        #region ��ȡ���е���һ��ֵ
        /// <summary>
        /// ��ȡ���е���һ��ֵ
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        private string GetNextSeq(string p_seqName)
        {
            long lngRes = 0;

            string strSQL;
            strSQL = @"SELECT " + p_seqName + ".NEXTVAL FROM dual";

            System.Data.DataTable dtbResult = new System.Data.DataTable();

            string newSeq = "0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    newSeq = dtbResult.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return newSeq;
        }
        #endregion

        #region �����ҩ��ϸ�����ˮ��
        /// <summary>
        /// �����ҩ��ϸ�����ˮ��
        /// </summary>
        /// <param name="m_strOperatorid"></param>
        /// <param name="objList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDSRecipeAccountInfo(string m_strOperatorid, List<clsPutMedicineDetailGroup> objList)
        {
            long lngRes = -1;
            string strSQL = string.Empty;

            if (objList == null)
            {
                return 99;
            }
            // ��ʿִ��ʱ�Ѵ���
            for (int i = objList.Count - 1; i >= 0; i--)
            {
                if (objList[i].m_lngStorageSerial <= 0)
                {
                    objList.RemoveAt(i);
                }
            }
            if (objList.Count == 0)
            {
                return 99;
            }

            clsHRPTableService objHRPServ = new clsHRPTableService();
            strSQL = @" insert into t_ds_putmedaccount_detail a
                              (seriesid_int,
                               medicineid_chr,
                               medicinename_vchr,
                               medicinetypeid_chr,
                               medspec_vchr,
                               packqty_dec,
                               drugstoreid_int,
                               lotno_vchr,
                               validperiod_dat,
                               ipretailprice_int,
                               opretailprice_int,
                               ipunit_chr,
                               ipamount_int,
                               opamount_int,
                               opunit_chr,
                               ipoldgross_int,
                               opoldgross_int,
                               type_int,
                               state_int,
                               isend_int,
                               endipamount_int,
                               endopamount_int,
                               endipretailprice_int,
                               endopretailprice_int,
                               inaccountid_chr,
                               inaccountdate_dat,
                               accountid_chr,
                               productorid_chr,
                               operatedate_dat,
                               putmeddetailid_chr,
                               medseriesid_int,
                               operatorid_chr,
                               ipavaigross_int,
                               opavaigross_int,deptid_chr)
                              select seq_ds_putmedaccount_detail.nextval,
                                     b.medicineid_chr,
                                     b.medicinename_vchr,
                                     c.medicinetypeid_chr,
                                     b.medspec_vchr,
                                     b.packqty_dec,
                                     b.drugstoreid_chr,
                                     b.lotno_vchr,
                                     b.validperiod_dat,
                                     b.ipretailprice_int,
                                     b.opretailprice_int,
                                     b.ipunit_chr,
                                     ?,
                                     ?,
                                     b.opunit_chr,
                                     ?,
                                     ?,
                                     ?,
                                     1,
                                     0,
                                     null,
                                     null,
                                     null,
                                     null,
                                     ?,
                                     sysdate,
                                     null,
                                     b.productorid_chr,
                                     sysdate,
                                     ?,
                                     b.seriesid_int,
                                     ?,
                                     b.ipavailablegross_num,
                                     b.opavailablegross_num,?
                                from t_ds_storage_detail b, t_bse_medicine c
                               where b.seriesid_int = ?
                                 and b.medicineid_chr = c.medicineid_chr(+)";
            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int64 };
            object[][] objValuesArr = new object[10][];
            object[][] objValuesArr1 = new object[10][];
            int m_intCount = objList.Count;
            for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
            {
                objValuesArr[j] = new object[m_intCount];
            }
            clsPutMedicineDetailGroup m_objTempVo = null;
            clsPutMedicineDetailGroup m_objTempVo1 = null;
            long lngAffected = -1;
            long lngAffected1 = -1;
            // ���ORA-01795����
            if (m_intCount >= 1000)
            {
                List<clsPutMedicineDetailGroup> objList1 = new List<clsPutMedicineDetailGroup>();
                List<clsPutMedicineDetailGroup> objList2 = new List<clsPutMedicineDetailGroup>();
                for (int k = 0; k < m_intCount; k++)
                {
                    if (k < 1000)
                    {
                        objList1.Add(objList[k]);
                    }
                    else
                    {
                        objList2.Add(objList[k]);
                    }
                }
                for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
                {
                    objValuesArr[j] = new object[objList1.Count];
                }
                for (int j = 0; j < objValuesArr1.Length; j++)//��ʼ������
                {
                    objValuesArr1[j] = new object[objList2.Count];
                }
                for (int j = 0; j < objList1.Count; j++)
                {
                    m_objTempVo = objList1[j];
                    objValuesArr[0][j] = m_objTempVo.m_dblIPAmount;
                    objValuesArr[1][j] = m_objTempVo.m_dblOPAmount;
                    objValuesArr[2][j] = m_objTempVo.m_dblIPDownGross;
                    objValuesArr[3][j] = m_objTempVo.m_dblOPDownGross;
                    objValuesArr[4][j] = 1;
                    objValuesArr[5][j] = m_strOperatorid;
                    objValuesArr[6][j] = m_objTempVo.m_strPutMedDetaileId;
                    objValuesArr[7][j] = m_strOperatorid;
                    objValuesArr[8][j] = m_objTempVo.m_strDrugStoreid;
                    objValuesArr[9][j] = m_objTempVo.m_lngStorageSerial;
                }
                for (int m = 0; m < objList2.Count; m++)
                {
                    m_objTempVo1 = objList1[m];
                    objValuesArr1[0][m] = m_objTempVo1.m_dblIPAmount;
                    objValuesArr1[1][m] = m_objTempVo1.m_dblOPAmount;
                    objValuesArr1[2][m] = m_objTempVo1.m_dblIPDownGross;
                    objValuesArr1[3][m] = m_objTempVo1.m_dblOPDownGross;
                    objValuesArr1[4][m] = 1;
                    objValuesArr1[5][m] = m_strOperatorid;
                    objValuesArr1[6][m] = m_objTempVo1.m_strPutMedDetaileId;
                    objValuesArr1[7][m] = m_strOperatorid;
                    objValuesArr1[8][m] = m_objTempVo1.m_strDrugStoreid;
                    objValuesArr1[9][m] = m_objTempVo1.m_lngStorageSerial;
                }
                try
                {

                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr, ref lngAffected, dbTypes);
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr1, ref lngAffected1, dbTypes);
                    if (lngAffected != objList1.Count && lngAffected1 != objList2.Count)
                    {
                        lngRes = -1;
                        ContextUtil.SetAbort();
                    }
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                for (int i = 0; i < m_intCount; i++)
                {
                    m_objTempVo = objList[i];
                    objValuesArr[0][i] = m_objTempVo.m_dblIPAmount;
                    objValuesArr[1][i] = m_objTempVo.m_dblOPAmount;
                    objValuesArr[2][i] = m_objTempVo.m_dblIPDownGross;
                    objValuesArr[3][i] = m_objTempVo.m_dblOPDownGross;
                    objValuesArr[4][i] = 1;
                    objValuesArr[5][i] = m_strOperatorid;
                    objValuesArr[6][i] = m_objTempVo.m_strPutMedDetaileId;
                    objValuesArr[7][i] = m_strOperatorid;
                    objValuesArr[8][i] = m_objTempVo.m_strDrugStoreid;
                    objValuesArr[9][i] = m_objTempVo.m_lngStorageSerial;
                }
                try
                {

                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr, ref lngAffected, dbTypes);
                    if (lngAffected != m_intCount)
                    {
                        lngRes = -1;
                        ContextUtil.SetAbort();
                    }
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��ҩ��ϸ��Ϣ
        /// <summary>
        /// ��ȡ��ҩ��ϸ��Ϣ
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="p_strDrugStoreid">ҩ��ID</param>
        /// <param name="p_dtbPutMed">��ҩ��Ϣ</param>
        /// <param name="objList">��ҩ��ϸ��ˮ��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedicineDetailGroup(Dictionary<string, clsPutMedicineDetailGroup> hsTable, string p_strDrugStoreid, DataTable p_dtbPutMed, out List<clsPutMedicineDetailGroup> objList)
        {
            long lngRes = 0;
            string strSeri = "";
            objList = new List<clsPutMedicineDetailGroup>();

            if (hsTable == null || hsTable.Count == 0)
            {
                return 99;
            }

            List<long> lstSeri = new List<long>();
            Dictionary<string, List<double>> p_dtnGross = new Dictionary<string, List<double>>();
            string strSQL = @"select a.seriesid_int, a.iprealgross_int, a.oprealgross_int,a.drugstoreid_chr,a.medicineid_chr
  from t_ds_storage_detail a
 where  a.seriesid_int in ([seri])
 order by a.validperiod_dat";
            clsHRPTableService objHRPSvc = null;

            try
            {
                int intRowscount = p_dtbPutMed.Rows.Count;
                clsPutMedicineDetailGroup objVo = null;
                DataRow dr2 = null;
                for (int i1 = 0; i1 < intRowscount; i1++)//����ҩ��ϸ��Ϣ��ֵ����ҩ��ˮ����ϸ����ϢVO
                {
                    dr2 = p_dtbPutMed.Rows[i1];
                    objVo = new clsPutMedicineDetailGroup();
                    objVo.m_strMedicineid_chr = dr2["medid_chr"].ToString();
                    objVo.m_strMedicineName = dr2["medicinename_vchr"].ToString();
                    objVo.m_strDrugStoreid = p_strDrugStoreid;
                    objVo.m_dblPackage = Convert.ToDouble(dr2["packqty_dec"]);
                    if (dr2["ipchargeflg_int"].ToString() == "0")
                    {
                        objVo.m_dblOPAmount = Convert.ToDouble(dr2["get_dec"]);
                        objVo.m_dblIPAmount = Convert.ToDouble(dr2["get_dec"]) * Convert.ToDouble(dr2["packqty_dec"]);
                    }
                    else
                    {
                        objVo.m_dblOPAmount = Math.Round(Convert.ToDouble(dr2["get_dec"]) / Convert.ToDouble(dr2["packqty_dec"]), 4);
                        objVo.m_dblIPAmount = Convert.ToDouble(dr2["get_dec"]);
                    }
                    objVo.m_strPutMedDetaileId = dr2["PUTMEDDETAILID_CHR"].ToString();

                    objList.Add(objVo);
                }
                foreach (clsPutMedicineDetailGroup de in hsTable.Values)//��ȡ��Ҫ�ۼ����ҩƷ����������
                {
                    for (int i = 0; i < de.m_listSubStorageDetail.Count; i++)
                    {
                        if (!lstSeri.Contains(de.m_listSubStorageDetail[i].m_lngStorageSerial))
                        {
                            lstSeri.Add(de.m_listSubStorageDetail[i].m_lngStorageSerial);
                        }
                    }
                }
                if (lstSeri.Count > 1000)
                {
                    int intArrayCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstSeri.Count / 1000.00)));
                    string[] strSeriArr = new string[intArrayCount];
                    for (int i3 = 0; i3 < lstSeri.Count; i3++)
                    {
                        strSeriArr[Convert.ToInt32(Math.Floor(Convert.ToDouble(i3 / 1000)))] += lstSeri[i3].ToString() + ",";
                    }
                    for (int i = 0; i < strSeriArr.Length; i++)
                    {
                        strSeriArr[i] = strSeriArr[i].TrimEnd(',');
                    }
                    string strTemp = string.Empty;
                    foreach (string str in strSeriArr)
                    {
                        strTemp += " a.seriesid_int in (" + str + ") or ";
                    }
                    //strTemp = strTemp.TrimEnd("or".ToCharArray());
                    strTemp = strTemp.Substring(0, strTemp.Length - 3);
                    strSQL = strSQL.Replace("a.seriesid_int in ([seri])", strTemp);
                }
                else
                {
                    for (int i3 = 0; i3 < lstSeri.Count; i3++)
                    {
                        strSeri += lstSeri[i3].ToString() + ",";
                    }
                    strSeri = strSeri.TrimEnd(',');
                    strSQL = strSQL.Replace("[seri]", strSeri);
                }
                DataTable dtbResult = new DataTable();
                objHRPSvc = new clsHRPTableService();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)//����ÿ����ҩ��ϸҩƷ�ۼ������ʣ�µĿ�������Լ���¼����
                {
                    DataRow[] drArr = null;
                    int intlstCount = objList.Count;
                    for (int i4 = 0; i4 < intlstCount; i4++)
                    {
                        drArr = dtbResult.Select("medicineid_chr='" + objList[i4].m_strMedicineid_chr + "'");
                        double m_dblIPAmount = objList[i4].m_dblIPAmount;
                        double m_dblOPAmount = objList[i4].m_dblOPAmount;
                        for (int i5 = 0; i5 < drArr.Length; i5++)
                        {
                            double dblIpGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                            double dblOpGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());

                            if (m_dblIPAmount <= dblIpGross && m_dblIPAmount > 0)
                            {
                                drArr[i5]["iprealgross_int"] = Convert.ToString(dblIpGross - m_dblIPAmount);
                                drArr[i5]["oprealgross_int"] = Convert.ToString(dblOpGross - m_dblOPAmount);
                                objList[i4].m_dblIPDownGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                                objList[i4].m_dblOPDownGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());
                                objList[i4].m_lngStorageSerial = Convert.ToInt32(drArr[i5]["seriesid_int"].ToString());
                                break;
                            }
                            else if (m_dblIPAmount > dblIpGross && m_dblIPAmount > 0)
                            {
                                m_dblIPAmount = m_dblIPAmount - dblIpGross;
                                m_dblOPAmount = m_dblOPAmount - dblOpGross;
                                drArr[i5]["iprealgross_int"] = Convert.ToString(0);
                                drArr[i5]["oprealgross_int"] = Convert.ToString(0);
                                objList[i4].m_dblIPDownGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                                objList[i4].m_dblOPDownGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());
                                objList[i4].m_lngStorageSerial = Convert.ToInt32(drArr[i5]["seriesid_int"].ToString());
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //List<double> lstTemp = null;
                    //for (int i2 = 0; i2 < intRowCount;i2++ )
                    //{
                    //    dr = dtbResult.Rows[i2];

                    //    if (!p_dtnGross.ContainsKey(dr["seriesid_int"].ToString()))
                    //    {
                    //        lstTemp = new List<double>();
                    //        lstTemp.Add(Convert.ToDouble(dr["iprealgross_int"].ToString()));
                    //        lstTemp.Add(Convert.ToDouble(dr["oprealgross_int"].ToString()));
                    //        p_dtnGross.Add(dr["seriesid_int"].ToString(), lstTemp);
                    //    }
                    //    else
                    //    {
                    //        p_dtnGross[dr["seriesid_int"].ToString()][0] = Convert.ToDouble(dr["iprealgross_int"].ToString());
                    //        p_dtnGross[dr["seriesid_int"].ToString()][1] = Convert.ToDouble(dr["oprealgross_int"].ToString());
                    //    }
                    //}

                    //int intlstCount=objList.Count;
                    ////string strSeriesid = "";
                    //for(int i4=0;i4<intlstCount;i4++)
                    //{
                    //    strSeriesid = objList[i4].m_lngStorageSerial.ToString();
                    //    p_dtnGross[strSeriesid][0] = p_dtnGross[strSeriesid][0] - objList[i4].m_dblIPAmount;
                    //    p_dtnGross[strSeriesid][1] = p_dtnGross[strSeriesid][1] - objList[i4].m_dblOPAmount;
                    //    objList[i4].m_dblIPDownGross = p_dtnGross[strSeriesid][0];
                    //    objList[i4].m_dblOPDownGross = p_dtnGross[strSeriesid][1];
                    //}
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
                strSQL = null;
            }
            return lngRes;
        }
        #endregion

    }
}
