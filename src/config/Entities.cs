#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct Entities {
#if BEPINEX
        public ConfigEntry<bool> disableCabinGoat;
        public ConfigEntry<bool> disableEagles;
        public ConfigEntry<bool> disableSwans;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> disableCabinGoat;
        public MelonPreferences_Entry<bool> disableEagles;
        public MelonPreferences_Entry<bool> disableSwans;

#endif
    }
}
