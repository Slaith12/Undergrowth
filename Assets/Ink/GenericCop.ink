INCLUDE DialogueTestFuncs.ink

~FadeInCharacter(1)
Hey, I'm a cop...
You got any plants you sellin'?
*   [Yup, here! (Sell Plant)]
    Hey, that's illegal!
    //Cop seizes plants or whatever (I don't have a function for that yet)
    -> END
*   [Nope, no plants here.]
    Alright, good. Stay out of trouble.
    ~FadeOutCharacter(1)
    ~Wait(0.4)
    -> END