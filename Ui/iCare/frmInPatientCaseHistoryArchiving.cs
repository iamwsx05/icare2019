using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing; 
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using iCare.ICU.Evaluation;

namespace iCare
{
	/// <summary>
	/// �����鵵
	/// �г�Ա�������ٴ�����7���ڵĳ�Ժ����
	/// ����7�����Զ��鵵
	/// </summary>
	public class frmInPatientCaseHistoryArchiving : iCare.frmHRPBaseForm, PublicFunction
	{
		#region system define
		private System.Windows.Forms.ListView m_lsvRecords;
		private System.Windows.Forms.ColumnHeader m_clmInPatientID;
		private System.Windows.Forms.ColumnHeader m_clmInPatientName;
		private System.Windows.Forms.ColumnHeader m_clmInPatientDate;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ContextMenu m_ctmRecords;
		private System.Windows.Forms.MenuItem m_mniArchive;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.MenuItem m_mniUnarchive;
		private System.Windows.Forms.CheckBox m_chkMultiPatient;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtMultiName;
		private com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtMultiID;
		private System.Windows.Forms.ColumnHeader m_clmSex;
		private System.Windows.Forms.ColumnHeader m_clmAge;
		private System.Windows.Forms.ColumnHeader m_clmOutDate;
		private System.Windows.Forms.ColumnHeader m_clmBed;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
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
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem menuItem45;
		private System.Windows.Forms.MenuItem menuItem46;
		private System.Windows.Forms.MenuItem menuItem47;
		private System.Windows.Forms.MenuItem menuItem48;
		private System.Windows.Forms.MenuItem menuItem49;
		private System.Windows.Forms.MenuItem menuItem50;
		private System.Windows.Forms.MenuItem menuItem51;
		private System.Windows.Forms.MenuItem menuItem52;
		private System.Windows.Forms.MenuItem menuItem53;
		private System.Windows.Forms.MenuItem menuItem54;
		private System.Windows.Forms.MenuItem menuItem55;
		private System.Windows.Forms.CheckedListBox m_lstDept2;
		private System.Windows.Forms.CheckBox m_chkAllDept;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton m_rdbNotArchive;
		private System.Windows.Forms.RadioButton m_rdbHasArchive;
		private System.Windows.Forms.RadioButton m_rdbAll;
		private System.Windows.Forms.Label label1;
		private PinkieControls.ButtonXP m_cmdFrech;
        private MenuItem menuItem56;
        private MenuItem menuItem57;
		private System.ComponentModel.IContainer components = null;


		#endregion 

		public frmInPatientCaseHistoryArchiving()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

//			m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{m_lsvRecords});
			m_cboArea.Visible=false;
//			m_cboDept.Visible=false;
			m_txtBedNO.Visible=false;
			// TODO: Add any initialization after the InitializeComponent call		
	
			new clsSortTool().m_mthSetListViewSortable(m_lsvRecords);

			m_mthInitContextMenu();
		}

		#region ��Ա����
		clsInPatientArchivingDomain m_objDomain=new clsInPatientArchivingDomain();
//		bool m_blnMultiPatient=false;
//		bool m_blnWaitToArchiveOnly=true;
//		bool m_blnIsByIDLike=true;
		// ��������������ֻ�ڴ򿪶�����˵ļ�¼ʱ�á�
//		string m_strInPatientID;
//		string m_strInPatientName;
		#endregion

//		clsBorderTool m_objBorderTool = new clsBorderTool(Color.White);

		#region override
		protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
		{
//			m_objBaseCurrentPatient=p_objSelectedPatient;
//			m_mthLoadRecordOfOnePatient();
		}
//		protected override clsPatient [] m_objGetPatient()
//		{
////			return m_objCurrentContext.m_ObjPatientManager.m_objGetAllPatientLike(txtInPatientID.Text);
//			
//		}
//		protected override clsPatient [] m_objGetPatientByPatientName()
//		{
////			return m_objCurrentContext.m_ObjPatientManager.m_objGetAllPatientLike(m_txtPatientName.Text);
//			
//		}
		#endregion

		#region Public Function
		public void Copy()
		{
			m_lngCopy();
		}

		public void Cut()
		{
			m_lngCut();
		}

		public void Delete()
		{
			//			m_lngDelete ();
		}
		public void Verify()
		{
			//long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
		}
		public void Display()
		{
		
		}

		public void Display(string cardno, string sendcheckdate)
		{
		
		}

		public void Paste()
		{
			m_lngPaste();
		}

		public void Print()
		{
			//			this.m_lngPrint();
		}

		public void Redo()
		{
		
		}

		public void Save()
		{
			//			this.m_lngSave();
		}

		public void Undo()
		{
		
		}
		#endregion

		#region ��������
		private bool m_blnIsEmptyString(string p_str)
		{
			if(p_str==null || p_str.Trim().Length==0) return true;
			else return false;
		}
		private void m_ShowMessage(string p_strMsg)
		{
			clsPublicFunction.ShowInformationMessageBox(p_strMsg);
		}
		#endregion

