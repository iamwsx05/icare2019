using System;
using System.Data;
using weCare.Core.Entity;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.iCare.Template.Client;
using System.Collections;

namespace com.digitalwave.iCare.gui.LIS
{

	#region clsLIS_App ���� 2004.05.28

    /// <summary>
    /// ���뵥Biz��
    /// </summary>
	public class clsLIS_App
	{

        #region ˽�г�Ա

        private enmObjectOperationState m_enmOprState;

        private clsLisApplMainVO m_objDataVO;
        private clsLisApplMainVO m_objOriginalDataVO;

        private clsAppCheckReportCollection m_objAppReports;
        private clsAppApplyUnitCollection m_objAppApplyUnits;

        private string m_strAppID; 

        #endregion

        #region ���캯��

        public clsLIS_App(clsLisApplMainVO p_objDataVO)
        {
            this.m_objDataVO = p_objDataVO;
            m_enmOprState = enmObjectOperationState.New;

            m_objAppReports = new clsAppCheckReportCollection();
            m_objAppApplyUnits = new clsAppApplyUnitCollection();
            m_objAppReports.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppReports_evtItemAdded);
            m_objAppApplyUnits.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppApplyUnits_evtItemAdded);
            m_objAppReports.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppReports_evtItemRemoved);
            m_objAppApplyUnits.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppApplyUnits_evtItemRemoved);
        }
        public clsLIS_App(clsLisApplMainVO p_objDataVO, bool p_blnIsOriginal)
        {
            if (p_blnIsOriginal)
            {
                this.m_objDataVO = p_objDataVO;
                this.m_objOriginalDataVO = new clsLisApplMainVO();

                m_mthDataTransfer(p_objDataVO, m_objOriginalDataVO);

                m_enmOprState = enmObjectOperationState.Original;
            }
            else
            {
                this.m_objDataVO = p_objDataVO;
                m_enmOprState = enmObjectOperationState.New;
            }
            m_objAppReports = new clsAppCheckReportCollection();
            m_objAppApplyUnits = new clsAppApplyUnitCollection();
            m_objAppReports.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppReports_evtItemAdded);
            m_objAppApplyUnits.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppApplyUnits_evtItemAdded);
            m_objAppReports.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppReports_evtItemRemoved);
            m_objAppApplyUnits.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppApplyUnits_evtItemRemoved);

        }

        #endregion

		#region VOManage

        /// <summary>
        /// ���뵥��
        /// </summary>
        public string m_StrAppID
        {
            get
            {
                return this.m_objDataVO.m_strAPPLICATION_ID;
            }
            set
            {
                if (this.m_objDataVO.m_strAPPLICATION_ID != value || this.m_strAppID != value)
                {
                    this.m_objDataVO.m_strAPPLICATION_ID = value;
                    this.m_strAppID = value;
                    foreach (clsLIS_AppApplyUnit objUnit in this.m_ObjAppApplyUnits)
                    {
                        objUnit.m_StrAppID = value;
                    }
                    foreach (clsLIS_AppCheckReport objAppReport in this.m_ObjAppReports)
                    {
                        objAppReport.m_StrAppID = value;
                    }
                    if (this.evtApplicationIDChanged != null)
                    {
                        evtApplicationIDChanged(this, new EventArgs());
                    }

                }
            }
        }

        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        public string m_StrMODIFY_DAT
        {
            get
            {
                return this.m_objDataVO.m_strMODIFY_DAT;
            }
        }
        /// <summary>
        /// ���˱�ţ�ÿ�����˵�άһ���
        /// </summary>
        public string m_StrPatientID
        {
            get
            {
                return this.m_objDataVO.m_strPatientID;
            }
            set
            {
                this.m_objDataVO.m_strPatientID = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string m_StrAppDat
        {
            get
            {
                return this.m_objDataVO.m_strAppl_Dat;
            }
            set
            {
                this.m_objDataVO.m_strAppl_Dat = value;
            }
        }
        /// <summary>
        /// �����Ա�
        /// </summary>
        public string m_StrSex
        {
            get
            {
                return this.m_objDataVO.m_strSex;
            }
            set
            {
                this.m_objDataVO.m_strSex = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string m_StrPatientName
        {
            get
            {
                return this.m_objDataVO.m_strPatient_Name;
            }
            set
            {
                this.m_objDataVO.m_strPatient_Name = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string m_StrPatientSubNO
        {
            get
            {
                return this.m_objDataVO.m_strPatient_SubNO;
            }
            set
            {
                this.m_objDataVO.m_strPatient_SubNO = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string m_StrAge
        {
            get
            {
                return this.m_objDataVO.m_strAge;
            }
            set
            {
                this.m_objDataVO.m_strAge = value;
            }
        }
        /// <summary>
        /// ������� �����סԺ���������
        /// </summary>
        public string m_StrPatientType
        {
            get
            {
                return this.m_objDataVO.m_strPatientType;
            }
            set
            {
                this.m_objDataVO.m_strPatientType = value;
            }
        }
        /// <summary>
        /// �ٴ����
        /// </summary>
        public string m_StrDiagnose
        {
            get
            {
                return this.m_objDataVO.m_strDiagnose;
            }
            set
            {
                this.m_objDataVO.m_strDiagnose = value;
            }
        }
        /// <summary>
        /// ���˴���
        /// </summary>
        public string m_StrBedNO
        {
            get
            {
                return this.m_objDataVO.m_strBedNO;
            }
            set
            {
                this.m_objDataVO.m_strBedNO = value;
            }
        }
        /// <summary>
        /// �ٴ����ICD��
        /// </summary>
        public string m_StrICD
        {
            get
            {
                return this.m_objDataVO.m_strICD;
            }
            set
            {
                this.m_objDataVO.m_strICD = value;
            }
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        public string m_StrPatientCardID
        {
            get
            {
                return this.m_objDataVO.m_strPatientcardID;
            }
            set
            {
                this.m_objDataVO.m_strPatientcardID = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string m_StrApplicationFormNO
        {
            get
            {
                return this.m_objDataVO.m_strApplication_Form_NO;
            }
            set
            {
                this.m_objDataVO.m_strApplication_Form_NO = value;
            }
        }
        /// <summary>
        /// ����Ա��ID
        /// </summary>
        public string m_StrOperatorID
        {
            get
            {
                return this.m_objDataVO.m_strOperator_ID;
            }
            set
            {
                this.m_objDataVO.m_strOperator_ID = value;
            }
        }
        /// <summary>
        /// ����ҽ����Ա��ID
        /// </summary>
        public string m_StrApplEmpID
        {
            get
            {
                return this.m_objDataVO.m_strAppl_EmpID;
            }
            set
            {
                this.m_objDataVO.m_strAppl_EmpID = value;
            }
        }
        /// <summary>
        /// �������Ŀ��Ҳ���ID
        /// </summary>
        public string m_StrApplDeptID
        {
            get
            {
                return this.m_objDataVO.m_strAppl_DeptID;
            }
            set
            {
                this.m_objDataVO.m_strAppl_DeptID = value;
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string m_StrSummary
        {
            get
            {
                return this.m_objDataVO.m_strSummary;
            }
            set
            {
                this.m_objDataVO.m_strSummary = value;
            }
        }
        /// <summary>
        /// ����״̬��ʶ
        /// </summary>
        public int m_IntPStatus
        {
            get
            {
                return this.m_objDataVO.m_intPStatus_int;
            }
            set
            {
                this.m_objDataVO.m_intPStatus_int = value;
            }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public int m_IntEmergency
        {
            get
            {
                return this.m_objDataVO.m_intEmergency;
            }
            set
            {
                this.m_objDataVO.m_intEmergency = value;
            }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public int m_IntSpecial
        {
            get
            {
                return this.m_objDataVO.m_intSpecial;
            }
            set
            {
                this.m_objDataVO.m_intSpecial = value;
            }
        }
        /// <summary>
        /// 0-��ʽ����,1-������
        /// </summary>
        public int m_IntForm
        {
            get
            {
                return this.m_objDataVO.m_intForm_int;
            }
            set
            {
                this.m_objDataVO.m_intForm_int = value;
            }
        }
        /// <summary>
        /// ����סԺ��
        /// </summary>
        public string m_StrPatientInhospNO
        {
            get
            {
                return this.m_objDataVO.m_strPatient_inhospitalno_chr;
            }
            set
            {
                this.m_objDataVO.m_strPatient_inhospitalno_chr = value;
            }
        }

		#endregion

        #region �¼�����

        /// <summary>
        /// ���뵥ԪId�ı��¼�
        /// </summary>
        private event EventHandler evtApplicationIDChanged;
        
        #endregion

        #region �� ��
       

        /// <summary>
        /// ��ǰ��״̬
        /// </summary>
        private enmObjectOperationState m_EnuOprState
        {
            get
            {
                return this.m_enmOprState;
            }
        }

        /// <summary>
        /// �޸�ǰ��ԭ����
        /// </summary>
        public clsLisApplMainVO m_ObjOriginalDataVO
        {
            get
            {
                return m_objOriginalDataVO;
            }
        }

        /// <summary>
        /// �޸��Ժ������
        /// </summary>
        public clsLisApplMainVO m_ObjDataVO
        {
            get
            {
                return this.m_objDataVO;
            }
        }

        /// <summary>
        /// ��ȡ���鱨�漯��
        /// </summary>
        public clsAppCheckReportCollection m_ObjAppReports
        {
            get { return this.m_objAppReports; }
        }

        /// <summary>
        /// ��ȡ���뵥Ԫ����
        /// </summary>
        public clsAppApplyUnitCollection m_ObjAppApplyUnits
        {
            get { return this.m_objAppApplyUnits; }
        }
        
        #endregion

        #region ��������

        /// <summary>
        /// ��ɾ��App������ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
        /// </summary>
        public void m_mthDelete()
        {
            if (this.m_enmOprState != enmObjectOperationState.New)
            {
                this.m_mthDataTransfer(this.m_objOriginalDataVO, this.m_objDataVO);
                this.m_objDataVO.m_intPStatus_int = 0;
                this.m_enmOprState = enmObjectOperationState.Deleted;
            }
        }

        private void m_mthMatchOpState()
        {
            if (this.m_EnuOprState != enmObjectOperationState.New
                && this.m_EnuOprState != enmObjectOperationState.Deleted)
            {
                if (m_objOriginalDataVO.m_intEmergency == m_objDataVO.m_intEmergency &&
                    m_objOriginalDataVO.m_intForm_int  == m_objDataVO.m_intForm_int &&
                    m_objOriginalDataVO.m_intPStatus_int == m_objDataVO.m_intPStatus_int &&
                    m_objOriginalDataVO.m_intSpecial     == m_objDataVO.m_intSpecial &&
                    m_objOriginalDataVO.m_strAge == m_objDataVO.m_strAge &&
                    m_objOriginalDataVO.m_strAppl_Dat == m_objDataVO.m_strAppl_Dat &&
                    m_objOriginalDataVO.m_strAppl_DeptID == m_objDataVO.m_strAppl_DeptID &&
                    m_objOriginalDataVO.m_strAppl_EmpID == m_objDataVO.m_strAppl_EmpID &&
                    m_objOriginalDataVO.m_strApplication_Form_NO == m_objDataVO.m_strApplication_Form_NO &&
                    m_objOriginalDataVO.m_strAPPLICATION_ID == m_objDataVO.m_strAPPLICATION_ID &&
                    m_objOriginalDataVO.m_strBedNO == m_objDataVO.m_strBedNO &&
                    m_objOriginalDataVO.m_strDiagnose == m_objDataVO.m_strDiagnose &&
                    m_objOriginalDataVO.m_strICD == m_objDataVO.m_strICD &&
                    m_objOriginalDataVO.m_strMODIFY_DAT == m_objDataVO.m_strMODIFY_DAT &&
                    m_objOriginalDataVO.m_strOperator_ID == m_objDataVO.m_strOperator_ID &&
                    m_objOriginalDataVO.m_strPatient_inhospitalno_chr == m_objDataVO.m_strPatient_inhospitalno_chr &&
                    m_objOriginalDataVO.m_strPatient_Name == m_objDataVO.m_strPatient_Name &&
                    m_objOriginalDataVO.m_strPatient_SubNO == m_objDataVO.m_strPatient_SubNO &&
                    m_objOriginalDataVO.m_strPatientcardID == m_objDataVO.m_strPatientcardID &&
                    m_objOriginalDataVO.m_strPatientID == m_objDataVO.m_strPatientID &&
                    m_objOriginalDataVO.m_strPatientType == m_objDataVO.m_strPatientType &&
                    m_objOriginalDataVO.m_strSex == m_objDataVO.m_strSex &&
                    m_objOriginalDataVO.m_strSummary == m_objDataVO.m_strSummary)
                {
                    m_enmOprState = enmObjectOperationState.Original;
                }
                else
                {
                    m_enmOprState = enmObjectOperationState.Modified;
                }
            }
        }

        public void m_mthAcceptChanges()
        {
            if (this.m_objOriginalDataVO == null)
            {
                this.m_objOriginalDataVO = new clsLisApplMainVO();
            }
            this.m_mthDataTransfer(this.m_objDataVO, this.m_objOriginalDataVO);
            this.m_enmOprState = enmObjectOperationState.Original;
        }

        private void m_mthSetPatientInfoFromPatient(clsLIS_Patient p_objPatient)
        {
            this.m_StrAge = p_objPatient.m_StrAge;
            this.m_StrBedNO = p_objPatient.m_StrBedNO;
            this.m_StrPatientCardID = p_objPatient.m_StrCardID;
            this.m_StrDiagnose = p_objPatient.m_StrDiagnose;
            this.m_StrICD = p_objPatient.m_StrICD;
            this.m_StrPatientInhospNO = p_objPatient.m_StrInhospNO;
            this.m_StrPatientID = p_objPatient.m_StrPatientID;
            this.m_StrPatientName = p_objPatient.m_StrPatientName;
            this.m_StrPatientType = p_objPatient.m_StrPatientTypeID;
            this.m_StrSex = p_objPatient.m_StrSex;
            this.m_StrPatientSubNO = p_objPatient.m_StrSubNO;

        }

        private void m_mthDataTransfer(clsLisApplMainVO p_objSource, clsLisApplMainVO p_objTarget)
        {
            p_objTarget.m_intEmergency = p_objSource.m_intEmergency;
            p_objTarget.m_intForm_int = p_objSource.m_intForm_int;
            p_objTarget.m_intPStatus_int = p_objSource.m_intPStatus_int;
            p_objTarget.m_intSpecial = p_objSource.m_intSpecial;
            p_objTarget.m_strAge = p_objSource.m_strAge;
            p_objTarget.m_strAppl_Dat = p_objSource.m_strAppl_Dat;
            p_objTarget.m_strAppl_DeptID = p_objSource.m_strAppl_DeptID;
            p_objTarget.m_strAppl_EmpID = p_objSource.m_strAppl_EmpID;
            p_objTarget.m_strApplication_Form_NO = p_objSource.m_strApplication_Form_NO;
            p_objTarget.m_strAPPLICATION_ID = p_objSource.m_strAPPLICATION_ID;
            p_objTarget.m_strBedNO = p_objSource.m_strBedNO;
            p_objTarget.m_strDiagnose = p_objSource.m_strDiagnose;
            p_objTarget.m_strICD = p_objSource.m_strICD;
            p_objTarget.m_strMODIFY_DAT = p_objSource.m_strMODIFY_DAT;
            p_objTarget.m_strOperator_ID = p_objSource.m_strOperator_ID;
            p_objTarget.m_strPatient_inhospitalno_chr = p_objSource.m_strPatient_inhospitalno_chr;
            p_objTarget.m_strPatient_Name = p_objSource.m_strPatient_Name;
            p_objTarget.m_strPatient_SubNO = p_objSource.m_strPatient_SubNO;
            p_objTarget.m_strPatientcardID = p_objSource.m_strPatientcardID;
            p_objTarget.m_strPatientID = p_objSource.m_strPatientID;
            p_objTarget.m_strPatientType = p_objSource.m_strPatientType;
            p_objTarget.m_strSex = p_objSource.m_strSex;
            p_objTarget.m_strSummary = p_objSource.m_strSummary;
            p_objTarget.m_strSampleType = p_objSource.m_strSampleType;
            p_objTarget.m_strSampleTypeID = p_objSource.m_strSampleTypeID;
            p_objTarget.m_strCheckContent = p_objSource.m_strCheckContent;
        } 

        #endregion

        #region �¼�ʵ��

        private void m_objAppReports_evtItemAdded(object sender, clsObjectArrEventArgs e)
        {
            if (e.m_ObjContentsArr != null)
            {
                for (int i = 0; i < e.m_ObjContentsArr.Length; i++)
                {
                    if (((clsLIS_AppCheckReport)e.m_ObjContentsArr[i]).m_ObjApp != this)
                    {
                        ((clsLIS_AppCheckReport)e.m_ObjContentsArr[i]).m_ObjApp = this;
                    }
                }
            }
        }

        private void m_objAppApplyUnits_evtItemAdded(object sender, clsObjectArrEventArgs e)
        {
            if (e.m_ObjContentsArr != null)
            {
                for (int i = 0; i < e.m_ObjContentsArr.Length; i++)
                {
                    if (((clsLIS_AppApplyUnit)e.m_ObjContentsArr[i]).m_ObjApp != this)
                    {
                        ((clsLIS_AppApplyUnit)e.m_ObjContentsArr[i]).m_ObjApp = this;
                    }
                }
            }
        }

        private void m_objAppReports_evtItemRemoved(object sender, clsObjectArrEventArgs e)
        {
            if (e.m_ObjContentsArr != null)
            {
                for (int i = 0; i < e.m_ObjContentsArr.Length; i++)
                {
                    if (((clsLIS_AppCheckReport)e.m_ObjContentsArr[i]).m_ObjApp == this)
                    {
                        ((clsLIS_AppCheckReport)e.m_ObjContentsArr[i]).m_ObjApp = null;
                    }
                }
            }
        }

        private void m_objAppApplyUnits_evtItemRemoved(object sender, clsObjectArrEventArgs e)
        {
            if (e.m_ObjContentsArr != null)
            {
                for (int i = 0; i < e.m_ObjContentsArr.Length; i++)
                {
                    if (((clsLIS_AppApplyUnit)e.m_ObjContentsArr[i]).m_ObjApp == this)
                    {
                        ((clsLIS_AppApplyUnit)e.m_ObjContentsArr[i]).m_ObjApp = null;
                    }
                }
            }
        } 

        #endregion

	}

	#endregion

	
    #region clsLIS_AppApplyUnit ���� 2004.05.28
	public class clsLIS_AppApplyUnit
	{
		private enmObjectOperationState m_enmOprState;

		private clsT_OPR_LIS_APP_APPLY_UNIT_VO m_objDataVO;
		private clsT_OPR_LIS_APP_APPLY_UNIT_VO m_objOriginalDataVO;
		private clsLIS_ApplyUnit m_objApplyUnit;
		private clsLisAppUnitItemVO[] m_objItemArr;
		

		private clsLIS_App m_objApp;

		#region VOManage
		/// <summary>
		///���뵥ID
		/// </summary>
		public string m_StrAppID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLICATION_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strAPPLICATION_ID_CHR != value)
				{
					this.m_objDataVO.m_strAPPLICATION_ID_CHR = value;
					if(this.evtApplicationIDChanged != null)
					{
						evtApplicationIDChanged(this,new EventArgs());
					}
				}
			}
		}
		/// <summary>
		///�û��Զ�����ۼ�����
		/// </summary>
		public string m_StrUserGroupPath
		{
			get
			{
				return this.m_objDataVO.m_strUSER_GROUP_STRING;
			}
			set
			{
				this.m_objDataVO.m_strUSER_GROUP_STRING = value;
			}
		}
		/// <summary>
		///�������뵥ԪID
		/// </summary>
		public string m_StrApplyUnitID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLY_UNIT_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strAPPLY_UNIT_ID_CHR = value;
			}
		}
		#endregion

		public event EventHandler evtApplicationIDChanged;

		public  enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public clsT_OPR_LIS_APP_APPLY_UNIT_VO m_ObjOriginalDataVO
		{
			get
			{
				return m_objOriginalDataVO;
			}
		}
		public clsT_OPR_LIS_APP_APPLY_UNIT_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
		}
		public clsLIS_ApplyUnit m_ObjApplyUnit{get{return this.m_objApplyUnit;} set{this.m_objApplyUnit = value;}}

		public clsLisAppUnitItemVO[] m_ObjItemArr
		{
			get
			{
				return this.m_objItemArr;
			}
			set
			{
				this.m_objItemArr = value;
			}
		}

		public clsLIS_App m_ObjApp
		{
			get
			{
				return this.m_objApp;
			} 
			set
			{
				if(value != this.m_objApp)
				{
					if(this.m_objApp != null)
					{	
						clsLIS_App objApp = this.m_objApp;
						this.m_objApp = null;
						objApp.m_ObjAppApplyUnits.Remove(this);
					}
					this.m_objApp = value;
					if(value != null)
					{
						this.m_objApp.m_ObjAppApplyUnits.Add(this);
					}
				}
			}
		}



		#region ���캯��
		public clsLIS_AppApplyUnit(clsT_OPR_LIS_APP_APPLY_UNIT_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
		}
		public clsLIS_AppApplyUnit(clsT_OPR_LIS_APP_APPLY_UNIT_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_APPLY_UNIT_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);

				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objDataVO.m_strAPPLICATION_ID_CHR == m_objOriginalDataVO.m_strAPPLICATION_ID_CHR
					&& m_objDataVO.m_strAPPLY_UNIT_ID_CHR == m_objOriginalDataVO.m_strAPPLY_UNIT_ID_CHR
					&& m_objDataVO.m_strUSER_GROUP_STRING == m_objOriginalDataVO.m_strUSER_GROUP_STRING)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}
		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_APPLY_UNIT_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}

		public void m_mthDataTransfer(clsT_OPR_LIS_APP_APPLY_UNIT_VO p_objSource, clsT_OPR_LIS_APP_APPLY_UNIT_VO p_objTarget)
		{
			p_objTarget.m_strAPPLICATION_ID_CHR = p_objSource.m_strAPPLICATION_ID_CHR;
			p_objTarget.m_strAPPLY_UNIT_ID_CHR = p_objSource.m_strAPPLY_UNIT_ID_CHR;
			p_objTarget.m_strUSER_GROUP_STRING = p_objSource.m_strUSER_GROUP_STRING;
		}

	}
	#endregion


	#region clsLIS_AppCheckReport ���� 2004.05.28
	public class clsLIS_AppCheckReport
	{
		private enmObjectOperationState m_enmOprState;

		private clsT_OPR_LIS_APP_REPORT_VO m_objDataVO ;
		private clsT_OPR_LIS_APP_REPORT_VO m_objOriginalDataVO ;

		private clsLIS_CheckReport m_objCheckReport;

		private clsLIS_App m_objApp;
		private clsAppSampleGroupCollection m_objAppSampleGroups;

		#region VOManage
		/// <summary>
		///���뵥ID
		/// </summary>
		public string m_StrAppID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLICATION_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strAPPLICATION_ID_CHR != value)
				{
					this.m_objDataVO.m_strAPPLICATION_ID_CHR = value;
					foreach(clsLIS_AppSampleGroup objAppSampleGroup in this.m_ObjAppSampleGroups)
					{
						objAppSampleGroup.m_StrAppID = value;
					}						
					if(this.evtApplicationIDChanged != null)
					{
						evtApplicationIDChanged(this,new EventArgs());
					}					
				}
			}
		}
		/// <summary>
		///���������XML��ʽ
		/// </summary>
		public string m_StrXMLSummary
		{
			get
			{
				return this.m_objDataVO.m_strXML_SUMMARY_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strXML_SUMMARY_VCHR = value;
			}
		}
		/// <summary>
		///���鱨����ID
		/// </summary>
		public string m_StrReportGroupID
		{
			get
			{
				return this.m_objDataVO.m_strREPORT_GROUP_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strREPORT_GROUP_ID_CHR = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string m_StrModifyDate
		{
			get
			{
				return this.m_objDataVO.m_strMODIFY_DAT;
			}
		}
		/// <summary>
		///�������
		/// </summary>
		public string m_StrSummary
		{
			get
			{
				return this.m_objDataVO.m_strSUMMARY_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strSUMMARY_VCHR = value;
			}
		}
		/// <summary>
		///����ԱID
		/// </summary>
		public string m_StrOperatorID
		{
			get
			{
				return this.m_objDataVO.m_strOPERATOR_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strOPERATOR_ID_CHR = value;
			}
		}
		/// <summary>
		///״̬��-1:��ʷ��¼ 0 -- ��Ч 1:��ʼ״̬ 2:�����
		/// </summary>
		public int m_IntStatus
		{
			get
			{
				return this.m_objDataVO.m_intSTATUS_INT;
			}
			set
			{
				this.m_objDataVO.m_intSTATUS_INT = value;
			}
		}
		/// <summary>
		///��������
		/// </summary>
		public string m_StrReportDate
		{
			get
			{
				return this.m_objDataVO.m_strREPORT_DAT;
			}
			set
			{
				this.m_objDataVO.m_strREPORT_DAT = value;
			}
		}
		/// <summary>
		///������id
		/// </summary>
		public string m_StrReportorId
		{
			get
			{
				return this.m_objDataVO.m_strREPORTOR_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strREPORTOR_ID_CHR = value;
			}
		}
		/// <summary>
		///���ʱ��
		/// </summary>
		public string m_StrConfirmDate
		{
			get
			{
				return this.m_objDataVO.m_strCONFIRM_DAT;
			}
			set
			{
				this.m_objDataVO.m_strCONFIRM_DAT = value;
			}
		}
		/// <summary>
		///�����id
		/// </summary>
		public string m_StrConfirmerID
		{
			get
			{
				return this.m_objDataVO.m_strCONFIRMER_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strCONFIRMER_ID_CHR = value;
			}
		}
		#endregion

		public event EventHandler evtApplicationIDChanged;

		public  enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public clsT_OPR_LIS_APP_REPORT_VO m_ObjOriginalDataVO
		{
			get
			{
				return m_objOriginalDataVO;
			}
		}
		public clsT_OPR_LIS_APP_REPORT_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			} 
		}


		public clsLIS_CheckReport m_ObjCheckReport{get{return this.m_objCheckReport;} set{this.m_objCheckReport = value;}}
		public clsLIS_App m_ObjApp
		{
			get
			{
				return this.m_objApp;
			} 
			set
			{
				if(value != this.m_objApp)
				{
					if(this.m_objApp != null)
					{	
						clsLIS_App objApp = this.m_objApp;
						this.m_objApp = null;
						objApp.m_ObjAppReports.Remove(this);
					}
					this.m_objApp = value;
					if(value != null)
					{
						this.m_objApp.m_ObjAppReports.Add(this);
					}
				}
			}
		}
		public clsAppSampleGroupCollection m_ObjAppSampleGroups{get{return this.m_objAppSampleGroups;} }

		#region ���캯��
		public clsLIS_AppCheckReport(clsT_OPR_LIS_APP_REPORT_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
			m_objAppSampleGroups = new clsAppSampleGroupCollection();
			m_objAppSampleGroups.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppSampleGroups_evtItemAdded);
			m_objAppSampleGroups.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppSampleGroups_evtItemRemoved);
		}
		public clsLIS_AppCheckReport(clsT_OPR_LIS_APP_REPORT_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_REPORT_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);

				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
			m_objAppSampleGroups = new clsAppSampleGroupCollection();
			m_objAppSampleGroups.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppSampleGroups_evtItemAdded);
			m_objAppSampleGroups.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppSampleGroups_evtItemRemoved);
		}
		#endregion


		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objOriginalDataVO.m_intSTATUS_INT == m_objDataVO.m_intSTATUS_INT && 
					m_objOriginalDataVO.m_strAPPLICATION_ID_CHR == m_objDataVO.m_strAPPLICATION_ID_CHR && 
					m_objOriginalDataVO.m_strCONFIRM_DAT == m_objDataVO.m_strCONFIRM_DAT && 
					m_objOriginalDataVO.m_strCONFIRMER_ID_CHR == m_objDataVO.m_strCONFIRMER_ID_CHR && 
					m_objOriginalDataVO.m_strMODIFY_DAT == m_objDataVO.m_strMODIFY_DAT && 
					m_objOriginalDataVO.m_strOPERATOR_ID_CHR == m_objDataVO.m_strOPERATOR_ID_CHR && 
					m_objOriginalDataVO.m_strREPORT_DAT == m_objDataVO.m_strREPORT_DAT && 
					m_objOriginalDataVO.m_strREPORT_GROUP_ID_CHR == m_objDataVO.m_strREPORT_GROUP_ID_CHR && 
					m_objOriginalDataVO.m_strREPORTOR_ID_CHR == m_objDataVO.m_strREPORTOR_ID_CHR && 
					m_objOriginalDataVO.m_strSUMMARY_VCHR == m_objDataVO.m_strSUMMARY_VCHR && 
					m_objOriginalDataVO.m_strXML_SUMMARY_VCHR == m_objDataVO.m_strXML_SUMMARY_VCHR)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}

		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_REPORT_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsT_OPR_LIS_APP_REPORT_VO p_objSource, clsT_OPR_LIS_APP_REPORT_VO p_objTarget)
		{
			p_objTarget.m_intSTATUS_INT = p_objSource.m_intSTATUS_INT;
			p_objTarget.m_strAPPLICATION_ID_CHR = p_objSource.m_strAPPLICATION_ID_CHR;
			p_objTarget.m_strCONFIRM_DAT = p_objSource.m_strCONFIRM_DAT;
			p_objTarget.m_strCONFIRMER_ID_CHR = p_objSource.m_strCONFIRMER_ID_CHR;
			p_objTarget.m_strMODIFY_DAT = p_objSource.m_strMODIFY_DAT;
			p_objTarget.m_strOPERATOR_ID_CHR = p_objSource.m_strOPERATOR_ID_CHR;
			p_objTarget.m_strREPORT_DAT = p_objSource.m_strREPORT_DAT;
			p_objTarget.m_strREPORT_GROUP_ID_CHR = p_objSource.m_strREPORT_GROUP_ID_CHR;
			p_objTarget.m_strREPORTOR_ID_CHR = p_objSource.m_strREPORTOR_ID_CHR;
			p_objTarget.m_strSUMMARY_VCHR = p_objSource.m_strSUMMARY_VCHR;
			p_objTarget.m_strXML_SUMMARY_VCHR = p_objSource.m_strXML_SUMMARY_VCHR;
		}


		private void m_objAppSampleGroups_evtItemAdded(object sender, clsObjectArrEventArgs e)
		{
			if(e.m_ObjContentsArr != null)
			{
				for(int i=0;i<e.m_ObjContentsArr.Length;i++)
				{
					if(((clsLIS_AppSampleGroup)e.m_ObjContentsArr[i]).m_ObjAppReport != this)
					{
						((clsLIS_AppSampleGroup)e.m_ObjContentsArr[i]).m_ObjAppReport = this;
					}
				}
			}
		}

		private void m_objAppSampleGroups_evtItemRemoved(object sender, clsObjectArrEventArgs e)
		{
			if(e.m_ObjContentsArr != null)
			{
				for(int i=0;i<e.m_ObjContentsArr.Length;i++)
				{
					if(((clsLIS_AppSampleGroup)e.m_ObjContentsArr[i]).m_ObjAppReport == this)
					{
						((clsLIS_AppSampleGroup)e.m_ObjContentsArr[i]).m_ObjAppReport = null;
					}
				}
			}
		}
	}
	#endregion
	#region clsLIS_AppSampleGroup ���� 2004.05.28
	public class clsLIS_AppSampleGroup
	{
		private enmObjectOperationState m_enmOprState;

		private clsT_OPR_LIS_APP_SAMPLE_VO m_objDataVO;
		private clsT_OPR_LIS_APP_SAMPLE_VO m_objOriginalDataVO;

		private clsLIS_SampleGroup m_objSampleGroup;
		private int m_intPrintSeq = -1;

		private clsLIS_AppCheckReport m_objAppReport;
		private clsAppCheckItemCollection m_objAppCheckItems;
		private clsLIS_Sample m_objSample;
		private clsLIS_AppApplyUnit[] m_objAppUnitArr;

		#region VOManage
		/// <summary>
		///���뵥ID
		/// </summary>
		public string m_StrAppID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLICATION_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strAPPLICATION_ID_CHR != value)
				{
					this.m_objDataVO.m_strAPPLICATION_ID_CHR = value;
					if(this.m_ObjSample != null)
					{
						this.m_ObjSample.m_StrAppID = value;
					}
					foreach(clsLIS_AppCheckItem objAppCheckItem in this.m_ObjAppCheckItems)
					{
						objAppCheckItem.m_StrAppID = value;
					}
										
					if(this.evtApplicationIDChanged != null)
					{
						evtApplicationIDChanged(this,new EventArgs());
					}					
				}
			}
		}
		/// <summary>
		///����������ID
		/// </summary>
		public string m_StrSampleGroupID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_GROUP_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLE_GROUP_ID_CHR = value;
			}
		}
		/// <summary>
		///���鱨����ID
		/// </summary>
		public string m_StrReportGroupID
		{
			get
			{
				return this.m_objDataVO.m_strREPORT_GROUP_ID_CHR;
			}	
			set
			{
				this.m_objDataVO.m_strREPORT_GROUP_ID_CHR = value;
			}
		}
		/// <summary>
		///����ID
		/// </summary>
		public string m_StrSampleID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLE_ID_CHR = value;
			}
		}
		#endregion

		public event EventHandler evtApplicationIDChanged;

		public  enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public clsT_OPR_LIS_APP_SAMPLE_VO m_ObjOriginalDataVO
		{
			get
			{
				return m_objOriginalDataVO;
			}
		}

        /// <summary>
        /// ���鵥�µı걾��
        /// </summary>
		public clsT_OPR_LIS_APP_SAMPLE_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			} 
		}

		public clsLIS_AppApplyUnit[] m_ObjAppUnitArr
		{
			get
			{
				return this.m_objAppUnitArr;
			}
			set
			{
				this.m_objAppUnitArr = value;
			}
		}

		public int m_IntPrintSeq
		{
			get
			{
				return this.m_intPrintSeq;
			}
			set
			{
				this.m_intPrintSeq = value;
			}
		}	
	

		public clsLIS_SampleGroup m_ObjSampleGroup{get{return this.m_objSampleGroup;} set{this.m_objSampleGroup = value;}}
		public clsLIS_AppCheckReport m_ObjAppReport
		{
			get
			{
				return this.m_objAppReport;
			} 
			set
			{
				if(value != this.m_objAppReport)
				{
					if(this.m_objAppReport != null)
					{	
						clsLIS_AppCheckReport objAppReport = this.m_objAppReport;
						this.m_objAppReport = null;
						objAppReport.m_ObjAppSampleGroups.Remove(this);
					}
					this.m_objAppReport = value;
					if(value != null)
					{
						this.m_objAppReport.m_ObjAppSampleGroups.Add(this);
					}
				}
			}
		}
		public clsAppCheckItemCollection m_ObjAppCheckItems{get{return this.m_objAppCheckItems;}}
		public clsLIS_Sample m_ObjSample
		{
			get
			{
				return this.m_objSample;
			} 
			set
			{
				if(this.m_objSample != null && !m_objSample.Equals(value))
				{
					this.m_objSample.m_ObjAppSampeGroup = null;
				}			
				if(value == null || !value.Equals(this.m_objSample))
				{

					this.m_objSample = value;
					if(this.m_objSample != null)
					{
						this.m_objSample.m_ObjAppSampeGroup = this;						
					}
				}
			}
		}


		#region ���캯��
		public clsLIS_AppSampleGroup(clsT_OPR_LIS_APP_SAMPLE_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
			m_objAppCheckItems = new clsAppCheckItemCollection();
			m_objAppCheckItems.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppCheckItems_evtItemAdded);
			m_objAppCheckItems.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppCheckItems_evtItemRemoved);
		}
		public clsLIS_AppSampleGroup(clsT_OPR_LIS_APP_SAMPLE_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_SAMPLE_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);

				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
			m_objAppCheckItems = new clsAppCheckItemCollection();
			m_objAppCheckItems.evtItemAdded += new dlgCollectionContentsChangedEventHandler(m_objAppCheckItems_evtItemAdded);
			m_objAppCheckItems.evtItemRemoved += new dlgCollectionContentsChangedEventHandler(m_objAppCheckItems_evtItemRemoved);
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objDataVO.m_strAPPLICATION_ID_CHR == m_objOriginalDataVO.m_strAPPLICATION_ID_CHR
					&& m_objDataVO.m_strSAMPLE_ID_CHR == m_objOriginalDataVO.m_strSAMPLE_ID_CHR
					&& m_objDataVO.m_strREPORT_GROUP_ID_CHR == m_objOriginalDataVO.m_strREPORT_GROUP_ID_CHR
					&& m_objDataVO.m_strSAMPLE_GROUP_ID_CHR == m_objOriginalDataVO.m_strSAMPLE_GROUP_ID_CHR)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}

		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_SAMPLE_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsT_OPR_LIS_APP_SAMPLE_VO p_objSource, clsT_OPR_LIS_APP_SAMPLE_VO p_objTarget)
		{
			p_objTarget.m_strAPPLICATION_ID_CHR = p_objSource.m_strAPPLICATION_ID_CHR;
			p_objTarget.m_strREPORT_GROUP_ID_CHR = p_objSource.m_strREPORT_GROUP_ID_CHR;
			p_objTarget.m_strSAMPLE_GROUP_ID_CHR = p_objSource.m_strSAMPLE_GROUP_ID_CHR;
			p_objTarget.m_strSAMPLE_ID_CHR = p_objSource.m_strSAMPLE_ID_CHR;
		}


		public void m_mthGenerateNewSample()
		{
			clsT_OPR_LIS_SAMPLE_VO obj = new clsT_OPR_LIS_SAMPLE_VO();
			this.m_ObjSample = new clsLIS_Sample(obj);
		}

		private void m_objAppCheckItems_evtItemAdded(object sender, clsObjectArrEventArgs e)
		{
			if(e.m_ObjContentsArr != null)
			{
				for(int i=0;i<e.m_ObjContentsArr.Length;i++)
				{
					if(((clsLIS_AppCheckItem)e.m_ObjContentsArr[i]).m_ObjAppSampleGroup != this)
					{
						((clsLIS_AppCheckItem)e.m_ObjContentsArr[i]).m_ObjAppSampleGroup = this;
					}
				}
			}
		}

		private void m_objAppCheckItems_evtItemRemoved(object sender, clsObjectArrEventArgs e)
		{
			if(e.m_ObjContentsArr != null)
			{
				for(int i=0;i<e.m_ObjContentsArr.Length;i++)
				{
					if(((clsLIS_AppCheckItem)e.m_ObjContentsArr[i]).m_ObjAppSampleGroup == this)
					{
						((clsLIS_AppCheckItem)e.m_ObjContentsArr[i]).m_ObjAppSampleGroup = null;
					}
				}
			}
		}
	}
	#endregion
	#region clsLIS_AppCheckItem ���� 2004.05.28
	public class clsLIS_AppCheckItem
	{
		private enmObjectOperationState m_enmOprState;

		private clsT_OPR_LIS_APP_CHECK_ITEM_VO m_objDataVO;
		private clsT_OPR_LIS_APP_CHECK_ITEM_VO m_objOriginalDataVO;

		private clsLIS_CheckItem m_objCheckItem;
		private int m_intPrintSeq = -1;

		private clsLIS_AppSampleGroup m_objAppSampleGroup;
		private clsLIS_CheckItemResult m_objCheckResult;
		private clsLIS_CheckItemDeviceResult m_objDeviceResult;

		#region VOManage
		/// <summary>
		///������Ŀ���
		/// </summary>
		public string m_StrCheckItemID
		{
			get
			{
				return this.m_objDataVO.m_strCHECK_ITEM_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strCHECK_ITEM_ID_CHR = value;
			}
		}
		/// <summary>
		///����������ID
		/// </summary>
		public string m_StrSampleGroupID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_GROUP_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLE_GROUP_ID_CHR = value;
			}
		}
		/// <summary>
		///���鱨����ID
		/// </summary>
		public string m_StrReportGroupID
		{
			get
			{
				return this.m_objDataVO.m_strREPORT_GROUP_ID_CHR;
			}	
			set
			{
				this.m_objDataVO.m_strREPORT_GROUP_ID_CHR = value;
			}
		}
		/// <summary>
		///���뵥ID
		/// </summary>
		public string m_StrAppID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLICATION_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strAPPLICATION_ID_CHR != value)
				{
					this.m_objDataVO.m_strAPPLICATION_ID_CHR = value;

					if(this.evtApplicationIDChanged != null)
					{
						evtApplicationIDChanged(this,new EventArgs());
					}	
				}
			}
		}
		#endregion

		public event EventHandler evtApplicationIDChanged;

		public  enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public clsT_OPR_LIS_APP_CHECK_ITEM_VO m_ObjOriginalDataVO
		{
			get
			{
				return m_objOriginalDataVO;
			}
		}
		public clsT_OPR_LIS_APP_CHECK_ITEM_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			} 
		}


		public clsLIS_CheckItem m_ObjCheckItem
		{
			get
			{
				return this.m_objCheckItem;
			} 
			set
			{
				this.m_objCheckItem = value;
			}
		}

		public int m_IntPrintSeq
		{
			get
			{
				return this.m_intPrintSeq;
			}
			set
			{
				this.m_intPrintSeq = value;
			}
		}


		public clsLIS_AppSampleGroup m_ObjAppSampleGroup
		{
			get
			{
				return this.m_objAppSampleGroup;
			} 
			set
			{
				if(value != this.m_objAppSampleGroup)
				{
					if(this.m_objAppSampleGroup != null)
					{	
						clsLIS_AppSampleGroup objAppSampleGroup = this.m_objAppSampleGroup;
						this.m_objAppSampleGroup = null;
						objAppSampleGroup.m_ObjAppCheckItems.Remove(this);
						
					}
					this.m_objAppSampleGroup = value;
					if(value != null)
					{
						this.m_objAppSampleGroup.m_ObjAppCheckItems.Add(this);
					}
				}
			}
		}

		public clsLIS_CheckItemResult m_ObjCheckResult
		{
			get
			{
				return this.m_objCheckResult;
			}
			set
			{
				this.m_objCheckResult.m_ObjAppCheckItem = null;
				this.m_objCheckResult = value;
				if(this.m_objCheckResult != null)
				{
					this.m_objCheckResult.m_ObjAppCheckItem = this;
				}
			}
		}
		public clsLIS_CheckItemDeviceResult m_ObjDeviceResult
		{
			get
			{
				return this.m_objDeviceResult;
			}
			set
			{
				this.m_objDeviceResult = value;
			}
		}


		public object Tag;


		#region ���캯��
		public clsLIS_AppCheckItem(clsT_OPR_LIS_APP_CHECK_ITEM_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
		}
		public clsLIS_AppCheckItem(clsT_OPR_LIS_APP_CHECK_ITEM_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_CHECK_ITEM_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);
				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objDataVO.m_strAPPLICATION_ID_CHR == m_objOriginalDataVO.m_strAPPLICATION_ID_CHR
					&& m_objDataVO.m_strCHECK_ITEM_ID_CHR == m_objOriginalDataVO.m_strCHECK_ITEM_ID_CHR
					&& m_objDataVO.m_strREPORT_GROUP_ID_CHR == m_objOriginalDataVO.m_strREPORT_GROUP_ID_CHR
					&& m_objDataVO.m_strSAMPLE_GROUP_ID_CHR == m_objOriginalDataVO.m_strSAMPLE_GROUP_ID_CHR)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}

		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_OPR_LIS_APP_CHECK_ITEM_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsT_OPR_LIS_APP_CHECK_ITEM_VO p_objSource, clsT_OPR_LIS_APP_CHECK_ITEM_VO p_objTarget)
		{
			p_objTarget.m_strAPPLICATION_ID_CHR = p_objSource.m_strAPPLICATION_ID_CHR;
			p_objTarget.m_strCHECK_ITEM_ID_CHR = p_objSource.m_strCHECK_ITEM_ID_CHR;
			p_objTarget.m_strREPORT_GROUP_ID_CHR = p_objSource.m_strREPORT_GROUP_ID_CHR;
			p_objTarget.m_strSAMPLE_GROUP_ID_CHR = p_objSource.m_strSAMPLE_GROUP_ID_CHR;

		}


		public void m_mthGenerateCheckResult()
		{
			clsCheckResult_VO objVO = new clsCheckResult_VO();
			clsLIS_CheckItemResult obj = new clsLIS_CheckItemResult(objVO);
			this.m_ObjCheckResult = obj;
		}
	}
	#endregion
	#region clsLIS_Sample ���� 2004.05.28
	public class clsLIS_Sample
	{
		private enmObjectOperationState m_enmOprState;

		private clsT_OPR_LIS_SAMPLE_VO m_objDataVO;
		private clsT_OPR_LIS_SAMPLE_VO m_objOriginalDataVO;

		private clsLIS_AppSampleGroup m_objAppSampeGroup;
		private clsLIS_DeviceRelation m_objDeviceRelation;

		private string m_strSampleID;

		#region VOManage
		/// <summary>
		///�������ţ�ָ�������ĵ�˳����
		/// </summary>
		public string m_StrSampleID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strSAMPLE_ID_CHR != value || this.m_strSampleID != value)
				{
					this.m_objDataVO.m_strSAMPLE_ID_CHR = value;
					this.m_strSampleID = value;
					if(this.m_objDeviceRelation != null)
					{
						this.m_objDeviceRelation.m_StrSampleID = value;
					}
					if(this.m_ObjAppSampeGroup != null)
					{
						this.m_ObjAppSampeGroup.m_StrSampleID = value;
						foreach(clsLIS_AppCheckItem objCheckItem in this.m_ObjAppSampeGroup.m_ObjAppCheckItems)
						{
							if(objCheckItem.m_ObjCheckResult != null)
							{
								objCheckItem.m_ObjCheckResult.m_StrSampleID = value;
							}							
						}
					}
					if(this.evtSampleIDChanged != null)
					{
						evtSampleIDChanged(this,new EventArgs());
					}
				}
			}
		}

		/// <summary>
		///���뵥���
		/// </summary>
		public string m_StrAppID
		{
			get
			{
				return this.m_objDataVO.m_strAPPLICATION_ID_CHR;
			}
			set
			{
				if(this.m_objDataVO.m_strAPPLICATION_ID_CHR != value)
				{
					this.m_objDataVO.m_strAPPLICATION_ID_CHR = value;
					if(this.evtApplicationIDChanged != null)
					{
						evtApplicationIDChanged(this,null);
					}
				}
			}
		}



		/// <summary>
		///��������
		/// </summary>
		public string m_StrAppDate
		{
			get
			{
				return this.m_objDataVO.m_strAPPL_DAT;
			}
			set
			{
				this.m_objDataVO.m_strAPPL_DAT = value;
			}
		}
		/// <summary>
		///�Ա�
		/// </summary>
		public string m_StrSex
		{
			get
			{
				return this.m_objDataVO.m_strSEX_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSEX_CHR = value;
			}
		}
		/// <summary>
		///��������
		/// </summary>
		public string m_StrPatientName
		{
			get
			{
				return this.m_objDataVO.m_strPATIENT_NAME_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strPATIENT_NAME_VCHR = value;
			}
		}
		/// <summary>
		///���˸������
		/// </summary>
		public string m_StrPatientSubNO
		{
			get
			{
				return this.m_objDataVO.m_strPATIENT_SUBNO_CHR;
			}
			set
			{
				this.m_objDataVO.m_strPATIENT_SUBNO_CHR = value;
			}
		}
		/// <summary>
		///����
		/// </summary>
		public string m_StrAge
		{
			get
			{
				return this.m_objDataVO.m_strAGE_CHR;
			}
			set
			{
				this.m_objDataVO.m_strAGE_CHR = value;
			}
		}
		/// <summary>
		///�������
		/// </summary>
		public string m_StrPatientType
		{
			get
			{
				return this.m_objDataVO.m_strPATIENT_TYPE_CHR;
			}
			set
			{
				this.m_objDataVO.m_strPATIENT_TYPE_CHR = value;
			}
		}
		/// <summary>
		///�ٴ����
		/// </summary>
		public string m_StrDiagnose
		{
			get
			{
				return this.m_objDataVO.m_strDIAGNOSE_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strDIAGNOSE_VCHR = value;
			}
		}
		/// <summary>
		///��������
		/// </summary>
		public string m_StrSampleType
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLETYPE_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLETYPE_VCHR = value;
			}
		}
		/// <summary>
		///����״̬
		/// </summary>
		public string m_StrSampleState
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLESTATE_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLESTATE_VCHR = value;
			}
		}
		/// <summary>
		///����
		/// </summary>
		public string m_StrBedNO
		{
			get
			{
				return this.m_objDataVO.m_strBEDNO_CHR;
			}
			set
			{
				this.m_objDataVO.m_strBEDNO_CHR = value;
			}
		}
		/// <summary>
		///�ٴ����ICD��
		/// </summary>
		public string m_StrICD
		{
			get
			{
				return this.m_objDataVO.m_strICD_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strICD_VCHR = value;
			}
		}
		/// <summary>
		///���￨��
		/// </summary>
		public string m_StrPatientCardID
		{
			get
			{
				return this.m_objDataVO.m_strPATIENTCARDID_CHR;
			}
			set
			{
					this.m_objDataVO.m_strPATIENTCARDID_CHR = value;
			}
		}
		/// <summary>
		///ƿǩ�Ż�����
		/// </summary>
		public string m_StrBarcode
		{
			get
			{
				return this.m_objDataVO.m_strBARCODE_VCHR;
			}
			set
			{
				this.m_objDataVO.m_strBARCODE_VCHR = value;
			}
		}
		/// <summary>
		///���˱�ţ�ÿ�����˵�άһ��š�
		/// </summary>
		public string m_StrPatientID
		{
			get
			{
				return this.m_objDataVO.m_strPATIENTID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strPATIENTID_CHR = value;
			}
		}
		/// <summary>
		///����ʱ��
		/// </summary>
		public string m_StrSamplingDate
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLING_DATE_DAT;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLING_DATE_DAT = value;
			}
		}
		/// <summary>
		///�����ݿ��в���������¼���û�Ա���š�
		/// </summary>
		public string m_StrOperatorID
		{
			get
			{
				return this.m_objDataVO.m_strOPERATOR_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strOPERATOR_ID_CHR = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string m_StrModifyDate
		{
			get
			{
				return this.m_objDataVO.m_strMODIFY_DAT;
			}
		}
		/// <summary>
		///����ҽ����Ա��ID
		/// </summary>
		public string m_StrAppDoctID
		{
			get
			{
				return this.m_objDataVO.m_strAPPL_EMPID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strAPPL_EMPID_CHR = value;
			}
		}
		/// <summary>
		///�������Ŀ��Ҳ���ID
		/// </summary>
		public string m_StrAppDeptID
		{
			get
			{
				return this.m_objDataVO.m_strAPPL_DEPTID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strAPPL_DEPTID_CHR = value;
			}
		}
		/// <summary>
		///��¼״̬  -1:��ʷ��¼ 0 -- ��Ч 1:��ʼ״̬ 2:�Ѳɼ� 3:�Ѻ��� 4:��(����) 5:�ѱ��� 6:����� 7:���˻�
		/// </summary>
		public int m_IntStatus
		{
			get
			{
				return this.m_objDataVO.m_intSTATUS_INT;
			}
			set
			{
				this.m_objDataVO.m_intSTATUS_INT = value;
			}
		}
		/// <summary>
		///��������ID,Ӧ��T_AID_LIS_SAMPLETYPE��
		/// </summary>
		public string m_StrSampleTypeID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_TYPE_ID_CHR;
			}	
			set
			{
				this.m_objDataVO.m_strSAMPLE_TYPE_ID_CHR = value;

			}
		}
		/// <summary>
		///�ʿ�ƷID����1��ʾ���ʿ�Ʒ
		/// </summary>
		public string m_strQCSampleID
		{
			get
			{
				return this.m_objDataVO.m_strQCSAMPLEID_CHR;
			}
			set
			{				
				this.m_objDataVO.m_strQCSAMPLEID_CHR = value;					
			}
		}
		/// <summary>
		///��Ʒ��� 1�������걾 2���ֹ��걾 3���ʿر걾
		/// </summary>
		public string m_StrSampleKind
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLEKIND_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLEKIND_CHR = value;
			}
		}
		/// <summary>
		///��Ʒ��������
		/// </summary>
		public string m_StrCheckDate
		{
			get
			{
				return this.m_objDataVO.m_strCHECK_DATE_DAT;
			}
			set
			{
				this.m_objDataVO.m_strCHECK_DATE_DAT = value;
			}
		}
		/// <summary>
		///����ʱ��
		/// </summary>
		public string m_StrAcceptDate
		{
			get
			{
				return this.m_objDataVO.m_strACCEPT_DAT;
			}
			set
			{
				this.m_objDataVO.m_strACCEPT_DAT = value;
			}
		}
		/// <summary>
		///������Ա
		/// </summary>
		public string m_StrAcceptorID
		{
			get
			{
				return this.m_objDataVO.m_strACCEPTOR_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strACCEPTOR_ID_CHR = value;
			}
		}
		/// <summary>
		///����סԺ��
		/// </summary>
		public string m_StrPatientInhospNO
		{
			get
			{
				return this.m_objDataVO.m_strPATIENT_INHOSPITALNO_CHR;
			}
			set
			{
				this.m_objDataVO.m_strPATIENT_INHOSPITALNO_CHR = value;
			}
		}
		/// <summary>
		///���ʱ��
		/// </summary>
		public string m_StrConfirmDAte
		{
			get
			{
				return this.m_objDataVO.m_strCONFIRM_DAT;
			}
			set
			{
				this.m_objDataVO.m_strCONFIRM_DAT = value;
			}
		}
		/// <summary>
		///�����id
		/// </summary>
		public string m_StrConfirmerID
		{
			get
			{
				return this.m_objDataVO.m_strCONFIRMER_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strCONFIRMER_ID_CHR = value;					
			}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
		public string m_StrCollcetorID
		{
			get
			{
				return this.m_objDataVO.m_strCOLLECTOR_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strCOLLECTOR_ID_CHR = value;					
			}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
		public string m_StrCheckerID
		{
			get
			{
				return this.m_objDataVO.m_strCHECKER_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strCHECKER_ID_CHR = value;					
			}
		}
		#endregion

		public event EventHandler evtApplicationIDChanged;
		public event EventHandler evtSampleIDChanged;

		public  enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public clsT_OPR_LIS_SAMPLE_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			} 
		}
		public clsT_OPR_LIS_SAMPLE_VO m_ObjOriginalDataVO
		{
			get
			{
				return this.m_objOriginalDataVO;
			} 
		}


		public clsLIS_AppSampleGroup m_ObjAppSampeGroup{get{return this.m_objAppSampeGroup;} set{this.m_objAppSampeGroup = value;}}

		public clsLIS_DeviceRelation m_ObjDeviceRelation
		{
			get
			{
				return m_objDeviceRelation;
			}
			set
			{
				if(this.m_objDeviceRelation != null && !m_objDeviceRelation.Equals(value))
				{
					this.m_objDeviceRelation.m_ObjSample = null;
				}			
				if(value == null || !value.Equals(this.m_objDeviceRelation))
				{

					this.m_objDeviceRelation = value;
					if(this.m_objDeviceRelation != null)
					{
						this.m_objDeviceRelation.m_ObjSample = this;
					}
				}
			}
		}


		#region ���캯��
		public clsLIS_Sample(clsT_OPR_LIS_SAMPLE_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
		}
		public clsLIS_Sample(clsT_OPR_LIS_SAMPLE_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_OPR_LIS_SAMPLE_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);

				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objOriginalDataVO.m_intSTATUS_INT == m_objDataVO.m_intSTATUS_INT && 
					m_objOriginalDataVO.m_strACCEPT_DAT == m_objDataVO.m_strACCEPT_DAT && 
					m_objOriginalDataVO.m_strACCEPTOR_ID_CHR == m_objDataVO.m_strACCEPTOR_ID_CHR && 
					m_objOriginalDataVO.m_strAGE_CHR == m_objDataVO.m_strAGE_CHR && 
					m_objOriginalDataVO.m_strAPPL_DAT == m_objDataVO.m_strAPPL_DAT && 
					m_objOriginalDataVO.m_strAPPL_DEPTID_CHR == m_objDataVO.m_strAPPL_DEPTID_CHR && 
					m_objOriginalDataVO.m_strAPPL_EMPID_CHR == m_objDataVO.m_strAPPL_EMPID_CHR && 
					m_objOriginalDataVO.m_strAPPLICATION_ID_CHR == m_objDataVO.m_strAPPLICATION_ID_CHR && 
					m_objOriginalDataVO.m_strBARCODE_VCHR == m_objDataVO.m_strBARCODE_VCHR && 
					m_objOriginalDataVO.m_strBEDNO_CHR == m_objDataVO.m_strBEDNO_CHR && 
					m_objOriginalDataVO.m_strCHECK_DATE_DAT == m_objDataVO.m_strCHECK_DATE_DAT && 
					m_objOriginalDataVO.m_strCOLLECTOR_ID_CHR == m_objDataVO.m_strCOLLECTOR_ID_CHR && 
					m_objOriginalDataVO.m_strCONFIRM_DAT == m_objDataVO.m_strCONFIRM_DAT && 
					m_objOriginalDataVO.m_strCONFIRMER_ID_CHR == m_objDataVO.m_strCONFIRMER_ID_CHR && 
					m_objOriginalDataVO.m_strDIAGNOSE_VCHR == m_objDataVO.m_strDIAGNOSE_VCHR && 
					m_objOriginalDataVO.m_strICD_VCHR == m_objDataVO.m_strICD_VCHR && 
					m_objOriginalDataVO.m_strMODIFY_DAT == m_objDataVO.m_strMODIFY_DAT && 
					m_objOriginalDataVO.m_strOPERATOR_ID_CHR == m_objDataVO.m_strOPERATOR_ID_CHR && 
					m_objOriginalDataVO.m_strPATIENT_INHOSPITALNO_CHR == m_objDataVO.m_strPATIENT_INHOSPITALNO_CHR && 
					m_objOriginalDataVO.m_strPATIENT_NAME_VCHR == m_objDataVO.m_strPATIENT_NAME_VCHR && 
					m_objOriginalDataVO.m_strPATIENT_SUBNO_CHR == m_objDataVO.m_strPATIENT_SUBNO_CHR && 
					m_objOriginalDataVO.m_strPATIENT_TYPE_CHR == m_objDataVO.m_strPATIENT_TYPE_CHR && 
					m_objOriginalDataVO.m_strPATIENTCARDID_CHR == m_objDataVO.m_strPATIENTCARDID_CHR && 
					m_objOriginalDataVO.m_strPATIENTID_CHR == m_objDataVO.m_strPATIENTID_CHR && 
					m_objOriginalDataVO.m_strQCSAMPLEID_CHR == m_objDataVO.m_strQCSAMPLEID_CHR && 
					m_objOriginalDataVO.m_strSAMPLE_ID_CHR == m_objDataVO.m_strSAMPLE_ID_CHR && 
					m_objOriginalDataVO.m_strSAMPLE_TYPE_ID_CHR == m_objDataVO.m_strSAMPLE_TYPE_ID_CHR && 
					m_objOriginalDataVO.m_strSAMPLEKIND_CHR == m_objDataVO.m_strSAMPLEKIND_CHR && 
					m_objOriginalDataVO.m_strSAMPLESTATE_VCHR == m_objDataVO.m_strSAMPLESTATE_VCHR && 
					m_objOriginalDataVO.m_strSAMPLETYPE_VCHR == m_objDataVO.m_strSAMPLETYPE_VCHR && 
					m_objOriginalDataVO.m_strSAMPLING_DATE_DAT == m_objDataVO.m_strSAMPLING_DATE_DAT && 
					m_objOriginalDataVO.m_strSEX_CHR == m_objDataVO.m_strSEX_CHR &&
					m_objOriginalDataVO.m_strCHECKER_ID_CHR == m_objDataVO.m_strCHECKER_ID_CHR)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}
		public void m_mthGenerateDeviceRelation()
		{
			clsT_LIS_DeviceRelationVO obj = new clsT_LIS_DeviceRelationVO();
			clsLIS_DeviceRelation objRelation = new clsLIS_DeviceRelation(obj);
			this.m_ObjDeviceRelation = objRelation;
		}

		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_OPR_LIS_SAMPLE_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsT_OPR_LIS_SAMPLE_VO p_objSource, clsT_OPR_LIS_SAMPLE_VO p_objTarget)
		{
			p_objTarget.m_intSTATUS_INT = p_objSource.m_intSTATUS_INT;
			p_objTarget.m_strACCEPT_DAT = p_objSource.m_strACCEPT_DAT;
			p_objTarget.m_strACCEPTOR_ID_CHR = p_objSource.m_strACCEPTOR_ID_CHR;
			p_objTarget.m_strAGE_CHR = p_objSource.m_strAGE_CHR;
			p_objTarget.m_strAPPL_DAT = p_objSource.m_strAPPL_DAT;
			p_objTarget.m_strAPPL_DEPTID_CHR = p_objSource.m_strAPPL_DEPTID_CHR;
			p_objTarget.m_strAPPL_EMPID_CHR = p_objSource.m_strAPPL_EMPID_CHR;
			p_objTarget.m_strAPPLICATION_ID_CHR = p_objSource.m_strAPPLICATION_ID_CHR;
			p_objTarget.m_strBARCODE_VCHR = p_objSource.m_strBARCODE_VCHR;
			p_objTarget.m_strBEDNO_CHR = p_objSource.m_strBEDNO_CHR;
			p_objTarget.m_strCHECK_DATE_DAT = p_objSource.m_strCHECK_DATE_DAT;
			p_objTarget.m_strCOLLECTOR_ID_CHR = p_objSource.m_strCOLLECTOR_ID_CHR;
			p_objTarget.m_strCONFIRM_DAT = p_objSource.m_strCONFIRM_DAT;
			p_objTarget.m_strCONFIRMER_ID_CHR = p_objSource.m_strCONFIRMER_ID_CHR;
			p_objTarget.m_strDIAGNOSE_VCHR = p_objSource.m_strDIAGNOSE_VCHR;
			p_objTarget.m_strICD_VCHR = p_objSource.m_strICD_VCHR;
			p_objTarget.m_strMODIFY_DAT = p_objSource.m_strMODIFY_DAT;
			p_objTarget.m_strOPERATOR_ID_CHR = p_objSource.m_strOPERATOR_ID_CHR;
			p_objTarget.m_strPATIENT_INHOSPITALNO_CHR = p_objSource.m_strPATIENT_INHOSPITALNO_CHR;
			p_objTarget.m_strPATIENT_NAME_VCHR = p_objSource.m_strPATIENT_NAME_VCHR;
			p_objTarget.m_strPATIENT_SUBNO_CHR = p_objSource.m_strPATIENT_SUBNO_CHR;
			p_objTarget.m_strPATIENT_TYPE_CHR = p_objSource.m_strPATIENT_TYPE_CHR;
			p_objTarget.m_strPATIENTCARDID_CHR = p_objSource.m_strPATIENTCARDID_CHR;
			p_objTarget.m_strPATIENTID_CHR = p_objSource.m_strPATIENTID_CHR;
			p_objTarget.m_strQCSAMPLEID_CHR = p_objSource.m_strQCSAMPLEID_CHR;
			p_objTarget.m_strSAMPLE_ID_CHR = p_objSource.m_strSAMPLE_ID_CHR;
			p_objTarget.m_strSAMPLE_TYPE_ID_CHR = p_objSource.m_strSAMPLE_TYPE_ID_CHR;
			p_objTarget.m_strSAMPLEKIND_CHR = p_objSource.m_strSAMPLEKIND_CHR;
			p_objTarget.m_strSAMPLESTATE_VCHR = p_objSource.m_strSAMPLESTATE_VCHR;
			p_objTarget.m_strSAMPLETYPE_VCHR = p_objSource.m_strSAMPLETYPE_VCHR;
			p_objTarget.m_strSAMPLING_DATE_DAT = p_objSource.m_strSAMPLING_DATE_DAT;
			p_objTarget.m_strSEX_CHR = p_objSource.m_strSEX_CHR;
			p_objTarget.m_strCHECKER_ID_CHR = p_objSource.m_strCHECKER_ID_CHR;

		}

	}
	#endregion
	#region clsLIS_CheckItemResult ���� 2004.05.28
	public class clsLIS_CheckItemResult
	{
		private enmObjectOperationState m_enmOprState;

		private weCare.Core.Entity.clsCheckResult_VO m_objDataVO;
		private weCare.Core.Entity.clsCheckResult_VO m_objOriginalDataVO;

		private clsLIS_AppCheckItem m_objAppCheckItem;
		
		#region VOManage
		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public string m_StrModifyDate
		{
			get
			{
				return this.m_objDataVO.m_strModify_Dat;
			}			
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string m_StrReportGroupID
		{
			get
			{
				return this.m_objDataVO.m_strGroupID;
			}
			set
			{
				this.m_objDataVO.m_strGroupID = value;
			}
		}
		/// <summary>
		/// ������Ŀ���
		/// </summary>
		public string m_StrCheckItemID
		{
			get
			{
				return this.m_objDataVO.m_strCheck_Item_ID;
			}
			set
			{
				this.m_objDataVO.m_strCheck_Item_ID = value;

			}
		}
		/// <summary>
		/// �������ţ�ָ�������ĵ�˳����
		/// </summary>
		public string m_StrSampleID 
		{
			get
			{
				return this.m_objDataVO.m_strSample_ID;
			}
			set
			{
				this.m_objDataVO.m_strSample_ID = value;

			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string m_StrResult
		{
			get
			{
				return this.m_objDataVO.m_strResult;
			}
			set
			{
				this.m_objDataVO.m_strResult = value;

			}
		}
		/// <summary>
		/// �����λ
		/// </summary>
		public string m_StrUnit
		{
			get
			{
				return this.m_objDataVO.m_strUnit;
			}
			set
			{
				this.m_objDataVO.m_strUnit = value;

			}
		}
		/// <summary>
		/// �ο�ֵ��Χ
		/// </summary>
		public string m_StrRefrange 
		{
			get
			{
				return this.m_objDataVO.m_strRefrange;
			}
			set
			{
				this.m_objDataVO.m_strRefrange = value;

			}
		}
		/// <summary>
		/// ��Сֵ
		/// </summary>
		public string m_StrMinValue
		{
			get
			{
				return this.m_objDataVO.m_strMin_Val;
			}
			set
			{
				this.m_objDataVO.m_strMin_Val = value;

			}
		}
		/// <summary>
		/// ���ֵ
		/// </summary>
		public string m_StrMaxValue
		{
			get
			{
				return this.m_objDataVO.m_strMax_Val;
			}
			set
			{
				this.m_objDataVO.m_strMax_Val = value;

			}
		}
		/// ��������־
		/// </summary>
		public string m_StrAbnormalFlag 
		{
			get
			{
				return this.m_objDataVO.m_strAbnormal_Flag;
			}
			set
			{
				this.m_objDataVO.m_strAbnormal_Flag = value;

			}
		}
		/// <summary>
		/// ���ͼ�ν��
		/// </summary>	
		public string m_StrPointListString
		{
			get
			{
				return this.m_objDataVO.m_strPointliststr;
			}
			set
			{
				this.m_objDataVO.m_strPointliststr = value;

			}
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string m_StrSummary
		{
			get
			{
				return this.m_objDataVO.m_strSummary;
			}
			set
			{
				this.m_objDataVO.m_strSummary = value;

			}
		}
		/// <summary>
		/// ���ͼ����
		/// </summary>
		public byte[] m_BytGraph
		{
			get
			{
				return this.m_objDataVO.m_byaGraph;
			}
			set
			{
				this.m_objDataVO.m_byaGraph = value;

			}
		}
		/// <summary>
		/// ����״̬��ʶ-1--��ʷ�ۼ� 0--��Ч��¼ 1--��ǰ��Ч��¼����ʼ״̬��
		/// </summary>
		public int m_IntStatus
		{
			get
			{
				return this.m_objDataVO.m_intStatus;
			}
			set
			{
				this.m_objDataVO.m_intStatus = value;

			}
		}
		/// <summary>
		/// ����Ա��ID
		/// </summary>
		public string m_StrOperatorID
		{
			get
			{
				return this.m_objDataVO.m_strOperator_ID;
			}
			set
			{
				this.m_objDataVO.m_strOperator_ID = value;

			}
		}


		#region OLD
		//		/// <summary>
		//		/// ������Ŀ����
		//		/// </summary>
		//		public string m_strCheckItemName
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strCheck_Item_Name;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strCheck_Item_Name != value)
		//				{
		//					this.m_objDataVO.m_strCheck_Item_Name = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// Ӣ������
		//		/// </summary>
		//		public string m_StrCheckItemEnglishName
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strCheck_Item_English_Name;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strCheck_Item_English_Name != value)
		//				{
		//					this.m_objDataVO.m_strCheck_Item_English_Name = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ��������
		//		/// </summary>
		//		public string  m_StrCheckDate
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strCheck_Dat;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strCheck_Dat != value)
		//				{
		//					this.m_objDataVO.m_strCheck_Dat = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// �ٴ�ӡ��
		//		/// </summary>
		//		public string m_StrClinicApp
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strClinicApp;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strClinicApp != value)
		//				{
		//					this.m_objDataVO.m_strClinicApp = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ��ע--���������
		//		/// </summary>
		//		public string m_StrMemo
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strMemo;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strMemo != value)
		//				{
		//					this.m_objDataVO.m_strMemo = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// �������
		//		/// </summary>
		//		public string m_StrConfirmDate
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strConfirm_Dat;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strConfirm_Dat != value)
		//				{
		//					this.m_objDataVO.m_strConfirm_Dat = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//
		//		/// <summary>
		//		/// ����ԱID
		//		/// </summary>
		//		public string m_StrChecker1
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strChecker1;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strChecker1 != value)
		//				{
		//					this.m_objDataVO.m_strChecker1 = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ��������Ա��ID
		//		/// </summary>
		//		public string m_StrChecker2
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strChecker2;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strChecker2 != value)
		//				{
		//					this.m_objDataVO.m_strChecker2 = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ���Ա��ID
		//		/// </summary>
		//		public string m_StrConfirmerID
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strConfirm_Person;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strConfirm_Person != value)
		//				{
		//					this.m_objDataVO.m_strConfirm_Person = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ���鲿��ID
		//		/// </summary>
		//		public string m_StrCheckDeptID
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strCheck_DeptID;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strCheck_DeptID != value)
		//				{
		//					this.m_objDataVO.m_strCheck_DeptID = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// �����豸��
		//		/// </summary>
		//		public string m_StrDeviceID 
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strDeviceID;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strDeviceID != value)
		//				{
		//					this.m_objDataVO.m_strDeviceID = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		//		/// <summary>
		//		/// ������������ļ����������ƣ�����д��
		//		/// </summary>
		//		public string m_StrDeviceCheckItemName
		//		{
		//			get
		//			{
		//				return this.m_objDataVO.m_strDevice_Check_Item_Name;
		//			}
		//			set
		//			{
		//				if(this.m_objDataVO.m_strDevice_Check_Item_Name != value)
		//				{
		//					this.m_objDataVO.m_strDevice_Check_Item_Name = value;
		//					if(this.m_EnuOprState == enmObjectOperationState.Original )
		//					{
		//						this.m_EnuOprState = enmObjectOperationState.Modified;
		//					}
		//				}
		//			}
		//		}
		#endregion

		#endregion

		public enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public weCare.Core.Entity.clsCheckResult_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
		}

		public weCare.Core.Entity.clsCheckResult_VO m_ObjOriginalDataVO
		{
			get
			{
				return this.m_objOriginalDataVO;
			}
		}


		public clsLIS_AppCheckItem m_ObjAppCheckItem
		{
			get
			{
				return m_objAppCheckItem;
			}
			set
			{
				m_objAppCheckItem = value;
			}
		}


		#region ���캯��
		public clsLIS_CheckItemResult(clsCheckResult_VO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
		}
		public clsLIS_CheckItemResult(clsCheckResult_VO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsCheckResult_VO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);
				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objOriginalDataVO.m_byaGraph == m_objDataVO.m_byaGraph && 
					m_objOriginalDataVO.m_intStatus == m_objDataVO.m_intStatus && 
					m_objOriginalDataVO.m_strAbnormal_Flag == m_objDataVO.m_strAbnormal_Flag && 
					m_objOriginalDataVO.m_strCheck_Dat == m_objDataVO.m_strCheck_Dat && 
					m_objOriginalDataVO.m_strCheck_DeptID == m_objDataVO.m_strCheck_DeptID && 
					m_objOriginalDataVO.m_strCheck_Item_English_Name == m_objDataVO.m_strCheck_Item_English_Name && 
					m_objOriginalDataVO.m_strCheck_Item_ID == m_objDataVO.m_strCheck_Item_ID && 
					m_objOriginalDataVO.m_strCheck_Item_Name == m_objDataVO.m_strCheck_Item_Name && 
					m_objOriginalDataVO.m_strChecker1 == m_objDataVO.m_strChecker1 && 
					m_objOriginalDataVO.m_strChecker2 == m_objDataVO.m_strChecker2 && 
					m_objOriginalDataVO.m_strClinicApp == m_objDataVO.m_strClinicApp && 
					m_objOriginalDataVO.m_strConfirm_Dat == m_objDataVO.m_strConfirm_Dat && 
					m_objOriginalDataVO.m_strConfirm_Person == m_objDataVO.m_strConfirm_Person && 
					m_objOriginalDataVO.m_strDevice_Check_Item_Name == m_objDataVO.m_strDevice_Check_Item_Name && 
					m_objOriginalDataVO.m_strDeviceID == m_objDataVO.m_strDeviceID && 
					m_objOriginalDataVO.m_strGroupID == m_objDataVO.m_strGroupID && 
					m_objOriginalDataVO.m_strMax_Val == m_objDataVO.m_strMax_Val && 
					m_objOriginalDataVO.m_strMemo == m_objDataVO.m_strMemo && 
					m_objOriginalDataVO.m_strMin_Val == m_objDataVO.m_strMin_Val && 
					m_objOriginalDataVO.m_strModify_Dat == m_objDataVO.m_strModify_Dat && 
					m_objOriginalDataVO.m_strOperator_ID == m_objDataVO.m_strOperator_ID && 
					m_objOriginalDataVO.m_strPointliststr == m_objDataVO.m_strPointliststr && 
					m_objOriginalDataVO.m_strRefrange == m_objDataVO.m_strRefrange && 
					m_objOriginalDataVO.m_strResult == m_objDataVO.m_strResult && 
					m_objOriginalDataVO.m_strSample_ID == m_objDataVO.m_strSample_ID && 
					m_objOriginalDataVO.m_strSummary == m_objDataVO.m_strSummary && 
					m_objOriginalDataVO.m_strUnit == m_objDataVO.m_strUnit)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}
		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsCheckResult_VO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsCheckResult_VO p_objSource, clsCheckResult_VO p_objTarget)
		{
			p_objTarget.m_byaGraph = p_objSource.m_byaGraph;
			p_objTarget.m_intStatus = p_objSource.m_intStatus;
			p_objTarget.m_strAbnormal_Flag = p_objSource.m_strAbnormal_Flag;
			p_objTarget.m_strCheck_Dat = p_objSource.m_strCheck_Dat;
			p_objTarget.m_strCheck_DeptID = p_objSource.m_strCheck_DeptID;
			p_objTarget.m_strCheck_Item_English_Name = p_objSource.m_strCheck_Item_English_Name;
			p_objTarget.m_strCheck_Item_ID = p_objSource.m_strCheck_Item_ID;
			p_objTarget.m_strCheck_Item_Name = p_objSource.m_strCheck_Item_Name;
			p_objTarget.m_strChecker1 = p_objSource.m_strChecker1;
			p_objTarget.m_strChecker2 = p_objSource.m_strChecker2;
			p_objTarget.m_strClinicApp = p_objSource.m_strClinicApp;
			p_objTarget.m_strConfirm_Dat = p_objSource.m_strConfirm_Dat;
			p_objTarget.m_strConfirm_Person = p_objSource.m_strConfirm_Person;
			p_objTarget.m_strDevice_Check_Item_Name = p_objSource.m_strDevice_Check_Item_Name;
			p_objTarget.m_strDeviceID = p_objSource.m_strDeviceID;
			p_objTarget.m_strGroupID = p_objSource.m_strGroupID;
			p_objTarget.m_strMax_Val = p_objSource.m_strMax_Val;
			p_objTarget.m_strMemo = p_objSource.m_strMemo;
			p_objTarget.m_strMin_Val = p_objSource.m_strMin_Val;
			p_objTarget.m_strModify_Dat = p_objSource.m_strModify_Dat;
			p_objTarget.m_strOperator_ID = p_objSource.m_strOperator_ID;
			p_objTarget.m_strPointliststr = p_objSource.m_strPointliststr;
			p_objTarget.m_strRefrange = p_objSource.m_strRefrange;
			p_objTarget.m_strResult = p_objSource.m_strResult;
			p_objTarget.m_strSample_ID = p_objSource.m_strSample_ID;
			p_objTarget.m_strSummary = p_objSource.m_strSummary;
			p_objTarget.m_strUnit = p_objSource.m_strUnit;
		}

	}
	#endregion
	#region clsLIS_DeviceRelation ���� 2004.05.28
	public class clsLIS_DeviceRelation
	{
		private enmObjectOperationState m_enmOprState;

		private weCare.Core.Entity.clsT_LIS_DeviceRelationVO m_objDataVO;
		private weCare.Core.Entity.clsT_LIS_DeviceRelationVO m_objOriginalDataVO;

		private clsLIS_Sample m_objSample;
	
		#region VOManage
		public int m_IntImportReq
		{
			get
			{
				return this.m_objDataVO.m_intIMPORT_REQ_INT;
			}
			set
			{
				this.m_objDataVO.m_intIMPORT_REQ_INT = value;
			}
		}
		/// <summary>
		///�豸���
		/// </summary>
		public string m_StrDeviceID
		{
			get
			{
				return this.m_objDataVO.m_strDEVICEID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strDEVICEID_CHR = value;
			}
		}
		/// <summary>
		///�������պ������������
		/// </summary>
		public string m_StrSeqID
		{
			get
			{
				return this.m_objDataVO.m_strSEQ_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSEQ_ID_CHR = value;

			}
		}
		/// <summary>
		///��������
		/// </summary>
		public string m_StrReceptDate
		{
			get
			{
				return this.m_objDataVO.m_strRECEPTION_DAT;
			}
			set
			{
				this.m_objDataVO.m_strRECEPTION_DAT = value;
			}
		}
		/// <summary>
		///����������������
		/// </summary>
		public string m_StrDeviceSampleID
		{
			get
			{
				return this.m_objDataVO.m_strDEVICE_SAMPLEID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strDEVICE_SAMPLEID_CHR = value;
			}
		}
		/// <summary>
		///��������
		/// </summary>
		public string m_StrCheckDate
		{
			get
			{
				return this.m_objDataVO.m_strCHECK_DAT;
			}
			set
			{
				this.m_objDataVO.m_strCHECK_DAT = value;
			}
		}
		/// <summary>
		///�������ţ�ָ�������ĵ�˳����
		/// </summary>
		public string m_StrSampleID
		{
			get
			{
				return this.m_objDataVO.m_strSAMPLE_ID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSAMPLE_ID_CHR = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public string m_StrPositionID
		{
			get
			{
				return this.m_objDataVO.m_strPOSITIONID_CHR;
			}
			set
			{
				this.m_objDataVO.m_strPOSITIONID_CHR = value;
			}
		}
		/// <summary>
		///��¼״̬2-�Ѱ� 1-δ�� 0-��Ч
		/// </summary>
		public int m_IntStatus
		{
			get
			{
				return this.m_objDataVO.m_intSTATUS_INT;
			}
			set
			{
				this.m_objDataVO.m_intSTATUS_INT = value;
			}
		}
		/// <summary>
		///����������������ţ��ͺ��������޹أ�
		/// </summary>
		public string m_StrSeqIDDevice
		{
			get
			{
				return this.m_objDataVO.m_strSEQ_ID_DEVICE_CHR;
			}
			set
			{
				this.m_objDataVO.m_strSEQ_ID_DEVICE_CHR = value;
			}
		}
		/// <summary>
		///�����������ķ���.0-�Զ�˳���,1-��ָ���ı�Ű�
		/// </summary>
		public int m_IntBindMethod
		{
			get
			{
				return this.m_objDataVO.m_intBIND_METHOD_INT;
			}
			set
			{
				this.m_objDataVO.m_intBIND_METHOD_INT = value;
			}
		}

		
		#endregion

		public enmObjectOperationState m_EnuOprState
		{
			get
			{
				return this.m_enmOprState;
			}
		}


		public weCare.Core.Entity.clsT_LIS_DeviceRelationVO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}			
		}

		public weCare.Core.Entity.clsT_LIS_DeviceRelationVO m_ObjOriginalDataVO
		{
			get
			{
				return this.m_objOriginalDataVO;
			}			
		}


		public clsLIS_Sample m_ObjSample
		{
			get
			{
				return this.m_objSample;
			}	
			set
			{
				this.m_objSample = value;
			}
		}


		#region ���캯��
		public clsLIS_DeviceRelation(clsT_LIS_DeviceRelationVO p_objDataVO)
		{
			this.m_objDataVO = p_objDataVO;
			m_enmOprState = enmObjectOperationState.New;
		}
		public clsLIS_DeviceRelation(clsT_LIS_DeviceRelationVO p_objDataVO,bool p_blnIsOriginal)
		{
			if(p_blnIsOriginal)
			{
				this.m_objDataVO = p_objDataVO;
				this.m_objOriginalDataVO = new clsT_LIS_DeviceRelationVO();

				m_mthDataTransfer(m_objDataVO,m_objOriginalDataVO);

				m_enmOprState = enmObjectOperationState.Original;
			}
			else
			{
				this.m_objDataVO = p_objDataVO;
				m_enmOprState = enmObjectOperationState.New;
			}
		}
		#endregion

		/// <summary>
		/// ֻ�� Original,Modified ״̬��ʹ�ñ���������Ч
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_enmOprState != enmObjectOperationState.New)
			{
				this.m_enmOprState = enmObjectOperationState.Deleted;
			}
		}

		public void m_mthMatchOpState()
		{
			if(this.m_EnuOprState != enmObjectOperationState.New 
				&& this.m_EnuOprState != enmObjectOperationState.Deleted)
			{
				if(m_objDataVO.m_intBIND_METHOD_INT == m_objOriginalDataVO.m_intBIND_METHOD_INT
					&& m_objDataVO.m_intSTATUS_INT == m_objOriginalDataVO.m_intSTATUS_INT
					&& m_objDataVO.m_strCHECK_DAT == m_objOriginalDataVO.m_strCHECK_DAT
					&& m_objDataVO.m_strDEVICE_SAMPLEID_CHR == m_objOriginalDataVO.m_strDEVICE_SAMPLEID_CHR
					&& m_objDataVO.m_strDEVICEID_CHR == m_objOriginalDataVO.m_strDEVICEID_CHR
					&& m_objDataVO.m_strPOSITIONID_CHR == m_objOriginalDataVO.m_strPOSITIONID_CHR
					&& m_objDataVO.m_strRECEPTION_DAT == m_objOriginalDataVO.m_strRECEPTION_DAT
					&& m_objDataVO.m_strSAMPLE_ID_CHR == m_objOriginalDataVO.m_strSAMPLE_ID_CHR
					&& m_objDataVO.m_strSEQ_ID_CHR == m_objOriginalDataVO.m_strSEQ_ID_CHR
					&& m_objDataVO.m_intIMPORT_REQ_INT == m_objOriginalDataVO.m_intIMPORT_REQ_INT
					&& m_objDataVO.m_strSEQ_ID_DEVICE_CHR == m_objOriginalDataVO.m_strSEQ_ID_DEVICE_CHR)
				{
					m_enmOprState = enmObjectOperationState.Original;
				}
				else
				{
					m_enmOprState = enmObjectOperationState.Modified;
				}
			}
		}
		public void m_mthAcceptChanges()
		{
			if(this.m_objOriginalDataVO == null)
			{
				this.m_objOriginalDataVO = new clsT_LIS_DeviceRelationVO();
			}
			this.m_mthDataTransfer(this.m_objDataVO,this.m_objOriginalDataVO);
			this.m_enmOprState = enmObjectOperationState.Original;
		}


		public void m_mthDataTransfer(clsT_LIS_DeviceRelationVO p_objSource, clsT_LIS_DeviceRelationVO p_objTarget)
		{
			p_objTarget.m_intBIND_METHOD_INT = p_objSource.m_intBIND_METHOD_INT;
			p_objTarget.m_intSTATUS_INT = p_objSource.m_intSTATUS_INT;
			p_objTarget.m_strCHECK_DAT = p_objSource.m_strCHECK_DAT;
			p_objTarget.m_strDEVICE_SAMPLEID_CHR = p_objSource.m_strDEVICE_SAMPLEID_CHR;
			p_objTarget.m_strDEVICEID_CHR = p_objSource.m_strDEVICEID_CHR;
			p_objTarget.m_strPOSITIONID_CHR = p_objSource.m_strPOSITIONID_CHR;
			p_objTarget.m_strRECEPTION_DAT = p_objSource.m_strRECEPTION_DAT;
			p_objTarget.m_strSAMPLE_ID_CHR = p_objSource.m_strSAMPLE_ID_CHR;
			p_objTarget.m_strSEQ_ID_CHR = p_objSource.m_strSEQ_ID_CHR;
			p_objTarget.m_intIMPORT_REQ_INT = p_objSource.m_intIMPORT_REQ_INT;
			p_objTarget.m_strSEQ_ID_DEVICE_CHR = p_objSource.m_strSEQ_ID_DEVICE_CHR;
		}

	}
	#endregion
	#region clsLIS_CheckItemDeviceResult ���� 2004.05.28
	public class clsLIS_CheckItemDeviceResult
	{
		private weCare.Core.Entity.clsDeviceReslutVO m_objDataVO = new clsDeviceReslutVO();
		public weCare.Core.Entity.clsDeviceReslutVO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
		public clsLIS_CheckItemDeviceResult()
		{
		}
	}
	#endregion


	#region clsLIS_Patient ���� 2004.05.28
	public class clsLIS_Patient
	{

		public event EventHandler evtValueChanged;

		private string m_strPatientName;
		private string m_strPatientID;
		private string m_strCardID;
		private string m_strInhospNO;
		private string m_strSubNO;
		private string m_strBedNO;
		private string m_strSex;
		private string m_strAge;
		private string m_strDiagnose;
		private string m_strICD;
		private string m_strPatientTypeID;
		private string m_strPatientTypeName;

		public string m_StrPatientName
		{
			get
			{
				return this.m_strPatientName;
			} 
			set
			{
				if(this.m_strPatientName != value)
				{
					this.m_strPatientName = value;
					if(this.evtValueChanged != null)
					{
						evtValueChanged(this,null);
					}
				}
			}
		}
		public string m_StrPatientID{get{return this.m_strPatientID;} set
																	  {
																		  if(this.m_strPatientID != value)
																		  {
																			  this.m_strPatientID = value;
																			  if(this.evtValueChanged != null)
																			  {
																				  evtValueChanged(this,null);
																			  }
																		  }
																	  }
		}
		public string m_StrCardID{get{return this.m_strCardID;} set
																{
																	if(this.m_strCardID != value)
																	{
																		this.m_strCardID = value;
																		if(this.evtValueChanged != null)
																		{
																			evtValueChanged(this,null);
																		}
																	}
																}
		}
		public string m_StrInhospNO{get{return this.m_strInhospNO;} set
																	{
																		if(this.m_strInhospNO != value)
																		{
																			this.m_strInhospNO = value;
																			if(this.evtValueChanged != null)
																			{
																				evtValueChanged(this,null);
																			}
																		}
																	}
		}
		public string m_StrSubNO{get{return this.m_strSubNO;}  set
															   {
																   if(this.m_strSubNO != value)
																   {
																	   this.m_strSubNO = value;
																	   if(this.evtValueChanged != null)
																	   {
																		   evtValueChanged(this,null);
																	   }
																   }
															   }
		}
		public string m_StrBedNO{get{return this.m_strBedNO;} set
															  {
																  if(this.m_strBedNO != value)
																  {
																	  this.m_strBedNO = value;
																	  if(this.evtValueChanged != null)
																	  {
																		  evtValueChanged(this,null);
																	  }
																  }
															  }
		}
		public string m_StrSex{get{return this.m_strSex;} set
														  {
															  if(this.m_strSex != value)
															  {
																  this.m_strSex = value;
																  if(this.evtValueChanged != null)
																  {
																	  evtValueChanged(this,null);
																  }
															  }
														  }
		}
		public string m_StrAge{get{return this.m_strAge;}set
														 {
															 if(this.m_strAge != value)
															 {
																 this.m_strAge = value;
																 if(this.evtValueChanged != null)
																 {
																	 evtValueChanged(this,null);
																 }
															 }
														 }
		}
		public string m_StrDiagnose{get{return this.m_strDiagnose;} set
																	{
																		if(this.m_strDiagnose != value)
																		{
																			this.m_strDiagnose = value;
																			if(this.evtValueChanged != null)
																			{
																				evtValueChanged(this,null);
																			}
																		}
																	}
		}
		public string m_StrICD{get{return this.m_strICD;} set
														  {
															  if(this.m_strICD != value)
															  {
																  this.m_strICD = value;
																  if(this.evtValueChanged != null)
																  {
																	  evtValueChanged(this,null);
																  }
															  }
														  }
		}
		public string m_StrPatientTypeID{get{return this.m_strPatientTypeID;}  set
																			   {
																				   if(this.m_strPatientTypeID != value)
																				   {
																					   this.m_strPatientTypeID = value;
																					   if(this.evtValueChanged != null)
																					   {
																						   evtValueChanged(this,null);
																					   }
																				   }
																			   }
		}
		public string m_StrPatientTypeName{get{return this.m_strPatientTypeName;}  set
																				   {
																					   if(this.m_strPatientTypeName != value)
																					   {
																						   this.m_strPatientTypeName = value;
																						   if(this.evtValueChanged != null)
																						   {
																							   evtValueChanged(this,null);
																						   }
																					   }
																				   }
		}	


		public clsLIS_Patient()
		{
		}
	}
	#endregion

	#region clsLIS_ApplyUnit ���� 2004.05.28
	public class clsLIS_ApplyUnit
	{
		private clsApplUnit_VO m_objDataVO = new clsApplUnit_VO();

		public clsApplUnit_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
		
		public clsLIS_ApplyUnit()
		{
		}
	}
	#endregion
	#region clsLIS_CheckReport ���� 2004.05.28
	public class clsLIS_CheckReport
	{
		private clsReportGroup_VO m_objDataVO = new clsReportGroup_VO();
		private clsCheckReportDetailCollection m_objDetails;

		public clsLIS_CheckReport()
		{
			this.m_objDetails = new clsCheckReportDetailCollection();
		}
		public clsReportGroup_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
		public clsCheckReportDetailCollection m_ObjDetails
		{
			get
			{
				return this.m_objDetails;
			}
		}
	}
	#endregion
	#region clsLIS_CheckReport_Detail ���� 2004.05.28
	public class clsLIS_CheckReport_Detail
	{
		private clsReportGroupDetail_VO m_objDataVO = new clsReportGroupDetail_VO();
		public clsReportGroupDetail_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
	}
	#endregion
	#region clsLIS_SampleGroup ���� 2004.05.28
	public class clsLIS_SampleGroup
	{
		private clsSampleGroup_VO m_objDataVO = new clsSampleGroup_VO();
		private clsSampleGroupDetailCollection m_objDetails;
		private string[] m_strDeviceModelArr;
		public string[] m_StrDeviceModelArr
		{
			get
			{
				return m_strDeviceModelArr;
			}
			set
			{
				this.m_strDeviceModelArr = value;
			}
		}
		public clsLIS_SampleGroup()
		{
			this.m_objDetails = new clsSampleGroupDetailCollection();
		}
		public clsSampleGroup_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
		public clsSampleGroupDetailCollection m_ObjDetails
		{
			get
			{
				return this.m_objDetails;
			}
		}
	}
	#endregion
	#region clsLIS_SampleGroup_Detail ���� 2004.05.28
	public class clsLIS_SampleGroup_Detail
	{
		private clsSampleGroupDetail_VO m_objDataVO = new clsSampleGroupDetail_VO();
		public clsSampleGroupDetail_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				if(value == null)
				{
					throw new Exception("clsLIS_Sample:m_ObjDataVO(�ؼ�ֵ����)����Ϊ��.");
				}
				this.m_objDataVO = value;
			}
		}
		public clsLIS_SampleGroup_Detail()
		{
		}
	}
	#endregion
	#region clsLIS_CheckItem ���� 2004.05.28
	public class clsLIS_CheckItem
	{
		private clsCheckItem_VO m_objDataVO = new clsCheckItem_VO();
		
		private string m_strDeviceCheckItemName = null;

		public string m_StrDeviceItemName
		{
			get
			{
				return this.m_strDeviceCheckItemName;
			}
			set
			{
				this.m_strDeviceCheckItemName = value;
			}
		}

		public clsCheckItem_VO m_ObjDataVO
		{
			get
			{
				return this.m_objDataVO;
			}
			set
			{
				this.m_objDataVO = value;
			}
		}
		public clsLIS_CheckItem()
		{
		}
		public object Tag;
	}

	#endregion

	#region clsLIS_App �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_App �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28
	/// </summary>
	public class clsAppCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_App)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_App)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_App this[int p_intIndex]
		{
			get
			{
				return (clsLIS_App) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_App p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsAppCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_App obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_App[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_App p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsAppCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_App obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_App RemoveAt(int p_intIndex)
		{
			clsLIS_App objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_App p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_App[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_App p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_App p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region תΪ����
		//		public clsLIS_App[] ToArray()
		//		{
		//			return this.InnerList.ToArray(typeof(clsLIS_App));
		//		}
		#endregion

	}
	#endregion
	#region clsLIS_AppApplyUnit �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_AppApplyUnit �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsAppApplyUnitCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppApplyUnit)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppApplyUnit)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_AppApplyUnit this[int p_intIndex]
		{
			get
			{
				return (clsLIS_AppApplyUnit) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_AppApplyUnit p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsAppApplyUnitCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_AppApplyUnit obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_AppApplyUnit[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_AppApplyUnit p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsAppApplyUnitCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_AppApplyUnit obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_AppApplyUnit RemoveAt(int p_intIndex)
		{
			clsLIS_AppApplyUnit objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_AppApplyUnit p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_AppApplyUnit[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_AppApplyUnit p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_AppApplyUnit p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_AppCheckReport �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_AppCheckReport �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsAppCheckReportCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppCheckReport)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppCheckReport)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_AppCheckReport this[int p_intIndex]
		{
			get
			{
				return (clsLIS_AppCheckReport) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_AppCheckReport p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsAppCheckReportCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_AppCheckReport obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_AppCheckReport[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_AppCheckReport p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsAppCheckReportCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_AppCheckReport obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_AppCheckReport RemoveAt(int p_intIndex)
		{
			clsLIS_AppCheckReport objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_AppCheckReport p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_AppCheckReport[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_AppCheckReport p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_AppCheckReport p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_AppSampleGroup �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_AppSampleGroup �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsAppSampleGroupCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppSampleGroup)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppSampleGroup)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_AppSampleGroup this[int p_intIndex]
		{
			get
			{
				return (clsLIS_AppSampleGroup) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_AppSampleGroup p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsAppSampleGroupCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_AppSampleGroup obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_AppSampleGroup[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_AppSampleGroup p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsAppSampleGroupCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_AppSampleGroup obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_AppSampleGroup RemoveAt(int p_intIndex)
		{
			clsLIS_AppSampleGroup objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_AppSampleGroup p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_AppSampleGroup[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_AppSampleGroup p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_AppSampleGroup p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_AppCheckItem �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_AppCheckItem �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsAppCheckItemCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppCheckItem)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_AppCheckItem)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_AppCheckItem this[int p_intIndex]
		{
			get
			{
				return (clsLIS_AppCheckItem) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_AppCheckItem p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsAppCheckItemCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_AppCheckItem obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_AppCheckItem[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_AppCheckItem p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsAppCheckItemCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_AppCheckItem obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_AppCheckItem RemoveAt(int p_intIndex)
		{
			clsLIS_AppCheckItem objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_AppCheckItem p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_AppCheckItem[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_AppCheckItem p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_AppCheckItem p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_Sample �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_Sample �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsSampleCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_Sample)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_Sample)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_Sample this[int p_intIndex]
		{
			get
			{
				return (clsLIS_Sample) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_Sample p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsSampleCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_Sample obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_Sample[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_Sample p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsSampleCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_Sample obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_Sample RemoveAt(int p_intIndex)
		{
			clsLIS_Sample objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_Sample p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_Sample[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_Sample p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_Sample p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_Patient �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_Patient �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsLISPatientCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_Patient)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_Patient)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_Patient this[int p_intIndex]
		{
			get
			{
				return (clsLIS_Patient) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_Patient p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsLISPatientCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_Patient obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_Patient[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_Patient p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsLISPatientCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_Patient obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_Patient RemoveAt(int p_intIndex)
		{
			clsLIS_Patient objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_Patient p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_Patient[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_Patient p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_Patient p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_ApplyUnit �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_ApplyUnit �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsApplyUnitCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_ApplyUnit)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_ApplyUnit)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_ApplyUnit this[int p_intIndex]
		{
			get
			{
				return (clsLIS_ApplyUnit) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_ApplyUnit p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsApplyUnitCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_ApplyUnit obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_ApplyUnit[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_ApplyUnit p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsApplyUnitCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_ApplyUnit obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_ApplyUnit RemoveAt(int p_intIndex)
		{
			clsLIS_ApplyUnit objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_ApplyUnit p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="���׳��쳣"> </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_ApplyUnit[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_ApplyUnit p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_ApplyUnit p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_CheckReport �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_CheckReport �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsCheckReportCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckReport)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckReport)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_CheckReport this[int p_intIndex]
		{
			get
			{
				return (clsLIS_CheckReport) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_CheckReport p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsCheckReportCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_CheckReport obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_CheckReport[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_CheckReport p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsCheckReportCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_CheckReport obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_CheckReport RemoveAt(int p_intIndex)
		{
			clsLIS_CheckReport objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_CheckReport p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_CheckReport[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_CheckReport p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_CheckReport p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_CheckReport_Detail �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_CheckReport_Detail �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsCheckReportDetailCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckReport_Detail)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckReport_Detail)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_CheckReport_Detail this[int p_intIndex]
		{
			get
			{
				return (clsLIS_CheckReport_Detail) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_CheckReport_Detail p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsCheckReportDetailCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_CheckReport_Detail obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_CheckReport_Detail[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_CheckReport_Detail p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsCheckReportDetailCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_CheckReport_Detail obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_CheckReport_Detail RemoveAt(int p_intIndex)
		{
			clsLIS_CheckReport_Detail objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_CheckReport_Detail p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_CheckReport_Detail[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_CheckReport_Detail p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_CheckReport_Detail p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_SampleGroup �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_SampleGroup �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsSampleGroupCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_SampleGroup)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_SampleGroup)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_SampleGroup this[int p_intIndex]
		{
			get
			{
				return (clsLIS_SampleGroup) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_SampleGroup p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsSampleGroupCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_SampleGroup obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_SampleGroup[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_SampleGroup p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsSampleGroupCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_SampleGroup obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_SampleGroup RemoveAt(int p_intIndex)
		{
			clsLIS_SampleGroup objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_SampleGroup p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_SampleGroup[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_SampleGroup p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_SampleGroup p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_SampleGroup_Detail �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_SampleGroup_Detail �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsSampleGroupDetailCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_SampleGroup_Detail)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_SampleGroup_Detail)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_SampleGroup_Detail this[int p_intIndex]
		{
			get
			{
				return (clsLIS_SampleGroup_Detail) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_SampleGroup_Detail p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsSampleGroupDetailCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_SampleGroup_Detail obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_SampleGroup_Detail[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_SampleGroup_Detail p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsSampleGroupDetailCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_SampleGroup_Detail obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_SampleGroup_Detail RemoveAt(int p_intIndex)
		{
			clsLIS_SampleGroup_Detail objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_SampleGroup_Detail p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_SampleGroup_Detail[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_SampleGroup_Detail p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_SampleGroup_Detail p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
	#region clsLIS_CheckItem �ļ����� ���� 2004.05.28
	/// <summary>
	/// clsLIS_CheckItem �ļ�����,һ�������ڴ˼�����ֻ���ܴ���һ������. ���� 2004.05.28,
	/// </summary>
	public class clsCheckItemCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// �ڶ�����뼯�Ϻ���;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemAdded = null;
		/// <summary>
		/// �ڶ�����뼯��ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemAdded = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�����;
		/// </summary>
		public event dlgCollectionContentsChangedEventHandler evtItemRemoved = null;
		/// <summary>
		/// �ڶ���Ӽ������Ƴ�ǰ����;
		/// </summary>
		public event dlgCollectionContentsBeforeChangeEventHandler evtBeforeItemRemoved = null;

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckItem)value});

			if(evtItemRemoved != null)
			{
				evtItemRemoved(this,eAfter);
			}
		}
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{(clsLIS_CheckItem)value});

			if(evtItemAdded != null)
			{
				evtItemAdded(this,eAfter);
			}
		}


		#region this[] ������(get)
		/// <summary>
		/// Item ������(get)
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		public clsLIS_CheckItem this[int p_intIndex]
		{
			get
			{
				return (clsLIS_CheckItem) List[p_intIndex];
			}
		}

		#endregion

		#region Add ����һ������
		/// <summary>
		/// ����һ������
		/// �����������Ѵ����ڼ��������ټ���,����������λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// -1:���뱻��ֹ;
		/// ����:����λ������
		/// </returns>
		public int Add(clsLIS_CheckItem p_obj)
		{
			int intRet = -1;
			if(this.IsInclude(p_obj))
			{
				intRet = this.IndexOf(p_obj);
			}
			else
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					intRet = List.Add(p_obj);
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			return intRet;				
		}
		#endregion

		#region AddRange ������һ�������еĶ���
		/// <summary>
		/// ������һ�������еĶ���,���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void AddRange(clsCheckItemCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			foreach(clsLIS_CheckItem obj in p_clt)
			{
				if(!this.IsInclude(obj))
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{obj});
					if(this.evtBeforeItemAdded != null)
					{
						evtBeforeItemAdded(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						List.Add(obj);
						//						clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{obj});
						//
						//						if(this.evtItemAdded != null)
						//						{
						//							evtItemAdded(this,eAfter);
						//						}
					}
				}				
			}
			//			this.InnerList.AddRange(p_clt);
		}
		#endregion

		#region AddArr ����һ�����
		/// <summary>
		/// ����һ�����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_objArr">һ�����,Ϊ���򲻽����κβ���</param>
		public void AddArr(clsLIS_CheckItem[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					if(!this.IsInclude(p_objArr[i]))
					{
						clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
						if(this.evtBeforeItemAdded != null)
						{
							evtBeforeItemAdded(this,eBefore);
						}
						if(!eBefore.Handled)
						{
							List.Add(p_objArr[i]);
							//							clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_objArr[i]});
							//
							//							if(this.evtItemAdded != null)
							//							{
							//								evtItemAdded(this,eAfter);
							//							}
						}
					}
				}
			}
		}
		#endregion

		#region Insert ��ָ��λ�ò������
		/// <summary>
		/// ��ָ��λ�ò������
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndex ������Ч����</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <param name="p_obj">Ҫ����Ķ���</param>
		/// <returns>
		/// ��������Ѵ��ڼ������򷵻���ʵ�ʵ�λ������;
		/// ���򷵻ز���p_intIndex��ֵ;
		/// -1:���뱻��ֹ
		/// </returns>
		public int Insert(int p_intIndex,clsLIS_CheckItem p_obj)
		{
			int intIndex = -1;
			if(!this.IsInclude(p_obj))
			{
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
				if(this.evtBeforeItemAdded != null)
				{
					evtBeforeItemAdded(this,eBefore);
				}
				if(eBefore.Handled)
				{
					intIndex = -1;
				}
				else
				{
					this.List.Insert(p_intIndex,p_obj);
					intIndex = p_intIndex;
					//					clsObjectArrEventArgs eAfter = new clsObjectArrEventArgs(new object[]{p_obj});
					//
					//					if(this.evtItemAdded != null)
					//					{
					//						evtItemAdded(this,eAfter);
					//					}
				}
			}
			else
			{
				intIndex = this.IndexOf(p_obj);
			}
			return intIndex;
		}
		#endregion

		#region InsertRange ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����
		/// <summary>
		/// ����һ�����ϵĶ����ָ��λ�ÿ�ʼ����;
		/// ���ĳ�������Ѵ����ڼ��������ټ���;
		/// ÿ����һ������֮ǰ������ evtBeforeItemAdded �¼�,�ʿ���Ӧ�¼���ֹ�κ�һ������ļ���;
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">p_intIndexStart ������Ч����</exception>
		/// <param name="p_intIndexStart">��ʼ�����λ������</param>
		/// <param name="p_clt">��һ��ͬ�༯��,Ϊ���򲻽����κβ���</param>
		public void InsertRange(int p_intIndexStart,clsCheckItemCollection p_clt)
		{
			if(p_clt == null)
			{
				return;
			}
			int intRet;
			foreach(clsLIS_CheckItem obj in p_clt)
			{
				intRet = this.Insert(p_intIndexStart,obj);
				if(intRet == p_intIndexStart)
				{
					p_intIndexStart++;
				}
			}
			//			this.InnerList.InsertRange(p_intIndex,p_clt);
		}

		#endregion

		#region RemoveAt �Ƴ�ָ��λ�õĶ���
		/// <summary>
		/// �Ƴ�ָ��λ�õĶ���
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">�������쳣</exception>
		/// <param name="p_intIndex">ָ��λ��</param>
		/// <returns>
		///	null: p_intIndex Խ�����ֹ�Ƴ�;
		///	����Ϊ���Ƴ��Ķ���
		/// </returns>
		public new clsLIS_CheckItem RemoveAt(int p_intIndex)
		{
			clsLIS_CheckItem objRet =null;
			if (p_intIndex > Count - 1 || p_intIndex < 0)
			{
				objRet = null;
			}
			else
			{ 
				objRet = this[p_intIndex];
				clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{objRet});
				if(this.evtBeforeItemRemoved != null)
				{
					evtBeforeItemRemoved(this,eBefore);
				}
				if(!eBefore.Handled)
				{
					List.RemoveAt(p_intIndex);
				}
			}
			return objRet;
		}
		#endregion
		#region Remove �Ӽ������Ƴ�ָ���Ķ���
		/// <summary>
		/// �Ӽ������Ƴ�ָ���Ķ���,�ɱ��¼���������ֹ�Ƴ�
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ���Ķ���</param>
		public void Remove(clsLIS_CheckItem p_obj)
		{
			clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_obj});
			if(this.evtBeforeItemRemoved != null)
			{
				evtBeforeItemRemoved(this,eBefore);
			}
			if(!eBefore.Handled)
			{
				List.Remove(p_obj);
			}
		}
		#endregion

		#region RemoveArr �Ӽ������Ƴ�ָ����һ�����
		/// <summary>
		/// �Ӽ������Ƴ�ָ����һ�����,�ɱ��¼���������ֹ�Ƴ�ĳ������
		/// </summary>
		/// <exception cref="">���׳��쳣 </exception>
		/// <param name="p_objArr">ָ����һ�����</param>
		public void RemoveArr(clsLIS_CheckItem[] p_objArr)
		{
			if(p_objArr != null)
			{
				for(int i=0;i<p_objArr.Length;i++)
				{
					clsObjectArrHandledEventArgs eBefore = new clsObjectArrHandledEventArgs(new object[]{p_objArr[i]});
					if(this.evtBeforeItemRemoved != null)
					{
						evtBeforeItemRemoved(this,eBefore);
					}
					if(!eBefore.Handled)
					{
						this.Remove(p_objArr[i]);
					}
				}
			}
		}
		#endregion

		#region IndexOf ȡ�ö����ڼ����е�λ������
		/// <summary>
		/// ȡ�ö����ڼ����е�λ������
		/// </summary>
		/// <exception cref="���׳��쳣"></exception>
		/// <param name="p_obj">ָ������</param>
		/// <returns>
		/// �����ڼ����е�λ������;
		/// -1:������û���������
		/// </returns>
		public int IndexOf(clsLIS_CheckItem p_obj)
		{
			return this.List.IndexOf(p_obj);
		}
		#endregion
		#region IsInclude �������Ƿ����ָ���Ķ���
		/// <summary>
		/// �������Ƿ����ָ���Ķ��� 
		/// </summary>
		/// <param name="p_obj">ָ���Ķ��� </param>
		/// <returns>true:���� ;false :������</returns>
		public bool IsInclude(clsLIS_CheckItem p_obj)
		{
			if(this.IndexOf(p_obj)>=0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion


    public enum enmObjectOperationState
    {
        Original, New, Deleted, Modified
    }
    public class clsObjectArrHandledEventArgs : System.EventArgs
    {
        private bool blnHandled = false;
        public bool Handled
        {
            get { return this.blnHandled; }
            set { blnHandled = value; }
        }

        private object[] m_objContentsArr;
        public object[] m_ObjContentsArr
        {
            get
            {
                return m_objContentsArr;
            }
        }
        public clsObjectArrHandledEventArgs(object[] p_objContentsArr)
        {
            this.m_objContentsArr = p_objContentsArr;
        }
    }
    public class clsObjectArrEventArgs : System.EventArgs
    {
        private object[] m_objContentsArr;
        public object[] m_ObjContentsArr
        {
            get
            {
                return m_objContentsArr;
            }
        }
        public clsObjectArrEventArgs(object[] p_objContentsArr)
        {
            this.m_objContentsArr = p_objContentsArr;
        }
    }
    public delegate void dlgCollectionContentsBeforeChangeEventHandler(object sender, clsObjectArrHandledEventArgs e);
    public delegate void dlgCollectionContentsChangedEventHandler(object sender, clsObjectArrEventArgs e);


}
