using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    #region ҩ�����ά��ҵ�����
    /// <summary>	
    /// ҩ�����ά��ҵ�����
    /// Create ��ΰ�� by 2005-09-8
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsHISMedTypeManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsHISMedTypeManageSvc()
        {

        }
        #endregion

        #region �м����������ҩ�����ά����ҵ�����

        #region ������ȡ�����н��
        /// <summary>
        /// ������ȡ�����н��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="strMainID">ȡ�����н��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        [AutoComplete]
        public long m_lngGetMedTypeInfo(out clsHISMedType_VO[] p_objResultArr, string strMainID)
        {
            long lngRes = 0;
            p_objResultArr = new clsHISMedType_VO[0];

            string strSQL = @"Select * From T_BSE_PHARMATYPE order by PHARMAID_CHR";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsHISMedType_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsHISMedType_VO();
                        p_objResultArr[i1].m_strPHARMAID_CHR = dtbResult.Rows[i1]["PHARMAID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPHARMANAME_VCHR = dtbResult.Rows[i1]["PHARMANAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_VCHR = dtbResult.Rows[i1]["ASSISTCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCODE_VCHR = dtbResult.Rows[i1]["PYCODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCODE_VCHR = dtbResult.Rows[i1]["WBCODE_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["PARENTID_CHR"] != System.DBNull.Value)
                            p_objResultArr[i1].m_strPARENTID_CHR = dtbResult.Rows[i1]["PARENTID_CHR"].ToString().Trim();
                        else
                            p_objResultArr[i1].m_strPARENTID_CHR = null;

                    }
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

        #region ҩ�����ά��ҵ�����:�޸ķ�����Ϣ���
        /// <summary>
        /// ҩ�����ά��ҵ�����:�޸ķ�����Ϣ���
        /// Create ��ΰ�� by 2005-09-8
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objRecord">VO����</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        /// </summary>
        [AutoComplete]
        public long m_lngModify(clsHISMedType_VO p_objRecord)
        {
            long lngRes = 0;
            string strSQL = @" UPDATE T_BSE_PHARMATYPE SET PHARMANAME_VCHR =?,ASSISTCODE_VCHR =?,PYCODE_VCHR =?,WBCODE_VCHR =? ,PARENTID_CHR = ? WHERE 
 PHARMAID_CHR =?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_objRecord.m_strPHARMANAME_VCHR;
                objDPArr[1].Value = p_objRecord.m_strASSISTCODE_VCHR;
                objDPArr[2].Value = p_objRecord.m_strPYCODE_VCHR;
                objDPArr[3].Value = p_objRecord.m_strWBCODE_VCHR;
                objDPArr[4].Value = p_objRecord.m_strPARENTID_CHR;
                objDPArr[5].Value = p_objRecord.m_strPHARMAID_CHR;
                long l = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref l, objDPArr);
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

        #region ҩ�����ά��ҵ�����:���ӷ�����Ϣ���
        /// <summary>
        /// ҩ�����ά��ҵ�����:���ӷ�����Ϣ���
        /// Create ��ΰ�� by 2005-09-9
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objRecord">VO����</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        /// </summary>
        [AutoComplete]
        public long m_lngAddNew(clsHISMedType_VO objTD_VO, out clsHISMedType_VO objTD_VOReturn)
        {
            long lngRes = 0;
            string strRecordID = "";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(5, "PHARMAID_CHR", "T_BSE_PHARMATYPE", out strRecordID);
            objTD_VO.m_strPHARMAID_CHR = strRecordID;

            //�ѽ�������
            objTD_VOReturn = objTD_VO;


            if (lngRes < 0)
            {
                return -1;
            }

            if (lngRes < 0)
                return lngRes;

            string strSQL = "INSERT INTO T_BSE_PHARMATYPE (PHARMAID_CHR,PHARMANAME_VCHR,ASSISTCODE_VCHR,PYCODE_VCHR,WBCODE_VCHR,PARENTID_CHR) VALUES (?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = objTD_VO.m_strPHARMAID_CHR;
                objLisAddItemRefArr[1].Value = objTD_VO.m_strPHARMANAME_VCHR;
                objLisAddItemRefArr[2].Value = objTD_VO.m_strASSISTCODE_VCHR;
                objLisAddItemRefArr[3].Value = objTD_VO.m_strPYCODE_VCHR;
                objLisAddItemRefArr[4].Value = objTD_VO.m_strWBCODE_VCHR;
                objLisAddItemRefArr[5].Value = objTD_VO.m_strPARENTID_CHR;
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

        #region �м��������ɾ��������Ϣ���
        /// <summary>
        /// ҩ�����ά��ҵ�����:ɾ��������Ϣ���
        /// Create ��ΰ�� by 2005-09-9
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objRecord">VO����</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        /// </summary>
        [AutoComplete]
        public long m_lngDelete(clsHISMedType_VO p_objRecord)
        {
            long lngRes = 0;

            string strSQL = "DELETE FROM T_BSE_PHARMATYPE WHERE PHARMAID_CHR ='" + p_objRecord.m_strPHARMAID_CHR.Trim() + "'";

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

        #region �������ж��������Ƿ�Ψһ��
        /// <summary>
        /// �������ж��������Ƿ�Ψһ
        /// </summary>
        /// <param name="blnHasThisZhujima">���ؽ�����Ѵ��ڴ��������򷵻�true</param>
        /// <param name="p_strZhuJiMa">����������</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        [AutoComplete]
        public long m_lngGetMedTypeZhuJiMaById(out bool blnHasThisZhujima, string p_strZhuJiMa)
        {

            long lngRes = 0;
            blnHasThisZhujima = false;

            string strSQL = @"Select count(*) From T_BSE_PHARMATYPE where ASSISTCODE_VCHR='" + p_strZhuJiMa + "'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtbResult.Rows[0][0].ToString()) != 0)
                    {
                        blnHasThisZhujima = true;//������ͬ
                    }
                    else
                    {
                        blnHasThisZhujima = false;
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

        #region �������ж�ĳ����Ƿ�ӵ���ӽ�㣺��ҩ��������Ƿ����ӷ��ࣩ
        /// <summary>
        /// �������ж�ĳ����Ƿ�ӵ���ӽ�㣺��ҩ��������Ƿ����ӷ��ࣩ
        /// </summary>
        /// <param name="blnHasSubNode">���ؽ���������ӽ���򷵻�true</param>
        /// <param name="m_strPHARMAID_CHR">���ݿ����Զ�������ID��</param>
        /// <returns>ʧ�ܣ�-1 ���ɹ�����Ӱ��Ľ����</returns>
        [AutoComplete]
        public long m_lngCheckMedTypeIsHasSubById(out bool blnHasSubNode, string m_strPHARMAID_CHR)
        {

            long lngRes = 0;
            blnHasSubNode = false;

            string strSQL = @"Select count(*) From T_BSE_PHARMATYPE where PARENTID_CHR='" + m_strPHARMAID_CHR + "'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtbResult.Rows[0][0].ToString()) != 0)
                    {
                        blnHasSubNode = true;//�����ӽ��
                    }
                    else
                    {
                        blnHasSubNode = false;
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

        #endregion
    }
    #endregion
}
