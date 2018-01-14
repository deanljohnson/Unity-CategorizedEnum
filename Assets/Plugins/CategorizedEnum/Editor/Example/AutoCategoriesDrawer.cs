using Assets.Plugins.CategorizedEnum.Example;
using UnityEditor;

namespace Assets.Plugins.CategorizedEnum.Editor.Example
{
    /// <summary>
    /// An example of how to enable a certain enum type to
    /// be displayed with auto-generated categories. The categories
    /// are generated based off of the naming of the enum members.
    /// See <see cref="AutoCategories"/>.
    /// 
    /// You could just add the [CustomPropertyDrawer(typeof(AutoCategories))]
    /// line to the AutoCategorizedEnumDrawer definition, but these examples
    /// use separate files so that you can delete the examples folders from
    /// your project without causing any problems.
    /// </summary>
    [CustomPropertyDrawer(typeof(AutoCategories))]
    public class AutoCategoriesDrawer : AutoCategorizedEnumDrawer
    {
    }
}
