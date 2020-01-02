using System;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.LIS
{
    public class clsTmdQCSampleLotParaSmp
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }

        }
        public static clsTmdQCSampleLotParaSmp s_object
        {
            get
            {
                return new clsTmdQCSampleLotParaSmp();
            }
        }

        #region Parameters

        #endregion

        #region Construtor
        public clsTmdQCSampleLotParaSmp()
        {
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsLisQCSampleLotParaVO p_objRecord)
        {
            int intID = -1;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsert(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region UPDATE
        public long m_lngUpdate(clsLisQCSampleLotParaVO p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdate(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region DELETE
        public long m_lngDelete(string p_strCheckItemId, int p_intQCSmplotSeq)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDelete(p_strCheckItemId, p_intQCSmplotSeq);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        public long m_lngFind(string p_strCheckItemId, int p_intQCSmplotSeq, out clsLisQCSampleLotParaVO p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(p_strCheckItemId, p_intQCSmplotSeq, out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}