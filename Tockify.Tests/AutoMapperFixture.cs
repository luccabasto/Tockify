using AutoMapper;
using Tockify.Application.Mappings;

namespace Tockify.Tests.Helpers
{
    public static class AutoMapperFixture
    {
        private static IMapper? _mapper;

        public static IMapper MapperInstance
        {
            get
            {
                if (_mapper is null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<ClientUserProfile>();
                    });
                    _mapper = config.CreateMapper();
                }

                return _mapper;
            }
        }
    }
}
