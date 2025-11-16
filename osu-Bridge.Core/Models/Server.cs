using osu_Bridge.Core.Attributes;

namespace osu_Bridge.Core.Models;

public class Server
{
    [Title("Server")]
    [UIField("サーバー名")]
    public string Name { get; set; } = "新しいサーバー";

    [UIField("サーバーエンドポイント")]
    [ConfigParameter("CredentialEndpoint")]
    [PlaceHolder("Bancho")]
    public string ServerEndpoint { get; set; } = string.Empty;
}
