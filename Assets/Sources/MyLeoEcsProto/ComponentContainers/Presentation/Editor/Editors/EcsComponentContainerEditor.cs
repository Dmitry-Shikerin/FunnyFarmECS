using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Client.BaseCommands;
using Leopotam.EcsProto.Unity;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using Sources.MyLeoEcsProto.ComponentContainers.Domain;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Extencions;
using Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Propertyes;
using Sources.Transforms;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using FieldInfo = System.Reflection.FieldInfo;
using Object = UnityEngine.Object;
using ObjectField = UnityEditor.Search.ObjectField;
using SerializedProperty = UnityEditor.SerializedProperty;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor
{
    [CustomEditor(typeof(ComponentContainer))]
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

        // private Button          _killBtn;
        // private Button          _delBtn;
        // private Button          _addBtn;
        private VisualElement _componentsContainer;
        private PropertyField[] _componentsDrawers = Array.Empty<PropertyField>();

        private ComponentContainer Target => (ComponentContainer)target;

        // private EcsWorld   World           => Target.World;
        // private int        Entity          => Target.Entity;
        // private string Label => Target.BakedIndex;
        private int ComponentsCount => Target.Components.Count;
        private SerializedProperty ComponentsProperty { get; set; }


        public override VisualElement CreateInspectorGUI()
        {
            CreateElements();
            StructureElements();
            InitElements();
            return _root;
        }


        private void CreateElements()
        {
            _root = new VisualElement();
            _header = new Box();
            _title = new Label();
            // _addBtn              = new Button(ChooseAndAdd) { text = _ADD_BTN_TEXT };
            // _delBtn              = new Button(ChooseAndDel) { text = _DEL_BTN_TEXT };
            // _killBtn             = new Button(Kill) { text         = _KILL_BTN_TEXT };
            _componentsContainer = new VisualElement();
        }

        private void StructureElements()
        {
            _root
                .AddChild(_header.AddChild(_title))
                .AddChild(_componentsContainer);
            
            _root.AddChild(new DropdownField(
                GetComponentsNames(), 
                string.Empty, 
                SetComponentType));

            // if (Target.IsAlive)
            // _header
            //   .AddChild(_addBtn)
            //   .AddChild(_delBtn)
            //   .AddChild(_killBtn);
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

            _title
                .SetText("Label")
                .style
                .FlexGrow();

            // _addBtn.style.width = _delBtn.style.width = _killBtn.style.width = StyleConsts.REM * 5;

            ComponentsProperty = serializedObject.FindProperty(ComponentsField);
            // Target.Components.Clear();
            
            if (Target.Components.Count <= 0)
                return;
            
            Debug.Log(Target.Components.Count);
            RefreshComponents();
            // _root.TrackPropertyValue(ComponentsCountProperty(), _ => RefreshComponents());
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


        // private void ChooseAndAdd() => ComponentsSearchWindow.OpenFor(World, AddComponent);
        // private void ChooseAndDel() => ComponentsSearchWindow.OpenFor(World, Entity, DelComponent);

        // private bool AddComponent(Type comp) {
        //    IEcsPool pool = World.GetPoolByType(comp);
        //
        //    if (pool.Has(Entity))
        //       return false;
        //
        //    pool.AddRaw(Entity, Activator.CreateInstance(comp));
        //    return true;
        

        // }


        // private bool DelComponent(Type comp) {

        //    IEcsPool pool = World.GetPoolByType(comp);

        //

        //    if (!pool.Has(Entity))

        //       return false;

        //

        //    pool.Del(Entity);

        //    return true;

        // }


        // private void Kill() => World.DelEntity(Entity);


        // private SerializedProperty ComponentsCountProperty() => serializedObject.FindProperty(_COMPONENTS_COUNT_FIELD);

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
            {
                object component = Activator.CreateInstance(componentType);
                ComponentView componentView = new ComponentView(Target);
                componentView.Component = component;
                Target.Components.Add(componentView);
            }
        
            return name;
        }

        //     private void DrawComponents (ComponentContainer componentContainer) 
        //     {
        //         if (componentContainer.gameObject.activeSelf) 
        //         {
        //             // var count = componentContainer.World.GetComponents(componentContainer.Entity, ref _componentsCache);
        //             //
        //             // for (var i = 0; i < count; i++) 
        //             // {
        //                 var component = componentContainer.Components;
        //                 _componentsCache[i] = null;
        //                 var type = component.GetType ();
        //                 GUILayout.BeginVertical(GUI.skin.box);
        //                 var typeName = EditorExtensions.GetCleanGenericTypeName (type);
        //                 var pool = componentContainer.World.GetPoolByType (type);
        //                 var (rendered, changed, newValue) = EcsComponentInspectors.Render(typeName, type, component, componentContainer);
        //                 if (!rendered) 
        //                 {
        //                     EditorGUILayout.LabelField (typeName, EditorStyles.boldLabel);
        //                     var indent = EditorGUI.indentLevel;
        //                     EditorGUI.indentLevel++;
        //                     
        //                     foreach (var field in type.GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) 
        //                     {
        //                         DrawTypeField (component, pool, field, componentContainer);
        //                     }
        //                     EditorGUI.indentLevel = indent;
        //                 } else 
        //                 {
        //                     if (changed) 
        //                     {
        //                         // update value.
        //                         pool.SetRaw (componentContainer.Entity, newValue);
        //                     }
        //                 }
        //                 
        //                 GUILayout.EndVertical ();
        //                 EditorGUILayout.Space ();
        //             // }
        //         }
        //     }
        //
        //     private void DrawTypeField (object component, IEcsPool pool, FieldInfo field, ComponentContainer debugView) 
        //     {
        //         var fieldValue = field.GetValue (component);
        //         var fieldType = field.FieldType;
        //         var (rendered, changed, newValue) = EcsComponentInspectors.Render (field.Name, fieldType, fieldValue, debugView);
        //         if (!rendered) 
        //         {
        //             if (fieldType == typeof (Object) || fieldType.IsSubclassOf (typeof (Object))) 
        //             {
        //                 GUILayout.BeginHorizontal ();
        //                 EditorGUILayout.LabelField (field.Name, GUILayout.MaxWidth (EditorGUIUtility.labelWidth - 16));
        //                 var newObjValue = EditorGUILayout.ObjectField (fieldValue as Object, fieldType, true);
        //                 
        //                 if (newObjValue != (Object) fieldValue) 
        //                 {
        //                     field.SetValue (component, newObjValue);
        //                     pool.SetRaw (debugView.Entity, component);
        //                 }
        //                 
        //                 GUILayout.EndHorizontal ();
        //                 
        //                 return;
        //             }
        //             if (fieldType.IsEnum) 
        //             {
        //                 var isFlags = Attribute.IsDefined (fieldType, typeof (FlagsAttribute));
        //                 var (enumChanged, enumNewValue) = EcsComponentInspectors.RenderEnum(field.Name, fieldValue, isFlags);
        //                 
        //                 if (enumChanged) 
        //                 {
        //                     field.SetValue (component, enumNewValue);
        //                     pool.SetRaw (debugView.Entity, component);
        //                 }
        //                 
        //                 return;
        //             }
        //             var strVal = fieldValue != null ? string.Format (System.Globalization.CultureInfo.InvariantCulture, "{0}", fieldValue) : "null";
        //             
        //             if (strVal.Length > MaxFieldToStringLength) 
        //             {
        //                 strVal = strVal.Substring (0, MaxFieldToStringLength);
        //             }
        //             
        //             GUILayout.BeginHorizontal ();
        //             EditorGUILayout.PrefixLabel (field.Name);
        //             EditorGUILayout.SelectableLabel (strVal, GUILayout.MaxHeight (EditorGUIUtility.singleLineHeight));
        //             GUILayout.EndHorizontal ();
        //         } 
        //         else 
        //         {
        //             if (changed) 
        //             {
        //                 // update value.
        //                 field.SetValue (component, newValue);
        //                 pool.SetRaw (debugView.Entity, component);
        //             }
        //         }
        //     }
        // }
        //
        // static class EcsComponentInspectors 
        // {
        //     static readonly Dictionary<Type, IEcsComponentInspector> Inspectors = new Dictionary<Type, IEcsComponentInspector> ();
        //
        //     static EcsComponentInspectors () 
        //     {
        //         foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) 
        //         {
        //             foreach (var type in assembly.GetTypes ()) 
        //             {
        //                 if (typeof(IEcsComponentInspector).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract) 
        //                 {
        //                     if (Activator.CreateInstance(type) is IEcsComponentInspector inspector)
        //                     {
        //                         var componentType = inspector.GetFieldType ();
        //                         if (!Inspectors.TryGetValue (componentType, out var prevInspector)
        //                             || inspector.GetPriority () > prevInspector.GetPriority ()) 
        //                         {
        //                             Inspectors[componentType] = inspector;
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //
        //     public static (bool, bool, object) Render (string label, Type type, object value, ComponentContainer debugView) 
        //     {
        //         if (Inspectors.TryGetValue (type, out var inspector)) 
        //         {
        //             var (changed, newValue) = inspector.OnGui (label, value, debugView);
        //             
        //             return (true, changed, newValue);
        //         }
        //         return (false, false, null);
        //     }
        //
        //     public static (bool, object) RenderEnum (string label, object value, bool isFlags) 
        //     {
        //         var enumValue = (Enum) value;
        //         Enum newValue;
        //         
        //         if (isFlags) 
        //         {
        //             newValue = EditorGUILayout.EnumFlagsField (label, enumValue);
        //         } else 
        //         {
        //             newValue = EditorGUILayout.EnumPopup (label, enumValue);
        //         }
        //
        //         if (Equals(newValue, value))
        //         {
        //             return (default, default);
        //         }
        //         
        //         return (true, newValue);
        //     }
        // }
        //
        // public interface IEcsComponentInspector 
        // {
        //     Type GetFieldType ();
        //     int GetPriority ();
        //     (bool, object) OnGui (string label, object value, ComponentContainer entityView);
        // }
        //
        // public abstract class EcsComponentInspectorTyped<T> : IEcsComponentInspector 
        // {
        //     public Type GetFieldType () => typeof (T);
        //     public virtual bool IsNullAllowed () => false;
        //     public virtual int GetPriority () => 0;
        //
        //     public (bool, object) OnGui (string label, object value, ComponentContainer entityView) 
        //     {
        //         if (value == null && !IsNullAllowed ()) 
        //         {
        //             GUILayout.BeginHorizontal ();
        //             EditorGUILayout.PrefixLabel (label);
        //             EditorGUILayout.SelectableLabel ("null", GUILayout.MaxHeight (EditorGUIUtility.singleLineHeight));
        //             GUILayout.EndHorizontal ();
        //             
        //             return (default, default);
        //         }
        //         var typedValue = (T) value;
        //         var changed = OnGuiTyped (label, ref typedValue, entityView);
        //         if (changed) {
        //             return (true, typedValue);
        //         }
        //         return (default, default);
        //     }
        //
        //     public abstract bool OnGuiTyped (string label, ref T value, ComponentContainer entityView);
    }
}