using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlReport ��ժҪ˵����
    /// </summary>
    public class clsDomainControlReport : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        public clsDomainControlReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region  �б�ҩƷͳ�Ʊ���  �Ź��� 2005-02-29
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        public long m_lngGetStanMed(out System.Data.DataTable p_datStanMed)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStanMed(out p_datStanMed);

            return lngRes;
        }
        #endregion
    }
}
