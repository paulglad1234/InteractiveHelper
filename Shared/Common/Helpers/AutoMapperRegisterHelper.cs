using Microsoft.Extensions.DependencyInjection;

namespace InteractiveHelper.Common.Helpers;

public static class AutoMapperRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.StartsWith("InteractiveHelper."));

        services.AddAutoMapper(assemblies);
    }
}
