using System.Runtime.InteropServices;

namespace PlotManager.Security.FileScanner
{
    public static class FileMimeTypeScanner
    {
        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        private static extern int FindMimeFromData(IntPtr pBC,
                                                   [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
                                                   [MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.I1, SizeParamIndex=3)]
                                                   byte[] pBuffer,
                                                   int cbSize,
                                                   [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
                                                   int dwMimeFlags,
                                                   out IntPtr ppwzMimeOut,
                                                   int dwReserved);

        public static string GetMimeFromFile(string file)
        {
            IntPtr mimeout;
            if (!File.Exists(file))
                throw new FileNotFoundException(file + " not found");
            int MaxContent = (int)new FileInfo(file).Length;
            if (MaxContent > 4096) MaxContent = 4096;
            FileStream fs = File.OpenRead(file);
            byte[] buf = new byte[MaxContent];
            fs.Read(buf, 0, MaxContent);
            fs.Close();
            int result = FindMimeFromData(IntPtr.Zero, file, buf, MaxContent, null!, 0, out mimeout, 0);
            if (result != 0)
                throw Marshal.GetExceptionForHR(result)!;
            string? mime = Marshal.PtrToStringUni(mimeout);
            Marshal.FreeCoTaskMem(mimeout);
            return mime!;
        }
    }
}