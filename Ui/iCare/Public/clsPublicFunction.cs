using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Emr.Signature_gui;
using System.Data;

namespace iCare
{
    /// <summary>
    /// �ṩһЩ���ú���
    /// </summary>
    public class clsPublicFunction
    {
        public clsPublicFunction()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region MessageBox

        /// <summary>
        /// ����ǩ�����ɹ���ʾmessage
        /// </summary>
        /// <returns></returns>
        public static DialogResult ShowDigitalSignMessageBox()
        {
            return MessageBox.Show("����ǩ�����ɹ�����ȷ���Ƿ����key��", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ����ǩ����֤���ɹ���ʾmessage
        /// </summary>
        /// <returns></returns>
        public static DialogResult ShowVerifySignMessageBox()
        {
            return MessageBox.Show("����ǩ����֤���ɹ�����ȷ���Ƿ����key��", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static DialogResult ShowInformationMessageBox(string strMessage)
        {
            return ShowInformationMessageBox(strMessage, MessageBoxButtons.OK);
        }

        public static DialogResult ShowInformationMessageBox(string strMessage, Form p_frmBase)
        {
            return ShowInformationMessageBox(strMessage, MessageBoxButtons.OK, p_frmBase);
        }

        public static DialogResult ShowInformationMessageBox(string strMessage, MessageBoxButtons buttons)
        {
            return MessageBox.Show(strMessage, "iCare Message", buttons, MessageBoxIcon.Information);
        }

        public static DialogResult ShowInformationMessageBox(string strMessage, MessageBoxButtons buttons, Form p_frmBase)
        {
            return MessageBox.Show(p_frmBase, strMessage, "iCare Message", buttons, MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestionMessageBox(string strMessage)
        {
            return ShowQuestionMessageBox(strMessage, MessageBoxButtons.YesNo);
        }

        public static DialogResult ShowQuestionMessageBox(string strMessage, MessageBoxButtons buttons)
        {
            return MessageBox.Show(strMessage, "iCare Message", buttons, MessageBoxIcon.Question);
        }

        public static DialogResult ShowQuestionMessageBox(IWin32Window p_owner, string strMessage)
        {
            return MessageBox.Show(p_owner, strMessage, "iCare Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void s_mthShowNotPermitMessage()
        {
            ShowInformationMessageBox("�Բ�������Ȩ�޲��㣡");
        }

        public static DialogResult ShowDetailExceptionDialog(Exception err)
        {
            return ShowInformationMessageBox(err.Message.ToString() + "\r\n" + err.StackTrace.ToString());
        }

        public static bool s_blnAskForModify()
        {
            //			return ShowQuestionMessageBox(clsHRPMessage.c_strAskForModify) == DialogResult.Yes;
            return true;
        }

        public static bool s_blnAskForDelete()
        {
            return ShowQuestionMessageBox(clsHRPMessage.c_strAskForDelete) == DialogResult.Yes;
            //			return true;
        }

        #endregion

        #region ����ListView���п�����ʾ���������
        /// <summary>
        /// ����ʾ����������6ʱ����С���һ�еĿ�ȣ�����ʾ���������,Jacky-2003-7-21
        /// </summary>
        /// <param name="p_lsvControl"></param>
        public static void s_mthChangeListViewLastColumnWidth(ListView p_lsvControl)
        {
            if (p_lsvControl.Columns.Count > 0)
            {
                int intLastColumnWidth = p_lsvControl.Width;
                for (int i = 0; i < p_lsvControl.Columns.Count - 1; i++)
                {
                    intLastColumnWidth -= p_lsvControl.Columns[i].Width;
                }
                if (p_lsvControl.Items.Count > 6)
                    intLastColumnWidth -= 18;

                p_lsvControl.Columns[p_lsvControl.Columns.Count - 1].Width = intLastColumnWidth;
            }
        }

        /// <summary>
        /// ����ʾ����������ָ������ʱ����С���һ�еĿ�ȣ�����ʾ���������
        /// </summary>
        /// <param name="p_lsvControl"></param>
        /// <param name="p_intRows">ָ��������</param>
        public static void s_mthChangeListViewLastColumnWidth(ListView p_lsvControl, int p_intRows)
        {
            if (p_lsvControl.Columns.Count > 0)
            {
                int intLastColumnWidth = p_lsvControl.Width;
                for (int i = 0; i < p_lsvControl.Columns.Count - 1; i++)
                {
                    //ʹ���һ���ұ������߸պ������ұ߿�
                    intLastColumnWidth -= p_lsvControl.Columns[i].Width;
                }
                if (p_lsvControl.Items.Count > p_intRows)
                    intLastColumnWidth -= 18;//18���ǹ������Ŀ��

                p_lsvControl.Columns[p_lsvControl.Columns.Count - 1].Width = intLastColumnWidth;
            }
        }
        #endregion ����ListView���п�����ʾ���������

        #region ��鵱ǰ�û��Ĳ���Ȩ��
        /// <summary>
        /// ��鵱ǰ�û��Ĳ���Ȩ��
        /// </summary>
        /// <param name="p_enmSF"></param>
        /// <param name="p_enmOperation"></param>
        /// <returns>����в���Ȩ�޷���true�����򷵻�false</returns>
        public static bool s_blnCheckCurrentPrivilege(enmPrivilegeSF p_enmSF, enmPrivilegeOperation p_enmOperation)
        {
            return true;//s_blnCheckCurrentPrivilege(clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID,p_enmSF,p_enmOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_enmSF"></param>
        /// <param name="p_enmOperation"></param>
        /// <returns>����в���Ȩ�޷���true�����򷵻�false</returns>
        public static bool s_blnCheckCurrentPrivilege(string p_strDeptID, enmPrivilegeSF p_enmSF, enmPrivilegeOperation p_enmOperation)
        {
            //			clsPrivilegeInfo [] objPIArr = clsLoginContext.s_ObjLoginContext.m_ObjPIArr;
            //
            //			if(objPIArr == null)
            //				return false;
            //
            //			for(int i=0;i<objPIArr.Length;i++)
            //			{
            //				if(objPIArr[i] == null)
            //					continue;
            //
            //				//DeptID���Ϊÿ�ŵ���ǰ���˵����ڲ���
            //				if(objPIArr[i].m_objGetOISF(p_strDeptID,(int)p_enmSF,(int)p_enmOperation) != null)
            //					return true;
            //			}

            return true;
        }
        #endregion ��鵱ǰ�û��Ĳ���Ȩ��

        #region �ı��򰴻س�������һ�ؼ�
        /// <summary>
        /// �ı��򰴻س�������һ�ؼ�
        /// </summary>
        /// <param name="p_txtBox"></param>
        public void m_mthSetControlEnter2Tab(System.Windows.Forms.Control p_ctlSender)
        {
            p_ctlSender.KeyDown += new KeyEventHandler(Control_KeyDown);
        }

        public void m_mthSetControlEnter2Tab(System.Windows.Forms.Control[] p_ctlSenderArr)
        {
            foreach (System.Windows.Forms.Control ctl in p_ctlSenderArr)
                ctl.KeyDown += new KeyEventHandler(Control_KeyDown);
        }

        private void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }
        #endregion

        public static void s_mthTextBoxOnlyDigit(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8);
        }
        #region Date Format
        private static readonly DateTime s_dtmEmptyDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// Ĭ������"1900-1-1"
        /// </summary>
        public static DateTime S_DtmEmptyDate
        {
            get { return s_dtmEmptyDate; }
        }
        /// <summary>
        /// ��ȡʱ���ʽ
        /// 0 = "yyyy��MM��dd�� HHʱmm��ss��"
        /// 1 = "yyyy��"
        /// 2 = "yyyy��MM��"
        /// 3 = "yyyy��MM��dd��"
        /// 4 = "yyyy��MM��dd�� HHʱ"
        /// 5 = "yyyy��MM��dd�� HHʱmm��"
        /// 6 = "HHʱmm��ss��"
        /// 7 = "HHʱmm��"
        /// 10 = "yyyy"
        /// 11 = "MM"
        /// 12 = "dd"
        /// 13 = "yyyy-MM"
        /// 14 = "yyyy-MM-dd"
        /// 15 = "yyyy-MM-dd HH"
        /// 16 = "yyyy-MM-dd HH:mm"
        /// 17 = "HH:mm:ss"
        /// Ĭ�� = "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="p_intType"></param>
        /// <returns></returns>
        public static string s_strGetDateTimeFormat(int p_intType)
        {
            switch (p_intType)
            {
                case 0:
                    return "yyyy��MM��dd�� HHʱmm��ss��";
                case 1:
                    return "yyyy��";
                case 2:
                    return "yyyy��MM��";
                case 3:
                    return "yyyy��MM��dd��";
                case 4:
                    return "yyyy��MM��dd�� HHʱ";
                case 5:
                    return "yyyy��MM��dd�� HHʱmm��";
                case 6:
                    return "HHʱmm��ss��";
                case 7:
                    return "HHʱmm��";
                case 10:
                    return "yyyy";
                case 11:
                    return "MM";
                case 12:
                    return "dd";
                case 13:
                    return "yyyy-MM";
                case 14:
                    return "yyyy-MM-dd";
                case 15:
                    return "yyyy-MM-dd HH";
                case 16:
                    return "yyyy-MM-dd HH:mm";
                case 17:
                    return "HH:mm:ss";
                default:
                    break;
            }
            return "yyyy-MM-dd HH:mm:ss";
        }
        #endregion Date Format

        /// <summary>
        /// �жϱ��Ƿ������Ȩ��ɾ������
        /// ��������֤�ϼ�
        /// ���ڷ���ʵ�ʲ����ߣ���ʱֻ�Ƿ���һ����֤��������¼�����ݿ��У��д�����
        /// </summary>
        /// <param name="pDeptID">����ID</param>
        /// <param name="pCreateUserID">��¼������</param>
        /// <param name="pOperatorID">��¼ɾ����</param>
        /// <param name="pType">��¼���� 1ҽ������վ���� 2��ʿ����վ����</param>
        /// <returns>���� ����Ϊ�� �ܾ�Ϊfalse</returns>
        public static bool IsAllowDelete(string pDeptID, string pCreateUserID, clsEmrEmployeeBase_VO pOperator, int pType)
        {
            //������
            if (pDeptID == null || pDeptID.Trim().Length == 0 || pOperator == null)
            {
                MessageBox.Show("��֤�������󣬲��ܼ�������", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //��ʼ��
            bool IsAllow = true;



            //��֤����
            //���Ǳ���ļ���ID����shortno�����²ߡ�����һ��Ƚϣ���һ��ƥ������Ϊ
            if (pCreateUserID.Trim() == pOperator.m_strEMPID_CHR.Trim() || pCreateUserID.Trim() == pOperator.m_strEMPNO_CHR.Trim())
            {

                //��֤ɾ��
                clsDeleteVerify objDeleteVerify = new clsDeleteVerify();
                if (objDeleteVerify.m_mthIsDelete(null, pOperator) == false)
                {
                    MessageBox.Show("Key����֤ʧ�ܣ����ܼ�������", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            else
            {
                #region ���ݹ��Ż�ID��ȡ����
                string strTempEemployeeName = pCreateUserID;
                 
                try
                {
                    DataTable dt = new DataTable();
                    long lngRes = 0;
                    if (pCreateUserID.Trim().Length < 7)//�����������7λ����Ϊ����
                    {
                        lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetEmpByNO(  pCreateUserID.Trim(), out dt);
                        if (dt.Rows.Count > 0)
                        {
                            strTempEemployeeName = dt.Rows[0][1].ToString();
                        }
                    }
                    else
                    {
                        lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetEmpByID(  pCreateUserID.Trim(), out dt);
                        if (dt.Rows.Count > 0)
                        {
                            strTempEemployeeName = dt.Rows[0][1].ToString();
                        }
                    }
                }
                catch (Exception exp)
                {
                    string strMsg = exp.Message;
                    //throw;
                }
                finally
                { 
                }
                #endregion

                if ((MessageBox.Show("ֻ���ɴ����ߡ�" + strTempEemployeeName.Trim() + "�����иò�������Ҫ����������Ҫ�ϼ��쵼ȷ��\n                            Ҫ�����뵥����ȷ����", "iCare Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) == DialogResult.Cancel)
                {
                    return false;
                }
                else
                {
                    //��֤������/��ʿ��
                    frmValidateByDirector frm = new frmValidateByDirector(pType, pDeptID);
                    frm.ShowDialog();
                    if (!frm.BlnValidateResult)
                    {
                        return false;
                    }

                }
            }

            return IsAllow;
        }

        /// <summary>
        /// ʵ��DataTable��distinct����
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Columns"></param>
        /// <returns></returns>
        public DataTable Distinct(DataTable source, DataColumn[] Columns)
        {
            DataTable table = source.Clone();

            object[] currentrow = null;

            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            table.BeginLoadData();

            foreach (DataRow row in source.Rows)
            {

                int hash = string.Concat(row.ItemArray).GetHashCode();
                if (ht.Contains(hash))
                    continue;
                ht.Add(hash, hash);
                //Insert a copy of current row 
                currentrow = new object[Columns.Length];
                Array.Copy(row.ItemArray, currentrow, Columns.Length);
                table.LoadDataRow(currentrow, true);

            }

            table.EndLoadData();
            return table;

        }

        #region Ĭ��ǩ��
        /// <summary>
        /// Ĭ��ǩ��
        /// </summary>
        /// <param name="p_ctl"></param>
        public static void m_mthSetDefaulEmployee(Control p_ctl)
        {
            if (p_ctl is TextBoxBase)
            {
                p_ctl.Text = clsEMRLogin.LoginEmployee.m_strGetTechnicalRankAndName;//.m_strTECHNICALRANK_CHR.Trim() + " " + clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR.Trim();// +"(" + clsEMRLogin.LoginEmployee.m_strTECHNICALRANK_CHR.Trim();
                p_ctl.Tag = clsEMRLogin.LoginEmployee;
                p_ctl.Enabled = true;
            }
            else if (p_ctl is ListView)
            {
                ListView lsvSign = (ListView)p_ctl;
                lsvSign.Items.Clear();
                //Ĭ��ǩ��
                ListViewItem lviNewItem = new ListViewItem(clsEMRLogin.LoginEmployee.m_strGetTechnicalRankAndName);// + "(" + clsEMRLogin.LoginEmployee.m_strTECHNICALRANK_CHR.Trim() );
                //ID ����ظ���
                lviNewItem.SubItems.Add(clsEMRLogin.LoginEmployee.m_strEMPID_CHR);
                //���� ������
                lviNewItem.SubItems.Add(clsEMRLogin.LoginEmployee.m_strLEVEL_CHR);
                //tag��Ϊ����
                lviNewItem.Tag = clsEMRLogin.LoginEmployee;
                lsvSign.Items.Add(lviNewItem);
            }
        }
        #endregion

        public bool m_blnCheckForOpenForm(Form p_frmOpen)
        {
            clsMainMenuFunction objMemu = new clsMainMenuFunction();
            if (objMemu.m_blnIsSaveBeforeNewForm())
                return true;

            if (objMemu.m_blnCheckSamePatientForm(p_frmOpen))
                return true;

            if (objMemu.m_blnCheckForFormOpen(p_frmOpen, false))
                return true;
            return false;
        }
        /// <summary>
        /// ����Ƿ��д�ӡ��Ȩ��
        /// </summary>
        /// <param name="p_strNameSpace"></param>
        /// <param name="p_strClassName"></param>
        /// <param name="p_strRoles"></param>
        /// <returns>true �� �У�false �� ��</returns>
        public static bool m_blnCheckCanPrint(string p_strNameSpace, string p_strClassName, string[] p_strRoles)
        {
            //com.digitalwave.Emr.ConfigService.clsConfigService objServ =
            //    (com.digitalwave.Emr.ConfigService.clsConfigService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.Emr.ConfigService.clsConfigService));
            return (new weCare.Proxy.ProxyEmr06()).Service.m_blnCheckCanPrint(  p_strNameSpace, p_strClassName, p_strRoles);
        }
    }
}
