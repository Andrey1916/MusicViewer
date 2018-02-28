using MusicView.Windows;
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
        public FolderUControl ownFolder;

        public SoundUControl(){
            InitializeComponent();
        }

        private void PART_border_Loaded(object sender, RoutedEventArgs e) => ContainerButtonBorder = (Border)sender;

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

        private void ButtonContainer_ContextMenuOpening(object sender, ContextMenuEventArgs e) => Button_Click(sender, e);
        
    }
}
