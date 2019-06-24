using System;
using System.Linq;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace XmlCrud.AppCode
{
    public class SongMethods
    {
        private DataSet ds = new DataSet("playlist");

        public SongMethods()
        {
        }

        public DataSet GetAllSongs(string pFile)
        {
            DataTable dtSongs = new DataTable("song");

            DataColumn dcId = new DataColumn("id");
            DataColumn dcArtist = new DataColumn("artist");
            DataColumn dcTitle = new DataColumn("title");
            DataColumn dcYear = new DataColumn("year");
            DataColumn dcGenre = new DataColumn("genre");
            DataColumn dcTime = new DataColumn("time");
            DataColumn dcFile = new DataColumn("file");

            dtSongs.Columns.Add(dcId);
            dtSongs.Columns.Add(dcArtist);
            dtSongs.Columns.Add(dcTitle);
            dtSongs.Columns.Add(dcYear);
            dtSongs.Columns.Add(dcGenre);
            dtSongs.Columns.Add(dcTime);
            dtSongs.Columns.Add(dcFile);
            ds.Tables.Add(dtSongs);

            //ds.ReadXml(HttpContext.Current.Server.MapPath(pFile));

            using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath(pFile)))
            {
                string json = r.ReadToEnd();
                ds = JsonConvert.DeserializeObject<DataSet>(json);
            }


            return ds;
        }

        public DataRow GetEmptyDataRow()
        {
            DataRow dr = ds.Tables["song"].NewRow();
            return dr;
        }


        public void CreateSong(DataRow dataRow, string file)
        {
            ds.Tables["song"].Rows.Add(dataRow);
            ds.WriteXml(HttpContext.Current.Server.MapPath(file));
        }

        public void DeleteSong(string id, string file)
        {
            DataRow[] drArray = ds.Tables["song"].Select("id = '" + id + "'");
            if (drArray != null && drArray.Length > 0)
            {
                drArray[0].Delete();
                ds.WriteXml(HttpContext.Current.Server.MapPath(file));
            }

        }

    }
}
 