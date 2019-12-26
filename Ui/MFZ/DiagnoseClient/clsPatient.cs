using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnoseClient
{
    public class clsPatient
    {
        private string m_name;

        public clsPatient(string name)
        {
            this.m_name = name;
        }

        public string Name
        {
            get { return m_name; }
        }
    }
}
