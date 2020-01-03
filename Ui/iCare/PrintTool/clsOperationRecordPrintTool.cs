using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ���������¼�Ĵ�ӡ������,Jacky-2003-6-6
	/// </summary>
	public class clsOperationRecordPrintTool: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsOperationRecordDomain m_objRecordsDomain;
		private clsPrintInfo_OperationRecord m_objPrintInfo;
		private clsOperationRecord_All m_objclsOperationRecord_All=null;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">CreatDate��������һ�δ�ӡһ����¼��������,���ܺ���CreatDate,ע���˴�m_objPrintInfo.m_dtmOpenDate�д�ŵ�ʵ�������û���¼ʱ��</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmCreatDate)
		{		
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new clsPrintInfo_OperationRecord();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmCreatDate;
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
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
			m_blnWantInit=false;//
			if(m_objPrintInfo==null)
			{
				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_dtmOpenDate==DateTime.MinValue)
				m_objclsOperationRecord_All=null;				
			else
			{
				m_objRecordsDomain=new clsOperationRecordDomain();	
				long lngRes=m_objRecordsDomain.m_lngGetOperationRecord_All(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objclsOperationRecord_All );
				if(lngRes <= 0)
					return ;   
			}
			//���ñ����ݵ���ӡ��			
			m_objPrintInfo.m_objclsOperationRecord_All=m_objclsOperationRecord_All;
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_OperationRecord")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_OperationRecord)p_objPrintContent;
			m_objclsOperationRecord_All= m_objPrintInfo. m_objclsOperationRecord_All ;		
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
				if(m_objPrintInfo==null)
				{
					clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
					return null;
				}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			
			
			return m_objPrintInfo;
		}		

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			#region �йش�ӡ��ʼ��

			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 14);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();
			m_objCPaint=new clsPublicControlPaint();			

			#endregion
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			
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
			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_objclsOperationRecord_All==null) return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate != null && m_objPrintInfo.m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate != "")
			{	
				if(m_objclsOperationRecord_All.m_objOperationRecord.strOpenDate==null || m_objclsOperationRecord_All.m_objOperationRecord.strOpenDate=="")return;
				long lngRes=m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objclsOperationRecord_All.m_objOperationRecord.strOpenDate,m_objPrintInfo.m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate);	
				if(lngRes<=0)
				{
					switch(lngRes)
					{
						case (long) enmOperationResult.Not_permission:
							clsPublicFunction.s_mthShowNotPermitMessage();
							break;
						case (long) enmOperationResult.DB_Fail://��������״δ�ӡ�򷵻�ֵΪ0����˴˴����ܼӴ��ж�							
							break;
						default : 
							clsPublicFunction.ShowInformationMessageBox("���´�ӡʱ��ʧ��");
							break;
					}	
					return;
				}
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
			Font fntNormal = new Font("SimSun",11);

			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY);
			if(m_intPages!=1)
				m_intYPos=m_intYPos+5;
			while(m_objPrintContext.m_BlnHaveMoreLine)
			{
				m_objPrintContext.m_mthPrintNextLine(ref m_intYPos,e.Graphics,fntNormal);
				
				#region ����ҳ
				if(m_intYPos >(int)enmRectangleInfo.BottomY 
					&& m_objPrintContext.m_BlnHaveMoreLine)
				{
					e.HasMorePages = true;
					switch(m_intEndIndex)
					{
											
						case 0:
							m_mthHandleOneEnd(m_intYPos,e.Graphics,fntNormal);
							m_intPreY=(int)enmRectangleInfo.TopY;
							m_intEndIndex--;
							break;
						case 1:
							m_mthHandleTwoEnd(m_intYPos,e.Graphics,fntNormal);
							m_intPreY=(int)enmRectangleInfo.TopY;
							m_intEndIndex--;
							break;
						case 2:
							m_mthHandleThreeEnd(m_intYPos,e.Graphics,fntNormal);
							m_intPreY=(int)enmRectangleInfo.TopY;
							m_intEndIndex--;
							break;						
					}				
				
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos-5);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos-5);
					
					m_intPages++;
					m_intYPos=(int)enmRectangleInfo.TopY+5;
					
					return;
				}
				#endregion
				
			}
			m_intYPos=m_intYPos-5;

//			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos ,(int)enmRectangleInfo.RightX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.BottomY-5);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.BottomY-5);
			
			m_intYPos += (int)enmRectangleInfo.RowStep+5;
			Font fntSign = new Font("",6);
			while(m_objPrintContext.m_BlnHaveMoreSign)
			{
				m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX,m_intYPos,e.Graphics,fntSign);

				m_intYPos += (int)enmRectangleInfo.RowStep-10;

//				if(m_intYPos >(int)enmRectangleInfo.BottomY 
//					&& m_objPrintContext.m_BlnHaveMoreLine)
//				{
//					e.HasMorePages = true;
//
//					m_intPages++;
//					m_intYPos=(int)enmRectangleInfo.TopY+5;
//					
//					return;
//				}
			}

			//ȫ������
			m_objPrintContext.m_mthReset();
			m_intPages=1;
			m_intEndIndex=0;
			m_intYPos = (int)enmRectangleInfo.TopY+5;
			m_intPreY=(int)enmRectangleInfo.TopY;	
		}

		// ��ӡ����ʱ�Ĳ���
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
		
		}		
		

		#region	��ӡ		
		private void m_mthSetPrintValue()
		{
			#region ��ӡ�г�ʼ��
			m_objLine1 = new clsPrintLine1();
			m_objLine2 = new clsPrintLine2();
			m_objLine3 = new clsPrintLine3();
			m_objLine4 = new clsPrintLine4();
			m_objLine5 = new clsPrintLine5();
			m_objLine6 = new clsPrintLine6();
			m_objLine7 = new clsPrintLine7();
			m_objLine8 = new clsPrintLine8();
			m_objLine9 = new clsPrintLine9();
			m_objLine10 = new clsPrintLine10();
			m_objLine11 = new clsPrintLine11();
			m_objLine12 = new clsPrintLine12();
			m_objLine13 = new clsPrintLine13();
			m_objLine14 = new clsPrintLine14();
			m_objLine15 = new clsPrintLine15();
			m_objLine16 = new clsPrintLine16();
			m_objLine17 = new clsPrintLine17();
			m_objLine18 = new clsPrintLine18();
			m_objLine19 = new clsPrintLine19();
			m_objLine20 = new clsPrintLine20();
			m_objLine21 = new clsPrintLine21();
			m_objLine22 = new clsPrintLine22();
			m_objLine23 = new clsPrintLine23();
			m_objLine24 = new clsPrintLine24();
			m_objLine25 = new clsPrintLine25();
			m_objLine26 = new clsPrintLine26();
			m_objLine27 = new clsPrintLine27();
			m_objSign = new clsPrintRecordSign();
			
			m_objLine7.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleOneEnd);
			m_objLine25.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleTwoEnd);
			m_objLine26.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleThreeEnd);
							
			m_objPrintContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  m_objLine1,
//										  m_objLine2,
										  m_objLine3,
//										  m_objLine4,
										  m_objLine5,
										  m_objLine6,
										  m_objLine7,
										  m_objLine8,
										  m_objLine9,
										  m_objLine10,
										  m_objLine11,
										  m_objLine12,
										  m_objLine13,
										  m_objLine14,
										  m_objLine15,
										  m_objLine16,
										  m_objLine17,
										  m_objLine18,
										  m_objLine19,
										  m_objLine20,
										  m_objLine21,
										  m_objLine22,
										  m_objLine23,
										  m_objLine24,
										  m_objLine25,
										  m_objLine26,
										  m_objLine27,
									  });
			m_objPrintContext.m_ObjPrintSign = m_objSign;
			#endregion  

			clsXML_DataGrid objXML_DataGrid=new clsXML_DataGrid();
			
			#region  ��һ�δ�ӡʱ�丳ֵ			
			DateTime dtmFirstPrintTime=DateTime.Now;

			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{
				if(m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate !=null && m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate.Trim()!="")
					dtmFirstPrintTime=DateTime.Parse(m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate);
				else 
					m_objclsOperationRecord_All.m_objOperationRecord.strFirstPrintDate=dtmFirstPrintTime.ToString("yyyy-MM-dd HH:mm:ss");
				#endregion  ��һ�δ�ӡʱ�丳ֵ
			
			}

            //����ҽʦ
            ArrayList m_strOperationerArr = new ArrayList();
            //����ҽʦ
            ArrayList m_strAnaDocSignArr = new ArrayList();
            //ϴ�ֻ�ʿ
            ArrayList m_strWashNurseSignArr = new ArrayList();
            //Ѳ�ػ�ʿ
            ArrayList m_strCircuitNurseSignArr = new ArrayList();
            //��¼��ʿ
            ArrayList m_strRecordNurseSignArr = new ArrayList();
            //�޾����
            ArrayList m_strBacilliCheckSignArr = new ArrayList();
            //ǩ��
            string m_strDocSign = null;

            #region ��ȡǩ��
            if (m_objclsOperationRecord_All !=null 
                && m_objclsOperationRecord_All.m_objOperationRecord !=null
                && m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr != null)
            {
                for (int i = 0; i < m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr.Length; i++)
                {
                    if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvOperationer")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strOperationerArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvAnaDocSign")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strAnaDocSignArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvWashNurseSign")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strWashNurseSignArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvCircuitNurseSign")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strCircuitNurseSignArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvBacilliCheckSign")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strBacilliCheckSignArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_lsvRecordNurseSign")
                    {
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        m_strRecordNurseSignArr.Add(m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                    }
                    else if (m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].controlName == "m_txtSign")
                    {
                        m_strDocSign = m_objclsOperationRecord_All.m_objOperationRecord.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
                    }
                }
            } 
            #endregion

			m_objPrintContext.m_DtmFirstPrintTime=dtmFirstPrintTime;

			#region ��ÿһ�е�Ԫ�ظ�ֵ
			///////////////1��/////////////////
