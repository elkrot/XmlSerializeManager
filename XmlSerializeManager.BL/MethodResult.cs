using System.Collections.Generic;

namespace XmlSerializeManager.BL
{
    #region Класс работы с результатом методов

    public class MethodResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public MethodResult()
        {
            Messages = new List<string>();
            Success = false;
        }
    }

    public class MethodResult<T> : MethodResult
    {
        public T Result { get; set; }
        public MethodResult(T result) : base()
        {
            Result = result;
        }
    }
    #endregion
}
