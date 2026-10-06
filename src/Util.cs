using HarmonyLib;
using Rewired;

namespace TweaksOfYore {
    internal static class Util {
        internal static string GetKeybindByName(string name) {
            if (Cache.player == null) {
                return "N/A";
            }

            ActionElementMap map = Cache.player.controllers.maps
                .GetFirstElementMapWithAction(name, true);

            if (map == null) {
                return "N/A";
            }

            return map.elementIdentifierName;
        }

        internal static FT GetFieldValue<T, FT>(T instance, string name) {
            return (FT) AccessTools.Field(typeof(T), name).GetValue(instance);
        }
    }
}
