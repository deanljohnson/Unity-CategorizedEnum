using UnityEngine;

namespace Assets.Plugins.CategorizedEnum
{
    /// <summary>
    /// Displays a categorized enum popup where the categories are
    /// auto-generated based on the names of the enum members.
    /// Use this to auto-categorize a single property of an enum type
    /// that is not categorized by default.
    /// </summary>
    public class AutoCategorizedAttribute : PropertyAttribute
    {
        /// <summary>
        /// The minimum number of times a starting substring must 
        /// be repeated within the enum before it becomes a category
        /// </summary>
        public int MinCategorySize { get; set; }

        public AutoCategorizedAttribute(int minCategorySize = 2)
        {
            MinCategorySize = minCategorySize;
        }
    }
}
