namespace InteractiveHelper.CharactetisricService;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddCharacteristicService(this IServiceCollection services)
    {
        services.AddSingleton<ICharacteristicService, CharacteristicService>();

        return services;
    }
}
