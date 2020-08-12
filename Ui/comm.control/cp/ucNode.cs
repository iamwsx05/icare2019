using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls.Emr;

namespace Common.Controls
{
    /// <summary>
    /// HandleNodeMouseLeftClick
    /// </summary>
    /// <param name="node"></param>
    public delegate void HandleNodeMouseLeftClick(EntityCPNode node);
    public delegate void HandleNodeMouseRightClick(EntityCPNode node);
    public delegate void HandleNodeReturnClick();

    /// <summary>
    /// ucNode
    /// </summary>
    public partial class ucNode : UserControl, IRuntimeDesignControl, ICpNode
    {
        #region ICPNode属性
        /// <summary>
        /// 节点名称
        /// </summary>
        [Category("ICPNode属性"), Description("节点名称")]
        public string NodeName
        {
            get;
            set;
        }

        private string _Text = string.Empty;

        /// <summary>
        /// 节点标题
        /// </summary>
        [Category("ICPNode属性"), Description("节点标题")]
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.lblMark.Visible = false;
                }
                else
                {
                    this.lblMark.Text = value;
                    this.lblMark.Visible = true;
                }
            }
        }

        /// <summary>
        /// 节点值
        /// </summary>
        [Category("ICPNode属性"), Description("节点值")]
        public string NodeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点名
        /// </summary>
        [Category("ICPNode属性"), Description("父节点")]
        public string ParentNodeName
        {
            get;
            set;
        }

        private string _NodeType = "1";
        /// <summary>
        /// 节点类型
        /// </summary>
        [Category("ICPNode属性"), Description("节点类型")]
        public string NodeType
        {
            get { return _NodeType; }
            set
            {
                _NodeType = value;
                Image img = null;
                if (value == "0")
                {
                    img = Properties.Resources._1_入;//.入院L;
                }
                else if (value == "1")
                {
                    img = Properties.Resources._2_住;//.住院L;
                }
                else if (value == "2")
                {
                    img = Properties.Resources._3_术;//.手术L;
                }
                else if (value == "3")
                {
                    img = Properties.Resources._6_出;//.出院L;
                }
                else
                {
                    img = Properties.Resources._2_住;//.住院L;
                }
                this.picCaption.Image = img;
            }
        }

        private string _NodeDays = string.Empty;

        /// <summary>
        /// 节点天/日
        /// </summary>
        [Category("ICPNode属性"), Description("节点天/日")]
        public string NodeDays
        {
            get { return _NodeDays; }
            set
            {
                _NodeDays = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.lblDay.Text = "第" + value + "天";
                }
                else
                {
                    this.lblDay.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// ForeColor
        /// </summary>
        public System.Drawing.Color ForeColor { get; set; }

        public event HandleNodeMouseLeftClick NodeMouseLeftClick;

        public event HandleNodeMouseRightClick NodeMouseRightClick;

        public event HandleNodeReturnClick NodeReturnClick;

        public bool IsExec { get; set; }

        public DateTime? ExecDateTime { get; set; }

        /// <summary>
        /// 是否末节点(叶子节点)
        /// </summary>
        public bool IsLeafNode { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanBeginDate { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndDate { get; set; }

        /// <summary>
        /// 是否设计模式
        /// </summary>
        [Browsable(false)]
        public bool IsDesign { get; set; }

        /// <summary>
        /// 执行轨迹
        /// </summary>
        [Browsable(false)]
        public List<EntityExec> lstExecTrack { get; set; }

        /// <summary>
        /// 当前执行节点名称
        /// </summary>
        [Browsable(false)]
        public string CurrExecNodeName { get; set; }
             
        /// <summary>
        /// 是否首节点
        /// </summary>
        private bool IsFirstNode
        {
            get
            {
                if (this.Tag != null)
                {
                    return (this.Tag as EntityCPNode).ParentNodeName == "^" ? true : false;
                }
                return false;
            }
        }

        #endregion

        public ucNode()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.Cursor = Cursors.Hand;
        }

        public bool IsNodeClick { get; set; }

        public void ucNode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (NodeMouseLeftClick != null)
                {
                    IsNodeClick = true;

                    if (this.Tag == null)
                        NodeMouseLeftClick(null);
                    else
                        NodeMouseLeftClick(this.Tag as EntityCPNode);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (NodeMouseRightClick != null)
                {
                    IsNodeClick = true;

                    if (!this.IsDesign && lstExecTrack != null)
                    {
                        bool b1 = lstExecTrack.Exists(t => t.NodeName == this.NodeName);
                        if (this.NodeName == this.CurrExecNodeName || !b1)
                        {
                            //bool b2 = CpTools.GetCpFirstNode(lstExecTrack[0].ExecID, true, this.NodeName).NodeName == this.NodeName ? true : false;
                            if (!b1 || this.IsFirstNode)
                            {
                                blbiReturn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                blbiReturn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            popupMenu.ShowPopup(Control.MousePosition);
                        }
                    }
                }
            }
        }

        private void blbiAdjust_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (NodeMouseRightClick != null)
            {
                if (this.Tag == null)
                    NodeMouseRightClick(null);
                else
                    NodeMouseRightClick(this.Tag as EntityCPNode);
            }
        }

        private void blbiReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (NodeReturnClick != null)
            {
                NodeReturnClick();
            }
        }

        private void SetBorder()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void CancelBorder()
        {
            if (!IsNodeClick)
            {
                this.BorderStyle = BorderStyle.None;
            }
        }

        private void ucNode_MouseEnter(object sender, EventArgs e)
        {
            SetBorder();
        }

        private void ucNode_MouseLeave(object sender, EventArgs e)
        {
            CancelBorder();
        }

        private void picCaption_MouseEnter(object sender, EventArgs e)
        {
            SetBorder();
        }

        private void picCaption_MouseLeave(object sender, EventArgs e)
        {
            CancelBorder();
        }

        private void lblInfo_MouseEnter(object sender, EventArgs e)
        {
            SetBorder();
        }

        private void lblInfo_MouseLeave(object sender, EventArgs e)
        {
            CancelBorder();
        }

        private void ucNode_SizeChanged(object sender, EventArgs e)
        {
            if (picCaption.Image == null)
            {
                this.Height = 96;
            }
            else
            {
                this.Height = 105;//101;
            }
        }


        #region IRuntimeDesignControl 成员

        [Category("IRuntimeDesignControl属性")]
        [Description("运行时只读")]
        public bool RunTimeReadOnly
        {
            get;
            set;
        }

        bool referencetype = true;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否资料调用")]
        public bool Referencetype
        {
            get
            {
                return referencetype;
            }
            set
            {
                referencetype = value;
            }
        }

        bool essential = false;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否必填")]
        public bool Essential
        {
            get
            {
                return essential;
            }
            set
            {
                essential = value;
            }
        }

        //int defaultRows = 1;
        //[Category("IRuntimeDesignControl属性")]
        //[Description("默认行数")]
        //public int DefaultRows
        //{
        //    get
        //    {
        //        return defaultRows;
        //    }
        //    set
        //    {
        //        defaultRows = value;
        //    }
        //}

        int masktype = 0;
        [Category("IRuntimeDesignControl属性")]
        [Description("掩码类型")]
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }

        [Category("IRuntimeDesignControl属性")]
        [Description("编辑值")]
        public object EditObject
        {
            get;
            set;
        }

        [Browsable(false)]
        public Font TextFont
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
            }
        }

        private bool visible4design = true;

        [Browsable(false)]
        public bool Visible4Design
        {
            get
            {
                return visible4design;
            }
            set
            {
                visible4design = value;
            }
        }

        [Browsable(false)]
        public decimal ZIndex { get; set; }

        private int _presentationMode = 1;

        /// <summary>
        /// 展现方式
        /// </summary>
        //[TypeConverter(typeof(PresentationModeConverter))]
        public int PresentationMode
        {
            get;
            set;
        }
        Color prevBackColor;

        [Browsable(false)]
        public bool ShowUnderLine { get; set; }

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

        #endregion
    }
}
