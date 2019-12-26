using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    public class clsTmdSchemeSmp
    {
        public static clsTmdSchemeSmp s_object
        {
            get
            {
                return new clsTmdSchemeSmp();
            }
          }

        #region Parameters
         //clsTmdSchemeSvc m_objSvc;
        #endregion

        #region Construtor
        public clsTmdSchemeSmp()
        {
            //m_objSvc = (clsTmdSchemeSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdSchemeSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZSchemeVO p_objRecord)
        {
            long lngRes = 0;
            int schemeID = -1;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdSchemeSvc(p_objRecord, out schemeID);
            }
            catch { lngRes = 0; }
            if (lngRes>0)
            {
                p_objRecord.m_intSchemeSeq = schemeID;
            }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsMFZSchemeVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate_clsTmdSchemeSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(int schemeID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete_clsTmdSchemeSvc(schemeID);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFind(int schemeID, out clsMFZSchemeVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdSchemeSvc(schemeID, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        public long m_lngFind(out clsMFZSchemeVO[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdSchemeSvc(out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}
