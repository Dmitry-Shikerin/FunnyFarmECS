using System;
using System.Linq;
using System.Reflection;
using Sources.MyLeoEcsProto.ComponentContainers.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor
{
    public class ComponentPropertyField : PropertyField
    {
        public ComponentPropertyField(SerializedProperty serializedProperty) 
            : base(serializedProperty, serializedProperty.name)
        {
            // foreach (PropertyInfo property in serializedProperty.GetType().GetProperties())
            // {
            //     CFAttribute attribute = property.GetCustomAttributes<CFAttribute>(true)
            //         .SingleOrDefault();
            //     
            //     if (attribute == null)
            //         continue;
            //     
            //     Debug.Log(property.Name);
            //
            //     SerializedProperty propertyField = serializedProperty.FindPropertyRelative(property.Name);
            //     contentContainer.Add(new PropertyField(propertyField));
            // }
        }

        private void FindFields()
        {
            
        }
    }
}