using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 修改中药代煎属性
    /// </summary>
    public partial class frmModifyProxyType : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmModifyProxyType()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// DataSource
        /// </summary>
        DataTable DataSource { get; set; }

        /// <summary>
        /// Main
        /// </summary>
        DataTable DataMain { get; set; }

        /// <summary>
        /// Det
        /// </summary>
        DataTable DataDet { get; set; }

        #region EntityParm
        /// <summary>
        /// EntityParm
        /// </summary>
        class EntityParm
        {
            public string funcCode { get; set; }
            public string groupNo { get; set; }
            public string opIp { get; set; }        // 1 门诊; 2 住院
            public string recorderId { get; set; }
            public string recipeId { get; set; }
            public string putMedId { get; set; }
        }
        #endregion

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            this.lblPatName.Text = string.Empty;
            this.lblSex.Text = string.Empty;
            this.lblAge.Text = string.Empty;
            this.lblMedSum.Text = string.Empty;
            this.lblDiagDesc.Text = string.Empty;
            this.txtNo.Text = string.Empty;
            this.cboProxy.Text = string.Empty;

            DataMain = new DataTable();
            DataMain.Columns.Add("sendstatus");
            DataMain.Columns.Add("recipedate");
            DataMain.Columns.Add("patname");
            DataMain.Columns.Add("cardno");
            DataMain.Columns.Add("recipeno");
            DataMain.Columns.Add("recipeid");
            DataMain.Columns.Add("deptname");
            DataMain.Columns.Add("doctname");
            DataMain.Columns.Add("bedno");
            DataMain.Columns.Add("putmedid");
            DataMain.Columns.Add("sex");
            DataMain.Columns.Add("birthday");
            DataMain.Columns.Add("diagdesc");

            DataDet = new DataTable();
            DataDet.Columns.Add("medcode");
            DataDet.Columns.Add("medname");
            DataDet.Columns.Add("spec");
            DataDet.Columns.Add("usageName");
            DataDet.Columns.Add("qty");
            DataDet.Columns.Add("unit");
            DataDet.Columns.Add("times");
            DataDet.Columns.Add("price");
            DataDet.Columns.Add("total");
            DataDet.Columns.Add("recipeid");
        }
        #endregion

        #region Query
        /// <summary>
        /// Query
        /// </summary>
        void Query()
        {
            try
            {
                int opIp = this.rdoOp.Checked ? 1 : 2;
                if (opIp == 1)
                {
                    this.gvMain.Columns["cardno"].HeaderText = "卡号";
                    this.gvMain.Columns["bedno"].Visible = false;
                }
                else if (opIp == 2)
                {
                    this.gvMain.Columns["cardno"].HeaderText = "住院号";
                    this.gvMain.Columns["bedno"].Visible = true;
                }
                string startDate = this.dteStart.Value.ToString("yyyy-MM-dd");
                string endDate = this.dteEnd.Value.ToString("yyyy-MM-dd");
                if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
                {
                    MessageBox.Show("开始日期不能大于结束日期。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string no = this.txtNo.Text.Trim();
                if (string.IsNullOrEmpty(no))
                {
                    MessageBox.Show(string.Format("请输入{0}。", this.rdoOp.Checked ? "诊疗卡号" : "住院号"), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNo.Focus();
                    return;
                }

                clsPublic.PlayAvi("查询数据，请稍候...");
                DataMain.Rows.Clear();
                DataDet.Rows.Clear();
                clsDomainControlOPMedStore svc = new clsDomainControlOPMedStore();
                DataTable dt = svc.QueryChinaMedRecipe(startDate, endDate, (this.rdoOp.Checked ? 1 : 2), no);
                svc = null;
                string checkId = string.Empty;
                this.DataSource = dt;
                List<string> lstCheckId = new List<string>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (this.rdoOp.Checked)             // 门诊时recipeId为处方Id
                            checkId = dr["recipeid"].ToString();
                        else if (this.rdoIp.Checked)        // 住院时recipeId为registerId
                            checkId = dr["recipeid"].ToString() + " " + dr["recipeno"].ToString() + Convert.ToDateTime(dr["recipedate"].ToString()).ToString("yyyy-MM-dd");
                        if (lstCheckId.IndexOf(checkId) < 0)
                        {
                            lstCheckId.Add(checkId);
                        }
                        else
                        {
                            continue;
                        }
                        DataRow drNew = DataMain.NewRow();
                        if (dr["sendstatus"] == DBNull.Value)
                        {
                            drNew["sendstatus"] = "药房";
                        }
                        else
                        {
                            switch (Convert.ToInt32(dr["sendstatus"]))
                            {
                                case 0:
                                    drNew["sendstatus"] = "药房";
                                    break;
                                case 1:
                                    drNew["sendstatus"] = "代煎代送";
                                    break;
                                case 2:
                                    drNew["sendstatus"] = "中药代送";
                                    break;
                                default:
                                    drNew["sendstatus"] = "药房";
                                    break;
                            }
                        }
                        drNew["recipedate"] = dr["recipedate"].ToString();
                        drNew["patname"] = dr["patname"].ToString();
                        drNew["recipeid"] = dr["recipeid"].ToString();
                        drNew["deptname"] = (this.rdoOp.Checked ? dr["deptname"].ToString() : dr["areaname"].ToString());
                        drNew["doctname"] = dr["doctname"].ToString();
                        drNew["recipeno"] = dr["recipeno"].ToString();
                        if (opIp == 1)
                        {
                            drNew["cardno"] = dr["cardno"].ToString();
                            drNew["bedno"] = "";
                        }
                        else if (opIp == 2)
                        {
                            drNew["cardno"] = dr["ipno"].ToString();
                            drNew["bedno"] = dr["bedno"].ToString();
                        }
                        drNew["putmedid"] = dr["putmedid"].ToString();
                        drNew["sex"] = dr["sex"].ToString();
                        drNew["birthday"] = dr["birthday"].ToString();
                        drNew["diagdesc"] = dr["diagdesc"].ToString();

                        DataMain.Rows.Add(drNew);
                    }
                    DataMain.AcceptChanges();
                    this.gvMain.DataSource = DataMain;

                    if (DataMain.Rows.Count > 0 && DataDet.Rows.Count == 0)
                    {
                        ShowMedDetail(0);
                    }

                    for (int i = 0; i < this.gvMain.RowCount; i++)
                    {
                        if (this.gvMain.Rows[i].Cells["sendstatus"].Value.ToString() == "代煎代送")
                        {
                            this.gvMain.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(103, 202, 103);
                        }
                        else if (this.gvMain.Rows[i].Cells["sendstatus"].Value.ToString() == "中药代送")
                        {
                            this.gvMain.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                clsPublic.CloseAvi();
            }
        }
        #endregion

        #region ShowMedDetail
        /// <summary>
        /// ShowMedDetail
        /// </summary>
        /// <param name="rowIndex"></param>
        void ShowMedDetail(int rowIndex)
        {
            if (rowIndex < 0) return;
            string recipeId = string.Empty;
            string recipeNo = string.Empty;
            string recipeDate = string.Empty;
            if (this.rdoOp.Checked)
            {
                recipeId = this.gvMain.Rows[rowIndex].Cells["recipeid1"].Value.ToString();
            }
            else if (this.rdoIp.Checked)
            {
                recipeId = this.gvMain.Rows[rowIndex].Cells["recipeid1"].Value.ToString();
                recipeNo = this.gvMain.Rows[rowIndex].Cells["recipeno"].Value.ToString();
                recipeDate = this.gvMain.Rows[rowIndex].Cells["recipedate"].Value.ToString();
            }
            this.lblPatName.Text = this.gvMain.Rows[rowIndex].Cells["patname"].Value.ToString();
            this.lblSex.Text = this.gvMain.Rows[rowIndex].Cells["sex"].Value.ToString();
            this.lblAge.Text = clsConvertDateTime.CalcAge(Convert.ToDateTime(this.gvMain.Rows[rowIndex].Cells["birthday"].Value.ToString()));
            this.lblDiagDesc.Text = this.gvMain.Rows[rowIndex].Cells["diagdesc"].Value.ToString();
            this.lblMedSum.Text = string.Empty;

            clsDomainControlOPMedStore svc = new clsDomainControlOPMedStore();
            DataTable dt = svc.QueryProxyBoilMedDet(recipeId, recipeNo, recipeDate, this.rdoOp.Checked ? 1 : 2);
            svc = null;
            DataDet.Rows.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                decimal medSum = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = DataDet.NewRow();
                    drNew["medcode"] = dr["medcode"].ToString();
                    drNew["medname"] = dr["medname"].ToString();
                    drNew["spec"] = dr["spec"].ToString();
                    drNew["usageName"] = dr["usageName"] == DBNull.Value ? "" : dr["usageName"].ToString().Trim();
                    drNew["qty"] = dr["qty"].ToString();
                    drNew["unit"] = dr["unit"] == DBNull.Value ? "" : dr["unit"].ToString().Trim();
                    drNew["times"] = dr["times"].ToString();
                    drNew["price"] = dr["price"].ToString();
                    drNew["total"] = dr["total"].ToString();
                    drNew["recipeid"] = dr["recipeid"].ToString();
                    DataDet.Rows.Add(drNew);
                    medSum += Convert.ToDecimal(dr["total"].ToString());
                }
                DataDet.AcceptChanges();
                this.gvDetail.DataSource = DataDet;
                this.lblMedSum.Text = medSum.ToString("0.00") + "元";

                for (int i = 0; i < this.gvDetail.RowCount; i++)
                {
                    if (Math.IEEERemainder(Convert.ToDouble(i + 1), 2) == 0)
                    {
                        this.gvDetail.Rows[i].DefaultCellStyle.BackColor = clsPublic.CustomBackColor;
                    }
                }
            }
        }
        #endregion

        #region ModifyProxyType
        /// <summary>
        /// ModifyProxyType
        /// </summary>
        void ModifyProxyType()
        {
            string proxyTypeName = this.cboProxy.Text;
            if (proxyTypeName == string.Empty)
            {
                MessageBox.Show("请选择需要修改的药品代煎属性。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int proxyTypeId = 0;
            if (proxyTypeName == "代煎代送")
                proxyTypeId = 1;
            else if (proxyTypeName == "中药代送")
                proxyTypeId = 2;
            else
                proxyTypeId = 0;
            if (this.gvMain.SelectedRows.Count > 0)
            {
                EntityParm vo = null;
                List<EntityParm> lstParm = new List<EntityParm>();
                for (int i = 0; i < this.gvMain.SelectedRows.Count; i++)
                {
                    if (this.gvMain.SelectedRows[i].Cells["sendstatus"].Value.ToString() == "药房")
                    {
                        vo = new EntityParm();
                        vo.funcCode = "add";
                        vo.groupNo = this.gvMain.SelectedRows[i].Cells["recipeno"].Value.ToString();
                        vo.opIp = this.rdoOp.Checked ? "1" : "2";        // 1 门诊; 2 住院
                        vo.recorderId = this.LoginInfo.m_strEmpID;
                        vo.recipeId = this.gvMain.SelectedRows[i].Cells["recipeid1"].Value.ToString();
                        vo.putMedId = this.rdoOp.Checked ? this.gvMain.SelectedRows[i].Cells["putmedid"].Value.ToString() : this.GetPutMedId(vo.recipeId, vo.groupNo);
                        lstParm.Add(vo);
                    }
                }
                if (lstParm.Count == 0)
                {
                    MessageBox.Show("请选择需要修改的药品处方。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string groupNos = string.Empty;
                string recipeIds = string.Empty;
                string putMedIds = string.Empty;
                foreach (EntityParm item in lstParm)
                {
                    groupNos += item.groupNo + ",";
                    putMedIds += item.putMedId + ",";
                    recipeIds += "'" + item.recipeId + "',";
                }
                putMedIds = putMedIds.TrimEnd(',');
                recipeIds = recipeIds.TrimEnd(',');
                clsDomainControlOPMedStore svc = new clsDomainControlOPMedStore();
                int affectRows = svc.ModifyProxyBoilMedType((this.rdoOp.Checked ? 1 : 2), proxyTypeId, (this.rdoOp.Checked ? recipeIds : putMedIds));
                svc = null;
                if (affectRows > 0)
                {
                    MessageBox.Show("修改药品处方代煎属性成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Query();
                }
                else
                {
                    MessageBox.Show("修改药品处方代煎属性失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请选择需要修改的药品处方。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region GetPutMedId
        /// <summary>
        /// GetPutMedId
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        string GetPutMedId(string registerId, string recipeNo)
        {
            string putMedIds = string.Empty;
            if (string.IsNullOrEmpty(registerId))
            {
                return putMedIds;
            }
            if (this.DataSource == null || this.DataSource.Rows.Count == 0)
            {
                return putMedIds;
            }
            DataRow[] drr = this.DataSource.Select(string.Format("recipeid = '{0}' and recipeno = {1}", registerId, recipeNo));
            if (drr != null && drr.Length > 0)
            {
                foreach (DataRow dr in drr)
                {
                    putMedIds += dr["putmedid"] == DBNull.Value ? "" : "'" + dr["putmedid"].ToString() + "',";
                }
                putMedIds = putMedIds.TrimEnd(',');
            }
            return putMedIds;
        }
        #endregion

        #endregion

        #region 事件

        private void frmModifyProxyType_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void gvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.ShowMedDetail(e.RowIndex);
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.rdoOp.Checked)
                {
                    string no = this.txtNo.Text.Trim();
                    if (no != string.Empty)
                    {
                        this.txtNo.Text = no.PadLeft(10, '0');
                    }
                }
            }
        }

        private void rdoOp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOp.Checked)
            {
                this.lblType.Text = "诊疗卡号：";
                this.txtNo.Text = "";
                this.txtNo.Focus();
            }
        }

        private void rdoIp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoIp.Checked)
            {
                this.lblType.Text = "住院号：";
                this.txtNo.Text = "";
                this.txtNo.Focus();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            this.ModifyProxyType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
