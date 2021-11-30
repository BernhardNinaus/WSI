using WSI.ConsoleTest;

var client = new HttpClient() {
    BaseAddress = new Uri("http://localhost:5197")
};

var top = Guid.NewGuid(); // 3 UUIDs erstellen damit diese dann auch Referenziert werden
var mid = Guid.NewGuid();
var bot = Guid.NewGuid();

KommentarModel kommentar;

// Zuerst die drei Kommentare erstellen und ausgeben was erstellt wurde
kommentar = await KommentarModel.Neu(client, top, "Titel", "Thomas", "Hallo, wie gehts?");
Console.WriteLine($"Neu erstellt Top:\n{kommentar}\n");

kommentar = await KommentarModel.Neu(client, mid, "", "Th", "Hab beim Kahoot alles richtig", top);
Console.WriteLine($"Neu erstellt Mid:\n{kommentar}\n");

kommentar = await KommentarModel.Neu(client, bot, "", "Thomas", "Sehr gut!", mid);
Console.WriteLine($"Neu erstellt Bot:\n{kommentar}\n");

// Oberstes Kommentar ausgeben, es sind weitere Kommentare vorhanden,
// diese werden jetzt auch ausgegeben.
kommentar = await KommentarModel.GetById(client, top);
Console.WriteLine($"Alle:\n{kommentar}\n");

// Löschen des Mittleren Kommentars und anzeigen, dass es funtkioniert hat
Console.WriteLine("Lösche mittleres Kommentar\n");
await KommentarModel.DeleteById(client, mid);
Console.WriteLine($"Alle:\n{await KommentarModel.GetById(client, top)}\n");

// Weitere Kommentare und Top hinzufügen damit auch gesehen werden kann, 
// dass auch mehrere Kommentare zu einem Kommentar gespeichert werden können.
await KommentarModel.Neu(client, Guid.NewGuid(), "", "Th", "Noch eins", top);
await KommentarModel.Neu(client, Guid.NewGuid(), "", "Th", "Noch zwei", top);
await KommentarModel.Neu(client, Guid.NewGuid(), "", "Th", "Böses Wort: '1te Wort'", top);

kommentar = await KommentarModel.GetById(client, top);
Console.WriteLine($"Weite Kommentare in der Mitte:\n{kommentar}\n");
