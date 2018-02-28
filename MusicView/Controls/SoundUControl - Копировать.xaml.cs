using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicView.Controls
{
    /// <summary>
    /// Логика взаимодействия для SoundUControl.xaml
    /// </summary>
    public partial class SoundUControl : UserControl
    {
        protected Border ContainerButtonBorder;
        internal int index;

        public SoundUControl(){
            InitializeComponent();
        }

        private void PART_border_Loaded(object sender, RoutedEventArgs e) => ContainerButtonBorder = (Border)sender;

        private void DeleteSound(object sender, RoutedEventArgs e)
        {
            if (MainWindow.SelectedSoundUC != this)
            {
                if (MainWindow.SelectedSoundUC != null)
                    MainWindow.SelectedSoundUC.ContainerButtonBorder.Background = Brushes.Transparent;

                MainWindow.SelectedSoundUC = this;
                ContainerButtonBorder.Background = Brushes.Tomato;
            }
            if (Windows.MessageDialogWindow.Show("Delete " + MainWindow.SelectedSoundUC.Tag.ToString() + "?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                bool resume;
                if (MainWindow.SelectedSoundUC.Tag.ToString() == MainWindow.mainWindow.CurrentSound) resume = false;
                else resume = true;

                MainWindow.mainWindow.Do(() =>
                {
                    MainWindow.mainWindow.DeleteSound(resume);
                }, resume);
                ((StackPanel)Parent).Children.Remove(this); 
            }
        }

        internal void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.SelectedSoundUC != null)
                MainWindow.SelectedSoundUC.ContainerButtonBorder.Background = Brushes.Transparent;
            
            MainWindow.SelectedSoundUC = this;
            ContainerButtonBorder.Background = Brushes.Tomato;
        }

        private void ButtonContainer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.SelectedSoundUC != null)
                MainWindow.mainWindow.PlayClick();
        }
    }
}
