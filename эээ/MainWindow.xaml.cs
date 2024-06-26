using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace эээ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        int[] mas;
        int Count;

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            if (Range1.Text != "" && Range2.Text != "")
            {
                int randMin, randMax;
                if (int.TryParse(Range1.Text, out randMin) && int.TryParse(Range2.Text, out randMax))
                {
                    if (randMin <= randMax)
                    {
                        if (randMin == 0 && (randMax == 0 || randMax == 1))
                        {
                            MessageBox.Show("Диапазон должен быть больше или меньше нуля", "Ошибка");
                        }
                        else
                        {
                            Rez.Clear();
                            mas = new int[Count];
                            Random rnd = new Random();
                            for (int i = 0; i < mas.Length; i++)
                            {
                                mas[i] = rnd.Next(randMin, randMax);
                            }
                            DataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Минимальное значение не может быть больше максимального", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Диапазон значений задается числами", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Задайте диапазон значений", "Ошибка");
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Column_Count.Text != "")
            {
                if (int.TryParse(Column_Count.Text, out Count))
                {
                    if (Count > 0)
                    {
                        mas = new int[Count];
                        DataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Размер должен быть больше 0", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Размер таблицы задается числом", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Задайте размер таблицы", "Ошибка");
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.ItemsSource != null)
            {
                int sum = 0;
                for (int i = 0; i < mas.Length; i++)
                {
                    sum -= mas[i];
                }
                Rez.Text = Convert.ToString(sum);
            }
            else
            {
                MessageBox.Show("Таблица пустая", "Ошибка");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            mas = null;
            Range1.Clear();
            Range2.Clear();
            Rez.Clear();
            Column_Count.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создатель: Журавлев\nВариант: 7\nЗадача: Ввести n целых чисел(>0 или <0). Найти разницу чисел. Результат вывести на экран.", "Справка");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* |Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (mas != null)
            {
                if (save.ShowDialog() == true)
                {
                    StreamWriter file = new StreamWriter(save.FileName);
                    file.WriteLine(mas.Length);
                    for (int i = 0; i < mas.Length; i++)
                    {
                        file.WriteLine(mas[i]);
                    }
                    file.Close();
                }
            }
            else
            {
                if (save.ShowDialog() == true)
                {
                    StreamWriter file = new StreamWriter(save.FileName);
                    file.Close();
                }
            }

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* |Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Сохранение таблицы";

            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName);
                int length = Convert.ToInt32(file.ReadLine());
                mas = new Int32[length];
                for (int i = 0; i < mas.Length; i++)
                {
                    mas[i] = Convert.ToInt32(file.ReadLine());
                }
                file.Close();
                DataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
            }
        }
    }
}