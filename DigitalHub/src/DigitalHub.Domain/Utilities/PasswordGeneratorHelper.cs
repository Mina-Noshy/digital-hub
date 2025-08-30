using DigitalHub.Domain.Entities.Auth.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DigitalHub.Domain.Utilities;

public static class PasswordGeneratorHelper
{
    private static readonly char[] LowerLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private static readonly char[] UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private static readonly char[] Digits = "0123456789".ToCharArray();
    private static readonly char[] Specials = "!@#$%^&*()_-+=[]{}|:'\",.<>/?~".ToCharArray();

    public static string GeneratePassword(
        int length = 12,
        int minUpper = 1,
        int minLower = 1,
        int minDigits = 1,
        int minSpecials = 1)
    {
        if (length < 6)
            throw new ArgumentException("Password length must be at least 4");

        if (minUpper + minLower + minDigits + minSpecials > length)
            throw new ArgumentException("Minimum character counts exceed password length");

        using var rng = RandomNumberGenerator.Create();

        var passwordChars = new char[length];

        // Ensure required characters are included
        int index = 0;
        index = AddRandomChars(UpperLetters, minUpper, rng, passwordChars, index);
        index = AddRandomChars(LowerLetters, minLower, rng, passwordChars, index);
        index = AddRandomChars(Digits, minDigits, rng, passwordChars, index);
        index = AddRandomChars(Specials, minSpecials, rng, passwordChars, index);

        // Fill remaining positions with random characters from all categories
        var allChars = LowerLetters.Concat(UpperLetters)
                                   .Concat(Digits)
                                   .Concat(Specials)
                                   .ToArray();

        for (int i = index; i < length; i++)
        {
            passwordChars[i] = allChars[GetRandomValue(rng, allChars.Length)];
        }

        // Shuffle the array to avoid predictable patterns
        Shuffle(passwordChars, rng);

        return new string(passwordChars);
    }

    public static string HashPassword(UserMaster user, string password)
    {
        var passwordHasher = new PasswordHasher<UserMaster>();
        return passwordHasher.HashPassword(user, password);
    }










    private static int AddRandomChars(char[] charSet, int count, RandomNumberGenerator rng,
        char[] passwordChars, int index)
    {
        for (int i = 0; i < count; i++)
        {
            passwordChars[index++] = charSet[GetRandomValue(rng, charSet.Length)];
        }
        return index;
    }

    private static int GetRandomValue(RandomNumberGenerator rng, int max)
    {
        byte[] uintBuffer = new byte[sizeof(uint)];

        while (true)
        {
            rng.GetBytes(uintBuffer);
            uint randomUint = BitConverter.ToUInt32(uintBuffer, 0);
            if (randomUint < max * (uint.MaxValue / max))
                return (int)(randomUint % max);
        }
    }

    private static void Shuffle(char[] array, RandomNumberGenerator rng)
    {
        int n = array.Length;
        while (n > 1)
        {
            int i = GetRandomValue(rng, n--);
            (array[n], array[i]) = (array[i], array[n]);
        }
    }
}