using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using CommandActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using CommandActor;
using Common;

namespace CommandSvc
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class CommandSvc : StatelessService, ICommandSvc
    {
        public CommandSvc(StatelessServiceContext context)
            : base(context)
        { }

        
        public async Task DispatchCommand(Command command)
        {
            ActorId cmdActorId = new ActorId(command.Id);
            // This only creates a proxy object, it does not activate an actor or invoke any methods yet.
            ICommandActor _cmdActor = ActorProxy.Create<ICommandActor>(cmdActorId, new Uri("fabric:/CommandTest/CommandActorService"));
            await _cmdActor.DoSomething(command.CmdVerb);
            Console.WriteLine("New Command Dispatched");

        }

        

        public async Task<CmdStatus> GetCommandStatus(Command cmd)
        {
            ActorId cmdActorId = new ActorId(cmd.Id);
            ICommandActor _cmdActor = ActorProxy.Create<ICommandActor>(cmdActorId, new Uri("fabric:/CommandTest/CommandActorService"));
            CmdStatus status = await  _cmdActor.GetCommandStatus();
            return status;
        }

        public async Task RecvCommandResponse(CommandResponse response)
        {
            Console.WriteLine("New Command received");
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(context =>
            this.CreateServiceRemotingListener(context)) };

            //return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

           /* while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }*/
        }
    }
}
