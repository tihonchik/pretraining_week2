namespace task3;

class ViewAllTasksHandler
{
    private readonly TaskRepository _repository;

    public ViewAllTasksHandler(TaskRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync()
    {
        TaskModel[] tasks = (await _repository.GetAllAsync()).ToArray();
        foreach (TaskModel task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Description: {task.Description}, IsCompleted: {task.IsCompleted}, CreatedAt: {task.CreatedAt}");

        }
        Console.WriteLine("Press any key");
    }
}