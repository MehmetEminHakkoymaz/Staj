   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.TableNameDialog   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  Enter Project Name��$	��600��$
��1024�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � 	Resources    KeyboardButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius
 �� Triggers P� Property P� Value  KeyboarddButtonStyle  KeyboardSpecialButtonStyle �� BasedOn��%Y  &	        &	 n    &	 �    7   �� " ��.���  � ��$ 60����  � ��$ 60����  � ��$ 5q���  � ��$ 24(���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$	 5��$��0q�._��� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ���  � �� �#3E4049P�  � ��$
 True��. ���  � �� �#1A1B21�� " ��.���  � ��$	 120����  � ��$ 60����  � ��$ 5q���  � ��$ 24(���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$	 5��$��0q�._��� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ���  � �� �#3E4049P�  � ��$
 True��. ���  � �� �#1A1B21�� 8   " ��.���  � ��$ 80����  � ��$ 16(���  � �� �#AF0101.��� ���#E7ECEF � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��ENTER PROJECT NAME��$��30(�$
��Bold"�$��0,5,0,5q�$��Center=��� $��1��
�������$��1q�$	 10��$	��400��$��60��$��0,0,0,0q�._��� -    ��Name$ TableNameTextBox��$��35(�$��0q�$��Center=�$��Center=�
������ �� -    ��Name$ WarningText��$��2��$+��%Invalid table name! Please try again.��
���  ��$��18(�$��Center=�$��0,5,0,0q�$y�	Collapsed=��� $��3�����#007BFF$��1q�$	 5��$��162,5,162,0q� �� Padding$ 5q�._��� $��Center=�$��Center=�.+��� $��Table Naming Rules:��$
��Bold"�$��16(�$��0,0,0,2q��� $
��Wrap=� J� Opacity$	 0.8Q� �� 
LineHeight$ 20���� $"�• Must start with a letter���� �� $)�#• Can contain letters and numbers���� �� $0�*• Length must be between 1-30 characters��� $��4��$	��700��$	��0,5q� �� $��* ��� $��* ��� $��* ��� $��* �.+�A� $��0�� A� Rows$ 1��.+��� -   #��[  $��1��$��3q��� -   #��[  $��2��$��3q��� -   #��[  $��3��$��3q��� -   #��[  $��4��$��3q��� -   #��[  $��5��$��3q��� -   #��[  $��6��$��3q��� -	   #��[  $��7��$��3q��� -
   #��[  $��8��$��3q��� -   #��[  $��9��$��3q��� -   #��[  $��0��$��3q�A� $��1��$ 1��.+��� -   #��[  $��Q��$��3q��� -   #��[  $��W��$��3q��� -   #��[  $��E��$��3q��� -   #��[  $��R��$��3q��� -   #��[  $��T��$��3q��� -   #��[  $��Y��$��3q��� -   #��[  $��U��$��3q��� -   #��[  $��I��$��3q��� -   #��[  $��O��$��3q��� -   #��[  $��P��$��3q�A� $��2��$ 1��.+��� -   #��[  $��A��$��3q��� -   #��[  $��S��$��3q��� -   #��[  $��D��$��3q��� -   #��[  $��F��$��3q��� -   #��[  $��G��$��3q��� -   #��[  $��H��$��3q��� -   #��[  $��J��$��3q��� -   #��[  $��K��$��3q��� -   #��[  $��L��$��3q�A� $��3��$ 1��.+��� -    #��[  $��Z��$��3q��� -!   #��[  $��X��$��3q��� -"   #��[  $��C��$��3q��� -#   #��[  $��V��$��3q��� -$   #��[  $��B��$��3q��� -%   #��[  $��N��$��3q��� -&   #��[  $��M��$��3q��� -'   #��[ $	��←��$��3q�� $��5��$��90��$��Bottom=�.+��� -(   $	��300��$��70��$��Center=�$��Center=����� " ��.R�� .+��� ���#007BFF$	 5�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ #808080��   	Direction$	 270Q�   ShadowDepth$ 3Q�   
BlurRadius$ 5Q��� $��Center=�$��Center=�.���� $��Create Table��$��35(����#FFFFFF$��Center=�$��Center=