using QuotesSong.Forms;
using QuotesSong.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;

namespace QuotesSong
{
    /// <summary>
    /// General Class with static operations(read-write to db, additional operations etc.)
    /// </summary>
    public class Crud
    {
        #region variables and properties
        public static bool ontop = false;
        public string[] listCountry
        {
            get
            {
                List<string> listc = new List<string>();
                listc.AddRange(Settings.Default.listCountries.Cast<string>());
                listc.Add("");
                return listc.ToArray();
            }
        }
        public string[] listLanguage
        {
            get
            {
                List<string> listl = new List<string>();
                listl.AddRange(Settings.Default.listLang.Cast<string>());
                listl.Add("");
                return listl.ToArray();
            }
        }

        #endregion

        #region CRUD Operations to db
        public static int GetRadiostationId(string radio)
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select Id from Radiostation where Radiostation.Name like @radio", conf))
                {
                    cmd.Parameters.AddWithValue("@radio", radio);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32((reader.GetValue(0)));
                        }
                        reader.Close();
                    }
                }
                return 0;
            }
        }
        public static async void InsertIntoSongs(List<SongClass> listsong)
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                foreach (var song in listsong)
                {
                    if (song != null)
                    {
                        SQLiteCommand cmd = new SQLiteCommand("select COUNT(*) from Song where Name like @name and Author like @author ", conf);
                        cmd.Parameters.AddWithValue("@name", song.Name);
                        cmd.Parameters.AddWithValue("@author", song.Author);

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetValue(0).ToString() == "0")
                            {
                                using (var cmd2 = new SQLiteCommand("insert into Song (Name, Author, Country, Language) values (@name, @author, @country, @language) ", conf))
                                {

                                    cmd2.Parameters.AddWithValue("@name", song.Name);
                                    cmd2.Parameters.AddWithValue("@author", song.Author);
                                    cmd2.Parameters.AddWithValue("@country", song.Country);
                                    cmd2.Parameters.AddWithValue("@language", song.Language);
                                    await cmd2.ExecuteNonQueryAsync();
                                }

                            }
                        }
                        reader.Close();
                    }
                }
            }
        }
        public static void InsertIntoSongs(string name, string author, string country, string language)
        {

            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                SQLiteCommand cmd = new SQLiteCommand("select COUNT(*) from Song where Name like @name and Author like @author ", conf);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@author", author);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(0).ToString() == "0")
                    {
                        cmd = new SQLiteCommand("insert into Song (Name, Author, Country, Language) values (@name, @author, @country, @language) ", conf);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@author", author);
                        cmd.Parameters.AddWithValue("@country", country);
                        cmd.Parameters.AddWithValue("@language", language);
                        cmd.ExecuteNonQuery();
                    }
                }
                reader.Close();
            }
        }
        public static void InsertIntoRadiostation(string radiostation)
        {

            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                SQLiteCommand cmd = new SQLiteCommand("select COUNT(*) from Radiostation where Radiostation.Name like @radiostation", conf);
                cmd.Parameters.AddWithValue("@radiostation", radiostation);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(0).ToString() == "0")
                    {
                        cmd = new SQLiteCommand("insert into Radiostation (Name) values (@radiostation)", conf);
                        cmd.Parameters.AddWithValue("@radiostation", radiostation);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void InsertIntoPlaylist(List<SongClass> songs, string radiostation, string site)
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();

                int radiostationid = 0;
                SQLiteCommand cmd = new SQLiteCommand("select Id from RadioStation where Radiostation.Name like @radiostation", conf);
                cmd.Parameters.AddWithValue("@radiostation", radiostation);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    radiostationid = Convert.ToInt32(reader.GetValue(0).ToString());
                }
                reader.Close();

                foreach (var song in songs)
                {
                    if (song != null)
                    {
                        string datestr = song.dt != null ? song.dt.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                        int songid = 0;
                        cmd = new SQLiteCommand("select Id from Song where Song.Name like @name and Song.Author like @author", conf);
                        cmd.Parameters.AddWithValue("@author", song.Author);
                        cmd.Parameters.AddWithValue("@name", song.Name);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            songid = Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                        reader.Close();

                        cmd = new SQLiteCommand("insert into Playlist (station_id, song_id, duration, datetimesong, site) values (@stationid, @songid, @duration, @datetimesong, @site) ", conf);
                        cmd.Parameters.AddWithValue("@stationid", radiostationid);
                        cmd.Parameters.AddWithValue("@songid", songid);
                        cmd.Parameters.AddWithValue("@duration", song.Duration);
                        cmd.Parameters.AddWithValue("@datetimesong", datestr);
                        cmd.Parameters.AddWithValue("@site", site);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void InsertIntoPlaylist(string name, string author, string radiostation, int duration, string site, DateTime? date)
        {

            string datestr = date != null ? date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();

                int radiostationid = 0;
                SQLiteCommand cmd = new SQLiteCommand("select Id from RadioStation where Radiostation.Name like @radiostation", conf);
                cmd.Parameters.AddWithValue("@radiostation", radiostation);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    radiostationid = Convert.ToInt32(reader.GetValue(0).ToString());
                }
                reader.Close();

                int songid = 0;
                cmd = new SQLiteCommand("select Id from Song where Song.Name like @name and Song.Author like @author", conf);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@name", name);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    songid = Convert.ToInt32(reader.GetValue(0).ToString());
                }
                reader.Close();

                cmd = new SQLiteCommand("insert into Playlist (station_id, song_id, duration, datetimesong, site) values (@stationid, @songid, @duration, @datetimesong, @site) ", conf);
                cmd.Parameters.AddWithValue("@stationid", radiostationid);
                cmd.Parameters.AddWithValue("@songid", songid);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@datetimesong", datestr);
                cmd.Parameters.AddWithValue("@site", site);
                cmd.ExecuteNonQuery();

            }
        }
        public static void UpdatePlaylist(int id, string name, string author, int duration)
        {

            int songid = 0;
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                using (var cmd = new SQLiteCommand("select Id from Song where Song.Name like @name and Song.Author like @author", conf))
                {
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@name", name);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            songid = Convert.ToInt32(reader.GetValue(0).ToString());
                        }
                    }

                }
                using (var cmd2 = new SQLiteCommand("update Playlist set song_id = @songid, duration=@duration where id=@id", conf))
                {
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.Parameters.AddWithValue("@songid", songid);
                    cmd2.Parameters.AddWithValue("@duration", duration);

                    cmd2.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteFromPlaylist(int id)
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                SQLiteCommand cmd = new SQLiteCommand("delete * from playlist where id=@id", conf);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public static void ClearPlayList()
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                SQLiteCommand cmd = new SQLiteCommand("delete  from playlist", conf);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<string> GetAllRadiostations()
        {
            List<string> radios = new List<string>();
            using (var con = new SQLiteConnection())
            {
                con.ConnectionString = Settings.Default.SongDBConnectionString;
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(string.Format("select Name from Radiostation order by name"), con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    radios.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                return radios;
            }
        }
        public static DateTime GetMinDate()
        {
            DateTime datetime = DateTime.MinValue;
            using (var con = new SQLiteConnection())
            {
                con.ConnectionString = Settings.Default.SongDBConnectionString;
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(string.Format("select min(datetimesong) from Playlist"), con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime.TryParse(reader.GetValue(0).ToString(), out datetime);
                }
                return datetime;
            }

        }
        public static DateTime GetMaxDate()
        {
            DateTime datetime = DateTime.MinValue;
            using (var con = new SQLiteConnection())
            {
                con.ConnectionString = Settings.Default.SongDBConnectionString;
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(string.Format("select max(datetimesong) from Playlist"), con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime.TryParse(reader.GetValue(0).ToString(), out datetime);
                }
                return datetime;
            }
        }

        #endregion
       
        #region additional operations
        public static string BuildSelectForMain(string type, DateTime dtFrom, DateTime dtTo)
        {
            string[] items;
            if (type == "Language")
                items = new Crud().listLanguage;
            else
                items = new Crud().listCountry;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT radio.Name");
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    items[i] = items[i].Replace("'", "''");
                    sb.AppendFormat(" , printf(\u0022 % .2f\u0022, (printf(\u0022 % .2f\u0022, t{0}sum.t{0}sum)/printf(\u0022 % .2f\u0022, tsum.allsum))*100) as {1}", i, items[i]);
                }
            }
            sb.Append(" , printf(\u0022  %i\u0022,cnt.unknowncounters) as unk");

            sb.AppendFormat(" FROM Radiostation radio  left JOIN (SELECT PlayList.station_id, SUM(PlayList.duration) AS allsum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where Playlist.Datetimesong between \u0022{0}\u0022 and \u0022{1}\u0022 GROUP BY PlayList.station_id) tsum ON radio.Id = tsum.station_id", DateTimeSQLite(dtFrom), DateTimeSQLite(dtTo));
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    sb.AppendFormat("  left JOIN(SELECT PlayList.station_id, SUM(PlayList.duration) AS t{0}sum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where {1} = '{2}' and Playlist.Datetimesong between \u0022{3}\u0022 and \u0022{4}\u0022 GROUP BY PlayList.station_id) t{0}sum ON radio.Id = t{0}sum.station_id", i, type, items[i], DateTimeSQLite(dtFrom), DateTimeSQLite(dtTo));
                }
            }
            sb.AppendFormat(" left join (select PlayList.station_id, count(*) as unknowncounters  from playlist join song on playlist.song_id=song.Id where ({0} = '' or {0} is null) and (Playlist.Datetimesong between \u0022{1}\u0022 and \u0022{2}\u0022) and (playlist.song_id<>0)GROUP BY PlayList.station_id) cnt on radio.id=cnt.station_id", type, DateTimeSQLite(dtFrom), DateTimeSQLite(dtTo));
            sb.Append(" GROUP BY radio.Name");
            return sb.ToString();
        }
        public static string BuildSelectForChartOneDay(string type, DateTime dtFrom, DateTime dtTo, string radiostation)
        {
            radiostation = radiostation.Replace("'", "''");
            string[] items;
            if (type == "Language")
                items = new Crud().listLanguage;
            else
                items = new Crud().listCountry;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT radio.Name");
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    items[i] = items[i].Replace("'", "''");
                    sb.AppendFormat(" , printf(\u0022 % .2f\u0022, (printf(\u0022 % .2f\u0022, t{0}sum.t{0}sum)/printf(\u0022 % .2f\u0022, tsum.allsum))*100) as {1}", i, items[i]);
                }
            }

            sb.AppendFormat(" FROM Radiostation radio left JOIN (SELECT PlayList.station_id, SUM(PlayList.duration) AS allsum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where Playlist.Datetimesong >= \u0022{0}\u0022 and Playlist.Datetimesong <\u0022{1}\u0022 GROUP BY PlayList.station_id) tsum ON radio.Id = tsum.station_id", DateTimeSQLiteChart(dtFrom), DateTimeSQLiteChart(dtTo));
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    sb.AppendFormat(" left JOIN(SELECT PlayList.station_id, SUM(PlayList.duration) AS t{0}sum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where {1} = '{2}' and Playlist.Datetimesong >= \u0022{3}\u0022 and Playlist.Datetimesong < \u0022{4}\u0022 GROUP BY PlayList.station_id) t{0}sum ON radio.Id = t{0}sum.station_id", i, type, items[i], DateTimeSQLiteChart(dtFrom), DateTimeSQLiteChart(dtTo));
                }
            }
            sb.AppendFormat(" GROUP BY radio.Name having radio.name like '{0}'", radiostation);
            return sb.ToString();

        }
        public static string BuildSelectForChart(string type, DateTime dtFrom, DateTime dtTo, string radiostation)
        {
            radiostation = radiostation.Replace("'", "''");
            string[] items;
            if (type == "Language")
                items = new Crud().listLanguage;
            else
                items = new Crud().listCountry;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT radio.Name");
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    items[i] = items[i].Replace("'", "''");
                    sb.AppendFormat(" , printf(\u0022 % .2f\u0022, (printf(\u0022 % .2f\u0022, t{0}sum.t{0}sum)/printf(\u0022 % .2f\u0022, tsum.allsum))*100) as {1}", i, items[i]);
                }
            }

            sb.AppendFormat(" FROM Radiostation radio left JOIN (SELECT PlayList.station_id, SUM(PlayList.duration) AS allsum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where Playlist.Datetimesong between \u0022{0}\u0022 and \u0022{1}\u0022 GROUP BY PlayList.station_id) tsum ON radio.Id = tsum.station_id", DateTimeSQLite(dtFrom), DateTimeSQLite(dtTo));
            for (int i = 0; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items[i]))
                {
                    sb.AppendFormat(" left JOIN(SELECT PlayList.station_id, SUM(PlayList.duration) AS t{0}sum FROM PlayList  JOIN Song ON PlayList.song_id = Song.Id where {1} = '{2}' and Playlist.Datetimesong between \u0022{3}\u0022 and \u0022{4}\u0022 GROUP BY PlayList.station_id) t{0}sum ON radio.Id = t{0}sum.station_id", i, type, items[i], DateTimeSQLite(dtFrom), DateTimeSQLite(dtTo));
                }
            }
            sb.AppendFormat(" GROUP BY radio.Name having radio.name like '{0}'", radiostation);
            return sb.ToString();

        }
        public static string DateTimeSQLiteChart(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH");
        }
        public static string DateTimeSQLite(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string DateTimeSQLiteForRadioscope(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
        public static int GetMinAndSecFromText(string str)
        {
            String[] minsec = str.Split(':');
            return Convert.ToInt32(minsec[0]) * 60 + Convert.ToInt32(minsec[1]);
        }
        public static void LoadEmptyLangWindow()
        {
            if (GetEmptyLangCounts() > 0)
            {
                DBSongForm form = new DBSongForm(" where (Language='' or Country ='')");
                form.ShowDialog();
            }
        }
        public static int GetEmptyLangCounts()
        {
            using (SQLiteConnection conf = new SQLiteConnection())
            {
                conf.ConnectionString = Settings.Default.SongDBConnectionString;
                conf.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select count(*) from song where Language='' or Country = ''", conf))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32((reader.GetValue(0)));
                        }
                        reader.Close();
                    }
                }
                return 0;
            }
        }
        public static string ReturnUkrLangForSong(string name)
        {
            if (name.ToLower().Contains("і") || name.ToLower().Contains("ї") || name.ToLower().Contains("є"))
                return "Українська";
            return string.Empty;
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
