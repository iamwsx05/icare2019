using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using com.digitalwave.iCare.gui.HIS;
using System.Windows.Forms;
using System.Linq;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsLisReportPrint 的摘要说明。
    /// </summary>
    public class clsUnifyReportPrint : infPrintRecord
    {
        #region 报告单参数
        private float m_fltPaperWidth;        //打印纸张的宽度
        private float m_fltPaperHeight;       //打印纸张的高度
        private float m_fltPrintWidth;        //打印区域的宽度
        private float m_fltPrintHeight;       //打印区域的高度
        private float m_fltStartX;            //报告单X起始位置
        private float m_fltEndY;              //报告单底部信息位置
        private float m_fltTitleSpace;        //报告单组打印标题间隔
        private float m_fltItemSpace;         //报告单组打印项目间隔
        private float m_fltImgSpace;          //图形打印间隔

        //图形缩放比例
        private float m_fltXRate = 0.6f;
        private float m_fltYRate = 0.45f;

        private string m_strPatientName = "姓名:";
        private string m_strSex = "性别:";
        private string m_strAge = "年龄:";
        private string m_cardType = "证件类型:";
        private string m_cardNo = "证件号码:";
        private bool isCov2019 = false;
        /// <summary>
        /// 当PatientType = 1时，== "住院号:"
        ///   PatientType = 2时，== "门诊号:"
        ///   PatientType = 3时，== "体检号:"
        /// </summary>
        private string m_strInPatientNo = "住院号:";
        private string m_strDepartment = "科室:";
        private string m_strBedNo = "床号:";
        private string m_strSampleType = "样本类型:";
        private string m_strApplyDoc = "送检医生:";
        private string m_strDiagnose = "临床诊断:";
        private string m_strSampleID = "样本号:";
        private string m_strCheckNo = "检验编号:";
        private string m_strCheckDate = "送检日期:";
        private string m_strSummary = "实验室提示:";
        private string m_strNotice = "祝您身体健康!此报告仅对检测标本负责,结果供医生参考!";
        private string m_strAnnotation = "附注:";
        private string m_strReportDate = "报告日期:";
        private string m_strCheckDoc = "检验者:";  //"报告医生:";
        private string m_strConfirmEmp = "审核者:";    //"审核医生:";
        private string m_strResult = "结    果";
        private string m_strReference = "参考区间";
        private string m_strResultUnit = "单位";

        //字体设置
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;
        private Font m_fntSmall2Bold;
        private Font m_fntsamll3NotBold;

        //报告单数据
        public DataTable m_dtbSample;
        public DataTable m_dtbResult;

        //打印方法工具
        clsCommonPrintMethod m_printMethodTool;

        //Y位置定位
        private float m_fltY;

        private bool m_blnDocked = true; //打印底部信息固定否

        private bool m_blnPrintPIc; //是否打印图形

        //打印信息分页
        clsPrintPerPageInfo[] m_objPrintPage;

        //指示当前打印页码
        private int m_intCurrentPageIdx = 0;
        private int m_intTotalPage = 0;

        //实验室提示和附注为空时是否显示
        bool m_blnSummaryEmptyVisible = false;
        bool m_blnAnnotationEmptyVisible = false;



        /// <summary>
        /// 申请单样式 0 项目加多图片缩小样式 1 字体加大图片缩小样式
        /// </summary>
        private int BillStyle = 0;

        /// <summary>
        /// 是否打印诊断
        /// </summary>
        public static bool blnSurePrintDiagnose = false;

        #endregion

        #region 底部打印信息固定属性
        public bool IsDocked
        {
            get
            {
                return m_blnDocked;
            }
            set
            {
                m_blnDocked = value;
            }
        }
        #endregion

        #region 构造函数

        List<string> lstAppUnitID { get; set; }

        List<EntityAidRemark> lstAidRemark { get; set; }
        /// <summary>
        /// 新冠报告格式参数
        /// </summary>
        List<string> lstCov2019 { get; set; }
        /// <summary>
        /// Mejer 尿沉渣带图片报告格式
        /// </summary>
        string mejerParm { get; set; }

        private Image objImage;  //医院图标

        public clsUnifyReportPrint()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //	
            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;
            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}

            string m_strStartUp = System.Windows.Forms.Application.StartupPath + "\\Picture\\茶山log.bmp"; //获取打印图标

            objImage = Image.FromFile(m_strStartUp, false);

            try
            {
                string strPath = Application.StartupPath + @"\LIS_GUI.dll.config";
                System.Configuration.ConfigXmlDocument appConfig = new System.Configuration.ConfigXmlDocument();
                appConfig.Load(strPath);
                string strIsBlood = appConfig["configuration"]["appSettings"].SelectSingleNode("add[@key=\"IsPrintPic\"]").Attributes["value"].Value.ToString();
                if (strIsBlood == "1")
                {
                    m_blnPrintPIc = true;
                }
                else
                {
                    m_blnPrintPIc = false;
                }
            }
            catch (Exception objEx)
            {

            }
        }

        #endregion

        #region 过敏源分级备注信息

        EntityAppUnit CurrAppUnit { get; set; }

        /// <summary>
        /// 过敏源分级备注信息
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        string GetAllergenRemarkInfo(string appId, string contrastStr, string contrastSex)
        {
            try
            {
                //if (this.lstAppUnitID == null || this.lstAppUnitID.Count == 0)
                //{
                //    this.CurrAppUnit = null;
                //    return "";
                //}
                if (this.lstAidRemark == null || this.lstAidRemark.Count == 0)
                {
                    this.CurrAppUnit = null;
                    return "";
                }

                if (this.CurrAppUnit != null && this.CurrAppUnit.appId == appId)
                {
                    return this.CurrAppUnit.remarkInfo;
                }

                this.CurrAppUnit = null;
                List<string> lstTempId = (new weCare.Proxy.ProxyLis()).Service.GetAppUnitIdByAppId(appId);

                if (lstTempId != null && lstTempId.Count > 0)
                {
                    string remarkInfoStr = string.Empty;
                    foreach (string id in lstTempId)
                    {
                        // 2020-07-15
                        if (lstAidRemark.Any(p => p.appUnitId.IndexOf(id) >= 0))
                        {
                            List<EntityAidRemark> lstAidRemarkVO = lstAidRemark.FindAll(p => p.appUnitId.IndexOf(id) >= 0);

                            foreach (EntityAidRemark aidRemarkVO in lstAidRemarkVO)
                            {
                                // 校验
                                // 1. 是否已人工添加
                                if (!string.IsNullOrEmpty(aidRemarkVO.keyWord))
                                {
                                    if (contrastStr.IndexOf(aidRemarkVO.keyWord) >= 0) continue;    // 已存在
                                }

                                // 2. 男/女                            
                                if (aidRemarkVO.sex == 1)  // 限男
                                {
                                    if (contrastSex == "女") continue;
                                }
                                else if (aidRemarkVO.sex == 2)  // 限女
                                {
                                    if (contrastSex == "男") continue;
                                }
                                // 3. 偏高(1) / 偏低(2)
                                if (aidRemarkVO.highOrLow == 1 || aidRemarkVO.highOrLow == 2 || aidRemarkVO.highOrLow == 3)
                                {
                                    bool isPass = false;
                                    //List<clsDeviceReslutVO> lstResult = (new weCare.Proxy.ProxyLis02()).Service.GetOttomanCheckResult(barCode);
                                    List<clsDeviceReslutVO> lstResult = null;
                                    if (m_dtbResult != null)
                                    {
                                        clsDeviceReslutVO vo = null;
                                        lstResult = new List<clsDeviceReslutVO>();
                                        foreach (DataRow dr in m_dtbResult.Rows)
                                        {
                                            vo = new clsDeviceReslutVO();
                                            vo.m_strAbnormalFlag = dr["abnormal_flag_chr"].ToString();
                                            vo.m_strDeviceCheckItemName = dr["device_check_item_name_vchr"].ToString();
                                            vo.m_strResult = dr["result_vchr"].ToString();
                                            vo.m_strDeviceSampleID = dr["check_item_id_chr"].ToString();
                                            lstResult.Add(vo);

                                        }
                                    }
                                    if (lstResult != null)
                                    {
                                        foreach (clsDeviceReslutVO item in lstResult)
                                        {

                                            if (aidRemarkVO.highOrLow == 1)
                                            {
                                                if (!string.IsNullOrEmpty(aidRemarkVO.checkItemId))
                                                {
                                                    clsDeviceReslutVO vo = lstResult.Find(r => r.m_strDeviceSampleID == aidRemarkVO.checkItemId);
                                                    if (vo != null)
                                                    {
                                                        if (vo.m_strAbnormalFlag == "H")
                                                        {
                                                            isPass = true;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }

                                                if (item.m_strAbnormalFlag == "H")
                                                {
                                                    isPass = true;
                                                    break;
                                                }
                                            }
                                            else if (aidRemarkVO.highOrLow == 2)
                                            {
                                                if (!string.IsNullOrEmpty(aidRemarkVO.checkItemId))
                                                {
                                                    clsDeviceReslutVO vo = lstResult.Find(r => r.m_strDeviceSampleID == aidRemarkVO.checkItemId);
                                                    if (vo != null)
                                                    {
                                                        if (vo.m_strAbnormalFlag == "L")
                                                        {
                                                            isPass = true;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }

                                                if (item.m_strAbnormalFlag == "L")
                                                {
                                                    isPass = true;
                                                    break;
                                                }
                                            }
                                            else if (aidRemarkVO.highOrLow == 3)
                                            {
                                                if (!string.IsNullOrEmpty(aidRemarkVO.checkItemId))
                                                {
                                                    clsDeviceReslutVO vo = lstResult.Find(r => r.m_strDeviceSampleID == aidRemarkVO.checkItemId);
                                                    if (vo != null)
                                                    {
                                                        if (vo.m_strResult.Contains("阳"))
                                                        {
                                                            isPass = true;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }

                                                if (item.m_strResult.Contains("阳"))
                                                {
                                                    isPass = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (isPass == false) continue;

                                }
                                if (!remarkInfoStr.Contains("项目") && aidRemarkVO.appunitgroup == 1)
                                {
                                    remarkInfoStr += "项目    卵泡期      排卵期     黄体期     绝经期      妊娠期    未妊娠    单位" + Environment.NewLine;
                                }

                                remarkInfoStr += aidRemarkVO.remarkInfo + Environment.NewLine;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(remarkInfoStr))
                    {
                        this.CurrAppUnit = new EntityAppUnit()
                        {
                            appId = appId,
                            remarkInfo = remarkInfoStr
                        };

                        return this.CurrAppUnit.remarkInfo;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region Mejer 尿沉渣报告图片
        /// <summary>
        /// Mejer 尿沉渣报告图片
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        Image GetMejerImage(string appId)
        {
            Image img = null;
            string Sql = @"SELECT DISTINCT  t2.deviceid_chr,t2.device_sampleid_chr,t2.check_dat
                FROM t_opr_lis_app_sample t1
                 left join t_opr_lis_device_relation t2
                 on t1.sample_id_chr = t2.sample_id_chr
                WHERE t2.status_int > 0
                AND t1.application_id_chr = '{0}' and t2.deviceid_chr= '{1}'";
            Sql = string.Format(Sql, appId, mejerParm);
            DataTable dt = null;
            (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                string deviceId = dt.Rows[0]["deviceid_chr"].ToString();
                string devSampleId = dt.Rows[0]["device_sampleid_chr"].ToString().Trim();
                string checkDate = Function.Datetime(dt.Rows[0]["check_dat"]).ToString("yyyy-MM-dd HH:mm:ss");

                if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(devSampleId) && !string.IsNullOrEmpty(checkDate))
                {
                    Sql = @"select a.sampleimg from t_checkresult_img a 
                                    where a.deviceid = '{0}' 
                                            and a.sampleid = '{1}' 
                                            and a.checkdate = to_date('{2}','yyyy-mm-dd hh24:mi:ss') ";

                    Sql = string.Format(Sql, deviceId, devSampleId, checkDate);

                    (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        byte[] bytGraph = (byte[])dt.Rows[0]["sampleimg"];
                        img = Function.ConvertByteToImage(bytGraph);
                    }
                }
            }
            return img;
        }
        #endregion

        #region 打印接口初始化方法
        private void m_mthInitalPrintTool(PrintDocument p_printDoc)
        {
            //获取纸张的宽和高
            m_fltPaperWidth = p_printDoc.DefaultPageSettings.Bounds.Width;
            m_fltPaperHeight = p_printDoc.DefaultPageSettings.Bounds.Height;

            //设置打印区域的宽和高
            m_fltPrintWidth = m_fltPaperWidth * 0.9f;
            m_fltPrintHeight = m_fltPaperHeight * 0.9f;
            m_fltStartX = m_fltPaperWidth * 0.05f;
            m_fltEndY = m_fltPaperHeight - 95;    //baojian.mo -2007.9.3 modify

            //设置报告单组打印间隔
            m_fltTitleSpace = 5;
            m_fltItemSpace = 2;
            m_fltImgSpace = 10;

            //设置打印字体
            m_fntTitle = new Font("SimSun", 16, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmall2Bold = new Font("SimSun", 10, FontStyle.Bold);

            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
            m_fntsamll3NotBold = new Font("SimSun", 8f, FontStyle.Regular);


            //get parm value  mobaojian.mo  -2007.09.03 Modify            
            BillStyle = clsPublic.m_intGetSysParm("4010");
        }
        #endregion

        #region 构造图形
        private Image m_imgDrawGraphic(byte[] p_bytGraph, string p_strImageFormat)
        {
            Image img = null;
            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(p_bytGraph);
                img = Image.FromStream(ms, true);
                string strFormat = (p_strImageFormat == null) ? null : p_strImageFormat.ToLower();
                switch (strFormat)
                {
                    case "lisb":
                        System.Drawing.Bitmap bm = new Bitmap(img.Width, img.Height);
                        Graphics g = Graphics.FromImage(bm);
                        g.DrawImage(img, 0, 0, bm.Width, bm.Height);
                        img.Dispose();
                        img = bm;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return img;
        }
        #endregion

        #region 打印报告单基本信息
        private void m_mthPrintBseInfo()
        {
            if (m_dtbSample == null)
                return;


            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 0.25f;
            float fltColumn3 = m_fltPaperWidth * 0.40f;
            float fltColumn4 = m_fltPaperWidth * 0.62f;

            bool isUseA4 = (clsPublic.ConvertObjToDecimal(clsPublic.m_strReadXML("Lis", "IsUseA4", "AnyOne")) == 1 ? true : false);
            if (isUseA4)
            {
                m_fltY = 30;
            }
            else
            {
                m_fltY = 5;
            }

            //图标
            m_printMethodTool.m_mthPrintImage(objImage, fltColumn1, m_fltY);

            //string m_strTitleImg = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Remove
            //    (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);

            string m_strTitleImg = "东 莞 市 茶 山 医 院";
            string m_strTitleImgEng = "ChaShan Hospital of DongGuang";

            //医院名称
            // m_printMethodTool.m_mthDrawString(m_strTitleImg, m_fntSmallBold, fltColumn1 + objImage.Width, m_fltY + 16);

            //英文
            // m_printMethodTool.m_mthDrawString(m_strTitleImgEng, m_fntsamll3NotBold, fltColumn1 + objImage.Width, m_fltY + 30);

            m_fltY += objImage.Height - 40;

            string m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Substring
                (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);

            if (!m_strTitle.Contains("检验报告单"))
            {
                m_strTitle = "检验报告单";
            }

            //DrawTitle
            m_printMethodTool.m_mthPrintTitle(m_strTitle, m_fntTitle, m_fltY, m_fltPaperWidth);

            //Locate Y
            m_fltY += 3 + m_printMethodTool.m_fltGetStringHeight(m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim(), m_fntTitle);
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);
            if (isUseA4)
            {
                //Locate Y
                m_fltY += 12;
            }
            else
            {
                //Locate Y
                m_fltY += 3;
            }

            //姓名
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntHeadNotBold, m_strPatientName,
                m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //性别
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strSex, m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //年龄
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strAge, m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), fltColumn3, m_fltY);

            //住院号、门诊卡号、体检号
            string strPatientType = m_dtbSample.Rows[0]["patient_type_chr"].ToString().Trim();
            string strPrintContent = null;
            switch (strPatientType)
            {
                case "2":
                    m_strInPatientNo = "诊疗卡号:";
                    strPrintContent = m_dtbSample.Rows[0]["patientcardid_chr"].ToString().Trim();
                    break;

                case "3":
                    m_strInPatientNo = "体检号:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;

                default:
                    m_strInPatientNo = "住院号:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;
            }


            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strInPatientNo,
                strPrintContent, fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            #region 新冠基本信息
            List<string> lstTempId = (new weCare.Proxy.ProxyLis()).Service.GetAppUnitIdByAppId(m_dtbSample.Rows[0]["application_id_chr"].ToString().Trim());

            if (lstTempId != null && lstTempId.Count > 0)
            {
                foreach (string id in lstTempId)
                {
                    if (lstCov2019.IndexOf(id) >= 0)
                    {
                        isCov2019 = true;

                        string Sql = @"select idcard_chr from t_bse_patient a where a.patientid_chr = '" + m_dtbSample.Rows[0]["patientid_chr"].ToString().Trim() + "'";
                        DataTable dt = null;
                        string cardNo = string.Empty;
                        (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            cardNo = dt.Rows[0]["idcard_chr"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(cardNo))
                        {
                            //证件类型
                            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                               m_cardType, "身份证", fltColumn1, m_fltY);
                        }
                        else
                        {
                            //证件类型
                            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                               m_cardType, "".Trim(), fltColumn1, m_fltY);
                        }

                        //证件号码
                        m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                            m_cardNo, cardNo, fltColumn2, m_fltY);

                        //Locate Y
                        m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);
                        break;
                    }

                }
            }
            #endregion

            //科  室
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDepartment,
                m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //床  号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strBedNo,
                m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //样本类型
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleType,
                m_dtbSample.Rows[0]["sample_type_desc_vchr"].ToString().Trim(), fltColumn3, m_fltY);

            //检验编号
            string temp_No = m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim();
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckNo,
                m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), fltColumn4, m_fltY);
            try
            {
                if (temp_No.Substring(0, 2) == "18")
                {
                    m_strReference = "MIC";
                }
                else
                {
                    m_strReference = "参考区间";
                }
            }
            catch
            {

            }

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);


            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 5;
        }

        /// <summary>
        /// 读程序配置
        /// baojian.mo add in 2008-03-01
        /// </summary>
        /// <param name="strCfgName"></param>
        /// <returns></returns>
        public static int intGetConfig(string strCfgName)
        {
            try
            {
                string strFlag = System.Configuration.ConfigurationManager.AppSettings[strCfgName];
                int intFlag = int.Parse(strFlag);
                return intFlag;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 打印报告单实验室提示
        private float m_fltPrintSummary(float p_fltX, float p_fltY, float p_fltPrintWidth)
        {
            string summaryInfo = "";
            if (m_dtbSample.Rows[0]["SUMMARY_VCHR"] != DBNull.Value)
            {
                summaryInfo = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            }
            string remarkInfo = GetAllergenRemarkInfo(m_dtbSample.Rows[0]["application_id_chr"].ToString(), m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(), m_dtbSample.Rows[0]["sex_chr"].ToString().Trim());
            if (!string.IsNullOrEmpty(remarkInfo) && remarkInfo.Trim() != "")
            {
                summaryInfo += "\r\n" + remarkInfo;
            }

            if (!m_blnSummaryEmptyVisible && summaryInfo == "")
                return p_fltY;
            float fltY = p_fltY + 10;

            if (lstCov2019 != null && lstCov2019.Count > 0)
            {
                // 核酸检测不打实验室提示
            }
            else
            {
                m_printMethodTool.m_mthDrawString(m_strSummary, m_fntSmallBold, p_fltX, fltY);
            }
            fltY += m_fntSmallBold.Height + m_fltTitleSpace;
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, summaryInfo, p_fltPrintWidth, m_fltTitleSpace, m_fltItemSpace);
            Rectangle rectSummary = new Rectangle((int)p_fltX, (int)fltY, (int)sf.Width, (int)sf.Height);
            new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(summaryInfo,
                m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, m_printMethodTool.m_printEventArg.Graphics);
            fltY += rectSummary.Height;
            return fltY;
        }
        #endregion

        #region ReadImageFile
        /// <summary>
        /// 读取图片文件
        /// </summary>
        /// <param name="path">图片文件路径</param>
        /// <returns>图片文件</returns>
        Bitmap ReadImageFile(string path)
        {
            Bitmap bitmap = null;
            try
            {
                FileStream fileStream = File.OpenRead(path);
                Int32 filelength = 0;
                filelength = (int)fileStream.Length;
                Byte[] image = new Byte[filelength];
                fileStream.Read(image, 0, filelength);
                System.Drawing.Image result = System.Drawing.Image.FromStream(fileStream);
                fileStream.Close();
                bitmap = new Bitmap(result);
            }
            catch (Exception ex)
            {
                //  异常输出
            }
            return bitmap;
        }
        #endregion

        #region PrintHsjcGZ
        /// <summary>
        /// PrintHsjcGZ 打印核酸检测公章
        /// </summary>
        void PrintHsjcGZ(int x, int y)
        {
            string filePath = Application.StartupPath + "\\csyyylzyz.jpg";
            if (File.Exists(filePath))
            {
                Image imgGZ = this.ReadImageFile(filePath);
                if (imgGZ != null)
                {
                    m_printMethodTool.DrawImageXYWH(imgGZ, x, y, 110, 110);
                }
            }
        }
        #endregion

        #region 打印报告单底部信息
        private void m_mthPrintEnd()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            float m_fltEnd = 0.0f;
            m_fltEnd = m_fltY;
            m_fltEnd += 10;

            // 是否打印采样时间
            bool isPrintCYSJ = false;
            // 是否使用A4
            bool isUseA4 = (clsPublic.ConvertObjToDecimal(clsPublic.m_strReadXML("Lis", "IsUseA4", "AnyOne")) == 1 ? true : false);
            // 采样时间
            float diff = 0;
            string str = string.Empty;

            if (isUseA4) m_fltEnd -= 30;//50;

            if (m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"] != DBNull.Value)
            {
                isPrintCYSJ = true;
                str = m_strReportDate;      // "采样时间:";
                m_printMethodTool.m_mthDrawString(str, m_fntSmallBold, m_fltStartX, m_fltEnd);
                diff = m_printMethodTool.m_fltGetStringWidth(str, m_fntSmallBold);
                str = Convert.ToDateTime(m_dtbSample.Rows[0]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm");  // Convert.ToDateTime(m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm");
                m_printMethodTool.m_mthDrawString(str, m_fntSmallBold, m_fltStartX + diff + 5, m_fltEnd);
                diff += m_printMethodTool.m_fltGetStringWidth(str, m_fntSmallBold) + 65;
            }
            //Notice
            m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX + diff, m_fltEnd);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, m_fntSmallNotBold);
            //附注
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltEnd);
            }
            if (isUseA4)
            {
                m_fltEnd += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold);
                //画线
                m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltEnd, m_fltPaperWidth * 0.9f, m_fltEnd);

                m_fltEnd += 15;
            }
            else
            {
                m_fltEnd += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold) + 3;
                //画线
                m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltEnd, m_fltPaperWidth * 0.9f, m_fltEnd);

                m_fltEnd += 6;
            }

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            if (isPrintCYSJ)
                //采样时间
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, "采样时间:", Convert.ToDateTime(m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm"),
                    fltColumn1, m_fltEnd);
            else
                //报告日期
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, Convert.ToDateTime(m_dtbSample.Rows[0]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm"),
                    fltColumn1, m_fltEnd);
            //检验医生
            if (m_dtbSample.Columns.IndexOf("reportorSign") >= 0)
            {
                if (m_dtbSample.Rows[0]["reportorSign"] == DBNull.Value)
                {
                    m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2, m_fltEnd);
                }
                else
                {
                    MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["reportorSign"]);
                    m_printMethodTool.DrawImage(m_strCheckDoc, m_fntSmallBold, Image.FromStream(ms), fltColumn2, m_fltEnd, isUseA4);
                }
            }
            else
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2, m_fltEnd);
            }

            //审核者
            if (m_dtbSample.Columns.IndexOf("confirmerSign") >= 0)
            {
                if (m_dtbSample.Rows[0]["confirmerSign"] == DBNull.Value)
                {
                    m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3, m_fltEnd);
                }
                else
                {
                    MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["confirmerSign"]);
                    m_printMethodTool.DrawImage(m_strConfirmEmp, m_fntSmallBold, Image.FromStream(ms), fltColumn3, m_fltEnd, isUseA4);
                }
            }
            else
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3, m_fltEnd);
            }

        }

        //东莞茶山式页尾
        private void m_mthPrintEnd_DGCS()
        {
            //if (m_blnDocked)
            //{
            //    if (m_fltY < m_fltEndY)
            //    {
            //        m_fltY = m_fltEndY;
            //    }
            //}
            float m_fltEnd = 0.0f;
            m_fltEnd = m_fltEndY;

            m_fltEnd += 3;

            //画线
            //m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltEnd += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            bool isPrintCYSJ = false;
            bool isUseA4 = (clsPublic.ConvertObjToDecimal(clsPublic.m_strReadXML("Lis", "IsUseA4", "AnyOne")) == 1 ? true : false);
            if (isUseA4) m_fltEnd -= 30;    // 50;

            if (m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"] != DBNull.Value)
            {
                isPrintCYSJ = true;
            }

            if (!string.IsNullOrEmpty(mejerParm))
            {
                Image graph = GetMejerImage(m_dtbSample.Rows[0]["application_id_chr"].ToString());
                if (graph != null)
                {
                    float m_fltWidth = 0.9f * graph.Width;
                    float m_fltHeight = 0.9f * graph.Height;
                    m_printMethodTool.m_printEventArg.Graphics.DrawImage(graph, fltColumn3 - 190,
                        m_fltEnd - 180, m_fltWidth, m_fltHeight);
                }
            }

            if (isUseA4)
            {
                //m_fltEnd -= 3;
                //画线
                m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltEnd, m_fltPaperWidth * 0.9f, m_fltEnd);
                m_fltEnd += 12;
            }
            else
            {
                //画线
                m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltEnd, m_fltPaperWidth * 0.9f, m_fltEnd);
                m_fltEnd += 6;
            }
            if (isPrintCYSJ)
                //采样时间
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, "采样时间:", Convert.ToDateTime(m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm"),
                    fltColumn1, m_fltEnd);
            else
                //报告日期
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, Convert.ToDateTime(m_dtbSample.Rows[0]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm"),
                    fltColumn1, m_fltEnd);
            //检验医生
            if (m_dtbSample.Columns.IndexOf("reportorSign") >= 0)
            {
                if (isCov2019)
                {
                    if (m_dtbSample.Rows[0]["reportorSign"] == DBNull.Value)
                    {
                        m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2 - 102, m_fltEnd);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["reportorSign"]);
                        m_printMethodTool.DrawImage(m_strCheckDoc, m_fntSmallBold, Image.FromStream(ms), fltColumn2 - 102, m_fltEnd, isUseA4);
                    }
                }
                else
                {
                    if (m_dtbSample.Rows[0]["reportorSign"] == DBNull.Value)
                    {
                        m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2, m_fltEnd);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["reportorSign"]);
                        m_printMethodTool.DrawImage(m_strCheckDoc, m_fntSmallBold, Image.FromStream(ms), fltColumn2, m_fltEnd, isUseA4);
                    }
                }

            }
            else
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2, m_fltEnd);
            }

            //审核者
            if (m_dtbSample.Columns.IndexOf("confirmerSign") >= 0)
            {
                if (isCov2019)
                {
                    if (m_dtbSample.Rows[0]["confirmerSign"] == DBNull.Value)
                    {
                        m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3 - 140, m_fltEnd);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["confirmerSign"]);
                        m_printMethodTool.DrawImage(m_strConfirmEmp, m_fntSmallBold, Image.FromStream(ms), fltColumn3 - 140, m_fltEnd, isUseA4);
                    }
                }
                else
                {
                    if (m_dtbSample.Rows[0]["confirmerSign"] == DBNull.Value)
                    {
                        m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3, m_fltEnd);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream((byte[])m_dtbSample.Rows[0]["confirmerSign"]);
                        m_printMethodTool.DrawImage(m_strConfirmEmp, m_fntSmallBold, Image.FromStream(ms), fltColumn3, m_fltEnd, isUseA4);
                    }
                }
            }
            else
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3, m_fltEnd);
            }
            if (isCov2019)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, "检测机构：机构名称（盖章）", "", fltColumn3 + 10, m_fltEnd);
                // 增加打印公章
                this.PrintHsjcGZ(Convert.ToInt32(fltColumn3 + 30), Convert.ToInt32(m_fltEnd - 120));
            }

            m_fltEnd += m_printMethodTool.m_fltGetStringHeight(m_strReportDate, m_fntSmallBold) + 6;

            ////画线
            //m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltEnd, m_fltPaperWidth * 0.9f, m_fltEnd);
            //m_fltEnd += 6;

            // 采样时间
            float diff = 0;
            string str = string.Empty;
            if (isPrintCYSJ)
            {
                str = m_strReportDate;  // "采样时间:";
                m_printMethodTool.m_mthDrawString(str, m_fntSmallBold, m_fltStartX, m_fltEnd);
                diff = m_printMethodTool.m_fltGetStringWidth(str, m_fntSmallBold);
                str = Convert.ToDateTime(m_dtbSample.Rows[0]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm");  // Convert.ToDateTime(m_dtbSample.Rows[0]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm");
                m_printMethodTool.m_mthDrawString(str, m_fntSmallBold, m_fltStartX + diff + 5, m_fltEnd);
                diff += m_printMethodTool.m_fltGetStringWidth(str, m_fntSmallBold) + 65;
            }
            if (isCov2019)
            {
                //Notice
                m_printMethodTool.m_printEventArg.Graphics.DrawString(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular), Brushes.Red, fltColumn2 - 102, m_fltEnd);
                //m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            }
            else
            {
                //Notice
                m_printMethodTool.m_printEventArg.Graphics.DrawString(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular), Brushes.Red, m_fltStartX + diff, m_fltEnd);
                //m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            }

            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular));
            //附注
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim(),
                                m_fltStartX + fltNoticeWidth, m_fltEnd);
            }
        }
        #endregion

        #region 打印页信息
        private void m_mthPrintDetail()
        {
            string summaryInfo = "";
            if (m_dtbSample.Rows[0]["SUMMARY_VCHR"] != DBNull.Value)
            {
                summaryInfo = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            }
            string remarkInfo = GetAllergenRemarkInfo(m_dtbSample.Rows[0]["application_id_chr"].ToString(), m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(), m_dtbSample.Rows[0]["sex_chr"].ToString().Trim());
            if (!string.IsNullOrEmpty(remarkInfo) && remarkInfo.Trim() != "")
            {
                summaryInfo += "\r\n" + remarkInfo;
            }
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, summaryInfo, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (m_objPrintPage == null)
            {
                m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
                    , m_fltPaperHeight - m_fltEndY - m_fltY, m_fltPaperHeight - 123 - (m_fltPaperHeight - m_fltEndY));
                m_intTotalPage = m_objPrintPage.Length;
            }
            if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            {
                m_printMethodTool.m_printEventArg.HasMorePages = false;
            }
            else
            {
                m_printMethodTool.m_printEventArg.HasMorePages = true;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            {
                //打印结果数据
                float fltY = m_fltPrintGroupData(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }

            if (m_blnPrintPIc)
            {
                if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
                {
                    //打印图形数据
                    float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
                    if (fltY != -1)
                        m_fltY = fltY;
                }
            }

            if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            {
                m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            }

        }

        //茶山式检验报告样式
        private void m_mthPrintDetail_DGCS()
        {
            string summaryInfo = "";
            if (m_dtbSample.Rows[0]["SUMMARY_VCHR"] != DBNull.Value)
            {
                summaryInfo = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            }
            string remarkInfo = GetAllergenRemarkInfo(m_dtbSample.Rows[0]["application_id_chr"].ToString(), m_dtbSample.Rows[0]["summary_vchr"].ToString().Trim(), m_dtbSample.Rows[0]["sex_chr"].ToString().Trim());
            if (!string.IsNullOrEmpty(remarkInfo) && remarkInfo.Trim() != "")
            {
                summaryInfo += "\r\n" + remarkInfo;
            }

            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, summaryInfo, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (m_objPrintPage == null)
            {
                m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
                    , m_fltEndY - m_fltY, m_fltEndY - m_fltY);
                m_intTotalPage = m_objPrintPage.Length;
            }
            if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            {
                m_printMethodTool.m_printEventArg.HasMorePages = false;
            }
            else
            {
                m_printMethodTool.m_printEventArg.HasMorePages = true;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            {
                //打印结果数据
                float fltY = m_fltPrintGroupData_DGCS(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_blnPrintPIc)
            {
                if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
                {
                    //打印图形数据
                    float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
                    if (fltY != -1)
                        m_fltY = fltY;
                }
            }

            if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            {
                m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            }
        }
        #endregion

        #region 打印检验结果
        //打印检验结果
        private float m_fltPrintGroupData(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.04f, m_fltPrintWidth * 0.25f, m_fltPrintWidth * 0.35f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.04f, m_fltPrintWidth * 0.30f, m_fltPrintWidth * 0.45f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.5f;
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);
            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];
                float fltColumn4 = fltColumn1 + fltColumnArr[2];

                //打印标题
                m_printMethodTool.m_mthDrawString("代号", m_fntSmallBold, fltColumn1, fltBseY);
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn2, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn3, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strReference, m_fntSmallBold, fltColumn4, fltBseY);
                fltBseY += fltTitleHeight;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    if ((p_objArr[i].m_intStartIdx + j) < p_objArr[i].m_dtvResult.Count)
                    {
                        string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                        string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["abnormal_flag_chr"].ToString().Trim();
                        string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["unit_vchr"].ToString().Trim();
                        string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString() + " " + strUnit;
                        string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["rptno_chr"].ToString().Trim();
                        string strEnglisName = p_objArr[i].m_dtvResult[p_objArr[i].m_intPageIdx + j]["check_item_english_name_vchr"].ToString().Trim();

                        //打印英文名称
                        m_printMethodTool.m_mthDrawString(strEnglisName, m_fntSmall2NotBold, fltColumn1, fltBseY);

                        //打印项目
                        m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntSmall2NotBold, fltColumn2, fltBseY);

                        //异常标志
                        if (strAbnormal != null)
                        {
                            System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                            string strPR;

                            strPR = strResult + " " + "↑";
                            float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                            if (strAbnormal == "H")
                            {
                                strPR = strResult + " " + "↑";
                                float fltStartPos = fltColumn3 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltColumn3, fltBseY);
                            }
                            else if (strAbnormal == "L")
                            {
                                // 20160913
                                //if (strResult.Contains(">") || strResult.Contains("<"))
                                //    strPR = strResult + " " + "↑";
                                //else
                                strPR = strResult + " " + "↓";
                                float fltStartPos = fltColumn3 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltColumn3, fltBseY);
                            }
                            else
                            {
                                strPR = strResult + " " + " ";
                                float fltStartPos = fltColumn3 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, m_fntSmall2NotBold, fltColumn3, fltBseY);
                            }
                        }
                        m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn4, fltBseY);

                        //Locate Y 
                        fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                        if (fltY < fltBseY)
                        {
                            fltY = fltBseY;
                        }
                    }
                }
            }
            return fltY;
        }

        #region 茶山式报告
        /// <summary>
        /// 茶山式报告(字体放大，图片缩小)  
        /// 
        /// 2008-06-30
        /// 新修改 参考值显示的单位分离出来,把单位独立在参考值后面加多一列
        /// </summary>
        /// <param name="p_objArr"></param>
        /// <returns></returns>
        private float m_fltPrintGroupData_DGCS(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.22f, m_fltPrintWidth * 0.30f, m_fltPrintWidth * 0.375f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.30f, m_fltPrintWidth * 0.50f, m_fltPrintWidth * 0.62f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.90f;
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);

            float fltItemNameWidth;    //记录检验项目名称的宽度
            Font m_fntItemName = new Font("SimSun", 11f, FontStyle.Regular);
            Font m_fntResultNotBold = new Font("SimSun", 11f, FontStyle.Regular);

            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];
                float fltColumn4 = fltColumn1 + fltColumnArr[2];


                //打印标题
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn1, fltBseY);
                if (blnHasTwoPart)
                {
                    m_strResult = "结果";
                    m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2 + 6, fltBseY);
                }
                else
                {
                    m_strResult = "结     果";
                    m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2 + 60, fltBseY);
                }
                m_printMethodTool.m_mthDrawString(m_strResultUnit, m_fntSmallBold, fltColumn3, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strReference, m_fntSmallBold, fltColumn4, fltBseY);

                fltBseY += fltTitleHeight;
                float fltStartPosTemp;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    if ((p_objArr[i].m_intStartIdx + j) < p_objArr[i].m_dtvResult.Count)
                    {
                        string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                        string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["abnormal_flag_chr"].ToString().Trim();
                        string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["unit_vchr"].ToString().Trim();
                        string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString().Trim();
                        string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["rptno_chr"].ToString().Trim();

                        // 打印项目 如果项目超出打印范围：1、截断；2、缩小字体。这里采用第二种
                        //for (int i2 = strCheckItemName.Length; i2 >= 0; i2--)
                        //{
                        //    strCheckItemName = strCheckItemName.Substring(0, i2);
                        //    fltItemNameWidth = m_printMethodTool.m_fltGetStringWidth(strCheckItemName, m_fntItemName);
                        //    if (fltItemNameWidth + m_fltTitleSpace > fltColumnArr[0])
                        //    {
                        //        continue;
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                        //m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntItemName, fltColumn1, fltBseY);

                        int ifntSize = Convert.ToInt32(m_fntItemName.Size);
                        Font m_fntItemNameTemp = m_fntItemName;
                        for (int iSize = ifntSize; iSize > 0; iSize--)
                        {
                            m_fntItemNameTemp = new Font(m_fntItemName.Name, iSize, FontStyle.Regular);
                            fltItemNameWidth = m_printMethodTool.m_fltGetStringWidth(strCheckItemName, m_fntItemNameTemp);
                            if (fltItemNameWidth + m_fltTitleSpace > fltColumnArr[0])
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntItemNameTemp, fltColumn1, fltBseY);


                        //异常标志
                        if (strAbnormal != null)
                        {
                            System.Drawing.Font objBoldFont = new Font("SimSun", 11f, FontStyle.Bold);
                            string strPR;

                            strPR = strResult + " " + "↑";
                            float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                            #region 检验结果的x坐标值   -baojian.mo 2007.09.04 Modify

                            if (fltResultPrintWidth - fltResultWidth > 0)
                            {
                                fltStartPosTemp = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            }
                            else   //实际结果宽度比预设宽度大的情况
                            {
                                fltStartPosTemp = fltColumn2;
                            }
                            #endregion

                            if (strAbnormal == "H")
                            {
                                strPR = strResult + " " + "↑";
                                float fltStartPos = fltStartPosTemp;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else if (strAbnormal == "L")
                            {
                                if (strResult.Contains(">") || strResult.Contains("<"))
                                    strPR = strResult + " " + "↑";
                                else
                                    strPR = strResult + " " + "↓";
                                float fltStartPos = fltStartPosTemp;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else
                            {
                                strPR = strResult + " " + " ";
                                float fltStartPos = fltStartPosTemp;
                                m_printMethodTool.m_mthDrawString(strPR, m_fntResultNotBold, fltStartPos, fltBseY);
                            }
                        }
                        if (!string.IsNullOrEmpty(strUnit))
                            m_printMethodTool.m_mthDrawString(strUnit, m_fntSmall2NotBold, fltColumn3, fltBseY);
                        if (!string.IsNullOrEmpty(strRefRange))
                            m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn4, fltBseY);


                        //Locate Y 
                        fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                        if (fltY < fltBseY)
                        {
                            fltY = fltBseY;
                        }
                    }
                }
            }
            return fltY;
        }
        #endregion
        #endregion

        #region 打印图形
        private float m_fltPrintImageArr(clsPrintImage[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            for (int i = 0; i < p_objArr.Length; i++)
            {
                m_printMethodTool.m_printEventArg.Graphics.DrawImage(p_objArr[i].m_img, p_objArr[i].m_fltX,
                    p_objArr[i].m_fltY, p_objArr[i].m_fltWidth, p_objArr[i].m_fltHeight);
                if (fltY < p_objArr[i].m_fltY + p_objArr[i].m_fltHeight)
                {
                    fltY = p_objArr[i].m_fltY + p_objArr[i].m_fltHeight;
                }
            }
            return fltY;
        }
        #endregion

        #region 测量字符串块大小
        //测量字符串块大小
        private SizeF m_rectGetPrintStringRectangle(Font p_fntTitle, Font p_fntContent, string p_strContent, float p_fltWidth, float p_fltTitleSpace,
            float p_fltItemSpace)
        {
            if ((p_strContent == "" || p_strContent == null) && !m_blnSummaryEmptyVisible)
            {
                return new SizeF(0, 0);
            }
            float fltTitleHeight = p_fntTitle.Height;
            float fltContentHeight = p_fntContent.Height;
            float fltHeight = 0;
            if (p_strContent != null && p_strContent != "")
            {
                SizeF sfString = m_printMethodTool.m_printEventArg.Graphics.MeasureString(p_strContent, p_fntContent);
                //fltHeight = (sfString.Width / p_fltWidth + 1) * fltContentHeight;
                fltHeight = sfString.Height;
            }
            else
            {
                fltHeight = fltTitleHeight + p_fltTitleSpace + fltContentHeight;
            }
            SizeF sf = new SizeF(p_fltWidth, fltHeight);
            return sf;
        }
        #endregion

        #region 分页函数
        //先决条件：明确打印结果区域大小，已经过滤出结果数据和图形数据，明确标本组的个数以及各个标本组的打印区域大小、顺序
        //输入参数：结果DataTable，打印起始的XY位置(fltX,fltY)，以及打印的宽度和高度(fltWidth,fltHeight)
        //          (DataTable p_dtbResult,float p_fltX,float p_fltY,float p_fltWidth,float p_fltHeight)
        //输出参数：clsPrintPerPageInfo[]
        //流程：
        //0 过滤出结果数据和图形数据，构造各个标本组打印信息（clsSampleResultInfo），以及初始化图形信息（clsPrintImage）
        //1 按顺序打印标本组结果数据
        //1.1 判断样本信息数据能否一页打完
        //1.1.1 Y GOTO 2
        //1.1.2 N 换页 GoTo 1.1
        //2 结果数据打印完成，判断是否有图像数据
        //2.1 Y 判断当前页底是否够位置打印图形
        //2.1.1 Y 判断当前页能否打印完所有的图形
        //2.1.1.1 Y 打印 GoTo 2.2
        //2.1.1.2 N 打印 换页 GoTo 2.1
        //2.1.2 N 判断当前页侧是否够位置打印图形
        //2.1.2.1 Y 打印
        //2.1.2.2 N 换页 GoTo 2.1
        //2.2 N 打印结束，返回参数

        private clsPrintPerPageInfo[] m_objConstructPrintPageInfo(DataTable p_dtbResult, float p_fltX, float p_fltY,
            float p_fltWidth, float p_fltHeight, float p_fltMaxHeight)
        {
            //过滤出结果数据和图形数据
            DataView dtvData = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 0");
            DataView dtvImage = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 1");

            //排序
            dtvData.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";
            dtvImage.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";

            // 
            clsSampleResultInfo[] objDataArr = m_objConstructSampleResultArr(dtvData);

            clsPrintImage[] objImgArr = m_objConstructPrintImage(dtvImage);

            #region xing.chen add 2005.9.22

            float fltImgHeight = 0;
            if (m_blnPrintPIc)
            {
                if (objImgArr != null && objImgArr.Length > 0)
                {
                    fltImgHeight = objImgArr[0].m_fltHeight + 5;      //baojian.mo -2007.9.3 modify
                }
            }
            #endregion

            int intPage = 0;

            //打印与分页
            ArrayList arlPageData = new ArrayList();

            #region 结果数据打印分页
            float fltLeft = 0;
            float fltRight = 0;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltItemSpace);
            //记录分页剩余的记录个数
            int intTotalLeftItemCount = dtvData.Count;
            float fltHeight = 0;
            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= (p_fltHeight - fltImgHeight) * 2)	//xing.chen modify
            {
                fltHeight = p_fltHeight - fltImgHeight;	//xing.chen modify
            }
            else
            {
                fltHeight = p_fltMaxHeight - fltImgHeight;	//xing.chen modify
            }

            ArrayList arlPrintData = new ArrayList();
            //指示当前是否在右边打印
            bool blnPrintRight = false;
            for (int i = 0; i < objDataArr.Length; i++)
            {
                int intDataCount = objDataArr[i].m_dtvResult.Count;
                objDataArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objDataArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                //左边打印
                if (!blnPrintRight && objDataArr[i].m_fltHeight < fltHeight - fltLeft)
                {
                    objDataArr[i].m_fltX = p_fltX;
                    objDataArr[i].m_fltY = fltLeft + p_fltY;
                    objDataArr[i].m_intStartIdx = 0;
                    objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                    objDataArr[i].m_intPageIdx = intPage;
                    fltLeft += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                    arlPrintData.Add(objDataArr[i]);
                    intTotalLeftItemCount -= objDataArr[i].m_intCount;
                }
                else
                {
                    //判断余下的记录能否在另一边打完,并且当前已经打印的记录个数必须大于或等于单列打印个数的1/2
                    if (fltLeft >= fltHeight / 2 && (fltItemHeight * intTotalLeftItemCount + m_fltImgSpace * intTotalLeftItemCount + m_fltTitleSpace * (objDataArr.Length - i - 1) + fltTitleHeight * (objDataArr.Length - i - 1)) < fltHeight)
                    {
                        blnPrintRight = true;
                        objDataArr[i].m_fltX = p_fltX + p_fltWidth / 2;
                        objDataArr[i].m_fltY = fltRight + p_fltY;
                        objDataArr[i].m_intStartIdx = 0;
                        objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                        objDataArr[i].m_intPageIdx = intPage;
                        fltRight += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                        arlPrintData.Add(objDataArr[i]);
                        intTotalLeftItemCount -= objDataArr[i].m_intCount;
                    }
                    else
                    {
                        while (intDataCount > 0)
                        {
                            if (fltTitleHeight + fltItemHeight < fltHeight - fltLeft)
                            {
                                int intPrintItemCount = 1;

                                while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltLeft)
                                {
                                    if (intPrintItemCount >= intDataCount)
                                    {
                                        break;
                                    }
                                    intPrintItemCount++;

                                }
                                clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                obj.m_fltX = p_fltX;
                                obj.m_fltY = fltLeft + p_fltY;
                                obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                obj.m_intCount = intPrintItemCount;
                                obj.m_intPageIdx = intPage;
                                fltLeft += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;

                                arlPrintData.Add(obj);
                                intDataCount -= intPrintItemCount;
                                intTotalLeftItemCount -= intPrintItemCount;
                            }
                            else
                            {
                                //右边打印
                                if (fltTitleHeight + fltItemHeight * intDataCount < fltHeight - fltRight)
                                {
                                    clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                    obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                    obj.m_fltX = p_fltX + p_fltWidth / 2;
                                    obj.m_fltY = fltRight + p_fltY;
                                    obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                    obj.m_intCount = intDataCount;
                                    obj.m_intPageIdx = intPage;
                                    fltRight += intDataCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                    arlPrintData.Add(obj);
                                    intDataCount -= intDataCount;
                                    intTotalLeftItemCount -= intDataCount;
                                }
                                else
                                {
                                    if (fltTitleHeight + fltItemHeight < fltHeight - fltRight)
                                    {
                                        int intPrintItemCount = 1;
                                        while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltRight)
                                        {
                                            intPrintItemCount++;
                                        }
                                        clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                        obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                        obj.m_fltX = p_fltX + p_fltWidth / 2;
                                        obj.m_fltY = fltRight + p_fltY;
                                        obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                        obj.m_intCount = intPrintItemCount;
                                        obj.m_intPageIdx = intPage;
                                        fltRight += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                        arlPrintData.Add(obj);
                                        intDataCount -= intPrintItemCount;
                                        intTotalLeftItemCount -= intPrintItemCount;
                                    }
                                    else
                                    {
                                        fltLeft = 0;
                                        fltRight = 0;
                                        blnPrintRight = false;
                                        intPage++;
                                        arlPageData.Add(arlPrintData);
                                        arlPrintData = new ArrayList();
                                        if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight * 2)
                                        {
                                            fltHeight = p_fltHeight;
                                        }
                                        else
                                        {
                                            fltHeight = p_fltMaxHeight;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (arlPrintData.Count > 0)
            {
                arlPageData.Add(arlPrintData);
            }
            #endregion

            float fltY = Math.Max(fltLeft, fltRight);
            //			fltY += 4*m_fltTitleSpace;
            int intImgStartIdx = intPage;
            ArrayList arlPageImg = null;
            ArrayList arlImg = null;

            if (m_blnPrintPIc)
            {
                #region 图形数据打印分页
                if (objImgArr != null && objImgArr.Length > 0)
                {
                    arlPageImg = new ArrayList();
                    arlImg = new ArrayList();
                    float fltX = 0;
                    for (int i = 0; i < objImgArr.Length; i++)
                    {
                        if (objImgArr[i].m_fltHeight < p_fltMaxHeight && objImgArr[i].m_fltWidth < p_fltWidth)
                        {
                            bool blnDrawed = false;
                            while (!blnDrawed)
                            {
                                if (p_fltMaxHeight - fltY > objImgArr[i].m_fltHeight)
                                {
                                    if (p_fltWidth - fltX > objImgArr[i].m_fltWidth)
                                    {
                                        objImgArr[i].m_fltX = fltX + p_fltX;
                                        //objImgArr[i].m_fltX = (fltX == 0 ? fltX + p_fltX : fltX + p_fltX + m_fltImgSpace);
                                        objImgArr[i].m_fltY = fltY + p_fltY;
                                        objImgArr[i].m_intPageIdx = intPage;
                                        arlImg.Add(objImgArr[i]);
                                        fltX += objImgArr[i].m_fltWidth + m_fltImgSpace + 20;
                                        blnDrawed = true;
                                    }
                                    else
                                    {
                                        if (i > 0)
                                        {
                                            fltY += objImgArr[i].m_fltHeight + m_fltImgSpace;
                                            fltX = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    fltX = 0;
                                    fltY = 0;
                                    if (arlImg.Count > 0)
                                    {
                                        arlPageImg.Add(arlImg);
                                        arlImg = new ArrayList();
                                    }
                                    intPage++;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (arlImg.Count > 0)
                    {
                        arlPageImg.Add(arlImg);
                    }
                }
            }
            #endregion

            //实验室提示
            string summaryInfo = "";
            if (m_dtbSample.Rows[0]["SUMMARY_VCHR"] != DBNull.Value)
            {
                summaryInfo = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            }
            string remarkInfo = GetAllergenRemarkInfo(m_dtbSample.Rows[0]["application_id_chr"].ToString(), m_dtbSample.Rows[0]["summary_vchr"].ToString().Trim(), m_dtbSample.Rows[0]["sex_chr"].ToString().Trim());
            if (!string.IsNullOrEmpty(remarkInfo) && remarkInfo.Trim() != "")
            {
                summaryInfo += "\r\n" + remarkInfo;
            }
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, summaryInfo, m_fltPrintWidth, m_fltTitleSpace, m_fltItemSpace);
            if (sf.Height > 0 && sf.Height > p_fltMaxHeight - fltY)
            {
                intPage++;
            }

            #region 构造页面打印信息
            clsPrintPerPageInfo[] objArr = new clsPrintPerPageInfo[intPage + 1];
            int intStartImgIdx = -1;
            if (arlPageImg != null)
            {
                intStartImgIdx = ((clsPrintImage[])((ArrayList)arlPageImg[0]).ToArray(typeof(clsPrintImage)))[0].m_intPageIdx;
            }
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = new clsPrintPerPageInfo();
                if (i <= arlPageData.Count - 1)
                {
                    objArr[i].m_objSampleArr = (clsSampleResultInfo[])((ArrayList)arlPageData[i]).ToArray(typeof(clsSampleResultInfo));
                }
                if (arlPageImg != null)
                {
                    if (intStartImgIdx <= i && i <= intStartImgIdx + arlPageImg.Count - 1)
                    {
                        objArr[i].m_imgArr = (clsPrintImage[])((ArrayList)arlPageImg[i - intStartImgIdx]).ToArray(typeof(clsPrintImage));
                    }
                }
            }
            #endregion

            return objArr;
        }
        #endregion

        #region FunctionMethod
        /// <summary>
        /// 获取打印组的高度
        /// </summary>
        /// <param name="p_objData"></param>
        /// <param name="p_fntTitle"></param>
        /// <param name="p_fntItem"></param>
        /// <param name="p_fltTitleSpace"></param>
        /// <param name="p_fltItemSpace"></param>
        /// <returns></returns>
        private float m_fltGetPrintGroupHeight(clsSampleResultInfo p_objData, Font p_fntTitle, Font p_fntItem,
            float p_fltTitleSpace, float p_fltItemSpace)
        {
            float fltHeight = 0;
            fltHeight += (p_fntTitle.Height + p_fltTitleSpace) + (p_objData.m_intCount * (p_fntItem.Height + p_fltItemSpace));
            return fltHeight;
        }

        private float m_fltGetPrintElementHeight(Font p_fnt, float p_fltPrintSpace)
        {
            float fltHeight = 0;
            fltHeight += p_fnt.Height + p_fltPrintSpace;
            return fltHeight;
        }

        /// <summary>
        /// 构造打印数据
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsSampleResultInfo[] m_objConstructSampleResultArr(DataView p_dtvData)
        {
            ArrayList arlGroupID = new ArrayList();
            clsSampleResultInfo[] objArr = null;
            for (int i = 0; i < p_dtvData.Count; i++)
            {
                if (i > 0)
                {
                    if (p_dtvData[i]["groupid_chr"].ToString().Trim() != p_dtvData[i - 1]["groupid_chr"].ToString().Trim())
                    {
                        arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                    }
                }
                else
                {
                    arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                }
            }
            if (arlGroupID.Count > 0)
            {
                objArr = new clsSampleResultInfo[arlGroupID.Count];
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtv = new DataView(p_dtvData.Table);
                    dtv.RowFilter = "IS_GRAPH_RESULT_NUM = 0 AND groupid_chr = " + arlGroupID[i].ToString().Trim();
                    objArr[i] = new clsSampleResultInfo(dtv);
                    objArr[i].m_dtvResult.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    //if (dtv[0]["print_title_vchr"].ToString().Trim() != "")
                    //    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    //else if (dtv[0]["groupid_chr"].ToString().Trim() == "000360")
                    //    objArr[i].m_strPrintTitle = "网织红";
                    //else
                    //    objArr[i].m_strPrintTitle = "血常规34项";
                    objArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                    objArr[i].m_intCount = objArr[i].m_dtvResult.Count;
                }
            }

            if (objArr == null)
            {
                return new clsSampleResultInfo[0];
            }
            return objArr;
        }

        /// <summary>
        /// 构造打印图形
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsPrintImage[] m_objConstructPrintImage(DataView p_dtvData)		// xing.chen modify 2005.9.22
        {
            int intCount = p_dtvData.Count;
            clsPrintImage[] objImgArr = null;
            ArrayList arl = new ArrayList();
            for (int i = 0; i < intCount; i++)
            {
                if (p_dtvData[i]["GRAPH_IMG"] is System.DBNull)
                {
                    continue;
                }
                Image img = m_imgDrawGraphic((byte[])p_dtvData[i]["GRAPH_IMG"], p_dtvData[i]["GRAPH_FORMAT_NAME_VCHR"].ToString());
                if (img != null)
                {
                    clsPrintImage objImg = new clsPrintImage(img);
                    objImg.m_fltWidth = m_fltXRate * objImg.m_fltWidth;
                    objImg.m_fltHeight = m_fltYRate * objImg.m_fltHeight;
                    arl.Add(objImg);
                }
            }
            objImgArr = (clsPrintImage[])arl.ToArray(typeof(clsPrintImage));
            return objImgArr;
        }

        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="p_dtbSource"></param>
        /// <param name="p_strFltExp"></param>
        /// <returns></returns>
        private DataView m_dtvFilterRows(DataTable p_dtbSource, string p_strFltExp)
        {
            DataView dtv = new DataView(p_dtbSource);
            dtv.RowFilter = p_strFltExp;
            return dtv;
        }
        #endregion

        #region 打印整张报告单
        private void m_mthPrint()
        {
            string appUnitId = clsPublic.m_strGetSysparm("7011");
            if (!string.IsNullOrEmpty(appUnitId) && appUnitId.Trim() != "")
            {
                lstAppUnitID = new List<string>();
                lstAppUnitID.AddRange(appUnitId.Split(';'));
            }

            string Sql = @"select appunitid, appunitname, sex, highorlow, remarkinfo, keyword,appunitGroup,checkItemId from t_aid_lis_report_remark";
            DataTable dt = null;
            (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                lstAidRemark = new List<EntityAidRemark>();
                foreach (DataRow dr in dt.Rows)
                {
                    lstAidRemark.Add(new EntityAidRemark()
                    {
                        appUnitId = dr["appunitid"].ToString(),
                        appUnitName = dr["appunitname"].ToString(),
                        sex = Function.Int(dr["sex"].ToString()),
                        highOrLow = Function.Int(dr["highorlow"].ToString()),
                        remarkInfo = dr["remarkinfo"].ToString(),
                        keyWord = dr["keyword"].ToString(),
                        appunitgroup = Function.Int(dr["appunitGroup"].ToString()),
                        checkItemId = dr["checkItemId"].ToString()
                    });
                }
            }

            //新冠报告参数
            string appUnitIdCov2019 = clsPublic.m_strGetSysparm("7012");

            if (!string.IsNullOrEmpty(appUnitIdCov2019))
                lstCov2019 = new List<string>(appUnitIdCov2019.Split(';'));
            else
                lstCov2019 = new List<string>();

            mejerParm = clsPublic.m_strGetSysparm("7015");

            m_mthPrintBseInfo();

            #region 自定义报告说明 -加入7006参数 mobaojian 2007.09.04
            switch (clsPublic.m_strGetSysparm("7006"))
            {
                case "":
                    m_strNotice = "祝您身体健康!此报告仅对检测标本负责,结果供医生参考!";
                    break;
                case "0":
                    m_strNotice = "祝您身体健康!此报告仅对检测标本负责,结果供医生参考!";
                    break;
                case "1":
                    m_strNotice = string.Empty;
                    break;
                default:
                    m_strNotice = clsPublic.m_strGetSysparm("7006");
                    break;
            }

            #endregion
            //0 项目加多图片缩小样式 1 字体加大图片缩小样式(茶山样式)
            if (BillStyle == 0)
            {
                m_mthPrintEnd();
                m_mthPrintDetail();

            }
            else
            {
                m_mthPrintEnd_DGCS();
                m_mthPrintDetail_DGCS();

            }
            if (m_intTotalPage - 1 > m_intCurrentPageIdx)
            {
                m_intCurrentPageIdx++;
            }
        }
        #endregion

        #region 继承打印接口

        public void m_mthInitPrintContent()
        {
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_mthInitalPrintTool((PrintDocument)p_objArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_printMethodTool = new clsCommonPrintMethod((PrintPageEventArgs)p_objPrintArg);
            m_mthPrint();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
        }

        #endregion
    }

    class EntityAppUnit
    {
        public string appId { get; set; }

        public string remarkInfo { get; set; }
    }

    class EntityAidRemark
    {
        public string appUnitId { get; set; }
        public string appUnitName { get; set; }
        public int sex { get; set; }
        public int highOrLow { get; set; }
        public string remarkInfo { get; set; }
        public string keyWord { get; set; }
        public int appunitgroup { get; set; }
        public string checkItemId { get; set; }
    }

    #region 封装打印相关的方法
    public class clsCommonPrintMethod
    {
        //PrintPageEventArgs
        public PrintPageEventArgs m_printEventArg;
        SizeF m_sf;
        float m_fltCurrentX;
        float m_fltBseSpace = 5;

        public float PageWidth
        {
            get
            {
                return (float)m_printEventArg.PageSettings.PaperSize.Width;
            }
        }

        public float PageHeight
        {
            get
            {
                return (float)m_printEventArg.PageSettings.PaperSize.Height;
            }
        }


        #region 说明文本与内容之间的基本间隔
        /// <summary>
        /// 说明文本与内容之间的基本间隔
        /// </summary>
        public float m_fltTextBseSpace
        {
            get
            {
                return m_fltBseSpace;
            }
            set
            {
                m_fltBseSpace = value;
            }
        }
        #endregion

        #region 构造函数
        //Constructor
        public clsCommonPrintMethod(PrintPageEventArgs p_objPrintArg)
        {
            m_printEventArg = p_objPrintArg;
        }
        #endregion

        #region 打印说明文本和内容
        /// <summary>
        /// 打印说明文本和内容
        /// </summary>
        /// <param name="p_fntText">文本字体</param>
        /// <param name="p_strText">说明文本</param>
        /// <param name="p_strContent">内容文本</param>
        /// <param name="p_fltX">打印X方向的起始位置</param>
        /// <param name="p_fltY">打印Y方向的起始位置</param>
        public void m_mthDrawTextAndContent(Font p_fntText, Font p_fntContent, string p_strText, string p_strContent, float p_fltX, float p_fltY)
        {
            try
            {
                m_mthDrawString(p_strText, p_fntText, p_fltX, p_fltY);
                m_fltCurrentX = p_fltX + m_fltGetStringWidth(p_strText, p_fntText) + m_fltBseSpace;
                m_mthDrawString(p_strContent, p_fntContent, m_fltCurrentX, p_fltY);
            }
            catch { }
        }
        #endregion

        #region DrawImage
        /// <summary>
        /// DrawImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawImage(string caption, Font fntCaption, Image image, float x, float y, bool isUseA4)
        {
            try
            {
                m_mthDrawString(caption, fntCaption, x, y);
                if (isUseA4)
                {
                    x = x + m_fltGetStringWidth(caption, fntCaption) + m_fltBseSpace + 15;
                    //m_printEventArg.Graphics.DrawImage(image, x, y - 2, 95, 50);
                    //m_printEventArg.Graphics.DrawImage(image, x, y - 5, 60, 25);
                    m_printEventArg.Graphics.DrawImage(image, x, y - 5, 72, 32);
                }
                else
                {
                    x = x + m_fltGetStringWidth(caption, fntCaption) + m_fltBseSpace + 8;
                    //m_printEventArg.Graphics.DrawImage(image, x, y - 2, 95, 50);
                    //m_printEventArg.Graphics.DrawImage(image, x, y - 5, 90, 25);
                    m_printEventArg.Graphics.DrawImage(image, x, y - 5, 60, 25);
                }
            }
            catch
            { }
        }
        #endregion

        #region 获取字符串的宽度
        /// <summary>
        /// 获取字符串的宽度
        /// </summary>
        /// <param name="p_str">字符串</param>
        /// <param name="m_fnt">字体</param>
        /// <returns></returns>
        public float m_fltGetStringWidth(string p_str, Font m_fnt)
        {
            try
            {
                m_sf = m_printEventArg.Graphics.MeasureString(p_str, m_fnt);
            }
            catch
            {
                return 0;
            }
            return m_sf.Width;
        }
        #endregion

        #region 获取字符串的高度
        /// <summary>
        /// 获取字符串的高度
        /// </summary>
        /// <param name="p_str">字符串</param>
        /// <param name="m_fnt">字体</param>
        /// <returns></returns>
        public float m_fltGetStringHeight(string p_str, Font m_fnt)
        {
            try
            {
                m_sf = m_printEventArg.Graphics.MeasureString(p_str, m_fnt);
            }
            catch
            {
                return 0;
            }
            return m_sf.Height;
        }
        #endregion

        #region 打印标题
        /// <summary>
        /// 打印标题
        /// </summary>
        /// <param name="p_str">字符串</param>
        /// <param name="m_fnt">字体</param>
        /// <param name="m_fltY">打印位置高度</param>
        public void m_mthPrintTitle(string p_str, Font m_fnt, float p_fltY, float p_fltWidth)
        {
            try
            {
                m_sf = m_printEventArg.Graphics.MeasureString(p_str, m_fnt);
                m_printEventArg.Graphics.DrawString(p_str, m_fnt, Brushes.Black, (p_fltWidth - m_sf.Width) / 2, p_fltY);
            }
            catch
            {
            }
        }
        #endregion

        #region 打印图标  2011-04-01 李杰文添加
        /// <summary>
        /// 打印图标
        /// </summary>
        /// <param name="p_objImage">图标</param>
        /// <param name="p_fltX">X</param>
        /// <param name="p_fltY">Y</param>
        public void m_mthPrintImage(Image p_objImage, float p_fltX, float p_fltY)
        {
            try
            {
                RectangleF destRect = new RectangleF(p_fltX, p_fltY, 200, 37);
                m_printEventArg.Graphics.DrawImage(p_objImage, destRect);
            }
            catch
            { }
        }
        #endregion

        #region DrawImageXYWH
        /// <summary>
        /// DrawImageXYWH 打印图像方法
        /// </summary>
        /// <param name="img"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawImageXYWH(Image img, float x, float y, float width, float height)
        {
            try
            {
                m_printEventArg.Graphics.DrawImage(img, x, y, width, height);
            }
            catch
            { }
        }
        #endregion

        #region 打印字符串
        /// <summary>
        /// 打印字符串
        /// </summary>
        /// <param name="p_str">打印字符串</param>
        /// <param name="p_fnt">字体</param>
        /// <param name="p_fltX">X</param>
        /// <param name="p_fltY">Y</param>
        public void m_mthDrawString(string p_str, Font p_fnt, float p_fltX, float p_fltY)
        {
            try
            {
                m_printEventArg.Graphics.DrawString(p_str, p_fnt, Brushes.Black, p_fltX, p_fltY);
            }
            catch
            {
            }
        }
        #endregion

        #region 右对齐文本
        public float m_fltFlushRightText(float p_fltStartX, string p_strSourceText, string p_strText, Font p_fnt)
        {
            try
            {
                SizeF sf1 = m_printEventArg.Graphics.MeasureString(p_strSourceText, p_fnt);
                SizeF sf2 = m_printEventArg.Graphics.MeasureString(p_strText, p_fnt);
                p_fltStartX += sf1.Width - sf2.Width;
            }
            catch
            {
                return p_fltStartX;
            }
            return p_fltStartX;
        }
        #endregion

        #region 画线
        public void m_mthDrawLine(float p_fltStartX, float p_fltStartY, float p_fltEndX, float p_fltEndY)
        {
            try
            {
                m_printEventArg.Graphics.DrawLine(Pens.Black, p_fltStartX, p_fltStartY, p_fltEndX, p_fltEndY);
            }
            catch
            {
            }
        }
        #endregion
    }
    #endregion

    #region 页打印信息封装
    /// <summary>
    /// 页打印信息封装
    /// </summary>
    public class clsPrintPerPageInfo
    {
        /// <summary>
        /// 样本结果信息数组
        /// </summary>
        public clsSampleResultInfo[] m_objSampleArr;
        /// <summary>
        /// 图形数据
        /// </summary>
        public clsPrintImage[] m_imgArr;
        /// <summary>
        /// 是否分边打印
        /// </summary>
        public bool m_blnHasTwoPart;
    }

    public class clsSampleResultInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_dtv"></param>
        public clsSampleResultInfo(DataView p_dtv)
        {
            m_dtvResult = p_dtv;
        }

        /// <summary>
        /// 打印结果
        /// </summary>
        public DataView m_dtvResult;
        /// <summary>
        /// 打印标题
        /// </summary>
        public string m_strPrintTitle;
        /// <summary>
        /// 打印左上方X位置
        /// </summary>
        public float m_fltX;
        /// <summary>
        /// 打印左上方Y位置
        /// </summary>
        public float m_fltY;
        /// <summary>
        /// 打印的宽度
        /// </summary>
        public float m_fltWidth;
        /// <summary>
        /// 打印的高度
        /// </summary>
        public float m_fltHeight;
        /// <summary>
        /// 打印起始索引
        /// </summary>
        public int m_intStartIdx;
        /// <summary>
        /// 打印个数
        /// </summary>
        public int m_intCount;
        /// <summary>
        /// 打印的页数
        /// </summary>
        public int m_intPageIdx;
    }

    public class clsPrintImage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_img"></param>
        public clsPrintImage(Image p_img)
        {
            m_img = p_img;
            m_fltWidth = p_img.Width;
            m_fltHeight = p_img.Height;
        }

        /// <summary>
        /// 图形
        /// </summary>
        public Image m_img;
        /// <summary>
        /// 打印左上方X位置
        /// </summary>
        public float m_fltX;
        /// <summary>
        /// 打印左上方Y位置
        /// </summary>
        public float m_fltY;
        /// <summary>
        /// 图形宽度
        /// </summary>
        public float m_fltWidth;
        /// <summary>
        /// 图形高度
        /// </summary>
        public float m_fltHeight;
        /// <summary>
        /// 打印页码
        /// </summary>
        public int m_intPageIdx;
    }
    #endregion

    public class clsUnifyReportPrintForChildHospital : infPrintRecord
    {
        #region 报告单参数
        private float m_fltPaperWidth;        //打印纸张的宽度
        private float m_fltPaperHeight;       //打印纸张的高度
        private float m_fltPrintWidth;        //打印区域的宽度
        private float m_fltPrintHeight;       //打印区域的高度
        private float m_fltStartX;            //报告单X起始位置
        private float m_fltEndY;              //报告单底部信息位置
        private float m_fltTitleSpace;        //报告单组打印标题间隔
        private float m_fltItemSpace;         //报告单组打印项目间隔
        private float m_fltImgSpace;          //图形打印间隔

        //图形缩放比例
        private float m_fltXRate = 0.8f;
        private float m_fltYRate = 0.8f;

        private string m_strPatientName = "姓名:";
        private string m_strSex = "性别:";
        private string m_strAge = "年龄:";

        private string m_strCardType = "证件类型:";
        private string m_strCardNo = "证件号码:";

        private string m_strInPatientNo = "住院号:";
        private string m_strDepartment = "科  室:";
        private string m_strBedNo = "床  号:";
        private string m_strSampleType = "样本类型:";
        private string m_strApplyDoc = "送检医生:";
        private string m_strDiagnose = "临床诊断:";
        private string m_strSampleID = "样本号:";
        private string m_strCheckNo = "检验编号:";
        private string m_strCheckDate = "送检日期:";
        private string m_strSummary = "实验室提示:";
        private string m_strNotice = "(检验结果仅供临床诊疗参考，只对该检测的标本负责!)";
        private string m_strAnnotation = "附注:";
        private string m_strReportDate = "报告日期:";
        private string m_strCheckDoc = "检验医生:";
        private string m_strConfirmEmp = "审核者:";
        private string m_strResult = "结果";
        private string m_strReference = "参考区间";

        //字体设置
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;

        //报告单数据
        public DataTable m_dtbSample;
        public DataTable m_dtbResult;

        //打印方法工具
        clsCommonPrintMethod m_printMethodTool;

        //Y位置定位
        private float m_fltY;

        private bool m_blnDocked = true; //打印底部信息固定否

        //打印信息分页
        clsPrintPerPageInfo[] m_objPrintPage;

        //指示当前打印页码
        private int m_intCurrentPageIdx = 0;
        private int m_intTotalPage = 0;

        //实验室提示和附注为空时是否显示
        bool m_blnSummaryEmptyVisible = false;
        bool m_blnAnnotationEmptyVisible = false;

        /// <summary>
        /// 是否打印诊断
        /// </summary>
        public static bool blnSurePrintDiagnose = false;
        #endregion

        #region 底部打印信息固定属性
        public bool IsDocked
        {
            get
            {
                return m_blnDocked;
            }
            set
            {
                m_blnDocked = value;
            }
        }
        #endregion

        #region 构造函数
        public clsUnifyReportPrintForChildHospital()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //	
            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;
            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}
        }
        #endregion

        #region 打印接口初始化方法
        private void m_mthInitalPrintTool(PrintDocument p_printDoc)
        {
            //获取纸张的宽和高
            m_fltPaperWidth = p_printDoc.DefaultPageSettings.Bounds.Width;
            m_fltPaperHeight = p_printDoc.DefaultPageSettings.Bounds.Height;

            //设置打印区域的宽和高
            m_fltPrintWidth = m_fltPaperWidth * 0.95f;
            m_fltPrintHeight = m_fltPaperHeight * 0.9f;
            m_fltStartX = m_fltPaperWidth * 0.05f;
            m_fltEndY = m_fltPaperHeight - 114;

            //设置报告单组打印间隔
            m_fltTitleSpace = 5;
            m_fltItemSpace = 2;
            m_fltImgSpace = 10;

            //设置打印字体
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
        }
        #endregion

        #region 构造图形
        private Image m_imgDrawGraphic(byte[] p_bytGraph, string p_strImageFormat)
        {
            Image img = null;
            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(p_bytGraph);
                img = Image.FromStream(ms, true);
                string strFormat = (p_strImageFormat == null) ? null : p_strImageFormat.ToLower();
                switch (strFormat)
                {
                    case "lisb":
                        System.Drawing.Bitmap bm = new Bitmap(20, img.Height);
                        Graphics g = Graphics.FromImage(bm);
                        g.DrawImage(img, 0, 0, bm.Width, bm.Height);
                        img.Dispose();
                        img = bm;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return img;
        }
        #endregion

        #region 打印报告单基本信息
        private void m_mthPrintBseInfo()
        {
            if (m_dtbSample == null)
                return;

            m_fltY = 10;
            float fltColumn1 = m_fltStartX + m_fltPaperWidth * 0.04f;
            float fltColumn2 = m_fltPaperWidth * 0.28f;
            float fltColumn3 = m_fltPaperWidth * 0.49f;
            float fltColumn4 = m_fltPaperWidth * 0.73f;

            string m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim();
            //if (m_dtbSample.Rows[0]["report_print_chr"] != System.DBNull.Value)
            //{
            //    string strTime = m_dtbSample.Rows[0]["report_print_chr"].ToString().Trim();
            //    int intTime = 0;
            //    try
            //    {
            //        intTime = Convert.ToInt32(strTime);
            //        if (intTime > 0)
            //        {
            //            m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString() + "(重打)";
            //        }
            //    }
            //    catch
            //    { }
            //}

            //DrawTitle
            m_printMethodTool.m_mthPrintTitle(m_strTitle, m_fntTitle, m_fltY, m_fltPaperWidth);

            //Locate Y
            m_fltY += 10 + m_printMethodTool.m_fltGetStringHeight(m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim(), m_fntTitle);

            //姓名
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strPatientName,
                m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), fltColumn1, m_fltY);

            //住院号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strInPatientNo,
                m_dtbSample.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim(), fltColumn2, m_fltY);

            //样本类型
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleType,
                m_dtbSample.Rows[0]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim(), fltColumn3, m_fltY);

            //样本号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleID,
                m_dtbSample.Rows[0]["sample_id_chr"].ToString().Trim(), fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            //性别
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSex, m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), fltColumn1, m_fltY);

            //科室
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDepartment,
                m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), fltColumn2, m_fltY);

            //送检医生
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strApplyDoc,
                m_dtbSample.Rows[0]["applyer"].ToString().Trim(), fltColumn3, m_fltY);

            //检验编号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckNo,
                m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            //年龄
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strAge, m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), fltColumn1, m_fltY);

            //床号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strBedNo,
                m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), fltColumn2, m_fltY);
            if (blnSurePrintDiagnose)
            {
                //临床诊断
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDiagnose,
                    m_dtbSample.Rows[0]["diagnose_vchr"].ToString().Trim(), fltColumn3, m_fltY);
            }


            //送检日期
            string strDate = "";
            try
            {
                strDate = DateTime.Parse(m_dtbSample.Rows[0]["accept_dat"].ToString().Trim()).ToString("yyyy-MM-dd");
            }
            catch { }
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckDate, strDate, fltColumn4, m_fltY);

            //Locate Y
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.96f, m_fltY);

            m_fltY += 5;
        }
        #endregion

        #region 打印报告单实验室提示
        private float m_fltPrintSummary(float p_fltX, float p_fltY, float p_fltPrintWidth)
        {
            if (!m_blnSummaryEmptyVisible && m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim() == "")
                return p_fltY;
            float fltY = p_fltY + 10;
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            m_printMethodTool.m_mthDrawString(m_strSummary, m_fntSmallBold, p_fltX, fltY);
            fltY += m_fntSmallBold.Height + m_fltTitleSpace;
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, p_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            Rectangle rectSummary = new Rectangle((int)p_fltX, (int)fltY, (int)sf.Width, (int)sf.Height);
            new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, m_printMethodTool.m_printEventArg.Graphics);
            fltY += rectSummary.Height;
            return fltY;
        }
        #endregion

        #region 打印报告单底部信息
        private void m_mthPrintEnd()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 5;
            //Notice
            m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, m_fntSmallNotBold);
            //附注
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold) + 3;
            //画线
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.96f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //报告日期
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //检验医生
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //审核者
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);
        }
        #endregion

        #region 打印页信息
        private void m_mthPrintDetail()
        {
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (m_objPrintPage == null)
            {
                m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
                    , m_fltPaperHeight - 83 - (m_fltPaperHeight - m_fltEndY) - sf.Height, m_fltPaperHeight - 83 - (m_fltPaperHeight - m_fltEndY));
                m_intTotalPage = m_objPrintPage.Length;
            }
            if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            {
                m_printMethodTool.m_printEventArg.HasMorePages = false;
            }
            else
            {
                m_printMethodTool.m_printEventArg.HasMorePages = true;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            {
                //打印结果数据
                float fltY = m_fltPrintGroupData(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
            {
                //打印图形数据
                float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            {
                m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            }
        }
        #endregion

        #region 打印检验结果
        //打印检验结果
        private float m_fltPrintGroupData(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.23f, m_fltPrintWidth * 0.33f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.25f, m_fltPrintWidth * 0.42f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.5f;
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);
            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];

                //打印标题
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn1, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strReference, m_fntSmallBold, fltColumn3, fltBseY);
                fltBseY += fltTitleHeight;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    if ((p_objArr[i].m_intStartIdx + j) < p_objArr[i].m_dtvResult.Count)		//xing.chen add 2005/12/15
                    {
                        string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                        string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["ABNORMAL_FLAG_CHR"].ToString().Trim();
                        string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["UNIT_VCHR"].ToString().Trim();
                        string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString() + " " + strUnit;
                        string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["RPTNO_CHR"].ToString().Trim();

                        //打印项目
                        m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntSmall2NotBold, fltColumn1, fltBseY);

                        //异常标志
                        if (strAbnormal != null)
                        {
                            System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                            string strPR;

                            strPR = strResult + " " + "↑";
                            float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                            if (strAbnormal == "H")
                            {
                                strPR = strResult + " " + "↑";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else if (strAbnormal == "L")
                            {
                                if (strResult.Contains(">") || strResult.Contains("<"))
                                    strPR = strResult + " " + "↑";
                                else
                                    strPR = strResult + " " + "↓";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else
                            {
                                strPR = strResult + " " + " ";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, m_fntSmall2NotBold, fltStartPos, fltBseY);
                            }
                        }
                        m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn3, fltBseY);

                        //Locate Y 
                        fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                        if (fltY < fltBseY)
                        {
                            fltY = fltBseY;
                        }
                    }
                }
            }
            return fltY;
        }
        #endregion

        #region 打印图形
        private float m_fltPrintImageArr(clsPrintImage[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            for (int i = 0; i < p_objArr.Length; i++)
            {
                m_printMethodTool.m_printEventArg.Graphics.DrawImage(p_objArr[i].m_img, p_objArr[i].m_fltX,
                    p_objArr[i].m_fltY, p_objArr[i].m_fltWidth, p_objArr[i].m_fltHeight);
                if (fltY < p_objArr[i].m_fltY + p_objArr[i].m_fltHeight)
                {
                    fltY = p_objArr[i].m_fltY + p_objArr[i].m_fltHeight;
                }
            }
            return fltY;
        }
        #endregion

        #region 测量字符串块大小
        //测量字符串块大小
        private SizeF m_rectGetPrintStringRectangle(Font p_fntTitle, Font p_fntContent, string p_strContent, float p_fltWidth, float p_fltTitleSpace,
            float p_fltItemSpace)
        {
            if ((p_strContent == "" || p_strContent == null) && !m_blnSummaryEmptyVisible)
            {
                return new SizeF(0, 0);
            }
            float fltTitleHeight = p_fntTitle.Height;
            float fltContentHeight = p_fntContent.Height;
            float fltHeight = 0;
            if (p_strContent != null && p_strContent != "")
            {
                SizeF sfString = m_printMethodTool.m_printEventArg.Graphics.MeasureString(p_strContent, p_fntContent);
                fltHeight = (sfString.Width / p_fltWidth + 1) * fltContentHeight;
            }
            else
            {
                fltHeight = fltTitleHeight + p_fltTitleSpace + fltContentHeight;
            }
            SizeF sf = new SizeF(p_fltWidth, fltHeight);
            return sf;
        }
        #endregion

        #region 分页函数
        //先决条件：明确打印结果区域大小，已经过滤出结果数据和图形数据，明确标本组的个数以及各个标本组的打印区域大小、顺序
        //输入参数：结果DataTable，打印起始的XY位置(fltX,fltY)，以及打印的宽度和高度(fltWidth,fltHeight)
        //          (DataTable p_dtbResult,float p_fltX,float p_fltY,float p_fltWidth,float p_fltHeight)
        //输出参数：clsPrintPerPageInfo[]
        //流程：
        //0 过滤出结果数据和图形数据，构造各个标本组打印信息（clsSampleResultInfo），以及初始化图形信息（clsPrintImage）
        //1 按顺序打印标本组结果数据
        //1.1 判断样本信息数据能否一页打完
        //1.1.1 Y GOTO 2
        //1.1.2 N 换页 GoTo 1.1
        //2 结果数据打印完成，判断是否有图像数据
        //2.1 Y 判断当前页底是否够位置打印图形
        //2.1.1 Y 判断当前页能否打印完所有的图形
        //2.1.1.1 Y 打印 GoTo 2.2
        //2.1.1.2 N 打印 换页 GoTo 2.1
        //2.1.2 N 判断当前页侧是否够位置打印图形
        //2.1.2.1 Y 打印
        //2.1.2.2 N 换页 GoTo 2.1
        //2.2 N 打印结束，返回参数

        private clsPrintPerPageInfo[] m_objConstructPrintPageInfo(DataTable p_dtbResult, float p_fltX, float p_fltY,
            float p_fltWidth, float p_fltHeight, float p_fltMaxHeight)
        {
            //过滤出结果数据和图形数据
            DataView dtvData = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 0");
            DataView dtvImage = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 1");

            //排序
            dtvData.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";
            dtvImage.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";

            // 
            clsSampleResultInfo[] objDataArr = m_objConstructSampleResultArr(dtvData);
            clsPrintImage[] objImgArr = m_objConstructPrintImage(dtvImage);

            #region xing.chen add 2005.9.22
            float fltImgHeight = 0;
            if (objImgArr != null && objImgArr.Length > 0)
            {
                fltImgHeight = objImgArr[0].m_fltHeight + 10;
            }
            #endregion

            int intPage = 0;

            //打印与分页
            ArrayList arlPageData = new ArrayList();

            #region 结果数据打印分页
            float fltLeft = 0;
            float fltRight = 0;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltItemSpace);
            //记录分页剩余的记录个数
            int intTotalLeftItemCount = dtvData.Count;
            float fltHeight = 0;
            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= (p_fltHeight - fltImgHeight) * 2)	//xing.chen modify
            {
                fltHeight = p_fltHeight - fltImgHeight;	//xing.chen modify
            }
            else
            {
                fltHeight = p_fltMaxHeight - fltImgHeight;	//xing.chen modify
            }

            ArrayList arlPrintData = new ArrayList();
            //指示当前是否在右边打印
            bool blnPrintRight = false;
            for (int i = 0; i < objDataArr.Length; i++)
            {
                int intDataCount = objDataArr[i].m_dtvResult.Count;
                objDataArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objDataArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                //左边打印
                if (!blnPrintRight && objDataArr[i].m_fltHeight < fltHeight - fltLeft)
                {
                    objDataArr[i].m_fltX = p_fltX;
                    objDataArr[i].m_fltY = fltLeft + p_fltY;
                    objDataArr[i].m_intStartIdx = 0;
                    objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                    objDataArr[i].m_intPageIdx = intPage;
                    fltLeft += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                    arlPrintData.Add(objDataArr[i]);
                    intTotalLeftItemCount -= objDataArr[i].m_intCount;
                }
                else
                {
                    //判断余下的记录能否在另一边打完,并且当前已经打印的记录个数必须大于或等于单列打印个数的1/2
                    if (fltLeft >= fltHeight / 2 && fltItemHeight * intTotalLeftItemCount < fltHeight)
                    {
                        blnPrintRight = true;
                        objDataArr[i].m_fltX = p_fltX + p_fltWidth / 2;
                        objDataArr[i].m_fltY = fltRight + p_fltY;
                        objDataArr[i].m_intStartIdx = 0;
                        objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                        objDataArr[i].m_intPageIdx = intPage;
                        fltRight += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                        arlPrintData.Add(objDataArr[i]);
                        intTotalLeftItemCount -= objDataArr[i].m_intCount;
                    }
                    else
                    {
                        while (intDataCount > 0)
                        {
                            if (fltTitleHeight + fltItemHeight < fltHeight - fltLeft)
                            {
                                int intPrintItemCount = 1;
                                while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltLeft)
                                {
                                    intPrintItemCount++;
                                }
                                clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                obj.m_fltX = p_fltX;
                                obj.m_fltY = fltLeft + p_fltY;
                                obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                obj.m_intCount = intPrintItemCount;
                                obj.m_intPageIdx = intPage;
                                fltLeft += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                arlPrintData.Add(obj);
                                intDataCount -= intPrintItemCount;
                                intTotalLeftItemCount -= intPrintItemCount;
                            }
                            else
                            {
                                //右边打印
                                if (fltTitleHeight + fltItemHeight * intDataCount < fltHeight - fltRight)
                                {
                                    clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                    obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                    obj.m_fltX = p_fltX + p_fltWidth / 2;
                                    obj.m_fltY = fltRight + p_fltY;
                                    obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                    obj.m_intCount = intDataCount;
                                    obj.m_intPageIdx = intPage;
                                    fltRight += intDataCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                    arlPrintData.Add(obj);
                                    intDataCount -= intDataCount;
                                    intTotalLeftItemCount -= intDataCount;
                                }
                                else
                                {
                                    if (fltTitleHeight + fltItemHeight < fltHeight - fltRight)
                                    {
                                        int intPrintItemCount = 1;
                                        while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltRight)
                                        {
                                            intPrintItemCount++;
                                        }
                                        clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                        obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                        obj.m_fltX = p_fltX + p_fltWidth / 2;
                                        obj.m_fltY = fltRight + p_fltY;
                                        obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                        obj.m_intCount = intPrintItemCount;
                                        obj.m_intPageIdx = intPage;
                                        fltRight += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                        arlPrintData.Add(obj);
                                        intDataCount -= intPrintItemCount;
                                        intTotalLeftItemCount -= intPrintItemCount;
                                    }
                                    else
                                    {
                                        fltLeft = 0;
                                        fltRight = 0;
                                        blnPrintRight = false;
                                        intPage++;
                                        arlPageData.Add(arlPrintData);
                                        arlPrintData = new ArrayList();
                                        if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight * 2)
                                        {
                                            fltHeight = p_fltHeight;
                                        }
                                        else
                                        {
                                            fltHeight = p_fltMaxHeight;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (arlPrintData.Count > 0)
            {
                arlPageData.Add(arlPrintData);
            }
            #endregion

            float fltY = Math.Max(fltLeft, fltRight);
            //			fltY += 4*m_fltTitleSpace;
            int intImgStartIdx = intPage;
            ArrayList arlPageImg = null;
            ArrayList arlImg = null;

            #region 图形数据打印分页
            if (objImgArr != null && objImgArr.Length > 0)
            {
                arlPageImg = new ArrayList();
                arlImg = new ArrayList();
                float fltX = 0;
                for (int i = 0; i < objImgArr.Length; i++)
                {
                    if (objImgArr[i].m_fltHeight < p_fltMaxHeight && objImgArr[i].m_fltWidth < p_fltWidth)
                    {
                        bool blnDrawed = false;
                        while (!blnDrawed)
                        {
                            if (p_fltMaxHeight - fltY > objImgArr[i].m_fltHeight)
                            {
                                if (p_fltWidth - fltX > objImgArr[i].m_fltWidth)
                                {
                                    objImgArr[i].m_fltX = (fltX == 0 ? fltX + p_fltX : fltX + p_fltX + m_fltImgSpace);
                                    objImgArr[i].m_fltY = fltY + p_fltY;
                                    objImgArr[i].m_intPageIdx = intPage;
                                    arlImg.Add(objImgArr[i]);
                                    fltX += objImgArr[i].m_fltWidth + m_fltImgSpace;
                                    blnDrawed = true;
                                }
                                else
                                {
                                    if (i > 0)
                                    {
                                        fltY += objImgArr[i].m_fltHeight + m_fltImgSpace;
                                        fltX = 0;
                                    }
                                }
                            }
                            else
                            {
                                fltX = 0;
                                fltY = 0;
                                if (arlImg.Count > 0)
                                {
                                    arlPageImg.Add(arlImg);
                                    arlImg = new ArrayList();
                                }
                                intPage++;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (arlImg.Count > 0)
                {
                    arlPageImg.Add(arlImg);
                }
            }
            #endregion

            //实验室提示
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (sf.Height > 0 && sf.Height > p_fltMaxHeight - fltY)
            {
                intPage++;
            }

            #region 构造页面打印信息
            clsPrintPerPageInfo[] objArr = new clsPrintPerPageInfo[intPage + 1];
            int intStartImgIdx = -1;
            if (arlPageImg != null)
            {
                intStartImgIdx = ((clsPrintImage[])((ArrayList)arlPageImg[0]).ToArray(typeof(clsPrintImage)))[0].m_intPageIdx;
            }
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = new clsPrintPerPageInfo();
                if (i <= arlPageData.Count - 1)
                {
                    objArr[i].m_objSampleArr = (clsSampleResultInfo[])((ArrayList)arlPageData[i]).ToArray(typeof(clsSampleResultInfo));
                }
                if (arlPageImg != null)
                {
                    if (intStartImgIdx <= i && i <= intStartImgIdx + arlPageImg.Count - 1)
                    {
                        objArr[i].m_imgArr = (clsPrintImage[])((ArrayList)arlPageImg[i - intStartImgIdx]).ToArray(typeof(clsPrintImage));
                    }
                }
            }
            #endregion

            return objArr;
        }
        #endregion

        #region FunctionMethod
        /// <summary>
        /// 获取打印组的高度
        /// </summary>
        /// <param name="p_objData"></param>
        /// <param name="p_fntTitle"></param>
        /// <param name="p_fntItem"></param>
        /// <param name="p_fltTitleSpace"></param>
        /// <param name="p_fltItemSpace"></param>
        /// <returns></returns>
        private float m_fltGetPrintGroupHeight(clsSampleResultInfo p_objData, Font p_fntTitle, Font p_fntItem,
            float p_fltTitleSpace, float p_fltItemSpace)
        {
            float fltHeight = 0;
            fltHeight += (p_fntTitle.Height + p_fltTitleSpace) + (p_objData.m_intCount * (p_fntItem.Height + p_fltItemSpace));
            return fltHeight;
        }

        private float m_fltGetPrintElementHeight(Font p_fnt, float p_fltPrintSpace)
        {
            float fltHeight = 0;
            fltHeight += p_fnt.Height + p_fltPrintSpace;
            return fltHeight;
        }

        /// <summary>
        /// 构造打印数据
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsSampleResultInfo[] m_objConstructSampleResultArr(DataView p_dtvData)
        {
            ArrayList arlGroupID = new ArrayList();
            clsSampleResultInfo[] objArr = null;
            for (int i = 0; i < p_dtvData.Count; i++)
            {
                if (i > 0)
                {
                    if (p_dtvData[i]["groupid_chr"].ToString().Trim() != p_dtvData[i - 1]["groupid_chr"].ToString().Trim())
                    {
                        arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                    }
                }
                else
                {
                    arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                }
            }
            if (arlGroupID.Count > 0)
            {
                objArr = new clsSampleResultInfo[arlGroupID.Count];
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtv = new DataView(p_dtvData.Table);
                    dtv.RowFilter = "IS_GRAPH_RESULT_NUM = 0 AND groupid_chr = " + arlGroupID[i].ToString().Trim();
                    objArr[i] = new clsSampleResultInfo(dtv);
                    objArr[i].m_dtvResult.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    objArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                    objArr[i].m_intCount = objArr[i].m_dtvResult.Count;
                }
            }
            return objArr;
        }

        /// <summary>
        /// 构造打印图形
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsPrintImage[] m_objConstructPrintImage(DataView p_dtvData)		// xing.chen modify 2005.9.22
        {
            int intCount = p_dtvData.Count;
            clsPrintImage[] objImgArr = null;
            ArrayList arl = new ArrayList();
            for (int i = 0; i < intCount; i++)
            {
                if (p_dtvData[i]["GRAPH_IMG"] is System.DBNull)
                {
                    continue;
                }
                Image img = m_imgDrawGraphic((byte[])p_dtvData[i]["GRAPH_IMG"], p_dtvData[i]["GRAPH_FORMAT_NAME_VCHR"].ToString());
                if (img != null)
                {
                    clsPrintImage objImg = new clsPrintImage(img);
                    objImg.m_fltWidth = m_fltXRate * objImg.m_fltWidth;
                    objImg.m_fltHeight = m_fltYRate * objImg.m_fltHeight;
                    arl.Add(objImg);
                }
            }
            objImgArr = (clsPrintImage[])arl.ToArray(typeof(clsPrintImage));
            return objImgArr;
        }

        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="p_dtbSource"></param>
        /// <param name="p_strFltExp"></param>
        /// <returns></returns>
        private DataView m_dtvFilterRows(DataTable p_dtbSource, string p_strFltExp)
        {
            DataView dtv = new DataView(p_dtbSource);
            dtv.RowFilter = p_strFltExp;
            return dtv;
        }
        #endregion

        #region 打印整张报告单
        private void m_mthPrint()
        {
            m_mthPrintBseInfo();
            m_mthPrintDetail();
            m_mthPrintEnd();
            if (m_intTotalPage - 1 > m_intCurrentPageIdx)
            {
                m_intCurrentPageIdx++;
            }
        }
        #endregion

        #region 继承打印接口

        public void m_mthInitPrintContent()
        {
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_mthInitalPrintTool((PrintDocument)p_objArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_printMethodTool = new clsCommonPrintMethod((PrintPageEventArgs)p_objPrintArg);
            m_mthPrint();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
        }

        #endregion
    }


    public class clsUnifyReportPrintForChildHospital_B5 : infPrintRecord
    {
        #region 报告单参数
        private float m_fltPaperWidth;        //打印纸张的宽度
        private float m_fltPaperHeight;       //打印纸张的高度
        private float m_fltPrintWidth;        //打印区域的宽度
        private float m_fltPrintHeight;       //打印区域的高度
        private float m_fltStartX;            //报告单X起始位置
        private float m_fltEndY;              //报告单底部信息位置
        private float m_fltTitleSpace;        //报告单组打印标题间隔
        private float m_fltItemSpace;         //报告单组打印项目间隔
        private float m_fltImgSpace;          //图形打印间隔

        //图形缩放比例
        private float m_fltXRate = 0.8f;
        private float m_fltYRate = 0.8f;

        private string m_strPatientName = "姓名:";
        private string m_strSex = "性别:";
        private string m_strAge = "年龄:";
        private string m_strInPatientNo = "住院号:";
        private string m_strDepartment = "科  室:";
        private string m_strBedNo = "床  号:";
        private string m_strSampleType = "样本类型:";
        private string m_strApplyDoc = "送检医生:";
        private string m_strDiagnose = "临床诊断:";
        private string m_strSampleID = "样本号:";
        private string m_strCheckNo = "检验编号:";
        private string m_strCheckDate = "送检日期:";
        private string m_strSummary = "实验室提示:";
        private string m_strNotice = "(检验结果仅供临床诊疗参考，只对该检测的标本负责!)";
        private string m_strAnnotation = "附注:";
        private string m_strReportDate = "报告日期:";
        private string m_strCheckDoc = "检验医生:";
        private string m_strConfirmEmp = "审核者:";
        private string m_strResult = "结果";
        private string m_strReference = "参考区间";

        //字体设置
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;

        //报告单数据
        public DataTable m_dtbSample;
        public DataTable m_dtbResult;

        //打印方法工具
        clsCommonPrintMethod m_printMethodTool;

        //Y位置定位
        private float m_fltY;

        private bool m_blnDocked = true; //打印底部信息固定否

        //打印信息分页
        clsPrintPerPageInfo[] m_objPrintPage;

        //指示当前打印页码
        private int m_intCurrentPageIdx = 0;
        private int m_intTotalPage = 0;

        //实验室提示和附注为空时是否显示
        bool m_blnSummaryEmptyVisible = false;
        bool m_blnAnnotationEmptyVisible = false;
        /// <summary>
        /// 是否打印诊断
        /// </summary>
        public static bool blnSurePrintDiagnose = false;
        #endregion

        #region 底部打印信息固定属性
        public bool IsDocked
        {
            get
            {
                return m_blnDocked;
            }
            set
            {
                m_blnDocked = value;
            }
        }
        #endregion

        #region 构造函数
        public clsUnifyReportPrintForChildHospital_B5()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //	
            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;
            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}
        }
        #endregion

        #region 打印接口初始化方法
        private void m_mthInitalPrintTool(PrintDocument p_printDoc)
        {
            //获取纸张的宽和高
            m_fltPaperWidth = p_printDoc.DefaultPageSettings.Bounds.Width;
            m_fltPaperHeight = p_printDoc.DefaultPageSettings.Bounds.Height;

            //设置打印区域的宽和高
            m_fltPrintWidth = m_fltPaperWidth * 0.95f;
            m_fltPrintHeight = m_fltPaperHeight * 0.9f;
            m_fltStartX = m_fltPaperWidth * 0.05f;
            m_fltEndY = m_fltPaperHeight - 114;

            //设置报告单组打印间隔
            m_fltTitleSpace = 5;
            m_fltItemSpace = 2;
            m_fltImgSpace = 10;

            //设置打印字体
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 10.5f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
        }
        #endregion

        #region 构造图形
        private Image m_imgDrawGraphic(byte[] p_bytGraph, string p_strImageFormat)
        {
            Image img = null;
            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(p_bytGraph);
                img = Image.FromStream(ms, true);
                string strFormat = (p_strImageFormat == null) ? null : p_strImageFormat.ToLower();
                switch (strFormat)
                {
                    case "lisb":
                        System.Drawing.Bitmap bm = new Bitmap(20, img.Height);
                        Graphics g = Graphics.FromImage(bm);
                        g.DrawImage(img, 0, 0, bm.Width, bm.Height);
                        img.Dispose();
                        img = bm;
                        break;
                    default:
                        System.Drawing.Bitmap bm2 = new Bitmap(17, img.Height - 4);
                        Graphics g2 = Graphics.FromImage(bm2);
                        g2.DrawImage(img, 0, 0, bm2.Width, bm2.Height);
                        img.Dispose();
                        img = bm2;
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return img;
        }
        #endregion

        #region 打印报告单基本信息
        private void m_mthPrintBseInfo()
        {
            if (m_dtbSample == null)
                return;

            m_fltY = 10;
            float fltColumn1 = m_fltStartX + m_fltPaperWidth * 0.04f;
            float fltColumn2 = m_fltPaperWidth * 0.28f;
            float fltColumn3 = m_fltPaperWidth * 0.49f;
            float fltColumn4 = m_fltPaperWidth * 0.73f;


            string m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim();
            //if (m_dtbSample.Rows[0]["report_print_chr"] != System.DBNull.Value)
            //{
            //    string strTime = m_dtbSample.Rows[0]["report_print_chr"].ToString().Trim();
            //    int intTime = 0;
            //    try
            //    {
            //        intTime = Convert.ToInt32(strTime);
            //        if (intTime > 0)
            //        {
            //            m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString() + "(重打)";
            //        }
            //    }
            //    catch
            //    { }
            //}
            //DrawTitle
            m_printMethodTool.m_mthPrintTitle(m_strTitle, m_fntTitle, m_fltY, m_fltPaperWidth);

            //Locate Y
            m_fltY += 10 + m_printMethodTool.m_fltGetStringHeight(m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim(), m_fntTitle);

            //姓名
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strPatientName,
                m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), fltColumn1, m_fltY);

            //住院号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strInPatientNo,
                m_dtbSample.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim(), fltColumn2, m_fltY);

            //样本类型
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleType,
                m_dtbSample.Rows[0]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim(), fltColumn3, m_fltY);

            //样本号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleID,
                m_dtbSample.Rows[0]["sample_id_chr"].ToString().Trim(), fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            //性别
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSex, m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), fltColumn1, m_fltY);

            //科  室
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDepartment,
                m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), fltColumn2, m_fltY);

            //送检医生
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strApplyDoc,
                m_dtbSample.Rows[0]["applyer"].ToString().Trim(), fltColumn3, m_fltY);

            //检验编号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckNo,
                m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            //年龄
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strAge, m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), fltColumn1, m_fltY);

            //床  号
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strBedNo,
                m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), fltColumn2, m_fltY);
            if (blnSurePrintDiagnose)
            {
                //临床诊断
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDiagnose,
                    m_dtbSample.Rows[0]["diagnose_vchr"].ToString().Trim(), fltColumn3, m_fltY);

            }

            //送检日期
            string strDate = "";
            try
            {
                strDate = DateTime.Parse(m_dtbSample.Rows[0]["accept_dat"].ToString().Trim()).ToString("yyyy-MM-dd");
            }
            catch { }
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckDate, strDate, fltColumn4, m_fltY);

            //Locate Y
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);

            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.96f, m_fltY);

            m_fltY += 5;
        }
        #endregion

        #region 打印报告单实验室提示
        private float m_fltPrintSummary(float p_fltX, float p_fltY, float p_fltPrintWidth)
        {
            if (!m_blnSummaryEmptyVisible && m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim() == "")
                return p_fltY;
            float fltY = p_fltY + 10;
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            m_printMethodTool.m_mthDrawString(m_strSummary, m_fntSmallBold, p_fltX, fltY);
            fltY += m_fntSmallBold.Height + m_fltTitleSpace;
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, p_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            Rectangle rectSummary = new Rectangle((int)p_fltX, (int)fltY, (int)sf.Width, (int)sf.Height);
            new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, m_printMethodTool.m_printEventArg.Graphics);
            fltY += rectSummary.Height;
            return fltY;
        }
        #endregion

        #region 打印报告单底部信息
        private void m_mthPrintEnd()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 5;
            //Notice
            m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, m_fntSmallNotBold);
            //附注
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold) + 3;
            //画线
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.96f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //报告日期
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //检验医生
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //审核者
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);
        }
        #endregion

        #region 打印页信息
        private void m_mthPrintDetail()
        {
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (m_objPrintPage == null)
            {
                m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
                    , m_fltPaperHeight - 83 - (m_fltPaperHeight - m_fltEndY) - sf.Height, m_fltPaperHeight - 83 - (m_fltPaperHeight - m_fltEndY));
                m_intTotalPage = m_objPrintPage.Length;
            }
            if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            {
                m_printMethodTool.m_printEventArg.HasMorePages = false;
            }
            else
            {
                m_printMethodTool.m_printEventArg.HasMorePages = true;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            {
                //打印结果数据
                float fltY = m_fltPrintGroupData(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
            {
                //打印图形数据
                float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
                if (fltY != -1)
                {
                    if (fltY > m_fltY)
                    {
                        m_fltY = fltY;
                    }
                }
            }
            if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            {
                m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            }
        }
        #endregion

        #region 打印检验结果
        //打印检验结果
        private float m_fltPrintGroupData(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.23f, m_fltPrintWidth * 0.33f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.25f, m_fltPrintWidth * 0.42f };
                //				fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0])*0.5f;
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.7f;			//xing.chen modify 2005/12/24
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);
            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];

                //打印标题
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn1, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2 + 40, fltBseY);		//xing.chen modify 2005/12/24
                m_printMethodTool.m_mthDrawString(m_strReference, m_fntSmallBold, fltColumn3, fltBseY);
                fltBseY += fltTitleHeight;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    if ((p_objArr[i].m_intStartIdx + j) < p_objArr[i].m_dtvResult.Count)		//xing.chen add 2005/12/15
                    {
                        string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                        string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["ABNORMAL_FLAG_CHR"].ToString().Trim();
                        string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["UNIT_VCHR"].ToString().Trim();
                        string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString() + " " + strUnit;
                        string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["RPTNO_CHR"].ToString().Trim();

                        //打印项目
                        m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntSmall2NotBold, fltColumn1, fltBseY);

                        //异常标志
                        if (strAbnormal != null)
                        {
                            System.Drawing.Font objBoldFont = new Font("SimSun", 10.5f, FontStyle.Bold);
                            string strPR;

                            strPR = strResult + " " + "↑";
                            float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                            if (strAbnormal == "H")
                            {
                                strPR = strResult + " " + "↑";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else if (strAbnormal == "L")
                            {
                                if (strResult.Contains(">") || strResult.Contains("<"))
                                    strPR = strResult + " " + "↑";
                                else
                                    strPR = strResult + " " + "↓";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                            }
                            else
                            {
                                strPR = strResult + " " + " ";
                                float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                                m_printMethodTool.m_mthDrawString(strPR, m_fntSmall2NotBold, fltStartPos, fltBseY);
                            }
                        }
                        m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn3, fltBseY);

                        //Locate Y 
                        fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                        if (fltY < fltBseY)
                        {
                            fltY = fltBseY;
                        }
                    }
                }
            }
            return fltY;
        }
        #endregion

        #region 打印图形
        private float m_fltPrintImageArr(clsPrintImage[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            for (int i = 0; i < p_objArr.Length; i++)
            {
                m_printMethodTool.m_printEventArg.Graphics.DrawImage(p_objArr[i].m_img, p_objArr[i].m_fltX,
                    p_objArr[i].m_fltY, p_objArr[i].m_fltWidth, p_objArr[i].m_fltHeight);
                if (fltY < p_objArr[i].m_fltY + p_objArr[i].m_fltHeight)
                {
                    fltY = p_objArr[i].m_fltY + p_objArr[i].m_fltHeight;
                }
            }
            return fltY;
        }
        #endregion

        #region 测量字符串块大小
        //测量字符串块大小
        private SizeF m_rectGetPrintStringRectangle(Font p_fntTitle, Font p_fntContent, string p_strContent, float p_fltWidth, float p_fltTitleSpace,
            float p_fltItemSpace)
        {
            if ((p_strContent == "" || p_strContent == null) && !m_blnSummaryEmptyVisible)
            {
                return new SizeF(0, 0);
            }
            float fltTitleHeight = p_fntTitle.Height;
            float fltContentHeight = p_fntContent.Height;
            float fltHeight = 0;
            if (p_strContent != null && p_strContent != "")
            {
                SizeF sfString = m_printMethodTool.m_printEventArg.Graphics.MeasureString(p_strContent, p_fntContent);
                fltHeight = (sfString.Width / p_fltWidth + 1) * fltContentHeight;
            }
            else
            {
                fltHeight = fltTitleHeight + p_fltTitleSpace + fltContentHeight;
            }
            SizeF sf = new SizeF(p_fltWidth, fltHeight);
            return sf;
        }
        #endregion

        #region 分页函数
        //先决条件：明确打印结果区域大小，已经过滤出结果数据和图形数据，明确标本组的个数以及各个标本组的打印区域大小、顺序
        //输入参数：结果DataTable，打印起始的XY位置(fltX,fltY)，以及打印的宽度和高度(fltWidth,fltHeight)
        //          (DataTable p_dtbResult,float p_fltX,float p_fltY,float p_fltWidth,float p_fltHeight)
        //输出参数：clsPrintPerPageInfo[]
        //流程：
        //0 过滤出结果数据和图形数据，构造各个标本组打印信息（clsSampleResultInfo），以及初始化图形信息（clsPrintImage）
        //1 按顺序打印标本组结果数据
        //1.1 判断样本信息数据能否一页打完
        //1.1.1 Y GOTO 2
        //1.1.2 N 换页 GoTo 1.1
        //2 结果数据打印完成，判断是否有图像数据
        //2.1 Y 判断当前页底是否够位置打印图形
        //2.1.1 Y 判断当前页能否打印完所有的图形
        //2.1.1.1 Y 打印 GoTo 2.2
        //2.1.1.2 N 打印 换页 GoTo 2.1
        //2.1.2 N 判断当前页侧是否够位置打印图形
        //2.1.2.1 Y 打印
        //2.1.2.2 N 换页 GoTo 2.1
        //2.2 N 打印结束，返回参数

        private clsPrintPerPageInfo[] m_objConstructPrintPageInfo(DataTable p_dtbResult, float p_fltX, float p_fltY,
            float p_fltWidth, float p_fltHeight, float p_fltMaxHeight)
        {
            //过滤出结果数据和图形数据
            DataView dtvData = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 0");
            DataView dtvImage = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 1");

            //排序
            dtvData.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";
            dtvImage.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";		//xing.chen 修改排序 

            // 构造结果数据＆图形数据
            clsSampleResultInfo[] objDataArr = m_objConstructSampleResultArr(dtvData);
            clsPrintImage[] objImgArr = m_objConstructPrintImage(dtvImage);

            //扩大图形高度
            #region xing.chen add 2005.9.22
            float fltImgHeight = 0;
            if (objImgArr != null && objImgArr.Length > 0)
            {
                fltImgHeight = objImgArr[0].m_fltHeight + 10;
            }
            #endregion

            int intPage = 0;

            //打印与分页
            ArrayList arlPageData = new ArrayList();

            #region 结果数据打印分页
            float fltDataY = 0;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltItemSpace);
            //记录分页剩余的记录个数
            int intTotalLeftItemCount = dtvData.Count;
            //判断是否能一页打完，同时根据分页情况设置可打印高度
            float fltHeight = 0;
            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight)	//xing.chen modify
            {
                fltHeight = p_fltHeight;	//xing.chen modify
            }
            else
            {
                fltHeight = p_fltMaxHeight;	//xing.chen modify
            }

            ArrayList arlPrintData = new ArrayList();
            for (int i = 0; i < objDataArr.Length; i++)
            {
                int intDataCount = objDataArr[i].m_dtvResult.Count;
                objDataArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objDataArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);

                //判断该项目组能否在fltHeight高度中打完
                if (objDataArr[i].m_fltHeight < fltHeight - fltDataY)
                {
                    objDataArr[i].m_fltX = p_fltX;
                    objDataArr[i].m_fltY = fltDataY + p_fltY;
                    objDataArr[i].m_intStartIdx = 0;
                    objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                    objDataArr[i].m_intPageIdx = intPage;
                    fltDataY += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                    arlPrintData.Add(objDataArr[i]);
                    intTotalLeftItemCount -= objDataArr[i].m_intCount;
                }
                else
                {
                    while (intDataCount > 0)
                    {
                        if (fltTitleHeight + fltItemHeight < fltHeight - fltDataY)
                        {
                            int intPrintItemCount = 1;
                            while ((intPrintItemCount + 1) <= intDataCount && (intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltDataY)
                            {
                                intPrintItemCount++;
                            }
                            clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                            obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                            obj.m_fltX = p_fltX;
                            obj.m_fltY = fltDataY + p_fltY;
                            obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                            obj.m_intCount = intPrintItemCount;
                            obj.m_intPageIdx = intPage;
                            fltDataY += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                            arlPrintData.Add(obj);
                            intDataCount -= intPrintItemCount;
                            intTotalLeftItemCount -= intPrintItemCount;
                        }
                        else
                        {
                            fltDataY = 0;
                            intPage++;
                            arlPageData.Add(arlPrintData);
                            arlPrintData = new ArrayList();
                            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight)
                            {
                                fltHeight = p_fltHeight;
                            }
                            else
                            {
                                fltHeight = p_fltMaxHeight;
                            }
                        }
                    }
                }
            }

            if (arlPrintData.Count > 0)
            {
                arlPageData.Add(arlPrintData);
            }
            #endregion

            float fltY = fltDataY;
            int intImgStartIdx = intPage;
            ArrayList arlPageImg = null;
            ArrayList arlImg = null;

            #region 图形数据打印分页
            float fltImgY = 0;
            int intImgPage = 0;
            if (objImgArr != null && objImgArr.Length > 0)
            {
                arlPageImg = new ArrayList();
                arlImg = new ArrayList();
                int intImgCount = objImgArr.Length;

                for (int i = 0; i < objImgArr.Length; i++)
                {
                    //判断该图能否在p_fltMaxHeight高度中打完
                    if (objImgArr[i].m_fltHeight + m_fltImgSpace < fltHeight - fltImgY)
                    {
                        objImgArr[i].m_fltX = p_fltX + p_fltWidth / 2 + 100;
                        objImgArr[i].m_fltY = fltImgY + p_fltY;
                        objImgArr[i].m_intPageIdx = intImgPage;
                        arlImg.Add(objImgArr[i]);
                        fltImgY += objImgArr[i].m_fltHeight + m_fltImgSpace;
                    }
                    else
                    {
                        fltImgY = 0;
                        if (arlImg.Count > 0)
                        {
                            arlPageImg.Add(arlImg);
                            arlImg = new ArrayList();
                        }
                        intImgPage++;
                    }
                }
                if (arlImg.Count > 0)
                {
                    arlPageImg.Add(arlImg);
                }
            }
            #endregion

            intPage = Math.Max(intPage, intImgPage);

            //实验室提示
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (sf.Height > 0 && sf.Height > p_fltMaxHeight - fltY)
            {
                intPage++;
            }

            #region 构造页面打印信息
            clsPrintPerPageInfo[] objArr = new clsPrintPerPageInfo[intPage + 1];
            int intStartImgIdx = -1;
            if (arlPageImg != null)
            {
                intStartImgIdx = ((clsPrintImage[])((ArrayList)arlPageImg[0]).ToArray(typeof(clsPrintImage)))[0].m_intPageIdx;
            }
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = new clsPrintPerPageInfo();
                if (i <= arlPageData.Count - 1)
                {
                    objArr[i].m_objSampleArr = (clsSampleResultInfo[])((ArrayList)arlPageData[i]).ToArray(typeof(clsSampleResultInfo));
                }
                if (arlPageImg != null)
                {
                    if (intStartImgIdx <= i && i <= intStartImgIdx + arlPageImg.Count - 1)
                    {
                        objArr[i].m_imgArr = (clsPrintImage[])((ArrayList)arlPageImg[i - intStartImgIdx]).ToArray(typeof(clsPrintImage));
                    }
                }
            }
            #endregion

            return objArr;
        }
        #endregion

        #region FunctionMethod
        /// <summary>
        /// 获取打印组的高度
        /// </summary>
        /// <param name="p_objData"></param>
        /// <param name="p_fntTitle"></param>
        /// <param name="p_fntItem"></param>
        /// <param name="p_fltTitleSpace"></param>
        /// <param name="p_fltItemSpace"></param>
        /// <returns></returns>
        private float m_fltGetPrintGroupHeight(clsSampleResultInfo p_objData, Font p_fntTitle, Font p_fntItem,
            float p_fltTitleSpace, float p_fltItemSpace)
        {
            float fltHeight = 0;
            fltHeight += (p_fntTitle.Height + p_fltTitleSpace) + (p_objData.m_intCount * (p_fntItem.Height + p_fltItemSpace));
            return fltHeight;
        }

        private float m_fltGetPrintElementHeight(Font p_fnt, float p_fltPrintSpace)
        {
            float fltHeight = 0;
            fltHeight += p_fnt.Height + p_fltPrintSpace;
            return fltHeight;
        }

        /// <summary>
        /// 构造打印数据
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsSampleResultInfo[] m_objConstructSampleResultArr(DataView p_dtvData)
        {
            ArrayList arlGroupID = new ArrayList();
            clsSampleResultInfo[] objArr = null;
            for (int i = 0; i < p_dtvData.Count; i++)
            {
                if (i > 0)
                {
                    if (p_dtvData[i]["groupid_chr"].ToString().Trim() != p_dtvData[i - 1]["groupid_chr"].ToString().Trim())
                    {
                        arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                    }
                }
                else
                {
                    arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                }
            }
            if (arlGroupID.Count > 0)
            {
                objArr = new clsSampleResultInfo[arlGroupID.Count];
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtv = new DataView(p_dtvData.Table);
                    dtv.RowFilter = "IS_GRAPH_RESULT_NUM = 0 AND groupid_chr = " + arlGroupID[i].ToString().Trim();
                    objArr[i] = new clsSampleResultInfo(dtv);
                    objArr[i].m_dtvResult.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    objArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                    objArr[i].m_intCount = objArr[i].m_dtvResult.Count;
                }
            }
            return objArr;
        }

        /// <summary>
        /// 构造打印图形
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsPrintImage[] m_objConstructPrintImage(DataView p_dtvData)		// xing.chen modify 2005.9.22
        {
            int intCount = p_dtvData.Count;
            clsPrintImage[] objImgArr = null;
            ArrayList arl = new ArrayList();
            for (int i = 0; i < intCount; i++)
            //			for(int i=intCount-1;i>=0;i--)			// xing.chen modify 2005/12/24
            {
                if (p_dtvData[i]["GRAPH_IMG"] is System.DBNull)
                {
                    continue;
                }
                Image img = m_imgDrawGraphic((byte[])p_dtvData[i]["GRAPH_IMG"], p_dtvData[i]["GRAPH_FORMAT_NAME_VCHR"].ToString());
                if (img != null)
                {
                    clsPrintImage objImg = new clsPrintImage(img);
                    objImg.m_fltWidth = m_fltXRate * objImg.m_fltWidth;
                    objImg.m_fltHeight = m_fltYRate * objImg.m_fltHeight;
                    arl.Add(objImg);
                }
            }
            objImgArr = (clsPrintImage[])arl.ToArray(typeof(clsPrintImage));
            return objImgArr;
        }

        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="p_dtbSource"></param>
        /// <param name="p_strFltExp"></param>
        /// <returns></returns>
        private DataView m_dtvFilterRows(DataTable p_dtbSource, string p_strFltExp)
        {
            DataView dtv = new DataView(p_dtbSource);
            dtv.RowFilter = p_strFltExp;
            return dtv;
        }
        #endregion

        #region 打印整张报告单
        private void m_mthPrint()
        {
            m_mthPrintBseInfo();
            m_mthPrintDetail();
            m_mthPrintEnd();
            if (m_intTotalPage - 1 > m_intCurrentPageIdx)
            {
                m_intCurrentPageIdx++;
            }
        }
        #endregion

        #region 继承打印接口

        public void m_mthInitPrintContent()
        {
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_mthInitalPrintTool((PrintDocument)p_objArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_printMethodTool = new clsCommonPrintMethod((PrintPageEventArgs)p_objPrintArg);
            m_mthPrint();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
        }

        #endregion
    }
}