using System;
using System.Data;
using com.digitalwave.iCare.common;
using com.digitalwave.Utility; //Utility.dll
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsController_addCheckCategory.
    /// </summary>
    public class clsController_addCheckCategory : com.digitalwave.GUI_Base.clsController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public clsController_addCheckCategory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //新增检验类别
        public long AddCheckCategory(string strCheckCategory, out string strCategoryID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            clsCheckCategory_VO objCheckCategoryVO = new clsCheckCategory_VO();
            objCheckCategoryVO.m_strCheck_Category_Name = strCheckCategory;
             lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddCheckCategory( ref objCheckCategoryVO);
            strCategoryID = objCheckCategoryVO.m_strCheck_Category_ID;
            return lngRes;
        }

        //删除检验类别
        public long DelCheckCategory(string strCategory)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckCategory( strCategory);
            return lngRes;
        }

        //查询所有的检验类别
        public long QryAllCheckCategory(out System.Data.DataTable dtbCheckCategory)
        {
            long lngRes = 0;
             lngRes = proxy.Service.m_lngGetAllCheckCategory( out dtbCheckCategory);
            return lngRes;
        }

        //更新选中的检验类别
        public long SetCheckCategory(com.digitalwave.iCare.gui.LIS.frmCheckCategory infrmCheckCategory)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            string strCheckCategory = infrmCheckCategory.txtCheckCategory.Text.ToString().Trim();
            string strCheckCategoryID = infrmCheckCategory.lsvCheckCategory.SelectedItems[0].SubItems[0].Text.ToString().Trim();
             lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetCheckCategoryInfo( strCheckCategory, strCheckCategoryID);
            if (lngRes > 0)
            {
                MessageBox.Show("记录修改成功", "检验类别", MessageBoxButtons.OK);
                infrmCheckCategory.lsvCheckCategory.SelectedItems[0].SubItems[1].Text = strCheckCategory;
            }
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
    }
}
