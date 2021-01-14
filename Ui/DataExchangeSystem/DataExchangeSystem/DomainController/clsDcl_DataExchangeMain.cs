using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.ValueObject;

namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    public class clsDcl_DataExchangeMain
    {
        /// <summary>
        /// ��ȡҩ���������
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        public long m_lngGetInStorageData(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsInStorageData_VO[] p_ArrResult)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC));
            lngRes = objSvc.m_lngGetInStorageData(p_dtmBegin, p_dtmEnd, out p_ArrResult);
            return lngRes;
        }

        /// <summary>
        /// �ϴ�ҩ���������
        /// </summary>
        /// <param name="InStorageData"></param>
        /// <returns></returns>
        public long m_lngUploadInStorageData(clsInStorageData_VO InStorageData)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngUploadInStorageData(InStorageData);
            return lngRes;
        }

        /// <summary>
        /// ҩ�����ɾ��
        /// </summary>
        /// <param name="InHospital"></param>
        /// <returns></returns>
        public long m_lngDelInStorageData(DateTime dayTime)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
            //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngDelInStorageData(dayTime);
            return lngRes;
        }


        /// <summary>
        /// ��ȡҩ���������
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        public long m_lngGetOutStorageData(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsOutStorageData_VO[] p_ArrResult)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC));
            lngRes = objSvc.m_lngGetOutStorageData(p_dtmBegin, p_dtmEnd, out p_ArrResult);
            return lngRes;
        }

        /// <summary>
        /// �ϴ�ҩ���������
        /// </summary>
        /// <param name="OutStorageData"></param>
        /// <returns></returns>
        public long m_lngUploadOutStorageData(clsOutStorageData_VO OutStorageData)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngUploadOutStorageData(OutStorageData);
            return lngRes;
        }

        /// <summary>
        /// ҩ�����ɾ��
        /// </summary>
        /// <param name="InHospital"></param>
        /// <returns></returns>
        public long m_lngDelOutStorageData(DateTime dayTime)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
            //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngDelOutStorageData(dayTime);
            return lngRes;
        }


        #region סԺ��������
        /// <summary>
        /// ��ȡסԺ��������
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        public long m_lngGetInHospital(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsInHospital_VO[] p_ArrResult)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC));
            lngRes = objSvc.m_lngGetInHospital(p_dtmBegin, p_dtmEnd, out p_ArrResult);
            return lngRes;
        }

        /// <summary>
        /// �ϴ�סԺ��������
        /// </summary>
        /// <param name="InHospital"></param>
        /// <returns></returns>
        public long m_lngUploadInHospital(clsInHospital_VO InHospital)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngUploadInHospital(InHospital);
            return lngRes;
        }

        /// <summary>
        /// �ϴ�סԺ����ɾ��
        /// </summary>
        /// <param name="InHospital"></param>
        /// <returns></returns>
        public long m_lngDelInHospital(DateTime dayTime)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
            //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngDelInHospital(dayTime);
            return lngRes;
        }
        #endregion

        #region ������������
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        public long m_lngGetOutpatient(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsOutpatient_VO[] p_ArrResult)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsGetExchangeDataSVC));
            lngRes = objSvc.m_lngGetOutpatient(p_dtmBegin, p_dtmEnd, out p_ArrResult);
            return lngRes;
        }

        /// <summary>
        /// �ϴ�������������
        /// </summary>
        /// <param name="Outpatient"></param>
        /// <returns></returns>
        public long m_lngUploadOutpatient(clsOutpatient_VO Outpatient)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
                //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngUploadOutpatient(Outpatient);
            return lngRes;
        }

        /// <summary>
        /// ��������ɾ��
        /// </summary>
        /// <param name="InHospital"></param>
        /// <returns></returns>
        public long m_lngDelOutpatient(DateTime dayTime)
        {
            long lngRes = -1;
            com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC objSvc = new middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC();
            //(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DataExchangeSystem_Svc.clsUploadExchangeDataSVC));
            lngRes = objSvc.m_lngDelOutpatient(dayTime);
            return lngRes;
        }


        #endregion
    }
}
