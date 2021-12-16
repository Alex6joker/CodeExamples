object Form3: TForm3
  Left = 0
  Top = 0
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = #1048#1079#1084#1077#1085#1077#1085#1080#1077' '#1101#1083#1077#1084#1077#1085#1090#1086#1074' '#1090#1072#1073#1083#1080#1094#1099
  ClientHeight = 448
  ClientWidth = 677
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnActivate = FormActivate
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Button1: TButton
    Left = 152
    Top = 368
    Width = 75
    Height = 25
    Caption = #1054#1082
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 256
    Top = 368
    Width = 75
    Height = 25
    Caption = #1054#1090#1084#1077#1085#1072
    TabOrder = 1
    OnClick = Button2Click
  end
  object ADOQuery1: TADOQuery
    Parameters = <>
    Left = 16
    Top = 16
  end
  object ADOQuery2: TADOQuery
    Parameters = <>
    Left = 16
    Top = 72
  end
  object ADOQuery3: TADOQuery
    Parameters = <>
    Left = 16
    Top = 128
  end
end
