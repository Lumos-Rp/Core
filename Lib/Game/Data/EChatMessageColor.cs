using HogWarp.Lib.Interop.Attributes;

namespace HogWarp.Lib.Game.Data
{
    public enum EChatMessageColor
    {
        [EnumAttribute("</><default>")]
        NONE = 0,
        [EnumAttribute("</><gryffindor>")]
        RED = 1,
        [EnumAttribute("</><slytherin>")]
        GREEN = 2,
        [EnumAttribute("</><bavenclaw>")]
        BLUE = 3,
        [EnumAttribute("</><hufflepuff>")]
        YELLOW = 4,
        [EnumAttribute("</><server>")]
        OTHER = 5,
    }
}
