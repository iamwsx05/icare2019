#define NewXml

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;

using com.digitalwave.Utility;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ˫�����¼��Ĳ���
	/// </summary>
	public class clsDoubleStrikeThoughEventArg : EventArgs
	{
		/// <summary>
		/// ˫���ߵ��ı�
		/// </summary>
		public string m_strDeleteText;

		/// <summary>
		/// ˫���ߵ���ɫ
		/// </summary>
		public Color m_clrDST;	
		
		/// <summary>
		/// ���˫���ߵ��û�ID
		/// </summary>
		public string m_strUserID;

		/// <summary>
		/// ���˫���ߵ��û�����
		/// </summary>
		public string m_strUserName;

		/// <summary>
		/// ���˫���ߵ�ʱ��
		/// </summary>
		public DateTime m_dtmDeleteTime;
	}		

	/// <summary>
	/// �����ı��¼��Ĳ���
	/// </summary>
	public class clsInsertEventArg : EventArgs
	{
		/// <summary>
		/// �����ı�������
		/// </summary>
		public string m_strInsertText;

		/// <summary>
		/// �����ı�����ɫ
		/// </summary>
		public Color m_clrInsert;	
		
		/// <summary>
		/// �����ı����û�ID
		/// </summary>
		public string m_strUserID;

		/// <summary>
		/// �����ı����û�����
		/// </summary>
		public string m_strUserName;

		/// <summary>
		/// �����ı��û���������
		/// </summary>
		public int m_intUserSeq;

		/// <summary>
		/// �����ı���ʱ��
		/// </summary>
		public DateTime m_dtmInsertTime;
	}		

	/// <summary>
	/// ҽԺ��д�淶�ؼ�
	/// </summary>
	public class ctlRichTextBox : RichTextBox
	{
		/// <summary>
		/// ��ǰ�ؼ�������״̬����Undo��Redoʱ��
		/// </summary>
		private class clsRichTextItemStatus
		{
			/// <summary>
			/// ��ǰ��RTF����
			/// </summary>
			public string m_strRTF;
			/// <summary>
			/// ��ǰ��������
			/// </summary>
			public int m_intCurrentCursorIndex;
			/// <summary>
			/// ��ǰ˫���ߵ���Ϣ
			/// </summary>
			public ArrayList m_arlDoubleStrikeThrough = new ArrayList();
			/// <summary>
			/// ��ǰ�ı����ݵ���Ϣ
			/// </summary>
			public ArrayList m_arlTextContentInfos = new ArrayList();
		}		

		#region ��Ч�Ĵ��룡��Ϊ����ɳ�����ݣ��ṩ������ʵ�ֵĽӿ�
		public bool m_BlnPartControl
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public int m_IntPartControlStartIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int m_IntPartControlLength
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
		#endregion

		public new string Text
		{
			get
			{
				return base.Text.TrimEnd();
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>
		/// �ṩ��Чƴ��String����ʱ����
		/// </summary>
		private static StringBuilder s_sbdTemp = new StringBuilder();

		/// <summary>
		/// ���ı������滻ʱʹ�õĹ���
		/// </summary>
		private static RichTextBox s_rtbRTFTrans = new RichTextBox();

		/// <summary>
		/// ��ȡXML�Ĺ���
		/// </summary>
		private static XmlParserContext s_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		/// <summary>
		/// ˫������Ϣ������
		/// </summary>
		private static ArrayList s_arlDSTIndex = new ArrayList();

		/// <summary>
		/// ������Ϣ������
		/// </summary>
		private static ArrayList s_arlContentInfo = new ArrayList();

		/// <summary>
		/// �û���Ϣ������
		/// </summary>
		private static ArrayList s_arlUserInfo = new ArrayList();
		/// <summary>
		/// ��ʱ�û���Ϣ
		/// </summary>
		private static ArrayList s_arlTempUserInfo = new ArrayList();

		/// <summary>
		/// �ڻ�ȡ��ȷ�ı�����ʱ��ȥ����˫������Ϣ������
		/// </summary>
		private static ArrayList s_arlDeleteDSTInfo = new ArrayList();
		/// <summary>
		/// ���ݡ�ȥ����˫������Ϣ�����顱�����ݣ���ʱ������Ҫ������ı�������Ϣ
		/// </summary>
		private static ArrayList s_arlContentTempInfo = new ArrayList();

		/// <summary>
		/// ���±���Ϣ������
		/// </summary>
		private static ArrayList s_arlSuperSubScriptInfo = new ArrayList();

//		public static Color m_ClrDefaultText
//		{
//			get
//			{
//				return m_clrDefaultText;
//			}
//			set
//			{
//				m_clrDefaultText = value;
//			}
//		}

		private static Color m_clrDefaultViewText = Color.Black;

		public static Color m_ClrDefaultViewText
		{
			get
			{
				return m_clrDefaultViewText;
			}
			set
			{
				m_clrDefaultViewText = value;				
			}
		}

		/// <summary>
		/// �����˫ɾ���ߵ�����
		/// </summary>
		private ArrayList m_arlDoubleStrikeThrough;

//		/// <summary>
//		/// ����ӵ��ı���Ϣ
//		/// </summary>
//		private ArrayList m_arlInsertInfo;

		/// <summary>
		/// ˫ɾ���ߵ���ʾ����
		/// </summary>
		private ArrayList m_arlDSTView;

		/// <summary>
		/// ����ӵ��ı���ʾ��Ϣ����������Ƿ�������ӵ��ı���ʱʹ�á�
		/// </summary>
		private ArrayList m_arlInsertView;

		/// <summary>
		/// ��ǰ�û���˫ɾ���ߵ���ɫ
		/// </summary>
		private Color m_clrDST;		

		/// <summary>
		/// ��ǰ�û���˫ɾ������ɫ�����úͻ�ȡ
		/// </summary>
		public Color m_ClrDST
		{
			get
			{
				return m_clrDST;
			}
			set
			{
				m_clrDST = value;
			}
		}

		/// <summary>
		/// ��ǰ�û��ھɵĲ����������ֵ���ɫ
		/// </summary>
		private Color m_clrOldPartInsertText;		

		/// <summary>
		/// ��ǰ�û��ھɵĲ�������������ɫ�����úͻ�ȡ
		/// </summary>
		public Color m_ClrOldPartInsertText
		{
			get
			{
				return m_clrOldPartInsertText;
			}
			set
			{
				if(m_arlModifyUsers.Count == 0)
				{
					//ʹ��Ĭ����ɫ
					m_clrOldPartInsertText = m_clrDefaultViewText;
				}
				else
				{
					m_clrOldPartInsertText = value;
					m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;				
				}
			}
		}

		/// <summary>
		/// ��ǰ�û�ID
		/// </summary>
		private string m_strUserID;

		/// <summary>
		/// ��ǰ�û�ID�����úͻ�ȡ
		/// </summary>
		public string m_StrUserID
		{
			get
			{
				return m_strUserID;
			}
			set
			{
				if(value != null)
				{
					m_strUserID = value;
					m_objCurrentModifyUser.m_strUserID = m_strUserID;
				}
			}
		}

		/// <summary>
		/// ��ǰ�û�ID
		/// </summary>
		private string m_strUserName;

		/// <summary>
		/// ��ǰ�û�ID�����úͻ�ȡ
		/// </summary>
		public string m_StrUserName
		{
			get
			{
				return m_strUserName;
			}
			set
			{
				if(value != null)
				{
					m_strUserName = value;
					m_objCurrentModifyUser.m_strUserName = m_strUserName;
				}
			}
		}

		/// <summary>
		/// �û�����ʱ�Ƿ��Ѿ�ѡ�����ı�
		/// </summary>
		private bool m_blnIsSelectedChanged;

		/// <summary>
		/// �Ƿ���ѡ��ı���¼�
		/// </summary>
		private bool m_blnCanSelectedChanged;

		/// <summary>
		/// �û��ı��ı�����֮ǰ�ĳ���
		/// </summary>
		private int m_intPreviouslyLen;

		/// <summary>
		/// ��ǰ�������
		/// </summary>
		private int m_intCurrentCursorIndex;

		/// <summary>
		/// ��¼�û�ѡ���ı��Ŀ�ʼ����
		/// </summary>
		private int m_intSelectedTextStartIndex;

		/// <summary>
		/// ��¼�û�ѡ���ı��ĳ���
		/// </summary>
		private int m_intSelectedTextLength;

		/// <summary>
		/// ����Ǻ��˼�����ɾ����
		/// </summary>
		private bool m_blnIsBackspace;

		/// <summary>
		/// ����XML���ڴ�
		/// </summary>
		private MemoryStream m_objXmlStream;

		/// <summary>
		/// ����XML�Ĺ���
		/// </summary>
		private XmlTextWriter m_objXmlWriter;

		/// <summary>
		/// ��ȡXML�Ĺ���
		/// </summary>
		private XmlParserContext m_objXmlParser;

		/// <summary>
		/// ����Ƿ���TextChanged�¼�
		/// </summary>
		private bool m_blnCanTextChanged;

		/// <summary>
		/// ����Ƿ�����޸�ѡ�������
		/// </summary>
		private bool m_blnCanModifySelection;

		/// <summary>
		/// �Ƿ��ס���±�
		/// </summary>
		private bool m_blnRememberScripts = true;

		/// <summary>
		/// ���ɼ�������
		/// </summary>
		private Point m_pntEndVisible;

		/// <summary>
		/// ������˫ɾ���������¼�
		/// </summary>
		public event EventHandler m_evtMouseEnterDeleteText;

		/// <summary>
		/// ����뿪˫ɾ���������¼�
		/// </summary>
		public event EventHandler m_evtMouseLeaveDeleteText;

		/// <summary>
		/// ����������ı������¼�
		/// </summary>
		public event EventHandler m_evtMouseEnterInsertText;

		/// <summary>
		/// ����뿪�����ı������¼�
		/// </summary>
		public event EventHandler m_evtMouseLeaveInsertText;

		/// <summary>
		/// ��ǰ˫ɾ���ߵ���������
		/// </summary>
		private int m_intCurrentDSTIndex;

		/// <summary>
		/// ��ǰ�����ı���������
		/// </summary>
		private int m_intCurrentInsertIndex;

		/// <summary>
		/// ��¼����������ʱ����
		/// </summary>
		private Point m_pntMousePositionTemp;

		/// <summary>
		/// ����߶�
		/// </summary>
		private int m_intFontHeight;

		/// <summary>
		/// �ı�����
		/// </summary>
		private int m_intTextLenght;

		/// <summary>
		/// ��ǰ��˫ɾ�����¼�����
		/// </summary>
		private clsDoubleStrikeThoughEventArg m_objCurrentEventArg;

		/// <summary>
		/// ��ǰ�Ĳ����ı��¼�����
		/// </summary>
		private clsInsertEventArg m_objCurrentInsertEventArg;

		/// <summary>
		/// ǰһ�ε��ı�����
		/// </summary>
		private string m_strPrevioslyText;

		/// <summary>
		/// �Ƿ��������뷨ģʽ�£����ʱ�����ػ���
		/// </summary>
		private bool m_blnIMEInput;		

		private StringBuilder m_sbdTemp;

		private clsModifyUserInfo m_objCurrentModifyUser;

		private ArrayList m_arlModifyUsers;

		private ArrayList m_arlTextContentInfos;

		private int m_intCanModifyTime = 6;

		public int m_IntCanModifyTime
		{
			get
			{
				return m_intCanModifyTime;
			}
			set
			{
				m_intCanModifyTime = value;
			}
		}

		public bool m_BlnCanModifyLast
		{
			set
			{
				if(value)
				{		
					if(m_objCurrentModifyUser.m_intUserSequence == -1
						&& m_arlModifyUsers.Count > 0)
					{
						clsModifyUserInfo objLastUser = (clsModifyUserInfo)m_arlModifyUsers[m_arlModifyUsers.Count-1];

						if(objLastUser.m_dtmModifyDate.AddHours(m_intCanModifyTime) >= DateTime.Now)
						{
							m_objCurrentModifyUser = objLastUser;
							m_clrOldPartInsertText = m_objCurrentModifyUser.m_clrText;
						}
					}
					else if(m_arlModifyUsers.Count == 0)
					{
						m_clrOldPartInsertText = m_clrDefaultViewText;
					}
				}
				else
				{
					m_objCurrentModifyUser = new clsModifyUserInfo();
					m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;
					m_objCurrentModifyUser.m_strUserID = m_strUserID;
					m_objCurrentModifyUser.m_strUserName = m_strUserName;
					m_objCurrentModifyUser.m_intUserSequence = -1;	
				}
			}
		}

		private bool m_blnNewEdit;

		private ArrayList m_arlContentViewInfo;

		private bool m_blnIsReadOnly;

		/// <summary>
		/// ȫ�ֿ��ƣ����˫�����Ƿ���˫����
		/// </summary>
		private bool m_blnUnderLineDST = false;

		/// <summary>
		/// ȫ�ֿ���˫�����Ƿ���˫����
		/// </summary>
		public bool m_BlnUnderLineDST
		{
			get
			{
				return m_blnUnderLineDST;
			}
			set
			{
				m_blnUnderLineDST = value;
			}
		}

		/// <summary>
		/// Undo��Redo�Ĺ���
		/// </summary>
		private clsUndoRedo m_objUndoRedoUtil;

		public ctlRichTextBox()
		{
			m_arlDoubleStrikeThrough = new ArrayList();

//			m_arlInsertInfo = new ArrayList();

			m_arlDSTView = new ArrayList();

			m_arlInsertView = new ArrayList();

			m_clrDST = Color.Red;
			m_clrOldPartInsertText = m_clrDefaultViewText;
			m_strUserID = "";
			m_strUserName = "";

			m_blnIsSelectedChanged = false;
			m_blnCanSelectedChanged = true;

			m_intCurrentCursorIndex = 0;

			m_intSelectedTextStartIndex = 0;
			m_intSelectedTextLength = 0;	
		
			m_objXmlStream = new MemoryStream();

			m_objXmlWriter = new XmlTextWriter(m_objXmlStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();

			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

			m_blnCanTextChanged = true;		
	
			m_blnCanModifySelection = true;

			m_pntEndVisible = new Point(this.Width-5,this.Height-5);

			m_intCurrentDSTIndex = -1;

			m_pntMousePositionTemp = new Point();

			m_intFontHeight = this.Font.Height;

			m_intTextLenght = 0;

			m_objCurrentEventArg = new clsDoubleStrikeThoughEventArg();
			m_objCurrentInsertEventArg = new clsInsertEventArg();

			m_strPrevioslyText = "";

			m_blnIMEInput = false;

			m_sbdTemp = new StringBuilder();
			
			m_objCurrentModifyUser = new clsModifyUserInfo();
			m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;
			m_objCurrentModifyUser.m_strUserID = m_strUserID;
			m_objCurrentModifyUser.m_strUserName = m_strUserName;
			m_objCurrentModifyUser.m_intUserSequence = -1;

			m_arlModifyUsers = new ArrayList();

			m_arlTextContentInfos = new ArrayList();

			m_blnNewEdit = true;

			m_arlContentViewInfo = new ArrayList();

			m_blnIsReadOnly = false;

			m_mthInitUndoRedo();
		}

		#region Undo\Redo
		/// <summary>
		/// ��ʼ��Undo\Rodo����
		/// </summary>
		private void m_mthInitUndoRedo()
		{
			clsDoItem objDefault = s_objGetDoItemFromPool();
			clsRichTextItemStatus objStatus = (clsRichTextItemStatus)objDefault.m_objGetDoStatus();

			lock(s_rtbRTFTrans)
			{
				s_rtbRTFTrans.Text = "";
				objStatus.m_strRTF = s_rtbRTFTrans.Rtf;
			}			

			m_objUndoRedoUtil = new clsUndoRedo(objDefault,10);
		}

		private void m_mthClearDoItems(clsDoItem p_objDefault)
		{	
			clsDoItem[] objOutItemArr = m_objUndoRedoUtil.m_objClearDoItems(p_objDefault);
			s_mthAddDoItemInPool(objOutItemArr);
		}

		private void m_mthClearDoItems()
		{
			clsDoItem objDefault = s_objGetDoItemFromPool();
			clsRichTextItemStatus objStatus = (clsRichTextItemStatus)objDefault.m_objGetDoStatus();

			lock(s_rtbRTFTrans)
			{
				s_rtbRTFTrans.Text = "";
				objStatus.m_strRTF = s_rtbRTFTrans.Rtf;
			}			

			m_mthClearDoItems(objDefault);
		}

		/// <summary>
		/// ����˫ɾ������Ϣ
		/// </summary>
		/// <param name="p_arlSourceDST">ԭ��Ϣ��</param>
		/// <param name="p_arlDestDST">Ŀ����Ϣ��</param>
		/// <param name="p_blnClearDest">�Ƿ����Ŀ����Ϣ��</param>
		private void m_mthCopyDST(ArrayList p_arlSourceDST,ArrayList p_arlDestDST,bool p_blnClearDest)
		{
			if(p_blnClearDest)
				p_arlDestDST.Clear();

			for(int i=0;i<p_arlSourceDST.Count;i++)
			{
				clsDSTInfo objInfo = (clsDSTInfo)p_arlSourceDST[i];

				clsDSTInfo objDoDSTInfo = s_objGetDSTInfoFromPool();
				objDoDSTInfo.m_clrDST = objInfo.m_clrDST;
				objDoDSTInfo.m_dtmDeleteTime = objInfo.m_dtmDeleteTime;
				objDoDSTInfo.m_intIndexRange = objInfo.m_intIndexRange;
				objDoDSTInfo.m_intUserSequence = objInfo.m_intUserSequence;
				objDoDSTInfo.m_strUserID = objInfo.m_strUserID;
				objDoDSTInfo.m_strUserName = objInfo.m_strUserName;

				p_arlDestDST.Add(objDoDSTInfo);
			}
		}

		/// <summary>
		/// �����ı�������Ϣ
		/// </summary>
		/// <param name="p_arlSourceTextContentInfo">ԭ��Ϣ��</param>
		/// <param name="p_arlDestTextContentInfo">Ŀ����Ϣ��</param>
		/// <param name="p_blnClearDest">�Ƿ����Ŀ����Ϣ��</param>
		private void m_mthCopyTextContentInfo(ArrayList p_arlSourceTextContentInfo,ArrayList p_arlDestTextContentInfo,bool p_blnClearDest)
		{
			if(p_blnClearDest)
				p_arlDestTextContentInfo.Clear();

			for(int i=0;i<p_arlSourceTextContentInfo.Count;i++)
			{
				clsUserContentInfo objContentInfo = 
					(clsUserContentInfo)p_arlSourceTextContentInfo[i];

				clsUserContentInfo objDoContentInfo = s_objGetTextContentFromPool();

				objDoContentInfo.m_intEndIndex = objContentInfo.m_intEndIndex;
				objDoContentInfo.m_intStartIndex = objContentInfo.m_intStartIndex;
				objDoContentInfo.objUserInfo = objContentInfo.objUserInfo;

				p_arlDestTextContentInfo.Add(objDoContentInfo);
			}
		}

		/// <summary>
		/// ��ӵ�ǰ������Ϣ
		/// </summary>
		private void m_mthAddCurrentDo()
		{
			clsDoItem objCurrentDo = s_objGetDoItemFromPool();
			clsRichTextItemStatus objStatus = (clsRichTextItemStatus)objCurrentDo.m_objGetDoStatus();

			objStatus.m_strRTF = this.Rtf;
			objStatus.m_intCurrentCursorIndex = this.m_intCurrentCursorIndex;
			m_mthCopyDST(m_arlDoubleStrikeThrough,objStatus.m_arlDoubleStrikeThrough,true);
			m_mthCopyTextContentInfo(m_arlTextContentInfos,objStatus.m_arlTextContentInfos,true);
			
			clsDoItem[] objOutItemArr = m_objUndoRedoUtil.m_objAddCurrentDoItem(objCurrentDo);
			s_mthAddDoItemInPool(objOutItemArr);
		}

		#region Do Item
		/// <summary>
		/// DoItem�����
		/// </summary>
		private static Queue s_queDoItemPool = new Queue();
		/// <summary>
		/// �ӻ���ػ�ȡDoItem��
		/// </summary>
		/// <returns></returns>
		private static clsDoItem s_objGetDoItemFromPool()
		{
			lock(s_queDoItemPool)
			{
				if(s_queDoItemPool.Count != 0)
				{
					return (clsDoItem)s_queDoItemPool.Dequeue();
				}
			}

			clsRichTextItemStatus objStatus = new clsRichTextItemStatus();			

			clsDoItem objDoItem = new clsDoItem(objStatus);

			return objDoItem;
		}
		/// <summary>
		/// ���DoItem�������
		/// </summary>
		/// <param name="p_objDoItemArr">DoItem����</param>
		private static void s_mthAddDoItemInPool(clsDoItem[] p_objDoItemArr)
		{
			if(p_objDoItemArr == null)
				return;

			for(int i=0;i<p_objDoItemArr.Length;i++)
			{
				s_mthAddDoItemInPool(p_objDoItemArr[i]);
			}
		}
		/// <summary>
		/// ���DoItem�������
		/// </summary>
		/// <param name="p_objDoItem">DoItem</param>
		private static void s_mthAddDoItemInPool(clsDoItem p_objDoItem)
		{
			lock(s_queDoItemPool)
			{
				clsRichTextItemStatus objStatus = (clsRichTextItemStatus)p_objDoItem.m_objGetDoStatus();

				for(int i=0;i<objStatus.m_arlDoubleStrikeThrough.Count;i++)
				{
					clsDSTInfo objDSTInfo = (clsDSTInfo)objStatus.m_arlDoubleStrikeThrough[i];
					s_mthAddDSTInfoInPool(objDSTInfo);
				}
				objStatus.m_arlDoubleStrikeThrough.Clear();

				for(int i=0;i<objStatus.m_arlTextContentInfos.Count;i++)
				{
					clsUserContentInfo objContentInfo = (clsUserContentInfo)objStatus.m_arlTextContentInfos[i];
					s_mthAddContentInfoInPool(objContentInfo);
				}
				objStatus.m_arlTextContentInfos.Clear();

				s_queDoItemPool.Enqueue(p_objDoItem);
			}
		}
		#endregion

		#region DST
		/// <summary>
		/// ˫���߼�¼�Ļ����
		/// </summary>
		private static Queue s_queDSTPool = new Queue();
		/// <summary>
		/// �ӻ���ػ�ȡ˫���߼�¼��
		/// </summary>
		/// <returns></returns>
		private static clsDSTInfo s_objGetDSTInfoFromPool()
		{
			lock(s_queDSTPool)
			{
				if(s_queDSTPool.Count != 0)
				{
					return (clsDSTInfo)s_queDSTPool.Dequeue();
				}
			}		

			return new clsDSTInfo();
		}
		/// <summary>
		/// ���˫���߼�¼�������
		/// </summary>
		/// <param name="p_objDSTInfo"></param>
		private static void s_mthAddDSTInfoInPool(clsDSTInfo p_objDSTInfo)
		{
			lock(s_queDSTPool)
			{
				s_queDSTPool.Enqueue(p_objDSTInfo);
			}
		}
		#endregion

		#region Content
		/// <summary>
		/// ����ı���Ϣ�Ļ����
		/// </summary>
		private static Queue s_queContentPool = new Queue();
		/// <summary>
		/// �ӻ���ػ�ȡ�ı���Ϣ��
		/// </summary>
		/// <returns></returns>
		private static clsUserContentInfo s_objGetTextContentFromPool()
		{
			lock(s_queContentPool)
			{
				if(s_queContentPool.Count != 0)
				{
					return (clsUserContentInfo)s_queContentPool.Dequeue();
				}
			}		

			return new clsUserContentInfo();
		}
		/// <summary>
		/// ����ı���Ϣ�������
		/// </summary>
		/// <param name="p_objContentInfo"></param>
		private static void s_mthAddContentInfoInPool(clsUserContentInfo p_objContentInfo)
		{
			lock(s_queContentPool)
			{
				s_queContentPool.Enqueue(p_objContentInfo);
			}
		}
		#endregion

		public void m_mthUndo()
		{
			if(m_objUndoRedoUtil.m_blnCanUndo())
			{
				clsDoItem objDo = m_objUndoRedoUtil.m_objUndo();

				m_mthSetDo(objDo);
			}
		}

		public void m_mthRedo()
		{
			if(m_objUndoRedoUtil.m_blnCanRedo())
			{
				clsDoItem objDo = m_objUndoRedoUtil.m_objRedo();

				m_mthSetDo(objDo);
			}
		}

		private void m_mthSetDo(clsDoItem p_objDoItem)
		{
			if(p_objDoItem == null || p_objDoItem.m_objGetDoStatus() == null)
				return;

			m_blnCanSelectedChanged = false;
			m_blnCanTextChanged = false;
			
					
			clsRichTextItemStatus objStatus = (clsRichTextItemStatus)p_objDoItem.m_objGetDoStatus();

			this.Rtf = objStatus.m_strRTF;
			m_mthCopyDST(objStatus.m_arlDoubleStrikeThrough,this.m_arlDoubleStrikeThrough,true);
			m_mthCopyTextContentInfo(objStatus.m_arlTextContentInfos,this.m_arlTextContentInfos,true);
			

			this.SelectionStart = objStatus.m_intCurrentCursorIndex;

			m_mthUpdateCurrentStatus();

			m_blnCanTextChanged = true;
			m_blnCanSelectedChanged = true;	

			m_blnIsSelectedChanged = this.SelectionLength != 0;

			m_intPreviouslyLen = this.TextLength;

			m_intCurrentDSTIndex = -1;

			m_intTextLenght = m_intPreviouslyLen;

			m_strPrevioslyText = this.Text;

			m_blnIMEInput = false;

			this.Invalidate();
		}
		#endregion

		protected override  void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if(m.Msg == 0x000F)//Repaint�¼�
			{				
				Graphics objGrp = this.CreateGraphics();

				if(!m_blnIMEInput)
					m_mthDrawDST(objGrp);

				objGrp.Dispose();
			}			
		}

		#region ��˫ɾ����
		private void m_mthDrawDST(Graphics p_objGrp)
		{
			try
			{
				Pen penStrike = new Pen(m_clrDST);				
				
				int intStartVisibleCharIndex = this.GetCharIndexFromPosition(Point.Empty);
				int intEndVisibleCharIndex = this.GetCharIndexFromPosition(m_pntEndVisible);

				if(intEndVisibleCharIndex == 0)
				{
					if(this.Text.Length != 1)
						return;
				}

				m_arlContentViewInfo.Clear();

			#region �����ھ������в���
				for(int i=0;i<m_arlTextContentInfos.Count;i++)
				{
					clsUserContentInfo objContentInfo = (clsUserContentInfo)m_arlTextContentInfos[i];

					if(objContentInfo.m_intEndIndex < intStartVisibleCharIndex)
						continue;

					if(intEndVisibleCharIndex > 0 && objContentInfo.m_intStartIndex > intEndVisibleCharIndex)
						break;

					int intStartIndex = objContentInfo.m_intStartIndex;
					int intEndIndex = objContentInfo.m_intEndIndex;

					if(intStartIndex < 0 ||
						intEndIndex < 0 ||
						intStartIndex >= this.TextLength || 
						intEndIndex >= this.TextLength)
						continue;
				
					while(intStartIndex<=intEndIndex)
					{
						if(intStartIndex < intStartVisibleCharIndex)
						{
							intStartIndex++;
							continue;
						}

						if(intEndVisibleCharIndex > 0 && intEndIndex > intEndVisibleCharIndex)
							intEndIndex = intEndVisibleCharIndex;

						int intUpLine = this.GetLineFromCharIndex(intStartIndex);
						int intDownLine = this.GetLineFromCharIndex(intEndIndex);

						if(intUpLine == intDownLine)
						{
						#region ͷβͬһ��
							Point pntStart = this.GetPositionFromCharIndex(intStartIndex);							

							int intLineWidth = 0;
				
							if(intEndIndex+1 < this.Text.Length)
							{
								Point pntEnd = this.GetPositionFromCharIndex(intEndIndex+1);
				
								if(pntEnd.Y == pntStart.Y)
								{
									intLineWidth = pntEnd.X - pntStart.X;
								}
								else
									intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
							}		
							else
								intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
				
							clsUserContentViewInfo objViewInfo = new clsUserContentViewInfo();
							objViewInfo.m_objUserContentInfo = objContentInfo;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlContentViewInfo.Add(objViewInfo);

						#endregion
						}
						else
						{
							Point pntTemp = new Point(this.Width,0);

							Point pntStart = this.GetPositionFromCharIndex(intStartIndex);							

							if(pntStart.Y < 0)
							{
								intStartIndex++;
								continue;
							}
							
							pntTemp.Y = pntStart.Y;

							int intEndCharIndex = this.GetCharIndexFromPosition(pntTemp)-1;							
							int intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
							intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(),this.Font).Width-5-pntStart.X;				

							clsUserContentViewInfo objViewInfo = new clsUserContentViewInfo();
							objViewInfo.m_objUserContentInfo = objContentInfo;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlContentViewInfo.Add(objViewInfo);


							intUpLine++;

							while(intUpLine<intDownLine)
							{
								int intStartCharIndex = intEndCharIndex+1;

								pntStart = this.GetPositionFromCharIndex(intEndCharIndex+1);
								
//								if(pntStart.Y == pntTemp.Y)
//									pntStart = this.GetPositionFromCharIndex(intEndCharIndex+2);
								int intCharAdd = 2;
								while(pntStart.Y == pntTemp.Y)
								{
									intStartCharIndex = intEndCharIndex+intCharAdd;
									pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
									intCharAdd++;
								}

								pntTemp.Y = pntStart.Y;

								intEndCharIndex = this.GetCharIndexFromPosition(pntTemp)-1;

								if(intStartCharIndex < this.Text.Length)
								{
									if(intEndCharIndex-intStartCharIndex <= 0 || this.Text.Substring(intStartCharIndex,intEndCharIndex-intStartCharIndex-1).Trim() == "")
									{
										intUpLine++;
										continue;
									}
								}
							
								intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
								intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(),this.Font).Width-5-pntStart.X;				

								objViewInfo = new clsUserContentViewInfo();
								objViewInfo.m_objUserContentInfo = objContentInfo;
								objViewInfo.m_intStartX = pntStart.X;
								objViewInfo.m_intEndX = pntStart.X+intLineWidth;
								objViewInfo.m_intUpY = pntStart.Y;
								objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
								m_arlContentViewInfo.Add(objViewInfo);


								intUpLine++;
							}

							pntStart = this.GetPositionFromCharIndex(intEndCharIndex+1);

//							if(pntStart.Y == pntTemp.Y)
//								pntStart = this.GetPositionFromCharIndex(intEndCharIndex+2);
							int intCharAdd1 = 2;
							while(pntStart.Y == pntTemp.Y)
							{
								int intStartCharIndex = intEndCharIndex+intCharAdd1;
								pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
								intCharAdd1++;
							}
							
							if(intEndIndex+1 < this.Text.Length)
							{
								Point pntEnd = this.GetPositionFromCharIndex(intEndIndex+1);
				
								if(pntEnd.Y == pntStart.Y)
								{
									intLineWidth = pntEnd.X - pntStart.X;
								}
								else
									intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
							}		
							else
								intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
				
							objViewInfo = new clsUserContentViewInfo();
							objViewInfo.m_objUserContentInfo = objContentInfo;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlContentViewInfo.Add(objViewInfo);

						}

						break;
					}
				}
			#endregion

				m_arlDSTView.Clear();

			#region ���㻭˫ɾ����
				for(int i=0;i<m_arlDoubleStrikeThrough.Count;i++)
				{
					clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];
					i++;
					clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];

					if(objEndInfo.m_intIndexRange < intStartVisibleCharIndex)
						continue;

					if(intEndVisibleCharIndex > 0 && objStartInfo.m_intIndexRange > intEndVisibleCharIndex)
						break;

					penStrike.Color = objStartInfo.m_clrDST;
				
					int intStartIndex = objStartInfo.m_intIndexRange;
					int intEndIndex = objEndInfo.m_intIndexRange;

					if(intStartIndex < 0 ||
						intEndIndex < 0 ||
						intStartIndex >= m_intPreviouslyLen || 
						intEndIndex >= m_intPreviouslyLen)
						continue;
				
					while(intStartIndex<=intEndIndex)
					{
						if(intStartIndex < intStartVisibleCharIndex)
						{
							intStartIndex++;
							continue;
						}

						if(intEndVisibleCharIndex > 0 && intEndIndex > intEndVisibleCharIndex)
							intEndIndex = intEndVisibleCharIndex;

						int intUpLine = this.GetLineFromCharIndex(intStartIndex);
						int intDownLine = this.GetLineFromCharIndex(intEndIndex);

						penStrike.Color = objStartInfo.m_clrDST;

						if(intUpLine == intDownLine)
						{
						#region ͷβͬһ��
							Point pntStart = this.GetPositionFromCharIndex(intStartIndex);							

							int intLineWidth = 0;
				
							if(intEndIndex+1 < this.Text.Length)
							{
								Point pntEnd = this.GetPositionFromCharIndex(intEndIndex+1);
				
								if(pntEnd.Y == pntStart.Y)
								{
									intLineWidth = pntEnd.X - pntStart.X;
								}
								else
									intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
							}		
							else
								intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
				
							clsDSTViewInfo objViewInfo = new clsDSTViewInfo();
							objViewInfo.m_intDSTInfoStartIndex = i-1;
							objViewInfo.m_intDSTInfoEndIndex = i;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlDSTView.Add(objViewInfo);

							if(!m_blnUnderLineDST)
							{
								pntStart.Offset(0,8);
							}
							else
							{
								pntStart.Offset(0,objViewInfo.m_intDownY+4);
							}
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
							pntStart.Offset(0,3);
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);

						#endregion
						}
						else
						{
							Point pntTemp = new Point(this.Width,0);

							Point pntStart = this.GetPositionFromCharIndex(intStartIndex);							

							if(pntStart.Y < 0)
							{
								intStartIndex++;
								continue;
							}
							
							pntTemp.Y = pntStart.Y;						

							int intEndCharIndex = this.GetCharIndexFromPosition(pntTemp)-1;							
							int intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
							intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(),this.Font).Width-5-pntStart.X;				

							clsDSTViewInfo objViewInfo = new clsDSTViewInfo();
							objViewInfo.m_intDSTInfoStartIndex = i-1;
							objViewInfo.m_intDSTInfoEndIndex = i;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlDSTView.Add(objViewInfo);

							if(!m_blnUnderLineDST)
							{
								pntStart.Offset(0,8);
							}
							else
							{
								pntStart.Offset(0,objViewInfo.m_intDownY+4);
							}
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
							pntStart.Offset(0,3);
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
							
							intUpLine++;

							while(intUpLine<intDownLine)
							{
								int intStartCharIndex = intEndCharIndex+1;

								pntStart = this.GetPositionFromCharIndex(intEndCharIndex+1);
								
								int intCharAdd = 2;
								while(pntStart.Y == pntTemp.Y)
								{
									intStartCharIndex = intEndCharIndex+intCharAdd;
									pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
									intCharAdd++;
								}

								pntTemp.Y = pntStart.Y;

								intEndCharIndex = this.GetCharIndexFromPosition(pntTemp)-1;

								if(intStartCharIndex < this.Text.Length)
								{
									if(intEndCharIndex-intStartCharIndex == 0 || intEndCharIndex-intStartCharIndex <= 0 || this.Text.Substring(intStartCharIndex,intEndCharIndex-intStartCharIndex-1).Trim() == "")
									{
										intUpLine++;
										continue;
									}
								}
								
								intLineWidth = this.GetPositionFromCharIndex(intEndCharIndex).X;
								intLineWidth += (int)p_objGrp.MeasureString(this.Text[intEndCharIndex].ToString(),this.Font).Width-5-pntStart.X;				

								objViewInfo = new clsDSTViewInfo();
								objViewInfo.m_intDSTInfoStartIndex = i-1;
								objViewInfo.m_intDSTInfoEndIndex = i;
								objViewInfo.m_intStartX = pntStart.X;
								objViewInfo.m_intEndX = pntStart.X+intLineWidth;
								objViewInfo.m_intUpY = pntStart.Y;
								objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
								m_arlDSTView.Add(objViewInfo);

								if(!m_blnUnderLineDST)
								{
									pntStart.Offset(0,8);
								}
								else
								{
									pntStart.Offset(0,objViewInfo.m_intDownY+4);
								}
								p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
								pntStart.Offset(0,3);
								p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
							
								intUpLine++;
							}

							pntStart = this.GetPositionFromCharIndex(intEndCharIndex+1);

							int intCharAdd1 = 2;
							while(pntStart.Y == pntTemp.Y)
							{
								int intStartCharIndex = intEndCharIndex+intCharAdd1;
								pntStart = this.GetPositionFromCharIndex(intStartCharIndex);
								intCharAdd1++;
							}

							if(intEndIndex+1 < this.Text.Length)
							{
								Point pntEnd = this.GetPositionFromCharIndex(intEndIndex+1);
				
								if(pntEnd.Y == pntStart.Y)
								{
									intLineWidth = pntEnd.X - pntStart.X;
								}
								else
									intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
							}		
							else
								intLineWidth = this.GetPositionFromCharIndex(intEndIndex).X+(int)p_objGrp.MeasureString(this.Text[intEndIndex].ToString(),this.Font).Width-5-pntStart.X;
				
							objViewInfo = new clsDSTViewInfo();
							objViewInfo.m_intDSTInfoStartIndex = i-1;
							objViewInfo.m_intDSTInfoEndIndex = i;
							objViewInfo.m_intStartX = pntStart.X;
							objViewInfo.m_intEndX = pntStart.X+intLineWidth;
							objViewInfo.m_intUpY = pntStart.Y;
							objViewInfo.m_intDownY = pntStart.Y+m_intFontHeight;
							m_arlDSTView.Add(objViewInfo);

							if(!m_blnUnderLineDST)
							{
								pntStart.Offset(0,8);
							}
							else
							{
								pntStart.Offset(0,objViewInfo.m_intDownY+4);
							}
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);
							pntStart.Offset(0,3);
							p_objGrp.DrawLine(penStrike,pntStart.X,pntStart.Y,pntStart.X+intLineWidth,pntStart.Y);							
						}

						break;
					}
				}
			#endregion

				penStrike.Dispose();	
			}
			catch(Exception err)
			{
				string strErr = err.Message;
			}
		}
		#endregion

		#region ����ѡ����ַ�������˫ɾ����
		/// <summary>
		/// ����ѡ����ַ�������˫ɾ����
		/// </summary>
		/// <param name="p_blnAddDST">�Ƿ������˫ɾ����</param>
		public void m_mthSelectionDoubleStrikeThough(bool p_blnAddDST)
		{
			/*
			 * ��Ϊ��ʹ���滻�����Ѿɵ�˫ɾ����ȥ����
			 * Ȼ�������ѡ������˫ɾ���ߡ�
			 * �����Ϊѡ������˫ɾ���ߣ�����ʵ����ȥ��˫ɾ���ߡ�
			 */
			if(ReadOnly)
				return;

			m_blnNewEdit = true;

			int intStart = this.SelectionStart;
			int intLen = this.SelectionLength;
			int intEnd = intStart+intLen-1;

			bool blnAdded = false;

			m_mthHandleReplace(intStart,intLen,0);

			if(p_blnAddDST)
			{
				for(int i=0;i<m_arlDoubleStrikeThrough.Count;i++)
				{
					clsDSTInfo objInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];

					if(objInfo.m_intIndexRange >= intStart && i%2==0)
					{
						clsDSTInfo objNewStartInfo = new clsDSTInfo();
						objNewStartInfo.m_clrDST = m_clrDST;
						objNewStartInfo.m_strUserID = m_strUserID;
						objNewStartInfo.m_strUserName = m_strUserName;
						objNewStartInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
						objNewStartInfo.m_intIndexRange = intStart;
						m_arlDoubleStrikeThrough.Insert(i,objNewStartInfo);
				
						clsDSTInfo objNewEndInfo = new clsDSTInfo();
						objNewEndInfo.m_clrDST = m_clrDST;
						objNewEndInfo.m_strUserID = m_strUserID;
						objNewEndInfo.m_strUserName = m_strUserName;
						objNewEndInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
						objNewEndInfo.m_intIndexRange = intEnd;
						m_arlDoubleStrikeThrough.Insert(i+1,objNewEndInfo);

						blnAdded = true;
						break;
					}
				}

				if(!blnAdded)
				{
					clsDSTInfo objNewStartInfo = new clsDSTInfo();
					objNewStartInfo.m_clrDST = m_clrDST;
					objNewStartInfo.m_strUserID = m_strUserID;
					objNewStartInfo.m_strUserName = m_strUserName;
					objNewStartInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
					objNewStartInfo.m_intIndexRange = intStart;
					m_arlDoubleStrikeThrough.Add(objNewStartInfo);
			
					clsDSTInfo objNewEndInfo = new clsDSTInfo();
					objNewEndInfo.m_clrDST = m_clrDST;
					objNewEndInfo.m_strUserID = m_strUserID;
					objNewEndInfo.m_strUserName = m_strUserName;
					objNewEndInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
					objNewEndInfo.m_intIndexRange = intEnd;
					m_arlDoubleStrikeThrough.Add(objNewEndInfo);
				}
			}

			m_intCurrentDSTIndex = -1;

			m_blnCanSelectedChanged = false;
			this.SelectionLength = 0;
			m_blnCanSelectedChanged = true;

			m_blnIsSelectedChanged = this.SelectionLength != 0;

			m_mthAddCurrentDo();

			this.Invalidate();
		}
		#endregion

		#region �����û������ı�
		private void m_mthReplaceChar(char p_chrOle,char p_chrNew)
		{
			lock(s_rtbRTFTrans)
			{
				s_rtbRTFTrans.Rtf = this.Rtf;
				s_rtbRTFTrans.Text = s_rtbRTFTrans.Text.Replace(p_chrOle,p_chrNew);

				this.Rtf = s_rtbRTFTrans.Rtf;
			}
		}
		protected override void OnTextChanged(System.EventArgs e)
		{
			if(!m_blnCanTextChanged)
				return;

			m_blnNewEdit = true;
			
			if(!m_blnIsSelectedChanged)
			{
				m_mthHandleNotSelectedChanged();
			}
			else
			{
				if(!m_blnCanModifySelection)
				{
					m_blnCanSelectedChanged = false;

					m_blnCanTextChanged = false;
					this.Text = m_strPrevioslyText;
					m_blnCanTextChanged = true;

					this.SelectionStart = m_intCurrentCursorIndex;

					m_blnCanSelectedChanged = true;
					return;
				}

				m_mthHandleSelectedChanged();
			}

			if(this.Text.IndexOf('\t') >= 0)
			{
				m_blnCanSelectedChanged = false;
				m_blnCanTextChanged = false;
				m_mthReplaceChar('\t',' ');
				this.SelectionStart = m_intCurrentCursorIndex;
				m_blnCanTextChanged = true;
				m_blnCanSelectedChanged = true;
			}

			m_intPreviouslyLen = this.TextLength;

			m_intCurrentDSTIndex = -1;

			m_intTextLenght = m_intPreviouslyLen;

			m_strPrevioslyText = this.Text;

			m_blnIMEInput = false;

			m_mthAddCurrentDo();

			base.OnTextChanged(e);
		}
		
		/// <summary>
		/// �û�ѡ�����ı����ٽ���
		/// </summary>
		private void m_mthHandleSelectedChanged()
		{
			/*
			 * �û���ѡ�����ı�����н�����
			 * �������������
			 * ������ѡ����ı�ȫ��ɾ����
			 * ���µ��ı������Ȳ���������ѡ����ı���
			 */
			m_blnCanSelectedChanged = false;

			int intDiffLen = this.TextLength - m_intPreviouslyLen;

			if(-1*intDiffLen == m_intSelectedTextLength)
			{
				//ɾ��ѡ��
				m_intCurrentCursorIndex = m_intSelectedTextStartIndex;
				m_blnIsBackspace = false;

				m_mthHandleDelete(m_intSelectedTextLength);
			}
			else
			{
				//�滻
				m_intCurrentCursorIndex = m_intSelectedTextStartIndex;
				
				m_mthHandleReplace(m_intSelectedTextStartIndex,m_intSelectedTextLength,intDiffLen);

				int intTempStartIndex = SelectionStart;
				SelectionStart = m_intCurrentCursorIndex;
				SelectionLength = m_intSelectedTextLength+intDiffLen; 
				SelectionColor = m_clrOldPartInsertText;	
	
				SelectionStart += SelectionLength;
				SelectionLength = 0;
			}			

			m_blnIsSelectedChanged = false;
			m_blnCanSelectedChanged = true;
		}

		/// <summary>
		/// �����û�û��ѡ���ı������еĽ���
		/// </summary>
		private void m_mthHandleNotSelectedChanged()
		{
			/*
			 * �û�û��ѡ���ı�������µĽ�����
			 * �������������
			 * �ı��䳤��������ӣ�
			 * �ı���̣��ӹ�꿪ʼ��ǰ�����ɾ����
			 */
			int intDiffLen = this.TextLength - m_intPreviouslyLen;

			if(intDiffLen > 0)
			{
				//����
				m_mthHandleInsert(intDiffLen);
			}
			else
			{
				//ɾ��
				m_mthHandleDelete(-1*intDiffLen);
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="p_intNewLen">����ӵĳ���</param>
		private void m_mthHandleInsert(int p_intNewLen)
		{
			for(int i=m_arlDoubleStrikeThrough.Count-1;i>=0;i--)
			{
				clsDSTInfo objInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];

				if(objInfo.m_intIndexRange > m_intCurrentCursorIndex-1)
					objInfo.m_intIndexRange = objInfo.m_intIndexRange+p_intNewLen;
				else
					break;
			}		
	
			bool blnTempCanSel = m_blnCanSelectedChanged;
			m_blnCanSelectedChanged = false;
			int intTempStartIndex = SelectionStart;
			SelectionStart = m_intCurrentCursorIndex;
			SelectionLength = p_intNewLen;
			SelectionColor = m_clrOldPartInsertText;

			SelectionLength = 0;			
			SelectionStart = intTempStartIndex;
			m_blnCanSelectedChanged = blnTempCanSel;

			bool blnJustUpDate = false;
			int intIndex = 0;
			while(intIndex < m_arlTextContentInfos.Count)
			{
				clsUserContentInfo objContentInfo = 
					(clsUserContentInfo)m_arlTextContentInfos[intIndex];

				if(objContentInfo.m_intEndIndex < m_intCurrentCursorIndex)
				{
					if(intIndex == m_arlTextContentInfos.Count-1)
					{
						if(objContentInfo.objUserInfo == m_objCurrentModifyUser)
						{
							objContentInfo.m_intEndIndex += p_intNewLen;
						}
						else
						{
							clsUserContentInfo objInfo = new clsUserContentInfo();
							objInfo.objUserInfo = m_objCurrentModifyUser;
							objInfo.m_intStartIndex = m_intCurrentCursorIndex;
							objInfo.m_intEndIndex = m_intCurrentCursorIndex+p_intNewLen-1;

							m_arlTextContentInfos.Add(objInfo);
						}

						break;
					}
					else if(objContentInfo.objUserInfo == m_objCurrentModifyUser)
					{
						if(objContentInfo.m_intEndIndex+1 == m_intCurrentCursorIndex)
						{
							objContentInfo.m_intEndIndex += p_intNewLen;
							blnJustUpDate = true;
						}
					}

					intIndex++;
				}
				else
				{
					if(blnJustUpDate)
					{
						objContentInfo.m_intStartIndex += p_intNewLen;
						objContentInfo.m_intEndIndex += p_intNewLen;

						intIndex++;
					}
					else
					{
						if(objContentInfo.objUserInfo == m_objCurrentModifyUser)
						{
							objContentInfo.m_intEndIndex += p_intNewLen;

							intIndex++;
						}
						else
						{
							if(objContentInfo.m_intStartIndex < m_intCurrentCursorIndex)
							{
								int intEndIndex = objContentInfo.m_intEndIndex;

								objContentInfo.m_intEndIndex = m_intCurrentCursorIndex-1;

								intIndex++;

								clsUserContentInfo objNewInfo = new clsUserContentInfo();
								objNewInfo.objUserInfo = m_objCurrentModifyUser;
								objNewInfo.m_intStartIndex = m_intCurrentCursorIndex;
								objNewInfo.m_intEndIndex = m_intCurrentCursorIndex+p_intNewLen-1;

								m_arlTextContentInfos.Insert(intIndex,objNewInfo);

								intIndex++;

								clsUserContentInfo objOldInfo = new clsUserContentInfo();
								objOldInfo.objUserInfo = objContentInfo.objUserInfo;
								objOldInfo.m_intStartIndex = m_intCurrentCursorIndex+p_intNewLen;
								objOldInfo.m_intEndIndex = intEndIndex+p_intNewLen;

								m_arlTextContentInfos.Insert(intIndex,objOldInfo);

								intIndex++;
							}
							else
							{
								if(objContentInfo.objUserInfo == m_objCurrentModifyUser)
								{
									objContentInfo.m_intEndIndex += p_intNewLen;
									intIndex++;
								}
								else
								{
									if(((clsUserContentInfo)m_arlTextContentInfos[intIndex]).objUserInfo != m_objCurrentModifyUser)
									{
										clsUserContentInfo objNewInfo = new clsUserContentInfo();
										objNewInfo.objUserInfo = m_objCurrentModifyUser;
										objNewInfo.m_intStartIndex = m_intCurrentCursorIndex;
										objNewInfo.m_intEndIndex = m_intCurrentCursorIndex+p_intNewLen-1;

										m_arlTextContentInfos.Insert(intIndex,objNewInfo);

										intIndex++;
									}
								}
							}
						}

						blnJustUpDate = true;
					}
				}
			}

			m_intCurrentCursorIndex = this.SelectionStart;
		}		
		/// <summary>
		/// �����滻
		/// </summary>
		/// <param name="p_intStartIndex">�ı��ı��Ŀ�ʼ����</param>
		/// <param name="p_intOldLength">���ı��ĳ���</param>
		/// <param name="p_intDiffLength">���ı��;��ı����ĳ��ȡ�</param>
		private void m_mthHandleReplace(int p_intStartIndex,int p_intOldLength,int p_intDiffLength)
		{
			int intEndIndex = p_intStartIndex+p_intOldLength-1;

			clsDSTInfo [] objDSTInfoArr = (clsDSTInfo [])m_arlDoubleStrikeThrough.ToArray(typeof(clsDSTInfo));

			int intDSTIndex = 0;

			m_arlDoubleStrikeThrough.Clear();

			bool blnIsFirst = true;
			bool blnFinished = false;

			while(intDSTIndex<objDSTInfoArr.Length)
			{
				if(blnFinished)
				{
					m_arlDoubleStrikeThrough.Add(objDSTInfoArr[intDSTIndex]);
					intDSTIndex++;
					continue;
				}

				if(blnIsFirst)
				{
					if(objDSTInfoArr[intDSTIndex].m_intIndexRange < p_intStartIndex)
					{
						m_arlDoubleStrikeThrough.Add(objDSTInfoArr[intDSTIndex]);
						intDSTIndex++;
					}
					else if(intDSTIndex%2 == 0)
					{
						blnIsFirst = false;
					}
					else
					{						
						clsDSTInfo objNewInfo = new clsDSTInfo();
						objNewInfo.m_clrDST = objDSTInfoArr[intDSTIndex].m_clrDST;
						objNewInfo.m_strUserID = objDSTInfoArr[intDSTIndex].m_strUserID;
						objNewInfo.m_strUserName = objDSTInfoArr[intDSTIndex].m_strUserName;
						objNewInfo.m_intUserSequence = objDSTInfoArr[intDSTIndex].m_intUserSequence;
						objNewInfo.m_dtmDeleteTime = objDSTInfoArr[intDSTIndex].m_dtmDeleteTime;
						objNewInfo.m_intIndexRange = p_intStartIndex-1;
						m_arlDoubleStrikeThrough.Add(objNewInfo);
						blnIsFirst = false;
					}
				}
				else
				{
					if(objDSTInfoArr[intDSTIndex].m_intIndexRange <= intEndIndex)
					{
						intDSTIndex++;
					}
					else if(intDSTIndex%2 == 0)
					{
						m_arlDoubleStrikeThrough.Add(objDSTInfoArr[intDSTIndex]);
						intDSTIndex++;
						blnFinished = true;
					}
					else
					{
						clsDSTInfo objNewInfo = new clsDSTInfo();
						objNewInfo.m_clrDST = objDSTInfoArr[intDSTIndex].m_clrDST;
						objNewInfo.m_strUserID = objDSTInfoArr[intDSTIndex].m_strUserID;
						objNewInfo.m_strUserName = objDSTInfoArr[intDSTIndex].m_strUserName;
						objNewInfo.m_intUserSequence = objDSTInfoArr[intDSTIndex].m_intUserSequence;
						objNewInfo.m_dtmDeleteTime = objDSTInfoArr[intDSTIndex].m_dtmDeleteTime;
						objNewInfo.m_intIndexRange = intEndIndex+1;
						m_arlDoubleStrikeThrough.Add(objNewInfo);
						blnFinished = true;
					}
				}
			}
			
			for(int i=m_arlDoubleStrikeThrough.Count-1;i>=0;i--)
			{
				clsDSTInfo objInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];

				if(objInfo.m_intIndexRange >= intEndIndex)
					objInfo.m_intIndexRange = objInfo.m_intIndexRange+p_intDiffLength;
				else
					break;
			}

			//ֻ�����Լ���ӵ������滻�����ɴ˾���������ض���������
			//�����ֻ��һ���������滻��
			//����Ҫ���滻���������������
			for(int i=0;i<m_arlTextContentInfos.Count;i++)
			{
				clsUserContentInfo objContentInfo = 
					(clsUserContentInfo)m_arlTextContentInfos[i];

				if(objContentInfo.m_intStartIndex <= p_intStartIndex
					&& objContentInfo.m_intEndIndex >= p_intStartIndex)					
				{
					//���������滻���򣬸��½�������
					objContentInfo.m_intEndIndex += p_intDiffLength;

					if(objContentInfo.m_intEndIndex < objContentInfo.m_intStartIndex)
					{
						//������ɾ��
						m_arlTextContentInfos.RemoveAt(i);
						i--;
					}
				}					
				else if(objContentInfo.m_intStartIndex > p_intStartIndex)
				{
					//���������滻����󣬸��¿�ʼ�ͽ�������
					objContentInfo.m_intStartIndex += p_intDiffLength;
					objContentInfo.m_intEndIndex += p_intDiffLength;
				}				
			}

			clsUserContentInfo objPreContentInfo = null;
			for(int i=0;i<m_arlTextContentInfos.Count;i++)
			{
				clsUserContentInfo objContentInfo = 
					(clsUserContentInfo)m_arlTextContentInfos[i];

				if(objPreContentInfo != null
					&& objPreContentInfo.m_intEndIndex+1 == objContentInfo.m_intStartIndex
					&& objPreContentInfo.objUserInfo == objContentInfo.objUserInfo)
				{
					objPreContentInfo.m_intEndIndex = objContentInfo.m_intEndIndex;

					m_arlTextContentInfos.RemoveAt(i);

					i--;
				}
				else
				{
					objPreContentInfo = objContentInfo;
				}
			}

		}
		/// <summary>
		/// ����ɾ��
		/// </summary>
		/// <param name="p_intOldLen">ɾ���ĳ���</param>
		private void m_mthHandleDelete(int p_intOldLen)
		{
			/*
			 * �������³���Ϊ0�������滻
			 */

			int intStartIndex = m_intCurrentCursorIndex;

			if(m_blnIsBackspace)
			{
				intStartIndex = m_intCurrentCursorIndex - p_intOldLen;
			}

			int intEndIndex = intStartIndex+p_intOldLen-1;

			m_mthHandleReplace(intStartIndex,p_intOldLen,-1*p_intOldLen);

			m_intCurrentCursorIndex = this.SelectionStart;
		}
		#endregion

		#region ��¼��ǰ����λ�ú��û�������Ϣ���ж��Ƿ���Ҫ����
		private void m_mthUpdateCurrentStatus()
		{
			m_intCurrentCursorIndex = this.SelectionStart;

			if(!m_blnIsReadOnly)
			{
				ReadOnly = false;

				for(int i=0;i<m_arlDoubleStrikeThrough.Count/2;i++)
				{
					clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2];
					clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2+1];
                
					if(m_intCurrentCursorIndex > objStartInfo.m_intIndexRange
						&& m_intCurrentCursorIndex <= objEndInfo.m_intIndexRange)
					{
						ReadOnly = true;
						break;
					}
					else if(m_intCurrentCursorIndex < objStartInfo.m_intIndexRange)
					{
						break;
					}
				}
			}
		}		

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs p_objArg)
		{
			m_mthUpdateCurrentStatus();		
	
			m_blnCanSelectedChanged = true;
			m_blnIsSelectedChanged = this.SelectionLength != 0;

			base.OnMouseDown(p_objArg);
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			if(!m_blnIMEInput)
				m_mthUpdateCurrentStatus();

			base.OnKeyUp(e);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{	
			if(e.Control && e.Shift && e.KeyCode == Keys.Z)
			{
				m_mthRedo();
			}
			else if(e.Control && e.KeyCode == Keys.Z)
			{
				m_mthUndo();
			}

			if(e.KeyCode == Keys.ProcessKey)
			{
				m_blnIMEInput = true;
			}

			switch(e.KeyCode)
			{
				case Keys.Delete:
					m_blnIsBackspace = false;

					int intTempIndex = m_intCurrentCursorIndex+1;
					for(int i=0;i<m_arlTextContentInfos.Count;i++)
					{
						clsUserContentInfo objContentInfo = 
							(clsUserContentInfo)m_arlTextContentInfos[i];				

						if(objContentInfo.m_intEndIndex+1 < intTempIndex)
							continue;
						else
						{
							if(objContentInfo.m_intEndIndex+1 == intTempIndex)
							{
								if(objContentInfo.objUserInfo != m_objCurrentModifyUser)
									e.Handled = true;

								break;
							}
							else
							{
								if(intTempIndex != 0
									&& objContentInfo.objUserInfo != m_objCurrentModifyUser)
									e.Handled = true;

								break;
							}
						}
					}
					break;
				case Keys.Back:
					m_blnIsBackspace = true;
					for(int i=0;i<m_arlTextContentInfos.Count;i++)
					{
						clsUserContentInfo objContentInfo = 
							(clsUserContentInfo)m_arlTextContentInfos[i];				

						if(objContentInfo.m_intEndIndex+1 < m_intCurrentCursorIndex)
							continue;
						else
						{
							if(objContentInfo.m_intEndIndex+1 == m_intCurrentCursorIndex)
							{
								if(objContentInfo.objUserInfo != m_objCurrentModifyUser)
									e.Handled = true;

								break;
							}
							else
							{
								if(m_intCurrentCursorIndex != 0
									&& objContentInfo.objUserInfo != m_objCurrentModifyUser)
									e.Handled = true;

								break;
							}
						}
					}
					break;
				case Keys.Insert:
					e.Handled = true;
					break;
			}
		
			if(m_blnIsSelectedChanged)
			{		
				if(!m_blnIsReadOnly)
				{
					if(!m_blnCanModifySelection)
						ReadOnly = true;
					else
						ReadOnly = false;
				}

				switch(e.KeyCode)
				{
					case Keys.Home:
					case Keys.End:
					case Keys.Left:
					case Keys.Right:
					case Keys.Up:
					case Keys.Down:
					case Keys.PageUp:
					case Keys.PageDown:	
						m_blnCanSelectedChanged = true;
						break;
					default:
						m_blnCanSelectedChanged = false;
						break;
						
				}
			}

			base.OnKeyDown(e);
		}

		protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if(m_blnIsSelectedChanged)
			{
				if(!m_blnCanModifySelection)
				{
					e.Handled = true;
					return;
				}
				else
					m_blnCanSelectedChanged = false;
			}

//			this.SelectionColor = m_clrOldPartInsertText;
			base.OnKeyPress(e);
		}

		protected override void OnSelectionChanged(System.EventArgs e)
		{
			if(m_blnCanSelectedChanged)
			{
				m_intSelectedTextStartIndex = this.SelectionStart;
				m_intSelectedTextLength = this.SelectionLength;

				m_blnIsSelectedChanged = this.SelectionLength != 0;

				if(m_blnIsSelectedChanged)
				{
					int intEndSeletedIndex = m_intSelectedTextStartIndex+m_intSelectedTextLength-1;

					m_blnCanModifySelection = true;
					bool blnIsFirstGet = true;
					for(int i=0;i<m_arlTextContentInfos.Count;i++)
					{
						clsUserContentInfo objContentInfo = 
							(clsUserContentInfo)m_arlTextContentInfos[i];				

						if(blnIsFirstGet)
						{
							if(m_intSelectedTextStartIndex >= objContentInfo.m_intStartIndex
								&& m_intSelectedTextStartIndex <= objContentInfo.m_intEndIndex)
							{
								if(objContentInfo.objUserInfo != m_objCurrentModifyUser)
								{
									m_blnCanModifySelection = false;
									break;
								}

								if(intEndSeletedIndex >= objContentInfo.m_intStartIndex
									&& intEndSeletedIndex <= objContentInfo.m_intEndIndex)
									break;

								blnIsFirstGet = false;
							}
						}
						else
						{
							if(objContentInfo.objUserInfo != m_objCurrentModifyUser)
							{
								m_blnCanModifySelection = false;
								break;
							}

							if(intEndSeletedIndex >= objContentInfo.m_intStartIndex
								&& intEndSeletedIndex <= objContentInfo.m_intEndIndex)
								break;
						}
					}

				}
			}
		}
		#endregion

		#region ����XML
		/// <summary>
		/// �����µ��ı���Ϣ
		/// </summary>
		/// <param name="p_strText">������Ϣ</param>
		/// <param name="p_strXml">���ָ�ʽ��Xml��Ϣ</param>
		public void m_mthSetNewText(string p_strText,string p_strXml)
		{
			try
			{
				m_blnCanTextChanged = false;
				m_blnCanSelectedChanged = false;
				
				this.Text = p_strText;
				m_arlDoubleStrikeThrough.Clear();	
//				m_arlInsertInfo.Clear();
				
				m_arlModifyUsers.Clear();
				m_arlTextContentInfos.Clear();

				XmlTextReader objReader = new XmlTextReader(p_strXml,XmlNodeType.Element,m_objXmlParser);
				objReader.WhitespaceHandling = WhitespaceHandling.None;
			
				while(objReader.Read())
				{
					switch(objReader.NodeType)
					{
						case XmlNodeType.Element:
							switch(objReader.Name)
							{
								case "DI":
									Color clrDST = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
#if NewXml
									string strUserID = objReader.GetAttribute("D");
#else
									string strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
									string strUserName = objReader.GetAttribute("N");
									DateTime dtmDeleteTime = DateTime.Parse(objReader.GetAttribute("T"));
									int intUserSequence = int.Parse(objReader.GetAttribute("I"));

									clsDSTInfo objStartInfo = new clsDSTInfo();							
									objStartInfo.m_clrDST = clrDST;
									objStartInfo.m_strUserID = strUserID;
									objStartInfo.m_strUserName = strUserName;
									objStartInfo.m_intUserSequence = intUserSequence;
									objStartInfo.m_dtmDeleteTime = dtmDeleteTime;
									objStartInfo.m_intIndexRange = int.Parse(objReader.GetAttribute("S"));
									m_arlDoubleStrikeThrough.Add(objStartInfo);
									clsDSTInfo objEndInfo = new clsDSTInfo();							
									objEndInfo.m_clrDST = clrDST;
									objEndInfo.m_strUserID = strUserID;
									objEndInfo.m_strUserName = strUserName;
									objEndInfo.m_intUserSequence = intUserSequence;
									objEndInfo.m_dtmDeleteTime = dtmDeleteTime;
									objEndInfo.m_intIndexRange = int.Parse(objReader.GetAttribute("E"));
									m_arlDoubleStrikeThrough.Add(objEndInfo);
									break;
								case "UI":
									clsModifyUserInfo objUserInfo = new clsModifyUserInfo();
#if NewXml
									objUserInfo.m_strUserID = objReader.GetAttribute("D");
#else
									objUserInfo.m_strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
									objUserInfo.m_strUserName = objReader.GetAttribute("N");
									objUserInfo.m_intUserSequence = int.Parse(objReader.GetAttribute("S"));
									objUserInfo.m_dtmModifyDate = DateTime.Parse(objReader.GetAttribute("M"));
									if(objUserInfo.m_intUserSequence == 1)
									{
										//��һ�������ߵ���ɫʹ����ʾ��ɫ
										objUserInfo.m_clrText = m_clrDefaultViewText;
									}
									else
									{
										objUserInfo.m_clrText = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
									}

									m_arlModifyUsers.Add(objUserInfo);
									break;
								case "CI":
									clsUserContentInfo objContentInfo = new clsUserContentInfo();
									objContentInfo.m_intStartIndex = int.Parse(objReader.GetAttribute("S"));
									objContentInfo.m_intEndIndex = int.Parse(objReader.GetAttribute("E"));
									int intUserIndex = int.Parse(objReader.GetAttribute("Q"));
									objContentInfo.objUserInfo = (clsModifyUserInfo)m_arlModifyUsers[intUserIndex-1];

									m_arlTextContentInfos.Add(objContentInfo);
																		
									this.SelectionStart = objContentInfo.m_intStartIndex;
									this.SelectionLength = objContentInfo.m_intEndIndex-objContentInfo.m_intStartIndex+1;
									this.SelectionColor = objContentInfo.objUserInfo.m_clrText;
									break;
								case "SuperSubScript":
									clsSuperSubScript objScript = new clsSuperSubScript();
									objScript.m_intIndex = int.Parse(objReader.GetAttribute("Index"));
									objScript.m_intCharOffset = int.Parse(objReader.GetAttribute("CharOffset"));
									objScript.m_strValue = objReader.GetAttribute("Value");
									
									this.SelectionStart = objScript.m_intIndex;
									this.SelectionLength = objScript.m_strValue.Length;
									this.SelectionFont = new Font(this.Font.FontFamily,c_intSuperSubScriptSize);
									this.SelectionCharOffset = objScript.m_intCharOffset;
									break;
							}
							break;
					}
				}
			
				m_blnCanTextChanged = true;

				m_intPreviouslyLen = this.TextLength;

				m_intTextLenght = m_intPreviouslyLen;

				m_blnIsSelectedChanged = false;

				this.SelectionStart = 0;
				this.SelectionLength = 0;
				this.SelectionColor = this.m_clrOldPartInsertText;
				m_blnCanSelectedChanged = true;
								
				m_intCurrentCursorIndex = this.SelectionStart;

				m_blnNewEdit = false;

				clsDoItem objCurrentDo = s_objGetDoItemFromPool();
				clsRichTextItemStatus objStatus = (clsRichTextItemStatus)objCurrentDo.m_objGetDoStatus();

				objStatus.m_strRTF = this.Rtf;
				objStatus.m_intCurrentCursorIndex = this.m_intCurrentCursorIndex;
				m_mthCopyDST(m_arlDoubleStrikeThrough,objStatus.m_arlDoubleStrikeThrough,true);
				m_mthCopyTextContentInfo(m_arlTextContentInfos,objStatus.m_arlTextContentInfos,true);
			
				m_mthClearDoItems(objCurrentDo);
				
				this.Invalidate();
			}
			catch
			{
				//MessageBox.Show(ex.ToString());
				//�������ո�ʽ
				try
				{
					m_mthClearText();
					m_mthSetNewText(p_strText,"<r></r>");
				}
				catch(Exception)
				{
					m_mthClearText();
					this.Text=p_strText;
				}
			}
		}

		/// <summary>
		/// ��ȡXML�ı�
		/// </summary>
		/// <returns>XML�ı�</returns>
		public string m_strGetXmlText()
		{
			m_mthFinishEdit();

			m_objXmlStream.SetLength(0);

			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("r");

			m_objXmlWriter.WriteStartElement("D");
			for(int i=0;i<m_arlDoubleStrikeThrough.Count/2;i++)
			{
				clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2];
				clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2+1];
					
				m_objXmlWriter.WriteStartElement("DI");
				m_objXmlWriter.WriteAttributeString("S",objStartInfo.m_intIndexRange.ToString());
				m_objXmlWriter.WriteAttributeString("E",objEndInfo.m_intIndexRange.ToString());
				m_objXmlWriter.WriteAttributeString("C",objStartInfo.m_clrDST.ToArgb().ToString());
