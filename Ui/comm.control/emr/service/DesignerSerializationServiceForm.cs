using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel.Design;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Reflection;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 控件序列化/反序列化服务
    /// </summary>
    public class DesignerSerializationServiceForm : IDesignerSerializationService
    {
        IDesignerHost designerHost;

        public DesignerSerializationServiceForm(IDesignerHost host)
        {
            designerHost = host;
        }

        #region IDesignerSerializationService 成员

        private List<EntityFormCtrl> GetRootControls(List<EntityFormCtrl> listAllComps)
        {
            List<EntityFormCtrl> roots = new List<EntityFormCtrl>();
            foreach (EntityFormCtrl item in listAllComps)
            {
                if ((item.Parent == null || item.Parent.Trim() == string.Empty || !listAllComps.Any(i => i.ControlName == item.Parent)) &&
                    (item.ItemParent == null || item.ItemParent.Trim() == string.Empty))
                {
                    roots.Add(item);
                }
            }
            return roots;
        }

        /// <summary>
        /// 反序列化控件
        /// </summary>
        /// <param name="serializationData"></param>
        /// <returns></returns>
        public System.Collections.ICollection Deserialize(object serializationData)
        {
            try
            {
                List<EntityFormCtrl> data = serializationData as List<EntityFormCtrl>;
                if (data == null)
                    return null;

                List<EntityFormCtrl> temp = GetRootControls(data);
                List<Component> comp = new List<Component>();

                foreach (EntityFormCtrl entity in temp)
                {
                    if (!entity.ControlName.Contains(typeof(DesignPanel).Name))
                    {
                        object obj = DeserializeControl(entity, data);

                        if (obj is Control)
                        {
                            comp.Add(obj as Control);

                            if (obj is DevExpress.XtraEditors.ButtonEdit)
                            {
                                (obj as DevExpress.XtraEditors.ButtonEdit).Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));
                            }
                        }
                    }
                }

                return comp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreateControlName(string prevName, IContainer container, Type dataType, List<EntityFormCtrl> listAllComps)
        {
            INameCreationService nameService = designerHost.GetService(typeof(INameCreationService)) as INameCreationService;

            bool isValidName = true;

            if (container.Components[prevName] != null)
            {
                isValidName = false;
            }

            if (isValidName)
            {
                return prevName;
            }
            else
            {
                string currName = nameService.CreateName(container, dataType);
                if (currName == prevName)
                {
                    return prevName;
                }
                else
                {
                    foreach (EntityFormCtrl item in listAllComps)
                    {
                        if (item.Parent == prevName)
                        {
                            item.Parent = currName;
                        }

                        if (item.ControlName == prevName)
                        {
                            item.ControlName = currName;
                        }
                    }
                    return currName;
                }
            }
        }

        /// <summary>
        /// (递归)反序列化单个控件
        /// 反序列化后如果当前控件名字已存在则重新命名当前控件：如果当前控件为子控件，则把序列化数据中的控件的父控件名字改为新的控件名字
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listAll"></param>
        /// <returns></returns>
        private IComponent DeserializeControl(EntityFormCtrl entity, List<EntityFormCtrl> listAll)
        {
            try
            {
                if (entity.ControlName.Contains(typeof(DesignPanel).Name))
                {
                    return null;
                }

                Type type = Type.GetType(entity.ControlType);

                object instance = null;
                if (type != null)
                {
                    if (typeof(IComponent).IsAssignableFrom(type))
                    {
                        instance = designerHost.CreateComponent(type, CreateControlName(entity.ControlName, designerHost as IContainer, Type.GetType(entity.ControlType), listAll));
                    }
                    else
                    {
                        instance = Activator.CreateInstance(type);
                    }

                    if (instance != null && instance is Control)
                    {
                        Control ctrlParent = instance as Control;

                        if (string.IsNullOrEmpty(ctrlParent.Name))
                        {
                            ctrlParent.Name = CreateControlName(entity.ControlName, designerHost as IContainer, Type.GetType(entity.ControlType), listAll);
                        }

                        if (ctrlParent is IFormCtrl)
                        {
                            IFormCtrl iForm = ctrlParent as IFormCtrl;
                            iForm.ItemName = entity.ItemName;
                            iForm.ItemCaption = entity.ItemCaption;
                            iForm.ItemType = entity.ItemType;
                            iForm.ParentNode = entity.ParentNode;
                            iForm.CalProperty = entity.CalProperty;
                            iForm.RowShrinkDigit = entity.RowShrinkDigit;
                        }

                        if (ctrlParent is ICheckBox)
                        {
                            ICheckBox iChk = ctrlParent as ICheckBox;
                            iChk.GroupName = entity.GroupName;
                            iChk.SumName = entity.SumName;
                            iChk.CheckedWeightValue = entity.CheckedWeightValue;
                            iChk.Checked = (entity.Checked == "1" ? true : false);
                        }
                        else if (ctrlParent is ICombox)
                        {
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                ICombox iCbx = ctrlParent as ICombox;
                                string[] items = entity.Items.Split(ConstValue.CONTROL_ITEMS_SPLITER);
                                foreach (string i in items)
                                {
                                    iCbx.Items.Add(i);
                                }
                            }
                        }
                        else if (ctrlParent is ICtlLine)
                        {
                            ICtlLine iLine = ctrlParent as ICtlLine;
                            iLine.LineStyle = (CtlLineStyle)Enum.Parse(typeof(CtlLineStyle), entity.LineStyle); ;
                            iLine.LineWidth = entity.LineWidth;
                        }
                        else if (ctrlParent is IPictureBox)
                        {
                            IPictureBox iPic = ctrlParent as IPictureBox;
                            iPic.FileName = entity.PicFileName;
                        }
                        else if (ctrlParent is IPanel)
                        {
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                IPanel ipnl = ctrlParent as IPanel;
                                if (!string.IsNullOrEmpty(entity.ReserveField))
                                {
                                    string[] items = entity.ReserveField.Split(ConstValue.CONTROL_ITEMS_SPLITER);

                                    if (items.Length >= 2)
                                    {
                                        int cols = 1;
                                        int rows = 1;
                                        if (int.TryParse(items[0], out cols) && int.TryParse(items[1], out rows))
                                        {
                                            ipnl.Columns = cols;
                                            ipnl.Rows = rows;
                                        }
                                    }
                                }
                                try
                                {
                                    ipnl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), entity.Items);
                                }
                                catch (Exception ex)
                                {
                                    weCare.Core.Utils.ExceptionLog.OutPutException(ex);
                                }
                            }
                        }
                        else if (ctrlParent is ITabControl)
                        {
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                ITabControl ITabCtrl = ctrlParent as ITabControl;
                                ITabCtrl.TabPages.Clear();

                                if (entity.Items == "top")
                                {
                                    ITabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
                                }
                                else if (entity.Items == "left")
                                {
                                    ITabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
                                }
                                else if (entity.Items == "bottom")
                                {
                                    ITabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
                                }
                                else if (entity.Items == "right")
                                {
                                    ITabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
                                }
                            }
                        }
                        else if (ctrlParent is IPatientControl)
                        {
                            IPatientControl ipatctrl = ctrlParent as IPatientControl;

                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                string[] items = entity.Items.Split(new string[] { ConstValue.EVENT_STRING_SPLITER }, StringSplitOptions.None);

                                if (items.Length >= 3)
                                {
                                    ipatctrl.CaptionText = items[0];
                                    ipatctrl.InfoType = (EnumPatientInfoType)Enum.Parse(typeof(EnumPatientInfoType), items[1]);
                                    ipatctrl.ShowCaption = Convert.ToBoolean(items[2]);
                                    //ipatctrl.ShowUnderLine = Convert.ToBoolean(items[3]);
                                }

                                int intCalcAgeType = 0;
                                if (items.Length >= 4)
                                {
                                    int.TryParse(items[3], out intCalcAgeType);
                                }
                                ipatctrl.CalcAgeType = intCalcAgeType;

                                if (items.Length >= 5)
                                {
                                    ipatctrl.BandingPage = Convert.ToBoolean(items[4]);
                                }
                            }
                        }
                        else if (ctrlParent is ISignatureControl)
                        {
                            ISignatureControl iSign = ctrlParent as ISignatureControl;
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                string[] items = entity.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                if (items.Length >= 3)
                                {
                                    iSign.Caption = items[0];
                                    iSign.IsAllowSignNull = Function.Int(items[1]);
                                    iSign.IsAutoSignature = Function.Int(items[2]);
                                }
                            }
                        }
                        else if (ctrlParent is IRtfEditor)
                        {
                            IRtfEditor iRtx = ctrlParent as IRtfEditor;
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                string[] items = entity.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                if (items.Length >= 5)
                                {
                                    iRtx.Multiline = Function.Int(items[0]) == 1 ? true : false;
                                    iRtx.FixedHeight = Function.Int(items[1]) == 1 ? true : false;
                                    iRtx.RowShrinkdigit = Function.Int(items[2]);
                                    iRtx.DefaultRows = Function.Int(items[3]);
                                    iRtx.FirstlineCaption = items[4];
                                }
                            }
                        }
                        else if (ctrlParent is IXtraDateTime)
                        {
                            if (!string.IsNullOrEmpty(entity.Items))
                            {
                                IXtraDateTime idatetime = ctrlParent as IXtraDateTime;
                                string[] items = entity.Items.Split(ConstValue.CONTROL_ITEMS_SPLITER);
                                if (items.Count() > 0)
                                {
                                    if (!string.IsNullOrEmpty(items[0]))//默认时间
                                    {
                                        DateTime dtDefDate = DateTime.Now;
                                        if (DateTime.TryParse(items[0], out dtDefDate))
                                        {
                                            idatetime.DateTimeValue = dtDefDate;
                                        }
                                    }
                                }
                                if (items.Count() > 1)
                                {
                                    if (!string.IsNullOrEmpty(items[1]))
                                    {
                                        idatetime.EditMask = items[1];
                                    }
                                }
                                if (items.Count() > 2)
                                {
                                    if (!string.IsNullOrEmpty(items[2]))
                                    {
                                        idatetime.SPDefaultValue = items[2];
                                    }
                                }
                            }
                        }

                        if (ctrlParent is IRuntimeDesignControl)
                        {
                            IRuntimeDesignControl ictrl = ctrlParent as IRuntimeDesignControl;
                            ictrl.Width = (int)entity.Width;
                            ictrl.Height = (int)entity.Height;
                            ictrl.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);
                            ictrl.Text = entity.Text;
                            ictrl.ForeColor = entity.ForeColor;
                            ictrl.BackColor = entity.BackColor;
                            if (!string.IsNullOrEmpty(entity.TextFont))
                            {
                                ictrl.TextFont = FontSerializationService.Deserialize(entity.TextFont);
                                ctrlParent.Font = FontSerializationService.Deserialize(entity.TextFont);
                            }
                            ictrl.TabIndex = entity.TabIndex;
                            ictrl.PresentationMode = entity.PresentationMode;
                            ictrl.Referencetype = entity.ReferenceType == 1 ? true : false;
                            ictrl.Essential = entity.Essential == 1 ? true : false;
                        }
                        else
                        {
                            ctrlParent.Width = (int)entity.Width;
                            ctrlParent.Height = (int)entity.Height;
                            ctrlParent.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);
                        }

                        var query = from item in listAll
                                    where item.Parent == ctrlParent.Name
                                    select item;

                        foreach (var item in query)
                        {
                            IComponent objChild = DeserializeControl(item, listAll);
                            if (objChild is Control)
                            {
                                if (objChild is DevExpress.XtraTab.XtraTabPage)
                                {
                                    ((DevExpress.XtraTab.XtraTabPage)objChild).Text = item.Text;
                                    (ctrlParent as ITabControl).TabPages.Add(objChild as DevExpress.XtraTab.XtraTabPage);
                                }
                                else
                                {
                                    ctrlParent.Controls.Add(objChild as Control);
                                }
                            }
                        }
                    }
                }
                return instance as IComponent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 序列化控件，包括容器控件的子控件
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public object Serialize(System.Collections.ICollection objects)
        {
            List<EntityFormCtrl> serializedData = new List<EntityFormCtrl>();

            System.Collections.ICollection comps = objects;
            if (comps == null)
                return serializedData;

            Hashtable nametable = new Hashtable();

            serializedData.Clear();
            foreach (IComponent comp in comps)
            {
                //等LayoutControl内部内容都加载到serializedData以后再次处理
                if (!nametable.ContainsKey(comp) && !(comp is DevExpress.XtraTab.XtraTabPage) && !(comp is DevExpress.XtraLayout.LayoutControl))
                {
                    SerializeControl(nametable, string.Empty, comp, serializedData, true);
                }
            }

            foreach (IComponent comp in comps)
            {
                //处理LayoutControl的关系
                if (!nametable.ContainsKey(comp) && !(comp is DevExpress.XtraTab.XtraTabPage))
                {
                    SerializeControl(nametable, string.Empty, comp, serializedData, false);
                }
            }

            SerializeControl(nametable, string.Empty, this.designerHost.RootComponent, serializedData, false);
            return serializedData;
        }

        /// <summary>
        /// (递归)序列化单个控件
        /// </summary>
        /// <param name="nametable"></param>
        /// <param name="parentSiteName"></param>
        /// <param name="level"></param>
        /// <param name="value"></param>
        /// <param name="serializedData"></param>
        private void SerializeControl(Hashtable nametable, string parentSiteName, object value, List<EntityFormCtrl> serializedData, bool recursive)
        {
            try
            {
                IComponent component = value as IComponent;
                EntityFormCtrl entity = new EntityFormCtrl();

                entity.ControlType = value.GetType().AssemblyQualifiedName;

                if (component != null && component.Site != null && component.Site.Name != null)
                {
                    entity.ControlName = component.Site.Name;
                    entity.Parent = parentSiteName;
                    nametable[value] = component.Site.Name;
                }

                bool isControl = (value is Control);
                Control ctrl = value as Control;

                if (isControl)
                {
                    if (ctrl is IRuntimeDesignControl)
                    {
                        IRuntimeDesignControl ictrl = ctrl as IRuntimeDesignControl;
                        entity.Height = (int)ictrl.Height;
                        entity.Width = (int)ictrl.Width;
                        entity.Top = (int)ictrl.Location.Y;
                        entity.Left = (int)ictrl.Location.X;
                        entity.Text = ictrl.Text;
                        entity.ForeColor = ictrl.ForeColor;
                        entity.BackColor = ictrl.BackColor;
                        entity.TabIndex = ictrl.TabIndex;
                        entity.PresentationMode = ictrl.PresentationMode;
                        entity.ReferenceType = ictrl.Referencetype ? 1 : 0;
                        entity.Essential = ictrl.Essential ? 1 : 0;

                        //序列化字体
                        if (ictrl.TextFont != null)
                        {
                            entity.TextFont = FontSerializationService.Serialize(ictrl.TextFont);
                        }
                    }
                    else
                    {
                        entity.Height = (int)ctrl.Height;
                        entity.Width = (int)ctrl.Width;
                        entity.Top = (int)ctrl.Top;
                        entity.Left = (int)ctrl.Left;
                        entity.ForeColor = ctrl.ForeColor;
                        entity.BackColor = ctrl.BackColor;
                    }

                    if (value is IFormCtrl)
                    {
                        IFormCtrl iForm = value as IFormCtrl;
                        entity.ItemName = iForm.ItemName;
                        entity.ItemCaption = iForm.ItemCaption;
                        entity.ItemType = iForm.ItemType;
                        entity.ParentNode = iForm.ParentNode;
                        entity.CalProperty = iForm.CalProperty;
                        entity.RowShrinkDigit = iForm.RowShrinkDigit;
                    }

                    if (string.IsNullOrEmpty(parentSiteName) && ctrl.Parent != null && ctrl.Parent != designerHost.RootComponent)
                    {
                        if (ctrl != designerHost.RootComponent)
                        {
                            entity.Parent = ctrl.Parent.Site.Name;
                        }
                    }

                    if (value is ICheckBox)
                    {
                        ICheckBox iChk = value as ICheckBox;
                        entity.GroupName = iChk.GroupName;
                        entity.SumName = iChk.SumName;
                        entity.CheckedWeightValue = iChk.CheckedWeightValue;
                        entity.Checked = (iChk.Checked ? "1" : "0");
                    }
                    else if (value is ICombox)
                    {
                        ICombox iCbx = value as ICombox;
                        if (iCbx.Items.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (string item in iCbx.Items)
                            {
                                sb.Append(string.Format(ConstValue.CONTROL_ITEMS_SPLITER + "{0}", item));
                            }
                            sb.Remove(0, 1);
                            entity.Items = sb.ToString();
                        }
                    }
                    else if (value is ICtlLine)
                    {
                        ICtlLine iLine = value as ICtlLine;
                        entity.LineStyle = iLine.LineStyle.ToString();
                        entity.LineWidth = iLine.LineWidth;
                    }
                    else if (value is IPictureBox)
                    {
                        IPictureBox iPic = value as IPictureBox;
                        entity.PicFileName = iPic.FileName;
                    }
                    else if (value is IPanel)
                    {
                        IPanel ipnl = ctrl as IPanel;
                        entity.ReserveField = string.Format("{0}{1}{2}", ipnl.Columns, ConstValue.CONTROL_ITEMS_SPLITER, ipnl.Rows);
                        entity.Items = ipnl.BorderStyle.ToString();
                    }
                    else if (ctrl is ITabControl)
                    {
                        ITabControl iTab = ctrl as ITabControl;
                        entity.Items = iTab.HeaderLocation.ToString().ToLower();
                    }
                    else if (ctrl is DevExpress.XtraTab.XtraTabPage)
                    {
                        entity.Text = ((DevExpress.XtraTab.XtraTabPage)ctrl).Text;
                    }
                    else if (ctrl is IXtraDateTime)
                    {
                        IXtraDateTime ictrl = ctrl as IXtraDateTime;
                        entity.Items = string.Format("{0}{1}{2}{3}{4}"
                                                    , ictrl.DateTimeValue
                                                    , ConstValue.CONTROL_ITEMS_SPLITER
                                                    , ictrl.EditMask
                                                    , ConstValue.CONTROL_ITEMS_SPLITER
                                                    , ictrl.SPDefaultValue
                                                    );
                    }
                    else if (ctrl is ISignatureControl)
                    {
                        ISignatureControl iSign = ctrl as ISignatureControl;
                        entity.Items = string.Format("{0}{1}{2}{3}{4}"
                                                    , iSign.Caption
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iSign.IsAllowSignNull.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iSign.IsAutoSignature.ToString());
                        entity.Text = string.Empty;
                    }
                    else if (ctrl is IRtfEditor)
                    {
                        IRtfEditor iRtx = ctrl as IRtfEditor;
                        entity.Items = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}"
                                                    , (iRtx.Multiline ? "1" : "0")
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , (iRtx.FixedHeight ? "1" : "0")
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iRtx.RowShrinkdigit.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iRtx.DefaultRows.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iRtx.FirstlineCaption);
                    }
                    else if (value is IPatientControl)
                    {
                        IPatientControl iPatctrl = ctrl as IPatientControl;
                        entity.Items = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}"
                                                    , iPatctrl.CaptionText
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iPatctrl.InfoType.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iPatctrl.ShowCaption.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iPatctrl.CalcAgeType.ToString()
                                                    , ConstValue.EVENT_STRING_SPLITER
                                                    , iPatctrl.BandingPage.ToString());

                    }
                }

                if (component != null && isControl)
                {
                    if (recursive)
                    {
                        foreach (Control child in ((Control)value).Controls)
                        {
                            if (child.Site != null && child.Site.Container == designerHost.Container)
                            {
                                if (!nametable.ContainsKey(child))
                                {
                                    SerializeControl(nametable, component.Site.Name, child, serializedData, recursive);
                                }
                            }
                        }
                    }
                }
                serializedData.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
