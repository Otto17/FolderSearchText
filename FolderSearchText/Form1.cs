using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FolderSearchText
{
    internal partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;

        // Свойства для доступа к элементам управления
        internal ListView LstResults => lstResults;

        // Конструктор формы
        internal Form1()
        {
            InitializeComponent();
        }

        // Обработчик загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // Настройка ListView
            lstResults.FullRowSelect = true; // Включаем выделение всей строки
            lstResults.DoubleClick += LstResults_DoubleClick; // Подключаем обработчик двойного клика
            lstResults.KeyDown += LstResults_KeyDown; // Подключаем обработчик нажатия клавиш

            // Создаём контекстное меню
            var contextMenu = new ContextMenu(this);
            contextMenu.CreateContextMenu();

            // Настройка ComboBox для путей
            cmbFolderPath.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFolderPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFolderPath.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Настройка ComboBox для искомого текста
            cmbSearchText.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSearchText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSearchText.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Загружаем историю
            var history = new History(this);
            history.LoadHistory();

            // Проверяем наличие Notepad++ и делаем пункт меню активным/неактивным
            bool isNotepadPlusPlusInstalled = contextMenu.IsNotepadPlusPlusInstalled();
            if (lstResults.ContextMenuStrip != null)
            {
                foreach (ToolStripMenuItem item in lstResults.ContextMenuStrip.Items)
                {
                    if (item.Text == "Открыть в Notepad++")
                    {
                        item.Enabled = isNotepadPlusPlusInstalled;
                        break;
                    }
                }
            }
        }

        // Обработчик двойного клика по элементу ListView
        private void LstResults_DoubleClick(object sender, EventArgs e)
        {
            var contextMenu = new ContextMenu(this);
            contextMenu.OpenSelectedFolder();
        }

        // Обработчик нажатия клавиш в ListView
        private void LstResults_KeyDown(object sender, KeyEventArgs e)
        {
            var contextMenu = new ContextMenu(this);
            contextMenu.LstResults_KeyDown(sender, e);
        }

        // Обработчик нажатия кнопки "Найти" или "Стоп"
        private async void BtnSearchStop_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Найти")
            {
                // Начинаем поиск
                string root = cmbFolderPath.Text;
                string searchText = cmbSearchText.Text;

                if (string.IsNullOrEmpty(root) || string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Введите путь к папке и текст для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Очищаем предыдущие результаты
                lstResults.Items.Clear();

                // Отключаем элементы управления
                multithreading.Enabled = false;
                wholeWords.Enabled = false;
                cmbFolderPath.Enabled = false;
                cmbSearchText.Enabled = false;

                // Создаём токен для отмены поиска
                _cancellationTokenSource = new CancellationTokenSource();
                btnSearch.Text = "Стоп"; // Меняем текст кнопки на "Стоп"

                try
                {
                    // Запускаем поиск в отдельном потоке
                    await Task.Run(() => SearchFiles(root, searchText, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // Поиск был отменён, но сообщение не показываем
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выполнении поиска: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Восстанавливаем состояние элементов управления
                    btnSearch.Text = "Найти";
                    multithreading.Enabled = true;
                    wholeWords.Enabled = true;
                    cmbFolderPath.Enabled = true;
                    cmbSearchText.Enabled = true;
                }

                // Сохраняем текущий путь и искомый текст в историю
                var history = new History(this);
                history.SaveHistory(root, searchText);
            }
            else
            {
                // Останавливаем поиск
                _cancellationTokenSource?.Cancel();
            }
        }

        // Поиск файлов по заданному тексту с возможностью использования многопоточности
        private void SearchFiles(string root, string searchText, CancellationToken cancellationToken)
        {
            try
            {
                // Получаем все файлы из директории (рекурсивно)
                var files = GetFiles(root, "*", SearchOption.AllDirectories, cancellationToken);

                // Проверяем, включён ли режим многопоточности
                if (multithreading.Checked)
                {
                    // Параллельная обработка файлов
                    Parallel.ForEach(files, new ParallelOptions { CancellationToken = cancellationToken }, file =>
                    {
                        try
                        {
                            ProcessFile(file, searchText, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            // Логируем ошибку, но не прерываем выполнение
                            Console.WriteLine($"Ошибка при обработке файла {file}: {ex.Message}");
                        }
                    });
                }
                else
                {
                    // Однопоточная обработка файлов
                    foreach (var file in files)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        try
                        {
                            ProcessFile(file, searchText, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            // Логируем ошибку, но не прерываем выполнение
                            Console.WriteLine($"Ошибка при обработке файла {file}: {ex.Message}");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Поиск был отменён, но сообщение не показываем
            }
            catch (Exception ex)
            {
                // Логируем общую ошибку
                Console.WriteLine($"Ошибка при выполнении поиска: {ex.Message}");
            }
        }

        // Метод для обработки одного файла
        private void ProcessFile(string file, string searchText, CancellationToken cancellationToken)
        {
            // Пропускаем бинарные файлы и архивы
            if (IsBinaryOrArchive(file))
            {
                return;
            }

            // Определяем кодировку файла
            Encoding encoding = DetectEncoding(file);

            // Читаем файл построчно
            int lineNumber = 1;
            try
            {
                foreach (string line in File.ReadLines(file, encoding))
                {
                    // Проверяем, не был ли запрошен останов
                    cancellationToken.ThrowIfCancellationRequested();

                    bool isMatch = false;

                    if (wholeWords.Checked)
                    {
                        // Поиск по целым словам с использованием регулярного выражения
                        string pattern = $@"\b{Regex.Escape(searchText)}\b";
                        isMatch = Regex.IsMatch(line, pattern);
                    }
                    else
                    {
                        // Обычный поиск
                        isMatch = line.Contains(searchText);
                    }

                    if (isMatch)
                    {
                        // Добавляем результат в ListView
                        string[] row = { lineNumber.ToString(), file };
                        lstResults.Invoke((MethodInvoker)(() => lstResults.Items.Add(new ListViewItem(row))));
                    }

                    lineNumber++;
                }
            }
            catch (Exception)
            {
                // Пропускаем файл, если возникла ошибка при чтении
            }
        }

        // Получение списка файлов в указанной папке
        private IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption, CancellationToken cancellationToken)
        {
            // Получаем файлы в текущей папке
            foreach (string file in Directory.EnumerateFiles(path, searchPattern))
            {
                // Проверяем, не был ли запрошен останов
                cancellationToken.ThrowIfCancellationRequested();
                yield return file;
            }

            if (searchOption == SearchOption.AllDirectories)
            {
                // Рекурсивно обходим подпапки
                foreach (string directory in Directory.EnumerateDirectories(path))
                {
                    // Проверяем, не был ли запрошен останов
                    cancellationToken.ThrowIfCancellationRequested();

                    // Используем вспомогательный метод для обработки исключений
                    var files = GetFilesSafe(directory, searchPattern, searchOption, cancellationToken);
                    foreach (string file in files)
                    {
                        // Проверяем, не был ли запрошен останов
                        cancellationToken.ThrowIfCancellationRequested();
                        yield return file;
                    }
                }
            }
        }

        // Безопасное получение списка файлов с обработкой исключений
        private IEnumerable<string> GetFilesSafe(string path, string searchPattern, SearchOption searchOption, CancellationToken cancellationToken)
        {
            var files = new List<string>();

            try
            {
                // Проверяем, не был ли запрошен останов
                cancellationToken.ThrowIfCancellationRequested();

                // Рекурсивно получаем файлы в подпапках
                files.AddRange(Directory.EnumerateFiles(path, searchPattern));
                if (searchOption == SearchOption.AllDirectories)
                {
                    foreach (string directory in Directory.EnumerateDirectories(path))
                    {
                        // Проверяем, не был ли запрошен останов
                        cancellationToken.ThrowIfCancellationRequested();
                        files.AddRange(GetFilesSafe(directory, searchPattern, searchOption, cancellationToken));
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Пропускаем папку, если доступ запрещён
            }
            catch (Exception)
            {
                // Пропускаем папку, если возникла другая ошибка
            }

            return files;
        }

        // Проверка, является ли файл бинарным или архивом
        private bool IsBinaryOrArchive(string filePath)
        {
            // Проверяем расширение файла
            string ext = Path.GetExtension(filePath).ToLower();
            string[] binaryExtensions = { ".exe", ".dll", ".so", ".bin", ".zip", ".rar", ".tar", ".gz", ".7z", ".jar" };

            if (Array.Exists(binaryExtensions, e => e == ext))
            {
                return true;
            }

            // Проверяем размер файла
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length == 0)
            {
                return false; // Пустой файл считаем текстовым
            }

            // Проверяем первые несколько байт файла на наличие бинарных данных
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[Math.Min(512, fileInfo.Length)];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    for (int i = 0; i < bytesRead; i++)
                    {
                        if (buffer[i] == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // Если файл нельзя прочитать, считаем его бинарным
                return true;
            }

            return false;
        }

        // Определение кодировки файла
        private Encoding DetectEncoding(string filePath)
        {
            try
            {
                // Читаем первые несколько байт файла для определения кодировки
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    // Определяем кодировку
                    if (bytesRead >= 2 && buffer[0] == 0xFF && buffer[1] == 0xFE)
                    {
                        return Encoding.Unicode; // UTF-16 (LE)
                    }
                    else if (bytesRead >= 2 && buffer[0] == 0xFE && buffer[1] == 0xFF)
                    {
                        return Encoding.BigEndianUnicode; // UTF-16 (BE)
                    }
                    else if (bytesRead >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                    {
                        return Encoding.UTF8; // UTF-8 с BOM
                    }
                    else
                    {
                        // По умолчанию используем UTF-8 без BOM
                        return Encoding.UTF8;
                    }
                }
            }
            catch
            {
                // Возвращаем UTF-8 по умолчанию в случае ошибки
                return Encoding.UTF8;
            }
        }

        // Ссылка на страницу автора
        private void LinkLabelAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitflic.ru/project/otto/foldersearchtext");
        }
    }
}