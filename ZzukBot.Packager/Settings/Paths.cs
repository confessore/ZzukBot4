using System.Reflection;
using ZzukBot.Packager.Utilities.Extensions;

namespace ZzukBot.Packager.Settings
{
    internal static class Paths
    {
        static Assembly Assembly = Assembly.GetExecutingAssembly();

        internal static string DestinationDruidLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Druid.dll";
        internal static string DestinationFisherLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Fisher.dll";
        internal static string DestinationFollowerLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Follower.dll";
        internal static string DestinationGathererLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Gatherer.dll";
        internal static string DestinationGrinderLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Grinder.dll";
        internal static string DestinationGrinderv2Library = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Grinderv2.dll";
        internal static string DestinationHarvesterLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\Harvester.dll";
        internal static string DestinationHunterLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Hunter.dll";
        internal static string DestinationLauncherExecutable = Assembly.ExtJumpUp(2) + "\\temp\\Launcher.exe";
        internal static string DestinationMageLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Mage.dll";
        internal static string DestinationPaladinLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Paladin.dll";
        internal static string DestinationPriestLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Priest.dll";
        internal static string DestinationProfileConverterLibrary = Assembly.ExtJumpUp(2) + "\\temp\\Plugins\\ProfileConverter.dll";
        internal static string DestinationProfileCreatorLibrary = Assembly.ExtJumpUp(2) + "\\temp\\Plugins\\ProfileCreator.dll";
        internal static string DestinationREADMEFile = Assembly.ExtJumpUp(2) + "\\temp\\README.txt";
        internal static string DestinationRogueLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Rogue.dll";
        internal static string DestinationShamanLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Shaman.dll";
        internal static string DestinationWarlockLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Warlock.dll";
        internal static string DestinationWarriorLibrary = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses\\Warrior.dll";
        internal static string DestinationWarriorRotationLibrary = Assembly.ExtJumpUp(2) + "\\temp\\BotBases\\WarriorRotation.dll";
        internal static string DestinationZipFile = Assembly.ExtJumpUp(3) + "\\ZzukBot.Web\\wwwroot\\downloads\\zzukbot.zip";
        internal static string DestinationZzukBotExecutable = Assembly.ExtJumpUp(2) + "\\temp\\Internal\\ZzukBot.exe";

        internal static string ServerSettings = Assembly.ExtJumpUp(3) + "\\ZzukBot.Server\\appsettings.json";

        internal static string SourceDruidLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Druid.dll";
        internal static string SourceFisherLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\Fisher.dll";
        internal static string SourceFollowerLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\Follower.dll";
        internal static string SourceGathererLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\Gatherer.dll";
        internal static string SourceGrinderLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\Grinder.dll";
        internal static string SourceGrinderv2Library = Assembly.ExtJumpUp(2) + "\\BotBases\\Grinderv2.dll";
        internal static string SourceHarvesterLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\Harvester.dll";
        internal static string SourceHunterLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Hunter.dll";
        internal static string SourceLauncherExecutable = Assembly.ExtJumpUp(2) + "\\Launcher.exe";
        internal static string SourceMageLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Mage.dll";
        internal static string SourcePaladinLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Paladin.dll";
        internal static string SourcePriestLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Priest.dll";
        internal static string SourceProfileConverterLibrary = Assembly.ExtJumpUp(2) + "\\Plugins\\ProfileConverter.dll";
        internal static string SourceProfileCreatorLibrary = Assembly.ExtJumpUp(2) + "\\Plugins\\ProfileCreator.dll";
        internal static string SourceREADMEFile = Assembly.ExtJumpUp(2) + "\\README.txt";
        internal static string SourceRogueLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Rogue.dll";
        internal static string SourceShamanLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Shaman.dll";
        internal static string SourceWarlockLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Warlock.dll";
        internal static string SourceWarriorLibrary = Assembly.ExtJumpUp(2) + "\\CustomClasses\\Warrior.dll";
        internal static string SourceWarriorRotationLibrary = Assembly.ExtJumpUp(2) + "\\BotBases\\WarriorRotation.dll";
        internal static string SourceZipFile = Assembly.ExtJumpUp(2) + "\\zzukbot.zip";
        internal static string SourceZzukBotExecutable = Assembly.ExtJumpUp(2) + "\\Internal\\ZzukBot.exe";

        internal static string Temp = Assembly.ExtJumpUp(2) + "\\temp";
        internal static string TempBotBases = Assembly.ExtJumpUp(2) + "\\temp\\BotBases";
        internal static string TempCustomClasses = Assembly.ExtJumpUp(2) + "\\temp\\CustomClasses";
        internal static string TempInternal = Assembly.ExtJumpUp(2) + "\\temp\\Internal";
        internal static string TempPlugins = Assembly.ExtJumpUp(2) + "\\temp\\Plugins";
    }
}
