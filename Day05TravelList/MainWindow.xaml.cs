using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Day05TravelList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PATH = @"..\..\trip.txt";
        private List<Trip> tripList = new List<Trip>();
        private Trip selectedTrip;

        public MainWindow()
        {
            InitializeComponent();
            loadDataFromFile();
            lvTrip.ItemsSource = tripList;
        }

        private void lvTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTrip = (Trip)lvTrip.SelectedItem;
            if (selectedTrip != null)
            {
                tbTravellerName.Text = selectedTrip.TravellerName;
                tbTravellerPassportNo.Text = selectedTrip.TravellerPassportNo;
                tbDestination.Text = selectedTrip.Destination;
                tbDepartureDate.Text = selectedTrip.DepartureDate.ToString();
                tbReturnDate.Text = selectedTrip.ReturnDate.ToString();
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tripList.Add(new Trip(tbTravellerName.Text, tbTravellerPassportNo.Text, tbDestination.Text, Convert.ToDateTime(tbDepartureDate.Text), Convert.ToDateTime(tbReturnDate.Text)));
                tbDestination.Text = "";
                tbTravellerName.Text = "";
                tbTravellerPassportNo.Text = "";
                tbDepartureDate.Text = "";
                tbReturnDate.Text = "";
                lvTrip.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedTrip != null)
                {
                    selectedTrip.TravellerName = tbTravellerName.Text;
                    selectedTrip.TravellerPassportNo = tbTravellerPassportNo.Text;
                    selectedTrip.Destination = tbDestination.Text;
                    selectedTrip.setDepartureAndReturnDates(Convert.ToDateTime(tbDepartureDate.Text), Convert.ToDateTime(tbReturnDate.Text));
                    lvTrip.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Input Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTrip == null)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete this item?\n" + selectedTrip, "Delete confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
                tripList.Remove(selectedTrip);
            lvTrip.Items.Refresh();
        }

        private void loadDataFromFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(PATH);
                foreach (string line in lines)
                {
                    string[] data = line.Split(';');
                    try
                    {
                        tripList.Add(new Trip(data[0], data[1], data[2], Convert.ToDateTime(data[3]), Convert.ToDateTime(data[4])));
                    }
                    catch (InvalidParameterException ex)
                    {
                        Console.WriteLine("Error parsing line '{0}':\n{1}\nskipping.", line, ex.Message);
                        //MessageBox.Show(this, "Error parsing line '{line}':\n{ex.Message}\nskipping.", "Save Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, "Load Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, ex.Message, "Load Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void saveDataToFile()
        {
            try
            {
                File.WriteAllText(PATH, string.Empty);
                foreach (Trip trip in tripList)
                {
                    File.AppendAllText(PATH, trip.ToFileString() + "\n");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, "Save Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveDataToFile();
        }
    }
}