		#region װ��ListView
		/// <summary>
		/// ��ȡ��������7���ڲ����б�
		/// ������
		/// </summary>
		private void m_mthLoadRecordOfOnePatient()
		{
				this.Cursor=Cursors.WaitCursor;
			string[] strDeptIDArr=m_strGetSelectedDeptIDArr();
			if(strDeptIDArr==null)
			{
				m_ShowMessage("������ѡ��һ��Ҫ���ҵĲ��ţ�");
				return;
			}
			try
			{	
				//���
				m_lsvRecords.Items.Clear();
				clsInPatientArchivingValue[] objArchivingValueArr=null; 


				long lngRes=m_objDomain.lngGetRecordWithin7DayByEmpDeptArr(strDeptIDArr,-1,out objArchivingValueArr);

				if(lngRes<=0 )
				{
					m_ShowMessage("�޷��������ݿ⡣");
					return;
				}
				if(objArchivingValueArr==null)
				{
					return;
				}
				m_mthAddItemToView(objArchivingValueArr);

			}
			catch(Exception exp)
			{
				string strErrorMessage=exp.Message;
			}
			finally
			{
				this.Cursor=Cursors.Default;
			}



		
		}
		private void m_mthAddItemToView(clsInPatientArchivingValue[] p_objInPatientArchivingValueArr)
		{
			for(int i=0;i<p_objInPatientArchivingValueArr.Length;i++)
			{
				Application.DoEvents();
				ListViewItem objLsvItem = null;
				if(p_objInPatientArchivingValueArr[i].m_strIfArchived.Trim() == "0" && m_rdbNotArchive.Checked)
				{
					objLsvItem=new ListViewItem(new string[]{"","","","","","","","","",""});
					objLsvItem.SubItems[0].Text=p_objInPatientArchivingValueArr[i].m_strInPatientID;
					objLsvItem.SubItems[1].Text=p_objInPatientArchivingValueArr[i].m_strInPatientName;
					objLsvItem.SubItems[2].Text=p_objInPatientArchivingValueArr[i].m_strPatientSex.Trim();
					objLsvItem.SubItems[3].Text=p_objInPatientArchivingValueArr[i].m_strpatientAge;
					objLsvItem.SubItems[4].Text=p_objInPatientArchivingValueArr[i].m_strBedName;
					objLsvItem.SubItems[5].Text=p_objInPatientArchivingValueArr[i].m_strInPatientDate;
					objLsvItem.SubItems[6].Text=p_objInPatientArchivingValueArr[i].m_strInPatientEndDate;
                    objLsvItem.SubItems[7].Text = "�ȴ��鵵(ʣ��" + p_objInPatientArchivingValueArr[i].m_intLeaveDay + "��" + p_objInPatientArchivingValueArr[i].m_intLeaveHour + "Сʱ)";
//					objLsvItem.SubItems[8].Text=p_objInPatientArchivingValueArr[i].m_strArchivingChangeUserName;
//					objLsvItem.SubItems[9].Text=p_objInPatientArchivingValueArr[i].m_strOpenDate;
					objLsvItem.Tag = p_objInPatientArchivingValueArr[i];
				}
				else if(p_objInPatientArchivingValueArr[i].m_strIfArchived.Trim() == "1" && m_rdbHasArchive.Checked)
				{
					objLsvItem=new ListViewItem(new string[]{"","","","","","","","","",""});
					objLsvItem.SubItems[0].Text=p_objInPatientArchivingValueArr[i].m_strInPatientID;
					objLsvItem.SubItems[1].Text=p_objInPatientArchivingValueArr[i].m_strInPatientName;
					objLsvItem.SubItems[2].Text=p_objInPatientArchivingValueArr[i].m_strPatientSex.Trim();
					objLsvItem.SubItems[3].Text=p_objInPatientArchivingValueArr[i].m_strpatientAge;
					objLsvItem.SubItems[4].Text=p_objInPatientArchivingValueArr[i].m_strBedName;
					objLsvItem.SubItems[5].Text=p_objInPatientArchivingValueArr[i].m_strInPatientDate;
					objLsvItem.SubItems[6].Text=p_objInPatientArchivingValueArr[i].m_strInPatientEndDate;
					objLsvItem.SubItems[7].Text="�ѹ鵵";
					objLsvItem.SubItems[8].Text=p_objInPatientArchivingValueArr[i].m_strArchivingChangeUserName;
					if(p_objInPatientArchivingValueArr[i].m_strArchivingChangeUserID.Trim() == clsEMRLogin.LoginInfo.m_strEmpID)
						objLsvItem.SubItems[8].ForeColor = Color.Green;
					objLsvItem.SubItems[9].Text=p_objInPatientArchivingValueArr[i].m_strOpenDate;
					objLsvItem.Tag = p_objInPatientArchivingValueArr[i];
					objLsvItem.ForeColor = Color.Gray;
				}
				else if(m_rdbAll.Checked)
				{
					objLsvItem=new ListViewItem(new string[]{"","","","","","","","","",""});
					objLsvItem.SubItems[0].Text=p_objInPatientArchivingValueArr[i].m_strInPatientID;
					objLsvItem.SubItems[1].Text=p_objInPatientArchivingValueArr[i].m_strInPatientName;
					objLsvItem.SubItems[2].Text=p_objInPatientArchivingValueArr[i].m_strPatientSex.Trim();
					objLsvItem.SubItems[3].Text=p_objInPatientArchivingValueArr[i].m_strpatientAge;
					objLsvItem.SubItems[4].Text=p_objInPatientArchivingValueArr[i].m_strBedName;
					objLsvItem.SubItems[5].Text=p_objInPatientArchivingValueArr[i].m_strInPatientDate;
					objLsvItem.SubItems[6].Text=p_objInPatientArchivingValueArr[i].m_strInPatientEndDate;
					if(p_objInPatientArchivingValueArr[i].m_strIfArchived=="1")
					{
						objLsvItem.SubItems[7].Text="�ѹ鵵";
						objLsvItem.SubItems[8].Text=p_objInPatientArchivingValueArr[i].m_strArchivingChangeUserName;
						if(p_objInPatientArchivingValueArr[i].m_strArchivingChangeUserID.Trim() == clsEMRLogin.LoginInfo.m_strEmpID)
							objLsvItem.SubItems[8].ForeColor = Color.Green;
						objLsvItem.SubItems[9].Text=p_objInPatientArchivingValueArr[i].m_strOpenDate;
						objLsvItem.ForeColor = Color.Gray;
					}
					else 
					{
                        objLsvItem.SubItems[7].Text = "�ȴ��鵵(ʣ��" + p_objInPatientArchivingValueArr[i].m_intLeaveDay + "��" + p_objInPatientArchivingValueArr[i].m_intLeaveHour + "Сʱ)";
					}
					objLsvItem.Tag = p_objInPatientArchivingValueArr[i];
				}
                if (objLsvItem != null)
                {
                    m_lsvRecords.BeginUpdate();
                    m_lsvRecords.Items.Add(objLsvItem);
                    m_lsvRecords.EndUpdate();
                }
			}
		}
		
