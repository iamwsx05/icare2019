using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Common.Controls.Emr
{
    public class XtraTabCollectionEditor : CollectionEditor
    {
        public XtraTabCollectionEditor(Type type)
            : base(type)
        {

        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            return base.EditValue(context, provider, value);
        }

        protected override object[] GetItems(object editValue)
        {
            return base.GetItems(editValue);
        }

        protected override Type[] CreateNewItemTypes()
        {
            return base.CreateNewItemTypes();
        }

        protected override object CreateInstance(Type itemType)
        {
            //object obj = base.CreateInstance(itemType);

            object obj = (this.Context.Instance as BaseControlWarpper).designerhost.CreateComponent(itemType);

            XtraTabPage tab = obj as DevExpress.XtraTab.XtraTabPage;
            tab.Text = tab.Name.Replace("XtraTabPage", "页签");

            //(obj as  DevExpress.XtraTab.XtraTabPage).Text = obj

            return obj;
        }

        protected override Type CreateCollectionItemType()
        {
            return base.CreateCollectionItemType();
        }

        protected override CollectionForm CreateCollectionForm()
        {
            return base.CreateCollectionForm();
        }
    }
}
