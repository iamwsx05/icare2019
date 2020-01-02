using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    public class clsTmdDoctorSmp
    {
        public static clsTmdDoctorSmp s_object
        {
            get
            {
                return new clsTmdDoctorSmp();
            }
        }

        #region Parameters
        //clsTmdDoctorSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdDoctorSmp()
        {
            //m_objSvc = (clsTmdDoctorSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdDoctorSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZDoctorVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdDoctorSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZDoctorVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdDoctorSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(string doctorID, string deptID, int schemeID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdDoctorSvc(doctorID,deptID,schemeID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        public long m_lngDelete(int areaId, int schemeId)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdDoctorSvc(areaId, schemeId);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND

        /// <summary>
        /// ���ҿ���ҽ��(�������Ѿ���������)
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="schemeID"></param>
        /// <param name="deptID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFindDoctorsByDeptID(int areaId, int schemeID, string deptID, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorsByDeptID_clsTmdDoctorSvc(areaId,schemeID,deptID,out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        /// <summary>
        /// ���ҿ�������ҽ��
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="schemeID"></param>
        /// <param name="deptID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFindDoctorsByDeptID(string deptID, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorsByDeptID_clsTmdDoctorSvc(deptID, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        /// <summary>
        /// ����ĳ���Ų����µ�����ҽ��,�ж�ĳ������ӵ�е������Ƿ�Ϊ��!
        /// </summary>
        /// <param name="deptID">����Id</param>
        /// <returns></returns>
        //private long m_lngFindDoctorsByDeptID(string deptID, out clsMFZDoctorVO[] p_objRecordArr)
        //{
        //    long lngRes = 0;
        //    p_objRecordArr = null;
        //    try
        //    {
        //        lngRes = m_objSvc.m_lngFindDoctorsByDeptID(deptID, out p_objRecordArr);
        //    }
        //    catch { lngRes = 0; }
        //    return lngRes;
        //}
        
        /// <summary>
        /// ���������µ�ҽ���б�
        /// </summary>
        /// <param name="deptID">����ID</param>
        /// <param name="p_objRecordArr">ҽ���б�</param>
        /// <returns></returns>
        public long m_lngFindNoWorkStationDoctorsByAreaID(int areaID, int schemeID, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindNoWorkStationDoctorsByAreaID_clsTmdDoctorSvc(areaID,schemeID, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        ///// <summary>
        ///// ���Ҳ����������ŵ�����ҽ����ҽ���Űࡳ
        ///// </summary>
        ///// <param name="areaId">�����ɣ�</param>
        ///// <param name="p_objRecordArr"></param>
        ///// <returns></returns>
        //private long m_lngFindNotInAreaDoctor(int areaId, out clsMFZDoctorVO[] p_objRecordArr)
        //{
        //    long lngRes = 0;
        //    p_objRecordArr = null;
        //    try
        //    {
        //        lngRes = m_objSvc.m_lngFindNotInAreaDoctor(areaId, out p_objRecordArr);
        //    }
        //    catch { lngRes = 0; }
        //    return lngRes;
        //}

        /// <summary>
        /// ���ҹ���վ�µ�ҽ��
        /// </summary>
        /// <param name="stationId">����վID</param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFindDoctorsByStationId(int stationId, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorsByWorkStationID_clsTmdDoctorSvc(stationId, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        /// <summary>
        /// ����schemeId����ҽ������(����ҽ������)
        /// </summary>
        /// <param name="schemeId">schemeId</param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFindDoctorsBySchemeId(int schemeId, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorsBySchemeID_clsTmdDoctorSvc(schemeId, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        /// <summary>
        /// ����ҽ���б�
        /// </summary>
        /// <param name="areaID">����Id</param>
        /// <param name="schemeID">�ƻ�,����Id</param>
        /// <param name="p_objResultArr">����б�</param>
        /// <returns></returns>
        public long m_lngFindDoctorsByAreaID(int areaId, int schemeID, out clsMFZDoctorVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorsByAreaID_clsTmdDoctorSvc(areaId, schemeID, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        
        #endregion
    }
}
