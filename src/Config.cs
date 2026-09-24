using BepInEx.Configuration;
using ModMenu.Config;

namespace TweaksOfYore {

    internal static class Config {
        // Entities
        [Field("Disable Cabin Goat")]
        internal static ConfigEntry<bool> disableCabinGoat { get; private set; }

        [Field("Disable Eagles")]
        internal static ConfigEntry<bool> disableEagles { get; private set; }

        [Field("Disable Swans")]
        internal static ConfigEntry<bool> disableSwans { get; private set; }

        [Field("Lower Mara's Arch Seagull Volume")]
        internal static ConfigEntry<bool> lowerMarasArchSeagullVolume { get; private set; }

        // Inventory
        [Field("Disable Belt Rope Detach")]
        internal static ConfigEntry<bool> disableBeltRopeDetach { get; private set; }

        // UI
        [Field("Disable Crux Notifications")]
        internal static ConfigEntry<bool> disableCruxNotifications { get; private set; }

        [Field("Disable Subtitles")]
        internal static ConfigEntry<bool> disableSubtitles { get; private set; }

        [Field("Display Accurate Records")]
        internal static ConfigEntry<bool> displayAccurateRecords { get; private set; }

        // Misc
        [Field("Skip Cleaning Items")]
        internal static ConfigEntry<bool> skipCleaningItems { get; private set; }

        [Field("Disable Snow Fall Particles")]
        internal static ConfigEntry<bool> disableSnowFallParticles { get; private set; }

        [Field("Extend Return To Cabin Bag")]
        internal static ConfigEntry<bool> extendReturnToCabinBag { get; private set; }

        [Field("Increase FOV Range")]
        internal static ConfigEntry<bool> increaseFovRange { get; private set; }

        [Field("Reduce Welkin Fog")]
        internal static ConfigEntry<bool> reduceWelkinFog { get; private set; }

        [Field("Mute On Unfocus")]
        internal static ConfigEntry<bool> muteOnUnfocus { get; private set; }

        [Field("Disable Exhale")]
        internal static ConfigEntry<bool> disableExhale { get; private set; }

        // Speedrun
        [Field("Pocketwatch% Mode")]
        internal static ConfigEntry<bool> pocketwatch { get; private set; }

        [Field("Full Game Mode")]
        internal static ConfigEntry<bool> fullGame { get; private set; }

        internal static void Init(ConfigFile configFile) {
            // Entities
            disableCabinGoat = configFile.Bind(
                "Entities", "disableCabinGoat", false,
                "Whether to disable the goat on the alps cabin."
            );
            disableEagles = configFile.Bind(
                "Entities", "disableEagles", false,
                "Whether to disable eagles once they have"
                + " all been collected."
            );
            disableSwans = configFile.Bind(
                "Entities", "disableSwans", false,
                "Whether to disable the swans at the castle."
            );
            lowerMarasArchSeagullVolume = configFile.Bind(
                "Entities", "lowerMarasArchSeagullVolume", false,
                "Whether to lower the volume of seagulls on Mara's Arch."
            );

            // Inventory
            disableBeltRopeDetach = configFile.Bind(
                "Inventory", "disableBeltRopeDetach", false,
                "Whether to disable detaching ropes by looking at your"
                + " belt and pressing the interact bind."
            );

            // UI
            disableCruxNotifications = configFile.Bind(
                "UI", "disableCruxNotifications", false,
                "Whether to disable crux notifications."
            );
            disableSubtitles = configFile.Bind(
                "UI", "disableSubtitles", false,
                "Whether to disable subtitles."
            );
            displayAccurateRecords = configFile.Bind(
                "UI", "displayAccurateRecords", false,
                "Whether to display accurate time records with the"
                + " pocketwatch open."
            );

            // Misc
            skipCleaningItems = configFile.Bind(
                "Misc", "skipCleaningItems", false,
                "Whether to skip cleaning items after collecting them."
            );
            disableSnowFallParticles = configFile.Bind(
                "Misc", "disableSnowFallParticles", false,
                "Whether to disable snow fall particle effects."
            );
            extendReturnToCabinBag = configFile.Bind(
                "Misc", "extendReturnToCabinBag", false,
                "Whether to extend the distance which the \"Return to Cabin\" bag"
                + " can be reached from."
            );
            increaseFovRange = configFile.Bind(
                "Misc", "increaseFovRange", false,
                "Whether to increase the configurable FOV range."
            );
            reduceWelkinFog = configFile.Bind(
                "Misc", "reduceWelkinFog", false,
                "Whether to reduce the fog on Welkin Pass."
            );
            muteOnUnfocus = configFile.Bind(
                "Misc", "muteOnUnfocus", false,
                "Whether to mute the game when it's no longer in focus."
            );
            disableExhale = configFile.Bind(
                "Misc", "disableExhale", false,
                "Whether to disable the random exhale particle effect."
            );

            // Speedrun
            pocketwatch = configFile.Bind(
                "Speedrun", "pocketwatch", false,
                "Only enable tweaks which are accepted in"
                + " Pocketwatch% (and Pipe Only) runs."
            );
            fullGame = configFile.Bind(
                "Speedrun", "fullGame", false,
                "Only enable tweaks which are accepted in"
                + " full game runs (Any%, 100%, All Peaks)."
            );
        }
    }
}
