using System.Text;
namespace WSI.WortFilter;

public abstract class WortFilterService<T1, T2> : IWortFilterService<T1, T2> 
        where T1 : IWortAustauschService
        where T2 : IWortSucheService {
    private readonly T1 _wortAustauscher;
    private readonly T2 _wortSucher;

    public abstract IEnumerable<string> WortVorrat { get; }

    protected WortFilterService(T1 wortAustauscher, T2 wortSucher) {
        _wortAustauscher = wortAustauscher;
        _wortSucher = wortSucher;
    }

    public bool EnthaltetWörter(string satz) {
        return _wortSucher.FindeVonBisLänge(new StringBuilder(satz), WortVorrat).Any();
    }

    public string FilterWörter(string satz) {
        var ret = new StringBuilder(satz);

        foreach (var (von, bis, länge, wort) in _wortSucher.FindeVonBisLänge(ret, WortVorrat)) {
            var neu = _wortAustauscher.TauscheWort(wort);

            ret.Remove(von, länge);
            ret.Insert(von, neu);
        }

        return ret.ToString();
    }
}
