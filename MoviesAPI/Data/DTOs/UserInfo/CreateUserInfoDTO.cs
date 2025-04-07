namespace MoviesAPI.Data.DTOs.UserInfo
{
    public class CreateUserInfoDTO
    {
        public string MotherName { get; set; }
        public string BloodType { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Medications { get; set; }
        public bool PreviousCirurgies { get; set; }
        public List<string> Cirurgies { get; set; }
        public string MedicalCondition { get; set; }
    }
}
