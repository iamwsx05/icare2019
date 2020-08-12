using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PATHFAST
{
    public class DataAnalysis_PATHFAST : clsLISDataAnalysisBase, infLISDataAnalysis
    {
        public Dictionary<string, string> dicField { get; set; }

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
            if (string.IsNullOrEmpty(recData)) return 0;

            p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            clsLIS_Device_Test_ResultVO vo = null;
            string strSampleID = string.Empty;
            string strCheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int idx = 0;
            string[] strDataArr = recData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (strDataArr == null || strDataArr.Length <= 0)
            {
                return -1;
            }

            string strTemp = null;
            int idxNext = 0;
            int idxNextNext = 0;
            string strCheckItemName = null;

            for (int i = 0; i < strDataArr.Length; i++)
            {
                strTemp = strDataArr[i];
                if (!strTemp.Contains("^||^^^") && !strTemp.Contains("^F|"))
                {
                    continue;
                }

                if (strTemp.Contains("^||^^^"))
                {
                    idx = strTemp.IndexOf("|");
                    idxNext = strTemp.IndexOf("|", idx + 1);
                    idxNextNext = strTemp.IndexOf("^", idxNext + 1);
                    strSampleID = strTemp.Substring(idxNext + 1, idxNextNext - idxNext - 1);
                    if (int.TryParse(strSampleID, out idx))
                    {
                        if (idx > 1000)
                        {
                            strSampleID = idx.ToString();
                        }
                        else
                        {
                            strSampleID = idx.ToString("000");
                        }
                    }
                    idx = strTemp.IndexOf("^||^^^") + 6;
                    idxNext = strTemp.IndexOf("^", idx);
                    idxNextNext = strTemp.IndexOf("^", idxNext + 1);
                    strCheckItemName = strTemp.Substring(idxNext + 1, idxNextNext - idxNext - 1);
                }

                if (strTemp.Contains("^F|"))
                {
                    vo = new clsLIS_Device_Test_ResultVO();
                    vo.strDevice_Check_Item_Name = strCheckItemName;
                    idx = strTemp.IndexOf("||System||") + 10;

                    try
                    {
                        strCheckDate = strTemp.Substring(idx, 4) + "-" + strTemp.Substring(idx + 4, 2) + "-" + strTemp.Substring(idx + 6, 2) + " "
                        + strTemp.Substring(idx + 8, 2) + ":" + strTemp.Substring(idx + 10, 2) + ":" + strTemp.Substring(idx + 12, 2);
                        DateTime dtTime = Convert.ToDateTime(strCheckDate);
                    }
                    catch
                    {
                        strCheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    vo.strCheck_Date = strCheckDate;
                    vo.strDevice_Sample_ID = strSampleID;
                    idx = strTemp.IndexOf("|");
                    idxNext = strTemp.IndexOf("|", idx + 1);
                    idxNextNext = strTemp.IndexOf("|", idxNext + 1);
                    idx = strTemp.IndexOf("^F|");
                    vo.strResult = strTemp.Substring(idxNextNext + 1, idx - idxNextNext - 1).Trim();
                    vo.strDevice_Check_Item_Name = this.m_strConvertItem(vo.strDevice_Check_Item_Name);

                    p_arlResult.Add(vo);
                }
            }

            return 1;
        }
    }
}
