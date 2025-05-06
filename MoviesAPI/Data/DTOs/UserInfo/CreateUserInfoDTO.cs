namespace MoviesAPI.Data.DTOs.UserInfo
{
    public class CreateUserInfoDTO
    {
        public string MotherName { get; set; }
        public string BloodType { get; set; }
        public string MedicalCondition { get; set; }
        public bool HasCirurgies { get; set; }
        public bool HasAllergies { get; set; }
        public bool HasMedications { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Medications { get; set; }
        public List<string> Cirurgies { get; set; }
    }
}
