using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace com.digitalwave.iCare.gui.MFZ
{
    class clsMain
    {
    }

    public class ListViewItemComparer : IComparer
    {
        private int col;
        private bool IsAsc = false; //是否为升序
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column, bool IsAsc, ListView objListView)
        {
            string strColTxt = "";
            for (int i = 0; i < objListView.Columns.Count; i++)
            {
                strColTxt = objListView.Columns[i].Text;
                //↓↑
                strColTxt = strColTxt.Replace(" ↑", "");
                strColTxt = strColTxt.Replace(" ↓", "");
                objListView.Columns[i].Text = strColTxt;
            }

            col = column;
            this.IsAsc = IsAsc;
            strColTxt = objListView.Columns[col].Text;
            if (IsAsc == true)//如果是升序
                objListView.Columns[col].Text = strColTxt + " ↑";
            else
                objListView.Columns[col].Text = strColTxt + " ↓";
        }
        //不出现箭头
        public ListViewItemComparer(int column, bool IsAsc)
        {
            col = column;
            this.IsAsc = IsAsc;
        }

        // created by Sam
        // modify by Cameron Wong on Aug 13, 2004
        // there is a bug in the origional version!
        public int Compare(object x, object y)
        {
            int i = 0;
            i = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);

            // the following line is added by Cameron Wong on Aug 13, 2004
            if (!IsAsc) i = -i;

            /* commented by Cameron Wong on Aug 13, 2004
 
                        if(i>0)//strA大于strB
                        {
                            if(IsAsc==true)//如果是升序
                                i=1;
                            else
                                i=-1;
                        }
                        if(i<0)//strA小于strB
                        {
                            if(IsAsc==true)//如果是升序
                                i=-1;
                            else
                                i=1;
                        }
                        else //相等
                        {
                            i=0;
                        }
            */
            return i;
        }
    }

    public  class clsAppConfig 
    {
        private int m_areaId;
        private string m_strCalledPatientStyle;
        private string m_strPreparePatientStyle;
        private const string filePath = @"LoginFile.xml";

        public int AreaId
        {
            get { return m_areaId; }
            set { m_areaId = value; }
        }
        public string CalledPatientStyle
        {
            get { return m_strCalledPatientStyle; }
            set { m_strCalledPatientStyle = value; }
        }
        public string PreparePatientStyle
        {
            get { return m_strPreparePatientStyle; }
            set { m_strPreparePatientStyle = value; }
        }

        public void Load() 
        {
            
        }

   
    }
}
