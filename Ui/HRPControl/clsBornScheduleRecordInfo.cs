using System;
using System.Collections ;
namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// clsBornScheduleRecordInfo ��ժҪ˵����
	/// </summary>
	public class clsBornScheduleRecordInfo
	{
		public clsBornScheduleRecordInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
		}
		public string m_strINPATIENTID      ; //����ID
		public DateTime m_dtmINPATIENTDATE  ;//��Ժʱ��
		public DateTime m_dtmOPENDATE     ;  //������
		public DateTime m_dtmCHILDBIRTHDATE ;//��������
		public string	m_strCREATEID     ;  //�����û�ID
		public DateTime m_dtmMODIFYDATE   ;   //�޸�����
		public string m_strFIRSTPOINT; // ��һ��
		public string m_strSECONDPOINT; // �ڶ���
		public string m_strTHREEPOINT; // ������
		public string m_strFOUTPOINT; // ���ĵ�
		public DateTime m_dtmFORECASTDATE   ; //Ԥ����
		public string m_strPREGNANCYNUM     ; //�в���
		public string m_strVENTERPOINTXML   ; //���ڵ㼯��
		public string m_strCHECKVENTERTIMEXML    ;  //���ʱ��
		public string m_strBLOODPRESSUREXML ;  //Ѫѹ
		public string m_strEMBRYOHEARTXML ; //̥��
		public string m_strVENTERSCALEXML ;  //����
		public string m_strEXCEPTIONNOTEXML    ;  //�쳣���
		public string m_strDEALNOTEXML    ;  //�����¼
		public string m_strSIGNXML        ;  //ǩ����
	}

	//һ���������̼�¼
	public class clsBornRecordManager //:IComparable 
	{
		//����ID
		public string  m_strINPATIENTID;

		//��Ժʱ��
		public DateTime  m_dtmINPATIENTDATE;

		//��ʱ��
		public DateTime  m_dtmOPENDATE;

		//��������
		public DateTime  m_dtmCHILDBIRTHDATE;

		//�����û�ID
		public string  m_dtmCREATEID;

		//�޸�����
		public DateTime m_dtmMODIFYDATE   ;  

		//Ԥ����
		public DateTime m_dtmFORECASTDATE   ;

		//�в���
		public string m_strPREGNANCYNUM     ;

		//Ĭ��ֵ�ĵ�һ��
		public string m_strFIRSTPOINT;

		//Ĭ��ֵ�ĵڶ���
		public string m_strSECONDPOINT ;

		//Ĭ��ֵ�ĵ�����
		public string m_strTHREEPOINT;

		//Ĭ��ֵ�ĵ��ĵ�
		public string m_strFOUTPOINT;

		//����һ����̼�¼��
		 public ArrayList  m_arlBornScheduleEveryDay; 

		//��ղ��̼�¼
		public void m_mthClear()
		{
			m_arlBornScheduleEveryDay.Clear();
		}

		//���캯��
		public clsBornRecordManager()
		{
			m_arlBornScheduleEveryDay=new ArrayList();
		}

		//����µ�����

		public clsBornScheduleEveryDay m_objAddDayRecord(DateTime p_dtmRecordDate,out bool p_blnIsAddNew)
		{
			int intIndex=m_intLastIndexOf(p_dtmRecordDate);

			if(intIndex>=0)
			{
				p_blnIsAddNew=false;
				return (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[intIndex];
			}

			p_blnIsAddNew=true;

			clsBornScheduleEveryDay objRecord=new clsBornScheduleEveryDay(p_dtmRecordDate);

			objRecord.m_dtmRecordDate=p_dtmRecordDate.Date ;

			m_arlBornScheduleEveryDay.Add(objRecord);

		//	m_arlBornScheduleEveryDay.Sort();

		


			return objRecord;

		 
		}

		//��ȥָ������

		public clsBornScheduleEveryDay m_objReMoveRecord(DateTime p_dtmRecordDate)
		{
				clsBornScheduleEveryDay objRemove=null;

			for(int i=0; i<m_arlBornScheduleEveryDay.Count ;i++)
			{
				clsBornScheduleEveryDay objRecord=(clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[i];

				if(objRecord.m_dtmRecordDate ==p_dtmRecordDate.Date)
				{
					objRemove=objRecord;
					m_arlBornScheduleEveryDay.RemoveAt(i);
					break;
				}
			}
			return objRemove;

		}

		//��ȥָ�����ڵļ�¼

		public  bool m_bnlRemove(clsBornScheduleEveryDay p_objRecord)
		{
			if(!m_arlBornScheduleEveryDay.Contains(p_objRecord))
				return false;

			m_arlBornScheduleEveryDay.Remove(p_objRecord);

			return true;
		}

		//�ж��Ƿ���ָ�����ڼ�¼

		public bool m_bnlContain(clsBornScheduleEveryDay p_objRecord)
		{
			return m_arlBornScheduleEveryDay.Contains(p_objRecord);
		}

		//��ȡָ���±�ļ�¼

		public clsBornScheduleEveryDay this[int p_intIndex]
		{
			get
			{

				if(p_intIndex>=m_arlBornScheduleEveryDay.Count)
					return null;

				return (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[p_intIndex];
			}
		}


		/// <summary>
		/// ��ȡָ�����ڵ�������Ϣ
		/// </summary>
		public clsBornScheduleEveryDay this[DateTime p_dtmRecordDate]
		{
			get
			{
				clsBornScheduleEveryDay objResultRecord = null;
				if(m_arlBornScheduleEveryDay!=null)
				{
					for(int i=0;i<m_arlBornScheduleEveryDay.Count;i++)
					{
						clsBornScheduleEveryDay objRecord = (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[i];

						if(objRecord.m_dtmRecordDate == p_dtmRecordDate.Date)
						{
							objResultRecord = objRecord;
							break;
						}
					}	
				}

				return objResultRecord;
			}
		}

		/// <summary>
		/// ��ȡָ�����ڵ�������Ϣ���±�
		/// </summary>
		/// <param name="p_dtmRecordDate">����</param>
		/// <returns></returns>
		public int m_intIndexOf(DateTime p_dtmRecordDate)
		{
			int intIndex = -1;

			for(int i=0;i<m_arlBornScheduleEveryDay.Count;i++)
			{
				clsBornScheduleEveryDay objRecord = (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[i];

				if(objRecord.m_dtmRecordDate == p_dtmRecordDate)
				{
					intIndex = i;
					break;
				}
			}
			return intIndex;
		}




		/// <summary>
		/// �Ӻ��ȡָ�����ڵ�������Ϣ���±�
		/// </summary>
		/// <param name="p_dtmRecordDate"></param>
		/// <returns></returns>
		public int m_intLastIndexOf(DateTime p_dtmRecordDate)
		{
			int intIndex = -1;
			if(m_arlBornScheduleEveryDay !=null)
			{
				for(int i=m_arlBornScheduleEveryDay.Count-1;i>=0;i--)
				{
					clsBornScheduleEveryDay objRecord = (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[i];

					if(objRecord.m_dtmRecordDate == p_dtmRecordDate)
					{
						intIndex = i;
						break;
					}
				}
			}
			return intIndex;
		}

		/// <summary>
		/// ���ڵ�����
		/// </summary>
		public int m_IntRecordCount
		{
			get
			{
				return m_arlBornScheduleEveryDay.Count;
			}
		}	

		/// <summary>
		/// �жϸ�������Ϣ�Ƿ����һ��������Ϣ
		/// </summary>
		/// <param name="p_objRecord">������Ϣ</param>
		/// <returns></returns>
		public bool m_blnIsLast(clsBornScheduleEveryDay p_objRecord)
		{
			if(m_arlBornScheduleEveryDay.Count <= 0)
				return false;

			clsBornScheduleEveryDay objRecord = (clsBornScheduleEveryDay)m_arlBornScheduleEveryDay[m_arlBornScheduleEveryDay.Count-1];

			return objRecord == p_objRecord;
				
		}
	}

	public enum enmParamTime24
	{
		am1 = 1,
		am2 = 2,
		am3 = 3,
		am4 = 4,
		am5 = 5,
		am6 = 6,
		am7 = 7,
		am8 = 8,
		am9 = 9,
		am10 = 10,
		am11 = 11,
		am12 = 12,
		pm1 = 13,
		pm2 = 14,
		pm3 = 15,
		pm4 = 16,
		pm5 = 17,
		pm6 = 18,
		pm7 = 19,
		pm8 = 20,
		pm9 = 21,
		pm10 = 22,
		pm11 = 23,
		pm12 = 24,
	
	}


	//����ÿ�����ݼ�
	public class clsBornScheduleEveryDay
	{
		 //����ÿСʱ���㼯
		public ArrayList m_arlBornScheduleEveryHourCol;

		//���ʱ�伯
		public ArrayList m_arlCheckTimeCol;

		//Ѫѹ��
		public ArrayList m_arlBloodPressureCol;

		//̥�ļ�
		public ArrayList m_arlEmbryoHeartCol;

		//������
		public ArrayList m_arlVenterScaleExtendCol;

		//�쳣�����
		public ArrayList m_arlExceptionNoteCol;

		//�����¼��
		public ArrayList m_arlDealNoteCol;

		//ǩ����
		public ArrayList m_arlSignNameCol;

		public DateTime m_dtmRecordDate;


		//���캯��
		public clsBornScheduleEveryDay(DateTime p_dtmRecordTime)
		{
			 this.m_dtmRecordDate = p_dtmRecordTime;
			m_arlBornScheduleEveryHourCol=new ArrayList();
			m_arlCheckTimeCol=new ArrayList();
			m_arlBloodPressureCol=new ArrayList();
			m_arlEmbryoHeartCol=new ArrayList();
			m_arlVenterScaleExtendCol=new ArrayList();
			m_arlExceptionNoteCol=new ArrayList();
			m_arlDealNoteCol=new ArrayList();
			m_arlSignNameCol=new ArrayList();

		}

		public int CompareTo(object p_objother)
		{
			clsBornScheduleEveryDay objRecord=(clsBornScheduleEveryDay)p_objother;
			if(this.m_dtmRecordDate>objRecord.m_dtmRecordDate)
				return 1;
			else if(this.m_dtmRecordDate==objRecord.m_dtmRecordDate)
				return 0;
			else 
				return -1;

		}

		//��ӻ��㼯
		public void m_thAddBornScheduleEveryHour(int p_intHourValue,clsBornScheduleEveryHourCol p_objRecordValue)
		{
			 if(p_intHourValue<=0)
			 return ;

			ArrayList arTempSort=new ArrayList();
			ArrayList arTemp=new ArrayList();
			
			for(int i=0;i<m_arlBornScheduleEveryHourCol.Count ;i++)
			{
				clsBornScheduleEveryHourCol objRecord=(clsBornScheduleEveryHourCol)m_arlBornScheduleEveryHourCol[i];
		
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlBornScheduleEveryHourCol[i]=p_objRecordValue; //�ҵ�,����ֵ
					return;
				}

			}
		
			m_arlBornScheduleEveryHourCol.Add(p_objRecordValue);

			for(int i=0;i<m_arlBornScheduleEveryHourCol.Count ;i++)
			{
				clsBornScheduleEveryHourCol objRecord=(clsBornScheduleEveryHourCol)m_arlBornScheduleEveryHourCol[i];
			
				arTempSort.Add(objRecord.m_intHourValue);
			}
			arTempSort.Sort();

			for(int i=0;i<arTempSort.Count ;i++)
			{
				

				for(int j=0;j<m_arlBornScheduleEveryHourCol.Count ;j++)
				{
					clsBornScheduleEveryHourCol objRecordTemp=(clsBornScheduleEveryHourCol)m_arlBornScheduleEveryHourCol[j];
					if(objRecordTemp.m_intHourValue==(int)arTempSort[i])
					{
						arTemp.Add(objRecordTemp);
						m_arlBornScheduleEveryHourCol.Remove(objRecordTemp);
						break;
					}

				}
				
			}
			for(int i=0;i<arTemp.Count ;i++)
			{
				m_arlBornScheduleEveryHourCol.Add(arTemp[i]);
			
			}
			//m_arlBornScheduleEveryHourCol.Add(p_objRecordValue);



		//	m_arlBornScheduleEveryHourCol.Sort();

		}


		//ɾ�����㼯

		public void m_thRemoveBornScheduleEveryHour(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlBornScheduleEveryHourCol.Count ;i++)
			{
				clsBornScheduleEveryHourCol objRecord=(clsBornScheduleEveryHourCol)m_arlBornScheduleEveryHourCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlBornScheduleEveryHourCol.RemoveAt(i);
					break;
				}

			}
			//m_arlBornScheduleEveryHourCol.Sort();

		}


		//��Ӽ��ʱ�伯
		public void m_thAddCheckTime(int p_intHourValue,clsCheckTimeCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlCheckTimeCol.Count ;i++)
			{
				clsCheckTimeCol objRecord=(clsCheckTimeCol)m_arlCheckTimeCol[i];
			
				//���ʱ���ж��Ƿ�Ϊ��,��Ϊ�ղ������
				if(objRecord.m_intHourValue==p_intHourValue)//objRecord.m_intHourValue==p_intHourValue
				{
					if(p_objRecordValue.m_strCheckTime!=null)
						m_arlCheckTimeCol[i] = p_objRecordValue;
					return;
				}

			}
			
			m_arlCheckTimeCol.Add(p_objRecordValue);

			//m_arlCheckTimeCol.Sort();

		}


		//ɾ�����ʱ�伯

		public void m_thRemoveCheckTime(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlCheckTimeCol.Count ;i++)
			{
				clsCheckTimeCol objRecord=(clsCheckTimeCol)m_arlCheckTimeCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlCheckTimeCol.RemoveAt(i);
					break;
				}

			}
			//m_arlCheckTimeCol.Sort();

		}

		//���Ѫѹ��
		public void m_thAddBloodPressure(int p_intHourValue,clsBloodPressureCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlBloodPressureCol.Count ;i++)
			{
				clsBloodPressureCol objRecord=(clsBloodPressureCol)m_arlBloodPressureCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strScaleBloodPressureValue!=null || p_objRecordValue.m_strExtendBloodPressureValue!=null)
					m_arlBloodPressureCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlBloodPressureCol.Add(p_objRecordValue);

			//m_arlBloodPressureCol.Sort();

		}


		//ɾ��Ѫѹ��

		public void m_thRemoveBloodPressure(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlBloodPressureCol.Count ;i++)
			{
				clsBloodPressureCol objRecord=(clsBloodPressureCol)m_arlBloodPressureCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlBloodPressureCol.RemoveAt(i);
					break;
				}

			}
			//m_arlBloodPressureCol.Sort();

		}


		//���̥�ļ�
		public void m_thAddEmbryoHeart(int p_intHourValue,clsEmbryoHeartCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlEmbryoHeartCol.Count ;i++)
			{
				clsEmbryoHeartCol objRecord=(clsEmbryoHeartCol)m_arlEmbryoHeartCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strEmbryoHeartValue!=null)
					m_arlEmbryoHeartCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlEmbryoHeartCol.Add(p_objRecordValue);

			//m_arlEmbryoHeartCol.Sort();

		}


		//ɾ��̥�ļ�

		public void m_thRemoveEmbryoHeart(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlEmbryoHeartCol.Count ;i++)
			{
				clsEmbryoHeartCol objRecord=(clsEmbryoHeartCol)m_arlEmbryoHeartCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlEmbryoHeartCol.RemoveAt(i);
					break;
				}

			}
			//m_arlEmbryoHeartCol.Sort();

		}


		//��ӹ�����
		public void m_thAddVenterScaleExtend(int p_intHourValue,clsVenterScaleExtendCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlVenterScaleExtendCol.Count ;i++)
			{
				clsVenterScaleExtendCol objRecord=(clsVenterScaleExtendCol)m_arlVenterScaleExtendCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strScaleVenterValue!=null || p_objRecordValue.m_strExtendVenterValue!=null)
					m_arlVenterScaleExtendCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlVenterScaleExtendCol.Add(p_objRecordValue);

			//m_arlVenterScaleExtendCol.Sort();

		}


		//ɾ��������

		public void m_thRemoveVenterScaleExtend(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlVenterScaleExtendCol.Count ;i++)
			{
				clsVenterScaleExtendCol objRecord=(clsVenterScaleExtendCol)m_arlVenterScaleExtendCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlVenterScaleExtendCol.RemoveAt(i);
					break;
				}

			}
			//m_arlVenterScaleExtendCol.Sort();

		}

		//����쳣�����
		public void m_thAddExceptionNote(int p_intHourValue,clsExceptionNoteCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlExceptionNoteCol.Count ;i++)
			{
				clsExceptionNoteCol objRecord=(clsExceptionNoteCol)m_arlExceptionNoteCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strExceptionNoteValue!=null)
					m_arlExceptionNoteCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlExceptionNoteCol.Add(p_objRecordValue);

			//m_arlExceptionNoteCol.Sort();

		}


		//ɾ���쳣�����

		public void m_thRemoveExceptionNote(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlExceptionNoteCol.Count ;i++)
			{
				clsExceptionNoteCol objRecord=(clsExceptionNoteCol)m_arlExceptionNoteCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlExceptionNoteCol.RemoveAt(i);
					break;
				}

			}
			//m_arlExceptionNoteCol.Sort();

		}


		//��Ӵ����¼��
		public void m_thAddDealNote(int p_intHourValue,clsDealNoteCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlDealNoteCol.Count ;i++)
			{
				clsDealNoteCol objRecord=(clsDealNoteCol)m_arlDealNoteCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strDealNoteValue!=null)
					m_arlDealNoteCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlDealNoteCol.Add(p_objRecordValue);

			//m_arlDealNoteCol.Sort();

		}


		//ɾ��ǩ����

		public void m_thRemovemDealNote(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlDealNoteCol.Count ;i++)
			{
				clsDealNoteCol objRecord=(clsDealNoteCol)m_arlDealNoteCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlDealNoteCol.RemoveAt(i);
					break;
				}

			}
			//m_arlDealNoteCol.Sort();

		}


		//���ǩ����
		public void m_thAddSignName(int p_intHourValue,clsSignNameCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlSignNameCol.Count ;i++)
			{
				clsSignNameCol objRecord=(clsSignNameCol)m_arlSignNameCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					if(p_objRecordValue.m_strSignNameID!=null)
					m_arlSignNameCol[i]=p_objRecordValue;
					return;
				}

			}
			
			m_arlSignNameCol.Add(p_objRecordValue);

		//	m_arlSignNameCol.Sort();

		}


		//ɾ�������¼��

		public void m_thRemoveSignName(int p_intHourValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlSignNameCol.Count ;i++)
			{
				clsSignNameCol objRecord=(clsSignNameCol)m_arlSignNameCol[i];
			
				if(objRecord.m_intHourValue==p_intHourValue)
				{
					m_arlSignNameCol.RemoveAt(i);
					break;
				}

			}
		//	m_arlSignNameCol.Sort();

		}




	}



	//ÿСʱ����ֵ��
	public class clsBornScheduleEveryHourCol //: IComparable 
	{
	
		//Сʱֵ
		public int m_intHourValue;

		//����ֵ
		public int m_intVenterValue;

		//�Ƿ�����һ��ֵ
		public bool m_bnlIsHavePreValue;

		//�޸��û� 
		public string m_strModifyUserID;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//�Ƿ�ɾ��
		public bool m_bnlIsDelete;


		
	}

	//���ʱ�伯
	public class clsCheckTimeCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//���ʱ��
		public string m_strCheckTime;

	}

	//Ѫѹ��
	public class clsBloodPressureCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//����ѹ
		public string  m_strScaleBloodPressureValue;

		//����ѹ
		public string  m_strExtendBloodPressureValue;
	}

	//̥�ļ�
	public class clsEmbryoHeartCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//̥��
		public string  m_strEmbryoHeartValue;
	}

	//������
	public class clsVenterScaleExtendCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//����ѹ
		public string  m_strScaleVenterValue;

		//����ѹ
		public string  m_strExtendVenterValue;
	}

	//�쳣�����
	public class clsExceptionNoteCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//�쳣���
		public string  m_strExceptionNoteValue;
	}

	//�����¼��
	public class clsDealNoteCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//�����¼
		public string  m_strDealNoteValue;
	}

	//ǩ����
	public class clsSignNameCol
	{
		//Сʱֵ
		public int m_intHourValue;

		//�޸�ʱ��
		public DateTime m_dtmModifyTime;

		//ǩ��
		public string  m_strSignNameID;
	}










	}
