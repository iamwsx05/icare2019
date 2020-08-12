using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using weCare.Core.Entity;

namespace weCare.eApp
{
    [DataContract, Serializable]
    public class EntityPrint 
    {
        [DataMember]
        public byte[] FA { get; set; }
        [DataMember]
        public byte[] FB { get; set; }

        [DataMember]
        public string F01 { get; set; }
        [DataMember]
        public string F02 { get; set; }
        [DataMember]
        public string F03 { get; set; }
        [DataMember]
        public string F04 { get; set; }
        [DataMember]
        public string F05 { get; set; }
        [DataMember]
        public string F06 { get; set; }
        [DataMember]
        public string F07 { get; set; }
        [DataMember]
        public string F08 { get; set; }
        [DataMember]
        public string F09 { get; set; }
        [DataMember]
        public string F10 { get; set; }
        [DataMember]
        public string F11 { get; set; }
        [DataMember]
        public string F12 { get; set; }
        [DataMember]
        public string F13 { get; set; }
        [DataMember]
        public string F14 { get; set; }
        [DataMember]
        public string F15 { get; set; }
        [DataMember]
        public string F16 { get; set; }
        [DataMember]
        public string F17 { get; set; }
        [DataMember]
        public string F18 { get; set; }
        [DataMember]
        public string F19 { get; set; }
        [DataMember]
        public string F20 { get; set; }
        [DataMember]
        public string F21 { get; set; }
        [DataMember]
        public string F22 { get; set; }
        [DataMember]
        public string F23 { get; set; }
        [DataMember]
        public string F24 { get; set; }
        [DataMember]
        public string F25 { get; set; }
        [DataMember]
        public string F26 { get; set; }
        [DataMember]
        public string F27 { get; set; }
        [DataMember]
        public string F28 { get; set; }
        [DataMember]
        public string F29 { get; set; }
        [DataMember]
        public string F30 { get; set; }

        [DataMember]
        public int F31 { get; set; }
        [DataMember]
        public int F32 { get; set; }
        [DataMember]
        public int F33 { get; set; }
        [DataMember]
        public int F34 { get; set; }
        [DataMember]
        public int F35 { get; set; }
        [DataMember]
        public int F36 { get; set; }
        [DataMember]
        public int F37 { get; set; }
        [DataMember]
        public int F38 { get; set; }
        [DataMember]
        public int F39 { get; set; }
        [DataMember]
        public int F40 { get; set; } 
    }
}
