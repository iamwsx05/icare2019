using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace VITROS_350
{
    public class DataAnalysis_VITROS_350 : clsLISDataAnalysisBase, infLISDataAnalysis
    {
        public System.Data.DataTable dtConfig { get; set; }

        // Methods
        public long lngDataAnalysis(string p_strRawData, out ArrayList p_arlResult)
        {
            p_arlResult = null;
            return 0L;
        }

        public string[] strGetIntactData(string p_strRawData)
        {
            return null;
        }

        public long lngDataAnalysis(string recData, out List<clsLIS_Device_Test_ResultVO> p_arlResult)
        {
            p_arlResult = null;
            if ((recData == "") || (recData == null))
            {
                return 0;
            }

            string[] data = recData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (data == null || data.Length <= 0)
            {
                return -1;
            }

            p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            clsLIS_Device_Test_ResultVO vo = null;
            string sampleID = string.Empty;
            string barCode = string.Empty;
            string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] tmpData = null;
            List<string> lstData = null;

            foreach (string item in data)
            {
                if (item.Trim().StartsWith(VITROS_350_ControlCode.StartCode))
                {
                    sampleID = item.Substring(29, 4).Trim();
                    continue;
                }
                if (item.Contains("!0") && item.Contains("h"))
                    continue;

                Log.Output("item-->" + item);
                // 包含样本号
                if (item.Trim().StartsWith("!0"))
                {
                    tmpData = item.Split(' ');
                    if (tmpData != null && tmpData.Length > 3)
                    {
                        vo = new clsLIS_Device_Test_ResultVO();
                        lstData = new List<string>();
                        for (int i = 0; i < tmpData.Length; i++)
                        {
                            string tmp = tmpData[i].Trim();
                            if (string.IsNullOrEmpty(tmp))
                                continue;
                            lstData.Add(tmp);
                        }
                        vo.strDevice_Sample_ID = sampleID;
                        vo.strDevice_Check_Item_Name = lstData[0].Substring(5, lstData[0].Length - 5);  // 名称
                        if (vo.strDevice_Check_Item_Name.Contains("99999.99"))
                            continue;
                        string strResult = lstData[1];   // 检验结果
                        if (strResult.Contains("99999.99"))
                            strResult = "/";
                        vo.strResult = strResult.Replace("mmol/L", " ").Replace("mg/dL", " ").Replace("U/mL", " ").Replace("U/L", " ").Trim();
                        if (vo.strResult.EndsWith("."))
                            vo.strResult = vo.strResult.TrimEnd('.');
                        if (vo.strResult.StartsWith("."))
                            vo.strResult = "0" + vo.strResult;
                        p_arlResult.Add(vo);
                    }
                }
            }

            return 1;
        }

    }

    public class Log
    {
        public static void Output(string txt)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string strFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + ".txt";
            bool blnAllWaysNew = false;
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(strFile);
                if (fi.Exists)
                {
                    if (fi.Length >= 2000000)
                    {
                        fi.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + "-" + DateTime.Now.ToString("HHmm") + ".txt", true);
                        sw = fi.CreateText();
                    }
                    else
                    {
                        if (blnAllWaysNew)
                        {
                            sw = fi.CreateText();
                        }
                        else
                        {
                            sw = fi.AppendText();
                        }
                    }
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine("-->>>>> " + strTime);
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public static void Output(string fileName, string txt)
        {
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    sw = fi.AppendText();
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
