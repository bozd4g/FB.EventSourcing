using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.EventSourcing.Application
{
    [Serializable]
    public class ApiResponse
    {
        public string Message { get; protected set; } = string.Empty;
        public object Data { get; protected set; }
        public IList<string> Errors { get; set; }
        public bool HasErrors => Errors != null && Errors.Any();

        public ApiResponse()
        {
        }
        
        public ApiResponse(string message = "")
        {
            Message = message;
        }

        public ApiResponse(object data)
        {
            Data = data;
        }
        
        public ApiResponse(object data, string message = "")
        {
            Message = message;
            Data = data;
        }
        
        public ApiResponse(string[] errors)
        {
            Errors = errors;
        }
        
        public ApiResponse(Exception exception)
        {
            Errors = new List<string>
            {
                exception.Message
            };
        }
    }
    
    public sealed class ApiResponse<T> : ApiResponse where T : class, new()
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}