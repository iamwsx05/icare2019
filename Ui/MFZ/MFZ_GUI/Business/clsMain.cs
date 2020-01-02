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
        private bool IsAsc = false; //�Ƿ�Ϊ����
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
                //����
                strColTxt = strColTxt.Replace(" ��", "");
                strColTxt = strColTxt.Replace(" ��", "");
                objListView.Columns[i].Text = strColTxt;
            }

            col = column;
            this.IsAsc = IsAsc;
            strColTxt = objListView.Columns[col].Text;
            if (IsAsc == true)//���������
                objListView.Columns[col].Text = strColTxt + " ��";
            else
                objListView.Columns[col].Text = strColTxt + " ��";
        }
        //�����ּ�ͷ
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
 
                        if(i>0)//strA����strB
                        {
                            if(IsAsc==true)//���������
                                i=1;
                            else
                                i=-1;
                        }
                        if(i<0)//strAС��strB
                        {
                            if(IsAsc==true)//���������
                                i=-1;
                            else
                                i=1;
                        }
                        else //���
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
