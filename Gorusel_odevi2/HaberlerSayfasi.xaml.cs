using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using CodeHollow.FeedReader;
using HtmlAgilityPack;
using Xamarin.Essentials;


namespace Gorusel_odevi2 {

    public partial class HaberlerSayfasi : ContentPage
    {

        private const string RssUrl = "https://www.trthaber.com/spor_articles.rss";
        private const string SondakikaRssUrl = "https://www.trthaber.com/sondakika_articles.rss";
        private const string GundemRssUrl = "https://www.trthaber.com/gundem_articles.rss";
        private const string EkonomiRssUrl = "https://www.trthaber.com/ekonomi_articles.rss";
        private const string BilimTeknolojiRssUrl = "https://www.trthaber.com/bilim_teknoloji_articles.rss";

        public HaberlerSayfasi()
        {
            InitializeComponent();
        }

        private async void OnCategoryButtonClicked(string rssUrl, string title)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var rssFeed = await httpClient.GetStringAsync(rssUrl);
                    var feed = FeedReader.ReadFromString(rssFeed);

                    var categoryPage = new ContentPage
                    {
                        Title = title,
                        Content = await CreateNewsListView(feed.Items)
                    };

                    await Navigation.PushAsync(categoryPage);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private async void OnSonDakikaButtonClicked(object sender, EventArgs e)
        {
            OnCategoryButtonClicked(SondakikaRssUrl, "Son Dakika Haberler");
        }

        private async void OnGundemButtonClicked(object sender, EventArgs e)
        {
            OnCategoryButtonClicked(GundemRssUrl, "Gündem Haberler");
        }

        private async void OnEkonomiButtonClicked(object sender, EventArgs e)
        {
            OnCategoryButtonClicked(EkonomiRssUrl, " Ekonomi Haberler");
        }

        private async void OnSporButtonClicked(object sender, EventArgs e)
        {
            OnCategoryButtonClicked(RssUrl, " Spor  Haberler");
        }

        private async void OnBilimTeknolojiButtonClicked(object sender, EventArgs e)
        {
            OnCategoryButtonClicked(BilimTeknolojiRssUrl, " Bilim ve Teknoloji Haberler");
        }

        

        private async void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Implement your news refreshing logic here
                // You can call the respective RSS feed URLs and update the news data

                // For example, if you want to refresh the Spor news
                using (var httpClient = new HttpClient())
                {
                    var rssFeed = await httpClient.GetStringAsync(RssUrl);
                    var feed = FeedReader.ReadFromString(rssFeed);

                    // Update the ListView with the new data
                    var listView = await CreateNewsListView(feed.Items);
                    // Assuming you have a ListView named 'newsListView' in your XAML
                    // newsListView.ItemsSource = listView.ItemsSource;
                }

                // Display a message indicating successful refresh
                DisplayAlert("Success", "News refreshed successfully.", "OK");
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private bool shouldShareAfterNavigation = false;
        private string webViewPageTitle = "";

