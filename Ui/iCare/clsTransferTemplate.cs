using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Text;
using System.Drawing;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using Crownwood.Magic.Menus;
//using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.GLS_WS;
using System.Reflection;
using com.digitalwave.Emr.Utility.DataShare;
using System.Xml;
using System.Xml.Schema;

namespace iCare
{
    public class clsTransferTemplate
    {
        #region Variable
        private ContextMenu m_ctmControl = new ContextMenu();
        private MenuItem m_mniDoubleStrike = new MenuItem("˫����ɾ��");
        private MenuItem m_mniTemplate = new MenuItem("ģ��");
        private MenuItem m_mniCommonUse = new MenuItem("����ֵģ��");
        private MenuItem m_mniDataShare = new MenuItem("������������");
        private MenuItem m_mniDataShareInvoking = new MenuItem("ֱ������Ƕ��");
        private MenuItem m_mniApplyReport = new MenuItem("ͼ�Ĺ���վ");
        private MenuItem m_mniLabCheckResult = new MenuItem("������");
        private MenuItem m_mniCheckResult = new MenuItem("�����");
        private MenuItem m_mniSuperSubScript = new MenuItem("���±�");
        private MenuItem m_mniTextTemplate = new MenuItem("��СԪ�ؼ�");

        private Hashtable m_hasMenuItems = new Hashtable();


        /// <summary>
        /// ��¼�Ѿ�չ�������ڵ㣬�����´ε��ʱ�ٴβ�����Ŀ¼
        /// </summary>
        private ArrayList m_arlExpandedNodes;

        //private clsInpatMedRec_Type_Dept[] m_objType_DeptArr;
        //		private clsInpatMedRec_Type[] m_objType_DeptArr;

        private ContextMenuStrip m_ppmMenu = new ContextMenuStrip();
        private ToolStripMenuItem m_mncDoubleStrike = new ToolStripMenuItem("˫����ɾ��");
        private ToolStripMenuItem m_mncTemplate = new ToolStripMenuItem("ģ��");
        private ToolStripMenuItem m_mncCommonUse = new ToolStripMenuItem("����ֵģ��");
        private ToolStripMenuItem m_mncDataShare = new ToolStripMenuItem("������������");
        private ToolStripMenuItem m_mncDataShareInvoking = new ToolStripMenuItem("ֱ������Ƕ��");
        private ToolStripMenuItem m_mncApplyReport = new ToolStripMenuItem("ͼ�Ĺ���վ");
        private ToolStripMenuItem m_mncLabCheckResult = new ToolStripMenuItem("������");
        private ToolStripMenuItem m_mncCheckResult = new ToolStripMenuItem("�����");
        private ToolStripMenuItem m_mncSuperSubScript = new ToolStripMenuItem("���±�");
        private ToolStripMenuItem m_mncTextTemplate = new ToolStripMenuItem("��СԪ�ؼ�");
        //private PopupMenu m_ppmMenu = new PopupMenu();
        //private MenuCommand m_mncDoubleStrike = new MenuCommand("˫����ɾ��");
        //private MenuCommand m_mncTemplate = new MenuCommand("ģ��");
        //private MenuCommand m_mncCommonUse = new MenuCommand("����ֵģ��");
        //private MenuCommand m_mncDataShare = new MenuCommand("������������");
        //private MenuCommand m_mncDataShareInvoking = new MenuCommand("ֱ������Ƕ��");
        //private MenuCommand m_mncApplyReport = new MenuCommand("ͼ�Ĺ���վ");
        //private MenuCommand m_mncLabCheckResult = new MenuCommand("������");
        //private MenuCommand m_mncCheckResult = new MenuCommand("�����");
        //private MenuCommand m_mncSuperSubScript = new MenuCommand("���±�");
        //private MenuCommand m_mncTextTemplate = new MenuCommand("��СԪ�ؼ�");

        //frmDataShareTool m_objfrmDataShare
        #endregion

        public clsTransferTemplate()
        {
            m_mthInit();
            m_arlExpandedNodes = new ArrayList();
        }

