namespace FilmsCatalog.Models
{
    /// <summary>
    /// ��� ������ ������.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Id-�������.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// �������� Id-�������.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}