        private async void OnShareButtonClicked(object sender, EventArgs e)
        {
            var selectedItem = ((View)sender)?.BindingContext as NewsItem;

            if (selectedItem != null)
            {
                try
                {
                    // Fetch the HTML content of the article page
                    var htmlContent = await GetHtmlContent(selectedItem.Link);

                    // Log the HTML content for debugging
                    System.Diagnostics.Debug.WriteLine($"HTML Content: {htmlContent}");

                    // Implement your share functionality here
                    // You can use the Xamarin.Essentials Share API directly
                    await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
                    {
                        Text = $"{selectedItem.Title}\n{selectedItem.Link}",
                        Title = "Share News"
                    });

                    // Log a message indicating that sharing was successful
                    System.Diagnostics.Debug.WriteLine("Sharing successful");
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");

                    // Handle exceptions appropriately
                    DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }





        private void WebViewNavigated(object sender, WebNavigatedEventArgs e)
        {
            // Check if sharing should be performed after navigation
            if (shouldShareAfterNavigation)
            {
                shouldShareAfterNavigation = false; // Reset the flag
                var webView = sender as WebView;
                if (webView != null)
                {
                    // Unsubscribe from the event to avoid unnecessary calls
                    webView.Navigated -= WebViewNavigated;

                    // Implement your share functionality here
                    // You can use the Xamarin.Essentials Share API
                    ShareUri(webView.Source.ToString(), webViewPageTitle);
                }
            }
        }


        public async Task ShareUri(string uri, string title)
        {
            await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }




        private async Task<View> CreateNewsListView(IEnumerable<FeedItem> items)
        {
            var newsData = await GetNewsData(items);

            // Create a ListView to display the news
            var listView = new ListView
            {
                ItemsSource = newsData,
                ItemTemplate = new DataTemplate(() =>
                {
                    var title = new Label();
                    title.SetBinding(Label.TextProperty, "Title");

                    var summary = new Label();
                    summary.SetBinding(Label.TextProperty, "Summary");

                    var link = new Label();
                    link.SetBinding(Label.TextProperty, "Link");

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "Image");
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (sender, e) =>
                    {
                        var selectedItem = ((View)sender)?.BindingContext as NewsItem;

                        if (selectedItem != null)
                        {
                            // Fetch the HTML content of the article page
                            var htmlContent = await GetHtmlContent(selectedItem.Link);

                            // Display the HTML content in a WebView
                            var webViewPage = new ContentPage
                            {
                                Title = selectedItem.Title
                            };

                            var webView = new WebView
                            {
                                Source = new HtmlWebViewSource { Html = htmlContent },
                                VerticalOptions = LayoutOptions.FillAndExpand
                            };

                            // Add a "Share" button to the toolbar
                            var shareButton = new ToolbarItem
                            {
                                Text = "Share",
                                Command = new Command(async () =>
                                {
                                    // Implement your share functionality here
                                    // You can use the Xamarin.Essentials Share API
                                    await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
                                    {
                                        Text = $"{selectedItem.Title}\n{selectedItem.Link}",
                                        Title = "Share News"
                                    });
                                })
                            };

                            // Add the toolbar item to the page
                            webViewPage.ToolbarItems.Add(shareButton);

                            webViewPage.Content = webView;

                            // Push the WebViewPage without using await Navigation.PushAsync
                            // to avoid potential navigation issues
                            Navigation.PushAsync(webViewPage);

                        }
                    };

                    // Add the tap gesture recognizer to each element in the ViewCell
                    title.GestureRecognizers.Add(tapGestureRecognizer);
                    summary.GestureRecognizers.Add(tapGestureRecognizer);
                    link.GestureRecognizers.Add(tapGestureRecognizer);
                    image.GestureRecognizers.Add(tapGestureRecognizer);


                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(10),
                            Children =
                            {
                                title,
                                summary,
                                link,
                                image
                            }
                        }
                    };
                })
            };

            return listView;
        }








        private async Task<List<NewsItem>> GetNewsData(IEnumerable<FeedItem> items)
        {
            var newsData = new List<NewsItem>();

            foreach (var item in items)
            {
                var htmlContent = await GetHtmlContent(item.Link);
                var (title, text, imageUrl) = ExtractArticleData(htmlContent);

                newsData.Add(new NewsItem
                {
                    Title = title,
                    Summary = item.Description, // You can still use the RSS summary if needed
                    Link = item.Link,
                    Image = imageUrl
                });
            }

            return newsData;
        }


        private async Task<string> GetHtmlContent(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(url);
            }
        }

        private (string Title, string Text, string Image) ExtractArticleData(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='page-title']");
            var textNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='description']");
            var imageNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='image-frame with-icon']/a/picture/img");

            var title = titleNode?.InnerText.Trim();
            var text = textNode?.InnerText.Trim();
            var imageUrl = imageNode?.GetAttributeValue("src", null);

            return (title, text, imageUrl);
        }



        private string ExtractImageUrl(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Select the first <source> element within the <picture> element
            var sourceNode = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'standard-right-thumb-card')]/div[@class='image-frame']/a/picture/source");

            // If <source> element is found, get its "data-srcset" attribute
            if (sourceNode != null)
            {
                return sourceNode.GetAttributeValue("data-srcset", null);
            }

            // If <source> element is not found, try to get the <img> element directly
            var imgNode = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'standard-right-thumb-card')]/div[@class='image-frame']/a/picture/img");

            return imgNode?.GetAttributeValue("data-src", null);
        }

    }
        public class NewsItem
        {
            public string Title { get; set; }
            public string Summary { get; set; }
            public string Link { get; set; }
            public string Image { get; set; }
        }

    }
