#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct Misc {
#if BEPINEX
        public ConfigEntry<bool> skipCleaningItems;
        public ConfigEntry<bool> disableSnowFallParticles;
        public ConfigEntry<bool> increaseFovRange;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> skipCleaningItems;
        public MelonPreferences_Entry<bool> disableSnowFallParticles;
        public MelonPreferences_Entry<bool> increaseFovRange;

#endif
    }
}
