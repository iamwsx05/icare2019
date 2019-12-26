using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    class clsTmdDeptSmp
    {
        public static clsTmdDeptSmp s_object
        {
            get
            {
                return new clsTmdDeptSmp();
            }
        }

        #region Parameters
        //clsTmdDeptSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdDeptSmp()
        {
            //m_objSvc = (clsTmdDeptSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdDeptSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZDeptVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdDeptSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZDeptVO p_objRecord,string oldDeptID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdDeptSvc(p_objRecord, oldDeptID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(string deptID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdDeptSvc(deptID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFindByDeptID(string deptID, out clsMFZDeptVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDeptID_clsTmdDeptSvc(deptID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFindByDiagnoseAreaID(int intDiagnoseAreaID,int schemeid, out clsMFZDeptVO[] p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFindByDiagnoseAreaID_clsTmdDeptSvc(intDiagnoseAreaID,schemeid, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(out clsMFZDeptVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdDeptSvc(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}
