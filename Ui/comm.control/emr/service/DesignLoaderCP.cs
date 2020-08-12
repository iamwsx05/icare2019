using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 设计器加载器:加载持久化数据生成控件并添加到设计面板中；把当前控件持久化
    /// 
    /// </summary>
    public class DesignLoaderCP : BasicDesignerLoader
    {
        IDesignerHost Host;
        int _templateControlFormID = -1;
        int _formVersion = 0;
        public bool Isdirty { get; private set; }
        public bool Unsave { get; private set; }

        public List<EntityCPNode> _data;

        public int InitWidth { get; set; }
        public int InitHeight { get; set; }
        public bool EfFlag { get; set; }

        public DesignLoaderCP(IDesignerHost host, List<EntityCPNode> data)
            : this(host, 0, -1, data)
        {

        }

        public DesignLoaderCP(IDesignerHost host, int templateControlFormID, int formversion, List<EntityCPNode> data)
        {
            Host = host;
            _data = data;
            _templateControlFormID = templateControlFormID;
            _formVersion = formversion;
            this.Isdirty = true;
            this.Unsave = false;
        }

        protected override void PerformFlush(IDesignerSerializationManager serializationManager)
        {
            if (!Isdirty)
            {
                return;
            }

            PerformFlushWorker();
        }

        public void PerformFlushWorker()
        {
            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            IDesignerSerializationService serService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;

            List<IComponent> comps = new List<IComponent>();

            foreach (IComponent comp in idh.Container.Components)
            {
                if (comp != idh.RootComponent)// && !nametable.ContainsKey(comp))
                {
                    comps.Add(comp);
                }
            }

            this._data = serService.Serialize(comps) as List<EntityCPNode>;
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="serializationManager"></param>
        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            try
            {
                ArrayList errors = new ArrayList();
                bool successful = true;

                //获取"控件更改"服务
                IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                cs.ComponentAdded -= new ComponentEventHandler(cs_ComponentAdded);
                cs.ComponentRemoved -= new ComponentEventHandler(cs_ComponentRemoved);
                cs.ComponentChanged -= new ComponentChangedEventHandler(cs_ComponentChanged);

                cs.ComponentAdded += new ComponentEventHandler(cs_ComponentAdded);
                cs.ComponentRemoved += new ComponentEventHandler(cs_ComponentRemoved);
                cs.ComponentChanged += new ComponentChangedEventHandler(cs_ComponentChanged);

                //加载设计面板
                DesignPanel ctrlContainer = null;
                if (this.Host.RootComponent == null)
                {
                    ctrlContainer = Host.CreateComponent(typeof(DesignPanel)) as DesignPanel;
                }
                else//如果设计面板不为空，则为导入数据
                {
                    ctrlContainer = this.Host.RootComponent as DesignPanel;
                    ctrlContainer.Controls.Clear();
                }

                //设置设计面板最大尺寸
                ctrlContainer.MaximumSize = new System.Drawing.Size(ConstValue.DesignPanelMaxWidth, ctrlContainer.MaximumSize.Height);
                if (this._data != null && this._data.Any(i => i.ControlName.Contains(typeof(DesignPanel).Name)))
                {
                    EntityCPNode entityRoot = this._data.Find(i => i.ControlName.Contains(typeof(DesignPanel).Name));
                    ctrlContainer.Width = (int)entityRoot.Width;
                    ctrlContainer.Height = (int)entityRoot.Height;
                    this._data.Remove(entityRoot);
                }
                else
                {
                    ctrlContainer.Width = (this.InitWidth > 0 ? this.InitWidth : System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - 420);
                    ctrlContainer.Height = (this.InitHeight > 0 ? this.InitHeight : 162);
                }
                ctrlContainer.BackColor = (this.EfFlag ? Color.White : Color.FromArgb(231, 240, 249)); //Color.FromArgb(9, 111, 133));

                #region 未确定代码

                if (_templateControlFormID > 0)
                {
                    //导入模板控件
                    List<EntityCPNode> tempControl = null;// FormLoader.GetControls(_templateControlFormID, ref _formVersion);
                    CustomFormControlCreatorCP creator = new CustomFormControlCreatorCP();

                    List<IRuntimeDesignControl> t = new List<IRuntimeDesignControl>();
                    creator.CreateCntrols(0, ctrlContainer, tempControl, ref t);
                }
                #endregion


                ctrlContainer.designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;

                //反序列化生成控件
                Deserialize(ctrlContainer, errors);
                //}

                this.LoaderHost.EndLoad(typeof(DesignPanel).Name, successful, errors);
                Isdirty = false;
                Unsave = false;
            }
            catch (Exception ex)
            {
                weCare.Core.Utils.ExceptionLog.OutPutException(ex);
                throw;
            }
        }

        /// <summary>
        /// 反序列化(导入)
        /// </summary>
        /// <param name="data"></param>
        public void Deserialize(List<EntityCPNode> data)
        {
            this._data = data;

            PerformLoad(null);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="rootControl"></param>
        /// <param name="errors"></param>
        private void Deserialize(Control rootControl, ArrayList errors)
        {
            try
            {
                //获取序列化反序列化服务
                IDesignerSerializationService serService = this.GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;

                //生成控件
                List<Component> comps = serService.Deserialize(this._data) as List<Component>;

                foreach (Component com in comps)
                {
                    if (com is Control)
                    {
                        //加载到设计面板
                        rootControl.Controls.Add(com as Control);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
        }

        void cs_ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            Isdirty = true;
            Unsave = true;
            if (ComponentChanged != null)
            {
                ComponentChanged(this, e);
            }
        }

        void cs_ComponentAdded(object sender, ComponentEventArgs e)
        {
            Isdirty = true;
            Unsave = true;
            if (ComponentAdded != null)
            {
                ComponentAdded(this, e);
            }
        }

        void cs_ComponentRemoved(object sender, ComponentEventArgs e)
        {
            Isdirty = true;
            Unsave = true;

            if (ComponentRemoved != null)
            {
                ComponentRemoved(this, e);
            }
        }

        #region 事件

        public event ComponentEventHandler ComponentAdded;
        public event ComponentChangedEventHandler ComponentChanged;
        public event ComponentEventHandler ComponentRemoved;

        #endregion

        public List<EntityCPNode> Save()
        {
            GetSerializeData();
            this.Isdirty = false;
            this.Unsave = false;
            return _data;
        }

        public List<EntityCPNode> GetSerializeData()
        {
            this.PerformFlush(null);
            return _data;
        }

        public override void Dispose()
        {
            //base.Dispose();

            IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

            if (cs != null)
            {
                cs.ComponentChanged -= new ComponentChangedEventHandler(cs_ComponentChanged);
                cs.ComponentAdded -= new ComponentEventHandler(cs_ComponentAdded);
                cs.ComponentRemoved -= new ComponentEventHandler(cs_ComponentRemoved);
            }
        }
    }
}
