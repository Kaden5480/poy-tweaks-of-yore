#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct Speedrun {
#if BEPINEX
        public ConfigEntry<bool> pocketwatch;
        public ConfigEntry<bool> fullGame;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> pocketwatch;
        public MelonPreferences_Entry<bool> fullGame;

#endif
    }
}
