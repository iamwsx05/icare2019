using System;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace weCare.eApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region args

            //args = new string[1];
            //args[0] = @"<patientId>888888</patientId>";
            //            args[0] = @"<request>
            //                                    <sourceId>1</sourceId>
            //                                    <classCode>0001</classCode>
            //                                    <patientId>888888</patientId>
            //                                    <patientName>测试人员</patientName>
            //                                    <cardNo>0000000001</cardNo>
            //                                    <registerId>666666</registerId>
            //                                    <appDeptId>1234567</appDeptId>
            //                                    <appDeptName>信息科</appDeptName>
            //                                    <appDoctId>0001</appDoctId>
            //                                    <appDoctName>admin</appDoctName>
            //                                    </request>";
            //             args[0] = @"<request>
            //                         <sourceId>1</sourceId>
            //                         <registerId>0000298554</registerId>
            //                         <patientId>0000298554</patientId>
            //                         <patientName>吴敏</patientName>
            //                         <sex>女</sex>
            //                         <birthday>1979-01-01 00:00:00</birthday>
            //                         <cardNo>0000000003</cardNo>
            //                         <ipNo></ipNo>
            //                         <bedNo></bedNo>
            //                         <homeTel>88453736</homeTel>
            //                         <homeAddr>向西工业园</homeAddr>
            //                         <marriage>未婚</marriage>
            //                         <occupation></occupation>
            //                         <nativeplace></nativeplace>
            //                         <appDeptId>0000232</appDeptId>
            //                         <appDeptName>儿科门诊</appDeptName>
            //                         <appDoctId>0000001</appDoctId>
            //                         <appDoctName>系统管理员</appDoctName>
            //                         <payTypeId>0001</payTypeId>
            //                         </request>";
            //            args[0] = @"<request>
            //                                     <sourceId>2</sourceId>
            //                                     <registerId>000000138628</registerId>
            //                                     <patientId>0001359942</patientId>
            //                                     <patientName>张一夫</patientName>
            //                                     <sex>男</sex>
            //                                     <birthday>1900-01-01 00:00:00</birthday>
            //                                     <cardNo></cardNo>
            //                                     <ipNo>777777</ipNo>
            //                                     <bedNo>001</bedNo>
            //                                     <homeTel></homeTel>
            //                                     <homeAddr>UUU</homeAddr>
            //                                     <marriage>未婚</marriage>
            //                                     <occupation></occupation>
            //                                     <nativeplace></nativeplace>
            //                                     <appDeptId>0000232</appDeptId>
            //                                     <appDeptName>儿科门诊</appDeptName>
            //                                     <appDoctId>0000001</appDoctId>
            //                                     <appDoctName>系统管理员</appDoctName>
            //                                     <payTypeId>0031</payTypeId>
            //                                     <currAreaId>0000222</currAreaId>
            //                                     <currBedId>0010200</currBedId>
            //                                     </request>";

            #endregion

            if (ProcessNums() == 0)
            {
                string parm0 = string.Empty;
                if (args != null && args.Length > 0)
                {
                    parm0 = args[0];
                    Log.Output(parm0);
                }
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    // 注册皮肤,用于某些DEV控件在表单书写时的视觉样式
                    DevExpress.UserSkins.BonusSkins.Register();
                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    // 运行模式
                    int intRunningMode = Tool.Int(Tool.ReadLocalSettingValue("Main|runningMode", "value"));
                    GlobalAppConfig.RunningMode = (intRunningMode < 2 ? 2 : intRunningMode);

                    if (!string.IsNullOrEmpty(parm0) && parm0.StartsWith("<patientId>"))
                    {
                        parm0 = "<request>" + Environment.NewLine + parm0 + Environment.NewLine + "</request>";
                        Application.Run(new frmQuery(parm0));
                    }
                    else
                    {
                        Application.Run(new frmConsole(parm0));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    Application.Exit();
                }
                finally
                { }
            }
            else
            {
                MessageBox.Show("eApp.exe 已经在运行...", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static int ProcessNums()
        {
            int count = 0;
            System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processes)
            {
                if (process.Id != current.Id && process.ProcessName == current.ProcessName) // 查找相同名称的进程.忽略当前进程 
                {
                    count++;
                }
            }
            return count;
        }
    }

    #region Tool

    public class Tool
    {
        #region 获取UPDATE.XML信息
        /// <summary>
        /// 获取UPDATE.XML信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUpdateXmlValue(string key)
        {
            string strValue = string.Empty;
            string strFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\update.client.xml";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(strFile);
            System.Xml.XmlElement element = doc["Main"][key];
            if (element != null)
            {
                strValue = element.Attributes["value"].Value.Trim();
            }

            doc = null;
            element = null;
            return strValue;
        }
        #endregion

        #region 从本地配置文件读取参数值

        /// <summary>
        /// 本地配置文件
        /// </summary>
        private static string LocalSettingFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\app.xml";

        /// <summary>
        /// 从本地配置文件读取参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <returns></returns>
        public static string ReadLocalSettingValue(string p_strNode, string p_strKey)
        {
            string strValue = string.Empty;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                System.Xml.XmlElement element = null;
                doc.Load(LocalSettingFile);

                int n = -1;
                string[] strNodeArr = p_strNode.Split('|');
                switch (strNodeArr.Length)
                {
                    case 1:
                        element = doc[strNodeArr[++n]];
                        break;
                    case 2:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 3:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 4:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 5:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 6:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 7:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 8:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 9:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 10:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    default:
                        return string.Empty;
                }

                if (element != null)
                {
                    strValue = element.Attributes[p_strKey].Value.Trim();
                }
            }
            catch
            {
            }
            finally
            {
                doc = null;
            }
            return strValue;
        }
        #endregion

        public static int Int(string str)
        {
            int i = 0;
            int.TryParse(str, out i);
            return i;
        }
    }

    #endregion

    #region Access
    /// <summary>
    /// Access
    /// </summary>
    public class Access
    {
        public void Invoke(string request)
        {
            if (!string.IsNullOrEmpty(request) && request.StartsWith("<patientId>"))
            {
                request = "<request>" + Environment.NewLine + request + Environment.NewLine + "</request>";
                frmQuery frm = new frmQuery(request);
                //frm.ShowDialog();
                frm.Show();
            }
            else
            {
                frmConsole frm = new frmConsole(request);
                //frm.ShowDialog(); 
                frm.Show();
            }
        }
    }
    #endregion
}