        /// <summary>
        /// ���ݸ��ò˵���
        /// </summary>
        public MenuItem m_MniDataShare
        {
            get
            {
                return m_mniDataShare;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void m_mthInit()
        {
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
            {
                m_mniCheckResult.Text = "Ӱ�񱨸浥";
                m_mncCheckResult.Text = "Ӱ�񱨸浥";
                m_mncApplyReport.Visible = false;
                m_mniApplyReport.Visible = false;
            }
            #region old menu
            s_mthInitDataShareItems(m_mniDataShare);
            s_mthInitDataShareItems(m_mniDataShareInvoking);

            //SQL�汾��ʱ��Ҫ
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
                m_mthSetApplyReportSubMenu(m_mniApplyReport);

            m_mthSetShareItem(m_mniDataShare);
            m_mthSetShareItem(m_mniDataShareInvoking);

            m_mthAssociateDataShareItemsEvent(m_mniDataShare);
            m_mthAssociateDataShareInvokingItemsEvent(m_mniDataShareInvoking);

            m_mniDoubleStrike.Click += new EventHandler(m_mniDoubleStrike_Click);
            m_mniTemplate.Click += new EventHandler(m_ctmTemplate_Click);
            m_mniLabCheckResult.Click += new EventHandler(m_ctmLabCheckResult_Click);
            m_mniCheckResult.Click += new EventHandler(m_ctmCheckResult_Click);
            m_mniTextTemplate.Click += new EventHandler(m_mniTextTemplate_Click);

            EventHandler evhSuperSubScript = new EventHandler(SuperSubScript_Click);
            m_mniSuperSubScript.MenuItems.Add("�ϱ�", evhSuperSubScript);
            m_mniSuperSubScript.MenuItems.Add("�±�", evhSuperSubScript);
            m_mniSuperSubScript.MenuItems.Add("����", evhSuperSubScript);

            m_ctmControl.MenuItems.Add(m_mniTemplate);
            m_ctmControl.MenuItems.Add(m_mniCommonUse);
            m_ctmControl.MenuItems.Add("-");
            m_ctmControl.MenuItems.Add(new MenuItem("�������", new EventHandler(m_mthInvokeSpecialSymbol)));
            m_ctmControl.MenuItems.Add("-");
            m_ctmControl.MenuItems.Add(m_mniDataShare);
            m_ctmControl.MenuItems.Add(m_mniDataShareInvoking);
            m_ctmControl.MenuItems.Add(m_mniApplyReport);
            m_ctmControl.MenuItems.Add(m_mniLabCheckResult);
            m_ctmControl.MenuItems.Add(m_mniCheckResult);
            m_ctmControl.MenuItems.Add(m_mniTextTemplate);
            m_ctmControl.MenuItems.Add("-");
            //m_ctmControl.MenuItems.Add(new MenuItem("����(&T)", new EventHandler(m_mthCut)));
            m_ctmControl.MenuItems.Add(new MenuItem("����(&C)", new EventHandler(m_mthCopy)));
            m_ctmControl.MenuItems.Add(new MenuItem("ճ��(&P)", new EventHandler(m_mthPaste)));

            m_ctmControl.Popup += new EventHandler(m_ctmPopup);
            #endregion

            #region new menu
            s_mthInitDataShareItems(m_mncDataShare);
            s_mthInitDataShareItems(m_mncDataShareInvoking);

            //m_mthSetShareItem2(m_mncDataShare);
            //m_mthSetShareItem2(m_mncDataShareInvoking);

            //SQL�汾��ʱ��Ҫ
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
                m_mthSetApplyReportSubMenu(m_mncApplyReport);

            m_mthAssociateDataShareItemsEvent(m_mncDataShare);
            m_mthAssociateDataShareInvokingItemsEvent(m_mncDataShareInvoking);

            m_mncDoubleStrike.Click += new EventHandler(m_mniDoubleStrike_Click);
            m_mncTemplate.Click += new EventHandler(m_ctmTemplate_Click);
            m_mncLabCheckResult.Click += new EventHandler(m_ctmLabCheckResult_Click);
            m_mncCheckResult.Click += new EventHandler(m_ctmCheckResult_Click);
            m_mncTextTemplate.Click += new EventHandler(m_mniTextTemplate_Click);

            EventHandler evhSuperSubScript2 = new EventHandler(SuperSubScript_Click);
            //MenuCommand mncUp = new MenuCommand("�ϱ�", evhSuperSubScript2);
            //MenuCommand mncDown = new MenuCommand("�±�", evhSuperSubScript2);
            //MenuCommand mncNormal = new MenuCommand("����", evhSuperSubScript2);
            ToolStripMenuItem mncUp = new ToolStripMenuItem("�ϱ�", null, evhSuperSubScript2);
            ToolStripMenuItem mncDown = new ToolStripMenuItem("�±�", null, evhSuperSubScript2);
            ToolStripMenuItem mncNormal = new ToolStripMenuItem("����", null, evhSuperSubScript2);
            m_mncSuperSubScript.DropDownItems.Add(mncUp);
            m_mncSuperSubScript.DropDownItems.Add(mncDown);
            m_mncSuperSubScript.DropDownItems.Add(mncNormal);

            #region set image
            frmImageList frmImg = new frmImageList();
            m_ppmMenu.ImageList = frmImg.m_ImgMenu;
            //m_mncDoubleStrike.ImageList = frmImg.m_ImgMenu;
            m_mncDoubleStrike.ImageIndex = 0;
            //m_mncSuperSubScript.ImageList = frmImg.m_ImgMenu;
            m_mncSuperSubScript.ImageIndex = 1;
            //m_mncTemplate.ImageList = frmImg.m_ImgMenu;
            m_mncTemplate.ImageIndex = 2;
            //m_mncCommonUse.ImageList = frmImg.m_ImgMenu;
            m_mncCommonUse.ImageIndex = 3;
            //m_mncDataShare.ImageList = frmImg.m_ImgMenu;
            m_mncDataShare.ImageIndex = 4;
            //m_mncDataShareInvoking.ImageList = frmImg.m_ImgMenu;
            m_mncDataShareInvoking.ImageIndex = 5;
            //m_mncLabCheckResult.ImageList = frmImg.m_ImgMenu;
            m_mncLabCheckResult.ImageIndex = 6;

            #endregion
            m_ppmMenu.Items.Add(m_mncDoubleStrike);
            m_ppmMenu.Items.Add(m_mncDoubleStrike);
            m_ppmMenu.Items.Add(m_mncSuperSubScript);
            m_ppmMenu.Items.Add(m_mncTemplate);
            m_ppmMenu.Items.Add(m_mncCommonUse);
            m_ppmMenu.Items.Add(new ToolStripSeparator());
            m_ppmMenu.Items.Add(new ToolStripMenuItem("�������", frmImg.m_ImgMenu.Images[10], new EventHandler(m_mthInvokeSpecialSymbol), "m_mncSpecialSymbol"));
            m_ppmMenu.Items.Add(new ToolStripSeparator());
            m_ppmMenu.Items.Add(m_mncDataShare);
            m_ppmMenu.Items.Add(m_mncDataShareInvoking);
            m_ppmMenu.Items.Add(m_mncApplyReport);
            m_ppmMenu.Items.Add(m_mncLabCheckResult);
            m_ppmMenu.Items.Add(m_mncCheckResult);
            m_ppmMenu.Items.Add(m_mncTextTemplate);
            m_ppmMenu.Items.Add(new ToolStripSeparator());
            //m_ppmMenu.Items.Add(new MenuCommand("����(&T)", frmImg.m_ImgMenu, 7, new EventHandler(m_mthCut)));
            m_ppmMenu.Items.Add(new ToolStripMenuItem("����(&C)", frmImg.m_ImgMenu.Images[8], new EventHandler(m_mthCopy), "m_mncCopy"));
            m_ppmMenu.Items.Add(new ToolStripMenuItem("ճ��(&P)", frmImg.m_ImgMenu.Images[9], new EventHandler(m_mthPaste), "m_mncPaste"));

            //m_ppmMenu.MenuCommands.Add(m_mncDoubleStrike);
            //m_ppmMenu.MenuCommands.Add(m_mncSuperSubScript);
            //m_ppmMenu.MenuCommands.Add(m_mncTemplate);
            //m_ppmMenu.MenuCommands.Add(m_mncCommonUse);
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("-"));
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("�������", frmImg.m_ImgMenu, 10, new EventHandler(m_mthInvokeSpecialSymbol)));
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("-"));
            //m_ppmMenu.MenuCommands.Add(m_mncDataShare);
            //m_ppmMenu.MenuCommands.Add(m_mncDataShareInvoking);
            //m_ppmMenu.MenuCommands.Add(m_mncApplyReport);
            //m_ppmMenu.MenuCommands.Add(m_mncLabCheckResult);
            //m_ppmMenu.MenuCommands.Add(m_mncCheckResult);
            //m_ppmMenu.MenuCommands.Add(m_mncTextTemplate);
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("-"));
            ////m_ppmMenu.MenuCommands.Add(new MenuCommand("����(&T)", frmImg.m_ImgMenu, 7, new EventHandler(m_mthCut)));
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("����(&C)", frmImg.m_ImgMenu, 8, new EventHandler(m_mthCopy)));
            //m_ppmMenu.MenuCommands.Add(new MenuCommand("ճ��(&P)", frmImg.m_ImgMenu, 9, new EventHandler(m_mthPaste)));

            //m_ppmMenu.MenuCommands.ExtraText = "iCare";
            //m_ppmMenu.MenuCommands.ExtraTextColor = Color.White;
            //m_ppmMenu.MenuCommands.ExtraBackColor = Color.SlateGray;
            //m_ppmMenu.MenuCommands.ShowInfrequent = true;
            //m_ppmMenu.MenuCommands.ExtraFont = new Font("Times New Roman", 12f, FontStyle.Bold | FontStyle.Italic);

            //			m_ppmMenu.
            #endregion
            this.m_ppmMenu.Opening += new System.ComponentModel.CancelEventHandler(ContextMenuStrip_Opening);
        }

        void m_mniTextTemplate_Click(object sender, EventArgs e)
        {
            iCare.CustomForm.clsExteriorFunctionInterface.m_ObjUserInfo = clsEMRLogin.LoginInfo;
            iCare.CustomForm.clsExteriorFunctionInterface.s_ObjCurrentPatient = MDIParent.s_ObjCurrentPatient;
            DateTime dtmCreatedDate = DateTime.Now;
            if (m_frmForm is frmHRPBaseForm)
                dtmCreatedDate = ((frmHRPBaseForm)m_frmForm).m_DtmCreatedDate;
            //int intSelectedStart = (m_txtRichTextBox.SelectionStart < 0 ? m_txtRichTextBox.Text.Length : m_txtRichTextBox.SelectionStart);
            int intSelectedStart = m_intGetControlSelectionStart(m_ctlText);
            using (iCare.CustomForm.frmTextTemplate frm = new iCare.CustomForm.frmTextTemplate(m_ctlText, m_ctlText.FindForm().Name, m_ctlText.Name, intSelectedStart, dtmCreatedDate))
            {
                if (frm.ShowDialog() == DialogResult.OK && m_frmForm is frmHRPBaseForm)
                {
                    frmHRPBaseForm frmBaseForm = (frmHRPBaseForm)m_frmForm;
                    frmBaseForm.m_arlMinElementColValue.AddRange((clsMinElementValues[])frm.m_ArlTextTemplate.ToArray(typeof(clsMinElementValues)));
                }
            }
        }

        /// <summary>
        /// ���볣��ֵģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthLoadCommonUseTemplate(MenuItem p_ctmControl)
        {
            m_mniCommonUse.MenuItems.Clear();

            clsTemplateSetValue[] objValueArr;
            long lngRes = m_objDomain.m_lngGetTemplateSetValue(m_strFormID, m_txtRichTextBox.Name, "����ֵ--%", MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR, out objValueArr);
            if (lngRes <= 0 || objValueArr == null)
                return;

            for (int i = 0; i < objValueArr.Length; i++)
            {
                MenuItem mniItem = new MenuItem(objValueArr[i].m_strSet_Name);
                mniItem.Click += new EventHandler(m_ctmCommonTemplate_Click);
                m_htCommonUseItem.Add(mniItem, objValueArr[i].m_strSet_ID);

                int intIndex = -1;
                string strKeyword = objValueArr[i].m_strKeyword.Replace("����ֵ--", "");
                for (int j = 0; j < m_mniCommonUse.MenuItems.Count; j++)
                {
                    if (strKeyword == m_mniCommonUse.MenuItems[j].Text)
                    {
                        intIndex = j;
                        break;
                    }
                }
                if (intIndex == -1)//û�ҵ�ͬһ����
                {
                    MenuItem mniNewKeyword = new MenuItem(strKeyword);
                    mniNewKeyword.MenuItems.Add(mniItem);
                    m_mniCommonUse.MenuItems.Add(mniNewKeyword);
                }
                else
                    m_mniCommonUse.MenuItems[intIndex].MenuItems.Add(mniItem);

            }

            if (m_mniCommonUse.MenuItems.Count == 1)//ֻ��һ������ֱ�ӽ����г���ֵģ��ŵ���Ŀ¼��
            {
                while (m_mniCommonUse.MenuItems[0].MenuItems.Count > 0)
                {
                    m_mniCommonUse.MenuItems.Add(m_mniCommonUse.MenuItems[0].MenuItems[0]);
                    m_mniCommonUse.MenuItems.RemoveAt(0);
                }
            }
        }

        ///// <summary>
        ///// ���볣��ֵģ��
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void m_mthLoadCommonUseTemplate(MenuCommand p_ppmMenu)
        //{
        //    //m_mniCommonUse.MenuItems.Clear();
        //    m_mncCommonUse.MenuCommands.Clear();

        //    clsTemplateSetValue[] objValueArr;
        //    //long lngRes = m_objDomain.m_lngGetTemplateSetValue(m_strFormID, m_txtRichTextBox.Name, "����ֵ--%", MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR, out objValueArr);
        //    long lngRes = m_objDomain.m_lngGetTemplateSetValue(m_strFormID, m_ctlText.Name, "����ֵ--%", MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR, out objValueArr);
        //    if (lngRes <= 0 || objValueArr == null)
        //        return;

        //    for (int i = 0; i < objValueArr.Length; i++)
        //    {
        //        MenuCommand mniItem = new MenuCommand(objValueArr[i].m_strSet_Name);
        //        mniItem.Click += new EventHandler(m_ctmCommonTemplate_Click);
        //        m_htCommonUseItem.Add(mniItem, objValueArr[i].m_strSet_ID);

        //        int intIndex = -1;
        //        string strKeyword = objValueArr[i].m_strKeyword.Replace("����ֵ--", "");
        //        for (int j = 0; j < m_mncCommonUse.MenuCommands.Count; j++)
        //        {
        //            if (strKeyword == m_mncCommonUse.MenuCommands[j].Text)
        //            {
        //                intIndex = j;
        //                break;
        //            }
        //        }
        //        if (intIndex == -1)//û�ҵ�ͬһ����
        //        {
        //            MenuCommand mniNewKeyword = new MenuCommand(strKeyword);
        //            mniNewKeyword.MenuCommands.Add(mniItem);
        //            m_mncCommonUse.MenuCommands.Add(mniNewKeyword);
        //        }
        //        else
        //            m_mncCommonUse.MenuCommands[intIndex].MenuCommands.Add(mniItem);

        //    }

        //    if (m_mncCommonUse.MenuCommands.Count == 1)//ֻ��һ������ֱ�ӽ����г���ֵģ��ŵ���Ŀ¼��
        //    {
        //        while (m_mncCommonUse.MenuCommands[0].MenuCommands.Count > 0)
        //        {
        //            m_mncCommonUse.MenuCommands.Add(m_mncCommonUse.MenuCommands[0].MenuCommands[0]);
        //            m_mncCommonUse.MenuCommands[0].MenuCommands.RemoveAt(0);
        //        }
        //        m_mncCommonUse.MenuCommands.RemoveAt(0);
        //    }
        //}

        /// <summary>
        /// ���볣��ֵģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthLoadCommonUseTemplate(ToolStripMenuItem p_ppmMenu)
        {
            m_mncCommonUse.DropDownItems.Clear();

            //clsTemplateSetValue[] objValueArr;
            //long lngRes = m_objDomain.m_lngGetTemplateSetValue(m_strFormID, m_ctlText.Name, "����ֵ--%", MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR, out objValueArr);
            //if (lngRes <= 0 || objValueArr == null)
            //    return;

            clsTemplateSet[] objValueArr = null;
            if (m_frmForm is frmHRPBaseForm)
            {
                frmHRPBaseForm frmBase = m_frmForm as frmHRPBaseForm;
                objValueArr = frmBase.m_ObjTemplateClient.m_objCommonUseTemplateSetList(m_strFormID, m_ctlText.Name,
                    com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            }
            if (objValueArr == null)
            {
                return;
            }

            for (int i = 0; i < objValueArr.Length; i++)
            {
                ToolStripMenuItem mniItem = new ToolStripMenuItem(objValueArr[i].m_strSetName);
                mniItem.Click += new EventHandler(m_ctmCommonTemplate_Click);
                m_htCommonUseItem.Add(mniItem, objValueArr[i].m_strSetID);

                int intIndex = -1;
                string strKeyword = objValueArr[i].m_strKeyword.Replace("����ֵ--", "");
                for (int j = 0; j < m_mncCommonUse.DropDownItems.Count; j++)
                {
                    if (strKeyword == m_mncCommonUse.DropDownItems[j].Text)
                    {
                        intIndex = j;
                        break;
                    }
                }
                if (intIndex == -1)//û�ҵ�ͬһ����
                {
                    ToolStripMenuItem rootItem = new ToolStripMenuItem(strKeyword);
                    m_mncCommonUse.DropDownItems.Add(rootItem);
                    rootItem.DropDownItems.Add(mniItem);
                }
                else
                    ((ToolStripMenuItem)m_mncCommonUse.DropDownItems[intIndex]).DropDownItems.Add(mniItem);

            }

            if (m_mncCommonUse.DropDownItems.Count == 1)//ֻ��һ������ֱ�ӽ����г���ֵģ��ŵ���Ŀ¼��
            {
                while (((ToolStripMenuItem)m_mncCommonUse.DropDownItems[0]).DropDownItems.Count > 0)
                {
                    m_mncCommonUse.DropDownItems.Add(((ToolStripMenuItem)m_mncCommonUse.DropDownItems[0]).DropDownItems[0]);
                    //((ToolStripMenuItem)m_mncCommonUse.DropDownItems[0]).DropDownItems.RemoveAt(0);
                }
                m_mncCommonUse.DropDownItems.RemoveAt(0);
            }
        }

        /// <summary>
        /// ��ʼ�����ݸ����ֶ�
        /// </summary>
        public static void s_mthInitDataShareItems(MenuItem p_mniDataShare)
        {
            p_mniDataShare.MenuItems.AddRange(new MenuItem[] { new MenuItem("����"), new MenuItem("-"), new MenuItem("������Ϣ") });

            p_mniDataShare.MenuItems[0].MenuItems.AddRange(new MenuItem[] { new MenuItem("����"), new MenuItem("�Ա�"), new MenuItem("����"), new MenuItem("����"), new MenuItem("��ַ"), new MenuItem("����"), new MenuItem("����"), new MenuItem("����"), new MenuItem("סԺ��"), new MenuItem("��Ժʱ��") });

        }

        /// <summary>
        /// ��ʼ�����ݸ����ֶ�
        /// </summary>
        public static void s_mthInitDataShareItems(ToolStripMenuItem p_mniDataShare)
        {
            p_mniDataShare.DropDownItems.AddRange(new ToolStripItem[] { new ToolStripMenuItem("����"), new ToolStripSeparator(), new ToolStripMenuItem("������Ϣ") });

            ((ToolStripMenuItem)p_mniDataShare.DropDownItems[0]).DropDownItems.AddRange(new ToolStripMenuItem[] { new ToolStripMenuItem("����"), new ToolStripMenuItem("�Ա�"), new ToolStripMenuItem("����"), new ToolStripMenuItem("����"), new ToolStripMenuItem("��ַ"), new ToolStripMenuItem("����"), new ToolStripMenuItem("����"), new ToolStripMenuItem("����"), new ToolStripMenuItem("סԺ��"), new ToolStripMenuItem("��Ժʱ��") });

        }

        /// <summary>
        /// ��ʼ�����ݸ����ֶ�
        /// </summary>
        public static void s_mthInitDataShareItems2(MenuCommand p_mniDataShare)
        {
            p_mniDataShare.MenuCommands.AddRange(new MenuCommand[] { new MenuCommand("����"), new MenuCommand("-"), new MenuCommand("������Ϣ") });

            p_mniDataShare.MenuCommands[0].MenuCommands.AddRange(new MenuCommand[] { new MenuCommand("����"), new MenuCommand("�Ա�"), new MenuCommand("����"), new MenuCommand("����"), new MenuCommand("��ַ"), new MenuCommand("����"), new MenuCommand("����"), new MenuCommand("����"), new MenuCommand("סԺ��"), new MenuCommand("��Ժʱ��") });
        }

        /// <summary>
        /// �������ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareItemsEvent(ToolStripMenuItem p_mniRoot)
        {
            if (p_mniRoot.DropDownItems.Count > 0)
            {
                for (int i = 0; i < p_mniRoot.DropDownItems.Count; i++)
                {
                    if (p_mniRoot.DropDownItems[i] is ToolStripMenuItem)
                    {
                        m_mthAssociateDataShareItemsEvent(((ToolStripMenuItem)p_mniRoot.DropDownItems[i]));
                    }
                }
            }
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareItemClick);
        }

        /// <summary>
        /// �������ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareItemsEvent(MenuItem p_mniRoot)
        {
            if (p_mniRoot.MenuItems.Count > 0)
                for (int i = 0; i < p_mniRoot.MenuItems.Count; i++)
                    m_mthAssociateDataShareItemsEvent(p_mniRoot.MenuItems[i]);
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareItemClick);
        }

        /// <summary>
        /// �������ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareItemsEvent2(MenuCommand p_mniRoot)
        {
            if (p_mniRoot.MenuCommands.Count > 0)
                for (int i = 0; i < p_mniRoot.MenuCommands.Count; i++)
                    m_mthAssociateDataShareItemsEvent2((p_mniRoot.MenuCommands[i]) as MenuCommand);
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareItemClick2);
        }

        /// <summary>
        /// ���ݸ��ø�ʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthDataShareItemClick(object sender, EventArgs e)
        {
            //�˴������жϣ�����ǡ�������Ϣ�����򵯳�����
            string strContent = "";
            string strText = string.Empty;
            bool blnIsToolStripMenuItem = false;

            if (sender is MenuItem)
            {
                strText = ((MenuItem)sender).Text;
            }
            else if (sender is ToolStripMenuItem)
            {
                strText = ((ToolStripMenuItem)sender).Text;
                blnIsToolStripMenuItem = true;
            }

            if (strText != "������Ϣ")
            {
                if (!blnIsToolStripMenuItem)
                {
                    strContent = "[" + ((MenuItem)sender).Parent.ToString().Substring(((MenuItem)sender).Parent.ToString().IndexOf("Text: ") + 6) + "--" + ((MenuItem)sender).Text + "]";
                }
                else
                {
                    strContent = "[" + ((ToolStripMenuItem)sender).OwnerItem.Text + "--" + ((ToolStripMenuItem)sender).Text + "]";
                }
                m_mthInertText(strContent);
            }
            else
            {
                com.digitalwave.Emr.Utility.DataShare.frmDataShareTool.m_mthShowDataShareTool(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate, true, out strContent);
                if (strContent != "") m_mthInertText(strContent);
            }


        }
        /// <summary>
        /// ���ݸ��ø�ʽ2,MenuCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthDataShareItemClick2(object sender, EventArgs e)
        {
            //�˴������жϣ�����ǡ�������Ϣ�����򵯳�����
            string strContent = "";
            //if((MenuItem)sender)
            if (((MenuCommand)sender).Text != "������Ϣ")
            {
                strContent = "[" + ((MenuCommand)sender).Parent.Text + "--" + ((MenuCommand)sender).Text + "]";
                m_mthInertText(strContent);
            }
            else
            {
                //this.Cursor =Cursors.WaitCursor;
                frmDataShareTool.m_mthShowDataShareTool(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate, true, out strContent);
                if (strContent != "") m_mthInertText(strContent);

                //this.Cursor =Cursors.Default;

            }


        }

        /// <summary>
        /// ����ֱ�����ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareInvokingItemsEvent(ToolStripMenuItem p_mniRoot)
        {
            if (p_mniRoot.DropDownItems.Count > 0)
            {
                for (int i = 0; i < p_mniRoot.DropDownItems.Count; i++)
                {
                    if (p_mniRoot.DropDownItems[i] is ToolStripMenuItem)
                    {
                        m_mthAssociateDataShareInvokingItemsEvent(((ToolStripMenuItem)p_mniRoot.DropDownItems[i]));
                    }
                }
            }
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareInvokingItemClick);
        }

        /// <summary>
        /// ����ֱ�����ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareInvokingItemsEvent(MenuItem p_mniRoot)
        {
            if (p_mniRoot.MenuItems.Count > 0)
                for (int i = 0; i < p_mniRoot.MenuItems.Count; i++)
                    m_mthAssociateDataShareInvokingItemsEvent(p_mniRoot.MenuItems[i]);
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareInvokingItemClick);
        }

        /// <summary>
        /// ����ֱ�����ݸ��ò˵�����¼�
        /// </summary>
        /// <param name="p_mniRoot"></param>
        private void m_mthAssociateDataShareInvokingItemsEvent2(MenuCommand p_mniRoot)
        {
            if (p_mniRoot.MenuCommands.Count > 0)
                for (int i = 0; i < p_mniRoot.MenuCommands.Count; i++)
                    m_mthAssociateDataShareInvokingItemsEvent2((p_mniRoot.MenuCommands[i]) as MenuCommand);
            else
                p_mniRoot.Click += new EventHandler(m_mthDataShareInvokingItemClick2);
        }

        /// <summary>
        /// ֱ�����ݸ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthDataShareInvokingItemClick(object sender, EventArgs e)
        {
            m_mthDataShareInvokingItemClick2(sender, e);
        }

        /// <summary>
        /// ֱ�����ݸ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthDataShareInvokingItemClick2(object sender, EventArgs e)
        {
            if (MDIParent.s_ObjCurrentPatient == null)
            {
                MessageBox.Show("��ѡ���ߣ�");
                return;
            }
            //�˴������жϣ�����ǡ�������Ϣ�����򵯳�����
            string strContent = "";
            string m_strMenuText = "";
            //if((MenuItem)sender)

            string strParentText = "";
            string strText = "";

            if (sender is MenuCommand)
            {
                strParentText = ((MenuCommand)sender).Parent.Text;
                strText = ((MenuCommand)sender).Text;
            }
            else if (sender is MenuItem)
            {
                strParentText = ((MenuItem)sender).Parent.ToString().Substring(((MenuItem)sender).Parent.ToString().IndexOf("Text: ") + 6);
                strText = ((MenuItem)sender).Text;
            }
            else if (sender is ToolStripMenuItem)
            {
                strParentText = ((ToolStripMenuItem)sender).OwnerItem.Text;
                strText = ((ToolStripMenuItem)sender).Text;
            }

            if (strText != "������Ϣ")
            {

                strContent = "[" + strParentText + "--" + strText + "]";
                clsDataShareReplace.s_mthReplaceDataShareValue(MDIParent.s_ObjCurrentPatient, ref strContent, true);

                m_mthInertText(strContent);
            }
            else
            {
                //this.Cursor =Cursors.WaitCursor;

                frmDataShareTool.m_mthShowDataShareTool(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate, false, out strContent);
                if (strContent != "") m_mthInertText(strContent);

                //this.Cursor =Cursors.Default;
            }
        }


        #region ��ʼ���ؼ�
        private void m_mthInitInput(Form p_frmInput)
        {
            //			if(m_blnIsInited)
            //				return;
            //
            //			m_blnIsInited = true;
            //
            //			m_lstTemplate=new ListBox ();
            //			m_txtInputKeyword=new TextBox ();
            //			m_txtPreviewTemplate = new ctlTemplateEditer();
            //
            //			p_frmInput.Controls.Add (m_lstTemplate);
            //			p_frmInput.Controls.Add (m_txtInputKeyword);
            //			p_frmInput.Controls.Add (m_txtPreviewTemplate);
            //
            //			this.m_lstTemplate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
            //			this.m_lstTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //			this.m_lstTemplate.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            //			this.m_lstTemplate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
            //			this.m_lstTemplate.ItemHeight = 25;
            //			this.m_lstTemplate.Location = new System.Drawing.Point(10, 10);
            //			this.m_lstTemplate.Name = "m_lstTemplate";
            //			this.m_lstTemplate.Size = new System.Drawing.Size(200, 300);
            //			this.m_lstTemplate.TabIndex = 15006;
            //			this.m_lstTemplate.BringToFront ();
            //			this.m_lstTemplate.Visible = false;
            //			this.m_lstTemplate.HorizontalScrollbar = true;
            //
            //			this.m_txtInputKeyword.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
            //			this.m_txtInputKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle ;
            //			this.m_txtInputKeyword.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            //			this.m_txtInputKeyword.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
            //			this.m_txtInputKeyword.Location = new System.Drawing.Point(10, 10);
            //			this.m_txtInputKeyword.Name = "m_txtInputKeyword";
            //			this.m_txtInputKeyword.Size = new System.Drawing.Size(200, 19);
            //			this.m_txtInputKeyword.TabIndex = 15000;
            //			this.m_txtInputKeyword.Text = "";
            //
            //			this.m_lstTemplate.BringToFront ();
            //			this.m_txtInputKeyword.Visible =false;
            //
            //			this.m_txtInputKeyword.KeyDown +=new System.Windows.Forms.KeyEventHandler(this.m_mthKeywordText_KeyDown ); 
            //			this.m_lstTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lstTemplate_KeyDown);
            //			this.m_lstTemplate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_lstTemplate_KeyPress);
            //			this.m_lstTemplate.MouseDown   +=new System.Windows.Forms.MouseEventHandler  (this.m_lstTemplate_MouseDown );
            //
            //			this.m_lstTemplate.LostFocus  += new System.EventHandler(this.m_txtPreviewTemplate_LostFocus);
            //			this.m_txtInputKeyword.LostFocus +=new System.EventHandler(this.m_txtPreviewTemplate_LostFocus);
            //			this.m_lstTemplate.SelectedIndexChanged += new System.EventHandler(this.m_lstTemplate_SelectedIndexChanged);
            //
            //			// 
            //			// m_txtPreviewTemplate
            //			// 
            //			this.m_txtPreviewTemplate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
            //			this.m_txtPreviewTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle ;
            //			this.m_txtPreviewTemplate.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            //			this.m_txtPreviewTemplate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
            //			this.m_txtPreviewTemplate.Location = new System.Drawing.Point(10, 10);
            //			this.m_txtPreviewTemplate.Name = "m_txtPreviewTemplate";
            ////			this.m_txtPreviewTemplate.Size = new System.Drawing.Size(400, 300+19-3);
            //			this.m_txtPreviewTemplate.Size = new System.Drawing.Size(400, 300-3);
            //			this.m_txtPreviewTemplate.TabIndex = 15007;
            //			this.m_txtPreviewTemplate.Text = "";
            //			this.m_txtPreviewTemplate.BringToFront();
            //			this.m_txtPreviewTemplate.Visible = false;
            //			this.m_txtPreviewTemplate.LostFocus += new System.EventHandler(m_txtPreviewTemplate_LostFocus);
        }
        #endregion

        private bool m_blnShowMessagebox = false;
        private void m_txtPreviewTemplate_LostFocus(object sender, System.EventArgs e)
        {
            if (!m_txtInputKeyword.Focused && !m_lstTemplate.Focused && !m_txtPreviewTemplate.Focused && !m_blnShowMessagebox)
            {
                m_txtInputKeyword.Visible = false;
                m_lstTemplate.Visible = false;
                m_txtPreviewTemplate.Visible = false;
                m_intPreSelectedIndex = -1;
            }
        }

        private int m_intPreSelectedIndex = -1;
        private void m_lstTemplate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (m_lstTemplate.SelectedIndex != m_intPreSelectedIndex)
            {
                m_intPreSelectedIndex = m_lstTemplate.SelectedIndex;
                m_mthPreviewTemplate();
            }
        }


        /// <summary>
        /// ��ʾԤ����
        /// </summary>
        private void m_mthShowPreviewTemplate()
        {
            m_txtPreviewTemplate.Top = m_lstTemplate.Top;
            m_txtPreviewTemplate.Left = m_lstTemplate.Right;
            m_txtPreviewTemplate.Visible = true;
            m_txtPreviewTemplate.BringToFront();
        }

        clsTemplatesetContentValue[] m_objTemplatesetContent;
        private string m_strSetID = "";

        /// <summary>
        /// ģ��Ԥ��
        /// </summary>
        private void m_mthPreviewTemplate()
        {
            m_mthShowPreviewTemplate();
            #region ��ǰ�������пؼ�
            //			clsGUI_Info_DetailValue[] objControls = m_objDomain.lngGetAllControls(m_strFormID);
            //			Hashtable htControls = new Hashtable();
            //			for(int i=0;i<objControls.Length;i++)
            //				htControls.Add(objControls[i].m_strControl_ID,objControls[i].m_strControl_Desc);
            #endregion

            StringBuilder sb = new StringBuilder();

            #region old
            //			switch(m_intSelectedLevel)
            //			{
            //				case 3:		//�ؼ��ֲ�
            //					if(m_lstTemplate.Text.EndsWith("..."))
            //					{
            //						//һ���ؼ��ֶ�Ӧ�������
            //						m_strKeyWord = m_lstTemplate.Text.Replace("...","");
            //						m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID,m_txtRichTextBox.Name ,m_strKeyWord,MDIParent.OperatorID,MDIParent.s_ObjDepartment.m_StrDeptID);
            //						if(m_objTemplateSet_ID_Name!=null && m_objTemplateSet_ID_Name.Length > 0)
            //						{
            //							for(int i=0;i<m_objTemplateSet_ID_Name.Length;i++)	
            //							{
            //								sb.Append(m_objTemplateSet_ID_Name[i].m_strSet_Name);
            //								sb.Append("\r\n");
            //							}
            //						}
            //					}
            //					else
            //					{
            //						//һ���ؼ��ֶ�Ӧһ�����ƣ�ֱ��load��ģ���ֵ
            //						m_strKeyWord = m_lstTemplate.Text;
            //						m_objTemplateSet_ID_Name=m_objDomain.lngGetSetTemplateKeywordName(m_strFormID,m_txtRichTextBox.Name,m_strKeyWord,MDIParent.OperatorID,MDIParent.s_ObjDepartment.m_StrDeptID);
            //						m_objTemplatesetContent = m_objDomain.lngGetAllTemplatesetContent(m_objTemplateSet_ID_Name[0].m_strSet_ID);
            //						if(m_objTemplatesetContent==null || m_objTemplatesetContent.Length<=0)
            //							return;
            //
            //						for(int i=0;i<m_objTemplatesetContent.Length;i++)	
            //						{							
            //							sb.Append(htControls[m_objTemplatesetContent[i].m_strControl_ID].ToString());
            //							sb.Append(":\r\n    ");
            //							sb.Append(m_objTemplatesetContent[i].m_strContent);
            //							sb.Append("\r\n");
            //							sb.Append("         ");
            //							sb.Append("\r\n");
            //						}
            //					}					
            //					break;
            //				case 4:		//ģ������					
            //					//					string strSetID="";
            //					if(m_objTemplateSet_ID_Name !=null && m_objTemplateSet_ID_Name.Length >m_intSelectContentIndex )
            //					{
            //						m_strSetID=m_objTemplateSet_ID_Name[m_lstTemplate.SelectedIndex].m_strSet_ID;
            //					}
            //					if(m_strSetID=="")return;
            //					m_objTemplatesetContent = m_objDomain.lngGetAllTemplatesetContent(m_strSetID);
            //					if(m_objTemplatesetContent==null || m_objTemplatesetContent.Length<=0)
            //						return;
            //					for(int i=0;i<m_objTemplatesetContent.Length;i++)	
            //					{
            //						sb.Append(htControls[m_objTemplatesetContent[i].m_strControl_ID].ToString());
            //						sb.Append(":\r\n    ");
            //						sb.Append(m_objTemplatesetContent[i].m_strContent);
            //						sb.Append("\r\n");
            //						sb.Append("         ");
            //						sb.Append("\r\n");
            //					}
            //					break;
            //			}
            #endregion

            if (m_lstTemplate.Text.EndsWith("..."))//ѡ�е��Ƿ���
            {
                sb.Append(m_strLoadSubKeywordOrTemplateName());
            }
            else//ѡ�е��Ǿ���ĳһ��ģ����
            {
                //				m_strSetID = ((clsTemplateSet_ID_NameValue)m_lstTemplate.SelectedItem).m_strSet_ID;
                m_strSetID = m_htSet_ID[m_lstTemplate.SelectedIndex].ToString();
                m_objTemplatesetContent = m_objDomain.lngGetAllTemplatesetContent(m_strSetID);
                if (m_objTemplatesetContent == null || m_objTemplatesetContent.Length <= 0)
                    return;
                for (int i = 0; i < m_objTemplatesetContent.Length; i++)
                {
                    //					sb.Append(htControls[m_objTemplatesetContent[i].m_strControl_ID].ToString());
                    sb.Append(m_objTemplatesetContent[i].m_strControl_Desc);
                    sb.Append(":\r\n");
                    sb.Append(m_objTemplatesetContent[i].m_strContent);
                    sb.Append("\r\n");
                    sb.Append("         ");
                    sb.Append("\r\n");
                }
            }
            m_txtPreviewTemplate.m_mthSetTemplateText(sb.ToString());
        }

        private void m_lstTemplate_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks > 1)
            {
                //				KeyEventArgs ee=new KeyEventArgs (System.Windows.Forms.Keys.Enter);
                //				m_lstTemplate_KeyDown(this.m_lstTemplate ,ee);

                if (m_lstTemplate.Text.EndsWith("..."))//ѡ�е��Ƿ���
                {
                    m_intPreSelectedIndex = -1;//������һ��֮ǰ���ϴ�ѡ����һ�и�λ

                    if (m_strCurFolder == "")
                        m_strCurFolder = m_lstTemplate.Text.Replace("...", "");
                    else
                        m_strCurFolder += ">>" + m_lstTemplate.Text.Replace("...", "");
                    m_mthLoadSubKeywordOrTemplateName();
                }
                else//ѡ�е��Ǿ���ĳһ��ģ����
                {
                    m_mthReplaceTemplateToTextBox();
                }
            }
        }
        private void m_txtInputKeyword_Leave(object sender, System.EventArgs e)
        {
            //			if(this.m_lstTemplate.Visible )
            //			{
            //				m_lstTemplate.Focus ();
            //				return;
            //			}
            //
            //			if(!this.m_txtInputKeyword.Focus () && !this.m_lstTemplate.Focus () && !this.m_txtRichTextBox.Focus () && !this.m_txtInputKeyword.Focus ())
            //			{
            //				this.m_txtInputKeyword.Visible =false;
            //				this.m_lstTemplate.Visible =false;
            //				this.m_txtPreviewTemplate.Visible = false;
            //				this.m_txtRichTextBox.Focus ();
            //				this.m_mthResetValiant ();
            //			}
        }
        #region ��Ա����,��ӱԴ,2003-5-9 16:11:08
        bool m_blnIsInited = false;

        private ListBox m_lstTemplate;
        private TextBox m_txtInputKeyword;
        private ctlTemplateEditer m_txtPreviewTemplate;
        private int m_intLeftOfferset = 0;
        private int m_intTopOfferset = 0;

        private int m_intSelectedLevel = -1;						//��ǰListBox�ڵڼ���
        private int m_intSingleSetTemplate = -1;					//ѡ���˵�Ԫģ�廹����װģ��
        private int m_intSelectContentIndex = -1;					//ѡ�е�ģ��ID

        private int m_intByTemplate = -1;							//ѡ����BY ICD-10,�ؼ���,�˴�ϵͳ

        internal Form m_frmForm;									//����
        private RichTextBox m_txtRichTextBox;					//�����TextBox
        private string m_strFormID = "";							//FormID
        private string m_strControlID = "";						//ControlID
        private string m_strKeyWord = "";						//�ؼ�������		

        private clsTemplatesetInvoke m_objTemplatesetParent;	//��ʵ��ָ��

        private clsTemplate_KeywordValue[] m_objTemplate_KeyWord = null;	//ģ��ؼ���
        private clsTemplate_DetailValue[] m_objTemplate_Detail = null;	//ģ������

        private clsTemplate_Set_KeywordValue[] m_objTemplateSet_KeywordValue = null;		//TemplateSetKeyword
        private clsTemplateSet_ID_NameValue[] m_objTemplateSet_ID_Name = null;		//TemplateSetKeywordName

        private clsTemplateDomain m_objDomain = new clsTemplateDomain();
        private Hashtable m_hasContolIDs = new Hashtable();

        /// <summary>
        /// ����Ŀؼ�(����Ϊ����TextBox��ComboBox)
        /// </summary>
        private Control m_ctlText = null;
        #endregion

        public void m_mthSetParent(clsTemplatesetInvoke p_objTemplatesetInvoke)
        {
            m_objTemplatesetParent = p_objTemplatesetInvoke;
        }
        private void m_mthResetValiant()
        {

            //			System.Drawing.Point pt= m_txtRichTextBox.GetPositionFromCharIndex(this.m_txtRichTextBox.Text.Length );
            //
            //			this.m_txtInputKeyword.Left = m_intLeftOfferset + pt.X ;
            //			this.m_txtInputKeyword.Top  =m_intTopOfferset +pt.Y+20;
            //			this.m_lstTemplate.Left = m_intLeftOfferset + pt.X ;
            //			this.m_lstTemplate.Top =m_intTopOfferset +pt.Y + 40;
            //		
            //			m_intSelectedLevel=-1;						//��ǰListBox�ڵڼ���
            //			m_intSingleSetTemplate=-1;					//ѡ���˵�Ԫģ�廹����װģ��
            //			m_intSelectContentIndex=-1;					//ѡ�е�ģ��ID
            //
            //			m_intByTemplate=-1;							//ѡ����BY ICD-10,�ؼ���,�˴�ϵͳ
            //
            //			m_objTemplate_Detail=null;	//ģ��
        }

        private void m_mthCalculatePosition(Form p_frmInput, Control p_txtInput)
        {
            Control ctlTemp = p_txtInput;

            Control ctlBase = p_frmInput;
            if (ctlBase == null) return;
            m_intLeftOfferset = 0;
            m_intTopOfferset = 0;

            while (!ctlTemp.Equals(ctlBase))
            {
                if (ctlTemp.Parent == null)
                    return;

                m_intLeftOfferset += ctlTemp.Location.X;
                m_intTopOfferset += ctlTemp.Location.Y;

                ctlTemp = ctlTemp.Parent;
            }
        }

        private Hashtable htIfAddMenuItem = new Hashtable();

        public void m_mthAddTextBox(Form p_frmInput, Control p_txtInput, string p_strFormID, string p_strControlID)
        {
            m_hasContolIDs[p_txtInput] = p_strControlID;

            m_frmForm = p_frmInput;
            //m_txtRichTextBox = p_txtInput;
            m_ctlText = p_txtInput;
            m_strFormID = p_strFormID;
            m_strControlID = p_strControlID;
            //			p_txtInput.KeyDown += new KeyEventHandler(m_mthKeyDown);//��Ҫmb.


            if (p_txtInput.ContextMenuStrip != null && htIfAddMenuItem[p_frmInput] == null)
            {
                this.m_ctlText.ContextMenuStrip.Items.Add(m_mncTemplate);
                this.m_ctlText.ContextMenuStrip.Items.Add(m_mncCommonUse);
                htIfAddMenuItem[p_frmInput] = false;
            }
            else
            {
                this.m_ctlText.ContextMenuStrip = m_ppmMenu;
            }

            //if (p_txtInput.ContextMenu != null && htIfAddMenuItem[p_frmInput] == null)
            //{
            //    p_txtInput.ContextMenu.MenuItems.Add(m_mniSuperSubScript);
            //    p_txtInput.ContextMenu.MenuItems.Add("-");
            //    while (m_ctmControl.MenuItems.Count > 0)
            //        p_txtInput.ContextMenu.MenuItems.Add(m_ctmControl.MenuItems[0]);
            //    //				p_txtInput.ContextMenu.MergeMenu(m_ctmControl);
            //    //				p_txtInput.ContextMenu = m_ctmControl;
            //    p_txtInput.ContextMenu.Popup += new EventHandler(m_ctmPopup);
            //    htIfAddMenuItem[p_frmInput] = false;
            //    m_intStartRemove = m_ctmControl.MenuItems.Count;
            //}
            //else if (p_txtInput.ContextMenu == null)//���µ��Ҽ��˵�
            //{
            //    this.m_ctlText.MouseUp += new MouseEventHandler(m_txtTextBox_MouseUp);
            //    //this.m_txtRichTextBox.MouseUp += new MouseEventHandler(m_txtRichTextBox_MouseUp);
            //    //				p_txtInput.ContextMenu = m_ctmControl;
            //    //				m_intStartRemove = m_ctmControl.MenuItems.Count;
            //}

            m_mthCalculatePosition(m_frmForm, m_ctlText);
            this.m_ctlText.Enter += new EventHandler(m_ctlText_Enter);
            //m_mthInitInput(p_frmInput);

            //this.m_txtRichTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_txtRichTextBox_MouseDown);
            //this.m_txtRichTextBox.Enter += new System.EventHandler(this.m_txtRichTextBox_Enter);
        }

        private void m_ctlText_Enter(object sender, EventArgs e)
        {
            m_ctlText = (Control)sender;
            m_mthCalculatePosition(m_frmForm, m_ctlText);
            m_strControlID = m_hasContolIDs[m_ctlText].ToString();
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                m_mthSetPatientID(((Control)sender).FindForm());
                m_mthLoadCommonUseTemplate(m_mncCommonUse);
                m_mncCommonUse.Enabled = m_mncCommonUse.DropDownItems.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private Hashtable m_htCommonUseItem = new Hashtable();

        /// <summary>
        /// ɾ������ֵģ��Ŀ�ʼ��
        /// </summary>
        private int m_intStartRemove = -1;

        /// <summary>
        /// �������ֵģ��
        /// </summary>
        private void m_mthLoadCommonUse()
        {
            m_mniCommonUse.MenuItems.Clear();
            //����ճ���ֵģ��
            while (m_ctmControl.MenuItems.Count > m_intStartRemove)
            {
                m_ctmControl.MenuItems.RemoveAt(m_intStartRemove);
            }

            if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
            {
                string strKeyword = "";
                for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                {
                    if (m_objTemplateSet_KeywordValue[i].m_strKeyword.StartsWith("����ֵ") && m_objTemplateSet_KeywordValue[i].m_strKeyword != strKeyword)
                    {
                        m_mniCommonUse.MenuItems.Add(m_objTemplateSet_KeywordValue[i].m_strKeyword);
                        strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;
                    }
                }

                if (m_mniCommonUse.MenuItems.Count < 1)
                    return;
                else
                {
                    m_ctmControl.MenuItems.Add("-");

                    if (m_mniCommonUse.MenuItems.Count == 1)
                    {
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, m_mniCommonUse.MenuItems[0].Text, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);

                        m_mniCommonUse.MenuItems.Clear();

                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int j = 0; j < m_objTemplateSet_ID_Name.Length; j++)
                            {
                                MenuItem mniItem = new MenuItem(m_objTemplateSet_ID_Name[j].m_strSet_Name);
                                mniItem.Click += new EventHandler(m_ctmCommonTemplate_Click);
                                m_ctmControl.MenuItems.Add(mniItem);
                                m_htCommonUseItem.Add(mniItem, m_objTemplateSet_ID_Name[j].m_strSet_ID);
                            }
                        }
                    }
                    else
                    {
                        ArrayList arlTemp = new ArrayList();

                        for (int i = 0; i < m_mniCommonUse.MenuItems.Count; i++)
                        {
                            m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, m_mniCommonUse.MenuItems[i].Text, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
                            if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                            {
                                for (int j = 0; j < m_objTemplateSet_ID_Name.Length; j++)
                                {
                                    MenuItem mniItem = new MenuItem(m_objTemplateSet_ID_Name[j].m_strSet_Name);
                                    mniItem.Click += new EventHandler(m_ctmCommonTemplate_Click);
                                    m_htCommonUseItem.Add(mniItem, m_objTemplateSet_ID_Name[j].m_strSet_ID);

                                    if (m_mniCommonUse.MenuItems[i].Text.Equals("����ֵ"))
                                    {
                                        m_ctmControl.MenuItems.Add(mniItem);
                                    }
                                    else
                                    {
                                        m_mniCommonUse.MenuItems[i].Text = m_mniCommonUse.MenuItems[i].Text.Replace("����ֵ--", "");

                                        m_mniCommonUse.MenuItems[i].MenuItems.Add(mniItem);
                                    }
                                }
                            }

                            if (!m_mniCommonUse.MenuItems[i].Text.Equals("����ֵ"))
                            {
                                arlTemp.Add(m_mniCommonUse.MenuItems[i]);
                            }
                        }

                        m_ctmControl.MenuItems.AddRange((MenuItem[])arlTemp.ToArray(typeof(MenuItem)));

                    }


                }

            }
        }

        /// <summary>
        /// ����ֵ�����
        /// </summary>
        //		private int m_intSelectionStart;

        /// <summary>
        /// ����Ƿ���ģ�壬���û�У���ģ�尴ť������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctmPopup(object sender, EventArgs e)
        {
            m_mthSetItemsVisible();

            //			m_mthLoadCommonUse();
            m_mthLoadCommonUseTemplate(m_mniCommonUse);
            m_mniCommonUse.Enabled = m_mniCommonUse.MenuItems.Count > 0 ? true : false;
        }

        /// <summary>
        /// ����һЩ�Ҽ��˵���Ŀ�Ŀɼ���
        /// </summary>
        private void m_mthSetItemsVisible()
        {
            ContextMenu ctm = m_txtRichTextBox.ContextMenu;

            if (m_frmForm.Name == "frmGeneralDisease" && m_txtRichTextBox.Name == "m_txtRecordTitle")
            {
                for (int i = 0; i < ctm.MenuItems.Count; i++)
                {
                    if (ctm.MenuItems[i].Text != "ģ��")
                        ctm.MenuItems[i].Visible = false;
                    else
                        break;
                }
            }
            else
            {
                for (int i = 0; i < ctm.MenuItems.Count; i++)
                {
                    ctm.MenuItems[i].Visible = true;
                }
            }
        }

        private void m_ctmCommonTemplate_Click(object sender, EventArgs e)
        {
            string strSetID = m_htCommonUseItem[sender].ToString();

            //clsTemplatesetContentValue[] objTemplatesetContent = m_objDomain.lngGetAllTemplatesetContent(strSetID);
            clsTemplate[] objTemplate = null;
            if (m_frmForm is frmHRPBaseForm)
            {
                frmHRPBaseForm frmBase = m_frmForm as frmHRPBaseForm;
                if (frmBase.m_ObjTemplateClient != null)
                {
                    objTemplate = frmBase.m_ObjTemplateClient.m_objCommonUseTemplateArr(strSetID);
                }
            }

            m_objTemplatesetParent.m_mthLoadTemplateset(objTemplate, m_ctlText);
        }

        /// <summary>
        /// ��������ģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctmTemplate_Click(object sender, EventArgs e)
        {
            //			if(m_frmForm.Name.StartsWith("frmInPatientCaseHistory") && (m_txtRichTextBox.Name == "m_txtPrimaryDiagnose" || m_txtRichTextBox.Name == "m_txtCurrentStatus"))
            //				m_blnIfSaveDisease = true;

            //			if(m_frmForm.Name.StartsWith("frmInPatientCaseHistory"))
            //				m_blnIfSaveDisease = true;
            //
            //			#region old
            ////			this.m_txtInputKeyword.Text ="mb.";
            ////			m_strTemplate_Type = ".��װģ��.���ؼ���.";
            ////			m_intSingleSetTemplate = 0;
            ////			m_intByTemplate = 0;
            ////			this.m_mthKeywordText_KeyDown (m_txtInputKeyword ,new KeyEventArgs(Keys.Decimal));
            //			#endregion
            //			m_intPreSelectedIndex = -1;
            //			m_strCurFolder = "";
            ////			m_mthLoadAllTemplateKeyword();
            //			new frmTemplateSelect(this).Show();
            try
            {
                if (m_frmForm is frmHRPBaseForm)
                {
                    frmHRPBaseForm frmBase = m_frmForm as frmHRPBaseForm;
                    if (frmBase.m_ObjTemplateClient != null)
                    {
                        frmBase.m_ObjTemplateClient.m_mthUseTemplate(m_ctlText, com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjSelectArea.m_strDEPTID_CHR);
                    }
                }
                else
                {
                    m_mthInvokeTemplate();
                }
            }
            catch (Exception exp)
            {
                string strErrMessage = exp.Message + "\n at Module:[" + exp.TargetSite.ReflectedType.Name + "]\n  Method:[" + exp.TargetSite.Name + "]";
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.Log2File(MDIParent.s_strErrorFilePath, "Exception: \r\n" + strErrMessage);
            }
        }

        private void m_txtRichTextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_mthSetCurRichTextBox((RichTextBox)sender);
        }
        private void m_txtRichTextBox_Enter(object sender, System.EventArgs e)
        {
            m_txtRichTextBox = (RichTextBox)sender;
            m_mthCalculatePosition(m_frmForm, m_txtRichTextBox);
            m_strControlID = m_hasContolIDs[m_txtRichTextBox].ToString();

        }

        private string m_strTemplate_Type = ".��װģ��.���ؼ���.";

        public void m_mthKeywordText_KeyDown(object p_objSender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.m_lstTemplate.Visible = false;
                this.m_txtInputKeyword.Visible = false;
                this.m_txtRichTextBox.Focus();
                m_mthResetValiant();
                return;
            }
            if (m_txtInputKeyword.Visible == false)
            {
                int intSelectionStartIndex = this.m_txtRichTextBox.SelectionStart;
                System.Drawing.Point pt = this.m_txtRichTextBox.GetPositionFromCharIndex(intSelectionStartIndex);

                this.m_txtInputKeyword.Left = m_intLeftOfferset + pt.X;
                this.m_txtInputKeyword.Top = m_intTopOfferset + pt.Y + 20;
                this.m_lstTemplate.Left = m_intLeftOfferset + pt.X;
                this.m_lstTemplate.Top = m_intTopOfferset + pt.Y + 40;
            }
            this.m_lstTemplate.Items.Clear();

            if ((e.KeyCode == System.Windows.Forms.Keys.Decimal || e.KeyCode == System.Windows.Forms.Keys.OemPeriod || e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                //�ж��Ƿ����ListBox,��С����ǰ�Ƿ�mb

                int intSelectStart = this.m_txtRichTextBox.SelectionStart;
                int intPointnumber = 0;
                string strText = "";
                strText = this.m_txtInputKeyword.Text + m_strTemplate_Type;
                //				m_intSingleSetTemplate = 0;
                //				m_intByTemplate = 0;
                strText = strText.Replace("..", ".");
                for (int i = 0; i < strText.Length; i++)
                    if (strText[i] == '.') intPointnumber++;
                m_intSelectedLevel = intPointnumber;
                switch (intPointnumber)
                {
                    case 1:
                        this.m_lstTemplate.Items.Add("��װģ��");
                        this.m_lstTemplate.Items.Add("����ģ��");
                        break;
                    case 2:
                        this.m_lstTemplate.Items.Add("���ؼ���");
                        this.m_lstTemplate.Items.Add("��ICD-10");
                        this.m_lstTemplate.Items.Add("���˴�ϵͳ");
                        if (this.m_txtInputKeyword.Text.EndsWith("����ģ��"))
                            this.m_lstTemplate.Items.Add("������ֵ");
                        break;
                    case 3:
                        m_mthLoadTemplate();
                        break;
                    case 4:
                        m_mthLoadTemplateName();
                        break;
                }

                m_mthAddListBoxItemPYCode(this.m_lstTemplate);

                this.m_txtInputKeyword.Text = this.m_txtInputKeyword.Text.Replace("..", ".");
                this.m_txtInputKeyword.Visible = true;
                this.m_txtInputKeyword.SelectionStart = this.m_txtInputKeyword.TextLength;
                this.m_txtInputKeyword.SelectionLength = 0;
                this.m_txtInputKeyword.BringToFront();
                this.m_lstTemplate.Visible = true;
                this.m_lstTemplate.BringToFront();

                if (this.m_lstTemplate.Items.Count > 0)
                {
                    this.m_lstTemplate.SelectedIndex = 0;
                }
                this.m_lstTemplate.Focus();
                if (this.m_lstTemplate.Items.Count <= 0 && this.m_txtInputKeyword.Visible == true)
                    this.m_txtInputKeyword.Focus();
            }


        }

        public void m_mthKeyDown(object p_objSender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.m_lstTemplate.Visible = false;
                this.m_txtInputKeyword.Visible = false;
                this.m_txtRichTextBox.Focus();
                m_mthResetValiant();
                return;
            }
            m_txtRichTextBox = (RichTextBox)p_objSender;
            m_mthCalculatePosition(m_frmForm, m_txtRichTextBox);

            if ((e.KeyCode == System.Windows.Forms.Keys.Decimal || e.KeyCode == System.Windows.Forms.Keys.OemPeriod))
            {
                //�ж��Ƿ����ListBox,��С����ǰ�Ƿ�mb
                int intSelectStart = this.m_txtRichTextBox.SelectionStart;

                if (intSelectStart < 2) return;
                string strMB = this.m_txtRichTextBox.Text.Substring(intSelectStart - 2, 2);
                if (strMB != "mb")
                {
                    return;
                }
                else
                {
                    this.m_txtInputKeyword.Text = "mb.";
                    this.m_mthKeywordText_KeyDown(m_txtInputKeyword, e);
                }
            }
        }


        private DateTime m_dtPre;//�൱��DateTime.MinValue
        private string m_strQuery;

        /// <summary>
        /// ListBoxģ����ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lstTemplate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TimeSpan ts;

            if (m_dtPre == DateTime.MinValue)
            {
                m_dtPre = DateTime.Now;
                m_strQuery = e.KeyChar.ToString().ToUpper();//���д����;���û�����ʱ������Сд״̬��Ϊ�˱����û���Shift��
            }
            else
            {
                ts = DateTime.Now - m_dtPre;
                if (ts.Seconds < 1)
                {
                    m_strQuery += e.KeyChar.ToString().ToUpper();
                }
                else
                {
                    m_strQuery = e.KeyChar.ToString().ToUpper();
                    m_dtPre = DateTime.Now;
                }
            }

            m_mthGoToItem(m_strQuery);

            e.Handled = true;//���¼��Ѿ�����ϵͳ����������
        }

        //ListBox�¼�����
        public void m_lstTemplate_KeyDown(object p_objSender, KeyEventArgs e)
        {
            #region old
            //			if(e.KeyCode ==System.Windows.Forms.Keys.Escape )
            //			{
            //				this.m_lstTemplate.Visible =false;
            //				m_mthResetValiant();
            //				this.m_txtInputKeyword.SelectionStart = this.m_txtInputKeyword.TextLength ;
            //				this.m_txtInputKeyword.SelectionLength =0;
            //				this.m_txtInputKeyword.Focus ();
            //				return;
            //			}
            //
            //			if(this.m_lstTemplate.Items.Count <=0)return;
            //			
            //			#region ���̴������(Space��Enter����),��ӱԴ,2003-5-8 17:26:57
            //			if((e.KeyCode == System.Windows.Forms.Keys.Space || e.KeyCode ==System.Windows.Forms.Keys.Enter ) && this.m_lstTemplate.Visible )
            //			{
            //				int intIndex=this.m_lstTemplate.SelectedIndex ;
            //				if(intIndex>=0 && intIndex < this.m_lstTemplate.Items.Count )
            //				{
            //					switch(m_intSelectedLevel)
            //					{
            //						case 1:
            //							m_intSingleSetTemplate=intIndex;			//��װ,����ģ��
            //							break;
            //						case 2:
            //							m_intByTemplate =intIndex;					//ICD-10,�ؼ���,�˴�ϵͳ
            //							break;
            //						case 3:		//�ؼ�����һ��
            //							if(m_lstTemplate.Text.EndsWith("..."))
            //							{
            //								//һ���ؼ��ֶ�Ӧ�������
            //								m_strKeyWord = m_lstTemplate.Text.Replace("...","");
            //								break;
            //							}
            //							else
            //							{
            //								//һ���ؼ��ֶ�Ӧһ�����ƣ�ֱ��load��ģ���ֵ
            //								m_strKeyWord = m_lstTemplate.Text;
            //								if(m_txtInputKeyword.Text.StartsWith("mb.����ģ��"))
            //								{
            //									if(m_txtInputKeyword.Text.EndsWith("���ؼ���"))
            //										m_objTemplate_Detail=m_objDomain.lngGetSingleKeywordTemplates(1,m_strFormID,m_strControlID ,m_strKeyWord,MDIParent.OperatorID,MDIParent.s_ObjDepartment.m_StrDeptID);
            //									else
            //										m_objTemplate_Detail=m_objDomain.lngGetSingleKeywordTemplates(4,m_strFormID,m_strControlID ,m_strKeyWord,MDIParent.OperatorID,MDIParent.s_ObjDepartment.m_StrDeptID);
            //								}
            //								else
            //								{
            //									m_objTemplateSet_ID_Name=m_objDomain.lngGetSetTemplateKeywordName(m_strFormID,m_txtRichTextBox.Name,m_strKeyWord,MDIParent.OperatorID,MDIParent.s_ObjDepartment.m_StrDeptID);
            //								}
            //								m_intSelectContentIndex=0 ;			
            //								m_mthReplaceTemplateToTextBox();
            //								return;
            //							}
            //						case 4:			//ģ������һ�㣬�����һ��
            //							m_intSelectContentIndex=intIndex ;			//ѡ�е�ģ��ID
            //							m_mthReplaceTemplateToTextBox();
            //							return;
            //
            //					}
            //
            //					this.m_txtInputKeyword.Text +="." + this.m_lstTemplate.Text.Replace("...","") ;	//mb(.)
            //					this.m_txtInputKeyword.Text =this.m_txtInputKeyword.Text.Replace ("..",".");
            //					this.m_txtInputKeyword.SelectionStart =this.m_txtInputKeyword.Text.Length ;
            //					this.m_txtInputKeyword.SelectionLength =0;
            //					this.m_txtInputKeyword.Focus ();					//ת�ƽ����Ա��ȡ�µ�'.'��س�	
            //
            //					KeyEventArgs ee=new KeyEventArgs (System.Windows.Forms.Keys.Enter);
            //					m_mthKeywordText_KeyDown(this.m_txtInputKeyword ,ee);
            //				}
            //				return;
            //			}
            //			#endregion			
            //			
            //			return;
            #endregion
            if (e.KeyCode == Keys.Enter)
                m_lstTemplate_MouseDown(null, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
        }

        /// <summary>
        /// ListBox��ģ����ѯ��λ
        /// </summary>
        /// <param name="p_strStart"></param>
        private void m_mthGoToItem(string p_strStart)
        {
            if (m_lstTemplate.Items.Count > 0)
            {
                int intCur;
                if (p_strStart.Length > 1)//����û������λ�ַ����в�ѯ����SelectedIndex��ʼ����
                    intCur = m_lstTemplate.SelectedIndex;
                else//����û�ֻ����һλ�ַ����в�ѯ����SelectedIndex����һ����ʼ����
                    intCur = (m_lstTemplate.SelectedIndex + 1 >= m_lstTemplate.Items.Count ? 0 : (m_lstTemplate.SelectedIndex + 1));

                for (int i = intCur; i < m_lstTemplate.Items.Count; i++)
                {
                    if (((clsListBoxSearchItem)m_lstTemplate.Items[i]).m_StrPY.StartsWith(p_strStart))
                    {
                        m_lstTemplate.SelectedIndex = i;
                        return;
                    }
                }
                for (int i1 = 0; i1 < m_lstTemplate.SelectedIndex; i1++)
                {
                    if (((clsListBoxSearchItem)m_lstTemplate.Items[i1]).m_StrPY.StartsWith(p_strStart))
                    {
                        m_lstTemplate.SelectedIndex = i1;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// �õ����ֵ�ƴ������
        /// </summary>
        /// <param name="p_strChinese"></param>
        /// <returns></returns>
        private string m_strGetPYCode(string p_strChinese)
        {
            long tmp;
            string getpychar = "";
            for (int i = 0; i < p_strChinese.Length; i++)
            {
                if (Math.Abs(Strings.Asc(p_strChinese[i])) <= 256) getpychar += p_strChinese[i];
                else
                {
                    tmp = 65536 + Strings.Asc(p_strChinese[i]);
                    if (tmp >= 45217 && tmp <= 45252) getpychar += "A";
                    if (tmp >= 45253 && tmp <= 45760) getpychar += "B";
                    if (tmp >= 45761 && tmp <= 46317) getpychar += "C";
                    if (tmp >= 46318 && tmp <= 46825) getpychar += "D";
                    if (tmp >= 46826 && tmp <= 47009) getpychar += "E";
                    if (tmp >= 47010 && tmp <= 47296) getpychar += "F";
                    if (tmp >= 47297 && tmp <= 47613) getpychar += "G";
                    if (tmp >= 47614 && tmp <= 48118) getpychar += "H";
                    if (tmp >= 48119 && tmp <= 49061) getpychar += "J";
                    if (tmp >= 49062 && tmp <= 49323) getpychar += "K";
                    if (tmp >= 49324 && tmp <= 49895) getpychar += "L";
                    if (tmp >= 49896 && tmp <= 50370) getpychar += "M";
                    if (tmp >= 50371 && tmp <= 50613) getpychar += "N";
                    if (tmp >= 50614 && tmp <= 50621) getpychar += "O";
                    if (tmp >= 50622 && tmp <= 50905) getpychar += "P";
                    if (tmp >= 50906 && tmp <= 51386) getpychar += "Q";
                    if (tmp >= 51387 && tmp <= 51445) getpychar += "R";
                    if (tmp >= 51446 && tmp <= 52217) getpychar += "S";
                    if (tmp >= 52218 && tmp <= 52697) getpychar += "T";
                    if (tmp >= 52698 && tmp <= 52979) getpychar += "W";
                    if (tmp >= 52980 && tmp <= 53688) getpychar += "X";
                    if (tmp >= 53689 && tmp <= 54480) getpychar += "Y";
                    if (tmp >= 54481 && tmp <= 56289) getpychar += "Z";
                }
            }
            return getpychar.ToUpper();
        }

        /// <summary>
        /// ΪListBox�ϵ�ÿ��Item����ƴ����--������
        /// </summary>
        /// <param name="p_lst"></param>
        private void m_mthAddListBoxItemPYCode(ListBox p_lst)
        {
            if (p_lst.Items.Count > 0)
            {
                ArrayList arlTemp = new ArrayList();
                clsListBoxSearchItem[] objItemArr = new clsListBoxSearchItem[p_lst.Items.Count];
                for (int i = 0; i < p_lst.Items.Count; i++)
                {
                    string strName = p_lst.Items[i].ToString();
                    string strPY = m_strGetPYCode(strName);
                    clsListBoxSearchItem objItem = new clsListBoxSearchItem(strName, strPY);
                    arlTemp.Add(objItem);
                }
                objItemArr = (clsListBoxSearchItem[])arlTemp.ToArray(typeof(clsListBoxSearchItem));
                p_lst.Items.Clear();
                p_lst.Items.AddRange(objItemArr);
            }
        }

        #region ����ģ��ؼ���
        private void m_mthLoadTemplate()
        {
            #region �������ģ��
            if (m_intSingleSetTemplate == 1)			//��ģ��
            {
                switch (m_intByTemplate)
                {
                    case 0:						//�ؼ���
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_KeyWord = m_objDomain.lngGetSingleKeyword(1/*1Ϊ�ؼ��֣�4Ϊ����ֵ*/, m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);

                        if (m_objTemplate_KeyWord != null && m_objTemplate_KeyWord.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_KeyWord.Length; i++)
                            {
                                if (m_lstTemplate.Items.Count == 0)
                                    this.m_lstTemplate.Items.Add(m_objTemplate_KeyWord[i].m_strKeyword);
                                else if (m_objTemplate_KeyWord[i].m_strKeyword != m_lstTemplate.Items[m_lstTemplate.Items.Count - 1].ToString().Replace("...", ""))
                                {
                                    this.m_lstTemplate.Items.Add(m_objTemplate_KeyWord[i].m_strKeyword);
                                }
                                else
                                    m_lstTemplate.Items[m_lstTemplate.Items.Count - 1] = m_objTemplate_KeyWord[i].m_strKeyword + "...";

                            }
                        }
                        break;
                    case 1:						//ICD - 10
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_Detail = m_objDomain.lngGetSingleICD_10Templates(m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    case 2:						//�˴�ϵͳ
                        m_objTemplate_Detail = m_objDomain.lngGetSingleBio_SystemTemplates(m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                    case 3:						//����ֵ
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_KeyWord = m_objDomain.lngGetSingleKeyword(4/*1Ϊ�ؼ��֣�4Ϊ����ֵ*/, m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);

                        if (m_objTemplate_KeyWord != null && m_objTemplate_KeyWord.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_KeyWord.Length; i++)
                            {
                                if (m_lstTemplate.Items.Count == 0)
                                    this.m_lstTemplate.Items.Add(m_objTemplate_KeyWord[i].m_strKeyword);
                                else if (m_objTemplate_KeyWord[i].m_strKeyword != m_lstTemplate.Items[m_lstTemplate.Items.Count - 1].ToString().Replace("...", ""))
                                {
                                    this.m_lstTemplate.Items.Add(m_objTemplate_KeyWord[i].m_strKeyword);
                                }
                                else
                                    m_lstTemplate.Items[m_lstTemplate.Items.Count - 1] = m_objTemplate_KeyWord[i].m_strKeyword + "...";

                            }
                        }

                        //���ֻ��һ�����ֱ࣬�ӳ�ģ����
                        if (m_lstTemplate.Items.Count == 1)
                        {
                            m_strKeyWord = m_lstTemplate.Items[0].ToString().Replace("...", "");
                            m_lstTemplate.Items.Clear();
                            m_intSelectedLevel = 4;		//ListBoxλ�ڵڼ��㣬4Ϊ���һ��ģ����
                            m_mthLoadTemplateName();
                        }

                        break;

                }
            }
            else									//��װģ��
            {
                switch (m_intByTemplate)
                {
                    case 1:						//ICD-10
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateICD_10(m_strFormID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                        }
                        break;

                    case 0:					//�ؼ���
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_KeywordValue = m_objDomain.lngGetSetTemplateKeyword(m_strFormID, m_txtRichTextBox.Name, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
                        {
                            //��һ���ļ������������ｫ����������Ϊ�ļ��У���ǰ��ķ���������Ŀ¼
                            string strPreFolder = "";
                            for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                            {
                                int intIndexOfArrow = m_objTemplateSet_KeywordValue[i].m_strKeyword.IndexOf(">>");
                                if (intIndexOfArrow != -1)//���Ŀ¼
                                {
                                    if (m_objTemplateSet_KeywordValue[i].m_strKeyword.Substring(0, intIndexOfArrow) != strPreFolder)
                                    {
                                        this.m_lstTemplate.Items.Add(m_objTemplateSet_KeywordValue[i].m_strKeyword.Substring(0, intIndexOfArrow) + "...");
                                        strPreFolder = m_objTemplateSet_KeywordValue[i].m_strKeyword.Substring(0, intIndexOfArrow);
                                    }
                                }
                                else if (m_objTemplateSet_KeywordValue[i].m_strKeyword != strPreFolder)
                                {
                                    this.m_lstTemplate.Items.Add(m_objTemplateSet_KeywordValue[i].m_strKeyword + "...");
                                    strPreFolder = m_objTemplateSet_KeywordValue[i].m_strKeyword;
                                }

                                //								if(m_lstTemplate.Items.Count==0)
                                //									this.m_lstTemplate.Items.Add (m_objTemplateSet_KeywordValue[i].m_strKeyword);
                                //								else if(m_objTemplateSet_KeywordValue[i].m_strKeyword!=m_lstTemplate.Items[m_lstTemplate.Items.Count-1].ToString().Replace("...",""))
                                //								{
                                //									this.m_lstTemplate.Items.Add (m_objTemplateSet_KeywordValue[i].m_strKeyword);
                                //								}								
                                //								else
                                //									m_lstTemplate.Items[m_lstTemplate.Items.Count-1] = m_objTemplateSet_KeywordValue[i].m_strKeyword + "...";
                            }
                        }

                        //���ֻ��һ�����ֱ࣬�ӳ�ģ����
                        //						if(m_lstTemplate.Items.Count == 1 && m_lstTemplate.Items[0].ToString().EndsWith("..."))
                        //						{
                        //							m_strKeyWord = m_lstTemplate.Items[0].ToString().Replace("...","");
                        //							m_lstTemplate.Items.Clear();
                        //							m_intSelectedLevel = 4;		//ListBoxλ�ڵڼ��㣬4Ϊ���һ��ģ����
                        //							m_mthLoadTemplateName();
                        //						}


                        break;
                    case 2:					//�˴�ϵͳ
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateBio_System(m_strFormID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                        }
                        break;
                }
            }

            #endregion

            #region Ϊÿ��ģ��������ƴ����--������
            //			m_mthAddListBoxItemPYCode(this.m_lstTemplate);
            #endregion
        }
        #endregion

        #region ����ģ������
        private void m_mthLoadTemplateName()
        {
            if (m_intSingleSetTemplate == 1)			//��ģ��
            {
                switch (m_intByTemplate)
                {
                    case 0:						//�ؼ���
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_Detail = m_objDomain.lngGetSingleKeywordTemplates(1, m_strFormID, m_strControlID, m_strKeyWord, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                    case 1:						//ICD - 10
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_Detail = m_objDomain.lngGetSingleICD_10Templates(m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    case 2:						//�˴�ϵͳ
                        m_objTemplate_Detail = m_objDomain.lngGetSingleBio_SystemTemplates(m_strFormID, m_strControlID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                    case 3:						//����ֵ
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplate_Detail = m_objDomain.lngGetSingleKeywordTemplates(4, m_strFormID, m_strControlID, m_strKeyWord, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplate_Detail.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplate_Detail[i].m_strTemplate_Name);
                        }
                        break;
                }
            }
            else									//��װģ��
            {
                switch (m_intByTemplate)
                {
                    case 1:						//ICD-10
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateICD_10(m_strFormID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                        }
                        break;

                    case 0:					//�ؼ���
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, m_strKeyWord, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                        }
                        break;
                    case 2:					//�˴�ϵͳ
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateBio_System(m_strFormID, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
                        if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
                        {
                            for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                                this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                        }
                        break;

                }
            }

            #region Ϊÿ��ģ��������ƴ����--������
            //			m_mthAddListBoxItemPYCode(this.m_lstTemplate);
            #endregion
        }
        #endregion

        public void m_lstTemplateLeave_Leave(object sender, System.EventArgs e)
        {
            //			if(m_txtInputKeyword.Visible )
            //			{
            //				this.m_txtInputKeyword.SelectionStart =this.m_txtInputKeyword.TextLength ;
            //				this.m_txtInputKeyword.SelectionLength =0;
            //				this.m_txtInputKeyword.Focus ();
            //			}

            //			if(m_txtPreviewTemplate.Visible )
            //			{
            //				this.m_txtInputKeyword.Focus ();
            //			}

            if (m_txtPreviewTemplate.Visible)
            {
                m_txtPreviewTemplate.Focus();
            }
        }


        public static string s_strSetID = "";

        #region ʹ��ģ���滻�ı�,��ӱԴ,2003-5-9 20:36:28
        private void m_mthReplaceTemplateToTextBox()
        {
            if (m_intSingleSetTemplate == 1)			//��Ԫģ��
            {
                if (m_objTemplate_Detail != null && m_objTemplate_Detail.Length > m_intSelectContentIndex)
                {
                    switch (this.m_txtRichTextBox.GetType().FullName)
                    {
                        case "System.Windows.Forms.RichTextBox":
                            this.m_txtRichTextBox.Text = this.m_txtRichTextBox.Text.Replace("mb.", "");
                            this.m_txtRichTextBox.Text += m_objTemplate_Detail[m_intSelectContentIndex].m_strContent;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                            ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtRichTextBox).m_mthReplace("mb.", "");
                            int intLength = ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtRichTextBox).Text.Length;
                            ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtRichTextBox).m_mthInsertText(m_objTemplate_Detail[m_intSelectContentIndex].m_strContent, intLength);
                            break;
                        case "com.digitalwave.controls.ctlRichTextBox":
                            ((com.digitalwave.controls.ctlRichTextBox)m_txtRichTextBox).m_mthReplace("mb.", "");
                            int intLength1 = ((com.digitalwave.controls.ctlRichTextBox)m_txtRichTextBox).Text.Length;
                            ((com.digitalwave.controls.ctlRichTextBox)m_txtRichTextBox).m_mthInsertText(m_objTemplate_Detail[m_intSelectContentIndex].m_strContent, intLength1);
                            break;
                    }

                }
            }
            else
            {
                #region �ɵ�ģ�����ݣ�ֱ�Ӵ����ݿ��ȡ
                //				clsTemplatesetContentValue[] objTemplatesetContent;
                //				string strSetID="";
                //				if(m_objTemplateSet_ID_Name !=null && m_objTemplateSet_ID_Name.Length >m_intSelectContentIndex )
                //				{
                //					strSetID=m_objTemplateSet_ID_Name[m_intSelectContentIndex].m_strSet_ID;
                //				}
                //				if(strSetID=="")return;
                //				objTemplatesetContent =m_objDomain.lngGetAllTemplatesetContent (strSetID);
                #endregion

                #region �µ�ģ�����ݣ���Ԥ�����ȡ
                //				if(!m_txtPreviewTemplate.m_BlnIsSelected)
                //				{
                //					m_blnShowMessagebox = true;
                //					if(MDIParent.ShowQuestionMessageBox("ģ����������һЩδѡ���Ƿ������",MessageBoxButtons.YesNo)==DialogResult.No)
                //					{
                //						m_blnShowMessagebox = false;
                //						return;
                //					}
                //				}
                //				string strContent = m_txtPreviewTemplate.m_StrEditedText;
                //				int intIndex1 = 0;
                //				int intIndex2 = 0;
                //				for(int i=0;i<m_objTemplatesetContent.Length;i++)
                //				{
                //					intIndex1 = strContent.IndexOf(":",intIndex2);
                //					intIndex2 = strContent.IndexOf("         ",intIndex1);
                //					if(intIndex1!=-1 && intIndex2!=-1)
                //						m_objTemplatesetContent[i].m_strContent = strContent.Substring(intIndex1+3,intIndex2-intIndex1-3-1).TrimEnd();
                //				}
                #endregion

                //���̼�¼���⴦��
                if (m_frmForm.Name.Equals("frmGeneralDisease") && m_objTemplatesetContent != null && m_objTemplatesetContent.Length == 1)
                {
                    string strTemplateContent = m_objTemplatesetContent[0].m_strContent;
                    clsDataShareReplace.s_mthReplaceDataShareValue(MDIParent.s_ObjCurrentPatient, ref strTemplateContent, true);
                    int intIndex = m_txtRichTextBox.Text.IndexOf("�������");
                    if (intIndex > 0)
                        m_txtRichTextBox.Text = m_txtRichTextBox.Text.Substring(0, intIndex).TrimEnd() + "\r\n";
                    m_txtRichTextBox.Text += strTemplateContent;
                }
                else
                {
                    this.m_objTemplatesetParent.m_mthLoadTemplateset(m_objTemplatesetContent, -1, m_txtRichTextBox.GetHashCode());
                }

                if (m_frmForm.Name == "frmGeneralDisease" && m_txtRichTextBox.Name == "m_txtRecordContent")
                {
                    m_txtRichTextBox.Tag = new clsTemplateDomain().m_strGetAssociateIDBySetID(m_strSetID, (int)enmAssociate.Operation);
                }
            }
            //			m_mthResetValiant();
            //
            //			this.m_txtInputKeyword.Visible =false;
            //			//��Ϊ���ɼ�֮����Զ�������һ�ؼ�
            //			m_lstTemplate.TabIndex = (m_txtRichTextBox.TabIndex > 0) ? m_txtRichTextBox.TabIndex - 1 : 0;
            //			this.m_lstTemplate.Visible =false;
            //			this.m_txtRichTextBox.SelectionStart = this.m_txtRichTextBox.Text.Length ;
            //			this.m_txtRichTextBox.Focus ();
            //			this.m_txtPreviewTemplate.Visible = false;
        }
        #endregion

        #region ���У����ƣ�ճ��
        private void m_mthCut(object sender, EventArgs e)
        {
            ((PublicFunction)m_frmForm).Cut();
        }
        private void m_mthCopy(object sender, EventArgs e)
        {
            ((PublicFunction)m_frmForm).Copy();
        }
        private void m_mthPaste(object sender, EventArgs e)
        {
            ((PublicFunction)m_frmForm).Paste();
        }
        #endregion

        #region �µ�ģ�����
        /// <summary>
        /// �������ģ�����
        /// </summary>
        private void m_mthLoadAllTemplateKeyword()
        {
            int intSelectionStartIndex = this.m_txtRichTextBox.SelectionStart;
            System.Drawing.Point pt = this.m_txtRichTextBox.GetPositionFromCharIndex(intSelectionStartIndex);
            this.m_lstTemplate.Left = m_intLeftOfferset + pt.X;
            this.m_lstTemplate.Top = m_intTopOfferset + pt.Y + 20;

            this.m_lstTemplate.Items.Clear();

            m_objTemplateSet_KeywordValue = m_objDomain.lngGetSetTemplateKeyword(m_strFormID, m_txtRichTextBox.Name, MDIParent.OperatorID, MDIParent.s_ObjDepartment.m_StrDeptID);
            if (m_objTemplateSet_KeywordValue == null || m_objTemplateSet_KeywordValue.Length == 0)
            {
                MDIParent.ShowInformationMessageBox("�Բ��𣬸���Ŀû��ģ����ã����Ƚ���ģ�壡");
                return;
            }

            //��һ���ļ������������ｫ����������Ϊ�ļ��У���ǰ��ķ���������Ŀ¼
            string strPreFolder = "";
            for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
            {
                string strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;
                if (strKeyword.StartsWith("����ֵ--"))
                    continue;

                int intIndexOfArrow = strKeyword.IndexOf(">>");
                if (intIndexOfArrow != -1)//���Ŀ¼
                {
                    if (strKeyword.Substring(0, intIndexOfArrow) != strPreFolder)
                    {
                        this.m_lstTemplate.Items.Add(strKeyword.Substring(0, intIndexOfArrow) + "...");
                        strPreFolder = strKeyword.Substring(0, intIndexOfArrow);
                    }
                }
                else if (strKeyword != strPreFolder)
                {
                    this.m_lstTemplate.Items.Add(strKeyword + "...");
                    strPreFolder = strKeyword;
                }
            }

            m_mthAddListBoxItemPYCode(this.m_lstTemplate);

            if (this.m_lstTemplate.Items.Count > 0)
            {
                this.m_lstTemplate.Visible = true;
                this.m_lstTemplate.BringToFront();
                this.m_lstTemplate.SelectedIndex = 0;
                this.m_lstTemplate.Focus();
            }

        }

        /// <summary>
        /// ��ǰĿ¼
        /// </summary>
        private string m_strCurFolder = "";

        /// <summary>
        /// ����ĳ�����µķ������ģ����
        /// </summary>
        private void m_mthLoadSubKeywordOrTemplateName()
        {
            m_lstTemplate.Items.Clear();

            if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
            {
                //��һ���ļ������������ｫ����������Ϊ�ļ��У���ǰ��ķ���������Ŀ¼
                string strPreFolder = "";
                for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                {
                    string strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;
                    if (strKeyword == m_strCurFolder || strKeyword.IndexOf(m_strCurFolder + ">>") != -1)//���ڸ÷�����
                    {
                        int intIndexOfArrow = strKeyword.IndexOf(">>", m_strCurFolder.Length);
                        if (intIndexOfArrow != -1)//���Ŀ¼
                        {
                            int intNextArrow = strKeyword.IndexOf(">>", intIndexOfArrow + 2);
                            string strToAdd = "";
                            if (intNextArrow != -1)//������һ��Ŀ¼
                            {
                                strToAdd = strKeyword.Substring(intIndexOfArrow + 2, intNextArrow - intIndexOfArrow - 2);
                            }
                            else//û����һ��Ŀ¼
                            {
                                strToAdd = strKeyword.Substring(intIndexOfArrow + 2);
                            }
                            if (strToAdd != strPreFolder)
                            {
                                this.m_lstTemplate.Items.Add(strToAdd + "...");
                                strPreFolder = strToAdd;
                            }
                        }
                        else//��һ��Ŀ¼�������һ��
                        {
                            if (strKeyword != strPreFolder)
                            {
                                m_mthLoadAllTemplateName(strKeyword);
                                strPreFolder = strKeyword;
                            }
                        }
                    }
                }

            }

            m_mthAddListBoxItemPYCode(this.m_lstTemplate);

            if (m_lstTemplate.Items.Count > 0)
            {
                m_lstTemplate.SelectedIndex = 0;
            }

            if (m_lstTemplate.Items.Count == 1 && !m_lstTemplate.Items[0].ToString().EndsWith("..."))//ֻ��һ��ģ����ʱ��ֱ�Ӹ�ֵ
            {
                m_mthReplaceTemplateToTextBox();
            }
        }


        /// <summary>
        /// ����ĳ�����µķ������ģ����,���ص���һ���ַ�
        /// </summary>
        private string m_strLoadSubKeywordOrTemplateName()
        {
            StringBuilder sb = new StringBuilder();
            string strCurFolder = (m_strCurFolder == "") ? m_lstTemplate.SelectedItem.ToString().Replace("...", "") : m_strCurFolder + ">>" + m_lstTemplate.SelectedItem.ToString().Replace("...", "");

            if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
            {
                //��һ���ļ������������ｫ����������Ϊ�ļ��У���ǰ��ķ���������Ŀ¼
                string strPreFolder = "";
                for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                {
                    string strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;
                    if (strKeyword == strCurFolder || strKeyword.IndexOf(strCurFolder + ">>") != -1)//���ڸ÷�����
                    {
                        int intIndexOfArrow = strKeyword.IndexOf(">>", strCurFolder.Length);
                        if (intIndexOfArrow != -1)//���Ŀ¼
                        {
                            int intNextArrow = strKeyword.IndexOf(">>", intIndexOfArrow + 2);
                            string strToAdd = "";
                            if (intNextArrow != -1)//������һ��Ŀ¼
                            {
                                strToAdd = strKeyword.Substring(intIndexOfArrow + 2, intNextArrow - intIndexOfArrow - 2);
                            }
                            else//û����һ��Ŀ¼
                            {
                                strToAdd = strKeyword.Substring(intIndexOfArrow + 2);
                            }
                            if (strToAdd != strPreFolder)
                            {
                                //								this.m_lstTemplate.Items.Add (strToAdd + "...");
                                sb.Append(strToAdd);
                                sb.Append("...");
                                sb.Append("\r\n");
                                strPreFolder = strToAdd;
                            }
                        }
                        else//��һ��Ŀ¼�������һ��
                        {
                            if (strKeyword != strPreFolder)
                            {
                                sb.Append(m_strLoadAllTemplateName(m_objTemplateSet_KeywordValue[i].m_strKeyword));
                                strPreFolder = strKeyword;
                            }
                        }
                    }
                }

            }

            return sb.ToString();
        }

        /// <summary>
        /// ���ģ��ID�Ĺ�ϣ��
        /// </summary>
        private Hashtable m_htSet_ID = new Hashtable();
        /// <summary>
        /// ����ĳ�ؼ����µ�����ģ��
        /// </summary>
        /// <param name="p_strKeyword"></param>
        private void m_mthLoadAllTemplateName(string p_strKeyword)
        {
            m_htSet_ID.Clear();

            m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, p_strKeyword, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
            {
                for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                {
                    this.m_lstTemplate.Items.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                    m_htSet_ID.Add(i, m_objTemplateSet_ID_Name[i].m_strSet_ID);
                }
            }
        }

        /// <summary>
        /// ����ĳ�ؼ����µ�����ģ��,���ص���һ���ַ�
        /// </summary>
        /// <param name="p_strKeyword"></param>
        private string m_strLoadAllTemplateName(string p_strKeyword)
        {
            StringBuilder sb = new StringBuilder();

            m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, p_strKeyword, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
            {
                for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                {
                    //					this.m_lstTemplate.Items.Add (m_objTemplateSet_ID_Name[i]); 
                    sb.Append(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                    sb.Append("\r\n");
                }
            }

            return sb.ToString();
        }
        #endregion

        #region ���ü�������

        private void m_ctmLabCheckResult_Click(object sender, EventArgs e)
        {
            clsPatient objPatient = MDIParent.s_ObjCurrentPatient;
            if (clsEMRLogin.m_StrCurrentHospitalNO == "440104001")//��һ����
            {
                frmLabCheckReport frmlabcheckreport = new frmLabCheckReport();
                frmlabcheckreport.m_mthSetPatient(objPatient);
                frmlabcheckreport.m_mthInitLabCheckResult(m_txtRichTextBox);
                frmlabcheckreport.ShowDialog(m_frmForm);
                frmlabcheckreport.Close();
                frmlabcheckreport.Dispose();
            }
            else if (clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
            {
                if (objPatient != null)
                {
                    using (com.digitalwave.Emr.LisCheckResult.frmCheckResult frmResult = new com.digitalwave.Emr.LisCheckResult.frmCheckResult(objPatient.m_StrExtendPatientID, objPatient.m_StrName))
                    {
                        if (frmResult.ShowDialog(m_frmForm) == DialogResult.OK)
                            m_mthInertText(frmResult.m_StrResult);
                    }
                }
            }
            else//����
            {
                if (m_strPatientID != null && m_strPatientID != "")
                    objPatient = new clsPatient(m_strPatientID, "");

                if (objPatient == null)
                    return;
                iCare.DoctorWorkStation.frmLISResult objfrm = new iCare.DoctorWorkStation.frmLISResult();
                objfrm.SetPatientInfo = objPatient;
                if (objfrm.ShowDialog(m_frmForm) == DialogResult.OK)
                {
                    m_mthInertText(objfrm.Result);
                    objfrm.Close();
                    objfrm.Dispose();
                }
                //
                //			frmLabCheckReport frmlabcheckreport=new frmLabCheckReport();
                //			frmlabcheckreport.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
                //			frmlabcheckreport.m_mthInitLabCheckResult(m_txtRichTextBox);
                //			frmlabcheckreport.ShowDialog(m_frmForm);
            }
        }

        private void m_ctmCheckResult_Click(object sender, EventArgs e)
        {
            clsPatient objPatient = MDIParent.s_ObjCurrentPatient;
            if (clsEMRLogin.m_StrCurrentHospitalNO == "440104001")//��һ����
            {
                frmImageReport frm = new frmImageReport();
                frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
                //frm.ShowDialog(m_frmForm);
                frm.m_mthShowPACS();
            }
            else //�������ã��Ժ��������ã�
            {
                if (m_strPatientID != null && m_strPatientID != "")
                    objPatient = new clsPatient(m_strPatientID, "");
                if (objPatient == null)
                    return;

                try
                {
                    //string strRes = m_strGetPacsType();
                    //if (strRes != "" && strRes == "1")
                    //{
                    //    com.digitalwave.iCare.gui.PACS.frmPacsWSLeftNew pacs = new com.digitalwave.iCare.gui.PACS.frmPacsWSLeftNew();
                    //    pacs.m_mthShow(objPatient.m_StrInPatientID, m_ctlText);
                    //}
                    //else
                    //{

                    // 2019
                    //com.digitalwave.iCare.gui.PACS.frmPacsWSLeftNew pacs = new com.digitalwave.iCare.gui.PACS.frmPacsWSLeftNew();
                    //pacs.m_mthShow(objPatient.m_StrInPatientID, m_ctlText);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //			System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom("Pacs_GUI.dll");
                //			object obj = asm.CreateInstance("com.digitalwave.iCare.gui.PACS.frmPacsWSLeft");
                //			Type type = obj.GetType();
                //			System.Reflection.MethodInfo mi = type.GetMethod("m_mthShow");
                //			mi.Invoke(obj,new object[]{MDIParent.s_ObjCurrentPatient.m_StrInPatientID,m_txtRichTextBox});
            }
        }

        private string m_strGetPacsType()
        {
            string strRes = "";
            string XMLFile = Application.StartupPath + "\\" + "LoginFile.xml";
            try
            {
                if (System.IO.File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + "PACS")[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//UseSimpleForm[@key='AnyOne']");

                    if (xndP != null)
                    {
                        strRes = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            return strRes;
        }

        /// <summary>
        /// �ؼ�����ֵ
        /// </summary>
        /// <param name="p_ctlInput"></param>
        /// <param name="p_strValue"></param>
        public void m_mthInsertControlValue(Control p_ctlInput, string p_strValue)
        {
            switch (p_ctlInput.GetType().FullName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    com.digitalwave.Utility.Controls.ctlRichTextBox txt = (com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlInput;
                    txt.m_mthInsertText(p_strValue, txt.SelectionStart);
                    break;
                case "com.digitalwave.controls.ctlRichTextBox":
                    com.digitalwave.controls.ctlRichTextBox txt1 = (com.digitalwave.controls.ctlRichTextBox)p_ctlInput;
                    txt1.m_mthInsertText(p_strValue, txt1.SelectionStart);
                    break;
                case "System.Windows.Forms.RichTextBox":
                case "System.Windows.Forms.TextBox":
                    System.Windows.Forms.TextBoxBase txt2 = (System.Windows.Forms.TextBoxBase)p_ctlInput;
                    int intPreStart = txt2.SelectionStart;
                    txt2.Text = txt2.Text.Insert(txt2.SelectionStart, p_strValue);
                    txt2.SelectionStart = intPreStart + p_strValue.Length;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// ���±괦��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuperSubScript_Click(object sender, EventArgs e)
        {
            string strText = "";
            if (sender is MenuItem)
            {
                strText = ((MenuItem)sender).Text;
            }
            else if (sender is MenuCommand)
            {
                strText = ((MenuCommand)sender).Text;
            }
            else if (sender is ToolStripMenuItem)
            {
                strText = ((ToolStripMenuItem)sender).Text;
            }

            //com.digitalwave.Utility.Controls.ctlRichTextBox txtRTB = m_txtRichTextBox as com.digitalwave.Utility.Controls.ctlRichTextBox;
            //com.digitalwave.controls.ctlRichTextBox textBase = m_txtRichTextBox as com.digitalwave.controls.ctlRichTextBox;
            com.digitalwave.Utility.Controls.ctlRichTextBox txtRTB = m_ctlText as com.digitalwave.Utility.Controls.ctlRichTextBox;
            com.digitalwave.controls.ctlRichTextBox textBase = m_ctlText as com.digitalwave.controls.ctlRichTextBox;

            if (txtRTB != null)
            {
                if (strText == "�ϱ�")
                {
                    txtRTB.m_mthSetSelectionScript(0);
                }
                else if (strText == "�±�")
                {
                    txtRTB.m_mthSetSelectionScript(1);
                }
                else
                {
                    txtRTB.m_mthSetSelectionScript(2);
                }
            }
            else if (textBase != null)
            {
                if (strText == "�ϱ�")
                {
                    textBase.m_mthSetSelectionScript(0);
                }
                else if (strText == "�±�")
                {
                    textBase.m_mthSetSelectionScript(1);
                }
                else
                {
                    textBase.m_mthUndoSuperSubScript();
                }
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        private void m_mthInvokeSpecialSymbol(object sender, EventArgs e)
        {
            string strContent = "";
            try
            {
                using (iCare.AssitantTool.frmSpecialSymbolList frmSpecialSymbolList = new AssitantTool.frmSpecialSymbolList())
                {
                    frmSpecialSymbolList.ShowDialog();
                    strContent = frmSpecialSymbolList.m_StrOutputSpectialSymbol;
                    if (strContent == null)
                        strContent = "";
                }
            }
            catch
            {
                strContent = "";
            }

            m_mthInertText(strContent);
        }

        /// <summary>
        /// �ڵ�ǰ�ı������ֵ
        /// </summary>
        /// <param name="p_strText"></param>
        private void m_mthInertText(string p_strText)
        {
            //switch (m_txtRichTextBox.GetType().FullName) 
            //{
            //    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
            //        ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtRichTextBox).m_mthInsertText(p_strText, m_txtRichTextBox.SelectionStart);
            //        break;
            //    case "com.digitalwave.controls.ctlRichTextBox":
            //        ((com.digitalwave.controls.ctlRichTextBox)m_txtRichTextBox).m_mthInsertText(p_strText, m_txtRichTextBox.SelectionStart);
            //        break;
            //    default:
            //        int intPreStart = m_txtRichTextBox.SelectionStart;
            //        m_txtRichTextBox.Text = m_txtRichTextBox.Text.Insert(m_txtRichTextBox.SelectionStart, p_strText);
            //        m_txtRichTextBox.SelectionStart = intPreStart + p_strText.Length;
            //        break;
            //}
            switch (m_ctlText.GetType().FullName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    com.digitalwave.Utility.Controls.ctlRichTextBox rtb1 = m_ctlText as com.digitalwave.Utility.Controls.ctlRichTextBox;
                    rtb1.m_mthInsertText(p_strText, rtb1.SelectionStart);
                    break;
                case "com.digitalwave.controls.ctlRichTextBox":
                    com.digitalwave.controls.ctlRichTextBox rtb2 = m_ctlText as com.digitalwave.controls.ctlRichTextBox;
                    rtb2.m_mthInsertText(p_strText, rtb2.SelectionStart);
                    break;
                case "System.Windows.Forms.TextBox":
                    System.Windows.Forms.TextBox text1 = m_ctlText as TextBox;
                    int intPreStart1 = text1.SelectionStart;
                    text1.Text = text1.Text.Insert(text1.SelectionStart, p_strText);
                    text1.SelectionStart = intPreStart1 + p_strText.Length;
                    break;
                case "com.digitalwave.Utility.Controls.ctlComboBox":
                    com.digitalwave.Utility.Controls.ctlComboBox text2 = m_ctlText as com.digitalwave.Utility.Controls.ctlComboBox;
                    int intPreStart2 = text2.SelectionStart;
                    text2.Text = text2.Text.Insert(text2.SelectionStart, p_strText);
                    text2.SelectionStart = intPreStart2 + p_strText.Length;
                    break;
                case "System.Windows.Forms.ComboBox":
                    System.Windows.Forms.ComboBox text3 = m_ctlText as System.Windows.Forms.ComboBox;
                    int intPreStart3 = text3.SelectionStart;
                    text3.Text = text3.Text.Insert(text3.SelectionStart, p_strText);
                    text3.SelectionStart = intPreStart3 + p_strText.Length;
                    break;
            }
        }

        private void m_mthClearText()
        {
            switch (m_txtRichTextBox.GetType().FullName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtRichTextBox).m_mthResetContextInfo();
                    break;
                case "com.digitalwave.controls.ctlRichTextBox":
                    ((com.digitalwave.controls.ctlRichTextBox)m_txtRichTextBox).m_mthClearText();
                    break;
                default:
                    m_txtRichTextBox.Text = "";
                    break;
            }
        }

        #region �²�������Ӳ˵�����  HB 2004-08-10

        /// <summary>
        /// ���ר�Ʋ����˵�
        /// </summary>
        /// <param name="p_mniPopuoItem"></param>
        private void m_mthSetShareItem(MenuItem p_mniPopuoItem)
        {
            //			clsInpatMedRec_Type[] objTypeArr;
            ////			long lngRes = new clsInpatMedRecDomain().m_lngGetAllFormID(out objTypeArr);
            //			objTypeArr=MDIParent.s_ObjInpatMedRec_DataShare.m_objTypeArr;
            //			if(objTypeArr == null)
            //				return;
            //			m_objType_DeptArr=objTypeArr;
            //			p_mniPopuoItem.MenuItems.Add("-");
            //			for(int i =0; i <objTypeArr.Length; i++)
            //			{
            //				MenuItem mniChild = new MenuItem(objTypeArr[i].m_strTypeName);
            //				m_mthSetShareChildItem(mniChild);
            //				p_mniPopuoItem.MenuItems.Add(mniChild);
            //			}
        }

        //		/// <summary>
        //		/// ���ר�Ʋ����˵�
        //		/// </summary>
        //		/// <param name="p_mniPopuoItem"></param>
        //		private void m_mthSetShareItem2(MenuCommand p_mniPopuoItem)
        //		{
        //			clsInpatMedRec_Type[] objTypeArr;
        ////			//long lngRes = new clsInpatMedRecDomain().m_lngGetAllFormID(out objTypeArr);
        ////			objTypeArr=MDIParent.s_ObjInpatMedRec_DataShare.m_objTypeArr;
        ////			if(objTypeArr == null)
        ////				return;
        ////			m_objType_DeptArr=objTypeArr;
        ////			p_mniPopuoItem.MenuCommands.Add(new MenuCommand("-"));
        ////			for(int i =0; i <objTypeArr.Length; i++)
        ////			{
        ////				MenuCommand mniChild = new MenuCommand(objTypeArr[i].m_strTypeName);
        ////				m_mthSetShareChildItem2(mniChild);
        ////				p_mniPopuoItem.MenuCommands.Add(mniChild);
        ////			}
        //		}

        //		/// <summary>
        //		/// ���ר�Ʋ����Ӳ˵�
        //		/// </summary>
        //		/// <param name="p_mniCaseItem"></param>
        //		private void m_mthSetShareChildItem(MenuItem p_mniCaseItem)
        //		{
        //			clsInpatMedRecDomain objInpatMedRecDomain = new clsInpatMedRecDomain();
        //			clsInpatMedRec_Type_Item[] objType_ItemArr;
        //			//long lngRes = objInpatMedRecDomain.m_lngGetType_ItemRecord(m_strGetTypeID(p_mniCaseItem.Text),out objType_ItemArr);
        //			objType_ItemArr=MDIParent.s_ObjInpatMedRec_DataShare.m_objGetInpatMedRec_Type_Item(m_strGetTypeID(p_mniCaseItem.Text));
        ////			if(lngRes <= 0)
        ////				return;
        //			if ((objType_ItemArr!=null) && (objType_ItemArr.Length > 0))
        //			{
        //				for(int i =0; i< objType_ItemArr.Length; i++)
        //				{
        //					if(objType_ItemArr[i].m_strItemType =="ctlRichTextBox" || objType_ItemArr[i].m_strItemType =="RichTextBox" || objType_ItemArr[i].m_strItemType =="ctlComboBox")
        //					{
        //						m_mniAddMenuItem(objType_ItemArr[i],p_mniCaseItem,objType_ItemArr[i].m_strItemName);
        //					}
        //				}
        //			}
        //			
        //		}
        //
        //		/// <summary>
        //		/// ���ר�Ʋ����Ӳ˵�
        //		/// </summary>
        //		/// <param name="p_mniCaseItem"></param>
        //		private void m_mthSetShareChildItem2(MenuCommand p_mniCaseItem)
        //		{
        //			clsInpatMedRecDomain objInpatMedRecDomain = new clsInpatMedRecDomain();
        //			clsInpatMedRec_Type_Item[] objType_ItemArr;
        //			//long lngRes = objInpatMedRecDomain.m_lngGetType_ItemRecord(m_strGetTypeID(p_mniCaseItem.Text),out objType_ItemArr);
        //			objType_ItemArr=MDIParent.s_ObjInpatMedRec_DataShare.m_objGetInpatMedRec_Type_Item(m_strGetTypeID(p_mniCaseItem.Text));
        ////			if(lngRes <= 0)
        ////				return;
        //			if ( ( objType_ItemArr != null ) && (objType_ItemArr.Length > 0) )
        //			{
        //				for(int i =0; i< objType_ItemArr.Length; i++)
        //				{
        //					if(objType_ItemArr[i].m_strItemType =="ctlRichTextBox" || objType_ItemArr[i].m_strItemType =="RichTextBox" || objType_ItemArr[i].m_strItemType =="ctlComboBox")
        //					{
        //						m_mniAddMenuItem2(objType_ItemArr[i],p_mniCaseItem as MenuCommand,objType_ItemArr[i].m_strItemName);
        //					}
        //				}
        //			}
        //			
        //		}

        //		/// <summary>
        //		/// ���ݲ˵����Ʒ��ش���ID
        //		/// </summary>
        //		/// <param name="p_strFormName"></param>
        //		/// <returns></returns>
        //		private string m_strGetTypeID(string p_strFormName)
        //		{
        //			if(m_objType_DeptArr == null || m_objType_DeptArr.Length < 1 || p_strFormName == null)
        //				return null;
        //			for(int i=0;i < m_objType_DeptArr.Length; i++)
        //			{
        //				if(m_objType_DeptArr[i].m_strTypeName == p_strFormName)
        //				{
        //					return m_objType_DeptArr[i].m_strTypeID;
        //				}
        //			}
        //			return null;
        //		}
        /// <summary>
        //		/// ���ݴ���ID���ش�������
        //		/// </summary>
        //		/// <param name="p_strTypeID"></param>
        //		/// <returns></returns>
        //		private string m_strGetTypeName(string p_strTypeID)
        //		{
        //			if(m_objType_DeptArr == null || m_objType_DeptArr.Length < 1 || p_strTypeID == null)
        //				return null;
        //			for(int i=0;i < m_objType_DeptArr.Length; i++)
        //			{
        //				if(m_objType_DeptArr[i].m_strTypeID == p_strTypeID)
        //				{
        //					return m_objType_DeptArr[i].m_strTypeName;
        //				}
        //			}
        //			return null;
        //		}

        /// <summary>
        /// �ݹ���Ӳ˵���
        /// </summary>
        /// <param name="p_strItemText"></param>
        /// <param name="p_mniItem"></param>
        ////		private void m_mniAddMenuItem(clsInpatMedRec_Type_Item p_objContent,MenuItem p_mniItem,string p_strItemName)
        //		{
        //			MenuItem mniItem = null;
        //			int intIndex = p_strItemName.IndexOf(">>");
        //			if(intIndex > 0)
        //			{
        //				//������
        //				string strFirstItem = p_strItemName.Substring(0,intIndex);
        //				p_strItemName = p_strItemName.Substring(intIndex+2);
        //				if(p_mniItem.MenuItems.Count > 0)
        //				{
        //					foreach(MenuItem mni in p_mniItem.MenuItems)
        //					{
        //						if(mni.Text == strFirstItem)
        //						{
        //							mniItem = mni;
        //							break;
        //						}
        //					}
        //					if(mniItem != null)
        //					{
        //						//�Ӳ˵��Ѵ���
        //						m_mniAddMenuItem(p_objContent,mniItem,p_strItemName);
        //					}
        //					else
        //					{
        //						mniItem = new MenuItem(strFirstItem);
        //						p_mniItem.MenuItems.Add(mniItem);
        //						m_mniAddMenuItem(p_objContent,mniItem,p_strItemName);
        //					}
        //					return;
        //				}
        //				else
        //				{
        //					//�˵�����Ϊ��
        //					mniItem = new MenuItem(strFirstItem);
        //					p_mniItem.MenuItems.Add(mniItem);
        //					m_mniAddMenuItem(p_objContent,mniItem,p_strItemName);
        //				}
        //				return;
        //			}
        //			else
        //			{
        //				mniItem = new MenuItem(p_strItemName);
        //				m_hasMenuItems.Add(mniItem,p_objContent);
        //			}
        //			p_mniItem.MenuItems.Add(mniItem);
        //		}

        //		/// <summary>
        //		/// �ݹ���Ӳ˵���
        //		/// </summary>
        //		/// <param name="p_strItemText"></param>
        //		/// <param name="p_mniItem"></param>
        //		private void m_mniAddMenuItem2(clsInpatMedRec_Type_Item p_objContent,MenuCommand p_mniItem,string p_strItemName)
        //		{
        //			MenuCommand mniItem = null;
        //			int intIndex = p_strItemName.IndexOf(">>");
        //			if(intIndex > 0)
        //			{
        //				//������
        //				string strFirstItem = p_strItemName.Substring(0,intIndex);
        //				p_strItemName = p_strItemName.Substring(intIndex+2);
        //				if(p_mniItem.MenuCommands.Count > 0)
        //				{
        //					foreach(MenuCommand mni in p_mniItem.MenuCommands)
        //					{
        //						if(mni.Text == strFirstItem)
        //						{
        //							mniItem = mni;
        //							break;
        //						}
        //					}
        //					if(mniItem != null)
        //					{
        //						//�Ӳ˵��Ѵ���
        //						m_mniAddMenuItem2(p_objContent,mniItem,p_strItemName);
        //					}
        //					else
        //					{
        //						mniItem = new MenuCommand(strFirstItem);
        //						p_mniItem.MenuCommands.Add(mniItem);
        //						m_mniAddMenuItem2(p_objContent,mniItem,p_strItemName);
        //					}
        //					return;
        //				}
        //				else
        //				{
        //					//�˵�����Ϊ��
        //					mniItem = new MenuCommand(strFirstItem);
        //					p_mniItem.MenuCommands.Add(mniItem);
        //					m_mniAddMenuItem2(p_objContent,mniItem,p_strItemName);
        //				}
        //				return;
        //			}
        //			else
        //			{
        //				mniItem = new MenuCommand(p_strItemName);
        //				m_hasMenuItems.Add(mniItem,p_objContent);
        //			}
        //			p_mniItem.MenuCommands.Add(mniItem);
        //		}
        #endregion

        #region ���ͽṹ

        //����ڵ���Ϣ
        class clsNodeInfo
        {
            public static string c_strNodeType_Folder = "F";
            public static string c_strNodeType_Text = "T";

            public static int c_intOpenFolder = 0;
            public static int c_intCloseFolder = 1;
            public static int c_intFile = 2;

            public string m_strNodeType = "";
            public bool m_blnIsFolder
            {
                get
                {
                    return (this.m_strNodeType == c_strNodeType_Folder);
                }
            }

            public string m_strText = "";
            public string m_strXml = "<r></r>";
            public string m_strSetID = "";
            public string m_strSetName = "";
        }


        //���ظ��ڵ�, ����m_mthLoadAllTemplateKeyword
        public void m_mthLoadTemplateRootNode(TreeView tvwTemp)
        {
            //Load Node Data

            tvwTemp.Nodes.Clear();

            //m_objTemplateSet_KeywordValue = m_objDomain.lngGetSetTemplateKeyword(m_strFormID, m_txtRichTextBox.Name, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);//			
            m_objTemplateSet_KeywordValue = m_objDomain.lngGetSetTemplateKeyword(m_strFormID, m_ctlText.Name, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            if (m_objTemplateSet_KeywordValue == null || m_objTemplateSet_KeywordValue.Length == 0)
            {
                //				MDIParent.ShowInformationMessageBox("�Բ��𣬸���Ŀû��ģ����ã����Ƚ���ģ�壡");
                //				return;
            }

            if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
            {
                //filter the same Name
                string strPreFolder = "";
                for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                {
                    string strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;

                    if (strKeyword.StartsWith("����ֵ--")) continue;										//����ֵ

                    int intIndexOfArrow = strKeyword.IndexOf(">>");										//�༶Ŀ¼
                    if (intIndexOfArrow > 0) strKeyword = strKeyword.Substring(0, intIndexOfArrow);

                    if (strKeyword == strPreFolder) continue;												//��ǰһĿ¼��ͬ


                    clsNodeInfo objNode = new clsNodeInfo();
                    objNode.m_strNodeType = clsNodeInfo.c_strNodeType_Folder;

                    TreeNode tnNode = tvwTemp.Nodes.Add(strKeyword);
                    tnNode.ImageIndex = clsNodeInfo.c_intCloseFolder;
                    tnNode.SelectedImageIndex = clsNodeInfo.c_intOpenFolder;
                    tnNode.Tag = objNode;
                    tnNode.Nodes.Add("null");

                    strPreFolder = strKeyword;

                }
            }

            //PY Search
            //m_mthAddListBoxItemPYCode(this.m_lstTemplate);
            //			for(int i=0;i<tvwTemp.Nodes.Count;i++)
            //			{
            //				m_mthExpandTemplateNode(tvwTemp.Nodes[i]);
            //			}
        }


        private string m_strGetNodePath(TreeNode tnNode)
        {
            if (tnNode == null) return "";
            string strPath = tnNode.Text;

            TreeNode tnParent = tnNode.Parent;
            while (tnParent != null)
            {
                strPath = tnParent.Text + ">>" + strPath;
                tnParent = tnParent.Parent;
            }
            return strPath;
        }


        //չ���ڵ�, ����m_mthPreviewTemplate
        public void m_mthExpandTemplateNode(TreeNode tnNode)
        {
            if (tnNode == null) return;
            if (tnNode.Tag == null) return;
            if (m_arlExpandedNodes.Contains(tnNode))//Already load , Don't load again.
                return;
            else
                m_arlExpandedNodes.Add(tnNode);

            tnNode.Nodes.Clear();//����ٽڵ�

            clsNodeInfo objNode = (clsNodeInfo)(tnNode.Tag);

            string strCurFolder = m_strGetNodePath(tnNode);

            //Load sub floder
            if (m_objTemplateSet_KeywordValue != null && m_objTemplateSet_KeywordValue.Length > 0)
            {
                string strPreFolder = "";
                for (int i = 0; i < m_objTemplateSet_KeywordValue.Length; i++)
                {
                    string strKeyword = m_objTemplateSet_KeywordValue[i].m_strKeyword;

                    if (!strKeyword.StartsWith(strCurFolder + ">>")) continue;

                    //					if(strKeyword == strCurFolder) continue;

                    string strLeave = strKeyword.Substring(strCurFolder.Length + 2);

                    int intIndexOfArrow = strLeave.IndexOf(">>");
                    if (intIndexOfArrow > 0) strLeave = strLeave.Substring(0, intIndexOfArrow);

                    if (strLeave == strPreFolder) continue;

                    clsNodeInfo objNewInfo = new clsNodeInfo();
                    objNewInfo.m_strNodeType = clsNodeInfo.c_strNodeType_Folder;

                    TreeNode tnNewNode = tnNode.Nodes.Add(strLeave);
                    tnNewNode.ImageIndex = clsNodeInfo.c_intCloseFolder;
                    tnNewNode.SelectedImageIndex = clsNodeInfo.c_intOpenFolder;
                    tnNewNode.Tag = objNewInfo;
                    tnNewNode.Nodes.Add("null");//�ڵ�չ��ʱֻ����ҵ�ǰ�ڵ��µ�Ŀ¼������ٶ�

                    strPreFolder = strLeave;
                }
            }

            //���ҵ�ǰ�ؼ����µ�ģ��
            //m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_txtRichTextBox.Name, strCurFolder, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            m_objTemplateSet_ID_Name = m_objDomain.lngGetSetTemplateKeywordName(m_strFormID, m_ctlText.Name, strCurFolder, MDIParent.OperatorID, com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment.m_strDEPTID_CHR);
            if (m_objTemplateSet_ID_Name != null && m_objTemplateSet_ID_Name.Length > 0)
            {
                for (int i = 0; i < m_objTemplateSet_ID_Name.Length; i++)
                {
                    clsNodeInfo objNewInfo = new clsNodeInfo();
                    objNewInfo.m_strNodeType = clsNodeInfo.c_strNodeType_Text;
                    objNewInfo.m_strSetID = m_objTemplateSet_ID_Name[i].m_strSet_ID;
                    objNewInfo.m_strSetName = m_objTemplateSet_ID_Name[i].m_strSet_Name;

                    TreeNode tnNewNode = tnNode.Nodes.Add(m_objTemplateSet_ID_Name[i].m_strSet_Name);
                    tnNewNode.ImageIndex = clsNodeInfo.c_intFile;
                    tnNewNode.SelectedImageIndex = clsNodeInfo.c_intFile;
                    tnNewNode.Tag = objNewInfo;
                }
            }

        }


        //��ȡ�ڵ�����
        public void m_mthGetTemplateNodeContent(TreeNode tnNode, out string[] strTitle, out string[] strContent, out string[] strXml)
        {
            strTitle = null;
            strContent = null;
            strXml = null;

            if (tnNode == null) return;
            if (tnNode.Tag == null) return;
            clsNodeInfo objNode = (clsNodeInfo)(tnNode.Tag);
            if (objNode.m_blnIsFolder) return;

            #region ��ǰ�������пؼ�
            //			clsGUI_Info_DetailValue[] objControls = m_objDomain.lngGetAllControls(m_strFormID);
            //			Hashtable htControls = new Hashtable();
            //			for(int i=0;i<objControls.Length;i++)
            //				htControls.Add(objControls[i].m_strControl_ID,objControls[i].m_strControl_Desc);
            #endregion

            m_objTemplatesetContent = m_objDomain.lngGetAllTemplatesetContent(objNode.m_strSetID);
            if (m_objTemplatesetContent != null || m_objTemplatesetContent.Length > 0)
            {
                strTitle = new string[m_objTemplatesetContent.Length];
                strContent = new string[m_objTemplatesetContent.Length];
                strXml = new string[m_objTemplatesetContent.Length];
                for (int i = 0; i < m_objTemplatesetContent.Length; i++)
                {
                    strTitle[i] = m_objTemplatesetContent[i].m_strControl_Desc;

                    strContent[i] = m_objTemplatesetContent[i].m_strContent;

                    //					strXml[i] = m_objTemplatesetContent[i].m_strContentXml;
                    strXml[i] = "<root/>";
                }
            }


        }


        //ʹ�õ�ǰģ������
        public void m_mthSelectedTemplateNode(TreeNode tnNode)
        {
            if (m_frmForm is frmHRPBaseForm)
            {
                if (((frmHRPBaseForm)m_frmForm).m_BlnIsMark)
                {
                    MessageBox.Show(m_frmForm, "��ǰ�����ѳ����޸�ʱ�ޣ����ܸ���ģ�壡", "����");
                    return;
                }
            }
            if (tnNode == null) return;
            if (tnNode.Tag == null) return;
            clsNodeInfo objNode = (clsNodeInfo)(tnNode.Tag);
            if (objNode.m_blnIsFolder) return;

            m_mthReplaceTemplateToTextBox();

        }


        //����ؼ�λ��
        private Point m_ptGetControlPosition(Control ctlCur)
        {
            if (ctlCur == null) return new Point(0, 0);

            Point ptCur = ctlCur.Location;

            Control ctlParent = ctlCur.Parent;
            while (ctlParent != null)
            {
                ptCur.Offset(ctlParent.Location.X, ctlParent.Location.Y);
                if (ctlParent is Form) break;

                ctlParent = ctlParent.Parent;
            }
            return ptCur;
        }

        //��ȡ���λ��
        public Point m_ptGetCursorPosition()
        {
            //Get the position
            int intSelectionStartIndex = this.m_txtRichTextBox.SelectionStart;
            System.Drawing.Point pt = this.m_txtRichTextBox.GetPositionFromCharIndex(intSelectionStartIndex);

            //			Point ptLocation=new Point(m_intLeftOfferset + pt.X, m_intTopOfferset + pt.Y);
            //			Point ptLocation=new Point(32 + pt.X, 8 + pt.Y + 20);
            //
            //			Point ptPosition=m_ptGetControlPosition(this.m_txtRichTextBox);
            //
            //			ptPosition.Offset(ptLocation.X,ptLocation.Y);

            return pt;
        }

        #endregion

        #region Public
        /// <summary>
        /// ���õ�ǰҪ����ģ���RTB
        /// </summary>
        /// <param name="p_rtbInput"></param>
        public void m_mthSetCurRichTextBox(RichTextBox p_rtbInput)
        {
            this.m_txtRichTextBox = p_rtbInput;
            //�����Ԥ���ؼ��������¼���λ��
            if (m_txtRichTextBox != m_txtPreviewTemplate)
            {
                //				m_mthResetValiant();
                m_mthCalculatePosition(m_frmForm, m_txtRichTextBox);
                m_strControlID = m_hasContolIDs[m_txtRichTextBox].ToString();
            }
        }

        /// <summary>
        /// ����ģ��
        /// </summary>
        public void m_mthInvokeTemplate()
        {
            m_intPreSelectedIndex = -1;
            m_strCurFolder = "";
            //			m_mthLoadAllTemplateKeyword();
            new frmTemplateSelect(this).Show();
        }
        #endregion

        private void m_mniDoubleStrike_Click(object sender, EventArgs e)
        {
            //if (this.m_txtRichTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
            //    ((com.digitalwave.Utility.Controls.ctlRichTextBox)this.m_txtRichTextBox).m_mthSelectionDoubleStrikeThough(true);
            //else if (this.m_txtRichTextBox is com.digitalwave.controls.ctlRichTextBox)
            //    ((com.digitalwave.controls.ctlRichTextBox)this.m_txtRichTextBox).m_mthSelectionDoubleStrikeThough(true);
            if (this.m_ctlText is com.digitalwave.Utility.Controls.ctlRichTextBox)
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)this.m_ctlText).m_mthSelectionDoubleStrikeThough(true);
            else if (this.m_ctlText is com.digitalwave.controls.ctlRichTextBox)
                ((com.digitalwave.controls.ctlRichTextBox)this.m_ctlText).m_mthSelectionDoubleStrikeThough(true);
        }

        private void m_txtTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    m_mthSetPatientID(((Control)sender).FindForm());
                    m_mthLoadCommonUseTemplate(m_mncCommonUse);
                    m_mncCommonUse.Enabled = m_mncCommonUse.DropDownItems.Count > 0 ? true : false;
                    //m_ppmMenu.TrackPopup(m_txtRichTextBox.PointToScreen(new Point(e.X, e.Y)));
                    //m_ppmMenu.TrackPopup(m_ctlText.PointToScreen(new Point(e.X, e.Y)));
                    m_ppmMenu.Show(m_ctlText.PointToScreen(new Point(e.X, e.Y)));
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
        }
        private void m_mthSetPatientID(Form p_frmParent)
        {
            m_strPatientID = "";
            if (p_frmParent is frmHRPBaseForm)
            {
                m_strPatientID = ((frmHRPBaseForm)p_frmParent).m_StrPatientID;
            }
            else if (p_frmParent is frmApplyReportBase)
            {
                m_strPatientID = ((frmApplyReportBase)p_frmParent).m_StrPatientID;
            }
        }

        #region ͼ�Ĺ���վ(��)
        /// <summary>
        /// ͼ�Ĺ���վ�˵����
        /// </summary>
        /// <param name="p_mniDataShare"></param>
        //private void m_mthSetApplyReportSubMenu(MenuCommand p_mniDataShare)
        //{
        //    //m_mncApplyReport.MenuCommands
        //    string SQL = "";
        //    string strOldFormName = "";
        //    ArrayList altTemp = new ArrayList();

        //    System.Data.DataTable dtRecords = null;
        //    MenuCommand McdMenuCmd = null;

        //    SQL = "select AR_CONTROL_DESC.FORMCLSNAME,FORMDESC,CONTROLID,CONTROLDESC from AR_FORM,AR_CONTROL_DESC where AR_FORM.FORMCLSNAME=AR_CONTROL_DESC.FORMCLSNAME and FORMSTATUS=0 order by AR_CONTROL_DESC.FORMCLSNAME,CONTROLDESC";

        //    new com.digitalwave.ApplyReportServer.clsApplyReportServer().m_lngGetData(SQL, ref dtRecords);

        //    if (dtRecords != null)
        //    {
        //        for (int i = 0; i < dtRecords.Rows.Count; i++)
        //        {
        //            if (strOldFormName != dtRecords.Rows[i]["FORMCLSNAME"].ToString())
        //            {
        //                McdMenuCmd = null;
        //                McdMenuCmd = new MenuCommand(dtRecords.Rows[i]["FORMDESC"].ToString());
        //                strOldFormName = dtRecords.Rows[i]["FORMCLSNAME"].ToString();

        //                p_mniDataShare.MenuCommands.Add(McdMenuCmd);

        //                altTemp.Clear();
        //            }

        //            int intIndex = -1;
        //            string strTemp = dtRecords.Rows[i]["CONTROLDESC"].ToString();
        //            strTemp = strTemp.Replace(">>", ">");
        //            string[] strSplitArry = strTemp.Split(">".ToCharArray());

        //            for (int j = 0; j < strSplitArry.Length; j++)
        //            {
        //                bool blnIsExists = false;//Ĭ�ϵ�ǰҪ��ӵĽڵ㲻����
        //                for (int k = 0; k < altTemp.Count; k++)
        //                {
        //                    MenuCommand McdTemp = (MenuCommand)altTemp[k];
        //                    if (McdTemp.Text == strSplitArry[j] && j != strSplitArry.Length - 1)
        //                    {
        //                        blnIsExists = true;
        //                        break;
        //                    }
        //                    if (j != 0)
        //                    {
        //                        if (McdTemp.Text == strSplitArry[j - 1] && McdTemp.Tag == null)
        //                        {
        //                            intIndex = k;
        //                        }
        //                    }

        //                }

        //                MenuCommand McdAdd = new MenuCommand(strSplitArry[j]);
        //                if (j == strSplitArry.Length - 1)
        //                {
        //                    McdAdd.Tag = strOldFormName + "^" + dtRecords.Rows[i]["CONTROLID"].ToString();
        //                    McdAdd.Click += new EventHandler(ApplyReprotItems_Click);
        //                }

        //                if (!blnIsExists)
        //                {
        //                    if (intIndex == -1)
        //                        McdMenuCmd.MenuCommands.Add(McdAdd);
        //                    else
        //                        ((MenuCommand)altTemp[intIndex]).MenuCommands.Add(McdAdd);
        //                    altTemp.Add(McdAdd);
        //                }

        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// ͼ�Ĺ���վ�˵��¼�
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ApplyReprotItems_Click(object sender, EventArgs e)
        //{
        //    string SQL = "";
        //    string str = ((MenuCommand)sender).Tag.ToString();
        //    string[] strSplitArry = str.Split("^".ToCharArray());
        //    string strContent = "";

        //    System.Data.DataTable dtRecords = null;

        //    string strPatientID = m_strPatientID;
        //    if (strPatientID.Trim() == string.Empty && MDIParent.s_ObjCurrentPatient != null)
        //        strPatientID = MDIParent.s_ObjCurrentPatient.m_StrPatientID;
        //    SQL = "select CTL_CONTENT from AR_CONTENT where RECORDID=(select RECORDID from AR_APPLY_REPORT where PATIENTID='" + strPatientID + "' and OPENDATE=(select max(OPENDATE) from AR_APPLY_REPORT where DELSTATUS=0 and PATIENTID='" + strPatientID + "' and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and CONTROLID='" + strSplitArry[1].Replace("'", "''") + "'";
        //    new com.digitalwave.ApplyReportServer.clsApplyReportServer().m_lngGetData(SQL, ref dtRecords);

        //    if (dtRecords != null && dtRecords.Rows.Count > 0)
        //    {
        //        strContent = dtRecords.Rows[0][0].ToString();
        //    }

        //    m_mthInertText(strContent);
        //}
        #endregion ͼ�Ĺ���վ

        #region ͼ�Ĺ���վ�˵����(��)
        /// <summary>
        /// ͼ�Ĺ���վ�˵����
        /// </summary>
        /// <param name="p_mniDataShare"></param>
        public void m_mthSetApplyReportSubMenu(ToolStripMenuItem p_mniDataShare)
        {
            string SQL = "";
            string strOldFormName = "";
            List<ToolStripMenuItem> altTemp = new List<ToolStripMenuItem>();

            System.Data.DataTable dtRecords = null;
            ToolStripMenuItem McdMenuCmd = null;

            SQL = "select AR_CONTROL_DESC.FORMCLSNAME,FORMDESC,CONTROLID,CONTROLDESC from AR_FORM,AR_CONTROL_DESC where AR_FORM.FORMCLSNAME=AR_CONTROL_DESC.FORMCLSNAME and FORMSTATUS=0 order by AR_CONTROL_DESC.FORMCLSNAME,CONTROLDESC";

            //com.digitalwave.ApplyReportServer.clsApplyReportServer objServ =
            //        (com.digitalwave.ApplyReportServer.clsApplyReportServer)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.ApplyReportServer.clsApplyReportServer));

            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecords);

            if (dtRecords != null)
            {
                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    if (strOldFormName != dtRecords.Rows[i]["FORMCLSNAME"].ToString())
                    {
                        McdMenuCmd = null;
                        McdMenuCmd = new ToolStripMenuItem(dtRecords.Rows[i]["FORMDESC"].ToString());
                        strOldFormName = dtRecords.Rows[i]["FORMCLSNAME"].ToString();

                        p_mniDataShare.DropDownItems.Add(McdMenuCmd);

                        altTemp.Clear();
                    }

                    int intIndex = -1;
                    string strTemp = dtRecords.Rows[i]["CONTROLDESC"].ToString();
                    strTemp = strTemp.Replace(">>", ">");
                    string[] strSplitArry = strTemp.Split(">".ToCharArray());

                    for (int j = 0; j < strSplitArry.Length; j++)
                    {
                        bool blnIsExists = false;//Ĭ�ϵ�ǰҪ��ӵĽڵ㲻����
                        for (int k = 0; k < altTemp.Count; k++)
                        {
                            ToolStripMenuItem McdTemp = altTemp[k];
                            if (McdTemp.Text == strSplitArry[j] && j != strSplitArry.Length - 1)
                            {
                                blnIsExists = true;
                                break;
                            }
                            if (j != 0)
                            {
                                if (McdTemp.Text == strSplitArry[j - 1] && McdTemp.Tag == null)
                                {
                                    intIndex = k;
                                }
                            }

                        }

                        ToolStripMenuItem McdAdd = new ToolStripMenuItem(strSplitArry[j]);
                        if (j == strSplitArry.Length - 1)
                        {
                            McdAdd.Tag = strOldFormName + "^" + dtRecords.Rows[i]["CONTROLID"].ToString();
                            McdAdd.Click += new EventHandler(ApplyReprotItems_Click);
                        }

                        if (!blnIsExists)
                        {
                            if (intIndex == -1)
                                McdMenuCmd.DropDownItems.Add(McdAdd);
                            else
                                altTemp[intIndex].DropDownItems.Add(McdAdd);
                            altTemp.Add(McdAdd);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// ͼ�Ĺ���վ�˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyReprotItems_Click(object sender, EventArgs e)
        {
            string SQL = "";
            string str = ((ToolStripMenuItem)sender).Tag.ToString();
            string[] strSplitArry = str.Split("^".ToCharArray());
            string strContent = "";

            System.Data.DataTable dtRecords = null;

            string strPatientID = m_strPatientID;
            if (strPatientID.Trim() == string.Empty && MDIParent.s_ObjCurrentPatient != null)
                strPatientID = MDIParent.s_ObjCurrentPatient.m_StrPatientID;
            SQL = "select CTL_CONTENT from AR_CONTENT where RECORDID=(select RECORDID from AR_APPLY_REPORT where PATIENTID='" + strPatientID + "' and OPENDATE=(select max(OPENDATE) from AR_APPLY_REPORT where DELSTATUS=0 and PATIENTID='" + strPatientID + "' and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and CONTROLID='" + strSplitArry[1].Replace("'", "''") + "'";

            //com.digitalwave.ApplyReportServer.clsApplyReportServer objServ =
            //        (com.digitalwave.ApplyReportServer.clsApplyReportServer)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.ApplyReportServer.clsApplyReportServer));

            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecords);

            if (dtRecords != null && dtRecords.Rows.Count > 0)
            {
                strContent = dtRecords.Rows[0][0].ToString();
            }

            m_mthInertText(strContent);
        }
        #endregion

        #region ͼ�Ĺ���վ��ͨ�˵�
        private static string m_strPatientID = "";
        private static Control m_ctlAccept;
        public static void m_mthSetARDataShareCondition(/*MenuItem p_mniDataShare,*/string p_strPatientID, Control p_ctlAccept)
        {
            m_strPatientID = p_strPatientID;
            m_ctlAccept = p_ctlAccept;
            //			if(p_mniDataShare.MenuItems.Count <= 0)
            //				m_mthSetApplyReportSubMenu(p_mniDataShare);
        }
        /// <summary>
        /// ͼ�Ĺ���վ�˵����
        /// </summary>
        /// <param name="p_mniDataShare"></param>
        public void m_mthSetApplyReportSubMenu(MenuItem p_mniDataShare)
        {
            //m_mncApplyReport.MenuCommands
            string SQL = "";
            string strOldFormName = "";
            ArrayList altTemp = new ArrayList();

            System.Data.DataTable dtRecords = null;
            MenuItem McdMenuCmd = null;

            SQL = "select AR_CONTROL_DESC.FORMCLSNAME,FORMDESC,CONTROLID,CONTROLDESC from AR_FORM,AR_CONTROL_DESC where AR_FORM.FORMCLSNAME=AR_CONTROL_DESC.FORMCLSNAME and FORMSTATUS=0 order by AR_CONTROL_DESC.FORMCLSNAME,CONTROLDESC";


            //com.digitalwave.ApplyReportServer.clsApplyReportServer objServ =
            //        (com.digitalwave.ApplyReportServer.clsApplyReportServer)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.ApplyReportServer.clsApplyReportServer));

            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecords);

            if (dtRecords != null)
            {
                for (int i = 0; i < dtRecords.Rows.Count; i++)
                {
                    if (strOldFormName != dtRecords.Rows[i]["FORMCLSNAME"].ToString())
                    {
                        McdMenuCmd = null;
                        McdMenuCmd = new MenuItem(dtRecords.Rows[i]["FORMDESC"].ToString());
                        strOldFormName = dtRecords.Rows[i]["FORMCLSNAME"].ToString();

                        p_mniDataShare.MenuItems.Add(McdMenuCmd);

                        for (int intMenuCount = 0; intMenuCount < altTemp.Count; intMenuCount++)
                        {
                            ((MenuItem)altTemp[intMenuCount]).Visible = true;
                        }

                        altTemp.Clear();
                    }

                    int intIndex = -1;
                    string strTemp = dtRecords.Rows[i]["CONTROLDESC"].ToString();
                    strTemp = strTemp.Replace(">>", ">");
                    string[] strSplitArry = strTemp.Split(">".ToCharArray());

                    for (int j = 0; j < strSplitArry.Length; j++)
                    {
                        bool blnIsExists = false;//Ĭ�ϵ�ǰҪ��ӵĽڵ㲻����
                        for (int k = 0; k < altTemp.Count; k++)
                        {
                            MenuItem McdTemp = (MenuItem)altTemp[k];
                            if (McdTemp.Text == strSplitArry[j] && j != strSplitArry.Length - 1)
                            {
                                blnIsExists = true;
                                break;
                            }
                            if (j != 0)
                            {
                                if (McdTemp.Text == strSplitArry[j - 1] && McdTemp.Visible == false)
                                {
                                    intIndex = k;
                                }
                            }

                        }

                        MenuItem McdAdd = new MenuItem(strSplitArry[j]);
                        if (j == strSplitArry.Length - 1)
                        {
                            //McdAdd.Tag=strOldFormName + "^" +dtRecords.Rows[i]["CONTROLID"].ToString();
                            string strTag = strOldFormName + "^" + dtRecords.Rows[i]["CONTROLID"].ToString();
                            m_hasMenuItems.Add(McdAdd, strTag);
                            McdAdd.Click += new EventHandler(ApplyReprotItems_Click1);
                        }
                        else
                        {
                            McdAdd.Visible = false;
                        }

                        if (!blnIsExists)
                        {
                            if (intIndex == -1)
                                McdMenuCmd.MenuItems.Add(McdAdd);
                            else
                                ((MenuItem)altTemp[intIndex]).MenuItems.Add(McdAdd);
                            altTemp.Add(McdAdd);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// ͼ�Ĺ���վ�˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyReprotItems_Click1(object sender, EventArgs e)
        {
            string SQL = "";
            string str = m_hasMenuItems[(MenuItem)sender].ToString();
            string[] strSplitArry = str.Split("^".ToCharArray());
            string strContent = "";

            System.Data.DataTable dtRecords = null;

            SQL = "select CTL_CONTENT from AR_CONTENT where RECORDID=(select RECORDID from AR_APPLY_REPORT where PATIENTID='" + m_strPatientID + "' and OPENDATE=(select max(OPENDATE) from AR_APPLY_REPORT where DELSTATUS=0 and PATIENTID='" + m_strPatientID + "' and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and FORMCLSNAME='" + strSplitArry[0].Replace("'", "''") + "') and CONTROLID='" + strSplitArry[1].Replace("'", "''") + "'";

            //com.digitalwave.ApplyReportServer.clsApplyReportServer objServ =
            //        (com.digitalwave.ApplyReportServer.clsApplyReportServer)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.ApplyReportServer.clsApplyReportServer));

            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecords);

            if (dtRecords != null && dtRecords.Rows.Count > 0)
            {
                strContent = dtRecords.Rows[0][0].ToString();
            }

            if (m_ctlAccept != null)
            {
                if (m_ctlAccept is ComboBox)
                    m_ctlAccept.Text = m_ctlAccept.Text.Insert(((ComboBox)m_ctlAccept).SelectionStart, strContent);
                else if (m_ctlAccept is TextBoxBase)
                    m_ctlAccept.Text = m_ctlAccept.Text.Insert(((TextBoxBase)m_ctlAccept).SelectionStart, strContent);
            }
        }
        #endregion ͼ�Ĺ���վ

        /// <summary>
        /// ��ȡ�ɱ༭�ؼ�ѡ���ı���ʼ��
        /// </summary>
        /// <param name="p_ctlText">�ؼ�</param>
        /// <returns></returns>
        internal int m_intGetControlSelectionStart(Control p_ctlText)
        {
            int intSelectionStart = 0;
            if (p_ctlText != null)
            {
                intSelectionStart = p_ctlText.Text.Length;
                switch (p_ctlText.GetType().FullName)
                {
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        com.digitalwave.Utility.Controls.ctlRichTextBox rtb1 = p_ctlText as com.digitalwave.Utility.Controls.ctlRichTextBox;
                        intSelectionStart = rtb1.SelectionStart;
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        com.digitalwave.controls.ctlRichTextBox rtb2 = p_ctlText as com.digitalwave.controls.ctlRichTextBox;
                        intSelectionStart = rtb2.SelectionStart;
                        break;
                    case "System.Windows.Forms.TextBox":
                        System.Windows.Forms.TextBox text1 = p_ctlText as TextBox;
                        intSelectionStart = text1.SelectionStart;
                        break;
                    case "com.digitalwave.Utility.Controls.ctlComboBox":
                        com.digitalwave.Utility.Controls.ctlComboBox text2 = p_ctlText as com.digitalwave.Utility.Controls.ctlComboBox;
                        intSelectionStart = text2.SelectionStart;
                        break;
                    case "System.Windows.Forms.ComboBox":
                        System.Windows.Forms.ComboBox text3 = p_ctlText as System.Windows.Forms.ComboBox;
                        intSelectionStart = text3.SelectionStart;
                        break;
                }
            }
            return intSelectionStart;
        }

        /// <summary>
        /// �ͷ����б����������������Դ
        /// ע�⣺��ȷ������Ҳ����Ҫʹ�ô˶���
        /// </summary>
        public void Release()
        {
            //			if (this.Disposing || this.IsDisposed)
            //				return;
            if (m_arlExpandedNodes != null) m_arlExpandedNodes.Clear();
            if (m_hasMenuItems != null) m_hasMenuItems.Clear();
            if (m_ctmControl != null) m_ctmControl.Dispose();
            if (m_htSet_ID != null) m_htSet_ID.Clear();
            if (m_hasContolIDs != null) m_hasContolIDs.Clear();

            Type type = this.GetType();

            //ֻɨ�豾���ֶ�
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public |
                BindingFlags.NonPublic;//| BindingFlags.DeclaredOnly ;

            FieldInfo[] fields = type.GetFields(flags);

            foreach (FieldInfo field in fields)
            {
                object obj = field.GetValue(this);
                if (obj == null)
                    continue;

                Type t = obj.GetType();

                try
                {
                    //�ų������ÿյ�����
                    if (!t.IsValueType && (t.ToString().IndexOf("Native") < 0))
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
    }
}
