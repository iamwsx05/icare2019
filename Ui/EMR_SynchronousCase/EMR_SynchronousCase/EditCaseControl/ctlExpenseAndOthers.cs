using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// 病人费用信息
    /// </summary>
    public partial class ctlExpenseAndOthers : UserControl, infSynchronousCaseControl
    {
        /// <summary>
        /// 病人费用信息
        /// </summary>
        public ctlExpenseAndOthers()
        {
            InitializeComponent();
        }

      
        #region infSynchronousCaseControl 成员
        /// <summary>
        /// 是否已初始化
        /// </summary>
        private bool m_blnHasInit = false;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        public bool m_BlnHasInit
        {
            get
            {
                return m_blnHasInit;
            }
            set
            {
                m_blnHasInit = value;
            }
        }

        #region 初始化病案内容
        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_dsContent">数据库中获取的已保存数据</param>
        public void m_mthInitCase(DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                return;
            }

            if (p_dsContent.Tables.Contains("HIS_BA1") && p_dsContent.Tables["HIS_BA1"].Rows.Count > 0)
            {
                DataRow drBA1 = p_dsContent.Tables["HIS_BA1"].Rows[0];
                m_txtTotalAMT.Text = drBA1["FSUM1"].ToString();
                m_txtNurseAMT.Text = drBA1["FHLF"].ToString();
                m_txtWMAMT.Text = drBA1["FXYF"].ToString();
                m_txtCMFinishedAMT.Text = drBA1["FZCHYF"].ToString();
                m_txtCMSemifinishedAMT.Text = drBA1["FZCYF"].ToString();
                m_txtBloodAMT.Text = drBA1["FSXF"].ToString();
                m_txtOperationAMT.Text = drBA1["FSSF"].ToString();
                m_txtAnaethesiaAMT.Text = drBA1["FMZF"].ToString();
                m_txtOtherAMT.Text = drBA1["FQTF"].ToString();
                //新增费用信息
                m_txtTotalAMT.Text = drBA1["FSUM1"].ToString();
                m_txtSelfAMT.Text = drBA1["FZFJE"].ToString();
                txtYbylfwfAmt.Text = drBA1["FZHFWLYLF"].ToString();
                txtYbylczfAmt.Text = drBA1["FZHFWLCZF"].ToString();
                m_txtNurseAMT.Text = drBA1["FZHFWLHLF"].ToString();
                txtQtfyAmt.Text = drBA1["FZHFWLQTF"].ToString();
                txtBlzdfAmt.Text = drBA1["FZDLBLF"].ToString();
                txtSyszdfAmt.Text = drBA1["FZDLSSSF"].ToString();
                txtYxxzdfAmt.Text = drBA1["FZDLYXF"].ToString();
                txtLczdxmfAmt.Text = drBA1["FZDLLCF"].ToString();
                txtFsszlxmAmt.Text = drBA1["FZLLFFSSF"].ToString();
                txtLcwlzlfAmt.Text = drBA1["FZLLFWLZWLF"].ToString();
                txtSszlamt.Text = drBA1["FZLLFSSF"].ToString();
                m_txtAnaethesiaAMT.Text = drBA1["FZLLFMZF"].ToString();
                m_txtOperationAMT.Text = drBA1["FZLLFSSZLF"].ToString();
                txtKffAmt.Text = drBA1["FKFLKFF"].ToString();
                txtZyzlfAmt.Text = drBA1["FZYLZF"].ToString();
                m_txtWMAMT.Text = drBA1["FXYF"].ToString();
                txtKjyfyAmt.Text = drBA1["FXYLGJF"].ToString();
                m_txtCMFinishedAMT.Text = drBA1["FZCHYF"].ToString();
                m_txtCMSemifinishedAMT.Text = drBA1["FZCYF"].ToString();
                m_txtBloodAMT.Text = drBA1["FXYLXF"].ToString();
                txtBdblzpfAmt.Text = drBA1["FXYLBQBF"].ToString();
                txtQdblzpfAmt.Text = drBA1["FXYLQDBF"].ToString();
                txtNxyzzpfAmt.Text = drBA1["FXYLYXYZF"].ToString();
                textBox19.Text = drBA1["FXYLXBYZF"].ToString();
                txtJcyycxyyclfAmt.Text = drBA1["FHCLCJF"].ToString();
                txtZlyycxyyclfAmt.Text = drBA1["FHCLZLF"].ToString();
                txtSsyycxyyclfAmt.Text = drBA1["FHCLSSF"].ToString();
                m_txtOtherAMT.Text = drBA1["FQTF"].ToString();
                if (drBA1["FBODYBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboCorpseCheck.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboCorpseCheck.Items[iI]).m_strCode == drBA1["FBODYBH"].ToString())
                        {
                            m_cboCorpseCheck.SelectedIndex = iI;
                            break;
                        }
                    }
                }
               
                if (drBA1["FBLOODBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboBloodType.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboBloodType.Items[iI]).m_strCode == drBA1["FBLOODBH"].ToString())
                        {
                            m_cboBloodType.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FRHBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboRh.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboRh.Items[iI]).m_strCode == drBA1["FRHBH"].ToString())
                        {
                            m_cboRh.SelectedIndex = iI;
                            break;
                        }
                    }
                }
               
            }
            m_blnHasInit = true;
        }

        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strPatientID">病人ID</param>
        public void m_mthInitCase(string p_strRegisterID, string p_strPatientID)
        {
            //费用
            clsEMR_SynchronousCaseDomain objDomainExpense = new clsEMR_SynchronousCaseDomain();
            clsInHospitalMainCharge[] objChargeArr = null;
            clsInHospitalMainRecord_Content p_objRecordcontent;
            //long lngRes = objDomainExpense.m_lngGetCHRCATE(p_strRegisterID, p_strPatientID, out objChargeArr);
            
            DataTable m_strBBRegisterID = null;
            long lngRes = 0;
            //入院时间大于更新时间，采用新版获取费用方式否则手填
            m_strBBRegisterID = objDomainExpense.m_lngGetRgisterIDByInpatientID(p_strRegisterID);
            //if (m_strBBRegisterID.Rows.Count < 1)         // 2017-07-05 产科只传妈妈费用，不再连带BB费用
                lngRes = objDomainExpense.m_lngGetCHRCATE(p_strRegisterID, p_strPatientID, out objChargeArr);
            //else
            //    lngRes = objDomainExpense.m_lngGetChargeChanKe(p_strRegisterID, m_strBBRegisterID, out objChargeArr);
            
            lngRes = objDomainExpense.m_lngGetSelfPay(p_strRegisterID, out p_objRecordcontent);
            objDomainExpense = null;
            if (objChargeArr == null || objChargeArr.Length <= 0)
            {
                m_txtTotalAMT.Text = "0.00";
            }
            else
            {
                double dblSum = 0D;
                double sumMoney = 0;
                initTxtAmt();
                for (int iC = 0; iC < objChargeArr.Length; iC++)
                {
                    m_mthSetMoneyToValue(objChargeArr[iC].m_dblMoney, objChargeArr[iC].m_strTypeName, ref dblSum);
                    sumMoney += objChargeArr[iC].m_dblMoney;
                }
                m_txtTotalAMT.Text = sumMoney.ToString(); //dblSum.ToString();
                m_txtSelfAMT.Text = p_objRecordcontent.m_strSelfamt.ToString();
            }

            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            DataTable dtbResult = null;
            lngRes = objDomain.m_lngGetOthersCaseInfo(p_strRegisterID, out dtbResult);
            if (dtbResult != null && dtbResult.Rows.Count > 0)
            {
                DataRow drInfo = dtbResult.Rows[0];
                int intTemp = 0;
                if (int.TryParse(drInfo["CORPSECHECK"].ToString(), out intTemp))
                {
                    if (intTemp == 0)
                    {
                        m_cboCorpseCheck.SelectedIndex = 1;
                    }
                    else if (intTemp == 1)
                    {
                        m_cboCorpseCheck.SelectedIndex = 0;
                    }                    
                }
               
                if (int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["BLOODTYPE"].ToString(), drInfo["BLOODTYPEXML"].ToString()), out intTemp))
                {
                    m_cboBloodType.SelectedIndex = intTemp-1;
                }
                if (int.TryParse(drInfo["BLOODRH"].ToString(), out intTemp))
                {
                    m_cboRh.SelectedIndex = intTemp-1;
                }
               
            }
            m_blnHasInit = true;
            objDomain = null;
        }

        void initTxtAmt()
        {
            txtLczdxmfAmt.Text = string.Empty;
            m_txtAnaethesiaAMT.Text = string.Empty;
            m_txtOperationAMT.Text = string.Empty;
            m_txtOtherAMT.Text = string.Empty;
            m_txtNurseAMT.Text = string.Empty;
            m_txtBloodAMT.Text = string.Empty;
            txtKjyfyAmt.Text = string.Empty;
            m_txtCMSemifinishedAMT.Text = string.Empty;
            m_txtCMFinishedAMT.Text = string.Empty;
            txtYbylfwfAmt.Text = string.Empty;
            txtYbylczfAmt.Text = string.Empty;
            txtQtfyAmt.Text = string.Empty;
            txtBlzdfAmt.Text = string.Empty;
            txtSyszdfAmt.Text = string.Empty;
            txtYxxzdfAmt.Text = string.Empty;
            txtLcwlzlfAmt.Text = string.Empty;
            txtKffAmt.Text = string.Empty;
            txtZyzlfAmt.Text = string.Empty;
            txtBdblzpfAmt.Text = string.Empty;
            txtQdblzpfAmt.Text = string.Empty;
            txtNxyzzpfAmt.Text = string.Empty;
            txtNxyzzpfAmt.Text = string.Empty;
            txtJcyycxyyclfAmt.Text = string.Empty;
            txtZlyycxyyclfAmt.Text = string.Empty;
            txtSsyycxyyclfAmt.Text = string.Empty;
        }

        #region 设置费用到病案内容
        double mzamt = 0.0;//麻醉费
        double ssamt = 0.0;//手术费
        double sszlamt = 0.0;//手术治疗费
        double kjyamt = 0.0;//抗菌药费
        double xyamt = 0.0;//西药费
        double fssxmamt = 0.0;//非手术项目治疗费    
        double lcwlzlf = 0.0;//临床物理治疗费
        /// <summary>
        /// 设置费用到病案内容
        /// </summary>
        /// <param name="p_dblMoney">费用金额</param>
        /// <param name="p_strChargeName">费用名称</param>
        /// <param name="p_dblSum">总和</param>
        private void m_mthSetMoneyToValue(double p_dblMoney, string p_strChargeName,  ref double p_dblSum)
        {
            p_dblSum += p_dblMoney;
            if (string.IsNullOrEmpty(p_strChargeName))
            {
                return;
            }           
            switch (p_strChargeName)
            {
                case "临床诊断项目费"://
                    txtLczdxmfAmt.Text = p_dblMoney.ToString();  
                    break;
                case "手术治疗费"://
                    sszlamt = p_dblMoney;
                    break;
                case "麻醉费"://
                    m_txtAnaethesiaAMT.Text = p_dblMoney.ToString();
                    mzamt = p_dblMoney;
                    break;
                case "手术费"://
                    m_txtOperationAMT.Text = p_dblMoney.ToString();
                    ssamt = p_dblMoney;
                    break;
                case "其他费":
                    m_txtOtherAMT.Text = p_dblMoney.ToString();
                    break;
                case "护理费"://
                    m_txtNurseAMT.Text = p_dblMoney.ToString();
                    break;
                case "血费":
                    m_txtBloodAMT.Text = p_dblMoney.ToString();
                    break;
                case "抗菌药物费用"://
                    txtKjyfyAmt.Text = p_dblMoney.ToString();
                    kjyamt = p_dblMoney;
                    break;
                case "西药费"://
                    xyamt = p_dblMoney;
                    break;
                case "中草药费":
                    m_txtCMSemifinishedAMT.Text = p_dblMoney.ToString();
                    break;
                case "中成药费"://
                    m_txtCMFinishedAMT.Text = p_dblMoney.ToString();
                    break;
                case "一般医疗服务费"://
                    txtYbylfwfAmt.Text = p_dblMoney.ToString();
                    break;
                case "一般治疗操作费"://
                    txtYbylczfAmt.Text = p_dblMoney.ToString();
                    break;
                case "其他费用":
                    txtQtfyAmt.Text = p_dblMoney.ToString();
                    break;
                case "病理诊断费":
                    txtBlzdfAmt.Text = p_dblMoney.ToString();
                    break;
                case "实验室诊断费"://
                    txtSyszdfAmt.Text = p_dblMoney.ToString();
                    break;
                case "影像学诊断费"://
                    txtYxxzdfAmt.Text = p_dblMoney.ToString();
                    break;
                case "非手术治疗项目费"://
                    fssxmamt = p_dblMoney;
                    break;
                case "临床物理治疗费"://
                    txtLcwlzlfAmt.Text = p_dblMoney.ToString();
                    lcwlzlf = p_dblMoney;
                    break;
                case "康复费":
                    txtKffAmt.Text = p_dblMoney.ToString();
                    break;
                case "中医治疗费":
                    txtZyzlfAmt.Text = p_dblMoney.ToString();
                    break;
                case "白蛋白类制品费":
                    txtBdblzpfAmt.Text = p_dblMoney.ToString();
                    break;
                case "球蛋白类制品费":
                    txtQdblzpfAmt.Text = p_dblMoney.ToString();
                    break;
                case "凝血因子类制品费":
                    txtNxyzzpfAmt.Text = p_dblMoney.ToString();
                    break;
                case "细胞因子类制品费":
                    txtNxyzzpfAmt.Text = p_dblMoney.ToString();
                    break;
                case "检查用一次性医用材料"://
                    txtJcyycxyyclfAmt.Text = p_dblMoney.ToString();
                    break;
                case "治疗用一次性医用材料费"://
                    txtZlyycxyyclfAmt.Text = p_dblMoney.ToString();
                    break;
                case "手术用一次性医用材料费"://
                    txtSsyycxyyclfAmt.Text = p_dblMoney.ToString();
                    break;
                #region 旧版屏蔽
                //case "手术费":
                //    m_txtOperationAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "接生费":
                //    m_txtDeliveryChildAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "检查费":
                //    m_txtCheckAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "麻醉费":
                //    m_txtAnaethesiaAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "婴儿费":
                //    m_txtBabyAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "陪床费":
                //    m_txtACCompanyAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "其他费":
                //    m_txtOtherAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "诊疗费":
                //    m_txtTreatmentAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "床位费":
                //    m_txtBedAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "护理费":
                //    m_txtNurseAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "放射费":
                //    m_txtRadiationAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "化验费":
                //    m_txtAssayAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "输氧费":
                //    m_txtO2AMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "输血费":
                //    m_txtBloodAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "西药费":
                //    m_txtWMAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "中草药费":
                //    m_txtCMFinishedAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                //case "中成药费":
                //    m_txtCMSemifinishedAMT.Text = p_dblMoney.ToString();
                //    p_dblSum += p_dblMoney;
                //    break;
                #endregion

            }


           // if (mzamt + ssamt + sszlamt != 0)
            //{
            txtSszlamt.Text = Convert.ToString(mzamt + ssamt + sszlamt);
            //}
            //if (kjyamt + xyamt != 0)
            //{
                m_txtWMAMT.Text = Convert.ToString(kjyamt + xyamt);
            //}
            //if (fssxmamt + lcwlzlf != 0)
            //{
                txtFsszlxmAmt.Text = Convert.ToString(fssxmamt + lcwlzlf);
            //}
        }
        #endregion
        #endregion

        #region 根据字典初始化界面固定选项值
        /// <summary>
        /// 根据字典初始化界面固定选项值
        /// </summary>
        /// <param name="p_dtbDict">字典</param>
        public void m_mthInitDict(DataTable p_dtbDict)
        {
            if (p_dtbDict == null || p_dtbDict.Rows.Count == 0)
            {
                return;
            }
            DataView drView = new DataView(p_dtbDict);
            //是否尸检
            drView.RowFilter = "fcode='GBIFSJ'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboCorpseCheck.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboCorpseCheck.Items.AddRange(objDict);
            }
            drView.RowFilter = "fcode='GBBLOOD'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboBloodType.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboBloodType.Items.AddRange(objDict);
            }
            //Rh血型
            drView.RowFilter = "fcode='GBRH'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboRh.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboRh.Items.AddRange(objDict);
            }
          
        } 
        #endregion

        #region 获取病案内容
        /// <summary>
        /// 获取病案内容
        /// </summary>
        /// <param name="p_dsCaseContent">病案内容</param>
        public void m_mthGetCaseContent(System.Data.DataSet p_dsCaseContent)
        {
            if (p_dsCaseContent == null)
            {
                p_dsCaseContent = new DataSet("CaseContent");
            }
            DataTable dtbBA1 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA1"))
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                long lngRes = objDomain.m_lngGetHIS_BA1Schema(out dtbBA1);
                dtbBA1.TableName = "HIS_BA1";
                p_dsCaseContent.Tables.Add(dtbBA1);
            }
            else
            {
                dtbBA1 = p_dsCaseContent.Tables["HIS_BA1"];
            }

            DataRow drBA1 = null;
            if (dtbBA1.Rows.Count == 0)
            {
                drBA1 = dtbBA1.NewRow();
                dtbBA1.Rows.Add(drBA1);
            }
            else
            {
                drBA1 = dtbBA1.Rows[0];
            }

            double dblTemp = 0d;


            if (double.TryParse(m_txtTotalAMT.Text,out dblTemp))
            {
                drBA1["FSUM1"] = dblTemp;
            }
            if (double.TryParse(m_txtSelfAMT.Text, out dblTemp))
            {
                drBA1["FZFJE"] = dblTemp;
            }
            if (double.TryParse(txtYbylfwfAmt.Text, out dblTemp))
            {
                drBA1["FZHFWLYLF"] = dblTemp;
            }
            if (double.TryParse(txtYbylczfAmt.Text, out dblTemp))
            {
                drBA1["FZHFWLCZF"] = dblTemp;
            }
            if (double.TryParse(m_txtNurseAMT.Text, out dblTemp))
            {
                drBA1["FZHFWLHLF"] = dblTemp;
            }
            if (double.TryParse(txtQtfyAmt.Text, out dblTemp))
            {
                drBA1["FZHFWLQTF"] = dblTemp;
            }
            if (double.TryParse(txtBlzdfAmt.Text, out dblTemp))
            {
                drBA1["FZDLBLF"] = dblTemp;
            }
            if (double.TryParse(txtSyszdfAmt.Text, out dblTemp))
            {
                drBA1["FZDLSSSF"] = dblTemp;
            }
            if (double.TryParse(txtYxxzdfAmt.Text, out dblTemp))
            {
                drBA1["FZDLYXF"] = dblTemp;
            }
            if (double.TryParse(txtLczdxmfAmt.Text, out dblTemp))
            {
                drBA1["FZDLLCF"] = dblTemp;
            }
            if (double.TryParse(txtFsszlxmAmt.Text, out dblTemp))
            {
                drBA1["FZLLFFSSF"] = dblTemp;
            }
            if (double.TryParse(txtLcwlzlfAmt.Text, out dblTemp))
            {
                drBA1["FZLLFWLZWLF"] = dblTemp;//FZDLLCF
            }
            if (double.TryParse(txtSszlamt.Text, out dblTemp))
            {
                drBA1["FZLLFSSF"] = dblTemp;
            }
            if (double.TryParse(m_txtAnaethesiaAMT.Text, out dblTemp))
            {
                drBA1["FZLLFMZF"] = dblTemp;
            }
            if (double.TryParse(m_txtOperationAMT.Text, out dblTemp))
            {
                drBA1["FZLLFSSZLF"] = dblTemp;
            }
            if (double.TryParse(txtKffAmt.Text, out dblTemp))
            {
                drBA1["FKFLKFF"] = dblTemp;
            }
            if (double.TryParse(txtZyzlfAmt.Text, out dblTemp))
            {
                drBA1["FZYLZF"] = dblTemp;
            }
            //西药费
            if (double.TryParse(m_txtWMAMT.Text, out dblTemp))
            {
                drBA1["FXYF"] = dblTemp;
            }
            if (double.TryParse(txtKjyfyAmt.Text, out dblTemp))
            {
                drBA1["FXYLGJF"] = dblTemp;
            }
            if (double.TryParse(m_txtBloodAMT.Text, out dblTemp))
            {
                drBA1["FXYLXF"] = dblTemp;
            }
            if (double.TryParse(txtBdblzpfAmt.Text, out dblTemp))
            {
                drBA1["FXYLBQBF"] = dblTemp;
            }
            if (double.TryParse(txtQdblzpfAmt.Text, out dblTemp))
            {
                drBA1["FXYLQDBF"] = dblTemp;
            }
            if (double.TryParse(txtNxyzzpfAmt.Text, out dblTemp))
            {
                drBA1["FXYLYXYZF"] = dblTemp;
            }
            if (double.TryParse(textBox19.Text, out dblTemp))
            {
                drBA1["FXYLXBYZF"] = dblTemp;
            }
            if (double.TryParse(txtJcyycxyyclfAmt.Text, out dblTemp))
            {
                drBA1["FHCLCJF"] = dblTemp;
            }
            if (double.TryParse(txtZlyycxyyclfAmt.Text, out dblTemp))
            {
                drBA1["FHCLZLF"] = dblTemp;
            }
            if (double.TryParse(txtSsyycxyyclfAmt.Text, out dblTemp))
            {
                drBA1["FHCLSSF"] = dblTemp;
            }

            if (double.TryParse(m_txtOtherAMT.Text, out dblTemp))
            {
                drBA1["FQTF"] = dblTemp;
            }
            if (double.TryParse(m_txtWMAMT.Text, out dblTemp))
            {
                drBA1["FXYF"] = dblTemp;
            }
            double dblCMAMT = 0d;
            if (double.TryParse(m_txtCMFinishedAMT.Text, out dblTemp))
            {
                drBA1["FZCHYF"] = dblTemp;
                dblCMAMT += dblTemp;
            }
            if (double.TryParse(m_txtCMSemifinishedAMT.Text, out dblTemp))
            {
                drBA1["FZCYF"] = dblTemp;
                dblCMAMT += dblTemp;
            }
            drBA1["FZYF"] = dblCMAMT;         
            if (double.TryParse(m_txtOtherAMT.Text, out dblTemp))
            {
                drBA1["FQTF"] = dblTemp;
            }
            if (m_cboCorpseCheck.SelectedItem != null && m_cboCorpseCheck.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FBODYBH"] = ((clsGDCaseDictVO)m_cboCorpseCheck.SelectedItem).m_strCode;
                drBA1["FBODY"] = m_cboCorpseCheck.Text;
            }
           
            if (m_cboBloodType.SelectedItem != null && m_cboBloodType.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FBLOODBH"] = ((clsGDCaseDictVO)m_cboBloodType.SelectedItem).m_strCode;
                drBA1["FBLOOD"] = m_cboBloodType.Text;
            }
            if (m_cboRh.SelectedItem != null && m_cboRh.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FRHBH"] = ((clsGDCaseDictVO)m_cboRh.SelectedItem).m_strCode;
                drBA1["FRH"] = m_cboRh.Text;
            }
            
        } 
        #endregion
        #endregion

    }
}
