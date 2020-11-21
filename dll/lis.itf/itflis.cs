using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using weCare.Core.Entity;

namespace Lis.Itf
{
    [ServiceContract]
    public interface ItfLis : weCare.Core.Itf.IWcf
    {       
        #region clsLIS_Svc

        /// <summary>
        /// 获取指定检验编号的检验项目通道号，并设置检验编号
        /// </summary>
        /// <param name="p_strCheckSampleNO">仪器检验编号</param>
        /// <param name="p_strSampleID">样本ID</param>
        /// <param name="p_strDeviceID">仪器ID</param>
        /// <param name="p_strDeviceNO">仪器代号</param>
        /// <param name="p_strCheckItemstring"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetSampleCheckItems2")]
        long m_lngGetSampleCheckItems(string p_strCheckSampleNO, string p_strSampleID, string p_strDeviceID, string p_strDeviceNO, out string p_strCheckItemstring);

        [OperationContract(Name = "lngAddLabResult1")]
        long lngAddLabResult(List<clsLIS_Device_Test_ResultVO> arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut);

        [OperationContract(Name = "lngAddLabResult2")]
        long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        /// <summary>
        /// 增加检验仪器结果, 多样本
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample"> TRUE = 多样本</param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "lngAddLabResult3")]
        long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        /// <summary>
        /// 增加检验仪器结果 + 图片
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="bytGraph"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "lngAddLabResultWithBytGraph")]
        long lngAddLabResultWithBytGraph(clsLIS_Device_Test_ResultVO[] p_objResultArr, Byte[] bytGraph, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        [OperationContract]
        int m_mthGetNewResultIndex(int p_intRowNum, bool p_blnNext);

        // m_blnIsAppendResult
        [OperationContract]
        bool m_blnIsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList);

        /// <summary>
        /// 获取多个序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSeqName">序列名</param>
        /// <param name="p_intNumber">数量</param>
        /// <param name="p_lngSeqIdArr">序列号</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSequenceArr(string p_strSeqName, int p_intNumber, out int[] p_intSeqIdArr);

        /// <summary>
        /// 医嘱执行后修改收费状态
        /// </summary>
        /// <param name="p_objStrArr">医嘱ID</param>
        /// <param name="p_intPatientType">病人类别1是住院，2是门诊</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngUpdateStatus(string[] p_objStrArr, int p_intPatientType);

        /// <summary>
        /// 与体检的接口
        /// </summary>
        /// <param name="patVo">体检号</param>
        /// <param name="dtPe">体检组合</param>
        /// <returns></returns>
        [OperationContract]
        bool PEItf(clsLisApplMainVO patVo, DataTable dtPe, out List<clsLisApplMainVO> lstApp);

        /// <summary>
        /// 打包-校验是否已打包
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool SamplePackIsExist(string barCode);

        /// <summary>
        /// 打包-插入
        /// </summary>
        /// <param name="lstSamplePack"></param>
        /// <param name="lstSamplePackDet"></param>
        /// <param name="packId"></param>
        /// <returns></returns>
        [OperationContract]
        int SamplePackInsert(List<EntitySamplePack> lstSamplePack, List<EntitySamplePackDetail> lstSamplePackDet, int bizType, out decimal packId);

        /// <summary>
        /// 打包-删除
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <returns></returns>
        [OperationContract]
        int SamplePackDel(List<string> lstBarCode);

        /// <summary>
        /// 打包-查询临时包
        /// </summary>
        /// <param name="floorNo"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool SamplePackQueryTemp(decimal floorNo, out string barCode);

        /// <summary>
        /// 打包-查询
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="bizType">1 体检打包；2 住院打包；3 体检核收；4 住院核收 </param>
        /// <returns></returns>
        [OperationContract]
        List<EntitySamplePack> SamplePackQuery(string barCode, int bizType);

        /// <summary>
        /// 打包-核收
        /// </summary>
        /// <param name="sampleVo"></param>
        /// <returns></returns>
        [OperationContract]
        bool SamplePackCheck(EntitySamplePack sampleVo);

        /// <summary>
        /// 住院检验项目查询
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetIpSample(string barCode);

        /// <summary>
        /// 病区报告查询
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ipNo"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable QueryAreaReport(string deptId, string startDate, string endDate, string ipNo);

        /// <summary>
        /// 通过条码找申请单号
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        string GetApplicationIdByBarcode(string barCode);

        /// <summary>
        /// 打包-获取体检申请信息
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetPeSample(string barCode);

        /// <summary>
        /// 微信检查是否绑卡
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsWechatBanding(string cardNo);

        /// <summary>
        /// 通过申请单号找标本信息(微信);
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetWechatSampleInfo(string applicationId);

        /// <summary>
        /// 获取标本拒收原因
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetRejectReason();

        /// <summary>
        /// 通过处方号返回诊疗卡号
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetCardNoByRecipeId(string recipeId);

        /// <summary>
        /// 获取医嘱字典.申请单元.采样次数
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="applyUnitId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOrderDicSamplingTimes(string orderId, string applyUnitId);

        /// <summary>
        /// 获取申请单检验人、审核人
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetApplicationOperInfo(string applicationId);

        /// <summary>
        /// 查询检验项目ID历史值
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="itemIdArr"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCheckItemHistoryValue(string applicationId, string itemIdArr);

        /// <summary>
        /// 查询:医嘱->诊疗项目-检验申请单元(一对多,如:糖耐量);
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOrderDicLisApplyUnitByOrderId(string orderId);

        /// <summary>
        /// 查询:诊疗项目-检验申请单元(一对多,如:糖耐量);
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOrderDicLisApplyUnit(string orderDicId);

        /// <summary>
        /// GetInstrumentSerialSetting
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        [OperationContract(Name = "lngGetInstrumentSerialSetting01")]
        long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_ConfigVO[] objConfig_List);

        [OperationContract]
        long SaveAllergenRec(List<string> lstFileFullName);

        [OperationContract]
        bool AllergenIsRead(string fileFullName);

        [OperationContract]
        List<string> GetAppUnitIdByAppId(string applicationId);

        [OperationContract]
        DataTable GetAu680ItemByBarCode(string barCode);

        #endregion

        #region clsLisBarcodeSortQuerySvc

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="p_strPatientCardID">诊疗卡号</param>
        /// <param name="p_objPatientInfoVO">返回病人信息VO</param>
        /// <returns>>0成功</returns>
        [OperationContract(Name = "m_lngQueryPatientInfo01")]
        long m_lngQueryPatientInfo(string p_strPatientCardID, out clsPatientBaseInfo_VO p_objPatientInfoVO);

        /// <summary>
        /// 获取病人检验内容
        /// </summary>
        /// <param name="p_strPatientCardID">诊疗卡号</param>
        /// <param name="p_strCheckContent">返回检验内容</param>
        /// <param name="p_objApplMainArr">返回申请单信息</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQueryPatientCheckContent(string p_strPatientCardID, string p_strFromDate, string p_strToDate, out string p_strCheckContent, out clsLisApplMainVO[] p_objApplMainArr);

        #endregion

        #region clsLisDeviceSvc

        // 添加仪器检验项目 
        [OperationContract]
        long m_lngAddNewDeviceItem(clsLisDeviceCheckItem_VO p_objRecord);

        // 修改仪器检验项目  
        [OperationContract]
        long m_lngModifyDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord);

        // 删除仪器检验项目  
        [OperationContract]
        long m_lngDelDeviceCheckItem(clsLisDeviceCheckItem_VO p_objRecord);

        // 根据 DeviceModelID, DeviceCheckItemName 得到对应的 CheckItem
        [OperationContract]
        long m_lngGetCheckItemByDeviceCheckItem(string p_strDeviceID, string p_strDeviceItemName, out clsCheckItem_VO[] p_objCheckItemVOArr);

        /// <summary>
        /// 得到所有的仪器项目及对应的检验项目信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeviceCheckItemInfo(out DataTable p_dtbResult);

        // 根据仪器检验项目ID查询对应的仪器检验项目与检验项目的关系 
        [OperationContract(Name = "m_lngGetCheckItemDeviceCheckItem2")]
        long m_lngGetCheckItemDeviceCheckItem(string p_strDeviceCheckItemID,
            string p_strDeviceModelID, out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem);

        // 查询所有的仪器检验项目与检验项目的对应关系  
        [OperationContract(Name = "m_lngGetCheckItemDeviceCheckItem1")]
        long m_lngGetCheckItemDeviceCheckItem(out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem);

        // 删除仪器设备  
        [OperationContract]
        long m_lngDelDevice(string p_strDeviceID);

        // 修改仪器信息  
        [OperationContract]
        long m_lngModifyDevice(ref clsLisDevice_VO p_objLisDeviceVO);

        // 添加仪器信息  
        [OperationContract]
        long m_lngAddDevice(ref clsLisDevice_VO p_objLisDeviceVO);

        // 获得所有的仪器列表信息  
        [OperationContract]
        long m_lngGetAllDevice(out clsLisDevice_VO[] p_objLisDeviceListVO);

        // 删除仪器串口通讯信息 
        [OperationContract]
        long m_lngDelDeviceSerial(string strDeviceModelID);

        // 修改仪器串口通讯信息  
        [OperationContract]
        long m_lngModifyDeviceSerial(ref clsLisDeviceSerialSetUp_VO objDeviceSerial);

        // 添加仪器串口通讯信息 
        [OperationContract]
        long m_lngAddDeviceSerial(ref clsLisDeviceSerialSetUp_VO objDeviceSerialVO);

        // 查询所有的仪器串口通讯参数列表  
        [OperationContract]
        long m_lngGetAllDeviceSerial(out clsLisDeviceSerialSetUp_VO[] objDeviceSerialVOList);

        // 根据仪器类别获取仪器型号 
        [OperationContract]
        long m_lngGetDeviceModelArrByDeviceCategoryID(string p_strDeviceCategoryID, out clsLisDeviceModel_VO[] p_objResultArr);

        // 删除仪器型号信息  
        [OperationContract]
        long m_lngDelDeviceModel(string strDeviceModelID);

        // 修改仪器型号信息  
        [OperationContract]
        long m_lngModifyDeviceModel(ref clsLisDeviceModel_VO objDeviceModelVO);

        // 添加仪器型号信息  
        [OperationContract]
        long m_lngAddDeviceModel(ref clsLisDeviceModel_VO p_objDevcieModelVO);

        // 根据check_item_id查询对应的Device_check_item的相关信息 
        [OperationContract]
        long m_lngGetDeviceCheckItemInfoByCheckItemID(string p_strCheckItemID, out clsLisDeviceCheckItem_VO objLisDeviceCheckItemVO);

        // 获得所有的仪器型号列表  
        [OperationContract(Name = "m_lngGetAllDeviceModel1")]
        long m_lngGetAllDeviceModel(out DataTable dtbDeviceModel);

        // 获得所有的仪器型号类别VO 
        [OperationContract(Name = "m_lngGetAllDeviceModel2")]
        long m_lngGetAllDeviceModel(out clsLisDeviceModel_VO[] objLisDeviceModelVOList);

        // 根据仪器型号ID查找所有可用的具体仪器
        [OperationContract]
        long m_lngGetDeviceByDeviceModelID(string[] p_strDeviceModelIDArr, out System.Data.DataTable p_dtbDevice);

        // 根据检验项目分类ID获得检验项目
        /// <summary>
        /// 根据检验项目分类ID获得检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetLisDeviceByDeviceID(string p_strDeviceID, out DataTable p_dtbResult);

        /// <summary>
        /// 获得检验设备的种类列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetLisDeviceCategory(out clsLisDeviceCategory_VO[] p_objResultArr);

        /// <summary>
        /// 获得检验设备
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetListDevice(string p_strDeviceCategoryID, out clsLisDevice_VO[] p_objResultArr);

        // 根据仪器型号获取设备检验项目
        [OperationContract(Name = "m_lngGetCheckItemByModelID1")]
        long m_lngGetCheckItemByModelID(string p_strDeviceModelID, out clsLisDeviceCheckItem_VO[] p_objResultArr);

        /// <summary>
        /// 获得该型号检验设备的检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModelID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetCheckItemByModelID2")]
        long m_lngGetCheckItemByModelID(string p_strModelID, out clsCheckItemAndDeviceItem_VO[] p_objResultArr);

        /// <summary>
        /// 添加设备检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModelID"></param>
        /// <param name="p_intGraphFlag"></param>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDoAddNewDeviceItem(string p_strModelID, int p_intGraphFlag, clsCheckItemAndDeviceItem_VO p_objItem);

        /// <summary>
        /// 修改设备检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModelID"></param>
        /// <param name="p_intGraphFlag"></param>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDoModifyDeviceItem(string p_strModelID, int p_intGraphFlag, clsCheckItemAndDeviceItem_VO p_objItem);

        /// <summary>
        /// 获得最大的DeviceCheckItemID
        /// </summary>
        /// <param name="p_strModelID"></param>
        /// <param name="p_strItemID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeviceCheckItemID(string p_strModelID, out string p_strItemID);

        /// <summary>
        /// 删除设备检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModelID"></param>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDeleteDeviceCheckItem(string p_strModelID, clsCheckItemAndDeviceItem_VO p_objItem);

        /// <summary>
        /// 添加或修改特殊仪器参数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objEquipVo"></param>
        /// <param name="p_blnAdd"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddSpecialDevice(clsLIS_Equip_DB_ConfigVO p_objEquipVo, bool p_blnAdd);

        /// <summary>
        /// 获取所有特殊仪器参数信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objEquipVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQuerySepcialDeviceInfo(out clsLIS_Equip_DB_ConfigVO[] p_objEquipVOArr);

        /// <summary>
        /// 删除特殊仪器参数
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceModelID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDeleteSpecialDevice(string p_strDeviceModelID);

        #endregion

        #region clsLisInerfaceSvc

        /// <summary>
        /// 根据检验申请单Id获取收费项目名称
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="chargeItemName"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetChargeItemName(string applicationId, out string chargeItemName);

        /// <summary>
        /// 根据检验申请单Id获取诊疗项目名称
        /// </summary>
        /// <param name="applicationId">检验申请单Id</param>
        ///<param name="orderDicItemName">诊疗项目名称</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetOrderDicItemName(string applicationId, out string orderDicItemName);

        #endregion

        #region clsLisMainSvc

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="setResult">设置结果</param>
        /// <param name="setId">设置的Id</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSystemSetting(out int setResult, string setId);

        #endregion

        #region clsMBY2010Svc

        /// <summary>
        /// 插入报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="instrumentFlag">0-酶标仪2010 1-Multiskan_Ascent</param>
        /// <param name="objResultArr"></param>
        /// <param name="datReportDate"></param>
        /// <returns></returns>
        [OperationContract]
        long lngInsertReport(int instrumentFlag, System.Collections.Generic.List<clsMBY2010VO> objResultArr, DateTime datReportDate);

        #endregion

        #region clsPublicSvc

        /// <summary>
        /// 获取科室部门信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeptMentInfo(out DataTable p_dtResult);

        #endregion

        #region clsQueryAdvis2120Svc

        /// <summary>
        /// 查询出所有的检验类别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetCheckCategory(out clsCheckCategory_VO[] p_objResultArr);

        /// <summary>
        /// 查询出该类别的检验项目
        /// </summary>
        [OperationContract]
        long m_lngGetCheckItemByCategoryID(string p_strCategoryID, out clsCheckItem_VO[] p_objResultArr);

        /// <summary>
        /// 查询所有的仪器检验项目与检验项目的对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckItemDeviceCheckItem"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetCheckItemDeviceCheckItem3")]
        long m_lngGetCheckItemDeviceCheckItem(string p_strDeviceModelID, out clsLisCheckItemDeviceCheckItem_VO[] p_objCheckItemDeviceCheckItem);

        /// <summary>
        /// 构造血球仪Advia2120VO
        /// </summary>
        /// <param name="p_intNum">报告人数</param>
        /// <param name="p_objResultList"></param>
        /// <param name="p_objCheckResultVO"></param>
        [OperationContract]
        void m_mthContructResultVO(int p_intNum, List<clsAdvia2120ResultInf_VO> p_objResultList, out List<clsCheckResult_VO> p_objCheckResultVO, out List<clsT_OPR_LIS_APP_REPORT_VO> p_objRecordVOList, out List<string> p_strSampleIDArr, ref string p_strOriginDate);

        #endregion

        #region clsQueryApplicationSvc

        //  根据员工工号查询员工信息
        [OperationContract]
        long m_lngFindEmpMsgByEmpNO(string p_strEmpNO, out string strEmpID, out string strEmpPwd);

        /// <summary>
        /// 联合查询（包括病人住院号）申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSchVO"></param>
        /// <param name="p_strInHospitalNO">承载联合查询条件的VO,其中值为NULL的成员会被必忽略</param>
        /// <param name="p_objAppVOArr">返回的承载申请单信息的数组</param>
        /// <returns>0:失败;1:成功</returns>
        [OperationContract]
        long m_lngGetAppInfoByConditionAndInHospitalNO(clsLISApplicationSchVO p_objSchVO, string p_strInHospitalNO, out clsLisApplMainVO[] p_objAppVOArr);

        /// <summary>
        /// 根据申请单取得对应样本的采样说明
        /// </summary>
        /// <param name="p_Principal"></param>
        /// <param name="p_ApplicationID"></param>
        /// <param name="p_objSampleGroupVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSampleRemark(string p_ApplicationID, out clsSampleGroup_VO[] p_objSampleGroupVOArr);

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="blIS"></param>
        /// <param name="strsetid"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetCollocate1")]
        long m_lngGetCollocate(out bool blIS, string strsetid);

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="blIS"></param>
        /// <param name="strsetid"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetCollocate2")]
        long m_lngGetCollocate(out string strFlag, string strsetid);

        /// <summary>
        /// 根据体检号和体检组合项目ID查询PIS申请报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strExamineID"></param>
        /// <param name="p_strItemGroupID"></param>
        /// <param name="p_objApplyReportArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngFindApplyReportArrByExamineIDItemGroupID(string p_strExamineID, string p_strItemGroupID, out clsLisApplyReportInfo_VO[] p_objApplyReportArr);

        /// <summary>
        /// 查询根据体检号PIS申请报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strExamineID"></param>
        /// <param name="p_objApplyReportArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngFindApplyReportArrByExamineID(string p_strExamineID, out clsLisApplyReportInfo_VO[] p_objApplyReportArr);

        /// <summary>
        /// 根据病人住院号和住院日期查询,出院日期 查询得到 检验结果信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientNO"></param>
        /// <param name="p_strInHospitalDate"></param>
        /// <param name="p_strOutHospitalDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetResultInfo(string p_strInPatientNO, string p_strInHospitalDate, string p_strOutHospitalDate, out clsLISPatientCheckResultInfoVO[] p_objResultArr);

        /// <summary>
        /// 根据申请单ID查询打印申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetApplicationReportInfo(string p_strApplicationID, out clsLisApplyReportInfo_VO p_objResult);

        /// <summary>
        /// 根据申请单ID查询得到申请单详细信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strOringinDate"></param>
        /// <param name="p_objLISInfoVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetLISInfoByApplicationID(string p_strApplicationID, string p_strOringinDate, out clsLISInfoVO p_objLISInfoVO);

        // 根据条件查询病人信息
        [OperationContract]
        long m_lngGetPatientInfoByCondition(string p_strPatientInHospitalNO, out DataTable p_dtbResult);

        /// <summary>
        /// 组合查询查询已发送申请单及样本信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSampleStatus">1为未采集,2为已采集,0为所有</param>
        /// <param name="p_strAppDept"></param>
        /// <param name="p_strFromDatApp"></param>
        /// <param name="p_strToDatApp"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAppAndSampleInfo1")]
        long m_lngGetAppAndSampleInfo(int p_intSampleStatus, string p_strAppDept,
                                             string p_strFromDatApp, string p_strToDatApp, string p_strPatientName,
                                             string p_strPatiendCardID, string p_strAcceptStatus, int p_intSampleBackeStatus, out clsLisApplMainVO[] p_objAppVOArr);

        //［住院采集样本］组合查询查询已发送申请单及样本信息
        [OperationContract(Name = "m_lngGetAppAndSampleInfo2")]
        long m_lngGetAppAndSampleInfo(int sampleStatus, string areaId,
                                             string beginDate, string endDate, string patientName,
                                             string patientCardId, string hosipitalNo, string bedNo, out clsLisApplMainVO[] p_objAppVOArr);

        // 根据申请日期、发送状态组合查询申请单信息
        [OperationContract(Name = "m_lngGetApplicationVOArrByCondition1")]
        long m_lngGetApplicationVOArrByCondition(string p_strFromDat, string p_strToDat, bool p_blnSend, bool p_blnUnSend, out clsLisApplMainVO[] p_objResultArr);

        // 根据检验日期、标本号、仪器和病人姓名组合查询查询申请单信息
        [OperationContract(Name = "m_lngGetApplicationVOArrByCondition2")]
        long m_lngGetApplicationVOArrByCondition(string p_strFromDat, string p_strToDat, string p_strDeviceID, string p_strPatientName, string p_strSampleID, out clsLisApplMainVO[] p_objResultArr);

        // 根据申请单ID查询申请单信息
        [OperationContract]
        long m_lngGetApplicationInfoByApplicationID(string p_strApplicationID, out clsLisApplMainVO[] p_objResultArr);

        // 根据申请日期查询所有已发送的检验申请单信息
        [OperationContract]
        long m_lngGetAllSendApplInfoByApplDat(string strFromDat, string strToDat, out DataTable dtbAppInfo);

        // 根据申请日期查询所有具有未采集的标本的检验申请信息
        [OperationContract]
        long m_lngGetAllNoCollectSampleByApplDat(string strFromDat, string strToDat, out DataTable dtbAppInfo);

        // 根据申请日期查询所有已采集的检验申请信息
        [OperationContract]
        long m_lngGetAllCollectedApplInfoByApplDat(string strFromDat, string strToDat, out DataTable dtbAppInfo);

        // 根据申请单号查询(已审核=3、未审核<3和全部>0);申请单信息
        [OperationContract]
        long m_lngQryApplInfoByFormNo(string p_strApplFormNo, string strPstatus, out System.Data.DataTable p_dtbAppl);

        // 根据诊疗卡号查询检验申请信息
        [OperationContract]
        long m_lngApplInfoByPatientCardID(string strPatientCardID, out DataTable dtbAppInfo);

        // 根据Application_Form_No查询表t_opr_lis_application_detail所有属于该申请单的大组
        [OperationContract]
        long m_lngGetCheckGroupByApplicationFormNo(string strApplFormNo, out DataTable dtbCheckGroup);

        // 根据申请单号查询检验组信息(含子组);
        [OperationContract]
        long m_lngGetCheckGroupByApplFormNo(string strApplFormNo, out DataTable dtbCheckGroup);

        // 按照某一字段，模糊查询表t_opr_lis_application，也可比较查询,可自定义按那个字段排序，是倒序还是顺序
        [OperationContract]
        long m_lngGetApplByQuery(int p_intQueryType, string p_strField, string p_strCompare, string p_strFieldValue, string p_strOrderByField, bool p_blnDesc, out System.Data.DataTable p_dtbAppl);

        // 根据ApplicationID查询t_opr_lis_application_detail
        [OperationContract]
        long m_lngGetApplDetailByApplID(string p_strApplID, out System.Data.DataTable p_dtbApplDetail);

        // 根据病人的诊疗卡号，查询出所有的未完成的检验申请。这里有一个样本采集的过滤条件。        
        //这里，过滤条件strFilter表示PStatus_int字段的条件。如"=1"，或者">1"，"in (1,2,3);"等。
        [OperationContract]
        long m_lngGetApplicationInfoByPtCard(string strFromDate, string strToDate, string strPtCardId, string strFilter, out System.Data.DataTable dtbAppInfo);

        // 根据申请号，获得所有的检查组。
        [OperationContract]
        long m_lngGetApplicationContent(string strAppId, out string[] strGroupIdArr);

        // 根据申请号，获得所有需要的样本列表。
        //这里假定所有的组都可以在t_lis_aid_group_sample表中找到样本要求。如果在t_lis_aid_group_sample表中
        //只有没有子组的检验项目的样本要求，则要先进行组的分析。相关方法见clsCheckGroupSvc类。
        [OperationContract]
        long m_lngGetApplicationSample(string strAppId, out System.Data.DataTable dtbAppSample);

        // 根据标本上的条码号，查询该标本所对应的检验申请的所有检验项目
        [OperationContract]
        long m_lngGetApplCheckGroupBySampleBarCode(string p_strSampleBarCode, out DataTable p_dtbCheckGroupList);

        // 根据申请单号（指申请单上贴得条码号或者印刷的号码，不是系统内部为每个申请指定的号码）查出对应的检验申请的资料
        [OperationContract(Name = "m_lngGetApplicationInfoByFormNo1")]
        long m_lngGetApplicationInfoByFormNo(string p_strApplFormNo, out clsLisApplMainVO p_objApplMainVO);

        // 根据申请单号（指申请单上贴得条码号或者印刷的号码，不是系统内部为每个申请指定的号码）查出对应的检验申请的资料
        [OperationContract(Name = "m_lngGetApplicationInfoByFormNo2")]
        long m_lngGetApplicationInfoByFormNo(string p_strApplFormNo, out System.Data.DataTable p_dtbAppl);

        // 根据日期范围，查询指定日期范围(按申请日期、采样日期、检验日期、审核日期);之内的全部申请资料
        [OperationContract]
        long m_lngGetApplInfoByDateRange(int p_intDateQueryType, string p_strDateFieldName, System.DateTime p_dtBegin, System.DateTime p_dtEnd, string strPstatus, out System.Data.DataTable p_dtbAppl);

        // 按申请日期范围、PStatus_int状态查询申请单信息
        [OperationContract]
        long m_lngGetApplByApplDateRange(System.DateTime p_dtBegin, System.DateTime p_dtEnd, string p_strStatusFilter, out System.Data.DataTable p_dtbAppl);

        // 根据申请号（系统内部给定的申请唯一号）查询该申请所有检验项目（不含有子组）资料(含SampleID);
        [OperationContract]
        long m_lngGetApplicationCheckInfo(string p_strApplFormID, out System.Data.DataTable p_dtbAppCheckInfo);

        // 根据t_opr_lis_application某一字段和各种日期范围(申请日期、采样日期、检验日期、审核日期);查询所有申请资料
        [OperationContract(Name = "m_lngGetApplInfoByFieldValue1")]
        long m_lngGetApplInfoByFieldValue(string p_strFieldName, string p_strFieldValue, int p_intDateQueryType, string p_strDateFieldName, DateTime p_dtBegin, DateTime p_dtEnd, out System.Data.DataTable p_dtbAppl);

        //  根据GroupID(无子组);和各种日期范围(申请日期、采样日期、检验日期、审核日期);查询所有申请资料
        [OperationContract(Name = "m_lngGetApplInfoByFieldValue2")]
        long m_lngGetApplInfoByFieldValue(string p_strGroupID, int p_intDateQueryType, string p_strDateFieldName, System.DateTime p_dtBegin, System.DateTime p_dtEnd, out System.Data.DataTable p_dtbAppl);

        /// <summary>
        /// 通过住院号，获取病人信息和诊断医生信息
        /// </summary>
        /// <param name="p_pbjprincipal"></param>
        /// <param name="p_InHospitalID"></param>
        /// <param name="objPatientVO"></param>
        /// <param name="objBihRegisterVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngFindPatientInfoByInpatientID(string p_InHospitalID, out DataTable p_dvbResult);

        #endregion

        #region clsQueryApplyPropertySvc

        // 查询所有申请单元属性资料
        [OperationContract]
        long m_lngGetAllUnitPropertyAndDetail(out clsUnitProperty_VO[] p_objPropertyArr, out clsUnitPropertyValue_VO[] p_objValueArr);

        // 根据申请单元ID查询得到一组使用中的 clsUnitPropertyRelate_VO
        [OperationContract]
        long m_lngGetRelatesByUnitID(string p_strApplyUnitID, out clsUnitPropertyRelate_VO[] p_objVOArr);

        /// <summary>
        /// 返回所有的使用的属性id
        /// </summary>
        /// <param name="p_objPrincipal">是否有权限</param>
        /// <param name="p_objResultArr">结果集合</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        long m_lngGetAllPropertyId(out string[] p_strResultArr);

        /// <summary>
        /// 返回满足条件的属性值集合
        /// </summary>
        /// <param name="p_objPrincipal">是否有权限</param>
        /// <param name="p_strPropertyId">属性id</param>
        /// <param name="p_strApplyUnitId">申请单元id</param>
        /// <param name="p_arlResult">返回结果集合</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        long m_lngGetPropertyValue(string p_strPropertyId, string p_strApplyUnitId, out List<string> p_arlResult);

        #endregion

        #region clsQueryApplyUnitSvc

        // 根据一组申请单元ID得到其所包含的样本ID列表
        [OperationContract]
        long m_lngGetSampleTypeIDList(string[] p_strApplyUnitIDArr, out string[] p_strSampleTypeIDArr);

        // 根据检验类别获取申请单元
        [OperationContract]
        long m_lngGetApplUnitByCheckCategory(string p_strCheckCategory, out clsApplUnit_VO[] p_objResultArr);

        // 根据申请单元ID得到申请单元VO
        [OperationContract]
        long m_lngGetApplyUnitVOByApplyUnitID(string p_strApplyUnitID, out clsApplUnit_VO p_objApplUnit);

        // 获取所有的申请单元组合 
        [OperationContract]
        long m_lngGetAllApplUnit(out clsApplUnit_VO[] objApplUnitVOList);

        // 获取所有的申请单元明细
        [OperationContract]
        long m_lngGetAllApplUnitDetail(out clsApplUnitDetail_VO[] objApplUnitDetailVOList);

        /// <summary>
        /// 获取申请单元排序
        /// </summary>
        /// <param name="p_strAppUnitArr"></param>
        /// <param name="p_strOutAppUnitArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQueryAppUnitSeq(string[] p_strAppUnitArr, out string[] p_strOutAppUnitArr);

        #endregion

        #region clsQueryCheckGroupSvc

        // 获取所有的用户自定义申请组
        [OperationContract]
        long m_lngGetAllUserGroup(out clsApplUserGroup_VO[] objApplUserGroupVOList);

        // 获取所有的一级用户自定义申请组
        [OperationContract]
        long m_lngGetMasterUserGroup(out clsApplUserGroup_VO[] objApplUserGroupVOList);

        // 获取所有的报告组明细
        [OperationContract]
        long m_lngGetAllReportGroupDetail(out clsReportGroupDetail_VO[] objReportGroupVOList);

        // 获取某一报告组下的标本组明细 (go);
        [OperationContract]
        long m_lngGetReportGroupDetail(string strReportGroupID, ref clsSampleGroup_VO[] objSampleGroupList);

        #endregion

        #region clsQuerySampleGroupSvc

        // 获取所有的标本组明细
        [OperationContract(Name = "m_lngGetAllSampleGroupDetail1")]
        long m_lngGetAllSampleGroupDetail(out clsSampleGroupDetail_VO[] objSampleGroupDetailVOList);

        #endregion

        #region clsQueryCheckGroupSvc

        // 分析一个Group下面有多少个子组（一直分解到所有子组都没有子组）。返回为结果集。
        [OperationContract]
        long m_lngQryBseGroup(string strGroupId, out clsCheckGroup_VO[] objCheckGroupVOList);

        // 根据Has_SubGroup查询相应的检验组信息
        [OperationContract]
        long m_lngGetCheckGroupByCheckGroupType(string strHasSubGroup, out clsCheckGroup_VO[] objCheckGroup);

        // 根据GroupID查询该检验组需要的样本信息
        [OperationContract]
        long m_lngGetSampleTypeInfo(string strGroupID, out DataTable dtbSampleType);

        // 根据GroupID查询属于该检验组的第一层子组信息
        [OperationContract]
        long m_lngGetSubGroupByGroupID(string strGroupID, out clsCheckGroup_VO[] objCheckGroupVO);

        // 查询所有的检验项目(包含有子组和无子组的);
        [OperationContract]
        long m_lngGetAllCheckGroup(out clsCheckGroup_VO[] p_objGroupVOList);

        // 按照某一字段，模糊查询组合信息，可自定义按那个字段排序，是倒序还是顺序
        [OperationContract]
        void m_mthGetGroupInfo(int intQueryType, string p_strFussField, string p_strFussValue, string p_strOrderByField, bool p_blnDesc, out clsCheckGroup_VO[] p_objGroupVOList);

        // 分析一个Group下面有多少个子组（一直分解到所有子组都没有子组）。返回为结果集。
        [OperationContract]
        long m_lngAnalysisGroup(string strGroupId, out System.Data.DataTable dtbGroupInfo);

        // 获得某一个Group需要的样本。这里输入的组号要求不含有子组。
        //这里的假设是，用户存放在t_aid_lis_group_sample表中的组，全部是没有子组的项目。所以需要上面的方
        //法进行分析。如果在t_aid_lis_group_sample表中存放了所有的组的样本清况，则不需要作上面的分析，这里传入的
        //参数可以是任何组号。实际情况如何，要根据具体情况决定。
        [OperationContract]
        long m_lngGetGroupSample(string strGroupId, out System.Data.DataTable dtbSampleInfo);

        // 根据标本类别，查询该类标本可能进行的检验项目（即检验组）,查询到细项（没有子组为止）
        [OperationContract]
        long m_lngGetCheckGroupBySampleType(string p_strSampleTypeId, out System.Data.DataTable p_objDT_CheckGroup);

        // 根据一个Group的Id，查询该Group包含的结果项（checkItem）
        [OperationContract]
        long m_lngGetCheckItemByGroupId(string p_strGroupId, out System.Data.DataTable p_objDT_CheckItem);

        //  根据GroupID获得组合信息（包括有子组和无子组的Group）
        [OperationContract]
        long m_lngGetGroupInfoByGroupID(string p_strGroupID, out System.Data.DataTable p_dtbGroup);

        // 根据ApplID查询申请单的检验项目信息
        [OperationContract(Name = "m_lngGetGroupInfoByApplID1")]
        long m_lngGetGroupInfoByApplID(string p_strApplID, out System.Data.DataTable p_dtbGroup);

        // 根据ApplID查询申请单的检验项目信息
        [OperationContract(Name = "m_lngGetGroupInfoByApplID2")]
        long m_lngGetGroupInfoByApplID(string p_strApplID, out clsCheckGroup_VO[] p_objCheckGroupVOList);

        #endregion

        #region clsQueryCheckItemSvc

        // 获取申请单元数组包含的检验项目列表
        [OperationContract]
        long m_lngGetApplyUnitArrCheckItem(string[] p_strApplyUnitArr, out clsPISApplyUnitItem[] p_objRecordArr);

        // 根据检验类别、样本类别查询模板信息
        [OperationContract]
        long m_lngGetTemplateInfoByCondition(string p_strCheckCategory, string p_strSampleType, out clsLisValueTemplate_VO[] p_objResultArr);

        // 根据模板ID查询相应的模板明细信息
        [OperationContract]
        long m_lngGetTemplateDetailByTemplateID(string p_strTemplateID, out clsLisValueTemplateDetail_VO[] p_objResultArr);

        // 根据检验项目ID查询相应的模板明细信息
        [OperationContract]
        long m_lngGetTemplateDetailByCheckItemID(string p_strCheckItemID, out clsLisValueTemplateDetail_VO[] p_objResultArr);

        // 根据检验项目ID查询表T_AID_LIS_VALUETEMPLATE_ITEM的记录
        [OperationContract]
        long m_lngGetValueTemplateItemByCheckItemID(string p_strCheckItemID, out clsLisValueTemplateItem_VO p_objResult);

        // 根据模板ID查询相应的模板信息
        [OperationContract]
        long m_lngGetValueTemplateByTemplateID(string p_strTemplateID, out clsLisValueTemplate_VO p_objResult);

        // 根据检验项目ID查询模板的所有信息
        [OperationContract]
        long m_lngGetAllTemplateInfoByCheckItemID(string p_strCheckItemID,
            out clsLisValueTemplateItem_VO p_objTemplateItem, out clsLisValueTemplate_VO p_objTemplate,
            out clsLisValueTemplateDetail_VO[] p_objTemplateDetailArr);

        // 根据检验类别获取检验项目
        [OperationContract]
        long m_lngGetCheckItemArrByCheckCategory(string p_strCheckCategory, out DataTable p_dtbResultArr);

        // 根据检验类别和样品类别,样本组,查询所有的检验项目
        [OperationContract(Name = "m_lngQryCheckItemByCheckCategoryAndSampleType1")]
        long m_lngQryCheckItemByCheckCategoryAndSampleType(string p_strCheckCategory, string p_strSampleType, string p_strSampleGroup, out DataTable p_dtbCheckItem);

        // 根据检验类别和样本类别组合查询相关的检验项目信息
        [OperationContract]
        long m_lngGetCheckItemArrByCondition(string p_strCheckCategoryID, string p_strSampleTypeID, out clsCheckItem_VO[] p_objResultArr);

        /// <summary>
        /// 根据check_item_id查询对应的检验项目信息VO
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetCheckItemVOByCheckItemID(string p_strCheckItemID, out clsCheckItem_VO p_objCheckItemVO);

        // 根据check_item_id、年龄、性别和月经周期查询符合条件的参考值范围
        [OperationContract]
        long m_lngGetCheckItemRefByCondition(string p_strAge, string p_strSex, string p_strMenses, string p_strCheckItemID, out clsCheckItemRef_VO objCheckItemRefVO);

        // 根据check_item_id查询对应的检验项目信息
        [OperationContract(Name = "m_lngGetCheckItemInfoByCheckItemID1")]
        long m_lngGetCheckItemInfoByCheckItemID(string p_strCheckItemID, out DataTable dtbCheckItem);

        // 根据申请单元ID查询所有的检验项目
        [OperationContract(Name = "m_lngGetCheckItemByApplUnitID1")]
        long m_lngGetCheckItemByApplUnitID(string strApplUnitID, out DataTable dtbCheckItem);

        // 根据申请单元ID查询所有的检验项目 VO
        [OperationContract(Name = "m_lngGetCheckItemByApplUnitID2")]
        long m_lngGetCheckItemByApplUnitID(string p_strApplUnitID, out clsCheckItem_VO[] p_objCheckItemVOArr);

        // 根据标本组ID查询所有的检验项目
        [OperationContract(Name = "m_lngGetCheckItemBySampleGroupID1")]
        long m_lngGetCheckItemBySampleGroupID(string strSampleGroupID, out clsCheckItem_VO[] objCheckItemVOList);

        // 根据标本组ID查询所有的检验项目
        [OperationContract(Name = "m_lngGetCheckItemBySampleGroupID2")]
        long m_lngGetCheckItemBySampleGroupID(string strSampleGroupID, out DataTable dtbCheckItem);

        // 构造CheckItemVO
        [OperationContract]
        void ConstructCheckItemVO(DataTable dtRow, ref clsCheckItem_VO objCheckItemVO);

        // 根据检验类别和样品类别查询所有的检验项目
        [OperationContract(Name = "m_lngQryCheckItemByCheckCategoryAndSampleType2")]
        long m_lngQryCheckItemByCheckCategoryAndSampleType(string strCheckCategory, string strSampleType, out DataTable dtbCheckItem);

        // 查询所有的打印类别信息
        [OperationContract]
        long m_lngGetAllPrintCategory(out DataTable dtbPrintCategory);

        // 查询出所有的检验类别
        [OperationContract]
        long m_lngGetAllCheckCategory(out System.Data.DataTable dtbCheckCategory);

        // 查询出所有属于某一检验类别的检验项目
        [OperationContract]
        long m_lngGetAllCheckItemByCheckCategory(string strCheckCategoryID, out System.Data.DataTable dtbAllCheckItem);

        /// <summary>
        /// 根据一组 Check_Item_ID 查询出每个Check_Item_ID 的详细资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemIDArr">
        /// 如为空则返回-1且dtbCheckItemsInfo为空
        /// 如长度为0则查出所有的CheckItem的资料</param>
        /// <param name="dtbCheckItemsInfo"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetCheckItemInfoByCheckItemID2")]
        long m_lngGetCheckItemInfoByCheckItemID(string[] p_strCheckItemIDArr, out System.Data.DataTable dtbCheckItemsInfo);

        // 查询某一检验组(无子组);包含的检验项目
        [OperationContract(Name = "m_lngGetCheckItemByNoSubCheckGroupID1")]
        long m_lngGetCheckItemByNoSubCheckGroupID(string strCheckGroupID, out DataTable dtbCheckGroupItem);

        // 查询某一检验组(无子组);包含的检验项目
        [OperationContract(Name = "m_lngGetCheckItemByNoSubCheckGroupID2")]
        long m_lngGetCheckItemByNoSubCheckGroupID(string strCheckGroupID, out clsCheckItem_VO[] p_objResultArr);

        // 查询某一检验组(含子组);包含的检验项目
        [OperationContract]
        long m_lngGetCheckItemByhasSubCheckGroupID(string strCheckGroupID, out DataTable dtbCheckGroupItem);

        // 查询出所有的检验项目
        [OperationContract(Name = "m_lngGetAllCheckItem01")]
        long m_lngGetAllCheckItem(out System.Data.DataTable dtbAllCheckItem);

        // 查询出所有的样品类别
        [OperationContract]
        long m_lngGetAllSampleType(out System.Data.DataTable dtbAllSampleType);

        // 根据check_item_id查询所有属于该检验项目的参考值范围(不包含默认参考值);
        [OperationContract]
        long m_lngGetItemRefByCheckItemID(string checkItemID, out System.Data.DataTable dtbItemRef);

        // 根据check_item_id查询该检验项目的默认参考值范围
        [OperationContract]
        long m_lngGetDefaultRefByCheckItemID(string checkItemID, out System.Data.DataTable dtbItemDefaultRef);

        /// <summary>
        /// 获取全部检验项目参考值范围
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetItemRef();

        /// <summary>
        /// 获取检验项目科室限制
        /// </summary>
        /// <param name="checkItemId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCriticalValueRefLisDept(string checkItemId);

        #endregion

    }
}
