using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Controls;
using System.IO;
using System.Collections;
using com.digitalwave.Emr.StaticObject; 
using com.digitalwave.iCare.gui.LIS;
using System.Diagnostics;

namespace iCare
{
    /// <summary>
    /// ���˵�����
    /// </summary>
    public class clsMainMenuFunction
    {
        private const int M_INTMAXFROMCOUNT = 3;
        volatile bool bDeadOne = false;
        public clsMainMenuFunction()
        {
            //			Thread th = new Thread(new ThreadStart(WatchDog));
            //			th.IsBackground = true;
            //			th.Start();
        }

        /// <summary>
        /// �򿪲�����
        /// </summary>
        /// <param name="p_strFormName">����</param>
        public void m_mthOpenMedicalRecord(string p_strFormName)
        {
            try
            {

                Type type = Type.GetType(p_strFormName);
                Form frmMR = (Form)Activator.CreateInstance(type);

                if (frmMR is com.digitalwave.GUI_Base.frmMDI_Child_Base)
                {
                    if(m_blnCheckSameOrderForm((com.digitalwave.GUI_Base.frmMDI_Child_Base)frmMR))
                    {
                        frmMR = null;
                        return;
                    }
                }

                if (m_blnIsSaveBeforeNewForm())
                    return;

                if (m_blnCheckSamePatientForm(frmMR))
                    return;

                if (m_blnCheckForFormOpen(frmMR, false))
                    return;
                frmMR.MdiParent = clsEMRLogin.s_FrmMDI;

                frmMR.WindowState = FormWindowState.Maximized;

                frmMR.Show();

                //				frmMR.Closed +=new EventHandler(frmMR_Closed); ;//new System.ComponentModel.CancelEventHandler(frm_Closing);	

                if (frmMR is frmHRPBaseForm && MDIParent.s_ObjCurrentPatient != null)
                    ((frmHRPBaseForm)frmMR).m_mthSetPatient(MDIParent.s_ObjCurrentPatient);

            }
            catch (Exception e)
            {
                string str = e.Message;
                clsPublicFunction.ShowInformationMessageBox(e.Message);
            }
        }
        public bool m_blnCheckForFormOpen(Form p_frmChild, bool blnShowDialog)
        {
            Form frmLastForm = null;
            int intCount = m_intCheckOpenForm(p_frmChild, out frmLastForm);
            if (intCount >= M_INTMAXFROMCOUNT)
            {
                if (MessageBox.Show("��ǰ�Ѿ�����" + intCount + "���ġ�" + p_frmChild.Text + "�����壬�Ƿ�������µĴ��壿", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    frmLastForm.WindowState = FormWindowState.Normal;
                    frmLastForm.TopMost = true;
                    if (!blnShowDialog)
                        frmLastForm.WindowState = FormWindowState.Maximized;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ����Ƿ��Ѵ��˲������
        /// </summary>
        /// <returns></returns>
        public bool m_blnCheckHasOpenHRPExplorer()
        {
            if (clsEMRLogin.s_FrmMDI.MdiChildren != null && clsEMRLogin.s_FrmMDI.MdiChildren.Length > 0)
            {
                for (int i = 0; i < clsEMRLogin.s_FrmMDI.MdiChildren.Length; i++)
                {
                    if (clsEMRLogin.s_FrmMDI.MdiChildren[i].Name == "frmHRPExplorer")
                    {
                        clsEMRLogin.s_FrmMDI.MdiChildren[i].Activate();
                        clsEMRLogin.s_FrmMDI.MdiChildren[i].WindowState = FormWindowState.Maximized;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ���򿪵��Ӵ������Ŀ�Ƿ���ڻ����ָ��ֵ
        /// </summary>
        /// <param name="p_intCount">ָ��ֵ</param>
        /// <param name="p_frmChild">�Ӵ���</param>
        /// <returns></returns>
        public int m_intCheckOpenForm(Form p_frmChild, out Form p_frmLastForm)
        {
            int intCount = 0;
            p_frmLastForm = null;
            for (int i = 0; i < clsEMRLogin.s_FrmMDI.MdiChildren.Length; i++)
            {
                if (clsEMRLogin.s_FrmMDI.MdiChildren[i].GetType().Name == p_frmChild.GetType().Name)
                {
                    p_frmLastForm = clsEMRLogin.s_FrmMDI.MdiChildren[i];
                    intCount++;
                }
            }
            return intCount;
        }

        public void m_mthLoadOutlookBar_EMR()
        {
            Type type = clsEMRLogin.s_FrmMDI.GetType();
            System.Reflection.PropertyInfo pi = type.GetProperty("m_OutlookBar");
            UtilityLibrary.WinControls.OutlookBar bar = (UtilityLibrary.WinControls.OutlookBar)pi.GetValue(clsEMRLogin.s_FrmMDI, null);
            if (bar.Tag == null || bar.Tag.ToString() != "emr")//��ʼ�����Ӳ���outlookbar
            {
                bar.Bands.Clear();
                if (frmOutlookBar.s_OutlookBar == null)//����ö���ûload���ڴ�
                {
                    frmOutlookBar frm = new frmOutlookBar();
                }
                for (int i = 0; i < frmOutlookBar.s_OutlookBar.Bands.Count; i++)
                {
                    if (frmOutlookBar.s_OutlookBar.Bands[i].ChildControl != null)
                        frmOutlookBar.s_OutlookBar.Bands[i].ChildControl.Parent = bar.FindForm();
                    bar.Bands.Add(frmOutlookBar.s_OutlookBar.Bands[i]);
                }
                for (int i = bar.Bands.Count - 1; i >= 0; i--)
                    bar.CurrentBand = i;
            }
            bar.Visible = true;
            bar.Tag = "emr";//��ǰoutlookbar�ѳ�ʼ��Ϊ���Ӳ���outlookbar
        }
        /// <summary>
        /// ���Ź�����
        /// </summary>
        void WatchDog()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(50);
                bDeadOne = bDeadOne || false;

                if (bDeadOne)
                {
                    bDeadOne = false;
                    GC.Collect();
                }
            }
        }

        private void frm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            iCare.iCareBaseForm.frmBaseForm frm = sender as iCare.iCareBaseForm.frmBaseForm;

            if (!(frm.IsDisposed || frm.Disposing))
            {
                //				frm.Release();
            }

            //����ֻ�ܵ��÷��йܶ���
            bDeadOne = true;
        }

        private void frmMR_Closed(object sender, EventArgs e)
        {
            iCare.iCareBaseForm.frmBaseForm frm = sender as iCare.iCareBaseForm.frmBaseForm;

            if (!(frm.IsDisposed || frm.Disposing))
            {
                //				frm.Release();
            }

            //����ֻ�ܵ��÷��йܶ���
            bDeadOne = true;

        }
        public bool m_blnCheckSamePatientForm(Form p_frmCheck)
        {
            frmHRPBaseForm p_frmHRP = p_frmCheck as frmHRPBaseForm;
            if (p_frmHRP == null)
                return false;

            string strInID = "";
            frmHRPBaseForm frmCurrent = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmHRPBaseForm;
            if (frmCurrent != null && frmCurrent.m_objBaseCurrentPatient != null)
            {
                strInID = frmCurrent.m_objBaseCurrentPatient.m_StrInPatientID;
            }
            else if (MDIParent.m_objCurrentPatient != null)
            {
                strInID = MDIParent.m_objCurrentPatient.m_strEMRInPatientID.Trim();
            }

            return m_blnCheckSameMDIChild(p_frmHRP, strInID);
        }

        public bool m_blnCheckSamePatientForm(Form p_frmCheck,string p_strInPatientID)
        {
            frmHRPBaseForm p_frmHRP = p_frmCheck as frmHRPBaseForm;
            if (p_frmHRP == null)
                return false;

            string strInID = p_strInPatientID;

            return m_blnCheckSameMDIChild(p_frmHRP, strInID);
        }

        private bool m_blnCheckSameMDIChild(frmHRPBaseForm p_frmHRP, string strInID)
        {
            for (int i = 0; i < clsEMRLogin.s_FrmMDI.MdiChildren.Length; i++)
            {
                if (clsEMRLogin.s_FrmMDI.MdiChildren[i] is frmHRPBaseForm
                    && !clsEMRLogin.s_FrmMDI.MdiChildren[i].Equals(p_frmHRP)
                    && clsEMRLogin.s_FrmMDI.MdiChildren[i].Name == p_frmHRP.Name)
                {
                    if (((frmHRPBaseForm)clsEMRLogin.s_FrmMDI.MdiChildren[i]).m_objBaseCurrentPatient != null
                        && ((frmHRPBaseForm)clsEMRLogin.s_FrmMDI.MdiChildren[i]).m_objBaseCurrentPatient.m_StrInPatientID != null
                        && ((frmHRPBaseForm)clsEMRLogin.s_FrmMDI.MdiChildren[i]).m_objBaseCurrentPatient.m_StrInPatientID.Trim() == strInID.Trim())
                    {
                        clsEMRLogin.s_FrmMDI.MdiChildren[i].Activate();
                        return true;
                    }
                    else if (((frmHRPBaseForm)clsEMRLogin.s_FrmMDI.MdiChildren[i]).m_objBaseCurrentPatient == null && strInID == "")
                    {
                        clsEMRLogin.s_FrmMDI.MdiChildren[i].Activate();
                        return true;
                    }
                }
            }
            return false;
        }

        #region ���´���ǰѯ���Ƿ񱣴�ԭ�д���
        /// <summary>
        /// ���´���ǰѯ���Ƿ񱣴�ԭ�д���
        /// </summary>
        /// <returns></returns>
        public bool m_blnIsSaveBeforeNewForm()
        {
            bool blnIsCancle = false;

            frmHRPBaseForm hrpForm = clsEMRLogin.s_FrmMDI.ActiveMdiChild as frmHRPBaseForm;
            if (hrpForm == null)
                return false;

            hrpForm.m_blnNotClickWindows = true;

            if (!hrpForm.m_blnHasClosing && !hrpForm.TopLevel)
            {
                DialogResult dlgResult = DialogResult.None;
                if (!MDIParent.s_ObjSaveCue.m_blnCheckStatusSame(hrpForm))
                {
                    if (MDIParent.s_ObjCurrentPatient != null && MDIParent.s_ObjCurrentPatient.m_IntCharacter == 1)
                    {
                        dlgResult = DialogResult.No;
                    }
                    else
                    {
                        dlgResult = clsPublicFunction.ShowQuestionMessageBox("[" + hrpForm.Text + "]���˸Ķ����Ƿ񱣴棿", MessageBoxButtons.YesNoCancel);

                        //dlgResult = DialogResult.Yes;
                    }

                    if (dlgResult == DialogResult.Yes)
                    {
                        long lngRes = hrpForm.m_lngSave();
                        //������ɺ󽫸ô���ر�
                        if(lngRes > 0)
                            hrpForm.Close();
                    }
                    else if (dlgResult == DialogResult.Cancel)
                    {
                        blnIsCancle = true;
                        hrpForm.m_blnNotClickWindows = false;
                        return blnIsCancle;
                    }
                }
            }
            return blnIsCancle;
        }
        #endregion

        #region ����Ƿ��Ѵ�ҽ�����
        /// <summary>
        /// ����Ƿ��Ѵ�ҽ�����
        /// </summary>
        /// <param name="p_frmOrders"></param>
        /// <returns></returns>
        private bool m_blnCheckSameOrderForm(com.digitalwave.GUI_Base.frmMDI_Child_Base p_frmOrders)
        {
            if (p_frmOrders == null)
                return true;

            if (clsEMRLogin.s_FrmMDI != null && clsEMRLogin.s_FrmMDI.MdiChildren != null)
            {
                for (int i = 0; i < clsEMRLogin.s_FrmMDI.MdiChildren.Length; i++)
                {
                    if (clsEMRLogin.s_FrmMDI.MdiChildren[i].Name == p_frmOrders.Name
                        && clsEMRLogin.s_FrmMDI.MdiChildren[i] is com.digitalwave.GUI_Base.frmMDI_Child_Base)
                    {
                        clsEMRLogin.s_FrmMDI.MdiChildren[i].Activate();
                        return true;
                    }
                }
            }
            return false;
        } 
        #endregion

        #region New OutLookBar
        public com.digitalwave.Controls.ctlOutLookBar m_ctlGetOutLookBar
        {
            get
            {
                clsEmrModuleNode_VO objModuleNode;
                 
                long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetModuleInfo(  clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, out objModuleNode);
             
                if (lngRes > 0)
                {
                    com.digitalwave.Controls.ctlOutLookBar ctlOutlookBar = new com.digitalwave.Controls.ctlOutLookBar(objModuleNode);
                    ctlOutlookBar.evnItemClick += new ctlOutLookBar.ItemClickEventHandler(ctlOutlookBar_evnItemClick);
                    return ctlOutlookBar;
                }
                return null;
            }
        }

        void ctlOutlookBar_evnItemClick(object sender, com.digitalwave.Controls.ItemClickEventArgs e)
        {
            if (e.Data != null)
            {
                m_mthSetEmrMenuItemClick(e.Data);
            }
        }
        private string m_strGetValidPath(string strPath)
        {
            string strNewPath = strPath;
            if (File.Exists(strNewPath)) return strNewPath;

            strNewPath = ".\\" + strPath;
            if (File.Exists(strNewPath)) return strNewPath;

            strNewPath = Application.StartupPath + "\\" + strPath;
            if (File.Exists(strNewPath)) return strNewPath;

            return null;
        }
        public void m_mthSetEmrModuleMemu(MenuItem p_mniParent)
        {
            if (p_mniParent == null)
                return;
            clsEmrModuleNode_VO objModuleNode;
             
            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetModuleInfo ( clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, out objModuleNode);
         
            clsEMR_StaticObject.s_ObjEmrModuleNode = objModuleNode;
            if (lngRes > 0 && objModuleNode != null)
            {
                m_mthInitModuleNodes(objModuleNode.m_ArlChilds, ref p_mniParent);
            }
            objModuleNode = null;
        }
        private void m_mthInitModuleNodes(System.Collections.Generic.List<clsEmrModuleNode_VO> p_arlModule, ref MenuItem p_mniParent)
        {
            if (p_arlModule.Count > 0)
            {
                for (int i = 0 ; i < p_arlModule.Count ; i++)
                {
                    clsEmrModuleNode_VO objNode = p_arlModule[i] as clsEmrModuleNode_VO;
                    MenuItem item = new MenuItem(objNode.m_ObjCurrentModuleNode.m_StrModuleName);
                    if (objNode.m_ArlChilds.Count > 0)
                        m_mthInitModuleNodes(objNode.m_ArlChilds, ref item);
                    if (item.Text.Trim() != "-" && objNode.m_ArlChilds.Count == 0)
                    {
                        item.Tag = objNode.m_ObjCurrentModuleNode;
                        item.Click += new EventHandler(item_Click);
                    }
                    p_mniParent.MenuItems.Add(item);
                    objNode = null;
                }
            }

        }

        void item_Click(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            clsEmrModule_VO objModule = item.Tag as clsEmrModule_VO;
            if (objModule != null)
            {
                m_mthSetEmrMenuItemClick(objModule);
            }
        }
        #endregion New OutLookBar

        #region ���ü������뵥
        /// <summary>
        /// Show�������뵥
        /// </summary>
        public void m_mthLISApply()
        {
            iCare.DoctorWorkStation.frmChoosePatient frmCP = new iCare.DoctorWorkStation.frmChoosePatient();

            if (MDIParent.s_ObjCurrentPatient == null)
            {
                frmCP.SetPatientInfo = null;
            }
            else
            {
                //frmCP.SetPatientInfo=MDIParent.s_ObjCurrentPatient;
            }

            clsEmrPatient_VO objPatient;
            //if(frmCP.ShowDialog()==DialogResult.No)
            if (frmCP.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            objPatient = frmCP.SetPatientInfo;
            frmCP.Close();
            frmLisAppl obj = new frmLisAppl();
            #region �շѲ��˻�������
            clsLisApplMainVO objLMVO = new clsLisApplMainVO();
            objLMVO.m_intEmergency = 0;
            objLMVO.m_intForm_int = 0;
            objLMVO.m_strAge = objPatient.m_strMARRIED_CHR;
            //objLMVO.m_strAge=objPatient.m_ObjPeopleInfo.m_StrAge;
            objLMVO.m_strAppl_DeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objLMVO.m_strAppl_EmpID = MDIParent.strOperatorID;
            objLMVO.m_strDiagnose = "";
            objLMVO.m_strOperator_ID = MDIParent.strOperatorID;
            objLMVO.m_strPatient_Name = objPatient.m_strLASTNAME_VCHR;
            //objLMVO.m_strPatient_Name=objPatient.m_StrName;
            objLMVO.m_strPatientcardID = "";
            com.digitalwave.iCare.gui.HIS.clsDcl_ShowReports objSR = new com.digitalwave.iCare.gui.HIS.clsDcl_ShowReports();
            objLMVO.m_strPatientID = objPatient.m_strPATIENTID_CHR;
            //objLMVO.m_strPatientID=objSR.m_mthFindPatientIDByInHospitalNo(objPatient.m_StrInPatientID);
            objLMVO.m_strPatientType = "1";
            objLMVO.m_strSex = objPatient.m_strSEX_CHR.Trim();
            objLMVO.m_strPatient_inhospitalno_chr = objPatient.m_strINPATIENTID_CHR;
            objLMVO.m_strAge = objPatient.m_strMARRIED_CHR.Trim();
            //objLMVO.m_strSex=objPatient.m_StrSex;
            //objLMVO.m_strPatient_inhospitalno_chr = objPatient.m_StrInPatientID;
            //objLMVO.m_strAge = objPatient.m_ObjPeopleInfo.m_IntAge.ToString();
            obj.m_mthNewApp(objLMVO);
            #endregion

        }
        #endregion

        #region ���ñ��浥
        /// <summary>
        /// Show���浥
        /// </summary>
        public void m_mthShowReport()
        {

            iCare.DoctorWorkStation.frmChoosePatient frmCP = new iCare.DoctorWorkStation.frmChoosePatient();
            if (MDIParent.s_ObjCurrentPatient == null)
            {
                frmCP.SetPatientInfo = null;
            }
            else
            {
                //frmCP.SetPatientInfo=MDIParent.s_ObjCurrentPatient;
            }
            clsEmrPatient_VO objPatient;
            //clsPatient objPatient;
            //if(frmCP.ShowDialog()==DialogResult.No)
            if (frmCP.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            objPatient = (clsEmrPatient_VO)frmCP.SetPatientInfo;
            com.digitalwave.iCare.gui.HIS.frmShowReports objfrmSR = new com.digitalwave.iCare.gui.HIS.frmShowReports();
            objfrmSR.InHospitalNO = objPatient.m_strINPATIENTID_CHR.Trim();
            objfrmSR.PatientName = objPatient.m_strLASTNAME_VCHR.Trim();
            objfrmSR.PatientSex = objPatient.m_strSEX_CHR.Trim();
            objfrmSR.PatientAge = objPatient.m_strMARRIED_CHR.Trim();
            //objfrmSR.InHospitalNO=objPatient.m_StrInPatientID;
            //objfrmSR.PatientName=objPatient.m_StrName;
            //objfrmSR.PatientSex=objPatient.m_StrSex;
            //objfrmSR.PatientAge=objPatient.m_ObjPeopleInfo.m_StrAge;
            objfrmSR.ShowDialog();

        }
        #endregion

        #region ͼ�Ĺ���վ
        /// <summary>
        /// showͼ�Ĺ���վ
        /// </summary>
        public void m_mthShowARWorkStation()
        {
            m_mthLoadAssemblyForm("GLS_WorkStation.dll", "com.digitalwave.GLS_WS.frm_AR_WorkStation");
        }
        #endregion ͼ�Ĺ���վ

        #region ��dll�� Load����
        /// <summary>
        /// ��dll�� Load����
        /// </summary>
        /// <param name="p_strDllName">dll�ļ�����������չ��</param>
        /// <param name="p_strFormFullName">����ȫ�ƣ����������ռ�</param>
        public void m_mthLoadAssemblyForm(string p_strDllName, string p_strFormFullName)
        {
            try
            {
                if (m_blnIsSaveBeforeNewForm())
                    return;

                Assembly asm = Assembly.LoadFrom(Application.StartupPath + "\\" + p_strDllName);//��ȡ�ⲿ�ļ�ʱ��ı䵱ǰӦ��·�������Լ���Application.StartupPath
                object obj = asm.CreateInstance(p_strFormFullName);
                Form frmMR = obj as Form;

                if (m_blnCheckSamePatientForm(frmMR))
                    return;
                if (m_blnCheckForFormOpen(frmMR, false))
                    return;
                frmMR.StartPosition = FormStartPosition.CenterScreen;
                if (obj is iLoginInfo)
                {
                    ((iLoginInfo)obj).LoginInfo = clsEMRLogin.LoginInfo;
                }
                frmMR.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "���ļ��������ռ�����");
            }
        }
        #endregion

        /// <summary>
        /// ���ò˵������¼�
        /// </summary>
        /// <param name="p_objModule"></param>
        public void m_mthSetEmrMenuItemClick(clsEmrModule_VO p_objModule)
        {
            if (p_objModule != null && p_objModule.m_ObjFormInfo != null)
            {
                try
                {
                    string strPath = m_strGetValidPath(p_objModule.m_ObjFormInfo.m_StrDLLName);
                    if (string.IsNullOrEmpty(strPath))
                        throw new Exception("Invalid Dll Name");


                    Assembly objAsm = Assembly.LoadFrom(strPath);
                    if (objAsm == null)
                        throw new Exception("Load Assembly Error");

                    object obj = objAsm.CreateInstance(p_objModule.m_ObjFormInfo.m_StrOpraClassName, true);
                    if (obj == null)
                        throw new Exception("Create Instance Error");

                    Type objType = obj.GetType();

                    MethodInfo objMi;
                    string strMethod = p_objModule.m_ObjFormInfo.m_StrOpraMethodName;
                    object[] objParams = null;
                    int intIndex = strMethod.IndexOf("(");
                    if (intIndex != -1)//��ȡ����
                    {
                        string strParam = strMethod.Substring(intIndex + 1, strMethod.Length - intIndex - 2);
                        //objParams = new string[]{strParam};
                        objParams = strParam.Split(',');
                        strMethod = strMethod.Substring(0, intIndex);
                        objMi = objType.GetMethod(strMethod);
                    }
                    else
                    {
                        objMi = objType.GetMethod(p_objModule.m_ObjFormInfo.m_StrOpraMethodName, new Type[0]);	//no param
                    }

                    if (objMi == null) throw new Exception("Get Method Error");

                    //
                    if ((clsEMR_StaticObject.s_FrmMDI != null) && (obj is Form))
                    {
                        Form objForm = obj as Form;
                        if (objForm is com.digitalwave.GUI_Base.frmMDI_Child_Base)
                        {
                            if (m_blnCheckSameOrderForm((com.digitalwave.GUI_Base.frmMDI_Child_Base)objForm))
                            {
                                objForm = null;
                                return;
                            }
                        }

                        if (m_blnIsSaveBeforeNewForm())
                            return;

                        if (m_blnCheckSamePatientForm(objForm))
                            return;

                        if (m_blnCheckForFormOpen(objForm, false))
                            return;
                        if (p_objModule.m_ObjFormInfo.m_IntIsSubForm == 1)
                        {
                            objForm.MdiParent = clsEMR_StaticObject.s_FrmMDI;
                            objForm.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            if (objForm.TopLevel)
                                objForm.Owner = clsEMR_StaticObject.s_FrmMDI;
                        }
                    }
                    if (obj != null && clsEMRLogin.LoginInfo != null)
                        if (obj is iLoginInfo)
                        {
                            ((iLoginInfo)obj).LoginInfo = clsEMRLogin.LoginInfo;
                        }
                    if (obj != null)
                    {
                        objMi.Invoke(obj, objParams);
                    }
                    if (obj is frmHRPBaseForm && MDIParent.s_ObjCurrentPatient != null)
                        ((frmHRPBaseForm)obj).m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
                }
                catch (Exception ee)
                {
                    com.digitalwave.Utility.clsLogText objLog = new com.digitalwave.Utility.clsLogText();
                    objLog.LogDetailError(ee, false);
                }
            }
        }

        #region ���ⲿ����
        /// <summary>
        /// ���ⲿ����,����Ĭ�ϵĲ���
        /// </summary>
        /// <param name="p_strExePath">����·��</param>
        /// <param name="p_strNeedParam">
        /// 1����Ҫ����������������˳���û�ID���û����ơ��������ơ�����ID��
        /// 0������Ҫ
        /// </param>
        public void m_mthOpenExe(string p_strExePath,string p_strNeedParam)
        {
            string strPath = m_strGetValidPath(p_strExePath);
            if (string.IsNullOrEmpty(strPath))
                return;
            string strArg = string.Empty;
            if (!string.IsNullOrEmpty(p_strNeedParam))
            {
                if (p_strNeedParam.Trim() == "1")
                {
                    strArg += clsEMRLogin.LoginInfo.m_strEmpID + " ";
                    strArg += clsEMRLogin.LoginInfo.m_strEmpName + " ";
                    if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr != null)
                    {
                        if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[0] != null)
                        {
                            strArg += com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[0].m_strDEPTNAME_VCHR + " ";
                            strArg += com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[0].m_strDEPTID_CHR;
                        }
                    }
                    else if (com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment != null)
                    {
                        strArg += com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTNAME_VCHR + " ";
                        strArg += com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR;
                    }
                }
            }
            try
            {
                System.Diagnostics.Process.Start(strPath, strArg);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,false);
            }

        }
        /// <summary>
        /// ���ⲿ����
        /// </summary>
        /// <param name="p_strExePath">����·��</param>
        /// <param name="p_strParams">�������ÿո�ֿ�</param>
        /// <param name="p_strFromFile">�Ƿ���ļ���ȡ������0����1���ǣ�,��δʵ��</param>
        public void m_mthOpenExeByParams(string p_strExePath,string p_strParams,string p_strFromFile)
        {
            string strPath = m_strGetValidPath(p_strExePath);
            if (string.IsNullOrEmpty(strPath))
                return;
            string strArg = string.Empty;
            if (!string.IsNullOrEmpty(p_strParams))
            {
                strArg = p_strParams;
            }
            try
            {
                System.Diagnostics.Process.Start(strPath, strArg);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, false);
            }

        }
        #endregion 
    }
}
