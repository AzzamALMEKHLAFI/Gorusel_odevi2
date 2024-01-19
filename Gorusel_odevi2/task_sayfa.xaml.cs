using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Task_BL;
using Task_EL;

using MyTask = Task_EL.Task;

namespace Gorusel_odevi2
{
    public partial class task_sayfa : ContentPage
    {
        public task_sayfa()
        {
            InitializeComponent();
            if (!BL.KisileriYukle(ref message))
                DisplayAlert("Error", message, "Cancel");

            listTask.ItemsSource = BL.Tasks;
        }

        private async void TaskEkleEvent(object sender, EventArgs e)
        {
            TaskDetayi page = new TaskDetayi()
            {
                Title = "Kişi Ekle",
                AddMethod = AddTask
            };
            await Navigation.PushModalAsync(page, true);

        }
        private async void TaskDuzenleEvent(object sender, EventArgs e)
        {
            var ib = sender as MenuItem;
            var task = BL.Tasks.First(o => o.ID == ib.CommandParameter.ToString());
            TaskDetayi page = new TaskDetayi(task)
            {
                Title = "Kişi Düzenle",
                AddMethod = EditTask
            };
            await Navigation.PushModalAsync(page, true);
        }

        private async void TaskSilEvent(object sender, EventArgs e)
        {
            var ib = sender as MenuItem;
            var task = BL.Tasks.First(o => o.ID == ib.CommandParameter.ToString());

            bool answer = await DisplayAlert("Silmeyi Onayla", $"{task.Title} silinsin mi", "Evet", "Hayır");

            if (answer)
                //BL.Kisiler.Remove(kisi);
                if (!BL.KisiSil(task, ref message))
                    await DisplayAlert("Error", message, "Cancel");
        }

        private void AddTask(MyTask task)
        {
            if (!BL.KisiEkle(task, ref message))
                DisplayAlert("Error", message, "Cancel");
        }


        string message = "";
        private void EditTask(MyTask task)
        {
            if (!BL.KisiDuzenle(task, ref message))
                DisplayAlert("Error", message, "Cancel");
        }



        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            BL.Tasks.Clear();
                if (!BL.KisileriYukle(ref message))
                DisplayAlert("Error", message, "Cancel");
            listTask.ItemsSource = BL.Tasks;
        }
    }
}