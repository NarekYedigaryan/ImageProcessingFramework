using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageProcessingFramework
{
    public class ConfigPluginLoader : IPluginLoader
    {
        private const string ConfigFileName = "plugins.config.json";
        private readonly string _configFilePath;

        public ConfigPluginLoader(string configDirectory = null)
        {
            _configFilePath = Path.Combine(configDirectory ?? AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
            EnsureConfigFileExists();
        }

        public IEnumerable<IEffect> LoadPlugins()
        {
            var plugins = new List<IEffect>();

            try
            {
                var configContent = File.ReadAllText(_configFilePath);
                var config = JsonConvert.DeserializeObject<PluginConfig>(configContent);

                foreach (var pluginConfig in config.Plugins)
                {
                    var effect = CreateEffectFromConfig(pluginConfig);
                    if (effect != null)
                    {
                        plugins.Add(effect);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading plugins: {ex.Message}");
            }

            return plugins;
        }

        public void ReloadPlugins()
        {
            // then can add any code if needed
        }

        private IEffect CreateEffectFromConfig(PluginDefinition config)
        {
            try
            {
                var type = Type.GetType(config.Type);
                if (type == null || !typeof(IEffect).IsAssignableFrom(type))
                    return null;

                var effect = (IEffect)Activator.CreateInstance(type);

                if (config.Parameters != null)
                {
                    foreach (var param in config.Parameters)
                    {
                        if (effect.Parameters.ContainsKey(param.Key))
                        {
                            effect.Parameters[param.Key] = ConvertParameterValue(param.Value, effect.Parameters[param.Key]?.GetType());
                        }
                    }
                }

                return effect;
            }
            catch
            {
                return null;
            }
        }

        private object ConvertParameterValue(object value, Type targetType)
        {
            if (value == null) return null;
            if (targetType.IsInstanceOfType(value)) return value;

            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return value;
            }
        }

        private void EnsureConfigFileExists()
        {
            if (!File.Exists(_configFilePath))
            {
                var defaultConfig = new PluginConfig
                {
                    Plugins = new List<PluginDefinition>
                    {
                        new PluginDefinition
                        {
                            Name = "ResizeEffect",
                            Type = "ImageProcessingFramework.Effects.ResizeEffect, ImageProcessingFramework",
                            Parameters = new Dictionary<string, object>
                            {
                                { "Width", 100 },
                                { "Height", 100 }
                            }
                        },
                        new PluginDefinition
                        {
                            Name = "BlurEffect",
                            Type = "ImageProcessingFramework.Effects.BlurEffect, ImageProcessingFramework",
                            Parameters = new Dictionary<string, object>
                            {
                                { "Radius", 2 }
                            }
                        },
                        new PluginDefinition
                        {
                            Name = "GrayscaleEffect",
                            Type = "ImageProcessingFramework.Effects.GrayscaleEffect, ImageProcessingFramework"
                        },
                        new PluginDefinition
                        {
                            Name = "BrowseEffect",
                            Type = "ImageProcessingFramework.Effects.BrowseEffect, ImageProcessingFramework"
                        }
                    }
                };

                var json = JsonConvert.SerializeObject(defaultConfig, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_configFilePath, json);
            }
        }
    }

    public class PluginConfig
    {
        public List<PluginDefinition> Plugins { get; set; }
    }

    public class PluginDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}