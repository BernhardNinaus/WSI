# Projektarbeit WSI

## Angabe

Ziel der Projektarbeit im Rahmen dieser Vorlesung ist es, eine moderne, flexibel erweiterbare, austauschbare und skalierbare Microservice-Architektur zu designen und mit Microsot ASP.NET Core zu entwickeln.

Das Projekt kann ein beliebiges Thema behandeln. Sie könnten zum Beispiel Services für einen Kinobetreiber implmentieren oder auch Services im Zusammenahng mit „Betrieb einer Fachhochschule“ erstellen.

### Focus
> Microservices a definition of this new architectural term  
> http://martinfowler.com/articles/microservices.html

### Aufgaben
 1. - [X] Beschreiben Sie mindestens 3 Services inkl. Bounded Context, Context Map, Datenmodel und Datenvalidierung

 2. - [X] Entwicklen Sie mindestens einen ASP.NET Core Web.API  Controller, welcher CRUD-Funktionaltitäten zur Verfügung stellt. Beschreiben Sie die REST-Prinzipien im Zusammenhang mit Ihrem Projekt.

 3. - [X] Beschreiben Sie die OpenAPI-Spec Ihres Service. Stellen Sie IDL, WSDL und OpenApi-Spec gegenüber.

 4. - [X] Erstellen Sie eine Service-Klasse, welche per Dependency Injection aus Ihrem Service aufgerufen wird.

 5. - [ ] Erstellen Sie eine Client-Applikation, welche Ihr erstelltes Service verwendet. Die Technologie bleibt dabei Ihnen überlassen (C#-Console-App, Java, JavaScript, Python,..).

 6. - [X] Beschreiben Sie das Thema Routing im Allgemeinen. Definieren Sie mindestesn eine Route, welche nicht dem ASP.NET Core-Standard entspricht

 7. - [X] Verwenden Sie Einträge aus der „appsettings.json“.
   
 8. - [ ] Aufbereitung und Präsentation

 9. - [ ] Funktionierende Gesamtlösung

 10. - [ ] Entwickeln Sie einen weiteren ASP.NET Core Web.API  Controller, bei welchem die einzelnen Methoden (GET, POST, PUT,DELETE) nur mit einem gültigen API Key aufgerufen werden dürfen. Demonstrieren Sie sowohl das Verhalten bei Verwendung eines gültigen API Keys als auch das Verhalten bei Verwendung eines falschen API Keys. Vergleichen Sie den Einsatz von API Keys mit OAuth und beschreiben Sie mögliche Anwendungsszenarien und Vor- und Nachteile

Aufgabe eins bis neun geben jeweils 10 Punkte, die zehnte Aufgabe gibt 20 Punkt.

## Projekt definition

In diesem Projekt wird ein Services erstellt und zwei weiterere werden erklärt.
 - Kommentar-Service (wird umgesetzt)
   - In diesem Service können Kommentare-Funktionen in verschiedenster Art und Weise verwendet werden.
   - Diese Kommentare werden zu externen Ressource oder zu anderen Kommentaren gespeichert.
   - Die Kommentare werden auf Inhalt überprüft.
     - Inhaltüberprüfung wird vielleicht später mit AI/ML implementiert.
   - Dieseer Service wird nur minmal umgesetzt, weitere Funktionen werden vielleicht einmal umgesetzt.
 - Image-Service (nur Beschreibung)
   - In diesem Service können Bilder gesichert werden.
   - Die Bilder werden auf Inhalt überprüft und sind dann in verschiedenen Größer aufrufbar.
     - Inhaltüberprüfung wird vielleicht später mit AI/ML implementiert.
 - Translation-Service (nur Beschreibung)
   - In diesem Service werden Sätze von einer zu einer andere übersetzt.

Es wird ein einfacher JS-Client geschrieben um den Kommentarservice zu verwenden.


## Ideen zu den Services

### Kommentar-Service
 - Nur als Erstellen/Lesen/Löschen
 - Reddit like Kommentieren oder nur chronologisch
 - Maximallänge
 - Verschiedene Formate (Rich, Text, Html, Md)
   - Automatische konvertierung
   - XSS?
   - HTML
     - Nur bestimmte Tags und Attribute?
 - Suchfunktion um alle Kommtare unter einer Ressource zu finden.
  - Verwendung bei Forum-Service
 - Löschung/Bearbeitung diese Kommentars mit weiterer Guid.

### Image-Service
 - Presist or TTL
 - Konvertieren in verschiedene Größen/Formaten
   - Thumbnail
 - CDN-Funktionalität um anderen Service zu verwenden