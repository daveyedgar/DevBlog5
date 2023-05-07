using System.ComponentModel;

namespace DevBlog5.Enums
{
    public enum ModerationType
    {
        [Description("Political Propaganda")]
        Political,
        [Description("Offensive Language")]
        Language,
        [Description("Illegal Substance Reference")]
        Drugs,
        [Description("Threatening or Flaming Content")]
        Threatening,
        [Description("Rated X Content")]
        Sexual,
        [Description("Hate Speach")]
        Speach,
        [Description("Targeted Shaming")]
        Shaming
    }
}
