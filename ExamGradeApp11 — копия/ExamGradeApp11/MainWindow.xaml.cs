using System;
using System.Windows;

namespace ExamGradeApp11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateModulesVisibility();

            rbBU.Checked += (s, e) => UpdateModulesVisibility();
            rbPU.Checked += (s, e) => UpdateModulesVisibility();
            rbPUPlus.Checked += (s, e) => UpdateModulesVisibility();
        }

        private void UpdateModulesVisibility()
        {
            bool isPU = rbPU.IsChecked == true;
            bool isPUPlus = rbPUPlus.IsChecked == true;

            txtModule4.IsEnabled = isPU || isPUPlus;
            txtModule5.IsEnabled = isPUPlus;

            if (!isPU && !isPUPlus)
            {
                txtModule4.Text = "0";
                txtModule5.Text = "0";
            }
            if (!isPUPlus)
            {
                txtModule5.Text = "0";
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string level = "";
                if (rbBU.IsChecked == true) level = "БУ";
                else if (rbPU.IsChecked == true) level = "ПУ";
                else if (rbPUPlus.IsChecked == true) level = "ПУ+";

                if (!int.TryParse(txtModule1.Text, out int s1))
                    throw new FormatException("Модуль 1: введите целое число");
                if (!int.TryParse(txtModule2.Text, out int s2))
                    throw new FormatException("Модуль 2: введите целое число");
                if (!int.TryParse(txtModule3.Text, out int s3))
                    throw new FormatException("Модуль 3: введите целое число");

                int s4 = 0, s5 = 0;
                if (txtModule4.IsEnabled && !int.TryParse(txtModule4.Text, out s4))
                    throw new FormatException("Модуль 4: введите целое число");
                if (txtModule5.IsEnabled && !int.TryParse(txtModule5.Text, out s5))
                    throw new FormatException("Модуль 5: введите целое число");

                var (total, grade) = ExamCalculator.Calculate(s1, s2, s3, s4, s5, level);

                tbTotalScore.Text = $"Сумма баллов: {total}";
                tbGrade.Text = $"Оценка: {grade}";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка в данных: {ex.Message}", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Неверный формат числа: {ex.Message}", "Ошибка ввода",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}