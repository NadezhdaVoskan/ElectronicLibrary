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
using System.Security.Policy;
using ElectronicLibraryAPI.Models;
using System.Net.Mail;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Legends;
using ElectronicLibraryAPI2.Models;
using System.Collections.ObjectModel;
using Publisher = ElectronicLibrary.Models.Publisher;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace ElectronicLibrary
{
    /// <summary>
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : Window
    {
        private string ip;
        private string sortOrder = null;
        string searchString = null;

        private int selectedFeedbackId;

        public List<ReturnBook> ReturnBookDetailsList { get; set; }

        public List<Feedback> FeedbackDetailsList { get; set; }

        public ObservableCollection<string> BooksCollection { get; set; }

        private string rootDirectory = "D:\\ASP projects\\ElectronicLibrary\\ElectronicLibrary";

        private string imagePath;
        private string fb2FilePath;
        private string txtFilePath;

        private HttpClient GetConfiguredHttpClient()
        {
            var httpClient = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            return httpClient;
        }


        public Seller()
        {
            InitializeComponent();
            Loaded += UserSeller_Loaded;
            string apiUrl = ((App)App.Current).ApiUrl;
            ip = apiUrl;

            ReturnBookDetailsList = new List<ReturnBook>();
            FeedbackDetailsList = new List<Feedback>();
            DataContext = this;

            BooksCollection = new ObservableCollection<string>();
        }

        private void UserSeller_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
            LoadPromocode();
            LoadReturnBookDetails();
            LoadFeedbackDetails();

            LoadAuthor();
            LoadGenre();
            LoadTypeLiterature();
            LoadPublisher();
        }


        private async Task LoadReturnBookDetails()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{ip}/ReturnBooks";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var returnBooks = JsonConvert.DeserializeObject<List<ReturnBook>>(content);


                        ReturnBookDetailsList.Clear();

                        foreach (var returnBook in returnBooks)
                        {
                            var bookId = returnBook.BookId;
                            var bookUrl = $"{ip}/Books/{bookId}";
                            var bookResponse = await httpClient.GetAsync(bookUrl);

                            if (bookResponse.IsSuccessStatusCode)
                            {
                                var bookContent = await bookResponse.Content.ReadAsStringAsync();
                                var book = JsonConvert.DeserializeObject<Book>(bookContent);

                                returnBook.Book = book;
                            }

                            if (!returnBook.DeletedReturn)
                            {
                                ReturnBookDetailsList.Add(returnBook);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные о возврате книг.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных о возврате: " + ex.Message);
            }
        }





        private async Task LoadFeedbackDetails()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{ip}/Feedbacks";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var feedBack = JsonConvert.DeserializeObject<List<Feedback>>(content);

                        FeedbackDetailsList.Clear();

                        foreach (var feedBacks in feedBack)
                        {
                           

                            if (!feedBacks.Done)
                            {
                                FeedbackDetailsList.Add(feedBacks);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные о возврате книг.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных о возврате: " + ex.Message);
            }
        }



        private async Task LoadPromocode()
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

                        dgPromocode.ItemsSource = promocodes;
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

        private async Task AddPromocodeAsync(Promocode promocode)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Promocodes";

                    var content = new StringContent(JsonConvert.SerializeObject(promocode), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ошибка добавления промокода");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private async Task UpdatePromocodeAsync(Promocode promocode)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Promocodes/{promocode.IdPromocode}";

                    var content = new StringContent(JsonConvert.SerializeObject(promocode), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ошибка обновления промокода");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private async Task DeletePromocodeAsync(int promocodeId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    var url = $"{ip}/Promocodes/{promocodeId}";

                    var response = await httpClient.DeleteAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ошибка при удалении промокода");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private async void btnAddPromocode_Click(object sender, RoutedEventArgs e)
        {
            Promocode newPromocode = new Promocode
            {
                NamePromocode = tbNamePromocode.Text,
                Discount = int.Parse(tbDiscount.Text)
            };

            await AddPromocodeAsync(newPromocode);
            await LoadPromocode(); 
        }

        private async void btnChangePromocode_Click(object sender, RoutedEventArgs e)
        {
            if (dgPromocode.SelectedItem != null)
            {
                Promocode selectedPromocode = (Promocode)dgPromocode.SelectedItem;
                selectedPromocode.NamePromocode = tbNamePromocode.Text;
                selectedPromocode.Discount = int.Parse(tbDiscount.Text);

                await UpdatePromocodeAsync(selectedPromocode);
                await LoadPromocode();
            }
        }

        private async void btnDeletePromocode_Click(object sender, RoutedEventArgs e)
        {
            if (dgPromocode.SelectedItem != null)
            {
                Promocode selectedPromocode = (Promocode)dgPromocode.SelectedItem;

                await DeletePromocodeAsync(selectedPromocode.IdPromocode);
                await LoadPromocode();
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

                        books = books.Where(b => b.Deleted_Book != true).ToList();

                        ListViewProducts.ItemsSource = books;


                        dgBook.ItemsSource = books;
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
                popup.IsOpen = true;
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
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

        private void dgPromocode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPromocode.SelectedItem != null)
            {
                Promocode selectedPromocode = (Promocode)dgPromocode.SelectedItem;

                tbNamePromocode.Text = selectedPromocode.NamePromocode;
                tbDiscount.Text = selectedPromocode.Discount.ToString();
            }
        }

        private async void btnAgreeReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is ReturnBook selectedReturnBook)
                {
                    selectedReturnBook.DeletedReturn = true;
                    selectedReturnBook.Book = null;

                    await UpdateReturnBookAsync(selectedReturnBook);

                    await LoadReturnBookDetails();
                }
                else
                {
                    MessageBox.Show("Не удалось получить информацию о возвращаемой книге.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обработке возврата: " + ex.Message);
            }
        }



        private async Task UpdateReturnBookAsync(ReturnBook returnBook)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{ip}/ReturnBooks/{returnBook.IdReturnBook}";

                    var content = new StringContent(JsonConvert.SerializeObject(returnBook), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ошибка обновления информации о возврате книги");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }


        private void btnNoAgreeReturn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ReturnBook selectedReturnBook)
            {
                popNoAgreeReturn.Tag = selectedReturnBook;
                popNoAgreeReturn.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Не удалось получить информацию о возвращаемой книге.");
            }
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sender is Button button && button.Tag is Feedback selectedFeedback)
                {
                    selectedFeedbackId = selectedFeedback.IdFeedback;

                    popupFeedback.IsOpen = true;
                }
                else
                {
                    MessageBox.Show("Не удалось получить информацию об обратной связи.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обработке обратной связи: " + ex.Message);
            }
        }

        public int GetSelectedFeedbackId()
        {
            return selectedFeedbackId;
        }

        private void canelPopupFeedback_Click(object sender, RoutedEventArgs e)
        {
            popupFeedback.IsOpen = false;
        }

        private void canelReturnBookPopup_Click(object sender, RoutedEventArgs e)
        {
            popNoAgreeReturn.IsOpen = false;
        }

        private async void tbSendAnswerFeedback_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedId = GetSelectedFeedbackId();

                Feedback selectedFeedback = FeedbackDetailsList.FirstOrDefault(feedback => feedback.IdFeedback == selectedId);

                if (selectedFeedback != null)
                {
                    string answerText = tbAnswerFeedback.Text;

                    selectedFeedback.Done = true;

                    await UpdateFeedbackAsync(selectedFeedback);

                    await SendEmailAsync(selectedFeedback.EmailUserMessage, answerText);

                    popupFeedback.IsOpen = false;

                    await LoadFeedbackDetails();
                }
                else
                {
                    MessageBox.Show("Не удалось получить информацию об обратной связи.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при отправке ответа: " + ex.Message);
            }
        }


        private async Task UpdateFeedbackAsync(Feedback feedback)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"{ip}/Feedbacks/{feedback.IdFeedback}";

                    var content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(url, content);

                    await LoadFeedbackDetails();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ошибка обновления информации об обратной связи");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private async Task SendEmailAsync(string email, string message)
        {
            MailAddress from = new MailAddress("myrka.mur2014@yandex.ru", "Электронная библиотека 'Книжня'");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);

            m.Subject = "Обратная связь";

            m.Body = $"<h3>{message}</h3>";

            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
  
            smtp.Credentials = new NetworkCredential("myrka.mur2014@yandex.ru", "bbwtjjnzddjhfeen");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }



        private async void btnDiagrem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime fromDate = datePickerFrom.SelectedDate ?? DateTime.MinValue;
                DateTime toDate = datePickerTo.SelectedDate ?? DateTime.MaxValue;

                List<IssueProduct> salesData = await LoadIssueProducts(fromDate, toDate);

                var groupedData = salesData
                    .OrderBy(item => item.DateIssue)
                    .GroupBy(item => item.DateIssue.Date)
                    .Select(group => new
                    {
                        Date = group.Key,
                        TotalSales = group.Sum(item => item.CostIssueFix ?? 0)
                    })
                    .ToList();

                var plotModel = new PlotModel
                {
                    Title = "Продажи"
                };

                var categoryAxis = new CategoryAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Дата",
                    Angle = 45,
                    IntervalLength = 0.5,
                    IsZoomEnabled = false
                };

                var valueAxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Общая выручка"
                };

                foreach (var data in groupedData)
                {
                    var barSeries = new BarSeries
                    {
                        Title = $"Продажи {data.Date.ToString("dd.MM.yyyy")}",
                        StrokeColor = OxyColors.Black,
                        StrokeThickness = 1,
                        BarWidth = 1.0
                    };


                    barSeries.Items.Add(new BarItem { Value = (double)data.TotalSales });

                   // categoryAxis.Labels.Add(data.Date.ToString("dd.MM.yyyy"));
                    plotModel.Series.Add(barSeries);
                }

                plotModel.Axes.Add(categoryAxis);
                plotModel.Axes.Add(valueAxis);

                salesPlot.Model = plotModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при построении диаграммы: " + ex.Message);
            }
        }




        private async Task<List<IssueProduct>> LoadIssueProducts(DateTime fromDate, DateTime toDate)
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

                        issueProducts = issueProducts
                            .Where(ip => ip.Deleted_Issue_Product != true && ip.DateIssue >= fromDate && ip.DateIssue <= toDate)
                            .ToList();

                        return issueProducts;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные с сервера.");
                        return new List<IssueProduct>();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
                return new List<IssueProduct>();
            }
        }

        private async void btnFindReaderTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string readerTicket = tbReaderTicket.Text;

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"{ip}/ReaderCard/GetBooksByRiderTicket?numberRiderTicket={readerTicket}");

                if (response.IsSuccessStatusCode)
                {
                    string booksJson = await response.Content.ReadAsStringAsync();
                    string[] booksArray = booksJson.Split(',');

                    BooksCollection.Clear();

                    foreach (var book in booksArray)
                    {
                        BooksCollection.Add(book);
                    }
                }
                else
                {
                    MessageBox.Show($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void btnSendReasonNotReturn_Click(object sender, RoutedEventArgs e)
        {
            if (popNoAgreeReturn.Tag is ReturnBook selectedReturnBook)
            {
                selectedReturnBook.ReasonNoAgree = tbReasonNotReturn.Text;
                selectedReturnBook.ReturnAgree = false;
                selectedReturnBook.DeletedReturn = true;
                selectedReturnBook.Book = null;

                await UpdateReturnBookAsync(selectedReturnBook);

                await LoadReturnBookDetails();

                popNoAgreeReturn.IsOpen = false;

                MessageBox.Show("Отказ на возврат средств успешно отправлен!");
            }
            else
            {
                MessageBox.Show("Не удалось получить информацию о возвращаемой книге.");
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
                MessageBox.Show($"Error retrieving : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                cbBookAuthor.ItemsSource = author;
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
                cbBookGenre.ItemsSource = genre;
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
                cbBookTypeLiterature.ItemsSource = typeLiterature;
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
                cbBookPublisher.ItemsSource = publisher;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg;)|*.png; *.jpg; *.jpeg; |All Files (*.*)|*.*";

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    imagePath = openFileDialog.FileName;

                    string relativePath = GetRelativePath(rootDirectory, imagePath);
                    imgBook.Source = new BitmapImage(new Uri(imagePath));
                    imagePath = relativePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void btnDownloadFb2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "FictionBook Files (*.fb2)|*.fb2|All Files (*.*)|*.*";

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    fb2FilePath = openFileDialog.FileName;

                    string relativePath = GetRelativePath(rootDirectory, fb2FilePath);

                    tbFileFB2.Text = relativePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }






        private void btnDownloadTXT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "FictionBook Files (*.txt)|*.txt|All Files (*.*)|*.*";

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    txtFilePath = openFileDialog.FileName;

                    string relativePath = GetRelativePath(rootDirectory, txtFilePath);

                    tbFileTXT.Text = relativePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private string GetRelativePath(string root, string fullPath)
        {
            DirectoryInfo rootDir = new DirectoryInfo(root);
            FileInfo fileInfo = new FileInfo(fullPath);

            string relativePath = "";

            string[] rootPathParts = rootDir.FullName.Split(System.IO.Path.DirectorySeparatorChar);
            string[] fullPathParts = fileInfo.FullName.Split(System.IO.Path.DirectorySeparatorChar);

            int commonRootIndex = 0;
            while (commonRootIndex < Math.Min(rootPathParts.Length, fullPathParts.Length) &&
                   rootPathParts[commonRootIndex].Equals(fullPathParts[commonRootIndex], StringComparison.OrdinalIgnoreCase))
            {
                commonRootIndex++;
            }

            relativePath = System.IO.Path.Combine(fullPathParts.Skip(commonRootIndex).ToArray());
            relativePath = $"..{System.IO.Path.DirectorySeparatorChar}{relativePath}";

            return relativePath;
        }


        private async void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book newBook = new Book
                {
                    NameBook = tbBookName.Text,
                    BriefPlot = tbBookNotes.Text,
                    CoverPhoto = imagePath,
                    FormatFB2 = tbFileFB2.Text,
                    FormatTXT = tbFileTXT.Text
                };

                if (int.TryParse(tbBookPage.Text, out int numberPages))
                {
                    newBook.NumberPages = numberPages;
                }
                else
                {
                    MessageBox.Show("Некорректное значение для количества страниц.");
                    return; 
                }
           
                if (decimal.TryParse(tbBookPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal price))
                {
                    newBook.Price = price;
                }
                else
                {
                    MessageBox.Show("Некорректное значение для цены.");
                    return;
                }


                if (DateTime.TryParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate))
                {
                    newBook.PublicationDate = publicationDate;
                }
                else
                {
                    MessageBox.Show("Некорректное значение для даты публикации. Используйте формат yyyy-MM-dd.");
                    return;
                }

                string formattedDate = newBook.PublicationDate.ToString("yyyy-MM-dd");

                if (string.IsNullOrWhiteSpace(newBook.NameBook) || string.IsNullOrWhiteSpace(newBook.NumberPages.ToString()) || string.IsNullOrWhiteSpace(newBook.Price.ToString()) ||
                    string.IsNullOrWhiteSpace(newBook.BriefPlot))
                {
                    MessageBox.Show("Пожалуйста, заполните все данные.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        var apiUrl = ((App)App.Current).ApiUrl;
                        var json = JsonConvert.SerializeObject(new
                        {
                            newBook.NameBook,
                            newBook.BriefPlot,
                            newBook.CoverPhoto,
                            newBook.FormatFB2,
                            newBook.FormatTXT,
                            newBook.NumberPages,
                            newBook.Price,
                            PublicationDate = formattedDate
                        });

                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync($"{apiUrl}/Books", content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var responseContent = await response.Content.ReadAsStringAsync();
                                var createdBook = JsonConvert.DeserializeObject<Book>(responseContent);

                                int createdBookId = createdBook.IdBook;

                                Author selectedAuthor = (Author)cbBookAuthor.SelectedItem;
                                Genre selectedGenre = (Genre)cbBookGenre.SelectedItem;
                                TypeLiterature selectedTypeLiterature = (TypeLiterature)cbBookTypeLiterature.SelectedItem;
                                Publisher selectedPublisher = (Publisher)cbBookPublisher.SelectedItem;

                                if (selectedAuthor != null )
                                {
                                    var authorView = new AuthorView
                                    {
                                        BookId = createdBookId,
                                        AuthorId = selectedAuthor.IdAuthor
                                    };

                                    var authorViewJson = JsonConvert.SerializeObject(authorView);
                                    var authorViewContent = new StringContent(authorViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PostAsync($"{apiUrl}/AuthorViews", authorViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при добавлении связи с автором", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пожалуйста, выберите автора", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

                                if (selectedGenre != null)
                                {
                                    var genreView = new GenreView
                                    {
                                        BookId = createdBookId,
                                        GenreId = selectedGenre.IdGenre
                                    };

                                    var genreViewJson = JsonConvert.SerializeObject(genreView);
                                    var genreViewContent = new StringContent(genreViewJson, Encoding.UTF8, "application/json");

                                    using (var genreViewResponse = await httpClient.PostAsync($"{apiUrl}/GenreViews", genreViewContent))
                                    {
                                        if (genreViewResponse.IsSuccessStatusCode)
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при добавлении связи с жанром", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пожалуйста, выберите жанр", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

                                if (selectedTypeLiterature != null)
                                {
                                    var typeLiteratureView = new TypeLiteratureView
                                    {
                                        BookId = createdBookId,
                                        TypeLiteratureId = selectedTypeLiterature.IdTypeLiterature
                                    };

                                    var typeLiteratureViewJson = JsonConvert.SerializeObject(typeLiteratureView);
                                    var typeLiteratureViewContent = new StringContent(typeLiteratureViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PostAsync($"{apiUrl}/TypeLiteratureViews", typeLiteratureViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при добавлении связи с типом литературы", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пожалуйста, выберите тип литературы", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

                                if (selectedPublisher != null)
                                {
                                    var publisherView = new PublisherView
                                    {
                                        BookId = createdBookId,
                                        PublisherId = selectedPublisher.IdPublisher
                                    };

                                    var authorViewJson = JsonConvert.SerializeObject(publisherView);
                                    var authorViewContent = new StringContent(authorViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PostAsync($"{apiUrl}/PublisherViews", authorViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {
                                            tbBookName.Text = "";
                                            tbBookNotes.Text = "";
                                            tbBookPage.Text = "";
                                            tbBookPrice.Text = "";
                                            tbFileFB2.Text = "";
                                            tbFileTXT.Text = "";
                                            imgBook.Source = null;
                                            cbBookAuthor.SelectedItem = null;
                                            cbBookGenre.SelectedItem = null;
                                            cbBookPublisher.SelectedItem = null;
                                            cbBookTypeLiterature.SelectedItem = null;
                                            await LoadBooks();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при добавлении связи с издателем", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пожалуйста, выберите издателя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
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


        private async void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    Book selectedBook = (Book)dgBook.SelectedItem;

                    if (selectedBook != null)
                    {
                        selectedBook.NameBook = tbBookName.Text;
                        selectedBook.BriefPlot = tbBookNotes.Text;

                        selectedBook.FormatFB2 = tbFileFB2.Text;
                        selectedBook.FormatTXT = tbFileTXT.Text;

                        if (int.TryParse(tbBookPage.Text, out int numberPages))
                        {
                            selectedBook.NumberPages = numberPages;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для количества страниц.");
                            return;
                        }

                        if (decimal.TryParse(tbBookPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal price))
                        {
                            selectedBook.Price = price;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для цены.");
                            return;
                        }


                        if (DateTime.TryParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate))
                        {
                            selectedBook.PublicationDate = publicationDate;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для даты публикации. Используйте формат yyyy-MM-dd.");
                            return;
                        }

                        string formattedDate = selectedBook.PublicationDate.ToString("yyyy-MM-dd");

                        if (string.IsNullOrWhiteSpace(selectedBook.NameBook) || string.IsNullOrWhiteSpace(selectedBook.NumberPages.ToString()) || string.IsNullOrWhiteSpace(selectedBook.Price.ToString()) ||
                            string.IsNullOrWhiteSpace(selectedBook.BriefPlot))
                        {
                            MessageBox.Show("Пожалуйста, заполните все данные.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }



                        var updatedBookJson = JsonConvert.SerializeObject(selectedBook);
                        var updatedBookContent = new StringContent(updatedBookJson, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{ip}/Books/{selectedBook.IdBook}", updatedBookContent))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                Author selectedAuthor = (Author)cbBookAuthor.SelectedItem;
                                if (selectedAuthor != null)
                                {
                                    string selectedAuthorView = GetAuthorViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedAuthorView, out int selectedAuthorViewInt)) { }
                                    else { }

                                    var authorView = new AuthorView
                                    {
                                        IdAuthorView = selectedAuthorViewInt,
                                        BookId = selectedBook.IdBook,
                                        AuthorId = selectedAuthor.IdAuthor
                                    };

                                    var authorViewJson = JsonConvert.SerializeObject(authorView);
                                    var authorViewContent = new StringContent(authorViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/AuthorViews/{selectedAuthorViewInt}", authorViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {
                                           
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с автором", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                Genre selectedGenre = (Genre)cbBookGenre.SelectedItem;
                                if (selectedGenre != null)
                                {
                                    string selectedGenreView = GetGenreViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedGenreView, out int selectedGenreViewInt)) { }
                                    else { }

                                    var genreView = new GenreView
                                    {
                                        IdGenreView = selectedGenreViewInt,
                                        BookId = selectedBook.IdBook,
                                        GenreId = selectedGenre.IdGenre
                                    };

                                    var genreViewJson = JsonConvert.SerializeObject(genreView);
                                    var genreViewContent = new StringContent(genreViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/GenreViews/{selectedGenreViewInt}", genreViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с жанром", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                Publisher selectedPublisher = (Publisher)cbBookPublisher.SelectedItem;
                                if (selectedPublisher != null)
                                {
                                    string selectedPublisherView = GetPublisherViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedPublisherView, out int selectedPublisherViewInt)) { }
                                    else { }

                                    var publisherView = new PublisherView
                                    {
                                        IdPublisherView = selectedPublisherViewInt,
                                        BookId = selectedBook.IdBook,
                                        PublisherId = selectedPublisher.IdPublisher
                                    };

                                    var publisherViewJson = JsonConvert.SerializeObject(publisherView);
                                    var publisherViewContent = new StringContent(publisherViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/PublisherViews/{selectedPublisherViewInt}", publisherViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с издателем", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                TypeLiterature selectedTypeLiterature = (TypeLiterature)cbBookTypeLiterature.SelectedItem;
                                if (selectedTypeLiterature != null)
                                {
                                    string selectedTypeLiteratureView = GetTypeLiteratureViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedTypeLiteratureView, out int selectedTypeLiteratureViewInt)) { }
                                    else { }

                                    var typeLiteratureView = new TypeLiteratureView
                                    {
                                        IdTypeLiteratureView = selectedTypeLiteratureViewInt,
                                        BookId = selectedBook.IdBook,
                                        TypeLiteratureId = selectedTypeLiterature.IdTypeLiterature
                                    };

                                    var typeLiteratureViewJson = JsonConvert.SerializeObject(typeLiteratureView);
                                    var typeLiteratureViewContent = new StringContent(typeLiteratureViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/TypeLiteratureViews/{selectedTypeLiteratureViewInt}", typeLiteratureViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {
                                            tbBookName.Text = "";
                                            tbBookNotes.Text = "";
                                            tbBookPage.Text = "";
                                            tbBookPrice.Text = "";
                                            tbFileFB2.Text = "";
                                            tbFileTXT.Text = "";
                                            imgBook.Source = null;
                                            cbBookAuthor.SelectedItem = null;
                                            cbBookGenre.SelectedItem = null;
                                            cbBookPublisher.SelectedItem = null;
                                            cbBookTypeLiterature.SelectedItem = null;
                                            await LoadBooks();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с типом литературы", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении книги", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите книгу из списка", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetAuthorViewByIdBook(int bookId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"{ip}/AuthorViews/GetAuthorViewIdByBookId/{bookId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    return content;
                }
                else
                {
                    return null;
                }
            }
        }

        private string GetGenreViewByIdBook(int bookId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"{ip}/GenreViews/GetGenreViewIdByBookId/{bookId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    return content;
                }
                else
                {
                    return null;
                }
            }
        }

        private string GetPublisherViewByIdBook(int bookId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"{ip}/PublisherViews/GetPublisherViewIdByBookId/{bookId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    return content;
                }
                else
                {
                    return null;
                }
            }
        }

        private string GetTypeLiteratureViewByIdBook(int bookId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"{ip}/TypeLiteratureViews/GetTypeLiteratureViewIdByBookId/{bookId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    return content;
                }
                else
                {
                    return null;
                }
            }
        }

        private async void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var httpClient = new HttpClient())
                {
                    Book selectedBook = (Book)dgBook.SelectedItem;

                    if (selectedBook != null)
                    {
                        selectedBook.NameBook = tbBookName.Text;
                        selectedBook.BriefPlot = tbBookNotes.Text;

                        selectedBook.FormatFB2 = tbFileFB2.Text;
                        selectedBook.FormatTXT = tbFileTXT.Text;
                        selectedBook.Deleted_Book = true;

                        if (int.TryParse(tbBookPage.Text, out int numberPages))
                        {
                            selectedBook.NumberPages = numberPages;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для количества страниц.");
                            return;
                        }

                        if (decimal.TryParse(tbBookPrice.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal price))
                        {
                            selectedBook.Price = price;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для цены.");
                            return;
                        }


                        if (DateTime.TryParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publicationDate))
                        {
                            selectedBook.PublicationDate = publicationDate;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для даты публикации. Используйте формат yyyy-MM-dd.");
                            return;
                        }

                        string formattedDate = selectedBook.PublicationDate.ToString("yyyy-MM-dd");

                        if (string.IsNullOrWhiteSpace(selectedBook.NameBook) || string.IsNullOrWhiteSpace(selectedBook.NumberPages.ToString()) || string.IsNullOrWhiteSpace(selectedBook.Price.ToString()) ||
                            string.IsNullOrWhiteSpace(selectedBook.BriefPlot))
                        {
                            MessageBox.Show("Пожалуйста, заполните все данные.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }



                        var updatedBookJson = JsonConvert.SerializeObject(selectedBook);
                        var updatedBookContent = new StringContent(updatedBookJson, Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync($"{ip}/Books/{selectedBook.IdBook}", updatedBookContent))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                Author selectedAuthor = (Author)cbBookAuthor.SelectedItem;
                                if (selectedAuthor != null)
                                {
                                    string selectedAuthorView = GetAuthorViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedAuthorView, out int selectedAuthorViewInt)) { }
                                    else { }

                                    var authorView = new AuthorView
                                    {
                                        IdAuthorView = selectedAuthorViewInt,
                                        BookId = selectedBook.IdBook,
                                        AuthorId = selectedAuthor.IdAuthor,
                                        
                                    };

                                    var authorViewJson = JsonConvert.SerializeObject(authorView);
                                    var authorViewContent = new StringContent(authorViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/AuthorViews/{selectedAuthorViewInt}", authorViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с автором", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                Genre selectedGenre = (Genre)cbBookGenre.SelectedItem;
                                if (selectedGenre != null)
                                {
                                    string selectedGenreView = GetGenreViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedGenreView, out int selectedGenreViewInt)) { }
                                    else { }

                                    var genreView = new GenreView
                                    {
                                        IdGenreView = selectedGenreViewInt,
                                        BookId = selectedBook.IdBook,
                                        GenreId = selectedGenre.IdGenre
                                    };

                                    var genreViewJson = JsonConvert.SerializeObject(genreView);
                                    var genreViewContent = new StringContent(genreViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/GenreViews/{selectedGenreViewInt}", genreViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с жанром", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                Publisher selectedPublisher = (Publisher)cbBookPublisher.SelectedItem;
                                if (selectedPublisher != null)
                                {
                                    string selectedPublisherView = GetPublisherViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedPublisherView, out int selectedPublisherViewInt)) { }
                                    else { }

                                    var publisherView = new PublisherView
                                    {
                                        IdPublisherView = selectedPublisherViewInt,
                                        BookId = selectedBook.IdBook,
                                        PublisherId = selectedPublisher.IdPublisher
                                    };

                                    var publisherViewJson = JsonConvert.SerializeObject(publisherView);
                                    var publisherViewContent = new StringContent(publisherViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/PublisherViews/{selectedPublisherViewInt}", publisherViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {

                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с издателем", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                                TypeLiterature selectedTypeLiterature = (TypeLiterature)cbBookTypeLiterature.SelectedItem;
                                if (selectedTypeLiterature != null)
                                {
                                    string selectedTypeLiteratureView = GetTypeLiteratureViewByIdBook(selectedBook.IdBook);

                                    if (int.TryParse(selectedTypeLiteratureView, out int selectedTypeLiteratureViewInt)) { }
                                    else { }

                                    var typeLiteratureView = new TypeLiteratureView
                                    {
                                        IdTypeLiteratureView = selectedTypeLiteratureViewInt,
                                        BookId = selectedBook.IdBook,
                                        TypeLiteratureId = selectedTypeLiterature.IdTypeLiterature
                                    };

                                    var typeLiteratureViewJson = JsonConvert.SerializeObject(typeLiteratureView);
                                    var typeLiteratureViewContent = new StringContent(typeLiteratureViewJson, Encoding.UTF8, "application/json");

                                    using (var authorViewResponse = await httpClient.PutAsync($"{ip}/TypeLiteratureViews/{selectedTypeLiteratureViewInt}", typeLiteratureViewContent))
                                    {
                                        if (authorViewResponse.IsSuccessStatusCode)
                                        {
                                            tbBookName.Text = "";
                                            tbBookNotes.Text = "";
                                            tbBookPage.Text = "";
                                            tbBookPrice.Text = "";
                                            tbFileFB2.Text = "";
                                            tbFileTXT.Text = "";
                                            imgBook.Source = null;
                                            cbBookAuthor.SelectedItem = null;
                                            cbBookGenre.SelectedItem = null;
                                            cbBookPublisher.SelectedItem = null;
                                            cbBookTypeLiterature.SelectedItem = null;
                                            await LoadBooks();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ошибка при обновлении связи с типом литературы", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении книги", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                        tbBookName.Text = "";
                        tbBookNotes.Text = "";
                        tbBookPage.Text = "";
                        tbBookPrice.Text = "";
                        tbFileFB2.Text = "";
                        tbFileTXT.Text = "";
                        imgBook.Source = null;
                        cbBookAuthor.SelectedItem = null;
                        cbBookGenre.SelectedItem = null;
                        cbBookPublisher.SelectedItem = null;
                        cbBookTypeLiterature.SelectedItem = null;
                        await LoadBooks();
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите книгу из списка", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка. Попробуйте еще раз", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selectedBook = (Book)dgBook.SelectedItem;

            if (selectedBook != null)
            {
                tbBookName.Text = selectedBook.NameBook;
                tbBookNotes.Text = selectedBook.BriefPlot;
                tbBookPage.Text = selectedBook.NumberPages.ToString();
                tbBookPrice.Text = selectedBook.Price.ToString().Replace(',', '.');
                tbFileFB2.Text = selectedBook.FormatFB2;
                tbFileTXT.Text = selectedBook.FormatTXT;

                if (selectedBook.CoverPhoto.Length >= 2)
                {
                    string modifiedCoverPhoto = selectedBook.CoverPhoto.Substring(2);

                    imgBook.Source = new BitmapImage(new Uri($"{rootDirectory}{modifiedCoverPhoto}"));
                }

            }
        }
    }
}
