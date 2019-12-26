using System;
using weCare.Core.Entity; 

namespace com.digitalwave.iCare.gui.MFZ
{
    public class clsTmdWorkStationSmp
    {
        public static clsTmdWorkStationSmp s_object
        {
            get
            {
                return new clsTmdWorkStationSmp();
            }
        }

        #region Parameters
        //clsTmdWorkStationSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdWorkStationSmp()
        {
            //m_objSvc = (clsTmdWorkStationSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdWorkStationSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZWorkStationVO p_objRecord)
        {
            int WorkStationID=-1;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdWorkStationSvc(p_objRecord,out WorkStationID);
            }
            catch { lngRes = 0; }
            if (lngRes > 0)
            {
                p_objRecord.m_intWorkStationID = WorkStationID;
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZWorkStationVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdWorkStationSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(int WorkStationID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdWorkStationSvc(WorkStationID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFind(int WorkStationID, out clsMFZWorkStationVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdWorkStationSvc(WorkStationID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(out clsMFZWorkStationVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdWorkStationSvc(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(int roomID,out clsMFZWorkStationVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdWorkStationSvc(roomID,out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFindByDoctorID(string doctorID,int schemeID, out clsMFZWorkStationVO p_objRecord) 
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDoctorID_clsTmdWorkStationSvc(doctorID,schemeID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFindDoctorStations(int roomID,int schemeID, out clsMFZDoctorStationVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindDoctorStations_clsTmdWorkStationSvc(roomID,schemeID, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}