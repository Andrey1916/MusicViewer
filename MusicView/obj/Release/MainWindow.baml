   M S B A M L   `   `   `  ����   	MusicView    MusicView.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � AllowsTransparency  .  � Title$ 
Music View��$	��435��$	��700��$	��350��$	��650�� � WindowStyle$
 None=�   VS2012WindowStyle#��[   � Icon$ Icon.ico��.���   TitleBarBackgroundBrush#���   � ColumnDefinitions �� $
��200* �$	��210���� $��2 ��� $
��200* �$	��210�� � RowDefinitions �� �� $��82 �.+��� -    ��Name$ 	listBoxFF��$��Stretch=�$��Stretch=�$��
10,33,5,10q�$��Disabled=� � ContextMenu ��   ContextMenuDark#��[ .@�m� -   $��Delete��  MenuItemDarkStyle#��[ �� -   $��First Folder��$
��Left=�$��	10,10,0,0q�$	��Top=�$��18��  ButtonDarkStyle#��[  
 Ubuntu#��[ �� -   $��Second Folder��$
��Left=�$��
120,10,0,0q�$	��Top=�$��18��#��[ #��[ �� $��1��$��	0,33,0,10q�$��Stretch=�$��Stretch=�  BorderBrushInactive#��[ �� -   $ 	listBoxSF��$��Stretch=�$��
5,33,10,10q�$��Stretch=�$��2��$��Disabled=� �� #��[ .@�m� -   $��Delete��#��[ � $��Stretch=�$��10q�$��1��$��3��$��Stretch=�.+��� -    ��Name$ PositionSlider��$��Stretch=�$	��Top=�	 � Maximum$		 100Q�  SliderDarkStyle#��[ 
 � Value$
 0Q�$��0,16,0,0q�{�.   � ToolTip ��  �� Path$ Value��� -	    ��Name$ EndPositionTextBlock��$��Right=�$	��100��$
��0:00��#��[ $	��Top=�#��[ �� -
   $ CurrentPositionTextBlock��$
��Left=�$
��0:00��$	��Top=�#��[ $	��100��#��[ � $��Center=�$��Bottom=�$	��300��.+��� -    ��Name$ 	playSound��$��Center=�$	��Top=�$ 
Play/Pause��$��35��$��30��#��[ ��F� -    F�Name$ playButtonSymbol��$	��1.5��$��Center=�"����@B �� �@T�  z �@B ��� 	$��Center=�$��0q�����  �� ElementName$ 	playSound��$ 
Foreground��� -   $ 	nextSound��$
��Left=�$��	182,0,0,0q�$	��Top=�$��36��$��30��$
 Next��#��[ ��F� $	��1.5��$��Center=�4����@B �� �@T�  z �@B ��� Ѐ��  � р�� ��� 	$��Center=�$��0q����� $ 	nextSound��$ 
Foreground��� -   $ previousSound��$��82,0,0,0q�$��36��$��30��$
��Left=�$	��Top=�#��[ $ Previous����F� $	��1.5��$��Center=�4����@B  � �@B ��� ���� �� р�  z р�� ��� 	$��Center=�$��0q����� $ previousSound��$ 
Foreground��� -   $ SoundNameTextBlock��$��Stretch=� �� TextAlignment$ Center=�$��100,0,100,0q�$
��Wrap=�$	��Top=�#��[ #��[ �� -   $��Update��$��2��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75��#��[ #��[ �� -   $
��Diff��$��2��$
��Left=�$��	90,10,0,0q�$	��Top=�$��75��#��[ #��[ 