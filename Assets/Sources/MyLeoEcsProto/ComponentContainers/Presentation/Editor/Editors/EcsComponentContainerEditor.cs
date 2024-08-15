using System;
using System.Collections.Generic;
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
        // private const string _COMPONENTS_COUNT_FIELD = nameof(ComponentContainer.componentsCount);

        private const string _ADD_BTN_TEXT = "Add";
        private const string _DEL_BTN_TEXT = "Del";
        private const string _KILL_BTN_TEXT = "Kill";

        private VisualElement _root;
        private VisualElement _header;
        private Label _title;
        
        private VisualElement _addContainer;
        private Label _addContainerTitle;
        private DropdownField _addContainerDropdown;
        private Button _addBtn;
        private Type _selectedType;

        // private Button          _killBtn;
        private Button _delBtn;
        private VisualElement _componentsContainer;
        private PropertyField[] _componentsDrawers = Array.Empty<PropertyField>();

        private EntityView EntityView { get; set; }

        // private EcsWorld   World           => Target.World;
        // private int        Entity          => Target.Entity;
        // private string Label => Target.BakedIndex;
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
            _componentsContainer = new VisualElement();
            _addContainerTitle = new Label();
            _addContainerDropdown = new DropdownField(
                GetComponentsNames(),
                string.Empty,
                SetComponentType);
        }

        private void AddComponent()
        {
            if (_selectedType != null)
            {
                object component = Activator.CreateInstance(_selectedType);
                ComponentView componentView = new ComponentView(EntityView);
                componentView.Component = component;
                componentView.componentName = component.GetType().Name;
                EntityView.Components.Add(componentView);
                EntityView.ComponentsCount++;
            }
        }

        private void ClearComponents()
        {
            EntityView.Components.Clear();
            EntityView.ComponentsCount = 0;
        }

        private void StructureElements()
        {
            _header
                .AddChild(_title)
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

            // _addBtn.style.width = _delBtn.style.width = _killBtn.style.width = StyleConsts.REM * 5;
            // EntityView.Components.Clear();
            // EntityView.ComponentsCount = 0;
            
            if (EntityView.Components.Count <= 0)
                return;
            
            RefreshComponents();
            _root.TrackPropertyValue(ComponentsCountProperty, _ => RefreshComponents());
        }


        private void RefreshComponents()
        {
            IList<SerializedProperty> properties =
                    ComponentsProperty.GetChildren(ComponentsCount + 1); // "0" - "Size" property => shift by 1
            
            if (NotEnoughDrawers())
                ResizeForNewDrawers();

            for (var i = 0; i < ComponentsCount; i++)
            {
                PropertyField drawer = GetField(i);
                SerializedProperty component = GetProperty(i);

                if (AlreadyBinded(drawer, component))
                    continue;

                drawer.BindProperty(component);
                drawer.RegisterValueChangeCallback(_ => ((ComponentView)component.GetUnderlyingValue()).SetValue());
            }

            for (int i = ComponentsCount; i < _componentsDrawers.Length; i++)
            {
                DisableDrawer(i);
            }

            return;

            PropertyField GetField(int i)
            {
                if (_componentsDrawers[i] != null)
                    return _componentsDrawers[i];

                var newField = new PropertyField();
                _componentsContainer.AddChild(newField);
                _componentsDrawers[i] = newField;

                return newField;
            }

            bool AlreadyBinded(IBindable drawer, SerializedProperty component) =>
                component.propertyPath == drawer.bindingPath;

            SerializedProperty GetProperty(int i) =>
                properties[i + 1]; // "0" - "Size" property => shift by 1
        }
        
        private void DisableDrawer(int i) =>
            _componentsDrawers[i].style.display = DisplayStyle.None;

        private void ResizeForNewDrawers() =>
            Array.Resize(ref _componentsDrawers, ComponentsCount);

        private bool NotEnoughDrawers() =>
            ComponentsCount > _componentsDrawers.Length;
        
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
            Type componentType = null;
        
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IComponent).IsAssignableFrom(type) && type.IsInterface == false &&
                        type.IsAbstract == false)
                    {
                        if (type.Name == name)
                        {
                            componentType = type;
                        }
                    }
                }
            }

            if (componentType != null)
                _selectedType = componentType;
        
            return name;
        }
    }
}