using System.Security.Cryptography;
using System.Text;

string[] keysHex =
{
    "68544020247570407220244063724074",
    "54684020247570407220244063724074",
    "54684020247570407220244063727440"
};

string targetHash = "f28fe539655fd6f7275a09b7c3508a3f81573fc42827ce34ddf1ec8d5c2421c3";

byte[]? correctKey = null;

Console.WriteLine("Finding correct symmetric key...\n");

foreach (var keyHex in keysHex)
{
    byte[] keyBytes = HexToBytes(keyHex);

    using var sha256 = SHA256.Create();
    byte[] hash = sha256.ComputeHash(keyBytes);

    string hashHex = BytesToHex(hash);

    if (hashHex.Equals(targetHash, StringComparison.OrdinalIgnoreCase))
    {
        correctKey = keyBytes;
        Console.WriteLine($"Correct Key: {keyHex}\n");
        break;
    }
}

if (correctKey == null)
{
    Console.WriteLine("No matching key found.");
    return;
}

// AES Decryption
string cipherHex = "876b4e970c3516f333bcf5f16d546a87aaeea5588ead29d213557efc1903997e";
string ivHex = "656e6372797074696f6e496e74566563";

byte[] cipherBytes = HexToBytes(cipherHex);
byte[] iv = HexToBytes(ivHex);

string decryptedMessage = DecryptAES(cipherBytes, correctKey, iv);

Console.WriteLine($"Decrypted Message: {decryptedMessage}\n");

// Generate EC Key Pair
using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

byte[] publicKey = ecdsa.ExportSubjectPublicKeyInfo();

Console.WriteLine($"Public Key (Base64): {Convert.ToBase64String(publicKey)}\n");

// Digital Signature
byte[] messageBytes = Encoding.UTF8.GetBytes(decryptedMessage);

byte[] signature = ecdsa.SignData(messageBytes, HashAlgorithmName.SHA256);

Console.WriteLine($"Digital Signature (Base64): {Convert.ToBase64String(signature)}");

static string DecryptAES(byte[] cipherText, byte[] key, byte[] iv)
{
    using var aes = Aes.Create();

    aes.Key = key;
    aes.IV = iv;
    aes.Mode = CipherMode.CBC;
    aes.Padding = PaddingMode.PKCS7;

    using var decryptor = aes.CreateDecryptor();

    byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

    return Encoding.UTF8.GetString(decrypted);
}

static byte[] HexToBytes(string hex)
{
    return Enumerable.Range(0, hex.Length / 2)
        .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
        .ToArray();
}

static string BytesToHex(byte[] bytes)
{
    return BitConverter.ToString(bytes).Replace("-", "").ToLower();
}
