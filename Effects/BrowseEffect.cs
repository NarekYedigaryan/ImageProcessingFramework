using System.Collections.Generic;

namespace ImageProcessingFramework.Effects
{
    public class BrowseEffect : IEffect
    {
        public string Name => "Browse";
        public string Description => "Allows browsing for image files";
        public Dictionary<string, object> Parameters { get; set; }

        public BrowseEffect()
        {
            Parameters = new Dictionary<string, object>();
        }

        public IImage Apply(IImage image)
        {
            Console.WriteLine($"Browsing for image replacement for '{image.Name}'");
            return image;
        }
    }
}