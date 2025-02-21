using System.Reflection;

using HarmonyLib;
using UnityEngine;

namespace TweaksOfYore.Patches {
    public static class Helper {
        public static FieldInfo GetFieldInfo<T>(string fieldName) {
            return AccessTools.Field(typeof(T), fieldName);
        }

        public static FT GetField<T, FT>(T instance, string fieldName) {
            FieldInfo info = GetFieldInfo<T>(fieldName);
            return (FT) info.GetValue(instance);
        }

        public static void SetField<T, FT>(T instance, string fieldName, FT value) {
            FieldInfo info = GetFieldInfo<T>(fieldName);
            info.SetValue(instance, value);
        }
    }
}
