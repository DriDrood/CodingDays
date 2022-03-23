using CodingDays.UserApi.Exceptions;

namespace CodingDays.UserApi.Models.Config;
public record SecretHolder
(
    string Secret
)
{
    public void ValidateSecret(string secret)
    {
        if (secret != Secret)
            throw new UsageException("Chybný secret");
    }
}
