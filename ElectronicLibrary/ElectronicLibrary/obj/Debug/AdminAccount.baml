   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   
   L HElectronicLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ElectronicLibraryElectronicLibrary 5   
   $   ElectronicLibrary.AdminAccount   5   
   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   
   @d2http://schemas.microsoft.com/expression/blend/2008     5   
   6sys&clr-namespace:System;assembly=mscorlib     5   
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   
   1localclr-namespace:ElectronicLibrary     5   
   X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   
   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   E ADragablz, Version=0.0.3.203, Culture=neutral, PublicKeyToken=null6dragablz'http://dragablz.net/winfx/xaml/dragablz  6S   -   5        $
��1540��5
   
   $	��800��5   
     � Title$   Администратор��5   
    � WindowState$ 	Maximized=�6     � WindowStyle$
 None=�68   .��5      � .+�5   
   � $��60��6   $	��Top=�6   ���#AB9396.+�5      �� $@��:Электронная библиотека 'Книжня'��6   
�������$��Center=�6R   $��Center=�6o   $��24(�6�   $��Yu Gothic Medium)�6�   $
��Bold"�6�   6�   �� 5      -   5         ��Name$ btnExit��5      $��Right=�6<   $��Center=�6X   ���#AB9396���#AB9396$��	20 0 20 0q�6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Power=�6*   69   5      5      � 5   
   $��0 60 0 0q�6   .+�5        Dragablz.TabablzControl     InterTabController 5      !  Dragablz.InterTabController  5      .@�5      �� $��
КНИГИ��6   .��5        ���	#FFF7F7F7 5        5      .@�5!      �� $	��200��6$   $
��Left=�60   ��5"      �� 5#   "   $��
КНИГИ��6,   ���	#FFAB93966S   .��5&      � $
��1540��6#    � RowDefinitions 5'   "   �� 5(   &   $	��230 �64   6C   �� 5)   &   66    � ColumnDefinitions 5-   "   �� 5.   &   69   �� 5/   &   69   .+�51   "   &   System.Windows.Controls.DataGrid  -   5          Name$ dgBook��51   +   	   AutoGenerateColumns	 .  $��0��6�   $��0��6�   $��5,0,5,5q�6�   
   Columns
 52   &   0  *System.Windows.Controls.DataGridTextColumn  53   *   ,  &System.Windows.Controls.DataGridColumn   Header$! Название книги��6=   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� *Название+  54   *   $! Год публикации��6=    6U   �� *ГодПубликации+  55   *   $  Краткий сюжет ��6=    6U   �� *КраткийСюжет+  56   *   $) #Количество страниц��6=    6Y   �� *$"КоличествоСтраниц+  57   *   $ Фото обложки��6=    6S   �� *ФотоОбложки+  58   *   $ Цена��6=    6K   �� *
