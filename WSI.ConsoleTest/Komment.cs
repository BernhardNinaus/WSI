namespace WSI.ConsoleTest;

public class KommentarModel {
    public Guid Id { get; set; } = new Guid();

    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string Kommentar { get; set; } = string.Empty;

    public DateTime Erstellt { get; set; } = DateTime.Now.ToUniversalTime();

    public List<KommentarModel> WeitereKommentare { get; set; }

    public static object Neu(Guid id, string title, string name, string kommentar, Guid? übergeordnet = null) {
        return new { 
            id, title, name, kommentar, 
            schreibSchlüssel = id, 
            weitereKommentareErlauben = true,
            maximalKomentarlänge = 50,
            übergeordneterKommentarId = übergeordnet
        };
    }

    public void LogKommentar(int i = 0) {
        var time = $"{new String(' ', i)}{Erstellt.ToLocalTime():dd.MM.yyyy HH:mm:ss}";
        Console.WriteLine($"{time,-30} - [{Name,-7}]: {Kommentar}");

        i += 2;

        foreach (var unterKom in WeitereKommentare) {
            unterKom.LogKommentar(i);
        }
    }
}
