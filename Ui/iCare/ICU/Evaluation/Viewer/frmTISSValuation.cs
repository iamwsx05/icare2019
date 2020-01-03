//#define FunctionPrivilege
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
//using iCare.Common;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// clin 10.15
    /// TISS-28评分
    /// 实现四大功能
    /// 数据库: 表TISSValuation
    /// ***************************
    /// 2004-10-27 修改由父类继承，并添加打印支持  －Hyphen
    /// ***************************
    /// </summary>
    public partial class frmTISSValuation : frmValuationBaseForm, PublicFunction
    {
        #region Define

        private System.Windows.Forms.CheckBox chkItem1;
        private System.Windows.Forms.CheckBox chkItem2;
        private System.Windows.Forms.CheckBox chkItem3;
        private System.Windows.Forms.CheckBox chkItem4;
        private System.Windows.Forms.CheckBox chkItem5;
        private System.Windows.Forms.CheckBox chkItem6;
        private System.Windows.Forms.CheckBox chkItem9;
        private System.Windows.Forms.CheckBox chkItem10;
        private System.Windows.Forms.CheckBox chkItem8;
        private System.Windows.Forms.CheckBox chkItem7;
        private System.Windows.Forms.CheckBox chkItem11;
        private System.Windows.Forms.CheckBox chkItem14;
        private System.Windows.Forms.CheckBox chkItem13;
        private System.Windows.Forms.CheckBox chkItem12;
        private System.Windows.Forms.CheckBox chkItem15;
        private System.Windows.Forms.CheckBox chkItem16;
        private System.Windows.Forms.CheckBox chkItem19;
        private System.Windows.Forms.CheckBox chkItem20;
        private System.Windows.Forms.CheckBox chkItem18;
        private System.Windows.Forms.CheckBox chkItem17;
        private System.Windows.Forms.CheckBox chkItem22;
        private System.Windows.Forms.CheckBox chkItem23;
        private System.Windows.Forms.CheckBox chkItem21;
        private System.Windows.Forms.CheckBox chkItem24;
        private System.Windows.Forms.CheckBox chkItem25;
        private System.Windows.Forms.CheckBox chkItem26;
        private System.Windows.Forms.CheckBox chkItem27;
        private System.Windows.Forms.CheckBox chkItem28;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label lblTitle10;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.TabControl tabPage;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblEvalDate;
        public com.digitalwave.Utility.Controls.ctlTimePicker dtpEvalDate;
        private com.digitalwave.Utility.Controls.ctlBorderTextBox txtEvalDoctor;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblMonth2;
        private System.Windows.Forms.Label lblDay2;
        private PinkieControls.ButtonXP m_cmdEvalDoctor;

        #region Constructor
        public frmTISSValuation()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            m_objGetDomain = new clsTISSValuationDomain();

            m_objBorderTool = new clsBorderTool(Color.White);
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{
                                                                             this.trvActivityTime
                                                                         });


            //击CheckBox计算评分结果
            this.chkItem1.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem2.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem3.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem4.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem5.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem6.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem7.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem8.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem9.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem10.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem11.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem12.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem13.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem14.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem15.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem16.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem17.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem18.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem19.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem20.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem21.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem22.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem23.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem24.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem25.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem26.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem27.Click += new System.EventHandler(m_mthCalculate);
            this.chkItem28.Click += new System.EventHandler(m_mthCalculate);

            txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

            m_mthSetQuickKeys();

            m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);
        }
        #endregion

        protected ctlHighLightFocus m_objHighLight;

        #region Member
        /// <summary>
        /// 自定义变量
        /// </summary>
        private clsTISSValuationDomain m_objGetDomain;

        private clsBorderTool m_objBorderTool;
        private int m_intSum = 0; //用于存当前选定项目值的结果(值的和)

        private clsTISSValuationInfo m_objTISSValuationInfo;

        private clsSystemContext m_objCurrentContext
        {
            get
            {
                return clsSystemContext.s_ObjCurrentContext;
            }
        }
        #endregion


        #region Public Function

        #region null impletement
        public void Display()
        {
        }
        public void Print()
        {
            m_lngPrint();
        }
        public void Display(string strcode, string strName)
        {
        }
        public void Copy()
        {
            m_lngCopy();
        }

        public void Cut()
        {
            m_lngCut();
        }

        public void Paste()
        {
            m_lngPaste();
        }

        public void Redo()
        {

        }

        public void Undo()
        {

        }
        public void Verify()
        {
            ////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
        }

        #endregion

        /// <summary>
        /// 保存记录(如果有原来的记录,则是修改,否则保存)
        /// </summary>
        public override void Save()
        {
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.frmTISSValuation,PrivilegeData.enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            m_lngSubSave();
        }
        public override long m_lngSubSave()
        {
            return m_lngSaveWithMessageBox();
        }
        /// <summary>
        /// 删除记录(通过住院号和评分时间)
        /// </summary>
        public override void Delete()
        {
#if FunctionPrivilege
			if(!MDIParent.s_blnCheckCurrentPrivilege(lbltxtSetionOffice.Tag.ToString(),PrivilegeData.enmPrivilegeSF.frmTISSValuation,PrivilegeData.enmPrivilegeOperation.Delete))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return;
			}			
