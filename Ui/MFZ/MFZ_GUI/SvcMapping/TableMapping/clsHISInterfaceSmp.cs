using System;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// HIS½Ó¿Ú
    /// </summary>
    public class clsHISInterfaceSmp
    {
        public static clsHISInterfaceSmp s_object
        {
            get
            {
                return new clsHISInterfaceSmp();
            }
        }

        #region Parameters
        //clsHISInterfaceSvc m_objSvc;
        #endregion

        #region Construtor
        public clsHISInterfaceSmp()
        {
            //m_objSvc = (clsHISInterfaceSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsHISInterfaceSvc));
        }
        #endregion

        #region FIND
        public long m_lngFind(string cardID, out clsMFZPatientVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsHISInterfaceSvc(cardID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        public long m_lngFind(string cardID,int AreaID, out clsMFZPatientVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsHISInterfaceSvc(cardID,AreaID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class clsQueueManageSmp
    {
        public static clsQueueManageSmp s_object
        {
            get
            {
                return new clsQueueManageSmp();
            }
        }

        #region Parameters
        //clsQueueManageSvc m_objSvc;
        #endregion

        #region Construtor
        public clsQueueManageSmp()
        {
            //m_objSvc = (clsQueueManageSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsQueueManageSvc));
        }
        #endregion

        #region FIND
        public long m_lngFind(int areaID,int schemeID,out clsMFZDeptVO[] depts,out clsMFZRoomVO[] rooms,out clsMFZWorkStationVO[] stations,out clsMFZDoctorVO[] doctors)
        {
            depts = null;
            rooms = null;
            stations = null;
            doctors = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDoctorRoomStationAll_clsQueueManageSvc(areaID,schemeID, out depts, out rooms, out stations, out doctors);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}