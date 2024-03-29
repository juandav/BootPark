object frmUHFReader28main: TfrmUHFReader28main
  Left = 141
  Top = 24
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'UHFReader28 Demo Software V1.5'
  ClientHeight = 675
  ClientWidth = 785
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 786
    Height = 654
    ActivePage = TabSheet_EPCC1G2
    TabOrder = 0
    OnChange = PageControl1Change
    object TabSheet1: TTabSheet
      Caption = 'Reader Parameter'
      object GroupBox_ReaderInfo: TGroupBox
        Left = 136
        Top = 10
        Width = 633
        Height = 102
        Caption = 'Reader Information'
        TabOrder = 0
        object Label2: TLabel
          Left = 162
          Top = 18
          Width = 38
          Height = 13
          Caption = 'Version:'
        end
        object Label3: TLabel
          Left = 10
          Top = 47
          Width = 41
          Height = 13
          Caption = 'Address:'
        end
        object Label4: TLabel
          Left = 328
          Top = 47
          Width = 118
          Height = 13
          Caption = 'Max InventoryScanTime:'
        end
        object Label10: TLabel
          Left = 10
          Top = 18
          Width = 27
          Height = 13
          Caption = 'Type:'
        end
        object Label11: TLabel
          Left = 328
          Top = 18
          Width = 36
          Height = 13
          Caption = 'Protocl:'
        end
        object Label8: TLabel
          Left = 160
          Top = 47
          Width = 33
          Height = 13
          Caption = 'Power:'
        end
        object Label13: TLabel
          Left = 160
          Top = 76
          Width = 51
          Height = 13
          Caption = 'Dmaxfre'#65306
        end
        object Label14: TLabel
          Left = 10
          Top = 76
          Width = 53
          Height = 13
          Caption = 'Dminxfre'#65306
        end
        object Edit_Version: TEdit
          Left = 225
          Top = 14
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 2
        end
        object Edit_ComAdr: TEdit
          Left = 72
          Top = 43
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 4
        end
        object Edit_scantime: TEdit
          Left = 488
          Top = 43
          Width = 129
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 6
        end
        object Edit_Type: TEdit
          Left = 72
          Top = 14
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 1
        end
        object Button3: TButton
          Left = 488
          Top = 69
          Width = 129
          Height = 25
          Action = Action_GetReaderInformation
          Caption = 'Get Reader Info'
          TabOrder = 7
        end
        object Edit_dmaxfre: TEdit
          Left = 225
          Top = 72
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 9
        end
        object Edit_dminfre: TEdit
          Left = 72
          Top = 72
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 8
        end
        object Edit_power: TEdit
          Left = 225
          Top = 43
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 5
        end
        object ISO180006B: TCheckBox
          Left = 488
          Top = 8
          Width = 89
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'ISO18000-6B'
          ParentBiDiMode = False
          TabOrder = 0
        end
        object EPCC1G2: TCheckBox
          Left = 488
          Top = 24
          Width = 73
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'EPCC1-G2'
          ParentBiDiMode = False
          TabOrder = 3
        end
      end
      object GroupBox2: TGroupBox
        Left = 136
        Top = 113
        Width = 633
        Height = 148
        Caption = 'Set Reader Parameter'
        TabOrder = 1
        object Label15: TLabel
          Left = 8
          Top = 88
          Width = 53
          Height = 13
          Caption = 'Dminxfre'#65306
        end
        object Label16: TLabel
          Left = 8
          Top = 119
          Width = 51
          Height = 13
          Caption = 'Dmaxfre'#65306
        end
        object Label17: TLabel
          Left = 202
          Top = 24
          Width = 37
          Height = 13
          Caption = 'Baud'#65306
        end
        object Label1: TLabel
          Left = 8
          Top = 26
          Width = 69
          Height = 13
          Caption = 'Address(HEX):'
        end
        object Label7: TLabel
          Left = 8
          Top = 57
          Width = 33
          Height = 13
          Caption = 'Power:'
        end
        object Label5: TLabel
          Left = 202
          Top = 57
          Width = 121
          Height = 13
          Caption = 'Max InventoryScanTime::'
        end
        object Button5: TButton
          Left = 344
          Top = 113
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation
          Caption = 'Set Parameter'
          TabOrder = 6
        end
        object ComboBox_baud: TComboBox
          Left = 331
          Top = 22
          Width = 129
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            '9600bps'
            '19200bps'
            '38400bps'
            '57600bps'
            '115200bps')
        end
        object Edit_NewComAdr: TEdit
          Left = 80
          Top = 22
          Width = 113
          Height = 21
          MaxLength = 2
          TabOrder = 0
          Text = '00'
        end
        object ComboBox_scantime: TComboBox
          Left = 331
          Top = 53
          Width = 129
          Height = 21
          Style = csDropDownList
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ItemHeight = 13
          TabOrder = 3
        end
        object Button1: TButton
          Left = 488
          Top = 113
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation_0
          Caption = 'Default Parameter'
          TabOrder = 7
        end
        object ComboBox_dminfre: TComboBox
          Left = 80
          Top = 84
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 4
          OnSelect = ComboBox_dminfreSelect
        end
        object ComboBox_dmaxfre: TComboBox
          Tag = 1
          Left = 80
          Top = 115
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 8
          OnSelect = ComboBox_dminfreSelect
        end
        object ComboBox_PowerDbm: TComboBox
          Left = 80
          Top = 53
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30')
        end
        object CheckBox_SameFre: TCheckBox
          Left = 202
          Top = 88
          Width = 81
          Height = 17
          Caption = 'Single Freq'
          TabOrder = 5
          OnClick = CheckBox_SameFreClick
        end
        object GroupBox7: TGroupBox
          Left = 464
          Top = 11
          Width = 153
          Height = 96
          Caption = 'FreqBaud'
          TabOrder = 9
          object RadioButton_band1: TRadioButton
            Left = 8
            Top = 13
            Width = 113
            Height = 17
            Caption = 'User band'
            TabOrder = 0
            OnClick = RadioButton_band1Click
          end
          object RadioButton_band2: TRadioButton
            Left = 8
            Top = 28
            Width = 113
            Height = 17
            Caption = 'Chinese band2'
            TabOrder = 1
            OnClick = RadioButton_band2Click
          end
          object RadioButton_band3: TRadioButton
            Left = 8
            Top = 43
            Width = 113
            Height = 17
            Caption = 'US band'
            TabOrder = 2
            OnClick = RadioButton_band3Click
          end
          object RadioButton_band4: TRadioButton
            Left = 8
            Top = 56
            Width = 113
            Height = 17
            Caption = 'Korean band'
            TabOrder = 3
            OnClick = RadioButton_band4Click
          end
          object RadioButton_band5: TRadioButton
            Left = 8
            Top = 73
            Width = 113
            Height = 17
            Caption = 'EU band'
            TabOrder = 4
            OnClick = RadioButton_band5Click
          end
        end
      end
      object GroupBox8: TGroupBox
        Left = 321
        Top = 428
        Width = 179
        Height = 41
        Caption = 'Beep Operation'
        TabOrder = 2
        object Radio_beepEn: TRadioButton
          Left = 8
          Top = 16
          Width = 49
          Height = 17
          Caption = 'On'
          TabOrder = 0
        end
        object Radio_beepDis: TRadioButton
          Left = 64
          Top = 16
          Width = 49
          Height = 17
          Caption = 'Off'
          TabOrder = 1
        end
        object Button_Beep: TButton
          Left = 104
          Top = 11
          Width = 57
          Height = 25
          Caption = 'Set'
          TabOrder = 2
          OnClick = Button_BeepClick
        end
      end
      object GroupBox25: TGroupBox
        Left = 135
        Top = 370
        Width = 178
        Height = 99
        Caption = 'Real Time Clock Setting'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -12
        Font.Name = #23435#20307
        Font.Style = []
        ParentFont = False
        TabOrder = 3
        object Label22: TLabel
          Left = 40
          Top = 16
          Width = 30
          Height = 12
          Caption = 'Month'
        end
        object Label23: TLabel
          Left = 152
          Top = 16
          Width = 18
          Height = 12
          Caption = 'Sec'
        end
        object Label26: TLabel
          Left = 76
          Top = 16
          Width = 18
          Height = 12
          Caption = 'Day'
        end
        object Label40: TLabel
          Left = 9
          Top = 16
          Width = 24
          Height = 12
          Caption = 'Year'
        end
        object Label41: TLabel
          Left = 99
          Top = 16
          Width = 24
          Height = 12
          Caption = 'Hour'
        end
        object Label44: TLabel
          Left = 128
          Top = 16
          Width = 18
          Height = 12
          Caption = 'Min'
        end
        object Edit_year: TEdit
          Left = 19
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 1
          OnKeyPress = Edit8KeyPress
        end
        object Edit_Month: TEdit
          Left = 46
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 2
          OnKeyPress = Edit8KeyPress
        end
        object Edit_date: TEdit
          Left = 73
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 3
          OnKeyPress = Edit8KeyPress
        end
        object Edit_Hour: TEdit
          Left = 100
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 4
          OnKeyPress = Edit8KeyPress
        end
        object Edit_minute: TEdit
          Left = 126
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 5
          OnKeyPress = Edit8KeyPress
        end
        object Edit_Sedcond: TEdit
          Left = 151
          Top = 32
          Width = 21
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          MaxLength = 2
          TabOrder = 6
          OnKeyPress = Edit8KeyPress
        end
        object ClockCMD: TButton
          Left = 98
          Top = 59
          Width = 73
          Height = 33
          Caption = 'Go'
          TabOrder = 8
          OnClick = ClockCMDClick
        end
        object RadioButton_SetClock: TRadioButton
          Left = 2
          Top = 58
          Width = 81
          Height = 17
          Caption = 'Set Clock'
          Checked = True
          TabOrder = 7
          TabStop = True
        end
        object RadioButton_GetClock: TRadioButton
          Left = 2
          Top = 78
          Width = 89
          Height = 17
          Caption = 'Query Clock'
          TabOrder = 9
        end
        object Edit8: TEdit
          Left = 5
          Top = 32
          Width = 17
          Height = 20
          ImeName = #20013#25991' ('#31616#20307') - '#25628#29399#25340#38899#36755#20837#27861
          ReadOnly = True
          TabOrder = 0
          Text = '20'
          OnKeyPress = Edit8KeyPress
        end
      end
      object GroupBox27: TGroupBox
        Left = 136
        Top = 267
        Width = 633
        Height = 40
        Caption = 'GPIO Operation'
        TabOrder = 4
        object Button_SetGPIO: TButton
          Left = 472
          Top = 10
          Width = 72
          Height = 25
          Caption = 'Set'
          TabOrder = 0
          OnClick = Button_SetGPIOClick
        end
        object Button_GetGPIO: TButton
          Left = 554
          Top = 10
          Width = 70
          Height = 25
          Caption = 'Get'
          TabOrder = 1
          OnClick = Button_GetGPIOClick
        end
        object CheckBox2: TCheckBox
          Left = 6
          Top = 16
          Width = 49
          Height = 17
          Caption = 'Pin1'
          TabOrder = 2
        end
        object CheckBox3: TCheckBox
          Left = 59
          Top = 15
          Width = 49
          Height = 17
          Caption = 'Pin2'
          TabOrder = 3
        end
        object CheckBox4: TCheckBox
          Left = 110
          Top = 15
          Width = 45
          Height = 17
          Caption = 'Pin3'
          TabOrder = 4
        end
        object CheckBox5: TCheckBox
          Left = 160
          Top = 15
          Width = 48
          Height = 17
          Caption = 'Pin4'
          TabOrder = 5
        end
        object CheckBox6: TCheckBox
          Left = 209
          Top = 15
          Width = 49
          Height = 17
          Caption = 'Pin5'
          TabOrder = 6
        end
        object CheckBox7: TCheckBox
          Left = 268
          Top = 15
          Width = 49
          Height = 17
          Caption = 'Pin6'
          TabOrder = 7
        end
        object CheckBox8: TCheckBox
          Left = 322
          Top = 15
          Width = 49
          Height = 17
          Caption = 'Pin7'
          TabOrder = 8
        end
        object CheckBox9: TCheckBox
          Left = 377
          Top = 15
          Width = 49
          Height = 17
          Caption = 'Pin8'
          TabOrder = 9
        end
      end
      object GroupBox26: TGroupBox
        Left = 135
        Top = 316
        Width = 324
        Height = 48
        Caption = 'Antenna configuration '
        TabOrder = 5
        object Button_Ant: TButton
          Left = 248
          Top = 16
          Width = 63
          Height = 25
          Caption = 'Set'
          TabOrder = 0
          OnClick = Button_AntClick
        end
        object CheckBox14: TCheckBox
          Left = 174
          Top = 20
          Width = 55
          Height = 17
          Caption = 'ANT4'
          TabOrder = 1
        end
        object CheckBox15: TCheckBox
          Left = 117
          Top = 20
          Width = 55
          Height = 17
          Caption = 'ANT3'
          TabOrder = 2
        end
        object CheckBox16: TCheckBox
          Left = 62
          Top = 20
          Width = 54
          Height = 17
          Caption = 'ANT2'
          TabOrder = 3
        end
        object CheckBox17: TCheckBox
          Left = 6
          Top = 21
          Width = 55
          Height = 17
          Caption = 'ANT1'
          TabOrder = 4
        end
      end
      object GroupBox28: TGroupBox
        Left = 320
        Top = 373
        Width = 286
        Height = 49
        Caption = 'Set Notification Pulse Output'
        TabOrder = 6
        object CheckBox10: TCheckBox
          Left = 6
          Top = 21
          Width = 49
          Height = 17
          Caption = 'OUT1'
          TabOrder = 0
        end
        object CheckBox11: TCheckBox
          Left = 57
          Top = 20
          Width = 49
          Height = 17
          Caption = 'OUT2'
          TabOrder = 1
        end
        object CheckBox12: TCheckBox
          Left = 108
          Top = 20
          Width = 48
          Height = 17
          Caption = 'OUT3'
          TabOrder = 2
        end
        object CheckBox13: TCheckBox
          Left = 160
          Top = 20
          Width = 48
          Height = 17
          Caption = 'OUT4'
          TabOrder = 3
        end
        object Button_OutputRep: TButton
          Left = 218
          Top = 14
          Width = 60
          Height = 25
          Caption = 'Set'
          TabOrder = 4
          OnClick = Button_OutputRepClick
        end
      end
      object GroupBox29: TGroupBox
        Left = 467
        Top = 317
        Width = 303
        Height = 47
        Caption = 'Relay control '
        TabOrder = 7
        object Label45: TLabel
          Left = 12
          Top = 19
          Width = 74
          Height = 13
          Caption = 'ReleaseTime'#65306
        end
        object Label46: TLabel
          Left = 179
          Top = 18
          Width = 29
          Height = 13
          Caption = '*50ms'
        end
        object ComboBox_RelayTime: TComboBox
          Left = 81
          Top = 13
          Width = 92
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Button_RelayTime: TButton
          Left = 224
          Top = 11
          Width = 70
          Height = 25
          Caption = 'Set'
          TabOrder = 1
          OnClick = Button_RelayTimeClick
        end
      end
      object GroupBox30: TGroupBox
        Left = 0
        Top = 10
        Width = 135
        Height = 402
        Caption = 'Communication'
        TabOrder = 8
        object GroupBox_COM: TGroupBox
          Left = 3
          Top = 40
          Width = 129
          Height = 199
          Caption = 'Communication'
          TabOrder = 0
          object Label6: TLabel
            Left = 7
            Top = 19
            Width = 58
            Height = 13
            Caption = 'COM Port'#65306
          end
          object Label12: TLabel
            Left = 8
            Top = 129
            Width = 87
            Height = 13
            Caption = 'Opened COM Port'
          end
          object Label47: TLabel
            Left = 8
            Top = 90
            Width = 37
            Height = 13
            Caption = 'Baud'#65306
          end
          object ComboBox_COM: TComboBox
            Left = 60
            Top = 14
            Width = 64
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 0
            OnChange = ComboBox_COMChange
          end
          object Button2: TButton
            Left = 7
            Top = 64
            Width = 114
            Height = 22
            Caption = 'Open Com Port'
            TabOrder = 3
            OnClick = Button2Click
          end
          object Button4: TButton
            Left = 5
            Top = 169
            Width = 117
            Height = 21
            Caption = 'Close Com Port'
            TabOrder = 5
            OnClick = Button4Click
          end
          object StaticText1: TStaticText
            Left = 7
            Top = 42
            Width = 83
            Height = 17
            Caption = 'Reader Address:'
            TabOrder = 2
          end
          object Edit_CmdComAddr: TEdit
            Left = 97
            Top = 39
            Width = 25
            Height = 21
            CharCase = ecUpperCase
            MaxLength = 2
            TabOrder = 1
            Text = 'FF'
          end
          object ComboBox_AlreadyOpenCOM: TComboBox
            Left = 8
            Top = 144
            Width = 115
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 4
            OnCloseUp = ComboBox_AlreadyOpenCOMCloseUp
          end
          object ComboBox_baud2: TComboBox
            Left = 8
            Top = 105
            Width = 115
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 6
            Items.Strings = (
              '9600bps'
              '19200bps'
              '38400bps'
              '57600bps'
              '115200bps')
          end
        end
        object RadioButton11: TRadioButton
          Left = 5
          Top = 18
          Width = 52
          Height = 17
          Caption = 'COM'
          TabOrder = 1
          OnClick = RadioButton11Click
        end
        object RadioButton12: TRadioButton
          Left = 61
          Top = 18
          Width = 52
          Height = 17
          Caption = 'TCPIP'
          TabOrder = 2
          OnClick = RadioButton12Click
        end
        object GroupBox31: TGroupBox
          Left = 3
          Top = 240
          Width = 129
          Height = 157
          Caption = 'TCPIP'
          TabOrder = 3
          object Label48: TLabel
            Left = 12
            Top = 19
            Width = 31
            Height = 13
            Caption = 'Port'#65306
          end
          object Label49: TLabel
            Left = 12
            Top = 48
            Width = 13
            Height = 13
            Caption = 'IP:'
          end
          object Label50: TLabel
            Left = 12
            Top = 74
            Width = 79
            Height = 13
            Caption = 'Reader Address:'
          end
          object Edit4: TEdit
            Left = 48
            Top = 14
            Width = 73
            Height = 21
            TabOrder = 0
            Text = '27011'
          end
          object Edit5: TEdit
            Left = 32
            Top = 40
            Width = 90
            Height = 21
            TabOrder = 1
            Text = '192.168.0.250'
          end
          object Edit6: TEdit
            Left = 80
            Top = 66
            Width = 41
            Height = 21
            TabOrder = 2
            Text = 'FF'
          end
          object opnet: TButton
            Left = 8
            Top = 94
            Width = 113
            Height = 25
            Caption = 'OpenNetPort'
            Enabled = False
            TabOrder = 3
            OnClick = opnetClick
          end
          object closenet: TButton
            Left = 8
            Top = 126
            Width = 113
            Height = 25
            Caption = 'CloseNetPort'
            Enabled = False
            TabOrder = 4
            OnClick = closenetClick
          end
        end
      end
      object GroupBox45: TGroupBox
        Left = 0
        Top = 413
        Width = 133
        Height = 72
        Caption = 'Reader seria number'
        TabOrder = 9
        object Edit15: TEdit
          Left = 9
          Top = 16
          Width = 115
          Height = 21
          TabOrder = 0
        end
        object Button8: TButton
          Left = 53
          Top = 42
          Width = 71
          Height = 24
          Caption = 'Get'
          TabOrder = 1
          OnClick = Button8Click
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Auto_running Mode'
      ImageIndex = 1
      object GroupBox33: TGroupBox
        Left = 9
        Top = 5
        Width = 224
        Height = 92
        Caption = 'EAS Sensitivity'
        TabOrder = 0
        object Label53: TLabel
          Left = 8
          Top = 64
          Width = 72
          Height = 13
          Caption = 'EAS Accuracy:'
        end
        object RadioButton1: TRadioButton
          Left = 8
          Top = 16
          Width = 215
          Height = 17
          Caption = 'Relay release 3s when detected EAS'
          TabOrder = 0
        end
        object RadioButton2: TRadioButton
          Left = 8
          Top = 38
          Width = 71
          Height = 17
          Caption = 'None'
          TabOrder = 1
        end
        object ComboBox_Accuracy: TComboBox
          Left = 80
          Top = 59
          Width = 65
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8')
        end
        object Button_Accuracy: TButton
          Left = 152
          Top = 56
          Width = 63
          Height = 25
          Caption = 'Set'
          TabOrder = 3
          OnClick = Button_AccuracyClick
        end
      end
      object GroupBox34: TGroupBox
        Left = 240
        Top = 5
        Width = 313
        Height = 92
        Caption = 'Mask Setting'
        TabOrder = 1
        object Label54: TLabel
          Left = 4
          Top = 40
          Width = 142
          Height = 13
          Caption = 'Mask Start bit address(Hex)'#65306
        end
        object Label55: TLabel
          Left = 173
          Top = 40
          Width = 105
          Height = 13
          Caption = 'Mask Bit Length(Hex):'
        end
        object Label56: TLabel
          Left = 7
          Top = 70
          Width = 80
          Height = 13
          Caption = 'Mask Data(Hex):'
        end
        object RadioButton3: TRadioButton
          Left = 7
          Top = 16
          Width = 66
          Height = 17
          Caption = 'EPC'
          TabOrder = 0
        end
        object RadioButton4: TRadioButton
          Left = 72
          Top = 16
          Width = 65
          Height = 17
          Caption = 'TID'
          TabOrder = 1
        end
        object RadioButton5: TRadioButton
          Left = 136
          Top = 16
          Width = 65
          Height = 17
          Caption = 'User'
          TabOrder = 2
        end
        object Edit7: TEdit
          Left = 136
          Top = 35
          Width = 36
          Height = 21
          MaxLength = 4
          TabOrder = 3
          Text = '0000'
          OnKeyPress = Edit2KeyPress
        end
        object Edit9: TEdit
          Left = 275
          Top = 33
          Width = 30
          Height = 21
          MaxLength = 2
          TabOrder = 4
          Text = '00'
          OnKeyPress = Edit2KeyPress
        end
        object Edit10: TEdit
          Left = 98
          Top = 62
          Width = 130
          Height = 21
          TabOrder = 5
          Text = '00'
          OnKeyPress = Edit2KeyPress
        end
        object Button10: TButton
          Left = 232
          Top = 60
          Width = 75
          Height = 25
          Caption = 'Set'
          TabOrder = 6
          OnClick = Button10Click
        end
      end
      object GroupBox35: TGroupBox
        Left = 560
        Top = 4
        Width = 213
        Height = 93
        Caption = 'Response conditions  Setting'
        TabOrder = 2
        object Label57: TLabel
          Left = 11
          Top = 20
          Width = 76
          Height = 13
          Caption = 'RepCondition'#65306
        end
        object Label58: TLabel
          Left = 13
          Top = 42
          Width = 85
          Height = 13
          Caption = 'RepPauseTime'#65306
        end
        object Label59: TLabel
          Left = 166
          Top = 41
          Width = 15
          Height = 13
          Caption = '*1s'
        end
        object ComboBox1: TComboBox
          Left = 104
          Top = 13
          Width = 101
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox1Change
          Items.Strings = (
            'Command notify'
            'Timer notify'
            'Add-in notify'
            'Delete notify'
            'Change notify')
        end
        object ComboBox2: TComboBox
          Left = 107
          Top = 38
          Width = 56
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Button11: TButton
          Left = 128
          Top = 62
          Width = 75
          Height = 25
          Caption = 'Set'
          TabOrder = 2
          OnClick = Button11Click
        end
      end
      object GroupBox36: TGroupBox
        Left = 7
        Top = 234
        Width = 225
        Height = 50
        Caption = 'Inventory Interval'
        TabOrder = 3
        object Label60: TLabel
          Left = 8
          Top = 21
          Width = 55
          Height = 13
          Caption = 'Pulse Time:'
        end
        object ComboBox3: TComboBox
          Left = 71
          Top = 16
          Width = 72
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            '10ms'
            '20ms'
            '30ms'
            '50ms'
            '100ms')
        end
        object Button12: TButton
          Left = 152
          Top = 13
          Width = 63
          Height = 25
          Caption = 'Set'
          TabOrder = 1
          OnClick = Button12Click
        end
      end
      object GroupBox37: TGroupBox
        Left = 8
        Top = 105
        Width = 765
        Height = 66
        Caption = 'Query Tag Type'
        TabOrder = 4
        object GroupBox38: TGroupBox
          Left = 9
          Top = 16
          Width = 206
          Height = 40
          Caption = 'Protocl'
          TabOrder = 0
          object RadioButton6: TRadioButton
            Left = 6
            Top = 16
            Width = 83
            Height = 17
            Caption = 'EPCC1-G2'
            TabOrder = 0
          end
          object RadioButton7: TRadioButton
            Left = 97
            Top = 16
            Width = 90
            Height = 17
            Caption = '18000-6B'
            TabOrder = 1
          end
        end
        object RadioButton8: TRadioButton
          Left = 223
          Top = 14
          Width = 113
          Height = 17
          Caption = 'Query G2 Tag'
          TabOrder = 1
        end
        object RadioButton9: TRadioButton
          Left = 385
          Top = 13
          Width = 194
          Height = 17
          Caption = 'Detect EAS before query G2 tag'
          TabOrder = 2
        end
        object RadioButton10: TRadioButton
          Left = 624
          Top = 13
          Width = 113
          Height = 17
          Caption = 'Detect EAS'
          TabOrder = 3
        end
        object Button13: TButton
          Left = 683
          Top = 35
          Width = 73
          Height = 25
          Action = Action_TagProtocol
          Caption = 'Set'
          TabOrder = 4
        end
        object RadioButton13: TRadioButton
          Left = 223
          Top = 38
          Width = 138
          Height = 17
          Caption = 'Query G2 Tag'#8216's TID'
          TabOrder = 5
        end
        object RadioButton14: TRadioButton
          Left = 386
          Top = 38
          Width = 231
          Height = 17
          Caption = 'Detect EAS before query G2 tag'#39's TID'
          TabOrder = 6
        end
      end
      object GroupBox39: TGroupBox
        Left = 238
        Top = 233
        Width = 292
        Height = 51
        Caption = 'Work Mode'
        TabOrder = 5
        object Label61: TLabel
          Left = 8
          Top = 23
          Width = 72
          Height = 13
          Caption = 'Mode Select'#65306
        end
        object ComboBox4: TComboBox
          Left = 89
          Top = 16
          Width = 127
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'Answer Mode'
            'Auto_running mode'
            'Trigger mode(Low)'
            'Trigger mode(High)')
        end
        object Button17: TButton
          Left = 221
          Top = 13
          Width = 64
          Height = 25
          Caption = 'Set'
          TabOrder = 1
          OnClick = Button17Click
        end
      end
      object Button18: TButton
        Left = 544
        Top = 247
        Width = 225
        Height = 26
        Caption = 'Get System Parameter'
        TabOrder = 6
        OnClick = Button18Click
      end
      object GroupBox40: TGroupBox
        Left = 8
        Top = 287
        Width = 764
        Height = 170
        Caption = 'Reader storage blocks Tag information'
        TabOrder = 7
        object Button19: TButton
          Left = 437
          Top = 136
          Width = 155
          Height = 25
          Caption = 'Get Tag Buffer Info'
          Enabled = False
          TabOrder = 0
          OnClick = Button19Click
        end
        object ListView1: TListView
          Left = 8
          Top = 16
          Width = 745
          Height = 113
          Columns = <
            item
              Caption = 'NO.'
            end
            item
              Caption = 'EPC'
              Width = 290
            end
            item
              Caption = 'First read tag time'
              Width = 150
            end
            item
              Caption = 'Last read tag time'
              Width = 150
            end
            item
              Caption = 'ANT'
            end
            item
              Caption = 'Times'
            end>
          GridLines = True
          TabOrder = 1
          ViewStyle = vsReport
        end
        object Button_clearBuffer: TButton
          Left = 597
          Top = 136
          Width = 155
          Height = 25
          Caption = 'Clear Tag Buffer'
          TabOrder = 2
          OnClick = Button_clearBufferClick
        end
      end
      object GroupBox41: TGroupBox
        Left = 8
        Top = 459
        Width = 766
        Height = 165
        Caption = 'Read Auto_running Mode Data'
        TabOrder = 8
        object SpeedButton1: TSpeedButton
          Left = 539
          Top = 134
          Width = 104
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Get'
          OnClick = SpeedButton1Click
        end
        object Memo1: TMemo
          Left = 9
          Top = 20
          Width = 744
          Height = 109
          ReadOnly = True
          TabOrder = 0
        end
        object Button20: TButton
          Left = 652
          Top = 134
          Width = 103
          Height = 25
          Caption = 'Clear'
          TabOrder = 1
          OnClick = Button20Click
        end
      end
      object GroupBox42: TGroupBox
        Left = 8
        Top = 172
        Width = 294
        Height = 57
        Caption = 'Set Trigger-Time'
        TabOrder = 9
        object Label51: TLabel
          Left = 5
          Top = 26
          Width = 62
          Height = 13
          Caption = 'Trigger Time:'
        end
        object Label52: TLabel
          Left = 191
          Top = 26
          Width = 15
          Height = 13
          Caption = '*1s'
        end
        object ComboBox5: TComboBox
          Left = 71
          Top = 21
          Width = 114
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Button6: TButton
          Left = 220
          Top = 18
          Width = 64
          Height = 25
          Caption = 'Set'
          TabOrder = 1
          OnClick = Button6Click
        end
      end
      object GroupBox43: TGroupBox
        Left = 306
        Top = 174
        Width = 466
        Height = 56
        Caption = 'TID Parameter Setting'
        TabOrder = 10
        object Label65: TLabel
          Left = 8
          Top = 28
          Width = 75
          Height = 13
          Caption = 'Start Address'#65306
        end
        object Label66: TLabel
          Left = 195
          Top = 28
          Width = 84
          Height = 13
          Caption = 'Data-word-num'#65306
        end
        object Button7: TButton
          Left = 392
          Top = 16
          Width = 63
          Height = 25
          Caption = 'Set'
          TabOrder = 0
          OnClick = Button7Click
        end
        object Edit11: TEdit
          Left = 79
          Top = 20
          Width = 105
          Height = 21
          MaxLength = 2
          TabOrder = 1
          Text = '02'
          OnKeyPress = Edit2KeyPress
        end
        object Edit12: TEdit
          Left = 277
          Top = 20
          Width = 110
          Height = 21
          MaxLength = 2
          TabOrder = 2
          Text = '04'
          OnKeyPress = Edit2KeyPress
        end
      end
    end
    object TabSheet_EPCC1G2: TTabSheet
      Caption = 'EPCC1-G2 Test'
      ImageIndex = 2
      object GroupBox11: TGroupBox
        Left = 8
        Top = 0
        Width = 480
        Height = 177
        Caption = 'List EPC of Tags'
        TabOrder = 0
        object Label89: TLabel
          Left = 11
          Top = 18
          Width = 70
          Height = 13
          Caption = 'Current EPC'#65306
        end
        object Label90: TLabel
          Left = 294
          Top = 26
          Width = 76
          Height = 13
          Caption = 'Total Number'#65306
        end
        object ListView_EPC: TListView
          Left = 8
          Top = 59
          Width = 465
          Height = 113
          Columns = <
            item
              Caption = 'No.'
            end
            item
              Caption = 'EPC'
              Width = 210
            end
            item
              Caption = 'EPC Length'
              Width = 70
            end
            item
              Caption = 'ANT(4,3,2,1)'
              Width = 80
            end
            item
              Caption = 'Times'
            end>
          GridLines = True
          TabOrder = 0
          ViewStyle = vsReport
        end
        object Edit17: TEdit
          Left = 10
          Top = 33
          Width = 268
          Height = 21
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 1
        end
        object Edit18: TEdit
          Left = 370
          Top = 11
          Width = 102
          Height = 45
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clBlue
          Font.Height = -32
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 2
        end
      end
      object GroupBox17: TGroupBox
        Left = 493
        Top = 0
        Width = 280
        Height = 79
        Caption = 'Query Tag'
        TabOrder = 1
        object Label25: TLabel
          Left = 10
          Top = 16
          Width = 67
          Height = 13
          Caption = 'Read Interval:'
        end
        object SpeedButton_Query: TSpeedButton
          Left = 192
          Top = 10
          Width = 81
          Height = 25
          AllowAllUp = True
          GroupIndex = 1
          Caption = 'Query Tag'
          OnClick = SpeedButton_QueryClick
        end
        object ComboBox_IntervalTime: TComboBox
          Left = 79
          Top = 12
          Width = 109
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTimeChange
        end
        object GroupBox44: TGroupBox
          Left = 4
          Top = 34
          Width = 205
          Height = 41
          Caption = 'Query TID Parameter'
          TabOrder = 1
          object Label67: TLabel
            Left = 9
            Top = 21
            Width = 47
            Height = 13
            Caption = 'StartAddr:'
          end
          object Label68: TLabel
            Left = 120
            Top = 21
            Width = 21
            Height = 13
            Caption = 'Len:'
          end
          object Edit13: TEdit
            Left = 58
            Top = 16
            Width = 53
            Height = 21
            TabOrder = 0
            Text = '02'
          end
          object Edit14: TEdit
            Left = 145
            Top = 15
            Width = 51
            Height = 21
            TabOrder = 1
            Text = '04'
          end
        end
        object CheckBox_TID: TCheckBox
          Left = 214
          Top = 50
          Width = 55
          Height = 17
          Caption = 'TID'
          TabOrder = 2
          OnClick = CheckBox_TIDClick
        end
      end
      object GroupBox9: TGroupBox
        Left = 493
        Top = 80
        Width = 280
        Height = 69
        Caption = 'Kill Tag'
        TabOrder = 2
        object Label33: TLabel
          Left = 9
          Top = 36
          Width = 62
          Height = 26
          Caption = 'Kill Password'#13#10'(8 Hex):'
        end
        object Button_DestroyCard: TButton
          Left = 192
          Top = 40
          Width = 81
          Height = 23
          Action = Action_DestroyCard
          Caption = 'Kill Tag'
          TabOrder = 2
        end
        object Edit_DestroyCode: TEdit
          Left = 82
          Top = 41
          Width = 91
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object ComboBox_EPC3: TComboBox
          Tag = 3
          Left = 10
          Top = 15
          Width = 261
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
      end
      object GroupBox23: TGroupBox
        Left = 492
        Top = 150
        Width = 281
        Height = 77
        Caption = 'Write EPC(Random write one tag in the antenna)'
        TabOrder = 3
        object Label38: TLabel
          Left = 9
          Top = 43
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object Label39: TLabel
          Left = 8
          Top = 16
          Width = 53
          Height = 26
          Caption = 'Write EPC:'#13#10'(1-15Word)'
        end
        object Edit_AccessCode3: TEdit
          Left = 100
          Top = 48
          Width = 76
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object Button_WriteEPC_G2: TButton
          Left = 192
          Top = 44
          Width = 81
          Height = 25
          Action = Action_WriteEPC_G2
          Caption = 'Write EPC'
          TabOrder = 1
        end
        object Edit_WriteEPC: TEdit
          Left = 66
          Top = 17
          Width = 208
          Height = 21
          MaxLength = 60
          TabOrder = 0
          Text = '0000'
          OnKeyPress = Edit2KeyPress
        end
      end
      object GroupBox20: TGroupBox
        Left = 492
        Top = 229
        Width = 281
        Height = 178
        Caption = 'Read Protection'
        TabOrder = 4
        object Label32: TLabel
          Left = 9
          Top = 35
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object ComboBox_EPC4: TComboBox
          Tag = 3
          Left = 10
          Top = 15
          Width = 262
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode4: TEdit
          Left = 97
          Top = 39
          Width = 175
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object Button_SetReadProtect_G2: TButton
          Left = 9
          Top = 63
          Width = 264
          Height = 25
          Action = Action_SetReadProtect_G2
          Caption = 'Set Privacy By EPC'
          TabOrder = 1
        end
        object Button_SetMultiReadProtect_G2: TButton
          Left = 8
          Top = 91
          Width = 265
          Height = 25
          Action = Action_SetMultiReadProtect_G2
          Caption = 'Set Privacy Without EPC'
          TabOrder = 3
        end
        object Button_RemoveReadProtect_G2: TButton
          Left = 8
          Top = 119
          Width = 265
          Height = 25
          Action = Action_RemoveReadProtect_G2
          Caption = 'Reset Privacy'
          TabOrder = 4
        end
        object Button_CheckReadProtected_G2: TButton
          Left = 8
          Top = 147
          Width = 265
          Height = 25
          Action = Action_CheckReadProtected_G2
          Caption = 'Check Privacy'
          TabOrder = 5
        end
      end
      object GroupBox21: TGroupBox
        Left = 491
        Top = 408
        Width = 281
        Height = 116
        Caption = 'EAS Alarm'
        TabOrder = 5
        object Label35: TLabel
          Left = 9
          Top = 44
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object SpeedButton_CheckAlarm_G2: TSpeedButton
          Left = 192
          Top = 82
          Width = 81
          Height = 25
          AllowAllUp = True
          GroupIndex = 3
          Caption = 'EAS Alarm'
          OnClick = SpeedButton_CheckAlarm_G2Click
        end
        object Label_Alarm: TLabel
          Left = 216
          Top = 43
          Width = 30
          Height = 30
          Caption = #9679
          Color = clBtnFace
          Font.Charset = GB2312_CHARSET
          Font.Color = clRed
          Font.Height = -30
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Visible = False
        end
        object Button_SetEASAlarm_G2: TButton
          Left = 96
          Top = 82
          Width = 81
          Height = 25
          Action = Action_SetEASAlarm_G2
          Caption = 'EAS Configure'
          TabOrder = 3
        end
        object ComboBox_EPC5: TComboBox
          Tag = 3
          Left = 10
          Top = 18
          Width = 264
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode5: TEdit
          Left = 97
          Top = 47
          Width = 76
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object GroupBox24: TGroupBox
          Left = 10
          Top = 69
          Width = 79
          Height = 43
          TabOrder = 2
          object Alarm_G2: TRadioButton
            Left = 9
            Top = 8
            Width = 57
            Height = 17
            Caption = 'Alarm'
            TabOrder = 0
          end
          object NoAlarm_G2: TRadioButton
            Left = 9
            Top = 24
            Width = 65
            Height = 17
            Caption = 'No Alarm'
            TabOrder = 1
          end
        end
      end
      object GroupBox22: TGroupBox
        Left = 491
        Top = 524
        Width = 281
        Height = 97
        Caption = 'Lock Block for User (Permanently Lock)'
        TabOrder = 6
        object Label36: TLabel
          Left = 10
          Top = 42
          Width = 98
          Height = 26
          Caption = 'Address of Tag Data'#13#10'(Word):'
        end
        object Label37: TLabel
          Left = 10
          Top = 68
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object Button_LockUserBlock_G2: TButton
          Left = 188
          Top = 68
          Width = 81
          Height = 25
          Action = Action_LockUserBlock_G2
          Caption = 'Block Lock'
          TabOrder = 2
        end
        object ComboBox_BlockNum: TComboBox
          Left = 118
          Top = 42
          Width = 153
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object ComboBox_EPC6: TComboBox
          Tag = 3
          Left = 10
          Top = 16
          Width = 261
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode6: TEdit
          Left = 103
          Top = 69
          Width = 76
          Height = 21
          MaxLength = 8
          TabOrder = 3
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
      end
      object GroupBox5: TGroupBox
        Left = 8
        Top = 446
        Width = 481
        Height = 175
        Caption = 'Set Protect For Reading Or Writing'
        TabOrder = 7
        object Label24: TLabel
          Left = 272
          Top = 133
          Width = 124
          Height = 13
          Caption = 'Access Password (8 Hex):'
        end
        object ComboBox_EPC1: TComboBox
          Tag = 1
          Left = 8
          Top = 18
          Width = 259
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Button_SetProtectState: TButton
          Left = 382
          Top = 146
          Width = 92
          Height = 25
          Action = Action_SetProtectState
          Caption = 'Lock'
          TabOrder = 4
        end
        object Edit_AccessCode1: TEdit
          Left = 272
          Top = 148
          Width = 89
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object GroupBox1: TGroupBox
          Left = 273
          Top = 10
          Width = 202
          Height = 31
          TabOrder = 0
          object P_Reserve: TRadioButton
            Left = 5
            Top = 8
            Width = 68
            Height = 17
            Caption = 'Password'
            TabOrder = 0
          end
          object P_EPC: TRadioButton
            Left = 72
            Top = 8
            Width = 45
            Height = 17
            Caption = 'EPC'
            TabOrder = 1
          end
          object P_TID: TRadioButton
            Left = 114
            Top = 8
            Width = 42
            Height = 17
            Caption = 'TID'
            TabOrder = 2
          end
          object P_User: TRadioButton
            Left = 152
            Top = 8
            Width = 48
            Height = 17
            Caption = 'User'
            TabOrder = 3
          end
        end
        object GroupBox16: TGroupBox
          Left = 8
          Top = 44
          Width = 257
          Height = 126
          Caption = 'Lock of Password'
          TabOrder = 2
          object GroupBox4: TGroupBox
            Left = 8
            Top = 12
            Width = 240
            Height = 41
            TabOrder = 0
            object DestroyCode: TRadioButton
              Left = 8
              Top = 14
              Width = 93
              Height = 17
              Caption = 'Kill Password'
              TabOrder = 0
            end
            object AccessCode: TRadioButton
              Left = 120
              Top = 14
              Width = 110
              Height = 17
              Caption = 'Access Password'
              TabOrder = 1
            end
          end
          object NoProect: TRadioButton
            Left = 4
            Top = 56
            Width = 231
            Height = 17
            Caption = 'Readable and  writeable from any state'
            TabOrder = 1
          end
          object Always: TRadioButton
            Left = 4
            Top = 88
            Width = 209
            Height = 17
            Caption = 'Permanently readable and writeable'
            TabOrder = 3
          end
          object Proect: TRadioButton
            Left = 4
            Top = 72
            Width = 221
            Height = 17
            Caption = 'Readable and writeable from the secured state'
            TabOrder = 2
          end
          object AlwaysNot: TRadioButton
            Left = 4
            Top = 106
            Width = 207
            Height = 17
            Caption = 'Never readable and writeable'
            TabOrder = 4
          end
        end
        object GroupBox18: TGroupBox
          Left = 272
          Top = 44
          Width = 202
          Height = 86
          Caption = 'Lock of EPC TID and User Bank'
          TabOrder = 3
          object NoProect2: TRadioButton
            Left = 8
            Top = 14
            Width = 129
            Height = 17
            Caption = 'Writeable from any state'
            TabOrder = 0
          end
          object AlwaysNot2: TRadioButton
            Left = 8
            Top = 64
            Width = 113
            Height = 17
            Caption = 'Never writeable'
            TabOrder = 3
          end
          object Proect2: TRadioButton
            Left = 8
            Top = 30
            Width = 137
            Height = 17
            Caption = 'Writeable from the secured state'
            TabOrder = 1
          end
          object Always2: TRadioButton
            Left = 8
            Top = 47
            Width = 113
            Height = 17
            Caption = 'Permanently writeable'
            TabOrder = 2
          end
        end
      end
      object GroupBox10: TGroupBox
        Left = 8
        Top = 248
        Width = 481
        Height = 199
        Caption = 'Read Data / Write Data / Block Erase'
        TabOrder = 8
        object Label9: TLabel
          Left = 8
          Top = 117
          Width = 140
          Height = 26
          Caption = 'Password(Read/Block Erase)'#13#10'(0-120/Word/D):'
        end
        object Label18: TLabel
          Left = 8
          Top = 147
          Width = 82
          Height = 13
          Caption = 'Write Data (Hex):'
        end
        object Label19: TLabel
          Left = 9
          Top = 74
          Width = 157
          Height = 13
          Caption = 'Address of Tag Data(Word/Hex):'
        end
        object Label20: TLabel
          Left = 11
          Top = 100
          Width = 165
          Height = 13
          Caption = 'Length of Data(Read/Block Erase:'
        end
        object SpeedButton_Read_G2: TSpeedButton
          Left = 6
          Top = 169
          Width = 42
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Read'
          OnClick = SpeedButton_Read_G2Click
        end
        object ComboBox_EPC2: TComboBox
          Tag = 2
          Left = 8
          Top = 16
          Width = 298
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Edit_AccessCode2: TEdit
          Left = 156
          Top = 120
          Width = 146
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = Edit2KeyPress
        end
        object Edit_WriteData: TEdit
          Left = 100
          Top = 144
          Width = 201
          Height = 21
          TabOrder = 6
          Text = '0000'
          OnChange = Edit_WriteDataChange
          OnKeyPress = Edit2KeyPress
        end
        object Edit_WordPtr: TEdit
          Left = 185
          Top = 73
          Width = 117
          Height = 21
          MaxLength = 2
          TabOrder = 3
          Text = '00'
          OnKeyPress = Edit2KeyPress
        end
        object Edit_Len: TEdit
          Left = 183
          Top = 96
          Width = 119
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '4'
          OnKeyPress = Edit2KeyPress
        end
        object Memo_DataShow: TMemo
          Left = 312
          Top = 40
          Width = 161
          Height = 140
          ScrollBars = ssVertical
          TabOrder = 0
        end
        object GroupBox6: TGroupBox
          Left = 8
          Top = 36
          Width = 296
          Height = 33
          TabOrder = 2
          object C_Reserve: TRadioButton
            Left = 2
            Top = 10
            Width = 65
            Height = 17
            Caption = 'Password'
            TabOrder = 0
            OnClick = C_ReserveClick
          end
          object C_EPC: TRadioButton
            Left = 76
            Top = 10
            Width = 49
            Height = 17
            Caption = 'EPC'
            TabOrder = 1
            OnClick = C_EPCClick
          end
          object C_TID: TRadioButton
            Left = 137
            Top = 10
            Width = 51
            Height = 17
            Caption = 'TID'
            TabOrder = 2
            OnClick = C_TIDClick
          end
          object C_User: TRadioButton
            Left = 195
            Top = 10
            Width = 60
            Height = 17
            Caption = 'User'
            TabOrder = 3
            OnClick = C_UserClick
          end
        end
        object Button16: TButton
          Left = 241
          Top = 169
          Width = 61
          Height = 25
          Caption = 'Clear'
          TabOrder = 9
          OnClick = Button16Click
        end
        object Button_DataWrite: TButton
          Left = 52
          Top = 169
          Width = 40
          Height = 25
          Action = Action_ShowOrChangeData_write
          Caption = 'Write'
          TabOrder = 7
        end
        object Button_BlockErase: TButton
          Left = 170
          Top = 169
          Width = 66
          Height = 25
          Action = Action_ShowOrChangeData_BlockErase
          Caption = 'Block Erase'
          TabOrder = 8
        end
        object Button_BlockWrite: TButton
          Left = 97
          Top = 169
          Width = 66
          Height = 25
          Action = Action_ShowOrChangeData_BlockWrite
          Caption = 'Block Write'
          TabOrder = 10
        end
        object Edit_PC: TEdit
          Left = 438
          Top = 12
          Width = 35
          Height = 21
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 11
          Text = '0800'
        end
        object CheckBox18: TCheckBox
          Left = 312
          Top = 16
          Width = 123
          Height = 17
          Caption = 'Compute and add PC: '
          TabOrder = 12
          OnClick = CheckBox18Click
        end
      end
      object GroupBox32: TGroupBox
        Left = 8
        Top = 177
        Width = 481
        Height = 71
        Caption = 'Mask conditions'
        TabOrder = 9
        object Label42: TLabel
          Left = 8
          Top = 19
          Width = 144
          Height = 13
          Caption = 'Mask Start Bit Address(Hex)'#65306
        end
        object Label43: TLabel
          Left = 232
          Top = 17
          Width = 114
          Height = 13
          Caption = 'Mask Bit Length(Hex)'#65306
        end
        object Label21: TLabel
          Left = 232
          Top = 45
          Width = 89
          Height = 13
          Caption = 'Mask Data(Hex)'#65306
        end
        object CheckBox1: TCheckBox
          Left = 415
          Top = 9
          Width = 57
          Height = 17
          Caption = 'Enable'
          TabOrder = 0
        end
        object Edit2: TEdit
          Left = 144
          Top = 12
          Width = 57
          Height = 21
          MaxLength = 4
          TabOrder = 1
          Text = '0000'
          OnKeyPress = Edit2KeyPress
        end
        object Edit3: TEdit
          Left = 339
          Top = 9
          Width = 57
          Height = 21
          MaxLength = 2
          TabOrder = 2
          Text = '00'
          OnKeyPress = Edit2KeyPress
        end
        object GroupBox3: TGroupBox
          Left = 7
          Top = 33
          Width = 194
          Height = 35
          TabOrder = 3
          object R_EPC: TRadioButton
            Left = 7
            Top = 14
            Width = 57
            Height = 17
            Caption = 'EPC'
            TabOrder = 0
          end
          object R_TID: TRadioButton
            Left = 71
            Top = 14
            Width = 56
            Height = 17
            Caption = 'TID'
            TabOrder = 1
          end
          object R_User: TRadioButton
            Left = 125
            Top = 14
            Width = 65
            Height = 17
            Caption = 'User'
            TabOrder = 2
          end
        end
        object Edit1: TEdit
          Left = 320
          Top = 39
          Width = 153
          Height = 21
          TabOrder = 4
          Text = '00'
          OnKeyPress = Edit2KeyPress
        end
      end
    end
    object TabSheet_6B: TTabSheet
      Caption = '18000-6B Test'
      ImageIndex = 3
      object GroupBox12: TGroupBox
        Left = 8
        Top = 4
        Width = 769
        Height = 309
        Caption = 'List ID of Tags'
        TabOrder = 0
        object ListView_ID_6B: TListView
          Left = 8
          Top = 16
          Width = 750
          Height = 282
          Columns = <
            item
              Caption = 'No.'
            end
            item
              Caption = 'ID'
              Width = 500
            end
            item
              Caption = 'Times'
            end>
          GridLines = True
          TabOrder = 0
          ViewStyle = vsReport
        end
      end
      object GroupBox19: TGroupBox
        Left = 8
        Top = 316
        Width = 321
        Height = 132
        Caption = 'Query Tag'
        TabOrder = 1
        object Label27: TLabel
          Left = 8
          Top = 30
          Width = 67
          Height = 13
          Caption = 'Read Interval:'
        end
        object SpeedButton_Query_6B: TSpeedButton
          Left = 221
          Top = 79
          Width = 89
          Height = 26
          AllowAllUp = True
          GroupIndex = 1
          Caption = 'Query by one'
          OnClick = Action_Query_6BExecute
        end
        object ComboBox_IntervalTime_6B: TComboBox
          Left = 104
          Top = 25
          Width = 207
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTime_6BChange
        end
        object Byone_6B: TRadioButton
          Left = 8
          Top = 70
          Width = 73
          Height = 17
          Caption = 'Query by one'
          TabOrder = 1
        end
        object Bycondition_6B: TRadioButton
          Left = 8
          Top = 98
          Width = 81
          Height = 17
          Caption = 'Query by Condition'
          TabOrder = 2
        end
      end
      object GroupBox13: TGroupBox
        Left = 336
        Top = 316
        Width = 441
        Height = 304
        Caption = 
          'Read and Write Data Block / Permanently Write  Protect Block of ' +
          ' byte'
        TabOrder = 2
        object Label29: TLabel
          Left = 9
          Top = 90
          Width = 165
          Height = 13
          Caption = 'Write Data (1-32 Byte/Hex):           '
        end
        object Label30: TLabel
          Left = 9
          Top = 49
          Width = 102
          Height = 26
          Caption = 'Start/Protect Address'#13#10'(00-E9)(Hex):   '
        end
        object Label31: TLabel
          Left = 242
          Top = 49
          Width = 75
          Height = 26
          Caption = 'Length of Data:'#13#10'(1-32/Byte/D)   '
        end
        object SpeedButton_Read_6B: TSpeedButton
          Left = 8
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Read'
          OnClick = SpeedButton_Read_6BClick
        end
        object SpeedButton_Write_6B: TSpeedButton
          Left = 66
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Write'
          OnClick = SpeedButton_Read_6BClick
        end
        object ComboBox_ID1_6B: TComboBox
          Tag = 2
          Left = 9
          Top = 20
          Width = 422
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_WriteData_6B: TEdit
          Left = 160
          Top = 85
          Width = 269
          Height = 21
          TabOrder = 3
          Text = '0000'
        end
        object Edit_StartAddress_6B: TEdit
          Left = 115
          Top = 54
          Width = 81
          Height = 21
          MaxLength = 2
          TabOrder = 1
          Text = '00'
        end
        object Edit_Len_6B: TEdit
          Left = 320
          Top = 54
          Width = 109
          Height = 21
          MaxLength = 2
          TabOrder = 2
          Text = '12'
        end
        object Memo_DataShow_6B: TMemo
          Left = 8
          Top = 152
          Width = 420
          Height = 143
          ScrollBars = ssVertical
          TabOrder = 7
        end
        object Button14: TButton
          Left = 123
          Top = 117
          Width = 102
          Height = 25
          Action = Action_LockByte_6B
          Caption = 'Lock'
          TabOrder = 4
        end
        object Button15: TButton
          Left = 235
          Top = 117
          Width = 126
          Height = 25
          Action = Action_CheckLock_6B
          Caption = 'Check Lock'
          TabOrder = 5
        end
        object Button22: TButton
          Left = 368
          Top = 117
          Width = 60
          Height = 25
          Caption = 'Clear'
          TabOrder = 6
          OnClick = Button22Click
        end
      end
      object GroupBox14: TGroupBox
        Left = 8
        Top = 452
        Width = 321
        Height = 168
        Caption = 'Query Tags by Condition'
        TabOrder = 3
        object Label34: TLabel
          Left = 8
          Top = 132
          Width = 133
          Height = 13
          Caption = 'Condition(<=8 Hex Number):'
        end
        object Label28: TLabel
          Left = 8
          Top = 92
          Width = 134
          Height = 13
          Caption = 'Address of Tag Data(0-223):'
        end
        object Edit_Query_StartAddress_6B: TEdit
          Left = 160
          Top = 87
          Width = 97
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '0'
        end
        object Edit_ConditionContent_6B: TEdit
          Left = 160
          Top = 124
          Width = 97
          Height = 21
          MaxLength = 16
          TabOrder = 5
          Text = '00'
        end
        object Less_6B: TRadioButton
          Left = 8
          Top = 56
          Width = 81
          Height = 17
          Caption = 'Less than Condition'
          TabOrder = 2
        end
        object Different_6B: TRadioButton
          Left = 160
          Top = 24
          Width = 113
          Height = 17
          Caption = 'Unequal Condition'
          TabOrder = 1
        end
        object Same_6B: TRadioButton
          Left = 8
          Top = 24
          Width = 113
          Height = 17
          Caption = 'Equal Condition'
          TabOrder = 0
        end
        object Greater_6B: TRadioButton
          Left = 160
          Top = 56
          Width = 81
          Height = 17
          Caption = 'Greater than Condition'
          TabOrder = 3
        end
      end
    end
    object TabSheet3: TTabSheet
      Caption = 'Frequency Analysis'
      ImageIndex = 4
      object Label62: TLabel
        Left = 42
        Top = 11
        Width = 71
        Height = 13
        AutoSize = False
        Caption = 'Frequency'
      end
      object Label63: TLabel
        Left = 204
        Top = 11
        Width = 121
        Height = 13
        AutoSize = False
        Caption = 'Times'
      end
      object Label64: TLabel
        Left = 388
        Top = 11
        Width = 55
        Height = 13
        Caption = 'Percentage'
      end
      object ListBox1: TListBox
        Left = 12
        Top = 27
        Width = 761
        Height = 561
        BiDiMode = bdLeftToRight
        Color = clBtnHighlight
        Ctl3D = False
        ExtendedSelect = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Courier'
        Font.Style = []
        ItemHeight = 13
        ParentBiDiMode = False
        ParentCtl3D = False
        ParentFont = False
        ParentShowHint = False
        ShowHint = False
        TabOrder = 0
      end
      object Button21: TButton
        Left = 509
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Start'
        Enabled = False
        TabOrder = 1
        OnClick = Button21Click
      end
      object Button23: TButton
        Left = 600
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Stop'
        Enabled = False
        TabOrder = 2
        OnClick = Button23Click
      end
      object Button24: TButton
        Left = 688
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Clear'
        TabOrder = 3
        OnClick = Button24Click
      end
    end
    object TabSheet4: TTabSheet
      Caption = 'TCPIP net comfig'
      ImageIndex = 5
      object GroupBox15: TGroupBox
        Left = 2
        Top = 30
        Width = 767
        Height = 591
        Caption = 'List  of device'
        TabOrder = 0
        object ListView2: TListView
          Left = 8
          Top = 15
          Width = 753
          Height = 569
          Columns = <
            item
              Caption = 'Device Name'
              Width = 210
            end
            item
              Caption = 'Device IP'
              Width = 210
            end
            item
              Caption = 'Device Mac'
              Width = 210
            end>
          GridLines = True
          RowSelect = True
          TabOrder = 0
          ViewStyle = vsReport
          OnDblClick = ListView2DblClick
        end
      end
      object ToolBar1: TToolBar
        Left = 0
        Top = 0
        Width = 778
        Height = 29
        ButtonHeight = 0
        ButtonWidth = 0
        Caption = 'ToolBar1'
        Flat = True
        Menu = MainMenu1
        ShowCaptions = True
        TabOrder = 1
      end
    end
    object TabSheet5: TTabSheet
      Caption = 'TCPIP serial config'
      ImageIndex = 6
      object GroupBox46: TGroupBox
        Left = 5
        Top = 1
        Width = 537
        Height = 105
        Caption = 'Serial setting'
        TabOrder = 0
        object Label69: TLabel
          Left = 23
          Top = 24
          Width = 39
          Height = 13
          Caption = 'Protocol'
        end
        object Label70: TLabel
          Left = 24
          Top = 52
          Width = 51
          Height = 13
          Caption = 'Baud Rate'
        end
        object Label71: TLabel
          Left = 26
          Top = 80
          Width = 26
          Height = 13
          Caption = 'Parity'
        end
        object Label72: TLabel
          Left = 203
          Top = 23
          Width = 23
          Height = 13
          Caption = 'FIFO'
        end
        object Label73: TLabel
          Left = 301
          Top = 23
          Width = 43
          Height = 13
          Caption = 'Data Bits'
        end
        object Label74: TLabel
          Left = 224
          Top = 52
          Width = 58
          Height = 13
          Caption = 'Flow Control'
        end
        object Label75: TLabel
          Left = 225
          Top = 80
          Width = 42
          Height = 13
          Caption = 'Stop Bits'
        end
        object protocolCB: TComboBox
          Left = 88
          Top = 18
          Width = 86
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'RS232'
            'RS422'
            'RS485')
        end
        object baudrateCB: TComboBox
          Left = 88
          Top = 48
          Width = 111
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            '110'
            '134'
            '150'
            '300'
            '600'
            '1200'
            '1800'
            '2400'
            '4800'
            '7200'
            '9600'
            '14400'
            '19200'
            '38400'
            '57600'
            '115200'
            '230400'
            '460800')
        end
        object parityCB: TComboBox
          Left = 88
          Top = 76
          Width = 111
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            'NONE'
            'ODD'
            'EVEN'
            'MARK'
            'SPACE')
        end
        object fifoCB: TComboBox
          Left = 235
          Top = 16
          Width = 49
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 3
          Items.Strings = (
            '14'
            '8'
            '4')
        end
        object databitCB: TComboBox
          Left = 352
          Top = 16
          Width = 49
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 4
          Items.Strings = (
            '5'
            '6'
            '7'
            '8')
        end
        object flowCB: TComboBox
          Left = 292
          Top = 48
          Width = 111
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 5
          Items.Strings = (
            'None'
            'Software'
            'Hardware')
        end
        object stopbitCB: TComboBox
          Left = 292
          Top = 76
          Width = 111
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 6
          Items.Strings = (
            '1'
            '1.5'
            '2')
        end
        object Button9: TButton
          Left = 448
          Top = 40
          Width = 75
          Height = 25
          Caption = 'Get'
          TabOrder = 7
          OnClick = Button9Click
        end
        object Button25: TButton
          Left = 448
          Top = 72
          Width = 75
          Height = 25
          Caption = 'Set'
          TabOrder = 8
          OnClick = Button25Click
        end
      end
      object GroupBox47: TGroupBox
        Left = 5
        Top = 110
        Width = 538
        Height = 145
        Caption = 'Connection setting'
        TabOrder = 1
        object Label76: TLabel
          Left = 24
          Top = 24
          Width = 53
          Height = 13
          Caption = 'Worked As'
        end
        object Label77: TLabel
          Left = 211
          Top = 24
          Width = 73
          Height = 13
          Caption = 'Active Connect'
        end
        object Label78: TLabel
          Left = 24
          Top = 55
          Width = 62
          Height = 13
          Caption = 'Remote Host'
        end
        object Label79: TLabel
          Left = 24
          Top = 87
          Width = 59
          Height = 13
          Caption = 'Remote Port'
        end
        object Label80: TLabel
          Left = 24
          Top = 119
          Width = 48
          Height = 13
          Caption = 'Local Port'
        end
        object workasCB: TComboBox
          Left = 88
          Top = 17
          Width = 112
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'Server'
            'Client')
        end
        object tcpActiveCB: TComboBox
          Left = 291
          Top = 17
          Width = 112
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            'None'
            'WithAnyCharacter'
            'WithStartCharacter'
            'AutoStart')
        end
        object tcpRomteHostTB: TEdit
          Left = 96
          Top = 48
          Width = 103
          Height = 21
          TabOrder = 2
        end
        object tcpRemotePortNUD: TEdit
          Left = 96
          Top = 80
          Width = 103
          Height = 21
          TabOrder = 3
        end
        object tcpLocalPortNUD: TEdit
          Left = 96
          Top = 112
          Width = 103
          Height = 21
          TabOrder = 4
        end
        object Button26: TButton
          Left = 448
          Top = 76
          Width = 75
          Height = 25
          Caption = 'Get'
          TabOrder = 5
          OnClick = Button26Click
        end
        object Button27: TButton
          Left = 448
          Top = 108
          Width = 75
          Height = 25
          Caption = 'Set'
          TabOrder = 6
          OnClick = Button27Click
        end
      end
      object GroupBox48: TGroupBox
        Left = 5
        Top = 260
        Width = 538
        Height = 197
        Caption = 'Net work setting'
        TabOrder = 2
        object Label81: TLabel
          Left = 24
          Top = 24
          Width = 51
          Height = 13
          Caption = 'IP Address'
        end
        object Label82: TLabel
          Left = 24
          Top = 55
          Width = 34
          Height = 13
          Caption = 'Subnet'
        end
        object Label83: TLabel
          Left = 24
          Top = 86
          Width = 42
          Height = 13
          Caption = 'Gateway'
        end
        object Label84: TLabel
          Left = 25
          Top = 108
          Width = 103
          Height = 13
          Caption = 'Preferred DNS Server'
        end
        object Label85: TLabel
          Left = 24
          Top = 149
          Width = 102
          Height = 13
          Caption = 'Alternate DNS Server'
        end
        object Label86: TLabel
          Left = 216
          Top = 29
          Width = 62
          Height = 13
          Caption = 'Mac Address'
        end
        object ipTB: TEdit
          Left = 96
          Top = 17
          Width = 102
          Height = 21
          TabOrder = 0
        end
        object subnetTB: TEdit
          Left = 96
          Top = 48
          Width = 102
          Height = 21
          TabOrder = 1
        end
        object gatewayTB: TEdit
          Left = 96
          Top = 79
          Width = 102
          Height = 21
          TabOrder = 2
        end
        object pDNSTB: TEdit
          Left = 96
          Top = 124
          Width = 102
          Height = 21
          TabOrder = 3
        end
        object altDNSTB: TEdit
          Left = 96
          Top = 166
          Width = 102
          Height = 21
          TabOrder = 4
        end
        object macTB: TEdit
          Left = 217
          Top = 48
          Width = 182
          Height = 21
          Color = cl3DLight
          ReadOnly = True
          TabOrder = 5
        end
        object Button28: TButton
          Left = 448
          Top = 131
          Width = 75
          Height = 25
          Caption = 'Get'
          TabOrder = 6
          OnClick = Button28Click
        end
        object Button29: TButton
          Left = 448
          Top = 163
          Width = 75
          Height = 25
          Caption = 'Set'
          TabOrder = 7
          OnClick = Button29Click
        end
      end
      object GroupBox50: TGroupBox
        Left = 550
        Top = 316
        Width = 131
        Height = 86
        Caption = 'Change AT Mode'
        TabOrder = 3
        object Button30: TButton
          Left = 9
          Top = 18
          Width = 115
          Height = 25
          Caption = 'GOTO'
          TabOrder = 0
          OnClick = Button30Click
        end
        object Button31: TButton
          Left = 8
          Top = 52
          Width = 116
          Height = 25
          Caption = 'EXIT'
          TabOrder = 1
          OnClick = Button31Click
        end
      end
      object Button34: TButton
        Left = 558
        Top = 436
        Width = 115
        Height = 25
        Caption = 'Save and restart'
        TabOrder = 4
        OnClick = Button34Click
      end
      object GroupBox49: TGroupBox
        Left = 5
        Top = 461
        Width = 676
        Height = 159
        Caption = 'AT transparent command'
        TabOrder = 5
        object Label87: TLabel
          Left = 8
          Top = 24
          Width = 44
          Height = 13
          Caption = 'Time out:'
        end
        object Label88: TLabel
          Left = 138
          Top = 24
          Width = 29
          Height = 13
          Caption = '*10ms'
        end
        object ComboBox6: TComboBox
          Left = 72
          Top = 19
          Width = 65
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit16: TEdit
          Left = 176
          Top = 19
          Width = 356
          Height = 21
          TabOrder = 1
        end
        object Button32: TButton
          Left = 538
          Top = 16
          Width = 60
          Height = 25
          Caption = 'Send'
          TabOrder = 2
          OnClick = Button32Click
        end
        object Button33: TButton
          Left = 603
          Top = 16
          Width = 63
          Height = 25
          Caption = 'Clear'
          TabOrder = 3
          OnClick = Button33Click
        end
        object RichEdit1: TRichEdit
          Left = 9
          Top = 48
          Width = 657
          Height = 103
          Font.Charset = GB2312_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentFont = False
          TabOrder = 4
        end
      end
      object Button35: TButton
        Left = 558
        Top = 406
        Width = 115
        Height = 25
        Caption = 'Load default value'
        TabOrder = 6
        OnClick = Button35Click
      end
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 656
    Width = 785
    Height = 19
    AutoHint = True
    Panels = <
      item
        Width = 600
      end
      item
        Text = 'Port'
        Width = 56
      end
      item
        Text = 'statusManufacturer nameBarPanel1'
        Width = 200
      end>
  end
  object ActionList1: TActionList
    Left = 99
    Top = 18
    object Action_GetReaderInformation: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #33719#24471#35835#20889#22120#20449#24687
      OnExecute = Action_GetReaderInformationExecute
      OnUpdate = Action_GetReaderInformationUpdate
    end
    object Action_OpenCOM: TAction
      Category = #36890#35759
      Caption = #25171#24320#31471#21475
    end
    object Action_OpenRf: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #25171#24320#23556#39057
    end
    object Action_CloseCOM: TAction
      Category = #36890#35759
      Caption = #20851#38381#31471#21475
    end
    object Action_CloseRf: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20851#38381#23556#39057
    end
    object Action_WriteComAdr: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20889#20837#35835#20889#22120#22320#22336
    end
    object Action_WriteInventoryScanTime: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20889#20837
      Hint = #20889#20837#35810#26597#21629#20196#26368#22823#21709#24212#26102#38388
    end
    object Action_OpenTestMode: TAction
      Category = #27979#35797#27169#24335
      Caption = #26597#35810#26631#31614
    end
    object Action_CloseTestMode: TAction
      Category = #27979#35797#27169#24335
      Caption = #20851#38381#27979#35797#27169#24335
    end
    object Action_GetSystemInformation: TAction
      Tag = 2
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #33719#21462#30005#23376#26631#31614#35814#32454#20449#24687
    end
    object Action_SetReaderInformation: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35774#32622#21442#25968
      OnExecute = Action_SetReaderInformationExecute
    end
    object Action_SetReaderInformation_0: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #24674#22797#20986#21378#21442#25968
      OnExecute = Action_SetReaderInformationExecute
    end
    object Action_Inventory: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = 'Action_Inventory'
      OnExecute = Action_InventoryExecute
    end
    object Action_ShowOrChangeData: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35835
    end
    object Action_SetProtectState: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#20445#25252
      OnExecute = Action_SetProtectStateExecute
      OnUpdate = Action_SetProtectStateUpdate
    end
    object Action_DestroyCard: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #38144#27585
      OnExecute = Action_DestroyCardExecute
    end
    object Action_SelfTestMode: TAction
      Category = #27979#35797#27169#24335
      Caption = #36827#20837
    end
    object Action_SelfTestMode2: TAction
      Category = #27979#35797#27169#24335
      Caption = #36864#20986
    end
    object Action_RfOutput: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #25171#24320
    end
    object Action_RfOutput2: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20851#38381
    end
    object Action_SetDAC: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35843#25972'PWM'#20540
    end
    object Action_GetDAC: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35835#21462'PWM'#37197#32622
    end
    object Action_SetPowerParameter: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35774#23450#21151#29575#21442#25968
    end
    object Action_SolidifyPower: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #22266#21270#21151#29575#37197#32622
    end
    object Action_CheckPowerParameter: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #26816#27979#21151#29575#21442#25968#37197#32622
    end
    object Action_GetStartInformation: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35835#21462#21551#21160#20449#24687
    end
    object Action_ReadPowerParameter: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35835#21462#21151#29575#21442#25968
    end
    object Action_Inventroy_6B: TAction
      Category = '18000-6B'
      Caption = #26597#35810#26631#31614
      OnExecute = Action_Inventroy_6BExecute
    end
    object Action_Query_6B: TAction
      Category = '18000-6B'
      Caption = #26597#35810#26631#31614
      OnExecute = Action_Query_6BExecute
    end
    object Action_WriteData_6B: TAction
      Category = '18000-6B'
      Caption = #20889#25968#25454
    end
    object Action_ReadData_6B: TAction
      Category = '18000-6B'
      Caption = #35835#25968#25454
    end
    object Action_LockByte_6B: TAction
      Category = '18000-6B'
      Caption = #38145#23450
      OnExecute = Action_LockByte_6BExecute
    end
    object Action_CheckLock_6B: TAction
      Category = '18000-6B'
      Caption = #26816#27979#38145#23450
      OnExecute = Action_CheckLock_6BExecute
      OnUpdate = Action_CheckLock_6BUpdate
    end
    object Action_Query2_6B: TAction
      Category = '18000-6B'
      Caption = 'Action_Query2_6B'
    end
    object Action_ShowOrChangeData_write: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #20889
      OnExecute = Action_ShowOrChangeDataExecuteExecute
    end
    object Action_ShowOrChangeData_BlockErase: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #22359#25830#38500
      OnExecute = Action_ShowOrChangeDataExecuteExecute
    end
    object Action_SetReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#21333#24352#35835#20445#25252
      OnExecute = Action_SetReadProtect_G2Execute
    end
    object Action_RemoveReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35299#38500#21333#24352#35835#20445#25252
      OnExecute = Action_RemoveReadProtect_G2Execute
    end
    object Action_SetMultiReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#22810#24352#35835#20445#25252
      OnExecute = Action_SetMultiReadProtect_G2Execute
    end
    object Action_CheckReadProtected_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #26816#27979#21333#24352#34987#35835#20445#25252#65288#19981#38656#35201#21345#21495#23494#30721#65289'       '
      OnExecute = Action_CheckReadProtected_G2Execute
    end
    object Action_SetEASAlarm_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622
      OnExecute = Action_SetEASAlarm_G2Execute
    end
    object Action_CheckEASAlarm_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #26816#27979
    end
    object Action_WriteEPC_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #20889'EPC'
      OnExecute = Action_WriteEPC_G2Execute
    end
    object Action_LockUserBlock_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #38145#23450
      OnExecute = Action_LockUserBlock_G2Execute
    end
    object Action_WriteList: TAction
      Category = 'Inside'
      Caption = #21462#20540#20889#21442#25968
    end
    object Action_SolidifyPWMandPowerlist: TAction
      Category = 'Inside'
      Caption = #22266#21270'PWM'#20540#21644#21151#29575#34920
    end
    object Action_DeleteRfOutput: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #21024#38500#24378#21046'RF'#36755#20986
    end
    object Action_ShowOrChangeData_BlockWrite: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #22359#20889
      OnExecute = Action_ShowOrChangeDataExecuteExecute
    end
    object Action_TagProtocol: TAction
      Category = #20027#21160#27169#24335
      Caption = #35774#32622
      OnExecute = Action_TagProtocolExecute
      OnUpdate = Action_TagProtocolUpdate
    end
  end
  object Timer_Test_: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer_Test_Timer
    Left = 176
    Top = 18
  end
  object Timer_G2_Read: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer_G2_ReadTimer
    Left = 211
    Top = 18
  end
  object Timer_G2_Alarm: TTimer
    Interval = 200
    OnTimer = Timer_G2_AlarmTimer
    Left = 384
    Top = 18
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer1Timer
    Left = 338
    Top = 18
  end
  object Timer_Test_6B: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer_Test_6BTimer
    Left = 251
    Top = 18
  end
  object Timer_6B_ReadWrite: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer_6B_ReadWriteTimer
    Left = 290
    Top = 18
  end
  object MainMenu1: TMainMenu
    Left = 412
    Top = 24
    object Operation1: TMenuItem
      Caption = 'Operation'
      object Search1: TMenuItem
        Caption = 'Search'
        OnClick = Search1Click
      end
      object clear1: TMenuItem
        Caption = 'Clear'
        OnClick = clear1Click
      end
      object xxit1: TMenuItem
        Caption = 'Exit'
        OnClick = xxit1Click
      end
    end
    object tools1: TMenuItem
      Caption = 'tools'
      object IE1: TMenuItem
        Caption = 'IE'
        OnClick = IE1Click
      end
      object elnet1: TMenuItem
        Caption = 'Telnet'
        OnClick = elnet1Click
      end
      object Ping1: TMenuItem
        Caption = 'Ping'
        OnClick = Ping1Click
      end
    end
    object Language1: TMenuItem
      Caption = 'Language'
      object English1: TMenuItem
        Caption = 'English'
      end
    end
    object Help1: TMenuItem
      Caption = 'Help'
      object About1: TMenuItem
        Caption = 'About'
      end
    end
  end
end
