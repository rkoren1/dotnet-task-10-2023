"Articles" aplikacija je backend aplikacija, ki omogoca vnos in urejanje artiklov ter omogoca komentiranje artiklov.

Tvoja naloga:

//ArticlesController
1. V metodi Post je napaka.
2. Implementiraj Put metodo, ki posodobi (update) Article entiteto, ter vrni posodobljeno entiteto.
3. Delete metoda ne odstrani Article zapisa iz baze.
4. BONUS naloga: izboljsaj search metodo, ker deluje pocasi.  

//CommentsController
1. Implementiraj metodo, ki vrne vse komentarje (Comment entitete) za doloceni artikel (Article entiteta).
2. Implementiraj metodo, ki vrne tocno doloceni komentar (Comment entiteta) za tocno doloceni artikel (Article entiteta).
3. Implementiraj metodo, ki omogoca dodajanje komentarjev (Comment entiteta) na artikel (Article entiteta).
4. Vse metode morajo biti dostopne na URLju "articles/{articleId}/comments".

Predpogoj:
- Kateri koli IDE (Visual studio, Visual studio code, ...)
- MSSQL ali MSSQLExpress ali MySQL >=5.5
- NET Core SDK 3.1

Razvojno okolje:
- Bodi pozoren, da imas nastavljen pravilen connection string v appsettings.json datoteki (trenutno je nastavljen za MSSQL na lokalni masini).
- Ce ne uporabljas Visual studia:
	- Postavi se v mapo Articles (mapa vsebuje datoteko Articles.csproj)
	- Pozeni: 
		- dotnet restore
		- dotnet build
		- dotnet run
