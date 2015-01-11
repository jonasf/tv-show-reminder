using System.IO;
using System.Net;

namespace TvShowReminder.TvRageApi.Utilities
{
    internal class HttpClient
    {
        internal string Get(string requestUri)
        {
            WebRequest request = WebRequest.Create(requestUri);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
