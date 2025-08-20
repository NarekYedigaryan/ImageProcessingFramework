using System.Collections.Generic;

namespace ImageProcessingFramework
{
    public interface IEffect
    {
        string Name { get; }
        string Description { get; }
        Dictionary<string, object> Parameters { get; set; }
        IImage Apply(IImage image);
    }
}