using System;
using System.IO;
using System.Text;
using Shell32;

class mp3ID3 {
    static void Main() {
        string currentDir = System.Environment.CurrentDirectory;
        ShellClass shell = new ShellClass();
        Folder folder = shell.NameSpace(currentDir);
        DirectoryInfo di = new DirectoryInfo(currentDir);
        FileInfo[] fiary = di.GetFiles("*.mp3");
        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        StreamWriter sw = new StreamWriter(currentDir + "\\" + "log.txt", false, sjisEnc);

        foreach (FileInfo fi in fiary) {
            string artistName = folder.GetDetailsOf(folder.ParseName(fi.Name), 13); // アーティスト名
            string albumName = folder.GetDetailsOf(folder.ParseName(fi.Name), 14);  // アルバム名
            string artistDir = currentDir + "\\" + artistName;
            string albumDir = artistDir + "\\" + albumName;
            DirectoryInfo diNewArtistFolder = new DirectoryInfo(artistDir);
            DirectoryInfo diNewAlbumFolder = new DirectoryInfo(albumDir);

            if (!File.Exists(artistDir)) {
                diNewArtistFolder.Create();
            }
            if (!File.Exists(albumDir)) {
                diNewAlbumFolder.Create();
            }

            sw.WriteLine(fi.FullName);
            sw.WriteLine("を");
            try {
                fi.MoveTo(albumDir+"\\"+fi.Name);
            } catch (Exception e) {
                Console.Error.WriteLine(e);
                Console.Error.WriteLine("何かキーを押してください.");
                Console.ReadLine();
                return;
            }
            sw.WriteLine(fi.FullName);
            sw.WriteLine("に移動しました。");
            sw.WriteLine();
        }

        sw.Close();
    }
}
