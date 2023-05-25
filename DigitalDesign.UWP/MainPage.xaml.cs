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
				string text = await FileIO.ReadTextAsync(file);
				previewTextBlock.Text = text;
				fileNameTextBlock.Text = file.DisplayName + file.FileType;
				saveButton.IsEnabled = true;
				MethodInfo method = typeof(ConvertString).GetMethod("ToDictionary", BindingFlags.NonPublic | BindingFlags.Instance);
				var convertString = new ConvertString();
				Dictionary<string, int>  keyValuePairs = method.Invoke(convertString, new object[] { text }) as Dictionary<string, int>;
				var result = keyValuePairs.Select(kvp => kvp.ToString());
				listOfNameTextBlock.Text = string.Join(", ", result);
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
