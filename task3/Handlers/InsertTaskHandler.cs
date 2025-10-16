namespace task3;

class InsertTaskHandler
{
    private readonly TaskRepository _repository;

    public InsertTaskHandler(TaskRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Write title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Write description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Task is completed? (y,n):");
        string isCompletedString = Console.ReadLine() ?? "";

        if (isCompletedString != "y" && isCompletedString != "n")
        {
            Console.WriteLine("Incorrect, press any key");
            return;
        }

        bool isCompleted = isCompletedString == "y";

        TaskModel task = new();

        task.Title = title;
        task.Description = description;
        task.IsCompleted = isCompleted;

        await _repository.InsertAsync(task);

        Console.WriteLine("Task successfully added");
        Console.WriteLine("Press any key");

    }
}
