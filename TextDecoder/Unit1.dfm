object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 649
  ClientWidth = 784
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  Menu = MainMenu1
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Memo1: TMemo
    Left = 0
    Top = 0
    Width = 785
    Height = 295
    ScrollBars = ssVertical
    TabOrder = 0
  end
  object Memo2: TMemo
    Left = 0
    Top = 352
    Width = 785
    Height = 295
    ScrollBars = ssVertical
    TabOrder = 1
  end
  object Button1: TButton
    Left = 615
    Top = 300
    Width = 161
    Height = 40
    Caption = #1056#1072#1089#1096#1080#1092#1088#1086#1074#1072#1090#1100
    Enabled = False
    TabOrder = 2
    OnClick = Button1Click
  end
  object Memo3: TMemo
    Left = 0
    Top = 297
    Width = 609
    Height = 47
    TabOrder = 3
  end
  object MainMenu1: TMainMenu
    Left = 736
    Top = 16
    object A1: TMenuItem
      Caption = #1060#1072#1081#1083
      object N2: TMenuItem
        Caption = #1054#1090#1082#1088#1099#1090#1100
        OnClick = N2Click
      end
      object N3: TMenuItem
        Caption = #1057#1086#1093#1088#1072#1085#1080#1090#1100
        OnClick = N3Click
      end
    end
    object N1: TMenuItem
      Caption = #1053#1072#1089#1090#1088#1086#1081#1082#1080
      object N4: TMenuItem
        Caption = #1040#1083#1092#1072#1074#1080#1090
        OnClick = N4Click
      end
    end
    object N5: TMenuItem
      Caption = #1044#1086#1087#1086#1083#1085#1080#1090#1077#1083#1100#1085#1099#1077' '#1076#1077#1081#1089#1090#1074#1080#1103
      object N7: TMenuItem
        Caption = #1055#1086#1087#1099#1090#1082#1072' '#1091#1083#1091#1095#1096#1077#1085#1080#1103' '#1088#1077#1079#1091#1083#1100#1090#1072#1090#1072
        OnClick = N7Click
      end
      object N21: TMenuItem
        Caption = #1055#1086#1087#1099#1090#1082#1072' '#1091#1083#1091#1095#1096#1077#1085#1080#1103' '#1088#1077#1079#1091#1083#1100#1090#1072#1090#1072': '#1089#1087#1086#1089#1086#1073' '#8470'2'
        OnClick = N21Click
      end
    end
  end
  object OpenDialog1: TOpenDialog
    Left = 736
    Top = 72
  end
  object SaveDialog1: TSaveDialog
    Left = 736
    Top = 120
  end
end
