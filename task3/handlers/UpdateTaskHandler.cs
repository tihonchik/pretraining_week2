namespace task3
{
    class UpdateTaskHandler
    {
        private readonly TaskRepository _repository;

        public UpdateTaskHandler(TaskRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync()
        {
            Console.Write("Write the Id of the task to update its 'IsCompleted' state : ");
            string idString = Console.ReadLine() ?? "";

            int id;
            if (!int.TryParse(idString, out id))
            {
                Console.WriteLine("Incorrect, press any key");
                return;
            }

            TaskModel? task = await _repository.GetByIdAsync(id);
            if (task is null)
            {
                Console.WriteLine("Not found task with this Id");
                return;
            }

            Console.Write("Task is completed? (y,n):");
            string isCompletedString = Console.ReadLine() ?? "";

            if (isCompletedString != "y" && isCompletedString != "n")
            {
                Console.WriteLine("Incorrect, press any key");
                return;
            }

            bool isCompleted = isCompletedString == "y";
            task.IsCompleted = isCompleted;

            bool result = await _repository.UpdateAsync(task);
            if (result)
            {
                Console.WriteLine($"Task with id {id} update successfully");
            }
            else
            {
                Console.WriteLine($"Task with id {id} update unsuccessfully");
            }

            Console.WriteLine("Press any key");

        }
    }
}