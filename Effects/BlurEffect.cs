using System.Collections.Generic;

namespace ImageProcessingFramework.Effects
{
    [EffectParameter("Radius", typeof(int), 2, "Blur radius in pixels")]
    public class BlurEffect : IEffect
    {
        public string Name => "Blur";
        public string Description => "Applies Gaussian blur to the image";
        public Dictionary<string, object> Parameters { get; set; }

        public BlurEffect()
        {
            Parameters = new Dictionary<string, object>
            {
                { "Radius", 2 }
            };
        }

        public IImage Apply(IImage image)
        {
            Console.WriteLine($"Applying blur with radius {Parameters["Radius"]} to image '{image.Name}'");
            return image;
        }
    }
}