using CodingDays.Exceptions;

namespace CodingDays.Models.Config;
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
