   M S B A M L   `   `   `  ����D  @MusicView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MusicView.ControlsMusicView.Controls  5      '    !MusicView.Controls.FolderUControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:MusicView.Controls     5        � ToolTip  5      ��  �� RelativeSource �� *Self+ �� Path$	 Tag�.��5	      �� -   5         ��Name$ ButtonContainer��5	      ��5
      �� 5       �� 
TargetType" ��6   .�5   
   ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6   .R�5      ��  ��Name$ PART_border��5      $��1q�5      ���	#2AAAAAAA	 �� Padding$	 1,1,0,0q�5      ._�5      �� 5      
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  �� 
TargetName$ PART_border��6    � �� �#49474c6Q   5      5      5      5   	   .��5       � $��Stretch=�6   $��20��6)    � ColumnDefinitions 5!      �� 5"   
   $
��auto �6   �� 5#   
   $��* �6   .+�5&      F� -   5         F�Name$ Chevron��5(      
�������$��10, 0q�5'      $	��1.5��5)      $��Center=�5*      "����@B ��� �@T� �� �@f��� 	$��Center=�5,      $��0��5-      �� 5/      -   5         ��Name$ 
FolderName��5/      $��Center=�50      $
��Left=�51      $��CharacterEllipsis=�52      $��10,0,0,0q�53      $��NoWrap=�54      
������� 
  Ubuntu#��[  56      $��16(�57      $��1��58      59      5;      5<      