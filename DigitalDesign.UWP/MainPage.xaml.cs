using System;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Reflection;
using DigitalDesign.UWPLib;
using System.Collections.Generic;
using System.Linq;

namespace DigitalDesign.UWP
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		private async void openButton_Click(object sender, RoutedEventArgs e)
		{
			FileOpenPicker openPicker = new FileOpenPicker();
			openPicker.ViewMode = PickerViewMode.Thumbnail;
			openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			openPicker.CommitButtonText = "Open";
			openPicker.FileTypeFilter.Add(".txt");
			var file = await openPicker.PickSingleFileAsync();

			if (file != null)
			{
				var text = await FileIO.ReadTextAsync(file);

				var exampleType = typeof(WorkWithString);
				MethodInfo method = exampleType.GetMethod("GetDictionary", BindingFlags.NonPublic | BindingFlags.Static);
				var dict = method.Invoke(null, new object[] { text }) as Dictionary<string, int>;
				string result = string.Empty;
				foreach (var item in dict)
				{
					result += $"{item.Key} - {item.Value}\n";
				}

				saveButton.IsEnabled = true;
				previewTextBlock.Text = text;
				fileNameTextBlock.Text = file.DisplayName + file.FileType;
				listOfNameTextBlock.Text = result;

				await WorkWithString.GetDictionaryAsync(text);
				WorkWithString.GetDictionaryThread(text);
				WorkWithString.GetDictionaryParallel(text);

				regularTextBlock.Text = WorkWithString.RegularTime.ToString() + " ms";
				taskTextBlock.Text = WorkWithString.TaskAsyncTime.ToString() + " ms";
				threadTextBlock.Text = WorkWithString.ThreadTime.ToString() + " ms";
				parallelTextBlock.Text = WorkWithString.ParallelTime.ToString() + " ms";
			}
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			var savePicker = new FileSavePicker();
			savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
			savePicker.SuggestedFileName = "New Document";
			savePicker.CommitButtonText = "Save";
			var newFile = await savePicker.PickSaveFileAsync();

			if (newFile != null)
			{
				await FileIO.WriteTextAsync(newFile, listOfNameTextBlock.Text);
			}
		}
	}
}
