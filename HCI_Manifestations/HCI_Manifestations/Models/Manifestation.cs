﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HCI_Manifestations.Config;

namespace HCI_Manifestations.Models
{
    [Serializable]
    public class Manifestation : INotifyPropertyChanged
    {
        #region Attributes
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private bool handicap;
        public bool Handicap
        {
            get { return handicap; }
            set
            {
                if (value != handicap)
                {
                    handicap = value;
                    OnPropertyChanged("Handicap");
                }
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private ManifestationType type;
        public ManifestationType Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private string iconPath;
        public string IconPath
        {
            get { return iconPath; }
            set
            {
                if (value != iconPath)
                {
                    iconPath = value;
                    string newPath = Directory.GetCurrentDirectory() + "\\" + @iconPath.Split('\\').Last();
                    if (!File.Exists(newPath) && newPath != null && !string.IsNullOrEmpty(newPath) && !string.IsNullOrWhiteSpace(newPath))
                    {
                        File.Copy(@iconPath, @newPath, true);
                    }
                    iconPath = newPath;
                    OnPropertyChanged("IconPath");
                }
            }
        }

        private bool smokingAllowed;
        public bool SmokingAllowed
        {
            get { return smokingAllowed; }
            set
            {
                if (value != smokingAllowed)
                {
                    smokingAllowed = value;
                    OnPropertyChanged("SmokingAllowed");
                }
            }
        }

        private bool inside;
        public bool Inside
        {
            get { return inside; }
            set
            {
                if (value != inside)
                {
                    inside = value;
                    OnPropertyChanged("Inside");
                }
            }
        }

        private bool outside;
        public bool Outside
        {
            get { return outside; }
            set
            {
                if (value != outside)
                {
                    outside = value;
                    OnPropertyChanged("Outside");
                }
            }
        }

        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                if (value != price)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        private string alcohol;
        public string Alcohol
        {
            get { return alcohol; }
            set
            {
                if (value != alcohol)
                {
                    alcohol = value;
                    OnPropertyChanged("Alcohol");
                }
            }
        }

        private string expectedPublic;
        public string ExpectedPublic
        {
            get { return expectedPublic; }
            set
            {
                if (value != expectedPublic)
                {
                    expectedPublic = value;
                    OnPropertyChanged("ExpectedPublic");
                }
            }
        }
        
        private ObservableCollection<ManifestationTag> tags;
        public ObservableCollection<ManifestationTag> Tags
        {
            get { return tags; }
            set
            {
                if (value != tags)
                {
                    tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }

        private int x;
        public int X
        {
            get { return x; }
            set
            {
                if (value != x)
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                if (value != y)
                {
                    y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        #endregion

        #region Constructors
        public Manifestation()
        {
            Tags = new ObservableCollection<ManifestationTag>();
        }

        public Manifestation(string id, string name, string description, DateTime date, ManifestationType type, string iconPath,
            bool smokingAllowed, bool inside, bool outside, string price, string alcohol, string expectedPublic, ObservableCollection<ManifestationTag> tags)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.date = date;
            this.type = type;
            string newPath = Directory.GetCurrentDirectory() + @iconPath.Split('/').Last();
            if (!File.Exists(newPath) && newPath != null && !string.IsNullOrEmpty(newPath) && !string.IsNullOrWhiteSpace(newPath))
            {
                File.Copy(@iconPath, @newPath, true);
            }
            this.iconPath = newPath;
            this.smokingAllowed = smokingAllowed;
            this.inside = inside;
            this.outside = outside;
            this.price = price;
            this.alcohol = alcohol;
            this.expectedPublic = expectedPublic;
            this.tags = tags;
            this.x = -1;
            this.y = -1;
        }

        public Manifestation(Manifestation manifestation)
        {
            id = manifestation.id;
            name = manifestation.name;
            description = manifestation.description;
            date = manifestation.date;
            handicap = manifestation.handicap;
            type = new ManifestationType(manifestation.type);
            iconPath = manifestation.iconPath;
            inside = manifestation.inside;
            outside = manifestation.outside;
            smokingAllowed = manifestation.smokingAllowed;
            price = manifestation.price;
            alcohol = manifestation.alcohol;
            expectedPublic = manifestation.expectedPublic;
            tags = new ObservableCollection<ManifestationTag>(manifestation.tags);
            x = manifestation.X;
            y = manifestation.Y;
        }
        #endregion

        #region PropertyChangedNotifier
        [field: NonSerialized]
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
