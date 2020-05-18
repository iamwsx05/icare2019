using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.BIHOrderServer
{
    /// <summary>
    /// ����ע���ֵ�
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsSpecialRemarkDicService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ȡ����ע��ID
        /// <summary>
        ///  ��ȡ����ע��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strSpecialRemarkID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialRemarkID(ref string m_strSpecialRemarkID)
        {
            long m_lngRes = -1;
            string m_strSQL = @"SELECT MAX (TO_NUMBER (a.remarkid_chr)) + 1 AS maxid
                              FROM t_bse_bih_specremark a";
            try
            {
                DataTable m_objTable = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
                m_lngRes = m_objHRP.lngGetDataTableWithoutParameters(m_strSQL, ref m_objTable);
                if (m_lngRes > 0)
                {
                    m_strSpecialRemarkID = m_objTable.Rows[0][0].ToString().Trim();
                    if (m_strSpecialRemarkID == string.Empty)
                    {
                        m_strSpecialRemarkID = "0000001";
                    }
                }
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }

            return m_lngRes;

        }
        #endregion 
        #region ��ȡ����ע���ֵ�
        /// <summary>
        /// ��ȡ����ע���ֵ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objTable"></param>
        [AutoComplete]
        public long m_lngGetSpecialRemarkDic(out DataTable m_objTable)
        {
            long m_lngRes = -1;
            m_objTable = new DataTable();
            string m_strSQL = @"SELECT A.remarkid_chr, A.remarkname_vchr, A.usercode_vchr,case when A.chargectl_int=0 then '������'
                                when A.chargectl_int=1 then '����' end as chargectl_status FROM t_bse_bih_specremark A order by A.remarkid_chr";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
                m_lngRes = m_objHRP.lngGetDataTableWithoutParameters(m_strSQL, ref m_objTable);

            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }

            return m_lngRes;

        }
        #endregion
        #region ���ݲ�ѯ�����Ͳ�ѯ���ݻ�ȡ����ע���ֵ�
        /// <summary>
        /// ���ݲ�ѯ�����Ͳ�ѯ���ݻ�ȡ����ע���ֵ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strConditionIndex"></param>
        /// <param name="m_strSearchContent"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecialRemarkDicByCondition(string m_strConditionIndex, string m_strSearchContent, out DataTable m_objTable)
        {
            long m_lngRes = -1;
            m_objTable = new DataTable();
            string m_strSQL = @"SELECT A.remarkid_chr, A.remarkname_vchr, A.usercode_vchr,case when A.chargectl_int=0 then '������'
                                when A.chargectl_int=1 then '����' end as chargectl_status FROM t_bse_bih_specremark A ";
            switch (m_strConditionIndex.Trim())
            {
                case "0": m_strSQL += " where A.remarkid_chr like'%" + m_strSearchContent.Trim() + "'order by A.remarkid_chr"; break;
                case "1": m_strSQL += " where A.remarkname_vchr like'%" + m_strSearchContent.Trim() + "%'order by A.remarkid_chr"; break;
                case "2": m_strSQL += " where A.usercode_vchr like'%" + m_strSearchContent.Trim() + "%'order by A.remarkid_chr"; break;
                case "3":
                    if (m_strSearchContent.Trim() == "����")
                    {
                        m_strSQL += "where A.chargectl_int=1 order by A.remarkid_chr";
                    }
                    else if (m_strSearchContent.Trim() == "������")
                    {
                        m_strSQL += "where A.chargectl_int=0 order by A.remarkid_chr";
                    }
                    break;
                default: break;
            }
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
                m_lngRes = m_objHRP.lngGetDataTableWithoutParameters(m_strSQL, ref m_objTable);

            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return m_lngRes;
        }
        #endregion
        #region ��������ע���ֵ�
        /// <summary>
        /// ��������ע���ֵ�
        /// </summary>
        /// <param name="p_objPricipal"></param>
        /// <param name="m_objVo"></param>
        /// <param name="m_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifySpecialRemakDic(clsSpecialRemarkDicVo m_objVo, ref string m_strResult)
        {
            long m_lngRes = -1;
            try
            {
                if (m_objVo == null)
                    return -1;
                DataTable m_objTable = new DataTable();
                string m_strSQL = @"SELECT *
                                    FROM t_bse_bih_specremark a
                                    WHERE a.remarkid_chr = '" + m_objVo.m_strRemarkID.Trim() + "'";
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
                m_lngRes = m_objHRP.lngGetDataTableWithoutParameters(m_strSQL, ref m_objTable);
                if (m_lngRes > 0)
                {
                    if (m_objTable.Rows.Count > 0)
                    {
                        m_strSQL = @"UPDATE t_bse_bih_specremark a
                                     SET a.remarkname_vchr = '" + m_objVo.m_strRemarkContent.Trim() + "', a.usercode_vchr = '" + m_objVo.m_strUserCode.Trim() + "',  a.chargectl_int =" + m_objVo.m_intDebtControll + " WHERE a.remarkid_chr = '" + m_objVo.m_strRemarkID.Trim() + "'";
                        m_lngRes = m_objHRP.DoExcute(m_strSQL);
                        if (m_lngRes > 0)
                        {
                            m_strResult = "�޸ĳɹ���";
                        }
                        else
                        {
                            m_strResult = "�޸�ʧ�ܣ�";
                        }
                    }
                    else
                    {
                        m_strSQL = @"INSERT INTO t_bse_bih_specremark a (a.remarkid_chr, a.remarkname_vchr, a.usercode_vchr, a.chargectl_int)VALUES ('" + m_objVo.m_strRemarkID.Trim() + "', '" + m_objVo.m_strRemarkContent.Trim() + "', '" + m_objVo.m_strUserCode.Trim() + "', " + m_objVo.m_intDebtControll + ")";
                        m_lngRes = m_objHRP.DoExcute(m_strSQL);
                        if (m_lngRes > 0)
                        {
                            m_strResult = "��ӳɹ���";
                        }
                        else
                        {
                            m_strResult = "���ʧ�ܣ�";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }

            return m_lngRes;


        }
        #endregion
        #region ɾ������ע���ֵ�
        /// <summary>
        /// ����ע�ͱ���ɾ������ע���ֵ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strSpecialRemakID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteSpecialRemarkDicByID(string m_strSpecialRemakID)
        {
            long m_lngRes = -1;
            string m_strSQL = @"delete from t_bse_bih_specremark a where  a.remarkid_chr='" + m_strSpecialRemakID.ToString().Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
                m_lngRes = m_objHRP.DoExcute(m_strSQL);
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }

            return m_lngRes;
        }
        #endregion 
    }
}
