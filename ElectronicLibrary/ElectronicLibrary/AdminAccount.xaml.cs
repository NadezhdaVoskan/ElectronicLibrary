using ElectronicLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElectronicLibraryAPI2.Models;
using ElectronicLibraryAPI.Models;
using System.Security.Policy;
using Publisher = ElectronicLibrary.Models.Publisher;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace ElectronicLibrary
{
    /// <summary>
    /// Логика взаимодействия для AdminAccount.xaml
    /// </summary>
    public partial class AdminAccount : Window
    {

        private string ip;
        private readonly string idUser;
        public AdminAccount()
        {
            InitializeComponent();

            Loaded += Window_Loaded;
            string apiUrl = ((App)App.Current).ApiUrl;
            ip = apiUrl;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Завершить работу приложения?", "Электронная библиотека Книжня", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка при выходе из приложения: {ex.Message}"); }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogging();
            LoadUsers();
            LoadEmployees();
            LoadRoles();
            LoadRiderTickets();
            LoadBookPredst();
            LoadAuthor();
            LoadGenre();
            LoadTypeLiterature();
            LoadPublisher();
            LoadAuthorArhive();
            LoadGenreArhive();
            LoadTypeLiteratureArhive();
            LoadPublisherArhive();
            LoadUsersArhive();
            LoadEmployeesArhive();
            LoadRolesArhive();
        }

        private async Task<List<Logging>> GetLogging()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {


                        using (var response = await httpClient.GetAsync($"{ip}/Loggings"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Logging> loggings = JsonConvert.DeserializeObject<List<Logging>>(apiResponse);
                                return loggings;
                            }

                            else
                            {
                                return new List<Logging>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Logging>();
            }
        }

        private async void dgLogging_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadLogging();
            }
        }

        private async Task LoadLogging()
        {
            try
            {
                var loggings = await GetLogging();
                dgLogging.ItemsSource = loggings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<List<User>> GetUserInfo()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Users"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<User> users = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                                List<User> filteredUsers = users.Where(user => user.RoleId == 3 && (user.Deleted_User == false || user.Deleted_User == null)).ToList();

                                return filteredUsers;
                            }
                            else
                            {
                                return new List<User>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
        }

        private async Task<List<User>> GetEmployeeInfo()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Users"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<User> users = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                                List<User> filteredUsers = users.Where(user => user.RoleId == 1 && (user.Deleted_User == false || user.Deleted_User == null)).ToList();

                                return filteredUsers;
                            }
                            else
                            {
                                return new List<User>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await GetUserInfo();
                dgUser.ItemsSource = users;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadEmployees()
        {
            try
            {
                var users = await GetEmployeeInfo();
                dgEmployee.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void dgUser_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadUsers();
            }
        }

        private async void dgEmployee_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadEmployees();
            }
        }

        private async Task<List<Role>> GetRoles()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Roles"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(apiResponse);
                                List<Role> filteredRole = roles.Where(p => p.Deleted_Role == false || p.Deleted_Role == null).ToList();

                                return filteredRole;

                            }
                            else
                            {
                                return new List<Role>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Role>();
            }
        }

        private async Task LoadRoles()
        {
            try
            {
                var roles = await GetRoles();
                var filteredRoles = roles.Where(role => role.NameRole != "Покупатель").ToList();

                dgRole.ItemsSource = roles;
                cbRole.ItemsSource = filteredRoles;
                cbRole.DisplayMemberPath = "NameRole";
                cbRole.SelectedValuePath = "IdRole";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void dgRole_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadRoles();
            }
        }

        private async Task<List<RiderTicket>> GetRiderTickets()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/RiderTickets"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<RiderTicket> riderTickets = JsonConvert.DeserializeObject<List<RiderTicket>>(apiResponse);
                                return riderTickets;

                            }
                            else
                            {
                                return new List<RiderTicket>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<RiderTicket>();
            }
        }

        private async Task LoadRiderTickets()
        {
            try
            {
                var riderTickets = await GetRiderTickets();
                dgRiderTicket.ItemsSource = riderTickets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void dgRiderTicket_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadRiderTickets();
            }
        }

        private async Task<List<BookList>> GetBookPredst()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/BookLists"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<BookList> books = JsonConvert.DeserializeObject<List<BookList>>(apiResponse);
                                return books;

                            }
                            else
                            {
                                return new List<BookList>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<BookList>();
            }
        }

        private async Task LoadBookPredst()
        {
            try
            {
                var book = await GetBookPredst();
                dgBook.ItemsSource = book;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void dgBook_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadBookPredst();
            }
        }

        private async Task<List<Author>> GetAuthor()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Authors"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(apiResponse);
                                List<Author> filteredAuthor = authors.Where(p => p.Deleted_Author == false || p.Deleted_Author == null).ToList();

                                return filteredAuthor;

                            }
                            else
                            {
                                return new List<Author>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Author>();
            }
        }



        private async Task<List<Genre>> GetGenre()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Genres"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(apiResponse);
                                List<Genre> filteredGenres = genres.Where(p => p.Deleted_Genre == false || p.Deleted_Genre == null).ToList();

                                return filteredGenres;

                            }
                            else
                            {
                                return new List<Genre>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Genre>();
            }
        }

        private async Task<List<TypeLiterature>> GetTypeLiterature()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/TypeLiteratures"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<TypeLiterature> typeLiterature = JsonConvert.DeserializeObject<List<TypeLiterature>>(apiResponse);
                                List<TypeLiterature> filteredTypeLiterature = typeLiterature.Where(p => p.Deleted_Type_Literature == false || p.Deleted_Type_Literature == null).ToList();

                                return filteredTypeLiterature;

                            }
                            else
                            {
                                return new List<TypeLiterature>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка : {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TypeLiterature>();
            }
        }

        private async Task<List<Publisher>> GetPublisher()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Publishers"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Publisher> publishers = JsonConvert.DeserializeObject<List<Publisher>>(apiResponse);
                                List<Publisher> filteredPublishers = publishers.Where(p => p.Deleted_Publisher == false || p.Deleted_Publisher == null).ToList();

                                return filteredPublishers;

                            }
                            else
                            {
                                return new List<Publisher>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Publisher>();
            }
        }

        private async Task LoadAuthor()
        {
            try
            {
                var author = await GetAuthor();
                dgAuthor.ItemsSource = author;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadGenre()
        {
            try
            {
                var genre = await GetGenre();
                dgGenre.ItemsSource = genre;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadTypeLiterature()
        {
            try
            {
                var typeLiterature = await GetTypeLiterature();
                dgTypeLiterature.ItemsSource = typeLiterature;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadPublisher()
        {
            try
            {
                var publisher = await GetPublisher();
                dgPublisher.ItemsSource = publisher;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void dgAuthor_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadAuthor();
            }
        }

        private async void dgGenre_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadGenre();
            }
        }

        private async void dgTypeLiterature_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadTypeLiterature();
            }
        }

        private async void dgPublisher_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                await LoadPublisher();
            }
        }

        private async void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Author newAuthor = new Author
                {
                    FirstNameAuthor = tbNameAuthor.Text,
                    SecondNameAuthor = tbSurnameAuthor.Text,
                    MiddleNameAuthor = tbMiddleNameAuthor.Text,
                    Deleted_Author = false
                };

                if (string.IsNullOrWhiteSpace(newAuthor.FirstNameAuthor) || string.IsNullOrWhiteSpace(newAuthor.SecondNameAuthor))
                {
                    MessageBox.Show("Пожалуйста, заполните имя и фамилию автора.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(newAuthor);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/Authors", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                tbNameAuthor.Text = "";
                                tbSurnameAuthor.Text = "";
                                tbMiddleNameAuthor.Text = "";
                                await LoadAuthor();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAuthor.SelectedItem != null)
            {
                Author selectedAuthor = (Author)dgAuthor.SelectedItem;
                tbSurnameAuthor.Text = selectedAuthor.SecondNameAuthor;
                tbNameAuthor.Text = selectedAuthor.FirstNameAuthor;
                tbMiddleNameAuthor.Text = selectedAuthor.MiddleNameAuthor;
            }
        }

        private void dgGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGenre.SelectedItem != null)
            {
                Genre selected = (Genre)dgGenre.SelectedItem;
                tbNameGenre.Text = selected.NameGenre;
            }
        }

        private void dgTypeLiterature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTypeLiterature.SelectedItem != null)
            {
                TypeLiterature selected = (TypeLiterature)dgTypeLiterature.SelectedItem;
                tbNameTypeLiterature.Text = selected.NameTypeLiterature;
            }
        }

        private void dgPublisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPublisher.SelectedItem != null)
            {
                Publisher selected = (Publisher)dgPublisher.SelectedItem;
                tbNamePublisher.Text = selected.NamePublisher;
            }
        }

        private async void btnUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAuthor.SelectedItem != null)
                {
                    Author selectedAuthor = (Author)dgAuthor.SelectedItem;
                    Author updatedAuthor = new Author
                    {
                        IdAuthor = selectedAuthor.IdAuthor,
                        FirstNameAuthor = tbNameAuthor.Text,
                        SecondNameAuthor = tbSurnameAuthor.Text,
                        MiddleNameAuthor = tbMiddleNameAuthor.Text,
                        Deleted_Author = false

                    };

                    if (string.IsNullOrWhiteSpace(updatedAuthor.FirstNameAuthor) || string.IsNullOrWhiteSpace(updatedAuthor.SecondNameAuthor))
                    {
                        MessageBox.Show("Пожалуйста, заполните имя и фамилию автора.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    await UpdateAuthor(updatedAuthor);

                    tbNameAuthor.Text = "";
                    tbSurnameAuthor.Text = "";
                    tbMiddleNameAuthor.Text = "";
                    await LoadAuthor();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAuthor.SelectedItem != null)
                {
                    Author selectedAuthor = (Author)dgAuthor.SelectedItem;
                    await DeleteAuthor(selectedAuthor);

                    tbNameAuthor.Text = "";
                    tbSurnameAuthor.Text = "";
                    tbMiddleNameAuthor.Text = "";
                    await LoadAuthor();
                    await LoadAuthorArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateAuthor(Author author)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(author);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Authors/{author.IdAuthor}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async Task DeleteAuthor(Author author)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        author.Deleted_Author = true;

                        var json = JsonConvert.SerializeObject(author);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Authors/{author.IdAuthor}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre newGenre = new Genre
                {
                    NameGenre = tbNameGenre.Text,
                };

                if (string.IsNullOrWhiteSpace(newGenre.NameGenre))
                {
                    MessageBox.Show("Пожалуйста, заполните название жанра.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(newGenre);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/Genres", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                tbNameGenre.Text = "";
                                await LoadGenre();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGenre.SelectedItem != null)
                {
                    Genre selected = (Genre)dgGenre.SelectedItem;
                    Genre updated = new Genre
                    {
                        IdGenre = selected.IdGenre,
                        NameGenre = tbNameGenre.Text

                    };

                    if (string.IsNullOrWhiteSpace(updated.NameGenre))
                    {
                        MessageBox.Show("Пожалуйста, заполните название жанра.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    await UpdateGenre(updated);

                    tbNameGenre.Text = "";
                    await LoadGenre();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGenre.SelectedItem != null)
                {
                    Genre selectedGenre = (Genre)dgGenre.SelectedItem;
                    await DeleteGenre(selectedGenre);

                    tbNameGenre.Text = "";
                    await LoadGenre();
                    await LoadGenreArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateGenre(Genre genre)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(genre);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Genres/{genre.IdGenre}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async Task DeleteGenre(Genre genre)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        genre.Deleted_Genre = true;

                        var json = JsonConvert.SerializeObject(genre);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Genres/{genre.IdGenre}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении жанра: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddTypeLiterature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TypeLiterature newTypeLiterature = new TypeLiterature
                {
                    NameTypeLiterature = tbNameTypeLiterature.Text,
                };

                if (string.IsNullOrWhiteSpace(newTypeLiterature.NameTypeLiterature))
                {
                    MessageBox.Show("Пожалуйста, заполните название типа литературы.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(newTypeLiterature);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/TypeLiteratures", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                tbNameTypeLiterature.Text = "";
                                await LoadTypeLiterature();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdateTypeLiterature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTypeLiterature.SelectedItem != null)
                {
                    TypeLiterature selected = (TypeLiterature)dgTypeLiterature.SelectedItem;
                    TypeLiterature updated = new TypeLiterature
                    {
                        IdTypeLiterature = selected.IdTypeLiterature,
                        NameTypeLiterature = tbNameTypeLiterature.Text
                    };

                    if (string.IsNullOrWhiteSpace(updated.NameTypeLiterature))
                    {
                        MessageBox.Show("Пожалуйста, заполните название типа литературы.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    await UpdateTypeLiterature(updated);

                    tbNameTypeLiterature.Text = "";
                    await LoadTypeLiterature();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeleteTypeLiterature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTypeLiterature.SelectedItem != null)
                {
                    TypeLiterature selected = (TypeLiterature)dgTypeLiterature.SelectedItem;
                    await DeleteTypeLiterature(selected);

                    tbNameTypeLiterature.Text = "";
                    await LoadTypeLiterature();
                    await LoadTypeLiteratureArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateTypeLiterature(TypeLiterature typeLiterature)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(typeLiterature);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/TypeLiteratures/{typeLiterature.IdTypeLiterature}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async Task DeleteTypeLiterature(TypeLiterature typeLiterature)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        typeLiterature.Deleted_Type_Literature = true;

                        var json = JsonConvert.SerializeObject(typeLiterature);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/TypeLiteratures/{typeLiterature.IdTypeLiterature}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddPublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Publisher newPublisher = new Publisher
                {
                    NamePublisher = tbNamePublisher.Text
                };

                if (string.IsNullOrWhiteSpace(newPublisher.NamePublisher))
                {
                    MessageBox.Show("Пожалуйста, заполните название издателя.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(newPublisher);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/Publishers", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                tbNamePublisher.Text = "";
                                await LoadPublisher();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdatePublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgPublisher.SelectedItem != null)
                {
                    Publisher selected = (Publisher)dgPublisher.SelectedItem;
                    Publisher updated = new Publisher
                    {
                        IdPublisher = selected.IdPublisher,
                        NamePublisher = tbNamePublisher.Text
                    };

                    if (string.IsNullOrWhiteSpace(updated.NamePublisher))
                    {
                        MessageBox.Show("Пожалуйста, заполните название издателя.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    await UpdatePublisher(updated);

                    tbNamePublisher.Text = "";
                    await LoadPublisher();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDeletePublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgPublisher.SelectedItem != null)
                {
                    Publisher selected = (Publisher)dgPublisher.SelectedItem;
                    await DeletePublisher(selected);

                    tbNamePublisher.Text = "";
                    await LoadPublisher();
                    await LoadPublisherArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdatePublisher(Publisher publisher)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(publisher);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Publishers/{publisher.IdPublisher}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async Task DeletePublisher(Publisher publisher)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        publisher.Deleted_Publisher = true;

                        var json = JsonConvert.SerializeObject(publisher);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Publishers/{publisher.IdPublisher}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRole.SelectedItem != null)
            {
                Role selected = (Role)dgRole.SelectedItem;
                tbNameRole.Text = selected.NameRole;
            }
        }

        private async void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Role newRole = new Role
                {
                    NameRole = tbNameRole.Text
                };

                if (string.IsNullOrWhiteSpace(newRole.NameRole))
                {
                    MessageBox.Show("Пожалуйста, заполните название роли.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(newRole);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/Roles", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                tbNameRole.Text = "";
                                await LoadRoles();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdateRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRole.SelectedItem != null)
                {
                    Role selected = (Role)dgRole.SelectedItem;
                    Role updated = new Role
                    {
                        IdRole = selected.IdRole,
                        NameRole = tbNameRole.Text
                    };

                    if (string.IsNullOrWhiteSpace(updated.NameRole))
                    {
                        MessageBox.Show("Пожалуйста, заполните название роли.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    await UpdateRole(updated);

                    tbNameRole.Text = "";
                    await LoadRoles();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateRole(Role role)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(role);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Roles/{role.IdRole}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async void btnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRole.SelectedItem != null)
                {
                    Role selected = (Role)dgRole.SelectedItem;
                    await DeleteRole(selected);

                    tbNameRole.Text = "";
                    await LoadRoles();
                    await LoadRolesArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteRole(Role role)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        role.Deleted_Role = true;

                        var json = JsonConvert.SerializeObject(role);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Roles/{role.IdRole}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdateRiderTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRiderTicket.SelectedItem != null)
                {
                    RiderTicket selectedRiderTicket = (RiderTicket)dgRiderTicket.SelectedItem;

                    string newNumberRiderTicket = tbRiderTicket.Text;

                    selectedRiderTicket.NumberRiderTicket = newNumberRiderTicket;

                    await UpdateRiderTicket(selectedRiderTicket);


                    tbRiderTicket.Text = "";
                    await LoadRiderTickets();
                }
                else
                {
                    MessageBox.Show("Выберите читательский билет для обновления.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении читательского билета: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateRiderTicket(RiderTicket riderTicket)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(riderTicket);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/RiderTickets/{riderTicket.IdRiderTicket}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при отправке запроса API: {ex.Message}");
            }
        }

        private void dgRiderTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRiderTicket.SelectedItem != null)
            {
                RiderTicket selected = (RiderTicket)dgRiderTicket.SelectedItem;
                tbRiderTicket.Text = selected.NumberRiderTicket;
            }
        }

        private async void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                User newUser = new User
                {
                    FirstName = tbUserName.Text,
                    SecondName = tbUserSurname.Text,
                    MiddleName = tbUserMiddleName.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                    PassportSeries = string.IsNullOrWhiteSpace(tbSeriesPassport.Text) ? null : tbSeriesPassport.Text,
                    PassportNumber = string.IsNullOrWhiteSpace(tbNumberPassport.Text) ? null : tbNumberPassport.Text,
                    BirthDate = dpDateBirthday.SelectedDate ?? DateTime.MinValue,
                    Email = string.IsNullOrWhiteSpace(tbEmail.Text) ? null : tbEmail.Text,
                    RoleId = cbRole.SelectedValue as int?,
                    Deleted_User = false
                };


                if (string.IsNullOrWhiteSpace(newUser.FirstName) || string.IsNullOrWhiteSpace(newUser.Login) || string.IsNullOrWhiteSpace(newUser.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните обязательные поля (Имя, Логин, Пароль).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await AddUser(newUser);

                tbUserName.Text = "";
                tbUserSurname.Text = "";
                tbUserMiddleName.Text = "";
                tbLogin.Text = "";
                tbPassword.Text = "";
                tbSeriesPassport.Text = "";
                tbNumberPassport.Text = "";
                dpDateBirthday.Text = "";
                tbEmail.Text = "";

                await LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddUser(User user)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync($"{apiUrl}/Users", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployee.SelectedItem != null)
                {
                    User selectedUser = (User)dgEmployee.SelectedItem;

                    selectedUser.FirstName = tbUserName.Text;
                    selectedUser.SecondName = tbUserSurname.Text;
                    selectedUser.MiddleName = tbUserMiddleName.Text;
                    selectedUser.Login = tbLogin.Text;
                    selectedUser.Password = tbPassword.Text;
                    selectedUser.PassportSeries = string.IsNullOrWhiteSpace(tbSeriesPassport.Text) ? null : tbSeriesPassport.Text;
                    selectedUser.PassportNumber = string.IsNullOrWhiteSpace(tbNumberPassport.Text) ? null : tbNumberPassport.Text;
                    selectedUser.BirthDate = dpDateBirthday.SelectedDate.HasValue
                        ? dpDateBirthday.SelectedDate.Value
                        : DateTime.MinValue;
                    selectedUser.Email = tbEmail.Text;
                    selectedUser.RoleId = cbRole.SelectedValue as int?;

                    await UpdateUser(selectedUser);

                    tbUserName.Text = "";
                    tbUserSurname.Text = "";
                    tbUserMiddleName.Text = "";
                    tbLogin.Text = "";
                    tbPassword.Text = "";
                    tbSeriesPassport.Text = "";
                    tbNumberPassport.Text = "";
                    dpDateBirthday.Text = "";
                    tbEmail.Text = "";

                    await LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Выберите пользователя для обновления.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateUser(User user)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Users/{user.IdUser}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployee.SelectedItem != null)
                {
                    User selectedUser = (User)dgEmployee.SelectedItem;

                    selectedUser.Deleted_User = true;

                    await DeleteUser(selectedUser);

                    tbUserName.Text = "";
                    tbUserSurname.Text = "";
                    tbUserMiddleName.Text = "";
                    tbLogin.Text = "";
                    tbPassword.Text = "";
                    tbSeriesPassport.Text = "";
                    tbNumberPassport.Text = "";
                    dpDateBirthday.Text = "";
                    tbEmail.Text = "";

                    await LoadEmployees();
                    await LoadEmployeesArhive();
                }
                else
                {
                    MessageBox.Show("Выберите пользователя для удаления.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteUser(User user)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;

                    user.Deleted_User = true;

                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Users/{user.IdUser}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEmployee.SelectedItem != null)
            {
                User selected = (User)dgEmployee.SelectedItem;
                tbUserName.Text = selected.FirstName;
                tbUserSurname.Text = selected.SecondName;
                tbUserMiddleName.Text = selected.MiddleName;
                tbLogin.Text = selected.Login;
                tbPassword.Text = selected.Password;
                tbSeriesPassport.Text = selected.PassportSeries;
                tbNumberPassport.Text = selected.PassportNumber;
                dpDateBirthday.Text = selected.BirthDate.ToString("yyyy-MM-dd");
                tbEmail.Text = selected.Email;
                cbRole.SelectedValue = selected.RoleId;

            }
        }

        private async Task<List<Author>> GetAuthorArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Authors"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(apiResponse);
                                List<Author> filteredAuthor = authors.Where(p => p.Deleted_Author == true).ToList();

                                return filteredAuthor;

                            }
                            else
                            {
                                return new List<Author>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Author>();
            }
        }



        private async Task<List<Genre>> GetGenreArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Genres"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(apiResponse);
                                List<Genre> filteredGenres = genres.Where(p => p.Deleted_Genre == true).ToList();

                                return filteredGenres;

                            }
                            else
                            {
                                return new List<Genre>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Genre>();
            }
        }

        private async Task<List<TypeLiterature>> GetTypeLiteratureArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/TypeLiteratures"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<TypeLiterature> typeLiterature = JsonConvert.DeserializeObject<List<TypeLiterature>>(apiResponse);
                                List<TypeLiterature> filteredTypeLiterature = typeLiterature.Where(p => p.Deleted_Type_Literature == true).ToList();

                                return filteredTypeLiterature;

                            }
                            else
                            {
                                return new List<TypeLiterature>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TypeLiterature>();
            }
        }

        private async Task<List<Publisher>> GetPublisherArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Publishers"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Publisher> publishers = JsonConvert.DeserializeObject<List<Publisher>>(apiResponse);
                                List<Publisher> filteredPublishers = publishers.Where(p => p.Deleted_Publisher == true).ToList();

                                return filteredPublishers;

                            }
                            else
                            {
                                return new List<Publisher>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Publisher>();
            }
        }

        private async Task LoadAuthorArhive()
        {
            try
            {
                var author = await GetAuthorArhive();
                dgAuthorArhive.ItemsSource = author;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadGenreArhive()
        {
            try
            {
                var genre = await GetGenreArhive();
                dgGenreArhive.ItemsSource = genre;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadTypeLiteratureArhive()
        {
            try
            {
                var typeLiterature = await GetTypeLiteratureArhive();
                dgTypeLiteratureArhive.ItemsSource = typeLiterature;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadPublisherArhive()
        {
            try
            {
                var publisher = await GetPublisherArhive();
                dgPublisherArhive.ItemsSource = publisher;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnTypeLiteratureReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTypeLiteratureArhive.SelectedItem != null)
                {
                    TypeLiterature selected = (TypeLiterature)dgTypeLiteratureArhive.SelectedItem;
                    await ReturnTypeLiterature(selected);

                    await LoadTypeLiteratureArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnTypeLiterature(TypeLiterature typeLiterature)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        typeLiterature.Deleted_Type_Literature = false;

                        var json = JsonConvert.SerializeObject(typeLiterature);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/TypeLiteratures/{typeLiterature.IdTypeLiterature}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении типа литературы: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAuthorReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAuthorArhive.SelectedItem != null)
                {
                    Author selectedAuthor = (Author)dgAuthorArhive.SelectedItem;
                    await ReturnAuthor(selectedAuthor);

                    await LoadAuthorArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnAuthor(Author author)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        author.Deleted_Author = false;

                        var json = JsonConvert.SerializeObject(author);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Authors/{author.IdAuthor}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении автора: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnGenreReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGenreArhive.SelectedItem != null)
                {
                    Genre selectedGenre = (Genre)dgGenreArhive.SelectedItem;
                    await ReturnGenre(selectedGenre);

                    await LoadGenreArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnGenre(Genre genre)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        genre.Deleted_Genre = false;

                        var json = JsonConvert.SerializeObject(genre);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Genres/{genre.IdGenre}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении жанра: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnPublisherReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgPublisherArhive.SelectedItem != null)
                {
                    Publisher selected = (Publisher)dgPublisherArhive.SelectedItem;
                    await ReturnPublisher(selected);

                    await LoadPublisherArhive();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnPublisher(Publisher publisher)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        publisher.Deleted_Publisher = false;

                        var json = JsonConvert.SerializeObject(publisher);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Publishers/{publisher.IdPublisher}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении издателя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<List<User>> GetUserInfoArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Users"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<User> users = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                                List<User> filteredUsers = users.Where(user => user.RoleId == 3 && user.Deleted_User == true).ToList();

                                return filteredUsers;
                            }
                            else
                            {
                                return new List<User>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
        }

        private async Task<List<User>> GetEmployeeInfoArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Users"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<User> users = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                                List<User> filteredUsers = users.Where(user => user.RoleId == 1 && user.Deleted_User == true).ToList();

                                return filteredUsers;
                            }
                            else
                            {
                                return new List<User>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<User>();
            }
        }

        private async Task LoadUsersArhive()
        {
            try
            {
                var users = await GetUserInfoArhive();
                dgUserArhive.ItemsSource = users;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadEmployeesArhive()
        {
            try
            {
                var users = await GetEmployeeInfoArhive();
                dgEmployeeArhive.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<List<Role>> GetRolesArhive()
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var response = await httpClient.GetAsync($"{ip}/Roles"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(apiResponse);
                                List<Role> filteredRole = roles.Where(p => p.Deleted_Role == true).ToList();

                                return filteredRole;

                            }
                            else
                            {
                                return new List<Role>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Role>();
            }
        }

        private async Task LoadRolesArhive()
        {
            try
            {
                var roles = await GetRolesArhive();

                dgRoleArhive.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEmployeeReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployeeArhive.SelectedItem != null)
                {
                    User selectedUser = (User)dgEmployeeArhive.SelectedItem;

                    await ReturnUser(selectedUser);

                    await LoadEmployeesArhive();

                    await LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Выберите пользователя для восстановления.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnUser(User user)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    var apiUrl = ((App)App.Current).ApiUrl;

                    user.Deleted_User = false;

                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{apiUrl}/Users/{user.IdUser}", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Ошибка: {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        private async void btnUserReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgUserArhive.SelectedItem != null)
                {
                    User selectedUser = (User)dgUserArhive.SelectedItem;

                    await ReturnUser(selectedUser);

                    await LoadUsersArhive();
                    await LoadUsers();
                }
                else
                {
                    MessageBox.Show("Выберите пользователя для восстановления.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnReturnRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRoleArhive.SelectedItem != null)
                {
                    Role selected = (Role)dgRoleArhive.SelectedItem;
                    await ReturnRole(selected);

                    await LoadRolesArhive();
                    await LoadRoles();
                }
                else
                {
                    MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReturnRole(Role role)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;

                        role.Deleted_Role = false;

                        var json = JsonConvert.SerializeObject(role);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{apiUrl}/Roles/{role.IdRole}", content))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception($"Ошибка: {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void TrustAllCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate (
                object sender,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_"+"Library_Database.bak",
                    DefaultExt = ".bak",
                    Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var apiUrl = $"{ip}/Export/backupdatabase";
                    TrustAllCertificates();

                    var fileName = System.IO.Path.GetFileName(saveFileDialog.FileName);

                    using (var client = new HttpClient())
                    {
                        var payload = JsonConvert.SerializeObject(new { SavePath = saveFileDialog.FileName, FileName = fileName });
                        var content = new StringContent(payload, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Резервная копия успешно создана");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при создании резервной копии: " + response.StatusCode);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании резервной копии: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class ImportRequest
        {
            public string SavePath { get; set; }
            public string FileName { get; set; }
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    DefaultExt = ".bak",
                    Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var apiUrl = $"{ip}/Export/restoredatabase";
                    TrustAllCertificates();

                    var fileName = System.IO.Path.GetFileName(openFileDialog.FileName);

                    using (var client = new HttpClient())
                    {
                        var importRequest = new ImportRequest
                        {
                            SavePath = openFileDialog.FileName.Replace("\\", "\\\\"),
                            FileName = fileName
                        };

                        var payload = JsonConvert.SerializeObject(importRequest);
                        var content = new StringContent(payload, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("База данных успешно восстановлена");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при восстановлении базы данных: " + response.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении базы данных: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
