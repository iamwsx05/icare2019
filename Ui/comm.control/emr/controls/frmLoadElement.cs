using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Entity;
using Common.Itf;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    public partial class frmLoadElement : System.Windows.Forms.Form
    {
        DevExpress.XtraEditors.TextEdit txtNew = null;
        /// <summary>
        /// 标志(0 - 新增,1 - 修改)
        /// </summary>
        private int m_intFlags = 0;

        public frmLoadElement()
        {
            InitializeComponent();
        }

        public frmLoadElement(string p_strElementID, string p_strElementName, string p_strCaseCode)
        {
            InitializeComponent();
            m_strElementID = p_strElementID;
            m_strElementName = p_strElementName;
            m_strCaseCode = p_strCaseCode;
            m_blnEdit = true;
        }

        public bool TemplateFlag { get; set; }
        public string CaseCode { get; set; }
        public string ElementID { get; set; }
        public string ElementName { get; set; }
        public EntityDragRichItem DragRichItem { get; set; }
        private string m_strCaseCode = string.Empty;
        private string m_strElementID = string.Empty;
        private string m_strElementName = string.Empty;
        private bool m_blnEdit = false;
        private Hashtable m_hasElement = new Hashtable();
        private void m_mthLoadData()
        {
            bool blnSingle = true;
            string strCaseCode = GlobalCase.caseInfo.CaseCode;
            if (!TemplateFlag && !string.IsNullOrEmpty(this.m_strCaseCode) && this.m_strCaseCode != GlobalCase.caseInfo.CaseCode)
            {
                blnSingle = false;
                strCaseCode = "'" + GlobalCase.caseInfo.CaseCode + "', '" + this.m_strCaseCode + "'";
            }
            EntityElementTemplate vo = new EntityElementTemplate();
            vo.status = 1;
            if (blnSingle)
                vo.caseCode = strCaseCode;
            else
                vo.caseCode = "in (" + strCaseCode + ")";
            ProxyEntityFactory proxy = new ProxyEntityFactory();
            DataTable dt = proxy.Service.Select(vo, new List<string> { EntityElementTemplate.Columns.caseCode, EntityElementTemplate.Columns.status });
            proxy = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!this.m_hasElement.ContainsKey(dr[EntityElementTemplate.Columns.serno].ToString()))
                    {
                        this.m_hasElement.Add(dr[EntityElementTemplate.Columns.serno].ToString(), dr[EntityElementTemplate.Columns.caseCode].ToString());
                    }
                }
            }
            this.ctlPopupSelect.DataSource = dt;
        }

        List<EntityElementTemplate> lstTemplateSource { get; set; }
        private void m_mthFilter(int p_intElementID)
        {
            ProxyCommon proxy = new ProxyCommon();
            lstTemplateSource = proxy.Service.GetElementTemplate(p_intElementID);
            proxy = null;
            if (lstTemplateSource != null)
            {
                foreach (EntityElementTemplate obj in lstTemplateSource)
                {
                    obj.pyCode = SpellCodeHelper.GetPyCode(obj.colcontent);
                    obj.wbCode = SpellCodeHelper.GetWbCode(obj.colcontent);
                }
                BindingListView<EntityElementTemplate> bindingSource = new BindingListView<EntityElementTemplate>();
                bindingSource.AddRange(lstTemplateSource);
                this.clstElement.DisplayMember = EntityElementTemplate.Columns.colcontent;
                this.clstElement.ValueMember = EntityElementTemplate.Columns.serno;
                this.clstElement.DataSource = bindingSource;
                for (int i = 0; i < bindingSource.Count; i++)
                {
                    this.clstElement.SetItemChecked(i, false);
                }
            }
            else
            {
                this.clstElement.DataSource = null;
            }
        }

        private void m_mthScroll(int p_intType)
        {
            //使用ItemCount而不是Items.Count,因为用数据源方式后者为0
            int max = this.clstElement.ItemCount;

            if (max == 0) return;
            max--;
            int idx = 0;
            if (this.clstElement.SelectedItems.Count > 0)
            {
                idx = this.clstElement.SelectedIndex;
                if (p_intType == 1) // UP
                {
                    if (idx == 0)
                        idx = max;
                    else
                        idx--;
                }
                else if (p_intType == 2) // Down
                {
                    if (idx == max)
                        idx = 0;
                    else
                        idx++;
                }
            }
            this.clstElement.SelectedIndex = idx;
        }

        private void m_mthSelect()
        {
            if (this.ctlPopupSelect.Value == null) return;
            this.ElementID = this.ctlPopupSelect.Value.ToString();
            this.TemplateFlag = false;
            if (this.TemplateFlag)
            {
                if (this.ctlPopupSelect.CurrentRow != null)
                    this.ElementName = this.ctlPopupSelect.CurrentRow[EntityElementTemplate.Columns.templateName].ToString();
                else
                    return;
            }
            else
            {
                BindingListView<EntityElementTemplate> lisDCElement = this.clstElement.DataSource as BindingListView<EntityElementTemplate>;

                if (lisDCElement == null)
                {
                    return;
                }

                if (this.m_blnMultiChooseFlag)
                {
                    if (this.txtElement.Text.Trim() == string.Empty) return;
                    this.ElementName = this.txtElement.Text.Trim();
                }
                else
                {
                    if (this.txtElement.Text.Trim() == string.Empty) return;
                    this.ElementName = this.txtElement.Text.Trim();

                    if (txtContent_vchr.Tag != null)
                    {
                        EntityDragRichItem dragRichItem = new EntityDragRichItem();
                        dragRichItem.DragString = txtContent_vchr.Text;
                        dragRichItem.DragMedicalTerm = txtContent_vchr.LstMedicalTerm;
                        this.DragRichItem = dragRichItem;
                    }
                }
                if (this.m_hasElement.ContainsKey(this.ElementID))
                {
                    this.CaseCode = this.m_hasElement[this.ElementID].ToString();
                }
                else
                {
                    this.CaseCode = GlobalCase.caseInfo.CaseCode;
                }
            }

            if (this.m_strElementName.Replace("[", "").Replace("]", "") == this.ElementName && (GlobalCase.caseInfo != null && !string.IsNullOrEmpty(GlobalCase.caseInfo.EmpNo)))
            {
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 多选标志
        /// </summary>
        private bool m_blnMultiChooseFlag = false;

        /// <summary>
        /// 设置多选标志
        /// </summary>
        private void m_mthSetMultiChooseFlag()
        {
            this.m_blnMultiChooseFlag = false;

            if (this.ctlPopupSelect.CurrentRow != null)
            {
                if (this.ctlPopupSelect.CurrentRow[EntityElementTemplate.Columns.multiFlag].ToString() == "1")
                {
                    this.m_blnMultiChooseFlag = true;
                }
            }

            if (this.m_blnMultiChooseFlag)
            {
                this.biMultiChoose.Caption = "确定";//"多选";
            }
            else
            {
                this.biMultiChoose.Caption = "确定";
            }
        }

        private void frmLoadElement_Load(object sender, EventArgs e)
        {
            try
            {
                this.DragRichItem = null;
                Cursor = Cursors.WaitCursor;
                pnlAddElement.Visible = false;
                this.m_mthLoadData();
                if (this.m_blnEdit)
                {
                    int intID = int.Parse(m_strElementID);
                    this.ctlPopupSelect.Value = intID;
                    this.m_mthFilter(intID);
                    this.m_mthSetMultiChooseFlag();

                    bool blnStatus = this.m_blnInitSet(m_strElementName);
                    if (blnStatus == false)
                    {
                        string[] strValArr = this.m_strElementName.Split(',');
                        foreach (string str in strValArr)
                        {
                            this.m_blnInitSet(str);
                        }
                    }
                }
                this.ctlPopupSelect.EnterMoveNextControl = false;

                this.m_mthVerify();

                if (!string.IsNullOrEmpty(this.m_strElementName.Trim()))
                {
                    txtElement.Text = this.m_strElementName;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 验证维护元素的权限
        /// </summary>
        private void m_mthVerify()
        {
            bool blnStatus = false;
            string strValue = string.Empty;
            if (GlobalParm.dicSysParameter.ContainsKey(14))
                strValue = GlobalParm.dicSysParameter[14];

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] strIDArr = strValue.Split(';');
                foreach (string strID in strIDArr)
                {
                    if (GlobalLogin.objLogin.lstRoleID.IndexOf(strID) >= 0)
                    {
                        blnStatus = true;
                        break;
                    }
                }
            }

            if (!blnStatus)
            {
                this.biAdd.Enabled = false;
                this.biDel.Enabled = false;
                this.biEdit.Enabled = false;
            }
        }

        private bool m_blnInitSet(string p_strElementName)
        {
            bool blnStatus = false;
            p_strElementName = p_strElementName.Replace("[", "").Replace("]", "");

            BindingListView<EntityElementTemplate> lisDCElement = this.clstElement.DataSource as BindingListView<EntityElementTemplate>;

            if (lisDCElement == null)
            {
                return false;
            }

            for (int i = 0; i < lisDCElement.Count; i++)
            {
                if (lisDCElement[i].colcontent == p_strElementName)
                {
                    this.clstElement.SelectedIndex = i;
                    this.clstElement.SetItemChecked(i, true);
                    blnStatus = true;
                    break;
                }
            }

            return blnStatus;
        }

        private void frmLoadElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void clstElement_DoubleClick(object sender, EventArgs e)
        {
            this.m_mthSelect();
        }

        private void ctlPopupSelect_AfterValueChanged(object sender, ctlPopupSelect.ValueChangedEventAgrs args)
        {
            if (this.ctlPopupSelect.Value != null)
            {
                this.m_mthFilter(Convert.ToInt32(this.ctlPopupSelect.Value.ToString()));
                this.m_mthSetMultiChooseFlag();
            }
            else
            {
                this.clstElement.DataSource = null;
            }
            this.txtElement.Text = string.Empty;
        }

        private void clstElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.m_mthSelect();
            //else if (e.KeyCode == Keys.Up)
            //    this.m_mthScroll(1);
            //else if (e.KeyCode == Keys.Down)
            //    this.m_mthScroll(2);
        }

        private void ctlPopupSelect_ExternalKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                this.m_mthScroll(1);
            else if (e.KeyCode == Keys.Down)
                this.m_mthScroll(2);
            else if (e.KeyCode == Keys.Enter)
                this.m_mthSelect();
        }

        private void biAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctlPopupSelect.Value == null)
            {
                pnlAddElement.Visible = true;
                txtTemplateName.Focus();
            }
            else
            {
                AddElement();
            }
        }

        private void AddElement()
        {
            BindingListView<EntityElementTemplate> source = clstElement.DataSource as BindingListView<EntityElementTemplate>;
            if (source != null)
            {
                BindingListView<EntityElementTemplate> tempSource = new BindingListView<EntityElementTemplate>();
                EntityElementTemplate objNew = new EntityElementTemplate();
                objNew.serno = -1;
                objNew.colcontent = "    ";
                tempSource.Add(objNew);
                foreach (EntityElementTemplate objCurrent in source)
                {
                    tempSource.Add(objCurrent);
                }
                clstElement.DataSource = tempSource;
            }

            biAdd.Enabled = false;
            biDel.Enabled = false;
            biMultiChoose.Enabled = false;
            biEdit.Enabled = false;

            txtNew = new DevExpress.XtraEditors.TextEdit();
            txtNew.Name = "txtNew";
            int intCount = clstElement.ItemCount;
            int intHeight = (intCount + 1) * clstElement.ItemHeight;

            txtNew.Location = new Point(0, 0);
            if (clstElement.Height <= intHeight)
            {
                txtNew.Width = clstElement.Width - 17;
            }
            else
            {
                txtNew.Width = clstElement.Width;
            }
            txtNew.Height = clstElement.ItemHeight;
            txtNew.KeyDown += new KeyEventHandler(txtNew_KeyDown);
            txtNew.Leave += new EventHandler(txtNew_Leave);


            clstElement.Controls.Add(txtNew);
            txtNew.BringToFront();
            txtNew.Focus();

        }

        private void RemoveTextEdit(bool p_blnFlags)
        {
            if (txtNew != null)
            {
                txtNew.Leave -= new EventHandler(txtNew_Leave);
                clstElement.Controls.Remove(txtNew);

                if (p_blnFlags)
                {
                    BindingListView<EntityElementTemplate> Source = clstElement.DataSource as BindingListView<EntityElementTemplate>;
                    if (Source != null && Source.Count > 0)
                    {
                        Source.RemoveAt(0);
                    }
                }
            }
            txtNew = null;
            biAdd.Enabled = true;
            biDel.Enabled = true;
            biEdit.Enabled = true;
            biMultiChoose.Enabled = true;
        }

        private bool m_blnSaveNewData()
        {
            string strContent = txtNew.Text.Trim();
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }
            EntityElementTemplateContent objNewContent = new EntityElementTemplateContent();
            objNewContent.status = 1;
            objNewContent.elementId = Convert.ToInt32(this.ctlPopupSelect.Value.ToString());
            objNewContent.colContent = strContent;

            ProxyEntityFactory proxy = new ProxyEntityFactory();
            int intResult = proxy.Service.Insert(objNewContent);
            proxy = null;
            if (intResult > 0)
            {
                DialogBox.Msg("保存成功!");
                m_mthRefresh();
                return true;
            }
            else
            {
                DialogBox.Msg("保存失败.");
                return false;
            }
        }

        private bool m_blnUpdateOldData()
        {
            string strContent = txtNew.Text.Trim();
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }
            if (txtNew.Tag == null)
            {
                return false;
            }

            EntityElementTemplateContent vo = new EntityElementTemplateContent();
            vo.colContent = strContent;
            vo.serNo = Function.Dec(txtNew.Tag.ToString());

            ProxyEntityFactory proxy = new ProxyEntityFactory();
            int intResult = proxy.Service.UpdateByPk(vo);
            if (intResult > 0)
            {
                DialogBox.Msg("保存成功!");
                m_mthRefresh();
                return true;
            }
            else
            {
                DialogBox.Msg("保存失败.");
                return false;
            }
        }

        private void m_mthRefresh()
        {
            object objSelect = clstElement.SelectedValue;
            this.m_mthLoadData();
            this.m_mthFilter(Convert.ToInt32(this.ctlPopupSelect.Value.ToString()));
            if (objSelect != null)
            {
                clstElement.SelectedValue = objSelect;
            }
            this.m_mthSetMultiChooseFlag();
        }

        void txtNew_Leave(object sender, EventArgs e)
        {
            txtNew.Leave -= new EventHandler(txtNew_Leave);
            if (m_intFlags == 0)
            {
                if (string.IsNullOrEmpty(txtNew.Text))
                {
                    RemoveTextEdit(true);
                }
                else
                {
                    if (DialogBox.Msg("是否保存？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (m_blnSaveNewData())
                        {
                            RemoveTextEdit(false);
                        }
                    }
                    else
                    {
                        RemoveTextEdit(true);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtNew.Text))
                {
                    DialogResult dlrResult = DialogBox.Msg("元素名称不能为空，是否继续？", MessageBoxIcon.Question, this.ParentForm);
                    if (dlrResult == DialogResult.Yes)
                    {
                        txtNew.Focus();
                        return;
                    }
                    else
                    {
                        RemoveTextEdit(false);
                    }
                }
                else
                {
                    if (DialogBox.Msg("是否保存？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (m_blnUpdateOldData())
                        {
                            RemoveTextEdit(false);
                        }
                    }
                    else
                    {
                        RemoveTextEdit(false);
                    }
                }
            }
        }

        void txtNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        /// <summary>
        /// 默认删除元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void biDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.biElementDel_ItemClick(null, null);
        }

        private bool m_blnDelData()
        {
            if (clstElement.SelectedItem != null)
            {
                int serno_int = Convert.ToInt32(clstElement.SelectedValue);

                EntityElementTemplateContent vo = new EntityElementTemplateContent();
                vo.status = 0;
                vo.serNo = Function.Dec(clstElement.SelectedValue);

                ProxyEntityFactory proxy = new ProxyEntityFactory();
                int intResult = proxy.Service.UpdateByPk(vo);
                proxy = null;
                if (intResult > 0)
                {
                    DialogBox.Msg("删除成功");
                    return true;
                }
                else
                {
                    DialogBox.Msg("删除失败");
                    return false;
                }
            }
            return false;
        }

        private void biTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_intFlags = 0;
            btnOK.Text = "确定";
            ctlPopupSelect.Value = null;
            pnlAddElement.Visible = true;
            txtTemplateName.Focus();
        }

        private void AddTemplate()
        {
            if (m_intFlags == 0)//新增
            {
                EntityElementTemplate entity = new EntityElementTemplate();
                entity.status = 1;
                entity.templateName = txtTemplateName.Text.Trim();
                if (!m_blnCheckTemplateName(entity.templateName))
                {
                    DialogBox.Msg("模板名称已存在");
                    return;
                }
                entity.createrId = GlobalLogin.objLogin.EmpNo;
                entity.createDate = Common.Utils.Utils.ServerTime();
                entity.pyCode = txtPYCode.Text.Trim();
                entity.wbCode = txtWBCode.Text.Trim();
                entity.caseCode = GlobalCase.caseInfo.CaseCode;
                entity.multiFlag = cbbMutiSel.SelectedIndex;

                ProxyEntityFactory proxy = new ProxyEntityFactory();
                int intRet = proxy.Service.Insert(entity);
                proxy = null;
                if (intRet > 0)
                {
                    if (DialogBox.Msg("成功增加了" + entity.templateName + "模板,是否继续增加该模板的元素？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        pnlAddElement.Visible = false;
                        m_mthLoadData();
                        DataTable dtbSource = this.ctlPopupSelect.DataSource as DataTable;
                        if (dtbSource != null)
                        {
                            DataRow[] drwArr = dtbSource.Select(EntityElementTemplate.Columns.templateName + " = '" + entity.templateName + "'");
                            if (drwArr != null && drwArr.Length > 0)
                            {
                                this.ctlPopupSelect.Value = drwArr[0][EntityElementTemplate.Columns.serno];
                                AddElement();
                            }
                        }
                    }
                    else
                    {
                        pnlAddElement.Visible = false;
                        m_mthLoadData();
                    }
                }
            }
            else//修改
            {
                if (txtTemplateName.Tag != null)
                {
                    string strSerno = txtTemplateName.Tag.ToString();
                    EntityElementTemplate vo = new EntityElementTemplate();
                    vo.templateName = txtTemplateName.Text.Trim();
                    vo.pyCode = txtPYCode.Text.Trim();
                    vo.wbCode = txtWBCode.Text.Trim();
                    vo.multiFlag = cbbMutiSel.SelectedIndex;
                    vo.serno = Function.Dec(strSerno);

                    ProxyEntityFactory proxy = new ProxyEntityFactory();
                    int intRet = proxy.Service.UpdateByPk(vo);
                    proxy = null;
                    if (intRet > 0)
                    {
                        DialogBox.Msg("成功修改了模板", MessageBoxIcon.Information);
                        pnlAddElement.Visible = false;
                        m_mthLoadData();
                        DataTable dtbSource = this.ctlPopupSelect.DataSource as DataTable;
                        if (dtbSource != null)
                        {
                            DataRow[] drwArr = dtbSource.Select(EntityElementTemplate.Columns.serno + " = " + strSerno);
                            if (drwArr != null && drwArr.Length > 0)
                            {
                                this.ctlPopupSelect.Value = drwArr[0][EntityElementTemplate.Columns.serno];
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 检查模板名
        /// </summary>
        /// <param name="p_strTemplateName"></param>
        /// <returns></returns>
        private bool m_blnCheckTemplateName(string p_strTemplateName)
        {
            DataTable dtbSource = this.ctlPopupSelect.DataSource as DataTable;
            if (dtbSource != null)
            {
                DataRow[] drwArr = dtbSource.Select(EntityElementTemplate.Columns.templateName + " = '" + p_strTemplateName + "'");
                if (drwArr != null && drwArr.Length > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void biElement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctlPopupSelect.Value == null)
            {
                DialogBox.Msg("请选择模板名称再添加模板元素");
                return;
            }
            AddElement();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTemplateName.Text))
            {
                DialogBox.Msg("模板名称不能为空");
                txtTemplateName.Focus();
                return;
            }
            AddTemplate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_intFlags = 0;
            pnlAddElement.Visible = false;
        }

        private void m_mthClear()
        {
            txtTemplateName.Text = "";
            txtPYCode.Text = "";
            txtWBCode.Text = "";
        }

        private void pnlAddElement_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlAddElement.Visible)
            {
                biAdd.Enabled = false;
                biDel.Enabled = false;
                biMultiChoose.Enabled = false;
                biEdit.Enabled = false;

                if (m_intFlags == 1)
                {
                    btnOK.Text = "保存";
                }
                else
                {
                    btnOK.Text = "确定";
                }
            }
            else
            {
                m_mthClear();
                biAdd.Enabled = true;
                biDel.Enabled = true;
                biMultiChoose.Enabled = true;
                biEdit.Enabled = true;

                txtTemplateName.Tag = null;
            }
        }

        private void pmAdd_Popup(object sender, EventArgs e)
        {
            biElement.Enabled = true;
            if (this.ctlPopupSelect.Value == null)
            {
                biElement.Enabled = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && pnlAddElement.Visible)
            {
                SendKeys.Send("{Tab}");
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtTemplateName_EditValueChanged(object sender, EventArgs e)
        {
            txtPYCode.Text = SpellCodeHelper.GetPyCode(txtTemplateName.Text);
            txtWBCode.Text = SpellCodeHelper.GetWbCode(txtTemplateName.Text);
        }

        private void biElementDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Msg("是否删除元素？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (m_blnDelData())
                {
                    m_mthRefresh();
                }
            }
        }

        private void biTemplateDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Msg("是否删除模板？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (m_blnDelTemplate())
                {
                    this.m_mthLoadData();
                    this.ctlPopupSelect.Value = null;
                }
            }
        }

        private bool m_blnDelTemplate()
        {
            if (this.ctlPopupSelect.Value != null)
            {
                EntityElementTemplate vo = new EntityElementTemplate();
                vo.status = 0;
                vo.serno = Function.Dec(this.ctlPopupSelect.Value.ToString());

                ProxyEntityFactory proxy = new ProxyEntityFactory();
                int intResult = proxy.Service.UpdateByPk(vo);
                proxy = null;
                if (intResult > 0)
                {
                    DialogBox.Msg("删除成功");
                    return true;
                }
                else
                {
                    DialogBox.Msg("删除失败");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 显示/隐藏元素模板联动信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void biMore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (biMore.Caption == ">>")
            //{
            //    m_mthOpenLinkedPage();
            //}
            //else
            //{
            //    m_mthCloseLinkedPage();
            //}
        }

        private void m_mthOpenLinkedPage()
        {
            //biMore.Caption = "<<";
            this.Width = 680;
            if (m_intFlags == 0)
            {
                txtContent_vchr.ReadOnly = true;
                btnSave.Enabled = false;
                btnReset.Enabled = false;
                rtfMenuStrip.Enabled = false;
            }
            else
            {
                txtContent_vchr.ReadOnly = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                rtfMenuStrip.Enabled = true;
            }
        }

        private void m_mthCloseLinkedPage()
        {
            //biMore.Caption = ">>";
            this.Width = 370;
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiTerm_Click(object sender, EventArgs e)
        {
            txtContent_vchr.m_mthAddMedicalTerm();
        }

        /// <summary>
        /// 保存关联字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clstElement.SelectedValue == null)
            {
                return;
            }
            string colcontent_vchr = txtContent_vchr.GetRightText();
            ProxyEntityFactory proxy = new ProxyEntityFactory();

            int result = 0;
            if (colcontent_vchr == "")
            {
                if (txtContent_vchr.Tag == null)
                {
                    //不做任何操作
                    result = 1;
                }
                else
                {
                    //删除数据
                    EntityElementTemplateLinkage entity = txtContent_vchr.Tag as EntityElementTemplateLinkage;
                    entity.status = 0;
                    result = proxy.Service.UpdateByPk(entity);
                }
            }
            else
            {
                if (txtContent_vchr.Tag == null)
                {
                    //新增数据
                    EntityElementTemplateLinkage entity = new EntityElementTemplateLinkage();
                    entity.colContent = colcontent_vchr;
                    entity.colContentRtf = txtContent_vchr.GetAllRtf();
                    entity.colContentXml = txtContent_vchr.GetXmlText();
                    entity.status = 1;
                    entity.elementId = Function.Int(clstElement.SelectedValue);

                    result = proxy.Service.Insert(entity);
                    LoadLinkage(clstElement.SelectedValue);
                }
                else
                {
                    //更新数据
                    EntityElementTemplateLinkage entity = txtContent_vchr.Tag as EntityElementTemplateLinkage;
                    entity.colContent = colcontent_vchr;
                    entity.colContentRtf = txtContent_vchr.GetAllRtf();
                    entity.colContentXml = txtContent_vchr.GetXmlText();
                    result = proxy.Service.UpdateByPk(entity);
                }
            }
            proxy = null;
            if (result > 0)
            {
                DialogBox.Msg("保存成功");
                m_mthRefresh();
                return;
            }
            else
            {
                DialogBox.Msg("保存失败");
                return;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadLinkage(clstElement.SelectedValue);
        }

        /// <summary>
        /// 显示关联字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clstElement_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadLinkage(clstElement.SelectedValue);
            clstElement.Focus();
        }

        /// <summary>
        /// 载入元素内容联动信息
        /// </summary>
        private void LoadLinkage(object value)
        {
            txtContent_vchr.ClearText();

            //标记是否原有数据,以便在保存时根据情况执行不同操作
            txtContent_vchr.Tag = null;
            if (value != null)
            {
                EntityElementTemplateLinkage vo = new EntityElementTemplateLinkage();
                vo.elementId = Function.Int(value);
                vo.status = 1;
                ProxyEntityFactory proxy = new ProxyEntityFactory();
                vo = EntityTools.ConvertToEntity<EntityElementTemplateLinkage>(proxy.Service.Select(vo, new List<string> { EntityElementTemplateLinkage.Columns.elementId, EntityElementTemplateLinkage.Columns.status }));
                proxy = null;
                if (vo != null)
                {
                    txtContent_vchr.SetXmlText(vo.colContentRtf, vo.colContentXml, false);
                    txtContent_vchr.Tag = vo;
                    m_mthOpenLinkedPage();
                }
                else
                {
                    if (m_intFlags == 0)
                    {
                        m_mthCloseLinkedPage();
                    }
                }
                proxy = null;
            }
        }

        private void clstElement_Click(object sender, EventArgs e)
        {
            if (!this.m_blnMultiChooseFlag)
            {
                for (int i = 0; i < this.clstElement.ItemCount; i++)
                {
                    this.clstElement.SetItemChecked(i, false);
                }
                SetElementName();
            }
        }

        private void SetElementName()
        {
            BindingListView<EntityElementTemplate> lisDCElement = this.clstElement.DataSource as BindingListView<EntityElementTemplate>;
            if (lisDCElement == null)
            {
                this.txtElement.Text = m_strElementName;
                return;
            }

            string strContent = string.Empty;
            string strRemoveTxt = string.Empty;
            for (int i = 0; i < this.clstElement.ItemCount; i++)
            {
                if (this.clstElement.GetItemChecked(i))
                {
                    if (this.txtElement.Text.Contains(lisDCElement[i].colcontent))
                        continue;
                    strContent += lisDCElement[i].colcontent + ",";
                }
                else
                {
                    strRemoveTxt = lisDCElement[i].colcontent;
                    if (this.txtElement.Text.Trim() != string.Empty)
                    {
                        if (this.txtElement.Text.Contains(strRemoveTxt + ","))
                        {
                            this.txtElement.Text = this.txtElement.Text.Replace(strRemoveTxt + ",", "");
                        }
                        if (this.txtElement.Text.Contains("," + strRemoveTxt))
                        {
                            this.txtElement.Text = this.txtElement.Text.Replace("," + strRemoveTxt, "");
                        }
                        else if (this.txtElement.Text.Trim() == strRemoveTxt)
                        {
                            this.txtElement.Text = string.Empty;
                        }
                    }
                }
            }

            string strOrgTxt = this.txtElement.Text.Trim();

            if (strContent != string.Empty)
            {
                strContent = strContent.Substring(0, strContent.Length - 1);
            }

            if (string.IsNullOrEmpty(strOrgTxt))
            {
                this.txtElement.Text = strContent;
            }
            else
            {
                if (string.IsNullOrEmpty(strContent))
                {
                    this.txtElement.Text = strOrgTxt;
                }
                else
                {
                    this.txtElement.Text = strOrgTxt + "," + strContent;
                }
            }
        }

        private void biMultiChoose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.m_mthSelect();
        }

        private void biEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            biElementEdit_ItemClick(sender, e);
        }

        private void clstElement_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            BindingListView<EntityElementTemplate> source = clstElement.DataSource as BindingListView<EntityElementTemplate>;
            if (source != null)
            {
                EntityElementTemplate objElementTemplate = source.FirstOrDefault(t => t.serno.ToString() == e.Item.ToString());
                if (objElementTemplate.linkSerno != null)
                {
                    e.Appearance.ForeColor = Color.Blue;
                    //e.Appearance.Font = new Font("宋体", 10.5f, FontStyle.Bold);
                }
            }
        }

        private void biElementEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (clstElement.SelectedItem == null)
            {
                return;
            }

            m_intFlags = 1;
            Rectangle rtg = clstElement.GetItemRectangle(clstElement.SelectedIndex);
            biAdd.Enabled = false;
            biDel.Enabled = false;
            biMultiChoose.Enabled = false;
            biEdit.Enabled = false;
            txtNew = new DevExpress.XtraEditors.TextEdit();
            txtNew.Name = "txtNew";
            int intCount = clstElement.ItemCount;
            int intHeight = (intCount + 1) * clstElement.ItemHeight;

            txtNew.Location = new Point(0, 0);
            if (clstElement.Height <= intHeight)
            {
                txtNew.Width = clstElement.Width - 17;
            }
            else
            {
                txtNew.Width = clstElement.Width;
            }
            txtNew.Height = clstElement.ItemHeight;
            txtNew.KeyDown += new KeyEventHandler(txtNew_KeyDown);
            txtNew.Leave += new EventHandler(txtNew_Leave);
            txtNew.Location = rtg.Location;
            txtNew.Text = clstElement.GetItemText(clstElement.SelectedIndex);
            txtNew.Tag = clstElement.GetItemValue(clstElement.SelectedIndex);

            clstElement.Controls.Add(txtNew);
            txtNew.BringToFront();
            txtNew.Focus();

        }

        private void biTemplateEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctlPopupSelect.Value == null)
            {
                return;
            }

            m_intFlags = 1;
            DataTable dtbSelectSource = this.ctlPopupSelect.DataSource as DataTable;
            if (dtbSelectSource != null)
            {
                DataRow drwCurrent = this.ctlPopupSelect.CurrentRow;

                txtTemplateName.Text = drwCurrent[EntityElementTemplate.Columns.templateName].ToString();
                txtTemplateName.Tag = drwCurrent[EntityElementTemplate.Columns.serno];
                txtPYCode.Text = drwCurrent[EntityElementTemplate.Columns.pyCode].ToString();
                txtWBCode.Text = drwCurrent[EntityElementTemplate.Columns.wbCode].ToString();
                if (drwCurrent[EntityElementTemplate.Columns.multiFlag] == DBNull.Value || drwCurrent[EntityElementTemplate.Columns.multiFlag].ToString() == "0")
                {
                    cbbMutiSel.SelectedIndex = 0;
                }
                else
                {
                    cbbMutiSel.SelectedIndex = 1;
                }

                pnlAddElement.Visible = true;
            }
        }

        private void biLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (clstElement.SelectedItem == null)
            {
                return;
            }
            m_intFlags = 1;
            m_mthOpenLinkedPage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            m_intFlags = 0;
            m_mthCloseLinkedPage();
        }

        private void txtFilter_Enter(object sender, EventArgs e)
        {
            this.lblHint.Visible = false;
        }

        private void lblHint_Click(object sender, EventArgs e)
        {
            this.lblHint.Visible = false;
        }

        private void txtFilter_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lstTemplateSource != null && this.lstTemplateSource.Count > 0)
            {
                if (this.clstElement.ItemCount <= 0)
                {
                    return;
                }
                for (int i = 0; i < this.lstTemplateSource.Count; i++)
                {
                    this.clstElement.SetItemChecked(i, false);
                }
                SetElementName();

                string strVal = this.txtFilter.Text.Trim();
                if (string.IsNullOrEmpty(strVal)) return;

                if (this.lstTemplateSource.Any(t => t.pyCode.Contains(strVal) || t.wbCode.Contains(strVal) || t.colcontent.Contains(strVal)))
                {
                    EntityElementTemplate objEle = this.lstTemplateSource.First(t => t.pyCode.Contains(strVal) || t.wbCode.Contains(strVal) || t.colcontent.Contains(strVal));
                    if (objEle != null)
                    {
                        int intIdx = this.clstElement.FindItem(objEle);
                        if (intIdx >= 0)
                        {
                            this.clstElement.SetSelected(intIdx, true);
                            this.clstElement.SetItemChecked(intIdx, true);
                            this.txtFilter.Focus();

                            SetElementName();
                        }
                    }
                }
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                this.clstElement.Focus();
            }
        }

        private void clstElement_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            SetElementName();
        }

    }
}
