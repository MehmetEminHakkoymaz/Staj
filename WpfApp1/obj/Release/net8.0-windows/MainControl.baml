   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad      WpfApp1.MainControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     'localclr-namespace:WpfApp1     .kepadclr-namespace:WpfApp1.Keypad     -   .��� ���#E7ECEF.+�� $��50��$	��Top=�.+��� $��	PARAMETER��$��Center=�$��
85,0,0,-20q�$	��122��$��27��$
��Left=�  J� Opacity$	  0.2Q�$��SemiBold"�$��5,0,5,0q�$��20(��� $��VALUE��$
��Left=�$��331,0,0,-20q�$��Center=�$	  0.2Q�$��SemiBold"�$��66��$��27��$��0,0,0,0q�$��20(��� $��SETPOINT��$
��Left=�$��Center=�$��561,0,0,-20q�$	��101��$��27��$��0,0,0,0q�$��SemiBold"�$	  0.2Q�$��20(��� ���	#FFD6D6D6$��2,2,2,3q�$	��815���� $��Center=�$��45��$	��Top=�$	��815��$��0,50,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -    �Name$ Temperature��.+��� $��Temperature��$��Center=�$
��Left=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -    ��Name$ TemperatureValue��$��5��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q��� $
��Left=�$��	545,0,0,0q�$	��127��$��30�� �� CornerRadius$ 5��
�������$��1q�._��� -    ��Name$ TemperatureTarget�� �� TextWrapping$
 Wrap=�$�� ��
