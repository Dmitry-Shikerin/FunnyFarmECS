using Agava.WebUtility;
using Agava.YandexGames;
using Sources.Frameworks.MyLocalization.Domain.Constant;
using Sources.Frameworks.MyLocalization.Domain.Data;
using Sources.Frameworks.MyLocalization.Domain.Data.Types;
using Sources.Frameworks.MyLocalization.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.MyLocalization.Infrastructure.Services.Implementation
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