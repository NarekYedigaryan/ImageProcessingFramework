using System;
using System.Collections.Generic;
using System.Reflection;

namespace ImageProcessingFramework
{
    public interface IImage
    {
        Guid Id { get; }
        string Name { get; set; }
        byte[] ImageData { get; set; }
        List<IEffect> AppliedEffects { get; }
        void ApplyEffect(IEffect effect);
        IImage Clone();
    }
}