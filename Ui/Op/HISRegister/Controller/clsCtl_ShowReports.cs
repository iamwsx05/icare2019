using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.iCare.Chain;
using com.digitalwave.iCare.middletier.HI;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsCtl_ShowReports ��ժҪ˵����
    /// </summary>
    public class clsCtl_ShowReports : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// �����־,�������Ե绹���ĵ��...
        /// </summary>
        private int flag;
        /// <summary>
        /// ���汨��ID
        /// </summary>
        private string strReportID;
        /// <summary>
        /// �������ID,ֻ����LIS
        /// </summary>
        private string strGroupID;
        Dictionary<string, Image> dicBL = new Dictionary<string, Image>();
        private clsDcl_ShowReports objSvc = null;
        public clsCalcPatientCharge objCalPatientCharge = null;
        public clsCtl_ShowReports()
        {
            objSvc = new clsDcl_ShowReports();
            m_objAge = new clsBrithdayToAge();
            objCalPatientCharge = new clsCalcPatientCharge(string.Empty, "0001", 100, this.m_objComInfo.m_strGetHospitalTitle(), 1, 100);
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmShowReports m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmShowReports)frmMDI_Child_Base_in;
        }
        #endregion

        /// <summary>
        /// �Ƿ����ò�����
        /// </summary>
        bool IsUseBLReport = true; //false;

        #region �����ʼ������
        public void m_mthFormLoad()
        {

        }
        #endregion
        #region ���ؽڵ�
        public void m_mthLoadNodes(string ID)
        {
            try
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                this.m_objViewer.treeView1.Nodes.Clear();
                ArrayList objclsArr = new ArrayList();
                string cardNo = this.m_objViewer.txtCardID.Text.Trim();
                if (cardNo == string.Empty) return;
                //�Һ�
                clsParameter_VO objPV = new clsParameter_VO();
                objPV.strPatientID = ID;
                objPV.strCardID = cardNo;
                clsLIS objLis = new clsLIS();
                clsCARDIOGRAM objCARDIOGRAM = new clsCARDIOGRAM();//�ĵ�ͼ
                objLis.SubClassOBJ = objCARDIOGRAM;
                clsDCARDIOGRAM objDCARDIOGRAM = new clsDCARDIOGRAM();//��̬�ĵ�ͼ
                objCARDIOGRAM.SubClassOBJ = objDCARDIOGRAM;
                clsTCD objTCD = new clsTCD();
                objDCARDIOGRAM.SubClassOBJ = objTCD;
                clsEEG objEEG = new clsEEG();
                objTCD.SubClassOBJ = objEEG;
                //clsPacssReport objPacs = new clsPacssReport();
                //objEEG.SubClassOBJ = objPacs;
                clsSeeDoctor objSeeDoc = new clsSeeDoctor();
                objEEG.SubClassOBJ = objSeeDoc;
                //objPacs.SubClassOBJ = objSeeDoc;
                clsCaseHistory objCH = new clsCaseHistory();
                objSeeDoc.SubClassOBJ = objCH;
                clsOpIVDri objIVDri = new clsOpIVDri();
                objIVDri.m_mthGetMainData(objPV);

                //����������
                objclsArr.Add(objLis);
                objclsArr.Add(objCARDIOGRAM);
                objclsArr.Add(objDCARDIOGRAM);
                objclsArr.Add(objTCD);
                objclsArr.Add(objEEG);
                //objclsArr.Add(objPacs);
                objclsArr.Add(objSeeDoc);
                objclsArr.Add(objCH);
                objclsArr.Add(objIVDri);

                DataTable dtPacs = null;
                DataTable dtBL = null;
                if (Common.Entity.GlobalParm.IsChaShan)
                {
                    #region  ��ӵ��Ӳ���
                    clsInitEMRModule objEmrModule = new clsInitEMRModule();
                    objEmrModule.ObjParam = objPV;
                    objEmrModule.m_mthAddToNodes(ref objclsArr, objCH, objCH.UseFlag);
                    #endregion ��ӵ��Ӳ���

                    #region �����pacs
                    // ����Դ
                    clsDcl_ShowReports dcl = new clsDcl_ShowReports();
                    dtPacs = dcl.GetNewPacsView(cardNo);
                    #endregion

                    #region ����²���
                    if (this.IsUseBLReport)
                    {
                        string appId = dcl.GetNewBLAppId(cardNo);
                        if (!string.IsNullOrEmpty(appId))
                        {
                            dtBL = dcl.GetNewBLView(appId);
                        }
                    }
                    dcl = null;
                    #endregion
                }
                objLis.m_mthGetMainData(objPV);
                foreach (clsDataBase objTemp in objclsArr)
                {
                    TreeNode tn = new TreeNode(objTemp.ToString());
                    tn.Tag = objTemp;
                    clsMainData_VO[] objDMArr = objTemp.MainData;
                    if (objDMArr != null)
                    {
                        for (int i = 0; i < objDMArr.Length; i++)
                        {
                            TreeNode subTN = new TreeNode(objDMArr[i].strCreatDate);
                            subTN.Tag = objDMArr[i];
                            if (objDMArr[i].strRemark == "2" || objDMArr[i].strRemark == "3" || objDMArr[i].strRemark == "5")
                            {
                                subTN.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (objDMArr[i].strRemark == "6")
                            {
                                subTN.ForeColor = System.Drawing.Color.Orange;
                            }
                            tn.Nodes.Add(subTN);
                        }
                    }
                    else
                    {
                        if ((dtPacs == null || dtPacs.Rows.Count == 0) && (dtBL == null || dtBL.Rows.Count == 0))
                        {
                            TreeNode subTN = new TreeNode("�޼�¼");
                            subTN.ForeColor = System.Drawing.Color.Gray;
                            tn.Nodes.Add(subTN);
                        }
                    }
                    if (m_mthGetParentNode(tn))
                    {
                        this.m_objViewer.treeView1.Nodes.Add(tn);
                    }
                }

                #region ��PACS�ڵ�
                string groupName = string.Empty;
                TreeNode tnParent = null;
                TreeNode tnChild = null;
                clsMainData_VO dataVo = null;
                Dictionary<string, List<DataRow>> dicGroupName = new Dictionary<string, List<DataRow>>();
                if (dtPacs != null && dtPacs.Rows.Count > 0)
                {
                    DataView dvPacs = new DataView(dtPacs);
                    dvPacs.Sort = "bgsj desc";
                    DataTable dtPacs2 = dvPacs.ToTable();
                    foreach (DataRow dr in dtPacs2.Rows)
                    {
                        groupName = dr["jclx"].ToString();
                        if (groupName == null) groupName = string.Empty;
                        if (dicGroupName.ContainsKey(groupName))
                            dicGroupName[groupName].Add(dr);
                        else
                            dicGroupName.Add(groupName, new List<DataRow>() { dr });
                    }
                    foreach (string key in dicGroupName.Keys)
                    {
                        tnParent = new TreeNode(key == string.Empty ? "PACS" : key);
                        foreach (DataRow dr1 in dicGroupName[key])
                        {
                            if (dr1["bgsj"] == DBNull.Value)
                                tnChild = new TreeNode(dr1["jczt"].ToString());
                            else
                                tnChild = new TreeNode(Convert.ToDateTime(dr1["bgsj"]).ToString("yyyy-MM-dd HH:mm:ss"));
                            dataVo = new clsMainData_VO();
                            dataVo.strRemark = "pacs";
                            dataVo.strID = dr1["exid"].ToString();
                            dataVo.strName = dr1["bgdz"].ToString();
                            tnChild.Tag = dataVo;
                            tnParent.Nodes.Add(tnChild);
                        }
                        this.m_objViewer.treeView1.Nodes.Add(tnParent);
                    }
                }
                #endregion

                #region �²���ڵ�
                dicGroupName.Clear();
                dicBL = null;
                dicBL = new Dictionary<string, Image>();
                if (this.IsUseBLReport && dtBL != null && dtBL.Rows.Count > 0)
                {
                    groupName = "����";
                    DataView dvBl = new DataView(dtBL);
                    dvBl.Sort = "ExamTime desc";
                    DataTable dtBl2 = dvBl.ToTable();
                    foreach (DataRow dr in dtBl2.Rows)
                    {
                        if (dicGroupName.ContainsKey(groupName))
                            dicGroupName[groupName].Add(dr);
                        else
                            dicGroupName.Add(groupName, new List<DataRow>() { dr });
                    }
                    tnParent = new TreeNode("������");
                    foreach (DataRow dr1 in dicGroupName[groupName])
                    {
                        tnChild = new TreeNode(Convert.ToDateTime(dr1["ExamTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                        dataVo = new clsMainData_VO();
                        dataVo.strRemark = "����";
                        dataVo.strID = dr1["Applyid"].ToString();
                        if (dr1["ReportImage"] != DBNull.Value)
                        {
                            if (!dicBL.ContainsKey(dataVo.strID))
                            {
                                System.IO.MemoryStream objTempStream = new System.IO.MemoryStream((byte[])dr1["ReportImage"]);
                                dicBL.Add(dataVo.strID, System.Drawing.Image.FromStream(objTempStream));
                            }
                        }
                        tnChild.Tag = dataVo;
                        tnParent.Nodes.Add(tnChild);
                    }
                    this.m_objViewer.treeView1.Nodes.Add(tnParent);
                }
                #endregion
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }
        private bool m_mthGetParentNode(TreeNode tn)
        {
            clsDataBase Objtemp = tn.Tag as clsDataBase;
            if (Objtemp.ParentNodeName.Trim() == "")
            {
                return true;
            }
            for (int i = 0; i < this.m_objViewer.treeView1.Nodes.Count; i++)
            {
                if (this.m_objViewer.treeView1.Nodes[i].Text.Trim() == Objtemp.ParentNodeName.Trim())
                {
                    this.m_objViewer.treeView1.Nodes[i].Nodes.Add(tn);
                    return false;
                }
            }
            TreeNode TempNode = new TreeNode(Objtemp.ParentNodeName.Trim());
            TempNode.Nodes.Add(tn);
            this.m_objViewer.treeView1.Nodes.Add(TempNode);
            return false;

        }
        #endregion
        #region ˫��treeView
        private clsDataBase objMain = null;
        private clsMainData_VO objMD = null;
        public void m_mthDoubleClick()
        {
            try
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                this.m_objViewer.btOpenImage.Tag = null;
                if (this.m_objViewer.treeView1.SelectedNode.Parent == null && this.m_objViewer.treeView1.SelectedNode.Parent.Tag == null)
                {
                    return;
                }
                if (this.m_objViewer.treeView1.SelectedNode.Tag == null)
                {
                    return;
                }
                objMain = this.m_objViewer.treeView1.SelectedNode.Parent.Tag as clsDataBase;
                objMD = this.m_objViewer.treeView1.SelectedNode.Tag as clsMainData_VO;
                if (objMain is clsPacssReport)
                {
                    clsPacssReport objPacss = (clsPacssReport)objMain;
                    this.m_objViewer.btOpenImage.Tag = objPacss.objItem;
                    this.m_objViewer.printPreviewControl.Document = objPacss.m_mthGetPrintDocument(objMD);
                    return;
                }
                if (objMain is clsLIS)
                {
                    clsLIS objLis = (clsLIS)objMain;
                    objLis.Document = this.m_objViewer.printDocument1;
                    objLis.m_mthBeginPrint(objMD, null);
                }
                if (objMD != null && objMD.strRemark == "pacs" && !string.IsNullOrEmpty(objMD.strID))
                {
                    this.m_objViewer.webBrowser.Visible = true;
                    this.m_objViewer.webBrowser.Dock = DockStyle.Fill;
                    this.m_objViewer.printPreviewControl.Visible = false;
                    this.m_objViewer.fpnlBL.Visible = false;
                    if (string.IsNullOrEmpty(objMD.strName))
                    {
                        this.m_objViewer.webBrowser.Navigate(string.Empty);
                        this.m_objViewer.lblPacsHint.Visible = true;
                    }
                    else
                    {
                        this.m_objViewer.webBrowser.Navigate(objMD.strName);
                        this.m_objViewer.lblPacsHint.Visible = false;
                    }
                    this.m_objViewer.webBrowser.Tag = objMD.strID;
                }
                else if (objMD != null && objMD.strRemark == "����" && !string.IsNullOrEmpty(objMD.strID))
                {
                    if (dicBL.ContainsKey(objMD.strID))
                    {
                        this.m_objViewer.webBrowser.Visible = false;
                        this.m_objViewer.printPreviewControl.Visible = false;
                        this.m_objViewer.fpnlBL.Dock = DockStyle.Fill;
                        this.m_objViewer.fpnlBL.Visible = true;
                        Image img = dicBL[objMD.strID];
                        this.m_objViewer.picBL.Image = img;
                        this.m_objViewer.picBL.Width = img.Width;
                        this.m_objViewer.picBL.Height = img.Height;
                    }
                }
                else
                {
                    this.m_objViewer.printPreviewControl.Visible = true;
                    this.m_objViewer.printPreviewControl.Dock = DockStyle.Fill;
                    this.m_objViewer.webBrowser.Visible = false;
                    this.m_objViewer.lblPacsHint.Visible = false;
                    this.m_objViewer.fpnlBL.Visible = false;
                    this.m_objViewer.printPreviewControl.Document = this.m_objViewer.printDocument1;
                }
            }
            catch
            {
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }
        #endregion
        #region ��ӡ
        public void m_mthPrintPreView(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (objMain != null)
            {
                objMain.m_mthPrintPage(e);
            }

        }
        #endregion
        public void m_mthBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            if (objMain != null)
            {
                objMain.m_mthBeginPrint(objMD, this.m_objViewer.printDocument1);
            }

        }

        #region ���Ҳ�����Ϣ
        public void m_mthFindPatientInfo(int intflag, string strValue)
        {

            if (intflag == 2)
            {
                this.m_objViewer.listView1.Left = this.m_objViewer.txtName.Left + 8;
                this.m_objViewer.listView1.Top = this.m_objViewer.txtName.Top + this.m_objViewer.txtName.Height + 3;
            }
            else
            {
                this.m_objViewer.listView1.Left = this.m_objViewer.txtCardID.Left + 12;
                this.m_objViewer.listView1.Top = this.m_objViewer.txtCardID.Top + this.m_objViewer.txtName.Height + 15;
            }
            DataTable dt = null;
            long strRet = objSvc.m_mthFindPatientInfo(intflag, strValue, out dt);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                this.m_objViewer.listView1.Items.Clear();
                if (dt.Rows.Count == 1)
                {
                    m_mthFillData(dt.Rows[0]);
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["PATIENTCARDID_CHR"].ToString().Trim());
                        lv.SubItems.Add(dt.Rows[i]["FIRSTNAME_VCHR"].ToString().Trim());
                        lv.SubItems.Add(dt.Rows[i]["SEX_CHR"].ToString().Trim());
                        string strAge = m_objAge.m_strGetAge(dt.Rows[i]["BIRTH_DAT"]);
                        lv.SubItems.Add(strAge);
                        lv.SubItems.Add(dt.Rows[i]["PATIENTID_CHR"].ToString().Trim());
                        lv.Tag = dt.Rows[i];
                        this.m_objViewer.listView1.Items.Add(lv);
                    }
                    this.m_objViewer.listView1.Visible = true;
                    this.m_objViewer.listView1.BringToFront();
                    this.m_objViewer.listView1.Items[0].Selected = true;
                    this.m_objViewer.listView1.Select();
                }
            }
        }
        #endregion
        #region ˫���¼�
        public void m_mthListViewDoubleClick()
        {
            if (this.m_objViewer.listView1.SelectedItems.Count > 0)
            {
                m_mthFillData(this.m_objViewer.listView1.SelectedItems[0].Tag as DataRow);
                this.m_objViewer.listView1.Hide();

            }
        }
        /// <summary>
        /// ���˵绰
        /// </summary>
        private string strPatientTelNo = "";
        /// <summary>
        /// ����סַ
        /// </summary>
        private string strPatientAddress = "";
        /// <summary>
        /// ����֤��
        /// </summary>
        private string strPatientGovNo = "";
        /// <summary>
        /// ҽ����
        /// </summary>
        private string strPatientBXNo = "";
        /// <summary>
        /// 
        /// </summary>
        private clsBrithdayToAge m_objAge;
        private void m_mthFillData(DataRow dr)
        {
            this.m_objViewer.PatientID = dr["PATIENTID_CHR"].ToString().Trim();
            this.m_objViewer.PatientName = dr["FIRSTNAME_VCHR"].ToString().Trim();
            this.m_objViewer.PatientSex = dr["SEX_CHR"].ToString().Trim();
            this.m_objViewer.PatientAge = m_objAge.m_strGetAge(dr["BIRTH_DAT"]);
            this.m_objViewer.PatientCardID = dr["PATIENTCARDID_CHR"].ToString().Trim();
            this.strPatientTelNo = dr["HOMEPHONE_VCHR"].ToString().Trim();
            this.strPatientAddress = dr["HOMEADDRESS_VCHR"].ToString().Trim();
            this.strPatientGovNo = dr["GOVCARD_CHR"].ToString().Trim();
            this.strPatientBXNo = dr["DIFFICULTY_VCHR"].ToString().Trim();

        }
        #endregion
        #region ����סԺ�Ų��Ҳ���ID
        public void m_mthFindPatientIDByInHospitalNo(string strInHospitalNo)
        {
            string strPatient = objSvc.m_mthFindPatientIDByInHospitalNo(strInHospitalNo);
            if (strPatient.Trim() != "")
            {
                this.m_mthLoadNodes(strPatient.Trim());
            }
        }
        #endregion
        #region ��ͼ��
        public void m_mthOpenImage()
        {
            // ��PACS
            if (this.m_objViewer.webBrowser.Visible && this.m_objViewer.webBrowser.Tag != null)
            {
                System.Diagnostics.Process pro = new System.Diagnostics.Process();
                pro.StartInfo.FileName = Application.StartupPath + @"\PACSReport\ClinicalAccessTo.exe";
                pro.StartInfo.Arguments = this.m_objViewer.webBrowser.Tag.ToString();
                pro.Start();
            }
            return;
            //if (this.m_objViewer.btOpenImage.Tag == null)
            //{
            //    return;
            //}
            //clsImageReportPrintValue obj = (clsImageReportPrintValue)this.m_objViewer.btOpenImage.Tag;
            //clsPatientInfoQuery PQVO = new clsPatientInfoQuery();
            //PQVO.m_strPatientName = obj.m_strPatientName.Trim();
            //PQVO.m_strPatientSex = obj.m_strPatientSex.Trim();
            ////			PQVO.m_strDiagnoreNO=obj.m_strPatientNO;
            ////			PQVO.m_strInHospitalNo=obj.m_strInHospitalNO;frmPacsWSLeft
            //com.digitalwave.iCare.gui.PACS.frmPacsWSLeftNew objfrm = new frmPacsWSLeftNew();
            //objfrm.PatientInfoQuery = PQVO;
            //objfrm.StratupClassByScreenControl("1");
        }
        #endregion

        #region ����סԺ���ҵ����˵�ID��
        /// <summary>
        /// ����סԺ���ҵ����˵�ID��
        /// </summary>
        /// <param name="p_strInPatientNo"></param>
        /// <returns></returns>
        public string m_strGetPatientIDByInPatNo(string p_strInPatientNo)
        {
            string strPatCardNo = string.Empty;
            string strPatName = string.Empty;
            strPatCardNo = objSvc.m_strGetPatientIDByInNo(p_strInPatientNo, out strPatName);
            m_objViewer.txtName.Text = strPatName;
            return strPatCardNo;
        }
        #endregion
    }
    public class clsConvertToDecimal
    {
        public static decimal m_mthConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString().Trim() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

}
