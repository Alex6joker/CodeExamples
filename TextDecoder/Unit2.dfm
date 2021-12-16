object Form2: TForm2
  Left = 0
  Top = 0
  Caption = 'Form2'
  ClientHeight = 184
  ClientWidth = 280
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 120
    Top = 8
    Width = 35
    Height = 19
    Caption = #1071#1079#1099#1082
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 48
    Top = 68
    Width = 184
    Height = 19
    Caption = #1058#1086#1095#1085#1086#1089#1090#1100' '#1088#1072#1089#1096#1080#1092#1088#1086#1074#1082#1080' (%)'
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Times New Roman'
    Font.Style = []
    ParentFont = False
  end
  object ComboBox1: TComboBox
    Left = 64
    Top = 33
    Width = 145
    Height = 21
    ParentShowHint = False
    ShowHint = False
    TabOrder = 0
    Items.Strings = (
      #1056#1091#1089#1089#1082#1080#1081
      #1040#1085#1075#1083#1080#1081#1089#1082#1080#1081)
  end
  object Edit1: TEdit
    Left = 80
    Top = 90
    Width = 121
    Height = 21
    TabOrder = 1
    OnChange = Edit1Change
  end
  object Button1: TButton
    Left = 24
    Top = 136
    Width = 75
    Height = 25
    Caption = #1054#1082
    TabOrder = 2
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 184
    Top = 136
    Width = 75
    Height = 25
    Caption = #1054#1090#1084#1077#1085#1072
    TabOrder = 3
    OnClick = Button2Click
  end
end
