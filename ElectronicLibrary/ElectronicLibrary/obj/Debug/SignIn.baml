   M S B A M L   `   `   `  ����L  HElectronicLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ElectronicLibraryElectronicLibrary  5   	       ElectronicLibrary.SignIn   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ElectronicLibrary     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   $
��1200��5   	   $	��800��5	   	     � Title$  Авторизация��5   	    � WindowState$ 	Maximized=�6    � WindowStyle$
 None=�65   .��5      � ���#AB9396 � ColumnDefinitions 5   
   �� 5      $
��288* �6   �� 5      $	��43* �6   �� 5      $
��269* �6   .+�5   
   �� $	��400��6   $��	15,0,15,0q�6    
�������$��Center=�6F    �� Padding$ 30q�6a   $	��650��6n    �� CornerRadius$ 20��6}   $��3��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 30Q�6#      Color$
 Gray��63   	   ShadowDepth$	 0Q�6@   6R   ._�5      �� .+�5      �� $��Авторизация��6   $��40(�6/   $
��Bold"�6=   $��0 0 0 20q�6O   $��Center=�6a   ���	#FF6F6F6F6�   �� 5      -   5        
 ��Name$
 txtLogin��5      $��20(�6,   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Введите логин��6:    %  !MaterialDesignFloatingHintTextBox#��[  6i    �� 
CaretBrush �	#FF6B3636 �� SelectionBrush �	#FF9A42346�   G� 5      -   5         G�Name$ pswPassword��5      $��20(�64   $! Введите пароль��6B    ) %MaterialDesignFloatingHintPasswordBox#��[ 6r    G� 
CaretBrush �	#FF6B3636 G� SelectionBrush �	#FF9A42346�   � 5      $��	0 20 0 20q�6   .+�5      �� -   5         ��Name$ 	btnSignIn��5      $��18(�6H   $��
Войти��6V   ���	#FF5B3E30  MaterialDesignRaisedButton#��[ 6~   $��100,30,100,0q�6�   $��32��6�   $	��Top=�6�   5      5      �� 5       -   5        $ 	btnSignUp��5       $��18(�6D   $��Регистрация��6R   ���	#FF5B3E30#��[ 6�   $��100 0 100 0q�6�    � 	Resources 5!       �� 
TargetType �� Property �� Value��%^   '��       �� 5"      " ��6    .�5#      ��  �@��CornerRadius$ 5��6=   5$      5%      5&      5'      5(      �� 5)   
   $
��Left=�6   $	��262��6+   $��
573,13,0,0q�68   $	��Top=�6L   $	��409��6d   $��	/Icon.png��6p   $��3��6�   5*      5+      