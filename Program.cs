using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

internal class Program {

    private static string PublicKey = String.Empty;
    private static string PrivateKey = String.Empty;

    private static void Main(string[] args) {

        if (args.Count() > 1) {
            if (args[0] == "--sign") {
                SignData(args[1]);
                return;
            }
            //if (args[0] == "--verify") {
            //    VerifySignature(args[1], args[2]);
            //    return;
            //}
        }

        Console.WriteLine("Signature Tool");
        Console.WriteLine("The data is expected to be a base64 string");
        Console.WriteLine("\t --sign [data to sign...]");
        //Console.WriteLine("\t --verify [original data] [signed data]");
    }


    private static void SignData(string data) {

        string signedData = String.Empty;

        using (var rsa = new RSACryptoServiceProvider()) {
            rsa.ImportFromPem(File.ReadAllText(Path.GetFullPath("public.key")));
            rsa.ImportFromPem(File.ReadAllText(Path.GetFullPath("private.key")));
            signedData = Convert.ToBase64String(rsa.SignData(Convert.FromBase64String(data), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
        }

        Console.WriteLine($"Data   : {data}");
        Console.WriteLine($"Signed : {signedData}");

    }

    //private static void VerifySignature(string original, string signed) {

    //}
}