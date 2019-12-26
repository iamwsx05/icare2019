using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ���Ҳ����ؼ�
    /// </summary>
    public partial class ctlFindTextBox : System.Windows.Forms.TextBox
    {
        private frmListView m_frmListView;

        public ctlFindTextBox()
            : base()
        {
        }

        private void m_lvwList_DoubleClick(object sender, System.EventArgs e)
        {
            m_frmListView.Close();
            if (m_frmListView.m_objListView.SelectedItems.Count > 0)
            {
                if (m_evtSelectItem != null)
                {
                    m_evtSelectItem(this, m_frmListView.m_objListView.SelectedItems[0]);
                }
            }
            //m_mthHideListView();
        }

        private void m_lvwList_Leave(object sender, System.EventArgs e)
        {
            m_mthHideListView();
        }

        private void m_lvwList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_lvwList_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                m_frmListView.Close();
                this.Focus();
                this.SelectAll();
            }
        }


        private void m_mthShowListView()
        {

            Point pt = this.Location;
            pt = this.Parent.PointToScreen(pt);
            if (pt.Y < 700 - m_frmListView.Height)
            {
                pt = new Point(pt.X, pt.Y + this.Height);
            }
            else
            {
                pt = new Point(pt.X, pt.Y - m_frmListView.Height);
            }

            //m_frmListView.Width=this.Width;
            m_frmListView.Location = pt;
            m_frmListView.Show();

            m_frmListView.m_objListView.Focus();
            if (m_frmListView.m_objListView.Items.Count > 0)
            {
                m_frmListView.m_objListView.Items[0].Focused = true;
                m_frmListView.m_objListView.Items[0].Selected = true;
            }
        }
        private void m_mthHideListView()
        {
            m_frmListView.m_objListView.Items.Clear();
            m_frmListView.Close();
        }
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (this.ReadOnly) return;

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (m_evtFindItem == null) return;		//û�д���m_evtFindItem�¼�.

                m_frmListView = new frmListView();

                m_mthInit();

                m_frmListView.m_objListView.Items.Clear();

                m_evtFindItem(this, this.Text.Trim(), m_frmListView.m_objListView);
                if (m_frmListView.m_objListView.Items.Count <= 0)
                {
                    this.SelectAll();
                }
                else if (m_frmListView.m_objListView.Items.Count == 1)
                {
                    if (m_evtSelectItem != null) m_evtSelectItem(this, m_frmListView.m_objListView.Items[0]);
                }
                else
                {
                    m_mthShowListView();
                }
            }
        }



        //private bool m_blnIsInit=false;
        private void m_mthInit()
        {
            //if(m_blnIsInit) return;

            if (m_evtInitListView != null)
            {
                m_evtInitListView(m_frmListView.m_objListView);
                m_frmListView.m_objListView.DoubleClick += new EventHandler(m_lvwList_DoubleClick);
                m_frmListView.m_objListView.Leave += new EventHandler(m_lvwList_Leave);
                m_frmListView.m_objListView.KeyDown += new KeyEventHandler(m_lvwList_KeyDown);

                m_frmListView.m_objListView.Location = new Point(0, 0);
                m_frmListView.Size = new Size(m_frmListView.m_objListView.Size.Width, m_frmListView.m_objListView.Height);
            }

            //m_blnIsInit=true;
        }

        public event EventHandler_OnFindItem m_evtFindItem;
        public event EventHandler_OnSelectItem m_evtSelectItem;
        public event EventHandler_InitListView m_evtInitListView;


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.SelectAll();
        }

    }

    public delegate void EventHandler_InitListView(ListView lvwList);
    public delegate void EventHandler_OnFindItem(object sender, string strFindCode, ListView lvwList);
    public delegate void EventHandler_OnSelectItem(object sender, ListViewItem lviSelected);
}
