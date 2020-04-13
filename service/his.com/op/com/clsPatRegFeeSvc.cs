using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �Һŷ�������ά��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPatRegFeeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsPatRegFeeSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �����Һŷ������
        /// <summary>
        /// �����Һŷ������
        /// </summary>
        [AutoComplete]
        public long m_lngNewFeeList(clsPatRegFee_VO clsVO)
        {
            long lngRes = 0;

            string strSQL = @"Insert Into t_opPatRegAmount(registertypeid_chr,paytypeid_chr,regfee,diagfee)
                             Values (?,?,?,?)";

            try
            {
                long lngRec = 0;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { clsVO.m_strRegisterTypeID, clsVO.m_strPayTypeID, clsVO.m_decRegFee, clsVO.m_decDiagFee });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region �޸ĹҺŷ������
        /// <summary>
        /// �޸ĹҺŷ������
        /// </summary>
        [AutoComplete]
        public long m_lngUPDateFeeList(clsPatRegFee_VO clsVO)
        {
            long lngRes = 0;

            string strSQL = @"UPDate t_opPatRegAmount Set regfee=?,diagfee=?
                             Where registertypeid_chr=? And paytypeid_chr=?";

            try
            {
                long lngRec = 0;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { clsVO.m_decRegFee, clsVO.m_decDiagFee, clsVO.m_strRegisterTypeID, clsVO.m_strPayTypeID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ɾ���Һŷ������
        /// <summary>
        /// ɾ���Һŷ������
        /// </summary>
        [AutoComplete]
        public long m_lngDelFeeList(clsPatRegFee_VO clsVO)
        {
            long lngRes = 0;

            string strSQL = @"Delete t_opPatRegAmount 
                             Where registertypeid_chr=? And paytypeid_chr=?";

            try
            {
                long lngRec = 0;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { clsVO.m_strRegisterTypeID, clsVO.m_strPayTypeID });
                lngRes = HRPSvc.lngExecuteParameterSQL(strSQL, ref lngRec, objPara);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���ҹҺŷ������
        /// <summary>
        /// ���ҹҺŷ������
        /// </summary>
        [AutoComplete]
        public long m_lngFindFeeList(ref DataTable dtResult)
        {
            long lngRes = 0;

            string strSQL = @"Select a.registertypeid_chr,a.paytypeid_chr,a.regfee,a.diagfee,
                            b.registertypename_vchr,c.paytypename_vchr from t_opPatRegAmount a,
                            t_bse_registertype b,t_bse_patientpaytype c  
                            Where a.registertypeid_chr=b.registertypeid_chr 
                            And a.paytypeid_chr=c.paytypeid_chr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                lngRes = HRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        #region ���ݲ�������ID�͹Һ�����ID���Ҽ�¼
        /// <summary>
        /// ���ݲ�������ID�͹Һ�����ID���Ҽ�¼
        /// </summary>
        [AutoComplete]
        public long m_lngFindFeeListByID(string RegTypeID, string PatTypeID, out clsPatRegFee_VO clsVO)
        {
            clsVO = new clsPatRegFee_VO();
            long lngRes = 0;
            string strSQL = @"Select a.registertypeid_chr,a.paytypeid_chr,a.regfee,a.diagfee,
                            b.registertypename_vchr,c.paytypename_vchr from t_opPatRegAmount a,
                            t_bse_registertype b,t_bse_patientpaytype c  
                            Where a.registertypeid_chr=? And a.paytypeid_chr=? 
                            And a.registertypeid_chr=b.registertypeid_chr 
                            And a.paytypeid_chr=c.paytypeid_chr";

            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService HRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objPara = HRPSvc.CreateDatabaseParameter(new object[] { RegTypeID, PatTypeID });
                lngRes = HRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objPara);
                if (lngRes < 0)
                    return lngRes;
                if (dtResult.Rows.Count == 0)
                    return 0;
                clsVO.m_strPayTypeID = PatTypeID;
                clsVO.m_strRegisterTypeID = RegTypeID;
                clsVO.m_decRegFee = decimal.Parse(dtResult.Rows[0]["regfee"].ToString().Trim());
                clsVO.m_decDiagFee = decimal.Parse(dtResult.Rows[0]["diagfee"].ToString().Trim());
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = -2;
            }
            return lngRes;
        }
        #endregion

        //���عҺ��շ�
        #region  ���عҺ��շѱ�׼ zlc 2004-8-3
        /// <summary>
        ///  ���عҺ��շ�
        /// </summary>
        [AutoComplete]
        public long m_lngGetRegCharge(out clsRegisterPay[] objResult)
        {
            objResult = new clsRegisterPay[0];
            long lngRes = 0;
            string strSQL = "Select * From V_BSE_REGISTERCHARGE ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objResult = new clsRegisterPay[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < objResult.Length; i1++)
                    {
                        objResult[i1] = new clsRegisterPay();
                        objResult[i1].m_strREGISTERTYPEID_CHR = dtbResult.Rows[i1]["REGISTERTYPEID_CHR"].ToString().Trim();
                        objResult[i1].m_strREGISTERTYPENAME_VCHR = dtbResult.Rows[i1]["REGISTERTYPENAME_VCHR"].ToString().Trim();
                        objResult[i1].m_strCHARGEID_CHR = dtbResult.Rows[i1]["CHARGEID_CHR"].ToString().Trim();
                        objResult[i1].m_strCHARGENAME_CHR = dtbResult.Rows[i1]["CHARGENAME_CHR"].ToString().Trim();
                        objResult[i1].m_strPAYTYPEID_CHR = dtbResult.Rows[i1]["PAYTYPEID_CHR"].ToString().Trim();
                        objResult[i1].m_strPAYTYPENAME_VCHR = dtbResult.Rows[i1]["PAYTYPENAME_VCHR"].ToString().Trim();
                        objResult[i1].m_strMEMO_VCHR = dtbResult.Rows[i1]["MEMO_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["PAYMENT_MNY"] != Convert.DBNull)
                            objResult[i1].m_dblPAYMENT_MNY = double.Parse(dtbResult.Rows[i1]["PAYMENT_MNY"].ToString());
                        if (dtbResult.Rows[i1]["DISCOUNT_DEC"] != Convert.DBNull)
                            objResult[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString());
                        //						if(dtbResult.Rows[i1]["DIAGPAY_MNY"].ToString().Trim()!="")
                        //						   objResult[i1].m_decDiagPay=decimal.Parse(dtbResult.Rows[i1]["DIAGPAY_MNY"].ToString().Trim());
                        //						if(dtbResult.Rows[i1]["REGPAY_MNY"].ToString().Trim()!="")
                        //						objResult[i1].m_decRegPay=decimal.Parse(dtbResult.Rows[i1]["REGPAY_MNY"].ToString().Trim());
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
