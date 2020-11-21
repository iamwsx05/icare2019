using com.digitalwave.iCare.middletier.LIS;
using System.Collections.Generic;

namespace MAGLUMI_4000_plus
{
    /// <summary>
    /// 定义控制代码
    /// </summary>
    public class MAGLUMI_X8_ControlCode
    {
        /// <summary>
        /// 请求应答符
        /// </summary>
        public const string ReqCode = "";

        /// <summary>
        /// 包起始字符
        /// </summary>
        public const string StartCode = "";        

        /// <summary>
        /// 包结束字符
        /// </summary>
        public const string EndCode = "";

        /// <summary>
        /// 发送命令字符
        /// </summary>
        public const string AckCode = "";
                
        /// <summary>
        /// 每项两位
        /// </summary>
        int CheckItems = 16;

        /// <summary>
        /// 获取样本申请指令
        /// </summary>
        /// <param name="p_strRackNO">样本合代号</param>
        /// <param name="p_strCupNO">样本位置</param>
        /// <param name="p_strSampleType">样本类型</param>
        /// <param name="p_strSampleNO">样本号</param>
        /// <param name="p_strSampleID">样本ID</param>
        /// <param name="p_strDeviceNO">仪器代号</param>
        /// <param name="p_strDeviceID">仪器ID</param>
        /// <returns></returns>
        public string[] GetSampleOrderToSendArr(string p_strRackNO, string p_strCupNO, string p_strSampleType, string p_strSampleNO, string p_strSampleID, string p_strDeviceNO, string p_strDeviceID)
        {
            List<string> lstOrder = new List<string>();
            string strCheckItems = GetSampleCheckItems(p_strSampleNO, p_strSampleID, p_strDeviceNO, p_strDeviceID);
            string strOrder = null;
            if (strCheckItems != null && strCheckItems.Length > CheckItems)
            {
                int iItems = 0;
                for (int idx = 0; idx < strCheckItems.Length; idx += CheckItems)
                {
                    if (idx + CheckItems < strCheckItems.Length)
                    {
                        strOrder = StartCode + "S " + p_strRackNO + p_strCupNO + p_strSampleType + p_strSampleNO + p_strSampleID + "    " + iItems.ToString() + strCheckItems.Substring(idx, CheckItems) + EndCode;
                    }
                    else
                    {
                        strOrder = StartCode + "S " + p_strRackNO + p_strCupNO + p_strSampleType + p_strSampleNO + p_strSampleID + "    E" + strCheckItems.Substring(idx, strCheckItems.Length - idx) + EndCode;
                    }
                    lstOrder.Add(strOrder);
                    iItems++;
                }
            }
            else
            {
                strOrder = StartCode + "S " + p_strRackNO + p_strCupNO + p_strSampleType + p_strSampleNO + p_strSampleID + "    E" + strCheckItems + EndCode;
                lstOrder.Add(strOrder);
            }
            return lstOrder.ToArray();
        }

        /// <summary>
        /// 获取样本申请指令
        /// </summary>
        /// <param name="p_strRackNO">样本合代号</param>
        /// <param name="p_strCupNO">样本位置</param>
        /// <param name="p_strSampleType">样本类型</param>
        /// <param name="p_strSampleNO">样本号</param>
        /// <param name="p_strSampleID">样本ID</param>
        /// <param name="p_strDeviceNO">仪器代号</param>
        /// <returns></returns>
        public string GetSampleOrderToSend(string p_strRackNO, string p_strCupNO, string p_strSampleType, string p_strSampleNO, string p_strSampleID, string p_strDeviceNO, string p_strDeviceID)
        {
            string strCheckItems = GetSampleCheckItems(p_strSampleNO, p_strSampleID, p_strDeviceNO, p_strDeviceID);
            string strOrder = StartCode + "S " + p_strRackNO + p_strCupNO + p_strSampleType + p_strSampleNO + p_strSampleID + "    E" + strCheckItems + EndCode;
            return strOrder;

        }
        /// <summary>
        /// 获取指定检验编号的检验项目字符串
        /// </summary>
        /// <param name="p_strSampleNO"></param>
        /// <returns></returns>
        private string GetSampleCheckItems(string p_strSampleNO, string p_strSampleID, string p_strDeviceNO, string p_strDeviceID)
        {
            string strReturn = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strSampleID))
            {
                 lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleCheckItems(p_strDeviceNO + p_strSampleNO, out strReturn);
            }
            else
            {
                 lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetSampleCheckItems(p_strSampleNO, p_strSampleID, p_strDeviceID, p_strDeviceNO, out strReturn);
            }
            return strReturn;
        }
    }
}
