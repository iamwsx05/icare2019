using System;
using System.Windows.Forms;
using System.Collections;
using weCare.Core.Entity;
using System.Reflection; 

namespace iCare
{
	/// <summary>
	/// Summary description for clsTemplatesetInvoke.
	/// </summary>
	public class clsTemplatesetInvoke
	{
		public clsTemplatesetInvoke()
		{			
			m_objTransTemplate=new clsTransferTemplate();

			for(int i=0;i<m_objControlCollection.Length ;i++)
			{
				m_objControlCollection[i]=null;
				m_strControlIDCollection[i]="-------%%%%$#####";
			}
			
		}

		private clsTransferTemplate  m_objTransTemplate;
		/// <summary>
		/// ����ģ�幤��
		/// </summary>
		public clsTransferTemplate m_ObjTransTemplate
		{
			get
			{
				return m_objTransTemplate;
			}
		}

        /// <summary>
        /// ����TextBox��ComboBox�ؼ�
        /// </summary>
        Control[] m_objControlCollection = new Control[500];
		string [] m_strControlIDCollection=new string[500];

		private int m_intTemplateNumber;
        /// <summary>
        /// ����ģ��Ŀؼ��ĸ���--wf20080109
        /// </summary>
        public int m_IntTemplateNumber
        {
            get
            {
                return m_intTemplateNumber;
            }
            set
            {
                m_intTemplateNumber = value;
            }
        }
//		public void m_mthInitTemplateControls(Form p_frmInput)
//		{
////			m_objTransTemplate.m_mthInitTemplateControls (p_frmInput );
//		}

        /// <summary>
        /// ��ӿɱ���/����ģ��Ŀؼ�(TextBox��ComboBox)
        /// </summary>
        /// <param name="p_frmInput"></param>
        /// <param name="p_txtInput"></param>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strControlID"></param>
        public void m_mthAddTextBox(Form p_frmInput, Control p_txtInput, string p_strFormID, string p_strControlID)
		{
			m_objTransTemplate.m_mthSetParent (this);
			m_objTransTemplate.m_mthAddTextBox (p_frmInput,p_txtInput,p_strFormID,p_strControlID);	
			m_objControlCollection [m_intTemplateNumber]=p_txtInput ;
			m_strControlIDCollection [m_intTemplateNumber]=p_strControlID ;
			m_intTemplateNumber++;
		}

