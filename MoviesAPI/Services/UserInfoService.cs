using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data.DTOs.UserInfo;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class UserInfoService
    {
        public UserInfo CreateUserInfo(CreateUserInfoDTO userInfoDTO)
        {
            UserInfo userInfo = new UserInfo
            {
                Allergies = userInfoDTO.Allergies,
                Cirurgies = userInfoDTO.Cirurgies,
                BloodType = userInfoDTO.BloodType,
                MedicalCondition = userInfoDTO.MedicalCondition,
                MotherName = userInfoDTO.MotherName,
                Medications = userInfoDTO.Medications,
                PreviousCirurgies = userInfoDTO.PreviousCirurgies
            };
            return userInfo;
        }
    }
}
