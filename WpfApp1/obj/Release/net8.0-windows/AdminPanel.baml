   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.AdminPanel   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  
AdminPanel��$	��600��$
��1024��.���  � RowDefinitions �� $��50 ��� $��* �.+�� $��0�����#272932.+��� -    ��Name$ 
BackButton��$
��Left=�$	��100��$��40��$
��10,5q�
������ 
�������
�������.����  �� Orientation$ 
Horizontal=�.+��� $	��←��$��20(�$��Center=�$��0,0,5,0q��� $
��Back��$��16(�$��Center=�� $��1�� � ColumnDefinitions �� $��* ��� $	��300 �.+�&   System.Windows.Controls.DataGrid  -     Name$ 	UsersGrid��   AutoGenerateColumns$ False��$��0��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Username��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *
Username+  $
 Role��	 �� *Role+�� $��1��$��10q�.+��� $��	Username:���� -   
 ��Name$
 UsernameTextBox���� $��	Password:��G� -    G�Name$ PasswordBox���� $��Role:���� -    ��Name$ RoleComboBox��.@��� .��user�� .��guest�� -   $��Add User��$
��0,10q��� -   $��Update User��$
��0,10q��� -   $��Delete User��$
��0,10q