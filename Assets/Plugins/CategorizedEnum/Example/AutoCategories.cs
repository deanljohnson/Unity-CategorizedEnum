namespace Assets.Plugins.CategorizedEnum.Example
{
    /// <summary>
    /// An example enum that can have categories auto-generated 
    /// based on the naming conventions of the enum members.
    /// </summary>
    public enum AutoCategories
    {
        // Will not be categorized
        UniqueTable,

        // Will be listed under the category 'Square'
        SquareTable,
        SquareCup,
        SquareCar,

        // Will be listed under the category 'Circle'
        CircleTable,
        CircleCup,
        CircleCar
    }
}
