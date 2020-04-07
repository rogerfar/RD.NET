using System;
using System.IO;

namespace RDNET.Test
{
    public static class Setup
    {
        public const String APP_ID = "X245A4XAIBGVM";
        public const String DEVICE_CODE = "";
        public const String CLIENT_ID = "";
        public const String CLIENT_SECRET = "";
        public const String REFRESH_TOKEN = "";

        public static String ACCESS_TOKEN => File.ReadAllText("secret.txt");
    }
}
