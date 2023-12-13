using ElectronicLibrary.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ElectronicLibrary
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private string ip;
        public SignUp()
        {
            InitializeComponent();
            string apiUrl = ((App)App.Current).ApiUrl;
            ip = apiUrl;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private async void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSurnameReg.Text) || string.IsNullOrEmpty(tbNameReg.Text) || string.IsNullOrEmpty(tbMiddleNameReg.Text)  || string.IsNullOrEmpty(tbPasswordTwo.Password.ToString()) || string.IsNullOrEmpty(tbPassword.Password.ToString()))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            Regex nameRegex = new Regex(@"^[А-Я][а-я]{1,}$");
            if (!nameRegex.IsMatch(tbSurnameReg.Text) || !nameRegex.IsMatch(tbNameReg.Text) || (!nameRegex.IsMatch(tbMiddleNameReg.Text) && !string.IsNullOrEmpty(tbMiddleNameReg.Text)))
            {
                MessageBox.Show("ФИО должно содержать только русские буквы и начинаться с заглавной буквы.");
                return;
            }

            Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if (!passwordRegex.IsMatch(tbPassword.Password))
            {
                MessageBox.Show("Пароль должен содержать как минимум одну заглавную букву, одну строчную букву, одну цифру, один специальный символ и иметь длину не менее 8 символов.");
                return;
            }
            if (tbPassword.Password.ToString() != tbPasswordTwo.Password.ToString())
            {
                MessageBox.Show("Пароли должны совпадать.");
            }

            User obj = new User()
            {
                FirstName = tbNameReg.Text,
                SecondName = tbSurnameReg.Text,
                MiddleName = tbMiddleNameReg.Text,
                Login = tbLogin.Text,
                Password = tbPassword.Password.ToString(),
                RoleId = 3

             };

          
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync($"{ip}/Users", content))
                        {
                            try
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    string apiResp = await response.Content.ReadAsStringAsync();
                                    SignIn main = new SignIn();
                                    main.Show();
                                    Close();


                                }
                                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                                {
                                    Token key = new Token();
                                    key.RefreshToken();
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка!");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
    }
}
