using ImageProcessingFramework.Effects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessingFramework
{
    public class Image : IImage
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public List<IEffect> AppliedEffects { get; private set; }

        public Image(string name, byte[] imageData = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ImageData = imageData ?? new byte[0];
            AppliedEffects = new List<IEffect>();
        }

        public void ApplyEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException(nameof(effect));

            effect.Apply(this);

            AppliedEffects.Add(effect);
        }

        public IImage Clone()
        {
            var clonedImage = new Image(Name, ImageData?.ToArray());

            foreach (var effect in AppliedEffects)
            {
                IEffect clonedEffect = effect switch
                {
                    ResizeEffect resize => CloneResizeEffect(resize),
                    BlurEffect blur => CloneBlurEffect(blur),
                    GrayscaleEffect grayscale => new GrayscaleEffect(),
                    BrowseEffect browse => new BrowseEffect(),
                    _ => throw new NotSupportedException($"Effect type {effect.GetType().Name} is not supported for cloning")
                };

                clonedImage.AppliedEffects.Add(clonedEffect);
            }

            return clonedImage;
        }

        private IEffect CloneResizeEffect(ResizeEffect original)
        {
            var clone = new ResizeEffect();
            foreach (var param in original.Parameters)
            {
                clone.Parameters[param.Key] = param.Value;
            }
            return clone;
        }

        private IEffect CloneBlurEffect(BlurEffect original)
        {
            var clone = new BlurEffect();
            foreach (var param in original.Parameters)
            {
                clone.Parameters[param.Key] = param.Value;
            }
            return clone;
        }
    }
}