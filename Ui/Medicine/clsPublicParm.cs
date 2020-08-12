using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Text;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��������
    /// </summary>
    public class clsPublicParm
    {
        public clsPublicParm()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���þ��洰��
        public void m_mthShowWarning(exDataGridSour.exComboBox txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(TextBox txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(com.digitalwave.iCare.gui.HIS.exComboBox txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        //public void m_mthShowWarning(SourceLibrary.Windows.Forms.TextBoxTypedNumeric txtBox, string strWaring)
        //{
        //    frmShowWarning ShowWarning = new frmShowWarning();
        //    ShowWarning.m_GetWaring = strWaring;
        //    Point p = txtBox.Parent.PointToScreen(txtBox.Location);
        //    p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
        //    ShowWarning.Location = p;
        //    ShowWarning.Show();
        //}
        public void m_mthShowWarning(com.digitalwave.controls.ctlTextBoxFind txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(System.Windows.Forms.Panel txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-0, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(System.Windows.Forms.GroupBox txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-0, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(System.Windows.Forms.ComboBox txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-50, -(ShowWarning.Height - txtBox.Height / 2));
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(System.Windows.Forms.ListView txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.X += (txtBox.Width / 2);
            p.Y += (txtBox.Height / 2);
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        public void m_mthShowWarning(com.digitalwave.controls.datagrid.ctlDataGrid txtBox, string strWaring)
        {
            frmShowWarning ShowWarning = new frmShowWarning();
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.X += (txtBox.Width / 2);
            p.Y += (txtBox.Height / 2);
            ShowWarning.Location = p;
            ShowWarning.Show();
        }
        #endregion

        #region �ж��������Ƿ��Ѿ����̵㲢���
        /// <summary>
        /// �ж��������Ƿ��Ѿ����̵㲢���
        /// </summary>
        /// <param name="strPeriod">������</param>
        /// <returns>ture-�Ѿ����̵㲢���</returns>
        public static bool m_EstimatePeriod(string strPeriod)
        {
            clsDomainControlStorageOrd doMain = new clsDomainControlStorageOrd();
            bool IsAppl = false;
            doMain.m_lngEstimatePeriod(strPeriod, out IsAppl);
            return IsAppl;
        }

        #endregion
        #region ��ȡ�µ��ݺ�
        /// <summary>
        /// ��ȡ�µ��ݺ�
        /// </summary>
        /// <param name="strOldDocument">�ɵ��ݺ�</param>
        /// <param name="strSing">��������,����Ҳ������ʵĵ�������ID������-(yyyyMMdd)��ʽ�ĵ��ݺ�</param>
        /// <param name="intStorage">�ֿ��</param>
        /// <param name="strDate">���������ԣ���yyyyMMdd������ʽ</param>
        /// <param name="ordType">���ݺŵĿ�ʼ��ĸ</param>
        /// <returns></returns>
        public static string m_mthGetNewDocument(string strOldDocument, string strSing, int intStorage, string strDate, string ordType)
        {
            string strNewDocument = "";
            if (strOldDocument == null || strOldDocument == "")
            {
                strNewDocument = ordType + strDate + intStorage.ToString() + "001";
            }
            else
            {
                Encoding ascii = Encoding.ASCII;
                Byte[] byCoding = ascii.GetBytes(strOldDocument);
                int intEnLengt = -1;
                for (int i1 = 0; i1 < byCoding.Length; i1++)
                {
                    if ((int)byCoding[i1] <= 57)
                    {
                        intEnLengt = i1 - 1;
                        break;
                    }
                }
                if (intEnLengt > -1)
                {
                    string strEN = strOldDocument.Substring(0, intEnLengt + 1);
                    string strNumber = strOldDocument.Substring(intEnLengt + 1);
                    long intNumber = 0;
                    try
                    {
                        strNumber = "1" + strNumber;
                        intNumber = Convert.ToInt64(strNumber) + 1;
                        strNumber = intNumber.ToString().Substring(1, strNumber.Length - 1);
                        strNewDocument = strEN + strNumber;
                    }
                    catch
                    {
                        strNewDocument = strEN;
                    }
                }
                else
                {

                    try
                    {
                        long n = Convert.ToInt64(strOldDocument) + 1;
                        strNewDocument = n.ToString();
                    }
                    catch
                    {
                        strNewDocument = ordType + strDate + intStorage.ToString() + "001";
                    }
                }

            }
            return strNewDocument;
        }

        #endregion

        #region ��ȡ�µ��ݺ�
        /// <summary>
        /// ��ȡ�µ��ݺ�
        /// </summary>
        /// <param name="strOldDocument">�ɵ��ݺ�</param>
        /// <param name="strSing">1-��ⵥ,2-���ⵥ,3-(yyyyMMdd)��ʽ�ĵ��ݺ�</param>
        /// <param name="intStorage">�ֿ��</param>
        /// <returns></returns>
        public static string m_mthGetNewDocument(string strOldDocument, string strSing, int intStorage)
        {
            string strNewDocument = "";
            if (strOldDocument == null || strOldDocument == "")
            {
                switch (strSing)
                {
                    case "1":
                        strNewDocument = "ZR" + clsPublicParm.s_datGetServerDate().ToString("yyMMdd") + intStorage.ToString() + "001";
                        break;
                    case "2":
                        strNewDocument = "CR" + clsPublicParm.s_datGetServerDate().ToString("yyMMdd") + intStorage.ToString() + "001";
                        break;
                    case "3":
                        strNewDocument = clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd") + "0001";
                        break;
                }

            }
            else
            {
                Encoding ascii = Encoding.ASCII;
                Byte[] byCoding = ascii.GetBytes(strOldDocument);
                int intEnLengt = -1;
                for (int i1 = 0; i1 < byCoding.Length; i1++)
                {
                    if ((int)byCoding[i1] <= 57)
                    {
                        intEnLengt = i1 - 1;
                        break;
                    }
                }
                if (intEnLengt > -1)
                {
                    string strEN = strOldDocument.Substring(0, intEnLengt + 1);
                    string strNumber = strOldDocument.Substring(intEnLengt + 1);
                    long intNumber = 0;
                    try
                    {
                        strNumber = "1" + strNumber;
                        intNumber = Convert.ToInt64(strNumber) + 1;
                        strNumber = intNumber.ToString().Substring(1, strNumber.Length - 1);
                        strNewDocument = strEN + strNumber;
                    }
                    catch
                    {
                        strNewDocument = strEN;
                    }
                }
                else
                {

                    try
                    {
                        long n = Convert.ToInt64(strOldDocument) + 1;
                        strNewDocument = n.ToString();
                    }
                    catch
                    {
                        switch (strSing)
                        {
                            case "1":
                                strNewDocument = "ZR" + clsPublicParm.s_datGetServerDate().ToString("yyMMdd") + intStorage.ToString() + "001";
                                break;
                            case "2":
                                strNewDocument = "CR" + clsPublicParm.s_datGetServerDate().ToString("yyMMdd") + intStorage.ToString() + "001";
                                break;
                            case "3":
                                strNewDocument = clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd") + "0001";
                                break;
                        }
                    }
                }

            }
            return strNewDocument;
        }

        #endregion

        #region//�����ID
        /// <summary>
        /// p_TableName����
        /// p_Columns����
        /// length������ID�ĳ���
        /// ����ֵNewId
        /// </summary>
        public void m_GetNewId(string p_TableName, string p_Columns, out string NewId, int length)
        {
            NewId = "";
            //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc =
            //    (com.digitalwave.iCare.middletier.HRPService.clsHRPTableService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HRPService.clsHRPTableService));
            NewId = (new weCare.Proxy.ProxyBase()).Service.m_strGetNewID(p_TableName, p_Columns, length);
            if (Convert.ToInt32(NewId) == 0)
            {
                NewId = "1";
                NewId = NewId.PadLeft(length, '0');
            }
        }
        #endregion

        #region �����ⵥ������ID
        public static string m_GetID()
        {
            //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc =
            //    (com.digitalwave.iCare.middletier.HRPService.clsHRPTableService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HRPService.clsHRPTableService));
            string NewId = (new weCare.Proxy.ProxyBase()).Service.m_strGetNewID("T_OPR_STORAGEORD", "STORAGEORDID_CHR", 14);
            if (Convert.ToInt32(NewId) == 0)
            {
                NewId = "1";
                NewId = NewId.PadLeft(14, '0');
            }
            return NewId;
        }
        #endregion

        #region �����ⵥ��ϸ������ID
        public static string m_GetStorageOrdDeID()
        {
            //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc =
            //    (com.digitalwave.iCare.middletier.HRPService.clsHRPTableService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HRPService.clsHRPTableService));
            string NewId = (new weCare.Proxy.ProxyBase()).Service.m_strGetNewID("T_OPR_STORAGEORDDE", "STORAGEORDDEID_CHR", 14);
            if (Convert.ToInt32(NewId) == 0)
            {
                NewId = "1";
                NewId = NewId.PadLeft(14, '0');
            }
            return NewId;
        }
        #endregion

        #region ���ҩ��ĳ�ʼ��
        /// <summary>
        /// ���ҩ��ĳ�ʼ��
        /// </summary>
        /// <param name="strID">�ⷿ����</param>
        /// <returns></returns>
        public static bool s_blnCheckStorageInitFlag(string strID)
        {
            bool blnResult = false;
            long lngRes = 0;
            int intInitFlag = 0;
            //com.digitalwave.iCare.gui.StorageManage.clsDomainControlStorageBase objManage =
            //    new com.digitalwave.iCare.gui.StorageManage.clsDomainControlStorageBase();
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageInitFlag(strID, out intInitFlag);

            blnResult = (intInitFlag == 0) ? false : true;

            return blnResult;
        }
        #endregion

        #region ����Ƿ�������������
        /// <summary>
        /// ����Ƿ�������������
        /// </summary>
        /// <returns></returns>
        public static bool s_blnCheckPeriod()
        {
            bool blnResult = false;
            long lngRes = 0;
            int intRow = 0;

            clsDomainControlStorageAidInfo objManage = new clsDomainControlStorageAidInfo();
            lngRes = objManage.m_lngGetPeriodRow(out intRow);

            blnResult = (intRow == 0) ? false : true;

            return blnResult;
        }
        #endregion

        #region ��ҩ��ҵ�����ʹ��ʱ���
        /// <summary>
        /// ����Ƿ���Խ���ҵ�����
        /// </summary>
        /// <param name="strID">�ⷿ����</param>
        /// <returns></returns>
        public static bool s_blnCheckCanOperator(string strID)
        {
            bool blnResult = false;

            blnResult = s_blnCheckStorageInitFlag(strID);

            if (!blnResult)
            {
                MessageBox.Show("��δ���ÿ�����\n��ȷ��ԭʼ�����ٽ���ҵ�����", "ϵͳ���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return blnResult;
            }

            blnResult = s_blnCheckPeriod();
            if (!blnResult)
            {
                MessageBox.Show("��δ���������ڡ�\n�������������ٽ���ҵ�����", "ϵͳ���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return blnResult;
            }
            return blnResult;

        }
        #endregion

        #region ������Ӧ�̳�����Ϣ
        /// <summary>
        /// ��ȡ��Ӧ����Ϣ
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public static long s_lngGetVendor(string p_strSQL, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            //com.digitalwave.iCare.gui.VendorManage.clsDomainControlVendor objVendor = new com.digitalwave.iCare.gui.VendorManage.clsDomainControlVendor();
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindVendorByAny(p_strSQL, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ������Ӧ�̳�����Ϣ�����VO��ŷ����ΰ��2004-05-20
        /// <summary>
		/// ��ȡ��Ӧ����Ϣ�����VO
		/// </summary>
		/// <param name="p_strSQL"></param>
		/// <param name="p_objResultArr"></param>
		/// <returns></returns>
		public static long s_lngGetVendor(string p_strSQL, out clsVendor_VO[] p_objResultArr)
        {
            p_objResultArr = new clsVendor_VO[0];
            long lngRes = 0;
            System.Data.DataTable dtbResult = new DataTable(); ;
            lngRes = s_lngGetVendor(p_strSQL, out dtbResult);

            #region ���ݸ�VO
            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsVendor_VO[intRow];
                    for (int i1 = 0; i1 < intRow; i1++)
                    {
                        p_objResultArr[i1] = new clsVendor_VO();
                        p_objResultArr[i1].m_strVendorID = dtbResult.Rows[i1]["VENDORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intVendorType = int.Parse(dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intProdcutType = int.Parse(dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strAddress = dtbResult.Rows[i1]["ADDRESS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPhone = dtbResult.Rows[i1]["PHONE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strContactor = dtbResult.Rows[i1]["CONTACTOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strContactorPhone = dtbResult.Rows[i1]["CONTACTORPHONE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEmail = dtbResult.Rows[i1]["EMAIL_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFax = dtbResult.Rows[i1]["FAX_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();

                    }
                }
            }
            #endregion

            return lngRes;
        }
        #endregion

        #region �����ֶ���Ϣ������Ӧ�̳��ҵ�����ֶε�ֵ  ŷ����ΰ  2004-06-04
        /// <summary>
        /// �����ֶ�ֵ������Ӧ�̳��ҵ�����ֶε�ֵ
        /// </summary>
        /// <param name="p_strDestFiled">��õ�ֵ���ֶ�</param>
        /// <param name="p_strSourceFiled">Դ�ֶ�</param>
        /// <param name="p_strValue">Դ�ֶ�ֵ</param>
        /// <returns></returns>
        public static string s_GetVendorFiledValue(string p_strDestFiled, string p_strSourceFiled, string p_strValue)
        {
            string strResult = "";
            long lngRes = 0;
            string strSQL = "";
            System.Data.DataTable dtbResult = new DataTable();
            lngRes = s_lngGetVendor(strSQL, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        if (dtbResult.Rows[i1][p_strSourceFiled].ToString() == p_strValue)
                        {
                            strResult = dtbResult.Rows[i1][p_strDestFiled].ToString();
                            break;
                        }
                    }
                }
            }
            return strResult;
        }
        #endregion

        #region ��ȡ������Ϣ��ŷ����ΰ��2004-05-24
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public static long s_lngGetDept(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            lngRes = (new weCare.Proxy.ProxyHisBase()).Service.s_lngGetDept(out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region ��ȡ������Ϣ��ŷ����ΰ��2004-06-21
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        public static clsDepartmentVO[] s_objGetDeptList()
        {
            clsDepartmentVO[] objResult = new clsDepartmentVO[0];
            long lngRes = 0;
            DataTable dtbResult = null;

            lngRes = s_lngGetDept(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    objResult = new clsDepartmentVO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        objResult[i] = new clsDepartmentVO();
                        objResult[i].strDeptID = dtbResult.Rows[i]["DEPTID_CHR"].ToString().Trim();
                        objResult[i].strDeptName = dtbResult.Rows[i]["DEPTNAME_VCHR"].ToString().Trim();
                    }
                }
            }

            return objResult;

        }
        #endregion

        #region ��ȡ�����ֶ�ֵ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="strDestFiled"></param>
        /// <param name="strSourceFiled"></param>
        /// <param name="strFiledValue"></param>
        /// <returns></returns>
        public static string s_strGetDeptFiled(string strDestFiled, string strSourceFiled, string strFiledValue)
        {
            string strResult = "";
            long lngRes = 0;
            System.Data.DataTable dtbResult = new DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            lngRes = (new weCare.Proxy.ProxyHisBase()).Service.s_lngGetDept(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        if (dtbResult.Rows[i][strSourceFiled].ToString().Trim() == strFiledValue.Trim())
                        {
                            strResult = dtbResult.Rows[i][strDestFiled].ToString().Trim();
                            break;
                        }
                    }
                }
            }

            return strResult;

        }
        #endregion

        #region ���ϵͳʱ��
        /// <summary>
        /// ��÷�����ʱ��
        /// </summary>
        /// <returns></returns>
        public static System.DateTime s_datGetServerDate()
        {
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            return (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate();
        }
        #endregion

        #region ���ҩƷ��桡ŷ����ΰ��2004-05-25
        /// <summary>
        /// ���ҩƷ���
        /// </summary>
        /// <param name="p_strMedID"></param>
        /// <param name="p_fltMedStorage"></param>
        /// <returns></returns>
        public static float s_flgCheckMedStorage(string p_strMedID)
        {
            float fltResult = 0;
            long lngRes = 0;


            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngCheckMdStorage(p_strMedID, out fltResult);

            return fltResult;
        }
        #endregion

        #region ��òֿ��б� ŷ����ΰ 2004-05-25
        /// <summary>
        /// ��òֿ��б�
        /// </summary>
        /// <param name="p_objStorageArr"></param>
        /// <returns></returns>
        public static long s_lngGetStorageList(out clsStorage_VO[] p_objStorageArr)
        {
            //			p_objStorageArr = new  clsStorage_VO[0];
            long lngRes = 0;

            //com.digitalwave.iCare.gui.StorageManage.clsDomainControlStorageBase objManage = new com.digitalwave.iCare.gui.StorageManage.clsDomainControlStorageBase();

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllStroage(out p_objStorageArr);

            return lngRes;

        }
        #endregion

        #region ���Ա���б� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ���Ա���б�
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public static long s_lngGetEmpList(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            lngRes = (new weCare.Proxy.ProxyHisBase()).Service.s_lngGetEmpList(out p_dtbResult);

            return lngRes;

        }
        #endregion

        #region ���Ա���б�

        public long m_lngGetEmployee(out clsEmployeeVO[] p_VoResult)
        {
            long lngRes = 0;
            p_VoResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc HisMedicine = new clsInitStorageMedicineSvc();
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetEmployee(out p_VoResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ӧ��
        public long m_lngGetDept(out clsT_BSE_DEPTDESC_VO[] DeptVO)
        {
            long lngRes = 0;
            clsDomainControlStockMedAppl objStockMed = new clsDomainControlStockMedAppl();
            lngRes = objStockMed.m_lngGetDept(out DeptVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ӧ��
        public long m_lngGetVendor(out clsVendor_VO[] VendorVO)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            System.Security.Principal.IPrincipal objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetVendor(out VendorVO);

            return lngRes;
        }
        #endregion

        #region ���Ա������ֶ�ֵ ŷ����ΰ 2004-05-31
        /// <summary>
        /// ����Դ�ֶ�������������Ա�����ö�ӦĿ���ֶε�ֵ
        /// </summary>
        /// <param name="p_strDestFiled">Ŀ���ֶ�</param>
        /// <param name="p_strSourceFiled">Դ�ֶ�</param>
        /// <param name="p_strValue">Դ�ֶα���</param>
        /// <returns>��ӦĿ�ĵ�ֵ</returns>
        public static string s_strGetEmpInfo(string p_strDestFiled, string p_strSourceFiled, string p_strValue)
        {
            string strResult = "";
            long lngRes = 0;
            System.Data.DataTable dtbResult = null;

            lngRes = s_lngGetEmpList(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        if (dtbResult.Rows[i][p_strSourceFiled].ToString() == p_strValue)
                        {
                            strResult = dtbResult.Rows[i][p_strDestFiled].ToString();
                            break;
                        }
                    }
                }
            }

            return strResult;

        }
        #endregion

        #region ����ҩƷID���ҩƷ��Ϣ ŷ����ΰ 2004-06-01
        /// <summary>
        /// ����ҩƷID���ҩƷ��Ϣ
        /// </summary>
        /// <param name="p_strMedID"></param>
        /// <returns></returns>
        public static clsMedicine_VO s_GetMedicineInfoByID(string p_strMedID)
        {
            long lngRes = 0;

            clsMedicine_VO objResult = new clsMedicine_VO();

            clsMedicine_VO[] objResultArr = new clsMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByID(p_strMedID, out objResultArr);

            if (objResultArr.Length > 0)
            {
                objResult = objResultArr[0];
            }
            return objResult;
        }
        #endregion

        #region ��ҩƷģ��ID����ҩƷ��ŷ����ΰ��2004-05-17
        /// <summary>
        /// ��ҩƷIDģ������ѯҩƷ�б�
        /// </summary>
        /// <param name="p_strMedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public static clsMedicine_VO[] s_GetMedicineListByID(string p_strMedID)
        {
            long lngRes = 0;
            clsMedicine_VO[] p_objResultArr = new clsMedicine_VO[0];

            //clsMedicineSvc objMedicine = (clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicineSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            string strSQL = "WHERE medicineid_chr like '" + p_strMedID + "%' ORDER BY medicineid_chr";
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByAny(strSQL, out p_objResultArr);

            return p_objResultArr;
        }
        #endregion

        #region ��ҩƷģ��ID����ҩƷ��ŷ����ΰ��2004-05-17
        /// <summary>
        /// ��ҩƷIDģ������ѯҩƷ�б�
        /// </summary>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strMedID">ҩƷ����</param>
        /// <returns></returns>
        public static clsMedicine_VO[] s_GetStorageMedListByID(string p_strStorageID, string p_strMedID)
        {
            long lngRes = 0;
            clsMedicine_VO[] p_objResultArr = new clsMedicine_VO[0];

            //clsMedicineSvc objMedicine = (clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicineSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            string strSQL = @",t_bse_storageandmedicine e
							 WHERE a.medicineid_chr = e.medicineid_chr AND
								   e.storageid_chr ='" + p_strStorageID.Trim() + "' AND ";
            strSQL += @" a.medicineid_chr LIKE '" + p_strMedID.Trim() + "%' ";
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByAny(strSQL, out p_objResultArr);

            return p_objResultArr;
        }
        #endregion

        #region ���ݵ�λID��õ�λ��Ϣ ŷ����ΰ 2004-06-01
        /// <summary>
        /// ���ݵ�λID��õ�λ��Ϣ
        /// </summary>
        /// <param name="p_strUnitID"></param>
        /// <returns></returns>
        public static clsMedUnitAndUnit s_GetUnitInfoByID(string p_strMedID, string p_strUnitID)
        {
            long lngRes = 0;

            clsMedUnitAndUnit[] objResultArr = new clsMedUnitAndUnit[0];

            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByID(p_strMedID, p_strUnitID, out objResultArr);

            return objResultArr[0];
        }
        #endregion

        #region ����ҩƷID��õ�λ��Ϣ��ŷ����ΰ��2004-06-01
        /// <summary>
        /// ����ҩƷID��õ�λ��Ϣ
        /// </summary>
        /// <param name="p_strMedID"></param>
        /// <returns></returns>
        public static clsMedUnitAndUnit[] s_FindUnitByMedID(string p_strMedID)
        {
            clsMedUnitAndUnit[] objResultArr;
            long lngRes = 0;
            objResultArr = new clsMedUnitAndUnit[0];

            //clsMedicineSvc objMedicine = (clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicineSvc));
            System.Security.Principal.IPrincipal objPrincipal = null;

            //			string strSQL = "WHERE medicineid_chr like '" + p_strMedID + "%'";
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByMedID(p_strMedID, out objResultArr);

            return objResultArr;
        }
        #endregion

        #region ���ҩƷ��λ��ĳ��ֵ ŷ����ΰ 2004-06-01
        /// <summary>
        /// ���ҩƷ��λ��ĳ���ֶ�ֵ������FiledֵΪid �� name
        /// </summary>
        /// <param name="strDestFiled"></param>
        /// <param name="strSourceFiled"></param>
        /// <param name="strValue"></param>
        /// <param name="strMedID"></param>
        /// <returns></returns>
        public static string s_GetUnitFiledValue(string strDestFiled, string strSourceFiled, string strValue, string strMedID)
        {
            string strResult = "";
            long lngRes = 0;

            clsMedUnitAndUnit[] objResultArr = new clsMedUnitAndUnit[0];

            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByMedID(strMedID, out objResultArr);

            if (objResultArr.Length > 0)
            {
                for (int i = 0; i < objResultArr.Length; i++)
                {
                    switch (strSourceFiled)
                    {
                        case "id":
                            if (strValue == objResultArr[i].m_objBigUnit.m_strUnitID)
                            {
                                strResult = objResultArr[i].m_objBigUnit.m_strUnitName;
                            }
                            break;
                        case "name":
                            if (strValue == objResultArr[i].m_objBigUnit.m_strUnitName)
                            {
                                strResult = objResultArr[i].m_objBigUnit.m_strUnitID;
                            }
                            break;
                        default:
                            strResult = "";
                            break;

                    }
                }
            }
            return strResult;

        }
        #endregion

        #region ���ҩƷ�Ĳο��� ŷ����ΰ 2004-06-01
        public static float s_fltCheckMedConPrice(string p_strMedID)
        {
            float fltResult = 0;
            //			long lngRes = 0;
            return fltResult;
        }
        #endregion

        #region ����������б�  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ����������б�
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable s_GetPeriodTable()
        {
            System.Data.DataTable dtbResult = new DataTable();
            long lngRes = 0;
            clsDomainControlStorageAidInfo objManage = new clsDomainControlStorageAidInfo();

            lngRes = objManage.m_lngFindAllPeriod(out dtbResult);

            return dtbResult;
        }
        #endregion

        #region ������ͷ
        /// <summary>
        /// ������ͷ
        /// </summary>
        /// <param name="dtsConstruct">�����ñ�</param>
        /// <param name="strColName">��ͷ��</param>
        /// <param name="strFieldName">�ֶ���</param>
        /// <param name="intWidth">���</param>
        public static void s_SetTableStyle(DataGridTableStyle dtsConstruct, string strColName, string strFieldName, int intWidth)
        {
            DataGridTextBoxColumn ColumnStyle = new DataGridTextBoxColumn();//��������ͷ
            ColumnStyle.HeaderText = strColName;
            ColumnStyle.MappingName = strFieldName;
            ColumnStyle.Width = intWidth;
            ColumnStyle.Alignment = HorizontalAlignment.Center;
            ColumnStyle.NullText = "";
            dtsConstruct.GridColumnStyles.Add(ColumnStyle);
        }
        #endregion

        #region ����������б�
        /// <summary>
        /// ����������б�
        /// </summary>
        /// <returns></returns>
        public static clsPeriod_VO[] s_GetPeriodList()
        {
            clsPeriod_VO[] objResultArr = new clsPeriod_VO[0];
            long lngRes = 0;
            clsDomainControlStorageAidInfo objManage = new clsDomainControlStorageAidInfo();

            lngRes = objManage.m_lngFindAllPeriod(out objResultArr);

            return objResultArr;
        }
        #endregion

        #region ��õ�ǰ������  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��õ�ǰ������
        /// </summary>
        /// <returns></returns>
        public static clsPeriod_VO s_GetCurPeriod()
        {
            clsPeriod_VO objResult = new clsPeriod_VO();

            clsPeriod_VO[] objTmpArr = new clsPeriod_VO[0];
            long lngRes = 0;
            clsDomainControlStorageAidInfo objManage = new clsDomainControlStorageAidInfo();

            lngRes = objManage.m_lngGetCurrentPeriod(out objTmpArr);

            if (lngRes > 0)
            {
                objResult = objTmpArr[0];
            }
            return objResult;
        }
        #endregion

        #region ��ҩƷģ��ID���ҿ��ҩƷ��ŷ����ΰ��2004-06-17
        /// <summary>
        /// ��ҩƷIDģ������ѯ���ҩƷ�б�
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public static long s_lngFindStorageMedicineByID(string p_strSQL, out clsStorageMedDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc objStorageMedDetail = (com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc));

            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindMedStoDetailByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ģ�����ҿ��ҩƷ��ŷ����ΰ��2004-05-17
        /// <summary>
        /// ��SQL�ű�ģ������ѯ���ҩƷ�б�
        /// </summary>
        /// <param name="p_strSQL">SQL�ű�</param>
        /// <param name="p_objResultArr">����ֵ</param>
        /// <returns></returns>
        public static long s_lngFindStorageMedicineByAny(string p_strSQL, out clsStorageMedDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc objStorageMedDetail = (com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc));

            System.Security.Principal.IPrincipal objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindMedStoDetailByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ������е�ҩƷ����
        /// <summary>
        /// ������е�ҩƷ����
        /// <param name="p_objResultArr">�������</param>
        /// <param name="strageID">��ID</param>
        /// </summary>
        public static long m_lngGetMed(string strageID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsDomainConrol_Medicne objManage = new clsDomainConrol_Medicne();
            lngRes = objManage.m_lngGetMed(strageID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public static long m_lngGetVendorName(string vendorID, out string vendorName)
        {
            long lngRes = 0;
            vendorName = "";
            clsDomainConrol_Medicne objManage = new clsDomainConrol_Medicne();
            lngRes = objManage.m_lngGetVendorName(vendorID, out vendorName);
            return lngRes;
        }
        #endregion

        #region ��ҩƷID�Ͳֿ�IDģ�����ҿ��ҩƷ��ŷ����ΰ��2004-05-17
        /// <summary>
        /// ��ҩƷID�Ͳֿ�ģ����ѯ���ҩƷ�б�
        /// </summary>
        /// <param name="p_strStorageId">�ֿ����</param>
        /// <param name="p_strMedID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public static long s_lngFindStorageMedicineByMedIDAndStorageID(string p_strStorageId, string p_strMedID, out clsStorageMedDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc objStorageMedDetail = (com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc));
            System.Security.Principal.IPrincipal objPrincipal = null;
            string strSQL = " WHERE storageid_chr='" + p_strStorageId + "' AND medicineid_chr like '" + p_strMedID + "%'";
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindMedStoDetailByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion
        public static int FindItemByValues1(ListView lvwItem, int MatchCol, string strValues)
        {
            if (MatchCol == 2)
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol - 1].Text.IndexOf(strValues) == 0
                        || lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues.ToUpper()) == 0)
                        return i1;
                }
            }
            else
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues) == 0)
                        return i1;
                }
            }
            return -1;
        }
        #region ����û��������ݵ�����
        /// <summary>
        /// ����û��������ݵ�����
        /// </summary>
        /// <param name="strVal">1-���֣�2-���ģ�3-Ӣ�ģ�4-Ӣ�ļ������Ļ����ļ���Ӣ���ַ�</param>
        /// <returns></returns>
        public static int IsEngOrNumOrChina(string strVal)
        {
            //ת���������ĵ��ַ���
            if (IsNumber(strVal))
                return 1;//����
            Byte[] byteArr = null;
            try
            {
                byteArr = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strVal);
            }
            catch { }
            if (byteArr.Length % 2 == 0 && strVal.Length == byteArr.Length)
            {
                return 3;//Ӣ�Ļ�Ӣ�ļ�������
            }
            if (byteArr.Length % 2 == 0)
            {
                return 2;//����
            }
            else if (strVal.Length == byteArr.Length)
            {
                return 3;//Ӣ�Ļ�Ӣ�ļ�������
            }
            else
            {
                return 4;//Ӣ�ļ������Ļ����ļ���Ӣ���ַ�
            }
        }
        #endregion
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static bool IsNumber(string strVal)
        {
            if (strVal == null)
                return false;
            strVal = ReplaceTo(strVal);
            if (strVal == "")
                return false;
            for (int i = 0; i < strVal.Length; i++)
            {
                if (!char.IsNumber(strVal.ToCharArray()[i]))
                    return false;
            }
            return true;
        }

        #region Creage by Sam 2004-5-24
        //�ض�������
        public object[] redefineArray(int newELementCount, object[] originalArray)
        {
            object[] temp = new object[newELementCount];
            if (newELementCount >= originalArray.Length)
            {
                for (int i = 0; i < originalArray.Length; i++)
                {
                    temp[i] = originalArray[i];
                }
            }
            else
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = originalArray[i];
                }
            }
            return temp;
        }
        #region ����listView�е���Ŀ
        /// <summary>
        /// ����listView�е���Ŀ
        /// </summary>
        /// <param name="lvwItem"></param>
        /// <param name="findType">1-���������в��ң��κ����͵��ַ�����2-�Ӵ����в��ң����֣���3-�������Ʋ��ң����ģ���4-ƴ������������ң�Ӣ�ģ�</param>
        /// <param name="MatchCol">��listView�еĵڼ��п�ʼ����</param>
        /// <param name="strValues"></param>
        /// <param name="isCode">��ʶ��������listView���Ƿ���ڲ���ʶ���ڵ��к�,-1-������</param>
        /// <returns></returns>
        public static int FindItemByValues(ListView lvwItem, int findType, int MatchCol, int isCode, string strValues)
        {
            if (isCode != -1)
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[isCode].Text.IndexOf(strValues) == 0)
                        return i1;
                }
            }
            if (findType == 2)//�Ӵ����в���
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues) == 0)
                        return i1;
                }
            }
            if (findType == 3)//�������Ʋ���
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues) == 0)
                        return i1;
                }
            }
            if (findType == 4)//ƴ�������������
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues.ToUpper()) == 0 || lvwItem.Items[i1].SubItems[MatchCol + 1].Text.IndexOf(strValues.ToUpper()) == 0)
                        return i1;
                }
            }
            return -1;
        }
        #endregion
        public static string IsNullToString(string strValues, string IsZero)
        {
            strValues = ReplaceTo(strValues);
            if (strValues == null)
            {
                if (IsZero != null)
                    return IsZero;
                else
                    return strValues;
            }
            else
            {
                if (strValues != "")
                    return strValues;
                else
                {
                    if (IsZero != null)
                        return IsZero;
                    else
                        return strValues;
                }
            }
        }
        public static string ReplaceTo(string strValues)
        {
            strValues = strValues.Replace(" ", "");
            strValues = strValues.Replace("'", "");
            return strValues;
        }
        public static bool ValNumer(char KeyChar, string strTxt)
        {
            if (KeyChar == (char)8) //ɾ����
                return true;
            if (strTxt == null)//ֻ����������
            {
                return char.IsDigit(KeyChar);
            }
            else
            {
                if (char.IsDigit(KeyChar))
                    return true;
                else
                {
                    if (KeyChar == '.') //�����С����
                    {
                        if (strTxt.IndexOf(".") >= 0) //����Ѿ���С����
                            return false;
                        else
                            return true;
                    }
                    else //��������Ҳ����С����
                        return false;
                }
            }
        }
    }
    //���¶���һListView�Ա�������
    /// <summary>
    ///  ��ListView�������� Create By Sam 2004-5-25
    /// </summary>
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
        public int Compare(object x, object y)
        {
            int i = 0;
            i = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);

            if (i > 0)//strA����strB
            {
                if (IsAsc == true)//���������
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            if (i < 0)//strAС��strB
            {
                if (IsAsc == true)//���������
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else //���
            {
                return 0;
            }
            //			return i;
        }
        #endregion
        /// <summary>
        /// ����listView�е���Ŀ
        /// </summary>
        /// <param name="lvwItem"></param>
        /// <param name="MatchCol"></param>
        /// <param name="strValues"></param>
        /// <returns></returns>
        public static int FindItemByValues(ListView lvwItem, int MatchCol, string strValues)
        {
            if (MatchCol == 2)
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol - 1].Text.IndexOf(strValues) == 0
                        || lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues.ToUpper()) == 0)
                        return i1;
                }
            }
            else
            {
                for (int i1 = 0; i1 < lvwItem.Items.Count; i1++)
                {
                    if (lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues) == 0)
                        return i1;
                }
            }
            return -1;
        }
        public static int FindItemByValues(DataGrid dg, int MatchCol, string strValues)
        {


            for (int i1 = 0; i1 < dg.VisibleRowCount; i1++)
            {
                if (dg[i1, MatchCol].ToString().Trim() == strValues)
                    return i1;
            }
            return -1;
        }
        /// <summary>
        /// ��nullת����""
        /// </summary>
        /// <param name="strValues"></param>
        /// <param name="IsZero"></param>
        /// <returns></returns>
        public static string IsNullToString(string strValues, string IsZero)
        {
            if (strValues == null)
            {
                if (IsZero != null)
                    return IsZero;
                else
                    return "";
            }
            else
            {
                strValues = ReplaceTo(strValues);
                if (strValues != "")
                    return strValues;
                else
                {
                    if (IsZero != null)
                        return IsZero;
                    else
                        return strValues;
                }
            }
        }
        /// <summary>
        /// ȥ���ո�͵�����
        /// </summary>
        /// <param name="strValues"></param>
        /// <returns></returns>
        public static string ReplaceTo(string strValues)
        {
            strValues = strValues.Trim();
            strValues = strValues.Replace(" ", "");
            strValues = strValues.Replace("'", "");
            return strValues;
        }
        /// <summary>
        /// ֻ����������strTxt��Ϊ��ʱ������С����
        /// </summary>
        /// <param name="KeyChar"></param>
        /// <param name="strTxt"></param>
        /// <returns></returns>
        public static bool ValNumer(char KeyChar, string strTxt)
        {
            if (KeyChar == (char)8) //ɾ����
                return true;
            if (strTxt == null)//ֻ����������
            {
                return char.IsDigit(KeyChar);
            }
            else
            {
                if (char.IsDigit(KeyChar))
                    return true;
                else
                {
                    if (KeyChar == '.') //�����С����
                    {
                        if (strTxt.IndexOf(".") >= 0) //����Ѿ���С����
                            return false;
                        else
                            return true;
                    }
                    else //��������Ҳ����С����
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ǵ���һ��С���������
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static bool IsNumberWithPoint(string strVal)
        {
            if (strVal == null)
                return false;
            strVal = ReplaceTo(strVal);
            if (strVal == "")
                return false;
            bool WithPoint = false;
            for (int i = 0; i < strVal.Length; i++)
            {
                if (strVal.ToCharArray()[i] == '.')
                {
                    if (!WithPoint) //û�й�С����
                        WithPoint = true;
                    else
                        return false;
                }
                else
                {
                    if (!char.IsNumber(strVal.ToCharArray()[i]))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static bool IsEngOrNum(string strVal)
        {
            if (IsNumber(strVal))
                return true;
            ASCIIEncoding AE = new ASCIIEncoding();
            byte[] ByteArray = AE.GetBytes(strVal);

            for (int x = 0; x <= ByteArray.Length - 1; x++)
            {
                if (ByteArray[x] < 0) //�������ַ�
                    return false;
            }
            return true;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static bool IsNumber(string strVal)
        {
            if (strVal == null)
                return false;
            strVal = ReplaceTo(strVal);
            if (strVal == "")
                return false;
            for (int i = 0; i < strVal.Length; i++)
            {
                if (!char.IsNumber(strVal.ToCharArray()[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// ��ʽ��Grid ����strColNameΪ��ʾ��������strFieldNameΪ�󶨵��ֶΡ�
        /// </summary>
        /// <param name="dtsConstruct"></param>
        /// <param name="strColName"></param>
        /// <param name="strFieldName"></param>
        /// <param name="intWidth"></param>
        public static void m_SetTableStyle(DataGridTableStyle dtsConstruct, string strColName, string strFieldName, int intWidth)
        {
            DataGridTextBoxColumn ColumnStyle = new DataGridTextBoxColumn();//��������ͷ
            ColumnStyle.HeaderText = strColName;
            ColumnStyle.MappingName = strFieldName;
            ColumnStyle.Width = intWidth;
            ColumnStyle.Alignment = HorizontalAlignment.Center;
            ColumnStyle.NullText = "";
            dtsConstruct.GridColumnStyles.Add(ColumnStyle);
        }
    }
    public class ComboBoxItem
    {
        internal ArrayList sText;
        internal ArrayList sValue;
        private exComboBox objCombo;
        public ComboBoxItem(exComboBox source)
        {
            this.objCombo = source;
            sText = new ArrayList();
            sValue = new ArrayList();
        }
        public void Add(string Text, string Value)
        {
            //this.sText=Text;
            this.objCombo.Items.Add(Text);
            //this.Value=Value;
            sText.Add(Text);
            sValue.Add(Value);
            //this.Add(Text);
        }
        public string this[int index]
        {
            get
            {
                string tmpText;
                if (index > -1 && sText.Count > 0)
                    tmpText = sText[index].ToString();
                else
                    tmpText = "";
                return tmpText;
            }
        }
    }

    public class exTreeNode : TreeNode
    {
        private string sKey;
        public string Key
        {
            get
            {
                return sKey;
            }
            set
            {
                sKey = value;
            }
        }
    }
    public class exComboBox : ComboBox
    {
        public ComboBoxItem Item;

        public exComboBox()
        {
            Item = new ComboBoxItem(this);
        }
        /// <summary>
        /// ��ǰѡ���ֵ
        /// </summary>
        public string SelectItemValue
        {
            get
            {
                string tmp = "";
                if (Item.sValue.Count == 0)
                    return tmp;
                if (SelectedIndex > -1)
                    tmp = Item.sValue[SelectedIndex].ToString();
                else
                    tmp = "";
                return tmp;
            }
        }

        /// <summary>
        /// ��ǰ��ʾ��ֵ
        /// </summary>
        public string SelectItemText
        {
            get
            {
                string tmp = "";
                if (Item.sText.Count == 0)
                    return tmp;
                if (SelectedIndex > -1)
                    tmp = Item.sText[SelectedIndex].ToString();
                else
                    tmp = "";
                return tmp;
            }
        }
        public void Clear()
        {
            this.SelectedIndex = -1;
            this.Text = "";
        }
        /// <summary>
        /// ���ұ����ֵ
        /// </summary>
        /// <param name="Key"></param>
        public void FindKey(string Key)
        {
            this.Clear();
            if (clsPublicParm.IsNullToString(Key, null) == "")
                return;
            if (Item.sValue.Count == 0)
                return;
            for (int i1 = 0; i1 < this.Items.Count; i1++)
            {
                if (Item.sValue[i1].ToString() == Key)
                {
                    this.SelectedIndex = i1;
                    return;
                }
            }
        }
        /// <summary>
        /// ȥ���ո�͵�����
        /// </summary>
        /// <param name="strValues"></param>
        /// <returns></returns>
        public static string ReplaceTo(string strValues)
        {
            strValues = strValues.Trim();
            strValues = strValues.Replace(" ", "");
            strValues = strValues.Replace("'", "");
            return strValues;
        }

    }
}
