# Trait Reference

This document acts as a reference for the traits that are introduced in this mod.

| Done? | Name | Effect |
|:-:|:-:|:-:|
| No | Blazing | A more powerful version of a Valks card. Resets immolation to 0 when played. |
| No | Inferno | This card does not reduce immolation or end your turn when played. |
| No | Autoplay | This card is automatically played when drawn. [Note: maybe postfix OnDraw() to do a TryPlayCard() here - if unplayable, may need to override that in TryPlayCard()] |