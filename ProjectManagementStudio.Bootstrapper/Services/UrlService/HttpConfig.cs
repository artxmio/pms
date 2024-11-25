using System.Runtime.Serialization;

namespace ProjectManagementStudio.Bootstrapper.Services.UrlService;

[DataContract]
internal class HttpConfig
{
    [DataMember(Name = "token")]
    public required string Token { get; set; }

    [DataMember(Name = "urlBase")]
    public required string URLBase { get; set; }
}