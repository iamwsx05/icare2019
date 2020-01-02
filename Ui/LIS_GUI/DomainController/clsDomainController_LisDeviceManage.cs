using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainControl_LisDevice 的摘要说明。
    /// Alex 2004-5-6
    /// </summary>
    public class clsDomainController_LisDeviceManage : com.digitalwave.GUI_Base.clsDomainController_Base
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
        public clsDomainController_LisDeviceManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }
        #endregion

        #region 添加仪器检验项目与检验项目对应关系 童华 2004.07.20
        public long m_lngAddNewCheckItemDeviceCheckItem(clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewCheckItemDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改仪器检验项目与检验项目对应关系 童华 2004.07.20
        public long m_lngModifyCheckItemDeviceCheckItem(string p_strSourceCheckItemID, clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngModifyCheckItemDeviceCheckItem(p_strSourceCheckItemID, p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除仪器检验项目与检验项目对应关系 童华 2004.07.20
        public long m_lngDelCheckItemDeviceCheckItem(string p_strSourceCheckItemID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemDeviceCheckItem(p_strSourceCheckItemID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据仪器检验项目ID查询对应的仪器检验项目与检验项目的关系 童华 2004.07.20
        public long m_lngGetCheckItemDeviceCheckItem(string p_strDeviceCheckItemID,
            string p_strDeviceModelID, out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemDeviceCheckItem(p_strDeviceCheckItemID, p_strDeviceModelID, out p_objCheckItemDeviceCheckItem);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除仪器检验项目 童华 2004.07.19
        public long m_lngDelDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改仪器检验项目 童华 2004.07.19
        public long m_lngModifyDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 添加仪器检验项目 童华 2004.07.19
        public long m_lngAddNewDeviceItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddNewDeviceItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询所有的仪器检验项目与检验项目的对应关系 童华 2004.06.16
        public long m_lngGetCheckItemDeviceCheckItem(out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemDeviceCheckItem(out p_objCheckItemDeviceCheckItem);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除仪器设备 童华 2004.06.16
        public long m_lngDelDevice(string p_strDeviceID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDevice(p_strDeviceID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改仪器设备 童华 2004.06.16
        public long m_lngModifyDevice(ref clsLisDevice_VO p_objDeviceVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDevice(ref p_objDeviceVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 添加仪器设备 童华 2004.06.16
        public long m_lngAddDevice(ref clsLisDevice_VO p_objDeviceVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDevice(ref p_objDeviceVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的仪器设备列表 童华 2004.06.16
        public long m_lngGetAllDevice(out clsLisDevice_VO[] p_objLisDeviceListVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDevice(out p_objLisDeviceListVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除仪器串口通讯参数 童华 2004.06.16
        public long m_lngDelDeviceSerial(string strDeviceModelID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceSerial(strDeviceModelID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改仪器串口通讯参数 童华 2004.06.16
        public long m_lngModifyDeviceSerial(ref clsLisDeviceSerialSetUp_VO p_objDeviceSerialVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceSerial(ref p_objDeviceSerialVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 添加仪器串口通讯参数 童华 2004.06.16
        public long m_lngAddDeviceSerial(ref clsLisDeviceSerialSetUp_VO p_objDeviceSerialVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDeviceSerial(ref p_objDeviceSerialVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的仪器串口通讯参数 童华 2004.06.15
        public long m_lngGetAllDeviceSerial(out clsLisDeviceSerialSetUp_VO[] p_objDeviceSerialVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceSerial(out p_objDeviceSerialVOList);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除仪器型号 童华 2004.06.15
        public long m_lngDelDeviceModel(string strDeviceModelID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceModel(strDeviceModelID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改仪器型号 童华 2004.06.15
        public long m_lngModifyDeviceModel(ref clsLisDeviceModel_VO p_objDeviceModelVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceModel(ref p_objDeviceModelVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 添加仪器型号 童华 2004.06.15
        public long m_lngAddDeviceModel(ref clsLisDeviceModel_VO p_objDeviceModelVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDeviceModel(ref p_objDeviceModelVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的仪器型号VO列表 童华 2004.06.15
        public long m_lngGetAllDeviceModelVOList(out clsLisDeviceModel_VO[] p_objDeviceModelVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceModel(out p_objDeviceModelVOList);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据check_item_id查询对应的Device_check_item的相关信息 童华 2004.06.10
        public long m_lngGetDeviceCheckItemInfoByCheckItemID(string p_strCheckItemID, out clsLisDeviceCheckItem_VO objLisDeviceCheckItemVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetDeviceCheckItemInfoByCheckItemID(p_strCheckItemID, out objLisDeviceCheckItemVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获得所有的检验设备型号 童华 2004.05.25
        public long m_lngGetDeviceModel(out DataTable dtbDeviceModel)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceModel(out dtbDeviceModel);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询检测设备ID及名称 刘彬 2004-4-8
        /// <summary>
        /// 查询检测设备ID及名称
        /// </summary>
        /// <param name="p_dtbDeviceID_Name"></param>
        /// <returns></returns>
        public long m_lngGetDeviceID_Name(out DataTable p_dtbDeviceID_Name)
        {
            long lngRes = 0;
            p_dtbDeviceID_Name = null;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDeviceModelNameByDeviceID(out p_dtbDeviceID_Name);

            return lngRes;
        }
        #endregion

        #region 根据仪器型号ID查找所有可用的具体仪器 刘彬 2004.05.07
        /// <summary>
        ///  根据仪器型号ID查找所有的具体仪器 刘彬 2004.05.07
        /// </summary>
        /// <param name="p_objPrinipal"></param>
        /// <param name="p_strDeviceModelID"></param>
        /// <param name="p_dtbDevice"></param>
        /// <returns>
        /// DEVICEID_CHR
        /// DEVICENAME_VCHR
        /// </returns>
        public long m_lngGetDeviceByDeviceModelID(string[] p_strDeviceModelIDArr, out System.Data.DataTable p_dtbDevice)
        {
            long lngRes = 0;
            p_dtbDevice = null;
            lngRes = proxy.Service.m_lngGetDeviceByDeviceModelID(p_strDeviceModelIDArr, out p_dtbDevice);

            return lngRes;
        }
        #endregion

        #region 根据仪器类别获取仪器型号列表  童华 2004.07.19
        public long m_lngGetDeviceModelArrByDeviceCategoryID(string p_strDeviceCategoryID, out clsLisDeviceModel_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetDeviceModelArrByDeviceCategoryID(p_strDeviceCategoryID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获得检验设备的种类列表
        /// <summary>
        /// 获得检验设备的种类列表
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetLisDeviceCategory(out clsLisDeviceCategory_VO[] p_objResultArr)
        {
            p_objResultArr = new clsLisDeviceCategory_VO[0];
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetLisDeviceCategory(out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 得到所有可用的检验设备列表 刘彬 2004.05.10	
        /// <summary>
        /// 查询所有当前可用检验仪器列表,
        /// 停用日期为NULL 或 大于 当前日期 且 起用日期 小于等于当前日期,  
        /// 刘彬 2004.05.26
        /// </summary>
        /// <param name="p_dtbDeviceList">
        /// table:t_bse_lis_device
        /// column:
        /// deviceid_chr
        /// device_model_id_chr
        /// dataacquisitioncomputerip_chr
        /// begin_date_dat
        /// end_date_dat
        /// devicename_vchr
        /// place_vchr
        /// deptid_chr
        /// isdatatrans_int
        /// </param>
        /// <returns></returns>
        public long m_lngGetDeviceList(out DataTable p_dtbDeviceList)
        {
            long lngRes = 0;
            p_dtbDeviceList = null;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDeviceList(out p_dtbDeviceList);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 通过仪器类别获得检验设备
        /// <summary>
        /// 通过仪器类别获得检验设备
        /// </summary>
        /// <param name="p_strDeviceCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetListDeviceByCategoryID(string p_strDeviceCategoryID, out clsLisDevice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsLisDevice_VO[0];
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetListDevice(p_strDeviceCategoryID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获得该型号检验设备的检验项目
        /// <summary>
        /// 获得该型号检验设备的检验项目
        /// </summary>
        /// <param name="p_strModelID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckItemByModelID(string p_strModelID, out clsCheckItemAndDeviceItem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckItemAndDeviceItem_VO[0];
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemByModelID(p_strModelID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据仪器型号获取设备的检验项目
        public long m_lngGetCheckItemByModelID(string p_strModelID, out clsLisDeviceCheckItem_VO[] p_objResultArr)
        {
            return proxy.Service.m_lngGetCheckItemByModelID(p_strModelID, out p_objResultArr);

        }
        #endregion

        #region 添加设备检验项目
        /// <summary>
        /// 添加设备检验项目
        /// </summary>
        /// <param name="p_strModelID"></param>
        /// <param name="p_intGraphFlag"></param>
        /// <param name="p_objItem"></param>
        public void m_mthDoAddNew(string p_strModelID, int p_intGraphFlag, clsCheckItemAndDeviceItem_VO p_objItem)
        {
            long lngRes = proxy.Service.m_lngDoAddNewDeviceItem(p_strModelID, p_intGraphFlag, p_objItem);
            //			objSvc.Dispose();
        }
        #endregion

        #region 修改设备检验项目
        /// <summary>
        /// 修改设备检验项目
        /// </summary>
        /// <param name="p_strModelID"></param>
        /// <param name="p_intGraphFlag"></param>
        /// <param name="p_objItem"></param>
        public void m_mthDoModify(string p_strModelID, int p_intGraphFlag, clsCheckItemAndDeviceItem_VO p_objItem)
        {
            long lngRes = proxy.Service.m_lngDoModifyDeviceItem(p_strModelID, p_intGraphFlag, p_objItem);
            //			objSvc.Dispose();
        }
        #endregion

        #region 删除设备检验项目
        /// <summary>
        /// 删除设备检验项目
        /// </summary>
        /// <param name="p_strModelID"></param>
        /// <param name="p_objItem"></param>
        public void m_mthDoDelete(string p_strModelID, clsCheckItemAndDeviceItem_VO p_objItem)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            long lngRes = proxy.Service.m_lngDeleteDeviceCheckItem(p_strModelID, p_objItem);
            //			objSvc.Dispose();
        }
        #endregion

        #region 添加或修改特殊仪器参数 2011-12-5
        /// <summary>
        /// 添加或修改特殊仪器参数
        /// </summary>
        /// <param name="p_objEquipVo"></param>
        /// <param name="p_blnAdd"></param>
        /// <returns></returns>
        public long m_lngAddSpecialDevice(clsLIS_Equip_DB_ConfigVO p_objEquipVo, bool p_blnAdd)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddSpecialDevice(p_objEquipVo, p_blnAdd);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有特殊仪器参数信息 2011-12-5 shichun.chen
        /// <summary>
        /// 获取所有特殊仪器参数信息
        /// </summary>
        /// <param name="p_objEquipVOArr"></param>
        /// <returns></returns>
        public long m_lngQuerySepcialDeviceInfo(out clsLIS_Equip_DB_ConfigVO[] p_objEquipVOArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngQuerySepcialDeviceInfo(out p_objEquipVOArr);
            return lngRes;
        }
        #endregion

        #region 删除特殊仪器参数 2011-12-5
        /// <summary>
        /// 删除特殊仪器参数
        /// </summary>
        /// <param name="p_strDeviceModelID"></param>
        /// <returns></returns>
        public long m_lngDeleteSpecialDevice(string p_strDeviceModelID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDeleteSpecialDevice(p_strDeviceModelID);
            return lngRes;
        }
        #endregion
    }
}
