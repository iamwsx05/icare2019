using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsSchQCBatchSmp
    {
        #region Property
        public static clsSchQCBatchSmp s_object
        {
            get
            {
                return new clsSchQCBatchSmp();
            }
        }
        #endregion

        #region Parameters


        #endregion

        #region Construtor
        public clsSchQCBatchSmp()
        { 
        }
        #endregion

        public long m_lngFindQCBatchCombinatorial(clsLisQCBatchSchVO p_objCondition, out clsLisQCBatchVO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatchCombinatorial(p_objCondition, out p_objRecordArr);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
    }
}