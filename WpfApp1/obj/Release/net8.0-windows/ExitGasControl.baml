   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad      WpfApp1.ExitGasControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     'localclr-namespace:WpfApp1     .kepadclr-namespace:WpfApp1.Keypad     .��� ���#E7ECEF.+�� $��50��$	��Top=�.+��� $��	PARAMETER��$��Center=�$��
85,0,0,-20q�$	��122��$��27��$
��Left=�  J� Opacity$	  0.2Q�$��SemiBold"�$��5,0,5,0q�$��20(��� $��VALUE��$
��Left=�$��331,0,0,-20q�$��Center=�$	  0.2Q�$��SemiBold"�$��66��$��27��$��0,0,0,0q�$��20(��� $��SETPOINT��$
��Left=�$��Center=�$��561,0,0,-20q�$	��101��$��27��$��0,0,0,0q�$��SemiBold"�$	  0.2Q�$��20(��� ���	#FFD6D6D6$��2,2,2,3q�$	��815���� $��Center=�$��45��$	��Top=�$	��815��$��0,50,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -    �Name$ 	Turbidity��.+��� $��Exit Gas O₂��$��Center=�$
��Left=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -    ��Name$ ExitTurbidityValue��$��5��$
��Left=�$��	356,0,0,0q�$��Center=�$��20(�$	  0.7Q��� $
��Left=�$��	545,0,0,0q�$	��127��$��30�� �� CornerRadius$ 5��
�������$��1q�._��� -    ��Name$ ExitTurbidityTarget�� �� TextWrapping$
 Wrap=�$�� ��
������ $��0q� �� TextAlignment$ Center=�$��0q�$��20(�$	  0.7Q�
 � Tag$ 0,100���� -    ��Name$  conditionalButtonTurbidity��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� 	 �� 
TargetType"	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� �� $��Center=�$��45��$	��Top=�$	��815��$��0,95,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ Balance��.+��� $��Exit Gas CO₂��$
��Left=�$��Center=�$��84,0,0,0q�$��Center=�$	��121��$��24��$��20(�$��0,0,0,0q��� -   $ ExitBalanceValue��$��5��$
��Left=�$��	356,0,0,0q�$��Center=�$��20(�$	  0.7Q��� $
��Left=�$��	545,0,0,0q�$	��127��$��30��$ 5��
�������$��1q�._��� -   $ ExitBalanceTarget��$
 Wrap=�$�� ��
������ $��0q�$ Center=�$��0q�$��20(�$	  0.7Q�$ 0,100���� -   $ conditionalButtonBalance��$��30��$��30��$��20,0,0,0q�$
��Left=�$��Center=�$y�	Collapsed=����	#FFAF0101���� "	 ��.R��� #��z��$ 5��._��� $��Center=�$��Center=�.���� � -	   $ 	RightGrid��$	��341��$	��448��$��Right=�$	��Top=�$��0,50,-325,0q�.+��� ���#E7ECEF���#D6D6D6$��2q�$��16,0,0,0q�._�� .+�� .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��10,5,244,409q�._��� -
   
 ��Name$
 canvas1��$��66.87��$��30��.+�A� -    A�Name$ 	ellipse14��$��20��$��20��
������ ���#D6D6D6$��1�� �� Left$ 6��
 �� Top$ 3���� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,5,21,409q�� � .+��� $��66.87��$��30��$ 15��
������ 
���   �$��1q�$��11,50,243,364q�._��� -   $
 canvas2��$��66.87��$��30��.+�A� -   $ 	ellipse15��$��20��$��20��
������ ���#D6D6D6$��1��$ 6��$ 3���� $?��9pack://application:,,,/WpfApp1;component/images/infoo.png��$��30��$��30��$��270,50,21,364q��� -   $ ToggleRightGridButton��$��16��$��30��$
��Left=�$	��Top=�$��	0,209,0,0q�$> 8pack://application:,,,/WpfApp1;component/images/Left.png������ "	 ��.R��� #��z��#��z��#��z��$ 5,0,0,5��._�� .+��� $��23��$��23��$��Center=�$��Center=�����  �� Path$	 Tag� �� RelativeSource ��  �� AncestorType" ��� -    �Name$ KeypadPopup��$��Center=�$��False��.��   WpfApp1.Keypad.KeyPad  -     Name$ KeypadControl��