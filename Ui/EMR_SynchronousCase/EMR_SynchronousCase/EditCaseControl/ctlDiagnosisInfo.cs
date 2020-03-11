using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.emr.EMR_SynchronousCase;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// 病人诊断信息
    /// </summary>
    public partial class ctlDiagnosisInfo : UserControl, infSynchronousCaseControl
    {
        /// <summary>
        /// 签名工具类
        /// </summary>
        clsEmrSignToolCollection m_objSign = null;
        /// <summary>
        /// 病人诊断信息
        /// </summary>
        public ctlDiagnosisInfo()
        {
            InitializeComponent();
            m_dgwOutDiagnosis.AutoGenerateColumns = false;
            //m_dgwInfection.AutoGenerateColumns = false;

            m_objSign = new clsEmrSignToolCollection();
            m_objSign.m_mthBindEmployeeSign(new Control[] { m_cmdDeptDirector, m_cmdSeniorDoctor, m_cmdAttendingDoctor, m_cmdResidentDoctor, m_cmdAdvancedDoctor, m_cmdGraduateDoctor, m_cmdInternDoctor, m_cmdCoder, m_cmdQualityControlDoc, m_cmdQualityControlNurse },
                new Control[] { m_txtDeptDirector, m_txtSeniorDoctor, m_txtAttendingDoctor, m_txtResidentDoctor, m_txtAdvancedDoctor, m_txtGraduateDoctor, m_txtInternDoctor, m_txtCoder, m_txtQualityControlDoc, m_txtQualityControlNurse },
                new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 }, new bool[] { false, false, false, false, false, false, false, false, false, false });
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
                m_txtPathologicalDiagnosis.Text = drBA1["FPHZD"].ToString();
                m_txtAllergic.Text = drBA1["FGMYW"].ToString();
                //if (drBA1["FHBSAGBH"] != DBNull.Value)
                //{
                //    for (int iI = 0; iI < m_cboHbsAg.Items.Count; iI++)
                //    {
                //        if (((clsGDCaseDictVO)m_cboHbsAg.Items[iI]).m_strCode == drBA1["FHBSAGBH"].ToString())
                //        {
                //            m_cboHbsAg.SelectedIndex = iI;
                //            break;
                //        }
                //    }
                //}
                //if (drBA1["FHCVABBH"] != DBNull.Value)
                //{
                //    for (int iI = 0; iI < m_cboHCVAb.Items.Count; iI++)
                //    {
                //        if (((clsGDCaseDictVO)m_cboHCVAb.Items[iI]).m_strCode == drBA1["FHCVABBH"].ToString())
                //        {
                //            m_cboHCVAb.SelectedIndex = iI;
                //            break;
                //        }
                //    }
                //}
                //if (drBA1["FHIVABBH"] != DBNull.Value)
                //{
                //    for (int iI = 0; iI < m_cboHIVAb.Items.Count; iI++)
                //    {
                //        if (((clsGDCaseDictVO)m_cboHIVAb.Items[iI]).m_strCode == drBA1["FHIVABBH"].ToString())
                //        {
                //            m_cboHIVAb.SelectedIndex = iI;
                //            break;
                //        }
                //    }
                //}
                if (drBA1["FMZCYACCOBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboOutPatientAndOut.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboOutPatientAndOut.Items[iI]).m_strCode == drBA1["FMZCYACCOBH"].ToString())
                        {
                            m_cboOutPatientAndOut.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FRYCYACCOBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboInAndOut.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboInAndOut.Items[iI]).m_strCode == drBA1["FRYCYACCOBH"].ToString())
                        {
                            m_cboInAndOut.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FLCBLACCOBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboClinicalAndPathological.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboClinicalAndPathological.Items[iI]).m_strCode == drBA1["FLCBLACCOBH"].ToString())
                        {
                            m_cboClinicalAndPathological.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FFSBLACCOBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboRadioAndPathological.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboRadioAndPathological.Items[iI]).m_strCode == drBA1["FFSBLACCOBH"].ToString())
                        {
                            m_cboRadioAndPathological.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FOPACCOBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboOpBeforeAndAfter.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboOpBeforeAndAfter.Items[iI]).m_strCode == drBA1["FOPACCOBH"].ToString())
                        {
                            m_cboOpBeforeAndAfter.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_txtRescueTimes.Text = drBA1["FQJTIMES"].ToString();
                m_txtRescueSuccTimes.Text = drBA1["FQJSUCTIMES"].ToString();
                List<TextBoxBase> lstSignText = new List<TextBoxBase>();
                List<string> lstEmpNO = new List<string>();
                if (drBA1["FKZRBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtDeptDirector);
                    lstEmpNO.Add(drBA1["FKZRBH"].ToString());
                }
                if (drBA1["FZRDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtSeniorDoctor);
                    lstEmpNO.Add(drBA1["FZRDOCTBH"].ToString());
                }
                if (drBA1["FZZDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtAttendingDoctor);
                    lstEmpNO.Add(drBA1["FZZDOCTBH"].ToString());
                }
                if (drBA1["FZYDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtResidentDoctor);
                    lstEmpNO.Add(drBA1["FZYDOCTBH"].ToString());
                }
                if (drBA1["FJXDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtAdvancedDoctor);
                    lstEmpNO.Add(drBA1["FJXDOCTBH"].ToString());
                }
                else
                {
                    m_txtAdvancedDoctor.Text = drBA1["FJXDOCT"].ToString();
                }
                if (drBA1["FNURSEBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtGraduateDoctor);
                    lstEmpNO.Add(drBA1["FNURSEBH"].ToString());
                }
                else
                {
                    m_txtGraduateDoctor.Text = drBA1["FNURSEBH"].ToString();
                }
                if (drBA1["FSXDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtInternDoctor);
                    lstEmpNO.Add(drBA1["FSXDOCTBH"].ToString());
                }
                else
                {
                    m_txtInternDoctor.Text = drBA1["FSXDOCTBH"].ToString();
                }
                if (drBA1["FBMYBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtCoder);
                    lstEmpNO.Add(drBA1["FBMYBH"].ToString());
                }
                if (drBA1["FZKDOCTBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtQualityControlDoc);
                    lstEmpNO.Add(drBA1["FZKDOCTBH"].ToString());
                }
                if (drBA1["FZKNURSEBH"] != DBNull.Value)
                {
                    lstSignText.Add(m_txtQualityControlNurse);
                    lstEmpNO.Add(drBA1["FZKNURSEBH"].ToString());
                }
                if (lstSignText.Count > 0)
                {
                    bool[] blnEnable = new bool[lstSignText.Count];
                    for (int iB = 0; iB < blnEnable.Length; iB++)
                    {
                        blnEnable[iB] = true;
                    }
                    clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                    objDomain.m_mthAddSignToTextBoxByEmpNO(lstSignText.ToArray(), lstEmpNO.ToArray(), blnEnable);   
                }

                if (drBA1["FQUALITYBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboCaseQuality.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboCaseQuality.Items[iI]).m_strCode == drBA1["FQUALITYBH"].ToString())
                        {
                            m_cboCaseQuality.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_dtpQualityControl.Value = Convert.ToDateTime(drBA1["FZKRQ"]);

                //su.liang 病理诊断
                m_txtPathologicalDiagnosis.Text = drBA1["FPHZD"].ToString();
                m_txtPathologyNo.Text = drBA1["FPHZDNUM"].ToString();//FPHZDNUM
                m_txtPathologyNumber.Text = drBA1["FPHZDBH"].ToString();//FPHZDNUM
                //离院方式
                if (drBA1["FLYFSBH"].ToString() == "1")
                {
                    m_gpbLeavehospital.Tag="1";
                    m_rdbLeaveByOrder.Checked = true;
                }
                else if (drBA1["FLYFSBH"].ToString() == "2")
                {
                    m_gpbLeavehospital.Tag = "2";
                    m_rdbReferralByOrder.Checked = true;
                }
                else if (drBA1["FLYFSBH"].ToString() == "3")
                {
                    m_gpbLeavehospital.Tag = "3";
                    m_rdbReferralToHospital.Checked = true;
                }
                else if (drBA1["FLYFSBH"].ToString() == "4")
                {
                    m_gpbLeavehospital.Tag = "4";
                    m_rdbLeaveNoOrder.Checked = true;
                }
                else if (drBA1["FLYFSBH"].ToString() == "5")
                {
                    m_gpbLeavehospital.Tag = "5";
                    m_rdbLeaveByDead.Checked = true;
                }
                else
                {
                    m_gpbLeavehospital.Tag = "9";
                    m_rdbOther.Checked = true;
                }


                //31日再入院计划
                if (drBA1["FISAGAINRYBH"].ToString() == "2")
                {
                    m_gpbReadmission.Tag = drBA1["FISAGAINRYBH"].ToString();
                    m_rdbReadmissionYes.Checked = true;
                    m_txtReadmissionForWhat.Text = drBA1["FISAGAINRYMD"].ToString();
                }
                else
                {
                    m_gpbReadmission.Tag = "1";
                    m_rdbReadmissionNo.Checked = true;
                }
                
                //颅脑损伤
                m_txtQday.Text = drBA1["FRYQHMDAYS"].ToString();
                m_txtQHour.Text = drBA1["FRYQHMHOURS"].ToString();
                m_txtQmin.Text = drBA1["FRYQHMMINS"].ToString();
                //FRYQHMCOUNTS
                m_txtHday.Text = drBA1["FRYHMDAYS"].ToString();
                m_txtHhour.Text = drBA1["FRYHMHOURS"].ToString();
                m_txtHmin.Text = drBA1["FRYHMMINS"].ToString();
                //FRYHMCOUNTSZ

            }

            DataTable dtbOutDig = new DataTable();
            dtbOutDig.Columns.Add("name");
            dtbOutDig.Columns.Add("code");
            dtbOutDig.Columns.Add("outinfo");
            m_dgwOutDiagnosis.DataSource = dtbOutDig;

            //DataTable dtbInfection = new DataTable();
            //dtbInfection.Columns.Add("name");
            //dtbInfection.Columns.Add("code");
            //dtbInfection.Columns.Add("outinfo");
            //m_dgwInfection.DataSource = dtbInfection;

            if (p_dsContent.Tables.Contains("HIS_BA3") && p_dsContent.Tables["HIS_BA3"].Rows.Count > 0)
            {
                DataTable dtbBA3 = p_dsContent.Tables["HIS_BA3"];
                DataRow[] drS = dtbBA3.Select("FZDLX = '1'");//主要诊断
                if (drS != null && drS.Length > 0)
                {
                    m_txtOutMainDiagICD.Text = drS[0]["FICDM"].ToString();
                    m_txtOutMainDiag.Text = drS[0]["FJBNAME"].ToString();
                    if (drS[0]["FRYBQBH"] != DBNull.Value)
                    {
                        for (int iI = 0; iI < m_cboOutMainDiagInfo.Items.Count; iI++)
                        {
                            if (((clsGDCaseDictVO)m_cboOutMainDiagInfo.Items[iI]).m_strCode == drS[0]["FRYBQBH"].ToString())
                            {
                                m_cboOutMainDiagInfo.SelectedIndex = iI;
                                break;
                            }
                        }
                    }
                }

                //drS = dtbBA3.Select("FZDLX = 'f'");//附属诊断
                //if (drS != null && drS.Length > 0)
                //{
                //    m_txtSubICD.Text = drS[0]["FICDM"].ToString();
                //    m_txtSubordinateDiag.Text = drS[0]["FJBNAME"].ToString();
                //    if (drS[0]["FRYBQBH"] != DBNull.Value)
                //    {
                //        for (int iI = 0; iI < m_cboSubOutInfo.Items.Count; iI++)
                //        {
                //            if (((clsGDCaseDictVO)m_cboSubOutInfo.Items[iI]).m_strCode == drS[0]["FRYBQBH"].ToString())
                //            {
                //                m_cboSubOutInfo.SelectedIndex = iI;
                //                break;
                //            }
                //        }
                //    }
                //}
                

                drS = dtbBA3.Select("FZDLX = 's'");//损伤中毒
                if (drS != null && drS.Length > 0)
                {
                    m_txtPoisoningResonICD.Text = drS[0]["FICDM"].ToString();
                    m_txtPoisoningReson.Text = drS[0]["FJBNAME"].ToString();
                }
               
                drS = dtbBA3.Select("FZDLX = '2'");//其他诊断
                if (drS != null && drS.Length > 0)
                {
                    dtbOutDig.BeginLoadData();
                    for (int iD = 0; iD < drS.Length; iD++)
                    {
                        DataRow drNew = dtbOutDig.NewRow();
                        drNew["name"] = drS[iD]["FJBNAME"].ToString();
                        drNew["code"] = drS[iD]["FICDM"].ToString();
                        drNew["outinfo"] = drS[iD]["FRYBQ"].ToString();
                        dtbOutDig.LoadDataRow(drNew.ItemArray,true);
                    }
                    dtbOutDig.EndLoadData();
                }

                //drS = dtbBA3.Select("FZDLX = 'y'");//院内感染
                //if (drS != null && drS.Length > 0)
                //{
                //    dtbInfection.BeginLoadData();
                //    for (int iD = 0; iD < drS.Length; iD++)
                //    {
                //        DataRow drNew = dtbOutDig.NewRow();
                //        drNew["name"] = drS[iD]["FJBNAME"].ToString();
                //        drNew["code"] = drS[iD]["FICDM"].ToString();
                //        drNew["outinfo"] = drS[iD]["FRYBQ"].ToString();
                //        dtbInfection.LoadDataRow(drNew.ItemArray, true);
                //    }
                //    dtbInfection.EndLoadData();
                //}
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
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            //获取出院其它诊断
            DataTable dtbOutDiag = null;
            long lngRes = objDomain.m_lngGetDiagnosis(p_strRegisterID, "3", out dtbOutDiag);
            if (dtbOutDiag != null && dtbOutDiag.Rows.Count > 0)
            {
                for (int iRow = 0; iRow < dtbOutDiag.Rows.Count; iRow++)
                {
                    dtbOutDiag.Rows[iRow]["outinfo"] = m_strOutInfo(dtbOutDiag.Rows[iRow]["outinfo"].ToString());
                }
            }
            m_dgwOutDiagnosis.DataSource = dtbOutDiag;
            ////医院感染名称
            //DataTable dtbInfection = null;
            //lngRes = objDomain.m_lngGetDiagnosis(p_strRegisterID, "2", out dtbInfection);
            //if (dtbInfection != null && dtbInfection.Rows.Count > 0)
            //{
            //    for (int iRow = 0; iRow < dtbInfection.Rows.Count; iRow++)
            //    {
            //        dtbInfection.Rows[iRow]["outinfo"] = m_strOutInfo(dtbInfection.Rows[iRow]["outinfo"].ToString());
            //    }
            //}
            //m_dgwInfection.DataSource = dtbInfection;

            //病案首页诊断信息及医生签名
            DataTable dtbDS = null;
            lngRes = objDomain.m_lngGetPatientDiagnosisInfo(p_strRegisterID, out dtbDS);
            if (dtbDS != null && dtbDS.Rows.Count > 0)
            {
                DataRow drDS = dtbDS.Rows[0];
                int intIndex = 0;
                m_txtOutMainDiag.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["maindiagnosis"].ToString(), drDS["maindiagnosisxml"].ToString());
                m_txtOutMainDiagICD.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["icd_10ofmain"].ToString(), drDS["icd_10ofmainxml"].ToString());
                if (int.TryParse(drDS["mainconditionseq"].ToString(), out intIndex))
                {
                    m_cboOutMainDiagInfo.SelectedIndex = intIndex;
                }

                //m_txtSubordinateDiag.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SUBSIDIARYDIAGNOSIS"].ToString(), drDS["subsidiarydiagnosisxml"].ToString());
                //m_txtSubICD.Text = drDS["ICDOFSUBSIDIARYDIAGNOSIS"].ToString();
                //if (int.TryParse(drDS["SUBSIDIARYDIAGNOSISSEQ"].ToString(), out intIndex))
                //{
                //    m_cboSubOutInfo.SelectedIndex = intIndex;
                //}

                m_txtPathologicalDiagnosis.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["PATHOLOGYDIAGNOSIS"].ToString(), drDS["PATHOLOGYDIAGNOSISXML"].ToString());
                m_txtPoisoningReson.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SCACHESOURCE"].ToString(), drDS["SCACHESOURCEXML"].ToString());
                m_txtPoisoningResonICD.Text = drDS["SCACHESOURCEICD"].ToString();//com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SCACHESOURCEICD"].ToString(), drDS["SCACHESOURCEICDXML"].ToString());
                m_txtAllergic.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SENSITIVE"].ToString(), drDS["SENSITIVEXML"].ToString());
                m_txtPathologyNo.Text = drDS["blzd_blh"].ToString();
                m_txtPathologyNumber.Text = drDS["blzd_jbbm"].ToString();
                m_gpbLeavehospital.Tag = drDS["discharged_int"].ToString();
                if (m_gpbLeavehospital.Tag.ToString() == "1")
                    m_rdbLeaveByOrder.Checked = true;
                else if (m_gpbLeavehospital.Tag.ToString() == "2")
                {
                    m_rdbReferralByOrder.Checked = true;
                    m_txtHospitalName.Text = drDS["discharged_varh"].ToString();
                }
                else if (m_gpbLeavehospital.Tag.ToString() == "3")
                {
                    m_rdbReferralToHospital.Checked = true;
                    m_txtHospitalName.Text = drDS["discharged_varh"].ToString();
                }
                else if (m_gpbLeavehospital.Tag.ToString() == "4")
                    m_rdbLeaveNoOrder.Checked = true;
                else if (m_gpbLeavehospital.Tag.ToString() == "5")
                    m_rdbLeaveByDead.Checked = true;
                else
                    m_rdbOther.Checked = true;
                
                m_gpbReadmission.Tag = drDS["readmitted31_int"].ToString();
                if (m_gpbReadmission.Tag.ToString() == "2")
                {
                    m_rdbReadmissionYes.Checked = true;
                    m_txtReadmissionForWhat.Text = drDS["readmitted31_varh"].ToString();
                }
                else
                    m_rdbReadmissionNo.Checked = true;
                
                m_txtQday.Text = drDS["inrnssday"].ToString();
                m_txtQHour.Text = drDS["inrnsshour"].ToString();
                m_txtQmin.Text = drDS["inrnssmin"].ToString();
                m_txtHday.Text = drDS["outrnssday"].ToString();
                m_txtHhour.Text = drDS["outrnsshour"].ToString();
                m_txtHmin.Text = drDS["outrnssmin"].ToString();
                //if (drDS["hbsag"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["hbsag"].ToString(),drDS["hbsagxml"].ToString()),out intIndex))
                //{
                //    m_cboHbsAg.SelectedIndex = intIndex;
                //}
                //else
                //{
                //    m_cboHbsAg.SelectedIndex = 0;
                //}
                //if (drDS["hiv_ab"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["hiv_ab"].ToString(), drDS["hiv_abxml"].ToString()), out intIndex))
                //{
                //    m_cboHIVAb.SelectedIndex = intIndex;
                //}
                //else
                //{
                //    m_cboHIVAb.SelectedIndex = 0;
                //}
                //if (drDS["hcv_ab"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["hcv_ab"].ToString(), drDS["hcv_abxml"].ToString()), out intIndex))
                //{
                //    m_cboHCVAb.SelectedIndex = intIndex;
                //}
                //else
                //{
                //    m_cboHCVAb.SelectedIndex = 0;
                //}
                if (drDS["ACCORDWITHOUTHOSPITAL"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["ACCORDWITHOUTHOSPITAL"].ToString(), drDS["ACCORDWITHOUTHOSPITALXML"].ToString()), out intIndex))
                {
                    m_cboOutPatientAndOut.SelectedIndex = intIndex;
                }
                else
                {
                    m_cboOutPatientAndOut.SelectedIndex = 0;
                }
                if (drDS["ACCORDINWITHOUT"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["ACCORDINWITHOUT"].ToString(), drDS["ACCORDINWITHOUTXML"].ToString()), out intIndex))
                {
                    m_cboInAndOut.SelectedIndex = intIndex;
                }
                else
                {
                    m_cboInAndOut.SelectedIndex = 0;
                }
                if (drDS["ACCORDBEFOREOPERATIONWITHAFTER"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["ACCORDBEFOREOPERATIONWITHAFTER"].ToString(), drDS["ACCORDBFOPRWITHAFXML"].ToString()), out intIndex))
                {
                    m_cboOpBeforeAndAfter.SelectedIndex = intIndex;
                }
                else
                {
                    m_cboOpBeforeAndAfter.SelectedIndex = 0;
                }
                if (drDS["ACCORDCLINICWITHPATHOLOGY"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["ACCORDCLINICWITHPATHOLOGY"].ToString(), drDS["ACCORDCLINICWITHPATHOLOGYXML"].ToString()), out intIndex))
                {
                    m_cboClinicalAndPathological.SelectedIndex = intIndex;
                }
                else
                {
                    m_cboClinicalAndPathological.SelectedIndex = 0;
                }
                if (drDS["ACCORDRADIATEWITHPATHOLOGY"] != DBNull.Value && int.TryParse(com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["ACCORDRADIATEWITHPATHOLOGY"].ToString(), drDS["ACCORDRADIATEWITHPATHOLOGYXML"].ToString()), out intIndex))
                {
                    m_cboRadioAndPathological.SelectedIndex = intIndex;
                }
                else
                {
                    m_cboRadioAndPathological.SelectedIndex = 0;
                }
                m_txtRescueTimes.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SALVETIMES"].ToString(), drDS["SALVETIMESXML"].ToString());
                m_txtRescueSuccTimes.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drDS["SALVESUCCESS"].ToString(), drDS["SALVESUCCESSXML"].ToString());
                if (drDS["QUALITY"] != DBNull.Value && int.TryParse(drDS["QUALITY"].ToString(), out intIndex))
                {
                    m_cboCaseQuality.SelectedIndex = intIndex;
                }
                DateTime dtmTemp = DateTime.MinValue;
                if (drDS["QCTIME"] != DBNull.Value && DateTime.TryParse(drDS["QCTIME"].ToString(),out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue)
                    {
                        m_dtpQualityControl.Value = dtmTemp;
                    }
                }

                objDomain.m_mthAddSignToTextBoxByEmpID(new TextBoxBase[] { m_txtDeptDirector, m_txtSeniorDoctor, m_txtAttendingDoctor, m_txtResidentDoctor, m_txtAdvancedDoctor, m_txtGraduateDoctor, m_txtInternDoctor, m_txtCoder, m_txtQualityControlDoc, m_txtQualityControlNurse },
                    new string[] { drDS["DIRECTORDT"].ToString(), drDS["SUBDIRECTORDT"].ToString(), drDS["DT"].ToString(), drDS["INHOSPITALDT"].ToString(), drDS["ATTENDINFORADVANCESSTUDYDT"].ToString(), drDS["GRADUATESTUDENTINTERN"].ToString(), drDS["INTERN"].ToString(), drDS["CODER"].ToString(), drDS["QCDT"].ToString(), drDS["QCNURSE"].ToString() },
                    new bool[] { true, true, true, true, true, true, true, true, true, true });
                if (m_txtInternDoctor.Tag == null && drDS["INTERN"] != DBNull.Value)
                {
                    m_txtInternDoctor.Text = drDS["INTERN"].ToString();
                }
            }
            objDomain = null;
            m_blnHasInit = true;
        }

        /// <summary>
        /// 根据选择的索引返回出院情况
        /// </summary>
        /// <param name="p_strIndex">选择的索引</param>
        /// <returns></returns>
        private string m_strOutInfo(string p_strIndex)
        {
            string strInfo = string.Empty;
            switch (p_strIndex)
            {
                //case "1":
                //    strInfo = "治愈";
                //    break;
                //case "2":
                //    strInfo = "好转";
                //    break;
                //case "3":
                //    strInfo = "未愈";
                //    break;
                //case "4":
                //    strInfo = "死亡";
                //    break;
                //case "5":
                //    strInfo = "其他";
                //    break;
                //default:
                //    break;
                case "1":
                    strInfo = "有";
                    break;
                case "2":
                    strInfo = "临床未确定";
                    break;
                case "3":
                    strInfo = "情况不明";
                    break;
                case "4":
                    strInfo = "无";
                    break;
                default:
                    break;
            }
            return strInfo;
        }
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
            //出院情况//入院情况GBRYBQ
            drView.RowFilter = "fcode='GBRYBQ'";//GBCYQK
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clsGDCaseDictVO[] objPayType = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objPayType[iM] = new clsGDCaseDictVO();
                    objPayType[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objPayType[iM].m_strName = drView[iM]["fmc"].ToString();
                    clmOutInfo.Items.Add(drView[iM]["fmc"].ToString());
                    //cmlInfectionOutInfo.Items.Add(drView[iM]["fmc"].ToString());
                }
                m_cboOutMainDiagInfo.Items.AddRange(objPayType);
                //m_cboSubOutInfo.Items.AddRange(objPayType);
            }
            ////HBsAG、HCV-Ab、HIV-Ab
            //drView.RowFilter = "fcode='GBJCKT'";
            //drView.Sort = "fbh ASC";
            //if (drView != null && drView.Count > 0)
            //{
            //    clsGDCaseDictVO[] objPayType = new clsGDCaseDictVO[drView.Count];
            //    for (int iM = 0; iM < drView.Count; iM++)
            //    {
            //        objPayType[iM] = new clsGDCaseDictVO();
            //        objPayType[iM].m_strCode = drView[iM]["fbh"].ToString();
            //        objPayType[iM].m_strName = drView[iM]["fmc"].ToString();
            //    }
            //    m_cboHbsAg.Items.AddRange(objPayType);
            //    m_cboHCVAb.Items.AddRange(objPayType);
            //    m_cboHIVAb.Items.AddRange(objPayType);
            //}
            //诊断符合情况
            drView.RowFilter = "fcode='GBZDFHQK'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clsGDCaseDictVO[] objPayType = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objPayType[iM] = new clsGDCaseDictVO();
                    objPayType[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objPayType[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboOutPatientAndOut.Items.AddRange(objPayType);
                m_cboInAndOut.Items.AddRange(objPayType);
                m_cboOpBeforeAndAfter.Items.AddRange(objPayType);
                m_cboClinicalAndPathological.Items.AddRange(objPayType);
                m_cboRadioAndPathological.Items.AddRange(objPayType);
            }
            //病案质量
            drView.RowFilter = "fcode='GBBAZL'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clsGDCaseDictVO[] objPayType = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objPayType[iM] = new clsGDCaseDictVO();
                    objPayType[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objPayType[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboCaseQuality.Items.AddRange(objPayType);
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
            drBA1["FPHZD"] = m_txtPathologicalDiagnosis.Text;
            drBA1["FGMYW"] = m_txtAllergic.Text;
            if (m_txtAllergic.Text.ToString().Length != 0)
            {
                drBA1["FIFGMYWBH"] = "2";
                drBA1["FIFGMYW"] = "有";
            }
            else
            {
                drBA1["FIFGMYWBH"] = "1";
                drBA1["FIFGMYW"] = "无";
            }
            //if (m_cboHbsAg.SelectedItem != null)
            //{
            //    drBA1["FHBSAGBH"] = ((clsGDCaseDictVO)m_cboHbsAg.SelectedItem).m_strCode;
            //    drBA1["FHBSAG"] = m_cboHbsAg.Text;
            //}
            //if (m_cboHCVAb.SelectedItem != null)
            //{
            //    drBA1["FHCVABBH"] = ((clsGDCaseDictVO)m_cboHCVAb.SelectedItem).m_strCode;
            //    drBA1["FHCVAB"] = m_cboHCVAb.Text;
            //}
            //if (m_cboHIVAb.SelectedItem != null)
            //{
            //    drBA1["FHIVABBH"] = ((clsGDCaseDictVO)m_cboHIVAb.SelectedItem).m_strCode;
            //    drBA1["FHIVAB"] = m_cboHIVAb.Text;
            //}
            if (m_cboOutPatientAndOut.SelectedItem != null)
            {
                drBA1["FMZCYACCOBH"] = ((clsGDCaseDictVO)m_cboOutPatientAndOut.SelectedItem).m_strCode;
                drBA1["FMZCYACCO"] = m_cboOutPatientAndOut.Text;
            }
            if (m_cboInAndOut.SelectedItem != null)
            {
                drBA1["FRYCYACCOBH"] = ((clsGDCaseDictVO)m_cboInAndOut.SelectedItem).m_strCode;
                drBA1["FRYCYACCO"] = m_cboInAndOut.Text;
            }
            if (m_cboClinicalAndPathological.SelectedItem != null)
            {
                drBA1["FLCBLACCOBH"] = ((clsGDCaseDictVO)m_cboClinicalAndPathological.SelectedItem).m_strCode;
                drBA1["FLCBLACCO"] = m_cboClinicalAndPathological.Text;
            }
            if (m_cboRadioAndPathological.SelectedItem != null)
            {
                drBA1["FFSBLACCOBH"] = ((clsGDCaseDictVO)m_cboRadioAndPathological.SelectedItem).m_strCode;
                drBA1["FFSBLACCO"] = m_cboRadioAndPathological.Text;
            }
            if (m_cboOpBeforeAndAfter.SelectedItem != null)
            {
                drBA1["FOPACCOBH"] = ((clsGDCaseDictVO)m_cboOpBeforeAndAfter.SelectedItem).m_strCode;
                drBA1["FOPACCO"] = m_cboOpBeforeAndAfter.Text;
            }
            int intTemp = 0;
            if (int.TryParse(m_txtRescueTimes.Text,out intTemp))
            {
                drBA1["FQJTIMES"] =  m_txtRescueTimes.Text;
                if (intTemp == 0)
                {
                    drBA1["FQJBR"] = 0;
                }
                else
                {
                    drBA1["FQJBR"] = 1;
                }
            }
            else
            {
                drBA1["FQJTIMES"] = 0;
                drBA1["FQJBR"] = 0;
            }
            if (int.TryParse(m_txtRescueSuccTimes.Text,out intTemp))
            {
                drBA1["FQJSUCTIMES"] = intTemp;
                if (Convert.ToInt32(drBA1["FQJBR"]) > 0)
                {
                    if (intTemp != Convert.ToInt32(drBA1["FQJTIMES"]))
                    {
                        drBA1["FQJSUC"] = 0;
                    }
                    else
                    {
                        drBA1["FQJSUC"] = 1;
                    }                    
                }
            }
            else
            {
                if (Convert.ToInt32(drBA1["FQJBR"]) > 0)
                {
                    drBA1["FQJSUC"] = 0;
                }
            }
            if (m_txtDeptDirector.Tag != null)
            {
                drBA1["FKZRBH"] = ((clsEmrEmployeeBase_VO)m_txtDeptDirector.Tag).m_strEMPNO_CHR;
                drBA1["FKZR"] = ((clsEmrEmployeeBase_VO)m_txtDeptDirector.Tag).m_strLASTNAME_VCHR;
            }
            if (m_txtSeniorDoctor.Tag != null)
            {
                drBA1["FZRDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtSeniorDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FZRDOCTOR"] = ((clsEmrEmployeeBase_VO)m_txtSeniorDoctor.Tag).m_strLASTNAME_VCHR;
            }
            if (m_txtAttendingDoctor.Tag != null)
            {
                drBA1["FZZDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtAttendingDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FZZDOCT"] = ((clsEmrEmployeeBase_VO)m_txtAttendingDoctor.Tag).m_strLASTNAME_VCHR;
            }
            if (m_txtResidentDoctor.Tag != null)
            {
                drBA1["FZYDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtResidentDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FZYDOCT"] = ((clsEmrEmployeeBase_VO)m_txtResidentDoctor.Tag).m_strLASTNAME_VCHR;
            }
            if (m_txtAdvancedDoctor.Tag != null)
            {
                drBA1["FJXDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtAdvancedDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FJXDOCT"] = ((clsEmrEmployeeBase_VO)m_txtAdvancedDoctor.Tag).m_strLASTNAME_VCHR;                
            }
            else
            {
                drBA1["FJXDOCT"] = m_txtAdvancedDoctor.Text;
            }
            
            if (m_txtGraduateDoctor.Tag != null)
            {
                drBA1["FNURSEBH"] = ((clsEmrEmployeeBase_VO)m_txtGraduateDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FNURSE"] = ((clsEmrEmployeeBase_VO)m_txtGraduateDoctor.Tag).m_strLASTNAME_VCHR;
            }
            else
            {
                drBA1["FNURSE"] = m_txtGraduateDoctor.Text;
            }
            
            if (m_txtInternDoctor.Tag != null)
            {
                drBA1["FSXDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtInternDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FSXDOCT"] = ((clsEmrEmployeeBase_VO)m_txtInternDoctor.Tag).m_strLASTNAME_VCHR;
            }
            else
            {
                drBA1["FSXDOCT"] = m_txtInternDoctor.Text;
            }
            
            if (m_txtCoder.Tag != null)
            {
                drBA1["FBMYBH"] = ((clsEmrEmployeeBase_VO)m_txtCoder.Tag).m_strEMPNO_CHR;
                drBA1["FBMY"] = ((clsEmrEmployeeBase_VO)m_txtCoder.Tag).m_strLASTNAME_VCHR;
            }
            if (m_cboCaseQuality.SelectedItem != null)
            {
                drBA1["FQUALITYBH"] = ((clsGDCaseDictVO)m_cboCaseQuality.SelectedItem).m_strCode;
                drBA1["FQUALITY"] = m_cboCaseQuality.Text;
            }
            if (m_txtQualityControlDoc.Tag != null)
            {
                drBA1["FZKDOCTBH"] = ((clsEmrEmployeeBase_VO)m_txtQualityControlDoc.Tag).m_strEMPNO_CHR;
                drBA1["FZKDOCT"] = ((clsEmrEmployeeBase_VO)m_txtQualityControlDoc.Tag).m_strLASTNAME_VCHR;
            }
            if (m_txtQualityControlNurse.Tag != null)
            {
                drBA1["FZKNURSEBH"] = ((clsEmrEmployeeBase_VO)m_txtQualityControlNurse.Tag).m_strEMPNO_CHR;
                drBA1["FZKNURSE"] = ((clsEmrEmployeeBase_VO)m_txtQualityControlNurse.Tag).m_strLASTNAME_VCHR;
            }

            drBA1["FZKRQ"] = m_dtpQualityControl.Value;

            //su.liang 病理诊断
            drBA1["FPHZD"] = m_txtPathologicalDiagnosis.Text;
            drBA1["FPHZDNUM"] = m_txtPathologyNo.Text;
            drBA1["FPHZDBH"] = m_txtPathologyNumber.Text;
            //离院方式
            if (m_gpbLeavehospital.Tag != null)
            {
                drBA1["FLYFSBH"] = m_gpbLeavehospital.Tag;
                if (m_gpbLeavehospital.Tag.ToString() == "1")
                    drBA1["FLYFS"] = m_rdbLeaveByOrder.Text.ToString();
                else if (m_gpbLeavehospital.Tag.ToString() == "2")
                {
                    drBA1["FLYFS"] = m_rdbReferralByOrder.Text.ToString();
                    drBA1["FYZOUTHOSTITAL"] = m_txtHospitalName.Text.ToString();
                }
                else if (m_gpbLeavehospital.Tag.ToString() == "3")
                {
                    drBA1["FLYFS"] = m_rdbReferralToHospital.Text.ToString();
                    drBA1["FSQOUTHOSTITAL"] = m_txtHospitalName.Text.ToString();
                }
                else if (m_gpbLeavehospital.Tag.ToString() == "4")
                    drBA1["FLYFS"] = m_rdbLeaveNoOrder.Text.ToString();
                else if (m_gpbLeavehospital.Tag.ToString() == "5")
                    drBA1["FLYFS"] = m_rdbLeaveByDead.Text.ToString();
                else
                    drBA1["FLYFS"] = m_rdbOther.Text.ToString();
            }
         
            //31日再入院计划
            if (m_gpbReadmission.Tag != null)
            {
                if (m_gpbReadmission.Tag.ToString() == "2")
                {
                    drBA1["FISAGAINRYBH"] = m_gpbReadmission.Tag;
                    drBA1["FISAGAINRY"] = "有";
                    drBA1["FISAGAINRYMD"] = m_txtReadmissionForWhat.Text.ToString();
                }
                else
                {
                    drBA1["FISAGAINRYBH"] = "1";
                    drBA1["FISAGAINRY"] = "无";
                }
            }
            //颅脑损伤
            //double result = 0.0;
            int IntResult = 0;
            if (int.TryParse(m_txtQday.Text.ToString(), out IntResult))
                drBA1["FRYQHMDAYS"] = IntResult;//m_txtQday.Text.ToString();
            else
                drBA1["FRYQHMDAYS"] = 0;
            //int.TryParse(m_txtQday.Text.ToString(), out IntResult);
            drBA1["FRYQHMCOUNTS"] = IntResult * 60 * 60;

            if (int.TryParse(m_txtQHour.Text.ToString(), out IntResult))
                drBA1["FRYQHMHOURS"] = IntResult;//m_txtQday.Text.ToString();
            else
                drBA1["FRYQHMHOURS"] = 0;
            //drBA1["FRYQHMHOURS"] = m_txtQHour.Text.ToString();
            //double.TryParse(m_txtQHour.Text.ToString(), out result);
            drBA1["FRYQHMCOUNTS"] = Convert.ToDouble(drBA1["FRYQHMCOUNTS"]) + (IntResult * 60);
            if (int.TryParse(m_txtQmin.Text.ToString(), out IntResult))
                drBA1["FRYQHMMINS"] = IntResult;//m_txtQday.Text.ToString();
            else
                drBA1["FRYQHMMINS"] = 0;
            //drBA1["FRYQHMMINS"] = m_txtQmin.Text.ToString();
            //double.TryParse(m_txtQmin.Text.ToString(), out result);
            drBA1["FRYQHMCOUNTS"] = Convert.ToDouble(drBA1["FRYQHMCOUNTS"]) + IntResult;
            //FRYQHMCOUNTS

            if (int.TryParse(m_txtHday.Text.ToString(), out IntResult))
                drBA1["FRYHMDAYS"] = IntResult;
            else
                drBA1["FRYHMDAYS"] = 0;
            //double.TryParse(m_txtHday.Text.ToString(), out result);
            drBA1["FRYHMCOUNTS"] = IntResult * 60 * 60;

            if (int.TryParse(m_txtHhour.Text.ToString(), out IntResult))
                drBA1["FRYHMHOURS"] = IntResult;//m_txtHhour.Text.ToString();
            else
                drBA1["FRYHMHOURS"] = 0;
            //double.TryParse(m_txtHhour.Text.ToString(), out result);
            drBA1["FRYQHMCOUNTS"] = Convert.ToDouble(drBA1["FRYQHMCOUNTS"]) + (IntResult * 60);
            if (int.TryParse(m_txtHmin.Text.ToString(), out IntResult))
                drBA1["FRYHMMINS"] = IntResult;//m_txtHmin.Text.ToString();
            else
                drBA1["FRYHMMINS"] = 0;
            //double.TryParse(m_txtHmin.Text.ToString(), out result);
            drBA1["FRYQHMCOUNTS"] = Convert.ToDouble(drBA1["FRYQHMCOUNTS"]) + IntResult;
            //FRYHMCOUNTSZ
            DataTable dtbBA3 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA3"))
            {
                dtbBA3 = new DataTable();
                dtbBA3.TableName = "HIS_BA3";
                dtbBA3.Columns.Add("fprn");
                dtbBA3.Columns.Add("FTIMES");
                dtbBA3.Columns.Add("FZDLX");
                dtbBA3.Columns.Add("FICDVersion");
                dtbBA3.Columns.Add("FICDM");
                dtbBA3.Columns.Add("FJBNAME");
                dtbBA3.Columns.Add("FRYBQBH");
                dtbBA3.Columns.Add("FRYBQ");
                p_dsCaseContent.Tables.Add(dtbBA3);
            }
            else
            {
                dtbBA3 = p_dsCaseContent.Tables["HIS_BA3"];
            }

            DataRow drDiag = null;
            if (!string.IsNullOrEmpty(m_txtOutMainDiag.Text.Trim()))//主要诊断
            {
                drDiag = dtbBA3.NewRow();
                drDiag["fprn"] = drBA1["fprn"].ToString();
                drDiag["FTIMES"] = drBA1["FTIMES"].ToString();
                drDiag["FZDLX"] = "1";
                drDiag["FICDVersion"] = 10;
                drDiag["FICDM"] = m_txtOutMainDiagICD.Text;
                drDiag["FJBNAME"] = m_txtOutMainDiag.Text;
                if (m_cboOutMainDiagInfo.SelectedItem != null)
                {
                    drDiag["FRYBQBH"] = ((clsGDCaseDictVO)m_cboOutMainDiagInfo.SelectedItem).m_strCode;
                }
                drDiag["FRYBQ"] = m_cboOutMainDiagInfo.Text;
                dtbBA3.Rows.Add(drDiag);
            }
            //if (!string.IsNullOrEmpty(m_txtSubordinateDiag.Text.Trim()))//附属诊断
            //{
            //    drDiag = dtbBA3.NewRow();
            //    drDiag["fprn"] = drBA1["fprn"].ToString();
            //    drDiag["FTIMES"] = drBA1["FTIMES"].ToString();
            //    drDiag["FZDLX"] = "f";
            //    drDiag["FICDVersion"] = 10;
            //    drDiag["FICDM"] = m_txtSubICD.Text;
            //    drDiag["FJBNAME"] = m_txtSubordinateDiag.Text;
            //    if (m_cboSubOutInfo.SelectedItem != null)
            //    {
            //        drDiag["FRYBQBH"] = ((clsGDCaseDictVO)m_cboSubOutInfo.SelectedItem).m_strCode;
            //    }
            //    drDiag["FRYBQ"] = m_cboSubOutInfo.Text;
            //    dtbBA3.Rows.Add(drDiag);
            //}
            if (!string.IsNullOrEmpty(m_txtPoisoningReson.Text.Trim()))//损伤中毒
            {
                drDiag = dtbBA3.NewRow();
                drDiag["fprn"] = drBA1["fprn"].ToString();
                drDiag["FTIMES"] = drBA1["FTIMES"].ToString();
                drDiag["FZDLX"] = "s";
                drDiag["FICDVersion"] = 10;
                drDiag["FICDM"] = m_txtPoisoningResonICD.Text;
                drDiag["FJBNAME"] = m_txtPoisoningReson.Text;
                drDiag["FRYBQBH"] = string.Empty;
                drDiag["FRYBQ"] = string.Empty;
                dtbBA3.Rows.Add(drDiag);
            }
            //其他诊断
            DataTable dtbOutD = m_dgwOutDiagnosis.DataSource as DataTable;
            if (dtbOutD != null && dtbOutD.Rows.Count > 0)
            {
                dtbBA3.BeginLoadData();
                for (int iRow = 0; iRow < dtbOutD.Rows.Count; iRow++)
                {
                    //if (string.IsNullOrEmpty(dtbOutD.Rows[iRow]["code"].ToString()))
                    //{
                    //    continue;
                    //}
                    drDiag = dtbBA3.NewRow();
                    drDiag["fprn"] = drBA1["fprn"].ToString();
                    drDiag["FTIMES"] = drBA1["FTIMES"].ToString();
                    drDiag["FZDLX"] = "2";
                    drDiag["FICDVersion"] = 10;
                    drDiag["FICDM"] = dtbOutD.Rows[iRow]["code"].ToString();
                    drDiag["FJBNAME"] = dtbOutD.Rows[iRow]["name"].ToString();
                    drDiag["FRYBQBH"] = m_strOutInfoCode(dtbOutD.Rows[iRow]["outinfo"].ToString());
                    drDiag["FRYBQ"] = dtbOutD.Rows[iRow]["outinfo"].ToString();
                    dtbBA3.LoadDataRow(drDiag.ItemArray, true);
                }
                dtbBA3.EndLoadData();
            }
            ////院内感染
            //DataTable dtbInfection = m_dgwInfection.DataSource as DataTable;
            //if (dtbInfection != null && dtbInfection.Rows.Count > 0)
            //{
            //    dtbBA3.BeginLoadData();
            //    for (int iRow = 0; iRow < dtbInfection.Rows.Count; iRow++)
            //    {
            //        //if (string.IsNullOrEmpty(dtbInfection.Rows[iRow]["code"].ToString()))
            //        //{
            //        //    continue;
            //        //}
            //        drDiag = dtbBA3.NewRow();
            //        drDiag["fprn"] = drBA1["fprn"].ToString();
            //        drDiag["FTIMES"] = drBA1["FTIMES"].ToString();
            //        drDiag["FZDLX"] = "y";
            //        drDiag["FICDVersion"] = 10;
            //        drDiag["FICDM"] = dtbInfection.Rows[iRow]["code"].ToString();
            //        drDiag["FJBNAME"] = dtbInfection.Rows[iRow]["name"].ToString();
            //        drDiag["FRYBQBH"] = m_strOutInfoCode(dtbInfection.Rows[iRow]["outinfo"].ToString());
            //        drDiag["FRYBQ"] = dtbInfection.Rows[iRow]["outinfo"].ToString();
            //        dtbBA3.LoadDataRow(drDiag.ItemArray, true);
            //    }
            //    dtbBA3.EndLoadData();
            //}
        }

        /// <summary>
        /// 根据疗效名称返回疗效编码
        /// </summary>
        /// <param name="p_strOutInfo">疗效名称</param>
        /// <returns></returns>
        private string m_strOutInfoCode(string p_strOutInfo)
        {
            string strCode = string.Empty;
            switch (p_strOutInfo)
            {
                case "有":
                    strCode = "1";
                    break;
                case "临床未确定":
                    strCode = "2";
                    break;
                case "情况不明":
                    strCode = "3";
                    break;
                case "无":
                    strCode = "4";
                    break;
                default:
                    break;
            }
            return strCode;
        }
        #endregion
        #endregion

        private void m_cmdAddOtherDiag_Click(object sender, EventArgs e)
        {
            DataTable dtbICD = m_dgwOutDiagnosis.DataSource as DataTable;
            if (dtbICD != null && dtbICD.Columns.Count > 0)
            {
                DataRow drNew = dtbICD.NewRow();
                dtbICD.Rows.Add(drNew);
            }
            else
            {
                dtbICD = new DataTable();
                dtbICD.Columns.Add("name");
                dtbICD.Columns.Add("code");
                dtbICD.Columns.Add("outinfo");
                m_dgwOutDiagnosis.DataSource = dtbICD;
            }
            m_dgwOutDiagnosis.CurrentCell = m_dgwOutDiagnosis.Rows[m_dgwOutDiagnosis.Rows.Count - 1].Cells[0];
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            bool blnHasGetICD = objDomain.m_blnAddICDToDataGridView(m_dgwOutDiagnosis);
            if (!blnHasGetICD)
            {
                dtbICD.Rows.RemoveAt(dtbICD.Rows.Count - 1);
            }
            objDomain = null;
        }

        private void m_cmdDeleteOtherDiag_Click(object sender, EventArgs e)
        {
            if (m_dgwOutDiagnosis.SelectedCells.Count > 0)
            {
                DataRowView drCurrent = m_dgwOutDiagnosis.Rows[m_dgwOutDiagnosis.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
                DataTable dtbICD = m_dgwOutDiagnosis.DataSource as DataTable;
                dtbICD.Rows.Remove(drCurrent.Row);
            }
        }

        //private void m_cmdAddInfection_Click(object sender, EventArgs e)
        //{
        //    DataTable dtbICD = m_dgwInfection.DataSource as DataTable;
        //    if (dtbICD != null && dtbICD.Columns.Count > 0)
        //    {
        //        DataRow drNew = dtbICD.NewRow();
        //        dtbICD.Rows.Add(drNew);
        //    }
        //    else
        //    {
        //        dtbICD = new DataTable();
        //        dtbICD.Columns.Add("name");
        //        dtbICD.Columns.Add("code");
        //        dtbICD.Columns.Add("outinfo");
        //        m_dgwInfection.DataSource = dtbICD;
        //    }
        //    m_dgwInfection.CurrentCell = m_dgwInfection.Rows[m_dgwInfection.Rows.Count - 1].Cells[0];
        //    clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
        //    bool blnHasGetICD = objDomain.m_blnAddICDToDataGridView(m_dgwInfection);
        //    if (!blnHasGetICD)
        //    {
        //        dtbICD.Rows.RemoveAt(dtbICD.Rows.Count - 1);
        //    }
        //    objDomain = null;
        //}

        //private void m_cmdDeleteInfection_Click(object sender, EventArgs e)
        //{
        //    if (m_dgwInfection.SelectedCells.Count > 0)
        //    {
        //        DataRowView drCurrent = m_dgwInfection.Rows[m_dgwInfection.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
        //        DataTable dtbICD = m_dgwInfection.DataSource as DataTable;
        //        dtbICD.Rows.Remove(drCurrent.Row);
        //    }
        //}

        private void m_dgwOutDiagnosis_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //取得被表示的控件
            DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
            if (tb == null)
            {
                return;
            }
            //事件处理器删除
            tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);

            //检测对应列
            if (m_dgwOutDiagnosis.CurrentCell.OwningColumn.Name == "clmOutdiagnosis" || m_dgwOutDiagnosis.CurrentCell.OwningColumn.Name == "clmOutDiagICD")
            {
                // KeyDown事件处理器追加
                tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);
            }
        }

        //private void m_dgwInfection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    //取得被表示的控件
        //    DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
        //    if (tb == null)
        //    {
        //        return;
        //    }
        //    //事件处理器删除
        //    tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);

        //    //检测对应列
        //    if (m_dgwInfection.CurrentCell.OwningColumn.Name == "clmInfectionName" || m_dgwInfection.CurrentCell.OwningColumn.Name == "clmInfectionICD")
        //    {
        //        // KeyDown事件处理器追加
        //        tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);
        //    }
        //}

        private void DataGridViewTextBoxCell_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl CurrentCell = sender as System.Windows.Forms.DataGridViewTextBoxEditingControl;
            if (e.KeyCode == Keys.F9 && CurrentCell.EditingControlDataGridView.SelectedCells.Count > 0)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_blnAddICDToDataGridView(CurrentCell.EditingControlDataGridView);
                objDomain = null;
            }
        }

        private void m_txtOutICD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_mthAddICDToTextBox(m_txtOutMainDiag, m_txtOutMainDiagICD);
                objDomain = null;
            }
        }

        //private void m_txtSubordinateICD_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F9)
        //    {
        //        clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
        //        objDomain.m_mthAddICDToTextBox(m_txtSubordinateDiag, m_txtSubICD);
        //        objDomain = null;
        //    }
        //}

        private void m_txtPoisoningICD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_mthAddICDToTextBox(m_txtPoisoningReson, m_txtPoisoningResonICD);
                objDomain = null;
            }
        }

        private void m_txtPathologicalDiagnosis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_mthAddICDToTextBox(m_txtPathologicalDiagnosis, m_txtPathologyNumber);
                objDomain = null;
            }
        }
    }
}
