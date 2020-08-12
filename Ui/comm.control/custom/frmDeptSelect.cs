using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 选择科室
    /// </summary>
    public partial class frmDeptSelect : System.Windows.Forms.Form
    {
        #region 变量

        /// <summary>
        /// 科室列表
        /// </summary>
        private List<EntityCodeDepartment> lstDept;

        /// <summary>
        /// 返回科室列表
        /// </summary>
        public List<EntityCodeDepartment> lstResult { get; set; }

        /// <summary>
        /// 已选科室id
        /// </summary>
        private List<string> SelectedDeptid;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_selectedId"></param>
        public frmDeptSelect(List<string> p_selectedId)
        {
            InitializeComponent();
            this.lstResult = new List<EntityCodeDepartment>();
            this.SelectedDeptid = p_selectedId;
        }

        #endregion

        #region load事件

        /// <summary>
        /// loading事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExcludeEdit_Load(object sender, EventArgs e)
        {
            //初始化数据
            InitData();
            //初始化已选
            txtDepart.Focus();
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            lstDept = new List<EntityCodeDepartment>();
            try
            {
                // 查询科室
                lstDept.AddRange(GlobalDic.DataSourceDepartment);//.FindAll(t => Function.Int(t.Status) == 1));
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
                return;
            }

            tlAllDept.DataSource = lstDept;
            tlAllDept.ExpandAll();
            // 初始化已选科室
            InitSelectedData();
        }

        #endregion

        #region 初始化已选

        /// <summary>
        /// 初始化已选科室
        /// </summary>
        private void InitSelectedData()
        {
            for (int i = 0; i < SelectedDeptid.Count; i++)
            {
                AddDept(SelectedDeptid[i]); //添加科室
            }
            //绑定数据源
            tlSelDept.DataSource = lstResult;
            tlSelDept.ExpandAll();
        }

        #endregion

        #region 查询科室

        #region 查询框回车定位

        /// <summary>
        /// 查询框回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtDepart.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                //定位科室
                DeptQuery();
            }
        }

        #endregion

        #region 定位查询的科室

        /// <summary>
        /// 定位查询的科室
        /// </summary>
        private void DeptQuery()
        {
            if (txtDepart.Text.Trim() == "")
            {
                return;
            }
            TreeListNode node = null;
            if (tlAllDept.FocusedNode != null)
            {
                //查询子节点
                if (tlAllDept.FocusedNode.Nodes.Count > 0)
                {
                    node = GetNode(tlAllDept.FocusedNode.Nodes[0]);
                }
                //查询下一个节点
                if (node != null)
                {
                    tlAllDept.SetFocusedNode(node);
                }
                else if (tlAllDept.FocusedNode.NextNode != null)
                {
                    node = GetNode(tlAllDept.FocusedNode.NextNode);
                }
                //查询父节点
                if (node != null)
                {
                    tlAllDept.SetFocusedNode(node);
                }
                else
                {
                    TreeListNode parentNode = tlAllDept.FocusedNode.ParentNode;
                    while (parentNode != null && node == null)
                    {
                        if (parentNode.NextNode != null)
                        {
                            node = GetNode(parentNode.NextNode);
                        }
                        parentNode = parentNode.ParentNode;
                    }
                    if (node != null)
                    {
                        tlAllDept.SetFocusedNode(node);
                    }
                }
            }
            if (node == null)
            {
                //遍历树
                if (tlAllDept.Nodes.Count > 0)
                {
                    node = GetNode(tlAllDept.Nodes[0]);
                    if (node != null)
                    {
                        tlAllDept.SetFocusedNode(node);
                    }
                }
            }
        }

        #endregion

        #region 遍历树

        /// <summary>
        /// 遍历树
        /// </summary>
        /// <param name="p_node">根节点</param>
        /// <returns>符合的节点</returns>
        private TreeListNode GetNode(TreeListNode p_node)
        {
            List<TreeListNode> lstNode = new List<TreeListNode>();
            lstNode.Add(p_node); //添加第一个节点
            int isFind = -1;
            string strCondition = txtDepart.Text.Trim().ToLower();
            for (int i = 0; i < lstNode.Count; i++)
            {
                //判断条件
                if (lstDept[lstNode[i].Id].deptName.ToLower().Contains(strCondition) ||
                     lstDept[lstNode[i].Id].deptCode.ToLower().Contains(strCondition) ||
                     lstDept[lstNode[i].Id].pyCode.ToLower().Contains(strCondition) ||
                     lstDept[lstNode[i].Id].wbCode.ToLower().Contains(strCondition))
                {
                    isFind = i;
                    break;
                }
                else
                {
                    int count = 1;
                    if (lstNode[i].Nodes.Count > 0) //添加第一个子节点
                    {
                        lstNode.Insert(i + 1, lstNode[i].Nodes[0]);
                        count++;
                    }
                    if (lstNode[i].NextNode != null) //添加下一个节点
                    {
                        lstNode.Insert(i + count, lstNode[i].NextNode);
                    }
                }
            }
            if (isFind > -1)
            {
                return lstNode[isFind];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region 确定按钮

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //剔除非叶子节点的科室
            for (int i = lstResult.Count - 1; i >= 0; i--)
            {
                if (lstResult[i].leafFlag == null || lstResult[i].leafFlag != "T")
                {
                    lstResult.RemoveAt(i);
                }
            }
            if (lstResult.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DialogBox.Msg("请选择科室.", MessageBoxIcon.Information);
            }
        }

        #endregion

        #region 取消按钮

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 添加移除科室

        #region 添加科室按钮

        /// <summary>
        /// 添加科室按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToleft_Click(object sender, EventArgs e)
        {
            if (tlAllDept.Selection.Count > 0) //判断是否选择了科室
            {
                foreach (TreeListNode node in tlAllDept.Selection) //添加选择了的科室
                {
                    if (lstDept[node.Id].leafFlag != null && lstDept[node.Id].leafFlag == "T") //判断是否底层
                    {
                        if (node.Tag == null) //判断是否已经选择
                        {
                            AddDept(lstDept[node.Id].deptCode); //添加科室
                            tlSelDept.RefreshDataSource();
                            tlSelDept.ExpandAll();
                        }
                    }
                }
            }
        }

        #endregion

        #region 移除科室按钮

        /// <summary>
        /// 移除科室按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToright_Click(object sender, EventArgs e)
        {
            if (tlSelDept.Selection.Count > 0) //判断是否选择了科室
            {
                List<string> lstDeptid = new List<string>();
                foreach (TreeListNode node in tlSelDept.Selection) //添加选择了的科室
                {
                    if (lstResult[node.Id].leafFlag != null && lstResult[node.Id].leafFlag == "T") //判断是否底层
                    {
                        lstDeptid.Add(lstResult[node.Id].deptCode); //添加需要删除的科室
                    }
                }
                for (int i = 0; i < lstDeptid.Count; i++)
                {
                    //移除
                    RemoveDept(lstDeptid[i]);
                    //刷新数据源
                    tlSelDept.RefreshDataSource();
                    tlSelDept.ExpandAll();
                }
            }
        }

        #endregion

        #region 添加科室

        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="p_id">科室id</param>
        private void AddDept(string p_id)
        {
            //判断是否已经包含科室
            if ((from a in lstResult where a.deptCode == p_id select a).Count() > 0)
            {
                return;
            }
            //添加科室到已选
            IEnumerable<EntityCodeDepartment> lstTemp = (from a in lstDept where a.deptCode == p_id select a);
            if (lstTemp.Count() > 0)
            {
                EntityCodeDepartment info = lstTemp.First();
                lstResult.Add(info);
                TreeListNode node = tlAllDept.FindNodeByID(lstDept.IndexOf(lstTemp.First()));
                if (node != null && info.leafFlag == "T")
                {
                    node.ImageIndex = 1;
                    node.SelectImageIndex = 1;
                    node.Tag = 0;
                }
                string parentid = lstTemp.First().parent;
                if (!string.IsNullOrEmpty(parentid))
                {
                    AddDept(parentid);
                }
            }
        }

        #endregion

        #region 移除科室

        /// <summary>
        /// 移除科室
        /// </summary>
        /// <param name="p_id">科室id</param>
        private void RemoveDept(string p_id)
        {
            //获取科室 
            IEnumerable<EntityCodeDepartment> lstTemp = (from a in lstResult where a.deptCode == p_id select a);
            if (lstTemp.Count() > 0)
            {
                EntityCodeDepartment info = lstTemp.First();
                if (info.leafFlag != null && info.leafFlag == "T") //判断是否子节点
                {
                    //更改已选标志
                    int nodeId = lstDept.IndexOf(info);
                    TreeListNode rightNode = tlAllDept.FindNodeByID(nodeId);
                    rightNode.ImageIndex = 0;
                    rightNode.SelectImageIndex = 0;
                    rightNode.Tag = null;
                }
                //移除科室
                if ((from a in lstResult where a.parent == p_id select a).Count() == 0) //判断是否没有子节点
                {
                    lstResult.Remove(info);
                    if (!string.IsNullOrEmpty(info.parent))
                    {
                        RemoveDept(info.parent);
                    }
                }
            }
        }

        #endregion

        #region 双击添加科室

        /// <summary>
        /// 全部科室双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlAllDept_DoubleClick(object sender, EventArgs e)
        {
            //获取当前节点
            TreeListHitInfo hitInfo = tlAllDept.CalcHitInfo(tlAllDept.PointToClient(Cursor.Position));
            TreeListNode node = hitInfo.Node;
            if (node != null && node.Tag == null) //判断是否选择了节点 科室是否被选中了
            {
                //判断是否子节点
                if (lstDept[node.Id].leafFlag != null && lstDept[node.Id].leafFlag == "T")
                {
                    AddDept(lstDept[node.Id].deptCode); //添加科室
                    //刷新
                    tlSelDept.RefreshDataSource();
                    tlSelDept.ExpandAll();
                }
            }
        }

        #endregion

        #region 双击移除科室

        /// <summary>
        /// 已选科室双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlSelDept_DoubleClick(object sender, EventArgs e)
        {
            //获取当前节点
            TreeListHitInfo hitInfo = tlSelDept.CalcHitInfo(tlSelDept.PointToClient(Cursor.Position));
            TreeListNode node = hitInfo.Node;
            if (node != null && node.Tag == null) //判断是否选择了节点 科室是否被选中了
            {
                //判断是否子节点
                if (lstResult[node.Id].leafFlag != null && lstResult[node.Id].leafFlag == "T")
                {
                    RemoveDept(lstResult[node.Id].deptCode); //添加科室
                    //刷新
                    tlSelDept.RefreshDataSource();
                    tlSelDept.ExpandAll();
                }
            }
        }

        #endregion

        private void frmDeptSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion
    }
}