using System.Globalization;
using System.Text;

namespace PVPTrackInfo_Sorter {

    class Program {

        static void Main(string[] _args) {
            if (_args.Length == 0) {
                Console.WriteLine("Drag a PVPTrackInfo file onto this .exe file to start sorting.");
                return;
            }

            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            foreach (string arg in _args) {
                if (Path.GetExtension(arg) != ".xlt") {
                    Console.WriteLine($"Invalid file: \"{arg}\"");
                    continue;
                }

                Sorter.Do(arg, GetEncoding(arg));
                Console.WriteLine($"Successfully converted: \"{arg}\"");
            }

            Console.WriteLine();
            Console.Read();
        }

        /// <summary>
        /// determines and returns the encoding of the provided file.
        /// </summary>
        /// <returns>Encoding</returns>
        public static Encoding GetEncoding(string filename) {
            byte[] bom = new byte[4];
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                file.Read(bom, 0, 4);
            }

            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            return Encoding.ASCII;
        }
    }
}