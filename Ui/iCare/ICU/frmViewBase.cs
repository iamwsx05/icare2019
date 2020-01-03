using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Controls.Domain.EmrControls;
using System.ComponentModel;
using com.digitalwave.emr.BEDExplorer;
using System.Drawing;
using System.IO;
using System.Data;

namespace iCare
{
    public class frmViewBase : Form
    {
        // Fields
        private IContainer components;
        protected ctlAreaPatientSelected m_ctlAreaPatientSelection;
        protected ctlEmrPatientInfo m_ctlPatientInfo;
        protected clsDepartment m_objCurrentDepartment;
        protected clsPatient m_objCurrentPatient;
        protected Panel m_pnlNewBase;

        // Methods
        public frmViewBase()
        {
            this.components = null;
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void frmViewBase_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.m_ctlAreaPatientSelection.m_mthInit();
            }
        }

        private void InitializeComponent()
        {
            this.m_pnlNewBase = new Panel();
            this.m_ctlPatientInfo = new ctlEmrPatientInfo();
            this.m_ctlAreaPatientSelection = new ctlAreaPatientSelected();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            this.m_pnlNewBase.AccessibleName = "NoDefaultIn";
            this.m_pnlNewBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlNewBase.BorderStyle = BorderStyle.FixedSingle;
            this.m_pnlNewBase.Controls.Add(this.m_ctlPatientInfo);
            this.m_pnlNewBase.Controls.Add(this.m_ctlAreaPatientSelection);
            this.m_pnlNewBase.Location = new Point(1, 3);
            this.m_pnlNewBase.Name = "m_pnlNewBase";
            this.m_pnlNewBase.Size = new Size(0x31a, 0x42);
            this.m_pnlNewBase.TabIndex = 0x989687;
            this.m_ctlPatientInfo.Dock = DockStyle.Fill;
            this.m_ctlPatientInfo.Font = new Font("宋体", 10.5f);
            this.m_ctlPatientInfo.Location = new Point(0, 0x22);
            this.m_ctlPatientInfo.m_BlnIsShowAddres = false;
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = false;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = false;
            this.m_ctlPatientInfo.m_BlnIsShowOccupy = false;
            this.m_ctlPatientInfo.m_BlnIsShowOffice = false;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowRace = false;
            this.m_ctlPatientInfo.m_BlnIsShowRelationName = false;
            this.m_ctlPatientInfo.m_BlnIsShowRelationPhone = false;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Name = "m_ctlPatientInfo";
            this.m_ctlPatientInfo.Size = new Size(0x318, 30);
            this.m_ctlPatientInfo.TabIndex = 0x989686;
            this.m_ctlAreaPatientSelection.AutoSize = false;
            this.m_ctlAreaPatientSelection.Font = new Font("宋体", 10.5f);
            this.m_ctlAreaPatientSelection.GripStyle = 0;
            this.m_ctlAreaPatientSelection.Location = new Point(0, 0);
            this.m_ctlAreaPatientSelection.m_BlnIsInUse = true;
            this.m_ctlAreaPatientSelection.m_BlnIsShowArea = true;
            this.m_ctlAreaPatientSelection.m_BlnIsShowBorder = false;
            this.m_ctlAreaPatientSelection.m_BlnIsShowDefaultArea = true;
            this.m_ctlAreaPatientSelection.m_ClrEnd = SystemColors.Control;
            this.m_ctlAreaPatientSelection.m_ClrStart = SystemColors.Control;
            this.m_ctlAreaPatientSelection.Name = "m_ctlAreaPatientSelection";
            this.m_ctlAreaPatientSelection.Size = new Size(0x318, 0x22);
            this.m_ctlAreaPatientSelection.TabIndex = 2;
            this.m_ctlAreaPatientSelection.evtAreaChanged += new AreaSelectedEventHandler(this.m_ctlAreaPatientSelection_evtAreaChanged);
            this.m_ctlAreaPatientSelection.evtBedChanged += new BedSelectedEventHandler(this.m_ctlAreaPatientSelection_evtBedChanged);
            this.m_ctlAreaPatientSelection.evtSessionSelected += new SelectedSessionEventHandler(this.m_ctlAreaPatientSelection_evtSessionSelected);
            this.m_ctlAreaPatientSelection.evtRefreshResult += new com.digitalwave.Controls.Domain.EmrControls.RefreshEventHandler(this.m_ctlAreaPatientSelection_evtRefreshResult);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x31c, 0x1ef);
            this.Controls.Add(this.m_pnlNewBase);
            this.Font = new Font("宋体", 10.5f);
            this.Name = "frmViewBase";
            this.Text = "frmViewBase";
            this.Load += new EventHandler(this.frmViewBase_Load);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void m_ctlAreaPatientSelection_evtAreaChanged(object sender, clsAreaDataEventArg e)
        {
            this.m_mthAreaChanged(e.SelectedArea);
        }

