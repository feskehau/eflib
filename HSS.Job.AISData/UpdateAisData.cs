using System;
using System.Linq;
using System.Threading.Tasks;
using HSS.Storage.Core.Entities;
using HSS.Storage.Core.Repository.Base;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace HSS.Job.AISData
{
    
    public class UpdateAisData
    {
        private readonly IRepository<Vessel> vesselRepository;
        
        public UpdateAisData(
            IRepository<Vessel> vesselRepository
        )
        {
            this.vesselRepository = vesselRepository;
        }

        [FunctionName("UpdateAisData")]
        public async Task Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var vesselDetails = await this.vesselRepository.GetAsync(x => x.Status.ToLower() == "active");
            var imoList = vesselDetails.Select(x => x.ImoNo.ToString()).ToList();

            string list = string.Join(",", imoList);
            log.LogInformation(list);
        }
    }
}
