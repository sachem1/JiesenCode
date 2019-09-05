using Jiesen.Core.Enums;

namespace Jiesen.Core.Models
{
    public class BaseMessage
    {
        public BaseMessage()
        {

        }
        public BaseMessage(ResultState state, string message = "")
        {
            State = state;
            Message = message;
        }


        public ResultState State { get; set; }


        public string Message { get; set; }
    }

    public class ResultMessage : BaseMessage
    {
        public ResultMessage() { }
        public ResultMessage(ResultState state, string message = "", object data = null)
            : base(state, message)
        {
            Data = data;
        }

        public object Data { get; set; }

    }

    public class ResultMessage<T> : BaseMessage
    {
        public ResultMessage() { }


        public T Data { get; set; }

        public ResultMessage(ResultState state, string message = "", T data = default(T))
            : base(state, message)
        {
            Data = data;
        }
    }

}