		/// <summary>
		/// ģ�����ݸ�ֵ
		/// </summary>
		/// <param name="p_objTemplateSetContent">ģ������</param>
		/// <param name="p_intIndex">����ֵ�����</param>
		/// <param name="p_intHash">��Ҫ���м�������ݵ�RichTextBox��hashCode</param>
		public void m_mthLoadTemplateset(clsTemplatesetContentValue [] p_objTemplateSetContent,int p_intIndex,int p_intHash)
		{
			if(p_objTemplateSetContent ==null || p_objTemplateSetContent.Length <=0)return;

			#region �滻���ݸ�������
			ArrayList arlTemp = new ArrayList();
			for(int i = 0; i < p_objTemplateSetContent.Length; i++)
				arlTemp.Add(p_objTemplateSetContent[i].m_strContent);
			string[] strContentArr = (string[])arlTemp.ToArray(typeof(string));
            com.digitalwave.Emr.Utility.DataShare.clsDataShareReplace.s_mthReplaceDataShareValue(MDIParent.s_ObjCurrentPatient,ref strContentArr);
			#endregion

            for (int i = 0; i < p_objTemplateSetContent.Length; i++)
            {
                for (int j = 0; j < m_intTemplateNumber; j++)
                {
                    Control txtBox = m_objControlCollection[j];
                    string strContent = strContentArr[i];
                    if (txtBox != null && p_objTemplateSetContent[i].m_strControl_ID == m_strControlIDCollection[j])
                    {
                        switch (txtBox.GetType().FullName)
                        {
                            case "System.Windows.Forms.RichTextBox":
                            case "iCare.CustomForm.ctlRichTextBox":
                                if (p_intIndex == -1)
                                {
                                    txtBox.Text = strContent;
                                }
                                else
                                {
                                    txtBox.Text = txtBox.Text.Replace("mb.", "");
                                    if (txtBox.GetHashCode() == p_intHash)
                                    {
                                        txtBox.Text = txtBox.Text.Insert(p_intIndex, strContent);
                                    }
                                    else
                                        txtBox.Text += strContent;
                                }
                                break;
                            case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                                com.digitalwave.Utility.Controls.ctlRichTextBox ctlTxt = (com.digitalwave.Utility.Controls.ctlRichTextBox)txtBox;

                                int intMaxLength = ctlTxt.MaxLength;
                                if (strContent != null && strContent.Length > intMaxLength)
                                {
                                    strContent = strContent.Substring(0, intMaxLength);
                                }
                                if (p_intIndex == -1)
                                {
                                    //���������¼�����������������ݣ����������ݲ���
                                    if (txtBox.FindForm().Name == "frmOperationRecordDoctor" && txtBox.Name == "m_txtOperationName" && txtBox.Text.Trim() != "")
                                        break;

                                    //int intLength = ctlTxt.Text.Length;
                                    ctlTxt.m_mthClearText();
                                    ctlTxt.m_mthInsertText(strContent, 0);

                                }
                                else
                                {
                                    ctlTxt.m_mthReplace("mb.", "");

                                    if (txtBox.GetHashCode() == p_intHash)
                                    {
                                        ctlTxt.m_mthInsertText(strContent, p_intIndex);
                                    }
                                    else
                                    {
                                        int intLength = ctlTxt.Text.Length;
                                        ctlTxt.m_mthInsertText(strContent, intLength);
                                    }
                                }
                                break;
                            case "com.digitalwave.controls.ctlRichTextBox":
                                com.digitalwave.controls.ctlRichTextBox ctlTxt2 = (com.digitalwave.controls.ctlRichTextBox)txtBox;

                                int intMaxLength2 = ctlTxt2.MaxLength;
                                if (strContent != null && strContent.Length > intMaxLength2)
                                {
                                    strContent = strContent.Substring(0, intMaxLength2);
                                }
                                if (p_intIndex == -1)
                                {
                                    //���������¼�����������������ݣ����������ݲ���
                                    if (txtBox.FindForm().Name == "frmOperationRecordDoctor" && txtBox.Name == "m_txtOperationName" && txtBox.Text.Trim() != "")
                                        break;

                                    //int intLength1 = ctlTxt2.Text.Length;
                                    ctlTxt2.m_mthClearText();
                                    ctlTxt2.m_mthInsertText(strContent, 0);

                                }
                                else
                                {
                                    ctlTxt2.m_mthReplace("mb.", "");

                                    if (txtBox.GetHashCode() == p_intHash)
                                    {
                                        ctlTxt2.m_mthInsertText(strContent, p_intIndex);
                                    }
                                    else
                                    {
                                        int intLength1 = ctlTxt2.Text.Length;
                                        ctlTxt2.m_mthInsertText(strContent, intLength1);
                                    }
                                }
                                break;
                            case "com.digitalwave.Utility.Controls.ctlComboBox":
                                com.digitalwave.Utility.Controls.ctlComboBox ctlBox = txtBox as com.digitalwave.Utility.Controls.ctlComboBox;
                                ctlBox.Text = strContent;
                                break;
                            default:
                                txtBox.Text = strContent;
                                break;
                        }
                        break;
                    }
                }
            }

//			clsDataShareTool.s_mthSetDataShare(MDIParent.s_ObjCurrentPatient,m_objControlCollection);


		}
		/// <summary>
		/// �ͷ����б����������������Դ
		/// ע�⣺��ȷ������Ҳ����Ҫʹ�ô˶���
		/// </summary>
		public void Release()
		{
			if(m_objTransTemplate != null)
				m_objTransTemplate.Release();

			Type type = this.GetType();

			//ֻɨ�豾���ֶ�
			BindingFlags flags = BindingFlags.Instance |  BindingFlags.Public | 
				BindingFlags.NonPublic  ;//| BindingFlags.DeclaredOnly ;

			FieldInfo[] fields = type.GetFields(flags);

			foreach(FieldInfo field in fields)
			{
				object obj = field.GetValue(this);
				if (obj == null)
					continue;
				
				Type t = obj.GetType();	         
				
				try
				{
					//�ų������ÿյ�����
					if (!t.IsValueType && (t.ToString().IndexOf("Native") < 0 ))
						field.SetValue(this, null);									
				}
				catch
				{
					//BUG	
					MessageBox.Show("������Ա���������" + t.ToString() + "�ӵ�Release()���ų�����ȥ��");
				}
			}	

//			this.Dispose(true);
		}

