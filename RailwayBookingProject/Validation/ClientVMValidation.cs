using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayBookingProject.Validation
{
    public class ClientVMValidation : INotifyPropertyChanged
    {
        private readonly ClientValidation validation;

        public ClientVMValidation()
        {
            validation = new ClientValidation();
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                validation.FirstName = value; // Обновляем значение в валидаторе
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                validation.LastName = value; // Обновляем значение в валидаторе
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                validation.Email = value; // Обновляем значение в валидаторе
                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                validation.Password = value; // Обновляем значение в валидаторе
                OnPropertyChanged(nameof(Password));
            }
        }

        // Метод для проверки валидности данных
        public bool IsValid()
        {
            return string.IsNullOrEmpty(validation[nameof(FirstName)]) &&
                   string.IsNullOrEmpty(validation[nameof(LastName)]) &&
                   string.IsNullOrEmpty(validation[nameof(Email)]) &&
                   string.IsNullOrEmpty(validation[nameof(Password)]);
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
