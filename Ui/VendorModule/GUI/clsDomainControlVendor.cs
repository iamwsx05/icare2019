using System;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;	//GUI_Base.dll

namespace com.digitalwave.iCare.gui.VendorManage
{
    /// <summary>
    /// clsDomainControlVendor 的摘要说明。
    /// kong 2004-05-10
    /// </summary>
    public class clsDomainControlVendor : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造函数
        public clsDomainControlVendor()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 模糊查询供应商列表  欧阳孔伟  2004-06-05
        /// <summary>
        /// 模糊查询获得供应商列表
        /// </summary>
        /// <param name="p_strSQL">SQL脚本</param>
        /// <param name="p_dtbResult">输出信息</param>
        /// <returns>返回1为成功，非1为失败</returns>
        public long m_lngGetVendorByAny(string p_strSQL, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngFindVendorByAny(p_strSQL, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region 根据ID获得供应商   欧阳孔伟  2004-06-05
        /// <summary>
        /// 根据ID获得供应商信息
        /// </summary>
        /// <param name="p_strVendorID">供应商ID</param>
        /// <param name="p_dtbResult">输出信息</param>
        /// <returns>返回1为成功，非1为失败</returns>
        public long m_lngGetVendorByID(string p_strVendorID, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null;
             

            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngFindVendorByID(  p_strVendorID, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region 根据类别获得供应商   欧阳孔伟  2004-06-05
        /// <summary>
        /// 根据类别获得供应商信息
        /// </summary>
        /// <param name="p_intType">供应商类别</param>
        /// <param name="p_dtbResult">输出信息</param>
        /// <returns>返回1为成功，非1为失败</returns>
        public long m_lngGetVendorByType(int p_intType, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngFindVendorByType( p_intType, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region 根据产品类型获得供应商   欧阳孔伟  2004-06-05
        /// <summary>
        /// 根据产品类型获得供应商信息
        /// </summary>
        /// <param name="p_intProductType">产品类型</param>
        /// <param name="p_dtbResult">输出信息</param>
        /// <returns>返回1为成功，非1为失败</returns>
        public long m_lngGetVendorByProductType(int p_intProductType, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngFindVendorByProductType(  p_intProductType, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region 获得最大的ID  欧阳孔伟  2004-06-05
        /// <summary>
        ///  获得最大的ID
        /// </summary>
        /// <returns></returns>
        public string m_strGetMaxID()
        {
            string strResult = "";
            long lngRes = 0; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngGetMaxID( out strResult);

            int intID = 1;

            if (strResult == "")
            {
                intID = 1;
                strResult = intID.ToString("0000000000");
            }
            else
            {
                intID = int.Parse(strResult);
                if (intID > 0)
                {
                }
                else
                {
                    intID = 1;
                    strResult = intID.ToString("0000000000");
                }
            }

            return strResult;

        }
        #endregion

        #region 添加供应商  欧阳孔伟  2004-06-05
        /// <summary>
        /// 添加新的供应商信息
        /// </summary>
        /// <param name="p_objVendor">供应商VO</param>
        /// <returns></returns>
        public long m_lngDoAddNew(clsVendor_VO p_objVendor)
        {
            long lngRes = 0; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngDoAddNewVendor(  p_objVendor);

            return lngRes;
        }
        #endregion

        #region 修改供应商  欧阳孔伟  2004-06-05
        /// <summary>
        /// 修改供应商信息
        /// </summary>
        /// <param name="p_objVendor">供应商信息</param>
        /// <returns></returns>
        public long m_lngDoModify(clsVendor_VO p_objVendor)
        {
            long lngRes = 0; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngDoUpdVendorByID( p_objVendor);

            return lngRes;
        }
        #endregion

        #region 删除供应商  欧阳孔伟  2004-06-05
        /// <summary>
        /// 删除选中的供应商
        /// </summary>
        /// <param name="p_strVendorID">供应商ID</param>
        /// <returns></returns>
        public long m_lngDoDelete(string p_strVendorID)
        {
            long lngRes = 0; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngDeleteVendorByID(  p_strVendorID);

            return lngRes;
        }
        #endregion

        #region 获取助记码
        /// <summary>
        /// 获取助记码
        /// </summary>
        /// <param name="returnHelpCode"></param>
        /// <returns></returns>
        public long m_lngGetHelpCode(out string returnHelpCode)
        {
            long lngRes = 0; 
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngGetHelpCode( out returnHelpCode);

            return lngRes;
        }
        #endregion

        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <param name="p_strCode"></param>
        /// <param name="p_strName"></param>
        /// <param name="p_strAlias"></param>
        /// <param name="p_intType"></param>
        /// <param name="p_strPhone"></param>
        /// <param name="p_strAddress"></param>
        /// <param name="p_strContact"></param>
        /// <param name="p_strContactPhone"></param>
        /// <param name="p_strFax"></param>
        /// <param name="p_strEmail"></param>
        /// <param name="p_strPYCode"></param>
        /// <param name="p_strWBCode"></param>
        /// <param name="p_dtVendorInfo"></param>
        /// <returns></returns>
        internal long m_lngGetVendorInfo(string p_strCode, string p_strName, string p_strAlias, int p_intType, string p_strPhone, string p_strAddress,
            string p_strContact, string p_strContactPhone, string p_strFax, string p_strEmail, string p_strPYCode, string p_strWBCode, out System.Data.DataTable p_dtVendorInfo)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngGetVendorInfo(  p_strCode, p_strName, p_strAlias, p_intType, p_strPhone, p_strAddress,
            p_strContact, p_strContactPhone, p_strFax, p_strEmail, p_strPYCode, p_strWBCode, out p_dtVendorInfo);
            return lngRes;
        }
    }
}
