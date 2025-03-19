using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBookingProject.Validation
{
    public class ClientValidation : IDataErrorInfo
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Реализация IDataErrorInfo
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                            error = "Имя обязательно для заполнения.";
                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                            error = "Фамилия обязательна для заполнения.";
                        break;

                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                            error = "Email обязателен для заполнения.";
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            error = "Некорректный формат email.";
                        break;

                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                            error = "Пароль обязателен для заполнения.";
                        else if (Password.Length < 6)
                            error = "Пароль должен содержать не менее 6 символов.";
                        break;
                }

                return error;
            }
        }
    }
}
