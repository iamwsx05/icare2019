using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Reflection;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public class GridCaption
    {
        DrawingGrid ParentGrid;
        public GridCaption(DrawingGrid parent)
        {
            ParentGrid = parent;
            this.HeightUnit = ThreeItemConstValue.CaptionHeightUnit;
        }
        
        public void Init()
        {
        }

        private string m_strFill(int p_intNums)
        {
            string str = string.Empty;
            for (int i = 0; i < p_intNums; i++)
            {
                str += "  ";
            }
            return str;
        }

        /// <summary>
        /// 画表头
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="blnLastPage">是否最后一页</param>
        public void Paint(EntityEmrTemperaturePatInfo pInfo, int pageIndex, bool blnLastPage)
        {
            string text = string.Empty;// = string.Format("姓名：{0}", PatName);
            if (pInfo != null && !string.IsNullOrEmpty(pInfo.Name))
            {
                string strDispAreaNo = string.Empty;
                string strDispBedNo = string.Empty;
                //获取病区和床号信息
                int intRegID = Convert.ToInt32(pInfo.RegID);
                //clsProxyThreeItems objProxy = new clsProxyThreeItems();
                DataTable dtResult = null;               
                //if (objProxy.Service != null)
                //{
                //    dtResult = objProxy.Service.m_dtGetSelfDefineCol(intRegID, pageIndex + 1);
                //}
                //else
                //{
                //    //使用代理
                //    Type type = null;
                //    object obj = null;

                //    string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\EMR.EnterpriseServices.dll";
                //    Assembly assembly = Assembly.LoadFrom(strPath);
                //    type = assembly.GetType("com.HopeBridge.ehr.biz.clsBizEhrThreeItems");
                //    obj = Activator.CreateInstance(type);

                //    MethodInfo objMethodInfo = type.GetMethod("m_dtGetSelfDefineCol");
                //    object[] invokeArgs = new object[] { intRegID, pageIndex + 1 };
                //    dtResult = (DataTable)objMethodInfo.Invoke(obj, invokeArgs);
                //}
                if (dtResult != null)
                {
                    string strAreaNo = string.Empty;
                    string strBedNo = string.Empty;

                    DataRow[] drSelArr = dtResult.Select("colcode_vchr = 'AreaNo'");
                    if (drSelArr != null && drSelArr.Length > 0)
                    {
                        strAreaNo = drSelArr[0]["coldesc_vchr"].ToString();
                    }
                    drSelArr = dtResult.Select("colcode_vchr = 'BedNo'");
                    if (drSelArr != null && drSelArr.Length > 0)
                    {
                        strBedNo = drSelArr[0]["coldesc_vchr"].ToString();
                    }

                    if (!string.IsNullOrEmpty(strAreaNo))
                    {
                        if (blnLastPage)
                        {
                            if (strAreaNo.EndsWith(pInfo.Area))
                            {
                                strDispAreaNo = strAreaNo;
                            }
                            else//转了科
                            {
                                strDispAreaNo = strAreaNo + "->" + pInfo.Area;
                            }
                        }
                        else
                        {
                            strDispAreaNo = strAreaNo;
                        }
                    }

                    if (!string.IsNullOrEmpty(strBedNo))
                    {
                        if (blnLastPage)
                        {
                            if (strBedNo.EndsWith(pInfo.BedNo))
                            {
                                strDispBedNo = strBedNo;
                            }
                            else//转了床
                            {
                                strDispBedNo = strBedNo + "->" + pInfo.BedNo;
                            }
                        }
                        else
                        {
                            strDispBedNo = strBedNo;
                        }
                    }
                }

                if (string.IsNullOrEmpty(strDispAreaNo))
                {
                    strDispAreaNo = pInfo.Area;
                }
                if (string.IsNullOrEmpty(strDispBedNo))
                {
                    strDispBedNo = pInfo.BedNo;
                }

                text = pInfo.ToString();

                string[] strPatNameArr = new string[3] { "      ", "", "      " };
                string[] strPatAgeArr = new string[3] { "    ", "", "    " };
                string[] strPatSexArr = new string[3] { "  ", "", "  " };
                string[] strInDateArr = new string[3] { "              ", "", "              " };
                string[] strDeptNameArr = new string[3] { "        ", "", "        " };
                string[] strBedNoArr = new string[3] { "     ", "", "     " };
                string[] strIpNoArr = new string[3] { "      ", "", "      " };

                if (pInfo.Name.Length > 3)
                {
                    strPatNameArr[0] = pInfo.Name.Substring(0, 3);
                    if (pInfo.Name.Length > 6)
                        strPatNameArr[1] = pInfo.Name.Substring(3, 3);
                    else
                        strPatNameArr[1] = pInfo.Name.Substring(3) + m_strFill(3 - pInfo.Name.Substring(3).Length);
                }
                else
                {
                    strPatNameArr[1] = pInfo.Name + m_strFill(3 - pInfo.Name.Length);
                }
                if (pInfo.Age.Length > 3)
                {
                    strPatAgeArr[0] = pInfo.Age.Substring(0, 3);
                    if (pInfo.Age.Length > 6)
                        strPatAgeArr[1] = pInfo.Age.Substring(3, 3);
                    else
                        strPatAgeArr[1] = pInfo.Age.Substring(3) + m_strFill(2 - pInfo.Age.Substring(3).Length);
                }
                else
                {
                    strPatAgeArr[1] = pInfo.Age + m_strFill(2 - pInfo.Age.Length);
                }
                if (pInfo.Sex.Length > 1)
                {
                    strPatSexArr[0] = pInfo.Sex.Substring(0, 1);
                    if (pInfo.Sex.Length > 2)
                        strPatSexArr[1] = pInfo.Sex.Substring(1, 1);
                    else
                        strPatSexArr[1] = pInfo.Sex.Substring(1);
                }
                else
                {
                    strPatSexArr[1] = pInfo.Sex;
                }
                strInDateArr[1] = pInfo.InDate.ToString("yyyy年MM月dd日");

                if (strDispAreaNo.Length > 4)
                {
                    strDeptNameArr[0] = strDispAreaNo.Substring(0, 4);
                    if (strDispAreaNo.Length > 8)
                    {
                        strDeptNameArr[1] = strDispAreaNo.Substring(4, 4);
                        if (strDispAreaNo.Length > 16)
                        {
                            strDeptNameArr[2] = strDispAreaNo.Substring(8, 8);
                        }
                        else
                        {
                            strDeptNameArr[2] = strDispAreaNo.Substring(8);
                        }
                    }
                    else
                    {
                        strDeptNameArr[1] = strDispAreaNo.Substring(4) + m_strFill(4 - strDispAreaNo.Substring(4).Length);
                    }
                }
                else
                {
                    strDeptNameArr[1] = strDispAreaNo + m_strFill(4 - strDispAreaNo.Length);
                }

                if (strDispBedNo.Length > 7)
                {
                    strBedNoArr[0] = strDispBedNo.Substring(0, 7);
                    if (strDispBedNo.Length > 12)
                    {
                        strBedNoArr[1] = strDispBedNo.Substring(7, 5);
                        if (strDispBedNo.Length > 18)
                        {
                            strBedNoArr[2] = strDispBedNo.Substring(12, 6);
                        }
                        else
                        {
                            strBedNoArr[2] = strDispBedNo.Substring(12);
                        }
                    }
                    else
                    {
                        strBedNoArr[1] = strDispBedNo.Substring(7) + m_strFill(2 - strDispBedNo.Substring(7).Length);
                    }
                }
                else
                {
                    strBedNoArr[1] = strDispBedNo + m_strFill(2 - strDispBedNo.Length);
                }
                strIpNoArr[1] = pInfo.IpNo;

                string strTitle1 = string.Format("      {0}       {1}       {2}           {3}    {4}       {5}         {6}",
                                                 strPatNameArr[0], strPatAgeArr[0], strPatSexArr[0], strInDateArr[0], strDeptNameArr[0], strBedNoArr[0], strIpNoArr[0]);
                this.ParentGrid.graphics.DrawString(strTitle1, new System.Drawing.Font("宋体", this.HeightUnit * this.ParentGrid.SizePerUnit * 0.37f, System.Drawing.FontStyle.Regular), Brushes.Black, new PointF(this.ParentGrid.LeftOffset - 3, this.ParentGrid.TopOffset + this.ParentGrid.SizePerUnit * 0.3f - this.ParentGrid.SizePerUnit));

                string strTitle2 = string.Format("姓名：{0} 年龄：{1} 性别：{2} 入院日期：{3} 病区：{4}床号：{5} 住院号：{6}",
                                                 strPatNameArr[1], strPatAgeArr[1], strPatSexArr[1], strInDateArr[1], strDeptNameArr[1], strBedNoArr[1], strIpNoArr[1]);
                this.ParentGrid.graphics.DrawString(strTitle2, new System.Drawing.Font("宋体", this.HeightUnit * this.ParentGrid.SizePerUnit * 0.37f, System.Drawing.FontStyle.Regular), Brushes.Black, new PointF(this.ParentGrid.LeftOffset - 3, this.ParentGrid.TopOffset + this.ParentGrid.SizePerUnit * 0.3f));

                string strTitle3 = string.Format("      {0}       {1}       {2}           {3}  {4}       {5}         {6}",
                                 strPatNameArr[2], strPatAgeArr[2], strPatSexArr[2], strInDateArr[2], strDeptNameArr[2], strBedNoArr[2], strIpNoArr[2]);
                this.ParentGrid.graphics.DrawString(strTitle3, new System.Drawing.Font("宋体", this.HeightUnit * this.ParentGrid.SizePerUnit * 0.37f, System.Drawing.FontStyle.Regular), Brushes.Black, new PointF(this.ParentGrid.LeftOffset - 3, this.ParentGrid.TopOffset + this.ParentGrid.SizePerUnit * 0.3f + this.ParentGrid.SizePerUnit));
            }
            else
            {
                text = "姓名：        年龄：     性别：     入院日期：          病区：            床号：     住院号：      ";

                this.ParentGrid.graphics.DrawString(text, new System.Drawing.Font("宋体", this.HeightUnit * this.ParentGrid.SizePerUnit * 0.37f, System.Drawing.FontStyle.Regular), Brushes.Black, new PointF(this.ParentGrid.LeftOffset, this.ParentGrid.TopOffset + this.ParentGrid.SizePerUnit * 0.3f));
            }

        }

        public float HeightUnit { get; private set; }

    }
}
