object Form4: TForm4
  Left = 0
  Top = 0
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = #1059#1076#1072#1083#1077#1085#1080#1077' '#1101#1083#1077#1084#1077#1085#1090#1086#1074' '#1090#1072#1073#1083#1080#1094#1099
  ClientHeight = 106
  ClientWidth = 382
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 24
    Top = 16
    Width = 340
    Height = 13
    Caption = #1042#1099' '#1091#1074#1077#1088#1077#1085#1099', '#1095#1090#1086' '#1093#1086#1090#1080#1090#1077' '#1091#1076#1072#1083#1080#1090#1100' '#1076#1072#1085#1085#1091#1102' '#1079#1072#1087#1080#1089#1100' '#1080#1079' '#1073#1072#1079#1099' '#1076#1072#1085#1085#1099#1093'?'
  end
  object Button1: TButton
    Left = 80
    Top = 48
    Width = 75
    Height = 25
    Caption = #1044#1072
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 224
    Top = 48
    Width = 75
    Height = 25
    Caption = #1053#1077#1090
    TabOrder = 1
    OnClick = Button2Click
  end
  object ADOQuery1: TADOQuery
    Parameters = <>
    Left = 16
    Top = 48
  end
  object ADOQuery2: TADOQuery
    Parameters = <>
    Left = 312
    Top = 48
  end
end
