using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public interface ILisInterface 
    {
        bool m_mthNewApp(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] unitItems, bool isSend);
        clsLISAppResults[] m_objGetMutiResults();
    }

    /// <summary>
    /// סԺҽ����LIS�ӿ�
    /// </summary>
    public class clsBIHLis : ILisInterface
    {
        private clsLisInterface inter = new clsLisInterface();
        private clsLisService service = new clsLisService();

        public clsLISAppResults[] m_objGetMutiResults()
        {
            //return inter.m_objGetMutiResults();
            return service.GetApplyResult();
        }

        public bool m_mthNewApp(clsLisApplMainVO patientInfo, clsTestApplyItme_VO[] unitItems, bool isSend)
        {
            //return inter.m_mthNewApp(p_objPatientInfo, p_objChargeInfoArr, p_blnSend);
            string message;
            string strAge = patientInfo.m_strAge;
            try
            {
                int intAgeNum = 0;
                int idx = 0;
                if (strAge.Contains("��"))
                {
                    idx = strAge.IndexOf("��");
                    intAgeNum = Convert.ToInt32(strAge.Substring(0, idx).Trim());
                    patientInfo.m_strAge = intAgeNum.ToString() + " " + "��";
                }
                else if (strAge.Contains("��"))
                {
                    idx = strAge.IndexOf("��");
                    intAgeNum = Convert.ToInt32(strAge.Substring(0, idx).Trim());
                    patientInfo.m_strAge = intAgeNum.ToString() + " " + "��";
                }
                else if (strAge.Contains("��"))
                {
                    idx = strAge.IndexOf("��");
                    intAgeNum = Convert.ToInt32(strAge.Substring(0, idx).Trim());
                    patientInfo.m_strAge = intAgeNum.ToString() + " " + "��";
                }
                else if (strAge.Contains("Сʱ"))
                {
                    idx = strAge.IndexOf("Сʱ");
                    intAgeNum = Convert.ToInt32(strAge.Substring(0, idx).Trim());
                    patientInfo.m_strAge = intAgeNum.ToString() + " " + "Сʱ";
                }
      
            }
            catch
            {

            }
            return service.CreateApply(patientInfo, unitItems, isSend,out message);
        }

        public bool m_mthDeleteApp(string orderId,out string message)
        {
            return service.DeleteOrder(orderId,out message);
        }

        public void m_mthGetLisSample(string p_orderId, out clsT_OPR_LIS_SAMPLE_VO p_objSample)
        {
            p_objSample = null;
            service.GetLisSample(p_orderId, ref p_objSample);
        }
    }
}
