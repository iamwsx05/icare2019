using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace YH_30
{
    public class DataAnalysis_YH_30 : clsLISDataAnalysisBase, infLISDataAnalysis
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
            string strLog = string.Empty;
            foreach (string item in data)
            {
                // 包含样本号
                if (item.Trim().StartsWith("3R"))
                {
                    tmpData = item.Split('|');
                    if (tmpData != null && tmpData.Length > 3)
                    {
                        sampleID = tmpData[1].Split('-')[1];
                        int no = 0;
                        int.TryParse(sampleID, out no);
                        if (no > 0)
                        {
                            if (no >= 1000)
                                sampleID = no.ToString();
                            else
                                sampleID = no.ToString("000");
                        }
                    }
                }
                if (item.Trim().StartsWith("3R") == false || string.IsNullOrEmpty(sampleID)) continue;

                tmpData = item.Split(new char[] { '|' });
                if (tmpData.Length < 3) continue;

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleID;
                
                vo.strCheck_Date = checkDate;
                vo.strResult = tmpData[6].Trim();   // 检验结果"
                vo.strDevice_Check_Item_Name = "HbA1c";
                strLog += vo.strDevice_Check_Item_Name + Environment.NewLine;
                strLog += vo.strResult + Environment.NewLine ;

                p_arlResult.Add(vo);
            }

            Log.Output(strLog);
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
