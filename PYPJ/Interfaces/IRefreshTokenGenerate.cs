namespace PYPJ.Interfaces
{
    public interface IRefreshTokenGenerate
    {
       public string GenerateToken(Guid UserId);
    }
}
