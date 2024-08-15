using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.MyLeoEcsProto.ComponentContainers.Presentation.Editor.Extencions
{
    public static class VisualExtension
    {
        public static T AddChild<T, TChild>(this T root, TChild visualElement)
            where T : VisualElement
            where TChild : VisualElement {
            root.Add(visualElement);
            return root;
        }
        
        public static VisualElement AddChildPropertiesOf(this VisualElement root, SerializedProperty property) 
        {
            foreach (SerializedProperty childProperty in property.GetChildren()) 
            {
                root.Add(new PropertyField(childProperty));
            }

            return root;
        }
        
        public static IList<SerializedProperty> GetChildren(
            this SerializedProperty property, 
            int maxAmount = int.MaxValue) 
        {
            List<SerializedProperty> properties = new List<SerializedProperty>();
            SerializedProperty prop = property.Copy();
            SerializedProperty nextProp = Next(property);

            if (HasChildren() == false)
                return properties;

            do 
            {
                StoreChild();
            } while (HasNextChild());

            return properties;


            void StoreChild() =>
                properties.Add(prop.Copy());

            bool HasChildren() =>
                prop.NextVisible(enterChildren: true)
                && !EqualNextProperty();


            bool HasNextChild() =>
                prop.NextVisible(enterChildren: false)
                && !EqualNextProperty()
                && properties.Count < maxAmount;

            bool EqualNextProperty() =>
                SerializedProperty.EqualContents(prop, nextProp);
        }

        private static SerializedProperty Next(SerializedProperty property) 
        {
            SerializedProperty nextProp = property.Copy();
            nextProp.NextVisible(enterChildren: false);
            
            return nextProp;
        }
        
        public static IStyle Margin(this IStyle style, StyleLength top, StyleLength bottom, StyleLength left, StyleLength right) 
        {
            style.marginTop    = top;
            style.marginBottom = bottom;
            style.marginLeft   = left;
            style.marginRight  = right;

            return style;
        }

        public static IStyle Margin(this IStyle style, StyleLength hor, StyleLength ver)
            => style.Margin(ver, ver, hor, hor);

        public static IStyle Margin(this IStyle style, StyleLength length)
            => style.Margin(length, length);
        
        public static IStyle Padding(
            this IStyle style,
            StyleLength lengthTop,
            StyleLength lengthBottom,
            StyleLength lengthLeft,
            StyleLength lengthRight) 
        {
            style.paddingTop    = lengthTop;
            style.paddingBottom = lengthBottom;
            style.paddingLeft   = lengthLeft;
            style.paddingRight  = lengthRight;

            return style;
        }


        public static IStyle Padding(this IStyle style, StyleLength hor, StyleLength ver)
            => style.Padding(ver, ver, hor, hor);


        public static IStyle Padding(this IStyle style, StyleLength length)
            => style.Padding(length, length);
        
        public static IStyle BorderRadius(
            this IStyle style,
            StyleLength tr,
            StyleLength br,
            StyleLength bl,
            StyleLength tl
        ) {
            style.borderTopRightRadius    = tr;
            style.borderBottomRightRadius = br;
            style.borderBottomLeftRadius  = bl;
            style.borderTopLeftRadius     = tl;

            return style;
        }

        public static IStyle BorderRadius(this IStyle style, StyleLength length) 
        {
            style.BorderRadius(length, length, length, length);

            return style; 
        }
        
        public static TextElement SetText(this TextElement label, string text) 
        {
            label.text = text;
            return label;
        }
        
        public static IStyle FontStyle(this IStyle style, FontStyle fontStyle) 
        {
            style.unityFontStyleAndWeight = fontStyle;
            return style;
        }
        
        public static IStyle FlexRow(this IStyle style, bool reverse = false) {
            style.flexDirection = !reverse
                ? FlexDirection.Row
                : FlexDirection.RowReverse;
            return style;
        }

        public static IStyle FlexColumn(this IStyle style, bool reverse = false) {
            style.flexDirection = !reverse
                ? FlexDirection.Column
                : FlexDirection.ColumnReverse;
            return style;
        }
        
        public static IStyle FontSize(this IStyle style, StyleLength value) {
            style.fontSize = value;
            return style;
        }
        
        public static IStyle OverflowHidden(this IStyle style) {
            style.overflow = Overflow.Hidden;
            return style;
        }

        public static IStyle OverflowVisible(this IStyle style) {
            style.overflow = Overflow.Visible;
            return style;
        }
        
        public static IStyle FlexGrow(this IStyle style, bool grow = true) {
            style.flexGrow = grow
                ? 1
                : 0;
            return style;
        }

        public static IStyle FlexShrink(this IStyle style, bool grow = true) {
            style.flexShrink = grow
                ? 1
                : 0;
            return style;
        }
    }
}