using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// 模块编号:
    /// 模块功能:对ListView的封装
    /// 作       者:huafeng.xiao
    /// 编写时间:2008-9-10
    /// </summary>
    public class ListViewHelper
    {
        /**/
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListViewHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            // 检查点击的列是不是现在的排序列.
            if (e.Column == (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn)
            {
                // 重新设置此列的排序方法.
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
                // 设置排序列，默认为正向排序
                (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
                (lv.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
            }
            // 用新的排序方法对ListView排序
            ((ListView)sender).Sort();
        }
    }
    /**/
    /// <summary>
    /// 继承自IComparer
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /**/
        /// <summary>
        /// 指定按照哪个列排序
        /// </summary>
        private int ColumnToSort;
        /**/
        /// <summary>
        /// 指定排序的方式
        /// </summary>
        private SortOrder OrderOfSort;
        /**/
        /// <summary>
        /// 声明CaseInsensitiveComparer类对象，
        /// 参见ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html /frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;
        /**/
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListViewColumnSorter()
        {
            // 默认按第一列排序
            ColumnToSort = 0;
            // 排序方式为不排序
            OrderOfSort = SortOrder.None;
            // 初始化CaseInsensitiveComparer类对象
            ObjectCompare = new CaseInsensitiveComparer();
        }
        /**/
        /// <summary>
        /// 重写IComparer接口.
        /// </summary>
        /// <param name="x">要比较的第一个对象</param>
        /// <param name="y">要比较的第二个对象</param>
        /// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            // 将比较对象转换为ListViewItem对象
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;
            // 比较
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
          
            // 根据上面的比较结果返回正确的比较结果
            if (OrderOfSort == SortOrder.Ascending)
            {
                // 因为是正序排序，所以直接返回结果
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // 如果是反序排序，所以要取负值再返回
                return (-compareResult);
            }
            else
            {
                // 如果相等返回0
                return 0;
            }
        }
        /**/
        /// <summary>
        /// 获取或设置按照哪一列排序.
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
        /// 获取或设置排序方式.
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