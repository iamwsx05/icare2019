using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// 血球仪ADVIA 2120 摘要说明
    /// baojian.mo 2007-10-19 for 茶山
    /// </summary>
    public class clsDomainController_Advia2120 : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region 构造函数

        /// <summary>
        /// 构造函数

        /// </summary>
        public clsDomainController_Advia2120()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion        

        #region 查询所有的检验项目类别

        /// <summary>
        /// 查询所有的检验项目类别

        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckCategory(out clsCheckCategory_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckCategory_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckCategory(out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据检验项目分类获得检验项目

        /// <summary>
        /// 根据检验项目分类获得检验项目

        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckItemByCategoryID(string p_strCategoryID, out clsCheckItem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckItem_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckItemByCategoryID(p_strCategoryID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 查询所有的仪器检验项目与检验项目的对应关系
        /// <summary>
        /// 查询所有的仪器检验项目与检验项目的对应关系
        /// </summary>
        /// <param name="p_strDeviceModelID"></param>
        /// <param name="p_objCheckItemDeviceCheckItem"></param>
        /// <returns></returns>
        public long m_lngGetCheckItemDeviceCheckItem(string p_strDeviceModelID, out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemDeviceCheckItem(p_strDeviceModelID, out p_objCheckItemDeviceCheckItem);
            return lngRes;
        }
        #endregion

        #region 添加仪器检验项目与检验项目对应关系

        /// <summary>
        /// 添加仪器检验项目与检验项目对应关系

        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngAddNewCheckItemDeviceCheckItem(clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewCheckItemDeviceCheckItem(p_objRecord);
            return lngRes;
        }
        #endregion


        #region 修改仪器检验项目与检验项目对应关系

        /// <summary>
        /// 修改仪器检验项目与检验项目对应关系

        /// </summary>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngModifyCheckItemDeviceCheckItem(string p_strSourceCheckItemID, clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngModifyCheckItemDeviceCheckItem(p_strSourceCheckItemID, p_objRecord);
            return lngRes;
        }
        #endregion

        #region 删除仪器检验项目与检验项目对应关系

        /// <summary>
        /// 删除仪器检验项目与检验项目对应关系

        /// </summary>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <returns></returns>
        public long m_lngDelCheckItemDeviceCheckItem(string p_strSourceCheckItemID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemDeviceCheckItem(p_strSourceCheckItemID);
            return lngRes;
        }
        #endregion

        #region 插入报告单

        /// <summary>
        /// 插入报告单

        /// </summary>
        /// <param name="intNum"></param>
        /// <param name="p_objResultList"></param>
        /// <returns></returns>
        public long m_lngInsertReport(int intNum, List<clsAdvia2120ResultInf_VO> p_objResultList, out int p_intInsertNum)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngInsertReport(intNum, p_objResultList, out p_intInsertNum);
            return lngRes;
        }
        #endregion
    }
}