		/// <summary>
		/// ��ȡ��������7���ڲ����б�
		/// �ಡ��
		/// </summary>
		/// <param name="p_blnIsByID"></param>
		private void m_mthLoadRecordOfMultiPatient(bool p_blnIsByID)
		{
//			m_lsvRecords.Items.Clear();
//			clsInPatientArchivingValue[] objArchivingValueArr=null; 
//			long lngRes=1;
//			if(p_blnIsByID)
//			{
////				if(m_blnIsEmptyString( m_strInPatientID))
////				{
////					m_ShowMessage("�㻹ûѡ�����ˡ�");
////					return;
////				}
//				string[] strDeptIDArr=m_strGetSelectedDeptIDArr();
//				if(strDeptIDArr==null)
//				{
//					m_ShowMessage("������ѡ��һ��Ҫ���ҵĲ��ţ�");
//					return;
//				}
//
//				this.Cursor=Cursors.WaitCursor;
//				lngRes=m_objDomain.lngGetArchiveByInPatientIDLikeArr(m_strInPatientID,strDeptIDArr,out objArchivingValueArr);	
//			}
//			else
//			{
//				if(m_blnIsEmptyString( m_strInPatientName))
//				{
//					m_ShowMessage("�㻹ûѡ�����ˡ�");
//					return;
//				}
//				string[] strDeptIDArr=m_strGetSelectedDeptIDArr();
//				if(strDeptIDArr==null)
//				{
//					m_ShowMessage("������ѡ��һ��Ҫ���ҵĲ��ţ�");
//					return;
//				}
//
//				this.Cursor=Cursors.WaitCursor;
//				lngRes=m_objDomain.lngGetArchiveByInPatientNameLikeArr(m_strInPatientName,strDeptIDArr,out objArchivingValueArr);	
//			}
//			m_objInPatientArchivingValueArr=objArchivingValueArr;
//			this.Cursor=Cursors.Default;
//			if(lngRes<=0 )
//			{
//				m_ShowMessage("�޷��������ݿ⡣");
//				return;
//			}
//			else if(objArchivingValueArr==null)
//			{
//				return;
//			}
//			string strCurrentID="";
//			this.Cursor=Cursors.WaitCursor;
//			if(!m_blnWaitToArchiveOnly)
//			{
//				ListViewItem[] objLsvItemArr=new ListViewItem[objArchivingValueArr.Length];
//				for (int i=0;i<objArchivingValueArr.Length;i++)
//				{
//				
//					objLsvItemArr[i]=new ListViewItem(new string[]{"","","","","","","","","",""});
//					objLsvItemArr[i].SubItems[6].Text=(DateTime.Parse(objArchivingValueArr[i].m_strInPatientDate)).ToString("yyyy��M��d��");
//					if(objArchivingValueArr[i].m_strIfArchived=="1")
//					{
//						objLsvItemArr[i].SubItems[7].Text="�ѹ鵵";
//						objLsvItemArr[i].SubItems[8].Text=objArchivingValueArr[i].m_strArchivingChangeUserName;
//						objLsvItemArr[i].SubItems[9].Text=(DateTime.Parse(objArchivingValueArr[i].m_strOpenDate)).ToString("yyyy��M��d��");
//					}
//					else if(m_objDomain.m_blnIfOverTime( objArchivingValueArr[i]))
//					{
//						objLsvItemArr[i].SubItems[7].Text="��ʱδ�鵵";																											   
//					}
//					else 
//					{
//						objLsvItemArr[i].SubItems[7].Text="�ȴ��鵵";	
//					}
//				}
//				m_lsvRecords.Items.AddRange(objLsvItemArr);
//				for (int i=0;i<m_lsvRecords.Items.Count;i++)
//				{
//					if(strCurrentID!=objArchivingValueArr[i].m_strInPatientID)
//					{
//						clsPatient objPat = new clsPatient(objArchivingValueArr[i].m_strInPatientID);
//						m_lsvRecords.Items[i].SubItems[0].Text=objArchivingValueArr[i].m_strInPatientID;
//						m_lsvRecords.Items[i].SubItems[1].Text=objArchivingValueArr[i].m_strInPatientName;
//						strCurrentID=objArchivingValueArr[i].m_strInPatientID;
//						m_lsvRecords.Items[i].SubItems[2].Text=objPat.m_StrSex;
//						m_lsvRecords.Items[i].SubItems[3].Text=objPat.m_ObjPeopleInfo.m_StrAge;
//						m_lsvRecords.Items[i].SubItems[4].Text=objPat.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
//						m_lsvRecords.Items[i].SubItems[6].Text=(DateTime.Parse(objArchivingValueArr[i].m_strInPatientEndDate)).ToString("yyyy��M��d��");
//					}
//				}
//				
//				
//			}
//			else
//			{
//				ArrayList arlArchiving=new ArrayList();
//				for(int i=0;i<objArchivingValueArr.Length;i++)
//				{
//					if(!(objArchivingValueArr[i].m_strIfArchived=="1" || m_objDomain.m_blnIfOverTime( objArchivingValueArr[i])))
//					{
//						ListViewItem objLsvItem=new ListViewItem(new string[]{"","","","","","","","","",""});
//						objLsvItem.SubItems[3].Text="�ȴ��鵵";	
//						if(strCurrentID!=objArchivingValueArr[i].m_strInPatientID)
//						{
//							clsPatient objPat = new clsPatient(objArchivingValueArr[i].m_strInPatientID);
//							objLsvItem.SubItems[0].Text=objArchivingValueArr[i].m_strInPatientID;
//							objLsvItem.SubItems[1].Text=objArchivingValueArr[i].m_strInPatientName;
//							objLsvItem.SubItems[5].Text=(DateTime.Parse(objArchivingValueArr[i].m_strInPatientDate)).ToString("yyyy��M��d��");
//							strCurrentID=objArchivingValueArr[i].m_strInPatientID;
//							objLsvItem.SubItems[2].Text=objPat.m_StrSex;
//							objLsvItem.SubItems[3].Text=objPat.m_ObjPeopleInfo.m_StrAge;
//							objLsvItem.SubItems[4].Text=objPat.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
//							objLsvItem.SubItems[6].Text=(DateTime.Parse(objArchivingValueArr[i].m_strInPatientEndDate)).ToString("yyyy��M��d��");
//							m_lsvRecords.Items.Add(objLsvItem);
//							arlArchiving.Add(objArchivingValueArr[i]);
//						}						
//
//					}
//					
//				}
//				m_objInPatientArchivingValueArr=(clsInPatientArchivingValue[])arlArchiving.ToArray(typeof(clsInPatientArchivingValue));
//				
//			}
//			this.Cursor=Cursors.Default;

		}
		#endregion

		#region �鵵
		private long m_lngSetArchived(clsInPatientArchivingValue objArchivingValue)
		{
			string[] strIsArchived = null;
			long lngRes=m_objDomain.lngSetArchived(objArchivingValue.m_strInPatientID,objArchivingValue.m_strInPatientDate,out strIsArchived);
			if(lngRes<=0)
			{
				return 0;
			}
			if(lngRes == 10 && strIsArchived != null)
			{
				if(strIsArchived[0] != null && strIsArchived[0] != "")
				MessageBox.Show(objArchivingValue.m_strInPatientID+"�Ĳ�������"+strIsArchived[0]+"��"+strIsArchived[1]+"�鵵���޷��ظ��鵵��");
			}
			return 1;
		}


		#endregion



