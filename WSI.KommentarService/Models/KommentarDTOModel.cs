using System.ComponentModel.DataAnnotations;

namespace WSI.KommentarService.Models;

public class KommentarDELETEModel {
    public Guid Id { get; set; }
    public Guid SchreibSchlüssel { get; set; }
}

public class KommentarPUTModel : KommentarDELETEModel {
    public string Kommentar { get; set; } = string.Empty;
}

public class KommentarPOSTModel {
    public Guid? Id { get; set; } = new Guid();

    public string? Title { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Kommentar { get; set; } = string.Empty;

    public Guid? ÜbergeordneterKommentarId { get; set; }
    public Guid SchreibSchlüssel { get; set; }

    public KommentarModel ZuKommentar() {
        var ret = new KommentarModel();
        
        if (Id != null) ret.Id = (Guid)Id;

        ret.Title = Title;
        ret.Name = Name;
        ret.Kommentar = Kommentar;
        ret.ÜbergeordneterKommentarId = ÜbergeordneterKommentarId;
        ret.SchreibSchlüssel = SchreibSchlüssel;

        return ret;
    }
}
