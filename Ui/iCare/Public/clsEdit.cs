using System;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using com.digitalwave.Emr.StaticObject;
using com.digitalwave.emr.AssistModule;

namespace iCare
{
	/// <summary>
	/// �༭:���ڵ��Ӳ�����ǰ�Ӵ���ı��桢ɾ������ӡ�ȹ�������
	/// </summary>
	public class clsEdit
	{
		public clsEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//	
		}
        /// <summary>
        /// ����ǩ����֤
        /// </summary>
		public void Verify()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
				return;
            try
            {
			    Cursor.Current =Cursors.WaitCursor;				
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Verify(); 
			
            }
            finally
            {
                Cursor.Current =Cursors.Default;

            }
		}
        /// <summary>
        /// ���浱ǰ��
        /// </summary>
		public void Save()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
				return;
            try
            {
                Cursor.Current =Cursors.WaitCursor;				
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Save(); 
            }
            finally
            {
                Cursor.Current =Cursors.Default;
            }
			
			
		}


        /// <summary>
        /// ɾ����ǰѡ�����¼
        /// </summary>
		public void Delete()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
				return;
            try
            {
			    Cursor.Current =Cursors.WaitCursor;				
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Delete(); 

            }
            finally
            {
			    Cursor.Current =Cursors.Default;

            }
		}

		/// <summary>
		/// ��ѯ��ɾ����¼
		/// </summary>
        public void LoadDeletedRecord()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
                {
                    frmHRPBaseForm frmHrp = (frmHRPBaseForm)clsEMRLogin.s_FrmMDI.ActiveMdiChild;
                    if (frmHrp.m_blnIsNewSetInactiveForm)
                    {
                            m_mthOpenInactiveView(frmHrp.m_ObjCurrentEmrPatientSession, frmHrp);
                    }
                    else
                    {
                        frmRecordsBase frmRecords = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmRecordsBase;

                        if (frmRecords != null && frmRecords.m_FrmCurrentSub != null && frmRecords.m_FrmCurrentSub is frmHRPBaseForm)
                        {
                            ((frmHRPBaseForm)(frmRecords.m_FrmCurrentSub)).m_mthSearchDeactiveInfo();
                        }
                        else if (clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
                        {
                            ((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_mthSearchDeactiveInfo();
                        }
                    }
                }
                else if (clsEMRLogin.s_FrmMDI.ActiveMdiChild is infInactiveRecord)
                {
                    m_mthOpenInactiveView(((infInactiveRecord)clsEMRLogin.s_FrmMDI.ActiveMdiChild).m_objGetSessionInfo, clsEMRLogin.s_FrmMDI.ActiveMdiChild);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }
        private void m_mthOpenInactiveView(weCare.Core.Entity.clsEmrPatientSessionInfo_VO p_objSessionInfo, Form p_frmSelectedForm)
        {
            if (p_objSessionInfo == null) return;
            frmInactiveView frm = new frmInactiveView(p_objSessionInfo, (infInactiveRecord)p_frmSelectedForm);
            frm.ShowDialog(p_frmSelectedForm);
        }
        /// <summary>
        /// ��ӡԤ��
        /// </summary>
		public void Preview()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null )
				return;

			frmRecordsBase frmRecords = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmRecordsBase;

			if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
			{
				//����ʹ���Ӵ��壬����ӡ��
				return;
			}
            try
            {
			    Cursor.Current =Cursors.WaitCursor;
			    if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
				    ((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_BlnDirectPrint = false;
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Print(); 

            }
            finally
            {
			    Cursor.Current =Cursors.Default;

            }
		}
        /// <summary>
        /// ��ӡ
        /// </summary>
		public void Print()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null )
				return;


			frmRecordsBase frmRecords = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmRecordsBase;

			if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
			{
				//����ʹ���Ӵ��壬����ӡ��
				return;
			}
            try
            {
			    Cursor.Current =Cursors.WaitCursor;
                if (clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
                {
                    if (((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_objBaseCurrentPatient != null
                        && ((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_objBaseCurrentPatient.m_IntCharacter == 1)
                    {
                        bool blnIsCase = false;
                        if (clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleNameArr != null)
                        {
                            int intRolesCount = clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleNameArr.Length;
                            for (int i = 0; i < intRolesCount; i++)
                            {
                                if (clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleNameArr[i] == "������")
                                {
                                    blnIsCase = true;
                                    break;
                                }
                            }
                        }
                        if (!blnIsCase)
                        {
                            clsPublicFunction.ShowInformationMessageBox("�˲��˲���Ϊֻ�������ܴ�ӡ��");
                            return;
                        }
                    }
                    ((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_BlnDirectPrint = true;
                }
                
                PrintDialog pdSet = new PrintDialog();
                if (string.IsNullOrEmpty(pdSet.PrinterSettings.PrinterName))
                {
                    pdSet.ShowDialog(clsEMRLogin.s_FrmMDI.ActiveMdiChild);
                }

                if (string.IsNullOrEmpty(pdSet.PrinterSettings.PrinterName))//�رմ�ӡ���ú��ٴ��ж�
                {
                    clsPublicFunction.ShowInformationMessageBox("����ָ��һ̨��ӡ����");
                    return;
                }

			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Print(); 

            }
            finally
            {
			    Cursor.Current =Cursors.Default;

            }
		}
        /// <summary>
        /// ����
        /// </summary>
		public void Cut()
		{
            //��Ϊ�ۼ������ԭ�����μ��й���
            //if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
            //    return;

            //Cursor.Current =Cursors.WaitCursor;				
            //((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Cut(); 
            //Cursor.Current =Cursors.Default;
		}
        /// <summary>
        /// ����
        /// </summary>
		public void Copy()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
				return;
            try
            {
			    Cursor.Current =Cursors.WaitCursor;				
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Copy(); 

            }
            finally
            {
			    Cursor.Current =Cursors.Default;

            }
		}
        /// <summary>
        /// ճ��
        /// </summary>
		public void Paste()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null && !(clsEMRLogin.s_FrmMDI.ActiveMdiChild is PublicFunction))
				return;
            try
            {
			    Cursor.Current =Cursors.WaitCursor;				
			    ((PublicFunction)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).Paste(); 

            }
            finally
            {
			    Cursor.Current =Cursors.Default;

            }
		}
        /// <summary>
        /// ��������
        /// </summary>
		public void Undo()
		{
			switch(clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().Name)
			{
				case "dwtBorderTextBox":
				case "ctlBorderTextBox":
				case "TextBox":
				case "RichTextBox":
					((TextBoxBase)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).Undo();
					break;
                case "ctlRichTextBox":
                    {
                        if(clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().FullName == 
                            "com.digitalwave.Utility.Controls.ctlRichTextBox")
                            ((ctlRichTextBox)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).m_mthUndo();
                        else if(clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().FullName == 
                            "com.digitalwave.controls.ctlRichTextBox")
                            ((com.digitalwave.controls.ctlRichTextBox)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).m_mthUndo();
                    }
                    break;
			}
		}
        /// <summary>
        /// ����
        /// </summary>
		public void Redo()
		{
			switch(clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().Name)
			{
				case "RichTextBox":
					((RichTextBox)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).Redo();
					break;
				case "dwtBorderTextBox":
				case "ctlBorderTextBox":
				case "TextBox":
					((TextBoxBase)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).Undo();
					break;
				case "ctlRichTextBox":
                    {
                        if (clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().FullName ==
                            "com.digitalwave.Utility.Controls.ctlRichTextBox")
                            ((ctlRichTextBox)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).m_mthRedo();
                        else if (clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl.GetType().FullName ==
                            "com.digitalwave.controls.ctlRichTextBox")
                            ((com.digitalwave.controls.ctlRichTextBox)clsEMRLogin.s_FrmMDI.ActiveMdiChild.ActiveControl).m_mthRedo();
                    }
					break;
			}
		}
        /// <summary>
        /// �˳�
        /// </summary>
		public void Exit()
		{
			Form activeform;			
				
			if (clsEMRLogin.s_FrmMDI.ActiveMdiChild  == null)
				clsEMRLogin.s_FrmMDI.Close();
			else
			{
				Cursor.Current =Cursors.WaitCursor;
				activeform=clsEMRLogin.s_FrmMDI.ActiveMdiChild ;
				activeform.Close() ; 
				Cursor.Current =Cursors.Default;
			}
		}


		/// <summary>
		/// ����ģ��
		/// </summary>
		public void NewTemplate()
		{
			if (clsEMRLogin.s_FrmMDI.ActiveMdiChild  != null)
			{
				frmRecordsBase frmRecords = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmRecordsBase;

				if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
				{
					frmRecords.m_FrmCurrentSub.m_mthNewTemplateWithThis();						
				}
				else if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
				{
					((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_mthNewTemplate(); 
				}

			}
		}

		/// <summary>
		/// ���ɳ���ֵģ��
		/// </summary>
		public void NewCommonUseTemplate()
		{
			if (clsEMRLogin.s_FrmMDI.ActiveMdiChild  != null)
			{
				frmRecordsBase frmRecords = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmRecordsBase;

				if(frmRecords != null && frmRecords.m_FrmCurrentSub != null)
				{
					frmRecords.m_FrmCurrentSub.m_mthNewCommonUseWithThis();						
				}
				else if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
				{
					((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_mthNewCommonUse(); 
				}

			}
		}

		/// <summary>
		/// ����Ĭ��ֵ
		/// </summary>
		public void SetDefault()
		{
			if (clsEMRLogin.s_FrmMDI.ActiveMdiChild  != null)
			{
				Form frmParent = clsEMRLogin.s_FrmMDI.ActiveMdiChild;

				clsDefaultValueTool objTool = null;

				if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmRecordsBase)
				{
					frmRecordsBase frmActive = (frmRecordsBase)clsEMRLogin.s_FrmMDI.ActiveMdiChild;
					if(frmActive.m_FrmCurrentSub != null)
					{
						objTool = new clsDefaultValueTool(frmActive.m_FrmCurrentSub);
						frmParent = frmActive.m_FrmCurrentSub;
					}
				}
				else if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
					objTool = new clsDefaultValueTool(clsEMRLogin.s_FrmMDI.ActiveMdiChild,MDIParent.s_ObjCurrentPatient);	

				if(objTool != null)
				{
					if(clsPublicFunction.ShowQuestionMessageBox(frmParent,"ע�⣡����Ĭ��ֵ�󽫻Ḳ��ԭ����Ĭ��ֵ���������ܻ��������ݻ��ң���δȷ�����������Ĭ��ֵ�Ƿ�Ϊ������Ĭ��ֵʱ���벻Ҫ��㱣�棬�Ƿ������") == DialogResult.Yes)
						objTool.m_mthSaveDefaultValue();
				}

			}
		}

		/// <summary>
		/// ����Ĭ��ֵ
		/// </summary>
		public void ResetDefault()
		{
			if (clsEMRLogin.s_FrmMDI.ActiveMdiChild  != null)
			{					
				Form frmParent = clsEMRLogin.s_FrmMDI.ActiveMdiChild;

				clsDefaultValueTool objTool = null;

				if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmRecordsBase)
				{
					frmRecordsBase frmActive = (frmRecordsBase)clsEMRLogin.s_FrmMDI.ActiveMdiChild;
					if(frmActive.m_FrmCurrentSub != null)
					{
						objTool = new clsDefaultValueTool(frmActive.m_FrmCurrentSub,MDIParent.s_ObjCurrentPatient);
						frmParent = frmActive.m_FrmCurrentSub;
					}
				}
						else if(clsEMRLogin.s_FrmMDI.ActiveMdiChild is frmHRPBaseForm)
					objTool = new clsDefaultValueTool(clsEMRLogin.s_FrmMDI.ActiveMdiChild,MDIParent.s_ObjCurrentPatient);

				if(objTool != null)
				{
					if(clsPublicFunction.ShowQuestionMessageBox(frmParent,"�Ƿ�����Ĭ��ֵ��") == DialogResult.Yes)
					{
						objTool.m_BlnReplaceDataShare = false;
						objTool.m_mthSetDefaultValue();
					}
				}

			}
		}

		
		public void Approve()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null)
				return;

			Cursor.Current =Cursors.WaitCursor;
			try
			{
				//���Ӵ�����ΪfrmHRPBaseForm				
				((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_lngApprove(); 
			}
			catch(InvalidCastException)//���ʹ�����Ϊ��Щ���岻�Ǽ̳�frmHRPBaseForm��
			{
			}
			Cursor.Current =Cursors.Default;
		}

		public void Unapprove()
		{
			if(clsEMRLogin.s_FrmMDI.ActiveMdiChild==null)
				return;

			Cursor.Current =Cursors.WaitCursor;
			try
			{				
				((frmHRPBaseForm)(clsEMRLogin.s_FrmMDI.ActiveMdiChild)).m_lngUnApprove(); 
			}
			catch(InvalidCastException)
			{
			}
			Cursor.Current =Cursors.Default;
		}

	}
}
