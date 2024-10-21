using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Extencions;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Propertyes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using SerializedProperty = UnityEditor.SerializedProperty;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor
{
    [CustomEditor(typeof(EntityView))]
    public class EcsComponentContainerEditor : UnityEditor.Editor
    {
        private const string ComponentsField = "_components";

        private const string _ADD_BTN_TEXT = "Add";

        private VisualElement _root;
        private VisualElement _header;
        private Label _title;
        
        private VisualElement _addContainer;
        private Label _addContainerTitle;
        private DropdownField _addContainerDropdown;
        private Button _addBtn;
        private Type _selectedType;

        private Button _delBtn;
        private VisualElement _componentsContainer;
        private PropertyField[] _componentsDrawers = Array.Empty<PropertyField>();
        private Button _refreshBtn;

        private EntityView EntityView { get; set; }

        private int ComponentsCount => EntityView.Components.Count;
        private SerializedProperty ComponentsProperty { get; set; }
        private SerializedProperty ComponentsCountProperty { get; set; }
        
        public override VisualElement CreateInspectorGUI()
        {
            CreateElements();
            StructureElements();
            InitElements();
            return _root;
        }
        
        private void CreateElements()
        {
            ComponentsProperty = serializedObject.FindProperty(ComponentsField);
            ComponentsCountProperty = serializedObject.FindProperty("_componentsCount");
            EntityView = (EntityView)serializedObject.targetObject;
            
            _root = new VisualElement();
            _header = new Box();
            _addContainer = new Box();
            _title = new Label();
            _addBtn = new Button(() =>
            {
                AddComponent();
                // RefreshComponents();
            }) { text = _ADD_BTN_TEXT };
            _delBtn = new Button(ClearComponents) { text = "Clear Components" };
            _refreshBtn = new Button(RefreshComponents) { text = "Refresh" };
            _componentsContainer = new VisualElement();
            _addContainerTitle = new Label();
            _addContainerDropdown = new DropdownField(
                GetComponentsNames(),
                string.Empty,
                SetComponentType);
        }

        private void AddComponent()
        {
            List<Type> types = EntityView.Components.Select(component => component.Component.GetType()).ToList();
            
            if (types.Any(type => type == _selectedType))
                return;
            
            if (_selectedType != null)
            {
                object component = Activator.CreateInstance(_selectedType);
                ComponentView componentView = new ComponentView(EntityView);
                componentView.Init(_selectedType);
                componentView.Component = component;
                EntityView.Add(componentView);
            }
        }

        private void ClearComponents()
        {
            EntityView.Clear();
        }

        private void StructureElements()
        {
            _header
                .AddChild(_title)
                .AddChild(_refreshBtn)
                .AddChild(_delBtn);
            _addContainer
                .AddChild(_addContainerTitle)
                .AddChild(_addContainerDropdown)
                .AddChild(_addBtn);
            _root
                .AddChild(_header)
                .AddChild(_addContainer)
                .AddChild(_componentsContainer);
        }

        private void InitElements()
        {
            _root
                .style
                .marginLeft = -StyleConsts.REM;
            _header
                .style
                .Margin(hor: 0, StyleConsts.REM_025)
                .Padding(StyleConsts.REM_05)
                .FlexRow()
                .BorderRadius(StyleConsts.REM_05)
                .FontStyle(FontStyle.Bold)
                .FontSize(StyleConsts.REM_125)
                .OverflowHidden();
            _addContainer                
                .style
                .Margin(hor: 0, StyleConsts.REM_025)
                .Padding(StyleConsts.REM_05)
                .FlexRow()
                .BorderRadius(StyleConsts.REM_05)
                .FontStyle(FontStyle.Bold)
                .FontSize(StyleConsts.REM_125)
                .OverflowHidden();
            _title
                .SetText("Entity View")
                .style
                .FlexGrow();           
            _addContainerTitle
                .SetText("Add Component")
                .style
                .FlexGrow();

            // _addBtn.style.width = _delBtn.style.width = StyleConsts.REM * 5;
            _root.TrackPropertyValue(ComponentsCountProperty, _ =>
            {
                RefreshComponents();
                serializedObject.Update();
            });
            
            RefreshComponents();
            serializedObject.Update();
        }

        private void MyRefreshComponents()
        {
            IList<SerializedProperty> properties =
                ComponentsProperty.GetChildren(ComponentsCount + 1); // "0" - "Size" property => shift by 1

            _componentsContainer.Clear();

            foreach (SerializedProperty property in properties)
            {
                _componentsContainer.AddChild(new PropertyField(property));
            }
        }
        
        private void RefreshComponents()
        {
            IList<SerializedProperty> properties =
                    ComponentsProperty.GetChildren(ComponentsCount + 1); // "0" - "Size" property => shift by 1
            
            if (ComponentsCount > _componentsDrawers.Length)
                Array.Resize(ref _componentsDrawers, ComponentsCount);

            for (var i = 0; i < ComponentsCount; i++)
            {
                SerializedProperty component = properties[i + 1]; // "0" - "Size" property => shift by 1
                PropertyField drawer = GetField(i, component);

                if (component.propertyPath == drawer.bindingPath)
                    continue;

                drawer.BindProperty(component);
                drawer.RegisterValueChangeCallback(_ => 
                    ((ComponentView)component
                    .GetUnderlyingValue())
                    .SetValue());
            }

            for (int i = ComponentsCount; i < _componentsDrawers.Length; i++)
                _componentsDrawers[i].style.display = DisplayStyle.None;
        }

        private PropertyField GetField(int i, SerializedProperty component)
        {
            if (_componentsDrawers[i] != null)
                return _componentsDrawers[i];

            PropertyField newField = new PropertyField();
            Box box = new Box();
            box
                .style
                .Margin(hor: 0, StyleConsts.REM_025)
                .Padding(StyleConsts.REM_05)
                .BorderRadius(StyleConsts.REM_05);
            Button delBtn = new Button(() =>
            {
                box.RemoveFromHierarchy();
                var componentView = (ComponentView)component.GetUnderlyingValue();
                componentView.EntityView.Remove(componentView);
            });
            delBtn.SetText("-");
            delBtn.style.minHeight = StyleConsts.REM * 1;
            box
                .AddChild(newField)
                .AddChild(delBtn);
            // _componentsContainer.AddChild(newField);
            _componentsContainer.AddChild(box);
            _componentsDrawers[i] = newField;

            return newField;
        }

        private List<string> GetComponentsNames()
        {
            List<string> types = new List<string>();
        
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IComponent).IsAssignableFrom(type) && type.IsInterface == false &&
                        type.IsAbstract == false)
                    {
                        types.Add(type.Name);
                    }
                }
            }
        
            types.Add(string.Empty);
        
            return types;
        }
        
        private string SetComponentType(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IComponent).IsAssignableFrom(type) && type.IsInterface == false &&
                        type.IsAbstract == false)
                    {
                        if (type.Name == name)
                        {
                            _selectedType = type;
                        }
                    }
                }
            }
        
            return name;
        }
    }
}