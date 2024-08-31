﻿// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using System.Linq;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Utils;
using Doozy.Editor.Reactor.Ticker;
using Doozy.Editor.UIManager.Editors.Animators.Internal;
using Doozy.Runtime.UIElements.Extensions;
using Doozy.Runtime.UIManager.Animators;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Doozy.Editor.UIManager.Editors.Animators
{
    [CustomEditor(typeof(UIToggleFloatAnimator))]
    [CanEditMultipleObjects]
    public class UIToggleFloatAnimatorEditor : BaseUIToggleAnimatorEditor
    {
        public UIToggleFloatAnimator castedTarget => (UIToggleFloatAnimator) target;
        public List<UIToggleFloatAnimator> castedTargets => targets.Cast<UIToggleFloatAnimator>().ToList();
        
        private FluidField valueTargetFluidField { get; set; }
        private SerializedProperty propertyValueTarget { get; set; }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            valueTargetFluidField?.Recycle();
        }

        protected override void ResetAnimatorInitializedState()
        {
            foreach (var a in castedTargets)
                a.animatorInitialized = false;
        }

        protected override void ResetToStartValues()
        {
            foreach (var a in castedTargets)
            {
                a.StopAllReactions();
                a.ResetToStartValues();
            }
        }
        
        protected override void PlayIsOn()
        {
            foreach (var a in castedTargets)
                a.PlayOnAnimation();
        }
        
        protected override void PlayIsOff()
        {
            foreach (var a in castedTargets)
                a.PlayOffAnimation();
        }
        
         protected override void HeartbeatCheck()
        {
            if (Application.isPlaying) return;
            foreach (var a in castedTargets)
                if (a.animatorInitialized == false)
                {
                    a.InitializeAnimator();
                    foreach (EditorHeartbeat eh in a.SetHeartbeat<EditorHeartbeat>().Cast<EditorHeartbeat>())
                        eh.StartSceneViewRefresh(a);
                }
        }

        protected override void FindProperties()
        {
            base.FindProperties();

            propertyValueTarget = serializedObject.FindProperty(nameof(UIToggleFloatAnimator.ValueTarget));
        }

        protected override void InitializeEditor()
        {
            base.InitializeEditor();

            componentHeader
                .SetComponentTypeText("Float Animator")
                .SetIcon(EditorSpriteSheets.Reactor.Icons.FloatAnimator)
                .AddManualButton()
                .AddApiButton()
                .AddYouTubeButton();
            
            valueTargetFluidField =
                FluidField.Get<PropertyField>(propertyValueTarget)
                    .SetIcon(EditorSpriteSheets.EditorUI.Icons.Atom)
                    .SetLabelText("Value Target");
            
            //refresh tabs enabled indicator
            root.schedule.Execute(() =>
            {
                void UpdateIndicator(FluidTab tab, bool toggleOn, bool animateChange)
                {
                    if (tab == null) return;
                    if (tab.indicator.isOn != toggleOn)
                        tab.indicator.Toggle(toggleOn, animateChange);
                }

                bool ShowEnabled() => castedTarget.onAnimation.animation.enabled;
                bool HideEnabled() => castedTarget.offAnimation.animation.enabled;
                
                //initial indicators state update (no animation)
                UpdateIndicator(onTab, ShowEnabled(), false);
                UpdateIndicator(offTab, HideEnabled(), false);
                
                //subsequent indicators state update (with animation)
                root.schedule.Execute(() =>
                {
                    UpdateIndicator(onTab, ShowEnabled(), true);
                    UpdateIndicator(offTab, HideEnabled(), true);
                    
                }).Every(200);
            });
        }

        protected override void Compose()
        {
            root
                .AddChild(reactionControls)
                .AddChild(componentHeader)
                .AddChild(Toolbar())
                .AddSpaceBlock(2)
                .AddChild(Content())
                .AddSpaceBlock(2)
                .AddChild(valueTargetFluidField)
                .AddSpaceBlock()
                .AddChild(GetController(propertyController))
                .AddEndOfLineSpace();
        }
    }
}
