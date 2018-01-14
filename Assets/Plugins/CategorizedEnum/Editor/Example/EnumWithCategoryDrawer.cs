using Assets.Plugins.CategorizedEnum.Example;
using UnityEditor;

namespace Assets.Plugins.CategorizedEnum.Editor.Example
{
    /// <summary>
    /// This example shows how to enable your enum with [Category]
    /// attributes to be displayed with a categorizd popup. 
    /// 
    /// You could just add the [CustomPropertyDrawer(typeof(EnumWithCategory))]
    /// line to the CategorizedEnumDrawer definition, but these examples
    /// use separate files so that you can delete the examples folders from
    /// your project without causing any problems.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumWithCategory))]
    public class EnumWithCategoryDrawer : CategorizedEnumDrawer
    { 
    }
}
