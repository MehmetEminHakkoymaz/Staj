   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfApp1.EditPagesWpfApp1.EditPages  5   	   /clr-namespace:WpfApp1.KeypadWpfApp1.Keypad  5   	       WpfApp1.EditPages.EditpH   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:WpfApp1.EditPages     5   	   .kepadclr-namespace:WpfApp1.Keypad     5   	     � Title$  EditpH��5	   	   $	��600��6   $
��1024��6%   .��5      � ���#E7ECEF.+�5   
   �� ���#DAE2E7$
��1024��6&   $��30��63   $��0,0,0,1q�6?   
���   �$	��Top=�6m   ._�5      � .+�5      �� $��28��6   $��28��6$   $
��Left=�60   $��0,2,0,0q�6K   
������ $��0q�6u   .��5      �� $A��;pack://application:,,,/WpfApp1;component/images/options.png��6   5      �� 5      $��MECHOSPHERE��6   $��5,0,5,0q�6.   $��20(�6@   $��Inter)�6N   $��Regular"�6a   $
��Left=�6v   $��41,0,0,0q�6�   $	��152��6�   $��24��6�   $��Center=�6�   �� 5      -   5         ��Name$ ClockTextBlock��5      $��Inter)�64   $��Regular"�6G   $��20(�6\   $	��106��6j   $��24��6v   $
��Left=�6�   $��	463,3,0,0q�6�   $��Center=�6�   �� 5      $=��7pack://application:,,,/WpfApp1;component/images/usb.png��6   $��28��6Y   $��28��6d   $��Right=�6p   $��	0,-1,36,1q�6�   �� 5      $>��8pack://application:,,,/WpfApp1;component/images/wifi.png��6   $��28��6Z   $��28��6e   $��Right=�6q   $��0,-1,2,1q�6�   5      5      �� 5   
   $��EDIT pH��6   $	��200��6"   $��50��6.   $
