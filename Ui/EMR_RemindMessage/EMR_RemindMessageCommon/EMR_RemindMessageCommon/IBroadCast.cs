using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.iCare.RemindMessage
{
    public delegate void BroadCastEventHandler(string info);

    public interface IBroadCast
    {
        void SubscribeEvent(string eventinfo, BroadCastEventHandler eventhandler);
        void UnSubscribeEvent(string eventinfo, BroadCastEventHandler eventhandler);
        void RaiseEvent(string eventName);
    }
}
