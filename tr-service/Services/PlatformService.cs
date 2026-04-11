using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Platform.Response;
using tr_core.Repositories;
using tr_core.Services;

namespace tr_service.Services
{
    public class PlatformService(IPlatformRepository repository, IMapper mapper) : IPlatformService
    {
        public async Task<List<PlatformResponse>> GetAllAsync()
        {
            var platforms = await repository.GetAllAsync();

            return mapper.Map<List<PlatformResponse>>(platforms);
        }
    }
}
