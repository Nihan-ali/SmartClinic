   M S B A M L   `   `   `  ����   SmartClinic=    7SmartClinic.View.UserControls.PatientProfileUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� ���#F3F3F3  � ColumnDefinitions  �� $	��20* ��� $	��80* �.+�� $��0��$��10q� � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��Auto �.+��� $��0��
�������$��1q�$��0,0,1,2q�
�������$��40��._��   �� $��* ��� $
��Auto �.+��� -    ��Name$ PatientNameTextBlock��$
��Bold"�$��16(�$��Center=�$
��Left=�$
��10,0q��� -    ��Name$
 edit��$
��Bold"�$��Center=�$
��10,0q�$��1��
��� � �$��5q�
���������Z� .����  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� $��../../user-edit.png��$��20��$��15���� $
��Edit��$��2,0,0,0q�
��� � �$
��Bold"��� $��1��
�������$��1q�$��0,0,1,2q�
�������$��30��$	��Top=�._��� -   $ IDTextBlock��$��Center=�$
��Bold"�$��Center=��� $��2��
�������$��1q�$��0,0,1,2q�
�������$��30��._��� -   $ AgeTextBlock��$
��Bold"�$��Center=�$��Center=��� -   $ writePrescription��$��Write Prescription��$
��Bold"�$��Center=�$��30��$��3��
��� � ���Z� � $��1��$��10q�
������� �� $	��10* �$��50���� $	��90* �.+��� $��0��
�������$��5q�._��� -   $ PastVisitTextBlock��
���   �$
��Bold"�$��16(�$��Center=�$��Center=��� -    ��Name$ PrescriptionList��$��1��$��5q�$��Stretch=����#eeeeee �� SelectedItem �� *
Patients+���� .R���  �� CornerRadius$ 5��$��5q�
�������$��Stretch=�$��0,.5,1,2q�
�������._��  �� $��60 �.+�� $��Stretch=�  �� $��* ��� $��2* ��� $��* ��� $��* ��� $��* �.+��� $��0��$
��10,5q�$��Center=��� $�Date: ��$
��Bold"� �� $��SemiBold"���� *FormattedVisit+ �� Mode$ OneWay=��� $��1��$ 
Horizontal=�$��Center=�.+��� $��Prescription ID: ��$
��Bold"�$��Center=�$��Center=�$��50,0,5,0q��� $��Center=�$��Center=�$��DemiBold"����� *prescriptionId+�� -   $��2��$��Show Prescription��$��70,10,80,10q�
�������
����  �$
��Bold"���Z� �� $ DeleteButton��-	   $��3��$��Delete��$
��10,5q�
�������
���  ��$
��Bold"���Z� 