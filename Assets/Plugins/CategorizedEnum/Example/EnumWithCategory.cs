namespace Assets.Plugins.CategorizedEnum.Example
{
    /// <summary>
    /// An example enum that will be categorized according to the
    /// category attributes placed on each member.
    /// </summary>
    public enum EnumWithCategory
    {
        // Will be listed under the category 'First'
        [Category("First")] One,
        [Category("First")] Two,

        // Will be listed under the category 'Second'
        [Category("Second")] Three,
        [Category("Second")] Four
    }
}
