using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using CommandActor.Interfaces;
using Microsoft.ServiceFabric.Data.Collections;

namespace CommandActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class CommandActor : Actor, ICommandActor
    {

        //IReliableDictionary<string, string> _commandAttributes = await this.StateManager.GetOrAddAsync <IReliableDictionary<string, string>();
        public async Task DoSomething(string cmd)
        {
            Console.WriteLine(" Something {0}",cmd);
        }

        public Task ProcessCommand(string cmdVerb, string cmdStatus, string cmdTarget)
        {
            throw new NotImplementedException();
            //_commandAttributes.Select()
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see http://aka.ms/servicefabricactorsstateserialization

            //IReliableDictionary<string, string> _commandAttributes = await this.StateManager.GetOrAddStateAsync <IReliableDictionary<string, string>>("CommandParameters", );
            //var myDictionary = await this.StateManager.GetOrAddSAsync<IReliableDictionary<string, long>>("myDictionary");
            //this.StateManager.
            

                return this.StateManager.TryAddStateAsync("count", 0);
        }

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <returns></returns>
        Task<int> ICommandActor.GetCountAsync()
        {
            return this.StateManager.GetStateAsync<int>("count");
        }

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task ICommandActor.SetCountAsync(int count)
        {
            // Requests are not guaranteed to be processed in order nor at most once.
            // The update function here verifies that the incoming count is greater than the current count to preserve order.
            return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value);
        }
    }
}
