using System;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_DictManage ��ժҪ˵����
    /// ���� 2004.05.26
    /// </summary>
    public class clsDomainController_DictManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public const string c_strPatientType = "61";

        #region ���캯��
        public clsDomainController_DictManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region �õ��ֵ������б� ���� 2004.05.26
        /// <summary>
        /// �����ֵ�����õ������б�(��ȥ��һ��������˵��) 
        /// ���� 2004.05.26 
        /// </summary>
        /// <param name="p_strDictKind"></param>
        /// <param name="p_dtbDictList">
        /// table name : t_aid_dict
        /// column:
        /// dictid_chr
        /// dictkind_chr
        /// dictname_vchr
        /// pycode_chr
        /// wbcode_chr
        /// jxcode_chr
        /// </param>
        /// <returns></returns>
        public long m_lngGetListFor(string p_strDictKind, out DataTable p_dtbDictList)
        {
            long lngRes = 0;
            p_dtbDictList = null;
            System.Security.Principal.IPrincipal objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDictListFor(p_strDictKind, out p_dtbDictList);

            return lngRes;
        }
        #endregion

        #region ����ֵ�����VO�б� ͯ�� 2004.06.21
        public long m_lngGetListFor(string p_strDictKind, out clsAIDDICT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDictListFor(p_strDictKind, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }

}
