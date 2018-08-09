using System;
using System.IO;
using AlphaTab.Importer;
using AlphaTab.IO;
using Newtonsoft.Json;

namespace tabpaser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin parser!");
            var p = new Program();
            p.TestScoreInfo();
        }

        internal byte[] Load(string name)
        {
            const string path = "TestFiles/";
            return File.ReadAllBytes(Path.Combine(path, name));
        }

        GpxImporter PrepareImporterWithBytes(byte[] buffer)
        {
            var readerBase = new GpxImporter();
            readerBase.Init(new StreamWrapper(new MemoryStream(buffer)));
            return readerBase;
        }

        GpxImporter PrepareImporterWithFile(string name)
        {
            return PrepareImporterWithBytes(Load(name));
        }

        public void TestScoreInfo()
        {
            var reader = PrepareImporterWithFile("GuitarPro6/Test01.gpx");
            var score = reader.ReadScore();
            Console.WriteLine(score.Tracks.Count);
            var json = JsonConvert.SerializeObject(score, Formatting.Indented, new JsonSerializerSettings{
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });
            Console.WriteLine(json);
        }
    }
}
