using System;
using System.Collections.Generic;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.LIS
{
    internal class clsQCBatch
    {
        // Fields
        private DateTime m_datBegin;
        private DateTime m_datEnd;
        private clsLisQCBatchVO m_objBatchSet;
        private List<clsLisQCConcentrationVO> m_objConcentrations;
        private List<clsLisQCDataVO> m_objDatas;
        //private clsDcl_QCDataBusiness m_objDomain;
        private List<clsLisQCReportVO> m_objReports;
        private string m_strBrokenRules;

        // Events
        public event EventHandler BrokenRulesChanged;
        public event EventHandler ConcentrationChanged;
        public event EventHandler DataChanged;
        public event EventHandler Loaded;
        public event DataLoadFailedEventHandler LoadFailed;
        public event EventHandler Reloaded;
        public event EventHandler Reseted;
        public event EventHandler SetChanged;

        // Methods
        public clsQCBatch()
        {
            this.m_objConcentrations = new List<clsLisQCConcentrationVO>();
            this.m_objDatas = new List<clsLisQCDataVO>();
            this.m_objReports = new List<clsLisQCReportVO>();
            this.m_strBrokenRules = string.Empty;
            this.m_datBegin = DateTime.MinValue;
            this.m_datEnd = DateTime.MaxValue;
            //this.m_objDomain = new clsDcl_QCDataBusiness(); 
        }

        public List<clsLisQCConcentrationVO> GetConcentrations()
        {
            List<clsLisQCConcentrationVO> concentratrionList = new List<clsLisQCConcentrationVO>();
            foreach (clsLisQCConcentrationVO var in m_objConcentrations)
            {
                clsLisQCConcentrationVO temp = new clsLisQCConcentrationVO();
                var.m_mthCopyTo(temp);
                concentratrionList.Add(temp);
            }
            return concentratrionList;
        }

        public List<clsLisQCDataVO> GetDatas()
        {
            List<clsLisQCDataVO> dataList = new List<clsLisQCDataVO>();
            foreach (clsLisQCDataVO data in m_objDatas)
            {
                clsLisQCDataVO temp = new clsLisQCDataVO();
                data.m_mthCopyTo(temp);
                dataList.Add(temp);
            }
            return dataList;
        }

        public clsLisQCBatchVO GetQCBatchSet()
        {
            clsLisQCBatchVO temp = new clsLisQCBatchVO();
            if (m_objBatchSet != null)
            {
                m_objBatchSet.m_mthCopyTo(temp);
            }
            return temp;
        }

        public List<clsLisQCReportVO> GetReports()
        {
            return this.m_objReports;
        }

        public void Load(int p_intQCBatchSeq, DateTime p_dtpDateStart, DateTime p_DateEnd)
        {
            clsLisQCBatchVO hvo = null;
            clsLisQCConcentrationVO[] nvoArray = null;
            clsLisQCDataVO[] avoArray = null;
            clsLisQCReportVO[] tvoArray = null;
            long num = 0;
            bool flag;
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCBatch(p_intQCBatchSeq, true, out hvo);
            if (num > 0 && hvo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCConcentration(p_intQCBatchSeq, out nvoArray);
            }
            if (num > 0 && hvo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out avoArray, p_intQCBatchSeq, p_dtpDateStart, p_DateEnd);
            }
            if (num > 0 && hvo != null)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(p_intQCBatchSeq, p_dtpDateStart, p_DateEnd, enmQCStatus.Natrural, out tvoArray);
            }
            if (num > 0 && hvo != null)
            {
                this.m_objConcentrations.Clear();
                this.m_objDatas.Clear();
                this.m_objReports.Clear();
                this.m_strBrokenRules = string.Empty;
                this.m_objBatchSet = hvo;

                if (nvoArray != null)
                {
                    this.m_objConcentrations.AddRange(nvoArray);
                }
                if (avoArray != null)
                {
                    this.m_objDatas.AddRange(avoArray);
                }
                if (tvoArray != null)
                {
                    this.m_objReports.AddRange(tvoArray);
                }

                this.m_datBegin = p_dtpDateStart;
                this.m_datEnd = p_DateEnd;
                if (this.Loaded != null)
                {
                    this.Loaded(this, null);
                }
            }
            else
            {
                if (num <= 0)
                {
                    if (this.LoadFailed != null)
                    {
                        this.LoadFailed(this, new DataLoadFailedEventArgs("读取数据失败."));
                    }
                }
                else if (hvo == null)
                {
                    if (this.LoadFailed == null)
                    {
                        this.LoadFailed(this, new DataLoadFailedEventArgs("指定的质控批不存在."));
                    }
                }
            }
        }

        public long m_lngInsertQCDataByArr(clsLisQCDataVO[] p_objQCDataArr, out int[] p_intSeqArr)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCDataByArr(p_objQCDataArr, out p_intSeqArr);
        }

        public long m_lngQueryDeviceInfo(string p_strComputerName, out DataTable p_dtResult)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryDeviceInfo(p_strComputerName, out p_dtResult);
        }

        public void m_lngQueryQCDeviceResult(int p_strQCId, out List<double> lstDbl)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryQCDeviceResult(p_strQCId, out lstDbl);
        }

        public void m_lngQueryQCInfo(int p_intQCID, out List<clsLisQCConcentrationVO> p_lstQCConTemp)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryQCInfo(p_intQCID, out p_lstQCConTemp);
        }

        public void m_mthGetAllCheckItemInfo(out DataTable p_dtResult)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAllCheckItemInfo(out p_dtResult);
        }

        public void m_mthQueryItemQCResult(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryItemQCResult(objSch, out p_dtResult);
        }

        public void m_mthQueryQCCheckItem(string p_strDeviceID, out DataTable p_dtResult)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQCCheckItem(p_strDeviceID, out p_dtResult);
        }

        public void m_mthQueryQCLot(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryQCLot(objSch, out p_dtResult);
        }

        public void m_mthQueryResult(clsLisQCBatchSchVO objSch, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, out DataTable p_dtQcResult)
        {
            (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryResult(objSch, p_strStartDate, p_strEndDate, out p_dtResult, out p_dtQcResult);
        }

        public void Reload(DateTime p_dtpDateStart, DateTime p_DateEnd)
        {
            clsLisQCDataVO[] avoArray = null;
            clsLisQCReportVO[] tvoArray = null;
            long num = 0;
            bool flag;

            if (this.IsNull)
                return;

            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCData(out avoArray, this.Seq, p_dtpDateStart, p_DateEnd);
            if (num > 0)
            {
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCReport(this.Seq, p_dtpDateStart, p_DateEnd, enmQCStatus.Natrural, out tvoArray);
            }

            if (num > 0)
            {
                this.m_objDatas.Clear();
                this.m_objReports.Clear();
                this.m_strBrokenRules = string.Empty;

                if (avoArray != null)
                {
                    this.m_objDatas.AddRange(avoArray);
                }
                if (tvoArray != null)
                {
                    this.m_objReports.AddRange(tvoArray);
                }
                this.m_datBegin = p_dtpDateStart;
                this.m_datEnd = p_DateEnd;
                if (this.Reloaded != null)
                {
                    this.Reloaded(this, null);
                }
            }
            else
            {
                if (this.LoadFailed != null)
                {
                    this.LoadFailed(this, new DataLoadFailedEventArgs("读取数据失败."));
                }
            }
        }

        public void Reset()
        {
            if (this.IsNull)
            {
                return;
            }
            this.m_objBatchSet = null;
            this.m_objConcentrations.Clear();
            this.m_objDatas.Clear();
            this.m_objReports.Clear();
            this.m_strBrokenRules = string.Empty;
            this.m_datBegin = DateTime.MinValue;
            this.m_datEnd = DateTime.MaxValue;

            if (this.Reseted != null)
            {
                this.Reseted(this, null);
            }
        }

        public void UpdateConcentrations(List<clsLisQCConcentrationVO> p_objConcentrations)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objConcentrations != null)
            {
                this.m_objConcentrations.Clear();
                this.m_strBrokenRules = string.Empty;

                foreach (clsLisQCConcentrationVO var in p_objConcentrations)
                {
                    clsLisQCConcentrationVO temp = new clsLisQCConcentrationVO();
                    var.m_mthCopyTo(temp);
                    this.m_objConcentrations.Add(temp);
                }

                clsLisQCDataVO[] objDatas = null;

                long lngRes = 0;

                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFind(out objDatas, this.Seq, this.m_datBegin, m_datEnd);

                if (lngRes > 0)
                {
                    this.m_objDatas.Clear();

                    if (objDatas != null)
                    {
                        this.m_objDatas.AddRange(objDatas);
                    }
                }
                else
                {
                    if (this.LoadFailed != null)
                    {
                        LoadFailed(this, new DataLoadFailedEventArgs("读取数据失败."));
                    }
                }
                if (this.ConcentrationChanged != null)
                {
                    ConcentrationChanged(this, null);
                }
            }
        }

        public void UpdateDatas(List<clsLisQCDataVO> p_objDatas)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objDatas != null)
            {
                this.m_objDatas.Clear();
                this.m_strBrokenRules = string.Empty;

                foreach (clsLisQCDataVO var in p_objDatas)
                {
                    clsLisQCDataVO temp = new clsLisQCDataVO();
                    var.m_mthCopyTo(temp);
                    this.m_objDatas.Add(temp);
                }
                if (this.DataChanged != null)
                {
                    DataChanged(this, null);
                }
            }
        }

        public void UpdateSet(clsLisQCBatchVO p_objSet)
        {
            if (this.IsNull)
            {
                return;
            }
            if (p_objSet != null)
            {
                if (!p_objSet.m_mthEquals(this.m_objBatchSet))
                {
                    p_objSet.m_mthCopyTo(this.m_objBatchSet);
                    this.m_strBrokenRules = string.Empty;

                    if (this.SetChanged != null)
                        SetChanged(this, null);
                }
            }
        }

        // Properties
        public string BrokenRules
        {
            set
            {
                if (this.IsNull)
                {
                    return;
                }
                if (value != null)
                {
                    if (this.m_strBrokenRules != value)
                    {
                        this.m_strBrokenRules = value;
                        if (this.BrokenRulesChanged != null)
                        {
                            BrokenRulesChanged(this, null);
                        }
                    }
                }
            }
            get
            {
                return this.m_strBrokenRules;
            }
        }

        public DateTime DateBegin
        {
            get
            {
                return this.m_datBegin;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return this.m_datEnd;
            }
        }

        public bool IsNull
        {
            get
            {
                if (this.m_objBatchSet == null)
                    return true;
                return false;
            }
        }

        public int Seq
        {
            get
            {
                if (this.m_objBatchSet != null)
                    return m_objBatchSet.m_intSeq;
                return DBAssist.NullInt;
            }
        }

        // Nested Types
        public class DataLoadFailedEventArgs : EventArgs
        {
            private string m_strFailedMessage;
            public string FailedMessage
            {
                get { return this.m_strFailedMessage; }
            }
            public DataLoadFailedEventArgs(string p_strFailedMessage)
            {
                this.m_strFailedMessage = p_strFailedMessage;
            }
        }

        public delegate void DataLoadFailedEventHandler(object sender, clsQCBatch.DataLoadFailedEventArgs e);
    }
}
