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

namespace Common.Controls.Emr
{
    /// <summary>
    /// 控件序列化/反序列化服务
    /// </summary>
    public class DesignerSerializationServiceCP : IDesignerSerializationService
    {
        IDesignerHost designerHost;

        public DesignerSerializationServiceCP(IDesignerHost host)
        {
            designerHost = host;
        }

        #region IDesignerSerializationService 成员

        private List<EntityCPNode> GetRootControls(List<EntityCPNode> listAllComps)
        {
            List<EntityCPNode> roots = new List<EntityCPNode>();
            foreach (EntityCPNode item in listAllComps)
            {
                roots.Add(item);
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
                List<EntityCPNode> data = serializationData as List<EntityCPNode>;
                if (data == null)
                    return null;

                List<EntityCPNode> temp = GetRootControls(data);
                List<Component> comp = new List<Component>();

                foreach (EntityCPNode entity in temp)
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

        private string CreateControlName(string prevName, IContainer container, Type dataType, List<EntityCPNode> listAllComps)
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
                    foreach (EntityCPNode item in listAllComps)
                    {
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
        private IComponent DeserializeControl(EntityCPNode entity, List<EntityCPNode> listAll)
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

                        if (ctrlParent is ICpNode)
                        {
                            ICpNode iNode = ctrlParent as ICpNode;
                            iNode.NodeName = entity.NodeName;                            
                            iNode.NodeType = entity.NodeType;
                            iNode.NodeDays = entity.NodeDays;
                            iNode.ParentNodeName = entity.ParentNodeName;
                        }

                        if (ctrlParent is IRuntimeDesignControl)
                        {
                            IRuntimeDesignControl ictrl = ctrlParent as IRuntimeDesignControl;
                            ictrl.Width = (int)entity.Width;
                            ictrl.Height = (int)entity.Height;
                            ictrl.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);
                            ictrl.Text = entity.NodeDesc;
                            ictrl.ForeColor = entity.ForeColor;
                            if (!string.IsNullOrEmpty(entity.TextFont))
                            {
                                ictrl.TextFont = FontSerializationService.Deserialize(entity.TextFont);
                                ctrlParent.Font = FontSerializationService.Deserialize(entity.TextFont);
                            }
                        }
                        else
                        {
                            ctrlParent.Width = (int)entity.Width;
                            ctrlParent.Height = (int)entity.Height;
                            ctrlParent.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);                            
                        }

                        //var query = from item in listAll
                        //            where item.Parent == ctrlParent.Name
                        //            select item;

                        //foreach (var item in query)
                        //{
                        //    IComponent objChild = DeserializeControl(item, listAll);
                        //    if (objChild is Control)
                        //    {
                        //        if (objChild is DevExpress.XtraTab.XtraTabPage)
                        //        {
                        //            (ctrlParent as ITabControl).TabPages.Add(objChild as DevExpress.XtraTab.XtraTabPage);
                        //        }
                        //        else
                        //        {
                        //            ctrlParent.Controls.Add(objChild as Control);
                        //        }
                        //    }
                        //}
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
            List<EntityCPNode> serializedData = new List<EntityCPNode>();

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
                if (!nametable.ContainsKey(comp) && !(comp is DevExpress.XtraTab.XtraTabPage) )
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
        private void SerializeControl(Hashtable nametable, string parentSiteName, object value, List<EntityCPNode> serializedData, bool recursive)
        {
            try
            {
                IComponent component = value as IComponent;
                EntityCPNode entity = new EntityCPNode();
                
                entity.ControlType = value.GetType().AssemblyQualifiedName;

                if (component != null && component.Site != null && component.Site.Name != null)
                {
                    entity.ControlName = component.Site.Name;
                    //entity.Parent = parentSiteName;
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
                        entity.NodeDesc = ictrl.Text;
                        entity.ForeColor = ictrl.ForeColor;
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
                    }

                    if (value is ICpNode)
                    {
                        ICpNode iNode = value as ICpNode;

                        entity.NodeName = iNode.NodeName;                        
                        entity.NodeType = iNode.NodeType;
                        entity.NodeDays = iNode.NodeDays;
                        entity.ParentNodeName = iNode.ParentNodeName;
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

                throw;
            }

        }
        #endregion
    }
}