#if NewXml
				m_objXmlWriter.WriteAttributeString("D",objEndInfo.m_strUserID);
#else
				m_objXmlWriter.WriteAttributeString("D",int.Parse(objEndInfo.m_strUserID).ToString());
#endif
				m_objXmlWriter.WriteAttributeString("N",objStartInfo.m_strUserName);
				m_objXmlWriter.WriteAttributeString("I",objStartInfo.m_intUserSequence.ToString());
				m_objXmlWriter.WriteAttributeString("T",objStartInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteEndElement();
			}
			m_objXmlWriter.WriteEndElement();

			m_objXmlWriter.WriteStartElement("U");
			for(int i=0;i<m_arlModifyUsers.Count;i++)
			{
				clsModifyUserInfo objUserInfo = (clsModifyUserInfo)m_arlModifyUsers[i];

				m_objXmlWriter.WriteStartElement("UI");
#if NewXml
				m_objXmlWriter.WriteAttributeString("D",objUserInfo.m_strUserID);
#else
				m_objXmlWriter.WriteAttributeString("D",int.Parse(objUserInfo.m_strUserID).ToString());
#endif
				m_objXmlWriter.WriteAttributeString("N",objUserInfo.m_strUserName);
				m_objXmlWriter.WriteAttributeString("S",objUserInfo.m_intUserSequence.ToString());
				m_objXmlWriter.WriteAttributeString("M",objUserInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
				if(i == 0)
				{
					//��һ�������ߣ�ʹ��Ĭ����ɫ
					m_objXmlWriter.WriteAttributeString("C",s_clrBaseColor.ToArgb().ToString());
				}
				else
				{
					m_objXmlWriter.WriteAttributeString("C",objUserInfo.m_clrText.ToArgb().ToString());
				}
				m_objXmlWriter.WriteEndElement();
			}
			for(int i=0;i<m_arlTextContentInfos.Count;i++)
			{
				clsUserContentInfo objContentInfo = (clsUserContentInfo)m_arlTextContentInfos[i];

				m_objXmlWriter.WriteStartElement("CI");
				m_objXmlWriter.WriteAttributeString("S",objContentInfo.m_intStartIndex.ToString());
				if(objContentInfo.m_intEndIndex+1 <= this.Text.Length)
				{
					m_objXmlWriter.WriteAttributeString("E",objContentInfo.m_intEndIndex.ToString());
				}
				else
				{
					//HB Add:���ı�����пո��ʱ��"E"Ϊȥ���ո���������
					//��Ϊ��Oracle�±���ʱ��ȥ���ո񣬶��ؼ��ᱣ���ո��λ��������ֵ�ͻ����
					m_objXmlWriter.WriteAttributeString("E",Convert.ToString(this.Text.Length-1));
				}
				m_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				m_objXmlWriter.WriteEndElement();
			}
			m_objXmlWriter.WriteEndElement();

			#region ��ס���±�
//			bool blnThisFocus = this.Focused;
//			//��ʹ�Լ�ʧȥ���㣬��Ȼ��Ļ����������
//			Panel pnlTemp = new Panel();
//			pnlTemp.Size = new Size(0,0);
//			this.Controls.Add(pnlTemp);
//			pnlTemp.Focus();

			int intSelectionStart = this.SelectionStart;
			this.SelectionLength = 0;
			int intPreCharOffSet = -1;//�ϸ��ַ�ƫ����
			int intPreIndex = -1;
			for(int i = 1; i <= this.Text.Length; i++)
			{
				this.SelectionStart = i;
//				this.SelectionLength = 1;
				int intCharOffSet = this.SelectionCharOffset;

				//ͬһ�����±�
				if(intCharOffSet == intPreCharOffSet)
				{
					if(i < this.Text.Length)//��û�����һ���ַ�
					{
						continue;
					}
					else
					{
						if(intPreCharOffSet == c_intSuperScriptCharOffSet || intPreCharOffSet == c_intSubScriptCharOffSet)
						{
							int intIndex = intPreIndex - 1; 
							m_objXmlWriter.WriteStartElement("SuperSubScript");
							m_objXmlWriter.WriteAttributeString("Index",intIndex.ToString());
							m_objXmlWriter.WriteAttributeString("CharOffset",intPreCharOffSet.ToString());
							m_objXmlWriter.WriteAttributeString("Value",this.Text.Substring(intIndex,i - intIndex));
							m_objXmlWriter.WriteEndElement();
						}
					}
				}
				else
				{
					if(i < this.Text.Length)//��û�����һ���ַ�
					{
						if(intPreCharOffSet == c_intSuperScriptCharOffSet || intPreCharOffSet == c_intSubScriptCharOffSet)
						{
							int intIndex = intPreIndex - 1;
							m_objXmlWriter.WriteStartElement("SuperSubScript");
							m_objXmlWriter.WriteAttributeString("Index",intIndex.ToString());
							m_objXmlWriter.WriteAttributeString("CharOffset",intPreCharOffSet.ToString());
							m_objXmlWriter.WriteAttributeString("Value",this.Text.Substring(intIndex,i - intIndex - 1));
							m_objXmlWriter.WriteEndElement();
						}

						intPreCharOffSet = intCharOffSet;
						intPreIndex = i;
					}
					else
					{
						if(intCharOffSet == c_intSuperScriptCharOffSet || intCharOffSet == c_intSubScriptCharOffSet)
						{
							int intIndex = i - 1;
							m_objXmlWriter.WriteStartElement("SuperSubScript");
							m_objXmlWriter.WriteAttributeString("Index",intIndex.ToString());
							m_objXmlWriter.WriteAttributeString("CharOffset",intCharOffSet.ToString());
							m_objXmlWriter.WriteAttributeString("Value",this.Text[intIndex].ToString());
							m_objXmlWriter.WriteEndElement();
						}
					}
				}
			}//end for
//			this.Controls.Remove(pnlTemp);
//			this.SelectionStart = intSelectionStart;
//			this.SelectionLength = 0;
//			if(blnThisFocus)
//				this.Focus();
			#endregion

			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();

			m_objXmlWriter.Flush();

			return System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
		}
		#endregion

		#region Other
		public class clsModifyUserInfo
		{
			public string m_strUserID;

			public string m_strUserName;

			public Color m_clrText;	

			public DateTime m_dtmModifyDate;

			public int m_intUserSequence;

//			public string m_strContent;
		}

		public class clsUserContentInfo
		{
			public clsModifyUserInfo objUserInfo = null;

			public int m_intStartIndex;

			public int m_intEndIndex;
		}

		public class clsUserContentViewInfo
		{
			public clsUserContentInfo m_objUserContentInfo;

			public int m_intStartX;

			public int m_intEndX;			

			public int m_intUpY;

			public int m_intDownY;
		}

		public class clsDSTInfo
		{
			public int m_intIndexRange;

			public Color m_clrDST;	
		
			public string m_strUserID;

			public string m_strUserName;

			public int m_intUserSequence;

			public DateTime m_dtmDeleteTime;
		}

		private class clsDSTViewInfo
		{
			public int m_intDSTInfoStartIndex;

			public int m_intDSTInfoEndIndex;

			public int m_intStartX;

			public int m_intEndX;			

			public int m_intUpY;

			public int m_intDownY;
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			m_pntEndVisible.X = this.Width-5;
			m_pntEndVisible.Y = this.Height-5;

			base.OnSizeChanged(e);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if(m_intTextLenght == 0 || m_objCurrentModifyUser==null || m_objCurrentModifyUser.m_intUserSequence==1)
				return;	

			if(m_arlContentViewInfo.Count != 0 &&  (m_evtMouseEnterInsertText != null || m_evtMouseLeaveInsertText != null))
			{
				for(int i=0;i<m_arlContentViewInfo.Count;i++)
				{
					clsUserContentViewInfo objViewInfo = (clsUserContentViewInfo)m_arlContentViewInfo[i];

					if(e.X >= objViewInfo.m_intStartX && e.Y >= objViewInfo.m_intUpY
						&& e.X <= objViewInfo.m_intEndX && e.Y <= objViewInfo.m_intDownY)
					{
						if(m_intCurrentInsertIndex == i)
							break;

						if(m_intCurrentInsertIndex != -1 && m_evtMouseLeaveInsertText != null)
							m_evtMouseLeaveInsertText(this,m_objCurrentInsertEventArg);

						if(m_evtMouseEnterInsertText != null)
						{
							m_objCurrentInsertEventArg.m_clrInsert = objViewInfo.m_objUserContentInfo.objUserInfo.m_clrText;
							m_objCurrentInsertEventArg.m_dtmInsertTime = objViewInfo.m_objUserContentInfo.objUserInfo.m_dtmModifyDate;
							m_objCurrentInsertEventArg.m_strUserID = objViewInfo.m_objUserContentInfo.objUserInfo.m_strUserID;
							m_objCurrentInsertEventArg.m_strUserName = objViewInfo.m_objUserContentInfo.objUserInfo.m_strUserName;
							m_objCurrentInsertEventArg.m_intUserSeq = objViewInfo.m_objUserContentInfo.objUserInfo.m_intUserSequence;

							if(objViewInfo.m_objUserContentInfo.m_intStartIndex < 0 
								|| objViewInfo.m_objUserContentInfo.m_intStartIndex > this.Text.Length-1
								|| objViewInfo.m_objUserContentInfo.m_intEndIndex < 0 
								|| objViewInfo.m_objUserContentInfo.m_intEndIndex > this.Text.Length-1)
								break;

							m_objCurrentInsertEventArg.m_strInsertText = this.Text.Substring(objViewInfo.m_objUserContentInfo.m_intStartIndex,objViewInfo.m_objUserContentInfo.m_intEndIndex-objViewInfo.m_objUserContentInfo.m_intStartIndex+1);

							m_evtMouseEnterInsertText(this,m_objCurrentInsertEventArg);						
						}

						m_intCurrentInsertIndex = i;

						break;
					}
				}

				if(m_intCurrentInsertIndex != -1 && m_evtMouseLeaveInsertText != null)
					m_evtMouseLeaveInsertText(this,m_objCurrentInsertEventArg);
				
				m_intCurrentInsertIndex = -1;
			}

			if(m_arlDoubleStrikeThrough.Count!= 0 && (m_evtMouseEnterDeleteText != null || m_evtMouseLeaveDeleteText != null))
			{
				for(int i=0;i<m_arlDSTView.Count;i++)
				{
					clsDSTViewInfo objViewInfo = (clsDSTViewInfo)m_arlDSTView[i];

					if(e.X >= objViewInfo.m_intStartX && e.Y >= objViewInfo.m_intUpY
						&& e.X <= objViewInfo.m_intEndX && e.Y <= objViewInfo.m_intDownY)
					{
						if(m_intCurrentDSTIndex == objViewInfo.m_intDSTInfoStartIndex)
							return;

						if(m_intCurrentDSTIndex != -1 && m_evtMouseLeaveDeleteText != null)
							m_evtMouseLeaveDeleteText(this,m_objCurrentEventArg);

						if(m_evtMouseEnterDeleteText != null)
						{
							clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[objViewInfo.m_intDSTInfoStartIndex];
							clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[objViewInfo.m_intDSTInfoEndIndex];

							m_objCurrentEventArg.m_clrDST = objStartInfo.m_clrDST;
							m_objCurrentEventArg.m_dtmDeleteTime = objStartInfo.m_dtmDeleteTime;
							m_objCurrentEventArg.m_strUserID = objStartInfo.m_strUserID;
							m_objCurrentEventArg.m_strUserName = objStartInfo.m_strUserName;

							if(objStartInfo.m_intIndexRange < 0 
								|| objStartInfo.m_intIndexRange > this.Text.Length-1
								|| objEndInfo.m_intIndexRange < 0 
								|| objEndInfo.m_intIndexRange > this.Text.Length-1)
								break;

							m_objCurrentEventArg.m_strDeleteText = this.Text.Substring(objStartInfo.m_intIndexRange,objEndInfo.m_intIndexRange-objStartInfo.m_intIndexRange+1);

							m_evtMouseEnterDeleteText(this,m_objCurrentEventArg);						
						}

						m_intCurrentDSTIndex = i;

						return;
					}
				}
		
				if(m_intCurrentDSTIndex != -1 && m_evtMouseLeaveDeleteText != null)
					m_evtMouseLeaveDeleteText(this,m_objCurrentEventArg);
				
				m_intCurrentDSTIndex = -1;
			}

			base.OnMouseMove(e);
		}

		protected override void OnFontChanged(System.EventArgs e)
		{
			m_intFontHeight = this.Font.Height;

			base.OnFontChanged(e);
		}		
		#endregion

		/// <summary>
		/// ��ǽ����޸ģ���¼�޸�ʱ��
		/// </summary>
		public void m_mthFinishEdit()
		{
			if(!m_blnNewEdit)
				return;

			m_blnNewEdit = false;

			DateTime dtmFinishEdit = DateTime.Now;

			int intUserSequence = m_objCurrentModifyUser.m_intUserSequence;

			if(intUserSequence == -1)
			{
				intUserSequence = m_arlModifyUsers.Count+1;
			}

			for(int i=0;i<m_arlDoubleStrikeThrough.Count;i++)
			{
				clsDSTInfo objDSTInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i];

				if(objDSTInfo.m_dtmDeleteTime.Year == 1900)
				{
					objDSTInfo.m_dtmDeleteTime = dtmFinishEdit;
					objDSTInfo.m_intUserSequence = intUserSequence;
				}
			}

