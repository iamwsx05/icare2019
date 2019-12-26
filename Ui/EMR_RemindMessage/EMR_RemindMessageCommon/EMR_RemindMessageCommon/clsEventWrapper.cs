using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace com.digitalwave.iCare.RemindMessage
{
    /// <summary>
    /// ‘∂≥Ã∂‘œÛ
    /// </summary>
    public class clsEventWrapper : MarshalByRefObject
    {
        public event BroadCastEventHandler LocalBroadCastEvent;

        public void BroadCasting(string message)
        {
            LocalBroadCastEvent(message);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
