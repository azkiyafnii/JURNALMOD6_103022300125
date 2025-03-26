using System;
using System.Collections.Generic;
using System.Linq.Expressions;
public class SayaTubeVideo
{
    private int id;
    private string title;
    private int playCount;
    public SayaTubeVideo(string title) //konstruktor 
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("judul video tidak boleh null atau kosong.");
        }
        else if (title.Length > 200)
        {
            throw new ArgumentException("judul video tidak boleh lebih dari 200 karakter");
        }

        Random random = new Random();
        this.id = random.Next(1000, 9999);
        this.title = title;
        this.playCount = 0;
    }

    public int GetPlayCount() //method untuk mengambil jumlah play count
    {
        return playCount;
    }
    public String GetTitle() //methon untuk mengambil judul video
    {
        return title;
    }

    public void IncreasePlayCount(int count) //method untuk menambah jumlah tayangan video
    {
        if (count < 0)
        {
            throw new ArgumentException("Play count tidak boleh negatif ");
        }
        else if (count > 25000000000)
        {
            throw new ArgumentException("jumlah play count tidak boleh lebih dari 25.000.000");
        }
        try
        {
            checked
            {
                playCount += count;

            }
        }
        catch (OverflowException)

        {
            Console.WriteLine("ERROR: play count melebihi batas integer!");
        }

    }

    public void PrintVideoDetails() //method untuk mencetak detail video
    {
        Console.WriteLine($"Video ID: {id}");
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Play Count: {playCount}");
    }
}

public class SayaTubeUser
{
    private int id;
    private List<SayaTubeVideo> uploadedVideos;
    public string Username { get; private set; }

    public SayaTubeUser(string username) //konstruktor
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("username tidak boleh null atau kosong.");
        }
        else if (username.Length > 100)
        {
            throw new ArgumentException("usrename tidak boleh lebih dari 100 karakter");
        }

        Random random = new Random();
        this.id = random.Next(100, 999);
        this.Username = username;
        this.uploadedVideos = new List<SayaTubeVideo>();
    }

    public void AddVideo(SayaTubeVideo video) //method untuk menambahkan video ke dlm daftar uploadedVideos
    {
        if (video == null)
        {
            throw new ArgumentException("video tidak boleh null");
        }
        else if (video.GetPlayCount() >= int.MinValue)
        {
            throw new ArgumentException("play count video sudah mencapai batas maksimum.");
        }
        uploadedVideos.Add(video);
    }

    public int GetTotalVideoPlayCount() //method untuk menghid=tung total playcount
    {
        int total = 0;
        foreach (var video in uploadedVideos)
        {
            total += video.GetPlayCount();
        }
        return total;
    }

    public void PrintAllVideoPlayCount() //method untuk mencetak daftar semua video
    {
        Console.WriteLine($"user: {Username}");
        int maxPrint = Math.Min(uploadedVideos.Count, 8);

        for (int i = 0; i < maxPrint; i++)
        {
            Console.WriteLine($"video {i + 1} judul: {uploadedVideos[i].GetTitle()}");
        }
    }
}

class program
{
    static void Main()
    {
        try
        {
            SayaTubeUser user = new SayaTubeUser("Azki");
            string[] filmTitles = //daftar judul film
            {
                "Review Shigenki No Kyojin oleh Azki",
                "Review Film Jujutsu Kaisen oleh Azki",
                "Review Film Boku No Hero oleh Azki",
                "Review Film Tenki No Ko oleh Azki",
                "Review Film Ao No Exorcist oleh Azki",
                "Review Film Kimi No Nawa oleh Azki",
                "Review Film Toilet Bound Hanako-kun oleh Azki",
                "Review Film Kimetsu No Yaiba oleh Azki"
            };

            foreach (var title in filmTitles) //mebambahkan setiap video ke dlm akun user
            {
                SayaTubeVideo video = new SayaTubeVideo(title);
                video.IncreasePlayCount(new Random().Next(1, 1000));
                user.AddVideo(video);
            }
            user.PrintAllVideoPlayCount(); //mencetak semua video
            Console.WriteLine($"Total Play Count: {user.GetTotalVideoPlayCount()}"); //mencetak total jumlah play count

            SayaTubeVideo testVideo = new SayaTubeVideo("uji overflow play count."); //menguji exception
            for (int i = 0; i < 100; i++)
            {
                testVideo.IncreasePlayCount(25000000);
            }
            testVideo.PrintVideoDetails();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
