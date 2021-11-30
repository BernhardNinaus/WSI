using System.Text;

namespace WSI.WortFilter;

public interface IWortSucheService {
    IEnumerable<Tuple<int, int, int, string>> FindeVonBisLänge(StringBuilder satz, IEnumerable<string> worte);
}
