﻿using HCI_Manifestations.Models;
using System;
using System.Collections.Generic;
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
    public partial class AddTag : Window
    {
        #region Attributes
        private ManifestationTag tag;
        public ManifestationTag mTag // Tag name Overshadows Tag from .NET library
        {
            get { return tag; }
            set { tag = value; }
        }
        #endregion

        #region Constructors
        public AddTag()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            tag = new ManifestationTag();
            DataContext = tag;
        }
        #endregion

        #region Event handlers
        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            tag.Color = ColorPicker.SelectedColor.ToString();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO validation later
            bool validated = true;
            if (validated)
            {
                Database.AddTag(mTag);
                Close();
            }
            else
            {
                // If data is not valid
            }

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!Fields_Empty())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Da li ste sigurni?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        private bool Fields_Empty()
        {
            if (string.IsNullOrWhiteSpace(textBoxId.Text) && string.IsNullOrWhiteSpace(mTag.Color) && string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}