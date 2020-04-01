using System;
using System.IO;

namespace RDNET.Test
{
    public static class Setup
    {
        public const String APP_ID = "X245A4XAIBGVM";
        public const String APP_SECRET = "";
        public const String DEVICE_CODE = "";
        public const String CLIENT_ID = "";
        public const String ACCESS_TOKEN = "";
        public const String REFRESH_TOKEN = "";

        public static String CLIENT_SECRET => File.ReadAllText("secret.txt");
    }
}
