using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Text.Json;
using System.Drawing.Imaging;

public class AmosFile
{
    public string ZipFilePath { get; private set; }

    public AmosFile(string zipFilePath)
    {
        ZipFilePath = zipFilePath;
    }
    // Vytvoření zipu s obrázky a JSON
    public static void CreateZippedData(string outputZipPath, Dictionary<string, Bitmap> images, object jsonObject)
    {
        using (var zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // Přidání obrázků do zipu
            foreach (var imageEntry in images)
            {
                var zipEntry = zipArchive.CreateEntry(imageEntry.Key);
                using (var entryStream = zipEntry.Open())
                {
                    //entryStream.Write(imageEntry.Value, 0, imageEntry.Value.Length);
                    Bitmap imageToSave = new Bitmap(imageEntry.Value);
                    imageToSave.Save(entryStream, ImageFormat.Jpeg);
                }
            }

            // Přidání JSON souboru do zipu
            var jsonEntry = zipArchive.CreateEntry("data.json");
            using (var entryStream = jsonEntry.Open())
            using (var writer = new StreamWriter(entryStream))
            {
                var json = JsonSerializer.Serialize(jsonObject);
                writer.Write(json);
            }
        }
    }

    // Načtení JSONu ze zipu
    public T ReadJson<T>()
    {
        using (var zipStream = new FileStream(ZipFilePath, FileMode.Open))
        using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            var jsonEntry = zipArchive.GetEntry("data.json");
            if (jsonEntry == null)
                throw new FileNotFoundException("JSON file not found in the zip.");

            using (var entryStream = jsonEntry.Open())
            using (var reader = new StreamReader(entryStream))
            {
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }

    // Načtení obrázků ze zipu
    public Dictionary<string, Bitmap> ReadImages()
    {
        var images = new Dictionary<string, Bitmap>();

        using (var zipStream = new FileStream(ZipFilePath, FileMode.Open))
        using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read))
        {
            foreach (var entry in zipArchive.Entries)
            {
                if (entry.Name.EndsWith(".png") || entry.Name.EndsWith(".jpg"))
                {
                    using (var entryStream = entry.Open())
                    using (var memoryStream = new MemoryStream())
                    {
                        entryStream.CopyTo(memoryStream);
                        images[entry.Name] = new Bitmap(memoryStream);
                    }
                }
            }
        }

        return images;
    }
    private static Bitmap resize(Bitmap Image, int longestSide) 
    {
        Bitmap Outp;
        if (Image.Width > Image.Height)
        {
            Outp = new Bitmap(Image, new Size(longestSide, Image.Height*(longestSide/Image.Height)));
        }
        else 
        {
            Outp = new Bitmap(Image, new Size(Image.Width * (longestSide / Image.Width), longestSide));
        }
        return Outp;
    }
}