//			Object objData1=new Object();
//			objData1="";	
//			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationID !=null)
////				objData1=m_objclsOperationRecord_All.m_objOperationID.strOperationName;
//				objData1=m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationName;
			
			Object[] objData1=new Object[3];
			objData1[0]="";
			objData1[1]="";
			objData1[2]="";
			if(m_objclsOperationRecord_All !=null &&m_objclsOperationRecord_All.m_objOperationRecordContent !=null && m_objclsOperationRecord_All.m_objOperationRecord !=null)
			{
				objData1[0]=m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationName;
				objData1[1]=m_objclsOperationRecord_All.m_objOperationRecord.strOperationNameXML;
			}
            if (m_strOperationerArr.Count > 0)
			{
                for (int i = 0; i<m_strOperationerArr.Count; i++)
				{
                    if (i == 0)
                    {
                        objData1[2] += m_strOperationerArr[i].ToString();
                    }
                    else
                    {
                        objData1[2] += "," + m_strOperationerArr[i].ToString();
                    }
				}
			}
			m_objLine1.m_ObjPrintLineInfo=objData1;
			///////////////2��/////////////////
			Object objData2=new Object();
			objData2="";
            if (m_strOperationerArr.Count > 0)
            {
                for (int i = 0; i < m_strOperationerArr.Count; i++)
                {
                    objData2 += m_strOperationerArr[i].ToString() + " ";
                }
            }
			m_objLine2.m_ObjPrintLineInfo=objData2;
						           
			///////////////3��/////////////////
			Object[] objData3=new Object[3];
			objData3[0]="";
			objData3[1]="";
			objData3[2]="";
			if(m_objclsOperationRecord_All !=null &&m_objclsOperationRecord_All.m_objOperationRecordContent !=null && m_objclsOperationRecord_All.m_objOperationRecord !=null)
				//			objData3=m_objclsOperationRecord_All.m_objAnaesthesiaModeID.strAnaesthesiaModeName;
			{
				objData3[0]=m_objclsOperationRecord_All.m_objOperationRecordContent.strAnaesthesiaMode;
				objData3[1]=m_objclsOperationRecord_All.m_objOperationRecord.strAnaesthesiaModeXML;
			}
            if (m_strAnaDocSignArr.Count > 0)
            {
                for (int i = 0; i < m_strAnaDocSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData3[2] += m_strAnaDocSignArr[i].ToString();
                    }
                    else
                    {
                        objData3[2] += "," + m_strAnaDocSignArr[i].ToString();
                    }
                }
            }

			m_objLine3.m_ObjPrintLineInfo=objData3;
				
			///////////////4��/////////////////
			Object objData4=new Object();
			objData4="";
            if (m_strOperationerArr.Count > 0)
            {
                for (int i = 0; i < m_strAnaDocSignArr.Count; i++)
                {
                    objData4 += m_strAnaDocSignArr[i].ToString() + " ";
                }
            }
			m_objLine4.m_ObjPrintLineInfo=objData4;
			
			///////////////5��/////////////////
			Object[] objData5=new object[8];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{
				objData5[0]=m_objclsOperationRecord_All.m_objOperationRecord.strCreateDate;
				objData5[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesClearHeaded=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesClearHeaded=="True"? 1:0);
				objData5[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesSleep=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesSleep=="True"? 1:0);
				objData5[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesComa=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSensesComa=="True"? 1:0);
				objData5[4]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotAllergic=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotAllergic=="True"? 1:0);
				objData5[5]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveAllergic=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveAllergic=="True"? 1:0);
				objData5[6]=m_objclsOperationRecord_All.m_objOperationRecordContent.strAllergicContent;
				objData5[7]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strAllergicContentXML);
			}
				
			else
			{
				for (int i=0;i<objData5.Length;i++)
				{
					objData5[i]="";
				}
			}
			m_objLine5.m_ObjPrintLineInfo=objData5;
			///////////////6��/////////////////
			Object[] objData6=new object[8];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData6[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationOnHisBack=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationOnHisBack=="True"? 1:0);
				objData6[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationSide=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationSide=="True"? 1:0);
				objData6[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationPA=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationPA=="True"? 1:0);
				objData6[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationParaplegic=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationParaplegic=="True"? 1:0);
				objData6[4]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationHypothyroid=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationHypothyroid=="True"? 1:0);
				objData6[5]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationOther=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLocationOther=="True"? 1:0);
				objData6[6]=m_objclsOperationRecord_All.m_objOperationRecordContent.strOtherOperationLocation;
				objData6[7]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strOtherOperationLocationXML);
			}
			else
			{
				for (int i=0;i<objData6.Length;i++)
				{
					objData6[i]="";
				}
			}
			m_objLine6.m_ObjPrintLineInfo=objData6;
			///////////////7��/////////////////
			Object[] objData7=new object[5];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData7[0]=m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationRoom;
				objData7[1]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strOperationRoomXML);
				try
				{
					if( m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationBeginDate!=null&& m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationBeginDate!="")
					{
						objData7[2]=DateTime.Parse( m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationBeginDate).ToString("yyyy-MM-dd HH:mm");
					}
				}
				catch
				{
					objData7[2] = "";
				}
				try
				{
					if( m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationEndDate!=null&& m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationEndDate!="")
					{
						objData7[3]=DateTime.Parse( m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationEndDate).ToString("yyyy-MM-dd HH:mm");
					}
				}
				catch
				{
					objData7[3] = "";
				}
				try
				{
					if(m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLeaveDate!=null&m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLeaveDate!="")
					{
						objData7[4]=DateTime.Parse( m_objclsOperationRecord_All.m_objOperationRecordContent.strOperationLeaveDate).ToString("yyyy-MM-dd HH:mm");
					}
				}
				catch
				{
					objData7[4] = "";
				}
			}
			else
			{
				for (int i=0;i<objData7.Length;i++)
				{
					objData7[i]="";
				}
			}
			m_objLine7.m_ObjPrintLineInfo=objData7;
			///////////////8��/////////////////
			Object objData8=new Object();
			objData8="";
            if (m_strBacilliCheckSignArr.Count > 0)
            {
                for (int i = 0; i < m_strBacilliCheckSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData8 += m_strBacilliCheckSignArr[i].ToString();
                    }
                    else
                    {
                        objData8 += "," + m_strBacilliCheckSignArr[i].ToString();
                    }
                }
            }
			m_objLine8.m_ObjPrintLineInfo=objData8;
		
			///////////////9��/////////////////
			Object[] objData9=new object[10];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData9[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotElectKnife=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotElectKnife=="True"? 1:0);
				objData9[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveUsedElectKnife=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveUsedElectKnife=="True"? 1:0);
				objData9[2]=m_objclsOperationRecord_All.m_objOperationRecordContent.strElectKnifeModel;
				objData9[3]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strElectKnifeModelXML);
				objData9[4]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotDoublePole=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveNotDoublePole=="True"? 1:0);
				objData9[5]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveDoublePole=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveDoublePole=="True"? 1:0);
				objData9[6]=m_objclsOperationRecord_All.m_objOperationRecordContent.strDoublePoleContent;
				objData9[7]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strDoublePoleContentXML);
				objData9[8]=m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocation;
				objData9[9]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strCathodeLocationXML);
			}
			else
			{
				for (int i=0;i<objData9.Length;i++)
				{
					objData9[i]="";
				}
			}


			m_objLine9.m_ObjPrintLineInfo=objData9;
			///////////////10��/////////////////
			Object[] objData10=new object[4];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{
				objData10[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinBeforOperationFull=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinBeforOperationFull=="True"? 1:0);
				objData10[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinBeforOperationMar=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinBeforOperationMar=="True"? 1:0);
				objData10[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinAfterOperationFull=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinAfterOperationFull=="True"? 1:0);
				objData10[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinAfterOperationMar=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strCathodeLocationSkinAfterOperationMar=="True"? 1:0);
			}
			else
			{
				for (int i=0;i<objData10.Length;i++)
				{
					objData10[i]="";
				}
			}

			m_objLine10.m_ObjPrintLineInfo=objData10;
			///////////////11��/////////////////
			Object[] objData11=new object[4];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData11[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strStypticRubber=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strStypticRubber=="True"? 1:0);
				objData11[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strStypticPressure=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strStypticPressure=="True"? 1:0);
				objData11[2]=m_objclsOperationRecord_All.m_objOperationRecordContent.strStypticPressureMode;
				objData11[3]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strStypticPressureModeXML);
			}
			else
			{
				for (int i=0;i<objData11.Length;i++)
				{
					objData11[i]="";
				}

			}
			m_objLine11.m_ObjPrintLineInfo=objData11;
			///////////////12��/////////////////
			
			///////////////13��/////////////////
			Object[] objData13=new object[12];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData13[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strUpForearm=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strUpForearm=="True"? 1:0);
				objData13[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strUpThigh=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strUpThigh=="True"? 1:0);
				objData13[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strUpLeft=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strUpLeft=="True"? 1:0);
				objData13[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strUpRight=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strUpRight=="True"? 1:0);
				objData13[4]=m_objclsOperationRecord_All.m_objOperationRecordContent.strUpPuffDateTime ;
				objData13[5]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strUpPuffDateTimeXML);
				objData13[6]=m_objclsOperationRecord_All.m_objOperationRecordContent.strUpDeflateDateTime;
				objData13[7]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strUpDeflateDateTimeXML);
				objData13[8]=m_objclsOperationRecord_All.m_objOperationRecordContent.strUpTotalDateTime;
				objData13[9]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strUpTotalDateTimeXML);
				objData13[10]=m_objclsOperationRecord_All.m_objOperationRecordContent.strUpPress;
				objData13[11]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strUpPressXML);
			}
			else
			{
				for (int i=0;i<objData13.Length;i++)
				{
					objData13[i]="";
				}

			}

			m_objLine13.m_ObjPrintLineInfo=objData13;
			///////////////14��/////////////////
			Object[] objData14=new object[12];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{
				objData14[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strDownForearm=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strDownForearm=="True"? 1:0);
				objData14[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strDownThigh=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strDownThigh=="True"? 1:0);
				objData14[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strDownLeft=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strDownLeft=="True"? 1:0);
				objData14[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strDownRight=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strDownRight=="True"? 1:0);
				objData14[4]=m_objclsOperationRecord_All.m_objOperationRecordContent.strDownPuffDateTime ;
				objData14[5]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strDownPuffDateTimeXML);
				objData14[6]=m_objclsOperationRecord_All.m_objOperationRecordContent.strDownDeflateDateTime ;
				objData14[7]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strDownDeflateDateTimeXML);
				objData14[8]=m_objclsOperationRecord_All.m_objOperationRecordContent.strDownTotalDateTime ;
				objData14[9]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strDownTotalDateTimeXML);
				objData14[10]=m_objclsOperationRecord_All.m_objOperationRecordContent.strDownPress ;
				objData14[11]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strDownPressXML);
				
			}
			else
			{
				for (int i=0;i<objData14.Length;i++)
				{
					objData14[i]="";
				}

			}
			m_objLine14.m_ObjPrintLineInfo=objData14;

			///////////////15��/////////////////
			Object[] objData15=new object[7];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData15[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleySickroom=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleySickroom=="True"? 1:0);
				objData15[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyOperationRoom=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyOperationRoom=="True"? 1:0);
				objData15[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyDoubleAntrum=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyDoubleAntrum=="True"? 1:0);
				objData15[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyThreeAntrum=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyThreeAntrum=="True"? 1:0);
				objData15[4]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyOther=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyOther=="True"? 1:0);
				objData15[5]=m_objclsOperationRecord_All.m_objOperationRecordContent.strFoleyOtherContent ;
				objData15[6]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strFoleyOtherContentXML) ;
			}
			else 
			{
				for(int i=0;i<objData15.Length ;i++)
				{
					objData15[i]="";
				}
			}

			m_objLine15.m_ObjPrintLineInfo=objData15;
			///////////////16��/////////////////
			Object[] objData16=new object[2];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData16[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strStomachSickroom=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strStomachSickroom=="True"? 1:0);
				objData16[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strStomachOprationRoom=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strStomachOprationRoom=="True"? 1:0);
			}
			else 
			{
				for(int i=0;i<objData16.Length ;i++)
				{
					objData16[i]="";
				}
			}

			m_objLine16.m_ObjPrintLineInfo=objData16;
			///////////////17��/////////////////
			Object[] objData17=new object[7];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData17[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsis2=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsis2=="True"? 1:0);
				objData17[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsis75=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsis75=="True"? 1:0);
				objData17[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisIodin=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisIodin=="True"? 1:0);
				objData17[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisIodinRare=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisIodinRare=="True"? 1:0);
				objData17[4]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisOther=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisOther=="True"? 1:0);
				objData17[5]=m_objclsOperationRecord_All.m_objOperationRecordContent.strSkinAntisepsisOtherContent;
				objData17[6]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strSkinAntisepsisOtherContentXML);
			}
			else 
			{
				for(int i=0;i<objData17.Length ;i++)
				{
					objData17[i]="";
				}
			}


			m_objLine17.m_ObjPrintLineInfo=objData17;
			
			///////////////18��/////////////////
			Object[] objData18=new object[15];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData18[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strAllBlood=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strAllBlood=="True"? 1:0);
				objData18[1]=m_objclsOperationRecord_All.m_objOperationRecordContent.strAllBloodQty;
				objData18[2]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strAllBloodQtyXML);
				objData18[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strRedCell=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strRedCell=="True"? 1:0);
				objData18[4]=m_objclsOperationRecord_All.m_objOperationRecordContent.strRedCellQty;
				objData18[5]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strRedCellQtyXML);
				objData18[6]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strBloodPlasm=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strBloodPlasm=="True"? 1:0);
				objData18[7]=m_objclsOperationRecord_All.m_objOperationRecordContent.strBloodPlasmQty;
				objData18[8]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strBloodPlasmQtyXML) ;
				objData18[9]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strOwnBlood=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strOwnBlood=="True"? 1:0);
				objData18[10]=m_objclsOperationRecord_All.m_objOperationRecordContent.strOwnBloodQty;
				objData18[11]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strOwnBloodQtyXML) ;
				objData18[13]=m_objclsOperationRecord_All.m_objOperationRecordContent.strBloodOtherQty ;
				objData18[14]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strBloodOtherQtyXML) ;
			}
			else 
			{
				for(int i=0;i<objData18.Length ;i++)
				{
					objData18[i]="";
				}
			}

		
			m_objLine18.m_ObjPrintLineInfo=objData18;
			///////////////19��/////////////////
			Object[] objData19=new object[4];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData19[0]=m_objclsOperationRecord_All.m_objOperationRecordContent.strInLiquidQty ;
				objData19[1]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strInLiquidQtyXML) ;
				objData19[2]=m_objclsOperationRecord_All.m_objOperationRecordContent.strPeeOperatingQty ;
				objData19[3]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strPeeOperatingQtyXML);
			}
			else 
			{
				for(int i=0;i<objData19.Length ;i++)
				{
					objData19[i]="";
				}
			}
			
			m_objLine19.m_ObjPrintLineInfo=objData19;
			///////////////20��/////////////////
			Object[] objData20=new object[2];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

			
				objData20[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strNotHaveOutFlow=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strNotHaveOutFlow =="True"? 1:0);
				objData20[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveOutFlow=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strHaveOutFlow=="True"? 1:0);
			}
			else 
			{
				for(int i=0;i<objData20.Length ;i++)
				{
					objData20[i]="";
				}
			}

			m_objLine20.m_ObjPrintLineInfo=objData20;
			///////////////21��/////////////////
			
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objWoundThingArr !=null)
			{
				Object[][] objData21=new object[m_objclsOperationRecord_All.m_objWoundThingArr.Length][];
				for(int i=0;i<m_objclsOperationRecord_All.m_objWoundThingArr.Length;i++)
				{
					objData21[i] = new object[m_objclsOperationRecord_All.m_objWoundThingArr.Length];
					try
					{	objData21[i][0]=new object();
						objData21[i][0]=m_objclsOperationRecord_All.m_objWoundThingArr[i].strWoundThingName;
						objData21[i][1]=m_objclsOperationRecord_All.m_objWoundThingArr[i].strQuantity;
					}
					catch(Exception ex)
					{
						System.Windows.Forms.MessageBox.Show(ex.Message);
					}					
				}
				m_objLine21.m_ObjPrintLineInfo=objData21;
			}

			///////////////22��/////////////////
			Object[] objData22=new object[4];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData22[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationFull=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationFull=="True"? 1:0);
				objData22[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationMar=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationMar=="True"? 1:0);
				objData22[2]=m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationContent;
				objData22[3]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strFromHeadToFootSkinBeforeOperationContentXML);
			}
			else 
			{
				for(int i=0;i<objData22.Length ;i++)
				{
					objData22[i]="";
				}
			}


			m_objLine22.m_ObjPrintLineInfo=objData22;
			///////////////23��/////////////////
			Object[] objData23=new object[4];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData23[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinAfterOperationFull=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinAfterOperationFull=="True"? 1:0);
				objData23[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinAfterOperationMar=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinAfterOperationMar=="True"? 1:0);
				objData23[2]=m_objclsOperationRecord_All.m_objOperationRecordContent.strFromHeadToFootSkinAfterOperationContent;
				objData23[3]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strFromHeadToFootSkinAfterOperationContentXML);
			}
			else 
			{
				for(int i=0;i<objData23.Length ;i++)
				{
					objData23[i]="";
				}
			}
			
			m_objLine23.m_ObjPrintLineInfo=objData23;
			///////////////24��/////////////////
			Object[] objData24=new object[6];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData24[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleGeneral=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleGeneral=="True"? 1:0);
				objData24[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleSlice=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleSlice=="True"? 1:0);
				objData24[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleBacilli=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleBacilli=="True"? 1:0);
				objData24[3]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleOther=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleOther=="True"? 1:0);
				objData24[4]=m_objclsOperationRecord_All.m_objOperationRecordContent.strSampleOtherContent;
				objData24[5]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strSampleOtherContentXML) ;
			}
			else 
			{
				for(int i=0;i<objData24.Length ;i++)
				{
					objData24[i]="";
				}
			}

			m_objLine24.m_ObjPrintLineInfo=objData24;
			///////////////25��/////////////////
			Object[] objData25=new object[3];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData25[0]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendRenew=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendRenew=="True"? 1:0);
				objData25[1]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendICU=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendICU=="True"? 1:0);
				objData25[2]=(m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendSickRoom=="1"||m_objclsOperationRecord_All.m_objOperationRecordContent.strAfterOperationSendSickRoom=="True"? 1:0);
			}
			else 
			{
				for(int i=0;i<objData25.Length ;i++)
				{
					objData25[i]="";
				}
			}

			m_objLine25.m_ObjPrintLineInfo=objData25;
			///////////////26��/////////////////
			Object[] objData26=new object[2];
			if(m_objclsOperationRecord_All !=null && m_objclsOperationRecord_All.m_objOperationRecordContent!=null && m_objclsOperationRecord_All.m_objOperationRecord!=null)
			{

				objData26[0]=m_objclsOperationRecord_All.m_objOperationRecordContent.strTendRecord ;
				objData26[1]=objXML_DataGrid.m_strReplaceWhiteToBlack(m_objclsOperationRecord_All.m_objOperationRecord.strTendRecordXML) ;
			}
			else 
			{
				for(int i=0;i<objData26.Length ;i++)
				{
					objData26[i]="";
				}
			}

		
			m_objLine26.m_ObjPrintLineInfo=objData26;
			///////////////27��/////////////////
			Object[] objData27=new object[6];
			objData27[0]="";
			objData27[1]="";
			objData27[2]="";
            objData27[3] = "";
            objData27[4] = "";
            objData27[5] = "";

            //��¼��ʿ
            if (m_strRecordNurseSignArr.Count > 0)
            {
                for (int i = 0; i < m_strRecordNurseSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[0] += m_strRecordNurseSignArr[i].ToString();
                    }
                    else
                    {
                        objData27[0] += "," + m_strRecordNurseSignArr[i].ToString();
                    }
                }
            }
            //ϴ�ֻ�ʿ
            if (m_strWashNurseSignArr.Count > 0)
            {
                for (int i = 0; i < m_strWashNurseSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[1] += m_strWashNurseSignArr[i].ToString();
                    }
                    else
                    {
                        objData27[1] += "," + m_strWashNurseSignArr[i].ToString();
                    }
                }
            }
            //Ѳ�ػ�ʿ
            if (m_strCircuitNurseSignArr.Count > 0)
            {
                for (int i = 0; i < m_strCircuitNurseSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[2] += m_strCircuitNurseSignArr[i].ToString();
                    }
                    else
                    {
                        objData27[2] += "," + m_strCircuitNurseSignArr[i].ToString();
                    }
                }
            }
            //����ҽʦ
            if (m_strOperationerArr.Count > 0)
            {
                for (int i = 0; i < m_strOperationerArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[3] += m_strOperationerArr[i].ToString();
                    }
                    else
                    {
                        objData27[3] += "," + m_strOperationerArr[i].ToString();
                    }
                }
            }
            //����ҽʦ
            if (m_strAnaDocSignArr.Count > 0)
            {
                for (int i = 0; i < m_strAnaDocSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[4] += m_strAnaDocSignArr[i].ToString();
                    }
                    else
                    {
                        objData27[4] += "," + m_strAnaDocSignArr[i].ToString();
                    }
                }
            }
            //�޾����
            if (m_strBacilliCheckSignArr.Count > 0)
            {
                for (int i = 0; i < m_strBacilliCheckSignArr.Count; i++)
                {
                    if (i == 0)
                    {
                        objData27[5] += m_strBacilliCheckSignArr[i].ToString();
                    }
                    else
                    {
                        objData27[5] += "," + m_strBacilliCheckSignArr[i].ToString();
                    }
                }
            }
			m_objLine27.m_ObjPrintLineInfo=objData27;	
			#endregion 
		}
		
		
		#region �йش�ӡ������
		private clsPublicControlPaint m_objCPaint;
		private clsPrintContext m_objPrintContext;
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
		/// ��ȡ�������
		/// </summary>
		private clsPrintPageSettingForRecord m_objPageSetting;
		/// <summary>
		/// ��ӡ�Ĳ��˻�����Ϣ��
		/// </summary>
		private int m_intYPos = (int)enmRectangleInfo.TopY+5;
		private int m_intPreY = (int)enmRectangleInfo.TopY;
		private int m_intEndIndex = 0;
		private int m_intPages=1;		

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		public enum enmRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 150,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 10,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 827-40,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 20,
			SmallRowStep=20,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=35,

			/// <summary>
			/// CheckBoxƫ���ұ��ı��ľ���
			/// </summary>
			CheckShift=15,

			/// <summary>
			/// �׻���ƫ���ı�����ľ���
			/// </summary>
			BottomLineShift=15,

			BottomY=1024
		
		}
		#region ��ӡ�ж���
		private clsPrintLine1 m_objLine1;
		private clsPrintLine2 m_objLine2;
		private clsPrintLine3 m_objLine3;
		private clsPrintLine4 m_objLine4;
		private clsPrintLine5 m_objLine5;
		private clsPrintLine6 m_objLine6;
		private clsPrintLine7 m_objLine7;
		private clsPrintLine8 m_objLine8;
		private clsPrintLine9 m_objLine9;
		private clsPrintLine10 m_objLine10;
		private clsPrintLine11 m_objLine11;
		private clsPrintLine12 m_objLine12;
		private clsPrintLine13 m_objLine13;
		private clsPrintLine14 m_objLine14;
		private clsPrintLine15 m_objLine15;
		private clsPrintLine16 m_objLine16;
		private clsPrintLine17 m_objLine17;
		private clsPrintLine18 m_objLine18;
		private clsPrintLine19 m_objLine19;
		private clsPrintLine20 m_objLine20;
		private clsPrintLine21 m_objLine21;
		private clsPrintLine22 m_objLine22;
		private clsPrintLine23 m_objLine23;
		private clsPrintLine24 m_objLine24;
		private clsPrintLine25 m_objLine25;
		private clsPrintLine26 m_objLine26;
		private clsPrintLine27 m_objLine27;
		private clsPrintRecordSign m_objSign;
	
		#endregion 
		
		/// <summary>
		/// ��ӡԪ��
		/// </summary>
		private enum enmItemDefination
		{
			//����Ԫ��
			InPatientID_Title,
			InPatientID,
			Name_Title,
			Name,
			Sex_Title,
			Sex,
			Age_Title,
			Age,
			Dept_Name_Title,
			Dept_Name,
			BedNo_Title,
			BedNo,
            
			Page_HospitalName,
			Page_Name_Title,
			Page_Title,
			Page_Num,
			Page_Of,
			Page_Count,
					
			Print_Date_Title,
			Print_Date,
			
		}
	  
	
		#region �����ӡ��Ԫ�ص������
		protected class clsPrintPageSettingForRecord
		{	
			public clsPrintPageSettingForRecord(){}
			
			/// <summary>
			/// ��������
			/// </summary>
			/// <param name="p_intItemName">��Ŀ����</param>
			/// <returns></returns>
			public PointF m_getCoordinatePoint(int p_intItemName)
			{
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                    
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(320f,30f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(235f,70f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,120f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,120f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(160f,120f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(210f,120f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(240f,120f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(280f,120f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(365f,120f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(415f,120f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(570f,120f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(620f,120f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,120f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(710f,120f);
						break;
						//					case (int)enmItemDefination.BasePointOne:
						//						m_fReturnPoint = new PointF(20f,150f);
						//						break;
						//					case (int)enmItemDefination.BasePointTwo:
						//						m_fReturnPoint = new PointF(50f,150f);
						//						break;
						//					case (int)enmItemDefination.BasePointThree:
						//						m_fReturnPoint = new PointF(20f,250f);
						//						break;
						//					case (int)enmItemDefination.BasePointFour:
						//						m_fReturnPoint = new PointF(20f,280f);
						//						break;
						//					case (int)enmItemDefination.BasePointFive:
						//						m_fReturnPoint = new PointF(50f,280f);
						//						break;
						//					case (int)enmItemDefination.BasePointSix:
						//						m_fReturnPoint = new PointF(20f,600f);
						//						break;
						//					case (int)enmItemDefination.BasePointSeven:
						//						m_fReturnPoint = new PointF(50f,600f);
						//						break;
					default:
						m_fReturnPoint = new PointF(400f,400f);
						break;
	
				}
				return m_fReturnPoint;
			}
		}

	    #endregion
		#endregion
	
		#region ÿһ��ӡ�ڶεĴ���
		private void m_mthHandleOneEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			p_objGrp.DrawLine(Pens.Black,intX1+(int)enmRectangleInfo.ColumnsMark1,m_intPreY,intX1+(int)enmRectangleInfo.ColumnsMark1,p_intEndY);
			int intIndex=(p_intEndY-m_intPreY)/5;
			if((p_intEndY-m_intPreY)>80)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("ǰ",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*4);
			}
		
			m_intPreY = p_intEndY+(int)enmRectangleInfo.RowStep;
			m_intEndIndex++;
		}
		private void m_mthHandleTwoEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			p_objGrp.DrawLine(Pens.Black,intX1+(int)enmRectangleInfo.ColumnsMark1,m_intPreY+3,intX1+(int)enmRectangleInfo.ColumnsMark1,p_intEndY);
			int intIndex=(p_intEndY-m_intPreY)/5;
			if((p_intEndY-m_intPreY)>80)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*4);
			}
		
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleThreeEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY - 5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX;

			p_objGrp.DrawLine(Pens.Black,intX1+(int)enmRectangleInfo.ColumnsMark1,m_intPreY,intX1+(int)enmRectangleInfo.ColumnsMark1,p_intEndY);

			int intIndex=(p_intEndY-m_intPreY)/5;
			
			if((p_intEndY-m_intPreY)>80)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("¼",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*4);
			}			
			
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		
		#endregion 				
		
		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("��  ��  ��  ��  ��  ¼",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(m_objPrintInfo.m_strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAreaName,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo));
		
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
		}
	
		#endregion		

		#region print class 

		private class clsPrintLine1 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine1()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);

				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("��������:_________________________________________________����ҽ��:_____________________________",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					
					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,390,15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+530,p_intPosY,200,15);
				bool blnText2Out = m_objText2.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				//�����������������һ��,ֻ����һ��
				if(blnText1Out || blnText2Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				m_blnHaveMoreLine = false;
			
//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,p_objGrp);
//	
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//				}				
			}

			public override void m_mthReset()
			{
//				m_intTimes = 0;
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
							
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
						Object[] objData=(object[])value;
						m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString() ,objData[1].ToString(),m_dtmFirstPrintTime);							
						m_objText2.m_mthSetContextWithCorrectBefore(objData[2].ToString() ,"",m_dtmFirstPrintTime);
					}
				}
			}
		}
	

		private class clsPrintLine2 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine2()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("����ҽ��:___________________",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					
					m_blnFirstPrint = false;
				}

				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,p_objGrp);
	
				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

			}

			public override void m_mthReset()
			{
				m_intTimes = 0;
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
						Object objData=(object)value;
						m_objText1.m_mthSetContextWithCorrectBefore(objData.ToString() ,"",m_dtmFirstPrintTime);
						
					}
				}
			}
		}
	
	
		private class clsPrintLine3 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine3()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);

				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("����ʽ:_________________________________________________����ҽ��:_____________________________",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					
					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,390,15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+530,p_intPosY,200,15);
				bool blnText2Out = m_objText2.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				//�����������������һ��,ֻ����һ��
				if(blnText1Out || blnText2Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,p_objGrp);
