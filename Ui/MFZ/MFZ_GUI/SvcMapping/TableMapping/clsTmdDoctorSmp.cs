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
        /// 查找科室医生(不包含已经分配诊室)
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
        /// 查找科室所有医生
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
        /// 查找某部门部门下的所有医生,判断某部门所拥有的诊室是否为空!
        /// </summary>
        /// <param name="deptID">部门Id</param>
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
        /// 查找诊区下的医生列表
        /// </summary>
        /// <param name="deptID">诊区ID</param>
        /// <param name="p_objRecordArr">医生列表</param>
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
        ///// 查找不在诊区部门的所有医生〔医生排班〕
        ///// </summary>
        ///// <param name="areaId">诊区Ｉｄ</param>
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
        /// 查找工作站下的医生
        /// </summary>
        /// <param name="stationId">工作站ID</param>
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
        /// 根据schemeId查找医生集合(不含医生姓名)
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
        /// 查找医生列表
        /// </summary>
        /// <param name="areaID">诊区Id</param>
        /// <param name="schemeID">计划,安排Id</param>
        /// <param name="p_objResultArr">结果列表</param>
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
