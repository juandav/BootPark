unit UHFReader28_Head;

interface
uses DevControl;
   Const
    COM1 = 1;
    COM2 = 2;
    COM3 = 3;
    COM4 = 4;
    COM5 = 5;
    COM6 = 6;
    COM7 = 7;
    COM8 = 8;
    COM9 = 9;
    COM10 = 10;


    OK			    	= $00;

    NoElectronicTag     = $0e;
    OperationError      = $0f;



    OtherError             = $00;//��������
    MemoryOutPcNotSupport  = $03;//�洢�����޻򲻱�֧�ֵ�PCֵ
    MemoryLocked           = $04;//�洢������
    NoPower                = $0b;//��Դ����
    NotSpecialError        = $0f;//���ض�����


    CmdNotIdentify                = $02;
    OperationNotSupport_          = $03;
    UnknownError                  = $0f;


    ReturnEarly            = $0E;//ѯ��ʱ�����ǰ����
  	TimeOut                = $0B;// ָ����ѯ��ʱ�����
  	MoreData               = $0D; //������Ϣ֮�󣬻�����Ϣ
  	MCUFull                = $0C;// ��дģ��洢�ռ�����
  	AbnormalCommunication  = $02;
//  	PasswordError          = $03;//�����������
//  	NoTagOperation         = $04;
//  	ParameterError         = $FF;


    
    CommunicationErr = $30;
    RetCRCErr        = $31;
    RetDataErr       = $32;    //���ݳ����д���
    CommunicationBusy= $33;
    ExecuteCmdBusy   = $34;
    ComPortOpened    = $35;
    ComPortClose     = $36;
    InvalidHandle    = $37;
    InvalidPort      = $38;
    RecmdErr         = $EE;    //����ָ�����



    InventoryReturnEarly_G2   = $01;//ѯ��ʱ�����ǰ����
  	InventoryTimeOut_G2       = $02;// ָ����ѯ��ʱ�����
  	InventoryMoreData_G2      = $03; //������Ϣ֮�󣬻�����Ϣ
    ReadermoduleMCUFull_G2    = $04;// ��дģ��洢�ռ�����
  	AccessPasswordError          = $05;//�����������
 //   AccessPasswordErrorWriteALittle =$06;   //�����������ֻ�в�������д��
 //   PoorCommunicationWriteALittle =$07;//ͨѶ������ֻ�в�������д��
 //   TagReturnErrorWriteALittle=$08; //���ӱ�ǩ���ش�����룬��ֻд���˲�������
    DestroyPasswordError=$09; // �����������
    DestroyPasswordCannotZero=$0a; //�������벻��Ϊȫ0
    TagNotSupportCMD=$0b;// ���ӱ�ǩ��֧�ָ�����
    AccessPasswordCannotZero=$0c;// �Ը�����������벻��Ϊȫ0
    TagProtectedCannotSetAgain=$0d;//���ӱ�ǩ�Ѿ��������˶������������ٴ�����
    TagNoProtectedDonotNeedUnlock=$0e;//  ���ӱ�ǩû�б����ö�����������Ҫ����
  //  PoorCommunicationWriteFail=$0f;//ͨѶ����, д��ʧ��
    ByteLockedWriteFail=$10;//  ���ֽڿռ䱻������д��ʧ��
    CannotLock=$11;// ��������
    LockedCannotLockAgain=$12;// �Ѿ������������ٴ�����
    ParameterSaveFailCanUseBeforeNoPower=$13;// ��������ʧ��,�����õ�ֵ�ڶ�дģ��ϵ�ǰ��Ч
    CannotAdjust=$14;//�޷�����
    InventoryReturnEarly_6B=$15;// ѯ��ʱ�����ǰ����
    InventoryTimeOut_6B=$16;//ָ����ѯ��ʱ�����
    InventoryMoreData_6B=$17;// ������Ϣ֮�󣬻�����Ϣ
    ReadermoduleMCUFull_6B=$18;// ��дģ��洢�ռ�����
    NotSupportCMDOrAccessPasswordCannotZero=$19;  //���Ӳ�֧�ָ�������߷������벻��Ϊ0
    CMDExecuteErr=$F9;// ����ִ�г���
    GetTagPoorCommunicationCannotOperation=$FA; //�е��ӱ�ǩ����ͨ�Ų������޷�����
    NoTagOperation=$FB; //�޵��ӱ�ǩ�ɲ���
    TagReturnErrorCode=$FC;// ���ӱ�ǩ���ش������
    CMDLengthWrong=$FD;// ����ȴ���
    IllegalCMD=$FE;//���Ϸ�������
    ParameterError=$FF;// ��������


  //	NoTagOperation         = $04;
  //	ParameterError         = $FF;

  //   	AbnormalCommunication  = $02;


    Function UHFReader28_GetErrorCodeDesc(errorCode : Byte) : String;
    Function UHFReader28_GetReturnCodeDesc(retCode : Byte) : String;
    Function GetDeviceErrorCodeDesc(errorCode : Byte) : String;
    Function GetDeviceErrorTypeDesc(errorCode : TagErrorCode) : String;
