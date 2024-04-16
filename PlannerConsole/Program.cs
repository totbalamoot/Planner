using PlannerLib;

namespace PlannerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Account Account = new Account();
            Account.CreateProject("project1");
            Account.CreateProject("project2");
            Account.CreateTask("task1", Account.Projects[0]);
            Account.CreateTask("task2", Account.Projects[0]);

            Task task2 = (Task)Account.Tasks[1].Clone();
            Project project2 = (Project)Account.Projects[1].Clone();
            Account.EditTask(task2, project2);
        }
    }
}
