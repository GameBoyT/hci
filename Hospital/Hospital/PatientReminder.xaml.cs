using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Controller;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for PatientReminder.xaml
    /// </summary>
    public partial class PatientReminder : Window
    {
        PatientController patientController = new PatientController();
        Patient patient;


        public PatientReminder()
        {
            InitializeComponent();
            patient = patientController.GetByJmbg("5");
            List<Reminder> reminderList = patientController.GettAllReminders(patient.User.Jmbg);
            ReminderListView.ItemsSource = reminderList;
        }

        private Reminder FormToReminder()
        {
            DateTime date = datePicker.SelectedDate.Value;
            int notifyHours = Int32.Parse(notifyTimeTB.Text.Split(':')[0]);
            int notifyMinutes = Int32.Parse(notifyTimeTB.Text.Split(':')[1]);

            int hours = Int32.Parse(timeTB.Text.Split(':')[0]);
            int minutes = Int32.Parse(timeTB.Text.Split(':')[1]);

            DateTime notify = new DateTime(date.Year, date.Month, date.Day, notifyHours, notifyMinutes, 00);
            DateTime time = new DateTime(date.Year, date.Month, date.Day, hours, minutes, 00);

            Reminder reminder = new Reminder(2,titleTB.Text, time, notify);

            return reminder;
            

        }

        private void addReminderBnt_Click(object sender, RoutedEventArgs e)
        {
            patientController.AddReminder(patient.User.Jmbg, FormToReminder());
            WindowUpdate();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Reminder reminder = (Reminder)ReminderListView.SelectedItems[0];
            patientController.DeleteRemider(patient.User.Jmbg, reminder.Id);
            WindowUpdate();
        }
        private void WindowUpdate()
        {
            ReminderListView.ItemsSource = patientController.GettAllReminders(patient.User.Jmbg);
        }

        private void ReminderToForm(Reminder reminder)
        {
            notifyTimeTB.Text = reminder.NotifyTime.ToString("hh:mm");
            timeTB.Text = reminder.Time.ToString("hh:mm");
            titleTB.Text = reminder.Title;
            datePicker.SelectedDate = reminder.Time;

        }

        private void ReminderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Reminder reminder = (Reminder)ReminderListView.SelectedItems[0];
                ReminderToForm(reminder);
            }
            catch { }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Reminder reminder = (Reminder)ReminderListView.SelectedItems[0];
            Reminder newReminder = FormToReminder();
            newReminder.Id = reminder.Id;
            patientController.UpdateReminder(patient.User.Jmbg, newReminder);
            WindowUpdate();
        }
    }
}
