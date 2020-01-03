//#define FunctionPrivilege
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices; 
using System.Runtime.Remoting; 
using System.Threading;
using System.Text;
using com.digitalwave.Utility;
using HRP;
using weCare.Core.Entity; 
using com.digitalwave.Utility.Controls;
using System.Reflection;
//using com.digitalwave.iCare.middletier.HRPService;
using iCare.ICU.Evaluation;

namespace iCare
{
	public class MDIParent : iCareBaseForm.frmBaseForm
	{
		#region Declare

		private System.Windows.Forms.StatusBar StatusBar;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		protected internal System.Windows.Forms.Timer timMDI;
		private System.ComponentModel.IContainer components;

		//Define ToolBar and munuBar
		ReBar reBar = new ReBar();
		CommandBar toolBar = new CommandBar(CommandBarStyle.ToolBar);
		public static bool ICCardCanRead=true;
		private System.Windows.Forms.MainMenu mnuMDI;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuOpenExplorer;
		private System.Windows.Forms.MenuItem mnuSave;
		private System.Windows.Forms.MenuItem mnuPrintPriview;
		private System.Windows.Forms.MenuItem mnuPrint;
		private System.Windows.Forms.MenuItem mnuSplitter1;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem mnuEmail;
		private System.Windows.Forms.MenuItem mnuSplitter6;
		private System.Windows.Forms.MenuItem mnuVersion;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.Timer timCount;
		private System.Windows.Forms.MenuItem mnuWindows;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuDel;
		private System.Windows.Forms.MenuItem mnuCopy;
		private System.Windows.Forms.MenuItem mnuPaste;
		private System.Windows.Forms.MenuItem mnuCut;
		private System.Windows.Forms.MenuItem mnuSplitter;
		private System.Windows.Forms.MenuItem mnuUndo;
		private System.Windows.Forms.MenuItem mnuRedo;
		private System.Windows.Forms.MenuItem mnuiCareTools;
		private System.Windows.Forms.MenuItem mnuReport;
		private System.Windows.Forms.MenuItem mnuAssTools;
		private System.Windows.Forms.MenuItem mnuDoctorWorkStationForEar;
		private System.Windows.Forms.MenuItem mnuNurseWorkStationForEar;
		private System.Windows.Forms.MenuItem mnuICUTendRecord;
		private System.Windows.Forms.MenuItem mnuThreeMeasureRecord;
		private System.Windows.Forms.MenuItem mnuTendRecord;
		private System.Windows.Forms.MenuItem mnuOperationQty;
		private System.Windows.Forms.MenuItem mnuEvaluate;
		private System.Windows.Forms.MenuItem mnuOperationRecord;
		private System.Windows.Forms.MenuItem mnuGeneralTenda;
		private System.Windows.Forms.MenuItem mnuMainRecord;
		private System.Windows.Forms.MenuItem mnuWatchItem;
		private System.Windows.Forms.MenuItem mnufrmPICUShiftOutForm;
		private System.Windows.Forms.MenuItem mnufrmPICUShiftInForm;
		private System.Windows.Forms.MenuItem mnuTemplateTools;
		private System.Windows.Forms.MenuItem mnuPatientProcessRecord;
		private System.Windows.Forms.MenuItem mnuApplyRecord;
		private System.Windows.Forms.MenuItem mnuSPECT;
		private System.Windows.Forms.MenuItem mnuHighOxygen;
		private System.Windows.Forms.MenuItem mnuBultransonic;
		private System.Windows.Forms.MenuItem mnuOperationRecordDoct;
		private System.Windows.Forms.MenuItem mnuCTCheckOrder;
		private System.Windows.Forms.MenuItem mnuOperationAgreed;
		private System.Windows.Forms.MenuItem mnuQC;
		private System.Windows.Forms.MenuItem mnuXRay;
		private System.Windows.Forms.MenuItem mnuOperationSummary;
		private System.Windows.Forms.MenuItem mnuPathologyOrgCheckOrder;
		private System.Windows.Forms.MenuItem mnuMRIOrder;
		private System.Windows.Forms.MenuItem mnuEightSystem;
		private System.Windows.Forms.MenuItem mnuGrade;
		private System.Windows.Forms.MenuItem mnuSIRS;
		private System.Windows.Forms.MenuItem mnuGlasgow;
		private System.Windows.Forms.MenuItem mnuLung;
		private System.Windows.Forms.MenuItem mnuNewBaby;
		private System.Windows.Forms.MenuItem mnuLittleBaby;
		private System.Windows.Forms.MenuItem mnuAPACHEII;
		private System.Windows.Forms.MenuItem mnuAPACHEIII;
		private System.Windows.Forms.MenuItem mnuTISS28;
		private System.Windows.Forms.MenuItem mnuCommonUse;
		private System.Windows.Forms.MenuItem mnuInPatientCaseHistory;
		private System.Windows.Forms.MenuItem mnuInPatientCaseMode2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mniOutExpertManage;
		private System.Windows.Forms.MenuItem m_mnuDebug;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem mnuDebuTool;
		private System.Windows.Forms.MenuItem m_mniManageExplorer;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem mnuICUIntensiveTend;
		private System.Windows.Forms.MenuItem menuItem26;
		public const float c_fltWordWidth=17.5f;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem m_mniIntelligentStatics;
		private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem m_mniArchiving;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		/// <summary>
		/// �ָ���ţ������Ƕ���ַ���
		/// </summary>
		public static char[] c_chrSplitChars=new char[]{';'};
        //private clsPrivilegeInfo[] m_objPIArr;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem mniPatientLabel;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.StatusBarPanel statusBarPanel4;
		private System.Windows.Forms.MenuItem mniManageDept;
		private System.Windows.Forms.MenuItem mniManageDocAndNur;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem mniImageBookingSearch;
		private System.Windows.Forms.MenuItem mniImageReport;
		private System.Windows.Forms.MenuItem mniLabCheckReport;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem menuItem45;
		private System.Windows.Forms.MenuItem mniEKGOrder;
		private System.Windows.Forms.MenuItem mniNuclearOrder;
		private System.Windows.Forms.MenuItem mniPSGOrder;
		private System.Windows.Forms.MenuItem menuItem46;
		private System.Windows.Forms.MenuItem menuItem47;
		private System.Windows.Forms.MenuItem m_mniEmployeeMove;
		private System.Windows.Forms.MenuItem menuItem48;
		private System.Windows.Forms.MenuItem m_mniDefaultValue;
		private System.Windows.Forms.MenuItem m_mniSaveDefaultValue;
		private System.Windows.Forms.MenuItem m_mniResetDefaultValue;
		private System.Windows.Forms.MenuItem menuItem50;
		private System.Windows.Forms.MenuItem m_mniTemplate;
		private System.Windows.Forms.MenuItem m_mniAddNewTemplate;
		private System.Windows.Forms.MenuItem m_mniAddNewCommonUseTemplate;
		private System.Windows.Forms.MenuItem m_mniTemplateManage;
		private System.Windows.Forms.MenuItem mnuSpectialSymbol;
		private System.Windows.Forms.MenuItem menuItem1;
		private string m_strCurrentDeptID = null;		
		#endregion
		/// <summary>
		/// �ж��Ƿ��½����ϵͳ
		/// </summary>
		
		#region Static Decare
		public static bool s_bolIAnaSystem = true;
		public static string strOperatorID
        {
            get { return clsEMRLogin.LoginInfo.m_strEmpNo; }
        }
        public static string strOperatorName
        {
            get { return clsEMRLogin.LoginInfo.m_strEmpName; }
        }
		private static MDIParent s_objMDIParent = null;
		internal static clsInpatMedRec_DataShare s_objInpatMedRec_DataShare;
		#endregion Static Decare
		private System.Windows.Forms.Timer timRevisitRemind;

		private clsDomainUserLoginInfo objclsDomainUserLoginInfo=new clsDomainUserLoginInfo();

		public MDIParent()
		{
			InitializeComponent();
            
			//			load = new Loading();
			//			load.Visible =true;
			//			load.Show(); 
			//			load.Refresh();

			#region Add ToolBar and MenuBar
			// Menu and toolbar
            //try
            //{
            //    CommandBarItem openItem = new CommandBarItem(Images.Open, "�򿪹�����", new EventHandler(Open_Click), Keys.Control | Keys.O);
            //    CommandBarItem saveItem = new CommandBarItem(Images.Save, "����", new EventHandler(Save_Click), Keys.Control | Keys.S);
            //    CommandBarItem deleteItem = new CommandBarItem(Images.Delete, "ɾ��", new EventHandler(Del_Click), Keys.Control | Keys.D);
            //    CommandBarItem PreviewItem = new CommandBarItem(Images.Preview, "��ӡԤ��", new EventHandler(Preview_Click), Keys.Control | Keys.Y);
            //    CommandBarItem PrintItem = new CommandBarItem(Images.Print, "��ӡ", new EventHandler(Print_Click), Keys.Control | Keys.P);
            //    CommandBarItem deactiveItem = new CommandBarItem(Images.Refresh, "ɾ����ѯ", new EventHandler(Refresh_Click), Keys.Control | Keys.F);

            //    CommandBarItem newTemplateItem = new CommandBarItem(Images.Icons, "����ģ��", new EventHandler(NewTemplate_Click), Keys.Control | Keys.T);
            //    CommandBarItem newCommonUseItem = new CommandBarItem(Images.List, "���ɳ���ֵģ��", new EventHandler(NewCommonUse_Click), Keys.Control | Keys.U);
            //    CommandBarItem DefaultValueItem = new CommandBarItem(Images.Languages, "����Ĭ��ֵ", new EventHandler(DefaultValue_Click), Keys.Control | Keys.M);
            //    CommandBarItem ResetDefaultValueItem = new CommandBarItem(Images.History, "����Ĭ��ֵ", new EventHandler(ResetDefaultValue_Click), Keys.Control | Keys.H);

            //    toolBar.Items.Add(openItem);				
            //    toolBar.Items.Add(saveItem);
            //    toolBar.Items.Add(deleteItem);
            //    toolBar.Items.Add(PreviewItem);
            //    toolBar.Items.Add(PrintItem);
            //    toolBar.Items.Add(deactiveItem);
            //    toolBar.Items.Add(new CommandBarItem(CommandBarItemStyle.Separator));

            //    toolBar.Items.Add(newTemplateItem);
            //    toolBar.Items.Add(newCommonUseItem);
            //    //				toolBar.Items.Add(DefaultValueItem);
            //    //				toolBar.Items.Add(ResetDefaultValueItem);
            //    toolBar.Items.Add(new CommandBarItem(CommandBarItemStyle.Separator));

            //    CommandBarItem approveItem = new CommandBarItem(Images.Properties, "���", new EventHandler(Approve_Click), Keys.Control | Keys.N);
            //    CommandBarItem unapproveItem = new CommandBarItem(Images.Stop, "����", new EventHandler(Unapprove_Click), Keys.Control | Keys.M);

            //    toolBar.Items.Add(approveItem);
            //    toolBar.Items.Add(unapproveItem);
            //    //��һ�ָ���
            //    toolBar.Items.Add(new CommandBarItem(CommandBarItemStyle.Separator));
				
				
            //    //*******************************************************************
            //    CommandBarItem cutItem = new CommandBarItem(Images.Cut, "����", new EventHandler(Cut_Click), Keys.Control | Keys.X);
            //    CommandBarItem copyItem = new CommandBarItem(Images.Copy, "����", new EventHandler(Copy_Click), Keys.Control | Keys.C);
            //    CommandBarItem pasteItem = new CommandBarItem(Images.Paste, "ճ��", new EventHandler(Paste_Click), Keys.Control | Keys.V);
            //    CommandBarItem undoItem = new CommandBarItem(Images.Undo, "����", new EventHandler(Undo_Click), Keys.Control | Keys.Z);
            //    CommandBarItem redoItem = new CommandBarItem(Images.Redo, "�ָ�", new EventHandler(Redo_Click), Keys.Control | Keys.Shift|Keys.Z);
				
            //    toolBar.Items.Add(cutItem);
            //    toolBar.Items.Add(copyItem);
            //    toolBar.Items.Add(pasteItem);
            //    toolBar.Items.Add(undoItem);
            //    toolBar.Items.Add(redoItem);
				
            //    //*********************************************************************************
            //    //				CommandBarItem SearchItem = new CommandBarItem(Images.Search, "����", null, Keys.Control | Keys.Z);
            //    //				
            //    //				toolBar.Items.Add(SearchItem);

            //    toolBar.Items.Add(new CommandBarItem(CommandBarItemStyle.Separator));

            //    //****************************************************************************************
            //    CommandBarItem exitItem = new CommandBarItem(Images.Exit, "�˳�", new EventHandler(Exit_Click), Keys.Control | Keys.E);
            //    toolBar.Items.Add(exitItem);

            //    reBar.Bands.Add(toolBar);
            //    Controls.Add(reBar);
		
            //}
            //catch
            //{}
			#endregion     

            //m_objPIArr = clsLoginContext.s_ObjLoginContext.m_ObjPIArr;

			#region ��ȡԱ����ǰ�Ĳ��ţ�Ա���������ڶ�����ţ�����ֻȡ����һ��
			//			string strEmployeeID = clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID;			
			//			new clsRoleManager().m_lngGetDeptByEmployeeID(strEmployeeID,out m_objOISFArr);
			//			for(int i=0;i<m_objOISFArr.Length;i++)
			//			{
			//				if(m_objOISFArr[i].m_strBaseID!="0000000")
			//				{
			//					m_strCurrentDeptID = m_objOISFArr[i].m_strBaseID;
			//					break;
			//				}
			//			}
			#endregion

            s_objMDIParent = this;

            //m_mthSetInpatMedRec_DataShare();
		}

		Loading load = null;

		#region Static Function

		public static bool s_blnAskForDelete()
		{
			return ShowQuestionMessageBox(clsHRPMessage.c_strAskForDelete) == DialogResult.Yes;
			//			return true;
		}
		public static bool s_blnAskForModify()
		{
			//			return ShowQuestionMessageBox(clsHRPMessage.c_strAskForModify) == DialogResult.Yes;
			return true;
		}
		public static DialogResult ShowInformationMessageBox(string strMessage)
		{
			return ShowInformationMessageBox(strMessage,MessageBoxButtons.OK);
		}

		public static DialogResult ShowInformationMessageBox(string strMessage,Form p_frmBase)
		{
			return ShowInformationMessageBox(strMessage,MessageBoxButtons.OK,p_frmBase);
		}

		public static DialogResult ShowInformationMessageBox(string strMessage,MessageBoxButtons buttons)
		{
			return MessageBox.Show(strMessage,"iCare",buttons,MessageBoxIcon.Information);
		}

		public static DialogResult ShowInformationMessageBox(string strMessage,MessageBoxButtons buttons,Form p_frmBase)
		{
			return MessageBox.Show(p_frmBase,strMessage,"iCare",buttons,MessageBoxIcon.Information);
		}

		public static DialogResult ShowQuestionMessageBox(string strMessage)
		{
			return ShowQuestionMessageBox(strMessage,MessageBoxButtons.YesNo);
		}

		public static DialogResult ShowQuestionMessageBox(string strMessage,MessageBoxButtons buttons)
		{
			return MessageBox.Show(strMessage,"iCare",buttons,MessageBoxIcon.Question);
		}

