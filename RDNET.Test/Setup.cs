using System;
using System.IO;

namespace RDNET.Test;

public static class Setup
{
    public static String API_KEY => File.ReadAllText("secret.txt");
}