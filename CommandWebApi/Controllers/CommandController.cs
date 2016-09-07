using CommandSvc;
using Common;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommandWebApi.Controllers
{
    public class CommandController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            ICommandSvc cmdSvc = ServiceProxy.Create<ICommandSvc>(new Uri("fabric:/CommandTest/CommandSvc"));
            
            

            var cmd = new Command() { Id = "123", CmdVerb = "TimeSync", CmdStatus=CmdStatus.Unknown };
            try {
                cmdSvc.DispatchCommand(cmd).Wait();
            } catch(System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        [HttpGet]
        public async Task<string> GetStatus(int id)
        {
            ICommandSvc cmdSvc = ServiceProxy.Create<ICommandSvc>(new Uri("fabric:/CommandTest/CommandSvc"));

            var cmd = new Command() { Id = "123" };
            CmdStatus status = CmdStatus.Unknown;
            try
            {
                status= await cmdSvc.GetCommandStatus(cmd);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //return new string[] { "value1", "value2" };
            return status.ToString();
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
