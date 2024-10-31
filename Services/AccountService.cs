using LMSOnline.Data;
using System.Security.Claims;

namespace LMSOnline.Services
{
    public interface AccountService
    {
        public AccountDTO Login(LoginDTO loginDTO);
        public dynamic ProfileAccount(int id);
        public bool UpdateAvatar(int id,UpdateAvatar updateAvatar);
        public bool CheckOldPassword(int id, string oldPassword);
        public bool ChangePassword(int id,ChangPasswordDTO password);
    }
}
