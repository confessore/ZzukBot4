using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Harvester.Engine.Loaders.Profile
{
    public enum ProfileExtension
    {
        JSON,
        XML
    }

    public enum ProfileType
    {
        Grinding,
        Questing,
        Gathering,
        Travel
    }

    public class Loader
    {
        public async Task<ProfileData> LoadProfileAsync(string fileName, string profileName, ProfileExtension profileExtension, ProfileType profileType)
            => await Task.Run(() => LoadProfile(fileName, profileName, profileExtension, profileType));

        public ProfileData LoadProfile(string fileName, string profileName, ProfileExtension profileExtension, ProfileType profileType)
        {
            string content = new StreamReader(fileName).ReadToEnd();
            if (profileExtension == ProfileExtension.XML)
                content = ConvertXmlToJson(profileName, content, profileType);
            return JsonConvert.DeserializeObject<ProfileData>(content);
        }

        string ConvertXmlToJson(string profileName, string content, ProfileType profileType)
        {
            string url = "http://profile-converter.herokuapp.com/xml/" + WebUtility.UrlEncode(profileName) + "/" + profileType;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] bytes = Encoding.ASCII.GetBytes(content);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            throw new Exception("Could not convert XML to JSON: " + response.ToString());
        }
    }
}
