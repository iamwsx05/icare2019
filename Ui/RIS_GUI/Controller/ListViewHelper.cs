using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// ģ����:
    /// ģ�鹦��:��ListView�ķ�װ
    /// ��       ��:huafeng.xiao
    /// ��дʱ��:2008-9-10
    /// </summary>
    public class ListViewHelper
    {
        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        public ListViewHelper()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            // ����������ǲ������ڵ�������.
            if (e.Column == (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn)
            {
                // �������ô��е����򷽷�.
                if ((lv.ListViewItemSorter as ListViewColumnSorter).Order == SortOrder.Ascending)
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Descending;
                }
                else
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
                }
            }
            else
            {
                // ���������У�Ĭ��Ϊ��������
                (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
                (lv.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
            }
            // ���µ����򷽷���ListView����
            ((ListView)sender).Sort();
        }
    }
    /**/
    /// <summary>
    /// �̳���IComparer
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /**/
        /// <summary>
        /// ָ�������ĸ�������
        /// </summary>
        private int ColumnToSort;
        /**/
        /// <summary>
        /// ָ������ķ�ʽ
        /// </summary>
        private SortOrder OrderOfSort;
        /**/
        /// <summary>
        /// ����CaseInsensitiveComparer�����
        /// �μ�ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html /frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;
        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        public ListViewColumnSorter()
        {
            // Ĭ�ϰ���һ������
            ColumnToSort = 0;
            // ����ʽΪ������
            OrderOfSort = SortOrder.None;
            // ��ʼ��CaseInsensitiveComparer�����
            ObjectCompare = new CaseInsensitiveComparer();
        }
        /**/
        /// <summary>
        /// ��дIComparer�ӿ�.
        /// </summary>
        /// <param name="x">Ҫ�Ƚϵĵ�һ������</param>
        /// <param name="y">Ҫ�Ƚϵĵڶ�������</param>
        /// <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            // ���Ƚ϶���ת��ΪListViewItem����
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;
            // �Ƚ�
            try
            {
                System.DateTime objDateTimeX = DateTime.Parse(listviewX.SubItems[ColumnToSort].Text);
                System.DateTime objDateTimeY = DateTime.Parse(listviewY.SubItems[ColumnToSort].Text);
                compareResult = ObjectCompare.Compare(objDateTimeX, objDateTimeY);
            }
            catch
            {
                try
                {
                    System.Int32 objintX = Int32.Parse(listviewX.SubItems[ColumnToSort].Text);
                    System.Int32 objintY = Int32.Parse(listviewY.SubItems[ColumnToSort].Text);
                    compareResult=ObjectCompare.Compare(objintX, objintY);
                }
                catch
                {
                    compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
                }
            }
          
            // ��������ıȽϽ��������ȷ�ıȽϽ��
            if (OrderOfSort == SortOrder.Ascending)
            {
                // ��Ϊ��������������ֱ�ӷ��ؽ��
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // ����Ƿ�����������Ҫȡ��ֵ�ٷ���
                return (-compareResult);
            }
            else
            {
                // �����ȷ���0
                return 0;
            }
        }
        /**/
        /// <summary>
        /// ��ȡ�����ð�����һ������.
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }
        /**/
        /// <summary>
        /// ��ȡ����������ʽ.
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}