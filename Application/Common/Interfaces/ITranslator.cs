namespace Application.Common.Interfaces;

public interface ITranslator
{
    string GetString(string name);
    string GetString(string name, params object[] arguments);
}
