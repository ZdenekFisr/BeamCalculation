This Blazor application calculates mechanical properties, namely vertical shear force, bending moment and bending stress in a beam in 2D space loaded with vertical forces, moments or vertical continuous loads of either constant or linearly varying value. In real life, the beam can also represent shafts, hubs, masts (rotated in horizontal direction), vehicle axles etc. The beam can be fixed (supported) either at one point which takes out all three degrees of freedom or at two points, one of which allows rotation and the other axial movement, most notably thermal expansion. The beam can also have multiple cross-sections, each with its own bending modulus.

The calculation is performed at several points along the beam with a given distance between each pair. First, the reactions at the supports are calculated and are part of the result. Then, the mechanical parameters are calculated and the maximum values are selected from them. If a point experiences a sudden change in value (jump), it contains both values.

The computing is done on the client side using **Blazor WebAssembly**. The UI is written using **Radzen** components.

# Projects

## Application

Contains all the logic needed for calculation. Input objects use polymorphism. The `Point` is defined by interfaces, each representing one mechanical property for better testability. The `CalculationService` injects multiple services, each providing a part of the calculation result. Error states are handled by throwing custom exceptions.

## BeamCalculation (Blazor)

Contains some components and pages of the Blazor app.

## BeamCalculation.Client (Blazor)

Contains most of the components, pagas, services and other parts of the Blazor app. Most important contents are:

**Localization:** Allows user to switch language of all pages. The strings are all stored in CSV files with a key and phrases in all supported languages. The `EmbeddedCsvService` reads the key column and a selected language column and provides them as a dictionary. The pages and components then inject `LanguageService` which passes the retrieved dictionary to them. Each page starts its endpoint either with the language code or without, in which case the user is redirected to the English version.

**Plot:** Consists of the `PointTransformationService` which takes the points from the calculation result and transforms them into a collection of `PointForPlot` objects suitable for plotting.

**Components:** The `Calculation.razor` component contains the UI for defining the input and displaying the output of the calculation. Both parts are separated by a wide-as-screen `Calculate` button. Some parts of it are in separate components, most notably `ModulusDialog.razor` which allows for easier definition of the modulus if it's one of the basic shapes, and `ResultChart.razor` for plotting each of the mechanical properties.

## Test projects

All test projects are stored in the `Tests` folder. Each test project tests one code project and can contain either unit tests or integration tests. They all use `xUnit` and `FluentAssertions`.
