namespace FolderSearchText
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelFolderPath = new System.Windows.Forms.Label();
            this.labelSearchText = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wholeWords = new System.Windows.Forms.CheckBox();
            this.cmbFolderPath = new System.Windows.Forms.ComboBox();
            this.cmbSearchText = new System.Windows.Forms.ComboBox();
            this.linkLabelAuthor = new System.Windows.Forms.LinkLabel();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.multithreading = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelFolderPath
            // 
            this.labelFolderPath.AutoSize = true;
            this.labelFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFolderPath.Location = new System.Drawing.Point(9, 14);
            this.labelFolderPath.Name = "labelFolderPath";
            this.labelFolderPath.Size = new System.Drawing.Size(110, 20);
            this.labelFolderPath.TabIndex = 2;
            this.labelFolderPath.Text = "Путь к папке:";
            // 
            // labelSearchText
            // 
            this.labelSearchText.AutoSize = true;
            this.labelSearchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSearchText.Location = new System.Drawing.Point(9, 80);
            this.labelSearchText.Name = "labelSearchText";
            this.labelSearchText.Size = new System.Drawing.Size(155, 20);
            this.labelSearchText.TabIndex = 2;
            this.labelSearchText.Text = "Текста для поиска:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.Location = new System.Drawing.Point(9, 157);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(779, 38);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchStop_Click);
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstResults.HideSelection = false;
            this.lstResults.Location = new System.Drawing.Point(9, 217);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(779, 283);
            this.lstResults.TabIndex = 6;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "В строке";
            this.columnHeader1.Width = 58;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Путь к файлу";
            this.columnHeader2.Width = 700;
            // 
            // wholeWords
            // 
            this.wholeWords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wholeWords.AutoSize = true;
            this.wholeWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wholeWords.Location = new System.Drawing.Point(653, 115);
            this.wholeWords.Name = "wholeWords";
            this.wholeWords.Size = new System.Drawing.Size(128, 24);
            this.wholeWords.TabIndex = 7;
            this.wholeWords.Text = "Целые слова";
            this.wholeWords.UseVisualStyleBackColor = true;
            // 
            // cmbFolderPath
            // 
            this.cmbFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFolderPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFolderPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbFolderPath.FormattingEnabled = true;
            this.cmbFolderPath.Location = new System.Drawing.Point(9, 37);
            this.cmbFolderPath.Name = "cmbFolderPath";
            this.cmbFolderPath.Size = new System.Drawing.Size(779, 28);
            this.cmbFolderPath.TabIndex = 8;
            // 
            // cmbSearchText
            // 
            this.cmbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSearchText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSearchText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSearchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSearchText.FormattingEnabled = true;
            this.cmbSearchText.Location = new System.Drawing.Point(9, 103);
            this.cmbSearchText.Name = "cmbSearchText";
            this.cmbSearchText.Size = new System.Drawing.Size(630, 28);
            this.cmbSearchText.TabIndex = 9;
            // 
            // linkLabelAuthor
            // 
            this.linkLabelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelAuthor.AutoSize = true;
            this.linkLabelAuthor.Location = new System.Drawing.Point(103, 503);
            this.linkLabelAuthor.Name = "linkLabelAuthor";
            this.linkLabelAuthor.Size = new System.Drawing.Size(60, 13);
            this.linkLabelAuthor.TabIndex = 10;
            this.linkLabelAuthor.TabStop = true;
            this.linkLabelAuthor.Text = "Автор Otto";
            this.linkLabelAuthor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAuthor_LinkClicked);
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(6, 503);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(91, 13);
            this.labelAuthor.TabIndex = 11;
            this.labelAuthor.Text = "г. Омск 22.01.25";
            // 
            // multithreading
            // 
            this.multithreading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.multithreading.AutoSize = true;
            this.multithreading.Checked = true;
            this.multithreading.CheckState = System.Windows.Forms.CheckState.Checked;
            this.multithreading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.multithreading.Location = new System.Drawing.Point(653, 94);
            this.multithreading.Name = "multithreading";
            this.multithreading.Size = new System.Drawing.Size(135, 24);
            this.multithreading.TabIndex = 7;
            this.multithreading.Text = "Все ядра CPU";
            this.multithreading.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.linkLabelAuthor);
            this.Controls.Add(this.cmbSearchText);
            this.Controls.Add(this.cmbFolderPath);
            this.Controls.Add(this.multithreading);
            this.Controls.Add(this.wholeWords);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.labelSearchText);
            this.Controls.Add(this.labelFolderPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 561);
            this.Name = "Form1";
            this.Text = "Поиск текста по файлам в папках и подпапках (FolderSearchText)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFolderPath;
        private System.Windows.Forms.Label labelSearchText;
        private System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.CheckBox wholeWords;
        internal System.Windows.Forms.ComboBox cmbFolderPath;
        internal System.Windows.Forms.ComboBox cmbSearchText;
        private System.Windows.Forms.LinkLabel linkLabelAuthor;
        private System.Windows.Forms.Label labelAuthor;
        internal System.Windows.Forms.CheckBox multithreading;
    }
}

