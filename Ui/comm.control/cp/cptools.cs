using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Common.Entity;
using Common.Controls;
using Common.Controls.Emr;
using Common.Utils;
using weCare.Core.Entity;
using weCare.Core.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraReports.UI;

namespace Common.Controls
{
    #region common

    public class CpObject
    {
        public static Color ForeColor = Color.FromArgb(189, 208, 232);  //Color.White; //Color.FromArgb(39, 52, 90);// Color.Black; //Color.Blue; //Color.White;
        public static Brush ForeBursh = new SolidBrush(Color.FromArgb(99, 150, 201)); //Brushes.DeepSkyBlue;//.SteelBlue;            // Color.FromArgb(30, 57, 91);//Brushes.White; //Brushes.Black; //Brushes.Blue; //Brushes.White;
        public static ToolboxItem ToolboxItem { get; set; }
        public static Color LableForeColor = Color.FromArgb(30, 57, 91); //(21, 66, 139);
        public static float LineWidth = 2f;
    }

    public enum enumCPObject
    {
        lineDU,
        lineLR,
        lineLRD,
        lineLRU,
        lineRL,
        lineRLD,
        lineRLU,
        lineUD,
        ucNode,
        label
    }

    public class EntityCPObject
    {
        public enumCPObject objEnum { get; set; }
        public Type objType { get; set; }
        public ToolboxItem ToolboxItem
        {
            get
            {
                if (objType != null)
                {
                    return new ToolboxItem(objType);
                }
                else
                {
                    return null;
                }
            }
        }
        public Image objImg { get; set; }
    }

    /// <summary>
    /// 节点实体
    /// </summary>
    [Serializable, DataContract]
    public class EntityCPNode : BaseDataContract
    {
        [DataMember]
        public int CPID { get; set; }

        [DataMember]
        public string NodeName { get; set; }

        [DataMember]
        public string NodeType { get; set; }

        [DataMember]
        public string NodeDesc { get; set; }

        [DataMember]
        public string NodeDays { get; set; }

        [DataMember]
        public string ControlName { get; set; }

        [DataMember]
        public string ControlType { get; set; }

