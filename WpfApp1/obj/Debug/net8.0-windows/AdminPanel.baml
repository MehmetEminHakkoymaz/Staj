   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.AdminPanel   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
AdminPanel��5   	   $	��600��6   $
��1024��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   � $��0��6   ���#272932.+�5      �� -   5         ��Name$ 
BackButton��5      $
��Left=�5      $	��100��5      $��40��5      $
��10,5q�5      
������ 
�������
�������.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $	��←��6    $��20(�5      $��Center=�5      $��0,0,5,0q�5      �� 5      $
��Back��6    $��16(�5       $��Center=�5!      5"      5#      5$      � 5'   
   $��1��6    � ColumnDefinitions 5(      �� 5)      $��* �6#   �� 5*      $	��300 �6#   .+�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ 	UsersGrid��5-         AutoGenerateColumns$ False��5.      $��0��5/         Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ Username��6)   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6;   �� *
Username+  52      $
 Role��6)   	 67   �� *Role+54      �� 56      $��1��6   $��10q�6)   .+�57      �� $��	Username:��6   �� 58      -   5        
 ��Name$
 UsernameTextBox��58      �� 59      $��	Password:��6   G� 5:      -   5         G�Name$ PasswordBox��5:      �� 5;      $��Role:��6   �� 5<      -   5         ��Name$ RoleComboBox��5<      .@�5=      �� .��6#   user6)   �� 5>      .��6#   guest6*   5?      �� 5@      -   5        $��Add User��5@      $
��0,10q�6B   �� 5A      -   5        $��Update User��5A      $
��0,10q�6H   �� 5B      -   5        $��Delete User��5B      $
��0,10q�6H   5C      5D      5E      5F      