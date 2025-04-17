using Module2.Classes;
using Module2.Models;
using System.Text.RegularExpressions;
using System.Windows;

namespace Module2.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPartner.xaml
    /// </summary>
    public partial class AddPartner : Window
    {
        private readonly List<PartnerType> _partnerTypes = new List<PartnerType>
        {
            new PartnerType{ IdType = 1, NameType = "ЗАО"},
            new PartnerType{ IdType = 2, NameType = "ПАО"},
            new PartnerType{ IdType = 3, NameType = "ООО"},
            new PartnerType{ IdType = 4, NameType = "ОАО"},
            new PartnerType{ IdType = 4, NameType = "ИП"}
        };

        private BochagovaDemExamContext _context;
        private Partner _partner;
        private Partner _partnerEdit;
        public AddPartner()
        {
            InitializeComponent();
            _context = new BochagovaDemExamContext();

            cbType.ItemsSource = _partnerTypes;
            cbType.DisplayMemberPath = "NameType";

            Title = "Добавление данных о партнере";
        }
        public AddPartner(Partner partner) : this()
        {
            _partner = partner;
            _partnerEdit = _context.Partners.FirstOrDefault(a => a.IdPartner == _partner.IdPartner);

            if (_partnerEdit != null)
            {
                cbType.Text = _partnerEdit.TypePartner;
                tbName.Text = _partnerEdit.NamePartner;
                tbLastNameDirector.Text = _partnerEdit.LastNameDirectorPartner;
                tbFirstNameDirector.Text = _partnerEdit.FirstNameDirectorPartner;
                tbPatronymicDirector.Text = _partnerEdit?.PatronymicDirectorPartner;
                tbEmail.Text = _partnerEdit?.EmailPartner;
                tbPhone.Text = _partnerEdit?.PhonePartner;
                tbIndex.Text = _partnerEdit?.IndexPartner.ToString();
                tbRegion.Text = _partnerEdit?.RegionPartner;
                tbCity.Text = _partnerEdit?.CityPartner;
                tbStreet.Text = _partnerEdit?.StreetPartner;
                tbHouse.Text = _partnerEdit?.HousePartner;
                tbINN.Text = _partnerEdit?.InnPartner.ToString();
                tbRating.Text = _partnerEdit?.RatingPartner.ToString();
            }

            Title = "Изменение данных о партнере";
        }

        private bool AreRequiredFieldsFilled()
        {
            if (cbType.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Тип партнера' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Поле 'Наименование' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbName.Text, @"\d"))
            {
                MessageBox.Show("В поле 'Наименование' запрещено вводить цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbLastNameDirector.Text))
            {
                MessageBox.Show("Поле 'Фамилия директора' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbLastNameDirector.Text, @"\d"))
            {
                MessageBox.Show("В поле 'Фамилия директора' запрещено вводить цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbFirstNameDirector.Text))
            {
                MessageBox.Show("Поле 'Имя директора' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbFirstNameDirector.Text, @"\d"))
            {
                MessageBox.Show("В поле 'Имя директора' запрещено вводить цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            Regex email = new Regex(@"^[a-zA-Z0-9.?_]+@[a-zA-Z._-]+\.[a-zA-Z]{2,}");
            if (!email.IsMatch(tbEmail.Text))
            {
                MessageBox.Show("Вводите почту в корректном формате. Примеры: zzz@Az.ru, 12ru@zr.com", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                MessageBox.Show("Поле 'Почта' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                MessageBox.Show("Поле 'Телефон' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbPhone.Text, @"\D"))
            {
                MessageBox.Show("В поле 'Имя директора' запрещено вводить что-то, кроме цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            Regex pattern = new Regex(@"^\d{3} \d{3} \d{2} \d{2}$");
            // Проверка на наличие +7 в начале строки и совпадение формата
            if (tbPhone.Text.StartsWith("+7") || !Regex.IsMatch(tbPhone.Text, @"^\d{3} \d{3} \d{2} \d{2}$"))
            {
                MessageBox.Show("Вводите номер в формате: *** *** ** ** без +7.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbIndex.Text))
            {
                MessageBox.Show("Поле 'Индекс' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (int.Parse(tbIndex.Text) < 100000 || int.Parse(tbIndex.Text) > 999999 || !int.TryParse(tbIndex.Text, out int value))
            {
                MessageBox.Show("Поле 'Индекс' должно содержать 6-ти значное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbIndex.Text, @"\D"))
            {
                MessageBox.Show("В поле 'Индекс' запрещено вводить что-то, кроме цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbRegion.Text))
            {
                MessageBox.Show("Поле 'Область' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbRegion.Text, @"\d"))
            {
                MessageBox.Show("В поле 'Область' запрещено вводить цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbCity.Text))
            {
                MessageBox.Show("Поле 'Город' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbCity.Text, @"\d"))
            {
                MessageBox.Show("В поле 'Город' запрещено вводить цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbStreet.Text))
            {
                MessageBox.Show("Поле 'Улица' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbHouse.Text))
            {
                MessageBox.Show("Поле 'Дом' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbINN.Text))
            {
                MessageBox.Show("Поле 'ИНН' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbINN.Text, @"\D"))
            {
                MessageBox.Show("В поле 'ИНН' запрещено вводить что-то, кроме цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (long.Parse(tbINN.Text) < 100000000000 || long.Parse(tbINN.Text) > 999999999999)
            {
                MessageBox.Show("Поле 'ИНН' должно содержать от 10-ти до 12-ти цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(tbRating.Text))
            {
                MessageBox.Show("Поле 'Рейтинг' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Regex.IsMatch(tbRating.Text, @"\D"))
            {
                MessageBox.Show("В поле 'Рейтинг' запрещено вводить что-то, кроме цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (int.Parse(tbRating.Text) < 1 || int.Parse(tbRating.Text) > 10)
            {
                MessageBox.Show("Поле 'Рейтинг' должно содержать значение от 1 до 10.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                return;
            }
            if (_partnerEdit == null)
            {
                _partnerEdit = new Partner();
                _partnerEdit.TypePartner = cbType.Text;
                _partnerEdit.NamePartner = tbName.Text;
                _partnerEdit.LastNameDirectorPartner = tbLastNameDirector.Text;
                _partnerEdit.FirstNameDirectorPartner = tbFirstNameDirector.Text;
                _partnerEdit.PatronymicDirectorPartner = tbPatronymicDirector.Text;
                _partnerEdit.EmailPartner = tbEmail.Text;
                _partnerEdit.PhonePartner = tbPhone.Text;
                _partnerEdit.IndexPartner = int.Parse(tbIndex.Text);
                _partnerEdit.RegionPartner = tbRegion.Text;
                _partnerEdit.CityPartner = tbCity.Text;
                _partnerEdit.StreetPartner = tbStreet.Text;
                _partnerEdit.HousePartner = tbHouse.Text;
                _partnerEdit.InnPartner = long.Parse(tbINN.Text);
                _partnerEdit.RatingPartner = int.Parse(tbRating.Text);

                _context.Partners.Add(_partnerEdit);
                MessageBox.Show("Партнер успешно сохранен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                _partnerEdit.TypePartner = cbType.Text;
                _partnerEdit.NamePartner = tbName.Text;
                _partnerEdit.LastNameDirectorPartner = tbLastNameDirector.Text;
                _partnerEdit.FirstNameDirectorPartner = tbFirstNameDirector.Text;
                _partnerEdit.PatronymicDirectorPartner = tbPatronymicDirector.Text;
                _partnerEdit.EmailPartner = tbEmail.Text;
                _partnerEdit.PhonePartner = tbPhone.Text;
                _partnerEdit.IndexPartner = int.Parse(tbIndex.Text);
                _partnerEdit.RegionPartner = tbRegion.Text;
                _partnerEdit.CityPartner = tbCity.Text;
                _partnerEdit.StreetPartner = tbStreet.Text;
                _partnerEdit.HousePartner = tbHouse.Text;
                _partnerEdit.InnPartner = long.Parse(tbINN.Text);
                _partnerEdit.RatingPartner = int.Parse(tbRating.Text);

                _context.Partners.Update(_partnerEdit);
                MessageBox.Show("Партнер успешно изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            _context.SaveChanges();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
