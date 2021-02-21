using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace GujaratiAlphabet
{
	public static class EmbeddedResource
	{
		private static readonly Assembly _executingAssembly;

		static EmbeddedResource()
		{
			_executingAssembly = Assembly.GetExecutingAssembly();
		}

		public static Stream GetLetterSound(string letter)
		{
			const string folderPath = "Wavs.Letters";
			return GetSoundStream(folderPath, letter);
		}

		public static Stream GetExampleSound(string letter)
		{
			const string folderPath = "Wavs.Examples";
			return GetSoundStream(folderPath, letter);
		}

		public static Stream GetLetterWithExampleSound(string letter)
		{
			const string folderPath = "Wavs.LetterWithExamples";
			return GetSoundStream(folderPath, letter);
		}

		public static Bitmap GetButtonCaptionImage(string letter)
		{
			const string folderPath = "Pictures.ButtonCaptions";
			return GetImageStream(folderPath, letter);
		}

		public static Bitmap GetLetterImage(string letter)
		{
			const string folderPath = "Pictures.Letters";
			return GetImageStream(folderPath, letter);
		}

		public static Bitmap GetExampleImage(string letter)
		{
			const string folderPath = "Pictures.Examples";
			return GetImageStream(folderPath, letter);
		}

		public static Bitmap GetLetterWithExampleImage(string letter)
		{
			const string folderPath = "Pictures.LetterWithExamples";
			return GetImageStream(folderPath, letter);
		}

		private static Bitmap GetImageStream(string folderPath, string letter)
		{
			return new Bitmap(GetResourceStream(folderPath, letter, "jpg"));
		}

		private static Stream GetSoundStream(string folderPath, string letter)
		{
			return GetResourceStream(folderPath, letter, "wav");
		}

		private static Stream GetResourceStream(string folderPath, string letter, string fileExtention)
		{
			var assemblyName = _executingAssembly.GetName().Name;
			var resourceUri = string.Format("{0}.{1}.{2}.{3}", assemblyName, folderPath, letter, fileExtention);
			var resourceStream = _executingAssembly.GetManifestResourceStream(resourceUri);
			if (resourceStream == null)
				throw new Exception(string.Format("Requested resource for letter {0} is not Found. Searched at location {1}. Make sure you have embedded Resource at right path (Remember name are case sensitive including file extensions).", letter, folderPath));
			return resourceStream;
		}
	}
}