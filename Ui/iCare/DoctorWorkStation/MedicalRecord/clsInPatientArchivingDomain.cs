using System;
using weCare.Core.Entity;
namespace iCare
{
    /// <summary>
    /// Summary description for clsInPatientArchivingDomain.
    /// </summary>
    public class clsInPatientArchivingDomain
    {

        //		clsInPatientArchivingService m_objServ=new clsInPatientArchivingService();
        public clsInPatientArchivingDomain()
        {
        }
        //
        // TODO: Add constructor logic here
        //

        //		public long lngGetOneRecord(string p_strInPatientID,string p_strInPatientDate,out clsInPatientArchivingValue p_objArchivingValue)
        //		{
        //			return m_objServ.lngGetOneRecord( p_strInPatientID,p_strInPatientDate,out p_objArchivingValue);
        //		}

        /// <summary>
        /// ��סԺ�Ų�ѯ���˳�Ժ״̬
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_intStatus">���p_intStatus=0���ѯδ�鵵�Ĳ��ˣ�>=1���ѯ�ѹ鵵����,= -1��ѯȫ��</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        public long lngGetRecordByInPatientIDArr(string p_strInPatientID, string p_strInPatientDate, int p_intStatus, out clsInPatientArchivingValue p_objArchivingValue)
        {
            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            return (new weCare.Proxy.ProxyEmr01()).Service.lngGetRecordByInPatientIDArr(p_strInPatientID, p_strInPatientDate, p_intStatus, out p_objArchivingValue);
        }

        /// <summary>
        /// ��סԺ��ģ����ѯ�ض�������7���ǳ�Ժ����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strDeptIDArr">�ٴ������б���ѯȫԺ����</param>
        /// <param name="p_intStatus">���p_intStatus=0���ѯδ�鵵�Ĳ��ˣ�>=1���ѯ�ѹ鵵����,= -1��ѯȫ��</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        public long lngGetArchiveByInPatientIDLikeArr(string p_strInPatientID, string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {
            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            return (new weCare.Proxy.ProxyEmr01()).Service.lngGetRecordByInPatientIDLikeArr(p_strInPatientID, p_strDeptIDArr, p_intStatus, out p_objArchivingValueArr);
        }
        /// <summary>
        /// ����������ģ����ѯ�ض�������7���ǳ�Ժ����
        /// </summary>
        /// <param name="p_strInPatientName"></param>
        /// <param name="p_strDeptIDArr">�ٴ������б���ѯȫԺ����null</param>
        /// <param name="p_intStatus">���p_intStatus=0���ѯδ�鵵�Ĳ��ˣ�>=1���ѯ�ѹ鵵����,= -1��ѯȫ��</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        public long lngGetArchiveByInPatientNameLikeArr(string p_strInPatientName, string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {

