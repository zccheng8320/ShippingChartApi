using System.Text;

namespace WebApplication1.DeviceGuid
{
    internal class ClientDeviceGuidProvider : IClientDeviceGuidProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        const string DeviceGuidSessionKey = "XWvTlsDQDR";
        public ClientDeviceGuidProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string Get()
        {
            var session = _httpContextAccessor?.HttpContext?.Session;
            if (session == null)
                throw new InvalidOperationException();
            if (session.TryGetValue(DeviceGuidSessionKey, out var value))
                return Encoding.ASCII.GetString(value);
            var newDeviceId = Guid.NewGuid().ToString("N");
            session.SetString(DeviceGuidSessionKey, newDeviceId);
            return newDeviceId;
        }
    }
}
