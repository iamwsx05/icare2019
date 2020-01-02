using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    public class clsTmdRoomSmp
    {
        public static clsTmdRoomSmp s_object
        {
            get
            {
                return new clsTmdRoomSmp();
            }
        }

        #region Parameters
        //clsTmdRoomSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdRoomSmp()
        {
            //m_objSvc = (clsTmdRoomSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdRoomSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZRoomVO p_objRecord)
        {
            int roomID = -1;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdRoomSvc(p_objRecord,out roomID);
            }
            catch { lngRes = 0; }
            if (lngRes > 0)
            {
                p_objRecord.m_intRoomID = roomID;
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZRoomVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdRoomSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(int roomID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdRoomSvc(roomID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND

        public long m_lngFind(int roomID, out clsMFZRoomVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdRoomSvc(roomID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        public long m_lngFind(out clsMFZRoomVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdRoomSvc(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }



        public long m_lngFindByAreaID(int diagnoseAreaID, int schemeId, out clsMFZRoomVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByAreaID_clsTmdRoomSvc(diagnoseAreaID, schemeId, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        /// <summary>
        /// 查找诊区下的诊室列表
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFind(int areaId,out clsMFZRoomVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdRoomSvc(areaId, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        #endregion
    }
}