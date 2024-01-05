using MVP_HW_130923.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace MVP_HW_130923
{
	public partial class MainWindow : Window
	{
		private ObservableCollection<Person> people = new ObservableCollection<Person>();

		public MainWindow()
		{
			InitializeComponent();
			lbList.ItemsSource = people;
			LoadData();
		}

		private void bSave_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbAge.Text)) {
				MessageBox.Show("Имя и возраст не должны быть пустыми.");
				return;
			}

			if (!int.TryParse(tbAge.Text, out int age)) {
				MessageBox.Show("Возраст должен быть числом.");
				return;
			}

			var person = new Person()
			{
				Name = tbName.Text,
				Age = age
			};
			people.Add(person);
		}

		private void UpdateListBox()
		{
			lbList.Items.Clear();
			foreach (var person in people) {
				lbList.Items.Add($"{person.Name}, {person.Age}");
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveData();
		}

		private void SaveData()
		{
			var serializer = new XmlSerializer(typeof(List<Person>));
			using (var writer = new StreamWriter("data.xml")) {
				serializer.Serialize(writer, people);
			}
		}

		private void LoadData()
		{
			if (File.Exists("data.xml")) {
				var serializer = new XmlSerializer(typeof(List<Person>));
				using (var reader = new StreamReader("data.xml")) {
					var deserializedList = (List<Person>)serializer.Deserialize(reader);

					people.Clear();

					foreach (var person in deserializedList) {
						people.Add(person);
					}
				}
			}
		}

		private void tbAge_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;

			if (!int.TryParse(textBox.Text, out _)) {
				string newText = "";
				foreach (char c in textBox.Text) {
					if (char.IsDigit(c)) {
						newText += c;
					}
				}

				textBox.Text = newText;

				textBox.CaretIndex = textBox.Text.Length;
			}
		}

		private void bShow_All_Click(object sender, RoutedEventArgs e)
		{
			lbList.ItemsSource = people;
		}
		private void bSearch_Click(object sender, RoutedEventArgs e)
		{
			var searchText = tbSearch.Text.ToLower();
			var filteredPeople = new ObservableCollection<Person>(people.Where(person => person.Name.ToLower().Contains(searchText)));

			lbList.ItemsSource = filteredPeople;
		}

	}
}
