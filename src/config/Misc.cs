#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct Misc {
#if BEPINEX
        public ConfigEntry<bool> skipCleaningItems;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> skipCleaningItems;

#endif
    }
}
