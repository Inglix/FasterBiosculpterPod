using Verse;

namespace FasterBiosculpterPod
{
    [StaticConstructorOnStartup]
    public static class FasterBiosculpterPod_PostInit
    {
        static FasterBiosculpterPod_PostInit()
        {
            //running this from here instead of SettingsUtils' own constructor allows other mods to get their patches in first
            SettingsUtils.ApplySettings(FasterBiosculpterPod.settings);
        }
    }
}