//	
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//				}

			}

			public override void m_mthReset()
			{
//				m_intTimes = 0;
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
							
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
						Object[] objData=(object[])value;
						m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString() ,objData[1].ToString(),m_dtmFirstPrintTime);
						m_objText2.m_mthSetContextWithCorrectBefore(objData[2].ToString() ,"",m_dtmFirstPrintTime);
					}
				}
			}
		}
	

		private class clsPrintLine4 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine4()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("����ҽ����",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					
					m_blnFirstPrint = false;
				}

				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY,p_objGrp);
	
				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

			}

			public override void m_mthReset()
			{
				m_intTimes = 0;
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
						Object objData=(object)value;
						m_objText1.m_mthSetContextWithCorrectBefore(objData.ToString() ,"",m_dtmFirstPrintTime);
						
					}
				}
			}
		}
	

		private class clsPrintLine5 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strTime;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private string strGroup1;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine5()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{		
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("��������ʱ�䣺",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					if(strTime!=""&&strTime!=null)
					{
						p_objGrp.DrawString(DateTime.Parse(strTime).ToString("yyyy-MM-dd HH:mm"),fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+110,p_intPosY);
					}

					p_objGrp.DrawString("��־��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+295,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+310,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+355,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��˯",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+370,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+415,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
					p_objGrp.DrawString("����ʷ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+530,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+545,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+570,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�У�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+585,p_intPosY);

					if(strGroup1=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+295-10,p_intPosY-10);
					else if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+355-10,p_intPosY-10);
					else if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+415-10,p_intPosY-10);
				    if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+530-10,p_intPosY-10);
					else if(strGroup5=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+570-10,p_intPosY-10);
					
					m_blnPrintFirst=false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+615,p_intPosY,(int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+615)-5,15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				fntTick.Dispose();
				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+615)-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+615,p_intPosY,p_objGrp);