Цена+  59   *   $ 
Автор��6=    6L   �� *
Автор+  5:   *   $ Жанр��6=    6K   �� *
Жанр+  5;   *   $! Тип литературы��6=    6U   �� *ТипЛитературы+  5<   *   $ Издатель��6=    6O   �� *Издатель+5>   #   � 5@   "   $��1��6'   $��1��67    5A   &   �� 5B   *   6=   �� 5C   *   6=   �� 5D   *   6=   �� 5E   *   6=    5G   &   �� 5H   *   $	��105 �68   6H   �� 5I   *   6:   �� 5J   *   6:   �� 5K   *   6:   �� 5L   *   6:   .+�5N   &   �� $��0��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��0,15,0,0q�6   $��20(�6�   .��6�   Фамилия автора6�   �� 5O   &   -   5         ��Name$ tbSurnameAuthor��5O   .   $��0��6G   $��0��6W   $��16(�6d   $��Center=�6r   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,5q�6�   6�   �� 5P   &   $��0��6,   $��1��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Имя автора6�   �� 5Q   &   -   5        $ tbNameAuthor��5Q   .   $��0��6D   $��1��6T   $��16(�6a   $��Center=�6o   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,05q�6�   6�   �� 5R   &   $��0��6,   $��2��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Отчество автора6�   �� 5S   &   -   5        $ tbMiddleNameAuthor��5S   .   $��0��6J   $��2��6Z   $��16(�6g   $��Center=�6u   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,05q�6�   6�   �� 5T   &   $��1��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��0,15,0,0q�6   $��20(�6�   .��6�   Название жанра6�   �� 5U   &   -   5        $ tbNameGenre��5U   .   $��1��6C   $��0��6S   $��16(�6`   $��Center=�6n   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,5q�6�   6�   �� 5V   &   $��2��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   $9��3Название типа 
    литературы��6�   �� 5W   &   -   5        $ tbNameTypeLiterature��5W   .   $��2��6L   $��0��6\   $��16(�6i   $��Center=�6w   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,5q�6�   6�   �� 5X   &   $��3��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��0,15,0,0q�6   $��20(�6�    � Cursor$
 Hand��6�   .��6�   #!Название издателя6�   �� 5Y   &   -	   5        $ tbNamePublisher��5Y   .   $��3��6G   $��0��6W   $��16(�6d   $��Center=�6r   $��Bottom=�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,5q�6�   6�   �� 5Z   &   -
   5        $ btnAddAuthor��5Z   -   $��0��6^   $��3��6n   $	��Top=�6{   $��0,20,0,0q�6�   $#��Добавить автора��6�   $��16(�6�   6�   �� 5[   &   -   5        $ btnUpdateAuthor��5[   -   $��0��6d   $��3��6t   $��2��6�   $��Center=�6�   $��0,0,0,20q�6�   $#��Изменить автора��6�   $��16(�6�   6�   �� 5\   &   -   5        $ btnDeleteAuthor��5\   -   $��0��6d   $��4��6t   $��2��6�   $��Center=�6�   $��0,0,0,15q�6�   $!��Удалить автора��6�   $��16(�6�   6�   �� 5]   &   -   5        $ btnAddGenre��5]   -   $��1��6\   $��1��6l   $	��Top=�6y   $��0,10,0,0q�6�   $	��180��6�   $��Добавить жанр��6�   $��16(�6�   6�   �� 5^   &   -   5        $ btnUpdateGenre��5^   -   $��1��6b   $��1��6r   $��2��6   $��Center=�6�   $��0,0,0,40q�6�   $	��180��6�   $��Изменить жанр��6�   $��16(�6�   6�   �� 5_   &   -   5        $ btnDeleteGenre��5_   -   $��1��6b   $��1��6r   $��2��6   $��Bottom=�6�   $��0,0,0,50q�6�   $	��180��6�   $��Удалить жанр��6�   $��16(�6�   �� 5`   &   -   5        $ btnAddTypeLiterature��5`   -   $��2��6n   $��1��6~   $	��Top=�6�   $��0,10,0,0q�6�   $	��180��6�   $5��/Добавить тип 
  литературы��6�   $��60��6�   $��16(�6�   6  �� 5a   &   -   5        $ btnUpdateTypeLiterature��5a   -   $��2��6t   $��2��6�   $��2��6�   $	��Top=�6�   $��0,0,0,0q�6�   $	��180��6�   $5��/Изменить тип 
  литературы��6�   $��60��6  $��16(�6  6  �� 5b   &   -   5        $ btnDeleteTypeLiterature��5b   -   $��2��6u   $��2��6�   $��2��6�   $��Center=�6�   $��	0,40,0,00q�6�   $	��180��6�   $2��,Удалить тип 
 литературы��6�   $��60��6  $��16(�6  �� 5c   &   -   5        $ btnAddPublisher��5c   -   $��3��6d   $��1��6t   $	��Top=�6�   $��0,10,0,0q�6�   $	��180��6�   $'��!Добавить издателя��6�   $��16(�6�   $
 Hand��6�   6�   �� 5d   &   -   5        $ btnUpdatePublisher��5d   -   $��3��6j   $��1��6z   $��2��6�   $��Center=�6�   $��0,0,0,40q�6�   $	��180��6�   $'��!Изменить издателя��6�   $��16(�6�   6�   �� 5e   &   -   5        $ btnDeletePublisher��5e   -   $��3��6j   $��1��6z   $��2��6�   $��Bottom=�6�   $��	0,0,0, 50q�6�   $	��180��6�   $%��Удалить издателя��6�   $��16(�6�   $
 Hand��6�   5g   #   � 5i   "   $��1��6'   $��0��67   $
 Hand��6D    5j   &   �� 5k   *   6=   �� 5l   *   6=   �� 5m   *   6=   �� 5n   *   6=   .+�5p   &     -   5        $ dgAuthor��5p   /   	 .  $��0��6�   $��	10,0,10,0q�6�   
 5q   *     5s   .   $! Фамилия автора��6A    6Y   �� *SecondNameAuthor+  5t   .   $ Имя автора��6A    6U   �� *FirstNameAuthor+  5u   .   $# Отчество автора��6A    6Z   �� *MiddleNameAuthor+5w   '     5x   &   -   5        $ dgGenre��5x   [   	 .  $��1��6�   $��	10,0,10,0q�6�   
 5y   *     5z   .   $ Жанр��6A    6O   �� *	NameGenre+5|   '     5}   &   -   5        $ dgTypeLiterature��5}   d   	 .  $��2��6�   $��	10,0,10,0q�6�   
 5~   *     5   .   $! Тип литературы��6A    6Y   �� *NameTypeLiterature+5�   '     5�   &   -   5        $ dgPublisher��5�   _   	 .  $��3��6�   $��	10,0,10,0q�6�   
 5�   *     5�   .   $ Издатель��6A    6S   �� *NamePublisher+5�   '   5�   #   5�      5�      �� 5�      $	��200��6%   ���	#FFAB9396$
��Left=�6H   ��5�      �� 5�   "   $��
АРХИВ��6,   ���	#FFAB93966S   .��5�      � $
��1540��6#   $
 Hand��61    5�   "   �� 5�   &   $	��200 �64   6C   �� 5�   &   $	��100 �64   6C   �� 5�   &   66   .+�5�   "     -   5        $ dgBookArchive��5�   +   	 .  $��0��6^   $
��1450��6k   $
 Hand��6x   
 5�   &   5�   #   �� 5�   "   -   5        $ btnBookReturn��5�   )   $��1��6@   $	��250��6M   $��Right=�6Y   $��0,0,40,0q�6u   $)��#Восстановить книгу��6�   $��16(�6�   $��40��6�   6�   � 5�   "   $��2��6'   $
 Hand��65    5�   &   �� 5�   *   $	��600 �6;   6I   �� 5�   *   $	��250 �6;   6I   �� 5�   *   $	��250 �6;   6I   �� 5�   *   6=   .+�5�   &     -   5        $ dgTypeLiteratureArhive��5�   0   	 .  $��0��6m   $	��300��6}   $��Right=�6�   $��	0,20,30,0q�6�   $	��200��6�   $	��Top=�6�   
 5�   *     5�   .   $! Тип литературы��6A    6Y   �� *NameTypeLiterature+5�   '   �� 5�   &   -   5        $ btnTypeLiteratureReturn��5�   -   $��0��6u   $	��300��6�   $��Right=�6�   $��
0,180,30,0q�6�   $��60��6�   $@��:Восстановление типа литературы��6�   $��16(�6�   6    5�   &   -   5        $ dgAuthorArhive��5�   /   	 .  $��1��6d   $	��200��6t   $��Right=�6�   $��	0,20,25,0q�6�   $	��200��6�   $	��Top=�6�   
 5�   *     5�   .   $! Фамилия автора��6A    6Y   �� *SecondNameAuthor+  5�   .   $ Имя автора��6A    6U   �� *FirstNameAuthor+  5�   .   $# Отчество автора��6A    6Z   �� *MiddleNameAuthor+5�   '   �� 5�   &   -   5        $ btnAuthorReturn��5�   -   $��1��6d   $	��190��6t   $��Right=�6�   $��
0,180,25,0q�6�   $��60��6�   $8��2Восстановление 
        автора��6�   $��16(�6�   6�     5�   &   -    5        $ dgGenreArhive��5�   /   	 .  $��2��6c   $	��200��6s   $��Right=�6   $��	0,20,25,0q�6�   $	��200��6�   $	��Top=�6�   
 5�   *     5�   .   $ Жанр��6A    6O   �� *	NameGenre+5�   '   �� 5�   &   -!   5        $ btnGenreReturn��5�   -   $��2��6b   $	��190��6r   $��Right=�6~   $��
0,180,28,0q�6�   $��60��6�   $5��/Восстановление 
         жанр��6�   $��16(�6�   6�     5�   &   -"   5        $ dgPublisherArhive��5�   /   	 .  $��3��6g   $	��200��6w   $
��Left=�6�   $��
25,20,25,0q�6�   $	��200��6�   $	��Top=�6�   $
 Hand��6�   
 5�   *     5�   .   $ Издатель��6A    6S   �� *NamePublisher+5�   '   �� 5�   &   -#   5        $ btnPublisherReturn��5�   -   $��3��6k   $	��190��6{   $
��Left=�6�   $��33,180,25,0q�6�   $��60��6�   $;��5Восстановление 
       издателя��6�   $��16(�6�   $
 Hand��6�   6  5�   #   5�      5�      5�      5�      �� 5�      $��ПОЛЬЗОВАТЕЛИ��6   .��5�        ���	#FFF7F7F7 5�        5�      .@�5�      �� $	��200��6$   
�������$
��Left=�6C   ��5�      �� 5�   "   $��ПОЛЬЗОВАТЕЛИ��6,   ���	#FFAB93966Z   .��5�      �  5�   "   �� 5�   &   $	��400 �64   6C   �� 5�   &   66    5�   "   �� 5�   &   6:   �� 5�   &   $	��550 �67   6E   .+�5�   "   � $��1��6'   $��0��67   .+�5�   &     -$   5        $ dgRole��5�   /   	 .  $	��450��6�   $	��150��6�   $	��Top=�6�   $  ��6�   
 5�   *     5�   .   $ Название роли��6B    6Y   �� *
NameRole+5�   '   �� 5�   &   $��Center=�6,   $	��Top=�6J   $��20(�6b   $��	0,160,0,0q�6p   .��6�   
Роль6�   �� 5�   &   -%   5        $ 
tbNameRole��5�   .   $��Center=�6B   $	��Top=�6_   ���#f4f1f1$	��240��6�   $��16(�6�   $��40��6�   $��	0,200,0,5q�6�   6�   �� 5�   &   -&   5        $ 
btnAddRole��5�   -   $	��Top=�6Z   $��	0,250,0,5q�6r   $��Добавить роль��6�   $��18(�6�   $	��240��6�   6�   �� 5�   &   -'   5        $ btnUpdateRole��5�   -   $	��Top=�6`   $��	0,300,0,0q�6x   $��Изменить роль��6�   $��18(�6�   $	��240��6�   6�   �� 5�   &   -(   5        $ btnDeleteRole��5�   -   $	��Top=�6`   $��	0,350,0,0q�6x   $��Удалить роль��6�   $��18(�6�   $	��240��6�   6�   5�   #   � 5�   "   $��0��6'   $��0��67   $
 Hand��6D    5�   &   �� 5�   *   6:   �� 5�   *   6:   .+�5�   &     -)   5        $ dgUser��5�   /   	 .  $��0��6�   $��0,0,0,10q�6�   
 5�   *     5�   .   $! Имя покупателя��6A    6Y   �� *	FirstName+  5�   .   $) #Фамилия покупателя��6A    6]   �� *
SecondName+  5�   .   $+ %Отчество покупателя��6A    6^   �� *
MiddleName+  5�   .   $ 
Логин��6A    6P   �� *Login+  5�   .   $ Дата рождения��6A    6X   �� *	BirthDate+  5�   .   $ 
Почта��6A    6P   �� *Email+5�   '     5�   &   -*   5        $ 
dgEmployee��5�   ^   	 .  $��1��6�   $��0,10,0,0q�6�   
 5�   *     5�   .   $ Имя продавца��6A    6W   �� *	FirstName+  5�   .   $% Фамилия продавца��6A    6[   �� *
SecondName+  5�   .   $' !Отчество продавца��6A    6\   �� *
MiddleName+  5�   .   $ 
Логин��6A    6P   �� *Login+  5�   .   $ Дата рождения��6A    6X   �� *	BirthDate+  5�   .   $ 
Почта��6A    6P   �� *Email+  5�   .   $! Серия паспорта��6A    6Y   �� *PassportSeries+  5�   .   $! Номер паспорта��6A    6Y   �� *PassportNumber+5�   '   5�   #   � 5�   "   $��1��6'   $��0��64   $��2��6D   $
 Hand��6X    5   &   �� 5  *   6=   �� 5  *   6=   �� 5  *   6=   �� 5  *   6=   �� 5  *   6=   �� 5  *   6=    5  &   �� 5	  *   6:   �� 5
  *   6:   �� 5  *   6:   .+�5  &   �� $��0��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Имя6�   �� 5  &   -+   5        $ 
tbUserName��5  .   $��0��6B   $��0��6R   $��Center=�6_   $��Bottom=�6|   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��0��6,   $��1��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Фамилия6�   �� 5  &   -,   5        $ tbUserSurname��5  .   $��0��6E   $��1��6U   $��Center=�6b   $��Bottom=�6   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��0��6,   $��2��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Отчество6�   �� 5  &   --   5        $ tbUserMiddleName��5  .   $��0��6H   $��2��6X   $��Center=�6e   $��Bottom=�6�   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��1��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   
Логин6�   �� 5  &   -.   5        $ tbLogin��5  .   $��1��6?   $��0��6O   $��Center=�6\   $��Bottom=�6y   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��01��6,   $��1��6=   $��Center=�6J   $	��Top=�6h   $��20(�6�   .��6�   Хеш-пароль6�   �� 5  &   -/   5        $ 
tbPassword��5  .   $��01��6B   $��1��6S   $��Center=�6`   $��Bottom=�6}   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��01��6,   $��2��6=   $��Center=�6J   $	��Top=�6h   $��20(�6�   .��6�   Серия паспорта6�   �� 5  &   -0   5        $ tbSeriesPassport��5  .   $��01��6H   $��2��6Y   $��Center=�6f   $��Bottom=�6�   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��2��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Номер паспорта6�   �� 5  &   -1   5        $ tbNumberPassport��5  .   $��2��6H   $��0��6X   $��Center=�6e   $��Bottom=�6�   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��2��6,   $��1��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   Дата рождения6�   (  "System.Windows.Controls.DatePicker  5  &   -2   5          Name$ dpDateBirthday��5  1   $��2��6I   $��1��6Y   $��Center=�6f   $��Bottom=�6�   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��2��6,   $��2��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   $
 Hand��6�   .��6�   
Почта6�   �� 5  &   -3   5        $ tbEmail��5  .   $��2 ��6?   $��2��6P   $��Center=�6]   $��Bottom=�6z   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6�   6�   �� 5  &   $��3��6,   $��0��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   .��6�   
Роль6�   �� 5   &   -4   5         ��Name$ cbRole��5   /    �� DisplayMemberPath$ NameRole��6?    �� SelectedValuePath$ RoleId��6\   $��3��6w   $��0��6�   $��Center=�6�   $��Bottom=�6�   $��16(�6�   ���#f4f1f1$	��180��6�   $��40��6�   $��0,0,0,30q�6  6  �� 5!  &   -5   5        $ 
btnAddUser��5!  -   $��3��6Z   $��1��6j   $	��Top=�6w   $��	0,-10,0,0q�6�   $/��)Добавить пользователя��6�   $��18(�6�   $	��240��6�   6�   �� 5"  &   -6   5        $ btnUpdateUser��5"  -   $��3��6`   $��1��6p   $��Bottom=�6}   $��0,0,0,25q�6�   $/��)Изменить пользователя��6�   $��18(�6�   $	��240��6�   6�   �� 5#  &   -7   5        $ btnDeleteUser��5#  -   $��3��6`   $��2��6p   $	��Top=�6}   $��0,0,0,30q�6�   $-��'Удалить пользователя��6�   $��18(�6�   $	��240��6�   6�     5$  &   -8   5        $ dgRiderTicket��5$  a   	 .  $��0��6�   $��2��6�   $��4��6�   $��2��6�   $��0,5,0,40q�6  
 5%  *     5&  .   $8 2Номер читательского билета��6A    6e   �� *NumberRiderTicket+  5'  .   $ Дата выдачи��6A    6V   �� *
DateTerm+  5(  .   $) #Логин пользователя��6A    6]   �� *
User.Login+5+  '   �� 5,  &   -9   5        $ btnUpdateRiderTicket��5,  -   $��5��6n   $��1��6~   $��2��6�   $��Center=�6�   $��	0,-90,0,0q�6�   $F��@         Изменить 
 читательский билет ��6�   $��18(�6   $	��240��6  $��60��6  6*  �� 5/  &   $��4��6,   $��1��6<   $��Center=�6I   $	��Top=�6g   $��20(�6   $
 Hand��6�   $��0,20,0,0q�6�   .��6�   %#Читательский билет6�   �� 50  &   -:   5        $ tbRiderTicket��50  .   $��4 ��6E   $��1��6V   $��Center=�6c   $��Bottom=�6�   ���#f4f1f1$��16(�6�   $	��180��6�   $��40��6�   $��0,0,0,10q�6�   6�   52  #   54     55     �� 56     $	��200��6#   ���	#FFAB9396$
��Left=�6F   ��57     �� 58  "   $��
АРХИВ��6,   ���	#FFAB93966S   .��5;     �  5<  "   �� 5=  &   66   �� 5>  &   66   �� 5?  &   66   .+�5A  "     -;   5        $ dgEmployeeArhive��5A  +   	 .  $��0��6a   $
��1300��6o   $	��150��6|   $	��Top=�6�   $��0,10,0,0q�6�   
 5B  &     5C  *   $ Имя продавца��6=    6S   �� *	FirstName+  5D  *   $% Фамилия продавца��6=    6W   �� *
SecondName+  5E  *   $' !Отчество продавца��6=    6X   �� *
MiddleName+  5F  *   $ 
Логин��6=    6L   �� *Login+  5G  *   $ Дата рождения��6=    6T   �� *	BirthDate+  5H  *   $ 
Почта��6=    6L   �� *Email+  5I  *   $! Серия паспорта��6=    6U   �� *PassportSeries+  5J  *   $! Номер паспорта��6=    6U   �� *PassportNumber+5M  #   �� 5N  "   -<   5        $ btnEmployeeReturn��5N  *   $��0��6e   $��Bottom=�6r   $��
0,0,120,10q�6�   $1��+Востановить сотрудника��6�   $��16(�6�   $	��240��6�   $��Right=�6�   6�     5O  "   -=   5        $ dgUserArhive��5O  +   $��1��6A   	 .  $
��1300��6k   $	��150��6x   $	��Top=�6�   $��0,10,0,0q�6�   
 5P  &     5Q  *   $! Имя покупателя��6=    6U   �� *	FirstName+  5R  *   $) #Фамилия покупателя��6=    6Y   �� *
SecondName+  5S  *   $+ %Отчество покупателя��6=    6Z   �� *
MiddleName+  5T  *   $ 
Логин��6=    6L   �� *Login+  5U  *   $ Дата рождения��6=    6T   �� *	BirthDate+  5V  *   $ 
Почта��6=    6L   �� *Email+5X  #   �� 5Y  "   ->   5        $ btnUserReturn��5Y  )   $��1��6\   $��Bottom=�6i   $��
0,0,120,10q�6�   $+��%Востановить клиента��6�   $��16(�6�   $	��240��6�   $��Right=�6�   6�     5Z  "   -?   5        $ dgRoleArhive��5Z  +   $��1��6A   	 .  $��2��6j   $	��400��6|   $	��250��6�   $��Bottom=�6�   $��
120,0,0,10q�6�   $
��Left=�6�   
 5[  &     5\  *   $ Название роли��6>    6U   �� *
NameRole+5^  #   �� 5_  "   -@   5        $ btnReturnRole��5_  )   $��2��6]   $��Bottom=�6j   $��
550,0,0,10q�6�   $%��Востановить роль��6�   $��16(�6�   $	��240��6�   $
��Left=�6�   6�   5a     5b     5c     5d     �� 5e     $>��8ЛОГИРОВАНИЕ И РАБОТА С ДАННЫМИ��6   $
 Hand��6B   .��5f     � .+�5g       -A   5        $ 	dgLogging��5g  #   $
��1400��6e   $	��500��6r   $	��Top=�6   $��0,50,0,0q�6�   $
 Hand��6�   	 .  
 5h       5i  "   $! Дата изменения��65    6M   �� *
DateForm+  5j  "   $' !Стоимость покупки��65    6P   �� *	CostIssue+  5k  "   $$ Статус действия ��65    6O   �� *StatusLogging.StatusLogName+  5l  "   $% Штрихкод покупки��65    6O   �� *IssueProduct.Barcode+  5m  "   $) #Читательский билет��65    6Q   �� *RiderTicket.NumberRiderTicket+  5n  "   $! Время действия��65    6M   �� *	TimeNotes+  5o  "   $) #Логин пользователя��65    6Q   �� *
User.Login+5r     �� 5s     -B   5        $ 	btnExport��5s  !   $	��250��6L   $��40��6X   $��Bottom=�6d   $��Right=�6   $��
0,50,70,50q�6�   $*��$Экспорт базы данных��6�   $��18(�6�   6�   �� 5t     -C   5        $ 	btnImport��5t  !   $	��250��6L   $��40��6X   $��Bottom=�6d   $��Right=�6   $��0,50,350,50q�6�   $(��"Импорт базы данных��6�   $
 Hand��6�   $��18(�6�   6�   5u     5v     5x     5y     5z     5{     