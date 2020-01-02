using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    internal class clsApplicationMainSmp
    {
        #region ����



        private clsApplicationMainSmp()
        { 
        } 

        public static clsApplicationMainSmp s_obj
        {
            get
            {
                return new clsApplicationMainSmp();
            }
        }

        #endregion

        /// <summary>
        /// �������뵥�Ĵ�ӡ״̬Ϊ�Ѵ�ӡ
        /// </summary>
        /// <param name="arrApplicationId">���뵥Id����</param>
        /// <returns></returns>
        public long m_mthSetApplPrinted(string[] arrApplicationId,bool isPrinted) 
        {
            long lngRes = 0;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngSetApplPrintedStatus(arrApplicationId, isPrinted);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }

    }
}
