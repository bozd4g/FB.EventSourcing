using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FB.EventSourcing.Application
{
    public class BaseAppService
    {
        protected IMapper Mapper { get; private set; }
        protected ILogger Logger { get; private set; }

        protected BaseAppService(IMapper mapper, ILogger logger)
        {
            this.Mapper = mapper;
            this.Logger = logger;
        }
    }
}