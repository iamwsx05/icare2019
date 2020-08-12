using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;

namespace Report.Entity
{
    /// <summary>
    /// rptEvent
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "rptEvent")]
    public class EntityRptEvent2 : BaseDataContract
    {
        /// <summary>
        /// rptId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "rptId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal rptId { get; set; }

        /// <summary>
        /// Eventid
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "eventId", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String eventId { get; set; }

        /// <summary>
        /// Reporttime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportTime", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String reportTime { get; set; }

        /// <summary>
        /// reportOperCode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportOperCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String reportOperCode { get; set; }

        /// <summary>
        /// reportOperName
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportOperName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String reportOperName { get; set; }

        /// <summary>
        /// reportType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportType", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String reportType { get; set; }

        /// <summary>
        /// Eventcode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "eventCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String eventCode { get; set; }

        /// <summary>
        /// Eventname
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "eventName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String eventName { get; set; }

        /// <summary>
        /// patType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "patType", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.Int32 patType { get; set; }

        /// <summary>
        /// Patno
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "patNo", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String patNo { get; set; }

        /// <summary>
        /// Patname
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "patName", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String patName { get; set; }

        /// <summary>
        /// Patsex
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "patSex", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.String patSex { get; set; }

        /// <summary>
        /// idCard
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "idCard", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.String idCard { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "birthday", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.DateTime? birthday { get; set; }

        /// <summary>
        /// contactAddr
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "contactAddr", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 15)]
        public System.String contactAddr { get; set; }

        /// <summary>
        /// Contacttel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "contactTel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 16)]
        public System.String contactTel { get; set; }

        /// <summary>
        /// Deptcode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "deptCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 17)]
        public System.String deptCode { get; set; }

        /// <summary>
        /// formId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "formId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 18)]
        public System.Decimal formId { get; set; }

        /// <summary>
        /// Opercode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "operCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 19)]
        public System.String operCode { get; set; }

        /// <summary>
        /// Recorddate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recordDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 20)]
        public System.DateTime recordDate { get; set; }

        /// <summary>
        /// status 0 无效; 1 有效
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 21)]
        public System.Int32 status { get; set; }

        /// <summary>
        /// reportDeptCode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportDeptCode", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 22)]
        public System.String reportDeptCode { get; set; }

        /// <summary>
        /// Reporttime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "reportTime2", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 3)]
        public DateTime reportTime2 { get; set; }

        [DataMember]
        public string xmlData { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string rptId = "rptId";
            public string eventId = "eventId";
            public string reportTime = "reportTime";
            public string reportOperCode = "reportOperCode";
            public string reportOperName = "reportOperName";
            public string reportType = "reportType";
            public string eventCode = "eventCode";
            public string eventName = "eventName";
            public string patType = "patType";
            public string patNo = "patNo";
            public string patName = "patName";
            public string patSex = "patSex";
            public string idCard = "idCard";
            public string birthday = "birthday";
            public string contactAddr = "contactAddr";
            public string contactTel = "contactTel";
            public string deptCode = "deptCode";
            public string formId = "formId";
            public string operCode = "operCode";
            public string recordDate = "recordDate";
            public string status = "status";
            public string reportDeptCode = "reportDeptCode";
            public string reportTime2 = "reportTime2";

            public string xmlData = "xmlData";
        }
    }

}
