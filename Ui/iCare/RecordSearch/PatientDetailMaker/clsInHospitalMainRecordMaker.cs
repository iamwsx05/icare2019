using System;
using weCare.Core.Entity;

namespace iCare.RecordSearch.PatientDetailMaker
{
	/// <summary>
	/// Summary description for clsInHospitalMainRecordMaker.
	/// </summary>
	public class clsInHospitalMainRecordMaker : clsPatientDetailMakerBase
	{
		
		public override string m_strMakerPatientDetailDesc(clsPatient p_objPatient,iCare.RecordSearch.clsRecordSearchDomain.clsPatientList p_objPatientList)
		{
			clsInHospitalMainRecordPrintTool objPrintRec = new  clsInHospitalMainRecordPrintTool();
			objPrintRec.m_BlnNeedModifyFlag = false;
			objPrintRec.m_mthSetPrintInfo(p_objPatient,DateTime.Parse(p_objPatientList.m_strInPatientDate),DateTime.Parse(p_objPatientList.m_strOpenDate));
			objPrintRec.m_mthInitPrintContent();
			 clsPrintInfo_InHospitalMainRecord  objRecordInfo = ( clsPrintInfo_InHospitalMainRecord)objPrintRec.m_objGetPrintInfo();
			objPrintRec.m_BlnNeedModifyFlag = true;

			System.Text.StringBuilder sbdTemp = new System.Text.StringBuilder(1000);			
			sbdTemp.Append(p_objPatient.m_StrName+"  "+p_objPatient.m_StrSex+"  "+p_objPatient.m_ObjPeopleInfo.m_StrAge+"\r\n");
			sbdTemp.Append(p_objPatient.m_ObjPeopleInfo.m_StrNationality+"  "+p_objPatient.m_ObjPeopleInfo.m_StrNativePlace+"  "+p_objPatient.m_ObjPeopleInfo.m_StrOccupation+"\r\n");
			sbdTemp.Append(p_objPatient.m_ObjPeopleInfo.m_StrMarried+"  "+p_objPatient.m_ObjPeopleInfo.m_StrHomeAddress+"\r\n");
			sbdTemp.Append("��ͥ�绰��"+p_objPatient.m_ObjPeopleInfo.m_StrHomePhone+"\r\n�ƶ��绰��"+p_objPatient.m_ObjPeopleInfo.m_StrMobile+"\r\n�칫�绰��"+p_objPatient.m_ObjPeopleInfo.m_StrOfficePhone+"\r\n\r\n");
			sbdTemp.Append("��Ժ���ڣ�"+objRecordInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd��")+"\r\n");
			sbdTemp.Append("��Ժ��ϣ�\r\n"+objRecordInfo.m_objCollection.m_objContent.m_strInHospitalDiagnosis+"\r\n\r\n");
			if(objRecordInfo.m_dtmOutHospitalDate != DateTime.Parse("1900-1-1"))
			{
				sbdTemp.Append("��Ժ���ڣ�"+objRecordInfo.m_dtmOutHospitalDate.ToString("yyyy��MM��dd��")+"\r\n");
				sbdTemp.Append("��Ժ��ϣ�\r\n"+objRecordInfo.m_objCollection.m_objContent.m_strMainDiagnosis+"\r\n\r\n");
			}
			if(objRecordInfo.m_objCollection.m_objOperationArr != null)
			{
				sbdTemp.Append("������Ϣ��"+"\r\n");
				for(int i1=0;i1<objRecordInfo.m_objCollection.m_objOperationArr.Length;i1++)
				{
					sbdTemp.Append(DateTime.Parse(objRecordInfo.m_objCollection.m_objOperationArr[i1].m_strOperationDate).ToString("yyyy��MM��dd�� HH:mm")+"\r\n    "+objRecordInfo.m_objCollection.m_objOperationArr[i1].m_strOperationName+"\r\n    ���ߣ�"+objRecordInfo.m_objCollection.m_objOperationArr[i1].m_strOperatorName+"\r\n");				
				}
				sbdTemp.Append("\r\n");
			}
			sbdTemp.Append("��¼�ߣ�"+p_objPatientList.m_strCreateUserName+"\r\n��¼���ڣ�"+DateTime.Parse(p_objPatientList.m_strCreateDate).ToString("yyyy��MM��dd�� HH:mm"));            			
			return sbdTemp.ToString();
		}

		public override string m_StrFormName
		{
			get
			{
				return "סԺ������ҳ";
			}
		}
	}
}
