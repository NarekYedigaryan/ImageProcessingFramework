using System;
using System.Collections.Generic;
using System.Linq;
using ImageProcessingFramework;
using ImageProcessingFramework.Effects;

namespace ImageProcessingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Image Processing Framework Demo");
            Console.WriteLine("===============================\n");

            var pluginLoader = new ConfigPluginLoader();
            var processor = new ImageProcessor(pluginLoader);

            var images = new List<IImage>
            {
                new Image("Image1", new byte[1024]),
                new Image("Image2", new byte[2048]),
                new Image("Image3", new byte[4096])
            };

            var availableEffects = processor.GetAvailableEffects().ToList();
            Console.WriteLine("Available Effects:");
            foreach (var effect in availableEffects)
            {
                Console.WriteLine($"- {effect.Name}: {effect.Description}");
                if (effect.Parameters.Any())
                {
                    Console.WriteLine("  Parameters:");
                    foreach (var param in effect.Parameters)
                    {
                        Console.WriteLine($"    {param.Key}: {param.Value}");
                    }
                }
            }
            Console.WriteLine();

            var jobs = new List<ImageProcessingJob>
            {
                new ImageProcessingJob(
                    images[0],
                    new List<IEffect>
                    {
                        CreateResizeEffect(100, 100),
                        CreateBlurEffect(2)
                    }
                ),
                new ImageProcessingJob(
                    images[1],
                    new List<IEffect>
                    {
                        CreateResizeEffect(100, 100)
                    }
                ),
                new ImageProcessingJob(
                    images[2],
                    new List<IEffect>
                    {
                        CreateResizeEffect(150, 150),
                        CreateBlurEffect(5),
                        CreateGrayscaleEffect()
                    }
                )
            };

            Console.WriteLine("Processing images...\n");
            processor.ProcessImages(jobs);

            Console.WriteLine("\nProcessing completed!");

            Console.WriteLine("\nApplied Effects Summary:");
            foreach (var image in images)
            {
                Console.WriteLine($"\n{image.Name}:");
                foreach (var effect in image.AppliedEffects)
                {
                    if (effect is ResizeEffect resize)
                    {
                        Console.WriteLine($"- Resize: {resize.Parameters["Width"]}x{resize.Parameters["Height"]}");
                    }
                    else if (effect is BlurEffect blur)
                    {
                        Console.WriteLine($"- Blur: Radius {blur.Parameters["Radius"]}");
                    }
                    else
                    {
                        Console.WriteLine($"- {effect.Name}");
                    }
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static IEffect CreateResizeEffect(int width, int height)
        {
            var effect = new ResizeEffect();
            effect.Parameters["Width"] = width;
            effect.Parameters["Height"] = height;
            return effect;
        }

        private static IEffect CreateBlurEffect(int radius)
        {
            var effect = new BlurEffect();
            effect.Parameters["Radius"] = radius;
            return effect;
        }

        private static IEffect CreateGrayscaleEffect()
        {
            return new GrayscaleEffect();
        }

        private static IEffect CreateBrowseEffect()
        {
            return new BrowseEffect();
        }
    }
}