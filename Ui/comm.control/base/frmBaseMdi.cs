using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class frmBaseMdi : frmBase
    {
        public frmBaseMdi()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 是否保存
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        /// 是否显示退出系统图标
        /// </summary>
        private bool _IsShowHaltIco = true;

        /// <summary>
        /// 当前病历编辑器
        /// </summary>
        public Emr.ctlRichTextBox CurrRtfEditor { get; set; }

        /// <summary>
        /// 是否显示退出系统图标
        /// </summary>
        [Browsable(false)]
        public bool IsShowHaltIco
        {
            get { return _IsShowHaltIco; }
            set { _IsShowHaltIco = value; }
        }

        #region 虚方法
        /// <summary>
        /// 新建
        /// </summary>
        public virtual void New()
        { }
        /// <summary>
        /// 删除
        /// </summary>
        public virtual void Delete()
        { }
        /// <summary>
        /// 编辑
        /// </summary>
        public virtual void Edit()
        { }
        /// <summary>
        /// 刷新
        /// </summary>
        public virtual void RefreshData()
        { }
        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        { }
        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Put()
        { }
        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop()
        { }
        /// <summary>
        /// 完成
        /// </summary>
        public virtual void Complete()
        { }
        /// <summary>
        /// 查找
        /// </summary>
        public virtual void Search()
        { }
        /// <summary>
        /// 审核
        /// </summary>
        public virtual void Confirm()
        { }
        /// <summary>
        /// 反审核
        /// </summary>
        public virtual void UnConfirm()
        { }
        /// <summary>
        /// 撤销
        /// </summary>
        public virtual void Cancel()
        { }
        /// <summary>
        /// 获取
        /// </summary>
        public virtual void LoadData()
        { }
        /// <summary>
        /// 模板
        /// </summary>
        public virtual void Template()
        { }
        /// <summary>
        /// 打印预览
        /// </summary>
        public virtual void Preview()
        { }
        /// <summary>
        /// 重载字典
        /// </summary>
        public virtual void ReloadDic()
        { }
        /// <summary>
        /// 设计
        /// </summary>
        public virtual void Design()
        { }
        /// <summary>
        /// 表单设计
        /// </summary>
        public virtual void EformDesign()
        { }
        /// <summary>
        /// 自定义表单
        /// </summary>
        public virtual void CustomForm()
        { }
        /// <summary>
        /// 定义项目
        /// </summary>
        public virtual void DefineItem()
        { }
        /// <summary>
        /// 导出
        /// </summary>
        public virtual void Export()
        { }
        /// <summary>
        /// 统计
        /// </summary>
        public virtual void Statistics()
        { }
        /// <summary>
        /// 复制功能
        /// </summary>
        public virtual void AidFunc()
        { }
        /// <summary>
        /// 提醒
        /// </summary>
        public virtual void Remind()
        { }
        /// <summary>
        /// 入径
        /// </summary>
        public virtual void Cpin()
        { }
        /// <summary>
        /// 路径管理
        /// </summary>
        public virtual void Cpmgt()
        { }
        /// <summary>
        /// 引用
        /// </summary>
        public virtual void Refrence()
        { }
        /// <summary>
        /// 查找
        /// </summary>
        public virtual void Find()
        { }
        /// <summary>
        /// 采集
        /// </summary>
        public virtual void Capture()
        { }
        public virtual DevExpress.XtraEditors.XtraScrollableControl GetScrollableContainer()
        {
            return new DevExpress.XtraEditors.XtraScrollableControl();
        }
        public virtual void NavPatientChange()
        { }
        public virtual void RefreshPatient(Common.Entity.EntityPatient patVo)
        { }
        /// <summary>
        /// 重置NavBarStyleView
        /// </summary>
        public virtual void SetNavBarStyleView()
        { }
        /// <summary>
        /// 插入
        /// </summary>
        public virtual void Insert()
        { }
        public virtual void Copy()
        { }
        public virtual void Paste()
        { }
        public virtual void Child()
        { }
        public virtual void Register()
        { }
        public virtual void BedMgr()
        { }
        public virtual void Previous()
        { }
        public virtual void Next()
        { }
        public virtual void CheckIn()
        { }
        public virtual void BarCode()
        { }
        public virtual void Consent()
        { }
        #endregion

        private void ResetToolBar(string className, bool isLoad)
        {
            Form frmMdiParent = this.MdiParent;
            if (frmMdiParent == null) return;
            MethodInfo objMth = frmMdiParent.GetType().GetMethod("ResetToolBar");
            object[] param = new object[2];
            param[0] = className;
            param[1] = isLoad;
            objMth.Invoke(frmMdiParent, param);
        }

        private void frmBaseMdi_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResetToolBar(this.AccessibleName, false);
        }

        private void frmBaseMdi_Activated(object sender, EventArgs e)
        {
            ResetToolBar(this.AccessibleName, true);
        }
    }
}
