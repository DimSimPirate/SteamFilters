   M S B A M L   `   `   `  ����G  CSteamFilters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SteamFiltersSteamFilters  5          SteamFilters.Home   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:SteamFilters     5        N� Title$
  Home��5	       � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $Q Kpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml?�6%   %    5      .��5      � 
���   � � RowDefinitions 5   
   �� 5      $	��300 �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��65��6    �� Padding$ 5q�6*   ���	#4E87D4AA$��Bottom=�6M   $��	0,0,0,226q�6h   ._�5      �� $��Center=�6   
�������$��5,0,0,5q�6B   $��30(�6S   .��6a   Login6h   5      �� 5   
   -   5        $��UsernameTextBox��5   I   $��Bottom=�6   $��0��6-   $��50��6=   $��	0,0,0,176q�6`   $#��Enter your Steam username ...��6s   ���	#AAAAAAAA
�������6�   �� 5   
   -   5        $��Bottom=�5      $��0��6,   $��
10,0,0,136q�6<   $	��125��6P   $��25��6\   $
��Left=�6h   $
��View��6�   �� 5    
   $	��Top=�6   $��Saved Players��6(   ���	#AAAAAAAA
�������$��1��6j   $��16(�6w   �� 5!   
   -   5        $��peopleListBox��5!   *   $	��Top=�6   $��0��6?   $��2��6O   ���	#AAAAAAAA$��0,30,0,0q�6s   $	��200��6�   5"      �� 5#   
   -   5        $	��Top=�5#      $��0��6)   $��2��69   $��
10,252,0,0q�6F   $	��125��6Z   $��25��6f   $
��Left=�6r   .��6�   View6�   �� 5$   
   -   5        $	��Top=�5$      $��0��6)   $��1��69   $��140,252,0,0q�6F   $	��125��6[   $��25��6g   $
��Left=�6s   .��6�   Delete6�   5%      5&      