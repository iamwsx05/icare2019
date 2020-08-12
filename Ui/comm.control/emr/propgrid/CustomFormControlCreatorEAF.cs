using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Reflection;
using DevExpress.XtraEditors;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 自定义窗体控件生成类
    /// </summary>
    public class CustomFormControlCreatorForm
    {
        /// <summary>
        /// 已序列化的控件信息
        /// </summary>
        List<EntityFormCtrl> _controlsdata;

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
        public void CreateControls(int presentationMode, Control container, List<EntityFormCtrl> data, ref List<IRuntimeDesignControl> createdControls/*, ref List<ctlTableCase> tableLayouts*/)
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
            return;
            if (rootControl is XtraScrollableControl)
            {
                //说明:XtraScrollableControl存在bug,先设置坐标再添加控件,如果滚动条位置不为0会影响加入的控件坐标
                XtraScrollableControl scrollControl = rootControl as XtraScrollableControl;
                scrollControl.VerticalScroll.Value = 0;
                scrollControl.HorizontalScroll.Value = 0;
            }

            //找出所有根控件(且不为ctlLayoutControl的分组和单元格)
            var query = from item in this._controlsdata
                        where (item.Parent == null || item.Parent.Trim() == string.Empty) && (item.ItemParent == null || item.ItemParent.Trim() == string.Empty)
                        select item;


            //生成根控件
            foreach (EntityFormCtrl entity in query)
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
                        if (!string.IsNullOrEmpty(entity.TextFont))
                        {
                            ICtrl.TextFont = FontSerializationService.Deserialize(entity.TextFont);
                        }
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
        private object CreateControl(EntityFormCtrl entityParent, int presentationMode)
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
                        ctrlParent.Text = entityParent.Text;
                        ctrlParent.ForeColor = entityParent.ForeColor;
                        ctrlParent.BackColor = entityParent.BackColor;
                        ctrlParent.BringToFront();

                        if (ctrlParent is IRuntimeDesignControl)
                        {
                            try
                            {
                                IRuntimeDesignControl ictrl = ctrlParent as IRuntimeDesignControl;
                                this._createdControls.Add(ictrl);
                                ((IRuntimeDesignControl)ictrl).TabIndex = entityParent.TabIndex;
                                ((IRuntimeDesignControl)ictrl).PresentationMode = entityParent.PresentationMode;
                                ((IRuntimeDesignControl)ictrl).Referencetype = entityParent.ReferenceType == 1 ? true : false;
                                ((IRuntimeDesignControl)ictrl).Essential = entityParent.Essential == 1 ? true : false;
                                if (ictrl is IFormCtrl)
                                {
                                    IFormCtrl iForm = ictrl as IFormCtrl;

                                    iForm.ItemName = entityParent.ItemName;
                                    iForm.ItemCaption = entityParent.ItemCaption;
                                    iForm.ItemType = entityParent.ItemType;
                                    iForm.ParentNode = entityParent.ParentNode;
                                    iForm.CalProperty = entityParent.CalProperty;
                                    iForm.RowShrinkDigit = entityParent.RowShrinkDigit;                                    

                                    if (ctrlParent is ctlTextBox)
                                    {
                                        ctrlParent.BringToFront();
                                    }
                                }

                                if (ictrl is ICheckBox)
                                {
                                    ICheckBox iChk = ictrl as ICheckBox;

                                    iChk.GroupName = entityParent.GroupName;
                                    iChk.SumName = entityParent.SumName;
                                    iChk.CheckedWeightValue = entityParent.CheckedWeightValue;
                                    iChk.Checked = (entityParent.Checked == "1" ? true : false);
                                }
                                else if (ictrl is ICombox)
                                {
                                    if (!string.IsNullOrEmpty(entityParent.Items))
                                    {
                                        ICombox iCbx = ictrl as ICombox;
                                        string[] items = entityParent.Items.Split(EmrTool.CONTROL_ITEMS_SPLITER);
                                        foreach (string i in items)
                                        {
                                            iCbx.Items.Add(i);
                                        }
                                    }
                                }
                                else if (ictrl is ICtlLine)
                                {
                                    ICtlLine iLine = ictrl as ICtlLine;

                                    iLine.LineStyle = (CtlLineStyle)Enum.Parse(typeof(CtlLineStyle), entityParent.LineStyle);
                                    iLine.LineWidth = entityParent.LineWidth;
                                }
                                else if (ictrl is IPictureBox)
                                {
                                    IPictureBox iPic = ictrl as IPictureBox;

                                    iPic.FileName = entityParent.PicFileName;
                                }
                                else if (ictrl is IPanel)
                                {
                                    IPanel ipnl = ctrlParent as IPanel;
                                    if (!string.IsNullOrEmpty(entityParent.ReserveField))
                                    {
                                        string[] items = entityParent.ReserveField.Split(EmrTool.CONTROL_ITEMS_SPLITER);
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
                                        //entity.ReserveField = string.Format("{0}{1}{2}", ipnl.Columns, ConstValue.CONTROL_ITEMS_SPLITER, ipnl.Rows);
                                        //entity.Items = ipnl.BorderStyle.ToString().ToLower();
                                        ipnl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), entityParent.Items);
                                    }
                                    catch (Exception ex)
                                    {
                                        weCare.Core.Utils.ExceptionLog.OutPutException(ex);
                                    }
                                }
                                else if (ctrlParent is ITabControl)
                                {
                                    ITabControl iTabCtrl = ctrlParent as ITabControl;

                                    ctlTabControl tabctrl = ctrlParent as ctlTabControl;
                                    tabctrl.AppearancePage.Header.Font = new System.Drawing.Font("宋体", 9.5f);

                                    if (entityParent.Items == "top")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
                                    }
                                    else if (entityParent.Items == "left")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
                                    }
                                    else if (entityParent.Items == "bottom")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
                                    }
                                    else if (entityParent.Items == "right")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
                                    }
                                }
                                else if (ictrl is IPatientControl)
                                {
                                    IPatientControl ipatctrl = ctrlParent as IPatientControl;
                                    if (!string.IsNullOrEmpty(entityParent.Items))
                                    {
                                        string[] items = entityParent.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                        if (items.Length >= 3)
                                        {
                                            ipatctrl.CaptionText = items[0];
                                            ipatctrl.InfoType = (EnumPatientInfoType)Enum.Parse(typeof(EnumPatientInfoType), items[1]);
                                            ipatctrl.ShowCaption = Convert.ToBoolean(items[2]);
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
                                else if (ictrl is IXtraDateTime)
                                {
                                    if (!string.IsNullOrEmpty(entityParent.Items))
                                    {
                                        IXtraDateTime idatetime = ctrlParent as IXtraDateTime;
                                        string[] items = entityParent.Items.Split(EmrTool.CONTROL_ITEMS_SPLITER);   //(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);

                                        if (items.Count() > 0)
                                        {
                                            if (!string.IsNullOrEmpty(items[0]))//默认时间
                                            {
                                                DateTime dtDefDate = DateTime.Now;

                                                if (DateTime.TryParse(items[0], out dtDefDate))
                                                {
                                                    idatetime.DateTimeValue = dtDefDate;
                                                    if (ctrlParent is IFormCtrl)
                                                    {
                                                        ((IFormCtrl)ctrlParent).ValueChangedFlag = false;
                                                    }
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
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            ctrlParent.Text = entityParent.Text; // .DesignTimeText;
                            ctrlParent.BackColor = entityParent.BackColor;
                            ctrlParent.ForeColor = entityParent.ForeColor;
                        }

                        //查找当前控件的子控件
                        var query = from item in this._controlsdata
                                    where item.Parent == ctrlParent.Name
                                    select item;

                        foreach (var itemChild in query)
                        {
                            object obj = CreateControl(itemChild, presentationMode);
                            Control ctrlChild = obj as Control;
                            if (ctrlChild != null)
                            {
                                if (ctrlParent is ITabControl && ctrlChild is DevExpress.XtraTab.XtraTabPage)
                                {
                                    ITabControl tabCtrl = ctrlParent as ITabControl;
                                    DevExpress.XtraTab.XtraTabPage tabpage = ctrlChild as DevExpress.XtraTab.XtraTabPage;
                                    tabpage.Text = itemChild.Text;
                                    tabCtrl.TabPages.Add(tabpage);
                                }
                                else
                                {
                                    ctrlParent.Controls.Add(ctrlChild);
                                }

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
                throw ex;
            }
        }
    }
}
