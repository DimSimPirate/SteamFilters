using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using SteamWebAPI2;
using SteamWebAPI2.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SteamFilters
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private const string APIKey = "1B59E2F2D73EF60A0641C772346D378E";
        static HttpClient client = new HttpClient();
        private static string userFilePath = "Users.txt";
        private List<string> savedUsers = new List<string>();
        private static ulong userKey = 0;
        private string username = "";

        /// <summary>
        /// Initialise home page
        /// </summary>
        public Home()
        {
            InitializeComponent();
            SavedPlayers();
        }

        /// <summary>
        /// Reads list of users saved from text file
        /// </summary>
        private void SavedPlayers()
        {
            try
            {
                using (StreamReader reader = new StreamReader(userFilePath))
                {
                    string user;
                    while ((user = reader.ReadLine()) != null)
                    {
                        Debug.WriteLine(user);
                        savedUsers.Add(user);
                    }
                }
                reloadSavedPlayers();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Reloads the list of players when updates are made
        /// </summary>
        private void reloadSavedPlayers()
        {
            peopleListBox.Items.Clear();
            foreach (string user in savedUsers)
            {
                if (user.Equals("")) continue;
                var item = new ListBoxItem();
                item.Name = user;
                item.Content = user;
                peopleListBox.Items.Add(item);
            }
        }

        /// <summary>
        /// Will call Steam api to find manually entered username, navigate to new page if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TypedUser(object sender, RoutedEventArgs e)
        {

            username = UsernameTextBox.Text;
            Debug.WriteLine(username);
            bool valid = await CheckUsername(username);

            if (valid)
            {
                if (!savedUsers.Contains(username))
                {
                    try
                    {
                        File.AppendAllText(userFilePath, "\n" + username);
                    }
                    catch (Exception error)
                    {
                        Debug.Print(error.Message);
                    }
                }
                ViewFilters filterPage = new ViewFilters(username, userKey);
                this.NavigationService.Navigate(filterPage);
            }
            else
            {

            }
        }
    
        /// <summary>
        /// Will call Steam api to find player from saved list, then navigate to new page if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SelectedUser(object sender, RoutedEventArgs e)
        {
            var listBox = peopleListBox;
            var listBoxItem = (ListBoxItem)peopleListBox.SelectedValue;
            username = listBoxItem.Content.ToString();
            bool valid = await CheckUsername(username);

            if (valid)
            {
                ViewFilters filterPage = new ViewFilters(username, userKey);
                this.NavigationService.Navigate(filterPage);
            }
        }

        /// <summary>
        /// Polls the Steam api to see if the player exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Bool: true if successful</returns>
        public static async Task<bool> CheckUsername(string username)
        {
            Debug.Print("Searching for steam user ...");
            var steamInterface = new SteamUser(APIKey);
            SteamWebAPI2.Utilities.ISteamWebResponse<ulong> userKeyResponse;
            try
            {
                userKeyResponse = await steamInterface.ResolveVanityUrlAsync(username);
                userKey = userKeyResponse.Data;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                MessageBoxResult error = MessageBox.Show("No player found");
                return false;
            }
            Debug.Print(userKey.ToString());
            return true;
        }

        /// <summary>
        /// Deletes a user from local storage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            try
            {
                var listBox = peopleListBox;
                var listBoxItem = (ListBoxItem)peopleListBox.SelectedValue;
                var selectedUser = listBoxItem.Content.ToString();
                savedUsers.Remove(selectedUser);
                //File.WriteAllText(userFilePath, string.Empty);
                TextWriter writer = new StreamWriter(userFilePath);
                foreach (string user in savedUsers)
                {
                    writer.WriteLine(user);
                }
                
            }
            catch (Exception error)
            {
                Debug.Print(error.Message);
            }
            reloadSavedPlayers();
        }
    }
}
