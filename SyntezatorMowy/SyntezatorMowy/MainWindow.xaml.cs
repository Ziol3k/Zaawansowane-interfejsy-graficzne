using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;

using UglyToad.PdfPig; // Założenie, że chcesz rozszerzyć o pdf

namespace SyntezatorMowy
{
    public partial class MainWindow : Window
    {
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Dictionary<string, VoiceInfo> availableVoices = new Dictionary<string, VoiceInfo>();

        private List<MemoryStream> audioSegments = new List<MemoryStream>();
        private List<string> tempFiles = new List<string>();

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private int currentSegmentIndex = 0;
        private bool isPaused = false;

        private List<(string voice, string text)> textSegments = new List<(string voice, string text)>();
        private string defaultVoice = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadVoices();

            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void LoadVoices()
        {
            VoiceComboBox.Items.Clear();
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                var name = voice.VoiceInfo.Name;
                availableVoices[name] = voice.VoiceInfo;
                VoiceComboBox.Items.Add(name);
            }

            if (VoiceComboBox.Items.Count > 0)
                VoiceComboBox.SelectedIndex = 0;
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string extension = System.IO.Path.GetExtension(dlg.FileName).ToLower();
                if (extension == ".pdf")
                {
                    InputTextBox.Text = ExtractTextFromPdf(dlg.FileName);
                }
                else
                {
                    InputTextBox.Text = File.ReadAllText(dlg.FileName);
                }
            }
        }

        private string ExtractTextFromPdf(string filePath)
        {
            try
            {
                using var document = UglyToad.PdfPig.PdfDocument.Open(filePath);
                var text = "";
                foreach (var page in document.GetPages())
                {
                    text += page.Text + "\n";
                }
                return text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas odczytu pliku PDF: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
        }

        private async void StartFromBeginning_Click(object sender, RoutedEventArgs e)
        {
            StopPlaybackAndCleanup();

            isPaused = false;
            currentSegmentIndex = 0;
            audioSegments.Clear();

            string inputText = InputTextBox.Text;
            defaultVoice = VoiceComboBox.SelectedItem?.ToString() ?? "";

            textSegments = ParseTextWithVoices(inputText, defaultVoice);

            await GenerateAudioSegmentsAsync();

            if (audioSegments.Count > 0)
            {
                PlaySegment(currentSegmentIndex);
            }
        }

        private async Task GenerateAudioSegmentsAsync()
        {
            ClearTempFiles();

            audioSegments.Clear();

            foreach (var segment in textSegments)
            {
                if (!availableVoices.ContainsKey(segment.voice))
                {
                    MessageBox.Show($"Głos '{segment.voice}' nie istnieje. Pomijam fragment.", "Błąd głosu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    continue;
                }

                var ms = new MemoryStream();
                synthesizer.SelectVoice(segment.voice);

                synthesizer.SetOutputToWaveStream(ms);
                synthesizer.Speak(segment.text);
                synthesizer.SetOutputToDefaultAudioDevice();

                ms.Position = 0;
                audioSegments.Add(ms);

                await Task.Delay(50);
            }
        }

        private void PlaySegment(int index)
        {
            if (index < 0 || index >= audioSegments.Count) return;

            // Zwolnij poprzedni plik tymczasowy
            ClearTempFiles();

            var ms = audioSegments[index];

            // Zapisz do pliku tymczasowego
            string tempFile = Path.Combine(Path.GetTempPath(), $"tts_segment_{index}_{Guid.NewGuid()}.wav");
            using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                ms.Position = 0;
                ms.CopyTo(fileStream);
            }
            tempFiles.Add(tempFile);

            mediaPlayer.Open(new Uri(tempFile, UriKind.Absolute));
            mediaPlayer.Play();

            UpdateProgress();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            currentSegmentIndex++;
            if (currentSegmentIndex < audioSegments.Count)
            {
                PlaySegment(currentSegmentIndex);
            }
            else
            {
                currentSegmentIndex = 0;
                isPaused = false;
                PlaybackProgress.Value = 100;
                ClearTempFiles();
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.CanPause)
            {
                mediaPlayer.Pause();
                isPaused = true;
            }
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                mediaPlayer.Play();
                isPaused = false;
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StopPlaybackAndCleanup();
        }

        private void StopPlaybackAndCleanup()
        {
            mediaPlayer.Stop();
            isPaused = false;
            currentSegmentIndex = 0;
            PlaybackProgress.Value = 0;
            ClearTempFiles();
        }

        private void ClearTempFiles()
        {
            foreach (var file in tempFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch
                {
                    // ignoruj błędy usuwania
                }
            }
            tempFiles.Clear();
        }

        private List<(string voice, string text)> ParseTextWithVoices(string input, string defaultVoice)
        {
            var segments = new List<(string voice, string text)>();
            var pattern = @"\[(.+?)\]";
            var matches = Regex.Matches(input, pattern);
            int lastIndex = 0;
            string currentVoice = defaultVoice ?? "";

            foreach (Match match in matches)
            {
                int index = match.Index;
                string textBefore = input.Substring(lastIndex, index - lastIndex);
                if (!string.IsNullOrWhiteSpace(textBefore))
                {
                    segments.Add((currentVoice, textBefore.Trim()));
                }

                string voiceName = match.Groups[1].Value.Trim();
                currentVoice = voiceName;
                lastIndex = index + match.Length;
            }

            if (lastIndex < input.Length)
            {
                string remainingText = input.Substring(lastIndex).Trim();
                if (!string.IsNullOrWhiteSpace(remainingText))
                {
                    segments.Add((currentVoice, remainingText));
                }
            }

            return segments;
        }

        private void UpdateProgress()
        {
            if (audioSegments.Count == 0) return;

            double progress = ((double)(currentSegmentIndex) / audioSegments.Count) * 100;
            PlaybackProgress.Value = progress;
        }
    }
}
