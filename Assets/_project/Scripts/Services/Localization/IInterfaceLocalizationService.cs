using System.Collections.Generic;

namespace CodeBase.Services.Localization
{
    public interface IInterfaceLocalizationService
    {
        public void SelectInterfaceLanguage(InterfaceLanguageType interfaceLanguageType);
        public string Localize(string key, Dictionary<string, object> param = null);
    }
}