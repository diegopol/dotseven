namespace Pescador.Core.Interfaces
{
    public interface IHtmlPage
    {
        /// <summary>
        /// Timeout en milisegundos, utilizado para las operaciones de Request y Response
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Url Base del sistema Peldar
        /// </summary>
        string BaseUrl { get; set; }
    }
}