//			if(this.Text.Length > 0)
//			{
				if(m_objCurrentModifyUser.m_intUserSequence == -1)
				{
					m_objCurrentModifyUser.m_intUserSequence = intUserSequence;
					m_objCurrentModifyUser.m_dtmModifyDate = dtmFinishEdit;
					m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;

					m_arlModifyUsers.Add(m_objCurrentModifyUser);
				}
			
				if(this.Text.Length > 0 && m_arlTextContentInfos.Count == 0)
				{
					clsUserContentInfo objContentInfo = new clsUserContentInfo();
					
					objContentInfo.objUserInfo = m_objCurrentModifyUser;
					objContentInfo.m_intStartIndex = 0;
					objContentInfo.m_intEndIndex = this.Text.Length-1;

					m_arlTextContentInfos.Add(objContentInfo);
				}

//				m_objCurrentModifyUser = new clsModifyUserInfo();
//				m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;
//				m_objCurrentModifyUser.m_strUserID = m_strUserID;
//				m_objCurrentModifyUser.m_strUserName = m_strUserName;
//				m_objCurrentModifyUser.m_intUserSequence = -1;
//			}
		}

		/// <summary>
		/// ����ı�����
		/// </summary>
		public void m_mthClearText()
		{
			m_blnCanTextChanged = false;

			m_blnNewEdit = true;

			this.Text = "";

			m_arlDoubleStrikeThrough.Clear();	
			m_arlDSTView.Clear();
//			m_arlInsertInfo.Clear();
			m_arlInsertView.Clear();
			m_arlModifyUsers.Clear();
			m_arlTextContentInfos.Clear();

			m_intPreviouslyLen = 0;

			m_intTextLenght = m_intPreviouslyLen;

			m_blnIsSelectedChanged = false;

			m_blnCanSelectedChanged = false;
			this.SelectionStart = 0;
			this.SelectionLength = 0;
			this.SelectionColor = m_clrDefaultViewText;
			m_blnCanSelectedChanged = true;
								
			m_intCurrentCursorIndex = 0;

			m_blnCanTextChanged = true;

			m_objCurrentModifyUser = new clsModifyUserInfo();
			m_objCurrentModifyUser.m_clrText = m_clrOldPartInsertText;
			m_objCurrentModifyUser.m_strUserID = m_strUserID;
			m_objCurrentModifyUser.m_strUserName = m_strUserName;
			m_objCurrentModifyUser.m_intUserSequence = -1;

			m_mthClearDoItems();
		}
		/// <summary>
		/// ���ÿؼ�״̬ʱ�����ۼ�
		/// </summary>
		public void m_mthResetContextInfo()
		{
			m_mthClearText();

			m_objCurrentModifyUser.m_intUserSequence = 1;
			m_objCurrentModifyUser.m_dtmModifyDate = DateTime.Now;
			m_arlModifyUsers.Add(m_objCurrentModifyUser);//������������һ���б�����պ��ٱ���ʱ���ܱ����ۼ�

			m_objCurrentModifyUser.m_intUserSequence = -1;
			m_objCurrentModifyUser.m_dtmModifyDate = DateTime.MinValue;
		}

		/// <summary>
		/// ��ȡ��ȷ���ı�
		/// </summary>
		/// <returns></returns>
		public string m_strGetRightText()
		{
			m_mthFinishEdit();

			m_sbdTemp.Length = 0;

			int intStartIndex = 0;

			for(int i=0;i<m_arlDoubleStrikeThrough.Count/2;i++)
			{
				clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2];
				clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i*2+1];

				m_sbdTemp.Append(this.Text.Substring(intStartIndex,objStartInfo.m_intIndexRange-intStartIndex));

				intStartIndex += objEndInfo.m_intIndexRange - intStartIndex +1;
			}

			m_sbdTemp.Append(this.Text.Substring(intStartIndex,this.Text.Length-intStartIndex));

			return m_sbdTemp.ToString();
		}
        //���أ����ж�com.digitalwave.Utility.Controls.ctlRichTextBox�����Ƿ�ı�ʱ��
        public string m_strGetRightText(bool p_blnCheckStatus)
        {
            if(!p_blnCheckStatus)
                m_mthFinishEdit();

            m_sbdTemp.Length = 0;

            int intStartIndex = 0;

            for (int i = 0; i < m_arlDoubleStrikeThrough.Count / 2; i++)
            {
                clsDSTInfo objStartInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i * 2];
                clsDSTInfo objEndInfo = (clsDSTInfo)m_arlDoubleStrikeThrough[i * 2 + 1];

                m_sbdTemp.Append(this.Text.Substring(intStartIndex, objStartInfo.m_intIndexRange - intStartIndex));

                intStartIndex += objEndInfo.m_intIndexRange - intStartIndex + 1;
            }

            m_sbdTemp.Append(this.Text.Substring(intStartIndex, this.Text.Length - intStartIndex));

            return m_sbdTemp.ToString();
        }
		#region �ı��������
		/// <summary>
		/// ��ȡ��ȷ���ı�
		/// </summary>
		/// <returns></returns>
		public static string s_strGetRightText(string p_strText,string p_strXml)
		{
			s_sbdTemp.Length = 0;

			int intStartIndex = 0;

			try
			{				
				XmlTextReader objReader = new XmlTextReader(p_strXml,XmlNodeType.Element,s_objXmlParser);
				objReader.WhitespaceHandling = WhitespaceHandling.None;
			
				while(objReader.Read())
				{
					switch(objReader.NodeType)
					{
						case XmlNodeType.Element:
							switch(objReader.Name)
							{
								case "DI":
									int intStartDSTIndex = int.Parse(objReader.GetAttribute("S"));
									int intEndDSTIndex = int.Parse(objReader.GetAttribute("E"));

									s_sbdTemp.Append(p_strText.Substring(intStartIndex,intStartDSTIndex-intStartIndex));

									intStartIndex += intEndDSTIndex - intStartIndex +1;
									break;									
							}
							break;
					}
				}				
			}
			catch
			{
				return "";
			}

			s_sbdTemp.Append(p_strText.Substring(intStartIndex,p_strText.Length-intStartIndex));

			return s_sbdTemp.ToString();
		}

		private static Color s_clrBaseColor = Color.Black;

		public static Color s_ClrBaseColor
		{
			get
			{
				return s_clrBaseColor;
			}
			set
			{
				s_clrBaseColor = value;
			}
		}

		/// <summary>
		/// ��ȡ��ȷ���ı�
		/// </summary>
		/// <param name="p_strText"></param>
		/// <param name="p_strXml"></param>
		/// <param name="p_dtmSeparateTime"></param>
		/// <param name="p_objDSTIndexArr"></param>
		/// <param name="p_objInsertIndexArr"></param>
		/// <returns></returns>
		public static string s_strGetRightText(string p_strText,string p_strXml,DateTime p_dtmSeparateTime,out clsDSTInfo [] p_objDSTIndexArr,out clsUserContentInfo [] p_objContentInfoArr,out clsModifyUserInfo [] p_objModifyUserArr,out clsSuperSubScript[] p_objSuperSubScriptArr)
		{
			s_sbdTemp.Length = 0;
			s_arlDSTIndex.Clear();
			s_arlContentInfo.Clear();
			s_arlUserInfo.Clear();
			s_arlTempUserInfo.Clear();
			s_arlDeleteDSTInfo.Clear();
			s_arlContentTempInfo.Clear();
			s_arlSuperSubScriptInfo.Clear();

			int intStartIndex = 0;

			bool blnFirstDST = false;
			int intPreDSTLength = 0;

			clsUserContentInfo objBaseContent = null;
			
			p_objSuperSubScriptArr = null;

			int intUserCount = 0;

			try
			{				
				XmlTextReader objReader = new XmlTextReader(p_strXml,XmlNodeType.Element,s_objXmlParser);
				objReader.WhitespaceHandling = WhitespaceHandling.None;
			
				while(objReader.Read())
				{
					switch(objReader.NodeType)
					{
						case XmlNodeType.Element:
							switch(objReader.Name)
							{
								case "DI":
									Color clrDST = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
#if NewXml
									string strUserID = objReader.GetAttribute("D");
#else
									string strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
									string strUserName = objReader.GetAttribute("N");
									DateTime dtmDeleteTime = DateTime.Parse(objReader.GetAttribute("T"));
									int intUserSequence = int.Parse(objReader.GetAttribute("I"));

									int intStartDSTIndex = int.Parse(objReader.GetAttribute("S"));
									int intEndDSTIndex = int.Parse(objReader.GetAttribute("E"));
																		
									if(dtmDeleteTime <= p_dtmSeparateTime)
									{
										s_arlDeleteDSTInfo.Add(intStartDSTIndex-intPreDSTLength);
										s_arlDeleteDSTInfo.Add(intEndDSTIndex-intPreDSTLength);

										s_sbdTemp.Append(p_strText.Substring(intStartIndex,intStartDSTIndex-intStartIndex));

										intStartIndex += intEndDSTIndex - intStartIndex +1;

										intPreDSTLength += intEndDSTIndex - intStartDSTIndex +1;
									}
									else
									{
										if(blnFirstDST)
										{
											s_sbdTemp.Append(p_strText.Substring(intStartIndex));
											intStartIndex = p_strText.Length;
										}

										clsDSTInfo objStartInfo = new clsDSTInfo();							
										objStartInfo.m_clrDST = clrDST;
										objStartInfo.m_strUserID = strUserID;
										objStartInfo.m_strUserName = strUserName;
										objStartInfo.m_intUserSequence = intUserSequence;
										objStartInfo.m_dtmDeleteTime = dtmDeleteTime;
										objStartInfo.m_intIndexRange = intStartDSTIndex-intPreDSTLength;
									
										clsDSTInfo objEndInfo = new clsDSTInfo();							
										objEndInfo.m_clrDST = clrDST;
										objEndInfo.m_strUserID = strUserID;
										objEndInfo.m_strUserName = strUserName;
										objEndInfo.m_intUserSequence = intUserSequence;
										objEndInfo.m_dtmDeleteTime = dtmDeleteTime;
										objEndInfo.m_intIndexRange = intEndDSTIndex-intPreDSTLength;
									
										s_arlDSTIndex.Add(objStartInfo);
										s_arlDSTIndex.Add(objEndInfo);
									}
									break;	
								case "UI":
									clsModifyUserInfo objUserInfo = new clsModifyUserInfo();
#if NewXml
									objUserInfo.m_strUserID = objReader.GetAttribute("D");
#else
									objUserInfo.m_strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
									objUserInfo.m_strUserName = objReader.GetAttribute("N");
									objUserInfo.m_intUserSequence = int.Parse(objReader.GetAttribute("S"));
									objUserInfo.m_dtmModifyDate = DateTime.Parse(objReader.GetAttribute("M"));
									objUserInfo.m_clrText = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
//									objUserInfo.m_strContent = "";

									if(objUserInfo.m_intUserSequence == 1)
									{
										s_arlTempUserInfo.Add(objUserInfo);
										objUserInfo.m_clrText = s_clrBaseColor;
									}
									else if(objUserInfo.m_dtmModifyDate > p_dtmSeparateTime)
									{
										s_arlTempUserInfo.Add(objUserInfo);
									}

									s_arlUserInfo.Add(objUserInfo);
									break;
								case "CI":
									clsUserContentInfo objContentInfo = new clsUserContentInfo();
									objContentInfo.m_intStartIndex = int.Parse(objReader.GetAttribute("S"));
									objContentInfo.m_intEndIndex = int.Parse(objReader.GetAttribute("E"));
									int intUserIndex = int.Parse(objReader.GetAttribute("Q"));
//									objContentInfo.objUserInfo = (clsModifyUserInfo)s_arlUserInfo[intUserIndex-1];

									for(int i=0;i<s_arlTempUserInfo.Count;i++)
									{
										clsModifyUserInfo objCurrentUserInfo = (clsModifyUserInfo)s_arlTempUserInfo[i];

										//�պö���
										if(intUserIndex == objCurrentUserInfo.m_intUserSequence)
										{
											objContentInfo.objUserInfo = objCurrentUserInfo;
										}
										else if(intUserIndex > objCurrentUserInfo.m_intUserSequence)
										{
											//����һ��ҪС��˵�����ݵ��û������������û���Ϊ��ǰ�û�
											if(i+1 < s_arlTempUserInfo.Count && ((clsModifyUserInfo)s_arlTempUserInfo[i+1]).m_intUserSequence > intUserIndex)
											{
												objContentInfo.objUserInfo = (clsModifyUserInfo)s_arlTempUserInfo[i];
											}
										}
									}

									if(objContentInfo.objUserInfo == null && s_arlTempUserInfo.Count > 0)
									{
										objContentInfo.objUserInfo = (clsModifyUserInfo)s_arlTempUserInfo[s_arlTempUserInfo.Count-1];
									}
									else
									{
										objContentInfo.objUserInfo = new clsModifyUserInfo();
										objContentInfo.objUserInfo.m_clrText = s_clrBaseColor;
									}

									if(objContentInfo.objUserInfo.m_dtmModifyDate <= p_dtmSeparateTime)
									{
										if(objBaseContent == null)
										{
											s_arlContentInfo.Add(objContentInfo);
											objBaseContent = objContentInfo;
										}
										else
										{
											objBaseContent.m_intEndIndex = objContentInfo.m_intEndIndex;											
										}

//										objBaseContent.objUserInfo.m_strContent += p_strText.Substring(objContentInfo.m_intStartIndex,objContentInfo.m_intEndIndex-objContentInfo.m_intStartIndex+1);
									}
									else
									{
										s_arlContentInfo.Add(objContentInfo);

//										objContentInfo.objUserInfo.m_strContent += p_strText.Substring(objContentInfo.m_intStartIndex,objContentInfo.m_intEndIndex-objContentInfo.m_intStartIndex+1);

										objBaseContent = null;
									}
									break;
								case "SuperSubScript":
									clsSuperSubScript objScript = new clsSuperSubScript();
									objScript.m_intIndex = int.Parse(objReader.GetAttribute("Index"));
									objScript.m_intCharOffset = int.Parse(objReader.GetAttribute("CharOffset"));
									objScript.m_strValue = objReader.GetAttribute("Value");
									
									s_arlSuperSubScriptInfo.Add(objScript);
									break;
							}
							break;
					}
				}				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + ":\n" + ex.TargetSite.ReflectedType.Name+":\n"+ ex.TargetSite.Name+ex.StackTrace);
				
				p_objDSTIndexArr = new clsDSTInfo[0];
				p_objContentInfoArr = new clsUserContentInfo[1];
				p_objContentInfoArr[0] = new clsUserContentInfo();
				p_objContentInfoArr[0].m_intStartIndex = 0;
				p_objContentInfoArr[0].m_intEndIndex = p_strText.Length-1;
				p_objContentInfoArr[0].objUserInfo = new clsModifyUserInfo();
				p_objContentInfoArr[0].objUserInfo.m_clrText = s_clrBaseColor;
//				p_objContentInfoArr[0].objUserInfo.m_strContent = p_strText;
				p_objModifyUserArr = null;
				return "";
			}

			s_sbdTemp.Append(p_strText.Substring(intStartIndex,p_strText.Length-intStartIndex));

			p_objDSTIndexArr = (clsDSTInfo[])s_arlDSTIndex.ToArray(typeof(clsDSTInfo));
			p_objContentInfoArr = (clsUserContentInfo[])s_arlContentInfo.ToArray(typeof(clsUserContentInfo));
			
			if(p_objContentInfoArr.Length == 0)
			{
				p_objContentInfoArr = new clsUserContentInfo[1];
				p_objContentInfoArr[0] = new clsUserContentInfo();
				p_objContentInfoArr[0].m_intStartIndex = 0;
				p_objContentInfoArr[0].m_intEndIndex = p_strText.Length-1;
				p_objContentInfoArr[0].objUserInfo = new clsModifyUserInfo();
				p_objContentInfoArr[0].objUserInfo.m_clrText = s_clrBaseColor;
//				p_objContentInfoArr[0].objUserInfo.m_strContent = p_strText;
			}

			for(int i1=0;i1<s_arlDeleteDSTInfo.Count/2;i1++)
			{
				int intStartDeleteDST = (int)s_arlDeleteDSTInfo[i1*2];
				int intEndDeleteDST = (int)s_arlDeleteDSTInfo[i1*2+1];
				int intLenth = intEndDeleteDST-intStartDeleteDST+1;

				bool blnIsFirst = true;
				bool blnSkip = false;

				for(int j2=0;j2<p_objContentInfoArr.Length;j2++)
				{
					if(blnSkip)
					{
						p_objContentInfoArr[j2].m_intStartIndex -= intLenth;
						p_objContentInfoArr[j2].m_intEndIndex -= intLenth;

						s_arlContentTempInfo.Add(p_objContentInfoArr[j2]);

						continue;
					}

					if(p_objContentInfoArr[j2].m_intEndIndex < 
						intStartDeleteDST)
					{
						//����֮ǰ�������仯����
						s_arlContentTempInfo.Add(p_objContentInfoArr[j2]);
						continue;
					}

					if(blnIsFirst)
					{
						if(intStartDeleteDST > p_objContentInfoArr[j2].m_intStartIndex
							&& intStartDeleteDST <= p_objContentInfoArr[j2].m_intEndIndex)
						{
							//ǰ���߲���ǰ���غϲ��ڷ�Χ�ڣ������·�Χ��������ʹ�ÿ�������һ
							clsUserContentInfo objContentInfo = new clsUserContentInfo();
							objContentInfo.objUserInfo = p_objContentInfoArr[j2].objUserInfo;
							objContentInfo.m_intStartIndex = p_objContentInfoArr[j2].m_intStartIndex;
							objContentInfo.m_intEndIndex = intStartDeleteDST-1;

							s_arlContentTempInfo.Add(objContentInfo);

							blnIsFirst = false;

							j2--;
						}
						else if(intStartDeleteDST == p_objContentInfoArr[j2].m_intStartIndex)
						{
							//ǰ������ǰ���غϣ�ǰ������󿨳�֮������ݲ���Ҫ��
							blnIsFirst = false;

							j2--;
						}
					}
					else
					{
						if(p_objContentInfoArr[j2].m_intEndIndex < intEndDeleteDST)
						{
							//��Χ��ɾ�������ڣ�ȥ��
							continue;
						}

						if(intEndDeleteDST >= p_objContentInfoArr[j2].m_intStartIndex
							&& intEndDeleteDST < p_objContentInfoArr[j2].m_intEndIndex)
						{
							//�󿨳߲������غϲ��ڷ�Χ�ڣ���ʼ��ʹ�ÿ�������һ�����Ժ������λ

							if(s_arlContentTempInfo.Count > 0 )
							{
								clsUserContentInfo objContentInfo = (clsUserContentInfo)s_arlContentTempInfo[s_arlContentTempInfo.Count-1];

								if(objContentInfo.objUserInfo.Equals(p_objContentInfoArr[j2].objUserInfo))
								{
									p_objContentInfoArr[j2].m_intEndIndex -= intLenth;
									objContentInfo.m_intEndIndex = p_objContentInfoArr[j2].m_intEndIndex;

									goto Skip;
								}
							}
							p_objContentInfoArr[j2].m_intStartIndex = intEndDeleteDST+1-intLenth;
							p_objContentInfoArr[j2].m_intEndIndex -= intLenth;

							s_arlContentTempInfo.Add(p_objContentInfoArr[j2]);

						Skip:
							blnSkip = true;
						}
						else if(intEndDeleteDST == p_objContentInfoArr[j2].m_intEndIndex)
						{
							//�󿨳������غ�
							blnSkip = true;
						}
					}
				}

				p_objContentInfoArr = (clsUserContentInfo[])s_arlContentTempInfo.ToArray(typeof(clsUserContentInfo));
				s_arlContentTempInfo.Clear();
			}
			
			for(int i=0;i<s_arlTempUserInfo.Count;i++)
			{
				((clsModifyUserInfo)s_arlTempUserInfo[i]).m_intUserSequence = i+1;
			}
			p_objModifyUserArr = (clsModifyUserInfo[])s_arlTempUserInfo.ToArray(typeof(clsModifyUserInfo));

			p_objSuperSubScriptArr = (clsSuperSubScript[])s_arlSuperSubScriptInfo.ToArray(typeof(clsSuperSubScript));

			return s_sbdTemp.ToString();
		}

		/// <summary>
		/// ��ȡ��ȷ���ı�
		/// </summary>
		/// <param name="p_strText">ԭ�ı�</param>
		/// <param name="p_strXml">ԭXml</param>
		/// <param name="p_dtmSeparateTime">�ָ��ʱ��</param>
		/// <param name="p_strNewXml">�µ�Xml</param>
		/// <returns></returns>
		public static string s_strGetRightText(string p_strText,string p_strXml,DateTime p_dtmSeparateTime,out string p_strNewXml)
		{
			clsDSTInfo [] objDSTIndexArr;
			clsUserContentInfo [] objContentInfoArr;
			clsModifyUserInfo [] objModifyUserArr;	
			clsSuperSubScript[] objSuperSubScriptArr;

			string strNewText = s_strGetRightText(p_strText,p_strXml,p_dtmSeparateTime,out objDSTIndexArr,out objContentInfoArr,out objModifyUserArr,out objSuperSubScriptArr);

			if(strNewText == "")
			{
				p_strNewXml = "";
				return strNewText;
			}

			clsXMLInfos objXmlInfo = new clsXMLInfos();
			objXmlInfo.m_intTextLenght = strNewText.Length;
			objXmlInfo.m_objCIArr = objContentInfoArr;
			objXmlInfo.m_objDSTArr = objDSTIndexArr;
			objXmlInfo.m_objUserInfo = objModifyUserArr;

			p_strNewXml = m_strGetSubXml(objXmlInfo,0,strNewText.Length-1);

			return strNewText;
		}
		#endregion				

		public int m_intGetModifyUserCount()
		{
			return m_arlModifyUsers.Count;
		}

		public clsModifyUserInfo [] m_objGetModifyUserArr()
		{
			return (clsModifyUserInfo[])m_arlModifyUsers.ToArray(typeof(clsModifyUserInfo));
		}

		public bool m_BlnReadOnly
		{
			get
			{
				return ReadOnly;
			}
			set
			{
				m_blnIsReadOnly = value;
				ReadOnly = value;
			}
		}			

		#region Xml �ϲ��Ͳ��
		/// <summary>
		/// ����XML���ڴ�
		/// </summary>
		private static MemoryStream s_objXmlStream = new MemoryStream();

		/// <summary>
		/// ����XML�Ĺ���
		/// </summary>
		private static XmlTextWriter s_objXmlWriter = new XmlTextWriter(s_objXmlStream,System.Text.Encoding.Unicode);
		
		/// <summary>
		/// ��ʱ����ı��ı���
		/// </summary>
		private static ArrayList s_arlStrigTemp = new ArrayList();

		/// <summary>
		/// ��Ϣ
		/// </summary>
		internal class clsXMLInfos
		{
			/// <summary>
			/// ˫����
			/// </summary>
			public clsDSTInfo[] m_objDSTArr;

			/// <summary>
			/// ÿ�����ݵ���Ϣ
			/// </summary>
			public clsUserContentInfo[] m_objCIArr;

			/// <summary>
			/// �û���Ϣ����ʱ������
			/// </summary>
			public clsModifyUserInfo[] m_objUserInfo;

			/// <summary>
			/// ���±���Ϣ
			/// </summary>
			public clsSuperSubScript[] m_objScripts;

			public int m_intTextLenght;
		}
		/// <summary>
		/// ��XML��ȡ��Ϣ����
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		private static clsXMLInfos s_objGetXMLInfo(string p_strXml)
		{
			s_arlDSTIndex.Clear();
			s_arlContentInfo.Clear();
			s_arlUserInfo.Clear();
			s_arlSuperSubScriptInfo.Clear();
			int intTextLength = 0;

			try
			{				
				XmlTextReader objReader = new XmlTextReader(p_strXml,XmlNodeType.Element,s_objXmlParser);
				objReader.WhitespaceHandling = WhitespaceHandling.None;
			
				while(objReader.Read())
				{
					switch(objReader.NodeType)
					{
						case XmlNodeType.Element:
						switch(objReader.Name)
						{
							case "DI":
								Color clrDST = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
#if NewXml
								string strUserID = objReader.GetAttribute("D");
#else
								string strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
								string strUserName = objReader.GetAttribute("N");
								DateTime dtmDeleteTime = DateTime.Parse(objReader.GetAttribute("T"));
								int intUserSequence = int.Parse(objReader.GetAttribute("I"));

								int intStartDSTIndex = int.Parse(objReader.GetAttribute("S"));
								int intEndDSTIndex = int.Parse(objReader.GetAttribute("E"));
																		
								clsDSTInfo objStartInfo = new clsDSTInfo();							
								objStartInfo.m_clrDST = clrDST;
								objStartInfo.m_strUserID = strUserID;
								objStartInfo.m_strUserName = strUserName;
								objStartInfo.m_intUserSequence = intUserSequence;
								objStartInfo.m_dtmDeleteTime = dtmDeleteTime;
								objStartInfo.m_intIndexRange = intStartDSTIndex;
										
								clsDSTInfo objEndInfo = new clsDSTInfo();							
								objEndInfo.m_clrDST = clrDST;
								objEndInfo.m_strUserID = strUserID;
								objEndInfo.m_strUserName = strUserName;
								objEndInfo.m_intUserSequence = intUserSequence;
								objEndInfo.m_dtmDeleteTime = dtmDeleteTime;
								objEndInfo.m_intIndexRange = intEndDSTIndex;
									
								s_arlDSTIndex.Add(objStartInfo);
								s_arlDSTIndex.Add(objEndInfo);
								break;	
							case "UI":
								clsModifyUserInfo objUserInfo = new clsModifyUserInfo();
#if NewXml
								objUserInfo.m_strUserID = objReader.GetAttribute("D");
#else
								objUserInfo.m_strUserID = int.Parse(objReader.GetAttribute("D")).ToString("0000000");
#endif
								objUserInfo.m_strUserName = objReader.GetAttribute("N");
								objUserInfo.m_intUserSequence = int.Parse(objReader.GetAttribute("S"));
								objUserInfo.m_dtmModifyDate = DateTime.Parse(objReader.GetAttribute("M"));
								objUserInfo.m_clrText = Color.FromArgb(int.Parse(objReader.GetAttribute("C")));
//								objUserInfo.m_strContent = "";

								s_arlUserInfo.Add(objUserInfo);
								break;
							case "CI":
								clsUserContentInfo objContentInfo = new clsUserContentInfo();
								objContentInfo.m_intStartIndex = int.Parse(objReader.GetAttribute("S"));
								objContentInfo.m_intEndIndex = int.Parse(objReader.GetAttribute("E"));
								int intUserIndex = int.Parse(objReader.GetAttribute("Q"));
								objContentInfo.objUserInfo = (clsModifyUserInfo)s_arlUserInfo[intUserIndex-1];

								s_arlContentInfo.Add(objContentInfo);

								intTextLength = objContentInfo.m_intEndIndex+1;
								break;
							case "SuperSubScript":
								clsSuperSubScript objScript = new clsSuperSubScript();
								objScript.m_intIndex = int.Parse(objReader.GetAttribute("Index"));
								objScript.m_intCharOffset = int.Parse(objReader.GetAttribute("CharOffset"));
								objScript.m_strValue = objReader.GetAttribute("Value");
									
								s_arlSuperSubScriptInfo.Add(objScript);
								break;
						}
							break;
					}
				}				
			}
			catch
			{
				return null;
			}

			clsXMLInfos objInfo = new clsXMLInfos();
			objInfo.m_objDSTArr = (clsDSTInfo[])s_arlDSTIndex.ToArray(typeof(clsDSTInfo));
			objInfo.m_objUserInfo = (clsModifyUserInfo[])s_arlUserInfo.ToArray(typeof(clsModifyUserInfo));
			objInfo.m_objCIArr = (clsUserContentInfo[])s_arlContentInfo.ToArray(typeof(clsUserContentInfo));
			if(s_arlSuperSubScriptInfo.Count > 0)
				objInfo.m_objScripts = (clsSuperSubScript[])s_arlSuperSubScriptInfo.ToArray(typeof(clsSuperSubScript));
			objInfo.m_intTextLenght = intTextLength;

			return objInfo;
		}
		/// <summary>
		/// �����Ϣ����
		/// </summary>
		private static ArrayList s_arlXMLInfos = new ArrayList();
		/// <summary>
		/// �ϲ�XML
		/// </summary>
		/// <param name="p_strXmlArr">Xml����</param>
		/// <returns></returns>
		public static string s_strCombineXml(string[] p_strXmlArr)
		{
			s_arlXMLInfos.Clear();
			
			if(p_strXmlArr == null)
				return "";

			for(int i=0;i<p_strXmlArr.Length;i++)
			{
				clsXMLInfos objInfos = s_objGetXMLInfo(p_strXmlArr[i]);
				
				if(objInfos == null)
					return "";

				s_arlXMLInfos.Add(objInfos);			
			}			

			if(s_arlXMLInfos.Count <= 0)
				return "";

			s_arlDeleteDSTInfo.Clear();
			s_arlDSTIndex.Clear();
			s_arlUserInfo.Clear();
			s_arlContentInfo.Clear();
			s_arlSuperSubScriptInfo.Clear();
			
			for(int i=0;i<s_arlXMLInfos.Count;i++)
			{
				clsXMLInfos objInfos = (clsXMLInfos)s_arlXMLInfos[i];

				for(int j2=0;j2<objInfos.m_objUserInfo.Length;j2++)
				{
					if(objInfos.m_objUserInfo[j2].m_dtmModifyDate != DateTime.MinValue)
					{
						s_arlUserInfo.Add(objInfos.m_objUserInfo[j2]);
						break;
					}
				}

				if(s_arlUserInfo.Count == 1)
					break;
			}	
		
			if(s_arlUserInfo.Count == 0)
			{
				clsXMLInfos objInfos = (clsXMLInfos)s_arlXMLInfos[0];

				objInfos.m_objUserInfo[0].m_dtmModifyDate = DateTime.Now;
				s_arlUserInfo.Add(objInfos.m_objUserInfo[0]);
			}

			for(int i=0;i<s_arlXMLInfos.Count;i++)
			{
				clsXMLInfos objXmlInfos = (clsXMLInfos)s_arlXMLInfos[i];
				
				for(int j2=0;j2<objXmlInfos.m_objUserInfo.Length;j2++)
				{
					if(objXmlInfos.m_objUserInfo[j2].m_dtmModifyDate == DateTime.MinValue)
						continue;

					for(int k3=0;k3<s_arlUserInfo.Count;k3++)
					{
						clsModifyUserInfo objUserInfo = (clsModifyUserInfo)s_arlUserInfo[k3];

						if(objXmlInfos.m_objUserInfo[j2].m_dtmModifyDate < objUserInfo.m_dtmModifyDate)
						{
							s_arlUserInfo.Insert(k3,objXmlInfos.m_objUserInfo[j2]);
							break;
						}
						else if(objXmlInfos.m_objUserInfo[j2].m_dtmModifyDate == objUserInfo.m_dtmModifyDate)
						{
							break;
						}
						else if(k3+1 == s_arlUserInfo.Count)
						{
							s_arlUserInfo.Add(objXmlInfos.m_objUserInfo[j2]);
							break;
						}
					}
				}
			}

			int intLenght = 0;
			DateTime dtmOpenDate = ((clsModifyUserInfo)s_arlUserInfo[0]).m_dtmModifyDate;
			for(int i=0;i<s_arlXMLInfos.Count;i++)
			{
				clsXMLInfos objXmlInfos = (clsXMLInfos)s_arlXMLInfos[i];
				
				for(int j2=0;j2<objXmlInfos.m_objDSTArr.Length;j2++)
				{
					clsDSTInfo objDSTInfo = (clsDSTInfo)objXmlInfos.m_objDSTArr[j2];

					objDSTInfo.m_intIndexRange += intLenght;

					if(objDSTInfo.m_dtmDeleteTime == DateTime.MinValue)
						objDSTInfo.m_dtmDeleteTime = dtmOpenDate;

					s_arlDSTIndex.Add(objDSTInfo);
				}

				for(int j2=0;j2<objXmlInfos.m_objCIArr.Length;j2++)
				{
					objXmlInfos.m_objCIArr[j2].m_intStartIndex += intLenght;
					objXmlInfos.m_objCIArr[j2].m_intEndIndex += intLenght;

					if(objXmlInfos.m_objCIArr[j2].objUserInfo.m_dtmModifyDate == DateTime.MinValue)
						objXmlInfos.m_objCIArr[j2].objUserInfo = (clsModifyUserInfo)s_arlUserInfo[0];
					else
					{
						for(int k3=0;k3<s_arlUserInfo.Count;k3++)
						{
							clsModifyUserInfo objUserInfo = (clsModifyUserInfo)s_arlUserInfo[k3];
						
							if(objUserInfo.m_strUserID == objXmlInfos.m_objCIArr[j2].objUserInfo.m_strUserID
								&& objUserInfo.m_dtmModifyDate == objXmlInfos.m_objCIArr[j2].objUserInfo.m_dtmModifyDate)
							{
								objXmlInfos.m_objCIArr[j2].objUserInfo = objUserInfo;
								break;
							}
						}
					}

					if(s_arlContentInfo.Count > 0)
					{
						clsUserContentInfo objPreContentInfo = (clsUserContentInfo)s_arlContentInfo[s_arlContentInfo.Count-1];

						if(objPreContentInfo.objUserInfo == objXmlInfos.m_objCIArr[j2].objUserInfo)
						{
							objPreContentInfo.m_intEndIndex = objXmlInfos.m_objCIArr[j2].m_intEndIndex;
						}
						else
							s_arlContentInfo.Add(objXmlInfos.m_objCIArr[j2]);
					}
					else
						s_arlContentInfo.Add(objXmlInfos.m_objCIArr[j2]);
				}

				#region �������±�
				if(objXmlInfos.m_objScripts != null && objXmlInfos.m_objScripts.Length > 0)
				{
					for(int j2=0;j2<objXmlInfos.m_objScripts.Length;j2++)
					{
						objXmlInfos.m_objScripts[j2].m_intIndex += intLenght;
						s_arlSuperSubScriptInfo.Add(objXmlInfos.m_objScripts[j2]);
					}
				}

				#endregion

				intLenght += objXmlInfos.m_intTextLenght;
			}

			s_objXmlStream.Flush();
			s_objXmlStream.SetLength(0);

			s_objXmlWriter.WriteStartDocument();
			s_objXmlWriter.WriteStartElement("r");

			s_objXmlWriter.WriteStartElement("D");
			for(int i=0;i<s_arlDSTIndex.Count/2;i++)
			{
				clsDSTInfo objStartInfo = (clsDSTInfo)s_arlDSTIndex[i*2];
				clsDSTInfo objEndInfo = (clsDSTInfo)s_arlDSTIndex[i*2+1];
					
				s_objXmlWriter.WriteStartElement("DI");
				s_objXmlWriter.WriteAttributeString("S",objStartInfo.m_intIndexRange.ToString());
				s_objXmlWriter.WriteAttributeString("E",objEndInfo.m_intIndexRange.ToString());
				s_objXmlWriter.WriteAttributeString("C",objStartInfo.m_clrDST.ToArgb().ToString());
#if NewXml
				s_objXmlWriter.WriteAttributeString("D",objEndInfo.m_strUserID);
#else
				s_objXmlWriter.WriteAttributeString("D",int.Parse(objEndInfo.m_strUserID).ToString());
#endif
				s_objXmlWriter.WriteAttributeString("N",objStartInfo.m_strUserName);
				s_objXmlWriter.WriteAttributeString("I",objStartInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteAttributeString("T",objStartInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
				s_objXmlWriter.WriteEndElement();
			}
			s_objXmlWriter.WriteEndElement();

			s_objXmlWriter.WriteStartElement("U");
			for(int i=0;i<s_arlUserInfo.Count;i++)
			{
				clsModifyUserInfo objUserInfo = (clsModifyUserInfo)s_arlUserInfo[i];
				objUserInfo.m_intUserSequence = i+1;

				s_objXmlWriter.WriteStartElement("UI");
#if NewXml
				s_objXmlWriter.WriteAttributeString("D",objUserInfo.m_strUserID);
#else
				s_objXmlWriter.WriteAttributeString("D",int.Parse(objUserInfo.m_strUserID).ToString());
#endif
				s_objXmlWriter.WriteAttributeString("N",objUserInfo.m_strUserName);
				s_objXmlWriter.WriteAttributeString("S",objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteAttributeString("M",objUserInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
				s_objXmlWriter.WriteAttributeString("C",objUserInfo.m_clrText.ToArgb().ToString());
				s_objXmlWriter.WriteEndElement();
			}
			for(int i=0;i<s_arlContentInfo.Count;i++)
			{
				clsUserContentInfo objContentInfo = (clsUserContentInfo)s_arlContentInfo[i];

				s_objXmlWriter.WriteStartElement("CI");
				s_objXmlWriter.WriteAttributeString("S",objContentInfo.m_intStartIndex.ToString());
				s_objXmlWriter.WriteAttributeString("E",objContentInfo.m_intEndIndex.ToString());//����ϲ�XML���ӡ�����޸Ĵ˴�
				s_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteEndElement();
			}
			s_objXmlWriter.WriteEndElement();

			#region �������±�
			for(int i=0;i<s_arlSuperSubScriptInfo.Count;i++)
			{
				clsSuperSubScript objScript = (clsSuperSubScript)s_arlSuperSubScriptInfo[i];
				s_objXmlWriter.WriteStartElement("SuperSubScript");
				s_objXmlWriter.WriteAttributeString("Index",objScript.m_intIndex.ToString());
				s_objXmlWriter.WriteAttributeString("CharOffset",objScript.m_intCharOffset.ToString());
				s_objXmlWriter.WriteAttributeString("Value",objScript.m_strValue);
				s_objXmlWriter.WriteEndElement();
			}
			#endregion

			s_objXmlWriter.WriteEndElement();
			s_objXmlWriter.WriteEndDocument();

			s_objXmlWriter.Flush();

			return System.Text.Encoding.Unicode.GetString(s_objXmlStream.ToArray(),39*2,(int)s_objXmlStream.Length-39*2);
		}

		/// <summary>
		/// ���
		/// </summary>
		/// <param name="p_strText">�ı�</param>
		/// <param name="p_strXml">Xml</param>
		/// <param name="p_intCharPerLine">һ���ж��ٸ��ַ�</param>
		/// <param name="p_strTextArr">��ֺ���ı�</param>
		/// <param name="p_strXmlArr">Xml</param>
		public static void m_mthSplitXml(string p_strText,string p_strXml,int p_intCharPerLine,out string [] p_strTextArr,out string [] p_strXmlArr)
		{
			p_strTextArr = null;
			p_strXmlArr = null;

			if(p_strText == null || p_strText == ""
				|| p_strXml == null || p_strXml == ""
				|| p_intCharPerLine <= 0)
			{
				p_strTextArr = new string[]{p_strText};
				p_strXmlArr = new string[]{p_strXml};
				return ;			
			}

			clsXMLInfos objInfo = s_objGetXMLInfo(p_strXml);

			int intLen = p_strText.Length;
			p_strTextArr = s_strSplitArr(p_strText,p_intCharPerLine,'\n');
			p_strXmlArr = new String[p_strTextArr.Length];			

			int intStartIndex = -1;
			int intEndIndex = -1;

			for(int i=0;i<p_strTextArr.Length;i++)
			{
				intStartIndex = intEndIndex+1;
				intEndIndex += p_strTextArr[i].Length;

				p_strXmlArr[i] = m_strGetSubXml(objInfo,intStartIndex,intEndIndex);
			}
		}

		private static string [] s_strSplitArr(string p_strText,int p_intCharCount,char p_chrSplit)
		{
			s_arlStrigTemp.Clear();

			int intStartIndex = 0;
			int intEndIndex = 0;

			while(intEndIndex < p_strText.Length)
			{
				if(p_strText[intEndIndex] == p_chrSplit
					|| intEndIndex == p_strText.Length-1
					|| intEndIndex-intStartIndex+1 == p_intCharCount)
				{
					s_arlStrigTemp.Add(p_strText.Substring(intStartIndex,intEndIndex-intStartIndex+1));

					intStartIndex = intEndIndex+1;
				}

				intEndIndex++;
			}

			return (string[])s_arlStrigTemp.ToArray(typeof(string));
		}

		private static string m_strGetSubXml(clsXMLInfos p_objInfo,int p_intStartIndex,int p_intEndIndex)
		{
			int intOldStartIndex;
			int intOldEndIndex;

			s_objXmlStream.Flush();
			s_objXmlStream.SetLength(0);

			s_objXmlWriter.WriteStartDocument();
			s_objXmlWriter.WriteStartElement("r");

			s_objXmlWriter.WriteStartElement("D");
			bool blnIsStart = true;
			for(int i=0;i<p_objInfo.m_objDSTArr.Length/2;i++)
			{
				clsDSTInfo objStartInfo = p_objInfo.m_objDSTArr[i*2];
				clsDSTInfo objEndInfo = p_objInfo.m_objDSTArr[i*2+1];

				intOldStartIndex = objStartInfo.m_intIndexRange;
				intOldEndIndex = objEndInfo.m_intIndexRange;

				if(blnIsStart)
				{
					
					if(!((objStartInfo.m_intIndexRange <= p_intStartIndex
						&& objEndInfo.m_intIndexRange >= p_intStartIndex)
						|| (objStartInfo.m_intIndexRange <= p_intEndIndex
						&& objEndInfo.m_intIndexRange >= p_intEndIndex)
						|| (p_intStartIndex <= objStartInfo.m_intIndexRange
						&& p_intEndIndex >= objStartInfo.m_intIndexRange)
						|| (p_intStartIndex <= objEndInfo.m_intIndexRange
						&& p_intEndIndex >= objEndInfo.m_intIndexRange)))
						continue;					

					//�����ʼλС��p_intStartIndex���޸Ŀ�ʼλΪp_intStartIndex
					if(objStartInfo.m_intIndexRange < p_intStartIndex)
						objStartInfo.m_intIndexRange = p_intStartIndex;

					blnIsStart = false;

					//�������λ����p_intEndIndex���޸Ľ���λΪp_intEndIndex
					if(objEndInfo.m_intIndexRange > p_intEndIndex)
					{
						objEndInfo.m_intIndexRange = p_intEndIndex;
						blnIsStart = true;
					}
				}
				else
				{
					//ֻ�п�ʼλС�ڵ���p_intEndIndex�ż�������������ѭ��
					if(objStartInfo.m_intIndexRange > p_intEndIndex)
						break;

					//�������λ����p_intEndIndex���޸Ľ���λΪp_intEndIndex
					if(objEndInfo.m_intIndexRange > p_intEndIndex)
						objEndInfo.m_intIndexRange = p_intEndIndex;
				}
					
				s_objXmlWriter.WriteStartElement("DI");
				s_objXmlWriter.WriteAttributeString("S",(objStartInfo.m_intIndexRange-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("E",(objEndInfo.m_intIndexRange-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("C",objStartInfo.m_clrDST.ToArgb().ToString());
#if NewXml
				s_objXmlWriter.WriteAttributeString("D",objEndInfo.m_strUserID);
#else
				s_objXmlWriter.WriteAttributeString("D",int.Parse(objEndInfo.m_strUserID).ToString());
#endif
				s_objXmlWriter.WriteAttributeString("N",objStartInfo.m_strUserName);
				s_objXmlWriter.WriteAttributeString("I",objStartInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteAttributeString("T",objStartInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
				s_objXmlWriter.WriteEndElement();

				//��ԭ��ʼλ�ͽ���λ
				objStartInfo.m_intIndexRange = intOldStartIndex;
				objEndInfo.m_intIndexRange = intOldEndIndex;
			}
			s_objXmlWriter.WriteEndElement();

			s_objXmlWriter.WriteStartElement("U");
			for(int i=0;i<p_objInfo.m_objUserInfo.Length;i++)
			{
				clsModifyUserInfo objUserInfo = p_objInfo.m_objUserInfo[i];
				
				s_objXmlWriter.WriteStartElement("UI");
#if NewXml
				s_objXmlWriter.WriteAttributeString("D",objUserInfo.m_strUserID);
#else
				s_objXmlWriter.WriteAttributeString("D",int.Parse(objUserInfo.m_strUserID).ToString());
#endif
				s_objXmlWriter.WriteAttributeString("N",objUserInfo.m_strUserName);
				s_objXmlWriter.WriteAttributeString("S",objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteAttributeString("M",objUserInfo.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
				s_objXmlWriter.WriteAttributeString("C",objUserInfo.m_clrText.ToArgb().ToString());
				s_objXmlWriter.WriteEndElement();
			}

			blnIsStart = true;
			for(int i=0;i<p_objInfo.m_objCIArr.Length;i++)
			{
				clsUserContentInfo objContentInfo = p_objInfo.m_objCIArr[i];

				intOldStartIndex = objContentInfo.m_intStartIndex;
				intOldEndIndex = objContentInfo.m_intEndIndex;

				if(blnIsStart)
				{
					//ֻ�н���λ���ڵ���p_intStartIndex�ſ�ʼ
					if(objContentInfo.m_intEndIndex < p_intStartIndex)
						continue;

					//�����ʼλС��p_intStartIndex���޸Ŀ�ʼλΪp_intStartIndex
					if(objContentInfo.m_intStartIndex < p_intStartIndex)
						objContentInfo.m_intStartIndex = p_intStartIndex;

					//�������λ����p_intEndIndex���޸Ľ���λΪp_intEndIndex
					if(objContentInfo.m_intEndIndex > p_intEndIndex)
						objContentInfo.m_intEndIndex = p_intEndIndex;

					blnIsStart = false;
				}
				else
				{
					//ֻ�п�ʼλС�ڵ���p_intEndIndex�ż�������������ѭ��
					if(objContentInfo.m_intStartIndex > p_intEndIndex)
						break;

					//�������λ����p_intEndIndex���޸Ľ���λΪp_intEndIndex
					if(objContentInfo.m_intEndIndex > p_intEndIndex)
						objContentInfo.m_intEndIndex = p_intEndIndex;
				}

				s_objXmlWriter.WriteStartElement("CI");
				s_objXmlWriter.WriteAttributeString("S",(objContentInfo.m_intStartIndex-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("E",(objContentInfo.m_intEndIndex-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteEndElement();

				//��ԭ��ʼλ�ͽ���λ
				objContentInfo.m_intStartIndex = intOldStartIndex;
				objContentInfo.m_intEndIndex = intOldEndIndex;
			}
			s_objXmlWriter.WriteEndElement();

			s_objXmlWriter.WriteEndElement();
			s_objXmlWriter.WriteEndDocument();

			s_objXmlWriter.Flush();

			return System.Text.Encoding.Unicode.GetString(s_objXmlStream.ToArray(),39*2,(int)s_objXmlStream.Length-39*2);
		}

		/// <summary>
		/// ���XML���ݵ���Ϣ
		/// </summary>
		public class clsUserXMLInfo
		{
			/// <summary>
			/// �ڲ�ʹ�õĹ��캯��
			/// </summary>
			internal clsUserXMLInfo()
			{
			}

			internal clsXMLInfos m_objXmlInfos;
		}

		/// <summary>
		/// ����Xml���ݻ�ȡ���XML���ݵ���Ϣ
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public static clsUserXMLInfo s_objGetUserXMLInfo(string p_strXml)
		{
			clsXMLInfos objInfo = s_objGetXMLInfo(p_strXml);

			clsUserXMLInfo objUserXmlInfo = new clsUserXMLInfo();
			objUserXmlInfo.m_objXmlInfos = objInfo;

			return objUserXmlInfo;
		}

		/// <summary>
		/// ��ȡ��Xml��Ϣ
		/// </summary>
		/// <param name="p_objUserXmlInfo">�û�Xml��Ϣ</param>
		/// <param name="p_intStartIndex">��ʼ����</param>
		/// <param name="p_intEndIndex">��������</param>
		/// <returns></returns>
		public static string s_strGetSubXml(clsUserXMLInfo p_objUserXmlInfo,int p_intStartIndex,int p_intEndIndex)
		{
			return m_strGetSubXml(p_objUserXmlInfo.m_objXmlInfos,p_intStartIndex,p_intEndIndex);
		}
		
		#endregion

		#region ����Ҫ������Xml�ı�
		/// <summary>
		/// ����DST�ı���Xml
		/// </summary>
		/// <param name="p_strText">�ı�</param>
		/// <param name="p_strUserID">�û�ID</param>
		/// <param name="p_strUserName">�û�����</param>
		/// <param name="p_clrDST">˫������ɫ</param>
		/// <param name="p_clrText">�ı���ɫ</param>
		/// <returns></returns>
		public static string s_strMakeDSTXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrDST,Color p_clrText)
		{
			return s_strMakeXml(p_strText,p_strUserID,p_strUserName,p_clrDST,p_clrText,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),true);
		}
		/// <summary>
		/// ������ȷ�ı�Xml
		/// </summary>
		/// <param name="p_strText">�ı�</param>
		/// <param name="p_strUserID">�û�ID</param>
		/// <param name="p_strUserName">�û�����</param>
		/// <param name="p_clrText">�ı���ɫ</param>
		/// <returns></returns>
		public static string s_strMakeTextXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrText)
		{
			return s_strMakeXml(p_strText,p_strUserID,p_strUserName,Color.Red,p_clrText,DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"),false);
		}
		/// <summary>
		/// �����ı�Xml
		/// </summary>
		/// <param name="p_strText">�ı�</param>
		/// <param name="p_strUserID">�û�ID</param>
		/// <param name="p_strUserName">�û�����</param>
		/// <param name="p_clrDST">˫������ɫ</param>
		/// <param name="p_clrText">�ı���ɫ</param>
		/// <param name="p_strMakeTime">����ʱ��</param>
		/// <param name="p_blnAllDST">�Ƿ�ȫ��ɾ��</param>
		/// <returns></returns>
		public static string s_strMakeXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrDST,Color p_clrText,string p_strMakeTime,bool p_blnAllDST)
		{
			string strXml = "<r><D>";

			if(p_blnAllDST)
				strXml += "<DI S=\"0\" E=\""+(p_strText.Length-1)+"\" C=\""+p_clrDST.ToArgb()+"\" D=\""+p_strUserID+"\" N=\""+p_strUserName+"\" I=\"1\" T=\""+p_strMakeTime+"\" />";

			strXml += "</D><U><UI D=\""+p_strUserID+"\" N=\""+p_strUserName+"\" S=\"1\" M=\""+p_strMakeTime+"\" C=\""+p_clrText.ToArgb()+"\" /><CI S=\"0\" E=\""+(p_strText.Length-1)+"\" Q=\"1\" /></U></r>";

			return strXml;
		}
		#endregion

		#region �ⲿ��������ı�
		/// <summary>
		/// ������ʾ�ı�
		/// </summary>
		private void m_mthReShow()
		{
			string strTextTemp = this.Text;

			m_blnCanTextChanged = false;
			
			this.Text = "";
			this.Text = strTextTemp;

			if(this.Text.IndexOf('\t') >= 0)
			{
				bool blnCanSelectedChanged = m_blnCanSelectedChanged;
				m_blnCanSelectedChanged = false;
				m_mthReplaceChar('\t',' ');
				this.SelectionStart = m_intCurrentCursorIndex;
				m_blnCanSelectedChanged = blnCanSelectedChanged;
			}

			for(int i=0;i<m_arlTextContentInfos.Count;i++)
			{
				clsUserContentInfo objContentInfo = (clsUserContentInfo)m_arlTextContentInfos[i];

				this.SelectionStart = objContentInfo.m_intStartIndex;
				this.SelectionLength = objContentInfo.m_intEndIndex-objContentInfo.m_intStartIndex+1;
				this.SelectionColor = objContentInfo.objUserInfo.m_clrText;
			}

			if(m_arlTextContentInfos.Count == 0)
			{
				this.SelectionStart = 0;
				this.SelectionLength = strTextTemp.Length;
				this.SelectionColor = m_clrOldPartInsertText;
			}

			this.SelectionStart = 0;
			this.SelectionLength = 0;

			m_blnCanTextChanged = true;
		}

		/// <summary>
		/// �����ⲿ���
		/// </summary>
		/// <param name="p_strText">����ӵ��ı�</param>
		/// <param name="p_intStartIndex">�����</param>
		public void m_mthInsertText(string p_strText, int p_intStartIndex)
		{	
			/*
			 *����RichTextBox�ĺ���Insert�������ı������Ǹú����ķ���ֵ������RichTextBox���ı���
			 �����ı������е�������ɫ�������һ���ַ���ͬ����ʱ�Ľ���취�ǽ�ԭ�����ı���Xml��ʽ
			 ����������Ȼ��RichTextBox�е��ı���գ���������¶������������ͱ�֤���������ɫ��
			 ��仯��������ӵ�ʱ�����һ�¡������֮��Ὣ���ŵ�ԭ���ĵط���
			 */
			int intCursorIndex = m_intCurrentCursorIndex;	
	
			m_intCurrentCursorIndex = p_intStartIndex;
				
			if (this.Text == "")
				this.Text = p_strText;			
			else
				this.Text= this.Text.Insert(p_intStartIndex, p_strText);	

			m_intPreviouslyLen = this.TextLength;

			m_intCurrentDSTIndex = -1;

			m_intTextLenght = m_intPreviouslyLen;

			m_strPrevioslyText = this.Text;

			m_blnIMEInput = false;

			m_mthReShow();				
			
			this.SelectionStart = intCursorIndex  + p_strText.Length;
			this.Focus();

			m_mthAddCurrentDo();
		}

		/// <summary>
		/// �����ⲿɾ��
		/// </summary>
		/// <param name="p_intTextLength">ɾ���ı��ĳ���</param>
		/// <param name="p_intStartIndex">��ʼɾ����λ��</param>
		public void m_mthDeleteText(int p_intTextLength, int p_intStartIndex)
		{	
			/*
			 *����RichTextBox�ĺ���Remove��ɾ���ı������Ǹú����ķ���ֵ������RichTextBox���ı���
			 �����ı������е�������ɫ�������һ���ַ���ͬ����ʱ�Ľ���취�ǽ�ԭ�����ı���Xml��ʽ
			 ����������Ȼ��RichTextBox�е��ı���գ���������¶������������ͱ�֤���������ɫ��
			 ��仯��������ӵ�ʱ�����һ�¡������֮��Ὣ���ŵ�ԭ���ĵط���
			 */
			

			int intCursorIndex = m_intCurrentCursorIndex;		
			
			m_intCurrentCursorIndex = p_intStartIndex;

			m_blnCanTextChanged = false;

			m_blnIsBackspace = false;

			if (this.Text == "")
				return;			
			else
				this.Text= this.Text.Remove(p_intStartIndex, p_intTextLength);	

			m_mthHandleDelete(p_intTextLength);

			m_blnCanTextChanged = true;

			m_intPreviouslyLen = this.TextLength;

			m_intCurrentDSTIndex = -1;

			m_intTextLenght = m_intPreviouslyLen;

			m_strPrevioslyText = this.Text;

			m_blnIMEInput = false;

			m_mthReShow();			
			
			this.Focus();

			m_mthAddCurrentDo();
		}	

		/// <summary>
		/// �����ⲿ�滻
		/// </summary>
		/// <param name="p_strOldValue">���ı�</param>
		/// <param name="p_strNewValue">���ı�</param>
		public void m_mthReplace(string p_strOldValue,string p_strNewValue)
		{
			/*
			 *����RichTextBox�ĺ���Replace���滻�ı������Ǹú����ķ���ֵ������RichTextBox���ı���
			 �����ı������е�������ɫ�������һ���ַ���ͬ����ʱ�Ľ���취�ǽ�ԭ�����ı���Xml��ʽ
			 ����������Ȼ��RichTextBox�е��ı���գ���������¶������������ͱ�֤���������ɫ��
			 ��仯��������ӵ�ʱ�����һ�¡������֮��Ὣ���ŵ�ԭ���ĵط���
			 */
			
			p_strOldValue = p_strOldValue.Replace("\r\n","\n");
			p_strNewValue = p_strNewValue.Replace("\r\n","\n");

			int intIndex = this.Text.IndexOf(p_strOldValue);

			if(intIndex < 0)
				return;

			int intCursorIndex = m_intCurrentCursorIndex;		
			
			m_intCurrentCursorIndex = intIndex;

			m_blnCanTextChanged = false;			

			if (this.Text == "")
				return;			
			else
				this.Text= this.Text.Replace(p_strOldValue,p_strNewValue);	

			m_mthHandleReplace(intIndex,p_strOldValue.Length,p_strNewValue.Length-p_strOldValue.Length);

			m_blnCanTextChanged = true;

			m_intPreviouslyLen = this.TextLength;

			m_intCurrentDSTIndex = -1;

			m_intTextLenght = m_intPreviouslyLen;

			m_strPrevioslyText = this.Text;

			m_blnIMEInput = false;

			m_mthReShow();

			this.Focus();

			m_mthAddCurrentDo();
		}
		#endregion

		#region �������±�
		private ArrayList m_arlSuperSubscript = new ArrayList();
		public const int c_intSuperSubScriptSize = 7;
		public const int c_intSuperScriptCharOffSet = 5;
		public const int c_intSubScriptCharOffSet = -3;
		/// <summary>
		/// �������±�
		/// </summary>
		/// <param name="p_intType">0���ϱꣻ1���±ꣻ2������</param>
		public void m_mthSetSelectionScript(int p_intType)
		{
			m_blnCanTextChanged = false;

			if(p_intType == 0)
			{
				this.SelectionFont = new Font(this.Font.FontFamily,c_intSuperSubScriptSize);
				this.SelectionCharOffset = c_intSuperScriptCharOffSet;
			}
			else if(p_intType == 1)
			{
				this.SelectionFont = new Font(this.Font.FontFamily,c_intSuperSubScriptSize);
				this.SelectionCharOffset = c_intSubScriptCharOffSet;
			}
			else if(p_intType == 2)
			{
				this.SelectionFont = new Font(this.Font.FontFamily,this.Font.Size);
				this.SelectionCharOffset = 0;
			}
		}
		
		/// <summary>
		/// ���±�
		/// </summary>
		public class clsSuperSubScript
		{
			public int m_intIndex;
			public int m_intCharOffset;
			public string m_strValue;
		}
		#endregion

		/// <summary>
		/// ������У�ʹ���޸ĳ���ָ��ʱ���ı�ʱ���μ��й���
		/// </summary>
		public void m_mthCut()
		{
			if(m_blnCanModifySelection)
				base.Cut();
		}
	}
}
