using Registration.Itf;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Microsoft.Practices.Unity;
using System;
using Middle.Itf;

namespace Registration.Ui
{
    public class ProxyMiddle : IDisposable
    {
        public ItfMiddle Service = null;

        public ProxyMiddle()
        {
            if (GlobalAppConfig.RunningMode == 2)
            {
                Service = Function.UnitySection("unity.xml", "unityRegistration", "middle").Resolve<ItfMiddle>();
            }
            else if (GlobalAppConfig.RunningMode == 3)
            {
                try
                {
                    Service = WcfEndpoint.Fac<ItfMiddle>().CreateChannel(WcfEndpoint.HisEndpointAddress(this.GetType().Name));
                    Service.Verify();
                }
                catch
                {
                    if (WcfEndpoint.AllowChange)
                    {
                        WcfEndpoint.ChangeServer();
                        Service = WcfEndpoint.Fac<ItfMiddle>().CreateChannel(WcfEndpoint.HisEndpointAddress(this.GetType().Name));
                    }
                }
            }
        }

        public void Dispose()
        {
            if (Service != null)
            {
                Service.Dispose();
                Service = null;
            }
        }

    }
}
