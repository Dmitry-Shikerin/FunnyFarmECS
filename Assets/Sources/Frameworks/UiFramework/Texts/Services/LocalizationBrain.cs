using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Models.Constants;
using Sources.Frameworks.GameServices.Singletones.Monobehaviours;
using Sources.Frameworks.UiFramework.Texts.Presentations.Views.Implementation;
using Sources.Frameworks.UiFramework.Texts.Services.Localizations.Configs;
using UnityEngine;

namespace Sources.Frameworks.UiFramework.Texts.Services
{
    public class LocalizationBrain : MonoBehaviourSingleton<LocalizationBrain>
    {
        private readonly List<UiLocalizationText> _texts = new ();
        private readonly List<UiLocalizationSprite> _sprites = new ();
        private IReadOnlyDictionary<string, Dictionary<string, string>> _textsDictionary;
        private IReadOnlyDictionary<string, Dictionary<string, Sprite>> _spritesDictionary;
        private IReadOnlyDictionary<string, string> _currentLanguageTextDictionary;
        private IReadOnlyDictionary<string, Sprite> _currentLanguageSpriteDictionary;
        private LocalizationDataBase _dataBase;

        private void Awake()
        {
            _dataBase = LocalizationDataBase.Instance;
            
            _textsDictionary = new Dictionary<string, Dictionary<string, string>>()
            {
                [LocalizationConst.Russian] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.Russian),
                [LocalizationConst.English] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.English),
                [LocalizationConst.Turkish] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.Turkish),
            };
            _spritesDictionary = new Dictionary<string, Dictionary<string, Sprite>>()
            {
                [LocalizationConst.Russian] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.RussianSprite),
                [LocalizationConst.English] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.EnglishSprite),
                [LocalizationConst.Turkish] = _dataBase.Phrases.ToDictionary(phrase => phrase.LocalizationId, phrase => phrase.TurkishSprite),
            };
        }
        
        public static void Add(UiLocalizationSprite sprite) => 
            Instance._sprites.Add(sprite);
        
        public static void Add(UiLocalizationText text) => 
            Instance._texts.Add(text);
        
        public static string GetText(string key)
        {
            if(Instance._currentLanguageTextDictionary.ContainsKey(key) == false)
                throw new KeyNotFoundException(nameof(key));
            
            return Instance._currentLanguageTextDictionary[key];
        }

        public static Sprite GetSprite(string key)
        {
            if(Instance._currentLanguageSpriteDictionary.ContainsKey(key) == false)
                throw new KeyNotFoundException(nameof(key));
            
            return Instance._currentLanguageSpriteDictionary[key];
        }
        
        public static void Translate(string key)
        {
            Instance._currentLanguageTextDictionary = Instance._textsDictionary[key];
            Instance._currentLanguageSpriteDictionary = Instance._spritesDictionary[key];

            foreach (UiLocalizationText textView in Instance._texts)
            {
                if (string.IsNullOrWhiteSpace(textView.Id))
                    Debug.Log($"LocalizationService: {textView.gameObject.name} has empty id");

                if (Instance._currentLanguageTextDictionary.ContainsKey(textView.Id) == false)
                {
                    Debug.Log($"LocalizationService: {textView.Id} not found in LocalizationData");
                    
                    continue;
                }

                textView.SetText(Instance._currentLanguageTextDictionary[textView.Id]);
            }

            foreach (UiLocalizationSprite localizationSprite in Instance._sprites)
            {
                if (Instance._currentLanguageSpriteDictionary.ContainsKey(localizationSprite.Id) == false)
                {
                    Debug.Log($"LocalizationService: {localizationSprite.Id} not found in LocalizationData");
                    
                    continue;
                }
                
                localizationSprite.SetSprite(Instance._currentLanguageSpriteDictionary[localizationSprite.Id]);
            }
            
            Debug.Log($"Translated: {key}, texts: {Instance._texts.Count}, sprites: {Instance._sprites.Count}");
        }
    }
}