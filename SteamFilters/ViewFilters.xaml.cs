using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SteamWebAPI2;
using SteamWebAPI2.Interfaces;
using RLSApi;
using RLSApi.Data;
using RLSApi.Net.Requests;
using IgdbApi.Models;
using IgdbApi.Search;
using IronWebScraper;
using HtmlAgilityPack;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SteamFilters
{
    /// <summary>
    /// Interaction logic for ViewFilters.xaml
    /// </summary>
    public partial class ViewFilters : Page
    {
        private static string username;
        private static ulong userKey;
        private const string APIKey = "1B59E2F2D73EF60A0641C772346D378E";
        private const string rocketLeagueAPIKey = "SA338AXCJ0QNQTLH4ZQQ21SZLKMZ5MVY";
        private const string GDBApiKey = "e21a025dad569b35ccbca98cb58963ef";
        private static uint numOfGames = 0;
        public static List<SteamGame> listOfGames = new List<SteamGame>();
        private static IgdbApi.IIgdbGamesApi gamesApi;
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorder = null;
        private CurrentUser currentUser = null;

        /// <summary>
        /// Initialises a new ViewFilters page
        /// </summary>
        /// <param name="user">Steam username</param>
        /// <param name="key">Steam user Id</param>
        public ViewFilters(string user, ulong key)
        {
            InitializeComponent();
            username = user;
            userKey = key;
            LoadUser();
            LoadSavedGames();
            gamesApi = new IgdbApi.IgdbApi(GDBApiKey);
        }

        /// <summary>
        /// Will update the list of games by calling Steam API and scraping steam store pages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void UpdateGames(object sender, RoutedEventArgs e)
        {
            listOfGames = new List<SteamGame>();
            LoadingSpinner.Visibility = Visibility.Visible;
            var player = new PlayerService(APIKey);
            var ownedGames = await player.GetOwnedGamesAsync(userKey, true);
            var games = ownedGames.Data.OwnedGames;
            Debug.Print("Owned games: " + ownedGames.Data.GameCount.ToString());
            numOfGames = ownedGames.Data.GameCount;

            var orderedGames = games.OrderBy(game => game.Name);
            SteamGame steamGameObject;

            foreach (var game in orderedGames)
            {
                Debug.Print(game.Name + ": " + game.PlaytimeForever);

                steamGameObject = new SteamGame();
                steamGameObject.Id = game.AppId;
                steamGameObject.Name = game.Name;
                steamGameObject.Playtime = game.PlaytimeForever;
                steamGameObject.PlaytimeLastTwoWeeks = game.PlaytimeLastTwoWeeks;
                steamGameObject.ImgIconUrl = "http://media.steampowered.com/steamcommunity/public/images/apps/" + game.AppId + "/" + game.ImgIconUrl + ".jpg";
                steamGameObject.ImgLogoUrl = "http://media.steampowered.com/steamcommunity/public/images/apps/" + game.AppId + "/" + game.ImgLogoUrl + ".jpg";
                steamGameObject.SteamPageUrl = "https://store.steampowered.com/app/" + game.AppId;

                listOfGames.Add(steamGameObject);
            }

            await Task.Factory.StartNew(() => ScrapeSteam());
            gamesListView.ItemsSource = listOfGames;
            LoadingSpinner.Visibility = Visibility.Hidden;
            File.Delete("SavedGames" + username + ".xml");
            SaveSerializeObject<List<SteamGame>>(listOfGames, "SavedGames" + username + ".xml");
        }

        /// <summary>
        /// Loads list of games from xml file if previously saved
        /// </summary>
        public void LoadSavedGames()
        {
            listOfGames = DeSerializeObject<List<SteamGame>>("SavedGames" + username + ".xml");
            gamesListView.ItemsSource = listOfGames;
        }

        /// <summary>
        /// Sorts a ListView column based on string alphabetical order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HeaderSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = (sender as GridViewColumnHeader);
            string sort = header.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorder);
                gamesListView.Items.SortDescriptions.Clear();
            }
            ListSortDirection dir = ListSortDirection.Ascending;
            if (listViewSortCol == header && listViewSortAdorder.Direction == dir)
            {
                dir = ListSortDirection.Descending;
            }
            listViewSortCol = header;
            listViewSortAdorder = new SortAdorner(listViewSortCol, dir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorder);
            gamesListView.Items.SortDescriptions.Add(new SortDescription(sort, dir));
        }

        /// <summary>
        /// Will call steam API to update user details
        /// </summary>
        public async void UpdateUser(object sender, RoutedEventArgs e)
        {
            CurrentUser currentUser = new CurrentUser();
            var steamInterface = new SteamUser(APIKey);
            var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync(userKey);
            var playerSummaryData = playerSummaryResponse.Data;
            currentUser.AvatarUrl = playerSummaryResponse.Data.AvatarFullUrl;
            Debug.Print("AVATAR: " + currentUser.AvatarUrl);
            currentUser.CreationDate = playerSummaryResponse.Data.AccountCreatedDate;
            currentUser.ProfileUrl = playerSummaryResponse.Data.ProfileUrl;

            var accountCreationDate = playerSummaryData.AccountCreatedDate;
            var nickname = playerSummaryData.Nickname;
            Debug.Print("Account creation date: " + accountCreationDate.ToString());
            Debug.Print("Username: " + nickname);

            var friendsListResponse = await steamInterface.GetFriendsListAsync(userKey);
            var friendsList = friendsListResponse.Data;

            foreach (var friend in friendsList)
            {
                Debug.Print("Friend: " + friend.SteamId.ToString());
            }

            var userStats = new SteamUserStats(APIKey);
            var steamStats = new SteamApps(APIKey);

            AvatarImage.Source = new BitmapImage(new Uri(currentUser.AvatarUrl));
            UserNameText.Text = "Username: " + username;
            AccountCreationDateText.Text = "Account creation date: " + currentUser.CreationDate;

            File.Delete("SavedUser" + username + ".xml");
            SaveSerializeObject<CurrentUser>(currentUser, "SavedUser" + username + ".xml");
        }

        /// <summary>
        /// Loads a users information saved in xml file
        /// </summary>
        public void LoadUser()
        {
            currentUser = DeSerializeObject<CurrentUser>("SavedUser" + username + ".xml");
            AvatarImage.Source = new BitmapImage(new Uri(currentUser.AvatarUrl));
            UserNameText.Text = "Username: " + username;
            AccountCreationDateText.Text = "Account creation date: " + currentUser.CreationDate;
        }

        /// <summary>
        /// Will call RLS Api to get current season statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GetRocketLeagueStats(object sender, RoutedEventArgs e)
        {
            string results = "";
            try
            {
                var client = new RLSClient(rocketLeagueAPIKey);
                var player = await client.GetPlayerAsync(RlsPlatform.Steam, userKey.ToString());
                var currentSeason = player.RankedSeasons.FirstOrDefault(x => x.Key == RlsSeason.Seven);
                if (currentSeason.Value != null)
                {
                    Debug.Write("Rocket League player name: " + player.DisplayName);
                    results += ("Welcome " + player.DisplayName + "!\n\n");
                    foreach (var playerRank in currentSeason.Value)
                    {
                        Debug.Write(playerRank.Key + ": " + playerRank.Value.RankPoints + " rating");
                        results += (playerRank.Key + ": " + playerRank.Value.RankPoints + " rating\n");
                    }
                }
            }
            catch (Exception error)
            {
                Debug.Print(error.Message);
                MessageBoxResult errBox = MessageBox.Show("No player found");
            }
            RLStatsLabel.Content = results;
        }

        /// <summary>
        /// Gathers game information from steam webpage
        /// </summary>
        public void ScrapeSteam()
        {
            var getHtml = new HtmlWeb();

            for (int i = 0; i < listOfGames.Count; i++)
            {
                var game = listOfGames[i];
                var doc = getHtml.Load("https://store.steampowered.com/app/" + game.Id);
                try
                {
                    var prices = doc.DocumentNode.SelectNodes("//div[@class='game_purchase_price price']");
                    listOfGames[i].Price = Regex.Replace(prices[0].InnerHtml, @"\t|\n|\r", "");
                    var reviews = doc.DocumentNode.SelectNodes("//div[@class='user_reviews_summary_row']");

                    var recent = "";
                    var overall = "";
                    if (reviews.Count == 1)
                    {
                        overall = reviews[0].Attributes["data-tooltip-text"].Value;
                    }
                    else if (reviews.Count >= 2)
                    {
                        recent = reviews[0].Attributes["data-tooltip-text"].Value;
                        overall = reviews[1].Attributes["data-tooltip-text"].Value;
                    }

                    recent = recent.Replace(" user reviews in the last 30 days are positive.", "");
                    recent = recent.Replace(" the", "");
                    recent = recent.Replace("$", "");
                    overall = overall.Replace(" user reviews for this game are positive.", "");
                    overall = overall.Replace(" the", "");
                    overall = overall.Replace("$", "");
                    listOfGames[i].RecentReviews = recent;
                    listOfGames[i].OverallReviews = overall;
                }
                catch (Exception error)
                {
                    Debug.Print(game.Name + ": " + error.Message);
                    game.Price = "";
                }
            }
        }

        /// <summary>
        /// Link to Rocket League Stats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RLStats(object sender, RoutedEventArgs e)
        {
            Process.Start("https://rocketleaguestats.com/");
        }

        /// <summary>
        /// Link to Steam game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SteamGameLink(object sender, RoutedEventArgs e)
        {
            var selectedObject = gamesListView.SelectedItem as SteamGame;
            Process.Start(selectedObject.SteamPageUrl);
        }

        /// <summary>
        /// Link to user profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewUserProfile(object sender, RoutedEventArgs e)
        {
            Process.Start(currentUser.ProfileUrl);
        }

        /// <summary>
        /// Saves a serialisable object to local xml file for later viewing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject">Object to save</param>
        /// <param name="filename">File path to save to</param>
        public void SaveSerializeObject<T>(T serializableObject, string filename)
        {
            if (serializableObject == null)
                return;

            try
            {
                XmlDocument doc = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    doc.Load(stream);
                    doc.Save(filename);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }

        /// <summary>
        /// Loads object from xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of xml file where object is stored</param>
        /// <returns>Requested object</returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return default(T);
            }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            return objectOut;
        }
    }

    /// <summary>
    /// Container holds variables for a Steam game
    /// </summary>
    [Serializable]
    public class SteamGame
    {
        public string Name { get; set; }
        public uint Id { get; set; }
        public TimeSpan Playtime { get; set; }
        public TimeSpan? PlaytimeLastTwoWeeks { get; set; }
        public string ImgIconUrl { get; set; }
        public string ImgLogoUrl { get; set; }
        public string SteamPageUrl { get; set; }
        public string Price { get; set; }
        public string OverallReviews { get; set; }
        public string RecentReviews { get; set; }
    }

    /// <summary>
    /// Container holds variables for the current user logged in
    /// </summary>
    [Serializable]
    public class CurrentUser
    {
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public List<string> Friends { get; set; }
        public DateTime CreationDate { get; set; }
        public string ProfileUrl { get; set; }
    }

    /// <summary>
    /// Scrape steam store
    /// </summary>
    class SteamScraper : WebScraper
    {
        private List<string> urls = new List<string>();
        private int counter = 0;

        public override void Init()
        {
            this.LoggingLevel = WebScraper.LogLevel.All;
            
            foreach (var game in ViewFilters.listOfGames)
            {
                urls.Add("https://store.steampowered.com/app/" + game.Id);
            }
            this.Request(urls, Parse);
        }
        public override void Parse(Response response)
        {
            string html = response.Html;

            counter++;
        }
    }

    /// <summary>
    /// An indicator for which column is currently being sorted
    /// </summary>
    public class SortAdorner : Adorner
    {
        private static Geometry ascendingTriangle = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
        private static Geometry descendingTriangle = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");
        public ListSortDirection Direction { get; set; }

        public SortAdorner(UIElement element, ListSortDirection dir) : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (AdornedElement.RenderSize.Width < 20)
            {
                return;
            }

            TranslateTransform transform = new TranslateTransform(
                AdornedElement.RenderSize.Width - 15,
                (AdornedElement.RenderSize.Height - 5) / 2);
            drawingContext.PushTransform(transform);

            Geometry geometry = ascendingTriangle;
            if (this.Direction == ListSortDirection.Descending)
            {
                geometry = descendingTriangle;
            }
            drawingContext.DrawGeometry(Brushes.Blue, null, geometry);
            drawingContext.Pop();
        }
    }
}
