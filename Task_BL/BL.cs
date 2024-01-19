using Task_DL;
using Task_EL;
using MyTask = Task_EL.Task;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Task_BL
{
    public class BL
    {
        public static ObservableCollection<MyTask> Tasks = new ObservableCollection<MyTask>();
        public static bool KisileriYukle(ref string message)
        {
            var list = DL.GetContacts(ref message);
            if (list != null)
            {
                foreach (var k in list)
                    Tasks.Add(k);

                return true;
            }
            return false;
        }

        public static bool KisiEkle(MyTask kisi, ref string message)
        {
            Tasks.Clear();

            if (kisi != null && DL.AddContact(kisi, ref message))
            {
                Tasks.Add(kisi);
                return true;
            }

            return false;
        }

        public static bool KisiDuzenle(MyTask task, ref string message)
        {
            if (task != null && DL.EditContact(task, ref message))
            {
                return true;
            }

            return false;
        }

        public static bool KisiSil(MyTask task, ref string message)
        {
            if (task != null && DL.DeleteContact(task, ref message))
            {

                return true;
            }

            return false;
        }
    }
}
