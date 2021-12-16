object Form1: TForm1
  Left = 303
  Top = 213
  HorzScrollBar.Visible = False
  BiDiMode = bdLeftToRight
  BorderIcons = [biMinimize, biMaximize]
  BorderStyle = bsToolWindow
  Caption = #1052#1072#1090#1088#1080#1094#1072' '#1089#1084#1077#1078#1085#1086#1089#1090#1080' '#1050#1077#1085#1080#1075#1072
  ClientHeight = 626
  ClientWidth = 1399
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  ParentBiDiMode = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 520
    Top = 56
    Width = 329
    Height = 27
    Alignment = taCenter
    AutoSize = False
    Caption = #1047#1072#1075#1088#1091#1079#1082#1072' '#1080#1089#1093#1086#1076#1085#1099#1093' '#1076#1072#1085#1085#1099#1093' '#1080#1079' '#1092#1072#1081#1083#1072' '
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Tahoma'
    Font.Style = [fsBold, fsItalic]
    ParentFont = False
  end
  object Label2: TLabel
    Left = 48
    Top = 136
    Width = 537
    Height = 25
    Alignment = taCenter
    AutoSize = False
    Caption = #1048#1089#1093#1086#1076#1085#1099#1077' '#1076#1072#1085#1085#1099#1077
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 704
    Top = 136
    Width = 537
    Height = 23
    Alignment = taCenter
    AutoSize = False
    Caption = #1052#1072#1090#1088#1080#1094#1072' '#1089#1084#1077#1078#1085#1086#1089#1090#1080
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Memo1: TMemo
    Left = 704
    Top = 160
    Width = 537
    Height = 313
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -24
    Font.Name = 'Microsoft Sans Serif'
    Font.Style = [fsItalic]
    ParentFont = False
    ReadOnly = True
    TabOrder = 0
  end
  object Button1: TButton
    Left = 112
    Top = 480
    Width = 193
    Height = 33
    Caption = #1042#1099#1074#1086#1076' '#1087#1088#1086#1095#1080#1090#1072#1085#1085#1099#1093' '#1076#1072#1085#1085#1099#1093
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 336
    Top = 480
    Width = 177
    Height = 33
    Caption = #1054#1095#1080#1089#1090#1080#1090#1100
    TabOrder = 2
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 848
    Top = 48
    Width = 169
    Height = 41
    Caption = #1047#1072#1075#1088#1091#1079#1080#1090#1100
    TabOrder = 3
    OnClick = Button3Click
  end
  object Memo3: TMemo
    Left = 0
    Top = 96
    Width = 1401
    Height = 33
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'MS Reference Sans Serif'
    Font.Style = []
    ParentFont = False
    ReadOnly = True
    TabOrder = 4
    OnChange = Memo3Change
  end
  object Memo4: TMemo
    Left = 48
    Top = 160
    Width = 537
    Height = 313
    DragCursor = crDefault
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'MS Reference Sans Serif'
    Font.Style = [fsItalic]
    ParentFont = False
    ReadOnly = True
    ScrollBars = ssVertical
    TabOrder = 5
  end
  object Button4: TButton
    Left = 776
    Top = 480
    Width = 193
    Height = 33
    Caption = #1042#1099#1074#1086#1076' '#1084#1072#1090#1088#1080#1094#1099' '#1089#1084#1077#1078#1085#1086#1089#1090#1080
    TabOrder = 6
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 1008
    Top = 480
    Width = 193
    Height = 33
    Caption = #1054#1095#1080#1089#1090#1080#1090#1100
    TabOrder = 7
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 1104
    Top = 568
    Width = 273
    Height = 41
    Caption = #1042#1099#1081#1090#1080
    TabOrder = 8
    OnClick = Button6Click
  end
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 1401
    Height = 41
    Caption = #1060#1086#1088#1084#1080#1088#1086#1074#1072#1085#1080#1077' '#1084#1072#1090#1088#1080#1094#1099' '#1089#1084#1077#1078#1085#1086#1089#1090#1080' '#1075#1088#1072#1092#1072' '#1050#1077#1085#1080#1075#1072
    Font.Charset = RUSSIAN_CHARSET
    Font.Color = clWindowText
    Font.Height = -21
    Font.Name = 'Times New Roman'
    Font.Style = [fsBold, fsItalic]
    ParentFont = False
    TabOrder = 9
  end
  object OpenDialog1: TOpenDialog
    Filter = #1057#1090#1072#1085#1076#1072#1088#1090#1085#1099#1081' '#1090#1077#1082#1089#1090#1086#1074#1099#1081' (*.txt)'
    Options = [ofHideReadOnly, ofExtensionDifferent, ofEnableSizing]
    Left = 1032
    Top = 56
  end
end
