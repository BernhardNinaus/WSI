namespace WSI.WortFilter;

public class SternchenWortAustauschService : IWortAustauschService {
    public string TauscheWort(string zuTauschen) => new String('*', zuTauschen.Length);
}

public class LeerWortAustauschService : IWortAustauschService {
    public string TauscheWort(string zuTauschen) => string.Empty;
}
