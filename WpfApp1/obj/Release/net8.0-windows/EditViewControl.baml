   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.EditViewControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     'localclr-namespace:WpfApp1     .��� ���#E7ECEF.+�� $��50��$	��Top=�.+��� $$��SELECTION OF MAX. 10 FAVORITES��$��Center=�$��65,0,0,0q�$	��357��$��26��$
��Left=�  J� Opacity$	  0.7Q�$��SemiBold"�$��5,0,5,0q�$��20(��� $��Center=�$��25,0,0,0q�$��40��$��37��$
��Left=���F� a�������    �@ɢ�р\��� �@�e��q���lр���,�@o@��l� H�@�eр� ���S�	$	  0.7Q�
���   �$��2��
������  �� Stretch$ Uniform=�$��29��$��27���� -    ��Name$ SelectedCountTextBlock��$��Center=�$��Right=�$	��250��$��30��$	  0.7Q�$��10q�$��20(�$��Medium"��� ���	#FFD6D6D6$��2,2,2,3q�$	��815���� $
��Left=�$��50��$	��Top=�$	��266��$��0,50,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -    �Name$ MainTemperature�� � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��Temperature��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��	0,100,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ MainStirrer�� �� $
��Auto ��� $��* �.+��� $��Stirrer��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��	0,150,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ MainpH�� �� $
��Auto ��� $��* �.+��� $��pH��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��	0,200,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ MainpO2�� �� $
��Auto ��� $��* �.+��� $	��pO2��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��	0,250,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ MainFoam�� �� $
��Auto ��� $��* �.+��� $
��Foam��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��
266,50,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ ExtendedTurbidity�� �� $
��Auto ��� $��* �.+��� $��Turbidty��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��266,100,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ ExtendedBalance�� �� $
��Auto ��� $��* �.+��� $��Balance��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��266,150,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -	   $ ExtendedAirFlow�� �� $
��Auto ��� $��* �.+��� $��AirFlow��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��266,200,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -
   $ ExtendedGas2Flow�� �� $
��Auto ��� $��* �.+��� $��Gas2Flow��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��
532,50,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ ExitGasTurbidity�� �� $
��Auto ��� $��* �.+��� $��Exit Gas O₂��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��532,100,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ ExitGasBalance�� �� $
��Auto ��� $��* �.+��� $��Exit Gas CO₂��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� $
��Left=�$��50��$	��Top=�$	��266��$��	0,300,0,0q����	#FFD6D6D6$��2,2,2,2q�._�� -   $ 	MainRedox�� �� $
��Auto ��� $��* �.+��� $��Redox��$��Center=�$��20(�$	  0.7Q�$��1��$��55,0,0,0q��� -    ��Name$ CheckBoxContainer��$��Center=�$��0,0,0,0q�$	��800��$	��400��.+��� -    ��Name$ Temperature��
 � Tag$ 1��$	��Top=�$
��Left=�$��5,3,0,0q�$��40��$��40������  �� 
TargetType" ��.R��� $��40��$��40��	 �� CornerRadius$	 5��
�������$��1q�._�� .+��� 
�������
 �� RadiusX$
 5�� �� RadiusY$ 5���� $��Center=�$��Center=�F�  F�Name$ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� �� Triggers P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  �� 
TargetName$ 	CheckMark�� �� Property � �� �� Value �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ Stirrer��$ 2��$	��Top=�$
��Left=�$��5,10,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ pH��$ 4��$	��Top=�$
��Left=�$��5,10,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $	 pO2��$ 8��$	��Top=�$
��Left=�$��5,10,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $
 Foam��$	 256��$	��Top=�$
��Left=�$��5,10,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ 	Turbidity��$	 512��$	��Top=�$
��Left=�$��271,-240,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ Balance��$
 1024��$	��Top=�$
��Left=�$��271,-190,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ AirFlow��$
 2048��$	��Top=�$
��Left=�$��271,-140,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ Gas2Flow��$
 4096��$	��Top=�$
��Left=�$��271,-90,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ ExitTurbidity��$
 8192��$	��Top=�$
��Left=�$��537,-240,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ ExitBalance��$ 16384��$	��Top=�$
��Left=�$��537,-190,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=��� -   $ Redox��$ 262144��$	��Top=�$
��Left=�$��5,10,0,0q�$��40��$��40������ " ��.R��� $��40��$��40��$	 5��
�������$��1q�._�� .+��� 
�������$
 5��$ 5���� $��Center=�$��Center=�F� $ 	CheckMark��"���Ѐ��  -1� -1���р����� 	
������ $��2��$y�	Collapsed=� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	CheckMark�� � �� �#AF0101�� $ 	CheckMark�� � y�$ Visible=�P�  �@d�	IsChecked$ FalseY�. ��� $ 	CheckMark�� � ��
 ���� �� $ 	CheckMark�� � y�$ 	Collapsed=