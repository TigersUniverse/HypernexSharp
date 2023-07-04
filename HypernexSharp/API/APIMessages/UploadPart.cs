using System;
using System.Collections.Generic;
using System.IO;
using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIMessages
{
    internal class UploadPart : APIMessage
    {
        private FileStream file { get; }
        private string TemporaryDirectory { get; }
        public string OriginalFileName { get; set; }
        public string ChunkId { get; set; } = String.Empty;
        private int ChunkNumber => maxAmount - streams.Count;
        private int AmountOfChunks => maxAmount;
        
        private string userid { get; }
        private string tokenContent { get; }
        private AvatarMeta avatarMeta { get; }
        private WorldMeta worldMeta { get; }

        public bool CanUpload => streams.Count > 0;
        private int maxAmount;
        private Queue<FileStream> streams = new Queue<FileStream>();

        protected override string Endpoint => "uploadPart";
        
        protected override (FileStream, Dictionary<string, string>) GetFileForm()
        {
            Dictionary<string, string> collection = new Dictionary<string, string>();
            collection.Add("originalFileName", OriginalFileName);
            collection.Add("chunkNumber", ChunkNumber.ToString());
            collection.Add("amountOfChunks", AmountOfChunks.ToString());
            collection.Add("chunkId", ChunkId);
            collection.Add("userid", userid);
            collection.Add("tokenContent", tokenContent);
            if(avatarMeta != null)
                collection.Add("avatarMeta", avatarMeta.GetNode().ToString());
            else if(worldMeta != null)
                collection.Add("worldMeta", worldMeta.GetNode().ToString());
            FileStream fs = streams.Dequeue();
            return (fs, collection);
        }
        
        private FileStream CreateFile(string path, byte[] arr)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,
                FileShare.ReadWrite | FileShare.Delete);
            fileStream.Write(arr, 0, arr.Length);
            fileStream.Dispose();
            FileStream readStream = new FileStream(path, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);
            return readStream;
        }

        internal void SplitStreams()
        {
            if (!Directory.Exists(TemporaryDirectory))
                Directory.CreateDirectory(TemporaryDirectory);
            else
            {
                Directory.Delete(TemporaryDirectory, true);
                Directory.CreateDirectory(TemporaryDirectory);
            }
            foreach (FileStream fileStream in streams)
                fileStream.Dispose();
            streams.Clear();
            // Split each one by 90MB
            List<byte> current = new List<byte>();
            int max = 1048576 * 90;
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            byte[] data = ms.ToArray();
            string path;
            for (int i = 0; i < data.Length; i++)
            {
                if (current.Count > max)
                {
                    path = Path.Combine(TemporaryDirectory, "file-" + streams.Count);
                    streams.Enqueue(CreateFile(path, current.ToArray()));
                    current.Clear();
                }
                current.Add(data[i]);
            }
            if(current.Count > 0)
            {
                path = Path.Combine(TemporaryDirectory, "file-" + streams.Count);
                streams.Enqueue(CreateFile(path, current.ToArray()));
            }
            maxAmount = streams.Count;
            current.Clear();
            ms.Dispose();
        }

        public UploadPart(string userid, string tokenContent, FileStream file, string TemporaryDirectory)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.TemporaryDirectory = TemporaryDirectory;
        }
        
        public UploadPart(string userid, string tokenContent, FileStream file, string TemporaryDirectory, AvatarMeta avatarMeta)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.avatarMeta = avatarMeta;
            this.TemporaryDirectory = TemporaryDirectory;
        }
        
        public UploadPart(string userid, string tokenContent, FileStream file, string TemporaryDirectory, WorldMeta worldMeta)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.worldMeta = worldMeta;
            this.TemporaryDirectory = TemporaryDirectory;
        }
    }
}