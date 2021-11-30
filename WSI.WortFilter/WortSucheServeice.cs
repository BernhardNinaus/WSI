using System.Text;

namespace WSI.WortFilter;

public class EinfacheWortSucheService : IWortSucheService {
    private readonly StringComparison _stringComparison;

    public EinfacheWortSucheService(StringComparison stringComparison) {
        _stringComparison = stringComparison;
    }

    public IEnumerable<Tuple<int, int, int, string>> FindeVonBisLänge(StringBuilder satz, IEnumerable<string> worte) {
        foreach (var wort in worte) {
            int intex;
            // Alle Startpositionen des aktuellen Wortes suchen.
            while ((intex = satz.ToString().IndexOf(wort, _stringComparison)) > 0) {
                // Werte Berechnen und Zurückgeben. Von, Bis, Länge
                yield return new (intex, intex + wort.Length, wort.Length, wort);
            }
        }
    }
}
