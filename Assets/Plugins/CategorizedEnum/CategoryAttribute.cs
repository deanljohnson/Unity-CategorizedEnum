using System;

namespace Assets.Plugins.CategorizedEnum
{
    /// <summary>
    /// Can be used to assign a category to an enum type.
    /// For use with either the CategorizedEnumDrawer or
    /// the Categorized attribute.
    /// </summary>
    public class CategoryAttribute : Attribute
    {
        public string Category { get; private set; }

        public CategoryAttribute(string category)
        {
            Category = category;
        }
    }
}