������ $��0q� �� TextAlignment$ Center=�$��0q�$��20(�$	  0.7Q�
 � Tag$ 0,100���� -    ��Name$ conditionalButton��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� 	 �� 
TargetType"	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��0,95,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ Stirrer��.+��� $��Stirrer��$��Center=�$
��Left=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ StirrerValue��$��4��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q��� -   
 ��Name$
 StirrerTargetBorder��$
��Left=�$��	545,0,0,0q�$	��127��$��30��$ 5��
�������$��1q�$y�Visible=�._��� -	   $ StirrerTarget��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$��0q�$��20(�$	  0.7Q�$ 15,60���� -
   $ conditionalButtonStirrer��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��	0,140,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ pH�� � ColumnDefinitions �� $
��230* ��� $
��581* �.+��� $��pH��$
��Left=�$��Center=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ pHValue��$��3��$
��Left=�$��	101,0,0,0q�$��Center=�$��1��$��20(�$	  0.7Q��� $
��Left=�$��	315,6,0,5q�$	��127��$��30��$ 5��
�������$��1q�$��1��._��� -   $ pHTarget��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$��0q�$��20(�$	  0.7Q�$ 0,1000����  �� Orientation$ 
Horizontal=�$��Right=�$��Center=�$��0,0,50,0q�$��1��.+��� -    ��Name$ TemperatureIndicator��$��25��$��16(�$��Center=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *IsHighTemperature+ �� Value True.a���  �� Property � �� �� Value
 �  ����  �� *IsWarningTemperature+ True.a���  � �� �#F6BE00��  �� *IsNormalTemperature+ True.a���  � ��
 � � ��� $	��°C��$��16(�$��Center=�$��2,0,0,0q��� -   $ conditionalButtonpH��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��	0,185,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $	 pO2��.+��� $	��pO2��$
��Left=�$��Center=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ pO2Value��$��2��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q��� $
��Left=�$��	545,0,0,0q�$	��127��$��30��$ 5��
�������$��1q�._��� -   $ 	pO2Target��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$��0q�$��20(�$	  0.7Q�$ 0,1000���� $ 
Horizontal=�$��Right=�$��Center=�$��0,0,50,0q�.+��� -   $ pO2TemperatureIndicator��$��25��$��16(�$��Center=����� " �� ��  �� *IspO2HighTemperature+ True.a���  � ��
 �  ����  �� *IspO2WarningTemperature+ True.a���  � �� �#F6BE00��  �� *IspO2NormalTemperature+ True.a���  � ��
 � � ��� $	��°C��$��16(�$��Center=�$��2,0,0,0q��� -   $ conditionalButtonpO2��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��	0,230,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $
 Foam��.+��� $
��Foam��$
��Left=�$��Center=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ 	AboveFoam��$"��Foam is above expected level��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q�$y�	Collapsed=�
���\\���� -   $ 	UnderFoam��$��Foam at expected level��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q�$y�Visible=��� -   $ conditionalButtonFoam��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��	0,275,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ Redox��.+��� $��Redox��$
��Left=�$��Center=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ 
RedoxValue��$��9��$
��Left=�$��	331,0,0,0q�$��Center=�$��20(�$	  0.7Q��� $
��Left=�$��	545,0,0,0q�$	��127��$��30��$ 5��
�������$��1q�._��� -   $ RedoxTarget��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$��0q�$��20(�$	  0.7Q�$ 0,1000���� $ 
Horizontal=�$��Right=�$��Center=�$��0,0,50,0q�.+��� -   $ RedoxTemperatureIndicator��$��25��$��16(�$��Center=����� " �� ��  �� *IsRedoxHighTemperature+ True.a���  � ��
 �  ����  �� *IsRedoxWarningTemperature+ True.a���  � �� �#F6BE00��  �� *IsRedoxNormalTemperature+ True.a���  � ��
 � � ��� $	��°C��$��16(�$��Center=�$��2,0,0,0q��� -   $ conditionalButtonRedox��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� � -   $ 	RightGrid��$	��341��$	��448��$��Right=�$	��Top=�$��0,50,-325,0q�.+��� ���#E7ECEF���#D6D6D6$��2q�$��16,0,0,0q�._�� .+�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,5,244,409q�._��� -    ��Name$ canvas1��$��66.87��$��30��.+�A� -     A�Name$ ellipse1��$��20��$��20��
������ ���#D6D6D6$��1�� �� Left$ 6��
 �� Top$ 3���� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,5,21,409q�� � .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��11,50,243,364q�._��� -!   $ canvas2��$��66.87��$��30��.+�A� -"   $ ellipse2��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,50,21,364q�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,95,244,319q�._��� -#   $ canvas3��$��66.87��$��30��.+�A� -$   $ ellipse3��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� $@��:pack://application:,,,/WpfApp1;component/images/target.png��$��30��$��30��$��122,95,169,319q��� -%   $?��9pack://application:,,,/WpfApp1;component/images/editt.png��$��30��$��30��$��197,95,94,319q��� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,95,21,319q�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,140,244,274q�._��� -&   $ canvas4��$��66.87��$��30��.+�A� -'   $ ellipse4��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� $@��:pack://application:,,,/WpfApp1;component/images/target.png��$��30��$��30��$��122,140,169,274q��� -(   $?��9pack://application:,,,/WpfApp1;component/images/editt.png��$��30��$��30��$��197,140,94,274q��� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,140,21,274q�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,185,244,229q�._��� -)   $ canvas9��$��66.87��$��30��.+�A� -*   $ ellipse9��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� -+   $?��9pack://application:,,,/WpfApp1;component/images/editt.png��$��30��$��30��$��197,185,94,229q��� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,185,21,229q�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,230,244,184q�._��� -,   $ canvas19��$��66.87��$��30��.+�A� --   $ 	ellipse19��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� -.   $?��9pack://application:,,,/WpfApp1;component/images/editt.png��$��30��$��30��$��197,230,94,184q��� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,230,21,184q��� -/   $ ToggleRightGridButton��$��16��$��30��$
��Left=�$	��Top=�$��	0,209,0,0q�$> 8pack://application:,,,/WpfApp1;component/images/Left.png������ "	 ��.R��� #��z��#��z��#��z��$ 5,0,0,5��._�� .+��� $��23��$��23��$��Center=�$��Center=�����  �� Path$	 Tag� �� RelativeSource ��  �� AncestorType" ��� -0    �Name$ KeypadPopup��$��Center=�$��False��.��   WpfApp1.KePad  -1     Name$ KeypadControl��