implementation


Function UHFReader28_GetErrorCodeDesc(errorCode : Byte) : String;
begin
    result := '';
    case errorCode of
       OtherError            : result := 'Other error';
        MemoryOutPcNotSupport : result := 'Memory out or pc not support';
        MemoryLocked          : result := 'Memory Locked and unwritable';
        NoPower               : result := 'No Power,memory write operation cannot be executed';
        NotSpecialError       : result := 'Not Special Error,tag not support special errorcode';

    end;
end;

Function UHFReader28_GetReturnCodeDesc(retCode : Byte) : String;
begin
    result := '';
    case retCode of
        InventoryReturnEarly_G2               : result := 'Return before Inventory finished';
        InventoryTimeOut_G2                   : result := 'the Inventory-scan-time overflow';
        InventoryMoreData_G2                  : result := 'More Data';
        ReadermoduleMCUFull_G2                : result := 'Reader module MCU is Full';
        AccessPasswordError                   : result := 'Access Password Error';
        DestroyPasswordError                  : result := 'Destroy Password Error';
        DestroyPasswordCannotZero             : result := 'Destroy Password Error Cannot be Zero';
        TagNotSupportCMD                      : result := 'Tag Not Support the command';
        AccessPasswordCannotZero              : result := 'Use the commmand,Access Password Cannot be Zero';
        TagProtectedCannotSetAgain            : result := 'Tag is protected,cannot set it again';
        TagNoProtectedDonotNeedUnlock         : result := 'Tag is unprotected,no need to reset it';
        ByteLockedWriteFail                   : result := 'There is some locked bytes,write fail';
        CannotLock                            : result := 'can not lock it';
        LockedCannotLockAgain                 : result := 'is locked,cannot lock it again';
        ParameterSaveFailCanUseBeforeNoPower  : result := 'Parameter Save Fail,Can Use Before Power';
        CannotAdjust                          : result := 'Cannot adjust';
        InventoryReturnEarly_6B               : result := 'Return before Inventory finished';
        InventoryTimeOut_6B                   : result := 'Inventory-Scan-Time overflow';
        InventoryMoreData_6B                  : result := 'More Data';
        ReadermoduleMCUFull_6B                : result := 'Reader module MCU is full';
        NotSupportCMDOrAccessPasswordCannotZero : result := 'Not Support Command Or AccessPassword Cannot be Zero';
        GetTagPoorCommunicationCannotOperation: result := 'Get Tag,Poor Communication,Inoperable';
        NoTagOperation                        : result := 'No Tag Operable';
        TagReturnErrorCode                    : result := 'Tag Return ErrorCode';
        CMDLengthWrong                        : result := 'Command length wrong';
        IllegalCMD                            : result := 'Illegal command';
        ParameterError                        : result := 'Parameter Error';


        RecmdErr            : result := 'Return command error';
        CommunicationErr    : result := 'Communication error';
        RetCRCErr           : result := 'CRC checksummat error';
        RetDataErr          : result := 'Return data length error';
        CommunicationBusy   : result := 'Communication busy';
        ExecuteCmdBusy      : result := 'Busy,command is being executed';
        ComPortOpened       : result := 'ComPort Opened';
        ComPortClose        : result := 'ComPort Closed';
        InvalidHandle       : result := 'Invalid Handle';
        InvalidPort         : result := 'Invalid Port';
    end;
end;
Function GetDeviceErrorCodeDesc(errorCode : Byte) : String;
begin
   result := '';
   case errorCode of
    0: Result:= 'Success!';
    1: Result:= 'Input configuration parameter invalid!';
    2: Result:= 'No login!';
    3: Result:= 'Authenticate fail!';
    4: Result:= 'Socket error!';
    5: Result:= 'Not enough memory!';
    6: Result:= 'Device no response!';
    7: Result:= 'Function argument error!';
    8: Result:= 'Error response from device!';
    9: Result:= 'Operation is not supported!';
   end;
end;

Function GetDeviceErrorTypeDesc(errorCode : TagErrorCode) : String;
begin
   result := '';
   case errorCode of
    DM_ERR_OK: Result:= 'Success!';
    DM_ERR_PARA: Result:= 'Input configuration parameter invalid!';
    DM_ERR_NOAUTH: Result:= 'No login!';
    DM_ERR_AUTHFAIL: Result:= 'Authenticate fail!';
    DM_ERR_SOCKET: Result:= 'Socket error!';
    DM_ERR_MEM: Result:= 'Not enough memory!';
    DM_ERR_TIMEOUT: Result:= 'Device no response!';
    DM_ERR_ARG: Result:= 'Function argument error!';
    DM_ERR_MATCH: Result:= 'Error response from device!';
    DM_ERR_MAX: Result:= 'Operation is not supported!';
   end;
end;

end.