using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using CodeHollow.FeedReader;
using HtmlAgilityPack;
namespace Gorusel_odevi2
{

    public partial class News : ContentPage
    {
        public News()
        {
            InitializeComponent();
        }


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
   
