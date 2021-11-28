using System.Text;

namespace WSI.WortFilter;

public interface IWortSucheService {
    /// <summary>
    /// Finden das 
    /// </summary>
    /// <param name="satz">Satz in dem gesucht wird.</param>
    /// <param name="worte">Das zu findente wort.</param>
    /// <returns>Startposition, Endposition und Länge der gefundenen Wortes</returns>
    IEnumerable<Tuple<int, int, int, string>> FindeVonBisLänge(StringBuilder satz, IEnumerable<string> worte);
}
