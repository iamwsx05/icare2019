using System;
using System.Windows.Forms;
using System.Drawing;
using weCare.Core.Entity;
using com.digitalwave.iCare.common;

namespace com.digitalwave.iCare.BIHOrder.Control
{
    #region clsAreaTextBox
    /// <summary>
    /// ����
    /// </summary>
    public class clsAreaTextBox : ctlFindTextBox
    {
        //private clsBIHOrderService m_objService=new clsBIHOrderService();
        //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
        private string m_strAreaID = "";
        private string m_strAreaName = "";
        public string AreaID
        {
            get { return m_strAreaID; }
            set
            {
                m_strAreaID = value;
                this.Tag = m_strAreaID;
            }
        }
        public string AreaName
        {
            get { return m_strAreaName; }
            set
            {
                m_strAreaName = value;
                this.Text = m_strAreaName;
            }
        }

        public clsAreaTextBox() : base()
        {
            this.m_evtFindItem += new EventHandler_OnFindItem(m_txtArea_m_evtFindItem);
            this.m_evtInitListView += new EventHandler_InitListView(m_txtArea_m_evtInitListView);
            this.m_evtSelectItem += new EventHandler_OnSelectItem(m_txtArea_m_evtSelectItem);
            this.Tag = "";
        }

        #region Area

