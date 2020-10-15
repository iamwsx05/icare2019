using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weCare.Core.Entity;
using weCare.Core.Utils;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Runtime.Serialization;

namespace GSSB
{
    public class Message
    {
        #region load.dll

        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        [DllImport("SSCardDriver.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "ReadCardBase")]
        public static extern string ReadCardBase();

        /// <summary>
        /// 消费交易
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="payInfo"></param>
        /// <returns></returns>
        [DllImport("SSCardDriver.dll", CallingConvention = CallingConvention.Winapi, EntryPoint = "DoDebit")]
        public static extern string DoDebit(string cardInfo, string payInfo);

        #endregion

        #region 变量

        /// <summary>
        /// xml头信息
        /// </summary>
        static string xmlTitle = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;

        /// <summary>
        /// 区域行政编码: 441900103000 	茶山镇
        /// </summary>
        static string geogId = "441900103000";

        /// <summary>
        /// 用户编码
        /// </summary>
        static string userId = "ss0028";

        /// <summary>
        /// 用户密码
        /// </summary>
        static string password = "EApYVLxl";

        /// <summary>
        /// 机构唯一码
        /// </summary>
        static string orgId = "ss0028";

        /// <summary>
        /// URI
        /// </summary>
        static string Uri = @"http://19.15.232.15:8080/hygeia_esb_sj/api/call.action";

        #endregion

        #region 统一入口
        /// <summary>
        /// 统一入口
        /// </summary>
        /// <param name="transNo"></param>
        /// <param name="request"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public static bool Access(string transNo, string request, out DataTable dtResult, out string error)
        {
            EntitySbRes res = null;
            dtResult = null;
            error = string.Empty;
            string req = string.Empty;
            try
            {
                switch (transNo)
                {
                    case "biz120001":       // 4.2.1 入院登记时取人员信息
                        req = Biz120001(request);
                        res = Proxy(req);
                        break;
                    case "bizh120102":       // 4.2.2 入院登记后取业务信息
                        req = Biz120102(request);
                        res = Proxy(req);
                        break;
                    //case "biz120002":       // 4.2.3 校验并保存费用信息
                    //    res = Proxy(transNo, Biz120002(request, dtIn));
                    //    break;
                    case "biz120003":       // 4.2.4 校验并计算费用信息
                        req = Biz120003(request);
                        res = Proxy(req);
                        break;
                    case "biz120004":       // 4.2.10 删除住院业务费用明细
                        req = Biz120004(request);
                        res = Proxy(req);
                        break;
                    case "biz120103":       // 4.2.2 入院登记
                        req = Biz120103(request);
                        res = Proxy(req);
                        break;
                    case "biz120104":       // 4.2.7 入院登记信息修改
                        req = Biz120104(request);
                        res = Proxy(req);
                        break;
                    case "biz120105":       // 4.2.5 出院登记
                        req = Biz120105(request);
                        res = Proxy(req);
                        break;
                    case "biz120106":       // 4.2.6 出院结算
                        req = Biz120106(request);
                        res = Proxy(req);
                        break;
                    case "bizh120107":       // 4.2.11 取消出院结算
                        req = Biz120107(request);
                        res = Proxy(req);
                        break;
                    case "biz120108":       // 4.2.9 取消出院登记
                        req = Biz120108(request);
                        res = Proxy(req);
                        break;
                    case "biz120109":       // 4.2.8 取消入院登记
                        req = Biz120109(request);
                        res = Proxy(req);
                        break;
                    default:
                        break;
                }
                error = req + Environment.NewLine + res.response;
                if (res != null && res.resultCode >= 0)
                {
                    DataSet ds = Function.ReadXml(res.response);
                    if (transNo == "biz120001" || transNo == "bizh120102")
                    {
                        if (ds != null && ds.Tables.Contains("row"))
                        {
                            dtResult = ds.Tables["row"];
                        }
                    }
                    else if (transNo == "biz120003" || transNo == "biz120103" || transNo == "biz120105" || transNo == "biz120106")
                    {
                        if (ds != null && ds.Tables.Contains("program"))
                        {
                            dtResult = ds.Tables["program"];
                            // biz120103
                            string jydjh = ds.Tables["program"].Rows[0]["aaz218"].ToString(); // *******
                        }
                    }
                    // 返回值>0,执行成功
                    if (ds != null && ds.Tables.Contains("program"))
                        return Function.Int(ds.Tables["program"].Rows[0]["return_code"].ToString()) >= 0 ? true : false;
                    else
                        return false;

                }
                else
                {
                    Common.Controls.DialogBox.Msg(res.response);
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.Output(error);
            }
            return false;
        }
        /// <summary>
        /// 校验并保存费用信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool Access120002(string request, List<EntitySGSFeeItem> lstFeeItems, out string error)
        {
            error = string.Empty;
            try
            {
                // 4.2.3 校验并保存费用信息
                EntitySbRes res = Proxy(Biz120002(request, lstFeeItems));
                if (res != null && res.resultCode >= 0)
                {
                    DataSet ds = Function.ReadXml(res.response);
                    // 返回值>0,执行成功
                    if (ds != null && ds.Tables.Contains("program"))
                        return Function.Int(ds.Tables["program"].Rows[0]["return_code"].ToString()) >= 0 ? true : false;
                    else
                        return false;
                }
                else
                {
                    error = res.response;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.Output(error);
            }
            return false;
        }
        #endregion

        #region Proxy
        /// <summary>
        /// Proxy
        /// </summary>
        /// <param name="transNo"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        static EntitySbRes Proxy(/*string transNo,*/ string request)
        {
            string tmpUri = Function.ReadConfigXml("uri");
            if (!string.IsNullOrEmpty(tmpUri))
            {
                Uri = tmpUri;
            }

            // Log.request 
            Log.Output(Uri + Environment.NewLine + request);
            // 返回实体 
            EntitySbRes res = new EntitySbRes();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] dataArray = encoding.GetBytes(request);
            // 创建请求
            HttpWebRequest httpWeb = (HttpWebRequest)HttpWebRequest.Create(Uri);
            httpWeb.Method = "POST";
            httpWeb.ContentLength = dataArray.Length;
            httpWeb.ContentType = "application/x-www-form-urlencoded";       // "application/json"; 
            // 创建输入流
            Stream dataStream = null;
            try
            {
                dataStream = httpWeb.GetRequestStream();
            }
            catch (WebException ex)
            {
                res.resultCode = -1;
                res.message = ex.Message;
                Log.Output(res.message);
                return res;//连接服务器失败
            }
            // 发送请求
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            // 获取返回值
            try
            {
                HttpWebResponse response = (HttpWebResponse)httpWeb.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string resp = reader.ReadToEnd();
                reader.Close();
                // log.response
                Log.Output(resp);
                if (resp.Contains("服务执行发生异常"))
                    res.resultCode = -2;
                res.response = resp;
                //res = JsonConvert.DeserializeObject<EntityRes>(resp);
            }
            catch (WebException ex)
            {
                res.resultCode = -2;
                res.message = ex.Message;
                Log.Output(res.message);
            }
            return res;
        }
        #endregion

        #region GetSessionId

        /// <summary>
        /// 当天24点有效 (当日有效)
        /// </summary>
        static string sessionId = "";

        /// <summary>
        /// 对于工伤保险协议服务机构，在调用API交易之前，必须首先进行一次登录。
        /// 登录交易调用后，将得到一个session_id有效期是到当天24点有效，因此每个工伤保险协议服务机构需要每天至少登录一次，获得最新的交易验证码session_id，
        /// 如果session_id失效，所有API请求将返回错误代码 -9 , 判断请求返回 -9 后，请立即执行登录，获取最新的session_id。
        /// </summary>
        /// <returns></returns>
        static string GetSessionId()
        {
            string request = string.Empty;
            string today = Function.ReadConfigXml("today");
            userId = Function.ReadConfigXml("userId");
            password = Function.ReadConfigXml("password");
            if (today != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                request = "<program>" + Environment.NewLine;
                request += string.Format("<function_id>{0}</function_id>", "sys0001") + Environment.NewLine;
                request += string.Format("<userid>{0}</userid>", userId) + Environment.NewLine;
                request += string.Format("<password>{0}</password>", password) + Environment.NewLine;
                request += "</program>" + Environment.NewLine;
                request = xmlTitle + request;
                // call service
                sessionId = "";
                EntitySbRes res = Proxy(request);
                if (res != null && res.resultCode >= 0)
                {
                    DataSet ds = Function.ReadXml(res.response);
                    // 返回值>0,执行成功
                    if (ds != null && ds.Tables.Contains("program"))
                    {
                        if (Function.Int(ds.Tables["program"].Rows[0]["return_code"].ToString()) >= 0)
                        {
                            sessionId = ds.Tables["program"].Rows[0]["session_id"].ToString();
                        }
                        Log.Output(ds.Tables["program"].Rows[0]["return_code_message"].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(sessionId))
                {
                    Function.SaveConfigXml("today", DateTime.Now.ToString("yyyy-MM-dd"));
                    Function.SaveConfigXml("sessionId", sessionId);
                }
            }
            else
            {
                sessionId = Function.ReadConfigXml("sessionId");
            }
            // 再判断一次
            if (string.IsNullOrEmpty(sessionId))
            {
                request = "<program>" + Environment.NewLine;
                request += string.Format("<function_id>{0}</function_id>", "sys0001") + Environment.NewLine;
                request += string.Format("<userid>{0}</userid>", userId) + Environment.NewLine;
                request += string.Format("<password>{0}</password>", password) + Environment.NewLine;
                request += "</program>" + Environment.NewLine;
                request = xmlTitle + request;
                // call service
                sessionId = "";
                EntitySbRes res = Proxy(request);
                if (res != null && res.resultCode >= 0)
                {
                    DataSet ds = Function.ReadXml(res.response);
                    // 返回值>0,执行成功
                    if (ds != null && ds.Tables.Contains("program"))
                    {
                        if (Function.Int(ds.Tables["program"].Rows[0]["return_code"].ToString()) >= 0)
                        {
                            sessionId = ds.Tables["program"].Rows[0]["session_id"].ToString();
                        }
                        Log.Output(ds.Tables["program"].Rows[0]["return_code_message"].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(sessionId))
                {
                    Function.SaveConfigXml("today", DateTime.Now.ToString("yyyy-MM-dd"));
                    Function.SaveConfigXml("sessionId", sessionId);
                }
            }
            return sessionId;
        }
        #endregion

        #region 4.1 门诊业务

        #region 4.1.1 门诊挂号时取人员信息
        /// <summary>
        /// bizh110001  门诊挂号时取人员信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz110001(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110001") + Environment.NewLine;     // 交易号
            request += string.Format("<bka895>{0}</bka895>", "aac002") + Environment.NewLine;               // 入参类型    aac001电脑号；aac002社会保障号码；bka100社保卡号
            request += string.Format("<bka896>{0}</bka896>", "513901198311295323") + Environment.NewLine;   // 入参值
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                  // 医疗待遇类型 (根据服务获取)
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.1.2 收费时提取门诊业务信息
        /// <summary>
        /// 通过输入就医登记号或个人标识（电脑号、社会保障号码、社保卡号）提取已登记的门诊业务信息。如“金额”为负数，则为退费。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Biz110102(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110102") + Environment.NewLine;     // 交易号
            request += string.Format("<bka895>{0}</bka895>", dicKey["NoType"] /*"aac002"*/) + Environment.NewLine;               // 入参类型    aac001电脑号；aac002社会保障号码；bka100社保卡号
            request += string.Format("<bka896>{0}</bka896>", dicKey["NoVal"] /*"513901198311295323"*/) + Environment.NewLine;   // 入参值
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                  // 医疗待遇类型 (根据服务获取)
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.1.3 退费时提取门诊业务信息
        /// <summary>
        /// bizh110103 退费时提取门诊业务信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz110103(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110103") + Environment.NewLine;     // 交易号
            request += string.Format("<bka895>{0}</bka895>", "aac002") + Environment.NewLine;               // 入参类型    aac001电脑号；aac002社会保障号码；bka100社保卡号
            request += string.Format("<bka896>{0}</bka896>", "513901198311295323") + Environment.NewLine;   // 入参值
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                  // 医疗待遇类型 (根据服务获取)
            request += string.Format("<bka001>{0}</bka001>", "1") + Environment.NewLine;                    // 费用批次	取bka001对应批次的费用；收费一次为“1”
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.1.4 门诊挂号
        /// <summary>
        /// 校验并保存门诊登记信息，可以不送费用信息参数集feeinfo，只送业务信息参数，这样就只保存门诊业务登记信息，不会产生计算结果。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz110104(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110104") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 
            request += string.Format("<aac001>{0}</aac001>", "1057203460") + Environment.NewLine;                  // 
            request += string.Format("<aka130>{0}</aka130>", "11") + Environment.NewLine;                  // 
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                  // 
            request += string.Format("<bka017>{0}</bka017>", "20160601") + Environment.NewLine;                  // 
            request += string.Format("<bka014>{0}</bka014>", "t10") + Environment.NewLine;                  // 
            request += string.Format("<bka015>{0}</bka015>", "test001") + Environment.NewLine;                  // 
            request += string.Format("<bka021>{0}</bka021>", "t01") + Environment.NewLine;                  // 
            request += string.Format("<bka022>{0}</bka022>", "测试病区01") + Environment.NewLine;                  // 
            request += string.Format("<bka019>{0}</bka019>", "x01") + Environment.NewLine;                  // 
            request += string.Format("<bka020>{0}</bka020>", "测试科室01") + Environment.NewLine;                  // 
            request += string.Format("<bka026>{0}</bka026>", "909.101") + Environment.NewLine;                  // 
            request += string.Format("<bka025>{0}</bka025>", "10002323210") + Environment.NewLine;                  // 
            request += string.Format("<bka070>{0}</bka070>", "") + Environment.NewLine;                  // 
            request += string.Format("<bka893>{0}</bka893>", "1") + Environment.NewLine;                  // 
            request += string.Format("<bka025>{0}</bka025>", "1000023223210") + Environment.NewLine;                  // 
            request += string.Format("<aaz267>{0}</aaz267>", "131351825") + Environment.NewLine;                  // 
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.1.5 门诊费用上传并结算
        /// <summary>
        /// 校验并保存普通门诊费用明细信息。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="lstVo"></param>
        /// <returns></returns>
        public static string Biz110105(string request, List<EntitySGS_RecipeItem> lstVo)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110105") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["JYDJH"] /*"002002160910000002"*/) + Environment.NewLine;               // 就医登记号
            request += string.Format("<bka014>{0}</bka014>", dicKey["GH"]/*"t10"*/) + Environment.NewLine;   // 工号
            request += string.Format("<bka015>{0}</bka015>", dicKey["XM"]/*"t10"*/) + Environment.NewLine;                  // 工号姓名
            request += string.Format("<bka893>{0}</bka893>", dicKey["FLAG"]/*"0"*/) + Environment.NewLine;                  // 结算标识 0费用试算、1结算收费
            request += "<feeinfo>" + Environment.NewLine;
            if (lstVo != null && lstVo.Count > 0)
            {
                foreach (EntitySGS_RecipeItem vo in lstVo)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<ake005>{0}</ake005>", vo.itemCode) + Environment.NewLine;                    // 医院药品项目编码
                    request += string.Format("<ake006>{0}</ake006>", vo.itemName) + Environment.NewLine;                    // 医院药品项目名称
                    request += string.Format("<aaz213>{0}</aaz213>", vo.sortNo) + Environment.NewLine;                      // 费用序号  如果是退费操作，需使用4.1.3接口来获取该费用序号进行传入，以确定具体要进行退费操作的药品项目；正常收费时该值传空；
                    request += string.Format("<bka052>{0}</bka052>", vo.dosageForm) + Environment.NewLine;                  // 剂型
                    request += string.Format("<bka053>{0}</bka053>", vo.vender) + Environment.NewLine;                      // 厂家
                    request += string.Format("<bka054>{0}</bka054>", vo.spec) + Environment.NewLine;                        // 规格
                    request += string.Format("<bka051>{0}</bka051>", vo.feeDate) + Environment.NewLine;                     // 费用发生日期 格式：“yyyyMMdd”
                    request += string.Format("<bka055>{0}</bka055>", vo.unit) + Environment.NewLine;                        // 计量单位
                    request += string.Format("<bka056>{0}</bka056>", vo.price) + Environment.NewLine;                       // 单价 精确到小数点后4位
                    request += string.Format("<bka057>{0}</bka057>", vo.amount) + Environment.NewLine;                      // 用量 负数时为退费 精确到小数点后2位
                    request += string.Format("<bka058>{0}</bka058>", vo.total) + Environment.NewLine;                       // 金额 精确到小数点后2位，负数时为退费
                    request += string.Format("<bka070>{0}</bka070>", vo.recipeNo) + Environment.NewLine;                    // 处方号
                    request += string.Format("<bka074>{0}</bka074>", vo.doctNo) + Environment.NewLine;                      // 处方医生编号
                    request += string.Format("<bka075>{0}</bka075>", vo.doctName) + Environment.NewLine;                    // 处方医生姓名
                    request += string.Format("<bka071>{0}</bka071>", vo.feeId) + Environment.NewLine;                       // 医院费用的唯一标识
                    request += string.Format("<aka063>{0}</aka063>", vo.limitFlag) + Environment.NewLine;                   // 限制使用标志 0 否; 1 是   或者是aka036?
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</feeinfo>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.1.6 取消门诊挂号
        /// <summary>
        /// 先调用bizh110102获取需要取消挂号的业务，然后通过医院编号和就医登记号取消门诊登记业务。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz110106(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110106") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", "002002160910000002") + Environment.NewLine;               // 就医登记号
            request += string.Format("<bka014>{0}</bka014>", "t10") + Environment.NewLine;   // 登记人员工号
            request += string.Format("<bka015>{0}</bka015>", "测试001") + Environment.NewLine;                  // 登记人姓名
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #endregion

        #region 4.2 住院业务

        #region 4.2.1 入院登记时取人员信息
        /// <summary>
        /// 在办理入院登记时，通过个人标识（电脑号、社会保障号码、社保卡号）获取参保人基本信息、工伤认定等信息。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120001(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120001") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                      // 医疗机构编码
            // 测试阶段用电脑号
            request += string.Format("<bka895>{0}</bka895>", "aac001" /*"aac002"*/) + Environment.NewLine;                   // 入参类型 aac001电脑号；aac002社会保障号码；bka100社保卡号
            request += string.Format("<bka896>{0}</bka896>", dicKey["socialNo"]) + Environment.NewLine;         // 入参值
            request += string.Format("<aka130>{0}</aka130>", "42") + Environment.NewLine;                       // 业务类型 见码表 12？ 42 工伤住院
            request += string.Format("<bka017>{0}</bka017>", dicKey["inDate"]) + Environment.NewLine;           // 住院时间 格式“yyyyMMdd”
            request += string.Format("<amc050>{0}</amc050>", " ") + Environment.NewLine;                        // 生育业务类型 工伤业务不需要考虑此字段内容
            request += string.Format("<bka912>{0}</bka912>", " ") + Environment.NewLine;                        // 生育类别 工伤业务不需要考虑此字段内容
            request += string.Format("<amc029>{0}</amc029>", " ") + Environment.NewLine;                        // 生育手术类别 工伤业务不需要考虑此字段内容
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                      // 工伤待遇类型 工伤业务必填
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.2 入院登记后取业务信息
        /// <summary>
        /// 通过输入就医登记号、住院号或个人标识（电脑号、社会保障号码、社保卡号）提取已登记的业务信息。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120102(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120102") + Environment.NewLine;     // 交易号
            request += string.Format("<bka895>{0}</bka895>", "aac001") + Environment.NewLine;                   // 入参类型 aac001电脑号；aac002社会保障号码；bka100社保卡号；aaz218就医登记号；bka025住院号
            request += string.Format("<bka896>{0}</bka896>", dicKey["computerno"]) + Environment.NewLine;         // 入参值
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;         // 入参值
            request += string.Format("<aka130>{0}</aka130>", "42") + Environment.NewLine;                       // 业务类型 见码表
            request += string.Format("<bka891>{0}</bka891>", dicKey["isCharge"]) + Environment.NewLine;         // 结算标识 1已结算0未结算
            request += string.Format("<aae030>{0}</aae030>", dicKey["beginDate"]) + Environment.NewLine;        // 开始时间 格式：yyyyMMdd
            request += string.Format("<aae031>{0}</aae031>", dicKey["endDate"]) + Environment.NewLine;          // 结束时间 格式：yyyyMMdd
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.3 校验并保存费用信息
        /// <summary>
        /// 用于校验并保存费用明细。入参分为两部分：参数和数据集“feeinfo”(费用参数集不允许为空)，如“金额”为负数，则为退费。
        /// [注] 只有在“入院登记后，出院登记前”，才可以录入保存费用明细信息。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120002(string request, List<EntitySGSFeeItem> lstFeeItems)
        {
            // feeinfo（说明：每一次上传交易不能超过300 条费用明细） 可能需要拆分多次上传
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120002") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号    ? 来源于 4.2.2 返回值
            request += "<feeinfo>" + Environment.NewLine;
            if (lstFeeItems != null && lstFeeItems.Count > 0)
            {
                foreach (EntitySGSFeeItem item in lstFeeItems)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<ake005>{0}</ake005>", item.itemCode) + Environment.NewLine;                  // 医院药品项目编码
                    request += string.Format("<ake006>{0}</ake006>", item.itemName.Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&aops;").Replace("\"", "&quot;")) + Environment.NewLine;                  // 医院药品项目名称
                    request += string.Format("<bka056>{0}</bka056>", item.price) + Environment.NewLine;                     // 单价 精确到小数点后4位
                    request += string.Format("<bka057>{0}</bka057>", item.amount) + Environment.NewLine;                    // 用量 精确到小数点后2位
                    request += string.Format("<bka058>{0}</bka058>", item.total) + Environment.NewLine;                     // 金额 精确到小数点后2位,负数时为退费
                    request += string.Format("<bka051>{0}</bka051>", item.feeDate) + Environment.NewLine;                   // 费用发生日期 格式：yyyyMMdd
                    request += string.Format("<bka063>{0}</bka063>", item.operCode) + Environment.NewLine;                  // 录入人工号
                    request += string.Format("<bka064>{0}</bka064>", item.operName) + Environment.NewLine;                  // 录入人姓名
                    request += string.Format("<bka052>{0}</bka052>", item.dosageUnit) + Environment.NewLine;                // 剂型
                    request += string.Format("<bka053>{0}</bka053>", item.factory) + Environment.NewLine;                   // 厂家
                    request += string.Format("<bka054>{0}</bka054>", item.spec) + Environment.NewLine;                      // 规格               
                    request += string.Format("<bka055>{0}</bka055>", item.unit) + Environment.NewLine;                      // 计量单位
                    request += string.Format("<bka070>{0}</bka070>", item.recipeNo) + Environment.NewLine;                  // 处方号
                    request += string.Format("<bka074>{0}</bka074>", item.doctCode) + Environment.NewLine;                  // 处方医生编号
                    request += string.Format("<bka075>{0}</bka075>", item.doctName) + Environment.NewLine;                  // 处方医生姓名
                    request += string.Format("<aaz213>{0}</aaz213>", item.sortNo /*item.sortNo*/) + Environment.NewLine;                    // 费用序列号
                    request += string.Format("<aka036>{0}</aka036>", item.limitFlag) + Environment.NewLine;                 // 限制使用标志 0 否；1 是  或者是 ?aka036
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</feeinfo>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.4 校验并计算费用信息
        /// <summary>
        /// 为便于医院中途结算，对已录入的费用进行计算（预结算），返回试算（预结算）结果。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120003(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120003") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;               // 就医登记号
            request += string.Format("<bka438>{0}</bka438>", "1") + Environment.NewLine;   // 业务场景阶段 1：业务开始 2：业务结算 3：业务结束
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.10 删除住院业务费用明细
        /// <summary>
        /// 删除本次住院业务的所有费用明细。
        /// 说明：有些地市不允许删除费用、只能退费做冲减，此交易根据地市的管理要求，确定是否提供给这个地市。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120004(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120004") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;               // 就医登记号
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.2 入院登记
        /// <summary>
        /// 用于校验并保存入院登记信息。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120103(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120103") + Environment.NewLine;         // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                          // 医疗机构编码
            request += string.Format("<aka130>{0}</aka130>", "42") + Environment.NewLine;                           // 医疗机构编码
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                          // 医疗待遇类型 见码表
            request += string.Format("<aac001>{0}</aac001>", dicKey["computerno"]) + Environment.NewLine;           // 电脑号
            request += string.Format("<bka017>{0}</bka017>", dicKey["inDate"]) + Environment.NewLine;               // 住院时间 格式：yyyyMMdd
            request += string.Format("<bka014>{0}</bka014>", dicKey["operCode"]) + Environment.NewLine;             // 登记人员工号
            request += string.Format("<bka015>{0}</bka015>", dicKey["operName"]) + Environment.NewLine;             // 登记人姓名
            request += string.Format("<bka021>{0}</bka021>", dicKey["areaCode"]) + Environment.NewLine;             // 病区编码
            request += string.Format("<bka022>{0}</bka022>", dicKey["areaName"]) + Environment.NewLine;             // 病区名称
            request += string.Format("<bka019>{0}</bka019>", dicKey["deptCode"]) + Environment.NewLine;             // 就诊科室
            request += string.Format("<bka020>{0}</bka020>", dicKey["deptName"]) + Environment.NewLine;             // 就诊科室名称
            request += string.Format("<bka026>{0}</bka026>", dicKey["icdCode"]) + Environment.NewLine;              // 诊断 疾病ICD编码
            request += string.Format("<bka025>{0}</bka025>", dicKey["ipNo"]) + Environment.NewLine;                 // 住院号
            request += string.Format("<bka023>{0}</bka023>", dicKey["bedNo"]) + Environment.NewLine;                // 床位号
            request += string.Format("<bka024>{0}</bka024>", "1") + Environment.NewLine;                            // 床位类型
            request += string.Format("<bka043>{0}</bka043>", " ") + Environment.NewLine;                            // 备注
            request += string.Format("<bka503>{0}</bka503>", dicKey["doctCode"]) + Environment.NewLine;             // 医师编码
            request += string.Format("<bkc500>{0}</bkc500>", " ") + Environment.NewLine;                            // 中途结算 工伤业务不需要考虑此字段内容            
            request += string.Format("<ykc679>{0}</ykc679>", " ") + Environment.NewLine;                            // 住院原因
            request += string.Format("<ykc680>{0}</ykc680>", " ") + Environment.NewLine;                            // 补助类型
            request += string.Format("<aaz065>{0}</aaz065>", " ") + Environment.NewLine;                            // 银行ID
            request += string.Format("<aae009>{0}</aae009>", " ") + Environment.NewLine;                            // 银行户名
            request += string.Format("<aae010>{0}</aae010>", " ") + Environment.NewLine;                            // 银行账号
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;

            // 返回字段
            // request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                // 就医登记号
        }
        #endregion

        #region 4.2.7 入院登记信息修改
        /// <summary>
        /// 用于校验并修改入院登记信息。仅可修改的内容：入院病区、入院病区名称、入院科室、入院科室名称、医院业务号(住院号)、入院床位号。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120104(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120104") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号
            request += string.Format("<bka021>{0}</bka021>", dicKey["areaCode"]) + Environment.NewLine;                 // 病区编码
            request += string.Format("<bka022>{0}</bka022>", dicKey["areaName"]) + Environment.NewLine;                 // 病区名称
            request += string.Format("<bka019>{0}</bka019>", dicKey["deptCode"]) + Environment.NewLine;                 // 就诊科室
            request += string.Format("<bka020>{0}</bka020>", dicKey["deptName"]) + Environment.NewLine;                 // 就诊科室名称
            request += string.Format("<bka025>{0}</bka025>", dicKey["ipNo"]) + Environment.NewLine;                     // 住院号
            request += string.Format("<bka023>{0}</bka023>", dicKey["bedNo"]) + Environment.NewLine;                    // 入院床位号
            request += string.Format("<bka503>{0}</bka503>", dicKey["doctCode"]) + Environment.NewLine;                 // 医师编号
            request += string.Format("<ykc679>{0}</ykc679>", " ") + Environment.NewLine;                                 //
            request += string.Format("<ykc680>{0}</ykc680>", " ") + Environment.NewLine;                                 //
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.5 出院登记
        /// <summary>
        /// 保存在院病人的出院登记信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120105(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120105") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号
            request += string.Format("<bka032>{0}</bka032>", dicKey["outDate"]) + Environment.NewLine;                  // 出院日期 格式：yyyyMMdd
            request += string.Format("<bka033>{0}</bka033>", dicKey["operCode"]) + Environment.NewLine;                 // 登记人员工号
            request += string.Format("<bka034>{0}</bka034>", dicKey["operName"]) + Environment.NewLine;                 // 登记人姓名
            request += string.Format("<bkf001>{0}</bkf001>", dicKey["bloodType"]) + Environment.NewLine;                // 血型
            request += string.Format("<bkf002>{0}</bkf002>", dicKey["inWay"]) + Environment.NewLine;                    // 入院方式
            request += string.Format("<bkf003>{0}</bkf003>", dicKey["inStatus"]) + Environment.NewLine;                 // 入院情况
            request += string.Format("<bkf004>{0}</bkf004>", dicKey["zgqk"]) + Environment.NewLine;                     // 出院转归情况
            request += string.Format("<bkf005>{0}</bkf005>", dicKey["qjcs"]) + Environment.NewLine;                     // 抢救次数
            request += string.Format("<bkf006>{0}</bkf006>", dicKey["qjcgcs"]) + Environment.NewLine;                   // 抢救成功次数
            request += string.Format("<bka031>{0}</bka031>", dicKey["outdiagicd10Code"]) + Environment.NewLine;                  // 出院诊断
            request += string.Format("<bka043>{0}</bka043>", dicKey["outDesc"]) + Environment.NewLine;                  // 出院说明
            request += string.Format("<amc050>{0}</amc050>", " ") + Environment.NewLine;                                // 生育业务类型
            request += string.Format("<amc029>{0}</amc029>", " ") + Environment.NewLine;                                // 生育手术类别
            request += string.Format("<amc031>{0}</amc031>", " ") + Environment.NewLine;                                // 胎次
            request += string.Format("<bka911>{0}</bka911>", " ") + Environment.NewLine;                                // 手术日期
            request += string.Format("<bka912>{0}</bka912>", " ") + Environment.NewLine;                                // 生育类别
            request += string.Format("<bka913>{0}</bka913>", " ") + Environment.NewLine;                                // 胎儿数
            request += string.Format("<bka914>{0}</bka914>", " ") + Environment.NewLine;                                // 母亲情况
            request += string.Format("<bka915>{0}</bka915>", " ") + Environment.NewLine;                                // 母亲死亡时间
            request += string.Format("<bka916>{0}</bka916>", " ") + Environment.NewLine;                                // 婴儿情况
            request += string.Format("<bka917>{0}</bka917>", " ") + Environment.NewLine;                                // 婴儿死亡时间
            request += string.Format("<ykc195>{0}</ykc195>", " ") + Environment.NewLine;                                // 出院原因
            request += string.Format("<akb063>{0}</akb063>", " ") + Environment.NewLine;                                // 住院天数
            request += string.Format("<bka856>{0}</bka856>", " ") + Environment.NewLine;                                // 血透次数
            request += string.Format("<bka006>{0}</bka006>", "420") + Environment.NewLine;                              // 待遇类型
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.6 出院结算
        /// <summary>
        /// 保存在院病人的出院结算信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120106(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120106") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号
            request += string.Format("<bka046>{0}</bka046>", dicKey["operCode"]) + Environment.NewLine;                 // 完成人工号
            request += string.Format("<bka047>{0}</bka047>", dicKey["operName"]) + Environment.NewLine;                 // 完成人
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.11 取消出院结算
        /// <summary>
        /// 取消出院结算操作，业务所在阶段变更为已出院登记，未出院结算状态。可调用取消出院登记继续往回回退业务状态，或重新办理出院结算终结业务。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120107(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120107") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.9 取消出院登记
        /// <summary>
        /// 取消出院登记操作，业务所在阶段变更为已入院登记，未出院登记状态。可调用取消入院登记回退业务状态，或重新办理出院登记。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120108(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120108") + Environment.NewLine;             // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                              // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                    // 就医登记号
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.2.8 取消入院登记
        /// <summary>
        /// 取消入院登记操作，将本次住院业务全部取消。
        /// [注] 如未取消入院登记，参保人还是处于在院状态，此时去别的医院办理业务将会提示业务互斥。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120109(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(request, "request");
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120109") + Environment.NewLine;         // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;                          // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", dicKey["jydjh"]) + Environment.NewLine;                // 就医登记号
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.4.1 累计查询接口
        /// <summary>
        /// 累计信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz410004(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh410004") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aac001>{0}</aac001>", "") + Environment.NewLine;               // 电脑号
            request += string.Format("<baa027>{0}</baa027>", "") + Environment.NewLine;               // 个人所属统筹区
            request += string.Format("<aae001>{0}</aae001>", "") + Environment.NewLine;               // 工伤保险年度【业务期间】
            request += string.Format("<aae140>{0}</aae140>", "") + Environment.NewLine;               // 险种编码
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.4.2 基金状态查询接口
        /// <summary>
        /// 基金状态查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz410005(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh410005") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aac001>{0}</aac001>", "") + Environment.NewLine;               // 电脑号
            request += string.Format("<bka977>{0}</bka977>", "") + Environment.NewLine;               // 标识（通用）开关1、0
            request += string.Format("<aae030>{0}</aae030>", "") + Environment.NewLine;               // 开始日期
            request += string.Format("<aae031>{0}</aae031>", "") + Environment.NewLine;               // 结束日期
            request += string.Format("<aae140>{0}</aae140>", "") + Environment.NewLine;               // 险种编码
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.4.3 码表服务接口
        /// <summary>
        /// 码表名称查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz120205(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh120205") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaa100>{0}</aaa100>", "") + Environment.NewLine;               // 代码类别
            request += string.Format("<aaa102>{0}</aaa102>", "") + Environment.NewLine;               // 代码码值
            request += string.Format("<aaa104>{0}</aaa104>", "") + Environment.NewLine;               // 预留(扩展)            
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #endregion

        #region 4.3 病案信息上传

        #region 4.3.1 住院病人信息（病案首页）录入
        /// <summary>
        /// 提交病案首页中住院病人的相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz200101(string request)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh110001") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医院编号
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致
            request += string.Format("<yzy001>{0}</yzy001>", " ") + Environment.NewLine;                    // 病案号
            request += string.Format("<yzy002>{0}</yzy002>", " ") + Environment.NewLine;                    // 住院次数
            request += string.Format("<yzy003>{0}</yzy003>", " ") + Environment.NewLine;                    // ICD版本
            request += string.Format("<yzy004>{0}</yzy004>", " ") + Environment.NewLine;                    // 住院流水号
            request += string.Format("<akc023>{0}</akc023>", " ") + Environment.NewLine;                    // 年龄
            request += string.Format("<aac003>{0}</aac003>", " ") + Environment.NewLine;                    // 性别编号
            request += string.Format("<yzy008>{0}</yzy008>", " ") + Environment.NewLine;                    // 性别
            request += string.Format("<aac006>{0}</aac006>", " ") + Environment.NewLine;                    // 出生日期 YYYYMMDD
            request += string.Format("<yzy010>{0}</yzy010>", " ") + Environment.NewLine;                    // 出生地
            request += string.Format("<yzy011>{0}</yzy011>", " ") + Environment.NewLine;                    // 身份证号
            request += string.Format("<aac161>{0}</aac161>", " ") + Environment.NewLine;                    // 国籍编号
            request += string.Format("<yzy013>{0}</yzy013>", " ") + Environment.NewLine;                    // 国籍
            request += string.Format("<aac005>{0}</aac005>", " ") + Environment.NewLine;                    // 民族编号
            request += string.Format("<yzy015>{0}</yzy015>", " ") + Environment.NewLine;                    // 民族
            request += string.Format("<yzy016>{0}</yzy016>", " ") + Environment.NewLine;                    // 职业
            request += string.Format("<aac017>{0}</aac017>", " ") + Environment.NewLine;                    // 婚姻状况编号
            request += string.Format("<yzy018>{0}</yzy018>", " ") + Environment.NewLine;                    // 婚姻状况
            request += string.Format("<aab004>{0}</aab004>", " ") + Environment.NewLine;                    // 单位名称
            request += string.Format("<yzy020>{0}</yzy020>", " ") + Environment.NewLine;                    // 单位地址
            request += string.Format("<yzy021>{0}</yzy021>", " ") + Environment.NewLine;                    // 单位电话
            request += string.Format("<yzy022>{0}</yzy022>", " ") + Environment.NewLine;                    // 单位邮编
            request += string.Format("<aac010>{0}</aac010>", " ") + Environment.NewLine;                    // 户口地址
            request += string.Format("<yzy024>{0}</yzy024>", " ") + Environment.NewLine;                    // 户口邮编
            request += string.Format("<aae004>{0}</aae004>", " ") + Environment.NewLine;                    // 联系人
            request += string.Format("<yzy026>{0}</yzy026>", " ") + Environment.NewLine;                    // 与病人关系
            request += string.Format("<yzy027>{0}</yzy027>", " ") + Environment.NewLine;                    // 联系人地址
            request += string.Format("<yzy028>{0}</yzy028>", " ") + Environment.NewLine;                    // 联系人电话
            request += string.Format("<yzy029>{0}</yzy029>", " ") + Environment.NewLine;                    // 健康卡号
            request += string.Format("<ykc701>{0}</ykc701>", " ") + Environment.NewLine;                    // 入院日期
            request += string.Format("<yzy032>{0}</yzy032>", " ") + Environment.NewLine;                    // 入院统一科号
            request += string.Format("<yzy033>{0}</yzy033>", " ") + Environment.NewLine;                    // 入院科别
            request += string.Format("<yzy034>{0}</yzy034>", " ") + Environment.NewLine;                    // 入院病室
            request += string.Format("<ykc702>{0}</ykc702>", " ") + Environment.NewLine;                    // 出院日期
            request += string.Format("<yzy037>{0}</yzy037>", " ") + Environment.NewLine;                    // 出院统一科号
            request += string.Format("<yzy038>{0}</yzy038>", " ") + Environment.NewLine;                    // 出院科别
            request += string.Format("<yzy039>{0}</yzy039>", " ") + Environment.NewLine;                    // 出院病室
            request += string.Format("<akb063>{0}</akb063>", " ") + Environment.NewLine;                    // 实际住院天数
            request += string.Format("<akc193>{0}</akc193>", " ") + Environment.NewLine;                    // 门（急）诊诊断编码
            request += string.Format("<akc050>{0}</akc050>", " ") + Environment.NewLine;                    // 门（急）诊诊断疾病名
            request += string.Format("<yzy043>{0}</yzy043>", " ") + Environment.NewLine;                    // 门、急诊医生编号
            request += string.Format("<ake022>{0}</ake022>", " ") + Environment.NewLine;                    // 门、急诊医生
            request += string.Format("<yzy045>{0}</yzy045>", " ") + Environment.NewLine;                    // 病理诊断
            request += string.Format("<yzy046>{0}</yzy046>", " ") + Environment.NewLine;                    // 过敏药物
            request += string.Format("<yzy047>{0}</yzy047>", " ") + Environment.NewLine;                    // 抢救次数
            request += string.Format("<yzy048>{0}</yzy048>", " ") + Environment.NewLine;                    // 抢救成功次数
            request += string.Format("<yzy049>{0}</yzy049>", " ") + Environment.NewLine;                    // 科主任编号
            request += string.Format("<yzy050>{0}</yzy050>", " ") + Environment.NewLine;                    // 科主任
            request += string.Format("<yzy051>{0}</yzy051>", " ") + Environment.NewLine;                    // 主（副主）任医生编号
            request += string.Format("<yzy052>{0}</yzy052>", " ") + Environment.NewLine;                    // 主（副主）任医生
            request += string.Format("<yzy053>{0}</yzy053>", " ") + Environment.NewLine;                    // 主治医生编号
            request += string.Format("<yzy054>{0}</yzy054>", " ") + Environment.NewLine;                    // 主治医生
            request += string.Format("<yzy055>{0}</yzy055>", " ") + Environment.NewLine;                    // 住院医生编号
            request += string.Format("<yzy056>{0}</yzy056>", " ") + Environment.NewLine;                    // 住院医生
            request += string.Format("<yzy057>{0}</yzy057>", " ") + Environment.NewLine;                    // 进修医师编号
            request += string.Format("<yzy058>{0}</yzy058>", " ") + Environment.NewLine;                    // 进修医师
            request += string.Format("<yzy059>{0}</yzy059>", " ") + Environment.NewLine;                    // 实习医师编号
            request += string.Format("<yzy060>{0}</yzy060>", " ") + Environment.NewLine;                    // 实习医师
            request += string.Format("<yzy061>{0}</yzy061>", " ") + Environment.NewLine;                    // 编码员编号
            request += string.Format("<yzy062>{0}</yzy062>", " ") + Environment.NewLine;                    // 编码员
            request += string.Format("<yzy063>{0}</yzy063>", " ") + Environment.NewLine;                    // 病案质量编号  1,2,3
            request += string.Format("<yzy064>{0}</yzy064>", " ") + Environment.NewLine;                    // 病案质量 甲,乙,丙
            request += string.Format("<yzy065>{0}</yzy065>", " ") + Environment.NewLine;                    // 质控医师编号
            request += string.Format("<yzy066>{0}</yzy066>", " ") + Environment.NewLine;                    // 质控医师
            request += string.Format("<yzy067>{0}</yzy067>", " ") + Environment.NewLine;                    // 质控护士编号
            request += string.Format("<yzy068>{0}</yzy068>", " ") + Environment.NewLine;                    // 质控护士
            request += string.Format("<yzy069>{0}</yzy069>", " ") + Environment.NewLine;                    // 质控日期
            request += string.Format("<akc264>{0}</akc264>", " ") + Environment.NewLine;                    // 总费用
            request += string.Format("<ake047>{0}</ake047>", " ") + Environment.NewLine;                    // 西药费
            request += string.Format("<yzy072>{0}</yzy072>", " ") + Environment.NewLine;                    // 中药费
            request += string.Format("<ake050>{0}</ake050>", " ") + Environment.NewLine;                    // 中成药费
            request += string.Format("<ake049>{0}</ake049>", " ") + Environment.NewLine;                    // 中草药费
            request += string.Format("<ake044>{0}</ake044>", " ") + Environment.NewLine;                    // 其他费
            request += string.Format("<yzy076>{0}</yzy076>", " ") + Environment.NewLine;                    // 是否尸检编号
            request += string.Format("<yzy077>{0}</yzy077>", " ") + Environment.NewLine;                    // 是否尸检
            request += string.Format("<yzy078>{0}</yzy078>", " ") + Environment.NewLine;                    // 血型编号 1,2,3,4,5,6
            request += string.Format("<yzy079>{0}</yzy079>", " ") + Environment.NewLine;                    // 血型 A型，B型，O型，AB型，不详，未查
            request += string.Format("<yzy080>{0}</yzy080>", " ") + Environment.NewLine;                    // RH编号 1,2,3,4
            request += string.Format("<yzy081>{0}</yzy081>", " ") + Environment.NewLine;                    // RH 阴，阳，不详，未查
            request += string.Format("<yzy082>{0}</yzy082>", " ") + Environment.NewLine;                    // 首次转科统一科号 
            request += string.Format("<yzy083>{0}</yzy083>", " ") + Environment.NewLine;                    // 首次转科科别
            request += string.Format("<yzy084>{0}</yzy084>", " ") + Environment.NewLine;                    // 首次转科日期
            request += string.Format("<yzy085>{0}</yzy085>", " ") + Environment.NewLine;                    // 首次转科时间
            request += string.Format("<yzy086>{0}</yzy086>", " ") + Environment.NewLine;                    // 疾病分型编号
            request += string.Format("<yzy087>{0}</yzy087>", " ") + Environment.NewLine;                    // 疾病分型
            request += string.Format("<yzy088>{0}</yzy088>", " ") + Environment.NewLine;                    // 籍贯
            request += string.Format("<yzy089>{0}</yzy089>", " ") + Environment.NewLine;                    // 现住址
            request += string.Format("<yzy090>{0}</yzy090>", " ") + Environment.NewLine;                    // 现电话
            request += string.Format("<yzy091>{0}</yzy091>", " ") + Environment.NewLine;                    // 现邮编
            request += string.Format("<aca111>{0}</aca111>", " ") + Environment.NewLine;                    // 职业编号
            request += string.Format("<yzy093>{0}</yzy093>", " ") + Environment.NewLine;                    // 新生儿出生体重
            request += string.Format("<yzy094>{0}</yzy094>", " ") + Environment.NewLine;                    // 新生儿入院体重
            request += string.Format("<yzy095>{0}</yzy095>", " ") + Environment.NewLine;                    // 入院途径编号 1,2,3,9
            request += string.Format("<yzy096>{0}</yzy096>", " ") + Environment.NewLine;                    // 入院途径 急诊，门诊，其它医疗机构转入，其它
            request += string.Format("<yzy097>{0}</yzy097>", " ") + Environment.NewLine;                    // 临床路径病例编号 1,2
            request += string.Format("<yzy098>{0}</yzy098>", " ") + Environment.NewLine;                    // 临床路径病例 是，否
            request += string.Format("<yzy099>{0}</yzy099>", " ") + Environment.NewLine;                    // 病理疾病编码
            request += string.Format("<yzy100>{0}</yzy100>", " ") + Environment.NewLine;                    // 病理号
            request += string.Format("<yzy101>{0}</yzy101>", " ") + Environment.NewLine;                    // 是否药物过敏编号 1,2
            request += string.Format("<yzy102>{0}</yzy102>", " ") + Environment.NewLine;                    // 是否药物过敏 1无，2有
            request += string.Format("<yzy103>{0}</yzy103>", " ") + Environment.NewLine;                    // 责任护士编号
            request += string.Format("<yzy104>{0}</yzy104>", " ") + Environment.NewLine;                    // 责任护士
            request += string.Format("<yzy105>{0}</yzy105>", " ") + Environment.NewLine;                    // 离院方式编号 1,2,3,4,5,6
            request += string.Format("<yzy106>{0}</yzy106>", " ") + Environment.NewLine;                    // 离院方式 1医嘱离院，2医嘱转院，3医嘱转社区服务机构/卫生院，4非医嘱离院，5死亡，6其它
            request += string.Format("<yzy107>{0}</yzy107>", " ") + Environment.NewLine;                    // 离院方式为医嘱转院，拟接收医疗机构名称
            request += string.Format("<yzy108>{0}</yzy108>", " ") + Environment.NewLine;                    // 离院方式为转社区卫生服务器机构/乡镇卫生院，拟接收医疗机构名称
            request += string.Format("<yzy109>{0}</yzy109>", " ") + Environment.NewLine;                    // 是否有出院31天内再住院计划编号 1,2
            request += string.Format("<yzy110>{0}</yzy110>", " ") + Environment.NewLine;                    // 是否有出院31天内再住院计划  1无，2有
            request += string.Format("<yzy111>{0}</yzy111>", " ") + Environment.NewLine;                    // 再住院目的
            request += string.Format("<yzy112>{0}</yzy112>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院前天
            request += string.Format("<yzy113>{0}</yzy113>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院前 小时
            request += string.Format("<yzy114>{0}</yzy114>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院前 分钟
            request += string.Format("<yzy115>{0}</yzy115>", " ") + Environment.NewLine;                    // 入院前昏迷总分钟
            request += string.Format("<yzy116>{0}</yzy116>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院后 天
            request += string.Format("<yzy117>{0}</yzy117>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院后 小时
            request += string.Format("<yzy118>{0}</yzy118>", " ") + Environment.NewLine;                    // 颅脑损伤患者昏迷时间：入院后 分钟
            request += string.Format("<yzy119>{0}</yzy119>", " ") + Environment.NewLine;                    // 入院后昏迷总分钟
            request += string.Format("<yzy120>{0}</yzy120>", " ") + Environment.NewLine;                    // 付款方式编号
            request += string.Format("<yzy121>{0}</yzy121>", " ") + Environment.NewLine;                    // 付款方式
            request += string.Format("<yzy122>{0}</yzy122>", " ") + Environment.NewLine;                    // 住院总费用：自费金额
            request += string.Format("<yzy123>{0}</yzy123>", " ") + Environment.NewLine;                    // 综合医疗服务类：（1）一般医疗服务费
            request += string.Format("<yzy124>{0}</yzy124>", " ") + Environment.NewLine;                    // 综合医疗服务类：（2）一般治疗操作费
            request += string.Format("<yzy125>{0}</yzy125>", " ") + Environment.NewLine;                    // 综合医疗服务类：（3）护理费
            request += string.Format("<yzy126>{0}</yzy126>", " ") + Environment.NewLine;                    // 综合医疗服务类：（4）其他费用
            request += string.Format("<yzy127>{0}</yzy127>", " ") + Environment.NewLine;                    // 诊断类：(5) 病理诊断费
            request += string.Format("<yzy128>{0}</yzy128>", " ") + Environment.NewLine;                    // 诊断类：(6) 实验室诊断费
            request += string.Format("<yzy129>{0}</yzy129>", " ") + Environment.NewLine;                    // 诊断类：(7) 影像学诊断费
            request += string.Format("<yzy130>{0}</yzy130>", " ") + Environment.NewLine;                    // 诊断类：(8) 临床诊断项目费
            request += string.Format("<yzy131>{0}</yzy131>", " ") + Environment.NewLine;                    // 治疗类：(9) 非手术治疗项目费
            request += string.Format("<yzy132>{0}</yzy132>", " ") + Environment.NewLine;                    // 治疗类：非手术治疗项目费 其中临床物理治疗费
            request += string.Format("<yzy133>{0}</yzy133>", " ") + Environment.NewLine;                    // 治疗类：(10) 手术治疗费
            request += string.Format("<yzy134>{0}</yzy134>", " ") + Environment.NewLine;                    // 治疗类：手术治疗费 其中麻醉费
            request += string.Format("<yzy135>{0}</yzy135>", " ") + Environment.NewLine;                    // 治疗类：手术治疗费 其中手术费
            request += string.Format("<yzy136>{0}</yzy136>", " ") + Environment.NewLine;                    // 康复类：(11) 康复费
            request += string.Format("<yzy137>{0}</yzy137>", " ") + Environment.NewLine;                    // 中医类：中医治疗类
            request += string.Format("<yzy138>{0}</yzy138>", " ") + Environment.NewLine;                    // 西药类： 西药费 其中抗菌药物费用
            request += string.Format("<yzy139>{0}</yzy139>", " ") + Environment.NewLine;                    // 血液和血液制品类： 血费
            request += string.Format("<yzy140>{0}</yzy140>", " ") + Environment.NewLine;                    // 血液和血液制品类： 白蛋白类制品费
            request += string.Format("<yzy141>{0}</yzy141>", " ") + Environment.NewLine;                    // 血液和血液制品类： 球蛋白制品费
            request += string.Format("<yzy142>{0}</yzy142>", " ") + Environment.NewLine;                    // 血液和血液制品类：凝血因子类制品费
            request += string.Format("<yzy143>{0}</yzy143>", " ") + Environment.NewLine;                    // 血液和血液制品类： 细胞因子类费
            request += string.Format("<yzy144>{0}</yzy144>", " ") + Environment.NewLine;                    // 耗材类：检查用一次性医用材料费
            request += string.Format("<yzy145>{0}</yzy145>", " ") + Environment.NewLine;                    // 耗材类：治疗用一次性医用材料费
            request += string.Format("<yzy146>{0}</yzy146>", " ") + Environment.NewLine;                    // 耗材类：手术用一次性医用材料费
            request += string.Format("<yzy147>{0}</yzy147>", " ") + Environment.NewLine;                    // 综合医疗服务类：一般医疗服务费 其中中医辨证论治费（中医）
            request += string.Format("<yzy148>{0}</yzy148>", " ") + Environment.NewLine;                    // 综合医疗服务类：一般医疗服务费 其中中医辨证论治会诊费（中医）
            request += string.Format("<yzy149>{0}</yzy149>", " ") + Environment.NewLine;                    // 中医类：诊断（中医）
            request += string.Format("<yzy150>{0}</yzy150>", " ") + Environment.NewLine;                    // 中医类：治疗（中医）
            request += string.Format("<yzy151>{0}</yzy151>", " ") + Environment.NewLine;                    // 中医类：治疗 其中外治（中医）
            request += string.Format("<yzy152>{0}</yzy152>", " ") + Environment.NewLine;                    // 中医类：治疗 其中骨伤（中医）
            request += string.Format("<yzy153>{0}</yzy153>", " ") + Environment.NewLine;                    // 中医类：治疗 其中针刺与灸法（中医）
            request += string.Format("<yzy154>{0}</yzy154>", " ") + Environment.NewLine;                    // 中医类：治疗推拿治疗（中医）
            request += string.Format("<yzy155>{0}</yzy155>", " ") + Environment.NewLine;                    // 中医类：治疗 其中肛肠治疗（中医）
            request += string.Format("<yzy156>{0}</yzy156>", " ") + Environment.NewLine;                    // 中医类：治疗 其中特殊治疗（中医）
            request += string.Format("<yzy157>{0}</yzy157>", " ") + Environment.NewLine;                    // 中医类：其他（中医）
            request += string.Format("<yzy158>{0}</yzy158>", " ") + Environment.NewLine;                    // 中医类：其他 其中中药特殊调配加工（中医）
            request += string.Format("<yzy159>{0}</yzy159>", " ") + Environment.NewLine;                    // 中医类：其他 其中辨证施膳（中医）
            request += string.Format("<yzy160>{0}</yzy160>", " ") + Environment.NewLine;                    // 中药类：中成药费 其中医疗机构中药制剂费（中医）
            request += string.Format("<yzy161>{0}</yzy161>", " ") + Environment.NewLine;                    // 治疗类别编号（中医类）
            request += string.Format("<yzy162>{0}</yzy162>", " ") + Environment.NewLine;                    // 治疗类别（中医类）
            request += string.Format("<yzy163>{0}</yzy163>", " ") + Environment.NewLine;                    // 门（急）诊中医诊断编码（中医类）
            request += string.Format("<yzy164>{0}</yzy164>", " ") + Environment.NewLine;                    // 门（急）诊中医诊断（中医类）
            request += string.Format("<yzy165>{0}</yzy165>", " ") + Environment.NewLine;                    // 实施临床路径编号（中医类）
            request += string.Format("<yzy166>{0}</yzy166>", " ") + Environment.NewLine;                    // 实施临床路径（中医类）
            request += string.Format("<yzy167>{0}</yzy167>", " ") + Environment.NewLine;                    // 使用医疗机构中药制剂编号（中医类）
            request += string.Format("<yzy168>{0}</yzy168>", " ") + Environment.NewLine;                    // 使用医疗机构中药制剂（中医类）
            request += string.Format("<yzy169>{0}</yzy169>", " ") + Environment.NewLine;                    // 使用中医诊疗设备编号（中医类）
            request += string.Format("<yzy170>{0}</yzy170>", " ") + Environment.NewLine;                    // 使用中医诊疗设备（中医类）
            request += string.Format("<yzy171>{0}</yzy171>", " ") + Environment.NewLine;                    // 使用中医诊疗技术编号（中医类）
            request += string.Format("<yzy172>{0}</yzy172>", " ") + Environment.NewLine;                    // 使用中医诊疗技术（中医类）
            request += string.Format("<yzy173>{0}</yzy173>", " ") + Environment.NewLine;                    // 辨证施护编号（中医类）
            request += string.Format("<yzy174>{0}</yzy174>", " ") + Environment.NewLine;                    // 辨证施护（中医类）
            request += string.Format("<yzy175>{0}</yzy175>", " ") + Environment.NewLine;                    // 医院感染次数
            request += string.Format("<yzy180>{0}</yzy180>", " ") + Environment.NewLine;                    // 扩展1
            request += string.Format("<yzy181>{0}</yzy181>", " ") + Environment.NewLine;                    // 扩展2
            request += string.Format("<yzy182>{0}</yzy182>", " ") + Environment.NewLine;                    // 扩展3
            request += string.Format("<yzy183>{0}</yzy183>", " ") + Environment.NewLine;                    // 扩展4
            request += string.Format("<yzy184>{0}</yzy184>", " ") + Environment.NewLine;                    // 扩展5
            request += string.Format("<yzy185>{0}</yzy185>", " ") + Environment.NewLine;                    // 扩展6
            request += string.Format("<yzy186>{0}</yzy186>", " ") + Environment.NewLine;                    // 扩展7
            request += string.Format("<yzy187>{0}</yzy187>", " ") + Environment.NewLine;                    // 扩展8
            request += string.Format("<yzy188>{0}</yzy188>", " ") + Environment.NewLine;                    // 扩展9
            request += string.Format("<yzy189>{0}</yzy189>", " ") + Environment.NewLine;                    // 扩展10
            request += string.Format("<yzy190>{0}</yzy190>", " ") + Environment.NewLine;                    // 扩展11
            request += string.Format("<yzy191>{0}</yzy191>", " ") + Environment.NewLine;                    // 扩展12
            request += string.Format("<yzy192>{0}</yzy192>", " ") + Environment.NewLine;                    // 扩展13
            request += string.Format("<yzy193>{0}</yzy193>", " ") + Environment.NewLine;                    // 扩展14
            request += string.Format("<yzy194>{0}</yzy194>", " ") + Environment.NewLine;                    // 扩展15
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.3.2 住院病人诊断信息（病案首页）录入
        /// <summary>
        /// 提交病案首页中住院病人的诊断相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static string Biz200102(string request, DataTable dt)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh200102") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致
            request += string.Format("<yzy003>{0}</yzy003>", "") + Environment.NewLine;                     // ICD版本
            request += "<detail>" + Environment.NewLine;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<yzy201>{0}</yzy201>", "") + Environment.NewLine;               // 排序
                    request += string.Format("<yzy202>{0}</yzy202>", "") + Environment.NewLine;               // 诊断类型
                    request += string.Format("<akc185>{0}</akc185>", "") + Environment.NewLine;               // 疾病名称
                    request += string.Format("<akc196>{0}</akc196>", "") + Environment.NewLine;               // ICD码
                    request += string.Format("<yzy205>{0}</yzy205>", "") + Environment.NewLine;               // 入院病情编号
                    request += string.Format("<yzy206>{0}</yzy206>", "") + Environment.NewLine;               // 入院病情
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</detail>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.3.3 住院病人手术信息（病案首页）录入
        /// <summary>
        /// 提交病案首页中住院病人的手术相关信息。选填接口，当病人发生手术治疗时需要提交
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        static string Biz200103(string request, DataTable dt)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh200103") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致
            request += "<detail>" + Environment.NewLine;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<yzy201>{0}</yzy201>", "") + Environment.NewLine;               // 排序
                    request += string.Format("<yzy901>{0}</yzy901>", "") + Environment.NewLine;               // 手术码类型
                    request += string.Format("<yzy207>{0}</yzy207>", "") + Environment.NewLine;               // 手术码
                    request += string.Format("<yzy208>{0}</yzy208>", "") + Environment.NewLine;               // 手术码对应名称
                    request += string.Format("<yzy209>{0}</yzy209>", "") + Environment.NewLine;               // 手术日期 yyymmdd
                    request += string.Format("<yzy210>{0}</yzy210>", "") + Environment.NewLine;               // 切口编号 1,2,3,4
                    request += string.Format("<yzy211>{0}</yzy211>", "") + Environment.NewLine;               // 切口 1.0类切口 2.Ⅰ类切口 3.Ⅱ类切口 4.Ⅲ类切口
                    request += string.Format("<yzy212>{0}</yzy212>", "") + Environment.NewLine;               // 愈合编号 1,2,3,4
                    request += string.Format("<yzy213>{0}</yzy213>", "") + Environment.NewLine;               // 愈合 1.切口愈合良好 2.切口愈合欠佳 3.切口化脓 4.切口愈合情况不确定
                    request += string.Format("<yzy214>{0}</yzy214>", "") + Environment.NewLine;               // 手术医生编号
                    request += string.Format("<yzy215>{0}</yzy215>", "") + Environment.NewLine;               // 手术医生
                    request += string.Format("<yzy216>{0}</yzy216>", "") + Environment.NewLine;               // 麻醉方式编号
                    request += string.Format("<yzy217>{0}</yzy217>", "") + Environment.NewLine;               // 麻醉方式
                    request += string.Format("<yzy218>{0}</yzy218>", "") + Environment.NewLine;               // 是否附加手术
                    request += string.Format("<yzy219>{0}</yzy219>", "") + Environment.NewLine;               // I 助编号
                    request += string.Format("<yzy220>{0}</yzy220>", "") + Environment.NewLine;               // I 助姓名
                    request += string.Format("<yzy221>{0}</yzy221>", "") + Environment.NewLine;               // II 助编号
                    request += string.Format("<yzy222>{0}</yzy222>", "") + Environment.NewLine;               // II 助姓名
                    request += string.Format("<yzy223>{0}</yzy223>", "") + Environment.NewLine;               // 麻醉医生编号
                    request += string.Format("<yzy224>{0}</yzy224>", "") + Environment.NewLine;               // 麻醉医生
                    request += string.Format("<yzy225>{0}</yzy225>", "") + Environment.NewLine;               // 择期手术编号
                    request += string.Format("<yzy226>{0}</yzy226>", "") + Environment.NewLine;               // 择期手术
                    request += string.Format("<yzy227>{0}</yzy227>", "") + Environment.NewLine;               // 手术级别编号 1，2,3,4
                    request += string.Format("<yzy228>{0}</yzy228>", "") + Environment.NewLine;               // 手术级别 1.一级手术 2.二级手术 3.三级手术 4.四级手术
                    request += string.Format("<bkb135>{0}</bkb135>", "") + Environment.NewLine;               // 诊疗代码 见码表诊疗代码bkb135
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</detail>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.3.4 住院病人产科分娩婴儿信息（病案首页）录入
        /// <summary>
        /// 住院病人产科分娩婴儿信息（病案首页）录入
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        static string Biz200104(string request, DataTable dt)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh200104") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致
            request += "<detail>" + Environment.NewLine;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<yzy201>{0}</yzy201>", "") + Environment.NewLine;               // 排序
                    request += string.Format("<aac004>{0}</aac004>", "") + Environment.NewLine;               // 婴儿性别编号
                    request += string.Format("<yzy230>{0}</yzy230>", "") + Environment.NewLine;               // 婴儿性别
                    request += string.Format("<yzy231>{0}</yzy231>", "") + Environment.NewLine;               // 婴儿体重
                    request += string.Format("<yzy232>{0}</yzy232>", "") + Environment.NewLine;               // 分娩结果编号
                    request += string.Format("<yzy233>{0}</yzy233>", "") + Environment.NewLine;               // 分娩结果
                    request += string.Format("<yzy234>{0}</yzy234>", "") + Environment.NewLine;               // 转归编号
                    request += string.Format("<yzy235>{0}</yzy235>", "") + Environment.NewLine;               // 转归
                    request += string.Format("<yzy236>{0}</yzy236>", "") + Environment.NewLine;               // 婴儿抢救成功次数 无时填0
                    request += string.Format("<yzy237>{0}</yzy237>", "") + Environment.NewLine;               // 呼吸编号
                    request += string.Format("<yzy238>{0}</yzy238>", "") + Environment.NewLine;               // 呼吸
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</detail>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.3.5 肿瘤专科病人治疗记录信息（病案首页）录入
        /// <summary>
        /// 提交病案首页中住院病人的肿瘤专科病人治疗记录相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        static string Biz200105(string request, DataTable dt)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh200105") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致 
            request += string.Format("<yzy239>{0}</yzy239>", "") + Environment.NewLine;               // 放疗方式编号
            request += string.Format("<yzy240>{0}</yzy240>", "") + Environment.NewLine;               // 放疗方式 
            request += string.Format("<yzy241>{0}</yzy241>", "") + Environment.NewLine;               // 放疗程序编号 
            request += string.Format("<yzy242>{0}</yzy242>", "") + Environment.NewLine;               // 放疗程序 
            request += string.Format("<yzy243>{0}</yzy243>", "") + Environment.NewLine;               // 放疗装置编号 
            request += string.Format("<yzy244>{0}</yzy244>", "") + Environment.NewLine;               // 放疗装置 
            request += string.Format("<yzy245>{0}</yzy245>", "") + Environment.NewLine;               // 1.原发灶剂量 
            request += string.Format("<yzy246>{0}</yzy246>", "") + Environment.NewLine;               // 原发灶次数 
            request += string.Format("<yzy247>{0}</yzy247>", "") + Environment.NewLine;               // 原发灶天数 
            request += string.Format("<yzy248>{0}</yzy248>", "") + Environment.NewLine;               // 原发灶开始日期  yyyymmdd
            request += string.Format("<yzy249>{0}</yzy249>", "") + Environment.NewLine;               // 原发灶结束时间  yyyymmdd
            request += string.Format("<yzy250>{0}</yzy250>", "") + Environment.NewLine;               // 区域淋巴结剂量 
            request += string.Format("<yzy251>{0}</yzy251>", "") + Environment.NewLine;               // 区域淋巴结次数 
            request += string.Format("<yzy252>{0}</yzy252>", "") + Environment.NewLine;               // 区域淋巴结天数 
            request += string.Format("<yzy253>{0}</yzy253>", "") + Environment.NewLine;               // 区域淋巴结开始时间 yyyymmdd
            request += string.Format("<yzy254>{0}</yzy254>", "") + Environment.NewLine;               // 区域淋巴结结束时间 yyyymmdd
            request += string.Format("<yzy255>{0}</yzy255>", "") + Environment.NewLine;               // 3.转移灶名称 
            request += string.Format("<yzy256>{0}</yzy256>", "") + Environment.NewLine;               // 3.转移灶剂量 
            request += string.Format("<yzy257>{0}</yzy257>", "") + Environment.NewLine;               // 转移灶次数 
            request += string.Format("<yzy258>{0}</yzy258>", "") + Environment.NewLine;               // 转移灶天数 
            request += string.Format("<yzy259>{0}</yzy259>", "") + Environment.NewLine;               // 转移灶开始时间 yyyymmdd
            request += string.Format("<yzy260>{0}</yzy260>", "") + Environment.NewLine;               // 转移灶结束时间 yyyymmdd
            request += string.Format("<yzy261>{0}</yzy261>", "") + Environment.NewLine;               // 化疗方式编号 
            request += string.Format("<yzy262>{0}</yzy262>", "") + Environment.NewLine;               // 化疗方式 
            request += string.Format("<yzy263>{0}</yzy263>", "") + Environment.NewLine;               // 化疗方法编号 
            request += string.Format("<yzy264>{0}</yzy264>", "") + Environment.NewLine;               // 化疗方法 
            request += "<detail>" + Environment.NewLine;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    request += "<row>" + Environment.NewLine;
                    request += string.Format("<yzy201>{0}</yzy201>", "") + Environment.NewLine;               // 排序
                    request += string.Format("<yzy265>{0}</yzy265>", "") + Environment.NewLine;               // 化疗日期 yyyymmdd
                    request += string.Format("<yzy266>{0}</yzy266>", "") + Environment.NewLine;               // 化疗药物名称及剂量
                    request += string.Format("<yzy267>{0}</yzy267>", "") + Environment.NewLine;               // 化疗疗程
                    request += string.Format("<yzy268>{0}</yzy268>", "") + Environment.NewLine;               // 疗效编号
                    request += string.Format("<yzy269>{0}</yzy269>", "") + Environment.NewLine;               // 疗效
                    request += "</row>" + Environment.NewLine;
                }
            }
            else
            {
                request += "<row>" + Environment.NewLine;
                request += "</row>" + Environment.NewLine;
            }
            request += "</detail>" + Environment.NewLine;
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #region 4.3.6 出院小结（出院记录）录入
        /// <summary>
        /// 提交住院病人出院小结（出院记录）相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        static string Biz200106(string request, DataTable dt)
        {
            request = string.Empty;
            request += "<program>" + Environment.NewLine;
            request += string.Format("<session_id>{0}</session_id>", GetSessionId()) + Environment.NewLine;
            request += string.Format("<function_id>{0}</function_id>", "bizh200106") + Environment.NewLine;     // 交易号
            request += string.Format("<akb020>{0}</akb020>", orgId) + Environment.NewLine;             // 医疗机构编码
            request += string.Format("<aaz218>{0}</aaz218>", " ") + Environment.NewLine;                    // 就医登记号
            request += string.Format("<aab299>{0}</aab299>", " ") + Environment.NewLine;                    // 行政区划代码（就医地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab600>{0}</yab600>", "") + Environment.NewLine;                     // 就医地社保分支机构代码 区县编码（ 例如： 440601 市辖区
            request += string.Format("<akb026>{0}</akb026>", " ") + Environment.NewLine;                    // 医疗机构执业许可证登记号 按照省统一编码
            request += string.Format("<akb021>{0}</akb021>", " ") + Environment.NewLine;                    // 医疗服务机构名称
            request += string.Format("<aab301>{0}</aab301>", " ") + Environment.NewLine;                    // 行政区划代码（参保地） 地市编码（ 例如： 440600 佛山市）
            request += string.Format("<yab060>{0}</yab060>", " ") + Environment.NewLine;                    // 参保地社保分支机构代码 区县编码（ 例如： 440601 市辖区 440604 禅城区 440605 ）
            request += string.Format("<aac002>{0}</aac002>", " ") + Environment.NewLine;                    // 社会保障号码
            request += string.Format("<aac043>{0}</aac043>", " ") + Environment.NewLine;                    // 证件类型 参见省内异地接口文档代码定义AAC043
            request += string.Format("<aac044>{0}</aac044>", " ") + Environment.NewLine;                    // 社会保障号码 以社会保障号为主，当证件类型为“社会保障卡”时，aac044要与aac002一致
            request += string.Format("<yzy301>{0}</yzy301>", "") + Environment.NewLine;                     // 入院诊断描述
            request += string.Format("<yzy302>{0}</yzy302>", "") + Environment.NewLine;                     // 出院诊断描述
            request += string.Format("<yzy303>{0}</yzy303>", "") + Environment.NewLine;                     // 入院时主要症状，体征
            request += string.Format("<yzy304>{0}</yzy304>", "") + Environment.NewLine;                     // 入院后处理及经过（包含实验室和其他特殊检查）
            request += string.Format("<yzy305>{0}</yzy305>", "") + Environment.NewLine;                     // 出院时情况
            request += string.Format("<yzy306>{0}</yzy306>", "") + Environment.NewLine;                     // 出院时医嘱
            request += string.Format("<akc273>{0}</akc273>", "") + Environment.NewLine;                     // 医师
            request += "</program>" + Environment.NewLine;
            return xmlTitle + request;
        }
        #endregion

        #endregion

    }

    #region 返回实体
    /// <summary>
    /// 返回实体
    /// </summary>
    public class EntitySbRes
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public int resultCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回XML详细节点
        /// </summary>
        public string response { get; set; }
    }
    #endregion

    #region 登录信息(省工伤)
    /// <summary>
    /// 登录信息(省工伤)
    /// </summary>
    public class EntitySGS_Login
    {
        /// <summary>
        /// 收费员工号
        /// </summary>
        public string empNo { get; set; }
        /// <summary>
        /// 收费员姓名
        /// </summary>
        public string empName { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string recipeNo { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string invoNo { get; set; }
        /// <summary>
        /// 病人ID
        /// </summary>
        public string patientId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// 费别ID
        /// </summary>
        public string payTypeId { get; set; }
        /// <summary>
        /// 医师ID
        /// </summary>
        public string doctId { get; set; }
        /// <summary>
        /// 医师工号
        /// </summary>
        public string doctNo { get; set; }
        /// <summary>
        /// 医师姓名
        /// </summary>
        public string doctName { get; set; }

    }
    #endregion

    #region 门诊项目(省工伤)
    /// <summary>
    /// 门诊项目(省工伤)
    /// </summary>
    public class EntitySGS_RecipeItem
    {
        /// <summary>
        /// 费用序号  如果是退费操作，需使用4.1.3接口来获取该费用序号进行传入，以确定具体要进行退费操作的药品项目；正常收费时该值传空；
        /// </summary>
        public string sortNo { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string recipeNo { get; set; }
        /// <summary>
        /// 医院药品项目编码
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// 医院药品项目名称
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string dosageForm { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string vender { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string spec { get; set; }
        /// <summary>
        /// 费用发生日期 格式：“yyyyMMdd”
        /// </summary>
        public string feeDate { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 单价 精确到小数点后4位
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 用量 负数时为退费 精确到小数点后2位
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 金额 精确到小数点后2位，负数时为退费
        /// </summary>
        public decimal total { get; set; }
        /// <summary>
        /// 处方医生编号
        /// </summary>
        public string doctNo { get; set; }
        /// <summary>
        /// 处方医生姓名
        /// </summary>
        public string doctName { get; set; }
        /// <summary>
        /// 医院费用的唯一标识
        /// </summary>
        public string feeId { get; set; }
        /// <summary>
        /// 限制使用标志 0 否; 1 是   或者是aka036?
        /// </summary>
        public int limitFlag { get; set; }
    }
    #endregion

    #region 门诊社保返回(省工伤)
    /// <summary>
    /// 门诊社保返回(省工伤)
    /// </summary>
    public class EntitySGS_PayInfo
    {
        /// <summary>
        /// akb020 String  20	医院编号
        /// </summary>
        public string YYBH { get; set; }
        /// <summary>
        /// aaz218  String	20	就医登记号
        /// </summary>
        public string JYDJH { get; set; }
        /// <summary>
        /// akc264  String	12	医疗总费用 akc264 = bka831 + bka832
        /// </summary>
        public string YLZFY { get; set; }
        /// <summary>
        /// bka831  String	12	个人自付 bka831 = akb067 + akb066 + bka839
        /// </summary>
        public string GRZF { get; set; }
        /// <summary>
        /// bka832  String	12	工伤保险支付 bka832 = ake039 + ake035 + ake026 + ake029 + bka841 + bka842 + bka840 + Bka821
        /// </summary>
        public string GSSBZF { get; set; }
        /// <summary>
        /// bka825  String	12	全自费费用  
        /// </summary>
        public string QZFFY { get; set; }
        /// <summary>
        /// bka826 String  12	部分自费费用 
        /// </summary>
        public string BFZFFY { get; set; }
        /// <summary>
        /// aka151 String  12	起付线费用 
        /// </summary>
        public string QFXFY { get; set; }
        /// <summary>
        /// bka838 String  12	超共付段费用个人自付 
        /// </summary>
        public string CGFDFYGRZF { get; set; }
        /// <summary>
        /// akb067 String  12	个人现金支付 
        /// </summary>
        public string GRXJZF { get; set; }
        /// <summary>
        /// akb066 String  12	个人账户支付 
        /// </summary>
        public string GRZHZF { get; set; }
        /// <summary>
        /// bka821 String  12	民政救助金支付 
        /// </summary>
        public string MZJZJZF { get; set; }
        /// <summary>
        /// bka839 String  12	其他支付  
        /// </summary>
        public string QTZF { get; set; }
        /// <summary>
        /// ake039 String  12	工伤保险统筹基金支付 
        /// </summary>
        public string GSSBTCJJZF { get; set; }
        /// <summary>
        /// ake035 String  12	公务员医疗补助基金支付
        /// </summary>
        public string GWYYLJZJJZF { get; set; }
        /// <summary>
        /// ake026 String  12	企业补充工伤保险基金支付
        /// </summary>
        public string QYBCGSBXJJZF { get; set; }
        /// <summary>
        /// ake029 String  12	大额医疗费用补助基金支付
        /// </summary>
        public string DEYLFYBZJJZF { get; set; }
        /// <summary>
        /// bka841 String  12	单位支付
        /// </summary>
        public string DWZF { get; set; }
        /// <summary>
        /// bka842 String  12	医院垫付
        /// </summary>
        public string YYDF { get; set; }
        /// <summary>
        /// bka840 String  12	其他基金支付
        /// </summary>
        public string QTJJZF { get; set; }

    }
    #endregion

    #region EntityFK
    /// <summary>
    /// EntityFK
    /// </summary>
    public class EntityFK
    {
        public string FId { get; set; }

        public string FName { get; set; }

        public string FValue { get; set; }
    }
    #endregion

    #region EntityDict
    /// <summary>
    /// EntityDict
    /// </summary>
    public class EntityDict : weCare.Core.Entity.BaseDataContract
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PyCode { get; set; }

        public static EnumCols Columns = new EnumCols();

        public class EnumCols
        {
            public string Id = "Id";
            public string Name = "Name";
            public string PyCode = "PyCode";
        }
    }
    #endregion

    public class Dictionary
    {
        /// <summary>
        /// 血型
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetXX()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "1", Name = "A 型", PyCode = "" });
            data.Add(new EntityDict() { Id = "2", Name = "B 型", PyCode = "" });
            data.Add(new EntityDict() { Id = "3", Name = "AB 型", PyCode = "" });
            data.Add(new EntityDict() { Id = "4", Name = "O 型", PyCode = "" });
            data.Add(new EntityDict() { Id = "5", Name = "Rh 阳性", PyCode = "" });
            data.Add(new EntityDict() { Id = "6", Name = "Rh 阴性", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 入院方式
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetRYFS()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "1", Name = "门诊入院", PyCode = "" });
            data.Add(new EntityDict() { Id = "2", Name = "急诊入院", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 入院情况
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetRYQK()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "1", Name = "危", PyCode = "" });
            data.Add(new EntityDict() { Id = "2", Name = "急", PyCode = "" });
            data.Add(new EntityDict() { Id = "3", Name = "一般", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 出院转归
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetCYZG()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "01", Name = "治愈", PyCode = "" });
            data.Add(new EntityDict() { Id = "02", Name = "好转", PyCode = "" });
            data.Add(new EntityDict() { Id = "03", Name = "无效", PyCode = "" });
            data.Add(new EntityDict() { Id = "04", Name = "未愈", PyCode = "" });
            data.Add(new EntityDict() { Id = "05", Name = "死亡", PyCode = "" });
            data.Add(new EntityDict() { Id = "99", Name = "其它", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 人员类别
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetRYLB()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "1", Name = "在职", PyCode = "" });
            data.Add(new EntityDict() { Id = "2", Name = "退休", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 住院原因
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetZYYY()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "0", Name = "普通原因住院", PyCode = "" });
            data.Add(new EntityDict() { Id = "4", Name = "外伤住院", PyCode = "" });
            return data;
        }
        /// <summary>
        /// 补助类型
        /// </summary>
        /// <returns></returns>
        public List<EntityDict> GetBZLX()
        {
            List<EntityDict> data = new List<EntityDict>();
            data.Add(new EntityDict() { Id = "1", Name = "普通", PyCode = "" });
            data.Add(new EntityDict() { Id = "2", Name = "抢救期间", PyCode = "" });
            data.Add(new EntityDict() { Id = "3", Name = "非抢救期间", PyCode = "" });
            return data;
        }
    }
}