//  
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					
//				}			

			}
	
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine5=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine5[6].ToString(), objLine5[7].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("����ʷ��", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                            m_objText1.m_mthSetContextWithAllCorrect(objLine5[6].ToString(), objLine5[7].ToString());
						

						strTime=objLine5[0].ToString();
						strGroup1 =objLine5[1].ToString();
						strGroup2 =objLine5[2].ToString();
						strGroup3 =objLine5[3].ToString();
						strGroup4 =objLine5[4].ToString();
						strGroup5 =objLine5[5].ToString();
					
                        					
					}
				}
			}
	
		}
	
	
		private class clsPrintLine6 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private string strGroup6;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine6()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("������λ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����λ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+95,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����λ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+165,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����λ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+290,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ʯλ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+305,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��״��λ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+375,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����:",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+465,p_intPosY);
					if(strGroup1=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80-10,p_intPosY-10);
					else if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+150-10,p_intPosY-10);
					else if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-10,p_intPosY-10);
					else if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+290-10,p_intPosY-10);
					else if(strGroup5=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-10,p_intPosY-10);
					else if(strGroup6=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-10,p_intPosY-10);

					m_blnPrintFirst=false;


				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				fntTick.Dispose();
				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505,p_intPosY,p_objGrp);
//  
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					
//				}

			}
	
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[6].ToString(), objLine[7].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("������λ��", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[6].ToString(), objLine[7].ToString());
						
						
					
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						strGroup3 =objLine[2].ToString();
						strGroup4 =objLine[3].ToString();
						strGroup5 =objLine[4].ToString();
						strGroup6 =objLine[5].ToString();
                        					
					}
				}
			}
	
		}
	
		
		private class clsPrintLine7 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine7()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
							
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);

				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("�����䣺",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawString("��ʼʱ�䣺",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY);
					p_objGrp.DrawString("����ʱ�䣺",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY);
					p_objGrp.DrawString("����ʱ�䣺",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY);
					
					p_objGrp.DrawString(strGroup1,fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
					p_objGrp.DrawString(strGroup2,fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
					p_objGrp.DrawString(strGroup3,fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+620,p_intPosY);

					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+54,p_intPosY,90,15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				m_blnHaveMoreLine = false;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);

//				m_objText1.m_mthPrintLine(96-10,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+54,p_intPosY,p_objGrp);
//	
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
//				}

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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[0].ToString(), objLine[1].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("�����䣺", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[0].ToString(), objLine[1].ToString());
						
					
						strGroup1 =objLine[2].ToString();
						strGroup2 =objLine[3].ToString();
						strGroup3 =objLine[4].ToString();
						
					}
				}
			}
		}
	
		
		private class clsPrintLine8 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine8()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",12);

				if(m_blnFirstPrint)
				{
					p_objGrp.DrawString("�޾�����⣺  �ϸ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+3,p_intPosY);
				
					p_objGrp.DrawString("ǩ����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
					
					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 28;

				fntNormalText.Dispose();
				m_blnHaveMoreLine = false;
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY,p_objGrp);
//	
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);
//
//				}

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
						Object objData=(object)value;
                        if (m_blnIsPrintMark)
						    m_objText1.m_mthSetContextWithCorrectBefore(objData.ToString() ,"",m_dtmFirstPrintTime);
	                    else
                            m_objText1.m_mthSetContextWithAllCorrect(objData.ToString(), "");
					}
				}
			}
		}
	
				
		private class clsPrintLine9 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
		
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine9()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));				
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText3 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("ʹ�õ絶��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+95,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�� �ͺţ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+135,p_intPosY);


					p_objGrp.DrawString("˫��������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+345,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�� �ͺţ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+395,p_intPosY);

					p_objGrp.DrawString("��������ò�λ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY);			
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+330-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370-10,p_intPosY-10);


					m_blnPrintFirst =false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY,40,15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY,80,15);
				bool blnText2Out = m_objText2.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);

				rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670),15);
				bool blnText3Out = m_objText3.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out || blnText2Out || blnText3Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				fntTick.Dispose();
				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine(50-10,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY,p_objGrp);
