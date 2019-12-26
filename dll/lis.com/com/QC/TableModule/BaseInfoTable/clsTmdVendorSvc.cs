using System;
using System.Data;
using System.EnterpriseServices;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdVendorSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region INSERT
        /// <summary>
        /// ����һ����ⷽ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">��ⷽ��ʵ��</param>
        /// <param name="p_intSeq">��ⷽ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsert(clsLisVendorVO p_objVendor, out int p_intSeq)
        {
            long lngRes = 0;
            p_intSeq = -1; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"
                            INSERT INTO T_BSE_LIS_VENDOR      (
        										                    VENDOR_SEQ_INT,
                                                                    VENDOR_VCHR,
                                                                    VENDOR_ID_VCHR,
                                                                    PYCODE_VCHR,
                                                                    WBCODE_VCHR
                                                                )
        							                            VALUES
                                                                ( ?, ? ,? , ?, ?)";
            

            try
            {
                lngRes = 0;
                lngRes = objHRPSvc.m_lngGenerateNewID("T_BSE_LIS_VENDOR", "VENDOR_SEQ_INT", out p_intSeq);
                if (lngRes <= 0)
                    return -1;
                lngRes = 0;


                System.Data.IDataParameter[] objODPArr = clsPublicSvc.m_objConstructIDataParameterArr(
                    p_intSeq,
                    p_objVendor.m_strVendor,
                    p_objVendor.m_strId,
                    p_objVendor.m_strPycode,
                    p_objVendor.m_strWbcode
                    );

                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objODPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0)
                {
                    p_objVendor.m_intSeq = p_intSeq;//��VO��ֵID
                }
                else
                {
                    p_intSeq = -1;
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// ����һ����ⷽ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">��ⷽ��ʵ��</param>
        /// <param name="p_intSeq">��ⷽ�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdate( clsLisVendorVO p_objVendor)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                                                                   
            string strSQL = @"UPDATE T_BSE_LIS_VENDOR SET
                                                                VENDOR_VCHR=?,
                                                                VENDOR_ID_VCHR=?,
                                                                PYCODE_VCHR=?,
                                                                WBCODE_VCHR=?
                                                        WHERE  VENDOR_SEQ_INT=?
        							                            ";
            try
            {
                System.Data.IDataParameter[] objODPArr = clsPublicSvc.m_objConstructIDataParameterArr(
                            p_objVendor.m_strVendor,
                            p_objVendor.m_strId,
                            p_objVendor.m_strPycode,
                            p_objVendor.m_strWbcode,
                             p_objVendor.m_intSeq
                            );
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objODPArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region DELETE
        [AutoComplete]
        public long m_lngDelete( int p_intSeq)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"DELETE T_BSE_LIS_VENDOR WHERE VENDOR_SEQ_INT = ?";
            try
            {
                System.Data.IDataParameter[] objODPArr = clsPublicSvc.m_objConstructIDataParameterArr(
                    p_intSeq);

                long lngRecEff = -1;
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objODPArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region FIND
        /// <summary>
        /// ���ݼ��鷽����Ų���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind( int p_intSeq, out clsLisVendorVO p_objVendor)
        {
            long lngRes = 0;
            p_objVendor = null; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT * FROM T_BSE_LIS_VENDOR WHERE VENDOR_SEQ_INT = ?";
            try
            {
                System.Data.IDataParameter[] objODPArr = clsPublicSvc.m_objConstructIDataParameterArr(
                    p_intSeq);

                DataTable dtbResult = null;
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objODPArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objVendor = new clsLisVendorVO();
                    this.ConstructVO(dtbResult.Rows[0], ref p_objVendor);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_lngFind( out clsLisVendorVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 
            string strSQL = @"SELECT * FROM T_BSE_LIS_VENDOR ";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                DataTable dtbResult = null;
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisVendorVO[dtbResult.Rows.Count];
                    for (int i = 0; i < p_objResultArr.Length; i++)
                    {
                        p_objResultArr[i] = new clsLisVendorVO();
                        this.ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
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

        #region ConstructVO
        /// <summary>
        /// �����ݿ��й���ʵ��
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="p_objVendor"></param>
        [AutoComplete]
        public void ConstructVO(DataRow p_dtrSource, ref clsLisVendorVO p_objVendor)
        {
            p_objVendor.m_intSeq = p_dtrSource["VENDOR_SEQ_INT"] == System.DBNull.Value ? 0 : int.Parse(p_dtrSource["VENDOR_SEQ_INT"].ToString().Trim());
            p_objVendor.m_strVendor = p_dtrSource["VENDOR_VCHR"].ToString().Trim();
            p_objVendor.m_strId = p_dtrSource["VENDOR_ID_VCHR"].ToString().Trim();
            p_objVendor.m_strPycode = p_dtrSource["PYCODE_VCHR"].ToString().Trim();
            p_objVendor.m_strWbcode = p_dtrSource["WBCODE_VCHR"].ToString().Trim();

        }
        #endregion
    }
}