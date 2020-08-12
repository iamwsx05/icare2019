using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Common.Controls.Emr
{
    public class MenuCommandServiceImpl : MenuCommandService
    {
        DesignerVerbCollection m_globalVerbs = new DesignerVerbCollection();

        public MenuCommandServiceImpl(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            //添加菜单操作谓词
            m_globalVerbs.Add(StandartVerb("选择全部", StandardCommands.SelectAll));
            m_globalVerbs.Add(null);//null代表生成右键菜单时插入分割符

            m_globalVerbs.Add(StandartVerb("置顶", StandardCommands.BringToFront));
            m_globalVerbs.Add(StandartVerb("置底", StandardCommands.SendToBack));
            m_globalVerbs.Add(null);

            //剪切,复制,粘贴需特殊处理
            m_globalVerbs.Add(new DesignerVerb("剪切", Cut, StandardCommands.Cut));
            m_globalVerbs.Add(new DesignerVerb("复制", Copy, StandardCommands.Copy));
            m_globalVerbs.Add(new DesignerVerb("粘贴", Paste, StandardCommands.Paste));

            m_globalVerbs.Add(StandartVerb("删除", StandardCommands.Delete));
        }

        public override bool GlobalInvoke(CommandID commandID)
        {
            bool b = base.GlobalInvoke(commandID);
            if (commandID == StandardCommands.Copy)
            {
                Copy(commandID, EventArgs.Empty);
            }
            else if (commandID == StandardCommands.Cut)
            {
                Cut(commandID, EventArgs.Empty);
            }
            else if (commandID == StandardCommands.Paste)
            {
                Paste(commandID, EventArgs.Empty);
            }
            else if (commandID == StandardCommands.Undo)
            {
                IDesignerHost idh = GetService(typeof(IDesignerHost)) as IDesignerHost;
                UndoEngineImpl undoEngine = idh.GetService(typeof(UndoEngine)) as UndoEngineImpl;
                undoEngine.Undo();
            }
            else if (commandID == StandardCommands.SendToBack)
            {
                ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            }
            else if (commandID == StandardCommands.BringToFront)
            {
                ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            }
            return b;
        }


        /// <summary>
        /// 添加谓词委托
        /// </summary>
        /// <param name="text"></param>
        /// <param name="commandID"></param>
        /// <returns></returns>
        private DesignerVerb StandartVerb(string text, CommandID commandID)
        {
            return new DesignerVerb(text, delegate(object o, EventArgs e)
            {
                IMenuCommandService ms = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
                bool success = ms.GlobalInvoke(commandID);
            }
                );
        }

        /// <summary>
        /// 使控件宽度相等
        /// </summary>
        public void HorizSpaceMakeEqual()
        {
            ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            IDesignerHost idh = GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cs = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (selection.SelectionCount > 1 && selection.PrimarySelection != idh.RootComponent)
            {
                Control pControl = selection.PrimarySelection as Control;

                foreach (Control ctrl in selection.GetSelectedComponents())
                {
                    if (ctrl != pControl)
                    {
                        PropertyDescriptor prop = TypeDescriptor.GetProperties(ctrl.GetType())["Width"];

                        cs.OnComponentChanging(ctrl, prop);
                        int oldWidth = ctrl.Width;
                        ctrl.Width = pControl.Width;
                        cs.OnComponentChanged(ctrl, prop, oldWidth, ctrl.Width);
                    }
                }
            }
        }

        /// <summary>
        /// 使控件高度相等
        /// </summary>
        public void VertSpaceMakeEqual()
        {
            ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            IDesignerHost idh = GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cs = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (selection.SelectionCount > 1 && selection.PrimarySelection != idh.RootComponent)
            {
                Control pControl = selection.PrimarySelection as Control;

                foreach (Control ctrl in selection.GetSelectedComponents())
                {
                    if (ctrl != pControl)
                    {
                        PropertyDescriptor prop = TypeDescriptor.GetProperties(ctrl.GetType())["Height"];

                        cs.OnComponentChanging(ctrl, prop);
                        int oldHeight = ctrl.Height;
                        ctrl.Height = pControl.Height;
                        cs.OnComponentChanged(ctrl, prop, oldHeight, ctrl.Height);
                    }
                }
            }
        }

        /// <summary>
        /// 使控件大小相等
        /// </summary>
        public void HorizVertSpaceMakeEqual()
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host == null)
                return;

            //启用设计器事务
            using (DesignerTransaction transaction = host.CreateTransaction("HorizVertSpaceMakeEqual"))
            {
                HorizSpaceMakeEqual();
                VertSpaceMakeEqual();

                transaction.Commit();
            }
        }


        #region 拷贝、复制、粘贴

        /// <summary>
        /// 剪贴板数据
        /// </summary>
        private object clipboardData = null;

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Copy(object sender, EventArgs args)
        {
            //序列化服务
            IDesignerSerializationService serialService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;

            //设计宿主
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            //选择服务
            ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            ICollection selectedComponents = selection.GetSelectedComponents();

            ArrayList toCopy = new ArrayList();

            //获取当前选中的组件序列化
            foreach (object component in selectedComponents)
            {
                if (component == host.RootComponent)
                    continue;
                toCopy.Add(component);
                ComponentDesigner designer = host.GetDesigner((IComponent)component) as ComponentDesigner;
                if (designer != null && designer.AssociatedComponents != null)
                    toCopy.AddRange(designer.AssociatedComponents);
            }
            List<EntityCPNode> stateData = serialService.Serialize(toCopy) as List<EntityCPNode>;
            clipboardData = stateData;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Cut(object sender, EventArgs args)
        {
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (host == null)
                return;

            //启用设计器事务
            using (DesignerTransaction transaction = host.CreateTransaction("Cut"))
            {
                //复制
                Copy(this, EventArgs.Empty);

                //删除
                this.GlobalInvoke(StandardCommands.Delete);
                transaction.Commit();
            }

        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Paste(object sender, EventArgs args)
        {
            IDesignerSerializationService serialService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            IComponentChangeService changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

            if (host == null || serialService == null || clipboardData == null)
                return;

            //从获取上次剪切/复制的数据并反序列化
            ICollection components = serialService.Deserialize(clipboardData);

            //如果当前选中的控件是容器控件则添加到容器中
            //否则添加到根设计器中
            if (components != null && components.Count > 0)
            {
                DesignerTransaction transaction = host.CreateTransaction("Paste");

                foreach (Component item in components)
                {
                    Control control = item as Control;
                    if (control == null)
                        continue;

                    //if (control is IDBColProperty)
                    //{
                    //    (control as IDBColProperty).DBColName = string.Empty;
                    //}

                    PropertyDescriptor parentProperty = TypeDescriptor.GetProperties(control)["Parent"];

                    //获取粘贴到的父容器
                    ParentControlDesigner parentDesigner = null;
                    if (selection != null && selection.PrimarySelection != null)
                        parentDesigner = host.GetDesigner((IComponent)selection.PrimarySelection) as ParentControlDesigner;

                    if (parentDesigner == null)
                    {
                        parentDesigner = host.GetDesigner(host.RootComponent) as DocumentDesigner;
                    }

                    if (parentDesigner != null && parentDesigner.CanParent(control))
                    {
                        //粘贴时粘贴到父容器中间
                        //control.Location = new Point(parentDesigner.Control.Width / 2 - control.Width / 2, parentDesigner.Control.Height / 2 - control.Height / 2);
                        parentProperty.SetValue(control, parentDesigner.Control);
                    }

                }

                clipboardData = null;
                transaction.Commit();
                ((IDisposable)transaction).Dispose();

                selection.SetSelectedComponents(components);
                this.GlobalInvoke(StandardCommands.BringToFront);
            }
        }
        #endregion

        /// <summary>
        /// 生成右键菜单
        /// </summary>
        /// <returns></returns>
        private ToolStripItem[] BuildMenuItems()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            foreach (DesignerVerb verb in m_globalVerbs)
            {
                if (verb == null)
                {
                    items.Add(new ToolStripSeparator());

                }
                else
                {
                    MenuItem i = new MenuItem(verb);
                    items.Add(i);
                    //Object obj = ResPic.ResourceManager.GetObject("Pic" + verb.Text);
                    //if (obj != null && obj is Bitmap)
                    //{
                    //    i.Image = obj as Bitmap;
                    //}
                }
            }
            return items.ToArray();
        }

        /// <summary>
        /// 弹出右键菜单
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void ShowContextMenu(System.ComponentModel.Design.CommandID menuID, int x, int y)
        {
            ISelectionService selection = GetService(typeof(ISelectionService)) as ISelectionService;
            IDesignerHost idh = GetService(typeof(IDesignerHost)) as IDesignerHost;

            //if (selection.PrimarySelection != idh.RootComponent)
            //{
            ContextMenuStrip cm = new ContextMenuStrip();
            cm.Items.AddRange(BuildMenuItems());
            ISelectionService ss = GetService(typeof(ISelectionService)) as ISelectionService;

            Control ps = ss.PrimarySelection as Control;

            if (ps != null)
            {
                Point s = ps.PointToScreen(new Point(0, 0));
                cm.Show(ps, new Point(x - s.X, y - s.Y));
            }
            //}
        }

        /// <summary>
        /// 菜单项
        /// </summary>
        internal class MenuItem : ToolStripMenuItem
        {
            DesignerVerb verb;
            public MenuItem(DesignerVerb verb)
                : base(verb.Text)
            {
                Enabled = verb.Enabled;
                this.verb = verb;
                Click += InvokeCommand;
            }

            void InvokeCommand(object sender, EventArgs e)
            {
                try
                {
                    verb.Invoke();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}

