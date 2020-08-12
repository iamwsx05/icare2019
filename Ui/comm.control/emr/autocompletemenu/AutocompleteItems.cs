using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    /// <summary>
    /// This autocomplete item appears after dot
    /// </summary>
    public class MethodAutocompleteItem : AutocompleteItem
    {
        string firstPart;
        string lowercaseText;

        public MethodAutocompleteItem(string text)
            : base(text)
        {
            lowercaseText = Text.ToLower();
        }

        public override CompareResult Compare(string fragmentText)
        {
            int i = fragmentText.LastIndexOf('.');
            if (i < 0)
                return CompareResult.Hidden;
            string lastPart = fragmentText.Substring(i + 1);
            firstPart = fragmentText.Substring(0, i);

            if (lastPart == "") return CompareResult.Visible;
            if (Text.StartsWith(lastPart, StringComparison.InvariantCultureIgnoreCase))
                return CompareResult.VisibleAndSelected;
            if (lowercaseText.Contains(lastPart.ToLower()))
                return CompareResult.Visible;

            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            return firstPart + "." + Text;
        }
    }

    /// <summary>
    /// Autocomplete item for code snippets
    /// </summary>
    /// <remarks>Snippet can contain special char ^ for caret position.</remarks>
    public class SnippetAutocompleteItem : AutocompleteItem
    {
        public SnippetAutocompleteItem(string snippet)
        {
            Text = snippet.Replace("\r", "");
            ToolTipTitle = "Code snippet:";
            ToolTipText = Text;
        }

        public override string ToString()
        {
            return MenuText ?? Text.Replace("\n", " ").Replace("^", "");
        }

        public override string GetTextForReplace()
        {
            return Text;
        }

        public override void OnSelected(SelectedEventArgs e)
        {
            var tb = Parent.TargetControlWrapper;
            //
            if (!Text.Contains("^"))
                return;
            var text = tb.Text;
            for (int i = Parent.Fragment.Start; i < text.Length; i++)
                if (text[i] == '^')
                {
                    tb.SelectionStart = i;
                    tb.SelectionLength = 1;
                    tb.SelectedText = "";
                    return;
                }
        }

        /// <summary>
        /// Compares fragment text with this item
        /// </summary>
        public override CompareResult Compare(string fragmentText)
        {
            if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) &&
                   Text != fragmentText)
                return CompareResult.Visible;

            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// This class finds items by substring
    /// </summary>
    public class SubstringAutocompleteItem : AutocompleteItem
    {
        protected readonly string lowercaseText;
        protected readonly bool ignoreCase;

        public SubstringAutocompleteItem(string text, bool ignoreCase)
            : base(text)
        {
            this.ignoreCase = ignoreCase;
            if(ignoreCase)
                lowercaseText = text.ToLower();
        }

        public override CompareResult Compare(string fragmentText)
        {
            if(ignoreCase)
            {
                if (lowercaseText.Contains(fragmentText.ToLower()))
                    return CompareResult.Visible;
            }
            else
            {
                if (Text.Contains(fragmentText))
                    return CompareResult.Visible;
            }

            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// This item draws multicolumn menu
    /// </summary>
    public class MulticolumnAutocompleteItem : SubstringAutocompleteItem
    {
        public bool CompareBySubstring { get; set; }
        public string[] MenuTextByColumns { get; set; }
        public int[] ColumnWidth { get; set; }

        public MulticolumnAutocompleteItem(string[] menuTextByColumns, string insertingText, bool compareBySubstring, bool ignoreCase)
            : base(insertingText, ignoreCase)
        {
            this.CompareBySubstring = compareBySubstring;
            this.MenuTextByColumns = menuTextByColumns;
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (CompareBySubstring)
                return base.Compare(fragmentText);

            if (ignoreCase)
            {
                if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                    return CompareResult.VisibleAndSelected;
            }
            else
                if (Text.StartsWith(fragmentText))
                    return CompareResult.VisibleAndSelected;

            return CompareResult.Hidden;
        }

        public override void OnPaint(PaintItemEventArgs e)
        {
            if (ColumnWidth != null && ColumnWidth.Length != MenuTextByColumns.Length)
                throw new Exception("ColumnWidth.Length != MenuTextByColumns.Length");

            int[] columnWidth = ColumnWidth;
            if (columnWidth == null)
            {
                columnWidth = new int[MenuTextByColumns.Length];
                float step = e.TextRect.Width / MenuTextByColumns.Length;
                for (int i = 0; i < MenuTextByColumns.Length; i++)
                    columnWidth[i] = (int)step;
            }

            //draw columns
            Pen pen = Pens.Silver;
            Brush brush = Brushes.Black;
            float x = e.TextRect.X;
            e.StringFormat.FormatFlags = e.StringFormat.FormatFlags | StringFormatFlags.NoWrap;

            for (int i = 0; i < MenuTextByColumns.Length; i++)
            {
                var width = columnWidth[i];
                var rect = new RectangleF(x, e.TextRect.Top, width, e.TextRect.Height);
                e.Graphics.DrawLine(pen, new PointF(x, e.TextRect.Top), new PointF(x, e.TextRect.Bottom));
                e.Graphics.DrawString(MenuTextByColumns[i], e.Font, brush, rect, e.StringFormat);
                x += width;
            }
        }
    }

    /// <summary>
    /// 字典选项
    /// </summary>
    public class DictAutocompleteItem : SubstringAutocompleteItem
    {
        public string ItemKey { get; set; }
        public int KeyWidth { get; set; }

        public string ItemValue { get; set; }
        public int ValueWidth { get; set; }

        public string[] FilterColumns { get; set; }

        public DictAutocompleteItem(string itemKey,string itemValue, string insertingText,
            bool ignoreCase)
            : base(insertingText, ignoreCase)
        {
            ItemKey = itemKey;
            ItemValue = itemValue;
            if (FilterColumns == null)
            {
                FilterColumns = new string[0];
            }
        }

        public override CompareResult Compare(string fragmentText)
        {
            double similarity = 0;
            if (ignoreCase)
            {
                if (ItemKey.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.SortIndex = LevenshteinDistance(ItemKey, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length + 1);
                    return CompareResult.VisibleAndSelected;
                }
                if (ItemValue.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.SortIndex = LevenshteinDistance(ItemValue, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length);
                    return CompareResult.VisibleAndSelected;
                }

                if (FilterColumns != null)
                {
                    int i = 1;
                    foreach (string strCol in FilterColumns)
                    {
                        if (strCol.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.SortIndex = LevenshteinDistance(strCol, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length - i);
                            return CompareResult.VisibleAndSelected;
                        }
                        i++;
                    }
                }
            }
            else
            {
                if (ItemKey.Contains(fragmentText))
                {
                    this.SortIndex = LevenshteinDistance(ItemKey, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length + 1);
                    return CompareResult.VisibleAndSelected;
                }
                if (ItemValue.Contains(fragmentText))
                {
                    this.SortIndex = LevenshteinDistance(ItemValue, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length);
                    return CompareResult.VisibleAndSelected;
                }

                if (FilterColumns != null)
                {
                    int i = 1;
                    foreach (string strCol in FilterColumns)
                    {
                        if (strCol.ToLower().Contains(fragmentText))
                        {
                            this.SortIndex = LevenshteinDistance(strCol, fragmentText, out similarity, ignoreCase) * Math.Pow(10, FilterColumns.Length - i);
                            return CompareResult.VisibleAndSelected;
                        }
                        i++;
                    }
                }
            }
            return CompareResult.Hidden;
        }

        public override void OnPaint(PaintItemEventArgs e)
        {
            Pen pen = Pens.Silver;
            Brush brush = Brushes.Black;
            float x = e.TextRect.X;
            e.StringFormat.FormatFlags = e.StringFormat.FormatFlags | StringFormatFlags.NoWrap;


            var width = KeyWidth;
            var rect = new RectangleF(x, e.TextRect.Top, width, e.TextRect.Height);
            e.Graphics.DrawLine(pen, new PointF(x, e.TextRect.Top), new PointF(x, e.TextRect.Bottom));
            e.Graphics.DrawString(ItemKey, e.Font, brush, rect, e.StringFormat);
            x += width;

            width = ValueWidth;
            rect = new RectangleF(x, e.TextRect.Top, width, e.TextRect.Height);
            e.Graphics.DrawLine(pen, new PointF(x, e.TextRect.Top), new PointF(x, e.TextRect.Bottom));
            e.Graphics.DrawString(ItemValue, e.Font, brush, rect, e.StringFormat);
        }

        /// <summary>
        /// 编辑距离（Levenshtein Distance）
        /// </summary>
        /// <param name="source">源串</param>
        /// <param name="target">目标串</param>
        /// <param name="similarity">输出：相似度，值在0～１</param>
        /// <param name="isCaseSensitive">是否大小写敏感</param>
        /// <returns>源串和目标串之间的编辑距离</returns>
        public static Int32 LevenshteinDistance(String source, String target, out double similarity, Boolean isCaseSensitive)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    similarity = 1;
                    return 0;
                }
                else
                {
                    similarity = 0;
                    return target.Length;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                similarity = 0;
                return source.Length;
            }

            String From, To;
            if (isCaseSensitive)
            {   // 大小写敏感
                From = source;
                To = target;
            }
            else
            {   // 大小写无关
                From = source.ToLower();
                To = target.ToLower();
            }

            // 初始化
            Int32 m = From.Length;
            Int32 n = To.Length;
            Int32[,] H = new Int32[m + 1, n + 1];
            for (Int32 i = 0; i <= m; i++) H[i, 0] = i;  // 注意：初始化[0,0]
            for (Int32 j = 1; j <= n; j++) H[0, j] = j;

            // 迭代
            for (Int32 i = 1; i <= m; i++)
            {
                Char SI = From[i - 1];
                for (Int32 j = 1; j <= n; j++)
                {   // 删除（deletion） 插入（insertion） 替换（substitution）
                    if (SI == To[j - 1])
                        H[i, j] = H[i - 1, j - 1];
                    else
                        H[i, j] = Math.Min(H[i - 1, j - 1], Math.Min(H[i - 1, j], H[i, j - 1])) + 1;
                }
            }

            // 计算相似度
            Int32 MaxLength = Math.Max(m, n);   // 两字符串的最大长度
            similarity = ((Double)(MaxLength - H[m, n])) / MaxLength;

            return H[m, n];    // 编辑距离
        }
    }

}