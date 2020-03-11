using System;
using System.Collections ;
namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// clsBornScheduleRecordInfo 的摘要说明。
	/// </summary>
	public class clsBornScheduleRecordInfo
	{
		public clsBornScheduleRecordInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
		}
		public string m_strINPATIENTID      ; //病人ID
		public DateTime m_dtmINPATIENTDATE  ;//入院时间
		public DateTime m_dtmOPENDATE     ;  //打开日期
		public DateTime m_dtmCHILDBIRTHDATE ;//分娩日期
		public string	m_strCREATEID     ;  //创建用户ID
		public DateTime m_dtmMODIFYDATE   ;   //修改日期
		public string m_strFIRSTPOINT; // 第一点
		public string m_strSECONDPOINT; // 第二点
		public string m_strTHREEPOINT; // 第三点
		public string m_strFOUTPOINT; // 第四点
		public DateTime m_dtmFORECASTDATE   ; //预产期
		public string m_strPREGNANCYNUM     ; //孕产次
		public string m_strVENTERPOINTXML   ; //宫口点集合
		public string m_strCHECKVENTERTIMEXML    ;  //检查时间
		public string m_strBLOODPRESSUREXML ;  //血压
		public string m_strEMBRYOHEARTXML ; //胎心
		public string m_strVENTERSCALEXML ;  //宫缩
		public string m_strEXCEPTIONNOTEXML    ;  //异常情况
		public string m_strDEALNOTEXML    ;  //处理记录
		public string m_strSIGNXML        ;  //签名病
	}

	//一个产妇产程记录
	public class clsBornRecordManager //:IComparable 
	{
		//病人ID
		public string  m_strINPATIENTID;

		//入院时间
		public DateTime  m_dtmINPATIENTDATE;

		//打开时间
		public DateTime  m_dtmOPENDATE;

		//分娩日期
		public DateTime  m_dtmCHILDBIRTHDATE;

		//创建用户ID
		public string  m_dtmCREATEID;

		//修改日期
		public DateTime m_dtmMODIFYDATE   ;  

		//预产期
		public DateTime m_dtmFORECASTDATE   ;

		//孕产次
		public string m_strPREGNANCYNUM     ;

		//默认值的第一点
		public string m_strFIRSTPOINT;

		//默认值的第二点
		public string m_strSECONDPOINT ;

		//默认值的第三点
		public string m_strTHREEPOINT;

		//默认值的第四点
		public string m_strFOUTPOINT;

		//产妇一天产程记录集
		 public ArrayList  m_arlBornScheduleEveryDay; 

		//清空产程记录
		public void m_mthClear()
		{
			m_arlBornScheduleEveryDay.Clear();
		}

		//构造函数
		public clsBornRecordManager()
		{
			m_arlBornScheduleEveryDay=new ArrayList();
		}

		//添加新的日期

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

		//移去指定日期

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

		//移去指定日期的记录

		public  bool m_bnlRemove(clsBornScheduleEveryDay p_objRecord)
		{
			if(!m_arlBornScheduleEveryDay.Contains(p_objRecord))
				return false;

			m_arlBornScheduleEveryDay.Remove(p_objRecord);

			return true;
		}

		//判断是否含有指定日期记录

		public bool m_bnlContain(clsBornScheduleEveryDay p_objRecord)
		{
			return m_arlBornScheduleEveryDay.Contains(p_objRecord);
		}

		//获取指定下标的记录

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
		/// 获取指定日期的日期信息
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
		/// 获取指定日期的日期信息的下标
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
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
		/// 从后获取指定日期的日期信息的下标
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
		/// 日期的数量
		/// </summary>
		public int m_IntRecordCount
		{
			get
			{
				return m_arlBornScheduleEveryDay.Count;
			}
		}	

		/// <summary>
		/// 判断该日期信息是否最后一个日期信息
		/// </summary>
		/// <param name="p_objRecord">日期信息</param>
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


	//产妇每天数据集
	public class clsBornScheduleEveryDay
	{
		 //产妇每小时画点集
		public ArrayList m_arlBornScheduleEveryHourCol;

		//检查时间集
		public ArrayList m_arlCheckTimeCol;

		//血压集
		public ArrayList m_arlBloodPressureCol;

		//胎心集
		public ArrayList m_arlEmbryoHeartCol;

		//宫缩集
		public ArrayList m_arlVenterScaleExtendCol;

		//异常情况集
		public ArrayList m_arlExceptionNoteCol;

		//处理记录集
		public ArrayList m_arlDealNoteCol;

		//签名集
		public ArrayList m_arlSignNameCol;

		public DateTime m_dtmRecordDate;


		//构造函数
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

		//添加画点集
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
					m_arlBornScheduleEveryHourCol[i]=p_objRecordValue; //找到,更改值
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


		//删除画点集

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


		//添加检查时间集
		public void m_thAddCheckTime(int p_intHourValue,clsCheckTimeCol p_objRecordValue)
		{
			if(p_intHourValue<=0)
				return ;

			for(int i=0;i<m_arlCheckTimeCol.Count ;i++)
			{
				clsCheckTimeCol objRecord=(clsCheckTimeCol)m_arlCheckTimeCol[i];
			
				//添加时先判断是否为空,不为空才能替代
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


		//删除检查时间集

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

		//添加血压集
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


		//删除血压集

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


		//添加胎心集
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


		//删除胎心集

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


		//添加宫缩集
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


		//删除宫缩集

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

		//添加异常情况集
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


		//删除异常情况集

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


		//添加处理记录集
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


		//删除签名集

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


		//添加签名集
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


		//删除处理记录集

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



	//每小时画点值集
	public class clsBornScheduleEveryHourCol //: IComparable 
	{
	
		//小时值
		public int m_intHourValue;

		//宫口值
		public int m_intVenterValue;

		//是否有下一个值
		public bool m_bnlIsHavePreValue;

		//修改用户 
		public string m_strModifyUserID;

		//修改时间
		public DateTime m_dtmModifyTime;

		//是否删除
		public bool m_bnlIsDelete;


		
	}

	//检查时间集
	public class clsCheckTimeCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//检查时间
		public string m_strCheckTime;

	}

	//血压集
	public class clsBloodPressureCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//收缩压
		public string  m_strScaleBloodPressureValue;

		//舒张压
		public string  m_strExtendBloodPressureValue;
	}

	//胎心集
	public class clsEmbryoHeartCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//胎心
		public string  m_strEmbryoHeartValue;
	}

	//宫缩集
	public class clsVenterScaleExtendCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//收缩压
		public string  m_strScaleVenterValue;

		//舒张压
		public string  m_strExtendVenterValue;
	}

	//异常情况集
	public class clsExceptionNoteCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//异常情况
		public string  m_strExceptionNoteValue;
	}

	//处理记录集
	public class clsDealNoteCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//处理记录
		public string  m_strDealNoteValue;
	}

	//签名集
	public class clsSignNameCol
	{
		//小时值
		public int m_intHourValue;

		//修改时间
		public DateTime m_dtmModifyTime;

		//签名
		public string  m_strSignNameID;
	}










	}
