using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.CategorizedEnum.Editor
{
    /// <summary>
    /// A PropertyDrawer that categorizes an enum popup according
    /// to the specified CategoryFunction
    /// </summary>
    public abstract class CustomCategorizedEnumDrawer : PropertyDrawer
    {
        /// <summary>
        /// Returns the category for the given Enum value
        /// </summary>
        protected abstract string CategoryFunction(Enum e);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.enumValueIndex = CategorizedEnumPopup.Popup(position, label, property, CategoryFunction);
        }
    }

    /// <summary>
    /// A PropertyDrawer that categorizes an enum by its naming convention
    /// </summary>
    [CustomPropertyDrawer(typeof(AutoCategorizedAttribute))]
    public class AutoCategorizedEnumDrawer : PropertyDrawer
    {
        public virtual int MinCategorySize {
            get
            {
                int minCategorySize = 2;
                if (attribute != null)
                    minCategorySize = ((AutoCategorizedAttribute)attribute).MinCategorySize;
                return minCategorySize;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            property.enumValueIndex = CategorizedEnumPopup.AutoCategorizedPopup(position, label, property, MinCategorySize);
        }
    }

    /// <summary>
    /// A PropertyDrawer that categorizes an enum popup according
    /// to the CategoryAttribute placed on each enum value
    /// </summary>
    [CustomPropertyDrawer(typeof(CategorizedAttribute))]
    public class CategorizedEnumDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.enumValueIndex = CategorizedEnumPopup.Popup(position, label, property);
        }
    }

    /// <summary>
    /// Provides the ability to display categorized enum popup fields
    /// </summary>
    public static class CategorizedEnumPopup
    {
        /// <summary>
        /// Displays a categorized enum popup where the categories are
        /// auto-generated based on the names of the enum members
        /// </summary>
        public static int AutoCategorizedPopup(Rect position, GUIContent label, SerializedProperty property, int minCategorySize = 2)
        {
            // All possible categories. The first portions of the enum names.
            string[] possibleCategoriesPerEnum = property.enumDisplayNames.Select(s => s.Split(' ')[0]).ToArray();

            // Count how many times a possible category appears within the enum
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (string category in possibleCategoriesPerEnum)
            {
                if (!categoryCount.ContainsKey(category))
                    categoryCount.Add(category, 0);
                categoryCount[category]++;
            }

            // The set of categories that have occured at least minCategorySize times
            HashSet<string> categories = new HashSet<string>(categoryCount.Where(kvp => kvp.Value >= minCategorySize).Select(kvp => kvp.Key));

            GUIContent[] options = new GUIContent[property.enumDisplayNames.Length];
            for (int i = 0; i < options.Length; i++)
            {
                string menuPath = property.enumDisplayNames[i];
                // If the possible category for this enum has occured enough times to be a category
                if (categories.Contains(possibleCategoriesPerEnum[i])) 
                    menuPath = possibleCategoriesPerEnum[i] + "/" + menuPath;

                options[i] = new GUIContent(menuPath);
            }

            return EditorGUI.Popup(position, label, property.enumValueIndex, options);
        }

        /// <summary>
        /// Displays a categorized enum popup according to given categoryFunction.
        /// If categoryFunction is null, categories are determined by the CategoryAttribute
        /// on the enum members.
        /// </summary>
        public static int Popup(Rect position, GUIContent label, Enum selected, Func<Enum, string> categoryFunction = null)
        {
            List<Enum> enumValues = Enum.GetValues(selected.GetType()).Cast<Enum>().ToList();
            if (categoryFunction == null) categoryFunction = DefaultyCategoryFunction;
            return Popup(position, label, enumValues.IndexOf(selected), enumValues, EnumDisplayNames(selected.GetType()), categoryFunction);
        }

        /// <summary>
        /// Displays a categorized enum popup according to given categoryFunction.
        /// If categoryFunction is null, categories are determined by the CategoryAttribute
        /// on the enum members.
        /// </summary>
        public static int Popup(Rect position, GUIContent label, SerializedProperty property, Func<Enum, string> categoryFunction = null)
        {
            Enum[] enumValues = Enum.GetValues(property.GetPropertyType()).Cast<Enum>().ToArray();
            if (categoryFunction == null) categoryFunction = DefaultyCategoryFunction;
            return Popup(position, label, property.enumValueIndex, enumValues, property.enumDisplayNames, categoryFunction);
        }

        private static int Popup(Rect position, GUIContent label, int selectedIndex, 
            IList<Enum> values, IList<string> displayStrings, Func<Enum, string> categoryFunction)
        {
            GUIContent[] options = new GUIContent[values.Count];
            for (var i = 0; i < values.Count; i++)
            {
                string categoryString = categoryFunction(values[i]);
                if (!string.IsNullOrEmpty(categoryString)) categoryString += "/";
                options[i] = new GUIContent(categoryString + displayStrings[i]);
            }
            return EditorGUI.Popup(position, label, selectedIndex, options);
        }

        private static string DefaultyCategoryFunction(Enum e)
        {
            CategoryAttribute category = e.GetAttributeOfType<CategoryAttribute>();
            string categoryString = category != null ? category.Category : "";
            return categoryString;
        }

        /// <summary>
        /// Returns display-friendly names for the enums of the specified type
        /// </summary>
        private static string[] EnumDisplayNames(Type enumType)
        {
            return Enum.GetNames(enumType).Select(s => s.SplitCamelCase()).ToArray();
        }

        /// <summary>
        /// Splits the given camel case string
        /// </summary>
        private static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this Enum enumVal)
            where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Returns the unerlying type of the SerializedProperty
        /// </summary>
        public static Type GetPropertyType(this SerializedProperty property)
        {
            string[] slices = property.propertyPath.Split('.');
            Type type = property.serializedObject.targetObject.GetType();

            for (int i = 0; i < slices.Length; i++)
            {
                if (slices[i] == "Array")
                {
                    i++; //skips "data[x]"
                    if (type.IsGenericType
                        && type.GetGenericTypeDefinition() == typeof(List<>))
                        type = type.GetGenericArguments()[0];
                    else
                        type = type.GetElementType(); //gets info on array elements
                }
                else
                {
                    type = type.GetField(slices[i], BindingFlags.NonPublic
                                                    | BindingFlags.Public
                                                    | BindingFlags.FlattenHierarchy
                                                    | BindingFlags.Instance).FieldType;
                }
            }

            return type;
        }
    }
}
