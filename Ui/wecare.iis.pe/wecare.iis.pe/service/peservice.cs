using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Com.svc
{
    /// <summary>
    /// 广东省.健康证服务.服务契约
    /// </summary>
    public class PeService : PeInterface
    {
        #region 服务调用
        /// <summary>
        /// 服务调用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string CallFunc(string request)
        {
            Dictionary<string, string> dicKey = Function.ReadXmlNodes(GetRequestXml(request), "Request");
            string funcCode = dicKey["funcCode"];
            string response = string.Empty;
            #region 功能列表
            //公共类	900100	数字签名生成功能接口
            //业务登记类	C00300	体检登记接口
            //    C00400	体检项目结果接口
            //    C00500	健康证打印接口
            //    C00600	短信通知上传接口
            //    C00700	人脸比对数据接口
            //业务回退类	C00800	体检登记回退接口
            //    C00900	体检结果体检明细回退接口
            //    C01100	人脸比对数据回退接口
            //    C01300	健康证打印回退接口
            //    C01400	短信通知上传回退接口
            //业务查询类	C00100	查询体检机构信息接口
            //    C01500	查询体检登记接口
            //    C01600	查询体检项目接口
            //    C01700	查询体检项目明细接口
            //    C01800	查询人脸比对数据接口
            //    C02000	查询健康证打印接口
            //    C02100	查询短信通知接口
            switch (funcCode)
            {
                case "900100":      // 数字签名生成功能接口
                    response = this.Func900100(funcCode, dicKey);
                    break;
                case "C00300":      // 体检登记接口
                    response = this.Func00300(funcCode, dicKey);
                    break;
                case "C00400":      // 体检项目结果接口
                    response = this.Func00400(funcCode, dicKey);
                    break;
                case "C00500":      // 健康证打印接口
                    response = this.Func00500(funcCode, dicKey);
                    break;
                case "C00600":      // 短信通知上传接口
                    response = this.Func00600(funcCode, dicKey);
                    break;
                case "C00700":      // 人脸比对数据接口
                    response = this.Func00700(funcCode, dicKey);
                    break;
                case "C00800":      // 体检登记回退接口
                    response = this.Func00800(funcCode, dicKey);
                    break;
                case "C00900":      // 体检结果体检明细回退接口
                    response = this.Func00900(funcCode, dicKey);
                    break;
                case "C01100":      // 人脸比对数据回退接口
                    response = this.Func01100(funcCode, dicKey);
                    break;
                case "C01300":      // 健康证打印回退接口
                    response = this.Func01300(funcCode, dicKey);
                    break;
                case "C01400":      // 短信通知上传回退接口
                    response = this.Func01400(funcCode, dicKey);
                    break;
                case "C00100":      // 查询体检机构信息接口
                    response = this.Func00100(funcCode, dicKey);
                    break;
                case "C01500":      // 查询体检登记接口
                    response = this.Func01500(funcCode, dicKey);
                    break;
                case "C01600":      // 查询体检项目接口
                    response = this.Func01600(funcCode, dicKey);
                    break;
                case "C01700":      // 查询体检项目明细接口
                    response = this.Func01700(funcCode, dicKey);
                    break;
                case "C01800":      // 查询人脸比对数据接口
                    response = this.Func01800(funcCode, dicKey);
                    break;
                case "C02000":      // 查询健康证打印接口
                    response = this.Func02000(funcCode, dicKey);
                    break;
                case "C02100":      // 查询短信通知接口
                    response = this.Func02100(funcCode, dicKey);
                    break;
                default:
                    break;
            }
            #endregion
            return response;
        }
        #endregion

        #region 

        public int ExecSql(string file, string sql)
        {
            int affect = -1;
            ODBCHelper svc = null;
            try
            {
                svc = new ODBCHelper(file);
                affect = svc.ExecSql(sql);
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
            

            return affect;
        }


        public DataTable GetDataTable(string file, string sql)
        {
            DataTable dt = null;
            ODBCHelper svc = null;
            try
            {
                svc = new ODBCHelper(file);
                dt = svc.GetDataTable(sql);

               
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
            
            return dt;
        }
        #endregion

        #region 方法

        #region 6.1 公共接口部分

        #region 6.1.1   数字签名生成功能接口
        /// <summary>
        /// 6.1.1   数字签名生成功能接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func900100(string funcCode, Dictionary<string, string> dicKey)
        {
            string signature = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<METHOD>{0}</METHOD>", funcCode));
            sb.AppendLine(string.Format("<ORGCODE>{0}</ORGCODE>", Function.ReadConfigXml("orgCode")));
            sb.AppendLine(string.Format("<USER>{0}</USER>", Function.ReadConfigXml("user")));
            sb.AppendLine(string.Format("<PASSWORD>{0}</PASSWORD>", weCare.Core.Utils.ESCryptography.EncryptMD5(Function.ReadConfigXml("password")).ToLower()));
            sb.AppendLine(string.Format("<UUID>{0}</UUID>", Function.ReadConfigXml("appCode")));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    // 数字签名，该签名用于业务接口调用的认证码生成，数字签名具有时效性
                    signature = ds.Tables["BODY"].Rows[0]["SIGNATURE"].ToString();
                }
            }
            return signature;
        }
        #endregion

        #endregion

        #region 6.2 业务登记接口部分

        #region 6.2.1   体检登记接口
        /// <summary>
        /// 6.2.1   体检登记接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00300(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from view_jkz_00300 where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            DataRow dr = dt.Rows[0];

            StringBuilder sb = new StringBuilder();
            // 体检车辆 车牌号码
            sb.AppendLine(string.Format("<VEHICLE>{0}</VEHICLE>", dr["VEHICLE"].ToString()));
            // 预约单编号
            sb.AppendLine(string.Format("<ORDER>{0}</ORDER>", dr["ORDERID"].ToString()));
            // 订购类型：1-个人，2-团体，3-临时加号
            sb.AppendLine(string.Format("<ORDERTYPE>{0}</ORDERTYPE>", dr["ORDERTYPE"].ToString()));
            // 姓名
            sb.AppendLine(string.Format("<NAME>{0}</NAME>", dr["NAME"].ToString()));
            // 身份证号码
            sb.AppendLine(string.Format("<IDCARD>{0}</IDCARD>", dr["IDCARD"].ToString()));
            // 性别 ：1-男，2-女  
            sb.AppendLine(string.Format("<GENDER>{0}</GENDER>", dr["GENDER"].ToString()));
            // 年龄
            sb.AppendLine(string.Format("<AGE>{0}</AGE>", dr["AGE"].ToString()));
            // 民族
            sb.AppendLine(string.Format("<ETHNIC>{0}</ETHNIC>", dr["ETHNIC"].ToString()));
            // 身份证头像 BASE64 
            sb.AppendLine(string.Format("<HEAD>{0}</HEAD>", (dr["HEAD"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["HEAD"])))));
            //string head = ReadTxtContent("C:\\Users\\Administrator\\Desktop\\0305.pe.iis\\HEAD.txt");
            // 身份证无法识别脸情况，身份证拍照  BASE64 
            //sb.AppendLine(string.Format("<CAPHEAD>{0}</CAPHEAD>", (dr["CAPHEAD"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["CAPHEAD"])))));
            sb.AppendLine(string.Format("<CAPHEAD>{0}</CAPHEAD>", ""));
            //  登记面部BASE 64
            sb.AppendLine(string.Format("<FACE>{0}</FACE>", (dr["FACE"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["FACE"])))));
            //sb.AppendLine(string.Format("<FACE>{0}</FACE>", head));
            // 联系电话
            sb.AppendLine(string.Format("<MOBILE>{0}</MOBILE>", dr["MOBILE"].ToString()));
            // 微信号
            sb.AppendLine(string.Format("<OPENID>{0}</OPENID>", dr["OPENID"].ToString()));
            // 行业类别大类
            sb.AppendLine(string.Format("<TYPE>{0}</TYPE>", dr["TYPE"].ToString()));
            // 行业类别小类
            sb.AppendLine(string.Format("<INDUSTRY>{0}</INDUSTRY>", dr["INDUSTRY"].ToString()));
            // 文化程度
            sb.AppendLine(string.Format("<DEGREE>{0}</DEGREE>", dr["DEGREE"].ToString()));
            // 预约时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ORDERTIME>{0}</ORDERTIME>", dr["ORDERTIME"].ToString()));
            // 登记时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ENROLLTIME>{0}</ENROLLTIME>", dr["ENROLLTIME"].ToString()));
            // 登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dr["ENROLLNO"].ToString()));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                    // 体检登记唯一码（接口系统保存的本次体检登记的唯一标识码，业务回退时用这个编码进行回退）
                    string certId = ds.Tables["BODY"].Rows[0]["CERTID"].ToString();
                    Sql = @"select 1 from table_jkz where regNo = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dicKey["regNo"];
                    dt = svc.GetDataTable(Sql, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Sql = @"update table_jkz set certId = ? where regNo = ?";
                        parm = svc.CreateParm(2);
                        parm[0].Value = certId;
                        parm[1].Value = regNo;
                    }
                    else
                    {
                        Sql = @"insert into table_jkz (regNo, certId) values (?, ?)";
                        parm = svc.CreateParm(2);
                        parm[0].Value = regNo;
                        parm[1].Value = certId;
                    }
                    svc.ExecSql(Sql, parm);
                }
            }
            return response;
        }
        #endregion

        #region 6.2.2   体检项目结果接口
        /// <summary>
        /// 6.2.2   体检项目结果接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00400(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;

            string certId = dt.Rows[0]["certId"].ToString();

            Sql = @"select * from view_jkz_00400_1 where regNo = ?";
            parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            dt = svc.GetDataTable(Sql, parm);
            DataRow dr = dt.Rows[0];
            StringBuilder sb1 = new StringBuilder();
            // 体检登记ID （对应体检登记接口返回的CERTID）
            sb1.AppendLine(string.Format("<CERTID>{0}</CERTID>", certId));
            // 体检类别：0-初检，1-复检
            sb1.AppendLine(string.Format("<TYPE>{0}</TYPE>", dr["TYPE"].ToString()));
            // 体检项目大类：01-血液，02-粪便，03-胸透，04-体征 
            sb1.AppendLine(string.Format("<ITEM>{0}</ITEM>", dr["ITEM"].ToString()));
            // 体检时间，格式yyyy-MM-dd HH:mm:ss 
            sb1.AppendLine(string.Format("<CHECK_TIME>{0}</CHECK_TIME>", dr["CHECK_TIME"].ToString()));
            // 体检操作人
            sb1.AppendLine(string.Format("<CHECK_NAME>{0}</CHECK_NAME>", dr["CHECK_NAME"].ToString()));
            // 结果：Y-合格、N-不合格
            sb1.AppendLine(string.Format("<RESULT>{0}</RESULT>", dr["RESULT"].ToString()));
            // 结果时间，格式yyyy-MM-dd HH:mm:ss
            sb1.AppendLine(string.Format("<RESULT_TIME>{0}</RESULT_TIME>", dr["RESULT_TIME"].ToString()));
            // 结果录入人
            sb1.AppendLine(string.Format("<RESULT_NAME>{0}</RESULT_NAME>", dr["RESULT_NAME"].ToString()));
            // 结果有效性，Y-有效，N-无效，当复检结果录入时，把初检的结果设置为无效。
            sb1.AppendLine(string.Format("<VALID>{0}</VALID>", dr["VALID"].ToString()));
            // 结果有效性，Y-有效，N-无效，当复检结果录入时，把初检的结果设置为无效。
            sb1.AppendLine(string.Format("<CHECKUP_TIME>{0}</CHECKUP_TIME>", dr["CHECKUP_TIME"].ToString()));
            // 结论时间，格式yyyy-MM-dd HH:mm:ss
            sb1.AppendLine(string.Format("<RESULT_TIME_CERT>{0}</RESULT_TIME_CERT>", dr["RESULT_TIME_CERT"].ToString()));
            // 检查结论：Y-合格，N-未合格
            sb1.AppendLine(string.Format("<RESULT_CERT>{0}</RESULT_CERT>", dr["RESULT_CERT"].ToString()));
            // 检查结论说明
            sb1.AppendLine(string.Format("<RESULT_NOTE>{0}</RESULT_NOTE>", dr["RESULT_NOTE"].ToString()));
            // 结论医师
            sb1.AppendLine(string.Format("<DOCTOR>{0}</DOCTOR>", dr["DOCTOR"].ToString()));
            // 体检登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb1.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dr["ENROLLNO"].ToString()));

            Sql = @"select * from view_jkz_00400_2 where regNo = ?";
            parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            dt = svc.GetDataTable(Sql, parm);
            StringBuilder sb2 = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                sb2.AppendLine("<RECORD>");
                sb2.AppendLine(string.Format("<ITEM>{0}</ITEM>", dr["ITEM"].ToString()));
                sb2.AppendLine(string.Format("<RESULT>{0}</RESULT>", dr["RESULT"].ToString()));
                sb2.AppendLine(string.Format("<DATA1>{0}</DATA1>", dr["DATA1"].ToString()));
                sb2.AppendLine(string.Format("<DATA2>{0}</DATA2>", dr["DATA2"].ToString()));
                sb2.AppendLine(string.Format("<DATA3>{0}</DATA3>", dr["DATA3"].ToString()));
                sb2.AppendLine(string.Format("<DATA4>{0}</DATA4>", dr["DATA4"].ToString()));
                sb2.AppendLine(string.Format("<MEMO>{0}</MEMO>", dr["MEMO"].ToString()));
                sb2.AppendLine("</RECORD>");
            }
            string response = this.GetResponseXml(funcCode, sb1.ToString() + Environment.NewLine + "<LIST>" + Environment.NewLine + sb2.ToString() + "</LIST>");
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                    // 体检结果ID（接口系统保存的体检结果唯一编号。务必保存，验血人脸比对、胸透人脸比对、以及体检结果明细回退等接口有作为入参）
                    string checkId = ds.Tables["BODY"].Rows[0]["CHECKID"].ToString();
                    Sql = @"select 1 from table_jkz where regNo = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dicKey["regNo"];
                    dt = svc.GetDataTable(Sql, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Sql = @"update table_jkz set checkId = ? where regNo = ?";
                        parm = svc.CreateParm(2);
                        parm[0].Value = checkId;
                        parm[1].Value = regNo;
                    }
                    else
                    {
                        Sql = @"insert into table_jkz (regNo, checkId) values (?, ?)";
                        parm = svc.CreateParm(2);
                        parm[0].Value = regNo;
                        parm[1].Value = checkId;
                    }
                    svc.ExecSql(Sql, parm);
                }
            }
            return response;
        }
        #endregion

        #region 6.2.3   健康证打印接口
        /// <summary>
        /// 6.2.3   健康证打印接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00500(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;

            string certId = dt.Rows[0]["certId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID （对应体检登记接口返回CERTID）
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", certId));
            // 登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dicKey["ENROLLNO"]));
            // 打印操作人
            sb.AppendLine(string.Format("<USER>{0}</USER>", dicKey["USER"]));
            // 打印时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<TIME>{0}</TIME>", dicKey["TIME"]));
            // 健康证姓名
            sb.AppendLine(string.Format("<NAME>{0}</NAME>", dicKey["NAME"]));
            // 所属单位
            sb.AppendLine(string.Format("<TEAM>{0}</TEAM>", dicKey["TEAM"]));
            // 健康证联系方式
            sb.AppendLine(string.Format("<MOBILE>{0}</MOBILE>", dicKey["MOBILE"]));
            // 行业类别小类   （参考字典7.2） 
            sb.AppendLine(string.Format("<INDUSTRY>{0}</INDUSTRY>", dicKey["INDUSTRY"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                    // 打印记录ID
                    string printId = ds.Tables["BODY"].Rows[0]["PRINTID"].ToString();
                    Sql = @"select 1 from table_jkz where regNo = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dicKey["regNo"];
                    dt = svc.GetDataTable(Sql, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Sql = @"update table_jkz set printId = ? where regNo = ?";
                        parm = svc.CreateParm(2);
                        parm[0].Value = printId;
                        parm[1].Value = regNo;
                    }
                    else
                    {
                        Sql = @"insert into table_jkz (regNo, printId) values (?, ?)";
                        parm = svc.CreateParm(2);
                        parm[0].Value = regNo;
                        parm[1].Value = printId;
                    }
                    svc.ExecSql(Sql, parm);
                }
            }
            return response;
        }
        #endregion

        #region 6.2.4   短信通知上传接口
        /// <summary>
        /// 6.2.4   短信通知上传接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00600(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;

            string certId = dt.Rows[0]["certId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID （对应体检登记接口返回CERTID）
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", certId));
            // 登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dicKey["ENROLLNO"]));
            // 手机号码
            sb.AppendLine(string.Format("<MOBILE>{0}</MOBILE>", dicKey["MOBILE"]));
            // 发送时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<TIME>{0}</TIME>", dicKey["TIME"]));
            // 发送内容
            sb.AppendLine(string.Format("<CONTENT>{0}</CONTENT>", dicKey["CONTENT"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                    // 短信唯一码
                    string smsId = ds.Tables["BODY"].Rows[0]["SMSID"].ToString();
                    Sql = @"select 1 from table_jkz where regNo = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dicKey["regNo"];
                    dt = svc.GetDataTable(Sql, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Sql = @"update table_jkz set smsId = ? where regNo = ?";
                        parm = svc.CreateParm(2);
                        parm[0].Value = smsId;
                        parm[1].Value = regNo;
                    }
                    else
                    {
                        Sql = @"insert into table_jkz (regNo, smsId) values (?, ?)";
                        parm = svc.CreateParm(2);
                        parm[0].Value = regNo;
                        parm[1].Value = smsId;
                    }
                    svc.ExecSql(Sql, parm);
                }
            }
            return response;
        }
        #endregion

        #region 6.2.5   人脸比对数据接口
        /// <summary>
        /// 6.2.5   人脸比对数据接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00700(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;

            string certId = dt.Rows[0]["certId"].ToString();

            Sql = @"select * from view_jkz_00700 where regNo = ?";
            parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            DataRow dr = dt.Rows[0];

            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID （对应体检登记接口返回CERTID）
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", certId));
            // 体检登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dr["ENROLLNO"].ToString()));
            // 姓名
            sb.AppendLine(string.Format("<NAME>{0}</NAME>", dr["NAME"].ToString()));
            // 身份证号码
            sb.AppendLine(string.Format("<IDCARD>{0}</IDCARD>", dr["IDCARD"].ToString()));
            // 身份证头像 BASE64
            sb.AppendLine(string.Format("<IDHEAD>{0}</IDHEAD>", (dr["IDHEAD"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["IDHEAD"])))));
            // 登记头像   BASE 64 
            sb.AppendLine(string.Format("<ENROLLHEAD>{0}</ENROLLHEAD>", (dr["ENROLLHEAD"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["ENROLLHEAD"])))));
            // 采血登记面部BASE 64 
            sb.AppendLine(string.Format("<BLOODFACE>{0}</BLOODFACE>", (dr["BLOODFACE"] == DBNull.Value ? string.Empty : Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dr["BLOODFACE"])))));
            // 比对结果
            sb.AppendLine(string.Format("<RESULT>{0}</RESULT>", dr["RESULT"].ToString()));
            // 比对相似度
            sb.AppendLine(string.Format("<SIMILARITY>{0}</SIMILARITY>", dr["SIMILARITY"].ToString()));
            // 比对时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<CECKTIME>{0}</CECKTIME>", dr["CECKTIME"].ToString()));
            // 类型：01-验血、03-胸透
            sb.AppendLine(string.Format("<TYPE>{0}</TYPE>", dr["TYPE"].ToString()));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                    // 验血人脸比对数据唯一码（务必保存，回退业务入参）
                    string resultId = ds.Tables["BODY"].Rows[0]["RESULTID"].ToString();
                    Sql = @"select 1 from table_jkz where regNo = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dicKey["regNo"];
                    dt = svc.GetDataTable(Sql, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Sql = @"update table_jkz set resultId = ? where regNo = ?";
                        parm = svc.CreateParm(2);
                        parm[0].Value = resultId;
                        parm[1].Value = regNo;
                    }
                    else
                    {
                        Sql = @"insert into table_jkz (regNo, resultId) values (?, ?)";
                        parm = svc.CreateParm(2);
                        parm[0].Value = regNo;
                        parm[1].Value = resultId;
                    }
                    svc.ExecSql(Sql, parm);
                }
            }
            return response;
        }
        #endregion

        #endregion

        #region 6.3 查询接口部分

        #region 6.3.1   体检机构信息接口
        /// <summary>
        /// 6.3.1   体检机构信息接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00100(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 机构简称
            sb.AppendLine(string.Format("<SNAME>{0}</SNAME>", dicKey["SNAME"]));
            // 机构名称
            sb.AppendLine(string.Format("<NAME>{0}</NAME>", dicKey["NAME"]));
            // 地址
            sb.AppendLine(string.Format("<ADDRESS>{0}</ADDRESS>", dicKey["ADDRESS"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["BODY"].Rows[0];
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_00100 where ORGID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = dr["ORGID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_00100(ORGID, CODE, NAME, SNAME, PIC, ORG_CODE, REGION, ADDRESS, HEAD, MOBILE,
                                                        LAT, LNG, SUPERVISE, MTIME, RECOMMEND, WORK_SCHEDUL, SCORE, EVAL_SCORE, EVAL_TIMES )
                                                values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                                        ?, ?, ?, ?, ?, ?, ?, ?, ?) ";
                    int n = -1;
                    parm = svc.CreateParm(19);
                    parm[++n].Value = dr["ORGID"].ToString();                           // ORGID	    varchar(40)	业务机构唯一码                    
                    parm[++n].Value = dr["CODE"].ToString();                            // CODE        	varchar(20)	机构编码                     
                    parm[++n].Value = dr["NAME"].ToString();                            // NAME        	varchar(128)	机构名称                       
                    parm[++n].Value = dr["SNAME"].ToString();                           // SNAME       	varchar(64)	机构简称                    
                    parm[++n].Value = dr["PIC"].ToString();                             // PIC         	varchar(40)	图标                     
                    parm[++n].Value = dr["ORG_CODE"].ToString();                        // ORG_CODE    	varchar(20)	医疗机构代码                    
                    parm[++n].Value = dr["REGION"].ToString();                          // REGION      	varchar(12)	所在地区                     
                    parm[++n].Value = dr["ADDRESS"].ToString();                         // ADDRESS     	varchar(256)	地址                    
                    parm[++n].Value = dr["HEAD"].ToString();                            // HEAD        	varchar(64)	负责人                      
                    parm[++n].Value = dr["MOBILE"].ToString();                          // MOBILE      	varchar(20)	联系电话                     
                    parm[++n].Value = Function.Dec(dr["LAT"].ToString());               // LAT         	decimal(12,6)	纬度                      
                    parm[++n].Value = Function.Dec(dr["LNG"].ToString());               // LNG         	decimal(12,6)	经度                    
                    parm[++n].Value = Function.Dec(dr["SUPERVISE"].ToString());         // SUPERVISE   	bit(1)	监管状态：0-非监管，1-监管    
                    if (dr["MTIME"] == DBNull.Value)
                        parm[++n].Value = null;
                    else
                        parm[++n].Value = Function.Datetime(dr["MTIME"].ToString());    // MTIME       	datetime	启用时间                    
                    parm[++n].Value = Function.Dec(dr["RECOMMEND"].ToString());         // RECOMMEND   	int(1)	推荐级别                      
                    parm[++n].Value = dr["WORK_SCHEDUL"].ToString();                    // WORK_SCHEDUL	varchar(128)	E工作时间说明                       
                    parm[++n].Value = Function.Dec(dr["SCORE"].ToString());             // SCORE       	decimal(3,1)	评价分数：0~5                      
                    parm[++n].Value = Function.Dec(dr["EVAL_SCORE"].ToString());        // EVAL_SCORE  	int(11)	评价总分                    
                    parm[++n].Value = Function.Dec(dr["EVAL_TIMES"].ToString());        // EVAL_TIMES  	int(11)	评级次数
                    if (svc.ExecSql(Sql, parm) > 0)
                    {
                        response = "SUCCESS";
                    }
                    else
                    {
                        response = "FAIL";
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.2   查询体检登记接口
        /// <summary>
        /// 6.3.2   查询体检登记接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01500(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID，通过ID查询可以精确查询到某条体检登记记录
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", dicKey["CERTID"]));
            // 登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dicKey["ENROLLNO"]));
            // 开始时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<BEGINTIME>{0}</BEGINTIME>", dicKey["BEGINTIME"]));
            // 结束时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ENDTIME>{0}</ENDTIME>", dicKey["ENDTIME"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_01500 where ID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["ID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_01500(SEQNO, ID, VEHICLE, ORDER, ORDERTYPE, NAME, IDCARD, GENDER, AGE, ETHNIC,
                                                        HEAD, CAPHEAD, FACE, MOBILE, OPENID, TYPE, INDUSTRY, DEGREE, ORDERTIME, ENROLLTIME, ENROLLNO )
                                                values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                                        ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(21);
                        parm[++n].Value = dr["SEQNO"].ToString();               // SEQNO	否	number(12)	顺序号                  
                        parm[++n].Value = dr["ID"].ToString();                  // ID	否	varchar(40)	体检登记主表ID                   
                        parm[++n].Value = dr["VEHICLE"].ToString();             // VEHICLE     	否	varchar(40)	体检车辆 车牌号码                      
                        parm[++n].Value = dr["ORDER"].ToString();               // ORDER       	否	varchar(40)	预约单编号                    
                        parm[++n].Value = dr["ORDERTYPE"].ToString();           // ORDERTYPE   	否	varchar(1)	订购类型：1-个人，2-团体，3-临时加号                    
                        parm[++n].Value = dr["NAME"].ToString();                // NAME        	否	varchar(64)	姓名                     
                        parm[++n].Value = dr["IDCARD"].ToString();              // IDCARD      	否	varchar(20)	身份证号码                     
                        parm[++n].Value = dr["GENDER"].ToString();              // GENDER      	否	varchar(1)	性别 ：1-男，2-女                   
                        parm[++n].Value = dr["AGE"].ToString();                 // AGE         	否	int(11)	年龄                     
                        parm[++n].Value = dr["ETHNIC"].ToString();              // ETHNIC      	否	varchar(2)	民族  
                        if (dr["HEAD"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["HEAD"].ToString()));         // HEAD        	否	varchar(40)	身份证头像 BASE64                     
                        if (dr["CAPHEAD"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["CAPHEAD"].ToString()));      // CAPHEAD     	是	varchar(40)	身份证无法识别脸情况，身份证拍照  BASE64                      
                        if (dr["FACE"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["FACE"].ToString()));         // FACE        	是	varchar(40)	登记面部BASE 64                     
                        parm[++n].Value = dr["MOBILE"].ToString();              // MOBILE      	是	varchar(20)	联系电话                     
                        parm[++n].Value = dr["OPENID"].ToString();              // OPENID      	是	varchar(40)	微信号                          
                        parm[++n].Value = dr["TYPE"].ToString();                // TYPE        	否	varchar(1)	行业类别大类                     
                        parm[++n].Value = dr["INDUSTRY"].ToString();            // INDUSTRY    	否	varchar(64)	行业类别小类                      
                        parm[++n].Value = dr["DEGREE"].ToString();              // DEGREE      	是	varchar(40)	文化程度  
                        if (dr["ORDERTIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["ORDERTIME"].ToString());    // ORDERTIME   	否	datetime	预约时间，格式yyyy-MM-dd HH:mm:ss   
                        if (dr["ENROLLTIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["ENROLLTIME"].ToString());   // ENROLLTIME  	否	datetime	登记时间，格式yyyy-MM-dd HH:mm:ss   
                        parm[++n].Value = dr["ENROLLNO"].ToString();            // ENROLLNO    	否	varchar(20)	登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.3   查询体检项目结果接口
        /// <summary>
        /// 6.3.3   查询体检项目结果接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01600(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检项目结果ID,精确查询
            sb.AppendLine(string.Format("<CHECKID>{0}</CHECKID>", dicKey["CHECKID"]));
            // 体检登记信息ID 
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", dicKey["CERTID"]));
            // 体检登记号码，体检机构自定义的唯一体检登记序列号，一个从业人员在此机构一次体检的标志。
            sb.AppendLine(string.Format("<ENROLLNO>{0}</ENROLLNO>", dicKey["ENROLLNO"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_01600 where CERTID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["CERTID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_01600(SEQNO, ID, CERTID, TYPE, ITEM, CHECK_TIME, CHECK_NAME, RESULT, RESULT_TIME, RESULT_NAME, VALID )
                                                values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? ) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(11);
                        parm[++n].Value = dr["SEQNO"].ToString();                   // SEQNO	    否	number(12)	顺序号                  
                        parm[++n].Value = dr["ID"].ToString();                      // ID	        否	varchar(40)	体检项目结果ID                 
                        parm[++n].Value = dr["CERTID"].ToString();                  // CERTID       否	varchar(40)	体检登记信息ID （对应体检登记接口返回的CERTID）                     
                        parm[++n].Value = dr["TYPE"].ToString();                    // TYPE        	否	varchar(1)	体检类别：0-初检，1-复检                  
                        parm[++n].Value = dr["ITEM"].ToString();                    // ITEM        	否	varchar(2)	体检项目大类：01-血液，02-粪便，03-胸透，04-体征     
                        if (dr["CHECK_TIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["CHECK_TIME"].ToString());       // CHECK_TIME  	否	datetime	体检时间，格式yyyy-MM-dd HH:mm:ss   
                        parm[++n].Value = dr["CHECK_NAME"].ToString();              // CHECK_NAME  	否	varchar(64)	体检操作人                 
                        parm[++n].Value = dr["RESULT"].ToString();                  // RESULT      	否	varchar(1)	结果：Y-合格、N-不合格 
                        if (dr["RESULT_TIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["RESULT_TIME"].ToString());      // RESULT_TIME 	否	datetime	结果时间，格式yyyy-MM-dd HH:mm:ss   
                        parm[++n].Value = dr["RESULT_NAME"].ToString();             // RESULT_NAME 	否	varchar(64)	结果录入人                    
                        parm[++n].Value = dr["VALID"].ToString();                   // VALID       	否	varchar(1)	结果有效性，Y-有效，N-无效，当复检结果录入时，把初检的结果设置为无效。                    
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.4   查询体检项目明细接口
        /// <summary>
        /// 6.3.4   查询体检项目明细接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01700(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检项目结果ID,精确查询
            sb.AppendLine(string.Format("<CHECKID>{0}</CHECKID>", dicKey["CHECKID"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_01700 where CHECKID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["CERTID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_01700(SEQNO, CHECKID, ITEM, RESULT, MEMO )
                                                values (?, ?, ?, ?, ? ) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(5);
                        parm[++n].Value = dr["SEQNO"].ToString();                   // SEQNO	否	number(12)	顺序号                  
                        parm[++n].Value = dr["CHECKID"].ToString();                 // CHECKID	否	varchar(40)	体检项目结果ID                 
                        parm[++n].Value = dr["ITEM"].ToString();                    // ITEM     否	varchar(2)	体检项目小类：0101-谷丙转氨酶， 0102-甲型肝炎抗体测定     
                        parm[++n].Value = dr["RESULT"].ToString();                  // RESULT   否	varchar(1)	项目结果，Y-合格，N-不合格，S-可疑 
                        parm[++n].Value = dr["MEMO"].ToString();                    // MEMO 	否	varchar(64)	项目结果描述                    
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.5   查询人脸比对数据接口
        /// <summary>
        /// 6.3.5   查询人脸比对数据接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01800(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", dicKey["CERTID"]));
            // 开始时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<BEGINTIME>{0}</BEGINTIME>", dicKey["BEGINTIME"]));
            // 结束时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ENDTIME>{0}</ENDTIME>", dicKey["ENDTIME"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_01800 where CERTID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["CERTID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_01800(SEQNO, ID, CERTID, NAME, IDCARD, IDHEAD, ENROLLHEAD, BLOODFACE, RESULT, SIMILARITY, CECKTIME, TYPE )
                                                values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? ) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(12);
                        parm[++n].Value = dr["SEQNO"].ToString();                           // SEQNO	    否	number(12)	顺序号                  
                        parm[++n].Value = dr["ID"].ToString();                              // ID	        否	varchar(40)	验血人脸比对数据主表唯一ID                  
                        parm[++n].Value = dr["CERTID"].ToString();                          // CERTID       否	varchar(40)	体检登记信息ID （对应体检登记接口返回的CERTID）                     
                        parm[++n].Value = dr["NAME"].ToString();                            // NAME        	否	varchar(64)	姓名                  
                        parm[++n].Value = dr["IDCARD"].ToString();                          // IDCARD      	否	varchar(20)	身份证号码 
                        if (dr["IDHEAD"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["IDHEAD"].ToString()));       // IDHEAD       否	varchar(40)	身份证头像 BASE64 
                        if (dr["ENROLLHEAD"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["ENROLLHEAD"].ToString()));   // ENROLLHEAD   否	varchar(40)	登记头像   BASE 64           
                        if (dr["BLOODFACE"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.ConvertObjectToByte(Function.ConvertBase64StringToImage(dr["BLOODFACE"].ToString()));    // BLOODFACE    否	varchar(40)	采血登记面部BASE 64 
                        parm[++n].Value = dr["RESULT"].ToString();                          // RESULT	    否	varchar(40)	比对结果
                        parm[++n].Value = dr["SIMILARITY"].ToString();                      // SIMILARITY	否	varchar(40)	比对相似度   
                        if (dr["CECKTIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["CECKTIME"].ToString()); // CECKTIME	    否	datetime	比对时间                   
                        parm[++n].Value = dr["TYPE"].ToString();                            // TYPE	        否	Varchar（40）	类型
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.6   查询健康证打印接口
        /// <summary>
        /// 6.3.6   查询健康证打印接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func02000(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", dicKey["CERTID"]));
            // 开始时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<BEGINTIME>{0}</BEGINTIME>", dicKey["BEGINTIME"]));
            // 结束时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ENDTIME>{0}</ENDTIME>", dicKey["ENDTIME"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_02000 where CERTID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["CERTID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_02000(SEQNO, ID, CERTID, USER, TIME, NAME, TEAM, MOBILE, INDUSTRY )
                                                values (?, ?, ?, ?, ?, ?, ?, ?, ? ) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(9);
                        parm[++n].Value = dr["SEQNO"].ToString();                           // SEQNO	    否	number(12)	顺序号                  
                        parm[++n].Value = dr["ID"].ToString();                              // ID	        否	varchar(40)	健康证打印主表唯一ID                  
                        parm[++n].Value = dr["CERTID"].ToString();                          // CERTID       否	varchar(40)	体检登记信息ID （对应体检登记接口返回的CERTID）                     
                        parm[++n].Value = dr["USER"].ToString();                            // USER    	    是	varchar(40)	打印操作人                  
                        if (dr["TIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["TIME"].ToString());     // TIME    	    是	datetime	打印时间，格式yyyy-MM-dd HH:mm:ss
                        parm[++n].Value = dr["NAME"].ToString();                            // NAME    	    是	varchar(40)	健康证姓名
                        parm[++n].Value = dr["TEAM"].ToString();                            // TEAM    	    否	varchar(40)	所属单位    
                        parm[++n].Value = dr["MOBILE"].ToString();                          // MOBILE  	    是	varchar(40)	健康证联系方式                  
                        parm[++n].Value = dr["INDUSTRY"].ToString();                        // INDUSTRY	    是	varchar(40)	行业类别小类
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 6.3.7   查询短信通知接口
        /// <summary>
        /// 6.3.7   查询短信通知接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func02100(string funcCode, Dictionary<string, string> dicKey)
        {
            StringBuilder sb = new StringBuilder();
            // 体检登记信息ID
            sb.AppendLine(string.Format("<CERTID>{0}</CERTID>", dicKey["CERTID"]));
            // 手机号码
            sb.AppendLine(string.Format("<MOBILE>{0}</MOBILE>", dicKey["MOBILE"]));
            // 开始时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<BEGINTIME>{0}</BEGINTIME>", dicKey["BEGINTIME"]));
            // 结束时间，格式yyyy-MM-dd HH:mm:ss
            sb.AppendLine(string.Format("<ENDTIME>{0}</ENDTIME>", dicKey["ENDTIME"]));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                string Sql = string.Empty;
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("ROW") && ds.Tables["ROW"].Rows.Count > 0)
                {
                    SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = null;

                    Sql = @"delete from table_jkz_02100 where CERTID = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = ds.Tables["ROW"].Rows[0]["CERTID"].ToString();
                    svc.ExecSql(Sql, parm);

                    Sql = @"insert into table_jkz_02100(SEQNO, ID, CERTID, MOBILE, CONTENT, TIME )
                                                values (?, ?, ?, ?, ?, ? ) ";
                    foreach (DataRow dr in ds.Tables["ROW"].Rows)
                    {
                        int n = -1;
                        parm = svc.CreateParm(6);
                        parm[++n].Value = dr["SEQNO"].ToString();                           // SEQNO	    否	number(12)	顺序号                  
                        parm[++n].Value = dr["ID"].ToString();                              // ID	        否	varchar(40)	健康证打印主表唯一ID                  
                        parm[++n].Value = dr["CERTID"].ToString();                          // CERTID       否	varchar(40)	体检登记信息ID （对应体检登记接口返回的CERTID）                     
                        parm[++n].Value = dr["MOBILE"].ToString();                          // MOBILE  	    是	varchar(40)	健康证联系方式     
                        parm[++n].Value = dr["CONTENT"].ToString();                         // CONTENT	    否	varchar(512)	发送内容
                        if (dr["TIME"] == DBNull.Value)
                            parm[++n].Value = null;
                        else
                            parm[++n].Value = Function.Datetime(dr["TIME"].ToString());     // TIME    	    是	datetime	打印时间，格式yyyy-MM-dd HH:mm:ss
                        if (svc.ExecSql(Sql, parm) > 0)
                        {
                            response = "SUCCESS";
                        }
                        else
                        {
                            response = "FAIL";
                            return response;
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #endregion

        #region 6.4 业务回退接口部分

        #region 6.4.1   体检登记回退接口
        /// <summary>
        /// 6.4.1   体检登记回退接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00800(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;

            string certId = dt.Rows[0]["certId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 使用体检登记接口返回的唯一码
            sb.AppendLine(string.Format("<OCERTID>{0}</OCERTID>", certId));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                }
            }
            return response;
        }
        #endregion

        #region 6.4.2   体检项目结果回退接口
        /// <summary>
        /// 6.4.2   体检项目结果回退接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func00900(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            // 使用体检结果明细接口返回的唯一码                                                        
            string checkId = dt.Rows[0]["checkId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 使用体检结果明细接口返回的唯一码
            sb.AppendLine(string.Format("<OCHECKID>{0}</OCHECKID>", checkId));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                }
            }
            return response;
        }
        #endregion

        #region 6.4.3   人脸比对数据回退接口
        /// <summary>
        /// 6.4.3   人脸比对数据回退接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01100(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            // 使用人脸比对数据接口返回的唯一码                                                         
            string resultId = dt.Rows[0]["resultId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 使用人脸比对数据接口返回的唯一码                                                        
            sb.AppendLine(string.Format("<ORESULTID>{0}</ORESULTID>", resultId));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                }
            }
            return response;
        }
        #endregion

        #region 6.4.4   健康证打印回退接口
        /// <summary>
        /// 6.4.4   健康证打印回退接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01300(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            // 使用健康证打印接口返回的打印ID                                                        
            string printId = dt.Rows[0]["printId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 使用健康证打印接口返回的打印ID                                                                                                           
            sb.AppendLine(string.Format("<OPRINTID>{0}</OPRINTID>", printId));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                }
            }
            return response;
        }
        #endregion

        #region 6.4.5   短信通知上传回退接口
        /// <summary>
        /// 6.4.5   短信通知上传回退接口
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public string Func01400(string funcCode, Dictionary<string, string> dicKey)
        {
            string regNo = dicKey["regNo"];
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = @"select * from table_jkz where regNo = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = regNo;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            // 使用健康证打印接口返回的打印ID                                                        
            string smsId = dt.Rows[0]["smsId"].ToString();

            StringBuilder sb = new StringBuilder();
            // 使用短信通知上传接口返回的短信唯一码                                                                                                                                                                 
            sb.AppendLine(string.Format("<OSMSID>{0}</OSMSID>", smsId));
            string response = this.GetResponseXml(funcCode, sb.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                DataSet ds = Function.ReadXml(response);
                if (ds.Tables.Contains("BODY") && ds.Tables["BODY"].Rows.Count > 0)
                {
                    response = ds.Tables["BODY"].Rows[0]["RESULT"].ToString() + " " + ds.Tables["BODY"].Rows[0]["REMESSAGE"].ToString();
                }
            }
            return response;
        }
        #endregion

        #endregion

        #region GetRequestXml
        /// <summary>
        /// GetRequestXml
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetRequestXml(string request)
        {
            return "<Request>" + Environment.NewLine + request + Environment.NewLine + "</Request>";
        }
        #endregion

        #region GetHeaderXml
        /// <summary>
        /// GetHeaderXml
        /// </summary>
        /// <param name="funcCode"></param>
        /// <returns></returns>
        public string GetHeaderXml(string funcCode)
        {
            string sign = string.Empty;
            if (funcCode != "900100") sign = Func900100("900100", null);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<APPCODE>{0}</APPCODE>", Function.ReadConfigXml("appCode")));
            sb.AppendLine(string.Format("<METHOD>{0}</METHOD>", funcCode));
            sb.AppendLine(string.Format("<VERSION>{0}</VERSION>", "2.0"));
            sb.AppendLine(string.Format("<SIGN>{0}</SIGN>", string.IsNullOrEmpty(sign) ? "" : weCare.Core.Utils.ESCryptography.EncryptMD5(sign).ToLower()));
            sb.AppendLine(string.Format("<REQTIME>{0}</REQTIME>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd")));
            sb.AppendLine(string.Format("<ORGCODE>{0}</ORGCODE>", Function.ReadConfigXml("orgCode")));
            return sb.ToString();
        }
        #endregion

        #region GetRequestXml
        /// <summary>
        /// GetRequestXml
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="bodyXml"></param>
        /// <returns></returns>
        public string GetRequestXml(string funcCode, string bodyXml)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<REQUEST>");
            sb.AppendLine("<HEADER>");
            sb.AppendLine(GetHeaderXml(funcCode));
            sb.AppendLine("</HEADER>");
            sb.AppendLine("<BODY>");
            sb.AppendLine(bodyXml);
            sb.AppendLine("</BODY>");
            sb.AppendLine("</REQUEST>");
            Log.Output(funcCode + "-->\r\n" + sb.ToString());
            return sb.ToString();
        }
        #endregion

        #region GetMd5Str
        /// <summary>
        /// GetMd5Str
        /// </summary>
        /// <param name="oriValue"></param>
        /// <returns></returns>
        public string GetMd5Str(string oriValue)
        {
            string md5Str = weCare.Core.Utils.ESCryptography.EncryptMD5(oriValue);
            return oriValue.Replace("<sign></sign>", "<sign>" + md5Str + "</sign>");
        }
        #endregion

        #region GetResponseXml
        /// <summary>
        /// GetResponseXml
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string GetResponseXml(string funcCode, string xml)
        {
            string request = weCare.Core.Utils.ESCryptography.EncryptBase64(System.Text.Encoding.UTF8, GetRequestXml(funcCode, xml));
            WebserviceCallEntranceImplService svc = new WebserviceCallEntranceImplService();
            string response = svc.CallFun(request);
            if (!string.IsNullOrEmpty(response)) response = ESCryptography.DecryptBase64(System.Text.Encoding.UTF8, response);
            Log.Output(funcCode + "-->\r\n" + response);
            return response;
        }
        #endregion


        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="Path">文件地址</param>
        public string  ReadTxtContent(string Path)
        {
            StreamReader sr = new StreamReader(Path, Encoding.Default);
            string content = string.Empty;
            content = sr.ReadLine();
            
            return content;
        }

        #endregion

        #region Verify
        /// <summary>
        /// Verify
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        { return true; }
        #endregion

        #region IDispose
        /// <summary>
        /// IDispose
        /// </summary>
        public void Dispose()
        { GC.SuppressFinalize(this); }
        #endregion
    }
}