using UnityEngine;
using UnityEngine.Audio;

namespace TweaksOfYore {
    internal static class Cache {
        internal static AudioMixer mixer { get; private set; }

        internal static void FindObjects() {
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
