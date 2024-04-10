namespace Application.Common.Interfaces;

public interface ITranslator
{
    public string GetString(string name);
    public string GetString(string name, params object[] arguments);
}
