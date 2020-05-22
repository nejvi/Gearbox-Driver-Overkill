# Gearbox Driver Overkill
Implementacja automatycznej skrzyni biegów na konkurs devupgrade.online.

## O rozwiązaniu
Całe rozwiązanie zrealizowaliśmy w około 4 dni (we dwójkę, co czyni 8 ludziodni) - cała praca odbywała się w trakcie połączenia na Skype ze screensharingiem. Jesteśmy znajomymi z różnych firm i jest to nasze pierwsze wspólne kodowanie (prozowanie). 

Implementację rozpoczęliśmy z jednym bazowym założeniem - *to ma działać*.
W tym celu w ciągu 5 godzinnej sesji na miro i seansów dokonaliśmy naszego pierwszego "event stormingu", który wyszedł raczej słabo (wiele akcji prowadziło do tych samych zdarzeń - zmiany biegu, etc.), natomiast bardzo przyjemnie zorganizowały nam się w ten sposób wymagania, a także dało nam to dobre spojrzenie na eksplorację problemu.

[Tablica w miro, która jest efektem naszej eksploracji](https://miro.com/app/board/o9J_kstqgEU=/)

Eksploracja problemu pokazała nam, że bardzo duża część feature'ów wymaga bardziej 'aplikacyjnego' podejścia - odkryliśmy liczne sprzężenia i zaczęliśmy zadawać pytania typu - kiedy po kickdown bieg ma wrócić do poprzedniego? Stwierdziliśmy, że potrzebujemy procesów (process managerów, sag...), do tego trzeba je napędzić eventami.

W związku z tym zaczęliśmy od implementacji dosyć rozbudowanej warstwy przeciwdegradacyjnej (projekt .ACL). Dodatkowo zastosowaliśmy tam mocne odwrócenie sterowania - pętla while wyczytuje wartości sensorów i zamienia je na zdarzenia, czyniąc cały flow asynchronicznym.

Eventy są interpretowane przez procesy (projekt .Processes). Odkryliśmy, że procesy mogą się ze sobą kłócić (kickdown może chcieć zmienić bieg, ale w tym czasie może mieć efekt tryb MDynamic, a do tego ciągniemy przyczepę) i istnieje pewna hierarchia. Powstał w tym celu negocjator, aby taką hierarchię pilnować. Mamy pewne wątpliwości co do jego roli.

Pod wpływem działania procesów i negocjatora, wynegocjowany zostaje program zmiany biegów, który jest wstrzykiwany do mechanizmu skrzyni (projekt Gearshift).

Projekt CabinControls zbiera wejście od użytkownika, jest tam mała zabawa z agregatami i prostym readmodelem, którego do tej pory jednak nie wykorzystaliśmy.

Całość zwieńczona jest programem demo, na którego potrzeby napisaliśmy bardzo prosty emulator silnika, który symuluje spadki Rpm przy zrzucaniu biegu, a także ich wzrosty przy dociskaniu/odpuszczaniu gazu.

Program demo demonstruje większość feature'ów.

Disclaimer - Żaden z nas nie stosował DDD komercyjnie i nasza wiedza jest raczej ograniczona (posiadamy ok. 3,5 i 1,5 roku doświadczenia), raczej nie należy stosować tego rozwiązania w celach edukacyjnych, gdyż niektórych technik i nazewnictw mogliśmy użyć błędnie  
Disclaimer 2 - Oprogramowanie zostało zostało dostarczone w stanie "jak jest". Nie bierzemy odpowiedzialności za katastrofy lądowe powstałe w wyniku wdrożenia. Przed użyciem skontaktuj się z administratorem lub developerem.

## Twórcy
Michał Wityk,  
Maciej Białobrzeski

![](gearbox_meme.png)
