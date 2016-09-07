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
        Task GetCommandStatus(Command cmd);
    }

    public class Command
    {
        public string Id { get; set; }
        public string CmdVerb { get; set; }

        public Status CmdStatus { get; set; }
    }

    public enum Status
    {
        Created, Dispatched, Delivered, ExecInProgress, ExecSuccess, ExecFailure
    }
}
