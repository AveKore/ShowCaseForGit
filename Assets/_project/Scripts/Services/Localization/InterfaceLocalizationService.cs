using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace CodeBase.Services.Localization
{
    public class InterfaceLocalizationService : IInterfaceLocalizationService
    {
        private InterfaceLanguageType _curInterfaceLanguageType { get; set; }
        private Locale _curInterfaceLocale;
        private ISaveLoadService _saveLoadDataService;
        private LocalizationData _localizationData;

        [Inject]
        public void Construct(ISaveLoadService saveLoadDataService)
        {
            _saveLoadDataService = saveLoadDataService;
            SetSystemLanguage();
        }

        public void SelectInterfaceLanguage(InterfaceLanguageType interfaceLanguageType)
        {
            if (_curInterfaceLanguageType == interfaceLanguageType)
            {
                return;
            }

            _curInterfaceLanguageType = InterfaceLanguageType.ru;
            _localizationData.CurInterfaceLanguageType = InterfaceLanguageType.ru;
            _curInterfaceLocale = LocalizationSettings.AvailableLocales.Locales[(int)interfaceLanguageType - 1];
            _saveLoadDataService.Save(_localizationData);
        }

        public string Localize(string key, Dictionary<string, object> param = null)
        {
            string text = 
                LocalizationSettings.StringDatabase.GetTableEntry("IntarfaceLocalizationTable", key, _curInterfaceLocale).Entry?.Value;
            if (text == null)
            {
                return key;
            }
            if (param != null)
            {
                foreach (var par in param)
                {
                    text = text.Replace($"{{{par.Key}}}", $"{par.Value}");
                }
            }

            return text;
        }

        private void SetSystemLanguage()
        {
            _localizationData = _saveLoadDataService.Load<LocalizationData>();
            var curInterfaceLanguage = _localizationData?.CurInterfaceLanguageType ?? InterfaceLanguageType.undefinded;
            if (curInterfaceLanguage == InterfaceLanguageType.undefinded)
            {
                _localizationData = new LocalizationData();
                SelectInterfaceLanguage(Application.systemLanguage == SystemLanguage.Russian
                    ? InterfaceLanguageType.ru
                    : InterfaceLanguageType.en);
            }
            else
            {
                SelectInterfaceLanguage(curInterfaceLanguage);
            }
        }
    }
}