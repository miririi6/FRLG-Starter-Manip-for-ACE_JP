using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Gen3_Starter_Manip_for_ACE.Types;

namespace Gen3_Starter_Manip_for_ACE
{
    public class ConfigData
    {
        private static ConfigData _instance = new ConfigData();
        // プロパティにするだけで () が不要になり、変数っぽく扱える
        public static ConfigData Instance => _instance;
        public ConfigData()
        {
            version = RomVersionType.LeafGreen;
            checkedNatures = new List<NatureType> { NatureType.Modest, NatureType.Mild, NatureType.Rash };
            starter = StarterPokemonType.Squirtle;
            minExp = 0;
            maxExp = 65536;
            requiredHIV = 0;
            requiredAIV = new int[3];
            requiredBIV = new int[3];
            requiredCIV = new int[3];
            requiredDIV = new int[3];
            requiredSIV = new int[3];
            searchArea = new Rectangle(0, 0, 100, 100);
            scanThreshold = 0.8;
        }
        public static void SetInstance(ConfigData newConfig)
        {
            if (newConfig != null) _instance = newConfig;
        }
        public RomVersionType version { get; set; }
        public List<NatureType> checkedNatures { get; set; }
        public StarterPokemonType starter { get; set; }
        public SeedPokemonType seedPokemon { get; set; }
        public bool isSearchForACE { get; set; }
        public int minExp { get; set; }
        public int maxExp { get; set; }
        public int minFrame { get; set; }
        public int maxFrame { get; set; }
        public int requiredHIV { get; set; }
        public int[] requiredAIV { get; set; }
        public int[] requiredBIV { get; set; }
        public int[] requiredCIV { get; set; }
        public int[] requiredDIV { get; set; }
        public int[] requiredSIV { get; set; }
        public Rectangle searchArea { get; set; }
        public double scanThreshold { get; set; } = 0.9;
        public double waitTime { get; set; } = 0.5;
        public string scanWindowTitle { get; set; }
        public bool isAutoConnectTimer { get; set; } = false;
    }
    public static class ConfigUtils
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IgnoreReadOnlyProperties = true,
            Converters = { new JsonStringEnumConverter() }
        };
        public static void saveConfigData(string filePath)
        {
            string json = JsonSerializer.Serialize(ConfigData.Instance, _options);
            File.WriteAllText(filePath, json);
        }
        public static void loadConfigData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("コンフィグファイルが見つかりません。\nデフォルトの設定を使用します。\nポケモン名のメールワードが検索できないのでご注意ください。");
                return;
            }
            string json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<ConfigData>(json, _options);
            ConfigData.SetInstance(config);
        }
    }
}