using System;
using System.Collections;
using System.Drawing;

namespace com.digitalwave.Utility.Controls.GN
{
	[Serializable]
	public class clsThreeMeasureDeleteInfo
	{
		public string m_strUserID;

		public string m_strUserName;

		public DateTime m_dtmDeleteTime;
	}

	#region ThreeMeasureDateRecord
	[Serializable]
	public class clsThreeMeasureDateRecord : IComparable
	{
		public DateTime m_dtmFirstPrintDate = DateTime.MinValue;

		public DateTime m_dtmRecordDate;

		internal clsThreeMeasureSpecialDate m_objSpecialDate;

		internal ArrayList m_arlEvent;

		internal ArrayList m_arlPulseValue;

		internal ArrayList m_arlTemperatureValue;

		internal ArrayList m_arlBreath;

		// 三角形
		internal ArrayList m_arlStayOut;

		internal ArrayList m_arlInputValue;

		internal ArrayList m_arlDejectaValue;

		internal ArrayList m_arlPeeValue;

		internal ArrayList m_arlOutStreamValue;

		internal ArrayList m_arlPressureValue1;
		internal ArrayList m_arlPressureValue2;

		internal ArrayList m_arlWeightValue;

		internal ArrayList m_arlSkinTestValue;

		internal ArrayList m_arlOtherValue;

		public clsThreeMeasureDateRecord(DateTime p_dtmRecordTime)
		{
			this.m_dtmRecordDate = p_dtmRecordTime;
			m_objSpecialDate = null;
			m_arlPulseValue = new ArrayList();
			m_arlTemperatureValue = new ArrayList();
			m_arlEvent = new ArrayList();
			m_arlBreath = new ArrayList();
			m_arlStayOut= new ArrayList();
			m_arlInputValue = new ArrayList();
			m_arlDejectaValue = new ArrayList();
			m_arlPeeValue = new ArrayList();
			m_arlOutStreamValue = new ArrayList();
			m_arlPressureValue1 = new ArrayList();
			m_arlPressureValue2 = new ArrayList();
			m_arlWeightValue = new ArrayList();
			m_arlSkinTestValue = new  ArrayList();
			m_arlOtherValue = new ArrayList();
		}

