using System;
using Assets.Plugins.CategorizedEnum.Example;
using UnityEditor;

namespace Assets.Plugins.CategorizedEnum.Editor.Example
{
    /// <summary>
    /// An example of how to define a custom categorizing function for a given enum type.
    /// This example uses an attribute other than the [Category] attribute distributed
    /// with the CategorizedEnum plugin. You do not have to use attributes to define your
    /// categories. The categories for the enum could come from anywhere.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomCategorizedEnum))]
    public class CustomCategoryDrawer : CustomCategorizedEnumDrawer
    {
        protected override string CategoryFunction(Enum e)
        {
            CustomCategoryAttribute att = e.GetAttributeOfType<CustomCategoryAttribute>();
            if (att != null) return att.Category.ToString();
            return "";
        }
    }
}
