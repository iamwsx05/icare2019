using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ucBase : UserControl
    {
        public ucBase()
        {
            InitializeComponent();
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            if (!DesignMode)
            {
                this.CreateController();
            }
        }

        #region 变量.属性
        /// <summary>
        /// ValueChanged
        /// </summary>
        [Browsable(false)]
        public bool ValueChanged { get; set; }

        private bool _IsDockFill = true;

        /// <summary>
        /// IsDockFill
        /// </summary>
        [Browsable(false)]
        public bool IsDockFill
        {
            get { return _IsDockFill; }
            set { _IsDockFill = value; }
        }

        /// <summary>
        /// 重新加载字典
        /// </summary>
        [Browsable(false)]
        public bool IsReloadDictionary { get; set; }
        /// <summary>
        /// 窗体控制器
        /// </summary>
        public BaseController Controller;

        /// <summary>
        /// 标题
        /// </summary>        
        public string Caption { get; set; }

        /// <summary>
        /// 是否保存
        /// </summary>
        [Browsable(false)]
        public bool IsSave { get; set; }

        #endregion

        /// <summary>
        /// CellSize
        /// </summary>
        protected class CellSize
        {
            /// <summary>
            /// 宽度
            /// </summary>
            public int Width { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public int Height { get; set; }
            /// <summary>
            /// LookUpEditContainer
            /// </summary>
            public LookUpEditContainer Cell { get; set; }
        }

        /// <summary>
        /// 创建窗体控制器
        /// </summary>
        protected virtual void CreateController()
        { }
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
        /// 勾选
        /// </summary>
        public virtual void Check()
        { }
        /// <summary>
        /// 审核
        /// </summary>
        public virtual void Confirm()
        { }
        /// <summary>
        /// 获取
        /// </summary>
        public virtual void LoadData()
        { }
        /// <summary>
        /// 导出
        /// </summary>
        public virtual void Export()
        { }
        /// <summary>
        /// 预览
        /// </summary>
        public virtual void Preview()
        { }
        /// <summary>
        /// 模板
        /// </summary>
        public virtual void Template()
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
        /// 检查数据是否更改
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckDataChanged()
        {
            return false;
        }
        #endregion


    }
}
