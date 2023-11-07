using Microsoft.AspNetCore.Mvc;
using Simple.Etl.Models;
using Simple.Etl.Web.Models;
using System.Diagnostics;
using Simple.Etl.Models.Entities;
using Simple.Etl.Actions.Receiver;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Simple.Etl.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SimpleEtlContext _context;
        private readonly TcpIpReceiver _tcpIpReceiver;
        private string received;
        public HomeController(ILogger<HomeController> logger, SimpleEtlContext context, TcpIpReceiver tcpIpReceiver)
        {
            _logger = logger;
            _context = context;
            _tcpIpReceiver = tcpIpReceiver;
            _tcpIpReceiver.Startup().Wait();
            _tcpIpReceiver.ReceivedData = (str) => { received = str; };
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestReceiver(string sendData)
        {
            IPEndPoint ipEndPoint = new(IPAddress.Any, 1302);
            using Socket client = new(
                 ipEndPoint.AddressFamily,
                 SocketType.Stream,
                 ProtocolType.Tcp);
            client.Connect(ipEndPoint);
            client.Send(Encoding.UTF8.GetBytes(sendData));
            client.Close();
            ViewBag.Received = received;
            return View("Privacy");
        }
        public IActionResult Logs()
        {
            return View(_context.Logs.AsEnumerable()) ;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}