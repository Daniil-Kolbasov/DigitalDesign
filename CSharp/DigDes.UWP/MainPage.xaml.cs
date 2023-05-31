using System;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using DigDes.UWP.Lib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DigDes.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

				ClassWithPtivateMethod classWithPtivateMethod = new ClassWithPtivateMethod();
				var dict1 = classWithPtivateMethod.InvokeMethod("GetDictionary", text) as Dictionary<string, int> ?? new Dictionary<string, int>();

				string result = string.Empty;
				foreach (var item in dict1)
				{
					result += $"{item.Key} - {item.Value}\n";
				}

				saveButton.IsEnabled = true;
				previewTextBlock.Text = text;
				fileNameTextBlock.Text = file.DisplayName + file.FileType;
				listOfNameTextBlock.Text = result;

				var dict = await ProcessesAndThreed.GetTimeOfProcessingMethod(text);

				regularTextBlock.Text = dict["Regular"] + " ms";
				taskTextBlock.Text = dict["Task"] + " ms";
				threadTextBlock.Text = dict["Thread"] + " ms";
				parallelTextBlock.Text = dict["Parallel"] + " ms";
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
