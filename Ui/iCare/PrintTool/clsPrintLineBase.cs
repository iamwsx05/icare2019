using System;
using System.Drawing;
using iCare;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_intEndY"></param>
    /// <param name="p_objGrp"></param>
    /// <param name="p_fntNormalText"></param>
	public delegate void d_mthPrintPartEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText);
	/// <summary>
    /// �д�ӡ�࣬(ֻ�ܵ��д�ӡ)
	/// </summary>
	public abstract class clsPrintLineBase
	{
		/// <summary>
		/// ��ӡ��һ������
		/// </summary>
		/// <param name="p_intPosY"></param>
		/// <param name="p_objGrp"></param>
		/// <param name="p_fntNormalText"></param>
		public abstract void m_mthPrintNextLine(ref int p_intPosY,Graphics p_objGrp,Font p_fntNormalText);
        /// <summary>
        /// 
        /// </summary>
		protected d_mthPrintPartEnd m_objHandlePartEnd;
        /// <summary>
        /// 
        /// </summary>
		public d_mthPrintPartEnd m_ObjHandlePartEnd
		{
			set
			{
				m_objHandlePartEnd = value;
			}
		}
		/// <summary>
		/// ���øôδ�ӡ��ʹ�õĲ���ֵ
		/// </summary>
		public abstract void m_mthReset();		
        /// <summary>
        /// ��ȡ�Ƿ�����һ�У�true Ϊ���У�false Ϊû��
        /// </summary>
		public virtual bool m_BlnHaveMoreLine
		{
			get
			{
				return m_blnHaveMoreLine;
			}
		}
        /// <summary>
        /// ��ʶ�Ƿ�����һ�У�true Ϊ���У�false Ϊû��
        /// </summary>
		protected bool m_blnHaveMoreLine = true;
        /// <summary>
        /// ��ӡ���ݶ���
        /// </summary>
		protected object m_objPrintLineInfo = null;
        /// <summary>
        /// ��ȡ�������ô�ӡ���ݶ���
        /// </summary>
		public virtual object m_ObjPrintLineInfo
		{
			get
			{
				return m_objPrintLineInfo;
			}
			set
			{
				m_objPrintLineInfo = value;
			}
		}
        /// <summary>
        /// ��ӡ��ǩ����Ϣ����
        /// </summary>
		protected clsPrintRecordSign m_objPrintSign = null;
        /// <summary>
        /// ��ȡ�������ô���ӡ��ǩ����Ϣ����
        /// </summary>
		public clsPrintRecordSign m_ObjPrintSign
		{
			get
			{
				return m_objPrintSign;
			}
			set
			{
				m_objPrintSign = value;
			}
		}
		/// <summary>
		/// ���ǩ��
		/// </summary>
		/// <param name="p_strPartDesc">ǩ��������</param>
		/// <param name="p_objRichTextBoxArr">ǩ������Ϣ</param>
		protected void m_mthAddSign(string p_strPartDesc,ctlRichTextBox.clsModifyUserInfo [] p_objModifyUserArr)
		{
			if(m_objPrintSign != null)
			{
				m_objPrintSign.m_mthAddSignInfo(p_strPartDesc,p_objModifyUserArr);
			}
		}
		/// <summary>
		/// ���ǩ��
		/// </summary>
		/// <param name="p_strPartDesc">ǩ��������</param>
		/// <param name="p_objModifyUserArr">ǩ������Ϣ</param>
		protected void m_mthAddSign2(string p_strPartDesc,com.digitalwave.controls.ctlRichTextBox.clsModifyUserInfo [] p_objModifyUserArr)
		{
			if(m_objPrintSign != null)
			{
				m_objPrintSign.m_mthAddSignInfo2(p_strPartDesc,p_objModifyUserArr);
			}
		}
        /// <summary>
        /// ��һ�δ�ӡʱ��
        /// </summary>
		protected DateTime m_dtmFirstPrintTime;
        /// <summary>
        /// ��ȡ�����õ�һ�δ�ӡʱ��
        /// </summary>
		public virtual object m_DtmFirstPrintTime
		{
			get
			{
				return m_dtmFirstPrintTime;
			}
			set
			{
				m_dtmFirstPrintTime =(DateTime)value;
			}
		}
	}
	/// <summary>
	/// ��ӡ������������
	/// </summary>
	public class clsPrintContext
	{
        /// <summary>
        /// ��ӡ��Ϣ����
        /// </summary>
		private object m_objPrintLineInfo = null;
		/// <summary>
		/// ��ȡ�������ô�ӡ��Ϣ����
		/// </summary>
		public object m_ObjPrintLineInfo
		{
			get
			{
				return m_objPrintLineInfo;
			}
			set
			{
				m_objPrintLineInfo = value;

				if(m_objPrintLineArr != null)
				{
					for(int i=0;i<m_objPrintLineArr.Length;i++)
					{
						m_objPrintLineArr[i].m_ObjPrintLineInfo = value;
					}
				}
			}
		}
        /// <summary>
        /// ��ӡ��ǩ�����ݶ���
        /// </summary>
		private clsPrintRecordSign m_objPrintSign = null;
        /// <summary>
        /// ���ô�ӡ��ǩ�����ݶ���
        /// </summary>
		public clsPrintRecordSign m_ObjPrintSign
		{
			set
			{
				m_objPrintSign = value;

				if(m_objPrintLineArr != null)
				{
					for(int i=0;i<m_objPrintLineArr.Length;i++)
					{
						m_objPrintLineArr[i].m_ObjPrintSign = value;
					}
				}
			}
		}
		/// <summary>
		/// ���õ�һ�δ�ӡ��ʱ��
		/// </summary>
		public DateTime m_DtmFirstPrintTime
		{
			set
			{
				for(int i=0;i<m_objPrintLineArr.Length;i++)
				{
					m_objPrintLineArr[i].m_DtmFirstPrintTime = value;
				}
				
			}
		}
        /// <summary>
        /// ��ӡ�����ݶ���
        /// </summary>
		private clsPrintLineBase [] m_objPrintLineArr;
        /// <summary>
        /// ��ȡ�����ô�ӡ�����ݶ���
        /// </summary>
		public clsPrintLineBase [] m_ObjPrintLineArr
		{
			get
			{
				return m_objPrintLineArr;
			}
			set
			{
				m_objPrintLineArr = value;

				if(m_objPrintLineArr != null && 
					m_objPrintLineArr.Length > 0 && 
					m_objPrintLineArr[0].m_BlnHaveMoreLine)
				{
					m_intCurrentPrintLineIndex = 0;

					m_blnHaveMoreLine = true;
				}
				else
				{
					m_intCurrentPrintLineIndex = -1;

					m_blnHaveMoreLine = false;
				}
			}
		}
        /// <summary>
        /// ��ǰ��ӡ�е��к�
        /// </summary>
		private int m_intCurrentPrintLineIndex;
        /// <summary>
        /// ���캯��1
        /// </summary>
		public clsPrintContext()
		{
			m_objPrintLineArr = null;

			m_intCurrentPrintLineIndex = -1;
		}
        /// <summary>
        /// ���캯��2 (��ʼ����ӡ����)
        /// </summary>
        /// <param name="p_objPrintLineArr">��ӡ���ݶ���</param>
		public clsPrintContext(clsPrintLineBase [] p_objPrintLineArr)
		{
			m_objPrintLineArr = p_objPrintLineArr;

			if(m_objPrintLineArr != null && 
				m_objPrintLineArr.Length > 0 && 
				m_objPrintLineArr[0].m_BlnHaveMoreLine)
			{
				m_intCurrentPrintLineIndex = 0;

				m_blnHaveMoreLine = true;
			}
			else
			{
				m_intCurrentPrintLineIndex = -1;

				m_blnHaveMoreLine = false;
			}
		}
        /// <summary>
        /// ��ӡһ��ǩ��
        /// </summary>
        /// <param name="p_intPosX">���еĺ�����</param>
        /// <param name="p_intPosY">���е�������</param>
        /// <param name="p_objGrp">��ӡ��ͼ����</param>
        /// <param name="p_fntNormalText">��ӡ������</param>
		public void m_mthPrintNextSign(int p_intPosX,int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
		{
			if(m_objPrintSign != null)
			{
				try
				{
					m_objPrintSign.m_mthPrintNextLine(p_intPosX,p_intPosY,p_objGrp,p_fntNormalText);
				}
				catch
				{
				}
			}
		}
        /// <summary>
        /// ��ȡ�Ƿ�����Ҫ��ӡ��ǩ���У�true Ϊ���У�false Ϊû��
        /// </summary>
		public bool m_BlnHaveMoreSign
		{
			get
			{
				if(m_objPrintSign != null && m_objPrintSign.m_BlnHaveMoreLine)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
        /// <summary>
        /// �Ƿ�����Ҫ��ӡ���У�true Ϊ���У�false Ϊû��
        /// </summary>
		private bool m_blnHaveMoreLine = false;
        /// <summary>
        /// ��ȡ�Ƿ�����Ҫ��ӡ���У�true Ϊ���У�false Ϊû��
        /// </summary>
		public bool m_BlnHaveMoreLine
		{
			get
			{
				return m_blnHaveMoreLine;
			}
		}
        /// <summary>
        /// ��ӡһ������
        /// </summary>
        /// <param name="p_intPosY">���е�������</param>
        /// <param name="p_objGrp">��ӡ��ͼ����</param>
        /// <param name="p_fntNormalText">��ӡ������</param>
		public void m_mthPrintNextLine(ref int p_intPosY,Graphics p_objGrp,Font p_fntNormalText)
		{
			if(!m_blnHaveMoreLine)
				return;

			try
			{
				m_objPrintLineArr[m_intCurrentPrintLineIndex].m_mthPrintNextLine(ref p_intPosY,p_objGrp,p_fntNormalText);//

				if(m_objPrintLineArr[m_intCurrentPrintLineIndex].m_BlnHaveMoreLine)
				{
					return;
				}
				else
				{
					m_intCurrentPrintLineIndex++;

					if(m_intCurrentPrintLineIndex >= m_objPrintLineArr.Length
						|| !m_objPrintLineArr[m_intCurrentPrintLineIndex].m_BlnHaveMoreLine)
					{
						m_intCurrentPrintLineIndex = -1;

						m_blnHaveMoreLine = false;
					}
				}
			}
			catch(Exception  ex)
			{				
				clsPublicFunction.ShowInformationMessageBox(ex.TargetSite.ReflectedType.ToString() +" : "+ ex.TargetSite.Name+" : " +ex.Message+":" + ex.StackTrace);
			}
		}
        /// <summary>
        /// ����������ôδ�ӡ����ұ������õĲ���ֵ
        /// </summary>
		public void m_mthReset()
		{
			if(m_objPrintSign != null)
				m_objPrintSign.m_mthReset();

			if(m_objPrintLineArr == null)
				return;

			for(int i=0;i<m_objPrintLineArr.Length;i++)
			{
				m_objPrintLineArr[i].m_mthReset();
			}

			if(m_objPrintLineArr.Length > 0 && 
				m_objPrintLineArr[0].m_BlnHaveMoreLine)
			{
				m_intCurrentPrintLineIndex = 0;

				m_blnHaveMoreLine = true;
			}
			else
			{
				m_intCurrentPrintLineIndex = -1;

				m_blnHaveMoreLine = false;
			}
		}
	}
}