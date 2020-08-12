using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class frmBaseNormal : frmBase
    {
        public frmBaseNormal()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                // 资源
                this.rm = new System.Resources.ResourceManager(typeof(Common.Controls.Properties.Resources));
            }
        }
        #region 变量.属性

        /// <summary>
        /// 是否显示退出系统图标
        /// </summary>
        private bool _IsShowHaltIco = true;

        /// <summary>
        /// 是否显示退出系统图标
        /// </summary>
        [Browsable(false)]
        public bool IsShowHaltIco
        {
            get { return _IsShowHaltIco; }
            set { _IsShowHaltIco = value; }
        }

        public List<EntitySysModule> FuncItems { get; set; }

        /// <summary>
        /// rm
        /// </summary>
        System.Resources.ResourceManager rm { get; set; }

        #endregion

        #region 虚方法
        /// <summary>
        /// 新建
        /// </summary>
        protected virtual void New()
        {
        }
        /// <summary>
        /// 删除
        /// </summary>
        protected virtual void Delete()
        {

        }
        /// <summary>
        /// 编辑
        /// </summary>
        protected virtual void Edit()
        {

        }
        /// <summary>
        /// 刷新
        /// </summary>
        protected virtual void RefreshData()
        {

        }
        /// <summary>
        /// 保存
        /// </summary>
        protected virtual void Save()
        {

        }
        /// <summary>
        /// 保存
        /// </summary>
        protected virtual void Put()
        {

        }
        /// <summary>
        /// 停止
        /// </summary>
        protected virtual void Stop()
        {

        }
        /// <summary>
        /// 完成
        /// </summary>
        protected virtual void Complete()
        {

        }
        /// <summary>
        /// 查找
        /// </summary>
        protected virtual void Search()
        {

        }
        /// <summary>
        /// 审核
        /// </summary>
        protected virtual void Confirm()
        {

        }
        /// <summary>
        /// 反审核
        /// </summary>
        protected virtual void UnConfirm()
        {

        }
        /// <summary>
        /// 撤销
        /// </summary>
        protected virtual void Cancel()
        {

        }
        /// <summary>
        /// 获取
        /// </summary>
        protected virtual void LoadData()
        {

        }
        /// <summary>
        /// 模板
        /// </summary>
        protected virtual void Template()
        {

        }
        /// <summary>
        /// 打印预览
        /// </summary>
        protected virtual void Preview()
        {

        }

        /// <summary>
        /// 重载字典
        /// </summary>
        protected virtual void ReloadDic()
        {

        }

        /// <summary>
        /// 设计
        /// </summary>
        protected virtual void Design()
        {

        }

        /// <summary>
        /// 自定义表单
        /// </summary>
        protected virtual void CustomForm()
        {

        }

        /// <summary>
        /// 定义项目
        /// </summary>
        protected virtual void DefineItem()
        {

        }

        /// <summary>
        /// 导出
        /// </summary>
        protected virtual void Export()
        {

        }

        /// <summary>
        /// 统计
        /// </summary>
        protected virtual void Statistics()
        {

        }

        /// <summary>
        /// 复制功能
        /// </summary>
        protected virtual void AidFunc()
        {

        }

        /// <summary>
        /// 提醒
        /// </summary>
        protected virtual void Remind()
        {

        }

        /// <summary>
        /// 随访--电话
        /// </summary>
        protected virtual void FuvTel()
        {

        }

        /// <summary>
        /// 随访--短信
        /// </summary>
        protected virtual void FuvMessage()
        {

        }

        /// <summary>
        /// 随访--微信
        /// </summary>
        protected virtual void FuvWeChat()
        {

        }

        /// <summary>
        /// 随访--邮件
        /// </summary>
        protected virtual void FuvEmail()
        {

        }

        protected virtual void ExecFuv()
        {

        }

        protected virtual void Adjust()
        {

        }

        protected virtual void BasicInfo()
        {

        }

        protected virtual void FuvTimes()
        {

        }

        protected virtual void FuvCase()
        {

        }

        protected virtual DevExpress.XtraEditors.XtraScrollableControl GetScrollableContainer()
        {
            return new DevExpress.XtraEditors.XtraScrollableControl();
        }

        protected virtual void NavPatientChange()
        {

        }

        #endregion

        #region CheckValueChanged
        /// <summary>
        /// CheckValueChanged
        /// </summary>
        /// <returns></returns>
        private int CheckValueChanged()
        {
            if (!ValueChanged) return 0;

            DialogResult dr = DialogBox.Msg("【" + this.Text.Replace(" ", "") + "】内容已变动，是否保存？", MessageBoxIcon.Question, true);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    this.Save();
                }
                catch (Exception e)
                {
                    DialogBox.Msg(e.Message);
                    return -1;
                }
                return 1;
            }
            else if (dr == DialogResult.No)
                return 0;
            else if (dr == DialogResult.Cancel)
                return -1;
            else
                return 0;
        }
        #endregion

        #region 权限列表
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        //private List<EntitySysModule> GetFuncButton()
        //{
        //    using (ProxyLogin proxy = new ProxyLogin())
        //    {
        //        return proxy.Service.GetFormFuncButton(Function.Int(GlobalLoginInfo.objLoginInfo.EmpId), this.GetType().FullName);
        //    }

        //    return null;
        //}

        /// <summary>
        /// SetToolBar
        /// </summary>
        private void SetToolBar()
        {
            List<EntitySysModule> lstFuncButton = this.FuncItems;
            if (lstFuncButton == null || lstFuncButton.Count == 0)
            {
                this.barTools.Visible = false;
            }
            else
            {
                #region 默认统一按钮
                // 分辨率
                EntitySysModule vo = null;
                //vo = new EntitySysModule();
                //vo.FuncId = 9999901;
                //vo.FuncName = "分辨率";
                //vo.OperName = "ratio";
                //lstFuncButton.Add(vo);
                // 锁系统
                //vo = new EntitySysModule();
                //vo.FuncId = 9999902;
                //vo.FuncName = "锁系统";
                //vo.OperName = "locksys";
                //lstFuncButton.Add(vo);
                // 关闭窗口
                vo = new EntitySysModule();
                vo.FuncId = 9999903;
                vo.FuncName = "关闭窗口";
                vo.OperName = "close";
                vo.ImageSource = "Close";
                lstFuncButton.Add(vo);

                if (IsShowHaltIco)
                {
                    // 退出系统
                    vo = new EntitySysModule();
                    vo.FuncId = 9999904;
                    vo.FuncName = "退出系统";
                    vo.OperName = "halt";
                    lstFuncButton.Add(vo);
                }
                #endregion

                this.barManager.BeginInit();
                this.barManager.BeginUpdate();
                DevExpress.XtraBars.BarLargeButtonItem bbi = null;
                foreach (EntitySysModule item in lstFuncButton)
                {
                    // 一个窗体多个挂接参数
                    if (!string.IsNullOrEmpty(this.AccessibleName) && item.ParentId > 0)
                    {
                        if (!item.ParentId.ToString().Equals(this.AccessibleName)) continue;
                    }

                    bbi = new DevExpress.XtraBars.BarLargeButtonItem();
                    bbi.Id = Function.Int(item.FuncCode);
                    bbi.Caption = item.FuncName;
                    bbi.Name = "bbi" + item.OperName.ToLower();
                    bbi.Appearance.Font = new System.Drawing.Font("宋体", 9.5f);
                    bbi.Glyph = (rm.GetObject(item.ImageSource) as System.Drawing.Image);
                    switch (item.OperName.ToLower().Trim())
                    {
                        case "adjust":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiAdjust_ItemClick);
                            break;
                        case "load":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiLoad_ItemClick);
                            break;
                        case "template":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiTemplate_ItemClick);
                            break;
                        case "new":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiNew_ItemClick);
                            break;
                        case "save":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiSave_ItemClick);
                            break;
                        case "delete":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDel_ItemClick);
                            break;
                        case "edit":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiEdit_ItemClick);
                            break;
                        case "cancel":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiCancel_ItemClick);
                            break;
                        case "confirm":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiConfirm_ItemClick);
                            break;
                        case "unconfirm":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiUnConfirm_ItemClick);
                            break;
                        case "stop":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiStop_ItemClick);
                            break;
                        case "print":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiPrint_ItemClick);
                            break;
                        case "export":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiExport_ItemClick);
                            break;
                        case "refresh":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiRefresh_ItemClick);
                            break;
                        case "search":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiSearch_ItemClick);
                            break;
                        case "complete":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiComplete_ItemClick);
                            break;
                        case "close":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiClose_ItemClick);
                            break;
                        case "halt":
                            bbi.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiHalt_ItemClick);
                            break;
                        case "design":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDesign_ItemClick);
                            break;
                        case "customform":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiCustomForm_ItemClick);
                            break;
                        case "defineitem":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiDefineItem_ItemClick);
                            break;
                        case "stat":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiStatItemClick);
                            break;
                        case "aidfunc":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiAidFunc_ItemClick);
                            break;
                        case "remind":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiRemind_ItemClick);
                            break;
                        case "fuvtel":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvTel_ItemClick);
                            break;
                        case "fuvmessage":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvMessage_ItemClick);
                            break;
                        case "fuvwechat":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvWeChat_ItemClick);
                            break;
                        case "fuvemail":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvEmail_ItemClick);
                            break;
                        case "execfuv":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiExecFuv_ItemClick);
                            break;
                        case "basicinfo":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiBasicInfo_ItemClick);
                            break;
                        case "fuvtimes":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvTimes_ItemClick);
                            break;
                        case "fuvcase":
                            bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbiFuvCase_ItemClick);
                            break;
                        default:
                            break;
                    }

                    this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { bbi });
                    this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, bbi, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});

                }
                this.barManager.EndUpdate();
                this.barManager.EndInit();
            }
        }

        private void bbiTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Template();
        }

        private void bbiAdjust_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Adjust();
        }

        private void bbiLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New();
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void bbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void bbiStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Stop();
        }

        private void bbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Preview();
        }

        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Export();
        }

        private void bbiComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Complete();
        }

        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cancel();
        }

        private void bbiConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm();
        }

        private void bbiUnConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UnConfirm();
        }

        private void bbiSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Search();
        }

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }

        private void bbiDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Design();
        }

        private void bbiCustomForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CustomForm();
        }

        private void bbiDefineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DefineItem();
        }

        private void bbiAidFunc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AidFunc();
        }

        private void bbiStatItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Statistics();
        }

        private void bbiRemind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Remind();
        }

        private void bbiFuvTel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvTel();
        }

        private void bbiFuvMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvMessage();
        }

        private void bbiFuvWeChat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvWeChat();
        }

        private void bbiFuvEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvEmail();
        }

        private void bbiBasicInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BasicInfo();
        }

        private void bbiFuvTimes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvTimes();
        }

        private void bbiFuvCase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FuvCase();
        }

        private void bbiExecFuv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExecFuv();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MdiParent != null && this.MdiParent.ActiveMdiChild != null)
            {
                this.MdiParent.ActiveMdiChild.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void bbiHalt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frmMdi = this.ParentForm;
            if (frmMdi == null) return;
            try
            {
                if (frmMdi.IsMdiContainer && frmMdi.MdiChildren != null)
                {
                    foreach (Form child in frmMdi.MdiChildren)
                    {
                        child.Close();
                    }
                }
            }
            catch
            {
                return;
            }
            frmMdi.Close();
        }

        #endregion

        #region 窗体事件

        private void frmBaseMdi_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.SetToolBar();
                if (IsShowHaltIco) this.WindowState = FormWindowState.Maximized;
            }
        }

        private void frmBaseMdi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckValueChanged() == -1)
            {
                e.Cancel = true;
            }
        }

        #endregion

    }
}
