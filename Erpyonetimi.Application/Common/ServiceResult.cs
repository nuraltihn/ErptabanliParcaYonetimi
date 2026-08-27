namespace Erpyonetimi.Application.Common
{
    public class ServiceResult
    {
        public bool Basarili { get; init; }
 public string Mesaj { get; init; } = string.Empty;

        public static ServiceResult Basarili_(string mesaj = "İşlem başarılı.")
            => new() { Basarili = true, Mesaj = mesaj };

        public static ServiceResult Basarisiz(string mesaj)
            => new() { Basarili = false, Mesaj = mesaj };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }

        public static ServiceResult<T> Basarili_(
            T data,
            string mesaj = "İşlem başarılı.")
            => new()
            {
                Basarili = true,
                Mesaj = mesaj,
                Data = data
            };

        public static new ServiceResult<T> Basarisiz(string mesaj)
            => new()
            {
                Basarili = false,
                Mesaj = mesaj,
                Data = default
            };
    }
}