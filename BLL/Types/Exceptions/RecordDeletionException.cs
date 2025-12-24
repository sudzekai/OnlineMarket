namespace BLL.Types.Exceptions
{
    public class RecordDeletionException : Exception
    {
        public RecordDeletionException() : base("Ошибка при удалении записи объекта") { }

        public RecordDeletionException(string message) : base(message) { }
    }
}
