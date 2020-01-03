using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using System.Windows.Forms;

namespace iCare
{
    /// <summary>
    /// Summary description for clsOutPatientRevisitDomain.
    /// </summary>
    public class clsOutPatientRevisitDomain
    {
        //private clsOutPatientRevisitServ m_objServ;
        private ctlComboBox m_cboDept;
        private ctlComboBox m_cboArea;
        private ListView m_lsvInPatientList;
        public clsOutPatientRevisitDomain(ctlComboBox p_cboDept, ctlComboBox p_cboArea, ListView p_lsvPatient)
        {
            m_cboDept = p_cboDept;
            m_cboArea = p_cboArea;
            m_lsvInPatientList = p_lsvPatient;
            new clsSortTool().m_mthSetListViewSortable(m_lsvInPatientList);
            //m_objServ = new clsOutPatientRevisitServ();
        }
        public clsOutPatientRevisitDomain()
        {
            //m_objServ = new clsOutPatientRevisitServ();
        }
        #region ��������
        /// <summary>
        /// ��Ӹ�����ʾ
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngAddRemindRecord(clsOutPatientRevisitRemind_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddRemindRecord(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// �޸ĸ�����ʾ
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngModifyRemindRecord(clsOutPatientRevisitRemind_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModifyRemindRecord(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// �޸ĸ�����ʾ(������¼)
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngSetRemindRecordStatusMRecord(int p_intStatus, string p_strInPatientID, string p_strInPatientDate, string p_strRevisitTime)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngSetRemindRecordStatusMRecord(p_intStatus, p_strInPatientID, p_strInPatientDate, p_strRevisitTime);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ȡԱ����������
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <returns></returns>
        public clsDepartment[] m_objGetEmpDept(string p_strEmployeeID)
        {
            if (p_strEmployeeID == null || p_strEmployeeID == string.Empty)
                return null;
            clsDept_Desc[] objDeptDescArr;
            long lngres = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeptArr_EmployeeCanManage(p_strEmployeeID, out objDeptDescArr);
            if (lngres <= 0 || objDeptDescArr == null)
                return null;
            clsDepartment[] objDeptArr = new clsDepartment[objDeptDescArr.Length];
            for (int i = 0; i < objDeptDescArr.Length; i++)
            {
                objDeptArr[i] = new clsDepartment(objDeptDescArr[i].m_strDeptID, objDeptDescArr[i].m_strDeptName);
            }
            if (objDeptArr.Length > 0)
                return objDeptArr;
            return null;
        }
        /// <summary>
        /// ��ȡ����������ʾ����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetRemindRecord(string p_strInPatientID, DateTime p_dtmInPatientDate, out clsOutPatientRevisitRemind_VO[] p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRemindRecord(p_strInPatientID, p_dtmInPatientDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ���ݲ��˵�����ʱ���ȡ������ʾ����
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_dtmInPatientDate">��Ժʱ��</param>
        /// <param name="p_dtmRevisitTime">����ʱ��</param>
        /// <param name="p_strRemaindText">������ʾ����</param>
        /// <returns></returns>
        public long m_lngGetRemindRecord(string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmRevisitTime, out string p_strRemaindText)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRemindRecord(p_strInPatientID, p_dtmInPatientDate, p_dtmRevisitTime, out p_strRemaindText);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// �޸�״̬
        /// </summary>
        /// <param name="p_intStatus"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public long m_lngSetRemindRecordStatus(int p_intStatus, string p_strInPatientID, string p_strInPatientDate)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngSetRemindRecordStatus(p_intStatus, p_strInPatientID, p_strInPatientDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// �����ѹ��ڻ�����ʾ��״̬Ϊ��2��
        /// </summary>
        /// <param name="p_blnSetToday"></param>
        /// <param name="p_strAreaID"></param>
        /// <returns></returns>
        public long m_lngSetOutDateRemindStatus(bool p_blnSetToday, string p_strAreaID)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngSetOutDateRemindStatus(p_blnSetToday, p_strAreaID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ȡȫ�����ڵĸ�����ʾ����
        /// </summary>
        /// <param name="p_strAreaID"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllNeedRemindRecord(string p_strAreaID, out clsOutPatientRevisitRemind_VO[] p_objContentArr)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllNeedRemindRecord(p_strAreaID, out p_objContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        public void m_mthSetRemindMessage()
        {
            if (MDIParent.s_ObjCurrInPatientArea == null)
                return;
            clsOutPatientRevisitRemind_VO[] m_objContentArr = null;
            long lngRes = m_lngGetAllNeedRemindRecord(MDIParent.s_ObjCurrInPatientArea.m_StrAreaID, out m_objContentArr);
            if (lngRes > 0 && m_objContentArr != null)
            {
                string strMessage = "";
                for (int i = 0; i < m_objContentArr.Length; i++)
                {
                    strMessage += m_objContentArr[i].m_StrRemaindText + "\n";
                }
                frmRevistRemindMessage frm = new frmRevistRemindMessage(strMessage);
                frm.Show();
            }
            else
                MDIParent.s_blnRevisitBegin = false;
        }
        #endregion ��������

        #region �����¼

        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngAddRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddRecordContent(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngModifyRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModifyRecordContent(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngDeleteRecordContent(clsOutPatientRevisitRecord_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngDeleteRecordContent(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ȡδɾ����¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetRecordContentByInPatient(string p_strInPatientID, DateTime p_dtmInPatientDate, out clsOutPatientRevisitRecord_VO[] p_objContentArr)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordContentByInPatient(p_strInPatientID, p_dtmInPatientDate, out p_objContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ȡ��ɾ����¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllDeActivedRecordContent(string p_strInPatientID, DateTime p_dtmInPatientDate, out clsOutPatientRevisitRecord_VO[] p_objContentArr)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllDeActivedRecordContent(p_strInPatientID, p_dtmInPatientDate, out p_objContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        /// <summary>
        /// ��ȡ��ɾ����¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetDeActivedRecordContent(string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmCreatedDate, out clsOutPatientRevisitRecord_VO p_objContent)
        {
            //clsOutPatientRevisitServ m_objServ =
            //    (clsOutPatientRevisitServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutPatientRevisitServ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeActivedRecordContent(p_strInPatientID, p_dtmInPatientDate, p_dtmCreatedDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion �����¼

        #region Funtion
        public void m_mthInitilize()
        {
            clsDepartment[] objDeptArr = m_objGetEmpDept(MDIParent.strOperatorID);
            if (objDeptArr != null)
            {
                m_cboDept.AddRangeItems(objDeptArr);
                m_cboDept.SelectedIndex = 0;
            }
        }
        public void m_thDept_SelectedIndexChanged()
        {
            m_cboArea.ClearItem();
            clsInPatientArea[] objAreaArr;
            new clsDepartmentManager().m_lngGetAllAreaInDept(((clsDepartment)(m_cboDept.SelectedItem)).m_StrDeptID, out objAreaArr);
            if (objAreaArr != null)
            {
                m_cboArea.AddRangeItems(objAreaArr);
                m_cboArea.SelectedIndex = 0;
            }
        }
        public void m_mthDisplayPatient(string p_strDate, bool p_blnIsRecord)
        {
            clsPatient[] objPatientArr = new clsPatientManager().m_objGetEndDateInPatientByAreaID(((clsInPatientArea)m_cboArea.SelectedItem).m_StrAreaID, p_strDate);
            m_mthSetPatientToLsv(objPatientArr, p_blnIsRecord);
        }

        public void m_mthDisplayPatient(string p_strStartDate, string p_strEndDate, bool p_blnIsRecord, bool p_blnIsOutPatient)
        {
            clsPatient[] objPatientArr = null;
            if (m_cboArea.SelectedItem != null)
            {
                objPatientArr = new clsPatientManager().m_objGetBetwDateInPatientByAreaID(((clsInPatientArea)m_cboArea.SelectedItem).m_StrAreaID, p_strStartDate, p_strEndDate, p_blnIsOutPatient, 1);
            }
            else if (m_cboDept.SelectedItem != null)
            {
                objPatientArr = new clsPatientManager().m_objGetBetwDateInPatientByAreaID(((clsDepartment)m_cboDept.SelectedItem).m_StrDeptID, p_strStartDate, p_strEndDate, p_blnIsOutPatient, 0);
            }
            m_mthSetPatientToLsv(objPatientArr, p_blnIsRecord);
        }

        public void m_mthDispalyPatientByInPatientID(string p_strInPatientID, bool p_blnIsRecord)
        {
            clsPatient[] objPatientArr = new clsPatientManager().m_objGetInPatientByInPatientID(p_strInPatientID);
            m_mthSetPatientToLsv(objPatientArr, p_blnIsRecord);
        }

        public void m_mthDispalyPatientByPatientName(string p_strPatientName, bool p_blnIsRecord)
        {
            clsPatient[] objPatientArr = new clsPatientManager().m_objGetInPatientByPatientName(p_strPatientName);
            m_mthSetPatientToLsv(objPatientArr, p_blnIsRecord);
        }

        private void m_mthSetPatientToLsv(clsPatient[] objPatientArr, bool p_blnIsRecord)
        {
            if (objPatientArr != null && objPatientArr.Length > 0)
            {
                m_lsvInPatientList.Items.Clear();
                for (int i = 0; i < objPatientArr.Length; i++)
                {
                    int intCount = objPatientArr[i].m_ObjInBedInfo.m_intGetSessionCount();
                    if (intCount == 1)
                    {
                        ListViewItem item = new ListViewItem();
                        if (p_blnIsRecord)
                            item = new ListViewItem(new string[] { objPatientArr[i].m_StrInPatientID, objPatientArr[i].m_StrName, objPatientArr[i].m_DtmLastInDate.ToString("yyyy��MM��dd��"), objPatientArr[i].m_DtmLastOutDate.ToString("yyyy��MM��dd��") });
                        else
                            item = new ListViewItem(new string[] { objPatientArr[i].m_StrInPatientID, objPatientArr[i].m_StrName, objPatientArr[i].m_DtmLastInDate.ToString("yyyy��MM��dd��"), objPatientArr[i].m_IntRevisitTimes.ToString() });
                        item.Tag = objPatientArr[i];
                        m_lsvInPatientList.Items.Add(item);
                    }
                    else
                    {
                        for (int j = 0; j < intCount; j++)
                        {
                            clsInBedSessionInfo objSessionInfo = objPatientArr[i].m_ObjInBedInfo.m_objGetSessionByIndex(j);

                            clsPatient objPatient = objPatientArr[i];
                            objPatient.m_DtmSelectedInDate = objSessionInfo.m_DtmInDate;
                            objPatient.m_DtmSelectedOutDate = objSessionInfo.m_DtmOutDate;
                            ListViewItem item2 = new ListViewItem();
                            if (p_blnIsRecord)
                                item2 = new ListViewItem(new string[] { objPatientArr[i].m_StrInPatientID, objPatientArr[i].m_StrName, objSessionInfo.m_DtmInDate.ToString("yyyy��MM��dd��"), objSessionInfo.m_DtmOutDate.ToString("yyyy��MM��dd��") });
                            else
                                item2 = new ListViewItem(new string[] { objPatientArr[i].m_StrInPatientID, objPatientArr[i].m_StrName, objSessionInfo.m_DtmInDate.ToString("yyyy��MM��dd��"), objPatientArr[i].m_IntRevisitTimes.ToString() });
                            item2.Tag = objPatient;
                            m_lsvInPatientList.Items.Add(item2);
                        }
                    }
                }
            }
            else
                m_lsvInPatientList.Items.Clear();
        }
        #endregion
    }
}
