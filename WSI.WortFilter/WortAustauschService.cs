using System.Data;
using System.Diagnostics.Contracts;
namespace WSI.WortFilter;

public class EinfacherWortAustaschService : IWortAustauschService {
    private readonly char _ziechen;
    public EinfacherWortAustaschService(char ziechen) {
        _ziechen = ziechen;
    }
    
    public string TauscheWort(string zuTauschen) => new String(_ziechen, zuTauschen.Length);
}

public class SternchenWortAustauschService : EinfacherWortAustaschService {
    public SternchenWortAustauschService() : base('*') { }
}

public class LeerWortAustauschService : IWortAustauschService {
    public string TauscheWort(string zuTauschen) => string.Empty;
}