//
//				m_objText2.m_mthPrintLine(90-10,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY,p_objGrp);
//
//				m_objText3.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,p_intPosY,p_objGrp);
//
//                
//				if(m_objText1.m_BlnHaveNextLine()||m_objText2.m_BlnHaveNextLine()||
//					m_objText3.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					
//				}
						
			}
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes =0;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
			
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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[2].ToString(), objLine[3].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("ʹ�õ絶�ͺţ�", m_objText1.m_ObjModifyUserArr);
                            m_objText2.m_mthSetContextWithCorrectBefore(objLine[6].ToString(), objLine[7].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("˫�������ͺţ�", m_objText2.m_ObjModifyUserArr);
                            m_objText3.m_mthSetContextWithCorrectBefore(objLine[8].ToString(), objLine[9].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("��������ò�λ��", m_objText3.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[2].ToString(), objLine[3].ToString());
                            m_objText2.m_mthSetContextWithAllCorrect(objLine[6].ToString(), objLine[7].ToString());
                            m_objText3.m_mthSetContextWithAllCorrect(objLine[8].ToString(), objLine[9].ToString());
                        }
					
						strGroup1 =objLine[0].ToString();

						strGroup2= objLine[1].ToString();
						strGroup3 =objLine[4].ToString();

						strGroup4= objLine[5].ToString();

                        						
					}
				}
			}
		}
	

		private class clsPrintLine10 : clsPrintLineBase
		{
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine10()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				p_objGrp.DrawString("��ǰ�����岿λƤ����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("���",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+165,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+225,p_intPosY);


				p_objGrp.DrawString("���󸺼��岿λƤ����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("���",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+465,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+510,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+525,p_intPosY);
			
				if(strGroup1 =="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+150-10,p_intPosY-10);

				if(strGroup2=="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210-10,p_intPosY-10);
				if(strGroup3=="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-10,p_intPosY-10);

				if(strGroup4=="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+510-10,p_intPosY-10);

				m_blnHaveMoreLine = false;

				p_intPosY += 30;
				
//				p_intPosY += (int)enmRectangleInfo.RowStep;

			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData27=(object[])value;
					strGroup1 =objData27[0].ToString();
					strGroup2=objData27[1].ToString ();
					strGroup3 =objData27[2].ToString();
					strGroup4=objData27[3].ToString ();
									
				}
			}
		}
	

		private class clsPrintLine11 : clsPrintLineBase
		{
			private string strGroup1;
			private string strGroup2;
			private clsPrintRichTextContext m_objText1;
		
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;

			public clsPrintLine11()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objCPaint=new clsPublicControlPaint ();
			
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				if(m_blnFirstPrint)
				{			
					p_objGrp.DrawString("ֹѪ����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ѫ��Ƥ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+95,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+185,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ѹֹѪ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY);

					p_objGrp.DrawString("��ѹֹѪ���ͺţ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY);
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+185-10,p_intPosY-10);

					
					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				fntNormalText.Dispose();
				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY,p_objGrp);
//	
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//				}

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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[2].ToString(), objLine[3].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("��ѹֹѪ���ͺţ�", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[2].ToString(), objLine[3].ToString());
					
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						
					}
				}
			}
		}
	

		private class clsPrintLine12 : clsPrintLineBase
		{
			private int intPreY=(int)enmRectangleInfo.TopY;
			public clsPrintLine12()
			{				
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);

				intPreY =p_intPosY-5 ;
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-5,(int)enmRectangleInfo.RightX,p_intPosY-5);

				p_objGrp.DrawString("��/˫֫��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+50,p_intPosY);
				p_objGrp.DrawString("����ʱ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY);
				p_objGrp.DrawString("����ʱ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+400,p_intPosY);
				p_objGrp.DrawString("��ʱ�䣨�֣�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY);
				p_objGrp.DrawString("ѹ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+680,p_intPosY);
				p_objGrp.DrawString("mmHg",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+680+20,p_intPosY+(int)enmRectangleInfo.RowStep);
				p_objGrp.DrawString("mmHg",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+680+20,p_intPosY+(int)enmRectangleInfo.RowStep*2);
				m_blnHaveMoreLine = false;
				fntNormalText.Dispose();
				p_intPosY += (int)enmRectangleInfo.RowStep+5;
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);
				
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY-10);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-10);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY-10);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,p_intPosY-10);

				
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				intPreY=(int)enmRectangleInfo.TopY;
				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					
									
				}
			}
		}
	

		private class clsPrintLine13 : clsPrintLineBase
		{
			private int intPreY=(int)enmRectangleInfo.TopY;
			
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private clsPrintRichTextContext m_objText4;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine13()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText2=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText3=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText4=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{				
				if(m_blnFirstPrint==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					intPreY=p_intPosY-10 ;

					p_intPosY -= 5;
				
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+10,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ǰ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+25,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+75,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+110,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+125,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+145,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+10-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+110-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+145-10,p_intPosY-10);
									
					m_blnFirstPrint =false;

					fntNormalText.Dispose();
					fntTick.Dispose();
				}
				
				m_objText1.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200+5,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350+5,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500+5,p_intPosY,p_objGrp);
				m_objText4.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650+5),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650+5,p_intPosY,p_objGrp);
			
				if(m_objText1.m_BlnHaveNextLine()||m_objText2.m_BlnHaveNextLine()||
					m_objText3.m_BlnHaveNextLine()||m_objText4.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);
                    
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,p_intPosY-10);

				}			

			}
			private int m_intTimes=0;
			public override void m_mthReset()
			{
				intPreY=(int)enmRectangleInfo.TopY;
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				m_objText4.m_mthRestartPrint();
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
                    if (m_blnIsPrintMark)
                    {
                        m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(), objData[5].ToString(), m_dtmFirstPrintTime);
                        m_objText2.m_mthSetContextWithCorrectBefore(objData[6].ToString(), objData[7].ToString(), m_dtmFirstPrintTime);
                        m_objText3.m_mthSetContextWithCorrectBefore(objData[8].ToString(), objData[9].ToString(), m_dtmFirstPrintTime);
                        m_objText4.m_mthSetContextWithCorrectBefore(objData[10].ToString(), objData[11].ToString(), m_dtmFirstPrintTime);
                    }
                    else
                    {
                        m_objText1.m_mthSetContextWithAllCorrect(objData[4].ToString(), objData[5].ToString());
                        m_objText2.m_mthSetContextWithAllCorrect(objData[6].ToString(), objData[7].ToString());
                        m_objText3.m_mthSetContextWithAllCorrect(objData[8].ToString(), objData[9].ToString());
                        m_objText4.m_mthSetContextWithAllCorrect(objData[10].ToString(), objData[11].ToString());
                    }

					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
									
				}
			}
		}
	
		
		private class clsPrintLine14 : clsPrintLineBase
		{
			private int intPreY=(int)enmRectangleInfo.TopY;

			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private clsPrintRichTextContext m_objText4;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine14()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText2=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText3=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText4=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{		
				if(m_blnFirstPrint==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					intPreY=p_intPosY-10 ;

					p_intPosY -= 5;
				
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+10,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ǰ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+25,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+75,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+110,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+125,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+145,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+10-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+110-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+145-10,p_intPosY-10);
				
					
					m_blnFirstPrint =false;

					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				m_objText1.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200+5,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350+5,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(150-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500+5,p_intPosY,p_objGrp);
				m_objText4.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650+5),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650+5,p_intPosY,p_objGrp);


			
				if(m_objText1.m_BlnHaveNextLine()||m_objText2.m_BlnHaveNextLine()||
					m_objText3.m_BlnHaveNextLine()||m_objText4.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);

					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY-10);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,intPreY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,p_intPosY-10);

				}			

			}
			private int m_intTimes=0;
			public override void m_mthReset()
			{
				intPreY=(int)enmRectangleInfo.TopY;
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				m_objText4.m_mthRestartPrint();
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
                    if (m_blnIsPrintMark)
                    {
                        m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(), objData[5].ToString(), m_dtmFirstPrintTime);
                        m_objText2.m_mthSetContextWithCorrectBefore(objData[6].ToString(), objData[7].ToString(), m_dtmFirstPrintTime);
                        m_objText3.m_mthSetContextWithCorrectBefore(objData[8].ToString(), objData[9].ToString(), m_dtmFirstPrintTime);
                        m_objText4.m_mthSetContextWithCorrectBefore(objData[10].ToString(), objData[11].ToString(), m_dtmFirstPrintTime);
                    }
                    else
                    {
                        m_objText1.m_mthSetContextWithAllCorrect(objData[4].ToString(), objData[5].ToString());
                        m_objText2.m_mthSetContextWithAllCorrect(objData[6].ToString(), objData[7].ToString());
                        m_objText3.m_mthSetContextWithAllCorrect(objData[8].ToString(), objData[9].ToString());
                        m_objText4.m_mthSetContextWithAllCorrect(objData[10].ToString(), objData[11].ToString());
                    }

					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
									
				}
			}
		}
	
		
		private class clsPrintLine15 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine15()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnPrintFirst==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("ͣ��Foley����ܣ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+130,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+145,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+290,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("˫ǻ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+305,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+345,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ǻ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����:",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+415,p_intPosY);					//					switch (strGroup1)
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+130-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+290-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+345-10,p_intPosY-10);
					if(strGroup5=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-10,p_intPosY-10);

					m_blnPrintFirst=false;
					
					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+455,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+455),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+455),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+455,p_intPosY,p_objGrp);
