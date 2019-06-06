using System.ComponentModel.DataAnnotations;

namespace AbpDemo.Controllers
{
    public class LoginFromBodyModel
    {
        private string _userNameOrEmailAddress;

        //public AuthenticateModel AuthModel { set; get; }

        //public LoginClientModel ClientModel { set; get; }

        [Required]
        [StringLength(30)]
        public string UserNameOrEmailAddress
        {
            get
            {
                return _userNameOrEmailAddress;
            }
            set
            {
                _userNameOrEmailAddress = value.Replace(" ", null).Replace("　", null);
            }
        }

        [Required]
        [StringLength(18)]
        public string Password { get; set; }

        public bool RememberClient { get; set; }

        public string ClientState { get; set; }
        public string ClientId { get; set; }
        public string ReturnUrl { get; set; }

    }
}