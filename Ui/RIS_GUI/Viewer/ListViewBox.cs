using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace com.digitalwave.iCare.gui.RIS
{
    #region ListViewBox
    public class ListViewBox : UserControl
    {
        #region Fields
        // Fields
        private bool _IsEnterShow;
        private Color backColor;
        private IContainer components;
        private ListViewItem currentItem;
        private Color foreColor;
        private DataTable GetDataTable;
        private DataTable GetDataTableMain;
        private bool HaveParent;
        private int intHide;
        private int intTxt;
        private int intUpOrDn;
        private int intValuse;
        public EventHandler<ItemClickedEventArgs> ItemClickedEvent;
        private int LeftOrRight;
        private int LsvHeight;
        private bool m_blnIsHideOnTxtChange;
        private DataTable m_dtTemp;
        private ListView m_lsvContent;
        private TextBox m_txtContent;
        private int maxListLength;
        private string ParentName;
        public EventHandler<PressEnterEventArgs> PressEnterEvent;
        private ListViewItem prevItem;
        public EventHandler TextChanged;
        #endregion

        // Methods
        public ListViewBox()
        {
            this.components = null;
            this.GetDataTable = new DataTable();
            this.intUpOrDn = 0;
            this.LeftOrRight = 0;
            this.HaveParent = false;
            this.ParentName = "";
            this._IsEnterShow = true;
            this.m_blnIsHideOnTxtChange = false;
            this.maxListLength = 0x19;
            this.backColor = Color.Blue;
            this.foreColor = Color.White;
            this.InitializeComponent();
        }

        public int FindItemByValues(ListView lvwItem, int findType, int MatchCol, int isCode, string strValues)
        {
            if (isCode == -1 && findType != 2 && findType != 3 && findType != 4)
            {
                return -1;
            }
            for (int i = 0; i < lvwItem.Items.Count; i++)
            {
                if (lvwItem.Items[i].SubItems[isCode].Text.IndexOf(strValues) >= 0)
                {
                    return i;
                }
            }
            for (int i = 0; i < lvwItem.Items.Count; i++)
            {
                if (lvwItem.Items[i].SubItems[MatchCol].Text.ToUpper().IndexOf(strValues.ToUpper()) >= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
            this.m_txtContent = new TextBox();
            this.SuspendLayout();
            this.m_txtContent.Dock = DockStyle.Fill;
            this.m_txtContent.Location = new Point(0, 0);
            this.m_txtContent.Name = "m_txtContent";
            this.m_txtContent.Size = new Size(80, 0x17);
            this.m_txtContent.TabIndex = 1;
            this.m_txtContent.TextChanged += new EventHandler(this.m_txtContent_TextChanged);
            this.m_txtContent.KeyDown += new KeyEventHandler(this.m_txtContent_KeyDown);
            this.m_txtContent.Leave += new EventHandler(this.m_txtContent_Leave);
            this.m_txtContent.Enter += new EventHandler(this.m_txtContent_Enter);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.m_txtContent);
            this.Font = new Font("宋体", 10.5f);
            this.Name = "ListViewBox";
            this.Size = new Size(80, 0x18);
            this.Load += new EventHandler(this.ListBox_Load);
            this.Leave += new EventHandler(this.ListViewBox_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public int IsEngOrNumOrChina(string strVal)
        {
            if (this.IsNumber(strVal))
            {
                return 1;
            }
            byte[] buffer = null;
            try
            {
                buffer = Encoding.GetEncoding("GB2312").GetBytes(strVal);
            }
            catch
            {
            }
            if ((buffer.Length % 2) == 0 && strVal.Length == buffer.Length)
            {
                return 3;
            }
            if ((buffer.Length % 2) != 0)
            {
                return 2;
            }
            if (strVal.Length == buffer.Length)
            {
                return 4;
            }
            return 3;
        }

        public bool IsNumber(string strVal)
        {
            if (strVal == null)
            {
                return false;
            }
            strVal = this.ReplaceTo(strVal);
            if (strVal == "")
            {
                return false;
            }
            char[] chrs = strVal.ToCharArray();
            foreach (char item in chrs)
            {
                if (char.IsNumber(item) == false)
                    return false;
            }
            return true;
        }

        private void ListBox_Load(object sender, EventArgs e)
        {
            this.m_lsvContent = new ListView();
            this.m_lsvContent.FullRowSelect = true;
            this.m_lsvContent.GridLines = true;
            this.m_lsvContent.Name = "m_lsvPart";
            this.m_lsvContent.TabIndex = 0;
            this.m_lsvContent.Visible = false;
            this.m_lsvContent.MultiSelect = false;
            this.m_lsvContent.TabStop = false;
            this.m_lsvContent.View = View.Details;
            this.m_lsvContent.Click += new EventHandler(this.m_lsvContent_Click);
            this.m_lsvContent.Leave += new EventHandler(this.m_lsvContent_Leave);
            this.LostFocus += new EventHandler(this.ListViewBox_LostFocus);
        }

        private void ListViewBox_Leave(object sender, EventArgs e)
        {
            this.m_lsvContent.Hide();
        }

        private void ListViewBox_LostFocus(object sender, EventArgs e)
        {
            this.m_lsvContent.Hide();
        }

        private void m_findData()
        {
            if (this.m_txtContent.Text.Trim() != "" && this.m_lsvContent.Items.Count > 0)
            {
                this.m_mthResetColor();
                string str = this.m_txtContent.Text.Trim();
                for (int i = 0; i < this.m_lsvContent.Items.Count; i++)
                {
                    for (int j = 0; j < this.m_lsvContent.Columns.Count; j++)
                    {
                        if (j != this.isHide)
                        {
                            if (this.m_lsvContent.Items[i].SubItems[i].Text.Contains(str))
                            {
                                this.m_mthSelectItem(i);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public void m_ListViewClick()
        {
            if (this.m_lsvContent.Items.Count == 0)
            {
                this.m_lsvContent.Hide();
                return;
            }
            DataRow row;
            ItemClickedEventArgs args;
            PressEnterEventArgs args2;
            if (this.m_lsvContent.SelectedItems.Count > 0)
            {
                row = (DataRow)this.m_lsvContent.SelectedItems[0].Tag;
                this.Tag = row[this.isValuse].ToString();
                this.m_txtContent.Text = row[this.isTxt].ToString();
                this.m_lsvContent.Visible = false;
                if (this.ItemClickedEvent != null)
                {
                    args = new ItemClickedEventArgs(this.m_txtContent.Text, this.Tag.ToString());
                    this.ItemClickedEvent(this, args);
                }
                else
                {
                    for (int i = 0; i < this.m_lsvContent.Items.Count; i++)
                    {
                        if (this.m_lsvContent.Items[i].Text == this.m_txtContent.Text)
                        {
                            row = (DataRow)this.m_lsvContent.Items[i].Tag;
                            this.Tag = row[this.isValuse].ToString();
                            this.m_txtContent.Text = row[this.isTxt].ToString();
                            this.m_lsvContent.Visible = false;
                            if (this.ItemClickedEvent != null)
                            {
                                args = new ItemClickedEventArgs(this.m_txtContent.Text, base.Tag.ToString());
                                this.ItemClickedEvent(this, args);
                            }
                            return;
                        }
                    }
                    args2 = new PressEnterEventArgs();
                    args2.m_StrText = this.m_txtContent.Text;
                    if (this.PressEnterEvent != null)
                    {
                        this.PressEnterEvent(this, args2);
                    }
                }
            }
        }

        private void m_lsvContent_Click(object sender, EventArgs e)
        {
            this.m_ListViewClick();
        }

        private void m_lsvContent_Leave(object sender, EventArgs e)
        {
            this.m_lsvContent.Hide();
        }

        public void m_mthClear()
        {
            this.m_txtContent.Text = string.Empty;
            this.Tag = null;
            this.m_lsvContent.Hide();
        }

        private void m_mthFillListView(DataTable dtValue)
        {
            Graphics graphics = base.CreateGraphics();
            Font font = this.m_lsvContent.Font;
            SizeF ef = new SizeF();
            ListViewItem item;
            int num = 0;

            this.m_lsvContent.BeginUpdate();
            this.m_lsvContent.Columns.Clear();
            this.m_lsvContent.Clear();

            for (int i = 0; i < dtValue.Columns.Count; i++)
            {
                if (i == this.isHide) continue;
                ef = graphics.MeasureString(dtValue.Columns[i].ColumnName, font);
                this.m_lsvContent.Columns.Add(dtValue.Columns[i].ColumnName, ((int)ef.Width) + 30, 0);
                num += ((int)ef.Width) + 30;
            }
            this.m_lsvContent.Width = num + 30;

            foreach (DataRow dr in dtValue.Rows)
            {
                item = new ListViewItem(dr[0].ToString().Trim());
                for (int j = 1; j < dtValue.Columns.Count; j++)
                {
                    if (j == this.isHide) continue;
                    item.SubItems.Add(dr[j].ToString().Trim());
                }
                item.Tag = dr;
                this.m_lsvContent.Items.Add(item);
            }
            this.m_ShowListView();
            graphics.Dispose();
            item = null;
            this.m_lsvContent.EndUpdate();
            return;

            int num2;

            int num3;
            DataRow row;
            int num4;
            DataRow[] rowArray;
            int num5;
            bool flag;




            num = 0;
            num2 = 0;
            goto Label_00FA;
        Label_004F:
            if (this.isHide == num2)
            {
                goto Label_00F3;
            }
            if (this.HaveParent && dtValue.Columns[num2].ColumnName == this.ParentName)
            {
                goto Label_0099;
            }
            goto Label_00F2;
        Label_0099:
            ef = graphics.MeasureString(dtValue.Columns[num2].ColumnName, font);
            this.m_lsvContent.Columns.Add(dtValue.Columns[num2].ColumnName, ((int)ef.Width) + 30, 0);
            num += ((int)ef.Width) + 30;
        Label_00F2:;
        Label_00F3:
            num2 += 1;
        Label_00FA:
            if (num2 < dtValue.Columns.Count)
            {
                goto Label_004F;
            }
            if (dtValue.Rows.Count <= 0)
            {
                goto Label_013B;
            }
            this.m_lsvContent.Width = num + 30;
            goto Label_0148;
        Label_013B:
            this.m_lsvContent.Width = num;
        Label_0148:
            this.m_ShowListView();
            graphics.Dispose();
            item = null;
            num3 = this.maxListLength;
            if (this.maxListLength != 0)
            {
                goto Label_017C;
            }
            num3 = 0x7fffffff;
        Label_017C:
            if (dtValue.Rows.Count >= num3)
            {
                goto Label_01A3;
            }
            num3 = dtValue.Rows.Count;
        Label_01A3:
            if (this.HaveParent)
            {
                goto Label_0294;
            }
            row = null;
            num2 = 0;
            goto Label_027F;
        Label_01BE:
            row = dtValue.Rows[num2];
            item = new ListViewItem(row[dtValue.Columns[0].ColumnName].ToString().Trim());
            num4 = 1;
            goto Label_0246;
        Label_01FC:
            if (this.isHide == num4)
            {
                goto Label_023F;
            }
            item.SubItems.Add(row[dtValue.Columns[num4].ColumnName].ToString().Trim());
        Label_023F:
            num4 += 1;
        Label_0246:
            if (num4 < dtValue.Columns.Count)
            {
                goto Label_01FC;
            }
            item.Tag = row;
            this.m_lsvContent.Items.Add(item);
            num2 += 1;
        Label_027F:
            if (num2 < num3)
            {
                goto Label_01BE;
            }
            goto Label_04E8;
        Label_0294:
            if (dtValue.Rows.Count <= 0)
            {
                goto Label_04E7;
            }
            row = null;
            num2 = 0;
            goto Label_04D7;
        Label_02BB:
            row = dtValue.Rows[num2];
            if (row[this.ParentName].ToString().Trim().Trim() != "")
            {
                goto Label_04D0;
            }
            item = new ListViewItem(row[dtValue.Columns[0].ColumnName].ToString().Trim());
            num4 = 1;
            goto Label_0376;
        Label_032C:
            if (this.isHide == num4)
            {
                goto Label_036F;
            }
            item.SubItems.Add(row[dtValue.Columns[num4].ColumnName].ToString().Trim());
        Label_036F:
            num4 += 1;
        Label_0376:
            if (num4 < dtValue.Columns.Count)
            {
                goto Label_032C;
            }
            item.Tag = row;
            this.m_lsvContent.Items.Add(item);
            rowArray = dtValue.Select(this.ParentName + "=" + row[dtValue.Columns["ID"].ColumnName].ToString().Trim());
            if (((int)rowArray.Length) <= 0)
            {
                goto Label_04CF;
            }
            num4 = 0;
            goto Label_04BD;
        Label_0402:
            item = new ListViewItem(rowArray[num4][dtValue.Columns[0].ColumnName].ToString().Trim());
            num5 = 1;
            goto Label_0481;
        Label_0434:
            if (this.isHide == num5)
            {
                goto Label_047A;
            }
            item.SubItems.Add(rowArray[num4][dtValue.Columns[num5].ColumnName].ToString().Trim());
        Label_047A:
            num5 += 1;
        Label_0481:
            if (num5 < dtValue.Columns.Count)
            {
                goto Label_0434;
            }
            item.Tag = rowArray[num4];
            this.m_lsvContent.Items.Add(item);
            num4 += 1;
        Label_04BD:
            if (num4 < ((int)rowArray.Length))
            {
                goto Label_0402;
            }
        Label_04CF:;
        Label_04D0:
            num2 += 1;
        Label_04D7:
            if (num2 < num3)
            {
                goto Label_02BB;
            }
        Label_04E7:;
        Label_04E8:
            this.m_lsvContent.EndUpdate();
            return;
        }

        private void m_mthResetColor()
        {
            if (this.currentItem == null)
                return;
            this.currentItem.BackColor = this.m_lsvContent.BackColor;
            this.currentItem.ForeColor = this.m_lsvContent.ForeColor;
        }

        private void m_mthSelectItem(int p_intIndex)
        {
            this.currentItem = this.m_lsvContent.Items[p_intIndex];
            this.currentItem.Selected = true;
            this.currentItem.EnsureVisible();
            this.currentItem.BackColor = this.backColor;
            this.currentItem.ForeColor = this.foreColor;
        }

        private void m_ShowListView()
        {
            Point point;
            if (this.FindForm().Controls.Contains(this.m_lsvContent))
            {
                goto Label_0034;
            }
            base.FindForm().Controls.Add(this.m_lsvContent);
        Label_0034:
            this.m_lsvContent.Height = this.intHeight;
            point = this.m_txtContent.Parent.PointToScreen(this.m_txtContent.Location);
            if (this.isUpOrDn != 0)
            {
                goto Label_00CB;
            }
            if (this.LeftOrRight != 0)
            {
                goto Label_00B2;
            }
            point.Offset(-this.m_lsvContent.Width + this.m_txtContent.Width, this.m_txtContent.Height);
            goto Label_00C8;
        Label_00B2:
            point.Offset(0, this.m_txtContent.Height);
        Label_00C8:
            goto Label_0124;
        Label_00CB:
            if (this.LeftOrRight != 0)
            {
                goto Label_010C;
            }
            point.Offset(-this.m_lsvContent.Width + this.m_txtContent.Width, -this.m_lsvContent.Height);
            goto Label_0123;
        Label_010C:
            point.Offset(0, -this.m_lsvContent.Height);
        Label_0123:;
        Label_0124:
            point = base.FindForm().PointToClient(point);
            this.m_lsvContent.Location = point;
            this.m_lsvContent.Visible = true;
            this.m_lsvContent.BringToFront();
        }

        public void m_txtChange()
        {
            StringBuilder builder;
            string str;
            DataColumn column;
            DataRow[] rowArray;
            DataRow row;
            bool flag;
            IEnumerator enumerator;
            IDisposable disposable;
            DataRow[] rowArray2;
            int num;
            if (this.m_lsvContent != null)
            {
                goto Label_0019;
            }
            goto Label_01EF;
        Label_0019:
            if (this.m_txtContent.Text.Trim() != "")
            {
                goto Label_006E;
            }
            this.m_mthFillListView(this.m_GetDataTable);
            if (this.m_blnIsHideOnTxtChange == false)
            {
                goto Label_0068;
            }
            this.m_lsvContent.Visible = false;
        Label_0068:
            goto Label_01EF;
        Label_006E:
            if (this.m_dtTemp != null)
            {
                goto Label_0093;
            }
            this.m_lsvContent.Hide();
            goto Label_01EF;
        Label_0093:
            this.m_dtTemp.Rows.Clear();
            builder = new StringBuilder();
            str = string.Empty;
            enumerator = this.m_GetDataTable.Columns.GetEnumerator();
        Label_00C3:
            try
            {
                goto Label_013D;
            Label_00C5:
                column = (DataColumn)enumerator.Current;
                if (column.DataType != typeof(string))
                {
                    goto Label_013C;
                }
                str = this.m_txtContent.Text.Replace("'", "''");
                builder.Append("[").Append(column.ColumnName).Append("] like '%").Append(str).Append("%' or");
            Label_013C:;
            Label_013D:
                if (enumerator.MoveNext())
                {
                    goto Label_00C5;
                }
                goto Label_016C;
            }
            finally
            {
            Label_014F:
                disposable = enumerator as IDisposable;
                if ((disposable == null))
                {
                    goto Label_016B;
                }
                disposable.Dispose();
            Label_016B:;
            }
        Label_016C:
            builder.Remove(builder.Length - 3, 3);
            rowArray = this.m_GetDataTable.Select(builder.ToString());
            rowArray2 = rowArray;
            num = 0;
            goto Label_01B5;
        Label_0198:
            row = rowArray2[num];
            this.m_dtTemp.ImportRow(row);
            num += 1;
        Label_01B5:
            if ((num < ((int)rowArray2.Length)))
            {
                goto Label_0198;
            }
            this.m_mthFillListView(this.m_dtTemp);
            if (this.m_blnIsHideOnTxtChange == false)
            {
                goto Label_01EE;
            }
            this.m_lsvContent.Visible = false;
        Label_01EE:;
        Label_01EF:
            return;
        }

        private void m_txtContent_Enter(object sender, EventArgs e)
        {
            if (this.IsEnterShow)
            {
                this.m_mthFillListView(this.m_GetDataTable);
            }
        }

        private void m_txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            int index = -1;
            PressEnterEventArgs args;
            if (e.KeyCode == Keys.Up)
            {
                if (this.m_lsvContent.Visible == false)                    return;
                for (int i = 0; i < this.m_lsvContent.Items.Count; i++)
                {
                    if(this.m_lsvContent.Items[i].Selected)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > 0) index = index - 1;
                this.m_UpDown(index, e);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (this.m_lsvContent.Visible == false) return;
                for (int i = 0; i < this.m_lsvContent.Items.Count; i++)
                {
                    if (this.m_lsvContent.Items[i].Selected)
                    {
                        index = i;
                        break;
                    }
                }
                if (index == this.m_lsvContent.Items.Count - 1)
                    index = -1;
                else
                    index = index + 1;
                this.m_UpDown(index, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (this.m_txtContent.Text.Trim().Length > 0 && this.PressEnterEvent == null)
                {
                    args = new PressEnterEventArgs();
                    args.m_StrText = this.m_txtContent.Text;
                    this.PressEnterEvent(this, args);
                    this.m_lsvContent.Hide();
                    SendKeys.Send("{Tab}");
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                this.m_lsvContent.Visible = true;
                this.m_mthFillListView(this.m_GetDataTable);
            }
            return;

             
            int num;
            int num2;
            bool flag;
            if (e.KeyCode != Keys.Enter)
            {
                goto Label_00CC;
            }
            if (this.m_lsvContent.Visible)
            {
                goto Label_0039;
            }
            this.m_mthFillListView(this.m_GetDataTable);
            goto Label_00C6;
        Label_0039:
            if (this.m_lsvContent.Items.Count != 0)
            {
                goto Label_005C;
            }
            this.m_ListViewClick();
            goto Label_00AE;
        Label_005C:
            if (this.m_txtContent.Text.Trim().Length > 0 && this.PressEnterEvent == null)
            {
                goto Label_00AD;
            }
            args = new PressEnterEventArgs();
            args.m_StrText = this.m_txtContent.Text;
            this.PressEnterEvent(this, args);
        Label_00AD:;
        Label_00AE:
            this.m_lsvContent.Hide();
            SendKeys.Send("{Tab}");
        Label_00C6:
            goto Label_015A;
        Label_00CC:
            if (e.KeyCode != Keys.Down || e.KeyCode == Keys.Up)
            {
                goto Label_013B;
            }
            num = -1;
            num2 = 0;
            goto Label_0118;
        Label_00F1:
            if (this.m_lsvContent.Items[num2].Selected == false)
            {
                goto Label_0113;
            }
            num = num2;
        Label_0113:
            num2 += 1;
        Label_0118:
            if (num2 < this.m_lsvContent.Items.Count)
            {
                goto Label_00F1;
            }
            this.m_UpDown(num, e);
            goto Label_015A;
        Label_013B:
            if (e.KeyCode == Keys.Escape)
            {
                goto Label_015A;
            }
            this.m_lsvContent.Hide();
        Label_015A:
            return;
        }

        private void m_txtContent_Leave(object sender, EventArgs e)
        {
            this.m_lsvContent.Hide();
        }

        private void m_txtContent_TextChanged(object sender, EventArgs e)
        {
            this.m_txtChange();
            if (this.TextChanged != null)
            {
                this.TextChanged(this, null);
            }
        }

        private void m_UpDown(int index, KeyEventArgs e)
        {
            if (this.m_lsvContent.Items.Count <= 0)
            {
                return;
            }
            this.m_mthResetColor();
            //if (index != this.m_lsvContent.Items.Count - 1 && e.KeyCode == Keys.Down)
            //{
            //    this.m_lsvContent.Items[index].Selected = false;
            //    this.prevItem = this.m_lsvContent.Items[index];
            //    this.m_lsvContent.Items[0].Selected = true;
            //    this.m_lsvContent.Items[0].EnsureVisible();
            //}
            //else
            //{
            //    this.m_lsvContent.Items[0].Selected = false;
            //    this.prevItem = this.m_lsvContent.Items[0];
            //    this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].Selected = true;
            //    this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].EnsureVisible();
            //}
            if ((index <= 0 || index > this.m_lsvContent.Items.Count - 1) && e.KeyCode == Keys.Down)
            {
                index = 0;
                this.m_lsvContent.Items[0].Selected = true;
                this.m_lsvContent.Items[0].EnsureVisible();
                this.currentItem = this.m_lsvContent.SelectedItems[0];
            }
            else if (index >= 0 && index < this.m_lsvContent.Items.Count - 2 && e.KeyCode == Keys.Down)
            {
                this.m_lsvContent.Items[index].Selected = false;
                this.m_lsvContent.Items[index + 1].Selected = true;
                this.m_lsvContent.Items[index + 1].EnsureVisible();
                this.prevItem = this.m_lsvContent.Items[index];
            }
            else if (index < this.m_lsvContent.Items.Count - 1 && e.KeyCode == Keys.Down)
            {
                this.m_lsvContent.Items[0].Selected = false;
                this.prevItem = this.m_lsvContent.Items[0];
                this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].Selected = true;
                this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].EnsureVisible();
            }
            else if (index > 0 && index < this.m_lsvContent.Items.Count - 1 && e.KeyCode == Keys.Up)
            {
                this.m_lsvContent.Items[index].Selected = false;
                this.m_lsvContent.Items[index - 1].Selected = true;
                this.m_lsvContent.Items[index - 1].EnsureVisible();
                this.prevItem = this.m_lsvContent.Items[index];
            }
            else if (index <= 0 && e.KeyCode == Keys.Up)
            {
                this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].Selected = true;
                this.m_lsvContent.Items[this.m_lsvContent.Items.Count - 1].EnsureVisible();
                this.currentItem = this.m_lsvContent.SelectedItems[0];

            }
            if (this.currentItem == null)
                return;
            this.currentItem.BackColor = this.backColor;
            this.currentItem.ForeColor = this.foreColor;
            if (this.prevItem != null)
            {
                this.prevItem.BackColor = this.m_lsvContent.BackColor;
                this.prevItem.ForeColor = this.m_lsvContent.ForeColor;
            }
        }

        public string ReplaceTo(string strValues)
        {
            string str;
            strValues = strValues.Trim();
            strValues = strValues.Replace(" ", "");
            strValues = strValues.Replace("'", "");
            str = strValues;
            return str;
        }

        #region 属性
        // Properties
        [Browsable(true), Description("设置ListView的高度")]
        public int intHeight
        {
            get
            {
                return this.LsvHeight;
            }
            set
            {
                this.LsvHeight = value;
            }
        }

        [Browsable(true), Description("是否获取蕉点时就显示下拉列表")]
        public bool IsEnterShow
        {
            get
            {
                return this._IsEnterShow;
            }
            set
            {
                this._IsEnterShow = value;
            }
        }

        [Browsable(true), Description("设置隐藏的列号")]
        public int isHide
        {
            get
            {
                return this.intHide;
            }
            set
            {
                this.intHide = value;
            }
        }

        [Browsable(true), Description("当赋值时, 是否隐藏下拉列表")]
        public bool IsTextChangeHideListView
        {
            get
            {
                return this.m_blnIsHideOnTxtChange;
            }
            set
            {
                this.m_blnIsHideOnTxtChange = value;
            }
        }

        [Description("设置要把表为的第几列的值符值给TXETBOX的TXT"), Browsable(true)]
        public int isTxt
        {
            get
            {
                return this.intTxt;
            }
            set
            {
                this.intTxt = value;
            }
        }

        [Browsable(true), Description("在TXETBOX的上面或下面显示选择列表 0-下面，1-上面")]
        public int isUpOrDn
        {
            get
            {
                return this.intUpOrDn;
            }
            set
            {
                this.intUpOrDn = value;
            }
        }

        [Description("设置要把表为的第几列的值符值给TXETBOX的Valuse"), Browsable(true)]
        public int isValuse
        {
            get
            {
                return this.intValuse;
            }
            set
            {
                this.intValuse = value;
            }
        }

        [Description("获取数据"), Browsable(false)]
        public DataTable m_GetDataTable
        {
            get
            {
                return this.GetDataTable;
            }
            set
            {
                this.GetDataTable = value;
                this.m_dtTemp = value.Copy();
                this.m_dtTemp.Rows.Clear();
            }
        }

        [Description("下拉框最大显示数"), Browsable(true)]
        public int m_IntMaxListLength
        {
            get
            {
                return this.maxListLength;
            }
            set
            {
                this.maxListLength = value;
            }
        }

        [Description("是否有父节点"), Browsable(true)]
        public bool m_IsHaveParent
        {
            get
            {
                return this.HaveParent;
            }
            set
            {
                this.HaveParent = value;
                return;
            }
        }

        [Description("父字段"), Browsable(false)]
        public string m_strParentName
        {
            get
            {
                return this.ParentName;
            }
            set
            {
                this.ParentName = value;
            }
        }

        [Browsable(true), Description("设置ListView当前选择项的背景色")]
        public Color SelectedItemBackColor
        {
            get
            {
                return this.backColor; ;
            }
            set
            {
                this.backColor = value;
            }
        }

        [Description("设置ListView当前选择项的前景色"), Browsable(true)]
        public Color SelectedItemForeColor
        {
            get
            {
                return this.foreColor;
            }
            set
            {
                this.foreColor = value;
            }
        }

        [Browsable(false), Description("设置获取TEXT值")]
        public string txtValuse
        {
            get
            {
                return this.m_txtContent.Text;
            }
            set
            {
                this.m_txtContent.Text = value;
            }
        }

        [Description("ListView与TXETBOX的对齐方式0-左对齐，1-右对齐"), Browsable(true)]
        public int VsLeftOrRight
        {
            get
            {
                return this.LeftOrRight;
            }
            set
            {
                this.LeftOrRight = value;
            }
        }

        #endregion
    }
    #endregion

    #region ItemClickedEventArgs
    public class ItemClickedEventArgs : EventArgs
    {
        // Fields
        private string m_strText;
        private string m_strValue;

        // Methods
        public ItemClickedEventArgs()
        {
            return;
        }

        public ItemClickedEventArgs(string p_strText, string p_strValue)
        {
            this.m_strText = p_strText;
            this.m_strValue = p_strValue;
        }

        // Properties
        public string m_StrText
        {
            get
            {
                return this.m_strText;
            }
            set
            {
                this.m_strText = value;
            }
        }

        public string m_StrValue
        {
            get
            {
                return this.m_strValue;
            }
            set
            {
                this.m_strValue = value;
            }
        }
    }
    #endregion

    #region PressEnterEventArgs
    public class PressEnterEventArgs : EventArgs
    {
        // Fields
        private string m_strText;

        // Methods
        public PressEnterEventArgs()
        {
            return;
        }

        // Properties
        public string m_StrText
        {
            get
            {
                return this.m_strText;
            }
            set
            {
                this.m_strText = value;
            }
        }
    }
    #endregion
}
