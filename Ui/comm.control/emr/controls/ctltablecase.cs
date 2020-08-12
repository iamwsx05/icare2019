using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using DevExpress.XtraTreeList.Nodes;
using Common.Controls;
using Common.Entity;
using Common.Utils;
using weCare.Core.Utils;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 表格控件
    /// </summary>
    public partial class ctlTableCase : UserControl
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public ctlTableCase()
        {
            InitializeComponent();
        }
        #endregion

        #region 父窗体
        /// <summary>
        /// 父窗体
        /// </summary>
        private frmBaseMdi frmParent = null;
        #endregion

        #region 父容器是否需要重载数据
        /// <summary>
        /// 父容器是否需要重载数据
        /// </summary>
        public bool IsParentContainerReLoadData { get; set; }
        #endregion

        #region 边距
        /// <summary>
        /// 左边距
        /// </summary>
        private int m_intLeftMargin = 0;//8;
        /// <summary>
        /// 上边距
        /// </summary>
        private int m_intTopMargin = 0;//100;
        /// <summary>
        /// 右边距
        /// </summary>
        private int m_intRightMargin = 0;//8;
        /// <summary>
        /// 下边距
        /// </summary>
        private int m_intBottomMargin = 0;//10;

        /// <summary>
        /// 原始高度
        /// </summary>
        private int m_intOldHeight = 0;
        #endregion

        #region 控件类

        /// <summary>
        /// 标题
        /// </summary>
        private class clsCaption
        {
            public Point Location = Point.Empty;
            public Font font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            public Size size = Size.Empty;
            public ContentAlignment TextAlign = ContentAlignment.MiddleCenter;
            public Point point = Point.Empty;
            public string Text = string.Empty;
            public string strParent = string.Empty;
            public bool blnCombField = false;
            public int intStartRow = 0;
            public int intStartCol = 0;
            public int intCrossRows = 0;
            public int intCrossCols = 0;
            public int intWidth = 0;
            public int intHeight = 0;
            public bool blnSpec = false;
            public string strColCode = string.Empty;
        }

        /// <summary>
        /// 控件
        /// </summary>
        private class clsControl : IComparable
        {
            public int intRow = 0;
            public int intCol = 0;
            public string strCtlType = string.Empty;
            public Point Location = Point.Empty;
            public Font font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            public Size size = Size.Empty;
            public ContentAlignment TextAlign = ContentAlignment.MiddleCenter;

            public string strTableCode = string.Empty;
            public string strBandName = string.Empty;
            public string strFieldName = string.Empty;
            public string strFieldCaption = string.Empty;
            public int intFieldWidth = 0;
            public int intStartX = 0;
            public int intStartY = 0;
            public string strFieldType = string.Empty;
            public string strFieldConfig = string.Empty;

            public int intAllowNull = 0;
            public int intAllowEditAfterSave = 0;
            public int intShowUnderLine = 0;
            public int intAutoSign = 0;
            public int intAllMultiLine = 0;
            public int intSortNo = 0;
            public bool blnGrow = false;

            /// <summary>
            /// 比较
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                if (obj is clsControl)
                {
                    return intSortNo.CompareTo(((clsControl)obj).intSortNo);
                }
                return 0;
            }
        }
        #endregion

        #region 表格信息
        /// <summary>
        /// 表头列行高
        /// </summary>
        private int m_intHeaderRowHeight = 25;
        /// <summary>
        /// 行高
        /// </summary>
        private int m_intTableRowHeight = 35;//32;
        /// <summary>
        /// 表格宽度
        /// </summary>
        private int m_intTableWidth = 0;
        /// <summary>
        /// 表格高度
        /// </summary>
        private int m_intTableHeight = 0;

        /// <summary>
        /// 表格基础信息
        /// </summary>
        private EntityEmrTableBasicInfo m_objTableInfo = new EntityEmrTableBasicInfo();
        /// <summary>
        /// 总列数
        /// </summary>
        private int m_intAllCols = 0;

        /// <summary>
        /// 表头列数
        /// </summary>
        private int m_intCaptionColumns = 0;

        /// <summary>
        /// 原点坐标X
        /// </summary>
        private int m_intPointX = 20;//3;
        /// <summary>
        /// 原点坐标Y
        /// </summary>
        private int m_intPointY = 0;//30;
        /// <summary>
        /// 控件列表
        /// </summary>
        private List<clsControl> m_lstControl = new List<clsControl>();
        /// <summary>
        /// 横表列宽
        /// </summary>
        private Dictionary<int, int> m_dicVCaptionWidth = new Dictionary<int, int>();
        /// <summary>
        /// 横表最大行宽
        /// </summary>
        private int m_intVMaxColumnWidth = 0;
        /// <summary>
        /// 竖表列高
        /// </summary>
        private Dictionary<int, int> m_dicHCaptionHeight = new Dictionary<int, int>();
        /// <summary>
        /// 控件列表
        /// </summary>
        private Dictionary<int, List<Control>> m_dicControls = new Dictionary<int, List<Control>>();
        /// <summary>
        /// 页行数
        /// </summary>
        private int PageRowCount
        {
            get { return Function.Int(this.m_objTableInfo.displayRows); }
        }
        /// <summary>
        /// 页列数
        /// </summary>
        private int PageColCount
        {
            get { return this.m_lstControl.Count; }
        }

        /// <summary>
        /// 当前页号
        /// </summary>
        private int _intCurrentPageNo = 1;//0;
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPageNo
        {
            get { return _intCurrentPageNo; }
            set
            {
                _intCurrentPageNo = value;
                if (GlobalCase.caseInfo != null)
                    GlobalCase.caseInfo.CurrentPageNo = _intCurrentPageNo;
            }
        }


        /// <summary>
        /// 业务数据表
        /// </summary>
        private string _strDBTableName = string.Empty;

        /// <summary>
        /// 业务数据表
        /// </summary>
        public string DBTableName
        {
            get
            {
                if (GlobalCase.caseInfo == null || GlobalCase.caseInfo.TableName == null)
                    return string.Empty;

                if (GlobalCase.caseInfo.TableName.ToLower().StartsWith("emrdata"))
                    _strDBTableName = GlobalCase.caseInfo.TableName;
                return _strDBTableName;
            }
        }

        /// <summary>
        /// 痕迹表
        /// </summary>
        private string _strDBTraceName = string.Empty;

        /// <summary>
        /// 痕迹表
        /// </summary>
        public string DBTraceName
        {
            get
            {
                if (GlobalCase.caseInfo != null && GlobalCase.caseInfo.TraceName.ToLower().StartsWith("emrdata"))
                    _strDBTraceName = GlobalCase.caseInfo.TraceName;
                return _strDBTraceName;
            }
        }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string PrintTemplateName
        {
            get
            {
                if (GlobalCase.caseInfo == null)
                    return string.Empty;
                else
                    return GlobalCase.caseInfo.PrintTemplateName;
            }
        }

        /// <summary>
        /// 病历CODE
        /// </summary>
        public string CaseCode
        {
            get
            {
                if (GlobalCase.caseInfo == null)
                    return string.Empty;
                else
                    return GlobalCase.caseInfo.CaseCode;
            }
        }

        /// <summary>
        /// 表格代码
        /// </summary>
        public string TableCode { get; set; }

        /// <summary>
        /// 当前病人入院登记ID
        /// </summary>
        public string RegisterID
        {
            get { return (GlobalPatient.currPatient == null ? null : GlobalPatient.currPatient.RegisterID); }
        }

        /// <summary>
        /// 表格内病人入院登记ID
        /// </summary>
        public string TableInnerRegisterID { get; set; }


        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperID
        {
            get { return GlobalLogin.objLogin.EmpNo; }
        }

        /// <summary>
        /// 单元格值数组
        /// </summary>
        private List<clsCellData> m_lstCellData = new List<clsCellData>();

        /// <summary>
        /// 单元格值
        /// </summary>
        private class clsCellData
        {
            public int intRow { get; set; }
            public int intCol { get; set; }
            public string strValue { get; set; }
        }

        /// <summary>
        /// 单元格坐标数组
        /// </summary>
        private List<clsCellPosition> m_lstCellPosition = new List<clsCellPosition>();

        /// <summary>
        /// 单元格坐标
        /// </summary>
        private class clsCellPosition
        {
            public int intX { get; set; }
            public int intY { get; set; }
            public int intRow { get; set; }
            public int intCol { get; set; }
            public int intWidth { get; set; }
            public int intHeight { get; set; }
        }

        /// <summary>
        /// 当前行控件容器
        /// </summary>
        /// <returns></returns>
        public Panel m_ctlGetCurrentRow()
        {
            Panel pnlCurr = new Panel();

            if (CurrentRowNo >= 0)
            {
                List<Control> lstControl = this.m_dicControls[CurrentRowNo];
                foreach (Control ctl in lstControl)
                {
                    if (ctl is ctlSignature)
                    {
                        pnlCurr.Controls.Add(ctl);
                    }
                }
            }

            return pnlCurr;
        }

        /// <summary>
        /// 插入行号
        /// </summary>
        public int m_intInsertRowNo = -1;
        /// <summary>
        /// 插入原始行
        /// </summary>
        public int m_intInsertOrgRowNo = -1;

        /// <summary>
        /// 数据行是否存在签名控件
        /// </summary>
        private bool m_blnRowExistsSignCtl = true;
        /// <summary>
        /// 保存进行中
        /// </summary>
        public bool m_blnSaving = false;

        /// <summary>
        /// 数据列数组
        /// </summary>
        public List<IFormCtrl> DBColList { get; set; }

        /// <summary>
        /// RICH限制字个数的控件
        /// </summary>
        private Dictionary<int, int> m_dicLimitFonts = new Dictionary<int, int>();

        /// <summary>
        /// 不签名既允许保存的CASECODE
        /// </summary>
        private List<string> m_lstNoSignAllowSaveCaseCode = new List<string>();

        /// <summary>
        /// 计算列信息
        /// </summary>
        private Dictionary<string, int> m_dicSumColInfo = new Dictionary<string, int>();

        /// <summary>
        /// 计算子列信息
        /// </summary>
        private Dictionary<string, List<clsControl>> m_dicSumSubColInfo = new Dictionary<string, List<clsControl>>();

        /// <summary>
        /// 表格字段
        /// </summary>
        public List<EntityEmrTableFieldInfo> TableFields { get; set; }

        /// <summary>
        /// 表格源数据XML
        /// </summary>
        public string TableXmlData { get; set; }

        #endregion

        #region XML.Document


        #endregion

        #region 列标题
        /// <summary>
        /// 列标题数组(宽)
        /// </summary>
        private List<clsColumnWidth> m_lstCaptionWidth = new List<clsColumnWidth>();
        /// <summary>
        /// 列标题数组(高)
        /// </summary>
        private List<clsColumnWidth> m_lstCaptionHeight = new List<clsColumnWidth>();
        /// <summary>
        /// 列宽
        /// </summary>
        private class clsColumnWidth
        {
            public int intRow = 0;
            public int intCol = 0;
            public int intWidth = 0;
            public int intHeight = 0;
        }

        /// <summary>
        /// 获取列宽
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int m_intVCW(int num)
        {
            var col = from cols in m_lstCaptionWidth
                      where (cols.intCol == num)
                      select cols;
            if (col.ToArray().Length > 0)
            {
                return col.Sum(t => t.intWidth);
            }
            return 0;
        }
        /// <summary>
        /// 列高
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int m_intHCH(int num)
        {
            var col = from cols in m_lstCaptionHeight
                      where (cols.intCol == num)
                      select cols;
            if (col.ToArray().Length > 0)
            {
                return col.Sum(t => t.intHeight);
            }
            return 0;
        }

        /// <summary>
        /// 合计横表标题列宽
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int m_intSumVCW(int num)
        {
            var col = from cols in m_lstCaptionWidth
                      where (cols.intCol < num)
                      select cols;
            if (col.ToArray().Length > 0)
            {
                return col.Sum(t => t.intWidth);
            }
            return 0;
        }

        /// <summary>
        /// 合计竖表标题列高
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int m_intSumHCH(int num)
        {
            var col = from cols in m_lstCaptionHeight
                      where (cols.intCol < num)
                      select cols;
            if (col.ToArray().Length > 0)
            {
                return col.Sum(t => t.intHeight);
            }
            return 0;
        }
        #endregion

        #region 记录时间
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? m_dtmRecordDate
        {
            get
            {
                DateTime? dtmRecordDate = null;
                if (GlobalCase.caseInfo != null && GlobalCase.caseInfo.RecordDate != null)
                    dtmRecordDate = GlobalCase.caseInfo.RecordDate.Value;
                else if (GlobalCase.caseInfo != null && GlobalCase.caseInfo.TmpSaveOperDate != null)
                    dtmRecordDate = GlobalCase.caseInfo.TmpSaveOperDate.Value;
                return dtmRecordDate;
            }
        }
        #endregion

        #region 读取表格、列定义信息
        /// <summary>
        /// 读取表格、列定义信息
        /// </summary>
        public void m_mthLoadDefineInfo()
        {
            this.m_objTableInfo = new EntityEmrTableBasicInfo();
            List<EntityEmrTableFieldInfo> lstTableColDefInfo = new List<EntityEmrTableFieldInfo>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                DataTable dt = null;
                this.m_objTableInfo.tableCode = this.TableCode;
                dt = proxy.Service.SelectByPk(this.m_objTableInfo);
                if (dt != null)
                {
                    this.m_objTableInfo = EntityTools.ConvertToEntity<EntityEmrTableBasicInfo>(dt);
                }
                EntityEmrTableFieldInfo vo = new EntityEmrTableFieldInfo();
                vo.tableCode = this.TableCode;
                dt = proxy.Service.SelectSort(vo, new List<string> { EntityEmrTableFieldInfo.Columns.tableCode }, new List<string> { EntityEmrTableFieldInfo.Columns.sortNo });
                if (dt != null)
                {
                    lstTableColDefInfo = EntityTools.ConvertToEntityList<EntityEmrTableFieldInfo>(dt);
                    this.TableFields = lstTableColDefInfo;
                }
            }
            if (lstTableColDefInfo != null && lstTableColDefInfo.Count > 0)
            {
                this.m_intVMaxColumnWidth = 0;
                this.m_intCaptionColumns = 0;
                foreach (EntityEmrTableFieldInfo obj in lstTableColDefInfo)
                {
                    this.m_intVMaxColumnWidth = Math.Max(Function.Int(obj.fieldWidth), m_intVMaxColumnWidth);
                    this.m_intCaptionColumns = Math.Max(obj.bandName.Split(',').Length, this.m_intCaptionColumns);
                }

                int intVSumColumnWidth = 0;
                int intHSumColumnHeight = 0;
                clsColumnWidth cw = null;
                if (Function.Int(this.m_objTableInfo.rowHeight) > 0)
                {
                    this.m_intTableRowHeight = Function.Int(this.m_objTableInfo.rowHeight);
                }
                if (this.m_objTableInfo.headerWidth == null) this.m_objTableInfo.headerWidth = string.Empty;
                string[] strWidthArr = this.m_objTableInfo.headerWidth.Split(',');
                if (Function.Int(this.m_objTableInfo.displayType) == 1)
                {
                    if (strWidthArr.Length > 0)
                    {
                        for (int i = 0; i < strWidthArr.Length; i++)
                        {
                            this.m_dicVCaptionWidth.Add(i, int.Parse(strWidthArr[i]));
                            intVSumColumnWidth += int.Parse(strWidthArr[i]);

                            cw = new clsColumnWidth();
                            cw.intCol = i;
                            cw.intWidth = int.Parse(strWidthArr[i]);
                            this.m_lstCaptionWidth.Add(cw);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lstTableColDefInfo.Count; i++)
                    {
                        cw = new clsColumnWidth();
                        cw.intCol = i;
                        cw.intWidth = Function.Int(lstTableColDefInfo[i].fieldWidth);
                        this.m_lstCaptionWidth.Add(cw);
                    }

                    for (int i = 0; i < this.m_intCaptionColumns + 1; i++)
                    {
                        cw = new clsColumnWidth();
                        cw.intCol = i;
                        if (this.m_intCaptionColumns == 1 && strWidthArr.Length == 1 && !string.IsNullOrEmpty(strWidthArr[0]))
                        {
                            cw.intHeight = int.Parse(strWidthArr[0]);
                        }
                        else
                        {
                            if (this.m_intCaptionColumns + 1 != strWidthArr.Length)
                                cw.intHeight = this.m_intHeaderRowHeight;
                            else
                                cw.intHeight = int.Parse(strWidthArr[i]);
                        }
                        intHSumColumnHeight += cw.intHeight;
                        this.m_lstCaptionHeight.Add(cw);
                        this.m_dicHCaptionHeight.Add(i, cw.intHeight);
                    }
                }

                int intRow = 0;
                clsControl ctl = null;
                List<string> lstSumSubCol = null;
                foreach (EntityEmrTableFieldInfo obj in lstTableColDefInfo)
                {
                    ctl = new clsControl();
                    ctl.strCtlType = obj.fieldType;
                    if (Function.Int(this.m_objTableInfo.displayType) == 1)
                    {
                        ctl.size = new Size(this.m_intVMaxColumnWidth, this.m_intTableRowHeight);
                        ctl.intStartX = this.m_intVMaxColumnWidth;
                        ctl.intStartY = this.m_intPointY;
                    }
                    else
                    {
                        ctl.size = new Size(Function.Int(obj.fieldWidth), this.m_intTableRowHeight);
                        ctl.intStartX = this.m_intPointX;
                        ctl.intStartY = (this.m_intCaptionColumns + 1) * this.m_intHeaderRowHeight;
                    }
                    ctl.TextAlign = ContentAlignment.MiddleLeft;
                    ctl.strTableCode = obj.tableCode;
                    ctl.strBandName = obj.bandName;
                    ctl.strFieldName = obj.fieldName;
                    ctl.strFieldCaption = obj.fieldCaptain;
                    ctl.intFieldWidth = Function.Int(obj.fieldWidth);
                    ctl.strFieldType = obj.fieldType;
                    ctl.strFieldConfig = obj.fieldConfig;
                    ctl.intAllowNull = Function.Int(obj.allowNull);
                    ctl.intAllowEditAfterSave = Function.Int(obj.allowEditAfterSave);
                    ctl.intShowUnderLine = Function.Int(obj.showUnderline);
                    ctl.intAutoSign = Function.Int(obj.autoSign);
                    ctl.intAllMultiLine = Function.Int(obj.allMultiline);
                    ctl.intSortNo = Function.Int(obj.sortNo);
                    this.m_lstControl.Add(ctl);
                    this.m_dicSumColInfo.Add(obj.fieldName, intRow);

                    if (ctl.strFieldType == "求和")
                    {
                        lstSumSubCol = obj.fieldConfig.Split(',').ToList();
                        foreach (string strSubCol in lstSumSubCol)
                        {
                            if (this.m_dicSumSubColInfo.ContainsKey(strSubCol))
                            {
                                if (this.m_dicSumSubColInfo[strSubCol].IndexOf(ctl) < 0)
                                {
                                    this.m_dicSumSubColInfo[strSubCol].Add(ctl);
                                }
                            }
                            else
                            {
                                List<clsControl> lst = new List<clsControl>();
                                lst.Add(ctl);
                                this.m_dicSumSubColInfo.Add(strSubCol, lst);
                            }
                        }
                    }
                    intRow++;
                }

                if (Function.Int(this.m_objTableInfo.displayType) == 0)
                {
                    this.m_intTableWidth = (int)lstTableColDefInfo.Sum(t => Function.Int(t.fieldWidth));
                    this.m_intTableHeight = this.m_intTableRowHeight * this.PageRowCount + intHSumColumnHeight;
                }
                else if (this.m_objTableInfo.displayType == 1)
                {
                    this.m_intTableWidth = intVSumColumnWidth + m_intVMaxColumnWidth * this.PageRowCount;
                    this.m_intTableHeight = lstTableColDefInfo.Count * m_intTableRowHeight;
                }
                this.m_intAllCols = lstTableColDefInfo.Count;
                this.Width = this.m_intLeftMargin + this.m_intTableWidth + this.m_intRightMargin + 2 * this.m_intPointX + 10;  //50;
                this.Height = this.m_intTopMargin + this.m_intTableHeight + this.m_intBottomMargin + this.pnlTooBar.Height + 2 * this.m_intPointY + 10;

                this.m_mthAnalysisCaption(lstTableColDefInfo, Function.Int(this.m_objTableInfo.displayType));

                if (this.m_objTableInfo.displayType == 1)
                    this.m_mthLoadEditControl(intVSumColumnWidth, 0);
                else
                    this.m_mthLoadEditControl(0, intHSumColumnHeight);

                //扩展
                this.m_mthGrow();
                this.m_mthDrawCaption();
            }
        }
        #endregion

        #region 解析列头

        /// <summary>
        /// 标题起始数字
        /// </summary>
        private string[] m_strCaptionStartWithNumsArr = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        /// <summary>
        /// 过滤行
        /// </summary>
        private List<int> m_lstFilterRow = new List<int>();
        /// <summary>
        /// 列标题数组
        /// </summary>
        private List<clsCaption> m_lstCaption = new List<clsCaption>();
        /// <summary>
        /// 过滤标题控件
        /// </summary>
        private Dictionary<int, clsCaption> m_dicFilterCaption = new Dictionary<int, clsCaption>();

        /// <summary>
        /// 设置过滤列标题
        /// </summary>
        /// <param name="p_intStartRow"></param>
        /// <param name="p_intStartCol"></param>
        /// <param name="p_intCrossRows"></param>
        /// <param name="p_intCrossCols"></param>
        /// <param name="p_intFlag"></param>
        private void m_mthSetFilterCaption(int p_intStartRow, int p_intStartCol, int p_intCrossRows, int p_intCrossCols, int p_intLevel, int p_intFlag)
        {
            clsCaption cap = new clsCaption();

            if (p_intFlag == 0)
            {
                cap.intStartRow = p_intStartRow;
                cap.intStartCol = p_intStartCol;
                cap.intCrossRows = p_intCrossRows;
                cap.intCrossCols = p_intCrossCols;
                this.m_dicFilterCaption.Add(p_intStartRow, cap);
                this.m_lstFilterRow.Add(p_intStartRow);
            }
            else if (p_intFlag == 1)
            {
                cap.intStartRow = p_intStartRow;
                cap.intStartCol = p_intStartCol;
                cap.intCrossRows = p_intCrossRows;
                cap.intCrossCols = p_intCrossCols;
                this.m_dicFilterCaption.Add(p_intStartCol, cap);
                this.m_lstFilterRow.Add(p_intStartCol);
            }
            else if (p_intFlag == 2)
            {
                cap.intStartRow = p_intStartRow;
                cap.intStartCol = p_intLevel;
                cap.intCrossRows = p_intCrossRows;
                cap.intCrossCols = p_intCrossCols;
                cap.blnSpec = true;
                this.m_dicFilterCaption.Add(p_intStartRow, cap);
            }
            else if (p_intFlag == 3)
            {
                cap.intStartRow = p_intLevel;
                cap.intStartCol = p_intStartCol;
                cap.intCrossRows = p_intCrossRows;
                cap.intCrossCols = p_intCrossCols;
                cap.blnSpec = true;
                this.m_dicFilterCaption.Add(p_intStartCol, cap);
            }
        }

        /// <summary>
        /// 递归生成列标题(横表)
        /// </summary>
        /// <param name="p_strBandName"></param>
        /// <param name="p_strParent"></param>
        /// <param name="p_strFieldCaption"></param>
        /// <param name="p_intStartPos"></param>
        /// <param name="p_intStartRow"></param>
        /// <param name="p_intStartCol"></param>
        private void m_mthRecursiveH(string p_strBandName, string p_strParent, string p_strFieldCaption, int p_intStartPos, int p_intStartRow, int p_intStartCol, int p_intLevel)
        {
            int index = p_strBandName.IndexOf(",", p_intStartPos);
            string strCaption = string.Empty;
            if (index < 0)
            {
                strCaption = p_strBandName.Substring(p_intStartPos);
                if (this.m_lstCaption.Exists(t => t.strParent == p_strParent && t.Text == strCaption))
                {
                    clsCaption obj = this.m_lstCaption.Single(t => t.strParent == p_strParent && t.Text == strCaption);
                    obj.intCrossRows += 1;
                    obj.intHeight = (obj.intCrossRows + 1) * this.m_intTableRowHeight;
                    if (obj.Text.Length > 1)
                    {
                        if (this.m_strCaptionStartWithNumsArr.ToList().IndexOf(obj.Text.Substring(0, 1)) >= 0)
                        {
                            obj.TextAlign = ContentAlignment.MiddleLeft;
                        }
                    }

                    this.m_lstFilterRow.Add(p_intStartRow);
                    if (this.m_lstFilterRow.IndexOf(obj.intStartRow) >= 0 && this.m_lstFilterRow.IndexOf(p_intStartRow) < 0)
                    {
                        if (p_intLevel == 1)
                        {
                            this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, 0, this.m_intCaptionColumns - p_intLevel, p_intLevel, 2);
                        }
                        else
                        {
                            this.m_dicFilterCaption.Add(p_intStartRow, this.m_dicFilterCaption[obj.intStartRow]);
                        }
                    }
                    else
                    {
                        this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, 0, this.m_intCaptionColumns - p_intLevel, p_intLevel, 2);
                    }
                }
                else
                {
                    clsCaption obj = new clsCaption();
                    obj.strParent = p_strParent;
                    obj.Text = strCaption;
                    obj.intStartRow = p_intStartRow;
                    obj.intStartCol = p_intStartCol;
                    obj.intCrossRows = 0;
                    obj.intHeight = this.m_intTableRowHeight;
                    obj.intWidth = this.m_dicVCaptionWidth[p_intStartCol];

                    int intTmpRows = this.m_intCaptionColumns - 1;
                    if (strCaption == p_strFieldCaption)
                    {
                        obj.Text = string.Empty;
                        obj.intWidth = 0;
                        intTmpRows = this.m_intCaptionColumns;

                        if (p_strParent != strCaption)
                        {
                            obj.TextAlign = ContentAlignment.MiddleLeft;
                        }
                    }
                    if (p_intStartCol < intTmpRows)
                        obj.intCrossCols = intTmpRows - p_intStartCol;
                    else
                        obj.intCrossCols = 0;
                    if (obj.intCrossCols > 0 && !this.m_dicFilterCaption.ContainsKey(p_intStartRow))
                    {
                        this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, 0, obj.intCrossCols, p_intLevel, 0);
                    }
                    this.m_lstCaption.Add(obj);
                }
            }
            else
            {
                string strParent = p_strBandName.Substring(0, index);
                int pos = strParent.LastIndexOf(',');
                if (pos < 0)
                    strCaption = strParent;
                else
                    strCaption = strParent.Substring(pos + 1);
                if (this.m_lstCaption.Exists(t => t.strParent == strParent && t.Text == strCaption))
                {
                    clsCaption obj = this.m_lstCaption.Single(t => t.strParent == strParent && t.Text == strCaption);
                    obj.intCrossRows += 1;
                    obj.intHeight = (obj.intCrossRows + 1) * this.m_intTableRowHeight;
                    if (obj.Text.Length > 1)
                    {
                        if (this.m_strCaptionStartWithNumsArr.ToList().IndexOf(obj.Text.Substring(0, 1)) >= 0)
                        {
                            obj.TextAlign = ContentAlignment.MiddleLeft;
                        }
                    }
                }
                else
                {
                    clsCaption obj = new clsCaption();
                    obj.strParent = strParent;
                    obj.Text = strCaption;
                    obj.intWidth = this.m_dicVCaptionWidth[p_intStartCol];
                    obj.intStartRow = p_intStartRow;
                    obj.intStartCol = p_intStartCol;
                    obj.intCrossRows = 0;
                    obj.intHeight = this.m_intTableRowHeight;

                    if (p_strBandName.Substring(index + 1) == p_strFieldCaption)
                    {
                        obj.Text = string.Empty;
                        obj.intWidth = 0;
                        int intTmpRows = this.m_intCaptionColumns - 1;
                        if (p_intStartCol < intTmpRows)
                            obj.intCrossCols = intTmpRows - p_intStartCol;
                        else
                            obj.intCrossCols = 0;
                        if (obj.intCrossCols > 0 && !this.m_dicFilterCaption.ContainsKey(p_intStartRow))
                        {
                            this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, 0, obj.intCrossCols, p_intLevel, 0);
                        }
                        this.m_lstCaption.Add(obj);
                        return;
                    }
                    else
                    {
                        this.m_lstCaption.Add(obj);
                    }
                }
                m_mthRecursiveH(p_strBandName, strParent, p_strFieldCaption, ++index, p_intStartRow, ++p_intStartCol, ++p_intLevel);
            }
        }
        /// <summary>
        /// 递归生成列标题(竖表)
        /// </summary>
        /// <param name="p_strBandName"></param>
        /// <param name="p_strParent"></param>
        /// <param name="p_strFieldCaption"></param>
        /// <param name="p_intStartPos"></param>
        /// <param name="p_intStartRow"></param>
        /// <param name="p_intStartCol"></param>
        private void m_mthRecursiveV(string p_strBandName, string p_strParent, string p_strFieldCaption, int p_intStartPos, int p_intStartRow, int p_intStartCol, int p_intLevel)
        {
            int index = p_strBandName.IndexOf(",", p_intStartPos);
            string strCaption = string.Empty;
            if (index < 0)
            {
                strCaption = p_strBandName.Substring(p_intStartPos);
                if (this.m_lstCaption.Exists(t => t.strParent == p_strParent && t.Text == strCaption))
                {
                    clsCaption obj = this.m_lstCaption.Single(t => t.strParent == p_strParent && t.Text == strCaption);
                    obj.intCrossCols += 1;
                    obj.intWidth += this.m_intVCW(obj.intStartCol + obj.intCrossCols);

                    this.m_lstFilterRow.Add(p_intStartCol);
                    if (this.m_lstFilterRow.IndexOf(obj.intStartCol) >= 0 && this.m_lstFilterRow.IndexOf(p_intStartCol) < 0)
                    {
                        if (p_intLevel == 1)
                        {
                            this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, this.m_intCaptionColumns - p_intLevel, 0, p_intLevel, 3);
                        }
                        else
                        {
                            this.m_dicFilterCaption.Add(p_intStartCol, this.m_dicFilterCaption[obj.intStartCol]);
                        }
                    }
                    else
                    {
                        this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, this.m_intCaptionColumns - p_intLevel, 0, p_intLevel, 3);
                    }
                }
                else
                {
                    clsCaption obj = new clsCaption();
                    obj.strParent = p_strParent;
                    obj.Text = strCaption;
                    obj.intStartRow = p_intStartRow;
                    obj.intStartCol = p_intStartCol;
                    obj.intCrossRows = 0;
                    obj.intHeight = this.m_intHCH(p_intStartRow);
                    obj.intWidth = this.m_intVCW(p_intStartCol);

                    int intTmpRows = this.m_intCaptionColumns - 1;
                    if (strCaption == p_strFieldCaption)
                    {
                        obj.Text = string.Empty;
                        obj.intHeight = 0;
                        intTmpRows = this.m_intCaptionColumns;

                        if (p_strParent != strCaption)
                        {
                            obj.TextAlign = ContentAlignment.MiddleLeft;
                        }
                    }
                    if (p_intStartRow < intTmpRows)
                        obj.intCrossRows = intTmpRows - p_intStartRow;
                    else
                        obj.intCrossRows = 0;
                    if (obj.intCrossRows > 0 && !this.m_dicFilterCaption.ContainsKey(p_intStartCol))
                    {
                        this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, obj.intCrossRows, 0, p_intLevel, 1);
                    }
                    this.m_lstCaption.Add(obj);
                }
            }
            else
            {
                string strParent = p_strBandName.Substring(0, index);
                int pos = strParent.LastIndexOf(',');
                if (pos < 0)
                    strCaption = strParent;
                else
                    strCaption = strParent.Substring(pos + 1);
                if (this.m_lstCaption.Exists(t => t.strParent == strParent && t.Text == strCaption))
                {
                    clsCaption obj = this.m_lstCaption.Single(t => t.strParent == strParent && t.Text == strCaption);
                    obj.intCrossCols += 1;
                    obj.intWidth += this.m_intVCW(obj.intStartCol + obj.intCrossCols);
                }
                else
                {
                    clsCaption obj = new clsCaption();
                    obj.strParent = strParent;
                    obj.Text = strCaption;
                    obj.intStartRow = p_intStartRow;
                    obj.intStartCol = p_intStartCol;
                    obj.intCrossRows = 0;
                    obj.intWidth = this.m_intVCW(p_intStartCol);
                    obj.intHeight = this.m_intHCH(p_intStartRow);

                    if (p_strBandName.Substring(index + 1) == p_strFieldCaption)
                    {
                        obj.Text = string.Empty;
                        obj.intHeight = 0;
                        int intTmpRows = this.m_intCaptionColumns - 1;

                        if (p_intStartRow < intTmpRows)
                            obj.intCrossRows = intTmpRows - p_intStartRow;
                        else
                            obj.intCrossRows = 0;
                        if (obj.intCrossRows > 0 && !this.m_dicFilterCaption.ContainsKey(p_intStartCol))
                        {
                            this.m_mthSetFilterCaption(p_intStartRow, p_intStartCol, obj.intCrossRows, 0, p_intLevel, 1);
                        }
                        this.m_lstCaption.Add(obj);
                        return;
                    }
                    else
                    {
                        this.m_lstCaption.Add(obj);
                    }
                }
                m_mthRecursiveV(p_strBandName, strParent, p_strFieldCaption, ++index, ++p_intStartRow, p_intStartCol, ++p_intLevel);
            }
        }
        /// <summary>
        /// 解析列标题
        /// </summary>
        /// <param name="p_lstTableColDefInfo"></param>
        /// <param name="p_intStyle"></param>
        private void m_mthAnalysisCaption(List<EntityEmrTableFieldInfo> p_lstTableColDefInfo, int p_intStyle)
        {
            this.m_lstCaption.Clear();
            this.m_lstFilterRow.Clear();

            if (p_intStyle == 1)
            {
                for (int i = 0; i < p_lstTableColDefInfo.Count; i++)
                {
                    this.m_mthRecursiveH(p_lstTableColDefInfo[i].bandName, p_lstTableColDefInfo[i].bandName, p_lstTableColDefInfo[i].fieldCaptain, 0, i, 0, 1);
                }
            }
            else
            {
                for (int i = 0; i < p_lstTableColDefInfo.Count; i++)
                {
                    this.m_mthRecursiveV(p_lstTableColDefInfo[i].bandName, p_lstTableColDefInfo[i].bandName, p_lstTableColDefInfo[i].fieldCaptain, 0, 0, i, 1);
                }
            }

            clsCaption objCaption = null;
            for (int i = 0; i < p_lstTableColDefInfo.Count; i++)
            {
                if (this.m_lstFilterRow.IndexOf(i) < 0)
                {
                    objCaption = new clsCaption();
                    objCaption.Text = p_lstTableColDefInfo[i].fieldCaptain;
                    if (p_intStyle == 1)
                    {
                        objCaption.intStartRow = i;
                        objCaption.intStartCol = this.m_intCaptionColumns;
                        objCaption.intHeight = this.m_intTableRowHeight;
                        objCaption.TextAlign = ContentAlignment.MiddleLeft;
                        objCaption.intWidth = this.m_dicVCaptionWidth[objCaption.intStartCol];
                    }
                    else
                    {
                        objCaption.intStartRow = this.m_intCaptionColumns;
                        objCaption.intStartCol = i;
                        objCaption.intHeight = this.m_intHCH(objCaption.intStartRow);
                        objCaption.TextAlign = ContentAlignment.MiddleCenter;
                        objCaption.intWidth = this.m_intVCW(objCaption.intStartCol);
                    }
                    objCaption.intCrossRows = 0;
                    objCaption.intCrossCols = 0;
                    objCaption.strColCode = p_lstTableColDefInfo[i].fieldName;
                }
                else
                {
                    if (this.m_dicFilterCaption.ContainsKey(i))
                    {
                        objCaption = new clsCaption();
                        objCaption.Text = p_lstTableColDefInfo[i].fieldCaptain;
                        objCaption.strColCode = p_lstTableColDefInfo[i].fieldName;
                        if (p_intStyle == 1)
                        {
                            objCaption.intStartRow = i;
                            if (this.m_dicFilterCaption[i].blnSpec)
                                objCaption.intStartCol = this.m_dicFilterCaption[i].intStartCol;
                            else
                                objCaption.intStartCol = this.m_intCaptionColumns - this.m_dicFilterCaption[i].intCrossCols;
                            objCaption.intHeight = this.m_intTableRowHeight;
                            if (objCaption.intStartCol == 0)
                            {
                                if (this.m_strCaptionStartWithNumsArr.ToList().IndexOf(objCaption.Text.Substring(0, 1)) >= 0)
                                    objCaption.TextAlign = ContentAlignment.MiddleLeft;
                                else
                                    objCaption.TextAlign = ContentAlignment.MiddleCenter;
                            }
                            else
                                objCaption.TextAlign = ContentAlignment.MiddleLeft;
                            objCaption.intWidth = this.m_dicVCaptionWidth[objCaption.intStartCol];
                            for (int w = 0; w < this.m_dicFilterCaption[i].intCrossCols; w++)
                            {
                                objCaption.intWidth += this.m_dicVCaptionWidth[objCaption.intStartCol + 1 + w];
                            }
                        }
                        else
                        {
                            if (this.m_dicFilterCaption[i].blnSpec)
                                objCaption.intStartRow = this.m_dicFilterCaption[i].intStartRow;
                            else
                                objCaption.intStartRow = this.m_intCaptionColumns - this.m_dicFilterCaption[i].intCrossRows;
                            objCaption.intStartCol = i;
                            objCaption.intHeight = this.m_intHCH(objCaption.intStartRow);
                            objCaption.intWidth = this.m_intVCW(objCaption.intStartCol);
                            for (int h = 0; h < this.m_dicFilterCaption[i].intCrossRows; h++)
                            {
                                objCaption.intHeight += this.m_dicHCaptionHeight[objCaption.intStartRow + 1 + h];
                            }
                        }
                    }
                    else continue;
                }
                this.m_lstCaption.Add(objCaption);
            }

            //int intHeight = (p_intStyle == 1 ? this.m_intTableRowHeight : this.m_intHeaderRowHeight);
            //foreach (clsCaption obj in this.m_lstCaption)
            //{
            //    obj.Location = new Point(this.m_intPointX + m_intSumVCW(obj.intStartCol), this.m_intPointY + obj.intStartRow * intHeight);
            //}
            if (p_intStyle == 1)
            {
                foreach (clsCaption obj in this.m_lstCaption)
                {
                    obj.Location = new Point(this.m_intPointX + m_intSumVCW(obj.intStartCol), this.m_intPointY + obj.intStartRow * this.m_intTableRowHeight);
                }
            }
            else
            {
                foreach (clsCaption obj in this.m_lstCaption)
                {
                    obj.Location = new Point(this.m_intPointX + m_intSumVCW(obj.intStartCol), this.m_intPointY + this.m_intSumHCH(obj.intStartRow));
                }
            }

            if (this.m_lstCaption.Count == 0)
            {
                return;
            }

            Label lblCaption = null;
            foreach (clsCaption obj in this.m_lstCaption)
            {
                lblCaption = new Label();
                lblCaption.Font = obj.font;
                lblCaption.Size = new Size(obj.intWidth - 1, obj.intHeight - 1);
                lblCaption.Location = new Point(obj.Location.X + 1, obj.Location.Y + 1);
                lblCaption.TextAlign = obj.TextAlign;
                lblCaption.Text = obj.Text;
                lblCaption.Tag = obj.Text;
                lblCaption.AutoSize = false;
                lblCaption.BorderStyle = BorderStyle.None;
                lblCaption.BackColor = Color.White;
                lblCaption.ForeColor = Color.Black;
                lblCaption.Name = obj.strColCode;
                if (obj.strColCode.ToLower().Contains("selfdefine"))
                {
                    lblCaption.Name = "lbl" + obj.strColCode;
                    lblCaption.MouseDoubleClick += new MouseEventHandler(lblCaption_MouseDoubleClick);
                }
                this.pnlMain.Controls.Add(lblCaption);
            }

            this.m_mthRefreshColCaption();
        }

        /// <summary>
        /// 刷新列标题
        /// </summary>
        private void m_mthRefreshColCaption()
        {
            string strColDesc = string.Empty;
            Label obj = null;
            DataTable dt = null;
            EntityEmrSelfDefineCol vo = null;
            ProxyEntityFactory proxy = new ProxyEntityFactory();
            foreach (Control item in this.pnlMain.Controls)
            {
                if (item is Label)
                {
                    obj = item as Label;
                    if (obj.Name.ToLower().Contains("lblselfdefine"))
                    {
                        vo = new EntityEmrSelfDefineCol();
                        vo.registerId = GlobalPatient.currPatient == null ? null : GlobalPatient.currPatient.RegisterID;
                        vo.caseCode = GlobalCase.caseInfo == null ? null : GlobalCase.caseInfo.CaseCode;
                        vo.colCode = obj.Name.Substring(3);
                        vo.pageNo = this.CurrentPageNo;
                        dt = proxy.Service.SelectByPk(vo);
                        if (dt != null && dt.Rows.Count > 0)
                            strColDesc = dt.Rows[0][EntityTools.GetFieldName(vo, EntityEmrSelfDefineCol.Columns.colDesc)].ToString();
                        else
                            strColDesc = string.Empty;
                        if (!string.IsNullOrEmpty(strColDesc))
                        {
                            obj.Text = strColDesc;
                        }
                        else
                        {
                            obj.Text = obj.Tag.ToString();
                        }
                    }
                }
            }
            proxy = null;

            // 宏元素页绑定
            this.m_mthSetTabPageBandingPatInfo();
        }

        private void lblCaption_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                frmSetSelfDefineColumn frm = new frmSetSelfDefineColumn(((Label)sender).Name.Substring(3), ((Label)sender).Text, (this.CurrentPageNo <= 0 ? 1 : this.CurrentPageNo));
                frm.ShowDialog(this.FindForm());
                if (!string.IsNullOrEmpty(frm.strColCaption))
                {
                    ((Label)sender).Text = frm.strColCaption;
                }
                else
                {
                    if (((Label)sender).Text != frm.strOrgColCaption)
                    {
                        ((Label)sender).Text = ((Label)sender).Tag.ToString();
                    }
                }
            }
        }

        private PictureBox m_picBackGround = null;
        /// <summary>
        /// 画表格
        /// </summary>
        private void m_mthDrawCaption()
        {
            Image imgBack = new Bitmap(this.pnlMain.Width, this.pnlMain.Height);
            Graphics g = Graphics.FromImage(imgBack);

            this.m_mthDrawCaption(g);
            m_picBackGround = new PictureBox();
            m_picBackGround.Image = imgBack;
            m_picBackGround.Size = new Size(imgBack.Width, imgBack.Height);
            m_picBackGround.Location = new Point(0, 0);
            m_picBackGround.SendToBack();
            pnlMain.Controls.Add(m_picBackGround);
        }

        /// <summary>
        /// 画标题及表格
        /// </summary>
        /// <param name="g"></param>
        private void m_mthDrawCaption(Graphics g)
        {
            int intRows = 0;
            int intX = this.m_intPointX;
            int intY = this.m_intPointY;

            for (int i = 0; i < this.m_lstCaptionWidth.Count; i++)
            {
                g.DrawRectangle(Pens.Black, intX, intY, this.m_lstCaptionWidth[i].intWidth, this.m_intTableHeight);
                intX += this.m_lstCaptionWidth[i].intWidth;
            }

            intRows = this.PageRowCount;
            //0: 纵向 1: 横向
            if (Function.Int(this.m_objTableInfo.displayType) == 1)
            {
                for (int i = 0; i < intRows; i++)
                {
                    g.DrawRectangle(Pens.Black, intX, intY, this.m_intVMaxColumnWidth, this.m_intTableHeight);
                    intX += this.m_intVMaxColumnWidth;
                }
                intX = this.m_intPointX;
                intRows = this.m_intAllCols;
                for (int i = 0; i < intRows; i++)
                {
                    g.DrawRectangle(Pens.Black, intX, intY, this.m_intTableWidth, this.m_intTableRowHeight);
                    intY += this.m_intTableRowHeight;
                }
            }
            else
            {
                intX = this.m_intPointX;
                for (int i = 0; i < this.m_intCaptionColumns + 1; i++)
                {
                    g.DrawRectangle(Pens.Black, intX, intY, this.m_intTableWidth, this.m_intHCH(i));
                    intY += this.m_intHCH(i);
                }
                for (int i = 0; i < intRows; i++)
                {
                    g.DrawString(Convert.ToString(i + 1), new Font("宋体", 9, FontStyle.Regular), new Pen(Color.Black).Brush, new Point(intX - 18, intY + this.m_intTableRowHeight / 3 + 3));
                    //g.DrawString(Convert.ToString(i + 1), new Font("FixedSys", 10, FontStyle.Regular), new Pen(Color.Black).Brush, new Point(intX - 18, intY + this.m_intTableRowHeight / 3));
                    g.DrawRectangle(Pens.Black, intX, intY, this.m_intTableWidth, this.m_intTableRowHeight);
                    //g.DrawString(Convert.ToString(i + 1), new Font("FixedSys", 10, FontStyle.Regular), new Pen(Color.Black).Brush, new Point(intX + this.m_intTableWidth + 10, intY + this.m_intTableRowHeight / 3));
                    intY += this.m_intTableRowHeight;
                }
            }
        }
        #endregion

        #region 加载编辑控件

        /// <summary>
        /// 赋TAG值
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="p_intRowNo"></param>
        /// <param name="p_intColNo"></param>
        /// <param name="p_strRowIndex"></param>
        private void m_mthSetTag(Control ctl, int p_intRowNo, int p_intColNo, string p_strRowIndex)
        {
            string strTag = p_intRowNo.ToString() + ";" + p_intColNo.ToString();

            if (!string.IsNullOrEmpty(p_strRowIndex)) strTag += ";" + p_strRowIndex;

            if (ctl.Tag != null)
            {
                if (ctl.Tag.ToString().Split(';').Length == 3 && strTag.Split(';').Length == 2)
                {
                    int ii = p_intRowNo;
                }
            }

            ctl.Tag = strTag;
        }

        /// <summary>
        /// 重置RichText
        /// </summary>
        private void m_mthResetRichText()
        {
            ContextMenuStrip cms = null;
            if (frmParent != null)
            {
                //cms = frmParent.rtfMenuStrip;
            }

            ctlRichTextBox rich = null;
            foreach (var item in this.m_dicControls.Values)
            {
                foreach (Control ctl in item)
                {
                    if (ctl is ctlRichTextBox)
                    {
                        rich = ctl as ctlRichTextBox;
                        rich.UseRowSpacing = true;
                        rich._intRowSpacing = 18;
                        rich.RowShrinkdigit = 0;
                        rich.FirstlineCaption = string.Empty;
                        rich.BorderStyle = BorderStyle.None;
                        rich.Padding = new Padding(0, 0, 0, 0);
                        rich.Validating += new CancelEventHandler(rich_Validating);
                        rich.ShowUnderLine = false;
                        //rich.Text = string.Empty;
                        if (cms != null)
                        {
                            rich.ContextMenuStrip = cms;
                            rich.SetLoginUser(GlobalLogin.objLogin.EmpNo, GlobalLogin.objLogin.EmpName);
                        }
                        rich.m_blnIniting = false;
                    }
                }
            }
        }

        /// <summary>
        /// 加载编辑控件
        /// </summary>
        /// <param name="p_intStartX"></param>
        /// <param name="p_intStartY"></param>
        private void m_mthLoadEditControl(int p_intStartX, int p_intStartY)
        {
            clsControl ctl = null;
            Control control = null;
            this.m_lstControl.Sort();

            int intX = 0;
            int intY = 0;
            int intDiffX = 0;
            int intDiffY = 0;
            int intDiffW = 0;

            int intStyle = Function.Int(this.m_objTableInfo.displayType);
            int intRows = this.PageRowCount;

            intX = this.m_intPointX + p_intStartX;
            intY = this.m_intPointY + p_intStartY;

            bool blnSumFlag = false;
            List<Control> lstControl = null;
            Dictionary<int, Control> dicControl = new Dictionary<int, Control>();

            clsCellPosition objCellPosition = null;
            this.m_lstCellPosition = new List<clsCellPosition>();

            pnlMain.SuspendLayout();
            for (int i = 0; i < intRows; i++)
            {
                blnSumFlag = false;
                dicControl.Clear();
                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_lstControl[j];
                    if (string.IsNullOrEmpty(ctl.strFieldConfig)) ctl.strFieldConfig = string.Empty;
                    switch (ctl.strCtlType)
                    {
                        case "标签":
                            control = new Label();
                            break;
                        case "文本":
                            control = new ctlTextBox();
                            ctlTextBox txt = control as ctlTextBox;
                            txt.Font = ctl.font;
                            txt.Text = string.Empty;
                            txt.ShowUnderLine = ctl.intShowUnderLine == 1 ? true : false;
                            txt.PresentationMode = 2;
                            // 2019-04-16
                            //txt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            txt.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            break;
                        case "是否":
                            control = new ctlCheckBox();
                            ctlCheckBox chk = control as ctlCheckBox;
                            chk.ShowUnderLine = false;
                            chk.PresentationMode = 2;
                            chk.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                            chk.Properties.LookAndFeel.SkinName = "Black";
                            chk.Text = string.Empty;
                            chk.BackColor = Color.Transparent;
                            chk.Properties.AutoWidth = false;
                            intDiffY = intY + this.m_intTableRowHeight / 4;
                            if (intStyle == 1)
                            {
                                chk.Location = new Point(intX - 8 + this.m_intVMaxColumnWidth / 2, intDiffY);
                                chk.Size = new Size(this.m_intVMaxColumnWidth / 2 - 3, ctl.size.Height / 2);
                            }
                            else
                            {
                                chk.Location = new Point(intX - 8 + ctl.intFieldWidth / 2, intDiffY);
                                chk.Size = new Size(ctl.intFieldWidth / 2 - 3, ctl.size.Height / 2);
                            }
                            chk.TabStop = false;
                            break;
                        case "日期":
                            control = new ctlDatetime();
                            ctlDatetime dtm = control as ctlDatetime;
                            dtm.ShowUnderLine = ctl.intShowUnderLine == 1 ? true : false;
                            dtm.PresentationMode = 2;
                            dtm.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                            dtm.Properties.LookAndFeel.SkinName = "Black";
                            dtm.EditValue = null;
                            dtm.EnterMoveNextControl = true;
                            if (!string.IsNullOrEmpty(ctl.strFieldConfig))
                            {
                                dtm.EditMask = ctl.strFieldConfig;
                            }
                            dtm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            dtm.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            dtm.Size = new Size(ctl.size.Width - 1, ctl.size.Height - 1);
                            dtm.Location = new Point(intX + 1, intY + (int)Math.Floor(((double)(this.m_intTableRowHeight - control.Height)) / 2) + 3);//this.m_intTableRowHeight / 2);//4);
                            break;
                        case "枚举":
                            control = new ctlComboBox();
                            ctlComboBox cbo = control as ctlComboBox;
                            cbo.Items.AddRange(ctl.strFieldConfig.Split(','));
                            cbo.Size = new Size(ctl.size.Width - 1, ctl.size.Height - 1);
                            cbo.Location = new Point(intX + 1, intY + (int)Math.Floor(((double)(this.m_intTableRowHeight - control.Height)) / 2) + 3);// this.m_intTableRowHeight / 2);//4);
                            cbo.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                            cbo.Properties.LookAndFeel.SkinName = "Black";
                            cbo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            cbo.PresentationMode = 0;
                            cbo.ShowUnderLine = ctl.intShowUnderLine == 1 ? true : false;
                            break;
                        case "数字":
                            control = new ctlTextBox();
                            ctlTextBox num = control as ctlTextBox;
                            num.ShowUnderLine = ctl.intShowUnderLine == 1 ? true : false;
                            num.PresentationMode = 2;
                            num.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            num.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            num.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                            num.Validating += new CancelEventHandler(textBox_Validating);
                            break;
                        case "求和":
                            control = new ctlLabel();
                            control.ContextMenuStrip = this.contextMenuStripSum;
                            ((ctlLabel)control).TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            blnSumFlag = true;
                            break;
                        case "病历":
                            control = new ctlRichTextBox(true, false);
                            ((ctlRichTextBox)control).m_blnTableFlag = true;
                            ((ctlRichTextBox)control).PresentationMode = 2;
                            ((ctlRichTextBox)control).ParentTable = this;
                            break;
                        case "签名":
                            control = new ctlSignature();
                            ctlSignature signature = control as ctlSignature;
                            signature.ShowUnderLine = ctl.intShowUnderLine == 1 ? true : false;
                            signature.PresentationMode = 2;
                            signature.Caption = ctl.strFieldConfig;
                            signature.TabStop = false;
                            signature.TableFlag = true;
                            signature.IsAllowSignNull = (int)ctl.intAllowNull;
                            signature.IsAutoSignature = (int)ctl.intAutoSign;
                            break;
                        default:
                            break;
                    }
                    if (control != null)
                    {
                        if (ctl.strCtlType != "是否")
                        {
                            control.Size = new Size(ctl.size.Width - 1, ctl.size.Height - 1);

                            if (intStyle == 1)
                            {
                                if (ctl.strCtlType == "文本" || ctl.strCtlType == "数字" || ctl.strCtlType == "日期" || ctl.strCtlType == "枚举")
                                    intDiffY = intY + (int)Math.Floor(((double)(this.m_intTableRowHeight - control.Height)) / 2) + 3;
                                else
                                    intDiffY = intY + this.m_intTableRowHeight - control.Height;
                                control.Location = new Point(intX + 1, intDiffY);
                            }
                            else
                            {
                                intDiffX = intX + this.m_lstControl[j].intFieldWidth - control.Width;
                                if (ctl.strCtlType == "文本")
                                {
                                    intDiffY = intY + (int)Math.Floor(((double)(this.m_intTableRowHeight - control.Height)) / 2) + 3;
                                    control.Location = new Point(intDiffX, intDiffY);
                                }
                                else
                                {
                                    if (ctl.strCtlType != "日期" && ctl.strCtlType != "枚举")
                                    {
                                        control.Location = new Point(intDiffX, intY + 1);
                                    }
                                }
                            }
                        }
                        if (ctl.strCtlType == "病历")
                        {
                            List<string> lstColLimit = new List<string>();
                            if (GlobalParm.dicSysParameter.ContainsKey(24))
                                lstColLimit = GlobalParm.dicSysParameter[24].ToLower().Split(';').ToList();
                            if (lstColLimit.Count > 0)
                            {
                                string[] sarr = null;
                                foreach (string str in lstColLimit)
                                {
                                    sarr = str.Split('-');
                                    if (sarr.Length == 3)
                                    {
                                        if (sarr[0] == this.m_objTableInfo.tableCode.ToLower() && sarr[1] == ctl.strFieldName.ToLower())
                                        {
                                            int intFontNums = 0;
                                            int.TryParse(sarr[2], out intFontNums);
                                            if (intFontNums > 0)
                                            {
                                                ((ctlRichTextBox)control).MaxLength = intFontNums;
                                                if (!this.m_dicLimitFonts.ContainsKey(j))
                                                {
                                                    this.m_dicLimitFonts.Add(j, intFontNums);
                                                }
                                                ((ctlRichTextBox)control).evtReachMaxLength += new HandleReachMaxLength(ctlRichTextBox_evtReachMaxLength);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        control.Enter += new EventHandler(control_Enter);
                        control.Leave += new EventHandler(control_Leave);
                        control.KeyDown += new KeyEventHandler(control_KeyDown);
                        this.m_mthSetTag(control, i, j, string.Empty);

                        if (control is IFormCtrl)
                        {
                            IFormCtrl col = control as IFormCtrl;
                            col.ValueChangedFlag = false;
                        }
                        dicControl.Add(j, control);

                        this.pnlMain.Controls.Add(control);

                        objCellPosition = new clsCellPosition();
                        objCellPosition.intX = intX;
                        objCellPosition.intY = intY;
                        objCellPosition.intRow = i;
                        objCellPosition.intCol = j;
                        if (intStyle == 1)
                        {
                            objCellPosition.intWidth = this.m_intVMaxColumnWidth;
                        }
                        else
                        {
                            objCellPosition.intWidth = this.m_lstControl[j].intFieldWidth;
                        }
                        objCellPosition.intHeight = this.m_intTableRowHeight;
                        this.m_lstCellPosition.Add(objCellPosition);
                        if (control is IFormCtrl)
                        {
                            this.DBColList.Add((IFormCtrl)control);
                        }
                    }

                    if (this.m_dicControls.ContainsKey(i))
                    {
                        this.m_dicControls[i].Add(control);
                    }
                    else
                    {
                        lstControl = new List<Control>();
                        lstControl.Add(control);
                        this.m_dicControls.Add(i, lstControl);
                    }

                    intDiffW = ctl.size.Width;

                    if (intStyle == 1)
                    {
                        intY += this.m_intTableRowHeight;
                    }
                    else
                    {
                        intX += this.m_lstControl[j].intFieldWidth;
                    }
                }

                if (blnSumFlag)
                {
                    clsControl ctlCurrent = null;
                    clsControl ctlTemp = null;
                    List<string> lstFieldConfig = new List<string>();
                    for (int m = 0; m < this.PageColCount; m++)
                    {
                        ctlCurrent = this.m_lstControl[m];
                        if (ctlCurrent.strFieldType == "求和" && !string.IsNullOrEmpty(ctlCurrent.strFieldConfig))
                        {
                            lstFieldConfig.Clear();
                            lstFieldConfig.AddRange(ctlCurrent.strFieldConfig.Split(','));

                            for (int n = 0; n < this.PageColCount; n++)
                            {
                                ctlTemp = this.m_lstControl[n];
                                if (lstFieldConfig.Contains(ctlTemp.strFieldName))
                                {
                                    if (dicControl[n] is ctlTextBox)
                                    {
                                        ((ctlTextBox)dicControl[n]).EditValueChanged += new EventHandler(ctlTextBox_EditValueChanged);
                                    }
                                    else if (dicControl[n] is ctlRichTextBox)
                                    {
                                        ((ctlRichTextBox)dicControl[n]).Validated += new EventHandler(ctlRichTextBox_Validated);
                                    }
                                    else if (dicControl[n] is ctlComboBox)
                                    {
                                        ((ctlComboBox)dicControl[n]).SelectedIndexChanged += new EventHandler(ctlComboBox_SelectedIndexChanged);
                                    }
                                }
                            }
                        }
                    }
                }

                if (intStyle == 1)
                {
                    intX += intDiffW;
                    intY = this.m_intPointY + p_intStartY;
                }
                else
                {
                    intX = this.m_intPointX + p_intStartX;
                    intY += this.m_intTableRowHeight;
                }
            }

            pnlMain.ResumeLayout();

            //bgwRich.RunWorkerAsync();
        }

        /// <summary>
        /// 获取求和列
        /// </summary>
        /// <returns></returns>
        private List<string> m_strGetComputeCols()
        {
            List<string> lstCols = new List<string>();
            clsControl ctlCurrent = this.m_lstControl[CurrentColNo];
            if (ctlCurrent.strFieldType == "求和" && !string.IsNullOrEmpty(ctlCurrent.strFieldConfig))
            {
                lstCols.AddRange(ctlCurrent.strFieldConfig.Split(','));
            }
            return lstCols;
        }

        /// <summary>
        /// 计算合计单元格值
        /// </summary>
        /// <param name="p_intStartRowNo"></param>
        /// <param name="p_intEndRowNo"></param>
        private decimal m_decComputeCellSum(int p_intStartRowNo, int p_intEndRowNo)
        {
            decimal decValue = 0;
            if (p_intStartRowNo < 1) return decValue;

            List<string> lstFieldCode = m_strGetComputeCols();
            if (lstFieldCode.Count > 0)
            {
                decimal decTemp = 0;
                foreach (string col in lstFieldCode)
                {
                    for (int i = p_intStartRowNo - 1; i < p_intEndRowNo; i++)
                    {
                        decimal.TryParse(this.m_dicControls[i][this.m_dicSumColInfo[col]].Text, out decTemp);
                        decValue += decTemp;
                    }
                }
            }
            return decValue;
        }

        private void ctlRichTextBox_evtReachMaxLength(object sender)
        {
            if (this.m_dicLimitFonts.ContainsKey(CurrentColNo))
            {
                if (CurrentRowNo < this.m_dicControls.Keys.Count - 1)
                {
                    ctlRichTextBox rich = ((ctlRichTextBox)this.m_dicControls[CurrentRowNo][CurrentColNo]);

                    if (rich.KeyCtrlC) return;

                    int intStart = rich.SelectionStart;
                    string strSubText = rich.Text.Substring(intStart).Trim();
                    if (CurrentRowNo < PageRowCount - 1 && strSubText != string.Empty)
                    {
                        int row = CurrentRowNo;
                        if (this.m_blnInsertRow(false))
                        {
                            this.m_intInsertOrgRowNo = row;
                            rich.Text = rich.Text.Substring(0, intStart);
                            rich.SelectionStart = intStart;
                            ((ctlRichTextBox)this.m_dicControls[row + 1][CurrentColNo]).Text = strSubText;

                            for (int k = 0; k < this.m_lstControl.Count; k++)
                            {
                                if (this.m_dicControls[row][k] is ctlSignature)
                                {
                                    List<EntitySignature> lstSign = ((ctlSignature)this.m_dicControls[row][k]).GetSignEmp();
                                    if (lstSign != null)
                                    {
                                        foreach (EntitySignature item in lstSign)
                                        {
                                            item.serNo = 0;
                                        }
                                        ((ctlSignature)this.m_dicControls[row + 1][k]).AddSignEmp(lstSign);
                                        ((ctlSignature)this.m_dicControls[row + 1][k]).m_lstNoSaveSignature.AddRange(lstSign.ToArray());
                                        //((ctlSignature)this.m_dicControls[row + 1][k]).ValueChangedFlag = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.m_dicControls[CurrentRowNo + 1][CurrentColNo].Enabled)
                        {
                            this.m_dicControls[CurrentRowNo + 1][CurrentColNo].Focus();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 提示行列表
        /// </summary>
        private List<int> m_lstWriteHintRowNo = new List<int>();

        /// <summary>
        /// 书写提示
        /// </summary>
        /// <param name="p_intRowNo"></param>
        private void m_mthWriteHint(int p_intRowNo)
        {
            if (this.m_lstWriteHintRowNo.IndexOf(p_intRowNo) >= 0) return;

            bool blnStatus = false;
            foreach (Control ctl in this.m_dicControls[p_intRowNo])
            {
                if (ctl is ctlSignature)
                {
                    if (((ctlSignature)ctl).IsSignature)
                    {
                        blnStatus = true;
                        break;
                    }
                }
            }

            if (blnStatus)
            {
                this.m_lstWriteHintRowNo.Add(p_intRowNo);
                if (GlobalHospital.HospitalCode == "0001")
                {
                    DialogBox.Msg("温馨提示：\r\n\r\n    当前行已签名保存。", MessageBoxIcon.Information, frmParent);
                }
            }
        }

        /// <summary>
        /// 控件KeyDow事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Control ctl = sender as Control;
            string strTag = ctl.Tag.ToString();
            int intRowNo = int.Parse(strTag.Split(';')[0]);
            int intColNo = int.Parse(strTag.Split(';')[1]);
            this.m_mthWriteHint(intRowNo);

            if ((e.KeyCode == Keys.Up && Function.Int(this.m_objTableInfo.displayType) == 0) ||
                (e.KeyCode == Keys.Left && Function.Int(this.m_objTableInfo.displayType) == 1))
            {
                if (intRowNo > 0)
                {
                    if (ctl is ctlComboBox)
                    {
                        if (((ctlComboBox)ctl).SelectedIndex > 0 && !string.IsNullOrEmpty(((ctlComboBox)ctl).Text))
                            ((ctlComboBox)ctl).SelectedIndex++;
                    }

                    if (this.m_dicControls[intRowNo - 1][intColNo].Enabled)
                    {
                        this.m_dicControls[intRowNo - 1][intColNo].Focus();
                    }
                }
            }
            else if ((e.KeyCode == Keys.Down && Function.Int(this.m_objTableInfo.displayType) == 0) ||
                    (e.KeyCode == Keys.Right && Function.Int(this.m_objTableInfo.displayType) == 1))
            {
                if (intRowNo < this.m_dicControls.Keys.Count - 1)
                {
                    if (ctl is ctlComboBox)
                    {
                        if (((ctlComboBox)ctl).SelectedIndex < ((ctlComboBox)ctl).Items.Count - 1 && !string.IsNullOrEmpty(((ctlComboBox)ctl).Text))
                            ((ctlComboBox)ctl).SelectedIndex--;
                    }

                    if (this.m_dicControls[intRowNo + 1][intColNo].Enabled)
                    {
                        this.m_dicControls[intRowNo + 1][intColNo].Focus();
                    }
                }
            }
            else if ((e.KeyCode == Keys.Left && Function.Int(this.m_objTableInfo.displayType) == 0) ||
                     (e.KeyCode == Keys.Up && Function.Int(this.m_objTableInfo.displayType) == 1))
            {
                if (intColNo > 0)
                {
                    if (ctl is ctlRichTextBox)
                    {
                        if (((ctlRichTextBox)ctl).SelectionStart > 0)
                            return;
                    }
                    if (ctl is ctlTextBox)
                    {
                        if (((ctlTextBox)ctl).SelectionStart > 0)
                            return;
                    }
                    if (ctl is ctlComboBox)
                    {
                        if (((ctlComboBox)ctl).SelectionStart > 0)
                            return;
                    }
                    if (ctl is ctlDatetime)
                    {
                        if (((ctlDatetime)ctl).SelectionStart > 0)
                            return;
                    }
                    if (this.m_dicControls[intRowNo][intColNo - 1].Enabled)
                    {
                        this.m_dicControls[intRowNo][intColNo - 1].Focus();
                    }
                }
            }
            else if ((e.KeyCode == Keys.Right && Function.Int(this.m_objTableInfo.displayType) == 0) ||
                     (e.KeyCode == Keys.Down && Function.Int(this.m_objTableInfo.displayType) == 1))
            {
                if (ctl is ctlRichTextBox)
                {
                    if (((ctlRichTextBox)ctl).SelectionStart < ((ctlRichTextBox)ctl).Text.Length)
                        return;
                }
                if (ctl is ctlTextBox)
                {
                    if (((ctlTextBox)ctl).SelectionStart < ((ctlTextBox)ctl).Text.Length)
                        return;
                }
                if (ctl is ctlComboBox)
                {
                    if (((ctlComboBox)ctl).SelectionStart < ((ctlComboBox)ctl).Text.Length)
                        return;
                }
                if (ctl is ctlDatetime)
                {
                    if (((ctlDatetime)ctl).SelectionStart < ((ctlDatetime)ctl).Text.Length - 2)
                        return;
                }
                if (intColNo < this.m_lstControl.Count - 1)
                {
                    if (this.m_dicControls[intRowNo][intColNo + 1].Enabled)
                    {
                        this.m_dicControls[intRowNo][intColNo + 1].Focus();
                    }
                }
            }
        }

        #region 表格控制
        /// <summary>
        /// 当前行号
        /// </summary>
        private int CurrentRowNo { get; set; }
        /// <summary>
        /// 当前列号
        /// </summary>
        private int CurrentColNo { get; set; }
        /// <summary>
        /// 当前背景Label
        /// </summary>
        private Label CurrentBackLabel = null;
        /// <summary>
        /// 当前背景Label组
        /// </summary>
        private List<Label> lstCurrentBackLabel = new List<Label>();
        /// <summary>
        /// 上一行号
        /// </summary>
        private int PreviousRowNo { get; set; }

        /// <summary>
        /// 控件Enter事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_Enter(object sender, EventArgs e)
        {
            string strTag = ((Control)sender).Tag.ToString();
            int intRowNo = int.Parse(strTag.Split(';')[0]);
            if (intRowNo != CurrentRowNo)
            {
                if (this.m_lstWriteHintRowNo.IndexOf(CurrentRowNo) >= 0)
                {
                    this.m_lstWriteHintRowNo.Remove(CurrentRowNo);
                }
            }
            CurrentRowNo = intRowNo;
            CurrentColNo = int.Parse(strTag.Split(';')[1]);

            if (this.m_intInsertRowNo >= 0 && this.CurrentRowNo != this.m_intInsertRowNo)
            {
                this.m_dicControls[this.m_intInsertRowNo][this.CurrentColNo].Focus();
                DialogBox.Msg("请保存(或删除)插入的新行记录后，再进行其他业务操作。", MessageBoxIcon.Information, this.ParentForm);
                return;
            }

            if (sender is ctlDatetime && ((ctlDatetime)sender).DateTime == null)
            {
                ((ctlDatetime)sender).DateTime = DateTime.Now;
            }
            else if (sender is ctlRichTextBox)
            {
                if (frmParent != null)
                {
                    frmParent.CurrRtfEditor = (ctlRichTextBox)sender;
                    clsControl ctlInfo = this.m_lstControl[CurrentColNo];
                    frmParent.CurrRtfEditor.ItemName = ctlInfo.strFieldName;
                    frmParent.CurrRtfEditor.ItemCaption = ctlInfo.strFieldCaption;
                }
            }

            #region 行选
            List<clsCellPosition> lstCP = new List<clsCellPosition>();
            var rowctls = from rc in this.m_lstCellPosition
                          where (rc.intRow == CurrentRowNo)
                          select rc;
            lstCP = rowctls.ToList();

            if (lstCP.Count > 0)
            {
                this.SuspendLayout();
                foreach (clsCellPosition cp in lstCP)
                {
                    Label lblCCP = new Label();
                    lblCCP.Size = new Size(cp.intWidth - 1, cp.intHeight - 1);
                    lblCCP.Location = new Point(cp.intX + 1, cp.intY + 1);
                    lblCCP.AutoSize = false;
                    lblCCP.BorderStyle = BorderStyle.None;
                    lblCCP.BackColor = uiHelper.GridRowCustomColor;
                    lblCCP.Tag = CurrentRowNo.ToString();
                    try
                    {
                        this.pnlMain.Controls.Add(lblCCP);
                        this.lstCurrentBackLabel.Add(lblCCP);
                    }
                    catch { }

                    lblCCP.SendToBack();

                    this.m_dicControls[cp.intRow][cp.intCol].BringToFront();
                    if (this.m_dicControls[cp.intRow][cp.intCol] is ctlSignature && this.m_dicControls[cp.intRow][cp.intCol].BackColor == Color.Blue)
                    {
                    }
                    else
                    {
                        this.m_dicControls[cp.intRow][cp.intCol].BackColor = uiHelper.GridRowCustomColor;
                    }
                }

                this.m_picBackGround.SendToBack();
                this.ResumeLayout();
            }
            #endregion

            #region 单元格选中
            //clsCellPosition cp = this.m_lstCellPosition.Single(t => t.intRow == CurrentRowNo && t.intCol == CurrentColNo);
            //if (cp != null)
            //{
            //    this.CurrentBackLabel = null;
            //    this.CurrentBackLabel = new Label();
            //    this.CurrentBackLabel.Size = new Size(cp.intWidth - 1, cp.intHeight - 1);
            //    this.CurrentBackLabel.Location = new Point(cp.intX + 1, cp.intY + 1);
            //    this.CurrentBackLabel.AutoSize = false;
            //    this.CurrentBackLabel.BorderStyle = BorderStyle.None;
            //    this.CurrentBackLabel.BackColor = clsHelper.s_clrTableBKColor;
            //    this.CurrentBackLabel.BringToFront();
            //    this.pnlMain.Controls.Add(this.CurrentBackLabel);
            //    this.m_picBackGround.SendToBack();
            //    ((Control)sender).BringToFront();
            //    ((Control)sender).BackColor = clsHelper.s_clrTableBKColor;
            //}
            #endregion
        }

        /// <summary>
        /// 清空背景控件
        /// </summary>
        private void m_mthClearBKColorCtl()
        {
            if (lstCurrentBackLabel.Count > 0)
            {
                foreach (Control ctl in this.m_dicControls[CurrentRowNo])
                {
                    if (ctl is ctlSignature && ctl.BackColor == Color.Blue)
                    {
                    }
                    else
                    {
                        ctl.BackColor = Color.White;
                    }
                }
                foreach (Label lbl in this.lstCurrentBackLabel)
                {
                    this.pnlMain.Controls.Remove(lbl);
                }
                this.lstCurrentBackLabel.Clear();
            }
        }

        /// <summary>
        /// 控件Leave事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_Leave(object sender, EventArgs e)
        {
            this.m_mthClearBKColorCtl();

            // 单元格选中
            //((Control)sender).BackColor = Color.White;
            //if (this.CurrentBackLabel != null)
            //{
            //    this.pnlMain.Controls.Remove(this.CurrentBackLabel);
            //}
        }

        private void ctlTableCase_Leave(object sender, EventArgs e)
        {
            //this.m_mthClearBKColorCtl();
        }

        #region 有效性判断
        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rich_Validating(object sender, CancelEventArgs e)
        {
            bool blnPass = m_blnRichTextValidating((ctlRichTextBox)sender);
            e.Cancel = !blnPass;
        }

        /// <summary>
        /// RICHTEXT有效性检查
        /// </summary>
        /// <param name="p_objRichTextBox"></param>
        /// <returns></returns>
        private bool m_blnRichTextValidating(ctlRichTextBox p_objRichTextBox)
        {
            if (p_objRichTextBox == null)
            {
                return true;
            }
            int intColNo = int.Parse(p_objRichTextBox.Tag.ToString().Split(';')[1]);

            string strRichTxt = p_objRichTextBox.GetRightText(true).Trim();
            //判断最大最小区间
            if (string.IsNullOrEmpty(strRichTxt) == false)
            {
                clsControl clsInfo = this.m_lstControl[intColNo];
                if (!string.IsNullOrEmpty(clsInfo.strFieldConfig))
                {
                    string[] strFieldConfigArr = clsInfo.strFieldConfig.Split(',');
                    if (strFieldConfigArr.Length == 2)
                    {
                        decimal decValue = 0;
                        if (decimal.TryParse(strRichTxt, out decValue))
                        {
                            try
                            {
                                decimal decMin = decimal.Parse(strFieldConfigArr[0]);
                                if (decValue < decMin)
                                {
                                    DialogBox.Msg("该输入框下限为：" + strFieldConfigArr[0], MessageBoxIcon.Information, this.ParentForm);
                                    p_objRichTextBox.m_mthCleartext(strRichTxt);
                                    return false;
                                }

                                decimal decMax = decimal.Parse(strFieldConfigArr[1]);
                                if (decValue > decMax)
                                {
                                    DialogBox.Msg("该输入框上限为：" + strFieldConfigArr[1], MessageBoxIcon.Information, this.ParentForm);
                                    p_objRichTextBox.m_mthCleartext(strRichTxt);
                                    return false;
                                }
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            DialogBox.Msg("请输入数值", MessageBoxIcon.Information, this.ParentForm);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region 数字录入框校验
        /// <summary>
        /// 数字录入框校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            bool blnPass = m_blnTextValidating((ctlTextBox)sender);
            e.Cancel = !blnPass;
        }
        /// <summary>
        /// TEXTBOX有效性检查
        /// </summary>
        /// <param name="p_objTextBox"></param>
        /// <returns></returns>
        private bool m_blnTextValidating(ctlTextBox p_objTextBox)
        {
            string strValue = p_objTextBox.Text.Trim();
            if (strValue == ".")
            {
                //允许数字输入框为空
                p_objTextBox.Text = "";
            }
            else
            {
                int intColNo = int.Parse(p_objTextBox.Tag.ToString().Split(';')[1]);

                //判断最大最小区间
                if (string.IsNullOrEmpty(strValue) == false)
                {
                    clsControl clsInfo = this.m_lstControl[intColNo];
                    string[] strFieldConfigArr = null;

                    if (!string.IsNullOrEmpty(clsInfo.strFieldConfig))
                    {
                        strFieldConfigArr = clsInfo.strFieldConfig.Split(',');
                    }
                    decimal val = decimal.Zero;
                    if (!decimal.TryParse(strValue, out val))
                    {
                        DialogBox.Msg("请输入数字！", MessageBoxIcon.Information, this.ParentForm);
                        return false;
                    }

                    if (strFieldConfigArr != null && strFieldConfigArr.Length == 2)
                    {
                        try
                        {
                            decimal valMin = decimal.Parse(strFieldConfigArr[0]);
                            decimal valMax = decimal.Parse(strFieldConfigArr[1]);
                            if (val < valMin || val > valMax)
                            {
                                DialogBox.Msg("该输入框值范围为：" + valMin.ToString() + "-" + valMax.ToString(), MessageBoxIcon.Information, this.ParentForm);
                                p_objTextBox.Text = string.Empty;
                                p_objTextBox.Focus();
                                //p_objTextBox.Text = strFieldConfigArr[0];
                            }
                            //if (val > valMax)
                            //{
                            //    p_objTextBox.Text = strFieldConfigArr[1];
                            //}
                        }
                        catch
                        {

                        }

                    }
                }
            }
            return true;
        }

        #endregion

        #region 计算合计值
        /// <summary>
        /// 计算合计值
        /// </summary>
        /// <param name="p_strNO"></param>
        private void m_mthComputeSum(string p_strNO, int p_intType)
        {
            int intRowNo = int.Parse(p_strNO.Split(';')[0]);
            int intColNo = int.Parse(p_strNO.Split(';')[1]);
            decimal decSum = 0;
            decimal decTemp = 0;
            ctlLabel lblSum = null;
            List<string> lstFieldConfig = new List<string>();

            clsControl ctlCell = this.m_lstControl[intColNo];
            if (this.m_dicSumSubColInfo.ContainsKey(ctlCell.strFieldName))
            {
                List<clsControl> lstSumCol = this.m_dicSumSubColInfo[ctlCell.strFieldName];
                foreach (clsControl ctlInfo in lstSumCol)
                {
                    lstFieldConfig.Clear();
                    lstFieldConfig.AddRange(ctlInfo.strFieldConfig.Split(','));

                    bool isSubZero = false;
                    if (lstFieldConfig.Count > 0)
                    {
                        decSum = 0;     // 2019-01-22 同一行有单个分组，还有总的合计, 不重置0的话会重复计算
                        decTemp = 0;
                        string subStr = string.Empty;
                        foreach (string col in lstFieldConfig)
                        {
                            if (p_intType == 0)
                            {
                                subStr = ((ctlRichTextBox)this.m_dicControls[intRowNo][this.m_dicSumColInfo[col]]).GetRightText(true).Trim();
                                decimal.TryParse(subStr, out decTemp);
                            }
                            else
                            {
                                subStr = this.m_dicControls[intRowNo][this.m_dicSumColInfo[col]].Text.Trim();
                                decimal.TryParse(subStr, out decTemp);
                            }
                            decSum += decTemp;
                            if (subStr == "0") isSubZero = true;
                        }
                        lblSum = this.m_dicControls[intRowNo][this.m_dicSumColInfo[ctlInfo.strFieldName]] as ctlLabel;
                    }
                    if (lblSum != null)
                    {
                        if (isSubZero)
                            lblSum.Text = decSum.ToString();
                        else
                        {
                            if (decSum != 0)
                                lblSum.Text = decSum.ToString();
                            else
                                lblSum.Text = string.Empty;
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// RichText.Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlRichTextBox_Validated(object sender, EventArgs e)
        {
            string strNo = Convert.ToString(((ctlRichTextBox)sender).Tag);
            this.m_mthComputeSum(strNo, 0);
        }

        /// <summary>
        /// TextBox.ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTextBox_EditValueChanged(object sender, EventArgs e)
        {
            string strNo = Convert.ToString(((ctlTextBox)sender).Tag);
            this.m_mthComputeSum(strNo, 1);
        }

        /// <summary>
        /// ComboBox.SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strNo = Convert.ToString(((ctlComboBox)sender).Tag);
            this.m_mthComputeSum(strNo, 1);
        }
        #endregion
        #endregion

        #region 业务操作

        /// <summary>
        /// 最大行数
        /// </summary>
        /// <returns></returns>
        private int m_intMaxRowIndex()
        {
            if (string.IsNullOrEmpty(this.DBTableName)) return 0;
            using (ProxyCommon proxy = new ProxyCommon())
            {
                return proxy.Service.GetTableMaxRowIndex(this.RegisterID, this.DBTableName, this.TableCode, this.m_dtmRecordDate);
            }
        }

        /// <summary>
        /// LOAD指定页数据
        /// </summary>
        /// <param name="p_intPageNo"></param>
        public void m_mthLoadData(int p_intPageNo)
        {
            try
            {
                if (!this.m_blnSaving)
                {
                    if (this.IsValueChanged() && this.ChangePageFlag)
                    {
                        if (DialogBox.Msg("表格数据已修改, 是否继续?\r\n\r\n点击[是]将丢失未保存的数据.", MessageBoxIcon.Question, this.ParentForm) == DialogResult.No)
                            return;
                    }
                    uiHelper.BeginLoading(frmParent);
                }
                if (string.IsNullOrEmpty(this.DBTableName))
                {
                    this.m_mthFillData(null, null);
                    return;
                }
                int intMaxPageNo = (int)Math.Ceiling((double)(this.m_intMaxRowIndex() + 1) / (double)this.PageRowCount);
                this.CurrentPageNo = p_intPageNo;
                if (p_intPageNo == -1 || p_intPageNo > intMaxPageNo)
                {
                    this.CurrentPageNo = intMaxPageNo;
                }
                else if (p_intPageNo < 1)
                {
                    this.CurrentPageNo = 1;
                }

                this.txtCurrentPage.Text = this.CurrentPageNo.ToString();
                this.txtTotalPage.Text = string.Format("/ {0} 页", intMaxPageNo.ToString());

                int intStartRowNo = (this.CurrentPageNo - 1) * this.PageRowCount;
                int intEndRowNo = this.CurrentPageNo * this.PageRowCount - 1;

                List<EntityEmrData> lstCaseDataArr = null;
                List<EntitySignature> lstSignatureArr = null;
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    proxy.Service.GetCaseTable(this.RegisterID, this.DBTableName, this.TableCode, intStartRowNo, intEndRowNo, this.m_dtmRecordDate, out lstCaseDataArr, out lstSignatureArr);
                }
                //uiHelper.UnCompressCaseData(ref lstCaseDataArr);
                this.m_mthFillData(lstCaseDataArr, lstSignatureArr);
                this.m_lstWriteHintRowNo.Clear();
            }
            finally
            {
                this.ChangePageFlag = false;
                uiHelper.CloseLoading(frmParent);
            }
        }
        /// <summary>
        /// LOAD第一页数据
        /// </summary>
        public void m_mthFirstPageData()
        {
            int intPageNo = 1;
            try
            {
                intPageNo = int.Parse(txtCurrentPage.Text);
            }
            catch { }
            this.m_mthLoadData(intPageNo);
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        /// <param name="ctl"></param>
        private void m_mthClearCell(Control ctl)
        {
            if (ctl is ctlSignature)
            {
                ((ctlSignature)ctl).ClearText();
            }
            else if (ctl is IRtfEditor)
            {
                ((ctlRichTextBox)ctl).ClearText();
            }
            //else if (ctl is IDBColProperty)
            //{
            //    ((IDBColProperty)ctl).DBValue = string.Empty;
            //}
            else if (ctl is ctlCheckBox)
            {
                ((ctlCheckBox)ctl).Checked = false;
            }
            else if (ctl is ctlDatetime)
            {
                ((ctlDatetime)ctl).EditValue = null;
            }
            else
            {
                ctl.Text = string.Empty;
            }
        }

        /// <summary>
        /// 清空行数据
        /// </summary>
        /// <param name="p_intRowNo"></param>
        private void m_mthClearRow(int p_intRowNo)
        {
            Control ctl = null;
            clsCellData objCellData = null;
            for (int j = 0; j < this.PageColCount; j++)
            {
                ctl = this.m_dicControls[p_intRowNo][j];
                if (ctl != null)
                {
                    this.m_mthClearCell(ctl);
                }
                else
                {
                    continue;
                }
                if (ctl is IFormCtrl)
                {
                    ((IFormCtrl)ctl).ValueChangedFlag = false;
                }
                this.m_mthSetTag(ctl, p_intRowNo, j, string.Empty);

                objCellData = new clsCellData();
                objCellData.intRow = p_intRowNo;
                objCellData.intCol = j;
                if (ctl is ctlCheckBox)
                    objCellData.strValue = "0";
                else
                    objCellData.strValue = string.Empty;
                this.m_lstCellData.Add(objCellData);
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="p_dcCaseDataArr"></param>
        /// <param name="p_dcSignatureArr"></param>
        public void m_mthFillData(List<EntityEmrData> lstCaseData, List<EntitySignature> lstSignature)
        {
            Control ctl = null;
            clsCellData objCellData = null;

            this.m_lstCellData.Clear();
            if (lstCaseData == null || lstCaseData.Count == 0)
            {
                for (int i = 0; i < this.PageRowCount; i++)
                {
                    this.m_mthClearRow(i);
                }
            }
            else
            {
                string strCellValue = string.Empty;
                EntityEmrData dcCaseData = null;
                clsControl clsCtl = null;

                int intPageNo = lstCaseData[0].RowIndex / this.PageRowCount;
                int intRowIndex = 0;
                Dictionary<int, int> dicRowNo = new Dictionary<int, int>();
                Dictionary<int, int> dicRowNo2 = new Dictionary<int, int>();
                foreach (EntityEmrData data in lstCaseData)
                {
                    intRowIndex = data.RowIndex % this.PageRowCount;
                    if (!dicRowNo.ContainsKey(intRowIndex))
                    {
                        dicRowNo.Add(intRowIndex, data.RowIndex);
                        dicRowNo2.Add(intRowIndex, intRowIndex);
                    }
                    if (intRowIndex == this.m_intInsertRowNo)
                    {
                        this.m_intInsertRowNo = -1;
                        this.m_intInsertOrgRowNo = -1;
                    }
                }
                if (lstSignature != null)
                {
                    foreach (EntitySignature data in lstSignature)
                    {
                        data.commId = data.commId % this.PageRowCount;
                    }
                }

                string strCellData = string.Empty;
                for (int i = 0; i < this.PageRowCount; i++)
                {
                    if (dicRowNo.ContainsKey(i))
                    {
                        for (int j = 0; j < this.PageColCount; j++)
                        {
                            ctl = this.m_dicControls[i][j];
                            clsCtl = this.m_lstControl[j];
                            strCellData = string.Empty;
                            if (lstCaseData.Exists(t => t.RowIndex == dicRowNo[i] && t.FieldName == clsCtl.strFieldName))
                            {
                                dcCaseData = lstCaseData.First(t => t.RowIndex == dicRowNo[i] && t.FieldName == clsCtl.strFieldName);
                                if (dcCaseData != null)
                                {
                                    if (ctl is ctlSignature)
                                    {
                                        ctlSignature ctlSignature = ctl as ctlSignature;

                                        //签名内容
                                        ctlSignature.ClearText();
                                        ctlSignature.Text = dcCaseData.FieldText;
                                        if (lstSignature != null && lstSignature.Exists(t => (t.objectID == clsCtl.strFieldName && t.commId == dicRowNo2[i])))
                                        {
                                            List<EntitySignature> data = lstSignature.FindAll(t => (t.objectID == clsCtl.strFieldName && t.commId == dicRowNo2[i]));
                                            foreach (EntitySignature obj in data)
                                            {
                                                obj.caseCode = GlobalCase.caseInfo == null ? string.Empty : GlobalCase.caseInfo.CaseCode;
                                                ctlSignature.AddSignEmp(obj);
                                                ctlSignature.SetSignName(obj.empName);
                                                ctlSignature.m_lstNoSaveSignature.Add(obj);
                                            }
                                        }
                                        strCellData = ctl.Text;
                                    }
                                    else if (ctl is ctlCheckBox)
                                    {
                                        ((ctlCheckBox)ctl).Checked = Function.Int(dcCaseData.FieldText) == 1 ? true : false;
                                        strCellData = dcCaseData.FieldText;
                                    }
                                    else if (ctl is ctlDatetime)
                                    {
                                        if (string.IsNullOrEmpty(dcCaseData.FieldText))
                                            ((ctlDatetime)ctl).EditValue = null;
                                        else
                                        {
                                            ((ctlDatetime)ctl).Text = dcCaseData.FieldText;
                                            strCellData = dcCaseData.FieldText;
                                        }
                                    }
                                    else if (ctl is IRtfEditor)
                                    {
                                        //((ctlRichTextBox)ctl).SetXmlText(dcCaseData.FieldRtf, dcCaseData.FieldMarkXml, true);
                                        ctl.Text = dcCaseData.FieldText;
                                        strCellData = ctl.Text;
                                    }
                                    else if (ctl is IRuntimeDesignControl)
                                    {
                                        ((IRuntimeDesignControl)ctl).Text = dcCaseData.FieldText;
                                        strCellData = ((IRuntimeDesignControl)ctl).Text;
                                    }
                                    else
                                    {
                                        ctl.Text = dcCaseData.FieldText;
                                        strCellData = ctl.Text;
                                    }
                                }
                                else
                                {
                                    this.m_mthClearCell(ctl);
                                }
                            }
                            else
                            {
                                this.m_mthClearCell(ctl);
                            }
                            this.m_mthSetTag(ctl, i, j, dicRowNo[i].ToString());

                            if (ctl is IFormCtrl)
                            {
                                ((IFormCtrl)ctl).ValueChangedFlag = false;
                            }

                            objCellData = new clsCellData();
                            objCellData.intRow = i;
                            objCellData.intCol = j;
                            objCellData.strValue = strCellData;
                            this.m_lstCellData.Add(objCellData);
                        }
                    }
                    else
                    {
                        this.m_mthClearRow(i);
                    }
                }
            }
            this.m_mthRefreshColCaption();
            this.TableInnerRegisterID = (GlobalPatient.currPatient == null ? null : GlobalPatient.currPatient.RegisterID);
        }
        /// <summary>
        /// 清空页面
        /// </summary>
        public void m_mthClearPage()
        {
            this.m_mthFillData(null, null);
        }

        /// <summary>
        /// 值改变
        /// </summary>
        /// <returns></returns>
        public bool IsValueChanged()
        {
            for (int i = 0; i < this.PageRowCount; i++)
            {
                if (m_intCheckRowStatus(i) == 2) //!= 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 检查行状态
        /// </summary>
        /// <param name="p_intRowNo"></param>
        /// <returns></returns>
        private int m_intCheckRowStatus(int p_intRowNo)
        {
            if (this.m_lstCellData == null || this.m_lstCellData.Count == 0)
            {
                return 1;
            }

            int intCheckStatus = 1;
            string strCellData = string.Empty;
            Control ctl = null;
            Control ctl2 = null;
            clsCellData objCellData = null;

            if (this.m_blnRowExistsSignCtl)
            {
                for (int j = 0; j < this.PageColCount; j++)
                {
                    intCheckStatus = 0;
                    ctl = this.m_dicControls[p_intRowNo][j];
                    if (ctl is ctlSignature)
                    {
                        //if (objSign.IsAllowSignNull == 1)
                        //{
                        //    continue;
                        //}
                        if (((ctlSignature)ctl).ValueChangedFlag)
                            return intCheckStatus = 2;
                        if (((ctlSignature)ctl).m_lstNoSaveSignature.Count > 0)
                        {
                            break;
                            //continue;
                        }
                        else
                        {
                            intCheckStatus = 1;
                            for (int k = 0; k < this.PageColCount; k++)
                            {
                                strCellData = string.Empty;
                                ctl2 = this.m_dicControls[p_intRowNo][k];
                                if (ctl2 is ctlSignature)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (ctl2 is IRtfEditor)
                                        strCellData = ctl2.Text;
                                    //else if (ctl2 is IDBColProperty)
                                    //    strCellData = ((IDBColProperty)ctl2).DBValue;
                                    else if (ctl2 is ctlCheckBox)
                                        strCellData = ((ctlCheckBox)ctl2).Checked ? "1" : "0";
                                    else
                                        strCellData = ctl2.Text;

                                    objCellData = this.m_lstCellData.FirstOrDefault(t => t.intRow == p_intRowNo && t.intCol == k);
                                    // 2019-04-16  护理记录-特殊情况记录，第一行书写需要空2格。
                                    //if (objCellData.strValue.Trim() != strCellData.Trim())
                                    if (objCellData.strValue.TrimEnd() != strCellData.TrimEnd())
                                    {
                                        if (ctl2 is ctlCheckBox && objCellData.strValue.Trim() == "" && strCellData == "0") continue;
                                        return intCheckStatus = 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                intCheckStatus = 1;
                for (int k = 0; k < this.PageColCount; k++)
                {
                    strCellData = string.Empty;
                    ctl2 = this.m_dicControls[p_intRowNo][k];

                    if (ctl2 is IRtfEditor)
                        strCellData = ctl2.Text;
                    //else if (ctl2 is IDBColProperty)
                    //    strCellData = ((IDBColProperty)ctl2).DBValue;
                    else if (ctl2 is ctlCheckBox)
                        strCellData = ((ctlCheckBox)ctl2).Checked ? "1" : "0";
                    else
                        strCellData = ctl2.Text;

                    objCellData = this.m_lstCellData.SingleOrDefault(t => t.intRow == p_intRowNo && t.intCol == k);
                    if (objCellData.strValue.Trim() != strCellData.Trim())
                    {
                        if (ctl2 is ctlCheckBox && objCellData.strValue.Trim() == "" && strCellData == "0") continue;
                        return intCheckStatus = 2;
                    }
                }
            }
            return intCheckStatus;
        }

        /// <summary>
        /// 设置签名控件红底色
        /// </summary>
        /// <param name="p_lstSignCtrlA"></param>
        /// <param name="p_lstSignCtrlB"></param>
        private void m_mthSetSignCtrlColor(List<Control> p_lstMustSignCtrl, List<Control> p_lstNormalSignCtrl)
        {
            if (p_lstMustSignCtrl != null && p_lstMustSignCtrl.Count > 0)
            {
                foreach (Control ctrl in p_lstMustSignCtrl)
                {
                    ctrl.BackColor = Color.Blue;
                }
                return;
            }
            if (p_lstNormalSignCtrl != null && p_lstNormalSignCtrl.Count > 0)
            {
                p_lstNormalSignCtrl[0].BackColor = Color.Blue;
            }
        }

        /// <summary>
        /// 检查行有效性
        /// </summary>
        /// <param name="p_intRowNo"></param>
        /// <returns></returns>
        private bool m_blnCheckRowValid(int p_intRowNo, bool p_blnCaseSaveFlag)
        {
            int intSignCtlNums = 0;
            int intSignEmpNums = 0;
            Control ctl = null;
            clsControl ctlInfo = null;
            IRtfEditor objRtf = null;
            List<Control> lstMustSignCtrl = new List<Control>();
            List<Control> lstNormalSignCtrl = new List<Control>();

            for (int j = 0; j < this.PageColCount; j++)
            {
                ctl = this.m_dicControls[p_intRowNo][j];
                ctlInfo = this.m_lstControl[j];

                if (ctl is IRtfEditor)
                {
                    objRtf = ctl as IRtfEditor;
                    if (ctlInfo.intAllowNull == 0 && string.IsNullOrEmpty(objRtf.Text))
                    {
                        this.m_mthNullHint(ctl, p_intRowNo, j, ctlInfo.strFieldCaption);
                        return false;
                    }
                }
                else if (ctl is ctlSignature)
                {
                    intSignCtlNums++;
                    if (((ctlSignature)ctl).m_lstNoSaveSignature.Count > 0)
                    {
                        intSignEmpNums += ((ctlSignature)ctl).m_lstNoSaveSignature.Count;
                        continue;
                    }
                    else if (((ctlSignature)ctl).IsAllowSignNull == 1)
                    {
                        if (lstNormalSignCtrl.Count == 0)
                            lstNormalSignCtrl.Add(ctl);
                        continue;
                    }
                    else
                    {
                        if (((ctlSignature)ctl).IsAllowSignNull == 0 && p_blnCaseSaveFlag)
                        {
                            if (((ctlSignature)ctl).GetSignEmp().Count > 0 || !string.IsNullOrEmpty(((ctlSignature)ctl).Text))
                            {
                                if (lstNormalSignCtrl.Count == 0)
                                    lstNormalSignCtrl.Add(ctl);
                                continue;
                            }
                            else
                            {
                                if (((ctlSignature)ctl).IsAutoSignature == 1)
                                {
                                    EntitySignature dcSign = new EntitySignature();
                                    dcSign.empId = GlobalLogin.objLogin.EmpNo;
                                    dcSign.empName = GlobalLogin.objLogin.EmpName;
                                    dcSign.signDate = DateTime.Now;
                                    dcSign.recordDate = DateTime.Now;
                                    dcSign.techLevelCode = GlobalLogin.objLogin.TechLevelCode;
                                    dcSign.techLevelName = GlobalLogin.objLogin.TechLevelName;
                                    dcSign.registerId = GlobalPatient.currPatient.RegisterID;
                                    dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                                    dcSign.objectID = ((ctlSignature)ctl).ItemName;
                                    ((ctlSignature)ctl).AddSignEmp(dcSign);
                                    intSignEmpNums++;
                                }
                                else
                                {
                                    lstMustSignCtrl.Add(ctl);
                                    this.m_mthSetSignCtrlColor(lstMustSignCtrl, lstNormalSignCtrl);
                                    DialogBox.Msg("内容已修改，请签名。", MessageBoxIcon.Information, this.ParentForm);
                                    return false;
                                }
                            }
                        }
                    }
                }
                else if (ctl is IFormCtrl) //IDBColProperty)
                {
                    if (ctlInfo.intAllowNull == 0)// && string.IsNullOrEmpty(((IDBColProperty)ctl).DBValue))
                    {
                        this.m_mthNullHint(ctl, p_intRowNo, j, ctlInfo.strFieldCaption);
                        return false;
                    }
                }
            }
            if ((intSignCtlNums > 0 && intSignEmpNums == 0) && (GlobalCase.caseInfo != null && GlobalCase.caseInfo.CaseStyle == 1) && p_blnCaseSaveFlag)
            {
                if (this.m_lstNoSignAllowSaveCaseCode.IndexOf(this.CaseCode) >= 0)
                {

                }
                else
                {
                    if (lstNormalSignCtrl.Count > 0) this.m_mthSetSignCtrlColor(lstMustSignCtrl, lstNormalSignCtrl);
                    DialogBox.Msg("内容已修改，请签名。", MessageBoxIcon.Information, this.ParentForm);
                    return false;
                }
            }

            for (int i = 0; i < this.PageRowCount; i++)
            {
                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_dicControls[i][j];
                    if (ctl is ctlSignature && ctl.BackColor == Color.Blue)
                    {
                        ctl.BackColor = Color.White;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 行签名信息
        /// </summary>
        /// <param name="p_intRowNo"></param>
        /// <returns></returns>
        private string m_strRowSignatureContent(int p_intRowNo)
        {
            string strCellData = string.Empty;
            Control ctl = null;
            clsControl ctlInfo = null;
            StringBuilder stbXML = new StringBuilder();

            stbXML.Append("<tablesignature>");
            stbXML.Append(System.Environment.NewLine);
            stbXML.Append("<registerid>" + this.RegisterID.ToString() + "</registerid>");
            stbXML.Append(System.Environment.NewLine);
            stbXML.Append("<casecode>" + this.CaseCode + "</casecode>");
            stbXML.Append(System.Environment.NewLine);
            stbXML.Append("<tablecode>" + this.TableCode + "</tablecode>");
            stbXML.Append(System.Environment.NewLine);
            stbXML.Append("<cols>");
            stbXML.Append(System.Environment.NewLine);
            for (int k = 0; k < this.PageColCount; k++)
            {
                ctl = this.m_dicControls[p_intRowNo][k];
                ctlInfo = this.m_lstControl[k];
                if (ctl is IRtfEditor)
                    strCellData = ((IRtfEditor)ctl).GetRightText();
                else if (ctl is ctlSignature)
                    strCellData = ctl.Text;
                else if (ctl is ctlCheckBox)
                    strCellData = ((ctlCheckBox)ctl).Checked ? "1" : "0";
                //else if (ctl is IDBColProperty)
                //    strCellData = ((IDBColProperty)ctl).DBValue;
                else
                    strCellData = ctl.Text;

                stbXML.Append("<col code=\"" + ctlInfo.strFieldName + "\" text=\"" + strCellData + "\" type=\"0\"/>");
            }
            stbXML.Append("</cols>");
            stbXML.Append(System.Environment.NewLine);
            stbXML.Append("</tablesignature>");
            stbXML.Append(System.Environment.NewLine);

            return stbXML.ToString();
        }

        /// <summary>
        /// 进修、实习医师(护士)检查
        /// </summary>
        /// <param name="p_intEmpID"></param>
        /// <returns></returns>
        private bool m_blnCheckTraineeFlag(string empID)
        {
            bool blnTraineeFlag = false;
            if (GlobalDic.dicEmpRole.ContainsKey(empID) && GlobalParm.dicSysParameter.ContainsKey(22))
            {
                if (GlobalDic.dicEmpRole[empID].IndexOf(GlobalParm.dicSysParameter[22]) > 0)
                {
                    blnTraineeFlag = true;
                }
            }
            return blnTraineeFlag;
        }

        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <param name="p_lstCaseData"></param>
        /// <param name="p_lstSignature"></param>
        /// <returns></returns>
        public bool m_blnGetTableData(ref List<EntityEmrData> p_lstCaseDataNew, ref List<EntityEmrData> p_lstCaseDataUpdate, ref List<EntitySignature> p_lstSignature, ref List<EntityEmrData> p_lstCaseDataInsert, ref List<EntitySignature> p_lstSignatureInsert, bool p_blnMainDataModiStatus, bool p_blnCaseSaveFlag, DateTime? p_dtmCaseWriteDate, bool isSaveMainInfo)
        {
            //if (string.IsNullOrEmpty(this.RegisterID))
            //{
            //    DialogBox.Msg("请选择病人。", MessageBoxIcon.Exclamation, this.frmParent);
            //    return false;
            //}

            //if (string.IsNullOrEmpty(this.CaseCode))
            //{
            //    DialogBox.Msg("请选择表单。", MessageBoxIcon.Exclamation, this.frmParent);
            //    return false;
            //}

            Control ctlCurrent = this.m_dicControls[CurrentRowNo][CurrentColNo];
            if (ctlCurrent != null)
            {
                if (ctlCurrent is ctlTextBox)
                {
                    if (this.m_lstControl[CurrentColNo].strCtlType == "数字" && !this.m_blnTextValidating((ctlTextBox)ctlCurrent))
                    {
                        return false;
                    }
                }
                else if (ctlCurrent is ctlRichTextBox)
                {
                    if (!this.m_blnRichTextValidating((ctlRichTextBox)ctlCurrent))
                    {
                        return false;
                    }
                }
            }

            if (GlobalPatient.currPatient != null && TableInnerRegisterID == GlobalPatient.currPatient.RegisterID)
            {
                ctlRichTextBox rich = null;
                ctlBasePatientInfoControl patcell = null;
                EntityEmrSelfDefineCol vo = null;
                ProxyEntityFactory proxy = new ProxyEntityFactory();
                foreach (Control ctl2 in this.Parent.Controls)
                {
                    if (ctl2 is ctlRichTextBox)
                    {
                        rich = ctl2 as ctlRichTextBox;
                        if (rich.BandingPage && rich.ValueChangedFlag)
                        {
                            try
                            {
                                vo = new EntityEmrSelfDefineCol();
                                vo.registerId = GlobalPatient.currPatient == null ? null : GlobalPatient.currPatient.RegisterID;
                                vo.caseCode = GlobalCase.caseInfo == null ? null : GlobalCase.caseInfo.CaseCode;
                                vo.colCode = rich.ItemName;
                                vo.colDesc = rich.GetRightText();
                                vo.pageNo = (this.CurrentPageNo <= 0 ? 1 : this.CurrentPageNo);

                                proxy.Service.DeleteByPk(vo);
                                proxy.Service.Insert(vo);
                            }
                            catch (Exception e)
                            {
                                DialogBox.Msg(e.Message);
                            }
                        }
                    }

                    if (ctl2 is ctlBasePatientInfoControl)
                    {
                        patcell = ctl2 as ctlBasePatientInfoControl;
                        if ((CurrentPageNo == 0 || CurrentPageNo == 1) && (GlobalCase.caseInfo != null && GlobalCase.caseInfo.CaseStatus == 0) && patcell.BandingPage && GlobalPatient.currPatient != null)
                        {
                            try
                            {
                                string strColCode = patcell.ItemName;   //.DBColName;
                                string strColValue = string.Empty;

                                if (patcell.InfoType == EnumPatientInfoType.科室)
                                    strColValue = GlobalPatient.currPatient.DeptName;
                                else if (patcell.InfoType == EnumPatientInfoType.病区)
                                    strColValue = GlobalPatient.currPatient.AreaName;
                                else if (patcell.InfoType == EnumPatientInfoType.病床号)
                                    strColValue = GlobalPatient.currPatient.BedNo;

                                vo = new EntityEmrSelfDefineCol();
                                vo.registerId = GlobalPatient.currPatient.RegisterID;
                                vo.caseCode = GlobalCase.caseInfo.CaseCode;
                                vo.colCode = strColCode;
                                vo.colDesc = strColValue;
                                vo.pageNo = (this.CurrentPageNo <= 0 ? 1 : this.CurrentPageNo);

                                proxy.Service.DeleteByPk(vo);
                                proxy.Service.Insert(vo);
                            }
                            catch (Exception e)
                            {
                                DialogBox.Msg(e.Message);
                            }
                        }
                    }
                }
                proxy = null;
            }

            bool blnCASignFlag = (GlobalParm.dicSysParameter.ContainsKey(21) && GlobalParm.dicSysParameter[21] == "1");

            // 0 不操作; 1 新增; 2 更新
            int intOpType = 0;
            int intDBRowIndex = 0;
            int intNewRowIndex = -1;
            EntityEmrData objCaseData = null;
            EntitySignature objSignature = null;
            Control ctl = null;
            clsControl ctlInfo = null;
            IRtfEditor objRtf = null;

            #region Insert
            if (this.m_intInsertRowNo >= 0)
            {
                GlobalParm.FormSignStatusIsSuccess = true;
                if (!this.m_blnCheckRowValid(this.m_intInsertRowNo, p_blnCaseSaveFlag))
                {
                    GlobalParm.FormSignStatusIsSuccess = false;
                    return false;
                }

                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_dicControls[this.m_intInsertRowNo][j];
                    ctlInfo = this.m_lstControl[j];

                    objCaseData = new EntityEmrData();
                    objCaseData.RegisterId = this.RegisterID;
                    objCaseData.CaseCode = this.CaseCode;
                    objCaseData.TableCode = this.TableCode;
                    objCaseData.FieldName = ctlInfo.strFieldName;
                    objCaseData.PrintFlag = 0;
                    objCaseData.ModifierId = this.OperID;

                    if (ctl is IRtfEditor)
                    {
                        objRtf = ctl as IRtfEditor;
                        objCaseData.FieldText = objRtf.GetRightText();
                        objCaseData.FieldMarkXml = objRtf.GetXmlText(p_dtmCaseWriteDate);
                        objCaseData.FieldRtf = objRtf.GetAllRtf();
                        objCaseData.FieldPrtRtf = objRtf.GetPrintRtf();
                        objCaseData.PrintFlag = 1;
                    }
                    else if (ctl is ctlSignature)
                    {
                        objCaseData.FieldText = ((ctlSignature)ctl).Text;
                    }
                    else if (ctl is IFormCtrl) // IDBColProperty)
                    {
                        if (ctl is ICheckBox)
                            objCaseData.FieldText = ((ctlCheckBox)ctl).Checked ? "1" : "0";
                        else
                            objCaseData.FieldText = ctl.Text; //((IDBColProperty)ctl).DBValue;
                    }
                    //if (this.m_intInsertRowNo == this.PageRowCount - 1)
                    //    objCaseData.intRowIndex = int.Parse(this.m_dicControls[this.m_intInsertRowNo - 1][j].Tag.ToString().Split(';')[2]) + 1;
                    //else
                    //    objCaseData.intRowIndex = int.Parse(this.m_dicControls[this.m_intInsertRowNo + 1][j].Tag.ToString().Split(';')[2]);
                    string[] strTagArr = this.m_dicControls[this.m_intInsertRowNo][j].Tag.ToString().Split(';');
                    if (strTagArr.Length >= 3)
                    {
                        objCaseData.RowIndex = int.Parse(strTagArr[2]);
                    }
                    else
                    {
                        if (this.m_intInsertRowNo == 0)
                        {
                            DialogBox.Msg("插入失败，请重新操作。", MessageBoxIcon.Exclamation, this.frmParent);
                            return false;
                        }
                        else
                        {
                            strTagArr = this.m_dicControls[this.m_intInsertRowNo - 1][j].Tag.ToString().Split(';');
                            if (strTagArr.Length >= 3)
                            {
                                objCaseData.RowIndex = int.Parse(strTagArr[2]) + 1;
                            }
                            else
                            {
                                DialogBox.Msg("插入失败，请重新操作。", MessageBoxIcon.Exclamation, this.frmParent);
                                return false;
                            }
                        }
                    }
                    p_lstCaseDataInsert.Add(objCaseData);

                    if (ctl is ctlSignature)
                    {
                        string strSignContent = string.Empty;
                        List<EntitySignature> lstDCSignature = ((ctlSignature)ctl).GetSignEmp();
                        foreach (EntitySignature obj in lstDCSignature)
                        {
                            if (obj.serNo <= 0)
                            {
                                objSignature = new EntitySignature();
                                objSignature.empId = obj.empId;
                                objSignature.empName = obj.empName;
                                objSignature.signDate = obj.signDate;
                                objSignature.techLevelCode = obj.techLevelCode;
                                objSignature.techLevelName = obj.techLevelName;
                                objSignature.signKeyId = obj.signKeyId;
                                if (blnCASignFlag && !this.m_blnCheckTraineeFlag(obj.empId))
                                {
                                    strSignContent = this.m_strRowSignatureContent(this.m_intInsertRowNo);
                                    objSignature.signContent = Compression.Zip(CA.GetSignContent(strSignContent, obj.signKeyId));
                                    if (!string.IsNullOrEmpty(strSignContent) && objSignature.signContent == null)
                                    {
                                        DialogBox.Msg("签名失败。", MessageBoxIcon.Exclamation, this.frmParent);
                                        return false;
                                    }
                                }

                                objSignature.caseCode = this.CaseCode;
                                objSignature.tableCode = this.TableCode;
                                objSignature.objectID = ctlInfo.strFieldName;
                                objSignature.commTypeId = 3;
                                objSignature.registerId = this.RegisterID;
                                objSignature.commId = objCaseData.RowIndex;

                                p_lstSignatureInsert.Add(objSignature);
                            }
                        }
                    }
                }
                if (this.m_blnRowExistsSignCtl && (p_lstSignatureInsert == null || p_lstSignatureInsert.Count == 0))
                {
                    if (this.m_lstNoSignAllowSaveCaseCode.IndexOf(this.CaseCode) >= 0)
                    {

                    }
                    else
                    {
                        if (((ctlSignature)ctl).IsAutoSignature == 1)
                        {
                            EntitySignature dcSign = new EntitySignature();
                            dcSign.empId = GlobalLogin.objLogin.EmpNo;
                            dcSign.empName = GlobalLogin.objLogin.EmpName;
                            dcSign.signDate = DateTime.Now;
                            dcSign.recordDate = DateTime.Now;
                            dcSign.techLevelCode = GlobalLogin.objLogin.TechLevelCode;
                            dcSign.techLevelName = GlobalLogin.objLogin.TechLevelName;
                            dcSign.registerId = GlobalPatient.currPatient.RegisterID;
                            dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                            dcSign.objectID = ((ctlSignature)ctl).ItemName;
                            ((ctlSignature)ctl).AddSignEmp(dcSign);
                        }
                        else
                        {
                            DialogBox.Msg("数据已修改，保存前请签名。", MessageBoxIcon.Exclamation, this.frmParent);
                            return false;
                        }
                    }
                }
                if (this.m_intInsertOrgRowNo < 0)
                {
                    return true;
                }
            }
            #endregion

            for (int i = 0; i < this.PageRowCount; i++)
            {
                if (this.m_intInsertOrgRowNo >= 0)
                {
                    if (i != this.m_intInsertOrgRowNo)
                        continue;
                }

                intOpType = 0;
                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_dicControls[i][j];
                    if (ctl is IFormCtrl)
                    {
                        if (ctl is ctlDatetime)
                        {
                            clsCellData objCellData = this.m_lstCellData.FirstOrDefault(t => t.intRow == i && t.intCol == j);
                            if (objCellData != null && objCellData.strValue.Trim() != ctl.Text.Trim())
                            {
                                if (ctl.Tag != null && ctl.Tag.ToString().Split(';').Length == 3)
                                {
                                    intOpType = 2;
                                    intDBRowIndex = int.Parse(ctl.Tag.ToString().Split(';')[2]);
                                }
                                else
                                {
                                    intOpType = 1;
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (((IFormCtrl)ctl).ValueChangedFlag)
                            {
                                if (ctl.Tag != null && ctl.Tag.ToString().Split(';').Length == 3)
                                {
                                    intOpType = 2;
                                    intDBRowIndex = int.Parse(ctl.Tag.ToString().Split(';')[2]);
                                }
                                else
                                {
                                    intOpType = 1;
                                }
                                break;
                            }
                        }
                    }
                }

                //if (intOpType == 0) continue;                
                if (this.m_intCheckRowStatus(i) == 1)
                {
                    continue;
                }
                else
                {
                    GlobalParm.FormSignStatusIsSuccess = true;
                    if (!this.m_blnCheckRowValid(i, p_blnCaseSaveFlag))
                    {
                        GlobalParm.FormSignStatusIsSuccess = false;
                        return false;
                    }
                }

                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_dicControls[i][j];
                    ctlInfo = this.m_lstControl[j];

                    objCaseData = new EntityEmrData();
                    objCaseData.RegisterId = this.RegisterID;
                    objCaseData.CaseCode = this.CaseCode;
                    objCaseData.TableCode = this.TableCode;
                    objCaseData.FieldName = ctlInfo.strFieldName;
                    objCaseData.PrintFlag = 0;
                    objCaseData.ModifierId = this.OperID;

                    if (ctl is IRtfEditor)
                    {
                        objRtf = ctl as IRtfEditor;
                        objCaseData.FieldText = objRtf.GetRightText();
                        objCaseData.FieldMarkXml = objRtf.GetXmlText(p_dtmCaseWriteDate);
                        objCaseData.FieldRtf = objRtf.GetAllRtf();
                        objCaseData.FieldPrtRtf = objRtf.GetPrintRtf();
                        objCaseData.PrintFlag = 1;
                    }
                    else if (ctl is ctlSignature)
                    {
                        objCaseData.FieldText = ((ctlSignature)ctl).Text;
                    }
                    else if (ctl is IFormCtrl) //IDBColProperty)
                    {
                        if (ctl is ctlCheckBox)
                            objCaseData.FieldText = ((ctlCheckBox)ctl).Checked ? "1" : "0";
                        else
                            objCaseData.FieldText = ctl.Text; //((IDBColProperty)ctl).DBValue;
                    }
                    else
                    {
                        if (ctlInfo.strCtlType == "求和")
                            objCaseData.FieldText = ctl.Text;
                    }

                    if (intOpType == 2)
                    {
                        objCaseData.RowIndex = intDBRowIndex;
                        p_lstCaseDataUpdate.Add(objCaseData);
                    }
                    else
                    {
                        objCaseData.RowIndex = intNewRowIndex;
                        p_lstCaseDataNew.Add(objCaseData);
                    }

                    if (ctl is ctlSignature)
                    {
                        string strSignContent = string.Empty;
                        List<EntitySignature> lstDCSignature = ((ctlSignature)ctl).GetSignEmp();
                        foreach (EntitySignature obj in lstDCSignature)
                        {
                            if (obj.serNo <= 0)
                            {
                                objSignature = new EntitySignature();
                                objSignature.empId = obj.empId;
                                objSignature.empName = obj.empName;
                                objSignature.signDate = obj.signDate;
                                objSignature.techLevelCode = obj.techLevelCode;
                                objSignature.techLevelName = obj.techLevelName;
                                objSignature.signKeyId = obj.signKeyId;
                                if (blnCASignFlag && !this.m_blnCheckTraineeFlag(obj.empId))
                                {
                                    strSignContent = this.m_strRowSignatureContent(i);
                                    objSignature.signContent = Compression.Zip(CA.GetSignContent(strSignContent, obj.signKeyId));
                                    if (!string.IsNullOrEmpty(strSignContent) && objSignature.signContent == null)
                                    {
                                        DialogBox.Msg("签名失败。", MessageBoxIcon.Exclamation, this.frmParent);
                                        return false;
                                    }
                                }

                                objSignature.caseCode = this.CaseCode;
                                objSignature.tableCode = this.TableCode;
                                objSignature.objectID = ctlInfo.strFieldName;
                                objSignature.commTypeId = 3;
                                objSignature.registerId = this.RegisterID;

                                if (intOpType == 2)
                                {
                                    objSignature.commId = intDBRowIndex;
                                }
                                else
                                {
                                    objSignature.commId = intNewRowIndex;
                                }

                                p_lstSignature.Add(objSignature);
                            }
                        }
                    }
                }
                if (intOpType == 1)
                {
                    intNewRowIndex--;
                }
            }

            bool blnNoSignFlag = (p_lstSignature == null || p_lstSignature.Count == 0);
            if (p_lstCaseDataNew.Count == 0 && p_lstCaseDataUpdate.Count == 0 && !p_blnMainDataModiStatus)  // 2017-03-31 && blnNoSignFlag)
            {
                if (blnNoSignFlag)
                {
                    //if (isSaveMainInfo == false)
                    //{
                    //    DialogBox.Msg("数据无更改，不需要保存。", MessageBoxIcon.Exclamation, this.frmParent);
                    //}
                    return false;
                }
            }
            //p_blnSaveFlag true 保存 false 暂存
            if (this.m_blnRowExistsSignCtl && blnNoSignFlag && p_blnCaseSaveFlag)   // 2017-03-31 && blnNoSignFlag)
            {
                if (this.m_lstNoSignAllowSaveCaseCode.IndexOf(this.CaseCode) >= 0 || (p_lstCaseDataNew.Count == 0 && p_lstCaseDataUpdate.Count == 0))
                {

                }
                else
                {
                    if (((ctlSignature)ctl).IsAutoSignature == 1)
                    {
                        EntitySignature dcSign = new EntitySignature();
                        dcSign.empId = GlobalLogin.objLogin.EmpNo;
                        dcSign.empName = GlobalLogin.objLogin.EmpName;
                        dcSign.signDate = DateTime.Now;
                        dcSign.recordDate = DateTime.Now;
                        dcSign.techLevelCode = GlobalLogin.objLogin.TechLevelCode;
                        dcSign.techLevelName = GlobalLogin.objLogin.TechLevelName;
                        dcSign.registerId = GlobalPatient.currPatient.RegisterID;
                        dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                        dcSign.objectID = ((ctlSignature)ctl).ItemName;
                        ((ctlSignature)ctl).AddSignEmp(dcSign);
                    }
                    else
                    {
                        DialogBox.Msg("数据已修改，保存前请签名。", MessageBoxIcon.Exclamation, this.frmParent);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 空值提醒
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="p_intRowNo"></param>
        /// <param name="p_intColNo"></param>
        /// <param name="p_strCaption"></param>
        private void m_mthNullHint(Control ctl, int p_intRowNo, int p_intColNo, string p_strCaption)
        {
            DialogBox.Msg("第" + Convert.ToString(p_intRowNo + 1) + "行 " + Convert.ToString(p_intColNo + 1) + "列 [" + p_strCaption + "]不允许为空。", MessageBoxIcon.Exclamation, this.frmParent);
            ctl.Focus();
        }

        #region 新页
        /// <summary>
        /// 新页
        /// </summary>
        private void m_mthNewPage()
        {
            for (int i = 0; i < this.PageRowCount; i++)
            {
                if (this.m_intCheckRowStatus(i) == 2) //!= 1)
                {
                    if (DialogBox.Msg("新加行前，是否保存未提交的记录？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            int intMaxRowNo = 0;
            int intMaxPageNo = 0;
            int intLastPageRows = 0;

            intMaxRowNo = this.m_intMaxRowIndex();
            intMaxPageNo = (int)Math.Ceiling((double)(intMaxRowNo + 1) / (double)this.PageRowCount);
            intLastPageRows = (intMaxRowNo + 1) % this.PageRowCount;

            this.CurrentPageNo = intMaxPageNo;
            if (intLastPageRows > 0)
            {
                int intRowIndex = intMaxRowNo + 1;
                Control ctl = null;
                clsControl ctlInfo = null;
                EntityEmrData objCaseData = null;
                List<EntityEmrData> lstCaseData = new List<EntityEmrData>();
                ctlRichTextBox rich = new ctlRichTextBox();
                for (int i = 0; i < this.PageRowCount - intLastPageRows; i++)
                {
                    for (int j = 0; j < this.PageColCount; j++)
                    {
                        ctl = this.m_dicControls[0][j];
                        ctlInfo = this.m_lstControl[j];

                        objCaseData = new EntityEmrData();
                        objCaseData.RegisterId = this.RegisterID;
                        objCaseData.CaseCode = this.CaseCode;
                        objCaseData.TableCode = this.TableCode;
                        objCaseData.FieldName = ctlInfo.strFieldName;
                        objCaseData.PrintFlag = 0;
                        objCaseData.ModifierId = this.OperID;

                        if (ctl is IRtfEditor)
                        {
                            objCaseData.FieldText = rich.GetRightText();
                            objCaseData.FieldMarkXml = rich.GetXmlText();
                            objCaseData.FieldRtf = rich.GetAllRtf();
                            objCaseData.FieldPrtRtf = rich.GetPrintRtf();
                            objCaseData.PrintFlag = 1;

                            objCaseData.FieldRtf = Compression.Zip(objCaseData.FieldRtf);
                            objCaseData.FieldPrtRtf = Compression.Zip(objCaseData.FieldPrtRtf);
                        }
                        else if (ctl is ctlSignature)
                        {
                            objCaseData.FieldText = string.Empty;
                        }
                        else if (ctl is IFormCtrl)//    IDBColProperty)
                        {
                            objCaseData.FieldText = string.Empty;
                        }
                        objCaseData.RowIndex = intRowIndex;
                        objCaseData.RecordDate = (this.m_dtmRecordDate == null ? DateTime.Now : this.m_dtmRecordDate.Value);
                        lstCaseData.Add(objCaseData);
                    }
                    intRowIndex++;
                }

                using (ProxyCommon proxy = new ProxyCommon())
                {
                    if( proxy.Service.AppendBlankRow(this.DBTableName, lstCaseData)>0)
                    {
                        this.IsParentContainerReLoadData = true;
                    }
                    else
                    {
                        DialogBox.Msg("保存空行数据失败。", MessageBoxIcon.Information, this.ParentForm);
                        return;
                    }
                }
            }
            using (ProxyCommon proxy = new ProxyCommon())
            {
                proxy.Service.CopySelfDefineCol(this.RegisterID, this.CaseCode, this.CurrentPageNo, this.m_lstGetTabPageBandingPatInfo());
            }

            this.CurrentPageNo += 1;
            this.txtCurrentPage.Text = this.CurrentPageNo.ToString();
            this.txtTotalPage.Text = string.Format("/ {0} 页", this.CurrentPageNo.ToString());

            this.m_mthClearPage();
            this.m_mthRefreshColCaption();
            this.m_dicControls[0][0].Focus();

        }
        #endregion

        #region 获取绑定页宏元素
        /// <summary>
        /// 获取绑定页宏元素
        /// </summary>
        /// <returns></returns>
        private List<EntityCasTablePagePatInfoCell> m_lstGetTabPageBandingPatInfo()
        {
            EntityCasTablePagePatInfoCell obj = null;
            List<EntityCasTablePagePatInfoCell> lstTabPagePatInfo = new List<EntityCasTablePagePatInfoCell>();
            foreach (Control ctl in this.Parent.Controls)
            {
                if (ctl is ctlBasePatientInfoControl)
                {
                    if (((ctlBasePatientInfoControl)ctl).BandingPage && GlobalPatient.currPatient != null)
                    {
                        obj = new EntityCasTablePagePatInfoCell();
                        obj.strDBColCode = ((ctlBasePatientInfoControl)ctl).ItemName;
                        if (((ctlBasePatientInfoControl)ctl).InfoType == EnumPatientInfoType.科室)
                            obj.strDBColDesc = GlobalPatient.currPatient.DeptName;
                        else if (((ctlBasePatientInfoControl)ctl).InfoType == EnumPatientInfoType.病区)
                            obj.strDBColDesc = GlobalPatient.currPatient.AreaName;
                        else if (((ctlBasePatientInfoControl)ctl).InfoType == EnumPatientInfoType.病床号)
                            obj.strDBColDesc = GlobalPatient.currPatient.BedNo;

                        lstTabPagePatInfo.Add(obj);
                    }
                }
                else if (ctl is ctlRichTextBox)
                {
                    if (((ctlRichTextBox)ctl).BandingPage)
                    {
                        obj = new EntityCasTablePagePatInfoCell();
                        obj.strDBColCode = ((ctlRichTextBox)ctl).ItemName;   //DBColName;
                        obj.strDBColDesc = ((ctlRichTextBox)ctl).GetRightText();

                        lstTabPagePatInfo.Add(obj);
                    }
                }
            }
            return lstTabPagePatInfo;
        }
        /// <summary>
        /// 设置绑定页宏元素
        /// </summary>
        private void m_mthSetTabPageBandingPatInfo()
        {
            if (string.IsNullOrEmpty(this.DBTableName)) return;

            EntityCasTablePagePatInfoCell obj = null;
            List<EntityCasTablePagePatInfoCell> lstDBColCode = new List<EntityCasTablePagePatInfoCell>();
            Dictionary<string, ctlBasePatientInfoControl> dicDBColPat = new Dictionary<string, ctlBasePatientInfoControl>();
            Dictionary<string, ctlRichTextBox> dicDBColRich = new Dictionary<string, ctlRichTextBox>();
            foreach (Control ctl in this.Parent.Controls)
            {
                if (ctl is ctlBasePatientInfoControl)
                {
                    if (((ctlBasePatientInfoControl)ctl).BandingPage)
                    {
                        obj = new EntityCasTablePagePatInfoCell();
                        obj.strDBColCode = ((ctlBasePatientInfoControl)ctl).ItemName; //DBColName;
                        lstDBColCode.Add(obj);
                        dicDBColPat.Add(obj.strDBColCode, (ctlBasePatientInfoControl)ctl);
                    }
                }
                else if (ctl is ctlRichTextBox)
                {
                    if (((ctlRichTextBox)ctl).BandingPage)
                    {
                        obj = new EntityCasTablePagePatInfoCell();
                        obj.strDBColCode = ((ctlRichTextBox)ctl).ItemName;//.DBColName;
                        lstDBColCode.Add(obj);
                        dicDBColRich.Add(obj.strDBColCode, (ctlRichTextBox)ctl);
                    }
                }
            }
            if (lstDBColCode.Count > 0)
            {
                ProxyCommon proxy = new ProxyCommon();
                proxy.Service.GetCaseSelfDefineCol(this.RegisterID, this.CaseCode, this.CurrentPageNo, ref lstDBColCode);

                bool blnCheck = true;
                List<EntityCasTablePagePatInfoCell> lstBusiInfo = new List<EntityCasTablePagePatInfoCell>();
                foreach (var item in lstDBColCode)
                {
                    //if (!string.IsNullOrEmpty(item.strDBColDesc))
                    //{
                    //    blnCheck = true;
                    //    break;
                    //}
                    if (item.strDBColDesc == null)
                    {
                        blnCheck = false;
                        break;
                    }
                }
                if (!blnCheck && !string.IsNullOrEmpty(this.DBTableName))
                {
                    lstBusiInfo.AddRange(lstDBColCode);
                    proxy.Service.GetCaseSelfDefineCol(this.RegisterID, this.DBTableName, ref lstBusiInfo);
                }
                proxy = null;

                foreach (var item in lstDBColCode)
                {
                    if (dicDBColPat.ContainsKey(item.strDBColCode))
                    {
                        if (string.IsNullOrEmpty(item.strDBColDesc))
                        {
                            EntityCasTablePagePatInfoCell objBusi = lstBusiInfo.SingleOrDefault(t => t.strDBColCode == item.strDBColCode);
                            if (objBusi != null && !string.IsNullOrEmpty(objBusi.strDBColDesc))
                            {
                                //防止： 1-10页没记录，11页为：科室A， 12页为科室B，从第11页直接到第1页 和 从第12页直接到第1页显示值不一样。 统一显示为当前业务表记录科室。
                                if (objBusi.strDBColDesc.Contains(dicDBColPat[item.strDBColCode].InfoType.ToString()) || (!string.IsNullOrEmpty(dicDBColPat[item.strDBColCode].CaptionText) && objBusi.strDBColDesc.Contains(dicDBColPat[item.strDBColCode].CaptionText)) || objBusi.strDBColDesc.Contains("："))
                                {
                                    dicDBColPat[item.strDBColCode].Text = objBusi.strDBColDesc;
                                }
                                else
                                {
                                    if (dicDBColPat[item.strDBColCode].ShowCaption)
                                    {
                                        dicDBColPat[item.strDBColCode].Text = dicDBColPat[item.strDBColCode].CaptionText + objBusi.strDBColDesc;
                                    }
                                    else
                                    {
                                        dicDBColPat[item.strDBColCode].Text = dicDBColPat[item.strDBColCode].InfoType.ToString() + "：" + objBusi.strDBColDesc;
                                    }
                                }
                            }
                            else
                            {
                                dicDBColPat[item.strDBColCode].RefreshData();
                            }
                        }
                        else
                        {
                            if (dicDBColPat[item.strDBColCode].ShowCaption)
                            {
                                dicDBColPat[item.strDBColCode].Text = dicDBColPat[item.strDBColCode].CaptionText + item.strDBColDesc;
                            }
                            else
                            {
                                dicDBColPat[item.strDBColCode].Text = dicDBColPat[item.strDBColCode].InfoType.ToString() + "：" + item.strDBColDesc;
                            }
                        }
                    }
                    else if (dicDBColRich.ContainsKey(item.strDBColCode))
                    {
                        if (!string.IsNullOrEmpty(item.strDBColDesc))
                        {
                            dicDBColRich[item.strDBColCode].Text = item.strDBColDesc;
                        }
                    }
                }
            }
        }
        #endregion

        #region 新行
        /// <summary>
        /// 新行
        /// </summary>
        private void m_mthNewRow()
        {
            for (int i = 0; i < this.PageRowCount; i++)
            {
                if (this.m_intCheckRowStatus(i) == 2) //!= 1)
                {
                    if (DialogBox.Msg("新加行前，是否保存未提交的记录？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (DialogBox.Msg("插入新列会影响后页已填写内容,是否继续？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.No)
            {
                return;
            }

            int intMaxRowNo = 0;
            int intMaxPageNo = 0;
            int intLastPageRows = 0;

            intMaxRowNo = this.m_intMaxRowIndex();
            intMaxPageNo = (int)Math.Ceiling((double)(intMaxRowNo + 1) / (double)this.PageRowCount);
            intLastPageRows = (intMaxRowNo + 1) % this.PageRowCount;

            this.CurrentPageNo = intMaxPageNo;
            if (intLastPageRows > 0)
            {
                this.m_mthLoadData(-1);
                this.m_dicControls[intLastPageRows][0].Focus();
            }
            else
            {
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    proxy.Service.CopySelfDefineCol(this.RegisterID, this.CaseCode, this.CurrentPageNo, this.m_lstGetTabPageBandingPatInfo());
                }

                this.CurrentPageNo += 1;
                this.txtCurrentPage.Text = this.CurrentPageNo.ToString();
                this.txtTotalPage.Text = string.Format("/ {0} 页", this.CurrentPageNo.ToString());

                this.m_mthClearPage();
                this.m_mthRefreshColCaption();
                this.m_dicControls[0][0].Focus();
            }
        }
        #endregion

        #region 插行
        /// <summary>
        /// 插行
        /// </summary>
        public void m_mthInsertRow()
        {
            m_blnInsertRow(true);
        }

        /// <summary>
        /// 插行
        /// </summary>
        private bool m_blnInsertRow(bool p_blnIsUp)
        {
            string strTag = this.m_dicControls[CurrentRowNo][CurrentColNo].Tag.ToString();
            if (strTag.Split(';').Length != 3)
            {
                DialogBox.Msg("不能自动换行，需保存该行记录。", MessageBoxIcon.Exclamation, this.frmParent);
                return false;
            }

            if (this.m_intInsertRowNo >= 0)
            {
                DialogBox.Msg("插入新行前，请保存当前记录。", MessageBoxIcon.Exclamation, this.frmParent);
                return false;
            }

            for (int i = 0; i < this.PageRowCount; i++)
            {
                if (this.m_intCheckRowStatus(i) == 2) //!= 1)
                {
                    DialogBox.Msg("插入新行前，请保存当前记录。", MessageBoxIcon.Exclamation, this.frmParent);
                    return false;
                }
            }

            if (DialogBox.Msg("插入新列会影响后页已填写内容,是否继续？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.No)
            {
                return false;
            }

            bool b = false;
            List<string> lstTag = new List<string>();
            for (int i = this.PageRowCount - 1; i >= 0; i--)
            {
                b = true;
                if (p_blnIsUp)
                {
                    if (i < CurrentRowNo)
                        break;
                    else if (i == CurrentRowNo)
                    {
                        foreach (Control item in this.m_dicControls[i])
                        {
                            lstTag.Add(((Control)item).Tag.ToString());
                        }
                        this.m_mthClearRow(i);
                        for (int j = 0; j < this.PageColCount; j++)
                        {
                            this.m_dicControls[i][j].Tag = lstTag[j];
                        }
                        b = false;
                    }
                }
                else
                {
                    if (i <= CurrentRowNo)
                        break;
                    else if (i == CurrentRowNo + 1)
                    {
                        foreach (Control item in this.m_dicControls[i])
                        {
                            lstTag.Add(((Control)item).Tag.ToString());
                        }
                        this.m_mthClearRow(i);
                        for (int j = 0; j < this.PageColCount; j++)
                        {
                            this.m_dicControls[i][j].Tag = lstTag[j];
                        }
                        b = false;
                    }
                }
                if (b)
                {
                    for (int j = 0; j < this.PageColCount; j++)
                    {
                        if (this.m_dicControls[i][j] is ctlSignature)
                        {
                            ((ctlSignature)this.m_dicControls[i][j]).ClearText();
                            ((ctlSignature)this.m_dicControls[i][j]).Text = ((ctlSignature)this.m_dicControls[i - 1][j]).Text;
                            //((ctlSignature)this.m_dicControls[i][j]).AddSignEmp(((ctlSignature)this.m_dicControls[i - 1][j]).GetSignEmp());
                        }
                        //else if (this.m_dicControls[i][j] is ctlRichTextBox)
                        //{
                        //    ((ctlRichTextBox)this.m_dicControls[i][j]).SetXmlText(((ctlRichTextBox)this.m_dicControls[i - 1][j]).GetAllRtf(), ((ctlRichTextBox)this.m_dicControls[i - 1][j]).GetXmlText(), true);
                        //}
                        //else if (this.m_dicControls[i][j] is IDBColProperty)
                        //    ((IDBColProperty)this.m_dicControls[i][j]).DBValue = ((IDBColProperty)this.m_dicControls[i - 1][j]).DBValue;
                        else if (this.m_dicControls[i][j] is ctlCheckBox)
                            ((ctlCheckBox)this.m_dicControls[i][j]).Checked = ((ctlCheckBox)this.m_dicControls[i - 1][j]).Checked;
                        else if (this.m_dicControls[i][j] is ctlDatetime)
                        {
                            if (((ctlDatetime)this.m_dicControls[i - 1][j]).DateTime.ToString("yyyy-MM-dd") == "0001-01-01")
                                ((ctlDatetime)this.m_dicControls[i][j]).EditValue = null;
                            else
                                this.m_dicControls[i][j].Text = this.m_dicControls[i - 1][j].Text;
                        }
                        else
                            this.m_dicControls[i][j].Text = this.m_dicControls[i - 1][j].Text;
                        this.m_dicControls[i][j].Tag = this.m_dicControls[i - 1][j].Tag;
                    }
                }
            }

            if (p_blnIsUp)
                this.m_intInsertRowNo = CurrentRowNo;
            else
                this.m_intInsertRowNo = CurrentRowNo + 1;

            return true;
        }
        #endregion

        #region 删行
        /// <summary>
        /// 删行
        /// </summary>
        internal void m_mthDelRow()
        {
            int intNums = 0;
            Control ctl = null;
            List<int> lstSignIndex = new List<int>();
            for (int i = 0; i < this.PageRowCount; i++)
            {
                for (int j = 0; j < this.PageColCount; j++)
                {
                    ctl = this.m_dicControls[i][j];
                    if (ctl is ctlSignature)
                    {
                        if (!string.IsNullOrEmpty(((ctlSignature)ctl).Text) && ((ctlSignature)ctl).m_lstNoSaveSignature.Count == 0)
                        {
                            if (lstSignIndex.IndexOf(i) < 0) lstSignIndex.Add(i);
                            intNums++;
                            continue;
                        }
                    }
                }
            }

            if (lstSignIndex.IndexOf(CurrentRowNo) >= 0 || (GlobalCase.caseInfo != null && (GlobalCase.caseInfo.CaseStatus == 1 || GlobalCase.caseInfo.CaseStatus == 2)))
            {
                if (DialogBox.Msg("是否删除当前行？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.No)
                {
                    return;
                }

                if (DialogBox.Msg("删除列会影响后页已填写内容,是否继续？", MessageBoxIcon.Question, this.ParentForm) == DialogResult.No)
                {
                    return;
                }

                bool blnStatus = false;
                if (GlobalParm.dicSysParameter.ContainsKey(25) && !string.IsNullOrEmpty(GlobalParm.dicSysParameter[25]))
                {
                    if (GlobalLogin.objLogin.lstRoleID.IndexOf(GlobalParm.dicSysParameter[46]) >= 0)
                    {
                        frmConfirm frm = new frmConfirm();
                        frm.IsReadOnly = true;
                        if (frm.ShowDialog(this.FindForm()) == DialogResult.OK)
                        {
                            blnStatus = true;
                        }
                    }
                }
                else
                {
                    //DialogBox.Msg("请维护参数: 46 . \r\n指定删除行记录角色.", MessageBoxIcon.Exclamation, this.frmParent);
                    //return;
                }

                if (!blnStatus)
                {
                    //DialogBox.Msg("对不起，你现在无权限删除表格记录。", MessageBoxIcon.Exclamation, this.frmParent);
                    //return;
                }
                if (this.m_dicControls[CurrentRowNo][0].Tag != null && !string.IsNullOrEmpty(this.m_dicControls[CurrentRowNo][0].Tag.ToString()))
                {
                    string strTag = this.m_dicControls[CurrentRowNo][0].Tag.ToString();
                    if (strTag.Split(';').Length >= 3)
                    {
                        using (ProxyCommon proxy = new ProxyCommon())
                        {
                            int intRet = proxy.Service.DelTableRowCase(this.RegisterID, this.DBTableName, this.CaseCode, this.TableCode, int.Parse(strTag.Split(';')[2]), this.m_dtmRecordDate);
                            if (intRet > 0)
                            {
                                //EntitySysLog log = new clsEntitySysLog();
                                //log.Deptid_int = (decimal)clsGlobalLoginInfo.objLoginInfo.intDeptID;
                                //log.Deptname_vchr = clsGlobalLoginInfo.objLoginInfo.strDeptName;
                                //log.Empid_int = decimal.Parse(clsGlobalLoginInfo.objLoginInfo.strEmpId);
                                //log.Empname_vchr = clsGlobalLoginInfo.objLoginInfo.strEmpName;
                                //log.Ipaddr_vchr = clsFunction.s_strLocalIP();
                                //log.Macaddr_vchr = clsFunction.s_strLocalMac();
                                //log.Machinename_vchr = clsFunction.s_strLocalHostName();
                                //log.Recorddate_dat = clsHelper.s_dtmMidderTime();
                                //if (frmParent != null)
                                //    log.Opercontent_vchr = "窗体名:" + frmParent.Text;

                                //if (clsGlobalCase.objCaseInfo != null)
                                //{
                                //    log.Opercontent_vchr += ";  表格名:" + clsGlobalCase.objCaseInfo.strCaseName;
                                //}
                                //log.Opername_vchr = "casetabledelete";

                                //clsProxyEntityFactory cls = new clsProxyEntityFactory();
                                //cls.Service.m_Insert(new clsEntityFactory(log));
                                //cls = null;
                                this.IsParentContainerReLoadData = true;
                                DialogBox.Msg("删除数据成功。", MessageBoxIcon.Exclamation, this.frmParent);
                            }
                            else
                            {
                                DialogBox.Msg("删除数据失败。", MessageBoxIcon.Exclamation, this.frmParent);
                            }
                        }
                    }
                }
            }
            if (intNums == 0)
                this.m_mthLoadData(CurrentPageNo - 1);
            else
                this.m_mthLoadData(CurrentPageNo);

        }
        #endregion

        #endregion

        #region 翻页
        /// <summary>
        /// 换页标志
        /// </summary>
        bool ChangePageFlag { get; set; }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.ChangePageFlag = true;
            this.m_mthLoadData(1);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int intPageNo = 1;
            try
            {
                this.ChangePageFlag = true;
                intPageNo = int.Parse(txtCurrentPage.Text) - 1;
            }
            catch { }
            this.m_mthLoadData(intPageNo);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int intPageNo = 1;
            try
            {
                this.ChangePageFlag = true;
                intPageNo = int.Parse(txtCurrentPage.Text) + 1;
            }
            catch { }
            this.m_mthLoadData(intPageNo);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.ChangePageFlag = true;
            this.m_mthLoadData(-1);
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 扩展
        /// </summary>
        public void m_mthGrow()
        {
            int intDiffHeight = this.Height - this.m_intOldHeight;

            if (this.Parent != null && intDiffHeight != 0)
            {
                foreach (Control ctl in this.Parent.Controls)
                {
                    if (ctl.Top > this.Top)
                    {
                        ctl.Top += intDiffHeight;
                    }
                }
            }
        }
        #endregion

        #region 窗体.按钮事件

        private bool m_blnCheckRowSignCtl()
        {
            foreach (clsControl ctl in this.m_lstControl)
            {
                if (ctl.strCtlType == "签名")
                    return true;
            }
            return false;
        }

        private void ctlTableCase_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.DBColList = new List<IFormCtrl>();
                this.m_intOldHeight = this.Height;
                //获取父窗体
                Form frm = this.FindForm();
                if (frm != null && frm is frmBaseMdi)
                {
                    frmParent = (frmBaseMdi)frm;
                }
                this.m_mthLoadDefineInfo();
                this.backgroundWorker.RunWorkerAsync();
                this.m_blnRowExistsSignCtl = this.m_blnCheckRowSignCtl();

                if (GlobalParm.dicSysParameter.ContainsKey(23))
                {
                    string strP23 = GlobalParm.dicSysParameter[23];
                    this.m_lstNoSignAllowSaveCaseCode = strP23.Split(';').ToList();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
        }

        private void tsiNewPage_Click(object sender, EventArgs e)
        {
            this.m_mthNewPage();
        }

        private void tsiAppendRow_Click(object sender, EventArgs e)
        {
            this.m_mthNewRow();
        }

        private void tsiInsertRow_Click(object sender, EventArgs e)
        {
            this.m_mthInsertRow();
        }

        private void tsiInsertLine_Click(object sender, EventArgs e)
        {
            //this.m_mthInsertRow();
        }

        private void tsiAppendLine_Click(object sender, EventArgs e)
        {
            //this.m_mthNewRow();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.m_mthDelRow();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.m_mthResetRichText();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlMain.ResumeLayout();
        }

        private void contextMenuStripSum_Opening(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip obj = sender as System.Windows.Forms.ContextMenuStrip;
            string strTag = obj.SourceControl.Tag.ToString();
            if (!string.IsNullOrEmpty(strTag) && strTag.Split(';').Length >= 2)
            {
                CurrentRowNo = int.Parse(strTag.Split(';')[0]);
                CurrentColNo = int.Parse(strTag.Split(';')[1]);
            }
        }

        private void tsmiTwoRowsSum_Click(object sender, EventArgs e)
        {
            if (CurrentRowNo > 0)
            {
                decimal decVal = 0;
                string strVal = this.m_dicControls[CurrentRowNo - 1][CurrentColNo].Text;
                decimal.TryParse(strVal, out decVal);
                this.m_dicControls[CurrentRowNo][CurrentColNo].Text = Convert.ToString(decVal + m_decComputeCellSum(CurrentRowNo + 1, CurrentRowNo + 1));
            }
        }

        private void tsmiSum_Click(object sender, EventArgs e)
        {
            frmSetSumRowScopes frm = new frmSetSumRowScopes();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.m_intSelectedType == 1)
                {
                    int intStartNo = -1;
                    int intEndNo = CurrentRowNo;

                    DateTime dtmComp1 = DateTime.Now;
                    DateTime dtmComp2 = DateTime.Now;

                    string strDate1 = frm.m_strTime;
                    DateTime.TryParse(strDate1, out dtmComp1);

                    string strDate2 = string.Empty;
                    for (int i = CurrentRowNo; i >= 0; i--)
                    {
                        strDate2 = this.m_dicControls[i][frm.m_intColNo].Text;
                        if (!string.IsNullOrEmpty(strDate2))
                        {
                            DateTime.TryParse(strDate2, out dtmComp2);
                            if (dtmComp2 <= dtmComp1)
                            {
                                intStartNo = i;
                                break;
                            }
                        }
                    }
                    decimal decSumValue = 0;
                    decimal decTempValue = 0;
                    if (intStartNo == -1)
                    {
                        int intPageEndRowNo = 0;
                        if (this.CurrentPageNo > 1) intPageEndRowNo = (this.CurrentPageNo - 1) * this.PageRowCount - 1;
                        using (ProxyCommon proxy = new ProxyCommon())
                        {
                            proxy.Service.GetTableTimePointValue(this.RegisterID, this.DBTableName, this.TableCode, this.m_lstControl[frm.m_intColNo - 1].strFieldName, m_strGetComputeCols(), intPageEndRowNo, frm.m_strTime, ref decTempValue);
                        }
                        intStartNo = 0;
                    }
                    decSumValue = m_decComputeCellSum(intStartNo, intEndNo) + decTempValue;
                    this.m_dicControls[CurrentRowNo][CurrentColNo].Text = decSumValue.ToString();
                }
                else if (frm.m_intSelectedType == 2)
                {
                    this.m_dicControls[CurrentRowNo][CurrentColNo].Text = m_decComputeCellSum(frm.m_intStartRowNo, frm.m_intEndRowNo).ToString();
                }
                else if (frm.m_intSelectedType == 3)
                {
                    this.m_dicControls[CurrentRowNo][CurrentColNo].Text = frm.m_strSumValue;
                }
            }
        }

        private void pnlTooBar_MouseEnter(object sender, EventArgs e)
        {
            foreach (Control ctl in pnlTooBar.Controls)
            {
                if (ctl is ctlPicbutton)
                {
                    //((ctlPicbutton)ctl).m_mthLeave();
                }
            }
        }

        private void txtPageNo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageNo.Text))
            {
                txtPageNo.Text = "1";
            }
        }

        private void txtPageNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strPageNo = txtPageNo.Text;
                int intPageNo = 0;
                int.TryParse(strPageNo, out intPageNo);
                if (intPageNo > 0)
                {
                    int intNo = 0;
                    string strNo = this.txtTotalPage.Text.Replace("/", "").Replace("页", "");
                    int.TryParse(strNo, out intNo);
                    if (intNo > 0 && intPageNo > intNo)
                    {
                        DialogBox.Msg("页号不能大于记录的最大页。");
                        return;
                    }
                    this.ChangePageFlag = true;
                    this.m_mthLoadData(intPageNo);
                }
                else
                {
                    DialogBox.Msg("页号必须大于0");
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (this.txtPageNo.Text == "0")
                {
                    txtPageNo.Text = "1";
                }
            }
        }
        #endregion

        #region 批量签名
        /// <summary>
        /// 批量签名
        /// </summary>
        public void BatchSign()
        {
            Dictionary<string, int> dicSignCol = new Dictionary<string, int>();
            for (int i = 0; i < this.m_lstControl.Count; i++)
            {
                if (this.m_lstControl[i].strCtlType == "签名" && !dicSignCol.ContainsKey(this.m_lstControl[i].strFieldCaption))
                {
                    dicSignCol.Add(this.m_lstControl[i].strFieldCaption, i);
                }
            }
            if (dicSignCol.Values.Count > 0)
            {
                frmTableBatchSign frm = new frmTableBatchSign();
                frm.MaxRowNo = PageRowCount;
                frm.SignColNameIn = dicSignCol.Keys.ToList();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int intColNo = dicSignCol[frm.SignColNameOut];
                    for (int i = frm.FromRowNo - 1; i < frm.ToRowNo; i++)
                    {
                        if (this.m_dicControls[i][intColNo] is ctlSignature)
                        {
                            ((ctlSignature)this.m_dicControls[i][intColNo]).AddSignEmp(frm.SignEmpInfo);
                            ((ctlSignature)this.m_dicControls[i][intColNo]).m_lstNoSaveSignature.AddRange(frm.SignEmpInfo.ToArray());
                            ((ctlSignature)this.m_dicControls[i][intColNo]).ValueChangedFlag = true;
                        }
                        else
                        {
                            DialogBox.Msg("指定列不是签名列，请重新选择。", MessageBoxIcon.Information, this.ParentForm);
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region ReLoadPage
        /// <summary>
        /// ReLoadPage
        /// </summary>
        public void ReLoadPage()
        {
            this.m_mthLoadData(this.CurrentPageNo);
        }
        #endregion
    }
}
