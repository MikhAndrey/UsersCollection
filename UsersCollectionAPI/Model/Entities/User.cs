using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersCollectionAPI.Model.Entities;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public Status Status { get; set; }
}
