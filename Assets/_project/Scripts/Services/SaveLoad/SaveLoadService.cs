using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using CodeBase.Configs;
using CodeBase.Core;
using CodeBase.Hero;
using Newtonsoft.Json;
using OfficeOpenXml;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private TextAsset _configFile;
        
        [Inject] private IAssetProvider _assetProvider;
        
        public void Save<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(typeof(T).Name, json);
        }

        public T Load<T>()
        {
            if (!PlayerPrefs.HasKey(typeof(T).Name))
            {
                return default;
            }

            var json = PlayerPrefs.GetString(typeof(T).Name);
            return JsonConvert.DeserializeObject<T>(json);
        }
        
       public Dictionary<CharacteristicType, CharacterStat> LoadStatsFromExcel()
       {
           _configFile ??=  Resources.Load("Configs/StatsConfigs.xlsx") as TextAsset;
            if (_configFile == null)
            {
                Debug.LogError("Файл конфигурации не найден");
                return null;
            }

            var stats = new Dictionary<CharacteristicType, CharacterStat>();

            using (var stream = new MemoryStream(_configFile.bytes))
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets["Stats"];

                    if (worksheet == null)
                    {
                        Debug.LogError("Лист 'Stats' не найден в файле");
                        return null;
                    }

                    // Читаем названия характеристик из первой строки
                    var statNames = new string[5];
                    for (int i = 0; i < 5; i++)
                    {
                        statNames[i] = worksheet.Cells[1, i * 2 + 2].Value?.ToString() ?? string.Empty;
                    }

                    // Читаем уровни характеристик
                    for (int i = 0; i < 5; i++)
                    {
                        var stat = new CharacterStat
                        {
                            Name = statNames[i],
                            Levels = new List<StatLevel>()
                        };

                        // Находим последний заполненный ряд для текущей характеристики
                        int lastRow = worksheet.Dimension.Rows;

                        for (int row = 2; row <= lastRow; row++)
                        {
                            if (worksheet.Cells[row, i * 2 + 2].Value == null)
                                break;

                            var level = new StatLevel
                            {
                                Level = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                Value = float.Parse(worksheet.Cells[row, i * 2 + 2].Value.ToString()),
                                Cost = Convert.ToInt32(worksheet.Cells[row, i * 2 + 3].Value),
                            };

                            stat.Levels.Add(level);
                        }

                        if (Enum.TryParse<CharacteristicType>(stat.Name, out var characteristic))
                        {
                            stats.Add(characteristic, stat);
                        }
                    }
                }
            }

            return stats;
        }
    }
}