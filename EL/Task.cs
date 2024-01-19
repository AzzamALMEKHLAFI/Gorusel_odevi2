using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_EL
{
    // All the code in this file is included in all platforms.
    public class Task
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        string id, title, note;
        public string ID
        {
            get
            {
                if (id == null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set { id = value; }
        }

        //public string Resim  => "person.png";

        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }
        public string Note
        {
            get => note;
            set
            {
                note = value;
                NotifyPropertyChanged();
            }
        }
    }
}