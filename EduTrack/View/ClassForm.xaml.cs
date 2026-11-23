using System;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>Create a class, or edit the one that is open.</summary>
    public partial class ClassForm : UserControl
    {
        private readonly ClassSummary _editing;

        public event Action<int> Saved;
        public event Action Cancelled;

        public ClassForm(ClassSummary editing)
        {
            InitializeComponent();
            _editing = editing;
            if (editing == null)
            {
                datePickerCreateDate.SelectedDate = DateTime.Today;
            }
            else
            {
                FormTitle.Text = Loc.T("Edit class");
                txtClassName.Text = editing.ClassName;
                txtMajor.Text = editing.Major;
                txtDescription.Text = editing.Description;
                datePickerCreateDate.SelectedDate = editing.Created;
            }
            Loaded += (s, e) => txtClassName.Focus();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClassName.Text))
            {
                FormError.Text = Loc.T("Give the class a name.");
                txtClassName.Focus();
                return;
            }
            FormError.Text = string.Empty;
            try
            {
                int id = await ClassData.SaveClassAsync(new ClassSummary
                {
                    ClassID = _editing?.ClassID ?? 0,
                    ClassName = txtClassName.Text.Trim(),
                    Major = txtMajor.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Created = datePickerCreateDate.SelectedDate
                });
                MessageBox.Show("Class saved successfully.");
                Saved?.Invoke(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The class could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => Cancelled?.Invoke();
    }
}
