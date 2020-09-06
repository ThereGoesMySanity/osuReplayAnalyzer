using System;
using System.Collections.Generic;
using System.IO;

namespace OsuDbAPI
{
    /*
        Holds the data for the osu!.db file
        Reference: https://osu.ppy.sh/wiki/Db_%28file_format%29
    */
    public class OsuDbFile
    {
        public int Version
        {
            get; set;
        }
        public int FolderCount
        {
            get; set;
        }
        public bool AccountUnlocked
        {
            get; set;
        }
        public DateTime DateUntilUnlock
        {
            get; set;
        }
        public string PlayerName
        {
            get; set;
        }
        public int NumBeatmaps
        {
            get; set;
        }
        public List<Beatmap> Beatmaps
        {
            get; set;
        }
        public Dictionary<int, Beatmap> BeatmapsById
        {
            get; set;
        }
        public Dictionary<string, Beatmap> BeatmapsByHash
        {
            get; set;
        }

        private BinaryReader fileReader;

        public OsuDbFile(string fname, bool byId = false, bool byHash = false)
        {
            if(fname.Length == 0)
            {
                return;
            }

            this.fileReader = new BinaryReader(new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read));
            this.Version = this.fileReader.ReadInt32();
            this.FolderCount = this.fileReader.ReadInt32();
            this.AccountUnlocked = this.fileReader.ReadBoolean();
            this.DateUntilUnlock = new DateTime(this.fileReader.ReadInt64());
            this.PlayerName = this.readNullableString();
            this.NumBeatmaps = this.fileReader.ReadInt32();
            this.Beatmaps = new List<Beatmap>(this.NumBeatmaps);
            if (byHash) this.BeatmapsByHash = new Dictionary<string, Beatmap>(this.NumBeatmaps);
            if (byId) this.BeatmapsById = new Dictionary<int, Beatmap>(this.NumBeatmaps);
            for(int i = 0; i < this.NumBeatmaps; i++)
            {
                Beatmap b = this.readBeatmap();
                Beatmaps.Add(b);
                if (byHash) BeatmapsByHash[b.Hash] = b;
                if (byId) BeatmapsById[b.ID] = b;
            }
            this.fileReader.Dispose();
        }

        public Beatmap GetBeatmapFromHash(string mapHash)
        {
            if (BeatmapsByHash != null) return BeatmapsByHash[mapHash];
            else return Beatmaps.Find(b => b.Hash == mapHash);
        }
        public Beatmap GetBeatmapFromId(int id)
        {
            if (BeatmapsById != null) return BeatmapsById[id];
            else return Beatmaps.Find(b => b.ID == id);
        }

        private string readNullableString()
        {
            if(this.fileReader.ReadByte() != 0x0B)
            {
                return null;
            }
            return this.fileReader.ReadString();
        }

        private void readIntDoublePair()
        {
            // no need for star difficulty, so we don't keep this data
            this.fileReader.ReadByte();
            this.fileReader.ReadInt32();
            this.fileReader.ReadByte();
            this.fileReader.ReadDouble();
        }

        private void readTimingPoint()
        {
            // no need for timing point, so we don't keep this data either
            this.fileReader.ReadDouble();
            this.fileReader.ReadDouble();
            this.fileReader.ReadBoolean();
        }

        private Beatmap readBeatmap()
        {
            int n;
            if (Version < 20191106) this.fileReader.ReadInt32();
            Beatmap beatmap = new Beatmap();
            beatmap.ArtistName = this.readNullableString();
            beatmap.ArtistNameUnicode = this.readNullableString();
            beatmap.SongTitle = this.readNullableString();
            beatmap.SongTitleUnicode = this.readNullableString();
            beatmap.Creator = this.readNullableString();
            beatmap.Difficulty = this.readNullableString();
            beatmap.AudioFile = this.readNullableString();
            beatmap.Hash = this.readNullableString();
            beatmap.OsuFile = this.readNullableString();
            beatmap.RankedStatus = this.fileReader.ReadByte();
            beatmap.NumHitcircles = this.fileReader.ReadUInt16();
            beatmap.NumSliders = this.fileReader.ReadUInt16();
            beatmap.NumSpinners = this.fileReader.ReadUInt16();
            beatmap.LastModificationTime = new DateTime(this.fileReader.ReadInt64());
            beatmap.ApproachRate = this.fileReader.ReadSingle();
            beatmap.CircleSize = this.fileReader.ReadSingle();
            beatmap.HPDrain = this.fileReader.ReadSingle();
            beatmap.OverallDifficulty = this.fileReader.ReadSingle();
            beatmap.SliderVelocity = this.fileReader.ReadDouble();
            for(int i = 0; i < 4; i++)
            {
                n = this.fileReader.ReadInt32();
                for(int j = 0; j < n; j++)
                {
                    this.readIntDoublePair();
                }
            }
            beatmap.DrainTimeSeconds = this.fileReader.ReadInt32();
            beatmap.DrainTimeMilliseconds = this.fileReader.ReadInt32();
            beatmap.PreviewPoint = this.fileReader.ReadInt32();
            n = this.fileReader.ReadInt32();
            for(int j = 0; j < n; j++)
            {
                this.readTimingPoint();
            }
            beatmap.ID = this.fileReader.ReadInt32();
            beatmap.SetID = this.fileReader.ReadInt32();
            beatmap.ThreadID = this.fileReader.ReadInt32();
            beatmap.GradeOsu = this.fileReader.ReadByte();
            beatmap.GradeTaiko = this.fileReader.ReadByte();
            beatmap.GradeCTB = this.fileReader.ReadByte();
            beatmap.GradeMania = this.fileReader.ReadByte();
            beatmap.LocalOffset = this.fileReader.ReadUInt16();
            beatmap.StackLeniency = this.fileReader.ReadSingle();
            beatmap.GameMode = this.fileReader.ReadByte();
            beatmap.Source = this.readNullableString();
            beatmap.Tags = this.readNullableString();
            beatmap.OnlineOffset = this.fileReader.ReadUInt16();
            beatmap.Font = this.readNullableString();
            beatmap.Unplayed = this.fileReader.ReadBoolean();
            beatmap.LastPlayed = new DateTime(this.fileReader.ReadInt64());
            beatmap.Osz2 = this.fileReader.ReadBoolean();
            beatmap.FolderName = this.readNullableString();
            beatmap.LastCheck = new DateTime(this.fileReader.ReadInt64());
            beatmap.IgnoreHitSounds = this.fileReader.ReadBoolean();
            beatmap.IgnoreSkin = this.fileReader.ReadBoolean();
            beatmap.DisableStoryboard = this.fileReader.ReadBoolean();
            beatmap.DisableVideo = this.fileReader.ReadBoolean();
            beatmap.VisualOverride = this.fileReader.ReadBoolean();
            beatmap.LastModTime = this.fileReader.ReadInt32();
            beatmap.ManiaScrollSpeed = this.fileReader.ReadByte();

            return beatmap;
        }
    }
}
