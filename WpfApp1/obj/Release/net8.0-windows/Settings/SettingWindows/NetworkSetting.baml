   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WpfApp1.Settings.SettingWindowsWpfApp1.Settings.SettingWindows  /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad  !clr-namespace:WpfApp1WpfApp1  4    .WpfApp1.Settings.SettingWindows.NetworkSetting   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:WpfApp1.Settings.SettingWindows     .kepadclr-namespace:WpfApp1.Keypad     )wpfapp1clr-namespace:WpfApp1       � 	Resources     ComboBoxToggleButton �� 
TargetType � ColumnDefinitions3  -System.Windows.Media.Effects.DropShadowEffect  DropShadowEffect   Color   	Direction   ShadowDepth   
BlurRadius   Opacity��%�   &	        &	 �     �� " d�.R��  �� �� $��20 �.+��� $��2��
������ F� $��1��
�������=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�$��Center=�  $ Black��$	 320Q�$ 5Q�$ 10Q�$	 0.5Q�.��� ���	#FFE7ECEF.+��� $��Center=�.+�� -    �Name$ ConType��$	��724��$��60��.+��� $��CONNECTION TYPE��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� -   	 ��Name$	 ConnectionType��
 �� Orientation$
 
Horizontal=�$��Right=�$��Center=�$��0,0,20,0q�.+�d� -    d�Name$ Ethernet��$	��160��$��50��$��Ethernet��$��20(� d� 	IsChecked$
 TrueY����� " d�.R�� .+���  ��Name$ border��
�������$��2,2,1,2q� �� CornerRadius$ 5,0,0,5�� J� Opacity$	 0.5Q�����  �� Color$ #E7ECEF���� $��Center=�$��Center=� �� Triggers P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  �� 
TargetName$ border�� �� Property � �� �� Value �#C33333d� -   $
 WLAN��$	��160��$��50��$
��WLAN��$��20(�$ FalseY����� " d�.R�� .+��� $ border��
�������$��1,2,2,2q�$ 0,5,5,0��$	 0.5Q����� $ #E7ECEF���� $��Center=�$��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333� -   $ Config��$	��724��$��60��.+��� $��CONFIGURATION��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
 
Horizontal=�$��Right=�$��Center=�$��0,0,20,0q�.+�d� -   $ Autodhcp��$	��160��$��50��$��
Auto(DHCP)��$��20(�$ FalseY����� " d�.R�� .+��� $ border��
�������$��2,2,1,2q�$ 5,0,0,5��$	 0.5Q����� $ #E7ECEF���� $��Center=�$��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333d� -   $ Manual��$	��160��$��50��$��Manual��$��20(�$
 TrueY����� " d�.R�� .+��� $ border��
�������$��1,2,2,2q�$ 0,5,5,0��$	 0.5Q����� $ #E7ECEF���� $��Center=�$��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333� -   $ IpAddressGrid��$	��724��$��60��.+��� $��
IP ADDRESS��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -	    ��Name$ IpAddressTextBox1�� �� TextWrapping$
 Wrap=�$	��192��
������ $��0q� �� TextAlignment$ Center=�$	��0,5q��� $
��Left=�$��	470,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -
   $ IpAddressTextBox2��$
 Wrap=�$	��168��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	555,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ IpAddressTextBox3��$
 Wrap=�$��1��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	640,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ IpAddressTextBox4��$
 Wrap=�$��1��
������ $��0q�$ Center=�$	��0,5q�� -   $ 
SubnetMask��$	��724��$��60��.+��� $��SUBNET MASK��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox1��$
 Wrap=�$	��255��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	470,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox2��$
 Wrap=�$	��255��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	555,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox3��$
 Wrap=�$	��255��
������ $��0q�$ Center=�$	��0,5q��� $
��Left=�$��	640,0,0,0q�$��60��$��35��$ 5��
�������$��1q�._��� -   $ SubnetMaskTextBox4��$
 Wrap=�$��0��
������ $��0q�$ Center=�$	��0,5q�� -   $
 Ssid��$	��724��$��60��.+��� $
��SSID��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
 
Horizontal=�$
��Left=�$��Center=�$��	385,0,0,0q�.+��� -    ��Name$ SsidComboBox��$	��270��$��50��$
��Left=�$��Center=�$��0,0,0,0q�$��20(�����  �� 
TargetType" ��.���  � �� �� " ��.R��� 
������ 
�������$��1q�$ 5��._�� .+���  �� Text#��z $��10,0,0,0q�$��Center=�d� #��[  $|�False��$��2�� ��  �� Path$ IsDropDownOpen� �� Mode$ TwoWay=� �� RelativeSource �� *TemplatedParent+�   �� IsDropDownOpen#��z  $��Bottom=�! � AllowsTransparency$
! True��.���� 
�������
�������$��1q�._��� $	��200��.���� " K� IsItemsHost$
" True���� -   # ��Name$# RefreshWifiButton��$��45��$��50��$��5,0,0,0q�.��F� �����PQ��` �@1� ��J ���  	=  �  	= � 	=  �   z  z � �  -1  z  z �P�  -1�yD
P-��� �PQ��� S0� �G�zT0@P�� �� � ��׀�[  � ���[ ��[ � � ��[ ���[ ��[ �`o� ��[ �� �f ���Q80@��v �@]� �ا � -1�ا � -1 	= �PQ��` 	
�������$��20��$��20��$ J� RenderTransformOrigin$$ 0.5,0.5&�z��� -   refreshIconRotation��� -   $ PASSWORD��$	��724��$��60��.+��� $��PASSWORD��$��25(�$��Regular"�$
��Left=�$��Center=�$��40,0,0,0q��� $
��Left=�$��	385,0,0,0q�$	��320��$��50��$ 5��
�������$��1q�._�G� -   % G�Name$% PasswordTextBox��
������ $��0q�$��Center=�$��Center=�$	��0,5q�$��20(�� -   & �Name$& KeypadPopup��$��Center=�$��False��.��   WpfApp1.Keypad.KeyPad  -   '  Name$' KeypadControl��� -   $& KeyboardPopup��$��Center=�( � 	StaysOpen$
( True��$
! True��.���� 
�������
�������$��1q�$ 5��) J� Effect#) [ ._�   WpfApp1.KeyboardPopup  -   *  Name$* CustomKeyboard���� -   $
��SAVE��$��20(�$��Regular"�$��Center=�$��	0,427,0,0q�$	��Top=�$��90��$��25������ " ��.R�� .+��� ���#AF0101$ 5��)   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�
�������