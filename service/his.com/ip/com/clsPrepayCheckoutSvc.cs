using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �շ�ԱԤ�������
    /// ���ߣ�He Guiqiu
    /// ����ʱ��:2006-06-14
    /// </summary>

    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPrepayCheckoutSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsPrepayCheckoutSvc()
        {
            //
        }

        #region �����տ�ԱID��ѯδ����Ԥ�տ���Ϣ
        /// <summary>
        /// �����տ�ԱID��ѯδ����Ԥ�տ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCreatorId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetDisCheckoutPrepayInfoById(string p_strCreatorId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" SELECT  PREPAYID_CHR,   
                                     PATIENTID_CHR,   
                                     REGISTERID_CHR,   
                                     LINER_INT,   
                                     PAYTYPE_INT,   
                                     CUYCATE_INT,   
                                     MONEY_DEC,   
                                     PREPAYINV_VCHR,   
                                     AREAID_CHR,   
                                     DES_VCHR,   
                                     CREATORID_CHR,   
                                     CREATE_DAT,   
                                     DEACTID_CHR,   
                                     DEACTIVATE_DAT,   
                                     STATUS_INT,   
                                     ISCLEAR_INT,   
                                     PRESSNO_VCHR,   
                                     UPTYPE_INT,   
                                     BALANCEEMP_CHR,   
                                     BALANCE_DAT,   
                                     PATIENTNAME_CHR,   
                                     AREANAME_VCHR,   
                                     BALANCEFLAG_INT,   
                                     BALANCEID_VCHR,   
                                     CONFIRMEMP_CHR,   
                                     CHARGENO_CHR,
                                     originvono_vchr  
                               FROM  T_OPR_BIH_PREPAY
                              WHERE BALANCEFLAG_INT = 0 AND CREATORID_CHR = ? ORDER BY PREPAYINV_VCHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;

                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strCreatorId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
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

        #region �����տ�ԱID�ͽ������ڲ�ѯԤ�տ������Ϣ
        /// <summary>
        /// �����տ�ԱID�ͽ������ڲ�ѯԤ�տ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckoutPrepayInfoById(string p_strOperatorId, string p_strDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT a.PREPAYID_CHR,   
                                     a.PATIENTID_CHR,   
                                     a.REGISTERID_CHR,   
                                     a.LINER_INT,   
                                     a.PAYTYPE_INT,   
                                     a.CUYCATE_INT,   
                                     a.MONEY_DEC,   
                                     a.PREPAYINV_VCHR,   
                                     a.AREAID_CHR,   
                                     a.DES_VCHR,   
                                     a.CREATORID_CHR,   
                                     a.CREATE_DAT,   
                                     a.DEACTID_CHR,   
                                     a.DEACTIVATE_DAT,   
                                     a.STATUS_INT,   
                                     a.ISCLEAR_INT,   
                                     a.PRESSNO_VCHR,   
                                     a.UPTYPE_INT,   
                                     a.BALANCEEMP_CHR,   
                                     a.BALANCE_DAT,   
                                     a.PATIENTNAME_CHR,   
                                     a.AREANAME_VCHR,   
                                     a.BALANCEFLAG_INT,   
                                     a.BALANCEID_VCHR,   
                                     a.CONFIRMEMP_CHR,   
                                     a.CHARGENO_CHR,   
                                     b.BALANCEID_VCHR,   
                                     b.BALANCEEMP_CHR,   
                                     b.BALANCE_DAT,   
                                     b.REMARK_VCHR,
                                     a.originvono_vchr 
                              FROM T_OPR_BIH_PREPAY a,   
                                   T_OPR_BIH_PREPAYBALANCE b
                              WHERE a.BALANCEID_VCHR = b.BALANCEID_VCHR AND
                              a.BALANCEFLAG_INT = 1 AND 
                              a.BALANCEEMP_CHR = ? AND a.BALANCE_DAT = ?
                              ORDER BY a.PREPAYINV_VCHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = p_strOperatorId;
                arrParams[1].Value = System.DateTime.Parse(p_strDate);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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

        #region �����տ�ԱID�����ڲ�ѯ�ش�Ԥ�յ���Ϣ
        /// <summary>
        ///�����տ�ԱID�����ڲ�ѯ�ش�Ԥ�յ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetReprintByPrintEmp(string p_strEmpId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT b.BILLTYPE_CHR,   
                                     b.BILLID_CHR,   
                                     b.SOURCEBILLNO_VCHR,   
                                     b.REPPRNBILLNO_VCHR,   
                                     b.PRINTEMP_CHR,   
                                     b.PRINTDATE_DAT,   
                                     b.PRINTSTATUS_INT,
                                     a.MONEY_DEC      
                                FROM T_OPR_BIH_PREPAY A,
                                     T_OPR_BIH_BILLREPEATPRINT B 
                                WHERE A.PREPAYID_CHR = B.BILLID_CHR AND
                                      B.BILLTYPE_CHR = '1' AND
                                      B.PRINTEMP_CHR = ? AND 
                                      (B.PRINTDATE_DAT BETWEEN ? AND ? ) ORDER BY B.BILLID_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_strEmpId;
                arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                arrParams[2].Value = System.DateTime.Parse(p_strEndDate);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region ���ݽ�����ˮ�Ų�ѯԤ�տ������Ϣ
        /// <summary>
        /// ���ݽ�����ˮ�Ų�ѯԤ�տ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_balanceId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckoutPrepayInfoByBalanceId(string p_balanceId, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" SELECT a.PREPAYID_CHR,   
                                     a.PATIENTID_CHR,   
                                     a.REGISTERID_CHR,   
                                     a.LINER_INT,   
                                     a.PAYTYPE_INT,   
                                     a.CUYCATE_INT,   
                                     a.MONEY_DEC,   
                                     a.PREPAYINV_VCHR,   
                                     a.AREAID_CHR,   
                                     a.DES_VCHR,   
                                     a.CREATORID_CHR,   
                                     a.CREATE_DAT,   
                                     a.DEACTID_CHR,   
                                     a.DEACTIVATE_DAT,   
                                     a.STATUS_INT,   
                                     a.ISCLEAR_INT,   
                                     a.PRESSNO_VCHR,   
                                     a.UPTYPE_INT,   
                                     a.BALANCEEMP_CHR,   
                                     a.BALANCE_DAT,   
                                     a.PATIENTNAME_CHR,   
                                     a.AREANAME_VCHR,   
                                     a.BALANCEFLAG_INT,   
                                     a.BALANCEID_VCHR,   
                                     a.CONFIRMEMP_CHR,   
                                     a.CHARGENO_CHR,   
                                     b.BALANCEID_VCHR,   
                                     b.BALANCEEMP_CHR,   
                                     b.BALANCE_DAT,   
                                     b.REMARK_VCHR,
                                     a.originvono_vchr      
                               FROM  T_OPR_BIH_PREPAY a,T_OPR_BIH_PREPAYBALANCE b
                              WHERE a.BALANCEID_VCHR = b.BALANCEID_VCHR AND
                                    b.BALANCEID_VCHR = ? ORDER BY a.PREPAYINV_VCHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_balanceId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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

        #region Ԥ�տ������ʷ��Ϣ
        /// <summary>
        /// Ԥ�տ������ʷ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckoutPrepayHis(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select BALANCEID_VCHR,   
                                     BALANCEEMP_CHR,   
                                     BALANCE_DAT,   
                                     REMARK_VCHR  
                               from T_OPR_BIH_PREPAYBALANCE 
                               where BALANCEEMP_CHR = ?
                               and (BALANCE_DAT between ? and ? ) ORDER BY BALANCE_DAT";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_strOperatorId;
                arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                arrParams[2].Value = System.DateTime.Parse(p_strEndDate);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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
        [AutoComplete]
        public long CheckOutPrepayData(DataTable p_dtPrepayData, string p_strOperatorId, string p_strDate, string p_strRemark)
        {
            long lngRes = 0;
            string strSQL = "";
            if (p_dtPrepayData.Rows.Count > 0)
            {
                long lngRecEff = -1;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] parm = null;
                string date = string.Empty;
                List<string> lstDate = new List<string>();
                // ΢�����⴦��
                if (p_strOperatorId == "0000000")
                {
                    DataView dv = new DataView(p_dtPrepayData);
                    dv.Sort = "create_dat asc";
                    DataTable dt = dv.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        date = Convert.ToDateTime(dr["create_dat"].ToString()).ToString("yyyy-MM-dd");
                        if (lstDate.IndexOf(date) < 0)
                        {
                            lstDate.Add(date);
                        }
                    }
                }

                //������ˮ��
                string balanceId = string.Empty;
                try
                {
                    if (lstDate.Count > 1)
                    {
                        for (int i = 0; i < lstDate.Count; i++)
                        {
                            DateTime dtmBalance = (i == lstDate.Count - 1) ? System.DateTime.Parse(p_strDate) : Convert.ToDateTime(lstDate[i] + " 23:59:59");
                            balanceId = GetNextNewId();
                            //����ʱ�ע��
                            strSQL = @"INSERT INTO T_OPR_BIH_PREPAYBALANCE (BALANCEID_VCHR, BALANCEEMP_CHR, BALANCE_DAT, REMARK_VCHR) VALUES (?, ?, ?, ?)";
                            objHRPSvc.CreateDatabaseParameter(4, out parm);
                            parm[0].Value = balanceId;
                            parm[1].Value = p_strOperatorId;
                            parm[2].Value = (i == lstDate.Count - 1) ? System.DateTime.Parse(p_strDate) : Convert.ToDateTime(lstDate[i] + " 23:59:59");
                            parm[3].Value = p_strRemark;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parm);

                            if (i == lstDate.Count - 1)
                            {
                                strSQL = @"update T_OPR_BIH_PREPAY 
                                              set BALANCEEMP_CHR = ?, BALANCE_DAT = ?, BALANCEID_VCHR =?, BALANCEFLAG_INT = 1
                                            where BALANCEFLAG_INT = 0 AND CREATORID_CHR = ? and create_dat > to_date(?, 'yyyy-mm-dd hh24:mi:ss') ";
                                objHRPSvc.CreateDatabaseParameter(5, out parm);
                                parm[0].Value = p_strOperatorId;
                                parm[1].Value = dtmBalance;
                                parm[2].Value = balanceId;
                                parm[3].Value = p_strOperatorId;
                                parm[4].Value = lstDate[i - 1] + " 23:59:59";
                            }
                            else
                            {
                                strSQL = @"update T_OPR_BIH_PREPAY 
                                              set BALANCEEMP_CHR = ?, BALANCE_DAT = ?, BALANCEID_VCHR =?, BALANCEFLAG_INT = 1
                                            where BALANCEFLAG_INT = 0 AND CREATORID_CHR = ? and create_dat <= to_date(?, 'yyyy-mm-dd hh24:mi:ss') ";
                                objHRPSvc.CreateDatabaseParameter(5, out parm);
                                parm[0].Value = p_strOperatorId;
                                parm[1].Value = dtmBalance;
                                parm[2].Value = balanceId;
                                parm[3].Value = p_strOperatorId;
                                parm[4].Value = lstDate[i] + " 23:59:59";
                            }
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parm);
                        }
                    }
                    else
                    {
                        balanceId = GetNextNewId();
                        //����ʱ�ע��
                        strSQL = @"INSERT INTO T_OPR_BIH_PREPAYBALANCE (BALANCEID_VCHR, BALANCEEMP_CHR, BALANCE_DAT, REMARK_VCHR) VALUES (?, ?, ?, ?)";
                        objHRPSvc.CreateDatabaseParameter(4, out parm);
                        parm[0].Value = balanceId;
                        parm[1].Value = p_strOperatorId;
                        parm[2].Value = System.DateTime.Parse(p_strDate);
                        parm[3].Value = p_strRemark;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parm);

                        strSQL = @"update T_OPR_BIH_PREPAY 
                                      set BALANCEEMP_CHR = ?, BALANCE_DAT = ?, BALANCEID_VCHR =?, BALANCEFLAG_INT = 1
                                    where BALANCEFLAG_INT = 0 AND CREATORID_CHR = ? ";
                        objHRPSvc.CreateDatabaseParameter(4, out parm);
                        parm[0].Value = p_strOperatorId;
                        parm[1].Value = System.DateTime.Parse(p_strDate);
                        parm[2].Value = balanceId;
                        parm[3].Value = p_strOperatorId;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, parm);
                    }
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region �������ڲ���Ԥ�տ���㱸ע��Ϣ
        /// <summary>
        /// �������ڲ���Ԥ�տ���㱸ע��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPrepayBalanceRemarkByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT BALANCEID_VCHR,   
                                     BALANCEEMP_CHR,   
                                     BALANCE_DAT,   
                                     REMARK_VCHR  
                                FROM T_OPR_BIH_PREPAYBALANCE 
                                WHERE T_OPR_BIH_PREPAYBALANCE.BALANCE_DAT BETWEEN ? AND ?
                                 ORDER BY T_OPR_BIH_PREPAYBALANCE.BALANCEEMP_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = System.DateTime.Parse(p_strStartDate);
                arrParams[1].Value = System.DateTime.Parse(p_strEndDate);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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

        #region �޸Ľ��㱸ע��Ϣ
        /// <summary>
        ///�޸Ľ��㱸ע��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBalanceId">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long ModifyBalanceRemark(string p_strBalanceId, string p_strRemark)
        {
            long lngRes = 0;
            string strSQL = @" UPDATE T_OPR_BIH_PREPAYBALANCE 
                              SET REMARK_VCHR = ? WHERE BALANCEID_VCHR = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] parameterArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out parameterArr);
                parameterArr[0].Value = p_strRemark;
                parameterArr[1].Value = p_strBalanceId;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, parameterArr);
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

        #region �������ڲ���Ԥ�տ������Ϣ
        /// <summary>
        /// �������ڲ���Ԥ�տ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPrepayBalanceInfoByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT T_BSE_EMPLOYEE.EMPNO_CHR,   
                                     T_BSE_EMPLOYEE.FIRSTNAME_VCHR,
                                     T_BSE_EMPLOYEE.LASTNAME_VCHR,   
                                     T_OPR_BIH_PREPAY.PREPAYID_CHR,   
                                     T_OPR_BIH_PREPAY.PATIENTID_CHR,   
                                     T_OPR_BIH_PREPAY.REGISTERID_CHR,   
                                     T_OPR_BIH_PREPAY.LINER_INT,   
                                     T_OPR_BIH_PREPAY.PAYTYPE_INT,   
                                     T_OPR_BIH_PREPAY.CUYCATE_INT,   
                                     T_OPR_BIH_PREPAY.MONEY_DEC,   
                                     T_OPR_BIH_PREPAY.PREPAYINV_VCHR,   
                                     T_OPR_BIH_PREPAY.AREAID_CHR,   
                                     T_OPR_BIH_PREPAY.DES_VCHR,   
                                     T_OPR_BIH_PREPAY.CREATORID_CHR,   
                                     T_OPR_BIH_PREPAY.CREATE_DAT,   
                                     T_OPR_BIH_PREPAY.DEACTID_CHR,   
                                     T_OPR_BIH_PREPAY.DEACTIVATE_DAT,   
                                     T_OPR_BIH_PREPAY.STATUS_INT,   
                                     T_OPR_BIH_PREPAY.ISCLEAR_INT,   
                                     T_OPR_BIH_PREPAY.PRESSNO_VCHR,   
                                     T_OPR_BIH_PREPAY.UPTYPE_INT,   
                                     T_OPR_BIH_PREPAY.PATIENTNAME_CHR,   
                                     T_OPR_BIH_PREPAY.AREANAME_VCHR,   
                                     T_OPR_BIH_PREPAY.BALANCEFLAG_INT,   
                                     T_OPR_BIH_PREPAY.BALANCEEMP_CHR,   
                                     T_OPR_BIH_PREPAY.BALANCE_DAT,
                                     T_OPR_BIH_PREPAY.BALANCEID_VCHR,
                                     t_opr_bih_prepay.originvono_vchr     
                                FROM T_OPR_BIH_PREPAY,   
                                     T_BSE_EMPLOYEE  
                                WHERE ( T_OPR_BIH_PREPAY.BALANCEEMP_CHR = T_BSE_EMPLOYEE.EMPID_CHR ) AND 
                                      (T_OPR_BIH_PREPAY.BALANCE_DAT BETWEEN ? AND ? ) ORDER BY T_BSE_EMPLOYEE.EMPNO_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = System.DateTime.Parse(p_strStartDate);
                arrParams[1].Value = System.DateTime.Parse(p_strEndDate);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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

        #region ȡ��һ���ս�ʱ��
        /// <summary>
        ///ȡ��һ���ս�ʱ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBalanceEemId"></param>
        /// <param name="p_strBalanceDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetFrontBalanceDate(string p_strBalanceEemId, string p_strBalanceDate, out string p_strFrontBalanceDate)
        {
            p_strFrontBalanceDate = "";

            long lngRes = 0;
            string strSQL = @" SELECT MAX(BALANCE_DAT) BALANCE_DAT FROM  T_OPR_BIH_PREPAYBALANCE 
                               WHERE balanceemp_chr = ? and BALANCE_DAT < ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                DataTable dt = new DataTable();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = p_strBalanceEemId;
                arrParams[1].Value = System.DateTime.Parse(p_strBalanceDate);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, arrParams);

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strFrontBalanceDate = dt.Rows[0]["BALANCE_DAT"].ToString();
                }
                else
                {
                    p_strFrontBalanceDate = "";
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

        #region ȡ��һ���ս�ʱ��
        /// <summary>
        ///ȡ���һ���ս�ʱ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBalanceEemId"></param>
        /// <param name="p_strBalanceDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetLastBalanceDate(string p_strBalanceEemId, out string p_strBalanceDate)
        {
            p_strBalanceDate = "";

            long lngRes = 0;
            string strSQL = @" SELECT MAX(BALANCE_DAT) BALANCE_DAT FROM  T_OPR_BIH_PREPAYBALANCE 
                               WHERE balanceemp_chr = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                DataTable dt = new DataTable();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strBalanceEemId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, arrParams);

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strBalanceDate = dt.Rows[0]["BALANCE_DAT"].ToString();
                }
                else
                {
                    p_strBalanceDate = "";
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

        #region ��ȡ����SEQ_PREPAYBALANCE����һ��ֵ
        /// <summary>
        /// ��ȡ����SEQ_PREPAYBALANCE����һ��ֵ
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        private string GetNextNewId()
        {
            long lngRes = 0;

            string strSQL;
            strSQL = @"SELECT SEQ_PREPAYBALANCE.NEXTVAL FROM dual";

            System.Data.DataTable dtbResult = new System.Data.DataTable();

            string newId = "0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    newId = dtbResult.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return newId;
        }
        #endregion

        #region ���ݲ���Ա����ȡ��ID
        /// <summary>
        /// ���ݲ���Ա����ȡ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpCode"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetEmpIdByCode(string p_strEmpCode, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select EMPID_CHR,   
                                     LASTNAME_VCHR  
                                     
                               from T_BSE_EMPLOYEE 
                               where EMPNO_CHR = ? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strEmpCode;


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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
        /// ���ݷ�Ʊ��ȡ��Ԥ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpCode"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetEmpIdByPreInvs(string p_strEmpCode, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"select t.CREATORID_CHR,
                                     t.DEACTID_CHR,
                                     t.PAYTYPE_INT,
                                     t.prepayinv_vchr,
                                     t.areaname_vchr,
                                     t.money_dec,t.BAlaNCEfLAG_INT,t.CREATE_DAT,t.BALANCE_DAT
                                from T_OPR_BIH_PREPAY t
                               where t.prepayinv_vchr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                new clsHRPTableService().CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strEmpCode;


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);

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
    }
}
