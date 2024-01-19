using System;

namespace Gorusel_odevi2
{

    public partial class HaberDetay : ContentPage
    {
        Item news;

        public HaberDetay(Item item)
        {
            InitializeComponent();
            news = item;
            webView.Source = item.link;
        }

        private async void ShareClicked(object sender, EventArgs e)
        {
            await ShareUri(news.link, Share.Default);
        }

        public async Task ShareUri(string uri, IShare share)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = news.title
            });
        }
    }
}