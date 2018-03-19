using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using MusicView.Controls;
using NAudio.Wave;
using System.Threading;
using System.Windows.Threading;
using Microsoft.VisualBasic.FileIO;
using MusicView.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Windows.Interop;

namespace MusicView
{
    public partial class MainWindow : Window
    {
        internal static MainWindow mainWindow;

        // NAudio
        private WaveOut waveOutDevice;
        private AudioFileReader audioFileReader;

        internal static SoundUControl SelectedSoundUC;
        // Thread for viewer position
        private Thread threadSliderPosition;
        TimeSpan time, currTime;

        // Chevrons for button
        private Geometry PauseChevron, PlayChevron;

        // Path to directories
        private string firstFolderPath = "", secondFolderPath = "";
        private List<FolderUControl> firstFolderList, secondFolderList;
        private SolidColorBrush DiffBrush;
        
        // Buttons for toolbar
        private ThumbnailToolBarButton ttbbPrevious, ttbbPlayPause, ttbbNext;


        public string CurrentSound
        {
            get => audioFileReader?.FileName;
        }

        /// <summary>
        /// Start new sound.
        /// </summary>
        private void StartNewSound()
        {
            waveOutDevice = new WaveOut();
            waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;

            audioFileReader = new AudioFileReader(SelectedSoundUC.Tag.ToString());

            waveOutDevice.Init(audioFileReader);

            PositionSlider.IsEnabled = true;
            PositionSlider.Maximum = audioFileReader.TotalTime.TotalSeconds;

            SoundNameTextBlock.Text = SelectedSoundUC.SoundName.Text.Remove(SelectedSoundUC.SoundName.Text.Length - 4);
            EndPositionTextBlock.Text = audioFileReader.TotalTime.Minutes + ":" + audioFileReader.TotalTime.Seconds;

            waveOutDevice.Play();

            if (threadSliderPosition.ThreadState == (ThreadState.Background | ThreadState.Unstarted))
                threadSliderPosition.Start();
            else
                threadSliderPosition.Resume();
        }

        /// <summary>
        /// Stop sound event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e){
            threadSliderPosition.Suspend();

            RoutedEventArgs ev = null;

            Button_Click_3(sender, ev);
        }

        /// <summary>
        /// Dispore player.
        /// </summary>
        private void ClearPleyer()
        {
            if (waveOutDevice != null)
                waveOutDevice.Stop();

            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
        }

        #region Загрузка песен
        /// <summary>
        /// Create and render new folder control.
        /// </summary>
        /// <param name="folder">Folder name</param>
        /// <param name="listBox">List box in which it will be rendered.</param>
        /// <param name="folderList">List of directories.</param>
        /// <returns></returns>
        private async Task AddFolder(string folder, ListBox listBox, List<FolderUControl> folderList)
        {
            string[] files = Directory.GetFiles(folder);
            bool hasFile = false;

            if (files.Length > 0)
            {
                foreach(string filePath in files)
                    if (filePath.EndsWith(".mp3"))
                    {
                        hasFile = true;
                        break;
                    }

                if (hasFile)
                    await AddFiles(folder, files, listBox, folderList);
            }

            string[] folders = Directory.GetDirectories(folder);
            if (folders.Length > 0)
                foreach (string folderPath in folders)
                    await AddFolder(folderPath, listBox, folderList);

            if (folders.Length <= 0 && files.Length <= 0) MessageDialogWindow.Show(this, "Directory is empty!", "Error");
        }
        /// <summary>
        /// Create and render new sound control.
        /// </summary>
        /// <param name="folder">Folder name.</param>
        /// <param name="files">Array of file names.</param>
        /// <param name="listBox">List box in whick it will be rendered.</param>
        /// <param name="folderList">List of directories.</param>
        /// <returns></returns>
        private async Task AddFiles(string folder, string[] files, ListBox listBox, List<FolderUControl> folderList)
        {
            FolderUControl folderUControl = new FolderUControl{
                Text = folder
            };

            listBox.Items.Add(folderUControl);
            folderList.Add(folderUControl);

            string[] filesName = null;

            await Task.Run(() =>
            {
                filesName = new string[files.Length];

                for (int i = 0; i < filesName.Length; i++)
                    filesName[i] = files[i].Remove(0, folder.Length + 1);
            });
            

            for (int i = 0; i < files.Length; i++)
                if (files[i].EndsWith(".mp3"))
                {
                    SoundUControl soundUControl = new SoundUControl();

                    soundUControl.SoundName.Text = filesName[i];
                    soundUControl.Tag = files[i];
                    soundUControl.index = i;
                    soundUControl.Visibility = Visibility.Collapsed;
                    soundUControl.ownFolder = folderUControl;

                    listBox.Items.Add(soundUControl);

                    folderUControl.listSound.Add(soundUControl);
                }

            folderUControl.UpdateName();
        }
        #endregion

