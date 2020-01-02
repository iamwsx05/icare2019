using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �޸�סԺ��Domain��
    /// </summary>
    public class clsDcl_ModifyZyh : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// �޸�סԺ��Domain��
        /// </summary>
        public clsDcl_ModifyZyh()
        {
        }

        weCare.Proxy.ProxyIP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP01();
            }
        }

        #region ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// <summary>
        /// ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            return proxy.Service.m_lngGetPatientinfoByZyh(no, out dt, type); 
        }
        #endregion

        #region �ж��º��Ƿ��Ѵ���
        /// <summary>
        /// �ж��º��Ƿ��Ѵ���
        /// </summary>
        /// <param name="newno"></param>
        /// <returns></returns>
        public bool m_blnCheckNewNO(string newno)
        {
            return proxy.Service.m_blnCheckNewNO(newno); 
        }
        #endregion

        #region ����ǰסԺ(����)�Ÿ�Ϊһ�º�
        /// <summary>
        /// ����ǰסԺ(����)�Ÿ�Ϊһ�º�
        /// </summary>
        /// <param name="patientid">���˱��</param>
        /// <param name="regid">��Ժ�ǼǺ�</param>
        /// <param name="currno">��ǰ��</param>
        /// <param name="newno">flag=1 �º� 2 �Զ����ɺ� 3 �ɺ�</param>
        /// <param name="zycs">flag=3ʱ�ɺŴ���+1</param>
        /// <param name="miflag">���סԺ��־</param>
        /// <param name="sameflag">ͬһ���˱�־</param>
        /// <param name="type">0 סԺ��->סԺ�� 1 סԺ��->���ۺ� 2 ���ۺ�->���ۺ� 3 ���ۺ�->סԺ��</param>
        /// <param name="flag">1 �½� 2 �Զ� 3 �ϲ�</param>
        /// <param name="operid">�޸Ĳ���ԱID</param>
        /// <returns>true �ɹ� false ʧ��</returns>
        public bool m_blnModifyNewNO(string patientid, string regid, string currno, ref string newno, int zycs, bool miflag, bool sameflag, int type, int flag, string operid)
        {
            return proxy.Service.m_blnModifyNewNO(patientid, regid, currno, ref newno, zycs, miflag, sameflag, type, flag, operid); 
        }
        #endregion

        #region ���ݲ���ID�͵�ǰ��Ժ����(��ͨסԺ������סԺ)��ȡ��Ӧ��(���ۡ�סԺ)��ʷ��¼
        /// <summary>
        /// ���ݲ���ID�͵�ǰ��Ժ����(��ͨסԺ������סԺ)��ȡ��Ӧ��(���ۡ�סԺ)��ʷ��¼
        /// </summary>
        /// <param name="pid">����ID</param>
        /// <param name="type">��Ժ���� 1 ��ͨסԺ 2 ����סԺ</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetHistoryinfoByPID(string pid, int type, out DataTable dt)
        {
            return proxy.Service.m_lngGetHistoryinfoByPID(pid, type, out dt); 
        }
        #endregion
    }
}