//  
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//				
//				}
			
			

			}
	
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[5].ToString(), objLine[6].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("ͣ��Foley����ܣ�", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[5].ToString(), objLine[6].ToString());
                        }
					
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						strGroup3 =objLine[2].ToString();
						strGroup4 =objLine[3].ToString();
						strGroup5 =objLine[4].ToString();
                        					
					}
				}
			}
	
		}
	

		private class clsPrintLine16 : clsPrintLineBase
		{
			
			private string strGroup1;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;		
					
			public clsPrintLine16()
			{
				m_objCPaint=new clsPublicControlPaint ();
				
						
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				p_objGrp.DrawString("ͣ��θ�ܣ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+95,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+170,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+185,p_intPosY);
				if(strGroup1 =="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80-10,p_intPosY-10);

				if(strGroup2=="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY-10);


				
				m_blnHaveMoreLine = false;

				fntNormalText.Dispose();
				fntTick.Dispose();

				p_intPosY += (int)enmRectangleInfo.RowStep+5;
						
			}
		
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
						
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
						Object[] objLine=(Object[])value ;
											
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						
          						
					}
				}
			}
		}
	

		private class clsPrintLine17 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine17()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnPrintFirst==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("Ƥ��������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("2������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+95,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("75���ƾ�",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+175,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("���ԭҺ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+265,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+345,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("���ϡ��Һ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����:",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+475,p_intPosY);					//					{
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+80-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+345-10,p_intPosY-10);
					if(strGroup5=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+460-10,p_intPosY-10);

					m_blnPrintFirst=false;
					
					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+515,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+515),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+515),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+515,p_intPosY,p_objGrp);
//  
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					
//				}
			
			

			}
	
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine[5].ToString(), objLine[6].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("Ƥ��������", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objLine[5].ToString(), objLine[6].ToString());
                        }
					
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						strGroup3 =objLine[2].ToString();
						strGroup4 =objLine[3].ToString();
						strGroup5 =objLine[4].ToString();
                        					
					}
				}
			}
	
		}
	

		private class clsPrintLine18 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private clsPrintRichTextContext m_objText4;
			private clsPrintRichTextContext m_objText5;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine18()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText2=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText3=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText4=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
				m_objText5=new clsPrintRichTextContext (Color.Black,new Font("SimSun",11));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("Ѫ��Ʒ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ȫѪ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+75,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+170,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ϸ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+185,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("Ѫ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+315,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+410,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������Ѫ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+425,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+565,p_intPosY);
					
					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY);

					p_objGrp.DrawString("��λ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+268,p_intPosY);

					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+390,p_intPosY);

					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+530,p_intPosY);

