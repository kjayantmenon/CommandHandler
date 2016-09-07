using CommandSvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CommandWebApi.Controllers
{
    public class CommandController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            ICommandSvc cmdSvc = ServiceProxy.Create<ICommandSvc>(new Uri("fabric:/CommandTest/CommandSvc"));
            
            

            var cmd = new Command() { Id = "123", CmdVerb = "TimeSync", CmdStatus=Status.Created };
            try {
                cmdSvc.DispatchCommand(cmd).Wait();
            } catch(System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            ICommandSvc cmdSvc = ServiceProxy.Create<ICommandSvc>(new Uri("fabric:/CommandTest/CommandSvc"));

            var cmd = new Command() { Id = "123" };
            try
            {
                cmdSvc.GetCommandStatus(cmd).Wait();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //return new string[] { "value1", "value2" };
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
