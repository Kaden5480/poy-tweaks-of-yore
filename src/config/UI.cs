#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct UI {
#if BEPINEX
        public ConfigEntry<bool> disableCruxNotifications;
        public ConfigEntry<bool> disableSubtitles;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> disableCruxNotifications;
        public MelonPreferences_Entry<bool> disableSubtitles;

#endif
    }
}
