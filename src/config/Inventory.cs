#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace TweaksOfYore.Config {
    public struct Inventory {
#if BEPINEX
        public ConfigEntry<bool> disableBeltRopeDetach;
        public ConfigEntry<bool> disableNonPbClipboard;

#elif MELONLOADER
        public MelonPreferences_Entry<bool> disableBeltRopeDetach;
        public MelonPreferences_Entry<bool> disableNonPbClipboard;

#endif
    }
}
