using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ZzukBot.Core.Utilities.Extensions;
using ZzukBot.Settings;

namespace ZzukBot.Core.Framework
{
    internal static class DependencyLoader
    {
        private static string _lastPath;

        internal static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name).Name.Trim('.');
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (name == "ZzukBot")
            {
                $"Dependency {name} got requested".Log(Logs.Injected, true);
                return executingAssembly;
            }
            var path = executingAssembly.ExtJumpUp(1);
            $"Dependency {name} got requested. Searching inside {path}".Log(Logs.Injected, true);
            foreach (var item in Directory.GetFiles(path))
            {
                var fileName = Path.GetFileName(item);
                var fileNameArr = fileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (fileNameArr.Count < 2) continue;
                if (fileNameArr.Last() != "dll") continue;
                fileNameArr.Remove("dll");
                fileName = fileNameArr.Aggregate((current, next) => current + "." + next).Trim('.');
                if (fileName.Trim('.') != name) continue;
                $"Resolved to assembly {item}".Log(Logs.Injected);
                return Assembly.LoadFrom(item);
            }
            $"Now searching inside {_lastPath}".Log(Logs.Injected, true);
            foreach (var item in Directory.GetFiles(_lastPath))
            {
                var fileName = Path.GetFileName(item);
                var fileNameArr = fileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (fileNameArr.Count < 2) continue;
                if (fileNameArr.Last() != "dll") continue;
                fileNameArr.Remove("dll");
                fileName = fileNameArr.Aggregate((current, next) => current + "." + next).Trim('.');
                if (fileName.Trim('.') != name) continue;
                $"Resolved to assembly {item}".Log(Logs.Injected);
                return Assembly.Load(File.ReadAllBytes(item));
            }
            return null;
        }

        internal static void SetPluginPath(string path)
        {
            _lastPath = path;
        }
    }
}
