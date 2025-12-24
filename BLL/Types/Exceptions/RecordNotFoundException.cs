namespace BLL.Types.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base("Запись объекта не найдена") { }

        public RecordNotFoundException(string message) : base(message) { }
    }
}