		/// <summary>
		/// Clean up any resources being used.
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_lsvRecords = new System.Windows.Forms.ListView();
            this.m_clmInPatientID = new System.Windows.Forms.ColumnHeader();
            this.m_clmInPatientName = new System.Windows.Forms.ColumnHeader();
            this.m_clmSex = new System.Windows.Forms.ColumnHeader();
            this.m_clmAge = new System.Windows.Forms.ColumnHeader();
            this.m_clmBed = new System.Windows.Forms.ColumnHeader();
            this.m_clmInPatientDate = new System.Windows.Forms.ColumnHeader();
            this.m_clmOutDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.m_ctmRecords = new System.Windows.Forms.ContextMenu();
            this.m_mniArchive = new System.Windows.Forms.MenuItem();
            this.m_mniUnarchive = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem54 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.menuItem48 = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.m_chkMultiPatient = new System.Windows.Forms.CheckBox();
            this.m_txtMultiName = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtMultiID = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lstDept2 = new System.Windows.Forms.CheckedListBox();
            this.m_chkAllDept = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rdbNotArchive = new System.Windows.Forms.RadioButton();
            this.m_rdbHasArchive = new System.Windows.Forms.RadioButton();
            this.m_rdbAll = new System.Windows.Forms.RadioButton();
            this.m_cmdFrech = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.menuItem56 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.m_pnlNewBase.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(492, 208);
            this.lblSex.Size = new System.Drawing.Size(24, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(564, 208);
            this.lblAge.Size = new System.Drawing.Size(36, 19);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(152, 208);
            this.lblBedNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblBedNoTitle.Text = "��  ��:";
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(152, 232);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(324, 208);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(448, 208);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(520, 208);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(164, 152);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(392, 60);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(208, 228);
            this.txtInPatientID.Size = new System.Drawing.Size(112, 23);
            this.txtInPatientID.TabIndex = 100;
            this.txtInPatientID.Visible = false;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(360, 204);
            this.m_txtPatientName.Size = new System.Drawing.Size(84, 23);
            this.m_txtPatientName.TabIndex = 110;
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(208, 204);
            this.m_txtBedNO.Size = new System.Drawing.Size(88, 23);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(208, 148);
            this.m_cboArea.Size = new System.Drawing.Size(176, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(364, 228);
            this.m_lsvPatientName.Size = new System.Drawing.Size(84, 104);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(208, 228);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(64, 10);
            this.m_cboDept.Size = new System.Drawing.Size(176, 23);
            this.m_cboDept.SelectedIndexChanged += new System.EventHandler(this.m_cboDept_SelectedIndexChanged);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(16, 12);
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(520, 240);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdNext.Location = new System.Drawing.Point(296, 204);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdPre.Location = new System.Drawing.Point(288, 11);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.AutoSize = true;
            this.m_lblForTitle.Location = new System.Drawing.Point(292, 212);
            this.m_lblForTitle.Size = new System.Drawing.Size(63, 14);
            this.m_lblForTitle.Text = "�����鵵";
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(522, -27);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(567, -33);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // m_lsvRecords
            // 
            this.m_lsvRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvRecords.BackColor = System.Drawing.Color.White;
            this.m_lsvRecords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_clmInPatientID,
            this.m_clmInPatientName,
            this.m_clmSex,
            this.m_clmAge,
            this.m_clmBed,
            this.m_clmInPatientDate,
            this.m_clmOutDate,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.m_lsvRecords.ContextMenu = this.m_ctmRecords;
            this.m_lsvRecords.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvRecords.ForeColor = System.Drawing.Color.Black;
            this.m_lsvRecords.FullRowSelect = true;
            this.m_lsvRecords.Location = new System.Drawing.Point(8, 56);
            this.m_lsvRecords.Name = "m_lsvRecords";
            this.m_lsvRecords.Size = new System.Drawing.Size(779, 560);
            this.m_lsvRecords.TabIndex = 501;
            this.m_lsvRecords.TabStop = false;
            this.m_lsvRecords.UseCompatibleStateImageBehavior = false;
            this.m_lsvRecords.View = System.Windows.Forms.View.Details;
            this.m_lsvRecords.SelectedIndexChanged += new System.EventHandler(this.m_lsvRecords_SelectedIndexChanged);
            // 
            // m_clmInPatientID
            // 
            this.m_clmInPatientID.Text = "סԺ��";
            this.m_clmInPatientID.Width = 87;
            // 
            // m_clmInPatientName
            // 
            this.m_clmInPatientName.Text = "��������";
            this.m_clmInPatientName.Width = 76;
            // 
            // m_clmSex
            // 
            this.m_clmSex.Text = "�Ա�";
            this.m_clmSex.Width = 48;
            // 
            // m_clmAge
            // 
            this.m_clmAge.Text = "����";
            // 
            // m_clmBed
            // 
            this.m_clmBed.Text = "����";
            this.m_clmBed.Width = 50;
            // 
            // m_clmInPatientDate
            // 
            this.m_clmInPatientDate.Text = "��Ժ����";
            this.m_clmInPatientDate.Width = 119;
            // 
            // m_clmOutDate
            // 
            this.m_clmOutDate.Text = "��Ժ����";
            this.m_clmOutDate.Width = 117;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "״̬";
            this.columnHeader4.Width = 88;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "�鵵��";
            this.columnHeader5.Width = 77;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "�鵵����";
            this.columnHeader6.Width = 140;
            // 
            // m_ctmRecords
            // 
            this.m_ctmRecords.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mniArchive,
            this.m_mniUnarchive,
            this.menuItem53,
            this.menuItem3,
            this.menuItem5,
            this.menuItem8,
            this.menuItem9,
            this.menuItem12,
            this.menuItem54,
            this.menuItem43,
            this.menuItem44,
            this.menuItem45,
            this.menuItem55,
            this.menuItem1,
            this.menuItem41,
            this.menuItem56,
            this.menuItem57});
            // 
            // m_mniArchive
            // 
            this.m_mniArchive.Index = 0;
            this.m_mniArchive.Text = "�鵵";
            this.m_mniArchive.Click += new System.EventHandler(this.m_mniArchive_Click);
            // 
            // m_mniUnarchive
            // 
            this.m_mniUnarchive.Index = 1;
            this.m_mniUnarchive.Text = "�����鵵";
            this.m_mniUnarchive.Click += new System.EventHandler(this.m_mniUnarchive_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 2;
            this.menuItem53.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "סԺ����";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "���̼�¼";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 5;
            this.menuItem8.Text = "��ǰС��";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 6;
            this.menuItem9.Text = "������¼��";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 7;
            this.menuItem12.Text = "��Ժ��¼";
            // 
            // menuItem54
            // 
            this.menuItem54.Index = 8;
            this.menuItem54.Text = "-";
            // 
            // menuItem43
            // 
            this.menuItem43.Index = 9;
            this.menuItem43.Text = "��Ժ��������";
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 10;
            this.menuItem44.Text = "�� �� ��";
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 11;
            this.menuItem45.Text = "һ�㻤���¼";
            // 
            // menuItem55
            // 
            this.menuItem55.Index = 12;
            this.menuItem55.Text = "-";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 13;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem15,
            this.menuItem30,
            this.menuItem39,
            this.menuItem40});
            this.menuItem1.Text = "ҽ������վ";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem6,
            this.menuItem7,
            this.menuItem10,
            this.menuItem11,
            this.menuItem13,
            this.menuItem14});
            this.menuItem2.Text = "��������";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "סԺ����ģʽ2";
            this.menuItem4.Visible = false;
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.Text = "�����¼";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.Text = "����֪��ͬ����";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.Text = "ICUת���¼";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 4;
            this.menuItem11.Text = "ICUת����¼";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 5;
            this.menuItem13.Text = "סԺ������ҳ";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 6;
            this.menuItem14.Text = "�����������ֱ�";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 1;
            this.menuItem15.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem16,
            this.menuItem17,
            this.menuItem18,
            this.menuItem19,
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem23,
            this.menuItem24,
            this.menuItem25,
            this.menuItem26,
            this.menuItem27,
            this.menuItem28,
            this.menuItem29});
            this.menuItem15.Text = "��  ��  ��";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 0;
            this.menuItem16.Text = "B�ͳ������������뵥";
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 1;
            this.menuItem17.Text = "CT������뵥";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 2;
            this.menuItem18.Text = "X�����뵥";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 3;
            this.menuItem19.Text = "SPECT������뵥";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 4;
            this.menuItem20.Text = "��ѹ���������뵥";
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 5;
            this.menuItem21.Text = "���������֯�ͼ쵥";
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 6;
            this.menuItem22.Text = "MRI���뵥";
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 7;
            this.menuItem23.Text = "�ĵ�ͼ���뵥";
            this.menuItem23.Visible = false;
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 8;
            this.menuItem24.Text = "���Զർ˯��ͼ������뵥";
            this.menuItem24.Visible = false;
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 9;
            this.menuItem25.Text = "��ҽѧ������뵥";
            this.menuItem25.Visible = false;
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 10;
            this.menuItem26.Text = "ʵ���Ҽ������뵥";
            this.menuItem26.Visible = false;
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 11;
            this.menuItem27.Text = "ʵ���Ҽ��鱨�浥";
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 12;
            this.menuItem28.Text = "Ӱ�񱨸浥";
            // 
            // menuItem29
            // 
            this.menuItem29.Index = 13;
            this.menuItem29.Text = "Ӱ��ԤԼ��ѯ";
            this.menuItem29.Visible = false;
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 2;
            this.menuItem30.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem31,
            this.menuItem32,
            this.menuItem33,
            this.menuItem34,
            this.menuItem35,
            this.menuItem36,
            this.menuItem37,
            this.menuItem38});
            this.menuItem30.Text = "��������ϵͳ";
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 0;
            this.menuItem31.Text = "SIRS�������";
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 1;
            this.menuItem32.Text = "����Glasgow��������";
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 2;
            this.menuItem33.Text = "���Է���������";
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 3;
            this.menuItem34.Text = "������Σ�ز�������";
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 4;
            this.menuItem35.Text = "С��Σ�ز�������";
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 5;
            this.menuItem36.Text = "APACHEII ����";
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 6;
            this.menuItem37.Text = "APACHEIII ����";
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 7;
            this.menuItem38.Text = "TISS-28����";
            // 
            // menuItem39
            // 
            this.menuItem39.Index = 3;
            this.menuItem39.Text = "���Ʒ���";
            // 
            // menuItem40
            // 
            this.menuItem40.Index = 4;
            this.menuItem40.Text = "ȫ�ײ���";
            this.menuItem40.Visible = false;
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 14;
            this.menuItem41.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem42,
            this.menuItem46,
            this.menuItem47,
            this.menuItem48,
            this.menuItem49,
            this.menuItem50,
            this.menuItem51,
            this.menuItem52});
            this.menuItem41.Text = "��ʿ����վ";
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 0;
            this.menuItem42.Text = "���˻�������ά��";
            // 
            // menuItem46
            // 
            this.menuItem46.Index = 1;
            this.menuItem46.Text = "�۲���Ŀ��¼��";
            // 
            // menuItem47
            // 
            this.menuItem47.Index = 2;
            this.menuItem47.Text = "Σ�ػ��߻����¼";
            // 
            // menuItem48
            // 
            this.menuItem48.Index = 3;
            this.menuItem48.Text = "Σ��֢�໤�����ػ���¼��";
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 4;
            this.menuItem49.Text = "ICUΣ�ػ��߻����¼";
            this.menuItem49.Visible = false;
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 5;
            this.menuItem50.Text = "���������¼";
            // 
            // menuItem51
            // 
            this.menuItem51.Index = 6;
            this.menuItem51.Text = "������е�����ϵ�����";
            // 
            // menuItem52
            // 
            this.menuItem52.Index = 7;
            this.menuItem52.Text = "����ICU���������Ƽ໤��¼��";
            // 
            // m_chkMultiPatient
            // 
            this.m_chkMultiPatient.Checked = true;
            this.m_chkMultiPatient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkMultiPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkMultiPatient.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_chkMultiPatient.Location = new System.Drawing.Point(348, 84);
            this.m_chkMultiPatient.Name = "m_chkMultiPatient";
            this.m_chkMultiPatient.Size = new System.Drawing.Size(124, 24);
            this.m_chkMultiPatient.TabIndex = 120;
            this.m_chkMultiPatient.Text = "��ʾ�������";
            this.m_chkMultiPatient.Visible = false;
            this.m_chkMultiPatient.CheckedChanged += new System.EventHandler(this.m_chkMultiPatient_CheckedChanged);
            // 
            // m_txtMultiName
            // 
            this.m_txtMultiName.AccessibleDescription = "���";
            this.m_txtMultiName.BackColor = System.Drawing.Color.White;
            this.m_txtMultiName.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtMultiName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMultiName.ForeColor = System.Drawing.SystemColors.Window;
            this.m_txtMultiName.Location = new System.Drawing.Point(472, 236);
            this.m_txtMultiName.Name = "m_txtMultiName";
            this.m_txtMultiName.Size = new System.Drawing.Size(100, 23);
            this.m_txtMultiName.TabIndex = 111;
            this.m_txtMultiName.Visible = false;
            this.m_txtMultiName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMultiName_KeyDown);
            // 
            // m_txtMultiID
            // 
            this.m_txtMultiID.AccessibleDescription = "���";
            this.m_txtMultiID.BackColor = System.Drawing.Color.White;
            this.m_txtMultiID.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtMultiID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtMultiID.ForeColor = System.Drawing.SystemColors.Window;
            this.m_txtMultiID.Location = new System.Drawing.Point(336, 236);
            this.m_txtMultiID.Name = "m_txtMultiID";
            this.m_txtMultiID.Size = new System.Drawing.Size(100, 23);
            this.m_txtMultiID.TabIndex = 101;
            this.m_txtMultiID.Text = "%";
            this.m_txtMultiID.Visible = false;
            this.m_txtMultiID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMultiID_KeyDown);
            // 
            // m_lstDept2
            // 
            this.m_lstDept2.BackColor = System.Drawing.Color.White;
            this.m_lstDept2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lstDept2.ForeColor = System.Drawing.Color.Black;
            this.m_lstDept2.Location = new System.Drawing.Point(312, 312);
            this.m_lstDept2.Name = "m_lstDept2";
            this.m_lstDept2.Size = new System.Drawing.Size(84, 22);
            this.m_lstDept2.TabIndex = 10000000;
            this.m_lstDept2.Visible = false;
            // 
            // m_chkAllDept
            // 
            this.m_chkAllDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_chkAllDept.Location = new System.Drawing.Point(524, 9);
            this.m_chkAllDept.Name = "m_chkAllDept";
            this.m_chkAllDept.Size = new System.Drawing.Size(112, 24);
            this.m_chkAllDept.TabIndex = 130;
            this.m_chkAllDept.Text = "ȫ����������";
            this.m_chkAllDept.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_chkAllDept.CheckedChanged += new System.EventHandler(this.m_chkAllDept_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_rdbNotArchive);
            this.panel1.Controls.Add(this.m_rdbHasArchive);
            this.panel1.Controls.Add(this.m_rdbAll);
            this.panel1.Location = new System.Drawing.Point(248, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 28);
            this.panel1.TabIndex = 10000004;
            // 
            // m_rdbNotArchive
            // 
            this.m_rdbNotArchive.Checked = true;
            this.m_rdbNotArchive.Location = new System.Drawing.Point(4, 4);
            this.m_rdbNotArchive.Name = "m_rdbNotArchive";
            this.m_rdbNotArchive.Size = new System.Drawing.Size(96, 24);
            this.m_rdbNotArchive.TabIndex = 0;
            this.m_rdbNotArchive.TabStop = true;
            this.m_rdbNotArchive.Text = "δ�鵵����";
            this.m_rdbNotArchive.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_rdbHasArchive
            // 
            this.m_rdbHasArchive.Location = new System.Drawing.Point(100, 4);
            this.m_rdbHasArchive.Name = "m_rdbHasArchive";
            this.m_rdbHasArchive.Size = new System.Drawing.Size(112, 24);
            this.m_rdbHasArchive.TabIndex = 0;
            this.m_rdbHasArchive.Text = "�ѹ鵵�Ĳ���";
            this.m_rdbHasArchive.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_rdbAll
            // 
            this.m_rdbAll.Location = new System.Drawing.Point(212, 4);
            this.m_rdbAll.Name = "m_rdbAll";
            this.m_rdbAll.Size = new System.Drawing.Size(56, 24);
            this.m_rdbAll.TabIndex = 0;
            this.m_rdbAll.Text = "ȫ��";
            this.m_rdbAll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_cmdFrech
            // 
            this.m_cmdFrech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdFrech.DefaultScheme = true;
            this.m_cmdFrech.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdFrech.ForeColor = System.Drawing.Color.Black;
            this.m_cmdFrech.Hint = "";
            this.m_cmdFrech.Location = new System.Drawing.Point(708, 7);
            this.m_cmdFrech.Name = "m_cmdFrech";
            this.m_cmdFrech.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdFrech.Size = new System.Drawing.Size(68, 28);
            this.m_cmdFrech.TabIndex = 10000005;
            this.m_cmdFrech.Text = "ˢ  ��";
            this.m_cmdFrech.Click += new System.EventHandler(this.m_cmdFrech_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(779, 2);
            this.label1.TabIndex = 10000006;
            // 
            // menuItem56
            // 
            this.menuItem56.Index = 15;
            this.menuItem56.Text = "-";
            // 
            // menuItem57
            // 
            this.menuItem57.Index = 16;
            this.menuItem57.Text = "ȫ�ײ���";
            this.menuItem57.Click += new System.EventHandler(this.menuItem57_Click);
            // 
            // frmInPatientCaseHistoryArchiving
            // 
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(800, 621);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmdFrech);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lsvRecords);
            this.Controls.Add(this.m_txtMultiID);
            this.Controls.Add(this.m_txtMultiName);
            this.Controls.Add(this.m_chkMultiPatient);
            this.Controls.Add(this.m_lstDept2);
            this.Controls.Add(this.m_chkAllDept);
            this.Name = "frmInPatientCaseHistoryArchiving";
            this.Text = "�����鵵";
            this.Load += new System.EventHandler(this.frmInPatientCaseHistoryArchiving_Load);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_chkAllDept, 0);
            this.Controls.SetChildIndex(this.m_lstDept2, 0);
            this.Controls.SetChildIndex(this.m_chkMultiPatient, 0);
            this.Controls.SetChildIndex(this.m_txtMultiName, 0);
            this.Controls.SetChildIndex(this.m_txtMultiID, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_lsvRecords, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_cmdFrech, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void m_mniArchive_Click(object sender, System.EventArgs e)
		{
			if(m_lsvRecords.SelectedItems.Count == 0)
			{
				MessageBox.Show("��ѡ��Ҫ�鵵�Ĳ�����");
				m_lsvRecords.Focus();
				return;
            }
            ListView.SelectedListViewItemCollection objItemArr = m_lsvRecords.SelectedItems;
				if(MessageBox.Show("��ע�⣡�鵵֮�󽫲�����������޸Ĳ������Ƿ������","����",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
					return;
			int intFault = 0;
			int intOK = 0;
			try
			{
				this.Cursor=Cursors.WaitCursor;
                for (int i = 0 ; i < objItemArr.Count ; i++)
				{
                    clsInPatientArchivingValue objValue = objItemArr[i].Tag as clsInPatientArchivingValue;
					if(objValue == null)
					{
						intFault++;
						continue;
					}
					if(objValue.m_strIfArchived == "1")
					{
						intFault++;
						continue;
					}
					long lng = m_lngSetArchived(objValue);
					if(lng <= 0)
						intFault++;
					else 
						intOK++;
				}
			}
			finally
			{
				this.Cursor=Cursors.Default;
			}
			m_mthLoadRecordOfOnePatient();
			MessageBox.Show("�鵵�ɹ���"+intOK.ToString()+"����" +Environment.NewLine+"�鵵ʧ�ܣ�"+intFault.ToString()+"����"+Environment.NewLine+"ע�⣺�޷��ظ��鵵��");
		}

		private void m_lsvRecords_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            for (int i = 0 ; i < m_ctmRecords.MenuItems.Count ; i++)
            {
                m_ctmRecords.MenuItems[i].Visible = true;
            }
            if (m_lsvRecords.SelectedItems.Count != 1)
            {
                for (int i = 0 ; i < m_ctmRecords.MenuItems.Count ; i++)
                {
                    if (m_ctmRecords.MenuItems[i].Text != "�����鵵" && m_ctmRecords.MenuItems[i].Text != "�鵵")
                        m_ctmRecords.MenuItems[i].Visible = false;
                }
                return;
            }
            clsInPatientArchivingValue objValue = m_lsvRecords.SelectedItems[0].Tag as clsInPatientArchivingValue;
            if (objValue != null)
            {
                if (objValue.m_strIfArchived == "1")
                {
                    for (int i = 0 ; i < m_ctmRecords.MenuItems.Count ; i++)
                    {
                        if (m_ctmRecords.MenuItems[i].Text != "�����鵵" || objValue.m_strArchivingChangeUserID.Trim() != clsEMRLogin.LoginInfo.m_strEmpID.Trim())
                            m_ctmRecords.MenuItems[i].Visible = false;
                    }
                }
                else
                {
                    m_mniUnarchive.Visible = false;
                }
            }
		}

		private void m_mniUnarchive_Click(object sender, System.EventArgs e)
		{
			if(m_lsvRecords.SelectedItems.Count == 0)
			{
				MessageBox.Show("��ѡ��Ҫȡ���鵵�Ĳ�����");
				m_lsvRecords.Focus();
				return;
			}
            ListView.SelectedListViewItemCollection objItemArr = m_lsvRecords.SelectedItems;
			int intFault = 0;
			int intOK = 0;
			try
			{
				this.Cursor=Cursors.WaitCursor;
                for (int i = 0 ; i < objItemArr.Count ; i++)
				{
                    clsInPatientArchivingValue objValue = objItemArr[i].Tag as clsInPatientArchivingValue;
					if(objValue == null)
					{
						intFault++;
						continue;
					}
					if(objValue.m_strIfArchived == "0")
					{
						intFault++;
						continue;
					}
					if(objValue.m_strArchivingChangeUserID.Trim() != clsEMRLogin.LoginInfo.m_strEmpID.Trim())
					{
						intFault++;
						continue;
					}
					long lng = m_objDomain.lngCancelArchived(objValue.m_strInPatientID,objValue.m_strInPatientDate);
					if(lng <= 0)
						intFault++;
					else 
						intOK++;
				}
			}
			finally
			{
				this.Cursor=Cursors.Default;
			}
			m_mthLoadRecordOfOnePatient();
			MessageBox.Show("ȡ���鵵��"+intOK.ToString()+"����" +Environment.NewLine+"ȡ���鵵ʧ�ܣ�"+intFault.ToString()+"����"+Environment.NewLine+"ע�⣺�޷�ȡ��û�й鵵�ͱ��˹鵵�Ĳ�����");
		}

		private void m_chkWaitToArchiveOnly_CheckedChanged(object sender, System.EventArgs e)
		{
//			m_blnWaitToArchiveOnly=m_chkWaitToArchiveOnly.Checked;
		}

		private void m_chkMultiPatient_CheckedChanged(object sender, System.EventArgs e)
		{
//			m_blnMultiPatient=m_chkMultiPatient.Checked;
//			if(m_blnMultiPatient)
//			{
//				m_txtMultiID.Visible=true;
//				m_txtMultiName.Visible=true;
//				txtInPatientID.Visible=false;
//				m_txtPatientName.Visible=false;
//				lblAge.Visible=false;
//				lblAgeTitle.Visible=false;
//				lblSex.Visible=false;
//				lblSexTitle.Visible=false;
//				m_strInPatientID=null;
//				m_strInPatientName=null;
//				m_txtMultiID.Text=txtInPatientID.Text;
//				m_txtMultiName.Text=m_txtPatientName.Text;
//			}
//			else
//			{
//				m_txtMultiID.Visible=false;
//				m_txtMultiName.Visible=false;
//				txtInPatientID.Visible=true;
//				m_txtPatientName.Visible=true;
//				lblAge.Visible=true;
//				lblAgeTitle.Visible=true;
//				lblSex.Visible=true;
//				lblSexTitle.Visible=true;
//				m_objBaseCurrentPatient=null;
//				txtInPatientID.Text=m_txtMultiID.Text;
//				m_txtPatientName.Text=m_txtMultiName.Text;
//			}
		}

		private void m_txtMultiName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
//			if(e.KeyValue==13)
//			{
//				this.Cursor=Cursors.WaitCursor;
//				m_blnIsByIDLike=false;
//				m_strInPatientName=m_txtMultiName.Text;
//				m_mthLoadRecordOfMultiPatient(m_blnIsByIDLike);
//				this.Cursor=Cursors.Default;
//			}
		}

		private void m_txtMultiID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
//			if(e.KeyValue==13)
//			{
//				this.Cursor=Cursors.WaitCursor;
//				m_blnIsByIDLike=true;
//				m_strInPatientID=m_txtMultiID.Text;
//				m_mthLoadRecordOfMultiPatient(m_blnIsByIDLike);
//				this.Cursor=Cursors.Default;
//			}
		}


		private string[] m_strGetSelectedDeptIDArr()
		{
			string[] strDeptIDArr=null;
			int intCount = m_cboDept.GetItemsCount();
			if(intCount>0 )
			{
				if(m_chkAllDept.Checked)
				{
					strDeptIDArr=new string[intCount];
					for(int i=0;i<intCount;i++)
					{				
						strDeptIDArr[i]=((clsDepartment)m_cboDept.GetItem(i)).m_strDeptNewID;					
					}
				}
				else
				{
					strDeptIDArr = new string[1];
					strDeptIDArr[0] = ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID;
				}
			}
			return strDeptIDArr;
		}

		/// <summary>
		/// ����Ҫ������ʾ
		/// </summary>
		protected override void m_mthAddFormStatusForClosingSave()
		{
		}

		#region �򿪼�¼��
		/// <summary>
		/// ��ʼ���Ҽ���
		/// </summary>
		private void m_mthInitContextMenu()
		{
			foreach(MenuItem mniSub in m_ctmRecords.MenuItems)
				m_mthAssociateItemEvent(mniSub);

			m_mthLoadCustomForms();
		}

		/// <summary>
		/// �����¼�
		/// </summary>
		/// <param name="p_mniParent"></param>
		private void m_mthAssociateItemEvent(MenuItem p_mniParent)
		{
			if(p_mniParent.MenuItems.Count == 0 && p_mniParent.Text != "�鵵" && p_mniParent.Text != "�����鵵")
				p_mniParent.Click += new EventHandler(m_mthMenuItem_Click);

			for(int i = 0; i < p_mniParent.MenuItems.Count; i++)
			{
				m_mthAssociateItemEvent(p_mniParent.MenuItems[i]);
			}			
		}

		private void m_mthMenuItem_Click(object sender, System.EventArgs e)
		{
			Form frmRecord = m_frmGetForm((MenuItem)sender);

			m_mthOpenForm(frmRecord);
		}

		/// <summary>
		/// �򿪼�¼��
		/// </summary>
		/// <param name="p_frmRecord"></param>
		private void m_mthOpenForm(Form p_frmRecord)
		{
			if(m_lsvRecords.SelectedItems.Count <= 0)
				return;
			clsInPatientArchivingValue objValue = m_lsvRecords.SelectedItems[0].Tag as clsInPatientArchivingValue;
			if(objValue == null)
				return;
				this.Cursor=Cursors.WaitCursor;
			//���±�����Ӧ��ͬ��������Ϣ
                clsPatient objNew = new clsPatient(objValue.m_strInPatientID, objValue.m_strPatientID, objValue.m_strDeptID, objValue.m_strAreaID, objValue.m_strBedName);
			objNew.m_DtmSelectedInDate = DateTime.Parse(objValue.m_strInPatientDate);
//			clsPatientInBedInfo objNewBed = new clsPatientInBedInfo(objNew);
//			string strNewDept = "";
//			string strNewArea = "";
//			string strNewBed = "";
//			if(objNewBed != null && objNewBed.m_ObjLastSessionInfo != null)
//			{
//				strNewDept = objNewBed.m_strNewDeptIDForSearch;
//				strNewArea = objNewBed.m_strNewAreaIDForSearch;
//				if(objNewBed.m_ObjLastRoomInfo.m_intGetBedCount() > 0)
//					strNewBed = objNewBed.m_ObjLastRoomInfo.m_objGetBedByIndex(0).m_ObjBed.m_StrBedName;
//			}
//			clsPatient objSelectPatient = new clsPatient(m_lsvRecords.SelectedItems[0].SubItems[0].Text.Trim(),true);
//			objSelectPatient.m_strDeptNewID = strNewDept;
//			objSelectPatient.m_strAreaNewID = strNewArea;
//			objSelectPatient.m_strBedCode = strNewBed;
			//			objSelectPatient.m_DtmSelectedInDate = DateTime.Parse(((RecordSearch.clsRecordSearchDomain.clsPatientList)m_lsvPatientList.SelectedItems[0].Tag).m_strInPatientDate);			 

			try
			{
				p_frmRecord.MdiParent = this.MdiParent;
				p_frmRecord.WindowState = FormWindowState.Maximized;
				p_frmRecord.Show(); 
				frmHRPBaseForm frmRecord = p_frmRecord as frmHRPBaseForm;
				if(frmRecord != null)
				{
					frmRecord.m_mthSetPatient(objNew);
					MDIParent.s_ObjCurrentPatient = objNew;
				}
			}
			catch
			{}
			finally
			{
				this.Cursor=Cursors.Default;
			}
		}

		/// <summary>
		/// ��ȡָ���Ĵ���
		/// </summary>
		/// <param name="p_mniForm"></param>
		/// <returns></returns>
		private Form m_frmGetForm(MenuItem p_mniForm)
		{
			#region �Ҽ��˵�ȫ������

			switch(p_mniForm.Text) 
			{
				case "סԺ����":
					//					return new frmInPatientCaseHistory(new clsPatient(m_lsvRecords.SelectedItems[0].SubItems[0].Text.Trim()));			
					return new frmInPatientCaseHistory();
//				case "סԺ����ģʽ2":
//					return new frmInPatientCaseHistoryMode1();					
				case "���̼�¼":
					return new frmSubDiseaseTrack();					
				case "SPECT������뵥":
					return new frmSPECT();					
				case "��ѹ���������뵥":
					return new frmHighOxygen();					
				case "B�ͳ������������뵥":
					return new frmBUltrasonicCheckOrder();
				case "CT������뵥":
					return new frmCTCheckOrder();
				case "X�����뵥":
					return new frmXRayCheckOrder();
				case "���������֯�ͼ쵥":
					return new frmPathologyOrgCheckOrder();
				case "MRI���뵥":
					return new frmMRIApply();
				case "ʵ���Ҽ������뵥":
					return new frmLabAnalysisOrder();
				case "ʵ���Ҽ��鱨�浥":
					return new frmLabCheckReport();
				case "����֪��ͬ����":
					return new frmOperationAgreedRecord();
				case "��ǰС��":
					return new frmBeforeOperationSummary();
				case "������¼��":
					return new frmOperationRecordDoctor();
				case "ICUת���¼":
					return new frmPICUShiftInForm();
				case "ICUת����¼":
					return new frmPICUShiftOutForm();
				case "SIRS�������":
					return new frmSIRSEvaluation();
				case "����Glasgow��������":
					return new frmImproveGlasgowComaEvaluation();
				case "���Է���������":
					return new frmLungInjuryEvaluation();
				case "������Σ�ز�������":
					return new frmNewBabyInjuryCaseEvaluation();
				case "С��Σ�ز�������":
					return new frmBabyInjuryCaseEvaluation();
				case "APACHEII ����":
					return new frmAPACHEIIValuation();
				case "APACHEIII ����":
					return new frmAPACHEIIIValuation();
				case "TISS-28����":
					return new frmTISSValuation();
				case "���Ʒ���":
					return new frmICUTrend();
				case "סԺ������ҳ":
					return new frmInHospitalMainRecord();
				case "�����������ֱ�":
					return new frmQCRecord();
				case "��Ժ��������":
					return new frmInPatientEvaluate();
				case "�� �� ��":
					return new frmThreeMeasureRecord();
				case "һ�㻤���¼":
					return new frmMainGeneralNurseRecord();
				case "�۲���Ŀ��¼��":
					return new frmWatchItemTrack();
				case "Σ�ػ��߻����¼":
					return new frmIntensiveTendMain();
				case "ICUΣ�ػ��߻����¼":
					return new frmICUIntensiveTendRecord();
				case "���������¼":
					return new frmOperationRecord();
				case "������е�����ϵ�����":
					return new frmOperationEquipmentQty();
				case "��Ժ��¼":
					return new frmOutHospital();
				case "�����¼":
					return new frmConsultation();
				case "Σ��֢�໤�����ػ���¼��":
					return new frmMainICUIntensiveTend();
				case "����ICU���������Ƽ໤��¼��":
					return new frmMainICUBreath();
				case "Ӱ�񱨸浥":
					return new frmImageReport();
				case "Ӱ��ԤԼ��ѯ":
					return new frmImageBookingSearch();	
				case "�ĵ�ͼ���뵥":
					return new iCare.frmEKGOrder();
				case "���Զർ˯��ͼ������뵥":
					return new iCare.frmNuclearOrder();
				case "��ҽѧ������뵥":
					return new iCare.frmPSGOrder();
				case "������Ժ������":
					return new frmEMR_InPatientEvaluate();
				case "һ�㻼�߻����¼":
					return new frmGeneralNurseRecord_GX();
                //case "ȫ�ײ���":
                //    return new frmInPatientCaseHistory_SetForm1();
			}
			#endregion �Ҽ��˵�ȫ������

			return null;
		}

		#region �Զ����
		private clsCustom_SubmitValue[] m_objCustomForms;
		/// <summary>
		/// Load���Զ����
		/// </summary>
		private void m_mthLoadCustomForms()
		{
			long lngRes = new iCare.CustomForm.clsCustomFormDomain().m_lngGetSubmitForms(MDIParent.OperatorID,clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID,out m_objCustomForms);
			if(lngRes <= 0 || m_objCustomForms == null || m_objCustomForms.Length == 0)
				return;
			MenuItem mniRoot = new MenuItem("�Զ����");
			for(int i = 0; i < m_objCustomForms.Length; i++)
			{
				MenuItem mniCustormForm = mniRoot.MenuItems.Add(m_objCustomForms[i].m_strFormName,new EventHandler(m_mthShowCustomForm));				
			}
			m_ctmRecords.MenuItems.Add(mniRoot);
		}

		/// <summary>
		/// ���Զ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_mthShowCustomForm(object sender,EventArgs e)
		{			
			if(m_lsvRecords.SelectedItems.Count <= 0)
				return;

//			clsPatient objSelectPatient = new clsPatient(m_lsvRecords.SelectedItems[0].SubItems[0].Text.Trim());

			try
			{
//				this.Cursor=Cursors.WaitCursor;
//				iCare.CustomForm.frmCustomFormBase frmChild = new iCare.CustomForm.frmCustomFormBase(m_objCustomForms[((MenuItem)sender).Index]);
//				frmChild.MdiParent =this.MdiParent;
//				frmChild.Show(); 
//				frmChild.m_mthSetPatient(objSelectPatient);
//				MDIParent.s_ObjCurrentPatient = objSelectPatient;
//				this.Cursor=Cursors.Default;
			}
			catch
			{}
		}
		#endregion

		private void m_cboDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_mthLoadRecordOfOnePatient();
		}


		#endregion

		private void m_chkAllDept_CheckedChanged(object sender, System.EventArgs e)
		{
			m_cboDept.Enabled = !m_chkAllDept.Checked;
		}

		private void m_cmdFrech_Click(object sender, System.EventArgs e)
		{
			m_mthLoadRecordOfOnePatient();
		}

		private void frmInPatientCaseHistoryArchiving_Load(object sender, System.EventArgs e)
		{
			if(m_cboDept.GetItemsCount() > 0)
				m_cboDept.SelectedIndex = 0;
		}

        private void menuItem57_Click(object sender, EventArgs e)
        {
            //frmInPatientCaseHistory_SetForm1 frmCS = new frmInPatientCaseHistory_SetForm1();
            //frmCS.strDeptID = ((clsDepartment)(this.m_cboDept.SelectedItem)).m_strDeptNewID;
            //frmCS.strBedNO = this.m_lsvRecords.SelectedItems[0].SubItems[4].Text.Trim();
            //frmCS.dtmInPatientDate = Convert.ToDateTime(this.m_lsvRecords.SelectedItems[0].SubItems[5].Text.Trim());
            //frmCS.Show();
        }

        //protected override clsPatient[] m_objGetPatientByBedNO()
        //{
            //string strId = "";
            //if (this.m_cboDept.SelectedItem != null)
            //    strId = ((clsDepartment)(this.m_cboDept.SelectedItem)).m_strDeptNewID;
            //com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objServ = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        //clsEmrBed_VO[] objResultArr = null;
            //long lngRes = 0;
            //string strBedNO = this.m_lsvRecords.SelectedItems[0].SubItems[4].ToString().Trim();
            //lngRes = objServ.m_lngGetBedInfoLikeBedCode(strId, false, strBedNO, out objResultArr);
            //if (lngRes > 0 && objResultArr != null)
            //{
            //    clsPatient[] objPatientArr = new clsPatient[objResultArr.Length];
            //    for (int i = 0; i < objResultArr.Length; i++)
            //        objPatientArr[i] = new clsPatient(objResultArr[i]);
            //    return objPatientArr;
            //}
            //return null;
        //}

	}

	
	
}


