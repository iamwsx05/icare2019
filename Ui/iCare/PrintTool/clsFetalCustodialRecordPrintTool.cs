using System;
using weCare.Core.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ̥�����Ӽ໤��¼�Ĵ�ӡ������,haozhong.liu-2007-7-27
	/// </summary>
    public class clsFetalCustodialRecordPrintTool : infPrintRecord
	{

        private class clsFetalCustodialRecordPrintInfo
        {
            public DateTime m_dtmHISInDate;
            public DateTime m_dtmInPatientDate;
            public DateTime m_dtmOpenDate;
            public clsFetalCustodialRecordContent m_objRecordContent;
            public string m_strAge;
            public string m_strAreaName;
            public string m_strBedName;
            public string m_strDeptName;
            public string m_strHISInPatientID;
            public string m_strInPatentID;
            public string m_strPatientName;
            public string m_strOutPatientID;
            public string m_strSex;
        }

		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        private DateTime m_dtmOutDate = DateTime.MinValue;

        private clsFetalCustodialRecordPrintInfo m_objPrintInfo = null;
        private clsFetalCustodialRecordDomain m_objRecordsDomain = null;
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient, clsFetalCustodialRecordContent p_objContent,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{	
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
            m_objPrintInfo = new clsFetalCustodialRecordPrintInfo();
            m_objPrintInfo.m_objRecordContent = p_objContent;
            m_objPrintInfo.m_strAreaName = p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName;            
            m_objPrintInfo.m_strInPatentID = p_objPatient != null ? p_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strInPatentID = p_objPatient != null ? p_objPatient.m_Str_OutPatientID : "";
            m_objPrintInfo.m_strPatientName = p_objPatient != null ? p_objPatient.m_StrName : "";
            m_objPrintInfo.m_strAge = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strSex = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_dtmHISInDate = p_objPatient != null ? p_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = p_objPatient != null ? p_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_strBedName = p_objPatient != null ? p_objPatient.m_strBedCode : "";
            m_mthGetPrintMarkConfig();
		}


        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            //m_objPatient = p_objPatient;

            m_mthGetPrintMarkConfig();
        }

        /// <summary>
        /// ��ȡ��ӡ�޸ĺۼ�����
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
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{
            m_blnWantInit = false;//
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
            {
            }
            else
            {
                m_objRecordsDomain = new clsFetalCustodialRecordDomain();
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                //long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                //if (lngRes <= 0)
                //    return;
                //m_objRecordContent = (clsOutHospitalRecordContent)objContent;
            }            
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

        public void m_mthSetOutDateValue(DateTime p_dtmOutDate)
        {
            m_dtmOutDate = p_dtmOutDate;
        }

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			 		 

			m_blnWantInit=false;
            if (p_objPrintContent.GetType().Name != "clsFetalCustodialRecordPrintInfo")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
				return;
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo=(clsFetalCustodialRecordPrintInfo)p_objPrintContent;            		
			m_mthSetPrintValue();			
		}

		/// <summary>
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{	
			if(m_blnIsFromDataSource )
			{
                //if(m_objPrintInfo==null)
                //{
                //    clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                //    return null;
                //}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			
			
			return null;
		}		

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			#region �йش�ӡ��ʼ��
			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",11);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
            m_mthInitCoordinatePoint();
			#endregion
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont .Dispose();
			m_GridPen .Dispose();
			m_slbBrush .Dispose();
		}

		/// <summary>
		/// ��ӡ��ʼ
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{			
			m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
		public void m_mthPrintPage(object p_objPrintArg)
		{
			m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthEndPrint(object p_objPrintArg)
		{
            if (m_blnIsFromDataSource == false || m_objPrintInfo == null || m_objPrintInfo.m_strInPatentID == "") return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
			{				
				m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),dtmFirstPrintTime);//�����Ҹ�m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate);	
			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}

		private  void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ���
		}
		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs e)
		{
			e.HasMorePages =false;
			m_mthPrintTitleInfo(e);
			Font fntNormal = new Font("SimSun",12);

			if(m_intPages==1)
			{				
				m_intYPos += (int)enmRectangleInfo.RowStep-20;						
			}
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY);
			
			while(m_objPrintContext.m_BlnHaveMoreLine)
			{
				m_objPrintContext.m_mthPrintNextLine(ref m_intYPos,e.Graphics,fntNormal);

				if(m_intYPos >=(int)enmRectangleInfo.BottomY 
					&& m_objPrintContext.m_BlnHaveMoreLine)
				{
					#region ��ҳ����
					e.HasMorePages = true;					
				
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos,(int)enmRectangleInfo.RightX,m_intYPos);
			
					m_intPages++;
                    m_intYPos = (int)enmRectangleInfo.TopY;

                    clsPrintLine2.m_blnSinglePage = false;
					return;

					#endregion ��ҳ���� 
				}				
				
			}

			#region ���һҳ����
			m_intYPos+=30;            
			
			m_intYPos+=25;
			if(m_intYPos< (int)enmRectangleInfo.BottomY)
				m_intYPos=(int)enmRectangleInfo.BottomY;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos,(int)enmRectangleInfo.RightX,m_intYPos);
			
			#endregion ���һҳ����

			m_intYPos += (int)enmRectangleInfo.RowStep+15;
			Font fntSign = new Font("",6);
			while(m_objPrintContext.m_BlnHaveMoreSign)
			{
				m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX,m_intYPos,e.Graphics,fntSign);

				m_intYPos += (int)enmRectangleInfo.RowStep-10;				
			}

			//ȫ������
			m_objPrintContext.m_mthReset();
			m_intPages=1;			
			m_intYPos = (int)enmRectangleInfo.TopY;			
		}

		// ��ӡ����ʱ�Ĳ���
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            clsPrintLine2.m_blnSinglePage = true;
		}		
		

		#region ��ӡ
		#region �йش�ӡ������
		
		private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintContext;		
		/// <summary>
		/// ���������
		/// </summary>
		private Font m_fotTitleFont;
		/// <summary>
		/// ��ͷ������
		/// </summary>
		private Font m_fotHeaderFont;
		/// <summary>
		/// �����ݵ�����
		/// </summary>
		private Font m_fotSmallFont;
		/// <summary>
		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;
		/// <summary>
		/// ˢ��
		/// </summary>
		private SolidBrush m_slbBrush;

		/// <summary>
		/// ��ӡ�Ĳ��˻�����Ϣ��
		/// </summary>
		/// 
		private int m_intYPos = (int)enmRectangleInfo.TopY+5;
		private int m_intPages=1;		

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		public enum enmRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 120,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 40,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-40,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 20,
			SmallRowStep=20,			

			ColumnsMark1=110,			

			/// <summary>
			/// �׻���ƫ���ı�����ľ���
			/// </summary>
			BottomLineShift=15,

			BottomY=1024,
	
			PrintWidth = 670,
			PrintWidth2 = 710
		}
		
		  
	
		#region �����ӡ��Ԫ�ص������

        private Dictionary<string, PointF> CoordinatePoint = new Dictionary<string, PointF>();
        private void m_mthInitCoordinatePoint()
        {
            CoordinatePoint.Add("HospitalName", new PointF(310f, 30f));
            CoordinatePoint.Add("Page_Name_Title", new PointF(310f, 60f));
            CoordinatePoint.Add("Name_Title", new PointF(40f, 100f));
            CoordinatePoint.Add("Name", new PointF(80f, 100f));
            
            CoordinatePoint.Add("Sex_Title", new PointF(180f, 100f));
            CoordinatePoint.Add("Sex", new PointF(230f, 100f));

            CoordinatePoint.Add("Age_Title", new PointF(300f,100f));
            CoordinatePoint.Add("Age", new PointF(350f,100f));

            CoordinatePoint.Add("Dep_Title", new PointF(400f, 100f));
            CoordinatePoint.Add("Dep", new PointF(450f, 100f));

            CoordinatePoint.Add("Bed_Title", new PointF(560f, 100f));
            CoordinatePoint.Add("Bed", new PointF(610f, 100f));

            CoordinatePoint.Add("InPatientID_Title", new PointF(650f, 100f));
            CoordinatePoint.Add("InPatientID", new PointF(710f, 100f));

        }		

	    #endregion
		#endregion
		
		#region ��ӡ�ж���
        private clsPrintLine1 m_objLine1;
        private clsPrintLine2 m_objLine2;	
		#endregion
		
		private DateTime dtmFirstPrintTime;
		/// <summary>
		/// ��ÿһ��ӡ�е�Ԫ�ظ�ֵ
		/// </summary>
		private void m_mthSetPrintValue()
		{		
			#region  ��һ�δ�ӡʱ�丳ֵ			
            dtmFirstPrintTime=DateTime.Now;
            if (m_objPrintInfo.m_objRecordContent != null && m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate != DateTime.MinValue)
                dtmFirstPrintTime = m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate;
			#endregion  ��һ�δ�ӡʱ�丳ֵ

			#region ��ӡ�г�ʼ��
            m_objLine1 = new clsPrintLine1();
            m_objLine2 = new clsPrintLine2();
            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                          m_objLine1,
                                          m_objLine2
                                      });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region ��ÿһ�е�Ԫ�ظ�ֵ
            string strBlanks = "������������������������";
            if (m_objPrintInfo != null)
            {
                ///////////////1��/////////////////
                Object[] objData1 = new object[30];
                objData1[0] = m_objPrintInfo.m_objRecordContent.m_strClinicalDiagnose;
                objData1[1] = m_objPrintInfo.m_objRecordContent.m_strClinicalDiagnoseXml;
                objData1[2] = m_objPrintInfo.m_objRecordContent.m_strCustodialIndication;
                objData1[3] = m_objPrintInfo.m_objRecordContent.m_strCustodialIndicationXml;
                objData1[4] = m_objPrintInfo.m_objRecordContent.m_strUltraSonicscanCue;
                objData1[5] = m_objPrintInfo.m_objRecordContent.m_strUltraSonicscanCueXml;
                objData1[6] = m_objPrintInfo.m_objRecordContent.m_strUltraSonicscanerType;
                objData1[7] = m_objPrintInfo.m_objRecordContent.m_strUltraSonicscanerTypeXml;
                objData1[8] = m_objPrintInfo.m_objRecordContent.m_strFetalHeartRate;
                objData1[9] = m_objPrintInfo.m_objRecordContent.m_strAmplitudeVariation;
                objData1[10] = m_objPrintInfo.m_objRecordContent.m_strPeriodicVariation;
                objData1[11] = m_objPrintInfo.m_objRecordContent.m_strAccerleration;
                objData1[12] = m_objPrintInfo.m_objRecordContent.m_strDecerleration;
                objData1[13] = m_objPrintInfo.m_objRecordContent.m_strTotalRate;
                objData1[14] = m_objPrintInfo.m_objRecordContent.m_strManagementSuggestion;
                objData1[15] = m_objPrintInfo.m_objRecordContent.m_strManagementSuggestionXml;
                objData1[16] = m_objPrintInfo.m_objRecordContent.m_strOCT;
                objData1[17] = m_objPrintInfo.m_objRecordContent.m_strOCTXml;
                objData1[18] = m_objPrintInfo.m_objRecordContent.m_strCSF;
                objData1[19] = m_objPrintInfo.m_objRecordContent.m_strCSFXml;
                objData1[20] = m_objPrintInfo.m_objRecordContent.m_strCustodialRecord;                
                objData1[21] = m_objPrintInfo.m_objRecordContent.m_strCustodialRecordXml;                
                objData1[22] = m_objPrintInfo.m_objRecordContent.m_strAfterParturientHour;
                objData1[23] = m_objPrintInfo.m_objRecordContent.m_strAfterParturientHourXml;
                objData1[24] = m_objPrintInfo.m_objRecordContent.m_strAfterParturientMinute;
                objData1[25] = m_objPrintInfo.m_objRecordContent.m_strAfterParturientMinuteXml;
                objData1[26] = m_objPrintInfo.m_objRecordContent.m_strOstiumUteri;
                objData1[27] = m_objPrintInfo.m_objRecordContent.m_strOstiumUteriXml;
                objData1[28] = m_objPrintInfo.m_objRecordContent.m_strSignName1;
                objData1[29] = m_objPrintInfo.m_objRecordContent.m_dtmSignTime1;
                m_objLine1.m_ObjPrintLineInfo = objData1;                
                m_objLine1.m_objPrintInfo = m_objPrintInfo;

                Object[] objData2 = new object[24];
                objData2[0] = m_objPrintInfo.m_objRecordContent.m_strNatalType;
                objData2[1] = m_objPrintInfo.m_objRecordContent.m_strNatalTypeXml;
                objData2[2] = m_objPrintInfo.m_objRecordContent.m_strBirthProcessHour;
                objData2[3] = m_objPrintInfo.m_objRecordContent.m_strBirthProcessHourXml;
                objData2[4] = m_objPrintInfo.m_objRecordContent.m_strBirthProcessMinute;
                objData2[5] = m_objPrintInfo.m_objRecordContent.m_strBirthProcessMinuteXml;
                objData2[6] = m_objPrintInfo.m_objRecordContent.m_strEvaluation;
                objData2[7] = m_objPrintInfo.m_objRecordContent.m_strEvaluationXml;
                objData2[8] = m_objPrintInfo.m_objRecordContent.m_strFetalWeight;
                objData2[9] = m_objPrintInfo.m_objRecordContent.m_strFetalWeightXml;
                objData2[10] = m_objPrintInfo.m_objRecordContent.m_strFetalLength;
                objData2[11] = m_objPrintInfo.m_objRecordContent.m_strFetalLengthXml;
                objData2[12] = m_objPrintInfo.m_objRecordContent.m_strAmnioticFluid;
                objData2[13] = m_objPrintInfo.m_objRecordContent.m_strAmnioticFluidXml;
                objData2[14] = m_objPrintInfo.m_objRecordContent.m_strColor;
                objData2[15] = m_objPrintInfo.m_objRecordContent.m_strColorXml;
                objData2[16] = m_objPrintInfo.m_objRecordContent.m_strPlacenta;
                objData2[17] = m_objPrintInfo.m_objRecordContent.m_strPlacentaXml;
                objData2[18] = m_objPrintInfo.m_objRecordContent.m_strUmbilicalcord;
                objData2[19] = m_objPrintInfo.m_objRecordContent.m_strUmbilicalcordXml;
                objData2[20] = m_objPrintInfo.m_objRecordContent.m_strRemark;
                objData2[21] = m_objPrintInfo.m_objRecordContent.m_strRemarkXml;
                objData2[22] = m_objPrintInfo.m_objRecordContent.m_strSignName2;
                objData2[23] = m_objPrintInfo.m_objRecordContent.m_dtmSignTime2;
                m_objLine2.m_ObjPrintLineInfo = objData2;
                m_objLine2.m_objPrintInfo = m_objPrintInfo;
            }
            else
            {
                ///////////////1��/////////////////
                Object[] objData1 = new object[200];
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = "";
                objData1[3] = "";
                objData1[4] = "";
                objData1[5] = "";
                objData1[6] = "";
                objData1[7] = "";
                objData1[8] = "";
                objData1[9] = "";
                objData1[10] = "";
                objData1[11] = "";
                objData1[12] = "";
                objData1[13] = "";
                objData1[14] = "";
                objData1[15] = "";
                objData1[16] = "";
                objData1[17] = "";
                objData1[18] = "";
                objData1[19] = "";
                objData1[20] = "";
                objData1[21] = "";
                objData1[22] = "";
                objData1[23] = "";
                objData1[24] = "";
                objData1[25] = "";
                objData1[26] = "";
                objData1[27] = "";
                objData1[28] = "";
                objData1[29] = "";
                objData1[30] = "";                
                m_objLine1.m_ObjPrintLineInfo = objData1;               

                Object[] objData2 = new object[24];
                objData2[0] = "";
                objData2[1] = "";
                objData2[2] = "";
                objData2[3] = "";
                objData2[4] = "";
                objData2[5] = "";
                objData2[6] = "";
                objData2[7] = "";
                objData2[8] = "";
                objData2[9] = "";
                objData2[10] = "";
                objData2[11] = "";
                objData2[12] = "";
                objData2[13] = "";
                objData2[14] = "";
                objData2[15] = "";
                objData2[16] = "";
                objData2[17] = "";
                objData2[18] = "";
                objData2[19] = "";
                objData2[20] = "";
                objData2[21] = "";
                objData2[22] = "";
                objData2[23] = "";
                m_objLine2.m_ObjPrintLineInfo = objData2;
                m_objLine2.m_objPrintInfo = m_objPrintInfo;

            }
			
			#endregion 
		}
				
		
		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotTitleFont, m_slbBrush, CoordinatePoint["HospitalName"]);

            e.Graphics.DrawString("̥�����Ӽ໤��¼��", m_fotTitleFont, m_slbBrush, CoordinatePoint["Page_Name_Title"]);
            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, CoordinatePoint["Name_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, CoordinatePoint["Name"]);

            e.Graphics.DrawString("�Ա�:", m_fotSmallFont, m_slbBrush, CoordinatePoint["Sex_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, CoordinatePoint["Sex"]);

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, CoordinatePoint["Age_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, CoordinatePoint["Age"]);

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, CoordinatePoint["Dep_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, CoordinatePoint["Dep"]);
            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, CoordinatePoint["Bed_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, CoordinatePoint["Bed"]);

            e.Graphics.DrawString("סԺ��:", m_fotSmallFont, m_slbBrush, CoordinatePoint["InPatientID_Title"]);
            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, CoordinatePoint["InPatientID"]);     
		}
		
	
		#endregion		
		
		#region print class 

		private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
		{
            private com.digitalwave.controls.clsPrintRichTextContext m_objText1;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText2;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText3;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText4;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText5;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText6;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText7;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText8;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText9;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText10;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText11;            
			private bool m_blnFirstPrint = true;
            public clsFetalCustodialRecordPrintInfo m_objPrintInfo = null;

			public clsPrintLine1()
			{
				m_objText1 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
                m_objText2 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText3 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText4 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText5 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText6 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText7 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText8 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText9 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText10 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText11 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("�ٴ���ϣ�",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX+3,p_intPosY);
                    p_objGrp.DrawString("����ţ�"+ m_objPrintInfo.m_strOutPatientID, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 560, p_intPosY);
                    p_objGrp.DrawString("�໤ָ����", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 20);
                    p_objGrp.DrawString("B����ʾ��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 40);                    
                    p_objGrp.DrawString("���͡����أ�", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 560, p_intPosY + 40);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 60, (int)enmRectangleInfo.RightX, p_intPosY + 60);
                    p_objGrp.DrawString("һ��NST������Fischer������", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 70);

                    p_objGrp.DrawString("����", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 160, p_intPosY + 95);
                    p_objGrp.DrawString("ָ��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 110);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 90, (int)enmRectangleInfo.RightX, p_intPosY + 90);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 90, (int)enmRectangleInfo.LeftX + 200, p_intPosY + 130);

                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 200, p_intPosY + 90, (int)enmRectangleInfo.LeftX + 200, p_intPosY + 330);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 340, p_intPosY + 90, (int)enmRectangleInfo.LeftX + 340, p_intPosY + 330);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 430, p_intPosY + 90, (int)enmRectangleInfo.LeftX + 430, p_intPosY + 330);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 550, p_intPosY + 90, (int)enmRectangleInfo.LeftX + 550, p_intPosY + 330);
                    p_objGrp.DrawString("                                0   ��        1  ��        2  ��       �ܼ��в����÷�", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 105);

                    p_objGrp.DrawString("100-119", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 355, p_intPosY + 135);
                    p_objGrp.DrawString("161-180", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 355, p_intPosY + 150);
                    p_objGrp.DrawString("5-10", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 370, p_intPosY + 175);
                    p_objGrp.DrawString(">30", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 370, p_intPosY + 190);


                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 130, (int)enmRectangleInfo.RightX, p_intPosY + 130);
                    p_objGrp.DrawString("    ̥���ʻ��ߣ�bpm��       < 99, > 181                  120��160", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 145);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 170, (int)enmRectangleInfo.RightX, p_intPosY + 170);
                    p_objGrp.DrawString("    ������죨bpm��            < 5                        10��30", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 185);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 210, (int)enmRectangleInfo.RightX, p_intPosY + 210);
                    p_objGrp.DrawString("    ���ڱ��죨bpm��            < 2            2��6          >7", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 225);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 250, (int)enmRectangleInfo.RightX, p_intPosY + 250);
                    p_objGrp.DrawString("    ���٣���/20���ӣ�           ��            1��4          >5", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 265);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 290, (int)enmRectangleInfo.RightX, p_intPosY + 290);
                    p_objGrp.DrawString("      ��     ��                 LD             VD       ��:typeo-dip", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 305);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 330, (int)enmRectangleInfo.RightX, p_intPosY + 330);
                    p_objGrp.DrawString("�ܼ��в����ܷ֣�      �֡�", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 350);
                    p_objGrp.DrawString("�����飺", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 370);
                    p_objGrp.DrawString("����OCT", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 470);
                    p_objGrp.DrawString("    CSF", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 490);
                    p_objGrp.DrawString("�໤��¼��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 150, p_intPosY + 490);
                    p_objGrp.DrawString("    �ٲ���        Сʱ       �֣� ����       cm��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 510);
                    p_objGrp.DrawString("ǩ����" + m_objPrintInfo.m_objRecordContent.m_strSignName1, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 450, p_intPosY + 530);                    
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 560, (int)enmRectangleInfo.RightX, p_intPosY + 560);

                    int intRealHeight;
                    Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 78, p_intPosY, 450, 20);
                    m_objText1.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 78, p_intPosY + 20, 450, 20);
                    m_objText2.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 78, p_intPosY + 40, 450, 20);
                    m_objText3.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 650, p_intPosY + 40, 90, 20);
                    //m_objText4.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    m_objText4.m_blnPrintInBlock("SimSun", 9, rtgBlock, p_objGrp, false, out intRealHeight, false, false);

                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strFetalHeartRate, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 145);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strAmplitudeVariation, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 185);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strPeriodicVariation, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 225);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strAccerleration, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 265);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strDecerleration, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 305);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strTotalRate, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 150, p_intPosY + 350);                    

                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 80, p_intPosY + 370, 655, 120);
                    m_objText5.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);

                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 80, p_intPosY + 470, 555, 20);
                    m_objText6.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 80, p_intPosY + 490, 60, 20);
                    m_objText7.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 230, p_intPosY + 490, 535, 20);
                    m_objText8.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 90, p_intPosY + 510, 80, 20);
                    m_objText9.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 190, p_intPosY + 510, 80, 20);
                    m_objText10.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 310, p_intPosY + 510, 80, 20);
                    m_objText11.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);

                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_dtmSignTime1.ToString("yyyy��MM��dd��"), new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 530);
                    p_intPosY += 580;

					m_blnFirstPrint = false;
				}

				
				m_blnHaveMoreLine = false;					
			}

			public override void m_mthReset()
			{
//				m_intTimes = 0;
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();							
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					if(value != null)
					{
						Object[] objData=(Object[])value ;
						m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString() ,objData[1].ToString(),m_dtmFirstPrintTime,true);
                        m_objText2.m_mthSetContextWithCorrectBefore(objData[2].ToString(), objData[3].ToString(), m_dtmFirstPrintTime, true);
                        m_objText3.m_mthSetContextWithCorrectBefore(objData[4].ToString(), objData[5].ToString(), m_dtmFirstPrintTime, true);
                        m_objText4.m_mthSetContextWithCorrectBefore(objData[6].ToString(), objData[7].ToString(), m_dtmFirstPrintTime, true);
                        m_objText5.m_mthSetContextWithCorrectBefore(objData[14].ToString(), objData[15].ToString(), m_dtmFirstPrintTime, true);
                        m_objText6.m_mthSetContextWithCorrectBefore(objData[16].ToString(), objData[17].ToString(), m_dtmFirstPrintTime, true); 
                        m_objText7.m_mthSetContextWithCorrectBefore(objData[18].ToString(), objData[19].ToString(), m_dtmFirstPrintTime, true);
                        m_objText8.m_mthSetContextWithCorrectBefore(objData[20].ToString(), objData[21].ToString(), m_dtmFirstPrintTime, true);
                        m_objText9.m_mthSetContextWithCorrectBefore(objData[22].ToString(), objData[23].ToString(), m_dtmFirstPrintTime, true);
                        m_objText10.m_mthSetContextWithCorrectBefore(objData[24].ToString(), objData[25].ToString(), m_dtmFirstPrintTime, true);
                        m_objText11.m_mthSetContextWithCorrectBefore(objData[26].ToString(), objData[27].ToString(), m_dtmFirstPrintTime, true);                        
					}
				}
			}			
		}				

        private class clsPrintLine2 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private com.digitalwave.controls.clsPrintRichTextContext m_objText1;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText2;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText3;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText4;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText5;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText6;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText7;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText8;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText9;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText10;
            private com.digitalwave.controls.clsPrintRichTextContext m_objText11;
            
            private bool m_blnFirstPrint = true;
            public clsFetalCustodialRecordPrintInfo m_objPrintInfo = null;

            internal static bool m_blnSinglePage = true;

			public clsPrintLine2()
			{
                m_objText1 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText2 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText3 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText4 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText5 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText6 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText7 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText8 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText9 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText10 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText11 = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{					
					m_blnFirstPrint = false;
                    p_objGrp.DrawString("�� �� �� ¼", new Font("SimSun", 15), Brushes.Black, (int)enmRectangleInfo.LeftX + 300, p_intPosY + 10);
                    p_objGrp.DrawString("������������䷽ʽ                ���ܲ���      Сʱ      �֣��������֣�      �֡�", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 80);
                    p_objGrp.DrawString("̥    ��������            kg;��          cm��  ��ˮ          ml��ɫ                ��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 100);
                    p_objGrp.DrawString("          ̥��                �����                ��", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 120);
                    p_objGrp.DrawString("��    ע�������쳣����������ϸ��¼���Ա�ع˷�����", new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 3, p_intPosY + 140);
                    p_objGrp.DrawString("ǩ����" + m_objPrintInfo.m_objRecordContent.m_strSignName2, new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 450, p_intPosY + 270);
                    p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_dtmSignTime2.ToString("yyyy��MM��dd��"), new Font("SimSun", 11), Brushes.Black, (int)enmRectangleInfo.LeftX + 600, p_intPosY + 270);

                    int intRealHeight;
                    Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 150, p_intPosY + 80, 100, 20);
                    m_objText1.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 350, p_intPosY + 80, 80, 20);
                    m_objText2.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 430, p_intPosY + 80, 80, 20);
                    m_objText3.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 590, p_intPosY + 80, 80, 20);
                    m_objText4.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 120, p_intPosY + 100, 80, 20);
                    m_objText5.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 260, p_intPosY + 100, 80, 20);
                    m_objText6.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 420, p_intPosY + 100, 80, 20);
                    m_objText7.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 540, p_intPosY + 100, 120, 20);
                    m_objText8.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 120, p_intPosY + 120, 120, 20);
                    m_objText9.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 295, p_intPosY + 120, 120, 20);
                    m_objText10.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, true);
                    rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX + 80, p_intPosY + 160, 655, 100);
                    m_objText11.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
				}
                m_blnHaveMoreLine = false;				
			}

			public override void m_mthReset()
			{
//				m_intTimes = 0;
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();							
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					if(value != null)
					{
						Object[] objData=(Object[])value ;
                        m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), m_dtmFirstPrintTime, true);
                        m_objText2.m_mthSetContextWithCorrectBefore(objData[2].ToString(), objData[3].ToString(), m_dtmFirstPrintTime, true);
                        m_objText3.m_mthSetContextWithCorrectBefore(objData[4].ToString(), objData[5].ToString(), m_dtmFirstPrintTime, true);
                        m_objText4.m_mthSetContextWithCorrectBefore(objData[6].ToString(), objData[7].ToString(), m_dtmFirstPrintTime, true);
                        m_objText5.m_mthSetContextWithCorrectBefore(objData[8].ToString(), objData[9].ToString(), m_dtmFirstPrintTime, true);
                        m_objText6.m_mthSetContextWithCorrectBefore(objData[10].ToString(), objData[11].ToString(), m_dtmFirstPrintTime, true);
                        m_objText7.m_mthSetContextWithCorrectBefore(objData[12].ToString(), objData[13].ToString(), m_dtmFirstPrintTime, true);
                        m_objText8.m_mthSetContextWithCorrectBefore(objData[14].ToString(), objData[15].ToString(), m_dtmFirstPrintTime, true);
                        m_objText9.m_mthSetContextWithCorrectBefore(objData[16].ToString(), objData[17].ToString(), m_dtmFirstPrintTime, true);
                        m_objText10.m_mthSetContextWithCorrectBefore(objData[18].ToString(), objData[19].ToString(), m_dtmFirstPrintTime, true);
                        m_objText11.m_mthSetContextWithCorrectBefore(objData[20].ToString(), objData[21].ToString(), m_dtmFirstPrintTime, true);                        
					}
				}
			}
        }
        #endregion 2
        #endregion
	}

}


