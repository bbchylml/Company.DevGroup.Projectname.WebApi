using System.ComponentModel.DataAnnotations;

namespace Company.DevGroup.Projectname.IdentityServer4.Domain
{
    public class User
    {
        [Key]
        [MaxLength(32)]
        public string ID { get; set; }

        [MaxLength(32)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

    }
}
