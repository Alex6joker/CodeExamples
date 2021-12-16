struct muz_akk 
{
	char name[8];
	char osn_ton[3];
	char tonal[8];
	int frequency;
	char razr_name[8];
	char gamma[5];
	int stupen;
	struct muz_akk *next;
	struct muz_akk *prev;
}*head,*tail,*error;
struct muz_akk* chtenie_s_file_diez(int);
struct muz_akk* chtenie_s_file_bemol(int);
void listing(struct muz_akk*,struct muz_akk*);
void listing_vybor(struct muz_akk*,struct muz_akk*);