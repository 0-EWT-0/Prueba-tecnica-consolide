using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class SessionModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id_session { get; set; }

    [Required]
    public string Session_token { get; set; } = string.Empty;

    [Required]
    public DateTime Created_at { get; set; } = DateTime.UtcNow;

    [ForeignKey("id_user_id")]
    public UserModel? User { get; set; }

}
