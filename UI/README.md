----

The UIManager script handles basic UI interactions in the game, including toggling UI panels and displaying temporary error messages using TextMeshPro with a fade-out effect.

Features

Panel Toggling
Easily enables or disables UI panels with a single method call.

Error Message Display
Shows temporary error messages to the player and smoothly fades them out.

Duplicate Error Prevention
Prevents multiple error messages from overlapping by checking if one is already active.

Dependencies

Unity (tested with Unity 2021+)

TextMeshPro for UI text rendering

Usage

Attach the UIManager script to a persistent UI GameObject (such as a Canvas manager).

Assign the following in the Inspector:

Error Text → A TextMeshProUGUI element used for displaying error messages.

This object should be disabled in the hierarchy by default.

Ensure the script exists in a scene that requires UI interactions.

Key Methods

Toggle(GameObject panel)
Toggles the active state of a given UI panel.

DisplayError(string message) (Coroutine)
Displays an error message for a short duration and fades it out smoothly.

----