        private void m_ctlAreaPatientSelection_evtBedChanged(object sender, clsBedDataEventArg e)
        {
            try
            {
                this.m_mthSetPatient(e.CurrentBed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                e.Cancel = true;
            }
        }

        private void m_ctlAreaPatientSelection_evtRefreshResult(object sender, clsBedDataEventArg e)
        {
            this.m_mthSetPatient(e.CurrentBed);
            this.m_mthPerformSessionChanged(this.m_ObjCurrentEmrPatientSession, 0);
        }

        private void m_ctlAreaPatientSelection_evtSessionSelected(object sender, clsSessionEventArg e)
        {
            this.m_mthPerformSessionChanged(e.SelectedSession, e.Index);
        }

        protected virtual void m_mthAreaChanged(clsEmrDept_VO p_objSelectedArea)
        {
            this.m_mthClearPatientBaseInfoButArea();
            this.m_mthClearAllInfo(this);
        }

        protected virtual void m_mthClearAllInfo(Control p_ctlControl)
        {
            return;
        }

        private void m_mthClearPatientBaseInfoButArea()
        {
            this.m_objCurrentPatient = null;
        }

        public virtual void m_mthClose()
        {
            return;
        }

        protected virtual void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            return;
        }

        protected virtual void m_mthSetFocusAfterSetPatientInfo()
        {
            return;
        }

