using CinameManageMent.Helper;
using LMSOnline.Data;
using LMSOnline.Models;
using System.Security.Claims;

namespace LMSOnline.Services
{
    public class AccountServiceImpl : AccountService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AccountServiceImpl(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.databaseContext = databaseContext;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        public AccountDTO Login(LoginDTO loginDTO)
        {
            var login=databaseContext.Accounts.FirstOrDefault(d=>d.Username==loginDTO.username);
            if(login!=null && BCrypt.Net.BCrypt.Verify(loginDTO.password, login.Password))
            {
                var user = new AccountDTO
                {
                    Id = login.Id,
                    FullName = login.FullName,
                    Email = login.Email,
                    Username = login.Username,
                    Phone = login.Phone,
                    Role = login.AccountType switch
                    {
                        1 => "LeaderShip",
                        2 => "Teacher",
                        3 => "Student"
                    },
                    Avatar = configuration["ImageUrl"] + login.Avatar,
                };
                return user;
            }
            else
            {
                return null;
            }

        }

        public dynamic ProfileAccount(int id)
        {
            return databaseContext.Accounts
     .Where(d => d.Id == id)
     .Select(d => new
     {
         id = d.Id,
         UserCode = d.UserCode,
         Gender = d.gender == true ? "Nam" : "Nữ",
         Role = d.AccountType == 1 ? "LeaderShip" :
           d.AccountType == 2 ? "Teacher" :
           d.AccountType == 3 ? "Student" : "Unknown",
        Email=d.Email,
        Username=d.Username,
        Phone=d.Phone,
        Address=d.Address,
        FullName=d.FullName,
        Avatar = configuration["ImageUrl"] + d.Avatar
     })
     .FirstOrDefault();
        }
        private void DeletePictureFromFolder(string base64String, string webrootpath)
        {
            string ImagePath = Path.Combine(webrootpath, "Images", base64String);
            if (System.IO.File.Exists(ImagePath))
            {
                System.IO.File.Delete(ImagePath);
            }
        }
        private string SavePictureToFolder(IFormFile file, string webrootpath)
        {
            try
            {
                var fileName = FileHelper.GenerateFileName(file.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "Images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return fileName;
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateAvatar(int id, UpdateAvatar updateAvatar)
        {
            try
            {
                var image = databaseContext.Accounts.FirstOrDefault(d => d.Id == id);
                if (image == null)
                {
                    throw new Exception("Account not found.");
                }

                // Check if the provided image name is valid
                if (updateAvatar?.ImageName == null)
                {
                    throw new ArgumentNullException("ImageName cannot be null.");
                }
                if (image.Avatar == "defaultImage.jpg")
                {
                    
                    string imageSave = SavePictureToFolder(updateAvatar.ImageName, webHostEnvironment.EnvironmentName);
                    image.Avatar = imageSave;


                }
                else
                {
                    DeletePictureFromFolder(image.Avatar, webHostEnvironment.EnvironmentName);
                    string imageSave = SavePictureToFolder(updateAvatar.ImageName, webHostEnvironment.EnvironmentName);
                    image.Avatar = imageSave;
                }
                return databaseContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
           
        }

        public bool CheckOldPassword(int id, string oldPassword)
        {
          var user=databaseContext.Accounts.FirstOrDefault(d=>d.Id==id);
            if (user != null && BCrypt.Net.BCrypt.Verify(oldPassword,user.Password))
            {
                return true;
            }
            return false;
        }
        private void SaveNewPassword(int id, string newPassword)
        {
            var user=databaseContext.Accounts.SingleOrDefault(d=>d.Id==id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            user.Password=BCrypt.Net.BCrypt.HashPassword(newPassword);
            databaseContext.SaveChanges();
        }

        public bool ChangePassword(int id, ChangPasswordDTO password)
        {
            try
            {
             if(CheckOldPassword(id, password.OldPassword))
                {
                    if (!password.IsPasswordConfirmed())
                    {
                        return false;
                    }
                    if(string.IsNullOrWhiteSpace(password.NewPassword) || password.NewPassword.Length < 8)
                    {
                        return false;
                    }
                    SaveNewPassword(id, password.NewPassword);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
