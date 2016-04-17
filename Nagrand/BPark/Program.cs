using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BPark
{
    static class Program
    {
        /// <summary>
        ///     El principal punto de entrada para la aplicación.
        /// </summary>
        static void Main()
        {
            #if DEBUG
                        BPark _service = new BPark();
                        _service.OnDebug();
                        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #else
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[]
                        {
                            new BPark()
                        };
                        ServiceBase.Run(ServicesToRun);
            #endif
        }
    }
}
