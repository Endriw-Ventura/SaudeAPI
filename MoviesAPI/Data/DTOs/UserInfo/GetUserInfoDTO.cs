namespace MoviesAPI.Data.DTOs.UserInfo
{
    public class GetUserInfoDTO
    {
        public string BloodType { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Medications { get; set; }
        public bool PreviousCirurgies { get; set; }
        public List<string> Cirurgies { get; set; }
        public string MedicalCondition { get; set; }
    }
}
