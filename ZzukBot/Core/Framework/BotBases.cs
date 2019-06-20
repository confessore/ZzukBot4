using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Core.Framework.Interfaces;
using ZzukBot.Core.Mem;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Settings;

namespace ZzukBot.Core.Framework
{
    internal sealed class BotBases
    {
        private static readonly Lazy<BotBases> _instance = new Lazy<BotBases>(() => new BotBases());

        [ImportMany(typeof(IBotBase), AllowRecomposition = true)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<IBotBase> _botBases = null;

        private AggregateCatalog _catalog;
        private CompositionContainer _container;

        private BotBases()
        {
            Refresh();
        }

        internal static BotBases Instance => _instance.Value;
        internal List<IBotBase> Items => _botBases;

        internal void Refresh()
        {
            if (_botBases != null)
                foreach (var x in _botBases)
                {
                    $"BotBases: Disposing botbase {x.Name}".Log(Logs.Injected, true);
                    x.Stop();
                    x.Dispose();
                }
            if (_catalog != null)
            {
                $"BotBases: Disposing old catalog".Log(Logs.Injected, true);
                _catalog.Catalogs.Clear();
                _catalog?.Dispose();
            }
            _catalog = new AggregateCatalog();
            _botBases?.Clear();
            _container?.Dispose();
            Action<string> load = item =>
            {
                if (!item.EndsWith(".dll")) return;
                $"BotBases: Noting down {item} as possible botbase".Log(Logs.Injected, true);
                var dir = Path.GetDirectoryName(item);
                DependencyLoader.SetPluginPath(dir);
                var assembly = Assembly.Load(File.ReadAllBytes(item));
                _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            };
            foreach (var path in Directory.EnumerateDirectories(Paths.BotBases, "*", SearchOption.TopDirectoryOnly))
            foreach (var item in Directory.GetFiles(path))
                load(item);
            foreach (var item in Directory.GetFiles(Paths.BotBases))
                load(item);
            _container = new CompositionContainer(_catalog);
            $"BotBases: Composing catalogs".Log(Logs.Injected, true);
            _container.ComposeParts(this);
            var tmpList = new HashSet<string>();
            foreach (var x in _botBases)
            {
                var name = x.Name.ToLower();
                $"BotBases: Loaded {name} botbase".Log(Logs.Injected, true);
                if (tmpList.Contains(name))
                {
                    MessageBox.Show(
                        $"Names of BotBases must be unique however there are two or more bases with name {x.Name}");
                    Environment.Exit(0);
                }
                tmpList.Add(name);
            }
            Memory.HideAdditionalModules();
        }
    }
}