		public static DialogResult ShowQuestionMessageBox(IWin32Window p_owner,string strMessage)
		{
			return MessageBox.Show(p_owner,strMessage,"iCare",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
		}

		public static void s_mthShowNotPermitMessage()
		{
			ShowInformationMessageBox("�Բ�������Ȩ�޲��㣡");
		}

		public static DialogResult ShowDetailExceptionDialog(Exception err)
		{
			return ShowInformationMessageBox(err.Message.ToString() + "\r\n" + err.StackTrace.ToString());
		}
		
		private static bool m_blnNoNeedRemind = false;
		public static bool m_BlnNoNeedRemind
		{
			set{m_blnNoNeedRemind = value;}
		}
	
		
		#region ����ListView���п�����ʾ���������
		/// <summary>
		/// ����ʾ����������6ʱ����С���һ�еĿ�ȣ�����ʾ���������,Jacky-2003-7-21
		/// </summary>
		/// <param name="p_lsvControl"></param>
		public static void s_mthChangeListViewLastColumnWidth(ListView p_lsvControl)
		{
			if(p_lsvControl.Columns.Count>0)
			{
				int intLastColumnWidth=p_lsvControl.Width;
				for(int i=0;i<p_lsvControl.Columns.Count-1;i++)
				{
					intLastColumnWidth -= p_lsvControl.Columns[i].Width;
				}
				if(p_lsvControl.Items.Count>6)
					intLastColumnWidth -=18;

				p_lsvControl.Columns[p_lsvControl.Columns.Count-1].Width =intLastColumnWidth;
			}
		}
		/// <summary>
		/// ����ʾ����������ָ������ʱ����С���һ�еĿ�ȣ�����ʾ���������
		/// </summary>
		/// <param name="p_lsvControl"></param>
		/// <param name="p_intRows">ָ��������</param>
		public static void s_mthChangeListViewLastColumnWidth(ListView p_lsvControl,int p_intRows)
		{
			if(p_lsvControl.Columns.Count>0)
			{
				int intLastColumnWidth=p_lsvControl.Width;
				for(int i=0;i<p_lsvControl.Columns.Count-1;i++)
				{
					//ʹ���һ���ұ������߸պ������ұ߿�
					intLastColumnWidth -= p_lsvControl.Columns[i].Width;
				}
				if(p_lsvControl.Items.Count>p_intRows)
					intLastColumnWidth -=18;//18���ǹ������Ŀ��

				p_lsvControl.Columns[p_lsvControl.Columns.Count-1].Width =intLastColumnWidth;
			}
		}
		#endregion ����ListView���п�����ʾ���������

		#region  UserInfo

		
		public static string OperatorID
		{
			get
			{
				//return strOperatorID;
                return clsEMRLogin.LoginInfo.m_strEmpNo; 
			}
		}

		public static string OperatorName
		{
			get
			{
				//return strOperatorName;
                return clsEMRLogin.LoginInfo.m_strEmpName; 

			}
		}

		//liyi 2002-9-30 ���Ӳ���ʵ���Ļ�ȡ
		private static clsDepartment s_objDepartment = clsSystemContext.s_ObjCurrentContext.m_ObjDepartment;

		public static clsDepartment s_ObjDepartment
		{
			get
			{

				//return s_objDepartment;
				//return s_objCurrDepartment;
				//ת��Ϊ�� ����ʹ�� modified by tfzhang at 2005��10��14�� 12:02:43
				if (com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment==null)
					return null;
				return new clsDepartment(com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strSHORTNO_CHR,com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTNAME_VCHR);
			}
            set { s_objDepartment = value; }
		}

		#endregion		

	
	
		private static RecordDateTime.clsRecordDateTimeInfo s_objRecordDateTimeInfo = new RecordDateTime.clsRecordDateTimeInfo(""/*s_objDepartment.m_StrDeptID*/);
		/// <summary>
		/// ��¼������Ҫ����Ϣ
		/// </summary>
		public static RecordDateTime.clsRecordDateTimeInfo s_ObjRecordDateTimeInfo
		{
			get
			{
				return s_objRecordDateTimeInfo;
			}
		}

		/// <summary>
		/// ����ı༭״̬
		/// </summary>
		public enum enmFormEditStatus
		{
			/// <summary>
			/// ����
			/// </summary>
			AddNew,
			/// <summary>
			/// �޸�
			/// </summary>
			Modify,
			/// <summary>
			/// ���ܱ༭
			/// </summary>
			None
		}
		/// <summary>
		/// ���ݴ��嵱ǰ״̬�ı䴰�������Ϣ
		/// </summary>
		/// <param name="p_frmNeeded">����</param>
		/// <param name="p_enmCurrentStatus">��ǰ״̬</param>
		public static void m_mthChangeFormText(Form p_frmNeeded,enmFormEditStatus p_enmCurrentStatus)
		{
			string strAddNew = "������";
			string strModify = "���޸ģ�";

			switch(p_enmCurrentStatus)
			{
				case enmFormEditStatus.AddNew:
					if(!p_frmNeeded.Text.EndsWith(strAddNew))
					{
						if(p_frmNeeded.Text.EndsWith(strModify))
							p_frmNeeded.Text = p_frmNeeded.Text.Replace(strModify,strAddNew);
						else
							p_frmNeeded.Text += strAddNew;
					}
					break;
				case enmFormEditStatus.Modify:
					if(!p_frmNeeded.Text.EndsWith(strModify))
					{
						if(p_frmNeeded.Text.EndsWith(strAddNew))
							p_frmNeeded.Text = p_frmNeeded.Text.Replace(strAddNew,strModify);
						else
							p_frmNeeded.Text += strModify;
					}
					break;
				case enmFormEditStatus.None:
					if(p_frmNeeded.Text.EndsWith(strAddNew))
					{
						p_frmNeeded.Text = p_frmNeeded.Text.Replace(strAddNew,"");
					}
					else if(p_frmNeeded.Text.EndsWith(strModify))
					{
						p_frmNeeded.Text = p_frmNeeded.Text.Replace(strModify,"");
					}
					break;
			}
				
		}

		private static FormUtility.clsSaveCue s_objSaveCue = new FormUtility.clsSaveCue();
		/// <summary>
		/// ����ر�ʱ��ʾ����
		/// </summary>
		public static FormUtility.clsSaveCue s_ObjSaveCue
		{
			get
			{
				return s_objSaveCue;
			}
        }

		#region ICU
		/// <summary>
		/// ������ID
		/// </summary>
		private static int s_intReceiverID = 0;

		/// <summary>
		/// ��ȡΨһ�Ľ�����ID
		/// </summary>
		/// <returns></returns>
		public static int s_intGetReceiverID()
		{
			lock(typeof(MDIParent))
			{
				s_intReceiverID++;
				return s_intReceiverID;
			}			
		}
		/// <summary>
		/// ���ݽ�����
		/// </summary>
		//private static readonly clsICUDataTransReceiver s_objICUDataTransReceiver = new clsICUDataTransReceiver();

		/// <summary>
		/// ��ǽ������Ƿ��Ѿ�����
		/// </summary>
		private static bool s_blnOpenReceivered = false;

		/// <summary>
		/// ��ȡ�������Ƿ��Ѿ�����
		/// </summary>
		public static bool s_BlnReceiverConnected
		{
			get
			{
				return s_blnOpenReceivered;
			}
		}

		/// <summary>
		/// �ಥ��ַ
		/// </summary>
		private const string c_strMultiAdd = "224.1.1";

		/// <summary>
		/// ��ȡ�����ߡ�ע�⣬���ʹ��ǰ�Ȳ�ѯ�Ƿ��Ѿ����ӣ����Ҳ���ʹ�ùر����ӡ�
		/// </summary>
		//public static clsICUDataTransReceiver s_ObjICUReceiver
		//{
		//	get
		//	{
		//		if(!s_blnOpenReceivered)
		//		{
		//			clsICUDataTransReceiver.enmReceiverResult enmResult = s_objICUDataTransReceiver.m_enmConnect(c_strMultiAdd);

		//			if(enmResult == clsICUDataTransReceiver.enmReceiverResult.Operate_Succed)
		//			{
		//				s_blnOpenReceivered = true;
		//			}					
		//		}
				
		//		return s_objICUDataTransReceiver;
		//	}
		//}

		//public static void s_mthReconnetToServer()
		//{
		//	s_objICUDataTransReceiver.m_mthDisconnect();

		//	clsICUDataTransReceiver.enmReceiverResult enmResult = s_objICUDataTransReceiver.m_enmConnect(c_strMultiAdd);

		//	if(enmResult == clsICUDataTransReceiver.enmReceiverResult.Operate_Succed)
		//	{
		//		s_blnOpenReceivered = true;
		//	}		
		//}
		
	
		#endregion

		public static clsInpatMedRec_DataShare s_ObjInpatMedRec_DataShare
		{
			get
			{
				return s_objInpatMedRec_DataShare;
            }
		}
		#endregion Static Function



		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region ȫ�ֹ�������

		#region ����
		/// <summary>
		/// ��ǰ������Ϣ
		/// </summary>
		private static clsEmrDept_VO _objCurrentDeppartment;
		private static clsDepartment s_objCurrDepartment; 
		/// <summary>
		/// ��ǰ������Ϣ(����)
		/// </summary>
		public static clsEmrDept_VO m_objCurrentDepartment
		{
			get
			{
				//return _objCurrentDeppartment;
				return com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment;
			}
			set
			{
				_objCurrentDeppartment= value;
			}
		}
		public static clsDepartment s_ObjCurrDepartment
		{
			get 
			{
				//return s_objCurrDepartment;
				//ת��Ϊ�� ����ʹ�� modified by tfzhang at 2005��10��14�� 12:02:43
				if (com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment==null)
					return null;
				return new clsDepartment(com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strSHORTNO_CHR,com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTNAME_VCHR);

			}
			set 
			{
				s_objCurrDepartment=value;
				clsSystemContext.s_ObjCurrentContext.m_ObjDepartment = value;
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ��ǰ������Ϣ
		/// </summary>
		private static clsEmrDept_VO _objCurrentArea;
		private static clsInPatientArea s_objCurrInPatientArea;

		/// <summary>
		/// ��ǰ����(����)
		/// </summary>
		public static clsEmrDept_VO m_objCurrentArea
		{
			get
			{
				//return _objCurrentArea;
				return com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea;
			}
			set
			{
				_objCurrentArea= value;
			}
		}
		public static clsInPatientArea s_ObjCurrInPatientArea
		{
			get 
			{
				//return s_objCurrInPatientArea;
				//ת��Ϊ���� ����ʹ�� modified by tfzhang at 2005��10��14�� 12:04:25
				if (com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea==null)
					return null;
				return new clsInPatientArea(com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea.m_strSHORTNO_CHR,com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea.m_strDEPTNAME_VCHR,com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea.m_strDEPTID_CHR);

			}
			set {s_objCurrInPatientArea=value;}
		}
		#endregion

		#region ����
        private static bool m_blnNoSetOldPateint = false;
		/// <summary>
		/// �û�ѡ��Ĳ���,���Դ˲���Ϊ����������
		/// </summary>
		private static clsEmrInBedPatient_VO _objCurrentPatient;
		private static clsPatient s_objCurrentPatient = null;
		/// <summary>
		/// �û�ѡ��Ĳ���,���Դ˲���Ϊ����������
		/// </summary>
		public static clsEmrInBedPatient_VO m_objCurrentPatient
		{
			get
			{
				//return  _objCurrentPatient;
				return com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient;
			}
			set
			{
				_objCurrentPatient= value;
                com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentPatient = value;
                m_blnNoSetOldPateint = true;
                if (_objCurrentPatient != null)
                {
                    s_objCurrentPatient = new clsPatient(_objCurrentPatient.m_strINPATIENTID_CHR, _objCurrentPatient.m_strPATIENTID_CHR, _objCurrentPatient.m_strDEPTID_CHR, _objCurrentPatient.m_strAREAID_CHR, _objCurrentPatient.m_strCODE_CHR, _objCurrentPatient.m_strEXTENDID_VCHR, _objCurrentPatient.m_strEMRInPatientID, _objCurrentPatient.m_strHISInPatientID);
                    s_objCurrentPatient.m_IntCharacter = _objCurrentPatient.m_intCharacter;
                    s_objCurrentPatient.m_DtmSelectedHISInDate = _objCurrentPatient.m_dtmHISInDate;
                    s_objCurrentPatient.m_DtmSelectedInDate = _objCurrentPatient.m_dtmEMRInDate;
                    s_objCurrentPatient.m_StrRegisterId = _objCurrentPatient.m_strREGISTERID_CHR;
                }
                m_blnNoSetOldPateint = false;
                #region ����״̬����ǰ����
				if(_objCurrentPatient != null && clsEMRLogin.s_FrmMDI != null)
				{
					Type objType = clsEMRLogin.s_FrmMDI.GetType();
					MethodInfo mi = objType.GetMethod("m_mthSetCurPatient");
                    if (mi != null)
                    {
                        mi.Invoke(clsEMRLogin.s_FrmMDI, new string[] { "��ǰ����:  " + _objCurrentPatient.m_strLASTNAME_VCHR.Trim() + "   " + _objCurrentPatient.m_strCODE_CHR.Trim() + "��" });
                    }
                    PropertyInfo pi = objType.GetProperty("m_StrEmrSelectPatient");
					if(pi != null)
					{
						pi.SetValue(clsEMRLogin.s_FrmMDI,_objCurrentPatient.m_strEXTENDID_VCHR,null);
					}
                    PropertyInfo pi2 = objType.GetProperty("m_StrPatientID");
                    if (pi2 != null)
                    {
                        pi2.SetValue(clsEMRLogin.s_FrmMDI, _objCurrentPatient.m_strPATIENTID_CHR, null);
                    }

				}
				#endregion

			}
		}
	
		public static clsPatient s_ObjCurrentPatient
		{
			get
			{
                //return s_objCurrentPatient;

                clsEmrInBedPatient_VO objP = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient;             
                
                if (s_objCurrentPatient == null)
                {
                    if (objP == null)
                        return null;
                    s_objCurrentPatient = new clsPatient(true, objP.m_strREGISTERID_CHR);
                    return s_objCurrentPatient;
                }
                else
                {
                    //s_objCurrentPatient��Ϊ�գ���com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatientҲ��Ϊ��
                    if (s_objCurrentPatient.m_StrPatientID != objP.m_strPATIENTID_CHR)
                    {
                        //return new clsPatient(objP.m_strINPATIENTID_CHR, objP.m_strPATIENTID_CHR, objP.m_strDEPTID_CHR, objP.m_strAREAID_CHR, objP.m_strCODE_CHR);
                        s_objCurrentPatient = new clsPatient(true, objP.m_strREGISTERID_CHR);
                        return s_objCurrentPatient;
                    }
                    else
                    {
                        return s_objCurrentPatient;
                    }
                }
			}
			set
			{
				s_objCurrentPatient = value;

				#region ����״̬����ǰ����
                if (s_objCurrentPatient != null && clsEMRLogin.s_FrmMDI != null && !m_blnNoSetOldPateint)
				{
                    new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain().m_lngGetMinPatinetInfoByIdAndDate(s_objCurrentPatient.m_StrEMRInPatientID, s_objCurrentPatient.m_DtmSelectedInDate, out _objCurrentPatient, com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001");
                    com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient = _objCurrentPatient;
                    com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentPatient = _objCurrentPatient;
					Type objType = clsEMRLogin.s_FrmMDI.GetType();
					MethodInfo mi = objType.GetMethod("m_mthSetCurPatient");
                    if (mi != null)
                    {
                        //mi.Invoke(clsEMRLogin.s_FrmMDI,new string[]{"��ǰ����:  "+s_objCurrentPatient.m_StrName+"   "+s_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName+"��"});
                        mi.Invoke(clsEMRLogin.s_FrmMDI, new string[] { "��ǰ����:  " + s_objCurrentPatient.m_StrName + "   " + s_objCurrentPatient.m_strBedCode + "��" });
                    }
                    PropertyInfo pi = objType.GetProperty("m_StrEmrSelectPatient");
					if(pi != null)
					{
						pi.SetValue(clsEMRLogin.s_FrmMDI,s_objCurrentPatient.m_StrExtendPatientID,null);
                    }
                    PropertyInfo pi2 = objType.GetProperty("m_StrPatientID");
                    if (pi2 != null)
                    {
                        pi2.SetValue(clsEMRLogin.s_FrmMDI, s_objCurrentPatient.m_StrPatientID, null);
                    }
				}
				#endregion
			}
		}
        public string m_strSetPatientByExtendIDForDeptOnly
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    clsEmrInBedPatient_VO obj = new clsEmrInBedPatient_VO();
                    new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain().m_lngGetMinPatinetInfoByDeptAndExtendID(value, out obj);
                    if (obj != null)
                        m_objCurrentPatient = obj;
                    obj = null;
                }
            }
        }
		#endregion �Բ���Ϊ��������

		#region �쳣�ļ�·��
		public static string s_strErrorFilePath
		{
			get
			{
				return @"d:\code\log.txt";
			}

		}
		#endregion

		#endregion ����

		#region Ĭ��ǩ��
        private static bool m_blnIsShowLevel()
        {
            string strReturnValue = string.Empty;

            //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objServ =
            //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsPublicMiddleTier_m_lngGetConfigBySettingID("3015", out strReturnValue);
            //objServ = null;
            if (strReturnValue == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		/// <summary>
		/// Ĭ��ǩ��
		/// </summary>
		/// <param name="p_ctl"></param>
		public static void m_mthSetDefaulEmployee(Control p_ctl)
		{
            bool IsShowLevel = m_blnIsShowLevel();
			switch(p_ctl.GetType().Name)
			{
				case "TextBox" :
                    if (IsShowLevel)
                    {
                        p_ctl.Text = clsEMRLogin.LoginEmployee.m_strGetTechnicalRankAndName;//.m_strTECHNICALRANK_CHR.Trim() + " " + clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR.Trim();// +"(" + clsEMRLogin.LoginEmployee.m_strTECHNICALRANK_CHR.Trim();
                        clsEMRLogin.LoginEmployee.m_blnIsShowTechnicalRank = true;
                    }
                    else
                    {
                        p_ctl.Text = clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR;
                        clsEMRLogin.LoginEmployee.m_blnIsShowTechnicalRank = false;
                    }                    
					p_ctl.Tag = clsEMRLogin.LoginEmployee;
					p_ctl.Enabled = true;
					break;
				case "ListView" :
					ListView lsvSign = (ListView)p_ctl;
					lsvSign.Items.Clear();
					//Ĭ��ǩ��
                    ListViewItem lviNewItem = null;// + "(" + clsEMRLogin.LoginEmployee.m_strTECHNICALRANK_CHR.Trim() );
                    if (IsShowLevel)
                    {
                        lviNewItem = new ListViewItem(clsEMRLogin.LoginEmployee.m_strGetTechnicalRankAndName);
                        clsEMRLogin.LoginEmployee.m_blnIsShowTechnicalRank = true;
                    }
                    else
                    {
                        lviNewItem = new ListViewItem(clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR);
                        clsEMRLogin.LoginEmployee.m_blnIsShowTechnicalRank = false;
                    }
                    //ID ����ظ���
					lviNewItem.SubItems.Add(clsEMRLogin.LoginEmployee.m_strEMPID_CHR);
					//���� ������
					lviNewItem.SubItems.Add(clsEMRLogin.LoginEmployee.m_strLEVEL_CHR);
					//tag��Ϊ����
					lviNewItem.Tag=clsEMRLogin.LoginEmployee;
					lsvSign.Items.Add(lviNewItem);
					break;
			}
		}
		#endregion  

		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent));
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel4 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
            this.timMDI = new System.Windows.Forms.Timer(this.components);
            this.mnuMDI = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuOpenExplorer = new System.Windows.Forms.MenuItem();
            this.mnuSave = new System.Windows.Forms.MenuItem();
            this.mnuDel = new System.Windows.Forms.MenuItem();
            this.mnuPrintPriview = new System.Windows.Forms.MenuItem();
            this.mnuPrint = new System.Windows.Forms.MenuItem();
            this.mnuSplitter1 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuCut = new System.Windows.Forms.MenuItem();
            this.mnuPaste = new System.Windows.Forms.MenuItem();
            this.mnuSplitter = new System.Windows.Forms.MenuItem();
            this.mnuUndo = new System.Windows.Forms.MenuItem();
            this.mnuRedo = new System.Windows.Forms.MenuItem();
            this.mnuDoctorWorkStationForEar = new System.Windows.Forms.MenuItem();
            this.mnuInPatientCaseHistory = new System.Windows.Forms.MenuItem();
            this.mnuPatientProcessRecord = new System.Windows.Forms.MenuItem();
            this.mnuOperationSummary = new System.Windows.Forms.MenuItem();
            this.mnuOperationRecordDoct = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.mnuInPatientCaseMode2 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.mnuOperationAgreed = new System.Windows.Forms.MenuItem();
            this.mnufrmPICUShiftInForm = new System.Windows.Forms.MenuItem();
            this.mnufrmPICUShiftOutForm = new System.Windows.Forms.MenuItem();
            this.mnuMainRecord = new System.Windows.Forms.MenuItem();
            this.mnuQC = new System.Windows.Forms.MenuItem();
            this.mnuApplyRecord = new System.Windows.Forms.MenuItem();
            this.mnuBultransonic = new System.Windows.Forms.MenuItem();
            this.mnuCTCheckOrder = new System.Windows.Forms.MenuItem();
            this.mnuXRay = new System.Windows.Forms.MenuItem();
            this.mnuSPECT = new System.Windows.Forms.MenuItem();
            this.mnuHighOxygen = new System.Windows.Forms.MenuItem();
            this.mnuPathologyOrgCheckOrder = new System.Windows.Forms.MenuItem();
            this.mnuMRIOrder = new System.Windows.Forms.MenuItem();
            this.mniEKGOrder = new System.Windows.Forms.MenuItem();
            this.mniNuclearOrder = new System.Windows.Forms.MenuItem();
            this.mniPSGOrder = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mniLabCheckReport = new System.Windows.Forms.MenuItem();
            this.mniImageReport = new System.Windows.Forms.MenuItem();
            this.mniImageBookingSearch = new System.Windows.Forms.MenuItem();
            this.mnuGrade = new System.Windows.Forms.MenuItem();
            this.mnuSIRS = new System.Windows.Forms.MenuItem();
            this.mnuGlasgow = new System.Windows.Forms.MenuItem();
            this.mnuLung = new System.Windows.Forms.MenuItem();
            this.mnuNewBaby = new System.Windows.Forms.MenuItem();
            this.mnuLittleBaby = new System.Windows.Forms.MenuItem();
            this.mnuAPACHEII = new System.Windows.Forms.MenuItem();
            this.mnuAPACHEIII = new System.Windows.Forms.MenuItem();
            this.mnuTISS28 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuNurseWorkStationForEar = new System.Windows.Forms.MenuItem();
            this.mnuEvaluate = new System.Windows.Forms.MenuItem();
            this.mnuThreeMeasureRecord = new System.Windows.Forms.MenuItem();
            this.mnuGeneralTenda = new System.Windows.Forms.MenuItem();
            this.mnuWatchItem = new System.Windows.Forms.MenuItem();
            this.mnuTendRecord = new System.Windows.Forms.MenuItem();
            this.mnuICUIntensiveTend = new System.Windows.Forms.MenuItem();
            this.mnuICUTendRecord = new System.Windows.Forms.MenuItem();
            this.mnuOperationRecord = new System.Windows.Forms.MenuItem();
            this.mnuOperationQty = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.mnuReport = new System.Windows.Forms.MenuItem();
            this.mnuiCareTools = new System.Windows.Forms.MenuItem();
            this.mnuAssTools = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.mniPatientLabel = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.m_mniArchiving = new System.Windows.Forms.MenuItem();
            this.m_mnuDebug = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.mnuCommonUse = new System.Windows.Forms.MenuItem();
            this.m_mniTemplate = new System.Windows.Forms.MenuItem();
            this.m_mniAddNewTemplate = new System.Windows.Forms.MenuItem();
            this.m_mniAddNewCommonUseTemplate = new System.Windows.Forms.MenuItem();
            this.m_mniTemplateManage = new System.Windows.Forms.MenuItem();
            this.m_mniDefaultValue = new System.Windows.Forms.MenuItem();
            this.m_mniSaveDefaultValue = new System.Windows.Forms.MenuItem();
            this.m_mniResetDefaultValue = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.mnuSpectialSymbol = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.m_mniManageExplorer = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mniManageDept = new System.Windows.Forms.MenuItem();
            this.mniManageDocAndNur = new System.Windows.Forms.MenuItem();
            this.mniOutExpertManage = new System.Windows.Forms.MenuItem();
            this.m_mniEmployeeMove = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.m_mniIntelligentStatics = new System.Windows.Forms.MenuItem();
            this.mnuTemplateTools = new System.Windows.Forms.MenuItem();
            this.mnuEightSystem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuWindows = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuEmail = new System.Windows.Forms.MenuItem();
            this.mnuSplitter6 = new System.Windows.Forms.MenuItem();
            this.mnuVersion = new System.Windows.Forms.MenuItem();
            this.mnuDebuTool = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.timCount = new System.Windows.Forms.Timer(this.components);
            this.timRevisitRemind = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusBar.Location = new System.Drawing.Point(0, 545);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2,
            this.statusBarPanel4,
            this.statusBarPanel3});
            this.StatusBar.ShowPanels = true;
            this.StatusBar.Size = new System.Drawing.Size(1030, 22);
            this.StatusBar.TabIndex = 3;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 280;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 280;
            // 
            // statusBarPanel4
            // 
            this.statusBarPanel4.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.statusBarPanel4.Name = "statusBarPanel4";
            this.statusBarPanel4.Width = 215;
            // 
            // statusBarPanel3
            // 
            this.statusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.statusBarPanel3.Name = "statusBarPanel3";
            this.statusBarPanel3.Width = 250;
            // 
            // timMDI
            // 
            this.timMDI.Enabled = true;
            this.timMDI.Interval = 1000;
            // 
            // mnuMDI
            // 
            this.mnuMDI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuDoctorWorkStationForEar,
            this.mnuNurseWorkStationForEar,
            this.menuItem10,
            this.mnuReport,
            this.mnuiCareTools,
            this.mnuAssTools,
            this.mnuWindows,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpenExplorer,
            this.mnuSave,
            this.mnuDel,
            this.mnuPrintPriview,
            this.mnuPrint,
            this.mnuSplitter1,
            this.mnuExit});
            this.mnuFile.Text = "��  ��";
            // 
            // mnuOpenExplorer
            // 
            this.mnuOpenExplorer.Index = 0;
            this.mnuOpenExplorer.Text = "iCare Explorer";
            this.mnuOpenExplorer.Click += new System.EventHandler(this.mnuOpenExplorer_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Index = 1;
            this.mnuSave.Text = "��       ��";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuDel
            // 
            this.mnuDel.Index = 2;
            this.mnuDel.Text = "ɾ       ��";
            this.mnuDel.Click += new System.EventHandler(this.mnuDel_Click);
            // 
            // mnuPrintPriview
            // 
            this.mnuPrintPriview.Index = 3;
            this.mnuPrintPriview.Text = "��ӡԤ��";
            this.mnuPrintPriview.Click += new System.EventHandler(this.mnuPrintPriview_Click);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Index = 4;
            this.mnuPrint.Text = "��       ӡ";
            this.mnuPrint.Visible = false;
            // 
            // mnuSplitter1
            // 
            this.mnuSplitter1.Index = 5;
            this.mnuSplitter1.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 6;
            this.mnuExit.Text = "��       ��";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCopy,
            this.mnuCut,
            this.mnuPaste,
            this.mnuSplitter,
            this.mnuUndo,
            this.mnuRedo});
            this.mnuEdit.Text = "��  ��";
            this.mnuEdit.Visible = false;
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 0;
            this.mnuCopy.Text = "��  ��";
            // 
            // mnuCut
            // 
            this.mnuCut.Index = 1;
            this.mnuCut.Text = "��  ��";
            // 
            // mnuPaste
            // 
            this.mnuPaste.Index = 2;
            this.mnuPaste.Text = "ճ  ��";
            // 
            // mnuSplitter
            // 
            this.mnuSplitter.Index = 3;
            this.mnuSplitter.Text = "-";
            // 
            // mnuUndo
            // 
            this.mnuUndo.Index = 4;
            this.mnuUndo.Text = "��  ��";
            // 
            // mnuRedo
            // 
            this.mnuRedo.Index = 5;
            this.mnuRedo.Text = "��  ��";
            // 
            // mnuDoctorWorkStationForEar
            // 
            this.mnuDoctorWorkStationForEar.Enabled = false;
            this.mnuDoctorWorkStationForEar.Index = 2;
            this.mnuDoctorWorkStationForEar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInPatientCaseHistory,
            this.mnuPatientProcessRecord,
            this.mnuOperationSummary,
            this.mnuOperationRecordDoct,
            this.menuItem16,
            this.menuItem47,
            this.menuItem15,
            this.mnuApplyRecord,
            this.mnuGrade,
            this.menuItem5});
            this.mnuDoctorWorkStationForEar.Text = "ҽ������վ";
            this.mnuDoctorWorkStationForEar.Popup += new System.EventHandler(this.mnuDoctorWorkStationForEar_Popup);
            // 
            // mnuInPatientCaseHistory
            // 
            this.mnuInPatientCaseHistory.Index = 0;
            this.mnuInPatientCaseHistory.Text = "סԺ����";
            this.mnuInPatientCaseHistory.Click += new System.EventHandler(this.mnuInPatientCaseHistory_Click);
            // 
            // mnuPatientProcessRecord
            // 
            this.mnuPatientProcessRecord.Index = 1;
            this.mnuPatientProcessRecord.Text = "���̼�¼";
            this.mnuPatientProcessRecord.Click += new System.EventHandler(this.mnuPatientProcessRecord_Click);
            // 
            // mnuOperationSummary
            // 
            this.mnuOperationSummary.Index = 2;
            this.mnuOperationSummary.Text = "��ǰС��";
            this.mnuOperationSummary.Click += new System.EventHandler(this.mnuOperationSummary_Click);
            // 
            // mnuOperationRecordDoct
            // 
            this.mnuOperationRecordDoct.Index = 3;
            this.mnuOperationRecordDoct.Text = "������¼��";
            this.mnuOperationRecordDoct.Click += new System.EventHandler(this.mnuOperationRecordDoct_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 4;
            this.menuItem16.Text = "��Ժ��¼";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 5;
            this.menuItem47.Text = "-";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 6;
            this.menuItem15.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInPatientCaseMode2,
            this.menuItem25,
            this.mnuOperationAgreed,
            this.mnufrmPICUShiftInForm,
            this.mnufrmPICUShiftOutForm,
            this.mnuMainRecord,
            this.mnuQC});
            this.menuItem15.Text = "��������";
            // 
            // mnuInPatientCaseMode2
            // 
            this.mnuInPatientCaseMode2.Index = 0;
            this.mnuInPatientCaseMode2.Text = "סԺ����ģʽ2";
            this.mnuInPatientCaseMode2.Visible = false;
            this.mnuInPatientCaseMode2.Click += new System.EventHandler(this.mnuInPatientCaseMode2_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 1;
            this.menuItem25.Text = "�����¼";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // mnuOperationAgreed
            // 
            this.mnuOperationAgreed.Index = 2;
            this.mnuOperationAgreed.Text = "����֪��ͬ����";
            this.mnuOperationAgreed.Click += new System.EventHandler(this.mnuOperationAgreed_Click);
            // 
            // mnufrmPICUShiftInForm
            // 
            this.mnufrmPICUShiftInForm.Index = 3;
            this.mnufrmPICUShiftInForm.Text = "ICUת���¼";
            this.mnufrmPICUShiftInForm.Click += new System.EventHandler(this.mnufrmPICUShiftInForm_Click);
            // 
            // mnufrmPICUShiftOutForm
            // 
            this.mnufrmPICUShiftOutForm.Index = 4;
            this.mnufrmPICUShiftOutForm.Text = "ICUת����¼";
            this.mnufrmPICUShiftOutForm.Click += new System.EventHandler(this.mnufrmPICUShiftOutForm_Click);
            // 
            // mnuMainRecord
            // 
            this.mnuMainRecord.Index = 5;
            this.mnuMainRecord.Text = "סԺ������ҳ";
            this.mnuMainRecord.Click += new System.EventHandler(this.mnuMainRecord_Click);
            // 
            // mnuQC
            // 
            this.mnuQC.Index = 6;
            this.mnuQC.Text = "�����������ֱ�";
            this.mnuQC.Click += new System.EventHandler(this.mnuQC_Click);
            // 
            // mnuApplyRecord
            // 
            this.mnuApplyRecord.Index = 7;
            this.mnuApplyRecord.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuBultransonic,
            this.mnuCTCheckOrder,
            this.mnuXRay,
            this.mnuSPECT,
            this.mnuHighOxygen,
            this.mnuPathologyOrgCheckOrder,
            this.mnuMRIOrder,
            this.mniEKGOrder,
            this.mniNuclearOrder,
            this.mniPSGOrder,
            this.menuItem8,
            this.mniLabCheckReport,
            this.mniImageReport,
            this.mniImageBookingSearch});
            this.mnuApplyRecord.Text = "��  ��  ��";
            // 
            // mnuBultransonic
            // 
            this.mnuBultransonic.Index = 0;
            this.mnuBultransonic.Text = "B�ͳ������������뵥";
            this.mnuBultransonic.Click += new System.EventHandler(this.mnuBultransonic_Click);
            // 
            // mnuCTCheckOrder
            // 
            this.mnuCTCheckOrder.Index = 1;
            this.mnuCTCheckOrder.Text = "CT������뵥";
            this.mnuCTCheckOrder.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // mnuXRay
            // 
            this.mnuXRay.Index = 2;
            this.mnuXRay.Text = "X�����뵥";
            this.mnuXRay.Click += new System.EventHandler(this.mnuXRay_Click);
            // 
            // mnuSPECT
            // 
            this.mnuSPECT.Index = 3;
            this.mnuSPECT.Text = "SPECT������뵥";
            this.mnuSPECT.Click += new System.EventHandler(this.mnuSPECT_Click);
            // 
            // mnuHighOxygen
            // 
            this.mnuHighOxygen.Index = 4;
            this.mnuHighOxygen.Text = "��ѹ���������뵥";
            this.mnuHighOxygen.Click += new System.EventHandler(this.mnuHighOxygen_Click);
            // 
            // mnuPathologyOrgCheckOrder
            // 
            this.mnuPathologyOrgCheckOrder.Index = 5;
            this.mnuPathologyOrgCheckOrder.Text = "���������֯�ͼ쵥";
            this.mnuPathologyOrgCheckOrder.Click += new System.EventHandler(this.mnuPathologyOrgCheckOrder_Click);
            // 
            // mnuMRIOrder
            // 
            this.mnuMRIOrder.Index = 6;
            this.mnuMRIOrder.Text = "MRI���뵥";
            this.mnuMRIOrder.Click += new System.EventHandler(this.mnuMRIOrder_Click);
            // 
            // mniEKGOrder
            // 
            this.mniEKGOrder.Index = 7;
            this.mniEKGOrder.Text = "�ĵ�ͼ���뵥";
            this.mniEKGOrder.Click += new System.EventHandler(this.mniEKGOrder_Click);
            // 
            // mniNuclearOrder
            // 
            this.mniNuclearOrder.Index = 8;
            this.mniNuclearOrder.Text = "���Զർ˯��ͼ������뵥";
            this.mniNuclearOrder.Click += new System.EventHandler(this.mniNuclearOrder_Click);
            // 
            // mniPSGOrder
            // 
            this.mniPSGOrder.Index = 9;
            this.mniPSGOrder.Text = "��ҽѧ������뵥";
            this.mniPSGOrder.Click += new System.EventHandler(this.mniPSGOrder_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 10;
            this.menuItem8.Text = "ʵ���Ҽ������뵥";
            this.menuItem8.Visible = false;
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // mniLabCheckReport
            // 
            this.mniLabCheckReport.Index = 11;
            this.mniLabCheckReport.Text = "ʵ���Ҽ��鱨�浥";
            this.mniLabCheckReport.Visible = false;
            this.mniLabCheckReport.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // mniImageReport
            // 
            this.mniImageReport.Index = 12;
            this.mniImageReport.Text = "Ӱ�񱨸浥";
            this.mniImageReport.Visible = false;
            this.mniImageReport.Click += new System.EventHandler(this.menuItem42_Click);
            // 
            // mniImageBookingSearch
            // 
            this.mniImageBookingSearch.Index = 13;
            this.mniImageBookingSearch.Text = "Ӱ��ԤԼ��ѯ";
            this.mniImageBookingSearch.Visible = false;
            this.mniImageBookingSearch.Click += new System.EventHandler(this.mniImageBookingSearch_Click);
            // 
            // mnuGrade
            // 
            this.mnuGrade.Index = 8;
            this.mnuGrade.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSIRS,
            this.mnuGlasgow,
            this.mnuLung,
            this.mnuNewBaby,
            this.mnuLittleBaby,
            this.mnuAPACHEII,
            this.mnuAPACHEIII,
            this.mnuTISS28});
            this.mnuGrade.Text = "��������ϵͳ";
            // 
            // mnuSIRS
            // 
            this.mnuSIRS.Index = 0;
            this.mnuSIRS.Text = "SIRS�������";
            this.mnuSIRS.Click += new System.EventHandler(this.mnuSIRS_Click);
            // 
            // mnuGlasgow
            // 
            this.mnuGlasgow.Index = 1;
            this.mnuGlasgow.Text = "����Glasgow��������";
            this.mnuGlasgow.Click += new System.EventHandler(this.mnuGlasgow_Click);
            // 
            // mnuLung
            // 
            this.mnuLung.Index = 2;
            this.mnuLung.Text = "���Է���������";
            this.mnuLung.Click += new System.EventHandler(this.mnuLung_Click);
            // 
            // mnuNewBaby
            // 
            this.mnuNewBaby.Index = 3;
            this.mnuNewBaby.Text = "������Σ�ز�������";
            this.mnuNewBaby.Click += new System.EventHandler(this.mnuNewBaby_Click);
            // 
            // mnuLittleBaby
            // 
            this.mnuLittleBaby.Index = 4;
            this.mnuLittleBaby.Text = "С��Σ�ز�������";
            this.mnuLittleBaby.Click += new System.EventHandler(this.mnuLittleBaby_Click);
            // 
            // mnuAPACHEII
            // 
            this.mnuAPACHEII.Index = 5;
            this.mnuAPACHEII.Text = "APACHEII ����";
            this.mnuAPACHEII.Click += new System.EventHandler(this.mnuAPACHEII_Click);
            // 
            // mnuAPACHEIII
            // 
            this.mnuAPACHEIII.Index = 6;
            this.mnuAPACHEIII.Text = "APACHEIII ����";
            this.mnuAPACHEIII.Click += new System.EventHandler(this.mnuAPACHEIII_Click);
            // 
            // mnuTISS28
            // 
            this.mnuTISS28.Index = 7;
            this.mnuTISS28.Text = "TISS-28����";
            this.mnuTISS28.Click += new System.EventHandler(this.mnuTISS28_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 9;
            this.menuItem5.Text = "���Ʒ���";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // mnuNurseWorkStationForEar
            // 
            this.mnuNurseWorkStationForEar.Enabled = false;
            this.mnuNurseWorkStationForEar.Index = 3;
            this.mnuNurseWorkStationForEar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEvaluate,
            this.mnuThreeMeasureRecord,
            this.mnuGeneralTenda,
            this.mnuWatchItem,
            this.mnuTendRecord,
            this.mnuICUIntensiveTend,
            this.mnuICUTendRecord,
            this.mnuOperationRecord,
            this.mnuOperationQty,
            this.menuItem35});
            this.mnuNurseWorkStationForEar.Text = "��ʿ����վ";
            this.mnuNurseWorkStationForEar.Popup += new System.EventHandler(this.mnuNurseWorkStationForEar_Popup);
            // 
            // mnuEvaluate
            // 
            this.mnuEvaluate.Index = 0;
            this.mnuEvaluate.Text = "��Ժ��������";
            this.mnuEvaluate.Click += new System.EventHandler(this.mnuEvaluate_Click);
            // 
            // mnuThreeMeasureRecord
            // 
            this.mnuThreeMeasureRecord.Index = 1;
            this.mnuThreeMeasureRecord.Text = "�� �� ��";
            this.mnuThreeMeasureRecord.Click += new System.EventHandler(this.mnuThreeMeasureRecord_Click);
            // 
            // mnuGeneralTenda
            // 
            this.mnuGeneralTenda.Index = 2;
            this.mnuGeneralTenda.Text = "һ�㻤���¼";
            this.mnuGeneralTenda.Click += new System.EventHandler(this.mnuGeneralTenda_Click);
            // 
            // mnuWatchItem
            // 
            this.mnuWatchItem.Index = 3;
            this.mnuWatchItem.Text = "�۲���Ŀ��¼��";
            this.mnuWatchItem.Click += new System.EventHandler(this.mnuWatchItem_Click);
            // 
            // mnuTendRecord
            // 
            this.mnuTendRecord.Index = 4;
            this.mnuTendRecord.Text = "Σ�ػ��߻����¼";
            this.mnuTendRecord.Click += new System.EventHandler(this.mnuTendRecord_Click);
            // 
            // mnuICUIntensiveTend
            // 
            this.mnuICUIntensiveTend.Index = 5;
            this.mnuICUIntensiveTend.Text = "Σ��֢�໤�����ػ���¼��";
            this.mnuICUIntensiveTend.Click += new System.EventHandler(this.mnuICUIntensiveTend_Click);
            // 
            // mnuICUTendRecord
            // 
            this.mnuICUTendRecord.Index = 6;
            this.mnuICUTendRecord.Text = "ICUΣ�ػ��߻����¼";
            this.mnuICUTendRecord.Visible = false;
            this.mnuICUTendRecord.Click += new System.EventHandler(this.mnuICUTendRecord_Click);
            // 
            // mnuOperationRecord
            // 
            this.mnuOperationRecord.Index = 7;
            this.mnuOperationRecord.Text = "���������¼";
            this.mnuOperationRecord.Click += new System.EventHandler(this.mnuOperationRecord_Click_1);
            // 
            // mnuOperationQty
            // 
            this.mnuOperationQty.Index = 8;
            this.mnuOperationQty.Text = "������е�����ϵ�����";
            this.mnuOperationQty.Click += new System.EventHandler(this.mnuOperationQty_Click);
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 9;
            this.menuItem35.Text = "����ICU���������Ƽ໤��¼��";
            this.menuItem35.Click += new System.EventHandler(this.menuItem35_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem21,
            this.menuItem9,
            this.menuItem42,
            this.menuItem11,
            this.menuItem22,
            this.menuItem23,
            this.menuItem13,
            this.menuItem12,
            this.menuItem14,
            this.menuItem17,
            this.menuItem18,
            this.menuItem27,
            this.menuItem28,
            this.menuItem46});
            this.menuItem10.Text = "��������ϵͳ";
            this.menuItem10.Visible = false;
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 0;
            this.menuItem21.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuItem19});
            this.menuItem21.Text = "�������Ź���";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "�������Ա����";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 1;
            this.menuItem19.Text = "��������Ա����";
            this.menuItem19.Visible = false;
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem37,
            this.menuItem38,
            this.menuItem39,
            this.menuItem40});
            this.menuItem9.Text = "����ά������";
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 0;
            this.menuItem37.Text = "�����Ҽ໤�ǰ���";
            this.menuItem37.Click += new System.EventHandler(this.menuItem37_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 1;
            this.menuItem38.Text = "һ������Ʒ����";
            this.menuItem38.Click += new System.EventHandler(this.menuItem38_Click);
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 2;
            this.menuItem39.Text = "������Ʒ����";
            this.menuItem39.Click += new System.EventHandler(this.menuItem39_Click);
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 3;
            this.menuItem40.Text = "����ҩƷ����";
            this.menuItem40.Click += new System.EventHandler(this.menuItem40_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 2;
            this.menuItem42.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem24,
            this.menuItem30});
            this.menuItem42.Text = "����ͳ��";
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 0;
            this.menuItem24.Text = "ÿ����ʹ����һ����";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 1;
            this.menuItem30.Text = "��ʱ��ͳ�Ʊ���";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 3;
            this.menuItem11.Text = "����֪ͨ��";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 4;
            this.menuItem22.Text = "�������Ű൥";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 5;
            this.menuItem23.Text = "������Ű൥";
            this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 6;
            this.menuItem13.Text = "����ƻ�";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 7;
            this.menuItem12.Text = "����֪��ͬ����";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 8;
            this.menuItem14.Text = "�������вɼ�";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 9;
            this.menuItem17.Text = "����ǰ����";
            this.menuItem17.Visible = false;
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 10;
            this.menuItem18.Text = "PCA��ʹ�ǼǱ�";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 11;
            this.menuItem27.Text = "����ָ�PAR��¼��";
            this.menuItem27.Visible = false;
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 12;
            this.menuItem28.Text = "�����¼���ع�";
            this.menuItem28.Visible = false;
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 13;
            this.menuItem46.Text = "����ϵͳ������";
            this.menuItem46.Click += new System.EventHandler(this.frmAnaFormContainer_Click);
            // 
            // mnuReport
            // 
            this.mnuReport.Index = 5;
            this.mnuReport.Text = "����ͳ��";
            this.mnuReport.Visible = false;
            // 
            // mnuiCareTools
            // 
            this.mnuiCareTools.Index = 6;
            this.mnuiCareTools.Text = "iCare����";
            this.mnuiCareTools.Visible = false;
            // 
            // mnuAssTools
            // 
            this.mnuAssTools.Index = 7;
            this.mnuAssTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem41,
            this.menuItem31,
            this.mniPatientLabel,
            this.menuItem26,
            this.m_mniArchiving,
            this.m_mnuDebug,
            this.menuItem44,
            this.mnuCommonUse,
            this.m_mniTemplate,
            this.m_mniDefaultValue,
            this.menuItem45,
            this.mnuSpectialSymbol,
            this.menuItem1,
            this.menuItem6,
            this.m_mniManageExplorer,
            this.menuItem2,
            this.mniManageDept,
            this.menuItem50,
            this.menuItem48,
            this.menuItem43});
            this.mnuAssTools.Text = "��������";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 0;
            this.menuItem41.Text = "����¼��ѯ";
            this.menuItem41.Click += new System.EventHandler(this.menuItem41_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 1;
            this.menuItem31.Text = "ɾ����¼��ѯ";
            this.menuItem31.Click += new System.EventHandler(this.menuItem31_Click);
            // 
            // mniPatientLabel
            // 
            this.mniPatientLabel.Index = 2;
            this.mniPatientLabel.Text = "������ǩ";
            this.mniPatientLabel.Click += new System.EventHandler(this.mniPatientLabel_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 3;
            this.menuItem26.Text = "����֪ͨ";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // m_mniArchiving
            // 
            this.m_mniArchiving.Index = 4;
            this.m_mniArchiving.Text = "�����鵵";
            this.m_mniArchiving.Click += new System.EventHandler(this.m_mniArchiving_Click);
            // 
            // m_mnuDebug
            // 
            this.m_mnuDebug.Index = 5;
            this.m_mnuDebug.Text = "ȫ�ײ���";
            this.m_mnuDebug.Click += new System.EventHandler(this.m_mnuDebug_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 6;
            this.menuItem44.Text = "-";
            // 
            // mnuCommonUse
            // 
            this.mnuCommonUse.Index = 7;
            this.mnuCommonUse.Text = "һ�㳣��ֵ";
            this.mnuCommonUse.Click += new System.EventHandler(this.mnuCommonUse_Click);
            // 
            // m_mniTemplate
            // 
            this.m_mniTemplate.Index = 8;
            this.m_mniTemplate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniAddNewTemplate,
            this.m_mniAddNewCommonUseTemplate,
            this.m_mniTemplateManage});
            this.m_mniTemplate.Text = "ģ��";
            // 
            // m_mniAddNewTemplate
            // 
            this.m_mniAddNewTemplate.Index = 0;
            this.m_mniAddNewTemplate.Text = "����ģ��";
            this.m_mniAddNewTemplate.Click += new System.EventHandler(this.NewTemplate_Click);
            // 
            // m_mniAddNewCommonUseTemplate
            // 
            this.m_mniAddNewCommonUseTemplate.Index = 1;
            this.m_mniAddNewCommonUseTemplate.Text = "���ɳ���ֵģ��";
            this.m_mniAddNewCommonUseTemplate.Click += new System.EventHandler(this.NewCommonUse_Click);
            // 
            // m_mniTemplateManage
            // 
            this.m_mniTemplateManage.Index = 2;
            this.m_mniTemplateManage.Text = "ģ��ά��";
            this.m_mniTemplateManage.Click += new System.EventHandler(this.m_mniTemplateManage_Click);
            // 
            // m_mniDefaultValue
            // 
            this.m_mniDefaultValue.Index = 9;
            this.m_mniDefaultValue.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniSaveDefaultValue,
            this.m_mniResetDefaultValue});
            this.m_mniDefaultValue.Text = "Ĭ��ֵ";
            // 
            // m_mniSaveDefaultValue
            // 
            this.m_mniSaveDefaultValue.Index = 0;
            this.m_mniSaveDefaultValue.Text = "����Ĭ��ֵ";
            this.m_mniSaveDefaultValue.Click += new System.EventHandler(this.DefaultValue_Click);
            // 
            // m_mniResetDefaultValue
            // 
            this.m_mniResetDefaultValue.Index = 1;
            this.m_mniResetDefaultValue.Text = "����Ĭ��ֵ";
            this.m_mniResetDefaultValue.Click += new System.EventHandler(this.ResetDefaultValue_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 10;
            this.menuItem45.Text = "-";
            // 
            // mnuSpectialSymbol
            // 
            this.mnuSpectialSymbol.Index = 11;
            this.mnuSpectialSymbol.Text = "�������ά��";
            this.mnuSpectialSymbol.Click += new System.EventHandler(this.mnuSpectialSymbol_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 12;
            this.menuItem1.Text = "-";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 13;
            this.menuItem6.Text = "�޸ĵ�¼��Ϣ";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // m_mniManageExplorer
            // 
            this.m_mniManageExplorer.Index = 14;
            this.m_mniManageExplorer.Text = "ҽԺ����";
            this.m_mniManageExplorer.Click += new System.EventHandler(this.m_mniManageExplorer_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 15;
            this.menuItem2.Text = "Ա��Ȩ�޹���";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // mniManageDept
            // 
            this.mniManageDept.Index = 16;
            this.mniManageDept.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniManageDocAndNur,
            this.mniOutExpertManage,
            this.m_mniEmployeeMove});
            this.mniManageDept.Text = "�������ù���";
            // 
            // mniManageDocAndNur
            // 
            this.mniManageDocAndNur.Index = 0;
            this.mniManageDocAndNur.Text = "Ա������ֵά��";
            this.mniManageDocAndNur.Click += new System.EventHandler(this.mniManageDocAndNur_Click);
            // 
            // mniOutExpertManage
            // 
            this.mniOutExpertManage.Index = 1;
            this.mniOutExpertManage.Text = "����ʽԱ�����Ϲ���";
            this.mniOutExpertManage.Click += new System.EventHandler(this.mniOutExpertManage_Click);
            // 
            // m_mniEmployeeMove
            // 
            this.m_mniEmployeeMove.Index = 2;
            this.m_mniEmployeeMove.Text = "Ա��ת��ת��";
            this.m_mniEmployeeMove.Click += new System.EventHandler(this.m_mniEmployeeMove_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 17;
            this.menuItem50.Text = "-";
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 18;
            this.menuItem48.Text = "�Զ�����༭";
            this.menuItem48.Click += new System.EventHandler(this.m_mniCustomFormEditor_Click);
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 19;
            this.menuItem43.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem29,
            this.menuItem32,
            this.menuItem36,
            this.m_mniIntelligentStatics,
            this.mnuTemplateTools,
            this.menuItem3});
            this.menuItem43.Text = "����";
            this.menuItem43.Visible = false;
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem33,
            this.menuItem34});
            this.menuItem7.Text = "������Ŀ����";
            this.menuItem7.Visible = false;
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 0;
            this.menuItem33.Text = "����ά��";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 1;
            this.menuItem34.Text = "�걾ά��";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 1;
            this.menuItem29.Text = "ʱ������";
            this.menuItem29.Visible = false;
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 2;
            this.menuItem32.Text = "ҽ������";
            this.menuItem32.Visible = false;
            this.menuItem32.Click += new System.EventHandler(this.menuItem32_Click);
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 3;
            this.menuItem36.Text = "���ܲ�ѯ";
            this.menuItem36.Click += new System.EventHandler(this.menuItem36_Click);
            // 
            // m_mniIntelligentStatics
            // 
            this.m_mniIntelligentStatics.Index = 4;
            this.m_mniIntelligentStatics.Text = "����ͳ��";
            this.m_mniIntelligentStatics.Click += new System.EventHandler(this.m_mniIntelligentStatics_Click);
            // 
            // mnuTemplateTools
            // 
            this.mnuTemplateTools.Index = 5;
            this.mnuTemplateTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEightSystem});
            this.mnuTemplateTools.Text = "ģ�幤��";
            // 
            // mnuEightSystem
            // 
            this.mnuEightSystem.Index = 0;
            this.mnuEightSystem.Text = "ҽ��ģ��---���˴�ϵͳ";
            this.mnuEightSystem.Visible = false;
            this.mnuEightSystem.Click += new System.EventHandler(this.mnuEightSystem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Text = "��Ԫģ��༭";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // mnuWindows
            // 
            this.mnuWindows.Index = 8;
            this.mnuWindows.MdiList = true;
            this.mnuWindows.Text = "��     ��";
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 9;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEmail,
            this.mnuSplitter6,
            this.mnuVersion,
            this.mnuDebuTool,
            this.menuItem4});
            this.mnuHelp.Text = "��    ��";
            // 
            // mnuEmail
            // 
            this.mnuEmail.Index = 0;
            this.mnuEmail.Text = "Email";
            this.mnuEmail.Click += new System.EventHandler(this.mnuEmail_Click);
            // 
            // mnuSplitter6
            // 
            this.mnuSplitter6.Index = 1;
            this.mnuSplitter6.Text = "-";
            // 
            // mnuVersion
            // 
            this.mnuVersion.Index = 2;
            this.mnuVersion.Text = "��    ��";
            this.mnuVersion.Click += new System.EventHandler(this.mnuVersion_Click);
            // 
            // mnuDebuTool
            // 
            this.mnuDebuTool.Index = 3;
            this.mnuDebuTool.Text = "���Թ���";
            this.mnuDebuTool.Visible = false;
            this.mnuDebuTool.Click += new System.EventHandler(this.mnuDebuTool_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "ģ���������";
            this.menuItem4.Visible = false;
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // timRevisitRemind
            // 
            this.timRevisitRemind.Enabled = true;
            this.timRevisitRemind.Interval = 60000;
            // 
            // MDIParent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1030, 567);
            this.Controls.Add(this.StatusBar);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mnuMDI;
            this.Name = "MDIParent";
            this.Text = "�嫵��ԿƼ����޹�˾---iCare";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MDIParent_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDIParent_Closing);
            this.Load += new System.EventHandler(this.MDIParent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Get Language VersionID 
		/// <summary>
		/// Get Language VersionID 
		/// </summary>
		[DllImport("kernel32.dll")]                          //API����
		public static extern int GetSystemDefaultLCID(); 

		private void MDIParent_Activated(object sender, System.EventArgs e)
		{
			try
			{
				int i = MDIParent.GetSystemDefaultLCID();
			}
			catch
			{
			}

		}
		#endregion

		private enmPrivilegeSF m_enmGetSFbyMenuItem(MenuItem p_mniItem) 
		{
			#region ���˵�ȫ������

			switch(p_mniItem.Text) 
			{ 
				case "סԺ����":
					return enmPrivilegeSF.frmInPatientCaseHistory;					
				case "סԺ����ģʽ2":
					return enmPrivilegeSF.frmInPatientCaseHistoryMode1;					
				case "���̼�¼":
					return enmPrivilegeSF.frmSubDiseaseTrack;					
				case "SPECT������뵥":
					return enmPrivilegeSF.frmSPECT;					
				case "��ѹ���������뵥":
					return enmPrivilegeSF.frmHighOxygen;
				case "B�ͳ������������뵥":
					return enmPrivilegeSF.frmBUltrasonicCheckOrder;
				case "CT������뵥":
					return enmPrivilegeSF.frmCTCheckOrder;
				case "X�����뵥":
					return enmPrivilegeSF.frmXRayCheckOrder;
				case "���������֯�ͼ쵥":
					return enmPrivilegeSF.frmPathologyOrgCheckOrder;
				case "MRI���뵥":
					return enmPrivilegeSF.frmMRIApply;
				case "ʵ���Ҽ������뵥":
					return enmPrivilegeSF.frmLabAnalysisOrder;
				case "ʵ���Ҽ��鱨�浥":
					return enmPrivilegeSF.frmLabCheckReport;
				case "����֪��ͬ����":
					return enmPrivilegeSF.frmOperationAgreedRecord;
				case "��ǰС��":
					return enmPrivilegeSF.frmBeforeOperationSummary;
				case "������¼��":
					return enmPrivilegeSF.frmOperationRecordDoctor;
				case "ICUת���¼":
					return enmPrivilegeSF.frmPICUShiftInForm;
				case "ICUת����¼":
					return enmPrivilegeSF.frmPICUShiftOutForm;
				case "SIRS�������":
					return enmPrivilegeSF.SIRSEvaluation;
				case "����Glasgow��������":
					return enmPrivilegeSF.ImproveGlasgowComaEvaluation;
				case "���Է���������":
					return enmPrivilegeSF.LungInjuryEvaluation;
				case "������Σ�ز�������":
					return enmPrivilegeSF.NewBabyInjuryCaseEvaluation;
				case "С��Σ�ز�������":
					return enmPrivilegeSF.BabyInjuryCaseEvaluation;
				case "APACHEII ����":
					return enmPrivilegeSF.APACHEIIValuation;
				case "APACHEIII ����":
					return enmPrivilegeSF.APACHEIIIValuation;
				case "TISS-28����":
					return enmPrivilegeSF.frmTISSValuation;
				case "���Ʒ���":
					return enmPrivilegeSF.frmICUTrend;
				case "סԺ������ҳ":
					return enmPrivilegeSF.frmInHospitalMainRecord;
				case "�����������ֱ�":
					return enmPrivilegeSF.frmQCRecord;
				case "��Ժ��������":
					return enmPrivilegeSF.frmInPatientEvaluate;
				case "�� �� ��":
					return enmPrivilegeSF.frmThreeMeasureRecord;
				case "һ�㻤���¼":
					return enmPrivilegeSF.frmMainGeneralNurseRecord;
				case "�۲���Ŀ��¼��":
					return enmPrivilegeSF.frmWatchItemTrack;
				case "Σ�ػ��߻����¼":
					//ʹ���°� modified by tfzhang at 2005��9��21�� 16:12:49
					return enmPrivilegeSF.frmIntensiveTendMain_FC;
//					return enmPrivilegeSF.frmIntensiveTendMain;
				case "ICUΣ�ػ��߻����¼":
					return enmPrivilegeSF.frmICUIntensiveTendRecord;
				case "���������¼":
					return enmPrivilegeSF.frmOperationRecord;
				case "������е�����ϵ�����":
					return enmPrivilegeSF.frmOperationEquipmentQty;
				case "��Ժ��¼":
					return enmPrivilegeSF.frmOutHospital;
				case "�����¼":
					return enmPrivilegeSF.frmConsultation;
				case "Σ��֢�໤�����ػ���¼��":
					return enmPrivilegeSF.frmMainICUIntensiveTend;
				case "����ICU���������Ƽ໤��¼��":
					return enmPrivilegeSF.frmMainICUBreath;

			#endregion ���˵�ȫ������
			}
			return enmPrivilegeSF.HRPExplorer;
		}

//		private clsOISF[] m_objOISFArr;

		private void m_mthSetMenuItemsUsable(MenuItem p_mniItem)
		{
			enmPrivilegeSF enmSF = m_enmGetSFbyMenuItem(p_mniItem);

            //if(enmSF != enmPrivilegeSF.HRPExplorer)
            //{
            //    //if(m_objPIArr == null)
            //    //    return;

            //    for(int i1=0;i1<m_objPIArr.Length;i1++)
            //    {
            //        if(m_objPIArr[i1] == null)
            //            continue;

            //        if(m_objPIArr[i1].m_objGetOISF(m_strCurrentDeptID,(int)enmSF,(int)enmPrivilegeOperation.Read) != null)
            //        {
            //            p_mniItem.Enabled = true;
            //            break;
            //        }
            //        else
            //            p_mniItem.Enabled = false;					
            //    }
					
            //}

			for(int i=0;i<p_mniItem.MenuItems.Count;i++)
			{
				m_mthSetMenuItemsUsable(p_mniItem.MenuItems[i]);
			}
		}


		private void MDIParent_Load(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

//			s_objDepartment = clsSystemContext.s_ObjCurrentContext.m_ObjDepartment;
//			strOperatorID = clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID;
//			strOperatorName = clsSystemContext.s_ObjCurrentContext.m_objEmployee.m_StrLastName;

//			s_objRecordDateTimeInfo = new RecordDateTime.clsRecordDateTimeInfo(s_objDepartment.m_StrDeptID);
//			s_objSaveCue = new FormUtility.clsSaveCue();

			timCount.Enabled =true;
			statusBarPanel1.Text ="� � �� �� �� �� �� �� �� ˾ ";
			statusBarPanel2.Text="�� �� ��:  "+ DateTime.Now.ToString("yyyy��M��d��")+"   "+DateTime.Now.ToLongTimeString() ;
			statusBarPanel4.Text = "��ǰ����:             ";
			statusBarPanel3.Text="��ǰ�û�:  "+strOperatorName; 	

			#region ����û���½��Ϣ,jacky-2003-1-6
			//begin-			
			objclsDomainUserLoginInfo.strEmployeeID=OperatorID;			

			objclsDomainUserLoginInfo.strComputerName=clsSystemContext.s_ObjCurrentContext.m_ObjMachine.m_StrComputerName;				

			System.Net.IPAddress objIPAdd = clsSystemContext.s_ObjCurrentContext.m_ObjMachine.m_IpaLocal;
			objclsDomainUserLoginInfo.strIPAddress = objIPAdd.ToString();
			string strDateTimeNow;
			long lngRes=new clsDomainUserLoginInfoDomain().m_lngAddDomainUserLoginInfo(objclsDomainUserLoginInfo,out strDateTimeNow);
			
			lngRes = 1;
			if(lngRes<=0)
			{
				load.Visible = false;
				load.Close();

				clsPublicFunction.ShowInformationMessageBox("��ʼ���û���½��Ϣʧ��!");
				this.Close(); 
				return;
			}
			objclsDomainUserLoginInfo.strLoginDateTime=strDateTimeNow;
			//end-����û���½��Ϣ,jacky-2003-1-6
			#endregion

			#region �����û���Ȩ�ޣ�����Ӧ�Ŀ���

			//ϵͳ����Ա����ת��ת��
			m_mniEmployeeMove.Enabled = !(clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID=="0000000");

			foreach(MenuItem mniItem in mnuMDI.MenuItems)
			{
				if(mniItem.Text=="ҽ������վ" || mniItem.Text=="��ʿ����վ")
				{
					for(int i=0;i<mniItem.MenuItems.Count;i++)
					{
						m_mthSetMenuItemsUsable(mniItem.MenuItems[i]);
					}
				}
				else if(mniItem.Text=="��������")
				{
                    //for(int i1=0;i1<m_objPIArr.Length;i1++)
                    //{
                        //if(m_objPIArr[i1] == null)
                        //    continue;
					
                        //if(m_objPIArr[i1].m_objGetOISF("0000000",(int)enmPrivilegeSF.frmManageExplorer,(int)enmPrivilegeOperation.AddOrModify) != null)
                        //{
							m_mniManageExplorer.Enabled = true;
							menuItem2.Enabled = true;
                        //    break;
                        //}
                        //else
                        //{
                        //    m_mniManageExplorer.Enabled = false;
                        //    menuItem2.Enabled = false;
                        //}
                    //}
				}
			}
			#endregion

			//modified by tfzhang at 2005��9��12�� 16:33:33
			com.digitalwave.emr.BEDExplorer.frmHRPExplorer form_mainexplorer=new com.digitalwave.emr.BEDExplorer.frmHRPExplorer();
//			Form_HRPExplorer form_mainexplorer = new Form_HRPExplorer() ;
			form_mainexplorer.MdiParent = this;
			form_mainexplorer.Show() ; 

			load.Visible = false;
			load.Close();
			m_mthLoadCustomForms();
			m_mthLoadAnaContainer();
			this.Cursor = Cursors.Default;
		}



		/// <summary>
		/// �������������
		/// </summary>
		private void m_mthLoadAnaContainer()
		{

            //if(s_ObjDepartment.m_StrDeptID == "9900001")
            //{
            //    try
            //    {
            //        s_bolIAnaSystem = true;
            //        reBar.Visible = false;
            //        //���ز˵�
            //        foreach(MenuItem mni in  mnuMDI.MenuItems)
            //        {
            //            mni.Visible=false;
            //        }

            //        this.ActiveMdiChild.Visible=false;
                    
            //        com.digitalwave.Utility.Controls.ctlRichTextBox.m_ClrDefaultViewText = SystemColors.WindowText;
            //        //					menuItem10.Visible = true;
            //        this.Cursor = Cursors.WaitCursor;
            //        frmAnaContainerForm frmanacontainerform = new frmAnaContainerForm();
            //        frmanacontainerform.MdiParent=this;
            //        frmanacontainerform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //        frmanacontainerform.Show();
            //        this.Cursor=Cursors.Default;
            //    }
            //    catch(Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        MessageBox.Show("װ�ش���ʧ��");
            //    }
            //}
		}

		/// <summary>
		/// ʱ����ʾ�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timMDI_Tick(object sender, System.EventArgs e)
		{
			try
			{
				DateTime t = DateTime.Now;
				string s = t.ToLongTimeString() ;
				statusBarPanel2.Text = "�� �� ��:  "+DateTime.Now.ToLongDateString()+" "+s ;
			}
			catch(Exception exp)
			{
				string strErrorMessage=exp.Message;
			}
		}
        /// <summary>
        /// ����ر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public void MDIParent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
 			try
			{
				//jacky-2003-1-6
				long lngRes=new clsDomainUserLoginInfoDomain().m_lngModifyDomainUserLoginInfo(objclsDomainUserLoginInfo.strEmployeeID,objclsDomainUserLoginInfo.strLoginDateTime,objclsDomainUserLoginInfo.strIPAddress);
				lngRes = 1;
				if(lngRes<=0)
				{
					clsPublicFunction.ShowInformationMessageBox("�û��˳�ʱ�����ʧ��!");
				}

				clsLoginContext.s_ObjLoginContext.m_lngLogout();	
			
				clsLoginContext.s_ObjLoginContext.m_mthClear();
				
			}
			catch (Exception exp)
			{
				string strErrMessage=exp.Message;
			}
    

		}

		#region ToolBar and MenuBar EventArge

		void AddTemplate_Click(Object s, EventArgs e)
		{
		}

		void Exit_Click(Object s, EventArgs e)
		{
			try
			{
				Form activeform;			
				
				if (this.ActiveMdiChild  == null)
					this.Close();
				else
				{
					Cursor.Current =Cursors.WaitCursor;
					activeform=this.ActiveMdiChild ;
					activeform.Close() ; 
					Cursor.Current =Cursors.Default;
				}				
			}
			catch
			{
			}
		}

		void Refresh_Click(Object s, EventArgs e)
		{
				try
			{
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  != null && this.ActiveMdiChild is frmHRPBaseForm)
				{
					((frmHRPBaseForm)(this.ActiveMdiChild)).m_mthSearchDeactiveInfo(); 

				}
				Cursor.Current =Cursors.Default;
			}
			catch
			{

			}
		}

		void NewTemplate_Click(Object s, EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  != null)
				{
					//�൱��(this.ActiveMdiChild is frmRecordsBase) ? (frmRecordsBase)this.ActiveMdiChild : null
					frmRecordsBase frmRecords = this.ActiveMdiChild as frmRecordsBase;

					if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
					{
						frmRecords.m_FrmCurrentSub.m_mthNewTemplateWithThis();						
					}
					else
					{
						((frmHRPBaseForm)(this.ActiveMdiChild)).m_mthNewTemplate(); 
					}

				}
				Cursor.Current =Cursors.Default;
			}
			catch
			{

			}
		}

		/// <summary>
		/// �½�����ֵ
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		void NewCommonUse_Click(Object s, EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  != null)
				{
					frmRecordsBase frmRecords = this.ActiveMdiChild as frmRecordsBase;

					if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
					{
						frmRecords.m_FrmCurrentSub.m_mthNewCommonUseWithThis();						
					}
					else
					{
						((frmHRPBaseForm)(this.ActiveMdiChild)).m_mthNewCommonUse(); 
					}

				}
				Cursor.Current =Cursors.Default;
			}
			catch
			{

			}
		}

		/// <summary>
		/// ����Ĭ��ֵ
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		void DefaultValue_Click(Object s,EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  != null)
				{
					Form frmParent = this.ActiveMdiChild;

					clsDefaultValueTool objTool = null;

					if(this.ActiveMdiChild is frmRecordsBase)
					{
						frmRecordsBase frmActive = (frmRecordsBase)this.ActiveMdiChild;
						if(frmActive.m_FrmCurrentSub != null)
						{
							objTool = new clsDefaultValueTool(frmActive.m_FrmCurrentSub);
							frmParent = frmActive.m_FrmCurrentSub;
						}
					}
					else
						objTool = new clsDefaultValueTool(this.ActiveMdiChild);	

					if(objTool != null)
					{
						if(clsPublicFunction.ShowQuestionMessageBox(frmParent,"ע�⣡����Ĭ��ֵ�󽫻Ḳ��ԭ����Ĭ��ֵ���������ܻ��������ݻ��ң���δȷ�����������Ĭ��ֵ�Ƿ�Ϊ������Ĭ��ֵʱ���벻Ҫ��㱣�棬�Ƿ������") == DialogResult.Yes)
							objTool.m_mthSaveDefaultValue();
					}

				}
				Cursor.Current =Cursors.Default;
			}
			catch{}
		}

		/// <summary>
		/// ����Ĭ��ֵ
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		void ResetDefaultValue_Click(Object s,EventArgs e)
		{
			try
			{					
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  != null)
				{					
					Form frmParent = this.ActiveMdiChild;

					clsDefaultValueTool objTool = null;

					if(this.ActiveMdiChild is frmRecordsBase)
					{
						frmRecordsBase frmActive = (frmRecordsBase)this.ActiveMdiChild;
						if(frmActive.m_FrmCurrentSub != null)
						{
							objTool = new clsDefaultValueTool(frmActive.m_FrmCurrentSub,s_ObjCurrentPatient);
							frmParent = frmActive.m_FrmCurrentSub;
						}
					}
					else
						objTool = new clsDefaultValueTool(this.ActiveMdiChild,s_ObjCurrentPatient);

					if(objTool != null)
					{
						if(clsPublicFunction.ShowQuestionMessageBox(frmParent,"�Ƿ�����Ĭ��ֵ��") == DialogResult.Yes)
						{
							objTool.m_BlnReplaceDataShare = false;
							objTool.m_mthSetDefaultValue();
						}
					}

				}
				Cursor.Current =Cursors.Default;
			}
			catch{}
		}

		void Preview_Click(Object s,EventArgs e)
		{			
			try
			{
				if(this.ActiveMdiChild==null )
					return;

				frmRecordsBase frmRecords = this.ActiveMdiChild as frmRecordsBase;

				if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
				{
					//����ʹ���Ӵ��壬����ӡ��
					return;
				}

				Cursor.Current =Cursors.WaitCursor;
				if(this.ActiveMdiChild is frmHRPBaseForm)
					((frmHRPBaseForm)(this.ActiveMdiChild)).m_BlnDirectPrint = false;
				((PublicFunction)(this.ActiveMdiChild)).Print(); 
				Cursor.Current =Cursors.Default;
			}
			catch
			{}
		}

		void Print_Click(Object s,EventArgs e)
		{			
			try
			{
				if(this.ActiveMdiChild==null )
					return;

				frmRecordsBase frmRecords = this.ActiveMdiChild as frmRecordsBase;

				if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
				{
					//����ʹ���Ӵ��壬����ӡ��
					return;
				}

				Cursor.Current =Cursors.WaitCursor;
				if(this.ActiveMdiChild is frmHRPBaseForm)
					((frmHRPBaseForm)(this.ActiveMdiChild)).m_BlnDirectPrint = true;
				((PublicFunction)(this.ActiveMdiChild)).Print();
				Cursor.Current =Cursors.Default;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		void New_Click(Object s,EventArgs e)
		{
		}

		void Approve_Click(Object s,EventArgs e)
		{
			if(this.ActiveMdiChild==null)
				return;

			Cursor.Current =Cursors.WaitCursor;
			try
			{
				//���Ӵ�����ΪfrmHRPBaseForm				
				((frmHRPBaseForm)(this.ActiveMdiChild)).m_lngApprove(); 
			}
			catch(InvalidCastException)//���ʹ�����Ϊ��Щ���岻�Ǽ̳�frmHRPBaseForm��
			{
			}
			Cursor.Current =Cursors.Default;
		}

		void Unapprove_Click(Object s,EventArgs e)
		{
			if(this.ActiveMdiChild==null)
				return;

			Cursor.Current =Cursors.WaitCursor;
			try
			{				
				((frmHRPBaseForm)(this.ActiveMdiChild)).m_lngUnApprove(); 
			}
			catch(InvalidCastException)
			{
			}
			Cursor.Current =Cursors.Default;
		}

		void Save_Click(Object s, EventArgs e)
		{
			try
			{
				if(this.ActiveMdiChild==null && !(this.ActiveMdiChild is PublicFunction))
					return;

				Cursor.Current =Cursors.WaitCursor;				
				((PublicFunction)(this.ActiveMdiChild)).Save(); 
				Cursor.Current =Cursors.Default;
			}
			catch
			{}
		}

		void Open_Click(Object s, EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				//modified by tfzhang at 2005��9��12�� 16:34:17
				com.digitalwave.emr.BEDExplorer.frmHRPExplorer form_mainexplorer=new com.digitalwave.emr.BEDExplorer.frmHRPExplorer();
//				Form_HRPExplorer form_mainexplorer = new Form_HRPExplorer() ;
				form_mainexplorer.MdiParent = this;
				form_mainexplorer.Show() ; 
				Cursor.Current =Cursors.Default;
			}
			catch
			{
				Cursor.Current =Cursors.Default;
			}
		}

		void Del_Click(Object s, EventArgs e)
		{
			try
			{
				if(this.ActiveMdiChild==null && !(this.ActiveMdiChild is PublicFunction))
					return;

				Cursor.Current =Cursors.WaitCursor;				
//				((frmHRPBaseForm)(this.ActiveMdiChild)).m_lngDeleteDocument();
				((PublicFunction)(this.ActiveMdiChild)).Delete();				
				Cursor.Current =Cursors.Default;
			}
			catch
			{
				
			}
		}
		//		void ToggleEnabled_Click(Object s, EventArgs e)
		//		{
		//			enabledItem.Enabled = !enabledItem.Enabled;
		//		}
		//
		//		void ToggleVisible_Click(Object s, EventArgs e)
		//		{
		//			visibleItem.Visible = !visibleItem.Visible;
		//		}
		//
		//		void ToggleCheck_Click(Object s, EventArgs e)
		//		{
		//			CommandBarItem item = (s as CommandBarItem);
		//			item.Checked = !item.Checked;
		//		}

		void ChangeFont_Click(Object s, EventArgs e)
		{
		}
		#endregion

		#region Open the Forms
		private void mnuDebuTool_Click(object sender, System.EventArgs e)
		{

			#if FunctionPrivilege
						if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmAutoTestTool,enmPrivilegeOperation.Open))
						{
							clsPublicFunction.s_mthShowNotPermitMessage();
							return;
						}			
			#endif

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmAutoTestTool frmautotesttool=new frmAutoTestTool();
				frmautotesttool.MdiParent =this;
				frmautotesttool.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		
		private void mnuOperationQty_Click(object sender, System.EventArgs e)
		{
			

#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmOperationEquipmentQty,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

//			try
//			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationEquipmentQty frmoperationequipmentqty=new frmOperationEquipmentQty();
				frmoperationequipmentqty.MdiParent =this;
				frmoperationequipmentqty.m_mthDisableSelectPatient(false);
				frmoperationequipmentqty.Show(); 
				frmoperationequipmentqty.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
//			}
//			catch
//			{}
		}

		private void mnuICUTendRecord_Click(object sender, System.EventArgs e)
		{
			

#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmICUIntensiveTendRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmICUIntensiveTendRecord frmicuintensivetendrecord=new frmICUIntensiveTendRecord();
				frmicuintensivetendrecord.MdiParent =this;
				frmicuintensivetendrecord.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuEvaluate_Click(object sender, System.EventArgs e)
		{
			

#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmInPatientEvaluate,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientEvaluate frminwardevalute;
				if(Form_HRPExplorer.c_strDeptID_ICUCenterDepartment !=clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID)
					frminwardevalute=new frmInPatientEvaluate(false);//�˴�Ϊ��ICU��ʹ��
				else
					frminwardevalute=new frmInPatientEvaluate(true);//�˴�ΪICU��ʹ��
				frminwardevalute.MdiParent =this;
				frminwardevalute.m_mthDisableSelectPatient(false);
				frminwardevalute.Show(); 
				frminwardevalute.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		

		private void mnuOperationRecord_Click_1(object sender, System.EventArgs e)
		{			

#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmOperationRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationRecord frmoperationrecord=new frmOperationRecord();
				frmoperationrecord.MdiParent =this;
				frmoperationrecord.m_mthDisableSelectPatient(false);
				frmoperationrecord.Show(); 
				frmoperationrecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuTendRecord_Click(object sender, System.EventArgs e)
		{			

#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmIntensiveTendMain,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif

			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmIntensiveTendMain frmintensivetendmain=new frmIntensiveTendMain();
				frmintensivetendmain.MdiParent =this;
				frmintensivetendmain.m_mthDisableSelectPatient(false);
				frmintensivetendmain.Show(); 
				frmintensivetendmain.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuGeneralTenda_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmMainGeneralNurseRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainGeneralNurseRecord frmmaingeneralnurserecord=new frmMainGeneralNurseRecord();
				frmmaingeneralnurserecord.MdiParent =this;
				frmmaingeneralnurserecord.m_mthDisableSelectPatient(false);
				frmmaingeneralnurserecord.Show(); 
				frmmaingeneralnurserecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuMainRecord_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmInHospitalMainRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInHospitalMainRecord frminhospitalmainrecord=new frmInHospitalMainRecord();
				frminhospitalmainrecord.MdiParent =this;
				frminhospitalmainrecord.Show(); 
				frminhospitalmainrecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuWatchItem_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmWatchItemTrack,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmWatchItemTrack frmwatchitemtrack=new frmWatchItemTrack();
				frmwatchitemtrack.MdiParent =this;
				frmwatchitemtrack.m_mthDisableSelectPatient(false);
				frmwatchitemtrack.Show(); 
				frmwatchitemtrack.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnufrmPICUShiftOutForm_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmPICUShiftOutForm,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPICUShiftOutForm frmpicuShiftOutForm=new frmPICUShiftOutForm();
				frmpicuShiftOutForm.MdiParent =this;
				frmpicuShiftOutForm.Show(); 
				frmpicuShiftOutForm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnufrmPICUShiftInForm_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmPICUShiftInForm,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPICUShiftInForm frmpicuShiftInForm=new frmPICUShiftInForm();
				frmpicuShiftInForm.MdiParent =this;
				frmpicuShiftInForm.Show(); 
				frmpicuShiftInForm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		

		

		private void mnuPatientProcessRecord_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmSubDiseaseTrack,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSubDiseaseTrack frmpatientprocessrecord=new frmSubDiseaseTrack();
				frmpatientprocessrecord.MdiParent =this;
				frmpatientprocessrecord.Show(); 
				frmpatientprocessrecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}


		private void mnuSPECT_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmSPECT,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSPECT frmspect=new frmSPECT();
				frmspect.MdiParent =this;
				frmspect.Show(); 
				frmspect.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuHighOxygen_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmHighOxygen,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmHighOxygen frmhighoxygen=new frmHighOxygen();
				frmhighoxygen.MdiParent =this;
				frmhighoxygen.Show(); 
				frmhighoxygen.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuBultransonic_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmBUltrasonicCheckOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBUltrasonicCheckOrder frmbultrasoniccheckorder=new frmBUltrasonicCheckOrder();
				frmbultrasoniccheckorder.MdiParent =this;
				frmbultrasoniccheckorder.Show(); 
				frmbultrasoniccheckorder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniEKGOrder_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmEKGOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmEKGOrder m_frmEKGOrder=new frmEKGOrder();
				m_frmEKGOrder.MdiParent =this;
				m_frmEKGOrder.Show(); 
				m_frmEKGOrder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniNuclearOrder_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmNuclearOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmNuclearOrder m_frmNuclearOrder=new frmNuclearOrder();
				m_frmNuclearOrder.MdiParent =this;
				m_frmNuclearOrder.Show(); 
				m_frmNuclearOrder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{
			}
		}

		private void mniPSGOrder_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmPSGOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPSGOrder m_frmPSGOrder=new frmPSGOrder();
				m_frmPSGOrder.MdiParent =this;
				m_frmPSGOrder.Show(); 
				m_frmPSGOrder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{

			}
		
		}

		private void mnuOperationRecordDoct_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmOperationRecordDoctor,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationRecordDoctor frmoperationrecorddoctor=new frmOperationRecordDoctor();
				frmoperationrecorddoctor.MdiParent =this;
				frmoperationrecorddoctor.Show(); 
				frmoperationrecorddoctor.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmCTCheckOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmCTCheckOrder frmctcheckorder=new frmCTCheckOrder("","","");
				frmctcheckorder.MdiParent =this;
				frmctcheckorder.Show(); 
				frmctcheckorder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuOperationAgreed_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmOperationAgreedRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOperationAgreedRecord frmoperationagreedrecord=new frmOperationAgreedRecord();
				frmoperationagreedrecord.MdiParent =this;
				frmoperationagreedrecord.Show(); 
				frmoperationagreedrecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuQC_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmQCRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmQCRecord frmqcrecord=new frmQCRecord();
				frmqcrecord.MdiParent =this;
				frmqcrecord.Show(); 
				frmqcrecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuXRay_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmXRayCheckOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmXRayCheckOrder frmXraycheckorder=new frmXRayCheckOrder();
				frmXraycheckorder.MdiParent =this;
				frmXraycheckorder.Show(); 
				frmXraycheckorder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuOperationSummary_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmBeforeOperationSummary,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBeforeOperationSummary frmbeforeoperationsummary=new frmBeforeOperationSummary();
				frmbeforeoperationsummary.MdiParent =this;
				frmbeforeoperationsummary.Show(); 
				frmbeforeoperationsummary.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuPathologyOrgCheckOrder_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmPathologyTissueCheckOrder,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmPathologyOrgCheckOrder frmpathologyorgcheckorder=new frmPathologyOrgCheckOrder();
				frmpathologyorgcheckorder.MdiParent =this;
				frmpathologyorgcheckorder.Show(); 
				frmpathologyorgcheckorder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		

		private void mnuMRIOrder_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmMRIApply,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMRIApply frmmriApplyOrder=new frmMRIApply();
				frmmriApplyOrder.MdiParent =this;
				frmmriApplyOrder.Show(); 
				frmmriApplyOrder.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuEightSystem_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmDoctorTemplateFirstPage,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmDoctorTemplateFirstPage frmdoctortemplatefirstpage=new frmDoctorTemplateFirstPage();
				frmdoctortemplatefirstpage.MdiParent =this;
				frmdoctortemplatefirstpage.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

//		private void menuItem1_Click_1(object sender, System.EventArgs e)
//		{
//			try
//			{
//				this.Cursor=Cursors.WaitCursor;
//				frmInputDemo frminputdemo=new frmInputDemo();
//				frminputdemo.MdiParent =this;
//				frminputdemo.Show(); 
//				this.Cursor=Cursors.Default;
//			}
//			catch
//			{}
//		}

		private void mnuAPACHEII_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.APACHEIIValuation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
                frmAPACHEIIIValuation apacheIIValuation = new frmAPACHEIIIValuation();
				apacheIIValuation.MdiParent =this;
				apacheIIValuation.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuAPACHEIII_Click(object sender, System.EventArgs e)
		{
			try
			{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.APACHEIIIValuation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
				this.Cursor=Cursors.WaitCursor;
				frmAPACHEIIIValuation apacheIIIValuation=new frmAPACHEIIIValuation();
				apacheIIIValuation.MdiParent =this;
				apacheIIIValuation.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuSIRS_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.SIRSEvaluation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmSIRSEvaluation frmsirsevaluation=new frmSIRSEvaluation();
				frmsirsevaluation.MdiParent =this;
				frmsirsevaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuGlasgow_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.ImproveGlasgowComaEvaluation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmImproveGlasgowComaEvaluation frmimproveglasgowcomaevaluation=new frmImproveGlasgowComaEvaluation();
				frmimproveglasgowcomaevaluation.MdiParent =this;
				frmimproveglasgowcomaevaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuNewBaby_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.NewBabyInjuryCaseEvaluation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmNewBabyInjuryCaseEvaluation frmnewbabyInjurycaseevaluation=new frmNewBabyInjuryCaseEvaluation();
				frmnewbabyInjurycaseevaluation.MdiParent =this;
				frmnewbabyInjurycaseevaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuLittleBaby_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.BabyInjuryCaseEvaluation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmBabyInjuryCaseEvaluation frmbabyinjurycaseevaluation=new frmBabyInjuryCaseEvaluation();
				frmbabyinjurycaseevaluation.MdiParent =this;
				frmbabyinjurycaseevaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuLung_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.LungInjuryEvaluation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLungInjuryEvaluation frmlunginjuryevaluation=new frmLungInjuryEvaluation();
				frmlunginjuryevaluation.MdiParent =this;
				frmlunginjuryevaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuTISS28_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmTISSValuation,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmTISSValuation frmtissvaluation=new frmTISSValuation();
				frmtissvaluation.MdiParent =this;
				frmtissvaluation.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuCommonUse_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmCommonUse,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmCommonUse frmcommonuse=new frmCommonUse();
				frmcommonuse.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuThreeMeasureRecord_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmThreeMeasureRecord,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmThreeMeasureRecord frmthreemeasurerecord=new frmThreeMeasureRecord();
				frmthreemeasurerecord.MdiParent =this;
				frmthreemeasurerecord.m_mthDisableSelectPatient(false);
				frmthreemeasurerecord.Show(); 	
				frmthreemeasurerecord.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}		

		private void mnuInPatientCaseHistory_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmInPatientCaseHistory,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
//				switch(MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID)
//				{	
//						//����ʹ�õĿ�����ʱ�����µĽṹ
//					case "1110000"://���Ǻ��
//					case "1040300"://�ǿ�
//					case "1030200"://�����ڿ�
//					case "1040700"://�ε����
//						frmInPatientCaseHistory frmChild = new frmInPatientCaseHistory(MDIParent.s_ObjCurrentPatient);
//						frmChild.MdiParent =this;
//						frmChild.Show();
//						frmChild.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
//						break;
//					default:
//						string strFormName = Form_HRPExplorer.c_strGetInpatMedRecType(MDIParent.s_ObjCurrentPatient);
//						if (strFormName == null)  break;
//						System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(".\\iCareComponent.dll");
//						object obj = asm.CreateInstance("iCare." + strFormName);
//						frmHRPBaseForm frm = (frmHRPBaseForm)obj;
//						frm.MdiParent =this;
//						frm.Show();
//						frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
//						break;
//				}
//				frmInPatientCaseHistory frmChild = new frmInPatientCaseHistory();
//				frmChild.MdiParent =this;
//				frmChild.Show();
//				frmChild.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				frmInPatMedRecChoose frmChoose = new frmInPatMedRecChoose();
				frmChoose.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mnuInPatientCaseMode2_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmInPatientCaseHistoryMode1,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientCaseHistoryMode1  frminpatientcaseHistorymode1=new frmInPatientCaseHistoryMode1();
				frminpatientcaseHistorymode1.MdiParent =this;
				frminpatientcaseHistorymode1.Show();
				frminpatientcaseHistorymode1.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void mniOutExpertManage_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmAddOuterExpert,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmAddOuterExpert frmaddouterexpert=new frmAddOuterExpert();
				frmaddouterexpert.ShowDialog();				
				this.Cursor=Cursors.Default;				
			}
			catch
			{			
			}
		}		

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmTemplateUnit,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmTemplateUnit  frmtemplateunit=new frmTemplateUnit();
				frmtemplateunit.MdiParent =this;
				frmtemplateunit.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmGetAllRuningForms,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmGetAllRuningForms  frmgetallruningforms=new frmGetAllRuningForms();
				frmgetallruningforms.MdiParent =this;
				frmgetallruningforms.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmICUTrend,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmICUTrend  frmicutrend=new frmICUTrend();
				frmicutrend.MdiParent =this;
				frmicutrend.Show();
				frmicutrend.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void Copy_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				((PublicFunction)(this.ActiveMdiChild)).Copy(); 
				Cursor.Current =Cursors.Default;
			}
			catch
			{}
		}

		private void Paste_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				((PublicFunction)(this.ActiveMdiChild)).Paste();
				Cursor.Current = Cursors.Default;
			}
			catch
			{}
		}

		private void Cut_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				((PublicFunction)(this.ActiveMdiChild)).Cut();
				Cursor.Current = Cursors.Default;
			}
			catch
			{
			
			}
		}

		private void Redo_Click(object sender, System.EventArgs e)
		{
			try
			{
				switch(this.ActiveMdiChild.ActiveControl.GetType().Name)
				{
					case "RichTextBox":
						((RichTextBox)this.ActiveMdiChild.ActiveControl).Redo();
						break;
					case "dwtBorderTextBox":
					case "ctlBorderTextBox":
					case "TextBox":
						((TextBoxBase)this.ActiveMdiChild.ActiveControl).Undo();
						break;
					case "ctlRichTextBox":
						((ctlRichTextBox)this.ActiveMdiChild.ActiveControl).m_mthRedo();
						break;
				}
			}
			catch
			{
			
			}
		}

		private void Undo_Click(object sender, System.EventArgs e)
		{
			try
			{
				switch(this.ActiveMdiChild.ActiveControl.GetType().Name)
				{
					case "dwtBorderTextBox":
					case "ctlBorderTextBox":
					case "TextBox":
					case "RichTextBox":
						((TextBoxBase)this.ActiveMdiChild.ActiveControl).Undo();
						break;
					case "ctlRichTextBox":
						((ctlRichTextBox)this.ActiveMdiChild.ActiveControl).m_mthUndo();
						break;
				}
			}
			catch
			{			
			}
		}

		private void m_mnuDebug_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmInPatientCaseHistory_SetForm,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientCaseHistory_SetForm frmForm=new frmInPatientCaseHistory_SetForm();
				frmForm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				frmForm.m_mthDisableSelectPatient(false);
				frmForm.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmRoleManage,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmRoleManage frmrolemanage=new frmRoleManage();
				frmrolemanage.MdiParent=this;
				frmrolemanage.Show();
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmUpdateLogin,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmUpdateLogin frmupdatelogin=new frmUpdateLogin();
				frmupdatelogin.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLabCheckItemAdmin frmlabcheckitemadmin=new frmLabCheckItemAdmin();
				frmlabcheckitemadmin.MdiParent=this;
				frmlabcheckitemadmin.Show();
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLabAnalysisOrder frmlabanalysisorder=new frmLabAnalysisOrder();
				frmlabanalysisorder.MdiParent=this;
				frmlabanalysisorder.Show();
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmLabCheckReport frmlabcheckreport=new frmLabCheckReport();
				frmlabcheckreport.MdiParent=this;
				frmlabcheckreport.Show();
				frmlabcheckreport.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{
			}
		}

		private void menuItem42_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmImageReport frmimagereport=new frmImageReport();
				frmimagereport.MdiParent=this;
				frmimagereport.Show();
				frmimagereport.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				frmimagereport.m_mthDisableSelectPatient(false);
				this.Cursor=Cursors.Default;
			}
			catch
			{
			}
		}

		private void mnuEmail_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			frmEmail frmemail=new frmEmail();
			frmemail.Show();
			this.Cursor=Cursors.Default;
		}

		private void m_mniManageExplorer_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			frmManageExplorer frmExpl=new frmManageExplorer();
			frmExpl.MdiParent=this;
			frmExpl.Show();
			this.Cursor=Cursors.Default;
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmOperationRequisition frmoperationrequisition=new frmOperationRequisition();
            //    frmoperationrequisition.MdiParent=this;
            //    frmoperationrequisition.m_mthDisableSelectPatient(false);
            //    frmoperationrequisition.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    clsPublicFunction.ShowInformationMessageBox(ex.Message);
            //}
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaesthesiaConfirm frmanaesthesiaconfirm=new frmAnaesthesiaConfirm();
            //    frmanaesthesiaconfirm.MdiParent=this;
            //    frmanaesthesiaconfirm.m_mthDisableSelectPatient(false);
            //    frmanaesthesiaconfirm.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    clsPublicFunction.ShowInformationMessageBox(ex.Message);
            //}
		
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaesthesiaPlan frmanaesthesiaplan=new frmAnaesthesiaPlan();
            //    frmanaesthesiaplan.MdiParent=this;
            //    frmanaesthesiaplan.m_mthDisableSelectPatient(false);
            //    frmanaesthesiaplan.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    clsPublicFunction.ShowInformationMessageBox(ex.Message);
            //}
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaParamSetting frmanaparamsetting=new frmAnaParamSetting();
            //    frmanaparamsetting.MdiParent=this;
            //    frmanaparamsetting.m_mthDisableSelectPatient(false);
            //    frmanaparamsetting.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    clsPublicFunction.ShowInformationMessageBox(ex.Message);
            //}
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmOutHospital frmouthospital=new frmOutHospital();
				frmouthospital.MdiParent =this;
				frmouthospital.Show(); 
				frmouthospital.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaesthesiaWork frmanaesthesiawork=new frmAnaesthesiaWork();
            //    frmanaesthesiawork.MdiParent =this;
            //    frmanaesthesiawork.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void menuItem22_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmOperationArrange frmoperationarrange=new frmOperationArrange();
            //    frmoperationarrange.MdiParent =this;
            //    frmoperationarrange.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmPCARegister frmpcaregister=new frmPCARegister();
            //    frmpcaregister.MdiParent =this;
            //    frmpcaregister.m_mthDisableSelectPatient(false);
            //    frmpcaregister.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void menuItem24_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmPCAPatientDayList frmpcapatientdaylist=new frmPCAPatientDayList();
            //    frmpcapatientdaylist.MdiParent =this;
            //    frmpcapatientdaylist.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmDutyArrange frmdutyarrange=new frmDutyArrange();
				frmdutyarrange.MdiParent =this;
				frmdutyarrange.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaDeptDutyArrange frmanadeptdutyarrange=new frmAnaDeptDutyArrange();
            //    frmanadeptdutyarrange.MdiParent =this;
            //    frmanadeptdutyarrange.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}



		private void menuItem23_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaesthesiaWorkArrange frmanaesthesiaworkarrange=new frmAnaesthesiaWorkArrange();
            //    frmanaesthesiaworkarrange.MdiParent =this;
            //    frmanaesthesiaworkarrange.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void mnuICUIntensiveTend_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmMainICUIntensiveTend frmmainicuintensivetend=new frmMainICUIntensiveTend();
				frmmainicuintensivetend.MdiParent =this;
				frmmainicuintensivetend.m_mthDisableSelectPatient(false);
				frmmainicuintensivetend.Show(); 
				frmmainicuintensivetend.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem26_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmConsultationSearch frmconsultationsearch=new frmConsultationSearch();
				frmconsultationsearch.ShowDialog(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmConsultation frmconsultation=new frmConsultation();
				frmconsultation.MdiParent = this;
				frmconsultation.Show(); 
				frmconsultation.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void menuItem27_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaPARRecord frmanaparrecord=new frmAnaPARRecord();
            //    frmanaparrecord.MdiParent = this;
            //    frmanaparrecord.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void m_mniIntelligentStatics_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			frmIntelligentStatistics frmIntel=new frmIntelligentStatistics();
			frmIntel.MdiParent = this;
			frmIntel.Show(); 
			this.Cursor=Cursors.Default;
		}

		private void menuItem28_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
            //    frmAnaesthesiaRecordHistory frmanaesthesiarecordhistory=new frmAnaesthesiaRecordHistory();
            //    frmanaesthesiarecordhistory.MdiParent = this;
            //    frmanaesthesiarecordhistory.Show(); 
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{}
		}

		private void menuItem29_Click(object sender, System.EventArgs e)
		{
			try
			{
                //this.Cursor=Cursors.WaitCursor;
                //frmTimeDirection frmTimeDirect=new frmTimeDirection();
                //frmTimeDirect.MdiParent = this;
                //frmTimeDirect.m_mthSetPatient(s_objCurrentPatient);
                //frmTimeDirect.Show(); 
                //this.Cursor=Cursors.Default;
			}
			catch
			{}
		}

		private void m_mniArchiving_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmInPatientCaseHistoryArchiving frm=new frmInPatientCaseHistoryArchiving();
				frm.MdiParent = this;
				frm.Show(); 
				this.Cursor=Cursors.Default;
			}
			catch
			{this.Cursor=Cursors.Default;}
		}

		private void mnuOpenExplorer_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				//modified by tfzhang at 2005��9��12�� 16:35:13
				com.digitalwave.emr.BEDExplorer.frmHRPExplorer form_mainexplorer=new com.digitalwave.emr.BEDExplorer.frmHRPExplorer();
