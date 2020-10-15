using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �̵�
    /// Create by kong 2004-06-11
    /// </summary>	
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageCheckSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase    //MiddleTierBase.dll
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsStorageCheckSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region �����̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"select max(STORAGECHECKID_CHR) as ID from t_opr_storagecheck where STORAGECHECKID_CHR like '" + DateTime.Parse(p_objItem.m_strCreateDate).ToString("yyyyMMdd") + "%'";
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
            if (lngRes == 1 && dt.Rows.Count > 0 && dt.Rows[0]["ID"] != System.DBNull.Value)
            {
                string Maxstr = dt.Rows[0]["ID"].ToString().Substring(8, 2);
                try
                {
                    int MaxNO = int.Parse(Maxstr) + 1;
                    p_objItem.m_strStorageCheckID = dt.Rows[0]["ID"].ToString().Substring(0, 8) + MaxNO.ToString("00");
                }
                catch
                {
                    p_objItem.m_strStorageCheckID = DateTime.Parse(p_objItem.m_strCreateDate).ToString("yyyyMMdd") + "01";
                }
            }
            else
            {
                p_objItem.m_strStorageCheckID = DateTime.Parse(p_objItem.m_strCreateDate).ToString("yyyyMMdd") + "01";
            }
            strSQL = @"INSERT INTO T_OPR_STORAGECHECK (STORAGECHECKID_CHR,STORAGEID_CHR,STORAGEORDTYPEID_CHR,PERIODID_CHR,CHECK_DAT,PSTATUS_INT,REMARK_VCHR,CREATORID_CHR,CREATEDATE_DAT,ADUITEMP_CHR,ADUITDATE_DAT,ACCTEMP_CHR,ACCTDATE_DAT,FLAG_INT) 
							VALUES ('";
            strSQL += p_objItem.m_strStorageCheckID.Trim() + "','";
            strSQL += p_objItem.m_objStorage.m_strStroageID + "','";
            strSQL += (p_objItem.m_objStorageOrdType != null ? p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID.Trim() : "") + "','";
            strSQL += (p_objItem.m_objPeriod != null ? p_objItem.m_objPeriod.m_strPeriodID : "");
            strSQL += "',TO_DATE('" + p_objItem.m_strCheckDate + "','yyyy-mm-dd hh24:mi:ss')," + p_objItem.m_intStatus.ToString().Trim() + ",'";
            strSQL += p_objItem.m_strRemake.Trim() + "','" + p_objItem.m_objCreator.strEmpID;
            strSQL += "',TO_DATE('" + p_objItem.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss'),'";
            strSQL += (p_objItem.m_objAduitEmp != null ? p_objItem.m_objAduitEmp.strEmpID : "") + "',TO_DATE('" + p_objItem.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss'),'";
            strSQL += (p_objItem.m_objAcctEmp != null ? p_objItem.m_objAcctEmp.strEmpID : "") + "',TO_DATE('" + p_objItem.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss') ,";
            strSQL += p_objItem.m_strFlag + ")";
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

        #region �޸��̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �޸��̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_STORAGECHECK
							SET CHECK_DAT=TO_DATE('" + p_objItem.m_strCheckDate + "','yyyy-mm-dd hh24:mi:ss'),PSTATUS_INT=" + p_objItem.m_intStatus.ToString().Trim() +
                ",REMARK_VCHR='" + p_objItem.m_strRemake + "' " +
                " WHERE STORAGECHECKID_CHR='" + p_objItem.m_strStorageCheckID + "' AND STORAGEID_CHR='" + p_objItem.m_objStorage.m_strStroageID.Trim() +
                "' AND STORAGEORDTYPEID_CHR='" + p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID.Trim() +
                "' AND PERIODID_CHR='" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ɾ���̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ɾ���̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDeleteStorageCheck(string p_strID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE T_OPR_STORAGECHECK
							WHERE STORAGECHECKID_CHR='" + p_strID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute("delete t_opr_storagecheckdetail where storagecheckid_chr ='" + p_strID + "'");//��ɾ��ϸ
                lngRes = objHRPSvc.DoExcute(strSQL);    //��ɾ����


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

        #region �����̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �����̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO T_OPR_STORAGECHECKDETAIL (STORAGECHECKDETAILID_CHR,STORAGECHECKID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,SYSLOTNO_CHR,CALCQTY_DEC,REALQTY_DEC,BUYPRICE_MNY,CURPRICE_MNY) 
							VALUES ('" + p_objItem.m_strStorageCheckDetailID.Trim() + "','" + p_objItem.m_strStorageCheckID.Trim() + "','" + p_objItem.m_strRowNo.Trim() + "','" + p_objItem.m_strMEDICINEID_CHR.Trim() +
                "','" + p_objItem.m_strUnit + "','" + p_objItem.m_strSysLotNo.Trim() + "'," + p_objItem.m_fltCalcQty.ToString().Trim() + "," + p_objItem.m_fltRealQty.ToString().Trim() +
                "," + p_objItem.m_fltBuyPrice.ToString().Trim() + "," + p_objItem.m_fltSalePrice.ToString().Trim() + " )";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region �����̵���ϸ��  ��ʤ��  2005-04-27
        /// <summary>
        /// �����̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewStorageChekDetail(clsStorageCheckDetail_VO[] p_objItem, string strReMark)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (p_objItem.Length <= 0)
            {
                return -1;
            }
            try
            {
                string strDeleteSQL = @"DELETE T_OPR_STORAGECHECKDETAIL WHERE STORAGECHECKID_CHR='" + p_objItem[0].m_strStorageCheckID.Trim() + "'";


                try
                {
                    lngRes = objHRPSvc.DoExcute(strDeleteSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                strDeleteSQL = @"update t_opr_storagecheck set REMARK_VCHR='" + strReMark + "' WHERE STORAGECHECKID_CHR='" + p_objItem[0].m_strStorageCheckID.Trim() + "'";


                try
                {
                    lngRes = objHRPSvc.DoExcute(strDeleteSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                string newID = "";
                for (int i = 0; i < p_objItem.Length; i++)
                {
                    if (p_objItem[i] != null)
                    {
                        lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storagecheckdetail", "storagecheckdetailid_chr", out newID);
                        string strSQL = @"insert into t_opr_storagecheckdetail(storagecheckdetailid_chr, storagecheckid_chr, medicineid_chr, calcqty_dec, realqty_dec, 
							 rowno_chr, buyprice_mny, unitid_chr,checkreason_vchr,lotno_vchr,CURPRICE_MNY) VALUES ('"
                            + newID + "','";
                        strSQL += p_objItem[i].m_strStorageCheckID.Trim() + "','";
                        strSQL += p_objItem[i].m_strMEDICINEID_CHR.Trim() + "',";
                        strSQL += p_objItem[i].m_fltCalcQty.ToString().Trim() + ",";
                        strSQL += p_objItem[i].m_fltRealQty.ToString().Trim() + ",'";
                        strSQL += p_objItem[i].m_strRowNo.Trim() + "',";
                        strSQL += p_objItem[i].m_fltBuyPrice.ToString().Trim() + ",'";
                        strSQL += p_objItem[i].m_strUnit.Trim() + "','";
                        strSQL += p_objItem[i].m_strCheckReason + "','";
                        strSQL += p_objItem[i].m_strSysLotNo + "',";
                        strSQL += p_objItem[i].m_fltSalePrice + ")";
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
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;

        }
        #endregion

        #region �̵㵥���  ��ʤ��  2005-04-27
        /// <summary>
        /// �����̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵���ϸ����</param>
        /// <param name="strAuditorID">�����</param>
        /// <param name="strAuditDate">���ʱ��</param>
        /// <param name="p_strStorageFlag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAuditCheckBill(clsStorageCheckDetail_VO[] p_objItem, string strAuditorID, string strAuditDate, string p_strStorageFlag)
        {
            long lngRes = 0;
            if (p_strStorageFlag.Length == 0)
            {
                p_strStorageFlag = "0";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            if (p_objItem.Length <= 0)
            {
                return -1;
            }
            try
            {
                string strSQL = @"update t_opr_storagecheck 
										set pstatus_int = '1',ADUITEMP_CHR = '" + strAuditorID + "',ADUITDATE_DAT = to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"','yyyy-mm-dd hh24:mi:ss') 
										 where storagecheckid_chr = '" + p_objItem[0].m_strStorageCheckID + "'";
                lngRes = objHRPSvc.DoExcute(strSQL);
                clsMedStorageManage mange = new com.digitalwave.iCare.middletier.HIS.clsMedStorageManage();
                for (int i = 0; i < p_objItem.Length; i++)
                {
                    //��˸��¿��
                    strSQL = "UPDATE t_bse_storagemedicine SET amount_dec = amount_dec -" + p_objItem[i].m_fltCalcQty + " WHERE medicineid_chr='" + p_objItem[i].m_strMEDICINEID_CHR + "' and storageid_chr='" + p_objItem[i].m_strstorageID + "' and FLAG_INT=" + p_strStorageFlag;  //�����ܿ��
                    objHRPSvc.DoExcute(strSQL);
                    strSQL = "update t_opr_storagemeddetail set CURQTY_DEC=" + p_objItem[i].m_fltRealQty + " where STORAGEID_CHR='" + p_objItem[i].m_strstorageID + "' and medicineid_chr='" + p_objItem[i].m_strMEDICINEID_CHR + "' and SYSLOTNO_CHR='" + p_objItem[i].m_strSysLotNo + "' and FLAG_INT=" + p_strStorageFlag;
                    objHRPSvc.DoExcute(strSQL);
                    strSQL = "update t_opr_storagecheckdetail set SALEUNITPRICE_MNY=" + p_objItem[i].m_fltSalePrice + " where STORAGECHECKID_CHR='" + p_objItem[0].m_strStorageCheckID + "' and medicineid_chr='" + p_objItem[i].m_strMEDICINEID_CHR + "' and LOTNO_VCHR='" + p_objItem[i].m_strSysLotNo + "'";
                    objHRPSvc.DoExcute(strSQL);
                    if (lngRes < 0)
                    {
                        throw new Exception("������");
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


        #region �޸��̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �޸��̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵���ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_STORAGECHECKDETAIL
							SET ROWNO_CHR='" + p_objItem.m_strRowNo.Trim() + "',UNITID_CHR='" + p_objItem.m_strUnit.Trim() +
                "',REALQTY_DEC=" + p_objItem.m_fltRealQty.ToString().Trim() + ",BUYPRICE_MNY=" + p_objItem.m_fltBuyPrice.ToString().Trim() +
                ",CURPRICE_MNY=" + p_objItem.m_fltSalePrice.ToString().Trim() + ",CALCQTY_DEC=" + p_objItem.m_fltCalcQty.ToString().Trim() + " " +
                " WHERE STORAGECHECKDETAILID_CHR='" + p_objItem.m_strStorageCheckDetailID.Trim() +
                "' AND MEDICINEID_CHR='" + p_objItem.m_strMEDICINEID_CHR +
                "' AND SYSLOTNO_CHR='" + p_objItem.m_strSysLotNo.Trim() + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ɾ���̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ɾ���̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵���ϸ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDeleteStorageChekDetail(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE T_OPR_STORAGECHECKDETAIL
							WHERE STORAGECHECKDETAILID_CHR='" + p_strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ����̵㵥  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ����̵㵥
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAduit(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_STORAGECHECK
							SET ADUITEMP_CHR='" + p_objItem.m_objAduitEmp.strEmpID.Trim() + "',ADUITDATE_DAT=TO_DATE('" + p_objItem.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss') " +
                " WHERE STORAGECHECKID_CHR='" + p_objItem.m_strStorageCheckID + "' AND STORAGEID_CHR='" + p_objItem.m_objStorage.m_strStroageID.Trim() +
                "' AND STORAGEORDTYPEID_CHR='" + p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID.Trim() +
                "' AND PERIODID_CHR='" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region ����̵㵥�󣬸ı���  δ���
        /// <summary>
        /// ����̵㵥�󣬸ı���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵㵥��</param>
        /// <param name="p_intFlag">�����Ϣ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeStorageAfterAduit(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            try
            {
                string strProcedure = "P_STORAGECHECKADUIT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "checkid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());

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

        #region ��DataTable���ݵ�VO��ȥ

        #region ���ݵ���¼��VO  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ��DataTable���ݴ��ݵ���¼��VO
        /// </summary>
        /// <param name="dtbSource">ԴDataTable</param>
        /// <param name="objItemArr">��¼��VO</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageCheck_VO[] objItemArr)
        {
            objItemArr = new clsStorageCheck_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItemArr = new clsStorageCheck_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItemArr[i] = new clsStorageCheck_VO();
                            objItemArr[i].m_strStorageCheckID = dtbSource.Rows[i]["STORAGECHECKID_CHR"].ToString().Trim();
                            objItemArr[i].m_objStorage = new clsStorage_VO();
                            objItemArr[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["STORAGEID_CHR"].ToString().Trim();
                            objItemArr[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["STORAGENAME_VCHR"].ToString().Trim();
                            objItemArr[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItemArr[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                            objItemArr[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();
                            objItemArr[i].m_objStorageOrdType.m_intSign = int.Parse(dtbSource.Rows[i]["SIGN_INT"].ToString().Trim());
                            objItemArr[i].m_objStorageOrdType.m_intDeptType = int.Parse(dtbSource.Rows[i]["DEPTTYPE_INT"].ToString().Trim());
                            objItemArr[i].m_objPeriod = new clsPeriod_VO();
                            objItemArr[i].m_objPeriod.m_strPeriodID = dtbSource.Rows[i]["PERIODID_CHR"].ToString().Trim();
                            objItemArr[i].m_objPeriod.m_strStartDate = dtbSource.Rows[i]["STARTDATE_DAT"].ToString().Trim();
                            objItemArr[i].m_objPeriod.m_strEndDate = dtbSource.Rows[i]["ENDDATE_DAT"].ToString().Trim();
                            objItemArr[i].m_strCheckDate = dtbSource.Rows[i]["CHECK_DAT"].ToString().Trim();
                            objItemArr[i].m_intStatus = int.Parse(dtbSource.Rows[i]["PSTATUS_INT"].ToString().Trim());
                            objItemArr[i].m_strRemake = dtbSource.Rows[i]["REMARK_VCHR"].ToString().Trim();
                            objItemArr[i].m_objCreator = new clsEmployeeVO();
                            objItemArr[i].m_objCreator.strEmpID = dtbSource.Rows[i]["CREATORID_CHR"].ToString().Trim();
                            objItemArr[i].m_strCreateDate = dtbSource.Rows[i]["CREATEDATE_DAT"].ToString().Trim();
                            objItemArr[i].m_objAduitEmp = new clsEmployeeVO();
                            objItemArr[i].m_objAduitEmp.strEmpID = dtbSource.Rows[i]["ADUITEMP_CHR"].ToString().Trim();
                            objItemArr[i].m_strAduitDate = dtbSource.Rows[i]["ADUITDATE_DAT"].ToString().Trim();
                            objItemArr[i].m_objAcctEmp = new clsEmployeeVO();
                            objItemArr[i].m_objAcctEmp.strEmpID = dtbSource.Rows[i]["ACCTEMP_CHR"].ToString().Trim();
                            objItemArr[i].m_strAcctDate = dtbSource.Rows[i]["ACCTDATE_DAT"].ToString().Trim();
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

        }
        #endregion

        #region ���ݵ���ϸVO  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ��DataTable���ݴ��ݵ���ϸVO
        /// </summary>
        /// <param name="dtbSource">ԴDataTable</param>
        /// <param name="objItemArr">��ϸVO</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageCheckDetail_VO[] objItemArr)
        {
            objItemArr = new clsStorageCheckDetail_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItemArr = new clsStorageCheckDetail_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItemArr[i] = new clsStorageCheckDetail_VO();
                            objItemArr[i].m_strStorageCheckDetailID = dtbSource.Rows[i]["STORAGECHECKDETAILID_CHR"].ToString().Trim();
                            objItemArr[i].m_strStorageCheckID = dtbSource.Rows[i]["STORAGECHECKID_CHR"].ToString().Trim();
                            objItemArr[i].m_strRowNo = dtbSource.Rows[i]["ROWNO_CHR"].ToString().Trim();
                            objItemArr[i].m_strSysLotNo = dtbSource.Rows[i]["SYSLOTNO_CHR"].ToString().Trim();
                            objItemArr[i].m_fltCalcQty = float.Parse(dtbSource.Rows[i]["CALCQTY_DEC"].ToString().Trim());
                            objItemArr[i].m_fltRealQty = float.Parse(dtbSource.Rows[i]["REALQTY_DEC"].ToString().Trim());
                            objItemArr[i].m_fltBuyPrice = float.Parse(dtbSource.Rows[i]["BUYPRICE_MNY"].ToString().Trim());
                            objItemArr[i].m_fltSalePrice = float.Parse(dtbSource.Rows[i]["CURPRICE_MNY"].ToString().Trim());
                            objItemArr[i].m_strUnit = dtbSource.Rows[i]["UNITID_CHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine = new clsStorageMedDetail_VO();
                            //							objItemArr[i].m_objMedicine.m_objMedicine = new clsMedicine_VO();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["MEDICINEID_CHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["MEDICINENAME_VCHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["MEDSPEC_VCHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strPYCode = dtbSource.Rows[i]["PYCODE_CHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strWBCode = dtbSource.Rows[i]["WBCODE_CHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["MEDICINEENGNAME_VCHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_strLotNo = dtbSource.Rows[i]["LOTNO_VCHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objProduct = new clsVendor_VO();
                            //							objItemArr[i].m_objMedicine.m_objProduct.m_strVendorID = dtbSource.Rows[i]["PRODUCTORID_CHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_objProduct.m_strVendorName = dtbSource.Rows[i]["VENDORNAME_VCHR"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_fltCurQty = float.Parse(dtbSource.Rows[i]["CURQTY_DEC"].ToString().Trim());
                            //							objItemArr[i].m_objMedicine.m_strUsefulLife = dtbSource.Rows[i]["USEFULLIFE_DAT"].ToString().Trim();
                            //							objItemArr[i].m_objMedicine.m_fltBuyUnitPrice = float.Parse(dtbSource.Rows[i]["BUYUNITPRICE_MNY"].ToString().Trim());
                            //							objItemArr[i].m_objMedicine.m_fltSaleUnitPrice = float.Parse(dtbSource.Rows[i]["SALEUNITPRICE_MNY"].ToString().Trim());
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
        }
        #endregion

        #endregion

        #region ģ�������̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ģ�������̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByAny(string p_strSQL, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = @"SELECT *
							    FROM v_opr_storagecheck
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region �����̵��¼��  ��ʤ��  2005-04-26
        /// <summary>
        /// �����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>		
        /// <param name="dtResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByAny(out System.Data.DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;

            string strSQL = @"SELECT a.*,b.firstname_vchr||b.lastname_vchr as CreaorName,c.firstname_vchr||c.lastname_vchr as AuditName
							    FROM t_opr_storagecheck a,t_bse_employee b,t_bse_employee c
                   where a.creatorid_chr = b.empid_chr
                   and c.empid_chr(+)= a.aduitemp_chr order by storagecheckid_chr desc";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

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

        #region �����̵��¼��ϸ  ��ʤ��  2005-04-26
        /// <summary>
        /// �����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="strStorageID">�ֿ��</param>		
        /// <param name="strCheckBillID">�̵㵥��</param>
        /// <param name="dtResult">�������</param>
        /// <param name="isEm">�Ƿ��Ѿ���˵ĵ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckDetail(string strCheckBillID, out System.Data.DataTable dtResult, string str1, bool isEm)
        {
            long lngRes = 0;
            dtResult = null;
            string strCol = "";
            if (isEm)
            {
                strCol = "b.SALEUNITPRICE_MNY";
            }
            else
            {
                strCol = "c.unitprice_mny";
            }
            string strSQL = @"SELECT    c.assistcode_chr," + strCol + @" as unitprice_mny, c.medicinename_vchr,
                 c.pycode_chr, c.wbcode_chr, c.medspec_vchr,
                 TO_CHAR (a.usefullife_dat, 'yyyy-mm-dd') AS usefullife,
                 a.productorid_chr, a.wholesaleunitprice_mny, a.storageid_chr,
                 a.unitid_chr AS unitname_chr, a.lotno_vchr AS medicinnumber,
                 a.buyunitprice_mny,
                 b.calcqty_dec AS curqty_dec,
                 b.calcqty_dec * buyunitprice_mny AS calcmoney, b.realqty_dec,
                 b.realqty_dec * buyunitprice_mny AS realmoney,
                 b.realqty_dec - b.calcqty_dec AS lostnum,
                   b.realqty_dec * buyunitprice_mny
                 - b.calcqty_dec * buyunitprice_mny AS lostmoney,
                 (b.realqty_dec - b.calcqty_dec)*" + strCol + @" as lostSalmoney,
                 b.checkreason_vchr, a.unitid_chr, c.medicinetypeid_chr,
                 c.medicinepreptype_chr, a.medicineid_chr, a.syslotno_chr,
                 d.pharmaid_chr, d.parentid_chr, d.pharmaname_vchr AS ����,
                 f.itemid_chr,
                 (SELECT k.pharmaname_vchr
                    FROM t_bse_pharmatype k
                   WHERE k.pharmaid_chr = d.parentid_chr) AS ����
            FROM t_opr_storagemeddetail a,
                 t_opr_storagecheckdetail b,
                 t_bse_medicine c,
                 t_bse_pharmatype d,
                 t_bse_chargeitem f
           WHERE b.medicineid_chr = a.medicineid_chr
             AND a.medicineid_chr = f.itemsrcid_vchr
             AND b.storagecheckid_chr = '" + strCheckBillID + @"'
             AND b.lotno_vchr = a.syslotno_chr
             AND a.medicineid_chr = c.medicineid_chr
             AND a.flag_int = " + str1 + @" 
             AND c.pharmaid_chr = d.pharmaid_chr(+)
        ORDER BY c.assistcode_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

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

        #region ��ȡ��ǰ�����ϸ����
        /// <summary>
        /// ��ȡ��ǰ�����ϸ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageCheckDetail(string strStorageID, out System.Data.DataTable dtResult, string strFlag, clsHISMedType_VO[] medType, clsMedicinePrepType_VO[] PrepType, clsMedicineType_VO[] MedicineType, bool isShowZero, bool isShowStop)
        {
            long lngRes = 0;
            dtResult = null;
            string strWhere = "";
            if (medType != null && PrepType != null)
            {
                for (int i1 = 0; i1 < PrepType.Length; i1++)
                {
                    if (i1 == 0)
                    {
                        if (PrepType.Length > 1)
                        {
                            strWhere += "  and  (c.MEDICINEPREPTYPE_CHR='" + PrepType[i1].m_strMedicinePrepTypeID + "'";
                        }
                        else
                        {
                            strWhere += "  and  c.MEDICINEPREPTYPE_CHR='" + PrepType[i1].m_strMedicinePrepTypeID + "'";
                        }
                    }
                    else
                    {
                        if (i1 == PrepType.Length - 1)
                        {
                            strWhere += "  or c.MEDICINEPREPTYPE_CHR='" + PrepType[i1].m_strMedicinePrepTypeID + "')";
                        }
                        else
                        {
                            strWhere += "  or c.MEDICINEPREPTYPE_CHR='" + PrepType[i1].m_strMedicinePrepTypeID + "'";
                        }
                    }
                }


                for (int i1 = 0; i1 < medType.Length; i1++)
                {
                    if (i1 == 0)
                    {
                        if (medType.Length > 1)
                        {

                            strWhere += "  and  (d.pharmaid_chr='" + medType[i1].m_strPHARMAID_CHR + "'";
                        }
                        else
                        {
                            strWhere += "  and  d.pharmaid_chr='" + medType[i1].m_strPHARMAID_CHR + "'";
                        }
                    }
                    else
                    {
                        if (i1 == medType.Length - 1)
                        {
                            strWhere += "  or d.pharmaid_chr='" + medType[i1].m_strPHARMAID_CHR + "')";
                        }
                        else
                        {
                            strWhere += "  or d.pharmaid_chr='" + medType[i1].m_strPHARMAID_CHR + "'";
                        }
                    }
                }
            }
            else
            {
                if (MedicineType != null && MedicineType.Length > 0)
                {
                    for (int i1 = 0; i1 < MedicineType.Length; i1++)
                    {
                        if (i1 == 0)
                        {
                            if (MedicineType.Length == 1)
                            {
                                strWhere += "  and (c.MEDICINETYPEID_CHR='" + MedicineType[i1].m_strMedicineTypeID + "')";
                            }
                            else
                            {
                                strWhere += "  and (c.MEDICINETYPEID_CHR='" + MedicineType[i1].m_strMedicineTypeID + "'";
                            }
                        }
                        else
                        {
                            if (i1 == MedicineType.Length - 1)
                                strWhere += "   or c.MEDICINETYPEID_CHR='" + MedicineType[i1].m_strMedicineTypeID + "')";
                            else
                                strWhere += "   or c.MEDICINETYPEID_CHR='" + MedicineType[i1].m_strMedicineTypeID + "'";
                        }
                    }
                }

            }
            if (isShowZero == false)
            {
                strWhere += " and a.curqty_dec > 0";
            }
            if (isShowStop == false)
            {
                strWhere += " and c.IFSTOP_INT = 0";
            }

            string strSQL = @"SELECT   c.assistcode_chr, c.unitprice_mny, c.medicinename_vchr,
                 c.pycode_chr, c.wbcode_chr, c.medspec_vchr,
                 TO_CHAR (a.usefullife_dat, 'yyyy-mm-dd') AS usefullife,a.storageid_chr,
                 a.productorid_chr, a.wholesaleunitprice_mny,
                 a.unitid_chr AS unitname_chr, a.lotno_vchr AS medicinnumber,
                 a.buyunitprice_mny, a.curqty_dec,
                 a.curqty_dec AS realqty_dec, '0' AS lostnum,
                 '' AS checkreason_vchr, '0' AS lostmoney, a.unitid_chr,
                 c.medicinetypeid_chr, a.medicineid_chr, a.syslotno_chr,
                 a.curqty_dec * buyunitprice_mny AS calcmoney,
                 a.curqty_dec * buyunitprice_mny AS realmoney,d.pharmaid_chr,
                 d.parentid_chr,d.pharmaname_vchr AS ����,f.itemid_chr,
                 (SELECT k.pharmaname_vchr
                    FROM t_bse_pharmatype k
                   WHERE k.pharmaid_chr = d.parentid_chr) AS ����,
                 c.medicinepreptype_chr,'0' AS lostSalmoney
            FROM t_opr_storagemeddetail a,
                 t_bse_medicine c,
                 t_bse_pharmatype d,
                 t_bse_chargeitem f
           WHERE a.medicineid_chr = c.medicineid_chr
             AND a.medicineid_chr = f.itemsrcid_vchr
             AND a.flag_int = " + strFlag + @"
             AND a.storageid_chr ='" + strStorageID + @"'
             AND c.pharmaid_chr = d.pharmaid_chr(+)" + strWhere + @" 
        ORDER BY c.assistcode_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

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


        #region ���̵㵥�Ų����̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ���̵㵥�Ų����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵㵥��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByID(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = "WHERE STORAGECHECKID_CHR='" + p_strID + "' ";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ���ֿ�����̵��¼�� ŷ����ΰ  2004-06-11
        /// <summary>
        /// ���ֿ�����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByStorage(string p_strStorageID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = " WHERE STORAGEID_CHR='" + p_strStorageID + "' ";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ���������Ͳ����̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ���������Ͳ����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strTypeID">��������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByType(string p_strTypeID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = " WHERE STORAGEORDTYPEID_CHR='" + p_strTypeID + "' ";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region �������ڲ����̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �������ڲ����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strPeriodID">������ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByPeriod(string p_strPeriodID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = " WHERE PERIODID_CHR='" + p_strPeriodID + "' ";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ��ȡ�������� ��ʤ��
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPeriod(out System.Data.DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            string strSQL = "SELECT PERIODID_CHR,STARTDATE_DAT,ENDDATE_DAT,to_char(STARTDATE_DAT,'yyyy-mm-dd')||'��'||to_char(ENDDATE_DAT,'yyyy-mm-dd') as PERIODNAME FROM t_bse_period order by STARTDATE_DAT";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
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

        #region ��ʱ��β����̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ��ʱ��β����̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckByDate(string p_strStartDate, string p_strEndDate, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = " WHERE CHECK_DAT >= TO_DATE('" + p_strStartDate + "','yyyy-mm-dd hh24:mi:ss')  AND CHECK_DAT <= TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region �������е��̵��¼��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// �������е��̵��¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllStorageCheck(out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            string strSQL = " ";
            lngRes = m_lngFindStorageCheckByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ��ȡ�ֿ��¼
        /// <summary>
        /// ��ȡ�ֿ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllStorage(out System.Data.DataTable dt)
        {
            dt = null;
            long lngRes = 0;

            string strSQL = @"SELECT *
							    FROM T_BSE_STORAGE";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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
        #endregion	��ȡ�ֿ��¼

        #region ��ȡҩƷ��������
        /// <summary>
        /// ��ȡҩƷ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedinicePrepType(out System.Data.DataTable dt, out System.Data.DataTable dt1, out System.Data.DataTable dt2)
        {
            long lngRes = 0;
            dt = null;
            dt1 = null;
            dt2 = null;

            string strSQL = @"SELECT *
							    FROM T_AID_MEDICINEPREPTYPE";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT ASSISTCODE_VCHR,PHARMANAME_VCHR,PYCODE_VCHR,WBCODE_VCHR,PHARMAID_CHR, PARENTID_CHR FROM t_bse_pharmatype ";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt1);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT * FROM T_Aid_MedicineType";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt2);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion ��ȡҩƷ��������

        #region ģ�������̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ģ�������̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckDetailByAny(string p_strSQL, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            string strSQL = @"SELECT *
								FROM v_opr_storagecheckdetail
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region  ���̵��¼���Ų����̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ���̵��¼���Ų����̵���ϸ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�̵��¼����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckDetailByCheckID(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            string strSQL = " WHERE STORAGECHECKID_CHR='" + p_strID + "' ";
            lngRes = m_lngFindStorageCheckDetailByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ��ҩƷ�����̵���ϸ��  ŷ��ΰ  2004-06-11
        /// <summary>
        /// ��ҩƷ�����̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageCheckDetailByMedicine(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            string strSQL = " WHERE MEDICINEID_CHR='" + p_strID + "' ";
            lngRes = m_lngFindStorageCheckDetailByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ��õ�ǰ�����̵��¼����  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ��õ�ǰ�����̵��¼����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxCheckID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("T_OPR_STORAGECHECK", "STORAGECHECKID_CHR", 10);


                if (p_strResult == "")
                {
                    p_strResult = "0";
                }
                long lngID = long.Parse(p_strResult);
                if (lngID <= 0)
                {
                    lngID = 1;
                    p_strResult = lngID.ToString("0000000000");
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

        #region ��õ�ǰ�����̵���ϸ��  ŷ����ΰ  2004-06-11
        /// <summary>
        /// ��õ�ǰ�����̵���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strResult">���ֵ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxDetailID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("T_OPR_STORAGECHECKDETAIL", "STORAGECHECKDETAILID_CHR", 18);


                if (p_strResult == "")
                {
                    p_strResult = "0";
                }
                long lngID = long.Parse(p_strResult);
                if (lngID <= 0)
                {
                    lngID = 1;
                    p_strResult = lngID.ToString("000000000000000000");
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


        #region ���ҽ�������ѣ�ҩ�������������
        /// <summary>
        /// ���ҽ�������ѣ�ҩ�������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <param name="strMedId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataPianDianDeTail(out DataTable dtbResult, string strMedId, string flag)
        {
            long lngRes = 0;
            dtbResult = null;

            string strSQL = @"SELECT DISTINCT *
           FROM (SELECT tp.*, ts.pharmaname_vchr AS ����
                   FROM (SELECT tb.precent_dec, td.parentid_chr,
                                td.pharmaname_vchr AS ����
                           FROM t_bse_chargeitem ta,
                                t_aid_inschargeitem tb,
                                t_bse_medicine tc,
                                t_bse_pharmatype td,
                                t_bse_patientpaytype te
                          WHERE ta.itemsrctype_int = 1";
            strSQL += "     AND ta.itemsrcid_vchr = '" + strMedId + "'";
            strSQL += @"    AND ta.itemid_chr = tb.itemid_chr
                            AND tc.medicineid_chr = ta.itemsrcid_vchr
                            AND tc.pharmaid_chr = td.pharmaid_chr(+) ";
            strSQL += "    AND te.internalflag_int =1";
            strSQL += @"     AND tb.copayid_chr = te.paytypeid_chr) tp,
                        t_bse_pharmatype ts
                  WHERE tp.parentid_chr = ts.pharmaid_chr(+)),
                (SELECT tp.*, ts.pharmaname_vchr AS ����
                   FROM (SELECT tb.precent_dec AS precent_dec2,
                                td.parentid_chr, td.pharmaname_vchr AS ����
                           FROM t_bse_chargeitem ta,
                                t_aid_inschargeitem tb,
                                t_bse_medicine tc,
                                t_bse_pharmatype td,
                                t_bse_patientpaytype te
                          WHERE ta.itemsrctype_int = 1";
            strSQL += "     AND ta.itemsrcid_vchr = '" + strMedId + "'";
            strSQL += @"           AND ta.itemid_chr = tb.itemid_chr
                            AND tc.medicineid_chr = ta.itemsrcid_vchr
                            AND tc.pharmaid_chr = td.pharmaid_chr(+) ";
            strSQL += "    AND te.internalflag_int =2";
            strSQL += @"             AND tb.copayid_chr = te.paytypeid_chr) tp,
                        t_bse_pharmatype ts
                  WHERE tp.parentid_chr = ts.pharmaid_chr(+))
";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region �ϲ��̵㵥
        /// <summary>
        /// �ϲ��̵㵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objList"></param>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnionData(System.Collections.Generic.List<string> objList, clsStorageCheck_VO p_objItem, out DataTable dtCheckOut)
        {
            long lngRes = 0;
            dtCheckOut = new DataTable();
            dtCheckOut.Columns.Add("���ݺ�");
            dtCheckOut.Columns.Add("ҩƷ����");
            dtCheckOut.Columns.Add("ҩƷ����");
            dtCheckOut.Columns.Add("ϵͳ����");
            dtCheckOut.Columns.Add("ʵ����");
            dtCheckOut.Columns.Add("״̬");
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            m_lngDoAddNewStorageCheck(p_objItem);
            if (objList.Count > 0)
            {
                string strSQL = "";
                string strWhere = "";
                for (int f2 = 0; f2 < objList.Count; f2++)
                {
                    if (f2 == 0)
                    {
                        strWhere += " STORAGECHECKID_CHR ='" + objList[f2].ToString() + "' ";
                    }
                    else
                    {
                        strWhere += " or STORAGECHECKID_CHR ='" + objList[f2].ToString() + "' ";
                    }
                }
                strSQL = @"select count(*) as RowNO,MEDICINEID_CHR,LOTNO_VCHR from T_OPR_STORAGECHECKDETAIL where " + strWhere + " group by MEDICINEID_CHR,LOTNO_VCHR";
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
                DataTable dt1 = new DataTable();
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    if (int.Parse(dt.Rows[i1]["RowNO"].ToString()) > 1)
                    {
                        strSQL = @"select a.STORAGECHECKID_CHR,b.MEDICINEID_CHR,b.CALCQTY_DEC,b.REALQTY_DEC,b.LOTNO_VCHR,c.MEDICINENAME_VCHR  from t_opr_storagecheck a,t_opr_storagecheckdetail b,t_bse_medicine c where a.PERIODID_CHR='" + p_objItem.m_objPeriod.m_strPeriodID + "' and a.STORAGECHECKID_CHR=b.STORAGECHECKID_CHR and b.MEDICINEID_CHR='" + dt.Rows[i1]["MEDICINEID_CHR"].ToString() + "' and b.LOTNO_VCHR='" + dt.Rows[i1]["LOTNO_VCHR"].ToString() + "' and b.MEDICINEID_CHR=c.MEDICINEID_CHR";
                        try
                        {
                            lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                        for (int i3 = 0; i3 < dt1.Rows.Count; i3++)
                        {
                            DataRow newRow = dtCheckOut.NewRow();
                            newRow["ҩƷ����"] = dt1.Rows[i3]["MEDICINENAME_VCHR"].ToString();
                            newRow["���ݺ�"] = dt1.Rows[i3]["STORAGECHECKID_CHR"].ToString();
                            newRow["ҩƷ����"] = dt1.Rows[i3]["LOTNO_VCHR"].ToString();
                            newRow["ʵ����"] = dt1.Rows[i3]["REALQTY_DEC"].ToString();
                            if (i3 == 0)
                            {
                                newRow["״̬"] = "����";
                            }
                            else
                            {
                                strSQL = @"delete t_opr_storagecheckdetail where STORAGECHECKID_CHR='" + dt1.Rows[i3]["STORAGECHECKID_CHR"].ToString() + "' and MEDICINEID_CHR='" + dt1.Rows[i3]["MEDICINEID_CHR"].ToString() + "' and LOTNO_VCHR='" + dt1.Rows[i3]["LOTNO_VCHR"].ToString() + "'";
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
                                newRow["״̬"] = "ɾ��";
                            }
                            dtCheckOut.Rows.Add(newRow);
                        }

                    }
                }
                for (int i1 = 0; i1 < objList.Count; i1++)
                {
                    strSQL = @"update T_OPR_STORAGECHECKDETAIL set STORAGECHECKID_CHR='" + p_objItem.m_strStorageCheckID + "' where STORAGECHECKID_CHR='" + objList[i1].ToString() + "'";
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
                    strSQL = @"delete  T_OPR_STORAGECHECK  where STORAGECHECKID_CHR='" + objList[i1].ToString() + "' and STORAGEID_CHR='" + p_objItem.m_objStorage.m_strStroageID + "'";
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

        [AutoComplete]
        public DataTable m_mthGetDatap(DataTable p_dt)
        {
            DataTable dt = p_dt.Copy();
            DataTable dttemp = new DataTable();
            dt.Columns.Add("ҽ��");
            dt.Columns.Add("����");
            dt.Columns.Add("����");
            dt.Columns.Add("����");

            string id = "";
            int percent;
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                id = dt.Rows[i1]["medicineid_chr"].ToString().Trim();
                this.m_lngGetDataPianDianDeTail(out dttemp, id, "1");
                if (dttemp.Rows.Count > 0)
                {
                    percent = Convert.ToInt32(dttemp.Rows[0]["PRECENT_DEC"].ToString().Trim());
                    if (percent < 100)
                        dt.Rows[i1]["����"] = "#";
                    else
                        dt.Rows[i1]["����"] = "";
                    percent = Convert.ToInt32(dttemp.Rows[0]["PRECENT_DEC2"].ToString().Trim());
                    if (percent < 100)
                        dt.Rows[i1]["ҽ��"] = "#";
                    else
                        dt.Rows[i1]["ҽ��"] = "";

                    if (dttemp.Rows[0]["����"] != System.DBNull.Value)
                        dt.Rows[i1]["����"] = dttemp.Rows[0]["����"].ToString();
                    else
                        dt.Rows[i1]["����"] = "";
                    if (dttemp.Rows[0]["����"] != System.DBNull.Value)
                        dt.Rows[i1]["����"] = dttemp.Rows[0]["����"].ToString();
                    else
                        dt.Rows[i1]["����"] = "";
                }
            }
            return dt;
        }
        #region ��ϵͳ�ķ���

        #region ������е��̵�����
        /// <summary>
        /// ������е��̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageDeTail(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            string strSQL = @"SELECT a.STORAGEID_CHR,a.MEDICINEID_CHR, a.SYSLOTNO_CHR,a.LOTNO_VCHR,a.UNITID_CHR,a.USEFULLIFE_DAT,a.BUYUNITPRICE_MNY,a.SALEUNITPRICE_MNY,a.CURQTY_DEC, b.storagename_vchr, c.medicinename_vchr, c.medspec_vchr,c.medicinepreptype_chr, e.vendorname_vchr
                            FROM t_opr_storagemeddetail a,t_bse_storage b,t_bse_medicine c,t_bse_vendor e
                            WHERE a.storageid_chr = b.storageid_chr  AND a.medicineid_chr = c.medicineid_chr(+)  AND a.productorid_chr = e.vendorid_chr(+)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region �Զ����ɳ���ⵥ
        /// <summary>
        /// �Զ����ɳ���ⵥ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="dtStorCheckData">�Զ����ɵ��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAutoGreat(DataTable dtStorCheckData)
        {
            long lngRes = 0;
            string newid;
            string newDeid;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = new clsHisBase();
            DateTime nowData = HisBase.s_GetServerDate().Date;
            strSQL = @"select * from t_bse_period";
            DataTable periodTable = new DataTable();
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref periodTable);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes == 0 || periodTable.Rows.Count == 0)
                return -3;
            string nowperodID = "";
            for (int i1 = 0; i1 < periodTable.Rows.Count; i1++)
            {
                if (nowData >= Convert.ToDateTime(periodTable.Rows[i1]["STARTDATE_DAT"]) && nowData <= Convert.ToDateTime(periodTable.Rows[i1]["ENDDATE_DAT"]))
                    nowperodID = (string)periodTable.Rows[i1]["PERIODID_CHR"];
            }
            if (nowperodID == "")
                return -3;
            for (int i1 = 0; i1 < dtStorCheckData.Rows.Count; i1++)
            {
                string newID = "";
                lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageord", "STORAGEORDID_CHR", out newid);

                strSQL = @"insert into t_opr_storageord(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT," +
                    "CREATORID_CHR,MEMO_VCHR,PERIODID_CHR) values('" + newid + "','"
                    + dtStorCheckData.Rows[i1]["STORAGEORDTYPEID_CHR"].ToString() + "','" + dtStorCheckData.Rows[i1]["STORAGEID_CHR"].ToString() + "',To_Date('" + dtStorCheckData.Rows[i1]["INORD_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')," + dtStorCheckData.Rows[i1]["TOLMNY_MNY"].ToString() + ",1,'"
                    + dtStorCheckData.Rows[i1]["CREATORID_CHR"].ToString() + "','"
                    + dtStorCheckData.Rows[i1]["MEMO_VCHR"].ToString() + "','" + nowperodID + "')";
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
                if (lngRes > 0)
                {
                    newDeid = objHRPSvc.m_strGetNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", 20);
                    strSQL = @"insert into t_opr_storageordde(STORAGEORDDEID_CHR,STORAGEORDID_CHR,MEDICINEID_CHR,
                         SYSLOTNO_CHR,ORD_DAT,ROWNO_CHR,UNITID_CHR,QTY_DEC,BUYUNITPRICE_MNY,SALEUNITPRICE_MNY,BUYTOLPRICE_MNY)"
                        + " values('" + newDeid + "','" + newid + "','" + dtStorCheckData.Rows[i1]["MEDICINEID_CHR"].ToString() + "','" + dtStorCheckData.Rows[i1]["SYSLOTNO_CHR"].ToString() +
                        "',To_Date('" + dtStorCheckData.Rows[i1]["ORD_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')" + ",'0001','"
                        + dtStorCheckData.Rows[i1]["UNITID_CHR"].ToString() + "'," + dtStorCheckData.Rows[i1]["QTY_DEC"].ToString() + ","
                        + dtStorCheckData.Rows[i1]["BUYUNITPRICE_MNY"].ToString() + "," + dtStorCheckData.Rows[i1]["SALEUNITPRICE_MNY"].ToString() + "," + dtStorCheckData.Rows[i1]["BUYTOLPRICE_MNY"].ToString() + ")";
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

        #region �ж��Ƿ��������̵��������ĵ�������
        /// <summary>
        /// �ж��Ƿ��������̵��������ĵ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="typeName">�������������</param>
        /// <param name="typeID">���ص�������ID</param>
        /// <returns>2�У�3���Ǹ������ڸ��¿�����в�����</returns>
        [AutoComplete]
        public long m_lngisCheckType(string typeName, out string typeID)
        {
            long lngRes = 0;
            typeID = "";
            DataTable dtResult = new DataTable();
            string strSQL = @"select STORAGEORDTYPEID_CHR from t_aid_storageordtype where STORAGEORDTYPENAME_VCHR='" + typeName + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtResult.Rows.Count == 0)
                lngRes = 3;
            if (lngRes > 0 && dtResult.Rows.Count > 0)
            {
                typeID = dtResult.Rows[0]["STORAGEORDTYPEID_CHR"].ToString();
                lngRes = 2;
            }
            return lngRes;
        }
        #endregion


        #region �̿���ϸ��ͳ�Ʊ���
        /// <summary>
        /// �̿���ϸ��ͳ�Ʊ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckLoseDe(string StartDate, string EndDate, int status, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            string strSQL = @"select rownum  RowNuber,u.* from (SELECT  b.realqty_dec - b.calcqty_dec AS loseqty, b.lotno_vchr,
       c.medicinename_vchr, c.medspec_vchr, c.productorid_chr,
       d.medicinepreptypename_vchr, f.unitid_chr, f.usefullife_dat,
       a.STORAGECHECKID_CHR as aduitdate_dat,c.UNITPRICE_MNY,f.BUYUNITPRICE_MNY,f.SYSLOTNO_CHR
  FROM t_opr_storagecheck a,
       t_opr_storagecheckdetail b,
       t_bse_medicine c,
       t_aid_medicinepreptype d,
       t_opr_storagemeddetail f
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   AND c.medicinepreptype_chr = d.MEDICINEPREPTYPE_CHR(+)
   and a.STORAGEID_CHR=f.storageid_chr
   and b.MEDICINEID_CHR=f.medicineid_chr
   and b.lotno_vchr=f.syslotno_chr  and c.MEDICINETYPEID_CHR='" + status.ToString() + "' and a.ADUITDATE_DAT between to_date('" + StartDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss'))u where u.loseqty!=0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);

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


        #region ��òֿ���Ϣ
        /// <summary>
        /// ��òֿ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArr(out DataTable dtStorage)
        {
            long lngRes = 0;
            dtStorage = null;
            string strSQL = @"select STORAGEID_CHR,STORAGENAME_VCHR,STORAGEGROSSPROFIT_DEC from t_bse_storage  order by STORAGEID_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorage);

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

        #region ��òֿ���Ϣ
        /// <summary>
        /// ��òֿ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strStorageFlag"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageArr2(string strStorageFlag, out DataTable dtStorage)
        {
            dtStorage = null;
            if (strStorageFlag == "0")
            {
                return this.m_lngGetStorageArr(out dtStorage);
            }
            long lngRes = 0;
            dtStorage = null;
            string strSQL = @"select a.medstorename_vchr as storagename_vchr,a.medstoreid_chr as storageid_chr, 1 as STORAGEGROSSPROFIT_DEC from T_BSE_MEDSTORE a order by storageid_chr";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtStorage);

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