		public int CompareTo(object p_objOther)
		{
			clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)p_objOther;
			if(this.m_dtmRecordDate > objRecord.m_dtmRecordDate)
				return 1;
			else if(this.m_dtmRecordDate == objRecord.m_dtmRecordDate)
				return 0;
			else 
				return -1;
		}
	}
	
	/// <summary>
	/// 以天为单位的日期信息记录集
	/// </summary>
	[Serializable]
	public class clsThreeMeasureDateRecordManager
	{
		/// <summary>
		/// 存放每天记录信息
		/// </summary>
		private ArrayList m_arlRecord = new ArrayList();

		/// <summary>
		/// 清空记录信息
		/// </summary>
		public void m_mthClear()
		{
			m_arlRecord.Clear();
		}

		/// <summary>
		/// 添加新的日期
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
		/// <param name="p_blnIsAddNew">返回说明是否是新添的日期</param>
		/// <returns></returns>
		public clsThreeMeasureDateRecord m_objAddDateRecord(DateTime p_dtmRecordDate,out bool p_blnIsAddNew)
		{
			int intIndex = m_intLastIndexOf(p_dtmRecordDate.Date);

			if(intIndex >= 0)
			{
				p_blnIsAddNew = false;
				return (clsThreeMeasureDateRecord)m_arlRecord[intIndex];
			}

			p_blnIsAddNew = true;

			clsThreeMeasureDateRecord objRecord = new clsThreeMeasureDateRecord(p_dtmRecordDate);

			objRecord.m_dtmRecordDate = p_dtmRecordDate.Date;

			m_arlRecord.Add(objRecord);
			m_arlRecord.Sort();

			return objRecord;
		}

		/// <summary>
		/// 移去指定日期
		/// </summary>
		/// <param name="p_dtmRecordDate">指定日期</param>
		/// <returns></returns>
		public clsThreeMeasureDateRecord m_objRemove(DateTime p_dtmRecordDate)
		{
			clsThreeMeasureDateRecord objRemove = null;

			for(int i=0;i<m_arlRecord.Count;i++)
			{
				clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)m_arlRecord[i];

				if(objRecord.m_dtmRecordDate == p_dtmRecordDate.Date)
				{
					objRemove = objRecord;
					m_arlRecord.RemoveAt(i);
					break;
				}
			}			

			return objRemove;
		}

		/// <summary>
		/// 移去指定日期
		/// </summary>
		/// <param name="p_objRecord">日期信息</param>
		/// <returns></returns>
		public bool m_blnRemove(clsThreeMeasureDateRecord p_objRecord)
		{
			if(!m_arlRecord.Contains(p_objRecord))
				return false;

			m_arlRecord.Remove(p_objRecord);

			return true;
		}

		/// <summary>
		/// 判断是否包含指定日期
		/// </summary>
		/// <param name="p_objRecord">日期信息</param>
		/// <returns></returns>
		public bool m_blnContain(clsThreeMeasureDateRecord p_objRecord)
		{
			return m_arlRecord.Contains(p_objRecord);
		}

		/// <summary>
		/// 获取指定下标的日期信息
		/// </summary>
		public clsThreeMeasureDateRecord this[int p_intIndex]
		{
			get
			{
				if(p_intIndex >= m_arlRecord.Count)
					return null;

				return (clsThreeMeasureDateRecord)m_arlRecord[p_intIndex];
			}
		}
		
		/// <summary>
		/// 获取指定日期的日期信息
		/// </summary>
		public clsThreeMeasureDateRecord this[DateTime p_dtmRecordDate]
		{
			get
			{
				clsThreeMeasureDateRecord objResultRecord = null;

				for(int i=0;i<m_arlRecord.Count;i++)
				{
					clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)m_arlRecord[i];

					if(objRecord.m_dtmRecordDate == p_dtmRecordDate.Date)
					{
						objResultRecord = objRecord;
						break;
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

			for(int i=0;i<m_arlRecord.Count;i++)
			{
				clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)m_arlRecord[i];

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

			for(int i=m_arlRecord.Count-1;i>=0;i--)
			{
				clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)m_arlRecord[i];

				if(objRecord.m_dtmRecordDate == p_dtmRecordDate)
				{
					intIndex = i;
					break;
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
				return m_arlRecord.Count;
			}
		}		

		/// <summary>
		/// 判断该日期信息是否最后一个日期信息
		/// </summary>
		/// <param name="p_objRecord">日期信息</param>
		/// <returns></returns>
		public bool m_blnIsLast(clsThreeMeasureDateRecord p_objRecord)
		{
			if(m_arlRecord.Count <= 0)
				return false;

			clsThreeMeasureDateRecord objRecord = (clsThreeMeasureDateRecord)m_arlRecord[m_arlRecord.Count-1];

			return objRecord == p_objRecord;
		}
	}
	#endregion

	#region clsThreeMeasureSpecialDate
	[Serializable]
	public class clsThreeMeasureSpecialDate
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmSpecialDate;

		public bool m_blnIsNewStart = false;
	}
	#endregion

	[Serializable]
	public enum enmParamTime
	{
		am4 = 2,
		am4h = 4,
		am8 = 6,
		am8h = 8,
		am12 = 10,
		am12h = 12,
		pm4 = 14,
		pm4h = 16,
		pm8 = 18,
		pm8h = 20,
		pm12 = 22,
		pm12h = 24,
	}
	
	#region ThreeMeasurePulseValue
	[Serializable]
	public enum enmThreeMeasurePulseType
	{
		脉搏 = 0,
		心率
	}

	[Serializable]
	public class clsThreeMeasurePulseValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmValueTime;

		public int m_intValue;

		public enmThreeMeasurePulseType m_enmType;

		public bool m_blnLineToPreValue;

		public enmParamTime m_enmParamTime;

		internal Image m_imgSymbol;

		internal float m_fltXPos;
		internal float m_fltYPos;

		internal clsThreeMeasurePulseValue m_objNextValue = null;
		internal clsThreeMeasurePulseValue m_objPreValue = null;	
	
		internal int m_intCoverID = int.MinValue;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	
		internal clsThreeMeasureDateRecord m_objRecordDate = null;

	}

	/// <summary>
	/// 脉搏的数据集
	/// </summary>
	[Serializable]
	internal class clsThreeMeasurePulseValueManager
	{
		/*
		 * 脉搏数据的存放采用链表存放
		 */

		/// <summary>
		/// 脉搏数据链的头
		/// </summary>
		private clsThreeMeasurePulseValue m_objHeader;

		/// <summary>
		/// 脉搏数据链的头的获取
		/// </summary>
		public clsThreeMeasurePulseValue m_ObjHeader
		{
			get
			{
				return m_objHeader;
			}
		}

		/// <summary>
		/// 日期信息记录集
		/// </summary>
		private clsThreeMeasureDateRecordManager m_objRecordManager;

		/// <summary>
		/// 当前的脉搏信息
		/// </summary>
		private clsThreeMeasurePulseValue m_objCurrent;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="p_objRecordManager"></param>
		public clsThreeMeasurePulseValueManager(clsThreeMeasureDateRecordManager p_objRecordManager)
		{
			m_objHeader = new clsThreeMeasurePulseValue();

			m_objRecordManager = p_objRecordManager;

			m_objCurrent = null;
		}

		/// <summary>
		/// 清空脉搏信息
		/// </summary>
		public void m_mthClear()
		{
			m_objCurrent = null;

			m_objHeader.m_objNextValue = null;
		}

		

		/// <summary>
		/// 指向下一个脉搏信息
		/// </summary>
		/// <returns></returns>
		public bool m_blnNext()
		{
			if(m_objCurrent == null)
			{
				m_objCurrent = m_objHeader.m_objNextValue;
			}
			else
			{
				m_objCurrent = m_objCurrent.m_objNextValue;
			}
			
			return m_objCurrent != null;
		}

		/// <summary>
		/// 当前脉搏信息的获取
		/// </summary>
		public clsThreeMeasurePulseValue m_ObjCurrent
		{
			get
			{
				if(m_objCurrent.Equals(m_objHeader))
					return null;

				return m_objCurrent;
			}
		}

		/// <summary>
		/// 重置当前脉搏信息
		/// </summary>
		public void m_mthReset()
		{
			m_objCurrent = null;
		}

		/// <summary>
		/// 移去脉搏信息
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		/// <param name="p_objRecord">脉搏信息所在的日期信息</param>
		public void m_mthRemoveValue(clsThreeMeasurePulseValue p_objValue,clsThreeMeasureDateRecord p_objRecord)
		{			
			p_objRecord.m_arlPulseValue.Remove(p_objValue);
			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}
		}

		/// <summary>
		/// 移去脉搏信息
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		/// <returns></returns>
		public bool m_blnRemoveValue(clsThreeMeasurePulseValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlPulseValue.Remove(p_objValue);

			p_objValue.m_objRecordDate = null;

            p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}

			return true;
		}

		/// <summary>
		/// 添加脉搏信息
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		/// <returns></returns>
		public bool m_blnAddValue(clsThreeMeasurePulseValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlPulseValue.Add(p_objValue);

			m_mthAddValueToList(p_objValue);

			p_objValue.m_objRecordDate = objRecord;

			return true;
		}

		/// <summary>
		/// 添加脉搏信息到脉搏信息集的链表
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		private void m_mthAddValueToList(clsThreeMeasurePulseValue p_objValue)
		{
			clsThreeMeasurePulseValue objTempValue = m_objHeader;

			bool blnFound = false;

			//查找应该排在新数值后的第一数值
			while(objTempValue.m_objNextValue != null)
			{
				objTempValue = objTempValue.m_objNextValue;

				if(objTempValue.m_dtmValueTime >= p_objValue.m_dtmValueTime)
				{
					blnFound = true;
					break;
				}				
			}

			if(blnFound)
			{
                //新数值后还有数值，先设置新数值的指向，再改变新数值前后数值的指向
				p_objValue.m_objNextValue = objTempValue;
				p_objValue.m_objPreValue = objTempValue.m_objPreValue;

				p_objValue.m_objNextValue.m_objPreValue = p_objValue;
				p_objValue.m_objPreValue.m_objNextValue = p_objValue;
			}
			else
			{
				//新数值是最后的数值
				objTempValue.m_objNextValue = p_objValue;
				p_objValue.m_objPreValue = objTempValue;
			}
		}
	}


	#endregion

	#region ThreeMeasureTemperatureValue
	/*
	 * 基本思想与脉搏一致
	 */
	[Serializable]
	public enum enmThreeMeasureTemperatureType
	{
		口表温度 = 0,
		腋表温度,
		肛表温度
	}

	[Serializable]
	public class clsThreeMeasureTemperaturePhyscalDownValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmValueTime;

		public float m_fltValue;

		internal clsThreeMeasureTemperatureValue m_objBaseValue;

		internal float m_fltYPos;
		
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;	

	}

	[Serializable]
	public class clsThreeMeasureTemperatureValue 
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmValueTime;

		public float m_fltValue;

		public bool m_blnLineToPreValue;	

		public enmParamTime m_enmParamTime;	

		public enmThreeMeasureTemperatureType m_enmType;

		public ArrayList m_arlPhyscalDownValue = new ArrayList(3);

		internal Image m_imgSymbol;

		internal float m_fltXPos;
		internal float m_fltYPos;

		internal clsThreeMeasureTemperatureValue m_objNextValue = null;
		internal clsThreeMeasureTemperatureValue m_objPreValue = null;	

		internal int m_intCoverID = int.MinValue;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;	
	
		internal clsThreeMeasureDateRecord m_objRecordDate = null;

	}

	[Serializable]
	internal class clsThreeMeasureTemperatureValueManager
	{
		private clsThreeMeasureTemperatureValue m_objHeader;

		public clsThreeMeasureTemperatureValue m_ObjHeader
		{
			get
			{
				return m_objHeader;
			}
		}

		private clsThreeMeasureTemperatureValue m_objTail;

		private clsThreeMeasureDateRecordManager m_objRecordManager;

		private clsThreeMeasureTemperatureValue m_objCurrent;

		public clsThreeMeasureTemperatureValueManager(clsThreeMeasureDateRecordManager p_objRecordManager)
		{
			m_objHeader = new clsThreeMeasureTemperatureValue();

			m_objTail = null;

			m_objRecordManager = p_objRecordManager;

			m_objCurrent = null;
		}

		public void m_mthClear()
		{
			m_objCurrent = null;
			m_objTail = null;

			m_objHeader.m_objNextValue = null;
		}

		public bool m_blnNext()
		{
			if(m_objCurrent == null)
			{
				m_objCurrent = m_objHeader.m_objNextValue;
			}
			else
			{
				m_objCurrent = m_objCurrent.m_objNextValue;
			}
			
			return m_objCurrent != null;
		}

		public clsThreeMeasureTemperatureValue m_ObjCurrent
		{
			get
			{
				if(m_objCurrent.Equals(m_objHeader))
					return null;

				return m_objCurrent;
			}
		}

		public void m_mthReset()
		{
			m_objCurrent = null;
		}

		public bool m_blnRemovePhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase)
		{
			if(p_objValue == null || p_objBase == null)
				return false;

			clsThreeMeasureTemperatureValue objTemp = m_objHeader.m_objNextValue;
			bool blnFind = false;
			
			while(objTemp != null)
			{
				if(objTemp == p_objBase)
				{
					blnFind = true;
					break;
				}
				objTemp = objTemp.m_objNextValue;
			}

			if(!blnFind)
				return false;

			p_objBase.m_arlPhyscalDownValue.Remove(p_objValue);

			return true;
		}

		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,int p_intDownMinutes)
		{
			if(p_objValue == null || p_intDownMinutes < 0 )
				return false;

			clsThreeMeasureTemperatureValue objTemp = m_objHeader.m_objNextValue;
			bool blnFind = false;
			
			while(objTemp != null)
			{
				DateTime dtmDownTime = objTemp.m_dtmValueTime.AddMinutes(p_intDownMinutes);

				if(dtmDownTime >= p_objValue.m_dtmValueTime)
				{
					blnFind = true;
					break;
				}

				objTemp = objTemp.m_objNextValue;
			}

			if(!blnFind)
				return false;

			objTemp.m_arlPhyscalDownValue.Add(p_objValue);
			p_objValue.m_objBaseValue = objTemp;

			return true;
		}

		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase)
		{
			if(p_objValue == null || p_objBase == null)
				return false;

			clsThreeMeasureTemperatureValue objTemp = m_objHeader.m_objNextValue;
			bool blnFind = false;
			
			while(objTemp != null)
			{
				if(objTemp == p_objBase)
				{
					blnFind = true;
					break;
				}
				objTemp = objTemp.m_objNextValue;
			}

			if(!blnFind)
				return false;

			p_objBase.m_arlPhyscalDownValue.Add(p_objValue);
			p_objValue.m_objBaseValue = p_objBase;

			return true;
		}

		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,DateTime p_dtmBaseValueTime)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureTemperatureValue objTemp = m_objHeader.m_objNextValue;
			bool blnFind = false;
			
			while(objTemp != null)
			{
				if(objTemp.m_dtmValueTime == p_dtmBaseValueTime)
				{
					blnFind = true;
					break;
				}
				objTemp = objTemp.m_objNextValue;
			}

			if(!blnFind)
				return false;

			objTemp.m_arlPhyscalDownValue.Add(p_objValue);
			p_objValue.m_objBaseValue = objTemp;

			return true;
		}

		public bool m_blnAddPhyscalDownValueToLast(clsThreeMeasureTemperaturePhyscalDownValue p_objValue)
		{
			if(p_objValue == null || m_objTail == null)
				return false;

			m_objTail.m_arlPhyscalDownValue.Add(p_objValue);

			return true;
		}

		public void m_mthRemoveValue(clsThreeMeasureTemperatureValue p_objValue,clsThreeMeasureDateRecord p_objRecord)
		{			
			p_objRecord.m_arlTemperatureValue.Remove(p_objValue);
			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}
		}

		public bool m_blnRemoveValue(clsThreeMeasureTemperatureValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlTemperatureValue.Remove(p_objValue);
			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}

			return true;
		}

		public bool m_blnAddValue(clsThreeMeasureTemperatureValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlTemperatureValue.Add(p_objValue);

			m_mthAddValueToList(p_objValue);

			p_objValue.m_objRecordDate = objRecord;

			return true;
		}

		public void m_mthAddValueToList(clsThreeMeasureTemperatureValue p_objValue)
		{
			clsThreeMeasureTemperatureValue objTempValue = m_objHeader;

			bool blnFound = false;

			while(objTempValue.m_objNextValue != null)
			{
				objTempValue = objTempValue.m_objNextValue;

				if(objTempValue.m_dtmValueTime >= p_objValue.m_dtmValueTime)
				{
					blnFound = true;
					break;
				}				
			}

			if(blnFound)
			{
				p_objValue.m_objNextValue = objTempValue;
				p_objValue.m_objPreValue = objTempValue.m_objPreValue;

				p_objValue.m_objNextValue.m_objPreValue = p_objValue;
				p_objValue.m_objPreValue.m_objNextValue = p_objValue;
			}
			else
			{
				objTempValue.m_objNextValue = p_objValue;
				p_objValue.m_objPreValue = objTempValue;
				m_objTail = p_objValue;
			}
		}
	}
	#endregion

	#region ThreeMeasureEvent
	[Serializable]
	public enum enmThreeMeasureEventType
	{
		入院 = 0,
		出院,
		转入,
        转出,
		手术,
		分娩,
		请假,
		死亡,
		外出
	}
	[Serializable]
	public class clsThreeMeasureEvent : IComparable
	{
		public DateTime m_dtmModifyTime;

		public enmThreeMeasureEventType m_enmEventType;

		public DateTime m_dtmEventTime;
        /// <summary>
        /// 当前时间格索引（am4＝0；am8＝1；am12＝2；pm4＝3；pm8＝4；pm12＝5）
        /// </summary>
		internal int m_intTimeIndex;
        /// <summary>
        /// 如果有重叠的时间，这是实际显示时的时间格索引
        /// </summary>
		internal int m_intNearTimeIndex;

		internal string m_strTime;	

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;	

		internal void m_mthSetTimeString()
		{
			string strTime = null;

			#region Hour
			switch(m_dtmEventTime.Hour)
			{
				case 0:
					strTime = "零时";
					break;
				case 1:
					strTime = "一时";
					break;
				case 2:
					strTime = "二时";
					break;
				case 3:
					strTime = "三时";
					break;
				case 4:
					strTime = "四时";
					break;
				case 5:
					strTime = "五时";
					break;
				case 6:
					strTime = "六时";
					break;
				case 7:
					strTime = "七时";
					break;
				case 8:
					strTime = "八时";
					break;
				case 9:
					strTime = "九时";
					break;
				case 10:
					strTime = "十时";
					break;
				case 11:
					strTime = "十一时";
					break;
				case 12:
					strTime = "十二时";
					break;
				case 13:
					strTime = "十三时";
					break;
				case 14:
					strTime = "十四时";
					break;
				case 15:
					strTime = "十五时";
					break;
				case 16:
					strTime = "十六时";
					break;
				case 17:
					strTime = "十七时";
					break;
				case 18:
					strTime = "十八时";
					break;
				case 19:
					strTime = "十九时";
					break;
				case 20:
					strTime = "二十时";
					break;
				case 21:
					strTime = "二十一时";
					break;
				case 22:
					strTime = "二十二时";
					break;
				case 23:
					strTime = "二十三时";
					break;
			}
			#endregion

			#region Minute
			switch(m_dtmEventTime.Minute/10)
			{
				case 1:
					strTime += "十";
					break;
				case 2:
					strTime += "二十";
					break;
				case 3:
					strTime += "三十";
					break;
				case 4:
					strTime += "四十";
					break;
				case 5:
					strTime += "五十";
					break;
			}
			
			switch(m_dtmEventTime.Minute%10)
			{
				case 0:
					if(m_dtmEventTime.Minute != 0)
						strTime += "分";
					break;
				case 1:
					strTime += "一分";
					break;
				case 2:
					strTime += "二分";
					break;
				case 3:
					strTime += "三分";
					break;
				case 4:
					strTime += "四分";
					break;
				case 5:
					strTime += "五分";
					break;
				case 6:
					strTime += "六分";
					break;
				case 7:
					strTime += "七分";
					break;
				case 8:
					strTime += "八分";
					break;
				case 9:
					strTime += "九分";
					break;
			}
			#endregion

			m_strTime = strTime;
		}

		public int CompareTo(object obj)
		{
			clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)obj;

			if(this.m_dtmEventTime == objEvent.m_dtmEventTime)
				return 0;
			else if(this.m_dtmEventTime < objEvent.m_dtmEventTime)
				return -1;
			else return 1;
		}
	}		
	#endregion

	#region ThreeMeasureStayOutValue
	[Serializable]
	public enum enmThreeMeasureStayOutType
	{
		一般 = 0,
		
	}

	[Serializable]
	public class clsThreeMeasureStayOutValue : IComparable
	{	
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmValueTime;

		public bool m_blnValue;

		public enmThreeMeasureStayOutType m_enmStayOutType;

		public bool m_blnLineToPreValue;

		public enmParamTime m_enmParamTime;

		internal Image m_imgSymbol;

		internal float m_fltXPos;
		internal float m_fltYPos;

		internal clsThreeMeasureStayOutValue m_objNextValue = null;
		internal clsThreeMeasureStayOutValue m_objPreValue = null;	
	
		internal int m_intCoverID = int.MinValue;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	
		internal clsThreeMeasureDateRecord m_objRecordDate = null;
		
		public DateTime m_dtmStayOutTime;

		internal int m_intTimeIndex;

		internal int m_intAddSeq;

		public int CompareTo(object obj)
		{
			clsThreeMeasureStayOutValue objValue = (clsThreeMeasureStayOutValue)obj;
			return ((this.m_intTimeIndex*1000)+this.m_intAddSeq) - ((objValue.m_intTimeIndex*1000)+objValue.m_intAddSeq);
		}
	}

	/// <summary>
	/// 外出的数据集
	/// </summary>
	[Serializable]
	internal class clsThreeMeasureStayOutValueManager
	{
		/*
		 * 外出数据的存放采用链表存放
		 */

		/// <summary>
		/// 外出数据链的头
		/// </summary>
		private clsThreeMeasureStayOutValue m_objHeader;

		/// <summary>
		/// 外出数据链的头的获取
		/// </summary>
		public clsThreeMeasureStayOutValue m_ObjHeader
		{
			get
			{
				return m_objHeader;
			}
		}

		/// <summary>
		/// 日期信息记录集
		/// </summary>
		private clsThreeMeasureDateRecordManager m_objRecordManager;

		/// <summary>
		/// 当前的外出信息
		/// </summary>
		private clsThreeMeasureStayOutValue m_objCurrent;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="p_objRecordManager"></param>
		public clsThreeMeasureStayOutValueManager(clsThreeMeasureDateRecordManager p_objRecordManager)
		{
			m_objHeader = new clsThreeMeasureStayOutValue();

			m_objRecordManager = p_objRecordManager;

			m_objCurrent = null;
		}

		/// <summary>
		/// 清空外出信息
		/// </summary>
		public void m_mthClear()
		{
			m_objCurrent = null;

			m_objHeader.m_objNextValue = null;
		}

		

		/// <summary>
		/// 指向下一个脉搏信息
		/// </summary>
		/// <returns></returns>
		public bool m_blnNext()
		{
			if(m_objCurrent == null)
			{
				m_objCurrent = m_objHeader.m_objNextValue;
			}
			else
			{
				m_objCurrent = m_objCurrent.m_objNextValue;
			}
			
			return m_objCurrent != null;
		}

		/// <summary>
		/// 当前外出信息的获取
		/// </summary>
		public clsThreeMeasureStayOutValue m_ObjCurrent
		{
			get
			{
				if(m_objCurrent.Equals(m_objHeader))
					return null;

				return m_objCurrent;
			}
		}

		/// <summary>
		/// 重置当前外出信息
		/// </summary>
		public void m_mthReset()
		{
			m_objCurrent = null;
		}

		/// <summary>
		/// 移去外出信息
		/// </summary>
		/// <param name="p_objValue">外出信息</param>
		/// <param name="p_objRecord">外出信息所在的日期信息</param>
		public void m_mthRemoveValue(clsThreeMeasureStayOutValue p_objValue,clsThreeMeasureDateRecord p_objRecord)
		{			
			p_objRecord.m_arlStayOut.Remove(p_objValue);
			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}
		}

		/// <summary>
		/// 移去外出信息
		/// </summary>
		/// <param name="p_objValue">外出信息</param>
		/// <returns></returns>
		public bool m_blnRemoveValue(clsThreeMeasureStayOutValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlStayOut.Remove(p_objValue);

			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}

			return true;
		}

		/// <summary>
		/// 添加外出信息
		/// </summary>
		/// <param name="p_objValue">外出信息</param>
		/// <returns></returns>
		public bool m_blnAddValue(clsThreeMeasureStayOutValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmValueTime];

			if(objRecord == null)
				return false;

			objRecord.m_arlStayOut.Add(p_objValue);

			m_mthAddValueToList(p_objValue);

			p_objValue.m_objRecordDate = objRecord;

			return true;
		}

		/// <summary>
		/// 添加外出信息到外出信息集的链表
		/// </summary>
		/// <param name="p_objValue">外出信息</param>
		private void m_mthAddValueToList(clsThreeMeasureStayOutValue p_objValue)
		{
			clsThreeMeasureStayOutValue objTempValue = m_objHeader;

			bool blnFound = false;

			//查找应该排在新数值后的第一数值
			while(objTempValue.m_objNextValue != null)
			{
				objTempValue = objTempValue.m_objNextValue;

				if(objTempValue.m_dtmValueTime >= p_objValue.m_dtmValueTime)
				{
					blnFound = true;
					break;
				}				
			}

			if(blnFound)
			{
				//新数值后还有数值，先设置新数值的指向，再改变新数值前后数值的指向
				p_objValue.m_objNextValue = objTempValue;
				p_objValue.m_objPreValue = objTempValue.m_objPreValue;

				p_objValue.m_objNextValue.m_objPreValue = p_objValue;
				p_objValue.m_objPreValue.m_objNextValue = p_objValue;
			}
			else
			{
				//新数值是最后的数值
				objTempValue.m_objNextValue = p_objValue;
				p_objValue.m_objPreValue = objTempValue;
			}
		}
	}
	#endregion

	#region ThreeMeasureBreathValue
	[Serializable]
	public enum enmThreeMeasureBreathType
	{
		一般 = 0,
		辅助呼吸,
		停辅助呼吸,
		/// <summary>
		/// 小于10的值
		/// </summary>
		特殊值
	}

	[Serializable]
	public class clsThreeMeasureBreathValue : IComparable
	{	
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmValueTime;

		public int m_intValue;

		public enmThreeMeasureBreathType m_enmBreathType;

		public bool m_blnLineToPreValue;

		public enmParamTime m_enmParamTime;

		internal Image m_imgSymbol;

		internal float m_fltXPos;
		internal float m_fltYPos;

		internal clsThreeMeasureBreathValue m_objNextValue = null;
		internal clsThreeMeasureBreathValue m_objPreValue = null;	
	
		internal int m_intCoverID = int.MinValue;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	
		internal clsThreeMeasureDateRecord m_objRecordDate = null;
		
		public DateTime m_dtmBreathTime;

		internal int m_intTimeIndex;

		internal int m_intAddSeq;

		public int CompareTo(object obj)
		{
			clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)obj;
			return ((this.m_intTimeIndex*1000)+this.m_intAddSeq) - ((objValue.m_intTimeIndex*1000)+objValue.m_intAddSeq);
		}
	}

	/// <summary>
	/// 呼吸的数据集
	/// </summary>
	[Serializable]
	internal class clsThreeMeasureBreathValueManager
	{
		/*
		 * 呼吸数据的存放采用链表存放
		 */

		/// <summary>
		/// 呼吸数据链的头
		/// </summary>
		private clsThreeMeasureBreathValue m_objHeader;

		/// <summary>
		/// 呼吸数据链的头的获取
		/// </summary>
		public clsThreeMeasureBreathValue m_ObjHeader
		{
			get
			{
				return m_objHeader;
			}
		}

		/// <summary>
		/// 日期信息记录集
		/// </summary>
		private clsThreeMeasureDateRecordManager m_objRecordManager;

		/// <summary>
		/// 当前的呼吸信息
		/// </summary>
		private clsThreeMeasureBreathValue m_objCurrent;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="p_objRecordManager"></param>
		public clsThreeMeasureBreathValueManager(clsThreeMeasureDateRecordManager p_objRecordManager)
		{
			m_objHeader = new clsThreeMeasureBreathValue();

			m_objRecordManager = p_objRecordManager;

			m_objCurrent = null;
		}

		/// <summary>
		/// 清空呼吸信息
		/// </summary>
		public void m_mthClear()
		{
			m_objCurrent = null;

			m_objHeader.m_objNextValue = null;
		}

		

		/// <summary>
		/// 指向下一个脉搏信息
		/// </summary>
		/// <returns></returns>
		public bool m_blnNext()
		{
			if(m_objCurrent == null)
			{
				m_objCurrent = m_objHeader.m_objNextValue;
			}
			else
			{
				m_objCurrent = m_objCurrent.m_objNextValue;
			}
			
			return m_objCurrent != null;
		}

		/// <summary>
		/// 当前呼吸信息的获取
		/// </summary>
		public clsThreeMeasureBreathValue m_ObjCurrent
		{
			get
			{
				if(m_objCurrent.Equals(m_objHeader))
					return null;

				return m_objCurrent;
			}
		}

		/// <summary>
		/// 重置当前呼吸信息
		/// </summary>
		public void m_mthReset()
		{
			m_objCurrent = null;
		}

		/// <summary>
		/// 移去呼吸信息
		/// </summary>
		/// <param name="p_objValue">呼吸信息</param>
		/// <param name="p_objRecord">呼吸信息所在的日期信息</param>
		public void m_mthRemoveValue(clsThreeMeasureBreathValue p_objValue,clsThreeMeasureDateRecord p_objRecord)
		{			
			p_objRecord.m_arlBreath.Remove(p_objValue);
			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}
		}

		/// <summary>
		/// 移去呼吸信息
		/// </summary>
		/// <param name="p_objValue">呼吸信息</param>
		/// <returns></returns>
		public bool m_blnRemoveValue(clsThreeMeasureBreathValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmBreathTime];//修改后值消失m_dtmValueTime

			if(objRecord == null)
				return false;

			objRecord.m_arlBreath.Remove(p_objValue);

			p_objValue.m_objRecordDate = null;

			p_objValue.m_objPreValue.m_objNextValue = p_objValue.m_objNextValue;
			if(p_objValue.m_objNextValue != null)
			{
				p_objValue.m_objNextValue.m_objPreValue = p_objValue.m_objPreValue;
			}

			return true;
		}

		/// <summary>
		/// 添加呼吸信息
		/// </summary>
		/// <param name="p_objValue">呼吸信息</param>
		/// <returns></returns>
		public bool m_blnAddValue(clsThreeMeasureBreathValue p_objValue)
		{
			if(p_objValue == null)
				return false;		

			clsThreeMeasureDateRecord objRecord = m_objRecordManager[p_objValue.m_dtmBreathTime];//修改后值消失m_dtmValueTime

			if(objRecord == null)
				return false;

			objRecord.m_arlBreath.Add(p_objValue);

			m_mthAddValueToList(p_objValue);

			p_objValue.m_objRecordDate = objRecord;

			return true;
		}

		/// <summary>
		/// 添加呼吸信息到呼吸信息集的链表
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		private void m_mthAddValueToList(clsThreeMeasureBreathValue p_objValue)
		{
			clsThreeMeasureBreathValue objTempValue = m_objHeader;

			bool blnFound = false;

			//查找应该排在新数值后的第一数值
			while(objTempValue.m_objNextValue != null)
			{
				objTempValue = objTempValue.m_objNextValue;

				if(objTempValue.m_dtmBreathTime >= p_objValue.m_dtmBreathTime)//if(objTempValue.m_dtmValueTime >= p_objValue.m_dtmBreathTime)//.m_dtmValueTime)//修改时值消失
				{
					blnFound = true;
					break;
				}				
			}

			if(blnFound)
			{
				//新数值后还有数值，先设置新数值的指向，再改变新数值前后数值的指向
				p_objValue.m_objNextValue = objTempValue;
				p_objValue.m_objPreValue = objTempValue.m_objPreValue;

				p_objValue.m_objNextValue.m_objPreValue = p_objValue;
				p_objValue.m_objPreValue.m_objNextValue = p_objValue;
			}
			else
			{
				//新数值是最后的数值
				objTempValue.m_objNextValue = p_objValue;
				p_objValue.m_objPreValue = objTempValue;
			}
		}
	}
	#endregion

	#region clsThreeMeasureInputValue
	[Serializable]
	public class clsThreeMeasureInputValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmInputDate;

		public float m_fltValue;
		
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasureDejectaValue
	[Serializable]
	public class clsThreeMeasureDejectaValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmDejectaDate;

		public bool m_blnNeedWeight = false;

		public int m_intBeforeTimes = 0;

		public bool m_blnAfterMoreTimes = false;
		public int m_intAfterTimes = 0;

		public int m_intClysisTimes = 0;

		public float m_fltWeight;	

		public bool m_blnCanDejecta = true;

		internal string m_strDesc;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasurePeeValue
	/// <summary>
	/// 尿量
	/// </summary>
	[Serializable]
	public class clsThreeMeasurePeeValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmPeeDate;

		public float m_fltValue;
	
		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasureOutStreamValue
	
	[Serializable]
	public enum enmIrretention
	{
		一般,
		失禁,
		导尿,
		留置导尿
	}
	/// <summary>
	/// 小便次数
	/// </summary>
	[Serializable]
	public class clsThreeMeasureOutStreamValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmOutStreamDate;

		public enmIrretention m_enmIsIrretention;

		public float m_fltValue;

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasurePressureValue
	[Serializable]
	public class clsThreeMeasurePressureValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmPressureDate;

		public float m_fltDiastolicValue;
		public float m_fltSystolicValue;

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region ThreeMeasureWeight
	[Serializable]
	public enum enmThreeMeasureWeightType
	{
		一般 = 0,
		平车,
		卧床,
        轮椅
	}

	[Serializable]
	public class clsThreeMeasureWeightValue
	{
		public DateTime m_dtmModifyTime;

		public enmThreeMeasureWeightType m_enmWeightType;

		public DateTime m_dtmWeightDate;

		public float m_fltValue;

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasureSkinTestValue
	[Serializable]
	public class clsThreeMeasureSkinTestValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmSkinTestDate;

		public string m_strMedicineName;		

		/// <summary>
		/// 皮试结果（－1＝无结果；0＝正常；1＝结构阳性(+～+++)；2＝其它）
		/// </summary>
		public int m_intBadStatus;
        /// <summary>
        /// 结果数目(+～+++)（1～3）
        /// </summary>
		public int m_intBadCount;

        private string m_strOtherResult;
        /// <summary>
        /// 皮试其它结果的内容
        /// </summary>
        public string m_StrOtherResult
        {
            get { return m_strOtherResult; }
            set { m_strOtherResult = value; }
        }

		internal string m_strPDDValue;

		internal int m_intTimeIndex;

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion

	#region clsThreeMeasureOtherValue
	[Serializable]
	public class clsThreeMeasureOtherValue
	{
		public DateTime m_dtmModifyTime;

		public DateTime m_dtmOtherDate;

		public string m_strOtherItem;

		string m_strOtherValue;
        public string m_StrOtherValue
        {
            get { return m_strOtherValue; }
            set{ m_strOtherValue = value;}
        }

		public string m_strOtherUnit;

		internal string m_strOther;

		public clsThreeMeasureDeleteInfo m_objDeleteInfo = null;
	}
	#endregion
}

