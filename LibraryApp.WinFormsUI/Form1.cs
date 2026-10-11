using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryApp.Core;

namespace LibraryApp.WinFormsUI
{
    public partial class Form1 : Form
    {
        private Logic _logic = new Logic();
        private DataGridView dataGridView;
        private TextBox txtTitle, txtAuthor, txtGenre, txtSearchAuthor;
        private Button btnAdd, btnUpdate, btnDelete, btnSearch, btnGroup;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
            RefreshGrid();
        }

        private void SetupUI()
        {
            this.Text = "Библиотека (WinForms)";
            this.Size = new Size(800, 600);

            dataGridView = new DataGridView();
            dataGridView.Location = new Point(10, 10);
            dataGridView.Size = new Size(760, 300);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.Click += DataGridView_Click;
            this.Controls.Add(dataGridView);

            Label lblTitle = new Label { Text = "Название:", Location = new Point(10, 330), Width = 70 };
            txtTitle = new TextBox { Location = new Point(80, 327), Width = 150 };
            Label lblAuthor = new Label { Text = "Автор:", Location = new Point(250, 330), Width = 50 };
            txtAuthor = new TextBox { Location = new Point(300, 327), Width = 150 };
            Label lblGenre = new Label { Text = "Жанр:", Location = new Point(470, 330), Width = 50 };
            txtGenre = new TextBox { Location = new Point(520, 327), Width = 150 };

            this.Controls.Add(lblTitle); this.Controls.Add(txtTitle);
            this.Controls.Add(lblAuthor); this.Controls.Add(txtAuthor);
            this.Controls.Add(lblGenre); this.Controls.Add(txtGenre);

            btnAdd = new Button { Text = "Добавить", Location = new Point(10, 370), Width = 100 };
            btnAdd.Click += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    _logic.CreateBook(new Book { Title = txtTitle.Text, Author = txtAuthor.Text, Genre = txtGenre.Text });
                    ClearInputs(); RefreshGrid();
                }
            };

            btnUpdate = new Button { Text = "Обновить", Location = new Point(120, 370), Width = 100 };
            btnUpdate.Click += (s, e) => {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var b = (Book)dataGridView.SelectedRows[0].DataBoundItem;
                    b.Title = txtTitle.Text; b.Author = txtAuthor.Text; b.Genre = txtGenre.Text;
                    _logic.UpdateBook(b); ClearInputs(); RefreshGrid();
                }
            };

            btnDelete = new Button { Text = "Удалить", Location = new Point(230, 370), Width = 100 };
            btnDelete.Click += (s, e) => {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var b = (Book)dataGridView.SelectedRows[0].DataBoundItem;
                    _logic.DeleteBook(b.Id); ClearInputs(); RefreshGrid();
                }
            };

            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnDelete);

            Label lblSearch = new Label { Text = "Поиск по автору:", Location = new Point(10, 420), Width = 110 };
            txtSearchAuthor = new TextBox { Location = new Point(120, 417), Width = 150 };
            btnSearch = new Button { Text = "Найти", Location = new Point(280, 415), Width = 100 };
            btnSearch.Click += (s, e) => {
                dataGridView.DataSource = null;
                dataGridView.DataSource = _logic.GetBooksByAuthor(txtSearchAuthor.Text);
            };

            btnGroup = new Button { Text = "Группировать по жанрам", Location = new Point(390, 415), Width = 180 };
            btnGroup.Click += (s, e) => {
                var groups = _logic.GroupBooksByGenre();
                string result = "Группировка по жанрам:\n\n";
                foreach (var g in groups)
                {
                    result += $"Жанр: {g.Key}\n";
                    foreach (var b in g.Value) result += $"  - {b.Title} ({b.Author})\n";
                    result += "\n";
                }
                MessageBox.Show(result, "Результат");
            };

            this.Controls.Add(lblSearch); this.Controls.Add(txtSearchAuthor);
            this.Controls.Add(btnSearch); this.Controls.Add(btnGroup);
        }

        private void RefreshGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _logic.ReadAllBooks();
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var b = (Book)dataGridView.SelectedRows[0].DataBoundItem;
                txtTitle.Text = b.Title; txtAuthor.Text = b.Author; txtGenre.Text = b.Genre;
            }
        }

        private void ClearInputs()
        {
            txtTitle.Clear(); txtAuthor.Clear(); txtGenre.Clear();
        }
    }
}