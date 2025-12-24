namespace BLL.Types.Exceptions
{
    public class RecordCreationException : Exception
    {
        public RecordCreationException() : base("Ошибка при создании записи объекта") { }

        public RecordCreationException(string message) : base(message) { }
    }
}
