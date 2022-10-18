using MediaManager;
using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SoundsDetect
{
    public partial class MainPage : ContentPage
    {
        AudioRecorderService recorder;
        public MainPage()
        {
            InitializeComponent();
            recorder = new AudioRecorderService
            {
               
                StopRecordingOnSilence = false, //will stop recording after 2 seconds (default)
                StopRecordingAfterTimeout = true,  //stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(180) //audio will stop recording after 3 minutes
            };
            recorder.AudioInputReceived += Recorder_AudioInputReceived;
        }

        private void Recorder_AudioInputReceived(object sender, string e)
        {
           Debug.WriteLine("Max Value is  " +e.Normalize().Max()+"  Min Value "+e.Normalize().Min());
        }

        async void RecordButton_Click(object sender, EventArgs e)
        {
            await RecordAudio();
        }
        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                 
                    await recorder.StartRecording();
                }
                else
                {
                    await recorder.StopRecording();
                }
            }
            catch (Exception ex)
            {
                //...
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var audioFile = await recorder;
            if (recorder.FilePath != null) //non-null audioFile indicates audio was successfully recorded
            {
                //do something with the file
                var path = recorder.FilePath;
                await CrossMediaManager.Current.Play("file://" + path);
            }
        }
    }
}
