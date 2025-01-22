using System;
using System.IO;
using System.Windows.Forms;

namespace FolderSearchText
{
    internal class ContextMenu
    {
        private readonly Form1 _form;   // Поле для хранения ссылки на главную форму (Form1)

        // Конструктор класса ContextMenu
        internal ContextMenu(Form1 form)
        {
            _form = form;
        }

        // Создание контекстного меню для ListView
        internal void CreateContextMenu()
        {
            // Создаём контекстное меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Добавляем пункты меню
            ToolStripMenuItem openPathItem = new ToolStripMenuItem("Открыть путь");
            openPathItem.Click += OpenPathItem_Click; // Подключаем обработчик
            contextMenu.Items.Add(openPathItem);

            ToolStripMenuItem openFileItem = new ToolStripMenuItem("Открыть файл");
            openFileItem.Click += OpenFileItem_Click;
            contextMenu.Items.Add(openFileItem);

            ToolStripMenuItem openInNotepadPlusPlusItem = new ToolStripMenuItem("Открыть в Notepad++");
            openInNotepadPlusPlusItem.Click += OpenInNotepadPlusPlusItem_Click; // Подключаем обработчик
            contextMenu.Items.Add(openInNotepadPlusPlusItem);

            ToolStripMenuItem copyPathItem = new ToolStripMenuItem("Скопировать путь");
            copyPathItem.Click += CopyPathItem_Click;
            contextMenu.Items.Add(copyPathItem);

            ToolStripMenuItem deleteFileItem = new ToolStripMenuItem("Удалить файл");
            deleteFileItem.Click += DeleteFileItem_Click;
            contextMenu.Items.Add(deleteFileItem);

            // Привязываем контекстное меню к ListView
            _form.LstResults.ContextMenuStrip = contextMenu;
        }

        // Обработчик для пункта меню "Открыть путь"
        private void OpenPathItem_Click(object sender, EventArgs e)
        {
            OpenSelectedFolder();
        }

        // Обработчик для пункта меню "Скопировать путь"
        private void CopyPathItem_Click(object sender, EventArgs e)
        {
            CopySelectedFolderPath();
        }

        // Обработчик для пункта меню "Удалить файл"
        private void DeleteFileItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedFile();
        }

        // Обработчик нажатия горячих клавиш в ListView
        internal void LstResults_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка сочетания клавиш Ctrl+C
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedFolderPath();
            }
            // Обработка клавиши Delete
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedFile();
            }
        }

        // Открытие папки с выделенным файлом в проводнике
        internal void OpenSelectedFolder()
        {
            if (_form.LstResults.SelectedItems.Count > 0)
            {
                string filePath = _form.LstResults.SelectedItems[0].SubItems[1].Text; // Получаем путь к файлу (вторая колонка)
                string folderPath = Path.GetDirectoryName(filePath); // Извлекаем путь к папке

                if (!string.IsNullOrEmpty(folderPath))
                {
                    if (Directory.Exists(folderPath))
                    {
                        try
                        {
                            // Открываем папку в проводнике с выделенным файлом
                            System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{filePath}\"");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось открыть папку: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Папка не существует: {folderPath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь путь к папке.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Открытие файла с помощью программы по умолчанию
        private void OpenFileItem_Click(object sender, EventArgs e)
        {
            if (_form.LstResults.SelectedItems.Count > 0)
            {
                string filePath = _form.LstResults.SelectedItems[0].SubItems[1].Text; // Получаем путь к файлу (вторая колонка)

                if (!string.IsNullOrEmpty(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            // Открываем файл с помощью программы по умолчанию
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = filePath,
                                UseShellExecute = true // Используем оболочку системы для открытия файла
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось открыть файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Файл не существует: {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь путь к файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Открытие файла в Notepad++
        private void OpenInNotepadPlusPlusItem_Click(object sender, EventArgs e)
        {
            if (_form.LstResults.SelectedItems.Count > 0)
            {
                string filePath = _form.LstResults.SelectedItems[0].SubItems[1].Text; // Получаем путь к файлу (вторая колонка)

                if (!string.IsNullOrEmpty(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            // Путь к Notepad++ (по умолчанию)
                            string notepadPlusPlusPath = @"C:\Program Files\Notepad++\notepad++.exe";

                            if (File.Exists(notepadPlusPlusPath))
                            {
                                // Открываем файл в Notepad++
                                System.Diagnostics.Process.Start(notepadPlusPlusPath, $"\"{filePath}\"");
                            }
                            else
                            {
                                MessageBox.Show("Notepad++ не найден. Убедитесь, что программа установлена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось открыть файл в Notepad++: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Файл не существует: {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь путь к файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Проверка наличия Notepad++ в системе
        internal bool IsNotepadPlusPlusInstalled()
        {
            // Пути, где может быть установлен Notepad++
            string[] possiblePaths =
            {
                @"C:\Program Files\Notepad++\notepad++.exe",
                 @"C:\Program Files (x86)\Notepad++\notepad++.exe"
            };

            // Проверяем каждый путь
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return true; // Notepad++ найден
                }
            }

            return false; // Notepad++ не найден
        }

        // Копирование полного пути к файлу в буфер обмена
        private void CopySelectedFolderPath()
        {
            if (_form.LstResults.SelectedItems.Count > 0)
            {
                string filePath = _form.LstResults.SelectedItems[0].SubItems[1].Text;

                if (!string.IsNullOrEmpty(filePath))
                {
                    int retryCount = 3; // Количество попыток
                    int delay = 100; // Задержка между попытками (в миллисекундах)

                    for (int i = 0; i < retryCount; i++)
                    {
                        try
                        {
                            Clipboard.SetText(filePath);
                            return; // Успешно скопировано, выходим из метода
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            if (i < retryCount - 1) // Если это не последняя попытка
                            {
                                System.Threading.Thread.Sleep(delay); // Ждём перед следующей попыткой
                            }
                        }
                        catch (Exception)
                        {
                            // Игнорируем другие исключения
                            return;
                        }
                    }
                }
            }
        }

        // Удаление выбранного файла
        private void DeleteSelectedFile()
        {
            if (_form.LstResults.SelectedItems.Count > 0)
            {
                string filePath = _form.LstResults.SelectedItems[0].SubItems[1].Text; // Получаем путь к файлу (вторая колонка)

                if (File.Exists(filePath))
                {
                    DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить файл?\n{filePath}", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Сбрасываем атрибут "Только для чтения", если он установлен
                            FileAttributes attributes = File.GetAttributes(filePath);
                            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                            }

                            // Удаляем файл
                            File.Delete(filePath);

                            // Удаляем строку из ListView
                            _form.LstResults.Items.Remove(_form.LstResults.SelectedItems[0]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось удалить файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Файл не существует: {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
