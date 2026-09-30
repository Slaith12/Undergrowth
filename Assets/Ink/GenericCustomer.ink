INCLUDE DialogueTestFuncs.ink

~FadeInCharacter(1)
Hey there, I'm a standard customer!
Can I buy a plant please?
*   [Sure Thing! (Sell Plant)]
    Thanks!
*   [Nope, got nothing.]
    Aw, man. :(
- ~FadeOutCharacter(1)
~Wait(0.4)
-> END