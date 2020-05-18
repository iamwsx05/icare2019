using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.iCare.ValueObject
{
    /// <summary>
    /// ��ɽ�������VO
    /// </summary>
    [Serializable]
    public class clsInStorageData_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [ҵ�����] [nvarchar] (20)  
        /// </summary>
        public string YWLB;
        /// <summary>
        /// [���ݱ��] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        ///  [����] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [ҩ���] [nvarchar] (20)  
        /// </summary>
        public string YKH;
        /// <summary>
        /// [ҩ������] [nvarchar] (100)  
        /// </summary>
        public string YKMC;
        /// <summary>
        /// [��λ���] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string DWBH;
        /// <summary>
        /// [��λ����] [nvarchar] (100)  
        /// </summary>
        public string DWMC;
        /// <summary>
        /// [��Ŀ���] [nvarchar] (20)  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [��Ŀ����] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [������] [float] NOT NULL 
        /// </summary>
        public float MRJE;
        /// <summary>
        /// [���۽��] [float] NOT NULL 
        /// </summary>
        public float LSJE;
        /// <summary>
        /// [������] [float] NOT NULL 
        /// </summary>
        public float JLCJ;
        /// <summary>
        /// [��ʶ] [nvarchar] (20) 
        /// </summary>
        public string BZ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (50)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [ƾ֤����] [smalldatetime]  
        /// </summary>
        public string PZRQ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20)  
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [����] [nvarchar] (10) 
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// ��ɽ��������VO
    /// </summary>
    [Serializable]
    public class clsOutStorageData_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [ҵ�����] [nvarchar] (20)  
        /// </summary>
        public string YWLB;
        /// <summary>
        /// [���ݱ��] [nvarchar] (20)  NOT NULL  
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [����] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [ҩ���] [nvarchar] (20)  
        /// </summary>
        public string YKH;
        /// <summary>
        /// [ҩ������] [nvarchar] (100)  
        /// </summary>
        public string YKMC;
        /// <summary>
        /// [��λ���] [nvarchar] (20)  NOT NULL  
        /// </summary>
        public string DWBH;
        /// <summary>
        /// [��λ����] [nvarchar] (100)  
        /// </summary>
        public string DWMC;
        /// <summary>
        /// [��Ŀ���] [nvarchar] (20)  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [��Ŀ����] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [������] [float] NOT NULL  
        /// </summary>
        public float MRJE;
        /// <summary>
        /// [���۽��] [float] NOT NULL 
        /// </summary>
        public float LSJE;
        /// <summary>
        /// [������] [float] NOT NULL  
        /// </summary>
        public float JLCJ;
        /// <summary>
        /// [��ʶ] [nvarchar] (20) 
        /// </summary>
        public string BZ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (50)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [ƾ֤����] [smalldatetime]  
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [����] [nvarchar] (10) 
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// ��ɽסԺ��������
    /// </summary>
    [Serializable]
    public class clsInHospital_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [���ݱ��] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [��ʶ] [nvarchar] (20)  
        /// </summary>
        public string BZ;
        /// <summary>
        /// [����] [smalldatetime] NOT NULL 
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [���ű��] [nvarchar] (20)   NOT NULL 
        /// </summary>
        public string BMBH;
        /// <summary>
        /// [��������] [nvarchar] (100)  
        /// </summary>
        public string BMMC;
        /// <summary>
        /// [ҽ�����] [nvarchar] (20) 
        /// </summary>
        public string YSBH;
        /// <summary>
        /// [ҽ������] [nvarchar] (100) 
        /// </summary>
        public string YSMC;
        /// <summary>
        /// [��Ŀ���] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [��Ŀ����] [nvarchar] (100)  
        /// </summary>
        public string XMMC;
        /// <summary>
        /// [��Ŀ���] [float] NOT NULL  
        /// </summary>
        public float XMJE;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20)  
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [ƾ֤����] [smalldatetime] 
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [����] [nvarchar] (10)  
        /// </summary>
        public string JZ;
    }

    /// <summary>
    /// ��ɽ������������
    /// </summary>
    [Serializable]
    public class clsOutpatient_VO : com.digitalwave.iCare.ValueObject.clsICARE_BaseVO
    {
        /// <summary>
        /// [���ݱ��] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string DJBH;
        /// <summary>
        /// [��ʶ] [nvarchar] (20)  
        /// </summary>
        public string BZ;
        /// <summary>
        /// [����] [smalldatetime] NOT NULL  
        /// </summary>
        public DateTime RQ;
        /// <summary>
        /// [���ű��] [nvarchar] (20)  NOT NULL 
        /// </summary>
        public string BMBH;
        /// <summary>
        /// [��������] [nvarchar] (100)  
        /// </summary>
        public string BMMC;
        /// <summary>
        /// [ҽ�����] [nvarchar] (20)  
        /// </summary>
        public string YSBH;
        /// <summary>
        /// [ҽ������] [nvarchar] (100)  
        /// </summary>
        public string YSMC;
        /// <summary>
        /// [��Ŀ���] [nvarchar] (20)   NOT NULL  
        /// </summary>
        public string XMBH;
        /// <summary>
        /// [��Ŀ����] [nvarchar] (100) 
        /// </summary>
        public string XMMC;
        /// <summary>
        ///  [��Ŀ���] [float] NOT NULL  
        /// </summary>
        public float XMJE;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20) 
        /// </summary>
        public string PZLB;
        /// <summary>
        /// [ƾ֤����] [smalldatetime]  
        /// </summary>
        public DateTime PZRQ;
        /// <summary>
        /// [ƾ֤���] [nvarchar] (20) 
        /// </summary>
        public string PZBH;
        /// <summary>
        /// [����] [nvarchar] (10) 
        /// </summary>
        public string JZ;
        /// <summary>
        /// [�շѴ���ʶ] [nvarchar] (50) 
        /// </summary>
        public string SFCBZ;
    }
}
