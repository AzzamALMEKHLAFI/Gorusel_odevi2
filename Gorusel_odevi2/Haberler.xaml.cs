using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;


using System.Text.Json;

namespace Gorusel_odevi2
{
    public partial class Haberler : ContentPage
    {
        public Haberler()
        {
            InitializeComponent();
            category.ItemsSource = Kategori.liste;
            Load();


        }
        Root root;
        Kategori kategori;
        async Task Load()
        {
            string jsondata = await HaberleriGetir(kategori);
            root = JsonSerializer.Deserialize<Root>(jsondata);
            lsHaberler.ItemsSource = root.items;
        }

        private async void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var l = (e.CurrentSelection.FirstOrDefault() as Kategori)?.Baslik;



            if (l != null)
            {
                foreach (var category in Kategori.liste)
                {
                    if (category.Baslik.Equals(l))
                    {
                        kategori = category;

                        break;
                    }
                }

            }


            await Load();



        }

        


        public static async Task<string> HaberleriGetir(Kategori ctg)
        {
            if (ctg == null)
            {
                HttpClient client = new HttpClient();
                string url = "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/ekonomi_articles.rss";
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsondata = await response.Content.ReadAsStringAsync();
                return jsondata;
            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    string url = $"https://api.rss2json.com/v1/api.json?rss_url={ctg.Link}";
                    using HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsondata = await response.Content.ReadAsStringAsync();
                    return jsondata;
                }
                catch
                {
                    return null;
                }
            }

        }
        private void lsHaberler_metot(object sender, SelectionChangedEventArgs e)
        {

            int index = (e.CurrentSelection.Count);
            Item item = root.items[index];

            HaberDetay newsDetail = new HaberDetay(item);

            Navigation.PushModalAsync(newsDetail);

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Load();
        }
    }

    public class Kategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }
        public static List<Kategori> liste = new List<Kategori>()
 {
 new Kategori() { Baslik = "Manşet", Link = "https://www.trthaber.com/manset_articles.rss"},
 new Kategori() { Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
 new Kategori() { Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
 new Kategori() { Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
 new Kategori() { Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
 new Kategori() { Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
 new Kategori() { Baslik = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss"},
 new Kategori() { Baslik = "Eğitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
 };
    }


    public class Enclosure
    {
        public string link { get; set; }
        public string type { get; set; }
    }

    public class Feed
    {
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public Enclosure enclosure { get; set; }
        public List<object> categories { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Feed feed { get; set; }
        public List<Item> items { get; set; }
    }
}