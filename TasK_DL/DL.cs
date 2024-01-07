
#define FIREBASE // Eğer MySQL kullanacaksanız FIREBASE yerine MySQL kullanınınusing System;
using Task_EL;
using MyTask = Task_EL.Task;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

#if FIREBASE
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
#endif
namespace Task_DL
{
    public class DL
    {
#if FIREBASE
        static FirebaseClient client = new FirebaseClient("https://taskdb-9d2d6-default-rtdb.firebaseio.com");

        public static bool AddContact(MyTask k, ref string message)
        {
            client.Child($"Tasks/{k.ID}").PutAsync(k);
            return true;
        }

        public static bool EditContact(MyTask k, ref string message)
        {
            client.Child($"Tasks/{k.ID}").PutAsync(k);
            return true;
        }

        public static bool DeleteContact(MyTask k, ref string message)
        {
            client.Child($"Tasks/{k.ID}").DeleteAsync();
            return true;
        }

        public static ObservableCollection<MyTask> GetContacts(ref string message)
        {
            ObservableCollection<MyTask> Tasklist = new ObservableCollection<MyTask>();

            var Tasks = client.Child("Tasks").OnceAsync<MyTask>();
            foreach (var task in Tasks.Result)
            {
                Tasklist.Add(task.Object);
            }
            return Tasklist;
        }
#endif
        


        }
    }
