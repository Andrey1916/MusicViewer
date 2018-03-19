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
    /// Dialog window for display message.
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
            }
        }

        /// <summary>
        /// Show message in to the dialog window.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="Message">Message.</param>
        /// <returns>MessageBoxResult</returns>
        public static MessageBoxResult Show(Window owner, string Message)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message);
            dialogWindow.Owner = owner;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            System.Media.SystemSounds.Exclamation.Play();

            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        /// <summary>
        /// Show message in to the dialog window.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="Message">Message.</param>
        /// <param name="caption">Title of dialog window.</param>
        /// <returns>MessageBoxResult</returns>
        public static MessageBoxResult Show(Window owner, string Message, string caption)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message, caption);
            dialogWindow.Owner = owner;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            System.Media.SystemSounds.Exclamation.Play();

            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        /// <summary>
        /// Show message in to the dialog window.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="Message">Message.</param>
        /// <param name="caption">Title of dialog window.</param>
        /// <param name="messageBoxButton">Set the button which will be displayed.</param>
        /// <returns>MessageBoxResult</returns>
        public static MessageBoxResult Show(Window owner, string Message, string caption, MessageBoxButton messageBoxButton)
        {
            MessageDialogWindow dialogWindow = new MessageDialogWindow(Message, caption, messageBoxButton);
            dialogWindow.Owner = owner;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            System.Media.SystemSounds.Exclamation.Play();
            dialogWindow.ShowDialog();
            return dialogWindow.DialogResult;
        }

        /// <summary>
        /// Click Ok.
        /// </summary>
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.OK;
            this.Close();
        }

        /// <summary>
        /// Click Yes.
        /// </summary>
        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.Yes;
            this.Close();
        }

        /// <summary>
        /// Click No.
        /// </summary>
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.No;
            this.Close();
        }

        /// <summary>
        /// Click Cancel.
        /// </summary>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.Cancel;
            this.Close();
        }
    }
}
