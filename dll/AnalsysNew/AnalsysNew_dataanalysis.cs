using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AnalsysNew
{
    public class DataAnalysis_AnalsysNew : clsLISDataAnalysisBase, infLISDataAnalysis
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

        public long lngDataAnalysis(string p_strRawData, out List<clsLIS_Device_Test_ResultVO> p_arlResult)
        {
            // 数据格式
            //ANALYSIS     
            //PATIENT'S NAME  

            //________________________
            //SAMPLE  010
            //Na 138.2 K  3.20 Cl 109.7
            //         K LO    Cl HI
            //BLOOD Na,K,Cl mmol/L  
            //(CORRELATED VALUES) 
            //MAY-14-18; 08:45

            p_arlResult = null;
            if ((p_strRawData == "") || (p_strRawData == null))
            {
                return 0L;
            }
            p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            string data = p_strRawData;

            clsLIS_Device_Test_ResultVO vo = null;
            string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int poss = data.IndexOf("SAMPLE");
            int poss2 = data.IndexOf("BLOOD Na,K,Cl");
            if (poss2 - poss > 0)
            {
                data = data.Substring(poss, poss2 - poss);

                poss = data.IndexOf("Na");
                string sampleId = data.Substring(6, poss - 6).Trim();
                data = data.Substring(poss);

                poss = data.IndexOf("K");
                string Na_value = data.Substring(2, poss - 2).Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                data = data.Substring(poss);

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "Na";            // 名称         Na
                vo.strResult = Na_value;                        // 检验结果
                p_arlResult.Add(vo);

                poss = data.IndexOf("Cl");
                string K_value = data.Substring(1, poss - 1).Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                data = data.Substring(poss);

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "K";             // 名称         K
                vo.strResult = K_value;                         // 检验结果
                p_arlResult.Add(vo);

                string Cl_value = string.Empty;
                poss = -1;
                if (data.IndexOf("Na") >= 0)
                    poss = data.IndexOf("Na");
                else if (data.IndexOf("K") >= 0)
                    poss = data.IndexOf("K");
                else if (data.IndexOf("Cl") >= 3)
                    poss = data.IndexOf("Cl");
                if (poss >= 0)
                {
                    Cl_value = data.Substring(2, poss - 2).Trim();
                    Cl_value = Cl_value.Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                }
                else
                    Cl_value = data.Replace("Cl", "").Replace("LO", "").Replace("HI", "").Trim();

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "Cl";            // 名称         Cl
                vo.strResult = Cl_value.Replace("LO", "").Replace("HI", "").Replace("L0", "").Trim();                        // 检验结果
                p_arlResult.Add(vo);
            }
            else
            {
                string sampleId = data.Substring(poss + 6, 5).Trim();
                poss = data.IndexOf("Na");
                data = data.Substring(poss);

                poss = data.IndexOf("K");
                string Na_value = data.Substring(2, poss - 2).Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                data = data.Substring(poss);

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "Na";            // 名称         Na
                vo.strResult = Na_value;                        // 检验结果
                p_arlResult.Add(vo);

                poss = data.IndexOf("Cl");
                string K_value = data.Substring(1, poss - 1).Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                data = data.Substring(poss);

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "K";             // 名称         K
                vo.strResult = K_value;                         // 检验结果
                p_arlResult.Add(vo);

                string Cl_value = string.Empty;
                poss = -1;
                if (data.IndexOf("Na") >= 0)
                    poss = data.IndexOf("Na");
                else if (data.IndexOf("K") >= 0)
                    poss = data.IndexOf("K");
                else if (data.IndexOf("Cl") >= 3)
                    poss = data.IndexOf("Cl");
                if (poss >= 0)
                {
                    Cl_value = data.Substring(2, poss - 2).Trim();
                    Cl_value = Cl_value.Replace('^', ' ').Replace("LO", "").Replace("HI", "").Trim();
                }
                else
                    Cl_value = data.Replace("Cl", "").Replace("LO", "").Replace("HI", "").Trim();

                vo = new clsLIS_Device_Test_ResultVO();
                vo.strDevice_Sample_ID = sampleId;
                vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = "Cl";            // 名称         Cl
                vo.strResult = Cl_value.Replace("LO", "").Replace("HI", "").Replace("L0", "").Trim();                        // 检验结果
                p_arlResult.Add(vo);
            }

            return (p_arlResult.Count > 0 ? 1 : 0);
        }
    }
}
