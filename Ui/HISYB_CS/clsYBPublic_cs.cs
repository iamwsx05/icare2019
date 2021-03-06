using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using weCare.Core.Entity;
using Sybase.DataWindow;
using com.digitalwave.Utility;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsYBPublic_cs
    {
        public clsYBPublic_cs()
        {

        }

        #region 接口函数
        /**初始化，在调用DLL前，初始化调用环境变量。整个调用工程只需调用该函数一次即可。
         * LPTSTR svrIP：代理服务器的ＩＰ地址。
         * USHORT svrPort： 代理服务器的监听端口。
         * SndBufSize：socket发送缓存大小。
         * RecvBufSize：socket接收缓存大小。
         * Return:HRESULT 1表示成功；-11表示系统初始化失败。*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "Initialize")]
        public static extern int Initialize(string svrIP, int svrPort, int SndBuffSize, int RecvBuffSize);

        /**创建实例
         * 创建一个功能调用实例。在进行一个新的功能调用前必须执行该操作，以取得调用的处理句柄。返回的句柄将成为其他功能调用的入口参数。
         * Return:HANDLE 大于0的LONG型值表示创建成功；-13表示创建失败。*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "CreateInstace")]
        public static extern int CreateInstace();

        /**设置入参
         * 提供功能调用的参数组，比如功能号以及其他功能的调用参数。（功能号的paramName规定为“FN”）
         * HANDLE pDataHandle：功能调用的处理句柄，由接口函数CreateInstace()创建。
         * LPCTSTR paramName：参数名称。
         * LPCTSTR paramValue：参数值。
         * Return:HRESULT 成功返回1；失败返回-14，详细的错误信息可以通过调用GetSysMessage()取得
         */
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "SetParam")]
        public static extern int SetParam(int pDataHandle, string paramName, string paramValue);

        /**数据集*/
        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "InsertDataSet")]
        public static extern int InsertDataSet(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "InsertRow")]
        public static extern int InsertRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "SetField")]
        public static extern int SetField(int pDataHandle, string fieldName, string fieldValue);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EndRow")]
        public static extern int EndRow(int pDataHandle, int rowID);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EndDataSet")]
        public static extern int EndDataSet(int pDataHandle, string name);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "Run")]
        public static extern int Run(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetParam")]
        public static extern int GetParam(int pDataHandle, string paramName, StringBuilder paramValue, int nMaxValueLenth);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "LocateDataSet")]
        public static extern int LocateDataSet(int pDataHandle, string Name);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetRowSize")]
        public static extern int GetRowSize(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "NextRow")]
        public static extern int NextRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetCurrentRow")]
        public static extern int GetCurrentRow(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetColSize")]
        public static extern int GetColSize(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetFieldValue")]
        public static extern int GetFieldValue(int pDataHandle, string name, StringBuilder value, int nMaxValueLenth);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "DestroyInstance")]
        public static extern int DestroyInstance(int pDataHandle);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "GetSysMessage")]
        public static extern int GetSysMessage(int pDataHandle, StringBuilder pMassage, int nMaxMessage);

        [DllImport("HNBridge.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "EncryptWithCipher")]
        public static extern int EncryptWithCipher(int pDataHandle, string PlainData, StringBuilder EncryptedData, int nMaxValueLenth);
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static long m_lngInitialize()
        {
            string svrIP = m_strReadXML("DGCSMZYB", "svrIPMZ", "AnyOne");
            string svrPort = m_strReadXML("DGCSMZYB", "svrPortMZ", "AnyOne");
            short shPort = 0;
            short.TryParse(svrPort, out shPort);
            string SndBufSize = m_strReadXML("DGCSMZYB", "SndBufSizeMZ", "AnyOne");
            string RecvBufSize = m_strReadXML("DGCSMZYB", "RecvBufSizeMZ", "AnyOne");
            int intSize = 0, intSize2 = 0;
            int.TryParse(SndBufSize, out intSize);
            int.TryParse(RecvBufSize, out intSize2);
            int intPtr = Initialize(svrIP, shPort, intSize, intSize2);
            if (intPtr < 0)
            {
                MessageBox.Show("初始化失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return -1;
            }
            return intPtr;
        }
        #endregion

        #region 医院登录
        /// <summary>
        /// 医院登录
        /// </summary>
        /// <param name="strUser">用户名</param>
        /// <param name="strPwd">口令</param>
        /// <param name="Blxml">判断是否读取xml用户名和密码</param>
        /// <returns></returns>
        public static long m_lngUserLoin(string strUser, string strPwd, bool Blxml)
        {
            //初始化
            m_lngInitialize();
            int intPtr = CreateInstace();
            string strHosCode = strUser;
            if (Blxml)
            {
                strUser = m_strReadXML("DGCSMZYB", "USERNAMEMZ", "AnyOne");//need modify 需要传进来
                strPwd = m_strReadXML("DGCSMZYB", "PASSWORDMZ", "AnyOne");
                strHosCode = m_strReadXML("DGCSMZYB", "YYBHMZ", "AnyOne");
            }
            int intRet;
            StringBuilder sbPwd = new StringBuilder(32);
            if (intPtr > 0)
            {
                intRet = EncryptWithCipher(intPtr, strPwd, sbPwd, 32);
                if (intRet < 0)
                {
                    MessageBox.Show("明文密码加密失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
                string strNewPwd = sbPwd.ToString();
                //医院登录
                intRet = SetParam(intPtr, "FN", "1");
                intRet = SetParam(intPtr, "YYBH", strHosCode);
                intRet = SetParam(intPtr, "USERID", strUser);
                intRet = SetParam(intPtr, "PWD", strNewPwd);
                intRet = SetParam(intPtr, "JBRLX", "1");
                intRet = SetParam(intPtr, "CLIENTTYPE", "HIS");
                intRet = SetParam(intPtr, "JBR", "001");
                intRet = Run(intPtr);
                if (intRet < 0)
                {
                    StringBuilder strRetValue1 = new StringBuilder(32);
                    intRet = GetSysMessage(intPtr, strRetValue1, 66);
                    MessageBox.Show(strRetValue1.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
                StringBuilder strRetValue = new StringBuilder(32);
                StringBuilder strRetMessage = new StringBuilder(1024);
                intRet = GetParam(intPtr, "FHZ", strRetValue, 32);
                intRet = GetParam(intPtr, "MSG", strRetMessage, 1024);
                if (strRetValue.ToString() == "EHIS9700")
                {
                    ExceptionLog.OutPutException("返回值：EHIS9700 \r\n" + "社保系统登录故障，请稍后重新登录.");
                    //m_lngUserPwsRevise(intPtr, strPwd, "123qwe");
                    MessageBox.Show("社保系统登录故障，请稍后重新登录！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
                else if (strRetValue.ToString() != "1")
                {
                    ExceptionLog.OutPutException(strRetMessage.ToString());
                    MessageBox.Show(strRetMessage.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (strRetMessage.ToString().IndexOf("未修改初始密码") > -1)
                    {
                        frmUserPwsRevise frmPwsRevise = new frmUserPwsRevise();// frmUserPwsRevise(intPtr);
                        frmPwsRevise.Show();
                    }
                    else
                    {
                        DestroyInstance(intPtr);
                        return -1;
                    }
                }
            }
            else
            {
                MessageBox.Show("创建实例失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DestroyInstance(intPtr);
                return -1;
            }
            DestroyInstance(intPtr);
            return 1;
        }
        #endregion

        #region 医院口令修改
        /// <summary>
        /// 医院口令修改
        /// </summary>
        /// <param name="intPtr">句柄参数值</param>
        /// <returns></returns>
        public static long m_lngUserPwsRevise(int intPtr, string strUser, string m_strOldPwd, string m_strNewPwd)
        {
            string strNodeName = "DGCSMZYB";//默认读门诊部编号
            string strPwd = m_strReadXML(strNodeName, "PASSWORDMZ", "AnyOne");
            if (strUser == "111014")//住院部编号
            {
                strNodeName = "DGCSZYYB";//住院节点
                strPwd = m_strReadXML(strNodeName, "PASSWORDZY", "AnyOne");
            }
            if (!strPwd.Equals(m_strOldPwd))
            {
                MessageBox.Show("原口令输入不正确，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return -1;
            }
            //初始化
            m_lngInitialize();
            intPtr = clsYBPublic_cs.CreateInstace();
            int intRet = -1;
            StringBuilder sbPwd = new StringBuilder(32);
            if (intPtr > 0)
            {
                //原口令
                intRet = EncryptWithCipher(intPtr, m_strOldPwd, sbPwd, 32);
                if (intRet < 0)
                {
                    MessageBox.Show("明文密码加密失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
                string strOldPwd = sbPwd.ToString();

                //新口令
                intRet = EncryptWithCipher(intPtr, m_strNewPwd, sbPwd, 32);
                if (intRet < 0)
                {
                    MessageBox.Show("明文密码加密失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
                string strNewPwd = sbPwd.ToString();

                //医院登录
                intRet = SetParam(intPtr, "FN", "2");
                intRet = SetParam(intPtr, "YYBH", strUser);
                intRet = SetParam(intPtr, "USERID", strUser);
                intRet = SetParam(intPtr, "OLDPWD", strOldPwd);
                intRet = SetParam(intPtr, "NEWPWD", strNewPwd);
                intRet = SetParam(intPtr, "JBR", "001");
                intRet = Run(intPtr);

                if (intRet < 0)
                {
                    StringBuilder strRetValue1 = new StringBuilder(32);
                    intRet = GetSysMessage(intPtr, strRetValue1, 66);
                    ExceptionLog.OutPutException(strRetValue1.ToString());
                    MessageBox.Show(strRetValue1.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }

                StringBuilder strRetValue = new StringBuilder(32);
                StringBuilder strRetMessage = new StringBuilder(1024);
                intRet = GetParam(intPtr, "FHZ", strRetValue, 32);
                intRet = GetParam(intPtr, "MSG", strRetMessage, 1024);
                if (strRetValue.ToString() != "1")
                {
                    ExceptionLog.OutPutException(strRetMessage.ToString());
                    MessageBox.Show(strRetMessage.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    DestroyInstance(intPtr);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("创建实例失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DestroyInstance(intPtr);
                return -1;
            }
            m_blnWriteXML(strNodeName, "PASSWORD1", "AnyOne", m_strNewPwd);
            DestroyInstance(intPtr);
            return 1;
        }
        #endregion

        #region 医院经办人注册
        /// <summary>
        /// 医院经办人注册
        /// </summary>
        /// <param name="m_strYybh">医院编号</param>
        /// <param name="m_strXm">姓名</param>
        /// <param name="m_strSfhm">公民身份号码</param>
        /// <param name="m_strSsks">所属科室</param>
        /// <param name="m_strJbr">经办人:向社保系统注册医院经办人用户ID</param>
        /// <param name="m_intJbrlx">经办人类型:字典项1、普通经办人 2、门诊收费员 3、自助终端</param>
        /// <param name="m_intBglx">变更类型:字典1、新增2、修改</param>
        /// <returns></returns>
        public static long m_lngUserRegister(clsDGYBjbrzc_VO m_objDGYBjbrzc)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "3");
                intRet = SetParam(intH, "YYBH", m_objDGYBjbrzc.YYBH);
                intRet = SetParam(intH, "_JBR", m_objDGYBjbrzc.JBR);
                intRet = SetParam(intH, "JBRLX", m_objDGYBjbrzc.JBRLX);
                intRet = SetParam(intH, "XM", m_objDGYBjbrzc.XM);
                intRet = SetParam(intH, "GMSFHM", m_objDGYBjbrzc.GMSFHM);
                intRet = SetParam(intH, "SSKS", m_objDGYBjbrzc.SSKS);
                intRet = SetParam(intH, "BGLX", m_objDGYBjbrzc.BGLX);
                intRet = SetParam(intH, "JBR", "001");
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊记帐处方项目传送 SP3_2002
        /// <summary>
        /// 门诊记帐处方项目传送
        /// </summary>
        /// <param name="intPtr">句柄参数值</param>
        /// <returns></returns>
        public static long m_lngFunSP2002(List<clsDGMzxmcs_VO> lstDgmzxmcsVo, clsDGExtra_VO objDgmzextraVo)
        {
            if (lstDgmzxmcsVo.Count <= 0)
            {
                return -1;
            }
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_2002");
                intRet = SetParam(intH, "JBR", objDgmzextraVo.JBR);//经办人 //need mondify
                intRet = SetParam(intH, "YYBH", objDgmzextraVo.YYBH);//经办日期 //need mondify
                intRet = InsertDataSet(intH);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lstDgmzxmcsVo.Count; i++)
                {
                    intRet = InsertRow(intH);
                    intRet = SetField(intH, "ZYH", lstDgmzxmcsVo[i].ZYH);//
                    intRet = SetField(intH, "CFH", lstDgmzxmcsVo[i].CFH);//
                    intRet = SetField(intH, "GMSFHM", lstDgmzxmcsVo[i].GMSFHM);//
                    intRet = SetField(intH, "JZLB", lstDgmzxmcsVo[i].JZLB);//
                    intRet = SetField(intH, "FYRQ", lstDgmzxmcsVo[i].FYRQ);//8位
                    intRet = SetField(intH, "XMXH", lstDgmzxmcsVo[i].XMXH);//
                    intRet = SetField(intH, "XMBH", lstDgmzxmcsVo[i].XMBH);//
                    intRet = SetField(intH, "XMMC", lstDgmzxmcsVo[i].XMMC);//

                    intRet = SetField(intH, "JG", lstDgmzxmcsVo[i].JG.ToString("0.00"));
                    intRet = SetField(intH, "MCYL", lstDgmzxmcsVo[i].MCYL.ToString("0.00"));//
                    intRet = SetField(intH, "JE", lstDgmzxmcsVo[i].JE.ToString("0.00"));
                    intRet = SetField(intH, "ZFBL", lstDgmzxmcsVo[i].ZFBL.ToString("0.00"));//0<= X <= 1 无比例时，默认传0

                    intRet = SetField(intH, "YSGH", lstDgmzxmcsVo[i].YSGH);
                    intRet = SetField(intH, "BZ", lstDgmzxmcsVo[i].BZ);
                    if (lstDgmzxmcsVo[i].FHXZBZ != "3")
                        lstDgmzxmcsVo[i].FHXZBZ = "2";
                    intRet = SetField(intH, "FHXZBZ", lstDgmzxmcsVo[i].FHXZBZ);   // 符合限制标志 字典项: 2 符合; 3 不符合    待测试 
                    EndRow(intH, i + 1);

                    sb.AppendLine("处方号:" + lstDgmzxmcsVo[i].CFH + " 费用日期:" + lstDgmzxmcsVo[i].FYRQ + " 项目序号:" + lstDgmzxmcsVo[i].XMXH + " 项目代码:" + lstDgmzxmcsVo[i].XMBH +
                      " 项目名称:" + lstDgmzxmcsVo[i].XMMC + " 单价:" + lstDgmzxmcsVo[i].JG.ToString("0.00") + " 用量:" + lstDgmzxmcsVo[i].MCYL.ToString("0.00") +
                      " 金额:" + lstDgmzxmcsVo[i].JE.ToString("0.00") + " 适应症:" + lstDgmzxmcsVo[i].FHXZBZ);
                }
                Log.Output("门诊社保上传数据:" + Environment.NewLine + sb.ToString());

                intRet = EndDataSet(intH, "MZCFXMDR");
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                Log.Output("上传返回值： " + Environment.NewLine + strValue);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    //need modify 这2个字段根据业务需求是否需要保存到his库
                    intRet = GetParam(intH, "PH", strValue, 1024);//批号，	批号（PH）：由医保系统产生，作为本次传送的标识。
                    string strPH = strValue.ToString();
                    intRet = GetParam(intH, "ZJE", strValue, 1024);//	总金额：本次传送的记帐处方项目汇总的医疗费用总额。每条传入的处方项目的“金额”字段合计出的总金额。
                    string strZJE = strValue.ToString();
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    //ExceptionLog.OutPutException(strValue.ToString());                    
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊记帐处方项目删除[SP3_2003]
        /// <summary>
        /// 门诊结算退款[SP3_2003]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP2003(clsDGExtra_VO objDgmzextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_2003");
                intRet = SetParam(intH, "YYBH", objDgmzextraVo.YYBH);
                intRet = SetParam(intH, "ZYH", objDgmzextraVo.ZYH);
                intRet = SetParam(intH, "CFH", objDgmzextraVo.SDYWH);
                intRet = SetParam(intH, "JBR", objDgmzextraVo.JBR);//经办人 //need mondify
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊结算[SP3_2004]
        /// <summary>
        /// 门诊结算[SP3_2004]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgmzjsVo"></param>
        /// <param name="objDgmzjsfhVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP2004(clsDGMzjs_VO objDgmzjsVo, out clsDGMzjsfh_VO objDgmzjsfhVo)
        {
            objDgmzjsfhVo = null;
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SP3_2004 入参:");
                sb.AppendLine(string.Format("YYBH := {0}", objDgmzjsVo.YYBH));
                sb.AppendLine(string.Format("JZYYBH := {0}", objDgmzjsVo.JZYYBH));
                sb.AppendLine(string.Format("ZYH := {0}", objDgmzjsVo.ZYH));
                sb.AppendLine(string.Format("CFH := {0}", objDgmzjsVo.CFH));
                sb.AppendLine(string.Format("GMSFHM := {0}", objDgmzjsVo.GMSFHM));
                sb.AppendLine(string.Format("JZRQ := {0}", objDgmzjsVo.JZRQ));
                sb.AppendLine(string.Format("CYZD := {0}", objDgmzjsVo.CYZD));
                sb.AppendLine(string.Format("CYBQDM := {0}", objDgmzjsVo.CYBQDM));
                sb.AppendLine(string.Format("YYRYKS := {0}", objDgmzjsVo.YYRYKS));
                sb.AppendLine(string.Format("YLFYZE := {0}", objDgmzjsVo.YLFYZE));
                sb.AppendLine(string.Format("FPHM := {0}", objDgmzjsVo.FPHM));
                sb.AppendLine(string.Format("LXDH := {0}", objDgmzjsVo.LXDH));
                sb.AppendLine(string.Format("BZ := {0}", objDgmzjsVo.BZ));
                sb.AppendLine(string.Format("JBR := {0}", objDgmzjsVo.JBR));
                Log.Output(sb.ToString());

                intRet = SetParam(intH, "FN", "SP3_2004");
                intRet = SetParam(intH, "YYBH", objDgmzjsVo.YYBH);
                intRet = SetParam(intH, "JZYYBH", objDgmzjsVo.JZYYBH);
                intRet = SetParam(intH, "ZYH", objDgmzjsVo.ZYH);
                intRet = SetParam(intH, "CFH", objDgmzjsVo.CFH);
                intRet = SetParam(intH, "GMSFHM", objDgmzjsVo.GMSFHM);
                intRet = SetParam(intH, "JZRQ", objDgmzjsVo.JZRQ);
                intRet = SetParam(intH, "CYZD", objDgmzjsVo.CYZD);
                intRet = SetParam(intH, "CYBQDM", objDgmzjsVo.CYBQDM);
                intRet = SetParam(intH, "YYRYKS", objDgmzjsVo.YYRYKS);
                intRet = SetParam(intH, "YLFYZE", objDgmzjsVo.YLFYZE);
                intRet = SetParam(intH, "FPHM", objDgmzjsVo.FPHM);
                intRet = SetParam(intH, "LXDH", objDgmzjsVo.LXDH);
                intRet = SetParam(intH, "BZ", objDgmzjsVo.BZ);
                intRet = SetParam(intH, "JBR", objDgmzjsVo.JBR);
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    objDgmzjsfhVo = new clsDGMzjsfh_VO();
                    intRet = GetParam(intH, "JZJLH", strValue, 1024);//就诊记录号
                    objDgmzjsfhVo.JZJLH = strValue.ToString();
                    intRet = GetParam(intH, "SDYWH", strValue, 1024);//结算序号,系列号
                    objDgmzjsfhVo.SDYWH = strValue.ToString();
                    intRet = GetParam(intH, "CFH", strValue, 1024);
                    objDgmzjsfhVo.CFH = strValue.ToString();
                    intRet = GetParam(intH, "YLFYZE", strValue, 1024);
                    decimal decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.YLFYZE = ConvertObjToDecimal(decTemp.ToString("0.00"));
                    intRet = GetParam(intH, "TCZF", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.TCZF = ConvertObjToDecimal(decTemp.ToString("0.00"));
                    intRet = GetParam(intH, "GRZFZE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.GRZFZE = ConvertObjToDecimal(decTemp.ToString("0.00"));
                    intRet = GetParam(intH, "JSRQ", strValue, 1024);
                    objDgmzjsfhVo.JSRQ = strValue.ToString();
                    intRet = GetParam(intH, "XM", strValue, 1024);
                    objDgmzjsfhVo.XM = strValue.ToString();
                    intRet = GetParam(intH, "JISFS", strValue, 1024);
                    objDgmzjsfhVo.JISFS = strValue.ToString();
                    intRet = GetParam(intH, "YYBH", strValue, 1024);
                    objDgmzjsfhVo.YYBH = strValue.ToString();
                    intRet = GetParam(intH, "GMSFHM", strValue, 1024);
                    objDgmzjsfhVo.GMSFHM = strValue.ToString();
                    intRet = GetParam(intH, "ZYH", strValue, 1024);
                    objDgmzjsfhVo.ZYH = strValue.ToString();
                    intRet = GetParam(intH, "ZFYY", strValue, 1024);
                    objDgmzjsfhVo.ZFYY = strValue.ToString();
                    intRet = GetParam(intH, "JZLB", strValue, 1024);
                    objDgmzjsfhVo.JZLB = strValue.ToString();
                    intRet = GetParam(intH, "ZH", strValue, 1024);//由社区系统返回
                    objDgmzjsfhVo.ZH = strValue.ToString();
                    intRet = GetParam(intH, "MZYFBXJE", strValue, 1024);//民政优抚报销金额 【预留字段】
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.MZYFBXJE = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF1", strValue, 1024);//补充医疗（1）统筹支付
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.BCYLTCZF1 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF2", strValue, 1024);
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.BCYLTCZF2 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF3", strValue, 1024);
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.BCYLTCZF3 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF4", strValue, 1024);
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.BCYLTCZF4 = decTemp;
                    intRet = GetParam(intH, "QTZHIFU", strValue, 1024);
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.QTZHIFU = decTemp;
                    intRet = GetParam(intH, "YBJZFPJE", strValue, 1024);
                    decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgmzjsfhVo.YBJZFPJE = decTemp;
                    lngRes = 1;

                    sb = null;
                    sb = new StringBuilder();
                    sb.AppendLine("SP3_2004 返回结果:");
                    sb.AppendLine(string.Format("JZJLH := {0}", objDgmzjsfhVo.JZJLH));
                    sb.AppendLine(string.Format("SDYWH := {0}", objDgmzjsfhVo.SDYWH));
                    sb.AppendLine(string.Format("CFH := {0}", objDgmzjsfhVo.CFH));
                    sb.AppendLine(string.Format("YLFYZE := {0}", objDgmzjsfhVo.YLFYZE));
                    sb.AppendLine(string.Format("TCZF := {0}", objDgmzjsfhVo.TCZF));
                    sb.AppendLine(string.Format("GRZFZE := {0}", objDgmzjsfhVo.GRZFZE));
                    sb.AppendLine(string.Format("JSRQ := {0}", objDgmzjsfhVo.JSRQ));
                    sb.AppendLine(string.Format("XM := {0}", objDgmzjsfhVo.XM));
                    sb.AppendLine(string.Format("JISFS := {0}", objDgmzjsfhVo.JISFS));
                    sb.AppendLine(string.Format("YYBH := {0}", objDgmzjsfhVo.YYBH));
                    sb.AppendLine(string.Format("GMSFHM := {0}", objDgmzjsfhVo.GMSFHM));
                    sb.AppendLine(string.Format("ZYH := {0}", objDgmzjsfhVo.ZYH));
                    sb.AppendLine(string.Format("ZFYY := {0}", objDgmzjsfhVo.ZFYY));
                    sb.AppendLine(string.Format("JZLB := {0}", objDgmzjsfhVo.JZLB));
                    sb.AppendLine(string.Format("ZH := {0}", objDgmzjsfhVo.ZH));
                    sb.AppendLine(string.Format("MZYFBXJE := {0}", objDgmzjsfhVo.MZYFBXJE));
                    sb.AppendLine(string.Format("BCYLTCZF1 := {0}", objDgmzjsfhVo.BCYLTCZF1));
                    sb.AppendLine(string.Format("BCYLTCZF2 := {0}", objDgmzjsfhVo.BCYLTCZF2));
                    sb.AppendLine(string.Format("BCYLTCZF3 := {0}", objDgmzjsfhVo.BCYLTCZF3));
                    sb.AppendLine(string.Format("BCYLTCZF4 := {0}", objDgmzjsfhVo.BCYLTCZF4));
                    sb.AppendLine(string.Format("QTZHIFU := {0}", objDgmzjsfhVo.QTZHIFU));
                    sb.AppendLine(string.Format("YBJZFPJE := {0}", objDgmzjsfhVo.YBJZFPJE));
                    Log.Output(sb.ToString());
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 取消门诊结算[SP3_2005]
        /// <summary>
        /// 取消门诊结算[SP3_2005]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP2005(clsDGExtra_VO objDgmzextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_2005");
                intRet = SetParam(intH, "YYBH", objDgmzextraVo.YYBH);
                intRet = SetParam(intH, "JZJLH", objDgmzextraVo.JZJLH);
                intRet = SetParam(intH, "SDYWH", objDgmzextraVo.SDYWH);
                intRet = SetParam(intH, "JBR", objDgmzextraVo.JBR);//经办人 //need mondify
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊结算信息修改[SP3_2006]
        /// <summary>
        /// 门诊结算信息修改[SP3_2006]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP2006(clsDGExtra_VO objDgextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_2006");

                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);//医院编号
                intRet = SetParam(intH, "FPHM", objDgextraVo.FPHM);//发票号码
                intRet = SetParam(intH, "SDYWH", objDgextraVo.SDYWH);//结算序号
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);//经办人
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Equals("1"))
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊结算退款[SP3_2007]
        /// <summary>
        /// 门诊结算退款[SP3_2007]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP2007(clsDGExtra_VO objDgextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_2007");

                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);//经办日期 //need mondify
                intRet = SetParam(intH, "JZJLH", objDgextraVo.JZJLH);
                intRet = SetParam(intH, "SDYWH", objDgextraVo.SDYWH);
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);//经办人 //need mondify
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 门诊待遇享受条件判断[SP3_1002]
        /// <summary>
        /// 门诊待遇享受条件判断[SP3_1002]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgmzdyVo"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP1002(int intPtr, clsDGMzdyxs_VO objDgmzdyVo, clsDGExtra_VO objDgextraVo, ref string p_strJZLB, ref string p_strYY, ref string p_strDYXSBZ, ref List<string> lstJzlb, bool isSybx, bool isCovi19,bool isFp)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                string jzlb = "";
                if (isSybx)
                    jzlb = "73";
                else if (isCovi19)
                    jzlb = "57";
                else if (isFp)
                    jzlb = "79";
                intRet = SetParam(intH, "FN", "SP3_1002");
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "GMSFHM", objDgmzdyVo.GMSFHM);
                // 73 产前检查; 57 重流门诊; 79 计划生育门诊 其他: 传空，默认全查
                intRet = SetParam(intH, "JZLB", jzlb);
                intRet = SetParam(intH, "JZRQ", DateTime.Now.ToString("yyyyMMdd"));
                intRet = SetParam(intH, "YYBH", objDgmzdyVo.YYBH);
                intRet = SetParam(intH, "JZYYBH", objDgmzdyVo.JZYYBH);

                #region Log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_1002"));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("GMSFHM := {0}", objDgmzdyVo.GMSFHM));
                sb.AppendLine(string.Format("JZLB := {0}", jzlb));
                sb.AppendLine(string.Format("JZRQ := {0}", DateTime.Now.ToString("yyyyMMdd")));
                sb.AppendLine(string.Format("YYBH := {0}", objDgmzdyVo.YYBH));
                sb.AppendLine(string.Format("JZYYBH := {0}", objDgmzdyVo.JZYYBH));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                    intRet = LocateDataSet(intH, "DYXSQK");//定位结果集
                    int intRowCount = GetRowSize(intH);
                    if (intRowCount <= 0)
                    {
                        DestroyInstance(intH);
                        return -1;//待遇享受情况查询结果集为空
                    }
                    string p_strDYXSBZ_Temp = string.Empty;
                    string p_strYY_Temp = string.Empty;
                    string p_strJZLB_Temp = string.Empty;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        try
                        {
                            StringBuilder sbValue = new StringBuilder(1024);
                            intRet = GetFieldValue(intH, "JZLB", sbValue, 1024);
                            p_strJZLB_Temp = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YY", sbValue, 1024);
                            p_strYY_Temp = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "DYXSBZ", sbValue, 1024);//0 不能享受 1 享受
                            p_strDYXSBZ_Temp = sbValue.ToString().Trim();

                            #region Log
                            sb = new StringBuilder();
                            sb.AppendLine(string.Format("JZLB := {0}", p_strJZLB_Temp));
                            sb.AppendLine(string.Format("YY := {0}", p_strYY_Temp));
                            sb.AppendLine(string.Format("DYXSBZ := {0}", p_strDYXSBZ_Temp));
                            Log.Output(sb.ToString());
                            #endregion

                            if (p_strDYXSBZ_Temp == "1")
                            {
                                p_strDYXSBZ = p_strDYXSBZ_Temp;
                                p_strYY = p_strYY_Temp;
                                p_strJZLB = p_strJZLB_Temp;
                                lstJzlb.Add(p_strJZLB_Temp);
                            }
                            intRet = NextRow(intH);
                        }
                        catch (Exception objEx)
                        {
                            ExceptionLog.OutPutException("读取结算返回数据集出错:" + objEx.Message);
                            MessageBox.Show("读取结算返回数据集出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question); ;
                            lngRes = -1;
                        }
                    }
                    if (p_strDYXSBZ == "0" || string.IsNullOrEmpty(p_strDYXSBZ))
                    {
                        p_strDYXSBZ = p_strDYXSBZ_Temp;
                        p_strYY = p_strYY_Temp;
                        p_strJZLB = p_strJZLB_Temp;
                    }
                }
                else
                {
                    lngRes = -1;
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院登记[SP3_3001]
        /// <summary>
        /// 住院登记[SP3_3001]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgzydjVo"></param>
        /// <param name="objDgextraVo">经办人</param>
        /// <param name="p_strJHJLH"></param>
        /// <returns></returns>
        public static long m_lngFunSP3001(clsDGZydj_VO objDgzydjVo, clsDGExtra_VO objDgextraVo, ref string p_strJHJLH)
        {
            int intH = CreateInstace();
            int intRet = -1;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3001");
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "GMSFHM", objDgzydjVo.GMSFHM);
                intRet = SetParam(intH, "YYBH", objDgzydjVo.YYBH);
                intRet = SetParam(intH, "ZYH", objDgzydjVo.ZYH);
                intRet = SetParam(intH, "ZYKS", objDgzydjVo.ZYKS);
                intRet = SetParam(intH, "YYRYKS", objDgzydjVo.YYRYKS);
                intRet = SetParam(intH, "CWH", objDgzydjVo.CWH);
                intRet = SetParam(intH, "RYRQ", objDgzydjVo.RYRQ);//入院日期，次方法在入院登记时调用，可直接传当前时间 YYYYMMDD
                intRet = SetParam(intH, "ZYLB", objDgzydjVo.ZYLB);
                intRet = SetParam(intH, "JZLB", objDgzydjVo.JZLB);//登记时，操作员选择。医保的字典项
                intRet = SetParam(intH, "RYZD", objDgzydjVo.RYZD);
                intRet = SetParam(intH, "WSBZ", objDgzydjVo.WSBZ);//登记时，操作员选择。医保的字典项
                intRet = SetParam(intH, "ZQQRQK", objDgzydjVo.ZQQRQK);//字典项（住院类别为工伤时，默认录入1、同意，且不可改变）住院类别为医疗时必须录入
                intRet = SetParam(intH, "ZQQRSBH", objDgzydjVo.ZQQRSBH);//不知道是什么东西，可为空
                intRet = SetParam(intH, "YSGH", objDgzydjVo.YSGH);
                intRet = SetParam(intH, "LXDH", objDgzydjVo.LXDH);
                intRet = SetParam(intH, "BZ", objDgzydjVo.BZ);
                intRet = SetParam(intH, "CBDTCQBM", objDgzydjVo.CBDTCQBM);
                intRet = SetParam(intH, "RYDYZDBY", objDgzydjVo.RYDYZDBY);
                // 异地医保
                intRet = SetParam(intH, "RYZDICD1", objDgzydjVo.Icd10_1);   // 入院诊断疾病编码1
                intRet = SetParam(intH, "RYZDICD2", objDgzydjVo.Icd10_2);   // 入院诊断编码2
                intRet = SetParam(intH, "RYZDICD3", objDgzydjVo.Icd10_3);   // 入院诊断编码3
                intRet = SetParam(intH, "ZYYY", objDgzydjVo.InReason);      // 住院原因
                intRet = SetParam(intH, "BZLX", objDgzydjVo.AssiType);      // 补助类型
                intRet = SetParam(intH, "FYBH", objDgzydjVo.FYBH);          // 分院编号 为空时默认为本院-0
                intRet = SetParam(intH, "TXBZ", objDgzydjVo.TXBZ);          // 透析标志 0 否 1 是
                
                #region Log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3001"));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("GMSFHM := {0}", objDgzydjVo.GMSFHM));
                sb.AppendLine(string.Format("YYBH := {0}", objDgzydjVo.YYBH));
                sb.AppendLine(string.Format("ZYH := {0}", objDgzydjVo.ZYH));
                sb.AppendLine(string.Format("ZYKS := {0}", objDgzydjVo.ZYKS));
                sb.AppendLine(string.Format("YYRYKS := {0}", objDgzydjVo.YYRYKS));
                sb.AppendLine(string.Format("CWH := {0}", objDgzydjVo.CWH));
                sb.AppendLine(string.Format("RYRQ := {0}", objDgzydjVo.RYRQ));
                sb.AppendLine(string.Format("ZYLB := {0}", objDgzydjVo.ZYLB));
                sb.AppendLine(string.Format("JZLB := {0}", objDgzydjVo.JZLB));
                sb.AppendLine(string.Format("RYZD := {0}", objDgzydjVo.RYZD));
                sb.AppendLine(string.Format("WSBZ := {0}", objDgzydjVo.WSBZ));
                sb.AppendLine(string.Format("ZQQRQK := {0}", objDgzydjVo.ZQQRQK));
                sb.AppendLine(string.Format("ZQQRSBH := {0}", objDgzydjVo.ZQQRSBH));
                sb.AppendLine(string.Format("YSGH := {0}", objDgzydjVo.YSGH));
                sb.AppendLine(string.Format("LXDH := {0}", objDgzydjVo.LXDH));
                sb.AppendLine(string.Format("BZ := {0}", objDgzydjVo.BZ));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgzydjVo.CBDTCQBM));
                sb.AppendLine(string.Format("RYDYZDBY := {0}", objDgzydjVo.RYDYZDBY));
                sb.AppendLine(string.Format("RYZDICD1 := {0}", objDgzydjVo.Icd10_1));
                sb.AppendLine(string.Format("RYZDICD2 := {0}", objDgzydjVo.Icd10_2));
                sb.AppendLine(string.Format("RYZDICD3 := {0}", objDgzydjVo.Icd10_3));
                sb.AppendLine(string.Format("ZYYY := {0}", objDgzydjVo.InReason));
                sb.AppendLine(string.Format("BZLX := {0}", objDgzydjVo.AssiType));
                sb.AppendLine(string.Format("FYBH := {0}", objDgzydjVo.FYBH));
                sb.AppendLine(string.Format("TXBZ := {0}", objDgzydjVo.TXBZ));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "JZJLH", strValue, 1024);
                    p_strJHJLH = strValue.ToString().Trim();
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    intRet = -1;
                }
            }
            DestroyInstance(intH);
            return intRet;
        }
        #endregion

        #region 修改住院登记信息[SP3_3009]
        /// <summary>
        /// 修改住院登记信息[SP3_3009]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgzydjVo"></param>
        /// <param name="objDgextraVo">经办人</param>
        /// <param name="p_strJHJLH"></param>
        /// <returns></returns>
        public static long m_lngFunSP3009(clsDGZydj_VO objDgzydjVo, clsDGExtra_VO objDgextraVo)
        {
            int intH = CreateInstace();
            int intRet = -1;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3009");
                intRet = SetParam(intH, "YYBH", objDgzydjVo.YYBH);
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "JZJLH", objDgextraVo.JZJLH);
                intRet = SetParam(intH, "ZYH", objDgzydjVo.ZYH);
                intRet = SetParam(intH, "RYKS", objDgzydjVo.ZYKS);
                intRet = SetParam(intH, "YYRYKS", objDgzydjVo.YYRYKS);
                intRet = SetParam(intH, "CYKS", objDgzydjVo.CYKS);
                intRet = SetParam(intH, "YYCYKS", objDgzydjVo.YYCYKS);
                intRet = SetParam(intH, "CWH", objDgzydjVo.CWH);
                intRet = SetParam(intH, "RYRQ", objDgzydjVo.RYRQ);//入院日期，次方法在入院登记时调用，可直接传当前时间 YYYYMMDD
                intRet = SetParam(intH, "CYRQ", objDgzydjVo.CYRQ);
                intRet = SetParam(intH, "CYBZ", objDgzydjVo.CYBZ);//出院标志 N
                intRet = SetParam(intH, "YSGH", objDgzydjVo.YSGH);
                intRet = SetParam(intH, "LXDH", objDgzydjVo.LXDH);
                intRet = SetParam(intH, "BZ", objDgzydjVo.BZ);
                intRet = SetParam(intH, "CBDTCQBM", objDgzydjVo.CBDTCQBM);//参保地统筹区编码
                intRet = SetParam(intH, "JZLB", objDgzydjVo.JZLB);//登记时，操作员选择。医保的字典项
                intRet = SetParam(intH, "ZQQRSBH", objDgzydjVo.ZQQRSBH);//知情确认书编号
                intRet = SetParam(intH, "RYDYZDBY", objDgzydjVo.RYDYZDBY);
                // 异地医保
                intRet = SetParam(intH, "RYZDICD1", objDgzydjVo.Icd10_1);   // 入院诊断疾病编码1
                intRet = SetParam(intH, "RYZDICD2", objDgzydjVo.Icd10_2);   // 入院诊断编码2
                intRet = SetParam(intH, "RYZDICD3", objDgzydjVo.Icd10_3);   // 入院诊断编码3
                intRet = SetParam(intH, "ZYYY", objDgzydjVo.InReason);      // 住院原因
                intRet = SetParam(intH, "BZLX", objDgzydjVo.AssiType);      // 补助类型
                intRet = SetParam(intH, "FYBH", objDgzydjVo.FYBH);          // 分院编号 为空时默认为本院-0
                intRet = SetParam(intH, "TXBZ", objDgzydjVo.TXBZ);          // 透析标志 0 否 1 是

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3009"));
                sb.AppendLine(string.Format("YYBH := {0}", objDgzydjVo.YYBH));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("JZJLH := {0}", objDgextraVo.JZJLH));
                sb.AppendLine(string.Format("ZYH := {0}", objDgzydjVo.ZYH));
                sb.AppendLine(string.Format("RYKS := {0}", objDgzydjVo.ZYKS));
                sb.AppendLine(string.Format("YYRYKS := {0}", objDgzydjVo.YYRYKS));
                sb.AppendLine(string.Format("CYKS := {0}", objDgzydjVo.CYKS));
                sb.AppendLine(string.Format("YYCYKS := {0}", objDgzydjVo.YYCYKS));
                sb.AppendLine(string.Format("CWH := {0}", objDgzydjVo.CWH));
                sb.AppendLine(string.Format("RYRQ := {0}", objDgzydjVo.RYRQ));
                sb.AppendLine(string.Format("CYRQ := {0}", objDgzydjVo.CYRQ));
                sb.AppendLine(string.Format("CYBZ := {0}", objDgzydjVo.CYBZ));
                sb.AppendLine(string.Format("YSGH := {0}", objDgzydjVo.YSGH));
                sb.AppendLine(string.Format("LXDH := {0}", objDgzydjVo.LXDH));
                sb.AppendLine(string.Format("BZ := {0}", objDgzydjVo.BZ));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgzydjVo.CBDTCQBM));
                sb.AppendLine(string.Format("JZLB := {0}", objDgzydjVo.JZLB));
                sb.AppendLine(string.Format("ZQQRSBH := {0}", objDgzydjVo.ZQQRSBH));
                sb.AppendLine(string.Format("RYDYZDBY := {0}", objDgzydjVo.RYDYZDBY));
                sb.AppendLine(string.Format("RYZDICD1 := {0}", objDgzydjVo.Icd10_1));
                sb.AppendLine(string.Format("RYZDICD2 := {0}", objDgzydjVo.Icd10_2));
                sb.AppendLine(string.Format("RYZDICD3 := {0}", objDgzydjVo.Icd10_3));
                sb.AppendLine(string.Format("ZYYY := {0}", objDgzydjVo.InReason));
                sb.AppendLine(string.Format("BZLX := {0}", objDgzydjVo.AssiType));
                sb.AppendLine(string.Format("FYBH := {0}", objDgzydjVo.FYBH));
                sb.AppendLine(string.Format("TXBZ := {0}", objDgzydjVo.TXBZ));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {

                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    intRet = -1;
                }
            }
            DestroyInstance(intH);
            return intRet;
        }
        #endregion

        #region 住院待遇享受条件判断[SP3_1003]
        /// <summary>
        /// 住院待遇享受条件判断[SP3_1003]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgzydyxsVo"></param>
        /// <param name="objDgextraVo">经办人、医院编号</param>
        /// <param name="p_strYY"></param>
        /// <param name="p_strDYXSBZ"></param>
        /// <returns></returns>
        public static long m_lngFunSP1003(clsDGZydyxs_VO objDgzydyxsVo, clsDGExtra_VO objDgextraVo, ref string p_strYY, ref string p_strDYXSBZ)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_1003");
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);
                intRet = SetParam(intH, "GMSFHM", objDgzydyxsVo.GMSFHM);
                intRet = SetParam(intH, "ZYLB", objDgzydyxsVo.ZYLB);
                intRet = SetParam(intH, "JZJLH", objDgzydyxsVo.JZJLH);
                intRet = SetParam(intH, "JSRQ", objDgzydyxsVo.JSRQ);
                intRet = SetParam(intH, "CBDTCQBM", objDgzydyxsVo.CBDTCQBM);

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_1003"));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("YYBH := {0}", objDgextraVo.YYBH));
                sb.AppendLine(string.Format("GMSFHM := {0}", objDgzydyxsVo.GMSFHM));
                sb.AppendLine(string.Format("ZYLB := {0}", objDgzydyxsVo.ZYLB));
                sb.AppendLine(string.Format("JZJLH := {0}", objDgzydyxsVo.JZJLH));
                sb.AppendLine(string.Format("JSRQ := {0}", objDgzydyxsVo.JSRQ));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgzydyxsVo.CBDTCQBM));
                Log.Output(sb.ToString());
                sb.Length = 0;
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "YY", sbValue, 1024);
                    p_strYY = sbValue.ToString().Trim();//原因
                    intRet = GetParam(intH, "DYXSBZ", sbValue, 1024);
                    p_strDYXSBZ = sbValue.ToString().Trim();//待遇享受标志
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院记帐处方项目传送[SP3_3002]
        /// <summary>
        /// 住院记帐处方项目传送[SP3_3002]
        /// </summary>
        /// <param name="lstDGzyxmcsVo"></param>
        /// <param name="objDgextraVo"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static long m_lngFunSP3002(List<clsDGZyxmcs_VO> lstDGzyxmcsVo, clsDGExtra_VO objDgextraVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3002");
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);
                intRet = SetParam(intH, "CBDTCQBM", lstDGzyxmcsVo[0].CBDTCQBM);
                intRet = InsertDataSet(intH);
                com.digitalwave.Utility.clsLogText jiachengLog = new com.digitalwave.Utility.clsLogText();

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3002"));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("YYBH := {0}", objDgextraVo.YYBH));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", lstDGzyxmcsVo[0].CBDTCQBM));
                Log.Output(sb.ToString());
                #endregion

                int j = 0;
                List<string> lstId = new List<string>();
                foreach (clsDGZyxmcs_VO item in lstDGzyxmcsVo)
                {
                    if (lstId.IndexOf(item.CFXMWYH.ToString().Trim()) >= 0) continue;
                    lstId.Add(item.CFXMWYH.ToString().Trim());

                    // 加收判断 2019-10-24  ( 儿童价格加收...)
                    if (item.MCYL == 1)
                    {
                        if (item.JE - item.JG > 10)
                        {
                            item.JG = item.JE;
                        }
                    }
                    else if (item.MCYL > 1)
                    {
                        decimal tmpPrice = weCare.Core.Utils.Function.Round(item.JE / item.MCYL, 2);
                        if (tmpPrice - item.JG > 10)
                        {
                            item.JG = tmpPrice;
                        }
                    }

                    intRet = InsertRow(intH);
                    intRet = SetField(intH, "JZJLH", item.JZJLH.ToString().Trim());//
                    intRet = SetField(intH, "GRSHBZH", item.GRSHBZH.ToString().Trim());//
                    intRet = SetField(intH, "ZYH", item.ZYH.ToString().Trim());//
                    intRet = SetField(intH, "XMXH", Convert.ToString(++j));
                    intRet = SetField(intH, "CFXMWYH", item.CFXMWYH.ToString().Trim());

                    intRet = SetField(intH, "YYXMBM", item.YYXMBM.ToString().Trim());
                    intRet = SetField(intH, "XMMC", item.XMMC.ToString().Trim());//
                    intRet = SetField(intH, "YYFLDM", item.YYFLDM.ToString().Trim());//
                    intRet = SetField(intH, "YPGG", item.YPGG.ToString().Trim());//
                    intRet = SetField(intH, "YPJX", item.YPJX.ToString().Trim());//

                    intRet = SetField(intH, "JG", item.JG.ToString("0.0000"));
                    intRet = SetField(intH, "MCYL", item.MCYL.ToString("0.00"));//
                    //decimal JE = item.JG * item.MCYL;
                    intRet = SetField(intH, "JE", item.JE.ToString("0.00"));
                    intRet = SetField(intH, "ZFEIBL", item.ZFEIBL.ToString("0.00"));//0<= X <= 1 无比例时，默认传0
                    intRet = SetField(intH, "ZFEIJE", item.ZFEIJE.ToString("0.00"));

                    intRet = SetField(intH, "FHXZBZ", item.FHXZBZ.ToString().Trim());
                    intRet = SetField(intH, "JZSJ", item.JZSJ.ToString().Trim());//记账时间 yyyymmddhh24miss
                    intRet = SetField(intH, "YSGH", item.YSGH.ToString().Trim());
                    intRet = SetField(intH, "BZ3", item.BZ3.ToString().Trim());
                    intRet = SetField(intH, "CBDTCQBM", item.CBDTCQBM);
                    EndRow(intH, j);

                    #region log
                    sb = new StringBuilder();
                    sb.AppendLine(string.Format("JZJLH := {0}", item.JZJLH.ToString().Trim()));
                    sb.AppendLine(string.Format("GRSHBZH := {0}", item.GRSHBZH.ToString().Trim()));
                    sb.AppendLine(string.Format("ZYH := {0}", item.ZYH.ToString().Trim()));
                    sb.AppendLine(string.Format("XMXH := {0}", j));
                    sb.AppendLine(string.Format("CFXMWYH := {0}", item.CFXMWYH.ToString().Trim()));
                    sb.AppendLine(string.Format("YYXMBM := {0}", item.YYXMBM.ToString().Trim()));
                    sb.AppendLine(string.Format("XMMC := {0}", item.XMMC.ToString().Trim()));
                    sb.AppendLine(string.Format("YYFLDM := {0}", item.YYFLDM.ToString().Trim()));
                    sb.AppendLine(string.Format("YPGG := {0}", item.YPGG.ToString().Trim()));
                    sb.AppendLine(string.Format("YPJX := {0}", item.YPJX.ToString().Trim()));
                    sb.AppendLine(string.Format("JG := {0}", item.JG.ToString("0.0000")));
                    sb.AppendLine(string.Format("MCYL := {0}", item.MCYL.ToString("0.00")));
                    sb.AppendLine(string.Format("JE := {0}", item.JE.ToString("0.00")));
                    sb.AppendLine(string.Format("ZFEIBL := {0}", item.ZFEIBL.ToString("0.00")));
                    sb.AppendLine(string.Format("ZFEIJE := {0}", item.ZFEIJE.ToString("0.00")));
                    sb.AppendLine(string.Format("FHXZBZ := {0}", item.FHXZBZ.ToString().Trim()));
                    sb.AppendLine(string.Format("JZSJ := {0}", item.JZSJ.ToString().Trim()));
                    sb.AppendLine(string.Format("YSGH := {0}", item.YSGH.ToString().Trim()));
                    sb.AppendLine(string.Format("BZ3 := {0}", item.BZ3.ToString().Trim()));
                    sb.AppendLine(string.Format("CBDTCQBM := {0}", item.CBDTCQBM));
                    Log.Output(sb.ToString());
                    #endregion
                }
                #region bak 0812
                //for (int i = 0; i < lstDGzyxmcsVo.Count; i++)
                //{
                //    intRet = InsertRow(intH);
                //    intRet = SetField(intH, "JZJLH", lstDGzyxmcsVo[i].JZJLH.ToString().Trim());//
                //    intRet = SetField(intH, "GRSHBZH", lstDGzyxmcsVo[i].GRSHBZH.ToString().Trim());//
                //    intRet = SetField(intH, "ZYH", lstDGzyxmcsVo[i].ZYH.ToString().Trim());//
                //    intRet = SetField(intH, "XMXH", (i + 1).ToString());//
                //    intRet = SetField(intH, "CFXMWYH", lstDGzyxmcsVo[i].CFXMWYH.ToString().Trim());


                //    intRet = SetField(intH, "YYXMBM", lstDGzyxmcsVo[i].YYXMBM.ToString().Trim());
                //    intRet = SetField(intH, "XMMC", lstDGzyxmcsVo[i].XMMC.ToString().Trim());//
                //    intRet = SetField(intH, "YYFLDM", lstDGzyxmcsVo[i].YYFLDM.ToString().Trim());//
                //    intRet = SetField(intH, "YPGG", lstDGzyxmcsVo[i].YPGG.ToString().Trim());//
                //    intRet = SetField(intH, "YPJX", lstDGzyxmcsVo[i].YPJX.ToString().Trim());//

                //    intRet = SetField(intH, "JG", lstDGzyxmcsVo[i].JG.ToString("0.0000"));
                //    intRet = SetField(intH, "MCYL", lstDGzyxmcsVo[i].MCYL.ToString("0.00"));//
                //    intRet = SetField(intH, "JE", lstDGzyxmcsVo[i].JE.ToString("0.00"));
                //    intRet = SetField(intH, "ZFEIBL", lstDGzyxmcsVo[i].ZFEIBL.ToString("0.00"));//0<= X <= 1 无比例时，默认传0
                //    intRet = SetField(intH, "ZFEIJE", lstDGzyxmcsVo[i].ZFEIJE.ToString("0.00"));

                //    intRet = SetField(intH, "FHXZBZ", lstDGzyxmcsVo[i].FHXZBZ.ToString().Trim());
                //    intRet = SetField(intH, "JZSJ", lstDGzyxmcsVo[i].JZSJ.ToString().Trim());//记账时间 yyyymmddhh24miss
                //    intRet = SetField(intH, "YSGH", lstDGzyxmcsVo[i].YSGH.ToString().Trim());
                //    intRet = SetField(intH, "BZ3", lstDGzyxmcsVo[i].BZ3.ToString().Trim());
                //    intRet = SetField(intH, "CBDTCQBM", lstDGzyxmcsVo[i].CBDTCQBM);
                //    EndRow(intH, i + 1);
                //    //jiachengLog.LogError("guoJG价格: " + lstDGzyxmcsVo[i].JG.ToString("0.0000") + " 数量" + lstDGzyxmcsVo[i].MCYL + " 合计:" + lstDGzyxmcsVo[i].JE);
                //    //jiachengLog.LogError("\r\n JZJLH:" + lstDGzyxmcsVo[i].JZJLH.ToString().Trim() + "\r\n GRSHBZH" + lstDGzyxmcsVo[i].GRSHBZH.ToString().Trim() + "\r\n ZYH" + lstDGzyxmcsVo[i].ZYH.ToString().Trim() + "\r\n XMXH" + (i + 1).ToString() + "\r\n CFXMWYH" + lstDGzyxmcsVo[i].CFXMWYH.ToString().Trim() + "\r\n YYXMBM" + lstDGzyxmcsVo[i].YYXMBM.ToString().Trim() + "\r\n XMMC" + lstDGzyxmcsVo[i].XMMC.ToString().Trim() + "\r\n YYFLDM" + lstDGzyxmcsVo[i].YYFLDM.ToString().Trim() + "\r\n YPGG" + lstDGzyxmcsVo[i].YPGG.ToString().Trim() + "\r\n YPJX" + lstDGzyxmcsVo[i].YPJX.ToString().Trim() + "\r\n JG" + lstDGzyxmcsVo[i].JG.ToString("0.0000") + "\r\n MCYL" + lstDGzyxmcsVo[i].MCYL.ToString("0.00") + "\r\n JE" + lstDGzyxmcsVo[i].JE.ToString("0.00") + "\r\n ZFEIBL" + lstDGzyxmcsVo[i].ZFEIBL.ToString("0.00") + "\r\n ZFEIJE" + lstDGzyxmcsVo[i].ZFEIJE.ToString("0.00") + "\r\n FHXZBZ" + lstDGzyxmcsVo[i].FHXZBZ.ToString().Trim() + "\r\n JZSJ" + lstDGzyxmcsVo[i].JZSJ.ToString().Trim() + "\r\n YSGH" + lstDGzyxmcsVo[i].YSGH.ToString().Trim() + "\r\n BZ3" + lstDGzyxmcsVo[i].BZ3.ToString().Trim() + "\r\n CBDTCQBM" + lstDGzyxmcsVo[i].CBDTCQBM);
                //}
                #endregion
                intRet = EndDataSet(intH, "ZYCFXMDR");
                intRet = Run(intH);
                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "CGBZ", sbValue, 1024);
                    string strCGBZ = sbValue.ToString().Trim();//原因
                    if (strCGBZ == "成功" || strCGBZ == "1")
                    {
                        //MessageBox.Show("住院明细数据上传成功！");//在form提示
                        lngRes = 1;
                    }
                    else
                    {
                        ExceptionLog.OutPutException(strCGBZ);
                        //MessageBox.Show(strCGBZ, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    //MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院记帐处方项目删除[SP3_3003]
        /// <summary>
        /// 住院记帐处方项目删除[SP3_3002]
        /// </summary>
        /// <param name="lstDGzyxmcsVo"></param>
        /// <param name="objDgextraVo"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static long m_lngFunSP3003(List<clsDGZyxmcs_VO> lstDGzyxmcsVo, clsDGExtra_VO objDgextraVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3003");
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);//医院编号
                intRet = SetParam(intH, "JZJLH", objDgextraVo.JZJLH);//就诊记录号
                intRet = SetParam(intH, "ZYH", objDgextraVo.ZYH);//住院号
                intRet = SetParam(intH, "SCFS", "1");//删除方式 1、按“就诊记录号”删除 2、按“处方项目唯一号”删除
                intRet = SetParam(intH, "CFXMWYH", "");//处方项目唯一号 “删除方式”为按“处方项目唯一号”删除时，不可为空，唯一标识一条处方，同一个“就诊记录号”下不可重复
                intRet = SetParam(intH, "CBDTCQBM", objDgextraVo.CBDTCQBM);
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);//参保地统筹区编码

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3003"));
                sb.AppendLine(string.Format("YYBH := {0}", objDgextraVo.YYBH));
                sb.AppendLine(string.Format("JZJLH := {0}", objDgextraVo.JZJLH));
                sb.AppendLine(string.Format("ZYH := {0}", objDgextraVo.ZYH));
                sb.AppendLine(string.Format("SCFS := {0}", "1"));
                sb.AppendLine(string.Format("CFXMWYH := {0}", ""));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgextraVo.CBDTCQBM));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine("********** 下面为返回信息 **********");
                #endregion

                intRet = Run(intH);
                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    strValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "CGBZ", strValue, 1024);
                    string strCGBZ = strValue.ToString().Trim();//原因
                    if (string.IsNullOrEmpty(strCGBZ))
                    {
                        lngRes = 1;
                    }
                    else
                    {
                        lngRes = -1;
                    }
                    sb.AppendLine(string.Format("CGBZ := {0}", strCGBZ));
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    sb.AppendLine(string.Format("MSG := {0}", strValue.ToString()));
                }
                // log
                Log.Output(sb.ToString());
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院结算[SP3_3004]
        /// <summary>
        /// 住院结算[SP3_3004]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgzyjsVo"></param>
        /// <param name="objDgextraVo"></param>
        /// <param name="objDgzyjsfhVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP3004(clsDGZyjs_VO objDgzyjsVo, clsDGExtra_VO objDgextraVo, out clsDGZyjsfh_VO objDgzyjsfhVo)
        {
            long lngRes = -1;
            objDgzyjsfhVo = new clsDGZyjsfh_VO();
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3004");
                //intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);

                intRet = SetParam(intH, "JZJLH", objDgzyjsVo.JZJLH);
                intRet = SetParam(intH, "GMSFHM", objDgzyjsVo.GMSFHM);
                intRet = SetParam(intH, "JZLB", objDgzyjsVo.JZLB);
                intRet = SetParam(intH, "JSLX", objDgzyjsVo.JSLX);
                intRet = SetParam(intH, "CYKS", objDgzyjsVo.CYKS);
                intRet = SetParam(intH, "YYCYKS", objDgzyjsVo.YYCYKS);
                intRet = SetParam(intH, "JDZFBL", objDgzyjsVo.JDZFBL.ToString("0.00"));

                intRet = SetParam(intH, "CWH", objDgzyjsVo.CWH);
                intRet = SetParam(intH, "CYZD", objDgzyjsVo.CYZD);
                intRet = SetParam(intH, "JSQSRQ", objDgzyjsVo.JSQSRQ);
                intRet = SetParam(intH, "JSZZRQ", objDgzyjsVo.JSZZRQ);
                intRet = SetParam(intH, "JSTS", objDgzyjsVo.JSTS.ToString());//结算终止日期  –结算起始日期  = 0 则结算天数取1，否则：结算天数取结算终止日期 – 结算起始日期
                intRet = SetParam(intH, "CYRQ", objDgzyjsVo.CYRQ);
                intRet = SetParam(intH, "FPHM", objDgzyjsVo.FPHM);
                intRet = SetParam(intH, "ZDZMHM", objDgzyjsVo.ZDZMHM);
                intRet = SetParam(intH, "LXDH", objDgzyjsVo.LXDH);
                intRet = SetParam(intH, "BZ", objDgzyjsVo.BZ);
                intRet = SetParam(intH, "ZYFYZE", objDgzyjsVo.ZYFYZE.ToString("0.00"));
                intRet = SetParam(intH, "CBDTCQBM", objDgzyjsVo.CBDTCQBM);
                intRet = SetParam(intH, "CYYY", objDgzyjsVo.CYYY);
                intRet = SetParam(intH, "JBR", objDgzyjsVo.JBR);
                intRet = SetParam(intH, "JSDYZDBY", objDgzyjsVo.JSDYZDBY);

                #region Log-up
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("FN" + ":= " + "SP3_3004");
                sb.AppendLine("YYBH" + ":= " + objDgextraVo.YYBH);
                sb.AppendLine("JZJLH" + ":= " + objDgzyjsVo.JZJLH);
                sb.AppendLine("GMSFHM" + ":= " + objDgzyjsVo.GMSFHM);
                sb.AppendLine("JZLB" + ":= " + objDgzyjsVo.JZLB);
                sb.AppendLine("JSLX" + ":= " + objDgzyjsVo.JSLX);
                sb.AppendLine("CYKS" + ":= " + objDgzyjsVo.CYKS);
                sb.AppendLine("YYCYKS" + ":= " + objDgzyjsVo.YYCYKS);
                sb.AppendLine("JDZFBL" + ":= " + objDgzyjsVo.JDZFBL.ToString("0.00"));
                sb.AppendLine("CWH" + ":= " + objDgzyjsVo.CWH);
                sb.AppendLine("CYZD" + ":= " + objDgzyjsVo.CYZD);
                sb.AppendLine("JSQSRQ" + ":= " + objDgzyjsVo.JSQSRQ);
                sb.AppendLine("JSZZRQ" + ":= " + objDgzyjsVo.JSZZRQ);
                sb.AppendLine("JSTS" + ":= " + objDgzyjsVo.JSTS.ToString());
                sb.AppendLine("CYRQ" + ":= " + objDgzyjsVo.CYRQ);
                sb.AppendLine("FPHM" + ":= " + objDgzyjsVo.FPHM);
                sb.AppendLine("ZDZMHM" + ":= " + objDgzyjsVo.ZDZMHM);
                sb.AppendLine("LXDH" + ":= " + objDgzyjsVo.LXDH);
                sb.AppendLine("BZ" + ":= " + objDgzyjsVo.BZ);
                sb.AppendLine("ZYFYZE" + ":= " + objDgzyjsVo.ZYFYZE.ToString("0.00"));
                sb.AppendLine("CBDTCQBM" + ":= " + objDgzyjsVo.CBDTCQBM);
                sb.AppendLine("CYYY" + ":= " + objDgzyjsVo.CYYY);
                sb.AppendLine("JBR" + ":= " + objDgzyjsVo.JBR);
                sb.AppendLine("JSDYZDBY" + ":= " + objDgzyjsVo.JSDYZDBY);
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);

                Log.Output("返回value := " + strValue.ToString() + Environment.NewLine + "如果value=1, 以下为结果明细：");

                if (strValue.ToString() == "1")
                {
                    #region 医保结算返回结果
                    objDgzyjsfhVo = new clsDGZyjsfh_VO();
                    intRet = GetParam(intH, "JZJLH", strValue, 1024);//就诊记录号
                    objDgzyjsfhVo.JZJLH = strValue.ToString();
                    intRet = GetParam(intH, "SDYWH", strValue, 1024);//结算序号,系列号 入参：结算类型为预结算时此出参为空
                    objDgzyjsfhVo.SDYWH = strValue.ToString();
                    intRet = GetParam(intH, "ZYFYZE", strValue, 1024);
                    decimal decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.ZYFYZE = decTemp;
                    intRet = GetParam(intH, "SBZFJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.SBZFJE = decTemp;
                    intRet = GetParam(intH, "JBYLTCZF", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.JBYLTCZF = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.BCYLTCZF = decTemp;
                    intRet = GetParam(intH, "YLBZ", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.YLBZ = decTemp;
                    intRet = GetParam(intH, "JZJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.JZJE = decTemp;
                    intRet = GetParam(intH, "DBYLJZJ", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.DBYLJZJ = decTemp;
                    intRet = GetParam(intH, "GRZFEIJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.GRZFEIJE = decTemp;
                    intRet = GetParam(intH, "ZFEIYY", strValue, 1024);
                    objDgzyjsfhVo.ZFEIYY = strValue.ToString();
                    intRet = GetParam(intH, "JSLX", strValue, 1024);
                    objDgzyjsfhVo.JSLX = strValue.ToString();
                    intRet = GetParam(intH, "MZYFBXJE", strValue, 1024);//民政优抚报销金额 【预留字段】
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.MZYFBXJE = decTemp;
                    intRet = GetParam(intH, "QTZHIFU", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.QTZHIFU = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF1", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.BCYLTCZF1 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF2", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.BCYLTCZF2 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF3", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.BCYLTCZF3 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF4", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.BCYLTCZF4 = decTemp;
                    intRet = GetParam(intH, "YBJZFPJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsfhVo.YBJZFPJE = decTemp;
                    #endregion
                    lngRes = 1;

                    #region log -- result

                    sb = new StringBuilder();
                    sb.AppendLine("JZJLH" + ":= " + objDgzyjsfhVo.JZJLH);
                    sb.AppendLine("SDYWH" + ":= " + objDgzyjsfhVo.SDYWH);
                    sb.AppendLine("ZYFYZE" + ":= " + objDgzyjsfhVo.ZYFYZE);
                    sb.AppendLine("SBZFJE" + ":= " + objDgzyjsfhVo.SBZFJE);
                    sb.AppendLine("JBYLTCZF" + ":= " + objDgzyjsfhVo.JBYLTCZF);
                    sb.AppendLine("BCYLTCZF" + ":= " + objDgzyjsfhVo.BCYLTCZF);
                    sb.AppendLine("YLBZ" + ":= " + objDgzyjsfhVo.YLBZ);
                    sb.AppendLine("JZJE" + ":= " + objDgzyjsfhVo.JZJE);
                    sb.AppendLine("DBYLJZJ" + ":= " + objDgzyjsfhVo.DBYLJZJ);
                    sb.AppendLine("GRZFEIJE" + ":= " + objDgzyjsfhVo.GRZFEIJE);
                    sb.AppendLine("ZFEIYY" + ":= " + objDgzyjsfhVo.ZFEIYY);
                    sb.AppendLine("JSLX" + ":= " + objDgzyjsfhVo.JSLX);
                    sb.AppendLine("MZYFBXJE" + ":= " + objDgzyjsfhVo.MZYFBXJE);
                    sb.AppendLine("QTZHIFU" + ":= " + objDgzyjsfhVo.QTZHIFU);
                    sb.AppendLine("BCYLTCZF1" + ":= " + objDgzyjsfhVo.BCYLTCZF1);
                    sb.AppendLine("BCYLTCZF2" + ":= " + objDgzyjsfhVo.BCYLTCZF2);
                    sb.AppendLine("BCYLTCZF3" + ":= " + objDgzyjsfhVo.BCYLTCZF3);
                    sb.AppendLine("BCYLTCZF4" + ":= " + objDgzyjsfhVo.BCYLTCZF4);
                    sb.AppendLine("YBJZFPJE" + ":= " + objDgzyjsfhVo.YBJZFPJE);
                    Log.Output(sb.ToString());
                    #endregion
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    lngRes = -1;
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 出院登记[SP3_3005]
        /// <summary>
        /// 出院登记[SP3_3005]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgzycydjVo"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP3005(clsDGZycydj_VO objDgzycydjVo, clsDGExtra_VO objDgextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                if (!string.IsNullOrEmpty(objDgzycydjVo.CYZD))
                {
                    if (objDgzycydjVo.CYZD.Length > 50) objDgzycydjVo.CYZD = objDgzycydjVo.CYZD.Substring(0, 48);
                }

                intRet = SetParam(intH, "FN", "SP3_3005");
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);

                intRet = SetParam(intH, "JZJLH", objDgzycydjVo.JZJLH);
                intRet = SetParam(intH, "GMSFHM", objDgzycydjVo.GMSFHM);
                intRet = SetParam(intH, "JZLB", objDgzycydjVo.JZLB);
                intRet = SetParam(intH, "CYKS", objDgzycydjVo.CYKS);
                intRet = SetParam(intH, "YYCYKS", objDgzycydjVo.YYCYKS);
                intRet = SetParam(intH, "CWH", objDgzycydjVo.CWH);
                intRet = SetParam(intH, "CYZD", objDgzycydjVo.CYZD);
                intRet = SetParam(intH, "CYRQ", objDgzycydjVo.CYRQ);
                intRet = SetParam(intH, "ZYTS", objDgzycydjVo.ZYTS.ToString());//出院日期 – 入院日期 = 0 则住院天数取1，否则：住院天数取出院日期 – 入院日期
                intRet = SetParam(intH, "LXDH", objDgzycydjVo.LXDH);
                intRet = SetParam(intH, "BZ", objDgzycydjVo.BZ);
                intRet = SetParam(intH, "CBDTCQBM", objDgzycydjVo.CBDTCQBM);
                intRet = SetParam(intH, "CYYY", objDgzycydjVo.CYYY);
                intRet = SetParam(intH, "CYZDICD1", objDgzycydjVo.Icd10_1);
                intRet = SetParam(intH, "CYZDICD2", objDgzycydjVo.Icd10_2);
                intRet = SetParam(intH, "CYZDICD3", objDgzycydjVo.Icd10_3);

                #region Log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3005"));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("YYBH := {0}", objDgextraVo.YYBH));
                sb.AppendLine(string.Format("JZJLH := {0}", objDgzycydjVo.JZJLH));
                sb.AppendLine(string.Format("GMSFHM := {0}", objDgzycydjVo.GMSFHM));
                sb.AppendLine(string.Format("JZLB := {0}", objDgzycydjVo.JZLB));
                sb.AppendLine(string.Format("CYKS := {0}", objDgzycydjVo.CYKS));
                sb.AppendLine(string.Format("YYCYKS := {0}", objDgzycydjVo.YYCYKS));
                sb.AppendLine(string.Format("CWH := {0}", objDgzycydjVo.CWH));
                sb.AppendLine(string.Format("CYZD := {0}", objDgzycydjVo.CYZD));
                sb.AppendLine(string.Format("CYRQ := {0}", objDgzycydjVo.CYRQ));
                sb.AppendLine(string.Format("ZYTS := {0}", objDgzycydjVo.ZYTS.ToString()));
                sb.AppendLine(string.Format("LXDH := {0}", objDgzycydjVo.LXDH));
                sb.AppendLine(string.Format("BZ := {0}", objDgzycydjVo.BZ));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgzycydjVo.CBDTCQBM));
                sb.AppendLine(string.Format("CYYY := {0}", objDgzycydjVo.CYYY));
                sb.AppendLine(string.Format("CYZDICD1 := {0}", objDgzycydjVo.Icd10_1));
                sb.AppendLine(string.Format("CYZDICD2 := {0}", objDgzycydjVo.Icd10_2));
                sb.AppendLine(string.Format("CYZDICD3 := {0}", objDgzycydjVo.Icd10_3));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 结算详细信息查询[SP3_3007]
        /// <summary>
        /// 结算详细信息查询[SP3_3007]
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <param name="objDgzyjsxxxxVo"></param>
        /// <param name="objDgzyjsGRZFXMMXVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP3007(clsDGExtra_VO objDgextraVo, out clsDGZYjsxxxx objDgzyjsxxxxVo, out List<clsDGZYjsGRZFXMMX_VO> lstDgzyjsGRZFXMMXVo)
        {
            long lngRes = -1;
            objDgzyjsxxxxVo = new clsDGZYjsxxxx();
            lstDgzyjsGRZFXMMXVo = new List<clsDGZYjsGRZFXMMX_VO>();
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3007");
                intRet = SetParam(intH, "JZJLH", objDgextraVo.JZJLH);
                intRet = SetParam(intH, "SDYWH", objDgextraVo.SDYWH);
                intRet = SetParam(intH, "CBDTCQBM", objDgextraVo.CBDTCQBM);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    #region 查询返回结果
                    objDgzyjsxxxxVo = new clsDGZYjsxxxx();
                    intRet = GetParam(intH, "YYMC", strValue, 1024);
                    objDgzyjsxxxxVo.YYMC = strValue.ToString();
                    intRet = GetParam(intH, "SDYWH", strValue, 1024);//结算序号
                    objDgzyjsxxxxVo.SDYWH = strValue.ToString();
                    intRet = GetParam(intH, "JZJLH", strValue, 1024);//就诊记录号
                    objDgzyjsxxxxVo.JZJLH = strValue.ToString();
                    intRet = GetParam(intH, "JZLB", strValue, 1024);//就诊类别
                    objDgzyjsxxxxVo.JZLB = strValue.ToString();
                    intRet = GetParam(intH, "YYMC", strValue, 1024);
                    objDgzyjsxxxxVo.YYMC = strValue.ToString();
                    intRet = GetParam(intH, "DYRQ", strValue, 1024);
                    objDgzyjsxxxxVo.DYRQ = strValue.ToString();
                    intRet = GetParam(intH, "ZJHM", strValue, 1024);
                    objDgzyjsxxxxVo.ZJHM = strValue.ToString();
                    intRet = GetParam(intH, "XM", strValue, 1024);
                    objDgzyjsxxxxVo.XM = strValue.ToString();
                    intRet = GetParam(intH, "SBJSJE", strValue, 1024);
                    decimal decTemp = 0;
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.SBJSJE = decTemp;
                    intRet = GetParam(intH, "YLZFY", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.YLZFY = decTemp;
                    intRet = GetParam(intH, "ZFEIXMJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.ZFEIXMJE = decTemp;
                    intRet = GetParam(intH, "CZFEIXMJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.CZFEIXMJE = decTemp;
                    intRet = GetParam(intH, "QFJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.QFJE = decTemp;
                    intRet = GetParam(intH, "FDGRZFEI", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.FDGRZFEI = decTemp;
                    intRet = GetParam(intH, "CXEZFEI", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.CXEZFEI = decTemp;
                    intRet = GetParam(intH, "YLBZ", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.YLBZ = decTemp;
                    intRet = GetParam(intH, "JZJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.JZJE = decTemp;
                    intRet = GetParam(intH, "JDBXBL", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.JDBXBL = decTemp;
                    intRet = GetParam(intH, "JBJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.JBJE = decTemp;
                    intRet = GetParam(intH, "JBFDZFEIJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.JBFDZFEIJE = decTemp;
                    intRet = GetParam(intH, "MZYFBXJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.MZYFBXJE = decTemp;
                    intRet = GetParam(intH, "DBYLJZJ", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.DBYLJZJ = decTemp;
                    intRet = GetParam(intH, "CDBJZJ", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.CDBJZJ = decTemp;
                    intRet = GetParam(intH, "GRSJZFJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.GRSJZFJE = decTemp;
                    intRet = GetParam(intH, "DBGRZFJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.DBGRZFJE = decTemp;
                    intRet = GetParam(intH, "DBWGJBJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.DBWGJBJE = decTemp;
                    intRet = GetParam(intH, "DBCZFEIJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.DBCZFEIJE = decTemp;
                    intRet = GetParam(intH, "DBQFJE", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.DBQFJE = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF1", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.BCYLTCZF1 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF2", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.BCYLTCZF2 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF3", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.BCYLTCZF3 = decTemp;
                    intRet = GetParam(intH, "BCYLTCZF4", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.BCYLTCZF4 = decTemp;
                    intRet = GetParam(intH, "QTZHIFU", strValue, 1024);
                    decimal.TryParse(strValue.ToString(), out decTemp);
                    objDgzyjsxxxxVo.QTZHIFU = decTemp;
                    intRet = GetParam(intH, "JSJBR", strValue, 1024);
                    objDgzyjsxxxxVo.JSJBR = strValue.ToString();
                    #endregion

                    #region 返回结果集GRZFXMMX
                    intRet = LocateDataSet(intH, "GRZFXMMX");//定位结果集
                    int intRowCount = GetRowSize(intH);
                    if (intRowCount <= 0)
                    {
                        DestroyInstance(intH);
                        return -1;//查询结果集为空
                    }
                    StringBuilder sbValue = new StringBuilder(1024);
                    lstDgzyjsGRZFXMMXVo = new List<clsDGZYjsGRZFXMMX_VO>();
                    clsDGZYjsGRZFXMMX_VO objDgzyjsGRZFXMMXVo = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objDgzyjsGRZFXMMXVo = new clsDGZYjsGRZFXMMX_VO();
                        try
                        {
                            intRet = GetFieldValue(intH, "CFXMWYH", sbValue, 1024);
                            objDgzyjsGRZFXMMXVo.CFXMWYH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "XMXH", sbValue, 1024);
                            objDgzyjsGRZFXMMXVo.XMXH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYXMBM", sbValue, 1024);
                            objDgzyjsGRZFXMMXVo.YYXMBM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYXMMC", sbValue, 1024);
                            objDgzyjsGRZFXMMXVo.YYXMMC = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JG", sbValue, 1024);
                            decTemp = 0;
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.JG = decTemp;
                            intRet = GetFieldValue(intH, "ZGXJ", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.ZGXJ = decTemp;
                            intRet = GetFieldValue(intH, "SL", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.SL = decTemp;
                            intRet = GetFieldValue(intH, "ZFEIBL", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.ZFEIBL = decTemp;
                            intRet = GetFieldValue(intH, "KBJE", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.KBJE = decTemp;
                            intRet = GetFieldValue(intH, "ZFEIJE", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.ZFEIJE = decTemp;
                            intRet = GetFieldValue(intH, "CZFEIBL", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgzyjsGRZFXMMXVo.CZFEIBL = decTemp;
                            intRet = GetFieldValue(intH, "SYZBZ", sbValue, 1024);
                            objDgzyjsGRZFXMMXVo.SYZBZ = sbValue.ToString().Trim();
                            lstDgzyjsGRZFXMMXVo.Add(objDgzyjsGRZFXMMXVo);
                            intRet = NextRow(intH);
                        }
                        catch (Exception objEx)
                        {
                            MessageBox.Show("读取结算返回数据集出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            lngRes = -1;
                        }

                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 取消费用结算[SP3_3008]
        /// <summary>
        /// 取消费用结算[SP3_3008]
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP3008(clsDGExtra_VO objDgextraVo, ref StringBuilder strValue)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3008");
                intRet = SetParam(intH, "JZJLH", objDgextraVo.JZJLH);
                intRet = SetParam(intH, "SDYWH", objDgextraVo.SDYWH);
                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);
                intRet = SetParam(intH, "CBDTCQBM", objDgextraVo.CBDTCQBM);

                #region Log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_3008"));
                sb.AppendLine(string.Format("JZJLH := {0}", objDgextraVo.JZJLH));
                sb.AppendLine(string.Format("SDYWH := {0}", objDgextraVo.SDYWH));
                sb.AppendLine(string.Format("YYBH := {0}", objDgextraVo.YYBH));
                sb.AppendLine(string.Format("JBR := {0}", objDgextraVo.JBR));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", objDgextraVo.CBDTCQBM));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (Convert.ToInt32(strValue.ToString()) == 1)
                {
                    lngRes = 1;//取消结算成功
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 医院对数查询[SP3_1208]
        /// <summary>
        ///  医院对数查询[SP3_1208]
        /// </summary>
        /// <param name="objDgybdscxVo"></param>
        /// <param name="objDgybdscxfhVo"></param>
        /// <param name="lstDgybdscxYYDSXXVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP1208(clsDGYBDscx_VO objDgybdscxVo, out clsDGYBDscxfh_VO objDgybdscxfhVo, out List<clsDGYBDscxYYDSXX_VO> lstDgybdscxYYDSXXVo)
        {
            long lngRes = -1;
            objDgybdscxfhVo = new clsDGYBDscxfh_VO();
            lstDgybdscxYYDSXXVo = new List<clsDGYBDscxYYDSXX_VO>();
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_1208");

                intRet = SetParam(intH, "YYBH", objDgybdscxVo.YYBH);
                intRet = SetParam(intH, "SFYBH", objDgybdscxVo.SFYBH);//此字段是否指当时做结算时的操作人 和JBR是否有区别？
                intRet = SetParam(intH, "YWLB", objDgybdscxVo.YWLB);
                intRet = SetParam(intH, "KSRQ", objDgybdscxVo.KSRQ);
                intRet = SetParam(intH, "ZZRQ", objDgybdscxVo.ZZRQ);
                intRet = SetParam(intH, "JBR", objDgybdscxVo.SFYBH);//此字段应该是指当前操作人，目前传值和SFYBH一样

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    #region 返回结果
                    intRet = GetParam(intH, "ZYZJE_HJ", strValue, 1024);//住院总金额
                    objDgybdscxfhVo.ZYZJE_HJ = strValue.ToString();
                    intRet = GetParam(intH, "ZRS", strValue, 1024);//总人数
                    objDgybdscxfhVo.ZRS = strValue.ToString();
                    intRet = GetParam(intH, "SBZFJE_HJ", strValue, 1024);//社保支付金额合计
                    objDgybdscxfhVo.SBZFJE_HJ = strValue.ToString();
                    intRet = GetParam(intH, "YLBZ_HJ", strValue, 1024);//医疗补助合计
                    objDgybdscxfhVo.YLBZ_HJ = strValue.ToString();
                    intRet = GetParam(intH, "JZJE_HJ", strValue, 1024);//记账金额合计
                    objDgybdscxfhVo.JZJE_HJ = strValue.ToString();
                    intRet = GetParam(intH, "DBYLJZJ_HJ", strValue, 1024);//低保医疗救助金合计
                    objDgybdscxfhVo.DBYLJZJ_HJ = strValue.ToString();
                    intRet = GetParam(intH, "GRZFEIJE_HJ", strValue, 1024);//个人自费金额合计
                    objDgybdscxfhVo.GRZFEIJE_HJ = strValue.ToString();
                    intRet = GetParam(intH, "MZYFBXJE_HJ", strValue, 1024);//民政优抚报销金额合计
                    objDgybdscxfhVo.MZYFBXJE_HJ = strValue.ToString();
                    #endregion

                    #region 返回结果集YYDSXX
                    intRet = LocateDataSet(intH, "YYDSXX");//定位结果集
                    int intRowCount = GetRowSize(intH);
                    if (intRowCount <= 0)
                    {
                        DestroyInstance(intH);
                        return -1;//查询结果集为空
                    }
                    StringBuilder sbValue = new StringBuilder(1024);
                    clsDGYBDscxYYDSXX_VO objDgybdscxYYDSXXVo = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objDgybdscxYYDSXXVo = new clsDGYBDscxYYDSXX_VO();
                        try
                        {
                            intRet = GetFieldValue(intH, "GMSFHM", sbValue, 1024);
                            objDgybdscxYYDSXXVo.GMSFHM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                            objDgybdscxYYDSXXVo.XM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSRQ", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JSRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZJLH", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JZJLH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSYWH", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JSYWH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YWLB", sbValue, 1024);
                            objDgybdscxYYDSXXVo.YWLB = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZLB", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JZLB = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYZJE", sbValue, 1024);
                            objDgybdscxYYDSXXVo.ZYZJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "SBZFJE", sbValue, 1024);
                            objDgybdscxYYDSXXVo.SBZFJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YLBZ", sbValue, 1024);
                            objDgybdscxYYDSXXVo.YLBZ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZJE", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JZJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "DBYLJZJ", sbValue, 1024);
                            objDgybdscxYYDSXXVo.DBYLJZJ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "GRZFEIJE", sbValue, 1024);
                            objDgybdscxYYDSXXVo.GRZFEIJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZFEIYY", sbValue, 1024);
                            objDgybdscxYYDSXXVo.ZFEIYY = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "FPHM", sbValue, 1024);
                            objDgybdscxYYDSXXVo.FPHM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSJKLX", sbValue, 1024);
                            objDgybdscxYYDSXXVo.JSJKLX = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "MZYFBXJE", sbValue, 1024);
                            objDgybdscxYYDSXXVo.MZYFBXJE = sbValue.ToString().Trim();
                            decimal decTemp = 0;
                            intRet = GetFieldValue(intH, "BCYLTCZF1", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgybdscxYYDSXXVo.BCYLTCZF1 = decTemp;
                            intRet = GetFieldValue(intH, "BCYLTCZF2", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgybdscxYYDSXXVo.BCYLTCZF2 = decTemp;
                            intRet = GetFieldValue(intH, "BCYLTCZF3", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgybdscxYYDSXXVo.BCYLTCZF3 = decTemp;
                            intRet = GetFieldValue(intH, "BCYLTCZF4", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgybdscxYYDSXXVo.BCYLTCZF4 = decTemp;
                            intRet = GetFieldValue(intH, "QTZHIFU", sbValue, 1024);
                            decimal.TryParse(sbValue.ToString(), out decTemp);
                            objDgybdscxYYDSXXVo.QTZHIFU = decTemp;
                            lstDgybdscxYYDSXXVo.Add(objDgybdscxYYDSXXVo);
                            intRet = NextRow(intH);
                        }
                        catch (Exception objEx)
                        {
                            ExceptionLog.OutPutException("读取对数返回数据集出错:" + objEx.Message);
                            MessageBox.Show("读取对数返回数据集出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            lngRes = -1;
                        }

                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 查询医保病人信息[SP3_1201]
        /// <summary>
        /// 查询医保病人信息[SP3_1201]
        /// </summary>
        /// <param name="m_objItem"></param>
        /// <param name="m_objPatientInfo"></param>
        /// <param name="m_objJXzlxx"></param>
        /// <param name="m_objYDryxx"></param>
        /// <param name="m_objZYxx"></param>
        /// <param name="m_objZJzyxx"></param>
        /// <returns></returns>
        public static long m_lngFunSP1201(clsDGZydj_VO m_objItem, out clsDGPaitentInfo_VO m_objPatientInfo, out List<clsDGJxzlxx_VO> m_objJXzlxx, out List<clsDGYdryxx_VO> m_objYDryxx, out List<clsDGZyxx_VO> m_objZYxx, out List<clsDGZjzyxx_VO> m_objZJzyxx)
        {
            long lngRes = 0;
            m_objPatientInfo = new clsDGPaitentInfo_VO();
            m_objJXzlxx = new List<clsDGJxzlxx_VO>();
            m_objYDryxx = new List<clsDGYdryxx_VO>();
            m_objZYxx = new List<clsDGZyxx_VO>();
            m_objZJzyxx = new List<clsDGZjzyxx_VO>();
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_1201");
                intRet = SetParam(intH, "GMSFHM", m_objItem.GMSFHM);
                intRet = SetParam(intH, "YYBH", m_objItem.YYBH);
                intRet = SetParam(intH, "CBDTCQBM", m_objItem.CBDTCQBM);
                intRet = SetParam(intH, "JBR", m_objItem.LXDH);
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Trim() == "1")
                {
                    #region 返回结果
                    intRet = GetParam(intH, "XM", strValue, 1024);
                    m_objPatientInfo.XM = strValue.ToString();
                    intRet = GetParam(intH, "XB", strValue, 1024);
                    m_objPatientInfo.XB = strValue.ToString();
                    intRet = GetParam(intH, "CSNY", strValue, 1024);
                    m_objPatientInfo.CSNY = strValue.ToString();
                    intRet = GetParam(intH, "XTJGDM", strValue, 1024);
                    m_objPatientInfo.XTJGDM = strValue.ToString();
                    intRet = GetParam(intH, "JSFS", strValue, 1024);
                    m_objPatientInfo.JSFS = strValue.ToString();
                    intRet = GetParam(intH, "RYLB", strValue, 1024);
                    m_objPatientInfo.RYLB = strValue.ToString();
                    intRet = GetParam(intH, "YLDYLX", strValue, 1024);
                    m_objPatientInfo.YLDYLX = strValue.ToString();
                    intRet = GetParam(intH, "GWYLB", strValue, 1024);
                    m_objPatientInfo.GWYLB = strValue.ToString();
                    intRet = GetParam(intH, "ZZMC", strValue, 1024);
                    m_objPatientInfo.ZZMC = strValue.ToString();
                    intRet = GetParam(intH, "LXDH", strValue, 1024);
                    m_objPatientInfo.LXDH = strValue.ToString();
                    intRet = GetParam(intH, "JBYILCBFS", strValue, 1024);
                    m_objPatientInfo.JBYILCBFS = strValue.ToString();
                    intRet = GetParam(intH, "JBYILCBSJ", strValue, 1024);
                    m_objPatientInfo.JBYILCBSJ = strValue.ToString();
                    intRet = GetParam(intH, "JBYILCBZT", strValue, 1024);
                    m_objPatientInfo.JBYILCBZT = strValue.ToString();
                    intRet = GetParam(intH, "JBYILKBYE", strValue, 1024);
                    m_objPatientInfo.JBYILKBYE = strValue.ToString();
                    intRet = GetParam(intH, "BCYILCBZT", strValue, 1024);
                    m_objPatientInfo.BCYILCBZT = strValue.ToString();
                    intRet = GetParam(intH, "BCYILKBYE", strValue, 1024);
                    m_objPatientInfo.BCYILKBYE = strValue.ToString();
                    intRet = GetParam(intH, "GSCBZT", strValue, 1024);
                    m_objPatientInfo.GSCBZT = strValue.ToString();
                    intRet = GetParam(intH, "GWYBZLB", strValue, 1024);
                    m_objPatientInfo.GWYBZLB = strValue.ToString();
                    intRet = GetParam(intH, "ZFYY", strValue, 1024);
                    m_objPatientInfo.ZFYY = strValue.ToString();
                    intRet = GetParam(intH, "GSBXXSBZ", strValue, 1024);
                    m_objPatientInfo.GSBXXSBZ = strValue.ToString();
                    intRet = GetParam(intH, "RQLB", strValue, 1024);
                    m_objPatientInfo.RQLB = strValue.ToString();
                    intRet = GetParam(intH, "GWYBZ", strValue, 1024);
                    m_objPatientInfo.GWYBZ = strValue.ToString();
                    intRet = GetParam(intH, "MZYLJZDX", strValue, 1024);
                    m_objPatientInfo.MZYLJZDX = strValue.ToString();
                    intRet = GetParam(intH, "SBBZ", strValue, 1024);
                    m_objPatientInfo.SBBZ = strValue.ToString();
                    intRet = GetParam(intH, "BCYILCBZT1", strValue, 1024);
                    m_objPatientInfo.BCYILCBZT1 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILCBZT2", strValue, 1024);
                    m_objPatientInfo.BCYILCBZT2 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILCBZT3", strValue, 1024);
                    m_objPatientInfo.BCYILCBZT3 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILCBZT4", strValue, 1024);
                    m_objPatientInfo.BCYILCBZT4 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILKBYE1", strValue, 1024);
                    m_objPatientInfo.BCYILKBYE1 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILKBYE2", strValue, 1024);
                    m_objPatientInfo.BCYILKBYE2 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILKBYE3", strValue, 1024);
                    m_objPatientInfo.BCYILKBYE3 = strValue.ToString();
                    intRet = GetParam(intH, "BCYILKBYE4", strValue, 1024);
                    m_objPatientInfo.BCYILKBYE4 = strValue.ToString();
                    #endregion

                    #region 返回结果集 继续诊疗信息 JXZLDJXX
                    intRet = LocateDataSet(intH, "JXZLDJXX");
                    int intRowCount = GetRowSize(intH);
                    StringBuilder sbValue = new StringBuilder(1024);
                    clsDGJxzlxx_VO objJXzlxx = null;
                    if (intRowCount > 0)
                    {
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objJXzlxx = new clsDGJxzlxx_VO();
                            try
                            {
                                intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                                objJXzlxx.XM = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SPYWH", sbValue, 1024);
                                objJXzlxx.SPYWH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYZDMC", sbValue, 1024);
                                objJXzlxx.ZYZDMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SCJZYYMC", sbValue, 1024);
                                objJXzlxx.SCJZYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SCJZKSSJ", sbValue, 1024);
                                objJXzlxx.SCJZKSSJ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SCJZZZSJ", sbValue, 1024);
                                objJXzlxx.SCJZZZSJ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                                objJXzlxx.JXZLYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JXZLKSSJ", sbValue, 1024);
                                objJXzlxx.JXZLKSSJ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JXZLZZSJ", sbValue, 1024);
                                objJXzlxx.JXZLZZSJ = sbValue.ToString().Trim();
                                m_objJXzlxx.Add(objJXzlxx);
                                intRet = NextRow(intH);
                            }
                            catch (Exception objEx)
                            {
                                ExceptionLog.OutPutException("参保病人基本资料查询中给继续诊疗信息VO赋值出错：" + objEx.Message);
                                MessageBox.Show("参保病人基本资料查询中给继续诊疗信息VO赋值出错：" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                lngRes = -1;
                            }
                        }

                    }
                    #endregion

                    #region 异地人员信息 结果集 YDRYDJXX
                    intRet = LocateDataSet(intH, "YDRYDJXX");
                    intRowCount = GetRowSize(intH);
                    clsDGYdryxx_VO objYDryxx = null;
                    if (intRowCount > 0)
                    {
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objYDryxx = new clsDGYdryxx_VO();
                            try
                            {
                                intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                                objYDryxx.XM = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SPYWH", sbValue, 1024);
                                objYDryxx.SPYWH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDYJYYBH", sbValue, 1024);
                                objYDryxx.XDYJYYBH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDYJYYMC", sbValue, 1024);
                                objYDryxx.XDYJYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDEJYYBH", sbValue, 1024);
                                objYDryxx.XDEJYYBH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDEJYYMC", sbValue, 1024);
                                objYDryxx.XDEJYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDSJYYBH", sbValue, 1024);
                                objYDryxx.XDSJYYBH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XDSJYYMC", sbValue, 1024);
                                objYDryxx.XDSJYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SHENXRQ", sbValue, 1024);
                                objYDryxx.SHENXRQ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZZRQ", sbValue, 1024);
                                objYDryxx.ZZRQ = sbValue.ToString().Trim();
                                m_objYDryxx.Add(objYDryxx);
                                intRet = NextRow(intH);
                            }
                            catch (Exception objEx)
                            {
                                ExceptionLog.OutPutException("参保病人基本资料查询出错中给异地人员信息VO赋值出错：" + objEx.Message);
                                MessageBox.Show("参保病人基本资料查询出错中给异地人员信息VO赋值出错：" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                lngRes = -1;
                            }
                        }
                    }
                    #endregion

                    #region 转院信息 结果集 ZZZLXX
                    intRet = LocateDataSet(intH, "ZZZLXX");
                    intRowCount = GetRowSize(intH);
                    clsDGZyxx_VO objZYxx = null;
                    if (intRowCount > 0)
                    {
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objZYxx = new clsDGZyxx_VO();
                            try
                            {
                                intRet = GetFieldValue(intH, "ZRYYBH", sbValue, 1024);
                                objZYxx.ZRYYBH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                                objZYxx.XM = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "SPYWH", sbValue, 1024);
                                objZYxx.SPYWH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZRYYMC", sbValue, 1024);
                                objZYxx.ZRYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZCYYMC", sbValue, 1024);
                                objZYxx.ZCYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZCKS", sbValue, 1024);
                                objZYxx.ZCKS = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZCRQ", sbValue, 1024);
                                objZYxx.ZCRQ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYLB", sbValue, 1024);
                                objZYxx.ZYLB = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYZD", sbValue, 1024);
                                objZYxx.ZYZD = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JZRQ", sbValue, 1024);
                                objZYxx.JZRQ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZZYY", sbValue, 1024);
                                objZYxx.ZZYY = sbValue.ToString().Trim();
                                m_objZYxx.Add(objZYxx);
                                intRet = NextRow(intH);
                            }
                            catch (Exception objEx)
                            {
                                ExceptionLog.OutPutException("参保病人基本资料查询出错中给转院信息VO赋值出错:" + objEx.Message);
                                MessageBox.Show("参保病人基本资料查询出错中给转院信息VO赋值出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                lngRes = -1;
                            }
                        }
                    }
                    #endregion

                    #region 最近住院信息 结果集 ZJJZXX
                    intRet = LocateDataSet(intH, "ZJJZXX");
                    intRowCount = GetRowSize(intH);
                    clsDGZjzyxx_VO objZJzyxx = null;
                    if (intRowCount > 0)
                    {
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objZJzyxx = new clsDGZjzyxx_VO();
                            try
                            {
                                intRet = GetFieldValue(intH, "GMSFHM", sbValue, 1024);
                                objZJzyxx.GMSFHM = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JZJLH", sbValue, 1024);
                                objZJzyxx.JZJLH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                                objZJzyxx.XM = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JZYYMC", sbValue, 1024);
                                objZJzyxx.JZYYMC = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYH", sbValue, 1024);
                                objZJzyxx.ZYH = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "CYZD", sbValue, 1024);
                                objZJzyxx.CYZD = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYLB", sbValue, 1024);
                                objZJzyxx.ZYLB = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "JZLB", sbValue, 1024);
                                objZJzyxx.JZLB = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "RYRQ", sbValue, 1024);
                                objZJzyxx.RYRQ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "CYRQ", sbValue, 1024);
                                objZJzyxx.CYRQ = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "ZYKS", sbValue, 1024);
                                objZJzyxx.ZYKS = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "YYRYKS", sbValue, 1024);
                                objZJzyxx.YYRYKS = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "CYKS", sbValue, 1024);
                                objZJzyxx.CYKS = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "YYCYKS", sbValue, 1024);
                                objZJzyxx.YYCYKS = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "RYZD", sbValue, 1024);
                                objZJzyxx.RYZD = sbValue.ToString().Trim();
                                intRet = GetFieldValue(intH, "CYYY", sbValue, 1024);
                                objZJzyxx.CYYY = sbValue.ToString().Trim();
                                m_objZJzyxx.Add(objZJzyxx);
                                intRet = NextRow(intH);
                            }
                            catch (Exception objEx)
                            {
                                ExceptionLog.OutPutException("参保病人基本资料查询出错中给转院信息VO赋值出错:" + objEx.Message);
                                MessageBox.Show("参保病人基本资料查询出错中给转院信息VO赋值出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                lngRes = -1;
                            }
                        }
                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region HISYB.XML读写操作
        /// <summary>
        /// XML文件名
        /// </summary>
        public static string XMLFile = Application.StartupPath + @"\HISYB.xml";
        /// <summary>
        /// 读操作
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        public static string m_strReadXML(string parentnode, string childnode, string key)
        {
            string strRet = "";
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }
        /// <summary>
        /// 写操作
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public static bool m_blnWriteXML(string parentnode, string childnode, string key, string val)
        {
            bool blnRet = false;
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null && xndC != null)
                    {
                        xndC.Attributes["value"].Value = val;
                        xdoc.Save(XMLFile);
                        blnRet = true;
                    }
                }
            }
            catch
            {
                blnRet = false;
            }
            return blnRet;
        }
        #endregion

        #region 将对象转换为数字
        /// <summary>
        /// 将对象转换为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return 0;
            }
        }
        #endregion

        #region 期帐查询-费用明细清单 CS-424 (ID:12567)
        /// <summary>
        /// 期帐查询-费用明细清单
        /// </summary>
        /// <param name="clsDGExtra_VO"></param>
        /// <param name="clsDGZYjsxxxx"></param>
        /// <param name="clsDGZYjsGRZFXMMX_VO"></param>
        /// <param name="Type">1 打印 2 导出</param>
        public static void m_mthRptChargeDet(clsDGExtra_VO objDgextraVo, clsDGZYjsxxxx objDgzyjsxxxxVo, List<clsDGZYjsGRZFXMMX_VO> lstDgzyjsGRZFXMMXVo, int Type)//DataGridView DGV, string DeptName, string Zyh, string Name, int Type, string DateScop)
        {
            DataStore dsRep = new DataStore();
            dsRep.LibraryList = Application.StartupPath + "\\PBReport.pbl";
            dsRep.DataWindowObject = "d_ins_chargedet";


            //DataTable dt = new DataTable();
            //dt = dvtodt(DGV);

            //DataView dvTemp = dt.DefaultView;
            //dvTemp.Sort = "colfpfl asc";
            //dt = dvTemp.ToTable();


            dsRep.Modify("t_yymc.text = '" + objDgzyjsxxxxVo.YYMC + "'");//医院名称
            dsRep.Modify("t_sdywh.text = '" + objDgzyjsxxxxVo.SDYWH + "'");//结算序号
            dsRep.Modify("t_jzjlh.text = '" + objDgzyjsxxxxVo.JZJLH + "'");//就诊记录号
            dsRep.Modify("t_jzlb.text = '" + objDgzyjsxxxxVo.JZLB + "'");//就诊类别
            dsRep.Modify("t_zjhm.text = '" + objDgzyjsxxxxVo.ZJHM + "'");//证件号码
            dsRep.Modify("t_xm.text = '" + objDgzyjsxxxxVo.XM + "'");//姓名


            //社保计算金额 = 医疗费总额 -(自费项目金额+起付金+分段个人自费+超过限额自)+医疗补助+记账金额
            //SBJSJE = YLZFY-（ZFEIXMJE+QFJE+FDGRZFEI+CXEZFEI)+YLBZ+JZJE
            decimal decSBJSJE = objDgzyjsxxxxVo.YLZFY - (objDgzyjsxxxxVo.ZFEIXMJE + objDgzyjsxxxxVo.QFJE + objDgzyjsxxxxVo.FDGRZFEI + objDgzyjsxxxxVo.CXEZFEI) + objDgzyjsxxxxVo.YLBZ + objDgzyjsxxxxVo.JZJE;
            dsRep.Modify("t_sbjsje.text = '" + decSBJSJE + "'");
            //注：个人支付金额=(自费项目金额+起付金+分段个人自费+超过限额自费)-医疗补助-记账金额
            //分段个人自费(0.00)=基本医疗费(0.00)*(基本自付比例(0%)+下调支付比例(0%))
            decimal decGRZFJE = (objDgzyjsxxxxVo.ZFEIXMJE + objDgzyjsxxxxVo.QFJE + objDgzyjsxxxxVo.FDGRZFEI + objDgzyjsxxxxVo.CXEZFEI) - objDgzyjsxxxxVo.YLBZ - objDgzyjsxxxxVo.JZJE;
            dsRep.Modify("t_grzfje.text = '" + decGRZFJE + "'");
            //降低报销比例：JDBXBL            降报金额：JBJE     基本分段自付金额 JBFDZFEIJE

            //低保医疗救助金=(个人支付金额-违规降报金额-纯自费金额-起付金)*90%+起付金-超低保救助金
            //DBYUZJ =(DBGRZFJE-DBWGJBJE-DBCZFEIJE-DBQFJE)*90%+DBQFJE-CDBJZJ
            decimal decDBYUZJ = (objDgzyjsxxxxVo.DBGRZFJE - objDgzyjsxxxxVo.DBWGJBJE - objDgzyjsxxxxVo.DBCZFEIJE - objDgzyjsxxxxVo.DBQFJE) * 0.9M + objDgzyjsxxxxVo.DBQFJE - objDgzyjsxxxxVo.CDBJZJ;
            dsRep.Modify("t_dbyuzj.text = '" + decDBYUZJ + "'");
            //个人实际自费金额=个人支付金额-低保医疗救助金
            //GRSJZFJE=DBGRZFJE-DBYLZJ
            decimal decGRSJZFJE = objDgzyjsxxxxVo.DBGRZFJE - objDgzyjsxxxxVo.DBYLJZJ;
            dsRep.Modify("t_dbyuzj.text = '" + decGRSJZFJE + "'");

            //ArrayList RowArr = new ArrayList();
            string strZfeibl = string.Empty;
            string strCzfeibl = string.Empty;
            for (int i = 0; i < lstDgzyjsGRZFXMMXVo.Count; i++)
            {
                //if (RowArr.IndexOf(i) >= 0)
                //{
                //    continue;
                //}

                //string areaname = dt.Rows[i]["colKdbq"].ToString();
                //string creatdate = dt.Rows[i]["colrq"].ToString();
                //string itemid = dt.Rows[i]["colxmdm"].ToString().Trim();
                //string price = dt.Rows[i]["coldj"].ToString().Trim();
                //string opername = dt.Rows[i]["collr"].ToString().Trim();
                //decimal amount = clsPublic.ConvertObjToDecimal(dt.Rows[i]["colsl"].ToString().Trim());
                //decimal totalmoney = clsPublic.ConvertObjToDecimal(dt.Rows[i]["colje"].ToString().Trim());
                //string spec = dt.Rows[i]["colgg"].ToString().Trim();
                //for (int j = i + 1; j < dt.Rows.Count; j++)
                //{
                //    if (dt.Rows[j]["colKdbq"].ToString().Trim() == areaname &&
                //        dt.Rows[j]["colrq"].ToString().Trim() == creatdate &&
                //        dt.Rows[j]["colxmdm"].ToString().Trim() == itemid &&
                //        dt.Rows[j]["coldj"].ToString().Trim() == price &&
                //        dt.Rows[j]["collr"].ToString().Trim() == opername)
                //    {
                //        amount += clsPublic.ConvertObjToDecimal(dt.Rows[j]["colsl"].ToString().Trim());
                //        totalmoney += clsPublic.ConvertObjToDecimal(dt.Rows[j]["colje"].ToString().Trim());

                //        RowArr.Add(j);
                //    }
                //}

                int row = dsRep.InsertRow(0);
                dsRep.SetItemString(row, "colyyxmmc", lstDgzyjsGRZFXMMXVo[i].YYXMMC);
                dsRep.SetItemDecimal(row, "coljg", lstDgzyjsGRZFXMMXVo[i].JG);
                dsRep.SetItemDecimal(row, "colzgxj", lstDgzyjsGRZFXMMXVo[i].ZGXJ);
                dsRep.SetItemDecimal(row, "colsl", lstDgzyjsGRZFXMMXVo[i].SL);
                strZfeibl = (lstDgzyjsGRZFXMMXVo[i].ZFEIBL * 100).ToString() + "%";
                dsRep.SetItemString(row, "colzfeibl", strZfeibl);
                dsRep.SetItemDecimal(row, "colkbje", lstDgzyjsGRZFXMMXVo[i].KBJE);
                dsRep.SetItemDecimal(row, "colzfeije", lstDgzyjsGRZFXMMXVo[i].ZFEIJE);
                strCzfeibl = (lstDgzyjsGRZFXMMXVo[i].CZFEIBL * 100).ToString() + "%";
                dsRep.SetItemString(row, "colczfeibl", strCzfeibl);
            }
            dsRep.CalculateGroups();

            if (Type == 1)
            {
                PrintDialog(dsRep);
            }
            else if (Type == 2)
            {
                ExportDataStore(dsRep, null);
            }
        }
        #endregion

        #region 数据窗口DataWindow/DataStore预览打印
        /// <summary>
        /// DataWindow预览打印
        /// </summary>
        /// <param name="DW"></param>
        public static void PrintDialog(Sybase.DataWindow.DataWindowControl DW)
        {
            frmPrintDialog PrintDialog = new frmPrintDialog(DW);
            PrintDialog.ShowDialog();
        }
        /// <summary>
        /// DataStore预览打印
        /// </summary>
        /// <param name="DS"></param>
        public static void PrintDialog(Sybase.DataWindow.DataStore DS)
        {
            frmPrintDialog PrintDialog = new frmPrintDialog(DS);
            PrintDialog.ShowDialog();
        }
        #endregion

        #region 数据窗口DataWindow/DataStore导出成HTML,再转成EXCEL
        /// <summary>
        /// 数据窗口DataWindow/DataStore导出成HTML,再转成EXCEL
        /// </summary>
        /// <param name="DW"></param>
        /// <param name="volExcel"></param>
        public static void ExportDataWindow(Sybase.DataWindow.DataWindowControl DW, clsVolDatawindowToExcel[] volExcel)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel 文档|*.xls";
            FD.Title = "导出";
            FD.ShowDialog();
            string filename = FD.FileName.Trim();

            if (filename != "")
            {
                DW.SaveAs(filename, Sybase.DataWindow.FileSaveAsType.HtmlTable, false, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
                if (volExcel != null)
                {
                    //定义
                    object MissingValue = Type.Missing;

                    Excel.Application Excel_app = new Excel.ApplicationClass();
                    //打开Excel文挡
                    Excel.Workbook Excel_work = Excel_app.Workbooks.Open(filename, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue);
                    //获取当前工作列表
                    Excel.Worksheet mySheet = (Excel.Worksheet)Excel_work.Worksheets[1];

                    for (int i = volExcel.Length - 1; i >= 0; i--)
                    {
                        Excel.Range range = null;
                        range = (Excel.Range)mySheet.Rows.get_Item(1, Type.Missing);
                        //在顶行插入空行
                        range.Rows.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        for (int j = 0; j < volExcel[i].m_title_text.Length; j++)
                        {
                            //合并行
                            if (volExcel[i].m_endcommn[j] == "ALL")
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], mySheet.Cells[1, mySheet.UsedRange.Columns.Count]);
                            }
                            else
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], volExcel[i].m_endcommn[j]);
                            }
                            range.Merge(MissingValue);

                            range.Value2 = volExcel[i].m_title_text[j];
                            if (volExcel[i].m_HorizontalAlignment[j] == "0")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            }
                            else if (volExcel[i].m_HorizontalAlignment[j] == "L")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            }
                            else
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            range.RowHeight = volExcel[i].m_rowheight[j];
                        }

                    }

                    mySheet.UsedRange.Rows.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    mySheet = null;
                    Excel_app.ActiveWorkbook.Close(true, filename.ToString(), null);
                    Excel_app.Workbooks.Close();
                    Excel_app.Quit();
                    Excel_app = null;
                }
            }
        }
        /// <summary>
        /// 数据窗口DataWindow/DataStore导出成HTML,再转成EXCEL
        /// </summary>
        /// <param name="DS"></param>
        /// <param name="volExcel"></param>
        public static void ExportDataStore(Sybase.DataWindow.DataStore DS, clsVolDatawindowToExcel[] volExcel)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel 文档|*.xls";
            FD.Title = "导出";
            FD.ShowDialog();
            string filename = FD.FileName.Trim();

            if (filename != "")
            {
                DS.SaveAs(filename, Sybase.DataWindow.FileSaveAsType.HtmlTable, true, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
                if (volExcel != null)
                {

                    //定义
                    object MissingValue = Type.Missing;
                    Excel.Application Excel_app = new Excel.ApplicationClass();

                    //打开Excel文挡
                    Excel.Workbook Excel_work = Excel_app.Workbooks.Open(filename, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue);
                    //获取当前工作列表
                    Excel.Worksheet mySheet = (Excel.Worksheet)Excel_work.Worksheets[1];

                    for (int i = volExcel.Length - 1; i >= 0; i--)
                    {
                        Excel.Range range = null;
                        range = (Excel.Range)mySheet.Rows.get_Item(1, Type.Missing);
                        //在顶行插入空行
                        range.Rows.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        for (int j = 0; j < volExcel[i].m_title_text.Length; j++)
                        {
                            //合并行
                            if (volExcel[i].m_endcommn[j] == "ALL")
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], mySheet.Cells[1, mySheet.UsedRange.Columns.Count]);
                            }
                            else
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], volExcel[i].m_endcommn[j]);
                            }
                            range.Merge(MissingValue);

                            range.Value2 = volExcel[i].m_title_text[j];
                            if (volExcel[i].m_HorizontalAlignment[j] == "0")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            }
                            else if (volExcel[i].m_HorizontalAlignment[j] == "L")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            }
                            else
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            range.RowHeight = volExcel[i].m_rowheight[j];
                        }

                    }

                    mySheet.UsedRange.Rows.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    mySheet = null;
                    Excel_app.ActiveWorkbook.Close(true, filename.ToString(), null);
                    Excel_app.Workbooks.Close();
                    Excel_app.Quit();
                    Excel_app = null;
                }
            }
        }
        #endregion

        #region 选择打印机打印
        /// <summary>
        /// 选择打印机打印
        /// </summary>
        /// <param name="DW">数据窗口</param>
        /// <param name="ShowPrintDialog">显示放弃打印窗口</param>
        public static void ChoosePrintDialog(Sybase.DataWindow.DataWindowControl DW, bool ShowCancelDialog)
        {
            PrintDialog pDiag = new PrintDialog();
            pDiag.UseEXDialog = true;
            pDiag.AllowSomePages = true;
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                DW.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;

                if (pDiag.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages)
                {
                    DW.PrintProperties.PageRange = pDiag.PrinterSettings.FromPage.ToString() + "-" + pDiag.PrinterSettings.ToPage.ToString();
                }

                DW.Print(ShowCancelDialog);
            }
            pDiag = null;
        }
        #endregion

        #region 查询记账处方明细
        /// <summary>
        /// 查询记账处方明细
        /// </summary>
        /// <param name="objListDGZyxm"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static long m_lngFunSP3011(clsDGExtra_VO objDGExVO, out List<clsDGZyxmcs_VO> objListDGZyxm, out StringBuilder strValue)
        {
            objListDGZyxm = new List<clsDGZyxmcs_VO>();
            strValue = null;
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3011");
                intRet = SetParam(intH, "YYBH", objDGExVO.YYBH);//医院编号
                intRet = SetParam(intH, "JZJLH", objDGExVO.JZJLH);//就诊记录号
                intRet = SetParam(intH, "JZQSSJ", objDGExVO.StarTime.ToString("yyyyMMddHHmmss"));
                intRet = SetParam(intH, "JZZZSJ", objDGExVO.EndTime.ToString("yyyyMMddHHmmss"));
                intRet = SetParam(intH, "JBR", objDGExVO.JBR);
                intRet = Run(intH);
                strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString() == "1")
                {
                    strValue = new StringBuilder(1024);
                    #region 返回结果集 ZYCFXMCXFH
                    intRet = LocateDataSet(intH, "ZYCFXMCXFH");
                    int intRowCount = GetRowSize(intH);
                    StringBuilder sbValue = new StringBuilder(1024);
                    clsDGZyxmcs_VO objDGZyxmcs = null;
                    if (intRowCount > 0)
                    {
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objDGZyxmcs = new clsDGZyxmcs_VO();
                            try
                            {
                                intRet = GetFieldValue(intH, "JZSJ", sbValue, 1024);
                                objDGZyxmcs.JZSJ = sbValue.ToString();
                                intRet = GetFieldValue(intH, "FYRQ", sbValue, 1024);
                                objDGZyxmcs.FYRQ = sbValue.ToString();
                                intRet = GetFieldValue(intH, "ZYH", sbValue, 1024);
                                objDGZyxmcs.ZYH = sbValue.ToString();
                                intRet = GetFieldValue(intH, "XMXH", sbValue, 1024);
                                objDGZyxmcs.XMXH = sbValue.ToString();
                                intRet = GetFieldValue(intH, "YYXMBM", sbValue, 1024);
                                objDGZyxmcs.YYXMBM = sbValue.ToString();
                                intRet = GetFieldValue(intH, "XMMC", sbValue, 1024);
                                objDGZyxmcs.XMMC = sbValue.ToString();
                                intRet = GetFieldValue(intH, "FLDM", sbValue, 1024);
                                objDGZyxmcs.FLDM = sbValue.ToString();
                                intRet = GetFieldValue(intH, "YBXMBM", sbValue, 1024);
                                objDGZyxmcs.YBXMBM = sbValue.ToString();
                                intRet = GetFieldValue(intH, "CFXMWYH", sbValue, 1024);
                                objDGZyxmcs.CFXMWYH = sbValue.ToString();
                                intRet = GetFieldValue(intH, "JG", sbValue, 1024);
                                objDGZyxmcs.JG = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                intRet = GetFieldValue(intH, "MCYL", sbValue, 1024);
                                objDGZyxmcs.MCYL = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                intRet = GetFieldValue(intH, "JE", sbValue, 1024);
                                objDGZyxmcs.JE = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                intRet = GetFieldValue(intH, "XZSYBZ", sbValue, 1024);
                                objDGZyxmcs.XZSYBZ = sbValue.ToString();
                                intRet = GetFieldValue(intH, "FHXZBZ", sbValue, 1024);
                                objDGZyxmcs.FHXZBZ = sbValue.ToString();
                                intRet = GetFieldValue(intH, "DYBZ", sbValue, 1024);
                                objDGZyxmcs.DYBZ = sbValue.ToString();
                                intRet = GetFieldValue(intH, "ZFEIBL", sbValue, 1024);
                                objDGZyxmcs.ZFEIBL = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                intRet = GetFieldValue(intH, "ZFEIJE", sbValue, 1024);
                                objDGZyxmcs.ZFEIJE = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                intRet = GetFieldValue(intH, "GWYBZBL", sbValue, 1024);
                                objDGZyxmcs.GWYBZBL = clsYBPublic_cs.ConvertObjToDecimal(sbValue.ToString());
                                objListDGZyxm.Add(objDGZyxmcs);
                                intRet = NextRow(intH);
                            }
                            catch (Exception objEx)
                            {
                                ExceptionLog.OutPutException("住院记帐处方项目查询赋值VO出错：" + objEx.Message);
                                MessageBox.Show("住院记帐处方项目查询赋值VO出错：" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                lngRes = -1;
                            }
                        }
                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    lngRes = -1;
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院记帐处方项目批量删除[SP3_3012]
        /// <summary>
        /// 住院记帐处方项目批量删除[SP3_3012]
        /// </summary>
        /// <param name="lstDGZyxm"></param>
        /// <param name="objDGExtr"></param>
        /// <returns></returns>
        public static long m_lngFunSP3012(List<clsDGZyxmcs_VO> lstDGZyxm, clsDGExtra_VO objDGExVO)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3012");
                intRet = SetParam(intH, "YYBH", objDGExVO.YYBH);//医院编号
                intRet = SetParam(intH, "JZJLH", objDGExVO.JZJLH);//就诊记录号
                intRet = SetParam(intH, "ZYH", objDGExVO.ZYH);
                intRet = SetParam(intH, "SCFS", "2");
                intRet = SetParam(intH, "CBDTCQBM", objDGExVO.CBDTCQBM);
                intRet = SetParam(intH, "JBR", objDGExVO.JBR);
                intRet = InsertDataSet(intH);
                for (int i = 0; i < lstDGZyxm.Count; i++)
                {
                    intRet = InsertRow(intH);
                    intRet = SetField(intH, "CFXMWYH", lstDGZyxm[i].CFXMWYH.ToString().Trim());
                    EndRow(intH, i + 1);
                    objLog.LogError("lstDGZyxm[i].CFXMWYH: " + lstDGZyxm[i].CFXMWYH.ToString().Trim() + "\n");
                }
                intRet = EndDataSet(intH, "CFXMWYH");
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Trim() == "1")
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院结算信息发票修改[SP3_3010]
        /// <summary>
        /// 住院结算信息发票修改[SP3_3010]
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public static long m_lngFunSP3010(clsDGExtra_VO objDgextraVo)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_3010");

                intRet = SetParam(intH, "YYBH", objDgextraVo.YYBH);//医院编号
                intRet = SetParam(intH, "FPHM", objDgextraVo.FPHM);//发票号码
                intRet = SetParam(intH, "SDYWH", objDgextraVo.SDYWH);//结算序号
                intRet = SetParam(intH, "ZDZMHM", "");//诊断证明号码
                intRet = SetParam(intH, "CBDTCQBM", "");//参保地统筹区编码
                intRet = SetParam(intH, "JBR", objDgextraVo.JBR);//经办人
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Equals("1"))
                {
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 查询门诊结算信息SP3_1206
        /// <summary>
        /// 查询门诊结算信息SP3_1206
        /// </summary>
        /// <param name="objDGex"></param>
        /// <param name="lstBillInfo"></param>
        /// <returns></returns>
        public static long m_lngFunSP1206(clsDGExtra_VO objDGex, out List<clsBillInfoMZ_VO> lstBillInfo)
        {
            lstBillInfo = new List<clsBillInfoMZ_VO>();
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_1206");

                intRet = SetParam(intH, "GMSFHM", objDGex.GMSFHM);//公民身份证号
                intRet = SetParam(intH, "KSSJ", objDGex.StarTime.ToString("yyyyMMdd"));//开始时间
                intRet = SetParam(intH, "ZZSJ", objDGex.EndTime.ToString("yyyyMMdd"));//终止时间
                intRet = SetParam(intH, "YYBH", objDGex.YYBH);//医院编号
                intRet = SetParam(intH, "JBR", objDGex.JBR);//经办人
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Equals("1"))
                {
                    #region 返回结果集
                    intRet = LocateDataSet(intH, "MZJSXX");//定位结果集
                    int intRowCount = GetRowSize(intH);
                    if (intRowCount <= 0)
                    {
                        DestroyInstance(intH);
                        return -1;//查询结果集为空
                    }
                    StringBuilder sbValue = new StringBuilder(1024);
                    clsBillInfoMZ_VO objBillInfo = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objBillInfo = new clsBillInfoMZ_VO();
                        try
                        {
                            intRet = GetFieldValue(intH, "GMSFHM", sbValue, 1024);
                            objBillInfo.GMSFHM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "GRBH", sbValue, 1024);
                            objBillInfo.GRBH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                            objBillInfo.XM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYBH", sbValue, 1024);
                            objBillInfo.YYBH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYMC", sbValue, 1024);
                            objBillInfo.YYMC = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYJSLB", sbValue, 1024);
                            objBillInfo.ZYJSLB = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZLB", sbValue, 1024);
                            objBillInfo.JZLB = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSRQ", sbValue, 1024);
                            objBillInfo.JSRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "RYRQ", sbValue, 1024);
                            objBillInfo.RYRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CYRQ", sbValue, 1024);
                            objBillInfo.CYRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CYZD", sbValue, 1024);
                            objBillInfo.CYZD = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZJLH", sbValue, 1024);
                            objBillInfo.JZJLH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "SDYWH", sbValue, 1024);
                            objBillInfo.SDYWH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CFH", sbValue, 1024);
                            objBillInfo.CFH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZH", sbValue, 1024);
                            objBillInfo.ZH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YLFYZE", sbValue, 1024);
                            objBillInfo.YLFYZE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "TCZF", sbValue, 1024);
                            objBillInfo.TCZF = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "GRZFZE", sbValue, 1024);
                            objBillInfo.GRZFZE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "KSMC", sbValue, 1024);
                            objBillInfo.KSMC = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYRYKS", sbValue, 1024);
                            objBillInfo.YYRYKS = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "MZYFBXJE", sbValue, 1024);
                            objBillInfo.MZYFBXJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF1", sbValue, 1024);
                            objBillInfo.BCYLTCZF1 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF2", sbValue, 1024);
                            objBillInfo.BCYLTCZF2 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF3", sbValue, 1024);
                            objBillInfo.BCYLTCZF3 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF4", sbValue, 1024);
                            objBillInfo.BCYLTCZF4 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "QTZHIFU", sbValue, 1024);
                            objBillInfo.QTZHIFU = sbValue.ToString().Trim();
                            lstBillInfo.Add(objBillInfo);
                            intRet = NextRow(intH);
                        }
                        catch (Exception objEx)
                        {
                            ExceptionLog.OutPutException("读取门诊结算信息出错:" + objEx.Message);
                            MessageBox.Show("读取门诊结算信息出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            lngRes = -1;
                        }

                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    lngRes = -1;
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 住院就诊记录查询SP3_1207
        /// <summary>
        /// 住院就诊记录查询SP3_1207
        /// </summary>
        /// <param name="objDGex"></param>
        /// <param name="lstChargeInfo"></param>
        /// <returns></returns>
        public static long m_lngFunSP1207(clsDGExtra_VO objDGex, out List<clsChargeInfoZY_VO> lstChargeInfo)
        {
            lstChargeInfo = new List<clsChargeInfoZY_VO>();
            long lngRes = -1;
            int intH = CreateInstace();
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_1207");

                intRet = SetParam(intH, "JZJLH", objDGex.JZJLH);//就诊记录号
                intRet = SetParam(intH, "SDYWH", objDGex.SDYWH);//结算序号
                intRet = SetParam(intH, "YYBH", objDGex.YYBH);//医院编号
                intRet = SetParam(intH, "CBDTCQBM", objDGex.CBDTCQBM);//参保地统筹区编码
                intRet = SetParam(intH, "JBR", objDGex.JBR);//经办人
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Equals("1"))
                {
                    #region 返回结果集
                    intRet = LocateDataSet(intH, "ZYJZJSXX");//定位结果集
                    int intRowCount = GetRowSize(intH);
                    if (intRowCount <= 0)
                    {
                        DestroyInstance(intH);
                        return -1;//查询结果集为空
                    }
                    StringBuilder sbValue = new StringBuilder(1024);
                    clsChargeInfoZY_VO objBillInfo = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objBillInfo = new clsChargeInfoZY_VO();
                        try
                        {
                            intRet = GetFieldValue(intH, "GMSFHM", sbValue, 1024);
                            objBillInfo.GMSFHM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "XM", sbValue, 1024);
                            objBillInfo.XM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZYY", sbValue, 1024);
                            objBillInfo.JZYY = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYH", sbValue, 1024);
                            objBillInfo.ZYH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "RYKS", sbValue, 1024);
                            objBillInfo.RYKS = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "YYRYKS", sbValue, 1024);
                            objBillInfo.YYRYKS = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CYKS", sbValue, 1024);
                            objBillInfo.CYKS = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "YYCYKS", sbValue, 1024);
                            objBillInfo.YYCYKS = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CWH", sbValue, 1024);
                            objBillInfo.CWH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "RYRQ", sbValue, 1024);
                            objBillInfo.RYRQ = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "CYRQ", sbValue, 1024);
                            objBillInfo.CYRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYLB", sbValue, 1024);
                            objBillInfo.ZYLB = m_mthDicConvert("ZYLB", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "JZLB", sbValue, 1024);
                            objBillInfo.JZLB = m_mthYBJzlbConvert(sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "RYZD", sbValue, 1024);
                            objBillInfo.RYZD = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CYZD", sbValue, 1024);
                            objBillInfo.CYZD = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "WSBZ", sbValue, 1024);
                            objBillInfo.WSBZ = m_mthDicConvert("WSBZ", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "ZQQRQK", sbValue, 1024);
                            objBillInfo.ZQQRQK = m_mthDicConvert("ZQQRQK", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "ZQQRSBH", sbValue, 1024);
                            objBillInfo.ZQQRSBH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "LXDH", sbValue, 1024);
                            objBillInfo.LXDH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BZ", sbValue, 1024);
                            objBillInfo.BZ = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "JBR", sbValue, 1024);
                            objBillInfo.JBR = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZJLH", sbValue, 1024);
                            objBillInfo.JZJLH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYTS", sbValue, 1024);
                            objBillInfo.ZYTS = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYZJE", sbValue, 1024);
                            objBillInfo.ZYZJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "SBZFJE", sbValue, 1024);
                            objBillInfo.SBZFJE = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "GWYBZJE", sbValue, 1024);
                            objBillInfo.GWYBZJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JZJE", sbValue, 1024);
                            objBillInfo.JZJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "GRZFJE", sbValue, 1024);
                            objBillInfo.GRZFJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "CYBZ", sbValue, 1024);
                            objBillInfo.CYBZ = m_mthDicConvert("CYBZ", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "JSQSRQ", sbValue, 1024);
                            objBillInfo.JSQSRQ = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "JSZZRQ", sbValue, 1024);
                            objBillInfo.JSZZRQ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSSJ", sbValue, 1024);
                            objBillInfo.JSSJ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "SDYWH", sbValue, 1024);
                            objBillInfo.SDYWH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZYSHJG", sbValue, 1024);
                            objBillInfo.ZYSHJG = m_mthDicConvert("ZYSHJG", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "CYYY", sbValue, 1024);
                            objBillInfo.CYYY = m_mthDicConvert("CYYY", sbValue.ToString().Trim());

                            intRet = GetFieldValue(intH, "MZYFBXJE", sbValue, 1024);
                            objBillInfo.MZYFBXJE = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "DBYLJZJ", sbValue, 1024);
                            objBillInfo.DBYLJZJ = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "ZTJSBZ", sbValue, 1024);
                            objBillInfo.ZTJSBZ = m_mthDicConvert("ZTJSBZ", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "YSGH", sbValue, 1024);
                            objBillInfo.YSGH = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "FPHM", sbValue, 1024);
                            objBillInfo.FPHM = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "ZDZMHM", sbValue, 1024);
                            objBillInfo.ZDZMHM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "JSBZ", sbValue, 1024);
                            objBillInfo.JSBZ = m_mthDicConvert("JSBZ", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "CBDTCQBM", sbValue, 1024);
                            objBillInfo.CBDTCQBM = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF1", sbValue, 1024);
                            objBillInfo.BCYLTCZF1 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF2", sbValue, 1024);
                            objBillInfo.BCYLTCZF2 = sbValue.ToString().Trim();

                            intRet = GetFieldValue(intH, "BCYLTCZF3", sbValue, 1024);
                            objBillInfo.BCYLTCZF3 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "BCYLTCZF4", sbValue, 1024);
                            objBillInfo.BCYLTCZF4 = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "QTZHIFU", sbValue, 1024);
                            objBillInfo.QTZHIFU = sbValue.ToString().Trim();
                            intRet = GetFieldValue(intH, "RYDYZDBY", sbValue, 1024);
                            objBillInfo.RYDYZDBY = m_mthDicConvert("RYDYZDBY", sbValue.ToString().Trim());
                            intRet = GetFieldValue(intH, "JSDYZDBY", sbValue, 1024);
                            objBillInfo.JSDYZDBY = m_mthDicConvert("JSDYZDBY", sbValue.ToString().Trim());
                            lstChargeInfo.Add(objBillInfo);
                            intRet = NextRow(intH);
                        }
                        catch (Exception objEx)
                        {
                            ExceptionLog.OutPutException("读取【住院记诊记录】信息出错:" + objEx.Message);
                            MessageBox.Show("读取【住院记诊记录】信息出错:" + objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            lngRes = -1;
                        }

                    }
                    #endregion
                    lngRes = 1;
                }
                else
                {
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    lngRes = -1;
                }
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 病人社保类别转换
        /// <summary>
        /// 病人社保类别转换
        /// </summary>
        /// <param name="strJZLB"></param>
        /// <returns></returns>
        public static string m_mthYBJzlbConvert(string strJZLB)
        {
            string p_JZLB = "";
            if (strJZLB == "12")
            {
                p_JZLB = "特定门诊";
            }
            else if (strJZLB == "32")
            {
                p_JZLB = "门诊康复";
            }
            else if (strJZLB == "51")
            {
                p_JZLB = "普通门诊";
            }
            else if (strJZLB == "52")
            {
                p_JZLB = "急诊门诊";
            }
            else if (strJZLB == "53")
            {
                p_JZLB = "转诊门诊";
            }
            else if (strJZLB == "54")
            {
                p_JZLB = "门诊抢救";
            }
            else if (strJZLB == "57")
            {
                p_JZLB = "重流门诊";
            }
            else if (strJZLB == "61")
            {
                p_JZLB = "特定门诊";
            }
            else if (strJZLB == "62")
            {
                p_JZLB = "社区特定门诊转诊";
            }
            else if (strJZLB == "63")
            {
                p_JZLB = "医院特定门诊(综保)";
            }
            else if (strJZLB == "64")
            {
                p_JZLB = "社区二类特定门诊";
            }
            else if(strJZLB == "73")
            {
                p_JZLB = "生育保险(产检)";
            }
            else if(strJZLB == "79")
            {
                p_JZLB = "计划生育门诊";
            }
            else if (strJZLB == "81")
            {
                p_JZLB = "公务员体检";
            }
            else if (strJZLB == "101")
            {
                p_JZLB = "医学检查";
            }
            return p_JZLB;
        }
        #endregion

        #region 字典转换
        /// <summary>
        /// 字典转换
        /// </summary>
        /// <param name="strkey"></param>
        /// <param name="strValue"></param>
        public static string m_mthDicConvert(string strkey, string strValue)
        {
            string strRespone = string.Empty;
            if (strkey == "WSBZ")
            {
                switch (strValue)
                {
                    case "0":
                        strRespone = "非外伤";
                        break;
                    case "1":
                        strValue = "外伤";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "ZYLB")
            {
                switch (strValue)
                {
                    case "1":
                        strRespone = "医疗住院";
                        break;
                    case "2":
                        strRespone = "医疗住院";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "ZQQRQK")
            {
                switch (strValue)
                {
                    case "1":
                        strRespone = "同意";
                        break;
                    case "2":
                        strRespone = "不同意";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "CYBZ")
            {
                switch (strValue)
                {
                    case "1":
                        strRespone = "在院";
                        break;
                    case "2":
                        strRespone = "出院";
                        break;
                    case "3":
                        strRespone = "取消入院";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "ZYSHJG")
            {
                switch (strValue)
                {
                    case "0":
                        strRespone = "待查";
                        break;
                    case "1":
                        strRespone = "可在院结算";
                        break;
                    case "2":
                        strRespone = "不可在院结算";
                        break;
                    case "3":
                        strRespone = "不可报";
                        break;
                    case "4":
                        strRespone = "锁定";
                        break;
                    case "5":
                        strRespone = "转医疗";
                        break;
                    case "6":
                        strRespone = "转工伤";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "CYYY")
            {
                switch (strValue)
                {
                    case "1":
                        strRespone = "治愈";
                        break;
                    case "2":
                        strRespone = "好转";
                        break;
                    case "3":
                        strRespone = "未愈";
                        break;
                    case "4":
                        strRespone = "死亡";
                        break;
                    case "5":
                        strRespone = "转院";
                        break;
                    case "6":
                        strRespone = "转外";
                        break;
                    case "9":
                        strRespone = "其他";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "ZTJSBZ")
            {
                switch (strValue)
                {
                    case "0":
                        strRespone = "否";
                        break;
                    case "1":
                        strRespone = "是";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "JSBZ")
            {
                switch (strValue)
                {
                    case "0":
                        strRespone = "未结算";
                        break;
                    case "2":
                        strRespone = "已结算";
                        break;
                    case "3":
                        strRespone = "取消结算";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "RYDYZDBY")
            {
                switch (strValue)
                {
                    case "1":
                        strRespone = "疾病";
                        break;
                    case "2":
                        strRespone = "自我因素导致的伤害";
                        break;
                    case "3":
                        strRespone = "其他非第三方导致的伤害";
                        break;
                    case "4":
                        strRespone = "突发事件导致的伤害";
                        break;
                    case "5":
                        strRespone = "第三方导致的伤害（交通事故除外）";
                        break;
                    case "6":
                        strRespone = "交通事故";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            else if (strkey == "JSDYZDBY")
            {
                switch (strValue)
                {
                    case "11":
                        strRespone = "疾病";
                        break;
                    case "12":
                        strRespone = "自我因素导致的伤害";
                        break;
                    case "13":
                        strRespone = "其他非第三方导致的伤害";
                        break;
                    case "14":
                        strRespone = "突发事件导致的伤害";
                        break;
                    case "15":
                        strRespone = "第三方导致的伤害（交通事故除外）";
                        break;
                    case "16":
                        strRespone = "交通事故";
                        break;
                    default:
                        strRespone = strValue;
                        break;
                }
            }
            return strRespone;
        }
        #endregion

        #region 社保卡密码验证
        /// <summary>
        /// 社保卡密码验证
        /// </summary>
        /// <param name="intFlag">1:门诊；  2:住院</param>
        /// <param name="qrCode">电子社保卡二维码</param>
        /// <returns></returns>
        public static long m_lngFunSP5001(int intFlag, clsDGExtra_VO objDGExtraVO, DateTime dtmFyrq, string qrCode)
        {
            long lngRes = -1;
            int intH = CreateInstace();
            string date = DateTime.Now.ToString("yyyyMMdd");
            int intRet;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_5001");
                intRet = SetParam(intH, "JBR", objDGExtraVO.JBR);   // 经办人
                intRet = SetParam(intH, "JBRQ", date);      // 经办日期
                intRet = SetParam(intH, "YYBH", objDGExtraVO.YYBH);     // 医院编号
                intRet = SetParam(intH, "CLIENTTYPE", "HIS");
                intRet = SetParam(intH, "GMSFHM", objDGExtraVO.GMSFHM); // 公民身份证号
                intRet = SetParam(intH, "QRCODE", qrCode);        // 电子社保卡二维码 -- 待测试
                intRet = SetParam(intH, "ZYLB", objDGExtraVO.ZYLB);
                intRet = SetParam(intH, "JZLB", objDGExtraVO.JZLB);
                intRet = SetParam(intH, "FYRQ", dtmFyrq.ToString("yyyyMMdd"));
                intRet = SetParam(intH, "JZJLH", objDGExtraVO.JZJLH);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("JBR : " + objDGExtraVO.JBR);
                sb.AppendLine("YYBH : " + objDGExtraVO.YYBH);
                sb.AppendLine("JBRQ : " + date);
                sb.AppendLine("GMSFHM : " + objDGExtraVO.GMSFHM);
                sb.AppendLine("QRCODE : " + qrCode);
                sb.AppendLine("ZYLB : " + objDGExtraVO.ZYLB);
                sb.AppendLine("JZLB : " + objDGExtraVO.JZLB);
                sb.AppendLine("FYRQ : " + date);
                sb.AppendLine("JZJLH : " + objDGExtraVO.JZJLH);
                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "MMYZFHZ", strValue, 1024);
                intRet = GetParam(intH, "FHZ", strValue, 1024);
                if (strValue.ToString().Equals("1"))
                {
                    intRet = GetParam(intH, "MMYZURL", strValue, 1024);
                    System.Diagnostics.Process.Start("iexplore.exe", strValue.ToString());
                    //frmCheckPswUrl objCheck = new frmCheckPswUrl(strValue.ToString());
                    //objCheck.ShowDialog();
                    lngRes = 1;
                }
                else
                {
                    // intRet = GetParam(intH, "MMYZMSG", strValue, 1024);
                    intRet = GetParam(intH, "MSG", strValue, 1024);
                    ExceptionLog.OutPutException(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    lngRes = -1;
                }
                // 新加日志
                Log.Output(sb.ToString());
                Log.Output(strValue.ToString());
            }
            DestroyInstance(intH);
            return lngRes;
        }
        #endregion

        #region 跨省异地卡鉴权
        /// <summary>
        /// 跨省异地卡鉴权
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="uri"></param>
        public static void SP3_5002(clsDGZydj_VO vo, string JBR, out string uri)
        {
            Sp3_5002_3("SP3_5002", JBR, string.Empty, vo, out uri);
        }
        #endregion

        #region 跨省异地卡交易认证
        /// <summary>
        /// 跨省异地卡交易认证
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="JBR"></param>
        /// <param name="JZJLH"></param>
        /// <param name="uri"></param>
        public static void SP3_5003(clsDGZydj_VO vo, string JBR, string JZJLH, out string uri)
        {
            Sp3_5002_3("SP3_5003", JBR, JZJLH, vo, out uri);
        }

        #region Sp3_5002_3
        /// <summary>
        /// Sp3_5002_3
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="JBR"></param>
        /// <param name="JZJLH"></param>
        /// <param name="vo"></param>
        /// <param name="uri"></param>
        static void Sp3_5002_3(string FuncCode, string JBR, string JZJLH, clsDGZydj_VO vo, out string uri)
        {
            uri = string.Empty;
            int intH = CreateInstace();
            int intRet = -1;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", FuncCode);
                intRet = SetParam(intH, "GMSFHM", vo.GMSFHM);                           // 证件号码: 社保卡上的社会保障号码
                intRet = SetParam(intH, "ZYLB", "1");                                   // 住院类别: 1 医疗住院
                intRet = SetParam(intH, "JZLB", vo.JZLB);                               // 就诊类别: 字典
                intRet = SetParam(intH, "FYRQ", vo.RYRQ);                               // 费用日期: 入院日期(登记); 结算终止日期(结算)
                intRet = SetParam(intH, "JZJLH", JZJLH);                                // 就诊记录号: 入院登记时社保返回的就诊记录号
                intRet = SetParam(intH, "CBDTCQBM", vo.CBDTCQBM);                       // 参保地统筹区编码 
                // 统一参数
                intRet = SetParam(intH, "JBR", JBR);                                    // 经办人
                intRet = SetParam(intH, "JBRQ", DateTime.Now.ToString("yyyyMMdd"));     // 经办日期
                intRet = SetParam(intH, "YYBH", vo.YYBH);                               // 经办医院的医院编号
                intRet = SetParam(intH, "CLIENTTYPE", "HIS");                           // 客户端类型: HIS

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", FuncCode));
                sb.AppendLine(string.Format("GMSFHM := {0}", vo.GMSFHM));
                sb.AppendLine(string.Format("ZYLB := {0}", "1"));
                sb.AppendLine(string.Format("JZLB := {0}", vo.JZLB));
                sb.AppendLine(string.Format("FYRQ := {0}", vo.RYRQ));
                sb.AppendLine(string.Format("JZJLH := {0}", JZJLH));
                sb.AppendLine(string.Format("CBDTCQBM := {0}", vo.CBDTCQBM));
                sb.AppendLine(string.Format("JBR := {0}", JBR));
                sb.AppendLine(string.Format("JBRQ := {0}", DateTime.Now.ToString("yyyyMMdd")));
                sb.AppendLine(string.Format("YYBH := {0}", vo.YYBH));
                sb.AppendLine(string.Format("CLIENTTYPE := {0}", "HIS"));
                Log.Output(sb.ToString());
                #endregion

                intRet = Run(intH);
                StringBuilder strValue = new StringBuilder(1024);
                intRet = GetParam(intH, "MMYZFHZ", strValue, 1024);                     // 返回值: 1:执行成功 否则:失败
                if (strValue.ToString() == "1")
                {
                    StringBuilder sbValue = new StringBuilder(1024);
                    intRet = GetParam(intH, "MMYZURL", strValue, 1024);                 // 公共页面访问的URL地址
                    uri = strValue.ToString().Trim();
                    Log.Output(uri);
                }
                else
                {
                    intRet = GetParam(intH, "MMYZMSG", strValue, 1024);                 // 返回信息: MMYZFHZ失败时，HIS系统需弹出MMYZMSG提示信息。
                    Log.Output(strValue.ToString());
                    MessageBox.Show("错误信息:" + strValue.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            DestroyInstance(intH);
        }
        #endregion

        #endregion

        #region urlBase64_toBase64
        /// <summary>
        /// urlBase64_toBase64
        /// </summary>
        /// <param name="urlBase64"></param>
        /// <returns></returns>
        static string urlBase64_toBase64(string urlBase64)
        {
            int a = urlBase64.Length % 4;
            if (a == 1)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "===";
            }
            else if (a == 2)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "==";
            }
            else if (a == 3)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "=";
            }
            else
            {
                return urlBase64.Replace("-", "+").Replace("_", "/");
            }
        }
        #endregion

        #region 读取电子社保卡
        /// <summary>
        /// 读取电子社保卡
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="zlhj"></param>
        /// <param name="YYBH"></param>
        /// <param name="JBR"></param>
        /// <param name="GMSFHM"></param>
        /// <param name="XM"></param>
        /// <param name="JRKH"></param>
        public static void SP3_6001(string qrCode, string zlhj, string YYBH, string JBR, out string GMSFHM, out string XM, out string JRKH, out System.Drawing.Image photo)
        {
            GMSFHM = string.Empty;      // 公民身份号码
            XM = string.Empty;          // 姓名
            JRKH = string.Empty;        // 社保卡号
            photo = null;
            int intH = CreateInstace();
            int intRet = -1;
            if (intH > 0)
            {
                intRet = SetParam(intH, "FN", "SP3_5004");                              // 二维码获取实体卡信息
                intRet = SetParam(intH, "QRCODE", qrCode);                              // 二维码动态码, 扫电子社保卡二维码获得
                intRet = SetParam(intH, "BUSITYPE", "01");                              // 业务类型, 01业务查询
                intRet = SetParam(intH, "ZLHJ", zlhj);                                  // 诊疗环节, 010101 挂号; 010102 诊断; 010103 取药; 010104 检查; 010105 收费; 010106 开方; 010107 手术;  000000 其他
                intRet = SetParam(intH, "CBDTCQBM", "441900");
                // 统一参数
                intRet = SetParam(intH, "JBR", JBR);                                    // 经办人
                intRet = SetParam(intH, "JBRQ", DateTime.Now.ToString("yyyyMMdd"));     // 经办日期
                intRet = SetParam(intH, "YYBH", YYBH);                                  // 经办医院的医院编号
                intRet = SetParam(intH, "CLIENTTYPE", "HIS");                           // 客户端类型: HIS

                #region log
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("FN := {0}", "SP3_5004"));
                sb.AppendLine(string.Format("qrCode := {0}", qrCode));
                Log.Output(sb.ToString());
                #endregion

                try
                {
                    intRet = Run(intH);

                    StringBuilder sbVal = new StringBuilder(1024);
                    GetParam(intH, "GMSFHM", sbVal, 1024);
                    GMSFHM = sbVal.ToString().Trim();

                    sbVal = new StringBuilder(1024);
                    GetParam(intH, "XM", sbVal, 1024);
                    XM = sbVal.ToString().Trim();

                    sbVal = new StringBuilder(1024);
                    GetParam(intH, "JRKH", sbVal, 1024);
                    JRKH = sbVal.ToString().Trim();

                    sbVal = new StringBuilder(30000);
                    GetParam(intH, "XP", sbVal, 30000);
                    photo = weCare.Core.Utils.Function.ConvertBase64StringToImage(urlBase64_toBase64(sbVal.ToString()));
                }
                catch (Exception ex)
                {
                    ExceptionLog.OutPutException(ex.Message);
                }
            }
            DestroyInstance(intH);
        }
        #endregion
    }

}
