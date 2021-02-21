using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace GujaratiAlphabet
{
	public partial class MainForm : Form
	{
		private readonly SoundPlayer _player;
		private Mode _currentMode;

		public MainForm()
		{
			InitializeComponent();
			_currentMode = Mode.Letter;
			AdjustAvailableModeOption(toolStripLetterOnlyButton);

			btn0A95.Click += ((s, e) => handleClick(s));
			btn0A96.Click += ((s, e) => handleClick(s));
			btn0A97.Click += ((s, e) => handleClick(s));
			btn0A98.Click += ((s, e) => handleClick(s));
			btn0A9A.Click += ((s, e) => handleClick(s));
			btn0A9B.Click += ((s, e) => handleClick(s));
			btn0A9C.Click += ((s, e) => handleClick(s));
			btn0A9D.Click += ((s, e) => handleClick(s));
			btn0A9F.Click += ((s, e) => handleClick(s));
			btn0AA0.Click += ((s, e) => handleClick(s));
			btn0AA1.Click += ((s, e) => handleClick(s));
			btn0AA2.Click += ((s, e) => handleClick(s));
			btn0AA3.Click += ((s, e) => handleClick(s));
			btn0AA4.Click += ((s, e) => handleClick(s));
			btn0AA5.Click += ((s, e) => handleClick(s));
			btn0AA6.Click += ((s, e) => handleClick(s));
			btn0AA7.Click += ((s, e) => handleClick(s));
			btn0AA8.Click += ((s, e) => handleClick(s));
			btn0AAA.Click += ((s, e) => handleClick(s));
			btn0AAB.Click += ((s, e) => handleClick(s));
			btn0AAC.Click += ((s, e) => handleClick(s));
			btn0AAD.Click += ((s, e) => handleClick(s));
			btn0AAE.Click += ((s, e) => handleClick(s));
			btn0AAF.Click += ((s, e) => handleClick(s));
			btn0AB0.Click += ((s, e) => handleClick(s));
			btn0AB2.Click += ((s, e) => handleClick(s));
			btn0AB3.Click += ((s, e) => handleClick(s));
			btn0AB5.Click += ((s, e) => handleClick(s));
			btn0AB6.Click += ((s, e) => handleClick(s));
			btn0AB7.Click += ((s, e) => handleClick(s));
			btn0AB8.Click += ((s, e) => handleClick(s));
			btn0AB9.Click += ((s, e) => handleClick(s));
			btnXa.Click += ((s, e) => handleClick(s));
			btnGya.Click += ((s, e) => handleClick(s));

			var xaStream = EmbeddedResource.GetButtonCaptionImage("Xa");
			var gyaStream = EmbeddedResource.GetButtonCaptionImage("Gya");

			btnXa.Image = new Bitmap(xaStream);
			btnXa.ImageAlign = ContentAlignment.BottomCenter;
			btnGya.Image = new Bitmap(gyaStream);
			btnGya.ImageAlign = ContentAlignment.BottomCenter;

			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			_player = new SoundPlayer();
		}

		private void handleClick(object sender)
		{
			var wordPressed = ((Button) sender).Name.Substring(3);
			performAction(wordPressed);
		}

		private void performAction(string wordPressed)
		{
			Stream soundStream = null;
			Bitmap letterImage = null;
			
			switch (_currentMode)
			{
				case Mode.LetterWithExample:
					soundStream = EmbeddedResource.GetLetterWithExampleSound(wordPressed);
					letterImage = EmbeddedResource.GetLetterWithExampleImage(wordPressed);
					break;
				case Mode.Letter:
					soundStream = EmbeddedResource.GetLetterSound(wordPressed);
					letterImage = EmbeddedResource.GetLetterImage(wordPressed);
					break;
				case Mode.Example:
					soundStream = EmbeddedResource.GetExampleSound(wordPressed);
					letterImage = EmbeddedResource.GetExampleImage(wordPressed);
					break;
			}
			_player.Stream = soundStream;
			_player.Play();
			pictureBox1.Image = letterImage;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		public void ThreadJob()
		{
			var letters = new[] {"0A95","0A96","0A97","0A98","0A9A","0A9B","0A9C","0A9D","0A9F",
				"0AA0","0AA1","0AA2","0AA3","0AA4","0AA5","0AA6","0AA7","0AA8","0AAA","0AAB",
				"0AAC","0AAD","0AAE","0AAF","0AB0","0AB2","0AB5","0AB6","0AB7","0AB8","0AB9","0AB3","Xa","Gya"};

			foreach (var letter in letters)
			{
				SimmulateButtonPress(letter);

				switch (_currentMode)
				{
					case Mode.Letter:
						Thread.Sleep(2*1000);
						break;
					case Mode.Example:
						Thread.Sleep(4*1000);
						break;
					case Mode.LetterWithExample:
						Thread.Sleep(6*1000);
						break;
				}
			}

			BeginInvoke(new BooleanParameterDelegate(AdjustAvailablityForAllLetterButton), new object[] {true});
		}

		private void SimmulateButtonPress(string code)
		{
			if (InvokeRequired)
			{
				// We're not in the UI thread, so we need to call BeginInvoke
				BeginInvoke(new StringParameterDelegate(SimmulateButtonPress), new object[] {code});
				return;
			}
			// Must be on the UI thread if we've got this far
			performAction(code);
		}


		private void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
//			bool Alt = (ModifierKeys & Keys.Alt) != 0;

			if (e.KeyCode == Keys.K)
				performAction(Letter.ક);

			e.Handled = true;
		}

		private void toolStripLetterOnlyButton_Click(object sender, EventArgs e)
		{
			_currentMode = Mode.Letter;
			AdjustAvailableModeOption(toolStripLetterOnlyButton);
		}

		private void toolStripButtonWithExampleButton_Click(object sender, EventArgs e)
		{
			_currentMode = Mode.LetterWithExample;
			AdjustAvailableModeOption(toolStripButtonWithExampleButton);
		}

		private void toolStripExampleOnlyButton_Click(object sender, EventArgs e)
		{
			_currentMode = Mode.Example;
			AdjustAvailableModeOption(toolStripExampleOnlyButton);
		}

		private void AdjustAvailableModeOption(ToolStripButton buttonPressed)
		{
			toolStripLetterOnlyButton.Enabled = true;
			toolStripButtonWithExampleButton.Enabled = true;
			toolStripExampleOnlyButton.Enabled = true;
			buttonPressed.Enabled = false;
		}

		private void btnLoop_Click(object sender, EventArgs e)
		{
			AdjustAvailablityForAllLetterButton(false);

			var backgroundThread = new Thread(ThreadJob) {IsBackground = true};
			backgroundThread.Start();
		}

		private void AdjustAvailablityForAllLetterButton(bool isAvailable)
		{
			foreach (var control in Controls)
			{
				if (control is Button)
				{
					((Button)control).Enabled = isAvailable;
				}
			}
			btnClose.Enabled = true;
		}

		private delegate void StringParameterDelegate(string code);
		private delegate void BooleanParameterDelegate(bool flag);
	}
}