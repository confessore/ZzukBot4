using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using ZzukBot.Packager.Settings;
using ZzukBot.Packager.Utilities.Extensions;

namespace ZzukBot.Packager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ZzukBot Archive Packager\n");

            Default.Version = Assembly.LoadFrom(Paths.SourceZzukBotExecutable).GetMd5AsBase64();
            Console.WriteLine("Updated version in server appsettings.json to reflect assembly changes");

            File.Delete(Paths.SourceZipFile);
            File.Delete(Paths.DestinationZipFile);
            Console.WriteLine("Deleted existing archives in the Release and wwwroot folders");

            Directory.CreateDirectory(Paths.Temp);
            Directory.CreateDirectory(Paths.TempBotBases);
            Directory.CreateDirectory(Paths.TempCustomClasses);
            Directory.CreateDirectory(Paths.TempInternal);
            Directory.CreateDirectory(Paths.TempPlugins);
            Console.WriteLine("Created new temp directory");

            File.Copy(Paths.SourceDruidLibrary, Paths.DestinationDruidLibrary);
            File.Copy(Paths.SourceFisherLibrary, Paths.DestinationFisherLibrary);
            File.Copy(Paths.SourceFollowerLibrary, Paths.DestinationFollowerLibrary);
            //File.Copy(Paths.SourceGathererLibrary, Paths.DestinationGathererLibrary);
            File.Copy(Paths.SourceGrinderLibrary, Paths.DestinationGrinderLibrary);
            File.Copy(Paths.SourceGrinderv2Library, Paths.DestinationGrinderv2Library);
            //File.Copy(Paths.SourceHarvesterLibrary, Paths.DestinationHarvesterLibrary);
            File.Copy(Paths.SourceHunterLibrary, Paths.DestinationHunterLibrary);
            File.Copy(Paths.SourceLauncherExecutable, Paths.DestinationLauncherExecutable);
            File.Copy(Paths.SourceMageLibrary, Paths.DestinationMageLibrary);
            File.Copy(Paths.SourcePaladinLibrary, Paths.DestinationPaladinLibrary);
            File.Copy(Paths.SourcePriestLibrary, Paths.DestinationPriestLibrary);
            File.Copy(Paths.SourceProfileConverterLibrary, Paths.DestinationProfileConverterLibrary);
            File.Copy(Paths.SourceProfileCreatorLibrary, Paths.DestinationProfileCreatorLibrary);
            File.Copy(Paths.SourceREADMEFile, Paths.DestinationREADMEFile);
            File.Copy(Paths.SourceRogueLibrary, Paths.DestinationRogueLibrary);
            File.Copy(Paths.SourceShamanLibrary, Paths.DestinationShamanLibrary);
            File.Copy(Paths.SourceWarlockLibrary, Paths.DestinationWarlockLibrary);
            File.Copy(Paths.SourceWarriorLibrary, Paths.DestinationWarriorLibrary);
            File.Copy(Paths.SourceWarriorRotationLibrary, Paths.DestinationWarriorRotationLibrary);
            File.Copy(Paths.SourceZzukBotExecutable, Paths.DestinationZzukBotExecutable);
            Console.WriteLine("Copied all files to the temp directory");

            ZipFile.CreateFromDirectory(Paths.Temp, Paths.SourceZipFile);
            Console.WriteLine("Created a new archive");

            Directory.Delete(Paths.Temp, true);
            Console.WriteLine("Deleted the temp directory");

            File.Copy(Paths.SourceZipFile, Paths.DestinationZipFile);
            Console.WriteLine("Copied archive to wwwroot folder");


            Console.WriteLine("\nComplete!\n\nPress ENTER to continue");

            Console.Read();
        }
    }
}
