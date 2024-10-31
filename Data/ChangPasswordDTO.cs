

namespace LMSOnline.Data
{
    public class ChangPasswordDTO
    {
     
        public string OldPassword { get; set; }
    
        public string NewPassword { get; set; }
       
        public string ConfirmPassword { get; set;}
        public bool IsPasswordConfirmed()
        {
            return NewPassword==ConfirmPassword;
        }
        
    }
}
