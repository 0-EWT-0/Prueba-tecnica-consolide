using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class OperationModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id_operation { get; set; }

    [Required]
    public int Total_amount { get; set; }

    [Required]
    public int Money_paid { get; set; }

    [Required]
    public int Money_exchange { get; set; }

    [Required]
    public DateTime Created_at { get; set; } = DateTime.UtcNow;

    [ForeignKey("Id_user_id")]
    public UserModel? User { get; set; }

}
