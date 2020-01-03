using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using com.digitalwave.GLS_WS;

namespace iCare.FormUtility
{
	/// <summary>
	/// ��ʾ���屣���޸�
    /// ԭ������һ����ϣ���Ÿ������塣
    /// ����key��valueΪһ����ϣ���ù�ϣ���Ÿô������пؼ���ֵ״̬
	/// </summary>
	public class clsSaveCue
	{
        #region ����
        /// <summary>
        /// ģ�������򣬲���Ҫ��¼���ж�״̬��
        /// </summary>
        private const string c_strTemplateTextBox = "m_txtInputKeyword";
        /// <summary>
        /// ģ���ѡ��򣬲���Ҫ��¼���ж�״̬��
        /// </summary>
        private const string c_strTemplateListBox = "m_lstTemplate";

        /// <summary>
        /// �Զ���������⴦��
        /// </summary>
        private const string c_strCustomForm = "frmCustomForm_";

        /// <summary>
        /// ������Ϣ������Ϊkey������״̬��HashtableΪvalue��
        /// ��ϣ�� ����
        /// </summary>
        private Hashtable m_hasFormInfo; 
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsSaveCue()
        {
            //
            // TODO: Add constructor logic here
            //
            m_hasFormInfo = new Hashtable();
        } 
        #endregion

        #region ��¼����״̬
        /// <summary>
        /// ��Ӵ���
        /// ��Ӵ��嵽�������С�Ĭ��ֵΪnull
        /// ͬʱ�󶨴���Ĺر��¼�
        /// </summary>
        /// <param name="p_frmNeedSaveCue"></param>
        public void m_mthAddForm(Form p_frmNeedSaveCue)
        {
            try
            {
                if (p_frmNeedSaveCue == null)
                    return;

                //���洰��
                if (m_hasFormInfo.Contains(p_frmNeedSaveCue))
                    return;
                else
                {
                    //ȡ�������ƣ���ǰסԺ�ţ���ʵ��һ������ֻ�ܴ�ͬ��һ�����幦��
                    string strHash = p_frmNeedSaveCue.Name + com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strINPATIENTID_CHR;
                    m_hasFormInfo.Add(strHash.GetHashCode(), null);
                }

                if (!(p_frmNeedSaveCue is frmApplyReportBase))
                    //��ӹرմ���
                    p_frmNeedSaveCue.Closing += new CancelEventHandler(m_mthHandleFormClosing);
   }
            catch 
            {
                //��û�����õ�ǰ�����׳�
            }
        }

