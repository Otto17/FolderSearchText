using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSearchText
{
    internal class History
    {
        private const string HistoryFileName = "history_FolderSearchText.txt"; // Имя файла для хранения истории в Temp пользователя
        private const int MaxHistoryItems = 20; // Максимальное количество элементов в истории

        private readonly Form1 _form; // Поле для хранения ссылки на главную форму (Form1)

        // Конструктор класса History
        internal History(Form1 form)
        {
            _form = form;
        }

        // Загрузка истории поиска из файла временной папки пользователя (Temp)
        internal void LoadHistory()
        {
            string historyFilePath = Path.Combine(Path.GetTempPath(), HistoryFileName);

            if (File.Exists(historyFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(historyFilePath).ToList();
                    var paths = new List<string>();
                    var searchTexts = new List<string>();
                    bool isPathsSection = false;
                    bool isSearchTextsSection = false;
                    bool isConfigSection = false;

                    foreach (var line in lines)
                    {
                        if (line == "[Paths]")
                        {
                            isPathsSection = true;
                            isSearchTextsSection = false;
                            isConfigSection = false;
                        }
                        else if (line == "[SearchTexts]")
                        {
                            isPathsSection = false;
                            isSearchTextsSection = true;
                            isConfigSection = false;
                        }
                        else if (line == "[Config]")
                        {
                            isPathsSection = false;
                            isSearchTextsSection = false;
                            isConfigSection = true;
                        }
                        else if (isPathsSection)
                        {
                            paths.Add(line);
                        }
                        else if (isSearchTextsSection)
                        {
                            searchTexts.Add(line);
                        }
                        else if (isConfigSection)
                        {
                            // Загружаем состояния чекбоксов
                            var configParts = line.Split('=');
                            if (configParts.Length == 2)
                            {
                                string key = configParts[0].Trim();
                                string value = configParts[1].Trim();

                                if (key == "multithreading")
                                {
                                    _form.multithreading.Checked = bool.Parse(value);
                                }
                                else if (key == "wholeWords")
                                {
                                    _form.wholeWords.Checked = bool.Parse(value);
                                }
                            }
                        }
                    }

                    // Загружаем пути
                    _form.cmbFolderPath.Items.Clear();
                    _form.cmbFolderPath.Items.AddRange(paths.Take(MaxHistoryItems).ToArray());

                    // Загружаем искомые тексты
                    _form.cmbSearchText.Items.Clear();
                    _form.cmbSearchText.Items.AddRange(searchTexts.Take(MaxHistoryItems).ToArray());
                }
                catch
                {
                    // Игнорируем ошибку загрузки истории
                }
            }
        }

        // Сохранение истории поиска в файл временной папки пользователя (Temp)
        internal void SaveHistory(string path, string searchText)
        {
            string historyFilePath = Path.Combine(Path.GetTempPath(), HistoryFileName);

            try
            {
                var paths = new List<string>();
                var searchTexts = new List<string>();
                var config = new List<string>();

                if (File.Exists(historyFilePath))
                {
                    var lines = File.ReadAllLines(historyFilePath).ToList();
                    bool isPathsSection = false;
                    bool isSearchTextsSection = false;
                    bool isConfigSection = false;

                    foreach (var line in lines)
                    {
                        if (line == "[Paths]")
                        {
                            isPathsSection = true;
                            isSearchTextsSection = false;
                            isConfigSection = false;
                        }
                        else if (line == "[SearchTexts]")
                        {
                            isPathsSection = false;
                            isSearchTextsSection = true;
                            isConfigSection = false;
                        }
                        else if (line == "[Config]")
                        {
                            isPathsSection = false;
                            isSearchTextsSection = false;
                            isConfigSection = true;
                        }
                        else if (isPathsSection)
                        {
                            paths.Add(line);
                        }
                        else if (isSearchTextsSection)
                        {
                            searchTexts.Add(line);
                        }
                        else if (isConfigSection)
                        {
                            config.Add(line);
                        }
                    }
                }

                // Добавляем новый путь и искомый текст
                paths.RemoveAll(p => p.Equals(path, StringComparison.OrdinalIgnoreCase));
                paths.Insert(0, path);

                searchTexts.RemoveAll(s => s.Equals(searchText, StringComparison.OrdinalIgnoreCase));
                searchTexts.Insert(0, searchText);

                // Сохраняем состояния чекбоксов
                config.Clear();
                config.Add($"multithreading={_form.multithreading.Checked}");
                config.Add($"wholeWords={_form.wholeWords.Checked}");

                // Ограничиваем количество элементов в истории
                if (paths.Count > MaxHistoryItems)
                {
                    paths = paths.Take(MaxHistoryItems).ToList();
                }

                if (searchTexts.Count > MaxHistoryItems)
                {
                    searchTexts = searchTexts.Take(MaxHistoryItems).ToList();
                }

                // Сохраняем историю в файл
                var linesToSave = new List<string>
                {
                    "[Paths]"
                };
                linesToSave.AddRange(paths);
                linesToSave.Add("[SearchTexts]");
                linesToSave.AddRange(searchTexts);
                linesToSave.Add("[Config]");
                linesToSave.AddRange(config);

                File.WriteAllLines(historyFilePath, linesToSave);

                // Обновляем ComboBox
                _form.cmbFolderPath.Items.Clear();
                _form.cmbFolderPath.Items.AddRange(paths.ToArray());

                _form.cmbSearchText.Items.Clear();
                _form.cmbSearchText.Items.AddRange(searchTexts.ToArray());
            }
            catch
            {
                // Игнорируем ошибку сохранения истории
            }
        }
    }
}