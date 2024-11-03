using System.Collections.Generic;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Components.Internal;
using Doozy.Editor.EditorUI.ScriptableObjects.Colors;
using Doozy.Runtime.UIElements.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Frameworks.MyAudio_master.MyAudio.Soundy.Editor.Presentation.Controlls
{
    public class SoundyDataBaseWindowLayout : FluidWindowLayout
    {
        public List<FluidToggleButtonTab> FluidToggleButtonTabs { get; }
        private List<SoundGroupVisualElement> _soundGroups;
        public Dictionary<string, FluidToggleButtonTab> DatabaseButtons { get; }
        
        public SoundyDataBaseWindowLayout()
        {
            _soundGroups = new List<SoundGroupVisualElement>();
            DatabaseButtons = new Dictionary<string, FluidToggleButtonTab>();
            
            AddHeader("Soundy Database", "Sound Groups", animatedIconTextures);
            sideMenu.RemoveSearch();
            
            SettingsButton = new FluidButton()
                .SetElementSize(ElementSize.Normal)
                .SetLabelText("Settings")
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Settings)
                // .SetStyleBackgroundColor(EditorColors.Default.BoxBackground)
                .SetAccentColor(EditorSelectableColors.EditorUI.Orange)
                .SetStyleBorderRadius(4,4,4,4);
            
            RefreshButton = new FluidButton()
                .SetElementSize(ElementSize.Normal)
                .SetLabelText("Refresh")
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Refresh)
                // .SetStyleBackgroundColor(EditorColors.Default.BoxBackground)
                .SetAccentColor(EditorSelectableColors.EditorUI.Orange)
                .SetStyleBorderRadius(4,4,4,4);
            
            NewDataBaseButton = new FluidButton()
                .SetElementSize(ElementSize.Normal)
                .SetLabelText("New Database")
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Plus)
                .SetAccentColor(EditorSelectableColors.Default.Add)
                // .SetStyleBackgroundColor(EditorColors.Default.BoxBackground)
                .SetStyleBorderRadius(4,4,4,4);

            sideMenu.buttonsScrollViewContainer
                .AddSpace(5)
                .AddChild(SettingsButton)
                .AddSpace(5)
                .AddChild(RefreshButton)
                .AddSpace(4)
                .AddChild(NewDataBaseButton)
                .AddSpace(20);
        }

        public override string layoutName => "Sound Databases";
        public override List<Texture2D> animatedIconTextures => EditorSpriteSheets.EditorUI.Components.LineMixedValues;
        public override Color accentColor => EditorColors.Default.UIComponent;
        public override EditorSelectableColorInfo selectableAccentColor => 
            EditorSelectableColors.Default.UIComponent;
        public FluidButton SettingsButton { get; }
        public FluidButton RefreshButton { get; }
        public FluidButton NewDataBaseButton { get; }
        
        public void RefreshDataBasesButtons()
        {
            foreach (FluidToggleButtonTab button in DatabaseButtons.Values)
                button.Recycle();

            DatabaseButtons.Clear();
        }
        
        public void ClickDataBaseButton(string buttonName)
        {
            DatabaseButtons[buttonName].isOn = true;
            DatabaseButtons[buttonName].OnClick.Invoke();
            Debug.Log($"Click {buttonName}");
        }

        public void AddDataBaseButton(string buttonName, UnityAction callback)
        {
            FluidToggleButtonTab button =
                sideMenu
                    .AddButton(buttonName, EditorSelectableColors.EditorUI.Orange)
                    .SetElementSize(ElementSize.Normal)
                    .SetIcon(EditorSpriteSheets.EditorUI.Icons.ToggleON)
                    .AddOnClick(() => callback?.Invoke());
            
            DatabaseButtons[buttonName] = button;
        }
    }
}