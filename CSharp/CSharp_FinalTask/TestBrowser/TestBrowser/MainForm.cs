using BinarySearchTreeLib;
using Services;
using FilterLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TestResultsBrowser
{
    public partial class MainForm : Form
    {
        private readonly StudentDataService studentDataService;
        private readonly List<StudentData> studentList;
        private Filter<StudentData> filter;
        private FilterService filterService;
        public MainForm(StudentDataService studentDataService, FilterService filterService)
        {
            InitializeComponent();
            this.studentDataService = studentDataService;
            this.filterService = filterService;
            this.filter = filterService.GetFilter();
            studentList = new List<StudentData>(studentDataService.GetStudentDatas().ToList());
            dgvTestResult.AutoGenerateColumns = false;

            dgvTestResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name"
            });
            dgvTestResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                HeaderText = "Last name"
            });
            dgvTestResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TestName",
                HeaderText = "Test"
            });
            dgvTestResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateOfTest",
                HeaderText = "Date of test"
            });

            dgvTestResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TestScore",
                HeaderText = "Score"
            });
            dgvTestResult.DataSource = filter.Apply(studentList);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            if(addForm.DialogResult == DialogResult.OK)
            {
                studentDataService.Add(addForm.StudentData);
                dgvTestResult.DataSource = studentDataService.GetStudentDatas().ToList();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm(filterService);
            filterForm.ShowDialog();
            if(filterForm.DialogResult == DialogResult.OK)
            {
                this.filter = filterForm.Filter.GetFilter();
                var filteredList = filter.Apply(studentList).ToList();
                dgvTestResult.DataSource = filteredList;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you shure?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                filterService.ResetFilter();
                dgvTestResult.DataSource = studentDataService.GetStudentDatas().ToList();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dialogResult = MessageBox.Show("Do you want to save changes?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                studentDataService.SaveStudentDatas();
                filterService.SaveFilter();
            }

            if(dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
