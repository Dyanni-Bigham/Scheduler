   M S B A M L   `   `   `  ����D  @Scheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Scheduler	Scheduler  5   	   1clr-namespace:Scheduler.UtilsScheduler.Utils  5   	       Scheduler.EditWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Scheduler     5   	   /utilsclr-namespace:Scheduler.Utils     5   	     � Title$  
EditWindow��5	   	   $	��357��6   $	��736��6)    � 
ResizeMode$ NoResize=�65    � 	Resources 5
      '   !Scheduler.Utils.FileNameConverter   FileNameConverter��%   &	          5   
   6"   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5         AutoGenerateColumns$ False��6%      CanUserAddRows$ False��6A      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Days��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 67   	   CellTemplate	 5      �� 5      .R�5      �� ��6+   �� *Days+5      5        5      $
 Apps��6)   $ * 67   	 5      �� 5      .R�5      �� ��6+   �� *Apps+��5   "   �� 5   &   .R�5   *   �� ��64   �� 
 �� 	Converter#
 [  5   '   5       5!      5#      0  *System.Windows.Controls.DataGridTextColumn  5$      $ Interval��6%   $ * 68   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *
Interval+5'      �� 5)   
   -   5        $��Save Changes��5)      $
��Left=�6A   $��604,289,0,0q�6\   $	��Top=�6q   $	��100��6�   �� 5+   
   -   5        $��Delete Selected��5+      $
��Left=�6F   $��604,247,0,0q�6a   $	��Top=�6v   $	��100��6�   5,      5-      