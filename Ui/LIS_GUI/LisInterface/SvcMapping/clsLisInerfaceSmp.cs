using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{

    /// <summary>
    /// 接口相关Smp
    /// </summary>
    internal class clsLisInerfaceSmp
    {

        #region 构造

        public clsLisInerfaceSmp()
        {
       
        }

      
        private static clsLisInerfaceSmp s_smp;
        public static clsLisInerfaceSmp s_obj
        {
            get
            {
                if (s_smp == null)
                {
                    return new clsLisInerfaceSmp();
                }

                return s_smp;
            }
        } 
        #endregion
        
        
        public long CreateApplicationArray(clsLisApplyAppliationInfo[] arrApplication,bool isSended)
        {
            return 0;
        }

    }
}
