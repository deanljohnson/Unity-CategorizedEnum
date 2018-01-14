using UnityEngine;

namespace Assets.Plugins.CategorizedEnum.Example
{
    public class CategorizationTest : MonoBehaviour
    {
        [TextArea(2,10)]
        public string CategoryExplanation;
        public EnumWithCategory Enum;

        [Space(20)]

        [TextArea(2,10)]
        public string AutoExplanation;
        public AutoCategories AutoEnum;

        [Space(20)]

        [TextArea(2, 10)]
        public string CustomExplanation;
        public CustomCategorizedEnum CustomCategorizedEnum;

        [Space(20)]

        [TextArea(2, 10)]
        public string SinglePropertyExplanation;
        public NotCategorizedByDefaultEnum Default;
        [Categorized]
        public NotCategorizedByDefaultEnum Categorized;
        [AutoCategorized]
        public NotCategorizedByDefaultEnum AutoCategorized;
        [AutoCategorized(3)]
        public NotCategorizedByDefaultEnum AutoCategorized3Min;
    }
}
