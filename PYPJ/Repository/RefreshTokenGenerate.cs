using PYPJ.Interfaces;
using PYPJ.Models;
using System.Security.Cryptography;

namespace PYPJ.Repository
{
    public class RefreshTokenGenerate : IRefreshTokenGenerate
    {
        private readonly PypjContext context;

        public RefreshTokenGenerate(PypjContext learn_DB)
        {
            context = learn_DB;
        }
        public string GenerateToken(Guid UserId)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);

                var _user = context.TblRefreshTokens.FirstOrDefault(o => o.UserId == UserId);
                if (_user != null)
                {
                    _user.RefreshToken = RefreshToken;
                    context.SaveChanges();
                }
                else
                {
                    TblRefreshToken tblRefreshtoken = new TblRefreshToken()
                    {
                        UserId = UserId,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = RefreshToken,
                        IsActive = true
                    };
                    context.TblRefreshTokens.Add(tblRefreshtoken);
                    context.SaveChanges();
                }

                return RefreshToken;
            }
        }
    }
}
