using Microsoft.Extensions.DependencyInjection;

namespace WSI.WortFilter;

public class WortSucheServiceCollection {
    private readonly IServiceCollection _services;

    public WortSucheServiceCollection(IServiceCollection services) {
        _services = services;

        #region Wort Austausche
        _services.AddSingleton<SternchenWortAustauschService>();
        _services.AddSingleton<LeerWortAustauschService>();
        #endregion

        #region Wort Sucher
        services.AddSingleton<EinfacheWortSucheService>(provider 
            => new EinfacheWortSucheService(StringComparison.CurrentCultureIgnoreCase));
        #endregion
    }

    public WortSucheServiceCollection AddDefautlWortAustasuch() {
        _services.AddSingleton<IWortAustauschService>(provider 
            => provider.GetRequiredService<SternchenWortAustauschService>());

        return this;
    }

    public WortSucheServiceCollection AddDefaultWortSuche() {
        _services.AddSingleton<IWortSucheService>(provider 
            => provider.GetRequiredService<EinfacheWortSucheService>());

        return this;
    }

    public WortSucheServiceCollection AddEinfacherWortFilter<T1, T2>(string[] worte)
            where T1 : IWortAustauschService
            where T2 : IWortSucheService {
        _services.AddSingleton<IWortFilterService>(provider 
            => new EinfacherWortFilterService(
                worte,
                provider.GetRequiredService<T1>(),
                provider.GetRequiredService<T2>()));

        return this;
    }

    public WortSucheServiceCollection AddEinfacherWortFilter<T>(string[] worte)
            where T : IWortAustauschService
        => AddEinfacherWortFilter<T, IWortSucheService>(worte);

    public WortSucheServiceCollection AddEinfacherWortFilter(string[] worte)
        => AddEinfacherWortFilter<IWortAustauschService, IWortSucheService>(worte);
}

public static class ExtensionIServiceCollection {
    public static WortSucheServiceCollection AddWortFilter(this IServiceCollection services)
        => new WortSucheServiceCollection(services);
}
