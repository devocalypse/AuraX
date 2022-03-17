using System.ServiceProcess;
using System.Configuration.Install;
using System.Reflection;

namespace AuraX
{
    class Program : ServiceBase
    {
        static void Main(string[] args)
        {
            if (System.Environment.UserInteractive)
            {
                switch (string.Concat(args))
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        StartService();
                        break;
                    case "--uninstall":
                        StopService();
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                    case "--stop":
                        StopService();
                        break;
                    case "--start":
                        StartService();
                        break;
                }
            }
            else
            {
                ServiceBase.Run(new AuraX());
            }
        }

        public static void StopService()
        {
            using (ServiceController service = new ServiceController("AuraX"))
            {
                if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                    service.Stop();
            }
        }

        public static void StartService()
        {
            using (ServiceController service = new ServiceController("AuraX"))
            {
                if (service.Status != ServiceControllerStatus.Running && service.Status != ServiceControllerStatus.StopPending)
                    service.Start();
            }
            
        }

    }
}
