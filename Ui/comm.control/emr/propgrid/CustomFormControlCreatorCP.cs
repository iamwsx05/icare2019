using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Reflection;
using DevExpress.XtraEditors;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 自定义窗体控件生成类
    /// </summary>
    public class CustomFormControlCreatorCP
    {
        public const string EVENT_STRING_SPLITER = "##";

        public const char CONTROL_ITEMS_SPLITER = ';';

        /// <summary>
        /// 已序列化的控件信息
        /// </summary>
        List<EntityCPNode> _controlsdata;

        /// <summary>
        /// 已生成成功的控件
        /// </summary>
        List<IRuntimeDesignControl> _createdControls;       

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="presentationMode">展示方式</param>
        /// <param name="container">存放生成控件的容器</param>
        /// <param name="data">控件布局定义表</param>
        /// <param name="createdControls">ref生成的控件</param>
        public void CreateCntrols(int presentationMode, Control container, List<EntityCPNode> data, ref List<IRuntimeDesignControl> createdControls/*, ref List<ctlTableCase> tableLayouts*/)
        {
            try
            {
                this._controlsdata = data;
                this._createdControls = createdControls;
              
                if (this._createdControls == null)
                {
                    this._createdControls = new List<IRuntimeDesignControl>();
                }

                container.SuspendLayout();

                //container.Controls.Clear();
                Deserialize(container, presentationMode);

                foreach (IRuntimeDesignControl item in createdControls)
                {
                    if (item is ctlLabelEf)
                    {
                        ((ctlLabelEf)item).SendToBack();
                    }
                }
                
                container.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 把数据库记录反序列化为控件
        /// </summary>
        /// <param name="rootControl"></param>
        private void Deserialize(Control rootControl, int presentationMode)
        {
            if (rootControl is XtraScrollableControl)
            {
                //说明:XtraScrollableControl存在bug,先设置坐标再添加控件,如果滚动条位置不为0会影响加入的控件坐标
                XtraScrollableControl scrollControl = rootControl as XtraScrollableControl;
                scrollControl.VerticalScroll.Value = 0;
                scrollControl.HorizontalScroll.Value = 0;
            }

            //找出所有根控件(且不为ctlLayoutControl的分组和单元格)
            //var query = from item in this._controlsdata
            //            where (item.Parent == null || item.Parent.Trim() == string.Empty) && (item.Itemparent_vchr == null || item.Itemparent_vchr.Trim() == string.Empty)
            //            select item;
            

            //生成根控件
            foreach (EntityCPNode entity in this._controlsdata)//query)
            {
                object obj = CreateControl(entity, presentationMode);

                if (obj is Control)
                {
                    Control ctrl = obj as Control;
                    rootControl.Controls.Add(ctrl);

                    if (ctrl is IRuntimeDesignControl)
                    {
                        IRuntimeDesignControl ICtrl = ctrl as IRuntimeDesignControl;
                        ICtrl.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);
                        ICtrl.Width = (int)entity.Width;
                        ICtrl.Height = (int)entity.Height;
                    }
                    else
                    {
                        ctrl.Location = new System.Drawing.Point((int)entity.Left, (int)entity.Top);
                        ctrl.Width = (int)entity.Width;
                        ctrl.Height = (int)entity.Height;
                    }
                }
            }            
        }


        /// <summary>
        /// 递归创建控件(单个)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private object CreateControl(EntityCPNode entityParent, int presentationMode)
        {
            try
            {
                if (entityParent.ControlName == typeof(DesignPanel).Name + "1")
                {
                    return null;
                }

                Type type = Type.GetType(entityParent.ControlType);  
                object instance = null;
                if (type != null)
                {
                    instance = Activator.CreateInstance(type);

                    if (instance != null && instance is Control)
                    {
                        Control ctrlParent = instance as Control;

                        ctrlParent.Name = entityParent.ControlName;
                        ctrlParent.Text = entityParent.NodeDesc;
                        ctrlParent.ForeColor = entityParent.ForeColor;                        
                        ctrlParent.BringToFront();                       
                                               
                        if (ctrlParent is IRuntimeDesignControl)
                        {
                            try
                            {
                                IRuntimeDesignControl ictrl = ctrlParent as IRuntimeDesignControl;

                                this._createdControls.Add(ictrl);

                                if (ictrl is ICpNode)
                                {
                                    ICpNode iNode = ictrl as ICpNode;

                                    iNode.NodeName = entityParent.NodeName;                                    
                                    iNode.NodeType = entityParent.NodeType;
                                    iNode.NodeDays = entityParent.NodeDays;
                                    iNode.ParentNodeName = entityParent.ParentNodeName;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        //查找当前控件的子控件
                        //var query = from item in this._controlsdata
                        //            where item.Parent == ctrlParent.Name
                        //            select item;

                        foreach (var itemChild in this._controlsdata)//query)
                        {
                            object obj = CreateControl(itemChild, presentationMode);
                            Control ctrlChild = obj as Control;
                            if (ctrlChild != null)
                            {
                                    ctrlParent.Controls.Add(ctrlChild);

                                if (ctrlChild is IRuntimeDesignControl)
                                {
                                    IRuntimeDesignControl ICtrl = ctrlChild as IRuntimeDesignControl;
                                    ICtrl.Location = new System.Drawing.Point((int)itemChild.Left, (int)itemChild.Top);
                                    ICtrl.Width = (int)itemChild.Width;
                                    ICtrl.Height = (int)itemChild.Height;
                                }
                                else
                                {
                                    ctrlChild.Location = new System.Drawing.Point((int)itemChild.Left, (int)itemChild.Top);
                                    ctrlChild.Width = (int)itemChild.Width;
                                    ctrlChild.Height = (int)itemChild.Height;
                                }

                            }
                        }
                    }
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
