using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using ElectronicLibrary;


namespace TestSignUp
{
    [TestClass]
    public class SignUpUnitTest
    {
        [TestMethod]

        public void CheckEmpty_LoginEmpty_ErrorReturned()
        {

            SignUp signUp = new SignUp();
            string login = signUp.GetLogin();
            string password = signUp.GetPassword();
            string passwordRepetition = signUp.GetPasswordRepetition();
            string firstname = signUp.GetFirstName();
            string surname = signUp.GetSurName();
            string middlename = signUp.GetMiddleName();
            login = "";
            password = "pa$$Word123";
            firstname = "Воскан";
            surname = "Надежда";
            middlename = "Николаевна";
            string loginCheckAssert = signUp.CheckSignUp(firstname, surname, middlename, login, password, passwordRepetition);

            Assert.AreEqual(loginCheckAssert, "Пожалуйста, заполните все поля. Поле \"Отчество\" является не обязательным.");
        }
    }
}
