
namespace appECMHV2.Models
{
    using Newtonsoft.Json;
    public class LoggedUser
    {
        [JsonProperty(PropertyName = "UserLogged")]
        public string UserLogged { get; set; }

        [JsonProperty(PropertyName = "NameStudent")]
        public string NameStudent { get; set; }

        [JsonProperty(PropertyName = "Curriculum")]
        public string Curriculum { get; set; }

        public string idPlayer { get; set; }

    }
}
