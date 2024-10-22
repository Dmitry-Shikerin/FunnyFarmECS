using Agava.WebUtility;
using Agava.YandexGames;
using Sources.Domain.Models.Constants;
using Sources.Frameworks.UiFramework.Core.Services.Localizations.Interfaces;
using Sources.Frameworks.UiFramework.Texts.Services;
using Sources.Frameworks.UiFramework.Texts.Services.Localizations.Configs;
using Sources.Frameworks.UiFramework.Views.Presentations.Implementation.Types;
using UnityEngine;

namespace Sources.Frameworks.UiFramework.Core.Services.Localizations.Implementation
{
    public class LocalizationService : ILocalizationService
    {
        public void Translate()
        {
            string key = WebApplication.IsRunningOnWebGL 
                ? YandexGamesSdk.Environment.i18n.lang 
                : GetEditorKey();
            
            LocalizationBrain.Translate(key);
        }

        public string GetText(string key) =>
            LocalizationBrain.GetText(key);

        public Sprite GetSprite(string key) =>
            LocalizationBrain.GetSprite(key);
        
        private string GetEditorKey()
        {
            return LocalizationDataBase.Instance.Localization switch
            {
                Localization.English => LocalizationConst.English,
                Localization.Turkish => LocalizationConst.Turkish,
                Localization.Russian => LocalizationConst.Russian,
                _ => LocalizationConst.English,
            };
        }
    }
}