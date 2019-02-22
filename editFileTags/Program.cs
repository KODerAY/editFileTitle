//https://github.com/judwhite/IdSharp
using System;
using System.IO;
using IdSharp.Common.Utils;
using IdSharp.Tagging.ID3v1;
using IdSharp.Tagging.ID3v2;
using IdSharp.Tagging.VorbisComment;

namespace IdSharp.Example.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Müziklerinizin bulunduğu dosya yolunu giriniz.");
            Console.WriteLine("İçindeki bütün müziklerin başlıklarını dosya adına eşitleyeceğiz.");
            Console.WriteLine("  Bu işlemi yapmak istemiyorsanız programı kapatınız!!");
            Console.WriteLine("Bu program şu sorunun sonucunda yazılmıştır:");
            Console.WriteLine("Telefonumda bulunan müzik çalar programımda müzikler dosya ismi ile değilde dosya başlık ismiyle gözüküyordu.");
            Console.WriteLine("Tek tek elle değiştirmek yerine bu programla tek seferde müzik isimlerini telefonda da olduğu gibi görebilirsiniz.");
            string[] fileNames = GetFileName(args);
            if (fileNames == null)
            {
                Console.WriteLine("Geçersiz dosya yolu!!");
                Console.ReadLine();
                return;
            }
            for(int i = 0; i < fileNames.Length; i++)
            {
                if (!File.Exists(fileNames[i]))
                {
                    string tryFileName = Path.Combine(Environment.CurrentDirectory, fileNames[i]);
                    if (!File.Exists(tryFileName))
                    {
                        fileNames[i] = tryFileName;
                    }
                    else
                    {
                        Console.WriteLine("\"{0}\" BULUNAMADI.", fileNames[i]);
                        continue;
                    }
                }
                if (ID3v2Tag.DoesTagExist(fileNames[i]))
                {
                    IID3v2Tag id3v2 = new ID3v2Tag(fileNames[i]);

                    //Console.WriteLine("{1}. Title: {0}", id3v2.Title, i);
                    id3v2.Title = Path.GetFileNameWithoutExtension(fileNames[i]);
                    //Console.WriteLine("Dosya adı:" + Path.GetFileNameWithoutExtension(fileNames[i]));
                    id3v2.Save(fileNames[i]);
                }
            }
            Console.ReadLine();
           
        }
            

        private static string[] GetFileName(string[] args)
        {
            string[] fileNames = null;
            if (args.Length == 1)
            {
                fileNames = Directory.GetFiles(args[0], "*.mp3");
            }
            else
            {
                Console.Write("Dosya ismi: ");
                string dir = Console.ReadLine();
                if (System.IO.Directory.Exists(dir))
                {
                    fileNames = Directory.GetFiles(dir, "*.mp3");
                }
            }
            return fileNames;
        }
    }
}
