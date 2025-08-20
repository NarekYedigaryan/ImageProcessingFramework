using System.Collections.Generic;

namespace ImageProcessingFramework.Effects
{
    [EffectParameter("Width", typeof(int), 100, "Target width in pixels")]
    [EffectParameter("Height", typeof(int), 100, "Target height in pixels")]
    public class ResizeEffect : IEffect
    {
        public string Name => "Resize";
        public string Description => "Resizes the image to specified dimensions";
        public Dictionary<string, object> Parameters { get; set; }

        public ResizeEffect()
        {
            Parameters = new Dictionary<string, object>
            {
                { "Width", 100 },
                { "Height", 100 }
            };
        }

        public IImage Apply(IImage image)
        {
            Console.WriteLine($"Resizing image '{image.Name}' to {Parameters["Width"]}x{Parameters["Height"]}");
            return image;
        }
    }
}