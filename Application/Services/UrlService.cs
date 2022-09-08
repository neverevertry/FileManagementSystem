using Application.Interfaces;


namespace Application.Services
{
    public class UrlService : IUrlService
    {
        public string GenerateUrl(string host)
        {
            string symbols = "asdfghjklqwertoyp";
            Random random = new Random();

            string url = "";

            for (int i = 0; i < symbols.Length; i++)
            {
                url += symbols[random.Next(0, symbols.Length - 1)];
            }

            return "https://" + host + "/file/download/templink/" + url;
        }
    }
}
