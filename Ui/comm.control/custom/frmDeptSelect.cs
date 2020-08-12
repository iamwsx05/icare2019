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
    /// ѡ�����
    /// </summary>
    public partial class frmDeptSelect : System.Windows.Forms.Form
    {
        #region ����

        /// <summary>
        /// �����б�
        /// </summary>
        private List<EntityCodeDepartment> lstDept;

        /// <summary>
        /// ���ؿ����б�
        /// </summary>
        public List<EntityCodeDepartment> lstResult { get; set; }

        /// <summary>
        /// ��ѡ����id
        /// </summary>
        private List<string> SelectedDeptid;

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_selectedId"></param>
        public frmDeptSelect(List<string> p_selectedId)
        {
            InitializeComponent();
            this.lstResult = new List<EntityCodeDepartment>();
            this.SelectedDeptid = p_selectedId;
        }

        #endregion

        #region load�¼�

        /// <summary>
        /// loading�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExcludeEdit_Load(object sender, EventArgs e)
        {
            //��ʼ������
            InitData();
            //��ʼ����ѡ
            txtDepart.Focus();
        }

        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitData()
        {
            lstDept = new List<EntityCodeDepartment>();
            try
            {
                // ��ѯ����
                lstDept.AddRange(GlobalDic.DataSourceDepartment);//.FindAll(t => Function.Int(t.Status) == 1));
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
                return;
            }

            tlAllDept.DataSource = lstDept;
            tlAllDept.ExpandAll();
            // ��ʼ����ѡ����
            InitSelectedData();
        }

        #endregion

        #region ��ʼ����ѡ

        /// <summary>
        /// ��ʼ����ѡ����
        /// </summary>
        private void InitSelectedData()
        {
            for (int i = 0; i < SelectedDeptid.Count; i++)
            {
                AddDept(SelectedDeptid[i]); //��ӿ���
            }
            //������Դ
            tlSelDept.DataSource = lstResult;
            tlSelDept.ExpandAll();
        }

        #endregion

        #region ��ѯ����

        #region ��ѯ��س���λ

        /// <summary>
        /// ��ѯ��س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtDepart.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                //��λ����
                DeptQuery();
            }
        }

        #endregion

        #region ��λ��ѯ�Ŀ���

        /// <summary>
        /// ��λ��ѯ�Ŀ���
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
                //��ѯ�ӽڵ�
                if (tlAllDept.FocusedNode.Nodes.Count > 0)
                {
                    node = GetNode(tlAllDept.FocusedNode.Nodes[0]);
                }
                //��ѯ��һ���ڵ�
                if (node != null)
                {
                    tlAllDept.SetFocusedNode(node);
                }
                else if (tlAllDept.FocusedNode.NextNode != null)
                {
                    node = GetNode(tlAllDept.FocusedNode.NextNode);
                }
                //��ѯ���ڵ�
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
                //������
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

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="p_node">���ڵ�</param>
        /// <returns>���ϵĽڵ�</returns>
        private TreeListNode GetNode(TreeListNode p_node)
        {
            List<TreeListNode> lstNode = new List<TreeListNode>();
            lstNode.Add(p_node); //��ӵ�һ���ڵ�
            int isFind = -1;
            string strCondition = txtDepart.Text.Trim().ToLower();
            for (int i = 0; i < lstNode.Count; i++)
            {
                //�ж�����
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
                    if (lstNode[i].Nodes.Count > 0) //��ӵ�һ���ӽڵ�
                    {
                        lstNode.Insert(i + 1, lstNode[i].Nodes[0]);
                        count++;
                    }
                    if (lstNode[i].NextNode != null) //�����һ���ڵ�
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

        #region ȷ����ť

        /// <summary>
        /// ȷ����ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //�޳���Ҷ�ӽڵ�Ŀ���
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
                DialogBox.Msg("��ѡ�����.", MessageBoxIcon.Information);
            }
        }

        #endregion

        #region ȡ����ť

        /// <summary>
        /// ȡ����ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region ����Ƴ�����

        #region ��ӿ��Ұ�ť

        /// <summary>
        /// ��ӿ��Ұ�ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToleft_Click(object sender, EventArgs e)
        {
            if (tlAllDept.Selection.Count > 0) //�ж��Ƿ�ѡ���˿���
            {
                foreach (TreeListNode node in tlAllDept.Selection) //���ѡ���˵Ŀ���
                {
                    if (lstDept[node.Id].leafFlag != null && lstDept[node.Id].leafFlag == "T") //�ж��Ƿ�ײ�
                    {
                        if (node.Tag == null) //�ж��Ƿ��Ѿ�ѡ��
                        {
                            AddDept(lstDept[node.Id].deptCode); //��ӿ���
                            tlSelDept.RefreshDataSource();
                            tlSelDept.ExpandAll();
                        }
                    }
                }
            }
        }

        #endregion

        #region �Ƴ����Ұ�ť

        /// <summary>
        /// �Ƴ����Ұ�ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToright_Click(object sender, EventArgs e)
        {
            if (tlSelDept.Selection.Count > 0) //�ж��Ƿ�ѡ���˿���
            {
                List<string> lstDeptid = new List<string>();
                foreach (TreeListNode node in tlSelDept.Selection) //���ѡ���˵Ŀ���
                {
                    if (lstResult[node.Id].leafFlag != null && lstResult[node.Id].leafFlag == "T") //�ж��Ƿ�ײ�
                    {
                        lstDeptid.Add(lstResult[node.Id].deptCode); //�����Ҫɾ���Ŀ���
                    }
                }
                for (int i = 0; i < lstDeptid.Count; i++)
                {
                    //�Ƴ�
                    RemoveDept(lstDeptid[i]);
                    //ˢ������Դ
                    tlSelDept.RefreshDataSource();
                    tlSelDept.ExpandAll();
                }
            }
        }

        #endregion

        #region ��ӿ���

        /// <summary>
        /// ��ӿ���
        /// </summary>
        /// <param name="p_id">����id</param>
        private void AddDept(string p_id)
        {
            //�ж��Ƿ��Ѿ���������
            if ((from a in lstResult where a.deptCode == p_id select a).Count() > 0)
            {
                return;
            }
            //��ӿ��ҵ���ѡ
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

        #region �Ƴ�����

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="p_id">����id</param>
        private void RemoveDept(string p_id)
        {
            //��ȡ���� 
            IEnumerable<EntityCodeDepartment> lstTemp = (from a in lstResult where a.deptCode == p_id select a);
            if (lstTemp.Count() > 0)
            {
                EntityCodeDepartment info = lstTemp.First();
                if (info.leafFlag != null && info.leafFlag == "T") //�ж��Ƿ��ӽڵ�
                {
                    //������ѡ��־
                    int nodeId = lstDept.IndexOf(info);
                    TreeListNode rightNode = tlAllDept.FindNodeByID(nodeId);
                    rightNode.ImageIndex = 0;
                    rightNode.SelectImageIndex = 0;
                    rightNode.Tag = null;
                }
                //�Ƴ�����
                if ((from a in lstResult where a.parent == p_id select a).Count() == 0) //�ж��Ƿ�û���ӽڵ�
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

        #region ˫����ӿ���

        /// <summary>
        /// ȫ������˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlAllDept_DoubleClick(object sender, EventArgs e)
        {
            //��ȡ��ǰ�ڵ�
            TreeListHitInfo hitInfo = tlAllDept.CalcHitInfo(tlAllDept.PointToClient(Cursor.Position));
            TreeListNode node = hitInfo.Node;
            if (node != null && node.Tag == null) //�ж��Ƿ�ѡ���˽ڵ� �����Ƿ�ѡ����
            {
                //�ж��Ƿ��ӽڵ�
                if (lstDept[node.Id].leafFlag != null && lstDept[node.Id].leafFlag == "T")
                {
                    AddDept(lstDept[node.Id].deptCode); //��ӿ���
                    //ˢ��
                    tlSelDept.RefreshDataSource();
                    tlSelDept.ExpandAll();
                }
            }
        }

        #endregion

        #region ˫���Ƴ�����

        /// <summary>
        /// ��ѡ����˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlSelDept_DoubleClick(object sender, EventArgs e)
        {
            //��ȡ��ǰ�ڵ�
            TreeListHitInfo hitInfo = tlSelDept.CalcHitInfo(tlSelDept.PointToClient(Cursor.Position));
            TreeListNode node = hitInfo.Node;
            if (node != null && node.Tag == null) //�ж��Ƿ�ѡ���˽ڵ� �����Ƿ�ѡ����
            {
                //�ж��Ƿ��ӽڵ�
                if (lstResult[node.Id].leafFlag != null && lstResult[node.Id].leafFlag == "T")
                {
                    RemoveDept(lstResult[node.Id].deptCode); //��ӿ���
                    //ˢ��
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