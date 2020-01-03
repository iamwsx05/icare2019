using System;
using System.Collections;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;
using ctlSecurForm;

namespace iCare
{
    /// <summary>
    /// ����ǩ��ʹ����
    /// </summary>
    public class clsElectronIdiographDomain
    {
        //private clsElectronIdiograph m_objElectronIdiograph;

        /// <summary>
        /// ǩ������������
        /// </summary>
        private Hashtable m_hasIdiographResult;

        /// <summary>
        /// ʹ�ô˶���ĵ�ǰ���塣
        /// </summary>
        private string m_strFormName = "";

        private ctlSecurForm.ctlSecurForm objSecurForm;

        /// <summary>
        ///  ����1
        /// </summary>
        /// <param name="p_strFormName">ʹ�ô˶���õ�ǰ����</param>
        public clsElectronIdiographDomain(string p_strFormName)
        {
            //m_objElectronIdiograph = new clsElectronIdiograph();
            m_hasIdiographResult = new Hashtable();

            objSecurForm = new ctlSecurForm.ctlSecurForm();


            if (p_strFormName != null)
                m_strFormName = p_strFormName;
        }

        /// <summary>
        /// ǩ�����
        /// </summary>
        public Hashtable MP_HasIdiographResult
        {
            get { return m_hasIdiographResult; }
        }

        /// <summary>
        /// ͨ������õ�����ǩ��(����ArrayList�ĸ���Ҫ��ͬ)
        /// </summary>
        /// <param name="p_strInPatientID">����סԺ��</param>
        /// <param name="p_strInPatientDate">����סԺ����</param>
        /// <param name="p_altIdiographObject">��Ҫǩ�����󼯺�</param>
        /// <param name="p_altIdiographObjectContent">��Ҫǩ����������ݼ���</param>
        public void m_mthGetIdiograph(ArrayList p_altIdiographObject, ArrayList p_altIdiographObjectContent)
        {
            m_hasIdiographResult.Clear();

            if (p_altIdiographObject == null)
                return;

            for (int i = 0; i < p_altIdiographObject.Count; i++)
            {
                #region ��ȡǩ�����÷���
                //((Control)p_altIdiographObject).Text
                string strIdiograph = objSecurForm.sign(p_altIdiographObjectContent[i].ToString(), false);
                #endregion ��ȡǩ�����÷���

                m_hasIdiographResult.Add(p_altIdiographObject[i].ToString(), strIdiograph);
            }
        }


        /// <summary>
        /// �õ����ݿ��ж�Ӧ��Ŀ��ǩ��
        /// </summary>
        /// <param name="p_strInPatientID">����סԺ��</param>
        /// <param name="p_strInPatientDate">����סԺ����</param>
        /// <param name="p_altIdiographObject">��Ҫ��ѯǩ�����󼯺�(�ַ�������)</param>
        public void m_mthGetIdiograph(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, ArrayList p_altIdiographObject)
        {
            m_hasIdiographResult.Clear();

            string strKey = m_strFormName + "_" + p_strInPatientID + "_" + p_strInPatientDate + "_" + p_strOpenDate;

            DataTable dtRecord = null;

            //clsElectronIdiograph m_objElectronIdiograph =
            //    (clsElectronIdiograph)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsElectronIdiograph));

            (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetIdiographResultArry(strKey, out dtRecord);

            if (dtRecord != null)
            {
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    for (int j = 0; j < p_altIdiographObject.Count; j++)
                    {
                        if (dtRecord.Rows[i]["SIGN_ID_VCHR"].ToString().ToUpper() == (strKey.ToUpper() + "_" + p_altIdiographObject[j].ToString().ToUpper()))
                        {
                            m_hasIdiographResult.Add(p_altIdiographObject[i].ToString(), dtRecord.Rows[i]["SIGN_DATA_VCHR"].ToString());
                            break;
                        }
                    }
                }
            }
        }

        public long m_lngSaveIdiographResultArry(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            System.Collections.Generic.List<string> altkey = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> altIdiographResult = new System.Collections.Generic.List<string>();
            string[] strarrykey = new string[m_hasIdiographResult.Keys.Count];
            m_hasIdiographResult.Keys.CopyTo(strarrykey, 0);

            for (int i = 0; i < strarrykey.Length; i++)
            {
                string strkey = m_strFormName + "_" + p_strInPatientID + "_" + p_strInPatientDate + "_" + p_strOpenDate + "_" + strarrykey[i];
                string strIdiographResult = m_hasIdiographResult[strarrykey[i]].ToString();
                altkey.Add(strkey);
                altIdiographResult.Add(strIdiographResult);
            }

            //clsElectronIdiograph m_objElectronIdiograph =
            //    (clsElectronIdiograph)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsElectronIdiograph));

            return (new weCare.Proxy.ProxyEmr01()).Service.m_lngSaveIdiographResultArry(altkey, altIdiographResult);
        }

        /// <summary>
        /// ��֤ǩ��
        /// </summary>
        /// <param name="p_altIdiographObject">��Ҫ��֤ǩ���Ŀؼ���������</param>
        /// <returns></returns>
        public bool m_blnValidateIdiograph(ArrayList p_altIdiographObject, ArrayList p_altIdiographObjectContent)
        {
            for (int i = 0; i < p_altIdiographObject.Count; i++)
            {
                #region ��ȡǩ�����÷���
                //				string strIdiograph = "AAAAAAAAAAAAAAAAAA";
                #endregion ��ȡǩ�����÷���

                //string strOldIdiograph = m_hasIdiographResult[((Control)p_altIdiographObject[i]).Name].ToString();
                string strOldIdiograph = m_hasIdiographResult[p_altIdiographObject[i].ToString()].ToString();

                //				if (strIdiograph != strOldIdiograph)
                //					return false;

                //return objSecurForm.verify(strOldIdiograph,((Control)p_altIdiographObject[i]).Text,false);
                return objSecurForm.verify(p_altIdiographObjectContent[i].ToString(), strOldIdiograph, false);

            }

            return true;
        }

    }
}
