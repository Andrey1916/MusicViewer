using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MusicView.Windows
{
    /// <summary>
    /// Логика взаимодействия для MessageDialogWindow.xaml
    /// </summary>
    public partial class MessageDialogWindow : Window
    {
        public new MessageBoxResult DialogResult
        {
            get;
            private set;
        } = MessageBoxResult.None;

        public MessageDialogWindow(string Message)
        {
            InitializeComponent();

            messageTextBlock.Text = Message;
            firstBtn.Click += OkBtn_Click;
        }

        public MessageDialogWindow(string Message, string caption)
        {
            InitializeComponent();

            messageTextBlock.Text = Message;
            Title = caption;
            firstBtn.Click += OkBtn_Click;
        }

        public MessageDialogWindow(string Message, string caption, MessageBoxButton messageBoxButton)
        {
            InitializeComponent();

            messageTextBlock.Text = Message;
            Title = caption;

            switch (messageBoxButton)
            {
                case MessageBoxButton.OK:
                    firstBtn.Click += OkBtn_Click;
                    break;
                case MessageBoxButton.OKCancel:
                    firstBtn.Content = "Cancel";
                    secondBtn.Content = "OK";
                    firstBtn.Click += CancelBtn_Click;
                    secondBtn.Click += OkBtn_Click;
                    break;
                case MessageBoxButton.YesNo:
                    firstBtn.Content = "No";
                    secondBtn.Content = "Yes";
                    secondBtn.Visibility = Visibility.Visible;
                    firstBtn.Click += NoBtn_Click;
                    secondBtn.Click += YesBtn_Click;
                    break;
                case MessageBoxButton.YesNoCancel:
                    firstBtn.Content = "Cancel";
                    secondBtn.Content = "No";
                    thirdBtn.Content = "Yes";
                    firstBtn.Click += CancelBtn_Click;
                    secondBtn.Click += NoBtn_Click;
                    thirdBtn.Click += YesBtn_Click;
                    secondBtn.Visibility = Visibility.Visible;
                    thirdBtn.Visibility = Visibility.Visible;
                    break;
                default: break;
            }
        }

        public static MessageBoxResult Show(string Message)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message);
            //dialogWindow.Owner = 
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            System.Media.SystemSounds.Exclamation.Play();
            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        public static MessageBoxResult Show(string Message, string caption)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message, caption);
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            System.Media.SystemSounds.Exclamation.Play();
            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        public static MessageBoxResult Show(string Message, string caption, MessageBoxButton messageBoxButton)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message, caption, messageBoxButton);
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            System.Media.SystemSounds.Exclamation.Play();
            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.OK;
            this.Close();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.Yes;
            this.Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.No;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.Cancel;
            this.Close();
        }
    }
}