#endif
            //if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
            //    == enmDBControlCheckResult.Disable)
            //{
            //    clsPublicFunction.s_mthShowNotPermitMessage();
            //    return;

            //}
            if (m_strInPatientID == null || m_strInPatientID == "")
            {
                clsPublicFunction.ShowInformationMessageBox("请输入病人住院号!");
                return;
            }
            if (m_strCreateDate == null || m_strCreateDate == "")
            {
                clsPublicFunction.ShowInformationMessageBox("请在列表中选择相应的评分时间！");
                return;
            }

            if (!clsPublicFunction.s_blnAskForDelete())
                return;

            long lngRes = m_objGetDomain.m_lngDeactive(clsBaseInfo.LoginEmployee.m_strEMPID_CHR, m_strInPatientID, m_strInPatientDate, m_strCreateDate);

            if (lngRes <= 0)
            {
                clsPublicFunction.ShowInformationMessageBox("删除失败，请重新操作!");
                return;
            }

            trvActivityTime.SelectedNode.Remove();

            this.trvActivityTime.SelectedNode = this.trvActivityTime.Nodes[0];
            ClearUp();
        }
        #endregion

        #region 计算评分
        /// <summary>
        /// 击CheckBox时计算所选定项目值的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthCalculate(object sender, System.EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                m_intSum = m_intSum + Convert.ToInt32(((CheckBox)sender).Tag.ToString());
            else
                m_intSum = m_intSum - Convert.ToInt32(((CheckBox)sender).Tag.ToString());

            this.lblResult.Text = Convert.ToString(m_intSum);
        }
        #endregion

        #region Display
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="p_objTISSInf">得到界面信息的对象</param>
        protected override void m_mthDisplay()
        {
            long lngRes = m_objGetDomain.m_lngGetTISSValue(m_strInPatientID, m_strInPatientDate, m_strCreateDate, out m_objTISSValuationInfo);

            if (m_objTISSValuationInfo == null)
                return;


            //基础项目
            this.chkItem1.Checked = bool.Parse(m_objTISSValuationInfo.blnItem1 == "1" ? "true" : "false");
            this.chkItem2.Checked = bool.Parse(m_objTISSValuationInfo.blnItem2 == "1" ? "true" : "false");
            this.chkItem3.Checked = bool.Parse(m_objTISSValuationInfo.blnItem3 == "1" ? "true" : "false");
            this.chkItem4.Checked = bool.Parse(m_objTISSValuationInfo.blnItem4 == "1" ? "true" : "false");
            this.chkItem5.Checked = bool.Parse(m_objTISSValuationInfo.blnItem5 == "1" ? "true" : "false");
            this.chkItem6.Checked = bool.Parse(m_objTISSValuationInfo.blnItem6 == "1" ? "true" : "false");
            this.chkItem7.Checked = bool.Parse(m_objTISSValuationInfo.blnItem7 == "1" ? "true" : "false");
            //通气支持
            this.chkItem8.Checked = bool.Parse(m_objTISSValuationInfo.blnItem8 == "1" ? "true" : "false");
            this.chkItem9.Checked = bool.Parse(m_objTISSValuationInfo.blnItem9 == "1" ? "true" : "false");
            this.chkItem10.Checked = bool.Parse(m_objTISSValuationInfo.blnItem10 == "1" ? "true" : "false");
            this.chkItem11.Checked = bool.Parse(m_objTISSValuationInfo.blnItem11 == "1" ? "true" : "false");
            //心血管支持
            this.chkItem12.Checked = bool.Parse(m_objTISSValuationInfo.blnItem12 == "1" ? "true" : "false");
            this.chkItem13.Checked = bool.Parse(m_objTISSValuationInfo.blnItem13 == "1" ? "true" : "false");
            this.chkItem14.Checked = bool.Parse(m_objTISSValuationInfo.blnItem14 == "1" ? "true" : "false");
            this.chkItem15.Checked = bool.Parse(m_objTISSValuationInfo.blnItem15 == "1" ? "true" : "false");
            this.chkItem16.Checked = bool.Parse(m_objTISSValuationInfo.blnItem16 == "1" ? "true" : "false");
            this.chkItem17.Checked = bool.Parse(m_objTISSValuationInfo.blnItem17 == "1" ? "true" : "false");
            this.chkItem18.Checked = bool.Parse(m_objTISSValuationInfo.blnItem18 == "1" ? "true" : "false");
            //肾脏支持
            this.chkItem19.Checked = bool.Parse(m_objTISSValuationInfo.blnItem19 == "1" ? "true" : "false");
            this.chkItem20.Checked = bool.Parse(m_objTISSValuationInfo.blnItem20 == "1" ? "true" : "false");
            this.chkItem21.Checked = bool.Parse(m_objTISSValuationInfo.blnItem21 == "1" ? "true" : "false");
            //神经系统支持
            this.chkItem22.Checked = bool.Parse(m_objTISSValuationInfo.blnItem22 == "1" ? "true" : "false");
            //代谢支持
            this.chkItem23.Checked = bool.Parse(m_objTISSValuationInfo.blnItem23 == "1" ? "true" : "false");
            this.chkItem24.Checked = bool.Parse(m_objTISSValuationInfo.blnItem24 == "1" ? "true" : "false");
            this.chkItem25.Checked = bool.Parse(m_objTISSValuationInfo.blnItem25 == "1" ? "true" : "false");
            //特殊干预措施
            this.chkItem26.Checked = bool.Parse(m_objTISSValuationInfo.blnItem26 == "1" ? "true" : "false");
            this.chkItem27.Checked = bool.Parse(m_objTISSValuationInfo.blnItem27 == "1" ? "true" : "false");
            this.chkItem28.Checked = bool.Parse(m_objTISSValuationInfo.blnItem28 == "1" ? "true" : "false");
            //结果
            this.lblResult.Text = m_objTISSValuationInfo.strResult;
            //把评分结果存放给m_intSum
            m_intSum = Convert.ToInt32(m_objTISSValuationInfo.strResult);

            dtpEvalDate.Value = DateTime.Parse(m_objTISSValuationInfo.strActivityTime);
            clsEmrEmployeeBase_VO objEmployee = null;
            clsBaseInfo.m_lngGetEmpByID(m_objTISSValuationInfo.strEvalDoctorID, out objEmployee);
            txtEvalDoctor.Text = objEmployee.m_strLASTNAME_VCHR;

        }
        #endregion

        #region 病人信息
        protected override void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID, string p_strPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strPatientID == null || p_strPatientID == "")
                return;

            this.trvActivityTime.Nodes[0].Nodes.Clear();

            DateTime[] m_dtmArr = m_objGetDomain.m_dtmGetTimeInfoOfAPatientArr(p_strPatientID, p_strPatientDate, p_strFromDate, p_strToDate);

            if (m_dtmArr != null)
            {
                for (int i = 0; i < m_dtmArr.Length; i++)
                {
                    string strDate = m_dtmArr[i].ToString("yyyy-MM-dd HH:mm:ss");
                    TreeNode trnDate = new TreeNode(strDate);
                    trnDate.Tag = m_dtmArr[i];
                    this.trvActivityTime.Nodes[0].Nodes.Add(trnDate);
                }
            }
            this.trvActivityTime.ExpandAll();
            trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];

            //			this.trvActivityTime_AfterSelect(this.trvActivityTime,new TreeViewEventArgs(trvActivityTime.Nodes[0]));
        }



        #endregion

        #region ClearUp

        protected override void ClearUp()
        {
            m_mthClearUp();
        }

        private void m_mthClearPatientInfo()
        {
            lblMonth.Text = "";
            lblDay.Text = "";

            m_strInPatientID = "";
            m_strInPatientDate = "";

        }

        /// <summary>
        /// 清除控件的属性值为空
        /// </summary>	
        private void m_mthClearUp()
        {
            try
            {
                //基础项目
                this.chkItem1.Checked = false;
                this.chkItem2.Checked = false;
                this.chkItem3.Checked = false;
                this.chkItem4.Checked = false;
                this.chkItem5.Checked = false;
                this.chkItem6.Checked = false;
                this.chkItem7.Checked = false;
                //通气支持
                this.chkItem8.Checked = false;
                this.chkItem9.Checked = false;
                this.chkItem10.Checked = false;
                this.chkItem11.Checked = false;
                //心血管支持
                this.chkItem12.Checked = false;
                this.chkItem13.Checked = false;
                this.chkItem14.Checked = false;
                this.chkItem15.Checked = false;
                this.chkItem16.Checked = false;
                this.chkItem17.Checked = false;
                this.chkItem18.Checked = false;
                //肾脏支持
                this.chkItem19.Checked = false;
                this.chkItem20.Checked = false;
                this.chkItem21.Checked = false;
                //神经系统支持
                this.chkItem22.Checked = false;
                //代谢支持
                this.chkItem23.Checked = false;
                this.chkItem24.Checked = false;
                this.chkItem25.Checked = false;
                //特殊干预措施
                this.chkItem26.Checked = false;
                this.chkItem27.Checked = false;
                this.chkItem28.Checked = false;
                //结果
                this.lblResult.Text = "0";

                m_intSum = 0;

                dtpEvalDate.Value = DateTime.Now;
                txtEvalDoctor.Text = clsBaseInfo.LoginEmployee.m_strLASTNAME_VCHR;

                m_strCreateDate = "";

            }
            catch { }
        }
        #endregion

        #region Save
        private long m_lngSaveWithMessageBox()
        {
            long lngRes = m_lngSaveWithoutMessageBox();
            if (lngRes == -11)
            {
                clsPublicFunction.ShowInformationMessageBox("你所修改的记录已被他人删除或不存在！");
            }
            else if (lngRes == -21)
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，保存失败！");
            }
            else if (lngRes == -31)
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，本记录已被他人修改，请重新读取一次！");
            }
            return lngRes;
        }

        private long m_lngSaveWithoutMessageBox()
        {
            if (m_strInPatientID == null || m_strInPatientID == "")
            {
                clsPublicFunction.ShowInformationMessageBox("对不起，请输入病人住院编号！");
                return 0;
            }

            string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            clsTISSValuationInfo objTISSValuationInfo = m_objGetInterfaceCtlVal();

            if (m_strCreateDate != "")
            {
                //if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;
                //}

                clsTISSValuationInfo objTemp;
                long lngExist = m_objGetDomain.m_lngGetTISSValue(m_strInPatientID, m_strInPatientDate, m_strCreateDate, out objTemp);

                if (lngExist == 0)
                    return -11;

                if (lngExist == 1)
                {
                    //if(DateTime.Parse(objTemp.strModifyDate) != DateTime.Parse(m_objTISSValuationInfo.strModifyDate))
                    //    return -31;

                    if (!clsPublicFunction.s_blnAskForModify())
                        return 0;
                }
            }
            else
            {
                //if(m_objCurrentContext.m_ObjControl.m_enmAddNewCheck(lbltxtSetionOffice.Tag.ToString(),this,enmFormState.NowUser)
                //    == enmDBControlCheckResult.Disable)
                //{
                //    clsPublicFunction.s_mthShowNotPermitMessage();
                //    return 0;

                //}
                clsTISSValuationInfo objTemp;
                long lngExist = m_objGetDomain.m_lngGetTISSValue(m_strInPatientID, m_strInPatientDate, dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), out objTemp);

                if (lngExist == 1)
                {
                    m_strCreateDate = dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    if (!clsPublicFunction.s_blnAskForModify())
                        return 0;
                }
                else
                {
                    m_strCreateDate = "";
                }
            }

            long lngRes = m_objGetDomain.m_lngSave(objTISSValuationInfo);
            if (lngRes <= 0)
            {
                return -21;
            }
            else
            {
                if (m_strCreateDate == "")
                {
                    TreeNode m_trnNewNode = new TreeNode(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    m_trnNewNode.Tag = DateTime.Parse(dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    trvActivityTime.Nodes[0].Nodes.Add(m_trnNewNode);
                    trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
                    trvActivityTime.SelectedNode = m_trnNewNode;
                }
                else
                {
                    TreeNode m_trnTempNode = trvActivityTime.SelectedNode;
                    trvActivityTime.SelectedNode = trvActivityTime.Nodes[0];
                    trvActivityTime.SelectedNode = m_trnTempNode;
                }
            }

            return 1;
        }

        /// <summary>
        /// 得到界面控件的属性值
        /// </summary>
        private clsTISSValuationInfo m_objGetInterfaceCtlVal()
        {
            clsTISSValuationInfo objTISSValuationInfo = new clsTISSValuationInfo();

            objTISSValuationInfo.strInHospitalNO = m_strInPatientID;
            objTISSValuationInfo.strInPatientDate = m_strInPatientDate;
            objTISSValuationInfo.strActivityTime = (m_strCreateDate == "") ? this.dtpEvalDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : m_strCreateDate;
            objTISSValuationInfo.strEvalDoctorID = clsBaseInfo.LoginEmployee.m_strEMPID_CHR;
            //基础项目
            objTISSValuationInfo.blnItem1 = (this.chkItem1.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem2 = (this.chkItem2.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem3 = (this.chkItem3.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem4 = (this.chkItem4.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem5 = (this.chkItem5.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem6 = (this.chkItem6.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem7 = (this.chkItem7.Checked) ? "1" : "0";
            //通气支持
            objTISSValuationInfo.blnItem8 = (this.chkItem8.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem9 = (this.chkItem9.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem10 = (this.chkItem10.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem11 = (this.chkItem11.Checked) ? "1" : "0";
            //心血管支持
            objTISSValuationInfo.blnItem12 = (this.chkItem12.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem13 = (this.chkItem13.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem14 = (this.chkItem14.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem15 = (this.chkItem15.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem16 = (this.chkItem16.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem17 = (this.chkItem17.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem18 = (this.chkItem18.Checked) ? "1" : "0";
            //肾脏支持
            objTISSValuationInfo.blnItem19 = (this.chkItem19.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem20 = (this.chkItem20.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem21 = (this.chkItem21.Checked) ? "1" : "0";
            //神经系统支持
            objTISSValuationInfo.blnItem22 = (this.chkItem22.Checked) ? "1" : "0";
            //代谢支持
            objTISSValuationInfo.blnItem23 = (this.chkItem23.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem24 = (this.chkItem24.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem25 = (this.chkItem25.Checked) ? "1" : "0";
            //特殊干预措施
            objTISSValuationInfo.blnItem26 = (this.chkItem26.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem27 = (this.chkItem27.Checked) ? "1" : "0";
            objTISSValuationInfo.blnItem28 = (this.chkItem28.Checked) ? "1" : "0";
            //评分结果
            objTISSValuationInfo.strResult = this.lblResult.Text;

            return objTISSValuationInfo;
        }
        #endregion

        private void frmTISSValuation_Load(object sender, System.EventArgs e)
        {
            m_objHighLight.m_mthAddControlInContainer(this);

            //this.dtpEvalDate.m_EnmVisibleFlag=MDIParent.s_ObjRecordDateTimeInfo.m_enmGetRecordTimeFlag(this.Name);
            this.dtpEvalDate.m_mthResetSize();

            //this.lsvCardNo.Visible=false;
            //lsvCardNo.GridLines=true;
            //this.lsvCardNo.Height=100;  
            //this.lsvCardNo.Columns.Add("就诊卡号",80,HorizontalAlignment.Center);
            //txtCardNo.Focus();
        }

        #region 添加键盘快捷键
        private void m_mthSetQuickKeys()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region 利用递归调用，读取并设置所有界面事件	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Lable" && strTypeName != "Button" && strTypeName != "CheckBox")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        string strSubTypeName = subcontrol.GetType().Name;
                        if (strSubTypeName != "Lable" && strSubTypeName != "Button" && strSubTypeName != "CheckBox")
                            m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }

        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {//F1 112  帮助, F2 113 Save，F3  114 Del，F4 115 Print，F5 116 Refresh，F6 117 Search
                case 13:// enter				
                    break;

                case 113://save
                         //this.txtCardNo.Focus();
                    this.Save();
                    break;
                case 114://del
                    this.Delete();
                    break;
                case 115://print
                    this.Print();
                    break;
                case 116://refresh
                    m_blnCanTextChange = false;
                    ClearUp();
                    m_mthClearPatientInfo();
                    this.trvActivityTime.Nodes[0].Nodes.Clear();
                    m_blnCanTextChange = true;
                    break;
                case 117://Search					
                    break;
            }
        }

        #endregion


        #region Copy,Cut,Paste
        /// <summary>
        /// 复制操作
        /// </summary>
        /// <returns>操作结果</returns>
        public long m_lngCopy()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
                {
                    case "ctlRichTextBox":
                        if (((ctlRichTextBox)ctlControl).Text != "")
                        {
                            ((ctlRichTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "RichTextBox":
                        if (((RichTextBox)ctlControl).Text != "")
                        {
                            ((RichTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "TextBox":
                        if (((TextBox)ctlControl).Text != "")
                        {
                            ((TextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "ctlBorderTextBox":
                        if (((ctlBorderTextBox)ctlControl).Text != "")
                        {
                            ((ctlBorderTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    case "DataGridTextBox":
                        if (((DataGridTextBox)ctlControl).Text != "")
                        {
                            ((DataGridTextBox)ctlControl).Copy();
                            return 1;
                        }
                        break;

                    default:
                        Clipboard.SetDataObject("");
                        break;
                }
            }

            return 0;
        }

        /// <summary>
        /// 剪切操作
        /// </summary>
        /// <returns>操作结果</returns>
        public long m_lngCut()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
                {
                    case "ctlRichTextBox":
                        if (((ctlRichTextBox)ctlControl).Text != "")
                        {
                            ((ctlRichTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "RichTextBox":
                        if (((RichTextBox)ctlControl).Text != "")
                        {
                            ((RichTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "TextBox":
                        if (((TextBox)ctlControl).Text != "")
                        {
                            ((TextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "ctlBorderTextBox":
                        if (((ctlBorderTextBox)ctlControl).Text != "")
                        {
                            ((ctlBorderTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;

                    case "DataGridTextBox":
                        if (((DataGridTextBox)ctlControl).Text != "")
                        {
                            ((DataGridTextBox)ctlControl).Cut();
                            return 1;
                        }
                        break;
                }
            }

            return 0;
        }

        /// <summary>
        /// 粘贴操作
        /// </summary>
        /// <returns>操作结果</returns>
        public long m_lngPaste()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().Name;

            if (strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            {
                switch (strTypeName)
                {
                    case "ctlRichTextBox":
                        ((ctlRichTextBox)ctlControl).Paste();
                        break;

                    case "RichTextBox":
                        ((RichTextBox)ctlControl).Paste();
                        break;

                    case "TextBox":
                        ((TextBox)ctlControl).Paste();
                        break;

                    case "ctlBorderTextBox":
                        ((ctlBorderTextBox)ctlControl).Paste();
                        break;

                    case "DataGridTextBox":
                        ((DataGridTextBox)ctlControl).Paste();
                        break;
                }
                return 1;
            }

            return 0;
        }
        #endregion

        #region Print Function

        public override void m_mthSetPrint()
        {
            clsTISSValuationInfo objValue;
            objPrintTool = new clsTISS_ValuationPrintTool();
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objCurrentPatient == null)
                objPrintTool.m_mthSetPrintInfo(null, null, DateTime.MinValue);
            else
            {
                if (this.trvActivityTime.SelectedNode == null || this.trvActivityTime.SelectedNode == trvActivityTime.Nodes[0] || trvActivityTime.SelectedNode.Tag == null)
                    objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient, null, DateTime.MinValue);
                else
                {
                    m_objGetDomain.m_lngGetTISSValue(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"), trvActivityTime.SelectedNode.Tag.ToString(), out objValue);
                    object obj = objValue;
                    objPrintTool.m_mthSetPrintInfo(m_objCurrentPatient, obj, DateTime.Parse(trvActivityTime.SelectedNode.Tag.ToString()));
                }
            }
            objPrintTool.m_mthInitPrintContent();
        }

        #endregion

    }
}
