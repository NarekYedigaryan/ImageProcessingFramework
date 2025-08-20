using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessingFramework
{
    public class ImageProcessor
    {
        private readonly IPluginLoader _pluginLoader;
        private List<IEffect> _availableEffects;

        public ImageProcessor(IPluginLoader pluginLoader)
        {
            _pluginLoader = pluginLoader;
            LoadAvailableEffects();
        }

        public IEnumerable<IEffect> GetAvailableEffects()
        {
            return _availableEffects.AsReadOnly();
        }

        public void ProcessImages(List<ImageProcessingJob> jobs)
        {
            foreach (var job in jobs)
            {
                ProcessImage(job);
            }
        }

        public IImage ProcessImage(ImageProcessingJob job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            var result = job.Image.Clone();

            foreach (var effect in job.Effects)
            {
                try
                {
                    result.ApplyEffect(effect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error applying effect {effect.Name} to image {job.Image.Name}: {ex.Message}");
                }
            }

            return result;
        }

        public void ReloadPlugins()
        {
            _pluginLoader.ReloadPlugins();
            LoadAvailableEffects();
        }

        private void LoadAvailableEffects()
        {
            _availableEffects = _pluginLoader.LoadPlugins().ToList();
        }
    }

    public class ImageProcessingJob
    {
        public IImage Image { get; set; }
        public List<IEffect> Effects { get; set; }

        public ImageProcessingJob(IImage image, List<IEffect> effects)
        {
            Image = image;
            Effects = effects;
        }
    }
}