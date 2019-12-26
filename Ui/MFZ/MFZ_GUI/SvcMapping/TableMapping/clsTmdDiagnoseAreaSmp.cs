using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    class clsTmdDiagnoseAreaSmp
    {
        public static clsTmdDiagnoseAreaSmp s_object
        {
            get
            {
                return new clsTmdDiagnoseAreaSmp();
            }
        }

        #region Parameters
        //clsTmdDiagnoseAreaSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdDiagnoseAreaSmp()
        {
            //m_objSvc = (clsTmdDiagnoseAreaSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdDiagnoseAreaSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZDiagnoseAreaVO p_objRecord)
        {
            long lngRes = 0;
            int intDiagnoseAreaID = -1;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdDiagnoseAreaSvc(p_objRecord, out intDiagnoseAreaID);
            }
            catch { lngRes = 0; }
            if (lngRes>0)
            {
                p_objRecord.m_intDiagnoseAreaID = intDiagnoseAreaID;
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZDiagnoseAreaVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdDiagnoseAreaSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(int intDiagnoseAreaID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdDiagnoseAreaSvc(intDiagnoseAreaID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFind(int intDiagnoseAreaID, out clsMFZDiagnoseAreaVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdDiagnoseAreaSvc(intDiagnoseAreaID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFindByRoomID(int intRoomID,out clsMFZDiagnoseAreaVO p_objRecord) 
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByRoomID_clsTmdDiagnoseAreaSvc(intRoomID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(out clsMFZDiagnoseAreaVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdDiagnoseAreaSvc(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}
