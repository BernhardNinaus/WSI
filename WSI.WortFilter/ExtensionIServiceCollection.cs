using Microsoft.Extensions.DependencyInjection;
namespace WSI.WortFilter;

public static class ExtensionIServiceCollection {
    public static IServiceCollection AddWortFilterBasisc(this IServiceCollection services) {
        #region Wort Austauscher
        services.AddSingleton<SternchenWortAustauschService>();

        services.AddSingleton<IWortAustauschService>(provider 
            => provider.GetRequiredService<SternchenWortAustauschService>());

        services.AddSingleton<LeerWortAustauschService>();
        #endregion


        #region Wort Sucher
        services.AddSingleton<EinfacheWortSucheService>(provider 
            => new EinfacheWortSucheService(StringComparison.CurrentCultureIgnoreCase));

        services.AddSingleton<IWortSucheService>(provider 
            => provider.GetRequiredService<EinfacheWortSucheService>());
        #endregion

        return services;
    }
}
