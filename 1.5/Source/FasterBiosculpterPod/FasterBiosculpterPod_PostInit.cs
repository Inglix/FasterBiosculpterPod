using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