        /// <summary>
        /// ��Ӵ����Ͽؼ�״̬
        /// </summary>
        /// <param name="p_frmNeedSaveCue"></param>
        public void m_mthAddFormStatus(Form p_frmNeedSaveCue)
        {
            try
            {
                //ȡ�������ƣ���ǰסԺ�ţ���ʵ��һ������ֻ�ܴ�ͬ��һ�����幦��
                string strHash = p_frmNeedSaveCue.Name + com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strINPATIENTID_CHR;
                if (!m_hasFormInfo.Contains(strHash.GetHashCode()))
                    m_mthAddForm(p_frmNeedSaveCue);

                Hashtable hasStatus = (Hashtable)m_hasFormInfo[strHash.GetHashCode()];
                if (hasStatus == null)
                {
                    hasStatus = new Hashtable();
                    m_hasFormInfo[strHash.GetHashCode()] = hasStatus;
                }
                else
                {
                    hasStatus.Clear();
                }

                //��¼��ǰ״̬

                m_mthAddStatus(p_frmNeedSaveCue, hasStatus);
            }
            catch 
            {
                //��û�����õ�ǰ�����׳�
            }
        }
        /// <summary>
        /// ��¼�ؼ��ĵ�ǰ״̬
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_hasStatus"></param>
        private void m_mthAddStatus(Control p_ctlControl, Hashtable p_hasStatus)
        {
            if (p_ctlControl.Name == c_strTemplateTextBox || p_ctlControl.Name == c_strTemplateListBox)
                return;
            try
            {
                switch (p_ctlControl.GetType().FullName)
                {
                    //�������������
                    //case "System.Windows.Forms.LinkLabel":
                    //case "System.Windows.Forms.Button":
                    //case "System.Windows.Forms.Label":
                    //ϵͳ�Դ�����
                    case "System.Windows.Forms.ComboBox":
                        p_hasStatus.Add(p_ctlControl.Name, p_ctlControl.Text.GetHashCode());
                        break;
                    case "System.Windows.Forms.TextBox":
                        p_hasStatus.Add(p_ctlControl.Name, p_ctlControl.Text.GetHashCode());
                        break;
                    case "System.Windows.Forms.MaskedTextBox":
                        p_hasStatus.Add(p_ctlControl.Name, p_ctlControl.Text.GetHashCode());
                        break;
                    case "System.Windows.Forms.NumericUpDown":
                        p_hasStatus.Add(p_ctlControl.Name, ((System.Windows.Forms.NumericUpDown)p_ctlControl).Value.GetHashCode());
                        break;
                    case "System.Windows.Forms.CheckBox":
                        p_hasStatus.Add(p_ctlControl.Name, ((CheckBox)p_ctlControl).Checked);
                        break;
                    case "System.Windows.Forms.CheckedListBox":
                        break;
                    case "System.Windows.Forms.RadioButton":
                        p_hasStatus.Add(p_ctlControl.Name, ((RadioButton)p_ctlControl).Checked);
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        p_hasStatus.Add(p_ctlControl.Name, ((System.Windows.Forms.DateTimePicker)p_ctlControl).Value.GetHashCode());
                        break;
                    case "System.Windows.Forms.ListView":
                        break;
                    case "System.Windows.Forms.ListBox":
                        break;
                    case "System.Windows.Forms.DataGridTextBox":
                        break;

                    //�Զ����ת������
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        p_hasStatus.Add(((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).Name, ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_strGetRightText().GetHashCode());
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        p_hasStatus.Add(((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).Name, ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_strGetRightText().GetHashCode());
                        break;
                    case "com.digitalwave.Utility.Controls.ctlTimePicker":
                        p_hasStatus.Add(((com.digitalwave.Utility.Controls.ctlTimePicker)p_ctlControl).Name, ((com.digitalwave.Utility.Controls.ctlTimePicker)p_ctlControl).Text.GetHashCode());
                        break;

                    case "com.digitalwave.Utility.Controls.ctlComboBox":
                        p_hasStatus.Add(((com.digitalwave.Utility.Controls.ctlComboBox)p_ctlControl).Name, ((com.digitalwave.Utility.Controls.ctlComboBox)p_ctlControl).Text.GetHashCode());
                        break;
                    case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                        p_hasStatus.Add(((com.digitalwave.Utility.Controls.ctlBorderTextBox)p_ctlControl).Name, ((com.digitalwave.Utility.Controls.ctlBorderTextBox)p_ctlControl).Text.GetHashCode());
                        break;
                    default:
                        break;
                }
                //�Զ���ؼ����ݹ�
                if (p_ctlControl.GetType().FullName.IndexOf("digitalwave") < 0
                    && !(p_ctlControl is System.Windows.Forms.DataGrid && p_ctlControl.Parent is frmRecordsBase))
                {
                    foreach (Control ctlChild in p_ctlControl.Controls)
                    {
                        m_mthAddStatus(ctlChild, p_hasStatus);
                    }
                }

            }
            catch (Exception exp)
            {
                string strError = exp.Message;
            }

        } 
        #endregion

        #region ��֤����״̬
        /// <summary>
        /// ��鴰��״̬�Ƿ��иı�
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_hasStatus"></param>
        /// <returns>û�иı䷵��True�����򷵻�False</returns>
        private bool m_blnCheckStatusSame(Control p_ctlControl, Hashtable p_hasStatus)
        {
            if (p_ctlControl.Name == c_strTemplateTextBox || p_ctlControl.Name == c_strTemplateListBox)
                return true;

            //�Զ��崰�����⴦��
            if (p_ctlControl.Name == c_strCustomForm)
                return m_blnCheckStatusSameOfCustomForm(p_ctlControl, p_hasStatus);

            bool blnRes = true;

                 switch (p_ctlControl.GetType().FullName)
                {
                    //�������������
                    //case "System.Windows.Forms.LinkLabel":
                    //case "System.Windows.Forms.Button":
                    //case "System.Windows.Forms.Label":
                    //ϵͳ�Դ�����
                    case "System.Windows.Forms.ComboBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != p_ctlControl.Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.TextBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != p_ctlControl.Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.MaskedTextBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != p_ctlControl.Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.NumericUpDown":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((System.Windows.Forms.NumericUpDown)p_ctlControl).Value.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.CheckBox":
                        if ((bool)p_hasStatus[p_ctlControl.Name] != ((System.Windows.Forms.CheckBox)p_ctlControl).Checked)
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.CheckedListBox":
                        break;
                    case "System.Windows.Forms.RadioButton":
                        if ((bool)p_hasStatus[p_ctlControl.Name] != ((System.Windows.Forms.RadioButton)p_ctlControl).Checked)
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((System.Windows.Forms.DateTimePicker)p_ctlControl).Value.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "System.Windows.Forms.ListView":
                        break;
                    case "System.Windows.Forms.ListBox":
                        break;
                    case "System.Windows.Forms.DataGridTextBox":
                        break;

                    //�Զ����ת������
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_strGetRightText(true).GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_strGetRightText(true).GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "com.digitalwave.Utility.Controls.ctlTimePicker":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((com.digitalwave.Utility.Controls.ctlTimePicker)p_ctlControl).Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "com.digitalwave.Utility.Controls.ctlComboBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((com.digitalwave.Utility.Controls.ctlComboBox)p_ctlControl).Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                        if ((int)p_hasStatus[p_ctlControl.Name] != ((com.digitalwave.Utility.Controls.ctlBorderTextBox)p_ctlControl).Text.GetHashCode())
                        {
                            blnRes = false;
                            return blnRes;
                        }
                        break;
                    default:
                        break;

                }
            //�Զ���ؼ����ݹ�
                if (p_ctlControl.GetType().FullName.IndexOf("digitalwave") < 0 
                    && !(p_ctlControl is System.Windows.Forms.DataGrid && p_ctlControl.Parent is frmRecordsBase))
            {
                foreach (Control ctlChild in p_ctlControl.Controls)
                {
                    if (!m_blnCheckStatusSame(ctlChild, p_hasStatus))
                        blnRes = false;
                }
            }

            return blnRes;
        }

        private void m_mthHandleFormClosing(object p_objForm, CancelEventArgs p_objArg)
        {
            m_mthHandleSaveCue((Form)p_objForm, p_objArg);

            //			m_hasFormInfo.Remove(p_objForm);
        }

        /// <summary>
        /// �ڼ�¼ѡ��ı�ǰ�����洰���¼��
        /// </summary>
        /// <param name="p_frmNeedSaveCue"></param>
        public void m_mthHandleRecordAfterSelect(Form p_frmNeedSaveCue)
        {
            //			m_mthHandleSaveCue(p_frmNeedSaveCue);
        }

        /// <summary>
        /// ���洰���¼
        /// </summary>
        /// <param name="p_frmNeedSaveCue">��Ҫ����Ĵ���</param>
        private void m_mthHandleSaveCue(Form p_frmNeedSaveCue, CancelEventArgs e)
        {   
            try
            {         
                //ȡ�������ƣ���ǰסԺ�ţ���ʵ��һ������ֻ�ܴ�ͬ��һ�����幦��
                string strHash = p_frmNeedSaveCue.Name + com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strINPATIENTID_CHR;

                Hashtable hasStatus = (Hashtable)m_hasFormInfo[strHash.GetHashCode()];

                if (hasStatus == null)
                    return;

           

            //��鴰��״̬�Ƿ�ı�
            bool blnIsSame = m_blnCheckStatusSame(p_frmNeedSaveCue, hasStatus);


            //����״̬�ı䣬��ʾ���棬����Ҫ���棬���ñ��湦��			
            if (!blnIsSame && p_frmNeedSaveCue.DialogResult != DialogResult.Yes)
            {            
                frmHRPBaseForm frmBase = p_frmNeedSaveCue as frmHRPBaseForm;
                if (frmBase != null)
                {
                    frmBase.m_blnHasClosing = true;
                }
                //if (p_frmNeedSaveCue is frmHRPBaseForm)
                //{
                //    ((frmHRPBaseForm)p_frmNeedSaveCue).m_blnHasClosing = true;
                //}

                DialogResult dlgResult = DialogResult.None;

                if (MDIParent.s_ObjCurrentPatient != null && MDIParent.s_ObjCurrentPatient.m_IntCharacter == 1)
                {
                    dlgResult = DialogResult.No;
                }
                else
                {
             
                    dlgResult = clsPublicFunction.ShowQuestionMessageBox("[" + p_frmNeedSaveCue.Text + "]���˸Ķ����Ƿ񱣴棿", MessageBoxButtons.YesNoCancel);
                }

                if (dlgResult == DialogResult.Yes)
                {
                    #region yes
                    if (p_frmNeedSaveCue is frmPatientInfoManage)
                    {
                        ((frmPatientInfoManage)p_frmNeedSaveCue).m_mthSave();
                    }
                    else if (p_frmNeedSaveCue is iCareBaseForm.frmPatientRecordForm)
                    {
                        ((iCareBaseForm.frmPatientRecordForm)p_frmNeedSaveCue).m_mthSave();
                    }
                    else if (p_frmNeedSaveCue is frmRecordsBase)
                    {
                        ((frmRecordsBase)p_frmNeedSaveCue).Save();
                    }
                    else
                    {
                        //frmHRPBaseForm frmBase = p_frmNeedSaveCue as frmHRPBaseForm;

                        if (frmBase != null)
                        {
                            if (frmBase.m_lngSave() > 0)
                                frmBase.DialogResult = DialogResult.Yes;
                        }
                        else
                        {
                            PublicFunction objPF = p_frmNeedSaveCue as PublicFunction;

                            if (objPF != null)
                            {
                                objPF.Save();
                                if (frmBase != null)
                                    frmBase.DialogResult = DialogResult.Yes;
                            }
                        }
                    }

                    m_hasFormInfo.Remove(strHash.GetHashCode()); 
                    #endregion
                }
                else if (dlgResult == DialogResult.No)
                {
                    #region no
                        m_hasFormInfo.Remove(strHash.GetHashCode()); 
                    #endregion
                }
                else if (dlgResult == DialogResult.Cancel)
                {
                    #region cancel
                    e.Cancel = true;

                    if (frmBase != null)
                    {
                        frmBase.m_blnHasClosing = false;
                    }
                    //if (p_frmNeedSaveCue is frmHRPBaseForm)
                    //{
                    //    ((frmHRPBaseForm)p_frmNeedSaveCue).m_blnHasClosing = false;
                    //} 
                    #endregion
                }
            }
            else
            {
                m_hasFormInfo.Remove(strHash.GetHashCode());
            }
        }
        catch
        {
            //��û�����õ�ǰ�����׳�
            return;
        }
        }

        /// <summary>
        /// ��鴰��״̬�Ƿ��иı�
        /// </summary>
        /// <param name="p_frmNeedSaveCue"></param>
        /// <returns></returns>
        public bool m_blnCheckStatusSame(Form p_frmNeedSaveCue)
        {
            bool blnRes = true;
            try
            {
                //ȡ�������ƣ���ǰסԺ�ţ���ʵ��һ������ֻ�ܴ�ͬ��һ�����幦��
                string strHash = p_frmNeedSaveCue.Name + com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strINPATIENTID_CHR;

                Hashtable hasStatus = (Hashtable)m_hasFormInfo[strHash.GetHashCode()];

                if (hasStatus == null)
                    return true;

                blnRes = m_blnCheckStatusSame(p_frmNeedSaveCue, hasStatus);

            }
            catch 
            {
                //��û�����õ�ǰ�����׳�
            }
            return blnRes;
        }

        /// <summary>
        /// ��鴰��״̬�Ƿ��иı䣬�Զ���������⴦��
        /// </summary>
        /// <returns>û�иı䷵��True�����򷵻�False</returns>
        private bool m_blnCheckStatusSameOfCustomForm(Control p_ctlControl, Hashtable p_hasStatus)
        {
            bool blnRes = true;

            if (p_hasStatus[p_ctlControl] != null)
            {
                switch (p_ctlControl.GetType().Name)
                {
                    case "iCare.CustomForm.ctlCheckBox":
                        if ((bool)p_hasStatus[p_ctlControl.Name] != ((iCare.CustomForm.ctlCheckBox)p_ctlControl).Checked)
                            blnRes = false;
                        return blnRes;
                    default:
                        if ((int)p_hasStatus[p_ctlControl.Name] != p_ctlControl.Text.GetHashCode())
                            blnRes = false;
                        break;
                }
            }

            foreach (Control ctlChild in p_ctlControl.Controls)
            {
                if (!m_blnCheckStatusSameOfCustomForm(ctlChild, p_hasStatus))
                    blnRes = false;
            }

            return blnRes;
        } 
        #endregion

        #region ɾ������״̬
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_frmCue"></param>
        public void m_mthRemoveForm(Form p_frmCue)
        {
            try
            {
                //ȡ�������ƣ���ǰסԺ�ţ���ʵ��һ������ֻ�ܴ�ͬ��һ�����幦��
               string strHash = p_frmCue.Name + com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strINPATIENTID_CHR;
               if (!(p_frmCue is frmApplyReportBase))
                     //��ӹرմ���,ȡ����
                     p_frmCue.Closing -= new CancelEventHandler(m_mthHandleFormClosing);

                //�ӹ�ϣ����ɾ��
                if (m_hasFormInfo.Contains(strHash.GetHashCode()))
                {
                    //�ӹ�ϣ����ɾ��
                    m_hasFormInfo.Remove(strHash.GetHashCode());
                }
             }
             catch 
            {
                //��û�����õ�ǰ�����׳�
            }


        } 
        #endregion


	}
}
