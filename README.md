# Platform Game - Kenuja

## Over dit project
Dit is een 2D platformgame gemaakt in C# met MonoGame. Het project voldoet volledig aan de examenvereisten en is ontwikkeld volgens de SOLID-principes.  

---

## Examenvereisten
| Werken met GitHub                                  | ✅ | Project staat in een GitHub repository en er is een invite gestuurd naar de begeleider. |
| Startscherm met startknop                          | ✅ | Het spel start in een menu (`GameState.Menu`) en kan worden gestart met Enter. |
| Geanimeerde held                                   | ✅ | De speler kan bewegen, springen en aanvallen. Animatie van bewegen/springen kan worden uitgebreid. |
| Minstens 2 levels                                  | ✅ | Level 1 en Level 2 zijn aanwezig. Levels laden opeenvolgend via ScoreScreen. |
| Minstens 3 verschillende vijanden (max 1 valstrik) | ✅ | Er zijn `PatrolEnemy`, `FastEnemy` en 1 `TrapEnemy`. |
| Keyboard input (up, down, left, right)             | ✅ | Alle vereiste toetsen worden ondersteund via `IInputHandler`. |
| Game Over scherm                                   | ✅ | GameOver en GameEnd schermen zijn aanwezig en tonen score. |
| Basic physics (springen, vallen, platforms)        | ✅ | De speler kan springen en vallen; platforms blokkeren de val. |
| Basic AI                                           | ✅ | Vijanden bewegen volgens hun gedrag; valstrik blijft staan. |
| Sprites en Tileset / Background                    | ✅ | Alle gameobjecten hebben sprites en een background. Er zijn geen blokjes die over de background bewegen. |
| SOLID programming                                  | ✅ | Code volgt de SOLID-principes: <br> - **S:** Single responsibility voor GameWorld, UIManager, InputHandler, Game1 <br> - **O:** Open voor uitbreiding, bv. nieuwe vijanden via abstract Enemy <br> - **L:** Liskov Substitution toegepast bij Enemy subclasses <br> - **I:** Interface segregation via IGameObject, IInputHandler, ILevelManager <br> - **D:** Dependency Injection toegepast voor GameWorld en InputHandler |

---

## Redenering
Dit project is volledig opgebouwd volgens objectgeoriënteerd programmeren en moderne softwareprincipes:

1. **Uitbreidbaarheid:** Nieuwe levels, vijanden en items kunnen makkelijk worden toegevoegd zonder bestaande code te breken.  
2. **Onderhoudbaarheid:** Elke klasse heeft een duidelijke verantwoordelijkheid en logica is opgesplitst in aparte modules.  
3. **Testbaarheid:** Dankzij dependency injection kan bijvoorbeeld `GameWorld` of `InputHandler` gemockt worden voor testen.  
4. **Gameplay:** Speler kan springen, vallen, aanvallen en interactie met coins, vijanden en eindflag is correct geïmplementeerd.  

---

## Gebruikte technologieën
- C#  
- MonoGame  
- GitHub (versiebeheer)  

---

## Instructies om te spelen
1. Clone de repository:  
```bash
git clone https://github.com/Kenu-94/Platform_Game_Kenuja.git

2. Open het project in Visual Studio.
3. Druk op Run of F5 om het spel te starten.
4. Gebruik de pijltjestoetsen om te bewegen en Enter om menu-opties te bevestigen.
5. Verzamel coins, vermijd vijanden en bereik de eindflag om het level te voltooien.
