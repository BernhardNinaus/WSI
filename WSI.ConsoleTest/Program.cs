using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using WSI.ConsoleTest;

var server = "http://localhost:5197";
var httpClient = new HttpClient() {
    BaseAddress = new Uri(server)
};
var apiPath = server + "/v1.0/Kommentar";
Action<KommentarModel, int> logKommentar;

logKommentar = (kom, i) => {
};

KommentarModel kommentar;

var top = Guid.NewGuid();
var mid = Guid.NewGuid();
var bot = Guid.NewGuid();

await httpClient.PostAsJsonAsync(apiPath, KommentarModel.Neu(top, "Titel", "Thomas", "Hallo, wie gehts?"));
kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{top}");
Console.WriteLine("Neu erstellt 1");
kommentar.LogKommentar();
Console.WriteLine("");


await httpClient.PostAsJsonAsync(apiPath, KommentarModel.Neu(mid, "", "Th", "Hab beim Kahoot alles richtig", top));
kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{mid}");
Console.WriteLine("Neu erstellt 2");
kommentar.LogKommentar();
Console.WriteLine("");


await httpClient.PostAsJsonAsync(apiPath, KommentarModel.Neu(bot, "", "Thomas", "Sehr gut!", mid));
kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{bot}");
Console.WriteLine("Neu erstellt 3");
kommentar.LogKommentar();
Console.WriteLine("");



kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{top}");
Console.WriteLine("Alle:");
kommentar.LogKommentar();
Console.WriteLine("");


Console.WriteLine("Lösche mittleres Kommentar");
var request = new HttpRequestMessage {
    Method = HttpMethod.Delete,
    RequestUri = new Uri("http://localhost:5197/v1.0/Kommentar"),
    Content = new StringContent(JsonConvert.SerializeObject(new { id = mid, schreibSchlüssel = mid }), Encoding.UTF8, "application/json")
};
var response = await httpClient.SendAsync(request);


kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{top}");
Console.WriteLine("Alle:");
kommentar.LogKommentar();
Console.WriteLine("");


await httpClient.PostAsJsonAsync(apiPath, KommentarModel.Neu(Guid.NewGuid(), "", "Th", "Noch eins", top));
await httpClient.PostAsJsonAsync(apiPath, KommentarModel.Neu(Guid.NewGuid(), "", "Th", "Noch zwei", top));


kommentar = await httpClient.GetFromJsonAsync<KommentarModel>($"{apiPath}/{top}");
Console.WriteLine("Weite Kommentare in der Mitte:");
kommentar.LogKommentar();
Console.WriteLine("");