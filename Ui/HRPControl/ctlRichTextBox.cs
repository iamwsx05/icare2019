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
	/// 双划线事件的参数
	/// </summary>
	public class clsDoubleStrikeThoughEventArg : EventArgs
	{
		/// <summary>
		/// 双划线的文本
		/// </summary>
		public string m_strDeleteText;

		/// <summary>
		/// 双划线的颜色
		/// </summary>
		public Color m_clrDST;	
		
		/// <summary>
		/// 添加双划线的用户ID
		/// </summary>
		public string m_strUserID;

		/// <summary>
		/// 添加双划线的用户姓名
		/// </summary>
		public string m_strUserName;

		/// <summary>
		/// 添加双划线的时间
		/// </summary>
		public DateTime m_dtmDeleteTime;
	}		

	/// <summary>
	/// 新添文本事件的参数
	/// </summary>
	public class clsInsertEventArg : EventArgs
	{
		/// <summary>
		/// 新添文本的内容
		/// </summary>
		public string m_strInsertText;

		/// <summary>
		/// 新添文本的颜色
		/// </summary>
		public Color m_clrInsert;	
		
		/// <summary>
		/// 新添文本的用户ID
		/// </summary>
		public string m_strUserID;

		/// <summary>
		/// 新添文本的用户姓名
		/// </summary>
		public string m_strUserName;

		/// <summary>
		/// 新添文本用户的索引号
		/// </summary>
		public int m_intUserSeq;

		/// <summary>
		/// 新添文本的时间
		/// </summary>
		public DateTime m_dtmInsertTime;
	}		

	/// <summary>
	/// 医院书写规范控件
	/// </summary>
	public class ctlRichTextBox : RichTextBox
	{
		/// <summary>
		/// 当前控件的内容状态，在Undo、Redo时用
		/// </summary>
		private class clsRichTextItemStatus
		{
			/// <summary>
			/// 当前的RTF内容
			/// </summary>
			public string m_strRTF;
			/// <summary>
			/// 当前光标的索引
			/// </summary>
			public int m_intCurrentCursorIndex;
			/// <summary>
			/// 当前双划线的信息
			/// </summary>
			public ArrayList m_arlDoubleStrikeThrough = new ArrayList();
			/// <summary>
			/// 当前文本内容的信息
			/// </summary>
			public ArrayList m_arlTextContentInfos = new ArrayList();
		}		

		#region 无效的代码！但为了与旧程序兼容，提供不具体实现的接口
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
		/// 提供高效拼凑String的临时缓冲
		/// </summary>
		private static StringBuilder s_sbdTemp = new StringBuilder();

		/// <summary>
		/// 在文本内容替换时使用的工具
		/// </summary>
		private static RichTextBox s_rtbRTFTrans = new RichTextBox();

		/// <summary>
		/// 读取XML的工具
		/// </summary>
		private static XmlParserContext s_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		/// <summary>
		/// 双划线信息的数组
		/// </summary>
		private static ArrayList s_arlDSTIndex = new ArrayList();

		/// <summary>
		/// 内容信息的数组
		/// </summary>
		private static ArrayList s_arlContentInfo = new ArrayList();

		/// <summary>
		/// 用户信息的数组
		/// </summary>
		private static ArrayList s_arlUserInfo = new ArrayList();
		/// <summary>
		/// 临时用户信息
		/// </summary>
		private static ArrayList s_arlTempUserInfo = new ArrayList();

		/// <summary>
		/// 在获取正确文本内容时，去除旧双划线信息的数组
		/// </summary>
		private static ArrayList s_arlDeleteDSTInfo = new ArrayList();
		/// <summary>
		/// 根据“去除旧双划线信息的数组”的内容，临时保存需要处理的文本内容信息
		/// </summary>
		private static ArrayList s_arlContentTempInfo = new ArrayList();

		/// <summary>
		/// 上下标信息的数组
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
		/// 标记有双删除线的区间
		/// </summary>
		private ArrayList m_arlDoubleStrikeThrough;

//		/// <summary>
//		/// 新添加的文本信息
//		/// </summary>
//		private ArrayList m_arlInsertInfo;

		/// <summary>
		/// 双删除线的显示区间
		/// </summary>
		private ArrayList m_arlDSTView;

		/// <summary>
		/// 新添加的文本显示信息，计算鼠标是否在新添加的文本上时使用。
		/// </summary>
		private ArrayList m_arlInsertView;

		/// <summary>
		/// 当前用户的双删除线的颜色
		/// </summary>
		private Color m_clrDST;		

		/// <summary>
		/// 当前用户的双删除线颜色的设置和获取
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
		/// 当前用户在旧的部分输入文字的颜色
		/// </summary>
		private Color m_clrOldPartInsertText;		

		/// <summary>
		/// 当前用户在旧的部分输入文字颜色的设置和获取
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
					//使用默认颜色
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
		/// 当前用户ID
		/// </summary>
		private string m_strUserID;

		/// <summary>
		/// 当前用户ID的设置和获取
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
		/// 当前用户ID
		/// </summary>
		private string m_strUserName;

		/// <summary>
		/// 当前用户ID的设置和获取
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
		/// 用户交互时是否已经选择了文本
		/// </summary>
		private bool m_blnIsSelectedChanged;

		/// <summary>
		/// 是否处理选择改变的事件
		/// </summary>
		private bool m_blnCanSelectedChanged;

		/// <summary>
		/// 用户改变文本内容之前的长度
		/// </summary>
		private int m_intPreviouslyLen;

		/// <summary>
		/// 当前光标索引
		/// </summary>
		private int m_intCurrentCursorIndex;

		/// <summary>
		/// 记录用户选择文本的开始索引
		/// </summary>
		private int m_intSelectedTextStartIndex;

		/// <summary>
		/// 记录用户选择文本的长度
		/// </summary>
		private int m_intSelectedTextLength;

		/// <summary>
		/// 标记是后退键还是删除键
		/// </summary>
		private bool m_blnIsBackspace;

		/// <summary>
		/// 生成XML的内存
		/// </summary>
		private MemoryStream m_objXmlStream;

		/// <summary>
		/// 生成XML的工具
		/// </summary>
		private XmlTextWriter m_objXmlWriter;

		/// <summary>
		/// 读取XML的工具
		/// </summary>
		private XmlParserContext m_objXmlParser;

		/// <summary>
		/// 标记是否处理TextChanged事件
		/// </summary>
		private bool m_blnCanTextChanged;

		/// <summary>
		/// 标记是否可以修改选择的内容
		/// </summary>
		private bool m_blnCanModifySelection;

		/// <summary>
		/// 是否记住上下标
		/// </summary>
		private bool m_blnRememberScripts = true;

		/// <summary>
		/// 最后可见的坐标
		/// </summary>
		private Point m_pntEndVisible;

		/// <summary>
		/// 鼠标进入双删除线区间事件
		/// </summary>
		public event EventHandler m_evtMouseEnterDeleteText;

		/// <summary>
		/// 鼠标离开双删除线区间事件
		/// </summary>
		public event EventHandler m_evtMouseLeaveDeleteText;

		/// <summary>
		/// 鼠标进入插入文本区间事件
		/// </summary>
		public event EventHandler m_evtMouseEnterInsertText;

		/// <summary>
		/// 鼠标离开插入文本区间事件
		/// </summary>
		public event EventHandler m_evtMouseLeaveInsertText;

		/// <summary>
		/// 当前双删除线的区间索引
		/// </summary>
		private int m_intCurrentDSTIndex;

		/// <summary>
		/// 当前插入文本区间索引
		/// </summary>
		private int m_intCurrentInsertIndex;

		/// <summary>
		/// 记录鼠标坐标的临时变量
		/// </summary>
		private Point m_pntMousePositionTemp;

		/// <summary>
		/// 字体高度
		/// </summary>
		private int m_intFontHeight;

		/// <summary>
		/// 文本长度
		/// </summary>
		private int m_intTextLenght;

		/// <summary>
		/// 当前的双删除线事件参数
		/// </summary>
		private clsDoubleStrikeThoughEventArg m_objCurrentEventArg;

		/// <summary>
		/// 当前的插入文本事件参数
		/// </summary>
		private clsInsertEventArg m_objCurrentInsertEventArg;

		/// <summary>
		/// 前一次的文本内容
		/// </summary>
		private string m_strPrevioslyText;

		/// <summary>
		/// 是否是在输入法模式下，如果时，不重画。
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
		/// 全局控制，标记双划线是否下双划线
		/// </summary>
		private bool m_blnUnderLineDST = false;

		/// <summary>
		/// 全局控制双划线是否下双划线
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
		/// Undo、Redo的工具
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
		/// 初始化Undo\Rodo工具
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
		/// 复制双删除线信息
		/// </summary>
		/// <param name="p_arlSourceDST">原信息集</param>
		/// <param name="p_arlDestDST">目标信息集</param>
		/// <param name="p_blnClearDest">是否清空目标信息集</param>
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
		/// 复制文本内容信息
		/// </summary>
		/// <param name="p_arlSourceTextContentInfo">原信息集</param>
		/// <param name="p_arlDestTextContentInfo">目标信息集</param>
		/// <param name="p_blnClearDest">是否清空目标信息集</param>
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
		/// 添加当前操作信息
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
		/// DoItem缓冲池
		/// </summary>
		private static Queue s_queDoItemPool = new Queue();
		/// <summary>
		/// 从缓冲池获取DoItem。
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
		/// 添加DoItem到缓冲池
		/// </summary>
		/// <param name="p_objDoItemArr">DoItem数组</param>
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
		/// 添加DoItem到缓冲池
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
		/// 双划线记录的缓冲池
		/// </summary>
		private static Queue s_queDSTPool = new Queue();
		/// <summary>
		/// 从缓冲池获取双划线记录。
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
		/// 添加双划线记录到缓冲池
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
		/// 添加文本信息的缓冲池
		/// </summary>
		private static Queue s_queContentPool = new Queue();
		/// <summary>
		/// 从缓冲池获取文本信息。
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
		/// 添加文本信息到缓冲池
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

			if(m.Msg == 0x000F)//Repaint事件
			{				
				Graphics objGrp = this.CreateGraphics();

				if(!m_blnIMEInput)
					m_mthDrawDST(objGrp);

				objGrp.Dispose();
			}			
		}

		#region 画双删除线
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

			#region 计算在旧内容中插入
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
						#region 头尾同一行
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

			#region 计算画双删除线
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
						#region 头尾同一行
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

		#region 设置选择的字符串有无双删除线
		/// <summary>
		/// 设置选择的字符串有无双删除线
		/// </summary>
		/// <param name="p_blnAddDST">是否是添加双删除线</param>
		public void m_mthSelectionDoubleStrikeThough(bool p_blnAddDST)
		{
			/*
			 * 简化为先使用替换操作把旧的双删除线去掉，
			 * 然后把所有选择的添加双删除线。
			 * 如果不为选择的添加双删除线，就是实现了去掉双删除线。
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

		#region 处理用户交互文本
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
		/// 用户选择了文本后再交互
		/// </summary>
		private void m_mthHandleSelectedChanged()
		{
			/*
			 * 用户在选择了文本后进行交互。
			 * 存在两种情况：
			 * 把所有选择的文本全部删除；
			 * 用新的文本（长度不定）代替选择的文本。
			 */
			m_blnCanSelectedChanged = false;

			int intDiffLen = this.TextLength - m_intPreviouslyLen;

			if(-1*intDiffLen == m_intSelectedTextLength)
			{
				//删除选择
				m_intCurrentCursorIndex = m_intSelectedTextStartIndex;
				m_blnIsBackspace = false;

				m_mthHandleDelete(m_intSelectedTextLength);
			}
			else
			{
				//替换
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
		/// 处理用户没有选择文本而进行的交互
		/// </summary>
		private void m_mthHandleNotSelectedChanged()
		{
			/*
			 * 用户没有选择文本的情况下的交互。
			 * 存在两种情况：
			 * 文本变长：光标后添加；
			 * 文本变短：从光标开始向前或向后删除。
			 */
			int intDiffLen = this.TextLength - m_intPreviouslyLen;

			if(intDiffLen > 0)
			{
				//插入
				m_mthHandleInsert(intDiffLen);
			}
			else
			{
				//删除
				m_mthHandleDelete(-1*intDiffLen);
			}
		}

		/// <summary>
		/// 处理添加
		/// </summary>
		/// <param name="p_intNewLen">新添加的长度</param>
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
		/// 处理替换
		/// </summary>
		/// <param name="p_intStartIndex">改变文本的开始索引</param>
		/// <param name="p_intOldLength">旧文本的长度</param>
		/// <param name="p_intDiffLength">新文本和旧文本相差的长度。</param>
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

			//只能在自己添加的区域替换，并由此决定此区域必定连续区域。
			//即添加只在一个区域内替换。
			//但需要把替换的区域后的区域更新
			for(int i=0;i<m_arlTextContentInfos.Count;i++)
			{
				clsUserContentInfo objContentInfo = 
					(clsUserContentInfo)m_arlTextContentInfos[i];

				if(objContentInfo.m_intStartIndex <= p_intStartIndex
					&& objContentInfo.m_intEndIndex >= p_intStartIndex)					
				{
					//此区域是替换区域，更新结束坐标
					objContentInfo.m_intEndIndex += p_intDiffLength;

					if(objContentInfo.m_intEndIndex < objContentInfo.m_intStartIndex)
					{
						//此区域被删除
						m_arlTextContentInfos.RemoveAt(i);
						i--;
					}
				}					
				else if(objContentInfo.m_intStartIndex > p_intStartIndex)
				{
					//此区域在替换区域后，更新开始和结束坐标
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
		/// 处理删除
		/// </summary>
		/// <param name="p_intOldLen">删除的长度</param>
		private void m_mthHandleDelete(int p_intOldLen)
		{
			/*
			 * 看作是新长度为0的特殊替换
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

		#region 记录当前光标的位置和用户按键信息，判断是否需要控制
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

		#region 操作XML
		/// <summary>
		/// 设置新的文本信息
		/// </summary>
		/// <param name="p_strText">文字信息</param>
		/// <param name="p_strXml">文字格式的Xml信息</param>
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
										//第一个输入者的颜色使用显示颜色
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
				//出错后清空格式
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
		/// 获取XML文本
		/// </summary>
		/// <returns>XML文本</returns>
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
					//第一个输入者，使用默认颜色
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
					//HB Add:在文本最后有空格的时候"E"为去掉空格后的索引，
					//因为在Oracle下保存时会去掉空格，而控件会保留空格的位数，索引值就会出错
					m_objXmlWriter.WriteAttributeString("E",Convert.ToString(this.Text.Length-1));
				}
				m_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				m_objXmlWriter.WriteEndElement();
			}
			m_objXmlWriter.WriteEndElement();

			#region 记住上下标
//			bool blnThisFocus = this.Focused;
//			//先使自己失去焦点，不然屏幕会闪，很慢
//			Panel pnlTemp = new Panel();
//			pnlTemp.Size = new Size(0,0);
//			this.Controls.Add(pnlTemp);
//			pnlTemp.Focus();

			int intSelectionStart = this.SelectionStart;
			this.SelectionLength = 0;
			int intPreCharOffSet = -1;//上个字符偏移量
			int intPreIndex = -1;
			for(int i = 1; i <= this.Text.Length; i++)
			{
				this.SelectionStart = i;
//				this.SelectionLength = 1;
				int intCharOffSet = this.SelectionCharOffset;

				//同一种上下标
				if(intCharOffSet == intPreCharOffSet)
				{
					if(i < this.Text.Length)//还没到最后一个字符
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
					if(i < this.Text.Length)//还没到最后一个字符
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
		/// 标记结束修改，记录修改时间
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
		/// 清空文本内容
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
		/// 重置控件状态时保留痕迹
		/// </summary>
		public void m_mthResetContextInfo()
		{
			m_mthClearText();

			m_objCurrentModifyUser.m_intUserSequence = 1;
			m_objCurrentModifyUser.m_dtmModifyDate = DateTime.Now;
			m_arlModifyUsers.Add(m_objCurrentModifyUser);//如果不添加这样一个列表，则清空后再保存时不能保留痕迹

			m_objCurrentModifyUser.m_intUserSequence = -1;
			m_objCurrentModifyUser.m_dtmModifyDate = DateTime.MinValue;
		}

		/// <summary>
		/// 获取正确的文本
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
        //重载，当判断com.digitalwave.Utility.Controls.ctlRichTextBox内容是否改变时用
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
		#region 文本分析输出
		/// <summary>
		/// 获取正确的文本
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
		/// 获取正确的文本
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

										//刚好对上
										if(intUserIndex == objCurrentUserInfo.m_intUserSequence)
										{
											objContentInfo.objUserInfo = objCurrentUserInfo;
										}
										else if(intUserIndex > objCurrentUserInfo.m_intUserSequence)
										{
											//比下一个要小，说明内容的用户被整理，内容用户改为当前用户
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
						//卡尺之前，不做变化保留
						s_arlContentTempInfo.Add(p_objContentInfoArr[j2]);
						continue;
					}

					if(blnIsFirst)
					{
						if(intStartDeleteDST > p_objContentInfoArr[j2].m_intStartIndex
							&& intStartDeleteDST <= p_objContentInfoArr[j2].m_intEndIndex)
						{
							//前卡尺不与前点重合并在范围内，生成新范围，结束点使用卡尺数减一
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
							//前卡尺与前点重合，前卡尺与后卡尺之间的内容不需要。
							blnIsFirst = false;

							j2--;
						}
					}
					else
					{
						if(p_objContentInfoArr[j2].m_intEndIndex < intEndDeleteDST)
						{
							//范围在删除区域内，去掉
							continue;
						}

						if(intEndDeleteDST >= p_objContentInfoArr[j2].m_intStartIndex
							&& intEndDeleteDST < p_objContentInfoArr[j2].m_intEndIndex)
						{
							//后卡尺不与后点重合并在范围内，开始点使用卡尺数加一，并以后各点移位

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
							//后卡尺与后点重合
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
		/// 获取正确的文本
		/// </summary>
		/// <param name="p_strText">原文本</param>
		/// <param name="p_strXml">原Xml</param>
		/// <param name="p_dtmSeparateTime">分割的时间</param>
		/// <param name="p_strNewXml">新的Xml</param>
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

		#region Xml 合并和拆分
		/// <summary>
		/// 生成XML的内存
		/// </summary>
		private static MemoryStream s_objXmlStream = new MemoryStream();

		/// <summary>
		/// 生成XML的工具
		/// </summary>
		private static XmlTextWriter s_objXmlWriter = new XmlTextWriter(s_objXmlStream,System.Text.Encoding.Unicode);
		
		/// <summary>
		/// 临时存放文本的变量
		/// </summary>
		private static ArrayList s_arlStrigTemp = new ArrayList();

		/// <summary>
		/// 信息
		/// </summary>
		internal class clsXMLInfos
		{
			/// <summary>
			/// 双划线
			/// </summary>
			public clsDSTInfo[] m_objDSTArr;

			/// <summary>
			/// 每段内容的信息
			/// </summary>
			public clsUserContentInfo[] m_objCIArr;

			/// <summary>
			/// 用户信息，以时间排序
			/// </summary>
			public clsModifyUserInfo[] m_objUserInfo;

			/// <summary>
			/// 上下标信息
			/// </summary>
			public clsSuperSubScript[] m_objScripts;

			public int m_intTextLenght;
		}
		/// <summary>
		/// 从XML获取信息对象
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
		/// 存放信息对象
		/// </summary>
		private static ArrayList s_arlXMLInfos = new ArrayList();
		/// <summary>
		/// 合并XML
		/// </summary>
		/// <param name="p_strXmlArr">Xml数组</param>
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

				#region 处理上下标
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
				s_objXmlWriter.WriteAttributeString("E",objContentInfo.m_intEndIndex.ToString());//如果合并XML后打印出错修改此处
				s_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteEndElement();
			}
			s_objXmlWriter.WriteEndElement();

			#region 处理上下标
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
		/// 拆分
		/// </summary>
		/// <param name="p_strText">文本</param>
		/// <param name="p_strXml">Xml</param>
		/// <param name="p_intCharPerLine">一行有多少个字符</param>
		/// <param name="p_strTextArr">拆分后的文本</param>
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

					//如果开始位小于p_intStartIndex，修改开始位为p_intStartIndex
					if(objStartInfo.m_intIndexRange < p_intStartIndex)
						objStartInfo.m_intIndexRange = p_intStartIndex;

					blnIsStart = false;

					//如果结束位大于p_intEndIndex，修改结束位为p_intEndIndex
					if(objEndInfo.m_intIndexRange > p_intEndIndex)
					{
						objEndInfo.m_intIndexRange = p_intEndIndex;
						blnIsStart = true;
					}
				}
				else
				{
					//只有开始位小于等于p_intEndIndex才继续，否则跳出循环
					if(objStartInfo.m_intIndexRange > p_intEndIndex)
						break;

					//如果结束位大于p_intEndIndex，修改结束位为p_intEndIndex
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

				//复原开始位和结束位
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
					//只有结束位大于等于p_intStartIndex才开始
					if(objContentInfo.m_intEndIndex < p_intStartIndex)
						continue;

					//如果开始位小于p_intStartIndex，修改开始位为p_intStartIndex
					if(objContentInfo.m_intStartIndex < p_intStartIndex)
						objContentInfo.m_intStartIndex = p_intStartIndex;

					//如果结束位大于p_intEndIndex，修改结束位为p_intEndIndex
					if(objContentInfo.m_intEndIndex > p_intEndIndex)
						objContentInfo.m_intEndIndex = p_intEndIndex;

					blnIsStart = false;
				}
				else
				{
					//只有开始位小于等于p_intEndIndex才继续，否则跳出循环
					if(objContentInfo.m_intStartIndex > p_intEndIndex)
						break;

					//如果结束位大于p_intEndIndex，修改结束位为p_intEndIndex
					if(objContentInfo.m_intEndIndex > p_intEndIndex)
						objContentInfo.m_intEndIndex = p_intEndIndex;
				}

				s_objXmlWriter.WriteStartElement("CI");
				s_objXmlWriter.WriteAttributeString("S",(objContentInfo.m_intStartIndex-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("E",(objContentInfo.m_intEndIndex-p_intStartIndex).ToString());
				s_objXmlWriter.WriteAttributeString("Q",objContentInfo.objUserInfo.m_intUserSequence.ToString());
				s_objXmlWriter.WriteEndElement();

				//复原开始位和结束位
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
		/// 存放XML内容的信息
		/// </summary>
		public class clsUserXMLInfo
		{
			/// <summary>
			/// 内部使用的构造函数
			/// </summary>
			internal clsUserXMLInfo()
			{
			}

			internal clsXMLInfos m_objXmlInfos;
		}

		/// <summary>
		/// 根据Xml内容获取存放XML内容的信息
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
		/// 获取子Xml信息
		/// </summary>
		/// <param name="p_objUserXmlInfo">用户Xml信息</param>
		/// <param name="p_intStartIndex">开始索引</param>
		/// <param name="p_intEndIndex">结束索引</param>
		/// <returns></returns>
		public static string s_strGetSubXml(clsUserXMLInfo p_objUserXmlInfo,int p_intStartIndex,int p_intEndIndex)
		{
			return m_strGetSubXml(p_objUserXmlInfo.m_objXmlInfos,p_intStartIndex,p_intEndIndex);
		}
		
		#endregion

		#region 根据要求生成Xml文本
		/// <summary>
		/// 生成DST文本的Xml
		/// </summary>
		/// <param name="p_strText">文本</param>
		/// <param name="p_strUserID">用户ID</param>
		/// <param name="p_strUserName">用户名称</param>
		/// <param name="p_clrDST">双划线颜色</param>
		/// <param name="p_clrText">文本颜色</param>
		/// <returns></returns>
		public static string s_strMakeDSTXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrDST,Color p_clrText)
		{
			return s_strMakeXml(p_strText,p_strUserID,p_strUserName,p_clrDST,p_clrText,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),true);
		}
		/// <summary>
		/// 生成正确文本Xml
		/// </summary>
		/// <param name="p_strText">文本</param>
		/// <param name="p_strUserID">用户ID</param>
		/// <param name="p_strUserName">用户名称</param>
		/// <param name="p_clrText">文本颜色</param>
		/// <returns></returns>
		public static string s_strMakeTextXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrText)
		{
			return s_strMakeXml(p_strText,p_strUserID,p_strUserName,Color.Red,p_clrText,DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"),false);
		}
		/// <summary>
		/// 生成文本Xml
		/// </summary>
		/// <param name="p_strText">文本</param>
		/// <param name="p_strUserID">用户ID</param>
		/// <param name="p_strUserName">用户名称</param>
		/// <param name="p_clrDST">双划线颜色</param>
		/// <param name="p_clrText">文本颜色</param>
		/// <param name="p_strMakeTime">生成时间</param>
		/// <param name="p_blnAllDST">是否全文删除</param>
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

		#region 外部代码操作文本
		/// <summary>
		/// 重新显示文本
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
		/// 处理外部添加
		/// </summary>
		/// <param name="p_strText">新添加的文本</param>
		/// <param name="p_intStartIndex">插入点</param>
		public void m_mthInsertText(string p_strText, int p_intStartIndex)
		{	
			/*
			 *利用RichTextBox的函数Insert来插入文本，但是该函数的返回值是整个RichTextBox的文本，
			 所以文本中所有的字体颜色都会跟第一个字符相同，暂时的解决办法是将原来的文本的Xml格式
			 保存起来，然后将RichTextBox中的文本清空，最后再重新读出来，这样就保证了字体的颜色不
			 会变化，但是添加的时候会闪一下。添加完之后会将光标放到原来的地方。
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
		/// 处理外部删除
		/// </summary>
		/// <param name="p_intTextLength">删除文本的长度</param>
		/// <param name="p_intStartIndex">开始删除的位置</param>
		public void m_mthDeleteText(int p_intTextLength, int p_intStartIndex)
		{	
			/*
			 *利用RichTextBox的函数Remove来删除文本，但是该函数的返回值是整个RichTextBox的文本，
			 所以文本中所有的字体颜色都会跟第一个字符相同，暂时的解决办法是将原来的文本的Xml格式
			 保存起来，然后将RichTextBox中的文本清空，最后再重新读出来，这样就保证了字体的颜色不
			 会变化，但是添加的时候会闪一下。添加完之后会将光标放到原来的地方。
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
		/// 处理外部替换
		/// </summary>
		/// <param name="p_strOldValue">旧文本</param>
		/// <param name="p_strNewValue">新文本</param>
		public void m_mthReplace(string p_strOldValue,string p_strNewValue)
		{
			/*
			 *利用RichTextBox的函数Replace来替换文本，但是该函数的返回值是整个RichTextBox的文本，
			 所以文本中所有的字体颜色都会跟第一个字符相同，暂时的解决办法是将原来的文本的Xml格式
			 保存起来，然后将RichTextBox中的文本清空，最后再重新读出来，这样就保证了字体的颜色不
			 会变化，但是添加的时候会闪一下。添加完之后会将光标放到原来的地方。
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

		#region 处理上下标
		private ArrayList m_arlSuperSubscript = new ArrayList();
		public const int c_intSuperSubScriptSize = 7;
		public const int c_intSuperScriptCharOffSet = 5;
		public const int c_intSubScriptCharOffSet = -3;
		/// <summary>
		/// 设置上下标
		/// </summary>
		/// <param name="p_intType">0：上标；1：下标；2：正常</param>
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
		/// 上下标
		/// </summary>
		public class clsSuperSubScript
		{
			public int m_intIndex;
			public int m_intCharOffset;
			public string m_strValue;
		}
		#endregion

		/// <summary>
		/// 处理剪切，使得修改超过指定时间文本时屏蔽剪切功能
		/// </summary>
		public void m_mthCut()
		{
			if(m_blnCanModifySelection)
				base.Cut();
		}
	}
}