//				Form_HRPExplorer form_mainexplorer = new Form_HRPExplorer() ;
				form_mainexplorer.MdiParent = this;
				form_mainexplorer.Show() ; 
				Cursor.Current =Cursors.Default;
			}
			catch
			{
				Cursor.Current =Cursors.Default;
			}
		}

		private void mnuSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				if(ActiveMdiChild==null)
					return;
				((PublicFunction)(this.ActiveMdiChild)).Save(); 
				Cursor.Current =Cursors.Default;
			}
			catch(Exception ex)
			{clsPublicFunction.ShowInformationMessageBox(ex.Message);}
		}

		private void mnuDel_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				((PublicFunction)(this.ActiveMdiChild)).Delete();
				Cursor.Current =Cursors.Default;
			}
			catch
			{
				Cursor.Current =Cursors.Default;
			}
		}

		private void mnuPrintPriview_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current =Cursors.WaitCursor;
				((PublicFunction)(this.ActiveMdiChild)).Print(); 
				Cursor.Current =Cursors.Default;
			}
			catch
			{}
		}

		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Form activeform;
			
				Cursor.Current =Cursors.WaitCursor;
				if (this.ActiveMdiChild  == null)
					this.Close();
				else
				{
					activeform=this.ActiveMdiChild ;
					activeform.Close() ; 
				}
				Cursor.Current =Cursors.Default;
			}
			catch
			{
			}
		}

		private void menuItem31_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmDeactiveRecord frmdeactiverecord =new frmDeactiveRecord();
				frmdeactiverecord.m_mthDisableSelectPatient(false);
				frmdeactiverecord.ShowDialog(this); 
				this.Cursor=Cursors.Default;
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
			}
		}

		private void menuItem30_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
				
            //    frmAnaOverTime frmanaovertime = new frmAnaOverTime();
            //    frmanaovertime.MdiParent = this;
            //    frmanaovertime.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{
				
            //}
		}

		private void menuItem32_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				
				frmPhysicianOrder frmanaovertime = new frmPhysicianOrder();
				frmanaovertime.MdiParent = this;
				frmanaovertime.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{
				
			}	
		}

		private void menuItem33_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				
				frmContainerManage frmcontainermanage = new frmContainerManage();
				frmcontainermanage.MdiParent = this;
				frmcontainermanage.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{
				
			}	
		}

		private void mnuVersion_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;				
			frmSystemVersion frmsystemversion = new frmSystemVersion();			
			frmsystemversion.Show();
			this.Cursor=Cursors.Default;
		}

		private void menuItem35_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;				
			frmMainICUBreath frmmainicubreath = new frmMainICUBreath();	
			frmmainicubreath.MdiParent=this;
			frmmainicubreath.m_mthDisableSelectPatient(false);
			frmmainicubreath.Show();
			frmmainicubreath.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
			this.Cursor=Cursors.Default;
		}

		private void menuItem36_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;				
			frmIntelligentSearch frmintelligentsearch = new frmIntelligentSearch();	
			frmintelligentsearch.MdiParent=this;			
			frmintelligentsearch.Show();
			this.Cursor=Cursors.Default;
		}

		private void menuItem41_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;				
			frmRecordSearch frmrecordsearch = new frmRecordSearch();	
			frmrecordsearch.MdiParent=this;
			frmrecordsearch.Show();
			this.Cursor=Cursors.Default;
		}

		private void menuItem37_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;
				
            //    frmChangeEquipmentArrange frmchangeequipmentarrange = new frmChangeEquipmentArrange();
            //    frmchangeequipmentarrange.ShowDialog();
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{
            //}	
			
		}

		private void mniPatientLabel_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;				
				frmPatientLabel frmpatientlabel = new frmPatientLabel();
				frmpatientlabel.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch
			{
			}	
		}

		private void menuItem38_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;				
            //    frmAnaModeMaterial frmanamodematerial = new frmAnaModeMaterial();
            //    frmanamodematerial.ShowDialog();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}		
		}

		private void menuItem39_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;				
            //    frmAnaInfoInit frmanamodematerial = new frmAnaInfoInit();
            //    frmanamodematerial.ShowDialog();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}		
		}

		private void menuItem40_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor=Cursors.WaitCursor;				
            //    frmAnaDrugManage frmanadrugmaterial = new frmAnaDrugManage();
            //    frmanadrugmaterial.ShowDialog();
            //    this.Cursor=Cursors.Default;
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}		
		}

		private void mnuDoctorWorkStationForEar_Popup(object sender, System.EventArgs e)
		{			
			
		}

		private void mnuNurseWorkStationForEar_Popup(object sender, System.EventArgs e)
		{
			
		}

		private void mnuWorking_Popup(object sender, System.EventArgs e)
		{
			
		}

		private void mniManageDocAndNur_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				frmManageDocAndNur frm = new frmManageDocAndNur();
				frm.ShowDialog();
				this.Cursor = Cursors.Default;
			}
			catch
			{}
		}

		private void mniImageBookingSearch_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				frmImageBookingSearch frmImageBooking = new frmImageBookingSearch();
				frmImageBooking.MdiParent=this;
				frmImageBooking.Show();
				frmImageBooking.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				frmImageBooking.m_mthDisableSelectPatient(false);
				this.Cursor=Cursors.Default;
			}
			catch
			{
				MessageBox.Show("װ�ش���ʧ��");
			}
		}

		private void frmAnaFormContainer_Click(object sender, System.EventArgs e)
		{
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    frmAnaContainerForm frmanacontainerform = new frmAnaContainerForm();
            //    frmanacontainerform.MdiParent=this;
            //    frmanacontainerform.Show();
            //    this.Cursor=Cursors.Default;
            //}
            //catch
            //{
            //    MessageBox.Show("װ�ش���ʧ��");
            //}
		}

		private void m_mniEmployeeMove_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				frmEmployeeMove frmChild = new frmEmployeeMove();
				frmChild.StartPosition = FormStartPosition.CenterParent;
				frmChild.ShowDialog();
				this.Cursor = Cursors.Default;
			}
			catch
			{}
		}

		/// <summary>
		/// ��ǰ����
		/// </summary>
		private static frmInPatientCaseHistory.enmCaseType m_enmCaseType;
		public static frmInPatientCaseHistory.enmCaseType m_EnmCaseType
		{
			get
			{
				return m_enmCaseType;				
			}
			set
			{
				m_enmCaseType = value;
			}
		}
	
		#region �Զ����
		/// <summary>
		/// �Զ�����༭��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_mniCustomFormEditor_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				iCare.CustomForm.frmUserDefinedEditor frmChild = new iCare.CustomForm.frmUserDefinedEditor();
				frmChild.ShowDialog();
				this.Cursor = Cursors.Default;
			}
			catch
			{}
		}

		private MenuItem m_mniCustomForm = new MenuItem("�Զ����");
		private clsCustom_SubmitValue[] m_objCustomForms;
		/// <summary>
		/// Load���Զ����
		/// </summary>
		private void m_mthLoadCustomForms()
		{
			long lngRes = new iCare.CustomForm.clsCustomFormDomain().m_lngGetSubmitForms(MDIParent.OperatorID,clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID,out m_objCustomForms);
			if(lngRes <= 0 || m_objCustomForms == null || m_objCustomForms.Length == 0)
				return;
			for(int i = 0; i < m_objCustomForms.Length; i++)
			{
				MenuItem mniChild = m_mniCustomForm.MenuItems.Add(m_objCustomForms[i].m_strFormName,new EventHandler(m_mthShowCustomForm));				
			}
			m_mniCustomForm.Enabled = false;
			mnuMDI.MenuItems.Add(4,m_mniCustomForm);
		}

		/// <summary>
		/// ���Զ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_mthShowCustomForm(object sender,EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.CustomForm.frmCustomFormBase frmChild = new iCare.CustomForm.frmCustomFormBase(m_objCustomForms[((MenuItem)sender).Index]);
				frmChild.MdiParent =this;
				frmChild.Show(); 
				frmChild.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		#endregion

		private void m_mniTemplateManage_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmTemplateSet,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				frmTemplateSet  frmtemplateset=new frmTemplateSet();
				frmtemplateset.MdiParent =this;
				frmtemplateset.Show();
				this.Cursor=Cursors.Default;
			}
			catch
			{}	
		}

		private void mnuSpectialSymbol_Click(object sender, System.EventArgs e)
		{
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmPrivilegeSF.frmCommonUse,enmPrivilegeOperation.Open))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
			try
			{
				this.Cursor=Cursors.WaitCursor;
				iCare.AssitantTool.frmSpecialSymbolManage frmSpecialSymbolManage = new AssitantTool.frmSpecialSymbolManage();
				frmSpecialSymbolManage.ShowDialog();
				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		#endregion

		public void m_mthSetInpatMedRec_DataShare()
		{
            //long lngRes;
            //clsInpatMedRecDomain objInpatMedRecDomain = new clsInpatMedRecDomain();
            //s_objInpatMedRec_DataShare=new clsInpatMedRec_DataShare();
            //if(clsEMRLogin.LoginInfo.m_strEmpNo.Trim() == "admin" || clsEMRLogin.LoginInfo.m_strEmpNo.Trim() == "0001" || clsHRPTableService.bytDatabase_Selector == 2)
            //{
            //    objInpatMedRecDomain.m_lngGetAllFormID(out s_objInpatMedRec_DataShare.m_objTypeArr);
            //}
            //else //if(clsHRPTableService.bytDatabase_Selector == 0)
            //{
            //    if(clsEMRLogin.m_ObjCurDeptOfEmpArr != null && clsEMRLogin.m_ObjCurDeptOfEmpArr.Length > 0)
            //    {
            //        string[] strDeptArr = new string[clsEMRLogin.m_ObjCurDeptOfEmpArr.Length];
            //        for(int j=0;j<clsEMRLogin.m_ObjCurDeptOfEmpArr.Length;j++)
            //        {
            //            strDeptArr[j] = clsEMRLogin.m_ObjCurDeptOfEmpArr[j].strShortNo;
            //        }
            //        new clsInpatMedRecDomain().m_lngGetFormByChargeDept(strDeptArr,out s_objInpatMedRec_DataShare.m_objTypeArr);
            //    }
            //}
            ////			else
            ////			{
            ////				lngRes = objInpatMedRecDomain.m_lngGetAllFormID(out s_objInpatMedRec_DataShare.m_objTypeArr);
            ////			}
            //lngRes = objInpatMedRecDomain.m_lngGetType_ItemRecord(null,out s_objInpatMedRec_DataShare.m_objType_ItemArr);
		}
		public static bool s_blnRevisitBegin = false;
		/// <summary>
		/// ������ʾ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timRevisitRemind_Tick(object sender, System.EventArgs e)
		{
//			if(m_blnNoNeedRemind)
//			{
//				s_blnRevisitBegin = false;
//				//				timRevisitRemind.Stop();
//				return;
//			}
			if(s_blnRevisitBegin)
			{
				new clsOutPatientRevisitDomain().m_mthSetRemindMessage();
				s_blnRevisitBegin = false;
				//				timRevisitRemind.Stop();
			}
		}
        public static void m_mthClearAll()
        {
            s_objCurrDepartment = null;
            s_objCurrentPatient = null;
            _objCurrentArea = null;
            _objCurrentDeppartment = null;
            _objCurrentPatient = null;
            s_objCurrInPatientArea = null;
            s_objDepartment = null;
        }
	}

    //public class clsInpatMedRec_DataShare
    //{
    //    public clsInpatMedRec_Type[] m_objTypeArr;
    //    public clsInpatMedRec_Type_Item[] m_objType_ItemArr;

    //    public clsInpatMedRec_Type_Item[] m_objGetInpatMedRec_Type_Item(string p_strFormId)
    //    {
    //        ArrayList arryList = new ArrayList();
    //        if (m_objType_ItemArr != null || m_objType_ItemArr.Length > 0)
    //        {
    //            for (int i = 0 ; i < m_objType_ItemArr.Length ; i++)
    //            {
    //                if (m_objType_ItemArr[i].m_strTypeID == p_strFormId)
    //                {
    //                    arryList.Add(m_objType_ItemArr[i]);
    //                }
    //            }
    //        }
    //        return (clsInpatMedRec_Type_Item[])arryList.ToArray(typeof(clsInpatMedRec_Type_Item));
    //    }
    //}


}

