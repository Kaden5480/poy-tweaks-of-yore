using UnityEngine;
using UnityEngine.Audio;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TweaksOfYore {
    internal static class Cache {
        internal static Scene scene { get; private set; }

        internal static AudioMixer mixer { get; private set; }
        internal static Player player { get; private set; }
        internal static TimeAttack timeAttack { get; private set; }

        internal static Text pocketwatchTutText { get; private set; }
        internal static Text recordTutText { get; private set; }
        internal static Text scoreboardTutText { get; private set; }
        internal static RectTransform recordTutBg { get; private set; }
        internal static RectTransform scoreboardTutBg { get; private set; }

        private static RectTransform GetImageTransformFor(GameObject obj) {
            Transform t = obj.transform.Find("bg");
            if (t == null) {
                return null;
            }

            return t.GetComponent<RectTransform>();
        }

        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;

            AudioMixerOptions mixerOptions = GameObject.FindObjectOfType<AudioMixerOptions>();
            if (mixerOptions != null) {
                mixer = mixerOptions.mixer;
            }

            timeAttack = GameObject.FindObjectOfType<TimeAttack>();
            if (timeAttack == null) {
                return;
            }

            player = Util.GetFieldValue<TimeAttack, Rewired.Player>(timeAttack, "player");

            pocketwatchTutText = timeAttack.pocketwatchDisplayTut.GetComponentInChildren<Text>();
            recordTutText = timeAttack.recordDisplayTut.GetComponentInChildren<Text>();
            scoreboardTutText = timeAttack.scoreboardDisplayTut.GetComponentInChildren<Text>();

            recordTutBg = GetImageTransformFor(timeAttack.recordDisplayTut.gameObject);
            scoreboardTutBg = GetImageTransformFor(timeAttack.scoreboardDisplayTut.gameObject);
        }

        internal static void Clear() {
            mixer = null;
            player = null;
            timeAttack = null;

            pocketwatchTutText = null;
            recordTutText = null;
            scoreboardTutText = null;

            recordTutBg = null;
            scoreboardTutBg = null;
        }
    }
}
