using System.Collections.Generic;

namespace ImageProcessingFramework
{
    public interface IPluginLoader
    {
        IEnumerable<IEffect> LoadPlugins();
        void ReloadPlugins();
    }
}