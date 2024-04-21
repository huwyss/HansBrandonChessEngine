Bemerkungen HU

Analyse vom 30.1.2022
- HansBrandon ist eine King Capture Engine, das heisst ...
     * der Move Generator liefert auch Züge, bei denen der König im Schach steht. Wenn kein König vorhanden ist, so liefert er keine Züge zurück.
     * Die Evaluation liefert -10000 zurück wenn der weisse König fehlt. +10000 wenn der schwarze König fehlt.
     * Die Minimax Suche merkt erst wenn der König fehlt, dass die Partie zu ende ist und nicht schon wenn die Partie patt oder Schachmatt ist.
       Wenn die Suche also merkt dass der König fehlt, dann war der letzte Halbzug illegal und die Partie war schon vor dem Halbzug zu Ende
       Wenn zb der schwarze König fehlt, dann war schon vor dem Halbzug weiss der Gewinner und schwarz war im Schachmatt.
     * In der Suche stimmt es noch nicht. Es wird wohl nicht korrekt unterschieden zwischen
          - Stellung ist jetzt patt
          - Stellung ist jetzt schachmatt
          - Patt in 1/2/3 Halbzügen
          - Schachmatt in 1/2/3 Halbzügen
          *** WEIL ICH DIE REKURSION nicht richtig durchschaue. ***


- Man könnte den MoveGenerator so schreiben, dass er nur gültige Züge liefert. Dazu müsste aber
     * für jeden Zug noch die Antwort berechnet werden. Es reicht nicht wenn die angegriffenen Felder berechnet werden und dann alle Züge des Königs,
       die auf ein nicht angegriffenes Feld führen als legal betrachtet werden. Es gibt auch Züge, bei denen eine Figur, die den König abschirmte weggeht
       und nun einen Angriff auf den eigenen König zulässt.
- Vermutlich würde aber so die Suche einfacher.