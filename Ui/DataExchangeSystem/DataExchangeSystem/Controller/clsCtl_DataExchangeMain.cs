using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.ValueObject;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    public class clsCtl_DataExchangeMain
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public frmDataExchangeMain m_objViewer;
        /// <summary>
        /// ��������

        /// </summary>
        private clsDcl_DataExchangeMain objDomain;

        /// <summary>
        /// �����
        /// </summary>
        private DateTime dtmTBRQ;

        #endregion

        public clsCtl_DataExchangeMain()
        {
            objDomain = new clsDcl_DataExchangeMain();
        }

        /// <summary>
        /// �Զ��ϴ�����
        /// </summary>
        public void m_mthUploadExchangeData()
        {
            this.m_objViewer.rtb_showLog.Focus();
            if (isToUpload("�ɹ����"))
            {
                m_mthInStorage();
            }
            if (isToUpload("����"))
            {
                m_mthOutStorage();
            }
            if (isToUpload("סԺ����"))
            {
                m_mthInHospital();
            }
            if (isToUpload("��������"))
            {
                m_mthOutpatient();
            }

        }

        /// <summary>
        /// �Ƿ��ϴ�������
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public bool isToUpload(string strTableName)
        {
            bool isToUp = false;
            for (int i = 0; i < this.m_objViewer.dataGridViewUpdata.RowCount; i++)
            {
                if (this.m_objViewer.dataGridViewUpdata.Rows[i].Cells[0].Value.ToString().Trim() == strTableName)
                {
                    if ((bool)this.m_objViewer.dataGridViewUpdata.Rows[i].Cells[1].EditedFormattedValue == false)
                    {
                        isToUp = false;
                    }
                    else
                    {
                        isToUp = true;
                    }
                }
            }
            return isToUp;
        }

        #region ҩ���������
        /// <summary>
        /// ҩ����������ϴ�
        /// </summary>
        public void m_mthInStorage()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "׼������ҩ����������ϴ�,���Ժ�...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "��������ҩ���������,���Ժ�...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsInStorageData_VO> lisInStorageData = new List<clsInStorageData_VO>();
            clsInStorageData_VO[] ArrInStorageData = null;
            try
            {
                long lngRes = objDomain.m_lngGetInStorageData(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrInStorageData);
                if (ArrInStorageData != null && ArrInStorageData.Length > 0)
                {
                    lisInStorageData.AddRange(ArrInStorageData);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "����ҩ���������ʧ��,��˲�!";
                    this.m_objViewer.Update();
                }
                if (lisInStorageData.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "׼���ϴ�ҩ���������,���Ժ�...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisInStorageData.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �����ϴ�ҩ��������ݹ�" + lisInStorageData.Count.ToString() + "��,�����ϴ���" + (i + 1).ToString() + "��");
                        this.m_objViewer.tslStat.Text = "�����ϴ���" + (i + 1).ToString() + "������";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadInStorageData(lisInStorageData[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�ҩ��������ݵ�" + (i + 1).ToString() + "������ʧ��,��˲�");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�ҩ��������ݵ�" + (i + 1).ToString() + "�����ݳɹ�");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "�ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�";
                    this.m_objViewer.rtb_showLog.AppendText("\n ҩ����������ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "ҩ����������ϴ��ɹ�");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "��ʱ�����û���������,��˲�!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region ҩ���������
        /// <summary>
        /// ҩ���������
        /// </summary>
        public void m_mthOutStorage()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "׼������ҩ����������ϴ�,���Ժ�...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "��������ҩ���������,���Ժ�...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsOutStorageData_VO> lisOutStorageData = new List<clsOutStorageData_VO>();
            clsOutStorageData_VO[] ArrOutStorageData = null;
            try
            {
                long lngRes = objDomain.m_lngGetOutStorageData(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrOutStorageData);
                if (ArrOutStorageData != null && ArrOutStorageData.Length > 0)
                {
                    lisOutStorageData.AddRange(ArrOutStorageData);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "����ҩ���������ʧ��,��˲�!";
                    this.m_objViewer.Update();
                }
                if (lisOutStorageData.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "׼���ϴ�ҩ���������,���Ժ�...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisOutStorageData.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �����ϴ�ҩ��������ݹ�" + lisOutStorageData.Count.ToString() + "��,�����ϴ���" + (i + 1).ToString() + "��");
                        this.m_objViewer.tslStat.Text = "�����ϴ���" + (i + 1).ToString() + "������";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadOutStorageData(lisOutStorageData[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�ҩ��������ݵ�" + (i + 1).ToString() + "������ʧ��,��˲�");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�ҩ��������ݵ�" + (i + 1).ToString() + "�����ݳɹ�");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "�ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�";
                    this.m_objViewer.rtb_showLog.AppendText("\n ҩ����������ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "ҩ����������ϴ��ɹ�");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "��ʱ�����û�г�������,��˲�!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region סԺ��������
        /// <summary>
        /// סԺ��������
        /// </summary>
        public void m_mthInHospital()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "׼������סԺ���������ϴ�,���Ժ�...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "��������סԺ��������,���Ժ�...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsInHospital_VO> lisInHospital = new List<clsInHospital_VO>();
            clsInHospital_VO[] ArrInHospital = null;
            try
            {
                long lngRes = objDomain.m_lngGetInHospital(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrInHospital);
                if (ArrInHospital != null && ArrInHospital.Length > 0)
                {
                    lisInHospital.AddRange(ArrInHospital);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "����סԺ��������ʧ��,��˲�!";
                    this.m_objViewer.Update();
                }
                if (lisInHospital.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "׼���ϴ�סԺ��������,���Ժ�...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisInHospital.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �����ϴ�סԺ�������ݹ�" + lisInHospital.Count.ToString() + "��,�����ϴ���" + (i + 1).ToString() + "��");
                        this.m_objViewer.tslStat.Text = "�����ϴ���" + (i + 1).ToString() + "������";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadInHospital(lisInHospital[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�סԺ�������ݵ�" + (i + 1).ToString() + "������ʧ��,��˲�");
                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ�סԺ�������ݵ�" + (i + 1).ToString() + "�����ݳɹ�");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "�ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�";
                    this.m_objViewer.rtb_showLog.AppendText("\n סԺ���������ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("["+DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "סԺ���������ϴ��ɹ�");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "��ʱ�����û��סԺ��������,��˲�!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        public void m_mthOutpatient()
        {
            this.m_objViewer.tsp_showProgress.Maximum = 100;
            this.m_objViewer.tsp_showProgress.Minimum = 0;

            this.m_objViewer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tslStat.Text = "׼�������������������ϴ�,���Ժ�...";
            this.m_objViewer.Update();

            this.m_objViewer.Invalidate(true);
            this.m_objViewer.tsp_showProgress.Value = 10;
            this.m_objViewer.tslStat.Text = "��������������������,���Ժ�...";
            this.m_objViewer.tsp_showProgress.Value = 20;
            this.m_objViewer.Update();

            List<clsOutpatient_VO> lisOutpatient = new List<clsOutpatient_VO>();
            clsOutpatient_VO[] ArrOutpatient = null;
            try
            {
                long lngRes = objDomain.m_lngGetOutpatient(Convert.ToDateTime(this.m_objViewer.dtmBegin.Value.ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(this.m_objViewer.dtmEnd.Value.ToString("yyyy-MM-dd 23:59:59")), out ArrOutpatient);
                if (ArrOutpatient != null && ArrOutpatient.Length > 0)
                {
                    lisOutpatient.AddRange(ArrOutpatient);
                }
                if (lngRes < 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "����������������ʧ��,��˲�!";
                    this.m_objViewer.Update();
                }
                if (lisOutpatient.Count > 0)
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 40;
                    this.m_objViewer.tslStat.Text = "׼���ϴ�������������,���Ժ�...";
                    this.m_objViewer.Update();

                    for (int i = 0; i < lisOutpatient.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �����ϴ������������ݹ�" + lisOutpatient.Count.ToString() + "��,�����ϴ���" + (i + 1).ToString() + "��");
                        this.m_objViewer.tslStat.Text = "�����ϴ���" + (i + 1).ToString() + "������";
                        this.m_objViewer.Update();
                        lngRes = objDomain.m_lngUploadOutpatient(lisOutpatient[i]);
                        if (lngRes < 0)
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ������������ݵ�" + (i + 1).ToString() + "������ʧ��,��˲�");
                            //this.m_objViewer.rtb_showLog.AppendText("\n [" +lisOutpatient[i].BMBH+lisOutpatient[i]. "");

                        }
                        else
                        {
                            this.m_objViewer.rtb_showLog.AppendText("\n [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] �ϴ������������ݵ�" + (i + 1).ToString() + "�����ݳɹ�");
                        }

                        this.m_objViewer.tsp_showProgress.Value = i % 100;

                        this.m_objViewer.Update();
                    }
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tslStat.Text = "�ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�";
                    this.m_objViewer.rtb_showLog.AppendText("\n �������������ϴ����,��鿴��־ȷ���Ƿ�ȫ���ɹ�");
                    com.digitalwave.Utility.clsLogText objLogErr = new clsLogText();
                    objLogErr.LogError("[" + DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss") + "]: " + "�������������ϴ��ɹ�");
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.Update();

                }
                else
                {
                    this.m_objViewer.Invalidate(true);
                    this.m_objViewer.tsp_showProgress.Value = 100;
                    this.m_objViewer.tslStat.Text = "��ʱ�����û��������������,��˲�!";
                    this.m_objViewer.Update();
                }
            }
            catch
            {

            }
            finally
            {
                this.m_objViewer.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

    }
}
