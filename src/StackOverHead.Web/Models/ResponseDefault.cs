namespace StackOverHead.Web.Models
{
    public class ResponseDefault<T>
    {
        public T Data { get; set; }
        public string message { set; get; }
    }
}
