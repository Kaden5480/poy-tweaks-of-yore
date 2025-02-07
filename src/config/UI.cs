#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct UI {
#if BEPINEX
        public ConfigEntry<bool> disableCruxNotifications;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> disableCruxNotifications;

#endif
    }
}
