using System;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Extencions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Propertyes
{
    [CustomPropertyDrawer(typeof(ComponentView), useForChildren: true)]
    public class ComponentDrawer : PropertyDrawer
    { 
       private const string ComponentNameField = nameof(ComponentView.componentName); 
       private const string ComponentField = nameof(ComponentView.Component);

      private Box _root;
      private VisualElement _header;
      // private Button _delButton;
      private Label _label;
      private VisualElement _main;
      private VisualElement _fields;

      private SerializedProperty _property;
      private SerializedProperty ComponentProperty { get; set; }
      private string ComponentName { get; set; }
      private SerializedProperty ComponentNameProperty { get; set; }
      private ComponentView _target;
      private object Component => _target.Component;
      private Type ComponentType => _target.ComponentType;
      
      public override VisualElement CreatePropertyGUI(SerializedProperty property) 
      {
         _property = property;
         _target = (ComponentView)property.GetUnderlyingValue();
         ComponentProperty = _property.FindPropertyRelative(ComponentField);
         ComponentNameProperty = _property.FindPropertyRelative(ComponentNameField);
         ComponentName = ComponentNameProperty.stringValue;
         
         // Debug.Log(Component.GetType().Name);
         //
         CreateElements();
         StructureElements();
         InitElements();
         
         return _root;
      }

      private void CreateElements() 
      {
         _root = new Box();
         _header = new VisualElement();
         // _delButton = new Button() { text = "-" };
         _label = new Label();
         _main = new VisualElement();
         _fields = new VisualElement();
      }

      private void StructureElements()
      {
         // _delButton.clicked -= OnClick;
         // _delButton.clicked += OnClick;
         _header
            .AddChild(_label);
            // .AddChild(_delButton);
         _fields
            .AddChildPropertiesOf(ComponentProperty);
         _main
            .AddChild(_fields);
         _root
           .AddChild(_header)
           .AddChild(_main);
      }

      private void OnClick()
      {
         _target.EntityView.Remove(_target);
         _property.Dispose();
      }

      private void InitElements() {
         _root
           .style
           .Margin(hor: 0, StyleConsts.REM_025)
           .Padding(StyleConsts.REM_05)
           .BorderRadius(StyleConsts.REM_05);

         _label
           .SetText(ComponentName)
           .style
           .FontStyle(FontStyle.Bold);

         _main.style.paddingLeft = StyleConsts.REM;
      }
    }
}