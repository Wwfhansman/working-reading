   M S B A M L   `   `   `  ����J  F凡哥阅读器, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ReadingApp.ViewsReadingApp.Views  5   	   =#clr-namespace:ReadingApp.ViewModelsReadingApp.ViewModels  5   	   =#clr-namespace:ReadingApp.ConvertersReadingApp.Converters  5   	   !    ReadingApp.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ReadingApp.Views     5   	   2vm#clr-namespace:ReadingApp.ViewModels     5   	   :
converters#clr-namespace:ReadingApp.Converters     5   	     � Title$  凡哥阅读器��5
   	   $	��500��6   $	��800��6$    � 	Resources 5      2   ,ReadingApp.Converters.BooleanInvertConverter   BooleanInvertConverter  BookItemStyle �� 
TargetType �� Property �� Value��%2  &	        &	        5   
   6,   �� 5   
   " ��6&   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 10q�6(   �� 5       � ��$ 1q�60   �� 5       � �� �#DDDDDD6,   �� 5       � �� �#F9F9F96+   �� 5       � ��$ Stretch=�6;   5      5      .��5      � .+�5   
   �� .@�5      �� $��上传��6    �� 
IsSelected 6"   �� *IsUploadTabSelected+.��5      � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $��* �6(   �� 5       $
��Auto �6(   .+�5#      �� $��上传小说文件��6    $��24(�6.   $��0,0,0,20q�6<   �� 5%      $��1��6!    �� Orientation$ 
Horizontal=�6.   $��0,0,0,20q�6G   .+�5&      �� $	��550��6D    �� 
IsReadOnly$
 True��6P   $��0,0,10,0q�6b   ��6"   �� *SelectedFilePath+6b   �� 5'      $��选择文件��6!   $	��100��6V   ��60   �� *SelectFileCommand+6V   5(      �� 5*      $��2��6    $����请选择要上传的TXT格式小说文件。
系统会自动解析文件名中的书名和作者信息。
例如：《斗破苍穹》-天蚕土豆.txt 会被解析为书名《斗破苍穹》，作者：天蚕土豆��6-   $
��Wrap=�5+       $��0,20,0,0q�64   ���#6666666F   �� 5-      $��3��6   $��上传��6*   $	��100��6]   $��Right=�5.      $��0,20,0,0q�69   $��30��6K   ��5-   7   �� *UploadFileCommand+5.   K   5/      50      �� 52      $��书架��6   .��53      � $��20q�6    54      �� 55      $
��Auto �6(   �� 56      $��* �6(   .+�59      �� $��我的书架��6    $��24(�6,   $��0,0,0,20q�6:   �� 5;      $��1��6   #��[ 5<      ��5;   +   �� *Books+	 �� SelectedItem	 5=      �� *SelectedBook+��5>      �� 5?      .R�5@   "   � 
 � ColumnDefinitions
 5A   &   �� 5B   *   $��* �6;   �� 5C   *   $
��Auto �6;   .+�5F   &   �� .+�5G   *   �� $
��Bold"�6K   $��16(�6]   ��64   �� *Title+6]   �� 5H   *   $��0,5,0,0q�6L   ���#666666��64   �� *Author+6]   �� 5I   *   $ 
Horizontal=�65   $��0,5,0,0q�6N   .+�5J   .   �� $��阅读进度：��68   ���#6666666E   �� 5K   .   �� 5L   2   �66   �� *CurrentPage+ �� 5M   2   $�/��66    �� 5N   2   �66   �� *
TotalPages+5O   /   5P   +   �� 5Q   *   $��0,5,0,0q�64   ���#666666�� 5R   .   $�最后阅读时间：��62    �� 5S   .   �62   �� *LastReadTime+ �� StringFormat$ yyyy-MM-dd HH:mm:ss��5T   +   5U   '   �� 5W   &   $��1��61   $ Vertical=�6A   $��10,0,0,0q�6X   .+�5X   *   �� $��阅读��61   $��80��5[   /   $��0,0,0,10q�6:   ��5X   >   �� *DataContext.OpenBookCommand+ �� RelativeSource ��  �� AncestorType ListBox��5Z   /   �� 5[   :   �� 5\   *   $��删除��61   $��80��5_   /   ��5\   >   �� *DataContext.DeleteBookCommand+ ��  ListBox��5^   /   �� 5_   /   5`   '   5a   #   5b      5d      5e      5f      5g      5h      5i      