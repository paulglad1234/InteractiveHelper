﻿namespace InteractiveHelper.Settings;

public interface IApiSettings
{
    IGeneralSettings General { get; }
    IDbSettings Db { get; }
    IIdentityServerConnectSettings IdentityServer { get; }
}

