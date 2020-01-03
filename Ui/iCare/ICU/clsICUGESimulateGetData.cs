using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace iCare
{
    public class clsICUGESimulateGetData
    {
        // Fields
        #region Fields
        private bool m_blnConnent;
        private bool m_blnIsExist;
        private float m_fltARTDiastolic;
        private float m_fltARTMean;
        private float m_fltARTSystolic;
        private float m_fltHR;
        private float m_fltNBPDiastolic;
        private float m_fltNBPMean;
        private float m_fltNBPSystolic;
        private float m_fltPluse;
        private float m_fltRR;
        private float m_fltSpO2;
        private float m_fltTEMP1;
        private float m_fltTEMP2;
        private AxGETICUDATAFORMLib.AxGetICUDataForm m_GetICUDataForm;
        private clsReceiverParam m_objParam;
        private string m_strBedID;
        private string m_strGEIP;
        private string m_strGENo;
        private string m_strInPatientDate;
        private string m_strInPatientID;
        private string m_strXML;
        private Timer m_timSaveGEParams;
        private XmlDocument m_xmlDoc;
        private XmlElement m_xmlElm;
        #endregion

        // Methods
        public clsICUGESimulateGetData(Form p_frm)
        {
            this.m_strInPatientID = "";
            this.m_strInPatientDate = "";
            this.m_strBedID = "";
            this.m_blnIsExist = false;
            this.m_strGEIP = "";
            this.m_strGENo = "";
            this.m_blnConnent = false;
            this.m_strXML = "";
            this.m_objParam = new clsReceiverParam();
            this.m_GetICUDataForm = new AxGETICUDATAFORMLib.AxGetICUDataForm();
            this.m_GetICUDataForm.Parent = p_frm;
            this.m_GetICUDataForm.Left = -50;
            this.m_GetICUDataForm.Top = -50;
            this.m_timSaveGEParams = new Timer();
            this.m_timSaveGEParams.Interval = 0x3e8;
            this.m_timSaveGEParams.Tick += new EventHandler(this.m_timSaveGEParams_Tick);
            this.m_timSaveGEParams.Stop();
        }

        protected /*override*/ void Finalize()
        {
            try
            {
                this.m_mthStopReceiveData();
            }
            finally
            {
                //this.Finalize();
            }
        }

        private long m_lngUpdateMonitorParamArr(clsICUParameterInfo[] p_objICUParameterInfo, string p_strMonitorID)
        {  
            DateTime time = DateTime.Now;
            int count = p_objICUParameterInfo.Length;
            clsICUGEPARAMParamValue[] valueArray = new clsICUGEPARAMParamValue[count];
            for (int i = 0; i < count; i++)
            {
                valueArray[i] = new clsICUGEPARAMParamValue();
                valueArray[i].m_strInPatientID = this.m_strInPatientID;
                valueArray[i].m_strInPatientDate = this.m_strInPatientDate;
                valueArray[i].m_strStatus = "0";
                valueArray[i].m_strParamValue = p_objICUParameterInfo[i].fltVaule.ToString();
                valueArray[i].m_strParamID = p_objICUParameterInfo[i].strParameterID;
                valueArray[i].m_strParamDate = time.ToString();
                valueArray[i].m_strMonitorID = p_strMonitorID;
            }
            //serv = (clsICUGEMaintenanceServ)clsObjectGenerator.objCreatorObjectByType(typeof(clsICUGEMaintenanceServ));
            //long rec = serv.m_lngAddNewParamRecordArr(valueArray);
            //return rec;
            return 0;
        }

        public void m_mthChangeApparatus(string p_strInPatientID, string p_strInPatientDate, string p_strBedID)
        {
            DataTable table = null;
            this.m_mthStopReceiveData();
            if (p_strBedID == null || p_strBedID.Length == 0)
                return;
            this.m_strBedID = p_strBedID;
            this.m_strInPatientDate = p_strInPatientDate;
            this.m_strInPatientID = p_strInPatientID;
            this.m_mthGetGEApparatusIP();
            if (this.m_blnIsExist == null)
                return;

            this.m_mthGetGEInf(this.m_strGENo, ref table);
            if (table != null && table.Rows.Count > 0)
            {
                this.m_strGEIP = table.Rows[0]["GE_IP"].ToString();
            }
            this.m_objParam = new clsReceiverParam();
            this.m_GetICUDataForm.blnSetWaveRefMode(Convert.ToInt16("0"));
            if (this.m_GetICUDataForm.blnStartGetMulticastData("225.6.7.8", int.Parse("2100")) == false)
            {
                return;
            }
            this.m_blnConnent = true;
            this.m_GetICUDataForm.mthSetCurMonitor(0x100, this.m_strGEIP, 0);
            this.m_timSaveGEParams.Start();
        }

        private void m_mthGetBedGEinf(string p_strBedID, ref string p_strGENo, ref bool p_blnIsExist)
        {
            //clsICU_QuerySvc svc;
            //p_blnIsExist = false;
            //svc = (clsICU_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsICU_QuerySvc));
            //svc.m_lngGetBedGEInfo(p_strBedID, p_blnIsExist, p_strGENo);
            //svc.Dispose();
        }

        private void m_mthGetGEApparatusIP()
        {
            this.m_strGENo = "";
            this.m_mthGetBedGEinf(this.m_strBedID, ref this.m_strGENo, ref this.m_blnIsExist);
        }

        private void m_mthGetGEInf(string p_strGENo, ref DataTable p_dtRecord)
        {
            //clsICU_QuerySvc svc;
            //svc = (clsICU_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsICU_QuerySvc));
            //svc.m_lngGetGEInf(p_strGENo, p_dtRecord);
            //svc.Dispose();
        }

        private void m_mthGetMonAllParams()
        {
            this.m_mthXMLParser(this.m_strXML);
        }

        private void m_mthSaveGEParam()
        {
            clsICUParameterInfo[] infoArray;
            this.m_objParam = this.m_objGetMonAllParams();

            if (this.m_objParam == null)
                return;

            try
            {
                clsReceiverParamID mid = new clsReceiverParamID();

                clsICUParameterInfo vo = null;
                List<clsICUParameterInfo> lst = new List<clsICUParameterInfo>();
                for (int i = 0; i < 13; i++)
                {
                    vo = new clsICUParameterInfo();
                    switch (i)
                    {
                        case 0:
                            vo.fltVaule = this.m_objParam.m_fltSpO2;
                            vo.strParameterID = mid.m_strSpO2;
                            break;
                        case 1:
                            // 
                            break;
                        case 2:
                            vo.fltVaule = this.m_objParam.m_fltHR;
                            vo.strParameterID = mid.m_strHR;
                            break;
                        case 3:
                            vo.fltVaule = this.m_objParam.m_fltPluse;
                            vo.strParameterID = mid.m_strPluse;
                            break;
                        case 4:
                            vo.fltVaule = this.m_objParam.m_fltNBPSystolic;
                            vo.strParameterID = mid.m_strNBPSystolic;
                            break;
                        case 5:
                            vo.fltVaule = this.m_objParam.m_fltNBPDiastolic;
                            vo.strParameterID = mid.m_strNBPDiastolic;
                            break;
                        case 6:
                            vo.fltVaule = this.m_objParam.m_fltNBPMean;
                            vo.strParameterID = mid.m_strNBPMean;
                            break;
                        case 7:
                            vo.fltVaule = this.m_objParam.m_fltRR;
                            vo.strParameterID = mid.m_strRR;
                            break;
                        case 8:
                            vo.fltVaule = this.m_objParam.m_fltARTDiastolic;
                            vo.strParameterID = mid.m_strARTDiastolic;
                            break;
                        case 9:
                            vo.fltVaule = this.m_objParam.m_fltARTSystolic;
                            vo.strParameterID = mid.m_strARTSystolic;
                            break;
                        case 10:
                            vo.fltVaule = this.m_objParam.m_fltARTMean;
                            vo.strParameterID = mid.m_strARTMean;
                            break;
                        case 11:
                            vo.fltVaule = this.m_objParam.m_fltTEMP1;
                            vo.strParameterID = mid.m_strTEMP1;
                            break;
                        case 12:
                            vo.fltVaule = this.m_objParam.m_fltTEMP2;
                            vo.strParameterID = mid.m_strTEMP2;
                            break;
                    }
                    if (vo.strParameterID != null && vo.strParameterID != "" && vo.fltVaule != 0)
                    {
                        lst.Add(vo);
                    }
                }
                infoArray = lst.ToArray();
                try
                {
                    this.m_lngUpdateMonitorParamArr(infoArray, this.m_strGEIP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void m_mthStopReceiveData()
        {
            this.m_timSaveGEParams.Stop();
            this.m_objParam = null;
            this.m_strInPatientID = "";
            this.m_strInPatientDate = "";
            this.m_strBedID = "";
            this.m_strGEIP = "";
            this.m_strXML = "";
            this.m_strGENo = "";
            this.m_strXML = "";
            this.m_blnConnent = false;
            this.m_blnIsExist = false;
        }

        private void m_mthXMLParser(string p_strXML)
        {
            this.m_xmlDoc = new XmlDocument();
            this.m_xmlDoc.LoadXml(this.m_strXML);
            this.m_xmlElm = this.m_xmlDoc.DocumentElement;
            this.m_fltHR = Convert.ToSingle(this.m_xmlElm.Attributes["HR"].Value);
            this.m_fltPluse = Convert.ToSingle(this.m_xmlElm.Attributes["PLUSE"].Value);
            this.m_fltSpO2 = Convert.ToSingle(this.m_xmlElm.Attributes["SPO2"].Value);
            this.m_fltNBPSystolic = Convert.ToSingle(this.m_xmlElm.Attributes["NBPSYSTOLIC"].Value);
            this.m_fltNBPDiastolic = Convert.ToSingle(this.m_xmlElm.Attributes["NBPDIASTOLIC"].Value);
            this.m_fltNBPMean = Convert.ToSingle(this.m_xmlElm.Attributes["NBPMEAN"].Value);
            this.m_fltRR = Convert.ToSingle(this.m_xmlElm.Attributes["RR"].Value);
            this.m_fltARTSystolic = Convert.ToSingle(this.m_xmlElm.Attributes["ARTSYSTOLIC"].Value);
            this.m_fltARTDiastolic = Convert.ToSingle(this.m_xmlElm.Attributes["ARTDIASTOLIC"].Value);
            this.m_fltARTMean = Convert.ToSingle(this.m_xmlElm.Attributes["ARTMEAN"].Value);
            this.m_fltTEMP1 = Convert.ToSingle(this.m_xmlElm.Attributes["TEMP1"].Value);
            this.m_fltTEMP2 = Convert.ToSingle(this.m_xmlElm.Attributes["TEMP2"].Value);
        }

        private clsReceiverParam m_objGetMonAllParams()
        {
            this.m_mthGetMonAllParams();
            clsReceiverParam param = new clsReceiverParam();
            param.m_fltHR = this.m_fltHR;
            param.m_fltNBPSystolic = this.m_fltNBPSystolic;
            param.m_fltNBPDiastolic = this.m_fltNBPDiastolic;
            param.m_fltNBPMean = this.m_fltNBPMean;
            param.m_fltPluse = this.m_fltPluse;
            param.m_fltSpO2 = this.m_fltSpO2;
            param.m_fltRR = this.m_fltRR;
            param.m_fltARTSystolic = this.m_fltARTSystolic;
            param.m_fltARTDiastolic = this.m_fltARTDiastolic;
            param.m_fltARTMean = this.m_fltARTMean;
            param.m_fltTEMP1 = this.m_fltTEMP1;
            param.m_fltTEMP2 = this.m_fltTEMP2;

            if ((this.m_fltHR == 0 || this.m_fltNBPSystolic == 0 || this.m_fltNBPDiastolic == 0 || this.m_fltNBPMean == 0 || this.m_fltPluse == 0 || this.m_fltSpO2 == 0 ||
                 this.m_fltRR == 0 || this.m_fltARTSystolic == 0 || this.m_fltARTDiastolic == 0 || this.m_fltARTMean == 0 || this.m_fltTEMP1 == 0) && this.m_fltTEMP2 == 0)
            {
                return null;
            }
            else
            {
                return param;
            }
        }

        private void m_timSaveGEParams_Tick(object sender, EventArgs e)
        {
            if (this.m_blnConnent == false)
            {
                return;
            }
            this.m_mthSaveGEParam();
        }

        // Properties
        public clsGECMSData M_objNumericParam
        {
            get
            {
                clsGECMSData data;
                clsGECMSData data2;
                bool flag;
                if (this.m_objParam == null)
                {
                    return null;
                }
                data = new clsGECMSData();
                data.m_Breath = this.m_objParam.m_fltRR.ToString();
                data.m_strARTDiastolic = "";
                data.m_strARTMean = "";
                data.m_strARTSystolic = "";
                data.m_strHR = this.m_objParam.m_fltHR.ToString();
                data.m_strNBPDiastolic = this.m_objParam.m_fltNBPDiastolic.ToString();
                data.m_strNBPMean = this.m_objParam.m_fltNBPMean.ToString();
                data.m_strNBPSystolic = this.m_objParam.m_fltNBPSystolic.ToString();
                data.m_strPluse = this.m_objParam.m_fltPluse.ToString();
                data.m_strRR = this.m_objParam.m_fltRR.ToString();
                data.m_strSpO2 = this.m_objParam.m_fltSpO2.ToString();
                data.m_strSpO2_2 = "";
                data.m_strTEMP1 = this.m_objParam.m_fltTEMP1.ToString();
                data.m_strTEMP2 = this.m_objParam.m_fltTEMP2.ToString();
                data2 = data;
                return data2;
            }
        }
    }
}
