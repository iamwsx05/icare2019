//using System;
//using com.digitalwave.iCare.middletier.HRPService;	//HRPService.dll

//namespace com.digitalwave.iCare.middletier.HIS
//{
//	/// <summary>
//	/// ����IDataParameter ����ĸ�����
//	/// </summary>
//	public class clsIDataParameterCreator
//	{
//		/// <summary>
//		/// ���ɲ���
//		/// </summary>
//		/// <param name="p_objParamArr"></param>
//		/// <returns></returns>
//		public static System.Data.IDataParameter[] s_objConstructIDataParameterArr(params object[] p_objParamArr)
//		{
//			if(p_objParamArr.Length == 0)
//				return null;

//			int intLength = p_objParamArr.Length;
//			System.Data.IDataParameter[] objReqCheckDetailArr= null;
//			com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
//			objHRPSvc.CreateDatabaseParameter(intLength,out objReqCheckDetailArr);
			

//			for(int i=0;i<intLength;i++)
//			{
//				objReqCheckDetailArr[i].Value = p_objParamArr[i];
//			}

//			return objReqCheckDetailArr;
//		}
//	}
//}
