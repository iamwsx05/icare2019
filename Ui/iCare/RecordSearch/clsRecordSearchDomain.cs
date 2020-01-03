using System;
using System.Data;
using weCare.Core.Entity;
using iCare.ICU.Evaluation;

namespace iCare.RecordSearch
{
    /// <summary>
    /// Summary description for clsRecordSearchDomain.
    /// </summary>
    public class clsRecordSearchDomain
    {
        //private clsRecordSearchService m_objService;
        //private CustomFromService.clsMinElementColServ m_objServ;
        //private clsDepartmentHandlerService m_objDeptServ;

        public clsRecordSearchDomain()
        {
            //m_objService = new clsRecordSearchService();
            //m_objServ = new CustomFromService.clsMinElementColServ();
            //m_objDeptServ = new clsDepartmentHandlerService();
        }

        /// <summary>
        /// ��ȡ����Ϣ
        /// </summary>
        /// <returns></returns>
        public clsFormInfo[] m_objGetFormInfoArr()
        {
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetFormInfo( out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }

            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return null;

            clsFormInfo[] objFormInfoArr = new clsFormInfo[dtResult.Rows.Count];
            DataRow objRow = null;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objRow = dtResult.Rows[i];
                clsFormInfo objFormInfo = new clsFormInfo();
                objFormInfo.m_strFormName = objRow["FORMNAME"].ToString();
                objFormInfo.m_strSearchInfoType = objRow["SEARCHINFOTYPE"].ToString();
                objFormInfo.m_strMainSearchInfo = objRow["MAINSEARCHINFO"].ToString();
                objFormInfoArr[i] = objFormInfo;
            }

            return objFormInfoArr;
        }

        /// <summary>
        /// ��ȡ�����ֶ���Ϣ
        /// </summary>
        /// <param name="p_objFormInfo">����Ϣ</param>
        public void m_objGetFieldInfoArr(ref clsFormInfo p_objFormInfo)
        {
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetFieldInfo( p_objFormInfo.m_strFormName, out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return;

            clsFieldInfo[] objFieldInfoArr = new clsFieldInfo[dtResult.Rows.Count];
            DataRow objRow = null;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objRow = dtResult.Rows[i];
                clsFieldInfo objFieldInfo = new clsFieldInfo();
                objFieldInfo.m_strFieldName = objRow["FIELDNAME"].ToString();
                objFieldInfo.m_strConditionFieldType = objRow["CONDITIONFIELDTYPE"].ToString();
                objFieldInfo.m_strConditionFieldName = objRow["CONDITIONFIELDNAME"].ToString();
                objFieldInfoArr[i] = objFieldInfo;
            }

            p_objFormInfo.m_objFieldInfoArr = objFieldInfoArr;
        }

        public clsPatientList[] m_objGetPatientListArr(weCare.Core.Entity.clsRecordSearch_SearchInfo p_objSearchInfo)
        {
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetPatientList(  p_objSearchInfo, out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return null;

            clsPatientList[] objPatientListArr = new clsPatientList[dtResult.Rows.Count];
            DataRow objRow = null;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objRow = dtResult.Rows[i];
                clsPatientList objPatientList = new clsPatientList();
                objPatientList.m_strInPatientNO = objRow["INPATIENTID"].ToString();
                objPatientList.m_strInPatientDate = objRow["INPATIENTDATE"].ToString();
                objPatientList.m_strOpenDate = objRow["OPENDATE"].ToString();
                objPatientList.m_strFirstName = objRow["FIRSTNAME"].ToString();
                objPatientList.m_strSex = objRow["SEX"].ToString();
                objPatientList.m_strAge = objRow["AGE"].ToString();
                objPatientList.m_strCreateDate = objRow["CREATEDATE"].ToString();
                objPatientList.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                objPatientList.m_strCreateUserName = objRow["CREATEUSERNAME"].ToString();
                objPatientListArr[i] = objPatientList;
            }

            return objPatientListArr;
        }