//					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+660,p_intPosY);

					
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+170-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+410-10,p_intPosY-10);
					if(strGroup5=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-10,p_intPosY-10);
					m_blnFirstPrint =false;

					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				m_objText1.m_mthPrintLine(37-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+75+38,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine(38-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+185+50,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(47-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+305+48,p_intPosY,p_objGrp);
				m_objText4.m_mthPrintLine(39-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+425+66,p_intPosY,p_objGrp);
				m_objText5.m_mthPrintLine(59-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+565+38,p_intPosY,p_objGrp);
				


			
				if(m_objText1.m_BlnHaveNextLine()||m_objText2.m_BlnHaveNextLine()||
					m_objText3.m_BlnHaveNextLine()||m_objText4.m_BlnHaveNextLine()||m_objText5.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

			}
			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				m_objText4.m_mthRestartPrint();
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
                    if (m_blnIsPrintMark)
                    {
                        m_objText1.m_mthSetContextWithCorrectBefore(objData[1].ToString(), objData[2].ToString(), m_dtmFirstPrintTime);
                        m_mthAddSign("ȫѪ��", m_objText1.m_ObjModifyUserArr);
                        m_objText2.m_mthSetContextWithCorrectBefore(objData[4].ToString(), objData[5].ToString(), m_dtmFirstPrintTime);
                        m_mthAddSign("��ϸ����", m_objText2.m_ObjModifyUserArr);
                        m_objText3.m_mthSetContextWithCorrectBefore(objData[7].ToString(), objData[8].ToString(), m_dtmFirstPrintTime);
                        m_mthAddSign("Ѫ����", m_objText3.m_ObjModifyUserArr);
                        m_objText4.m_mthSetContextWithCorrectBefore(objData[10].ToString(), objData[11].ToString(), m_dtmFirstPrintTime);
                        m_mthAddSign("������Ѫ��", m_objText4.m_ObjModifyUserArr);
                        m_objText5.m_mthSetContextWithCorrectBefore(objData[13].ToString(), objData[14].ToString(), m_dtmFirstPrintTime);
                        m_mthAddSign("������", m_objText5.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objText1.m_mthSetContextWithAllCorrect(objData[1].ToString(), objData[2].ToString());
                        m_objText2.m_mthSetContextWithAllCorrect(objData[4].ToString(), objData[5].ToString());
                        m_objText3.m_mthSetContextWithAllCorrect(objData[7].ToString(), objData[8].ToString());
                        m_objText4.m_mthSetContextWithAllCorrect(objData[10].ToString(), objData[11].ToString());
                        m_objText5.m_mthSetContextWithAllCorrect(objData[13].ToString(), objData[14].ToString());
                    }

					strGroup1=objData[0].ToString();
					strGroup2=objData[5].ToString();
					strGroup3=objData[0].ToString();
					strGroup4=objData[5].ToString();
					strGroup5=objData[0].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine19 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine19()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					Font fntNormalText = new Font("SimSun",11);

					p_objGrp.DrawString("����Һ������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY);
					p_objGrp.DrawString("����������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
					p_objGrp.DrawString("ml",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY);
					
					m_blnFirstPrint = false;

					fntNormalText.Dispose();
				}

				m_objText1.m_mthPrintLine(210-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+90,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY,p_objGrp);
	
				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

			}

			public override void m_mthReset()
			{
				m_intTimes = 0;
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
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("����Һ������", m_objText1.m_ObjModifyUserArr);
                            m_objText2.m_mthSetContextWithCorrectBefore(objData[2].ToString(), objData[3].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("����������", m_objText2.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                            m_objText2.m_mthSetContextWithAllCorrect(objData[2].ToString(), objData[3].ToString());
                        }
					}
				}
			}
		}
	

		private class clsPrintLine20 : clsPrintLineBase
		{
			
			private string strGroup1;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;
					
					
			public clsPrintLine20()
			{
				m_objCPaint=new clsPublicControlPaint ();
						
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				p_objGrp.DrawString("�˿������������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+130,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+145,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+195,p_intPosY);
				

				if(strGroup1 =="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+130-10,p_intPosY-10);

				if(strGroup2=="1")
					p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-10,p_intPosY-10);

				
				m_blnHaveMoreLine = false;

				fntNormalText.Dispose();
				fntTick.Dispose();

				p_intPosY += (int)enmRectangleInfo.RowStep+5;
						
			}
		
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
						
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
						Object[] objLine=(Object[])value ;
					
						strGroup1 =objLine[0].ToString();
						strGroup2 =objLine[1].ToString();
						
          						
					}
				}
			}
		}
	
			
		private class clsPrintLine21 : clsPrintLineBase
		{
//			private clsPrintRichTextContext[] m_objText;
			
//			private int intRows=0;
		
			private bool m_blnFirstPrint = true;

			private string[] m_strNameArr;
			private string[] m_strQuantityArr;

			private int m_intColumns = 0;
			private int m_intPerColumnLength = 0;
			private int m_intUsableLength = (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX - (int)enmRectangleInfo.ColumnsMark1 - 50;

			public clsPrintLine21()
			{
			
			}

			//			private int m_intTimes = 0;			

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					Font fntNormalText = new Font("SimSun",11);
					
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-5,(int)enmRectangleInfo.RightX,p_intPosY-5);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY+18,(int)enmRectangleInfo.RightX,p_intPosY+18);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY+40,(int)enmRectangleInfo.RightX,p_intPosY+40);

					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+10,p_intPosY);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+10,p_intPosY + (int)enmRectangleInfo.RowStep);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60,p_intPosY+40);

					if(m_intColumns <= 4)
					{
						m_intPerColumnLength = (int)(m_intUsableLength/4);
						p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength,p_intPosY+40);						
						p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*2,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*2,p_intPosY+40);
						p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*3,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*3,p_intPosY+40);

						if(m_strNameArr!=null)
						{
							for(int i=0;i<m_strNameArr.Length;i++)
							{
								p_objGrp.DrawString(m_strNameArr[i],fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60+10+m_intPerColumnLength*i,p_intPosY);
							}	
						}
						if(m_strQuantityArr!=null)
						{
							for(int i1=0;i1<m_strQuantityArr.Length;i1++)
							{
								p_objGrp.DrawString(m_strQuantityArr[i1],fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60+10+m_intPerColumnLength*i1,p_intPosY+25);
							}	
						}											
					}
					else
					{
						m_intPerColumnLength = (int)(m_intUsableLength/m_intColumns);
						for(int i2=1;i2<m_intColumns;i2++)
						{
							p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*i2,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1 + 60 + m_intPerColumnLength*i2,p_intPosY+40);
						}

						if(m_strNameArr!=null)
						{
							for(int i=0;i<m_strNameArr.Length;i++)
							{
								p_objGrp.DrawString(m_strNameArr[i],fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60+10+m_intPerColumnLength*i,p_intPosY);
							}	
						}
						if(m_strQuantityArr!=null)
						{
							for(int i1=0;i1<m_strQuantityArr.Length;i1++)
							{
								p_objGrp.DrawString(m_strQuantityArr[i1],fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60+10+m_intPerColumnLength*i1,p_intPosY+25);
							}	
						}		
					}
							
//					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY+(int)enmRectangleInfo.RowStep-5,(int)enmRectangleInfo.RightX,p_intPosY+(int)enmRectangleInfo.RowStep-5);
//					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY+(int)enmRectangleInfo.RowStep-5);

					fntNormalText.Dispose();

					p_intPosY += 50;

//					if(m_objText==null)
//					{
//						m_blnHaveMoreLine = false;
//						return ;
//					}
					m_blnFirstPrint = false;
//					if(m_objText.Length > 0)
//					{
//						m_blnHaveMoreLine = true;
//						intRows = 0;
//					}
//					else
//					{
//						m_blnHaveMoreLine = false;
//					}
					return;
				}

//				if(intRows< m_objText.Length)
//				{
//					m_objText[intRows].m_mthPrintLine(157-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+3,p_intPosY,p_objGrp);
//					m_objText[intRows+1].m_mthPrintLine(167-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+353,p_intPosY,p_objGrp);
//                    
//
//					p_intPosY +=(int)enmRectangleInfo.RowStep+5 ;	
//					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-(int)enmRectangleInfo.RowStep-15,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY-10);
//
//
//					if(m_objText[intRows].m_BlnHaveNextLine() || m_objText[intRows+1].m_BlnHaveNextLine())
//					{
//						
//						m_blnHaveMoreLine = true;
//					}
//					else
//					{
//						//����
//						p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY-10,(int)enmRectangleInfo.RightX,p_intPosY-10);
//						
//						//						p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//						intRows=intRows+2;
//					}
//
//					
//				}
//				else 

				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
//				intRows=0;
				//				m_intTimes = 0;
				
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
//				if(m_objText==null) return ;
//				for(int i=0;i<m_objText.Length;i++)
//				{
//					m_objText[i].m_mthRestartPrint();
//				}
							
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
						Object[][] objData=(Object[][])value ;
//						m_objText=new clsPrintRichTextContext[objData.Length];
						m_strNameArr = new string[objData.Length];
						m_strQuantityArr = new string[objData.Length];
						for(int i=0;i<objData.Length;i++)
						{
//							m_objText[i][0] = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
//							m_objText[i][0].m_mthSetContextWithCorrectBefore(objData[i].ToString(),"",m_dtmFirstPrintTime);							
							m_strNameArr[i] = objData[i][0].ToString();
							m_strQuantityArr[i] = objData[i][1].ToString();
						}

						m_intColumns = objData.Length;						
					}
				}
			}
		}
	
		
		private class clsPrintLine22 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine22()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnPrintFirst==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("ȫ��Ƥ�����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawString("����ǰ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+175,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY);
					p_objGrp.DrawString("Ƥ����������:________________________________________________",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+275,p_intPosY);					
					
					m_blnPrintFirst=false;

					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-10,p_intPosY-10);

					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				if(m_intTimes==0)
					m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+385),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+385,p_intPosY,p_objGrp);
				else
					m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100),(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY,p_objGrp);
  
				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep;
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY+15,(int)enmRectangleInfo.RightX,p_intPosY+15);					
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;				
				}
			}
	
			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine5=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine5[2].ToString(), objLine5[3].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("����ǰƤ������������", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objLine5[2].ToString(), objLine5[3].ToString());
                        }
					
						strGroup1 =objLine5[0].ToString();
						strGroup2 =objLine5[1].ToString();
                        					
					}
				}
			}
	
		}
	

		private class clsPrintLine23 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine23()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnPrintFirst==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("����������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+85,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+175,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY);
					p_objGrp.DrawString("Ƥ����������:________________________________________________",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+275,p_intPosY);
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-10,p_intPosY-10);
					m_blnPrintFirst=false;

					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				if(m_intTimes==0)
					m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+385),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+385,p_intPosY,p_objGrp);
				else
					m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100),(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY,p_objGrp);
  
				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep;
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY+15,(int)enmRectangleInfo.RightX,p_intPosY+15);					
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;				
				}
			}
	
			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnPrintFirst=true;
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
						Object[] objLine5=(Object[])value ;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine5[2].ToString(), objLine5[3].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("������Ƥ������������", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objLine5[2].ToString(), objLine5[3].ToString());
                        }
						strGroup1 =objLine5[0].ToString();
						strGroup2 =objLine5[1].ToString();
                        					
					}
				}
			}
	
		}
	

		private class clsPrintLine24 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine24()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnPrintFirst==true)
				{
					Font fntNormalText = new Font("SimSun",11);
					Font fntTick = new Font("SimSun",18);

					p_objGrp.DrawString("�걾��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+40,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("���没����",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+55,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������Ƭ",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+175,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ϸ������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+265,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+355,p_intPosY);
					if(strGroup1 =="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+40-10,p_intPosY-10);

					if(strGroup2=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-10,p_intPosY-10);
					if(strGroup3=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250-10,p_intPosY-10);

					if(strGroup4=="1")
						p_objGrp.DrawString("��",fntTick ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-10,p_intPosY-10);

					m_blnPrintFirst=false;
		
					fntNormalText.Dispose();
					fntTick.Dispose();
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+395,p_intPosY,(int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+395),15);
				bool blnText1Out = m_objText1.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,true);
				
				if(blnText1Out) p_intPosY += 35;
				else p_intPosY += 25;

				m_blnHaveMoreLine = false;

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+395),(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+395,p_intPosY,p_objGrp);
//  
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					//					m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
//				}
			}
	
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
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
                        Object[] objLine5 = (Object[])value;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objLine5[4].ToString(), objLine5[5].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("�걾��", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                            m_objText1.m_mthSetContextWithAllCorrect(objLine5[4].ToString(), objLine5[5].ToString());
                        strGroup1 = objLine5[0].ToString();
                        strGroup2 = objLine5[1].ToString();
                        strGroup3 = objLine5[2].ToString();
                        strGroup4 = objLine5[3].ToString();

                    }
				}
			}
	
		}
	

		private class clsPrintLine25 : clsPrintLineBase
		{
			
			private string strGroup1;
			private string strGroup2;
			private string strGroup3;
			private clsPublicControlPaint m_objCPaint;
					
					
			public clsPrintLine25()
			{
				m_objCPaint=new clsPublicControlPaint ();
						
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntNormalText = new Font("SimSun",11);
				Font fntTick = new Font("SimSun",18);

				p_objGrp.DrawString("�����ͻأ�",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��������",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+115,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("ICU",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+215,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",new Font("SimSun",11) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+265,p_intPosY);
			
				if(strGroup1 =="1")
					p_objGrp.DrawString("��",new Font("SimSun",18) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-10,p_intPosY-10);

				if(strGroup2=="1")
					p_objGrp.DrawString("��",new Font("SimSun",18) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-10,p_intPosY-10);
				if(strGroup3=="1")
					p_objGrp.DrawString("��",new Font("SimSun",18) ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250-10,p_intPosY-10);


				
				m_blnHaveMoreLine = false;
				fntNormalText.Dispose();
				fntTick.Dispose();

				p_intPosY += (int)enmRectangleInfo.RowStep+5;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
						
			}
		
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
						
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
						Object[] objLine5=(Object[])value ;
				
						strGroup1 =objLine5[0].ToString();
						strGroup2 =objLine5[1].ToString();
						strGroup3 =objLine5[2].ToString();
						
          						
					}
				}
			}
		}
	

		private class clsPrintLine26 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine26()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
			
			}

