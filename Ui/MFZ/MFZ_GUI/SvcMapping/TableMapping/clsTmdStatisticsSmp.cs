using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.MFZ
{
    internal class clsTmdStatisticsSmp
    {
        internal static clsTmdStatisticsSmp s_object
        {
            get
            {
                return new clsTmdStatisticsSmp();
            }
        }

        //clsTmdStatisticsSvc m_objSvc;

        #region Construtor
        public clsTmdStatisticsSmp()
        {
            //m_objSvc = (clsTmdStatisticsSvc)
            //    com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //    typeof(clsTmdStatisticsSvc));
        }
        #endregion

        #region INSERT
        public long m_lngInsert(clsMFZStatistics p_objRecord)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert_clsTmdStatisticsSvc(p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion

        #region FIND
        internal long m_lngFind(out DataTable p_objRecord)
        {
            long lngRes = 0;
            p_objRecord = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind_clsTmdStatisticsSvc(out p_objRecord);
            }
            catch { lngRes = 0; }
            return lngRes;
        }

        internal long m_lngSaved(clsMFZStatistics p_objRecord, out int count)
        {

            long lngRes = 0;
            count = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngSaved_clsTmdStatisticsSvc(p_objRecord,out count);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
           
        #endregion

    }
}
