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
        public ConfigEntry<bool> reduceWelkinFog;
        public ConfigEntry<bool> muteOnUnfocus;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> skipCleaningItems;
        public MelonPreferences_Entry<bool> disableSnowFallParticles;
        public MelonPreferences_Entry<bool> increaseFovRange;
        public MelonPreferences_Entry<bool> reduceWelkinFog;
        public MelonPreferences_Entry<bool> muteOnUnfocus;

#endif
    }
}
