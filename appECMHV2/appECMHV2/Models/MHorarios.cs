namespace appECMHV2.Models
{
    using Newtonsoft.Json;
    using System;

    public class MHorarios
    {
        [JsonProperty(PropertyName = "PeopleID")]
        public string PeopleID { get; set; }

        [JsonProperty(PropertyName = "LegalName")]
        public string LegalName { get; set; }

        [JsonProperty(PropertyName = "AcademicYear")]
        public string AcademicYear { get; set; }

        [JsonProperty(PropertyName = "AcademicTerm")]
        public string AcademicTerm { get; set; }

        [JsonProperty(PropertyName = "EventID")]
        public string EventID { get; set; }

        [JsonProperty(PropertyName = "EventLongName")]
        public string EventLongName { get; set; }

        [JsonProperty(PropertyName = "Section")]
        public string Section { get; set; }

        [JsonProperty(PropertyName = "Section2")]
        public string Section2 { get; set; }

        [JsonProperty(PropertyName = "HorariosProcesados")]
        public string HorariosProcesados { get; set; }



    }
}
