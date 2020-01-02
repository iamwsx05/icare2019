using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Windows.Forms;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// 定义检验数据采集接口
    /// </summary>
    public interface infLISDataAcquisition
    {
        /// <summary>
        /// 设备配置信息
        /// </summary>
        clsLIS_Equip_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// 所属窗体
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// 是否记录日志，true = 是
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="p_frmParent">所属窗体</param>
        /// <param name="p_objDeviceConfigVO">设备配置信息</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// 结束工作
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// 显示仪器结果
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// 显示接口信息
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
    }
        /// <summary>
        /// 定义数据采集接口 ---文本模式 李泳潮2012-01-10
        /// </summary>
    public interface infLISDataAcquisition_TXT
    {
        /// <summary>
        /// 设备配置信息
        /// </summary>
        clsLIS_Equip_DB_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// 所属窗体
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// 是否记录日志，true = 是
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="p_frmParent">所属窗体</param>
        /// <param name="p_objDeviceConfigVO">设备配置信息</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// 各接口根据需要重写此方法
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByAuto();
        /// <summary>
        /// 各接口根据需要重写此方法
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByHandle();
        /// <summary>
        /// 结束工作
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// 显示仪器结果
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// 显示接口信息
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;

    }
    public interface infLISDataAcquisition_DB
    {
        /// <summary>
        /// 设备配置信息
        /// </summary>
        clsLIS_Equip_DB_ConfigVO m_objDeviceConfigVO
        {
            get;
            set;
        }
        /// <summary>
        /// 所属窗体
        /// </summary>
        Form m_frmParent
        {
            get;
            set;
        }
        /// <summary>
        /// 是否记录日志，true = 是
        /// </summary>
        bool m_blnLogger
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <returns></returns>
        long m_lngInitDataAcquisition();
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <returns></returns>
        long m_lngStartWork();
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="p_frmParent">所属窗体</param>
        /// <param name="p_objDeviceConfigVO">设备配置信息</param>
        /// <returns></returns>
        long m_lngStartWork(Form p_frmParent, clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO);
        /// <summary>
        /// 各接口根据需要重写此方法
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByAuto();
        /// <summary>
        /// 各接口根据需要重写此方法
        /// </summary>
        /// <returns></returns>
        long m_lngWorkByHandle();
        /// <summary>
        /// 结束工作
        /// </summary>
        /// <returns></returns>
        long m_lngFinishWork();
        /// <summary>
        /// 显示仪器结果
        /// </summary>
        event DataShowEventHandler evnDataShow;
        /// <summary>
        /// 显示接口信息
        /// </summary>
        event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
    }


    /// <summary>
    /// 申明委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataShowEventHandler(clsDeviceSampleDataKey p_SampleDateKey, clsLIS_Device_Test_ResultVO[] p_objDeviceResultArr);
    /// <summary>
    /// 申明委托，显示接口信息。
    /// </summary>
    /// <param name="p_strInfo"></param>
    public delegate void DataAcquisitionInfoEventHandler(string p_strInfo);

    /// <summary>
    /// 重写toString（）方法，返回仪器样本数据字符串
    /// </summary>
    public class clsDeviceSampleDataKey
    {
        public string strDeviceID;
        public string strDeviceName;
        public string strDeviceSampleID;
        public string strCheckDate;
        public int intResultBeginIndex;
        public int intResultEndIndex;
        public string strCommingDateTime;
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(strDeviceID);
            sb.Append("||");
            sb.Append(strCheckDate);
            sb.Append("||");
            sb.Append(strDeviceSampleID);
            sb.Append("||");
            sb.Append(intResultBeginIndex.ToString());
            sb.Append("||");
            sb.Append(intResultEndIndex.ToString());
            sb.Append("||");
            sb.Append(strCommingDateTime);
            return sb.ToString();
        }

    }
}
