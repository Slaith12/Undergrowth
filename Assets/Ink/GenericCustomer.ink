INCLUDE DialogueTestFuncs.ink

~FadeInCharacter(1)
Hey there, I'm a standard customer! # speaker: 1 # portrait: Normal
Can I buy a plant please? # speaker: 1 # portrait: Normal
*   [Sure Thing! (Sell Plant)]
    Thanks! # speaker: 1 # portrait: Normal
*   [Nope, got nothing.]
    Aw, man. :( # speaker: 1 # portrait: Angry
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END