        /// <summary>
        /// ģ�����ݸ�ֵ
        /// </summary>
        /// <param name="p_objTemplateSetContent">ģ������</param>
        /// <param name="p_ctlText">Ӧ�ó���ֵ�Ŀؼ�</param>
        public void m_mthLoadTemplateset(weCare.Core.Entity.clsTemplate[] p_objTemplateSetContent, Control p_ctlText)
        {
            if (p_objTemplateSetContent == null || p_objTemplateSetContent.Length <= 0 || p_ctlText == null) return;

            int intIndex = m_objTransTemplate.m_intGetControlSelectionStart(p_ctlText);
            int intHashCode = p_ctlText.GetHashCode();

            #region �滻���ݸ�������
            string[] strContentArr = new string[p_objTemplateSetContent.Length];
            for (int i = 0; i < p_objTemplateSetContent.Length; i++)
            {
                strContentArr[i] = p_objTemplateSetContent[i].m_strContent;
            }

            com.digitalwave.Emr.Utility.DataShare.clsDataShareReplace.s_mthReplaceDataShareValue(MDIParent.s_ObjCurrentPatient, ref strContentArr);
            #endregion

            for (int i = 0; i < p_objTemplateSetContent.Length; i++)
            {
                for (int j = 0; j < m_intTemplateNumber; j++)
                {
                    Control txtBox = m_objControlCollection[j];
                    string strContent = strContentArr[i];
                    if (txtBox != null && p_objTemplateSetContent[i].m_strControlID == m_strControlIDCollection[j])
                    {
                        switch (txtBox.GetType().FullName)
                        {
                            case "System.Windows.Forms.RichTextBox":
                            case "System.Windows.Forms.TextBox":
                            case "iCare.CustomForm.ctlRichTextBox":
                                if (intIndex == -1)
                                {
                                    txtBox.Text = strContent;
                                }
                                else
                                {
                                    txtBox.Text = txtBox.Text.Replace("mb.", "");
                                    if (txtBox.GetHashCode() == intHashCode)
                                    {
                                        txtBox.Text = txtBox.Text.Insert(intIndex, strContent);
                                    }
                                    else
                                        txtBox.Text += strContent;
                                }
                                break;
                            case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                                com.digitalwave.Utility.Controls.ctlRichTextBox ctlTxt = (com.digitalwave.Utility.Controls.ctlRichTextBox)txtBox;

                                int intMaxLength = ctlTxt.MaxLength;
                                if (strContent != null && strContent.Length > intMaxLength)
                                {
                                    strContent = strContent.Substring(0, intMaxLength);
                                }
                                if (intIndex == -1)
                                {
                                    //���������¼�����������������ݣ����������ݲ���
                                    if (txtBox.FindForm().Name == "frmOperationRecordDoctor" && txtBox.Name == "m_txtOperationName" && txtBox.Text.Trim() != "")
                                        break;

                                    ctlTxt.m_mthClearText();
                                    ctlTxt.m_mthInsertText(strContent, 0);
                                }
                                else
                                {
                                    ctlTxt.m_mthReplace("mb.", "");

                                    if (txtBox.GetHashCode() == intHashCode)
                                    {
                                        ctlTxt.m_mthInsertText(strContent, intIndex);
                                    }
                                    else
                                    {
                                        int intLength = ctlTxt.Text.Length;
                                        ctlTxt.m_mthInsertText(strContent, intLength);
                                    }
                                }
                                break;
                            case "com.digitalwave.controls.ctlRichTextBox":
                                com.digitalwave.controls.ctlRichTextBox ctlTxt2 = (com.digitalwave.controls.ctlRichTextBox)txtBox;

                                int intMaxLength2 = ctlTxt2.MaxLength;
                                if (strContent != null && strContent.Length > intMaxLength2)
                                {
                                    strContent = strContent.Substring(0, intMaxLength2);
                                }
                                if (intIndex == -1)
                                {
                                    //���������¼�����������������ݣ����������ݲ���
                                    if (txtBox.FindForm().Name == "frmOperationRecordDoctor" && txtBox.Name == "m_txtOperationName" && txtBox.Text.Trim() != "")
                                        break;

                                    ctlTxt2.m_mthClearText();
                                    ctlTxt2.m_mthInsertText(strContent, 0);

                                }
                                else
                                {
                                    ctlTxt2.m_mthReplace("mb.", "");

                                    if (txtBox.GetHashCode() == intHashCode)
                                    {
                                        ctlTxt2.m_mthInsertText(strContent, intIndex);
                                    }
                                    else
                                    {
                                        int intLength1 = ctlTxt2.Text.Length;
                                        ctlTxt2.m_mthInsertText(strContent, intLength1);
                                    }
                                }
                                break;
                            case "System.Windows.Forms.ComboBox":
                            case "com.digitalwave.Utility.Controls.ctlComboBox":
                                com.digitalwave.Utility.Controls.ctlComboBox ctlBox = txtBox as com.digitalwave.Utility.Controls.ctlComboBox;
                                ctlBox.Text = strContent;
                                break;
                            default:
                                txtBox.Text = strContent;
                                break;
                        }
                        break;
                    }
                }
            }
        }

	}
}
