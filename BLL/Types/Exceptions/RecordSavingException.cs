namespace BLL.Types.Exceptions
{
    public class RecordSavingException : Exception
    {
        public RecordSavingException() : base("Ошибка при сохранении изменений в БД") { }

        public RecordSavingException(string message) : base(message) { }
    }
}
