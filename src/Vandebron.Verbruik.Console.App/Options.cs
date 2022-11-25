using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Vandebron.Verbruik.Console.App
{
    public class Options
    {

        [Option("consumer", Required = true, HelpText = "ID of consumer (can be found by inspecting usage URLs in vandebron portal)")]
        public string ConsumerId { get; set; }

        [Option("connection", Required = true, HelpText = "ID of connection (can be found by inspecting usage URLs in vandebron portal)")]
        public string ConnectionId { get; set; }

        [Option("token", Required = true, HelpText = "Authorization token")]
        public string Token { get; set; }

    }
}
