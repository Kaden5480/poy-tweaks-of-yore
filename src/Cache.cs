using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace TweaksOfYore {
    internal static class Cache {
        internal static Scene scene { get; private set; }
        internal static AudioMixer mixer { get; private set; }

        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;

            AudioMixerOptions mixerOptions = GameObject.FindObjectOfType<AudioMixerOptions>();
            if (mixerOptions != null) {
                mixer = mixerOptions.mixer;
            }
        }

        internal static void Clear() {
            mixer = null;
        }
    }
}
