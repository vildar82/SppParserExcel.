using System.ComponentModel;

namespace SppData
{
    public enum ApartmentTypeEnum
    {
        /// <summary>
        /// Студия
        /// </summary>
        [Description("Студия")]
        Studio,
        /// <summary>
        /// 1-комнатная
        /// </summary>
        [Description("1-комнатная")]
        One,
        /// <summary>
        /// 2-комнатная
        /// </summary>
        [Description("2-комнатная")]
        Two,
        /// <summary>
        /// 3-комнатная
        /// </summary>
        [Description("3-комнатная")]
        Three,
        /// <summary>
        /// 4-комнатная
        /// </summary>
        [Description("4-комнатная")]
        Four
    }
}