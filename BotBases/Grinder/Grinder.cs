using Grinder.Engine;
using Grinder.Engine.Modules;
using Grinder.GUI;
using System;
using System.ComponentModel.Composition;
using ZzukBot.Core.Framework;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Framework.Loaders;
using ZzukBot.Core.Game.Statics;
using ZzukBot.Core.Utilities.DependencyInjection;

namespace Grinder
{
    [Export(typeof(IBotBase))]
    public class Grinder : IBotBase
    {
        DependencyMap dm = new DependencyMap();

        public Grinder()
        {
            dm.Add(this);
            dm.Add(Common.Instance);
            dm.Add(CustomClasses.Instance);
            dm.Add(Inventory.Instance);
            dm.Add(Lua.Instance);
            dm.Add(Navigation.Instance);
            dm.Add(ObjectManager.Instance);
            dm.Add(ProfileLoader.Instance);
            dm.Add(Skills.Instance);
            dm.Add(Spell.Instance);
            dm.Add(CCLoader.Instance);
            dm.Add(new CMD(
                dm.Get<Common>(),
                dm.Get<ProfileLoader>()));
            dm.Add(new ConsumablesModule(
                dm.Get<Inventory>(),
                dm.Get<ObjectManager>()));
            dm.Add(new PathModule(
                dm.Get<Navigation>(),
                dm.Get<ObjectManager>(),
                dm.Get<ProfileLoader>()));
            dm.Add(new CombatModule(
                dm.Get<CustomClasses>(),
                dm.Get<ObjectManager>(),
                dm.Get<PathModule>()));
            dm.Add(new NPCScanModule(
                dm.Get<ObjectManager>(),
                dm.Get<PathModule>(),
                dm.Get<ProfileLoader>(),
                dm.Get<Skills>()));
            dm.Add(new MerchantModule(
                dm.Get<Inventory>(),
                dm.Get<ObjectManager>(),
                dm.Get<PathModule>()));
            dm.Add(new Flow(
                dm.Get<CMD>(),
                dm.Get<CombatModule>(),
                dm.Get<ConsumablesModule>(), 
                dm.Get<Inventory>(),
                dm.Get<Lua>(),
                dm.Get<MerchantModule>(),
                dm.Get<NPCScanModule>(),
                dm.Get<ObjectManager>(),
                dm.Get<PathModule>(),
                dm.Get<Spell>()));
            dm.Add(new Controller(
                dm.Get<Flow>(),
                dm.Get<Inventory>(),
                dm.Get<ObjectManager>(), 
                dm.Get<PathModule>()));
            dm.Add(new Manager(
                dm.Get<CCLoader>(),
                dm.Get<Controller>(),
                dm.Get<ObjectManager>(), 
                dm.Get<ProfileLoader>()));
        }

        public string Author { get; } = "krycess";
        public string Name { get; } = "Grinder";
        public Version Version { get; } = new Version(4, 0, 0, 1812);
        public void ShowGui()
        {
            CMD settings = dm.Get<CMD>();

            if (!settings.Visible)
                settings.Show();
            else
                settings.Hide();
        }
        public bool Start(Action stopCallback) => dm.Get<Manager>().Start(stopCallback);
        public void Stop() => dm.Get<Manager>().Stop();
        public void Dispose() => dm.Get<Manager>().Dispose();
        public void PauseBotbase(Action onPauseCallback) => dm.Get<Manager>().Pause();
        public bool ResumeBotbase() => dm.Get<Manager>().Resume();
    }
}
