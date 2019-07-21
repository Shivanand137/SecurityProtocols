namespace Todo.Entities
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static Response<T> Success(string message)
        {
            return new Response<T>() { IsSuccess = true, Message = message };
        }

        public static Response<T> Success(T data)
        {
            return new Response<T>() { IsSuccess = true, Data = data };
        }

        public static Response<T> Success(string message, T data)
        {
            return new Response<T>() { IsSuccess = true, Message = message, Data = data };
        }

        public static Response<T> Failure(string message)
        {
            return new Response<T>() { IsSuccess = false, Message = message };
        }
    }
}