using System;

namespace Assets.Plugins.CategorizedEnum.Example
{
    /// <summary>
    /// An example enum that will be categorized according the 
    /// the CustomCategory attribute assigned to each member.
    /// See CustomCategoryDrawer.cs
    /// </summary>
    public enum CustomCategorizedEnum
    {
        // Will be listed under the category 'Asia'
        [CustomCategory(CustomCategory.Asia)] China,
        [CustomCategory(CustomCategory.Asia)] Japan,

        // Will be listed under the category 'Europe'
        [CustomCategory(CustomCategory.Europe)] Sweden,
        [CustomCategory(CustomCategory.Europe)] Norway
    }

    public enum CustomCategory
    {
        Asia,
        Europe
    }

    public class CustomCategoryAttribute : Attribute
    {
        public CustomCategory Category { get; set; }

        public CustomCategoryAttribute(CustomCategory cat)
        {
            Category = cat;
        }
    }
}
