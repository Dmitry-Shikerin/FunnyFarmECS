using UnityEngine;

namespace Sources.Frameworks.MyLocalization.Infrastructure.Services.Interfaces
{
    public interface ILocalizationService
    {
        void Translate();
        string GetText(string key);
        Sprite GetSprite(string key);
    }
}