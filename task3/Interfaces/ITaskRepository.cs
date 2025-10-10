namespace task3
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetAllAsync();

        public Task<bool> DeleteAsync(int Id);

        public Task<int> InsertAsync(TaskModel task);

        public Task<bool> UpdateAsync(TaskModel task);

        public Task<TaskModel?> GetByIdAsync(int Id);
    }
}