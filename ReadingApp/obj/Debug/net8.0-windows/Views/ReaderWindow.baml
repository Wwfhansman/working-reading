   M S B A M L   `   `   `  ����J  F凡哥阅读器, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ReadingApp.ViewsReadingApp.Views  5   	   #    ReadingApp.Views.ReaderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ReadingApp.Views     5   	   -   5        $	��400��5   &   $	��600��63     � WindowStyle$
  None=�5	   	    � AllowsTransparency$
 True��6   ���	#00000000 � Title 5   	   �� *
Book.Title+ � Topmost 5
   	   �� *	IsTopMost+ � 	Resources 5         BorderStyle �� 
TargetType �� Property �� Value  ControlButtonStyle��%G  &	        &	 &    �� 5   
   " ��6$   .�5      ��  � �� �	#800000006+   �� 5       � �� �	#40FFFFFF6,   �� 5       � ��$ 1q�60   �� 5       �@��CornerRadius$ 5��6-   �� 5       �@��Padding$ 10q�6(   �� 5       �@J�Opacity$ 0Q�6(   5      �� 5   
   " ��6+   .�5      ��  � ��$ 30��6&   �� 5       � ��$ 30��6'   �� 5       � ��$ 5,0,0,0q�6'   �� 5       � ��
 ���� 6+   �� 5       � �� �	#40FFFFFF6,   �� 5       � ��
 �����6+   5      5       .��5"      � ���	#00FFFFFF � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $��* �6   �� 5&      $
��Auto �6   .+�5*   
   �� -   5        $��	TopBorder��5*   `   $��0��6   #��[  6   ._�5+      � 	 � ColumnDefinitions	 5,      �� 5-      $��* �6'   �� 5.      $
��Auto �6'   .+�51      �� 
���������6   �� *
Book.Title+68   �� 53      $��1��6   
 �� Orientation$
 
Horizontal=�6-   .+�54      �� $
��📌��6   #��[ 56      ��55      �� *ToggleTopMostCommand+56      �� 58      $	��✕��6   #��[ 5:      ��59      �� *CloseCommand+5:      5;      5<      5=      �� 5@   
   $��1��6   ���	#00FFFFFF J� IsHitTestVisible$
 True��5B      ._�5C      �� -   5        $��ContentScrollViewer��5C   @   $��Hidden=�6   .��5D      �� .+�5E      �� $
��Wrap=�5F       $��20,20,20,10q�5G       ���	#00FFFFFF$��18(�5J       ��5E       �� *
PageText+��5I       �� *IsMouseOver+ # VisibilityToForegroundConverter �� 	Converter# [ 5J       �� 5M      $
 
Horizontal=�6!   $��Center=�5N   !   $��	0,10,0,20q�5P   !   y�5O   !   �� *IsMouseOver+   BooleanToVisibilityConverter# [ .+�5Q      �� $��	上一页��6!   $	��100��5S   !   $��30��5T   !   $��0,0,20,0q�5U   !   ���	#80000000
����������	#40FFFFFF��5R   !   �� *PreviousPageCommand+5X   !   �� 5Z      $��	下一页��6!   $	��100��5\   !   $��30��5]   !   ���	#80000000
����������	#40FFFFFF��5[   !   �� *NextPageCommand+5`   !   5a      5b      5c      5d      �� 5g   
   -   5        $��BottomBorder��5g   C   $��2��6   #��[  6   ._�5h      � 	 5i      �� 5j      $
��Auto �6'   �� 5k      $��* �6'   �� 5l      $
��Auto �6'   .+�5o      �� $
 
Horizontal=�6   $��0��66   .+�5p      �� $��	上一页��6   $��70��5r      $��0,0,10,0q�5s      ��5q      �� *PreviousPageCommand+5s      �� 5u      $��	下一页��6   $��70��5w      ��5v      �� *NextPageCommand+5w      5x      �� 5z      $��2��6   $
 
Horizontal=�6-   .+�5{      �� $��50��5|      $��0,0,5,0q�5}      $��Center=�5~      ��5{      �� *PageInputText+ �� UpdateSourceTrigger$ PropertyChanged=�5~      �� 5�      $��/��6    
�������$��Center=�6<   $��0,0,5,0q�6W   �� 5�      
�������$��Center=�5�       $��0,0,10,0q�6;   ��5�       �� *
TotalPages+5�   ;   �� 5�      $��跳转��6   $��50��5�      ��5�      �� *JumpToPageCommand+5�      5�      5�      5�      5�      5�      