        /// <summary>
        /// ��ѯ���˼�¼��Ϣ
        /// </summary>
        /// <param name="p_strInPatientNO"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetPatientRecordCount(string p_strTypeID, string p_strTypeName, string p_strInPatientNO, string p_strInPatientDate, weCare.Core.Entity.clsCustom_SubmitValue[] p_objCustomForms, out DataTable p_dtbResult)
        {
            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetPatientRecordCount(  p_strTypeID, p_strTypeName, p_strInPatientNO, p_strInPatientDate, p_objCustomForms, out p_dtbResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡר�Ʋ���������ר�Ʋ����ã�
        /// </summary>
        /// <param name="p_strDeptID">����ID</param>
        /// <param name="p_objFormInfoArr"></param>
        /// <returns></returns>
        public long m_lngIMR_GetFormInfo(string p_strDeptID, out clsFormInfo[] p_objFormInfoArr)
        {
            p_objFormInfoArr = null;
            DataTable dtResult;
            string strSearchInfoSql = null;
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440104001")
            {
                strSearchInfoSql = @"select distinct PBI.FirstName,PBI.Sex,DATEDIFF(year, convert(datetime,PBI.Birth,120), getdate()) as Age,
						b.InPatientID,b.InPatientDate,b.OpenDate,EBI_Record.FirstName as CreateUserName,
						b.OpenDate as CreateDate,a.CreateUserID
						from InpatMedRec a inner join InpatMedRec_Item b
						on a.TypeID = b.TypeID
						and a.InPatientID = b.InPatientID
						and a.InPatientDate = b.InpatientDate
						and a.OpenDate = b.OpenDate
						inner join PatientBaseInfo PBI
						on a.InPatientID = PBI.InPatientID
						inner join EmployeeBaseInfo EBI_Record
						on a.CreateUserID = EBI_Record.EmployeeID
						inner join Dept_Employee DE
						on a.CreateUserID = DE.EmployeeID and DE.EndDate = $NULLDate$
						where a.Status = 0
						and a.TypeID = $TypeID$
						and DE.DeptID = $DeptID$
						and ";
            }
            else
            {
                strSearchInfoSql = @"select distinct PBI.FirstName,PBI.Sex,to_number(To_Char(sysdate,'YYYY'))-to_number(To_Char(PBI.Birth,'YYYY')) as Age,
					b.InPatientID,b.InPatientDate,b.OpenDate,EBI_Record.FirstName as CreateUserName,
					b.OpenDate as CreateDate,a.CreateUserID
					from InpatMedRec a inner join InpatMedRec_Item b
					on a.TypeID = b.TypeID
					and a.InPatientID = b.InPatientID
					and a.InPatientDate = b.InpatientDate
					and a.OpenDate = b.OpenDate
					inner join PatientBaseInfo PBI
					on a.InPatientID = PBI.InPatientID
					inner join EmployeeBaseInfo EBI_Record
					on a.CreateUserID = EBI_Record.EmployeeID
					inner join Dept_Employee DE
					on a.CreateUserID = DE.EmployeeID and DE.EndDate = $NULLDate$
					where a.Status = 0
					and a.TypeID = $TypeID$
					and DE.DeptID = $DeptID$
					and ";

            }

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngIMR_GetFormInfo( out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return lngRes;

            p_objFormInfoArr = new clsFormInfo[dtResult.Rows.Count];
            DataRow objRow = null;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                clsFormInfo objFormInfo = new clsFormInfo();
                objRow = dtResult.Rows[i];
                objFormInfo.m_strFormID = objRow["TYPEID"].ToString();
                objFormInfo.m_strFormName = objRow["TYPENAME"].ToString();
                objFormInfo.m_strSearchInfoType = "AND";
                objFormInfo.m_strMainSearchInfo = strSearchInfoSql;
                p_objFormInfoArr[i] = objFormInfo;
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�����ֶ���Ϣ����ר�Ʋ����ã�
        /// </summary>
        /// <param name="p_objFormInfo">����Ϣ</param>
        public void m_mthIMR_GetFieldInfo(ref clsFormInfo p_objFormInfo)
        {
            if (p_objFormInfo == null || p_objFormInfo.m_strFormName == null)
                return;
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngIMR_GetFieldInfo( p_objFormInfo.m_strFormID, out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return;

            clsFieldInfo[] objFieldInfoArr = new clsFieldInfo[dtResult.Rows.Count + 5];

            #region �̶�����
            objFieldInfoArr[0] = new clsFieldInfo();
            objFieldInfoArr[0].m_strFieldName = "סԺ��";
            objFieldInfoArr[0].m_strConditionFieldType = "text";
            objFieldInfoArr[0].m_strConditionFieldName = "b.InPatientID";

            objFieldInfoArr[1] = new clsFieldInfo();
            objFieldInfoArr[1].m_strFieldName = "��������";
            objFieldInfoArr[1].m_strConditionFieldType = "text";
            objFieldInfoArr[1].m_strConditionFieldName = "PBI.FirstName";

            objFieldInfoArr[2] = new clsFieldInfo();
            objFieldInfoArr[2].m_strFieldName = "�Ա�";
            objFieldInfoArr[2].m_strConditionFieldType = "text";
            objFieldInfoArr[2].m_strConditionFieldName = "PBI.Sex";

            objFieldInfoArr[3] = new clsFieldInfo();
            objFieldInfoArr[3].m_strFieldName = "����";
            objFieldInfoArr[3].m_strConditionFieldType = "number";
            objFieldInfoArr[3].m_strConditionFieldName = "datediff(year,PBI.Birth,getdate())";

            objFieldInfoArr[4] = new clsFieldInfo();
            objFieldInfoArr[4].m_strFieldName = "��Ժ����";
            objFieldInfoArr[4].m_strConditionFieldType = "date";
            objFieldInfoArr[4].m_strConditionFieldName = "b.InPatientDate";
            #endregion

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objFieldInfoArr[i + 5] = new clsFieldInfo();
                objFieldInfoArr[i + 5].m_strFieldName = dtResult.Rows[i]["ITEMNAME"].ToString();
                switch (dtResult.Rows[i]["ITEMTYPE"].ToString().Trim())
                {
                    case "RichTextBox":
                    case "ctlBorderTextBox":
                    case "ctlComboBox":
                    case "ctlRichTextBox":
                    default:
                        objFieldInfoArr[i + 5].m_strConditionFieldType = "text";
                        break;
                    case "ctlTimePicker":
                        objFieldInfoArr[i + 5].m_strConditionFieldType = "date";
                        break;
                    case "RadioButton":
                    case "CheckBox":
                        objFieldInfoArr[i + 5].m_strConditionFieldType = "truefalse";
                        break;
                }
                objFieldInfoArr[i + 5].m_strConditionFieldName = @"b.ItemID = '" + dtResult.Rows[i]["ITEMID"].ToString() + "' and b.ItemContent";
            }

            p_objFormInfo.m_objFieldInfoArr = objFieldInfoArr;
        }
        /// <summary>
        ///  ��ȡ������ר�Ʋ����������ݼ����ã�
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objFormInfoArr"></param>
        /// <returns></returns>
        public long m_lngIMR_NewGetFormInfo(/*string p_strDeptID,*/out weCare.Core.Entity.clsInpatMedRec_Type[] p_objFormInfoArr)
        {
            p_objFormInfoArr = null;
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngIMR_GetFormInfo(out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return lngRes;

            p_objFormInfoArr = new clsInpatMedRec_Type[dtResult.Rows.Count];

            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                p_objFormInfoArr[i] = new clsInpatMedRec_Type();
                p_objFormInfoArr[i].m_strTypeID = dtResult.Rows[i]["TYPEID"].ToString();
                p_objFormInfoArr[i].m_strTypeName = dtResult.Rows[i]["TYPENAME"].ToString();
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ�����ֶ���Ϣ�����ݼ����ã�
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="p_objItemInfo"></param>
        public void m_mthIMR_NewGetItemInfo(string p_strFormID, out weCare.Core.Entity.clsInpatMedRec_Type_Item[] p_objItemInfo)
        {
            p_objItemInfo = null;
            if (p_strFormID == null || p_strFormID == "")
                return;
            DataTable dtResult;

            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngIMR_GetFieldInfo(p_strFormID, out dtResult);
            }
            finally
            {
                //m_objService.Dispose();
            }
            if (lngRes <= 0 || dtResult.Rows.Count == 0)
                return;
            p_objItemInfo = new clsInpatMedRec_Type_Item[dtResult.Rows.Count];
            DataRow objRow = null;
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objRow = dtResult.Rows[i];
                clsInpatMedRec_Type_Item objItemInfo = new clsInpatMedRec_Type_Item();
                objItemInfo.m_strTypeID = p_strFormID;
                objItemInfo.m_strItemID = objRow["ITEMID"].ToString();
                objItemInfo.m_strItemName = objRow["ITEMNAME"].ToString();
                objItemInfo.m_strItemType = objRow["ITEMTYPE"].ToString();
                p_objItemInfo[i] = objItemInfo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtbValues"></param>
        /// <returns></returns>
        public long m_lngSearchesBySQL(string p_strSQL, ref DataTable p_dtbValues)
        {
            //clsRecordSearchService m_objService =
            //    (clsRecordSearchService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsRecordSearchService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngSearchesBySQL(p_strSQL, out p_dtbValues);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡָ���Ĵ���
        /// </summary>
        /// <param name="p_mniForm"></param>
        /// <returns></returns>
        public System.Windows.Forms.Form m_frmGetForm(System.Windows.Forms.MenuItem p_mniForm)
        {
            #region �Ҽ��˵�ȫ������

            switch (p_mniForm.Text)
            {
                case "סԺ����":
                case "סԺ����(����¼����)":
                    return new frmInPatientCaseHistory();
                case "סԺ����ģʽ2":
                    return new frmInPatientCaseHistoryMode1();
                case "���̼�¼":
                    return new frmSubDiseaseTrack();
                case "SPECT������뵥":
                    return new frmSPECT();
                case "��ѹ���������뵥":
                    return new frmHighOxygen();
                case "B�ͳ������������뵥":
                    return new frmBUltrasonicCheckOrder();
                case "CT������뵥":
                    return new frmCTCheckOrder();
                case "X�����뵥":
                    return new frmXRayCheckOrder();
                case "���������֯�ͼ쵥":
                    return new frmPathologyOrgCheckOrder();
                case "MRI���뵥":
                    return new frmMRIApply();
                case "ʵ���Ҽ������뵥":
                    return new frmLabAnalysisOrder();
                case "ʵ���Ҽ��鱨�浥":
                    return new frmLabCheckReport();
                case "��ǰС��":
                    return new frmBeforeOperationSummary();
                case "������¼��":
                    return new frmOperationRecordDoctor();
                case "ICUת���¼":
                    return new frmPICUShiftInForm();
                case "ICUת����¼":
                    return new frmPICUShiftOutForm();
                case "SIRS�������":
                    return new frmSIRSEvaluation();
                case "����Glasgow��������":
                    return new frmImproveGlasgowComaEvaluation();
                case "���Է���������":
                    return new frmLungInjuryEvaluation();
                case "������Σ�ز�������":
                    return new frmNewBabyInjuryCaseEvaluation();
                case "С��Σ�ز�������":
                    return new frmBabyInjuryCaseEvaluation();
                case "APACHEII ����":
                    return new frmAPACHEIIValuation();
                case "APACHEIII ����":
                    return new frmAPACHEIIIValuation();
                case "TISS-28����":
                    return new frmTISSValuation();
                case "���Ʒ���":
                    return new frmICUTrend();
                case "סԺ������ҳ":
                    if (clsEMRLogin.m_StrCurrentHospitalNO != null && clsEMRLogin.m_StrCurrentHospitalNO.Trim() == "450101001")//�ж�ҽԺ���ƣ���ʱֱ����Ӳ����Ƚ�
                    {
                        return new frmInHospitalMainRecord_GX();
                    }
                    else
                    {
                        return new frmInHospitalMainRecord();
                    }
                case "�����������ֱ�":
                    return new frmQCRecord();
                case "��Ժ��������":
                    //return new frmInPatientEvaluate();
                    return new frmEMR_InPatientEvaluate();
                case "�� �� ��":
                    if (clsEMRLogin.m_StrCurrentHospitalNO != null && clsEMRLogin.m_StrCurrentHospitalNO.Trim() == "450101001")//�ж�ҽԺ���ƣ���ʱֱ����Ӳ����Ƚ�
                    {
                        return new frmThreeMeasureRecordGN();
                    }
                    else
                    {
                        return new frmThreeMeasureRecord();
                    }
                case "һ�㻤���¼":
                    return new frmMainGeneralNurseRecord();
                case "�۲���Ŀ��¼��":
                    return new frmWatchItemTrack();
                case "Σ�ػ��߻����¼":
                    if (clsEMRLogin.m_StrCurrentHospitalNO != null && clsEMRLogin.m_StrCurrentHospitalNO.Trim() == "450101001")//�ж�ҽԺ���ƣ���ʱֱ����Ӳ����Ƚ�
                    {
                        return new frmIntensiveTendMain_GX();
                    }
                    else
                    {
                        return new frmIntensiveTendMain_FC();
                    }
                case "ICUΣ�ػ��߻����¼":
                    return new frmICUIntensiveTendRecord();
                case "���������¼":
                    return new frmOperationRecord();
                case "������е�����ϵ�����":
                    return new frmOperationEquipmentQty();
                case "��Ժ��¼":
                    return new frmOutHospital();
                case "�����¼":
                    return new frmConsultation();
                case "Σ��֢�໤�����ػ���¼��":
                    return new frmMainICUIntensiveTend();
                case "����ICU���������Ƽ໤��¼��":
                    return new frmMainICUBreath();
                case "Ӱ�񱨸浥":
                    return new frmImageReport();
                case "Ӱ��ԤԼ��ѯ":
                    return new frmImageBookingSearch();
                case "�ĵ�ͼ���뵥":
                    return new iCare.frmEKGOrder();
                case "���Զർ˯��ͼ������뵥":
                    return new iCare.frmNuclearOrder();
                case "��ҽѧ������뵥":
                    return new iCare.frmPSGOrder();
                case "������Ժ������":
                    return new frmEMR_InPatientEvaluate();
                //case "����֪ͨ��":
                //    return new frmOperationRequisition();
                case "����֪��ͬ����":
                    return new frmOpraAnaSignAgree();
                case "��ǰ������ӵ�":
                    return new frmIMR_PrePostOperateSee();
                //case "�����¼��":
                //    return new frmAnaParamSetting();
                case "һ�㻼�߻����¼":
                    return new frmGeneralNurseRecord_GX();
                case "ICU�����¼":
                    return new frmICUNurseRecord_GX();
                case "��Ѫ����ƻ����¼":
                    return new frmCardiovascularTendMain_GX();
                case "����΢��Ѫ�Ǽ���¼��":
                    return new frmMiniBooldSugarChk_GX();
                case "���ICU�໤��¼":
                    return new frmSurgeryICUWardship();
                case "������¼":
                    return new frmDeathRecord();
                case "�����������ۼ�¼":
                    return new frmDeathCaseDiscuss();
                case "24Сʱ�����Ժ��¼":
                    return new frmEMR_OutHospitalIn24Hours();
                case "��Ժ24Сʱ��������¼":
                    return new frmDeathRecordIn24Hours();
                default:
                    break;
            }
            #endregion �Ҽ��˵�ȫ������

            return null;
        }
        /// <summary>
        /// ��ȡ�ؼ������ģ��
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="p_strControlID"></param>
        /// <param name="p_objTemplateInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetTemplateName(string p_strFormID, string p_strControlID, out weCare.Core.Entity.clsTemplateInfo[] p_objTemplateInfoArr)
        {
            //CustomFromService.clsMinElementColServ m_objServ =
            //    (CustomFromService.clsMinElementColServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(CustomFromService.clsMinElementColServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsMinElementColServ_m_lngGetTemplateName(p_strFormID, p_strControlID, out p_objTemplateInfoArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ�ؼ�Ԫ��
        /// </summary>
        /// <param name="p_objTextTemp"></param>
        /// <returns></returns>
        public long m_lngGetTemplateControls(string strTemplateID, out weCare.Core.Entity.clsTemplateControlValue[] arrItems)
        {
            //CustomFromService.clsMinElementColServ m_objServ =
            //    (CustomFromService.clsMinElementColServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(CustomFromService.clsMinElementColServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsMinElementColServ_m_lngGetTemplateControls(strTemplateID, out arrItems);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡҽ��״̬
        /// </summary>
        /// <param name="p_objStatus">ҽ��״̬</param>
        /// <returns></returns>
        public long m_lngGetOrderStatusDicMap(out clsOrderStatus_VO p_objStatus)
        {
            long lngRes = 0;

            //clsQuery8iServ m_objServ =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOrderStatusDicMap(out p_objStatus);
            return lngRes;
        }

        /// <summary>
        /// ��ѯ����������ҽ������
        /// </summary>
        /// <param name="p_strSQL">��ѯ���</param>
        /// <param name="p_dtbPatient">����</param>
        /// <returns></returns>
        public long m_lngGetOrdersPatient(string p_strSQL, out DataTable p_dtbPatient)
        {
            //clsQuery8iServ m_objServ =
            //    (clsQuery8iServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQuery8iServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOrdersPatient(p_strSQL, out p_dtbPatient);
            return lngRes;
        }

        #region Class
        [Serializable]
        public class clsFormInfo
        {
            public string m_strFormID;

            public string m_strFormName;

            public string m_strSearchInfoType;

            public string m_strMainSearchInfo;

            public clsFieldInfo[] m_objFieldInfoArr;

            public override string ToString()
            {
                return m_strFormName;
            }
        }

        [Serializable]
        public class clsFieldInfo
        {
            /// <summary>
            /// �ؼ���������ӦItemName
            /// </summary>
            public string m_strFieldName;
            /// <summary>
            /// �ؼ����ͣ���ӦItemType
            /// </summary>
            public string m_strConditionFieldType;
            /// <summary>
            /// �ؼ�ID����ӦItemID
            /// </summary>
            public string m_strConditionFieldName;

            public override string ToString()
            {
                int intIndex = m_strFieldName.LastIndexOf(">>");
                if (intIndex > 0)
                    return m_strFieldName.Substring(intIndex + 2);
                return m_strFieldName;
            }
        }

        [Serializable]
        public class clsPatientList
        {
            public string m_strInPatientNO;

            public string m_strInPatientDate;

            public string m_strOpenDate;

            public string m_strCreateUserID;

            public string m_strCreateUserName;

            public string m_strCreateDate;

            public string m_strFirstName;

            public string m_strSex;

            public string m_strAge;
        }
        #endregion
    }
}
