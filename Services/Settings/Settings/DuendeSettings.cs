namespace InteractiveHelper.Settings;

public class DuendeSettings : IDuendeSettings
{
    private readonly ISettingsSource source;
    private readonly IDbSettings dbSettings;

    public DuendeSettings(ISettingsSource source) => this.source = source;

    public DuendeSettings(ISettingsSource source, IDbSettings dbSettings)
    {
        this.source = source;
        this.dbSettings = dbSettings;
    }

    public IDbSettings Db => dbSettings ?? new DbSettings(source);
}
