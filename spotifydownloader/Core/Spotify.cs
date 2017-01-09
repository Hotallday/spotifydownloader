using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using spotifydownloader.Core.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace spotifydownloader.Core {
    public class Spotify {
        public async Task<Song> DownloadSongInformationAsync(string id) {
            int tries = 0;
        Over:
            try {
                string result;
                using (var web = new WebClient()) {
                    result = await web.DownloadStringTaskAsync("https://api.spotify.com/v1/tracks/" + id);
                }
                var info = JsonConvert.DeserializeObject<JObject>(result);
               var song = new Song {
                    Id = id,
                    AlbumName = (string)info["album"]["name"],
                    SongName = (string)info["name"],
                    Image = (string)info["album"]["images"][0]["url"],
                    Ico = (string)info["album"]["images"][2]["url"],
                    Artists = new List<string>(),
                    Doing = "Waiting...",
                    Length = 0,
                    Progress = 0,
                    YoutubeId = ""
                };

                foreach (var artist in info["artists"]) {
                    song.Artists.Add((string)artist["name"]);
                }
                return song;
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                await Task.Delay(2000);
                if (tries < 6) {
                    tries++;
                    goto Over;
                } else {
                    return new Song() {
                        Id = "",
                        AlbumName = "Error",
                        SongName = "Error",
                        Image = "Error",
                        Ico = "Error",
                        Artists = null,
                        Doing = "Error downloading song.",
                        Length = 0,
                        Progress = 0,
                        YoutubeId = ""
                    };
                }
            }
        }
    }
}