using Task_EL;
using MyTask = Task_EL.Task;

namespace Gorusel_odevi2
{ 
public partial class TaskDetayi : ContentPage
{
        

    public bool Result = false;
    public MyTask mTask = null;
    bool edit = false;
    public Action<MyTask> AddMethod = null;
        public TaskDetayi(MyTask task = null)
        {
            InitializeComponent();

            if (task == null)
            {
                mTask = new MyTask();
                edit = false;
            }
            else
            {
                txttitle.Text = task.Title;
                txtnote.Text = task.Note;

                mTask = task;
                edit = true;

            }
        }

            private void OKClicked(object sender, EventArgs e)
            {
                Result = true;
                mTask.Title = txttitle.Text;
                mTask.Note = txtnote.Text;
              
               
                {
                    if (AddMethod != null)
                        AddMethod(mTask);

                }

                Navigation.PopModalAsync();
            }

            private void CancelClicked(object sender, EventArgs e)
            {
                Result = false;
                Navigation.PopModalAsync();
            }
        
}}