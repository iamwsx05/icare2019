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
    /// ҽ�����ֲ�
    /// �����ˣ���Ӣ��	2006-6-22
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsYBSpecialTypeDisease_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsYBSpecialTypeDisease_SVC()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion
        #region �������ֲ�ID��ȡ���ֲ���Ϣ
        /// <summary>
        /// �������ֲ�ID��ȡ���ֲ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDieaseCode">��������</param>
        /// <param name="m_objYBSpeTypeDiease_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetYBSpecialTypeDiseaseByID(string m_strDieaseCode, out clsYBSpecialTypeDisease_VO m_objYBSpeTypeDiease_VO)
        {
            m_objYBSpeTypeDiease_VO = null;
            DataTable p_dtbRecord = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.deacode_chr,
       a.deadesc_vchr,
       a.yearmoney_int,
       a.sort_int,
       a.status_int,
       a.note_vchr from t_opr_ybspecialtypedisease a where a.deacode_chr='" + m_strDieaseCode + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbRecord);
                if (p_dtbRecord.Rows.Count > 0)
                {
                    m_objYBSpeTypeDiease_VO = new clsYBSpecialTypeDisease_VO();
                    m_objYBSpeTypeDiease_VO.m_strDieaseCode = p_dtbRecord.Rows[0]["DEACODE_CHR"].ToString().Trim();
                    m_objYBSpeTypeDiease_VO.m_strDieaseNamae = p_dtbRecord.Rows[0]["DEADESC_VCHR"].ToString().Trim();
                    m_objYBSpeTypeDiease_VO.m_strComment = p_dtbRecord.Rows[0]["NOTE_VCHR"].ToString().Trim();
                    if (p_dtbRecord.Rows[0]["SORT_INT"].ToString().Trim() != "")
                    {
                        m_objYBSpeTypeDiease_VO.m_intSortNO = int.Parse(p_dtbRecord.Rows[0]["SORT_INT"].ToString().Trim());
                    }
                    else
                    {
                        m_objYBSpeTypeDiease_VO.m_intSortNO = 0;
                    }
                    if (p_dtbRecord.Rows[0]["STATUS_INT"].ToString().Trim() != "")
                    {
                        m_objYBSpeTypeDiease_VO.m_intStatus = int.Parse(p_dtbRecord.Rows[0]["STATUS_INT"].ToString().Trim());
                    }
                    else
                    {
                        m_objYBSpeTypeDiease_VO.m_intStatus = 0;
                    }
                    if (p_dtbRecord.Rows[0]["YEARMONEY_INT"].ToString().Trim() != "")
                    {
                        m_objYBSpeTypeDiease_VO.m_floatYearMoney = float.Parse(p_dtbRecord.Rows[0]["YEARMONEY_INT"].ToString().Trim());
                    }
                    else
                    {
                        m_objYBSpeTypeDiease_VO.m_floatYearMoney = 0;
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

        #region ��ȡ���ֲ���Ϣ
        /// <summary>
        /// ��ȡ���ֲ���Ϣ,����ҽ�����ֱ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetTableForYBSpecialTypeDisease(out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;
            string strSQL = @"select deacode_chr,
       deadesc_vchr,
       yearmoney_int,
       sort_int,
       status_int,
       note_vchr from T_OPR_YBSPECIALTYPEDISEASE ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
        #region ɾ�����ֲ���Ϣ
        /// <summary>
        /// ͨ����������ɾ�����ֲ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDiseCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthDelectYBSpecialTypeDiseaseByDiseaseCode(string m_strDiseCode)
        {
            long lngRes = 0;
            string strSQL = @"delete from T_OPR_YBSPECIALTYPEDISEASE A where A.deacode_chr='" + m_strDiseCode + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    //ɾ���ü��������Ӧ��icd���¼
                    strSQL = @"delete from t_opr_ybdeadeficd10 A where A.deacode_chr='" + m_strDiseCode + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
                    //ɾ���ü��������Ӧ���շ���Ŀ��¼
                    strSQL = @"delete from t_opr_ybdeadefchargeitem A where A.deacode_chr='" + m_strDiseCode + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region ɾ���������ֲ���Ϣ
        /// <summary>
        /// ͨ����������ɾ�����ֲ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objSpeTypeDise_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthDelectYBSpecialTypeDiseaseByDiseaseCode(clsYBSpecialTypeDisease_VO[] m_objSpeTypeDise_VO)
        {
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            for (int i = 0; i < m_objSpeTypeDise_VO.Length; i++)
            {
                strSQL = @"delete from T_OPR_YBSPECIALTYPEDISEASE A where A.deacode_chr='" + m_objSpeTypeDise_VO[i].m_strDieaseCode.Trim() + "'";
                try
                {

                    lngRes = objHRPSvc.DoExcute(strSQL);
                    if (lngRes > 0)
                    {
                        //ɾ���ü��������Ӧ��icd���¼
                        strSQL = @"delete from t_opr_ybdeadeficd10 A where A.deacode_chr='" + m_objSpeTypeDise_VO[i].m_strDieaseCode.Trim() + "'";
                        lngRes = objHRPSvc.DoExcute(strSQL);
                        //ɾ���ü��������Ӧ���շ���Ŀ��¼
                        strSQL = @"delete from t_opr_ybdeadefchargeitem A where A.deacode_chr='" + m_objSpeTypeDise_VO[i].m_strDieaseCode.Trim() + "'";
                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }


                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            objHRPSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ���ӻ����ҽ�����ֲ���Ϣ
        /// <summary>
        /// ���ӻ����ҽ�����ֲ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objYBSpeTypeDise_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthModifyYBSpecialTypeDiseaseInfo(clsYBSpecialTypeDisease_VO m_objYBSpeTypeDise_VO)
        {

            long lngRes = 0;
            DataTable m_objTable = new DataTable();
            IDataParameter[] objDBParams = null;
            string strSQL = @"select a.deacode_chr,
       a.deadesc_vchr,
       a.yearmoney_int,
       a.sort_int,
       a.status_int,
       a.note_vchr from T_OPR_YBSPECIALTYPEDISEASE A where A.deacode_chr='" + m_objYBSpeTypeDise_VO.m_strDieaseCode + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                if (m_objTable.Rows.Count > 0)//���ڣ�����¸ü�����Ӧ��ҽ�����ֲ���Ϣ
                {
                    strSQL = @"update T_OPR_YBSPECIALTYPEDISEASE A set A.DEADESC_VCHR=?,A.YEARMONEY_INT=?,A.SORT_INT=?,A.STATUS_INT=?,A.NOTE_VCHR=? where A.deacode_chr='" + m_objYBSpeTypeDise_VO.m_strDieaseCode + "'";
                    objDBParams = null;
                    objHRPSvc.CreateDatabaseParameter(5, out objDBParams);
                    objDBParams[0].Value = m_objYBSpeTypeDise_VO.m_strDieaseNamae;
                    objDBParams[1].Value = m_objYBSpeTypeDise_VO.m_floatYearMoney;
                    objDBParams[2].Value = m_objYBSpeTypeDise_VO.m_intSortNO;
                    objDBParams[3].Value = m_objYBSpeTypeDise_VO.m_intStatus;
                    objDBParams[4].Value = m_objYBSpeTypeDise_VO.m_strComment;
                    long lngRecordAffect = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordAffect, objDBParams);
                }
                else //û�У������Ӹü�����ҽ�����ֲ���Ϣ
                {
                    strSQL = @"insert into T_OPR_YBSPECIALTYPEDISEASE(deacode_chr,DEADESC_VCHR,YEARMONEY_INT,SORT_INT,STATUS_INT,NOTE_VCHR)values(?,?,?,?,?,?)";
                    objDBParams = null;
                    objHRPSvc.CreateDatabaseParameter(6, out objDBParams);
                    objDBParams[0].Value = m_objYBSpeTypeDise_VO.m_strDieaseCode;
                    objDBParams[1].Value = m_objYBSpeTypeDise_VO.m_strDieaseNamae;
                    objDBParams[2].Value = m_objYBSpeTypeDise_VO.m_floatYearMoney;
                    objDBParams[3].Value = m_objYBSpeTypeDise_VO.m_intSortNO;
                    objDBParams[4].Value = m_objYBSpeTypeDise_VO.m_intStatus;
                    objDBParams[5].Value = m_objYBSpeTypeDise_VO.m_strComment;
                    long lngRecordAffect = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordAffect, objDBParams);
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
        #region ���ݲ�ѯ�����Ͳ������ݻ�ȡ���ֲ���Ϣ
        /// <summary>
        /// ���ݲ�ѯ�����Ͳ������ݻ�ȡ���ֲ���Ϣ,����ҽ�����ֱ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objTable"></param>
        ///  <param name="m_intCondition"></param>
        ///  <param name="m_strContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetTableForYBSpeTypeDiseByCondition(int m_intCondition, string m_strContent, out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;
            string strSQL = @"select deacode_chr,
       deadesc_vchr,
       yearmoney_int,
       sort_int,
       status_int,
       note_vchr from t_opr_ybspecialtypedisease where [condition] like '%[content]%'";
            if (m_strContent.Trim() != string.Empty)
            {

                if (m_intCondition == 0)
                    strSQL = strSQL.Replace("[condition]", "DEACODE_CHR");
                if (m_intCondition == 1)
                    strSQL = strSQL.Replace("[condition]", "DEADESC_VCHR");
                if (m_intCondition == 2)
                    strSQL = strSQL.Replace("[condition]", "YEARMONEY_INT");
                strSQL = strSQL.Replace("[content]", "" + m_strContent + "");
            }
            else
            {
                strSQL = @"select deacode_chr,
       deadesc_vchr,
       yearmoney_int,
       sort_int,
       status_int,
       note_vchr from T_OPR_YBSPECIALTYPEDISEASE";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
