using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainControl_LisDevice ��ժҪ˵����
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

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainController_LisDeviceManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //

        }
        #endregion

        #region �������������Ŀ�������Ŀ��Ӧ��ϵ ͯ�� 2004.07.20
        public long m_lngAddNewCheckItemDeviceCheckItem(clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewCheckItemDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�����������Ŀ�������Ŀ��Ӧ��ϵ ͯ�� 2004.07.20
        public long m_lngModifyCheckItemDeviceCheckItem(string p_strSourceCheckItemID, clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngModifyCheckItemDeviceCheckItem(p_strSourceCheckItemID, p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ������������Ŀ�������Ŀ��Ӧ��ϵ ͯ�� 2004.07.20
        public long m_lngDelCheckItemDeviceCheckItem(string p_strSourceCheckItemID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemDeviceCheckItem(p_strSourceCheckItemID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������������ĿID��ѯ��Ӧ������������Ŀ�������Ŀ�Ĺ�ϵ ͯ�� 2004.07.20
        public long m_lngGetCheckItemDeviceCheckItem(string p_strDeviceCheckItemID,
            string p_strDeviceModelID, out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemDeviceCheckItem(p_strDeviceCheckItemID, p_strDeviceModelID, out p_objCheckItemDeviceCheckItem);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ������������Ŀ ͯ�� 2004.07.19
        public long m_lngDelDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�����������Ŀ ͯ�� 2004.07.19
        public long m_lngModifyDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceCheckItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������������Ŀ ͯ�� 2004.07.19
        public long m_lngAddNewDeviceItem(clsLisDeviceCheckItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddNewDeviceItem(p_objRecord);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���е�����������Ŀ�������Ŀ�Ķ�Ӧ��ϵ ͯ�� 2004.06.16
        public long m_lngGetCheckItemDeviceCheckItem(out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemDeviceCheckItem(out p_objCheckItemDeviceCheckItem);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ�������豸 ͯ�� 2004.06.16
        public long m_lngDelDevice(string p_strDeviceID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDevice(p_strDeviceID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸������豸 ͯ�� 2004.06.16
        public long m_lngModifyDevice(ref clsLisDevice_VO p_objDeviceVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDevice(ref p_objDeviceVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������豸 ͯ�� 2004.06.16
        public long m_lngAddDevice(ref clsLisDevice_VO p_objDeviceVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDevice(ref p_objDeviceVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���е������豸�б� ͯ�� 2004.06.16
        public long m_lngGetAllDevice(out clsLisDevice_VO[] p_objLisDeviceListVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDevice(out p_objLisDeviceListVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ����������ͨѶ���� ͯ�� 2004.06.16
        public long m_lngDelDeviceSerial(string strDeviceModelID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceSerial(strDeviceModelID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸���������ͨѶ���� ͯ�� 2004.06.16
        public long m_lngModifyDeviceSerial(ref clsLisDeviceSerialSetUp_VO p_objDeviceSerialVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceSerial(ref p_objDeviceSerialVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����������ͨѶ���� ͯ�� 2004.06.16
        public long m_lngAddDeviceSerial(ref clsLisDeviceSerialSetUp_VO p_objDeviceSerialVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDeviceSerial(ref p_objDeviceSerialVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���е���������ͨѶ���� ͯ�� 2004.06.15
        public long m_lngGetAllDeviceSerial(out clsLisDeviceSerialSetUp_VO[] p_objDeviceSerialVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceSerial(out p_objDeviceSerialVOList);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ�������ͺ� ͯ�� 2004.06.15
        public long m_lngDelDeviceModel(string strDeviceModelID)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngDelDeviceModel(strDeviceModelID);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸������ͺ� ͯ�� 2004.06.15
        public long m_lngModifyDeviceModel(ref clsLisDeviceModel_VO p_objDeviceModelVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngModifyDeviceModel(ref p_objDeviceModelVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ͺ� ͯ�� 2004.06.15
        public long m_lngAddDeviceModel(ref clsLisDeviceModel_VO p_objDeviceModelVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngAddDeviceModel(ref p_objDeviceModelVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���е������ͺ�VO�б� ͯ�� 2004.06.15
        public long m_lngGetAllDeviceModelVOList(out clsLisDeviceModel_VO[] p_objDeviceModelVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceModel(out p_objDeviceModelVOList);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����check_item_id��ѯ��Ӧ��Device_check_item�������Ϣ ͯ�� 2004.06.10
        public long m_lngGetDeviceCheckItemInfoByCheckItemID(string p_strCheckItemID, out clsLisDeviceCheckItem_VO objLisDeviceCheckItemVO)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetDeviceCheckItemInfoByCheckItemID(p_strCheckItemID, out objLisDeviceCheckItemVO);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������еļ����豸�ͺ� ͯ�� 2004.05.25
        public long m_lngGetDeviceModel(out DataTable dtbDeviceModel)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllDeviceModel(out dtbDeviceModel);
            //			objDeviceSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ����豸ID������ ���� 2004-4-8
        /// <summary>
        /// ��ѯ����豸ID������
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

        #region ���������ͺ�ID�������п��õľ������� ���� 2004.05.07
        /// <summary>
        ///  ���������ͺ�ID�������еľ������� ���� 2004.05.07
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

        #region ������������ȡ�����ͺ��б�  ͯ�� 2004.07.19
        public long m_lngGetDeviceModelArrByDeviceCategoryID(string p_strDeviceCategoryID, out clsLisDeviceModel_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetDeviceModelArrByDeviceCategoryID(p_strDeviceCategoryID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ü����豸�������б�
        /// <summary>
        /// ��ü����豸�������б�
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

        #region �õ����п��õļ����豸�б� ���� 2004.05.10	
        /// <summary>
        /// ��ѯ���е�ǰ���ü��������б�,
        /// ͣ������ΪNULL �� ���� ��ǰ���� �� �������� С�ڵ��ڵ�ǰ����,  
        /// ���� 2004.05.26
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

        #region ͨ����������ü����豸
        /// <summary>
        /// ͨ����������ü����豸
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

        #region ��ø��ͺż����豸�ļ�����Ŀ
        /// <summary>
        /// ��ø��ͺż����豸�ļ�����Ŀ
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

        #region ���������ͺŻ�ȡ�豸�ļ�����Ŀ
        public long m_lngGetCheckItemByModelID(string p_strModelID, out clsLisDeviceCheckItem_VO[] p_objResultArr)
        {
            return proxy.Service.m_lngGetCheckItemByModelID(p_strModelID, out p_objResultArr);

        }
        #endregion

        #region ����豸������Ŀ
        /// <summary>
        /// ����豸������Ŀ
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

        #region �޸��豸������Ŀ
        /// <summary>
        /// �޸��豸������Ŀ
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

        #region ɾ���豸������Ŀ
        /// <summary>
        /// ɾ���豸������Ŀ
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

        #region ��ӻ��޸������������� 2011-12-5
        /// <summary>
        /// ��ӻ��޸�������������
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

        #region ��ȡ������������������Ϣ 2011-12-5 shichun.chen
        /// <summary>
        /// ��ȡ������������������Ϣ
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

        #region ɾ�������������� 2011-12-5
        /// <summary>
        /// ɾ��������������
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
