using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static async Task Main(string[] args)
    {
        // GET Method 

        string url = @"https://jsonplaceholder.typicode.com/posts";

    /* string url = @"*/
    https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5";

        /*using (var client = new HttpClient())
        {
            var responce = await client.GetAsync(url);

            Console.WriteLine($"Status :: {responce.StatusCode}");
            var result = await responce.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }*/

        /*var post = new Post()
        {
            Title = "NewTitle",
            Body = "NewBody",
            UserId = 1
        };

        // POST Method 

        var json = JsonConvert.SerializeObject(post);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using(var client = new HttpClient())
        {
            var responce = await client.PostAsync(url, data);
            Console.WriteLine($"Status :: {responce.StatusCode}");
            var result = await responce.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }*/


        
        string urlImg = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAjDQ80mUWopbjiKrAOpxJIErLkSqwmnJpdJRn5MhnKA&s=10";

        // way 1

        /*HttpClient client = new HttpClient();
        HttpRequestMessage message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(urlImg)
        };

        var responce = await client.SendAsync(message);
        Console.WriteLine($"Status :: {responce.StatusCode}");
        using (FileStream fs = new FileStream(Path.Combine(desktopPath, Path.GetFileName(urlImg)), FileMode.Create))
        {
            await responce.Content.CopyToAsync(fs);
        }*/



        // way 2

        /*var img2 = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRC2KSPmd-WDIPRG1IIv1cS6AlBlYu2FFm9QJ-CXx0gvQ&s=10";

        *//*HttpClient client = new HttpClient();

        byte[] data = await client.GetByteArrayAsync(img2);

        System.IO.File.WriteAllBytes(Path.Combine(desktopPath, "img.jpg"), data);*//*

        WebClient client = new WebClient();
        client.DownloadFile(new Uri(@"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzbbOEXsHQr21TaL2PuRF4jo9-9oHfbf7rX-Qp9Qr1nA&s=10"), desktopPath + "/2.jpg");
        Console.WriteLine("File loaded");*/
    }

    private static async void DownloadFileAsync(string address)
    {
        WebClient client = new WebClient();

        client.DownloadFileCompleted += Client_DownloadComplated;
        client.DownloadProgressChanged += Client_DownloadProgresschanged;

        string fileName = Path.Combine(desktopPath, Path.GetFileName(address));
         
        await client.DownloadFileTaskAsync(address, fileName);

        Console.WriteLine("File loaded");
    }

    private static void Client_DownloadComplated(object? sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
            Console.WriteLine("File downloads was cancelled");
        else
            Console.WriteLine("File downloaded succesfully!");
    }
    private static void Client_DownloadProgresschanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Console.WriteLine($"Downloading ..... \t {(float)e.BytesReceived / 1024 / 1024}Mb of {(float)e.TotalBytesToReceive / 1024 / 1024}Mb \t {e.ProgressPercentage}%");
    }

}