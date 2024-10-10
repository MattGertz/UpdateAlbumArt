// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

using TagLib;
using File = System.IO.File;

namespace UpdateAlbumArt
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error: No directory specified.");
                Console.WriteLine("Usage: UpdateAlbumArt <directory>");
                return;
            }

            string newDirectory = args[0].Trim();
            if (!Directory.Exists(newDirectory))
            {
                Console.WriteLine("Error: The specified directory does not exist.");
                Console.WriteLine("Usage: UpdateAlbumArt <directory>");
                return;
            }

            Directory.SetCurrentDirectory(newDirectory);
            CheckDirectory();
        }

        private static void CheckDirectory()
        {
            try
            {
                string[] albumArtFiles = { @".\folder.jpg", @".\ZuneAlbumArt.jpg" };
                string? albumArtPath = albumArtFiles.FirstOrDefault(File.Exists);

                if (albumArtPath == null)
                {
                    Console.WriteLine("No album art found in directory {0}, skipping.", Directory.GetCurrentDirectory());
                } 
                else 
                {
                    var albumArt = new TagLib.Picture(albumArtPath)
                    {
                        Type = PictureType.FrontCover,
                        Description = "Cover",
                        MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg
                    };

                    foreach (var filename in Directory.GetFiles(@".\"))
                    {
                        try
                        {
                            if (filename.EndsWith(".mp3") || filename.EndsWith(".wma"))
                            {
                                TagLib.File file = TagLib.File.Create(filename);
                                var coverArt = file.Tag.Pictures;

                                if (coverArt.Length == 0)
                                {
                                    Console.Write("Album art not found: {0}...", filename);
                                    file.Tag.Pictures = new IPicture[] { albumArt };
                                    file.Save();
                                    Console.WriteLine("added album art.");
                                }
                                else
                                {
                                    Console.WriteLine("Album art found: {0}, skipping.", filename);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error processing file {0}: {1}", filename, ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking directory: {0}", ex.Message);
            }

            foreach (var directory in Directory.GetDirectories(@".\"))
            {
                try
                {
                    string oldDirectory = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(directory);
                    CheckDirectory();
                    Directory.SetCurrentDirectory(oldDirectory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing directory {0}: {1}", directory, ex.Message);
                }
            }
        }
    }
}
