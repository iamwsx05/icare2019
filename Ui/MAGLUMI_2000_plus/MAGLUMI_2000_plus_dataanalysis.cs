using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MAGLUMI_2000_plus
{
    public class DataAnalysis_MAGLUMI_2000_plus : clsLISDataAnalysisBase, infLISDataAnalysis
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
            string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] tmpData = null;
            foreach (string item in data)
            {
                // 包含样本号
                if (item.Trim().StartsWith("O|"))
                {
                    tmpData = item.Split('|');
                    if (tmpData != null && tmpData.Length > 3)
                    {
                        sampleID = tmpData[2];
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
                    continue;
                }
                if (item.Trim().StartsWith("R|") == false || string.IsNullOrEmpty(sampleID)) continue;

                tmpData = item.Split(new char[] { '|' });
                if (tmpData.Length < 3) continue;

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleID;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = tmpData[2].Replace("^", "").Trim();  // 名称
                vo.strResult = tmpData[3].Trim();   // 检验结果

                // 读取配置
                if (this.dtConfig != null && this.dtConfig.Rows.Count > 0)
                {
                    for (int i2 = 0; i2 < this.dtConfig.Columns.Count; i2++)
                    {
                        if (this.dtConfig.Columns[i2].ColumnName == "F" + vo.strDevice_Check_Item_Name)
                        {
                            vo.strDevice_Check_Item_Name = this.dtConfig.Rows[0]["F" + vo.strDevice_Check_Item_Name].ToString().Trim();
                            break;
                        }
                    }
                }
                p_arlResult.Add(vo);
            }
            return 1;
        }
    }
}
