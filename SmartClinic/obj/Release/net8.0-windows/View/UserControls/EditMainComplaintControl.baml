   M S B A M L   `   `   `  ���� F  BSmartClinic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:SmartClinic.View.UserControlsSmartClinic.View.UserControls  <    6SmartClinic.View.UserControls.EditMainComplaintControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     =local+clr-namespace:SmartClinic.View.UserControls     -     � WindowStyle$
  None=� � Title$ EditMainComplaintControl��$��80��$	��650�����	#FFF0F0F0���	#FF707070$��1q�'  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 0q�   CornerRadius$ 15������  �� 
TargetType" �.���  �� Property � �� �� Value ��  �� 
TargetType" �.R��� #��z��#��z��#��z��	 �� CornerRadius$	 10��._��� .��� 
 � ColumnDefinitions
 �� $��2* ��� $��1* ��� $��2* �.+��� $��3�����#ebebeb$	 5��$��2q�
���   �._��� $��20,10,10,10q� �� Orientation$ 
Horizontal=�$��2��.+��� $��0��$ 
Horizontal=�$��0,0,0,0q�.+��� $
��Bold"�$��13(�$��
Content : ��$��Center=��� $
��20,0q�$	��200��$��30��$��Center=�$	 5��._��� -    ��Name$ complaintContentTextBox��$��Center=����� *	Content+ �� Mode$ TwoWay=��� $��1��$ 
Horizontal=�$��20,0,0,0q�.+��� -   $ 
periodUnit��$��30��$��25��$��0,0,5,0q����� *
PeriodTime+�� -    ��Name$ PeriodComboBox��$��60��$��25��$��0,0,5,0q����#ECF0F1���#2C3E50$��2q�
������� �� SelectedValue �� *
PeriodUnit+$ TwoWay=�.@��� $��	দিন�� �� 
IsSelected$
 True���� $��সপ্তাহ���� $��	মাস���� $��2��$ 
Horizontal=�$��20,0,0,0q�.+��� $
��Bold"�$��13(�$��Note : ��$��Center=��� $
��20,0q�$	��200��$��30��$��Center=�$	 5��._��� -   $ noteTextBox��$��Center=����� *Note+$ TwoWay=