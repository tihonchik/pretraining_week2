using Microsoft.Extensions.Configuration;

namespace task3
{
    public class Program
    {
        static async Task Main()
        {

            var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            if (connectionString == "")
            {
                Console.WriteLine("Connection string not found");
                return;
            }

            SqlConnectionFactory sqlConnectionFactory = new SqlConnectionFactory(connectionString);

            TaskRepository taskRepository = new(sqlConnectionFactory);

            DeleteTaskHandler deleteTaskHandler = new(taskRepository);
            ViewAllTasksHandler viewAllTasksHandler = new(taskRepository);
            UpdateTaskHandler updateTasksHandler = new(taskRepository);
            InsertTaskHandler insertTasksHandler = new(taskRepository);

            if (connectionString == "")
            {
                Console.WriteLine("Connection string not found");
                return;
            }

            while (true)
            {
                Console.WriteLine("Add new task - 1");
                Console.WriteLine("Watch all tasks - 2");
                Console.WriteLine("Update state of task - 3");
                Console.WriteLine("Delete task by ID - 4");
                Console.WriteLine("Exit - 0");
                Console.Write("Choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {

                    case "1":
                        {
                            Console.Clear();
                            await insertTasksHandler.ExecuteAsync();
                            Console.ReadKey();
                        }
                        break;

                    case "2":
                        {
                            Console.Clear();
                            await viewAllTasksHandler.ExecuteAsync();
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        {
                            Console.Clear();
                            await updateTasksHandler.ExecuteAsync();
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        {
                            Console.Clear();
                            await deleteTaskHandler.ExecuteAsync();
                            Console.ReadKey();
                        }
                        break;
                    case "0": return;
                }

                Console.Clear();
            }
        }
    }
}