        [DataMember]
        public string ParentNodeName { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Top { get; set; }

        [DataMember]
        public int Bottom { get; set; }

        [DataMember]
        public int Left { get; set; }

        [DataMember]
        public int Right { get; set; }

        [DataMember]
        public int ExecID { get; set; }

        [DataMember]
        public string CpName { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        [DataMember]
        public Color ForeColor
        {
            get
            {
                try
                {
                    ColorConverter cc = new ColorConverter();
                    Color c = (Color)cc.ConvertFromString(this.ForeColorText);
                    return c;
                }
                catch
                {
                    return Color.Transparent;
                }
            }
            set
            {
                ColorConverter cc = new ColorConverter();
                this.ForeColorText = cc.ConvertToString(value);
            }
        }

        /// <summary>
        /// 前景色存储值
        /// </summary>
        [DataMember]
        public string ForeColorText
        {
            get;
            set;
        }

        /// <summary>
        /// 文本字体
        /// </summary>
        [DataMember]
        public string TextFont { get; set; }

        [DataMember]
        public int Status { get; set; }
    }

    public class ConstValue
    {
        public static float[] DashPattern
        {
            get
            {
                return new float[] { 2.0F, 2.0F };
            }
        }

        public const string EVENT_STRING_SPLITER = "##";

        public const char CONTROL_ITEMS_SPLITER = ';';

        public const int DesignPanelMaxWidth = 9999;
    }

    [Serializable, DataContract]
    public class EntityExec
    {
        [DataMember]
        public string NodeName { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public DateTime ExecDateTime { get; set; }

        [DataMember]
        public bool IsCurr { get; set; }

        [DataMember]
        public int ExecID { get; set; }
    }

    #endregion

    #region CP
    /// <summary>
    /// CP
    /// </summary>
    public class CpTools
    {
        public static object CreateControl(EntityCPNode node)
        {
            try
            {
                object instance = null;
                node.ControlType = node.ControlType.Replace(".ctlLabel,", ".ctlLabelCp,");
                node.ControlType = node.ControlType.Replace("HopeBridge.His.Editor", "Common.Controls");
                node.ControlType = node.ControlType.Replace("His.Ui", "Common.Controls");
                Type type = Type.GetType(node.ControlType);
                if (type != null)
                {
                    instance = Activator.CreateInstance(type);

                    if (instance != null && instance is Control)
                    {
                        Control ctrl = instance as Control;
                        ctrl.Name = node.ControlName;
                        ctrl.BringToFront();

                        if (ctrl is IRuntimeDesignControl)
                        {
                            ctrl.Text = ((IRuntimeDesignControl)ctrl).Text;
                            try
                            {
                                IRuntimeDesignControl ictrl = ctrl as IRuntimeDesignControl;

                                if (ictrl is ICpNode)
                                {
                                    ICpNode iNode = ictrl as ICpNode;
                                    iNode.NodeDays = node.NodeDays;
                                    iNode.NodeName = node.NodeName;
                                    iNode.NodeType = node.NodeType;
                                    iNode.ParentNodeName = node.ParentNodeName;
                                }
                            }
                            catch (Exception ex1)
                            {
                                throw ex1;
                            }
                        }
                    }
                }
                return instance;
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
        }

        public static string DiagramXml(List<EntityCPNode> lstNodes)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<cpdiagram>");
            foreach (EntityCPNode node in lstNodes)
            {
                //if(node.ControlType.StartsWith("HopeBridge.His.Editor.DesignPanel")) continue ;

                xml.Append("<ctrl ctrlname=\"" + node.ControlName + "\" ctrlText=\"" + node.NodeDesc + "\" ctrltype=\"" + node.ControlType + "\" forecolor=\"" + node.ForeColorText + "\" font=\"" + node.TextFont +
                             "\" top=\"" + node.Top.ToString() + "\" left=\"" + node.Left.ToString() + "\" width=\"" + node.Width.ToString() + "\" height=\"" + node.Height.ToString() +
                             "\" nodename=\"" + node.NodeName + "\" nodetype=\"" + node.NodeType + "\" nodedays=\"" + node.NodeDays + "\" parentnode=\"" + node.ParentNodeName + "\"/>");
                xml.Append(System.Environment.NewLine);
            }
            xml.Append("</cpdiagram>");
            xml.Append(System.Environment.NewLine);

            return xml.ToString();
        }

        public static List<EntityCPNode> NodeEntities(string xml)
        {
            List<EntityCPNode> lstNodes = new List<EntityCPNode>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nodelist = doc["cpdiagram"].GetElementsByTagName("ctrl");
            if (nodelist == null || nodelist.Count == 0)
            {
                return lstNodes;
            }

            EntityCPNode entity = null;
            foreach (XmlNode node in nodelist)
            {
                entity = new EntityCPNode();
                entity.ControlName = node.Attributes["ctrlname"].Value.Trim();
                entity.ControlType = node.Attributes["ctrltype"].Value.Trim();
                entity.NodeDesc = node.Attributes["ctrlText"].Value.Trim();
                entity.TextFont = node.Attributes["font"].Value.Trim();
                if (node.Attributes["forecolor"].Value.Trim() == "White" || node.Attributes["forecolor"].Value.Trim() == "ControlText")
                    entity.ForeColorText = "30, 57, 91"; // "Black";
                else
                    entity.ForeColorText = node.Attributes["forecolor"].Value.Trim();
                entity.Top = Function.Int(node.Attributes["top"].Value);
                entity.Left = Function.Int(node.Attributes["left"].Value);
                entity.Width = Function.Int(node.Attributes["width"].Value);
                entity.Height = Function.Int(node.Attributes["height"].Value);
                entity.NodeName = node.Attributes["nodename"].Value.Trim();
                entity.NodeType = node.Attributes["nodetype"].Value.Trim();
                entity.NodeDays = node.Attributes["nodedays"].Value.Trim();

                try
                {
                    entity.ParentNodeName = node.Attributes["parentnode"].Value.Trim();
                }
                catch
                {
                    entity.ParentNodeName = string.Empty;
                }
                lstNodes.Add(entity);
            }
            nodelist = null;
            doc = null;

            return lstNodes;
        }

        #region GetNodes
        /// <summary>
        /// GetNodes
        /// </summary>
        /// <param name="cpID"></param>
        /// <returns></returns>
        //private static List<EntityCPNode> GetNodes(int cpID, bool isExec)
        //{
        //    EntityCpFlowDiagram entityDiagram = new EntityCpFlowDiagram();
        //    List<EntityCPDepts> lstDept = new List<EntityCPDepts>();

        //    ProxyCpDesign proxy = new ProxyCpDesign();
        //    if (isExec)
        //    {
        //        EntityCpExecFlowdiagram vo = proxy.Service.GetCpExecFlowdiagram(cpID);
        //        if (vo != null && !string.IsNullOrEmpty(vo.Diagram))
        //        {
        //            List<EntityCpExecFlowdiagramAdjust> lstAdjust = proxy.Service.GetCpExecFlowdiagramAdjust(cpID);
        //            List<EntityCPNode> nodes = CpTools.NodeEntities(vo.Diagram);

        //            if (lstAdjust != null && lstAdjust.Count > 0)
        //            {
        //                EntityCpExecFlowdiagramAdjust voAdjust = null;
        //                foreach (EntityCPNode item in nodes)
        //                {
        //                    if (lstAdjust.Exists(t => t.Nodename == item.NodeName))
        //                    {
        //                        voAdjust = lstAdjust.FirstOrDefault(t => t.Nodename == item.NodeName);
        //                        item.NodeDays = voAdjust.Nodedays;
        //                        item.ParentNodeName = voAdjust.Parentnode;
        //                        item.Status = Function.Int(voAdjust.Status);
        //                    }
        //                }
        //            }
        //            return nodes;
        //        }
        //    }
        //    else
        //    {
        //        proxy.Service.GetFlowDiagram(cpID, out entityDiagram, out lstDept);
        //        if (entityDiagram != null && !string.IsNullOrEmpty(entityDiagram.Diagram))
        //        {
        //            return CpTools.NodeEntities(entityDiagram.Diagram);
        //        }
        //    }
        //    proxy = null;
        //    return null;
        //}
        #endregion

        public static List<string> ExecedNodesName { get; set; }

        /// <summary>
        /// 获取首节点
        /// </summary>
        /// <param name="cpID"></param>
        /// <returns></returns>
        public static EntityCPNode GetCpFirstNode(List<EntityCPNode> listControls, string nodeName)
        {
            //if (listControls != null && listControls.Count > 0)
            //{
            //    if (listControls.Exists(t => t.Status == 0 && t.ParentNodeName == "^" && t.NodeName == nodeName))
            //    {
            //        return listControls.FirstOrDefault(t => t.Status == 0 && t.ParentNodeName == "^" && t.NodeName == nodeName);
            //    }
            //    else
            //    {
            //        List<EntityCPNode> lstFirstNodes = listControls.FindAll(t => t.Status == 0 && t.ParentNodeName == "^");
            //        if (lstFirstNodes.Count > 1)
            //        {
            //            if (ExecedNodesName != null)
            //            {
            //                foreach (EntityCPNode item in lstFirstNodes)
            //                {
            //                    if (ExecedNodesName.IndexOf(item.NodeName) >= 0)
            //                    {
            //                        return item;
            //                    }
            //                }
            //            }
            //            frmPathNodes frm = new frmPathNodes(lstFirstNodes);
            //            if (frm.ShowDialog() == DialogResult.OK)
            //            {
            //                return lstFirstNodes.FirstOrDefault(t => t.Status == 0 && t.ParentNodeName == "^" && t.NodeName == frm.NodeName);
            //            }
            //        }
            //        else
            //        {
            //            return lstFirstNodes[0];
            //        }
            //    }
            //}
            return null;
        }

        /// <summary>
        /// 获取前一个节点
        /// </summary>
        /// <param name="cpID"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static List<EntityCPNode> GetCpPriorNode(List<EntityCPNode> listControls, string nodeName)
        {
            List<EntityCPNode> lstNode = new List<EntityCPNode>();
            if (listControls != null && listControls.Count > 0)
            {
                EntityCPNode node = listControls.FirstOrDefault(t => t.Status == 0 && t.NodeName == nodeName);
                if (node == null)
                {
                    return null;
                }
                else
                {
                    if (node.ParentNodeName == "^" || node.ParentNodeName.Trim() == string.Empty)
                    {
                        //lstNode.Add(node);   // 自身
                        return null;
                    }
                    else
                    {
                        if (node.ParentNodeName.IndexOf("|") < 0)
                        {
                            lstNode.Add(listControls.FirstOrDefault(t => t.Status == 0 && t.NodeName == node.ParentNodeName));
                        }
                        else
                        {
                            string[] data = node.ParentNodeName.Split('|');
                            foreach (string str in data)
                            {
                                lstNode.AddRange(listControls.FindAll(t => t.Status == 0 && t.NodeName == str));
                            }
                        }
                    }
                }
            }

            return lstNode;
        }

        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="cpID"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static List<EntityCPNode> GetCpNextNode(List<EntityCPNode> listControls, string nodeName)
        {
            List<EntityCPNode> lstNode = new List<EntityCPNode>();
            if (listControls != null && listControls.Count > 0)
            {
                EntityCPNode node = listControls.FirstOrDefault(t => t.Status == 0 && t.NodeName == nodeName);
                if (node == null)
                {
                    return null;
                }
                else
                {
                    lstNode = listControls.FindAll(t => ((t.ParentNodeName == node.NodeName) || (t.ParentNodeName.IndexOf(node.NodeName + "|") >= 0) ||
                                                        (t.ParentNodeName.IndexOf("|" + node.NodeName) >= 0)) && t.Status == 0);
                }
            }

            return lstNode;
        }

        #region 获取当前节点后所有节点

        /// <summary>
        /// 节点
        /// </summary>
        private static List<EntityCPNode> Nodes { get; set; }

        /// <summary>
        /// 获取当前节点后所有节点
        /// </summary>
        /// <param name="cpID"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static List<EntityCPNode> GetCpNextNodes(List<EntityCPNode> listControls, string nodeName)
        {
            Nodes = new List<EntityCPNode>();
            if (listControls != null && listControls.Count > 0)
            {
                GetNextNodes(listControls, nodeName);
            }
            return Nodes;
        }

        /// <summary>
        /// GetNextNodes
        /// </summary>
        /// <param name="listControls"></param>
        /// <param name="nodeName"></param>
        private static void GetNextNodes(List<EntityCPNode> listControls, string nodeName)
        {
            EntityCPNode node = listControls.FirstOrDefault(t => t.NodeName == nodeName);
            if (node != null)
            {
                List<EntityCPNode> tmpNodes = listControls.FindAll(t => ((t.ParentNodeName == node.NodeName) || (t.ParentNodeName.IndexOf(node.NodeName + "|") >= 0) ||
                                                                         (t.ParentNodeName.IndexOf("|" + node.NodeName) >= 0)) && t.Status == 0);

                if (tmpNodes != null && tmpNodes.Count > 0)
                {
                    Nodes.AddRange(tmpNodes);
                    foreach (EntityCPNode item in tmpNodes)
                    {
                        GetNextNodes(listControls, item.NodeName);
                    }
                }
            }
        }

        #endregion

    }
    #endregion

    #region 电子表单工具类
    /// <summary>
    /// 电子表单工具类
    /// </summary>
    public class FormTool
    {
        /// <summary>
        /// ShowUc
        /// </summary>
        static Control ShowUc { get; set; }
        /// <summary>
        /// 父容器
        /// </summary>
        static Control ParentContainer { get; set; }
        /// <summary>
        /// 父容器 -- xPage
        /// </summary>
        static DevExpress.XtraTab.XtraTabPage ParentPage { get; set; }

        #region SetParentObject
        /// <summary>
        /// 设置父对象
        /// </summary>
        /// <param name="_ShowUc"></param>
        /// <param name="_ParentContainer"></param>
        public static void SetParentObject(Control _ShowUc, Control _ParentContainer)
        {
            ShowUc = _ShowUc;
            ParentContainer = _ParentContainer;
        }
        #endregion

        #region ClearStaticObj
        /// <summary>
        /// ClearStaticObj
        /// </summary>
        public static void ClearStaticObj()
        {
            lstCheck.Clear();
            lstSumCheck.Clear();
            lstSumLbl.Clear();
            lstSumTxt.Clear();
            lstAllCheckBox.Clear();
        }
        #endregion

        #region SetShowUcHeight
        /// <summary>
        /// SetShowUcHeight
        /// </summary>
        static void SetShowUcHeight()
        {
            if (ShowUc != null)
            {
                ShowUc.Height = maxHeight + 20;
            }
        }
        #endregion

        #region CreateControl
        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CtrlSource"></param>
        /// <returns></returns>
        public static object CreateControl(EntityFormCtrl Ctrl, List<EntityFormCtrl> CtrlSource)
        {
            try
            {
                object instance = null;
                Type type = Type.GetType(Ctrl.ControlType);
                if (type != null)
                {
                    // 特殊处理内置表格
                    if (type == typeof(ctlTableLayout4Design))
                    {
                        type = typeof(ctlTableCase);
                    }
                    instance = Activator.CreateInstance(type);
                    if (instance != null && instance is Control)
                    {
                        Control ctrl = instance as Control;
                        ctrl.Name = Ctrl.ControlName;
                        ctrl.Location = new System.Drawing.Point(Ctrl.Left, Ctrl.Top);
                        ctrl.Width = Ctrl.Width;
                        ctrl.Height = Ctrl.Height;
                        ctrl.TabIndex = Ctrl.TabIndex;
                        ctrl.Tag = Ctrl;
                        ctrl.BringToFront();
                        try
                        {
                            if (ctrl is ctlTableCase)
                            {
                                (ctrl as ctlTableCase).TableCode = Ctrl.ItemName;
                                return instance;
                            }
                            if (ctrl is IRuntimeDesignControl)
                            {
                                ctrl.Text = Ctrl.Text;
                                ctrl.BackColor = Ctrl.BackColor;
                                ctrl.TabIndex = Ctrl.TabIndex;
                                ((IRuntimeDesignControl)ctrl).PresentationMode = Ctrl.PresentationMode;
                                ((IRuntimeDesignControl)ctrl).Referencetype = Ctrl.ReferenceType == 1 ? true : false;
                                ((IRuntimeDesignControl)ctrl).Essential = Ctrl.Essential == 1 ? true : false;
                            }
                            IFormCtrl iForm = null;
                            if (ctrl is IFormCtrl)
                            {
                                iForm = ctrl as IFormCtrl;
                                iForm.ItemName = Ctrl.ItemName;
                                iForm.ItemType = Ctrl.ItemType;
                                iForm.ItemCaption = Ctrl.ItemCaption;
                                iForm.ParentNode = Ctrl.ParentNode;
                                iForm.CalProperty = Ctrl.CalProperty;
                                iForm.RowShrinkDigit = Ctrl.RowShrinkDigit;

                                if (iForm.ItemType == "2")
                                {
                                    if (ctrl is ctlLabelEf) lstSumLbl.Add(ctrl as ctlLabelEf);
                                    if (ctrl is ctlTextBox) lstSumTxt.Add(ctrl as ctlTextBox);
                                }
                            }
                            if (ctrl is ICheckBox)
                            {
                                ICheckBox iChk = ctrl as ICheckBox;
                                ((ICheckBox)ctrl).Checked = (Ctrl.Checked == "1" ? true : false);
                                iChk.CheckedChanged += new EventHandler(iChk_CheckedChanged);
                                if (!string.IsNullOrEmpty(Ctrl.GroupName))
                                {
                                    ((ICheckBox)ctrl).GroupName = Ctrl.GroupName;
                                    lstCheck.Add(iChk);
                                }
                                if (!string.IsNullOrEmpty(Ctrl.SumName))
                                {
                                    ((ICheckBox)ctrl).SumName = Ctrl.SumName;
                                    lstSumCheck.Add(iChk);
                                }
                                lstAllCheckBox.Add(ctrl as ctlCheckBox);
                                ((ICheckBox)ctrl).CheckedWeightValue = Ctrl.CheckedWeightValue;
                                if (Ctrl.Checked == "1") ctrl.ForeColor = Color.Blue;
                            }
                            else if (ctrl is ICombox)
                            {
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    ICombox iCbx = ctrl as ICombox;
                                    string[] items = Ctrl.Items.Split(EmrTool.CONTROL_ITEMS_SPLITER);
                                    foreach (string i in items)
                                    {
                                        iCbx.Items.Add(i);
                                    }
                                }
                                ((ctlComboBox)ctrl).ForeColor = Ctrl.ForeColor;
                                ((ctlComboBox)ctrl).Text = Ctrl.Text;
                                ((ctlComboBox)ctrl).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            }
                            else if (ctrl is ICtlLine)
                            {
                                ICtlLine iLine = ctrl as ICtlLine;
                                iLine.LineStyle = (CtlLineStyle)Enum.Parse(typeof(CtlLineStyle), Ctrl.LineStyle);
                                iLine.LineWidth = Ctrl.LineWidth;
                            }
                            else if (ctrl is IPictureBox)
                            {
                                IPictureBox iPic = ctrl as IPictureBox;
                                iPic.FileName = Ctrl.PicFileName;
                            }
                            else if (ctrl is IPanel)
                            {
                                IPanel ipnl = ctrl as IPanel;
                                if (!string.IsNullOrEmpty(Ctrl.ReserveField))
                                {
                                    string[] items = Ctrl.ReserveField.Split(EmrTool.CONTROL_ITEMS_SPLITER);
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
                                    ipnl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), Ctrl.Items);
                                }
                                catch (Exception ex)
                                {
                                    weCare.Core.Utils.ExceptionLog.OutPutException(ex);
                                }
                            }
                            else if (ctrl is ITabControl)
                            {
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    ITabControl iTabCtrl = ctrl as ITabControl;
                                    iTabCtrl.TabPages.Clear();

                                    if (Ctrl.Items == "top")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
                                    }
                                    else if (Ctrl.Items == "left")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
                                    }
                                    else if (Ctrl.Items == "bottom")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
                                    }
                                    else if (Ctrl.Items == "right")
                                    {
                                        iTabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
                                    }
                                }
                            }
                            else if (ctrl is IXtraDateTime)
                            {
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    IXtraDateTime iDateTime = ctrl as IXtraDateTime;
                                    string[] items = Ctrl.Items.Split(EmrTool.CONTROL_ITEMS_SPLITER);   //(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);

                                    if (items.Length > 0)
                                    {
                                        if (!string.IsNullOrEmpty(items[0]))//默认时间
                                        {
                                            DateTime dtDefDate = DateTime.Now;
                                            if (DateTime.TryParse(items[0], out dtDefDate))
                                            {
                                                iDateTime.DateTimeValue = dtDefDate;
                                                if (ctrl is IFormCtrl)
                                                {
                                                    ((IFormCtrl)ctrl).ValueChangedFlag = false;
                                                }
                                            }
                                        }
                                    }
                                    if (items.Length > 1)
                                    {
                                        if (!string.IsNullOrEmpty(items[1]))
                                        {
                                            iDateTime.EditMask = items[1];
                                        }
                                    }
                                    if (items.Length > 2)
                                    {
                                        if (!string.IsNullOrEmpty(items[2]))
                                        {
                                            iDateTime.SPDefaultValue = items[2];
                                        }
                                    }
                                }
                            }
                            else if (ctrl is IPatientControl)
                            {
                                IPatientControl iPatctrl = ctrl as IPatientControl;
                                iPatctrl.ReadPatientInfoFromGolbolEnv = Ctrl.ReadPatientInfoFromGolbolEnv;
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    string[] items = Ctrl.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                    if (items.Length >= 3)
                                    {
                                        iPatctrl.CaptionText = items[0];
                                        iPatctrl.InfoType = (EnumPatientInfoType)Enum.Parse(typeof(EnumPatientInfoType), items[1]);
                                        iPatctrl.ShowCaption = Convert.ToBoolean(items[2]);
                                    }
                                    int intCalcAgeType = 0;
                                    if (items.Length >= 4)
                                    {
                                        int.TryParse(items[3], out intCalcAgeType);
                                    }
                                    iPatctrl.CalcAgeType = intCalcAgeType;
                                    if (items.Length >= 5)
                                    {
                                        iPatctrl.BandingPage = Convert.ToBoolean(items[4]);
                                    }
                                }
                            }
                            else if (ctrl is ISignatureControl && ctrl is ctlSignature)
                            {
                                ISignatureControl iSign = ctrl as ISignatureControl;
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    string[] items = Ctrl.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                    if (items.Length >= 3)
                                    {
                                        iSign.Caption = items[0];
                                        iSign.IsAllowSignNull = Function.Int(items[1]);
                                        iSign.IsAutoSignature = Function.Int(items[2]);
                                    }
                                }
                                ((ctlSignature)ctrl).SetSignName(Ctrl.Text);
                            }
                            else if (ctrl is IRtfEditor && ctrl is ctlRichTextBox && iForm != null)
                            {
                                IRtfEditor iRtf = ctrl as IRtfEditor;
                                if (!string.IsNullOrEmpty(Ctrl.Items))
                                {
                                    string[] items = Ctrl.Items.Split(new string[] { EmrTool.EVENT_STRING_SPLITER }, StringSplitOptions.None);
                                    if (items.Length >= 5)
                                    {
                                        iRtf.Multiline = Function.Int(items[0]) == 1 ? true : false;
                                        iRtf.FixedHeight = Function.Int(items[1]) == 1 ? true : false;
                                        iRtf.RowShrinkdigit = Function.Int(items[2]);
                                        iRtf.DefaultRows = Function.Int(items[3]);
                                        iRtf.FirstlineCaption = items[4];
                                        if (!string.IsNullOrEmpty(iRtf.FirstlineCaption))
                                        {
                                            if (!Ctrl.Text.StartsWith(iRtf.FirstlineCaption))
                                            {
                                                Ctrl.Text = iRtf.FirstlineCaption + Ctrl.Text;
                                            }
                                        }
                                    }
                                }
                                ctlRichTextBox rich = ctrl as ctlRichTextBox;
                                rich.Enter += new EventHandler(iRtf_Enter);
                                rich.SetFirstlineCaption();
                                rich.SetLoginUser(GlobalLogin.objLogin.EmpNo, GlobalLogin.objLogin.EmpName);
                                ctrl.Text = Ctrl.Text;
                                if (iRtf.Multiline && !iRtf.FixedHeight)
                                {
                                    if (GlobalCase.caseInfo != null && !string.IsNullOrEmpty(iForm.ItemName) && uiHelper.TabDiagSettingArr != null)
                                    {
                                        if (uiHelper.TabDiagSettingArr.ToList().Exists(t => t.CaseCode.ToLower() == GlobalCase.caseInfo.CaseCode.ToLower() && t.ColCode.ToLower() == iForm.ItemName.ToLower()))
                                        {
                                            rich.ContentsResized += new ContentsResizedEventHandler(irtf_ContentsResizedTop);
                                        }
                                        else
                                        {
                                            rich.ContentsResized += new ContentsResizedEventHandler(irtf_ContentsResized);
                                        }
                                    }
                                    else
                                    {
                                        rich.ContentsResized += new ContentsResizedEventHandler(irtf_ContentsResized);
                                    }
                                    rich.ScrollBars = RichTextBoxScrollBars.None;
                                    //else if (rich.Parent is DevExpress.XtraTab.XtraTabPage)
                                    //{
                                    //    rich.ContentsResized += new ContentsResizedEventHandler(irtf_ContentsResized2);
                                    //    rich.ScrollBars = RichTextBoxScrollBars.None;
                                    //    ParentPage = (DevExpress.XtraTab.XtraTabPage)rich.Parent;
                                    //}
                                }
                            }

                            //查找当前控件的子控件
                            var query = from item in CtrlSource
                                        where item.Parent == ctrl.Name
                                        select item;
                            foreach (var itemChild in query)
                            {
                                object obj = CreateControl(itemChild, CtrlSource);
                                if (obj == null) continue;
                                Control ctrlChild = obj as Control;
                                if (ctrlChild != null)
                                {
                                    ctrlChild.Parent = ctrl;
                                    if (ctrlChild is ctlRichTextBox)
                                    {
                                        if (((ctlRichTextBox)ctrlChild).Parent is DevExpress.XtraTab.XtraTabPage)
                                        {
                                            ((ctlRichTextBox)ctrlChild).ContentsResized += new ContentsResizedEventHandler(irtf_ContentsResized2);
                                            ((ctlRichTextBox)ctrlChild).ScrollBars = RichTextBoxScrollBars.None;
                                            ParentPage = (DevExpress.XtraTab.XtraTabPage)((ctlRichTextBox)ctrlChild).Parent;
                                        }
                                    }
                                    if (ctrlChild is DevExpress.XtraTab.XtraTabPage)
                                    {
                                        ((DevExpress.XtraTab.XtraTabPage)ctrlChild).Text = itemChild.Text;
                                        (ctrl as ITabControl).TabPages.Add(ctrlChild as DevExpress.XtraTab.XtraTabPage);
                                    }
                                    else
                                    {
                                        ctrl.Controls.Add(ctrlChild);
                                    }
                                    if (ctrlChild is IRuntimeDesignControl)
                                    {
                                        IRuntimeDesignControl ICtrl = ctrlChild as IRuntimeDesignControl;
                                        ICtrl.Location = new System.Drawing.Point((int)itemChild.Left, (int)itemChild.Top);
                                        ICtrl.Width = (int)itemChild.Width;
                                        ICtrl.Height = (int)itemChild.Height;
                                        ICtrl.TabIndex = itemChild.TabIndex;
                                        ICtrl.PresentationMode = itemChild.PresentationMode;
                                        ICtrl.Referencetype = itemChild.ReferenceType == 1 ? true : false;
                                        ICtrl.Essential = itemChild.Essential == 1 ? true : false;
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
                        catch (Exception ex1)
                        {
                            throw ex1;
                        }
                    }
                }
                return instance;
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
        }
        #endregion

        #region event

        static List<ICheckBox> lstCheck = new List<ICheckBox>();
        static List<ICheckBox> lstSumCheck = new List<ICheckBox>();
        static List<ctlCheckBox> lstAllCheckBox = new List<ctlCheckBox>();
        static List<ctlLabelEf> lstSumLbl = new List<ctlLabelEf>();
        static List<ctlTextBox> lstSumTxt = new List<ctlTextBox>();

        static void iChk_CheckedChanged(object sender, EventArgs e)
        {
            ICheckBox iChk = sender as ICheckBox;
            // 同时勾选
            List<string> lstCGroup = new List<string>();
            string itemCaption = (sender as ctlCheckBox).ItemCaption;
            if (!string.IsNullOrEmpty(itemCaption))
            {
                int pos1 = itemCaption.IndexOf("|S");
                int pos2 = itemCaption.IndexOf("E|");
                if (pos1 >= 0 && pos2 > 0)
                {
                    string groupCheckFields = itemCaption.Substring(pos1 + 3, pos2 - (pos1 + 3)).ToLower().Trim();
                    lstCGroup = groupCheckFields.Split(';').ToList();
                }
            }
            if (iChk.Checked)
            {
                // 根据分组属性控制只选一个
                foreach (ICheckBox chk in lstCheck)
                {
                    if (chk != iChk && chk.GroupName == iChk.GroupName)
                    {
                        chk.Checked = false;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Black;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                        ((DevExpress.XtraEditors.CheckEdit)sender).Update();
                    }
                }
                ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Blue;
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
            else
            {
                ((DevExpress.XtraEditors.CheckEdit)sender).Properties.Appearance.ForeColor = Color.Black;
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
            if (lstSumCheck != null && lstSumCheck.Count > 0)
            {
                decimal sumWeightVal = 0;
                foreach (ICheckBox chk in lstSumCheck)
                {
                    if (chk.Checked && chk.SumName == iChk.SumName)
                        sumWeightVal += chk.CheckedWeightValue;
                }
                foreach (ctlTextBox txt in lstSumTxt)
                {
                    if (txt.ItemName == iChk.SumName)
                    {
                        txt.Text = sumWeightVal.ToString();
                        break;
                    }
                }
                foreach (ctlLabelEf lbl in lstSumLbl)
                {
                    if (lbl.ItemName == iChk.SumName)
                    {
                        lbl.Text = sumWeightVal.ToString();
                        break;
                    }
                }
            }
            // 同时勾选
            if (lstCGroup.Count > 0)
            {
                foreach (ctlCheckBox chk in lstAllCheckBox)
                {
                    if (lstCGroup.IndexOf(chk.ItemName.ToLower()) >= 0) chk.Checked = iChk.Checked;
                }
            }
        }

        #region Rtf
        /// <summary>
        /// 当前病历编辑器
        /// </summary>
        public static ctlRichTextBox CurrentRtfEditor { get; set; }

        #region rtf控件获得焦点
        /// <summary>
        /// rtf控件获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void iRtf_Enter(object sender, EventArgs e)
        {
            if (sender is ctlRichTextBox)
            {
                CurrentRtfEditor = sender as ctlRichTextBox;
            }
        }
        #endregion

        #region rtf控件编辑区域大小改变.自身坐标上移
        /// <summary>
        /// rtf控件编辑区域大小改变.自身坐标上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void irtf_ContentsResizedTop(object sender, ContentsResizedEventArgs e)
        {
            if (sender is ctlRichTextBox)
            {
                ctlRichTextBox ctl = sender as ctlRichTextBox;
                if (ctl.Multiline)
                {
                    int intDiffH = e.NewRectangle.Height - ctl.Height + 4;
                    ctl.Top -= intDiffH;
                    ctl.Height += intDiffH;
                }
            }
        }
        #endregion

        #region rtf控件编辑区域大小改变
        /// <summary>
        /// rtf控件编辑区域大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void irtf_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (sender is ctlRichTextBox)
            {
                ctlRichTextBox ctl = sender as ctlRichTextBox;
                if (ctl.Multiline)
                {
                    ChangeCtrlLocation(ctl, e.NewRectangle.Height - ctl.Height);
                }
            }
            //else if (sender is ctlICD)
            //{
            //    ctlICD ctl = sender as ctlICD;
            //    ChangeCtrlLocation(ctl, e.NewRectangle.Height - ctl.Height);
            //}
            //else if (sender is ctlAllergy)
            //{
            //    ctlAllergy ctl = sender as ctlAllergy;
            //    ChangeCtrlLocation(ctl, e.NewRectangle.Height - ctl.Height);
            //}
        }

        /// <summary>
        /// 面板最大高度
        /// </summary>
        public static int maxHeight { get; set; }

        /// <summary>
        /// 表格宽度
        /// </summary>
        public static int maxTableWidth { get; set; }

        /// <summary>
        /// 表格高度
        /// </summary>
        public static int maxTableHeight { get; set; }

        /// <summary>
        /// rtf控件编辑区域大小改变后，坐标以下控件下移
        /// </summary>
        /// <param name="p_objCtl"></param>
        /// <param name="p_intHeight"></param>
        static void ChangeCtrlLocation(Control p_objCtl, int p_intHeight)
        {
            //if (this.m_blnLoading) return;
            int diffHeight = 4;
            ParentContainer.SuspendLayout();
            Control parentControl = p_objCtl.Parent;

            if (parentControl == ParentContainer)
            {
                p_objCtl.Height += p_intHeight + diffHeight;
                foreach (Control ctl in ParentContainer.Controls)
                {
                    if (ctl.Top > p_objCtl.Top && ctl != p_objCtl)
                    {
                        ctl.Top += p_intHeight + diffHeight;
                    }
                }
            }
            ParentContainer.ResumeLayout();
            maxHeight += p_intHeight + diffHeight;
            SetShowUcHeight();
        }
        #endregion

        #region rtf控件编辑区域大小改变2
        /// <summary>
        /// rtf控件编辑区域大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void irtf_ContentsResized2(object sender, ContentsResizedEventArgs e)
        {
            if (sender is ctlRichTextBox)
            {
                ctlRichTextBox ctl = sender as ctlRichTextBox;
                if (ctl.Multiline)
                {
                    //if (e.NewRectangle.Height - ctl.Height < 0) return;
                    ChangeCtrlLocation2(ctl, e.NewRectangle.Height - ctl.Height);
                }
            }
        }

        /// <summary>
        /// rtf控件编辑区域大小改变后，坐标以下控件下移
        /// </summary>
        /// <param name="p_objCtl"></param>
        /// <param name="p_intHeight"></param>
        static void ChangeCtrlLocation2(Control p_objCtl, int p_intHeight)
        {
            //if (this.m_blnLoading) return;
            ParentContainer.SuspendLayout();

            int intHeightTmp = p_intHeight + 4;
            if (intHeightTmp == 0)
            {
                ParentContainer.ResumeLayout();
                return;
            }

            Control ctlParent = p_objCtl.Parent;
            if (ctlParent is DevExpress.XtraTab.XtraTabPage)
            {
                p_objCtl.Height += intHeightTmp;

                Control ctlPage = ctlParent as Control;

                foreach (Control ctl in ctlPage.Controls)
                {
                    if (ctl.Top > p_objCtl.Top && ctl != p_objCtl && ctl.Location.X + 5 > p_objCtl.Location.X)
                    {
                        ctl.Top += intHeightTmp;
                    }
                }

                Control ctlTab = ctlParent.Parent;
                if (p_intHeight > 0)
                {
                    if (GetTabMaxTop(ctlTab as XtraTabControl) > ctlTab.Height - 30)
                    {
                        ctlTab.Height += intHeightTmp;
                    }
                }
                else if (p_intHeight < 0)
                {
                    int intDiff = GetDiffHeight(ctlTab as XtraTabControl, intHeightTmp);
                    if (intDiff > 0)
                    {
                        ctlTab.Height -= intDiff;
                        intHeightTmp = -intDiff;
                    }

                    //if (this.m_intGetTabMaxTop(ctlTab as XtraTabControl) < ctlTab.Height + 10)
                    //{
                    //    ctlTab.Height += intHeightTmp + 2;
                    //}
                }

                if (ctlTab.Parent == ParentContainer)
                {
                    foreach (Control ctl in ParentContainer.Controls)
                    {
                        if (ctl.Top > ctlTab.Top + ctlTab.Height + 2 && ctl.Parent != ctlTab && ctl != ctlTab && ctl.Parent == ParentContainer)
                        {
                            ctl.Top += intHeightTmp;
                        }
                    }
                }
            }

            ParentContainer.ResumeLayout();
            maxHeight += intHeightTmp;
            SetShowUcHeight();
        }

        #region 获取XTabPage最大坐标
        /// <summary>
        /// 获取XTabPage最大坐标
        /// </summary>
        /// <param name="p_objTable"></param>
        /// <returns></returns>
        static int GetTabMaxTop(XtraTabControl p_objTable)
        {
            int intMaxTop = 0;
            foreach (XtraTabPage tabpage in p_objTable.TabPages)
            {
                foreach (Control ctl in tabpage.Controls)
                {
                    intMaxTop = Math.Max(ctl.Top + ctl.Height, intMaxTop);
                    //if (ctl.Top + ctl.Height > intMaxTop)
                    //{
                    //    intMaxTop = ctl.Top + ctl.Height;                        
                    //}
                }
            }
            return intMaxTop;
        }

        /// <summary>
        /// 最大差值
        /// </summary>
        /// <param name="p_objTable"></param>
        /// <param name="p_intSubHeight"></param>
        /// <returns></returns>
        static int GetDiffHeight(XtraTabControl p_objTable, int p_intSubHeight)
        {
            int intMaxTop = 0;
            int intMaxDiff = 0;

            foreach (XtraTabPage tabpage in p_objTable.TabPages)
            {
                foreach (Control ctl in tabpage.Controls)
                {
                    intMaxTop = Math.Max(ctl.Top + ctl.Height, intMaxTop);
                }

                if (p_objTable.Height - Math.Abs(p_intSubHeight) < intMaxTop)
                {
                    intMaxDiff = Math.Max(p_objTable.Height - intMaxTop, intMaxDiff) - 30;
                }
            }
            return intMaxDiff;
        }

        #endregion
        #endregion
        #endregion
        #endregion

        #region CtrlName
        /// <summary>
        /// CtrlName
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static string CtrlName(System.Type dataType)
        {
            string name = dataType.Name;

            Dictionary<string, string> dicCtrlName = new Dictionary<string, string>();

            // CP
            dicCtrlName.Add("Common.Controls.Emr.ctlLineDU", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineLR", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineLRD", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineLRU", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineRL", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineRLD", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineRLU", "线");
            dicCtrlName.Add("Common.Controls.Emr.ctlLineUD", "线");
            dicCtrlName.Add("Common.Controls.Emr.ucNode", "节点");

            dicCtrlName.Add("Common.Controls.Emr.ctlLabel", "标签");
            dicCtrlName.Add("Common.Controls.Emr.ctlButton", "按钮");
            dicCtrlName.Add("Common.Controls.Emr.ctlPanel", "Panel");
            dicCtrlName.Add("Common.Controls.Emr.ctlCheckBox", "勾选框");
            dicCtrlName.Add("Common.Controls.Emr.ctlTextBox", "文本框");
            dicCtrlName.Add("Common.Controls.Emr.ctlMemoEdit", "多行编辑框");
            dicCtrlName.Add("Common.Controls.Emr.ctlLine", "水平线");
            dicCtrlName.Add("Common.Controls.Emr.ctlVertLine", "垂直线");
            dicCtrlName.Add("Common.Controls.Emr.ctlDatetime", "时间");
            dicCtrlName.Add("Common.Controls.Emr.ctlCombox", "下拉框");
            dicCtrlName.Add("Common.Controls.Emr.ctlTabControl", "分页控件");
            dicCtrlName.Add("Common.Controls.Emr.ctlTreeSelect_Dept", "科室字典");
            dicCtrlName.Add("Common.Controls.Emr.ctlTreeSelect_Employee", "职工字典");
            dicCtrlName.Add("Common.Controls.Emr.ctlSignature", "签名控件");
            dicCtrlName.Add("Common.Controls.Emr.ctlRichTextBox", "书写控件");
            dicCtrlName.Add("Common.Controls.Emr.ctlTableLayout4Design", "表格控件");
            dicCtrlName.Add("Common.Controls.Emr.ctlHospitalName", "医院名称");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientAge", "年龄");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientArea", "病区");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientBedNo", "床号");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientDept", "科室");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientHomeAddr", "家庭地址");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientInDate", "入院时间");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientInDays", "住院天数");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientIpNo", "住院号");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientIpTimes", "住院次数");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientName", "病人姓名");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientNativePlace", "籍贯");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientSex", "性别");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientMarriage", "婚姻");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientNation", "民族");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientHkadr", "户口地址");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientIdNumber", "身份证号");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientBirthPlace", "出生地");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientProfession", "职业");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientCompany", "工作单位");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientTel", "联系电话");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientContactPerson", "联系人");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientContactTel", "联系人电话");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientContactRelation", "与联系人关系");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientHeight", "身高");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientWeight", "体重");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientBloodType", "血型");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientTemperature", "体温");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientPulse", "脉搏");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientBreath", "呼吸");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientBloodPressure", "血压");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientCulture", "文化程度");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientRecordDate", "记录时间");
            dicCtrlName.Add("Common.Controls.Emr.ctlPatientOpNo", "门诊号");

            dicCtrlName.Add("Common.Controls.Emr.DesignPanel", "设计面板");

            if (dicCtrlName.ContainsKey(dataType.FullName))
            {
                name = dicCtrlName[dataType.FullName];
            }

            return name;
        }
        #endregion

        #region LayoutXml
        /// <summary>
        /// LayoutXml
        /// </summary>
        /// <param name="lstCtrls"></param>
        /// <returns></returns>
        public static string LayoutXml(List<EntityFormCtrl> lstCtrls)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<eflayout>");
            foreach (EntityFormCtrl ctrl in lstCtrls)
            {
                if (ctrl.ControlType.StartsWith("Common.Controls.Emr.DesignPanel")) continue;

                xml.Append("<ctrl ctrlname=\"" + ctrl.ControlName + "\" ctrlText=\"" + ctrl.Text + "\" ctrltype=\"" + ctrl.ControlType + "\" forecolor=\"" + ctrl.ForeColorText + "\" backcolor=\"" + ctrl.BackColorText + "\" font=\"" + ctrl.TextFont +
                    "\" top=\"" + ctrl.Top.ToString() + "\" left=\"" + ctrl.Left.ToString() + "\" width=\"" + ctrl.Width.ToString() + "\" height=\"" + ctrl.Height.ToString() + "\" linestyle=\"" + ctrl.LineStyle +
                    "\" linewidth=\"" + ctrl.LineWidth + "\" groupname=\"" + ctrl.GroupName + "\" checked=\"" + ctrl.Checked + "\" picfilename=\"" + ctrl.PicFileName +
                    "\" itemname=\"" + ctrl.ItemName + "\" itemcaption=\"" + ctrl.ItemCaption + "\" itemtype=\"" + ctrl.ItemType + "\" parentnode=\"" + ctrl.ParentNode +
                    "\" parent=\"" + ctrl.Parent + "\" itemparent=\"" + ctrl.ItemParent + "\" items=\"" + ctrl.Items + "\" calproperty=\"" + ctrl.CalProperty + "\" rowshrinkdigit=\"" + ctrl.RowShrinkDigit +
                    "\" referencetype=\"" + ctrl.ReferenceType + "\" essential=\"" + ctrl.Essential +
                    "\" defaultrows=\"" + ctrl.DefaultRows + "\" presentationmode=\"" + ctrl.PresentationMode + "\" tabindex=\"" + ctrl.TabIndex +
                    "\" sumname=\"" + ctrl.SumName + "\" checkedweightvalue=\"" + ctrl.CheckedWeightValue + "\" reservefield=\"" + ctrl.ReserveField + "\"/>");

                xml.Append(System.Environment.NewLine);
            }
            xml.Append("</eflayout>");
            xml.Append(System.Environment.NewLine);

            return xml.ToString();
        }
        #endregion

        #region Entities
        /// <summary>
        /// FormEntities
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<EntityFormCtrl> Entities(string xml)
        {
            List<EntityFormCtrl> lstCtrls = new List<EntityFormCtrl>();
            if (string.IsNullOrEmpty(xml) || xml.Trim() == string.Empty || xml == "<XmlData />" || xml == "<XmlData/>\n" || xml == "<XmlData></XmlData>") return lstCtrls;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nodelist = doc["eflayout"].GetElementsByTagName("ctrl");
            if (nodelist == null || nodelist.Count == 0)
            {
                return lstCtrls;
            }

            EntityFormCtrl entity = null;
            foreach (XmlNode node in nodelist)
            {
                entity = new EntityFormCtrl();
                try
                {
                    entity.ControlName = node.Attributes["ctrlname"].Value.Trim();
                    entity.ControlType = node.Attributes["ctrltype"].Value.Trim();
                    entity.Text = node.Attributes["ctrlText"].Value.Trim();
                    entity.TextFont = node.Attributes["font"].Value.Trim();
                    entity.ForeColorText = node.Attributes["forecolor"].Value.Trim();
                    entity.BackColorText = node.Attributes["backcolor"].Value.Trim();
                    entity.Top = Function.Int(node.Attributes["top"].Value);
                    entity.Left = Function.Int(node.Attributes["left"].Value);
                    entity.Width = Function.Int(node.Attributes["width"].Value);
                    entity.Height = Function.Int(node.Attributes["height"].Value);

                    entity.ItemName = node.Attributes["itemname"].Value.Trim();
                    entity.ItemCaption = node.Attributes["itemcaption"].Value.Trim();
                    entity.ItemType = node.Attributes["itemtype"].Value.Trim();
                    entity.GroupName = node.Attributes["groupname"].Value.Trim();
                    entity.LineWidth = Function.Int(node.Attributes["linewidth"].Value);
                    entity.LineStyle = node.Attributes["linestyle"].Value.Trim();
                    entity.Checked = node.Attributes["checked"].Value.Trim();
                    entity.PicFileName = node.Attributes["picfilename"].Value.Trim();
                    entity.ParentNode = node.Attributes["parentnode"].Value.Trim();

                    entity.Parent = node.Attributes["parent"].Value.Trim();
                    entity.ItemParent = node.Attributes["itemparent"].Value.Trim();
                    entity.Items = node.Attributes["items"].Value.Trim();

                    entity.CalProperty = node.Attributes["calproperty"].Value.Trim();
                    entity.RowShrinkDigit = Function.Int(node.Attributes["rowshrinkdigit"].Value);
                    entity.ReferenceType = Function.Int(node.Attributes["referencetype"].Value);
                    entity.Essential = Function.Int(node.Attributes["essential"].Value);
                    entity.DefaultRows = Function.Int(node.Attributes["defaultrows"].Value);
                    entity.PresentationMode = Function.Int(node.Attributes["presentationmode"].Value);
                    entity.TabIndex = Function.Int(node.Attributes["tabindex"].Value);

                    entity.ReserveField = node.Attributes["reservefield"].Value.Trim();
                    entity.SumName = node.Attributes["sumname"].Value.Trim();
                    entity.CheckedWeightValue = Function.Dec(node.Attributes["checkedweightvalue"].Value);
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
                lstCtrls.Add(entity);
            }
            nodelist = null;
            doc = null;

            return lstCtrls;
        }
        #endregion

        #region GetPrintDataSource
        /// <summary>
        /// 节点
        /// </summary>
        class EntityNode
        {
            public string itemName { get; set; }
            public string itemCaption { get; set; }
        }
        /// <summary>
        /// EntityField
        /// </summary>
        class EntityField
        {
            public string fieldCode { get; set; }
            public string fieldDesc { get; set; }
            public string fieldVal { get; set; }
        }
        /// <summary>
        /// GetPrintDataTable
        /// </summary>
        /// <param name="xmlLayout"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public static DataTable GetPrintDataTable(string xmlLayout, string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData)) return null;
            List<EntityNode> lstNode = null;
            List<EntityFormCtrl> formCtrls = FormTool.Entities(xmlLayout).FindAll(t => t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6");
            if (formCtrls != null)
            {
                lstNode = new List<EntityNode>();
                foreach (EntityFormCtrl item in formCtrls)
                {
                    lstNode.Add(new EntityNode() { itemName = item.ItemName, itemCaption = item.ItemCaption });
                }
            }
            if (lstNode == null || lstNode.Count == 0)
            {
                return null;
            }

            EntityField fieldVo = null;
            List<EntityField> lstField = new List<EntityField>();
            DataTable dtMerger = new DataTable();
            DataSet ds = Function.ReadXml(xmlData);
            DataColumn colNew = null;
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    colNew = new DataColumn();
                    colNew.ColumnName = col.ColumnName;
                    colNew.Caption = col.Caption;
                    colNew.DataType = col.DataType;
                    if (!lstField.Any(t => t.fieldCode == colNew.ColumnName))
                    {
                        dtMerger.Columns.Add(colNew);

                        fieldVo = new EntityField();
                        fieldVo.fieldCode = colNew.ColumnName;
                        if (dt.Rows.Count == 1)
                            fieldVo.fieldVal = dt.Rows[0][fieldVo.fieldCode].ToString();
                        else
                            fieldVo.fieldVal = dt.Rows[1][fieldVo.fieldCode].ToString();
                        lstField.Add(fieldVo);
                    }
                }
            }
            // 
            dtMerger.Columns.Add("[医院名称]", typeof(string));
            DataRow drMerger = dtMerger.NewRow();
            foreach (EntityField item in lstField)
            {
                drMerger[item.fieldCode] = item.fieldVal;
            }
            drMerger["[医院名称]"] = GlobalHospital.HospitalName;
            dtMerger.Rows.Add(drMerger);
            dtMerger.AcceptChanges();
            if (lstNode != null && lstNode.Count > 0)
            {
                foreach (EntityNode item in lstNode)
                {
                    if (lstField.Any(t => t.fieldCode.Trim() == item.itemName.Trim()))
                    {
                        lstField.FirstOrDefault(t => t.fieldCode.Trim() == item.itemName.Trim()).fieldDesc = item.itemCaption;
                    }
                }
            }
            for (int i = 0; i < dtMerger.Columns.Count; i++)
            {
                if (lstField.Any(t => t.fieldCode == dtMerger.Columns[i].ColumnName && !string.IsNullOrEmpty(t.fieldDesc)))
                {
                    dtMerger.Columns[i].ColumnName = lstField.FirstOrDefault(t => t.fieldCode == dtMerger.Columns[i].ColumnName && !string.IsNullOrEmpty(t.fieldDesc)).fieldDesc;
                }
            }
            return dtMerger;
        }
        /// <summary>
        /// GetPrintDataSet
        /// </summary>
        /// <param name="xmlLayout"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public static DataSet GetPrintDataSet(string xmlLayout, string xmlData, List<EntityEmrSelfDefineCol> lstSelfDefineCol)
        {
            if (string.IsNullOrEmpty(xmlData)) return null;
            List<string> lstTableColName = new List<string>();
            DataSet ds = Function.ReadXml(xmlData);
            DataSet dsPrint = new DataSet();

            #region 表头(主记录)
            if (ds.Tables.Contains("FormData"))
            {
                List<EntityNode> lstNode = null;
                List<EntityFormCtrl> formCtrls = FormTool.Entities(xmlLayout).FindAll(t => t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6");
                if (formCtrls != null)
                {
                    lstNode = new List<EntityNode>();
                    foreach (EntityFormCtrl item in formCtrls)
                    {
                        lstNode.Add(new EntityNode() { itemName = item.ItemName, itemCaption = item.ItemCaption });
                    }
                }

                DataTable dtFormData = ds.Tables["FormData"];
                foreach (DataColumn dataCol in dtFormData.Columns)
                {
                    lstTableColName.Add(dataCol.ColumnName);
                }
                DataTable dtMain = dtFormData.Clone();
                dtMain.TableName = "Main";
                dtMain.Columns.Add("[医院名称]", typeof(string));

                DataRow drMain = dtMain.NewRow();
                drMain["[医院名称]"] = GlobalHospital.HospitalName;
                if (dtFormData.Rows.Count > 0)
                {
                    foreach (string colName in lstTableColName)
                    {
                        drMain[colName] = dtFormData.Rows[0][colName];
                    }
                }
                dtMain.Rows.Add(drMain);
                dtMain.AcceptChanges();

                for (int i = 0; i < dtMain.Columns.Count; i++)
                {
                    if (lstNode.Any(t => t.itemName == dtMain.Columns[i].ColumnName))
                    {
                        dtMain.Columns[i].ColumnName = lstNode.FirstOrDefault(t => t.itemName == dtMain.Columns[i].ColumnName).itemCaption;
                    }
                }
                dsPrint.Tables.Add(dtMain);
            }
            #endregion

            #region 表格
            DataTable dtTable = null;
            List<string> lstTableCode = new List<string>();
            if (ds.Tables.Contains("Row"))
            {
                dtTable = ds.Tables["Row"];
                foreach (DataRow dr in dtTable.Rows)
                {
                    if (lstTableCode.IndexOf(dr["tableCode"].ToString()) < 0) lstTableCode.Add(dr["tableCode"].ToString());
                }
            }
            if (lstTableCode.Count > 0)
            {
                lstTableColName.Clear();
                foreach (DataColumn dataCol in dtTable.Columns)
                {
                    lstTableColName.Add(dataCol.ColumnName);
                }
                foreach (string tablecode in lstTableCode)
                {
                    EntityEmrPrintTemplate printObj = null;
                    List<EntityEmrTableFieldInfo> lstTableField = null;
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        printObj = proxy.Service.GetFormPrintTemplate(2, tablecode);
                        lstTableField = proxy.Service.GetTableFieldInfo(tablecode);
                    }
                    DataTable dtClone = new DataTable();
                    foreach (DataColumn dc in dtTable.Columns)
                    {
                        if (dc.ColumnName == "rowIndex") dtClone.Columns.Add("rowIndex", typeof(int));
                        else dtClone.Columns.Add(dc.ColumnName, typeof(string));
                    }
                    dtClone.BeginLoadData();
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        dtClone.LoadDataRow(dr.ItemArray, true);
                    }
                    dtClone.EndLoadData();

                    DataTable dtData = dtClone.Clone();
                    dtData.TableName = tablecode;
                    DataRow[] drr = dtTable.Select("tableCode = '" + tablecode + "'");
                    if (printObj.tableType == 1)
                    {
                        #region 横表
                        DataView dv = new DataView(dtClone);
                        dv.Sort = "tableCode asc, rowIndex asc";
                        dtClone = dv.ToTable();

                        int intAcrossCols = printObj.acrossCols == null ? 1 : Function.Int(printObj.acrossCols);
                        int intVrows = printObj.vrows == null ? 0 : Function.Int(printObj.vrows);
                        dtData.Columns.Clear();
                        for (int j = 0; j < intAcrossCols; j++)
                        {
                            foreach (string colName in lstTableColName)
                            {
                                dtData.Columns.Add(colName + "_" + j.ToString(), typeof(string));
                                if (lstSelfDefineCol.Count > 0)
                                {
                                    if (lstSelfDefineCol.Exists(t => t.colCode == colName))
                                    {
                                        if (colName.ToLower().StartsWith("selfdefine") && !dtData.Columns.Contains("lbl" + colName))
                                        {
                                            dtData.Columns.Add("lbl" + colName, typeof(string));
                                        }
                                    }
                                }
                            }
                        }
                        int maxRows = 0;
                        foreach (DataRow dr in drr)
                        {
                            maxRows = Math.Max(Function.Int(dr["rowIndex"].ToString()), maxRows);
                        }
                        foreach (EntityEmrSelfDefineCol item in lstSelfDefineCol)
                        {
                            item.vRows = intVrows;
                        }

                        int currRow = 0;
                        string findRow = string.Empty;
                        string fieldName = string.Empty;
                        int maxPage = Convert.ToInt32(Math.Ceiling((double)(maxRows + 1) / (double)intAcrossCols));
                        for (int k = 1; k <= maxPage; k++)
                        {
                            currRow = 0;
                            findRow = string.Empty;
                            for (int l = 1; l <= intAcrossCols; l++)
                            {
                                findRow += Convert.ToString(k * intAcrossCols - l) + ",";
                            }
                            findRow = findRow.Substring(0, findRow.Length - 1);
                            drr = dtClone.Select("tableCode = '" + tablecode + "' and rowIndex in (" + findRow + ")");

                            DataRow drNew = dtData.NewRow();
                            foreach (DataRow dr1 in drr)
                            {
                                currRow = int.Parse(dr1["rowIndex"].ToString()) - (k - 1) * intAcrossCols;
                                foreach (string colName in lstTableColName)
                                {
                                    fieldName = colName + "_" + currRow.ToString();
                                    if (lstTableField.Any(t => t.fieldName == colName))
                                    {
                                        if (lstTableField.FirstOrDefault(t => t.fieldName == colName).fieldType == "是否")
                                            drNew[fieldName] = Function.Int(dr1[colName]) == 1 ? "√" : "";
                                        else
                                            drNew[fieldName] = dr1[colName];
                                    }
                                    else
                                    {
                                        drNew[fieldName] = dr1[colName];
                                    }
                                    if (lstSelfDefineCol.Count > 0)
                                    {
                                        if (lstSelfDefineCol.Exists(t => t.colCode == colName && t.pageNo == k))
                                        {
                                            EntityEmrSelfDefineCol selfVo = lstSelfDefineCol.FirstOrDefault(t => t.colCode == colName && t.pageNo == k);
                                            if (colName.ToLower().StartsWith("selfdefine"))
                                                drNew["lbl" + colName] = selfVo.colDesc;
                                            else
                                                drNew[colName] = selfVo.colDesc;
                                        }
                                    }
                                }
                            }
                            dtData.Rows.Add(drNew);
                        }
                        dtData.AcceptChanges();
                        dsPrint.Tables.Add(dtData);
                        #endregion
                    }
                    else
                    {
                        #region 竖表
                        foreach (string colName in lstTableColName)
                        {
                            if (lstSelfDefineCol.Count > 0)
                            {
                                if (lstSelfDefineCol.Exists(t => t.colCode == colName))
                                {
                                    if (colName.ToLower().StartsWith("selfdefine") && !dtData.Columns.Contains("lbl" + colName))
                                    {
                                        dtData.Columns.Add("lbl" + colName, typeof(string));
                                    }
                                }
                            }
                        }
                        int intVrows = printObj.vrows == null ? 0 : Function.Int(printObj.vrows);
                        foreach (EntityEmrSelfDefineCol item in lstSelfDefineCol)
                        {
                            item.vRows = intVrows;
                        }
                        int pageNo = 0;
                        if (drr != null && drr.Length > 0)
                        {
                            foreach (DataRow dr in drr)
                            {
                                DataRow drNew = dtData.NewRow();
                                foreach (string colName in lstTableColName)
                                {
                                    if (lstTableField.Any(t => t.fieldName == colName))
                                    {
                                        if (lstTableField.FirstOrDefault(t => t.fieldName == colName).fieldType == "是否")
                                        {
                                            drNew[colName] = Function.Int(dr[colName]) == 1 ? "√" : "";
                                        }
                                        else
                                            drNew[colName] = dr[colName];
                                    }
                                    else
                                    {
                                        drNew[colName] = dr[colName];
                                    }
                                    if (lstSelfDefineCol.Count > 0)
                                    {
                                        if (lstSelfDefineCol[0].vRows > 1)
                                        {
                                            pageNo = Convert.ToInt32(Math.Ceiling((double)(drr.Length + 1) / (double)lstSelfDefineCol[0].vRows));
                                            if (lstSelfDefineCol.Exists(t => t.colCode == colName && t.pageNo == pageNo))
                                            {
                                                EntityEmrSelfDefineCol selfVo = lstSelfDefineCol.FirstOrDefault(t => t.colCode == colName && t.pageNo == pageNo);
                                                if (colName.ToLower().StartsWith("selfdefine"))
                                                    drNew["lbl" + colName] = selfVo.colDesc;
                                                else
                                                    drNew[colName] = selfVo.colDesc;
                                            }
                                        }
                                    }
                                }
                                dtData.Rows.Add(drNew);
                                dtData.AcceptChanges();
                            }
                        }
                        for (int i = 0; i < dtData.Columns.Count; i++)
                        {
                            if (lstTableField.Any(t => t.fieldName == dtData.Columns[i].ColumnName))
                            {
                                dtData.Columns[i].ColumnName = lstTableField.FirstOrDefault(t => t.fieldName == dtData.Columns[i].ColumnName).fieldCaptain;
                            }
                        }
                        DataView dv = new DataView(dtData);
                        dv.Sort = "rowIndex asc";
                        DataTable dtNew = dv.ToTable();
                        #region 补空行
                        if (printObj.vrows > 1)
                        {
                            int intPageRows = Function.Int(printObj.vrows);
                            int intBlankRows = dtNew.Rows.Count % intPageRows;
                            if (intBlankRows > 0)
                            {
                                for (int i = 0; i < intPageRows - intBlankRows; i++)
                                {
                                    dtNew.Rows.Add(dtNew.NewRow());
                                }
                            }
                            dtNew.AcceptChanges();
                        }
                        #endregion
                        dsPrint.Tables.Add(dtNew);
                        #endregion
                    }
                }
            }
            #endregion

            return dsPrint;
        }
        #endregion

        #region ConvertSignImage
        /// <summary>
        /// ConvertSignImage
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        static DataTable ConvertSignImage(DataTable dataSource, int imageWidth, int imageHeight)
        {
            Dictionary<string, string> dicSignImage = new Dictionary<string, string>();
            List<string> lstCols = new List<string>();
            foreach (DataColumn dc in dataSource.Columns)
            {
                lstCols.Add(dc.ColumnName);
            }
            foreach (string colName in lstCols)
            {
                if (colName.IndexOf("签名") >= 0)
                {
                    string fieldName = colName;
                    string fieldName2 = colName + "Image";
                    dicSignImage.Add(fieldName, fieldName2);
                    dataSource.Columns.Add(fieldName2, typeof(byte[]));
                    foreach (DataRow dr1 in dataSource.Rows)
                    {
                        string empName = dr1[fieldName] == DBNull.Value ? "" : dr1[fieldName].ToString().Trim();
                        if (empName != "")
                        {
                            using (ProxyFormDesign proxy = new ProxyFormDesign())
                            {
                                byte[] empSignData = proxy.Service.GetEmpSign(empName);
                                Image empSignImg = null;
                                if (empSignData != null)
                                {
                                    empSignImg = Function.ConvertByteToImage(empSignData);
                                    empSignImg = Function.Thumbnail(empSignImg, imageWidth, imageHeight); //Function.PictureProcess(empSignImg, imageWidth, imageHeight);
                                    dr1[fieldName2] = Function.ConvertImageToByte(empSignImg, 3);
                                    dataSource.AcceptChanges();
                                }
                            }
                        }
                    }
                }
            }
            if (dicSignImage.Count > 0)
            {
                foreach (KeyValuePair<string, string> kv in dicSignImage)
                {
                    dataSource.Columns.Remove(kv.Key);
                    dataSource.Columns[kv.Value].ColumnName = kv.Key;
                }
            }
            return dataSource;
        }
        #endregion

        #region GetXtraReport
        /// <summary>
        /// GetXtraReport
        /// </summary>
        /// <param name="printTemplate"></param>
        /// <param name="printData"></param>
        /// <returns></returns>
        public static XtraReport GetXtraReport(byte[] printTemplate, DataSet printData)
        {
            if (printData == null)
            {
                DialogBox.Msg("打印数据源为空");
                return null;
            }
            XtraReport xr = new XtraReport();
            MemoryStream stream = new MemoryStream(printTemplate);
            xr.LoadLayout(stream);
            // 主
            if (printData.Tables.Contains("Main")) xr.DataSource = ConvertSignImage(printData.Tables["Main"], 70, 30);
            // 表格
            foreach (Band band in xr.Bands)
            {
                foreach (XRControl xrControl in band.Controls)
                {
                    if (xrControl is XRSubreport && printData.Tables.Contains(xrControl.Name))
                    {
                        using (ProxyFormDesign proxy = new ProxyFormDesign())
                        {
                            EntityEmrPrintTemplate printObj = proxy.Service.GetFormPrintTemplate(2, xrControl.Name);
                            if (printObj != null && printObj.templateFile != null)
                            {
                                XtraReport xrSub = new XtraReport();
                                MemoryStream streamSub = new MemoryStream(printObj.templateFile);
                                xrSub.LoadLayout(streamSub);
                                if (printObj.tableType == 1)
                                {
                                    AutoBanding(ref xrSub, Function.Int(printObj.acrossCols), 1, xrControl.Name);
                                    DataTable data = printData.Tables[xrControl.Name];
                                    ConvertCols(printObj.templateColumns, ref data);
                                    xrSub.DataSource = data;
                                }
                                else
                                {
                                    xrSub.DataSource = ConvertSignImage(printData.Tables[xrControl.Name], 60, 26);
                                }
                                ((XRSubreport)xrControl).ReportSource = xrSub;
                            }
                        }
                    }
                }
            }
            return xr;
        }
        #endregion

        #region ConvertCols
        /// <summary>
        /// ConvertCols
        /// </summary>
        /// <param name="templateColumns"></param>
        /// <param name="dataSource"></param>
        static void ConvertCols(byte[] templateColumns, ref DataTable dataSource)
        {
            //列名转换
            char[] sep = new char[] { '\r', '\n' };
            string strCols = "";

            if (templateColumns != null)
            {
                strCols = System.Text.Encoding.Default.GetString(templateColumns);
            }
            string[] cols = strCols.Split(sep);
            foreach (string col in cols)
            {
                string[] thisCol = col.Split(',');
                string fieldName = thisCol[0].Trim();
                string displayName = thisCol[0].Trim();
                if (thisCol.Length > 1)
                {
                    displayName = thisCol[1].Trim();
                }
                if (dataSource.Columns.Contains(fieldName) && !dataSource.Columns.Contains(displayName))
                {
                    dataSource.Columns[fieldName].ColumnName = displayName;
                }
            }
            // 表格.签名图片
            dataSource = ConvertSignImage(dataSource, 60, 26);
        }
        #endregion

        #region 自动绑定
        /// <summary>
        /// 自动绑定
        /// </summary>
        /// <param name="xr"></param>
        /// <param name="cols"></param>
        /// <param name="typeId"></param>
        /// <param name="tableCode"></param>
        static void AutoBanding(ref XtraReport xr, int cols, int typeId, string tableCode)
        {
            List<EntityEmrTableFieldInfo> lstField = null;
            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                lstField = proxy.Service.GetTableFieldInfo(tableCode);
            }

            foreach (Band band in xr.Bands)
            {
                foreach (XRControl xrControl in band.Controls)
                {
                    if (xrControl is XRTable)
                    {
                        XRTable tb = (XRTable)xrControl;
                        XRRichText xrRich = null;
                        EntityEmrTableFieldInfo objColumn = null;
                        string strColName = string.Empty;
                        List<string> lstColCode = new List<string>();

                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            lstColCode.Clear();
                            if (typeId == 1)
                            {
                                lstColCode.Add(tb.Rows[i].Name);
                            }
                            else if (typeId == 2)
                            {
                                for (int k = 0; k < tb.Rows[i].Cells.Count; k++)
                                {
                                    strColName = tb.Rows[i].Cells[k].Name;
                                    if (strColName.Length > 1)
                                        strColName = strColName.Substring(0, strColName.Length - 1);
                                    if (lstColCode.IndexOf(strColName) >= 0) continue;
                                    if (lstField.Exists(t => t.fieldName == strColName))
                                    {
                                        lstColCode.Add(strColName);
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }
                            if (lstColCode.Count == 0) continue;
                            foreach (string strColCode in lstColCode)
                            {
                                if (lstField.Exists(t => t.fieldName == strColCode))
                                {
                                    objColumn = lstField.Single(t => t.fieldName == strColCode);
                                    if (objColumn != null)
                                    {
                                        for (int j = 0; j < cols; j++)
                                        {
                                            strColName = strColCode + j.ToString();
                                            //if (objColumn.Fieldtype_vchr == "病历")
                                            //{
                                            //    xrRich = new XRRichText();
                                            //    xrRich.DataBindings.Add("Rtf", null, objColumn.Fieldcaptain_vchr + j.ToString() + "_" + strColCode);
                                            //    xrRich.Location = new Point(0, 0);
                                            //    xrRich.Size = tb.Rows[i].Cells[strColName].Size;
                                            //    xrRich.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                            //    xrRich.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;// .MiddleCenter;
                                            //    xrRich.Text = string.Empty;
                                            //    xrRich.BringToFront();
                                            //    tb.Rows[i].Cells[strColName].Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { xrRich });
                                            //}
                                            //else
                                            //{
                                            try
                                            {
                                                if (objColumn.fieldCaptain.IndexOf("签名") >= 0)
                                                {
                                                    tb.Rows[i].Cells[strColName].Controls[0].DataBindings.Add("Image", null, objColumn.fieldCaptain + j.ToString() + "_" + strColCode);
                                                }
                                                else
                                                {
                                                    tb.Rows[i].Cells[strColName].DataBindings.Add("Text", null, objColumn.fieldCaptain + j.ToString() + "_" + strColCode);
                                                    tb.Rows[i].Cells[strColName].Text = string.Empty;
                                                    tb.Rows[i].Cells[strColName].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                DialogBox.Msg("行号：" + i.ToString() + " 名称：" + strColName + Environment.NewLine + ex.Message);
                                            }
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
