   M S B A M L   `   `   `  ����L  HImageRegistration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ImageRegistrationImageRegistration  5   	   "    ImageRegistration.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ImageRegistration     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��376* �6   �� 5      $	��43* �6    � ColumnDefinitions 5   
   �� 5      $
��250* �6   �� 5      $
��250* �6   �� 5      $
��250* �6   .+�5   
   �� -   5         ��Name$ Images1��5      $��0��6"   $��0��62   �� 5   
   -   5        $ Images2��5      $��1��6"   $��0��62   �� 5   
   -   5        $ Images3��5      $��2��6"   $��0��62   �� 5   
   -   5         ��Name$ 	InitShape��5      $��Initialize Shapes��6$   $��0��6@   $��1��6P   $��Center=�6]   6z   �� 5   
   -   5        $ ApplyTransf��5      $��Apply Transformation��6&   $��1��6E   $��1��6U   $��Center=�6b   6   �� 5   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��1��6>   $��Center=�6K   .+�5      �� -   5        $ OutlierRemove��5      $��Outlier Removal��6-   $��Center=�6G   6d   �� 5      -   5         ��Name$ isRansac��5      $��Ransac��6)   $��Center=�6:   $��5q�6U   5      5      5      