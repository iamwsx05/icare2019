using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ���˴�ӡ����������Ʋ�
    /// </summary>
    public class clsCtl_LisBarcodeSort : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// Domain��
        /// </summary>
        clsDcl_LisBarcodeSort m_objDomain;
        /// <summary>
        /// ����Viewer��
        /// </summary>
        frmLisBarcodeSort m_objViewer;
        List<clsLisApplMainVO> m_lstAppInfo = new List<clsLisApplMainVO>();
        #endregion

        #region ���캯��
        /// <summary>
        /// ����GUI��
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmLisBarcodeSort)frmMDI_Child_Base_in;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_LisBarcodeSort()
        {
            m_objDomain = new clsDcl_LisBarcodeSort();
        }
        #endregion

        #region ��ѯ������ͬʱ�������ݵ�XML�ĵ���
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        public void m_mthQuery()
        {
            string strPatientCardId = m_objViewer.m_txtPatientCard.Text.Trim();
            if (string.IsNullOrEmpty(strPatientCardId))
            {
                return;
            }
            m_objViewer.m_txtPatientCard.Clear();
            long lngRes = 0;
            clsPatientBaseInfo_VO m_objPatientInfo = null;
            lngRes = m_objDomain.m_lngQueryPatientInfo(strPatientCardId, out m_objPatientInfo);
            if (lngRes > 0 && m_objPatientInfo != null)
            {
                for (int i = 0; i < m_objViewer.m_dgBarcodeSort.Rows.Count; i++)
                {
                    if (m_objViewer.m_dgBarcodeSort.Rows[i].Cells["colPatientCard"].Value.ToString() == strPatientCardId)
                    {
                        return;
                    }
                }
                if (m_objViewer.m_xmlBarcodeSort != null)
                {
                    try
                    {
                        XmlNode root = m_objViewer.m_xmlBarcodeSort.SelectSingleNode("Sort");

                        XmlElement xmlEment = m_objViewer.m_xmlBarcodeSort.CreateElement("Item");

                        XmlNode childNode = m_objViewer.m_xmlBarcodeSort.CreateElement("PatientCard");
                        childNode.InnerText = strPatientCardId;
                        xmlEment.AppendChild(childNode);

                        childNode = m_objViewer.m_xmlBarcodeSort.CreateElement("DateTime");
                        childNode.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        xmlEment.AppendChild(childNode);

                        childNode = m_objViewer.m_xmlBarcodeSort.CreateElement("PatientName");
                        childNode.InnerText = m_objPatientInfo.m_strName;
                        xmlEment.AppendChild(childNode);

                        childNode = m_objViewer.m_xmlBarcodeSort.CreateElement("PatientSex");
                        childNode.InnerText = m_objPatientInfo.m_strSex;
                        xmlEment.AppendChild(childNode);

                        childNode = m_objViewer.m_xmlBarcodeSort.CreateElement("PatientAge");
                        childNode.InnerText = m_objPatientInfo.m_strAge;
                        xmlEment.AppendChild(childNode);
                        root.AppendChild(xmlEment);
                        DataRow drTemp = m_objViewer.m_dtBaricodeSort.NewRow();
                        drTemp["PatientCard"] = strPatientCardId;
                        drTemp["DateTime"] = DateTime.Now;
                        drTemp["PatientName"] = m_objPatientInfo.m_strName;
                        drTemp["PatientSex"] = m_objPatientInfo.m_strSex;
                        drTemp["PatientAge"] = m_objPatientInfo.m_strAge;
                        m_objViewer.m_dtBaricodeSort.Rows.Add(drTemp);
                        m_objViewer.m_dtBaricodeSort.AcceptChanges();
                        m_objViewer.m_xmlBarcodeSort.Save("PatientBarcodeSort.XML");
                    }
                    catch (Exception objEx)
                    {
                        MessageBox.Show(objEx.Message);
                    }
                }
                if (m_objViewer.m_dgBarcodeSort.DataSource == null)
                {
                    m_objViewer.m_dgBarcodeSort.DataSource = m_objViewer.m_dtBaricodeSort;

                }
                m_objViewer.m_dgBarcodeSort.Rows[0].Selected = true;
            }
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public void m_mthGetCheckContent()
        {
            if (m_objViewer.m_dgBarcodeSort.SelectedRows.Count <= 0)
            {
                return;
            }
            m_lstAppInfo.Clear();
            m_objViewer.m_txtCheckContent.Clear();
            string strPatientCard = m_objViewer.m_dgBarcodeSort.SelectedRows[0].Cells["colPatientCard"].Value.ToString().Trim();
            long lngRes = 0;
            string strCheckContent = null;
            clsLisApplMainVO[] m_objAppMainVOArr = null;
            string strFromDate = m_objViewer.m_dtFromDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string strToDate = m_objViewer.m_dtToDate.Value.ToString("yyyy-MM-dd 23:59:59");
            lngRes = m_objDomain.m_lngQueryPatientCheckContent(strPatientCard, strFromDate, strToDate, out strCheckContent, out m_objAppMainVOArr);
            if (lngRes > 0 && m_objAppMainVOArr != null && m_objAppMainVOArr.Length > 0)
            {
                m_objViewer.m_txtCheckContent.Text = strCheckContent;
                m_lstAppInfo.AddRange(m_objAppMainVOArr);
            }
        }
        #endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            string[] SampleTypeIdArr = null;
            if (this.m_objViewer.BizType == 1)
            {
                SampleTypeIdArr = m_objViewer.m_strParmBlood.Trim().Split(new char[] { ';' });
            }
            else if (this.m_objViewer.BizType == 2)
            {
                SampleTypeIdArr = m_objViewer.m_strParmBloodNo.Trim().Split(new char[] { ';' });
            }

            List<clsLisApplMainVO> lstApp = new List<clsLisApplMainVO>();
            clsLisApplMainVO vo = null;
            if (this.m_objViewer.BizType == 0)
            {
                foreach (clsLisApplMainVO item in m_lstAppInfo)
                {
                    vo = new clsLisApplMainVO();
                    item.m_mthCopyTo(vo);
                    lstApp.Add(vo);
                }
            }
            else
            {
                if (SampleTypeIdArr == null || SampleTypeIdArr.Length == 0) return;
                List<string> lstSampleId = new List<string>();
                lstSampleId.AddRange(SampleTypeIdArr);
                foreach (clsLisApplMainVO item in m_lstAppInfo)
                {
                    vo = new clsLisApplMainVO();
                    item.m_mthCopyTo(vo);
                    if (string.IsNullOrEmpty(vo.m_strSampleTypeID)) continue;
                    if (lstSampleId.IndexOf(vo.m_strSampleTypeID) >= 0)
                    {
                        lstApp.Add(vo);
                    }
                }
            }
            clsSealedLisApplyReportPrint objPrint = new clsSealedLisApplyReportPrint();
            string strLoginID = m_objViewer.m_strLoginID;
            long lngRes = 0;
            foreach (clsLisApplMainVO item in lstApp)
            {
                if (item.m_intReportPrint == 1) continue;
                
                clsSealedBIHLisApplyReportPrint printTool = new clsSealedBIHLisApplyReportPrint();
                printTool.m_mthGetPrintContent(item.m_strAPPLICATION_ID, item.m_strBarcode);
                printTool.m_mthReport(0, "");
                lngRes = m_objDomain.m_lngInsertCollector(strLoginID, item.m_strSampleID, item.m_strAPPLICATION_ID);
                if (lngRes <= 0)
                {
                    MessageBox.Show(m_objViewer, "�����ӡʧ�ܣ������´�ӡ", "��ӡ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (lngRes > 0)
            {
                m_mthDeleteRow();
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void m_mthPrint2()
        {
            string[] strSampleTypeIDArr = m_objViewer.m_strParmBlood.Split(new char[] { ';' });
            List<clsLisApplMainVO> m_lstPrint = new List<clsLisApplMainVO>();
            clsLisApplMainVO objTemp = null;
            if (strSampleTypeIDArr != null && strSampleTypeIDArr.Length > 0)
            {
                Dictionary<string, int> m_dicSampleTypeID = new Dictionary<string, int>();
                for (int i = 0; i < strSampleTypeIDArr.Length; i++)
                {
                    m_dicSampleTypeID.Add(strSampleTypeIDArr[i], i);
                }
                string strSampleTypeID = null;
                for (int i = 0; i < m_lstAppInfo.Count; i++)
                {
                    objTemp = new clsLisApplMainVO();
                    m_lstAppInfo[i].m_mthCopyTo(objTemp);
                    strSampleTypeID = m_lstAppInfo[i].m_strSampleTypeID;
                    if (string.IsNullOrEmpty(strSampleTypeID))
                    {
                        continue;
                    }
                    if (m_dicSampleTypeID.ContainsKey(strSampleTypeID))
                    {
                        if (m_objViewer.m_blnIsBlood)
                        {
                            m_lstPrint.Add(objTemp);
                        }
                    }
                    else
                    {
                        if (!m_objViewer.m_blnIsBlood)
                        {
                            m_lstPrint.Add(objTemp);
                        }
                    }
                }
            }
            clsSealedLisApplyReportPrint objPrint = new clsSealedLisApplyReportPrint();
            string strLoginID = m_objViewer.m_strLoginID;
            long lngRes = 0;
            for (int i = 0; i < m_lstPrint.Count; i++)
            {
                objTemp = m_lstPrint[i];
                if (objTemp.m_intReportPrint == 1)
                {
                    continue;
                } 
                clsSealedBIHLisApplyReportPrint printTool = new clsSealedBIHLisApplyReportPrint();
                printTool.m_mthGetPrintContent(objTemp.m_strAPPLICATION_ID, objTemp.m_strBarcode);
                printTool.m_mthReport(0, "");
                lngRes = m_objDomain.m_lngInsertCollector(strLoginID, objTemp.m_strSampleID, objTemp.m_strAPPLICATION_ID);
                if (lngRes <= 0)
                {
                    MessageBox.Show(m_objViewer, "�����ӡʧ�ܣ������´�ӡ", "��ӡ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (lngRes > 0)
            {
                m_mthDeleteRow();
            }
        }
        #endregion


        #region ���Ƴ�XML�ĵ��Ľڵ�
        /// <summary>
        /// ���Ƴ�XML�ĵ��Ľڵ�
        /// </summary>
        public void m_mthDeleteRow()
        {
            if (m_objViewer.m_dgBarcodeSort.SelectedRows.Count <= 0)
            {
                return;
            }
            DataRow drTemp = ((DataRowView)m_objViewer.m_dgBarcodeSort.SelectedRows[0].DataBoundItem).Row;
            string strPatientCard = drTemp["PatientCard"].ToString().Trim();
            m_objViewer.m_dtBaricodeSort.Rows.Remove(drTemp);
            m_objViewer.m_dtBaricodeSort.AcceptChanges();
            XmlNode xmlRootNode = m_objViewer.m_xmlBarcodeSort.SelectSingleNode("Sort");
            XmlNodeList xmlNodeList = xmlRootNode.ChildNodes;
            XmlNode xmlChildNode = null;
            List<XmlNode> m_lstXmlNode = new List<XmlNode>();
            foreach (XmlNode node in xmlNodeList)
            {
                if (node.Name == "Item")
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name == "PatientCard")
                        {
                            if (childNode.InnerText == strPatientCard)
                            {
                                m_lstXmlNode.Add(node);
                            }
                            break;
                        }
                    }
                }
            }
            foreach (XmlNode node in m_lstXmlNode)
            {
                xmlRootNode.RemoveChild(node);
            }
            m_objViewer.m_xmlBarcodeSort.Save("PatientBarcodeSort.XML");
        }
        #endregion
    }
}
