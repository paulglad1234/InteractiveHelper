﻿namespace InteractiveHelper.Settings;

public interface IRabbitMqSettings
{
    string Uri { get; }
    string UserName { get; }
    string Password { get; }    
}
