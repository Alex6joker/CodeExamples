//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ExtCtrls.hpp>
#include <Vcl.Grids.hpp>
#include <Vcl.CategoryButtons.hpp>
#include <Vcl.ButtonGroup.hpp>
#include <Vcl.Menus.hpp>
#include <Vcl.Imaging.pngimage.hpp>
#include <Vcl.Dialogs.hpp>
#include <Vcl.CheckLst.hpp>
#include <Vcl.Buttons.hpp>
#include <Vcl.ComCtrls.hpp>
#include <Vcl.Graphics.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TImage *Image1;
	TLabel *Label3;
	TStringGrid *StringGrid1;
	TPanel *RightPanel;
	TPanel *Panel1;
	TImage *ImageSave;
	TImage *ImageOpen;
	TOpenDialog *OpenDialog1;
	TImage *ImageClear;
	TPanel *Panel2;
	TPanel *PanelCustomInput;
	TBitBtn *BitBtn7;
	TEdit *EditY;
	TEdit *EditX;
	TScrollBox *ScrollBox1;
	TSaveDialog *SaveDialog1;
	TPanel *PanelFunc6;
	TButton *Button6;
	TBitBtn *Cancel6;
	TImage *ImageColor6;
	TPanel *PanelFunc5;
	TImage *ImageColor5;
	TButton *Button5;
	TBitBtn *Cancel5;
	TPanel *PanelFunc4;
	TImage *ImageColor4;
	TButton *Button4;
	TBitBtn *Cancel4;
	TPanel *PanelFunc3;
	TImage *ImageColor3;
	TButton *Button3;
	TBitBtn *Cancel3;
	TPanel *PanelFunc2;
	TImage *ImageColor2;
	TButton *Button2;
	TBitBtn *Cancel2;
	TPanel *PanelFunc1;
	TImage *ImageColor1;
	TButton *Button1;
	TBitBtn *Cancel1;
	TImage *ImageMove;
	TPanel *PanelMove;
	TPanel *PanelDrag;
	TImage *ImageDrag;
	TPanel *PanelZoom;
	TImage *ImageZoom;
	TGroupBox *GroupBox1;
	TPanel *PanelSetBg;
	TImage *ImageBgUser;
	TButton *Button7;
	TButton *Button8;
	TButton *Button9;
	TOpenDialog *OpenDialog2;
	TButton *Button10;
	void __fastcall Image1Click(TObject *Sender);
	void __fastcall Image1MouseMove(TObject *Sender, TShiftState Shift, int X, int Y);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall ImageOpenClick(TObject *Sender);
	void __fastcall ImageSaveClick(TObject *Sender);
	void __fastcall ImageClearClick(TObject *Sender);
	void __fastcall Image1ContextPopup(TObject *Sender, TPoint &MousePos, bool &Handled);
	void __fastcall BitBtn7Click(TObject *Sender);
	void __fastcall FormResize(TObject *Sender);
	void __fastcall Cancel1Click(TObject *Sender);
	void __fastcall ImageMoveClick(TObject *Sender);
	void __fastcall ImageDragClick(TObject *Sender);
	void __fastcall Image1MouseDown(TObject *Sender, TMouseButton Button, TShiftState Shift,
          int X, int Y);
	void __fastcall Image1MouseUp(TObject *Sender, TMouseButton Button, TShiftState Shift,
          int X, int Y);
	void __fastcall ImageZoomClick(TObject *Sender);
	void __fastcall Button8Click(TObject *Sender);
	void __fastcall Button7Click(TObject *Sender);
	void __fastcall StringGrid1SelectCell(TObject *Sender, int ACol, int ARow, bool &CanSelect);
	void __fastcall StringGrid1SetEditText(TObject *Sender, int ACol, int ARow, const UnicodeString Value);
	void __fastcall Button10Click(TObject *Sender);







private:	// User declarations
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
