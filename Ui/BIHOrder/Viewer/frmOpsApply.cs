using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;			//申请单用到
using com.digitalwave.iCare.BIHOrder.Control;
using com.digitalwave.iCare.common;
using com.digitalwave.iCare.gui.HIS;	//申请单用到
using com.digitalwave.iCare.gui.LIS;
using weCare.Core.Entity;
using iCare;
using iCare.CustomForm;					//申请单用到 
using com.digitalwave.iCare.BIHOrder;
using com.digitalwave.Emr.Signature_gui;

namespace iCare.Anaesthesia.Requisition
{
    /// <summary>
    /// 手术申请
    /// </summary>
    public partial class frmOpsApply : com.digitalwave.GUI_Base.frmMDI_Child_Base//Form
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_patVo"></param>
        public frmOpsApply(clsBIHPatientInfo _patVo)
        {
            InitializeComponent();
            patVo = _patVo;

        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 病人信息
        /// </summary>
        clsBIHPatientInfo patVo { get; set; }

        /// <summary>
        /// 术前诊断
        /// </summary>
        string diagDesc { get; set; }

        /// <summary>
        /// 是否分级管理
        /// </summary>
        bool isStepControl { get; set; }

        DataTable dtOps { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                clsPublic.PlayAvi("请稍候...");
                this.lblPatName.Text = patVo.m_strPatientName;
                this.lblAreaName.Text = patVo.m_strAreaName;
                this.lblBedNo.Text = patVo.m_strBedName;
                this.lblIpNo.Text = patVo.m_strInHospitalNo;
                this.lblSex.Text = patVo.m_strSex;
                this.lblAge.Text = patVo.m_strAge;
                this.lblNoticeDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                DataTable dt = null;
                //clsOperationRequisitionService_Oracle svc1 = (clsOperationRequisitionService_Oracle)clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRequisitionService_Oracle));
                (new weCare.Proxy.ProxyIP()).Service.m_lngQueryDiagnosis(patVo.m_strPatientID, out dt);
                //svc1 = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = "inpatient_dat desc";
                    diagDesc = dv[0]["primarydiagnoseall"].ToString().Trim();
                }

                //clsAssistDicService_2 svc2 = (clsAssistDicService_2)clsObjectGenerator.objCreatorObjectByType(typeof(clsAssistDicService_2));
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetOperationName(out dt);
                //svc2 = null;
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    this.txtOpName.DataSource = dt;
                //    this.txtOpName.TextColumnIndex = 1;
                //    this.txtOpName.ValueColumnIndex = 0;
                //    this.txtOpName.AutoMatch = true;
                //    this.txtOpName.MaxDataGridWidth = 1000;
                //}
                this.dtOps = dt;
                if (this.dtOps != null)
                {
                    this.dtOps.Columns[0].ColumnName = "opsCode";
                    this.dtOps.Columns[1].ColumnName = "opsName";
                    this.dtOps.Columns[2].ColumnName = "pyCode";
                }

                clsEmrSignToolCollection tools = new clsEmrSignToolCollection();
                tools.m_mthBindEmployeeSign(this.btnDoctMain, this.txtDoctMain, 1, false);
                tools.m_mthBindEmployeeSign(this.btnDoctAss1, this.txtDoctAss1, 1, false);
                tools.m_mthBindEmployeeSign(this.btnDoctAss2, this.txtDoctAss2, 1, false);
                tools.m_mthBindEmployeeSign(this.btnDoctAss3, this.txtDoctAss3, 1, false);

                //clsBIHOrderService svc3 = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                this.isStepControl = (new weCare.Proxy.ProxyIP()).Service.IsOpStepControl();
                //svc3 = null;

                GetHistory();
                New();
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region GetHistory
        /// <summary>
        /// GetHistory
        /// </summary>
        void GetHistory()
        {
            this.lvHistory.Items.Clear();
            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            DataTable dt = (new weCare.Proxy.ProxyIP()).Service.GetOpHistory(patVo.m_strPatientID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ListViewItem obj = null;
                this.lvHistory.BeginUpdate();
                foreach (DataRow dr in dt.Rows)
                {
                    obj = new ListViewItem();
                    obj.Text = Convert.ToDateTime(dr["opendate_dat"]).ToString("yyyy-MM-dd HH:mm");
                    obj.Tag = dr;
                    this.lvHistory.Items.Add(obj);
                }
                this.lvHistory.EndUpdate();
            }
        }
        #endregion

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        void LoadData(decimal anaId, DateTime openDate)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtMain = null;
                DataTable dtSign = null;
                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                (new weCare.Proxy.ProxyIP()).Service.GetOpRecord(anaId, openDate, out dtMain, out dtSign);
                if (dtMain != null && dtMain.Rows.Count > 0)
                {
                    DataRow dr = dtMain.Rows[0];
                    SetData(dr, dtSign);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region SetData
        /// <summary>
        /// SetData
        /// </summary>
        /// <param name="dr"></param>
        void SetData(DataRow dr, DataTable dtSign)
        {
            #region check
            this.chk101.Checked = false;
            this.chk102.Checked = false;
            this.chk103.Checked = false;
            this.chk104.Checked = false;
            this.chk105.Checked = false;
            this.chk106.Checked = false;
            this.chk107.Checked = false;
            this.chk108.Checked = false;
            this.chk109.Checked = false;
            this.chkSpecPart.Checked = false;
            this.rdo201.Checked = false;
            this.rdo202.Checked = false;
            this.rdo203.Checked = false;
            this.rdo204.Checked = false;
            this.rdo205.Checked = false;
            this.chk301.Checked = false;
            this.chk302.Checked = false;
            this.chk303.Checked = false;
            this.chk304.Checked = false;
            this.chk305.Checked = false;
            this.chkGzhc.Checked = false;
            #endregion

            if (dr == null)
            {
                this.dtpOpDate.Tag = null;
                this.dtpOpDate.Value = DateTime.Now;
                this.lblNoticeDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                this.txtDiag.Text = this.diagDesc;
                this.txtLt.Text = string.Empty;
                this.chkLt.Checked = false;
                this.cboOpLocation.SelectedIndex = 0;
                this.cboOpType.SelectedIndex = 0;
                this.txtOpsName.Text = string.Empty;
                this.cboAnaType.Text = string.Empty;
                this.cboWc.SelectedIndex = 0;
                this.txtWeight.Text = string.Empty;
                this.txtOpPart.Text = string.Empty;
                this.cboOpLevel.SelectedIndex = 0;
                this.cboIsolation.SelectedIndex = 0;
                this.cboWjop.SelectedIndex = 0;
                this.cboZqtys.SelectedIndex = 0;
                this.txtDoctMain.Text = string.Empty;
                this.txtDoctAss1.Text = string.Empty;
                this.txtDoctAss2.Text = string.Empty;
                this.txtDoctAss3.Text = string.Empty;
                this.txtDoctConfirm.Text = string.Empty;
                this.txtDoctVisit.Text = string.Empty;
                this.txtComment.Text = string.Empty;

                this.cboOpLevel.Enabled = true;
                this.lblStatus.Text = string.Empty;
            }
            else
            {
                this.dtpOpDate.Tag = dr;
                this.dtpOpDate.Value = Convert.ToDateTime(dr["operationdate_dat"]);
                this.lblNoticeDate.Text = Convert.ToDateTime(dr["opendate_dat"]).ToString("yyyy-MM-dd HH:mm");
                this.txtDiag.Text = dr["preoperativediagnosis_chr"].ToString();
                this.txtLt.Text = dr["continuedoperation_vchr"].ToString();
                this.chkLt.Checked = dr["iscontinuedoperation_int"] == DBNull.Value ? false : (dr["iscontinuedoperation_int"].ToString() == "1" ? true : false);
                if (dr["anadeptid_chr"].ToString() == "0001")
                    this.cboOpLocation.SelectedIndex = 0;
                else if (dr["anadeptid_chr"].ToString() == "0002")
                    this.cboOpLocation.SelectedIndex = 1;
                else
                    this.cboOpLocation.Text = string.Empty;
                this.cboOpType.Text = dr["emergency_chr"].ToString();
                this.txtOpsName.Text = dr["operationname_chr"].ToString();
                this.cboAnaType.Text = dr["anamode_chr"].ToString();
                //this.cboWc.SelectedIndex = 0;
                this.txtWeight.Text = dr["weight_chr"].ToString();
                this.txtOpPart.Text = dr["operationpart_chr"].ToString();
                this.cboOpLevel.Text = dr["asalevel_chr"].ToString();
                this.cboIsolation.SelectedIndex = Convert.ToInt32(dr["isisolated_int"].ToString());
                this.cboWjop.SelectedIndex = Convert.ToInt32(dr["isaxenic_int"].ToString());
                this.cboZqtys.SelectedIndex = Convert.ToInt32(dr["issignedicf"].ToString());

                this.txtDoctMain.Text = string.Empty;
                this.txtDoctAss1.Text = string.Empty;
                this.txtDoctAss2.Text = string.Empty;
                this.txtDoctAss3.Text = string.Empty;
                this.txtDoctConfirm.Text = string.Empty;
                if (dtSign != null && dtSign.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dtSign.Rows)
                    {
                        if (dr1["tag_chr"].ToString() == "主刀医师")
                            this.txtDoctMain.Text = dr1["employeename_chr"].ToString();
                        else if (dr1["tag_chr"].ToString() == "一助")
                            this.txtDoctAss1.Text = dr1["employeename_chr"].ToString();
                        else if (dr1["tag_chr"].ToString() == "二助")
                            this.txtDoctAss2.Text = dr1["employeename_chr"].ToString();
                        else if (dr1["tag_chr"].ToString() == "三助")
                            this.txtDoctAss3.Text = dr1["employeename_chr"].ToString();
                        else if (dr1["tag_chr"].ToString() == "审批医师")
                            this.txtDoctConfirm.Text = dr1["employeename_chr"].ToString();
                    }
                }

                this.txtDoctVisit.Text = dr["visitor_chr"].ToString();
                this.txtComment.Text = dr["remark_chr"].ToString();
                string statusName = string.Empty;
                // 状态 0 保存，-2退回申请单， 1 审核，-1 取消 ,2已排班，3麻醉计划，4，手术同意书，5病人已签手术同意书，6麻醉记录审核
                switch (Convert.ToInt32(dr["status_int"]))
                {
                    case -1:
                        statusName = "【已取消】";
                        break;
                    case 0:
                        statusName = "【已保存】";
                        break;
                    case 1:
                        statusName = "【已审核】";
                        break;
                    case 2:
                        statusName = "【已排班】";
                        break;
                    default:
                        break;
                }
                this.lblStatus.Text = statusName;
            }

        }
        #endregion

        #region SetCheckValue
        /// <summary>
        /// SetCheckValue
        /// </summary>
        void SetCheckValue()
        {
            string comment = string.Empty;

            string str1 = string.Empty;
            if (this.chk101.Checked)
                str1 += this.chk101.Text + "/";
            if (this.chk102.Checked)
                str1 += this.chk102.Text + "/";
            if (this.chk103.Checked)
                str1 += this.chk103.Text + "/";
            if (this.chk104.Checked)
                str1 += this.chk104.Text + "/";
            if (this.chk105.Checked)
                str1 += this.chk105.Text + "/";
            if (this.chk106.Checked)
                str1 += this.chk106.Text + "/";
            if (this.chk107.Checked)
                str1 += this.chk107.Text + "/";
            if (this.chk108.Checked)
                str1 += this.chk108.Text + "/";
            if (this.chk109.Checked)
                str1 += this.chk109.Text + "/";
            if (str1 != string.Empty) str1 = "特殊病人特异性感染：" + str1.TrimEnd('/') + " ";

            string str2 = string.Empty;
            if (this.chkSpecPart.Checked)
            {
                if (this.rdo201.Checked)
                    str2 += this.rdo201.Text + "/";
                else if (this.rdo202.Checked)
                    str2 += this.rdo202.Text + "/";
                else if (this.rdo203.Checked)
                    str2 += this.rdo203.Text + "/";
                else if (this.rdo204.Checked)
                    str2 += this.rdo204.Text + "/";
                else if (this.rdo205.Checked)
                    str2 += this.rdo205.Text + "/";
                if (str2 != string.Empty) str2 = "特殊体位:" + str2.TrimEnd('/') + " ";
            }

            string str3 = string.Empty;
            if (this.chk301.Checked)
                str3 += this.chk301.Text + "/";
            if (this.chk302.Checked)
                str3 += this.chk302.Text + "/";
            if (this.chk303.Checked)
                str3 += this.chk303.Text + "/";
            if (this.chk304.Checked)
                str3 += this.chk304.Text + "/";
            if (this.chk305.Checked)
                str3 += this.chk305.Text + "/";
            if (str3 != string.Empty) str3 = "备特殊器械：" + str3.TrimEnd('/') + " ";

            string str4 = string.Empty;
            if (this.chkGzhc.Checked)
                str4 = "备高值耗材";

            this.txtComment.Text = str1 + str2 + str3 + str4;
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        void New()
        {
            SetData(null, null);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        void Delete()
        {
            if (this.dtpOpDate.Tag == null)
            {
                SetData(null, null);
            }
            else
            {
                if (MessageBox.Show("删除前请再次确认？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow dr = this.dtpOpDate.Tag as DataRow;
                    Dictionary<string, string> dicData = new Dictionary<string, string>();
                    dicData.Add("AnaId", dr["anaid_int"].ToString());
                    dicData.Add("OpenDate", dr["opendate_dat"].ToString());
                    dicData.Add("SignSequence", dr["signsequence_int"].ToString());
                    try
                    {
                        decimal anaId = 0;
                        DateTime openDate = DateTime.Now;
                        this.Cursor = Cursors.WaitCursor;
                        //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                        int ret = (new weCare.Proxy.ProxyIP()).Service.SaveOpApply(patVo, dicData, out anaId, out openDate, true);
                        //svc = null;
                        if (ret > 0)
                        {
                            this.GetHistory();
                            SetData(null, null);
                            MessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("删除失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "删除异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        void Save()
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            dicData.Add("OpDate", this.dtpOpDate.Value.ToString());
            dicData.Add("NoticeDate", this.lblNoticeDate.Text);
            dicData.Add("Diag", this.txtDiag.Text.Trim());
            dicData.Add("LtStr", this.txtLt.Text.Trim());
            dicData.Add("LtFlag", this.chkLt.Checked ? "1" : "0");
            if (this.cboOpLocation.SelectedIndex == 0)
                dicData.Add("OpLocation", "0001");
            else if (this.cboOpLocation.SelectedIndex == 1)
                dicData.Add("OpLocation", "0002");
            else
                dicData.Add("OpLocation", "");
            dicData.Add("OpType", this.cboOpType.Text);
            dicData.Add("OpName", this.txtOpsName.Text.Trim());
            dicData.Add("AnaType", this.cboAnaType.Text);
            dicData.Add("Weight", this.txtWeight.Text);
            dicData.Add("OpPart", this.txtOpPart.Text);
            dicData.Add("OpLevel", this.cboOpLevel.Text);
            dicData.Add("Isolation", this.cboIsolation.SelectedIndex.ToString());
            dicData.Add("Wjop", this.cboWjop.SelectedIndex.ToString());
            dicData.Add("Zqtys", this.cboZqtys.SelectedIndex.ToString());
            dicData.Add("主刀医师", this.txtDoctMain.Text);
            dicData.Add("一助", this.txtDoctAss1.Text);
            dicData.Add("二助", this.txtDoctAss2.Text);
            dicData.Add("三助", this.txtDoctAss3.Text);
            dicData.Add("审批医师", "");
            dicData.Add("开单医师", LoginInfo.m_strEmpID + "|" + LoginInfo.m_strEmpName);
            dicData.Add("主管医师", patVo.m_strDOCTORID_CHR + "|" + patVo.m_strDOCTOR_VCHR);
            dicData.Add("开单医师ID", LoginInfo.m_strEmpID);
            dicData.Add("审批医师ID", "");
            dicData.Add("开单科室ID", LoginInfo.m_strInpatientAreaID); // .m_strDepartmentID);
            dicData.Add("DoctVisit", this.txtDoctVisit.Text.Trim());
            dicData.Add("Comment", this.txtComment.Text.Trim());

            if (dicData["OpType"] == string.Empty)
            {
                MessageBox.Show("手术类型不能为空", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboOpType.Focus();
                return;
            }
            if (dicData["OpLocation"] == string.Empty)
            {
                MessageBox.Show("手术地点不能为空", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboOpLocation.Focus();
                return;
            }
            if (dicData["OpName"] == string.Empty)
            {
                MessageBox.Show("手术名称不能为空", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtOpsName.Focus();
                return;
            }
            if (dicData["主刀医师"] == string.Empty)
            {
                MessageBox.Show("主刀医师不能为空", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDoctMain.Focus();
                return;
            }

            bool isNew = false;
            if (this.dtpOpDate.Tag != null)
            {
                DataRow dr = this.dtpOpDate.Tag as DataRow;
                dicData.Add("AnaId", dr["anaid_int"].ToString());
                dicData.Add("OpenDate", dr["opendate_dat"].ToString());
                dicData.Add("SignSequence", dr["signsequence_int"].ToString());
                dicData.Add("Status", dr["status_int"].ToString());
            }
            else
            {
                dicData.Add("AnaId", "");
                dicData.Add("OpenDate", "");
                dicData.Add("SignSequence", "");
                dicData.Add("Status", "0");
                isNew = true;
            }

            try
            {
                decimal anaId = 0;
                DateTime openDate = DateTime.Now;
                this.Cursor = Cursors.WaitCursor;
                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                int ret = (new weCare.Proxy.ProxyIP()).Service.SaveOpApply(patVo, dicData, out anaId, out openDate, false);
                //svc = null;
                if (ret > 0)
                {
                    MessageBox.Show("保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (isNew)
                    {
                        this.GetHistory();
                        LoadData(anaId, openDate);
                    }
                }
                else
                {
                    MessageBox.Show("保存失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Confirm
        /// <summary>
        /// Confirm
        /// </summary>
        void Confirm()
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            DataRow dr = null;
            if (this.dtpOpDate.Tag != null)
            {
                dr = this.dtpOpDate.Tag as DataRow;
                dicData.Add("AnaId", dr["anaid_int"].ToString());
                dicData.Add("OpenDate", dr["opendate_dat"].ToString());
                dicData.Add("SignSequence", dr["signsequence_int"].ToString());
                dicData.Add("Status", "1");
            }
            else
            {
                MessageBox.Show("手术申请还未保存，不能审核。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string confirmerID = string.Empty;
            string confirmerName = string.Empty;
            if (clsPublic.m_dlgConfirm(out confirmerID, out confirmerName) != DialogResult.Yes)
            {
                return;
            }
            dicData.Add("OpDate", this.dtpOpDate.Value.ToString());
            dicData.Add("NoticeDate", this.lblNoticeDate.Text);
            dicData.Add("Diag", this.txtDiag.Text.Trim());
            dicData.Add("LtStr", this.txtLt.Text.Trim());
            dicData.Add("LtFlag", this.chkLt.Checked ? "1" : "0");
            if (this.cboOpLocation.SelectedIndex == 0)
                dicData.Add("OpLocation", "0001");
            else if (this.cboOpLocation.SelectedIndex == 1)
                dicData.Add("OpLocation", "0002");
            else
                dicData.Add("OpLocation", "");
            dicData.Add("OpType", this.cboOpType.Text);
            dicData.Add("OpName", this.txtOpsName.Text.Trim());
            dicData.Add("AnaType", this.cboAnaType.Text);
            dicData.Add("Weight", this.txtWeight.Text);
            dicData.Add("OpPart", this.txtOpPart.Text);
            dicData.Add("OpLevel", this.cboOpLevel.Text);
            dicData.Add("Isolation", this.cboIsolation.SelectedIndex.ToString());
            dicData.Add("Wjop", this.cboWjop.SelectedIndex.ToString());
            dicData.Add("Zqtys", this.cboZqtys.SelectedIndex.ToString());
            dicData.Add("主刀医师", this.txtDoctMain.Text);
            dicData.Add("一助", this.txtDoctAss1.Text);
            dicData.Add("二助", this.txtDoctAss2.Text);
            dicData.Add("三助", this.txtDoctAss3.Text);

            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            DataTable dtSign = (new weCare.Proxy.ProxyIP()).Service.GetOpRecordSign(Convert.ToDecimal(dr["signsequence_int"]));
            if (dtSign != null && dtSign.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dtSign.Rows)
                {
                    if (dr2["tag_chr"].ToString().Trim() == "开单医师")
                        dicData.Add("开单医师", dr2["employeeid_chr"].ToString().Trim() + "|" + dr2["employeename_chr"].ToString().Trim());
                    else if (dr2["tag_chr"].ToString().Trim() == "主管医师")
                        dicData.Add("主管医师", dr2["employeeid_chr"].ToString().Trim() + "|" + dr2["employeename_chr"].ToString().Trim());
                }
            }
            if (dicData.ContainsKey("开单医师") == false) dicData.Add("开单医师", LoginInfo.m_strEmpID + "|" + LoginInfo.m_strEmpName);
            if (dicData.ContainsKey("主管医师") == false) dicData.Add("主管医师", patVo.m_strDOCTORID_CHR + "|" + patVo.m_strDOCTOR_VCHR);

            dicData.Add("开单医师ID", dr["createuserid_chr"].ToString());
            dicData.Add("开单科室ID", dr["deptid_chr"].ToString());
            dicData.Add("DoctVisit", this.txtDoctVisit.Text.Trim());
            dicData.Add("Comment", this.txtComment.Text.Trim());
            // 审批医师
            dicData.Add("审批医师", confirmerID + "|" + confirmerName);
            dicData.Add("审批医师ID", confirmerID);

            try
            {
                decimal anaId = 0;
                DateTime openDate = DateTime.Now;
                this.Cursor = Cursors.WaitCursor;
                int ret = (new weCare.Proxy.ProxyIP()).Service.SaveOpApply(patVo, dicData, out anaId, out openDate, false);
                //svc = null;
                if (ret > 0)
                {
                    MessageBox.Show("审批成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(anaId, openDate);
                }
                else
                {
                    MessageBox.Show("审批失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "审批异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Stop
        /// <summary>
        /// Stop
        /// </summary>
        void Stop()
        {
            if (this.dtpOpDate.Tag == null)
            {
                MessageBox.Show("手术申请还未保存，不能取消。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = this.dtpOpDate.Tag as DataRow;
            decimal anaId = Convert.ToDecimal(dr["anaid_int"]);
            DateTime openDate = Convert.ToDateTime(dr["opendate_dat"].ToString());
            if (MessageBox.Show("取消手术前请再次确认？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                    int ret = (new weCare.Proxy.ProxyIP()).Service.CancelOpApply(anaId, openDate, LoginInfo.m_strEmpID);
                    //svc = null;
                    if (ret > 0)
                    {
                        MessageBox.Show("取消手术成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.GetHistory();
                        LoadData(anaId, openDate);
                    }
                    else
                    {
                        MessageBox.Show("取消手术失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "取消手术异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        void Print()
        {
            if (this.dtpOpDate.Tag == null)
            {
                MessageBox.Show("手术申请还未保存，不能打印。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = this.dtpOpDate.Tag as DataRow;
            clsSchedulingPrintTool print = new clsSchedulingPrintTool();
            clsOperationAna operation = new clsOperationAna();
            clsOperationRequisition prtVo = new clsOperationRequisition();
            clsPatientBaseInfo_VO pVo = new clsPatientBaseInfo_VO();

            #region prtVo
            if (dr["approval_date"] != DBNull.Value)
            {
                prtVo.m_dtConfirmdat_dat = Convert.ToDateTime(dr["approval_date"]);
                prtVo.m_intIfconfirm = 1;
                prtVo.m_intIfConfirm = 1;
                prtVo.m_intIscomfim_int = 1;
            }
            prtVo.m_dtmApplyOperationDate = dr["operationdate_dat"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["operationdate_dat"]);    // Convert.ToDateTime("2016-11-10 10:10:00");
            prtVo.m_dtmNoticeDate = Convert.ToDateTime(this.lblNoticeDate.Text);
            prtVo.m_dtmOperationDate = prtVo.m_dtmApplyOperationDate;   //Convert.ToDateTime(dr["operationdate_dat"]);
            prtVo.m_dtmOperationEnd = prtVo.m_dtmOperationDate;     // Convert.ToDateTime("");
            prtVo.m_dtmInPatientDate = dr["inpatientdate_dat"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["inpatientdate_dat"]);
            prtVo.m_dtmCreateDate = Convert.ToDateTime(dr["opendate_dat"]);
            //prtVo.m_dtmDeActivedDate = Convert.ToDateTime("2016-11-12 10:10:00");
            prtVo.m_dtmModifyDate = dr["modifydate_dat"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["modifydate_dat"]);
            prtVo.m_dtmOpenDate = Convert.ToDateTime(dr["opendate_dat"]);
            prtVo.m_dtmOperationEnd = prtVo.m_dtmOpenDate;   //Convert.ToDateTime("2016-11-12 10:10:00");

            prtVo.m_intBacterium = 0;
            prtVo.m_intEmergency = dr["isemergency_int"] == DBNull.Value ? 0 : Convert.ToInt32(dr["isemergency_int"]);
            prtVo.m_intFrom_int = dr["from_int"] == DBNull.Value ? 0 : Convert.ToInt32(dr["from_int"]);
            prtVo.m_intIsContinuedOperation = dr["iscontinuedoperation_int"] == DBNull.Value ? 0 : Convert.ToInt32(dr["iscontinuedoperation_int"]);
            prtVo.m_intISMINIINVASIVE = dr["isminiinvasive_int"] == DBNull.Value ? 0 : Convert.ToInt32(dr["isminiinvasive_int"]);
            prtVo.m_intIsOperationICF = dr["issignedicf"] == DBNull.Value ? 0 : Convert.ToInt32(dr["issignedicf"]);

            prtVo.m_strAge_vchr = dr["age_vchr"].ToString();
            prtVo.m_strAnadeptid_chr = dr["anadeptid_chr"].ToString();
            prtVo.m_strAnaMode = dr["anamode_chr"].ToString();
            prtVo.m_strBedno_vchr = dr["bedno_vchr"].ToString();
            prtVo.m_strConfirmid_vchr = dr["confirmid_vchr"].ToString();
            prtVo.m_strContinuedoperation_vchr = dr["continuedoperation_vchr"].ToString();
            prtVo.m_strDeptID = dr["deptid_chr"].ToString();
            prtVo.m_strDiagnose = dr["preoperativediagnosis_chr"].ToString();
            //prtVo.m_strDiagnoseicd10_vchr = dr[""].ToString();
            prtVo.m_strDiseaseName = dr["diseasename_chr"].ToString();
            prtVo.m_strEmergency = dr["emergency_chr"].ToString();
            if (dr["isisolated_int"] != DBNull.Value)
            {
                if (dr["isisolated_int"].ToString() == "1")
                    prtVo.m_strIsOlationIndicator = "正常";
                else if (dr["isisolated_int"].ToString() == "2")
                    prtVo.m_strIsOlationIndicator = "隔离";
                else if (dr["isisolated_int"].ToString() == "3")
                    prtVo.m_strIsOlationIndicator = "放射";
            }
            //prtVo.m_strOperationcode_vchr = dr[""].ToString();
            prtVo.m_strOperationName = dr["operationname_chr"].ToString();
            prtVo.m_strOperationPart = dr["operationpart_chr"].ToString();
            prtVo.m_strOperationRoomID = dr["operationroomid_chr"].ToString();
            //prtVo.m_strOperationRoomName = dr[""].ToString();
            //prtVo.m_strOperationScale = dr[""].ToString();
            //prtVo.m_strORDERID_CHR = dr[""].ToString();
            prtVo.m_strPatientname_vchr = dr["patientname_vchr"].ToString();
            prtVo.m_strRemark = dr["remark_chr"].ToString();
            prtVo.m_strRollbackreason = dr["rollbackreason"].ToString();
            //prtVo.m_strSequence = dr["signsequence_int"].ToString();
            prtVo.m_strSex_vchr = dr["sex_vchr"].ToString();
            prtVo.m_strSpecialCase = dr["specialcase_chr"].ToString();
            prtVo.m_strVisitor = dr["visitor_chr"].ToString();
            prtVo.m_strWeight = dr["weight_chr"].ToString();
            prtVo.strOPENDATEOriginal = dr["opendate_dat"].ToString();
            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            DataTable dtSign = (new weCare.Proxy.ProxyIP()).Service.GetOpRecordSign(Convert.ToDecimal(dr["signsequence_int"]));
            if (dtSign != null && dtSign.Rows.Count > 0)
            {
                string doctType = string.Empty;
                string doctName = string.Empty;
                clsEmrSigns_VO signVo = null;
                prtVo.objSignerArr = new List<clsEmrSigns_VO>(); //new clsEmrSigns_VO[dtSign.Rows.Count];//List<clsEmrSigns_VO>();
                //for (int j = 0; j < dtSign.Rows.Count; j++)
                //{
                //    doctType = dtSign.Rows[j]["tag_chr"].ToString().Trim();
                //    doctName = dtSign.Rows[j]["employeename_chr"].ToString().Trim();
                //    prtVo.objSignerArr[j].
                //}
                foreach (DataRow dr2 in dtSign.Rows)
                {
                    if (dr2["tag_chr"] != DBNull.Value && dr2["employeename_chr"] != DBNull.Value)
                    {
                        doctType = dr2["tag_chr"].ToString().Trim();
                        doctName = dr2["employeename_chr"].ToString().Trim();
                        signVo = new clsEmrSigns_VO();
                        signVo.controlName = doctType;
                        signVo.objEmployee = new clsEmrEmployeeBase_VO();
                        signVo.objEmployee.m_strLASTNAME_VCHR = doctName;
                        prtVo.objSignerArr.Add(signVo);
                    }
                }
            }
            #endregion

            #region patVo
            pVo.m_datBirthDate = patVo.m_dtBorn;
            pVo.m_datEmrInPatientDate = patVo.m_dtInHospital;
            pVo.m_datInPatientDate = patVo.m_dtInHospital;
            pVo.m_intInPatientCount = 1;
            pVo.m_strAge = patVo.m_strAge;
            pVo.m_strBedID = patVo.m_strBedID;
            pVo.m_strBedName = patVo.m_strBedName;
            pVo.m_strDaptName = patVo.m_strDeptName;
            pVo.m_strDeptID = patVo.m_strDeptID;
            pVo.m_strDeptName = patVo.m_strDeptName;
            pVo.m_strEmrInPatientID = patVo.m_strInHospitalNo;
            pVo.m_strInPatientID = patVo.m_strInHospitalNo;
            pVo.m_strName = patVo.m_strPatientName;
            pVo.m_strPatientCardNO = patVo.m_strPATIENTCARDID_CHR;
            pVo.m_strPatientID = patVo.m_strPatientID;
            pVo.m_strPatientName = patVo.m_strPatientName;
            pVo.m_strRegisterID = patVo.m_strRegisterID;
            pVo.m_strSex = patVo.m_strSex;
            #endregion

            operation.m_objRequisition = prtVo;
            operation.m_objPatient = pVo;
            operation.m_objPatient.m_strDaptName = patVo.m_strDeptName;
            print.m_mthPrintSingle(operation);
            print.isPrintLandscape = true;

        }
        #endregion

        #endregion

        #region 事件

        private void frmOpsApply_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            this.Init();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            New();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                DataRow dr = lvHistory.SelectedItems[0].Tag as DataRow;
                LoadData(Convert.ToDecimal(dr["anaid_int"].ToString()), Convert.ToDateTime(dr["opendate_dat"].ToString()));
            }
            else
            {
                //this.lblCardNo.Tag = string.Empty;
            }
        }

        private void chkLt_CheckedChanged(object sender, EventArgs e)
        {
            this.txtLt.ReadOnly = !this.chkLt.Checked;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region SetComment

        private void chk101_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk102_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk103_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk104_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk105_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk106_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk107_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk108_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk109_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chkSpecPart_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void rdo201_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void rdo202_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void rdo203_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void rdo204_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void rdo205_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk301_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk302_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk303_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk304_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chk305_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }

        private void chkGzhc_CheckedChanged(object sender, EventArgs e)
        {
            this.SetCheckValue();
        }
        #endregion

        #region 分级校验


        private void cboAnaType_Enter(object sender, EventArgs e)
        {
            //txtOpName_Leave(null, null);
        }

        private void dtpOpDate_Enter(object sender, EventArgs e)
        {
            //txtOpName_Leave(null, null);
        }

        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPopupAddOps frm = new frmPopupAddOps(this.dtOps);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(frm.OpsName))
                {
                    string opName = frm.OpsName; // this.txtOpName.Text.Trim();
                    if (opName != string.Empty) // && this.isStepControl)
                    {
                        DataTable dt = (new weCare.Proxy.ProxyIP()).Service.GetOpInfo(opName);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            string fOpjb = dr["fopjb"].ToString().Trim();

                            string rank = this.LoginInfo.m_strTechnicalRank;
                            if (string.IsNullOrEmpty(rank))
                            {
                                MessageBox.Show("很抱歉！您当前在系统中未设置职称，不能开手术申请。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                // '主任医师', '副主任医师', '主治医师', '医师'
                                rank = rank.Trim();
                                if (rank == "主任医师" || rank == "主任中医师")
                                {

                                }
                                else if (rank == "副主任医师" || rank == "副主任中医师")
                                {
                                    if (fOpjb == "四级")
                                    {
                                        MessageBox.Show("很抱歉！您只能申请一、二、三级手术.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else if (rank == "主治医师" || rank == "主治中医师")
                                {
                                    if (fOpjb == "四级" || fOpjb == "三级")
                                    {
                                        MessageBox.Show("很抱歉！您只能申请一、二级手术.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else if (rank == "医师" || rank == "住院医师" || rank == "中医师")
                                {
                                    if (fOpjb != "一级")
                                    {
                                        MessageBox.Show("很抱歉！您只能申请一级手术.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("很抱歉！您不是医师，不能申请手术。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (fOpjb == "一级")
                                    this.cboOpLevel.Text = "I";
                                else if (fOpjb == "二级")
                                    this.cboOpLevel.Text = "II";
                                else if (fOpjb == "三级")
                                    this.cboOpLevel.Text = "III";
                                else if (fOpjb == "四级")
                                    this.cboOpLevel.Text = "IV";
                                this.cboOpLevel.Enabled = false;
                            }

                            string txtOpsName = this.txtOpsName.Text.Trim();
                            if (txtOpsName.IndexOf(opName) < 0)
                            {
                                if (txtOpsName == "")
                                    this.txtOpsName.Text = opName;
                                else
                                    this.txtOpsName.Text = txtOpsName + "+" + opName;
                            }
                        }
                        else
                        {
                            //this.txtOpName.Text = string.Empty;
                            //this.txtOpName.Focus();
                            MessageBox.Show("输入的手术名称不在广东省手术目录中.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        this.cboOpLevel.Enabled = true;
                    }
                }
            }
        }

        private void txtOpsName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAdd_Click(null, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtOpsName.Text = string.Empty;
        }

        #endregion


    }

    public class clsSchedulingPrintTool
    {
        // Fields
        public bool isPrintLandscape;
        private DataTable m_dtReport;
        private DataSet m_dtsRept;
        private clsEmrSigns_VO[] m_lstGetSignArr;
        public string m_strOprationType;
        private string m_strTemplatePath;

        // Methods
        public clsSchedulingPrintTool()
        {
            this.isPrintLandscape = false;
            this.m_strOprationType = null;
            this.m_dtReport = new DataTable();
            this.m_dtsRept = this.m_dtsInitdsAnaesthesiaWorkArrangeDataSet();
            this.m_strTemplatePath = Application.StartupPath + @"\Report\";
            return;
        }

        public string CutStr(string conten, int start, string sSymbol)
        {
            string str;
            char[] chArray;
            int num;
            int num2;
            string str2;
            bool flag;
            str = "";
            chArray = conten.ToCharArray();
            if (chArray.Length <= 0)
            {
                goto Label_007A;
            }
            if (chArray.Length <= start)
            {
                goto Label_0075;
            }
            num = 1;
            num2 = 0;
            goto Label_0066;
        Label_0034:
            str = str + ((char)chArray[num2]);
            if (num2 != start * num)
            {
                goto Label_0061;
            }
            num += 1;
            str = str + sSymbol;
        Label_0061:
            num2 += 1;
        Label_0066:
            if (num2 < chArray.Length)
            {
                goto Label_0034;
            }
            goto Label_0079;
        Label_0075:
            str = conten;
        Label_0079:;
        Label_007A:
            str2 = str;
        Label_007F:
            return str2;
        }

        internal DataSet m_dtsInitdsAnaesthesiaWorkArrangeDataSet()
        {
            DataSet set;
            DataTable table;
            DataColumn column;
            DataColumn column2;
            DataColumn column3;
            DataColumn column4;
            DataColumn column5;
            DataColumn column6;
            DataColumn column7;
            DataColumn column8;
            DataColumn column9;
            DataColumn column10;
            DataColumn column11;
            DataColumn column12;
            DataColumn column13;
            DataColumn column14;
            DataColumn column15;
            DataSet set2;
            set = new DataSet("dsAnaesthesiaWorkArrange");
            table = new DataTable("dtbAnaesthesiaWorkArrange");
            column = new DataColumn("OperationRoomName", typeof(string));
            table.Columns.Add(column);
            column2 = new DataColumn("InPatientName", typeof(string));
            table.Columns.Add(column2);
            column3 = new DataColumn("Sex", typeof(string));
            table.Columns.Add(column3);
            column4 = new DataColumn("Age", typeof(string));
            table.Columns.Add(column4);
            column5 = new DataColumn("InPatientArea", typeof(string));
            table.Columns.Add(column5);
            column6 = new DataColumn("Weight", typeof(string));
            table.Columns.Add(column6);
            column7 = new DataColumn("Diagnose", typeof(string));
            table.Columns.Add(column7);
            column8 = new DataColumn("OperationName", typeof(string));
            table.Columns.Add(column8);
            column9 = new DataColumn("AnaMode", typeof(string));
            table.Columns.Add(column9);
            column10 = new DataColumn("DoctorNameList", typeof(string));
            table.Columns.Add(column10);
            column11 = new DataColumn("other1", typeof(string));
            table.Columns.Add(column11);
            column12 = new DataColumn("other2", typeof(string));
            table.Columns.Add(column12);
            column13 = new DataColumn("other3", typeof(string));
            table.Columns.Add(column13);
            column14 = new DataColumn("other4", typeof(string));
            table.Columns.Add(column14);
            column15 = new DataColumn("other5", typeof(string));
            table.Columns.Add(column15);
            set.Tables.Add(table);
            set2 = set;
        Label_0241:
            return set2;
        }

        private void m_mthCreateReportDatable()
        {
            this.m_dtReport.Rows.Clear();
            this.m_dtReport.Columns.Clear();
            this.m_dtReport.Columns.Add("inpatientid", typeof(string));
            this.m_dtReport.Columns.Add("hospitalname", typeof(string));
            this.m_dtReport.Columns.Add("diagnoise", typeof(string));
            this.m_dtReport.Columns.Add("operationname", typeof(string));
            this.m_dtReport.Columns.Add("anamode", typeof(string));
            this.m_dtReport.Columns.Add("operationdate", typeof(DateTime));
            this.m_dtReport.Columns.Add("remark", typeof(string));
            this.m_dtReport.Columns.Add("operating_no", typeof(string));
            this.m_dtReport.Columns.Add("visitor", typeof(string));
            this.m_dtReport.Columns.Add("notice_dat", typeof(DateTime));
            this.m_dtReport.Columns.Add("lastname_vchr", typeof(string));
            this.m_dtReport.Columns.Add("sex", typeof(string));
            this.m_dtReport.Columns.Add("notice_dept", typeof(string));
            this.m_dtReport.Columns.Add("anadoctor", typeof(string));
            this.m_dtReport.Columns.Add("maindoctor", typeof(string));
            this.m_dtReport.Columns.Add("assistant1", typeof(string));
            this.m_dtReport.Columns.Add("assistant2", typeof(string));
            this.m_dtReport.Columns.Add("notice_doctor", typeof(string));
            this.m_dtReport.Columns.Add("wash_nurse", typeof(string));
            this.m_dtReport.Columns.Add("back_nurse", typeof(string));
            this.m_dtReport.Columns.Add("operationroom", typeof(string));
            this.m_dtReport.Columns.Add("age", typeof(string));
            this.m_dtReport.Columns.Add("t_bse_bed_bed_name", typeof(string));
            this.m_dtReport.Columns.Add("print_dat", typeof(DateTime));
            this.m_dtReport.Columns.Add("operationroom_desc_operationroom", typeof(string));
            this.m_dtReport.Columns.Add("sequence", typeof(string));
            this.m_dtReport.Columns.Add("Anaesthetist", typeof(string));
            this.m_dtReport.Columns.Add("casedoctor", typeof(string));
            this.m_dtReport.Columns.Add("approvdoctor", typeof(string));
            this.m_dtReport.Columns.Add("operation_scale", typeof(string));
            this.m_dtReport.Columns.Add("optime", typeof(DateTime));
            this.m_dtReport.Columns.Add("emergency_chr", typeof(string));
            return;
        }

        internal void m_mthPrintAll(ICollection<clsOperationAna> p_objOperationArr, bool p_blnIsList)
        {
            int num;
            clsOperationAna operation;
            frmNoticePrint print;
            bool flag;
            IEnumerator<clsOperationAna> enumerator;
            this.m_mthCreateReportDatable();
            if (p_objOperationArr != null)
            {
                goto Label_0018;
            }
            goto Label_00A7;
        Label_0018:
            num = p_objOperationArr.Count;
            enumerator = p_objOperationArr.GetEnumerator();
        Label_0028:
            try
            {
                goto Label_004A;
            Label_002A:
                operation = enumerator.Current;
                if (operation == null)
                {
                    goto Label_004A;
                }
                this.m_mthSetOperation2LsvItem(operation, p_blnIsList);
            Label_004A:
                if (enumerator.MoveNext())
                {
                    goto Label_002A;
                }
                goto Label_0069;
            }
            finally
            {
            Label_0057:
                if (enumerator == null)
                {
                    goto Label_0068;
                }
                enumerator.Dispose();
            Label_0068:;
            }
        Label_0069:
            print = new frmNoticePrint();
            if (p_blnIsList == false)
            {
                goto Label_0087;
            }
            print.p_strPBName = "anaesthesia_workarrge";
            goto Label_0094;
        Label_0087:
            print.p_strPBName = "anaesthesia_noticesingleapply";
        Label_0094:
            print.dtRelust = this.m_dtReport;
            print.Show();
        Label_00A7:
            return;
        }

        public void m_mthPrintSingle(clsOperationAna p_objOperation)
        {
            frmNoticePrint print;
            bool flag;
            this.m_mthCreateReportDatable();
            if (p_objOperation == null)
            {
                goto Label_0055;
            }
            this.m_mthSetOperation2LsvItem(p_objOperation, false);
            print = new frmNoticePrint();
            print.p_strPBName = "anaesthesia_noticesingleapply";
            print.dtRelust = this.m_dtReport;
            print.IsPrintLandscape = true;
            print.M_strOprationType = this.m_strOprationType;
            print.Show();
        Label_0055:
            return;
        }

        private void m_mthSetOperation2LsvItem(clsOperationAna m_objCurrentOperation, bool p_blnIsList)
        {
            DataRow row;
            DateTime time;
            int num;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            string str9;
            string str10;
            string str11;
            string str12;
            string str13;
            clsSignHelper helper;
            int num2;
            clsEmrSigns_VO s_vo;
            string str14;
            string str15;
            string str16;
            string str17;
            bool flag;
            IEnumerator<clsEmrSigns_VO> enumerator;
            if (m_objCurrentOperation.m_objRequisition.m_intStatus != 2)
            {
                goto Label_001E;
            }
            goto Label_0744;
        Label_001E:
            row = this.m_dtReport.NewRow();
            time = DateTime.Now;
            if (m_objCurrentOperation.m_objRequisition.m_intIsContinuedOperation != 1)
            {
                goto Label_004B;
            }
            goto Label_006E;
        Label_004B:
            row["operationdate"] = m_objCurrentOperation.m_objRequisition.m_dtmOperationDate.ToString("yyyy-MM-dd HH:mm");
        Label_006E:
            row["operationroom"] = m_objCurrentOperation.m_objRoom == null ? "" : m_objCurrentOperation.m_objRoom.m_strRoomName;
            row["inpatientid"] = m_objCurrentOperation.m_objPatient.m_strInPatientID;
            row["lastname_vchr"] = m_objCurrentOperation.m_objPatient.m_strName;
            row["sex"] = m_objCurrentOperation.m_objPatient.m_strSex;
            num = 0;
            row["age"] = (new weCare.Core.Entity.clsBrithdayToAge()).m_strGetAge(m_objCurrentOperation.m_objPatient.m_datBirthDate);
            row["t_bse_bed_bed_name"] = m_objCurrentOperation.m_objPatient.m_strBedName;
            row["operationname"] = m_objCurrentOperation.m_objRequisition.m_strOperationName;
            row["notice_dept"] = m_objCurrentOperation.m_objPatient.m_strDaptName;
            row["notice_dat"] = (DateTime)m_objCurrentOperation.m_objRequisition.m_dtmCreateDate;
            row["hospitalname"] = "东莞市茶山医院手术通知单";
            row["print_dat"] = time.ToString();
            str = string.Empty;
            str2 = string.Empty;
            str3 = string.Empty;
            str4 = string.Empty;
            str5 = string.Empty;
            str6 = string.Empty;
            str7 = string.Empty;
            str8 = string.Empty;
            str9 = string.Empty;
            str10 = string.Empty;
            str11 = string.Empty;
            str12 = string.Empty;
            str13 = string.Empty;
            if (m_objCurrentOperation.m_objRequisition.objSignerArr == null)
            {
                goto Label_040C;
            }
            helper = new clsSignHelper(m_objCurrentOperation.m_objRequisition.objSignerArr);
            this.m_lstGetSignArr = helper.m_lstGetSigns();
            num2 = 0;
            enumerator = m_objCurrentOperation.m_objRequisition.objSignerArr.GetEnumerator();
        Label_0232:
            try
            {
                goto Label_03E4;
            Label_0237:
                s_vo = enumerator.Current;
                if (this.m_lstGetSignArr[num2].controlName != "主刀医师")
                {
                    goto Label_027C;
                }
                str = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_027C:
                if (this.m_lstGetSignArr[num2].controlName != "开单医师")
                {
                    goto Label_02B8;
                }
                str3 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_02B8:
                if (this.m_lstGetSignArr[num2].controlName != "一助")
                {
                    goto Label_02F4;
                }
                str10 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_02F4:
                if (this.m_lstGetSignArr[num2].controlName != "二助")
                {
                    goto Label_0330;
                }
                str11 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_0330:
                if (this.m_lstGetSignArr[num2].controlName != "三助")
                {
                    goto Label_0369;
                }
                str12 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_0369:
                if (this.m_lstGetSignArr[num2].controlName != "主管医师")
                {
                    goto Label_03A2;
                }
                str13 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_03A2:
                if (this.m_lstGetSignArr[num2].controlName != "审批医师")
                {
                    goto Label_03DB;
                }
                str2 = s_vo.objEmployee.m_strLASTNAME_VCHR;
                num2 += 1;
                goto Label_03E3;
            Label_03DB:
                num2 += 1;
            Label_03E3:;
            Label_03E4:
                if (enumerator.MoveNext())
                {
                    goto Label_0237;
                }
                goto Label_040A;
            }
            finally
            {
            Label_03F6:
                if (enumerator == null)
                {
                    goto Label_0409;
                }
                enumerator.Dispose();
            Label_0409:;
            }
        Label_040A:;
        Label_040C:
            row["maindoctor"] = str;
            if (string.IsNullOrEmpty(str10))
            {
                goto Label_0438;
            }
            str6 = str6 + str10 + " ";
        Label_0438:
            if (string.IsNullOrEmpty(str11))
            {
                goto Label_0457;
            }
            str6 = str6 + str11 + " ";
        Label_0457:
            if (string.IsNullOrEmpty(str12))
            {
                goto Label_0476;
            }
            str6 = str6 + str12 + " ";
        Label_0476:
            row["anamode"] = m_objCurrentOperation.m_objRequisition.m_strAnaMode;
            row["approvdoctor"] = str2;
            row["casedoctor"] = str3;
            row["anadoctor"] = str5;
            row["wash_nurse"] = str7;
            row["back_nurse"] = str8;
            row["Anaesthetist"] = str6;
            row["assistant1"] = str10;
            row["assistant2"] = str11;
            row["notice_doctor"] = str3;
            row["operating_no"] = m_objCurrentOperation.m_objRequisition.m_strSequence;
            str14 = this.CutStr(m_objCurrentOperation.m_objRequisition.m_strDiagnose.Replace("\n", ""), 0x23, "\r\n");
            row["diagnoise"] = str14;
            str15 = string.Empty;
            str16 = string.Empty;
            if (m_objCurrentOperation.m_objRequisition.m_intIfConfirm != 0)
            {
                goto Label_0589;
            }
            str15 = "未审核";
            goto Label_05ED;
        Label_0589:
            if (m_objCurrentOperation.m_objRequisition.m_intIfConfirm != 1)
            {
                goto Label_05AB;
            }
            str15 = "已审核";
            goto Label_05ED;
        Label_05AB:
            if (m_objCurrentOperation.m_objRequisition.m_intIfConfirm != 2)
            {
                goto Label_05CD;
            }
            str15 = "停手术";
            goto Label_05ED;
        Label_05CD:
            if (m_objCurrentOperation.m_objRequisition.m_intIfConfirm != 3)
            {
                goto Label_05ED;
            }
            str15 = "未通过";
        Label_05ED:
            str16 = m_objCurrentOperation.m_objRequisition.m_strEmergency;
            row["emergency_chr"] = str16;
            this.m_strOprationType = str16;
            if (m_objCurrentOperation.m_objRequisition.m_strIsOlationIndicator != "隔离" && m_objCurrentOperation.m_objRequisition.m_strIsOlationIndicator != "放射")
            {
                goto Label_066D;
            }
            row["sequence"] = "★" + m_objCurrentOperation.m_objRequisition.m_strSequence;
            goto Label_0686;
        Label_066D:
            row["sequence"] = m_objCurrentOperation.m_objRequisition.m_strSequence;
        Label_0686:
            row["operation_scale"] = m_objCurrentOperation.m_objRequisition.m_strOperationScale;
            row["visitor"] = m_objCurrentOperation.m_objRequisition.m_strVisitor;
            str17 = this.CutStr(m_objCurrentOperation.m_objRequisition.m_strRemark, 0x26, "\r\n");
            row["remark"] = str17;
            if (p_blnIsList == false)
            {
                goto Label_0714;
            }
            row["assistant1"] = str16;
            row["assistant2"] = str15;
            row["visitor"] = str16;
            goto Label_0716;
        Label_0714:;
        Label_0716:
            row["optime"] = (DateTime)m_objCurrentOperation.m_objRequisition.m_dtmOperationDate;
            this.m_dtReport.Rows.Add(row);
        Label_0744:
            return;
        }
    }

    public class clsSignHelper
    {
        // Fields
        private Dictionary<string, string> m_lstName;
        private Dictionary<string, List<clsEmrSigns_VO>> m_lstSign;
        private Dictionary<string, string> m_lstTag;

        // Methods
        public clsSignHelper(ICollection<clsEmrSigns_VO> p_lstSign)
        {
            bool flag;
            this.m_lstTag = new Dictionary<string, string>();
            this.m_lstName = new Dictionary<string, string>();
            this.m_lstSign = new Dictionary<string, List<clsEmrSigns_VO>>();
            this.m_mthInitTag();
            if (p_lstSign != null)
            {
                goto Label_003E;
            }
            goto Label_0047;
        Label_003E:
            this.m_mthInitSign(p_lstSign);
        Label_0047:
            return;
        }

        public clsEmrSigns_VO[] m_lstGetSigns()
        {
            List<clsEmrSigns_VO> lstData = new List<clsEmrSigns_VO>();
            if (this.m_lstSign != null)
            {
                foreach (KeyValuePair<string, List<clsEmrSigns_VO>> kvp in this.m_lstSign)
                {
                    if (kvp.Value != null)
                    {
                        lstData.AddRange(kvp.Value);
                    }
                }
                return lstData.ToArray();
            }
            else
            {
                return null;
            }
        }

        public ICollection<clsEmrSigns_VO> m_lstGetSigns(string p_strTag)
        {
            ICollection<clsEmrSigns_VO> is2;
            bool flag;
            if (this.m_lstSign.ContainsKey(p_strTag) == false)
            {
                goto Label_0024;
            }
            is2 = this.m_lstSign[p_strTag];
            goto Label_0029;
        Label_0024:
            is2 = null;
        Label_0029:
            return is2;
        }

        public void m_mthChangeSign(string p_strTag, List<clsEmrSigns_VO> p_lstSign)
        {
            bool flag;
            if (this.m_lstSign.ContainsKey(p_strTag) == false)
            {
                goto Label_0026;
            }
            this.m_lstSign[p_strTag] = p_lstSign;
            goto Label_0036;
        Label_0026:
            this.m_lstSign.Add(p_strTag, p_lstSign);
        Label_0036:
            return;
        }

        public void m_mthInitSign(ICollection<clsEmrSigns_VO> p_lstSign)
        {
            string str;
            List<clsEmrSigns_VO> list;
            clsEmrSigns_VO s_vo;
            bool flag;
            IEnumerator<clsEmrSigns_VO> enumerator;
            this.m_lstName.Clear();
            this.m_lstSign.Clear();
            if (this.m_lstSign != null)
            {
                goto Label_002F;
            }
            goto Label_0130;
        Label_002F:
            str = string.Empty;
            list = null;
            enumerator = p_lstSign.GetEnumerator();
        Label_0040:
            try
            {
                goto Label_010D;
            Label_0045:
                s_vo = enumerator.Current;
                if (s_vo.controlName.StartsWith("m_") == false)
                {
                    goto Label_007B;
                }
                str = this.m_lstTag[s_vo.controlName];
                goto Label_0084;
            Label_007B:
                str = s_vo.controlName;
            Label_0084:
                if (this.m_lstSign.ContainsKey(str) == false)
                {
                    goto Label_00D6;
                }
                list = this.m_lstSign[str];
                this.m_lstName[str] = this.m_lstName[str] + "," + s_vo.objEmployee.m_strLASTNAME_VCHR;
                goto Label_0104;
            Label_00D6:
                list = new List<clsEmrSigns_VO>();
                this.m_lstSign.Add(str, list);
                this.m_lstName.Add(str, s_vo.objEmployee.m_strLASTNAME_VCHR);
            Label_0104:
                list.Add(s_vo);
            Label_010D:
                if (enumerator.MoveNext())
                {
                    goto Label_0045;
                }
                goto Label_012F;
            }
            finally
            {
            Label_011D:
                if (enumerator == null)
                {
                    goto Label_012E;
                }
                enumerator.Dispose();
            Label_012E:;
            }
        Label_012F:;
        Label_0130:
            return;
        }

        private void m_mthInitTag()
        {
            this.m_lstTag.Add("m_txtOperationDoctor", "主刀医师");
            this.m_lstTag.Add("m_txtAssistant1", "一助");
            this.m_lstTag.Add("m_txtAssistant2", "二助");
            this.m_lstTag.Add("m_txtAssistant3", "三助");
            this.m_lstTag.Add("m_txtAssistant4", "四助");
            this.m_lstTag.Add("m_lsvAnaesthetist", "麻醉医师");
            this.m_lstTag.Add("m_lsvScrubNurse", "洗手护士");
            this.m_lstTag.Add("m_lsvCirculatingNurse", "巡回护士");
            this.m_lstTag.Add("m_txtApproveDoctor", "审批医师");
            this.m_lstTag.Add("m_txtApplyDoctor", "开单医师");
            this.m_lstTag.Add("m_txtChargeDoctor", "主管医师");
            return;
        }

        public static void m_mthSetSign(clsEmrSigns_VO p_objSign, Control p_ctl)
        {
            string str;
            Control control;
            ListView view;
            ListViewItem item;
            IEnumerator enumerator;
            bool flag;
            IDisposable disposable;
            str = string.Empty;
            enumerator = p_ctl.Controls.GetEnumerator();
        Label_0015:
            try
            {
                goto Label_0150;
            Label_001A:
                control = (Control)enumerator.Current;
                if (p_objSign.controlName.StartsWith("m_") == false)
                {
                    goto Label_004C;
                }
                str = control.Name;
                goto Label_0055;
            Label_004C:
                str = control.AccessibleDescription;
            Label_0055:
                if (str != p_objSign.controlName)
                {
                    goto Label_014F;
                }
                if (control is TextBox)
                {
                    control.Text = p_objSign.objEmployee.m_strLASTNAME_VCHR;
                    control.Tag = p_objSign.objEmployee;
                    goto Label_014D;
                }
                else
                {
                    goto Label_00A6;
                }
            Label_00A6:
                if (control is ListView)
                {
                    view = control as ListView;
                    item = new ListViewItem(p_objSign.objEmployee.m_strLASTNAME_VCHR);
                    item.SubItems.Add(p_objSign.objEmployee.m_strEMPID_CHR);
                    if (!string.IsNullOrEmpty(p_objSign.objEmployee.m_strLEVEL_CHR))
                    {
                        goto Label_0119;
                    }
                    item.SubItems.Add("0");
                    goto Label_0132;
                }
                else
                {
                    goto Label_0160;
                }
            Label_0119:
                item.SubItems.Add(p_objSign.objEmployee.m_strLEVEL_CHR);
            Label_0132:
                item.Tag = p_objSign.objEmployee;
                view.Items.Add(item);
            Label_014D:
                goto Label_0160;
            Label_014F:;
            Label_0150:
                if (enumerator.MoveNext())
                {
                    goto Label_001A;
                }
            Label_0160:
                goto Label_017F;
            }
            finally
            {
            Label_0162:
                disposable = enumerator as IDisposable;
                if ((disposable == null))
                {
                    goto Label_017E;
                }
                disposable.Dispose();
            Label_017E:;
            }
        Label_017F:
            return;
        }

        public string m_strGetRelateTag(string strSignTag)
        {
            string str;
            bool flag;
            if (this.m_lstTag.ContainsKey(strSignTag) == false)
            {
                goto Label_0024;
            }
            str = this.m_lstTag[strSignTag];
            goto Label_0029;
        Label_0024:
            str = strSignTag;
        Label_0029:
            return str;
        }

        public string m_strGetSignName(string p_strTag)
        {
            string str;
            bool flag;
            if (this.m_lstName.ContainsKey(p_strTag) == false)
            {
                goto Label_0024;
            }
            str = this.m_lstName[p_strTag];
            goto Label_002D;
        Label_0024:
            str = string.Empty;
        Label_002D:
            return str;
        }
    }

}
