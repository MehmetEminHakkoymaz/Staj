   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  
MainWindow��$
��1024��$	��600�����#E7ECEF.���  � ColumnDefinitions �� $
��257* ��� $
��767* �.+�� -    �Name$ TopGrid��$	��Top=�$��	0,-30,0,0q� K� ZIndex$	 100��$��2�� � RowDefinitions �� $��30 ��� $
��Auto �.+��� ���#DAE2E7$��0��$
��1024��$��30��$��0,0,0,1q�
���   �._�� .+��� -    ��Name$ SettingsButtonClk��$��28��$
��Left=�$��0,2,0,-1q�
������ $��0q�.���� $/��)pack://application:,,,/images/options.png���� $��MECHOSPHERE��$��5,0,5,0q�$��20(�$��Inter)�$��Regular"�$
��Left=�$��41,0,0,0q�$	��152��$��24��$��Center=��� -    ��Name$ ClockTextBlock��$��Inter)�$��Regular"�$��20(�$	��106��$��24��$
��Left=�$��	463,3,0,0q�$��Center=��� -   $ CloseAppButton��$��53��
���  ��
�������
���  ��$
��Left=�$��	840,0,0,0q�����  �� 
TargetType" �� �� Triggers P� 	 P� Property	 �@J�IsMouseOver
 P� Value$

 True��. ���  �� Property � �� �� Value �#C33333.��� .+��� $	��App��$��16(�
�������$��Center=�$��Center=��� -   $ ShutdownButton��$��53��
���  ��
�������
���  ��$
��Left=�$��	780,0,0,0q����� " �� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#C33333.��� .+�F� ����� � ��- ��Dk ��- ��- �Dk ��-  � ���- ��Dk @o@ � @o@��@o@@o@�@o@ � �@o@�Dk ���-  � ��- � � ��!���(\�B @��!@KL 0(� @KL  � S@KL ��(\�B @��(\�B @@KL  � @KL �0(� @KL ��!��(\�B @��! � ���!0(� 0(� ��! � ��!� �  z � �  $� ����  � �@T�  � 	
�������$��2�� �� StrokeLineJoin$ Round=�$��20��$��20���� -   $ LogoutButton��$��Logout��$
��Bold"�$��53��
���  ��
�������
���  ��$
��Left=�$��	901,0,0,0q����� " �� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#C33333�� $+��%pack://application:,,,/images/usb.png��$��28��$��28��$��Right=�$��	0,-1,36,1q��� $,��&pack://application:,,,/images/wifi.png��$��28��$��28��$��Right=�$��0,-1,2,1q��� -   $ TogglePopupButton��$��1��$��30��$��16�����#DAE2E7$	��Top=�$��Center=�$��2��
 � Tag$> 8pack://application:,,,/WpfApp1;component/images/Down.png������  �� 
TargetType" ��.R��� #��z��#��z��#��z�� �� CornerRadius$ 0,0,5,5��._�� .+��� $��23��$��23��$��Center=�$��Center=�����  �� Path$	 Tag� �� RelativeSource ��  �� AncestorType" ��� $��2�� �� $	��204 ��� $��1 ��� $
��730* ��� $	��89* � �� $��* ��� $��95 �.+��� $��0�����#D6D6D6$��2q�$	��204��$	��505��$	��Top=�$��2��._��� $	��200��$	��501��$	��Top=�.+��� -   $��
FAVOURITES��$��64��$	��204�����	#FFE7ECEF���	#FF272932���	#FFD6D6D6$��20(��� -	   $
��MAIN��$��64��$	��204�����	#FFE7ECEF���	#FF272932���	#FFD6D6D6$��20(��� -
   $��EXTENDED��$��64��$	��204�����	#FFE7ECEF���	#FF272932���	#FFD6D6D6$��20(��� -   $��EXIT GAS��$��64��$	��204�����	#FFE7ECEF���	#FF272932���	#FFD6D6D6$��20(��� -   $��PUMPS��$��64��$	��204�����	#FFE7ECEF���	#FF272932���	#FFD6D6D6$��20(��� $��1�����#E7ECEF$��1��$	��505��$	��Top=�$��2���� -    ��Name$ contentArea��$��2��$��0��$	��819��$	��505��$	��Top=�$��2��$��2��� $��0��$��4��$��1��$��95��$��Center=�$
��1024��.+��� ���#AF0101$��0,0,0,0q�$��1��$	��Top=��� ���#E7ECEF���#D6D6D6$��	2,1.5,2,2q�$��0,1,0,0q�._�� $��94��$��Center=�.+��� -   $ clockTextBlock��$��00:00:00�� �� TextAlignment$ Center=�$��40(�$��SemiBold"����#272932$��41,0,0,0q�$
��Left=�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ #808080��   	Direction$	 270Q�   ShadowDepth$ 3Q�   Opacity$	 0.4Q��� -   $ SampleButton��$y�	Collapsed=�$	��175��$��50��$
��Left=�$��Center=�$��	243,0,0,0q����� " ��.R�� .+��� ���#E7ECEF$ 5��   $ #808080��$	 270Q�$ 3Q�   
BlurRadius$ 5Q��� $��Center=�$��Center=�.���� -   $ SampleButtonText��$��1.Sample��$��20(����#272932$��Center=�$��Center=��� -   $ EditViewButton��$	��175��$��50��$��Center=�$��Center=����� " ��.R�� .+��� ���#E7ECEF$ 5��   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�.���� $��	Edit View��$��20(����#272932$��Center=�$��Center=��� -   $ StartButton��$	��250��$��50��$��0,0,34,0q�$��Right=�$��Center=�$y�	Collapsed=�$ 1������ " ��.R�� .+��� ���#AF0101$ 5��   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�.���� $��START��$��30(����#FFFFFF$��Center=�$��Center=��� -   $ FirstStartButton��$	��250��$��50��$��0,0,34,0q�$��Right=�$��Center=�$ 2������ " ��.R�� .+��� ���#AF0101$ 5��   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�.���� $��START��$��30(����#FFFFFF$��Center=�$��Center=��� -   $ 
StopButton��$	��250��$��50��$��0,0,34,0q�$��Right=�$��Center=�$ 0��$y�	Collapsed=����� " ��.R�� .+��� ���#AF0101$ 5��   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�.���� $
��STOP��$��30(����#FFFFFF$��Center=�$��Center=��� $
��Time��$
��Left=�$��37,4,0,0q�$	��Top=�$��15(�$��Regular"