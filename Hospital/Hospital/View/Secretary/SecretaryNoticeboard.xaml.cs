using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for SecretaryNoticeboard.xaml
    /// </summary>
    public partial class SecretaryNoticeboard : Window
    {
        private NoticeboardController noticeboardController = new NoticeboardController();
        ObservableCollection<Noticeboard> noticeboardsOC = new ObservableCollection<Noticeboard>();
        List<Noticeboard> noticeboards = new List<Noticeboard>();

        
        
        public SecretaryNoticeboard()
        {
            InitializeComponent();
            noticeboards = noticeboardController.GetAll();
            //noticeboardDataGrid.ItemsSource = noticeboards;
            saveButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            //DatePickerTB.SelectedDate = DateTime.Now;
            foreach(Noticeboard noticeboard in noticeboards)
            {
                noticeboardsOC.Add(noticeboard);
            }
            noticeboardDataGrid.ItemsSource = noticeboardsOC;
        }

        private void WindowUpdate()
        {
            noticeboards = noticeboardController.GetAll();
            noticeboardDataGrid.ItemsSource = noticeboards;
        }

        private void ClearFileds()
        {
            TitleTB.Clear();
            TextTB.Clear();
            //DatePickerTB.SelectedDate = null;

        }

        private Noticeboard CreateNoticeFromData()
        {
            int id = noticeboardController.GenerateNewId();
            string title = TitleTB.Text;
            string text = TextTB.Text;
            //DateTime date = DatePickerTB.SelectedDate.Value;
            DateTime dateNotice = DateTime.Now; //new DateTime.(date.Year, date.Month, date.Day);
            Noticeboard noticeboard = new Noticeboard(id, title, text, dateNotice);
            return noticeboard;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SecretaryFun secretaryFun = new SecretaryFun();
            secretaryFun.Show();
            this.Close();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                Noticeboard noticeboard = CreateNoticeFromData();
                noticeboardController.Save(noticeboard);
                WindowUpdate();
                ClearFileds();
            }
            catch
            {
                MessageBox.Show("Fill in all the fields!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Noticeboard noticeboard = (Noticeboard)noticeboardDataGrid.SelectedItems[0];
                noticeboardController.Delete(noticeboard.Id);
                //WindowUpdate();
                noticeboardsOC.Remove(noticeboard);
            }
            catch
            {
                MessageBox.Show("You have to select an notice to delete!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Noticeboard noticeboard = (Noticeboard)noticeboardDataGrid.SelectedItems[0];
                TitleTB.Text = noticeboard.Title;
                TextTB.Text = noticeboard.Text;
                //DatePickerTB.SelectedDate = noticeboard.NoticeDate;
                saveButton.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
                
            }
            catch
            {
                MessageBox.Show("Select the patient", "greska");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Noticeboard noticeboard = (Noticeboard)noticeboardDataGrid.SelectedItems[0];
            int id = noticeboard.Id;
            string title = TitleTB.Text;
            string text = TextTB.Text;
            //DateTime dateTime = DatePickerTB.SelectedDate.Value;
            //DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DateTime date = DateTime.Now;
            Noticeboard noticeboard1 = new Noticeboard(id, title, text, date);
            noticeboardController.Update(noticeboard1);
            saveButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            noticeboards = noticeboardController.GetAll();
            noticeboardsOC.Clear();
           

            foreach (Noticeboard noticeboard2 in noticeboards)
            {
                noticeboardsOC.Add(noticeboard2);
            }

            //WindowUpdate();
            ClearFileds();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            saveButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            ClearFileds();

        }

        
    }
}
