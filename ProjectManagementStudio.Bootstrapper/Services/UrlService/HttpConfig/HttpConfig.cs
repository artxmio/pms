using System.Runtime.Serialization;

namespace ProjectManagementStudio.Bootstrapper.Services.UrlService;

[DataContract]
public class HttpConfig
{
    [DataMember(Name = "token")]
    public required string Token { get; set; }

    [DataMember(Name = "urlBase")]
    public required string URLBase { get; set; }
}