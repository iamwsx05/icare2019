using System;
using iCareData;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// 腹腔镜手术同意书的打印工具类
    /// </summary>
    public class clsFuqiangOperationyesPrintTool_xj : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//表明是从数据库读取还是从文件直接提取信息
        private bool m_blnWantInit = true;
        /// <summary>
        /// 是否打印修改痕迹
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        private clsDiseaseTrackDomain m_objRecordsDomain;
        private clsPrintInfo_FuqiangOperationyes_xj m_objPrintInfo;
        private clsFuqiangOperationyesContent_xj m_objRecordContent = null;
        private DateTime m_dtmOutDate = DateTime.MinValue;

        /// <summary>
        /// 设置打印信息(当从数据库读取时要首先调用.)
        /// </summary>
        /// <param name="p_objPatient">病人</param>
        /// <param name="p_dtmInPatientDate">入院日期</param>
        /// <param name="p_dtmOpenDate">OpenDate，如果是一次打印多次记录表单的类型（如病案记录），忽略OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_blnIsFromDataSource = true;//表明是从数据库读取
            clsPatient m_objPatient = p_objPatient;
            m_objPrintInfo = new clsPrintInfo_FuqiangOperationyes_xj();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";

            m_mthGetPrintMarkConfig();
        }

        /// <summary>
        /// 获取打印修改痕迹设置
        /// </summary>
        private void m_mthGetPrintMarkConfig()
        {
            int intConfig = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3012");
            if (intConfig == 0)
            {
                m_blnIsPrintMark = false;
            }
            else
            {
                m_blnIsPrintMark = true;
            }
        }

        /// <summary>
        /// 从数据库初始化打印内容。如果没有记录，打印空报表。(当从数据库读取时要调用.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            m_blnWantInit = false;//
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("调用m_mthInitPrintContent之前请首先调用m_mthSetPrintInfo函数");
                return;
            }
            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
                m_objRecordContent = null;
            else
            {
                m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.FuqiangOperationyes_xj);
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                if (lngRes <= 0)
                    return;
                m_objRecordContent = (clsFuqiangOperationyesContent_xj)objContent;
            }
            //if (m_objRecordContent != null)
            //    m_objRecordContent.m_dtmOutHospitalDate = m_dtmOutDate;
            //设置表单内容到打印中			
            m_objPrintInfo.m_objRecordContent = m_objRecordContent;
            m_mthSetPrintValue();//无论有否打印数据,即使在打印空白表时,此行也必须执行.			
        }

        public void m_mthSetOutDateValue(DateTime p_dtmOutDate)
        {
            m_dtmOutDate = p_dtmOutDate;
        }

        /// <summary>
        /// 设置打印内容。(当数据已经存在时使用。)
        /// </summary>
        /// <param name="p_objPrintContent">打印内容</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {


            m_blnWantInit = false;
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_FuqiangOperationyes_xj")
            {
                clsPublicFunction.ShowInformationMessageBox("参数错误");
                return;
            }
            m_blnIsFromDataSource = false;//表明是从文件直接提取信息
            m_objPrintInfo = (clsPrintInfo_FuqiangOperationyes_xj)p_objPrintContent;
            m_objRecordContent = m_objPrintInfo.m_objRecordContent;
            m_mthSetPrintValue();
        }

        /// <summary>
        /// 获取打印内容,(当从数据库读取时,调用本函数之前请首先调用m_mthSetPrintInfo函数)
        /// </summary>
        /// <returns>打印内容</returns>
        public object m_objGetPrintInfo()
        {
            if (m_blnIsFromDataSource)
            {
                if (m_objPrintInfo == null)
                {
                    clsPublicFunction.ShowInformationMessageBox("当从数据库读取时,调用m_objGetPrintInfo之前请首先调用m_mthSetPrintInfo函数");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

            return m_objPrintInfo;
        }

        /// <summary>
        /// 初始化打印变量,本例传入空对象即可.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            #region 有关打印初始化
            m_fotTitleFont = new Font("SimSun", 16, FontStyle.Bold);
            m_fotHeaderFont = new Font("SimSun", 18, FontStyle.Bold);
            m_fotSmallFont = new Font("SimSun", 11);
            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);
            m_objPageSetting = new clsPrintPageSettingForRecord();

            #endregion
        }

        /// <summary>
        /// 释放打印变量
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_fotTitleFont.Dispose();
            m_fotHeaderFont.Dispose();
            m_fotSmallFont.Dispose();
            m_GridPen.Dispose();
            m_slbBrush.Dispose();
        }

        /// <summary>
        /// 打印开始
        /// </summary>
        /// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintEventArgs类型的对象</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// 打印中
        /// </summary>
        /// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintPageEventArgs类型的对象</param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// 打印结束。一般使用它来更新数据库信息。
        /// </summary>
        /// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintEventArgs类型的对象</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_objRecordContent == null) return;
            //如果打印成功，查找有无需要更新的时间，如果有，更新时间。 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
            {
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), dtmFirstPrintTime);//蔡沐忠改m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate);	
            }
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }

        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //缺省不做任何动作
        }
        // 打印页
        private void m_mthPrintPageSub(PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            m_mthPrintTitleInfo(e);
            Font fntNormal = new Font("SimSun", 12);

            if (m_intPages == 1)
            {
                m_intYPos += (int)enmRectangleInfo.RowStep - 20;
            }

            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY);

            while (m_objPrintContext.m_BlnHaveMoreLine)
            {
                m_objPrintContext.m_mthPrintNextLine(ref m_intYPos, e.Graphics, fntNormal);

                if (m_intYPos >= (int)enmRectangleInfo.BottomY
                    && m_objPrintContext.m_BlnHaveMoreLine)
                {
                    #region 换页处理
                    e.HasMorePages = true;

                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.LeftX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

                    m_intPages++;
                    m_intYPos = (int)enmRectangleInfo.TopY + 10;

                    clsPrintLine2.m_blnSinglePage = false;
                    return;

                    #endregion 换页处理
                }

            }

            #region 最后一页处理
          //  m_intYPos += 20;

            string strRecordName = "                 ";
            string strGuanChangName = "";
            string strZhuRenName = "";
            if (m_objRecordContent != null)
            {
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO = null;
                objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strRecordID, out objEmpVO);
                if (objEmpVO != null)
                    strRecordName = objEmpVO.ToString();
              
            }
            e.Graphics.DrawString("主管医师签字:" + strRecordName , new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 5, m_intYPos);
            e.Graphics.DrawString( m_objRecordContent.m_dtmDoctorDate.ToString("yyyy-MM-dd"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 5, m_intYPos+20);

            e.Graphics.DrawString("患者本人签字:" , new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 290, m_intYPos);
            e.Graphics.DrawString("患者亲属代签:" + "              " + "与患者关系：             ", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 290, m_intYPos + 20);
            e.Graphics.DrawString("患者单位主要负责人签字:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 290, m_intYPos + 40);
            e.Graphics.DrawString(m_objRecordContent.m_dtmHuanzheDate.ToString("yyyy-MM-dd"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 290, m_intYPos + 60);
            //			m_intYPos+=25;
            //			e.Graphics.DrawString("工　　号:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560,m_intYPos);
            //			if(m_objRecordContent!=null)
            //				e.Graphics.DrawString(m_objRecordContent.m_strDoctorID,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560+(int)(5f*17.5f),m_intYPos);
            /////////////////////////////////*******************************************
            m_intYPos += 50;
            if (m_intYPos < (int)enmRectangleInfo.BottomY)
                m_intYPos = (int)enmRectangleInfo.BottomY;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.LeftX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

            #endregion 最后一页处理

            m_intYPos += (int)enmRectangleInfo.RowStep + 15;
            Font fntSign = new Font("", 6);
            while (m_objPrintContext.m_BlnHaveMoreSign)
            {
                m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX, m_intYPos, e.Graphics, fntSign);

                m_intYPos += (int)enmRectangleInfo.RowStep - 10;
            }

            //全部打完
            m_objPrintContext.m_mthReset();
            m_intPages = 1;
            m_intYPos = (int)enmRectangleInfo.TopY;
        }

        // 打印结束时的操作
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            clsPrintLine2.m_blnSinglePage = true;
        }


        #region 打印
        #region 有关打印的声明

        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintContext;
        /// <summary>
        /// 标题的字体
        /// </summary>
        private Font m_fotTitleFont;
        /// <summary>
        /// 表头的字体
        /// </summary>
        private Font m_fotHeaderFont;
        /// <summary>
        /// 表内容的字体
        /// </summary>
        private Font m_fotSmallFont;
        /// <summary>
        /// 边框画笔
        /// </summary>
        private Pen m_GridPen;
        /// <summary>
        /// 刷子
        /// </summary>
        private SolidBrush m_slbBrush;
        /// <summary>
        /// 获取坐标的类
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;
        /// <summary>
        /// 打印的病人基本信息类
        /// </summary>
        /// 
        private int m_intYPos = (int)enmRectangleInfo.TopY + 5;
        private int m_intPages = 1;

        /// <summary>
        /// 格子的信息
        /// </summary>
        public enum enmRectangleInfo
        {
            /// <summary>
            /// 格子的顶端TopY = 120,
            /// </summary>
            TopY = 160,
            ///<summary>
            /// 格子的左端
            /// </summary>
            LeftX = 40,
            /// <summary>
            /// 格子的右端
            /// </summary>
            RightX = 820 - 40,
            /// <summary>
            /// 格子每行的步长
            /// </summary>
            RowStep = 20,
            SmallRowStep = 20,

            ColumnsMark1 = 110,

            /// <summary>
            /// 底划线偏移文本顶点的距离
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1024,

            PrintWidth = 670,
            PrintWidth2 = 710
        }

        /// <summary>
        /// 打印元素
        /// </summary>
        private enum enmItemDefination
        {
            //基本元素
            Area_Title,
            Area,
            Bed_Title,
            Bed,
            InPatientID_Title,
            InPatientID,
            Name_Title,
            Name,
            Sex_Title,
            Sex,
            Age_Title,
            Age,
            YiBao_title,
            YiBao,
            Number,
            //HeartID_Title,
            //HeartID,
            //XRayID_Title,
            //XRayID,

            Page_HospitalName,
            Page_Name_Title,
            Page_Title,
            Page_Num,
            Page_Of,
            Page_Count,

            Print_Date_Title,
            Print_Date,

        }


        #region 定义打印各元素的坐标点
        private class clsPrintPageSettingForRecord
        {
            public clsPrintPageSettingForRecord() { }

            /// <summary>
            /// 获得坐标点
            /// </summary>
            /// <param name="p_intItemName">项目名称</param>
            /// <returns></returns>
            public PointF m_getCoordinatePoint(int p_intItemName)
            {
                PointF m_fReturnPoint;
                switch (p_intItemName)
                {
                    case (int)enmItemDefination.Area:
                        m_fReturnPoint = new PointF(370f, 140f);
                        break;
                    case (int)enmItemDefination.Area_Title:
                        m_fReturnPoint = new PointF(330f, 140f);
                        break;
                    case (int)enmItemDefination.Bed:
                        m_fReturnPoint = new PointF(510f, 137f);
                        break;
                    case (int)enmItemDefination.Bed_Title:
                        m_fReturnPoint = new PointF(470f, 140f);
                        break;
                    case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(320f, 70f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(290f, 100f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(30f, 140f);
                        break;
                    case (int)enmItemDefination.Number:
                        m_fReturnPoint = new PointF(35f, 50f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(75f, 140f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(155f, 140f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(200f, 140f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(225f, 140f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(270f, 140f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(540f, 140f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(600f, 140f);
                        break;

                    case (int)enmItemDefination.YiBao_title:
                        m_fReturnPoint = new PointF(645f, 140f);
                        break;
                    case (int)enmItemDefination.YiBao:
                        m_fReturnPoint = new PointF(700f, 140f);
                        break;

                    //case (int)enmItemDefination.HeartID_Title:
                    //    m_fReturnPoint = new PointF(60f, 155f);
                    //    break;
                    //case (int)enmItemDefination.HeartID:
                    //    m_fReturnPoint = new PointF(145f, 153f);
                    //    break;

                    //case (int)enmItemDefination.XRayID_Title:
                    //    m_fReturnPoint = new PointF(220f, 155f);
                    //    break;
                    //case (int)enmItemDefination.XRayID:
                    //    m_fReturnPoint = new PointF(280f, 153f);
                    //    break;

                    default:
                        m_fReturnPoint = new PointF(400f, 440f);
                        break;

                }

                return m_fReturnPoint;
            }
        }

        #endregion
        #endregion

        #region 打印行定义
        private clsPrintLine1[] m_objLine1Arr;
        private clsPrintLine2[] m_objLine2Arr;
        #endregion

        private DateTime dtmFirstPrintTime;
        /// <summary>
        /// 给每一打印行的元素赋值
        /// </summary>
        private void m_mthSetPrintValue()
        {
            #region  第一次打印时间赋值
            dtmFirstPrintTime = DateTime.Now;
            if (m_objRecordContent != null && m_objRecordContent.m_dtmFirstPrintDate != DateTime.MinValue)
                dtmFirstPrintTime = m_objRecordContent.m_dtmFirstPrintDate;
            #endregion  第一次打印时间赋值

            #region 打印行初始化
            m_objLine1Arr = new clsPrintLine1[6];
            m_objLine2Arr = new clsPrintLine2[1];
            for (int i = 0; i < m_objLine1Arr.Length; i++)
                m_objLine1Arr[i] = new clsPrintLine1();


            m_objLine2Arr[0] = new clsPrintLine2(20);
            //m_objLine2Arr[1] = new clsPrintLine2(400);
            //m_objLine2Arr[2] = new clsPrintLine2(560);
            ////     m_objLine2Arr[3] = new clsPrintLine2(790);
            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										 m_objLine1Arr[1],m_objLine1Arr[3], m_objLine1Arr[4],m_objLine1Arr[2],m_objLine1Arr[5],
                                         m_objLine2Arr[0]
									  });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region 给每一行的元素赋值
            string strBlanks = "　　　　　　　　　　　　";
            if (m_objRecordContent != null)
            {
                ///////////////1行/////////////////
                string strOutDate = "";
                //if (m_objRecordContent.m_dtmOutHospitalDate != DateTime.MinValue
                //    && m_objRecordContent.m_dtmOutHospitalDate != new DateTime(1900, 1, 1))
                //    strOutDate = m_objRecordContent.m_dtmOutHospitalDate.ToString("yyyy年MM月dd日");
                Object[] objData1 = new object[6];

                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "入院日期: " + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy年MM月dd日") + "　　　　　　　　　　　" + "手术时间: " + m_objRecordContent.m_dtmDiscussDate.ToString("yyyy年MM月dd日");
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////2行/////////////////
                objData1[0] = "    ";
                foreach (string str in m_objRecordContent.m_strAttendeeIDArr)
                {
                    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                    clsEmrEmployeeBase_VO objEmpVO = null;
                    objEmployeeSign.m_lngGetEmpByNO(str, out objEmpVO);
                    //if (objEmpVO != null)
                    //    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                    //        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);

                    objData1[0] += objEmpVO.m_strGetTechnicalRankAndName + "　";
                }
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "手术者：";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;
                //////////////////////////////

                //////////////////////////////
                //objData1[0] = "    ";
                //com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign2 = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                //clsEmrEmployeeBase_VO objEmpVO2 = null;
                //objEmployeeSign2.m_lngGetEmpByNO(m_objRecordContent.m_strHuiBaoID, out objEmpVO2);
                ////if (objEmpVO != null)
                ////    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                ////        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);

                //objData1[0] += objEmpVO2.m_strGetTechnicalRankAndName + "　";
                //objData1[1] = "";
                //objData1[2] = dtmFirstPrintTime;
                //objData1[3] = "病史汇报者：";
                //m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;


                ///////////////3行/////////////////
                objData1[0] = m_objRecordContent.m_strShuQian;
                objData1[1] = m_objRecordContent.m_strShuQianXML;
                objData1[3] = "术前诊断:";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                /////////////////////////////////////
                objData1[0] = m_objRecordContent.m_strNiShi;
                objData1[1] = m_objRecordContent.m_strNiShiXML;
                objData1[3] = "拟施手术:";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;

                /////////////////////////////////////
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign2 = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO2 = null;
                objEmployeeSign2.m_lngGetEmpByNO(m_objRecordContent.m_strCompereID, out objEmpVO2);

                objData1[0] = m_objRecordContent.m_strMaZui + "                                               \n  麻醉医师" + objEmpVO2.m_strGetTechnicalRankAndName;
                objData1[1] = "";
                objData1[3] = "麻醉方式:";
                m_objLine1Arr[5].m_ObjPrintLineInfo = objData1;


                ///////////////4行/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////5行/////////////////
                objData1[0] = " 1、麻醉意外；" + "\n 2、造气腹和穿刺套管针所致并发症：" + "\n （1）皮下气肿，气胸，纵隔气肿，气体栓塞等；" + "\n （2）误伤腹腔内器官；" + "\n （3）血管损伤；腹壁血管、腹膜后大血管几肠系膜血管等；" + "\n （4）高碳酸血症；" + 
                               "\n 3、术中操作所导致并发症：" + "\n （1）术中大出血、严重者乃至死亡；" + "\n （2）误伤病变部位周围器官（实、空腔脏器）。术中发现异常情况，如造气腹失败、病变为肿瘤、大血管损伤及脏器损伤、腹内粘连严重需要中转开腹；" + "\n （3）术中由于局部粘连，或水肿较甚，操作中勿伤胆总管，则行胆总管控查，T管引流术，并有可能长期置管，半年---1年；" +
                               "\n 4、术后并发症：" + "\n （1）操作孔感染；" + "\n （2）术后腹腔内大出血，必要时输血；" + "\n （3）胆漏，肠漏等；" + "\n （4）粘连性肠梗阻；" + "\n （5）术后肩部酸胀不适等；" + "\n 5、其他：" + "\n 以上并发症均可在术中、术后发生，严重者可危急生命，甚至死亡，如出现上述并发症，望患者及家属予以谅解，协助治疗。如同意手术，请签字，以此为据。";
                objData1[1] = "";
                objData1[3] = "术中可能发生以下并发症，特向本人及家属说明:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;

                //objData1[0] = m_objRecordContent.m_strTaoLunYiJian;
                //objData1[1] = m_objRecordContent.m_strTaoLunYiJianXML;
                //objData1[3] = "讨论意见:";
                //m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
                /////////////////8行/////////////////			
                //objData1[0] = m_objRecordContent.m_strTaoLunXiaoJie;
                //objData1[1] = m_objRecordContent.m_strTaoLunXiaoJieXML;
                //objData1[3] = "讨论小结:";
                //m_objLine2Arr[2].m_ObjPrintLineInfo = objData1;

            }
            else
            {
                ///////////////1行/////////////////
                Object[] objData1 = new object[5];
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[2] = dtmFirstPrintTime;
                //if (m_objPrintInfo.m_dtmHISInDate != DateTime.MinValue)
                //{
                //    objData1[3] = "入院日期:" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy年MM月dd日") + "　　　　　　　　　　　" + "出院日期:" + "    年  月  日";
                //}
                //else
                //{
                //    objData1[3] = "入院日期:" + "    年  月  日" + "　　　　　　　　　　　" + "出院日期:" + "    年  月  日";
                //}
                //m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////2行/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "入院日期:";
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "手术者:";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;

                ///////////////3行/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "术前诊断:";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                //////////////////////////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "拟施手术:";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;
                //////////////////////////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "麻醉方式:";
                m_objLine1Arr[5].m_ObjPrintLineInfo = objData1;
                ///////////////4行/////////////////	
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                //   objData1[3] = "  心电图号:" + strBlanks + "X光号:" + strBlanks;// +"主治医师:";
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;
                ///////////////5行/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "说明:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;
                ///////////////6行/////////////////
               
             
            }

            #endregion
        }


        #region 标题文字部分
        /// <summary>
        /// 标题文字部分
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("腹腔镜手术同意书", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

            e.Graphics.DrawString("XHTCM/RD-1032", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Number));

            e.Graphics.DrawString("姓名:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));

            if (m_objPrintInfo.m_strPatientName.Length > 5)
            {
                RectangleF rtgf1 = new RectangleF((int)enmRectangleInfo.LeftX + 36, (int)enmRectangleInfo.TopY - 29, 85, 35);
                StringFormat frmat = new StringFormat();

                e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, new Font("", 8), m_slbBrush, rtgf1, frmat);
            }
            else
            {
                e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));
            }      
                    

            e.Graphics.DrawString("性别:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("年龄:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("病区:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area));

            e.Graphics.DrawString("床号:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed));

            e.Graphics.DrawString("住院号:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));

            e.Graphics.DrawString("医保号:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.YiBao_title));
            e.Graphics.DrawString(m_objPrintInfo.m_objRecordContent.m_strYiBao, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.YiBao));
 
        }


        #endregion

        #region print class

     

        private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private bool m_blnIsFirstPrint = true;
            private string m_strTitle;

            /// <summary>
            /// 默认值
            /// </summary>
            private readonly int c_intTop4 = 20;

            internal static bool m_blnSinglePage = true;

            public clsPrintLine1()
            {
                // c_intTop4 = intTop4;
                m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (p_intPosY < c_intTop4 && m_blnSinglePage)
                        p_intPosY = c_intTop4;
                    p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 5, p_intPosY);
                    p_intPosY += 20;
                    m_blnIsFirstPrint = false;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                {
                    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 45, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                m_objDiagnose.m_mthRestartPrint();
            }

            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return m_objPrintLineInfo;
                }
                set
                {
                    if (value != null)
                    {
                        Object[] objData = (object[])value;
                        m_strTitle = objData[3].ToString();
                        dtmFirstPrint = (DateTime)objData[2];
                        if (clsDifficultCaseDiscuss_XJPrintTool.m_blnIsPrintMark)
                        {
                            if (objData[1].ToString() == "")
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
                            }
                            else
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
                                m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
                            }
                        }
                        else
                        {
                            m_objDiagnose.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                        }
                        if (m_objDiagnose.m_ObjModifyUserArr != null)
                        {
                            for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
                            {
                                if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                    m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 2
        private class clsPrintLine2 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private bool m_blnIsFirstPrint = true;
            private string m_strTitle;

            /// <summary>
            /// 默认值
            /// </summary>
            private readonly int c_intTop4 = 0;

            internal static bool m_blnSinglePage = true;

            public clsPrintLine2(int intTop4)
            {
                c_intTop4 = intTop4;
                m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (p_intPosY < c_intTop4 && m_blnSinglePage)
                        p_intPosY = c_intTop4;
                    p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 5, p_intPosY);
                    p_intPosY += 20;
                    m_blnIsFirstPrint = false;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                {
                    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 45, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                m_objDiagnose.m_mthRestartPrint();
            }

            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return m_objPrintLineInfo;
                }
                set
                {
                    if (value != null)
                    {
                        Object[] objData = (object[])value;
                        m_strTitle = objData[3].ToString();
                        dtmFirstPrint = (DateTime)objData[2];
                        if (clsDifficultCaseDiscuss_XJPrintTool.m_blnIsPrintMark)
                        {
                            if (objData[1].ToString() == "")
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
                            }
                            else
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
                                m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
                            }
                        }
                        else
                        {
                            m_objDiagnose.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                        }
                        if (m_objDiagnose.m_ObjModifyUserArr != null)
                        {
                            for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
                            {
                                if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                    m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
                            }
                        }
                    }
                }
            }
        }
        #endregion 2
        #endregion
    }
}


