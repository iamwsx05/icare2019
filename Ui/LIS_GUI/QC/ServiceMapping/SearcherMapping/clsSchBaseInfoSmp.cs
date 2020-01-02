using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsSchBaseInfoSmp
    {
        #region Property
        public static clsSchBaseInfoSmp s_object
        {
            get
            {
                return new clsSchBaseInfoSmp();
            }
        }
        #endregion

        #region Parameters


        #endregion

        #region Construtor
        public clsSchBaseInfoSmp()
        {
        }
        #endregion

        #region 返回检验项目树
        public long m_lngGetCheckItemTree(out clsLISUserGroupNode root)
        {
            root = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetCheckItemTree(out root);
            }
            catch { lngRes = 0; }
            return lngRes;
        }
        #endregion
    }
}