//			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{				
					m_blnFirstPrint = false;
				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1,p_intPosY,(int)enmRectangleInfo.RightX - 50,(int)enmRectangleInfo.BottomY-25-p_intPosY);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock,p_objGrp,out intRealHeight,false);
				
				p_intPosY = (int)enmRectangleInfo.BottomY - 25;

                //p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY,(int)enmRectangleInfo.RightX,p_intPosY);

				m_blnHaveMoreLine = false;

                p_intPosY = (int)enmRectangleInfo.BottomY;

				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);

//				m_objText1.m_mthPrintLine((int)enmRectangleInfo.RightX-5-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80),(int)enmRectangleInfo.LeftX+80,p_intPosY,p_objGrp);
//	            
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY+(int)enmRectangleInfo.RowStep-5,(int)enmRectangleInfo.RightX,p_intPosY+(int)enmRectangleInfo.RowStep-5);
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
//				}

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
						Object[] objData=(object[])value;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), m_dtmFirstPrintTime, true);
                            m_mthAddSign("�����¼��", m_objText1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                        }
					}
				}
			}
		}
	

		private class clsPrintLine27 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
            private clsPrintRichTextContext m_objText4;
            private clsPrintRichTextContext m_objText5;
            private clsPrintRichTextContext m_objText6;
		
			private bool m_blnFirstPrint = true;

			public clsPrintLine27()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
				m_objText3 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",11));
                m_objText4 = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText5 = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
                m_objText6 = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 11));
			
			}

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					Font fntNormalText = new Font("SimSun",11);

					p_intPosY += 3;

					p_objGrp.DrawString("��¼��ʿ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+180,p_intPosY);
					p_objGrp.DrawString("ϴ�ֻ�ʿ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+480,p_intPosY);
					p_objGrp.DrawString("Ѳ�ػ�ʿ��",fntNormalText ,Brushes.Black,(int)enmRectangleInfo.LeftX+180,p_intPosY + 25);
                    p_objGrp.DrawString("����ҽʦ��", fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 480, p_intPosY + 25);
                    p_objGrp.DrawString("����ҽ����", fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX +180, p_intPosY + 50);
                    p_objGrp.DrawString("�޾���⣺", fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 480, p_intPosY + 50);
					m_blnFirstPrint = false;

					fntNormalText.Dispose();
				}

				m_objText1.m_mthPrintLine(150,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine(150,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+520,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(150,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY + 25,p_objGrp);
                m_objText4.m_mthPrintLine(150, (int)enmRectangleInfo.LeftX + (int)enmRectangleInfo.ColumnsMark1 + 520, p_intPosY + 25, p_objGrp);
                m_objText5.m_mthPrintLine(150, (int)enmRectangleInfo.LeftX + (int)enmRectangleInfo.ColumnsMark1 + 220, p_intPosY + 50, p_objGrp);
                m_objText6.m_mthPrintLine(150, (int)enmRectangleInfo.LeftX + (int)enmRectangleInfo.ColumnsMark1 + 520, p_intPosY + 50, p_objGrp);
	
				if(m_objText1.m_BlnHaveNextLine()||m_objText2.m_BlnHaveNextLine()||m_objText3.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

			}

			public override void m_mthReset()
			{
				m_intTimes = 0;
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
                m_objText4.m_mthRestartPrint();
                m_objText5.m_mthRestartPrint();
                m_objText6.m_mthRestartPrint();
							
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
						Object[] objData=(object[])value;
                        if (m_blnIsPrintMark)
                        {
                            m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString(), "", m_dtmFirstPrintTime);
                            m_objText2.m_mthSetContextWithCorrectBefore(objData[1].ToString(), "", m_dtmFirstPrintTime);
                            m_objText3.m_mthSetContextWithCorrectBefore(objData[2].ToString(), "", m_dtmFirstPrintTime);
                            m_objText4.m_mthSetContextWithCorrectBefore(objData[3].ToString(), "", m_dtmFirstPrintTime);
                            m_objText5.m_mthSetContextWithCorrectBefore(objData[4].ToString(), "", m_dtmFirstPrintTime);
                            m_objText6.m_mthSetContextWithCorrectBefore(objData[5].ToString(), "", m_dtmFirstPrintTime);
                        }
                        else
                        {
                            m_objText1.m_mthSetContextWithAllCorrect(objData[0].ToString(), "");
                            m_objText2.m_mthSetContextWithAllCorrect(objData[1].ToString(), "");
                            m_objText3.m_mthSetContextWithAllCorrect(objData[2].ToString(), "");
                            m_objText4.m_mthSetContextWithAllCorrect(objData[3].ToString(), "");
                            m_objText5.m_mthSetContextWithAllCorrect(objData[4].ToString(), "");
                            m_objText6.m_mthSetContextWithAllCorrect(objData[5].ToString(), "");
                        }
						
					}
				}
			}
		}
	
		
		#endregion 
		
		#endregion

//		/// <summary>
//		/// Σ�ػ���Ĵ�ӡ��Ϣ.
//		/// </summary>
//		[Serializable]			
//		private class clsPrintInfo_OperationRecord
//		{
//			public string m_strInPatentID;			
//			public string m_strPatientName;
//			public string m_strSex;
//			public string m_strAge;
//			public string m_strBedName;
//			public string m_strDeptName;
//			public string m_strAreaName;	
//			public DateTime m_dtmInPatientDate;
//			public DateTime m_dtmOpenDate;
//
//			public clsOperationRecord_All m_objclsOperationRecord_All;			
//		}

		#region ���ⲿ���Ա���ӡ����ʾʵ��.	
		//		using System.IO;
		//		using System.Runtime.Serialization;
		//		System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
		//		private void m_mthfrmLoad()
		//		{	
		//			this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
		//			this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
		//			this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
		//			this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
		//		}
		//		private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		//		{			
		//			objPrintTool.m_mthPrintPage(e);
		//		}
		//
		//		private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//		{
		//			objPrintTool.m_mthBeginPrint(e);				
		//		}
		//
		//		private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		//		{
		//			objPrintTool.m_mthEndPrint(e);
		//		}
		//
		//		clsOperationRecordPrintTool objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsOperationRecordPrintTool();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//			if(m_objBaseCurrentPatient==null)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
		//			else if(this.trvTime.SelectedNode ==null || this.trvTime.SelectedNode==trvTime.Nodes[0])
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
		//			else 
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.Parse(trvTime.SelectedNode.Tag.ToString()));
		//										
		//			objPrintTool.m_mthInitPrintContent();	
		//			
		//			//���浽�ļ�
		//			object objtemp=objPrintTool.m_objGetPrintInfo();
		//			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		//				
		//			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Create);
		//				
		//			objForm.Serialize(objStream,objtemp);
		//				
		//			objStream.Flush();
		//			objStream.Close();
		//						
		//			m_mthStartPrint();
		//		}
		//		private void m_mthDemoPrint_FromFile()
		//		{	
		//			objPrintTool=new clsOperationRecordPrintTool();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//		
		//			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		//			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Open);
		//			object objtemp = objForm.Deserialize(objStream);//
		//			objStream.Close();
		//				
		//			objPrintTool.m_mthSetPrintContent(objtemp);		
		//		
		//			m_mthStartPrint();
		//		}
		//		private void m_mthStartPrint()
		//		{			
		//			PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
		//			ppdPrintPreview.Document = m_pdcPrintDocument;
		//			ppdPrintPreview.ShowDialog();
		//		}
		//		bool bbb=true;
		//		protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
		//		{
		//			if(bbb)
		//				m_mthDemoPrint_FromDataSource();
		//			else m_mthDemoPrint_FromFile();
		//			bbb= !bbb;
		//			return 1;
		//		}
		#endregion ���ⲿ���Ա���ӡ����ʾʵ��.
	}	
}



