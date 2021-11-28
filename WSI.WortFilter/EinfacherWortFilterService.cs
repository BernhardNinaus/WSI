using System.Text;
namespace WSI.WortFilter;

public class EinfacherWortFilterService
        : WortFilterService<IWortAustauschService, IWortSucheService> {
    private readonly string[] _wortVorrat;

    public EinfacherWortFilterService(
            string[] wortVorrat, 
            IWortAustauschService wortAustauscher, 
            IWortSucheService wortSucher) : base(wortAustauscher, wortSucher) {
        _wortVorrat = wortVorrat;
    }

    public override IEnumerable<string> WortVorrat { get => _wortVorrat; }
}
