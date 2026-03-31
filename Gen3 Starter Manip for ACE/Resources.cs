using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gen3_Starter_Manip_for_ACE
{
    public static class Resources
    {
        public static Types.WordDataType[] wordData;
        public static Types.WordDataType[] pokeWordData;
        public static Types.CorruptedPokemon[] corruptedPokemonData;
        public static void loadResources(string wordFilePath, string pokeWordFilePath)
        {
            if (File.Exists(wordFilePath))
            {
                string[] lines = File.ReadAllLines(wordFilePath);
                wordData = new Types.WordDataType[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 3)
                    {
                        ushort id;
                        if (ushort.TryParse(parts[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out id))
                        {
                            var word = new Types.WordDataType
                            {
                                ID = id,
                                Group = parts[1],
                                Text = parts[2]
                            };
                            wordData[i] = word;
                        }
                        else
                        {
                            MessageBox.Show("ワードリストのIDが無効です");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ワードリストに3つの要素が含まれていない行があります");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(wordFilePath + " が見つかりません");
            }
            if (File.Exists(pokeWordFilePath))
            {
                string[] lines = File.ReadAllLines(pokeWordFilePath);
                pokeWordData = new Types.WordDataType[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 3)
                    {
                        ushort id;
                        if (ushort.TryParse(parts[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out id))
                        {
                            var word = new Types.WordDataType
                            {
                                ID = id,
                                Group = parts[1],
                                Text = parts[2]
                            };
                            pokeWordData[i] = word;
                        }
                        else
                        {
                            MessageBox.Show("ワードリストのIDが無効です");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ワードリストに3つの要素が含まれていない行があります");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(pokeWordFilePath + " が見つかりません");
            }
        }
        public static void loadCorruptedPokemonData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                corruptedPokemonData = new Types.CorruptedPokemon[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 2)
                    {
                        try
                        {
                            ushort id = Convert.ToUInt16(parts[0], 16);
                            var poke = new Types.CorruptedPokemon
                            {
                                ID = id,
                                Pattern = parts[1]
                            };
                            corruptedPokemonData[i] = poke;

                        }
                        catch
                        {
                            MessageBox.Show("ワードリストのIDが無効です");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ワードリストに2つの要素が含まれていない行があります");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(filePath + " が見つかりません");
                return;
            }
        }
    }
}
