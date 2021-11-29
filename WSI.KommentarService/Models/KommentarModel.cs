using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WSI.KommentarService.Models;

public class KommentarModel {
    [Key]
    public Guid Id { get; set; } = new Guid();

    public string? Title { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Kommentar { get; set; } = string.Empty;

    public DateTime Erstellt { get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime? Bearbeitet { get; set; } = null;
    public DateTime LetzteÄnderung { get; set; } = DateTime.Now.ToUniversalTime();

    public bool Gelöscht { get; set; } = false;

    public Guid? KopfId { get; set; }
    [JsonIgnore]
    public virtual KommentarModel? KopfKommentar { get; set; }
    [JsonIgnore]
    public List<KommentarModel>? AlleKommentare { get; set; }


    public Guid? ÜbergeordneterKommentarId { get; set; }
    [JsonIgnore]
    public virtual KommentarModel? ÜbergeordneterKommentar { get; set; }
    public List<KommentarModel>? WeitereKommentare { get; set; }


    [JsonIgnore]
    public Guid SchreibSchlüssel { get; set; } = new Guid();

    public void Update(KommentarPUTModel neu) {
        this.Kommentar = neu.Kommentar;
        this.Bearbeitet = DateTime.Now;
        this.LetzteÄnderung = DateTime.Now;
    }
}
