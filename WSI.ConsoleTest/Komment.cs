using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace WSI.ConsoleTest;

public class KommentarModel {
    public Guid Id { get; set; } = new Guid();

    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string Kommentar { get; set; } = string.Empty;

    public DateTime Erstellt { get; set; } = DateTime.Now.ToUniversalTime();

    public List<KommentarModel> WeitereKommentare { get; set; }

    public static async Task<KommentarModel> Neu(HttpClient client, Guid id, string title, string name, string kommentar, Guid? übergeordnet = null) {
        await client.PostAsJsonAsync("/v1.0/Kommentar", new { 
            id, title, name, kommentar, 
            schreibSchlüssel = id, 
            weitereKommentareErlauben = true,
            maximalKomentarlänge = 50,
            übergeordneterKommentarId = übergeordnet
        });
        
        return await GetById(client, id);
    }

    public static async Task<KommentarModel> GetById(HttpClient client, Guid id)
        => await client.GetFromJsonAsync<KommentarModel>($"/v1.0/Kommentar/{id}");

    public static async Task DeleteById(HttpClient client, Guid id) {
        var request = new HttpRequestMessage {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"{client.BaseAddress}v1.0/Kommentar"),
            Content = new StringContent(JsonConvert.SerializeObject(new { id, schreibSchlüssel = id }), Encoding.UTF8, "application/json")
        };

        await client.SendAsync(request);
    }

    public override string ToString() => _toString();

    private string _toString(int i = 0) {
        var ret = $"{new String(' ', i)}{Erstellt.ToLocalTime():dd.MM.yyyy HH:mm:ss}";
        ret = $"{ret,-30} - [{Name,-7}]: {Kommentar}";

        i += 2;

        foreach (var unterKom in WeitereKommentare) {
            ret += "\n" + unterKom._toString(i);
        }

        return ret;
    }
}