        private void m_txtArea_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHArea[] arrArea;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode, out arrArea);
            if ((ret > 0) && (arrArea != null))
            {
                for (int i = 0; i < arrArea.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrArea[i].m_strAreaName);
                    lvi.Tag = arrArea[i];
                }
            }
        }

        private void m_txtArea_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("", 120, HorizontalAlignment.Left);
            lvwList.Width = 140;
        }

        private void m_txtArea_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                this.Text = lviSelected.Text;
                this.Tag = (lviSelected.Tag as clsBIHArea).m_strAreaID;
                m_strAreaName = this.Text.Trim();
                m_strAreaID = this.Tag.ToString().Trim();
            }
        }

        #endregion

        public void m_mthSetArea(string strAreaID, string strAreaName)
        {
            this.Text = strAreaName;
            this.Tag = strAreaID;
            m_strAreaID = strAreaID;
            m_strAreaName = strAreaName;
        }

        public void m_mthSetArea(string strAreaID)
        {
            clsBIHArea objArea;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAreaByID(strAreaID, out objArea);
            if (objArea == null)
            {
                this.Text = "";
                this.Tag = "";
            }
            else
            {
                this.Text = objArea.m_strAreaName;
                this.Tag = objArea.m_strAreaID;
                m_strAreaID = objArea.m_strAreaID;
                m_strAreaName = objArea.m_strAreaName;
            }

        }

    }

    #endregion
    #region clsBedTextBox

    public class clsBedTextBox : ctlFindTextBox
    {
        //private clsBIHOrderService m_objService=new clsBIHOrderService();
        //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
        public clsBedTextBox() : base()
        {
            this.m_evtFindItem += new EventHandler_OnFindItem(clsBedTextBox_m_evtFindItem);
            this.m_evtInitListView += new EventHandler_InitListView(clsBedTextBox_m_evtInitListView);
            this.m_evtSelectItem += new EventHandler_OnSelectItem(clsBedTextBox_m_evtSelectItem);
            this.Tag = "";
        }

        private string m_strAreaID = "";
        public string AreaID
        {
            get
            {
                return m_strAreaID;
            }
            set
            {
                if (m_strAreaID != value)
                {
                    this.Text = "";
                    this.Tag = "";

                    m_strAreaID = value;
                }
            }
        }

        private void clsBedTextBox_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            if (m_strAreaID.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("����ָ������!");
                return;
            }

            clsBIHBed[] arrBed;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBedByArea(m_strAreaID, strFindCode, out arrBed);
            if ((ret > 0) && (arrBed != null))
            {
                for (int i = 0; i < arrBed.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrBed[i].m_strBedName);
                    lvi.Tag = arrBed[i];
                }
            }
        }

        private void clsBedTextBox_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("", 100, HorizontalAlignment.Left);
            lvwList.Width = 120;
        }

        private void clsBedTextBox_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                this.Text = lviSelected.Text;
                this.Tag = (lviSelected.Tag as clsBIHBed).m_strBedID;
            }
        }
        public string BedID
        {
            get
            {
                if (this.Tag == null)
                    return "";
                else
                    return (this.Tag.ToString().Trim());
            }
            set
            {
                this.Tag = value;
            }
        }
    }
    #endregion
    #region clsDoctorTextBox

    public class clsDoctorTextBox : ctlFindTextBox
    {
        //private clsBIHOrderService m_objService=new clsBIHOrderService();
        public clsDoctorTextBox() : base()
        {
            this.m_evtFindItem += new EventHandler_OnFindItem(m_txtDoctor_m_evtFindItem);
            this.m_evtInitListView += new EventHandler_InitListView(m_txtDoctor_m_evtInitListView);
            this.m_evtSelectItem += new EventHandler_OnSelectItem(m_txtDoctor_m_evtSelectItem);
        }
        #region Doctor

        private void m_txtDoctor_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsBIHOrderService();
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            clsBIHDoctor[] arrDoctor;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngFindDoctor(strFindCode, out arrDoctor);
            if ((ret > 0) && (arrDoctor != null))
            {
                for (int i = 0; i < arrDoctor.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrDoctor[i].m_strDoctorNo);
                    lvi.SubItems.Add(arrDoctor[i].m_strDoctorName);
                    lvi.Tag = arrDoctor[i];
                }
            }
        }

        private void m_txtDoctor_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            this.Text = lviSelected.SubItems[1].Text;
            this.Tag = (lviSelected.Tag as clsBIHDoctor).m_strDoctorID;
            m_strDoctorID = this.Tag.ToString().Trim();
            m_strDoctorName = this.Text.Trim();
        }

        private void m_txtDoctor_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Clear();
            ColumnHeader ch1 = lvwList.Columns.Add("No", 60, HorizontalAlignment.Left);
            ColumnHeader ch2 = lvwList.Columns.Add("Name", 60, HorizontalAlignment.Left);
            lvwList.Width = 140;
        }

        #endregion

        public void m_mthSetDoctor(string DoctorID, string DoctorName)
        {
            this.Text = DoctorName;
            this.Tag = DoctorID;
            m_strDoctorID = DoctorID;
            m_strDoctorName = DoctorName;
        }
        private string m_strDoctorID = "";
        public string DoctorID
        {
            get { return m_strDoctorID; }
            set
            {
                m_strDoctorID = value;
                this.Tag = m_strDoctorID;
            }
        }
        private string m_strDoctorName = "";
        public string DoctorName
        {
            get { return m_strDoctorName; }
            set
            {
                m_strDoctorName = value;
                this.Text = m_strDoctorName;
            }
        }
    }
    #endregion
    #region clsAsyncCall
    public class clsAsyncCall
    {
        private event clsAsyncCallStart m_objStart;

        private void AllFun()
        {
            if (m_objStart != null)
            {
                m_objStart();
            }
        }
        public clsAsyncCall AddMethod(clsAsyncCallStart objMethod)
        {
            m_objStart += objMethod;
            return this;
        }

        public void Run()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(AllFun)).Start();
        }
    }
    public delegate void clsAsyncCallStart();
    #endregion
    #region clsTextFocusHighlight
    public class clsTextFocusHighlight
    {
        private void m_mthEnter(object sender, System.EventArgs e)
        {
            System.Drawing.Color clrBack = System.Drawing.SystemColors.Info;

            if (sender is System.Windows.Forms.TextBox)
            {
                System.Windows.Forms.TextBox objTxt = sender as System.Windows.Forms.TextBox;
                if (objTxt.Enabled && (!objTxt.ReadOnly))
                {
                    objTxt.BackColor = clrBack;
                    objTxt.SelectAll();
                }
            }
            else if (sender is System.Windows.Forms.ComboBox)
            {
                System.Windows.Forms.ComboBox objCbo = sender as System.Windows.Forms.ComboBox;
                if (objCbo.Enabled)
                {
                    objCbo.BackColor = clrBack;
                }
            }
        }
        private void m_mthLeave(object sender, System.EventArgs e)
        {
            System.Drawing.Color clrBack = System.Drawing.Color.White;

            if (sender is System.Windows.Forms.TextBox)
            {
                System.Windows.Forms.TextBox objTxt = sender as System.Windows.Forms.TextBox;
                if (objTxt.Enabled && (!objTxt.ReadOnly))
                {
                    objTxt.BackColor = clrBack;
                }
            }
            else if (sender is System.Windows.Forms.ComboBox)
            {
                System.Windows.Forms.ComboBox objCbo = sender as System.Windows.Forms.ComboBox;
                if (objCbo.Enabled)
                {
                    objCbo.BackColor = clrBack;
                }
            }
        }


        public void m_mthBindTextBox(System.Windows.Forms.Control objCtrl)
        {
            objCtrl.Enter += new EventHandler(m_mthEnter);
            objCtrl.Leave += new EventHandler(m_mthLeave);
        }

        public void m_mthBindForm(System.Windows.Forms.Control objForm)
        {
            foreach (System.Windows.Forms.Control ctrl in objForm.Controls)
            {
                if ((ctrl is System.Windows.Forms.TextBox) || (ctrl is System.Windows.Forms.ComboBox))
                {
                    m_mthBindTextBox(ctrl as System.Windows.Forms.Control);
                }
            }
        }

        public void m_mthBindForm(System.Windows.Forms.Control objForm, bool blnRecursive)
        {
            if (blnRecursive)
            {
                //�ݹ�
                m_mthBindControl(objForm);
            }
            else
            {
            }
        }
        private void m_mthBindControl(System.Windows.Forms.Control objControl)
        {
            foreach (System.Windows.Forms.Control ctrl in objControl.Controls)
            {
                if ((ctrl is System.Windows.Forms.TextBox) || (ctrl is System.Windows.Forms.ComboBox))
                {
                    m_mthBindTextBox(ctrl as System.Windows.Forms.Control);
                }

                if ((ctrl is System.Windows.Forms.Panel) || (ctrl is System.Windows.Forms.GroupBox))
                {
                    m_mthBindControl(ctrl);
                }
            }
        }


        public void m_mthBindControls(System.Windows.Forms.Control[] arrControl)
        {
            if ((arrControl != null) && (arrControl.Length > 0))
            {
                for (int i = 0; i < arrControl.Length; i++)
                {
                    m_mthBindTextBox(arrControl[i]);
                }
            }
        }
    }
    #endregion

    #region clsOrderStatus
    public class clsOrderStatus
    {
        /// <summary>
        /// ����ҽ������״̬������ɫ
        /// </summary>
        /// <param name="intType">ҽ������	{1=����;2=��ʱ}</param>
        /// <param name="intStatus">ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}</param>
        /// <param name="clrBack">[��������ҽ������]</param>
        /// <param name="clrFore">[��������ִ��״̬]</param>
        public static void s_mthGetColorByStatus(int intType, int intStatus, out Color clrBack, out Color clrFore)
        {
            clrFore = clsOrderColor.ForeColor;
            clrBack = clsOrderColor.BackColor;

            if (intType == 1)
                clrBack = clsOrderColor.BackColorLongOrder;
            else if (intType == 2)
                clrBack = clsOrderColor.BackColorTemOrder;
            else
                clrBack = clsOrderColor.BackColor;

            switch (intStatus)
            {
                case 0:     //�½�
                    clrFore = clsOrderColor.ForeColorOrderStatus0;
                    break;
                case 1:     //���ύ
                    clrFore = clsOrderColor.ForeColorOrderStatus1;
                    break;
                case 2:     //ִ��
                    clrFore = clsOrderColor.ForeColorOrderStatus2;
                    break;
                case 3:     //ֹͣ
                    clrFore = clsOrderColor.ForeColorOrderStatus3;
                    break;
                case 4:     //����
                    clrFore = clsOrderColor.ForeColorOrderStatus4;
                    break;
                case 5:     //����ύ
                    clrFore = clsOrderColor.ForeColorOrderStatus5;
                    break;
                case 6:     //���ֹͣ
                    clrFore = clsOrderColor.ForeColorOrderStatus6;
                    break;
                case 7:     //�˻�
                    clrFore = clsOrderColor.ForeColorOrderStatus7;
                    break;
                case -1:    //����
                    clrFore = clsOrderColor.ForeColorOrderStatus_1;
                    break;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="intPStatus"></param>
        /// <returns></returns>
        public static string m_strGetChargeStatusMessage(int intPStatus)
        {
            string strStatus = "";
            switch (intPStatus)
            {
                case 0:
                    strStatus = "��ȷ��";
                    break;
                case 1:
                    strStatus = "����";
                    break;
                case 2:
                    strStatus = "����";
                    break;
                case 3:
                    strStatus = "����";
                    break;
                case 4:
                    strStatus = "ֱ��";
                    break;
            }
            return strStatus;
        }
    }
    #endregion
    #region ҽ����ɫ
    public class clsOrderColor
    {
        /// <summary>
        /// ����������ɫ
        /// </summary>
        public static Color BackColor = SystemColors.Control;
        /// <summary>
        /// ����ǰ����ɫ
        /// </summary>
        public static Color ForeColor = SystemColors.ControlText;
        /// <summary>
        /// ����ҽ��������ɫ
        /// </summary>
        public static Color BackColorLongOrder = SystemColors.Window;//Thistle
                                                                     /// <summary>
                                                                     /// ��ʱҽ��������ɫ
                                                                     /// </summary>
        public static Color BackColorTemOrder = Color.Bisque;//LightGray;//SkyBlue;
                                                             /// <summary>
                                                             /// 0-�½�״̬ǰ��ɫ
                                                             /// </summary>
        public static Color ForeColorOrderStatus0 = Color.Black;
        /// <summary>
        /// 1-�ύ״̬ǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus1 = Color.Blue;
        /// <summary>
        /// 2-ִ��״̬ǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus2 = Color.Green;
        /// <summary>
        /// 3-ֹͣ״̬ǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus3 = Color.Red;
        /// <summary>
        /// 4-����״̬ǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus4 = Color.Brown;
        /// <summary>
        /// 5-����ύǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus5 = Color.CornflowerBlue;
        /// <summary>
        /// 6-���ֹͣǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus6 = Color.DarkRed;
        /// <summary>
        /// 7-�˻�ǰ��ɫ
        /// </summary>
        public static Color ForeColorOrderStatus7 = Color.DarkCyan;//Olive;
                                                                   /// <summary>
                                                                   /// -1����״̬ǰ��ɫ
                                                                   /// </summary>
        public static Color ForeColorOrderStatus_1 = Color.Yellow;
        /// <summary>
        /// ����ִ�е�ҽ��ǰ��ɫ
        /// </summary>
        public static Color ForeColorCanExecute = Color.Teal;
        /// <summary>
        /// ����ִ�е�ҽ��ǰ��ɫ
        /// </summary>
        public static Color ForeColorCanNotExecute = Color.Red;
        /// <summary>
        /// ����ִ�е�ҽ������ɫ
        /// </summary>
        public static Color BackColorCanExecute = SystemColors.GrayText;
        /// <summary>
        /// ����ִ�е�ҽ������ɫ
        /// </summary>
        public static Color BackColorCanNotExecute = SystemColors.Control;
        /// <summary>
        /// �����������޵ı���ɫ
        /// </summary>
        public static Color BackColorChargeUnderLowerLimit = Color.Tomato;
        /// <summary>
        /// �����������޵�ǰ��ɫ
        /// </summary>
        public static Color ForeColorChargeUnderLowerLimit = Color.Green;
    }
    #endregion

    #region clsGenerator
    //public class clsGenerator
    //{
    //	public static object CreateObject(System.Type objType)
    //	{
    //		return com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(objType);
    //	}
    //}
    #endregion
    #region clsDoseTypeTextBox �÷�

    public class clsDoseTypeTextBox : System.Windows.Forms.TextBox
    {
        private ListView m_objListView;
        private System.Windows.Forms.Form m_objForm;
        //private clsBIHOrderService m_objService=(clsBIHOrderService)(clsGenerator.CreateObject(typeof(clsBIHOrderService)));
        //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
        public clsDoseTypeTextBox() : base()
        {
        }

        public void m_mthSetForm(System.Windows.Forms.Form objForm)
        {
            m_objForm = objForm;

            m_mthInitListView();

            //this.KeyDown += new KeyEventHandler(clsDoseTypeTextBox_KeyDown);
            //this.Enter += new EventHandler(clsDoseTypeTextBox_Enter);
            //this.Leave += new EventHandler(clsDoseTypeTextBox_Leave);
        }


        /// <summary>
        /// ��ȡ��ѡ�е��÷�
        /// </summary>
        public clsBSEUsageType[] SelectUsageType
        {
            get
            {


                System.Collections.ArrayList arl = new System.Collections.ArrayList();
                for (int i = 0; i < m_objListView.Items.Count; i++)
                {
                    if (m_objListView.Items[i].Checked)
                        arl.Add(m_objListView.Items[i].Tag);
                }

                return (clsBSEUsageType[])(arl.ToArray(typeof(clsBSEUsageType)));
            }
        }
        /// <summary>
        /// ��ȡ�����ñ�ѡ�е��÷�(ͨ��ID)
        /// </summary>
        public string[] SelectUsageTypeIDs
        {
            get
            {
                clsBSEUsageType[] arrType = SelectUsageType;
                string[] arrID = new string[arrType.Length];
                for (int i = 0; i < arrType.Length; i++)
                {
                    arrID[i] = arrType[i].m_strUsageID;
                }
                return arrID;
            }
            set
            {
                if (value == null) value = new string[0];
                for (int i = 0; i < m_objListView.Items.Count; i++)
                {
                    clsBSEUsageType objType = (m_objListView.Items[i].Tag as clsBSEUsageType);
                    if (objType == null) continue;
                    string strTypeID = objType.m_strUsageID.Trim();
                    m_objListView.Items[i].Checked = false;

                    for (int j = 0; j < value.Length; j++)
                    {
                        if (strTypeID == value[j].Trim())
                        {
                            m_objListView.Items[i].Checked = true;
                            break;
                        }
                    }
                }
                m_mthRefreshText();
            }
        }

        private void m_mthInitListView()
        {
            m_objListView = new ListView();

            //
            m_objListView.Width = this.Width;
            m_objListView.Columns.Add("�÷�����", this.Width - 30, HorizontalAlignment.Left);
            m_objListView.CheckBoxes = true;
            m_objListView.Visible = false;
            m_objListView.View = View.Details;
            m_objListView.Height = 120;
            m_objListView.HeaderStyle = ColumnHeaderStyle.None;

            m_objListView.Leave += new EventHandler(m_objListView_Leave);
            m_objListView.KeyDown += new KeyEventHandler(m_objListView_KeyDown);
            //
            clsBSEUsageType[] arrType;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageType("", out arrType);
            if ((ret > 0) && (arrType != null))
            {
                for (int i = 0; i < arrType.Length; i++)
                {
                    ListViewItem lvi = m_objListView.Items.Add(arrType[i].m_strUsageName);
                    lvi.Tag = arrType[i];
                }
            }

            //
            m_objForm.Controls.Add(m_objListView);

        }

        private void clsDoseTypeTextBox_Enter(object sender, EventArgs e)
        {
            m_mthShowListView();
        }

        private void m_mthShowListView()
        {
            Point pt = this.Location;
            pt = this.Parent.PointToScreen(pt);
            if (pt.Y < 700 - m_objListView.Height)
            {
                pt = new Point(pt.X, pt.Y + this.Height);
            }
            else
            {
                pt = new Point(pt.X, pt.Y - m_objListView.Height);
            }

            //m_frmListView.Width=this.Width;
            pt = m_objForm.PointToClient(pt);
            m_objListView.Location = pt;
            m_objListView.Visible = true;
            m_objListView.BringToFront();

            m_objListView.Focus();

        }

        public void m_mthRefreshText()
        {
            string strText = "";
            for (int i = 0; i < m_objListView.Items.Count; i++)
            {
                if (m_objListView.Items[i].Checked)
                {
                    if (strText.Length > 0) strText += ";";
                    strText += m_objListView.Items[i].Text.Trim();
                }
            }
            this.Text = strText;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            m_mthRefreshText();
        }

        private void m_objListView_Leave(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                m_objListView.Visible = false;
                m_mthRefreshText();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                m_mthShowListView();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                m_objListView.Visible = false;
            }
        }

        private void m_objListView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Escape))
            {
                m_objListView_Leave(sender, e);
            }
        }
    }
    #endregion
    #region clsSelectBedControl �÷�
    /// <summary>
    /// �����ؼ�
    /// ע�⣺�ָ���Ϊ��,���ţ�
    /// </summary>	
    public class clsSelectBedControl : System.Windows.Forms.TextBox
    {
        #region ��������
        private ListView m_objListView;
        System.Windows.Forms.Form m_objForm;
        /// <summary>
        /// ����ID
        /// </summary>
        private string m_strAreaID = "";
        private clsDcl_InputOrder m_objManage = new clsDcl_InputOrder();
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsSelectBedControl() : base()
        {
        }
        #endregion
        #region ����
        /// <summary>
        /// ��ȡ��ѡ�еĲ�������
        /// </summary>
        public clsT_Bse_Bed_VO[] m_GetSelectBed
        {
            get
            {
                System.Collections.ArrayList arl = new System.Collections.ArrayList();
                for (int i = 0; i < m_objListView.Items.Count; i++)
                {
                    if (m_objListView.Items[i].Checked)
                        arl.Add(m_objListView.Items[i].Tag);
                }
                return (clsT_Bse_Bed_VO[])(arl.ToArray(typeof(clsT_Bse_Bed_VO)));
            }
        }
        /// <summary>
        /// ��ȡ������ѡ�еĲ���	[����ID]
        /// </summary>
        public string[] m_SelectBed
        {
            get
            {
                clsT_Bse_Bed_VO[] objItemArr = m_GetSelectBed;
                string[] strBedIDArr = new string[objItemArr.Length];
                for (int i = 0; i < strBedIDArr.Length; i++)
                {
                    strBedIDArr[i] = objItemArr[i].m_strBEDID_CHR;
                }
                return strBedIDArr;
            }
            set
            {
                if (value == null) value = new string[0];
                for (int i = 0; i < m_objListView.Items.Count; i++)
                {
                    clsT_Bse_Bed_VO objItem = (m_objListView.Items[i].Tag as clsT_Bse_Bed_VO);
                    if (objItem == null) continue;
                    string stBedID = objItem.m_strBEDID_CHR.Trim();
                    m_objListView.Items[i].Checked = false;
                    for (int j = 0; j < value.Length; j++)
                    {
                        if (stBedID == value[j].Trim())
                        {
                            m_objListView.Items[i].Checked = true;
                            break;
                        }
                    }
                }
                m_mthRefreshText();
            }
        }

        public string m_SetAreaID
        {
            set
            {
                m_strAreaID = value;
                LoadListViewItem(m_strAreaID);
            }
        }
        #endregion
        #region ˽�з���
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        public void InitControl(System.Windows.Forms.Form objForm)
        {
            //
            m_objForm = objForm;

            m_objListView = new ListView();
            m_objListView.Width = this.Width;
            m_objListView.Columns.Add("����", this.Width - 30, HorizontalAlignment.Left);
            m_objListView.CheckBoxes = true;
            m_objListView.Visible = true;
            m_objListView.View = View.Details;
            m_objListView.Height = 120;
            m_objListView.HeaderStyle = ColumnHeaderStyle.None;
            m_objListView.Leave += new EventHandler(m_objListView_Leave);
            m_objListView.KeyDown += new KeyEventHandler(m_objListView_KeyDown);

            m_objForm.Controls.Add(m_objListView);
        }
        private void LoadListViewItem(string p_strAreaID)
        {
            m_objListView.Items.Clear();
            if (p_strAreaID == null || p_strAreaID.Trim() == "") return;
            clsT_Bse_Bed_VO[] objItemArr;
            long lngRes = m_objManage.m_lngGetBedInfoByAreaID(m_strAreaID, out objItemArr);
            if (lngRes > 0 && objItemArr != null)
            {
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    ListViewItem lvi = m_objListView.Items.Add(objItemArr[i].m_strCODE_CHR);
                    lvi.Tag = objItemArr[i];
                }
            }
        }
        private void m_mthShowListView()
        {
            Point pt = this.Location;
            pt = this.Parent.PointToScreen(pt);
            int intHeight = this.Parent.Height;

            if (pt.Y < intHeight - m_objListView.Height)
            {
                pt = new Point(pt.X, pt.Y + this.Height);
            }
            else
            {
                pt = new Point(pt.X, pt.Y - m_objListView.Height);
            }
            pt = m_objForm.PointToClient(pt);
            m_objListView.Location = pt;
            m_objListView.Visible = true;
            m_objListView.BringToFront();
            m_objListView.Focus();
        }

        private void clsDoseTypeTextBox_Enter(object sender, EventArgs e)
        {
            m_mthShowListView();
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            m_mthRefreshText();
        }
        private void m_objListView_Leave(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                m_objForm.Visible = false;
                m_mthRefreshText();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                m_mthShowListView();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                m_objForm.Visible = false;
            }
        }
        private void m_objListView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Escape))
            {
                m_objListView_Leave(sender, e);
            }
        }
        private void m_mthRefreshText()
        {
            string strText = "";
            for (int i = 0; i < m_objListView.Items.Count; i++)
            {
                if (m_objListView.Items[i].Checked)
                {
                    if (strText.Length > 0) strText += ",";
                    strText += m_objListView.Items[i].Text.Trim();
                }
            }
            this.Text = strText;
        }
        #endregion
    }
    #endregion

    #region ҽ�����ʹ���
    /// <summary>
    /// ҽ�����ʹ���
    /// </summary>
    public class clsOrderDicCates
    {
        private static clsBIHOrderCate[] s_arrCate = null;
        //private clsBIHOrderService objObject;
        public clsOrderDicCates()
        {
            //objObject = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));			
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderCate(out s_arrCate);
            if (ret <= 0) s_arrCate = null;
        }

        /// <summary>
        /// ���ڼ�����ͱ��ٴζ�ȡʱ�Ƿ�����˸���
        /// </summary>
        public static bool Cate_tag = false;
        /// <summary>
        /// ���»�ȡҽ������Vo
        /// </summary>
        private static void s_initCate()
        {
            long ret = 1;
            clsBIHOrderCate[] s_arrCate_temp = null;
            if (!Cate_tag || s_arrCate == null)
            {
                //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
                ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderCate(out s_arrCate_temp);
                if (ret > 0 && s_arrCate != null && s_arrCate.Length > 0 && s_arrCate.Length == s_arrCate_temp.Length)
                {
                    for (int i = 0; i < s_arrCate.Length; i++)
                    {
                        if (s_arrCate[i].m_intISATTACH_INT == s_arrCate_temp[i].m_intISATTACH_INT
                       && s_arrCate[i].m_strCLASSNAME_VCHR.Equals(s_arrCate_temp[i].m_strCLASSNAME_VCHR)
                       && s_arrCate[i].m_strDes.Equals(s_arrCate_temp[i].m_strDes)
                       && s_arrCate[i].m_strDLLNAME_VCHR.Equals(s_arrCate_temp[i].m_strDLLNAME_VCHR)
                       && s_arrCate[i].m_strName.Equals(s_arrCate_temp[i].m_strName)
                       && s_arrCate[i].m_strOPRADD_VCHR.Equals(s_arrCate_temp[i].m_strOPRADD_VCHR)
                       && s_arrCate[i].m_strOPRUPD_VCHR.Equals(s_arrCate_temp[i].m_strOPRUPD_VCHR)
                       && s_arrCate[i].m_strOrderCateID.Equals(s_arrCate_temp[i].m_strOrderCateID)
                       && s_arrCate[i].m_strSourceTable.Equals(s_arrCate_temp[i].m_strSourceTable)
                       && s_arrCate[i].m_strTABLEPK_VCHR.Equals(s_arrCate_temp[i].m_strTABLEPK_VCHR))
                        {

                            Cate_tag = true;
                        }
                        else
                        {
                            Cate_tag = false;
                            break;
                        }

                    }
                }

                s_arrCate = s_arrCate_temp;

            }
            /*<------------------------*/

            if (ret <= 0) s_arrCate = null;
        }
        /// <summary>
        /// ��ȡ�Ƿ�����и��ӵ���	���ݸ��ӵ�����ˮ��
        /// </summary>
        /// <param name="strCateID">���ӵ�����ˮ��</param>
        /// <returns></returns>
        public static bool IsMedOrder(string strCateID)
        {
            s_initCate();//�������»�ȡ����
            for (int i1 = 0; i1 < s_arrCate.Length; i1++)
            {
                if (s_arrCate[i1].m_strOrderCateID.ToString().Trim() == strCateID.Trim() && s_arrCate[i1].m_intISATTACH_INT == 1)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ��ȡ���ӵ���Vo����	���ݸ��ӵ�����ˮ��
        /// </summary>
        /// <param name="strCateID">���ӵ�����ˮ��</param>
        /// <returns></returns>
        public static clsBIHOrderCate m_objGetCate(string strCateID)
        {
            s_initCate();
            if (s_arrCate == null) return null;

            for (int i = 0; i < s_arrCate.Length; i++)
            {
                if (s_arrCate[i].m_strOrderCateID == strCateID.Trim())
                {
                    return s_arrCate[i];
                }
            }
            return null;
        }
    }
    #endregion
    #region Ƥ�Ա༭��
    /// <summary>
    /// ����Ƥ�Ա༭	{��Ϊ���Ӵ��ڵ�����}
    /// </summary>
    public class clsFeelEdit
    {
        public clsFeelEdit()
        { }
        #region ���
        /// <summary>
        /// ҽ��ID
        /// </summary>
        public string m_strOrderID = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string m_strPatientName = "";
        /// <summary>
        /// ҽ������
        /// </summary>
        public string m_strOrderName = "";
        #endregion
        #region ����
        /// <summary>
        /// �Ƴ�״̬	{0=ִ�д����û�в����˳�	1=ִ�гɹ��Ƴ�}
        /// </summary>
        public int m_intExitState = 0;
        /// <summary>
        /// Ƥ�Խ��	{���ԡ�����}
        /// </summary>
        public string m_strFeelResult = "";
        /// <summary>
        /// Ƥ�Խ��	{1=���ԡ�2=����}
        /// </summary>
        public int m_intFeelResult = 0;
        #endregion
    }
    #endregion
}
