using RailwayBookingProject.Command;
using RailwayBookingProject.Model;
using RailwayBookingProject.Validation;
using RailwayBookingProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace RailwayBookingProject.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        private HttpClient httpClient;
        private RelayCommand addCommand;
        private RelayCommand deleteCommand;
        ////private RelayCommand updateCommand;
        ////private RelayCommand deleteAllCommand;
        ////private RelayCommand updateAllCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(async obj =>
                {
                    ClientWindow cw = new ClientWindow(new Model.Клиент());
                    if (cw.ShowDialog() == true)
                    {
                        await SendClient(cw.Клиент);
                    }
                }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(async (selectedItem) =>
                {
                    Model.Клиент? client = selectedItem as Model.Клиент;
                    if (client == null) return;
                    if (MessageBox.Show("Вы действительно хотите удалить элемент?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        await RemoveClient(client);
                    }
                }));
            }
        }

        private async Task RemoveClient(Model.Клиент client)
        {
            JsonContent content = JsonContent.Create(client);
            using var request = new HttpRequestMessage(HttpMethod.Delete, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            if (responseText == "Yes") Load();
        }

        private async Task SendClient(Model.Клиент client)
        {
            try
            {
                JsonContent content = JsonContent.Create(client);
                content.Headers.Add("table", "client");
                using var response = await httpClient.PostAsync("http://127.0.0.1:8888/connection/", content);
                Load();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private ObservableCollection<Model.Клиент>? clients;
        public ObservableCollection<Model.Клиент>? Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        private Model.Клиент? selectedClient;

        public Model.Клиент? SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }
        public ClientViewModel()
        {
            httpClient = new HttpClient();
            Load();
        }
        private void Load()
        {
            Clients = null;
            Task<ObservableCollection<Model.Клиент>> task = Task.Run(() => getClients());
            Clients = task.Result;
        }

        private async Task<ObservableCollection<Model.Клиент>> getClients()
        {
            StringContent content = new StringContent("getClients");
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:8888/connection/");
            request.Headers.Add("table", "client");
            request.Content = content;
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            List<Model.Клиент> clients = JsonSerializer.Deserialize<List<Model.Клиент>>(responseText)!;
            return new ObservableCollection<Model.Клиент>(clients);
        }

        internal bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

}
    





    //public class ClientViewModel : INotifyPropertyChanged
    //{
    //    private readonly ClientValidation _validation;

    //    public ClientViewModel()
    //    {
    //        _validation = new ClientValidation();
    //    }

    //    private string _firstName;
    //    public string FirstName
    //    {
    //        get => _firstName;
    //        set
    //        {
    //            _firstName = value;
    //            _validation.FirstName = value; // Обновляем значение в валидаторе
    //            OnPropertyChanged(nameof(FirstName));
    //        }
    //    }

    //    private string _lastName;
    //    public string LastName
    //    {
    //        get => _lastName;
    //        set
    //        {
    //            _lastName = value;
    //            _validation.LastName = value; // Обновляем значение в валидаторе
    //            OnPropertyChanged(nameof(LastName));
    //        }
    //    }

    //    private string _email;
    //    public string Email
    //    {
    //        get => _email;
    //        set
    //        {
    //            _email = value;
    //            _validation.Email = value; // Обновляем значение в валидаторе
    //            OnPropertyChanged(nameof(Email));
    //        }
    //    }

    //    private string _password;
    //    public string Password
    //    {
    //        get => _password;
    //        set
    //        {
    //            _password = value;
    //            _validation.Password = value; // Обновляем значение в валидаторе
    //            OnPropertyChanged(nameof(Password));
    //        }
    //    }

    //    // Метод для проверки валидности данных
    //    public bool IsValid()
    //    {
    //        return string.IsNullOrEmpty(_validation[nameof(FirstName)]) &&
    //               string.IsNullOrEmpty(_validation[nameof(LastName)]) &&
    //               string.IsNullOrEmpty(_validation[nameof(Email)]) &&
    //               string.IsNullOrEmpty(_validation[nameof(Password)]);
    //    }

    //    // Реализация INotifyPropertyChanged
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void OnPropertyChanged(string propertyName)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}

    //public class ClientViewModel : INotifyPropertyChanged, IDataErrorInfo
    //{
//        private string? firstName;
//        private string? lastName;
//        private string? email;
//        private string? password;

//        public string FirstName
//        {
//            get => firstName;
//            set
//            {
//                firstName = value;
//                OnPropertyChanged(nameof(FirstName));
//            }
//        }

//        public string LastName
//        {
//            get => lastName;
//            set
//            {
//                lastName = value;
//                OnPropertyChanged(nameof(LastName));
//            }
//        }

//        public string Email
//        {
//            get => email;
//            set
//            {
//                email = value;
//                OnPropertyChanged(nameof(Email));
//            }
//        }

//        public string Password
//        {
//            get => password;
//            set
//            {
//                password = value;
//                OnPropertyChanged(nameof(Password));
//            }
//        }

//        //// Реализация IDataErrorInfo
//        //public string Error => null;
//        public string Error => throw new NotImplementedException();



//        public string this[string columnName]
//        {
//            get
//            {
//                string error = null;

//                switch (columnName)
//                {
//                    case nameof(FirstName):
//                        if (string.IsNullOrWhiteSpace(FirstName))
//                            error = "Имя обязательно для заполнения.";
//                        break;

//                    case nameof(LastName):
//                        if (string.IsNullOrWhiteSpace(LastName))
//                            error = "Фамилия обязательна для заполнения.";
//                        break;

//                    case nameof(Email):
//                        if (string.IsNullOrWhiteSpace(Email))
//                            error = "Email обязателен для заполнения.";
//                        else if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//                            error = "Некорректный формат email.";
//                        break;

//                    case nameof(Password):
//                        if (string.IsNullOrWhiteSpace(Password))
//                            error = "Пароль обязателен для заполнения.";
//                        else if (Password.Length < 6)
//                            error = "Пароль должен содержать не менее 6 символов.";
//                        break;
//                }

//                return error;
//            }
//        }

//        // Реализация INotifyPropertyChanged
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}

