using System.Collections.Generic;

namespace ImageProcessingFramework.Effects
{
    public class GrayscaleEffect : IEffect
    {
        public string Name => "Grayscale";
        public string Description => "Converts image to grayscale";
        public Dictionary<string, object> Parameters { get; set; }

        public GrayscaleEffect()
        {
            Parameters = new Dictionary<string, object>();
        }

        public IImage Apply(IImage image)
        {
            Console.WriteLine($"Converting image '{image.Name}' to grayscale");
            return image;
        }
    }
}