            //clsInPatientArchivingService objServ =
            //        (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            return (new weCare.Proxy.ProxyEmr01()).Service.lngGetRecordByInPatientNameLikeArr(p_strInPatientName, p_strDeptIDArr, p_intStatus, out p_objArchivingValueArr);
        }
        /// <summary>
        /// ��ȡ�ض��ٴ�������7���ڳ�Ժ�����б�
        /// </summary>
        /// <param name="p_strDeptIDArr">�ٴ������б���ѯȫԺ����null</param>
        /// <param name="p_intStatus">���p_intStatus=0���ѯδ�鵵�Ĳ��ˣ�>=1���ѯ�ѹ鵵����,= -1��ѯȫ��</param>
        /// <param name="p_objArchivingValueArr"></param>
        /// <returns></returns>
        public long lngGetRecordWithin7DayByEmpDeptArr(string[] p_strDeptIDArr, int p_intStatus, out clsInPatientArchivingValue[] p_objArchivingValueArr)
        {
            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            return (new weCare.Proxy.ProxyEmr01()).Service.lngGetRecordWithin7DayByEmpDeptArr(p_strDeptIDArr, p_intStatus, out p_objArchivingValueArr);
        }

        /// <summary>
        /// �ô����ڴ򿪲��˵ĵ�ʱ���жϡ����ж�Ҫ�����ݿ⣬����ֻ��frmHRPBaseForm�е��ô˺���
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_blnIsReadOnly"></param>
        /// <param name="p_strTimeRemaining"></param>
        /// <returns></returns>
        public long lngIsReadOnly(string p_strInPatientID, string p_strInPatientDate, out bool p_blnIsReadOnly, out string p_strTimeRemaining)
        {
            p_strTimeRemaining = null;
            p_blnIsReadOnly = false;
            //��ʱδ�鵵���ѹ鵵�Ķ�Ӧ��Ϊֻ��
            clsInPatientArchivingValue objValue = null;

            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngCheckFormReadOnly(p_strInPatientID, p_strInPatientDate, out objValue);
            if (lngRes <= 0) return lngRes;
            if (objValue != null)
            {
                if (objValue.m_strIfArchived == "1")
                {
                    p_blnIsReadOnly = true;
                }
                else
                {
                    //int intLeaveDay = int.Parse(objValue.m_strLeaveDay);
                    if (objValue.m_intLeaveDay < 0 || (objValue.m_intLeaveDay == 0 && objValue.m_intLeaveHour <= 0))
                        p_blnIsReadOnly = true;
                    else
                        p_blnIsReadOnly = false;
                    p_strTimeRemaining = objValue.m_intLeaveDay.ToString() + "��" + objValue.m_intLeaveHour.ToString() + "Сʱ";
                }

            }

            return lngRes;
        }

        /// <summary>
        /// �鵵
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_blnSetArchived"></param>
        /// <returns></returns>
        public long lngSetArchived(string p_strInPatientID, string p_strInPatientDate, out string[] p_strIsArchived)
        {
            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngSetArchived(p_strInPatientID, p_strInPatientDate, clsEMRLogin.LoginInfo.m_strEmpID, out p_strIsArchived);

            return lngRes;
        }
        public long lngCancelArchived(string p_strInPatientID, string p_strInPatientDate)
        {
            //clsInPatientArchivingService objServ =
            //    (clsInPatientArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientArchivingService));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngUnsetArchived(p_strInPatientID, p_strInPatientDate, clsEMRLogin.LoginInfo.m_strEmpID);

            return lngRes;
        }
        /// <summary>
        /// �жϸò����Ƿ�ʱδ�鵵��Ҫ�����ݿ⡣
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_blnIsOverTime"></param>
        /// <returns></returns>
        public long lngIfOverTime(string p_strInPatientID, string p_strInPatientDate, out bool p_blnIsOverTime)
        {
            p_blnIsOverTime = false;
            //			clsInPatientArchivingValue objValue=null;
            //			long lngRes=new clsInPatientArchivingService().lngGetOneRecord( p_strInPatientID,p_strInPatientDate,out objValue);
            //			if(lngRes<=0)return lngRes;
            //			if(objValue!=null)
            //			{
            //				if(objValue.m_strIfArchived=="1")
            //				{
            //					p_blnIsOverTime=false;
            //				}
            //				else if(objValue.m_strEndReason=="0" || objValue.m_strEndReason=="2")
            //				{
            //					TimeSpan objSpan= DateTime.Parse( new clsPublicDomain().m_strGetServerTime())-DateTime.Parse( objValue.m_strInPatientEndDate);
            //					if(objSpan.TotalHours>24) p_blnIsOverTime=true;	
            //					else
            //					{
            //						p_blnIsOverTime=false;
            //					}															   
            //				}
            //				else if(objValue.m_strEndReason=="1" )
            //				{
            //					TimeSpan objSpan= DateTime.Parse( new clsPublicDomain().m_strGetServerTime())-DateTime.Parse( objValue.m_strInPatientEndDate);
            //					if(objSpan.TotalHours>6) p_blnIsOverTime= true;	
            //					else
            //					{
            //						p_blnIsOverTime=false;
            //					}
            //				}
            //				
            //			}

            return 0;//lngRes;
        }
        /// <summary>
        /// �жϸò����Ƿ�ʱ����������������ݿ�
        /// </summary>
        /// <param name="p_objArchivingValue"></param>
        /// <returns></returns>
        public bool m_blnIfOverTime(clsInPatientArchivingValue p_objArchivingValue)
        {
            if (p_objArchivingValue == null || p_objArchivingValue.m_strIfArchived == "1") return false;
            if (p_objArchivingValue.m_strEndReason == "0" || p_objArchivingValue.m_strEndReason == "2")
            {
                TimeSpan objSpan = DateTime.Parse(new clsPublicDomain().m_strGetServerTime()) - DateTime.Parse(p_objArchivingValue.m_strInPatientEndDate);
                if (objSpan.TotalHours > (24 * 7)) return true;
            }
            else if (p_objArchivingValue.m_strEndReason == "1")
            {
                TimeSpan objSpan = DateTime.Parse(new clsPublicDomain().m_strGetServerTime()) - DateTime.Parse(p_objArchivingValue.m_strInPatientEndDate);
                if (objSpan.TotalHours > 6) return true;
            }
            return false;

        }

    }
}

