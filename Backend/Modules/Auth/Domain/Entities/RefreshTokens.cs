using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Domain.Entities
{


    public class RefreshToken
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionId { get; set; } = Guid.NewGuid().ToString();
        public string TokenHash { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }

        [ForeignKey("UserId")]
        public virtual User user { get; set; }






    }



}