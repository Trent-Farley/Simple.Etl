using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Etl.Models.Interfaces
{
    public interface IReceiver<T> where T : class
    {
        Task Startup();
        Action<string> ReceivedData { set; }
        Task ShutDown();
    }
}
