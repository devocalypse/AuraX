using System.ServiceProcess;
using AuraServiceLib;

namespace AuraX
{
    public partial class AuraX : ServiceBase
    {
        private readonly IAuraSdk2 sdk;
        private IAuraSyncDeviceCollection syncDevices;
        public AuraX()
        {
            //Allow actions on workstation session changes
            CanHandleSessionChangeEvent = true;

            InitializeComponent();
            sdk = (IAuraSdk2)new AuraSdk();
            syncDevices = sdk.Enumerate(0);
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            //Handle session changes via https://tousu.in/?qa=917098/
            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                    //Debug.WriteLine(changeDescription.SessionId + " logon");
                    break;
                case SessionChangeReason.SessionLogoff:
                    //Debug.WriteLine(changeDescription.SessionId + " logoff");
                    break;
                case SessionChangeReason.SessionLock:
                    //Session is locked
                    Darken();
                    break;
                case SessionChangeReason.SessionUnlock:
                    //Session unlocked - release
                    Release();
                    break;
            }

            base.OnSessionChange(changeDescription);
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
            Release();
        }

        private void Darken()
        {
            sdk.SwitchMode();
            
            foreach (IAuraSyncDevice dev in syncDevices)
            {
                foreach (IAuraRgbLight light in dev.Lights)
                {
                    //Console.WriteLine($"R={light.Red} G={light.Green} B={light.Blue}");
                    light.Color = 0x00000000;
                }

                dev.Apply();
            }
        }

        private void Release()
        {
            sdk.ReleaseControl(0);
        }
    }
}
