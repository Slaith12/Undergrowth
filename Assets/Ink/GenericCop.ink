INCLUDE DialogueTestFuncs.ink

~FadeInCharacter(1)
Hey, I'm a cop...
You got any plants you sellin'?
*   [Yup, here! (Sell Plant)]
    Hey, that's illegal!
    ~CopArrest()
*   [Nope, no plants here.]
    Alright, good. Stay out of trouble.
    ~FadeOutCharacter(1)
    ~Wait(0.4)
    -> END