   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfApp1.EditPagesWpfApp1.EditPages  /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad       WpfApp1.EditPages.EditFoam   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:WpfApp1.EditPages     .kepadclr-namespace:WpfApp1.Keypad       � Title$  EditFoam��$	��600��$
��1024��.��� ���#E7ECEF.+��� ���#DAE2E7$
��1024��$��30��$��0,0,0,1q�
���   �$	��Top=�._�� .+��� $��28��$��28��$
��Left=�$��0,2,0,0q�
������ $��0q�.���� $A��;pack://application:,,,/WpfApp1;component/images/options.png���� $��MECHOSPHERE��$��5,0,5,0q�$��20(�$��Inter)�$��Regular"�$
��Left=�$��41,0,0,0q�$	��152��$��24��$��Center=��� -    ��Name$ ClockTextBlock��$��Inter)�$��Regular"�$��20(�$	��106��$��24��$
��Left=�$��	463,3,0,0q�$��Center=��� $=��7pack://application:,,,/WpfApp1;component/images/usb.png��$��28��$��28��$��Right=�$��	0,-1,36,1q��� $>��8pack://application:,,,/WpfApp1;component/images/wifi.png��$��28��$��28��$��Right=�$��0,-1,2,1q��� $��	EDIT FOAM��$	��200��$��50��$
��Left=�$	��Top=�$��0,30,0,0q�$��25(�$��Regular"� J� Opacity$	 0.7Q�$��Center=�$��Center=��� $��1��
���   �$��
0,79,0,504q�$	 0.3Q���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�$��0,100,0,400q�.+�d� -    d�Name$
 None��$	��133��$��50��$
��None��$��20(�����  �� 
TargetType" d�.R�� .+���  ��Name$ border��
�������$��2,2,1,2q� �� CornerRadius$ 5,0,0,5��$	 0.5Q�����  �� Color$ #E7ECEF���� $��Center=�$��Center=�	 �� Triggers	 P� 
 P� Property
 �@d�	IsChecked P� Value$
 TrueY�. ���  �� 
TargetName$ border�� �� Property � �� �� Value �#C33333d� -   $ AntiFoam��$	��133��$��50��$��AntiFoam��$��20(����� " d�.R�� .+��� $ border��
�������$��1,2,1,2q�$ 0��$	 0.5Q����� $ #E7ECEF���� $��Center=�$��Center=�	 P� 
 �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333d� -   $ Level��$	��133��$��50��$��Level��$��20(����� " d�.R�� .+��� $ border��
�������$��1,2,2,2q�$ 0,5,5,0��$	 0.5Q����� $ #E7ECEF���� $��Center=�$��Center=�	 P� 
 �@d�	IsChecked$
 TrueY�. ��� $ border�� � �� �#C33333�� -    ��Name$ ParametersContent��$��	0,200,0,0q�$��Center=�$	��Top=� � 	Resources    AntiFoamTemplate ��Name ��Name �� TextWrapping
 � Tag  LevelTemplate��%"  &	        &	     �� .R��� $ AntiFoamPanel��$��20q�.+�� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��	DOSE TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ AntiFoamDoseTime��-   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400��� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��	WAIT TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ AntiFoamWaitTime��-   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400��� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��
ALARM TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ AntiFoamAlarmTime��-   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400���� .R��� $ 
LevelPanel��$��20q�.+�� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��	DOSE TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ LevelDoseTime��-	   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400��� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��	WAIT TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ LevelWaitTime��-
   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400��� $��Center=�$
��0,10q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� ���#c74d4d$ 5,0,0,5��$	��120��$��40��$ 1Q�$��1q�
�������._�� .+��� $��
ALARM TIME��$��Center=�$	��Top=�$��19(�$��0,-5,0,0q�
���   �$��Regular"��� $��(Second)��$��Center=�$��Bottom=�$	 0.6Q�$��15(�$��0,0,0,-6q�
���   �$��Regular"��� ���#E7ECEF$ 0,5,5,0��$	��120��$��40��$��0,0,0,0q�$ 1Q�
�������$��1q�._��� $ LevelAlarmTime��-   
������ $��20(�$��Regular"�$��Center=�$��Center=�$
 Wrap=�$ 0,1400��� $	��108��$��Bottom=�.+��� ���#AF0101$��0,0,0,0q�$��1��$	��Top=��� -   $	��200��$��50��$��0,0,80,0q�$��Right=�$��Center=����� " ��.R�� .+��� ���#AF0101$ 5�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ #808080��   	Direction$	 270Q�   ShadowDepth$ 3Q�   
BlurRadius$ 5Q��� $��Center=�$��Center=�.���� $��OK��$��30(����#FFFFFF$��Center=�$��Center=��� -   $	��200��$��50��$��	0,0,292,0q�$��Right=�$��Center=����� " ��.R�� .+��� ���#E7ECEF$ 5��   $ #808080��$	 270Q�$ 3Q�$ 5Q��� $��Center=�$��Center=�.���� $��CANCEL��$��30(����#000000$��Center=�$��Center=�� -    �Name$ KeypadPopup��$��Center=�$��False��.��   WpfApp1.Keypad.KeyPad  -     Name$ KeypadControl��