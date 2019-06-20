using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Core.Framework.Classes;
using ZzukBot.Core.Mem;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Settings;

namespace ZzukBot.Core.Framework
{
    /// <summary>
    /// CustomClass management useable in all kind of botbases
    /// </summary>
    public sealed class CustomClasses
    {
        private static readonly Lazy<CustomClasses> _instance = new Lazy<CustomClasses>(() => new CustomClasses());
        /// <summary>
        /// Access to the CustomClass instance
        /// </summary>
        public static CustomClasses Instance => _instance.Value;

        [ImportMany(typeof(CustomClass), AllowRecomposition = true)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<CustomClass> _customClasses = null;

        private AggregateCatalog _catalog;
        private CompositionContainer _container;

        /// <summary>
        /// List of all CustomClasses currently loaded
        /// </summary>
        public IReadOnlyCollection<CustomClass> Enumerator => _customClasses.AsReadOnly();

        /// <summary>
        /// The CustomClass which is currently selected for usage
        /// </summary>
        public CustomClass Current { get; private set; }

        private CustomClasses()
        {
            Refresh();
        }
        /// <summary>
        /// Loads all CustomClasses from CustomClasses folder
        /// </summary>
        public void Refresh()
        {
            if (_customClasses != null)
                foreach (var x in _customClasses)
                {
                    $"CustomClasses: Disposing CC {x.Name}".Log(Logs.Injected, true);
                    x.Dispose();
                }
            if (_catalog != null)
            {
                $"CustomClasses: Disposing old catalog".Log(Logs.Injected, true);
                _catalog.Catalogs.Clear();
                _catalog?.Dispose();
            }
            _catalog = new AggregateCatalog();
            _customClasses?.Clear();
            _container?.Dispose();
            Action<string> load = item =>
            {
                if (!item.EndsWith(".dll")) return;
                $"CustomClasses: Noting down {item} as possible CustomClass".Log(Logs.Injected, true);
                var dir = Path.GetDirectoryName(item);
                DependencyLoader.SetPluginPath(dir);
                var assembly = Assembly.Load(File.ReadAllBytes(item));
                _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            };
            foreach (var path in Directory.EnumerateDirectories(Paths.CustomClasses, "*", SearchOption.TopDirectoryOnly))
                foreach (var item in Directory.GetFiles(path))
                    load(item);
            foreach (var item in Directory.GetFiles(Paths.CustomClasses))
                load(item);
            _container = new CompositionContainer(_catalog);
            $"CustomClasses: Composing catalogs".Log(Logs.Injected, true);
            _container.ComposeParts(this);
            var tmpList = new HashSet<string>();
            foreach (var x in _customClasses)
            {
                var name = x.Name.ToLower();
                $"CustomClasses: Loaded {name} botbase".Log(Logs.Injected, true);
                if (tmpList.Contains(name))
                {
                    MessageBox.Show(
                        $"Name of CustomClasses must be unique however there are two or more bases with name {x.Name}");
                    Environment.Exit(0);
                }
                tmpList.Add(name);
            }
            Memory.HideAdditionalModules();
        }

        /// <summary>
        /// Set a CC of the list as current CC
        /// </summary>
        public void SetCurrent(int parIndex)
        {
            Current = Enumerator.ElementAt(parIndex);
        }
    }
}