using CommandActor;
using Common;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSvc
{
    public interface ICommandSvc: IService
    {
        Task DispatchCommand(Command command);
        Task RecvCommandResponse(CommandResponse response);
        Task<CmdStatus> GetCommandStatus(Command cmd);
    }

    public class Command
    {
        public string Id { get; set; }
        public string CmdVerb { get; set; }

        public CmdStatus CmdStatus { get; set; }
    }

    
}
