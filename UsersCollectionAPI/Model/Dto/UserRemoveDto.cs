namespace UsersCollectionAPI.Model.Dto;

public class UserRemoveDto
{
    public required UserIdDto RemoveUser { get; set; }
}

public class UserIdDto
{
    public int Id { get; set; }
}
