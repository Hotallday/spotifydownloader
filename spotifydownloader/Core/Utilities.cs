using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using spotifydownloader.Core.Classes;

namespace spotifydownloader.Core {
    public class Utilities {
        public void CreateFoler() {
            if (!Directory.Exists("./.temp/")) {
                Directory.CreateDirectory("./.temp/");
            }
            if (!Directory.Exists("./.music/")) {
                Directory.CreateDirectory("./.music/");
            }
            if (!Directory.Exists("./.libs/")) {
                Directory.CreateDirectory("./.libs/");
            }
        }

        public async void Convert(Song song) {
            await Task.Factory.StartNew(() => {
                if (File.Exists("./libs/ffmpeg.exe")) {
                    using (var proccess = new Process()) {
                        proccess.StartInfo.UseShellExecute = false;
                        proccess.StartInfo.CreateNoWindow = true;
                        proccess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proccess.StartInfo.FileName = "./libs/ffmpeg.exe";
                        if (string.IsNullOrEmpty(song.AlbumName)) {
                        }
                        proccess.StartInfo.Arguments = "ffmpeg -i filename.mp4 -b:a 192K -vn filename.mp3";
                    }
                }
            });
        }
    }
}