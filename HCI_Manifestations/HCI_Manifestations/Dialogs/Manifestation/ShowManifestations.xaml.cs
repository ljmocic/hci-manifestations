﻿using HCI_Manifestations.Dialogs;
using HCI_Manifestations.Help;
using HCI_Manifestations.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Manifestations.dialogs
{
    public partial class ShowManifestations: Window, INotifyPropertyChanged
    {
        #region Attributes
        private ObservableCollection<Manifestation> manifestations;
        public ObservableCollection<Manifestation> Manifestations
        {
            get { return manifestations; }
            set
            {
                if (value != manifestations)
                {
                    manifestations = value;
                    OnPropertyChanged("Manifestations");
                }
            }
        }

        public Manifestation SelectedManifestation { get; set; }

        public DateTime fromDate;
        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                if (value != fromDate)
                {
                    fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }

        public DateTime toDate;
        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                if (value != toDate)
                {
                    toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        #endregion

        #region Constructor
        public ShowManifestations()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            SelectedManifestation = null;
            DataContext = this;
            comboBoxType.DataContext = Database.getInstance();
            Manifestations = Database.getInstance().Manifestations;

            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }
        #endregion

        #region Event handlers
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddManifestation addManifestation = new AddManifestation();
            addManifestation.Show();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedManifestation != null)
            {
                EditManifestation editManifestation = new EditManifestation(SelectedManifestation.Id);
                editManifestation.Show();
            }
            else
            {
                MessageBox.Show("Molimo, odaberite manifestaciju za ažuriranje", "Ažuriranje manifestacije");
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedManifestation != null)
            {
                Database.DeleteManifestation(SelectedManifestation);
                SelectedManifestation = null;
            }
            else
            {
                MessageBox.Show("Molimo, odaberite manifestaciju za brisanje", "Brisanje manifestacije");
            }
        }

        private void manifestationsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedManifestation != null)
            {
                buttonEdit.IsEnabled = true;
                buttonDelete.IsEnabled = true;
            }
        }
        
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            Manifestations = Database.getInstance().Manifestations;

            searchInputId.Text = "";
            searchInputName.Text = "";
            comboBoxAlcohol.SelectedIndex = 0;
            comboBoxPrice.SelectedIndex = 0;
            comboBoxType.SelectedValue = null;
            checkBoxHandicap.IsChecked = false;
            checkBoxSmokingAllowed.IsChecked = false;
            radioButtonInside.IsChecked = false;
            radioButtonOutside.IsChecked = false;
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            var result = new ObservableCollection<Manifestation>();
            result = Database.getInstance().Manifestations;
            if (DateTime.Compare(fromDate, toDate) != 0)
            {
                result = filterDate(result, true);
            }
            if (checkBoxHandicap.IsChecked == true)
            {
                result = filterHandicap(result, true);
            }
            if (checkBoxSmokingAllowed.IsChecked == true)
            {
                result = filterSmoking(result, true);
            }
            if (radioButtonInside.IsChecked == true)
            {
                result = filterInside(result, true);
            }
            if (radioButtonOutside.IsChecked == true)
            {
                result = filterOutside(result, true);
            }
            if (!string.IsNullOrWhiteSpace(searchInputId.Text))
            {
                result = filterId(result, true);
            }
            if (!string.IsNullOrWhiteSpace(searchInputName.Text))
            {
                result = filterName(result, true);
            }
            if (!comboBoxAlcohol.Text.Equals("---"))
            {
                result = filterAlcohol(result);
            }
            if (!comboBoxPrice.Text.Equals("---"))
            {
                result = filterPrice(result);
            }
            if (!string.IsNullOrWhiteSpace(comboBoxType.Text))
            {
                result = filterType(result);
            }

            Manifestations = result;
        }

        private void searchInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonSearch_Click(null, null);
            }
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("ShowManifestations", this);
        }

        private void manifestationsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            buttonEdit_Click(null, null);
        }
        #endregion

        #region Filters
        private ObservableCollection<Manifestation> filterDate(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                int result = DateTime.Compare(fromDate, toDate);
                if (result <= 0)
                {
                    int compareFrom = DateTime.Compare(fromDate, data.Date);
                    int compareTo = DateTime.Compare(data.Date, toDate);
                    if (compareFrom <= 0 && compareTo <= 0)
                    {
                        replace.Add(new Manifestation(data));
                    }
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterHandicap(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Handicap == value)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterInside(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Inside == value)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterOutside(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Outside == value)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterSmoking(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.SmokingAllowed == value)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterId(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Id.Contains(searchInputId.Text) && value == true)
                {
                    replace.Add(new Manifestation(data));
                }
                if (!data.Id.Contains(searchInputId.Text) && value == false)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterName(ObservableCollection<Manifestation> manifestations, bool value)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Name.Contains(searchInputName.Text) && value == true)
                {
                    replace.Add(new Manifestation(data));
                }
                else if (!data.Name.Contains(searchInputName.Text) && value == false)
                {
                    replace.Add(new Manifestation(data));
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterAlcohol(ObservableCollection<Manifestation> manifestations)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Alcohol != null)
                { 
                    if (data.Alcohol.Equals(comboBoxAlcohol.Text))
                    {
                        replace.Add(new Manifestation(data));
                    }
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterPrice(ObservableCollection<Manifestation> manifestations)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Price != null)
                {
                    if (data.Price.Equals(comboBoxPrice.Text))
                    {
                        replace.Add(new Manifestation(data));
                    }
                }
            }
            return replace;
        }

        private ObservableCollection<Manifestation> filterType(ObservableCollection<Manifestation> manifestations)
        {
            var replace = new ObservableCollection<Manifestation>();

            foreach (var data in manifestations)
            {
                if (data.Type != null)
                { 
                    if (data.Type.Id.Equals(comboBoxType.Text))
                    {
                        replace.Add(new Manifestation(data));
                    }
                }
            }
            return replace;
        }
        #endregion

        #region PropertyChangedNotifier
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
}
