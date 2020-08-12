using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraVerticalGrid.Rows;

namespace Common.Controls.Emr
{
    public class ControlPropertyGrid : DevExpress.XtraVerticalGrid.PropertyGridControl
    {
        public ControlPropertyGrid()
        {
            this.TreeButtonStyle = DevExpress.XtraVerticalGrid.TreeButtonStyle.TreeView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="bindingReadonly">"数据绑定"部分属性是否可修改</param>
        public void SetEditObject(BaseControlWarpper wrapper)
        {
            object wrappedObject = wrapper.WrappedObject;
            if (wrappedObject is IRuntimeDesignControl)
            {
                this.SelectedObject = wrapper;

                #region 隐藏/显示分类

                ShowCategory("节点", wrappedObject is ICpNode);
                ShowCategory("名称", wrappedObject is IFormCtrl);
                ShowCategory("CheckBox属性", wrappedObject is ICheckBox);
                ShowCategory("ComboBox属性", wrappedObject is ICombox);
                ShowCategory("线条属性", wrappedObject is ICtlLine);
                ShowCategory("PictureBox属性", wrappedObject is IPictureBox);
                ShowCategory("Panel属性", wrappedObject is IPanel);
                ShowCategory("TabControl属性", wrappedObject is ITabControl);
                ShowCategory("签名控件属性", wrappedObject is ISignatureControl);
                ShowCategory("RichText属性", wrappedObject is IRtfEditor);
                ShowCategory("日期时间属性", wrappedObject is IXtraDateTime);

                #endregion
            }
            else
            {
                this.SelectedObject = null;
            }            
        }

        internal void SetEditEnable(bool isEnable)
        {
            CategoryRow category = GetCategory("字段");
            if (category != null)
            {
                foreach (BaseRow dr in category.ChildRows)
                {
                    dr.Properties.ReadOnly = isEnable;
                }
            }
        }

        private void ShowCategory(string catName, bool visible)
        {
            CategoryRow row = GetCategory(catName);
            if (row != null)
            {
                row.Visible = visible;
            }
        }

        private CategoryRow GetCategory(string catName)
        {
            foreach (CategoryRow row in this.Rows)
            {
                if (row.Properties.Caption == catName)
                {
                    return row;
                }
            }
            return null;
        }
    }
}