��Left=�6:   $	��Top=�6U   $��0,30,0,0q�6m   $��25(�6   $��Regular"�6�    J� Opacity$	 0.7Q�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $��1��6   
���   �$��
0,79,0,504q�6-   $	 0.3Q�6A   �� 5   
   $��CASCADE��6   $	��120��6"   $��30��6.   $
��Left=�6:   $	��Top=�6U   $��	54,99,0,0q�6m   $��25(�6�   $ 1Q�6�   $��0,0,0,0q�6�   $��Center=�6�    J� RenderTransformOrigin$ 0.501,0.364&�6�   � 5   
   .+�5      ��  �� CornerRadius$ 5��6   ���	#FFE7ECEF
�������$��1q�6P   $��224,96,351,443q�6d   $��45��6|   ._�5      �� -   5         ��Name$ contentComboBox��5      $��25(�64   $��Regular"�6B   $��1,1,1,1q�6W   $��0,0,0,0q�6h   $��Center=�6z   .@�5      �� -   5         ��Name$
 None��5   #   $
��None��61   $��25(�6@   $��Regular"�6N   �� 5      -   5        $
 Base��5   #   $
��Base��62   $��25(�6A   $��Regular"�6O   �� 5       -   5        $
 Acid��5    #   $
��Acid��62   $��Regular"�6A   $��25(�6V   �� 5!      -   5        $ BaseAcid��5!   #   $��
Base->Acid��66   $��Regular"�6K   $��25(�6`   5"      5#      �� 5$      -   5         ��Name$ contentArea��5$      $
��Left=�62   $��224,147,0,357q�6M   $	��750��6d   $��80��6p    � 	Resources 5%         NoneTemplate  BaseTemplate	 �� Orientation
 ��Name �� TextWrapping
 � Tag  AcidTemplate  BaseAcidTemplate��%�  &	        &	      &	 {    &	 �    �� 5&      5(      �� 5+      .R�5,      �� $	 Vertical=�6%   $
��Left=�6<   $��Center=�6W   .+�5-      �� ���#c74d4d$ 5,5,0,0��6:   $	��120��6Q   $��40��6]   $ 1Q�6i   $��1q�5.   !   
�������._�5/   "   � .+�50   &   �� $
��Base��6,   $��Center=�6;   $	��Top=�6X   $��25(�51   )   $��	0,-10,0,0q�67   
���   �$��Regular"�6]   �� 52   &   $��(0-?)��6,   $��Center=�6<   $��Bottom=�6Y   $	 0.6Q�53   )   $��15(�67   $��0,0,0,-6q�6E   
���   �$��Regular"�6j   54   #   55      �� 56      ���#E7ECEF$ 0,0,5,5��6:   $	��120��6Q   $��40��6]   $��0,0,0,0q�6i   $ 1Q�57   !   
�������$��1q�6D   ._�58   "   �� $
 
pHBaseBase��6*   -   5        
������ $��20(�58   W   $��Regular"�6e   $��Center=�59   *   $��Center=�6L   $
 Wrap=�5:   F   $ 0,100��6Z   5;      5<      5=      �� 5@      .R�5A      �� $	 Vertical=�6%   $
��Left=�6<   $��Center=�6W   .+�5B      �� ���#c74d4d$ 5,5,0,0��6:   $	��120��6Q   $��40��6]   $ 1Q�6i   $��1q�5C   !   
�������._�5D   "   � .+�5E   &   �� $
��Acid��6,   $��Center=�6;   $	��Top=�6X   $��25(�5F   )   $��	0,-10,0,0q�67   
���   �$��Regular"�6]   �� 5G   &   $��(0-?)��6,   $��Center=�6<   $��Bottom=�6Y   $	 0.6Q�5H   )   $��15(�67   $��0,0,0,-6q�6E   
���   �$��Regular"�6j   5I   #   5J      �� 5K      ���#E7ECEF$ 0,0,5,5��6:   $	��120��6Q   $��40��6]   $��0,0,0,0q�6i   $ 1Q�5L   !   
�������$��1q�6D   ._�5M   "   �� $
 
pHAcidAcid��6*   -	   5        
������ $��20(�5M   W   $��Regular"�6e   $��Center=�5N   *   $��Center=�6L   $
 Wrap=�5O   F   $ 0,100��6Z   5P      5Q      5R      �� 5U      .R�5V      � .+�5W      �� $	 Vertical=�6)   $
��Left=�6@   $��Center=�6[   .+�5X   "   �� ���#c74d4d$ 5,5,0,0��6>   $	��120��6U   $��40��6a   $ 1Q�6m   $��1q�5Y   %   
�������._�5Z   &   � .+�5[   *   �� $
��Base��60   $��Center=�6?   $	��Top=�6\   $��25(�5\   -   $��	0,-10,0,0q�6;   
���   �$��Regular"�6a   �� 5]   *   $��(0-?)��60   $��Center=�6@   $��Bottom=�6]   $	 0.6Q�5^   -   $��15(�6;   $��0,0,0,-6q�6I   
���   �$��Regular"�6n   5_   '   5`   #   �� 5a   "   ���#E7ECEF$ 0,0,0,0��6>   $	��120��6U   $��40��6a   $��0,0,0,0q�6m   $ 1Q�5b   %   
�������$��1q�6H   ._�5c   &   �� $
 pHBaseAcidBase��6.   -
   5        
������ $��20(�5c   _   $��Regular"�6m   $��Center=�5d   .   $��Center=�6P   $
 Wrap=�5e   J   $ 0,100��6^   5f   #   5g      �� 5i      $	 Vertical=�6)   $��Center=�6@   $��Center=�6]   .+�5j   "   �� ���#c74d4d$ 5,5,0,0��6>   $	��120��6U   $��40��6a   $ 1Q�6m   $��1q�5k   %   
�������._�5l   &   � .+�5m   *   �� $
��Acid��60   $��Center=�6?   $	��Top=�6\   $��25(�5n   -   $��	0,-10,0,0q�6;   
���   �$��Regular"�6a   �� 5o   *   $��(0-?)��60   $��Center=�6@   $��Bottom=�6]   $	 0.6Q�5p   -   $��15(�6;   $��0,0,0,-6q�6I   
���   �$��Regular"�6n   5q   '   5r   #   �� 5s   "   ���#E7ECEF$ 0,0,5,5��6>   $	��120��6U   $��40��6a   $��0,0,0,0q�6m   $ 1Q�5t   %   
�������$��1q�6H   ._�5u   &   �� $
 pHBaseAcidAcid��6.   -   5        
������ $��20(�5u   _   $��Regular"�6m   $��Center=�5v   .   $��Center=�6P   $
 Wrap=�5w   J   $ 0,100��6^   5x   #   5y      5z      5{      5|      5}      5~      �� 5�   
   $ 10��6   ._�5�      � $	��225��6   $	��750��6    $��224,233,50,126q�6,   .+�5�      �� $	��419��6   $��30��6$   $��18,8,0,0q�60   $
��Left=�6B   $	��Top=�6]   $��Center=�6u   $��Center=�6�   $#��PID SETTING OF THE pH CASCADE��6�   $��25(�6�   $	 0.5Q�6�   $��0,0,0,0q�6�   �� 5�      $��P��6   $��20(�6$   $	 0.5Q�62   $��	50,63,0,0q�6@   $��0,0,0,0q�6S   $��13��6e   $��24��6p   $
��Left=�6|   $	��Top=�6�   �� 5�      $��I��6   $��20(�6$   $	 0.5Q�62   $��
50,115,0,0q�6@   $��0,0,0,0q�6T   $��8��6f   $��24��6p   $
��Left=�6|   $	��Top=�6�   �� 5�      $��I Limit��6   $��20(�6*   $	 0.5Q�68   $��
50,167,0,0q�6F   $��0,0,0,0q�6Z   $��57��6l   $��24��6w   $
��Left=�6�   $	��Top=�6�   �� 5�      $��Deadband��6   $��20(�6+   $	 0.5Q�69   $��
290,63,0,0q�6G   $��0,0,0,0q�6[   $��95��6m   $��24��6x   $
��Left=�6�   $	��Top=�6�   �� 5�      $��Neg. factor��6   $��20(�6.   $	 0.5Q�6<   $��290,115,0,0q�6J   $��0,0,0,0q�6_   $	��105��6q   $��24��6}   $
��Left=�6�   $	��Top=�6�   �� 5�      $��
Eval. Time��6   $��20(�6-   $	 0.5Q�6;   $��290,167,0,0q�6I   $��0,0,0,0q�6^   $	��105��6p   $��24��6|   $
��Left=�6�   $	��Top=�6�   �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��115,58,495,127q�6�   ._�5�      �� -   5        $	
 pHP��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   G   $ 0,100��6[   5�      �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��115,110,495,75q�6�   ._�5�      �� -   5        $	
 pHI��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   G   $ 0,100��6[   5�      �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��115,162,495,23q�6�   ._�5�      �� -   5        $
 pHILimit��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   L   $ 0,100��6`   5�      �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��400,58,210,127q�6�   ._�5�      �� -   5        $
 
pHDeadband��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   N   $ 0,100��6b   5�      �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��400,110,210,75q�6�   ._�5�      �� -   5        $
 pHNegFactor��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   O   $ 0,100��6c   5�      �� 5�      $ 5��6   $	��140��6*   $��40��66   
������ 
�������$��1q�6n   $��400,162,210,23q�6�   ._�5�      �� -   5        $
 
pHEvalTime��5�      $	��136��5�      $��36��6*   
�������$��0q�6I   $��20(�6]   $��Regular"�6k   $��Center=�5�   *   $��Center=�6L   $
 Wrap=�5�   N   $ 0,100��6b   5�      �� 5�      -   5        $	��150��5�      $��40��6%   $��	Reset PID��61   $��25(�6E   $	 0.7Q�6S   $��581,57,19,128q�6a   ��5�      �� 5�       �� Color$ #D6D6D6��6*    J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         Color$ Black��6+      	Direction$	 320Q�69      ShadowDepth$ 5Q�6I      Opacity$	 0.5Q�6Y      
BlurRadius$ 10Q�6g   ��5�      �� 5�       �� 
TargetType" ��6*   .R�5�      �� #��z��6%   $ 5��6O   ._�5�   "   �� $��Center=�63   $��Center=�6P   5�      5�      5�      5�      5�      � 5�   
   $	��108��6   $��Bottom=�6   .+�5�      �� ���#AF0101$��0,0,0,0q�6*   $��1��6;   $	��Top=�6F   �� 5�      -   5        $	��200��5�      $��50��6!   $��0,0,80,0q�6-   $��Right=�6?   $��Center=�6[   ��5�      �� 5�      " ��6&   .R�5�      � .+�5�      �� ���#AF0101$ 5��6:    5�   "     5�   &   $ #808080��67   $	 270Q�6G   $ 3Q�6W   $ 5Q�6g   5�      �� 5�      $��Center=�6/   $��Center=�6L   5�      5�      .��5�      �� $��OK��6   $��30(�6&   ���#FFFFFF$��Center=�6I   $��Center=�6f   5�      �� 5�      -   5        $	��200��5�      $��50��6!   $��	0,0,292,0q�6-   $��Right=�6@   $��Center=�6\   ��5�      �� 5�      " ��6&   .R�5�      � .+�5�      �� ���#E7ECEF$ 5��6:    5�   "     5�   &   $ #808080��67   $	 270Q�6G   $ 3Q�6W   $ 5Q�6g   5�      �� 5�      $��Center=�6/   $��Center=�6L   5�      5�      .��5�      �� $��CANCEL��6   $��30(�6*   ���#000000$��Center=�6M   $��Center=�6j   5�      5�      � 5�   
   -   5         �Name$ KeypadPopup��5�      $��Center=�6%   $��False��68   .��5�         WpfApp1.Keypad.KeyPad  -   5          Name$ KeypadControl��5�      5�      5�      5�      