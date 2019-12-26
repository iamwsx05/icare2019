using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using weCare.Core.Entity;
using System.Runtime.Serialization;

namespace com.digitalwave.iCare.gui.MFZ
{
    [Serializable]
    public class clsDataSerial
    {
        #region ��  ��
        public int diagAreaID = -1;
        public DateTime serializeTime;
        public Dictionary<string, List<clsMFZPatientVO>> queueDictionary;
        

        private Hashtable hasCalledPatient;

        public Hashtable HasCalledPatient
        {
            get { return hasCalledPatient==null?new Hashtable():hasCalledPatient; }
            set { hasCalledPatient = value; }
        }

        private int m_intSchemeId;

        /// <summary>
        /// ��ȡ�����ð��Id
        /// </summary>
        public int SchemeId
        {
            get { return m_intSchemeId; }
            set { m_intSchemeId = value; }
        }


        [NonSerialized]
        private string serializationFileName; 
        #endregion

        #region ���캯��
        public clsDataSerial(int p_diagAreaID, Dictionary<string, List<clsMFZPatientVO>> p_queueDictionary, Hashtable p_hasCalledPatient)
            : this(p_diagAreaID)
        {
            this.queueDictionary = p_queueDictionary;
            this.hasCalledPatient = p_hasCalledPatient;
        }
        public clsDataSerial(int p_diagAreaID)
        {
            this.diagAreaID = p_diagAreaID;
            serializationFileName = this.GetType().Assembly.Location.Replace(".dll", diagAreaID.ToString() + ".data"); // �趨���л��ļ�����
            serializeTime = DateTime.Now;
        } 
        #endregion

        //���л����ݵ�����
        public bool SerializeDataToLocal()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                stream = new FileStream(serializationFileName, FileMode.OpenOrCreate);
                formatter.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError("���л��ļ�����:" + ex.Message);
                return false;
            }
            finally 
            {
                stream.Close();
            }

            return true;
        }

        /// �����л�
        public bool DeSerialize()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                stream = new FileStream(serializationFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                clsDataSerial localData = (clsDataSerial)formatter.Deserialize(stream);
                queueDictionary = localData.queueDictionary;
                hasCalledPatient = localData.hasCalledPatient;
                serializeTime = localData.serializeTime;
                diagAreaID = localData.diagAreaID;
                m_intSchemeId = localData.m_intSchemeId;
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError("�ļ������л�����:" + ex.Message);
                return false;
            }
            finally 
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return true;
        }
    }
}