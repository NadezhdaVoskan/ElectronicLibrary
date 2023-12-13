using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using ElectronicLibrary.Models;

namespace ElectronicLibrary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        RegistryKey registry = Registry.CurrentUser;
        private static readonly string registryPath = System.IO.Path.Combine(Registry.CurrentUser.Name, "Software", "MyLoginSaver", "ElectronicLibrary");
        private string ip;
        public SignIn()
        {
            InitializeComponent();
            LoadUserSession();
            string apiUrl = ((App)App.Current).ApiUrl;
            ip = apiUrl;
        }

        public static string EncodeDecrypt(string pas, ushort secretKey)
        {
            var ch = pas.ToArray(); 
            string newStr = "";      
            foreach (var c in ch)  
                newStr += TopSecret(c, secretKey); 
            return newStr;
        }

        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }
        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            ushort secretKey = 0x0088;
            string login = txtLogin.Text;
            string password = pswPassword.Password.ToString();
            string pas = EncodeDecrypt(password, secretKey);
            string log = EncodeDecrypt(login, secretKey);

            RegistryKey key = registry.CreateSubKey(registryPath, true);
            if (key != null)
            {
                key.SetValue("login", log);
                key.SetValue("password", pas);
                key.Close();
            }

            User user = new User()
            {
                Login = login,
                Password = password
            };

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync(ip + "/Users/Login", content))
                        {
                            try
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    string apiRes = await response.Content.ReadAsStringAsync();
                                    Token.token = apiRes;
                                    dynamic data = JObject.Parse(Token.token);
                                    string role = data.role;
                                    string idUser = data.id;
                                    UserID.IdUser = idUser;

                                    try
                                    {
                                        if (role == "3")
                                        {
                                            UserAccount userAc = new UserAccount(idUser);
                                            userAc.Show();
                                            this.Close();
                                        }
                                        else if (role == "2")
                                        {
                                            AdminAccount adminAc = new AdminAccount();
                                            adminAc.Show();
                                            this.Close();
                                        }
                                        else if (role == "1")
                                        {
                                            Seller seller = new Seller();
                                            seller.Show();
                                            this.Close();
                                        }
                                    }
                                    catch (Exception ex) { MessageBox.Show("Не верный логин или пароль."); }

                                }
                                else
                                {
                                    MessageBox.Show("Не верный логин или пароль.");
                                    string errorResponse = await response.Content.ReadAsStringAsync();
                                    Console.WriteLine(errorResponse);
                                    MessageBox.Show("Ответ сервера: " + errorResponse);
                                
                            }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не верный логин или пароль.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void LoadUserSession()
        {
            try
            {
                using (RegistryKey key = registry.OpenSubKey(registryPath))
                {
                    if (key != null)
                    {
                        string login = key.GetValue("login")?.ToString();
                        string password = key.GetValue("password")?.ToString();
                        if (!string.IsNullOrEmpty(login))
                        {
                            txtLogin.Text = EncodeDecrypt(login, 0x0088);
                        }
                        if (!string.IsNullOrEmpty(password))
                        {
                            pswPassword.Password = EncodeDecrypt(password, 0x0088);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }
    }
}
