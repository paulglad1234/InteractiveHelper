namespace InteractiveHelper.QuizFront;

public interface IConfigurationService
{
    Task<bool> GetDarkMode();
    Task SetDarkMode(bool value);
}
