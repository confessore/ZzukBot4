using Harvester.Engine;
using Harvester.Engine.Loaders;
using Harvester.Engine.Loaders.Profile;
using Harvester.Engine.Modules;
using Harvester.GUI;
using System;
using System.ComponentModel.Composition;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Harvester
{
    [Export(typeof(IBotBase))]
    public class Harvester : IBotBase
    {
        DependencyMap dm = new DependencyMap();

        public Harvester()
        {
            dm.Add(this);
            dm.Add(CustomClasses.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(Skills.Instance);
            dm.Add(Spell.Instance);
            dm.Add(new Loader());
            dm.Add(new CCLoader(dm.Get<CustomClasses>()));
            dm.Add(new ProfileLoader(dm.Get<Loader>()));
            dm.Add(new CMD(dm.Get<ProfileLoader>()));
            dm.Add(new ConsumablesModule(dm.Get<Inventory>(), dm.Get<ObjectManager>()));
            dm.Add(new PathModule(dm.Get<Navigation>(), dm.Get<ObjectManager>(), dm.Get<ProfileLoader>()));
            dm.Add(new CombatModule(dm.Get<CustomClasses>(), dm.Get<ObjectManager>(), dm.Get<PathModule>()));
            dm.Add(new NodeScanModule(dm.Get<CMD>(), dm.Get<ObjectManager>(), dm.Get<Skills>()));
            dm.Add(new Flow(dm.Get<CMD>(), dm.Get<CombatModule>(), dm.Get<ConsumablesModule>(), 
                dm.Get<Inventory>(), dm.Get<Lua>(), dm.Get<NodeScanModule>(), 
                dm.Get<ObjectManager>(), dm.Get<PathModule>(), dm.Get<Spell>()));
            dm.Add(new Controller(dm.Get<Flow>(), dm.Get<Inventory>(), dm.Get<ObjectManager>(), 
                dm.Get<PathModule>()));
            dm.Add(new Manager(dm.Get<CCLoader>(), dm.Get<Controller>(), dm.Get<ObjectManager>(), 
                dm.Get<ProfileLoader>()));
        }

        public string Author { get; } = "krycess";
        public string Name { get; } = "Harvester";
        public Version Version { get; } = new Version(2, 0, 11, 64);

        public void ShowGui()
        {
            CMD settings = dm.Get<CMD>();

            if (!settings.Visible)
                settings.Show();
        }

        public bool Start(Action stopCallback) => dm.Get<Manager>().Start(stopCallback);
        public void Stop() => dm.Get<Manager>().Stop();
        public void Dispose() => dm.Get<Manager>().Dispose();
        public void PauseBotbase(Action onPauseCallback) => dm.Get<Manager>().Pause();
        public bool ResumeBotbase() => dm.Get<Manager>().Resume();
    }
}
