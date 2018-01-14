namespace Assets.Plugins.CategorizedEnum.Example
{
    /// <summary>
    /// An example enum that will be categorized only when
    /// a property of this type is decorated with the
    /// [Categorized] or [AutoCategorized] attributes
    /// </summary>
    public enum NotCategorizedByDefaultEnum
    {
        [Category("Brown")] BrownBook,
        [Category("Brown")] BrownPaper,
        [Category("Brown")] BrownMovie,
        [Category("Brown")] BrownComputer,

        [Category("Blue")] BlueBook,
        [Category("Blue")] BluePaper,
        [Category("Blue")] BlueMovie,
        [Category("Blue")] BlueComputer,

        [Category("Green")] GreenBook,
        [Category("Green")] GreenPaper
    }
}
