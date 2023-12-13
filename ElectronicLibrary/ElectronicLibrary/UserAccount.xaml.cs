using ElectronicLibrary.Models;
using ElectronicLibraryAPI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ElectronicLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainInfo.xaml
    /// </summary>
    public partial class UserAccount : Window
    {

        private readonly string idUser;
        private string ip;
        private string sortOrder = null;
        string searchString = null;
        decimal totalCostWithPromocode = 0;
        decimal totalCostWithPromocodeOrderOneBook = 0;
        Window parentWindow = Application.Current.MainWindow;
        Boolean basketOrder = false;


        public ObservableCollection<Basket> Basket { get; set; }
        public ObservableCollection<IssueProduct> IssueProduct { get; set; } = new ObservableCollection<IssueProduct>();

        public ObservableCollection<ReturnBook> ReturnBook { get; set; } = new ObservableCollection<ReturnBook>();


        private List<Book> booksList;
        public UserAccount(string idUser)
        {
            InitializeComponent();

            string apiUrl = ((App)App.Current).ApiUrl;
            ip = apiUrl;

            this.idUser = idUser;
            Loaded += UserAccount_Loaded;
            Basket = new ObservableCollection<Basket>();
        }

        private void UserAccount_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserData();
            LoadBooks();
            LoadBooksIssue();
            LoadBooksBasket();
            ListViewProducts.ItemsSource = booksList;
        }



        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.Register("Birthday", typeof(DateTime?), typeof(UserAccount), new PropertyMetadata(null));


        private async Task LoadUserData()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var userResponse = await httpClient.GetAsync($"{ip}/Users/{idUser}");

                    if (userResponse.IsSuccessStatusCode)
                    {
                        var userContent = await userResponse.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(userContent);

                        tbEnterNameAccount.Text = user.FirstName;
                        tbEnterSurnameAccount.Text = user.SecondName;
                        tbEnterMiddleNameAccount.Text = user.MiddleName;
                        dpBirthday.SelectedDate = user.BirthDate;
                        tbEmail.Text = user.Email;
                        tbNameMessage.Text = user.FirstName;
                        tbEmailMessage.Text = user.Email;

                        emailBuy.Text= user.Email;

                        tbNameReturn.Text = user.FirstName;
                        tbEmailReturn.Text = user.Email;

                        var riderTicketResponse = await httpClient.GetAsync($"{ip}/RiderTickets?userId={idUser}");

                      
                        if (riderTicketResponse.IsSuccessStatusCode)
                        {
                            var riderTicketContent = await riderTicketResponse.Content.ReadAsStringAsync();
                            var riderTicketList = JsonConvert.DeserializeObject<List<RiderTicket>>(riderTicketContent);
                            if (riderTicketList.Count > 0)
                            {
                                var firstRiderTicket = riderTicketList[0];
                                tbNumberReadTicket.Text = "Номер читательского билета : "+ firstRiderTicket.NumberRiderTicket;

                                var formattedDateTerm = firstRiderTicket.DateTerm.ToString("dd.MM.yyyy") ?? "";
                                tbDateIssue.Text = "Дата выдачи читательского билета : " + formattedDateTerm;
                            }
                            else
                            {
                                tbNumberReadTicket.Text = "Не найден читательский билет";
                            }
                        }
                        else
                        {
                            
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private async Task LoadBooks(string searchString = null, string sortOrder = null, List<string> genres = null, List<string> types = null, List<string> publishers = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var searchParam = string.IsNullOrEmpty(searchString) ? "" : $"&searchString={searchString}";

                    var sortParam = string.IsNullOrEmpty(sortOrder) ? "" : $"&sortOrder={sortOrder}";

                    var genresParam = genres != null && genres.Any() ? $"&genres={string.Join(",", genres)}" : "";
                    var typesParam = types != null && types.Any() ? $"&types={string.Join(",", types)}" : "";
                    var publishersParam = publishers != null && publishers.Any() ? $"&publishers={string.Join(",", publishers)}" : "";

                    var parameters = string.Join("&", new[] { searchParam, sortParam, genresParam, typesParam, publishersParam }.Where(p => !string.IsNullOrEmpty(p)));

                    var url = $"{ip}/Books";
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        url += $"?{parameters}";
                    }

                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var books = JsonConvert.DeserializeObject<List<Book>>(content);
                        ListViewProducts.ItemsSource = books;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные с сервера.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
        }



        private async Task<Book> LoadBook(int bookId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Books/{bookId}";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var book = JsonConvert.DeserializeObject<Book>(content);
                        return book;
                    }
                    else
                    {
                        MessageBox.Show($"Не удалось загрузить данные о книге с ID {bookId} с сервера. Код ошибки: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке данных о книге с ID {bookId}: {ex.Message}");
            }

            return null;
        }


        private async Task LoadBooksIssue()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/IssueProducts";

                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var issueProducts = JsonConvert.DeserializeObject<List<IssueProduct>>(content);

                        issueProducts = issueProducts.Where(ip => ip.Deleted_Issue_Product != true).ToList();

                        foreach (var issueProduct in issueProducts)
                        {
                            issueProduct.Book = await LoadBook(issueProduct.BookId);


                            await LoadBookDetails(issueProduct.Book);
                        }

                        ListViewIssueProducts.ItemsSource = issueProducts;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные с сервера.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
        }



        private async Task LoadBooksBasket()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Baskets";

                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var basketBook = JsonConvert.DeserializeObject<List<Basket>>(content);

                        Basket.Clear();

                        foreach (var basketItems in basketBook)
                        {
                            Basket.Add(basketItems);

                            basketItems.Book = await LoadBook(basketItems.BookId);
                            await LoadBookDetails(basketItems.Book);
                        }

                        int countBook = Basket.Count;

                        string countBooksMessage = "книг";
                        if (countBook % 10 == 1 && countBook % 100 != 11)
                        {
                            countBooksMessage = "книга";
                        }
                        else if (countBook % 10 >= 2 && countBook % 10 <= 4 && (countBook % 100 < 10 || countBook % 100 >= 20))
                        {
                            countBooksMessage = "книги";
                        }

                        countBooksBasket.Text = $"В вашей корзине {countBook} {countBooksMessage}:";

                        decimal totalCost = 0;
                        foreach (var basketItem in Basket)
                        {
                            totalCost += (decimal)(basketItem.Book.Price ?? 0m);
                        }

                        finalPriceBasket.Text = $"Итоговая стоимость: {totalCost} рублей";
                        totalCostWithPromocode = totalCost;



                        ListItemsBasket.ItemsSource = null;
                        ListItemsBasket.ItemsSource = basketBook;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные с сервера.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
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
            catch { }
        }

        private async void btnOkFilter_Click(object sender, RoutedEventArgs e)
        {
            filterPopup.IsOpen = false;
            await LoadBooks(searchString, sortOrder, selectedGenres, selectedTypes, selectedPublishers);
        }


        private void btFilter_Click(object sender, RoutedEventArgs e)
        {

            filterPopup.IsOpen = true;
            
        }

        private List<string> selectedGenres = new List<string>();
        private List<string> selectedTypes = new List<string>();
        private List<string> selectedPublishers = new List<string>();

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var content = checkbox?.Content as string;
            var filterType = checkbox?.Tag as string;

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(filterType))
            {
                switch (filterType)
                {
                    case "Genre":
                        selectedGenres.Add(content);
                        break;
                    case "TypeLiterature":
                        selectedTypes.Add(content);
                        break;
                    case "Publisher":
                        selectedPublishers.Add(content);
                        break;
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var content = checkbox?.Content as string;
            var filterType = checkbox?.Tag as string;

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(filterType))
            {
                switch (filterType)
                {
                    case "Genre":
                        selectedGenres.Remove(content);
                        break;
                    case "TypeLiterature":
                        selectedTypes.Remove(content);
                        break;
                    case "Publisher":
                        selectedPublishers.Remove(content);
                        break;
                }
            }
        }


        private void Diteils(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Book clickedBook)
            {
                LoadBookDetails(clickedBook);
                btnAddBasketInPopup.DataContext = clickedBook;
                popupBuyCatalog.DataContext = clickedBook;
                popup.IsOpen = true;
            }
        }

        private void DiteilMyBook(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is IssueProduct clickedIssueProduct)
            {
                LoadBookDetailsForPopupBack(clickedIssueProduct.Book);
                popupBack.DataContext = clickedIssueProduct.Book;
                popupDownload.DataContext = clickedIssueProduct.Book;
                popupBook.IsOpen = true;
                overlay.Visibility = Visibility.Visible;
            }
        }


        private async Task LoadBookDetails(Book book)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var bookDetailsUrl = $"{ip}/Books/{book.IdBook}";

                    var response = await httpClient.GetAsync(bookDetailsUrl);

                    response.EnsureSuccessStatusCode();

                    var detailedBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());


                    tbNameBook.Text = $"{detailedBook.NameBook}";
                    tbYearPublic.Text = $"Год издания: {detailedBook.PublicationDate.ToString("dd.MM.yyyy")}";
                    tbBriefFull.Text = $"{detailedBook.BriefPlot}";
                    tbNumPage.Text = $"Количество страниц: {detailedBook.NumberPages}";
                    tbPrice.Text = $"Цена: {detailedBook.Price} ₽";
                    tbTypeLiterature.Text = $"Тип литературы: {string.Join(", ", detailedBook.TypeLiteratureView.Select(tlv => tlv.TypeLiterature.NameTypeLiterature))}";
                    tbGenre.Text = $"Жанр: {string.Join(", ", detailedBook.GenreView.Select(gv => gv.Genre.NameGenre))}";
                    tbPublisher.Text = $"Издатель: {string.Join(", ", detailedBook.PublisherView.Select(pv => pv.Publisher.NamePublisher))}";

                    BitmapImage bitmapImage = new BitmapImage(new Uri(detailedBook.CoverPhoto));
                    bookImage.Source = bitmapImage;

                    SolidColorBrush backgroundBrush = new SolidColorBrush(Colors.Gray); 
                    bookImageContainer.Background = backgroundBrush;

                    popup.DataContext = detailedBook;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task LoadBookDetailsForPopupBack(Book book)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var bookDetailsUrl = $"{ip}/Books/{book.IdBook}";

                    var response = await httpClient.GetAsync(bookDetailsUrl);

                    response.EnsureSuccessStatusCode();

                    var detailedBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());


                    tbNameBookIssue.Text = $"{detailedBook.NameBook}";
                    tbYearPublicIssue.Text = $"Год издания: {detailedBook.PublicationDate.ToString("dd.MM.yyyy")}";
                    tbBriefFullIssue.Text = $"{detailedBook.BriefPlot}";
                    tbNumPageIssue.Text = $"Количество страниц: {detailedBook.NumberPages}";
                    tbPriceIssue.Text = $"Цена: {detailedBook.Price} ₽";
                    tbTypeLiteratureIssue.Text = $"Тип литературы: {string.Join(", ", detailedBook.TypeLiteratureView.Select(tlv => tlv.TypeLiterature.NameTypeLiterature))}";
                    tbGenreIssue.Text = $"Жанр: {string.Join(", ", detailedBook.GenreView.Select(gv => gv.Genre.NameGenre))}";
                    tbPublisherIssue.Text = $"Издатель: {string.Join(", ", detailedBook.PublisherView.Select(pv => pv.Publisher.NamePublisher))}";

                    BitmapImage bitmapImage = new BitmapImage(new Uri(detailedBook.CoverPhoto));
                    ImageBookIssue.Source = bitmapImage;

                    SolidColorBrush backgroundBrush = new SolidColorBrush(Colors.Gray);
                    bookImageContainerIssue.Background = backgroundBrush;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
        private void ReturnBookPoop(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            popupBack.IsOpen = true;

        }
        private void CloseButton2_Click(object sender, RoutedEventArgs e)
        {
            popupBook.IsOpen = false;
            overlay.Visibility = Visibility.Collapsed;
        }

        private async void ButtonReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (popupBack.DataContext is Book selectedBook)
            {
                string name = tbNameReturn.Text;
                string email = tbEmailReturn.Text;
                string reason = tbReasonReturn.Text;

                ReturnBook returnBook = new ReturnBook
                {
                    UserId = int.TryParse(idUser, out int parsedUserId) ? (int?)parsedUserId : null,
                    BookId = selectedBook.IdBook,
                    ReasonReturn = reason,
                    NameUserForReturn = name,
                    EmailUserForReturn = email,
                };

                bool addToReturnBookSuccess = await AddToReturnBook(returnBook);

                if (addToReturnBookSuccess)
                {
                    tbNameReturn.Text = string.Empty;
                    tbEmailReturn.Text = string.Empty;
                    tbReasonReturn.Text = string.Empty;
                    popupBack.IsOpen = false;
                }
            }
            else
            {
                MessageBox.Show("Не удалось получить информацию о выбранной книге.");
            }
        }


        private async Task<bool> AddToReturnBook(ReturnBook returnBook)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var returnBookUrl = $"{ip}/ReturnBooks";

                    var content = JsonConvert.SerializeObject(returnBook);
                    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(returnBookUrl, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        ReturnBook.Add(returnBook);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить запись в таблицу ReturnBook. Код ошибки: " + response.StatusCode);
                    }

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении записи в таблицу ReturnBook: " + ex.Message);
                return false;
            }
        }


        private async void btEditProfile_Click(object sender, RoutedEventArgs e)
        {
            if (tbEnterNameAccount.IsEnabled == true)
            {
                tbEnterNameAccount.IsEnabled = false;
                tbEnterSurnameAccount.IsEnabled = false;
                tbEnterMiddleNameAccount.IsEnabled = false;
                dpBirthday.IsEnabled = false;
                UserRed.Content = "Редактировать профиль";
                await SaveUserProfileChanges();

            }
            else
            {
                tbEnterNameAccount.IsEnabled = true;
                tbEnterSurnameAccount.IsEnabled = true;
                tbEnterMiddleNameAccount.IsEnabled = true;
                dpBirthday.IsEnabled = true;
                UserRed.Content = "Сохранить";
            }
        }

        private async Task SaveUserProfileChanges()
        {
            using (var httpClient = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                var userResponse = await httpClient.GetAsync($"{ip}/Users/{idUser}");

                if (userResponse.IsSuccessStatusCode)
                {
                    var userContent = await userResponse.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(userContent);

                    user.FirstName = tbEnterNameAccount.Text;
                    user.SecondName = tbEnterSurnameAccount.Text;
                    user.MiddleName = tbEnterMiddleNameAccount.Text;
                    user.BirthDate = dpBirthday.SelectedDate ?? DateTime.MinValue; 
                    var updatedUserJson = JsonConvert.SerializeObject(user);

                    var updateUserResponse = await httpClient.PutAsync($"{ip}/Users/{idUser}",
                        new StringContent(updatedUserJson, Encoding.UTF8, "application/json"));

                    if (updateUserResponse.IsSuccessStatusCode)
                    {

                        tbEnterNameAccount.IsEnabled = false;
                        tbEnterSurnameAccount.IsEnabled = false;
                        tbEnterMiddleNameAccount.IsEnabled = false;
                        dpBirthday.IsEnabled = false;
                        UserRed.Content = "Редактировать профиль";
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить данные.");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить данные пользователя.");
                }
            }
        }
        private async void ChangePassword_Click(object sender, RoutedEventArgs e)
        {

            if (tbOldPass.IsEnabled == true)
            {
                tbOldPass.IsEnabled = false;
                tbNewPass.IsEnabled = false;
                tbReapetNewPass.IsEnabled = false;
                PassRed.Content = "Изменить пароль";
                await ChangePassword(idUser, tbOldPass.Password, tbNewPass.Password, tbReapetNewPass.Password);

            }
            else
            {
                tbOldPass.IsEnabled = true;
                tbNewPass.IsEnabled = true;
                tbReapetNewPass.IsEnabled = true;
                PassRed.Content = "Сохранить";
            }

        }

        private async Task ChangePassword(string userId, string oldPassword, string newPassword, string repeatedNewPassword)
        {
            int idUser = int.Parse(userId);
            string apiUrl = $"{ip}/Users/ChangePassword/" + idUser;
            Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

            ChangePasswordRequest request = new ChangePasswordRequest
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                RepeatedNewPassword = repeatedNewPassword
            };

            if (oldPassword == null || newPassword == null || repeatedNewPassword == null)
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else if (!passwordRegex.IsMatch(newPassword))
            {
                MessageBox.Show("Пароль должен содержать как минимум одну заглавную букву, одну строчную букву, одну цифру, один специальный символ и иметь длину не менее 8 символов.");
            }
            else if (newPassword != repeatedNewPassword)
            {
                MessageBox.Show("Новый пароль и повтор пароля должны совпадать!");
            }
            else {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string jsonContent = JsonConvert.SerializeObject(request);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пароль успешно изменен.");
                }
                else
                {
                    MessageBox.Show("Пароль не изменен. Ошибка: " + response.ReasonPhrase);
                }
            }
            }
        }
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchString = txtSearch.Text;
            await LoadBooks(searchString, null, null);
        }

        private async void btnSort_Click(object sender, RoutedEventArgs e)
        {
            if (sortOrder == "price_asc")
                sortOrder = "price_desc";
            else
                sortOrder = "price_asc";

            await LoadBooks(null, sortOrder, null);
        }


        private async void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            await DeleteAccountDoing(idUser);
        }

        private async Task DeleteAccountDoing(string userId)
        {
            bool isConfirmed = ShowConfirmationDialog("Вы уверены, что хотите удалить аккаунт? Данные будут навсегда потеряны.");

            if (isConfirmed)
            {
                string apiUrl = $"{ip}/Users/{userId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Аккаунт успешно удален");
                    }
                    else
                    {
                    }
                }
            }
        }

        private  bool ShowConfirmationDialog(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Book selectedBook)
            {
                popupBuyCatalog.DataContext = selectedBook;
                popupBuyCatalog.IsOpen = true;
                finalPriceOrder.Text = $"Сумма к оплате: {selectedBook.Price} ₽";
                totalCostWithPromocodeOrderOneBook = (decimal)selectedBook.Price;
            }
        }

        private async void btnAddBasket_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.DataContext is Book selectedBook)
            {


                btnAddBasketInPopup.DataContext = selectedBook;
                Basket basketItem = new Basket
                {
                    Cost = (decimal)selectedBook.Price,
                    RiderTicketId = 1,
                    BookId = selectedBook.IdBook,
                    PromocodeId = 1
                };

                bool addToBasketSuccess = await AddToBasket(basketItem);

                if (addToBasketSuccess)
                {
                    if (button.Name == "btnAddBasketInPopup")
                    {
                        popup.IsOpen = true;

                    }
                    else
                    {

                    }
                    LoadBooksBasket();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить книгу в корзину. Пожалуйста, попробуйте ещё раз.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу, которую вы хотите добавить в корзину.");
            }
        }


        private async Task<bool> AddToBasket(Basket basketItem)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var basketItemUrl = $"{ip}/Baskets";

                    var content = JsonConvert.SerializeObject(basketItem);
                    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(basketItemUrl, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Basket.Add(basketItem);

                        ListItemsBasket.ItemsSource = null;
                        ListItemsBasket.ItemsSource = Basket;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить книгу в корзину. Код ошибки: " + response.StatusCode);
                    }

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении книги в корзину: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> AddToOrder(IssueProduct issueItem)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var basketItemUrl = $"{ip}/IssueProducts";

                    var content = JsonConvert.SerializeObject(issueItem);
                    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(basketItemUrl, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        IssueProduct.Add(issueItem);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить книгу в корзину. Код ошибки: " + response.StatusCode);
                    }

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении книги в корзину: " + ex.Message);
                return false;
            }
        }



        private async void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Feedback feedback = new Feedback
                {
                    Message = tbMessage.Text,
                    NameUserMessage = tbNameMessage.Text,
                    EmailUserMessage = tbEmailMessage.Text,
                    UserId = int.Parse(idUser), 
                    Done = false 
                };

                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, errors) => true;


                    var jsonFeedback = JsonConvert.SerializeObject(feedback);
                    var content = new StringContent(jsonFeedback, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync($"{ip}/Feedbacks", content);

                    MessageBox.Show("Заявка успешно отправлена. Ожидайте ответа на указанной почте!");
                    tbMessage.Text = string.Empty;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Заявка успешно отправлена. Ожидайте ответа на указанной почте!");
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void deleteBookFromBasket_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.DataContext is Basket basketItem)
                {

                    if (Basket != null)
                    {
                        Basket.Remove(basketItem);
                    }
                    else
                    {

                    }


                    ListItemsBasket.ItemsSource = null;
                    ListItemsBasket.ItemsSource = Basket;


                    DeleteItemFromApi(basketItem);
                }
            }
        }

        private async void DeleteItemFromApi(Basket basketItem)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Baskets/{basketItem.IdBasket}"; 

                    var response = await httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        LoadBooksBasket();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при удалении элемента из корзины. Код ошибки: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении элемента из корзины: " + ex.Message);
            }
        }

        private async void EnterPromocode_Click(object sender, RoutedEventArgs e)
        {
            string enteredPromocode = tbPromocodeEnterBasket.Text;

            var promocodes = await GetPromocodesFromApi();

            if (promocodes != null)
            {
                var matchingPromocode = promocodes.FirstOrDefault(p => p.NamePromocode == enteredPromocode);

                if (matchingPromocode != null)
                {

                    decimal discountAmount = (totalCostWithPromocode * matchingPromocode.Discount) / 100;
                    totalCostWithPromocode -= discountAmount;

                    finalPriceBasket.Text = $"Итоговая стоимость: {totalCostWithPromocode} рублей";

                    MessageBox.Show($"Промокод '{enteredPromocode}' успешно применен. Скидка {matchingPromocode.Discount}%.");
                }
                else
                {
                    MessageBox.Show("Введенный промокод не найден.");
                }
            }
        }

        private async Task<List<Promocode>> GetPromocodesFromApi()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Promocodes";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var promocodes = JsonConvert.DeserializeObject<List<Promocode>>(content);
                        return promocodes;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные о промокодах с сервера.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
                return null;
            }
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            popupDownload.IsOpen = true;
        }

        private void DownloadButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string extension = (string)clickedButton.Content;

            if (popupDownload.DataContext is Book selectedBook)
            {
                string fieldToUse = (extension == ".fb2") ? selectedBook.FormatFB2 : selectedBook.FormatTXT;

                if (!string.IsNullOrEmpty(fieldToUse))
                {
                    string filePath = fieldToUse;

                    string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";

                    string currentDateFolder = "Книжня - книги";
                    string destinationFolderPath = System.IO.Path.Combine(downloadsFolder, currentDateFolder);

                    if (!Directory.Exists(destinationFolderPath))
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    string fileName = selectedBook.NameBook + extension;
                    string destinationPath = System.IO.Path.Combine(destinationFolderPath, fileName);

                    try
                    {
                        File.Copy(filePath, destinationPath);

                        MessageBox.Show("Книга успешно скопирована в папку 'Загрузки'.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при копировании файла: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось найти файл для скачивания.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу, которую вы хотите скопировать в 'Загрузки'.");
            }
        }

        private void btnExitDownload_Click(object sender, RoutedEventArgs e)
        {
            popupDownload.IsOpen = false;

        }

        private void rbCard_Checked(object sender, RoutedEventArgs e)
        {
            if (paymentDetails != null)
            {
                paymentDetails.Visibility = Visibility.Visible;
                phoneDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void rbPhone_Checked(object sender, RoutedEventArgs e)
        {
            if (paymentDetails != null)
            {
                paymentDetails.Visibility = Visibility.Collapsed;
                phoneDetails.Visibility = Visibility.Visible;
            }
        }


        private void phoneTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDigitKey(e.Key) && !IsControlKey(e.Key))
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = (TextBox)sender;
            string currentText = textBox.Text;

            if (e.Key == Key.Back)
            {
                if (currentText.StartsWith("+7") && textBox.CaretIndex <= 2)
                {
                    e.Handled = true;
                }
            }
        }

        private bool IsDigitKey(Key key)
        {
            return key >= Key.D0 && key <= Key.D9 || key >= Key.NumPad0 && key <= Key.NumPad9;
        }

        private bool IsControlKey(Key key)
        {
            return key == Key.Back || key == Key.Delete || key == Key.Left || key == Key.Right || key == Key.Home || key == Key.End;
        }

        private void btnExitBuy_Click(object sender, RoutedEventArgs e)
        {
            popupBuyCatalog.IsOpen = false;
        }

        private void dateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = (TextBox)sender;
            string currentText = textBox.Text;
            string inputText = e.Text;

            string digits = new string(currentText.Where(char.IsDigit).ToArray());

            if (digits.Length == 2 && !currentText.Contains("/"))
            {
                currentText += "/";
            }

            string formattedText = currentText + e.Text;

            if (formattedText.Length > 5)
            {
                e.Handled = true;
                return;
            }

            textBox.Text = formattedText;

            e.Handled = true;
        }

        private void CardNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text) && !e.Text.All(char.IsDigit))
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = (TextBox)sender;
            string currentText = textBox.Text;
            string inputText = e.Text;

            if (currentText.Length < 19)
            {
                string digits = new string(currentText.Where(char.IsDigit).ToArray());

                if (digits.Length == 4 || digits.Length == 8 || digits.Length == 12)
                {
                    currentText += " ";
                }

                currentText += inputText;

                textBox.Text = currentText;
                textBox.CaretIndex = currentText.Length;
            }

            e.Handled = true;
        }

        private async void tbPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            bool isValid = true;

            if (rbPhone.IsChecked == true)
            {
                if (phoneTextBox.Text == null || phoneTextBox.Text.Length < 12)
                {
                    ShowMessage("Введите верный номер телефона!");

                    isValid = false;
                }
            }
            else if (rbCard.IsChecked == true)
            {
                if (cvcCode.Text == null || dateTextBox.Text == null || numberCard.Text == null || cvcCode.Text.Length < 3 || dateTextBox.Text.Length < 5 || numberCard.Text.Length < 19)
                {
                    ShowMessage("Все данные должны быть заполнены!");
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (basketOrder == false) {
                    tbPromocodeEnterOrder.Visibility = Visibility.Visible;
                    EnterPromocodeOrder.Visibility = Visibility.Visible;

                    if (button.DataContext is Book selectedBook)
                    {

                        popupBuyCatalog.DataContext = selectedBook;
                        Random random = new Random();

                        int barcodeNumber = random.Next(1000, 99999999);


                        btnBuyInPopup.DataContext = selectedBook;
                        IssueProduct issueItem = new IssueProduct
                        {
                            Barcode = barcodeNumber.ToString(),
                            RiderTicketId = 1,
                            BookId = selectedBook.IdBook,
                            CostIssueFix = totalCostWithPromocodeOrderOneBook,
                            Deleted_Issue_Product = false
                        };



                        bool addToBookSuccess = await AddToOrder(issueItem);

                        LoadBooksIssue();

                        tbPromocodeEnterOrder.Text = null;
                        popupBuyCatalog.IsOpen = false;


                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите книгу, которую вы хотите купить.");
                    }
                } else if (basketOrder == true)
                {

                    if (Basket.Count > 0)
                    {
                        decimal totalCostForSelectedBooks = Basket.Sum(basketItem => (decimal)(basketItem.Book.Price ?? 0m));

                        finalPriceOrder.Text = $"Сумма к оплате: {totalCostForSelectedBooks} ₽";

                        popupBuyCatalog.IsOpen = true;



                        List<IssueProduct> issueProducts = new List<IssueProduct>();
                        foreach (var basketItem in Basket)
                        {
                            Random random = new Random();

                            int barcodeNumber = random.Next(1000, 99999999);
                            IssueProduct issueItem = new IssueProduct
                            {
                                Barcode = barcodeNumber.ToString(),
                                RiderTicketId = 1,
                                BookId = basketItem.Book.IdBook,
                                CostIssueFix = (decimal)(basketItem.Book.Price ?? 0m),
                                Deleted_Issue_Product = false
                            };

                            bool addToBookSuccess = await AddToOrder(issueItem);
                            LoadBooksIssue();
                        }

                        DeleteAllItemsFromBasket();
                    }
                    else
                    {
                        MessageBox.Show("Ваша корзина пуста. Пожалуйста, выберите книги для покупки.");
                    }
                }
               
            }
        }

        private async void DeleteAllItemsFromBasket()
        {
            if (Basket.Count > 0)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                        var url = $"{ip}/Baskets/DeleteAll"; 

                        var response = await httpClient.DeleteAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            Basket.Clear();
                            ListItemsBasket.ItemsSource = null;
                            countBooksBasket.Text = "В вашей корзине 0 книг:";
                            finalPriceBasket.Text = "Итоговая стоимость: 0 рублей";
                            tbPromocodeEnterOrder.Visibility = Visibility.Visible;
                            EnterPromocodeOrder.Visibility = Visibility.Visible;
                            basketOrder = false;
                            popupBuyCatalog.IsOpen= false;

                        }
                        else
                        {
                            MessageBox.Show("Произошла ошибка при удалении элементов из корзины. Код ошибки: " + response.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении элементов из корзины: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ваша корзина уже пуста.");
            }
        }


        private Window GetActiveWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.IsActive)
                {
                    return window;
                }
            }
            return null;
        }

        private void ShowMessage(string message)
        {
            Window messageWindow = new Window
            {
                Title = "Сообщение",
                Content = new TextBlock
                {
                    Text = message,
                    Margin = new Thickness(10)
                },
                SizeToContent = SizeToContent.WidthAndHeight,
                Topmost = true,
                  WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            Window parentWindow = GetActiveWindow();

            if (parentWindow != null)
            {
                messageWindow.Owner = parentWindow;

                messageWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show(message, "Сообщение");
            }
        }


        private async void EnterPromocodeOrder_Click(object sender, RoutedEventArgs e)
        {
            string enteredPromocode = tbPromocodeEnterOrder.Text;

            var promocodes = await GetPromocodesFromApi();

            if (promocodes != null)
            {
                var matchingPromocode = promocodes.FirstOrDefault(p => p.NamePromocode == enteredPromocode);

                if (matchingPromocode != null)
                {

                    decimal discountAmount = (totalCostWithPromocodeOrderOneBook * matchingPromocode.Discount) / 100;
                    totalCostWithPromocodeOrderOneBook -= discountAmount;

                    finalPriceOrder.Text = $"Сумма к оплате: {totalCostWithPromocodeOrderOneBook} ₽";

                    MessageBox.Show($"Промокод '{enteredPromocode}' успешно применен. Скидка {matchingPromocode.Discount}%.");
                }
                else
                {
                    MessageBox.Show("Введенный промокод не найден.");
                }
            }
        }

        private async void btnBuyBasketBooks_Click(object sender, RoutedEventArgs e)
        {
            popupBuyCatalog.IsOpen = true;

            basketOrder = true;
            tbPromocodeEnterOrder.Visibility = Visibility.Hidden;
            EnterPromocodeOrder.Visibility = Visibility.Hidden;
            decimal totalCostForSelectedBooks = Basket.Sum(basketItem => (decimal)(basketItem.Book.Price ?? 0m));
            finalPriceOrder.Text = $"Сумма к оплате: {totalCostForSelectedBooks} ₽";

        }
    }
}
