   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WpfApp1.Settings.SettingWindowsWpfApp1.Settings.SettingWindows  /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad  4    .WpfApp1.Settings.SettingWindows.NetworkSetting   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:WpfApp1.Settings.SettingWindows     .kepadclr-namespace:WpfApp1.Keypad     .��� ���	#FFE7ECEF.+��� $��Center=�.+�� -     �Name$  ConType��$	��724��$��60��.+��� $��CONNECTION TYPE��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� -    ��Name$ ConnectionType�� �� Orientation$ 
Horizontal=�$��Right=�$��Center=�$��0,0,20,0q�.+�d� -    d�Name$ Ethernet��$	��160��$��50��$��Ethernet��$��20(� d� 	IsChecked$
 TrueY�����  �� 
TargetType" d�.R�� .+���  ��Name$ border��
�������$��2,2,1,2q� �� CornerRadius$ 5,0,0,5�� J� Opacity$	 0.5Q����� 	 �� Color$	 #E7ECEF���� $��Center=�$��Center=�
 �� Triggers
 P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  �� 
TargetName$ border�� �� Property � �� �� Value �#C33333d� -   $
 WLAN��$	��160��$��50��$
��WLAN��$��20(����� " d�.R�� .+��� $ border��
�������$��1,2,2,2q�$ 0,5,5,0��$	 0.5Q����� $	 #E7ECEF���� $��Center=�$��Center=�
 P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333� -   $  Config��$	��724��$��60��.+��� $��CONFIGURATION��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $ 
Horizontal=�$��Right=�$��Center=�$��0,0,20,0q�.+�d� -   $ Autodhcp��$	��160��$��50��$��
Auto(DHCP)��$��20(�$
 TrueY����� " d�.R�� .+��� $ border��
�������$��2,2,1,2q�$ 5,0,0,5��$	 0.5Q����� $	 #E7ECEF���� $��Center=�$��Center=�
 P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333d� -   $ Manual��$	��160��$��50��$��Manual��$��20(����� " d�.R�� .+��� $ border��
�������$��1,2,2,2q�$ 0,5,5,0��$	 0.5Q����� $	 #E7ECEF���� $��Center=�$��Center=�
 P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333� -   $  	IPAddress��$	��724��$��60��.+��� $��
IP ADDRESS��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -	    ��Name$ IpAddressTextBox1�� �� TextWrapping$
 Wrap=�$�� ��
������ $��0q� �� TextAlignment$ Center=�$	��0,5q��� $
��Left=�$��	470,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -
   $ IpAddressTextBox2��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	555,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ IpAddressTextBox3��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	640,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ IpAddressTextBox4��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q�� -   $  
SubnetMask��$	��724��$��60��.+��� $��SUBNET MASK��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox1��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	470,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox2��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	555,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox3��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	640,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox4��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q�� -   $
  Ssid��$	��724��$��60��.+��� $
��SSID��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� -    ��Name$ SsidComboBox��$	��320��$��50��$
��Left=�$��Center=�$��	385,0,0,0q�� -   $  PASSWORD��$	��724��$��60��.+��� $��PASSWORD��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$	��320��$��50��$ 5��
�������$��1q�._��� -   $ PasswordTextBox��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$	��0,5q�� -    �Name$ KeypadPopup��$��Center=�$��False��.��   WpfApp1.Keypad.KeyPad  -     Name$ KeypadControl���� -   $
��SAVE��$��20(�$��Regular"�$��Center=�$��	0,427,0,0q�$	��Top=�$��90��$��25������ " ��.R�� .+��� ���#AF0101$ 5�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ #808080��   	Direction$	 270Q�   ShadowDepth$ 3Q�   
BlurRadius$ 5Q��� $��Center=�$��Center=