        //Events
        public MainWindow()
        {
            InitializeComponent();

            waveOutDevice = new WaveOut();
            mainWindow = this;

            PlayChevron = Geometry.Parse("M1,2 L9,8 L1,14 Z");
            PauseChevron = Geometry.Parse("M1,0.5 L1,15.5 M6,0.5 L6,15.5");

            DiffBrush = new SolidColorBrush(Color.FromRgb(62, 62, 66));

            firstFolderList = new List<FolderUControl>();
            secondFolderList = new List<FolderUControl>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ttbbPrevious = new ThumbnailToolBarButton(System.Drawing.Icon.FromHandle(Properties.Resources.PreviousIcon.GetHicon()), "Previous");
            ttbbPrevious.Click += new EventHandler<ThumbnailButtonClickedEventArgs> (Button_Click_4);

            ttbbPlayPause = new ThumbnailToolBarButton(System.Drawing.Icon.FromHandle(Properties.Resources.PlayIcon.GetHicon()), "Play");
            ttbbPlayPause.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(Button_Click_2);

            ttbbNext = new ThumbnailToolBarButton(System.Drawing.Icon.FromHandle(Properties.Resources.NextIcon.GetHicon()), "Next");
            ttbbNext.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(Button_Click_3);

            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(new WindowInteropHelper(this).Handle, ttbbPrevious, ttbbPlayPause, ttbbNext);

            threadSliderPosition = new Thread(
                () => {
                    while (true)
                    {
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                            (ThreadStart)delegate ()
                            {
                                PositionSlider.Value = audioFileReader.CurrentTime.TotalSeconds;
                            }
                        );
                        Thread.Sleep(400);
                    }
                }
                )
            { IsBackground = true };
        }

        /// <summary>
        /// Suspend position thread 
        /// </summary>
        internal void SuspendPositionThread()
        {
            if (threadSliderPosition.ThreadState != (ThreadState.Background | ThreadState.Unstarted))
                if (threadSliderPosition.ThreadState != (ThreadState.Background | ThreadState.Suspended))
                    threadSliderPosition.Suspend();
        }

        /// <summary>
        /// Deleting sound.
        /// </summary>
        internal async void DeleteSound()
        {
            bool eq = CurrentSound == SelectedSoundUC.Tag.ToString();
            string name = SelectedSoundUC.Tag.ToString();

            await Task.Run(() =>
            {
                if (eq)
                {
                    SuspendPositionThread();

                    // Wait from suspended
                    while (!(threadSliderPosition.ThreadState != (ThreadState.Background | ThreadState.Suspended) ||
                        threadSliderPosition.ThreadState != (ThreadState.Background | ThreadState.Unstarted))) { }

                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)ClearPleyer);
                }

                while (waveOutDevice != null) { }

                FileSystem.DeleteFile(name, UIOption.OnlyErrorDialogs,
                    RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);

                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate ()
                    {
                        if (eq) waveOutDevice = new WaveOut();
                        SelectedSoundUC.ownFolder.listSound.RemoveAt(SelectedSoundUC.index);
                        ((ListBox)SelectedSoundUC.Parent).Items.Remove(SelectedSoundUC);
                        playButtonSymbol.Data = PlayChevron;
                    }
                );

            });
        }

        /// <summary>
        /// Play or stop sound
        /// </summary>
        internal void PlayClick() => Button_Click_2(new object(), new RoutedEventArgs());

        // Open First Folder
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBoxFF.Items.Clear();
                firstFolderPath = folderBrowserDialog.SelectedPath;
                await AddFolder(folderBrowserDialog.SelectedPath, listBoxFF, firstFolderList);
            }
        }

        // Open Second Folder
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBoxSF.Items.Clear();
                secondFolderPath = folderBrowserDialog.SelectedPath;
                await AddFolder(folderBrowserDialog.SelectedPath, listBoxSF, secondFolderList);
            }
        }

        //Play/Pause
        private void Button_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (SelectedSoundUC == null) return;

                if (CurrentSound != SelectedSoundUC.Tag.ToString() &&
                    waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    threadSliderPosition.Suspend();
                    waveOutDevice.Dispose();
                    audioFileReader.Dispose();
                    playButtonSymbol.Data = PauseChevron;
                    ttbbPlayPause.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.PauseIcon.GetHicon());
                    StartNewSound();
                    return;
                }

                if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDevice.Pause();
                    threadSliderPosition.Suspend();
                    playButtonSymbol.Data = PlayChevron;
                    ttbbPlayPause.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.PlayIcon.GetHicon());
                    return;
                }

                if (waveOutDevice.PlaybackState == PlaybackState.Paused &&
                    audioFileReader.FileName == SelectedSoundUC.Tag.ToString())
                {
                    playButtonSymbol.Data = PauseChevron;
                    ttbbPlayPause.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.PauseIcon.GetHicon());
                    waveOutDevice.Play();
                    threadSliderPosition.Resume();
                    return;
                }

                playButtonSymbol.Data = PauseChevron;
                StartNewSound();
            } catch(Exception ex)
            {
                MessageDialogWindow.Show(this, ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => ClearPleyer();

        private void PositionSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => SuspendPositionThread();

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int curSec = Convert.ToInt32(PositionSlider.Value);
            time = new TimeSpan(0, 0, Convert.ToInt32(audioFileReader.TotalTime.TotalSeconds - curSec));

            EndPositionTextBlock.Text = time.Minutes + ":" + (time.Seconds < 10 ? ("0" + time.Seconds)
                : time.Seconds.ToString());

            currTime = new TimeSpan(0, 0, curSec);
            CurrentPositionTextBlock.Text = currTime.Minutes + ":" +
                (currTime.Seconds < 10 ? ("0" + currTime.Seconds) : currTime.Seconds.ToString());
        }

        private void PositionSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan newCurrentTime = new TimeSpan(0,0, Convert.ToInt32(PositionSlider.Value));
            audioFileReader.CurrentTime = newCurrentTime;
            if (threadSliderPosition.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                threadSliderPosition.Resume();
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (firstFolderPath != "" && secondFolderPath != "")
            {
                string[] folders1 = Directory.GetDirectories(firstFolderPath);
                string[] folders2 = Directory.GetDirectories(secondFolderPath);

                List<string> folderIntersection = new List<string>{ "" };

                await Task.Run(() =>
                {
                    string buff = null;

                    for (int i = 0; i < folders1.Length; i++)
                    {
                        buff = folders1[i].Remove(0, firstFolderPath.Length + 1);
                        for (int j = 0; j < folders2.Length; j++)
                        {
                            if (folders2[j] == null) continue;
                            if (folders2[j].EndsWith(buff))
                            {
                                folderIntersection.Add("\\" + buff);
                                folders1[i] = null;
                                folders2[j] = null;
                                break;
                            }
                        }
                    }
                });
                
                for (int i = 0; i < firstFolderList.Count; i++)
                    if (folders1.Contains(firstFolderList[i].Text) )
                        firstFolderList[i].Background = DiffBrush;

                for (int i = 0; i < secondFolderList.Count; i++)
                    if (folders2.Contains(secondFolderList[i].Text))
                        secondFolderList[i].Background = DiffBrush;

                await Task.Run(() =>
                {
                    foreach (string folder in folderIntersection)
                    {
                        string path1 = firstFolderPath + folder;
                        string path2 = secondFolderPath + folder;

                        string[] files1 = Directory.GetFiles(path1);
                        string[] files2 = Directory.GetFiles(path2);

                        string buff = null;

                        for (int i = 0; i < files1.Length; i++)
                        {
                            buff = files1[i].Remove(0, path1.Length + 1);
                            for (int j = 0; j < files2.Length; j++)
                            {
                                if (files2[j] == null) continue;
                                if (files2[j].EndsWith(buff))
                                {
                                    files1[i] = null;
                                    files2[j] = null;
                                    break;
                                }
                            }
                        }

                        this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                            (ThreadStart)delegate ()
                            {
                                for (int i = 0; i < firstFolderList.Count; i++)
                                    if (firstFolderList[i].Text == path1)
                                    {
                                        for (int j = 0; j < firstFolderList[i].listSound.Count; j++)
                                            if (files1.Contains(firstFolderList[i].listSound[j].Tag.ToString()))
                                                firstFolderList[i].listSound[j].Background = DiffBrush;

                                        break;
                                    }

                                for (int i = 0; i < secondFolderList.Count; i++)
                                    if (secondFolderList[i].Text == path2)
                                    {
                                        for (int j = 0; j < secondFolderList[i].listSound.Count; j++)
                                            if (files2.Contains(secondFolderList[i].listSound[j].Tag.ToString()))
                                                secondFolderList[i].listSound[j].Background = DiffBrush;

                                        break;
                                    }
                            }
                        );
                    }
                    
                });

            }
        }

        //Update
        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            listBoxFF.Items.Clear();
            listBoxSF.Items.Clear();

            firstFolderList.Clear();
            secondFolderList.Clear();

            if (firstFolderPath != "")
                await AddFolder(firstFolderPath, listBoxFF, firstFolderList);
            if (secondFolderPath != "")
                await AddFolder(secondFolderPath, listBoxSF, secondFolderList);
        }

        //<<
        private void Button_Click_4(object sender, EventArgs e)
        {
            if (SelectedSoundUC == null) return;
            FolderUControl ownFolder = SelectedSoundUC.ownFolder;

            if (SelectedSoundUC.index - 1 > -1)
            {
                ownFolder.listSound[SelectedSoundUC.index - 1].Button_Click(sender, new RoutedEventArgs());
                Button_Click_2(sender, e);
            }
        }

        //>>
        private void Button_Click_3(object sender, EventArgs e)
        {
            if (SelectedSoundUC == null) return;
            FolderUControl ownFolder = SelectedSoundUC.ownFolder;

            if (SelectedSoundUC.index + 1 < ownFolder.listSound.Count)
            {
                ownFolder.listSound[SelectedSoundUC.index + 1].Button_Click(sender, new RoutedEventArgs());
                Button_Click_2(sender, e);
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSoundUC == null) return;
            if (MessageDialogWindow.Show(this, "Delete " + SelectedSoundUC.Tag.ToString(), "Delete sound", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                DeleteSound();
        }
    }
}
