   M S B A M L   `   `   `  ����D  @Scheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Scheduler	Scheduler  5   	       Scheduler.CustomInterval   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Scheduler     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Change Interval��5	   	   $	��200��6!   $	��300��6.   .��5
      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��100 �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $	��200 �6   .+�5   
   �� $��16(�6   .��6   Enter Time:6,   �� 5   
   -   5         ��Name$ 	timeInput��5      $��16(�6%   $��
5,4,100,19q�63   $��1��6G   �� 5   
   $��0,5,0,68q�6   $��16(�6"   $��1��61   .��6>   Unit of Time6L   �� 5   
   -   5         ��Name$ 
myComboBox��5      $��1��6'   $��1��64   $��	5,4,99,66q�6D   .@�5      �� $��Minute��6   �� 5      $
��Hour��6   5      �� 5   
   -   5         ��Name$ 	btnSubmit��5      $��1��6$   $��5,70,124,-1q�61   $��1��6F   .��6n   Submit6v   5      5       