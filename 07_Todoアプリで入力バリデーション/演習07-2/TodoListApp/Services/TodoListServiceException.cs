namespace TodoListApp.Services;

public class TodoListServiceException : Exception
{
    public TodoListServiceException(string message) : base(message) { }
    public TodoListServiceException(string message, Exception inner) : base(message, inner) { }
}