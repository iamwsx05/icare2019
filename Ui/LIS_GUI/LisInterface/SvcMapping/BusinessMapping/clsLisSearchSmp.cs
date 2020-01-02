﻿using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.LIS
{
    internal class clsLisSearchSmp
    {
          #region 构造


        private clsLisSearchSmp()
        { 
        }
      
        public static clsLisSearchSmp s_obj
        {
            get
            {
                return new clsLisSearchSmp();
            }
        }

        #endregion

        public long m_lngGetBIHQuery(string p_strFromDate, string p_strToDate, string p_strAppDeptID, 
                                        int p_intStatus, string p_strPatientName, string p_strPatientCardID, 
                                        string p_strHosipitalNO, String bedNo,int p_intSampleBack, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            p_objAppVOArr = null;
            try
            {
                
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAppAndSampleInfo(p_intStatus, p_strAppDeptID, p_strFromDate, p_strToDate, p_strPatientName, p_strPatientCardID, p_strHosipitalNO, bedNo,p_intSampleBack, out p_objAppVOArr);
            }
            catch (Exception ex)
            {
                lngRes = 0;
                new clsLogText().LogError(ex.Message);
            }

            return lngRes;
        }
    }
}