        private void m_mthSetPatient(clsEmrBed_VO p_objEmrBed)
        {
            //clsHospitalManagerDomain domain;
            clsEmrDept_VO t_vo = null;
            clsEmrDept_VO t_vo2 = null;
            clsEmrInBedPatient_VO t_vo3 = null;
            clsPatient patient = new clsPatient(p_objEmrBed);
            patient.m_ObjPeopleInfo = null;
            string str = patient.m_StrName;
            string str2 = patient.m_StrPatientID;
            string str3 = patient.m_strDeptNewID;
            string str4 = patient.m_strAreaNewID;
            string str5 = patient.m_strBedCode;

            #region 赋值
            DataRow dr = null;
            DataTable dt = null;
            (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetSpecialDeptInfo(patient.m_strDeptNewID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                t_vo = new clsEmrDept_VO();
                t_vo.m_strDEPTID_CHR = dr["deptid_chr"].ToString().Trim();
                t_vo.m_strDEPTNAME_VCHR = dr["deptname_vchr"].ToString();
                t_vo.m_strADDRESS_VCHR = dr["address_vchr"].ToString();
                t_vo.m_strSHORTNO_CHR = dr["shortno_chr"].ToString();
                if (t_vo.m_strSHORTNO_CHR != null)
                    t_vo.m_strSHORTNO_CHR = t_vo.m_strSHORTNO_CHR.Trim();
                t_vo.m_strATTRIBUTEID = dr["attributeid"].ToString();
                t_vo.m_intDEFAULT_INPATIENT_DEPT_INT = Convert.ToInt32(dr["default_dept_int"]);
            }
            (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetSpecialDeptInfo(patient.m_strAreaNewID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                t_vo2 = new clsEmrDept_VO();
                t_vo2.m_strDEPTID_CHR = dr["deptid_chr"].ToString().Trim();
                t_vo2.m_strDEPTNAME_VCHR = dr["deptname_vchr"].ToString();
                t_vo2.m_strADDRESS_VCHR = dr["address_vchr"].ToString();
                t_vo2.m_strSHORTNO_CHR = dr["shortno_chr"].ToString();
                if (t_vo2.m_strSHORTNO_CHR != null)
                    t_vo2.m_strSHORTNO_CHR = t_vo2.m_strSHORTNO_CHR.Trim();
                t_vo2.m_strATTRIBUTEID = dr["attributeid"].ToString();
                t_vo2.m_intDEFAULT_INPATIENT_DEPT_INT = Convert.ToInt32(dr["default_dept_int"]);
            }
            (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetSpecialMinPatinetInfoByDeptID(patient.m_StrPatientID, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime dtmTemp = DateTime.Now;
                t_vo3 = new clsEmrInBedPatient_VO();
                t_vo3.m_arlSessionInfo = new List<clsEmrPatientSessionInfo_VO>();
                clsEmrPatientSessionInfo_VO obj = null;
                int max = 0;
                foreach (DataRow dr1 in dt.Rows)
                {
                    obj = new clsEmrPatientSessionInfo_VO();
                    obj.m_strEMRInpatientId = dr1["emrinpatientid"].ToString();
                    dtmTemp = new DateTime(1900, 1, 1);
                    if (DateTime.TryParse(dr1["emrinpatientdate"].ToString(), out dtmTemp))
                        obj.m_dtmEMRInpatientDate = dtmTemp;
                    else
                        obj.m_dtmEMRInpatientDate = new DateTime(1900, 1, 1);
                    obj.m_strHISInpatientId = dr1["hisinpatientid_chr"].ToString();
                    dtmTemp = new DateTime(1900, 1, 1);
                    if (DateTime.TryParse(dr1["hisinpatientdate"].ToString(), out dtmTemp))
                        obj.m_dtmHISInpatientDate = dtmTemp;
                    else
                        obj.m_dtmHISInpatientDate = new DateTime(1900, 1, 1);
                    dtmTemp = new DateTime(1900, 1, 1);
                    if (DateTime.TryParse(dr1["outdate"].ToString(), out dtmTemp))
                        obj.m_dtmOutDate = dtmTemp;
                    else
                        obj.m_dtmOutDate = new DateTime(1900, 1, 1);
                    obj.m_strInTimes = dr1["inpatientcount_int"].ToString();
                    obj.m_strRegisterId = dr1["registerid_chr"].ToString().Trim();
                    obj.m_strDeptId = dr1["deptid_chr"].ToString();
                    obj.m_strDeptName = dr1["deptname_vchr"].ToString();
                    obj.m_strAreaId = dr1["INBEDAREAID_CHR"].ToString();
                    obj.m_strAreaName = dr1["inbedareaname"].ToString();
                    obj.m_strPatientID = dr1["PATIENTID_CHR"].ToString();
                    //选择指定的住院信息
                    if (Convert.ToInt32(dr["inpatientcount_int"]) > max)
                    {
                        max = Convert.ToInt32(dr["inpatientcount_int"]);
                        dr = dr1;
                    }
                    t_vo3.m_arlSessionInfo.Add(obj);
                }
                //     性别
                t_vo3.m_strSEX_CHR = dr["SEX_CHR"].ToString();
                //     病人诊疗卡号
                t_vo3.m_strPatientCardID_VCHR = dr["patientcardid_chr"].ToString();
                //     病人费别ID
                t_vo3.m_strPayTypeID = dr["paytypeid_chr"].ToString();
                //     多次住院信息
                //t_vo3.m_arlSessionInfo;
                //     当前选择的住院信息的索引（默认为最后一次）
                t_vo3.m_intSelectedSessionIndex = Convert.ToInt32(dr["inpatientcount_int"]);
                //     入院诊断
                t_vo3.m_strDiagnos = dr["diagnose_vchr"].ToString();
                //     主治医生姓名
                t_vo3.m_strCaseDoctorName = dr["doctname"].ToString();
                //     主治医生ID
                t_vo3.m_strCaseDoctorId = dr["doctid"].ToString();
                //     获取或设置病人特征(0,普通病人;1,此病人所有病历只读，且不能选择其它病人)
                t_vo3.m_intCharacter = 0;
                //     入院日期(会有变动)
                t_vo3.m_dtmHISInDate = Convert.ToDateTime(dr["hisinpatientdate"]);
                //     入院日期(不会变动)
                t_vo3.m_dtmEMRInDate = Convert.ToDateTime(dr["emrinpatientdate"]);
                //     住院号(会有变动)
                t_vo3.m_strHISInPatientID = dr["inpatientid_chr"].ToString();
                //     住院号(不会变动)
                t_vo3.m_strEMRInPatientID = dr["inpatientid_chr"].ToString();
                //     病人扩展ID
                t_vo3.m_strEXTENDID_VCHR = dr["patientextendid"].ToString();
                //     护理等级{-1=无护理;0=特级护理;1=一级护理;2=二级护理;3=三级护理}
                t_vo3.m_intNurseClass = (dr["nursing_class"] == DBNull.Value ? -1 : Convert.ToInt32(dr["nursing_class"]));
                //     病情状态{1=危;2=急;3=普通}
                t_vo3.m_intSTATE_INT = Convert.ToInt32(dr["state_int"]);
                //     入院次数
                t_vo3.m_intINPATIENTCOUNT_INT = Convert.ToInt32(dr["inpatientcount_int"]);
                //     入院日期
                t_vo3.m_strINPATIENT_DAT = Convert.ToDateTime(dr["inpatient_dat"]).ToString("yyyy-MM-dd HH:mm:ss");
                //     床号
                t_vo3.m_strCODE_CHR = dr["bed_no"].ToString();
                //     病床id
                t_vo3.m_strBEDID_CHR = dr["bedid_chr"].ToString();
                //     入院病区
                t_vo3.m_strAREAID_CHR = dr["areaid_chr"].ToString();
                //     入院科室
                t_vo3.m_strDEPTID_CHR = dr["deptid_chr"].ToString();
                //     年龄 岁数(无单位)
                t_vo3.m_intAge = (new clsBrithdayToAge()).m_intGetAge(Convert.ToDateTime(dr["birth_dat"]));
                //     年龄 精确到月(带单位)
                t_vo3.m_strAGELONG_CHR = (new clsBrithdayToAge()).m_strGetAge(Convert.ToDateTime(dr["birth_dat"]));
                //     年龄 岁数(带单位)
                t_vo3.m_strAGESHORT_CHR = (new clsBrithdayToAge()).m_strGetAge(Convert.ToDateTime(dr["birth_dat"]));
                //     出生年月
                t_vo3.m_strBIRTH_DAT = Convert.ToDateTime(dr["birth_dat"]).ToString("yyyy-MM-dd");
                //     住院状态 {0=未上床;1=已上床;2=预出院;3=实际出院;4=请假}
                t_vo3.m_intPSTATUS_INT = Convert.ToInt32(dr["pstatus_int"]);
            }
            #endregion

            //domain = new clsHospitalManagerDomain();
            //domain.m_lngGetSpecialDeptInfo(patient.m_strDeptNewID, &t_vo);
            frmHRPExplorer.objpCurrentDepartment = t_vo;
            //domain.m_lngGetSpecialAreaInfo(patient.m_strAreaNewID, &t_vo2);
            frmHRPExplorer.objpCurrentArea = t_vo2;
            //domain.m_lngGetSpecialMinPatinetInfoByDeptID(patient.m_StrPatientID, &t_vo3);
            com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentPatient = t_vo3;
            frmHRPExplorer.objpCurrentPatient = t_vo3;
            this.m_mthClearPatientBaseInfoButArea();
            this.m_mthClearAllInfo(this);
            this.m_mthSetPatientInfo(patient);
            this.m_ctlPatientInfo.m_mthClearText();
            this.m_ctlPatientInfo.m_mthSetPatientBaseInfo(p_objEmrBed);
        }

        protected virtual void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            return;
        }

        protected void m_mthSetPatientInfo(clsPatient p_objSelectedPatient)
        {
            string str = p_objSelectedPatient.m_strBedCode;
            if (p_objSelectedPatient != null)
            {
                this.m_mthShowNoPatient();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                string str2 = p_objSelectedPatient.m_strBedCode;
                this.m_objCurrentPatient = p_objSelectedPatient;
                this.m_mthSetPatientFormInfo(p_objSelectedPatient);
                this.m_mthSetFocusAfterSetPatientInfo();
                if (this.m_objCurrentPatient != null)
                {
                    this.m_objCurrentPatient = p_objSelectedPatient;
                }
            }
        }

        protected virtual void m_mthShowNoPatient()
        {
            MessageBox.Show("对不起，没有此病人！");
        }

        // Properties
        protected clsEmrDept_VO m_ObjCurrentArea
        {
            get
            {
                return this.m_ctlAreaPatientSelection.CurrentArea;
            }
        }

        protected clsEmrBed_VO m_ObjCurrentBed
        {
            get
            {
                return this.m_ctlAreaPatientSelection.CurrentBed;
            }
        }

        public clsDepartment m_ObjCurrentDepartment
        {
            get
            {
                return this.m_objCurrentDepartment;
            }
        }

        public clsEmrPatient_VO m_ObjCurrentEmrPatient
        {
            get
            {
                return this.m_ctlPatientInfo.CurrentEmrPatient;
            }
        }

        public clsEmrPatientSessionInfo_VO m_ObjCurrentEmrPatientSession
        {
            get
            {
                return this.m_ctlAreaPatientSelection.CurrentSessionInfo;
            }
        }

        public clsPatient m_ObjCurrentPatient
        {
            get
            {
                return this.m_objCurrentPatient;
            }
        }

        protected clsEmrPatientSessionInfo_VO m_ObjLastEmrPatientSession
        {
            get
            {
                return this.m_ctlAreaPatientSelection.m_objGetLastPatientSession();
            }
        }
    }
}
