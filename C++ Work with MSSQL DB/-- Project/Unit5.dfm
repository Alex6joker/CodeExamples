object Form5: TForm5
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = #1055#1086#1083#1091#1095#1077#1085#1080#1077' '#1089#1074#1077#1076#1077#1085#1080#1081' '#1080#1079' '#1073#1072#1079#1099' '#1076#1072#1085#1085#1099#1093
  ClientHeight = 446
  ClientWidth = 752
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
  object Label1: TLabel
    Left = 296
    Top = 129
    Width = 31
    Height = 13
    Alignment = taCenter
    Caption = 'Label1'
    WordWrap = True
  end
  object Label2: TLabel
    Left = 296
    Top = 176
    Width = 31
    Height = 13
    Alignment = taCenter
    Caption = 'Label2'
    WordWrap = True
  end
  object Label3: TLabel
    Left = 296
    Top = 215
    Width = 31
    Height = 13
    Alignment = taCenter
    Caption = 'Label3'
    WordWrap = True
  end
  object Label4: TLabel
    Left = 296
    Top = 272
    Width = 31
    Height = 13
    Alignment = taCenter
    Caption = 'Label4'
    WordWrap = True
  end
  object ComboBox1: TComboBox
    Left = 8
    Top = 8
    Width = 145
    Height = 21
    TabOrder = 0
    Text = 'ComboBox1'
    Visible = False
  end
  object Button1: TButton
    Left = 176
    Top = 35
    Width = 75
    Height = 25
    Caption = 'Button1'
    TabOrder = 1
    Visible = False
    OnClick = Button1Click
  end
  object Edit1: TEdit
    Left = 8
    Top = 99
    Width = 121
    Height = 21
    TabOrder = 2
    Text = 'Edit1'
    Visible = False
  end
  object Edit2: TEdit
    Left = 8
    Top = 126
    Width = 121
    Height = 21
    TabOrder = 3
    Text = 'Edit2'
    Visible = False
  end
  object ADOQuery1: TADOQuery
    Parameters = <>
    Left = 296
    Top = 8
  end
end
