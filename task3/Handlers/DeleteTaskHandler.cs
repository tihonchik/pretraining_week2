namespace task3;

class DeleteTaskHandler
{
    private readonly TaskRepository _repository;

    public DeleteTaskHandler(TaskRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Write the Id of the task to delete: ");
        string idString = Console.ReadLine() ?? "";

        int id;
        if (!int.TryParse(idString, out id))
        {
            Console.WriteLine("Incorrect, press any key");
            return;
        }

        bool result = await _repository.DeleteAsync(id);
        if (result)
        {
            Console.WriteLine($"Task with id {id} deleted successfully");
        }
        else
        {
            Console.WriteLine($"Task with id {id} does not exist");
        }
        Console.WriteLine("Press any key");
    }
}